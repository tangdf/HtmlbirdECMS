// ===============================================================================
//  产品名称：网鸟电子商务管理系统(Htmlbird ECMS)
//  产品作者：YMind Chan
//  版权所有：网鸟IT技术论坛 颜铭工作室
// ===============================================================================
//  Copyright © Htmlbird.Net. All rights reserved .
//  官方网站：http://www.htmlbird.net/
//  技术论坛：http://bbs.htmlbird.net/
// ===============================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Net.Htmlbird.Framework.Modules;
using Net.Htmlbird.Framework.Utilities;

namespace Net.Htmlbird.Framework.Web.Configuration
{
	/// <summary>
	/// 数据库配置信息管理器。
	/// </summary>
	public static class DatabaseConfiguration
	{
		public const string DatabaseConfigPath = @"~/App_Data/Configuration/Database/";
		private static readonly object _asyncObject = new object();
		private static readonly FileSystemWatcher _watcher;
		private static HashSet<IniFile> _sqlServerConfigurationFiles;
		private static HashSet<IniFile> _mySqlConfigurationFiles;
		private static HashSet<IniFile> _sqLiteConfigurationFiles;
		private static HashSet<IniFile> _mongoDBConfigurationFiles;

		public static string PhysicalDatabaseConfigPath = PathUtils.MapPath(DatabaseConfigPath);

		/// <summary>
		/// 初始化 <see cref="DatabaseConfiguration"/> 对象。
		/// </summary>
		static DatabaseConfiguration()
		{
			if (Directory.Exists(PhysicalDatabaseConfigPath) == false) return;

			_LoadConfigs();

			_watcher = new FileSystemWatcher(PhysicalDatabaseConfigPath, "*.ini");
			_watcher.Created += _WatcherCreated;
			_watcher.Deleted += _WatcherDeleted;
			_watcher.Renamed += _WatcherRenamed;
			_watcher.NotifyFilter = NotifyFilters.FileName;
			_watcher.EnableRaisingEvents = true;
		}

		/// <summary>
		/// 获取 SQL Server 数据库连接配置信息的集合。
		/// </summary>
		public static HashSet<DatabaseConnectionString> SQLServerConnectionStrings { get; private set; }

		/// <summary>
		/// 获取 MySql 数据库连接配置信息的集合。
		/// </summary>
		public static HashSet<DatabaseConnectionString> MySqlConnectionStrings { get; private set; }

		/// <summary>
		/// 获取 SQLite 数据库连接配置信息的集合。
		/// </summary>
		public static HashSet<DatabaseConnectionString> SQLiteConnectionStrings { get; private set; }

		/// <summary>
		/// 获取 MongoDB 数据库连接配置信息的集合。
		/// </summary>
		public static HashSet<DatabaseConnectionString> MongoDBConnectionStrings { get; private set; }

		/// <summary>
		/// 获取 SQL Server 数据库连接配置文档的集合。
		/// </summary>
		public static HashSet<IniFile> SQLServerConfigurationFiles { get { return _sqlServerConfigurationFiles ?? (_sqlServerConfigurationFiles = _GetDatabaseConfigurationFiles(Path.Combine(PhysicalDatabaseConfigPath, "SqlServer"))); } }

		/// <summary>
		/// 获取 MySql 数据库连接配置文档的集合。
		/// </summary>
		public static HashSet<IniFile> MySqlConfigurationFiles { get { return _mySqlConfigurationFiles ?? (_mySqlConfigurationFiles = _GetDatabaseConfigurationFiles(Path.Combine(PhysicalDatabaseConfigPath, "MySql"))); } }

		/// <summary>
		/// 获取 SQLite 数据库连接配置文档的集合。
		/// </summary>
		public static HashSet<IniFile> SQLiteConfigurationFiles { get { return _sqLiteConfigurationFiles ?? (_sqLiteConfigurationFiles = _GetDatabaseConfigurationFiles(Path.Combine(PhysicalDatabaseConfigPath, "SQLite"))); } }

		/// <summary>
		/// 获取 MongoDB 数据库连接配置文档的集合。
		/// </summary>
		public static HashSet<IniFile> MongoDBConfigurationFiles { get { return _mongoDBConfigurationFiles ?? (_mongoDBConfigurationFiles = _GetDatabaseConfigurationFiles(Path.Combine(PhysicalDatabaseConfigPath, "MongoDB"))); } }

		/// <summary>
		/// 返回数据库连接配置信息。
		/// </summary>
		/// <param name="databaseConnectionStrings">数据库连接配置信息的集合。</param>
		/// <param name="groupName">分组名称。</param>
		/// <returns>如果找到至少一个匹配项则随机从中返回一个可用项，否则返回 null。</returns>
		public static DatabaseConnectionString GetConnectionStrings(HashSet<DatabaseConnectionString> databaseConnectionStrings, string groupName)
		{
			if (databaseConnectionStrings == null || databaseConnectionStrings.Count == 0) return null;
			if (String.IsNullOrEmpty(groupName)) return null;

			var connList = databaseConnectionStrings.AsParallel().Where(item => item.GroupName == groupName).ToList();

			return connList.Count == 0 ? new DatabaseConnectionString
			{
				ConnectionStrings = String.Empty,
				GroupName = "DefaultGroup",
				Name = "Default"
			} : connList[StringUtils.Random.Next(0, connList.Count)];
		}

		public static void Reload(string path)
		{
			if (Directory.Exists(path) == false) throw new DirectoryNotFoundException();

			PhysicalDatabaseConfigPath = path;

			_LoadConfigs();
		}

		private static void _LoadConfigs()
		{
			lock (_asyncObject)
			{
				_sqlServerConfigurationFiles = null;
				_mySqlConfigurationFiles = null;
				_sqLiteConfigurationFiles = null;
				_mongoDBConfigurationFiles = null;

				SQLServerConnectionStrings = _LoadConfigs(SQLServerConfigurationFiles);
				MySqlConnectionStrings = _LoadConfigs(MySqlConfigurationFiles);
				SQLiteConnectionStrings = _LoadConfigs(SQLiteConfigurationFiles);
				MongoDBConnectionStrings = _LoadConfigs(MongoDBConfigurationFiles);
			}
		}

		private static HashSet<DatabaseConnectionString> _LoadConfigs(ICollection<IniFile> configurationFiles)
		{
			if (configurationFiles == null || configurationFiles.Count == 0) return new HashSet<DatabaseConnectionString>();

			var databaseConnectionStrings = new HashSet<DatabaseConnectionString>();

			foreach (var iniFile in configurationFiles)
			{
				foreach (var section in iniFile.Sections)
				{
					foreach (var sectionItem in section.Items)
					{
						databaseConnectionStrings.Add(new DatabaseConnectionString {
							Name = sectionItem.Key,
							GroupName = section.Name,
							ConnectionStrings = sectionItem.Value
						});
					}
				}
			}

			return databaseConnectionStrings;
		}

		private static void _WatcherRenamed(object sender, RenamedEventArgs e) { _LoadConfigs(); }
		private static void _WatcherDeleted(object sender, FileSystemEventArgs e) { _LoadConfigs(); }
		private static void _WatcherCreated(object sender, FileSystemEventArgs e) { _LoadConfigs(); }

		private static HashSet<IniFile> _GetDatabaseConfigurationFiles(string path)
		{
			var iniFiles = new HashSet<IniFile>();
			var fileList = Directory.GetFiles(path, "*.ini", SearchOption.AllDirectories);

			foreach (var fileName in fileList) iniFiles.Add(new IniFile(fileName, Encoding.UTF8));

			return iniFiles;
		}
	}
}