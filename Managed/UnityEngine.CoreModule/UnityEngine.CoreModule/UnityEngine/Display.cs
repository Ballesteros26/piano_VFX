using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000D6 RID: 214
	[NativeHeader("Runtime/Graphics/DisplayManager.h")]
	[UsedByNativeCode]
	public class Display
	{
		// Token: 0x06000625 RID: 1573 RVA: 0x00009F52 File Offset: 0x00008152
		internal Display()
		{
			this.nativeDisplay = new IntPtr(0);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00009F68 File Offset: 0x00008168
		internal Display(IntPtr nativeDisplay)
		{
			this.nativeDisplay = nativeDisplay;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x00009F7C File Offset: 0x0000817C
		public int renderingWidth
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out num, out num2);
				return num;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00009FA4 File Offset: 0x000081A4
		public int renderingHeight
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetRenderingExtImpl(this.nativeDisplay, out num, out num2);
				return num2;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x00009FCC File Offset: 0x000081CC
		public int systemWidth
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out num, out num2);
				return num;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x00009FF4 File Offset: 0x000081F4
		public int systemHeight
		{
			get
			{
				int num = 0;
				int num2 = 0;
				Display.GetSystemExtImpl(this.nativeDisplay, out num, out num2);
				return num2;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0000A01C File Offset: 0x0000821C
		public RenderBuffer colorBuffer
		{
			get
			{
				RenderBuffer renderBuffer;
				RenderBuffer renderBuffer2;
				Display.GetRenderingBuffersImpl(this.nativeDisplay, out renderBuffer, out renderBuffer2);
				return renderBuffer;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0000A040 File Offset: 0x00008240
		public RenderBuffer depthBuffer
		{
			get
			{
				RenderBuffer renderBuffer;
				RenderBuffer renderBuffer2;
				Display.GetRenderingBuffersImpl(this.nativeDisplay, out renderBuffer, out renderBuffer2);
				return renderBuffer2;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0000A064 File Offset: 0x00008264
		public bool active
		{
			get
			{
				return Display.GetActiveImpl(this.nativeDisplay);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0000A084 File Offset: 0x00008284
		public bool requiresBlitToBackbuffer
		{
			get
			{
				int num = this.nativeDisplay.ToInt32();
				bool flag = num < HDROutputSettings.displays.Length;
				if (flag)
				{
					bool flag2 = HDROutputSettings.displays[num].available && HDROutputSettings.displays[num].active;
					bool flag3 = flag2;
					if (flag3)
					{
						return true;
					}
				}
				return Display.RequiresBlitToBackbufferImpl(this.nativeDisplay);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0000A0E8 File Offset: 0x000082E8
		public bool requiresSrgbBlitToBackbuffer
		{
			get
			{
				return Display.RequiresSrgbBlitToBackbufferImpl(this.nativeDisplay);
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0000A105 File Offset: 0x00008305
		public void Activate()
		{
			Display.ActivateDisplayImpl(this.nativeDisplay, 0, 0, 60);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0000A118 File Offset: 0x00008318
		public void Activate(int width, int height, int refreshRate)
		{
			Display.ActivateDisplayImpl(this.nativeDisplay, width, height, refreshRate);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0000A12A File Offset: 0x0000832A
		public void SetParams(int width, int height, int x, int y)
		{
			Display.SetParamsImpl(this.nativeDisplay, width, height, x, y);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0000A13E File Offset: 0x0000833E
		public void SetRenderingResolution(int w, int h)
		{
			Display.SetRenderingResolutionImpl(this.nativeDisplay, w, h);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0000A150 File Offset: 0x00008350
		[Obsolete("MultiDisplayLicense has been deprecated.", false)]
		public static bool MultiDisplayLicense()
		{
			return true;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0000A164 File Offset: 0x00008364
		public static Vector3 RelativeMouseAt(Vector3 inputMouseCoordinates)
		{
			int num = 0;
			int num2 = 0;
			int num3 = (int)inputMouseCoordinates.x;
			int num4 = (int)inputMouseCoordinates.y;
			Vector3 vector;
			vector.z = (float)Display.RelativeMouseAtImpl(num3, num4, out num, out num2);
			vector.x = (float)num;
			vector.y = (float)num2;
			return vector;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0000A1B4 File Offset: 0x000083B4
		public static Display main
		{
			get
			{
				return Display._mainDisplay;
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0000A1CC File Offset: 0x000083CC
		[RequiredByNativeCode]
		private static void RecreateDisplayList(IntPtr[] nativeDisplay)
		{
			bool flag = nativeDisplay.Length == 0;
			if (!flag)
			{
				Display.displays = new Display[nativeDisplay.Length];
				for (int i = 0; i < nativeDisplay.Length; i++)
				{
					Display.displays[i] = new Display(nativeDisplay[i]);
				}
				Display._mainDisplay = Display.displays[0];
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0000A220 File Offset: 0x00008420
		[RequiredByNativeCode]
		private static void FireDisplaysUpdated()
		{
			bool flag = Display.onDisplaysUpdated != null;
			if (flag)
			{
				Display.onDisplaysUpdated();
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000639 RID: 1593 RVA: 0x0000A248 File Offset: 0x00008448
		// (remove) Token: 0x0600063A RID: 1594 RVA: 0x0000A27C File Offset: 0x0000847C
		[field: DebuggerBrowsable(0)]
		public static event Display.DisplaysUpdatedDelegate onDisplaysUpdated;

		// Token: 0x0600063B RID: 1595
		[FreeFunction("UnityDisplayManager_DisplaySystemResolution")]
		[MethodImpl(4096)]
		private static extern void GetSystemExtImpl(IntPtr nativeDisplay, out int w, out int h);

		// Token: 0x0600063C RID: 1596
		[FreeFunction("UnityDisplayManager_DisplayRenderingResolution")]
		[MethodImpl(4096)]
		private static extern void GetRenderingExtImpl(IntPtr nativeDisplay, out int w, out int h);

		// Token: 0x0600063D RID: 1597
		[FreeFunction("UnityDisplayManager_GetRenderingBuffersWrapper")]
		[MethodImpl(4096)]
		private static extern void GetRenderingBuffersImpl(IntPtr nativeDisplay, out RenderBuffer color, out RenderBuffer depth);

		// Token: 0x0600063E RID: 1598
		[FreeFunction("UnityDisplayManager_SetRenderingResolution")]
		[MethodImpl(4096)]
		private static extern void SetRenderingResolutionImpl(IntPtr nativeDisplay, int w, int h);

		// Token: 0x0600063F RID: 1599
		[FreeFunction("UnityDisplayManager_ActivateDisplay")]
		[MethodImpl(4096)]
		private static extern void ActivateDisplayImpl(IntPtr nativeDisplay, int width, int height, int refreshRate);

		// Token: 0x06000640 RID: 1600
		[FreeFunction("UnityDisplayManager_SetDisplayParam")]
		[MethodImpl(4096)]
		private static extern void SetParamsImpl(IntPtr nativeDisplay, int width, int height, int x, int y);

		// Token: 0x06000641 RID: 1601
		[FreeFunction("UnityDisplayManager_RelativeMouseAt")]
		[MethodImpl(4096)]
		private static extern int RelativeMouseAtImpl(int x, int y, out int rx, out int ry);

		// Token: 0x06000642 RID: 1602
		[FreeFunction("UnityDisplayManager_DisplayActive")]
		[MethodImpl(4096)]
		private static extern bool GetActiveImpl(IntPtr nativeDisplay);

		// Token: 0x06000643 RID: 1603
		[FreeFunction("UnityDisplayManager_RequiresBlitToBackbuffer")]
		[MethodImpl(4096)]
		private static extern bool RequiresBlitToBackbufferImpl(IntPtr nativeDisplay);

		// Token: 0x06000644 RID: 1604
		[FreeFunction("UnityDisplayManager_RequiresSRGBBlitToBackbuffer")]
		[MethodImpl(4096)]
		private static extern bool RequiresSrgbBlitToBackbufferImpl(IntPtr nativeDisplay);

		// Token: 0x06000645 RID: 1605 RVA: 0x0000A2AF File Offset: 0x000084AF
		// Note: this type is marked as 'beforefieldinit'.
		static Display()
		{
			Display.onDisplaysUpdated = null;
		}

		// Token: 0x04000256 RID: 598
		internal IntPtr nativeDisplay;

		// Token: 0x04000257 RID: 599
		public static Display[] displays = new Display[]
		{
			new Display()
		};

		// Token: 0x04000258 RID: 600
		private static Display _mainDisplay = Display.displays[0];

		// Token: 0x020000D7 RID: 215
		// (Invoke) Token: 0x06000647 RID: 1607
		public delegate void DisplaysUpdatedDelegate();
	}
}
