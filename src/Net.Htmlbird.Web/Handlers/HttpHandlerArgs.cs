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
using System.Web;
using Net.Htmlbird.Framework.Extensions.String_;

namespace Net.Htmlbird.Framework.Web.Handlers
{
	/// <summary>
	/// 为所有以从 <see cref="HttpRequest"/> 对象获取的变量作为参数的对象提供基类。<br />
	/// 所有用于网鸟门户网站管理系统内的页面参数必须封装于此类的派生类当中。
	/// </summary>
	[Serializable]
	public abstract class HttpHandlerArgs
	{
		#region QueryString

		/// <summary>
		/// 获取 HTTP 查询字符串变量集合中具有指定键的项。
		/// </summary>
		/// <param name="name">指定要查询的键名。</param>
		/// <param name="defs">指定一个默认值，当要获取的项不存在或者键值为空时则使用此默认值。</param>
		/// <param name="safeit">如果设置为 true，那么返回值中的“'”将被替换为“''”。</param>
		/// <returns>如果找到包含指定键的项并且该项的键值不为空的时候返回其键值；否则返回 <paramref name="defs"/> 的指定值。</returns>
		public static string GetQuery(string name, string defs, bool safeit = false)
		{
			if (Request == null) return defs;

			var value = Request[name];

			if (String.IsNullOrEmpty(value)) value = defs;

			return safeit ? value.Replace("'", "''") : value;
		}

		/// <summary>
		/// 获取 HTTP 查询字符串变量集合中具有指定键的项。
		/// </summary>
		/// <param name="name">指定要查询的键名。</param>
		/// <param name="defs">指定一个默认值，当要获取的项不存在或者键值为空时则使用此默认值。</param>
		/// <returns>如果找到包含指定键的项并且该项的键值不为空的时候返回其键值；否则返回 <paramref name="defs"/> 的指定值。</returns>
		public static int GetQuery(string name, int defs)
		{
			int value;

			return !int.TryParse(GetQuery(name, "@"), out value) ? defs : value;
		}

		/// <summary>
		/// 获取 HTTP 查询字符串变量集合中具有指定键的项。
		/// </summary>
		/// <param name="name">指定要查询的键名。</param>
		/// <param name="defs">指定一个默认值，当要获取的项不存在或者键值为空时则使用此默认值。</param>
		/// <returns>如果找到包含指定键的项并且该项的键值不为空的时候返回其键值；否则返回 <paramref name="defs"/> 的指定值。</returns>
		public static uint GetQuery(string name, uint defs)
		{
			uint value;

			return !uint.TryParse(GetQuery(name, "@"), out value) ? defs : value;
		}

		/// <summary>
		/// 获取 HTTP 查询字符串变量集合中具有指定键的项。
		/// </summary>
		/// <param name="name">指定要查询的键名。</param>
		/// <param name="defs">指定一个默认值，当要获取的项不存在或者键值为空时则使用此默认值。</param>
		/// <returns>如果找到包含指定键的项并且该项的键值不为空的时候返回其键值；否则返回 <paramref name="defs"/> 的指定值。</returns>
		public static long GetQuery(string name, long defs)
		{
			long value;

			return !long.TryParse(GetQuery(name, "@"), out value) ? defs : value;
		}

		/// <summary>
		/// 获取 HTTP 查询字符串变量集合中具有指定键的项。
		/// </summary>
		/// <param name="name">指定要查询的键名。</param>
		/// <param name="defs">指定一个默认值，当要获取的项不存在或者键值为空时则使用此默认值。</param>
		/// <returns>如果找到包含指定键的项并且该项的键值不为空的时候返回其键值；否则返回 <paramref name="defs"/> 的指定值。</returns>
		public static decimal GetQuery(string name, decimal defs)
		{
			decimal value;

			return !decimal.TryParse(GetQuery(name, "@"), out value) ? defs : value;
		}

		/// <summary>
		/// 获取 HTTP 查询字符串变量集合中具有指定键的项。
		/// </summary>
		/// <param name="name">指定要查询的键名。</param>
		/// <param name="defs">指定一个默认值，当要获取的项不存在或者键值为空时则使用此默认值。</param>
		/// <returns>如果找到包含指定键的项并且该项的键值不为空的时候返回其键值；否则返回 <paramref name="defs"/> 的指定值。</returns>
		public static float GetQuery(string name, float defs)
		{
			float value;

			return !float.TryParse(GetQuery(name, "@"), out value) ? defs : value;
		}

		/// <summary>
		/// 获取 HTTP 查询字符串变量集合中具有指定键的项。
		/// </summary>
		/// <param name="name">指定要查询的键名。</param>
		/// <param name="defs">指定一个默认值，当要获取的项不存在或者键值为空时则使用此默认值。</param>
		/// <returns>如果找到包含指定键的项并且该项的键值不为空的时候返回其键值；否则返回 <paramref name="defs"/> 的指定值。</returns>
		public static double GetQuery(string name, double defs)
		{
			double value;

			return !double.TryParse(GetQuery(name, "@"), out value) ? defs : value;
		}

		/// <summary>
		/// 获取 HTTP 查询字符串变量集合中具有指定键的项。
		/// </summary>
		/// <param name="name">指定要查询的键名。</param>
		/// <param name="defs">指定一个默认值，当要获取的项不存在或者键值为空时则使用此默认值。</param>
		/// <returns>如果找到包含指定键的项并且该项的键值不为空的时候返回其键值；否则返回 <paramref name="defs"/> 的指定值。</returns>
		public static DateTime GetQuery(string name, DateTime defs)
		{
			DateTime value;

			return !DateTime.TryParse(GetQuery(name, String.Empty), out value) ? defs : value;
		}

		/// <summary>
		/// 获取 HTTP 查询字符串变量集合中具有指定键的项。
		/// </summary>
		/// <param name="name">指定要查询的键名。</param>
		/// <param name="defs">指定一个默认值，当要获取的项不存在或者键值为空时则使用此默认值。</param>
		/// <returns>如果找到包含指定键的项并且该项的键值不为空的时候返回其键值；否则返回 <paramref name="defs"/> 的指定值。</returns>
		public static Boolean GetQuery(string name, Boolean defs)
		{
			Boolean value;

			return !Boolean.TryParse(GetQuery(name, "false"), out value) ? defs : value;
		}

		#endregion

		#region Form

		/// <summary>
		/// 获取 Web 窗体变量变量集合中具有指定键的项。
		/// </summary>
		/// <param name="name">指定要查询的键名。</param>
		/// <param name="defs">指定一个默认值，当要获取的项不存在或者键值为空时则使用此默认值。</param>
		/// <param name="safeit">如果设置为 true，那么返回值中的“'”将被替换为“''”。</param>
		/// <returns>如果找到包含指定键的项并且该项的键值不为空的时候返回其键值；否则返回 <paramref name="defs"/> 的指定值。</returns>
		public static string GetForm(string name, string defs, bool safeit = false)
		{
			if (Request == null) return defs;

			var value = Request.Form[name];

			if (String.IsNullOrEmpty(value)) value = defs;

			return safeit ? value.Replace("'", "''") : value;
		}

		/// <summary>
		/// 获取 Web 窗体变量变量集合中具有指定键的项。
		/// </summary>
		/// <param name="name">指定要查询的键名。</param>
		/// <param name="defs">指定一个默认值，当要获取的项不存在或者键值为空时则使用此默认值。</param>
		/// <returns>如果找到包含指定键的项并且该项的键值不为空的时候返回其键值；否则返回 <paramref name="defs"/> 的指定值。</returns>
		public static int GetForm(string name, int defs)
		{
			int value;

			return !int.TryParse(GetForm(name, "@"), out value) ? defs : value;
		}

		/// <summary>
		/// 获取 Web 窗体变量变量集合中具有指定键的项。
		/// </summary>
		/// <param name="name">指定要查询的键名。</param>
		/// <param name="defs">指定一个默认值，当要获取的项不存在或者键值为空时则使用此默认值。</param>
		/// <returns>如果找到包含指定键的项并且该项的键值不为空的时候返回其键值；否则返回 <paramref name="defs"/> 的指定值。</returns>
		public static uint GetForm(string name, uint defs)
		{
			uint value;

			return !uint.TryParse(GetForm(name, "@"), out value) ? defs : value;
		}

		/// <summary>
		/// 获取 Web 窗体变量变量集合中具有指定键的项。
		/// </summary>
		/// <param name="name">指定要查询的键名。</param>
		/// <param name="defs">指定一个默认值，当要获取的项不存在或者键值为空时则使用此默认值。</param>
		/// <returns>如果找到包含指定键的项并且该项的键值不为空的时候返回其键值；否则返回 <paramref name="defs"/> 的指定值。</returns>
		public static long GetForm(string name, long defs)
		{
			long value;

			return !long.TryParse(GetForm(name, "@"), out value) ? defs : value;
		}

		/// <summary>
		/// 获取 Web 窗体变量变量集合中具有指定键的项。
		/// </summary>
		/// <param name="name">指定要查询的键名。</param>
		/// <param name="defs">指定一个默认值，当要获取的项不存在或者键值为空时则使用此默认值。</param>
		/// <returns>如果找到包含指定键的项并且该项的键值不为空的时候返回其键值；否则返回 <paramref name="defs"/> 的指定值。</returns>
		public static decimal GetForm(string name, decimal defs)
		{
			decimal value;

			return !decimal.TryParse(GetForm(name, "@"), out value) ? defs : value;
		}

		/// <summary>
		/// 获取 Web 窗体变量变量集合中具有指定键的项。
		/// </summary>
		/// <param name="name">指定要查询的键名。</param>
		/// <param name="defs">指定一个默认值，当要获取的项不存在或者键值为空时则使用此默认值。</param>
		/// <returns>如果找到包含指定键的项并且该项的键值不为空的时候返回其键值；否则返回 <paramref name="defs"/> 的指定值。</returns>
		public static float GetForm(string name, float defs)
		{
			float value;

			return !float.TryParse(GetForm(name, "@"), out value) ? defs : value;
		}

		/// <summary>
		/// 获取 Web 窗体变量变量集合中具有指定键的项。
		/// </summary>
		/// <param name="name">指定要查询的键名。</param>
		/// <param name="defs">指定一个默认值，当要获取的项不存在或者键值为空时则使用此默认值。</param>
		/// <returns>如果找到包含指定键的项并且该项的键值不为空的时候返回其键值；否则返回 <paramref name="defs"/> 的指定值。</returns>
		public static double GetForm(string name, double defs)
		{
			double value;

			return !double.TryParse(GetForm(name, "@"), out value) ? defs : value;
		}

		/// <summary>
		/// 获取 Web 窗体变量变量集合中具有指定键的项。
		/// </summary>
		/// <param name="name">指定要查询的键名。</param>
		/// <param name="defs">指定一个默认值，当要获取的项不存在或者键值为空时则使用此默认值。</param>
		/// <returns>如果找到包含指定键的项并且该项的键值不为空的时候返回其键值；否则返回 <paramref name="defs"/> 的指定值。</returns>
		public static DateTime GetForm(string name, DateTime defs)
		{
			DateTime value;

			return !DateTime.TryParse(GetForm(name, String.Empty), out value) ? defs : value;
		}

		/// <summary>
		/// 获取 Web 窗体变量变量集合中具有指定键的项。
		/// </summary>
		/// <param name="name">指定要查询的键名。</param>
		/// <param name="defs">指定一个默认值，当要获取的项不存在或者键值为空时则使用此默认值。</param>
		/// <returns>如果找到包含指定键的项并且该项的键值不为空的时候返回其键值；否则返回 <paramref name="defs"/> 的指定值。</returns>
		public static Boolean GetForm(string name, Boolean defs)
		{
			Boolean value;

			return !Boolean.TryParse(GetForm(name, "false"), out value) ? defs : value;
		}

		#endregion

		#region 其他

		/// <summary>
		/// 获取远程客户端的 IP 主机地址。
		/// </summary>
		/// <param name="isval">是否对 IP 地址格式执行有效性验证。</param>
		/// <returns>客户端的 IP 主机地址。</returns>
		public static string GetUserHostAddress(bool isval = false)
		{
			if (Request == null) return null;

			// 为安全起见，不主动穿透代理。
			var value = Request.UserHostAddress;

			return String.IsNullOrEmpty(value) ? "0.0.0.0" : (isval && !value.IsIPAddress() ? "0.0.0.0" : value);
		}

		/// <summary>
		/// 将此 <see cref="HttpHandlerArgs"/> 转换为可读字符串。
		/// </summary>
		/// <returns>表示此 <see cref="HttpHandlerArgs"/> 的字符串。</returns>
		public override string ToString() { return this.ToJsonString(); }

		#endregion

		#region 公有属性

		/// <summary>
		/// 获取当前 HTTP 请求的 <see cref="HttpRequest"/> 对象。
		/// </summary>
		public static HttpRequest Request { get { return HttpContext.Current.Request; } }

		#endregion
	}
}