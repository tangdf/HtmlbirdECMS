// ===============================================================================
//  产品名称：网鸟电子商务管理系统(Htmlbird ECMS)
//  产品作者：YMind Chan
//  版权所有：网鸟IT技术论坛 颜铭工作室
// ===============================================================================
//  Copyright © Htmlbird.Net. All rights reserved .
//  官方网站：http://www.htmlbird.net/
//  技术论坛：http://bbs.htmlbird.net/
// ===============================================================================
using System.Collections.Generic;
using System.Net;
using MongoDB.Driver;
using Net.Htmlbird.Framework.Web.Configuration;
using Net.Htmlbird.Framework.Web.Entities;
using MongoDBDriver = MongoDB.Driver.Builders;

namespace Net.Htmlbird.Framework.Web.Data
{
	/// <summary>
	/// 为实体对象基于 MongoDB 的数据访问层提供基类。
	/// </summary>
	/// <typeparam name="T">实体对象的类型。</typeparam>
	/// <typeparam name="TId">实体对象唯一标志符的类型。</typeparam>
	/// <typeparam name="TDisplayId">实体对象用于显示的标识符的类型。</typeparam>
	public abstract class MongoDBDALBase<T, TId, TDisplayId> where T : EntityObject<TId, TDisplayId>
	{
		/// <summary>
		/// 初始化 <see cref="MongoDBDALBase{T, TId, TDisplayId}"/> 对象。
		/// </summary>
		/// <param name="groupName">数据库分组名称。</param>
		protected MongoDBDALBase(string groupName) { this.GroupName = groupName; }

		/// <summary>
		/// 获取数据库分组名称。
		/// </summary>
		protected string GroupName { get; private set; }

		/// <summary>
		/// 获取默认数据库名称。
		/// </summary>
		protected abstract string DefaultDatabaseName { get; }

		/// <summary>
		/// 获取默认数据集名称。
		/// </summary>
		protected abstract string DefaultCollectionName { get; }

		/// <summary>
		/// 返回 <see cref="MongoServer"/> 的新实例，用于数据库查询。
		/// </summary>
		/// <returns>返回 <see cref="MongoServer"/> 的新实例。</returns>
		protected MongoServer GetMongoServer()
		{
			var conn = DatabaseConfiguration.GetConnectionStrings(DatabaseConfiguration.MongoDBConnectionStrings, this.GroupName);

			if (conn == null) throw new HtmlbirdECMSException(HttpStatusCode.InternalServerError, string.Format("数据库连接初始化失败。{0}", this.GroupName));

			return MongoServer.Create(conn.ConnectionStrings);
		}

		/// <summary>
		/// 返回一个可用的对象唯一标识符。
		/// </summary>
		/// <returns><see cref="TId"/>，一个可用的对象唯一标识符。</returns>
		public abstract TId GetNewId();

		/// <summary>
		/// 返回一个可用的对象用于显示的标识符。
		/// </summary>
		/// <returns><see cref="TId"/>，一个可用的对象用于显示的标识符。</returns>
		public abstract TDisplayId GetNewDisplayId();

		/// <summary>
		/// 返回所有 <see cref="T"/> 对象的集合。
		/// </summary>
		/// <returns>包含 <see cref="T"/> 对象的集合。</returns>
		public virtual IEnumerable<T> GetAllItems()
		{
			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<T>(this.DefaultCollectionName);

				return document.FindAll();
			}
		}

		/// <summary>
		/// 返回具有指定唯一标志符的 <see cref="T"/> 对象。
		/// </summary>
		/// <param name="id">要返回的 <see cref="T"/> 对象的唯一标志符。</param>
		/// <returns><see cref="T"/> 对象。</returns>
		public abstract T GetItemById(TId id);

		/// <summary>
		/// 向数据库添加一个 <see cref="T"/> 对象。
		/// </summary>
		/// <param name="item">要添加的 <see cref="T"/> 对象。</param>
		public abstract void AddNew(T item);

		/// <summary>
		/// 向数据库添加一个 <see cref="T"/> 对象的集合。
		/// </summary>
		/// <param name="items">要添加的 <see cref="T"/> 对象的集合。</param>
		public abstract void AddNew(IEnumerable<T> items);

		/// <summary>
		/// 从数据库删除所有 <see cref="T"/> 对象。
		/// </summary>
		public virtual void RemoveAll()
		{
			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<FriendlinkInfo>(this.DefaultCollectionName);

				document.RemoveAll();
			}
		}

		/// <summary>
		/// 从数据库删除具有指定唯一标志符的 <see cref="T"/> 对象。
		/// </summary>
		/// <param name="id">要删除的 <see cref="T"/> 对象的唯一标志符。</param>
		public abstract void Remove(TId id);

		/// <summary>
		/// 从数据库删除指定的 <see cref="T"/> 对象。
		/// </summary>
		/// <param name="item">要删除的 <see cref="T"/> 对象。</param>
		public abstract void Remove(T item);

		/// <summary>
		/// 从数据库删除指定的 <see cref="T"/> 对象的集合。
		/// </summary>
		/// <param name="items">要删除的 <see cref="T"/> 对象的集合。</param>
		public abstract void Remove(IEnumerable<T> items);

		/// <summary>
		/// 更新数据库中的一个 <see cref="T"/> 对象。
		/// </summary>
		/// <param name="item">要更新的 <see cref="T"/> 对象。</param>
		public abstract void Update(T item);

		/// <summary>
		/// 更新数据库中的多个 <see cref="T"/> 对象。
		/// </summary>
		/// <param name="items">要更新的 <see cref="T"/> 对象的集合。</param>
		public virtual void Update(IEnumerable<T> items) { foreach (var item in items) this.Update(item); }

		/// <summary>
		/// 重建所有索引。
		/// </summary>
		public abstract void RebuildIndex();

		/// <summary>
		/// 删除所有索引。
		/// </summary>
		public virtual void DeleteIndex()
		{
			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<FriendlinkInfo>(this.DefaultCollectionName);

				document.DropAllIndexes();
			}
		}
	}
}