using System;
using System.Collections;
using System.Drawing;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x02000449 RID: 1097
	internal abstract class XplatUIDriver
	{
		// Token: 0x14000470 RID: 1136
		// (add) Token: 0x060047F5 RID: 18421
		// (remove) Token: 0x060047F6 RID: 18422
		internal abstract event EventHandler Idle;

		// Token: 0x060047F7 RID: 18423
		internal abstract IntPtr InitializeDriver();

		// Token: 0x060047F8 RID: 18424
		internal abstract void ShutdownDriver(IntPtr token);

		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x060047F9 RID: 18425 RVA: 0x0011991C File Offset: 0x00117B1C
		internal virtual int ActiveWindowTrackingDelay
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x060047FA RID: 18426 RVA: 0x00119920 File Offset: 0x00117B20
		internal virtual Color ForeColor
		{
			get
			{
				return ThemeEngine.Current.DefaultWindowForeColor;
			}
		}

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x060047FB RID: 18427 RVA: 0x0011992C File Offset: 0x00117B2C
		internal virtual Color BackColor
		{
			get
			{
				return ThemeEngine.Current.DefaultWindowBackColor;
			}
		}

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x060047FC RID: 18428 RVA: 0x00119938 File Offset: 0x00117B38
		internal virtual Size Border3DSize
		{
			get
			{
				return new Size(2, 2);
			}
		}

		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x060047FD RID: 18429 RVA: 0x00119944 File Offset: 0x00117B44
		internal virtual Size BorderSize
		{
			get
			{
				return new Size(1, 1);
			}
		}

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x060047FE RID: 18430 RVA: 0x00119950 File Offset: 0x00117B50
		internal virtual Size CaptionButtonSize
		{
			get
			{
				return new Size(18, 18);
			}
		}

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x060047FF RID: 18431 RVA: 0x0011995C File Offset: 0x00117B5C
		internal virtual int CaretBlinkTime
		{
			get
			{
				return 530;
			}
		}

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x06004800 RID: 18432 RVA: 0x00119964 File Offset: 0x00117B64
		internal virtual int CaretWidth
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x06004801 RID: 18433 RVA: 0x00119968 File Offset: 0x00117B68
		internal virtual Size DoubleClickSize
		{
			get
			{
				return new Size(4, 4);
			}
		}

		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x06004802 RID: 18434 RVA: 0x00119974 File Offset: 0x00117B74
		internal virtual int DoubleClickTime
		{
			get
			{
				return 500;
			}
		}

		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x06004803 RID: 18435 RVA: 0x0011997C File Offset: 0x00117B7C
		internal virtual Size FixedFrameBorderSize
		{
			get
			{
				return new Size(3, 3);
			}
		}

		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x06004804 RID: 18436 RVA: 0x00119988 File Offset: 0x00117B88
		internal virtual Font Font
		{
			get
			{
				return ThemeEngine.Current.DefaultFont;
			}
		}

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x06004805 RID: 18437 RVA: 0x00119994 File Offset: 0x00117B94
		internal virtual int FontSmoothingContrast
		{
			get
			{
				return 1400;
			}
		}

		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x06004806 RID: 18438 RVA: 0x0011999C File Offset: 0x00117B9C
		internal virtual int FontSmoothingType
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x06004807 RID: 18439 RVA: 0x001199A0 File Offset: 0x00117BA0
		internal virtual int HorizontalResizeBorderThickness
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x06004808 RID: 18440 RVA: 0x001199A4 File Offset: 0x00117BA4
		internal virtual bool IsActiveWindowTrackingEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x06004809 RID: 18441 RVA: 0x001199A8 File Offset: 0x00117BA8
		internal virtual bool IsComboBoxAnimationEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x0600480A RID: 18442 RVA: 0x001199AC File Offset: 0x00117BAC
		internal virtual bool IsDropShadowEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x0600480B RID: 18443 RVA: 0x001199B0 File Offset: 0x00117BB0
		internal virtual bool IsFontSmoothingEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x0600480C RID: 18444 RVA: 0x001199B4 File Offset: 0x00117BB4
		internal virtual bool IsHotTrackingEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x0600480D RID: 18445 RVA: 0x001199B8 File Offset: 0x00117BB8
		internal virtual bool IsIconTitleWrappingEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x0600480E RID: 18446 RVA: 0x001199BC File Offset: 0x00117BBC
		internal virtual bool IsKeyboardPreferred
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x0600480F RID: 18447 RVA: 0x001199C0 File Offset: 0x00117BC0
		internal virtual bool IsListBoxSmoothScrollingEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x06004810 RID: 18448 RVA: 0x001199C4 File Offset: 0x00117BC4
		internal virtual bool IsMenuAnimationEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x06004811 RID: 18449 RVA: 0x001199C8 File Offset: 0x00117BC8
		internal virtual bool IsMenuFadeEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06004812 RID: 18450 RVA: 0x001199CC File Offset: 0x00117BCC
		internal virtual bool IsMinimizeRestoreAnimationEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x06004813 RID: 18451 RVA: 0x001199D0 File Offset: 0x00117BD0
		internal virtual bool IsSelectionFadeEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06004814 RID: 18452 RVA: 0x001199D4 File Offset: 0x00117BD4
		internal virtual bool IsSnapToDefaultEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x06004815 RID: 18453 RVA: 0x001199D8 File Offset: 0x00117BD8
		internal virtual bool IsTitleBarGradientEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06004816 RID: 18454 RVA: 0x001199DC File Offset: 0x00117BDC
		internal virtual bool IsToolTipAnimationEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06004817 RID: 18455 RVA: 0x001199E0 File Offset: 0x00117BE0
		internal virtual Size MenuBarButtonSize
		{
			get
			{
				return new Size(19, 19);
			}
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x06004818 RID: 18456 RVA: 0x001199EC File Offset: 0x00117BEC
		public virtual Size MenuButtonSize
		{
			get
			{
				return new Size(18, 18);
			}
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x06004819 RID: 18457 RVA: 0x001199F8 File Offset: 0x00117BF8
		internal virtual int MenuShowDelay
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x0600481A RID: 18458 RVA: 0x001199FC File Offset: 0x00117BFC
		internal virtual Keys ModifierKeys
		{
			get
			{
				return Keys.None;
			}
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x0600481B RID: 18459 RVA: 0x00119A00 File Offset: 0x00117C00
		internal virtual MouseButtons MouseButtons
		{
			get
			{
				return MouseButtons.None;
			}
		}

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x0600481C RID: 18460 RVA: 0x00119A04 File Offset: 0x00117C04
		internal virtual Size MouseHoverSize
		{
			get
			{
				return new Size(1, 1);
			}
		}

		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x0600481D RID: 18461 RVA: 0x00119A10 File Offset: 0x00117C10
		internal virtual int MouseHoverTime
		{
			get
			{
				return 500;
			}
		}

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x0600481E RID: 18462 RVA: 0x00119A18 File Offset: 0x00117C18
		internal virtual int MouseSpeed
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x0600481F RID: 18463 RVA: 0x00119A1C File Offset: 0x00117C1C
		internal virtual int MouseWheelScrollDelta
		{
			get
			{
				return 120;
			}
		}

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06004820 RID: 18464 RVA: 0x00119A20 File Offset: 0x00117C20
		internal virtual Point MousePosition
		{
			get
			{
				return Point.Empty;
			}
		}

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x06004821 RID: 18465 RVA: 0x00119A28 File Offset: 0x00117C28
		internal virtual int MenuHeight
		{
			get
			{
				return 19;
			}
		}

		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x06004822 RID: 18466 RVA: 0x00119A2C File Offset: 0x00117C2C
		internal virtual LeftRightAlignment PopupMenuAlignment
		{
			get
			{
				return LeftRightAlignment.Left;
			}
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06004823 RID: 18467 RVA: 0x00119A30 File Offset: 0x00117C30
		internal virtual PowerStatus PowerStatus
		{
			get
			{
				throw new NotImplementedException("Has not been implemented yet for this platform.");
			}
		}

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06004824 RID: 18468 RVA: 0x00119A3C File Offset: 0x00117C3C
		internal virtual int SizingBorderWidth
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06004825 RID: 18469 RVA: 0x00119A40 File Offset: 0x00117C40
		internal virtual Size SmallCaptionButtonSize
		{
			get
			{
				return new Size(15, 15);
			}
		}

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x06004826 RID: 18470 RVA: 0x00119A4C File Offset: 0x00117C4C
		internal virtual bool UIEffectsEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x06004827 RID: 18471 RVA: 0x00119A50 File Offset: 0x00117C50
		// (set) Token: 0x06004828 RID: 18472 RVA: 0x00119A54 File Offset: 0x00117C54
		internal virtual bool DropTarget
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06004829 RID: 18473 RVA: 0x00119A58 File Offset: 0x00117C58
		internal virtual int HorizontalScrollBarHeight
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x0600482A RID: 18474 RVA: 0x00119A5C File Offset: 0x00117C5C
		internal virtual bool UserClipWontExposeParent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x0600482B RID: 18475 RVA: 0x00119A60 File Offset: 0x00117C60
		internal virtual int VerticalResizeBorderThickness
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x0600482C RID: 18476 RVA: 0x00119A64 File Offset: 0x00117C64
		internal virtual int VerticalScrollBarWidth
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x0600482D RID: 18477
		internal abstract int CaptionHeight { get; }

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x0600482E RID: 18478
		internal abstract Size CursorSize { get; }

		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x0600482F RID: 18479
		internal abstract bool DragFullWindows { get; }

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x06004830 RID: 18480
		internal abstract Size DragSize { get; }

		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06004831 RID: 18481
		internal abstract Size FrameBorderSize { get; }

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06004832 RID: 18482
		internal abstract Size IconSize { get; }

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x06004833 RID: 18483
		internal abstract Size MaxWindowTrackSize { get; }

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x06004834 RID: 18484
		internal abstract bool MenuAccessKeysUnderlined { get; }

		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x06004835 RID: 18485 RVA: 0x00119A68 File Offset: 0x00117C68
		internal virtual Size MinimizedWindowSize
		{
			get
			{
				return new Size(160, SystemInformation.CaptionHeight + 6 - 1);
			}
		}

		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x06004836 RID: 18486
		internal abstract Size MinimizedWindowSpacingSize { get; }

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x06004837 RID: 18487
		internal abstract Size MinimumWindowSize { get; }

		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x06004838 RID: 18488 RVA: 0x00119A88 File Offset: 0x00117C88
		internal virtual Size MinimumFixedToolWindowSize
		{
			get
			{
				return Size.Empty;
			}
		}

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x06004839 RID: 18489 RVA: 0x00119A90 File Offset: 0x00117C90
		internal virtual Size MinimumSizeableToolWindowSize
		{
			get
			{
				return Size.Empty;
			}
		}

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x0600483A RID: 18490 RVA: 0x00119A98 File Offset: 0x00117C98
		internal virtual Size MinimumNoBorderWindowSize
		{
			get
			{
				return Size.Empty;
			}
		}

		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x0600483B RID: 18491 RVA: 0x00119AA0 File Offset: 0x00117CA0
		internal virtual Size MinWindowTrackSize
		{
			get
			{
				return new Size(112, 27);
			}
		}

		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x0600483C RID: 18492
		internal abstract Size SmallIconSize { get; }

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x0600483D RID: 18493
		internal abstract int MouseButtonCount { get; }

		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x0600483E RID: 18494
		internal abstract bool MouseButtonsSwapped { get; }

		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x0600483F RID: 18495
		internal abstract bool MouseWheelPresent { get; }

		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x06004840 RID: 18496
		internal abstract Rectangle VirtualScreen { get; }

		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06004841 RID: 18497
		internal abstract Rectangle WorkingArea { get; }

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06004842 RID: 18498
		internal abstract bool ThemesEnabled { get; }

		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06004843 RID: 18499 RVA: 0x00119AAC File Offset: 0x00117CAC
		internal virtual bool RequiresPositiveClientAreaSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06004844 RID: 18500 RVA: 0x00119AB0 File Offset: 0x00117CB0
		public virtual int ToolWindowCaptionHeight
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06004845 RID: 18501 RVA: 0x00119AB4 File Offset: 0x00117CB4
		public virtual Size ToolWindowCaptionButtonSize
		{
			get
			{
				return new Size(15, 15);
			}
		}

		// Token: 0x06004846 RID: 18502
		internal abstract void AudibleAlert(AlertType alert);

		// Token: 0x06004847 RID: 18503
		internal abstract void EnableThemes();

		// Token: 0x06004848 RID: 18504
		internal abstract void GetDisplaySize(out Size size);

		// Token: 0x06004849 RID: 18505
		internal abstract IntPtr CreateWindow(CreateParams cp);

		// Token: 0x0600484A RID: 18506
		internal abstract IntPtr CreateWindow(IntPtr Parent, int X, int Y, int Width, int Height);

		// Token: 0x0600484B RID: 18507
		internal abstract void DestroyWindow(IntPtr handle);

		// Token: 0x0600484C RID: 18508
		internal abstract FormWindowState GetWindowState(IntPtr handle);

		// Token: 0x0600484D RID: 18509
		internal abstract void SetWindowState(IntPtr handle, FormWindowState state);

		// Token: 0x0600484E RID: 18510
		internal abstract void SetWindowMinMax(IntPtr handle, Rectangle maximized, Size min, Size max);

		// Token: 0x0600484F RID: 18511
		internal abstract void SetWindowStyle(IntPtr handle, CreateParams cp);

		// Token: 0x06004850 RID: 18512
		internal abstract double GetWindowTransparency(IntPtr handle);

		// Token: 0x06004851 RID: 18513
		internal abstract void SetWindowTransparency(IntPtr handle, double transparency, Color key);

		// Token: 0x06004852 RID: 18514
		internal abstract TransparencySupport SupportsTransparency();

		// Token: 0x06004853 RID: 18515 RVA: 0x00119AC0 File Offset: 0x00117CC0
		internal virtual void SetAllowDrop(IntPtr handle, bool value)
		{
			Console.Error.WriteLine("Drag and Drop is currently not supported on this platform");
		}

		// Token: 0x06004854 RID: 18516 RVA: 0x00119AD4 File Offset: 0x00117CD4
		internal virtual DragDropEffects StartDrag(IntPtr handle, object data, DragDropEffects allowedEffects)
		{
			Console.Error.WriteLine("Drag and Drop is currently not supported on this platform");
			return DragDropEffects.None;
		}

		// Token: 0x06004855 RID: 18517
		internal abstract void SetBorderStyle(IntPtr handle, FormBorderStyle border_style);

		// Token: 0x06004856 RID: 18518
		internal abstract void SetMenu(IntPtr handle, Menu menu);

		// Token: 0x06004857 RID: 18519
		internal abstract bool GetText(IntPtr handle, out string text);

		// Token: 0x06004858 RID: 18520
		internal abstract bool Text(IntPtr handle, string text);

		// Token: 0x06004859 RID: 18521
		internal abstract bool SetVisible(IntPtr handle, bool visible, bool activate);

		// Token: 0x0600485A RID: 18522
		internal abstract bool IsVisible(IntPtr handle);

		// Token: 0x0600485B RID: 18523
		internal abstract bool IsEnabled(IntPtr handle);

		// Token: 0x0600485C RID: 18524 RVA: 0x00119AE8 File Offset: 0x00117CE8
		internal virtual bool IsKeyLocked(VirtualKeys key)
		{
			return false;
		}

		// Token: 0x0600485D RID: 18525
		internal abstract IntPtr SetParent(IntPtr handle, IntPtr parent);

		// Token: 0x0600485E RID: 18526
		internal abstract IntPtr GetParent(IntPtr handle);

		// Token: 0x0600485F RID: 18527
		internal abstract void UpdateWindow(IntPtr handle);

		// Token: 0x06004860 RID: 18528
		internal abstract PaintEventArgs PaintEventStart(ref Message msg, IntPtr handle, bool client);

		// Token: 0x06004861 RID: 18529
		internal abstract void PaintEventEnd(ref Message msg, IntPtr handle, bool client);

		// Token: 0x06004862 RID: 18530
		internal abstract void SetWindowPos(IntPtr handle, int x, int y, int width, int height);

		// Token: 0x06004863 RID: 18531
		internal abstract void GetWindowPos(IntPtr handle, bool is_toplevel, out int x, out int y, out int width, out int height, out int client_width, out int client_height);

		// Token: 0x06004864 RID: 18532
		internal abstract void Activate(IntPtr handle);

		// Token: 0x06004865 RID: 18533
		internal abstract void EnableWindow(IntPtr handle, bool Enable);

		// Token: 0x06004866 RID: 18534
		internal abstract void SetModal(IntPtr handle, bool Modal);

		// Token: 0x06004867 RID: 18535
		internal abstract void Invalidate(IntPtr handle, Rectangle rc, bool clear);

		// Token: 0x06004868 RID: 18536
		internal abstract void InvalidateNC(IntPtr handle);

		// Token: 0x06004869 RID: 18537
		internal abstract IntPtr DefWndProc(ref Message msg);

		// Token: 0x0600486A RID: 18538
		internal abstract void HandleException(Exception e);

		// Token: 0x0600486B RID: 18539
		internal abstract void DoEvents();

		// Token: 0x0600486C RID: 18540
		internal abstract bool PeekMessage(object queue_id, ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax, uint flags);

		// Token: 0x0600486D RID: 18541
		internal abstract void PostQuitMessage(int exitCode);

		// Token: 0x0600486E RID: 18542
		internal abstract bool GetMessage(object queue_id, ref MSG msg, IntPtr hWnd, int wFilterMin, int wFilterMax);

		// Token: 0x0600486F RID: 18543
		internal abstract bool TranslateMessage(ref MSG msg);

		// Token: 0x06004870 RID: 18544
		internal abstract IntPtr DispatchMessage(ref MSG msg);

		// Token: 0x06004871 RID: 18545
		internal abstract bool SetZOrder(IntPtr hWnd, IntPtr AfterhWnd, bool Top, bool Bottom);

		// Token: 0x06004872 RID: 18546
		internal abstract bool SetTopmost(IntPtr hWnd, bool Enabled);

		// Token: 0x06004873 RID: 18547
		internal abstract bool SetOwner(IntPtr hWnd, IntPtr hWndOwner);

		// Token: 0x06004874 RID: 18548
		internal abstract bool CalculateWindowRect(ref Rectangle ClientRect, CreateParams cp, Menu menu, out Rectangle WindowRect);

		// Token: 0x06004875 RID: 18549
		internal abstract Region GetClipRegion(IntPtr hwnd);

		// Token: 0x06004876 RID: 18550
		internal abstract void SetClipRegion(IntPtr hwnd, Region region);

		// Token: 0x06004877 RID: 18551
		internal abstract void SetCursor(IntPtr hwnd, IntPtr cursor);

		// Token: 0x06004878 RID: 18552
		internal abstract void ShowCursor(bool show);

		// Token: 0x06004879 RID: 18553
		internal abstract void OverrideCursor(IntPtr cursor);

		// Token: 0x0600487A RID: 18554
		internal abstract IntPtr DefineCursor(Bitmap bitmap, Bitmap mask, Color cursor_pixel, Color mask_pixel, int xHotSpot, int yHotSpot);

		// Token: 0x0600487B RID: 18555
		internal abstract IntPtr DefineStdCursor(StdCursor id);

		// Token: 0x0600487C RID: 18556
		internal abstract Bitmap DefineStdCursorBitmap(StdCursor id);

		// Token: 0x0600487D RID: 18557
		internal abstract void DestroyCursor(IntPtr cursor);

		// Token: 0x0600487E RID: 18558
		internal abstract void GetCursorInfo(IntPtr cursor, out int width, out int height, out int hotspot_x, out int hotspot_y);

		// Token: 0x0600487F RID: 18559
		internal abstract void GetCursorPos(IntPtr hwnd, out int x, out int y);

		// Token: 0x06004880 RID: 18560
		internal abstract void SetCursorPos(IntPtr hwnd, int x, int y);

		// Token: 0x06004881 RID: 18561
		internal abstract void ScreenToClient(IntPtr hwnd, ref int x, ref int y);

		// Token: 0x06004882 RID: 18562
		internal abstract void ClientToScreen(IntPtr hwnd, ref int x, ref int y);

		// Token: 0x06004883 RID: 18563
		internal abstract void GrabWindow(IntPtr hwnd, IntPtr ConfineToHwnd);

		// Token: 0x06004884 RID: 18564
		internal abstract void GrabInfo(out IntPtr hwnd, out bool GrabConfined, out Rectangle GrabArea);

		// Token: 0x06004885 RID: 18565
		internal abstract void UngrabWindow(IntPtr hwnd);

		// Token: 0x06004886 RID: 18566
		internal abstract void SendAsyncMethod(AsyncMethodData method);

		// Token: 0x06004887 RID: 18567
		internal abstract void SetTimer(Timer timer);

		// Token: 0x06004888 RID: 18568
		internal abstract void KillTimer(Timer timer);

		// Token: 0x06004889 RID: 18569
		internal abstract void CreateCaret(IntPtr hwnd, int width, int height);

		// Token: 0x0600488A RID: 18570
		internal abstract void DestroyCaret(IntPtr hwnd);

		// Token: 0x0600488B RID: 18571
		internal abstract void SetCaretPos(IntPtr hwnd, int x, int y);

		// Token: 0x0600488C RID: 18572
		internal abstract void CaretVisible(IntPtr hwnd, bool visible);

		// Token: 0x0600488D RID: 18573
		internal abstract IntPtr GetFocus();

		// Token: 0x0600488E RID: 18574
		internal abstract void SetFocus(IntPtr hwnd);

		// Token: 0x0600488F RID: 18575
		internal abstract IntPtr GetActive();

		// Token: 0x06004890 RID: 18576
		internal abstract IntPtr GetPreviousWindow(IntPtr hwnd);

		// Token: 0x06004891 RID: 18577
		internal abstract void ScrollWindow(IntPtr hwnd, Rectangle rectangle, int XAmount, int YAmount, bool with_children);

		// Token: 0x06004892 RID: 18578
		internal abstract void ScrollWindow(IntPtr hwnd, int XAmount, int YAmount, bool with_children);

		// Token: 0x06004893 RID: 18579
		internal abstract bool GetFontMetrics(Graphics g, Font font, out int ascent, out int descent);

		// Token: 0x06004894 RID: 18580
		internal abstract bool SystrayAdd(IntPtr hwnd, string tip, Icon icon, out ToolTip tt);

		// Token: 0x06004895 RID: 18581
		internal abstract bool SystrayChange(IntPtr hwnd, string tip, Icon icon, ref ToolTip tt);

		// Token: 0x06004896 RID: 18582
		internal abstract void SystrayRemove(IntPtr hwnd, ref ToolTip tt);

		// Token: 0x06004897 RID: 18583
		internal abstract void SystrayBalloon(IntPtr hwnd, int timeout, string title, string text, ToolTipIcon icon);

		// Token: 0x06004898 RID: 18584
		internal abstract Point GetMenuOrigin(IntPtr hwnd);

		// Token: 0x06004899 RID: 18585
		internal abstract void MenuToScreen(IntPtr hwnd, ref int x, ref int y);

		// Token: 0x0600489A RID: 18586
		internal abstract void ScreenToMenu(IntPtr hwnd, ref int x, ref int y);

		// Token: 0x0600489B RID: 18587
		internal abstract void SetIcon(IntPtr handle, Icon icon);

		// Token: 0x0600489C RID: 18588
		internal abstract void ClipboardClose(IntPtr handle);

		// Token: 0x0600489D RID: 18589
		internal abstract IntPtr ClipboardOpen(bool primary_selection);

		// Token: 0x0600489E RID: 18590
		internal abstract int ClipboardGetID(IntPtr handle, string format);

		// Token: 0x0600489F RID: 18591
		internal abstract void ClipboardStore(IntPtr handle, object obj, int id, XplatUI.ObjectToClipboard converter);

		// Token: 0x060048A0 RID: 18592
		internal abstract int[] ClipboardAvailableFormats(IntPtr handle);

		// Token: 0x060048A1 RID: 18593
		internal abstract object ClipboardRetrieve(IntPtr handle, int id, XplatUI.ClipboardToObject converter);

		// Token: 0x060048A2 RID: 18594
		internal abstract void DrawReversibleLine(Point start, Point end, Color backColor);

		// Token: 0x060048A3 RID: 18595
		internal abstract void DrawReversibleRectangle(IntPtr handle, Rectangle rect, int line_width);

		// Token: 0x060048A4 RID: 18596
		internal abstract void FillReversibleRectangle(Rectangle rectangle, Color backColor);

		// Token: 0x060048A5 RID: 18597
		internal abstract void DrawReversibleFrame(Rectangle rectangle, Color backColor, FrameStyle style);

		// Token: 0x060048A6 RID: 18598
		internal abstract SizeF GetAutoScaleSize(Font font);

		// Token: 0x060048A7 RID: 18599
		internal abstract IntPtr SendMessage(IntPtr hwnd, Msg message, IntPtr wParam, IntPtr lParam);

		// Token: 0x060048A8 RID: 18600
		internal abstract bool PostMessage(IntPtr hwnd, Msg message, IntPtr wParam, IntPtr lParam);

		// Token: 0x060048A9 RID: 18601
		internal abstract int SendInput(IntPtr hwnd, Queue keys);

		// Token: 0x060048AA RID: 18602
		internal abstract object StartLoop(Thread thread);

		// Token: 0x060048AB RID: 18603
		internal abstract void EndLoop(Thread thread);

		// Token: 0x060048AC RID: 18604
		internal abstract void RequestNCRecalc(IntPtr hwnd);

		// Token: 0x060048AD RID: 18605
		internal abstract void ResetMouseHover(IntPtr hwnd);

		// Token: 0x060048AE RID: 18606
		internal abstract void RequestAdditionalWM_NCMessages(IntPtr hwnd, bool hover, bool leave);

		// Token: 0x060048AF RID: 18607
		internal abstract void RaiseIdle(EventArgs e);

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x060048B0 RID: 18608
		internal abstract int KeyboardSpeed { get; }

		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x060048B1 RID: 18609
		internal abstract int KeyboardDelay { get; }

		// Token: 0x060048B2 RID: 18610 RVA: 0x00119AEC File Offset: 0x00117CEC
		internal virtual void CreateOffscreenDrawable(IntPtr handle, int width, int height, out object offscreen_drawable)
		{
			Bitmap bitmap = new Bitmap(width, height, 2498570);
			offscreen_drawable = bitmap;
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x00119B0C File Offset: 0x00117D0C
		internal virtual void DestroyOffscreenDrawable(object offscreen_drawable)
		{
			Bitmap bitmap = (Bitmap)offscreen_drawable;
			bitmap.Dispose();
		}

		// Token: 0x060048B4 RID: 18612 RVA: 0x00119B28 File Offset: 0x00117D28
		internal virtual Graphics GetOffscreenGraphics(object offscreen_drawable)
		{
			Bitmap bitmap = (Bitmap)offscreen_drawable;
			return Graphics.FromImage(bitmap);
		}

		// Token: 0x060048B5 RID: 18613 RVA: 0x00119B44 File Offset: 0x00117D44
		internal virtual void BlitFromOffscreen(IntPtr dest_handle, Graphics dest_dc, object offscreen_drawable, Graphics offscreen_dc, Rectangle r)
		{
			dest_dc.DrawImage((Bitmap)offscreen_drawable, r, r, 2);
		}

		// Token: 0x060048B6 RID: 18614 RVA: 0x00119B58 File Offset: 0x00117D58
		internal virtual void SetForegroundWindow(IntPtr handle)
		{
		}

		// Token: 0x0200064E RID: 1614
		// (Invoke) Token: 0x060050EA RID: 20714
		internal delegate IntPtr WndProc(IntPtr hwnd, Msg msg, IntPtr wParam, IntPtr lParam);
	}
}
