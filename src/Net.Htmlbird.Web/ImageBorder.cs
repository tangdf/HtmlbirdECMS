// ===============================================================================
//  产品名称：网鸟电子商务管理系统(Htmlbird ECMS)
//  产品作者：YMind Chan
//  版权所有：网鸟IT技术论坛 颜铭工作室
// ===============================================================================
//  Copyright © Htmlbird.Net. All rights reserved .
//  官方网站：http://www.htmlbird.net/
//  技术论坛：http://bbs.htmlbird.net/
// ===============================================================================
using System.Drawing;

namespace Net.Htmlbird.Framework.Web
{
	/// <summary>
	/// 表示图形边框。
	/// </summary>
	public struct ImageBorder
	{
		#region 私有字段

		private Pen _bottom;
		private Pen _left;
		private Pen _right;
		private Pen _top;

		private bool _useRandomBrush;

		#endregion

		#region 公有字段

		/// <summary>
		/// 表示无边框。
		/// </summary>
		public static ImageBorder None { get; private set; }

		#endregion

		#region 构造函数

		/// <summary>
		/// 初始化图形边框中的静态常量。
		/// </summary>
		static ImageBorder() { None = new ImageBorder(0, Brushes.Transparent); }

		/// <summary>
		/// 初始化图形边框。
		/// </summary>
		/// <param name="width">指定用于绘制图形边框的画笔的大小。</param>
		public ImageBorder(int width) : this(width, Brushes.Gray) { }

		/// <summary>
		/// 初始化图形边框。
		/// </summary>
		/// <param name="pen">指定用于绘制图形边框的画笔的实例。</param>
		public ImageBorder(Pen pen) : this(pen, pen, pen, pen) { }

		/// <summary>
		/// 初始化图形边框。
		/// </summary>
		/// <param name="width">指定用于绘制图形边框的画笔的大小。</param>
		/// <param name="brush">指定用于绘制图形边框的画笔的笔刷样式。</param>
		public ImageBorder(int width, Brush brush) : this(new Pen(brush, width), new Pen(brush, width), new Pen(brush, width), new Pen(brush, width)) { }

		/// <summary>
		/// 初始化图形边框。
		/// </summary>
		/// <param name="lar">指定用于绘制图形边框的左边框和右边框的画笔的大小。</param>
		/// <param name="tab">指定用于绘制图形边框的上边框和下边框的画笔的大小。</param>
		/// <param name="blar">指定用于绘制图形边框的左边框和右边框的画笔的大小。</param>
		/// <param name="btab">指定用于绘制图形边框的上边框和下边框的画笔的大小。</param>
		public ImageBorder(int lar, int tab, Brush blar, Brush btab) : this(new Pen(blar, lar), new Pen(btab, tab), new Pen(blar, lar), new Pen(btab, tab)) { }

		/// <summary>
		/// 初始化图形边框。
		/// </summary>
		/// <param name="lar">指定用于绘制图形边框的左边框和右边框的画笔的实例。</param>
		/// <param name="tab">指定用于绘制图形边框的上边框和下边框的画笔的实例。</param>
		public ImageBorder(Pen lar, Pen tab) : this(lar, tab, lar, tab) { }

		/// <summary>
		/// 初始化图形边框。
		/// </summary>
		/// <param name="left">指定用于绘制图形边框的左边框的画笔的实例。</param>
		/// <param name="top">指定用于绘制图形边框的上边框的画笔的实例。</param>
		/// <param name="right">指定用于绘制图形边框的右边框的画笔的实例。</param>
		/// <param name="bottom">指定用于绘制图形边框的下边框的画笔的实例。</param>
		public ImageBorder(Pen left, Pen top, Pen right, Pen bottom)
		{
			this._left = left;
			this._top = top;
			this._right = right;
			this._bottom = bottom;

			this._useRandomBrush = false;
		}

		#endregion

		#region 公有方法

		/// <summary>
		/// 设置绘制图形边框时使用的笔刷。
		/// </summary>
		/// <param name="brush">指定一个笔刷实例。</param>
		public void SetBrush(Brush brush)
		{
			this._left.Brush = brush;
			this._top.Brush = brush;
			this._right.Brush = brush;
			this._bottom.Brush = brush;
		}

		#endregion

		#region 公有属性

		/// <summary>
		/// 获取或设置是否使用随机笔刷绘制，该属性将覆盖所有边框的笔刷设置。
		/// </summary>
		public bool UseRandomBrush { get { return this._useRandomBrush; } set { this._useRandomBrush = value; } }

		/// <summary>
		/// 获取或设置左边框的画笔。
		/// </summary>
		public Pen Left
		{
			get
			{
				if (this._left.Width < 0) this._left.Width = 0;

				return this._left;
			}
			set { this._left = value; }
		}

		/// <summary>
		/// 获取或设置上边框的画笔。
		/// </summary>
		public Pen Top
		{
			get
			{
				if (this._top.Width < 0) this._top.Width = 0;

				return this._top;
			}
			set { this._top = value; }
		}

		/// <summary>
		/// 获取或设置右边框的画笔。
		/// </summary>
		public Pen Right
		{
			get
			{
				if (this._right.Width < 0) this._right.Width = 0;

				return this._right;
			}
			set { this._right = value; }
		}

		/// <summary>
		/// 获取或设置下边框的画笔。
		/// </summary>
		public Pen Bottom
		{
			get
			{
				if (this._bottom.Width < 0) this._bottom.Width = 0;

				return this._bottom;
			}
			set { this._bottom = value; }
		}

		#endregion
	}
}