using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Implements the basic functionality required by a spin box (also known as an up-down control).</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200039F RID: 927
	[ComVisible(true)]
	[Designer("System.Windows.Forms.Design.UpDownBaseDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ClassInterface(1)]
	public abstract class UpDownBase : ContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.UpDownBase" /> class.</summary>
		// Token: 0x0600438D RID: 17293 RVA: 0x0010ADB4 File Offset: 0x00108FB4
		public UpDownBase()
		{
			this._UpDownAlign = LeftRightAlignment.Right;
			base.InternalBorderStyle = BorderStyle.Fixed3D;
			this.spnSpinner = new UpDownBase.UpDownSpinner(this);
			this.txtView = new UpDownBase.UpDownTextBox(this);
			this.txtView.ModifiedChanged += new EventHandler(this.OnChanged);
			this.txtView.AcceptsReturn = true;
			this.txtView.AutoSize = false;
			this.txtView.BorderStyle = BorderStyle.None;
			this.txtView.Location = new Point(17, 17);
			this.txtView.TabIndex = base.TabIndex;
			this.spnSpinner.Width = 16;
			this.spnSpinner.Dock = DockStyle.Right;
			this.txtView.Dock = DockStyle.Fill;
			base.SuspendLayout();
			base.Controls.Add(this.txtView);
			base.Controls.Add(this.spnSpinner);
			base.ResumeLayout();
			base.Height = this.PreferredHeight;
			base.BackColor = this.txtView.BackColor;
			base.TabIndexChanged += new EventHandler(this.TabIndexChangedHandler);
			this.txtView.KeyDown += this.OnTextBoxKeyDown;
			this.txtView.KeyPress += this.OnTextBoxKeyPress;
			this.txtView.Resize += new EventHandler(this.OnTextBoxResize);
			this.txtView.TextChanged += new EventHandler(this.OnTextBoxTextChanged);
			this.auto_select_child = false;
			base.SetStyle(ControlStyles.FixedHeight, true);
			base.SetStyle(ControlStyles.Selectable, true);
			base.SetStyle(ControlStyles.Opaque | ControlStyles.ResizeRedraw, true);
			base.SetStyle(ControlStyles.StandardClick | ControlStyles.UseTextForAccessibility, false);
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x0010AF68 File Offset: 0x00109168
		// Note: this type is marked as 'beforefieldinit'.
		static UpDownBase()
		{
			UpDownBase.UIAUpButtonClickEvent = new object();
			UpDownBase.UIADownButtonClickEvent = new object();
		}

		// Token: 0x1400042C RID: 1068
		// (add) Token: 0x0600438F RID: 17295 RVA: 0x0010AF80 File Offset: 0x00109180
		// (remove) Token: 0x06004390 RID: 17296 RVA: 0x0010AF94 File Offset: 0x00109194
		internal event EventHandler UIAUpButtonClick
		{
			add
			{
				base.Events.AddHandler(UpDownBase.UIAUpButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(UpDownBase.UIAUpButtonClickEvent, value);
			}
		}

		// Token: 0x1400042D RID: 1069
		// (add) Token: 0x06004391 RID: 17297 RVA: 0x0010AFA8 File Offset: 0x001091A8
		// (remove) Token: 0x06004392 RID: 17298 RVA: 0x0010AFBC File Offset: 0x001091BC
		internal event EventHandler UIADownButtonClick
		{
			add
			{
				base.Events.AddHandler(UpDownBase.UIADownButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(UpDownBase.UIADownButtonClickEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.UpDownBase.AutoSize" /> property changes.</summary>
		// Token: 0x1400042E RID: 1070
		// (add) Token: 0x06004393 RID: 17299 RVA: 0x0010AFD0 File Offset: 0x001091D0
		// (remove) Token: 0x06004394 RID: 17300 RVA: 0x0010AFDC File Offset: 0x001091DC
		[Browsable(true)]
		[EditorBrowsable(0)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.UpDownBase.BackgroundImage" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400042F RID: 1071
		// (add) Token: 0x06004395 RID: 17301 RVA: 0x0010AFE8 File Offset: 0x001091E8
		// (remove) Token: 0x06004396 RID: 17302 RVA: 0x0010AFF4 File Offset: 0x001091F4
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.UpDownBase.BackgroundImageLayout" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000430 RID: 1072
		// (add) Token: 0x06004397 RID: 17303 RVA: 0x0010B000 File Offset: 0x00109200
		// (remove) Token: 0x06004398 RID: 17304 RVA: 0x0010B00C File Offset: 0x0010920C
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the mouse pointer enters the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000431 RID: 1073
		// (add) Token: 0x06004399 RID: 17305 RVA: 0x0010B018 File Offset: 0x00109218
		// (remove) Token: 0x0600439A RID: 17306 RVA: 0x0010B024 File Offset: 0x00109224
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler MouseEnter
		{
			add
			{
				base.MouseEnter += value;
			}
			remove
			{
				base.MouseEnter -= value;
			}
		}

		/// <summary>Occurs when the mouse pointer rests on the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000432 RID: 1074
		// (add) Token: 0x0600439B RID: 17307 RVA: 0x0010B030 File Offset: 0x00109230
		// (remove) Token: 0x0600439C RID: 17308 RVA: 0x0010B03C File Offset: 0x0010923C
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler MouseHover
		{
			add
			{
				base.MouseHover += value;
			}
			remove
			{
				base.MouseHover -= value;
			}
		}

		/// <summary>Occurs when the mouse pointer leaves the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000433 RID: 1075
		// (add) Token: 0x0600439D RID: 17309 RVA: 0x0010B048 File Offset: 0x00109248
		// (remove) Token: 0x0600439E RID: 17310 RVA: 0x0010B054 File Offset: 0x00109254
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler MouseLeave
		{
			add
			{
				base.MouseLeave += value;
			}
			remove
			{
				base.MouseLeave -= value;
			}
		}

		/// <summary>Occurs when the user moves the mouse pointer over the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000434 RID: 1076
		// (add) Token: 0x0600439F RID: 17311 RVA: 0x0010B060 File Offset: 0x00109260
		// (remove) Token: 0x060043A0 RID: 17312 RVA: 0x0010B06C File Offset: 0x0010926C
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event MouseEventHandler MouseMove
		{
			add
			{
				base.MouseMove += value;
			}
			remove
			{
				base.MouseMove -= value;
			}
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x0010B078 File Offset: 0x00109278
		internal void OnUIAUpButtonClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[UpDownBase.UIAUpButtonClickEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x0010B0AC File Offset: 0x001092AC
		internal void OnUIADownButtonClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[UpDownBase.UIADownButtonClickEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x0010B0E0 File Offset: 0x001092E0
		private void TabIndexChangedHandler(object sender, EventArgs e)
		{
			this.txtView.TabIndex = base.TabIndex;
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x0010B0F4 File Offset: 0x001092F4
		internal override void OnPaintInternal(PaintEventArgs e)
		{
			e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.BackColor), base.ClientRectangle);
		}

		/// <summary>Gets a value indicating whether the container will allow the user to scroll to any controls placed outside of its visible boundaries.</summary>
		/// <returns>false in all cases.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x060043A5 RID: 17317 RVA: 0x0010B128 File Offset: 0x00109328
		// (set) Token: 0x060043A6 RID: 17318 RVA: 0x0010B130 File Offset: 0x00109330
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
			set
			{
				base.AutoScroll = value;
			}
		}

		/// <summary>Gets or sets the size of the auto-scroll margin.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the height and width, in pixels, of the auto-scroll margin.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Size.Height" /> or <see cref="P:System.Drawing.Size.Width" /> is less than 0.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x060043A7 RID: 17319 RVA: 0x0010B13C File Offset: 0x0010933C
		// (set) Token: 0x060043A8 RID: 17320 RVA: 0x0010B144 File Offset: 0x00109344
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new Size AutoScrollMargin
		{
			get
			{
				return base.AutoScrollMargin;
			}
			set
			{
				base.AutoScrollMargin = value;
			}
		}

		/// <summary>Gets or sets the minimum size of the auto-scroll area.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the minimum height and width, in pixels, of the scroll bars.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x060043A9 RID: 17321 RVA: 0x0010B150 File Offset: 0x00109350
		// (set) Token: 0x060043AA RID: 17322 RVA: 0x0010B158 File Offset: 0x00109358
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new Size AutoScrollMinSize
		{
			get
			{
				return base.AutoScrollMinSize;
			}
			set
			{
				base.AutoScrollMinSize = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control should automatically resize based on its contents.</summary>
		/// <returns>true to indicate the control should automatically resize based on its contents; otherwise, false.</returns>
		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x060043AB RID: 17323 RVA: 0x0010B164 File Offset: 0x00109364
		// (set) Token: 0x060043AC RID: 17324 RVA: 0x0010B16C File Offset: 0x0010936C
		[DesignerSerializationVisibility(1)]
		[EditorBrowsable(0)]
		[Browsable(true)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		/// <summary>Gets or sets the background color for the text box portion of the spin box (also known as an up-down control).</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the text box portion of the spin box.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x060043AD RID: 17325 RVA: 0x0010B178 File Offset: 0x00109378
		// (set) Token: 0x060043AE RID: 17326 RVA: 0x0010B180 File Offset: 0x00109380
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
				this.txtView.BackColor = value;
			}
		}

		/// <summary>Gets or sets the background image for the <see cref="T:System.Windows.Forms.UpDownBase" />.</summary>
		/// <returns>The background image for the <see cref="T:System.Windows.Forms.UpDownBase" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x060043AF RID: 17327 RVA: 0x0010B198 File Offset: 0x00109398
		// (set) Token: 0x060043B0 RID: 17328 RVA: 0x0010B1A0 File Offset: 0x001093A0
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
				this.txtView.BackgroundImage = value;
			}
		}

		/// <summary>Gets or sets the layout of the <see cref="P:System.Windows.Forms.UpDownBase.BackgroundImage" /> of the <see cref="T:System.Windows.Forms.UpDownBase" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x060043B1 RID: 17329 RVA: 0x0010B1B8 File Offset: 0x001093B8
		// (set) Token: 0x060043B2 RID: 17330 RVA: 0x0010B1C0 File Offset: 0x001093C0
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
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

		/// <summary>Gets or sets the border style for the spin box (also known as an up-down control).</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default value is <see cref="F:System.Windows.Forms.BorderStyle.Fixed3D" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x060043B3 RID: 17331 RVA: 0x0010B1CC File Offset: 0x001093CC
		// (set) Token: 0x060043B4 RID: 17332 RVA: 0x0010B1D4 File Offset: 0x001093D4
		[DefaultValue(BorderStyle.Fixed3D)]
		[DispId(-504)]
		public BorderStyle BorderStyle
		{
			get
			{
				return base.InternalBorderStyle;
			}
			set
			{
				base.InternalBorderStyle = value;
			}
		}

		/// <summary>Gets or sets the shortcut menu associated with the spin box (also known as an up-down control).</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenu" /> associated with the spin box.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x060043B5 RID: 17333 RVA: 0x0010B1E0 File Offset: 0x001093E0
		// (set) Token: 0x060043B6 RID: 17334 RVA: 0x0010B1E8 File Offset: 0x001093E8
		public override ContextMenu ContextMenu
		{
			get
			{
				return base.ContextMenu;
			}
			set
			{
				base.ContextMenu = value;
				this.txtView.ContextMenu = value;
				this.spnSpinner.ContextMenu = value;
			}
		}

		/// <summary>Gets or sets the shortcut menu for the spin box (also known as an up-down control).</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the control.</returns>
		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x060043B7 RID: 17335 RVA: 0x0010B20C File Offset: 0x0010940C
		// (set) Token: 0x060043B8 RID: 17336 RVA: 0x0010B214 File Offset: 0x00109414
		public override ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return base.ContextMenuStrip;
			}
			set
			{
				base.ContextMenuStrip = value;
				this.txtView.ContextMenuStrip = value;
				this.spnSpinner.ContextMenuStrip = value;
			}
		}

		/// <summary>Gets the dock padding settings for all edges of the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x060043B9 RID: 17337 RVA: 0x0010B238 File Offset: 0x00109438
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new ScrollableControl.DockPaddingEdges DockPadding
		{
			get
			{
				return base.DockPadding;
			}
		}

		/// <summary>Returns true if this control has focus.</summary>
		/// <returns>true if the control has focus; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x060043BA RID: 17338 RVA: 0x0010B240 File Offset: 0x00109440
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public override bool Focused
		{
			get
			{
				return this.txtView.Focused;
			}
		}

		/// <summary>Gets or sets the foreground color of the spin box (also known as an up-down control).</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the spin box.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x060043BB RID: 17339 RVA: 0x0010B250 File Offset: 0x00109450
		// (set) Token: 0x060043BC RID: 17340 RVA: 0x0010B258 File Offset: 0x00109458
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
				this.txtView.ForeColor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can use the UP ARROW and DOWN ARROW keys to select values.</summary>
		/// <returns>true if the control allows the use of the UP ARROW and DOWN ARROW keys to select values; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x060043BD RID: 17341 RVA: 0x0010B270 File Offset: 0x00109470
		// (set) Token: 0x060043BE RID: 17342 RVA: 0x0010B278 File Offset: 0x00109478
		[DefaultValue(true)]
		public bool InterceptArrowKeys
		{
			get
			{
				return this._InterceptArrowKeys;
			}
			set
			{
				this._InterceptArrowKeys = value;
			}
		}

		/// <summary>Gets or sets the maximum size of the spin box (also known as an up-down control).</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" />, which is the maximum size of the spin box.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x060043BF RID: 17343 RVA: 0x0010B284 File Offset: 0x00109484
		// (set) Token: 0x060043C0 RID: 17344 RVA: 0x0010B28C File Offset: 0x0010948C
		public override Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
			set
			{
				base.MaximumSize = new Size(value.Width, 0);
			}
		}

		/// <summary>Gets or sets the minimum size of the spin box (also known as an up-down control).</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" />, which is the minimum size of the spin box.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x060043C1 RID: 17345 RVA: 0x0010B2A4 File Offset: 0x001094A4
		// (set) Token: 0x060043C2 RID: 17346 RVA: 0x0010B2AC File Offset: 0x001094AC
		public override Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = new Size(value.Width, 0);
			}
		}

		/// <summary>Gets the height of the spin box (also known as an up-down control).</summary>
		/// <returns>The height, in pixels, of the spin box.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x060043C3 RID: 17347 RVA: 0x0010B2C4 File Offset: 0x001094C4
		[EditorBrowsable(2)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int PreferredHeight
		{
			get
			{
				int num = this.Font.Height;
				switch (this.border_style)
				{
				case BorderStyle.FixedSingle:
				case BorderStyle.Fixed3D:
					num += 3;
					return num + 4;
				}
				return num;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text can be changed by the use of the up or down buttons only.</summary>
		/// <returns>true if the text can be changed by the use of the up or down buttons only; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x060043C4 RID: 17348 RVA: 0x0010B304 File Offset: 0x00109504
		// (set) Token: 0x060043C5 RID: 17349 RVA: 0x0010B314 File Offset: 0x00109514
		[DefaultValue(false)]
		public bool ReadOnly
		{
			get
			{
				return this.txtView.ReadOnly;
			}
			set
			{
				this.txtView.ReadOnly = value;
			}
		}

		/// <summary>Gets or sets the text displayed in the spin box (also known as an up-down control).</summary>
		/// <returns>The string value displayed in the spin box.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x060043C6 RID: 17350 RVA: 0x0010B324 File Offset: 0x00109524
		// (set) Token: 0x060043C7 RID: 17351 RVA: 0x0010B344 File Offset: 0x00109544
		[Localizable(true)]
		public override string Text
		{
			get
			{
				if (this.txtView != null)
				{
					return this.txtView.Text;
				}
				return string.Empty;
			}
			set
			{
				this.txtView.Text = value;
				if (this.UserEdit)
				{
					this.ValidateEditText();
				}
				this.txtView.SelectionLength = 0;
			}
		}

		/// <summary>Gets or sets the alignment of the text in the spin box (also known as an up-down control).</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. The default value is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x060043C8 RID: 17352 RVA: 0x0010B37C File Offset: 0x0010957C
		// (set) Token: 0x060043C9 RID: 17353 RVA: 0x0010B38C File Offset: 0x0010958C
		[Localizable(true)]
		[DefaultValue(HorizontalAlignment.Left)]
		public HorizontalAlignment TextAlign
		{
			get
			{
				return this.txtView.TextAlign;
			}
			set
			{
				this.txtView.TextAlign = value;
			}
		}

		/// <summary>Gets or sets the alignment of the up and down buttons on the spin box (also known as an up-down control).</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values. The default value is <see cref="F:System.Windows.Forms.LeftRightAlignment.Right" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x060043CA RID: 17354 RVA: 0x0010B39C File Offset: 0x0010959C
		// (set) Token: 0x060043CB RID: 17355 RVA: 0x0010B3A4 File Offset: 0x001095A4
		[Localizable(true)]
		[DefaultValue(LeftRightAlignment.Right)]
		public LeftRightAlignment UpDownAlign
		{
			get
			{
				return this._UpDownAlign;
			}
			set
			{
				if (this._UpDownAlign != value)
				{
					this._UpDownAlign = value;
					if (value == LeftRightAlignment.Left)
					{
						this.spnSpinner.Dock = DockStyle.Left;
					}
					else
					{
						this.spnSpinner.Dock = DockStyle.Right;
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the text property is being changed internally by its parent class.</summary>
		/// <returns>true if the <see cref="P:System.Windows.Forms.UpDownBase.Text" /> property is being changed internally by the <see cref="T:System.Windows.Forms.UpDownBase" /> class; otherwise, false.</returns>
		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x060043CC RID: 17356 RVA: 0x0010B3E8 File Offset: 0x001095E8
		// (set) Token: 0x060043CD RID: 17357 RVA: 0x0010B3F0 File Offset: 0x001095F0
		protected bool ChangingText
		{
			get
			{
				return this.changing_text;
			}
			set
			{
				this.changing_text = value;
			}
		}

		/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.CreateParams" /> property.</summary>
		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x060043CE RID: 17358 RVA: 0x0010B3FC File Offset: 0x001095FC
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x060043CF RID: 17359 RVA: 0x0010B404 File Offset: 0x00109604
		protected override Size DefaultSize
		{
			get
			{
				return new Size(120, this.PreferredHeight);
			}
		}

		/// <summary>Gets or sets a value indicating whether a value has been entered by the user.</summary>
		/// <returns>true if the user has changed the <see cref="P:System.Windows.Forms.UpDownBase.Text" /> property; otherwise, false.</returns>
		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x060043D0 RID: 17360 RVA: 0x0010B414 File Offset: 0x00109614
		// (set) Token: 0x060043D1 RID: 17361 RVA: 0x0010B41C File Offset: 0x0010961C
		protected bool UserEdit
		{
			get
			{
				return this.user_edit;
			}
			set
			{
				this.user_edit = value;
			}
		}

		/// <summary>When overridden in a derived class, handles the clicking of the down button on the spin box (also known as an up-down control).</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060043D2 RID: 17362
		public abstract void DownButton();

		/// <summary>Selects a range of text in the spin box (also known as an up-down control) specifying the starting position and number of characters to select.</summary>
		/// <param name="start">The position of the first character to be selected. </param>
		/// <param name="length">The total number of characters to be selected. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060043D3 RID: 17363 RVA: 0x0010B428 File Offset: 0x00109628
		public void Select(int start, int length)
		{
			this.txtView.Select(start, length);
		}

		/// <summary>When overridden in a derived class, handles the clicking of the up button on the spin box (also known as an up-down control).</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060043D4 RID: 17364
		public abstract void UpButton();

		/// <summary>When overridden in a derived class, raises the Changed event.</summary>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060043D5 RID: 17365 RVA: 0x0010B438 File Offset: 0x00109638
		protected virtual void OnChanged(object source, EventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060043D6 RID: 17366 RVA: 0x0010B43C File Offset: 0x0010963C
		protected override void OnFontChanged(EventArgs e)
		{
			this.txtView.Font = this.Font;
			base.Height = this.PreferredHeight;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060043D7 RID: 17367 RVA: 0x0010B468 File Offset: 0x00109668
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060043D8 RID: 17368 RVA: 0x0010B474 File Offset: 0x00109674
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x060043D9 RID: 17369 RVA: 0x0010B480 File Offset: 0x00109680
		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060043DA RID: 17370 RVA: 0x0010B48C File Offset: 0x0010968C
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event. </summary>
		/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060043DB RID: 17371 RVA: 0x0010B498 File Offset: 0x00109698
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			base.OnMouseUp(mevent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060043DC RID: 17372 RVA: 0x0010B4A4 File Offset: 0x001096A4
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (e.Delta > 0)
			{
				this.UpButton();
			}
			else if (e.Delta < 0)
			{
				this.DownButton();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" />  that contains the event data. </param>
		// Token: 0x060043DD RID: 17373 RVA: 0x0010B4D0 File Offset: 0x001096D0
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
		/// <param name="source">The source of the event. </param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x060043DE RID: 17374 RVA: 0x0010B4DC File Offset: 0x001096DC
		protected virtual void OnTextBoxKeyDown(object source, KeyEventArgs e)
		{
			if (this._InterceptArrowKeys && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
			{
				e.Handled = true;
				if (e.KeyCode == Keys.Up)
				{
					this.UpButton();
				}
				if (e.KeyCode == Keys.Down)
				{
					this.DownButton();
				}
			}
			this.OnKeyDown(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="source">The source of the event. </param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data. </param>
		// Token: 0x060043DF RID: 17375 RVA: 0x0010B544 File Offset: 0x00109744
		protected virtual void OnTextBoxKeyPress(object source, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				e.Handled = true;
				this.ValidateEditText();
			}
			this.OnKeyPress(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
		/// <param name="source">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060043E0 RID: 17376 RVA: 0x0010B574 File Offset: 0x00109774
		protected virtual void OnTextBoxLostFocus(object source, EventArgs e)
		{
			if (this.UserEdit)
			{
				this.ValidateEditText();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="source">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060043E1 RID: 17377 RVA: 0x0010B588 File Offset: 0x00109788
		protected virtual void OnTextBoxResize(object source, EventArgs e)
		{
			base.Height = this.PreferredHeight;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.</summary>
		/// <param name="source">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060043E2 RID: 17378 RVA: 0x0010B598 File Offset: 0x00109798
		protected virtual void OnTextBoxTextChanged(object source, EventArgs e)
		{
			if (this.changing_text)
			{
				this.ChangingText = false;
			}
			else
			{
				this.UserEdit = true;
			}
			this.OnTextChanged(e);
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x0010B5C0 File Offset: 0x001097C0
		internal override void SetBoundsCoreInternal(int x, int y, int width, int height, BoundsSpecified specified)
		{
			base.SetBoundsCoreInternal(x, y, width, Math.Min(width, this.PreferredHeight), specified);
		}

		/// <summary>When overridden in a derived class, updates the text displayed in the spin box (also known as an up-down control).</summary>
		// Token: 0x060043E4 RID: 17380
		protected abstract void UpdateEditText();

		/// <summary>When overridden in a derived class, validates the text displayed in the spin box (also known as an up-down control).</summary>
		// Token: 0x060043E5 RID: 17381 RVA: 0x0010B5E4 File Offset: 0x001097E4
		protected virtual void ValidateEditText()
		{
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" /> method.</summary>
		// Token: 0x060043E6 RID: 17382 RVA: 0x0010B5E8 File Offset: 0x001097E8
		[EditorBrowsable(2)]
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			switch (msg)
			{
			case Msg.WM_KEYDOWN:
			case Msg.WM_KEYUP:
			case Msg.WM_CHAR:
				XplatUI.SendMessage(this.txtView.Handle, (Msg)m.Msg, m.WParam, m.LParam);
				break;
			default:
				if (msg != Msg.WM_SETFOCUS)
				{
					if (msg != Msg.WM_KILLFOCUS)
					{
						base.WndProc(ref m);
					}
					else
					{
						this.ActiveControl = null;
					}
				}
				else
				{
					this.ActiveControl = this.txtView;
				}
				break;
			}
		}

		// Token: 0x04001C71 RID: 7281
		internal UpDownBase.UpDownTextBox txtView;

		// Token: 0x04001C72 RID: 7282
		private UpDownBase.UpDownSpinner spnSpinner;

		// Token: 0x04001C73 RID: 7283
		private bool _InterceptArrowKeys = true;

		// Token: 0x04001C74 RID: 7284
		private LeftRightAlignment _UpDownAlign;

		// Token: 0x04001C75 RID: 7285
		private bool changing_text;

		// Token: 0x04001C76 RID: 7286
		private bool user_edit;

		// Token: 0x020003A0 RID: 928
		internal sealed class UpDownSpinner : Control
		{
			// Token: 0x060043E7 RID: 17383 RVA: 0x0010B678 File Offset: 0x00109878
			public UpDownSpinner(UpDownBase owner)
			{
				this.owner = owner;
				this.mouse_pressed = 0;
				base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
				base.SetStyle(ControlStyles.DoubleBuffer, true);
				base.SetStyle(ControlStyles.Opaque, true);
				base.SetStyle(ControlStyles.ResizeRedraw, true);
				base.SetStyle(ControlStyles.UserPaint, true);
				base.SetStyle(ControlStyles.FixedHeight, true);
				base.SetStyle(ControlStyles.Selectable, false);
				this.tmrRepeat = new Timer();
				this.tmrRepeat.Enabled = false;
				this.tmrRepeat.Interval = 10;
				this.tmrRepeat.Tick += new EventHandler(this.tmrRepeat_Tick);
				this.compute_rects();
			}

			// Token: 0x060043E8 RID: 17384 RVA: 0x0010B720 File Offset: 0x00109920
			private void compute_rects()
			{
				int num = base.ClientSize.Height / 2;
				int num2 = base.ClientSize.Height - num;
				this.top_button_rect = new Rectangle(0, 0, base.ClientSize.Width, num);
				this.bottom_button_rect = new Rectangle(0, num, base.ClientSize.Width, num2);
			}

			// Token: 0x060043E9 RID: 17385 RVA: 0x0010B78C File Offset: 0x0010998C
			private void redraw(Graphics graphics)
			{
				PushButtonState pushButtonState = PushButtonState.Normal;
				PushButtonState pushButtonState2 = PushButtonState.Normal;
				if (this.owner.Enabled)
				{
					if (this.mouse_pressed != 0)
					{
						if (this.mouse_pressed == 1 && this.top_button_rect.Contains(this.mouse_x, this.mouse_y))
						{
							pushButtonState = PushButtonState.Pressed;
						}
						if (this.mouse_pressed == 2 && this.bottom_button_rect.Contains(this.mouse_x, this.mouse_y))
						{
							pushButtonState2 = PushButtonState.Pressed;
						}
					}
					else
					{
						if (this.top_button_entered)
						{
							pushButtonState = PushButtonState.Hot;
						}
						if (this.bottom_button_entered)
						{
							pushButtonState2 = PushButtonState.Hot;
						}
					}
				}
				else
				{
					pushButtonState = PushButtonState.Disabled;
					pushButtonState2 = PushButtonState.Disabled;
				}
				ThemeEngine.Current.UpDownBaseDrawButton(graphics, this.top_button_rect, true, pushButtonState);
				ThemeEngine.Current.UpDownBaseDrawButton(graphics, this.bottom_button_rect, false, pushButtonState2);
			}

			// Token: 0x060043EA RID: 17386 RVA: 0x0010B85C File Offset: 0x00109A5C
			private void tmrRepeat_Tick(object sender, EventArgs e)
			{
				if (this.repeat_delay > 1)
				{
					this.repeat_counter++;
					if (this.repeat_counter < this.repeat_delay)
					{
						return;
					}
					this.repeat_counter = 0;
					this.repeat_delay = this.repeat_delay * 3 / 4;
				}
				if (this.mouse_pressed == 0)
				{
					this.tmrRepeat.Enabled = false;
				}
				if (this.mouse_pressed == 1 && this.top_button_rect.Contains(this.mouse_x, this.mouse_y))
				{
					this.owner.UpButton();
				}
				if (this.mouse_pressed == 2 && this.bottom_button_rect.Contains(this.mouse_x, this.mouse_y))
				{
					this.owner.DownButton();
				}
			}

			// Token: 0x060043EB RID: 17387 RVA: 0x0010B92C File Offset: 0x00109B2C
			protected override void OnMouseDown(MouseEventArgs e)
			{
				if (e.Button != MouseButtons.Left)
				{
					return;
				}
				if (this.top_button_rect.Contains(e.X, e.Y))
				{
					this.mouse_pressed = 1;
					this.owner.UpButton();
				}
				else if (this.bottom_button_rect.Contains(e.X, e.Y))
				{
					this.mouse_pressed = 2;
					this.owner.DownButton();
				}
				this.mouse_x = e.X;
				this.mouse_y = e.Y;
				base.Capture = true;
				this.tmrRepeat.Enabled = true;
				this.repeat_counter = 0;
				this.repeat_delay = 50;
				this.Refresh();
			}

			// Token: 0x060043EC RID: 17388 RVA: 0x0010B9EC File Offset: 0x00109BEC
			protected override void OnMouseMove(MouseEventArgs e)
			{
				ButtonState buttonState = ButtonState.Normal;
				if (this.mouse_pressed == 1 && this.top_button_rect.Contains(this.mouse_x, this.mouse_y))
				{
					buttonState = ButtonState.Pushed;
				}
				if (this.mouse_pressed == 2 && this.bottom_button_rect.Contains(this.mouse_x, this.mouse_y))
				{
					buttonState = ButtonState.Pushed;
				}
				this.mouse_x = e.X;
				this.mouse_y = e.Y;
				ButtonState buttonState2 = ButtonState.Normal;
				if (this.mouse_pressed == 1 && this.top_button_rect.Contains(this.mouse_x, this.mouse_y))
				{
					buttonState2 = ButtonState.Pushed;
				}
				if (this.mouse_pressed == 2 && this.bottom_button_rect.Contains(this.mouse_x, this.mouse_y))
				{
					buttonState2 = ButtonState.Pushed;
				}
				bool flag = this.top_button_rect.Contains(e.Location);
				bool flag2 = this.bottom_button_rect.Contains(e.Location);
				if (buttonState != buttonState2)
				{
					if (buttonState2 == ButtonState.Pushed)
					{
						this.tmrRepeat.Enabled = true;
						this.repeat_counter = 0;
						this.repeat_delay = 50;
						if (this.mouse_pressed == 1)
						{
							this.owner.UpButton();
						}
						if (this.mouse_pressed == 2)
						{
							this.owner.DownButton();
						}
					}
					else
					{
						this.tmrRepeat.Enabled = false;
					}
					this.top_button_entered = flag;
					this.bottom_button_entered = flag2;
					this.Refresh();
				}
				else if (ThemeEngine.Current.UpDownBaseHasHotButtonStyle)
				{
					Region region = new Region();
					bool flag3 = false;
					region.MakeEmpty();
					if (this.top_button_entered != flag)
					{
						this.top_button_entered = flag;
						region.Union(this.top_button_rect);
						flag3 = true;
					}
					if (this.bottom_button_entered != flag2)
					{
						this.bottom_button_entered = flag2;
						region.Union(this.bottom_button_rect);
						flag3 = true;
					}
					if (flag3)
					{
						base.Invalidate(region);
					}
					region.Dispose();
				}
				else
				{
					this.top_button_entered = flag;
					this.bottom_button_entered = flag2;
				}
			}

			// Token: 0x060043ED RID: 17389 RVA: 0x0010BC08 File Offset: 0x00109E08
			protected override void OnMouseUp(MouseEventArgs e)
			{
				this.mouse_pressed = 0;
				base.Capture = false;
				this.Refresh();
			}

			// Token: 0x060043EE RID: 17390 RVA: 0x0010BC20 File Offset: 0x00109E20
			protected override void OnMouseWheel(MouseEventArgs e)
			{
				if (e.Delta > 0)
				{
					this.owner.UpButton();
				}
				else if (e.Delta < 0)
				{
					this.owner.DownButton();
				}
			}

			// Token: 0x060043EF RID: 17391 RVA: 0x0010BC58 File Offset: 0x00109E58
			protected override void OnMouseLeave(EventArgs e)
			{
				if (this.top_button_entered)
				{
					this.top_button_entered = false;
					if (ThemeEngine.Current.UpDownBaseHasHotButtonStyle)
					{
						base.Invalidate(this.top_button_rect);
					}
				}
				if (this.bottom_button_entered)
				{
					this.bottom_button_entered = false;
					if (ThemeEngine.Current.UpDownBaseHasHotButtonStyle)
					{
						base.Invalidate(this.bottom_button_rect);
					}
				}
			}

			// Token: 0x060043F0 RID: 17392 RVA: 0x0010BCC0 File Offset: 0x00109EC0
			protected override void OnPaint(PaintEventArgs e)
			{
				this.redraw(e.Graphics);
			}

			// Token: 0x060043F1 RID: 17393 RVA: 0x0010BCD0 File Offset: 0x00109ED0
			protected override void OnResize(EventArgs e)
			{
				base.OnResize(e);
				this.compute_rects();
			}

			// Token: 0x04001C79 RID: 7289
			private const int InitialRepeatDelay = 50;

			// Token: 0x04001C7A RID: 7290
			private UpDownBase owner;

			// Token: 0x04001C7B RID: 7291
			private Timer tmrRepeat;

			// Token: 0x04001C7C RID: 7292
			private Rectangle top_button_rect;

			// Token: 0x04001C7D RID: 7293
			private Rectangle bottom_button_rect;

			// Token: 0x04001C7E RID: 7294
			private int mouse_pressed;

			// Token: 0x04001C7F RID: 7295
			private int mouse_x;

			// Token: 0x04001C80 RID: 7296
			private int mouse_y;

			// Token: 0x04001C81 RID: 7297
			private int repeat_delay;

			// Token: 0x04001C82 RID: 7298
			private int repeat_counter;

			// Token: 0x04001C83 RID: 7299
			private bool top_button_entered;

			// Token: 0x04001C84 RID: 7300
			private bool bottom_button_entered;
		}

		// Token: 0x020003A1 RID: 929
		internal class UpDownTextBox : TextBox
		{
			// Token: 0x060043F2 RID: 17394 RVA: 0x0010BCE0 File Offset: 0x00109EE0
			public UpDownTextBox(UpDownBase owner)
			{
				this.owner = owner;
				base.SetStyle(ControlStyles.FixedWidth, false);
				base.SetStyle(ControlStyles.Selectable, false);
			}

			// Token: 0x060043F3 RID: 17395 RVA: 0x0010BD10 File Offset: 0x00109F10
			protected override void OnGotFocus(EventArgs e)
			{
				base.ShowSelection = true;
				this.owner.OnGotFocus(e);
			}

			// Token: 0x060043F4 RID: 17396 RVA: 0x0010BD28 File Offset: 0x00109F28
			protected override void OnLostFocus(EventArgs e)
			{
				base.ShowSelection = false;
				this.owner.OnLostFocus(e);
			}

			// Token: 0x060043F5 RID: 17397 RVA: 0x0010BD40 File Offset: 0x00109F40
			protected override void OnMouseDown(MouseEventArgs e)
			{
				this.owner.OnMouseDown(e);
				base.OnMouseDown(e);
			}

			// Token: 0x060043F6 RID: 17398 RVA: 0x0010BD58 File Offset: 0x00109F58
			protected override void OnMouseUp(MouseEventArgs e)
			{
				this.owner.OnMouseUp(e);
				base.OnMouseUp(e);
			}

			// Token: 0x04001C85 RID: 7301
			private UpDownBase owner;
		}
	}
}
