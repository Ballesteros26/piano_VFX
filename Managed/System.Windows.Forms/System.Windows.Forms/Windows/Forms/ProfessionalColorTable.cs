using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides colors used for Microsoft Office display elements.</summary>
	// Token: 0x02000298 RID: 664
	public class ProfessionalColorTable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ProfessionalColorTable" /> class. </summary>
		// Token: 0x06002BE0 RID: 11232 RVA: 0x000A7894 File Offset: 0x000A5A94
		public ProfessionalColorTable()
		{
			this.CalculateColors();
		}

		/// <summary>Gets the starting color of the gradient used when the button is checked.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the button is checked.</returns>
		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06002BE1 RID: 11233 RVA: 0x000A78A4 File Offset: 0x000A5AA4
		public virtual Color ButtonCheckedGradientBegin
		{
			get
			{
				return this.button_checked_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used when the button is checked.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is checked.</returns>
		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06002BE2 RID: 11234 RVA: 0x000A78AC File Offset: 0x000A5AAC
		public virtual Color ButtonCheckedGradientEnd
		{
			get
			{
				return this.button_checked_gradient_end;
			}
		}

		/// <summary>Gets the middle color of the gradient used when the button is checked.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when the button is checked.</returns>
		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06002BE3 RID: 11235 RVA: 0x000A78B4 File Offset: 0x000A5AB4
		public virtual Color ButtonCheckedGradientMiddle
		{
			get
			{
				return this.button_checked_gradient_middle;
			}
		}

		/// <summary>Gets the solid color used when the button is checked.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color used when the button is checked.</returns>
		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x000A78BC File Offset: 0x000A5ABC
		public virtual Color ButtonCheckedHighlight
		{
			get
			{
				return this.button_checked_highlight;
			}
		}

		/// <summary>Gets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonCheckedHighlight" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonCheckedHighlight" />.</returns>
		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06002BE5 RID: 11237 RVA: 0x000A78C4 File Offset: 0x000A5AC4
		public virtual Color ButtonCheckedHighlightBorder
		{
			get
			{
				return this.button_checked_highlight_border;
			}
		}

		/// <summary>Gets the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientEnd" /> colors.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientEnd" /> colors.</returns>
		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x000A78CC File Offset: 0x000A5ACC
		public virtual Color ButtonPressedBorder
		{
			get
			{
				return this.button_pressed_border;
			}
		}

		/// <summary>Gets the starting color of the gradient used when the button is pressed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the button is pressed.</returns>
		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06002BE7 RID: 11239 RVA: 0x000A78D4 File Offset: 0x000A5AD4
		public virtual Color ButtonPressedGradientBegin
		{
			get
			{
				return this.button_pressed_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used when the button is pressed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is pressed.</returns>
		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06002BE8 RID: 11240 RVA: 0x000A78DC File Offset: 0x000A5ADC
		public virtual Color ButtonPressedGradientEnd
		{
			get
			{
				return this.button_pressed_gradient_end;
			}
		}

		/// <summary>Gets the middle color of the gradient used when the button is pressed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when the button is pressed.</returns>
		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06002BE9 RID: 11241 RVA: 0x000A78E4 File Offset: 0x000A5AE4
		public virtual Color ButtonPressedGradientMiddle
		{
			get
			{
				return this.button_pressed_gradient_middle;
			}
		}

		/// <summary>Gets the solid color used when the button is pressed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color used when the button is pressed.</returns>
		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06002BEA RID: 11242 RVA: 0x000A78EC File Offset: 0x000A5AEC
		public virtual Color ButtonPressedHighlight
		{
			get
			{
				return this.button_pressed_highlight;
			}
		}

		/// <summary>Gets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedHighlight" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedHighlight" />.</returns>
		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06002BEB RID: 11243 RVA: 0x000A78F4 File Offset: 0x000A5AF4
		public virtual Color ButtonPressedHighlightBorder
		{
			get
			{
				return this.button_pressed_highlight_border;
			}
		}

		/// <summary>Gets the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientEnd" /> colors.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientBegin" />, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientMiddle" />, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientEnd" /> colors.</returns>
		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06002BEC RID: 11244 RVA: 0x000A78FC File Offset: 0x000A5AFC
		public virtual Color ButtonSelectedBorder
		{
			get
			{
				return this.button_selected_border;
			}
		}

		/// <summary>Gets the starting color of the gradient used when the button is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the button is selected.</returns>
		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06002BED RID: 11245 RVA: 0x000A7904 File Offset: 0x000A5B04
		public virtual Color ButtonSelectedGradientBegin
		{
			get
			{
				return this.button_selected_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used when the button is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the button is selected.</returns>
		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06002BEE RID: 11246 RVA: 0x000A790C File Offset: 0x000A5B0C
		public virtual Color ButtonSelectedGradientEnd
		{
			get
			{
				return this.button_selected_gradient_end;
			}
		}

		/// <summary>Gets the middle color of the gradient used when the button is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when the button is selected.</returns>
		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06002BEF RID: 11247 RVA: 0x000A7914 File Offset: 0x000A5B14
		public virtual Color ButtonSelectedGradientMiddle
		{
			get
			{
				return this.button_selected_gradient_middle;
			}
		}

		/// <summary>Gets the solid color used when the button is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color used when the button is selected.</returns>
		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06002BF0 RID: 11248 RVA: 0x000A791C File Offset: 0x000A5B1C
		public virtual Color ButtonSelectedHighlight
		{
			get
			{
				return this.button_selected_highlight;
			}
		}

		/// <summary>Gets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedHighlight" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedHighlight" />.</returns>
		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06002BF1 RID: 11249 RVA: 0x000A7924 File Offset: 0x000A5B24
		public virtual Color ButtonSelectedHighlightBorder
		{
			get
			{
				return this.button_selected_highlight_border;
			}
		}

		/// <summary>Gets the solid color to use when the button is checked and gradients are being used.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when the button is checked and gradients are being used.</returns>
		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x000A792C File Offset: 0x000A5B2C
		public virtual Color CheckBackground
		{
			get
			{
				return this.check_background;
			}
		}

		/// <summary>Gets the solid color to use when the button is checked and selected and gradients are being used.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when the button is checked and selected and gradients are being used.</returns>
		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06002BF3 RID: 11251 RVA: 0x000A7934 File Offset: 0x000A5B34
		public virtual Color CheckPressedBackground
		{
			get
			{
				return this.check_pressed_background;
			}
		}

		/// <summary>Gets the solid color to use when the button is checked and selected and gradients are being used.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when the button is checked and selected and gradients are being used.</returns>
		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06002BF4 RID: 11252 RVA: 0x000A793C File Offset: 0x000A5B3C
		public virtual Color CheckSelectedBackground
		{
			get
			{
				return this.check_selected_background;
			}
		}

		/// <summary>Gets the color to use for shadow effects on the grip (move handle).</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use for shadow effects on the grip (move handle).</returns>
		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06002BF5 RID: 11253 RVA: 0x000A7944 File Offset: 0x000A5B44
		public virtual Color GripDark
		{
			get
			{
				return this.grip_dark;
			}
		}

		/// <summary>Gets the color to use for highlight effects on the grip (move handle).</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use for highlight effects on the grip (move handle).</returns>
		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06002BF6 RID: 11254 RVA: 0x000A794C File Offset: 0x000A5B4C
		public virtual Color GripLight
		{
			get
			{
				return this.grip_light;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</returns>
		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06002BF7 RID: 11255 RVA: 0x000A7954 File Offset: 0x000A5B54
		public virtual Color ImageMarginGradientBegin
		{
			get
			{
				return this.image_margin_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</returns>
		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06002BF8 RID: 11256 RVA: 0x000A795C File Offset: 0x000A5B5C
		public virtual Color ImageMarginGradientEnd
		{
			get
			{
				return this.image_margin_gradient_end;
			}
		}

		/// <summary>Gets the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</returns>
		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06002BF9 RID: 11257 RVA: 0x000A7964 File Offset: 0x000A5B64
		public virtual Color ImageMarginGradientMiddle
		{
			get
			{
				return this.image_margin_gradient_middle;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</returns>
		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06002BFA RID: 11258 RVA: 0x000A796C File Offset: 0x000A5B6C
		public virtual Color ImageMarginRevealedGradientBegin
		{
			get
			{
				return this.image_margin_revealed_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</returns>
		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06002BFB RID: 11259 RVA: 0x000A7974 File Offset: 0x000A5B74
		public virtual Color ImageMarginRevealedGradientEnd
		{
			get
			{
				return this.image_margin_revealed_gradient_end;
			}
		}

		/// <summary>Gets the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> when an item is revealed.</returns>
		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06002BFC RID: 11260 RVA: 0x000A797C File Offset: 0x000A5B7C
		public virtual Color ImageMarginRevealedGradientMiddle
		{
			get
			{
				return this.image_margin_revealed_gradient_middle;
			}
		}

		/// <summary>Gets the color that is the border color to use on a <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use on a <see cref="T:System.Windows.Forms.MenuStrip" />.</returns>
		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x000A7984 File Offset: 0x000A5B84
		public virtual Color MenuBorder
		{
			get
			{
				return this.menu_border;
			}
		}

		/// <summary>Gets the border color to use with a <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use with a <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</returns>
		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06002BFE RID: 11262 RVA: 0x000A798C File Offset: 0x000A5B8C
		public virtual Color MenuItemBorder
		{
			get
			{
				return this.menu_item_border;
			}
		}

		/// <summary>Gets the starting color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed.</returns>
		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06002BFF RID: 11263 RVA: 0x000A7994 File Offset: 0x000A5B94
		public virtual Color MenuItemPressedGradientBegin
		{
			get
			{
				return this.menu_item_pressed_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed.</returns>
		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06002C00 RID: 11264 RVA: 0x000A799C File Offset: 0x000A5B9C
		public virtual Color MenuItemPressedGradientEnd
		{
			get
			{
				return this.menu_item_pressed_gradient_end;
			}
		}

		/// <summary>Gets the middle color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is pressed.</returns>
		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06002C01 RID: 11265 RVA: 0x000A79A4 File Offset: 0x000A5BA4
		public virtual Color MenuItemPressedGradientMiddle
		{
			get
			{
				return this.menu_item_pressed_gradient_middle;
			}
		}

		/// <summary>Gets the solid color to use when a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> other than the top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid color to use when a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> other than the top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</returns>
		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06002C02 RID: 11266 RVA: 0x000A79AC File Offset: 0x000A5BAC
		public virtual Color MenuItemSelected
		{
			get
			{
				return this.menu_item_selected;
			}
		}

		/// <summary>Gets the starting color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</returns>
		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06002C03 RID: 11267 RVA: 0x000A79B4 File Offset: 0x000A5BB4
		public virtual Color MenuItemSelectedGradientBegin
		{
			get
			{
				return this.menu_item_selected_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is selected.</returns>
		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06002C04 RID: 11268 RVA: 0x000A79BC File Offset: 0x000A5BBC
		public virtual Color MenuItemSelectedGradientEnd
		{
			get
			{
				return this.menu_item_selected_gradient_end;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</returns>
		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06002C05 RID: 11269 RVA: 0x000A79C4 File Offset: 0x000A5BC4
		public virtual Color MenuStripGradientBegin
		{
			get
			{
				return this.menu_strip_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip" />.</returns>
		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06002C06 RID: 11270 RVA: 0x000A79CC File Offset: 0x000A5BCC
		public virtual Color MenuStripGradientEnd
		{
			get
			{
				return this.menu_strip_gradient_end;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</returns>
		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x000A79D4 File Offset: 0x000A5BD4
		public virtual Color OverflowButtonGradientBegin
		{
			get
			{
				return this.overflow_button_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</returns>
		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06002C08 RID: 11272 RVA: 0x000A79DC File Offset: 0x000A5BDC
		public virtual Color OverflowButtonGradientEnd
		{
			get
			{
				return this.overflow_button_gradient_end;
			}
		}

		/// <summary>Gets the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton" />.</returns>
		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x000A79E4 File Offset: 0x000A5BE4
		public virtual Color OverflowButtonGradientMiddle
		{
			get
			{
				return this.overflow_button_gradient_middle;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06002C0A RID: 11274 RVA: 0x000A79EC File Offset: 0x000A5BEC
		public virtual Color RaftingContainerGradientBegin
		{
			get
			{
				return this.rafting_container_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06002C0B RID: 11275 RVA: 0x000A79F4 File Offset: 0x000A5BF4
		public virtual Color RaftingContainerGradientEnd
		{
			get
			{
				return this.rafting_container_gradient_end;
			}
		}

		/// <summary>Gets the color to use to for shadow effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use to for shadow effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</returns>
		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06002C0C RID: 11276 RVA: 0x000A79FC File Offset: 0x000A5BFC
		public virtual Color SeparatorDark
		{
			get
			{
				return this.separator_dark;
			}
		}

		/// <summary>Gets the color to use to for highlight effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the color to use to for highlight effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</returns>
		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06002C0D RID: 11277 RVA: 0x000A7A04 File Offset: 0x000A5C04
		public virtual Color SeparatorLight
		{
			get
			{
				return this.separator_light;
			}
		}

		/// <summary>Gets the starting color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</returns>
		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06002C0E RID: 11278 RVA: 0x000A7A0C File Offset: 0x000A5C0C
		public virtual Color StatusStripGradientBegin
		{
			get
			{
				return this.status_strip_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip" />.</returns>
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06002C0F RID: 11279 RVA: 0x000A7A14 File Offset: 0x000A5C14
		public virtual Color StatusStripGradientEnd
		{
			get
			{
				return this.status_strip_gradient_end;
			}
		}

		/// <summary>Gets the border color to use on the bottom edge of the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the border color to use on the bottom edge of the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06002C10 RID: 11280 RVA: 0x000A7A1C File Offset: 0x000A5C1C
		public virtual Color ToolStripBorder
		{
			get
			{
				return this.tool_strip_border;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</returns>
		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06002C11 RID: 11281 RVA: 0x000A7A24 File Offset: 0x000A5C24
		public virtual Color ToolStripContentPanelGradientBegin
		{
			get
			{
				return this.tool_strip_content_panel_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</returns>
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06002C12 RID: 11282 RVA: 0x000A7A2C File Offset: 0x000A5C2C
		public virtual Color ToolStripContentPanelGradientEnd
		{
			get
			{
				return this.tool_strip_content_panel_gradient_end;
			}
		}

		/// <summary>Gets the solid background color of the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the solid background color of the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</returns>
		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06002C13 RID: 11283 RVA: 0x000A7A34 File Offset: 0x000A5C34
		public virtual Color ToolStripDropDownBackground
		{
			get
			{
				return this.tool_strip_drop_down_background;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</returns>
		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06002C14 RID: 11284 RVA: 0x000A7A3C File Offset: 0x000A5C3C
		public virtual Color ToolStripGradientBegin
		{
			get
			{
				return this.tool_strip_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</returns>
		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06002C15 RID: 11285 RVA: 0x000A7A44 File Offset: 0x000A5C44
		public virtual Color ToolStripGradientEnd
		{
			get
			{
				return this.tool_strip_gradient_end;
			}
		}

		/// <summary>Gets the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip" /> background.</returns>
		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06002C16 RID: 11286 RVA: 0x000A7A4C File Offset: 0x000A5C4C
		public virtual Color ToolStripGradientMiddle
		{
			get
			{
				return this.tool_strip_gradient_middle;
			}
		}

		/// <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</returns>
		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06002C17 RID: 11287 RVA: 0x000A7A54 File Offset: 0x000A5C54
		public virtual Color ToolStripPanelGradientBegin
		{
			get
			{
				return this.tool_strip_panel_gradient_begin;
			}
		}

		/// <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</returns>
		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06002C18 RID: 11288 RVA: 0x000A7A5C File Offset: 0x000A5C5C
		public virtual Color ToolStripPanelGradientEnd
		{
			get
			{
				return this.tool_strip_panel_gradient_end;
			}
		}

		/// <summary>Gets or sets a value indicating whether to use <see cref="T:System.Drawing.SystemColors" /> rather than colors that match the current visual style. </summary>
		/// <returns>true to use <see cref="T:System.Drawing.SystemColors" />; otherwise, false. The default is false.</returns>
		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06002C19 RID: 11289 RVA: 0x000A7A64 File Offset: 0x000A5C64
		// (set) Token: 0x06002C1A RID: 11290 RVA: 0x000A7A6C File Offset: 0x000A5C6C
		public bool UseSystemColors
		{
			get
			{
				return this.use_system_colors;
			}
			set
			{
				if (value != this.use_system_colors)
				{
					this.use_system_colors = value;
					this.CalculateColors();
				}
			}
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x000A7A88 File Offset: 0x000A5C88
		private void CalculateColors()
		{
			switch (this.GetCurrentStyle())
			{
			case ProfessionalColorTable.ColorSchemes.Classic:
				this.button_checked_gradient_begin = Color.Empty;
				this.button_checked_gradient_end = Color.Empty;
				this.button_checked_gradient_middle = Color.Empty;
				this.button_checked_highlight = Color.FromArgb(184, 191, 211);
				this.button_checked_highlight_border = Color.FromKnownColor(13);
				this.button_pressed_border = Color.FromKnownColor(13);
				this.button_pressed_gradient_begin = Color.FromArgb(133, 146, 181);
				this.button_pressed_gradient_end = Color.FromArgb(133, 146, 181);
				this.button_pressed_gradient_middle = Color.FromArgb(133, 146, 181);
				this.button_pressed_highlight = Color.FromArgb(131, 144, 179);
				this.button_pressed_highlight_border = Color.FromKnownColor(13);
				this.button_selected_border = Color.FromKnownColor(13);
				this.button_selected_gradient_begin = Color.FromArgb(182, 189, 210);
				this.button_selected_gradient_end = Color.FromArgb(182, 189, 210);
				this.button_selected_gradient_middle = Color.FromArgb(182, 189, 210);
				this.button_selected_highlight = Color.FromArgb(184, 191, 211);
				this.button_selected_highlight_border = Color.FromKnownColor(13);
				this.check_background = Color.FromKnownColor(13);
				this.check_pressed_background = Color.FromArgb(133, 146, 181);
				this.check_selected_background = Color.FromArgb(133, 146, 181);
				this.grip_dark = Color.FromArgb(160, 160, 160);
				this.grip_light = SystemColors.Window;
				this.image_margin_gradient_begin = Color.FromArgb(245, 244, 242);
				this.image_margin_gradient_end = SystemColors.Control;
				this.image_margin_gradient_middle = Color.FromArgb(234, 232, 228);
				this.image_margin_revealed_gradient_begin = Color.FromArgb(238, 236, 233);
				this.image_margin_revealed_gradient_end = Color.FromArgb(216, 213, 206);
				this.image_margin_revealed_gradient_middle = Color.FromArgb(225, 222, 217);
				this.menu_border = Color.FromArgb(102, 102, 102);
				this.menu_item_border = SystemColors.Highlight;
				this.menu_item_pressed_gradient_begin = Color.FromArgb(245, 244, 242);
				this.menu_item_pressed_gradient_end = Color.FromArgb(234, 232, 228);
				this.menu_item_pressed_gradient_middle = Color.FromArgb(225, 222, 217);
				this.menu_item_selected = SystemColors.Window;
				this.menu_item_selected_gradient_begin = Color.FromArgb(182, 189, 210);
				this.menu_item_selected_gradient_end = Color.FromArgb(182, 189, 210);
				this.menu_strip_gradient_begin = SystemColors.ButtonFace;
				this.menu_strip_gradient_end = Color.FromArgb(246, 245, 244);
				this.overflow_button_gradient_begin = Color.FromArgb(225, 222, 217);
				this.overflow_button_gradient_end = SystemColors.ButtonShadow;
				this.overflow_button_gradient_middle = Color.FromArgb(216, 213, 206);
				this.rafting_container_gradient_begin = SystemColors.ButtonFace;
				this.rafting_container_gradient_end = Color.FromArgb(246, 245, 244);
				this.separator_dark = Color.FromArgb(166, 166, 166);
				this.separator_light = SystemColors.ButtonHighlight;
				this.status_strip_gradient_begin = SystemColors.ButtonFace;
				this.status_strip_gradient_end = Color.FromArgb(246, 245, 244);
				this.tool_strip_border = Color.FromArgb(219, 216, 209);
				this.tool_strip_content_panel_gradient_begin = SystemColors.ButtonFace;
				this.tool_strip_content_panel_gradient_end = Color.FromArgb(246, 245, 244);
				this.tool_strip_drop_down_background = SystemColors.Window;
				this.tool_strip_gradient_begin = Color.FromArgb(245, 244, 242);
				this.tool_strip_gradient_end = SystemColors.ButtonFace;
				this.tool_strip_gradient_middle = Color.FromArgb(234, 232, 228);
				this.tool_strip_panel_gradient_begin = SystemColors.ButtonFace;
				this.tool_strip_panel_gradient_end = Color.FromArgb(246, 245, 244);
				break;
			case ProfessionalColorTable.ColorSchemes.NormalColor:
				this.button_checked_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(255, 223, 154) : Color.Empty);
				this.button_checked_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(255, 166, 76) : Color.Empty);
				this.button_checked_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(255, 195, 116) : Color.Empty);
				this.button_checked_highlight = Color.FromArgb(195, 211, 237);
				this.button_checked_highlight_border = Color.FromKnownColor(13);
				this.button_pressed_border = ((!this.use_system_colors) ? Color.FromArgb(0, 0, 128) : Color.FromKnownColor(13));
				this.button_pressed_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(254, 128, 62) : Color.FromArgb(152, 181, 226));
				this.button_pressed_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(255, 223, 154) : Color.FromArgb(152, 181, 226));
				this.button_pressed_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(255, 177, 109) : Color.FromArgb(152, 181, 226));
				this.button_pressed_highlight = ((!this.use_system_colors) ? Color.FromArgb(150, 179, 225) : Color.FromArgb(150, 179, 225));
				this.button_pressed_highlight_border = Color.FromKnownColor(13);
				this.button_selected_border = ((!this.use_system_colors) ? Color.FromArgb(0, 0, 128) : Color.FromKnownColor(13));
				this.button_selected_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(255, 255, 222) : Color.FromArgb(193, 210, 238));
				this.button_selected_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(255, 203, 136) : Color.FromArgb(193, 210, 238));
				this.button_selected_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(255, 225, 172) : Color.FromArgb(193, 210, 238));
				this.button_selected_highlight = ((!this.use_system_colors) ? Color.FromArgb(195, 211, 237) : Color.FromArgb(195, 211, 237));
				this.button_selected_highlight_border = ((!this.use_system_colors) ? Color.FromArgb(0, 0, 128) : Color.FromKnownColor(13));
				this.check_background = ((!this.use_system_colors) ? Color.FromArgb(255, 192, 111) : Color.FromKnownColor(13));
				this.check_pressed_background = ((!this.use_system_colors) ? Color.FromArgb(254, 128, 62) : Color.FromArgb(152, 181, 226));
				this.check_selected_background = ((!this.use_system_colors) ? Color.FromArgb(254, 128, 62) : Color.FromArgb(152, 181, 226));
				this.grip_dark = ((!this.use_system_colors) ? Color.FromArgb(39, 65, 118) : Color.FromArgb(193, 190, 179));
				this.grip_light = ((!this.use_system_colors) ? Color.FromArgb(255, 255, 255) : SystemColors.Window);
				this.image_margin_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(227, 239, 255) : Color.FromArgb(251, 250, 246));
				this.image_margin_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(123, 164, 224) : SystemColors.Control);
				this.image_margin_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(203, 225, 252) : Color.FromArgb(246, 244, 236));
				this.image_margin_revealed_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(203, 221, 246) : Color.FromArgb(247, 246, 239));
				this.image_margin_revealed_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(114, 155, 215) : Color.FromArgb(238, 235, 220));
				this.image_margin_revealed_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(161, 197, 249) : Color.FromArgb(242, 240, 228));
				this.menu_border = ((!this.use_system_colors) ? Color.FromArgb(0, 45, 150) : Color.FromArgb(138, 134, 122));
				this.menu_item_border = ((!this.use_system_colors) ? Color.FromArgb(0, 0, 128) : SystemColors.Highlight);
				this.menu_item_pressed_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(227, 239, 255) : Color.FromArgb(251, 250, 246));
				this.menu_item_pressed_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(123, 164, 224) : Color.FromArgb(246, 244, 236));
				this.menu_item_pressed_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(161, 197, 249) : Color.FromArgb(242, 240, 228));
				this.menu_item_selected = ((!this.use_system_colors) ? Color.FromArgb(255, 238, 194) : SystemColors.Window);
				this.menu_item_selected_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(255, 255, 222) : Color.FromArgb(193, 210, 238));
				this.menu_item_selected_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(255, 203, 136) : Color.FromArgb(193, 210, 238));
				this.menu_strip_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(158, 190, 245) : SystemColors.ButtonFace);
				this.menu_strip_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(196, 218, 250) : Color.FromArgb(251, 250, 247));
				this.overflow_button_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(127, 177, 250) : Color.FromArgb(242, 240, 228));
				this.overflow_button_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(0, 53, 145) : SystemColors.ButtonShadow);
				this.overflow_button_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(82, 127, 208) : Color.FromArgb(238, 235, 220));
				this.rafting_container_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(158, 190, 245) : SystemColors.ButtonFace);
				this.rafting_container_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(196, 218, 250) : Color.FromArgb(251, 250, 247));
				this.separator_dark = ((!this.use_system_colors) ? Color.FromArgb(106, 140, 203) : Color.FromArgb(197, 194, 184));
				this.separator_light = ((!this.use_system_colors) ? Color.FromArgb(241, 249, 255) : SystemColors.ButtonHighlight);
				this.status_strip_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(158, 190, 245) : SystemColors.ButtonFace);
				this.status_strip_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(196, 218, 250) : Color.FromArgb(251, 250, 247));
				this.tool_strip_border = ((!this.use_system_colors) ? Color.FromArgb(59, 97, 156) : Color.FromArgb(239, 237, 222));
				this.tool_strip_content_panel_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(158, 190, 245) : SystemColors.ButtonFace);
				this.tool_strip_content_panel_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(196, 218, 250) : Color.FromArgb(251, 250, 247));
				this.tool_strip_drop_down_background = ((!this.use_system_colors) ? Color.FromArgb(246, 246, 246) : Color.FromArgb(252, 252, 249));
				this.tool_strip_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(227, 239, 255) : Color.FromArgb(251, 250, 246));
				this.tool_strip_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(123, 164, 224) : SystemColors.ButtonFace);
				this.tool_strip_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(203, 225, 252) : Color.FromArgb(246, 244, 236));
				this.tool_strip_panel_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(158, 190, 245) : SystemColors.ButtonFace);
				this.tool_strip_panel_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(196, 218, 250) : Color.FromArgb(251, 250, 247));
				break;
			case ProfessionalColorTable.ColorSchemes.HomeStead:
				this.button_checked_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(255, 223, 154) : Color.Empty);
				this.button_checked_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(255, 166, 76) : Color.Empty);
				this.button_checked_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(255, 195, 116) : Color.Empty);
				this.button_checked_highlight = Color.FromArgb(223, 227, 213);
				this.button_checked_highlight_border = Color.FromKnownColor(13);
				this.button_pressed_border = ((!this.use_system_colors) ? Color.FromArgb(63, 93, 56) : Color.FromKnownColor(13));
				this.button_pressed_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(254, 128, 62) : Color.FromArgb(201, 208, 184));
				this.button_pressed_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(255, 223, 154) : Color.FromArgb(201, 208, 184));
				this.button_pressed_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(255, 177, 109) : Color.FromArgb(201, 208, 184));
				this.button_pressed_highlight = ((!this.use_system_colors) ? Color.FromArgb(200, 206, 182) : Color.FromArgb(200, 206, 182));
				this.button_pressed_highlight_border = Color.FromKnownColor(13);
				this.button_selected_border = ((!this.use_system_colors) ? Color.FromArgb(63, 93, 56) : Color.FromKnownColor(13));
				this.button_selected_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(255, 255, 222) : Color.FromArgb(223, 227, 212));
				this.button_selected_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(255, 203, 136) : Color.FromArgb(223, 227, 212));
				this.button_selected_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(255, 225, 172) : Color.FromArgb(223, 227, 212));
				this.button_selected_highlight = ((!this.use_system_colors) ? Color.FromArgb(223, 227, 213) : Color.FromArgb(223, 227, 213));
				this.button_selected_highlight_border = ((!this.use_system_colors) ? Color.FromArgb(63, 93, 56) : Color.FromKnownColor(13));
				this.check_background = ((!this.use_system_colors) ? Color.FromArgb(255, 192, 111) : Color.FromKnownColor(13));
				this.check_pressed_background = ((!this.use_system_colors) ? Color.FromArgb(254, 128, 62) : Color.FromArgb(201, 208, 184));
				this.check_selected_background = ((!this.use_system_colors) ? Color.FromArgb(254, 128, 62) : Color.FromArgb(201, 208, 184));
				this.grip_dark = ((!this.use_system_colors) ? Color.FromArgb(81, 94, 51) : Color.FromArgb(193, 190, 179));
				this.grip_light = ((!this.use_system_colors) ? Color.FromArgb(255, 255, 255) : SystemColors.Window);
				this.image_margin_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(255, 255, 237) : Color.FromArgb(251, 250, 246));
				this.image_margin_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(181, 196, 143) : SystemColors.Control);
				this.image_margin_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(206, 220, 167) : Color.FromArgb(246, 244, 236));
				this.image_margin_revealed_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(230, 230, 209) : Color.FromArgb(247, 246, 239));
				this.image_margin_revealed_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(160, 177, 116) : Color.FromArgb(238, 235, 220));
				this.image_margin_revealed_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(186, 201, 143) : Color.FromArgb(242, 240, 228));
				this.menu_border = ((!this.use_system_colors) ? Color.FromArgb(117, 141, 94) : Color.FromArgb(138, 134, 122));
				this.menu_item_border = ((!this.use_system_colors) ? Color.FromArgb(63, 93, 56) : SystemColors.Highlight);
				this.menu_item_pressed_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(237, 240, 214) : Color.FromArgb(251, 250, 246));
				this.menu_item_pressed_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(181, 196, 143) : Color.FromArgb(246, 244, 236));
				this.menu_item_pressed_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(186, 201, 143) : Color.FromArgb(242, 240, 228));
				this.menu_item_selected = ((!this.use_system_colors) ? Color.FromArgb(255, 238, 194) : SystemColors.Window);
				this.menu_item_selected_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(255, 255, 222) : Color.FromArgb(223, 227, 212));
				this.menu_item_selected_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(255, 203, 136) : Color.FromArgb(223, 227, 212));
				this.menu_strip_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(217, 217, 167) : SystemColors.ButtonFace);
				this.menu_strip_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(242, 241, 228) : Color.FromArgb(251, 250, 247));
				this.overflow_button_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(186, 204, 150) : Color.FromArgb(242, 240, 228));
				this.overflow_button_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(96, 119, 107) : SystemColors.ButtonShadow);
				this.overflow_button_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(141, 160, 107) : Color.FromArgb(238, 235, 220));
				this.rafting_container_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(217, 217, 167) : SystemColors.ButtonFace);
				this.rafting_container_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(242, 241, 228) : Color.FromArgb(251, 250, 247));
				this.separator_dark = ((!this.use_system_colors) ? Color.FromArgb(96, 128, 88) : Color.FromArgb(197, 194, 184));
				this.separator_light = ((!this.use_system_colors) ? Color.FromArgb(244, 247, 222) : SystemColors.ButtonHighlight);
				this.status_strip_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(217, 217, 167) : SystemColors.ButtonFace);
				this.status_strip_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(242, 241, 228) : Color.FromArgb(251, 250, 247));
				this.tool_strip_border = ((!this.use_system_colors) ? Color.FromArgb(96, 128, 88) : Color.FromArgb(239, 237, 222));
				this.tool_strip_content_panel_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(217, 217, 167) : SystemColors.ButtonFace);
				this.tool_strip_content_panel_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(242, 241, 228) : Color.FromArgb(251, 250, 247));
				this.tool_strip_drop_down_background = ((!this.use_system_colors) ? Color.FromArgb(244, 244, 238) : Color.FromArgb(252, 252, 249));
				this.tool_strip_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(255, 255, 237) : Color.FromArgb(251, 250, 246));
				this.tool_strip_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(181, 196, 143) : SystemColors.ButtonFace);
				this.tool_strip_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(206, 220, 167) : Color.FromArgb(246, 244, 236));
				this.tool_strip_panel_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(217, 217, 167) : SystemColors.ButtonFace);
				this.tool_strip_panel_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(242, 241, 228) : Color.FromArgb(251, 250, 247));
				break;
			case ProfessionalColorTable.ColorSchemes.Metallic:
				this.button_checked_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(255, 223, 154) : Color.Empty);
				this.button_checked_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(255, 166, 76) : Color.Empty);
				this.button_checked_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(255, 195, 116) : Color.Empty);
				this.button_checked_highlight = Color.FromArgb(231, 232, 235);
				this.button_checked_highlight_border = Color.FromKnownColor(13);
				this.button_pressed_border = ((!this.use_system_colors) ? Color.FromArgb(75, 75, 111) : Color.FromKnownColor(13));
				this.button_pressed_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(254, 128, 62) : Color.FromArgb(217, 218, 223));
				this.button_pressed_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(255, 223, 154) : Color.FromArgb(217, 218, 223));
				this.button_pressed_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(255, 177, 109) : Color.FromArgb(217, 218, 223));
				this.button_pressed_highlight = ((!this.use_system_colors) ? Color.FromArgb(215, 216, 222) : Color.FromArgb(215, 216, 222));
				this.button_pressed_highlight_border = Color.FromKnownColor(13);
				this.button_selected_border = ((!this.use_system_colors) ? Color.FromArgb(75, 75, 111) : Color.FromKnownColor(13));
				this.button_selected_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(255, 255, 222) : Color.FromArgb(232, 233, 236));
				this.button_selected_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(255, 203, 136) : Color.FromArgb(232, 233, 236));
				this.button_selected_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(255, 225, 172) : Color.FromArgb(232, 233, 236));
				this.button_selected_highlight = ((!this.use_system_colors) ? Color.FromArgb(231, 232, 235) : Color.FromArgb(231, 232, 235));
				this.button_selected_highlight_border = ((!this.use_system_colors) ? Color.FromArgb(75, 75, 111) : Color.FromKnownColor(13));
				this.check_background = ((!this.use_system_colors) ? Color.FromArgb(255, 192, 111) : Color.FromKnownColor(13));
				this.check_pressed_background = ((!this.use_system_colors) ? Color.FromArgb(254, 128, 62) : Color.FromArgb(217, 218, 223));
				this.check_selected_background = ((!this.use_system_colors) ? Color.FromArgb(254, 128, 62) : Color.FromArgb(217, 218, 223));
				this.grip_dark = ((!this.use_system_colors) ? Color.FromArgb(84, 84, 117) : Color.FromArgb(182, 182, 185));
				this.grip_light = ((!this.use_system_colors) ? Color.FromArgb(255, 255, 255) : SystemColors.Window);
				this.image_margin_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(249, 249, 255) : Color.FromArgb(248, 248, 249));
				this.image_margin_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(147, 145, 176) : SystemColors.Control);
				this.image_margin_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(225, 226, 236) : Color.FromArgb(240, 239, 241));
				this.image_margin_revealed_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(215, 215, 226) : Color.FromArgb(243, 242, 244));
				this.image_margin_revealed_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(118, 116, 151) : Color.FromArgb(227, 226, 230));
				this.image_margin_revealed_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(184, 185, 202) : Color.FromArgb(233, 233, 235));
				this.menu_border = ((!this.use_system_colors) ? Color.FromArgb(124, 124, 148) : Color.FromArgb(126, 126, 129));
				this.menu_item_border = ((!this.use_system_colors) ? Color.FromArgb(75, 75, 111) : SystemColors.Highlight);
				this.menu_item_pressed_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(232, 233, 242) : Color.FromArgb(248, 248, 249));
				this.menu_item_pressed_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(172, 170, 194) : Color.FromArgb(240, 239, 241));
				this.menu_item_pressed_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(184, 185, 202) : Color.FromArgb(233, 233, 235));
				this.menu_item_selected = ((!this.use_system_colors) ? Color.FromArgb(255, 238, 194) : SystemColors.Window);
				this.menu_item_selected_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(255, 255, 222) : Color.FromArgb(232, 233, 236));
				this.menu_item_selected_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(255, 203, 136) : Color.FromArgb(232, 233, 236));
				this.menu_strip_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(215, 215, 229) : SystemColors.ButtonFace);
				this.menu_strip_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(243, 243, 247) : Color.FromArgb(249, 248, 249));
				this.overflow_button_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(186, 185, 206) : Color.FromArgb(233, 233, 235));
				this.overflow_button_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(118, 116, 146) : SystemColors.ButtonShadow);
				this.overflow_button_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(156, 155, 180) : Color.FromArgb(227, 226, 230));
				this.rafting_container_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(215, 215, 229) : SystemColors.ButtonFace);
				this.rafting_container_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(243, 243, 247) : Color.FromArgb(249, 248, 249));
				this.separator_dark = ((!this.use_system_colors) ? Color.FromArgb(110, 109, 143) : Color.FromArgb(186, 186, 189));
				this.separator_light = ((!this.use_system_colors) ? Color.FromArgb(255, 255, 255) : SystemColors.ButtonHighlight);
				this.status_strip_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(215, 215, 229) : SystemColors.ButtonFace);
				this.status_strip_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(243, 243, 247) : Color.FromArgb(249, 248, 249));
				this.tool_strip_border = ((!this.use_system_colors) ? Color.FromArgb(124, 124, 148) : Color.FromArgb(229, 228, 232));
				this.tool_strip_content_panel_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(215, 215, 229) : SystemColors.ButtonFace);
				this.tool_strip_content_panel_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(243, 243, 247) : Color.FromArgb(249, 248, 249));
				this.tool_strip_drop_down_background = ((!this.use_system_colors) ? Color.FromArgb(253, 250, 255) : Color.FromArgb(251, 250, 251));
				this.tool_strip_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(249, 249, 255) : Color.FromArgb(248, 248, 249));
				this.tool_strip_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(147, 145, 176) : SystemColors.ButtonFace);
				this.tool_strip_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(225, 226, 236) : Color.FromArgb(240, 239, 241));
				this.tool_strip_panel_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(215, 215, 229) : SystemColors.ButtonFace);
				this.tool_strip_panel_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(243, 243, 247) : Color.FromArgb(249, 248, 249));
				break;
			case ProfessionalColorTable.ColorSchemes.MediaCenter:
				this.button_checked_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(226, 229, 238) : Color.Empty);
				this.button_checked_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(226, 229, 238) : Color.Empty);
				this.button_checked_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(226, 229, 238) : Color.Empty);
				this.button_checked_highlight = Color.FromArgb(196, 208, 229);
				this.button_checked_highlight_border = Color.FromKnownColor(13);
				this.button_pressed_border = ((!this.use_system_colors) ? Color.FromArgb(51, 94, 168) : Color.FromKnownColor(13));
				this.button_pressed_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(153, 175, 212) : Color.FromArgb(153, 175, 212));
				this.button_pressed_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(153, 175, 212) : Color.FromArgb(153, 175, 212));
				this.button_pressed_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(153, 175, 212) : Color.FromArgb(153, 175, 212));
				this.button_pressed_highlight = ((!this.use_system_colors) ? Color.FromArgb(152, 173, 210) : Color.FromArgb(152, 173, 210));
				this.button_pressed_highlight_border = Color.FromKnownColor(13);
				this.button_selected_border = ((!this.use_system_colors) ? Color.FromArgb(51, 94, 168) : Color.FromKnownColor(13));
				this.button_selected_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(194, 207, 229) : Color.FromArgb(194, 207, 229));
				this.button_selected_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(194, 207, 229) : Color.FromArgb(194, 207, 229));
				this.button_selected_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(194, 207, 229) : Color.FromArgb(194, 207, 229));
				this.button_selected_highlight = ((!this.use_system_colors) ? Color.FromArgb(196, 208, 229) : Color.FromArgb(196, 208, 229));
				this.button_selected_highlight_border = ((!this.use_system_colors) ? Color.FromArgb(51, 94, 168) : Color.FromKnownColor(13));
				this.check_background = ((!this.use_system_colors) ? Color.FromArgb(226, 229, 238) : Color.FromKnownColor(13));
				this.check_pressed_background = ((!this.use_system_colors) ? Color.FromArgb(51, 94, 168) : Color.FromArgb(153, 175, 212));
				this.check_selected_background = ((!this.use_system_colors) ? Color.FromArgb(51, 94, 168) : Color.FromArgb(153, 175, 212));
				this.grip_dark = ((!this.use_system_colors) ? Color.FromArgb(189, 188, 191) : Color.FromArgb(189, 188, 191));
				this.grip_light = ((!this.use_system_colors) ? Color.FromArgb(255, 255, 255) : SystemColors.Window);
				this.image_margin_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(252, 252, 252) : Color.FromArgb(250, 250, 251));
				this.image_margin_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(235, 233, 237) : SystemColors.Control);
				this.image_margin_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(245, 244, 246) : Color.FromArgb(245, 244, 246));
				this.image_margin_revealed_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(247, 246, 248) : Color.FromArgb(247, 246, 248));
				this.image_margin_revealed_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(228, 226, 230) : Color.FromArgb(237, 235, 239));
				this.image_margin_revealed_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(241, 240, 242) : Color.FromArgb(241, 240, 242));
				this.menu_border = ((!this.use_system_colors) ? Color.FromArgb(134, 133, 136) : Color.FromArgb(134, 133, 136));
				this.menu_item_border = ((!this.use_system_colors) ? Color.FromArgb(51, 94, 168) : SystemColors.Highlight);
				this.menu_item_pressed_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(252, 252, 252) : Color.FromArgb(250, 250, 251));
				this.menu_item_pressed_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(245, 244, 246) : Color.FromArgb(245, 244, 246));
				this.menu_item_pressed_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(241, 240, 242) : Color.FromArgb(241, 240, 242));
				this.menu_item_selected = ((!this.use_system_colors) ? Color.FromArgb(194, 207, 229) : SystemColors.Window);
				this.menu_item_selected_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(194, 207, 229) : Color.FromArgb(194, 207, 229));
				this.menu_item_selected_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(194, 207, 229) : Color.FromArgb(194, 207, 229));
				this.menu_strip_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(235, 233, 237) : SystemColors.ButtonFace);
				this.menu_strip_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(251, 250, 251) : Color.FromArgb(251, 250, 251));
				this.overflow_button_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(242, 242, 242) : Color.FromArgb(241, 240, 242));
				this.overflow_button_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(167, 166, 170) : SystemColors.ButtonShadow);
				this.overflow_button_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(224, 224, 225) : Color.FromArgb(237, 235, 239));
				this.rafting_container_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(235, 233, 237) : SystemColors.ButtonFace);
				this.rafting_container_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(251, 250, 251) : Color.FromArgb(251, 250, 251));
				this.separator_dark = ((!this.use_system_colors) ? Color.FromArgb(193, 193, 196) : Color.FromArgb(193, 193, 196));
				this.separator_light = ((!this.use_system_colors) ? Color.FromArgb(255, 255, 255) : SystemColors.ButtonHighlight);
				this.status_strip_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(235, 233, 237) : SystemColors.ButtonFace);
				this.status_strip_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(251, 250, 251) : Color.FromArgb(251, 250, 251));
				this.tool_strip_border = ((!this.use_system_colors) ? Color.FromArgb(238, 237, 240) : Color.FromArgb(238, 237, 240));
				this.tool_strip_content_panel_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(235, 233, 237) : SystemColors.ButtonFace);
				this.tool_strip_content_panel_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(251, 250, 251) : Color.FromArgb(251, 250, 251));
				this.tool_strip_drop_down_background = ((!this.use_system_colors) ? Color.FromArgb(252, 252, 252) : Color.FromArgb(252, 252, 252));
				this.tool_strip_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(252, 252, 252) : Color.FromArgb(250, 250, 251));
				this.tool_strip_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(235, 233, 237) : SystemColors.ButtonFace);
				this.tool_strip_gradient_middle = ((!this.use_system_colors) ? Color.FromArgb(245, 244, 246) : Color.FromArgb(245, 244, 246));
				this.tool_strip_panel_gradient_begin = ((!this.use_system_colors) ? Color.FromArgb(235, 233, 237) : SystemColors.ButtonFace);
				this.tool_strip_panel_gradient_end = ((!this.use_system_colors) ? Color.FromArgb(251, 250, 251) : Color.FromArgb(251, 250, 251));
				break;
			case ProfessionalColorTable.ColorSchemes.Aero:
				this.button_checked_gradient_begin = Color.Empty;
				this.button_checked_gradient_end = Color.Empty;
				this.button_checked_gradient_middle = Color.Empty;
				this.button_checked_highlight = Color.FromArgb(196, 225, 255);
				this.button_checked_highlight_border = Color.FromKnownColor(13);
				this.button_pressed_border = Color.FromKnownColor(13);
				this.button_pressed_gradient_begin = Color.FromArgb(153, 204, 255);
				this.button_pressed_gradient_end = Color.FromArgb(153, 204, 255);
				this.button_pressed_gradient_middle = Color.FromArgb(153, 204, 255);
				this.button_pressed_highlight = Color.FromArgb(152, 203, 255);
				this.button_pressed_highlight_border = Color.FromKnownColor(13);
				this.button_selected_border = ((!this.use_system_colors) ? Color.FromArgb(51, 94, 168) : Color.FromKnownColor(13));
				this.button_selected_gradient_begin = Color.FromArgb(194, 224, 255);
				this.button_selected_gradient_end = Color.FromArgb(194, 224, 255);
				this.button_selected_gradient_middle = Color.FromArgb(194, 224, 255);
				this.button_selected_highlight = Color.FromArgb(196, 225, 255);
				this.button_selected_highlight_border = Color.FromKnownColor(13);
				this.check_background = Color.FromKnownColor(13);
				this.check_pressed_background = Color.FromArgb(153, 204, 255);
				this.check_selected_background = Color.FromArgb(153, 204, 255);
				this.grip_dark = Color.FromArgb(184, 184, 184);
				this.grip_light = SystemColors.Window;
				this.image_margin_gradient_begin = Color.FromArgb(252, 252, 252);
				this.image_margin_gradient_end = SystemColors.Control;
				this.image_margin_gradient_middle = Color.FromArgb(250, 250, 250);
				this.image_margin_revealed_gradient_begin = Color.FromArgb(251, 251, 251);
				this.image_margin_revealed_gradient_end = Color.FromArgb(245, 245, 245);
				this.image_margin_revealed_gradient_middle = Color.FromArgb(247, 247, 247);
				this.menu_border = Color.FromArgb(128, 128, 128);
				this.menu_item_border = SystemColors.Highlight;
				this.menu_item_pressed_gradient_begin = Color.FromArgb(252, 252, 252);
				this.menu_item_pressed_gradient_end = Color.FromArgb(250, 250, 250);
				this.menu_item_pressed_gradient_middle = Color.FromArgb(247, 247, 247);
				this.menu_item_selected = SystemColors.Window;
				this.menu_item_selected_gradient_begin = Color.FromArgb(194, 224, 255);
				this.menu_item_selected_gradient_end = Color.FromArgb(194, 224, 255);
				this.menu_strip_gradient_begin = SystemColors.ButtonFace;
				this.menu_strip_gradient_end = Color.FromArgb(253, 253, 253);
				this.overflow_button_gradient_begin = Color.FromArgb(247, 247, 247);
				this.overflow_button_gradient_end = SystemColors.ButtonShadow;
				this.overflow_button_gradient_middle = Color.FromArgb(245, 245, 245);
				this.rafting_container_gradient_begin = SystemColors.ButtonFace;
				this.rafting_container_gradient_end = Color.FromArgb(253, 253, 253);
				this.separator_dark = Color.FromArgb(189, 189, 189);
				this.separator_light = SystemColors.ButtonHighlight;
				this.status_strip_gradient_begin = SystemColors.ButtonFace;
				this.status_strip_gradient_end = Color.FromArgb(253, 253, 253);
				this.tool_strip_border = Color.FromArgb(246, 246, 246);
				this.tool_strip_content_panel_gradient_begin = SystemColors.ButtonFace;
				this.tool_strip_content_panel_gradient_end = Color.FromArgb(253, 253, 253);
				this.tool_strip_drop_down_background = Color.FromArgb(253, 253, 253);
				this.tool_strip_gradient_begin = Color.FromArgb(252, 252, 252);
				this.tool_strip_gradient_end = SystemColors.ButtonFace;
				this.tool_strip_gradient_middle = Color.FromArgb(250, 250, 250);
				this.tool_strip_panel_gradient_begin = SystemColors.ButtonFace;
				this.tool_strip_panel_gradient_end = Color.FromArgb(253, 253, 253);
				break;
			}
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x000AB25C File Offset: 0x000A945C
		private ProfessionalColorTable.ColorSchemes GetCurrentStyle()
		{
			if (!VisualStyleInformation.IsEnabledByUser || string.IsNullOrEmpty(VisualStylesEngine.Instance.VisualStyleInformationFileName))
			{
				return ProfessionalColorTable.ColorSchemes.Classic;
			}
			string text = Path.GetFileNameWithoutExtension(VisualStylesEngine.Instance.VisualStyleInformationFileName).ToLowerInvariant();
			if (text != null)
			{
				if (ProfessionalColorTable.<>f__switch$mapB == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(2);
					dictionary.Add("aero", 0);
					dictionary.Add("royale", 1);
					ProfessionalColorTable.<>f__switch$mapB = dictionary;
				}
				int num;
				if (ProfessionalColorTable.<>f__switch$mapB.TryGetValue(text, ref num))
				{
					if (num == 0)
					{
						return ProfessionalColorTable.ColorSchemes.Aero;
					}
					if (num == 1)
					{
						return ProfessionalColorTable.ColorSchemes.MediaCenter;
					}
				}
			}
			string colorScheme = VisualStyleInformation.ColorScheme;
			if (colorScheme != null)
			{
				if (ProfessionalColorTable.<>f__switch$mapA == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
					dictionary.Add("NormalColor", 0);
					dictionary.Add("HomeStead", 1);
					dictionary.Add("Metallic", 2);
					ProfessionalColorTable.<>f__switch$mapA = dictionary;
				}
				int num2;
				if (ProfessionalColorTable.<>f__switch$mapA.TryGetValue(colorScheme, ref num2))
				{
					switch (num2)
					{
					case 0:
						return ProfessionalColorTable.ColorSchemes.NormalColor;
					case 1:
						return ProfessionalColorTable.ColorSchemes.HomeStead;
					case 2:
						return ProfessionalColorTable.ColorSchemes.Metallic;
					}
				}
			}
			return ProfessionalColorTable.ColorSchemes.Classic;
		}

		// Token: 0x04001555 RID: 5461
		private bool use_system_colors;

		// Token: 0x04001556 RID: 5462
		private Color button_checked_gradient_begin;

		// Token: 0x04001557 RID: 5463
		private Color button_checked_gradient_end;

		// Token: 0x04001558 RID: 5464
		private Color button_checked_gradient_middle;

		// Token: 0x04001559 RID: 5465
		private Color button_checked_highlight;

		// Token: 0x0400155A RID: 5466
		private Color button_checked_highlight_border;

		// Token: 0x0400155B RID: 5467
		private Color button_pressed_border;

		// Token: 0x0400155C RID: 5468
		private Color button_pressed_gradient_begin;

		// Token: 0x0400155D RID: 5469
		private Color button_pressed_gradient_end;

		// Token: 0x0400155E RID: 5470
		private Color button_pressed_gradient_middle;

		// Token: 0x0400155F RID: 5471
		private Color button_pressed_highlight;

		// Token: 0x04001560 RID: 5472
		private Color button_pressed_highlight_border;

		// Token: 0x04001561 RID: 5473
		private Color button_selected_border;

		// Token: 0x04001562 RID: 5474
		private Color button_selected_gradient_begin;

		// Token: 0x04001563 RID: 5475
		private Color button_selected_gradient_end;

		// Token: 0x04001564 RID: 5476
		private Color button_selected_gradient_middle;

		// Token: 0x04001565 RID: 5477
		private Color button_selected_highlight;

		// Token: 0x04001566 RID: 5478
		private Color button_selected_highlight_border;

		// Token: 0x04001567 RID: 5479
		private Color check_background;

		// Token: 0x04001568 RID: 5480
		private Color check_pressed_background;

		// Token: 0x04001569 RID: 5481
		private Color check_selected_background;

		// Token: 0x0400156A RID: 5482
		private Color grip_dark;

		// Token: 0x0400156B RID: 5483
		private Color grip_light;

		// Token: 0x0400156C RID: 5484
		private Color image_margin_gradient_begin;

		// Token: 0x0400156D RID: 5485
		private Color image_margin_gradient_end;

		// Token: 0x0400156E RID: 5486
		private Color image_margin_gradient_middle;

		// Token: 0x0400156F RID: 5487
		private Color image_margin_revealed_gradient_begin;

		// Token: 0x04001570 RID: 5488
		private Color image_margin_revealed_gradient_end;

		// Token: 0x04001571 RID: 5489
		private Color image_margin_revealed_gradient_middle;

		// Token: 0x04001572 RID: 5490
		private Color menu_border;

		// Token: 0x04001573 RID: 5491
		private Color menu_item_border;

		// Token: 0x04001574 RID: 5492
		private Color menu_item_pressed_gradient_begin;

		// Token: 0x04001575 RID: 5493
		private Color menu_item_pressed_gradient_end;

		// Token: 0x04001576 RID: 5494
		private Color menu_item_pressed_gradient_middle;

		// Token: 0x04001577 RID: 5495
		private Color menu_item_selected;

		// Token: 0x04001578 RID: 5496
		private Color menu_item_selected_gradient_begin;

		// Token: 0x04001579 RID: 5497
		private Color menu_item_selected_gradient_end;

		// Token: 0x0400157A RID: 5498
		private Color menu_strip_gradient_begin;

		// Token: 0x0400157B RID: 5499
		private Color menu_strip_gradient_end;

		// Token: 0x0400157C RID: 5500
		private Color overflow_button_gradient_begin;

		// Token: 0x0400157D RID: 5501
		private Color overflow_button_gradient_end;

		// Token: 0x0400157E RID: 5502
		private Color overflow_button_gradient_middle;

		// Token: 0x0400157F RID: 5503
		private Color rafting_container_gradient_begin;

		// Token: 0x04001580 RID: 5504
		private Color rafting_container_gradient_end;

		// Token: 0x04001581 RID: 5505
		private Color separator_dark;

		// Token: 0x04001582 RID: 5506
		private Color separator_light;

		// Token: 0x04001583 RID: 5507
		private Color status_strip_gradient_begin;

		// Token: 0x04001584 RID: 5508
		private Color status_strip_gradient_end;

		// Token: 0x04001585 RID: 5509
		private Color tool_strip_border;

		// Token: 0x04001586 RID: 5510
		private Color tool_strip_content_panel_gradient_begin;

		// Token: 0x04001587 RID: 5511
		private Color tool_strip_content_panel_gradient_end;

		// Token: 0x04001588 RID: 5512
		private Color tool_strip_drop_down_background;

		// Token: 0x04001589 RID: 5513
		private Color tool_strip_gradient_begin;

		// Token: 0x0400158A RID: 5514
		private Color tool_strip_gradient_end;

		// Token: 0x0400158B RID: 5515
		private Color tool_strip_gradient_middle;

		// Token: 0x0400158C RID: 5516
		private Color tool_strip_panel_gradient_begin;

		// Token: 0x0400158D RID: 5517
		private Color tool_strip_panel_gradient_end;

		// Token: 0x02000299 RID: 665
		private enum ColorSchemes
		{
			// Token: 0x04001591 RID: 5521
			Classic,
			// Token: 0x04001592 RID: 5522
			NormalColor,
			// Token: 0x04001593 RID: 5523
			HomeStead,
			// Token: 0x04001594 RID: 5524
			Metallic,
			// Token: 0x04001595 RID: 5525
			MediaCenter,
			// Token: 0x04001596 RID: 5526
			Aero
		}
	}
}
