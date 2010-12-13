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
	public sealed class WebsiteTemplateInfoCollection : List<WebsiteTemplateInfo>
	{
		private WebsiteTemplateInfo _current;

		public WebsiteTemplateInfoCollection() { }

		public WebsiteTemplateInfoCollection(int capacity) : base(capacity) { }

		public WebsiteTemplateInfoCollection(IEnumerable<WebsiteTemplateInfo> collection) : base(collection) { }

		public WebsiteTemplateInfo Current
		{
			get { return this._current ?? (this._current = this.Find(item => item.IsDefault)); }
			set
			{
				lock (this)
				{
					this.ForEach(item => item.IsDefault = item == value);

					this._current = value;
				}
			}
		}

		public new WebsiteTemplateInfo this[int id] { get { return this.FindLast(item => item.DisplayId == id); } set { this[id] = value; } }

		public new void Add(WebsiteTemplateInfo template)
		{
			if (base.Contains(template)) return;

			base.Add(template);

			if (this._current == null && template.IsDefault) this._current = template;
		}
	}
}