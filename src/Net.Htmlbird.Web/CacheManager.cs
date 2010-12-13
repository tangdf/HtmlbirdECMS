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
using System.Web;
using System.Web.Caching;

namespace Net.Htmlbird.Framework.Web
{
	/// <summary>
	/// 为 HtmlbirdPortal 实现用于 Web 应用程序的缓存管理。无法继承此类。
	/// </summary>
	public sealed class CacheManager
	{
		#region 私有字段

		private const string _DEFAULT_PREFIX = "HtmlbirdECMS";
		private string _prefix;

		#endregion

		#region 构造函数

		/// <summary>
		/// 初始化缓存管理器对象。
		/// </summary>
		internal CacheManager() : this(_DEFAULT_PREFIX) { }

		/// <summary>
		/// 初始化缓存管理器对象。
		/// </summary>
		/// <param name="prefix">包含缓存键名前缀的字符串。</param>
		internal CacheManager(string prefix)
		{
			if (prefix == null) throw new ArgumentNullException("prefix");
			if (prefix.Length == 0) throw new ArgumentOutOfRangeException("prefix");

			this._prefix = prefix;
		}

		#endregion

		#region 私有方法

		/// <summary>
		/// 检查给定的缓存键是否合法，如果不合法则直接修正它。
		/// </summary>
		/// <param name="key">要检查的缓存键。</param>
		/// <returns>经过检查并修正的缓存键。</returns>
		private string _CheckCacheKey(string key)
		{
			if (key == null) throw new ArgumentNullException("key");
			if (key.Length == 0) throw new ArgumentOutOfRangeException("key");

			return key.StartsWith(this._prefix + "-") == false ? (String.Format("{0}-{1}", this._prefix, key)) : key;
		}

		#endregion

		#region 公有方法

		#region 添加缓存

		/// <summary>
		/// 添加一个缓存对象。
		/// </summary>
		/// <param name="key">用于引用该项的缓存键。</param>
		/// <param name="value">要插入缓存中的对象。</param> 
		/// <param name="absoluteExpiration">所插入对象将过期并被从缓存中移除的时间。。</param>
		/// <param name="dependencies">所插入对象的文件依赖项或缓存键依赖项。当任何依赖项更改时，该对象即无效，并从缓存中移除。如果没有依赖项，则此参数包含 null。</param>
		/// <param name="priority">该对象相对于缓存中存储的其他项的成本，由 <see cref="System.Web.Caching.CacheItemPriority"/> 枚举表示。该值由缓存在退出对象时使用；具有较低成本的对象在具有较高成本的对象之前被从缓存移除。</param>
		/// <param name="onRemoveCallback">在从缓存中移除对象时将调用的委托（如果提供）。当从缓存中删除应用程序的对象时，可使用它来通知应用程序。</param>
		public void Add(string key, object value, TimeSpan absoluteExpiration, CacheDependency dependencies = null, CacheItemPriority priority = CacheItemPriority.Normal, CacheItemRemovedCallback onRemoveCallback = null) { this.Add(key, value, DateTime.Now.Add(absoluteExpiration), Cache.NoSlidingExpiration, dependencies, priority, onRemoveCallback); }

		/// <summary>
		/// 添加一个缓存对象。
		/// </summary>
		/// <param name="key">用于引用该项的缓存键。</param>
		/// <param name="value">要插入缓存中的对象。</param> 
		/// <param name="absoluteExpiration">所插入对象将过期并被从缓存中移除的时间。如果使用绝对过期，则 <paramref name="slidingExpiration"/> 参数必须为 <see cref="System.Web.Caching.Cache.NoSlidingExpiration"/>。</param>
		/// <param name="slidingExpiration"> 最后一次访问所插入对象时与该对象过期时之间的时间间隔。如果该值等效于 20 分钟，则对象在最后一次被访问 20 分钟之后将过期并被从缓存中移除。如果使用可调过期，则 <paramref name="absoluteExpiration"/> 参数必须为 <see cref="System.Web.Caching.Cache.NoAbsoluteExpiration"/>。</param>
		/// <param name="dependencies">所插入对象的文件依赖项或缓存键依赖项。当任何依赖项更改时，该对象即无效，并从缓存中移除。如果没有依赖项，则此参数包含 null。</param>
		/// <param name="priority">该对象相对于缓存中存储的其他项的成本，由 <see cref="System.Web.Caching.CacheItemPriority"/> 枚举表示。该值由缓存在退出对象时使用；具有较低成本的对象在具有较高成本的对象之前被从缓存移除。</param>
		/// <param name="onRemoveCallback">在从缓存中移除对象时将调用的委托（如果提供）。当从缓存中删除应用程序的对象时，可使用它来通知应用程序。</param>
		public void Add(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheDependency dependencies = null, CacheItemPriority priority = CacheItemPriority.Default, CacheItemRemovedCallback onRemoveCallback = null)
		{
			if (key == null) throw new ArgumentNullException("key");
			if (key.Length == 0) throw new ArgumentOutOfRangeException("key");

			if (value == null) return;

			HttpContext.Current.Cache.Insert(this._CheckCacheKey(key), value, dependencies, absoluteExpiration, dependencies == null ? slidingExpiration : TimeSpan.Zero, priority, onRemoveCallback);
		}

		#endregion

		#region 删除缓存

		/// <summary>
		/// 从缓存对象中移除指定项。
		/// </summary>
		/// <param name="key">要移除的缓存项的 <see cref="System.String"/> 标识符。</param>
		/// <returns>从缓存中移除的项。如果未找到键参数中的值，则返回 null。</returns>
		public object Remove(string key)
		{
			if (key == null) throw new ArgumentNullException("key");
			if (key.Length == 0) throw new ArgumentOutOfRangeException("key");

			return HttpContext.Current.Cache.Remove(this._CheckCacheKey(key));
		}

		/// <summary>
		/// 从缓存对象中移除所有包含指定键名的项。
		/// </summary>
		/// <param name="key">要移除的缓存项的 <see cref="System.String"/> 标识符。</param>
		public void RemoveMatch(string key)
		{
			if (key == null) throw new ArgumentNullException("key");
			if (key.Length == 0) throw new ArgumentOutOfRangeException("key");

			var keyNames = new List<string>();
			var e = HttpContext.Current.Cache.GetEnumerator();

			while (e.MoveNext())
			{
				var keyName = e.Key.ToString();
				var cacheName = this._CheckCacheKey(key);

				if (keyNames.Contains(keyName) == false && keyName.StartsWith(cacheName, StringComparison.CurrentCultureIgnoreCase)) keyNames.Add(keyName);
			}

			foreach (var keyName in keyNames) HttpContext.Current.Cache.Remove(keyName);
		}

		#endregion

		#region 获取缓存数目

		/// <summary>
		/// 根据键名前缀返回缓存项数。注意该方法返回的是仅仅以 <paramref name="prefix"/> 为前缀的缓存项目的总数。
		/// </summary>
		/// <param name="prefix">指定的缓存键名前缀。</param>
		/// <returns>以 <paramref name="prefix"/> 为前缀的缓存项目的总数。</returns>
		public int GetCountByPrefix(string prefix)
		{
			if (prefix == null) throw new ArgumentNullException("prefix");
			if (prefix.Length == 0) throw new ArgumentOutOfRangeException("prefix");

			var i = 0;
			var e = HttpContext.Current.Cache.GetEnumerator();

			while (e.MoveNext()) if (e.Key.ToString().StartsWith(prefix)) i++;

			return i;
		}

		#endregion

		#region 判断缓存是否已经存在

		/// <summary>
		/// 判断给定的键是否已经拥有缓存项。
		/// </summary>
		/// <param name="key">用于引用该项的缓存键。</param>
		/// <returns>返回 true 表示已经存在缓存项。</returns>
		public bool Contains(string key)
		{
			if (key == null) throw new ArgumentNullException("key");
			if (key.Length == 0) throw new ArgumentOutOfRangeException("key");

			return this.Get(key) != null;
		}

		#endregion

		#region 读取缓存

		/// <summary>
		/// 从缓存对象中检索指定项。
		/// </summary>
		/// <param name="key">要检索的缓存项的标识符。</param>
		/// <returns>检索到的缓存项，未找到该键时为 null。</returns>
		public object Get(string key)
		{
			if (key == null) throw new ArgumentNullException("key");
			if (key.Length == 0) throw new ArgumentOutOfRangeException("key");

			return HttpContext.Current.Cache.Get(this._CheckCacheKey(key));
		}

		#endregion

		#region 删除所有缓存

		/// <summary>
		/// 重置缓存管理器，这将清空所有已缓存的项。
		/// </summary>
		public void Reset()
		{
			var e = HttpContext.Current.Cache.GetEnumerator();

			while (e.MoveNext()) HttpContext.Current.Cache.Remove(this._CheckCacheKey((string)e.Key));
		}

		#endregion

		#endregion

		#region 公有属性

		/// <summary>
		/// 获取当前缓存项数。
		/// </summary>
		public int Count { get { return HttpContext.Current.Cache.Count; } }

		/// <summary>
		/// 获取或设置缓存键名前缀。
		/// </summary>
		public string Prefix { get { return String.IsNullOrEmpty(this._prefix) ? (this._prefix = _DEFAULT_PREFIX) : this._prefix; } set { this._prefix = value; } }

		/// <summary>
		/// 获取或设置指定键处的缓存项。
		/// </summary>
		/// <param name="key">表示缓存项的键的 <see cref="System.String"/> 对象。</param>
		/// <returns>指定的缓存项。</returns>
		public object this[string key]
		{
			get
			{
				if (key == null) throw new ArgumentNullException("key");
				if (key.Length == 0) throw new ArgumentOutOfRangeException("key");

				return HttpContext.Current.Cache.Get(this._CheckCacheKey(key));
			}
			set { HttpContext.Current.Cache.Insert(this._CheckCacheKey(key), value); }
		}

		/// <summary>
		/// 获取在 ASP.NET 开始从缓存中移除项之前应用程序可使用的物理内存百分比。
		/// </summary>
		public long EffectivePercentagePhysicalMemoryLimit { get { return HttpContext.Current.Cache.EffectivePercentagePhysicalMemoryLimit; } }

		/// <summary>
		/// 获取可用于缓存的字节数。
		/// </summary>
		public long EffectivePrivateBytesLimit { get { return HttpContext.Current.Cache.EffectivePrivateBytesLimit; } }

		#endregion
	}
}