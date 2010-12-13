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

namespace Net.Htmlbird.Framework.Web.Entities
{
	/// <summary>
	/// 表示一个网站域名。
	/// </summary>
	[Serializable]
	public sealed class WebsiteDomainInfo : EntityObject
	{
		/*
			CREATE TABLE [dbo].[hbp_Websites_Domains](
				[WebsiteId] [uniqueidentifier] NOT NULL,
				[domain_Name] [nvarchar](50) NOT NULL,
				[domain_PortNumber] [int] NOT NULL,
				[domain_ICPNumber] [nvarchar](30) NOT NULL,
				[domain_Description] [nvarchar](256) NOT NULL,
				[domain_Default] [bit] NOT NULL,
				[domain_Enabled] [bit] NOT NULL,
				[domain_CreateDate] [datetime] NOT NULL,
				[domain_CreateUserId] [uniqueidentifier] NOT NULL,
				[domain_UpdateDate] [datetime] NULL,
				[domain_UpdateUserId] [uniqueidentifier] NULL
			)
		*/

		/// <summary>
		/// 初始化 <see cref="WebsiteDomainInfo"/> 类的新实例。
		/// </summary>
		public WebsiteDomainInfo() : this(0, String.Empty, 0, String.Empty) { }

		/// <summary>
		/// 初始化 <see cref="WebsiteDomainInfo"/> 类的新实例。
		/// </summary>
		/// <param name="id">域名唯一标识符。</param>
		/// <param name="displayId">域名用于显示的标识符。</param>
		/// <param name="websiteId">域名隶属的网站的编号。</param>
		/// <param name="name">域名的主机名。</param>
		/// <param name="portNumber">访问此域名事使用的端口号。</param>
		public WebsiteDomainInfo(int id, string displayId, int websiteId, string name, int portNumber = 80) : base(id, displayId)
		{
			this.WebsiteId = websiteId;
			this.Name = name;
			this.PortNumber = portNumber;
		}

		/// <summary>
		/// 获取域名隶属的网站的编号。
		/// </summary>
		public int WebsiteId { get; private set; }

		/// <summary>
		/// 获取域名的主机名。
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// 获取访问此域名时使用的端口号。
		/// </summary>
		public int PortNumber { get; private set; }

		/// <summary>
		/// 获取或设置域名的备案编号。
		/// </summary>
		public string ICPNumber { get; set; }

		/// <summary>
		/// 获取或设置域名的描述。
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// 获取或设置一个值，该值指示当前域名是否已设置为默认。
		/// </summary>
		public bool IsDefault { get; set; }

		/// <summary>
		/// 获取或设置一个值，该值指示当前域名是否处于启用状态。
		/// </summary>
		public bool Enabled { get; set; }

		/// <summary>
		/// 较当前对象和同一类型的另一对象。
		/// </summary>
		/// <param name="other">与此对象进行比较的对象。</param>
		/// <returns>一个值，指示要比较的对象的相对顺序。</returns>
		public override int CompareTo(EntityObject<int, string> other) { return this.Id - other.Id; }
	}
}