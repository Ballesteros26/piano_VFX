using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x02000328 RID: 808
	internal abstract class Theme
	{
		// Token: 0x060035E6 RID: 13798 RVA: 0x000D3798 File Offset: 0x000D1998
		protected Theme()
		{
			this.default_font = SystemFonts.DefaultFont;
			this.syscolors = null;
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x000D37C0 File Offset: 0x000D19C0
		private void SetSystemColors(KnownColor kc, Color value)
		{
			if (this.update == null)
			{
				Type type = Type.GetType("System.Drawing.KnownColors, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				if (type != null)
				{
					this.update = type.GetMethod("Update", 24);
				}
			}
			if (this.update != null)
			{
				this.update.Invoke(null, new object[]
				{
					kc,
					value.ToArgb()
				});
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x060035E8 RID: 13800
		public abstract Version Version { get; }

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x060035E9 RID: 13801 RVA: 0x000D3834 File Offset: 0x000D1A34
		// (set) Token: 0x060035EA RID: 13802 RVA: 0x000D383C File Offset: 0x000D1A3C
		public virtual Color ColorScrollBar
		{
			get
			{
				return SystemColors.ScrollBar;
			}
			set
			{
				this.SetSystemColors(23, value);
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x060035EB RID: 13803 RVA: 0x000D3848 File Offset: 0x000D1A48
		// (set) Token: 0x060035EC RID: 13804 RVA: 0x000D3850 File Offset: 0x000D1A50
		public virtual Color ColorDesktop
		{
			get
			{
				return SystemColors.Desktop;
			}
			set
			{
				this.SetSystemColors(11, value);
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x060035ED RID: 13805 RVA: 0x000D385C File Offset: 0x000D1A5C
		// (set) Token: 0x060035EE RID: 13806 RVA: 0x000D3864 File Offset: 0x000D1A64
		public virtual Color ColorActiveCaption
		{
			get
			{
				return SystemColors.ActiveCaption;
			}
			set
			{
				this.SetSystemColors(2, value);
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x060035EF RID: 13807 RVA: 0x000D3870 File Offset: 0x000D1A70
		// (set) Token: 0x060035F0 RID: 13808 RVA: 0x000D3878 File Offset: 0x000D1A78
		public virtual Color ColorInactiveCaption
		{
			get
			{
				return SystemColors.InactiveCaption;
			}
			set
			{
				this.SetSystemColors(17, value);
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x060035F1 RID: 13809 RVA: 0x000D3884 File Offset: 0x000D1A84
		// (set) Token: 0x060035F2 RID: 13810 RVA: 0x000D388C File Offset: 0x000D1A8C
		public virtual Color ColorMenu
		{
			get
			{
				return SystemColors.Menu;
			}
			set
			{
				this.SetSystemColors(21, value);
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x060035F3 RID: 13811 RVA: 0x000D3898 File Offset: 0x000D1A98
		// (set) Token: 0x060035F4 RID: 13812 RVA: 0x000D38A0 File Offset: 0x000D1AA0
		public virtual Color ColorWindow
		{
			get
			{
				return SystemColors.Window;
			}
			set
			{
				this.SetSystemColors(24, value);
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x060035F5 RID: 13813 RVA: 0x000D38AC File Offset: 0x000D1AAC
		// (set) Token: 0x060035F6 RID: 13814 RVA: 0x000D38B4 File Offset: 0x000D1AB4
		public virtual Color ColorWindowFrame
		{
			get
			{
				return SystemColors.WindowFrame;
			}
			set
			{
				this.SetSystemColors(25, value);
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x060035F7 RID: 13815 RVA: 0x000D38C0 File Offset: 0x000D1AC0
		// (set) Token: 0x060035F8 RID: 13816 RVA: 0x000D38C8 File Offset: 0x000D1AC8
		public virtual Color ColorMenuText
		{
			get
			{
				return SystemColors.MenuText;
			}
			set
			{
				this.SetSystemColors(22, value);
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x060035F9 RID: 13817 RVA: 0x000D38D4 File Offset: 0x000D1AD4
		// (set) Token: 0x060035FA RID: 13818 RVA: 0x000D38DC File Offset: 0x000D1ADC
		public virtual Color ColorWindowText
		{
			get
			{
				return SystemColors.WindowText;
			}
			set
			{
				this.SetSystemColors(26, value);
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x060035FB RID: 13819 RVA: 0x000D38E8 File Offset: 0x000D1AE8
		// (set) Token: 0x060035FC RID: 13820 RVA: 0x000D38F0 File Offset: 0x000D1AF0
		public virtual Color ColorActiveCaptionText
		{
			get
			{
				return SystemColors.ActiveCaptionText;
			}
			set
			{
				this.SetSystemColors(3, value);
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x060035FD RID: 13821 RVA: 0x000D38FC File Offset: 0x000D1AFC
		// (set) Token: 0x060035FE RID: 13822 RVA: 0x000D3904 File Offset: 0x000D1B04
		public virtual Color ColorActiveBorder
		{
			get
			{
				return SystemColors.ActiveBorder;
			}
			set
			{
				this.SetSystemColors(1, value);
			}
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x060035FF RID: 13823 RVA: 0x000D3910 File Offset: 0x000D1B10
		// (set) Token: 0x06003600 RID: 13824 RVA: 0x000D3918 File Offset: 0x000D1B18
		public virtual Color ColorInactiveBorder
		{
			get
			{
				return SystemColors.InactiveBorder;
			}
			set
			{
				this.SetSystemColors(16, value);
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06003601 RID: 13825 RVA: 0x000D3924 File Offset: 0x000D1B24
		// (set) Token: 0x06003602 RID: 13826 RVA: 0x000D392C File Offset: 0x000D1B2C
		public virtual Color ColorAppWorkspace
		{
			get
			{
				return SystemColors.AppWorkspace;
			}
			set
			{
				this.SetSystemColors(4, value);
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06003603 RID: 13827 RVA: 0x000D3938 File Offset: 0x000D1B38
		// (set) Token: 0x06003604 RID: 13828 RVA: 0x000D3940 File Offset: 0x000D1B40
		public virtual Color ColorHighlight
		{
			get
			{
				return SystemColors.Highlight;
			}
			set
			{
				this.SetSystemColors(13, value);
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06003605 RID: 13829 RVA: 0x000D394C File Offset: 0x000D1B4C
		// (set) Token: 0x06003606 RID: 13830 RVA: 0x000D3954 File Offset: 0x000D1B54
		public virtual Color ColorHighlightText
		{
			get
			{
				return SystemColors.HighlightText;
			}
			set
			{
				this.SetSystemColors(14, value);
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06003607 RID: 13831 RVA: 0x000D3960 File Offset: 0x000D1B60
		// (set) Token: 0x06003608 RID: 13832 RVA: 0x000D3968 File Offset: 0x000D1B68
		public virtual Color ColorControl
		{
			get
			{
				return SystemColors.Control;
			}
			set
			{
				this.SetSystemColors(5, value);
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06003609 RID: 13833 RVA: 0x000D3974 File Offset: 0x000D1B74
		// (set) Token: 0x0600360A RID: 13834 RVA: 0x000D397C File Offset: 0x000D1B7C
		public virtual Color ColorControlDark
		{
			get
			{
				return SystemColors.ControlDark;
			}
			set
			{
				this.SetSystemColors(6, value);
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x0600360B RID: 13835 RVA: 0x000D3988 File Offset: 0x000D1B88
		// (set) Token: 0x0600360C RID: 13836 RVA: 0x000D3990 File Offset: 0x000D1B90
		public virtual Color ColorGrayText
		{
			get
			{
				return SystemColors.GrayText;
			}
			set
			{
				this.SetSystemColors(12, value);
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x0600360D RID: 13837 RVA: 0x000D399C File Offset: 0x000D1B9C
		// (set) Token: 0x0600360E RID: 13838 RVA: 0x000D39A4 File Offset: 0x000D1BA4
		public virtual Color ColorControlText
		{
			get
			{
				return SystemColors.ControlText;
			}
			set
			{
				this.SetSystemColors(10, value);
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x0600360F RID: 13839 RVA: 0x000D39B0 File Offset: 0x000D1BB0
		// (set) Token: 0x06003610 RID: 13840 RVA: 0x000D39B8 File Offset: 0x000D1BB8
		public virtual Color ColorInactiveCaptionText
		{
			get
			{
				return SystemColors.InactiveCaptionText;
			}
			set
			{
				this.SetSystemColors(18, value);
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06003611 RID: 13841 RVA: 0x000D39C4 File Offset: 0x000D1BC4
		// (set) Token: 0x06003612 RID: 13842 RVA: 0x000D39CC File Offset: 0x000D1BCC
		public virtual Color ColorControlLight
		{
			get
			{
				return SystemColors.ControlLight;
			}
			set
			{
				this.SetSystemColors(8, value);
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06003613 RID: 13843 RVA: 0x000D39D8 File Offset: 0x000D1BD8
		// (set) Token: 0x06003614 RID: 13844 RVA: 0x000D39E0 File Offset: 0x000D1BE0
		public virtual Color ColorControlDarkDark
		{
			get
			{
				return SystemColors.ControlDarkDark;
			}
			set
			{
				this.SetSystemColors(7, value);
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06003615 RID: 13845 RVA: 0x000D39EC File Offset: 0x000D1BEC
		// (set) Token: 0x06003616 RID: 13846 RVA: 0x000D39F4 File Offset: 0x000D1BF4
		public virtual Color ColorControlLightLight
		{
			get
			{
				return SystemColors.ControlLightLight;
			}
			set
			{
				this.SetSystemColors(9, value);
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06003617 RID: 13847 RVA: 0x000D3A00 File Offset: 0x000D1C00
		// (set) Token: 0x06003618 RID: 13848 RVA: 0x000D3A08 File Offset: 0x000D1C08
		public virtual Color ColorInfoText
		{
			get
			{
				return SystemColors.InfoText;
			}
			set
			{
				this.SetSystemColors(20, value);
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06003619 RID: 13849 RVA: 0x000D3A14 File Offset: 0x000D1C14
		// (set) Token: 0x0600361A RID: 13850 RVA: 0x000D3A1C File Offset: 0x000D1C1C
		public virtual Color ColorInfo
		{
			get
			{
				return SystemColors.Info;
			}
			set
			{
				this.SetSystemColors(19, value);
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x0600361B RID: 13851 RVA: 0x000D3A28 File Offset: 0x000D1C28
		// (set) Token: 0x0600361C RID: 13852 RVA: 0x000D3A30 File Offset: 0x000D1C30
		public virtual Color ColorHotTrack
		{
			get
			{
				return SystemColors.HotTrack;
			}
			set
			{
				this.SetSystemColors(15, value);
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x0600361D RID: 13853 RVA: 0x000D3A3C File Offset: 0x000D1C3C
		// (set) Token: 0x0600361E RID: 13854 RVA: 0x000D3A44 File Offset: 0x000D1C44
		public virtual Color DefaultControlBackColor
		{
			get
			{
				return this.ColorControl;
			}
			set
			{
				this.ColorControl = value;
			}
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x0600361F RID: 13855 RVA: 0x000D3A50 File Offset: 0x000D1C50
		// (set) Token: 0x06003620 RID: 13856 RVA: 0x000D3A58 File Offset: 0x000D1C58
		public virtual Color DefaultControlForeColor
		{
			get
			{
				return this.ColorControlText;
			}
			set
			{
				this.ColorControlText = value;
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06003621 RID: 13857 RVA: 0x000D3A64 File Offset: 0x000D1C64
		public virtual Font DefaultFont
		{
			get
			{
				return this.default_font;
			}
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06003622 RID: 13858 RVA: 0x000D3A6C File Offset: 0x000D1C6C
		public virtual Color DefaultWindowBackColor
		{
			get
			{
				return this.defaultWindowBackColor;
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06003623 RID: 13859 RVA: 0x000D3A74 File Offset: 0x000D1C74
		public virtual Color DefaultWindowForeColor
		{
			get
			{
				return this.defaultWindowForeColor;
			}
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x000D3A7C File Offset: 0x000D1C7C
		public virtual Color GetColor(XplatUIWin32.GetSysColorIndex idx)
		{
			return (Color)this.syscolors.GetValue((int)idx);
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x000D3A90 File Offset: 0x000D1C90
		public virtual void SetColor(XplatUIWin32.GetSysColorIndex idx, Color color)
		{
			this.syscolors.SetValue(color, (int)idx);
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x000D3AA4 File Offset: 0x000D1CA4
		public virtual ArrangeDirection ArrangeDirection
		{
			get
			{
				return ArrangeDirection.Down;
			}
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06003627 RID: 13863 RVA: 0x000D3AA8 File Offset: 0x000D1CA8
		public virtual ArrangeStartingPosition ArrangeStartingPosition
		{
			get
			{
				return ArrangeStartingPosition.BottomLeft;
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06003628 RID: 13864 RVA: 0x000D3AAC File Offset: 0x000D1CAC
		public virtual int BorderMultiplierFactor
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06003629 RID: 13865 RVA: 0x000D3AB0 File Offset: 0x000D1CB0
		public virtual Size BorderSizableSize
		{
			get
			{
				return new Size(3, 3);
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x0600362A RID: 13866 RVA: 0x000D3ABC File Offset: 0x000D1CBC
		public virtual Size Border3DSize
		{
			get
			{
				return XplatUI.Border3DSize;
			}
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x0600362B RID: 13867 RVA: 0x000D3AC4 File Offset: 0x000D1CC4
		public virtual Size BorderStaticSize
		{
			get
			{
				return new Size(1, 1);
			}
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x0600362C RID: 13868 RVA: 0x000D3AD0 File Offset: 0x000D1CD0
		public virtual Size BorderSize
		{
			get
			{
				return XplatUI.BorderSize;
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x0600362D RID: 13869 RVA: 0x000D3AD8 File Offset: 0x000D1CD8
		public virtual Size CaptionButtonSize
		{
			get
			{
				return XplatUI.CaptionButtonSize;
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x0600362E RID: 13870 RVA: 0x000D3AE0 File Offset: 0x000D1CE0
		public virtual int CaptionHeight
		{
			get
			{
				return XplatUI.CaptionHeight;
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x0600362F RID: 13871 RVA: 0x000D3AE8 File Offset: 0x000D1CE8
		public virtual Size DoubleClickSize
		{
			get
			{
				return new Size(4, 4);
			}
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06003630 RID: 13872 RVA: 0x000D3AF4 File Offset: 0x000D1CF4
		public virtual int DoubleClickTime
		{
			get
			{
				return XplatUI.DoubleClickTime;
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06003631 RID: 13873 RVA: 0x000D3AFC File Offset: 0x000D1CFC
		public virtual Size FixedFrameBorderSize
		{
			get
			{
				return XplatUI.FixedFrameBorderSize;
			}
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06003632 RID: 13874 RVA: 0x000D3B04 File Offset: 0x000D1D04
		public virtual Size FrameBorderSize
		{
			get
			{
				return XplatUI.FrameBorderSize;
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06003633 RID: 13875 RVA: 0x000D3B0C File Offset: 0x000D1D0C
		public virtual int HorizontalFocusThickness
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06003634 RID: 13876 RVA: 0x000D3B10 File Offset: 0x000D1D10
		public virtual int HorizontalScrollBarArrowWidth
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06003635 RID: 13877 RVA: 0x000D3B14 File Offset: 0x000D1D14
		public virtual int HorizontalScrollBarHeight
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06003636 RID: 13878 RVA: 0x000D3B18 File Offset: 0x000D1D18
		public virtual int HorizontalScrollBarThumbWidth
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06003637 RID: 13879 RVA: 0x000D3B1C File Offset: 0x000D1D1C
		public virtual Size IconSpacingSize
		{
			get
			{
				return new Size(75, 75);
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06003638 RID: 13880 RVA: 0x000D3B28 File Offset: 0x000D1D28
		public virtual bool MenuAccessKeysUnderlined
		{
			get
			{
				return XplatUI.MenuAccessKeysUnderlined;
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06003639 RID: 13881 RVA: 0x000D3B30 File Offset: 0x000D1D30
		public virtual Size MenuBarButtonSize
		{
			get
			{
				return XplatUI.MenuBarButtonSize;
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x0600363A RID: 13882 RVA: 0x000D3B38 File Offset: 0x000D1D38
		public virtual Size MenuButtonSize
		{
			get
			{
				return XplatUI.MenuButtonSize;
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x0600363B RID: 13883 RVA: 0x000D3B40 File Offset: 0x000D1D40
		public virtual Size MenuCheckSize
		{
			get
			{
				return new Size(13, 13);
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x0600363C RID: 13884 RVA: 0x000D3B4C File Offset: 0x000D1D4C
		public virtual Font MenuFont
		{
			get
			{
				return this.default_font;
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x0600363D RID: 13885 RVA: 0x000D3B54 File Offset: 0x000D1D54
		public virtual int MenuHeight
		{
			get
			{
				return XplatUI.MenuHeight;
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x0600363E RID: 13886 RVA: 0x000D3B5C File Offset: 0x000D1D5C
		public virtual int MouseWheelScrollLines
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x0600363F RID: 13887 RVA: 0x000D3B60 File Offset: 0x000D1D60
		public virtual bool RightAlignedMenus
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06003640 RID: 13888 RVA: 0x000D3B64 File Offset: 0x000D1D64
		public virtual Size ToolWindowCaptionButtonSize
		{
			get
			{
				return XplatUI.ToolWindowCaptionButtonSize;
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06003641 RID: 13889 RVA: 0x000D3B6C File Offset: 0x000D1D6C
		public virtual int ToolWindowCaptionHeight
		{
			get
			{
				return XplatUI.ToolWindowCaptionHeight;
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06003642 RID: 13890 RVA: 0x000D3B74 File Offset: 0x000D1D74
		public virtual int VerticalFocusThickness
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06003643 RID: 13891 RVA: 0x000D3B78 File Offset: 0x000D1D78
		public virtual int VerticalScrollBarArrowHeight
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06003644 RID: 13892 RVA: 0x000D3B7C File Offset: 0x000D1D7C
		public virtual int VerticalScrollBarThumbHeight
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06003645 RID: 13893 RVA: 0x000D3B80 File Offset: 0x000D1D80
		public virtual int VerticalScrollBarWidth
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06003646 RID: 13894 RVA: 0x000D3B84 File Offset: 0x000D1D84
		public virtual Font WindowBorderFont
		{
			get
			{
				return this.window_border_font;
			}
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x000D3B8C File Offset: 0x000D1D8C
		public int Clamp(int value, int lower, int upper)
		{
			if (value < lower)
			{
				return lower;
			}
			if (value > upper)
			{
				return upper;
			}
			return value;
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x000D3BA4 File Offset: 0x000D1DA4
		[MonoInternalNote("Figure out where to point for My Network Places")]
		public virtual string Places(UIIcon index)
		{
			switch (index)
			{
			case UIIcon.PlacesRecentDocuments:
				return Environment.GetFolderPath(8);
			case UIIcon.PlacesDesktop:
				return Environment.GetFolderPath(16);
			case UIIcon.PlacesPersonal:
				return Environment.GetFolderPath(5);
			case UIIcon.PlacesMyComputer:
				return Environment.GetFolderPath(17);
			case UIIcon.PlacesMyNetwork:
				return "/tmp";
			default:
				throw new ArgumentOutOfRangeException("index", index, "Unsupported place");
			}
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x000D3C0C File Offset: 0x000D1E0C
		private Image GetSizedResourceImage(string name, int width)
		{
			Image image = this.ResPool.GetUIImage(name, width);
			if (image != null)
			{
				return image;
			}
			if (width > 0)
			{
				string text = string.Format("{1}_{0}", name, width);
				image = ResourceImageLoader.Get(text);
				if (image != null)
				{
					this.ResPool.AddUIImage(image, name, width);
					return image;
				}
			}
			image = ResourceImageLoader.Get(name);
			if (image == null)
			{
				return null;
			}
			this.ResPool.AddUIImage(image, name, 0);
			if (image.Width != width && width != 0)
			{
				Console.Error.WriteLine("warning: requesting icon that not been tuned {0}_{1} {2}", width, name, image.Width);
				int num = image.Height * width / image.Width;
				Bitmap bitmap = new Bitmap(width, num);
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.DrawImage(image, 0, 0, width, num);
				this.ResPool.AddUIImage(bitmap, name, width);
				return bitmap;
			}
			return image;
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x000D3CF4 File Offset: 0x000D1EF4
		public virtual Image Images(UIIcon index)
		{
			return this.Images(index, 0);
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x000D3D00 File Offset: 0x000D1F00
		public virtual Image Images(UIIcon index, int size)
		{
			switch (index)
			{
			case UIIcon.PlacesRecentDocuments:
				return this.GetSizedResourceImage("document-open.png", size);
			case UIIcon.PlacesDesktop:
				return this.GetSizedResourceImage("user-desktop.png", size);
			case UIIcon.PlacesPersonal:
				return this.GetSizedResourceImage("user-home.png", size);
			case UIIcon.PlacesMyComputer:
				return this.GetSizedResourceImage("computer.png", size);
			case UIIcon.PlacesMyNetwork:
				return this.GetSizedResourceImage("folder-remote.png", size);
			case UIIcon.MessageBoxError:
				return this.GetSizedResourceImage("dialog-error.png", size);
			case UIIcon.MessageBoxQuestion:
				return this.GetSizedResourceImage("dialog-question.png", size);
			case UIIcon.MessageBoxWarning:
				return this.GetSizedResourceImage("dialog-warning.png", size);
			case UIIcon.MessageBoxInfo:
				return this.GetSizedResourceImage("dialog-information.png", size);
			case UIIcon.NormalFolder:
				return this.GetSizedResourceImage("folder.png", size);
			default:
				throw new ArgumentException("Invalid Icon type requested", "index");
			}
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x000D3DD4 File Offset: 0x000D1FD4
		public virtual Image Images(string mimetype, string extension, int size)
		{
			return null;
		}

		// Token: 0x0600364D RID: 13901
		public abstract void ResetDefaults();

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x0600364E RID: 13902
		public abstract bool DoubleBufferingSupported { get; }

		// Token: 0x0600364F RID: 13903
		public abstract void DrawOwnerDrawBackground(DrawItemEventArgs e);

		// Token: 0x06003650 RID: 13904
		public abstract void DrawOwnerDrawFocusRectangle(DrawItemEventArgs e);

		// Token: 0x06003651 RID: 13905
		public abstract Size CalculateButtonAutoSize(Button button);

		// Token: 0x06003652 RID: 13906
		public abstract void CalculateButtonTextAndImageLayout(ButtonBase b, out Rectangle textRectangle, out Rectangle imageRectangle);

		// Token: 0x06003653 RID: 13907
		public abstract void DrawButton(Graphics g, Button b, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle);

		// Token: 0x06003654 RID: 13908
		public abstract void DrawFlatButton(Graphics g, ButtonBase b, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle);

		// Token: 0x06003655 RID: 13909
		public abstract void DrawPopupButton(Graphics g, Button b, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle);

		// Token: 0x06003656 RID: 13910
		public abstract void DrawButtonBase(Graphics dc, Rectangle clip_area, ButtonBase button);

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06003657 RID: 13911
		public abstract Size ButtonBaseDefaultSize { get; }

		// Token: 0x06003658 RID: 13912
		public abstract Size CalculateCheckBoxAutoSize(CheckBox checkBox);

		// Token: 0x06003659 RID: 13913
		public abstract void CalculateCheckBoxTextAndImageLayout(ButtonBase b, Point offset, out Rectangle glyphArea, out Rectangle textRectangle, out Rectangle imageRectangle);

		// Token: 0x0600365A RID: 13914
		public abstract void DrawCheckBox(Graphics g, CheckBox cb, Rectangle glyphArea, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle);

		// Token: 0x0600365B RID: 13915
		public abstract void DrawCheckBox(Graphics dc, Rectangle clip_area, CheckBox checkbox);

		// Token: 0x0600365C RID: 13916
		public abstract void DrawCheckedListBoxItem(CheckedListBox ctrl, DrawItemEventArgs e);

		// Token: 0x0600365D RID: 13917
		public abstract void DrawComboBoxItem(ComboBox ctrl, DrawItemEventArgs e);

		// Token: 0x0600365E RID: 13918
		public abstract void DrawFlatStyleComboButton(Graphics graphics, Rectangle rectangle, ButtonState state);

		// Token: 0x0600365F RID: 13919
		public abstract void ComboBoxDrawNormalDropDownButton(ComboBox comboBox, Graphics g, Rectangle clippingArea, Rectangle area, ButtonState state);

		// Token: 0x06003660 RID: 13920
		public abstract bool ComboBoxNormalDropDownButtonHasTransparentBackground(ComboBox comboBox, ButtonState state);

		// Token: 0x06003661 RID: 13921
		public abstract bool ComboBoxDropDownButtonHasHotElementStyle(ComboBox comboBox);

		// Token: 0x06003662 RID: 13922
		public abstract void ComboBoxDrawBackground(ComboBox comboBox, Graphics g, Rectangle clippingArea, FlatStyle style);

		// Token: 0x06003663 RID: 13923
		public abstract bool CombBoxBackgroundHasHotElementStyle(ComboBox comboBox);

		// Token: 0x06003664 RID: 13924
		public abstract Font GetLinkFont(Control control);

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06003665 RID: 13925
		public abstract int DataGridPreferredColumnWidth { get; }

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06003666 RID: 13926
		public abstract int DataGridMinimumColumnCheckBoxHeight { get; }

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06003667 RID: 13927
		public abstract int DataGridMinimumColumnCheckBoxWidth { get; }

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06003668 RID: 13928
		public abstract Color DataGridAlternatingBackColor { get; }

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06003669 RID: 13929
		public abstract Color DataGridBackColor { get; }

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x0600366A RID: 13930
		public abstract Color DataGridBackgroundColor { get; }

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x0600366B RID: 13931
		public abstract Color DataGridCaptionBackColor { get; }

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x0600366C RID: 13932
		public abstract Color DataGridCaptionForeColor { get; }

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x0600366D RID: 13933
		public abstract Color DataGridGridLineColor { get; }

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x0600366E RID: 13934
		public abstract Color DataGridHeaderBackColor { get; }

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x0600366F RID: 13935
		public abstract Color DataGridHeaderForeColor { get; }

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06003670 RID: 13936
		public abstract Color DataGridLinkColor { get; }

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06003671 RID: 13937
		public abstract Color DataGridLinkHoverColor { get; }

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06003672 RID: 13938
		public abstract Color DataGridParentRowsBackColor { get; }

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06003673 RID: 13939
		public abstract Color DataGridParentRowsForeColor { get; }

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06003674 RID: 13940
		public abstract Color DataGridSelectionBackColor { get; }

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06003675 RID: 13941
		public abstract Color DataGridSelectionForeColor { get; }

		// Token: 0x06003676 RID: 13942
		public abstract void DataGridPaint(PaintEventArgs pe, DataGrid grid);

		// Token: 0x06003677 RID: 13943
		public abstract void DataGridPaintCaption(Graphics g, Rectangle clip, DataGrid grid);

		// Token: 0x06003678 RID: 13944
		public abstract void DataGridPaintColumnHeaders(Graphics g, Rectangle clip, DataGrid grid);

		// Token: 0x06003679 RID: 13945
		public abstract void DataGridPaintColumnHeader(Graphics g, Rectangle bounds, DataGrid grid, int col);

		// Token: 0x0600367A RID: 13946
		public abstract void DataGridPaintRowContents(Graphics g, int row, Rectangle row_rect, bool is_newrow, Rectangle clip, DataGrid grid);

		// Token: 0x0600367B RID: 13947
		public abstract void DataGridPaintRowHeader(Graphics g, Rectangle bounds, int row, DataGrid grid);

		// Token: 0x0600367C RID: 13948
		public abstract void DataGridPaintRowHeaderArrow(Graphics g, Rectangle bounds, DataGrid grid);

		// Token: 0x0600367D RID: 13949
		public abstract void DataGridPaintRowHeaderStar(Graphics g, Rectangle bounds, DataGrid grid);

		// Token: 0x0600367E RID: 13950
		public abstract void DataGridPaintParentRows(Graphics g, Rectangle bounds, DataGrid grid);

		// Token: 0x0600367F RID: 13951
		public abstract void DataGridPaintParentRow(Graphics g, Rectangle bounds, DataGridDataSource row, DataGrid grid);

		// Token: 0x06003680 RID: 13952
		public abstract void DataGridPaintRows(Graphics g, Rectangle cells, Rectangle clip, DataGrid grid);

		// Token: 0x06003681 RID: 13953
		public abstract void DataGridPaintRow(Graphics g, int row, Rectangle row_rect, bool is_newrow, Rectangle clip, DataGrid grid);

		// Token: 0x06003682 RID: 13954
		public abstract void DataGridPaintRelationRow(Graphics g, int row, Rectangle row_rect, bool is_newrow, Rectangle clip, DataGrid grid);

		// Token: 0x06003683 RID: 13955
		public abstract bool DataGridViewRowHeaderCellDrawBackground(DataGridViewRowHeaderCell cell, Graphics g, Rectangle bounds);

		// Token: 0x06003684 RID: 13956
		public abstract bool DataGridViewRowHeaderCellDrawSelectionBackground(DataGridViewRowHeaderCell cell);

		// Token: 0x06003685 RID: 13957
		public abstract bool DataGridViewRowHeaderCellDrawBorder(DataGridViewRowHeaderCell cell, Graphics g, Rectangle bounds);

		// Token: 0x06003686 RID: 13958
		public abstract bool DataGridViewColumnHeaderCellDrawBackground(DataGridViewColumnHeaderCell cell, Graphics g, Rectangle bounds);

		// Token: 0x06003687 RID: 13959
		public abstract bool DataGridViewColumnHeaderCellDrawBorder(DataGridViewColumnHeaderCell cell, Graphics g, Rectangle bounds);

		// Token: 0x06003688 RID: 13960
		public abstract bool DataGridViewHeaderCellHasPressedStyle(DataGridView dataGridView);

		// Token: 0x06003689 RID: 13961
		public abstract bool DataGridViewHeaderCellHasHotStyle(DataGridView dataGridView);

		// Token: 0x0600368A RID: 13962
		public abstract void DrawDateTimePicker(Graphics dc, Rectangle clip_rectangle, DateTimePicker dtp);

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x0600368B RID: 13963
		public abstract bool DateTimePickerBorderHasHotElementStyle { get; }

		// Token: 0x0600368C RID: 13964
		public abstract Rectangle DateTimePickerGetDropDownButtonArea(DateTimePicker dateTimePicker);

		// Token: 0x0600368D RID: 13965
		public abstract Rectangle DateTimePickerGetDateArea(DateTimePicker dateTimePicker);

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x0600368E RID: 13966
		public abstract bool DateTimePickerDropDownButtonHasHotElementStyle { get; }

		// Token: 0x0600368F RID: 13967
		public abstract void DrawGroupBox(Graphics dc, Rectangle clip_area, GroupBox box);

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06003690 RID: 13968
		public abstract Size GroupBoxDefaultSize { get; }

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06003691 RID: 13969
		public abstract Size HScrollBarDefaultSize { get; }

		// Token: 0x06003692 RID: 13970
		public abstract void DrawListBoxItem(ListBox ctrl, DrawItemEventArgs e);

		// Token: 0x06003693 RID: 13971
		public abstract void DrawListViewItems(Graphics dc, Rectangle clip_rectangle, ListView control);

		// Token: 0x06003694 RID: 13972
		public abstract void DrawListViewHeader(Graphics dc, Rectangle clip_rectangle, ListView control);

		// Token: 0x06003695 RID: 13973
		public abstract void DrawListViewHeaderDragDetails(Graphics dc, ListView control, ColumnHeader drag_column, int target_x);

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06003696 RID: 13974
		public abstract bool ListViewHasHotHeaderStyle { get; }

		// Token: 0x06003697 RID: 13975
		public abstract int ListViewGetHeaderHeight(ListView listView, Font font);

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06003698 RID: 13976
		public abstract Size ListViewCheckBoxSize { get; }

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06003699 RID: 13977
		public abstract int ListViewColumnHeaderHeight { get; }

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x0600369A RID: 13978
		public abstract int ListViewDefaultColumnWidth { get; }

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x0600369B RID: 13979
		public abstract int ListViewVerticalSpacing { get; }

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x0600369C RID: 13980
		public abstract int ListViewEmptyColumnWidth { get; }

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x0600369D RID: 13981
		public abstract int ListViewHorizontalSpacing { get; }

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x0600369E RID: 13982
		public abstract Size ListViewDefaultSize { get; }

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x0600369F RID: 13983
		public abstract int ListViewGroupHeight { get; }

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x060036A0 RID: 13984
		public abstract int ListViewItemPaddingWidth { get; }

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x060036A1 RID: 13985
		public abstract int ListViewTileWidthFactor { get; }

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x060036A2 RID: 13986
		public abstract int ListViewTileHeightFactor { get; }

		// Token: 0x060036A3 RID: 13987
		public abstract void CalcItemSize(Graphics dc, MenuItem item, int y, int x, bool menuBar);

		// Token: 0x060036A4 RID: 13988
		public abstract void CalcPopupMenuSize(Graphics dc, Menu menu);

		// Token: 0x060036A5 RID: 13989
		public abstract int CalcMenuBarSize(Graphics dc, Menu menu, int width);

		// Token: 0x060036A6 RID: 13990
		public abstract void DrawMenuBar(Graphics dc, Menu menu, Rectangle rect);

		// Token: 0x060036A7 RID: 13991
		public abstract void DrawMenuItem(MenuItem item, DrawItemEventArgs e);

		// Token: 0x060036A8 RID: 13992
		public abstract void DrawPopupMenu(Graphics dc, Menu menu, Rectangle cliparea, Rectangle rect);

		// Token: 0x060036A9 RID: 13993
		public abstract void DrawMonthCalendar(Graphics dc, Rectangle clip_rectangle, MonthCalendar month_calendar);

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x060036AA RID: 13994
		public abstract Size PanelDefaultSize { get; }

		// Token: 0x060036AB RID: 13995
		public abstract void DrawPictureBox(Graphics dc, Rectangle clip, PictureBox pb);

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x060036AC RID: 13996
		public abstract Size PictureBoxDefaultSize { get; }

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x060036AD RID: 13997
		public abstract int PrintPreviewControlPadding { get; }

		// Token: 0x060036AE RID: 13998
		public abstract Size PrintPreviewControlGetPageSize(PrintPreviewControl preview);

		// Token: 0x060036AF RID: 13999
		public abstract void PrintPreviewControlPaint(PaintEventArgs pe, PrintPreviewControl preview, Size page_image_size);

		// Token: 0x060036B0 RID: 14000
		public abstract void DrawProgressBar(Graphics dc, Rectangle clip_rectangle, ProgressBar progress_bar);

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x060036B1 RID: 14001
		public abstract Size ProgressBarDefaultSize { get; }

		// Token: 0x060036B2 RID: 14002
		public abstract Size CalculateRadioButtonAutoSize(RadioButton rb);

		// Token: 0x060036B3 RID: 14003
		public abstract void CalculateRadioButtonTextAndImageLayout(ButtonBase b, Point offset, out Rectangle glyphArea, out Rectangle textRectangle, out Rectangle imageRectangle);

		// Token: 0x060036B4 RID: 14004
		public abstract void DrawRadioButton(Graphics g, RadioButton rb, Rectangle glyphArea, Rectangle textBounds, Rectangle imageBounds, Rectangle clipRectangle);

		// Token: 0x060036B5 RID: 14005
		public abstract void DrawRadioButton(Graphics dc, Rectangle clip_rectangle, RadioButton radio_button);

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x060036B6 RID: 14006
		public abstract Size RadioButtonDefaultSize { get; }

		// Token: 0x060036B7 RID: 14007
		public abstract void DrawScrollBar(Graphics dc, Rectangle clip_rectangle, ScrollBar bar);

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x060036B8 RID: 14008
		public abstract int ScrollBarButtonSize { get; }

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x060036B9 RID: 14009
		public abstract bool ScrollBarHasHotElementStyles { get; }

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x060036BA RID: 14010
		public abstract bool ScrollBarHasPressedThumbStyle { get; }

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x060036BB RID: 14011
		public abstract bool ScrollBarHasHoverArrowButtonStyle { get; }

		// Token: 0x060036BC RID: 14012
		public abstract void DrawStatusBar(Graphics dc, Rectangle clip_rectangle, StatusBar sb);

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x060036BD RID: 14013
		public abstract int StatusBarSizeGripWidth { get; }

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x060036BE RID: 14014
		public abstract int StatusBarHorzGapWidth { get; }

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x060036BF RID: 14015
		public abstract Size StatusBarDefaultSize { get; }

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x060036C0 RID: 14016
		public abstract Size TabControlDefaultItemSize { get; }

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x060036C1 RID: 14017
		public abstract Point TabControlDefaultPadding { get; }

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x060036C2 RID: 14018
		public abstract int TabControlMinimumTabWidth { get; }

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x060036C3 RID: 14019
		public abstract Rectangle TabControlSelectedDelta { get; }

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x060036C4 RID: 14020
		public abstract int TabControlSelectedSpacing { get; }

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x060036C5 RID: 14021
		public abstract int TabPanelOffsetX { get; }

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x060036C6 RID: 14022
		public abstract int TabPanelOffsetY { get; }

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x060036C7 RID: 14023
		public abstract int TabControlColSpacing { get; }

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x060036C8 RID: 14024
		public abstract Point TabControlImagePadding { get; }

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x060036C9 RID: 14025
		public abstract int TabControlScrollerWidth { get; }

		// Token: 0x060036CA RID: 14026
		public abstract Rectangle TabControlGetDisplayRectangle(TabControl tab);

		// Token: 0x060036CB RID: 14027
		public abstract Rectangle TabControlGetPanelRect(TabControl tab);

		// Token: 0x060036CC RID: 14028
		public abstract Size TabControlGetSpacing(TabControl tab);

		// Token: 0x060036CD RID: 14029
		public abstract void DrawTabControl(Graphics dc, Rectangle area, TabControl tab);

		// Token: 0x060036CE RID: 14030
		public abstract void TextBoxBaseFillBackground(TextBoxBase textBoxBase, Graphics g, Rectangle clippingArea);

		// Token: 0x060036CF RID: 14031
		public abstract bool TextBoxBaseHandleWmNcPaint(TextBoxBase textBoxBase, ref Message m);

		// Token: 0x060036D0 RID: 14032
		public abstract bool TextBoxBaseShouldPaintBackground(TextBoxBase textBoxBase);

		// Token: 0x060036D1 RID: 14033
		public abstract void DrawToolBar(Graphics dc, Rectangle clip_rectangle, ToolBar control);

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x060036D2 RID: 14034
		public abstract int ToolBarGripWidth { get; }

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x060036D3 RID: 14035
		public abstract int ToolBarImageGripWidth { get; }

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x060036D4 RID: 14036
		public abstract int ToolBarSeparatorWidth { get; }

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x060036D5 RID: 14037
		public abstract int ToolBarDropDownWidth { get; }

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x060036D6 RID: 14038
		public abstract int ToolBarDropDownArrowWidth { get; }

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x060036D7 RID: 14039
		public abstract int ToolBarDropDownArrowHeight { get; }

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x060036D8 RID: 14040
		public abstract Size ToolBarDefaultSize { get; }

		// Token: 0x060036D9 RID: 14041
		public abstract bool ToolBarHasHotElementStyles(ToolBar toolBar);

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x060036DA RID: 14042
		public abstract bool ToolBarHasHotCheckedElementStyles { get; }

		// Token: 0x060036DB RID: 14043
		public abstract void DrawToolTip(Graphics dc, Rectangle clip_rectangle, ToolTip.ToolTipWindow control);

		// Token: 0x060036DC RID: 14044
		public abstract Size ToolTipSize(ToolTip.ToolTipWindow tt, string text);

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x060036DD RID: 14045
		public abstract bool ToolTipTransparentBackground { get; }

		// Token: 0x060036DE RID: 14046
		public abstract void ShowBalloonWindow(IntPtr handle, int timeout, string title, string text, ToolTipIcon icon);

		// Token: 0x060036DF RID: 14047
		public abstract void DrawBalloonWindow(Graphics dc, Rectangle clip, NotifyIcon.BalloonWindow control);

		// Token: 0x060036E0 RID: 14048
		public abstract Rectangle BalloonWindowRect(NotifyIcon.BalloonWindow control);

		// Token: 0x060036E1 RID: 14049
		public abstract void DrawTrackBar(Graphics dc, Rectangle clip_rectangle, TrackBar tb);

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x060036E2 RID: 14050
		public abstract Size TrackBarDefaultSize { get; }

		// Token: 0x060036E3 RID: 14051
		public abstract int TrackBarValueFromMousePosition(int x, int y, TrackBar tb);

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x060036E4 RID: 14052
		public abstract bool TrackBarHasHotThumbStyle { get; }

		// Token: 0x060036E5 RID: 14053
		public abstract void UpDownBaseDrawButton(Graphics g, Rectangle bounds, bool top, PushButtonState state);

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x060036E6 RID: 14054
		public abstract bool UpDownBaseHasHotButtonStyle { get; }

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x060036E7 RID: 14055
		public abstract Size VScrollBarDefaultSize { get; }

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x060036E8 RID: 14056
		public abstract Size TreeViewDefaultSize { get; }

		// Token: 0x060036E9 RID: 14057
		public abstract void TreeViewDrawNodePlusMinus(TreeView treeView, TreeNode node, Graphics dc, int x, int middle);

		// Token: 0x060036EA RID: 14058
		public abstract void DrawManagedWindowDecorations(Graphics dc, Rectangle clip, InternalWindowManager wm);

		// Token: 0x060036EB RID: 14059
		public abstract int ManagedWindowTitleBarHeight(InternalWindowManager wm);

		// Token: 0x060036EC RID: 14060
		public abstract int ManagedWindowBorderWidth(InternalWindowManager wm);

		// Token: 0x060036ED RID: 14061
		public abstract int ManagedWindowIconWidth(InternalWindowManager wm);

		// Token: 0x060036EE RID: 14062
		public abstract Size ManagedWindowButtonSize(InternalWindowManager wm);

		// Token: 0x060036EF RID: 14063
		public abstract void ManagedWindowSetButtonLocations(InternalWindowManager wm);

		// Token: 0x060036F0 RID: 14064
		public abstract Rectangle ManagedWindowGetTitleBarIconArea(InternalWindowManager wm);

		// Token: 0x060036F1 RID: 14065
		public abstract Size ManagedWindowGetMenuButtonSize(InternalWindowManager wm);

		// Token: 0x060036F2 RID: 14066
		public abstract bool ManagedWindowTitleButtonHasHotElementStyle(TitleButton button, Form form);

		// Token: 0x060036F3 RID: 14067
		public abstract void ManagedWindowDrawMenuButton(Graphics dc, TitleButton button, Rectangle clip, InternalWindowManager wm);

		// Token: 0x060036F4 RID: 14068
		public abstract void ManagedWindowOnSizeInitializedOrChanged(Form form);

		// Token: 0x060036F5 RID: 14069
		public abstract void CPDrawBorder(Graphics graphics, Rectangle bounds, Color leftColor, int leftWidth, ButtonBorderStyle leftStyle, Color topColor, int topWidth, ButtonBorderStyle topStyle, Color rightColor, int rightWidth, ButtonBorderStyle rightStyle, Color bottomColor, int bottomWidth, ButtonBorderStyle bottomStyle);

		// Token: 0x060036F6 RID: 14070
		public abstract void CPDrawBorder(Graphics graphics, RectangleF bounds, Color leftColor, int leftWidth, ButtonBorderStyle leftStyle, Color topColor, int topWidth, ButtonBorderStyle topStyle, Color rightColor, int rightWidth, ButtonBorderStyle rightStyle, Color bottomColor, int bottomWidth, ButtonBorderStyle bottomStyle);

		// Token: 0x060036F7 RID: 14071
		public abstract void CPDrawBorder3D(Graphics graphics, Rectangle rectangle, Border3DStyle style, Border3DSide sides);

		// Token: 0x060036F8 RID: 14072
		public abstract void CPDrawBorder3D(Graphics graphics, Rectangle rectangle, Border3DStyle style, Border3DSide sides, Color control_color);

		// Token: 0x060036F9 RID: 14073
		public abstract void CPDrawButton(Graphics graphics, Rectangle rectangle, ButtonState state);

		// Token: 0x060036FA RID: 14074
		public abstract void CPDrawCaptionButton(Graphics graphics, Rectangle rectangle, CaptionButton button, ButtonState state);

		// Token: 0x060036FB RID: 14075
		public abstract void CPDrawCheckBox(Graphics graphics, Rectangle rectangle, ButtonState state);

		// Token: 0x060036FC RID: 14076
		public abstract void CPDrawComboButton(Graphics graphics, Rectangle rectangle, ButtonState state);

		// Token: 0x060036FD RID: 14077
		public abstract void CPDrawContainerGrabHandle(Graphics graphics, Rectangle bounds);

		// Token: 0x060036FE RID: 14078
		public abstract void CPDrawFocusRectangle(Graphics graphics, Rectangle rectangle, Color foreColor, Color backColor);

		// Token: 0x060036FF RID: 14079
		public abstract void CPDrawGrabHandle(Graphics graphics, Rectangle rectangle, bool primary, bool enabled);

		// Token: 0x06003700 RID: 14080
		public abstract void CPDrawGrid(Graphics graphics, Rectangle area, Size pixelsBetweenDots, Color backColor);

		// Token: 0x06003701 RID: 14081
		public abstract void CPDrawImageDisabled(Graphics graphics, Image image, int x, int y, Color background);

		// Token: 0x06003702 RID: 14082
		public abstract void CPDrawLockedFrame(Graphics graphics, Rectangle rectangle, bool primary);

		// Token: 0x06003703 RID: 14083
		public abstract void CPDrawMenuGlyph(Graphics graphics, Rectangle rectangle, MenuGlyph glyph, Color color, Color backColor);

		// Token: 0x06003704 RID: 14084
		public abstract void CPDrawMixedCheckBox(Graphics graphics, Rectangle rectangle, ButtonState state);

		// Token: 0x06003705 RID: 14085
		public abstract void CPDrawRadioButton(Graphics graphics, Rectangle rectangle, ButtonState state);

		// Token: 0x06003706 RID: 14086
		public abstract void CPDrawReversibleFrame(Rectangle rectangle, Color backColor, FrameStyle style);

		// Token: 0x06003707 RID: 14087
		public abstract void CPDrawReversibleLine(Point start, Point end, Color backColor);

		// Token: 0x06003708 RID: 14088
		public abstract void CPDrawScrollButton(Graphics graphics, Rectangle rectangle, ScrollButton button, ButtonState state);

		// Token: 0x06003709 RID: 14089
		public abstract void CPDrawSelectionFrame(Graphics graphics, bool active, Rectangle outsideRect, Rectangle insideRect, Color backColor);

		// Token: 0x0600370A RID: 14090
		public abstract void CPDrawSizeGrip(Graphics graphics, Color backColor, Rectangle bounds);

		// Token: 0x0600370B RID: 14091
		public abstract void CPDrawStringDisabled(Graphics graphics, string s, Font font, Color color, RectangleF layoutRectangle, StringFormat format);

		// Token: 0x0600370C RID: 14092
		public abstract void CPDrawStringDisabled(IDeviceContext dc, string s, Font font, Color color, Rectangle layoutRectangle, TextFormatFlags format);

		// Token: 0x0600370D RID: 14093
		public abstract void CPDrawVisualStyleBorder(Graphics graphics, Rectangle bounds);

		// Token: 0x0600370E RID: 14094
		public abstract void CPDrawBorderStyle(Graphics dc, Rectangle area, BorderStyle border_style);

		// Token: 0x0400198A RID: 6538
		public const int ManagedWindowSpacingAfterLastTitleButton = 2;

		// Token: 0x0400198B RID: 6539
		protected Array syscolors;

		// Token: 0x0400198C RID: 6540
		private readonly Font default_font;

		// Token: 0x0400198D RID: 6541
		protected Font window_border_font;

		// Token: 0x0400198E RID: 6542
		protected Color defaultWindowBackColor;

		// Token: 0x0400198F RID: 6543
		protected Color defaultWindowForeColor;

		// Token: 0x04001990 RID: 6544
		internal SystemResPool ResPool = new SystemResPool();

		// Token: 0x04001991 RID: 6545
		private MethodInfo update;
	}
}
