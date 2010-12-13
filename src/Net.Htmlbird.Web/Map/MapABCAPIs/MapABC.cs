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
using Net.Htmlbird.Framework.Utilities;

namespace Net.Htmlbird.Framework.Web.Map.MapABCAPIs
{
	/// <summary>
	/// 包含一组方法和属性，提供对 MapABC 电子地图平台的基本操作。
	/// </summary>
	public static class MapABC
	{
		#region 私有字段

		/// <summary>
		/// 表示用户编码和解码 MapABC 经纬度的键的索引表。
		/// </summary>
		private static readonly byte[][] _keys = new[] {new byte[] {0, 2, 1, 2, 8, 9, 4, 1, 7, 2, 5, 3, 9}, new byte[] {0, 3, 2, 2, 9, 5, 8, 2, 6, 8, 4, 6, 3}, new byte[] {1, 5, 2, 7, 1, 4, 7, 2, 4, 1, 4, 3, 0}, new byte[] {0, 7, 8, 3, 4, 9, 0, 6, 7, 7, 4, 4, 2}, new byte[] {0, 2, 1, 8, 4, 9, 3, 2, 3, 1, 5, 7, 8}, new byte[] {0, 0, 9, 5, 4, 7, 3, 0, 8, 7, 5, 2, 8}, new byte[] {0, 1, 5, 1, 1, 8, 2, 7, 1, 9, 1, 3, 5}, new byte[] {0, 5, 2, 5, 6, 0, 3, 4, 6, 7, 1, 3, 5}, new byte[] {1, 3, 2, 1, 8, 1, 8, 3, 7, 9, 2, 7, 0}, new byte[] {1, 2, 7, 7, 4, 3, 1, 5, 5, 0, 6, 4, 4}, new byte[] {1, 5, 2, 8, 9, 2, 5, 9, 6, 7, 3, 3, 5}, new byte[] {1, 7, 9, 4, 5, 0, 9, 4, 9, 6, 1, 9, 9}, new byte[] {0, 6, 8, 3, 3, 6, 3, 5, 2, 0, 0, 9, 1}, new byte[] {1, 1, 1, 4, 7, 8, 6, 9, 6, 8, 8, 4, 6}, new byte[] {0, 5, 2, 1, 2, 5, 7, 0, 0, 4, 7, 4, 1}, new byte[] {0, 7, 6, 4, 2, 3, 9, 0, 7, 8, 5, 6, 7}, new byte[] {0, 1, 7, 6, 0, 5, 4, 7, 6, 7, 7, 5, 7}, new byte[] {0, 5, 2, 9, 8, 1, 7, 8, 3, 8, 5, 4, 5}, new byte[] {0, 4, 3, 1, 2, 8, 3, 7, 0, 9, 4, 8, 8}, new byte[] {1, 0, 6, 7, 9, 4, 3, 5, 2, 9, 8, 7, 7}, new byte[] {1, 6, 4, 4, 6, 7, 1, 4, 4, 2, 6, 7, 5}, new byte[] {0, 8, 1, 7, 7, 5, 2, 6, 4, 3, 9, 7, 5}, new byte[] {1, 7, 0, 5, 6, 2, 5, 2, 7, 4, 6, 2, 8}, new byte[] {0, 4, 9, 2, 3, 0, 5, 4, 7, 8, 7, 0, 5}, new byte[] {1, 1, 0, 5, 1, 7, 2, 8, 7, 2, 6, 9, 3}, new byte[] {1, 4, 2, 3, 6, 1, 5, 3, 2, 0, 3, 6, 2}, new byte[] {1, 1, 6, 5, 1, 0, 6, 8, 9, 7, 1, 7, 9}, new byte[] {0, 6, 5, 4, 0, 7, 1, 7, 6, 2, 5, 4, 2}, new byte[] {1, 9, 8, 6, 6, 6, 8, 4, 5, 4, 0, 4, 0}, new byte[] {1, 2, 7, 1, 5, 0, 6, 8, 0, 1, 3, 7, 9}, new byte[] {1, 1, 6, 4, 9, 8, 6, 0, 6, 2, 1, 9, 8}, new byte[] {0, 0, 1, 9, 5, 3, 3, 9, 6, 7, 4, 1, 1}, new byte[] {0, 2, 8, 5, 7, 8, 6, 7, 3, 3, 1, 6, 4}, new byte[] {1, 8, 2, 5, 8, 4, 7, 6, 8, 8, 5, 7, 6}, new byte[] {0, 8, 3, 4, 9, 6, 1, 7, 8, 3, 0, 5, 5}, new byte[] {1, 3, 2, 6, 7, 4, 2, 8, 7, 4, 9, 6, 8}, new byte[] {1, 8, 8, 9, 3, 9, 1, 8, 5, 7, 2, 5, 0}, new byte[] {0, 5, 8, 3, 1, 8, 8, 0, 3, 9, 3, 8, 1}, new byte[] {1, 6, 0, 1, 1, 0, 3, 4, 3, 3, 3, 5, 9}, new byte[] {1, 0, 5, 1, 7, 9, 6, 2, 4, 6, 0, 3, 5}, new byte[] {1, 8, 2, 0, 9, 7, 1, 0, 5, 5, 8, 0, 6}, new byte[] {1, 8, 9, 6, 7, 3, 9, 4, 1, 9, 6, 6, 2}, new byte[] {0, 6, 0, 0, 8, 2, 6, 5, 9, 4, 1, 6, 2}, new byte[] {1, 7, 9, 7, 9, 4, 4, 2, 1, 1, 5, 7, 4}, new byte[] {1, 3, 0, 4, 3, 4, 6, 8, 6, 9, 1, 7, 0}, new byte[] {0, 1, 2, 3, 9, 4, 1, 8, 7, 2, 2, 9, 8}, new byte[] {1, 6, 5, 3, 2, 7, 6, 6, 9, 0, 0, 7, 7}, new byte[] {1, 6, 8, 4, 9, 7, 8, 0, 3, 6, 5, 4, 8}, new byte[] {0, 6, 6, 0, 9, 9, 4, 5, 5, 6, 8, 3, 7}, new byte[] {1, 0, 1, 3, 4, 0, 0, 1, 4, 8, 5, 7, 0}, new byte[] {1, 0, 2, 5, 8, 2, 2, 4, 8, 9, 7, 1, 6}, new byte[] {1, 4, 2, 6, 6, 8, 4, 5, 6, 6, 4, 5, 9}, new byte[] {1, 4, 4, 1, 7, 2, 0, 4, 6, 3, 3, 6, 7}, new byte[] {0, 2, 2, 3, 8, 0, 0, 8, 6, 0, 2, 1, 7}, new byte[] {0, 9, 4, 4, 8, 1, 2, 7, 3, 2, 6, 8, 0}, new byte[] {0, 9, 8, 4, 2, 1, 4, 5, 2, 4, 9, 5, 1}, new byte[] {0, 7, 2, 4, 7, 4, 3, 2, 4, 1, 5, 6, 9}, new byte[] {1, 1, 8, 4, 8, 8, 8, 4, 3, 4, 1, 2, 5}, new byte[] {0, 3, 2, 7, 5, 7, 0, 2, 7, 4, 5, 3, 5}, new byte[] {0, 3, 0, 4, 6, 6, 6, 5, 7, 2, 1, 9, 5}, new byte[] {1, 5, 6, 0, 1, 3, 2, 7, 3, 0, 9, 8, 6}, new byte[] {0, 5, 5, 1, 7, 1, 0, 7, 9, 0, 3, 5, 7}, new byte[] {0, 5, 4, 9, 7, 9, 7, 3, 8, 0, 1, 6, 3}, new byte[] {1, 9, 2, 7, 3, 7, 9, 4, 3, 9, 8, 8, 2}, new byte[] {0, 3, 1, 8, 9, 0, 9, 0, 4, 5, 5, 0, 9}, new byte[] {1, 8, 6, 1, 7, 7, 2, 4, 7, 9, 2, 0, 8}, new byte[] {0, 6, 1, 2, 7, 1, 4, 8, 4, 1, 1, 6, 0}, new byte[] {0, 3, 9, 8, 5, 5, 3, 0, 8, 7, 9, 3, 5}, new byte[] {0, 8, 4, 3, 7, 3, 1, 8, 2, 9, 1, 4, 7}, new byte[] {0, 1, 5, 3, 4, 0, 5, 5, 5, 8, 0, 7, 2}, new byte[] {0, 1, 7, 1, 8, 2, 1, 9, 8, 6, 1, 7, 0}, new byte[] {0, 7, 1, 6, 9, 7, 2, 7, 2, 4, 4, 3, 6}, new byte[] {0, 6, 2, 7, 2, 3, 4, 9, 3, 0, 1, 6, 3}, new byte[] {0, 2, 9, 1, 9, 9, 9, 1, 9, 5, 4, 4, 4}, new byte[] {0, 1, 8, 7, 0, 0, 5, 2, 1, 5, 7, 4, 6}, new byte[] {1, 9, 0, 8, 7, 3, 3, 5, 5, 4, 9, 0, 1}, new byte[] {1, 5, 8, 0, 1, 7, 0, 2, 3, 7, 3, 2, 9}, new byte[] {1, 3, 2, 0, 5, 2, 7, 5, 0, 2, 6, 8, 1}, new byte[] {0, 2, 7, 2, 3, 2, 2, 9, 6, 9, 4, 1, 6}, new byte[] {1, 6, 4, 7, 9, 6, 5, 9, 5, 8, 2, 7, 1}, new byte[] {1, 8, 1, 2, 6, 0, 2, 4, 0, 8, 0, 1, 6}, new byte[] {1, 6, 2, 4, 1, 2, 4, 1, 7, 2, 7, 0, 6}, new byte[] {0, 1, 8, 0, 5, 0, 4, 5, 5, 1, 0, 4, 7}, new byte[] {0, 8, 7, 6, 4, 3, 5, 5, 7, 8, 4, 9, 0}, new byte[] {0, 2, 7, 7, 0, 1, 6, 6, 1, 0, 9, 3, 5}, new byte[] {0, 7, 6, 9, 8, 3, 8, 6, 2, 9, 3, 7, 0}, new byte[] {1, 6, 6, 6, 0, 3, 0, 1, 0, 2, 5, 6, 1}, new byte[] {0, 0, 4, 5, 1, 0, 9, 4, 4, 9, 4, 0, 9}, new byte[] {0, 1, 6, 9, 4, 7, 5, 7, 8, 3, 5, 7, 0}, new byte[] {1, 2, 7, 1, 6, 6, 1, 5, 2, 8, 6, 3, 8}, new byte[] {1, 9, 1, 6, 7, 5, 1, 7, 4, 7, 6, 1, 8}, new byte[] {1, 7, 6, 7, 0, 2, 9, 6, 9, 8, 6, 7, 8}, new byte[] {0, 9, 8, 7, 3, 8, 1, 5, 2, 5, 2, 7, 5}, new byte[] {0, 7, 3, 5, 7, 9, 7, 6, 6, 9, 1, 7, 5}, new byte[] {1, 6, 7, 3, 4, 4, 7, 6, 2, 6, 6, 2, 3}, new byte[] {0, 1, 4, 2, 2, 8, 5, 0, 9, 2, 7, 3, 1}, new byte[] {0, 1, 4, 2, 1, 0, 0, 2, 1, 8, 9, 8, 3}, new byte[] {1, 7, 0, 8, 7, 9, 9, 6, 4, 8, 6, 2, 2}, new byte[] {1, 9, 3, 9, 9, 8, 7, 0, 8, 1, 1, 7, 3}, new byte[] {1, 0, 4, 3, 5, 8, 0, 4, 6, 5, 4, 5, 8}, new byte[] {0, 4, 8, 0, 5, 2, 3, 2, 3, 9, 4, 2, 3}, new byte[] {0, 7, 9, 0, 9, 7, 2, 7, 7, 0, 4, 8, 5}, new byte[] {1, 6, 5, 5, 3, 3, 2, 6, 1, 3, 4, 7, 1}, new byte[] {0, 2, 9, 0, 0, 2, 9, 1, 8, 8, 2, 8, 4}, new byte[] {1, 3, 2, 5, 0, 6, 2, 5, 3, 3, 6, 1, 1}, new byte[] {1, 9, 2, 9, 3, 3, 8, 9, 9, 7, 2, 3, 7}, new byte[] {1, 1, 8, 4, 0, 8, 2, 4, 8, 0, 0, 9, 2}, new byte[] {1, 5, 2, 6, 0, 6, 1, 3, 0, 4, 7, 3, 8}, new byte[] {1, 9, 3, 8, 1, 1, 7, 8, 6, 9, 0, 6, 8}, new byte[] {1, 3, 2, 7, 7, 2, 2, 4, 2, 5, 8, 3, 0}, new byte[] {1, 1, 1, 0, 7, 7, 3, 4, 7, 3, 6, 6, 8}, new byte[] {0, 9, 4, 2, 8, 9, 4, 8, 4, 3, 2, 5, 3}, new byte[] {0, 1, 0, 9, 2, 7, 2, 3, 9, 4, 5, 0, 8}, new byte[] {1, 0, 4, 5, 8, 4, 0, 0, 5, 2, 2, 1, 2}, new byte[] {0, 5, 0, 4, 5, 3, 2, 5, 4, 1, 3, 6, 9}, new byte[] {1, 3, 0, 2, 7, 8, 1, 7, 7, 3, 5, 5, 9}, new byte[] {1, 3, 7, 0, 0, 5, 8, 1, 7, 5, 6, 5, 2}, new byte[] {1, 8, 1, 9, 9, 9, 4, 8, 6, 0, 7, 7, 3}, new byte[] {0, 8, 3, 6, 2, 7, 4, 2, 1, 9, 1, 6, 8}, new byte[] {0, 4, 4, 4, 2, 6, 0, 4, 0, 1, 5, 1, 7}, new byte[] {1, 2, 7, 4, 7, 6, 6, 6, 3, 7, 7, 2, 9}, new byte[] {0, 9, 8, 9, 3, 3, 3, 9, 0, 7, 4, 2, 3}, new byte[] {0, 7, 6, 0, 9, 1, 7, 2, 4, 5, 8, 3, 3}, new byte[] {1, 6, 1, 5, 5, 3, 1, 3, 2, 1, 0, 5, 6}, new byte[] {0, 6, 2, 4, 1, 6, 6, 3, 4, 9, 2, 7, 0}, new byte[] {1, 6, 3, 2, 3, 6, 1, 7, 7, 5, 6, 7, 1}, new byte[] {1, 0, 4, 9, 2, 3, 3, 6, 2, 6, 9, 3, 2}, new byte[] {0, 3, 7, 3, 9, 1, 3, 9, 5, 8, 5, 8, 9}, new byte[] {1, 9, 0, 0, 3, 0, 9, 1, 2, 7, 8, 0, 3}, new byte[] {1, 0, 1, 2, 7, 7, 0, 0, 1, 8, 4, 1, 1}, new byte[] {0, 0, 5, 5, 9, 6, 9, 8, 1, 2, 1, 7, 2}, new byte[] {0, 1, 8, 7, 9, 0, 3, 5, 6, 3, 2, 9, 4}, new byte[] {1, 3, 1, 5, 7, 5, 0, 8, 5, 3, 2, 5, 0}, new byte[] {1, 1, 7, 3, 5, 0, 7, 7, 9, 6, 8, 9, 0}, new byte[] {0, 7, 7, 0, 9, 4, 2, 8, 8, 0, 2, 2, 0}, new byte[] {1, 6, 5, 8, 3, 1, 0, 9, 0, 2, 7, 2, 9}, new byte[] {1, 3, 5, 8, 4, 7, 6, 3, 1, 4, 3, 4, 7}, new byte[] {0, 8, 8, 7, 8, 2, 7, 0, 3, 9, 6, 2, 9}, new byte[] {1, 1, 6, 2, 6, 7, 5, 2, 5, 0, 8, 5, 5}, new byte[] {0, 9, 6, 7, 3, 0, 2, 3, 9, 5, 3, 7, 4}, new byte[] {1, 5, 2, 7, 3, 6, 0, 8, 3, 3, 9, 0, 3}, new byte[] {0, 3, 6, 8, 9, 1, 7, 7, 3, 8, 7, 3, 8}, new byte[] {0, 1, 2, 5, 4, 9, 8, 0, 3, 6, 4, 0, 4}, new byte[] {1, 2, 4, 1, 6, 8, 1, 5, 8, 3, 6, 4, 3}, new byte[] {1, 9, 3, 1, 0, 8, 4, 4, 0, 1, 6, 0, 8}, new byte[] {0, 4, 5, 1, 0, 2, 1, 7, 1, 6, 1, 3, 3}, new byte[] {0, 9, 5, 6, 8, 2, 2, 4, 0, 3, 9, 8, 1}, new byte[] {1, 9, 3, 5, 4, 3, 1, 2, 2, 2, 0, 8, 7}, new byte[] {0, 5, 6, 8, 1, 5, 7, 7, 8, 9, 4, 0, 6}, new byte[] {1, 0, 4, 6, 4, 6, 7, 4, 6, 0, 3, 6, 2}, new byte[] {1, 3, 3, 0, 2, 5, 3, 1, 9, 2, 3, 6, 8}, new byte[] {0, 6, 9, 6, 3, 6, 9, 6, 2, 1, 5, 0, 7}, new byte[] {1, 6, 5, 3, 0, 0, 0, 6, 2, 3, 8, 6, 0}, new byte[] {1, 0, 7, 1, 2, 0, 3, 0, 3, 0, 8, 8, 0}, new byte[] {0, 7, 1, 4, 3, 1, 8, 6, 7, 8, 1, 5, 4}, new byte[] {0, 6, 3, 5, 5, 4, 8, 9, 4, 8, 3, 1, 7}, new byte[] {0, 6, 4, 3, 1, 0, 7, 2, 9, 0, 5, 6, 7}, new byte[] {0, 6, 3, 7, 7, 0, 6, 8, 6, 7, 4, 6, 0}, new byte[] {0, 4, 2, 7, 2, 4, 1, 4, 6, 1, 8, 1, 7}, new byte[] {1, 1, 7, 9, 0, 7, 0, 5, 1, 8, 6, 3, 5}, new byte[] {1, 2, 0, 2, 7, 2, 7, 9, 1, 2, 7, 0, 3}, new byte[] {0, 3, 3, 6, 2, 0, 9, 1, 1, 0, 3, 5, 8}, new byte[] {1, 4, 0, 9, 9, 2, 5, 6, 5, 6, 8, 0, 5}, new byte[] {0, 3, 5, 3, 3, 3, 4, 6, 7, 5, 7, 0, 5}, new byte[] {0, 5, 8, 8, 5, 8, 5, 4, 7, 0, 5, 7, 3}, new byte[] {0, 5, 0, 7, 6, 4, 2, 7, 8, 3, 6, 1, 4}, new byte[] {0, 4, 7, 8, 6, 5, 3, 7, 7, 5, 7, 0, 7}, new byte[] {1, 3, 6, 5, 3, 0, 8, 5, 4, 9, 7, 7, 1}, new byte[] {1, 4, 8, 2, 8, 2, 8, 3, 4, 9, 4, 6, 7}, new byte[] {1, 4, 1, 6, 9, 4, 5, 7, 7, 4, 6, 7, 7}, new byte[] {0, 2, 8, 2, 3, 0, 7, 7, 1, 0, 1, 1, 0}, new byte[] {1, 2, 2, 4, 5, 4, 7, 1, 0, 1, 8, 6, 7}, new byte[] {0, 0, 7, 2, 4, 7, 2, 8, 2, 4, 4, 3, 9}, new byte[] {1, 9, 1, 3, 2, 4, 1, 3, 3, 7, 5, 6, 1}, new byte[] {1, 4, 7, 4, 6, 8, 6, 7, 4, 4, 1, 2, 8}, new byte[] {0, 1, 6, 7, 3, 9, 0, 4, 7, 2, 9, 6, 7}, new byte[] {0, 1, 3, 9, 1, 1, 1, 1, 6, 3, 0, 1, 1}, new byte[] {1, 2, 7, 0, 2, 0, 7, 9, 7, 2, 1, 5, 2}, new byte[] {0, 9, 1, 0, 4, 2, 8, 2, 2, 4, 2, 4, 0}, new byte[] {1, 1, 7, 9, 7, 9, 3, 0, 5, 3, 4, 5, 2}, new byte[] {0, 0, 7, 4, 3, 0, 8, 6, 7, 7, 7, 9, 6}, new byte[] {0, 7, 0, 4, 0, 6, 7, 6, 3, 2, 0, 7, 1}, new byte[] {0, 4, 8, 8, 0, 5, 3, 0, 7, 8, 4, 7, 9}, new byte[] {0, 6, 3, 3, 3, 6, 6, 3, 7, 0, 4, 8, 3}, new byte[] {0, 1, 2, 0, 6, 0, 3, 1, 0, 9, 9, 8, 0}, new byte[] {0, 7, 0, 3, 8, 2, 5, 0, 7, 5, 0, 0, 4}, new byte[] {1, 8, 8, 8, 2, 0, 6, 2, 5, 6, 2, 3, 2}, new byte[] {1, 6, 2, 5, 8, 0, 1, 9, 7, 3, 7, 6, 0}, new byte[] {0, 3, 6, 1, 9, 1, 6, 8, 2, 6, 5, 2, 5}, new byte[] {0, 3, 9, 7, 8, 9, 4, 5, 4, 8, 5, 5, 1}, new byte[] {1, 1, 5, 5, 2, 5, 3, 4, 5, 3, 5, 0, 9}, new byte[] {1, 0, 9, 4, 9, 6, 1, 7, 0, 0, 6, 0, 1}, new byte[] {0, 8, 4, 9, 9, 9, 3, 4, 1, 3, 5, 7, 7}, new byte[] {0, 7, 8, 0, 0, 3, 5, 5, 9, 4, 1, 8, 1}, new byte[] {1, 7, 3, 7, 6, 3, 2, 5, 6, 2, 7, 5, 0}, new byte[] {0, 0, 2, 6, 0, 6, 6, 2, 7, 6, 1, 6, 2}, new byte[] {1, 1, 6, 4, 7, 7, 9, 7, 0, 6, 2, 6, 6}, new byte[] {0, 2, 1, 1, 4, 7, 6, 8, 8, 8, 9, 4, 3}, new byte[] {0, 0, 8, 7, 5, 1, 9, 3, 1, 9, 8, 6, 0}, new byte[] {0, 3, 4, 4, 0, 7, 1, 8, 7, 2, 7, 9, 9}, new byte[] {1, 0, 4, 5, 3, 6, 0, 6, 6, 6, 4, 1, 5}, new byte[] {0, 9, 7, 9, 9, 5, 9, 2, 3, 0, 4, 6, 2}, new byte[] {1, 6, 5, 2, 7, 2, 1, 3, 5, 2, 5, 2, 1}, new byte[] {1, 9, 9, 4, 8, 6, 3, 7, 8, 3, 3, 0, 6}, new byte[] {0, 8, 2, 6, 6, 7, 8, 2, 1, 3, 2, 9, 2}, new byte[] {0, 4, 8, 1, 9, 2, 4, 8, 4, 5, 4, 6, 4}, new byte[] {1, 1, 7, 0, 7, 3, 5, 1, 4, 9, 5, 3, 1}, new byte[] {1, 7, 8, 8, 3, 5, 3, 1, 5, 7, 6, 1, 9}, new byte[] {1, 4, 5, 6, 5, 3, 2, 5, 3, 0, 3, 5, 5}, new byte[] {0, 0, 2, 1, 3, 8, 9, 1, 0, 9, 7, 6, 7}, new byte[] {0, 0, 7, 6, 1, 9, 1, 9, 5, 8, 9, 4, 0}, new byte[] {1, 5, 4, 4, 6, 8, 7, 3, 9, 9, 0, 7, 4}, new byte[] {1, 3, 0, 4, 8, 1, 2, 3, 9, 7, 1, 9, 5}, new byte[] {1, 2, 6, 1, 4, 6, 9, 4, 7, 1, 1, 2, 6}, new byte[] {0, 1, 6, 7, 5, 8, 3, 2, 7, 0, 4, 1, 1}, new byte[] {1, 6, 2, 7, 8, 7, 6, 8, 7, 2, 0, 3, 3}, new byte[] {0, 2, 1, 9, 2, 6, 7, 5, 9, 5, 2, 2, 2}, new byte[] {0, 5, 2, 0, 4, 7, 7, 3, 8, 1, 5, 0, 9}, new byte[] {1, 6, 5, 8, 6, 4, 0, 9, 6, 9, 0, 1, 8}, new byte[] {1, 2, 0, 8, 7, 9, 2, 4, 4, 0, 9, 8, 9}, new byte[] {1, 6, 5, 2, 0, 6, 1, 0, 4, 4, 1, 5, 8}, new byte[] {1, 5, 4, 2, 5, 6, 2, 5, 6, 2, 2, 9, 5}, new byte[] {1, 6, 9, 7, 2, 5, 1, 0, 6, 9, 1, 8, 1}, new byte[] {0, 0, 3, 9, 9, 0, 6, 7, 9, 5, 7, 4, 6}, new byte[] {1, 5, 8, 9, 9, 0, 6, 7, 9, 7, 9, 6, 1}, new byte[] {1, 3, 6, 4, 6, 3, 6, 8, 4, 5, 2, 8, 3}, new byte[] {0, 7, 4, 8, 4, 9, 7, 8, 0, 0, 1, 2, 2}, new byte[] {0, 4, 2, 9, 1, 3, 8, 8, 3, 0, 0, 9, 8}, new byte[] {1, 9, 0, 9, 2, 1, 2, 9, 3, 6, 5, 3, 2}, new byte[] {1, 1, 0, 2, 0, 5, 9, 9, 5, 4, 7, 8, 9}, new byte[] {1, 6, 0, 5, 9, 9, 1, 9, 0, 5, 4, 7, 1}, new byte[] {1, 0, 4, 0, 0, 3, 2, 4, 1, 6, 4, 6, 5}, new byte[] {1, 7, 3, 7, 3, 3, 7, 6, 1, 7, 7, 8, 6}, new byte[] {0, 9, 1, 7, 3, 5, 1, 8, 9, 3, 8, 6, 2}, new byte[] {1, 4, 9, 9, 3, 7, 5, 4, 4, 4, 4, 4, 0}, new byte[] {0, 3, 7, 7, 4, 3, 6, 1, 1, 3, 5, 1, 6}, new byte[] {0, 8, 5, 4, 3, 9, 3, 3, 1, 3, 4, 8, 1}, new byte[] {1, 6, 1, 9, 4, 6, 4, 6, 4, 5, 2, 1, 5}, new byte[] {1, 1, 1, 6, 8, 3, 9, 1, 1, 3, 0, 9, 9}, new byte[] {0, 5, 1, 6, 8, 4, 8, 8, 2, 4, 4, 9, 2}, new byte[] {0, 2, 3, 0, 1, 4, 2, 7, 1, 9, 9, 0, 6}, new byte[] {0, 8, 4, 2, 5, 1, 4, 9, 5, 2, 0, 4, 3}, new byte[] {0, 9, 1, 2, 5, 0, 6, 6, 5, 0, 3, 1, 8}, new byte[] {1, 7, 8, 7, 1, 7, 4, 6, 3, 3, 3, 3, 9}, new byte[] {0, 3, 7, 2, 9, 4, 1, 5, 4, 7, 2, 1, 0}, new byte[] {1, 2, 8, 1, 1, 6, 4, 7, 8, 2, 0, 5, 2}, new byte[] {1, 8, 3, 5, 4, 8, 0, 9, 7, 8, 0, 1, 8}, new byte[] {1, 7, 9, 9, 0, 4, 5, 7, 2, 9, 0, 1, 9}, new byte[] {0, 6, 6, 5, 6, 7, 0, 4, 0, 7, 8, 5, 1}, new byte[] {0, 6, 0, 6, 3, 1, 1, 5, 0, 9, 2, 2, 3}, new byte[] {1, 6, 3, 5, 6, 7, 1, 6, 6, 9, 7, 4, 9}, new byte[] {0, 9, 5, 9, 8, 2, 4, 3, 3, 2, 3, 5, 6}, new byte[] {0, 1, 6, 3, 8, 9, 9, 2, 8, 2, 5, 8, 6}, new byte[] {1, 4, 7, 6, 6, 5, 7, 3, 3, 3, 4, 1, 1}, new byte[] {1, 8, 2, 9, 0, 3, 8, 6, 8, 3, 3, 7, 3}, new byte[] {0, 2, 8, 4, 8, 5, 4, 8, 9, 5, 0, 5, 7}};

		#endregion

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
		/// 解码 MapABC 的字符串格式的经纬度，并返回数字格式的经纬度。
		/// </summary>
		/// <param name="coord">指定包含要解码的经纬度的字符串。</param>
		/// <returns>如果指定的经纬度字符串合法且成功解码则返回数字格式的经纬度，否则返回 0.0。</returns>
		public static double DecodeCoordinate(string coord)
		{
			int keysindex;

			return DecodeCoordinate(coord, out keysindex);
		}

		/// <summary>
		/// 解码 MapABC 的字符串格式的经纬度，并返回数字格式的经纬度。
		/// </summary>
		/// <param name="coord">指定包含要解码的经纬度的字符串。</param>
		/// <param name="keysindex">在方法返回时包含键值索引，该索引表示使用了哪一组键进行了编码运算。</param>
		/// <returns>如果指定的经纬度字符串合法且成功解码则返回数字格式的经纬度，否则返回 0.0。</returns>
		public static double DecodeCoordinate(string coord, out int keysindex)
		{
			keysindex = 0;
			if (coord == null || coord.Length <= 4) return 0.0;

			double result;
			var buffer = Encoding.ASCII.GetBytes(coord.Substring(coord.Length - 4));

			keysindex |= buffer[0] & 3;
			keysindex |= (buffer[1] & 3) << 2;
			keysindex |= (buffer[2] & 3) << 4;
			keysindex |= (buffer[3] & 3) << 6;

			var keys = _keys[keysindex];
			var la = Encoding.ASCII.GetBytes(coord.Substring(0, coord.Length - 4));
			byte f = 0;

			switch (keys[0])
			{
				case 0:
					f = 23;
					break;
				case 1:
					f = 53;
					break;
			}

			//将所有的asc值进行处理
			for (var i = 0; i < la.Length; i++)
			{
				la[i] -= f;
				la[i] -= keys[i + 1];
			}

			return double.TryParse(Encoding.ASCII.GetString(la), out result) ? result : 0.0;
		}

		/// <summary>
		/// 将数字格式的经纬度编码为 MapABC 的字符串格式的经纬度。
		/// </summary>
		/// <param name="coord">指定包含要编码的经纬度的 <see cref="System.Double"/>。</param>
		/// <returns>如果指定的经纬度成功编码则返回包含 MapABC 格式的经纬度的字符串，否则返回 <see cref="string.Empty"/>。</returns>
		public static string EncodeCooridnate(double coord)
		{
			int keysindex;

			return EncodeCooridnate(coord, out keysindex);
		}

		/// <summary>
		/// 将数字格式的经纬度编码为 MapABC 的字符串格式的经纬度。
		/// </summary>
		/// <param name="coord">指定包含要编码的经纬度的 <see cref="System.Double"/>。</param>
		/// <param name="keysindex">在方法返回时包含键值索引，该索引表示使用了哪一组键进行了编码运算。</param>
		/// <returns>如果指定的经纬度成功编码则返回包含 MapABC 格式的经纬度的字符串，否则返回 <see cref="string.Empty"/>。</returns>
		public static string EncodeCooridnate(double coord, out int keysindex)
		{
			string result;

			var buffer = new byte[4];

			keysindex = StringUtils.Random.Next(0, 256);

			var keys = _keys[keysindex];

			try
			{
				for (var i = 0; i < 4; i++)
				{
					buffer[i] = (byte)Math.Round(StringUtils.Random.NextDouble() * 10);
					buffer[i] = (byte)(buffer[i] + 70);
					buffer[i] = (byte)(buffer[i] >> 2);
					buffer[i] = (byte)(buffer[i] << 2);
				}

				buffer[0] |= (byte)(keysindex & 3);
				buffer[1] |= (byte)((keysindex & 12) >> 2);
				buffer[2] |= (byte)((keysindex & 48) >> 4);
				buffer[3] |= (byte)((keysindex & 192) >> 6);

				var c = coord.ToString("F7").Trim('0');
				c = (c.Length == 0 || c == ".") ? "0.0" : c;
				var la = Encoding.ASCII.GetBytes(c);
				byte f = 0;

				switch (keys[0])
				{
					case 0:
						f = 23;
						break;
					case 1:
						f = 53;
						break;
				}

				for (var i = 0; i < la.Length; i++)
				{
					la[i] += f;
					la[i] += keys[i + 1];
				}

				result = String.Concat(Encoding.ASCII.GetString(la), Encoding.ASCII.GetString(buffer));
			}
			catch
			{
				result = String.Empty;
			}

			return result;
		}
	}
}