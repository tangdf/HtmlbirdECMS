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
using System.Text;
using Net.Htmlbird.oSo;

namespace Net.Htmlbird.Framework.Web.Data
{
	/// <summary>
	/// 表示 HtmlbirdECMS 数据访问层的为 Sql 数据库提供访问支持的基类。
	/// </summary>
	public abstract class SqlDALBase
	{
		#region 构造函数

		/// <summary>
		/// 初始化 <see cref="SqlDALBase"/> 类的新实例。
		/// </summary>
		/// <param name="connectionString">数据库连接字符串。</param>
		/// <param name="sqlProvider">用于访问基础数据存储区的 ADO.NET 提供程序的名称。</param>
		protected SqlDALBase(string connectionString, string sqlProvider)
		{
			if (connectionString == null) throw new ArgumentNullException("connectionString");
			if (String.IsNullOrWhiteSpace(connectionString)) throw new ArgumentOutOfRangeException("connectionString");

			if (sqlProvider == null) throw new ArgumentNullException("sqlProvider");
			if (String.IsNullOrWhiteSpace(sqlProvider)) throw new ArgumentOutOfRangeException("sqlProvider");

			this.ConnectionString = connectionString;
			this.SqlProvider = sqlProvider;
		}

		#endregion

		#region 受保护方法

		/// <summary>
		/// 通过指定存储过程名称获取查询构造器。
		/// </summary>
		/// <param name="procName">指定要查询的存储过程名称。</param>
		/// <returns>返回 <see cref="QueryBuilder&lt;T&gt;"/> 的实例。</returns>
		protected QueryBuilder<StoredProcedure> StoredProcedure(string procName) { return SimpleSql.StoredProcedure(procName, this.ConnectionString, this.SqlProvider); }

		/// <summary>
		/// 通过指定 Sql 查询语句获取查询构造器。
		/// </summary>
		/// <param name="sqlBuilder">指定包含 Sql 查询语句的字符串。</param>
		/// <returns>返回 <see cref="QueryBuilder&lt;Query&gt;"/> 的实例。</returns>
		protected QueryBuilder<Query> Query(StringBuilder sqlBuilder) { return SimpleSql.Query(sqlBuilder.ToString(), this.ConnectionString, this.SqlProvider); }

		/// <summary>
		/// 通过指定 Sql 查询语句获取查询构造器。
		/// </summary>
		/// <param name="sqlStatement">指定包含 Sql 查询语句的字符串。</param>
		/// <returns>返回 <see cref="QueryBuilder&lt;Query&gt;"/> 的实例。</returns>
		protected QueryBuilder<Query> Query(string sqlStatement) { return SimpleSql.Query(sqlStatement, this.ConnectionString, this.SqlProvider); }

		/// <summary>
		/// 为多个查询创建简单的事务。
		/// </summary>
		/// <returns>返回 <see cref="SimpleTransaction"/> 的实例。</returns>
		protected SimpleTransaction CreateTransaction() { return SimpleSql.CreateTransaction(this.ConnectionString, this.SqlProvider); }

		#endregion

		#region 公有属性

		/// <summary>
		/// 获取或设置数据库连接字符串。
		/// </summary>
		protected string ConnectionString { get; set; }

		/// <summary>
		/// 获取或设置数据库查询提供程序。
		/// </summary>
		protected string SqlProvider { get; set; }

		#endregion
	}
}