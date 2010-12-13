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
	/// 表示内容标题。
	/// </summary>
	[Serializable]
	public class ContentTitle
	{
		/// <summary>
		/// 获取或设置标题文本。
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// 获取或设置标题前景色。
		/// </summary>
		public string ForeColor { get; set; }

		/// <summary>
		/// 获取或设置标题背景色。
		/// </summary>
		public string BackColor { get; set; }

		/// <summary>
		/// 获取或设置标题粗细。
		/// </summary>
		public FontWeight Weight { get; set; }

		/// <summary>
		/// 获取或设置标题样式。
		/// </summary>
		public FontStyle Style { get; set; }

		/// <summary>
		/// 获取或设置标题的文本修饰。
		/// </summary>
		public TextDecoration Decoration { get; set; }

		/// <summary>
		/// 获取或设置用于标题显示的自定义 CSS 样式。
		/// </summary>
		public string CustomCSS { get; set; }
	}
}