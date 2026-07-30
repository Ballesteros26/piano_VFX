using System;

namespace System.Windows.Forms.VisualStyles
{
	// Token: 0x02000629 RID: 1577
	internal class VisualStylesEngine
	{
		// Token: 0x17001553 RID: 5459
		// (get) Token: 0x06005001 RID: 20481 RVA: 0x0013843C File Offset: 0x0013663C
		public static IVisualStyles Instance
		{
			get
			{
				return VisualStylesEngine.instance;
			}
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x00138444 File Offset: 0x00136644
		private static IVisualStyles Initialize()
		{
			string text = Environment.GetEnvironmentVariable("MONO_VISUAL_STYLES");
			if (text != null)
			{
				text = text.ToLower();
			}
			if (text == "gtkplus" && VisualStylesGtkPlus.Initialize())
			{
				return new VisualStylesGtkPlus();
			}
			return new VisualStylesNative();
		}

		// Token: 0x04002D47 RID: 11591
		private static IVisualStyles instance = VisualStylesEngine.Initialize();
	}
}
