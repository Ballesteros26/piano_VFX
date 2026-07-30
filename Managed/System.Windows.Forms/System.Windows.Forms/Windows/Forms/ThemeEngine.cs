using System;

namespace System.Windows.Forms
{
	// Token: 0x02000329 RID: 809
	internal class ThemeEngine
	{
		// Token: 0x06003710 RID: 14096 RVA: 0x000D3DE0 File Offset: 0x000D1FE0
		static ThemeEngine()
		{
			string text = Environment.GetEnvironmentVariable("MONO_THEME");
			if (text != null)
			{
				text = text.ToLower();
			}
			if (Application.VisualStylesEnabled)
			{
				ThemeEngine.theme = new ThemeVisualStyles();
			}
			else
			{
				ThemeEngine.theme = new ThemeWin32Classic();
			}
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06003711 RID: 14097 RVA: 0x000D3E34 File Offset: 0x000D2034
		public static Theme Current
		{
			get
			{
				return ThemeEngine.theme;
			}
		}

		// Token: 0x04001992 RID: 6546
		private static Theme theme;
	}
}
