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
using Net.Htmlbird.Framework.Modules;

namespace Net.Htmlbird.Framework.Web.Entities
{
	/// <summary>
	/// 表示一个友情链接项。
	/// </summary>
	[Serializable]
	public class FriendlinkInfo : EntityObject<int, int>
	{
		/// <summary>
		/// 初始化 <see cref="FriendlinkInfo"/> 类的新实例。
		/// </summary>
		public FriendlinkInfo() : this(0, 0) { }

		/// <summary>
		/// 初始化 <see cref="FriendlinkInfo"/> 类的新实例。
		/// </summary>
		/// <param name="id">指定友情链接的唯一标识符。</param>
		/// <param name="displayId">实体对象的用于显示的标识符。</param>
		public FriendlinkInfo(int id, int displayId) : base(id, displayId) { }

		/// <summary>
		/// 获取或设置友情链接的链接地址。
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// 获取或设置友情链接的友好显示名称。
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		/// 获取或设置友情链接的简要说明。
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// 获取或设置友情链接的排序序号。
		/// </summary>
		public int SortNumber { get; set; }

		/// <summary>
		/// 获取或设置友情链接的 Logo 地址。
		/// </summary>
		public string LogoUrl { get; set; }

		/// <summary>
		/// 获取或设置友情链接的联系人的电子信箱。
		/// </summary>
		public string EMail { get; set; }

		/// <summary>
		/// 获取或设置友情链接的显示位置。
		/// </summary>
		public string Location { get; set; }

		/// <summary>
		/// 获取或设置友情链接的分类。
		/// </summary>
		public string Category { get; set; }

		/// <summary>
		/// 获取或设置友情链接的点击次数。
		/// </summary>
		public int Clicks { get; set; }

		/// <summary>
		/// 获取或设置友情链接的展示方式。
		/// </summary>
		public FriendlinkDisplayRule DisplayRule { get; set; }

		/// <summary>
		/// 获取或设置友情链接的激活日期。<see cref="FriendlinkDisplayRule.TimeLimit"/> 方式下有效。
		/// </summary>
		[DynamicJsonOptions(true, "yyyy-MM-dd HH:mm:ss")]
		public DateTime ActivationDate { get; set; }

		/// <summary>
		/// 获取或设置友情链接的过期日期。<see cref="FriendlinkDisplayRule.TimeLimit"/> 方式下有效。
		/// </summary>
		[DynamicJsonOptions(true, "yyyy-MM-dd HH:mm:ss")]
		public DateTime ExpirationDate { get; set; }

		/// <summary>
		/// 较当前对象和同一类型的另一对象。
		/// </summary>
		/// <param name="other">与此对象进行比较的对象。</param>
		/// <returns>一个值，指示要比较的对象的相对顺序。</returns>
		public override int CompareTo(EntityObject<int, int> other)
		{
			return this.Id - other.Id;
		}
	}
}