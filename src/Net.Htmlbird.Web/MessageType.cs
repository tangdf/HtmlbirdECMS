// ===============================================================================
//  产品名称：网鸟电子商务管理系统(Htmlbird ECMS)
//  产品作者：YMind Chan
//  版权所有：网鸟IT技术论坛 颜铭工作室
// ===============================================================================
//  Copyright © Htmlbird.Net. All rights reserved .
//  官方网站：http://www.htmlbird.net/
//  技术论坛：http://bbs.htmlbird.net/
// ===============================================================================
namespace Net.Htmlbird.Framework.Web
{
	/// <summary>
	/// 表示系统消息类型常数的枚举。
	/// </summary>
	public enum MessageType
	{
		/// <summary>
		/// 未指定消息类型。
		/// </summary>
		[EnumDescription("未知")]
		None = 0,

		/// <summary>
		/// 系统提示信息。
		/// </summary>
		[EnumDescription("提示")]
		Tips = 1,

		/// <summary>
		/// 系统错误信息。
		/// </summary>
		[EnumDescription("错误")]
		Error = 2,

		/// <summary>
		/// 系统警告信息。
		/// </summary>
		[EnumDescription("警告")]
		Warning = 3,
	}
}