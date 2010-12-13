// ===============================================================================
//  产品名称：网鸟电子商务管理系统(Htmlbird ECMS)
//  产品作者：YMind Chan
//  版权所有：网鸟IT技术论坛 颜铭工作室
// ===============================================================================
//  Copyright © Htmlbird.Net. All rights reserved .
//  官方网站：http://www.htmlbird.net/
//  技术论坛：http://bbs.htmlbird.net/
// ===============================================================================
using System.Collections.Generic;
using System.Web;
using Net.Htmlbird.Framework.Web.Modules;

namespace Net.Htmlbird.Framework.Web
{
	/// <summary>
	/// 提供枚举值的常用操作。
	/// </summary>
	public static class HtmlbirdECMSEnumVictors
	{
		/// <summary>
		/// 表示对 <see cref="AspNetHostingPermissionLevel"/> 枚举值的实用操作的封装。
		/// </summary>
		public static EnumVictor<AspNetHostingPermissionLevel> AspNetHostingPermissionLevelVictor = new EnumVictor<AspNetHostingPermissionLevel>(new Dictionary<AspNetHostingPermissionLevel, string> {
			{AspNetHostingPermissionLevel.None, "没有权限"},
			{AspNetHostingPermissionLevel.Minimal, "最小权限"},
			{AspNetHostingPermissionLevel.Low, "较低权限"},
			{AspNetHostingPermissionLevel.Medium, "中等权限"},
			{AspNetHostingPermissionLevel.High, "较高权限"},
			{AspNetHostingPermissionLevel.Unrestricted, "所有权限"}
		});

		/// <summary>
		/// 表示对 <see cref="MessageType"/> 枚举值的实用操作的封装。
		/// </summary>
		public static EnumVictor<MessageType> MessageTypeEnumVictor = new EnumVictor<MessageType>();
	}
}