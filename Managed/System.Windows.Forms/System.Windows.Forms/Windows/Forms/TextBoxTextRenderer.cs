using System;
using System.Collections;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000315 RID: 789
	internal class TextBoxTextRenderer
	{
		// Token: 0x060034F8 RID: 13560 RVA: 0x000CA35C File Offset: 0x000C855C
		static TextBoxTextRenderer()
		{
			int platform = Environment.OSVersion.Platform;
			if (platform == 4 || platform == 128 || platform == 6)
			{
				TextBoxTextRenderer.use_textrenderer = false;
			}
			else
			{
				TextBoxTextRenderer.use_textrenderer = true;
			}
			TextBoxTextRenderer.max_size = new Size(32767, 32767);
			TextBoxTextRenderer.sf_nonprinting = new StringFormat(StringFormat.GenericTypographic);
			TextBoxTextRenderer.sf_nonprinting.Trimming = 0;
			TextBoxTextRenderer.sf_nonprinting.FormatFlags = 32;
			TextBoxTextRenderer.sf_nonprinting.HotkeyPrefix = 0;
			TextBoxTextRenderer.sf_printing = StringFormat.GenericTypographic;
			TextBoxTextRenderer.sf_printing.HotkeyPrefix = 0;
			TextBoxTextRenderer.measure_cache = new Hashtable();
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x000CA404 File Offset: 0x000C8604
		public static void DrawText(Graphics g, string text, Font font, Color color, float x, float y, bool showNonPrint)
		{
			if (!TextBoxTextRenderer.use_textrenderer)
			{
				if (showNonPrint)
				{
					g.DrawString(text, font, ThemeEngine.Current.ResPool.GetSolidBrush(color), x, y, TextBoxTextRenderer.sf_nonprinting);
				}
				else
				{
					g.DrawString(text, font, ThemeEngine.Current.ResPool.GetSolidBrush(color), x, y, TextBoxTextRenderer.sf_printing);
				}
			}
			else if (showNonPrint)
			{
				TextRenderer.DrawTextInternal(g, text, font, new Rectangle(new Point((int)x, (int)y), TextBoxTextRenderer.max_size), color, TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding, false);
			}
			else
			{
				TextRenderer.DrawTextInternal(g, text, font, new Rectangle(new Point((int)x, (int)y), TextBoxTextRenderer.max_size), color, TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding, false);
			}
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x000CA4C4 File Offset: 0x000C86C4
		public static SizeF MeasureText(Graphics g, string text, Font font)
		{
			if (text.Length == 1)
			{
				string text2 = font.GetHashCode().ToString() + "|" + text;
				if (TextBoxTextRenderer.measure_cache.ContainsKey(text2))
				{
					return (SizeF)TextBoxTextRenderer.measure_cache[text2];
				}
				SizeF sizeF;
				if (!TextBoxTextRenderer.use_textrenderer)
				{
					sizeF = g.MeasureString(text, font, 10000, TextBoxTextRenderer.sf_nonprinting);
				}
				else
				{
					sizeF = TextRenderer.MeasureTextInternal(g, text, font, Size.Empty, TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding, false);
				}
				TextBoxTextRenderer.measure_cache[text2] = sizeF;
				return sizeF;
			}
			else
			{
				if (!TextBoxTextRenderer.use_textrenderer)
				{
					return g.MeasureString(text, font, 10000, TextBoxTextRenderer.sf_nonprinting);
				}
				return TextRenderer.MeasureTextInternal(g, text, font, Size.Empty, TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding, false);
			}
		}

		// Token: 0x040018D9 RID: 6361
		private static Size max_size;

		// Token: 0x040018DA RID: 6362
		private static bool use_textrenderer;

		// Token: 0x040018DB RID: 6363
		private static StringFormat sf_nonprinting;

		// Token: 0x040018DC RID: 6364
		private static StringFormat sf_printing;

		// Token: 0x040018DD RID: 6365
		private static Hashtable measure_cache;
	}
}
