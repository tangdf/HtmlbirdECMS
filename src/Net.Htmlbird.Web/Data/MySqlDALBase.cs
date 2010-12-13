// ===============================================================================
//  产品名称：网鸟电子商务管理系统(Htmlbird ECMS)
//  产品作者：YMind Chan
//  版权所有：网鸟IT技术论坛 颜铭工作室
// ===============================================================================
//  Copyright © Htmlbird.Net. All rights reserved .
//  官方网站：http://www.htmlbird.net/
//  技术论坛：http://bbs.htmlbird.net/
// ===============================================================================
namespace Net.Htmlbird.Framework.Web.Data
{
	/// <summary>
	/// 表示 HtmlbirdECMS 数据访问层的为 MySql 数据库提供访问支持的基类。
	/// </summary>
	public abstract class MySqlDALBase<T> : SqlObjectDALBase<T>
	{
		/// <summary>
		/// 初始化 <see cref="MySqlDALBase&lt;T&gt;"/> 类的新实例。
		/// </summary>
		/// <param name="connectionString">数据库连接字符串。</param>
		protected MySqlDALBase(string connectionString) : this(connectionString, "MySql.Data.MySqlClient") { }

		/// <summary>
		/// 初始化 <see cref="MySqlDALBase&lt;T&gt;"/> 类的新实例。
		/// </summary>
		/// <param name="connectionString">数据库连接字符串。</param>
		/// <param name="sqlProvider">用于访问基础数据存储区的 ADO.NET 提供程序的名称。</param>
		protected MySqlDALBase(string connectionString, string sqlProvider) : base(connectionString, sqlProvider) { }
	}
}