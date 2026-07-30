using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides <see cref="T:System.Drawing.Color" /> structures that are colors of a Windows display element. This class cannot be inherited. </summary>
	// Token: 0x02000297 RID: 663
	public sealed class ProfessionalColors
	{
		// Token: 0x06002BA6 RID: 11174 RVA: 0x000A75E0 File Offset: 0x000A57E0
		private ProfessionalColors()
		{
		}

		/// <summary>Gets the starting color of the gradient used when the button is checked.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the gradient used when the button is checked.</returns>
		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x000A75F4 File Offset: 0x000A57F4
		public static Color ButtonCheckedGradientBegin
		{
			get
			{
				return ProfessionalColors.color_table.ButtonCheckedGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used when the button is checked.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is checked.</returns>
		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06002BA9 RID: 11177 RVA: 0x000A7600 File Offset: 0x000A5800
		public static Color ButtonCheckedGradientEnd
		{
			get
			{
				return ProfessionalColors.color_table.ButtonCheckedGradientEnd;
			}
		}

		/// <summary>Gets the middle color of the gradient used when the button is checked.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when the button is checked.</returns>
		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06002BAA RID: 11178 RVA: 0x000A760C File Offset: 0x000A580C
		public static Color ButtonCheckedGradientMiddle
		{
			get
			{
				return ProfessionalColors.color_table.ButtonCheckedGradientMiddle;
			}
		}

		/// <summary>Gets the solid color used when the button is checked.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color used when the button is checked.</returns>
		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06002BAB RID: 11179 RVA: 0x000A7618 File Offset: 0x000A5818
		public static Color ButtonCheckedHighlight
		{
			get
			{
				return ProfessionalColors.color_table.ButtonCheckedHighlight;
			}
		}

		/// <summary>Gets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonCheckedHighlight" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonCheckedHighlight" />.</returns>
		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06002BAC RID: 11180 RVA: 0x000A7624 File Offset: 0x000A5824
		public static Color ButtonCheckedHighlightBorder
		{
			get
			{
				return ProfessionalColors.color_table.ButtonCheckedHighlightBorder;
			}
		}

		/// <summary>Gets the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedGradientEnd" /> colors.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedGradientEnd" /> colors.</returns>
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06002BAD RID: 11181 RVA: 0x000A7630 File Offset: 0x000A5830
		public static Color ButtonPressedBorder
		{
			get
			{
				return ProfessionalColors.color_table.ButtonPressedBorder;
			}
		}

		/// <summary>Gets the starting color of the gradient used when the button is pressed down.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the button is pressed down.</returns>
		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06002BAE RID: 11182 RVA: 0x000A763C File Offset: 0x000A583C
		public static Color ButtonPressedGradientBegin
		{
			get
			{
				return ProfessionalColors.color_table.ButtonPressedGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used when the button is pressed down.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is pressed down.</returns>
		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06002BAF RID: 11183 RVA: 0x000A7648 File Offset: 0x000A5848
		public static Color ButtonPressedGradientEnd
		{
			get
			{
				return ProfessionalColors.color_table.ButtonPressedGradientEnd;
			}
		}

		/// <summary>Gets the middle color of the gradient used when the button is pressed down.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when the button is pressed.</returns>
		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06002BB0 RID: 11184 RVA: 0x000A7654 File Offset: 0x000A5854
		public static Color ButtonPressedGradientMiddle
		{
			get
			{
				return ProfessionalColors.color_table.ButtonPressedGradientMiddle;
			}
		}

		/// <summary>Gets the solid color used when the button is pressed down.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color used when the button is pressed down.</returns>
		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06002BB1 RID: 11185 RVA: 0x000A7660 File Offset: 0x000A5860
		public static Color ButtonPressedHighlight
		{
			get
			{
				return ProfessionalColors.color_table.ButtonPressedHighlight;
			}
		}

		/// <summary>Gets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedHighlight" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonPressedHighlight" />.</returns>
		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06002BB2 RID: 11186 RVA: 0x000A766C File Offset: 0x000A586C
		public static Color ButtonPressedHighlightBorder
		{
			get
			{
				return ProfessionalColors.color_table.ButtonPressedHighlightBorder;
			}
		}

		/// <summary>Gets the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedGradientEnd" /> colors.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedGradientEnd" /> colors.</returns>
		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06002BB3 RID: 11187 RVA: 0x000A7678 File Offset: 0x000A5878
		public static Color ButtonSelectedBorder
		{
			get
			{
				return ProfessionalColors.color_table.ButtonSelectedBorder;
			}
		}

		/// <summary>Gets the starting color of the gradient used when the button is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the button is selected.</returns>
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06002BB4 RID: 11188 RVA: 0x000A7684 File Offset: 0x000A5884
		public static Color ButtonSelectedGradientBegin
		{
			get
			{
				return ProfessionalColors.color_table.ButtonSelectedGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used when the button is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is selected.</returns>
		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06002BB5 RID: 11189 RVA: 0x000A7690 File Offset: 0x000A5890
		public static Color ButtonSelectedGradientEnd
		{
			get
			{
				return ProfessionalColors.color_table.ButtonSelectedGradientEnd;
			}
		}

		/// <summary>Gets the middle color of the gradient used when the button is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when the button is selected.</returns>
		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x000A769C File Offset: 0x000A589C
		public static Color ButtonSelectedGradientMiddle
		{
			get
			{
				return ProfessionalColors.color_table.ButtonSelectedGradientMiddle;
			}
		}

		/// <summary>Gets the solid color used when the button is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color used when the button is selected.</returns>
		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06002BB7 RID: 11191 RVA: 0x000A76A8 File Offset: 0x000A58A8
		public static Color ButtonSelectedHighlight
		{
			get
			{
				return ProfessionalColors.color_table.ButtonSelectedHighlight;
			}
		}

		/// <summary>Gets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedHighlight" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColors.ButtonSelectedHighlight" />.</returns>
		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06002BB8 RID: 11192 RVA: 0x000A76B4 File Offset: 0x000A58B4
		public static Color ButtonSelectedHighlightBorder
		{
			get
			{
				return ProfessionalColors.color_table.ButtonSelectedHighlightBorder;
			}
		}

		/// <summary>Gets the solid color to use when the check box is selected and gradients are being used.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when the check box is selected and gradients are being used.</returns>
		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06002BB9 RID: 11193 RVA: 0x000A76C0 File Offset: 0x000A58C0
		public static Color CheckBackground
		{
			get
			{
				return ProfessionalColors.color_table.CheckBackground;
			}
		}

		/// <summary>Gets the solid color to use when the check box is selected and gradients are being used.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when the check box is selected and gradients are being used.</returns>
		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06002BBA RID: 11194 RVA: 0x000A76CC File Offset: 0x000A58CC
		public static Color CheckPressedBackground
		{
			get
			{
				return ProfessionalColors.color_table.CheckPressedBackground;
			}
		}

		/// <summary>Gets the solid color to use when the check box is selected and gradients are being used.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when the check box is selected and gradients are being used.</returns>
		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06002BBB RID: 11195 RVA: 0x000A76D8 File Offset: 0x000A58D8
		public static Color CheckSelectedBackground
		{
			get
			{
				return ProfessionalColors.color_table.CheckSelectedBackground;
			}
		}

		/// <summary>Gets the color to use for shadow effects on the grip or move handle.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use for shadow effects on the grip or move handle.</returns>
		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06002BBC RID: 11196 RVA: 0x000A76E4 File Offset: 0x000A58E4
		public static Color GripDark
		{
			get
			{
				return ProfessionalColors.color_table.GripDark;
			}
		}

		/// <summary>Gets the color to use for highlight effects on the grip or move handle.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use for highlight effects on the grip or move handle.</returns>
		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06002BBD RID: 11197 RVA: 0x000A76F0 File Offset: 0x000A58F0
		public static Color GripLight
		{
			get
			{
				return ProfessionalColors.color_table.GripLight;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</returns>
		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06002BBE RID: 11198 RVA: 0x000A76FC File Offset: 0x000A58FC
		public static Color ImageMarginGradientBegin
		{
			get
			{
				return ProfessionalColors.color_table.ImageMarginGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</returns>
		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06002BBF RID: 11199 RVA: 0x000A7708 File Offset: 0x000A5908
		public static Color ImageMarginGradientEnd
		{
			get
			{
				return ProfessionalColors.color_table.ImageMarginGradientEnd;
			}
		}

		/// <summary>Gets the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</returns>
		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06002BC0 RID: 11200 RVA: 0x000A7714 File Offset: 0x000A5914
		public static Color ImageMarginGradientMiddle
		{
			get
			{
				return ProfessionalColors.color_table.ImageMarginGradientMiddle;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</returns>
		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06002BC1 RID: 11201 RVA: 0x000A7720 File Offset: 0x000A5920
		public static Color ImageMarginRevealedGradientBegin
		{
			get
			{
				return ProfessionalColors.color_table.ImageMarginRevealedGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</returns>
		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06002BC2 RID: 11202 RVA: 0x000A772C File Offset: 0x000A592C
		public static Color ImageMarginRevealedGradientEnd
		{
			get
			{
				return ProfessionalColors.color_table.ImageMarginRevealedGradientEnd;
			}
		}

		/// <summary>Gets the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</returns>
		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06002BC3 RID: 11203 RVA: 0x000A7738 File Offset: 0x000A5938
		public static Color ImageMarginRevealedGradientMiddle
		{
			get
			{
				return ProfessionalColors.color_table.ImageMarginRevealedGradientMiddle;
			}
		}

		/// <summary>Gets the border color or a <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color or a <see cref="T:System.Windows.Forms.MenuStrip" />.</returns>
		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x000A7744 File Offset: 0x000A5944
		public static Color MenuBorder
		{
			get
			{
				return ProfessionalColors.color_table.MenuBorder;
			}
		}

		/// <summary>Gets the border color to use with a <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with a <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</returns>
		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x000A7750 File Offset: 0x000A5950
		public static Color MenuItemBorder
		{
			get
			{
				return ProfessionalColors.color_table.MenuItemBorder;
			}
		}

		/// <summary>Gets the starting color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed down.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed down.</returns>
		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x000A775C File Offset: 0x000A595C
		public static Color MenuItemPressedGradientBegin
		{
			get
			{
				return ProfessionalColors.color_table.MenuItemPressedGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed down.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed down.</returns>
		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06002BC7 RID: 11207 RVA: 0x000A7768 File Offset: 0x000A5968
		public static Color MenuItemPressedGradientEnd
		{
			get
			{
				return ProfessionalColors.color_table.MenuItemPressedGradientEnd;
			}
		}

		/// <summary>Gets the middle color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed down.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed down.</returns>
		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x000A7774 File Offset: 0x000A5974
		public static Color MenuItemPressedGradientMiddle
		{
			get
			{
				return ProfessionalColors.color_table.MenuItemPressedGradientMiddle;
			}
		}

		/// <summary>Gets the solid color to use when a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> other than the top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> other than the top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</returns>
		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06002BC9 RID: 11209 RVA: 0x000A7780 File Offset: 0x000A5980
		public static Color MenuItemSelected
		{
			get
			{
				return ProfessionalColors.color_table.MenuItemSelected;
			}
		}

		/// <summary>Gets the starting color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</returns>
		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06002BCA RID: 11210 RVA: 0x000A778C File Offset: 0x000A598C
		public static Color MenuItemSelectedGradientBegin
		{
			get
			{
				return ProfessionalColors.color_table.MenuItemSelectedGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</returns>
		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06002BCB RID: 11211 RVA: 0x000A7798 File Offset: 0x000A5998
		public static Color MenuItemSelectedGradientEnd
		{
			get
			{
				return ProfessionalColors.color_table.MenuItemSelectedGradientEnd;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</returns>
		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x000A77A4 File Offset: 0x000A59A4
		public static Color MenuStripGradientBegin
		{
			get
			{
				return ProfessionalColors.color_table.MenuStripGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</returns>
		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06002BCD RID: 11213 RVA: 0x000A77B0 File Offset: 0x000A59B0
		public static Color MenuStripGradientEnd
		{
			get
			{
				return ProfessionalColors.color_table.MenuStripGradientEnd;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</returns>
		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06002BCE RID: 11214 RVA: 0x000A77BC File Offset: 0x000A59BC
		public static Color OverflowButtonGradientBegin
		{
			get
			{
				return ProfessionalColors.color_table.OverflowButtonGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</returns>
		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06002BCF RID: 11215 RVA: 0x000A77C8 File Offset: 0x000A59C8
		public static Color OverflowButtonGradientEnd
		{
			get
			{
				return ProfessionalColors.color_table.OverflowButtonGradientEnd;
			}
		}

		/// <summary>Gets the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</returns>
		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06002BD0 RID: 11216 RVA: 0x000A77D4 File Offset: 0x000A59D4
		public static Color OverflowButtonGradientMiddle
		{
			get
			{
				return ProfessionalColors.color_table.OverflowButtonGradientMiddle;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06002BD1 RID: 11217 RVA: 0x000A77E0 File Offset: 0x000A59E0
		public static Color RaftingContainerGradientBegin
		{
			get
			{
				return ProfessionalColors.color_table.RaftingContainerGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06002BD2 RID: 11218 RVA: 0x000A77EC File Offset: 0x000A59EC
		public static Color RaftingContainerGradientEnd
		{
			get
			{
				return ProfessionalColors.color_table.RaftingContainerGradientEnd;
			}
		}

		/// <summary>Gets the color to use to for shadow effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use to for shadow effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</returns>
		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06002BD3 RID: 11219 RVA: 0x000A77F8 File Offset: 0x000A59F8
		public static Color SeparatorDark
		{
			get
			{
				return ProfessionalColors.color_table.SeparatorDark;
			}
		}

		/// <summary>Gets the color to use to for highlight effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use to create highlight effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</returns>
		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06002BD4 RID: 11220 RVA: 0x000A7804 File Offset: 0x000A5A04
		public static Color SeparatorLight
		{
			get
			{
				return ProfessionalColors.color_table.SeparatorLight;
			}
		}

		/// <summary>Gets the starting color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</returns>
		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06002BD5 RID: 11221 RVA: 0x000A7810 File Offset: 0x000A5A10
		public static Color StatusStripGradientBegin
		{
			get
			{
				return ProfessionalColors.color_table.StatusStripGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</returns>
		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06002BD6 RID: 11222 RVA: 0x000A781C File Offset: 0x000A5A1C
		public static Color StatusStripGradientEnd
		{
			get
			{
				return ProfessionalColors.color_table.StatusStripGradientEnd;
			}
		}

		/// <summary>Gets the border color to use on the bottom edge of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use on the bottom edge of the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06002BD7 RID: 11223 RVA: 0x000A7828 File Offset: 0x000A5A28
		public static Color ToolStripBorder
		{
			get
			{
				return ProfessionalColors.color_table.ToolStripBorder;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</returns>
		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06002BD8 RID: 11224 RVA: 0x000A7834 File Offset: 0x000A5A34
		public static Color ToolStripContentPanelGradientBegin
		{
			get
			{
				return ProfessionalColors.color_table.ToolStripContentPanelGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</returns>
		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06002BD9 RID: 11225 RVA: 0x000A7840 File Offset: 0x000A5A40
		public static Color ToolStripContentPanelGradientEnd
		{
			get
			{
				return ProfessionalColors.color_table.ToolStripContentPanelGradientEnd;
			}
		}

		/// <summary>Gets the solid background color of the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid background color of the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</returns>
		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06002BDA RID: 11226 RVA: 0x000A784C File Offset: 0x000A5A4C
		public static Color ToolStripDropDownBackground
		{
			get
			{
				return ProfessionalColors.color_table.ToolStripDropDownBackground;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</returns>
		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06002BDB RID: 11227 RVA: 0x000A7858 File Offset: 0x000A5A58
		public static Color ToolStripGradientBegin
		{
			get
			{
				return ProfessionalColors.color_table.ToolStripGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</returns>
		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06002BDC RID: 11228 RVA: 0x000A7864 File Offset: 0x000A5A64
		public static Color ToolStripGradientEnd
		{
			get
			{
				return ProfessionalColors.color_table.ToolStripGradientEnd;
			}
		}

		/// <summary>Gets the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</returns>
		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06002BDD RID: 11229 RVA: 0x000A7870 File Offset: 0x000A5A70
		public static Color ToolStripGradientMiddle
		{
			get
			{
				return ProfessionalColors.color_table.ToolStripGradientMiddle;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</returns>
		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06002BDE RID: 11230 RVA: 0x000A787C File Offset: 0x000A5A7C
		public static Color ToolStripPanelGradientBegin
		{
			get
			{
				return ProfessionalColors.color_table.ToolStripPanelGradientBegin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</returns>
		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06002BDF RID: 11231 RVA: 0x000A7888 File Offset: 0x000A5A88
		public static Color ToolStripPanelGradientEnd
		{
			get
			{
				return ProfessionalColors.color_table.ToolStripPanelGradientEnd;
			}
		}

		// Token: 0x04001554 RID: 5460
		private static ProfessionalColorTable color_table = new ProfessionalColorTable();
	}
}
