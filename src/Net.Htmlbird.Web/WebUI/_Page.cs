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
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using Net.Htmlbird.Framework.Utilities;
using Net.Htmlbird.Framework.Web.Modules;
using Net.Htmlbird.TemplateEngine;

namespace Net.Htmlbird.Framework.Web.WebUI
{
	/// <summary>
	/// 表示网鸟网站内容管理系统提供的从 ASP.NET Web 应用程序的宿主服务器请求的 .aspx 文件（又称为 Web 窗体页）。
	/// </summary>
	public class _Page : _Page<WebPageArgs> {}

	/// <summary>
	/// 表示网鸟网站内容管理系统提供的从 ASP.NET Web 应用程序的宿主服务器请求的 .aspx 文件（又称为 Web 窗体页）。
	/// </summary>
	/// <typeparam name="T">表示为 Web 窗体页提供用户参数的对象的类型。</typeparam>
	public class _Page<T> : Page, ISDEHandler, IMultiLanguage where T : WebPageArgs, new()
	{
		private static readonly object _asyncObject = new object();

		/// <summary>
		/// 获取与该页面关联的包含用户参数集合的对象。
		/// </summary>
		public virtual T Arguments { get; protected set; }

		/// <summary>
		/// 获取或设置动态文件的根目录的虚拟路径。
		/// </summary>
		public string DynamicFilePath { get; set; }

		/// <summary>
		/// 获取或设置静态文件的根目录的虚拟路径。
		/// </summary>
		public string StaticFilePath { get; set; }

		/// <summary>
		/// 获取当前页面对应的可执行页面的文件名，不含扩展名。
		/// </summary>
		public string PageFileName { get; protected set; }

		/// <summary>
		/// 获取当前页面对应的可执行页面的完整虚拟路径，包含路径、文件名和扩展名，但不包含查询字串。
		/// </summary>
		public string PageFullName { get; protected set; }

		/// <summary>
		/// 获取当前页面对应的可执行页面的扩展名，包含“.”符号。
		/// </summary>
		public string PageFileExtension { get; protected set; }

		/// <summary>
		/// 获取当前页面相对于网站根路径的相对路径。
		/// </summary>
		public string PageFilePath { get; protected set; }

		/// <summary>
		/// 获取或设置浏览器标题信息的实例。
		/// </summary>
		public PageTitleInfo PageTitle { get; set; }

		/// <summary>
		/// 获取或设置用户位置导航信息的实例。
		/// </summary>
		public PageNavigationInfo PageNavigation { get; set; }

		/// <summary>
		/// 获取一个值，该值指示当前页面是否需要身份验证。
		/// </summary>
		public virtual bool NeedAuthorise { get { return false; } }

		/// <summary>
		/// 获取一个值，该值指示当前页面是否需要显示页面导航。
		/// </summary>
		public virtual bool NeedPageNavigation { get { return true; } }

		/// <summary>
		/// 获取一个值，该值表示当前页面是否为网站首页。
		/// </summary>
		public virtual bool IsHomePage { get { return false; } }

		/// <summary>
		/// 获取用于当前会话的消息管理器的实例。
		/// </summary>
		public MessageBox Messages { get { return HtmlbirdECMS.Messages; } }

		/// <summary>
		/// 获取或设置“作者”meta 元素的内容。
		/// </summary>
		public string MetaAuthor { get; set; }

		/// <summary>
		/// 获取或设置“生成工具”meta 元素的内容。
		/// </summary>
		public string MetaGenerator { get; set; }

		/// <summary>
		/// 获取或设置“版权”meta 元素的内容。
		/// </summary>
		public string MetaCopyright { get; set; }

		/// <summary>
		/// 获取或设置“描述”meta 元素的内容。
		/// </summary>
		public new string MetaDescription { get; set; }

		/// <summary>
		/// 获取或设置“关键字”meta 元素的内容。
		/// </summary>
		public new string MetaKeywords { get; set; }

		/// <summary>
		/// 包含当前访问页面的来源页面的路径信息。
		/// </summary>
		public string UrlReferer
		{
			get
			{
				string url = this.Session["HtmlbirdPortal_UrlReferrer_Current"] as string;

				if (String.IsNullOrEmpty(url))
				{
					url = this.Request.ServerVariables["HTTP_REFERER"];

					if (String.IsNullOrEmpty(url)) url = "~/";
				}

				return url;
			}

			set
			{
				var url = value ?? String.Empty;

				this.Session["HtmlbirdPortal_UrlReferrer_Current"] = url;
			}
		}

		#region ISDEHandler 成员

		/// <summary>
		/// 获取或设置保存文件时使用的字符编码。
		/// </summary>
		public Encoding TextEncoding { get { return Encoding.UTF8; } }

		/// <summary>
		/// 获取或设置静态文件的有效期。
		/// </summary>
		public TimeSpan ExpirationTime { get; set; }

		/// <summary>
		/// 获取或设置静态文件的完整文件名（相对于应用程序根目录）。
		/// </summary>
		public string StaticFileName { get; set; }

		/// <summary>
		/// 获取或设置动态文件的完整文件名（相对于应用程序根目录）。
		/// </summary>
		public string DynamicFileName { get; set; }

		/// <summary>
		/// 执行动静平衡支持程序。
		/// </summary>
		/// <returns>如果静态文件不需要更新且已经成功切换到并运行了静态文件，则返回 true；否则返回 false。</returns>
		public bool ExecuteSDE()
		{
			lock (this.StaticFileName)
			{
				if (this.ExpirationTime == TimeSpan.Zero) return false;

				var filename = PathUtils.MapPath(this.StaticFileName);
				var file = new FileInfo(filename);

				if (file.Exists == false || DateTime.Now - file.LastWriteTime > this.ExpirationTime) return false;

				using (var reader = file.OpenText())
				{
					HttpContext.Current.Response.Write(reader.ReadToEnd());

					reader.Close();

					return true;
				}
			}
		}

		/// <summary>
		/// 更新静态文件。
		/// </summary>
		/// <param name="document">包含了已经生成的静态文件的文档代码的字符串。</param>
		public void UpdateStaticFile(StringBuilder document)
		{
			lock (_asyncObject)
			{
				var filename = PathUtils.CreateDirectories(this.Server.MapPath(this.StaticFileName));

				File.WriteAllText(filename, document.ToString(), this.TextEncoding);
			}
		}

		#endregion

		#region IMultiLanguage 成员

		/// <summary>
		/// 获取多语言引擎的实例。
		/// </summary>
		public MultiLanguageEngineBase Language
		{
			get
			{
				const string cacheName = "HtmlbirdPortal_MultiLanguageEngine";

				if (HtmlbirdECMS.Cache.Contains(cacheName)) return HtmlbirdECMS.Cache[cacheName] as MultiLanguageEngine;

				MultiLanguageEngine language = new MultiLanguageEngine(this.LanguagesPath, this.LanguageCode);

				HtmlbirdECMS.Cache.Add(cacheName, language, TimeSpan.FromDays(1));

				return language;
			}
		}

		/// <summary>
		/// 获取包含默认语言类型的代码的字符串。
		/// </summary>
		public string LanguageCode { get { return "zh-CHS"; } }

		/// <summary>
		/// 获取包含语言包路径的字符串。
		/// </summary>
		public string LanguagesPath { get { return HtmlbirdECMS.SystemInfo.PhysicalCurrentLanguagesSetupPath; } }

		/// <summary>
		/// 重置到默认语言类型。
		/// </summary>
		public void ResetLanguage() { this.Language.LanguageCode = this.LanguageCode; }

		#endregion

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			this.Arguments = new T();

			this.PageTitle = new PageTitleInfo {
				WebsiteTitle = Website.Current.Name,
				Tags = ""
			};
			this.PageNavigation = new PageNavigationInfo(this.PageTitle);
			this.PageFullName = String.IsNullOrEmpty(HttpContext.Current.Request.Url.Query) ? (this.PageFullName = HttpContext.Current.Request.Url.PathAndQuery) : HttpContext.Current.Request.Url.PathAndQuery.Substring(0, HttpContext.Current.Request.Url.PathAndQuery.IndexOf('?'));
			this.PageFileExtension = VirtualPathUtility.GetExtension(this.PageFullName);
			this.PageFileName = VirtualPathUtility.GetFileName(this.PageFullName);

			if (String.IsNullOrEmpty(this.PageFileName) == false && String.IsNullOrEmpty(this.PageFileExtension) == false) this.PageFileName = this.PageFileName.Substring(0, this.PageFileName.IndexOf(this.PageFileExtension));

			this.PageFilePath = VirtualPathUtility.GetDirectory(this.PageFullName);
			this.DynamicFilePath = this.PageFilePath;
			this.DynamicFileName = String.Format("{0}{1}{2}", this.PageFilePath, this.PageFileName, this.PageFileExtension);
			this.StaticFilePath = this.PageFilePath.Replace("/Aspx/", "/StaticFiles/Html/");
			this.StaticFileName = String.Format("{0}{1}.html", this.StaticFilePath, this.PageFileName);
			this.ExpirationTime = TimeSpan.FromMinutes(5);

			this.MetaAuthor = Website.Current.Name;
			this.MetaCopyright = Website.Current.FullName;
			this.MetaGenerator = "网鸟网站管理系统(HtmlbirdPortal)";

			//this.Title
			//this.MetaKeywords;
			//this.MetaDescription;
		}
	}
}