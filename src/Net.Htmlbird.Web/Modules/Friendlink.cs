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
	/// 封装 <see cref="FriendlinkInfo"/> 类的常用操作。
	/// </summary>
	public static class Friendlink
	{
		private static readonly FriendlinkDAL _dal = new FriendlinkDAL();
		private static List<FriendlinkInfo> _friendlinks;

		/// <summary>
		/// 获取所有 <see cref="FriendlinkInfo"/> 对象的集合。
		/// </summary>
		public static List<FriendlinkInfo> AllFriendlinks { get { return _friendlinks == null || _friendlinks.Count == 0 ? _friendlinks = _dal.GetAllItems().ToList() : _friendlinks; } }

		/// <summary>
		/// 返回一个可用的对象唯一标识符。
		/// </summary>
		/// <returns></returns>
		public static int GetNewId() { return _dal.GetNewId(); }

		/// <summary>
		/// 返回一个可用的对象用于显示的标识符。
		/// </summary>
		/// <returns><see cref="string"/>，一个可用的对象用于显示的标识符。</returns>
		public static int GetNewDisplayId() { return _dal.GetNewDisplayId(); }

		/// <summary>
		/// 返回具有指定唯一标志符的 <see cref="FriendlinkInfo"/> 对象。
		/// </summary>
		/// <param name="id">要返回的 <see cref="FriendlinkInfo"/> 对象的唯一标志符。</param>
		/// <returns><see cref="FriendlinkInfo"/> 对象。</returns>
		public static FriendlinkInfo GetItemById(int id)
		{
			if (id == 0) throw new ArgumentOutOfRangeException("id");

			return AllFriendlinks == null || AllFriendlinks.Count == 0 ? null : AllFriendlinks.FindLast(item => item.Id == id);
		}

		/// <summary>
		/// 返回具有指定展示位置的 <see cref="FriendlinkInfo"/> 对象的集合。
		/// </summary>
		/// <param name="location">展示位置。</param>
		/// <returns><see cref="HashSet{FriendlinkInfo}"/> 对象。</returns>
		public static HashSet<FriendlinkInfo> GetItems(string location = null) { return _dal.GetItems(location); }

		/// <summary>
		/// 向数据库添加一个 <see cref="FriendlinkInfo"/> 对象。
		/// </summary>
		/// <param name="item">要添加的 <see cref="FriendlinkInfo"/> 对象。</param>
		public static void AddNew(FriendlinkInfo item)
		{
			_dal.AddNew(item);

			ClearCache();
		}

		/// <summary>
		/// 向数据库添加一个 <see cref="FriendlinkInfo"/> 对象的集合。
		/// </summary>
		/// <param name="items">要添加的 <see cref="FriendlinkInfo"/> 对象的集合。</param>
		public static void AddNew(IEnumerable<FriendlinkInfo> items)
		{
			_dal.AddNew(items);

			ClearCache();
		}

		/// <summary>
		/// 从数据库删除所有 <see cref="FriendlinkInfo"/> 对象。
		/// </summary>
		public static void RemoveAll()
		{
			_dal.RemoveAll();

			ClearCache();
		}

		/// <summary>
		/// 从数据库删除具有指定唯一标志符的 <see cref="FriendlinkInfo"/> 对象。
		/// </summary>
		/// <param name="id">要删除的 <see cref="FriendlinkInfo"/> 对象的唯一标志符。</param>
		public static void Remove(int id)
		{
			_dal.Remove(id);

			ClearCache();
		}

		/// <summary>
		/// 从数据库删除指定的 <see cref="FriendlinkInfo"/> 对象。
		/// </summary>
		/// <param name="item">要删除的 <see cref="FriendlinkInfo"/> 对象。</param>
		public static void Remove(FriendlinkInfo item)
		{
			_dal.Remove(item);

			ClearCache();
		}

		/// <summary>
		/// 从数据库删除指定的 <see cref="FriendlinkInfo"/> 对象的集合。
		/// </summary>
		/// <param name="items">要删除的 <see cref="FriendlinkInfo"/> 对象的集合。</param>
		public static void Remove(IEnumerable<FriendlinkInfo> items)
		{
			_dal.Remove(items);

			ClearCache();
		}

		/// <summary>
		/// 更新数据库中的一个 <see cref="FriendlinkInfo"/> 对象。
		/// </summary>
		/// <param name="item">要更新的 <see cref="FriendlinkInfo"/> 对象。</param>
		public static void Update(FriendlinkInfo item)
		{
			_dal.Update(item);

			ClearCache();
		}

		/// <summary>
		/// 更新数据库中的多个 <see cref="FriendlinkInfo"/> 对象。
		/// </summary>
		/// <param name="items">要更新的 <see cref="FriendlinkInfo"/> 对象的集合。</param>
		public static void Update(IEnumerable<FriendlinkInfo> items) { _dal.Update(items); }

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
		/// 清理与 <see cref="FriendlinkInfo"/> 对象关联的所有缓存。
		/// </summary>
		public static void ClearCache()
		{
			if (_friendlinks == null || _friendlinks.Count == 0) return;

			_friendlinks.Clear();
		}
	}
}