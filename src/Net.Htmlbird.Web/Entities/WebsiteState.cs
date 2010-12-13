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
	/// 表示网站维护状态的枚举值。
	/// </summary>
	[Serializable]
	public enum WebsiteState : byte
	{
		/// <summary>
		/// 未知状态。
		/// </summary>
		[EnumDescription("未知状态")]
		Unknow = 0,

		/// <summary>
		/// 已经停止。
		/// </summary>
		[EnumDescription("已经停止")]
		Stopped = 1,

		/// <summary>
		/// 正在运行。
		/// </summary>
		[EnumDescription("正在运行")]
		Running = 2,
	}
}