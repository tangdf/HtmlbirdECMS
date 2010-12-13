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
using System.Xml;
using Net.Htmlbird.Framework.Net;

namespace Net.Htmlbird.Framework.Web.Map
{
	/// <summary>
	/// 封装谷歌地图的常用操作。
	/// </summary>
	public static class GoogleMap
	{
		/// <summary>
		/// 根据指定关键字返回其经纬度信息。
		/// </summary>
		/// <param name="keywords">包含地域关键字的字符串。</param>
		/// <returns>返回经纬度信息。</returns>
		public static MapPoint GetMapPointFromKeywords(string keywords)
		{
			MapBounds bounds;

			return GetMapPointFromKeywords(keywords, out bounds);
		}

		/// <summary>
		/// 根据指定关键字返回其经纬度信息。
		/// </summary>
		/// <param name="keywords">包含地域关键字的字符串。</param>
		/// <param name="bounds">在方法返回时包含该区域的矩形边界信息。</param>
		/// <returns>返回经纬度信息。</returns>
		public static MapPoint GetMapPointFromKeywords(string keywords, out MapBounds bounds)
		{
			bounds = MapBounds.Empty;

			try
			{
				string result;

				using (YMindWebClient webClient = new YMindWebClient {
					Encoding = Encoding.UTF8
				})
				{
					var url = Uri.EscapeUriString(String.Format("http://ditu.google.cn/maps/geo?q={0}&output=xml&sensor=false&key=abcdefg", keywords));

					result = webClient.DownloadString(url);
				}

				if (String.IsNullOrEmpty(result)) return MapPoint.Empty;

				var document = new XmlDocument();
				document.LoadXml(result);

				double lat = 0D, lng = 0D, north = 0D, south = 0D, east = 0D, west = 0D;
				XmlNode node = document["kml"]["Response"]["Placemark"]["ExtendedData"]["LatLonBox"];
				//<LatLonBox north="40.1780212" south="39.6302178" east="116.9204356" west="115.8959604" />
				if (node != null)
				{
					if (double.TryParse(node.Attributes["north"].Value, out north) == false) north = 0;
					if (double.TryParse(node.Attributes["south"].Value, out south) == false) south = 0;
					if (double.TryParse(node.Attributes["east"].Value, out east) == false) east = 0;
					if (double.TryParse(node.Attributes["west"].Value, out west) == false) west = 0;
				}
				//<coordinates>116.4081980,39.9046670,0</coordinates>
				node = document["kml"]["Response"]["Placemark"]["Point"]["coordinates"];

				if (node != null)
				{
					var nodeData = node.InnerText.Split(',');

					if (double.TryParse(nodeData[1], out lat) == false) lat = 0;
					if (double.TryParse(nodeData[0], out lng) == false) lng = 0;
				}

				bounds = new MapBounds(east, west, south, north);

				return new MapPoint(lng, lat);
			}
			catch
			{
				return MapPoint.Empty;
			}
		}
	}
}