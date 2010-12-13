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

namespace Net.Htmlbird.Framework.Web.Map
{
	/// <summary>
	/// 表示地图区域标识对象的类型的枚举值。
	/// </summary>
	[Serializable]
	public enum MapRegionType
	{
		/// <summary>
		/// 未知类型。
		/// </summary>
		[EnumDescription("未指定")]
		None = 0,

		/// <summary>
		/// 表示城市或县区。
		/// </summary>
		[EnumDescription("城市")]
		City = 1,

		/// <summary>
		/// 表示城市内的行政区。
		/// </summary>
		[EnumDescription("行政区")]
		District = 2,

		/// <summary>
		/// 表示城市或行政区内的商业区。
		/// </summary>
		[EnumDescription("商业区")]
		Commercial = 3,

		/// <summary>
		/// 表示城市、行政区或商业区内的地标。
		/// </summary>
		[EnumDescription("地标")]
		Landmark = 4,
	}
}