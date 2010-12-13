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
using System.Web;
using System.Web.Configuration;
using Net.Htmlbird.Framework.Web.Configuration;
using Net.Htmlbird.Framework.Web.Modules;

namespace Net.Htmlbird.Framework.Web
{
	/// <summary>
	/// 封装程序集 <see cref="Framework.Web"/> 的常用操作。无法继承此类。
	/// </summary>
	public static class HtmlbirdECMS
	{
		/// <summary>
		/// 获取用于 HtmlbirdPortal 的缓存管理器的实例。
		/// </summary>
		public static CacheManager Cache = new CacheManager();

		/// <summary>
		/// 获取当前的系统环境信息。
		/// </summary>
		public static SystemInfo SystemInfo { get { return new SystemInfo(ConfigurationManager.AppSettings["Version"] ?? "1.0.0.0"); } }

		/// <summary>
		/// 获取当前网站的全局配置信息。
		/// </summary>
		public static SystemOptionsSectionGroup SystemOptions { get { return WebConfigurationManager.OpenWebConfiguration("~/web.config").GetSectionGroup("HtmlbirdPortal.Settings") as SystemOptionsSectionGroup; } }

		/// <summary>
		/// 获取用于当前会话的系统消息管理器的实例。
		/// </summary>
		public static MessageBox Messages
		{
			get
			{
				var msg = HttpContext.Current.Session["HtmlbirdPortal_MessageBox"] as MessageBox;

				if (msg == null)
				{
					msg = new MessageBox();

					HttpContext.Current.Session["HtmlbirdPortal_MessageBox"] = msg;
				}

				return msg;
			}
		}
	}
}