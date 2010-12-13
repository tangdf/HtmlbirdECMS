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
	/// 处理对配置节 HtmlbirdPortal.Settings.DateTimeFormatOptions 的访问。
	/// </summary>
	public sealed class DateTimeFormatOptionsSection : ConfigurationSection
	{
		/// <summary>
		/// 获取或设置短日期值的格式模式，该模式与“d”格式模式关联。
		/// </summary>
		[ConfigurationProperty("shortDate", DefaultValue = "yyyy-MM-dd", IsRequired = true)]
		public string ShortDate { get { return this["shortDate"].ToString(); } set { this["shortDate"] = value; } }

		/// <summary>
		/// 获取或设置长日期值的格式模式，该模式与“D”格式模式关联。
		/// </summary>
		[ConfigurationProperty("longDate", DefaultValue = "yyyy-MM-dd HH:mm:ss", IsRequired = true)]
		public string LongDate { get { return this["longDate"].ToString(); } set { this["longDate"] = value; } }

		/// <summary>
		/// 获取或设置短时间值的格式模式，该模式与“t”格式模式关联。
		/// </summary>
		[ConfigurationProperty("shortTime", DefaultValue = "HH:mm", IsRequired = true)]
		public string ShortTime { get { return this["shortTime"].ToString(); } set { this["shortTime"] = value; } }

		/// <summary>
		/// 获取或设置长时间值的格式模式，该模式与“T”格式模式关联。
		/// </summary>
		[ConfigurationProperty("longTime", DefaultValue = "HH:mm:ss", IsRequired = true)]
		public string LongTime { get { return this["longTime"].ToString(); } set { this["longTime"] = value; } }
	}
}