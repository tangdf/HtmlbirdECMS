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
using Net.Htmlbird.Framework.Utilities;

namespace Net.Htmlbird.Framework.Web
{
	/// <summary>
	/// 表示用户位置导航信息。
	/// </summary>
	[Serializable]
	public sealed class PageNavigationInfo
	{
		#region 构造函数

		/// <summary>
		/// 初始化位置导航。
		/// </summary>
		/// <param name="title">浏览器标题信息</param>
		public PageNavigationInfo(PageTitleInfo title)
		{
			this.Title = title;

			this.Root = String.Empty;
			this.WebPath = HtmlbirdECMS.SystemInfo.ApplicationSetupPath.TrimEnd('/');
			this.Node = String.Empty;
			this.Action = String.Empty;
			this.NodeSeparator = " &raquo; ";
			this.ActionSeparator = "：";
		}

		#endregion

		#region 私有方法

		private string _FixPath(string path)
		{
			var p = path.StartsWith(this.WebPath) || path.StartsWith("http://") || path.StartsWith("https://") ? path : this.WebPath + path;

			p = HttpUtility.UrlPathEncode(p);

			return p;
		}

		#endregion

		#region 公有方法

		/// <summary>
		/// 设置位置导航信息的根节点信息。
		/// </summary>
		/// <param name="roottext">根路径的文字信息。</param>
		/// <returns>位置导航的根路径信息。</returns>
		public string SetRoot(string roottext) { return this.SetRoot(roottext); }

		/// <summary>
		/// 设置位置导航信息的根节点信息。
		/// </summary>
		/// <param name="roottext">根路径的文字信息。</param>
		/// <param name="rootlink">根路径的链接地址。</param>
		/// <returns>位置导航的根路径信息。</returns>
		public string SetRoot(string roottext, string rootlink)
		{
			// 无论何时都清理一次节点信息
			this.Clear();

			var s = "<a href=\"{0}\">{1}</a>";

			if (String.IsNullOrEmpty(roottext)) s = "首页";
			if (String.IsNullOrEmpty(rootlink)) s = "{0}";

			this.Root = String.Format(s, this._FixPath(rootlink), roottext);

			return this.Root;
		}

		/// <summary>
		/// 添加一个位置导航信息节点。
		/// </summary>
		/// <param name="nodetext">节点文本。</param>
		/// <returns>位置导航信息全部节点的 HTML 形式（包含分隔符）。</returns>
		public string AddNode(string nodetext) { return this.AddNode(nodetext, String.Empty); }

		/// <summary>
		/// 添加一个位置导航信息节点。
		/// </summary>
		/// <param name="nodetext">节点文本。</param>
		/// <param name="nodelink">节点链接。</param>
		/// <returns>位置导航信息全部节点的 HTML 形式（包含分隔符）。</returns>
		public string AddNode(string nodetext, string nodelink)
		{
			var s = "{0}<a href=\"{1}\">{2}</a>";

			if (String.IsNullOrEmpty(nodetext)) return String.Empty;
			if (String.IsNullOrEmpty(nodelink)) s = "{0}{2}";

			this.Node += String.Format(s, this.NodeSeparator, this._FixPath(nodelink), nodetext);
			this.Title.NavText = this.NodeText;

			return this.Node;
		}

		/// <summary>
		/// 设置位置导航信息的动作内容。
		/// </summary>
		/// <param name="actiontext">动作文本。</param>
		/// <param name="actioncontent">动作目标内容。</param>
		/// <returns>位置导航信息动作内容的 HTML 形式。</returns>
		public string SetAction(string actiontext, string actioncontent) { return this.SetAction(actiontext, actioncontent, String.Empty); }

		/// <summary>
		/// 设置位置导航信息的动作内容。
		/// </summary>
		/// <param name="actiontext">动作文本。</param>
		/// <param name="actioncontent">动作目标内容。</param>
		/// <param name="actionlink">动作目标内容的链接地址（一般不使用）。</param>
		/// <returns>位置导航信息动作内容的 HTML 形式。</returns>
		public string SetAction(string actiontext, string actioncontent, string actionlink)
		{
			var s = "{0}{1}{2}<a href=\"{3}\">{4}</a>";

			if (String.IsNullOrEmpty(actiontext)) s = "{0}<a href=\"{3}\">{4}</a>";
			if (String.IsNullOrEmpty(actioncontent)) return String.Empty;
			if (String.IsNullOrEmpty(actionlink)) s = s.Replace("<a href=\"{3}\">{4}</a>", "{4}");

			this.Action = String.Format(s, this.NodeSeparator, actiontext, this.ActionSeparator, this._FixPath(actionlink), actioncontent);
			this.Title.NavAction = this.ActionText;

			return this.Node;
		}

		/// <summary>
		/// 清理位置导航信息中的所有节点。
		/// </summary>
		public void Clear()
		{
			this.Root = String.Empty;
			this.Node = String.Empty;
			this.Action = String.Empty;

			// 同时清理浏览器标题信息
			this.Title.Clear();
		}

		/// <summary>
		/// 返回位置导航信息内容的 HTML 形式。
		/// </summary>
		/// <returns>位置导航信息内容的 HTML 形式。</returns>
		public override string ToString() { return this.HTML; }

		#endregion

		#region 公有属性

		/// <summary>
		/// 获取或设置自定义位置导航信息。
		/// </summary>
		public string CustomText { get; set; }

		/// <summary>
		/// 获取位置导航信息所有内容的 HTML 形式。
		/// </summary>
		public string HTML { get { return this.Root + this.Node + this.Action; } }

		/// <summary>
		/// 获取位置导航信息所有内容的文本形式（注意不包含根节点）。
		/// </summary>
		public string Text { get { return StringUtils.Replace(StringUtils.ClearHTML(this.Node + this.Action), "^" + this.NodeSeparator, String.Empty); } }

		/// <summary>
		/// 获取与当前导航信息相关联的浏览器标题信息构造器的实例。
		/// </summary>
		public PageTitleInfo Title { get; private set; }

		/// <summary>
		/// 获取位置导航信息的根节点。
		/// </summary>
		public string Root { get; private set; }

		/// <summary>
		/// 获取与当前实例相关联的网站根目录。
		/// </summary>
		public string WebPath { get; private set; }

		/// <summary>
		/// 获取位置导航信息根节点的文本形式。
		/// </summary>
		public string RootText { get { return StringUtils.ClearHTML(this.Root); } }

		/// <summary>
		/// 获取或设置位置导航信息的所有节点的内容。
		/// </summary>
		public string Node { get; set; }

		/// <summary>
		/// 获取位置导航信息根节点的文本形式。
		/// </summary>
		public string NodeText { get { return StringUtils.ClearHTML(this.Node); } }

		/// <summary>
		/// 获取或设置位置导航信息的动作内容。
		/// </summary>
		public string Action { get; set; }

		/// <summary>
		/// 获取位置导航信息根节点的文本形式。
		/// </summary>
		public string ActionText { get { return StringUtils.ClearHTML(this.Action).Replace(this.NodeSeparator, String.Empty); } }

		/// <summary>
		/// 获取或设置节点信息的连接符。
		/// </summary>
		public string NodeSeparator { get; set; }

		/// <summary>
		/// 获取或设置动作信息的连接符。
		/// </summary>
		public string ActionSeparator { get; set; }

		#endregion
	}
}