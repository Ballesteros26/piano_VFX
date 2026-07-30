using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x02000446 RID: 1094
	internal class XplatUI
	{
		// Token: 0x06004643 RID: 17987 RVA: 0x00114D78 File Offset: 0x00112F78
		static XplatUI()
		{
			if (XplatUI.RunningOnUnix)
			{
				if (Environment.GetEnvironmentVariable("MONO_MWF_MAC_FORCE_X11") != null)
				{
					XplatUI.driver = XplatUIX11.GetInstance();
				}
				else
				{
					IntPtr intPtr = Marshal.AllocHGlobal(8192);
					if (XplatUI.uname(intPtr) != 0)
					{
						XplatUI.driver = XplatUIX11.GetInstance();
					}
					else
					{
						string text = Marshal.PtrToStringAnsi(intPtr);
						if (text == "Darwin")
						{
							XplatUI.driver = XplatUICarbon.GetInstance();
						}
						else
						{
							XplatUI.driver = XplatUIX11.GetInstance();
						}
					}
					Marshal.FreeHGlobal(intPtr);
				}
			}
			else
			{
				XplatUI.driver = XplatUIWin32.GetInstance();
			}
			XplatUI.driver.InitializeDriver();
			DataFormats.GetFormat(0);
			Application.FirePreRun();
		}

		// Token: 0x1400046E RID: 1134
		// (add) Token: 0x06004644 RID: 17988 RVA: 0x00114E54 File Offset: 0x00113054
		// (remove) Token: 0x06004645 RID: 17989 RVA: 0x00114E64 File Offset: 0x00113064
		internal static event EventHandler Idle
		{
			add
			{
				XplatUI.driver.Idle += value;
			}
			remove
			{
				XplatUI.driver.Idle -= value;
			}
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x00114E74 File Offset: 0x00113074
		internal static string Window(IntPtr handle)
		{
			return string.Format("'{0}' ({1:X})", Control.FromHandle(handle), handle.ToInt32());
		}

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x06004647 RID: 17991 RVA: 0x00114E94 File Offset: 0x00113094
		public static bool RunningOnUnix
		{
			get
			{
				int platform = Environment.OSVersion.Platform;
				return platform == 4 || platform == 6 || platform == 128;
			}
		}

		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x06004648 RID: 17992 RVA: 0x00114EC8 File Offset: 0x001130C8
		public static int ActiveWindowTrackingDelay
		{
			get
			{
				return XplatUI.driver.ActiveWindowTrackingDelay;
			}
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x06004649 RID: 17993 RVA: 0x00114ED4 File Offset: 0x001130D4
		// (set) Token: 0x0600464A RID: 17994 RVA: 0x00114EDC File Offset: 0x001130DC
		internal static string DefaultClassName
		{
			get
			{
				return XplatUI.default_class_name;
			}
			set
			{
				XplatUI.default_class_name = value;
			}
		}

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x0600464B RID: 17995 RVA: 0x00114EE4 File Offset: 0x001130E4
		public static Size Border3DSize
		{
			get
			{
				return XplatUI.driver.Border3DSize;
			}
		}

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x0600464C RID: 17996 RVA: 0x00114EF0 File Offset: 0x001130F0
		public static Size BorderSize
		{
			get
			{
				return XplatUI.driver.BorderSize;
			}
		}

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x0600464D RID: 17997 RVA: 0x00114EFC File Offset: 0x001130FC
		public static Size CaptionButtonSize
		{
			get
			{
				return XplatUI.driver.CaptionButtonSize;
			}
		}

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x0600464E RID: 17998 RVA: 0x00114F08 File Offset: 0x00113108
		public static int CaptionHeight
		{
			get
			{
				return XplatUI.driver.CaptionHeight;
			}
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x0600464F RID: 17999 RVA: 0x00114F14 File Offset: 0x00113114
		public static int CaretBlinkTime
		{
			get
			{
				return XplatUI.driver.CaretBlinkTime;
			}
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x06004650 RID: 18000 RVA: 0x00114F20 File Offset: 0x00113120
		public static int CaretWidth
		{
			get
			{
				return XplatUI.driver.CaretWidth;
			}
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x06004651 RID: 18001 RVA: 0x00114F2C File Offset: 0x0011312C
		public static Size CursorSize
		{
			get
			{
				return XplatUI.driver.CursorSize;
			}
		}

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x06004652 RID: 18002 RVA: 0x00114F38 File Offset: 0x00113138
		public static Size DoubleClickSize
		{
			get
			{
				return XplatUI.driver.DoubleClickSize;
			}
		}

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x06004653 RID: 18003 RVA: 0x00114F44 File Offset: 0x00113144
		public static int DoubleClickTime
		{
			get
			{
				return XplatUI.driver.DoubleClickTime;
			}
		}

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x06004654 RID: 18004 RVA: 0x00114F50 File Offset: 0x00113150
		public static bool DragFullWindows
		{
			get
			{
				return XplatUI.driver.DragFullWindows;
			}
		}

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x06004655 RID: 18005 RVA: 0x00114F5C File Offset: 0x0011315C
		public static Size DragSize
		{
			get
			{
				return XplatUI.driver.DragSize;
			}
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x06004656 RID: 18006 RVA: 0x00114F68 File Offset: 0x00113168
		public static Size FixedFrameBorderSize
		{
			get
			{
				return XplatUI.driver.FixedFrameBorderSize;
			}
		}

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x06004657 RID: 18007 RVA: 0x00114F74 File Offset: 0x00113174
		public static int FontSmoothingContrast
		{
			get
			{
				return XplatUI.driver.FontSmoothingContrast;
			}
		}

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06004658 RID: 18008 RVA: 0x00114F80 File Offset: 0x00113180
		public static int FontSmoothingType
		{
			get
			{
				return XplatUI.driver.FontSmoothingType;
			}
		}

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06004659 RID: 18009 RVA: 0x00114F8C File Offset: 0x0011318C
		public static Size FrameBorderSize
		{
			get
			{
				return XplatUI.driver.FrameBorderSize;
			}
		}

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x0600465A RID: 18010 RVA: 0x00114F98 File Offset: 0x00113198
		public static int HorizontalResizeBorderThickness
		{
			get
			{
				return XplatUI.driver.HorizontalResizeBorderThickness;
			}
		}

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x0600465B RID: 18011 RVA: 0x00114FA4 File Offset: 0x001131A4
		public static int HorizontalScrollBarHeight
		{
			get
			{
				return XplatUI.driver.HorizontalScrollBarHeight;
			}
		}

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x0600465C RID: 18012 RVA: 0x00114FB0 File Offset: 0x001131B0
		public static Size IconSize
		{
			get
			{
				return XplatUI.driver.IconSize;
			}
		}

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x0600465D RID: 18013 RVA: 0x00114FBC File Offset: 0x001131BC
		public static bool IsActiveWindowTrackingEnabled
		{
			get
			{
				return XplatUI.driver.IsActiveWindowTrackingEnabled;
			}
		}

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x0600465E RID: 18014 RVA: 0x00114FC8 File Offset: 0x001131C8
		public static bool IsComboBoxAnimationEnabled
		{
			get
			{
				return XplatUI.driver.IsComboBoxAnimationEnabled;
			}
		}

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x0600465F RID: 18015 RVA: 0x00114FD4 File Offset: 0x001131D4
		public static bool IsDropShadowEnabled
		{
			get
			{
				return XplatUI.driver.IsDropShadowEnabled;
			}
		}

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06004660 RID: 18016 RVA: 0x00114FE0 File Offset: 0x001131E0
		public static bool IsFontSmoothingEnabled
		{
			get
			{
				return XplatUI.driver.IsFontSmoothingEnabled;
			}
		}

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06004661 RID: 18017 RVA: 0x00114FEC File Offset: 0x001131EC
		public static bool IsHotTrackingEnabled
		{
			get
			{
				return XplatUI.driver.IsHotTrackingEnabled;
			}
		}

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06004662 RID: 18018 RVA: 0x00114FF8 File Offset: 0x001131F8
		public static bool IsIconTitleWrappingEnabled
		{
			get
			{
				return XplatUI.driver.IsIconTitleWrappingEnabled;
			}
		}

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06004663 RID: 18019 RVA: 0x00115004 File Offset: 0x00113204
		public static bool IsKeyboardPreferred
		{
			get
			{
				return XplatUI.driver.IsKeyboardPreferred;
			}
		}

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06004664 RID: 18020 RVA: 0x00115010 File Offset: 0x00113210
		public static bool IsListBoxSmoothScrollingEnabled
		{
			get
			{
				return XplatUI.driver.IsListBoxSmoothScrollingEnabled;
			}
		}

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06004665 RID: 18021 RVA: 0x0011501C File Offset: 0x0011321C
		public static bool IsMenuAnimationEnabled
		{
			get
			{
				return XplatUI.driver.IsMenuAnimationEnabled;
			}
		}

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06004666 RID: 18022 RVA: 0x00115028 File Offset: 0x00113228
		public static bool IsMenuFadeEnabled
		{
			get
			{
				return XplatUI.driver.IsMenuFadeEnabled;
			}
		}

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06004667 RID: 18023 RVA: 0x00115034 File Offset: 0x00113234
		public static bool IsMinimizeRestoreAnimationEnabled
		{
			get
			{
				return XplatUI.driver.IsMinimizeRestoreAnimationEnabled;
			}
		}

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06004668 RID: 18024 RVA: 0x00115040 File Offset: 0x00113240
		public static bool IsSelectionFadeEnabled
		{
			get
			{
				return XplatUI.driver.IsSelectionFadeEnabled;
			}
		}

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06004669 RID: 18025 RVA: 0x0011504C File Offset: 0x0011324C
		public static bool IsSnapToDefaultEnabled
		{
			get
			{
				return XplatUI.driver.IsSnapToDefaultEnabled;
			}
		}

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x0600466A RID: 18026 RVA: 0x00115058 File Offset: 0x00113258
		public static bool IsTitleBarGradientEnabled
		{
			get
			{
				return XplatUI.driver.IsTitleBarGradientEnabled;
			}
		}

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x0600466B RID: 18027 RVA: 0x00115064 File Offset: 0x00113264
		public static bool IsToolTipAnimationEnabled
		{
			get
			{
				return XplatUI.driver.IsToolTipAnimationEnabled;
			}
		}

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x0600466C RID: 18028 RVA: 0x00115070 File Offset: 0x00113270
		public static int KeyboardSpeed
		{
			get
			{
				return XplatUI.driver.KeyboardSpeed;
			}
		}

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x0600466D RID: 18029 RVA: 0x0011507C File Offset: 0x0011327C
		public static int KeyboardDelay
		{
			get
			{
				return XplatUI.driver.KeyboardDelay;
			}
		}

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x0600466E RID: 18030 RVA: 0x00115088 File Offset: 0x00113288
		public static Size MaxWindowTrackSize
		{
			get
			{
				return XplatUI.driver.MaxWindowTrackSize;
			}
		}

		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x0600466F RID: 18031 RVA: 0x00115094 File Offset: 0x00113294
		public static bool MenuAccessKeysUnderlined
		{
			get
			{
				return XplatUI.driver.MenuAccessKeysUnderlined;
			}
		}

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x06004670 RID: 18032 RVA: 0x001150A0 File Offset: 0x001132A0
		public static Size MenuBarButtonSize
		{
			get
			{
				return XplatUI.driver.MenuBarButtonSize;
			}
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x06004671 RID: 18033 RVA: 0x001150AC File Offset: 0x001132AC
		public static Size MenuButtonSize
		{
			get
			{
				return XplatUI.driver.MenuButtonSize;
			}
		}

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x06004672 RID: 18034 RVA: 0x001150B8 File Offset: 0x001132B8
		public static int MenuShowDelay
		{
			get
			{
				return XplatUI.driver.MenuShowDelay;
			}
		}

		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x06004673 RID: 18035 RVA: 0x001150C4 File Offset: 0x001132C4
		public static Size MinimizedWindowSize
		{
			get
			{
				return XplatUI.driver.MinimizedWindowSize;
			}
		}

		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x06004674 RID: 18036 RVA: 0x001150D0 File Offset: 0x001132D0
		public static Size MinimizedWindowSpacingSize
		{
			get
			{
				return XplatUI.driver.MinimizedWindowSpacingSize;
			}
		}

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x06004675 RID: 18037 RVA: 0x001150DC File Offset: 0x001132DC
		public static Size MinimumWindowSize
		{
			get
			{
				return XplatUI.driver.MinimumWindowSize;
			}
		}

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x06004676 RID: 18038 RVA: 0x001150E8 File Offset: 0x001132E8
		public static Size MinimumFixedToolWindowSize
		{
			get
			{
				return XplatUI.driver.MinimumFixedToolWindowSize;
			}
		}

		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x06004677 RID: 18039 RVA: 0x001150F4 File Offset: 0x001132F4
		public static Size MinimumSizeableToolWindowSize
		{
			get
			{
				return XplatUI.driver.MinimumSizeableToolWindowSize;
			}
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x06004678 RID: 18040 RVA: 0x00115100 File Offset: 0x00113300
		public static Size MinimumNoBorderWindowSize
		{
			get
			{
				return XplatUI.driver.MinimumNoBorderWindowSize;
			}
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06004679 RID: 18041 RVA: 0x0011510C File Offset: 0x0011330C
		public static Size MinWindowTrackSize
		{
			get
			{
				return XplatUI.driver.MinWindowTrackSize;
			}
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x0600467A RID: 18042 RVA: 0x00115118 File Offset: 0x00113318
		public static int MouseSpeed
		{
			get
			{
				return XplatUI.driver.MouseSpeed;
			}
		}

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x0600467B RID: 18043 RVA: 0x00115124 File Offset: 0x00113324
		public static Size SmallIconSize
		{
			get
			{
				return XplatUI.driver.SmallIconSize;
			}
		}

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x0600467C RID: 18044 RVA: 0x00115130 File Offset: 0x00113330
		public static int MenuHeight
		{
			get
			{
				return XplatUI.driver.MenuHeight;
			}
		}

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x0600467D RID: 18045 RVA: 0x0011513C File Offset: 0x0011333C
		public static int MouseButtonCount
		{
			get
			{
				return XplatUI.driver.MouseButtonCount;
			}
		}

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x0600467E RID: 18046 RVA: 0x00115148 File Offset: 0x00113348
		public static bool MouseButtonsSwapped
		{
			get
			{
				return XplatUI.driver.MouseButtonsSwapped;
			}
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x0600467F RID: 18047 RVA: 0x00115154 File Offset: 0x00113354
		public static Size MouseHoverSize
		{
			get
			{
				return XplatUI.driver.MouseHoverSize;
			}
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x06004680 RID: 18048 RVA: 0x00115160 File Offset: 0x00113360
		public static int MouseHoverTime
		{
			get
			{
				return XplatUI.driver.MouseHoverTime;
			}
		}

		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x06004681 RID: 18049 RVA: 0x0011516C File Offset: 0x0011336C
		public static int MouseWheelScrollDelta
		{
			get
			{
				return XplatUI.driver.MouseWheelScrollDelta;
			}
		}

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x06004682 RID: 18050 RVA: 0x00115178 File Offset: 0x00113378
		public static bool MouseWheelPresent
		{
			get
			{
				return XplatUI.driver.MouseWheelPresent;
			}
		}

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x06004683 RID: 18051 RVA: 0x00115184 File Offset: 0x00113384
		public static LeftRightAlignment PopupMenuAlignment
		{
			get
			{
				return XplatUI.driver.PopupMenuAlignment;
			}
		}

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x06004684 RID: 18052 RVA: 0x00115190 File Offset: 0x00113390
		public static PowerStatus PowerStatus
		{
			get
			{
				return XplatUI.driver.PowerStatus;
			}
		}

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x06004685 RID: 18053 RVA: 0x0011519C File Offset: 0x0011339C
		public static bool RequiresPositiveClientAreaSize
		{
			get
			{
				return XplatUI.driver.RequiresPositiveClientAreaSize;
			}
		}

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x06004686 RID: 18054 RVA: 0x001151A8 File Offset: 0x001133A8
		public static int SizingBorderWidth
		{
			get
			{
				return XplatUI.driver.SizingBorderWidth;
			}
		}

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x06004687 RID: 18055 RVA: 0x001151B4 File Offset: 0x001133B4
		public static Size SmallCaptionButtonSize
		{
			get
			{
				return XplatUI.driver.SmallCaptionButtonSize;
			}
		}

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x06004688 RID: 18056 RVA: 0x001151C0 File Offset: 0x001133C0
		public static bool UIEffectsEnabled
		{
			get
			{
				return XplatUI.driver.UIEffectsEnabled;
			}
		}

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x06004689 RID: 18057 RVA: 0x001151CC File Offset: 0x001133CC
		public static bool UserClipWontExposeParent
		{
			get
			{
				return XplatUI.driver.UserClipWontExposeParent;
			}
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x0600468A RID: 18058 RVA: 0x001151D8 File Offset: 0x001133D8
		public static int VerticalResizeBorderThickness
		{
			get
			{
				return XplatUI.driver.VerticalResizeBorderThickness;
			}
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x0600468B RID: 18059 RVA: 0x001151E4 File Offset: 0x001133E4
		public static int VerticalScrollBarWidth
		{
			get
			{
				return XplatUI.driver.VerticalScrollBarWidth;
			}
		}

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x0600468C RID: 18060 RVA: 0x001151F0 File Offset: 0x001133F0
		public static Rectangle VirtualScreen
		{
			get
			{
				return XplatUI.driver.VirtualScreen;
			}
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x0600468D RID: 18061 RVA: 0x001151FC File Offset: 0x001133FC
		public static Rectangle WorkingArea
		{
			get
			{
				return XplatUI.driver.WorkingArea;
			}
		}

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x0600468E RID: 18062 RVA: 0x00115208 File Offset: 0x00113408
		public static bool ThemesEnabled
		{
			get
			{
				return XplatUI.driver.ThemesEnabled;
			}
		}

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x0600468F RID: 18063 RVA: 0x00115214 File Offset: 0x00113414
		public static int ToolWindowCaptionHeight
		{
			get
			{
				return XplatUI.driver.ToolWindowCaptionHeight;
			}
		}

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x06004690 RID: 18064 RVA: 0x00115220 File Offset: 0x00113420
		public static Size ToolWindowCaptionButtonSize
		{
			get
			{
				return XplatUI.driver.ToolWindowCaptionButtonSize;
			}
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x0011522C File Offset: 0x0011342C
		internal static void Activate(IntPtr handle)
		{
			XplatUI.driver.Activate(handle);
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x0011523C File Offset: 0x0011343C
		internal static void AudibleAlert(AlertType alert)
		{
			XplatUI.driver.AudibleAlert(alert);
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x0011524C File Offset: 0x0011344C
		internal static bool CalculateWindowRect(ref Rectangle ClientRect, CreateParams cp, Menu menu, out Rectangle WindowRect)
		{
			return XplatUI.driver.CalculateWindowRect(ref ClientRect, cp, menu, out WindowRect);
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x0011525C File Offset: 0x0011345C
		internal static void CaretVisible(IntPtr handle, bool visible)
		{
			XplatUI.driver.CaretVisible(handle, visible);
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x0011526C File Offset: 0x0011346C
		internal static void CreateCaret(IntPtr handle, int width, int height)
		{
			XplatUI.driver.CreateCaret(handle, width, height);
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x0011527C File Offset: 0x0011347C
		internal static IntPtr CreateWindow(CreateParams cp)
		{
			return XplatUI.driver.CreateWindow(cp);
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x0011528C File Offset: 0x0011348C
		internal static IntPtr CreateWindow(IntPtr Parent, int X, int Y, int Width, int Height)
		{
			return XplatUI.driver.CreateWindow(Parent, X, Y, Width, Height);
		}

		// Token: 0x06004698 RID: 18072 RVA: 0x001152A0 File Offset: 0x001134A0
		internal static void ClientToScreen(IntPtr handle, ref int x, ref int y)
		{
			XplatUI.driver.ClientToScreen(handle, ref x, ref y);
		}

		// Token: 0x06004699 RID: 18073 RVA: 0x001152B0 File Offset: 0x001134B0
		internal static int[] ClipboardAvailableFormats(IntPtr handle)
		{
			return XplatUI.driver.ClipboardAvailableFormats(handle);
		}

		// Token: 0x0600469A RID: 18074 RVA: 0x001152C0 File Offset: 0x001134C0
		internal static void ClipboardClose(IntPtr handle)
		{
			XplatUI.driver.ClipboardClose(handle);
		}

		// Token: 0x0600469B RID: 18075 RVA: 0x001152D0 File Offset: 0x001134D0
		internal static int ClipboardGetID(IntPtr handle, string format)
		{
			return XplatUI.driver.ClipboardGetID(handle, format);
		}

		// Token: 0x0600469C RID: 18076 RVA: 0x001152E0 File Offset: 0x001134E0
		internal static IntPtr ClipboardOpen(bool primary_selection)
		{
			return XplatUI.driver.ClipboardOpen(primary_selection);
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x001152F0 File Offset: 0x001134F0
		internal static void ClipboardStore(IntPtr handle, object obj, int type, XplatUI.ObjectToClipboard converter)
		{
			XplatUI.driver.ClipboardStore(handle, obj, type, converter);
		}

		// Token: 0x0600469E RID: 18078 RVA: 0x00115300 File Offset: 0x00113500
		internal static object ClipboardRetrieve(IntPtr handle, int type, XplatUI.ClipboardToObject converter)
		{
			return XplatUI.driver.ClipboardRetrieve(handle, type, converter);
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x00115310 File Offset: 0x00113510
		internal static IntPtr DefineCursor(Bitmap bitmap, Bitmap mask, Color cursor_pixel, Color mask_pixel, int xHotSpot, int yHotSpot)
		{
			return XplatUI.driver.DefineCursor(bitmap, mask, cursor_pixel, mask_pixel, xHotSpot, yHotSpot);
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x00115324 File Offset: 0x00113524
		internal static IntPtr DefineStdCursor(StdCursor id)
		{
			return XplatUI.driver.DefineStdCursor(id);
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x00115334 File Offset: 0x00113534
		internal static Bitmap DefineStdCursorBitmap(StdCursor id)
		{
			return XplatUI.driver.DefineStdCursorBitmap(id);
		}

		// Token: 0x060046A2 RID: 18082 RVA: 0x00115344 File Offset: 0x00113544
		internal static IntPtr DefWndProc(ref Message msg)
		{
			return XplatUI.driver.DefWndProc(ref msg);
		}

		// Token: 0x060046A3 RID: 18083 RVA: 0x00115354 File Offset: 0x00113554
		internal static void DestroyCaret(IntPtr handle)
		{
			XplatUI.driver.DestroyCaret(handle);
		}

		// Token: 0x060046A4 RID: 18084 RVA: 0x00115364 File Offset: 0x00113564
		internal static void DestroyCursor(IntPtr cursor)
		{
			XplatUI.driver.DestroyCursor(cursor);
		}

		// Token: 0x060046A5 RID: 18085 RVA: 0x00115374 File Offset: 0x00113574
		internal static void DestroyWindow(IntPtr handle)
		{
			XplatUI.driver.DestroyWindow(handle);
		}

		// Token: 0x060046A6 RID: 18086 RVA: 0x00115384 File Offset: 0x00113584
		internal static IntPtr DispatchMessage(ref MSG msg)
		{
			return XplatUI.driver.DispatchMessage(ref msg);
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x00115394 File Offset: 0x00113594
		internal static void DoEvents()
		{
			XplatUI.driver.DoEvents();
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x001153A0 File Offset: 0x001135A0
		internal static void DrawReversibleRectangle(IntPtr handle, Rectangle rect, int line_width)
		{
			XplatUI.driver.DrawReversibleRectangle(handle, rect, line_width);
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x001153B0 File Offset: 0x001135B0
		internal static void FillReversibleRectangle(Rectangle rectangle, Color backColor)
		{
			XplatUI.driver.FillReversibleRectangle(rectangle, backColor);
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x001153C0 File Offset: 0x001135C0
		internal static void DrawReversibleFrame(Rectangle rectangle, Color backColor, FrameStyle style)
		{
			XplatUI.driver.DrawReversibleFrame(rectangle, backColor, style);
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x001153D0 File Offset: 0x001135D0
		internal static void DrawReversibleLine(Point start, Point end, Color backColor)
		{
			XplatUI.driver.DrawReversibleLine(start, end, backColor);
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x001153E0 File Offset: 0x001135E0
		internal static void EnableThemes()
		{
			XplatUI.driver.EnableThemes();
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x001153EC File Offset: 0x001135EC
		internal static void EnableWindow(IntPtr handle, bool Enable)
		{
			XplatUI.driver.EnableWindow(handle, Enable);
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x001153FC File Offset: 0x001135FC
		internal static void EndLoop(Thread thread)
		{
			XplatUI.driver.EndLoop(thread);
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x0011540C File Offset: 0x0011360C
		internal static IntPtr GetActive()
		{
			return XplatUI.driver.GetActive();
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x00115418 File Offset: 0x00113618
		internal static SizeF GetAutoScaleSize(Font font)
		{
			return XplatUI.driver.GetAutoScaleSize(font);
		}

		// Token: 0x060046B1 RID: 18097 RVA: 0x00115428 File Offset: 0x00113628
		internal static Region GetClipRegion(IntPtr handle)
		{
			return XplatUI.driver.GetClipRegion(handle);
		}

		// Token: 0x060046B2 RID: 18098 RVA: 0x00115438 File Offset: 0x00113638
		internal static void GetCursorInfo(IntPtr cursor, out int width, out int height, out int hotspot_x, out int hotspot_y)
		{
			XplatUI.driver.GetCursorInfo(cursor, out width, out height, out hotspot_x, out hotspot_y);
		}

		// Token: 0x060046B3 RID: 18099 RVA: 0x0011544C File Offset: 0x0011364C
		internal static void GetCursorPos(IntPtr handle, out int x, out int y)
		{
			XplatUI.driver.GetCursorPos(handle, out x, out y);
		}

		// Token: 0x060046B4 RID: 18100 RVA: 0x0011545C File Offset: 0x0011365C
		internal static void GetDisplaySize(out Size size)
		{
			XplatUI.driver.GetDisplaySize(out size);
		}

		// Token: 0x060046B5 RID: 18101 RVA: 0x0011546C File Offset: 0x0011366C
		internal static IntPtr GetFocus()
		{
			return XplatUI.driver.GetFocus();
		}

		// Token: 0x060046B6 RID: 18102 RVA: 0x00115478 File Offset: 0x00113678
		internal static bool GetFontMetrics(Graphics g, Font font, out int ascent, out int descent)
		{
			return XplatUI.driver.GetFontMetrics(g, font, out ascent, out descent);
		}

		// Token: 0x060046B7 RID: 18103 RVA: 0x00115488 File Offset: 0x00113688
		internal static Point GetMenuOrigin(IntPtr handle)
		{
			return XplatUI.driver.GetMenuOrigin(handle);
		}

		// Token: 0x060046B8 RID: 18104 RVA: 0x00115498 File Offset: 0x00113698
		internal static bool GetMessage(object queue_id, ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax)
		{
			return XplatUI.driver.GetMessage(queue_id, ref msg, hWnd, wFilterMin, wFilterMax);
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x001154AC File Offset: 0x001136AC
		internal static IntPtr GetParent(IntPtr handle)
		{
			return XplatUI.driver.GetParent(handle);
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x001154BC File Offset: 0x001136BC
		internal static IntPtr GetPreviousWindow(IntPtr handle)
		{
			return XplatUI.driver.GetPreviousWindow(handle);
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x001154CC File Offset: 0x001136CC
		internal static bool GetText(IntPtr handle, out string text)
		{
			return XplatUI.driver.GetText(handle, out text);
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x001154DC File Offset: 0x001136DC
		internal static void GetWindowPos(IntPtr handle, bool is_toplevel, out int x, out int y, out int width, out int height, out int client_width, out int client_height)
		{
			XplatUI.driver.GetWindowPos(handle, is_toplevel, out x, out y, out width, out height, out client_width, out client_height);
		}

		// Token: 0x060046BD RID: 18109 RVA: 0x00115500 File Offset: 0x00113700
		internal static FormWindowState GetWindowState(IntPtr handle)
		{
			return XplatUI.driver.GetWindowState(handle);
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x00115510 File Offset: 0x00113710
		internal static void GrabInfo(out IntPtr handle, out bool GrabConfined, out Rectangle GrabArea)
		{
			XplatUI.driver.GrabInfo(out handle, out GrabConfined, out GrabArea);
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x00115520 File Offset: 0x00113720
		internal static void GrabWindow(IntPtr handle, IntPtr ConfineToHwnd)
		{
			XplatUI.driver.GrabWindow(handle, ConfineToHwnd);
		}

		// Token: 0x060046C0 RID: 18112 RVA: 0x00115530 File Offset: 0x00113730
		internal static void HandleException(Exception e)
		{
			XplatUI.driver.HandleException(e);
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x00115540 File Offset: 0x00113740
		internal static void Invalidate(IntPtr handle, Rectangle rc, bool clear)
		{
			XplatUI.driver.Invalidate(handle, rc, clear);
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x00115550 File Offset: 0x00113750
		internal static void InvalidateNC(IntPtr handle)
		{
			XplatUI.driver.InvalidateNC(handle);
		}

		// Token: 0x060046C3 RID: 18115 RVA: 0x00115560 File Offset: 0x00113760
		internal static bool IsEnabled(IntPtr handle)
		{
			return XplatUI.driver.IsEnabled(handle);
		}

		// Token: 0x060046C4 RID: 18116 RVA: 0x00115570 File Offset: 0x00113770
		internal static bool IsKeyLocked(VirtualKeys key)
		{
			return XplatUI.driver.IsKeyLocked(key);
		}

		// Token: 0x060046C5 RID: 18117 RVA: 0x00115580 File Offset: 0x00113780
		internal static bool IsVisible(IntPtr handle)
		{
			return XplatUI.driver.IsVisible(handle);
		}

		// Token: 0x060046C6 RID: 18118 RVA: 0x00115590 File Offset: 0x00113790
		internal static void KillTimer(Timer timer)
		{
			XplatUI.driver.KillTimer(timer);
		}

		// Token: 0x060046C7 RID: 18119 RVA: 0x001155A0 File Offset: 0x001137A0
		internal static void MenuToScreen(IntPtr handle, ref int x, ref int y)
		{
			XplatUI.driver.MenuToScreen(handle, ref x, ref y);
		}

		// Token: 0x060046C8 RID: 18120 RVA: 0x001155B0 File Offset: 0x001137B0
		internal static void OverrideCursor(IntPtr cursor)
		{
			XplatUI.driver.OverrideCursor(cursor);
		}

		// Token: 0x060046C9 RID: 18121 RVA: 0x001155C0 File Offset: 0x001137C0
		internal static void PaintEventEnd(ref Message msg, IntPtr handle, bool client)
		{
			XplatUI.driver.PaintEventEnd(ref msg, handle, client);
		}

		// Token: 0x060046CA RID: 18122 RVA: 0x001155D0 File Offset: 0x001137D0
		internal static PaintEventArgs PaintEventStart(ref Message msg, IntPtr handle, bool client)
		{
			return XplatUI.driver.PaintEventStart(ref msg, handle, client);
		}

		// Token: 0x060046CB RID: 18123 RVA: 0x001155E0 File Offset: 0x001137E0
		internal static bool PeekMessage(object queue_id, ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax, uint flags)
		{
			return XplatUI.driver.PeekMessage(queue_id, ref msg, hWnd, wFilterMin, wFilterMax, flags);
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x001155F4 File Offset: 0x001137F4
		internal static bool PostMessage(IntPtr hwnd, Msg message, IntPtr wParam, IntPtr lParam)
		{
			return XplatUI.driver.PostMessage(hwnd, message, wParam, lParam);
		}

		// Token: 0x060046CD RID: 18125 RVA: 0x00115604 File Offset: 0x00113804
		internal static bool PostMessage(ref MSG msg)
		{
			return XplatUI.driver.PostMessage(msg.hwnd, msg.message, msg.wParam, msg.lParam);
		}

		// Token: 0x060046CE RID: 18126 RVA: 0x00115634 File Offset: 0x00113834
		internal static void PostQuitMessage(int exitCode)
		{
			XplatUI.driver.PostQuitMessage(exitCode);
		}

		// Token: 0x060046CF RID: 18127 RVA: 0x00115644 File Offset: 0x00113844
		internal static void RaiseIdle(EventArgs e)
		{
			XplatUI.driver.RaiseIdle(e);
		}

		// Token: 0x060046D0 RID: 18128 RVA: 0x00115654 File Offset: 0x00113854
		internal static void RequestAdditionalWM_NCMessages(IntPtr handle, bool hover, bool leave)
		{
			XplatUI.driver.RequestAdditionalWM_NCMessages(handle, hover, leave);
		}

		// Token: 0x060046D1 RID: 18129 RVA: 0x00115664 File Offset: 0x00113864
		internal static void RequestNCRecalc(IntPtr handle)
		{
			XplatUI.driver.RequestNCRecalc(handle);
		}

		// Token: 0x060046D2 RID: 18130 RVA: 0x00115674 File Offset: 0x00113874
		internal static void ResetMouseHover(IntPtr handle)
		{
			XplatUI.driver.ResetMouseHover(handle);
		}

		// Token: 0x060046D3 RID: 18131 RVA: 0x00115684 File Offset: 0x00113884
		internal static void ScreenToClient(IntPtr handle, ref int x, ref int y)
		{
			XplatUI.driver.ScreenToClient(handle, ref x, ref y);
		}

		// Token: 0x060046D4 RID: 18132 RVA: 0x00115694 File Offset: 0x00113894
		internal static void ScreenToMenu(IntPtr handle, ref int x, ref int y)
		{
			XplatUI.driver.ScreenToMenu(handle, ref x, ref y);
		}

		// Token: 0x060046D5 RID: 18133 RVA: 0x001156A4 File Offset: 0x001138A4
		internal static void ScrollWindow(IntPtr handle, Rectangle rectangle, int XAmount, int YAmount, bool with_children)
		{
			XplatUI.driver.ScrollWindow(handle, rectangle, XAmount, YAmount, with_children);
		}

		// Token: 0x060046D6 RID: 18134 RVA: 0x001156B8 File Offset: 0x001138B8
		internal static void ScrollWindow(IntPtr handle, int XAmount, int YAmount, bool with_children)
		{
			XplatUI.driver.ScrollWindow(handle, XAmount, YAmount, with_children);
		}

		// Token: 0x060046D7 RID: 18135 RVA: 0x001156C8 File Offset: 0x001138C8
		internal static void SendAsyncMethod(AsyncMethodData data)
		{
			XplatUI.driver.SendAsyncMethod(data);
		}

		// Token: 0x060046D8 RID: 18136 RVA: 0x001156D8 File Offset: 0x001138D8
		internal static int SendInput(IntPtr hwnd, Queue keys)
		{
			return XplatUI.driver.SendInput(hwnd, keys);
		}

		// Token: 0x060046D9 RID: 18137 RVA: 0x001156E8 File Offset: 0x001138E8
		internal static IntPtr SendMessage(IntPtr handle, Msg message, IntPtr wParam, IntPtr lParam)
		{
			return XplatUI.driver.SendMessage(handle, message, wParam, lParam);
		}

		// Token: 0x060046DA RID: 18138 RVA: 0x001156F8 File Offset: 0x001138F8
		internal static void SendMessage(ref Message m)
		{
			m.Result = XplatUI.driver.SendMessage(m.HWnd, (Msg)m.Msg, m.WParam, m.LParam);
		}

		// Token: 0x060046DB RID: 18139 RVA: 0x00115730 File Offset: 0x00113930
		internal static void SetAllowDrop(IntPtr handle, bool value)
		{
			XplatUI.driver.SetAllowDrop(handle, value);
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x00115740 File Offset: 0x00113940
		internal static void SetBorderStyle(IntPtr handle, FormBorderStyle border_style)
		{
			XplatUI.driver.SetBorderStyle(handle, border_style);
		}

		// Token: 0x060046DD RID: 18141 RVA: 0x00115750 File Offset: 0x00113950
		internal static void SetCaretPos(IntPtr handle, int x, int y)
		{
			XplatUI.driver.SetCaretPos(handle, x, y);
		}

		// Token: 0x060046DE RID: 18142 RVA: 0x00115760 File Offset: 0x00113960
		internal static void SetClipRegion(IntPtr handle, Region region)
		{
			XplatUI.driver.SetClipRegion(handle, region);
		}

		// Token: 0x060046DF RID: 18143 RVA: 0x00115770 File Offset: 0x00113970
		internal static void SetCursor(IntPtr handle, IntPtr cursor)
		{
			XplatUI.driver.SetCursor(handle, cursor);
		}

		// Token: 0x060046E0 RID: 18144 RVA: 0x00115780 File Offset: 0x00113980
		internal static void SetCursorPos(IntPtr handle, int x, int y)
		{
			XplatUI.driver.SetCursorPos(handle, x, y);
		}

		// Token: 0x060046E1 RID: 18145 RVA: 0x00115790 File Offset: 0x00113990
		internal static void SetFocus(IntPtr handle)
		{
			XplatUI.driver.SetFocus(handle);
		}

		// Token: 0x060046E2 RID: 18146 RVA: 0x001157A0 File Offset: 0x001139A0
		internal static void SetForegroundWindow(IntPtr handle)
		{
			XplatUI.driver.SetForegroundWindow(handle);
		}

		// Token: 0x060046E3 RID: 18147 RVA: 0x001157B0 File Offset: 0x001139B0
		internal static void SetIcon(IntPtr handle, Icon icon)
		{
			XplatUI.driver.SetIcon(handle, icon);
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x001157C0 File Offset: 0x001139C0
		internal static void SetMenu(IntPtr handle, Menu menu)
		{
			XplatUI.driver.SetMenu(handle, menu);
		}

		// Token: 0x060046E5 RID: 18149 RVA: 0x001157D0 File Offset: 0x001139D0
		internal static void SetModal(IntPtr handle, bool Modal)
		{
			XplatUI.driver.SetModal(handle, Modal);
		}

		// Token: 0x060046E6 RID: 18150 RVA: 0x001157E0 File Offset: 0x001139E0
		internal static IntPtr SetParent(IntPtr handle, IntPtr hParent)
		{
			return XplatUI.driver.SetParent(handle, hParent);
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x001157F0 File Offset: 0x001139F0
		internal static void SetTimer(Timer timer)
		{
			XplatUI.driver.SetTimer(timer);
		}

		// Token: 0x060046E8 RID: 18152 RVA: 0x00115800 File Offset: 0x00113A00
		internal static bool SetTopmost(IntPtr handle, bool Enabled)
		{
			return XplatUI.driver.SetTopmost(handle, Enabled);
		}

		// Token: 0x060046E9 RID: 18153 RVA: 0x00115810 File Offset: 0x00113A10
		internal static bool SetOwner(IntPtr handle, IntPtr hWndOwner)
		{
			return XplatUI.driver.SetOwner(handle, hWndOwner);
		}

		// Token: 0x060046EA RID: 18154 RVA: 0x00115820 File Offset: 0x00113A20
		internal static bool SetVisible(IntPtr handle, bool visible, bool activate)
		{
			return XplatUI.driver.SetVisible(handle, visible, activate);
		}

		// Token: 0x060046EB RID: 18155 RVA: 0x00115830 File Offset: 0x00113A30
		internal static void SetWindowMinMax(IntPtr handle, Rectangle maximized, Size min, Size max)
		{
			XplatUI.driver.SetWindowMinMax(handle, maximized, min, max);
		}

		// Token: 0x060046EC RID: 18156 RVA: 0x00115840 File Offset: 0x00113A40
		internal static void SetWindowPos(IntPtr handle, int x, int y, int width, int height)
		{
			XplatUI.driver.SetWindowPos(handle, x, y, width, height);
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x00115854 File Offset: 0x00113A54
		internal static void SetWindowState(IntPtr handle, FormWindowState state)
		{
			XplatUI.driver.SetWindowState(handle, state);
		}

		// Token: 0x060046EE RID: 18158 RVA: 0x00115864 File Offset: 0x00113A64
		internal static void SetWindowStyle(IntPtr handle, CreateParams cp)
		{
			XplatUI.driver.SetWindowStyle(handle, cp);
		}

		// Token: 0x060046EF RID: 18159 RVA: 0x00115874 File Offset: 0x00113A74
		internal static double GetWindowTransparency(IntPtr handle)
		{
			return XplatUI.driver.GetWindowTransparency(handle);
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x00115884 File Offset: 0x00113A84
		internal static void SetWindowTransparency(IntPtr handle, double transparency, Color key)
		{
			XplatUI.driver.SetWindowTransparency(handle, transparency, key);
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x00115894 File Offset: 0x00113A94
		internal static bool SetZOrder(IntPtr handle, IntPtr AfterhWnd, bool Top, bool Bottom)
		{
			return XplatUI.driver.SetZOrder(handle, AfterhWnd, Top, Bottom);
		}

		// Token: 0x060046F2 RID: 18162 RVA: 0x001158A4 File Offset: 0x00113AA4
		internal static void ShowCursor(bool show)
		{
			XplatUI.driver.ShowCursor(show);
		}

		// Token: 0x060046F3 RID: 18163 RVA: 0x001158B4 File Offset: 0x00113AB4
		internal static DragDropEffects StartDrag(IntPtr handle, object data, DragDropEffects allowedEffects)
		{
			return XplatUI.driver.StartDrag(handle, data, allowedEffects);
		}

		// Token: 0x060046F4 RID: 18164 RVA: 0x001158C4 File Offset: 0x00113AC4
		internal static object StartLoop(Thread thread)
		{
			return XplatUI.driver.StartLoop(thread);
		}

		// Token: 0x060046F5 RID: 18165 RVA: 0x001158D4 File Offset: 0x00113AD4
		internal static TransparencySupport SupportsTransparency()
		{
			return XplatUI.driver.SupportsTransparency();
		}

		// Token: 0x060046F6 RID: 18166 RVA: 0x001158E0 File Offset: 0x00113AE0
		internal static bool SystrayAdd(IntPtr handle, string tip, Icon icon, out ToolTip tt)
		{
			return XplatUI.driver.SystrayAdd(handle, tip, icon, out tt);
		}

		// Token: 0x060046F7 RID: 18167 RVA: 0x001158F0 File Offset: 0x00113AF0
		internal static void SystrayChange(IntPtr handle, string tip, Icon icon, ref ToolTip tt)
		{
			XplatUI.driver.SystrayChange(handle, tip, icon, ref tt);
		}

		// Token: 0x060046F8 RID: 18168 RVA: 0x00115904 File Offset: 0x00113B04
		internal static void SystrayRemove(IntPtr handle, ref ToolTip tt)
		{
			XplatUI.driver.SystrayRemove(handle, ref tt);
		}

		// Token: 0x060046F9 RID: 18169 RVA: 0x00115914 File Offset: 0x00113B14
		internal static void SystrayBalloon(IntPtr handle, int timeout, string title, string text, ToolTipIcon icon)
		{
			XplatUI.driver.SystrayBalloon(handle, timeout, title, text, icon);
		}

		// Token: 0x060046FA RID: 18170 RVA: 0x00115928 File Offset: 0x00113B28
		internal static bool Text(IntPtr handle, string text)
		{
			return XplatUI.driver.Text(handle, text);
		}

		// Token: 0x060046FB RID: 18171 RVA: 0x00115938 File Offset: 0x00113B38
		internal static bool TranslateMessage(ref MSG msg)
		{
			return XplatUI.driver.TranslateMessage(ref msg);
		}

		// Token: 0x060046FC RID: 18172 RVA: 0x00115948 File Offset: 0x00113B48
		internal static void UngrabWindow(IntPtr handle)
		{
			XplatUI.driver.UngrabWindow(handle);
		}

		// Token: 0x060046FD RID: 18173 RVA: 0x00115958 File Offset: 0x00113B58
		internal static void UpdateWindow(IntPtr handle)
		{
			XplatUI.driver.UpdateWindow(handle);
		}

		// Token: 0x060046FE RID: 18174 RVA: 0x00115968 File Offset: 0x00113B68
		internal static void CreateOffscreenDrawable(IntPtr handle, int width, int height, out object offscreen_drawable)
		{
			XplatUI.driver.CreateOffscreenDrawable(handle, width, height, out offscreen_drawable);
		}

		// Token: 0x060046FF RID: 18175 RVA: 0x00115978 File Offset: 0x00113B78
		internal static void DestroyOffscreenDrawable(object offscreen_drawable)
		{
			XplatUI.driver.DestroyOffscreenDrawable(offscreen_drawable);
		}

		// Token: 0x06004700 RID: 18176 RVA: 0x00115988 File Offset: 0x00113B88
		internal static Graphics GetOffscreenGraphics(object offscreen_drawable)
		{
			return XplatUI.driver.GetOffscreenGraphics(offscreen_drawable);
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x00115998 File Offset: 0x00113B98
		internal static void BlitFromOffscreen(IntPtr dest_handle, Graphics dest_dc, object offscreen_drawable, Graphics offscreen_dc, Rectangle r)
		{
			XplatUI.driver.BlitFromOffscreen(dest_handle, dest_dc, offscreen_drawable, offscreen_dc, r);
		}

		// Token: 0x06004702 RID: 18178 RVA: 0x001159AC File Offset: 0x00113BAC
		internal static void Version()
		{
			Console.WriteLine("Xplat version $Revision: $");
		}

		// Token: 0x06004703 RID: 18179 RVA: 0x001159B8 File Offset: 0x00113BB8
		internal static void AddKeyFilter(IKeyFilter value)
		{
			ArrayList arrayList = XplatUI.key_filters;
			lock (arrayList)
			{
				XplatUI.key_filters.Add(value);
			}
		}

		// Token: 0x06004704 RID: 18180 RVA: 0x00115A08 File Offset: 0x00113C08
		internal static bool FilterKey(KeyFilterData key)
		{
			ArrayList arrayList = XplatUI.key_filters;
			lock (arrayList)
			{
				for (int i = 0; i < XplatUI.key_filters.Count; i++)
				{
					IKeyFilter keyFilter = (IKeyFilter)XplatUI.key_filters[i];
					if (keyFilter.PreFilterKey(key))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06004705 RID: 18181
		[DllImport("libc")]
		private static extern int uname(IntPtr buf);

		// Token: 0x040022CA RID: 8906
		private static XplatUIDriver driver;

		// Token: 0x040022CB RID: 8907
		private static string default_class_name = "SWFClass" + Thread.GetDomainID().ToString();

		// Token: 0x040022CC RID: 8908
		internal static ArrayList key_filters = new ArrayList();

		// Token: 0x02000447 RID: 1095
		public class State
		{
			// Token: 0x17001233 RID: 4659
			// (get) Token: 0x06004707 RID: 18183 RVA: 0x00115A94 File Offset: 0x00113C94
			public static Keys ModifierKeys
			{
				get
				{
					return XplatUI.driver.ModifierKeys;
				}
			}

			// Token: 0x17001234 RID: 4660
			// (get) Token: 0x06004708 RID: 18184 RVA: 0x00115AA0 File Offset: 0x00113CA0
			public static MouseButtons MouseButtons
			{
				get
				{
					return XplatUI.driver.MouseButtons;
				}
			}

			// Token: 0x17001235 RID: 4661
			// (get) Token: 0x06004709 RID: 18185 RVA: 0x00115AAC File Offset: 0x00113CAC
			public static Point MousePosition
			{
				get
				{
					return XplatUI.driver.MousePosition;
				}
			}
		}

		// Token: 0x0200064C RID: 1612
		// (Invoke) Token: 0x060050E2 RID: 20706
		public delegate bool ClipboardToObject(int type, IntPtr data, out object obj);

		// Token: 0x0200064D RID: 1613
		// (Invoke) Token: 0x060050E6 RID: 20710
		public delegate bool ObjectToClipboard(ref int type, object obj, out byte[] data);
	}
}
