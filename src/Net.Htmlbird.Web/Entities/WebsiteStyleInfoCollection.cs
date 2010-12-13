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
	public sealed class WebsiteStyleInfoCollection : List<WebsiteStyleInfo>
	{
		private WebsiteStyleInfo _current;

		public WebsiteStyleInfoCollection() { }

		public WebsiteStyleInfoCollection(int capacity) : base(capacity) { }

		public WebsiteStyleInfoCollection(IEnumerable<WebsiteStyleInfo> collection) : base(collection) { }

		public WebsiteStyleInfo Current
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

		public new WebsiteStyleInfo this[int id] { get { return this[Convert.ToInt16(id)]; } set { this[Convert.ToByte(id)] = value; } }

		public WebsiteStyleInfo this[byte id] { get { return this.FindLast(item => item.DisplayId == id); } set { this[id] = value; } }

		public new void Add(WebsiteStyleInfo template)
		{
			if (base.Contains(template)) return;

			base.Add(template);

			if (this._current == null && template.IsDefault) this._current = template;
		}
	}
}