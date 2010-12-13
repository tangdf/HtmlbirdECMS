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
	/// 表示文字线条样式的枚举值。
	/// </summary>
	[Flags]
	[Serializable]
	public enum TextDecoration
	{
		/// <summary>
		/// 无。
		/// </summary>
		[EnumDescription("无")]
		None,

		/// <summary>
		/// 下划线。
		/// </summary>
		[EnumDescription("下划线")]
		Underline,

		/// <summary>
		/// 上划线。
		/// </summary>
		[EnumDescription("上划线")]
		Overline,

		/// <summary>
		/// 穿越线。
		/// </summary>
		[EnumDescription("穿越线")]
		LineThrough,

		/// <summary>
		/// 闪烁。
		/// </summary>
		[EnumDescription("闪烁")]
		Blink,
	}
}