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

namespace Net.Htmlbird.Framework.Web
{
	/// <summary>
	/// 表示浏览器标题信息。
	/// </summary>
	[Serializable]
	public sealed class PageTitleInfo
	{
		#region 私有字段

		private string _text;

		private string _titleOnly;
		private string _titleWithNav;
		private string _titleWithNavAction;
		private string _titleWithNavActionAndTags;
		private string _titleWithNavText;
		private string _titleWithSEO;
		private string _titleWithTags;

		#endregion

		#region 公有方法

		/// <summary>
		/// 清理标题信息。
		/// </summary>
		public void Clear()
		{
			this._text = String.Empty;
			this.NavAction = String.Empty;
			this.NavText = String.Empty;

			this._titleOnly = String.Empty;
			this._titleWithNav = String.Empty;
			this._titleWithNavAction = String.Empty;
			this._titleWithNavText = String.Empty;
			this._titleWithTags = String.Empty;
		}

		/// <summary>
		/// 返回当前网站的浏览器标题信息。
		/// </summary>
		/// <returns>当前网站的浏览器标题信息。</returns>
		public override string ToString() { return this.Text; }

		#endregion

		#region 公有属性

		/// <summary>
		/// 获取或设置自定义标题信息。
		/// </summary>
		public string CustomText { get; set; }

		/// <summary>
		/// 获取当前网站的浏览器标题信息。
		/// </summary>
		public string Text
		{
			get
			{
				// NavAction - WebsiteTitle - NavText - TagListForTitle
				if (String.IsNullOrEmpty(this._text))
				{
					var f = "{0} - {1}{2} - {3}";

					if (String.IsNullOrEmpty(this.NavAction)) f = f.Replace("{0} - ", String.Empty);
					if (String.IsNullOrEmpty(this.WebsiteTitle)) this.WebsiteTitle = "欢迎光临我们的网站！";
					if (String.IsNullOrEmpty(this.NavText)) f = f.Replace("{2}", String.Empty);
					if (String.IsNullOrEmpty(this.Tags)) f = f.Replace(" {3}", String.Empty);

					this._text = String.Format(f, this.NavAction, this.WebsiteTitle, this.NavText, this.Tags);
				}

				return this._text;
			}
		}

		/// <summary>
		/// 获取标题。
		/// </summary>
		public string TitleOnly
		{
			get
			{
				// WebsiteTitle
				this._titleOnly = this.WebsiteTitle;

				return this._titleOnly;
			}
		}

		/// <summary>
		/// 获取标题、页面动作和页面导航。
		/// </summary>
		public string TitleWithNav
		{
			get
			{
				// NavAction - WebsiteTitle - NavText
				if (String.IsNullOrEmpty(this._titleWithNav))
				{
					var f = "{0} - {1}{2}";

					if (String.IsNullOrEmpty(this.NavAction)) f = f.Replace("{0} - ", String.Empty);
					if (String.IsNullOrEmpty(this.WebsiteTitle)) this.WebsiteTitle = "欢迎光临我们的网站！";
					if (String.IsNullOrEmpty(this.NavText)) f = f.Replace("{2}", String.Empty);

					this._titleWithNav = String.Format(f, this.NavAction, this.WebsiteTitle, this.NavText);
				}

				return this._titleWithNav;
			}
		}

		/// <summary>
		/// 获取标题和页面动作、关键字列表。
		/// </summary>
		public string TitleWithNavActionAndTags
		{
			get
			{
				// NavAction - WebsiteTitle - TagListForTitle
				if (String.IsNullOrEmpty(this._titleWithNavActionAndTags))
				{
					var f = "{0} - {1} - {2}";

					if (String.IsNullOrEmpty(this.NavAction)) f = f.Replace("{0} - ", String.Empty);
					if (String.IsNullOrEmpty(this.WebsiteTitle)) this.WebsiteTitle = "欢迎光临我们的网站！";
					if (String.IsNullOrEmpty(this.Tags)) f = f.Replace("{2}", String.Empty);

					this._titleWithNavActionAndTags = String.Format(f, this.NavAction, this.WebsiteTitle, this.Tags);
				}

				return this._titleWithNavActionAndTags;
			}
		}

		/// <summary>
		/// 获取标题和页面动作。
		/// </summary>
		public string TitleWithNavAction
		{
			get
			{
				// NavAction - WebsiteTitle
				if (String.IsNullOrEmpty(this._titleWithNavAction))
				{
					var f = "{0} - {1}";

					if (String.IsNullOrEmpty(this.NavAction)) f = f.Replace("{0} - ", String.Empty);
					if (String.IsNullOrEmpty(this.WebsiteTitle)) this.WebsiteTitle = "欢迎光临我们的网站！";

					this._titleWithNavAction = String.Format(f, this.NavAction, this.WebsiteTitle);
				}

				return this._titleWithNavAction;
			}
		}

		/// <summary>
		/// 获取标题和页面导航。
		/// </summary>
		public string TitleWithNavText
		{
			get
			{
				// WebsiteTitle NavText
				if (String.IsNullOrEmpty(this._titleWithNavText))
				{
					var f = "{0}{1}";

					if (String.IsNullOrEmpty(this.WebsiteTitle)) this.WebsiteTitle = "欢迎光临我们的网站！";
					if (String.IsNullOrEmpty(this.NavText)) f = f.Replace("{1}", String.Empty);

					this._titleWithNavText = String.Format(f, this.WebsiteTitle, this.NavText);
				}

				return this._titleWithNavText;
			}
		}

		/// <summary>
		/// 获取面向 SEO 的标题。
		/// </summary>
		public string TitleWithSEO
		{
			get
			{
				if (String.IsNullOrEmpty(this._titleWithSEO))
				{
					var f = "{0} - {1}";

					if (String.IsNullOrEmpty(this.SEOKeywords)) f = f.Replace("{0} - ", String.Empty);

					this._titleWithSEO = String.Format(f, this.SEOKeywords, this.TitleWithTags);
				}

				return this._titleWithSEO;
			}
		}

		/// <summary>
		/// 获取标题和关键字列表
		/// </summary>
		public string TitleWithTags
		{
			get
			{
				// NavAction - TagListForTitle
				if (String.IsNullOrEmpty(this._titleWithTags))
				{
					var f = "{0} - {1}";

					if (String.IsNullOrEmpty(this.WebsiteTitle)) this.WebsiteTitle = "欢迎光临我们的网站！";
					if (String.IsNullOrEmpty(this.Tags)) f = f.Replace(" - {1}", String.Empty);

					this._titleWithTags = String.Format(f, this.WebsiteTitle, this.Tags);
				}

				return this._titleWithTags;
			}
		}

		/// <summary>
		/// 获取或设置网站用于显示在浏览器标题栏的文字信息。
		/// </summary>
		public string WebsiteTitle { get; set; }

		/// <summary>
		/// 获取或设置当前的页面动作信息，该信息可能被附加在标题文字的前边。
		/// </summary>
		public string NavAction { get; set; }

		/// <summary>
		/// 获取或设置当前的页面导航信息，该信息可能被附加在标题文字的后边。
		/// </summary>
		public string NavText { get; set; }

		/// <summary>
		/// 获取或设置用于 SEO 的标题关键字列表。
		/// </summary>
		public string SEOKeywords { get; set; }

		/// <summary>
		/// 获取或设置当前网站用于显示在浏览器标题栏的关键字列表。
		/// </summary>
		public string Tags { get; set; }

		#endregion
	}
}