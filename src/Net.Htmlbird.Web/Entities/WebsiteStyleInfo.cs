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
using Net.Htmlbird.Framework.Modules;

namespace Net.Htmlbird.Framework.Web.Entities
{
	[Serializable]
	public sealed class WebsiteStyleInfo : EntityObject<int, int>
	{
		public WebsiteStyleInfo() : this(0, 0, 0) { }
		public WebsiteStyleInfo(int id, int displayId, int templateId) : base(id, displayId) { this.TemplateId = templateId; }

		public int TemplateId { get; private set; }

		public bool IsDefault { get; set; }
		public bool Enabled { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }
		public string Alias { get; set; }
		public string SetupPath { get; set; }

		[DynamicJsonOptions(true, "yyyy-MM-dd HH:mm:ss")]
		public DateTime BuildDate { get; set; }

		public string Author { get; set; }
		public string EMail { get; set; }
		public string HomePage { get; set; }

		/// <summary>
		/// 较当前对象和同一类型的另一对象。
		/// </summary>
		/// <param name="other">与此对象进行比较的对象。</param>
		/// <returns>一个值，指示要比较的对象的相对顺序。</returns>
		public override int CompareTo(EntityObject<int, int> other) { return this.Id - other.Id; }
	}
}