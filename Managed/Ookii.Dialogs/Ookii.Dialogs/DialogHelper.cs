using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Ookii.Dialogs
{
	// Token: 0x02000008 RID: 8
	internal static class DialogHelper
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000318C File Offset: 0x0000138C
		public static bool IsTaskDialogThemeSupported
		{
			get
			{
				return NativeMethods.IsWindowsVistaOrLater && VisualStyleRenderer.IsSupported && Application.RenderWithVisualStyles;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000031B4 File Offset: 0x000013B4
		public static int GetTextHeight(IDeviceContext dc, string mainInstruction, string content, Font mainInstructionFallbackFont, Font contentFallbackFont, int width)
		{
			Point empty = Point.Empty;
			DialogHelper.DrawText(dc, mainInstruction, content, ref empty, mainInstructionFallbackFont, contentFallbackFont, true, width);
			return empty.Y;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000031E4 File Offset: 0x000013E4
		public static Size SizeDialog(IDeviceContext dc, string mainInstruction, string content, Screen screen, Font mainInstructionFallbackFont, Font contentFallbackFont, int horizontalSpacing, int verticalSpacing, int minimumWidth, int textMinimumHeight)
		{
			int num = minimumWidth - horizontalSpacing;
			int i;
			for (i = DialogHelper.GetTextHeight(dc, mainInstruction, content, mainInstructionFallbackFont, contentFallbackFont, num); i > num; i = DialogHelper.GetTextHeight(dc, mainInstruction, content, mainInstructionFallbackFont, contentFallbackFont, num))
			{
				int num2 = i * num;
				num = (int)(Math.Sqrt((double)num2) * 1.1);
			}
			bool flag = i < textMinimumHeight;
			if (flag)
			{
				i = textMinimumHeight;
			}
			int num3 = num + horizontalSpacing;
			int num4 = i + verticalSpacing;
			Rectangle workingArea = screen.WorkingArea;
			bool flag2 = (double)num4 > 0.9 * (double)workingArea.Height;
			if (flag2)
			{
				int num5 = i * num;
				num4 = (int)(0.9 * (double)workingArea.Height);
				i = num4 - verticalSpacing;
				num = num5 / i;
				num3 = num + horizontalSpacing;
			}
			bool flag3 = (double)num3 > 0.9 * (double)workingArea.Width;
			if (flag3)
			{
				num3 = (int)(0.9 * (double)workingArea.Width);
			}
			return new Size(num3, num4);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000032E0 File Offset: 0x000014E0
		public static void DrawText(IDeviceContext dc, string text, VisualStyleElement element, Font fallbackFont, ref Point location, bool measureOnly, int width)
		{
			Rectangle rectangle;
			rectangle..ctor(location.X, location.Y, width, NativeMethods.IsWindowsXPOrLater ? int.MaxValue : 100000);
			TextFormatFlags textFormatFlags = TextFormatFlags.WordBreak;
			bool isTaskDialogThemeSupported = DialogHelper.IsTaskDialogThemeSupported;
			if (isTaskDialogThemeSupported)
			{
				VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(element);
				Rectangle textExtent = visualStyleRenderer.GetTextExtent(dc, rectangle, text, textFormatFlags);
				location += new Size(0, textExtent.Height);
				bool flag = !measureOnly;
				if (flag)
				{
					visualStyleRenderer.DrawText(dc, textExtent, text, false, textFormatFlags);
				}
			}
			else
			{
				bool flag2 = !measureOnly;
				if (flag2)
				{
					TextRenderer.DrawText(dc, text, fallbackFont, rectangle, SystemColors.WindowText, textFormatFlags);
				}
				Size size = TextRenderer.MeasureText(dc, text, fallbackFont, new Size(rectangle.Width, rectangle.Height), textFormatFlags);
				location += new Size(0, size.Height);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000033D0 File Offset: 0x000015D0
		public static void DrawText(IDeviceContext dc, string mainInstruction, string content, ref Point location, Font mainInstructionFallbackFont, Font contentFallbackFont, bool measureOnly, int width)
		{
			bool flag = !string.IsNullOrEmpty(mainInstruction);
			if (flag)
			{
				DialogHelper.DrawText(dc, mainInstruction, AdditionalVisualStyleElements.TextStyle.MainInstruction, mainInstructionFallbackFont, ref location, measureOnly, width);
			}
			bool flag2 = !string.IsNullOrEmpty(content);
			if (flag2)
			{
				bool flag3 = !string.IsNullOrEmpty(mainInstruction);
				if (flag3)
				{
					content = Environment.NewLine + content;
				}
				DialogHelper.DrawText(dc, content, AdditionalVisualStyleElements.TextStyle.BodyText, contentFallbackFont, ref location, measureOnly, width);
			}
		}
	}
}
