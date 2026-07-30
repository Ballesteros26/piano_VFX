using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows control to display a list of items. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000213 RID: 531
	[Designer("System.Windows.Forms.Design.ListBoxDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("Items")]
	[DefaultEvent("SelectedIndexChanged")]
	[ComVisible(true)]
	[ClassInterface(1)]
	[DefaultBindingProperty("SelectedValue")]
	public class ListBox : ListControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListBox" /> class.</summary>
		// Token: 0x060020A2 RID: 8354 RVA: 0x00079E4C File Offset: 0x0007804C
		public ListBox()
		{
			this.items = this.CreateItemCollection();
			this.selected_indices = new ListBox.SelectedIndexCollection(this);
			this.selected_items = new ListBox.SelectedObjectCollection(this);
			this.requested_height = this.bounds.Height;
			base.InternalBorderStyle = BorderStyle.Fixed3D;
			this.BackColor = ThemeEngine.Current.ColorWindow;
			this.vscrollbar = new ImplicitVScrollBar();
			this.vscrollbar.Minimum = 0;
			this.vscrollbar.SmallChange = 1;
			this.vscrollbar.LargeChange = 1;
			this.vscrollbar.Maximum = 0;
			this.vscrollbar.ValueChanged += new EventHandler(this.VerticalScrollEvent);
			this.vscrollbar.Visible = false;
			this.hscrollbar = new ImplicitHScrollBar();
			this.hscrollbar.Minimum = 0;
			this.hscrollbar.SmallChange = 1;
			this.hscrollbar.LargeChange = 1;
			this.hscrollbar.Maximum = 0;
			this.hscrollbar.Visible = false;
			this.hscrollbar.ValueChanged += new EventHandler(this.HorizontalScrollEvent);
			base.Controls.AddImplicit(this.vscrollbar);
			base.Controls.AddImplicit(this.hscrollbar);
			base.MouseDown += this.OnMouseDownLB;
			base.MouseMove += this.OnMouseMoveLB;
			base.MouseUp += this.OnMouseUpLB;
			base.MouseWheel += this.OnMouseWheelLB;
			base.KeyUp += this.OnKeyUpLB;
			base.GotFocus += new EventHandler(this.OnGotFocus);
			base.LostFocus += new EventHandler(this.OnLostFocus);
			base.SetStyle(ControlStyles.UserPaint, false);
			this.custom_tab_offsets = new ListBox.IntegerCollection(this);
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x0007A064 File Offset: 0x00078264
		// Note: this type is marked as 'beforefieldinit'.
		static ListBox()
		{
			ListBox.DrawItemEvent = new object();
			ListBox.MeasureItemEvent = new object();
			ListBox.SelectedIndexChangedEvent = new object();
			ListBox.UIASelectionModeChangedEvent = new object();
			ListBox.UIAFocusedItemChangedEvent = new object();
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListBox.BackgroundImage" /> property of the label changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001F9 RID: 505
		// (add) Token: 0x060020A4 RID: 8356 RVA: 0x0007A0A4 File Offset: 0x000782A4
		// (remove) Token: 0x060020A5 RID: 8357 RVA: 0x0007A0B0 File Offset: 0x000782B0
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListBox.BackgroundImageLayout" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001FA RID: 506
		// (add) Token: 0x060020A6 RID: 8358 RVA: 0x0007A0BC File Offset: 0x000782BC
		// (remove) Token: 0x060020A7 RID: 8359 RVA: 0x0007A0C8 File Offset: 0x000782C8
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ListBox" /> control is clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001FB RID: 507
		// (add) Token: 0x060020A8 RID: 8360 RVA: 0x0007A0D4 File Offset: 0x000782D4
		// (remove) Token: 0x060020A9 RID: 8361 RVA: 0x0007A0E0 File Offset: 0x000782E0
		[EditorBrowsable(0)]
		[Browsable(true)]
		public new event EventHandler Click
		{
			add
			{
				base.Click += value;
			}
			remove
			{
				base.Click -= value;
			}
		}

		/// <summary>Occurs when a visual aspect of an owner-drawn <see cref="T:System.Windows.Forms.ListBox" /> changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001FC RID: 508
		// (add) Token: 0x060020AA RID: 8362 RVA: 0x0007A0EC File Offset: 0x000782EC
		// (remove) Token: 0x060020AB RID: 8363 RVA: 0x0007A100 File Offset: 0x00078300
		public event DrawItemEventHandler DrawItem
		{
			add
			{
				base.Events.AddHandler(ListBox.DrawItemEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListBox.DrawItemEvent, value);
			}
		}

		/// <summary>Occurs when an owner-drawn <see cref="T:System.Windows.Forms.ListBox" /> is created and the sizes of the list items are determined.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001FD RID: 509
		// (add) Token: 0x060020AC RID: 8364 RVA: 0x0007A114 File Offset: 0x00078314
		// (remove) Token: 0x060020AD RID: 8365 RVA: 0x0007A128 File Offset: 0x00078328
		public event MeasureItemEventHandler MeasureItem
		{
			add
			{
				base.Events.AddHandler(ListBox.MeasureItemEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListBox.MeasureItemEvent, value);
			}
		}

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.ListBox" /> control with the mouse pointer.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001FE RID: 510
		// (add) Token: 0x060020AE RID: 8366 RVA: 0x0007A13C File Offset: 0x0007833C
		// (remove) Token: 0x060020AF RID: 8367 RVA: 0x0007A148 File Offset: 0x00078348
		[Browsable(true)]
		[EditorBrowsable(0)]
		public new event MouseEventHandler MouseClick
		{
			add
			{
				base.MouseClick += value;
			}
			remove
			{
				base.MouseClick -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ListBox.Padding" /> property changes.</summary>
		// Token: 0x140001FF RID: 511
		// (add) Token: 0x060020B0 RID: 8368 RVA: 0x0007A154 File Offset: 0x00078354
		// (remove) Token: 0x060020B1 RID: 8369 RVA: 0x0007A160 File Offset: 0x00078360
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ListBox" /> control is painted.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000200 RID: 512
		// (add) Token: 0x060020B2 RID: 8370 RVA: 0x0007A16C File Offset: 0x0007836C
		// (remove) Token: 0x060020B3 RID: 8371 RVA: 0x0007A178 File Offset: 0x00078378
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event PaintEventHandler Paint
		{
			add
			{
				base.Paint += value;
			}
			remove
			{
				base.Paint -= value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListBox.SelectedIndex" /> property or the <see cref="P:System.Windows.Forms.ListBox.SelectedIndices" /> collection has changed. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000201 RID: 513
		// (add) Token: 0x060020B4 RID: 8372 RVA: 0x0007A184 File Offset: 0x00078384
		// (remove) Token: 0x060020B5 RID: 8373 RVA: 0x0007A198 File Offset: 0x00078398
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(ListBox.SelectedIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListBox.SelectedIndexChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ListBox.Text" /> property is changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000202 RID: 514
		// (add) Token: 0x060020B6 RID: 8374 RVA: 0x0007A1AC File Offset: 0x000783AC
		// (remove) Token: 0x060020B7 RID: 8375 RVA: 0x0007A1B8 File Offset: 0x000783B8
		[Browsable(false)]
		[EditorBrowsable(2)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		// Token: 0x14000203 RID: 515
		// (add) Token: 0x060020B8 RID: 8376 RVA: 0x0007A1C4 File Offset: 0x000783C4
		// (remove) Token: 0x060020B9 RID: 8377 RVA: 0x0007A1D8 File Offset: 0x000783D8
		internal event EventHandler UIASelectionModeChanged
		{
			add
			{
				base.Events.AddHandler(ListBox.UIASelectionModeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListBox.UIASelectionModeChangedEvent, value);
			}
		}

		// Token: 0x14000204 RID: 516
		// (add) Token: 0x060020BA RID: 8378 RVA: 0x0007A1EC File Offset: 0x000783EC
		// (remove) Token: 0x060020BB RID: 8379 RVA: 0x0007A200 File Offset: 0x00078400
		internal event EventHandler UIAFocusedItemChanged
		{
			add
			{
				base.Events.AddHandler(ListBox.UIAFocusedItemChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListBox.UIAFocusedItemChangedEvent, value);
			}
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x0007A214 File Offset: 0x00078414
		internal void OnUIASelectionModeChangedEvent()
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListBox.UIASelectionModeChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x0007A24C File Offset: 0x0007844C
		internal void OnUIAFocusedItemChangedEvent()
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListBox.UIAFocusedItemChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060020BE RID: 8382 RVA: 0x0007A284 File Offset: 0x00078484
		// (set) Token: 0x060020BF RID: 8383 RVA: 0x0007A28C File Offset: 0x0007848C
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				if (base.BackColor == value)
				{
					return;
				}
				base.BackColor = value;
				base.Refresh();
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060020C0 RID: 8384 RVA: 0x0007A2B0 File Offset: 0x000784B0
		// (set) Token: 0x060020C1 RID: 8385 RVA: 0x0007A2B8 File Offset: 0x000784B8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
				base.Refresh();
			}
		}

		/// <summary>Gets or sets the background image layout for a <see cref="T:System.Windows.Forms.ListBox" /> as defined in the <see cref="T:System.Windows.Forms.ImageLayout" /> enumeration.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.ImageLayout" />. The values are Center, None, Stretch, Tile, or Zoom. Center is the default value.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified enumeration value does not exist. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060020C2 RID: 8386 RVA: 0x0007A2C8 File Offset: 0x000784C8
		// (set) Token: 0x060020C3 RID: 8387 RVA: 0x0007A2D0 File Offset: 0x000784D0
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		/// <summary>Gets or sets the type of border that is drawn around the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.BorderStyle.Fixed3D" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060020C4 RID: 8388 RVA: 0x0007A2DC File Offset: 0x000784DC
		// (set) Token: 0x060020C5 RID: 8389 RVA: 0x0007A2E4 File Offset: 0x000784E4
		[DispId(-504)]
		[DefaultValue(BorderStyle.Fixed3D)]
		public BorderStyle BorderStyle
		{
			get
			{
				return base.InternalBorderStyle;
			}
			set
			{
				base.InternalBorderStyle = value;
				this.UpdateListBoxBounds();
			}
		}

		/// <summary>Gets or sets the width of columns in a multicolumn <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>The width, in pixels, of each column in the control. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentException">A value less than zero is assigned to the property. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060020C6 RID: 8390 RVA: 0x0007A2F4 File Offset: 0x000784F4
		// (set) Token: 0x060020C7 RID: 8391 RVA: 0x0007A2FC File Offset: 0x000784FC
		[DefaultValue(0)]
		[Localizable(true)]
		public int ColumnWidth
		{
			get
			{
				return this.column_width;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("A value less than zero is assigned to the property.");
				}
				this.column_width = value;
				if (value == 0)
				{
					this.ColumnWidthInternal = 120;
				}
				else
				{
					this.ColumnWidthInternal = value;
				}
				base.Refresh();
			}
		}

		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060020C8 RID: 8392 RVA: 0x0007A338 File Offset: 0x00078538
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Gets the width of the tabs between the items in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>A collection of integers representing the tab widths.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060020C9 RID: 8393 RVA: 0x0007A340 File Offset: 0x00078540
		[Browsable(false)]
		[DesignerSerializationVisibility(2)]
		public ListBox.IntegerCollection CustomTabOffsets
		{
			get
			{
				return this.custom_tab_offsets;
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060020CA RID: 8394 RVA: 0x0007A348 File Offset: 0x00078548
		protected override Size DefaultSize
		{
			get
			{
				return new Size(120, 96);
			}
		}

		/// <summary>Gets or sets the drawing mode for the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DrawMode" /> values representing the mode for drawing the items of the control. The default is DrawMode.Normal.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a member of the <see cref="T:System.Windows.Forms.DrawMode" /> enumeration. </exception>
		/// <exception cref="T:System.ArgumentException">A multicolumn <see cref="T:System.Windows.Forms.ListBox" /> cannot have a variable-sized height. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x0007A354 File Offset: 0x00078554
		// (set) Token: 0x060020CC RID: 8396 RVA: 0x0007A35C File Offset: 0x0007855C
		[DefaultValue(DrawMode.Normal)]
		[RefreshProperties(2)]
		public virtual DrawMode DrawMode
		{
			get
			{
				return this.draw_mode;
			}
			set
			{
				if (!Enum.IsDefined(typeof(DrawMode), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for DrawMode", value));
				}
				if (value == DrawMode.OwnerDrawVariable && this.multicolumn)
				{
					throw new ArgumentException("Cannot have variable height and multicolumn");
				}
				if (this.draw_mode == value)
				{
					return;
				}
				this.draw_mode = value;
				if (this.draw_mode == DrawMode.OwnerDrawVariable)
				{
					this.item_heights = new Hashtable();
				}
				else
				{
					this.item_heights = null;
				}
				if (base.Parent != null)
				{
					base.Parent.PerformLayout(this, "DrawMode");
				}
				base.Refresh();
			}
		}

		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x0007A410 File Offset: 0x00078610
		// (set) Token: 0x060020CE RID: 8398 RVA: 0x0007A418 File Offset: 0x00078618
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x0007A424 File Offset: 0x00078624
		// (set) Token: 0x060020D0 RID: 8400 RVA: 0x0007A42C File Offset: 0x0007862C
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				if (base.ForeColor == value)
				{
					return;
				}
				base.ForeColor = value;
				base.Refresh();
			}
		}

		/// <summary>Gets or sets the width by which the horizontal scroll bar of a <see cref="T:System.Windows.Forms.ListBox" /> can scroll.</summary>
		/// <returns>The width, in pixels, that the horizontal scroll bar can scroll the control. The default is zero.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x0007A450 File Offset: 0x00078650
		// (set) Token: 0x060020D2 RID: 8402 RVA: 0x0007A458 File Offset: 0x00078658
		[Localizable(true)]
		[DefaultValue(0)]
		public int HorizontalExtent
		{
			get
			{
				return this.horizontal_extent;
			}
			set
			{
				if (this.horizontal_extent == value)
				{
					return;
				}
				this.horizontal_extent = value;
				base.Refresh();
			}
		}

		/// <summary>Gets or sets a value indicating whether a horizontal scroll bar is displayed in the control.</summary>
		/// <returns>true to display a horizontal scroll bar in the control; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x0007A474 File Offset: 0x00078674
		// (set) Token: 0x060020D4 RID: 8404 RVA: 0x0007A47C File Offset: 0x0007867C
		[Localizable(true)]
		[DefaultValue(false)]
		public bool HorizontalScrollbar
		{
			get
			{
				return this.horizontal_scrollbar;
			}
			set
			{
				if (this.horizontal_scrollbar == value)
				{
					return;
				}
				this.horizontal_scrollbar = value;
				this.UpdateScrollBars();
				base.Refresh();
			}
		}

		/// <summary>Gets or sets a value indicating whether the control should resize to avoid showing partial items.</summary>
		/// <returns>true if the control resizes so that it does not display partial items; otherwise, false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x0007A4AC File Offset: 0x000786AC
		// (set) Token: 0x060020D6 RID: 8406 RVA: 0x0007A4B4 File Offset: 0x000786B4
		[DefaultValue(true)]
		[Localizable(true)]
		[RefreshProperties(2)]
		public bool IntegralHeight
		{
			get
			{
				return this.integral_height;
			}
			set
			{
				if (this.integral_height == value)
				{
					return;
				}
				this.integral_height = value;
				this.UpdateListBoxBounds();
			}
		}

		/// <summary>Gets or sets the height of an item in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>The height, in pixels, of an item in the control.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Windows.Forms.ListBox.ItemHeight" /> property was set to less than 0 or more than 255 pixels. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x0007A4D0 File Offset: 0x000786D0
		// (set) Token: 0x060020D8 RID: 8408 RVA: 0x0007A510 File Offset: 0x00078710
		[RefreshProperties(2)]
		[DefaultValue(13)]
		[Localizable(true)]
		public virtual int ItemHeight
		{
			get
			{
				if (this.item_height == -1)
				{
					this.item_height = (int)TextRenderer.MeasureString("The quick brown Fox", this.Font).Height;
				}
				return this.item_height;
			}
			set
			{
				if (value > 255)
				{
					throw new ArgumentOutOfRangeException("The ItemHeight property was set beyond 255 pixels");
				}
				this.explicit_item_height = true;
				if (this.item_height == value)
				{
					return;
				}
				this.item_height = value;
				if (this.IntegralHeight)
				{
					this.UpdateListBoxBounds();
				}
				this.LayoutListBox();
			}
		}

		/// <summary>Gets the items of the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> representing the items in the <see cref="T:System.Windows.Forms.ListBox" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x0007A568 File Offset: 0x00078768
		[MergableProperty(false)]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[DesignerSerializationVisibility(2)]
		public ListBox.ObjectCollection Items
		{
			get
			{
				return this.items;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ListBox" /> supports multiple columns.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ListBox" /> supports multiple columns; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentException">A multicolumn <see cref="T:System.Windows.Forms.ListBox" /> cannot have a variable-sized height. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x0007A570 File Offset: 0x00078770
		// (set) Token: 0x060020DB RID: 8411 RVA: 0x0007A578 File Offset: 0x00078778
		[DefaultValue(false)]
		public bool MultiColumn
		{
			get
			{
				return this.multicolumn;
			}
			set
			{
				if (this.multicolumn == value)
				{
					return;
				}
				if (value && this.DrawMode == DrawMode.OwnerDrawVariable)
				{
					throw new ArgumentException("A multicolumn ListBox cannot have a variable-sized height.");
				}
				this.multicolumn = value;
				this.LayoutListBox();
				base.Invalidate();
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value.</returns>
		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x0007A5B8 File Offset: 0x000787B8
		// (set) Token: 0x060020DD RID: 8413 RVA: 0x0007A5C0 File Offset: 0x000787C0
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new Padding Padding
		{
			get
			{
				return this.padding;
			}
			set
			{
				this.padding = value;
			}
		}

		/// <summary>Gets the combined height of all items in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>The combined height, in pixels, of all items in the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060020DE RID: 8414 RVA: 0x0007A5CC File Offset: 0x000787CC
		[Browsable(false)]
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		public int PreferredHeight
		{
			get
			{
				int num = 0;
				if (this.draw_mode == DrawMode.Normal)
				{
					num = base.FontHeight * this.items.Count;
				}
				else if (this.draw_mode == DrawMode.OwnerDrawFixed)
				{
					num = this.ItemHeight * this.items.Count;
				}
				else if (this.draw_mode == DrawMode.OwnerDrawVariable)
				{
					for (int i = 0; i < this.items.Count; i++)
					{
						num += (int)this.item_heights[this.Items[i]];
					}
				}
				return num;
			}
		}

		/// <summary>Gets or sets a value indicating whether text displayed by the control is displayed from right to left.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x0007A66C File Offset: 0x0007886C
		// (set) Token: 0x060020E0 RID: 8416 RVA: 0x0007A674 File Offset: 0x00078874
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
				if (base.RightToLeft == RightToLeft.Yes)
				{
					this.StringFormat.Alignment = 2;
				}
				else
				{
					this.StringFormat.Alignment = 0;
				}
				base.Refresh();
			}
		}

		/// <summary>Gets or sets a value indicating whether the vertical scroll bar is shown at all times.</summary>
		/// <returns>true if the vertical scroll bar should always be displayed; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060020E1 RID: 8417 RVA: 0x0007A6B8 File Offset: 0x000788B8
		// (set) Token: 0x060020E2 RID: 8418 RVA: 0x0007A6C0 File Offset: 0x000788C0
		[DefaultValue(false)]
		[Localizable(true)]
		public bool ScrollAlwaysVisible
		{
			get
			{
				return this.scroll_always_visible;
			}
			set
			{
				if (this.scroll_always_visible == value)
				{
					return;
				}
				this.scroll_always_visible = value;
				this.UpdateScrollBars();
			}
		}

		/// <summary>Gets or sets the zero-based index of the currently selected item in a <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>A zero-based index of the currently selected item. A value of negative one (-1) is returned if no item is selected.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than -1 or greater than or equal to the item count.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Windows.Forms.ListBox.SelectionMode" /> property is set to None.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060020E3 RID: 8419 RVA: 0x0007A6DC File Offset: 0x000788DC
		// (set) Token: 0x060020E4 RID: 8420 RVA: 0x0007A71C File Offset: 0x0007891C
		[DesignerSerializationVisibility(0)]
		[Bindable(true)]
		[Browsable(false)]
		public override int SelectedIndex
		{
			get
			{
				if (this.selected_indices == null)
				{
					return -1;
				}
				return (this.selected_indices.Count <= 0) ? (-1) : this.selected_indices[0];
			}
			set
			{
				if (value < -1 || value >= this.Items.Count)
				{
					throw new ArgumentOutOfRangeException("Index of out range");
				}
				if (this.SelectionMode == SelectionMode.None)
				{
					throw new ArgumentException("cannot call this method if SelectionMode is SelectionMode.None");
				}
				if (value == -1)
				{
					this.selected_indices.Clear();
				}
				else
				{
					this.selected_indices.Add(value);
				}
			}
		}

		/// <summary>Gets a collection that contains the zero-based indexes of all currently selected items in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" /> containing the indexes of the currently selected items in the control. If no items are currently selected, an empty <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" /> is returned.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x060020E5 RID: 8421 RVA: 0x0007A788 File Offset: 0x00078988
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public ListBox.SelectedIndexCollection SelectedIndices
		{
			get
			{
				return this.selected_indices;
			}
		}

		/// <summary>Gets or sets the currently selected item in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>An object that represents the current selection in the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x0007A790 File Offset: 0x00078990
		// (set) Token: 0x060020E7 RID: 8423 RVA: 0x0007A7BC File Offset: 0x000789BC
		[DesignerSerializationVisibility(0)]
		[Bindable(true)]
		[Browsable(false)]
		public object SelectedItem
		{
			get
			{
				if (this.SelectedItems.Count > 0)
				{
					return this.SelectedItems[0];
				}
				return null;
			}
			set
			{
				if (value != null && !this.Items.Contains(value))
				{
					return;
				}
				this.SelectedIndex = ((value != null) ? this.Items.IndexOf(value) : (-1));
			}
		}

		/// <summary>Gets a collection containing the currently selected items in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListBox.SelectedObjectCollection" /> containing the currently selected items in the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060020E8 RID: 8424 RVA: 0x0007A800 File Offset: 0x00078A00
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public ListBox.SelectedObjectCollection SelectedItems
		{
			get
			{
				return this.selected_items;
			}
		}

		/// <summary>Gets or sets the method in which items are selected in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.SelectionMode" /> values. The default is SelectionMode.One.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.SelectionMode" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x060020E9 RID: 8425 RVA: 0x0007A808 File Offset: 0x00078A08
		// (set) Token: 0x060020EA RID: 8426 RVA: 0x0007A810 File Offset: 0x00078A10
		[DefaultValue(SelectionMode.One)]
		public virtual SelectionMode SelectionMode
		{
			get
			{
				return this.selection_mode;
			}
			set
			{
				if (!Enum.IsDefined(typeof(SelectionMode), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for SelectionMode", value));
				}
				if (this.selection_mode == value)
				{
					return;
				}
				this.selection_mode = value;
				SelectionMode selectionMode = this.selection_mode;
				if (selectionMode != SelectionMode.None)
				{
					if (selectionMode == SelectionMode.One)
					{
						ArrayList arrayList = (ArrayList)this.SelectedIndices.List.Clone();
						for (int i = 1; i < arrayList.Count; i++)
						{
							this.SelectedIndices.Remove((int)arrayList[i]);
						}
					}
				}
				else
				{
					this.SelectedIndices.Clear();
				}
				this.OnUIASelectionModeChangedEvent();
			}
		}

		/// <summary>Gets or sets a value indicating whether the items in the <see cref="T:System.Windows.Forms.ListBox" /> are sorted alphabetically.</summary>
		/// <returns>true if items in the control are sorted; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x060020EB RID: 8427 RVA: 0x0007A8E0 File Offset: 0x00078AE0
		// (set) Token: 0x060020EC RID: 8428 RVA: 0x0007A8E8 File Offset: 0x00078AE8
		[DefaultValue(false)]
		public bool Sorted
		{
			get
			{
				return this.sorted;
			}
			set
			{
				if (this.sorted == value)
				{
					return;
				}
				this.sorted = value;
				if (this.sorted)
				{
					this.Sort();
				}
			}
		}

		/// <summary>Gets or searches for the text of the currently selected item in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>The text of the currently selected item in the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060020ED RID: 8429 RVA: 0x0007A910 File Offset: 0x00078B10
		// (set) Token: 0x060020EE RID: 8430 RVA: 0x0007A948 File Offset: 0x00078B48
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(2)]
		[Bindable(false)]
		[Browsable(false)]
		public override string Text
		{
			get
			{
				if (this.SelectionMode != SelectionMode.None && this.SelectedIndex != -1)
				{
					return base.GetItemText(this.SelectedItem);
				}
				return base.Text;
			}
			set
			{
				base.Text = value;
				if (this.SelectionMode == SelectionMode.None)
				{
					return;
				}
				int num = this.FindStringExact(value);
				if (num == -1)
				{
					return;
				}
				this.SelectedIndex = num;
			}
		}

		/// <summary>Gets or sets the index of the first visible item in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>The zero-based index of the first visible item in the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060020EF RID: 8431 RVA: 0x0007A980 File Offset: 0x00078B80
		// (set) Token: 0x060020F0 RID: 8432 RVA: 0x0007A988 File Offset: 0x00078B88
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public int TopIndex
		{
			get
			{
				return this.top_index;
			}
			set
			{
				if (value == this.top_index)
				{
					return;
				}
				if (value < 0 || value >= this.Items.Count)
				{
					return;
				}
				int num = this.items_area.Height / this.ItemHeight;
				if (this.Items.Count < num)
				{
					value = 0;
				}
				else if (!this.multicolumn)
				{
					this.top_index = Math.Min(value, this.Items.Count - num);
				}
				else
				{
					this.top_index = value;
				}
				this.UpdateTopItem();
				base.Refresh();
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ListBox" /> recognizes and expands tab characters when it draws its strings by using the <see cref="P:System.Windows.Forms.ListBox.CustomTabOffsets" /> integer array.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ListBox" /> recognizes and expands tab characters; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060020F1 RID: 8433 RVA: 0x0007AA24 File Offset: 0x00078C24
		// (set) Token: 0x060020F2 RID: 8434 RVA: 0x0007AA2C File Offset: 0x00078C2C
		[Browsable(false)]
		[DefaultValue(false)]
		public bool UseCustomTabOffsets
		{
			get
			{
				return this.use_custom_tab_offsets;
			}
			set
			{
				if (this.use_custom_tab_offsets != value)
				{
					this.use_custom_tab_offsets = value;
					this.CalculateTabStops();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ListBox" /> can recognize and expand tab characters when drawing its strings.</summary>
		/// <returns>true if the control can expand tab characters; otherwise, false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x0007AA48 File Offset: 0x00078C48
		// (set) Token: 0x060020F4 RID: 8436 RVA: 0x0007AA50 File Offset: 0x00078C50
		[DefaultValue(true)]
		public bool UseTabStops
		{
			get
			{
				return this.use_tabstops;
			}
			set
			{
				if (this.use_tabstops == value)
				{
					return;
				}
				this.use_tabstops = value;
				this.CalculateTabStops();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ListBox" /> currently enables selection of list items.</summary>
		/// <returns>true if <see cref="T:System.Windows.Forms.SelectionMode" /> is not <see cref="F:System.Windows.Forms.SelectionMode.None" />; otherwise, false.</returns>
		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x0007AA6C File Offset: 0x00078C6C
		protected override bool AllowSelection
		{
			get
			{
				return this.SelectionMode != SelectionMode.None;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x060020F6 RID: 8438 RVA: 0x0007AA7C File Offset: 0x00078C7C
		// (set) Token: 0x060020F7 RID: 8439 RVA: 0x0007AA84 File Offset: 0x00078C84
		private int ColumnWidthInternal
		{
			get
			{
				return this.column_width_internal;
			}
			set
			{
				this.column_width_internal = value;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x060020F8 RID: 8440 RVA: 0x0007AA90 File Offset: 0x00078C90
		private int RowCount
		{
			get
			{
				return (!this.MultiColumn) ? this.Items.Count : this.row_count;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x0007AAB4 File Offset: 0x00078CB4
		internal ScrollBar UIAHScrollBar
		{
			get
			{
				return this.hscrollbar;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x060020FA RID: 8442 RVA: 0x0007AABC File Offset: 0x00078CBC
		internal ScrollBar UIAVScrollBar
		{
			get
			{
				return this.vscrollbar;
			}
		}

		/// <summary>This member is obsolete, and there is no replacement.</summary>
		/// <param name="value">An array of objects.</param>
		// Token: 0x060020FB RID: 8443 RVA: 0x0007AAC4 File Offset: 0x00078CC4
		[Obsolete("this method has been deprecated")]
		protected virtual void AddItemsCore(object[] value)
		{
			this.Items.AddRange(value);
		}

		/// <summary>Maintains performance while items are added to the <see cref="T:System.Windows.Forms.ListBox" /> one at a time by preventing the control from drawing until the <see cref="M:System.Windows.Forms.ListBox.EndUpdate" /> method is called.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060020FC RID: 8444 RVA: 0x0007AAD4 File Offset: 0x00078CD4
		public void BeginUpdate()
		{
			this.suspend_layout = true;
		}

		/// <summary>Unselects all items in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060020FD RID: 8445 RVA: 0x0007AAE0 File Offset: 0x00078CE0
		public void ClearSelected()
		{
			this.selected_indices.Clear();
		}

		/// <summary>Creates a new instance of the item collection.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> that represents the new item collection.</returns>
		// Token: 0x060020FE RID: 8446 RVA: 0x0007AAF0 File Offset: 0x00078CF0
		protected virtual ListBox.ObjectCollection CreateItemCollection()
		{
			return new ListBox.ObjectCollection(this);
		}

		/// <summary>Resumes painting the <see cref="T:System.Windows.Forms.ListBox" /> control after painting is suspended by the <see cref="M:System.Windows.Forms.ListBox.BeginUpdate" /> method.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060020FF RID: 8447 RVA: 0x0007AAF8 File Offset: 0x00078CF8
		public void EndUpdate()
		{
			this.suspend_layout = false;
			this.LayoutListBox();
			base.Refresh();
		}

		/// <summary>Finds the first item in the <see cref="T:System.Windows.Forms.ListBox" /> that starts with the specified string.</summary>
		/// <returns>The zero-based index of the first item found; returns ListBox.NoMatches if no match is found.</returns>
		/// <param name="s">The text to search for. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="s" /> parameter is less than -1 or greater than or equal to the item count.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002100 RID: 8448 RVA: 0x0007AB10 File Offset: 0x00078D10
		public int FindString(string s)
		{
			return this.FindString(s, -1);
		}

		/// <summary>Finds the first item in the <see cref="T:System.Windows.Forms.ListBox" /> that starts with the specified string. The search starts at a specific starting index.</summary>
		/// <returns>The zero-based index of the first item found; returns ListBox.NoMatches if no match is found.</returns>
		/// <param name="s">The text to search for. </param>
		/// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to negative one (-1) to search from the beginning of the control. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="startIndex" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListBox.ObjectCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> class. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002101 RID: 8449 RVA: 0x0007AB1C File Offset: 0x00078D1C
		public int FindString(string s, int startIndex)
		{
			if (this.Items.Count == 0)
			{
				return -1;
			}
			if (startIndex < -1 || startIndex >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("Index of out range");
			}
			startIndex = ((startIndex != this.Items.Count - 1) ? (startIndex + 1) : 0);
			int num = startIndex;
			for (;;)
			{
				string itemText = base.GetItemText(this.Items[num]);
				if (CultureInfo.CurrentCulture.CompareInfo.IsPrefix(itemText, s, 1))
				{
					break;
				}
				num = ((num != this.Items.Count - 1) ? (num + 1) : 0);
				if (num == startIndex)
				{
					return -1;
				}
			}
			return num;
		}

		/// <summary>Finds the first item in the <see cref="T:System.Windows.Forms.ListBox" /> that exactly matches the specified string.</summary>
		/// <returns>The zero-based index of the first item found; returns ListBox.NoMatches if no match is found.</returns>
		/// <param name="s">The text to search for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002102 RID: 8450 RVA: 0x0007ABDC File Offset: 0x00078DDC
		public int FindStringExact(string s)
		{
			return this.FindStringExact(s, -1);
		}

		/// <summary>Finds the first item in the <see cref="T:System.Windows.Forms.ListBox" /> that exactly matches the specified string. The search starts at a specific starting index.</summary>
		/// <returns>The zero-based index of the first item found; returns ListBox.NoMatches if no match is found.</returns>
		/// <param name="s">The text to search for. </param>
		/// <param name="startIndex">The zero-based index of the item before the first item to be searched. Set to negative one (-1) to search from the beginning of the control. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="startIndex" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListBox.ObjectCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> class. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002103 RID: 8451 RVA: 0x0007ABE8 File Offset: 0x00078DE8
		public int FindStringExact(string s, int startIndex)
		{
			if (this.Items.Count == 0)
			{
				return -1;
			}
			if (startIndex < -1 || startIndex >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("Index of out range");
			}
			startIndex = ((startIndex + 1 != this.Items.Count) ? (startIndex + 1) : 0);
			int num = startIndex;
			while (string.Compare(base.GetItemText(this.Items[num]), s, true) != 0)
			{
				num = ((num + 1 != this.Items.Count) ? (num + 1) : 0);
				if (num == startIndex)
				{
					return -1;
				}
			}
			return num;
		}

		/// <summary>Returns the height of an item in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>The height, in pixels, of the specified item.</returns>
		/// <param name="index">The zero-based index of the item to return the height for. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value of the <paramref name="index" /> parameter is less than zero or greater than the item count. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002104 RID: 8452 RVA: 0x0007AC9C File Offset: 0x00078E9C
		public int GetItemHeight(int index)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("Index of out range");
			}
			if (this.DrawMode != DrawMode.OwnerDrawVariable || !base.IsHandleCreated)
			{
				return this.ItemHeight;
			}
			object obj = this.Items[index];
			if (this.item_heights.Contains(obj))
			{
				return (int)this.item_heights[obj];
			}
			MeasureItemEventArgs measureItemEventArgs = new MeasureItemEventArgs(base.DeviceContext, index, this.ItemHeight);
			this.OnMeasureItem(measureItemEventArgs);
			this.item_heights[obj] = measureItemEventArgs.ItemHeight;
			return measureItemEventArgs.ItemHeight;
		}

		/// <summary>Returns the bounding rectangle for an item in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding rectangle for the specified item.</returns>
		/// <param name="index">The zero-based index of item whose bounding rectangle you want to return. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListBox.ObjectCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> class. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002105 RID: 8453 RVA: 0x0007AD54 File Offset: 0x00078F54
		public Rectangle GetItemRectangle(int index)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("GetItemRectangle index out of range.");
			}
			Rectangle rectangle = default(Rectangle);
			if (this.MultiColumn)
			{
				int num = index / this.RowCount;
				int num2 = index;
				if (num2 < 0)
				{
					num2 += this.RowCount * (this.top_index / this.RowCount);
				}
				rectangle.Y = num2 % this.RowCount * this.ItemHeight;
				rectangle.X = (num - this.top_index / this.RowCount) * this.ColumnWidthInternal;
				rectangle.Height = this.ItemHeight;
				rectangle.Width = this.ColumnWidthInternal;
			}
			else
			{
				rectangle.X = 0;
				rectangle.Height = this.GetItemHeight(index);
				rectangle.Width = this.items_area.Width;
				if (this.DrawMode == DrawMode.OwnerDrawVariable)
				{
					rectangle.Y = 0;
					if (index >= this.top_index)
					{
						for (int i = this.top_index; i < index; i++)
						{
							rectangle.Y += this.GetItemHeight(i);
						}
					}
					else
					{
						for (int j = index; j < this.top_index; j++)
						{
							rectangle.Y -= this.GetItemHeight(j);
						}
					}
				}
				else
				{
					rectangle.Y = this.ItemHeight * (index - this.top_index);
				}
			}
			if (this is CheckedListBox)
			{
				rectangle.Width += 15;
			}
			return rectangle;
		}

		/// <summary>Retrieves the bounds within which the <see cref="T:System.Windows.Forms.ListBox" /> is scaled.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the bounds within which the control is scaled.</returns>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area for which to retrieve the display bounds.</param>
		/// <param name="factor">The height and width of the control's bounds.</param>
		/// <param name="specified">One of the values of <see cref="T:System.Windows.Forms.BoundsSpecified" /> that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x06002106 RID: 8454 RVA: 0x0007AEF4 File Offset: 0x000790F4
		[EditorBrowsable(2)]
		protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			bounds.Height = this.requested_height;
			return base.GetScaledBounds(bounds, factor, specified);
		}

		/// <summary>Returns a value indicating whether the specified item is selected.</summary>
		/// <returns>true if the specified item is currently selected in the <see cref="T:System.Windows.Forms.ListBox" />; otherwise, false.</returns>
		/// <param name="index">The zero-based index of the item that determines whether it is selected. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListBox.ObjectCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> class. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002107 RID: 8455 RVA: 0x0007AF0C File Offset: 0x0007910C
		public bool GetSelected(int index)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("Index of out range");
			}
			return this.SelectedIndices.Contains(index);
		}

		/// <summary>Returns the zero-based index of the item at the specified coordinates.</summary>
		/// <returns>The zero-based index of the item found at the specified coordinates; returns ListBox.NoMatches if no match is found.</returns>
		/// <param name="p">A <see cref="T:System.Drawing.Point" /> object containing the coordinates used to obtain the item index. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002108 RID: 8456 RVA: 0x0007AF48 File Offset: 0x00079148
		public int IndexFromPoint(Point p)
		{
			return this.IndexFromPoint(p.X, p.Y);
		}

		/// <summary>Returns the zero-based index of the item at the specified coordinates.</summary>
		/// <returns>The zero-based index of the item found at the specified coordinates; returns ListBox.NoMatches if no match is found.</returns>
		/// <param name="x">The x-coordinate of the location to search. </param>
		/// <param name="y">The y-coordinate of the location to search. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002109 RID: 8457 RVA: 0x0007AF60 File Offset: 0x00079160
		public int IndexFromPoint(int x, int y)
		{
			if (this.Items.Count == 0)
			{
				return -1;
			}
			for (int i = this.top_index; i <= this.last_visible_index; i++)
			{
				if (this.GetItemRectangle(i).Contains(x, y))
				{
					return i;
				}
			}
			return -1;
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.UICuesEventArgs" /> that contains the event data. </param>
		// Token: 0x0600210A RID: 8458 RVA: 0x0007AFB4 File Offset: 0x000791B4
		protected override void OnChangeUICues(UICuesEventArgs e)
		{
			base.OnChangeUICues(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600210B RID: 8459 RVA: 0x0007AFC0 File Offset: 0x000791C0
		protected override void OnDataSourceChanged(EventArgs e)
		{
			base.OnDataSourceChanged(e);
			base.BindDataItems();
			if (base.DataSource == null || base.DataManager == null)
			{
				this.SelectedIndex = -1;
			}
			else
			{
				this.SelectedIndex = base.DataManager.Position;
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600210C RID: 8460 RVA: 0x0007B010 File Offset: 0x00079210
		protected override void OnDisplayMemberChanged(EventArgs e)
		{
			base.OnDisplayMemberChanged(e);
			if (base.DataManager == null || !base.IsHandleCreated)
			{
				return;
			}
			base.BindDataItems();
			base.Refresh();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListBox.DrawItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> that contains the event data. </param>
		// Token: 0x0600210D RID: 8461 RVA: 0x0007B048 File Offset: 0x00079248
		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{
			DrawMode drawMode = this.DrawMode;
			if (drawMode != DrawMode.OwnerDrawFixed && drawMode != DrawMode.OwnerDrawVariable)
			{
				ThemeEngine.Current.DrawListBoxItem(this, e);
			}
			else
			{
				DrawItemEventHandler drawItemEventHandler = (DrawItemEventHandler)base.Events[ListBox.DrawItemEvent];
				if (drawItemEventHandler != null)
				{
					drawItemEventHandler(this, e);
				}
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600210E RID: 8462 RVA: 0x0007B0AC File Offset: 0x000792AC
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			if (this.use_tabstops)
			{
				this.StringFormat.SetTabStops(0f, new float[] { (float)((double)this.Font.Height * 3.7) });
			}
			if (this.explicit_item_height)
			{
				base.Refresh();
			}
			else
			{
				this.item_height = (int)TextRenderer.MeasureString("The quick brown Fox", this.Font).Height;
				if (this.IntegralHeight)
				{
					this.UpdateListBoxBounds();
				}
				this.LayoutListBox();
			}
		}

		/// <summary>Specifies when the window handle has been created so that column width and other characteristics can be set. Inheriting classes should call base.OnHandleCreated.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600210F RID: 8463 RVA: 0x0007B148 File Offset: 0x00079348
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (this.IntegralHeight)
			{
				this.UpdateListBoxBounds();
			}
			this.LayoutListBox();
			this.EnsureVisible(this.focused_item);
		}

		/// <summary>Overridden to be sure that items are set up and cleared out correctly. Inheriting controls should call base.OnHandleDestroyed.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002110 RID: 8464 RVA: 0x0007B180 File Offset: 0x00079380
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ListBox.MeasureItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> that contains the event data. </param>
		// Token: 0x06002111 RID: 8465 RVA: 0x0007B18C File Offset: 0x0007938C
		protected virtual void OnMeasureItem(MeasureItemEventArgs e)
		{
			if (this.draw_mode != DrawMode.OwnerDrawVariable)
			{
				return;
			}
			MeasureItemEventHandler measureItemEventHandler = (MeasureItemEventHandler)base.Events[ListBox.MeasureItemEvent];
			if (measureItemEventHandler != null)
			{
				measureItemEventHandler(this, e);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002112 RID: 8466 RVA: 0x0007B1CC File Offset: 0x000793CC
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002113 RID: 8467 RVA: 0x0007B1D8 File Offset: 0x000793D8
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (this.canvas_size.IsEmpty || this.MultiColumn)
			{
				this.LayoutListBox();
			}
			base.Invalidate();
		}

		/// <param name="e">Event object with the details </param>
		// Token: 0x06002114 RID: 8468 RVA: 0x0007B214 File Offset: 0x00079414
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			EventHandler eventHandler = (EventHandler)base.Events[ListBox.SelectedIndexChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002115 RID: 8469 RVA: 0x0007B24C File Offset: 0x0007944C
		protected override void OnSelectedValueChanged(EventArgs e)
		{
			base.OnSelectedValueChanged(e);
		}

		/// <summary>Forces the control to invalidate its client area and immediately redraw itself and any child controls.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002116 RID: 8470 RVA: 0x0007B258 File Offset: 0x00079458
		public override void Refresh()
		{
			if (this.draw_mode == DrawMode.OwnerDrawVariable)
			{
				this.item_heights.Clear();
			}
			base.Refresh();
		}

		/// <summary>Refreshes the item contained at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to refresh.</param>
		// Token: 0x06002117 RID: 8471 RVA: 0x0007B278 File Offset: 0x00079478
		protected override void RefreshItem(int index)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("Index of out range");
			}
			if (this.draw_mode == DrawMode.OwnerDrawVariable)
			{
				this.item_heights.Remove(this.Items[index]);
			}
		}

		/// <summary>Refreshes all <see cref="T:System.Windows.Forms.ListBox" /> items and retrieves new strings for them.</summary>
		// Token: 0x06002118 RID: 8472 RVA: 0x0007B2CC File Offset: 0x000794CC
		protected override void RefreshItems()
		{
			for (int i = 0; i < this.Items.Count; i++)
			{
				this.RefreshItem(i);
			}
			this.LayoutListBox();
			this.Refresh();
		}

		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002119 RID: 8473 RVA: 0x0007B308 File Offset: 0x00079508
		public override void ResetBackColor()
		{
			base.ResetBackColor();
		}

		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600211A RID: 8474 RVA: 0x0007B310 File Offset: 0x00079510
		public override void ResetForeColor()
		{
			base.ResetForeColor();
		}

		/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
		/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x0600211B RID: 8475 RVA: 0x0007B318 File Offset: 0x00079518
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			base.ScaleControl(factor, specified);
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x0007B324 File Offset: 0x00079524
		private int SnapHeightToIntegral(int height)
		{
			int num;
			switch (this.border_style)
			{
			case BorderStyle.FixedSingle:
				num = ThemeEngine.Current.BorderSize.Height;
				goto IL_0055;
			case BorderStyle.Fixed3D:
				num = ThemeEngine.Current.Border3DSize.Height;
				goto IL_0055;
			}
			num = 0;
			IL_0055:
			height -= 2 * num;
			height -= height % this.ItemHeight;
			height += 2 * num;
			return height;
		}

		/// <summary>Sets the specified bounds of the <see cref="T:System.Windows.Forms.ListBox" /> control.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x0600211D RID: 8477 RVA: 0x0007B3A4 File Offset: 0x000795A4
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
			{
				this.requested_height = height;
			}
			if (this.IntegralHeight && base.IsHandleCreated)
			{
				height = this.SnapHeightToIntegral(height);
			}
			base.SetBoundsCore(x, y, width, height, specified);
			this.UpdateScrollBars();
			this.last_visible_index = this.LastVisibleItem();
		}

		/// <summary>Sets the object with the specified index in the derived class.</summary>
		/// <param name="index">The array index of the object.</param>
		/// <param name="value">The object.</param>
		// Token: 0x0600211E RID: 8478 RVA: 0x0007B404 File Offset: 0x00079604
		protected override void SetItemCore(int index, object value)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				return;
			}
			this.Items[index] = value;
		}

		/// <summary>Clears the contents of the <see cref="T:System.Windows.Forms.ListBox" /> and adds the specified items to the control.</summary>
		/// <param name="value">An array of objects to insert into the control. </param>
		// Token: 0x0600211F RID: 8479 RVA: 0x0007B438 File Offset: 0x00079638
		protected override void SetItemsCore(IList value)
		{
			this.BeginUpdate();
			try
			{
				this.Items.Clear();
				this.Items.AddItems(value);
			}
			finally
			{
				this.EndUpdate();
			}
		}

		/// <summary>Selects or clears the selection for the specified item in a <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <param name="index">The zero-based index of the item in a <see cref="T:System.Windows.Forms.ListBox" /> to select or clear the selection for. </param>
		/// <param name="value">true to select the specified item; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index was outside the range of valid values. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.ListBox.SelectionMode" /> property was set to None.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002120 RID: 8480 RVA: 0x0007B48C File Offset: 0x0007968C
		public void SetSelected(int index, bool value)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("Index of out range");
			}
			if (this.SelectionMode == SelectionMode.None)
			{
				throw new InvalidOperationException();
			}
			if (value)
			{
				this.SelectedIndices.Add(index);
			}
			else
			{
				this.SelectedIndices.Remove(index);
			}
		}

		/// <summary>Sorts the items in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		// Token: 0x06002121 RID: 8481 RVA: 0x0007B4F0 File Offset: 0x000796F0
		protected virtual void Sort()
		{
			this.Sort(true);
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x0007B4FC File Offset: 0x000796FC
		private void Sort(bool paint)
		{
			if (this.Items.Count == 0)
			{
				return;
			}
			this.Items.Sort();
			if (paint)
			{
				base.Refresh();
			}
		}

		/// <summary>Returns a string representation of the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <returns>A string that states the control type, the count of items in the <see cref="T:System.Windows.Forms.ListBox" /> control, and the Text property of the first item in the <see cref="T:System.Windows.Forms.ListBox" />, if the count is not 0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002123 RID: 8483 RVA: 0x0007B534 File Offset: 0x00079734
		public override string ToString()
		{
			return base.ToString();
		}

		/// <summary>Processes the command message the <see cref="T:System.Windows.Forms.ListView" /> control receives from the top-level window.</summary>
		/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> the top-level window sent to the <see cref="T:System.Windows.Forms.ListBox" /> control.</param>
		// Token: 0x06002124 RID: 8484 RVA: 0x0007B53C File Offset: 0x0007973C
		protected virtual void WmReflectCommand(ref Message m)
		{
		}

		/// <summary>The list's window procedure. </summary>
		/// <param name="m">A Windows Message Object. </param>
		// Token: 0x06002125 RID: 8485 RVA: 0x0007B540 File Offset: 0x00079740
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 256)
			{
				if (this.ProcessKeyMessage(ref m))
				{
					m.Result = IntPtr.Zero;
				}
				else
				{
					this.HandleKeyDown((Keys)m.WParam.ToInt32());
					this.DefWndProc(ref m);
				}
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x0007B59C File Offset: 0x0007979C
		private void CalculateTabStops()
		{
			if (this.use_tabstops)
			{
				if (this.use_custom_tab_offsets)
				{
					float[] array = new float[this.custom_tab_offsets.Count];
					this.custom_tab_offsets.CopyTo(array, 0);
					this.StringFormat.SetTabStops(0f, array);
				}
				else
				{
					this.StringFormat.SetTabStops(0f, new float[] { (float)((double)this.Font.Height * 3.7) });
				}
			}
			else
			{
				this.StringFormat.SetTabStops(0f, new float[0]);
			}
			base.Invalidate();
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x0007B644 File Offset: 0x00079844
		private void LayoutListBox()
		{
			if (!base.IsHandleCreated || this.suspend_layout)
			{
				return;
			}
			if (this.MultiColumn)
			{
				this.LayoutMultiColumn();
			}
			else
			{
				this.LayoutSingleColumn();
			}
			this.last_visible_index = this.LastVisibleItem();
			this.UpdateScrollBars();
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x0007B698 File Offset: 0x00079898
		private void LayoutSingleColumn()
		{
			int num;
			int num2;
			switch (this.DrawMode)
			{
			case DrawMode.OwnerDrawFixed:
				num = this.Items.Count * this.ItemHeight;
				num2 = this.HorizontalExtent;
				goto IL_00EF;
			case DrawMode.OwnerDrawVariable:
			{
				num = 0;
				num2 = this.HorizontalExtent;
				for (int i = 0; i < this.Items.Count; i++)
				{
					num += this.GetItemHeight(i);
				}
				goto IL_00EF;
			}
			}
			num = this.Items.Count * this.ItemHeight;
			num2 = 0;
			for (int j = 0; j < this.Items.Count; j++)
			{
				int num3 = (int)TextRenderer.MeasureString(base.GetItemText(this.Items[j]), this.Font).Width;
				if (this is CheckedListBox)
				{
					num3 += 15;
				}
				if (num3 > num2)
				{
					num2 = num3;
				}
			}
			IL_00EF:
			this.canvas_size = new Size(num2, num);
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x0007B7A4 File Offset: 0x000799A4
		private void LayoutMultiColumn()
		{
			int num = base.ClientRectangle.Height - ((!this.ScrollAlwaysVisible) ? 0 : this.hscrollbar.Height);
			this.row_count = Math.Max(1, num / this.ItemHeight);
			int num2 = (int)Math.Ceiling((double)((float)this.Items.Count / (float)this.row_count));
			Size size;
			size..ctor(num2 * this.ColumnWidthInternal, this.row_count * this.ItemHeight);
			if (!this.ScrollAlwaysVisible && size.Width > base.ClientRectangle.Width && this.row_count > 1)
			{
				num = base.ClientRectangle.Height - this.hscrollbar.Height;
				this.row_count = Math.Max(1, num / this.ItemHeight);
				num2 = (int)Math.Ceiling((double)((float)this.Items.Count / (float)this.row_count));
				size..ctor(num2 * this.ColumnWidthInternal, this.row_count * this.ItemHeight);
			}
			this.canvas_size = size;
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x0007B8CC File Offset: 0x00079ACC
		internal void Draw(Rectangle clip, Graphics dc)
		{
			Theme theme = ThemeEngine.Current;
			if (this.hscrollbar.Visible && this.vscrollbar.Visible)
			{
				Rectangle rectangle;
				rectangle..ctor(this.hscrollbar.Right, this.vscrollbar.Bottom, this.vscrollbar.Width, this.hscrollbar.Height);
				if (rectangle.IntersectsWith(clip))
				{
					dc.FillRectangle(theme.ResPool.GetSolidBrush(theme.ColorControl), rectangle);
				}
			}
			dc.FillRectangle(theme.ResPool.GetSolidBrush(this.BackColor), this.items_area);
			if (this.Items.Count == 0)
			{
				return;
			}
			for (int i = this.top_index; i <= this.last_visible_index; i++)
			{
				Rectangle itemDisplayRectangle = this.GetItemDisplayRectangle(i, this.top_index);
				if (clip.IntersectsWith(itemDisplayRectangle))
				{
					DrawItemState drawItemState = DrawItemState.None;
					if (this.SelectedIndices.Contains(i))
					{
						drawItemState |= DrawItemState.Selected;
					}
					if (this.has_focus && this.FocusedItem == i)
					{
						drawItemState |= DrawItemState.Focus;
					}
					if (!this.MultiColumn && this.hscrollbar != null && this.hscrollbar.Visible)
					{
						itemDisplayRectangle.X -= this.hscrollbar.Value;
						itemDisplayRectangle.Width += this.hscrollbar.Value;
					}
					Color color = (((drawItemState & DrawItemState.Selected) == DrawItemState.None) ? this.ForeColor : ThemeEngine.Current.ColorHighlightText);
					this.OnDrawItem(new DrawItemEventArgs(dc, this.Font, itemDisplayRectangle, i, drawItemState, color, this.BackColor));
				}
			}
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x0007BA90 File Offset: 0x00079C90
		internal Rectangle GetItemDisplayRectangle(int index, int first_displayble)
		{
			Rectangle itemRectangle = this.GetItemRectangle(first_displayble);
			Rectangle itemRectangle2 = this.GetItemRectangle(index);
			itemRectangle2.X -= itemRectangle.X;
			itemRectangle2.Y -= itemRectangle.Y;
			if (this is CheckedListBox)
			{
				itemRectangle2.Width -= 14;
			}
			return itemRectangle2;
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x0007BAF4 File Offset: 0x00079CF4
		private void HorizontalScrollEvent(object sender, EventArgs e)
		{
			if (this.multicolumn)
			{
				int num = this.top_index;
				int num2 = this.last_visible_index;
				this.top_index = this.RowCount * this.hscrollbar.Value;
				this.last_visible_index = this.LastVisibleItem();
				if (num != this.top_index || num2 != this.last_visible_index)
				{
					base.Invalidate(this.items_area);
				}
			}
			else
			{
				int num3 = this.hbar_offset;
				this.hbar_offset = this.hscrollbar.Value;
				if (this.hbar_offset < 0)
				{
					this.hbar_offset = 0;
				}
				if (base.IsHandleCreated)
				{
					XplatUI.ScrollWindow(this.Handle, this.items_area, num3 - this.hbar_offset, 0, false);
				}
			}
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x0007BBB8 File Offset: 0x00079DB8
		private int IndexAtClientPoint(int x, int y)
		{
			if (this.Items.Count == 0)
			{
				return -1;
			}
			if (x < 0)
			{
				x = 0;
			}
			else if (x > base.ClientRectangle.Right)
			{
				x = base.ClientRectangle.Right;
			}
			if (y < 0)
			{
				y = 0;
			}
			else if (y > base.ClientRectangle.Bottom)
			{
				y = base.ClientRectangle.Bottom;
			}
			for (int i = this.top_index; i <= this.last_visible_index; i++)
			{
				if (this.GetItemDisplayRectangle(i, this.top_index).Contains(x, y))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x0007BC7C File Offset: 0x00079E7C
		internal override bool IsInputCharInternal(char charCode)
		{
			return true;
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x0007BC80 File Offset: 0x00079E80
		private int LastVisibleItem()
		{
			int num = this.items_area.Y + this.items_area.Height;
			if (this.top_index >= this.Items.Count)
			{
				return this.top_index;
			}
			int i;
			for (i = this.top_index; i < this.Items.Count; i++)
			{
				Rectangle itemDisplayRectangle = this.GetItemDisplayRectangle(i, this.top_index);
				if (this.MultiColumn)
				{
					if (itemDisplayRectangle.X > this.items_area.Width)
					{
						return i - 1;
					}
				}
				else if (itemDisplayRectangle.Y + itemDisplayRectangle.Height > num)
				{
					return i;
				}
			}
			return i - 1;
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x0007BD38 File Offset: 0x00079F38
		private void UpdateTopItem()
		{
			if (this.MultiColumn)
			{
				int num = this.top_index / this.RowCount;
				if (num > this.hscrollbar.Maximum)
				{
					this.hscrollbar.Value = this.hscrollbar.Maximum;
				}
				else
				{
					this.hscrollbar.Value = num;
				}
			}
			else
			{
				if (this.top_index > this.vscrollbar.Maximum)
				{
					this.vscrollbar.Value = this.vscrollbar.Maximum;
				}
				else
				{
					this.vscrollbar.Value = this.top_index;
				}
				this.Scroll(this.vscrollbar, this.vscrollbar.Value - this.top_index);
			}
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x0007BDFC File Offset: 0x00079FFC
		private int NavigateItemVisually(ListBox.ItemNavigation navigation)
		{
			int num = -1;
			int num3;
			if (this.multicolumn)
			{
				int num2 = this.items_area.Width / this.ColumnWidthInternal;
				num3 = num2 * this.RowCount;
				if (num3 == 0)
				{
					num3 = this.RowCount;
				}
			}
			else
			{
				num3 = this.items_area.Height / this.ItemHeight;
			}
			switch (navigation)
			{
			case ListBox.ItemNavigation.First:
				this.top_index = 0;
				num = 0;
				this.UpdateTopItem();
				break;
			case ListBox.ItemNavigation.Last:
			{
				int num4 = this.items_area.Height / this.ItemHeight;
				if (this.multicolumn)
				{
					num = this.Items.Count - 1;
				}
				else if (this.Items.Count < num4)
				{
					this.top_index = 0;
					num = this.Items.Count - 1;
					this.UpdateTopItem();
				}
				else
				{
					this.top_index = this.Items.Count - num4;
					num = this.Items.Count - 1;
					this.UpdateTopItem();
				}
				break;
			}
			case ListBox.ItemNavigation.Next:
				if (this.FocusedItem == this.Items.Count - 1)
				{
					return -1;
				}
				if (this.multicolumn)
				{
					num = this.FocusedItem + 1;
				}
				else
				{
					int num5 = 0;
					ArrayList arrayList = new ArrayList();
					if (this.draw_mode == DrawMode.OwnerDrawVariable)
					{
						for (int i = this.top_index; i <= this.FocusedItem + 1; i++)
						{
							int itemHeight = this.GetItemHeight(i);
							num5 += itemHeight;
							arrayList.Add(itemHeight);
						}
					}
					else
					{
						num5 = (this.FocusedItem + 1 - this.top_index + 1) * this.ItemHeight;
					}
					if (num5 >= this.items_area.Height)
					{
						int j = num5 - this.items_area.Height;
						int num6 = 0;
						if (this.draw_mode == DrawMode.OwnerDrawVariable)
						{
							while (j > 0)
							{
								j -= (int)arrayList[num6];
							}
						}
						else
						{
							num6 = (int)Math.Ceiling((double)((float)j / (float)this.ItemHeight));
						}
						this.top_index += num6;
						this.UpdateTopItem();
					}
					num = this.FocusedItem + 1;
				}
				break;
			case ListBox.ItemNavigation.Previous:
				if (this.FocusedItem > 0)
				{
					if (this.FocusedItem - 1 < this.top_index)
					{
						this.top_index--;
						this.UpdateTopItem();
					}
					num = this.FocusedItem - 1;
				}
				break;
			case ListBox.ItemNavigation.NextPage:
				if (this.Items.Count < num3)
				{
					this.NavigateItemVisually(ListBox.ItemNavigation.Last);
				}
				else if (this.FocusedItem + num3 - 1 >= this.Items.Count)
				{
					this.top_index = this.Items.Count - num3;
					this.UpdateTopItem();
					num = this.Items.Count - 1;
				}
				else
				{
					if (this.FocusedItem + num3 - 1 > this.last_visible_index)
					{
						this.top_index = this.FocusedItem;
						this.UpdateTopItem();
					}
					num = this.FocusedItem + num3 - 1;
				}
				break;
			case ListBox.ItemNavigation.PreviousPage:
			{
				int num7 = this.items_area.Height / this.ItemHeight;
				if (this.FocusedItem - (num7 - 1) <= 0)
				{
					this.top_index = 0;
					this.UpdateTopItem();
					num = 0;
				}
				else
				{
					if (this.SelectedIndex - (num7 - 1) < this.top_index)
					{
						this.top_index = this.FocusedItem - (num7 - 1);
						this.UpdateTopItem();
					}
					num = this.FocusedItem - (num7 - 1);
				}
				break;
			}
			case ListBox.ItemNavigation.PreviousColumn:
				if (this.SelectedIndex - this.RowCount < 0)
				{
					return -1;
				}
				if (this.SelectedIndex - this.RowCount < this.top_index)
				{
					this.top_index = this.SelectedIndex - this.RowCount;
					this.UpdateTopItem();
				}
				num = this.SelectedIndex - this.RowCount;
				break;
			case ListBox.ItemNavigation.NextColumn:
				if (this.SelectedIndex + this.RowCount < this.Items.Count)
				{
					if (this.SelectedIndex + this.RowCount > this.last_visible_index)
					{
						this.top_index = this.SelectedIndex;
						this.UpdateTopItem();
					}
					num = this.SelectedIndex + this.RowCount;
				}
				break;
			}
			return num;
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x0007C270 File Offset: 0x0007A470
		private void OnGotFocus(object sender, EventArgs e)
		{
			if (this.Items.Count == 0)
			{
				return;
			}
			if (this.FocusedItem == -1)
			{
				this.FocusedItem = 0;
			}
			this.InvalidateItem(this.FocusedItem);
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x0007C2B0 File Offset: 0x0007A4B0
		private void OnLostFocus(object sender, EventArgs e)
		{
			if (this.FocusedItem != -1)
			{
				this.InvalidateItem(this.FocusedItem);
			}
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x0007C2CC File Offset: 0x0007A4CC
		private bool KeySearch(Keys key)
		{
			char c = (char)key;
			if (!char.IsLetterOrDigit(c))
			{
				return false;
			}
			int num = this.FindString(c.ToString(), this.SelectedIndex);
			if (num != -1)
			{
				this.SelectedIndex = num;
			}
			return true;
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x0007C30C File Offset: 0x0007A50C
		internal void HandleKeyDown(Keys key)
		{
			int num = -1;
			if (this.Items.Count == 0)
			{
				return;
			}
			if (this.KeySearch(key))
			{
				return;
			}
			switch (key)
			{
			case Keys.Space:
				if (this.selection_mode == SelectionMode.MultiSimple)
				{
					this.SelectedItemFromNavigation(this.FocusedItem);
				}
				break;
			case Keys.PageUp:
				num = this.NavigateItemVisually(ListBox.ItemNavigation.PreviousPage);
				break;
			case Keys.PageDown:
				num = this.NavigateItemVisually(ListBox.ItemNavigation.NextPage);
				break;
			case Keys.End:
				num = this.NavigateItemVisually(ListBox.ItemNavigation.Last);
				break;
			case Keys.Home:
				num = this.NavigateItemVisually(ListBox.ItemNavigation.First);
				break;
			case Keys.Left:
				if (this.multicolumn)
				{
					num = this.NavigateItemVisually(ListBox.ItemNavigation.PreviousColumn);
				}
				break;
			case Keys.Up:
				num = this.NavigateItemVisually(ListBox.ItemNavigation.Previous);
				break;
			case Keys.Right:
				if (this.multicolumn)
				{
					num = this.NavigateItemVisually(ListBox.ItemNavigation.NextColumn);
				}
				break;
			case Keys.Down:
				num = this.NavigateItemVisually(ListBox.ItemNavigation.Next);
				break;
			default:
				if (key != Keys.ShiftKey)
				{
					if (key == Keys.ControlKey)
					{
						this.ctrl_pressed = true;
					}
				}
				else
				{
					this.shift_pressed = true;
				}
				break;
			}
			if (num != -1)
			{
				this.FocusedItem = num;
				if (this.selection_mode != SelectionMode.MultiSimple)
				{
					this.SelectedItemFromNavigation(num);
				}
			}
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x0007C458 File Offset: 0x0007A658
		private void OnKeyUpLB(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode != Keys.ShiftKey)
			{
				if (keyCode == Keys.ControlKey)
				{
					this.ctrl_pressed = false;
				}
			}
			else
			{
				this.shift_pressed = false;
			}
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0007C4A0 File Offset: 0x0007A6A0
		internal void InvalidateItem(int index)
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			Rectangle itemDisplayRectangle = this.GetItemDisplayRectangle(index, this.top_index);
			if (base.ClientRectangle.IntersectsWith(itemDisplayRectangle))
			{
				base.Invalidate(itemDisplayRectangle);
			}
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x0007C4E4 File Offset: 0x0007A6E4
		internal virtual void OnItemClick(int index)
		{
			this.OnSelectedIndexChanged(EventArgs.Empty);
			this.OnSelectedValueChanged(EventArgs.Empty);
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x0007C4FC File Offset: 0x0007A6FC
		private void SelectExtended(int index)
		{
			base.SuspendLayout();
			ArrayList arrayList = new ArrayList();
			int num = ((this.anchor >= index) ? index : this.anchor);
			int num2 = ((this.anchor <= index) ? index : this.anchor);
			for (int i = num; i <= num2; i++)
			{
				arrayList.Add(i);
			}
			if (this.ctrl_pressed)
			{
				foreach (int num3 in this.prev_selection)
				{
					if (!arrayList.Contains(num3))
					{
						arrayList.Add(num3);
					}
				}
			}
			ArrayList arrayList2 = (ArrayList)this.selected_indices.List.Clone();
			foreach (object obj in arrayList2)
			{
				int num4 = (int)obj;
				if (!arrayList.Contains(num4))
				{
					this.selected_indices.Remove(num4);
				}
			}
			foreach (object obj2 in arrayList)
			{
				int num5 = (int)obj2;
				if (!arrayList2.Contains(num5))
				{
					this.selected_indices.AddCore(num5);
				}
			}
			base.ResumeLayout();
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x0007C6D0 File Offset: 0x0007A8D0
		private void OnMouseDownLB(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.None)
			{
				return;
			}
			int num = this.IndexAtClientPoint(e.X, e.Y);
			if (num == -1)
			{
				return;
			}
			switch (this.SelectionMode)
			{
			case SelectionMode.None:
				break;
			case SelectionMode.One:
				this.SelectedIndices.AddCore(num);
				break;
			case SelectionMode.MultiSimple:
				if (this.SelectedIndices.Contains(num))
				{
					this.SelectedIndices.RemoveCore(num);
				}
				else
				{
					this.SelectedIndices.AddCore(num);
				}
				break;
			case SelectionMode.MultiExtended:
				this.shift_pressed = (XplatUI.State.ModifierKeys & Keys.Shift) != Keys.None;
				this.ctrl_pressed = (XplatUI.State.ModifierKeys & Keys.Control) != Keys.None;
				if (this.shift_pressed)
				{
					this.SelectedIndices.ClearCore();
					this.SelectExtended(num);
				}
				else
				{
					this.anchor = num;
					if (this.ctrl_pressed)
					{
						this.prev_selection = new int[this.SelectedIndices.Count];
						this.SelectedIndices.CopyTo(this.prev_selection, 0);
						if (this.SelectedIndices.Contains(num))
						{
							this.SelectedIndices.RemoveCore(num);
						}
						else
						{
							this.SelectedIndices.AddCore(num);
						}
					}
					else
					{
						this.SelectedIndices.ClearCore();
						this.SelectedIndices.AddCore(num);
					}
				}
				break;
			default:
				return;
			}
			this.button_pressed = true;
			this.button_pressed_loc = new Point(e.X, e.Y);
			this.FocusedItem = num;
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x0007C87C File Offset: 0x0007AA7C
		private void OnMouseMoveLB(object sender, MouseEventArgs e)
		{
			if (!this.button_pressed || this.button_pressed_loc == new Point(e.X, e.Y))
			{
				return;
			}
			int num = this.IndexAtClientPoint(e.X, e.Y);
			if (num == -1)
			{
				return;
			}
			switch (this.SelectionMode)
			{
			case SelectionMode.None:
				break;
			case SelectionMode.One:
				this.SelectedIndices.AddCore(num);
				break;
			case SelectionMode.MultiSimple:
				break;
			case SelectionMode.MultiExtended:
				this.SelectExtended(num);
				break;
			default:
				return;
			}
			this.FocusedItem = num;
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x0007C924 File Offset: 0x0007AB24
		internal override void OnDragDropEnd(DragDropEffects effects)
		{
			this.button_pressed = false;
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x0007C930 File Offset: 0x0007AB30
		private void OnMouseUpLB(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.None)
			{
				return;
			}
			if (e.Clicks > 1)
			{
				this.OnDoubleClick(EventArgs.Empty);
				this.OnMouseDoubleClick(e);
			}
			else if (e.Clicks == 1)
			{
				this.OnClick(EventArgs.Empty);
				this.OnMouseClick(e);
			}
			if (!this.button_pressed)
			{
				return;
			}
			int num = this.IndexAtClientPoint(e.X, e.Y);
			this.OnItemClick(num);
			this.button_pressed = (this.ctrl_pressed = (this.shift_pressed = false));
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x0007C9D0 File Offset: 0x0007ABD0
		private void Scroll(ScrollBar scrollbar, int delta)
		{
			if (delta == 0 || !scrollbar.Visible || !scrollbar.Enabled)
			{
				return;
			}
			int num;
			if (scrollbar == this.hscrollbar)
			{
				num = this.hscrollbar.Maximum - this.items_area.Width / this.ColumnWidthInternal + 1;
			}
			else
			{
				num = this.vscrollbar.Maximum - this.items_area.Height / this.ItemHeight + 1;
			}
			int num2 = scrollbar.Value + delta;
			if (num2 > num)
			{
				num2 = num;
			}
			else if (num2 < scrollbar.Minimum)
			{
				num2 = scrollbar.Minimum;
			}
			scrollbar.Value = num2;
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x0007CA80 File Offset: 0x0007AC80
		private void OnMouseWheelLB(object sender, MouseEventArgs me)
		{
			if (this.Items.Count == 0)
			{
				return;
			}
			int num = me.Delta / 120;
			if (this.MultiColumn)
			{
				this.Scroll(this.hscrollbar, -SystemInformation.MouseWheelScrollLines * num);
			}
			else
			{
				this.Scroll(this.vscrollbar, -num);
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x0007CADC File Offset: 0x0007ACDC
		internal override void OnPaintInternal(PaintEventArgs pevent)
		{
			if (this.suspend_layout)
			{
				return;
			}
			this.Draw(pevent.ClipRectangle, pevent.Graphics);
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x0007CAFC File Offset: 0x0007ACFC
		internal void RepositionScrollBars()
		{
			if (this.vscrollbar.is_visible)
			{
				this.vscrollbar.Size = new Size(this.vscrollbar.Width, this.items_area.Height);
				this.vscrollbar.Location = new Point(this.items_area.Width, 0);
			}
			if (this.hscrollbar.is_visible)
			{
				this.hscrollbar.Size = new Size(this.items_area.Width, this.hscrollbar.Height);
				this.hscrollbar.Location = new Point(0, this.items_area.Height);
			}
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x0007CBB0 File Offset: 0x0007ADB0
		internal void SelectedItemFromNavigation(int index)
		{
			switch (this.SelectionMode)
			{
			case SelectionMode.None:
				this.EnsureVisible(index);
				this.OnSelectedIndexChanged(EventArgs.Empty);
				this.OnSelectedValueChanged(EventArgs.Empty);
				break;
			case SelectionMode.One:
				this.SelectedIndex = index;
				break;
			case SelectionMode.MultiSimple:
				if (this.SelectedIndex == -1)
				{
					this.SelectedIndex = index;
				}
				else if (this.SelectedIndices.Contains(index))
				{
					this.SelectedIndices.Remove(index);
				}
				else
				{
					this.SelectedIndices.AddCore(index);
					this.OnSelectedIndexChanged(EventArgs.Empty);
					this.OnSelectedValueChanged(EventArgs.Empty);
				}
				break;
			case SelectionMode.MultiExtended:
				if (this.SelectedIndex == -1)
				{
					this.SelectedIndex = index;
				}
				else
				{
					if (!this.ctrl_pressed && !this.shift_pressed)
					{
						this.SelectedIndices.Clear();
					}
					if (this.shift_pressed)
					{
						this.ShiftSelection(index);
					}
					else
					{
						this.SelectedIndices.AddCore(index);
					}
					this.OnSelectedIndexChanged(EventArgs.Empty);
					this.OnSelectedValueChanged(EventArgs.Empty);
				}
				break;
			}
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x0007CCEC File Offset: 0x0007AEEC
		private void ShiftSelection(int index)
		{
			int num = -1;
			int num2 = this.Items.Count + 1;
			foreach (object obj in this.selected_indices)
			{
				int num3 = (int)obj;
				int num4;
				if (num3 > index)
				{
					num4 = num3 - index;
				}
				else
				{
					num4 = index - num3;
				}
				if (num4 < num2)
				{
					num2 = num4;
					num = num3;
				}
			}
			if (num != -1)
			{
				int num5;
				int num6;
				if (num > index)
				{
					num5 = index;
					num6 = num;
				}
				else
				{
					num5 = num;
					num6 = index;
				}
				this.selected_indices.Clear();
				for (int i = num5; i <= num6; i++)
				{
					this.selected_indices.AddCore(i);
				}
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06002144 RID: 8516 RVA: 0x0007CDDC File Offset: 0x0007AFDC
		// (set) Token: 0x06002145 RID: 8517 RVA: 0x0007CDE4 File Offset: 0x0007AFE4
		internal int FocusedItem
		{
			get
			{
				return this.focused_item;
			}
			set
			{
				if (this.focused_item == value)
				{
					return;
				}
				int num = this.focused_item;
				this.focused_item = value;
				if (!this.has_focus)
				{
					return;
				}
				if (num != -1)
				{
					this.InvalidateItem(num);
				}
				if (value != -1)
				{
					this.InvalidateItem(value);
				}
				this.OnUIAFocusedItemChangedEvent();
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06002146 RID: 8518 RVA: 0x0007CE3C File Offset: 0x0007B03C
		internal StringFormat StringFormat
		{
			get
			{
				if (this.string_format == null)
				{
					this.string_format = new StringFormat();
					this.string_format.FormatFlags = 4096;
					if (this.RightToLeft == RightToLeft.Yes)
					{
						this.string_format.Alignment = 2;
					}
					else
					{
						this.string_format.Alignment = 0;
					}
					this.CalculateTabStops();
				}
				return this.string_format;
			}
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x0007CEA4 File Offset: 0x0007B0A4
		internal virtual void CollectionChanged()
		{
			if (this.sorted)
			{
				this.Sort(false);
			}
			if (this.Items.Count == 0)
			{
				this.selected_indices.List.Clear();
				this.focused_item = -1;
				this.top_index = 0;
			}
			if (this.Items.Count <= this.focused_item)
			{
				this.focused_item = this.Items.Count - 1;
			}
			if (!base.IsHandleCreated || this.suspend_layout)
			{
				return;
			}
			this.LayoutListBox();
			base.Refresh();
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x0007CF40 File Offset: 0x0007B140
		private void EnsureVisible(int index)
		{
			if (!base.IsHandleCreated || index == -1)
			{
				return;
			}
			if (index < this.top_index)
			{
				this.top_index = index;
				this.UpdateTopItem();
				base.Invalidate();
			}
			else if (!this.multicolumn)
			{
				int num = this.items_area.Height / this.ItemHeight;
				if (index >= this.top_index + num)
				{
					this.top_index = index - num + 1;
				}
				this.UpdateTopItem();
			}
			else
			{
				int num2 = Math.Max(1, this.items_area.Height / this.ItemHeight);
				int num3 = Math.Max(1, this.items_area.Width / this.ColumnWidthInternal);
				if (index >= this.top_index + num2 * num3)
				{
					int num4 = index / num2;
					this.top_index = (num4 - (num3 - 1)) * num2;
					this.UpdateTopItem();
					base.Invalidate();
				}
			}
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x0007D028 File Offset: 0x0007B228
		private void UpdateListBoxBounds()
		{
			if (base.IsHandleCreated)
			{
				base.SetBoundsInternal(this.bounds.X, this.bounds.Y, this.bounds.Width, (!this.IntegralHeight) ? this.requested_height : this.SnapHeightToIntegral(this.requested_height), BoundsSpecified.None);
			}
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x0007D08C File Offset: 0x0007B28C
		private void UpdateScrollBars()
		{
			this.items_area = base.ClientRectangle;
			if (this.UpdateHorizontalScrollBar())
			{
				this.items_area.Height = this.items_area.Height - this.hscrollbar.Height;
				if (this.UpdateVerticalScrollBar())
				{
					this.items_area.Width = this.items_area.Width - this.vscrollbar.Width;
					this.UpdateHorizontalScrollBar();
				}
			}
			else if (this.UpdateVerticalScrollBar())
			{
				this.items_area.Width = this.items_area.Width - this.vscrollbar.Width;
				if (this.UpdateHorizontalScrollBar())
				{
					this.items_area.Height = this.items_area.Height - this.hscrollbar.Height;
					this.UpdateVerticalScrollBar();
				}
			}
			this.RepositionScrollBars();
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x0007D160 File Offset: 0x0007B360
		private bool UpdateHorizontalScrollBar()
		{
			bool flag = false;
			bool flag2 = true;
			if (this.MultiColumn)
			{
				if (this.canvas_size.Width > this.items_area.Width)
				{
					flag = true;
					this.hscrollbar.Maximum = this.canvas_size.Width / this.ColumnWidthInternal - 1;
				}
				else if (this.ScrollAlwaysVisible)
				{
					flag2 = false;
					flag = true;
					this.hscrollbar.Maximum = 0;
				}
			}
			else if (this.canvas_size.Width > base.ClientRectangle.Width && this.HorizontalScrollbar)
			{
				flag = true;
				this.hscrollbar.Maximum = this.canvas_size.Width;
				this.hscrollbar.LargeChange = Math.Max(0, this.items_area.Width);
			}
			else if (this.scroll_always_visible && this.horizontal_scrollbar)
			{
				flag = true;
				flag2 = false;
				this.hscrollbar.Maximum = 0;
			}
			this.hbar_offset = this.hscrollbar.Value;
			this.hscrollbar.Enabled = flag2;
			this.hscrollbar.Visible = flag;
			return flag;
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x0007D290 File Offset: 0x0007B490
		private bool UpdateVerticalScrollBar()
		{
			if (this.MultiColumn || (this.Items.Count == 0 && !this.scroll_always_visible))
			{
				this.vscrollbar.Visible = false;
				return false;
			}
			if (this.Items.Count == 0)
			{
				this.vscrollbar.Visible = true;
				this.vscrollbar.Enabled = false;
				this.vscrollbar.Maximum = 0;
				return true;
			}
			bool flag = false;
			bool flag2 = true;
			if (this.canvas_size.Height > this.items_area.Height)
			{
				flag = true;
				this.vscrollbar.Maximum = this.Items.Count - 1;
				this.vscrollbar.LargeChange = Math.Max(this.items_area.Height / this.ItemHeight, 0);
			}
			else if (this.ScrollAlwaysVisible)
			{
				flag = true;
				flag2 = false;
				this.vscrollbar.Maximum = 0;
			}
			this.vscrollbar.Enabled = flag2;
			this.vscrollbar.Visible = flag;
			return flag;
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x0007D39C File Offset: 0x0007B59C
		private void VerticalScrollEvent(object sender, EventArgs e)
		{
			int num = this.top_index;
			this.top_index = this.vscrollbar.Value;
			this.last_visible_index = this.LastVisibleItem();
			int num2 = (num - this.top_index) * this.ItemHeight;
			if (this.DrawMode == DrawMode.OwnerDrawVariable)
			{
				num2 = 0;
				if (this.top_index < num)
				{
					for (int i = this.top_index; i < num; i++)
					{
						num2 += this.GetItemHeight(i);
					}
				}
				else
				{
					for (int j = num; j < this.top_index; j++)
					{
						num2 -= this.GetItemHeight(j);
					}
				}
			}
			if (base.IsHandleCreated)
			{
				XplatUI.ScrollWindow(this.Handle, this.items_area, 0, num2, false);
			}
		}

		/// <summary>Specifies the default item height for an owner-drawn <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400119D RID: 4509
		public const int DefaultItemHeight = 13;

		/// <summary>Specifies that no matches are found during a search.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400119E RID: 4510
		public const int NoMatches = -1;

		// Token: 0x0400119F RID: 4511
		private Hashtable item_heights;

		// Token: 0x040011A0 RID: 4512
		private int item_height = -1;

		// Token: 0x040011A1 RID: 4513
		private int column_width;

		// Token: 0x040011A2 RID: 4514
		private int requested_height;

		// Token: 0x040011A3 RID: 4515
		private DrawMode draw_mode;

		// Token: 0x040011A4 RID: 4516
		private int horizontal_extent;

		// Token: 0x040011A5 RID: 4517
		private bool horizontal_scrollbar;

		// Token: 0x040011A6 RID: 4518
		private bool integral_height = true;

		// Token: 0x040011A7 RID: 4519
		private bool multicolumn;

		// Token: 0x040011A8 RID: 4520
		private bool scroll_always_visible;

		// Token: 0x040011A9 RID: 4521
		private ListBox.SelectedIndexCollection selected_indices;

		// Token: 0x040011AA RID: 4522
		private ListBox.SelectedObjectCollection selected_items;

		// Token: 0x040011AB RID: 4523
		private SelectionMode selection_mode = SelectionMode.One;

		// Token: 0x040011AC RID: 4524
		private bool sorted;

		// Token: 0x040011AD RID: 4525
		private bool use_tabstops = true;

		// Token: 0x040011AE RID: 4526
		private int column_width_internal = 120;

		// Token: 0x040011AF RID: 4527
		private ImplicitVScrollBar vscrollbar;

		// Token: 0x040011B0 RID: 4528
		private ImplicitHScrollBar hscrollbar;

		// Token: 0x040011B1 RID: 4529
		private int hbar_offset;

		// Token: 0x040011B2 RID: 4530
		private bool suspend_layout;

		// Token: 0x040011B3 RID: 4531
		private bool ctrl_pressed;

		// Token: 0x040011B4 RID: 4532
		private bool shift_pressed;

		// Token: 0x040011B5 RID: 4533
		private bool explicit_item_height;

		// Token: 0x040011B6 RID: 4534
		private int top_index;

		// Token: 0x040011B7 RID: 4535
		private int last_visible_index;

		// Token: 0x040011B8 RID: 4536
		private Rectangle items_area;

		// Token: 0x040011B9 RID: 4537
		private int focused_item = -1;

		// Token: 0x040011BA RID: 4538
		private ListBox.ObjectCollection items;

		// Token: 0x040011BB RID: 4539
		private ListBox.IntegerCollection custom_tab_offsets;

		// Token: 0x040011BC RID: 4540
		private Padding padding;

		// Token: 0x040011BD RID: 4541
		private bool use_custom_tab_offsets;

		// Token: 0x040011C3 RID: 4547
		private int row_count = 1;

		// Token: 0x040011C4 RID: 4548
		private Size canvas_size;

		// Token: 0x040011C5 RID: 4549
		private int anchor = -1;

		// Token: 0x040011C6 RID: 4550
		private int[] prev_selection;

		// Token: 0x040011C7 RID: 4551
		private bool button_pressed;

		// Token: 0x040011C8 RID: 4552
		private Point button_pressed_loc = new Point(-1, -1);

		// Token: 0x040011C9 RID: 4553
		private StringFormat string_format;

		// Token: 0x02000214 RID: 532
		internal enum ItemNavigation
		{
			// Token: 0x040011CB RID: 4555
			First,
			// Token: 0x040011CC RID: 4556
			Last,
			// Token: 0x040011CD RID: 4557
			Next,
			// Token: 0x040011CE RID: 4558
			Previous,
			// Token: 0x040011CF RID: 4559
			NextPage,
			// Token: 0x040011D0 RID: 4560
			PreviousPage,
			// Token: 0x040011D1 RID: 4561
			PreviousColumn,
			// Token: 0x040011D2 RID: 4562
			NextColumn
		}

		/// <summary>Represents a collection of integers in a <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		// Token: 0x02000215 RID: 533
		public class IntegerCollection : ICollection, IEnumerable, IList
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" /> class. </summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListBox" /> that owns the collection.</param>
			// Token: 0x0600214E RID: 8526 RVA: 0x0007D460 File Offset: 0x0007B660
			public IntegerCollection(ListBox owner)
			{
				this.owner = owner;
				this.list = new List<int>();
			}

			/// <summary>Retrieves an enumeration of all the integers in the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</returns>
			// Token: 0x0600214F RID: 8527 RVA: 0x0007D47C File Offset: 0x0007B67C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.list.GetEnumerator();
			}

			/// <summary>Adds a tab stop to the collection.</summary>
			/// <returns>The index at which the integer was added to the collection.</returns>
			/// <param name="item">The tab stop to add to the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="item" /> is null.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="item" /> is not an 32-bit signed integer.</exception>
			/// <exception cref="T:System.SystemException">There is insufficient space to store the new item in the collection.</exception>
			// Token: 0x06002150 RID: 8528 RVA: 0x0007D490 File Offset: 0x0007B690
			int IList.Add(object item)
			{
				int? num = item as int?;
				if (num == null)
				{
					throw new ArgumentException("item");
				}
				return this.Add(num.Value);
			}

			/// <summary>Clears all the tab stops from the collection.</summary>
			// Token: 0x06002151 RID: 8529 RVA: 0x0007D4D0 File Offset: 0x0007B6D0
			void IList.Clear()
			{
				this.Clear();
			}

			/// <summary>Determines whether the specified tab stop is in the collection.</summary>
			/// <returns>true if item is an integer located in the IntegerCollection; otherwise, false.</returns>
			/// <param name="item">The tab stop to locate in the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</param>
			// Token: 0x06002152 RID: 8530 RVA: 0x0007D4D8 File Offset: 0x0007B6D8
			bool IList.Contains(object item)
			{
				int? num = item as int?;
				return num != null && this.Contains(num.Value);
			}

			/// <summary>Returns the index of the specified tab stop in the collection.</summary>
			/// <returns>The zero-based index of item if it was found in the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />; otherwise, -1.</returns>
			/// <param name="item">The tab stop to locate in the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</param>
			// Token: 0x06002153 RID: 8531 RVA: 0x0007D50C File Offset: 0x0007B70C
			int IList.IndexOf(object item)
			{
				int? num = item as int?;
				if (num == null)
				{
					return -1;
				}
				return this.IndexOf(num.Value);
			}

			/// <summary>Inserts an item into the collection at a specified index.</summary>
			/// <param name="index">The zero-based index at which value should be inserted.</param>
			/// <param name="value">The object to insert into the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002154 RID: 8532 RVA: 0x0007D540 File Offset: 0x0007B740
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "No items can be inserted into {0}, since it is a sorted collection.", new object[] { base.GetType() }));
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x17000839 RID: 2105
			// (get) Token: 0x06002155 RID: 8533 RVA: 0x0007D570 File Offset: 0x0007B770
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x1700083A RID: 2106
			// (get) Token: 0x06002156 RID: 8534 RVA: 0x0007D574 File Offset: 0x0007B774
			bool IList.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Removes the first occurrence of an item from the collection.</summary>
			/// <param name="value">The object to add to the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002157 RID: 8535 RVA: 0x0007D578 File Offset: 0x0007B778
			void IList.Remove(object value)
			{
				int? num = value as int?;
				if (num == null)
				{
					throw new ArgumentException("value");
				}
				this.Remove(num.Value);
			}

			/// <summary>Removes the item at a specified index.</summary>
			/// <param name="index">The index of the item to remove.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002158 RID: 8536 RVA: 0x0007D5B8 File Offset: 0x0007B7B8
			void IList.RemoveAt(int index)
			{
				this.RemoveAt(index);
			}

			/// <summary>Gets or sets the tab stop at the specified index.</summary>
			/// <returns>The tab stop that is stored at the specified location in the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</returns>
			/// <param name="index">The zero-based index that specifies which tab stop to get.</param>
			/// <exception cref="T:System.ArgumentException">The object is not an integer.</exception>
			// Token: 0x1700083B RID: 2107
			// (get) Token: 0x06002159 RID: 8537 RVA: 0x0007D5C4 File Offset: 0x0007B7C4
			// (set) Token: 0x0600215A RID: 8538 RVA: 0x0007D5D4 File Offset: 0x0007B7D4
			object IList.Item
			{
				get
				{
					return this[index];
				}
				set
				{
					this[index] = (int)value;
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x1700083C RID: 2108
			// (get) Token: 0x0600215B RID: 8539 RVA: 0x0007D5E4 File Offset: 0x0007B7E4
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
			/// <returns>The object used to synchronize to the collection.</returns>
			// Token: 0x1700083D RID: 2109
			// (get) Token: 0x0600215C RID: 8540 RVA: 0x0007D5E8 File Offset: 0x0007B7E8
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets the number of selected items in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
			/// <returns>The number of selected items in the <see cref="T:System.Windows.Forms.ListBox" />.</returns>
			// Token: 0x1700083E RID: 2110
			// (get) Token: 0x0600215D RID: 8541 RVA: 0x0007D5EC File Offset: 0x0007B7EC
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.list.Count;
				}
			}

			/// <summary>Gets or sets the <see cref="P:System.Windows.Forms.ListBox.IntegerCollection.Item(System.Int32)" /> having the specified index.</summary>
			/// <returns>The selected <see cref="P:System.Windows.Forms.ListBox.IntegerCollection.Item(System.Int32)" /> at the specified position.</returns>
			/// <param name="index">The position of the <see cref="P:System.Windows.Forms.ListBox.IntegerCollection.Item(System.Int32)" /> in the collection.</param>
			// Token: 0x1700083F RID: 2111
			public int this[int index]
			{
				get
				{
					return this.list[index];
				}
				set
				{
					this.list[index] = value;
					this.owner.CalculateTabStops();
				}
			}

			/// <summary>Adds a unique integer to the collection in sorted order.</summary>
			/// <returns>The index of the added item.</returns>
			/// <param name="item">The integer to add to the collection.</param>
			/// <exception cref="T:System.SystemException">There is insufficient space available to store the new item.</exception>
			// Token: 0x06002160 RID: 8544 RVA: 0x0007D628 File Offset: 0x0007B828
			public int Add(int item)
			{
				if (!this.list.Contains(item))
				{
					this.list.Add(item);
					this.list.Sort();
					this.owner.CalculateTabStops();
				}
				return this.list.IndexOf(item);
			}

			/// <summary>Adds an array of integers to the collection.</summary>
			/// <param name="items">The array of integers to add to the collection.</param>
			// Token: 0x06002161 RID: 8545 RVA: 0x0007D674 File Offset: 0x0007B874
			public void AddRange(int[] items)
			{
				this.AddItems(items);
			}

			/// <summary>Adds the contents of an existing <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" /> to another collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" /> to add to another collection.</param>
			// Token: 0x06002162 RID: 8546 RVA: 0x0007D680 File Offset: 0x0007B880
			public void AddRange(ListBox.IntegerCollection value)
			{
				this.AddItems(value);
			}

			// Token: 0x06002163 RID: 8547 RVA: 0x0007D68C File Offset: 0x0007B88C
			private void AddItems(IList items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				foreach (object obj in items)
				{
					int num = (int)obj;
					if (!this.list.Contains(num))
					{
						this.list.Add(num);
					}
				}
				this.list.Sort();
			}

			/// <summary>Removes all integers from the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</summary>
			// Token: 0x06002164 RID: 8548 RVA: 0x0007D728 File Offset: 0x0007B928
			public void Clear()
			{
				this.list.Clear();
				this.owner.CalculateTabStops();
			}

			/// <summary>Determines whether the specified integer is in the collection.</summary>
			/// <returns>true if the specified integer is in the collection; otherwise, false. </returns>
			/// <param name="item">The integer to search for in the collection.</param>
			// Token: 0x06002165 RID: 8549 RVA: 0x0007D740 File Offset: 0x0007B940
			public bool Contains(int item)
			{
				return this.list.Contains(item);
			}

			/// <summary>Copies the entire <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" /> into an existing array of integers at a specified location within the array.</summary>
			/// <param name="destination">The array into which the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" /> is copied.</param>
			/// <param name="index">The location within the destination array to which to copy the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</param>
			// Token: 0x06002166 RID: 8550 RVA: 0x0007D750 File Offset: 0x0007B950
			public void CopyTo(Array destination, int index)
			{
				for (int i = 0; i < this.list.Count; i++)
				{
					destination.SetValue(this.list[i], index++);
				}
			}

			/// <summary>Retrieves the index within the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" /> of the specified integer.</summary>
			/// <returns>The zero-based index of the integer in the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />; otherwise, negative one (-1).</returns>
			/// <param name="item">The integer for which to retrieve the index.</param>
			// Token: 0x06002167 RID: 8551 RVA: 0x0007D798 File Offset: 0x0007B998
			public int IndexOf(int item)
			{
				return this.list.IndexOf(item);
			}

			/// <summary>Removes the specified integer from the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</summary>
			/// <param name="item">The integer to remove from the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</param>
			// Token: 0x06002168 RID: 8552 RVA: 0x0007D7A8 File Offset: 0x0007B9A8
			public void Remove(int item)
			{
				this.list.Remove(item);
				this.list.Sort();
				this.owner.CalculateTabStops();
			}

			/// <summary>Removes the integer at the specified index from the <see cref="T:System.Windows.Forms.ListBox.IntegerCollection" />.</summary>
			/// <param name="index">The zero-based index of the integer to remove.</param>
			// Token: 0x06002169 RID: 8553 RVA: 0x0007D7D0 File Offset: 0x0007B9D0
			public void RemoveAt(int index)
			{
				if (index < 0)
				{
					throw new IndexOutOfRangeException();
				}
				this.list.RemoveAt(index);
				this.list.Sort();
				this.owner.CalculateTabStops();
			}

			// Token: 0x040011D3 RID: 4563
			private ListBox owner;

			// Token: 0x040011D4 RID: 4564
			private List<int> list;
		}

		/// <summary>Represents the collection of items in a <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		// Token: 0x02000216 RID: 534
		[ListBindable(false)]
		public class ObjectCollection : ICollection, IEnumerable, IList
		{
			/// <summary>Initializes a new instance of <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" />.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListBox" /> that owns the collection. </param>
			// Token: 0x0600216A RID: 8554 RVA: 0x0007D804 File Offset: 0x0007BA04
			public ObjectCollection(ListBox owner)
			{
				this.owner = owner;
			}

			/// <summary>Initializes a new instance of <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> containing an array of objects.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListBox" /> that owns the collection. </param>
			/// <param name="value">An array of objects to add to the collection. </param>
			// Token: 0x0600216B RID: 8555 RVA: 0x0007D820 File Offset: 0x0007BA20
			public ObjectCollection(ListBox owner, object[] value)
			{
				this.owner = owner;
				this.AddRange(value);
			}

			/// <summary>Initializes a new instance of <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> based on another <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" />.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ListBox" /> that owns the collection. </param>
			/// <param name="value">A <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> from which the contents are copied to this collection. </param>
			// Token: 0x0600216C RID: 8556 RVA: 0x0007D844 File Offset: 0x0007BA44
			public ObjectCollection(ListBox owner, ListBox.ObjectCollection value)
			{
				this.owner = owner;
				this.AddRange(value);
			}

			// Token: 0x0600216D RID: 8557 RVA: 0x0007D868 File Offset: 0x0007BA68
			// Note: this type is marked as 'beforefieldinit'.
			static ObjectCollection()
			{
				ListBox.ObjectCollection.UIACollectionChangedEvent = new object();
			}

			// Token: 0x14000205 RID: 517
			// (add) Token: 0x0600216E RID: 8558 RVA: 0x0007D874 File Offset: 0x0007BA74
			// (remove) Token: 0x0600216F RID: 8559 RVA: 0x0007D88C File Offset: 0x0007BA8C
			internal event CollectionChangeEventHandler UIACollectionChanged
			{
				add
				{
					this.owner.Events.AddHandler(ListBox.ObjectCollection.UIACollectionChangedEvent, value);
				}
				remove
				{
					this.owner.Events.RemoveHandler(ListBox.ObjectCollection.UIACollectionChangedEvent, value);
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000840 RID: 2112
			// (get) Token: 0x06002170 RID: 8560 RVA: 0x0007D8A4 File Offset: 0x0007BAA4
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" />.</returns>
			// Token: 0x17000841 RID: 2113
			// (get) Token: 0x06002171 RID: 8561 RVA: 0x0007D8A8 File Offset: 0x0007BAA8
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x17000842 RID: 2114
			// (get) Token: 0x06002172 RID: 8562 RVA: 0x0007D8AC File Offset: 0x0007BAAC
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Copies the elements of the collection to an array, starting at a particular array index.</summary>
			/// <param name="destination">The one-dimensional array that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in the array at which copying begins.</param>
			/// <exception cref="T:System.ArrayTypeMismatchException">The array type is not compatible with the items in the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" />.</exception>
			// Token: 0x06002173 RID: 8563 RVA: 0x0007D8B0 File Offset: 0x0007BAB0
			void ICollection.CopyTo(Array destination, int index)
			{
				this.object_items.CopyTo(destination, index);
			}

			/// <summary>Adds an object to the <see cref="T:System.Windows.Forms.ListBox" /> class.</summary>
			/// <param name="item">The object to be added to the <see cref="T:System.Windows.Forms.ListBox" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="item" /> is null.</exception>
			/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Windows.Forms.ListBox" /> has a data source.</exception>
			/// <exception cref="T:System.SystemException">There is insufficient space available to store the new item.</exception>
			// Token: 0x06002174 RID: 8564 RVA: 0x0007D8CC File Offset: 0x0007BACC
			int IList.Add(object item)
			{
				return this.Add(item);
			}

			// Token: 0x06002175 RID: 8565 RVA: 0x0007D8D8 File Offset: 0x0007BAD8
			internal void OnUIACollectionChangedEvent(CollectionChangeEventArgs args)
			{
				CollectionChangeEventHandler collectionChangeEventHandler = (CollectionChangeEventHandler)this.owner.Events[ListBox.ObjectCollection.UIACollectionChangedEvent];
				if (collectionChangeEventHandler != null)
				{
					collectionChangeEventHandler.Invoke(this.owner, args);
				}
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection </returns>
			// Token: 0x17000843 RID: 2115
			// (get) Token: 0x06002176 RID: 8566 RVA: 0x0007D914 File Offset: 0x0007BB14
			public int Count
			{
				get
				{
					return this.object_items.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if this collection is read-only; otherwise, false.</returns>
			// Token: 0x17000844 RID: 2116
			// (get) Token: 0x06002177 RID: 8567 RVA: 0x0007D924 File Offset: 0x0007BB24
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets or sets the item at the specified index within the collection.</summary>
			/// <returns>An object representing the item located at the specified index within the collection.</returns>
			/// <param name="index">The index of the item in the collection to get or set. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListBox.ObjectCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> class. </exception>
			// Token: 0x17000845 RID: 2117
			[DesignerSerializationVisibility(0)]
			[Browsable(false)]
			public virtual object this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("Index of out range");
					}
					return this.object_items[index];
				}
				set
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("Index of out range");
					}
					if (value == null)
					{
						throw new ArgumentNullException("value");
					}
					this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(2, this.object_items[index]));
					this.object_items[index] = value;
					this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(1, value));
					this.owner.CollectionChanged();
				}
			}

			/// <summary>Adds an item to the list of items for a <see cref="T:System.Windows.Forms.ListBox" />.</summary>
			/// <returns>The zero-based index of the item in the collection, or -1 if <see cref="M:System.Windows.Forms.ListBox.BeginUpdate" /> has been called.</returns>
			/// <param name="item">An object representing the item to add to the collection. </param>
			/// <exception cref="T:System.SystemException">There is insufficient space available to add the new item to the list. </exception>
			// Token: 0x0600217A RID: 8570 RVA: 0x0007D9DC File Offset: 0x0007BBDC
			public int Add(object item)
			{
				int num = this.AddItem(item);
				this.owner.CollectionChanged();
				if (this.owner.sorted)
				{
					return this.IndexOf(item);
				}
				return num;
			}

			/// <summary>Adds an array of items to the list of items for a <see cref="T:System.Windows.Forms.ListBox" />.</summary>
			/// <param name="items">An array of objects to add to the list. </param>
			// Token: 0x0600217B RID: 8571 RVA: 0x0007DA18 File Offset: 0x0007BC18
			public void AddRange(object[] items)
			{
				this.AddItems(items);
			}

			/// <summary>Adds the items of an existing <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> to the list of items in a <see cref="T:System.Windows.Forms.ListBox" />.</summary>
			/// <param name="value">A <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> to load into this collection. </param>
			// Token: 0x0600217C RID: 8572 RVA: 0x0007DA24 File Offset: 0x0007BC24
			public void AddRange(ListBox.ObjectCollection value)
			{
				this.AddItems(value);
			}

			// Token: 0x0600217D RID: 8573 RVA: 0x0007DA30 File Offset: 0x0007BC30
			internal void AddItems(IList items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				foreach (object obj in items)
				{
					this.AddItem(obj);
				}
				this.owner.CollectionChanged();
			}

			/// <summary>Removes all items from the collection.</summary>
			// Token: 0x0600217E RID: 8574 RVA: 0x0007DAB4 File Offset: 0x0007BCB4
			public virtual void Clear()
			{
				this.owner.selected_indices.ClearCore();
				this.object_items.Clear();
				this.owner.CollectionChanged();
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(3, null));
			}

			/// <summary>Determines whether the specified item is located within the collection.</summary>
			/// <returns>true if the item is located within the collection; otherwise, false.</returns>
			/// <param name="value">An object representing the item to locate in the collection. </param>
			// Token: 0x0600217F RID: 8575 RVA: 0x0007DAF8 File Offset: 0x0007BCF8
			public bool Contains(object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				return this.object_items.Contains(value);
			}

			/// <summary>Copies the entire collection into an existing array of objects at a specified location within the array.</summary>
			/// <param name="destination">The object array in which the items from the collection are copied to. </param>
			/// <param name="arrayIndex">The location within the destination array to copy the items from the collection to. </param>
			// Token: 0x06002180 RID: 8576 RVA: 0x0007DB18 File Offset: 0x0007BD18
			public void CopyTo(object[] destination, int arrayIndex)
			{
				this.object_items.CopyTo(destination, arrayIndex);
			}

			/// <summary>Returns an enumerator to use to iterate through the item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the item collection.</returns>
			// Token: 0x06002181 RID: 8577 RVA: 0x0007DB34 File Offset: 0x0007BD34
			public IEnumerator GetEnumerator()
			{
				return this.object_items.GetEnumerator();
			}

			/// <summary>Returns the index within the collection of the specified item.</summary>
			/// <returns>The zero-based index where the item is located within the collection; otherwise, negative one (-1).</returns>
			/// <param name="value">An object representing the item to locate in the collection. </param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
			// Token: 0x06002182 RID: 8578 RVA: 0x0007DB44 File Offset: 0x0007BD44
			public int IndexOf(object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				return this.object_items.IndexOf(value);
			}

			/// <summary>Inserts an item into the list box at the specified index.</summary>
			/// <param name="index">The zero-based index location where the item is inserted. </param>
			/// <param name="item">An object representing the item to insert. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than value of the <see cref="P:System.Windows.Forms.ListBox.ObjectCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> class. </exception>
			// Token: 0x06002183 RID: 8579 RVA: 0x0007DB64 File Offset: 0x0007BD64
			public void Insert(int index, object item)
			{
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("Index of out range");
				}
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				this.owner.BeginUpdate();
				this.object_items.Insert(index, item);
				this.owner.CollectionChanged();
				this.owner.EndUpdate();
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(1, item));
			}

			/// <summary>Removes the specified object from the collection.</summary>
			/// <param name="value">An object representing the item to remove from the collection. </param>
			// Token: 0x06002184 RID: 8580 RVA: 0x0007DBDC File Offset: 0x0007BDDC
			public void Remove(object value)
			{
				if (value == null)
				{
					return;
				}
				int num = this.IndexOf(value);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Removes the item at the specified index within the collection.</summary>
			/// <param name="index">The zero-based index of the item to remove. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListBox.ObjectCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> class. </exception>
			// Token: 0x06002185 RID: 8581 RVA: 0x0007DC08 File Offset: 0x0007BE08
			public void RemoveAt(int index)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("Index of out range");
				}
				object obj = this.object_items[index];
				this.UpdateSelection(index);
				this.object_items.RemoveAt(index);
				this.owner.CollectionChanged();
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(2, obj));
			}

			// Token: 0x06002186 RID: 8582 RVA: 0x0007DC6C File Offset: 0x0007BE6C
			internal int AddItem(object item)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				int count = this.object_items.Count;
				this.object_items.Add(item);
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(1, item));
				return count;
			}

			// Token: 0x06002187 RID: 8583 RVA: 0x0007DCB4 File Offset: 0x0007BEB4
			private void UpdateSelection(int removed_index)
			{
				this.owner.selected_indices.Remove(removed_index);
				if (this.owner.selection_mode != SelectionMode.None)
				{
					int num = this.object_items.Count - 1;
					if (this.owner.selected_indices.Contains(num))
					{
						this.owner.selected_indices.Remove(num);
						int num2 = num - 1;
						if (this.owner.selection_mode == SelectionMode.One && num2 > -1)
						{
							this.owner.selected_indices.Add(num2);
						}
					}
				}
			}

			// Token: 0x06002188 RID: 8584 RVA: 0x0007DD44 File Offset: 0x0007BF44
			internal void Sort()
			{
				this.object_items.Sort(new ListBox.ObjectCollection.ListObjectComparer());
			}

			// Token: 0x040011D5 RID: 4565
			private ListBox owner;

			// Token: 0x040011D6 RID: 4566
			internal ArrayList object_items = new ArrayList();

			// Token: 0x02000217 RID: 535
			internal class ListObjectComparer : IComparer
			{
				// Token: 0x0600218A RID: 8586 RVA: 0x0007DD60 File Offset: 0x0007BF60
				public int Compare(object a, object b)
				{
					string text = a.ToString();
					string text2 = b.ToString();
					return text.CompareTo(text2);
				}
			}
		}

		/// <summary>Represents the collection containing the indexes to the selected items in a <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		// Token: 0x02000218 RID: 536
		public class SelectedIndexCollection : ICollection, IEnumerable, IList
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" /> class.</summary>
			/// <param name="owner">A <see cref="T:System.Windows.Forms.ListBox" /> representing the owner of the collection. </param>
			// Token: 0x0600218B RID: 8587 RVA: 0x0007DD84 File Offset: 0x0007BF84
			public SelectedIndexCollection(ListBox owner)
			{
				this.owner = owner;
				this.selection = new ArrayList();
			}

			// Token: 0x0600218C RID: 8588 RVA: 0x0007DDA0 File Offset: 0x0007BFA0
			// Note: this type is marked as 'beforefieldinit'.
			static SelectedIndexCollection()
			{
				ListBox.SelectedIndexCollection.UIACollectionChangedEvent = new object();
			}

			// Token: 0x14000206 RID: 518
			// (add) Token: 0x0600218D RID: 8589 RVA: 0x0007DDAC File Offset: 0x0007BFAC
			// (remove) Token: 0x0600218E RID: 8590 RVA: 0x0007DDC4 File Offset: 0x0007BFC4
			internal event CollectionChangeEventHandler UIACollectionChanged
			{
				add
				{
					this.owner.Events.AddHandler(ListBox.SelectedIndexCollection.UIACollectionChangedEvent, value);
				}
				remove
				{
					this.owner.Events.RemoveHandler(ListBox.SelectedIndexCollection.UIACollectionChangedEvent, value);
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000846 RID: 2118
			// (get) Token: 0x0600218F RID: 8591 RVA: 0x0007DDDC File Offset: 0x0007BFDC
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
			/// <returns>true in all cases.</returns>
			// Token: 0x17000847 RID: 2119
			// (get) Token: 0x06002190 RID: 8592 RVA: 0x0007DDE0 File Offset: 0x0007BFE0
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" />.</returns>
			// Token: 0x17000848 RID: 2120
			// (get) Token: 0x06002191 RID: 8593 RVA: 0x0007DDE4 File Offset: 0x0007BFE4
			object ICollection.SyncRoot
			{
				get
				{
					return this.selection;
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			/// <returns>The position into which the index was inserted.</returns>
			/// <param name="value">The index to add to the collection.</param>
			// Token: 0x06002192 RID: 8594 RVA: 0x0007DDEC File Offset: 0x0007BFEC
			int IList.Add(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
			// Token: 0x06002193 RID: 8595 RVA: 0x0007DDF4 File Offset: 0x0007BFF4
			void IList.Clear()
			{
				throw new NotSupportedException();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
			/// <returns>true if the specified index from the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> for the <see cref="T:System.Windows.Forms.ListBox" /> is an item in this collection; otherwise, false.</returns>
			/// <param name="selectedIndex">The index to locate in the collection.</param>
			// Token: 0x06002194 RID: 8596 RVA: 0x0007DDFC File Offset: 0x0007BFFC
			bool IList.Contains(object selectedIndex)
			{
				return this.Contains((int)selectedIndex);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
			/// <returns>The zero-based index in the collection where the specified index of the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> is located if it is in the <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" />; otherwise, -1.</returns>
			/// <param name="selectedIndex">The zero-based index from the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> to locate in this collection.</param>
			// Token: 0x06002195 RID: 8597 RVA: 0x0007DE0C File Offset: 0x0007C00C
			int IList.IndexOf(object selectedIndex)
			{
				return this.IndexOf((int)selectedIndex);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
			/// <param name="index">The index at which value should be inserted.</param>
			/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002196 RID: 8598 RVA: 0x0007DE1C File Offset: 0x0007C01C
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
			/// <param name="value">The object to be removed from the <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" />.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002197 RID: 8599 RVA: 0x0007DE24 File Offset: 0x0007C024
			void IList.Remove(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
			// Token: 0x06002198 RID: 8600 RVA: 0x0007DE2C File Offset: 0x0007C02C
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
			/// <returns>The index value from the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> that is stored at the specified location.</returns>
			/// <param name="index">The zero-based index of the element to get.</param>
			// Token: 0x17000849 RID: 2121
			// (get) Token: 0x06002199 RID: 8601 RVA: 0x0007DE34 File Offset: 0x0007C034
			// (set) Token: 0x0600219A RID: 8602 RVA: 0x0007DE44 File Offset: 0x0007C044
			object IList.Item
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x0600219B RID: 8603 RVA: 0x0007DE4C File Offset: 0x0007C04C
			internal void OnUIACollectionChangedEvent(CollectionChangeEventArgs args)
			{
				CollectionChangeEventHandler collectionChangeEventHandler = (CollectionChangeEventHandler)this.owner.Events[ListBox.SelectedIndexCollection.UIACollectionChangedEvent];
				if (collectionChangeEventHandler != null)
				{
					collectionChangeEventHandler.Invoke(this.owner, args);
				}
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x1700084A RID: 2122
			// (get) Token: 0x0600219C RID: 8604 RVA: 0x0007DE88 File Offset: 0x0007C088
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.selection.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x1700084B RID: 2123
			// (get) Token: 0x0600219D RID: 8605 RVA: 0x0007DE98 File Offset: 0x0007C098
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets the index value at the specified index within this collection.</summary>
			/// <returns>The index value from the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> that is stored at the specified location.</returns>
			/// <param name="index">The index of the item in the collection to get. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListBox.SelectedIndexCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" /> class. </exception>
			// Token: 0x1700084C RID: 2124
			public int this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("Index of out range");
					}
					this.CheckSorted();
					return (int)this.selection[index];
				}
			}

			/// <summary>Adds the <see cref="T:System.Windows.Forms.ListBox" /> at the specified index location.</summary>
			/// <param name="index">The location in the array at which to add the <see cref="T:System.Windows.Forms.ListBox" />.</param>
			// Token: 0x0600219F RID: 8607 RVA: 0x0007DED4 File Offset: 0x0007C0D4
			public void Add(int index)
			{
				if (this.AddCore(index))
				{
					this.owner.OnSelectedIndexChanged(EventArgs.Empty);
					this.owner.OnSelectedValueChanged(EventArgs.Empty);
				}
			}

			// Token: 0x060021A0 RID: 8608 RVA: 0x0007DF10 File Offset: 0x0007C110
			internal bool AddCore(int index)
			{
				if (this.selection.Contains(index))
				{
					return false;
				}
				if (index == -1)
				{
					return false;
				}
				if (index < -1 || index >= this.owner.Items.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this.owner.selection_mode == SelectionMode.None)
				{
					throw new InvalidOperationException("Cannot call this method when selection mode is SelectionMode.None");
				}
				if (this.owner.selection_mode == SelectionMode.One && this.Count > 0)
				{
					this.RemoveCore((int)this.selection[0]);
				}
				this.selection.Add(index);
				this.sorting_needed = true;
				this.owner.EnsureVisible(index);
				this.owner.FocusedItem = index;
				this.owner.InvalidateItem(index);
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(1, index));
				return true;
			}

			/// <summary>Removes all controls from the collection.</summary>
			// Token: 0x060021A1 RID: 8609 RVA: 0x0007E008 File Offset: 0x0007C208
			public void Clear()
			{
				if (this.ClearCore())
				{
					this.owner.OnSelectedIndexChanged(EventArgs.Empty);
					this.owner.OnSelectedValueChanged(EventArgs.Empty);
				}
			}

			// Token: 0x060021A2 RID: 8610 RVA: 0x0007E038 File Offset: 0x0007C238
			internal bool ClearCore()
			{
				if (this.selection.Count == 0)
				{
					return false;
				}
				foreach (object obj in this.selection)
				{
					int num = (int)obj;
					this.owner.InvalidateItem(num);
				}
				this.selection.Clear();
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(3, -1));
				return true;
			}

			/// <summary>Determines whether the specified index is located within the collection.</summary>
			/// <returns>true if the specified index from the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> for the <see cref="T:System.Windows.Forms.ListBox" /> is an item in this collection; otherwise, false.</returns>
			/// <param name="selectedIndex">The index to locate in the collection. </param>
			// Token: 0x060021A3 RID: 8611 RVA: 0x0007E0DC File Offset: 0x0007C2DC
			public bool Contains(int selectedIndex)
			{
				foreach (object obj in this.selection)
				{
					int num = (int)obj;
					if (num == selectedIndex)
					{
						return true;
					}
				}
				return false;
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="destination">The destination array. </param>
			/// <param name="index">The index in the destination array at which storing begins. </param>
			// Token: 0x060021A4 RID: 8612 RVA: 0x0007E158 File Offset: 0x0007C358
			public void CopyTo(Array destination, int index)
			{
				this.CheckSorted();
				this.selection.CopyTo(destination, index);
			}

			/// <summary>Returns an enumerator to use to iterate through the selected indexes collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the selected indexes collection.</returns>
			// Token: 0x060021A5 RID: 8613 RVA: 0x0007E17C File Offset: 0x0007C37C
			public IEnumerator GetEnumerator()
			{
				this.CheckSorted();
				return this.selection.GetEnumerator();
			}

			/// <summary>Removes the specified control from the collection.</summary>
			/// <param name="index">The control to be removed.</param>
			// Token: 0x060021A6 RID: 8614 RVA: 0x0007E190 File Offset: 0x0007C390
			public void Remove(int index)
			{
				if (this.RemoveCore(index))
				{
					this.owner.OnSelectedIndexChanged(EventArgs.Empty);
					this.owner.OnSelectedValueChanged(EventArgs.Empty);
				}
			}

			// Token: 0x060021A7 RID: 8615 RVA: 0x0007E1CC File Offset: 0x0007C3CC
			internal bool RemoveCore(int index)
			{
				int num = this.IndexOf(index);
				if (num == -1)
				{
					return false;
				}
				this.selection.RemoveAt(num);
				this.owner.InvalidateItem(index);
				this.OnUIACollectionChangedEvent(new CollectionChangeEventArgs(2, index));
				return true;
			}

			/// <summary>Returns the index within the <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" /> of the specified index from the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> of the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
			/// <returns>The zero-based index in the collection where the specified index of the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> was located within the <see cref="T:System.Windows.Forms.ListBox.SelectedIndexCollection" />; otherwise, negative one (-1).</returns>
			/// <param name="selectedIndex">The zero-based index from the <see cref="T:System.Windows.Forms.ListBox.ObjectCollection" /> to locate in this collection. </param>
			// Token: 0x060021A8 RID: 8616 RVA: 0x0007E218 File Offset: 0x0007C418
			public int IndexOf(int selectedIndex)
			{
				this.CheckSorted();
				for (int i = 0; i < this.selection.Count; i++)
				{
					if ((int)this.selection[i] == selectedIndex)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x1700084D RID: 2125
			// (get) Token: 0x060021A9 RID: 8617 RVA: 0x0007E264 File Offset: 0x0007C464
			internal ArrayList List
			{
				get
				{
					this.CheckSorted();
					return this.selection;
				}
			}

			// Token: 0x060021AA RID: 8618 RVA: 0x0007E274 File Offset: 0x0007C474
			private void CheckSorted()
			{
				if (this.sorting_needed)
				{
					this.sorting_needed = false;
					this.selection.Sort();
				}
			}

			// Token: 0x040011D8 RID: 4568
			private ListBox owner;

			// Token: 0x040011D9 RID: 4569
			private ArrayList selection;

			// Token: 0x040011DA RID: 4570
			private bool sorting_needed;
		}

		/// <summary>Represents the collection of selected items in the <see cref="T:System.Windows.Forms.ListBox" />.</summary>
		// Token: 0x02000219 RID: 537
		public class SelectedObjectCollection : ICollection, IEnumerable, IList
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListBox.SelectedObjectCollection" /> class.</summary>
			/// <param name="owner">A <see cref="T:System.Windows.Forms.ListBox" /> representing the owner of the collection. </param>
			// Token: 0x060021AB RID: 8619 RVA: 0x0007E294 File Offset: 0x0007C494
			public SelectedObjectCollection(ListBox owner)
			{
				this.owner = owner;
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>true if the list is synchronized; otherwise false.</returns>
			// Token: 0x1700084E RID: 2126
			// (get) Token: 0x060021AC RID: 8620 RVA: 0x0007E2A4 File Offset: 0x0007C4A4
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>An object that can be used to synchronize access to the underlying list.</returns>
			// Token: 0x1700084F RID: 2127
			// (get) Token: 0x060021AD RID: 8621 RVA: 0x0007E2A8 File Offset: 0x0007C4A8
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
			/// <returns>true if the underlying list has a fixed size; otherwise false.</returns>
			// Token: 0x17000850 RID: 2128
			// (get) Token: 0x060021AE RID: 8622 RVA: 0x0007E2AC File Offset: 0x0007C4AC
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			/// <returns>The position into which the object was inserted.</returns>
			/// <param name="value">The object to add to the collection.</param>
			// Token: 0x060021AF RID: 8623 RVA: 0x0007E2B0 File Offset: 0x0007C4B0
			int IList.Add(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
			// Token: 0x060021B0 RID: 8624 RVA: 0x0007E2B8 File Offset: 0x0007C4B8
			void IList.Clear()
			{
				throw new NotSupportedException();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
			/// <param name="index">The zero-based index at which the object should be inserted.</param>
			/// <param name="value">The object to insert into the <see cref="T:System.Windows.Forms.ListBox.SelectedObjectCollection" />.</param>
			// Token: 0x060021B1 RID: 8625 RVA: 0x0007E2C0 File Offset: 0x0007C4C0
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
			/// <param name="value">The object to remove.</param>
			// Token: 0x060021B2 RID: 8626 RVA: 0x0007E2C8 File Offset: 0x0007C4C8
			void IList.Remove(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
			/// <param name="index">The zero-based index of the object to remove from the <see cref="T:System.Windows.Forms.ListBox.SelectedObjectCollection" />.</param>
			// Token: 0x060021B3 RID: 8627 RVA: 0x0007E2D0 File Offset: 0x0007C4D0
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of items in the collection.</returns>
			// Token: 0x17000851 RID: 2129
			// (get) Token: 0x060021B4 RID: 8628 RVA: 0x0007E2D8 File Offset: 0x0007C4D8
			public int Count
			{
				get
				{
					return this.owner.selected_indices.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x17000852 RID: 2130
			// (get) Token: 0x060021B5 RID: 8629 RVA: 0x0007E2EC File Offset: 0x0007C4EC
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets the item at the specified index within the collection.</summary>
			/// <returns>An object representing the item located at the specified index within the collection.</returns>
			/// <param name="index">The index of the item in the collection to retrieve. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.ListBox.ObjectCollection.Count" /> property of the <see cref="T:System.Windows.Forms.ListBox.SelectedObjectCollection" /> class. </exception>
			// Token: 0x17000853 RID: 2131
			[DesignerSerializationVisibility(0)]
			[Browsable(false)]
			public object this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("Index of out range");
					}
					return this.owner.items[this.owner.selected_indices[index]];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			/// <summary>Adds an item to the list of selected items for a <see cref="T:System.Windows.Forms.ListBox" />.</summary>
			/// <param name="value">An object representing the item to add to the collection of selected items.</param>
			// Token: 0x060021B8 RID: 8632 RVA: 0x0007E344 File Offset: 0x0007C544
			public void Add(object value)
			{
				if (this.owner.selection_mode == SelectionMode.None)
				{
					throw new ArgumentException("Cannot call this method if SelectionMode is SelectionMode.None");
				}
				int num = this.owner.items.IndexOf(value);
				if (num == -1)
				{
					return;
				}
				this.owner.selected_indices.Add(num);
			}

			/// <summary>Removes all items from the collection of selected items.</summary>
			// Token: 0x060021B9 RID: 8633 RVA: 0x0007E398 File Offset: 0x0007C598
			public void Clear()
			{
				this.owner.selected_indices.Clear();
			}

			/// <summary>Determines whether the specified item is located within the collection.</summary>
			/// <returns>true if the specified item is located in the collection; otherwise, false.</returns>
			/// <param name="selectedObject">An object representing the item to locate in the collection. </param>
			// Token: 0x060021BA RID: 8634 RVA: 0x0007E3AC File Offset: 0x0007C5AC
			public bool Contains(object selectedObject)
			{
				int num = this.owner.items.IndexOf(selectedObject);
				return num != -1 && this.owner.selected_indices.Contains(num);
			}

			/// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
			/// <param name="destination">An <see cref="T:System.Array" /> representing the array to copy the contents of the collection to. </param>
			/// <param name="index">The location within the destination array to copy the items from the collection to. </param>
			// Token: 0x060021BB RID: 8635 RVA: 0x0007E3EC File Offset: 0x0007C5EC
			public void CopyTo(Array destination, int index)
			{
				for (int i = 0; i < this.Count; i++)
				{
					destination.SetValue(this[i], index++);
				}
			}

			/// <summary>Removes the specified object from the collection of selected items.</summary>
			/// <param name="value">An object representing the item to remove from the collection.</param>
			// Token: 0x060021BC RID: 8636 RVA: 0x0007E428 File Offset: 0x0007C628
			public void Remove(object value)
			{
				if (value == null)
				{
					return;
				}
				int num = this.owner.items.IndexOf(value);
				if (num == -1)
				{
					return;
				}
				this.owner.selected_indices.Remove(num);
			}

			/// <summary>Returns the index within the collection of the specified item.</summary>
			/// <returns>The zero-based index of the item in the collection; otherwise, -1.</returns>
			/// <param name="selectedObject">An object representing the item to locate in the collection. </param>
			// Token: 0x060021BD RID: 8637 RVA: 0x0007E468 File Offset: 0x0007C668
			public int IndexOf(object selectedObject)
			{
				int num = this.owner.items.IndexOf(selectedObject);
				return (num != -1) ? this.owner.selected_indices.IndexOf(num) : (-1);
			}

			/// <summary>Returns an enumerator that can be used to iterate through the selected item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the item collection.</returns>
			// Token: 0x060021BE RID: 8638 RVA: 0x0007E4A8 File Offset: 0x0007C6A8
			public IEnumerator GetEnumerator()
			{
				object[] array = new object[this.Count];
				for (int i = 0; i < this.Count; i++)
				{
					array[i] = this.owner.items[this.owner.selected_indices[i]];
				}
				return array.GetEnumerator();
			}

			// Token: 0x040011DC RID: 4572
			private ListBox owner;
		}
	}
}
