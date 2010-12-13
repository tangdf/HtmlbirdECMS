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

namespace Net.Htmlbird.Framework.Web.Configuration
{
	/// <summary>
	/// 表示数据库连接配置信息。
	/// </summary>
	[Serializable]
	public sealed class DatabaseConnectionString
	{
		/// <summary>
		/// 获取或设置数据库连接的唯一名称。
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 获取或设置数据库连接的分组名称，同组的多个连接会自动轮询调度访问。
		/// </summary>
		public string GroupName { get; set; }

		/// <summary>
		/// 获取或设置数据库连接字符串。
		/// </summary>
		public string ConnectionStrings { get; set; }
	}
}