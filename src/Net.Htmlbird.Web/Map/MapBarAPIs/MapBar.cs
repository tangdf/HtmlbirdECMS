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
using System.Text;
using Net.Htmlbird.Framework.Modules;

namespace Net.Htmlbird.Framework.Web.Map.MapBarAPIs
{
	/// <summary>
	/// 包含一组方法和属性，提供对 MapBar 电子地图平台的基本操作。
	/// </summary>
	public static class MapBar
	{
		/// <summary>
		/// 表示 MapBar 的授权码。该授权码用于加密和解密经纬度信息。
		/// </summary>
		public static int LicenseKeyCode = 3409;

		#region 距离运算

		/// <summary>
		/// 计算地图上两点之间的距离。
		/// </summary>
		/// <param name="point1">指定要参与运算的地图上的第一个点的实例。</param>
		/// <param name="point2">指定要参与运算的地图上的第二个点的实例。</param>
		/// <returns>返回地图上两点之间的大约距离，单位为米（M）。</returns>
		public static double CalculationDistance(MapPoint point1, MapPoint point2) { return CalculationDistance(point1.Lng, point1.Lat, point2.Lng, point2.Lat); }

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

			// 有误差可以调整比例因子，目前误差为 100000 米小于 2 米
			return Math.Round(6378.13627 * Math.Acos(ow) * 1000, 2);
		}

		#endregion

		/// <summary>
		/// 解码 MapBar 的字符串格式的经纬度，并返回 <see cref="MapPoint"/>。
		/// </summary>
		/// <param name="coord">MapBar 的字符串格式的经纬度。</param>
		/// <returns>如果指定的经纬度字符串合法且成功解码则返回包含其等效数值的 <see cref="MapPoint"/>，否则返回 <see cref="MapPoint.Empty"/>。</returns>
		public static MapPoint DecodeCoordinate(string coord)
		{
			var maxNumPos = -1;
			var maxNum = 0L;
			var org = String.Empty;

			for (var i = 0; i < coord.Length; i++)
			{
				var c = AnyRadixConvert.ToInt64(coord[i].ToString(), 36) - 10;

				if (c > 10) c -= 7;

				org += AnyRadixConvert.ToString(c, 36);

				if (c <= maxNum) continue;

				maxNumPos = i;
				maxNum = c;
			}

			var diff = AnyRadixConvert.ToInt64(org.Substring(0, maxNumPos), 16);
			var sum = AnyRadixConvert.ToInt64(org.Substring(maxNumPos + 1), 16);
			var x = (diff + sum - LicenseKeyCode) / 2.0;
			var y = (sum - x) / 100000.0;

			return new MapPoint(x / 100000.0, y);
		}

		/// <summary>
		/// 编码指定 <see cref="MapPoint"/>，并返回与其等效的字符串表示形式。
		/// </summary>
		/// <param name="coord"><see cref="MapPoint"/>。</param>
		/// <returns>返回与 <paramref name="coord"/> 的数值等效的字符串表示形式。</returns>
		public static string EncodeCooridnate(MapPoint coord) { return EncodeCooridnate(coord.Lng * 100000.0, coord.Lat * 100000.0); }

		/// <summary>
		/// 编码指定的经纬度，并返回与其等效的字符串表示形式。
		/// </summary>
		/// <param name="lng">经度。</param>
		/// <param name="lat">纬度。</param>
		/// <returns>返回表示经纬度的经过编码的字符串表示形式。</returns>
		public static string EncodeCooridnate(double lng, double lat)
		{
			var diff = AnyRadixConvert.ToString(lng - lat + LicenseKeyCode, 16);
			var sum = AnyRadixConvert.ToString(lng + lat, 16);
			var result = new StringBuilder();

			for (var i = 0; i < diff.Length; i++)
			{
				var c = AnyRadixConvert.ToInt64(diff[i].ToString(), 16);

				result.Append(AnyRadixConvert.ToString((c >= 10 ? c + 7 : c) + 10, 36));
			}

			result.Append('z');

			for (var i = 0; i < sum.Length; i++)
			{
				var c = AnyRadixConvert.ToInt64(sum[i].ToString(), 16);

				result.Append(AnyRadixConvert.ToString((c >= 10 ? c + 7 : c) + 10, 36));
			}

			return result.ToString().ToUpper();
		}
	}
}