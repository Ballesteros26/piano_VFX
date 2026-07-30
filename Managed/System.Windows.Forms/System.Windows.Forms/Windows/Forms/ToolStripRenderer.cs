using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace System.Windows.Forms
{
	/// <summary>Handles the painting functionality for <see cref="T:System.Windows.Forms.ToolStrip" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000375 RID: 885
	public abstract class ToolStripRenderer
	{
		// Token: 0x06003F94 RID: 16276 RVA: 0x000FDED8 File Offset: 0x000FC0D8
		// Note: this type is marked as 'beforefieldinit'.
		static ToolStripRenderer()
		{
			float[][] array = new float[5][];
			int num = 0;
			float[] array2 = new float[5];
			array2[0] = 0.22f;
			array2[1] = 0.22f;
			array2[2] = 0.22f;
			array[num] = array2;
			int num2 = 1;
			float[] array3 = new float[5];
			array3[0] = 0.27f;
			array3[1] = 0.27f;
			array3[2] = 0.27f;
			array[num2] = array3;
			int num3 = 2;
			float[] array4 = new float[5];
			array4[0] = 0.04f;
			array4[1] = 0.04f;
			array4[2] = 0.04f;
			array[num3] = array4;
			array[3] = new float[] { 0.365f, 0.365f, 0.365f, 0.7f, 0f };
			array[4] = new float[]
			{
				default(float),
				default(float),
				default(float),
				default(float),
				1f
			};
			ToolStripRenderer.grayscale_matrix = new ColorMatrix(array);
			ToolStripRenderer.RenderArrowEvent = new object();
			ToolStripRenderer.RenderButtonBackgroundEvent = new object();
			ToolStripRenderer.RenderDropDownButtonBackgroundEvent = new object();
			ToolStripRenderer.RenderGripEvent = new object();
			ToolStripRenderer.RenderImageMarginEvent = new object();
			ToolStripRenderer.RenderItemBackgroundEvent = new object();
			ToolStripRenderer.RenderItemCheckEvent = new object();
			ToolStripRenderer.RenderItemImageEvent = new object();
			ToolStripRenderer.RenderItemTextEvent = new object();
			ToolStripRenderer.RenderLabelBackgroundEvent = new object();
			ToolStripRenderer.RenderMenuItemBackgroundEvent = new object();
			ToolStripRenderer.RenderOverflowButtonBackgroundEvent = new object();
			ToolStripRenderer.RenderSeparatorEvent = new object();
			ToolStripRenderer.RenderSplitButtonBackgroundEvent = new object();
			ToolStripRenderer.RenderStatusStripSizingGripEvent = new object();
			ToolStripRenderer.RenderToolStripBackgroundEvent = new object();
			ToolStripRenderer.RenderToolStripBorderEvent = new object();
			ToolStripRenderer.RenderToolStripContentPanelBackgroundEvent = new object();
			ToolStripRenderer.RenderToolStripPanelBackgroundEvent = new object();
			ToolStripRenderer.RenderToolStripStatusLabelBackgroundEvent = new object();
		}

		/// <summary>Occurs when an arrow on a <see cref="T:System.Windows.Forms.ToolStripItem" /> is rendered.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003D8 RID: 984
		// (add) Token: 0x06003F95 RID: 16277 RVA: 0x000FE048 File Offset: 0x000FC248
		// (remove) Token: 0x06003F96 RID: 16278 RVA: 0x000FE05C File Offset: 0x000FC25C
		public event ToolStripArrowRenderEventHandler RenderArrow
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderArrowEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderArrowEvent, value);
			}
		}

		/// <summary>Occurs when the background for a <see cref="T:System.Windows.Forms.ToolStripButton" /> is rendered</summary>
		// Token: 0x140003D9 RID: 985
		// (add) Token: 0x06003F97 RID: 16279 RVA: 0x000FE070 File Offset: 0x000FC270
		// (remove) Token: 0x06003F98 RID: 16280 RVA: 0x000FE084 File Offset: 0x000FC284
		public event ToolStripItemRenderEventHandler RenderButtonBackground
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderButtonBackgroundEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderButtonBackgroundEvent, value);
			}
		}

		/// <summary>Occurs when the background for a <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> is rendered.</summary>
		// Token: 0x140003DA RID: 986
		// (add) Token: 0x06003F99 RID: 16281 RVA: 0x000FE098 File Offset: 0x000FC298
		// (remove) Token: 0x06003F9A RID: 16282 RVA: 0x000FE0AC File Offset: 0x000FC2AC
		public event ToolStripItemRenderEventHandler RenderDropDownButtonBackground
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderDropDownButtonBackgroundEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderDropDownButtonBackgroundEvent, value);
			}
		}

		/// <summary>Occurs when the move handle for a <see cref="T:System.Windows.Forms.ToolStrip" /> is rendered.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003DB RID: 987
		// (add) Token: 0x06003F9B RID: 16283 RVA: 0x000FE0C0 File Offset: 0x000FC2C0
		// (remove) Token: 0x06003F9C RID: 16284 RVA: 0x000FE0D4 File Offset: 0x000FC2D4
		public event ToolStripGripRenderEventHandler RenderGrip
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderGripEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderGripEvent, value);
			}
		}

		/// <summary>Draws the margin between an image and its container. </summary>
		// Token: 0x140003DC RID: 988
		// (add) Token: 0x06003F9D RID: 16285 RVA: 0x000FE0E8 File Offset: 0x000FC2E8
		// (remove) Token: 0x06003F9E RID: 16286 RVA: 0x000FE0FC File Offset: 0x000FC2FC
		public event ToolStripRenderEventHandler RenderImageMargin
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderImageMarginEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderImageMarginEvent, value);
			}
		}

		/// <summary>Occurs when the background for a <see cref="T:System.Windows.Forms.ToolStripItem" /> is rendered.</summary>
		// Token: 0x140003DD RID: 989
		// (add) Token: 0x06003F9F RID: 16287 RVA: 0x000FE110 File Offset: 0x000FC310
		// (remove) Token: 0x06003FA0 RID: 16288 RVA: 0x000FE124 File Offset: 0x000FC324
		public event ToolStripItemRenderEventHandler RenderItemBackground
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderItemBackgroundEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderItemBackgroundEvent, value);
			}
		}

		/// <summary>Occurs when the image for a selected <see cref="T:System.Windows.Forms.ToolStripItem" /> is rendered.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003DE RID: 990
		// (add) Token: 0x06003FA1 RID: 16289 RVA: 0x000FE138 File Offset: 0x000FC338
		// (remove) Token: 0x06003FA2 RID: 16290 RVA: 0x000FE14C File Offset: 0x000FC34C
		public event ToolStripItemImageRenderEventHandler RenderItemCheck
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderItemCheckEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderItemCheckEvent, value);
			}
		}

		/// <summary>Occurs when the image for a <see cref="T:System.Windows.Forms.ToolStripItem" /> is rendered.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003DF RID: 991
		// (add) Token: 0x06003FA3 RID: 16291 RVA: 0x000FE160 File Offset: 0x000FC360
		// (remove) Token: 0x06003FA4 RID: 16292 RVA: 0x000FE174 File Offset: 0x000FC374
		public event ToolStripItemImageRenderEventHandler RenderItemImage
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderItemImageEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderItemImageEvent, value);
			}
		}

		/// <summary>Occurs when the text for a <see cref="T:System.Windows.Forms.ToolStripItem" /> is rendered.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003E0 RID: 992
		// (add) Token: 0x06003FA5 RID: 16293 RVA: 0x000FE188 File Offset: 0x000FC388
		// (remove) Token: 0x06003FA6 RID: 16294 RVA: 0x000FE19C File Offset: 0x000FC39C
		public event ToolStripItemTextRenderEventHandler RenderItemText
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderItemTextEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderItemTextEvent, value);
			}
		}

		/// <summary>Occurs when the background for a <see cref="T:System.Windows.Forms.ToolStripLabel" /> is rendered.</summary>
		// Token: 0x140003E1 RID: 993
		// (add) Token: 0x06003FA7 RID: 16295 RVA: 0x000FE1B0 File Offset: 0x000FC3B0
		// (remove) Token: 0x06003FA8 RID: 16296 RVA: 0x000FE1C4 File Offset: 0x000FC3C4
		public event ToolStripItemRenderEventHandler RenderLabelBackground
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderLabelBackgroundEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderLabelBackgroundEvent, value);
			}
		}

		/// <summary>Occurs when the background for a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is rendered.</summary>
		// Token: 0x140003E2 RID: 994
		// (add) Token: 0x06003FA9 RID: 16297 RVA: 0x000FE1D8 File Offset: 0x000FC3D8
		// (remove) Token: 0x06003FAA RID: 16298 RVA: 0x000FE1EC File Offset: 0x000FC3EC
		public event ToolStripItemRenderEventHandler RenderMenuItemBackground
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderMenuItemBackgroundEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderMenuItemBackgroundEvent, value);
			}
		}

		/// <summary>Occurs when the background for an overflow button is rendered.</summary>
		// Token: 0x140003E3 RID: 995
		// (add) Token: 0x06003FAB RID: 16299 RVA: 0x000FE200 File Offset: 0x000FC400
		// (remove) Token: 0x06003FAC RID: 16300 RVA: 0x000FE214 File Offset: 0x000FC414
		public event ToolStripItemRenderEventHandler RenderOverflowButtonBackground
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderOverflowButtonBackgroundEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderOverflowButtonBackgroundEvent, value);
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Windows.Forms.ToolStripSeparator" /> is rendered.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003E4 RID: 996
		// (add) Token: 0x06003FAD RID: 16301 RVA: 0x000FE228 File Offset: 0x000FC428
		// (remove) Token: 0x06003FAE RID: 16302 RVA: 0x000FE23C File Offset: 0x000FC43C
		public event ToolStripSeparatorRenderEventHandler RenderSeparator
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderSeparatorEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderSeparatorEvent, value);
			}
		}

		/// <summary>Occurs when the background for a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is rendered.</summary>
		// Token: 0x140003E5 RID: 997
		// (add) Token: 0x06003FAF RID: 16303 RVA: 0x000FE250 File Offset: 0x000FC450
		// (remove) Token: 0x06003FB0 RID: 16304 RVA: 0x000FE264 File Offset: 0x000FC464
		public event ToolStripItemRenderEventHandler RenderSplitButtonBackground
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderSplitButtonBackgroundEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderSplitButtonBackgroundEvent, value);
			}
		}

		/// <summary>Occurs when the display style changes.</summary>
		// Token: 0x140003E6 RID: 998
		// (add) Token: 0x06003FB1 RID: 16305 RVA: 0x000FE278 File Offset: 0x000FC478
		// (remove) Token: 0x06003FB2 RID: 16306 RVA: 0x000FE28C File Offset: 0x000FC48C
		public event ToolStripRenderEventHandler RenderStatusStripSizingGrip
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderStatusStripSizingGripEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderStatusStripSizingGripEvent, value);
			}
		}

		/// <summary>Occurs when the background for a <see cref="T:System.Windows.Forms.ToolStrip" /> is rendered.</summary>
		// Token: 0x140003E7 RID: 999
		// (add) Token: 0x06003FB3 RID: 16307 RVA: 0x000FE2A0 File Offset: 0x000FC4A0
		// (remove) Token: 0x06003FB4 RID: 16308 RVA: 0x000FE2B4 File Offset: 0x000FC4B4
		public event ToolStripRenderEventHandler RenderToolStripBackground
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderToolStripBackgroundEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderToolStripBackgroundEvent, value);
			}
		}

		/// <summary>Occurs when the border for a <see cref="T:System.Windows.Forms.ToolStrip" /> is rendered.</summary>
		// Token: 0x140003E8 RID: 1000
		// (add) Token: 0x06003FB5 RID: 16309 RVA: 0x000FE2C8 File Offset: 0x000FC4C8
		// (remove) Token: 0x06003FB6 RID: 16310 RVA: 0x000FE2DC File Offset: 0x000FC4DC
		public event ToolStripRenderEventHandler RenderToolStripBorder
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderToolStripBorderEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderToolStripBorderEvent, value);
			}
		}

		/// <summary>Draws the background of a <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
		// Token: 0x140003E9 RID: 1001
		// (add) Token: 0x06003FB7 RID: 16311 RVA: 0x000FE2F0 File Offset: 0x000FC4F0
		// (remove) Token: 0x06003FB8 RID: 16312 RVA: 0x000FE304 File Offset: 0x000FC504
		public event ToolStripContentPanelRenderEventHandler RenderToolStripContentPanelBackground
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderToolStripContentPanelBackgroundEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderToolStripContentPanelBackgroundEvent, value);
			}
		}

		/// <summary>Draws the background of a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		// Token: 0x140003EA RID: 1002
		// (add) Token: 0x06003FB9 RID: 16313 RVA: 0x000FE318 File Offset: 0x000FC518
		// (remove) Token: 0x06003FBA RID: 16314 RVA: 0x000FE32C File Offset: 0x000FC52C
		public event ToolStripPanelRenderEventHandler RenderToolStripPanelBackground
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderToolStripPanelBackgroundEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderToolStripPanelBackgroundEvent, value);
			}
		}

		/// <summary>Draws the background of a <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</summary>
		// Token: 0x140003EB RID: 1003
		// (add) Token: 0x06003FBB RID: 16315 RVA: 0x000FE340 File Offset: 0x000FC540
		// (remove) Token: 0x06003FBC RID: 16316 RVA: 0x000FE354 File Offset: 0x000FC554
		public event ToolStripItemRenderEventHandler RenderToolStripStatusLabelBackground
		{
			add
			{
				this.Events.AddHandler(ToolStripRenderer.RenderToolStripStatusLabelBackgroundEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(ToolStripRenderer.RenderToolStripStatusLabelBackgroundEvent, value);
			}
		}

		/// <summary>Creates a gray-scale copy of a given image.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that is a copy of the given image, but with a gray-scale color matrix.</returns>
		/// <param name="normalImage">The image to be copied. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FBD RID: 16317 RVA: 0x000FE368 File Offset: 0x000FC568
		public static Image CreateDisabledImage(Image normalImage)
		{
			if (normalImage == null)
			{
				return null;
			}
			ImageAttributes imageAttributes = new ImageAttributes();
			imageAttributes.SetColorMatrix(ToolStripRenderer.grayscale_matrix);
			Bitmap bitmap = new Bitmap(normalImage.Width, normalImage.Height);
			Graphics.FromImage(bitmap).DrawImage(normalImage, new Rectangle(0, 0, normalImage.Width, normalImage.Height), 0, 0, normalImage.Width, normalImage.Height, 2, imageAttributes);
			return bitmap;
		}

		/// <summary>Draws an arrow on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripArrowRenderEventArgs" /> that contains data to draw the arrow.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003FBE RID: 16318 RVA: 0x000FE3D0 File Offset: 0x000FC5D0
		public void DrawArrow(ToolStripArrowRenderEventArgs e)
		{
			this.OnRenderArrow(e);
		}

		/// <summary>Draws the background for a <see cref="T:System.Windows.Forms.ToolStripButton" />.</summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains data to draw the button's background.</param>
		// Token: 0x06003FBF RID: 16319 RVA: 0x000FE3DC File Offset: 0x000FC5DC
		public void DrawButtonBackground(ToolStripItemRenderEventArgs e)
		{
			this.OnRenderButtonBackground(e);
		}

		/// <summary>Draws the background for a <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the data to draw the drop-down button's background.</param>
		// Token: 0x06003FC0 RID: 16320 RVA: 0x000FE3E8 File Offset: 0x000FC5E8
		public void DrawDropDownButtonBackground(ToolStripItemRenderEventArgs e)
		{
			this.OnRenderDropDownButtonBackground(e);
		}

		/// <summary>Draws a move handle on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripGripRenderEventArgs" /> that contains the data to draw the move handle.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FC1 RID: 16321 RVA: 0x000FE3F4 File Offset: 0x000FC5F4
		public void DrawGrip(ToolStripGripRenderEventArgs e)
		{
			this.OnRenderGrip(e);
		}

		/// <summary>Draws the space around an image on a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the data to draw the space around the image.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FC2 RID: 16322 RVA: 0x000FE400 File Offset: 0x000FC600
		public void DrawImageMargin(ToolStripRenderEventArgs e)
		{
			this.OnRenderImageMargin(e);
		}

		/// <summary>Draws the background for a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the data to draw the background of the item.</param>
		// Token: 0x06003FC3 RID: 16323 RVA: 0x000FE40C File Offset: 0x000FC60C
		public void DrawItemBackground(ToolStripItemRenderEventArgs e)
		{
			this.OnRenderItemBackground(e);
		}

		/// <summary>Draws an image on a <see cref="T:System.Windows.Forms.ToolStripItem" /> that indicates the item is in a selected state.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> that contains the data to draw the selected image.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FC4 RID: 16324 RVA: 0x000FE418 File Offset: 0x000FC618
		public void DrawItemCheck(ToolStripItemImageRenderEventArgs e)
		{
			this.OnRenderItemCheck(e);
		}

		/// <summary>Draws an image on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> that contains the data to draw the image.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FC5 RID: 16325 RVA: 0x000FE424 File Offset: 0x000FC624
		public void DrawItemImage(ToolStripItemImageRenderEventArgs e)
		{
			this.OnRenderItemImage(e);
		}

		/// <summary>Draws text on a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemTextRenderEventArgs" /> that contains the data to draw the text.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003FC6 RID: 16326 RVA: 0x000FE430 File Offset: 0x000FC630
		public void DrawItemText(ToolStripItemTextRenderEventArgs e)
		{
			this.OnRenderItemText(e);
		}

		/// <summary>Draws the background for a <see cref="T:System.Windows.Forms.ToolStripLabel" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the data to draw the background for the label.</param>
		// Token: 0x06003FC7 RID: 16327 RVA: 0x000FE43C File Offset: 0x000FC63C
		public void DrawLabelBackground(ToolStripItemRenderEventArgs e)
		{
			this.OnRenderLabelBackground(e);
		}

		/// <summary>Draws the background for a <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the data to draw the background for the menu item.</param>
		// Token: 0x06003FC8 RID: 16328 RVA: 0x000FE448 File Offset: 0x000FC648
		public void DrawMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			this.OnRenderMenuItemBackground(e);
		}

		/// <summary>Draws the background for an overflow button.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FC9 RID: 16329 RVA: 0x000FE454 File Offset: 0x000FC654
		public void DrawOverflowButtonBackground(ToolStripItemRenderEventArgs e)
		{
			this.OnRenderOverflowButtonBackground(e);
		}

		/// <summary>Draws a <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripSeparatorRenderEventArgs" /> that contains the data to draw the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FCA RID: 16330 RVA: 0x000FE460 File Offset: 0x000FC660
		public void DrawSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			this.OnRenderSeparator(e);
		}

		/// <summary>Draws a <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003FCB RID: 16331 RVA: 0x000FE46C File Offset: 0x000FC66C
		public void DrawSplitButton(ToolStripItemRenderEventArgs e)
		{
			this.OnRenderSplitButtonBackground(e);
		}

		/// <summary>Draws a sizing grip.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FCC RID: 16332 RVA: 0x000FE478 File Offset: 0x000FC678
		public void DrawStatusStripSizingGrip(ToolStripRenderEventArgs e)
		{
			this.OnRenderStatusStripSizingGrip(e);
		}

		/// <summary>Draws the background for a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the data to draw the background for the <see cref="T:System.Windows.Forms.ToolStrip" />.</param>
		// Token: 0x06003FCD RID: 16333 RVA: 0x000FE484 File Offset: 0x000FC684
		public void DrawToolStripBackground(ToolStripRenderEventArgs e)
		{
			this.OnRenderToolStripBackground(e);
		}

		/// <summary>Draws the border for a <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the data to draw the border for the <see cref="T:System.Windows.Forms.ToolStrip" />.</param>
		// Token: 0x06003FCE RID: 16334 RVA: 0x000FE490 File Offset: 0x000FC690
		public void DrawToolStripBorder(ToolStripRenderEventArgs e)
		{
			this.OnRenderToolStripBorder(e);
		}

		/// <summary>Draws the background of the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripContentPanelRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FCF RID: 16335 RVA: 0x000FE49C File Offset: 0x000FC69C
		public void DrawToolStripContentPanelBackground(ToolStripContentPanelRenderEventArgs e)
		{
			this.OnRenderToolStripContentPanelBackground(e);
		}

		/// <summary>Draws the background of the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripPanelRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FD0 RID: 16336 RVA: 0x000FE4A8 File Offset: 0x000FC6A8
		public void DrawToolStripPanelBackground(ToolStripPanelRenderEventArgs e)
		{
			this.OnRenderToolStripPanelBackground(e);
		}

		/// <summary>Draws the background of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FD1 RID: 16337 RVA: 0x000FE4B4 File Offset: 0x000FC6B4
		public void DrawToolStripStatusLabelBackground(ToolStripItemRenderEventArgs e)
		{
			this.OnRenderToolStripStatusLabelBackground(e);
		}

		/// <summary>When overridden in a derived class, provides for custom initialization of the given <see cref="T:System.Windows.Forms.ToolStrip" />. </summary>
		/// <param name="toolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> to be initialized.</param>
		// Token: 0x06003FD2 RID: 16338 RVA: 0x000FE4C0 File Offset: 0x000FC6C0
		protected internal virtual void Initialize(ToolStrip toolStrip)
		{
		}

		/// <summary>Initializes the specified <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
		/// <param name="contentPanel">The <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</param>
		// Token: 0x06003FD3 RID: 16339 RVA: 0x000FE4C4 File Offset: 0x000FC6C4
		protected internal virtual void InitializeContentPanel(ToolStripContentPanel contentPanel)
		{
		}

		/// <summary>When overridden in a derived class, provides for custom initialization of the given <see cref="T:System.Windows.Forms.ToolStripItem" />. </summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to be initialized.</param>
		// Token: 0x06003FD4 RID: 16340 RVA: 0x000FE4C8 File Offset: 0x000FC6C8
		protected internal virtual void InitializeItem(ToolStripItem item)
		{
		}

		/// <summary>Initializes the specified <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <param name="toolStripPanel">The <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
		// Token: 0x06003FD5 RID: 16341 RVA: 0x000FE4CC File Offset: 0x000FC6CC
		protected internal virtual void InitializePanel(ToolStripPanel toolStripPanel)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderArrow" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripArrowRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FD6 RID: 16342 RVA: 0x000FE4D0 File Offset: 0x000FC6D0
		protected virtual void OnRenderArrow(ToolStripArrowRenderEventArgs e)
		{
			ArrowDirection direction = e.Direction;
			if (direction != ArrowDirection.Left)
			{
				if (direction != ArrowDirection.Up)
				{
					if (direction != ArrowDirection.Right)
					{
						if (direction == ArrowDirection.Down)
						{
							using (Pen pen = new Pen(e.ArrowColor))
							{
								int num = e.ArrowRectangle.Left + e.ArrowRectangle.Width / 2 - 3;
								int num2 = e.ArrowRectangle.Top + e.ArrowRectangle.Height / 2 - 2;
								ToolStripRenderer.DrawDownArrow(e.Graphics, pen, num, num2);
							}
						}
					}
					else
					{
						using (Pen pen2 = new Pen(e.ArrowColor))
						{
							int num3 = e.ArrowRectangle.Left + e.ArrowRectangle.Width / 2 - 3;
							int num4 = e.ArrowRectangle.Top + e.ArrowRectangle.Height / 2 - 4;
							ToolStripRenderer.DrawRightArrow(e.Graphics, pen2, num3, num4);
						}
					}
				}
			}
			ToolStripArrowRenderEventHandler toolStripArrowRenderEventHandler = (ToolStripArrowRenderEventHandler)this.Events[ToolStripRenderer.RenderArrowEvent];
			if (toolStripArrowRenderEventHandler != null)
			{
				toolStripArrowRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderButtonBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06003FD7 RID: 16343 RVA: 0x000FE668 File Offset: 0x000FC868
		protected virtual void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripItemRenderEventHandler toolStripItemRenderEventHandler = (ToolStripItemRenderEventHandler)this.Events[ToolStripRenderer.RenderButtonBackgroundEvent];
			if (toolStripItemRenderEventHandler != null)
			{
				toolStripItemRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderDropDownButtonBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06003FD8 RID: 16344 RVA: 0x000FE69C File Offset: 0x000FC89C
		protected virtual void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripItemRenderEventHandler toolStripItemRenderEventHandler = (ToolStripItemRenderEventHandler)this.Events[ToolStripRenderer.RenderDropDownButtonBackgroundEvent];
			if (toolStripItemRenderEventHandler != null)
			{
				toolStripItemRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderGrip" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripGripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06003FD9 RID: 16345 RVA: 0x000FE6D0 File Offset: 0x000FC8D0
		protected virtual void OnRenderGrip(ToolStripGripRenderEventArgs e)
		{
			ToolStripGripRenderEventHandler toolStripGripRenderEventHandler = (ToolStripGripRenderEventHandler)this.Events[ToolStripRenderer.RenderGripEvent];
			if (toolStripGripRenderEventHandler != null)
			{
				toolStripGripRenderEventHandler(this, e);
			}
		}

		/// <summary>Draws the item background.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06003FDA RID: 16346 RVA: 0x000FE704 File Offset: 0x000FC904
		protected virtual void OnRenderImageMargin(ToolStripRenderEventArgs e)
		{
			ToolStripRenderEventHandler toolStripRenderEventHandler = (ToolStripRenderEventHandler)this.Events[ToolStripRenderer.RenderImageMarginEvent];
			if (toolStripRenderEventHandler != null)
			{
				toolStripRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ToolStripSystemRenderer.OnRenderItemBackground(System.Windows.Forms.ToolStripItemRenderEventArgs)" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06003FDB RID: 16347 RVA: 0x000FE738 File Offset: 0x000FC938
		protected virtual void OnRenderItemBackground(ToolStripItemRenderEventArgs e)
		{
			if (e.Item.BackgroundImage != null)
			{
				Rectangle rectangle;
				rectangle..ctor(0, 0, e.Item.Width, e.Item.Height);
				e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(e.Item.BackColor), rectangle);
				this.DrawBackground(e.Graphics, rectangle, e.Item.BackgroundImage, e.Item.BackgroundImageLayout);
			}
			ToolStripItemRenderEventHandler toolStripItemRenderEventHandler = (ToolStripItemRenderEventHandler)this.Events[ToolStripRenderer.RenderItemBackgroundEvent];
			if (toolStripItemRenderEventHandler != null)
			{
				toolStripItemRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderItemCheck" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FDC RID: 16348 RVA: 0x000FE7E4 File Offset: 0x000FC9E4
		protected virtual void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
		{
			ToolStripItemImageRenderEventHandler toolStripItemImageRenderEventHandler = (ToolStripItemImageRenderEventHandler)this.Events[ToolStripRenderer.RenderItemCheckEvent];
			if (toolStripItemImageRenderEventHandler != null)
			{
				toolStripItemImageRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderItemImage" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06003FDD RID: 16349 RVA: 0x000FE818 File Offset: 0x000FCA18
		protected virtual void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
		{
			bool flag = false;
			Image image = e.Image;
			if (e.Item.RightToLeft == RightToLeft.Yes && e.Item.RightToLeftAutoMirrorImage)
			{
				image = ToolStripRenderer.CreateMirrorImage(image);
				flag = true;
			}
			if (e.Item.ImageTransparentColor != Color.Empty)
			{
				ImageAttributes imageAttributes = new ImageAttributes();
				imageAttributes.SetColorKey(e.Item.ImageTransparentColor, e.Item.ImageTransparentColor);
				e.Graphics.DrawImage(image, e.ImageRectangle, 0, 0, image.Width, image.Height, 2, imageAttributes);
				imageAttributes.Dispose();
			}
			else
			{
				e.Graphics.DrawImage(image, e.ImageRectangle);
			}
			if (flag)
			{
				image.Dispose();
			}
			ToolStripItemImageRenderEventHandler toolStripItemImageRenderEventHandler = (ToolStripItemImageRenderEventHandler)this.Events[ToolStripRenderer.RenderItemImageEvent];
			if (toolStripItemImageRenderEventHandler != null)
			{
				toolStripItemImageRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderItemText" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemTextRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06003FDE RID: 16350 RVA: 0x000FE904 File Offset: 0x000FCB04
		protected virtual void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
		{
			if (e.TextDirection == ToolStripTextDirection.Vertical90)
			{
				GraphicsState graphicsState = e.Graphics.Save();
				PointF pointF;
				pointF..ctor(e.Graphics.Transform.OffsetX, e.Graphics.Transform.OffsetY);
				e.Graphics.ResetTransform();
				e.Graphics.RotateTransform(90f);
				RectangleF rectangleF;
				rectangleF..ctor((float)((e.Item.Height - e.TextRectangle.Height) / 2), ((float)e.TextRectangle.Width + pointF.X) * -1f - 18f, (float)e.TextRectangle.Height, (float)e.TextRectangle.Width);
				StringFormat stringFormat = new StringFormat();
				stringFormat.Alignment = 1;
				e.Graphics.DrawString(e.Text, e.TextFont, ThemeEngine.Current.ResPool.GetSolidBrush(e.TextColor), rectangleF, stringFormat);
				e.Graphics.Restore(graphicsState);
			}
			else if (e.TextDirection == ToolStripTextDirection.Vertical270)
			{
				GraphicsState graphicsState2 = e.Graphics.Save();
				PointF pointF2;
				pointF2..ctor(e.Graphics.Transform.OffsetX, e.Graphics.Transform.OffsetY);
				e.Graphics.ResetTransform();
				e.Graphics.RotateTransform(270f);
				RectangleF rectangleF2;
				rectangleF2..ctor((float)(-(float)e.TextRectangle.Height - (e.Item.Height - e.TextRectangle.Height) / 2), (float)e.TextRectangle.Width + pointF2.X + 4f, (float)e.TextRectangle.Height, (float)e.TextRectangle.Width);
				StringFormat stringFormat2 = new StringFormat();
				stringFormat2.Alignment = 1;
				e.Graphics.DrawString(e.Text, e.TextFont, ThemeEngine.Current.ResPool.GetSolidBrush(e.TextColor), rectangleF2, stringFormat2);
				e.Graphics.Restore(graphicsState2);
			}
			else
			{
				TextRenderer.DrawText(e.Graphics, e.Text, e.TextFont, e.TextRectangle, e.TextColor, e.TextFormat);
			}
			ToolStripItemTextRenderEventHandler toolStripItemTextRenderEventHandler = (ToolStripItemTextRenderEventHandler)this.Events[ToolStripRenderer.RenderItemTextEvent];
			if (toolStripItemTextRenderEventHandler != null)
			{
				toolStripItemTextRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderLabelBackground" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FDF RID: 16351 RVA: 0x000FEB90 File Offset: 0x000FCD90
		protected virtual void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripItemRenderEventHandler toolStripItemRenderEventHandler = (ToolStripItemRenderEventHandler)this.Events[ToolStripRenderer.RenderLabelBackgroundEvent];
			if (toolStripItemRenderEventHandler != null)
			{
				toolStripItemRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderMenuItemBackground" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FE0 RID: 16352 RVA: 0x000FEBC4 File Offset: 0x000FCDC4
		protected virtual void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripItemRenderEventHandler toolStripItemRenderEventHandler = (ToolStripItemRenderEventHandler)this.Events[ToolStripRenderer.RenderMenuItemBackgroundEvent];
			if (toolStripItemRenderEventHandler != null)
			{
				toolStripItemRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderOverflowButtonBackground" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06003FE1 RID: 16353 RVA: 0x000FEBF8 File Offset: 0x000FCDF8
		protected virtual void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripItemRenderEventHandler toolStripItemRenderEventHandler = (ToolStripItemRenderEventHandler)this.Events[ToolStripRenderer.RenderOverflowButtonBackgroundEvent];
			if (toolStripItemRenderEventHandler != null)
			{
				toolStripItemRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderSeparator" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripSeparatorRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06003FE2 RID: 16354 RVA: 0x000FEC2C File Offset: 0x000FCE2C
		protected virtual void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			ToolStripSeparatorRenderEventHandler toolStripSeparatorRenderEventHandler = (ToolStripSeparatorRenderEventHandler)this.Events[ToolStripRenderer.RenderSeparatorEvent];
			if (toolStripSeparatorRenderEventHandler != null)
			{
				toolStripSeparatorRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ToolStripRenderer.OnRenderSplitButtonBackground(System.Windows.Forms.ToolStripItemRenderEventArgs)" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FE3 RID: 16355 RVA: 0x000FEC60 File Offset: 0x000FCE60
		protected virtual void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripItemRenderEventHandler toolStripItemRenderEventHandler = (ToolStripItemRenderEventHandler)this.Events[ToolStripRenderer.RenderSplitButtonBackgroundEvent];
			if (toolStripItemRenderEventHandler != null)
			{
				toolStripItemRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderStatusStripSizingGrip" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FE4 RID: 16356 RVA: 0x000FEC94 File Offset: 0x000FCE94
		protected virtual void OnRenderStatusStripSizingGrip(ToolStripRenderEventArgs e)
		{
			StatusStrip statusStrip = (StatusStrip)e.ToolStrip;
			if (statusStrip.SizingGrip)
			{
				this.DrawSizingGrip(e.Graphics, statusStrip.SizeGripBounds);
			}
			ToolStripRenderEventHandler toolStripRenderEventHandler = (ToolStripRenderEventHandler)this.Events[ToolStripRenderer.RenderStatusStripSizingGripEvent];
			if (toolStripRenderEventHandler != null)
			{
				toolStripRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderToolStripBackground" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data. </param>
		// Token: 0x06003FE5 RID: 16357 RVA: 0x000FECF0 File Offset: 0x000FCEF0
		protected virtual void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			ToolStripRenderEventHandler toolStripRenderEventHandler = (ToolStripRenderEventHandler)this.Events[ToolStripRenderer.RenderToolStripBackgroundEvent];
			if (toolStripRenderEventHandler != null)
			{
				toolStripRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderToolStripBorder" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FE6 RID: 16358 RVA: 0x000FED24 File Offset: 0x000FCF24
		protected virtual void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			ToolStripRenderEventHandler toolStripRenderEventHandler = (ToolStripRenderEventHandler)this.Events[ToolStripRenderer.RenderToolStripBorderEvent];
			if (toolStripRenderEventHandler != null)
			{
				toolStripRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderToolStripContentPanelBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripContentPanelRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FE7 RID: 16359 RVA: 0x000FED58 File Offset: 0x000FCF58
		protected virtual void OnRenderToolStripContentPanelBackground(ToolStripContentPanelRenderEventArgs e)
		{
			ToolStripContentPanelRenderEventHandler toolStripContentPanelRenderEventHandler = (ToolStripContentPanelRenderEventHandler)this.Events[ToolStripRenderer.RenderToolStripContentPanelBackgroundEvent];
			if (toolStripContentPanelRenderEventHandler != null)
			{
				toolStripContentPanelRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderToolStripPanelBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripPanelRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FE8 RID: 16360 RVA: 0x000FED8C File Offset: 0x000FCF8C
		protected virtual void OnRenderToolStripPanelBackground(ToolStripPanelRenderEventArgs e)
		{
			ToolStripPanelRenderEventHandler toolStripPanelRenderEventHandler = (ToolStripPanelRenderEventHandler)this.Events[ToolStripRenderer.RenderToolStripPanelBackgroundEvent];
			if (toolStripPanelRenderEventHandler != null)
			{
				toolStripPanelRenderEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderToolStripStatusLabelBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06003FE9 RID: 16361 RVA: 0x000FEDC0 File Offset: 0x000FCFC0
		protected virtual void OnRenderToolStripStatusLabelBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripItemRenderEventHandler toolStripItemRenderEventHandler = (ToolStripItemRenderEventHandler)this.Events[ToolStripRenderer.RenderToolStripStatusLabelBackgroundEvent];
			if (toolStripItemRenderEventHandler != null)
			{
				toolStripItemRenderEventHandler(this, e);
			}
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x06003FEA RID: 16362 RVA: 0x000FEDF4 File Offset: 0x000FCFF4
		private EventHandlerList Events
		{
			get
			{
				if (this.events == null)
				{
					this.events = new EventHandlerList();
				}
				return this.events;
			}
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x000FEE14 File Offset: 0x000FD014
		internal static Image CreateMirrorImage(Image normalImage)
		{
			if (normalImage == null)
			{
				return null;
			}
			Bitmap bitmap = new Bitmap(normalImage);
			bitmap.RotateFlip(4);
			return bitmap;
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x000FEE38 File Offset: 0x000FD038
		private void DrawBackground(Graphics g, Rectangle bounds, Image image, ImageLayout layout)
		{
			if ((layout == ImageLayout.Center || layout == ImageLayout.Tile) && image.Size.Width >= bounds.Size.Width && image.Size.Height >= bounds.Size.Height)
			{
				layout = ImageLayout.None;
			}
			switch (layout)
			{
			case ImageLayout.None:
				g.DrawImageUnscaledAndClipped(image, bounds);
				break;
			case ImageLayout.Tile:
			{
				int i = 0;
				for (int j = 0; j < bounds.Height; j += image.Height)
				{
					while (i < bounds.Width)
					{
						g.DrawImageUnscaledAndClipped(image, bounds);
						i += image.Width;
					}
					i = 0;
				}
				break;
			}
			case ImageLayout.Center:
			{
				Rectangle rectangle;
				rectangle..ctor((bounds.Size.Width - image.Size.Width) / 2, (bounds.Size.Height - image.Size.Height) / 2, image.Width, image.Height);
				g.DrawImageUnscaledAndClipped(image, rectangle);
				break;
			}
			case ImageLayout.Stretch:
				g.DrawImage(image, bounds);
				break;
			case ImageLayout.Zoom:
				if ((float)image.Height / (float)image.Width < (float)bounds.Height / (float)bounds.Width)
				{
					Rectangle rectangle2;
					rectangle2..ctor(0, 0, bounds.Width, (int)((float)bounds.Width * ((float)image.Height / (float)image.Width)));
					rectangle2.Y = (bounds.Height - rectangle2.Height) / 2;
					g.DrawImage(image, rectangle2);
				}
				else
				{
					Rectangle rectangle3;
					rectangle3..ctor(0, 0, (int)((float)bounds.Height * ((float)image.Width / (float)image.Height)), bounds.Height);
					rectangle3.X = (bounds.Width - rectangle3.Width) / 2;
					g.DrawImage(image, rectangle3);
				}
				break;
			}
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x000FF050 File Offset: 0x000FD250
		internal static void DrawRightArrow(Graphics g, Pen p, int x, int y)
		{
			g.DrawLine(p, x, y, x, y + 6);
			g.DrawLine(p, x + 1, y + 1, x + 1, y + 5);
			g.DrawLine(p, x + 2, y + 2, x + 2, y + 4);
			g.DrawLine(p, x + 2, y + 3, x + 3, y + 3);
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x000FF0A4 File Offset: 0x000FD2A4
		internal static void DrawDownArrow(Graphics g, Pen p, int x, int y)
		{
			g.DrawLine(p, x + 1, y, x + 5, y);
			g.DrawLine(p, x + 2, y + 1, x + 4, y + 1);
			g.DrawLine(p, x + 3, y + 1, x + 3, y + 2);
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x000FF0E8 File Offset: 0x000FD2E8
		private void DrawSizingGrip(Graphics g, Rectangle rect)
		{
			this.DrawGripBox(g, rect.Right - 5, rect.Bottom - 5);
			this.DrawGripBox(g, rect.Right - 9, rect.Bottom - 5);
			this.DrawGripBox(g, rect.Right - 5, rect.Bottom - 9);
			this.DrawGripBox(g, rect.Right - 13, rect.Bottom - 5);
			this.DrawGripBox(g, rect.Right - 5, rect.Bottom - 13);
			this.DrawGripBox(g, rect.Right - 9, rect.Bottom - 9);
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x000FF194 File Offset: 0x000FD394
		private void DrawGripBox(Graphics g, int x, int y)
		{
			g.DrawRectangle(Pens.White, x + 1, y + 1, 1, 1);
			g.DrawRectangle(ThemeEngine.Current.ResPool.GetPen(Color.FromArgb(172, 168, 153)), x, y, 1, 1);
		}

		// Token: 0x04001B43 RID: 6979
		private static ColorMatrix grayscale_matrix;

		// Token: 0x04001B44 RID: 6980
		private EventHandlerList events;
	}
}
