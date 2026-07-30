using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a combination of a standard button on the left and a drop-down button on the right, or the other way around if the value of <see cref="T:System.Windows.Forms.RightToLeft" /> is Yes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200037A RID: 890
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
	[DefaultEvent("ButtonClick")]
	public class ToolStripSplitButton : ToolStripDropDownItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class.</summary>
		// Token: 0x06004031 RID: 16433 RVA: 0x000FF55C File Offset: 0x000FD75C
		public ToolStripSplitButton()
			: this(string.Empty, null, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class with the specified image. </summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		// Token: 0x06004032 RID: 16434 RVA: 0x000FF570 File Offset: 0x000FD770
		public ToolStripSplitButton(Image image)
			: this(string.Empty, image, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class with the specified text. </summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		// Token: 0x06004033 RID: 16435 RVA: 0x000FF584 File Offset: 0x000FD784
		public ToolStripSplitButton(string text)
			: this(text, null, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class with the specified text and image.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		// Token: 0x06004034 RID: 16436 RVA: 0x000FF594 File Offset: 0x000FD794
		public ToolStripSplitButton(string text, Image image)
			: this(text, image, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class with the specified display text, image, and <see cref="E:System.Windows.Forms.Control.Click" /> event handler.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		/// <param name="onClick">Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the user clicks the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		// Token: 0x06004035 RID: 16437 RVA: 0x000FF5A4 File Offset: 0x000FD7A4
		public ToolStripSplitButton(string text, Image image, EventHandler onClick)
			: this(text, image, onClick, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class with the specified text, image, and <see cref="T:System.Windows.Forms.ToolStripItem" /> array.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		/// <param name="dropDownItems">A <see cref="T:System.Windows.Forms.ToolStripItem" /> array of controls.</param>
		// Token: 0x06004036 RID: 16438 RVA: 0x000FF5B4 File Offset: 0x000FD7B4
		public ToolStripSplitButton(string text, Image image, params ToolStripItem[] dropDownItems)
			: base(text, image, dropDownItems)
		{
			this.ResetDropDownButtonWidth();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> class with the specified display text, image, <see cref="E:System.Windows.Forms.Control.Click" /> event handler, and name.</summary>
		/// <param name="text">The text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		/// <param name="onClick">Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the user clicks the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</param>
		// Token: 0x06004037 RID: 16439 RVA: 0x000FF5C8 File Offset: 0x000FD7C8
		public ToolStripSplitButton(string text, Image image, EventHandler onClick, string name)
			: base(text, image, onClick, name)
		{
			this.ResetDropDownButtonWidth();
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x000FF5DC File Offset: 0x000FD7DC
		// Note: this type is marked as 'beforefieldinit'.
		static ToolStripSplitButton()
		{
			ToolStripSplitButton.ButtonClickEvent = new object();
			ToolStripSplitButton.ButtonDoubleClickEvent = new object();
			ToolStripSplitButton.DefaultItemChangedEvent = new object();
		}

		/// <summary>Occurs when the standard button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003EF RID: 1007
		// (add) Token: 0x06004039 RID: 16441 RVA: 0x000FF5FC File Offset: 0x000FD7FC
		// (remove) Token: 0x0600403A RID: 16442 RVA: 0x000FF610 File Offset: 0x000FD810
		public event EventHandler ButtonClick
		{
			add
			{
				base.Events.AddHandler(ToolStripSplitButton.ButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripSplitButton.ButtonClickEvent, value);
			}
		}

		/// <summary>Occurs when the standard button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is double-clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003F0 RID: 1008
		// (add) Token: 0x0600403B RID: 16443 RVA: 0x000FF624 File Offset: 0x000FD824
		// (remove) Token: 0x0600403C RID: 16444 RVA: 0x000FF638 File Offset: 0x000FD838
		public event EventHandler ButtonDoubleClick
		{
			add
			{
				base.Events.AddHandler(ToolStripSplitButton.ButtonDoubleClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripSplitButton.ButtonDoubleClickEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripSplitButton.DefaultItem" /> has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003F1 RID: 1009
		// (add) Token: 0x0600403D RID: 16445 RVA: 0x000FF64C File Offset: 0x000FD84C
		// (remove) Token: 0x0600403E RID: 16446 RVA: 0x000FF660 File Offset: 0x000FD860
		public event EventHandler DefaultItemChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripSplitButton.DefaultItemChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripSplitButton.DefaultItemChangedEvent, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether default or custom <see cref="T:System.Windows.Forms.ToolTip" /> text is displayed on the <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</summary>
		/// <returns>true if default <see cref="T:System.Windows.Forms.ToolTip" /> text is displayed; otherwise, false. The default is true.</returns>
		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x0600403F RID: 16447 RVA: 0x000FF674 File Offset: 0x000FD874
		// (set) Token: 0x06004040 RID: 16448 RVA: 0x000FF67C File Offset: 0x000FD87C
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

		/// <summary>Gets the size and location of the standard button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the standard button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x06004041 RID: 16449 RVA: 0x000FF688 File Offset: 0x000FD888
		[Browsable(false)]
		public Rectangle ButtonBounds
		{
			get
			{
				return new Rectangle(this.Bounds.Left, this.Bounds.Top, this.Bounds.Width - this.drop_down_button_width - 1, base.Height);
			}
		}

		/// <summary>Gets a value indicating whether the button portion of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is in the pressed state. </summary>
		/// <returns>true if the button portion of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is in the pressed state; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x06004042 RID: 16450 RVA: 0x000FF6D4 File Offset: 0x000FD8D4
		[Browsable(false)]
		public bool ButtonPressed
		{
			get
			{
				return this.button_pressed;
			}
		}

		/// <summary>Gets a value indicating whether the standard button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is selected or the <see cref="P:System.Windows.Forms.ToolStripSplitButton.DropDownButtonPressed" /> property is true.</summary>
		/// <returns>true if the button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is selected or whether <see cref="P:System.Windows.Forms.ToolStripSplitButton.DropDownButtonPressed" /> is true; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x06004043 RID: 16451 RVA: 0x000FF6DC File Offset: 0x000FD8DC
		[Browsable(false)]
		public bool ButtonSelected
		{
			get
			{
				return base.Selected;
			}
		}

		/// <summary>Gets or sets the portion of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> that is activated when the control is first selected.</summary>
		/// <returns>A Forms.ToolStripItem representing the portion of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> that is activated when first selected. The default value is null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x06004044 RID: 16452 RVA: 0x000FF6E4 File Offset: 0x000FD8E4
		// (set) Token: 0x06004045 RID: 16453 RVA: 0x000FF6EC File Offset: 0x000FD8EC
		[DefaultValue(null)]
		[Browsable(false)]
		public ToolStripItem DefaultItem
		{
			get
			{
				return this.default_item;
			}
			set
			{
				if (this.default_item != value)
				{
					this.default_item = value;
					this.OnDefaultItemChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the size and location, in screen coordinates, of the drop-down button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the drop-down button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" />, in screen coordinates.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x06004046 RID: 16454 RVA: 0x000FF70C File Offset: 0x000FD90C
		[Browsable(false)]
		public Rectangle DropDownButtonBounds
		{
			get
			{
				return new Rectangle(this.Bounds.Right - this.drop_down_button_width, 0, this.drop_down_button_width, this.Bounds.Height);
			}
		}

		/// <summary>Gets a value indicating whether the drop-down portion of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is in the pressed state. </summary>
		/// <returns>true if the drop-down portion of the <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is in the pressed state; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x06004047 RID: 16455 RVA: 0x000FF748 File Offset: 0x000FD948
		[Browsable(false)]
		public bool DropDownButtonPressed
		{
			get
			{
				return this.drop_down_button_selected || (this.HasDropDownItems && base.DropDown.Visible);
			}
		}

		/// <summary>Gets a value indicating whether the drop-down button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is selected.</summary>
		/// <returns>true if the drop-down button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> is selected; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x06004048 RID: 16456 RVA: 0x000FF77C File Offset: 0x000FD97C
		[Browsable(false)]
		public bool DropDownButtonSelected
		{
			get
			{
				return base.Selected;
			}
		}

		/// <summary>The width, in pixels, of the drop-down button portion of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the width in pixels.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than zero (0). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x06004049 RID: 16457 RVA: 0x000FF784 File Offset: 0x000FD984
		// (set) Token: 0x0600404A RID: 16458 RVA: 0x000FF78C File Offset: 0x000FD98C
		public int DropDownButtonWidth
		{
			get
			{
				return this.drop_down_button_width;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (this.drop_down_button_width != value)
				{
					this.drop_down_button_width = value;
					base.CalculateAutoSize();
				}
			}
		}

		/// <summary>Gets the boundaries of the separator between the standard and drop-down button portions of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size and location of the separator.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x0600404B RID: 16459 RVA: 0x000FF7C0 File Offset: 0x000FD9C0
		[Browsable(false)]
		public Rectangle SplitterBounds
		{
			get
			{
				return new Rectangle(this.Bounds.Width - this.drop_down_button_width - 1, 0, 1, base.Height);
			}
		}

		/// <summary>Gets a value indicating whether to display the <see cref="T:System.Windows.Forms.ToolTip" /> that is defined as the default. </summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x0600404C RID: 16460 RVA: 0x000FF7F4 File Offset: 0x000FD9F4
		protected override bool DefaultAutoToolTip
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether items on a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> are hidden after they are clicked.</summary>
		/// <returns>true if the items are hidden after they are clicked; otherwise, false.</returns>
		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x0600404D RID: 16461 RVA: 0x000FF7F8 File Offset: 0x000FD9F8
		protected internal override bool DismissWhenClicked
		{
			get
			{
				return true;
			}
		}

		/// <summary>Retrieves the size of a rectangular area into which a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> can be fitted.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" />, representing the width and height of a rectangle.</returns>
		/// <param name="constrainingSize">The custom-sized area for a control. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600404E RID: 16462 RVA: 0x000FF7FC File Offset: 0x000FD9FC
		public override Size GetPreferredSize(Size constrainingSize)
		{
			Size preferredSize = base.GetPreferredSize(constrainingSize);
			if (preferredSize.Width < 23)
			{
				preferredSize.Width = 23;
			}
			if (base.AutoSize)
			{
				preferredSize.Width += this.drop_down_button_width - 2;
			}
			return preferredSize;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripSplitButton.ButtonDoubleClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600404F RID: 16463 RVA: 0x000FF84C File Offset: 0x000FDA4C
		public virtual void OnButtonDoubleClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripSplitButton.ButtonDoubleClickEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>If the <see cref="P:System.Windows.Forms.ToolStripItem.Enabled" /> property is true, calls the <see cref="M:System.Windows.Forms.ToolStripSplitButton.OnButtonClick(System.EventArgs)" /> method.</summary>
		// Token: 0x06004050 RID: 16464 RVA: 0x000FF880 File Offset: 0x000FDA80
		public void PerformButtonClick()
		{
			if (this.Enabled)
			{
				this.OnButtonClick(EventArgs.Empty);
			}
		}

		/// <summary>This method is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004051 RID: 16465 RVA: 0x000FF898 File Offset: 0x000FDA98
		[EditorBrowsable(1)]
		public virtual void ResetDropDownButtonWidth()
		{
			this.DropDownButtonWidth = 11;
		}

		// Token: 0x06004052 RID: 16466 RVA: 0x000FF8A4 File Offset: 0x000FDAA4
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripSplitButton.ToolStripSplitButtonAccessibleObject(this);
		}

		// Token: 0x06004053 RID: 16467 RVA: 0x000FF8AC File Offset: 0x000FDAAC
		protected override ToolStripDropDown CreateDefaultDropDown()
		{
			return new ToolStripDropDownMenu
			{
				OwnerItem = this
			};
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripSplitButton.ButtonClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06004054 RID: 16468 RVA: 0x000FF8C8 File Offset: 0x000FDAC8
		protected virtual void OnButtonClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripSplitButton.ButtonClickEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripSplitButton.DefaultItemChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06004055 RID: 16469 RVA: 0x000FF8FC File Offset: 0x000FDAFC
		protected virtual void OnDefaultItemChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripSplitButton.DefaultItemChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06004056 RID: 16470 RVA: 0x000FF930 File Offset: 0x000FDB30
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (this.ButtonBounds.Contains(e.Location))
			{
				this.button_pressed = true;
				base.Invalidate();
				base.OnMouseDown(e);
			}
			else if (this.DropDownButtonBounds.Contains(e.Location))
			{
				if (base.DropDown.Visible)
				{
					base.HideDropDown(ToolStripDropDownCloseReason.ItemClicked);
				}
				else
				{
					base.ShowDropDown();
				}
				base.Invalidate();
				base.OnMouseDown(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06004057 RID: 16471 RVA: 0x000FF9B8 File Offset: 0x000FDBB8
		protected override void OnMouseLeave(EventArgs e)
		{
			this.drop_down_button_selected = false;
			this.button_pressed = false;
			base.Invalidate();
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06004058 RID: 16472 RVA: 0x000FF9D8 File Offset: 0x000FDBD8
		protected override void OnMouseUp(MouseEventArgs e)
		{
			this.button_pressed = false;
			base.Invalidate();
			base.OnMouseUp(e);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x06004059 RID: 16473 RVA: 0x000FF9F0 File Offset: 0x000FDBF0
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (base.Owner != null)
			{
				Color color = ((!this.Enabled) ? SystemColors.GrayText : this.ForeColor);
				Image image = ((!this.Enabled) ? ToolStripRenderer.CreateDisabledImage(this.Image) : this.Image);
				base.Owner.Renderer.DrawSplitButton(new ToolStripItemRenderEventArgs(e.Graphics, this));
				Rectangle contentRectangle = base.ContentRectangle;
				contentRectangle.Width -= this.drop_down_button_width + 1;
				Rectangle rectangle;
				Rectangle rectangle2;
				base.CalculateTextAndImageRectangles(contentRectangle, out rectangle, out rectangle2);
				if (rectangle != Rectangle.Empty)
				{
					base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, this.Text, rectangle, color, this.Font, this.TextAlign));
				}
				if (rectangle2 != Rectangle.Empty)
				{
					base.Owner.Renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, image, rectangle2));
				}
				base.Owner.Renderer.DrawArrow(new ToolStripArrowRenderEventArgs(e.Graphics, this, new Rectangle(base.Width - 9, 1, 6, base.Height), Color.Black, ArrowDirection.Down));
				return;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600405A RID: 16474 RVA: 0x000FFB3C File Offset: 0x000FDD3C
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
		}

		// Token: 0x0600405B RID: 16475 RVA: 0x000FFB48 File Offset: 0x000FDD48
		protected internal override bool ProcessDialogKey(Keys keyData)
		{
			if (this.Selected && keyData == Keys.Return && this.DefaultItem != null)
			{
				this.DefaultItem.FireEvent(EventArgs.Empty, ToolStripItemEventType.Click);
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}

		/// <returns>true in all cases.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x0600405C RID: 16476 RVA: 0x000FFB90 File Offset: 0x000FDD90
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

		// Token: 0x0600405D RID: 16477 RVA: 0x000FFBD4 File Offset: 0x000FDDD4
		internal override void HandleClick(EventArgs e)
		{
			base.HandleClick(e);
			MouseEventArgs mouseEventArgs = e as MouseEventArgs;
			if (mouseEventArgs != null && this.ButtonBounds.Contains(mouseEventArgs.Location))
			{
				this.OnButtonClick(EventArgs.Empty);
			}
		}

		// Token: 0x04001B64 RID: 7012
		private bool button_pressed;

		// Token: 0x04001B65 RID: 7013
		private ToolStripItem default_item;

		// Token: 0x04001B66 RID: 7014
		private bool drop_down_button_selected;

		// Token: 0x04001B67 RID: 7015
		private int drop_down_button_width;

		/// <summary>Provides information that accessibility applications use to adjust the user interface of a <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> for users with impairments.</summary>
		// Token: 0x0200037B RID: 891
		public class ToolStripSplitButtonAccessibleObject : ToolStripItem.ToolStripItemAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSplitButton.ToolStripSplitButtonAccessibleObject" /> class. </summary>
			/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> that owns this <see cref="T:System.Windows.Forms.ToolStripSplitButton.ToolStripSplitButtonAccessibleObject" />.</param>
			// Token: 0x0600405E RID: 16478 RVA: 0x000FFC1C File Offset: 0x000FDE1C
			public ToolStripSplitButtonAccessibleObject(ToolStripSplitButton item)
				: base(item)
			{
			}

			/// <summary>Performs the default action associated with this <see cref="T:System.Windows.Forms.ToolStripSplitButton.ToolStripSplitButtonAccessibleObject" />.</summary>
			// Token: 0x0600405F RID: 16479 RVA: 0x000FFC28 File Offset: 0x000FDE28
			public override void DoDefaultAction()
			{
				(this.owner_item as ToolStripSplitButton).PerformButtonClick();
			}
		}
	}
}
