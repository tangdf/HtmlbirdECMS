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
using System.Net;
using System.Web;

namespace Net.Htmlbird.Framework.Web
{
	/// <summary>
	/// 表示 HtmlbirdECMS 运行期间发生的异常。
	/// </summary>
	[Serializable]
	public class HtmlbirdECMSException : HttpException
	{
		/// <summary>
		/// 初始化 <see cref="HtmlbirdECMSException"/> 类的新实例。
		/// </summary>
		/// <param name="statusCode">HTTP 状态码。</param>
		/// <param name="message">异常消息。</param>
		public HtmlbirdECMSException(HttpStatusCode statusCode, string message) : base((int)statusCode, message) { }
	}
}