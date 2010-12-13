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
using Net.Htmlbird.Framework.Web.Modules;

namespace Net.Htmlbird.Framework.Web.Handlers
{
	/// <summary>
	/// 提供生成验证码图像的支持。
	/// </summary>
	public sealed class RandomizeCodeImageHandler : SessionHttpHandlerBase<RandomizeCodeImageHandlerArgs>
	{
		/// <summary>
		/// 获取一个值，该值指示其他请求是否可以使用 <see cref="IHttpHandler"/> 实例。
		/// </summary>
		public override bool IsReusable { get { return true; } }

		/// <summary>
		/// 通过实现 <see cref="IHttpHandler"/> 接口的自定义 <see cref="IHttpHandler"/> 启用 HTTP Web 请求的处理。
		/// </summary>
		/// <param name="context"><see cref="HttpContext"/> 对象，它提供对用于为 HTTP 请求提供服务的内部服务器对象（如 Request、Response、Session 和 Server）的引用。</param>
		public override void ProcessRequest(HttpContext context)
		{
			// ^vCode\.ashx(\?o=(\d+)-(\d+)-(\d+)-(\d+)-(\d+)-(\d+)-([\d\.]+)(.*?))?$
			// /vcode.ashx?o=180-60-5-1-2-1-0.005&t=0.182843845349538
			// /vCode.ashx?w=$2&h=$3&c=$4&b=$5&s=$6&p=$7&r=$8$9
			// /vCode.ashx?w=180&h=60&c=5&b=1&s=2&p=1&r=0.005
			ImageBorder border = new ImageBorder(this.Arguments.BorderWidth) {
				UseRandomBrush = true
			};

			using (RandomizeCodeImage rImage = RandomizeCodeImage.Create(this.Arguments.CharCount, this.Arguments.Width, this.Arguments.Height, border, this.Arguments.StrukLineCount, this.Arguments.PatternCount, this.Arguments.RandomNoise))
			{
				// 保存验证码
				this.Arguments.Text = rImage.Text;
				this.Context.Session["Form-ValidationCode"] = this.Arguments.Text;

				// 输出验证码图像
				this.Context.Response.ClearContent();
				this.Context.Response.Expires = -1;
				this.Context.Response.ExpiresAbsolute = DateTime.Now.AddHours(-1);
				this.Context.Response.ContentType = rImage.ContentType;
				this.Context.Response.BinaryWrite(rImage.ImageStream.ToArray());
			}
		}
	}
}