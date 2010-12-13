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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Net.Htmlbird.Framework.Extensions.Int64_;
using Net.Htmlbird.Framework.Modules;
using Net.Htmlbird.Framework.Security.Cryptography;
using Net.Htmlbird.Framework.Utilities;
using Net.Htmlbird.Framework.Web.Modules;
using Net.Htmlbird.TemplateEngine;
using Net.Htmlbird.TemplateEngine.Tags;

namespace Net.Htmlbird.Framework.Web.WebUI.AdminTools
{
	public class TemplateManager : _Page<TemplateManagerPageArgs>
	{
		private readonly Dictionary<string, string> _fileContents = new Dictionary<string, string>();
		private readonly Dictionary<string, Dictionary<FileInfo, PackingTag>> _javascriptFiles = new Dictionary<string, Dictionary<FileInfo, PackingTag>>();
		private readonly Dictionary<string, Dictionary<FileInfo, PackingTag>> _styleSheetFiles = new Dictionary<string, Dictionary<FileInfo, PackingTag>>();

		public List<FileInfo> TemplateFiles;

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			this.TemplateFiles = this._GetTemplateFiles();

			if (this.Arguments.Action != "rebuild") return;

			this._Process();

			this.Response.End();
			this.Response.Close();
		}

		private void _Process()
		{
			var watch = Stopwatch.StartNew();

			var encoding = Encoding.UTF8;
			const string tagPrefix = "mte";

			var languageEngine = new MultiLanguageEngine(HtmlbirdECMS.SystemInfo.PhysicalCurrentLanguagesSetupPath, "zh-CHS");
			var application = ApplicationInfo.YMind;
			var files = this.Arguments.TemplateFiles;

			if (files.Count == 0) return;

			var applicationPath = this.Request.PhysicalApplicationPath;

			if (String.IsNullOrEmpty(applicationPath)) return;

			int index = 0, count = files.Count;

			foreach (var file in files)
			{
				index++;

				try
				{
					var templateEngine = new AspxTemplateEngine(file, applicationPath, encoding, tagPrefix, true, application) {
						LanguageEngine = languageEngine
					};
					templateEngine.CreateStyleSheetFile += this._TemplateEngineCreateStyleSheetFile;
					templateEngine.CreateJavascriptFile += this._TemplateEngineCreateJavascriptFile;
					var aspxFile = PathUtils.CreateDirectories(file.Replace(".shtml", ".aspx").Replace(HtmlbirdECMS.SystemInfo.PhysicalTemplatesSetupPath, HtmlbirdECMS.SystemInfo.PhysicalAspxFilesSetupPath));

					File.WriteAllText(aspxFile, templateEngine.ToAspx().ToString());

					this.Response.Write(String.Format(@"<script type=""text/javascript"">parent.setProgressBar(""{0:P2}"", ""~/{1}"");</script>{2}", (double)index / count, this.Server.HtmlEncode(file.Replace(applicationPath, String.Empty).Replace('\\', '/')), Environment.NewLine));
					this.Response.Flush();
				}
				catch (ParseException e)
				{
					this.Response.Write(String.Format(@"<script type=""text/javascript"">alert('{0}');</script>{1}", String.Format(@"生成模板时遇到错误，详细信息如下：\r\n\r\n{0}\r\n\r\n文件：{1}", e.Message, this.Server.HtmlEncode(String.Format("~/{0}", file.Replace(applicationPath, String.Empty).Replace('\\', '/')))), Environment.NewLine));
					this.Response.End();
				}
			}

			// 对打包文件进行后期处理
			this._SaveJavascriptFiles();
			this._SaveStyleSheetFiles();

			watch.Stop();

			this.Response.Write(String.Format(@"<script type=""text/javascript"">parent.setProgressBar(""100.00%"", ""模板重建完毕！耗时：{0}"");</script>\r\n", watch.Elapsed));
		}

		#region 私有方法

		private void _TemplateEngineCreateJavascriptFile(object sender, CreateFileNameEventArgs e)
		{
			if (String.IsNullOrEmpty(this.Request.PhysicalApplicationPath)) return;

			// 取文件列表字符串的 MD5 哈希值作为文件名的唯一标识符
			var hashCode = CryptoService.MD5(String.Join(", ", e.Tags.Select(item => item.Key.FullName).ToArray()));

			// 取得模板名称，分文件夹保存
			var templateName = e.TemplateEngine.FileName.Replace(HtmlbirdECMS.SystemInfo.PhysicalTemplatesSetupPath, String.Empty).TrimStart('\\');
			templateName = templateName.Substring(0, templateName.IndexOf('\\'));
			templateName = String.Format(Path.Combine(HtmlbirdECMS.SystemInfo.PhysicalJScriptFilesSetupPath, templateName));

			// 构建保存路径
			var scriptFileName = PathUtils.CreateDirectories(String.Format("{0}\\{1}.js", templateName, hashCode));

			// 返回代码引用路径给模板引擎，模板引擎将会把这个路径作为 script 标签的 src 属性的值。
			e.FileName = scriptFileName.Replace(this.Request.PhysicalApplicationPath, String.Empty).Replace('\\', '/').Insert(0, "/");

			// 保存到缓存字典，避免对重名文件重复读写造成的性能损失
			if (this._javascriptFiles.ContainsKey(scriptFileName) == false) this._javascriptFiles.Add(scriptFileName, e.Tags);
		}

		private void _TemplateEngineCreateStyleSheetFile(object sender, CreateFileNameEventArgs e)
		{
			if (String.IsNullOrEmpty(this.Request.PhysicalApplicationPath)) return;

			// 取文件列表字符串的 MD5 哈希值作为文件名的唯一标识符
			var hashCode = CryptoService.MD5(String.Join(", ", e.Tags.Select(item => item.Key.FullName).ToArray()));

			// 取得模板名称，分文件夹保存
			var templateName = e.TemplateEngine.FileName.Replace(HtmlbirdECMS.SystemInfo.PhysicalTemplatesSetupPath, String.Empty).TrimStart('\\');
			templateName = templateName.Substring(0, templateName.IndexOf('\\'));
			templateName = String.Format(Path.Combine(HtmlbirdECMS.SystemInfo.PhysicalStyleFilesSetupPath, templateName));

			// 构建保存路径
			var styleFileName = PathUtils.CreateDirectories(String.Format("{0}\\{1}.css", templateName, hashCode));

			// 返回代码引用路径给模板引擎，模板引擎将会把这个路径作为 link 标签的 href 属性的值。
			e.FileName = styleFileName.Replace(this.Request.PhysicalApplicationPath, String.Empty).Replace('\\', '/').Insert(0, "/");

			// 保存到缓存字典，避免对重名文件重复读写造成的性能损失
			if (this._styleSheetFiles.ContainsKey(styleFileName) == false) this._styleSheetFiles.Add(styleFileName, e.Tags);
		}

		private void _SaveJavascriptFiles()
		{
			var applicationPath = this.Request.PhysicalApplicationPath;
			var index = 0;
			var count = this._javascriptFiles.Count;

			if (String.IsNullOrEmpty(applicationPath)) return;

			foreach (var item in this._javascriptFiles)
			{
				index++;

				var fileContents = JavaScriptMinifier.Minify(this._GetJavascriptFileContents(item.Value));

				this.Response.Write(String.Format(@"<script type=""text/javascript"">parent.setProgressBar(""{0:P2}"", ""正在打包 Javascript 文件({2})：~/{1}"");</script>{3}", (double)index / count, this.Server.HtmlEncode(item.Key.Replace(applicationPath, String.Empty).Replace('\\', '/')), ((long)fileContents.Length).ToFileSize(), Environment.NewLine));
				this.Response.Flush();

				File.WriteAllText(item.Key, fileContents.ToString().Trim(), Encoding.UTF8);
			}

			this.Response.Write(@"<script type=""text/javascript"">parent.setProgressBar(""100.00%"", ""Javascript 文件打包完毕！"");</script>\r\n");
			this.Response.Flush();
		}

		private void _SaveStyleSheetFiles()
		{
			var applicationPath = this.Request.PhysicalApplicationPath;
			var index = 0;
			var count = this._styleSheetFiles.Count;

			if (String.IsNullOrEmpty(applicationPath)) return;

			foreach (var item in this._styleSheetFiles)
			{
				index++;

				var fileContents = CssMinifier.Minify(this._GetStyleSheetFileContents(item.Value));

				this.Response.Write(String.Format(@"<script type=""text/javascript"">parent.setProgressBar(""{0:P2}"", ""正在打包 StyleSheet 文件({2})：~/{1}"");</script>{3}", (double)index / count, this.Server.HtmlEncode(item.Key.Replace(applicationPath, String.Empty).Replace('\\', '/')), ((long)fileContents.Length).ToFileSize(), Environment.NewLine));
				this.Response.Flush();

				File.WriteAllText(item.Key, fileContents.ToString().Trim(), Encoding.UTF8);
			}

			this.Response.Write(@"<script type=""text/javascript"">parent.setProgressBar(""100.00%"", ""StyleSheet 文件打包完毕！"");</script>\r\n");
			this.Response.Flush();
		}

		private StringBuilder _GetJavascriptFileContents(Dictionary<FileInfo, PackingTag> scripts)
		{
			var codeDocument = new StringBuilder();

			foreach (var pair in scripts)
			{
				using (var reader = pair.Key.OpenText())
				{
					var code = this._fileContents.ContainsKey(pair.Key.FullName) ? this._fileContents[pair.Key.FullName] : reader.ReadToEnd().Trim();

					if (this._fileContents.ContainsKey(pair.Key.FullName) == false) this._fileContents.Add(pair.Key.FullName, code);

					if (String.IsNullOrEmpty(code)) continue;

					if (codeDocument.Length > 0) codeDocument.AppendLine();

					codeDocument.AppendLine(code);
					codeDocument.AppendLine();

					reader.Close();
				}
			}

			return codeDocument;
		}

		private StringBuilder _GetStyleSheetFileContents(Dictionary<FileInfo, PackingTag> styles)
		{
			var codeDocument = new StringBuilder();

			foreach (var pair in styles)
			{
				using (var reader = pair.Key.OpenText())
				{
					var code = this._fileContents.ContainsKey(pair.Key.FullName) ? this._fileContents[pair.Key.FullName] : reader.ReadToEnd().Trim();

					if (this._fileContents.ContainsKey(pair.Key.FullName) == false) this._fileContents.Add(pair.Key.FullName, code);

					if (String.IsNullOrEmpty(code)) continue;

					if (codeDocument.Length > 0)
					{
						var charsetIndex = code.IndexOf("@charset");

						if (charsetIndex >= 0) code = code.Remove(charsetIndex, code.IndexOf(";", charsetIndex + 8) + 1);

						codeDocument.AppendLine();
					}

					codeDocument.AppendLine(code);
					codeDocument.AppendLine();

					reader.Close();
				}
			}

			return codeDocument;
		}

		private List<FileInfo> _GetTemplateFiles()
		{
			var list = new List<FileInfo>();

			if (this.Arguments.TemplateId == 0) return list;

			var template = Website.Current.Templates[this.Arguments.TemplateId];
			var path = PathUtils.MapPath(PathUtils.Combine(HtmlbirdECMS.SystemInfo.TemplatesSetupPath, template.SetupPath));

			var di = new DirectoryInfo(path);

			if (di.Exists == false) return list;

			var files = di.GetFiles("*.shtml", SearchOption.AllDirectories);

			list.AddRange(files.Where(file => file.Name.StartsWith("_") == false));

			return list.Count > 0 ? list.OrderBy(item => item.DirectoryName).ThenBy(item => item.FullName).ToList() : list;
		}

		#endregion
	}
}