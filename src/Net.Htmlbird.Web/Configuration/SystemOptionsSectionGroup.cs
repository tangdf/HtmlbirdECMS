// ===============================================================================
//  产品名称：网鸟电子商务管理系统(Htmlbird ECMS)
//  产品作者：YMind Chan
//  版权所有：网鸟IT技术论坛 颜铭工作室
// ===============================================================================
//  Copyright © Htmlbird.Net. All rights reserved .
//  官方网站：http://www.htmlbird.net/
//  技术论坛：http://bbs.htmlbird.net/
// ===============================================================================
using System.Configuration;

namespace Net.Htmlbird.Framework.Web.Configuration
{
	/// <summary>
	/// 处理对配置节 HtmlbirdPortal.Settings 的访问。
	/// </summary>
	public class SystemOptionsSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>
		/// 获取网站默认的日期时间的格式。
		/// </summary>
		[ConfigurationProperty("DateTimeFormatOptions")]
		public DateTimeFormatOptionsSection DateTimeOptions { get { return this.Sections["DateTimeFormatOptions"] as DateTimeFormatOptionsSection; } }
	}
}