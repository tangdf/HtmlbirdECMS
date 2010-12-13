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

namespace Net.Htmlbird.Framework.Web.Entities
{
	/// <summary>
	/// 表示友情链接展示模式的枚举值。
	/// </summary>
	[Serializable]
	public enum FriendlinkDisplayRule
	{
		/// <summary>
		/// 常规模式。
		/// </summary>
		[EnumDescription("常规模式")]
		Normal = 0,

		/// <summary>
		/// 时间限制模式。
		/// </summary>
		[EnumDescription("时间限制模式")]
		TimeLimit = 1
	}
}