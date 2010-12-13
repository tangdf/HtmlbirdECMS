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
using Net.Htmlbird.Framework.Web.Entities;

namespace Net.Htmlbird.Framework.Web.Map
{
	/// <summary>
	/// 为地图区域标识对象信息提供基类。
	/// </summary>
	[Serializable]
	public abstract class MapRegion : EntityObject
	{
		#region 构造函数

		/// <summary>
		/// 使用指定的 <see cref="System.Guid"/> 和 <see cref="MapPoint"/> 初始化 <see cref="MapRegion"/> 类的新实例。
		/// </summary>
		/// <param name="id">唯一标识符。</param>
		/// <param name="displayId">显示标识符。</param>
		/// <param name="relative">一个 <see cref="MapPoint"/>。</param>
		/// <param name="relativeDistance">与 <paramref name="relative"/> 的距离。</param>
		protected MapRegion(int id, string displayId, MapPoint relative, double relativeDistance) : base(id, displayId)
		{
			this.Id = id;
			this.Relative = relative;
			this.RelativeDistance = relativeDistance;
		}

		#endregion

		#region 公有方法

		/// <summary>
		/// 确定坐标点是否包含着当前对象区域内。
		/// </summary>
		/// <param name="mLngLat">一个 <see cref="MapPoint"/>。</param>
		/// <returns>如果坐标点包含在当前对象区域之内则返回 true，否则返回 false。</returns>
		public bool Contains(MapPoint mLngLat) { return this.Location.Lng == mLngLat.Lng && this.Location.Lat == mLngLat.Lat || this.Bounds.Contains(mLngLat); }

		/// <summary>
		/// 将此 <see cref="MapRegion"/> 转换为可读字符串。
		/// </summary>
		/// <returns>表示此 <see cref="MapRegion"/> 的字符串。</returns>
		public override string ToString() { return this.ToJsonString(); }

		/// <summary>
		/// 较当前对象和同一类型的另一对象。
		/// </summary>
		/// <param name="other">与此对象进行比较的对象。</param>
		/// <returns>一个值，指示要比较的对象的相对顺序。</returns>
		public override int CompareTo(EntityObject<int, string> other) { return this.Id - other.Id; }

		#endregion

		#region 公有属性

		/// <summary>
		/// 获取或设置对象的名称。
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 获取或设置对象的名称的拼音。
		/// </summary>
		public string Spell { get; set; }

		/// <summary>
		/// 获取或设置对象的别名。
		/// </summary>
		public string Alias { get; set; }

		/// <summary>
		/// 获取或设置对象的简拼。
		/// </summary>
		public string Abbr { get; set; }

		/// <summary>
		/// 获取或设置对象区域的中心点。
		/// </summary>
		public MapPoint Location { get; set; }

		/// <summary>
		/// 获取或设置对象区域的矩形边界。
		/// </summary>
		public MapBounds Bounds { get; set; }

		/// <summary>
		/// 获取对象与 <see cref="MapRegion.Relative"/> 的距离。
		/// </summary>
		public double RelativeDistance { get; set; }

		/// <summary>
		/// 获取与其相比较的另一坐标点。
		/// </summary>
		public MapPoint Relative { get; private set; }

		/// <summary>
		/// 获取或设置对象的类型。
		/// </summary>
		public abstract MapRegionType RegionType { get; }

		/// <summary>
		/// 获取或设置对象的维护状态。
		/// </summary>
		public int State { get; set; }

		/// <summary>
		/// 获取或设置对象的最后更新时间。
		/// </summary>
		public DateTime UpdateTime { get; set; }

		#endregion
	}
}