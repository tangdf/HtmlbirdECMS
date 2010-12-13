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

namespace Net.Htmlbird.Framework.Web.Entities
{
	[Serializable]
	public sealed class WebsiteDomainInfoCollection : List<WebsiteDomainInfo>
	{
		private WebsiteDomainInfo _current;

		public WebsiteDomainInfoCollection() { }

		public WebsiteDomainInfoCollection(int capacity) : base(capacity) { }

		public WebsiteDomainInfoCollection(IEnumerable<WebsiteDomainInfo> collection) : base(collection) { }

		public WebsiteDomainInfo Current
		{
			get { return this._current; }
			set
			{
				lock (this)
				{
					this.ForEach(item => item.IsDefault = item == value);

					this._current = value;
				}
			}
		}

		public WebsiteDomainInfo this[string domainName] { get { return this.FindLast(item => item.Name == domainName); } set { this[domainName] = value; } }

		public new void Add(WebsiteDomainInfo domain)
		{
			if (base.Contains(domain)) return;

			base.Add(domain);

			if (this._current == null && domain.IsDefault) this._current = domain;
		}
	}
}