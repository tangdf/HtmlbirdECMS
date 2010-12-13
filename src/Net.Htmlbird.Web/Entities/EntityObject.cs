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
using MongoDB.Bson.DefaultSerializer;
using Net.Htmlbird.Framework.Modules;

namespace Net.Htmlbird.Framework.Web.Entities
{
	/// <summary>
	/// 为门户系统组件提供基类。此类无法实例化。
	/// </summary>
	[Serializable]
	public abstract class EntityObject : EntityObject<int, string>
	{
		/// <summary>
		/// 初始化 <see cref="EntityObject"/> 类的新实例。
		/// </summary>
		protected EntityObject() : this(0, String.Empty) { }

		/// <summary>
		/// 初始化 <see cref="EntityObject"/> 类的新实例。
		/// </summary>
		/// <param name="id">实体对象的标识符。</param>
		/// <param name="displayId">实体对象的用于显示的标识符。</param>
		protected EntityObject(int id, string displayId) : base(id, displayId)
		{
			if (displayId == null) throw new ArgumentNullException("displayId");

			this.Id = id;
			this.DisplayId = displayId;
		}
	}

	/// <summary>
	/// 为门户系统组件提供基类。此类无法实例化。
	/// </summary>
	/// <typeparam name="TId">实体对象的唯一标志符的类型。</typeparam>
	[Serializable]
	public abstract class EntityObject<TId> : EntityObject<TId, string>
	{
		/// <summary>
		/// 初始化 <see cref="EntityObject&lt;TId&gt;"/> 类的新实例。
		/// </summary>
		protected EntityObject() : this(default(TId), String.Empty) { }

		/// <summary>
		/// 初始化 <see cref="EntityObject&lt;TId&gt;"/> 类的新实例。
		/// </summary>
		/// <param name="id">实体对象的标识符。</param>
		/// <param name="displayId">实体对象的用于显示的标识符。</param>
		protected EntityObject(TId id, string displayId) : base(id, displayId)
		{
			if (displayId == null) throw new ArgumentNullException("displayId");
			if (String.IsNullOrWhiteSpace(displayId)) throw new ArgumentOutOfRangeException("displayId");

			this.Id = id;
			this.DisplayId = displayId;
		}
	}

	/// <summary>
	/// 为门户系统组件提供基类。此类无法实例化。
	/// </summary>
	/// <typeparam name="TId">实体对象的唯一标志符的类型。</typeparam>
	/// <typeparam name="TDisplayId">实体对象用于显示的标识符的类型。</typeparam>
	[Serializable]
	public abstract class EntityObject<TId, TDisplayId> : IComparable<EntityObject<TId, TDisplayId>>
	{
		/// <summary>
		/// 初始化 <see cref="EntityObject&lt;TId, TDisplayId&gt;"/> 类的新实例。
		/// </summary>
		protected EntityObject() : this(default(TId), default(TDisplayId)) { }

		/// <summary>
		/// 初始化 <see cref="EntityObject&lt;TId, TDisplayId&gt;"/> 类的新实例。
		/// </summary>
		/// <param name="id">实体对象的标识符。</param>
		/// <param name="displayId">实体对象的用于显示的标识符。</param>
		protected EntityObject(TId id, TDisplayId displayId)
		{
			this.Id = id;
			this.DisplayId = displayId;
		}

		/// <summary>
		/// 获取实体对象的唯一标识符。
		/// </summary>
		[BsonId]
		public TId Id { get; protected set; }

		/// <summary>
		/// 获取实体对象的用于显示的标识符。
		/// </summary>
		public TDisplayId DisplayId { get; protected set; }

		/// <summary>
		/// 获取或设置创建此对象的时间。
		/// </summary>
		[DynamicJsonOptions(true, "yyyy-MM-dd HH:mm:ss")]
		public virtual DateTime CreateDate { get; set; }

		/// <summary>
		/// 获取或设置创建此对象的用户的唯一标识符。
		/// </summary>
		public virtual TId CreateUserId { get; set; }

		/// <summary>
		/// 获取或设置最后一次更新此对象信息的时间。
		/// </summary>
		[DynamicJsonOptions(true, "yyyy-MM-dd HH:mm:ss")]
		public virtual DateTime UpdateDate { get; set; }

		/// <summary>
		/// 获取或设置最后一次更新此对象信息的用户的唯一标识符。
		/// </summary>
		public virtual TId UpdateUserId { get; set; }

		#region IComparable<EntityObject<TId,TDisplayId>> Members

		/// <summary>
		/// 较当前对象和同一类型的另一对象。
		/// </summary>
		/// <param name="other">与此对象进行比较的对象。</param>
		/// <returns>一个值，指示要比较的对象的相对顺序。</returns>
		public abstract int CompareTo(EntityObject<TId, TDisplayId> other);

		#endregion
	}
}