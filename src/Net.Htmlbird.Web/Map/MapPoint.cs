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

namespace Net.Htmlbird.Framework.Web.Map
{
	/// <summary>
	/// 表示地图上的一个点。
	/// </summary>
	[Serializable]
	public class MapPoint
	{
		#region 私有字段

		private double _lat;
		private double _lng;

		#endregion

		#region 公有字段

		/// <summary>
		/// 表示其属性未被初始化的 <see cref="MapPoint"/> 结构。
		/// </summary>
		public static MapPoint Empty = new MapPoint(0.0, 0.0);

		#endregion

		#region 构造函数

		/// <summary>
		/// 初始化 <see cref="MapPoint"/> 类的新实例。
		/// </summary>
		public MapPoint() : this(0, 0) { }

		/// <summary>
		/// 使用指定的经度和纬度初始化 <see cref="MapPoint"/> 类的新实例。
		/// </summary>
		/// <param name="lng">指定地图上的点的经度。</param>
		/// <param name="lat">指定地图上的点的纬度。</param>
		public MapPoint(double lng, double lat)
		{
			if (lng > 180.0 || lng < -180.0) throw new ArgumentOutOfRangeException("lng", "经度值必须是大于等于 -180.0 且小于等于 180.0 的数字。");
			if (lat > 90.0 || lat < -90.0) throw new ArgumentOutOfRangeException("lat", "纬度值必须是大于等于 -90.0 且小于等于 90.0 的数字。");

			this.Lng = lng;
			this.Lat = lat;
		}

		#endregion

		#region 公有方法

		/// <summary>
		/// 指定此 <see cref="MapPoint"/> 是否包含与指定 <see cref="MapPoint"/> 有相同的坐标。
		/// </summary>
		/// <param name="point">要测试的 <see cref="MapPoint"/>。</param>
		/// <returns>如果 <paramref name="point"/> 为 <see cref="MapPoint"/> 并与此 <see cref="MapPoint"/> 的坐标相等，则为 true。</returns>
		public bool Equals(MapPoint point) { return point != null && ((point.Lng == this.Lng) && (point.Lat == this.Lat)); }

		/// <summary>
		/// 指定此 <see cref="MapPoint"/> 是否包含与指定 <see cref="System.Object"/> 有相同的坐标。
		/// </summary>
		/// <param name="obj">要测试的 <see cref="System.Object"/>。</param>
		/// <returns>如果 <paramref name="obj"/> 为 <see cref="MapPoint"/> 并与此 <see cref="MapPoint"/> 的坐标相等，则为 true。</returns>
		public override bool Equals(object obj) { return this.Equals(obj as MapPoint); }

		/// <summary>
		/// 返回此 <see cref="MapPoint"/> 的哈希代码。
		/// </summary>
		/// <returns>一个整数值，它指定此 <see cref="MapPoint"/> 的哈希值。</returns>
		public override int GetHashCode() { return (this.Lng.GetHashCode() * 397) ^ this.Lat.GetHashCode(); }

		/// <summary>
		/// 将此 <see cref="MapPoint"/> 转换为可读字符串。
		/// </summary>
		/// <returns>表示此 <see cref="MapPoint"/> 的字符串。</returns>
		public override string ToString() { return this.ToJsonString(); }

		/// <summary>
		/// 计算地图上两点之间的距离。
		/// </summary>
		/// <param name="lng">指定要参与运算的地图上的一个点的经度。</param>
		/// <param name="lat">指定要参与运算的地图上的一个点的纬度。</param>
		/// <returns>返回地图上两点之间的大约距离，单位为米（M）。</returns>
		public double DistanceTo(double lng, double lat) { return CalculationDistance(this.Lng, this.Lat, lng, lat); }

		/// <summary>
		/// 计算地图上两点之间的距离。
		/// </summary>
		/// <param name="lng1">指定要参与运算的地图上的第一个点的经度。</param>
		/// <param name="lat1">指定要参与运算的地图上的第一个点的纬度。</param>
		/// <param name="lng2">指定要参与运算的地图上的第二个点的经度。</param>
		/// <param name="lat2">指定要参与运算的地图上的第二个点的纬度。</param>
		/// <returns>返回地图上两点之间的大约距离，单位为米（M）。</returns>
		public static double CalculationDistance(double lng1, double lat1, double lng2, double lat2)
		{
			if ((lng1 == lng2) && (lat1 == lat2)) return 0;

			var aw = (lng2 - lng1) * Math.PI / 180;
			var sw = Math.PI / 2 - lat1 * Math.PI / 180;
			var iw = Math.PI / 2 - lat2 * Math.PI / 180;
			var ow = Math.Cos(iw) * Math.Cos(sw) + Math.Sin(iw) * Math.Sin(sw) * Math.Cos(aw);

			return Math.Round(6378.13627 * Math.Acos(ow) * 1000, 2);
		}

		#endregion

		#region 公有属性

		/// <summary>
		/// 获取或设置地图上的点的经度。
		/// </summary>
		public double Lng
		{
			get { return this._lng; }
			set
			{
				this._lng = value;

				//base.Longitude = CalculationDistance(this._lng, 0, 0, 0);
			}
		}

		/// <summary>
		/// 获取或设置地图上的点的纬度。
		/// </summary>
		public double Lat
		{
			get { return this._lat; }
			set
			{
				this._lat = value;

				//base.Latitude = CalculationDistance(0, this._lat, 0, 0);
			}
		}

		#endregion
	}
}