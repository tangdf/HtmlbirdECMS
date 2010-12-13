// ===============================================================================
//  产品名称：网鸟电子商务管理系统(Htmlbird ECMS)
//  产品作者：YMind Chan
//  版权所有：网鸟IT技术论坛 颜铭工作室
// ===============================================================================
//  Copyright © Htmlbird.Net. All rights reserved .
//  官方网站：http://www.htmlbird.net/
//  技术论坛：http://bbs.htmlbird.net/
// ===============================================================================
using Net.Htmlbird.TemplateEngine;

namespace Net.Htmlbird.Framework.Web
{
	/// <summary>
	/// 表示用于网鸟网站内容管理系统的多语言引擎的实现。
	/// </summary>
	public class MultiLanguageEngine : MultiLanguageEngineBase
	{
		/// <summary>
		/// 从指定路径初始化多语言引擎。
		/// </summary>
		/// <param name="languagePath">指定语言包所在的完整路径。</param>
		/// <param name="languageCode">指定当前正在使用的语言代码。</param>
		public MultiLanguageEngine(string languagePath, string languageCode) : base(languagePath, languageCode) { }
	}
}