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
using System.Security;
using System.Web;
using Net.Htmlbird.Framework.Utilities;
using Net.Htmlbird.Framework.Web.Handlers;
using Net.Htmlbird.Framework.Web.Modules;

namespace Net.Htmlbird.Framework.Web
{
	/// <summary>
	/// 表示系统环境信息。
	/// </summary>
	public struct SystemInfo
	{
		#region 构造函数

		/// <summary>
		/// 初始化系统环境信息。
		/// </summary>
		/// <param name="version">指定当前网站应用程序的主版本号。</param>
		public SystemInfo(string version) : this() { this.Version = version; }

		#endregion

		#region 公有属性

		/// <summary>
		/// 获取或设置系统缓存单元的数量。
		/// </summary>
		public int CacheCount { get { return HtmlbirdECMS.Cache.Count; } }

		/// <summary>
		/// 获取当前请求的原始 URL。
		/// </summary>
		public Uri RawUrl
		{
			get
			{
				var uri = HttpContext.Current.Items["HtmlbirdPortal_RawUrl"] as Uri;

				if (uri == null)
				{
					var url = HttpContext.Current.Request.Url;

					uri = new Uri(String.Format("{0}://{1}:{2}{3}", url.Scheme, url.Host, url.Port, HttpContext.Current.Request.RawUrl));
				}

				return uri;
			}
		}

		/// <summary>
		/// 获取客户端浏览器的原始用户代理信息。
		/// </summary>
		public string UserAgent { get { return HttpContext.Current.Request.UserAgent; } }

		/// <summary>
		/// 获取远程客户端的 IP 主机地址。
		/// </summary>
		public string UserHostAddress { get { return HttpHandlerArgs.GetUserHostAddress(); } }

		/// <summary>
		/// 获取远程客户端的 DNS 名称。
		/// </summary>
		public string UserHostName { get { return HttpContext.Current.Request.UserHostName; } }

		/// <summary>
		/// 获取正在使用的网站应用程序的主版本号。
		/// </summary>
		public string Version { get; private set; }

		/// <summary>
		/// 获取表示系统使用的最小时间取值的字符串。
		/// </summary>
		public DateTime SqlMinimumDateTime { get { return new DateTime(1900, 1, 1, 0, 0, 0); } }

		/// <summary>
		/// 获取表示系统使用的最大时间取值的字符串。
		/// </summary>
		public DateTime SqlMaximumDateTime { get { return new DateTime(9999, 12, 31, 23, 59, 59); } }

		/// <summary>
		/// 获取表示网站模板安装位置的虚拟路径的字符串。
		/// </summary>
		public string TemplatesSetupPath { get { return "~/Resources/Templates/"; } }

		/// <summary>
		/// 获取表示网站模板安装位置的物理路径的字符串。
		/// </summary>
		public string PhysicalTemplatesSetupPath { get { return PathUtils.MapPath(this.TemplatesSetupPath); } }

		/// <summary>
		/// 获取表示网站模板安装位置的虚拟路径的字符串。
		/// </summary>
		public string AspxFilesSetupPath { get { return "~/Aspx/"; } }

		/// <summary>
		/// 获取表示网站模板安装位置的物理路径的字符串。
		/// </summary>
		public string PhysicalAspxFilesSetupPath { get { return PathUtils.MapPath(this.AspxFilesSetupPath); } }

		/// <summary>
		/// 获取表示网站模板安装位置的虚拟路径的字符串。
		/// </summary>
		public string HtmlFilesSetupPath { get { return "~/StaticFiles/Html/"; } }

		/// <summary>
		/// 获取表示网站模板安装位置的物理路径的字符串。
		/// </summary>
		public string PhysicalHtmlFilesSetupPath { get { return PathUtils.MapPath(this.HtmlFilesSetupPath); } }

		/// <summary>
		/// 获取表示 JScripts 资源安装位置的虚拟路径的字符串。
		/// </summary>
		public string JScriptFilesSetupPath { get { return "~/StaticFiles/JScripts/"; } }

		/// <summary>
		/// 获取表示 JScripts 资源安装位置的物理路径的字符串。
		/// </summary>
		public string PhysicalJScriptFilesSetupPath { get { return PathUtils.MapPath(this.JScriptFilesSetupPath); } }

		/// <summary>
		/// 获取表示 Styles 资源安装位置的虚拟路径的字符串。
		/// </summary>
		public string StyleFilesSetupPath { get { return "~/StaticFiles/Styles/"; } }

		/// <summary>
		/// 获取表示 Styles 资源安装位置的物理路径的字符串。
		/// </summary>
		public string PhysicalStyleFilesSetupPath { get { return PathUtils.MapPath(this.StyleFilesSetupPath); } }

		/// <summary>
		/// 获取表示当前正在使用的网站模板的安装位置的虚拟路径的字符串。
		/// </summary>
		public string CurrentTemplateSetupPath { get { return PathUtils.Combine(this.TemplatesSetupPath, Website.Current.Templates.Current.SetupPath); } }

		/// <summary>
		/// 获取表示当前正在使用的网站模板的安装位置的物理路径的字符串。
		/// </summary>
		public string PhysicalCurrentTemplateSetupPath { get { return PathUtils.MapPath(this.CurrentTemplateSetupPath); } }

		/// <summary>
		/// 获取表示当前正在使用的模板样式的安装位置的虚拟路径的字符串。
		/// </summary>
		public string CurrentStyleSetupPath { get { return Website.Current.Templates.Current.Styles.Current.SetupPath; } }

		/// <summary>
		/// 获取表示当前正在使用的模板样式的安装位置的物理路路径的字符串。
		/// </summary>
		public string PhysicalCurrentStyleSetupPath { get { return PathUtils.MapPath(this.CurrentStyleSetupPath); } }

		/// <summary>
		/// 获取表示当前正在使用的模板的语言包的安装位置的虚拟路径的字符串。
		/// </summary>
		public string CurrentLanguagesSetupPath { get { return PathUtils.Combine(this.CurrentTemplateSetupPath, "Languages/"); } }

		/// <summary>
		/// 获取表示当前正在使用的模板的语言包的安装位置的物理路路径的字符串。
		/// </summary>
		public string PhysicalCurrentLanguagesSetupPath { get { return PathUtils.MapPath(this.CurrentLanguagesSetupPath); } }

		/// <summary>
		/// 获取表示当前正在使用的网站模板的地址重写规则文件的位置的虚拟路径的字符串。
		/// </summary>
		public string CurrentRewriteRuleSetupPath { get { return PathUtils.Combine(this.CurrentTemplateSetupPath, Website.Current.Templates.Current.RewriteRules); } }

		/// <summary>
		/// 获取表示当前正在使用的网站模板的地址重写规则文件的位置的物理路径的字符串。
		/// </summary>
		public string PhysicalCurrentRewriteRuleSetupPath { get { return PathUtils.MapPath(this.CurrentRewriteRuleSetupPath); } }

		/// <summary>
		/// 获取包含承载在当前应用程序域中的应用程序的目录的虚拟路径。
		/// </summary>
		public string ApplicationSetupPath
		{
			get
			{
				var path = HttpRuntime.AppDomainAppVirtualPath;

				return path == null ? String.Empty : path.EndsWith("/") ? path : path + "/";
			}
		}

		/// <summary>
		/// 获取当前授予 ASP.NET Web 应用程序的信任级别。
		/// </summary>
		public AspNetHostingPermissionLevel CurrentTrustLevel
		{
			get
			{
				var list = HtmlbirdECMSEnumVictors.AspNetHostingPermissionLevelVictor.Descriptions.Keys;

				foreach (var trustLevel in list)
				{
					try
					{
						new AspNetHostingPermission(trustLevel).Demand();
					}
					catch (SecurityException)
					{
						continue;
					}

					return trustLevel;
				}

				return AspNetHostingPermissionLevel.None;
			}
		}

		#endregion
	}
}