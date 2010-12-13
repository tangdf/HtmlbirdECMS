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
using System.Globalization;
using System.Threading;
using System.Web;
using Net.Htmlbird.Framework.Web.Configuration;

namespace Net.Htmlbird.Framework.Web
{
	/// <summary>
	/// 程序集 <see cref="Framework.Web"/> 的初始化模块。
	/// </summary>
	public class HttpModule : UrlRewriter.HttpModule
	{
		/// <summary>
		/// 初始化日期时间的格式。
		/// </summary>
		private static void _InitDateTimeFormat()
		{
			CultureInfo culture = Thread.CurrentThread.CurrentCulture.Clone() as CultureInfo;

			if (culture == null) return;

			culture.DateTimeFormat.ShortDatePattern = HtmlbirdECMS.SystemOptions.DateTimeOptions.ShortDate;
			culture.DateTimeFormat.LongDatePattern = HtmlbirdECMS.SystemOptions.DateTimeOptions.LongDate;
			culture.DateTimeFormat.ShortTimePattern = HtmlbirdECMS.SystemOptions.DateTimeOptions.ShortTime;
			culture.DateTimeFormat.LongTimePattern = HtmlbirdECMS.SystemOptions.DateTimeOptions.LongTime;

			Thread.CurrentThread.CurrentCulture = culture;
		}

		/// <summary>
		/// 在 ASP.NET 响应请求时作为 HTTP 执行管线链中的第一个事件发生。
		/// </summary>
		/// <param name="sender">事件源。</param>
		/// <param name="e">包含时间数据的 <see cref="EventArgs"/>。</param>
		protected override void BeginRequest(object sender, EventArgs e)
		{
			var context = ((HttpApplication)sender).Context;
			var url = context.Request.Url;

			HtmlbirdECMS.Cache.Prefix = url.Host;

			_InitDateTimeFormat();

			// 指派自定义地址重写配置信息提供程序，并执行地址重写操作
			base.SetConfigurationProvider(UrlRewriterConfiguration.Create());
			base.BeginRequest(sender, e);
		}
	}
}