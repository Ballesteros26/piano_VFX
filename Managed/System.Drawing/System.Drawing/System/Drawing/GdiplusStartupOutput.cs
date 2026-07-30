using System;

namespace System.Drawing
{
	// Token: 0x0200009B RID: 155
	internal struct GdiplusStartupOutput
	{
		// Token: 0x06000A02 RID: 2562 RVA: 0x00015BB8 File Offset: 0x00013DB8
		internal static GdiplusStartupOutput MakeGdiplusStartupOutput()
		{
			GdiplusStartupOutput gdiplusStartupOutput = default(GdiplusStartupOutput);
			gdiplusStartupOutput.NotificationHook = (gdiplusStartupOutput.NotificationUnhook = IntPtr.Zero);
			return gdiplusStartupOutput;
		}

		// Token: 0x040005E0 RID: 1504
		internal IntPtr NotificationHook;

		// Token: 0x040005E1 RID: 1505
		internal IntPtr NotificationUnhook;
	}
}
