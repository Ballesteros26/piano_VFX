using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms.Theming.Default;

namespace System.Windows.Forms.Theming
{
	// Token: 0x020004C3 RID: 1219
	internal class ThemeElements
	{
		// Token: 0x06004C24 RID: 19492 RVA: 0x0012F4A8 File Offset: 0x0012D6A8
		static ThemeElements()
		{
			string text = Environment.GetEnvironmentVariable("MONO_THEME");
			if (text == null)
			{
				text = "win32";
			}
			else
			{
				text = text.ToLower();
			}
			ThemeElements.theme = ThemeElements.LoadTheme(text);
		}

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x06004C25 RID: 19493 RVA: 0x0012F4E4 File Offset: 0x0012D6E4
		public static ThemeElementsDefault CurrentTheme
		{
			get
			{
				return ThemeElements.theme;
			}
		}

		// Token: 0x06004C26 RID: 19494 RVA: 0x0012F4EC File Offset: 0x0012D6EC
		private static ThemeElementsDefault LoadTheme(string themeName)
		{
			if (!(themeName == "visualstyles"))
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				string fullName = typeof(ThemeElements).FullName;
				string text = fullName + themeName;
				Type type = executingAssembly.GetType(text, false, true);
				if (type != null)
				{
					object obj = executingAssembly.CreateInstance(type.FullName);
					if (obj != null)
					{
						return (ThemeElementsDefault)obj;
					}
				}
				return new ThemeElementsDefault();
			}
			if (Application.VisualStylesEnabled)
			{
				return new ThemeElementsVisualStyles();
			}
			return new ThemeElementsDefault();
		}

		// Token: 0x06004C27 RID: 19495 RVA: 0x0012F570 File Offset: 0x0012D770
		public static void DrawButton(Graphics g, Rectangle bounds, ButtonThemeState state, Color backColor, Color foreColor)
		{
			ThemeElements.theme.ButtonPainter.Draw(g, bounds, state, backColor, foreColor);
		}

		// Token: 0x06004C28 RID: 19496 RVA: 0x0012F594 File Offset: 0x0012D794
		public static void DrawFlatButton(Graphics g, Rectangle bounds, ButtonThemeState state, Color backColor, Color foreColor, FlatButtonAppearance appearance)
		{
			ThemeElements.theme.ButtonPainter.DrawFlat(g, bounds, state, backColor, foreColor, appearance);
		}

		// Token: 0x06004C29 RID: 19497 RVA: 0x0012F5B8 File Offset: 0x0012D7B8
		public static void DrawPopupButton(Graphics g, Rectangle bounds, ButtonThemeState state, Color backColor, Color foreColor)
		{
			ThemeElements.theme.ButtonPainter.DrawPopup(g, bounds, state, backColor, foreColor);
		}

		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x06004C2A RID: 19498 RVA: 0x0012F5DC File Offset: 0x0012D7DC
		public virtual ButtonPainter ButtonPainter
		{
			get
			{
				return ThemeElements.theme.ButtonPainter;
			}
		}

		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x06004C2B RID: 19499 RVA: 0x0012F5E8 File Offset: 0x0012D7E8
		public static LabelPainter LabelPainter
		{
			get
			{
				return ThemeElements.theme.LabelPainter;
			}
		}

		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x06004C2C RID: 19500 RVA: 0x0012F5F4 File Offset: 0x0012D7F4
		public static LinkLabelPainter LinkLabelPainter
		{
			get
			{
				return ThemeElements.theme.LinkLabelPainter;
			}
		}

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x06004C2D RID: 19501 RVA: 0x0012F600 File Offset: 0x0012D800
		public virtual TabControlPainter TabControlPainter
		{
			get
			{
				return ThemeElements.theme.TabControlPainter;
			}
		}

		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x06004C2E RID: 19502 RVA: 0x0012F60C File Offset: 0x0012D80C
		public virtual CheckBoxPainter CheckBoxPainter
		{
			get
			{
				return ThemeElements.theme.CheckBoxPainter;
			}
		}

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x06004C2F RID: 19503 RVA: 0x0012F618 File Offset: 0x0012D818
		public virtual RadioButtonPainter RadioButtonPainter
		{
			get
			{
				return ThemeElements.theme.RadioButtonPainter;
			}
		}

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x06004C30 RID: 19504 RVA: 0x0012F624 File Offset: 0x0012D824
		public virtual ToolStripPainter ToolStripPainter
		{
			get
			{
				return ThemeElements.theme.ToolStripPainter;
			}
		}

		// Token: 0x040029DF RID: 10719
		private static ThemeElementsDefault theme;
	}
}
