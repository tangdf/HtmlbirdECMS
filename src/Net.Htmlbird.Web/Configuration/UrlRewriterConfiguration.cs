// ===============================================================================
//  产品名称：网鸟电子商务管理系统(Htmlbird ECMS)
//  产品作者：YMind Chan
//  版权所有：网鸟IT技术论坛 颜铭工作室
// ===============================================================================
//  Copyright © Htmlbird.Net. All rights reserved .
//  官方网站：http://www.htmlbird.net/
//  技术论坛：http://bbs.htmlbird.net/
// ===============================================================================
using System.IO;
using System.Net;
using Net.Htmlbird.UrlRewriter.Configuration;

namespace Net.Htmlbird.Framework.Web.Configuration
{
	/// <summary>
	/// 为网鸟门户网站管理系统提供地址重写配置信息。
	/// </summary>
	public sealed class UrlRewriterConfiguration : RewriterConfigurationProvider
	{
		/// <summary>
		/// 初始化 <see cref="UrlRewriterConfiguration"/> 对象。
		/// </summary>
		private UrlRewriterConfiguration() { }

		/// <summary>
		/// 加载地址重写配置。
		/// </summary>
		/// <returns>返回 <see cref="RewriterConfiguration"/> 对象。</returns>
		public override RewriterConfiguration Load()
		{
			var fileName = HtmlbirdECMS.SystemInfo.PhysicalCurrentRewriteRuleSetupPath;

			if (string.IsNullOrWhiteSpace(fileName)) return null;

			var file = new FileInfo(fileName);

			if (file.Exists == false) throw new FileNotFoundException();

			var config = this.LoadFromFile(file.FullName);

			if (config == null) throw new HtmlbirdECMSException(HttpStatusCode.InternalServerError, "未找到地址重写规则库。");

			return config;
		}

		/// <summary>
		/// 创建 <see cref="UrlRewriterConfiguration"/> 类的新实例。
		/// </summary>
		/// <returns><see cref="UrlRewriterConfiguration"/> 对象的新实例。</returns>
		public static UrlRewriterConfiguration Create() { return new UrlRewriterConfiguration(); }
	}
}