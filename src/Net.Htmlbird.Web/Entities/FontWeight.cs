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
	/// 表示文字粗细的枚举值。
	/// </summary>
	[Serializable]
	public enum FontWeight
	{
		/// <summary>
		/// 正常。
		/// </summary>
		[EnumDescription("正常")]
		Normal,

		/// <summary>
		/// 加粗。
		/// </summary>
		[EnumDescription("加粗")]
		Bold,

		/// <summary>
		/// 更粗。
		/// </summary>
		[EnumDescription("更粗")]
		Bolder,

		/// <summary>
		/// 较细。
		/// </summary>
		[EnumDescription("较细")]
		Lighter
	}
}