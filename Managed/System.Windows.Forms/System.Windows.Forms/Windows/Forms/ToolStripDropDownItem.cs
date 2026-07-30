using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides basic functionality for controls that display a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> when a <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />, <see cref="T:System.Windows.Forms.ToolStripMenuItem" />, or <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> control is clicked.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200034E RID: 846
	[DefaultProperty("DropDownItems")]
	[Designer("System.Windows.Forms.Design.ToolStripMenuItemDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public abstract class ToolStripDropDownItem : ToolStripItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> class. </summary>
		// Token: 0x06003CA0 RID: 15520 RVA: 0x000F3D34 File Offset: 0x000F1F34
		protected ToolStripDropDownItem()
			: this(string.Empty, null, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> class with the specified display text, image, and action to take when the drop-down control is clicked.</summary>
		/// <param name="text">The display text of the drop-down control.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the control.</param>
		/// <param name="onClick">The action to take when the drop-down control is clicked.</param>
		// Token: 0x06003CA1 RID: 15521 RVA: 0x000F3D48 File Offset: 0x000F1F48
		protected ToolStripDropDownItem(string text, Image image, EventHandler onClick)
			: this(text, image, onClick, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> class with the specified display text, image, and <see cref="T:System.Windows.Forms.ToolStripItem" /> collection that the drop-down control contains.</summary>
		/// <param name="text">The display text of the drop-down control.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the control.</param>
		/// <param name="dropDownItems">A <see cref="T:System.Windows.Forms.ToolStripItem" /> collection that the drop-down control contains.</param>
		// Token: 0x06003CA2 RID: 15522 RVA: 0x000F3D58 File Offset: 0x000F1F58
		protected ToolStripDropDownItem(string text, Image image, params ToolStripItem[] dropDownItems)
			: this(text, image, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> class with the specified display text, image, action to take when the drop-down control is clicked, and control name.</summary>
		/// <param name="text">The display text of the drop-down control.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the control.</param>
		/// <param name="onClick">The action to take when the drop-down control is clicked.</param>
		/// <param name="name">The name of the control.</param>
		// Token: 0x06003CA3 RID: 15523 RVA: 0x000F3D68 File Offset: 0x000F1F68
		protected ToolStripDropDownItem(string text, Image image, EventHandler onClick, string name)
			: base(text, image, onClick, name)
		{
		}

		// Token: 0x06003CA4 RID: 15524 RVA: 0x000F3D78 File Offset: 0x000F1F78
		// Note: this type is marked as 'beforefieldinit'.
		static ToolStripDropDownItem()
		{
			ToolStripDropDownItem.DropDownClosedEvent = new object();
			ToolStripDropDownItem.DropDownItemClickedEvent = new object();
			ToolStripDropDownItem.DropDownOpenedEvent = new object();
			ToolStripDropDownItem.DropDownOpeningEvent = new object();
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> closes. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003A4 RID: 932
		// (add) Token: 0x06003CA5 RID: 15525 RVA: 0x000F3DB0 File Offset: 0x000F1FB0
		// (remove) Token: 0x06003CA6 RID: 15526 RVA: 0x000F3DC4 File Offset: 0x000F1FC4
		public event EventHandler DropDownClosed
		{
			add
			{
				base.Events.AddHandler(ToolStripDropDownItem.DropDownClosedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripDropDownItem.DropDownClosedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003A5 RID: 933
		// (add) Token: 0x06003CA7 RID: 15527 RVA: 0x000F3DD8 File Offset: 0x000F1FD8
		// (remove) Token: 0x06003CA8 RID: 15528 RVA: 0x000F3DEC File Offset: 0x000F1FEC
		public event ToolStripItemClickedEventHandler DropDownItemClicked
		{
			add
			{
				base.Events.AddHandler(ToolStripDropDownItem.DropDownItemClickedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripDropDownItem.DropDownItemClickedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> has opened.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003A6 RID: 934
		// (add) Token: 0x06003CA9 RID: 15529 RVA: 0x000F3E00 File Offset: 0x000F2000
		// (remove) Token: 0x06003CAA RID: 15530 RVA: 0x000F3E14 File Offset: 0x000F2014
		public event EventHandler DropDownOpened
		{
			add
			{
				base.Events.AddHandler(ToolStripDropDownItem.DropDownOpenedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripDropDownItem.DropDownOpenedEvent, value);
			}
		}

		/// <summary>Occurs as the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is opening.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003A7 RID: 935
		// (add) Token: 0x06003CAB RID: 15531 RVA: 0x000F3E28 File Offset: 0x000F2028
		// (remove) Token: 0x06003CAC RID: 15532 RVA: 0x000F3E3C File Offset: 0x000F203C
		public event EventHandler DropDownOpening
		{
			add
			{
				base.Events.AddHandler(ToolStripDropDownItem.DropDownOpeningEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripDropDownItem.DropDownOpeningEvent, value);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> that will be displayed when this <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> is clicked.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripDropDown" /> that is associated with the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06003CAD RID: 15533 RVA: 0x000F3E50 File Offset: 0x000F2050
		// (set) Token: 0x06003CAE RID: 15534 RVA: 0x000F3E94 File Offset: 0x000F2094
		[TypeConverter(typeof(ReferenceConverter))]
		public ToolStripDropDown DropDown
		{
			get
			{
				if (this.drop_down == null)
				{
					this.drop_down = this.CreateDefaultDropDown();
					this.drop_down.ItemAdded += this.DropDown_ItemAdded;
				}
				return this.drop_down;
			}
			set
			{
				this.drop_down = value;
				this.drop_down.OwnerItem = this;
			}
		}

		/// <summary>Gets or sets a value indicating the direction in which the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> emerges from its parent container.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property is set to a value that is not one of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection" /> values.</exception>
		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06003CAF RID: 15535 RVA: 0x000F3EAC File Offset: 0x000F20AC
		// (set) Token: 0x06003CB0 RID: 15536 RVA: 0x000F3EB4 File Offset: 0x000F20B4
		[Browsable(false)]
		public ToolStripDropDownDirection DropDownDirection
		{
			get
			{
				return this.drop_down_direction;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ToolStripDropDownDirection), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ToolStripDropDownDirection", value));
				}
				this.drop_down_direction = value;
			}
		}

		/// <summary>Gets the collection of items in the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> that is associated with this <see cref="T:System.Windows.Forms.ToolStripDropDownItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> of controls.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06003CB1 RID: 15537 RVA: 0x000F3EF0 File Offset: 0x000F20F0
		[DesignerSerializationVisibility(2)]
		public ToolStripItemCollection DropDownItems
		{
			get
			{
				return this.DropDown.Items;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> has <see cref="T:System.Windows.Forms.ToolStripDropDown" /> controls associated with it. </summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> has <see cref="T:System.Windows.Forms.ToolStripDropDown" /> controls; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06003CB2 RID: 15538 RVA: 0x000F3F00 File Offset: 0x000F2100
		[Browsable(false)]
		public virtual bool HasDropDownItems
		{
			get
			{
				return this.drop_down != null && this.DropDown.Items.Count != 0;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> is in the pressed state.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> is in the pressed state; otherwise, false. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06003CB3 RID: 15539 RVA: 0x000F3F34 File Offset: 0x000F2134
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public override bool Pressed
		{
			get
			{
				return base.Pressed || (this.drop_down != null && this.DropDown.Visible);
			}
		}

		/// <summary>Gets the screen coordinates, in pixels, of the upper-left corner of the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" />.</summary>
		/// <returns>A Point representing the x and y screen coordinates, in pixels.</returns>
		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06003CB4 RID: 15540 RVA: 0x000F3F68 File Offset: 0x000F2168
		protected internal virtual Point DropDownLocation
		{
			get
			{
				Point point;
				if (base.IsOnDropDown)
				{
					point = base.Parent.PointToScreen(new Point(this.Bounds.Left, this.Bounds.Top - 1));
					point.X += this.Bounds.Width;
					point.Y += this.Bounds.Left;
					return point;
				}
				point..ctor(this.Bounds.Left, this.Bounds.Bottom - 1);
				return base.Parent.PointToScreen(point);
			}
		}

		/// <summary>Makes a visible <see cref="T:System.Windows.Forms.ToolStripDropDown" /> hidden.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003CB5 RID: 15541 RVA: 0x000F4020 File Offset: 0x000F2220
		public void HideDropDown()
		{
			if (this.drop_down == null || !this.DropDown.Visible)
			{
				return;
			}
			this.OnDropDownHide(EventArgs.Empty);
			this.DropDown.Close(ToolStripDropDownCloseReason.CloseCalled);
			this.is_pressed = false;
			base.Invalidate();
		}

		/// <summary>Displays the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> control associated with this <see cref="T:System.Windows.Forms.ToolStripDropDownItem" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> is the same as the parent <see cref="T:System.Windows.Forms.ToolStrip" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003CB6 RID: 15542 RVA: 0x000F4070 File Offset: 0x000F2270
		public void ShowDropDown()
		{
			if (this.DropDown.Visible)
			{
				return;
			}
			this.OnDropDownShow(EventArgs.Empty);
			if (!this.HasDropDownItems)
			{
				return;
			}
			base.Invalidate();
			this.DropDown.Show(this.DropDownLocation);
		}

		// Token: 0x06003CB7 RID: 15543 RVA: 0x000F40BC File Offset: 0x000F22BC
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripDropDownItemAccessibleObject(this);
		}

		/// <summary>Creates a generic <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for which events can be defined.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</returns>
		// Token: 0x06003CB8 RID: 15544 RVA: 0x000F40C4 File Offset: 0x000F22C4
		protected virtual ToolStripDropDown CreateDefaultDropDown()
		{
			return new ToolStripDropDown
			{
				OwnerItem = this
			};
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003CB9 RID: 15545 RVA: 0x000F40E0 File Offset: 0x000F22E0
		protected override void Dispose(bool disposing)
		{
			if (!base.IsDisposed)
			{
				if (this.HasDropDownItems)
				{
					foreach (object obj in this.DropDownItems)
					{
						ToolStripItem toolStripItem = (ToolStripItem)obj;
						if (toolStripItem is ToolStripMenuItem)
						{
							ToolStripManager.RemoveToolStripMenuItem((ToolStripMenuItem)toolStripItem);
						}
					}
				}
				if (this.drop_down != null)
				{
					ToolStripManager.RemoveToolStrip(this.drop_down);
				}
				base.Dispose(disposing);
			}
		}

		// Token: 0x06003CBA RID: 15546 RVA: 0x000F4194 File Offset: 0x000F2394
		protected override void OnBoundsChanged()
		{
			base.OnBoundsChanged();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDownItem.DropDownClosed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003CBB RID: 15547 RVA: 0x000F419C File Offset: 0x000F239C
		protected internal virtual void OnDropDownClosed(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripDropDownItem.DropDownClosedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raised in response to the <see cref="M:System.Windows.Forms.ToolStripDropDownItem.HideDropDown" /> method.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003CBC RID: 15548 RVA: 0x000F41D0 File Offset: 0x000F23D0
		protected virtual void OnDropDownHide(EventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDownItem.DropDownItemClicked" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs" /> that contains the event data.</param>
		// Token: 0x06003CBD RID: 15549 RVA: 0x000F41D4 File Offset: 0x000F23D4
		protected internal virtual void OnDropDownItemClicked(ToolStripItemClickedEventArgs e)
		{
			ToolStripItemClickedEventHandler toolStripItemClickedEventHandler = (ToolStripItemClickedEventHandler)base.Events[ToolStripDropDownItem.DropDownItemClickedEvent];
			if (toolStripItemClickedEventHandler != null)
			{
				toolStripItemClickedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDownItem.DropDownOpened" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003CBE RID: 15550 RVA: 0x000F4208 File Offset: 0x000F2408
		protected internal virtual void OnDropDownOpened(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripDropDownItem.DropDownOpenedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raised in response to the <see cref="M:System.Windows.Forms.ToolStripDropDownItem.ShowDropDown" /> method.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003CBF RID: 15551 RVA: 0x000F423C File Offset: 0x000F243C
		protected virtual void OnDropDownShow(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripDropDownItem.DropDownOpeningEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003CC0 RID: 15552 RVA: 0x000F4270 File Offset: 0x000F2470
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			if (this.drop_down != null)
			{
				this.drop_down.Font = this.Font;
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003CC1 RID: 15553 RVA: 0x000F4298 File Offset: 0x000F2498
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
		}

		/// <returns>false in all cases.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06003CC2 RID: 15554 RVA: 0x000F42A4 File Offset: 0x000F24A4
		protected internal override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			if (this.HasDropDownItems)
			{
				foreach (object obj in this.DropDownItems)
				{
					ToolStripItem toolStripItem = (ToolStripItem)obj;
					if (toolStripItem.ProcessCmdKey(ref m, keyData))
					{
						return true;
					}
				}
			}
			return base.ProcessCmdKey(ref m, keyData);
		}

		/// <returns>true if the key was processed by the item; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06003CC3 RID: 15555 RVA: 0x000F4338 File Offset: 0x000F2538
		protected internal override bool ProcessDialogKey(Keys keyData)
		{
			if (!this.Selected || !this.HasDropDownItems)
			{
				return base.ProcessDialogKey(keyData);
			}
			if (!base.IsOnDropDown)
			{
				if (base.Parent.Orientation == Orientation.Horizontal)
				{
					if (keyData == Keys.Down || keyData == Keys.Return)
					{
						if (base.Parent is MenuStrip)
						{
							(base.Parent as MenuStrip).MenuDroppedDown = true;
						}
						this.ShowDropDown();
						this.DropDown.SelectNextToolStripItem(null, true);
						return true;
					}
				}
				else if (keyData == Keys.Right || keyData == Keys.Return)
				{
					if (base.Parent is MenuStrip)
					{
						(base.Parent as MenuStrip).MenuDroppedDown = true;
					}
					this.ShowDropDown();
					this.DropDown.SelectNextToolStripItem(null, true);
					return true;
				}
			}
			else if ((keyData == Keys.Right || keyData == Keys.Return) && this.HasDropDownItems)
			{
				this.ShowDropDown();
				this.DropDown.SelectNextToolStripItem(null, true);
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x06003CC4 RID: 15556 RVA: 0x000F4450 File Offset: 0x000F2650
		internal override void Dismiss(ToolStripDropDownCloseReason reason)
		{
			if (this.HasDropDownItems && this.DropDown.Visible)
			{
				this.DropDown.Dismiss(reason);
			}
			base.Dismiss(reason);
		}

		// Token: 0x06003CC5 RID: 15557 RVA: 0x000F448C File Offset: 0x000F268C
		internal override void HandleClick(EventArgs e)
		{
			this.OnClick(e);
		}

		// Token: 0x06003CC6 RID: 15558 RVA: 0x000F4498 File Offset: 0x000F2698
		internal void HideDropDown(ToolStripDropDownCloseReason reason)
		{
			if (this.drop_down == null || !this.DropDown.Visible)
			{
				return;
			}
			this.OnDropDownHide(EventArgs.Empty);
			this.DropDown.Close(reason);
			this.is_pressed = false;
			base.Invalidate();
		}

		// Token: 0x06003CC7 RID: 15559 RVA: 0x000F44E8 File Offset: 0x000F26E8
		private void DropDown_ItemAdded(object sender, ToolStripItemEventArgs e)
		{
			e.Item.owner_item = this;
		}

		// Token: 0x04001A80 RID: 6784
		internal ToolStripDropDown drop_down;

		// Token: 0x04001A81 RID: 6785
		private ToolStripDropDownDirection drop_down_direction;
	}
}
