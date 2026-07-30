using System;

namespace System.Drawing
{
	// Token: 0x0200009A RID: 154
	internal struct GdiplusStartupInput
	{
		// Token: 0x06000A01 RID: 2561 RVA: 0x00015B7C File Offset: 0x00013D7C
		internal static GdiplusStartupInput MakeGdiplusStartupInput()
		{
			return new GdiplusStartupInput
			{
				GdiplusVersion = 1U,
				DebugEventCallback = IntPtr.Zero,
				SuppressBackgroundThread = 0,
				SuppressExternalCodecs = 0
			};
		}

		// Token: 0x040005DC RID: 1500
		internal uint GdiplusVersion;

		// Token: 0x040005DD RID: 1501
		internal IntPtr DebugEventCallback;

		// Token: 0x040005DE RID: 1502
		internal int SuppressBackgroundThread;

		// Token: 0x040005DF RID: 1503
		internal int SuppressExternalCodecs;
	}
}
