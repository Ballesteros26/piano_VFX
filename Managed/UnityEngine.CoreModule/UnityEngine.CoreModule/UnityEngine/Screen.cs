using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000DB RID: 219
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/ScreenManager.h")]
	[StaticAccessor("GetScreenManager()", StaticAccessorType.Dot)]
	public sealed class Screen
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600064B RID: 1611
		public static extern int width
		{
			[NativeMethod(Name = "GetWidth", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600064C RID: 1612
		public static extern int height
		{
			[NativeMethod(Name = "GetHeight", IsThreadSafe = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600064D RID: 1613
		public static extern float dpi
		{
			[NativeName("GetDPI")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600064E RID: 1614
		[MethodImpl(4096)]
		private static extern void RequestOrientation(ScreenOrientation orient);

		// Token: 0x0600064F RID: 1615
		[MethodImpl(4096)]
		private static extern ScreenOrientation GetScreenOrientation();

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0000A2D8 File Offset: 0x000084D8
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x0000A2F0 File Offset: 0x000084F0
		public static ScreenOrientation orientation
		{
			get
			{
				return Screen.GetScreenOrientation();
			}
			set
			{
				bool flag = value == ScreenOrientation.Unknown;
				if (flag)
				{
					Debug.Log("ScreenOrientation.Unknown is deprecated. Please use ScreenOrientation.AutoRotation");
					value = ScreenOrientation.AutoRotation;
				}
				Screen.RequestOrientation(value);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000652 RID: 1618
		// (set) Token: 0x06000653 RID: 1619
		[NativeProperty("ScreenTimeout")]
		public static extern int sleepTimeout
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000654 RID: 1620
		[NativeName("GetIsOrientationEnabled")]
		[MethodImpl(4096)]
		private static extern bool IsOrientationEnabled(EnabledOrientation orient);

		// Token: 0x06000655 RID: 1621
		[NativeName("SetIsOrientationEnabled")]
		[MethodImpl(4096)]
		private static extern void SetOrientationEnabled(EnabledOrientation orient, bool enabled);

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0000A320 File Offset: 0x00008520
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x0000A338 File Offset: 0x00008538
		public static bool autorotateToPortrait
		{
			get
			{
				return Screen.IsOrientationEnabled(EnabledOrientation.kAutorotateToPortrait);
			}
			set
			{
				Screen.SetOrientationEnabled(EnabledOrientation.kAutorotateToPortrait, value);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0000A344 File Offset: 0x00008544
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x0000A35C File Offset: 0x0000855C
		public static bool autorotateToPortraitUpsideDown
		{
			get
			{
				return Screen.IsOrientationEnabled(EnabledOrientation.kAutorotateToPortraitUpsideDown);
			}
			set
			{
				Screen.SetOrientationEnabled(EnabledOrientation.kAutorotateToPortraitUpsideDown, value);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0000A368 File Offset: 0x00008568
		// (set) Token: 0x0600065B RID: 1627 RVA: 0x0000A380 File Offset: 0x00008580
		public static bool autorotateToLandscapeLeft
		{
			get
			{
				return Screen.IsOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeLeft);
			}
			set
			{
				Screen.SetOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeLeft, value);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0000A38C File Offset: 0x0000858C
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x0000A3A4 File Offset: 0x000085A4
		public static bool autorotateToLandscapeRight
		{
			get
			{
				return Screen.IsOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeRight);
			}
			set
			{
				Screen.SetOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeRight, value);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0000A3B0 File Offset: 0x000085B0
		public static Resolution currentResolution
		{
			get
			{
				Resolution resolution;
				Screen.get_currentResolution_Injected(out resolution);
				return resolution;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600065F RID: 1631
		// (set) Token: 0x06000660 RID: 1632
		public static extern bool fullScreen
		{
			[NativeName("IsFullscreen")]
			[MethodImpl(4096)]
			get;
			[NativeName("RequestSetFullscreenFromScript")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000661 RID: 1633
		// (set) Token: 0x06000662 RID: 1634
		public static extern FullScreenMode fullScreenMode
		{
			[NativeName("GetFullscreenMode")]
			[MethodImpl(4096)]
			get;
			[NativeName("RequestSetFullscreenModeFromScript")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0000A3C8 File Offset: 0x000085C8
		public static Rect safeArea
		{
			get
			{
				Rect rect;
				Screen.get_safeArea_Injected(out rect);
				return rect;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000664 RID: 1636
		public static extern Rect[] cutouts
		{
			[FreeFunction("ScreenScripting::GetCutouts")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000665 RID: 1637
		[NativeName("RequestResolution")]
		[MethodImpl(4096)]
		public static extern void SetResolution(int width, int height, FullScreenMode fullscreenMode, [DefaultValue("0")] int preferredRefreshRate);

		// Token: 0x06000666 RID: 1638 RVA: 0x0000A3DD File Offset: 0x000085DD
		public static void SetResolution(int width, int height, FullScreenMode fullscreenMode)
		{
			Screen.SetResolution(width, height, fullscreenMode, 0);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0000A3EA File Offset: 0x000085EA
		public static void SetResolution(int width, int height, bool fullscreen, [DefaultValue("0")] int preferredRefreshRate)
		{
			Screen.SetResolution(width, height, fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed, preferredRefreshRate);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0000A3FD File Offset: 0x000085FD
		public static void SetResolution(int width, int height, bool fullscreen)
		{
			Screen.SetResolution(width, height, fullscreen, 0);
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000669 RID: 1641
		public static extern Resolution[] resolutions
		{
			[FreeFunction("ScreenScripting::GetResolutions")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600066A RID: 1642
		// (set) Token: 0x0600066B RID: 1643
		public static extern float brightness
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x0000A40C File Offset: 0x0000860C
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x0000A428 File Offset: 0x00008628
		[Obsolete("Use Cursor.lockState and Cursor.visible instead.", false)]
		[EditorBrowsable(1)]
		public static bool lockCursor
		{
			get
			{
				return CursorLockMode.Locked == Cursor.lockState;
			}
			set
			{
				if (value)
				{
					Cursor.visible = false;
					Cursor.lockState = CursorLockMode.Locked;
				}
				else
				{
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;
				}
			}
		}

		// Token: 0x0600066F RID: 1647
		[MethodImpl(4096)]
		private static extern void get_currentResolution_Injected(out Resolution ret);

		// Token: 0x06000670 RID: 1648
		[MethodImpl(4096)]
		private static extern void get_safeArea_Injected(out Rect ret);
	}
}
