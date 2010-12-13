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
using System.Diagnostics;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using Net.Htmlbird.Framework.Web.Entities;

namespace Net.Htmlbird.Framework.Web.Data.MongoDB
{
	/// <summary>
	/// 表示 <see cref="FriendlinkDAL"/> 对象的数据访问层。
	/// </summary>
	public sealed class FriendlinkDAL : MongoDBDALBase<FriendlinkInfo, int, int>
	{
		/// <summary>
		/// 初始化 <see cref="FriendlinkDAL"/> 类的新实例。
		/// </summary>
		public FriendlinkDAL() : base("Friendlinks") { }

		/// <summary>
		/// 获取默认数据库名称。
		/// </summary>
		protected override string DefaultDatabaseName { get { return "HtmlbirdECMS"; } }

		/// <summary>
		/// 获取默认数据集名称。
		/// </summary>
		protected override string DefaultCollectionName { get { return "hbecms_Friendlinks"; } }

		/// <summary>
		/// 返回一个可用的对象唯一标识符。
		/// </summary>
		/// <returns><see cref="int"/>，一个可用的对象唯一标识符。</returns>
		public override int GetNewId()
		{
			int newId;

			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<FriendlinkInfo>(this.DefaultCollectionName);
				var friendlink = document.FindAll().SetSortOrder(SortBy.Descending("Id")).SetFields("Id").FirstOrDefault();

				newId = friendlink == null ? -1 : friendlink.Id;
			}

			return newId <= 0 ? 1 : newId + 1;
		}

		/// <summary>
		/// 返回一个可用的对象用于显示的标识符。
		/// </summary>
		/// <returns><see cref="string"/>，一个可用的对象用于显示的标识符。</returns>
		public override int GetNewDisplayId()
		{
			int newDisplayId;

			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<FriendlinkInfo>(this.DefaultCollectionName);
				var query = Query.GTE("CreateDate", DateTime.Today).LT(DateTime.Today.AddDays(1));
				var friendlink = document.Find(query).SetSortOrder(SortBy.Descending("DisplayId")).SetFields("Id", "DisplayId", "CreateDate").FirstOrDefault();

				newDisplayId = friendlink == null ? -1 : friendlink.DisplayId;
			}

			return newDisplayId <= 0 ? 1 : newDisplayId + 1;
		}

		/// <summary>
		/// 返回具有指定唯一标志符的 <see cref="FriendlinkInfo"/> 对象。
		/// </summary>
		/// <param name="id">要返回的 <see cref="FriendlinkInfo"/> 对象的唯一标志符。</param>
		/// <returns><see cref="FriendlinkInfo"/> 对象。</returns>
		public override FriendlinkInfo GetItemById(int id)
		{
			if (id == 0) throw new ArgumentOutOfRangeException("id");

			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<FriendlinkInfo>(this.DefaultCollectionName);

				return document.FindOne(Query.EQ("Id", id));
			}
		}

		/// <summary>
		/// 返回具有指定展示位置的 <see cref="FriendlinkInfo"/> 对象的集合。
		/// </summary>
		/// <param name="location">展示位置。</param>
		/// <param name="category">分类名称。</param>
		/// <param name="logoStatus">是否包含图标。</param>
		/// <returns><see cref="HashSet{FriendlinkInfo}"/> 对象。</returns>
		public HashSet<FriendlinkInfo> GetItems(string location = null, string category = null, FriendlinkLogoStatus logoStatus = FriendlinkLogoStatus.Including)
		{
			if (location == null) throw new ArgumentNullException("location");
			if (String.IsNullOrEmpty(location)) throw new ArgumentOutOfRangeException("location");

			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<FriendlinkInfo>(this.DefaultCollectionName);
				/*
					{
						$or: [
						{
							"Location": "page_Copyright",
							"DisplayRule": 0
						},
						{
							"Location": "page_Copyright",
							"DisplayRule": 1,
							"ActivationDate": { $lte: new Date("2010-12-18") },
							"ExpirationDate": { $gt: new Date("2010-12-18") }
						}]
					}
				 */
				var queryCategory = String.IsNullOrEmpty(category) ? null : Query.EQ("Category", category);
				var queryLogoStatus = logoStatus == FriendlinkLogoStatus.Including ? Query.NE("LogoUrl", String.Empty) : logoStatus == FriendlinkLogoStatus.None ? Query.EQ("LogoUrl", String.Empty) : null;
				var query1 = Query.And(Query.EQ("Location", location), queryCategory, queryLogoStatus, Query.EQ("DisplayRule", 0));
				var query2 = Query.And(Query.EQ("Location", location), queryCategory, queryLogoStatus, Query.EQ("DisplayRule", 1), Query.LTE("ActivationDate", DateTime.Now), Query.GT("ExpirationDate", DateTime.Now));
				var query = Query.Or(query1, query2);
				var resultList = document.Find(query).SetSortOrder(SortBy.Ascending("SortNumber"));

				return new HashSet<FriendlinkInfo>(resultList);
			}
		}

		/// <summary>
		/// 向数据库添加一个 <see cref="FriendlinkInfo"/> 对象。
		/// </summary>
		/// <param name="item">要添加的 <see cref="FriendlinkInfo"/> 对象。</param>
		public override void AddNew(FriendlinkInfo item)
		{
			if (item == null) throw new ArgumentNullException("item");
			if (item.Id == 0) throw new ArgumentOutOfRangeException("item");

			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<FriendlinkInfo>(this.DefaultCollectionName);

				document.Insert(item);
			}
		}

		/// <summary>
		/// 向数据库添加一个 <see cref="FriendlinkInfo"/> 对象的集合。
		/// </summary>
		/// <param name="items">要添加的 <see cref="FriendlinkInfo"/> 对象的集合。</param>
		public override void AddNew(IEnumerable<FriendlinkInfo> items)
		{
			if (items == null) throw new ArgumentNullException("items");
			if (items.Any(item => item.Id == 0)) throw new ArgumentOutOfRangeException("items", "序列中有一项或多项包含非法标识符。");

			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<FriendlinkInfo>(this.DefaultCollectionName);

				document.InsertBatch(items);
			}
		}

		/// <summary>
		/// 从数据库删除具有指定唯一标志符的 <see cref="FriendlinkInfo"/> 对象。
		/// </summary>
		/// <param name="id">要删除的 <see cref="FriendlinkInfo"/> 对象的唯一标志符。</param>
		public override void Remove(int id)
		{
			if (id == 0) throw new ArgumentOutOfRangeException("id");

			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<FriendlinkInfo>(this.DefaultCollectionName);

				document.Remove(Query.EQ("Id", id));
			}
		}

		/// <summary>
		/// 从数据库删除指定的 <see cref="FriendlinkInfo"/> 对象。
		/// </summary>
		/// <param name="item">要删除的 <see cref="FriendlinkInfo"/> 对象。</param>
		public override void Remove(FriendlinkInfo item)
		{
			if (item == null) throw new ArgumentNullException("item");
			if (item.Id == 0) throw new ArgumentOutOfRangeException("item");

			this.Remove(item.Id);
		}

		/// <summary>
		/// 从数据库删除指定的 <see cref="FriendlinkInfo"/> 对象的集合。
		/// </summary>
		/// <param name="items">要删除的 <see cref="FriendlinkInfo"/> 对象的集合。</param>
		public override void Remove(IEnumerable<FriendlinkInfo> items)
		{
			if (items == null) throw new ArgumentNullException("items");
			if (items.Any(item => item.Id == 0)) throw new ArgumentOutOfRangeException("items", "序列中有一项或多项包含非法标识符。");

			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<FriendlinkInfo>(this.DefaultCollectionName);

				document.Remove(Query.In("Id", String.Join(", ", items.Select(item => item.Id))));
			}
		}

		/// <summary>
		/// 更新数据库中的一个 <see cref="FriendlinkInfo"/> 对象。
		/// </summary>
		/// <param name="item">要更新的 <see cref="FriendlinkInfo"/> 对象。</param>
		public override void Update(FriendlinkInfo item)
		{
			if (item == null) throw new ArgumentNullException("item");
			if (item.Id == 0) throw new ArgumentOutOfRangeException("item");

			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<FriendlinkInfo>(this.DefaultCollectionName);

				document.Update(Query.EQ("Id", item.Id), global::MongoDB.Driver.Builders.Update.Wrap(item));
			}
		}

		/// <summary>
		/// 重建所有索引。
		/// </summary>
		public override void RebuildIndex()
		{
			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<FriendlinkInfo>(this.DefaultCollectionName);

				document.EnsureIndex(IndexKeys.Ascending("Id"));
				document.EnsureIndex(IndexKeys.Descending("DisplayId"));
				document.EnsureIndex(IndexKeys.Ascending("Url"));
				document.EnsureIndex(IndexKeys.Ascending("ActivationDate"));
				document.EnsureIndex(IndexKeys.Ascending("ExpirationDate"));
				document.EnsureIndex(IndexKeys.Ascending("CreateDate"));
				document.EnsureIndex(IndexKeys.Ascending("SortNumber"));
			}
		}
	}
}