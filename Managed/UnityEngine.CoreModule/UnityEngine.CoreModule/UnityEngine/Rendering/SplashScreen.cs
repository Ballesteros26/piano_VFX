using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x02000350 RID: 848
	[NativeHeader("Runtime/Graphics/DrawSplashScreenAndWatermarks.h")]
	public class SplashScreen
	{
		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001CEE RID: 7406
		public static extern bool isFinished
		{
			[FreeFunction("IsSplashScreenFinished")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001CEF RID: 7407
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern void CancelSplashScreen();

		// Token: 0x06001CF0 RID: 7408
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern void BeginSplashScreenFade();

		// Token: 0x06001CF1 RID: 7409
		[FreeFunction("BeginSplashScreen_Binding")]
		[MethodImpl(4096)]
		public static extern void Begin();

		// Token: 0x06001CF2 RID: 7410 RVA: 0x0002F6CC File Offset: 0x0002D8CC
		public static void Stop(SplashScreen.StopBehavior stopBehavior)
		{
			bool flag = stopBehavior == SplashScreen.StopBehavior.FadeOut;
			if (flag)
			{
				SplashScreen.BeginSplashScreenFade();
			}
			else
			{
				SplashScreen.CancelSplashScreen();
			}
		}

		// Token: 0x06001CF3 RID: 7411
		[FreeFunction("DrawSplashScreen_Binding")]
		[MethodImpl(4096)]
		public static extern void Draw();

		// Token: 0x06001CF4 RID: 7412
		[FreeFunction("SetSplashScreenTime")]
		[MethodImpl(4096)]
		internal static extern void SetTime(float time);

		// Token: 0x02000351 RID: 849
		public enum StopBehavior
		{
			// Token: 0x040009FE RID: 2558
			StopImmediate,
			// Token: 0x040009FF RID: 2559
			FadeOut
		}
	}
}
