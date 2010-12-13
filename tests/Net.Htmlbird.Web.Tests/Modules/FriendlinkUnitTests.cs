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
using System.Diagnostics;
using Net.Htmlbird.Framework;
using Net.Htmlbird.Framework.Utilities;
using Net.Htmlbird.Framework.Web.Configuration;
using Net.Htmlbird.Framework.Web.Entities;
using Net.Htmlbird.Framework.Web.Modules;
using NUnit.Framework;

namespace Net.Htmlbird.Web.Tests.Modules
{
	/// <summary>
	/// 友情链接系统单元测试。
	/// </summary>
	/// <remarks>
	/// 测试内容：
	///		添加/批量添加
	///		删除/批量删除
	///		更新/批量更新
	///		创建索引/重建索引
	///		
	///		按照指定位置以指定排序方式调用，考虑有效期
	/// </remarks>
	[TestFixture]
	public class FriendlinkUnitTests
	{
		/// <summary>
		/// 准备工作。这里是从指定目录加载数据库连接信息。
		/// </summary>
		[TestFixtureSetUp]
		public void Init() { DatabaseConfiguration.Reload(@"..\..\..\..\www.htmlbird.net\wwwroot\App_Data\Configuration\Database"); }

		/// <summary>
		/// 添加友情链接。
		/// </summary>
		[Test]
		public void InsertFriendlinkTest()
		{
			var friendlink = _CreateFriendlink();

			Friendlink.AddNew(friendlink);

			var item = Friendlink.GetItemById(1);

			Trace.WriteLine(friendlink.ToJsonString());

			Assert.IsNotNull(item);
			Assert.AreEqual(friendlink.Url, item.Url);
		}

		[Test]
		public void GetItemsTest()
		{
			var friendlinks = Friendlink.GetItems("page_Copyright");

			foreach (var friendlink in friendlinks) Trace.WriteLine(friendlink.ToJsonString());

			Assert.Pass("friendlinks", friendlinks.Count);
		}

		[Test]
		public void GetNewIdTest()
		{
			var id = Friendlink.GetNewId();

			Trace.WriteLine(id);

			Assert.Pass("NewId", id);
		}

		[Test]
		public void GetNewDisplayIdTest()
		{
			var displayId = Friendlink.GetNewDisplayId();

			Trace.WriteLine(displayId);

			Assert.Pass("DisplayId", displayId);
		}

		[Test]
		public void CreateIndexTest()
		{
			Friendlink.RebuildIndex();

			Assert.Pass("完成！");
		}

		private static FriendlinkInfo _CreateFriendlink()
		{
			var newIndex = Friendlink.GetNewId();

			return new FriendlinkInfo(newIndex, Friendlink.GetNewDisplayId())
			{
				ActivationDate = DateTime.Now,
				ExpirationDate = DateTime.Now.AddDays(30),
				CreateDate = DateTime.Now,
				CreateUserId = 1,
				UpdateDate = DateTime.Now,
				UpdateUserId = 1,
				Category = "网络技术",
				Clicks = 0,
				Description = "网鸟IT技术社区 颜铭工作室",
				DisplayName = String.Format("颜铭工作室({0})", newIndex),
				DisplayRule = FriendlinkDisplayRule.TimeLimit,
				EMail = "ymind@htmlbird.net",
				Location = "page_Copyright",
				SortNumber = 1,
				LogoUrl = "",
				Url = "http://www.ymind.net/"
			};
		}
	}
}