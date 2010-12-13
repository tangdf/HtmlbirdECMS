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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Net.Htmlbird.Framework.Web
{
	/// <summary>
	/// 包含一组方法和属性，为生成验证码图像提供基本功能。
	/// </summary>
	public abstract class RandomizeImageProvider : IDisposable
	{
		#region 私有字段

		protected int FontSize = 12;
		private List<Font> _font;
		private List<ImageFormat> _formats;
		private List<Brush> _ownerBrushes;

		#endregion

		#region 构造函数

		/// <summary>
		/// 初始化图形验证码的新实例。
		/// </summary>
		/// <param name="width">指定验证码图像的宽度。</param>
		/// <param name="height">指定验证码图像的高度。</param>
		/// <param name="strukLineCount">指定验证码图像包含的干扰线的数量。</param>
		/// <param name="patternCount">指定验证码图像包含的干扰色块的数量。</param>
		/// <param name="randomNoise">指定验证码图像生成噪点的几率。</param>
		protected RandomizeImageProvider(int width, int height, int strukLineCount, int patternCount, double randomNoise) : this(String.Empty, width, height, ImageBorder.None, ImageBorder.None, new ImageMargin(0), new ImagePadding(0), strukLineCount, patternCount, randomNoise) { }

		/// <summary>
		/// 初始化图形验证码的新实例。
		/// </summary>
		/// <param name="width">指定验证码图像的宽度。</param>
		/// <param name="height">指定验证码图像的高度。</param>
		/// <param name="outsetBorder">指定验证码图像的外部边框。</param>
		/// <param name="strukLineCount">指定验证码图像包含的干扰线的数量。</param>
		/// <param name="patternCount">指定验证码图像包含的干扰色块的数量。</param>
		/// <param name="randomNoise">指定验证码图像生成噪点的几率。</param>
		protected RandomizeImageProvider(int width, int height, ImageBorder outsetBorder, int strukLineCount, int patternCount, double randomNoise) : this(String.Empty, width, height, outsetBorder, ImageBorder.None, new ImageMargin(0), new ImagePadding(0), strukLineCount, patternCount, randomNoise) { }

		/// <summary>
		/// 初始化图形验证码的新实例。
		/// </summary>
		/// <param name="width">指定验证码图像的宽度。</param>
		/// <param name="height">指定验证码图像的高度。</param>
		/// <param name="outsetBorder">指定验证码图像的外部边框。</param>
		/// <param name="insetBorder">指定验证码图像的内部边框。</param>
		/// <param name="margin">指定验证码图像的边距。</param>
		/// <param name="padding">指定验证码图像的补白。</param>
		/// <param name="strukLineCount">指定验证码图像包含的干扰线的数量。</param>
		/// <param name="patternCount">指定验证码图像包含的干扰色块的数量。</param>
		/// <param name="randomNoise">指定验证码图像生成噪点的几率。</param>
		protected RandomizeImageProvider(int width, int height, ImageBorder outsetBorder, ImageBorder insetBorder, ImageMargin margin, ImagePadding padding, int strukLineCount, int patternCount, double randomNoise) : this(String.Empty, width, height, outsetBorder, insetBorder, margin, padding, strukLineCount, patternCount, randomNoise) { }

		/// <summary>
		/// 初始化图形验证码的新实例。
		/// </summary>
		/// <param name="textSeeds">指定随机字符串的种子序列。</param>
		/// <param name="width">指定验证码图像的宽度。</param>
		/// <param name="height">指定验证码图像的高度。</param>
		/// <param name="outsetBorder">指定验证码图像的外部边框。</param>
		/// <param name="insetBorder">指定验证码图像的内部边框。</param>
		/// <param name="margin">指定验证码图像的边距。</param>
		/// <param name="padding">指定验证码图像的补白。</param>
		/// <param name="strukLineCount">指定验证码图像包含的干扰线的数量。</param>
		/// <param name="patternCount">指定验证码图像包含的干扰色块的数量。</param>
		/// <param name="randomNoise">指定验证码图像生成噪点的几率。</param>
		protected RandomizeImageProvider(string textSeeds, int width, int height, ImageBorder outsetBorder, ImageBorder insetBorder, ImageMargin margin, ImagePadding padding, int strukLineCount, int patternCount, double randomNoise)
		{
			if (String.IsNullOrEmpty(textSeeds)) textSeeds = "23456789ABCDEFGHJKMNPQRSTUVWXYZ";

			if (width <= 10) width = 10;
			if (height <= 10) height = 10;

			if (strukLineCount <= 0) strukLineCount = 0;
			if (patternCount <= 0) patternCount = 0;
			if (randomNoise <= 0.0) randomNoise = 0.0;
			if (randomNoise >= 1.0) randomNoise = 0.85;

			this.TextSeeds = textSeeds;
			this.Width = width;
			this.Height = height;
			this.OutsetBorder = outsetBorder;
			this.InsetBorder = insetBorder;
			this.Margin = margin;
			this.Padding = padding;
			this.StrukLineCount = strukLineCount;
			this.PatternCount = patternCount;
			this.RandomNoise = randomNoise;

			this._Initialize();
		}

		#endregion

		#region 私有方法

		private void _Initialize() { this.Initialize(); }

		#endregion

		#region 受保护方法

		/// <summary>
		/// 初始化绘制验证码图像所需要的参数。
		/// </summary>
		protected virtual void Initialize()
		{
			byte[] b = new byte[4];

			(new RNGCryptoServiceProvider()).GetBytes(b);

			this.Random = new Random(BitConverter.ToInt32(b, 0));
			this.ImageStream = new MemoryStream();
			this.Bitmap = new Bitmap(this.Width, this.Height);
			this.Graphics = Graphics.FromImage(this.Bitmap);
			this.Format = this.RandomImageFormat;
			this.Rectangle = new Rectangle(0, 0, this.Width, this.Height);

			this.OutsetBorderRect = new Rectangle(this.Margin.Left + (int)this.OutsetBorder.Left.Width / 2, this.Margin.Top + (int)this.OutsetBorder.Top.Width / 2, this.Rectangle.Width - this.Margin.Right * 2 - (int)this.OutsetBorder.Right.Width, this.Rectangle.Height - this.Margin.Bottom * 2 - (int)this.OutsetBorder.Bottom.Width);
			this.InsetBorderRect = new Rectangle(this.Margin.Left + (int)this.OutsetBorder.Left.Width + (int)this.InsetBorder.Left.Width / 2, this.Margin.Top + (int)this.OutsetBorder.Top.Width + (int)this.InsetBorder.Top.Width / 2, this.Rectangle.Width - this.Margin.Right * 2 - (int)this.OutsetBorder.Right.Width * 2 - (int)this.InsetBorder.Right.Width, this.Rectangle.Height - this.Margin.Bottom * 2 - (int)this.OutsetBorder.Bottom.Width * 2 - (int)this.InsetBorder.Bottom.Width);
			this.ContentRect = new Rectangle(this.Margin.Left + (int)this.OutsetBorder.Left.Width + (int)this.InsetBorder.Left.Width + this.Padding.Left, this.Margin.Top + (int)this.OutsetBorder.Top.Width + (int)this.InsetBorder.Top.Width + this.Padding.Top, this.Rectangle.Width - this.Margin.Right * 2 - (int)this.OutsetBorder.Right.Width * 2 - (int)this.InsetBorder.Right.Width * 2 - this.Padding.Right * 2, this.Rectangle.Height - this.Margin.Bottom * 2 - (int)this.OutsetBorder.Bottom.Width * 2 - (int)this.InsetBorder.Bottom.Width * 2 - this.Padding.Bottom * 2);
		}

		/// <summary>
		/// 初始化验证码图像将要显示的用于验证字符串。
		/// </summary>
		protected virtual void InitializeText()
		{
			StringBuilder s = new StringBuilder();

			for (var i = 0; i < this.CharCount; i++) s.Append(this.TextSeeds.Substring(this.Random.Next(0, this.TextSeeds.Length - 1), 1));

			this.Text = s.ToString();
		}

		/// <summary>
		/// 在子类重写时实现绘制背景内容的功能。
		/// </summary>
		protected abstract void DrawBackground();

		/// <summary>
		/// 在子类重写时实现绘制干扰色块的功能。
		/// </summary>
		protected abstract void DrawPattern();

		/// <summary>
		/// 在子类重写时实现绘制噪点的功能。
		/// </summary>
		protected abstract void DrawRandomNoise();

		/// <summary>
		/// 在子类重写时实现绘制文本内容的功能。
		/// </summary>
		protected abstract void DrawText();

		/// <summary>
		/// 在子类重写时实现绘制干扰线的功能。
		/// </summary>
		protected abstract void DrawStrukLine();

		/// <summary>
		/// 在子类重写时实现绘制外部边框的功能。
		/// </summary>
		protected virtual void DrawOutsideBorder()
		{
			if (this.OutsetBorder.UseRandomBrush) this.OutsetBorder.SetBrush(this.GetRandomBrush(Color.FromArgb(this.Random.Next(100, 255), this.RandomColor)));

			if (this.OutsetBorder.Left.Width > 0) this.Graphics.DrawLine(this.OutsetBorder.Left, this.OutsetBorderRect.Left, this.OutsetBorderRect.Bottom + this.OutsetBorder.Left.Width / 2, this.OutsetBorderRect.Left, this.OutsetBorderRect.Top + this.OutsetBorder.Left.Width / 2);

			if (this.OutsetBorder.Top.Width > 0) this.Graphics.DrawLine(this.OutsetBorder.Top, this.OutsetBorderRect.Left - this.OutsetBorder.Top.Width / 2, this.OutsetBorderRect.Top, this.OutsetBorderRect.Right - this.OutsetBorder.Top.Width / 2, this.OutsetBorderRect.Top);

			if (this.OutsetBorder.Right.Width > 0) this.Graphics.DrawLine(this.OutsetBorder.Right, this.OutsetBorderRect.Right, this.OutsetBorderRect.Top - this.OutsetBorder.Right.Width / 2, this.OutsetBorderRect.Right, this.OutsetBorderRect.Bottom - this.OutsetBorder.Right.Width / 2);

			if (this.OutsetBorder.Bottom.Width > 0) this.Graphics.DrawLine(this.OutsetBorder.Bottom, this.OutsetBorderRect.Right + this.OutsetBorder.Bottom.Width / 2, this.OutsetBorderRect.Bottom, this.OutsetBorderRect.Left + this.OutsetBorder.Bottom.Width / 2, this.OutsetBorderRect.Bottom);
		}

		/// <summary>
		/// 在子类重写时实现绘制内部边框的功能。
		/// </summary>
		protected virtual void DrawInsideBorder()
		{
			if (this.InsetBorder.UseRandomBrush) this.InsetBorder.SetBrush(this.GetRandomBrush(Color.FromArgb(this.Random.Next(100, 255), this.RandomColor)));

			if (this.InsetBorder.Left.Width > 0) this.Graphics.DrawLine(this.InsetBorder.Left, this.InsetBorderRect.Left, this.InsetBorderRect.Bottom + this.InsetBorder.Left.Width / 2, this.InsetBorderRect.Left, this.InsetBorderRect.Top + this.InsetBorder.Left.Width / 2);

			if (this.InsetBorder.Top.Width > 0) this.Graphics.DrawLine(this.InsetBorder.Top, this.InsetBorderRect.Left - this.InsetBorder.Top.Width / 2, this.InsetBorderRect.Top, this.InsetBorderRect.Right - this.InsetBorder.Top.Width / 2, this.InsetBorderRect.Top);

			if (this.InsetBorder.Right.Width > 0) this.Graphics.DrawLine(this.InsetBorder.Right, this.InsetBorderRect.Right, this.InsetBorderRect.Top - this.InsetBorder.Right.Width / 2, this.InsetBorderRect.Right, this.InsetBorderRect.Bottom - this.InsetBorder.Right.Width / 2);

			if (this.InsetBorder.Bottom.Width > 0) this.Graphics.DrawLine(this.InsetBorder.Bottom, this.InsetBorderRect.Right + this.InsetBorder.Bottom.Width / 2, this.InsetBorderRect.Bottom, this.InsetBorderRect.Left + this.InsetBorder.Bottom.Width / 2, this.InsetBorderRect.Bottom);
		}

		/// <summary>
		/// 获取可用于绘制验证码图像的随机笔刷。
		/// </summary>
		/// <param name="color">指定笔刷颜色。</param>
		protected Brush GetRandomBrush(Color color)
		{
			var brush = this.OwnerBrushes[this.Random.Next(0, this.OwnerBrushes.Count)];

			if (brush is HatchBrush) brush = new HatchBrush(((HatchBrush)brush).HatchStyle, color);
			else brush = new SolidBrush(color);

			return brush;
		}

		#endregion

		#region 公有方法

		/// <summary>
		/// 绘制验证码图像并返回其实例。
		/// </summary>
		/// <param name="charCount">指定要包含的用于验证的字符串的长度。</param>
		/// <returns>返回所绘制的验证码图像的实例。</returns>
		public virtual Image Generate(int charCount)
		{
			this.CharCount = charCount;

			this.InitializeText();
			this.DrawBackground();
			this.DrawPattern();
			this.DrawRandomNoise();
			this.DrawText();
			this.DrawStrukLine();
			this.DrawOutsideBorder();
			this.DrawInsideBorder();

			this.Bitmap.Save(this.ImageStream, this.Format);

			return this.Bitmap;
		}

		/// <summary>
		/// 绘制验证码图像并返回其实例。
		/// </summary>
		/// <param name="charCount">指定要包含的用于验证的字符串的长度。</param>
		/// <param name="format">指定图像的文件格式。经测试只有以下格式可用：Bmp、Gif、Jpeg、Png、Tiff。</param>
		/// <returns>返回所绘制的验证码图像的实例。</returns>
		public Image Generate(int charCount, ImageFormat format)
		{
			this.Format = format;

			return this.Generate(charCount);
		}

		#endregion

		#region 受保护属性

		/// <summary>
		/// 在子类重写时获取用于绘制验证码图像中用于验证的字符串的字体。
		/// </summary>
		protected virtual List<Font> Font
		{
			get
			{
				return this._font ?? (this._font = new List<Font> {
					new Font("宋体", this.FontSize, (FontStyle)(2 ^ (this.Random.Next(1, 5) - 1)), GraphicsUnit.Pixel),
					new Font("黑体", this.FontSize, (FontStyle)(2 ^ (this.Random.Next(1, 5) - 1)), GraphicsUnit.Pixel),
					new Font("楷书", this.FontSize, (FontStyle)(2 ^ (this.Random.Next(1, 5) - 1)), GraphicsUnit.Pixel),
					new Font("幼圆", this.FontSize, (FontStyle)(2 ^ (this.Random.Next(1, 5) - 1)), GraphicsUnit.Pixel),
					new Font("Arial Black", this.FontSize, (FontStyle)(2 ^ (this.Random.Next(1, 5) - 1)), GraphicsUnit.Pixel),
					new Font("Times New Roman", this.FontSize, (FontStyle)(2 ^ (this.Random.Next(1, 5) - 1)), GraphicsUnit.Pixel),
					new Font("Verdana", this.FontSize, (FontStyle)(2 ^ (this.Random.Next(1, 5) - 1)), GraphicsUnit.Pixel),
					new Font("Lucida Console", this.FontSize, (FontStyle)(2 ^ (this.Random.Next(1, 5) - 1)), GraphicsUnit.Pixel),
					new Font("Tahoma", this.FontSize, (FontStyle)(2 ^ (this.Random.Next(1, 5) - 1)), GraphicsUnit.Pixel),
					new Font("Microsoft Sans Serif", this.FontSize, (FontStyle)(2 ^ (this.Random.Next(1, 5) - 1)), GraphicsUnit.Pixel),
					new Font("Lucida Sans Unicode", this.FontSize, (FontStyle)(2 ^ (this.Random.Next(1, 5) - 1)), GraphicsUnit.Pixel),
					new Font("Courier New", this.FontSize, (FontStyle)(2 ^ (this.Random.Next(1, 5) - 1)), GraphicsUnit.Pixel),
					new Font("MS Sans Serif", this.FontSize, (FontStyle)(2 ^ (this.Random.Next(1, 5) - 1)), GraphicsUnit.Pixel)
				});
			}
		}

		/// <summary>
		/// 获取或设置验证码图像的外部边框。
		/// </summary>
		protected ImageBorder OutsetBorder { get; set; }

		/// <summary>
		/// 获取或设置验证码图像的内部边框。
		/// </summary>
		protected ImageBorder InsetBorder { get; set; }

		/// <summary>
		/// 获取一个随机字体的实例。
		/// </summary>
		protected Font RandomFont { get { return this.Font[this.Random.Next(0, this.Font.Count)]; } }

		/// <summary>
		/// 获取用于绘制验证码图像的图像画面的实例。
		/// </summary>
		protected Graphics Graphics { get; private set; }

		/// <summary>
		/// 获取用于绘制验证码图像的图形区域。
		/// </summary>
		protected Rectangle Rectangle { get; private set; }

		/// <summary>
		/// 获取用于绘制验证码图像的外部边框的图形区域。
		/// </summary>
		protected Rectangle OutsetBorderRect { get; private set; }

		/// <summary>
		/// 获取用于绘制验证码图像的内部边框的图形区域。
		/// </summary>
		protected Rectangle InsetBorderRect { get; private set; }

		/// <summary>
		/// 获取用于绘制验证码图像的验证字符、干扰线、干扰色块以及噪点的图形区域。
		/// </summary>
		protected Rectangle ContentRect { get; private set; }

		/// <summary>
		/// 获取用于验证码的随机字符串的种子序列。
		/// </summary>
		protected string TextSeeds { get; private set; }

		/// <summary>
		/// 获取绘制验证码图像的位图的实例。
		/// </summary>
		protected Bitmap Bitmap { get; private set; }

		/// <summary>
		/// 获取伪随机数生成器的实例。
		/// </summary>
		protected Random Random { get; private set; }

		/// <summary>
		/// 获取一个随机数，该随机数大于等于 0，小于等于 255。
		/// </summary>
		protected int Random255 { get { return this.Random.Next(0, 256); } }

		/// <summary>
		/// 获取一个随机颜色。
		/// </summary>
		protected Color RandomColor
		{
			get
			{
				Color color;

				do
				{
					color = Color.FromArgb(255, this.Random255, this.Random255, this.Random255);
				}
				while (color == Color.Empty);

				return color;
			}
		}

		/// <summary>
		/// 获取一个带有透明度的随机颜色。
		/// </summary>
		protected Color RandomColorAlpha
		{
			get
			{
				Color color;

				do
				{
					color = Color.FromArgb(this.Random255, this.Random255, this.Random255, this.Random255);
				}
				while (color == Color.Empty);

				return color;
			}
		}

		/// <summary>
		/// 获取可用于绘制验证码图像的笔刷的集合。
		/// </summary>
		protected List<Brush> OwnerBrushes
		{
			get
			{
				return this._ownerBrushes ?? (this._ownerBrushes = new List<Brush> {
					new SolidBrush(this.RandomColorAlpha),
					new LinearGradientBrush(new Rectangle(1, 2, 3, 4), this.RandomColorAlpha, this.RandomColorAlpha, this.Random.Next(0, 360)),
					new LinearGradientBrush(new Rectangle(2, 2, 4, 3), this.RandomColorAlpha, this.RandomColorAlpha, this.Random.Next(0, 360)),
					new LinearGradientBrush(new Rectangle(1, 1, 2, 2), this.RandomColorAlpha, this.RandomColorAlpha, this.Random.Next(0, 360)),
					new LinearGradientBrush(new Rectangle(2, 2, 1, 1), this.RandomColorAlpha, this.RandomColorAlpha, this.Random.Next(0, 360)),
					new LinearGradientBrush(new Rectangle(3, 4, 1, 2), this.RandomColorAlpha, this.RandomColorAlpha, this.Random.Next(0, 360)),
					new LinearGradientBrush(new Rectangle(5, 5, 10, 10), this.RandomColorAlpha, this.RandomColorAlpha, this.Random.Next(0, 360)),
					new LinearGradientBrush(new Rectangle(5, 5, 20, 20), this.RandomColorAlpha, this.RandomColorAlpha, this.Random.Next(0, 360)),
					new LinearGradientBrush(new Rectangle(10, 10, 5, 5), this.RandomColorAlpha, this.RandomColorAlpha, this.Random.Next(0, 360)),
					new HatchBrush(HatchStyle.Percent75, this.RandomColorAlpha),
					new HatchBrush(HatchStyle.Percent80, this.RandomColorAlpha),
					new HatchBrush(HatchStyle.Percent90, this.RandomColorAlpha),
					new HatchBrush(HatchStyle.Trellis, this.RandomColorAlpha)
				});
			}
		}

		/// <summary>
		/// 获取验证码图像的边距设置。
		/// </summary>
		protected ImageMargin Margin { get; private set; }

		/// <summary>
		/// 获取验证码图像的补白设置。
		/// </summary>
		protected ImagePadding Padding { get; private set; }

		/// <summary>
		/// 获取验证码图像的干扰线数量。
		/// </summary>
		protected int StrukLineCount { get; private set; }

		/// <summary>
		/// 获取验证码图像的干扰色块数量。
		/// </summary>
		protected int PatternCount { get; private set; }

		/// <summary>
		/// 获取验证码图像生成噪点的几率。
		/// </summary>
		protected double RandomNoise { get; private set; }

		#endregion

		#region 公有属性

		/// <summary>
		/// 获取或设置验证码图像的文件格式，只能是 Bmp、Gif、Jpeg、Png、Tiff 其中的一种。
		/// </summary>
		public ImageFormat Format { get; set; }

		/// <summary>
		/// 获取一个随机的可用于验证码图像的文件格式，返回的图像格式必然是 Bmp、Gif、Jpeg、Png、Tiff 其中的一种。
		/// </summary>
		public ImageFormat RandomImageFormat
		{
			get
			{
				if (this._formats == null || this._formats.Count == 0)
				{
					this._formats = new List<ImageFormat> {
						ImageFormat.Bmp,
						ImageFormat.Gif,
						ImageFormat.Jpeg,
						ImageFormat.Png
					};
				}

				return this._formats[this.Random.Next(0, this._formats.Count)];
			}
		}

		/// <summary>
		/// 获取与验证码凸显高度文件格式相匹配的 MIME 类型。
		/// </summary>
		public string ContentType
		{
			get
			{
				if (this.Format == ImageFormat.Bmp) return "image/bmp";
				if (this.Format == ImageFormat.Gif) return "image/gif";
				if (this.Format == ImageFormat.Jpeg) return "image/jpeg";
				return this.Format == ImageFormat.Png ? "image/png" : String.Empty;
			}
		}

		/// <summary>
		/// 获取与验证码图像的文件格式相匹配的扩展名。
		/// </summary>
		public string FileExtension
		{
			get
			{
				if (this.Format == ImageFormat.Bmp) return "bmp";
				if (this.Format == ImageFormat.Gif) return "gif";
				if (this.Format == ImageFormat.Jpeg) return "jpg";
				return this.Format == ImageFormat.Png ? "png" : String.Empty;
			}
		}

		/// <summary>
		/// 获取验证码图像中包含的用于验证的字符串的长度。
		/// </summary>
		public int CharCount { get; private set; }

		/// <summary>
		/// 获取验证码图像的宽度。
		/// </summary>
		public int Width { get; private set; }

		/// <summary>
		/// 获取验证码图像的高度。
		/// </summary>
		public int Height { get; private set; }

		/// <summary>
		/// 获取包含验证码图像的支持存储区为内存的流。
		/// </summary>
		public MemoryStream ImageStream { get; protected set; }

		/// <summary>
		/// 获取验证码图像中包含的用于验证的字符串。
		/// </summary>
		public string Text { get; protected set; }

		#endregion

		#region IDisposable 成员

		/// <summary>
		/// 释放由 YMind.RandomizeCodeImage.RandomizeCodeImageBase 占用的系统资源。
		/// </summary>
		public void Dispose()
		{
			if (this.Graphics != null) this.Graphics.Dispose();
			if (this.Bitmap != null) this.Bitmap.Dispose();
		}

		#endregion
	}
}