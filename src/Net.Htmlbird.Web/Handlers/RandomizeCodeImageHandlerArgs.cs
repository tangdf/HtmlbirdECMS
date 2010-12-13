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

namespace Net.Htmlbird.Framework.Web.Handlers
{
	/// <summary>
	/// 表示 <see cref="RandomizeCodeImageHandler"/> 的页面参数。
	/// </summary>
	[Serializable]
	public sealed class RandomizeCodeImageHandlerArgs : HttpHandlerArgs
	{
		/// <summary>
		/// 获取验证码图像中包含的用于验证的字符串。
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// 获取验证码宽度。
		/// </summary>
		public int Width
		{
			get
			{
				int width = GetQuery("w", 180);

				if (width > 600) width = 600;

				return width;
			}
		}

		/// <summary>
		/// 获取验证码高度。
		/// </summary>
		public int Height
		{
			get
			{
				int height = GetQuery("h", 60);

				if (height > 420) height = 420;
				return height;
			}
		}

		/// <summary>
		/// 获取验证码图像中包含的用于验证的字符串的长度。
		/// </summary>
		public int CharCount { get { return GetQuery("c", 5); } }

		/// <summary>
		/// 获取验证码图像的边框大小。
		/// </summary>
		public int BorderWidth { get { return GetQuery("b", 1); } }

		/// <summary>
		/// 获取验证码图像的干扰线数量。
		/// </summary>
		public int StrukLineCount { get { return GetQuery("s", 2); } }

		/// <summary>
		/// 获取验证码图像的干扰色块数量。
		/// </summary>
		public int PatternCount { get { return GetQuery("p", 1); } }

		/// <summary>
		/// 获取验证码图像生成噪点的几率。
		/// </summary>
		public double RandomNoise { get { return GetQuery("r", 0.005); } }
	}
}