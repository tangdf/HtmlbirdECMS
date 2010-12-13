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
	/// <summary>
	/// 表示一个数据分类。
	/// </summary>
	[Serializable]
	public class CategoryInfo : EntityObject
	{
		/// <summary>
		/// 初始化 <see cref="CategoryInfo"/> 类的新实例。
		/// </summary>
		public CategoryInfo() : this(0, String.Empty, String.Empty) { }

		/// <summary>
		/// 初始化 <see cref="CategoryInfo"/> 类的新实例。
		/// </summary>
		/// <param name="id">分类编号。</param>
		/// <param name="name">分类名称。</param>
		public CategoryInfo(int id, string name) : this(id, name, name) { }

		/// <summary>
		/// 初始化 <see cref="CategoryInfo"/> 类的新实例。
		/// </summary>
		/// <param name="id">分类编号。</param>
		/// <param name="displayId">分类显示标识。</param>
		/// <param name="name">分类名称。</param>
		public CategoryInfo(int id, string displayId, string name) : base(id, displayId)
		{
			if (name == null) throw new ArgumentNullException("name");
			if (String.IsNullOrWhiteSpace(name)) throw new ArgumentOutOfRangeException("name");

			this.Name = name;
			this.ChildNodes = new SortedSet<CategoryInfo>(new CategoryInfoComparer());
		}

		/// <summary>
		/// 获取或设置分类名称。
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 获取或设置分类的排序序号，该序号在同级分类上不能重复。
		/// </summary>
		public int SortNumber { get; set; }

		/// <summary>
		/// 获取节点的所有子节点。
		/// </summary>
		public SortedSet<CategoryInfo> ChildNodes { get; private set; }

		/// <summary>
		/// 较当前对象和同一类型的另一对象。
		/// </summary>
		/// <param name="other">与此对象进行比较的对象。</param>
		/// <returns>一个值，指示要比较的对象的相对顺序。</returns>
		public override int CompareTo(EntityObject<int, string> other) { return this.Id - other.Id; }
	}
}