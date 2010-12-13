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
using Net.Htmlbird.Framework.Utilities;

namespace Net.Htmlbird.Framework.Web
{
	/// <summary>
	/// 封装验证码图像生成器。
	/// </summary>
	public class RandomizeCodeImage : RandomizeImageProvider
	{
		#region 私有字段

		private const string _TEXT_SEEDS = "23456789ABCDEFGHJKMNPQRSTUVWXYZ";
		//private static string _TextSeeds2 = "的一是在不了有和人这中大为上个国我以要他时来用们生到作地于出就分对成会可主发年动同工也能下过子说产种面而方后多定行学法所民得经十三之进等部度家电力里如水化高自二理起小物现实加都两体制机当使点从业本去把好应开它合还因由其些然前外天政四日那社义平形相全表间样与关各重新线内数正心反你明看原又么利比或但质气第向道命此变条只没结解问意建月公无系军很情者最立代想已通并提直题党程展五果料象员革位入常文总次品式活设及管特件长求老头基资边流路级少图山统接知较将组见计别她手角期根论运农指几九区强放决西被干做必战先回则任取据处队南给色光门即保治北造百规热领七海口东导器压志世金争济阶油思术极交受联什认六共权收证改清己美再采转更单风切打白教速花带安场身车例真务具万每目至达走积示议声报斗完类八离华名确才科张信马节话米整空元况今集温传土许步群广石记需段研界拉林律叫且究观越织装影算低持音众书布复容儿须际商非验连断深难近矿千周委素技备半办青省列习响约支般史感劳便团往历市克何除消构府称太准精值号率族维划选标写存候毛亲快效斯院查江型眼王按格养易置派层片始却专状育厂京识适属圆包火住调满县局参红细引听该铁价严";

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
		public RandomizeCodeImage(int width, int height, int strukLineCount, int patternCount, double randomNoise) : this(String.Empty, width, height, ImageBorder.None, ImageBorder.None, new ImageMargin(0), new ImagePadding(0), strukLineCount, patternCount, randomNoise) { }

		/// <summary>
		/// 初始化图形验证码的新实例。
		/// </summary>
		/// <param name="width">指定验证码图像的宽度。</param>
		/// <param name="height">指定验证码图像的高度。</param>
		/// <param name="outsetBorder">指定验证码图像的外部边框。</param>
		/// <param name="strukLineCount">指定验证码图像包含的干扰线的数量。</param>
		/// <param name="patternCount">指定验证码图像包含的干扰色块的数量。</param>
		/// <param name="randomNoise">指定验证码图像生成噪点的几率。</param>
		public RandomizeCodeImage(int width, int height, ImageBorder outsetBorder, int strukLineCount, int patternCount, double randomNoise) : this(String.Empty, width, height, outsetBorder, ImageBorder.None, new ImageMargin(0), new ImagePadding(0), strukLineCount, patternCount, randomNoise) { }

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
		public RandomizeCodeImage(int width, int height, ImageBorder outsetBorder, ImageBorder insetBorder, ImageMargin margin, ImagePadding padding, int strukLineCount, int patternCount, double randomNoise) : this(String.Empty, width, height, outsetBorder, insetBorder, margin, padding, strukLineCount, patternCount, randomNoise) { }

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
		public RandomizeCodeImage(string textSeeds, int width, int height, ImageBorder outsetBorder, ImageBorder insetBorder, ImageMargin margin, ImagePadding padding, int strukLineCount, int patternCount, double randomNoise) : base(textSeeds, width, height, outsetBorder, insetBorder, margin, padding, strukLineCount, patternCount, randomNoise) { }

		#endregion

		#region 静态方法

		/// <summary>
		/// 初始化图形验证码的新实例。
		/// </summary>
		/// <param name="charCount">指定要包含的用于验证的字符串的长度。</param>
		/// <param name="width">指定验证码图像的宽度。</param>
		/// <param name="height">指定验证码图像的高度。</param>
		/// <param name="strukLineCount">指定验证码图像包含的干扰线的数量。</param>
		/// <param name="patternCount">指定验证码图像包含的干扰色块的数量。</param>
		/// <param name="randomNoise">指定验证码图像生成噪点的几率。</param>
		/// <returns>返回图形验证码的新实例。</returns>
		public static RandomizeCodeImage Create(int charCount, int width, int height, int strukLineCount, int patternCount, double randomNoise) { return Create(charCount, width, height, ImageBorder.None, ImageBorder.None, new ImageMargin(0), new ImagePadding(0), strukLineCount, patternCount, randomNoise); }

		/// <summary>
		/// 初始化图形验证码的新实例。
		/// </summary>
		/// <param name="charCount">指定要包含的用于验证的字符串的长度。</param>
		/// <param name="width">指定验证码图像的宽度。</param>
		/// <param name="height">指定验证码图像的高度。</param>
		/// <param name="outsetBorder">指定验证码图像的外部边框。</param>
		/// <param name="strukLineCount">指定验证码图像包含的干扰线的数量。</param>
		/// <param name="patternCount">指定验证码图像包含的干扰色块的数量。</param>
		/// <param name="randomNoise">指定验证码图像生成噪点的几率。</param>
		/// <returns>返回图形验证码的新实例。</returns>
		public static RandomizeCodeImage Create(int charCount, int width, int height, ImageBorder outsetBorder, int strukLineCount, int patternCount, double randomNoise) { return Create(charCount, width, height, outsetBorder, ImageBorder.None, new ImageMargin(0), new ImagePadding(0), strukLineCount, patternCount, randomNoise); }

		/// <summary>
		/// 初始化图形验证码的新实例。
		/// </summary>
		/// <param name="charCount">指定要包含的用于验证的字符串的长度。</param>
		/// <param name="width">指定验证码图像的宽度。</param>
		/// <param name="height">指定验证码图像的高度。</param>
		/// <param name="outsetBorder">指定验证码图像的外部边框。</param>
		/// <param name="insetBorder">指定验证码图像的内部边框。</param>
		/// <param name="margin">指定验证码图像的边距。</param>
		/// <param name="padding">指定验证码图像的补白。</param>
		/// <param name="strukLineCount">指定验证码图像包含的干扰线的数量。</param>
		/// <param name="patternCount">指定验证码图像包含的干扰色块的数量。</param>
		/// <param name="randomNoise">指定验证码图像生成噪点的几率。</param>
		/// <returns>返回图形验证码的新实例。</returns>
		public static RandomizeCodeImage Create(int charCount, int width, int height, ImageBorder outsetBorder, ImageBorder insetBorder, ImageMargin margin, ImagePadding padding, int strukLineCount, int patternCount, double randomNoise)
		{
			var rImage = new RandomizeCodeImage(_TEXT_SEEDS, width, height, outsetBorder, insetBorder, margin, padding, strukLineCount, patternCount, randomNoise) {
				Format = ImageFormat.Png
			};
			rImage.Generate(charCount);

			return rImage;
		}

		#endregion

		#region 受保护方法

		/// <summary>
		/// 绘制验证码图像的背景。
		/// </summary>
		protected override void DrawBackground()
		{
			if (String.IsNullOrEmpty(this.BackgroundImagePath)) this.BackgroundImagePath = PathUtils.MapPath("~/Resources/Images/rImage/");
			if (Directory.Exists(this.BackgroundImagePath))
			{
				// 注意此处扩展名，也可以自行定义
				var images = Directory.GetFiles(this.BackgroundImagePath, "*.jpg", SearchOption.AllDirectories);

				using (var image = Image.FromFile(images[this.Random.Next(0, images.Length)]))
				{
					// 在图像中随机裁剪一块区域
					var rect = new Rectangle(this.Random.Next(0, image.Width - this.Width), this.Random.Next(0, image.Height - this.Height), this.Width, this.Height);

					this.Graphics.DrawImage(image, this.Rectangle, rect, GraphicsUnit.Pixel);
				}
			}
			else this.Graphics.Clear(Color.FromArgb(255, this.Random.Next(216, 255), this.Random.Next(216, 255), this.Random.Next(216, 255)));

			//this.Graphics.DrawString("Htmlbird.Net", new Font("Arial", 6, FontStyle.Italic, GraphicsUnit.Pixel), Brushes.LightGray, this.ContentRect.Left + 3, this.ContentRect.Bottom - 12);
		}

		/// <summary>
		/// 绘制验证码图像的干扰色块。
		/// </summary>
		protected override void DrawPattern()
		{
			if (this.PatternCount == 0) return;

			for (var i = 0; i < this.PatternCount; i++)
			{
				var points = new List<Point>();
				var x = this.Random.Next(1, 10);

				do
				{
					var point = new Point(this.Random.Next(this.ContentRect.Left, this.ContentRect.Right), this.Random.Next(this.ContentRect.Top, this.ContentRect.Bottom));

					if (points.Contains(point) == false) points.Add(point);
				}
				while (points.Count < x);

				this.Graphics.FillClosedCurve(this.GetRandomBrush(Color.FromArgb(this.Random.Next(10, 80), this.RandomColor)), points.ToArray());
			}
		}

		/// <summary>
		/// 绘制验证码图像的噪点。
		/// </summary>
		protected override void DrawRandomNoise()
		{
			if (this.RandomNoise == 0.0) return;

			var x = this.ContentRect.Width + this.ContentRect.Left;
			var y = this.ContentRect.Height + this.ContentRect.Top;
			var t = x * y;

			for (var i = 0; i < t; i++)
			{
				if (this.Random.NextDouble() >= this.RandomNoise) continue;

				var iX = this.Random.Next(this.ContentRect.Left, x);
				var iY = this.Random.Next(this.ContentRect.Top, y);

				this.Bitmap.SetPixel(iX, iY, Color.FromArgb(this.Random.Next(200, 255), this.RandomColor));
			}
		}

		/// <summary>
		/// 绘制验证码图像的文本信息。
		/// </summary>
		protected override void DrawText()
		{
			if (this.CharCount == 0) return;

			var pw = this.ContentRect.Width / 30;
			var charArray = this.Text.ToCharArray();
			var sepDist = (this.ContentRect.Width - pw * 2) / this.CharCount;
			var pf = new PointF(this.ContentRect.Left + pw, 0);

			var x = pf.X;

			foreach (var c in charArray)
			{
				this.FontSize = this.ContentRect.Height * this.Random.Next(45, 85) / 100;

				var font = this.RandomFont;
				var charSize = this.Graphics.MeasureString(c.ToString(), font);
				var lBrush1 = Brushes.DarkGray;
				var lBrush2 = Brushes.LightYellow;
				var lBrush3 = new LinearGradientBrush(this.ContentRect, Color.FromArgb(this.Random.Next(200, 255), this.RandomColor), Color.FromArgb(this.Random.Next(150, 255), this.RandomColor), LinearGradientMode.Horizontal);

				pf.X = x - this.Random.Next(0, sepDist / 3);
				pf.Y = (this.ContentRect.Height - charSize.Height) / 2;

				// 变形
				var mat = new Matrix();
				mat.RotateAt(18 - this.Random.Next(30), pf, MatrixOrder.Append);
				mat.Shear((float)(0.3 - this.Random.Next(20) / 30F), 0, MatrixOrder.Append);

				this.Graphics.Transform = mat;

				this.Graphics.DrawString(c.ToString(), font, lBrush1, PointF.Add(pf, new Size(2, 2)));
				this.Graphics.DrawString(c.ToString(), font, lBrush2, PointF.Add(pf, new Size(1, 1)));
				this.Graphics.DrawString(c.ToString(), font, lBrush3, pf);
				//this.Graphics.DrawRectangle(new Pen(this.RandomColor), pf.X, pf.Y, sepDist, 40);

				mat.Reset();
				this.Graphics.Transform = mat;
				mat.Dispose();

				x += sepDist;
			}
		}

		/// <summary>
		/// 绘制验证码图像的干扰线。
		/// </summary>
		protected override void DrawStrukLine()
		{
			if (this.StrukLineCount == 0) return;

			for (var i = 0; i < this.StrukLineCount; i++)
			{
				var points = new List<Point>();
				var x = this.Random.Next(1, 3) * 3 + 1;

				// 确保节点数是 3n + 1
				do
				{
					var point = new Point(this.Random.Next(this.ContentRect.Left, this.ContentRect.Right), this.Random.Next(this.ContentRect.Top, this.ContentRect.Bottom));

					if (points.Contains(point) == false) points.Add(point);
				}
				while (points.Count < x);

				var p = new Pen(new SolidBrush(Color.FromArgb(this.Random.Next(160, 255), this.RandomColor)), this.Random.Next(1, 4));

				this.Graphics.DrawBeziers(p, points.ToArray());
			}
		}

		#endregion

		#region 公有属性

		/// <summary>
		/// 获取或设置背景图片的路径。
		/// </summary>
		public string BackgroundImagePath { get; set; }

		#endregion
	}
}