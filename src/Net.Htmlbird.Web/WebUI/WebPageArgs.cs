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
using Net.Htmlbird.Framework.Web.Handlers;

namespace Net.Htmlbird.Framework.Web.WebUI
{
	/// <summary>
	/// 表示 <see cref="_Page"/> 的页面参数。
	/// </summary>
	[Serializable]
	public class WebPageArgs : HttpHandlerArgs
	{
		/// <summary>
		/// 获取验证码图像的地址，用于显示。
		/// </summary>
		public string RandomizeCodeImageUrl { get { return "/vCode.ashx"; } }

		/// <summary>
		/// 获取验证码图像的标签，用于嵌入到 HTML 文档中。
		/// </summary>
		public string RandomizeCodeImageTag { get { return String.Format("<img id=\"ValidationCodeImage\" alt=\"验证码图片\" src=\"{0}\" />", this.RandomizeCodeImageUrl); } }

		/// <summary>
		/// 获取从网页表单提交的验证码字符串。
		/// </summary>
		public string ValidationCode { get { return GetForm("ValidationCode", String.Empty, true); } }

		/// <summary>
		/// 获取从 Url 查询字符串中提交的值，该值指示用户代理是否正以 Ajax 的形式请求页面。
		/// </summary>
		public bool IsAjax
		{
			get
			{
				var x = Request.Headers["X-Requested-With"];

				if (String.IsNullOrEmpty(x)) x = Request.ServerVariables["X-Requested-With"];
				if (String.IsNullOrEmpty(x)) x = String.Empty;

				return GetQuery("ajax", false) || (x == "XMLHttpRequest");
			}
		}

		/// <summary>
		/// 获取从网页表单提交的操作标识符，用来判断页面接下来要执行什么任务。
		/// </summary>
		public string Action { get { return GetQuery("act", String.Empty, true); } }
	}
}