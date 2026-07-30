using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to measure and render text. This class cannot be inherited. </summary>
	// Token: 0x02000324 RID: 804
	public sealed class TextRenderer
	{
		// Token: 0x060035AD RID: 13741 RVA: 0x000D27A8 File Offset: 0x000D09A8
		private TextRenderer()
		{
		}

		/// <summary>Draws the specified text at the specified location using the specified device context, font, and color.</summary>
		/// <param name="dc">The device context in which to draw the text.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the drawn text.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x060035AE RID: 13742 RVA: 0x000D27B0 File Offset: 0x000D09B0
		public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor)
		{
			TextRenderer.DrawTextInternal(dc, text, font, pt, foreColor, Color.Transparent, TextFormatFlags.Left, false);
		}

		/// <summary>Draws the specified text within the specified bounds, using the specified device context, font, and color.</summary>
		/// <param name="dc">The device context in which to draw the text.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the text.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x060035AF RID: 13743 RVA: 0x000D27D0 File Offset: 0x000D09D0
		public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor)
		{
			TextRenderer.DrawTextInternal(dc, text, font, bounds, foreColor, Color.Transparent, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter, false);
		}

		/// <summary>Draws the specified text at the specified location, using the specified device context, font, color, and back color.</summary>
		/// <param name="dc">The device context in which to draw the text.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the drawn text.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> to apply to the background area of the drawn text.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x060035B0 RID: 13744 RVA: 0x000D27F0 File Offset: 0x000D09F0
		public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, Color backColor)
		{
			TextRenderer.DrawTextInternal(dc, text, font, pt, foreColor, backColor, TextFormatFlags.Left, false);
		}

		/// <summary>Draws the specified text at the specified location using the specified device context, font, color, and formatting instructions. </summary>
		/// <param name="dc">The device context in which to draw the text.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the drawn text. </param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x060035B1 RID: 13745 RVA: 0x000D280C File Offset: 0x000D0A0C
		public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, TextFormatFlags flags)
		{
			TextRenderer.DrawTextInternal(dc, text, font, pt, foreColor, Color.Transparent, flags, false);
		}

		/// <summary>Draws the specified text within the specified bounds using the specified device context, font, color, and back color.</summary>
		/// <param name="dc">The device context in which to draw the text.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the text.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> to apply to the area represented by <paramref name="bounds" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x060035B2 RID: 13746 RVA: 0x000D282C File Offset: 0x000D0A2C
		public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, Color backColor)
		{
			TextRenderer.DrawTextInternal(dc, text, font, bounds, foreColor, backColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter, false);
		}

		/// <summary>Draws the specified text within the specified bounds using the specified device context, font, color, and formatting instructions.</summary>
		/// <param name="dc">The device context in which to draw the text.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the text.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x060035B3 RID: 13747 RVA: 0x000D2848 File Offset: 0x000D0A48
		public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, TextFormatFlags flags)
		{
			TextRenderer.DrawTextInternal(dc, text, font, bounds, foreColor, Color.Transparent, flags, false);
		}

		/// <summary>Draws the specified text at the specified location using the specified device context, font, color, back color, and formatting instructions </summary>
		/// <param name="dc">The device context in which to draw the text.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the drawn text.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the text.</param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> to apply to the background area of the drawn text.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x060035B4 RID: 13748 RVA: 0x000D2868 File Offset: 0x000D0A68
		public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, Color backColor, TextFormatFlags flags)
		{
			TextRenderer.DrawTextInternal(dc, text, font, pt, foreColor, backColor, flags, false);
		}

		/// <summary>Draws the specified text within the specified bounds using the specified device context, font, color, back color, and formatting instructions.</summary>
		/// <param name="dc">The device context in which to draw the text.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the text.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the text.</param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> to apply to the area represented by <paramref name="bounds" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x060035B5 RID: 13749 RVA: 0x000D2888 File Offset: 0x000D0A88
		public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, Color backColor, TextFormatFlags flags)
		{
			TextRenderer.DrawTextInternal(dc, text, font, bounds, foreColor, backColor, flags, false);
		}

		/// <summary>Provides the size, in pixels, of the specified text when drawn with the specified font.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn on a single line with the specified <paramref name="font" />. You can manipulate how the text is drawn by using one of the <see cref="M:System.Windows.Forms.TextRenderer.DrawText(System.Drawing.IDeviceContext,System.String,System.Drawing.Font,System.Drawing.Rectangle,System.Drawing.Color,System.Windows.Forms.TextFormatFlags)" /> overloads that takes a <see cref="T:System.Windows.Forms.TextFormatFlags" /> parameter. For example, the default behavior of the <see cref="T:System.Windows.Forms.TextRenderer" /> is to add padding to the bounding rectangle of the drawn text to accommodate overhanging glyphs. If you need to draw a line of text without these extra spaces you should use the versions of <see cref="M:System.Windows.Forms.TextRenderer.DrawText(System.Drawing.IDeviceContext,System.String,System.Drawing.Font,System.Drawing.Point,System.Drawing.Color)" /> and <see cref="M:System.Windows.Forms.TextRenderer.MeasureText(System.Drawing.IDeviceContext,System.String,System.Drawing.Font)" /> that take a <see cref="T:System.Drawing.Size" /> and <see cref="T:System.Windows.Forms.TextFormatFlags" /> parameter. For an example, see <see cref="M:System.Windows.Forms.TextRenderer.MeasureText(System.Drawing.IDeviceContext,System.String,System.Drawing.Font,System.Drawing.Size,System.Windows.Forms.TextFormatFlags)" />.</returns>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
		// Token: 0x060035B6 RID: 13750 RVA: 0x000D28A8 File Offset: 0x000D0AA8
		public static Size MeasureText(string text, Font font)
		{
			return TextRenderer.MeasureTextInternal(Hwnd.GraphicsContext, text, font, Size.Empty, TextFormatFlags.Left, false);
		}

		/// <summary>Provides the size, in pixels, of the specified text drawn with the specified font in the specified device context.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn in a single line with the specified <paramref name="font" /> in the specified device context.</returns>
		/// <param name="dc">The device context in which to measure the text.</param>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
		// Token: 0x060035B7 RID: 13751 RVA: 0x000D28C0 File Offset: 0x000D0AC0
		public static Size MeasureText(IDeviceContext dc, string text, Font font)
		{
			return TextRenderer.MeasureTextInternal(dc, text, font, Size.Empty, TextFormatFlags.Left, false);
		}

		/// <summary>Provides the size, in pixels, of the specified text when drawn with the specified font, using the specified size to create an initial bounding rectangle.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn with the specified <paramref name="font" />.</returns>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
		/// <param name="proposedSize">The <see cref="T:System.Drawing.Size" /> of the initial bounding rectangle.</param>
		// Token: 0x060035B8 RID: 13752 RVA: 0x000D28D4 File Offset: 0x000D0AD4
		public static Size MeasureText(string text, Font font, Size proposedSize)
		{
			return TextRenderer.MeasureTextInternal(Hwnd.GraphicsContext, text, font, proposedSize, TextFormatFlags.Left, false);
		}

		/// <summary>Provides the size, in pixels, of the specified text when drawn with the specified font in the specified device context, using the specified size to create an initial bounding rectangle for the text.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn with the specified <paramref name="font" />.</returns>
		/// <param name="dc">The device context in which to measure the text.</param>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
		/// <param name="proposedSize">The <see cref="T:System.Drawing.Size" /> of the initial bounding rectangle.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x060035B9 RID: 13753 RVA: 0x000D28E8 File Offset: 0x000D0AE8
		public static Size MeasureText(IDeviceContext dc, string text, Font font, Size proposedSize)
		{
			return TextRenderer.MeasureTextInternal(dc, text, font, proposedSize, TextFormatFlags.Left, false);
		}

		/// <summary>Provides the size, in pixels, of the specified text when drawn with the specified font and formatting instructions, using the specified size to create the initial bounding rectangle for the text.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn with the specified <paramref name="font" /> and format.</returns>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
		/// <param name="proposedSize">The <see cref="T:System.Drawing.Size" /> of the initial bounding rectangle.</param>
		/// <param name="flags">The formatting instructions to apply to the measured text.</param>
		// Token: 0x060035BA RID: 13754 RVA: 0x000D28F8 File Offset: 0x000D0AF8
		public static Size MeasureText(string text, Font font, Size proposedSize, TextFormatFlags flags)
		{
			return TextRenderer.MeasureTextInternal(Hwnd.GraphicsContext, text, font, proposedSize, flags, false);
		}

		/// <summary>Provides the size, in pixels, of the specified text when drawn with the specified device context, font, and formatting instructions, using the specified size to create the initial bounding rectangle for the text.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn with the specified <paramref name="font" /> and format.</returns>
		/// <param name="dc">The device context in which to measure the text.</param>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
		/// <param name="proposedSize">The <see cref="T:System.Drawing.Size" /> of the initial bounding rectangle.</param>
		/// <param name="flags">The formatting instructions to apply to the measured text.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is null.</exception>
		// Token: 0x060035BB RID: 13755 RVA: 0x000D290C File Offset: 0x000D0B0C
		public static Size MeasureText(IDeviceContext dc, string text, Font font, Size proposedSize, TextFormatFlags flags)
		{
			return TextRenderer.MeasureTextInternal(dc, text, font, proposedSize, flags, false);
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x000D291C File Offset: 0x000D0B1C
		internal static void DrawTextInternal(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, Color backColor, TextFormatFlags flags, bool useDrawString)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (text == null || text.Length == 0)
			{
				return;
			}
			if (!useDrawString && !XplatUI.RunningOnUnix)
			{
				if ((flags & TextFormatFlags.VerticalCenter) == TextFormatFlags.VerticalCenter || (flags & TextFormatFlags.Bottom) == TextFormatFlags.Bottom)
				{
					flags |= TextFormatFlags.SingleLine;
				}
				Rectangle rectangle = TextRenderer.PadRectangle(bounds, flags);
				rectangle.Offset((int)(dc as Graphics).Transform.OffsetX, (int)(dc as Graphics).Transform.OffsetY);
				IntPtr intPtr = IntPtr.Zero;
				bool flag = false;
				if ((flags & TextFormatFlags.PreserveGraphicsClipping) == TextFormatFlags.PreserveGraphicsClipping)
				{
					Graphics graphics = (Graphics)dc;
					Region clip = graphics.Clip;
					if (!clip.IsInfinite(graphics))
					{
						IntPtr hrgn = clip.GetHrgn(graphics);
						intPtr = dc.GetHdc();
						TextRenderer.SelectClipRgn(intPtr, hrgn);
						TextRenderer.DeleteObject(hrgn);
						flag = true;
					}
				}
				if (intPtr == IntPtr.Zero)
				{
					intPtr = dc.GetHdc();
				}
				if (foreColor != Color.Empty)
				{
					TextRenderer.SetTextColor(intPtr, ColorTranslator.ToWin32(foreColor));
				}
				if (backColor != Color.Transparent && backColor != Color.Empty)
				{
					TextRenderer.SetBkMode(intPtr, 2);
					TextRenderer.SetBkColor(intPtr, ColorTranslator.ToWin32(backColor));
				}
				else
				{
					TextRenderer.SetBkMode(intPtr, 1);
				}
				XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(rectangle);
				if (font != null)
				{
					IntPtr intPtr2 = TextRenderer.SelectObject(intPtr, font.ToHfont());
					TextRenderer.Win32DrawText(intPtr, text, text.Length, ref rect, (int)flags);
					intPtr2 = TextRenderer.SelectObject(intPtr, intPtr2);
					TextRenderer.DeleteObject(intPtr2);
				}
				else
				{
					TextRenderer.Win32DrawText(intPtr, text, text.Length, ref rect, (int)flags);
				}
				if (flag)
				{
					TextRenderer.SelectClipRgn(intPtr, IntPtr.Zero);
				}
				dc.ReleaseHdc();
			}
			else
			{
				IntPtr intPtr3 = IntPtr.Zero;
				Graphics graphics2;
				if (dc is Graphics)
				{
					graphics2 = (Graphics)dc;
				}
				else
				{
					intPtr3 = dc.GetHdc();
					graphics2 = Graphics.FromHdc(intPtr3);
				}
				StringFormat stringFormat = TextRenderer.FlagsToStringFormat(flags);
				Rectangle rectangle2 = TextRenderer.PadDrawStringRectangle(bounds, flags);
				graphics2.DrawString(text, font, ThemeEngine.Current.ResPool.GetSolidBrush(foreColor), rectangle2, stringFormat);
				if (!(dc is Graphics))
				{
					graphics2.Dispose();
					dc.ReleaseHdc();
				}
			}
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000D2B74 File Offset: 0x000D0D74
		internal static Size MeasureTextInternal(IDeviceContext dc, string text, Font font, Size proposedSize, TextFormatFlags flags, bool useMeasureString)
		{
			if (!useMeasureString && !XplatUI.RunningOnUnix)
			{
				flags |= (TextFormatFlags)1024;
				IntPtr hdc = dc.GetHdc();
				XplatUIWin32.RECT rect = XplatUIWin32.RECT.FromRectangle(new Rectangle(Point.Empty, proposedSize));
				if (font != null)
				{
					IntPtr intPtr = TextRenderer.SelectObject(hdc, font.ToHfont());
					TextRenderer.Win32DrawText(hdc, text, text.Length, ref rect, (int)flags);
					intPtr = TextRenderer.SelectObject(hdc, intPtr);
					TextRenderer.DeleteObject(intPtr);
				}
				else
				{
					TextRenderer.Win32DrawText(hdc, text, text.Length, ref rect, (int)flags);
				}
				dc.ReleaseHdc();
				Size size = rect.ToRectangle().Size;
				if (size.Width > 0 && (flags & TextFormatFlags.NoPadding) == TextFormatFlags.Left)
				{
					size.Width += 6;
					size.Width += size.Height / 8;
				}
				return size;
			}
			StringFormat stringFormat = TextRenderer.FlagsToStringFormat(flags);
			Size size2;
			if (dc is Graphics)
			{
				size2 = (dc as Graphics).MeasureString(text, font, (proposedSize.Width != 0) ? proposedSize.Width : int.MaxValue, stringFormat).ToSize();
			}
			else
			{
				size2 = TextRenderer.MeasureString(text, font, (proposedSize.Width != 0) ? proposedSize.Width : int.MaxValue, stringFormat).ToSize();
			}
			if (size2.Width > 0 && (flags & TextFormatFlags.NoPadding) == TextFormatFlags.Left)
			{
				size2.Width += 9;
			}
			return size2;
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x000D2D08 File Offset: 0x000D0F08
		internal static void DrawTextInternal(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, bool useDrawString)
		{
			TextRenderer.DrawTextInternal(dc, text, font, pt, foreColor, Color.Transparent, TextFormatFlags.Left, useDrawString);
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x000D2D28 File Offset: 0x000D0F28
		internal static void DrawTextInternal(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, bool useDrawString)
		{
			TextRenderer.DrawTextInternal(dc, text, font, bounds, foreColor, Color.Transparent, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter, useDrawString);
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x000D2D48 File Offset: 0x000D0F48
		internal static void DrawTextInternal(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, Color backColor, bool useDrawString)
		{
			TextRenderer.DrawTextInternal(dc, text, font, pt, foreColor, backColor, TextFormatFlags.Left, useDrawString);
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000D2D68 File Offset: 0x000D0F68
		internal static void DrawTextInternal(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, TextFormatFlags flags, bool useDrawString)
		{
			TextRenderer.DrawTextInternal(dc, text, font, pt, foreColor, Color.Transparent, flags, useDrawString);
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000D2D8C File Offset: 0x000D0F8C
		internal static void DrawTextInternal(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, Color backColor, bool useDrawString)
		{
			TextRenderer.DrawTextInternal(dc, text, font, bounds, foreColor, backColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter, useDrawString);
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000D2DAC File Offset: 0x000D0FAC
		internal static void DrawTextInternal(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, TextFormatFlags flags, bool useDrawString)
		{
			TextRenderer.DrawTextInternal(dc, text, font, bounds, foreColor, Color.Transparent, flags, useDrawString);
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000D2DD0 File Offset: 0x000D0FD0
		internal static Size MeasureTextInternal(string text, Font font, bool useMeasureString)
		{
			return TextRenderer.MeasureTextInternal(Hwnd.GraphicsContext, text, font, Size.Empty, TextFormatFlags.Left, useMeasureString);
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000D2DE8 File Offset: 0x000D0FE8
		internal static void DrawTextInternal(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, Color backColor, TextFormatFlags flags, bool useDrawString)
		{
			Size size = TextRenderer.MeasureTextInternal(dc, text, font, useDrawString);
			TextRenderer.DrawTextInternal(dc, text, font, new Rectangle(pt, size), foreColor, backColor, flags, useDrawString);
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000D2E18 File Offset: 0x000D1018
		internal static Size MeasureTextInternal(IDeviceContext dc, string text, Font font, bool useMeasureString)
		{
			return TextRenderer.MeasureTextInternal(dc, text, font, Size.Empty, TextFormatFlags.Left, useMeasureString);
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x000D2E2C File Offset: 0x000D102C
		internal static Size MeasureTextInternal(string text, Font font, Size proposedSize, bool useMeasureString)
		{
			return TextRenderer.MeasureTextInternal(Hwnd.GraphicsContext, text, font, proposedSize, TextFormatFlags.Left, useMeasureString);
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000D2E40 File Offset: 0x000D1040
		internal static Size MeasureTextInternal(IDeviceContext dc, string text, Font font, Size proposedSize, bool useMeasureString)
		{
			return TextRenderer.MeasureTextInternal(dc, text, font, proposedSize, TextFormatFlags.Left, useMeasureString);
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000D2E50 File Offset: 0x000D1050
		internal static Size MeasureTextInternal(string text, Font font, Size proposedSize, TextFormatFlags flags, bool useMeasureString)
		{
			return TextRenderer.MeasureTextInternal(Hwnd.GraphicsContext, text, font, proposedSize, flags, useMeasureString);
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000D2E64 File Offset: 0x000D1064
		internal static SizeF MeasureString(string text, Font font)
		{
			return Hwnd.GraphicsContext.MeasureString(text, font);
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x000D2E74 File Offset: 0x000D1074
		internal static SizeF MeasureString(string text, Font font, int width)
		{
			return Hwnd.GraphicsContext.MeasureString(text, font, width);
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x000D2E84 File Offset: 0x000D1084
		internal static SizeF MeasureString(string text, Font font, SizeF layoutArea)
		{
			return Hwnd.GraphicsContext.MeasureString(text, font, layoutArea);
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x000D2E94 File Offset: 0x000D1094
		internal static SizeF MeasureString(string text, Font font, int width, StringFormat format)
		{
			return Hwnd.GraphicsContext.MeasureString(text, font, width, format);
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x000D2EA4 File Offset: 0x000D10A4
		internal static SizeF MeasureString(string text, Font font, PointF origin, StringFormat stringFormat)
		{
			return Hwnd.GraphicsContext.MeasureString(text, font, origin, stringFormat);
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x000D2EB4 File Offset: 0x000D10B4
		internal static SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat)
		{
			return Hwnd.GraphicsContext.MeasureString(text, font, layoutArea, stringFormat);
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x000D2EC4 File Offset: 0x000D10C4
		internal static SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat, out int charactersFitted, out int linesFilled)
		{
			return Hwnd.GraphicsContext.MeasureString(text, font, layoutArea, stringFormat, ref charactersFitted, ref linesFilled);
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x000D2EE4 File Offset: 0x000D10E4
		internal static Region[] MeasureCharacterRanges(string text, Font font, RectangleF layoutRect, StringFormat stringFormat)
		{
			return Hwnd.GraphicsContext.MeasureCharacterRanges(text, font, layoutRect, stringFormat);
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x000D2EF4 File Offset: 0x000D10F4
		internal static SizeF GetDpi()
		{
			return new SizeF(Hwnd.GraphicsContext.DpiX, Hwnd.GraphicsContext.DpiY);
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x000D2F1C File Offset: 0x000D111C
		private static StringFormat FlagsToStringFormat(TextFormatFlags flags)
		{
			StringFormat stringFormat = new StringFormat();
			if ((flags & TextFormatFlags.HorizontalCenter) == TextFormatFlags.HorizontalCenter)
			{
				stringFormat.Alignment = 1;
			}
			else if ((flags & TextFormatFlags.Right) == TextFormatFlags.Right)
			{
				stringFormat.Alignment = 2;
			}
			else
			{
				stringFormat.Alignment = 0;
			}
			if ((flags & TextFormatFlags.Bottom) == TextFormatFlags.Bottom)
			{
				stringFormat.LineAlignment = 2;
			}
			else if ((flags & TextFormatFlags.VerticalCenter) == TextFormatFlags.VerticalCenter)
			{
				stringFormat.LineAlignment = 1;
			}
			else
			{
				stringFormat.LineAlignment = 0;
			}
			if ((flags & TextFormatFlags.EndEllipsis) == TextFormatFlags.EndEllipsis)
			{
				stringFormat.Trimming = 3;
			}
			else if ((flags & TextFormatFlags.PathEllipsis) == TextFormatFlags.PathEllipsis)
			{
				stringFormat.Trimming = 5;
			}
			else if ((flags & TextFormatFlags.WordEllipsis) == TextFormatFlags.WordEllipsis)
			{
				stringFormat.Trimming = 4;
			}
			else
			{
				stringFormat.Trimming = 1;
			}
			if ((flags & TextFormatFlags.NoPrefix) == TextFormatFlags.NoPrefix)
			{
				stringFormat.HotkeyPrefix = 0;
			}
			else if ((flags & TextFormatFlags.HidePrefix) == TextFormatFlags.HidePrefix)
			{
				stringFormat.HotkeyPrefix = 2;
			}
			else
			{
				stringFormat.HotkeyPrefix = 1;
			}
			if ((flags & TextFormatFlags.NoPadding) == TextFormatFlags.NoPadding)
			{
				stringFormat.FormatFlags |= 4;
			}
			if ((flags & TextFormatFlags.SingleLine) == TextFormatFlags.SingleLine)
			{
				stringFormat.FormatFlags |= 4096;
			}
			else if ((flags & TextFormatFlags.TextBoxControl) == TextFormatFlags.TextBoxControl)
			{
				stringFormat.FormatFlags |= 8192;
			}
			if ((flags & TextFormatFlags.NoClipping) == TextFormatFlags.NoClipping)
			{
				stringFormat.FormatFlags |= 16384;
			}
			return stringFormat;
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000D30B8 File Offset: 0x000D12B8
		private static Rectangle PadRectangle(Rectangle r, TextFormatFlags flags)
		{
			if ((flags & TextFormatFlags.NoPadding) == TextFormatFlags.Left && (flags & TextFormatFlags.Right) == TextFormatFlags.Left && (flags & TextFormatFlags.HorizontalCenter) == TextFormatFlags.Left)
			{
				r.X += 3;
				r.Width -= 3;
			}
			if ((flags & TextFormatFlags.NoPadding) == TextFormatFlags.Left && (flags & TextFormatFlags.Right) == TextFormatFlags.Right)
			{
				r.Width -= 4;
			}
			if ((flags & TextFormatFlags.LeftAndRightPadding) == TextFormatFlags.LeftAndRightPadding)
			{
				r.X += 2;
				r.Width -= 2;
			}
			if ((flags & TextFormatFlags.WordEllipsis) == TextFormatFlags.WordEllipsis || (flags & TextFormatFlags.EndEllipsis) == TextFormatFlags.EndEllipsis || (flags & TextFormatFlags.WordBreak) == TextFormatFlags.WordBreak)
			{
				r.Width -= 4;
			}
			if ((flags & TextFormatFlags.VerticalCenter) == TextFormatFlags.VerticalCenter)
			{
				r.Y++;
			}
			return r;
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x000D31A8 File Offset: 0x000D13A8
		private static Rectangle PadDrawStringRectangle(Rectangle r, TextFormatFlags flags)
		{
			if ((flags & TextFormatFlags.NoPadding) == TextFormatFlags.Left && (flags & TextFormatFlags.Right) == TextFormatFlags.Left && (flags & TextFormatFlags.HorizontalCenter) == TextFormatFlags.Left)
			{
				r.X++;
				r.Width--;
			}
			if ((flags & TextFormatFlags.NoPadding) == TextFormatFlags.Left && (flags & TextFormatFlags.Right) == TextFormatFlags.Right)
			{
				r.Width -= 4;
			}
			if ((flags & TextFormatFlags.NoPadding) == TextFormatFlags.NoPadding)
			{
				r.X -= 2;
			}
			if ((flags & TextFormatFlags.NoPadding) == TextFormatFlags.Left && (flags & TextFormatFlags.Bottom) == TextFormatFlags.Bottom)
			{
				r.Y++;
			}
			if ((flags & TextFormatFlags.LeftAndRightPadding) == TextFormatFlags.LeftAndRightPadding)
			{
				r.X += 2;
				r.Width -= 2;
			}
			if ((flags & TextFormatFlags.WordEllipsis) == TextFormatFlags.WordEllipsis || (flags & TextFormatFlags.EndEllipsis) == TextFormatFlags.EndEllipsis || (flags & TextFormatFlags.WordBreak) == TextFormatFlags.WordBreak)
			{
				r.Width -= 4;
			}
			if ((flags & TextFormatFlags.VerticalCenter) == TextFormatFlags.VerticalCenter)
			{
				r.Y++;
			}
			return r;
		}

		// Token: 0x060035D6 RID: 13782
		[DllImport("user32", CharSet = 3, EntryPoint = "DrawText")]
		private static extern int Win32DrawText(IntPtr hdc, string lpStr, int nCount, ref XplatUIWin32.RECT lpRect, int wFormat);

		// Token: 0x060035D7 RID: 13783
		[DllImport("gdi32")]
		private static extern int SetTextColor(IntPtr hdc, int crColor);

		// Token: 0x060035D8 RID: 13784
		[DllImport("gdi32")]
		private static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

		// Token: 0x060035D9 RID: 13785
		[DllImport("gdi32")]
		private static extern int SetBkColor(IntPtr hdc, int crColor);

		// Token: 0x060035DA RID: 13786
		[DllImport("gdi32")]
		private static extern int SetBkMode(IntPtr hdc, int iBkMode);

		// Token: 0x060035DB RID: 13787
		[DllImport("gdi32")]
		private static extern bool DeleteObject(IntPtr objectHandle);

		// Token: 0x060035DC RID: 13788
		[DllImport("gdi32")]
		private static extern bool SelectClipRgn(IntPtr hdc, IntPtr hrgn);
	}
}
