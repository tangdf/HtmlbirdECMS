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

namespace Net.Htmlbird.Framework.Web
{
	/// <summary>
	/// 表示系统消息。
	/// </summary>
	[Serializable]
	public sealed class MessageBox
	{
		#region 私有字段

		private MessageItemCollection _items;

		#endregion

		#region 构造函数

		/// <summary>
		/// 初始化系统消息。
		/// </summary>
		public MessageBox() { this.Type = MessageType.None; }

		#endregion

		#region 公有方法

		/// <summary>
		/// 添加一条消息。
		/// </summary>
		/// <param name="item">指定要添加的消息项的实例。</param>
		public void Add(MessageItem item) { this.Items.Add(item); }

		/// <summary>
		/// 添加一条消息。
		/// </summary>
		/// <param name="id">指定消息编号。</param>
		/// <param name="caption">指定消息标题。</param>
		public void Add(string id, string caption) { this.Items.Add(new MessageItem(id, caption, String.Empty)); }

		/// <summary>
		/// 添加一条消息。
		/// </summary>
		/// <param name="id">指定消息编号。</param>
		/// <param name="caption">指定消息标题。</param>
		/// <param name="content">指定消息内容。</param>
		public void Add(string id, string caption, string content) { this.Items.Add(new MessageItem(id, caption, content)); }

		/// <summary>
		/// 添加一条消息。
		/// </summary>
		/// <param name="id">指定消息编号。</param>
		/// <param name="caption">指定消息标题。</param>
		/// <param name="content">指定消息内容。</param>
		/// <param name="pageUrl">指定触发消息的页面地址。</param>
		public void Add(string id, string caption, string content, string pageUrl)
		{
			this.Items.Add(new MessageItem(id, caption, content) {
				PageUrl = pageUrl
			});
		}

		/// <summary>
		/// 清空消息项的集合。
		/// </summary>
		public void Clear() { this.Clear(true); }

		/// <summary>
		/// 清空消息项的集合。
		/// </summary>
		/// <param name="isResetType">是否同时重置消息类型。</param>
		public void Clear(bool isResetType)
		{
			this.Items.Clear();

			if (isResetType) this.Type = MessageType.None;
		}

		/// <summary>
		/// 显示消息并终止当前页面的执行。
		/// </summary>
		public void Show() { this.Show(true); }

		/// <summary>
		/// 显示消息。
		/// </summary>
		/// <param name="endResponse">指示当前页的执行是否应终止。</param>
		public void Show(bool endResponse) { HttpContext.Current.Response.Redirect("~/Message.html", endResponse); }

		/// <summary>
		/// 显示消息，并跳转到指定页面。
		/// </summary>
		/// <param name="url">目标的位置。</param>
		/// <param name="endResponse">指示当前页的执行是否应终止。</param>
		public void Show(string url, bool endResponse) { HttpContext.Current.Response.Redirect(url, endResponse); }

		#endregion

		#region 公有属性

		/// <summary>
		/// 获取消息类型的枚举值。
		/// </summary>
		public MessageType Type { get; set; }

		/// <summary>
		/// 获取消息类型的名称。
		/// </summary>
		public string TypeName { get { return HtmlbirdECMSEnumVictors.MessageTypeEnumVictor.GetDescriptionValue(this.Type); } }

		/// <summary>
		/// 获取消息项的集合。
		/// </summary>
		public MessageItemCollection Items { get { return this._items ?? (this._items = new MessageItemCollection()); } }

		#endregion
	}
}