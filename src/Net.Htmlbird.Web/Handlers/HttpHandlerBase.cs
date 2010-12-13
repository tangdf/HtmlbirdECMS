// ===============================================================================
//  产品名称：网鸟电子商务管理系统(Htmlbird ECMS)
//  产品作者：YMind Chan
//  版权所有：网鸟IT技术论坛 颜铭工作室
// ===============================================================================
//  Copyright © Htmlbird.Net. All rights reserved .
//  官方网站：http://www.htmlbird.net/
//  技术论坛：http://bbs.htmlbird.net/
// ===============================================================================
using System.Web;

namespace Net.Htmlbird.Framework.Web.Handlers
{
	/// <summary>
	/// 为自定义 HTTP 处理程序提供基类。
	/// </summary>
	public abstract class HttpHandlerBase : IHttpHandler
	{
		#region IHttpHandler 成员

		/// <summary>
		/// 获取一个值，该值指示其他请求是否可以使用 <see cref="IHttpHandler"/> 实例。
		/// </summary>
		public virtual bool IsReusable { get { return false; } }

		/// <summary>
		/// 通过实现 <see cref="IHttpHandler"/> 接口的自定义 <see cref="IHttpHandler"/> 启用 HTTP Web 请求的处理。
		/// </summary>
		/// <param name="context"></param>
		public virtual void ProcessRequest(HttpContext context)
		{
			this.Context = context;

			this.OnInit();
		}

		#endregion

		/// <summary>
		/// 获取与该页关联的 <see cref="HttpContext"/> 对象。
		/// </summary>
		public HttpContext Context { get; private set; }

		/// <summary>
		/// 为当前 HTTP 请求获取 <see cref="HttpRequest"/> 对象。 
		/// </summary>
		public HttpRequest Request { get { return this.Context.Request; } }

		/// <summary>
		/// 为当前 HTTP 响应获取 <see cref="HttpResponse"/> 对象。
		/// </summary>
		public HttpResponse Response { get { return this.Context.Response; } }

		/// <summary>
		/// 对处理程序进行初始化。
		/// </summary>
		protected virtual void OnInit() { }
	}

	/// <summary>
	/// 为自定义 HTTP 处理程序提供基类。
	/// </summary>
	public abstract class HttpHandlerBase<T> : IHttpHandler where T : HttpHandlerArgs, new()
	{
		#region IHttpHandler 成员

		/// <summary>
		/// 获取一个值，该值指示其他请求是否可以使用 <see cref="IHttpHandler"/> 实例。
		/// </summary>
		public virtual bool IsReusable { get { return false; } }

		/// <summary>
		/// 通过实现 <see cref="IHttpHandler"/> 接口的自定义 <see cref="IHttpHandler"/> 启用 HTTP Web 请求的处理。
		/// </summary>
		/// <param name="context"></param>
		public virtual void ProcessRequest(HttpContext context)
		{
			this.Context = context;

			this.OnInit();
		}

		#endregion

		/// <summary>
		/// 获取与该页关联的 <see cref="HttpContext"/> 对象。
		/// </summary>
		public HttpContext Context { get; private set; }

		/// <summary>
		/// 为当前 HTTP 请求获取 <see cref="HttpRequest"/> 对象。 
		/// </summary>
		public HttpRequest Request { get { return this.Context.Request; } }

		/// <summary>
		/// 为当前 HTTP 响应获取 <see cref="HttpResponse"/> 对象。
		/// </summary>
		public HttpResponse Response { get { return this.Context.Response; } }

		/// <summary>
		/// 获取与该页面关联的包含用户参数集合的对象。
		/// </summary>
		public virtual T Arguments { get; protected set; }

		/// <summary>
		/// 对处理程序进行初始化。
		/// </summary>
		protected virtual void OnInit() { this.Arguments = new T(); }
	}
}