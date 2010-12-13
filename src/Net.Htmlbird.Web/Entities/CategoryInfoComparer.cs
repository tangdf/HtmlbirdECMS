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
using System.Runtime.InteropServices;

namespace Net.Htmlbird.Framework.Web.Entities
{
	/// <summary>
	/// 表示一种 <see cref="CategoryInfo"/> 比较操作。
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	public class CategoryInfoComparer : IComparer<CategoryInfo>
	{
		#region IComparer<CategoryInfo> Members

		/// <summary>
		/// 比较两个对象并返回一个值，指示一个对象是小于、等于还是大于另一个对象。
		/// </summary>
		/// <param name="x">要比较的第一个对象。</param>
		/// <param name="y">要比较的第二个对象。</param>
		/// <returns>一个带符号整数，它指示 <paramref name="x"/> 与 <paramref name="y"/> 的相对值，如下表所示。</returns>
		public int Compare(CategoryInfo x, CategoryInfo y) { return x.SortNumber - y.SortNumber; }

		#endregion
	}
}