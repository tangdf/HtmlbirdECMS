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
using System.Linq;
using Net.Htmlbird.Framework.Modules;

namespace Net.Htmlbird.Framework.Web
{
	/// <summary>
	/// 表示系统消息项的集合。
	/// </summary>
	[Serializable]
	public sealed class MessageItemCollection : List<MessageItem>
	{
		/// <summary>
		/// 获取具有指定编号的消息项的实例。
		/// </summary>
		/// <param name="id">指定消息编号。</param>
		/// <returns>如果找到具有指定编号的消息项则返回对其实例的引用，否则返回 <see cref="MessageItem.Empty"/>。</returns>
		public MessageItem this[string id] { get { return this.Where(item => item.Id == id).FirstOrDefault(); } }

		/// <summary>
		/// 返回表示当前 <see cref="MessageItemCollection"/> 的 <see cref="System.String"/>。
		/// </summary>
		/// <returns><see cref="System.String"/>，表示当前的 <see cref="MessageItemCollection"/>。</returns>
		public override string ToString() { return DynamicJson.Serialize(this); }
	}
}