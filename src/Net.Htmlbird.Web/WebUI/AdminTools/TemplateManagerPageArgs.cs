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
using System.IO;
using System.Linq;
using Net.Htmlbird.Framework.Utilities;

namespace Net.Htmlbird.Framework.Web.WebUI.AdminTools
{
	/// <summary>
	/// 表示 <see cref="TemplateManager"/> 的页面参数。
	/// </summary>
	[Serializable]
	public sealed class TemplateManagerPageArgs : _PageArgs
	{
		/// <summary>
		/// 获取当前处理的模板的编号。
		/// </summary>
		public int TemplateId { get { return GetQuery("template", 0); } }

		/// <summary>
		/// 获取当前处理的模板文档的集合。
		/// </summary>
		public HashSet<string> TemplateFiles
		{
			get
			{
				var files = GetForm("files", String.Empty).Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

				return files.Length == 0 ? new HashSet<string>() : new HashSet<string>(files.Where(fileName => !String.IsNullOrEmpty(fileName)).Select(fileName => PathUtils.MapPath(PathUtils.Combine(HtmlbirdECMS.SystemInfo.TemplatesSetupPath, fileName))).Where(File.Exists));
			}
		}
	}
}