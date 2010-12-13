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
using Net.Htmlbird.Framework.Web.Data.MongoDB;
using Net.Htmlbird.Framework.Web.Entities;

namespace Net.Htmlbird.Framework.Web.Modules
{
	/// <summary>
	/// 封装 <see cref="WebsiteInfo"/> 对象的常用操作。
	/// </summary>
	public static class Website
	{
		private static readonly WebsiteDAL _dal = new WebsiteDAL();
		private static List<WebsiteInfo> _websites;

		/// <summary>
		/// 获取与当前域名相关联的 <see cref="WebsiteInfo"/> 对象。
		/// </summary>
		public static WebsiteInfo Current { get { return AllWebsites.FirstOrDefault(item => item.DomainList.Exists(domain => domain.Name == HtmlbirdECMS.SystemInfo.RawUrl.Host.ToLower())) ?? AllWebsites[0]; } }

		/// <summary>
		/// 获取所有 <see cref="WebsiteInfo"/> 对象的集合。
		/// </summary>
		public static List<WebsiteInfo> AllWebsites { get { return _websites == null || _websites.Count == 0 ? _websites = _dal.GetAllItems().ToList() : _websites; } }

		/// <summary>
		/// 返回一个可用的对象唯一标识符。
		/// </summary>
		/// <returns></returns>
		public static int GetNewId() { return _dal.GetNewId(); }

		/// <summary>
		/// 返回一个可用的对象用于显示的标识符。
		/// </summary>
		/// <returns><see cref="string"/>，一个可用的对象用于显示的标识符。</returns>
		public static string GetNewDisplayId() { return _dal.GetNewDisplayId(); }

		/// <summary>
		/// 返回具有指定唯一标志符的 <see cref="WebsiteInfo"/> 对象。
		/// </summary>
		/// <param name="id">要返回的 <see cref="WebsiteInfo"/> 对象的唯一标志符。</param>
		/// <returns><see cref="WebsiteInfo"/> 对象。</returns>
		public static WebsiteInfo GetItemById(int id)
		{
			if (id == 0) throw new ArgumentOutOfRangeException("id");

			return AllWebsites == null || AllWebsites.Count == 0 ? null : AllWebsites.FindLast(item => item.Id == id);
		}

		/// <summary>
		/// 向数据库添加一个 <see cref="WebsiteInfo"/> 对象。
		/// </summary>
		/// <param name="item">要添加的 <see cref="WebsiteInfo"/> 对象。</param>
		public static void AddNew(WebsiteInfo item)
		{
			_dal.AddNew(item);

			ClearCache();
		}

		/// <summary>
		/// 向数据库添加一个 <see cref="WebsiteInfo"/> 对象的集合。
		/// </summary>
		/// <param name="items">要添加的 <see cref="WebsiteInfo"/> 对象的集合。</param>
		public static void AddNew(IEnumerable<WebsiteInfo> items)
		{
			_dal.AddNew(items);

			ClearCache();
		}

		/// <summary>
		/// 从数据库删除所有 <see cref="WebsiteInfo"/> 对象。
		/// </summary>
		public static void RemoveAll()
		{
			_dal.RemoveAll();

			ClearCache();
		}

		/// <summary>
		/// 从数据库删除具有指定唯一标志符的 <see cref="WebsiteInfo"/> 对象。
		/// </summary>
		/// <param name="id">要删除的 <see cref="WebsiteInfo"/> 对象的唯一标志符。</param>
		public static void Remove(int id)
		{
			_dal.Remove(id);

			ClearCache();
		}

		/// <summary>
		/// 从数据库删除指定的 <see cref="WebsiteInfo"/> 对象。
		/// </summary>
		/// <param name="item">要删除的 <see cref="WebsiteInfo"/> 对象。</param>
		public static void Remove(WebsiteInfo item)
		{
			_dal.Remove(item);

			ClearCache();
		}

		/// <summary>
		/// 从数据库删除指定的 <see cref="WebsiteInfo"/> 对象的集合。
		/// </summary>
		/// <param name="items">要删除的 <see cref="WebsiteInfo"/> 对象的集合。</param>
		public static void Remove(IEnumerable<WebsiteInfo> items)
		{
			_dal.Remove(items);

			ClearCache();
		}

		/// <summary>
		/// 更新数据库中的一个 <see cref="WebsiteInfo"/> 对象。
		/// </summary>
		/// <param name="item">要更新的 <see cref="WebsiteInfo"/> 对象。</param>
		public static void Update(WebsiteInfo item)
		{
			_dal.Update(item);

			ClearCache();
		}

		/// <summary>
		/// 更新数据库中的多个 <see cref="WebsiteInfo"/> 对象。
		/// </summary>
		/// <param name="items">要更新的 <see cref="WebsiteInfo"/> 对象的集合。</param>
		public static void Update(IEnumerable<WebsiteInfo> items) { _dal.Update(items); }

		/// <summary>
		/// 重建所有索引。
		/// </summary>
		public static void RebuildIndex()
		{
			_dal.RebuildIndex();

			ClearCache();
		}

		/// <summary>
		/// 删除所有索引。
		/// </summary>
		public static void DeleteIndex()
		{
			_dal.DeleteIndex();

			ClearCache();
		}

		/// <summary>
		/// 清理与 <see cref="WebsiteInfo"/> 对象关联的所有缓存。
		/// </summary>
		public static void ClearCache()
		{
			if (_websites == null || _websites.Count == 0) return;

			_websites.Clear();
		}
	}
}