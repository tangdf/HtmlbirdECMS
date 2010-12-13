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
using System.Net;
using System.Web;
using Net.Htmlbird.Framework.Modules;

namespace Net.Htmlbird.Framework.Web.Entities
{
	/// <summary>
	/// 表示一个网站。
	/// </summary>
	[Serializable]
	public sealed class WebsiteInfo : EntityObject
	{
		/// <summary>
		/// 初始化 <see cref="WebsiteInfo"/> 类的新实例。
		/// </summary>
		public WebsiteInfo() : this(0, String.Empty, new List<WebsiteDomainInfo>(), new List<WebsiteTemplateInfo>()) { }

		/// <summary>
		/// 使用指定的 <see cref="Guid"/> 初始化 <see cref="WebsiteInfo"/> 类的新实例。
		/// </summary>
		/// <param name="id">网站唯一标识符。</param>
		/// <param name="displayId">网站用于显示的标识符。</param>
		/// <param name="domains">与网站绑定的域名的集合。</param>
		/// <param name="templates">与网站绑定的模板的集合。</param>
		public WebsiteInfo(int id, string displayId, List<WebsiteDomainInfo> domains, List<WebsiteTemplateInfo> templates) : base(id, displayId)
		{
			if (domains == null) throw new ArgumentNullException("domains");
			if (templates == null) throw new ArgumentNullException("templates");

			this.Id = id;
			this.DomainList = domains;
			this.TemplateList = templates;
		}

		/// <summary>
		/// 获取或设置网站名称。
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 获取或设置网站全名。
		/// </summary>
		public string FullName { get; set; }

		/// <summary>
		/// 获取或设置网站所有者。
		/// </summary>
		public string Owner { get; set; }

		/// <summary>
		/// 获取或设置网站创建日期。
		/// </summary>
		[DynamicJsonOptions(true, "yyyy-MM-dd HH:mm:ss")]
		public DateTime BuildDate { get; set; }

		/// <summary>
		/// 获取或设置网站管理员。
		/// </summary>
		public string WebMaster { get; set; }

		/// <summary>
		/// 获取或设置网站管理员的电子信箱。
		/// </summary>
		public string WebMasterEMail { get; set; }

		/// <summary>
		/// 获取或设置网站版权声明文本。
		/// </summary>
		public string Copyright { get; set; }

		/// <summary>
		/// 获取或设置网站运行状态。
		/// </summary>
		public WebsiteState State { get; set; }

		//public string Keywords { get; set; }

		/// <summary>
		/// 获取或设置网站域名列表。
		/// </summary>
		internal List<WebsiteDomainInfo> DomainList { get; private set; }

		/// <summary>
		/// 获取或设置网站所有域名的集合。
		/// </summary>
		public WebsiteDomainInfoCollection Domains { get { return new WebsiteDomainInfoCollection(this.DomainList); } set { this.DomainList = value; } }

		/// <summary>
		/// 获取或设置网站所有模板的集合。
		/// </summary>
		internal List<WebsiteTemplateInfo> TemplateList { get; private set; }

		/// <summary>
		/// 获取或设置网站所有模板的集合。
		/// </summary>
		public WebsiteTemplateInfoCollection Templates { get { return new WebsiteTemplateInfoCollection(this.TemplateList); } set { this.TemplateList = value; } }

		/// <summary>
		/// 较当前对象和同一类型的另一对象。
		/// </summary>
		/// <param name="other">与此对象进行比较的对象。</param>
		/// <returns>一个值，指示要比较的对象的相对顺序。</returns>
		public override int CompareTo(EntityObject<int, string> other) { return this.Id - other.Id; }

		/// <summary>
		/// 返回当前网站正在使用的域名。
		/// </summary>
		/// <returns><see cref="System.String"/>。</returns>
		public string GetDomainName()
		{
			var domain = HttpContext.Current.Request.Url.Host;

			if (String.IsNullOrEmpty(domain)) throw new HtmlbirdECMSException(HttpStatusCode.InternalServerError, "获取主机名时遇到人品问题，请刷新页面重试。");

			if (this.Domains.Exists(item => item.Name == domain && item.Enabled)) return domain;

			throw new HtmlbirdECMSException(HttpStatusCode.InternalServerError, "获取主机名时发现当前访问的主机名不在网站域名列表中，请联系网站管理员。");
		}
	}
}