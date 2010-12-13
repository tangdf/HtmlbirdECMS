// ===============================================================================
//  产品名称：网鸟电子商务管理系统(Htmlbird ECMS)
//  产品作者：YMind Chan
//  版权所有：网鸟IT技术论坛 颜铭工作室
// ===============================================================================
//  Copyright © Htmlbird.Net. All rights reserved .
//  官方网站：http://www.htmlbird.net/
//  技术论坛：http://bbs.htmlbird.net/
// ===============================================================================
using System.Collections.Generic;
using System.Data.Common;

namespace Net.Htmlbird.Framework.Web.Data
{
	public abstract class SqlObjectDALBase<T> : SqlDALBase
	{
		/// <summary>
		/// 初始化 <see cref="SqlObjectDALBase&lt;T&gt;"/> 类的新实例。
		/// </summary>
		/// <param name="connectionString">数据库连接字符串。</param>
		/// <param name="sqlProvider">用于访问基础数据存储区的 ADO.NET 提供程序的名称。</param>
		protected SqlObjectDALBase(string connectionString, string sqlProvider) : base(connectionString, sqlProvider) { }

		#region 受保护方法

		/// <summary>
		/// 从指定的 <see cref="DbDataReader"/> 实例中提取实体对象。
		/// </summary>
		/// <param name="reader">指定包含要提取的数据的 <see cref="DbDataReader"/>。</param>
		/// <returns>如果提取成功则返回 <see cref="T"/> 的实例，否则返回 default(T)。</returns>
		protected virtual T FetchItem(DbDataReader reader) { return default(T); }

		#endregion

		#region 公有方法

		/// <summary>
		/// 返回所有 <see cref="List{T}"/> 的集合。
		/// </summary>
		/// <returns>返回 <see cref="List&lt;T&gt;"/> 集合。</returns>
		public abstract List<T> GetAllItems();

		/// <summary>
		/// 向数据库添加一条数据。
		/// </summary>
		/// <param name="item">指定一个 <see cref="T"/> 的实例。</param>
		public abstract void AddItem(T item);

		/// <summary>
		/// 向数据库添加一个数据集合。
		/// </summary>
		/// <param name="items">指定一个 <see cref="List&lt;T&gt;"/> 集合。</param>
		public abstract void AddItem(List<T> items);

		/// <summary>
		/// 清空数据库中（当前表）的所有记录。
		/// </summary>
		public abstract void DeleteAllItems();

		/// <summary>
		/// 删除一条数据。
		/// </summary>
		/// <param name="item">指定一个 <see cref="T"/> 的实例。</param>
		public abstract void DeleteItem(T item);

		/// <summary>
		/// 删除一个数据集合。
		/// </summary>
		/// <param name="items">指定一个 <see cref="List&lt;T&gt;"/> 集合。</param>
		public abstract void DeleteItem(List<T> items);

		/// <summary>
		/// 更新一条数据。
		/// </summary>
		/// <param name="item">指定一个 <see cref="T"/> 的实例。</param>
		public abstract void UpdateItem(T item);

		/// <summary>
		/// 更新一个数据集合。
		/// </summary>
		/// <param name="items">指定一个 <see cref="List&lt;T&gt;"/> 集合。</param>
		public abstract void UpdateItem(List<T> items);

		/// <summary>
		/// 更新或插入数据。当指定的内容已经存在于数据库时将执行更新，当指定的内容不存在时将在数据库中添加新的记录。
		/// </summary>
		/// <param name="item">指定一个 <see cref="T"/> 的实例。</param>
		public abstract void UpdateAndInsert(T item);

		/// <summary>
		/// 更新或插入数据集合。当集合中的项已经存在于数据库时将执行更新，当集合中的项不存在时将在数据库中添加新的记录。
		/// </summary>
		/// <param name="items">指定一个 <see cref="List&lt;T&gt;"/> 集合。</param>
		public abstract void UpdateAndInsert(List<T> items);

		#endregion
	}
}