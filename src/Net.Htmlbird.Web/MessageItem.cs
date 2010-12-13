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
using Net.Htmlbird.Framework.Modules;

namespace Net.Htmlbird.Framework.Web
{
	/// <summary>
	/// 表示系统消息的一个项。
	/// </summary>
	[Serializable]
	public class MessageItem
	{
		#region 私有字段

		private string _caption;
		private string _id;

		#endregion

		#region 公有字段

		/// <summary>
		/// 表示空消息。
		/// </summary>
		public static MessageItem Empty;

		#endregion

		#region 构造函数

		static MessageItem() { Empty = new MessageItem("NONE-00000000"); }

		private MessageItem(string id)
		{
			this._id = id;
			this._caption = String.Empty;
			this.Content = String.Empty;
			this.PageUrl = String.Empty;
			this.Reason = String.Empty;
			this.Resolvent = String.Empty;
		}

		/// <summary>
		/// 初始化一个系统消息项的新实例。
		/// </summary>
		/// <param name="id">指定消息编号。</param>
		/// <param name="caption">指定消息标题。</param>
		/// <param name="content">指定消息内容。</param>
		public MessageItem(string id, string caption, string content)
		{
			if (id.StartsWith("TIPS-") == false && id.StartsWith("ERROR-") == false && id.StartsWith("WARNING-") == false) throw new ArgumentOutOfRangeException("Id", "消息编号必须以“TIPS-”、“ERROR-”或“WARNING-”其中一个开头。");
			if (String.IsNullOrEmpty(caption)) throw new ArgumentOutOfRangeException("Caption", "消息标题不能为 null 或空字符串。");

			this._id = id;
			this._caption = caption;
			this.Content = content;
			this.PageUrl = String.Empty;
			this.Reason = String.Empty;
			this.Resolvent = String.Empty;
		}

		#endregion

		#region 运算符

		/// <summary>
		/// 确定 <see cref="MessageItem"/> 的两个指定的实例是否不等。
		/// </summary>
		/// <param name="m1">第一个 <see cref="MessageItem"/> 的实例。</param>
		/// <param name="m2">第二个 <see cref="MessageItem"/> 的实例。</param>
		/// <returns>如果 <paramref name="m1"/> 和 <paramref name="m2"/> 不表示同一语言包，则为 true；否则为 false。</returns>
		public static bool operator !=(MessageItem m1, MessageItem m2) { return !(m1 == m2); }

		/// <summary>
		/// 确定 <see cref="MessageItem"/> 的两个指定的实例是否相等。
		/// </summary>
		/// <param name="m1">第一个 <see cref="MessageItem"/> 的实例。</param>
		/// <param name="m2">第二个 <see cref="MessageItem"/> 的实例。</param>
		/// <returns>如果 <paramref name="m1"/> 和 <paramref name="m2"/> 表示同一语言包，则为 true；否则为 false。</returns>
		public static bool operator ==(MessageItem m1, MessageItem m2) { return m1.Id == m2.Id; }

		#endregion

		#region 公有方法

		/// <summary>
		/// 返回一个值，该值指示此实例是否与指定的对象相等。
		/// </summary>
		/// <param name="obj">要与此实例进行比较的对象。</param>
		/// <returns>如果 <paramref name="obj"/> 是 <see cref="MessageItem"/> 的实例并且等于此实例的值，则为 true；否则为 false。</returns>
		public override bool Equals(object obj)
		{
			if (obj is MessageItem)
			{
				MessageItem item = (MessageItem)obj;

				return this == item;
			}

			return false;
		}

		/// <summary>
		/// 返回此实例的哈希代码。
		/// </summary>
		/// <returns>32 位有符号整数哈希代码。</returns>
		public override int GetHashCode() { return this.Id.GetHashCode(); }

		/// <summary>
		/// 返回表示当前 <see cref="MessageItem"/> 的 <see cref="System.String"/>。
		/// </summary>
		/// <returns><see cref="System.String"/>，表示当前的 <see cref="MessageItem"/>。</returns>
		public override string ToString() { return DynamicJson.Serialize(this); }

		#endregion

		#region 公有属性

		/// <summary>
		/// 获取或设置该消息的编号。
		/// </summary>
		public string Id
		{
			get { return this._id; }
			set
			{
				this._id = value;

				if (this._id.StartsWith("TIPS-") == false && this._id.StartsWith("ERROR-") == false && this._id.StartsWith("WARNING-") == false) throw new ArgumentOutOfRangeException("value", "消息编号必须以“TIPS-”、“ERROR-”或“WARNING-”其中一个开头。");
			}
		}

		/// <summary>
		/// 获取或设置该消息的标题。
		/// </summary>
		public string Caption
		{
			get { return this._caption; }
			set
			{
				this._caption = value;

				if (String.IsNullOrEmpty(this._caption)) throw new ArgumentOutOfRangeException("value", "消息标题不能为 null 或空字符串。");
			}
		}

		/// <summary>
		/// 获取或设置该消息的详细内容。
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// 获取或设置触发该消息的页面地址。
		/// </summary>
		public string PageUrl { get; set; }

		/// <summary>
		/// 获取或设置触发该消息的具体原因。
		/// </summary>
		public string Reason { get; set; }

		/// <summary>
		/// 获取或设置该消息所关联的解决方案，这通常在错误消息或警告消息下有用。
		/// </summary>
		public string Resolvent { get; set; }

		#endregion
	}
}