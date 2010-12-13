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
using System.Linq;
using MongoDB.Driver.Builders;
using Net.Htmlbird.Framework.Web.Entities;
using MongoDBDriver = MongoDB.Driver.Builders;

namespace Net.Htmlbird.Framework.Web.Data.MongoDB
{
	/// <summary>
	/// 表示 <see cref="WebsiteInfo"/> 对象的数据访问层。
	/// </summary>
	internal sealed class WebsiteDAL : MongoDBDALBase<WebsiteInfo, int, string>
	{
		/// <summary>
		/// 初始化 <see cref="WebsiteDAL"/> 类的新实例。
		/// </summary>
		public WebsiteDAL() : base("Websites") { }

		/// <summary>
		/// 获取默认数据库名称。
		/// </summary>
		protected override string DefaultDatabaseName { get { return "HtmlbirdECMS"; } }

		/// <summary>
		/// 获取默认数据集名称。
		/// </summary>
		protected override string DefaultCollectionName { get { return "hbecms_Websites"; } }

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
				var document = database.GetCollection<WebsiteInfo>(this.DefaultCollectionName);
				var website = document.FindAll().SetSortOrder(SortBy.Descending("Id")).FirstOrDefault();

				newId = website == null ? -1 : website.Id;
			}

			return newId <= 0 ? 1 : newId + 1;
		}

		/// <summary>
		/// 返回一个可用的对象用于显示的标识符。
		/// </summary>
		/// <returns><see cref="string"/>，一个可用的对象用于显示的标识符。</returns>
		public override string GetNewDisplayId()
		{
			int newDisplayId;

			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<WebsiteInfo>(this.DefaultCollectionName);
				var website = document.Find(Query.And(Query.GTE("CreateDate", DateTime.Today.ToShortDateString()), Query.GTE("CreateDate", DateTime.Today.AddDays(1).ToShortDateString()))).SetSortOrder(SortBy.Descending("Id")).FirstOrDefault();

				newDisplayId = website == null ? -1 : website.Id;
			}

			return (newDisplayId <= 0 ? 1 : newDisplayId + 1).ToString("D6");
		}

		/// <summary>
		/// 返回具有指定唯一标志符的 <see cref="WebsiteInfo"/> 对象。
		/// </summary>
		/// <param name="id">要返回的 <see cref="WebsiteInfo"/> 对象的唯一标志符。</param>
		/// <returns><see cref="WebsiteInfo"/> 对象。</returns>
		public override WebsiteInfo GetItemById(int id)
		{
			if (id == 0) throw new ArgumentOutOfRangeException("id");

			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<WebsiteInfo>(this.DefaultCollectionName);

				return document.FindOne(Query.EQ("Id", id));
			}
		}

		/// <summary>
		/// 向数据库添加一个 <see cref="WebsiteInfo"/> 对象。
		/// </summary>
		/// <param name="item">要添加的 <see cref="WebsiteInfo"/> 对象。</param>
		public override void AddNew(WebsiteInfo item)
		{
			if (item == null) throw new ArgumentNullException("item");
			if (item.Id == 0) throw new ArgumentOutOfRangeException("item");

			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<WebsiteInfo>(this.DefaultCollectionName);

				document.Insert(item);
			}
		}

		/// <summary>
		/// 向数据库添加一个 <see cref="WebsiteInfo"/> 对象的集合。
		/// </summary>
		/// <param name="items">要添加的 <see cref="WebsiteInfo"/> 对象的集合。</param>
		public override void AddNew(IEnumerable<WebsiteInfo> items)
		{
			if (items == null) throw new ArgumentNullException("items");
			if (items.Any(item => item.Id == 0)) throw new ArgumentOutOfRangeException("items", "序列中有一项或多项包含非法标识符。");

			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<WebsiteInfo>(this.DefaultCollectionName);

				document.InsertBatch(items);
			}
		}

		/// <summary>
		/// 从数据库删除具有指定唯一标志符的 <see cref="WebsiteInfo"/> 对象。
		/// </summary>
		/// <param name="id">要删除的 <see cref="WebsiteInfo"/> 对象的唯一标志符。</param>
		public override void Remove(int id)
		{
			if (id == 0) throw new ArgumentOutOfRangeException("id");

			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<WebsiteInfo>(this.DefaultCollectionName);

				document.Remove(Query.EQ("Id", id));
			}
		}

		/// <summary>
		/// 从数据库删除指定的 <see cref="WebsiteInfo"/> 对象。
		/// </summary>
		/// <param name="item">要删除的 <see cref="WebsiteInfo"/> 对象。</param>
		public override void Remove(WebsiteInfo item)
		{
			if (item == null) throw new ArgumentNullException("item");
			if (item.Id == 0) throw new ArgumentOutOfRangeException("item");

			this.Remove(item.Id);
		}

		/// <summary>
		/// 从数据库删除指定的 <see cref="WebsiteInfo"/> 对象的集合。
		/// </summary>
		/// <param name="items">要删除的 <see cref="WebsiteInfo"/> 对象的集合。</param>
		public override void Remove(IEnumerable<WebsiteInfo> items)
		{
			if (items == null) throw new ArgumentNullException("items");
			if (items.Any(item => item.Id == 0)) throw new ArgumentOutOfRangeException("items", "序列中有一项或多项包含非法标识符。");

			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<WebsiteInfo>(this.DefaultCollectionName);

				document.Remove(Query.In("Id", String.Join(", ", items.Select(item => item.Id))));
			}
		}

		/// <summary>
		/// 更新数据库中的一个 <see cref="WebsiteInfo"/> 对象。
		/// </summary>
		/// <param name="item">要更新的 <see cref="WebsiteInfo"/> 对象。</param>
		public override void Update(WebsiteInfo item)
		{
			if (item == null) throw new ArgumentNullException("item");
			if (item.Id == 0) throw new ArgumentOutOfRangeException("item");

			using (var mongo = this.GetMongoServer())
			{
				mongo.Connect();

				var database = mongo.GetDatabase(this.DefaultDatabaseName);
				var document = database.GetCollection<WebsiteInfo>(this.DefaultCollectionName);

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
				var document = database.GetCollection<WebsiteInfo>(this.DefaultCollectionName);

				document.EnsureIndex(IndexKeys.Ascending("Id", "DisplayId", "Name", "DomainList", "TemplateList"));
			}
		}
	}
}