// ===============================================================================
//  产品名称：网鸟电子商务管理系统(Htmlbird ECMS)
//  产品作者：YMind Chan
//  版权所有：网鸟IT技术论坛 颜铭工作室
// ===============================================================================
//  Copyright © Htmlbird.Net. All rights reserved .
//  官方网站：http://www.htmlbird.net/
//  技术论坛：http://bbs.htmlbird.net/
// ===============================================================================
using System.Web.SessionState;

namespace Net.Htmlbird.Framework.Web.Handlers
{
	/// <summary>
	/// 为自定义 HTTP 处理程序提供基类。
	/// </summary>
	public abstract class SessionHttpHandlerBase : HttpHandlerBase, IRequiresSessionState
	{
		/// <summary>
		/// 获取一个值，该值指示当前的 HTTP Handler 是否可以读写会话状态值。
		/// </summary>
		public bool CanReadWriteSessionState { get { return this.Context.Handler is IRequiresSessionState; } }

		/// <summary>
		/// 为当前 HTTP 请求获取 <see cref="HttpSessionState"/> 对象。 
		/// </summary>
		public HttpSessionState Session { get { return this.Context.Session; } }
	}

	/// <summary>
	/// 为自定义 HTTP 处理程序提供基类。
	/// </summary>
	public abstract class SessionHttpHandlerBase<T> : HttpHandlerBase<T>, IRequiresSessionState where T : HttpHandlerArgs, new()
	{
		/// <summary>
		/// 获取一个值，该值指示当前的 HTTP Handler 是否可以读写会话状态值。
		/// </summary>
		public bool CanReadWriteSessionState { get { return this.Context.Handler is IRequiresSessionState; } }

		/// <summary>
		/// 为当前 HTTP 请求获取 <see cref="HttpSessionState"/> 对象。 
		/// </summary>
		public HttpSessionState Session { get { return this.Context.Session; } }
	}
}