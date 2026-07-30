using System;

namespace System.Drawing
{
	/// <summary>Each property of the <see cref="T:System.Drawing.SystemColors" /> class is a <see cref="T:System.Drawing.Color" /> structure that is the color of a Windows display element.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000089 RID: 137
	public sealed class SystemColors
	{
		// Token: 0x06000724 RID: 1828 RVA: 0x00002050 File Offset: 0x00000250
		private SystemColors()
		{
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the active window's border.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the active window's border.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001477A File Offset: 0x0001297A
		public static Color ActiveBorder
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.ActiveBorder);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of the active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the active window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x00014782 File Offset: 0x00012982
		public static Color ActiveCaption
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.ActiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text in the active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text in the active window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001478A File Offset: 0x0001298A
		public static Color ActiveCaptionText
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.ActiveCaptionText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the application workspace. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the application workspace.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x00014792 File Offset: 0x00012992
		public static Color AppWorkspace
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.AppWorkspace);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the face color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the face color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001479A File Offset: 0x0001299A
		public static Color Control
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.Control);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the shadow color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the shadow color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x000147A2 File Offset: 0x000129A2
		public static Color ControlDark
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.ControlDark);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the dark shadow color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the dark shadow color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x000147AA File Offset: 0x000129AA
		public static Color ControlDarkDark
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.ControlDarkDark);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the light color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the light color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x000147B2 File Offset: 0x000129B2
		public static Color ControlLight
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.ControlLight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the highlight color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the highlight color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x000147BA File Offset: 0x000129BA
		public static Color ControlLightLight
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.ControlLightLight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of text in a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of text in a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x000147C3 File Offset: 0x000129C3
		public static Color ControlText
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.ControlText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the desktop.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the desktop.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x000147CC File Offset: 0x000129CC
		public static Color Desktop
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.Desktop);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of dimmed text. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of dimmed text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x000147D5 File Offset: 0x000129D5
		public static Color GrayText
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.GrayText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of selected items.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of selected items.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x000147DE File Offset: 0x000129DE
		public static Color Highlight
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.Highlight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text of selected items.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text of selected items.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x000147E7 File Offset: 0x000129E7
		public static Color HighlightText
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.HighlightText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color used to designate a hot-tracked item. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color used to designate a hot-tracked item.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x000147F0 File Offset: 0x000129F0
		public static Color HotTrack
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.HotTrack);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of an inactive window's border.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of an inactive window's border.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x000147F9 File Offset: 0x000129F9
		public static Color InactiveBorder
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.InactiveBorder);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of an inactive window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x00014802 File Offset: 0x00012A02
		public static Color InactiveCaption
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.InactiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text in an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text in an inactive window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001480B File Offset: 0x00012A0B
		public static Color InactiveCaptionText
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.InactiveCaptionText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of a ToolTip.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of a ToolTip.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x00014814 File Offset: 0x00012A14
		public static Color Info
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.Info);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text of a ToolTip.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text of a ToolTip.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001481D File Offset: 0x00012A1D
		public static Color InfoText
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.InfoText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of a menu's background.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of a menu's background.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00014826 File Offset: 0x00012A26
		public static Color Menu
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.Menu);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of a menu's text.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of a menu's text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001482F File Offset: 0x00012A2F
		public static Color MenuText
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.MenuText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of a scroll bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of a scroll bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00014838 File Offset: 0x00012A38
		public static Color ScrollBar
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.ScrollBar);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background in the client area of a window.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background in the client area of a window.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x00014841 File Offset: 0x00012A41
		public static Color Window
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.Window);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of a window frame.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of a window frame.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001484A File Offset: 0x00012A4A
		public static Color WindowFrame
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.WindowFrame);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the text in the client area of a window.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the text in the client area of a window.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x00014853 File Offset: 0x00012A53
		public static Color WindowText
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.WindowText);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the face color of a 3-D element.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the face color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x0001485C File Offset: 0x00012A5C
		public static Color ButtonFace
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.ButtonFace);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the highlight color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the highlight color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x00014868 File Offset: 0x00012A68
		public static Color ButtonHighlight
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.ButtonHighlight);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the shadow color of a 3-D element. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the shadow color of a 3-D element.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x00014874 File Offset: 0x00012A74
		public static Color ButtonShadow
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.ButtonShadow);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the lightest color in the color gradient of an active window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the lightest color in the color gradient of an active window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x00014880 File Offset: 0x00012A80
		public static Color GradientActiveCaption
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.GradientActiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the lightest color in the color gradient of an inactive window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the lightest color in the color gradient of an inactive window's title bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x0001488C File Offset: 0x00012A8C
		public static Color GradientInactiveCaption
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.GradientInactiveCaption);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color of the background of a menu bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color of the background of a menu bar.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00014898 File Offset: 0x00012A98
		public static Color MenuBar
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.MenuBar);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Color" /> structure that is the color used to highlight menu items when the menu appears as a flat menu.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color used to highlight menu items when the menu appears as a flat menu.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x000148A4 File Offset: 0x00012AA4
		public static Color MenuHighlight
		{
			get
			{
				return KnownColors.FromKnownColor(KnownColor.MenuHighlight);
			}
		}
	}
}
