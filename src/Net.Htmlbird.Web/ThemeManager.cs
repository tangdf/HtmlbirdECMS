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
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Net.Htmlbird.Framework.Web.Entities;

namespace Net.Htmlbird.Framework.Web
{
	/// <summary>
	/// 主题管理器。提供网站主题模板的切换、重建、导入和导出等功能。
	/// </summary>
	public sealed class ThemeManager
	{
		public ThemeManager()
		{
			this.Templates = new WebsiteTemplateInfoCollection();
			this._Load();
		}

		public WebsiteTemplateInfoCollection Templates { get; private set; }

		private void _Load()
		{
			var templatePath = new DirectoryInfo(HtmlbirdECMS.SystemInfo.PhysicalTemplatesSetupPath);

			if (templatePath.Exists == false) throw new DirectoryNotFoundException();

			var configFiles = templatePath.EnumerateFiles("*.xml", SearchOption.TopDirectoryOnly);

			if (configFiles == null || configFiles.Count() == 0) throw new FileNotFoundException("在模板安装目录下未找到任何模板配置文档。");

			foreach (var configFile in configFiles)
			{
				if (configFile == null || configFile.Exists == false) continue;

				using (var fileStream = configFile.OpenRead())
				{
					var document = XDocument.Load(fileStream);

					if (document == null) continue;

					var root = document.Element("Templates");

					if (root == null) continue;

					var templateElements = root.Elements("Template");

					this.Templates.AddRange(templateElements.Select(_CreateTemplate).Where(template => template != null));

					fileStream.Close();
				}
			}
		}

		private static WebsiteTemplateInfo _CreateTemplate(XElement templateElement)
		{
			if (templateElement == null || templateElement.HasElements == false) return null;

			var id = Convert.ToInt32(_TryGetValue(templateElement, "Id", "0"));
			var displayId = Convert.ToInt32(_TryGetValue(templateElement, "DisplayId", "0"));
			var websiteId = Convert.ToInt32(_TryGetValue(templateElement, "WebsiteId", "0"));
			var isDefault = Convert.ToBoolean(_TryGetValue(templateElement, "IsDefault", "false"));
			var enabled = Convert.ToBoolean(_TryGetValue(templateElement, "Enabled", "false"));
			var name = _TryGetValue(templateElement, "Name", "0");
			var description = _TryGetValue(templateElement, "Description", "0");
			var alias = _TryGetValue(templateElement, "Alias", "0");
			var setupPath = _TryGetValue(templateElement, "SetupPath", "0");
			var rewriteRules = _TryGetValue(templateElement, "RewriteRules", "0");
			var buildDate = Convert.ToDateTime(_TryGetValue(templateElement, "BuildDate", DateTime.Now.ToLongDateString()));
			var author = _TryGetValue(templateElement, "Author", "0");
			var eMail = _TryGetValue(templateElement, "EMail", "0");
			var homePage = _TryGetValue(templateElement, "HomePage", "0");

			var styles = new WebsiteStyleInfoCollection();
			var styleElements = templateElement.Elements("Styles").Where(el => el != null && el.HasElements).Elements("Style");

			if (styleElements == null) return null;

			styles.AddRange(styleElements.Select(_CreateStyle).Where(style => style != null));

			if (styles.Count == 0) return null;

			return new WebsiteTemplateInfo(id, displayId, websiteId) {
				Alias = alias,
				Author = author,
				BuildDate = buildDate,
				CreateDate = DateTime.Now,
				CreateUserId = 0,
				Description = description,
				EMail = eMail,
				Enabled = enabled,
				HomePage = homePage,
				IsDefault = isDefault,
				Name = name,
				RewriteRules = rewriteRules,
				SetupPath = setupPath,
				Styles = styles,
				UpdateDate = DateTime.Now,
				UpdateUserId = 0,
			};
		}

		private static WebsiteStyleInfo _CreateStyle(XElement styleElement)
		{
			if (styleElement == null || styleElement.HasElements == false) return null;

			var id = Convert.ToInt32(_TryGetValue(styleElement, "Id", "0"));
			var displayId = Convert.ToInt32(_TryGetValue(styleElement, "DisplayId", "0"));
			var templateId = Convert.ToInt32(_TryGetValue(styleElement, "TemplateId", "0"));
			var isDefault = Convert.ToBoolean(_TryGetValue(styleElement, "IsDefault", "false"));
			var enabled = Convert.ToBoolean(_TryGetValue(styleElement, "Enabled", "false"));
			var name = _TryGetValue(styleElement, "Name", "0");
			var description = _TryGetValue(styleElement, "Description", "0");
			var alias = _TryGetValue(styleElement, "Alias", "0");
			var setupPath = _TryGetValue(styleElement, "SetupPath", "0");
			var buildDate = Convert.ToDateTime(_TryGetValue(styleElement, "BuildDate", DateTime.Now.ToLongDateString()));
			var author = _TryGetValue(styleElement, "Author", "0");
			var eMail = _TryGetValue(styleElement, "EMail", "0");
			var homePage = _TryGetValue(styleElement, "HomePage", "0");

			return new WebsiteStyleInfo(id, displayId, templateId) {
				Alias = alias,
				Author = author,
				BuildDate = buildDate,
				CreateDate = DateTime.Now,
				CreateUserId = 0,
				Description = description,
				EMail = eMail,
				Enabled = enabled,
				HomePage = homePage,
				IsDefault = isDefault,
				Name = name,
				SetupPath = setupPath,
				UpdateDate = DateTime.Now,
				UpdateUserId = 0,
			};
		}

		private static string _TryGetValue(XElement element, string nodeName, string defaultValue)
		{
			if (element == null || element.HasElements == false) return defaultValue ?? String.Empty;

			var el = element.Element(nodeName);

			if (el == null) return defaultValue ?? String.Empty;

			var value = String.IsNullOrWhiteSpace(el.Value) ? defaultValue : el.Value;

			return value ?? String.Empty;
		}

		public void Refresh() { this._Load(); }
	}

	/*
		<?xml version="1.0" encoding="utf-8"?>
		<Templates>
			<Template>
				<WebsiteId>0</WebsiteId>
				<IsDefault>true</IsDefault>
				<Enabled>true</Enabled>
				<Name>默认模板</Name>
				<Description>网鸟门户网站管理系统默认模板</Description>
				<Alias>Default</Alias>
				<SetupPath>Default/</SetupPath>
				<RewriteRules></RewriteRules>
				<BuildDate>2010-05-21 16:00:00</BuildDate>
				<Author>YMind Chan</Author>
				<EMail>ymind@htmlbird.net</EMail>
				<HomePage>http://www.htmlbird.net/</HomePage>
				<Styles>
					<Style>
						<TemplateId>0</TemplateId>
						<IsDefault>true</IsDefault>
						<Enabled>true</Enabled>
						<Name>默认样式</Name>
						<Description>默认样式</Description>
						<Alias>Default</Alias>
						<SetupPath>Default/Styles/Default/</SetupPath>
						<BuildDate>2010-05-21 16:00:00</BuildDate>
						<Author>YMind Chan</Author>
						<EMail>ymind@htmlbird.net</EMail>
						<HomePage>http://www.htmlbird.net/</HomePage>
						<Id>0</Id>
						<DisplayId>0</DisplayId>
					</Style>
				</Styles>
				<Id>0</Id>
				<DisplayId>0</DisplayId>
			</Template>
		</Templates>
	*/
}