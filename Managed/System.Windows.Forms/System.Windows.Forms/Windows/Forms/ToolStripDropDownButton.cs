using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a control that when clicked displays an associated <see cref="T:System.Windows.Forms.ToolStripDropDown" /> from which the user can select a single item.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000349 RID: 841
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
	public class ToolStripDropDownButton : ToolStripDropDownItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class. </summary>
		// Token: 0x06003C89 RID: 15497 RVA: 0x000F3A1C File Offset: 0x000F1C1C
		public ToolStripDropDownButton()
			: this(string.Empty, null, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class that displays the specified image.</summary>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		// Token: 0x06003C8A RID: 15498 RVA: 0x000F3A30 File Offset: 0x000F1C30
		public ToolStripDropDownButton(Image image)
			: this(string.Empty, image, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class that displays the specified text.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		// Token: 0x06003C8B RID: 15499 RVA: 0x000F3A44 File Offset: 0x000F1C44
		public ToolStripDropDownButton(string text)
			: this(text, null, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class that displays the specified text and image.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		// Token: 0x06003C8C RID: 15500 RVA: 0x000F3A54 File Offset: 0x000F1C54
		public ToolStripDropDownButton(string text, Image image)
			: this(text, image, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class that displays the specified text and image and raises the Click event.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		/// <param name="onClick">The event handler for the <see cref="E:System.Windows.Forms.Control.Click" /> event.</param>
		// Token: 0x06003C8D RID: 15501 RVA: 0x000F3A64 File Offset: 0x000F1C64
		public ToolStripDropDownButton(string text, Image image, EventHandler onClick)
			: this(text, image, onClick, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		/// <param name="dropDownItems">An array of type <see cref="T:System.Windows.Forms.ToolStripItem" /> containing the items of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		// Token: 0x06003C8E RID: 15502 RVA: 0x000F3A74 File Offset: 0x000F1C74
		public ToolStripDropDownButton(string text, Image image, params ToolStripItem[] dropDownItems)
		{
			this.show_drop_down_arrow = true;
			base..ctor(text, image, dropDownItems);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> class that has the specified name, displays the specified text and image, and raises the Click event.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		/// <param name="onClick">The event handler for the <see cref="E:System.Windows.Forms.Control.Click" /> event.</param>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />.</param>
		// Token: 0x06003C8F RID: 15503 RVA: 0x000F3A88 File Offset: 0x000F1C88
		public ToolStripDropDownButton(string text, Image image, EventHandler onClick, string name)
		{
			this.show_drop_down_arrow = true;
			base..ctor(text, image, onClick, name);
		}

		/// <summary>Gets or sets a value indicating whether to use the Text property or the <see cref="P:System.Windows.Forms.ToolStripItem.ToolTipText" /> property for the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> ToolTip.</summary>
		/// <returns>true to use the <see cref="P:System.Windows.Forms.Control.Text" /> property for the ToolTip; otherwise, false. The default is true.</returns>
		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06003C90 RID: 15504 RVA: 0x000F3A9C File Offset: 0x000F1C9C
		// (set) Token: 0x06003C91 RID: 15505 RVA: 0x000F3AA4 File Offset: 0x000F1CA4
		[DefaultValue(true)]
		public new bool AutoToolTip
		{
			get
			{
				return base.AutoToolTip;
			}
			set
			{
				base.AutoToolTip = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether an arrow is displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />, which indicates that further options are available in a drop-down list.</summary>
		/// <returns>true to show an arrow on the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06003C92 RID: 15506 RVA: 0x000F3AB0 File Offset: 0x000F1CB0
		// (set) Token: 0x06003C93 RID: 15507 RVA: 0x000F3AB8 File Offset: 0x000F1CB8
		[DefaultValue(true)]
		public bool ShowDropDownArrow
		{
			get
			{
				return this.show_drop_down_arrow;
			}
			set
			{
				if (this.show_drop_down_arrow != value)
				{
					this.show_drop_down_arrow = value;
					base.CalculateAutoSize();
				}
			}
		}

		/// <summary>Gets a value indicating whether to display the <see cref="T:System.Windows.Forms.ToolTip" /> that is defined as the default.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06003C94 RID: 15508 RVA: 0x000F3AD4 File Offset: 0x000F1CD4
		protected override bool DefaultAutoToolTip
		{
			get
			{
				return true;
			}
		}

		/// <summary>Creates a generic <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for which events can be defined.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</returns>
		// Token: 0x06003C95 RID: 15509 RVA: 0x000F3AD8 File Offset: 0x000F1CD8
		protected override ToolStripDropDown CreateDefaultDropDown()
		{
			return new ToolStripDropDownMenu
			{
				OwnerItem = this
			};
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06003C96 RID: 15510 RVA: 0x000F3AF4 File Offset: 0x000F1CF4
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (base.DropDown.Visible)
				{
					base.HideDropDown(ToolStripDropDownCloseReason.ItemClicked);
				}
				else
				{
					base.ShowDropDown();
				}
			}
			base.OnMouseDown(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003C97 RID: 15511 RVA: 0x000F3B3C File Offset: 0x000F1D3C
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" />  that contains the event data. </param>
		// Token: 0x06003C98 RID: 15512 RVA: 0x000F3B48 File Offset: 0x000F1D48
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06003C99 RID: 15513 RVA: 0x000F3B54 File Offset: 0x000F1D54
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (base.Owner != null)
			{
				Color color = ((!this.Enabled) ? SystemColors.GrayText : this.ForeColor);
				Image image = ((!this.Enabled) ? ToolStripRenderer.CreateDisabledImage(this.Image) : this.Image);
				base.Owner.Renderer.DrawDropDownButtonBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
				Rectangle rectangle;
				Rectangle rectangle2;
				base.CalculateTextAndImageRectangles(out rectangle, out rectangle2);
				if (rectangle != Rectangle.Empty)
				{
					base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, this.Text, rectangle, color, this.Font, this.TextAlign));
				}
				if (rectangle2 != Rectangle.Empty)
				{
					base.Owner.Renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, image, rectangle2));
				}
				if (this.ShowDropDownArrow)
				{
					base.Owner.Renderer.DrawArrow(new ToolStripArrowRenderEventArgs(e.Graphics, this, new Rectangle(base.Width - 10, 0, 6, base.Height), Color.Black, ArrowDirection.Down));
				}
				return;
			}
		}

		/// <summary>Retrieves a value indicating whether the drop-down list of the <see cref="T:System.Windows.Forms.ToolStripDropDownButton" /> has items.</summary>
		/// <returns>true if the drop-down list has items; otherwise, false.</returns>
		/// <param name="charCode">The character to process.</param>
		// Token: 0x06003C9A RID: 15514 RVA: 0x000F3C8C File Offset: 0x000F1E8C
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (!this.Selected)
			{
				base.Parent.ChangeSelection(this);
			}
			if (this.HasDropDownItems)
			{
				base.ShowDropDown();
			}
			else
			{
				base.PerformClick();
			}
			return true;
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x000F3CD0 File Offset: 0x000F1ED0
		internal override Size CalculatePreferredSize(Size constrainingSize)
		{
			Size size = base.CalculatePreferredSize(constrainingSize);
			if (this.ShowDropDownArrow)
			{
				size.Width += 9;
			}
			return size;
		}

		// Token: 0x04001A6F RID: 6767
		private bool show_drop_down_arrow;
	}
}
