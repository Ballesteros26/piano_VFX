using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Implements the basic functionality required by text controls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000312 RID: 786
	[ComVisible(true)]
	[DefaultBindingProperty("Text")]
	[ClassInterface(1)]
	[DefaultEvent("TextChanged")]
	[Designer("System.Windows.Forms.Design.TextBoxBaseDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class TextBoxBase : Control
	{
		// Token: 0x06003442 RID: 13378 RVA: 0x000C6058 File Offset: 0x000C4258
		internal TextBoxBase()
		{
			this.alignment = HorizontalAlignment.Left;
			this.accepts_return = false;
			this.accepts_tab = false;
			this.auto_size = true;
			base.InternalBorderStyle = BorderStyle.Fixed3D;
			this.actual_border_style = BorderStyle.Fixed3D;
			this.character_casing = CharacterCasing.Normal;
			this.hide_selection = true;
			this.max_length = 32767;
			this.password_char = '\0';
			this.read_only = false;
			this.word_wrap = true;
			this.richtext = false;
			this.show_selection = false;
			this.enable_links = false;
			this.list_links = new ArrayList();
			this.current_link = null;
			this.show_caret_w_selection = this is TextBox;
			this.document = new Document(this);
			this.document.WidthChanged += new EventHandler(this.document_WidthChanged);
			this.document.HeightChanged += new EventHandler(this.document_HeightChanged);
			this.document.Wrap = false;
			this.click_last = DateTime.Now;
			this.click_mode = CaretSelection.Position;
			base.MouseDown += this.TextBoxBase_MouseDown;
			base.MouseUp += this.TextBoxBase_MouseUp;
			base.MouseMove += this.TextBoxBase_MouseMove;
			base.SizeChanged += new EventHandler(this.TextBoxBase_SizeChanged);
			base.FontChanged += new EventHandler(this.TextBoxBase_FontOrColorChanged);
			base.ForeColorChanged += new EventHandler(this.TextBoxBase_FontOrColorChanged);
			base.MouseWheel += this.TextBoxBase_MouseWheel;
			base.RightToLeftChanged += new EventHandler(this.TextBoxBase_RightToLeftChanged);
			this.scrollbars = RichTextBoxScrollBars.None;
			this.hscroll = new ImplicitHScrollBar();
			this.hscroll.ValueChanged += new EventHandler(this.hscroll_ValueChanged);
			this.hscroll.SetStyle(ControlStyles.Selectable, false);
			this.hscroll.Enabled = false;
			this.hscroll.Visible = false;
			this.hscroll.Maximum = int.MaxValue;
			this.vscroll = new ImplicitVScrollBar();
			this.vscroll.ValueChanged += new EventHandler(this.vscroll_ValueChanged);
			this.vscroll.SetStyle(ControlStyles.Selectable, false);
			this.vscroll.Enabled = false;
			this.vscroll.Visible = false;
			this.vscroll.Maximum = int.MaxValue;
			base.SuspendLayout();
			base.Controls.AddImplicit(this.hscroll);
			base.Controls.AddImplicit(this.vscroll);
			base.ResumeLayout();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick, false);
			base.SetStyle(ControlStyles.UseTextForAccessibility, false);
			base.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
			this.canvas_width = base.ClientSize.Width;
			this.canvas_height = base.ClientSize.Height;
			this.document.ViewPortWidth = this.canvas_width;
			this.document.ViewPortHeight = this.canvas_height;
			this.Cursor = Cursors.IBeam;
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x000C6350 File Offset: 0x000C4550
		// Note: this type is marked as 'beforefieldinit'.
		static TextBoxBase()
		{
			TextBoxBase.AcceptsTabChangedEvent = new object();
			TextBoxBase.AutoSizeChangedEvent = new object();
			TextBoxBase.BorderStyleChangedEvent = new object();
			TextBoxBase.HideSelectionChangedEvent = new object();
			TextBoxBase.ModifiedChangedEvent = new object();
			TextBoxBase.MultilineChangedEvent = new object();
			TextBoxBase.ReadOnlyChangedEvent = new object();
			TextBoxBase.HScrolledEvent = new object();
			TextBoxBase.VScrolledEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.AcceptsTab" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400032E RID: 814
		// (add) Token: 0x06003444 RID: 13380 RVA: 0x000C63C4 File Offset: 0x000C45C4
		// (remove) Token: 0x06003445 RID: 13381 RVA: 0x000C63D8 File Offset: 0x000C45D8
		public event EventHandler AcceptsTabChanged
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.AcceptsTabChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.AcceptsTabChangedEvent, value);
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x1400032F RID: 815
		// (add) Token: 0x06003446 RID: 13382 RVA: 0x000C63EC File Offset: 0x000C45EC
		// (remove) Token: 0x06003447 RID: 13383 RVA: 0x000C6400 File Offset: 0x000C4600
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.AutoSizeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.AutoSizeChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.BorderStyle" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000330 RID: 816
		// (add) Token: 0x06003448 RID: 13384 RVA: 0x000C6414 File Offset: 0x000C4614
		// (remove) Token: 0x06003449 RID: 13385 RVA: 0x000C6428 File Offset: 0x000C4628
		public event EventHandler BorderStyleChanged
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.BorderStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.BorderStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.HideSelection" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000331 RID: 817
		// (add) Token: 0x0600344A RID: 13386 RVA: 0x000C643C File Offset: 0x000C463C
		// (remove) Token: 0x0600344B RID: 13387 RVA: 0x000C6450 File Offset: 0x000C4650
		public event EventHandler HideSelectionChanged
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.HideSelectionChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.HideSelectionChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.Modified" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000332 RID: 818
		// (add) Token: 0x0600344C RID: 13388 RVA: 0x000C6464 File Offset: 0x000C4664
		// (remove) Token: 0x0600344D RID: 13389 RVA: 0x000C6478 File Offset: 0x000C4678
		public event EventHandler ModifiedChanged
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.ModifiedChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.ModifiedChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.Multiline" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000333 RID: 819
		// (add) Token: 0x0600344E RID: 13390 RVA: 0x000C648C File Offset: 0x000C468C
		// (remove) Token: 0x0600344F RID: 13391 RVA: 0x000C64A0 File Offset: 0x000C46A0
		public event EventHandler MultilineChanged
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.MultilineChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.MultilineChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.ReadOnly" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000334 RID: 820
		// (add) Token: 0x06003450 RID: 13392 RVA: 0x000C64B4 File Offset: 0x000C46B4
		// (remove) Token: 0x06003451 RID: 13393 RVA: 0x000C64C8 File Offset: 0x000C46C8
		public event EventHandler ReadOnlyChanged
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.ReadOnlyChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.ReadOnlyChangedEvent, value);
			}
		}

		// Token: 0x14000335 RID: 821
		// (add) Token: 0x06003452 RID: 13394 RVA: 0x000C64DC File Offset: 0x000C46DC
		// (remove) Token: 0x06003453 RID: 13395 RVA: 0x000C64F0 File Offset: 0x000C46F0
		internal event EventHandler HScrolled
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.HScrolledEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.HScrolledEvent, value);
			}
		}

		// Token: 0x14000336 RID: 822
		// (add) Token: 0x06003454 RID: 13396 RVA: 0x000C6504 File Offset: 0x000C4704
		// (remove) Token: 0x06003455 RID: 13397 RVA: 0x000C6518 File Offset: 0x000C4718
		internal event EventHandler VScrolled
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.VScrolledEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.VScrolledEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.BackgroundImage" /> property changes. This event is not relevant for this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000337 RID: 823
		// (add) Token: 0x06003456 RID: 13398 RVA: 0x000C652C File Offset: 0x000C472C
		// (remove) Token: 0x06003457 RID: 13399 RVA: 0x000C6538 File Offset: 0x000C4738
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.BackgroundImageLayout" /> property changes. This event is not relevant for this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000338 RID: 824
		// (add) Token: 0x06003458 RID: 13400 RVA: 0x000C6544 File Offset: 0x000C4744
		// (remove) Token: 0x06003459 RID: 13401 RVA: 0x000C6550 File Offset: 0x000C4750
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

		/// <summary>Occurs when the control is clicked by the mouse.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000339 RID: 825
		// (add) Token: 0x0600345A RID: 13402 RVA: 0x000C655C File Offset: 0x000C475C
		// (remove) Token: 0x0600345B RID: 13403 RVA: 0x000C6568 File Offset: 0x000C4768
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x1400033A RID: 826
		// (add) Token: 0x0600345C RID: 13404 RVA: 0x000C6574 File Offset: 0x000C4774
		// (remove) Token: 0x0600345D RID: 13405 RVA: 0x000C6580 File Offset: 0x000C4780
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
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

		/// <summary>Occurs when the text box is clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400033B RID: 827
		// (add) Token: 0x0600345E RID: 13406 RVA: 0x000C658C File Offset: 0x000C478C
		// (remove) Token: 0x0600345F RID: 13407 RVA: 0x000C6598 File Offset: 0x000C4798
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

		/// <summary>Occurs when the control is redrawn. This event is not relevant for this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400033C RID: 828
		// (add) Token: 0x06003460 RID: 13408 RVA: 0x000C65A4 File Offset: 0x000C47A4
		// (remove) Token: 0x06003461 RID: 13409 RVA: 0x000C65C0 File Offset: 0x000C47C0
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event PaintEventHandler Paint;

		// Token: 0x06003462 RID: 13410 RVA: 0x000C65DC File Offset: 0x000C47DC
		internal string CaseAdjust(string s)
		{
			if (this.character_casing == CharacterCasing.Normal)
			{
				return s;
			}
			if (this.character_casing == CharacterCasing.Lower)
			{
				return s.ToLower();
			}
			return s.ToUpper();
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x000C6610 File Offset: 0x000C4810
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			return new Size(base.Width, base.Height);
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x000C6624 File Offset: 0x000C4824
		internal override void HandleClick(int clicks, MouseEventArgs me)
		{
			bool style = base.GetStyle(ControlStyles.StandardClick);
			bool style2 = base.GetStyle(ControlStyles.StandardDoubleClick);
			base.SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);
			base.HandleClick(clicks, me);
			if (!style)
			{
				base.SetStyle(ControlStyles.StandardClick, false);
			}
			if (!style2)
			{
				base.SetStyle(ControlStyles.StandardDoubleClick, false);
			}
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x000C6684 File Offset: 0x000C4884
		internal override void PaintControlBackground(PaintEventArgs pevent)
		{
			if (!ThemeEngine.Current.TextBoxBaseShouldPaintBackground(this))
			{
				return;
			}
			base.PaintControlBackground(pevent);
		}

		/// <summary>Gets or sets a value indicating whether pressing the TAB key in a multiline text box control types a TAB character in the control instead of moving the focus to the next control in the tab order.</summary>
		/// <returns>true if users can enter tabs in a multiline text box using the TAB key; false if pressing the TAB key moves the focus. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06003466 RID: 13414 RVA: 0x000C66A0 File Offset: 0x000C48A0
		// (set) Token: 0x06003467 RID: 13415 RVA: 0x000C66A8 File Offset: 0x000C48A8
		[DefaultValue(false)]
		[MWFCategory("Behavior")]
		public bool AcceptsTab
		{
			get
			{
				return this.accepts_tab;
			}
			set
			{
				if (value != this.accepts_tab)
				{
					this.accepts_tab = value;
					this.OnAcceptsTabChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the height of the control automatically adjusts when the font assigned to the control is changed.</summary>
		/// <returns>true if the height of the control automatically adjusts when the font is changed; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06003468 RID: 13416 RVA: 0x000C66C8 File Offset: 0x000C48C8
		// (set) Token: 0x06003469 RID: 13417 RVA: 0x000C66D0 File Offset: 0x000C48D0
		[MWFCategory("Behavior")]
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DefaultValue(true)]
		[Localizable(true)]
		[RefreshProperties(2)]
		public override bool AutoSize
		{
			get
			{
				return this.auto_size;
			}
			set
			{
				if (value != this.auto_size)
				{
					this.auto_size = value;
					if (this.auto_size && this.PreferredHeight != base.ClientSize.Height)
					{
						base.ClientSize = new Size(base.ClientSize.Width, this.PreferredHeight);
					}
					this.OnAutoSizeChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the background color of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background of the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x0600346A RID: 13418 RVA: 0x000C6740 File Offset: 0x000C4940
		// (set) Token: 0x0600346B RID: 13419 RVA: 0x000C6748 File Offset: 0x000C4948
		[DispId(-501)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				this.backcolor_set = true;
				base.BackColor = this.ChangeBackColor(value);
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x0600346C RID: 13420 RVA: 0x000C6760 File Offset: 0x000C4960
		// (set) Token: 0x0600346D RID: 13421 RVA: 0x000C6768 File Offset: 0x000C4968
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
			}
		}

		/// <summary>Gets or sets the border type of the text box control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.BorderStyle" /> that represents the border type of the text box control. The default is Fixed3D.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value that is not within the range of valid values for the enumeration was assigned to the property. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x0600346E RID: 13422 RVA: 0x000C6774 File Offset: 0x000C4974
		// (set) Token: 0x0600346F RID: 13423 RVA: 0x000C677C File Offset: 0x000C497C
		[DispId(-504)]
		[DefaultValue(BorderStyle.Fixed3D)]
		[MWFCategory("Appearance")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.actual_border_style;
			}
			set
			{
				if (value == this.actual_border_style)
				{
					return;
				}
				if (this.actual_border_style != BorderStyle.Fixed3D || value != BorderStyle.Fixed3D)
				{
					base.Invalidate();
				}
				this.actual_border_style = value;
				this.document.UpdateMargins();
				if (value != BorderStyle.Fixed3D)
				{
					value = BorderStyle.None;
				}
				base.InternalBorderStyle = value;
				this.OnBorderStyleChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets a value indicating whether the user can undo the previous operation in a text box control.</summary>
		/// <returns>true if the user can undo the previous operation performed in a text box control; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06003470 RID: 13424 RVA: 0x000C67E0 File Offset: 0x000C49E0
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public bool CanUndo
		{
			get
			{
				return this.document.undo.CanUndo;
			}
		}

		/// <summary>Gets or sets the foreground color of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the control's foreground color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06003471 RID: 13425 RVA: 0x000C67F4 File Offset: 0x000C49F4
		// (set) Token: 0x06003472 RID: 13426 RVA: 0x000C67FC File Offset: 0x000C49FC
		[DispId(-513)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the selected text in the text box control remains highlighted when the control loses focus.</summary>
		/// <returns>true if the selected text does not appear highlighted when the text box control loses focus; false, if the selected text remains highlighted when the text box control loses focus. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06003473 RID: 13427 RVA: 0x000C6808 File Offset: 0x000C4A08
		// (set) Token: 0x06003474 RID: 13428 RVA: 0x000C6810 File Offset: 0x000C4A10
		[DefaultValue(true)]
		[MWFCategory("Behavior")]
		public bool HideSelection
		{
			get
			{
				return this.hide_selection;
			}
			set
			{
				if (value != this.hide_selection)
				{
					this.hide_selection = value;
					this.OnHideSelectionChanged(EventArgs.Empty);
				}
				this.document.selection_visible = !this.hide_selection;
				this.document.InvalidateSelectionArea();
			}
		}

		/// <summary>Gets or sets the lines of text in a text box control.</summary>
		/// <returns>An array of strings that contains the text in a text box control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06003475 RID: 13429 RVA: 0x000C6850 File Offset: 0x000C4A50
		// (set) Token: 0x06003476 RID: 13430 RVA: 0x000C6904 File Offset: 0x000C4B04
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MWFCategory("Appearance")]
		[Localizable(true)]
		[MergableProperty(false)]
		[DesignerSerializationVisibility(0)]
		public string[] Lines
		{
			get
			{
				int lines = this.document.Lines;
				if (lines == 1 && this.document.GetLine(1).text.Length == 0)
				{
					return new string[0];
				}
				ArrayList arrayList = new ArrayList();
				int i = 1;
				while (i <= lines)
				{
					StringBuilder stringBuilder = new StringBuilder();
					Line line;
					do
					{
						line = this.document.GetLine(i++);
						stringBuilder.Append(line.TextWithoutEnding());
					}
					while (line.ending == LineEnding.Wrap && i <= lines);
					arrayList.Add(stringBuilder.ToString());
				}
				return (string[])arrayList.ToArray(typeof(string));
			}
			set
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < value.Length; i++)
				{
					if (i == value.Length - 1 && value[i].Length == 0)
					{
						break;
					}
					stringBuilder.Append(value[i] + Environment.NewLine);
				}
				int length = Environment.NewLine.Length;
				if (stringBuilder.Length >= length)
				{
					stringBuilder.Remove(stringBuilder.Length - length, length);
				}
				this.Text = stringBuilder.ToString();
			}
		}

		/// <summary>Gets or sets the maximum number of characters the user can type or paste into the text box control.</summary>
		/// <returns>The number of characters that can be entered into the control. The default is 32767.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value assigned to the property is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06003477 RID: 13431 RVA: 0x000C6990 File Offset: 0x000C4B90
		// (set) Token: 0x06003478 RID: 13432 RVA: 0x000C69AC File Offset: 0x000C4BAC
		[Localizable(true)]
		[DefaultValue(32767)]
		[MWFCategory("Behavior")]
		public virtual int MaxLength
		{
			get
			{
				if (this.max_length == 2147483646)
				{
					return 0;
				}
				return this.max_length;
			}
			set
			{
				if (value != this.max_length)
				{
					if (value == 0)
					{
						value = 2147483646;
					}
					this.max_length = value;
				}
			}
		}

		/// <summary>Gets or sets a value that indicates that the text box control has been modified by the user since the control was created or its contents were last set.</summary>
		/// <returns>true if the control's contents have been modified; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06003479 RID: 13433 RVA: 0x000C69DC File Offset: 0x000C4BDC
		// (set) Token: 0x0600347A RID: 13434 RVA: 0x000C69E4 File Offset: 0x000C4BE4
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public bool Modified
		{
			get
			{
				return this.modified;
			}
			set
			{
				if (value != this.modified)
				{
					this.modified = value;
					this.OnModifiedChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether this is a multiline text box control.</summary>
		/// <returns>true if the control is a multiline text box control; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x0600347B RID: 13435 RVA: 0x000C6A04 File Offset: 0x000C4C04
		// (set) Token: 0x0600347C RID: 13436 RVA: 0x000C6A14 File Offset: 0x000C4C14
		[DefaultValue(false)]
		[Localizable(true)]
		[MWFCategory("Behavior")]
		[RefreshProperties(1)]
		public virtual bool Multiline
		{
			get
			{
				return this.document.multiline;
			}
			set
			{
				if (value != this.document.multiline)
				{
					this.document.multiline = value;
					if (this is TextBox)
					{
						base.SetStyle(ControlStyles.FixedHeight, !value);
					}
					this.SetBoundsCore(base.Left, base.Top, base.Width, base.ExplicitBounds.Height, BoundsSpecified.None);
					if (base.Parent != null)
					{
						base.Parent.PerformLayout();
					}
					this.OnMultilineChanged(EventArgs.Empty);
				}
				if (this.document.multiline)
				{
					this.document.Wrap = this.word_wrap;
					this.document.PasswordChar = string.Empty;
				}
				else
				{
					this.document.Wrap = false;
					if (this.password_char != '\0')
					{
						if (this is TextBox)
						{
							this.document.PasswordChar = (this as TextBox).PasswordChar.ToString();
						}
					}
					else
					{
						this.document.PasswordChar = string.Empty;
					}
				}
				if (base.IsHandleCreated)
				{
					this.CalculateDocument();
				}
			}
		}

		/// <summary>Gets the preferred height for a text box.</summary>
		/// <returns>The preferred height of a text box.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x0600347D RID: 13437 RVA: 0x000C6B3C File Offset: 0x000C4D3C
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(2)]
		[Browsable(false)]
		public int PreferredHeight
		{
			get
			{
				if (this.BorderStyle != BorderStyle.None)
				{
					return this.Font.Height + 7;
				}
				return this.Font.Height + this.TopMargin;
			}
		}

		/// <summary>Gets or sets a value indicating whether text in the text box is read-only.</summary>
		/// <returns>true if the text box is read-only; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x000C6B74 File Offset: 0x000C4D74
		// (set) Token: 0x0600347F RID: 13439 RVA: 0x000C6B7C File Offset: 0x000C4D7C
		[RefreshProperties(2)]
		[DefaultValue(false)]
		[MWFCategory("Behavior")]
		public bool ReadOnly
		{
			get
			{
				return this.read_only;
			}
			set
			{
				if (value != this.read_only)
				{
					this.read_only = value;
					if (!this.backcolor_set)
					{
						if (this.read_only)
						{
							this.background_color = SystemColors.Control;
						}
						else
						{
							this.background_color = SystemColors.Window;
						}
					}
					this.OnReadOnlyChanged(EventArgs.Empty);
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value indicating the currently selected text in the control.</summary>
		/// <returns>A string that represents the currently selected text in the text box.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06003480 RID: 13440 RVA: 0x000C6BE0 File Offset: 0x000C4DE0
		// (set) Token: 0x06003481 RID: 13441 RVA: 0x000C6BFC File Offset: 0x000C4DFC
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public virtual string SelectedText
		{
			get
			{
				return this.document.GetSelection();
			}
			set
			{
				this.document.ReplaceSelection(this.CaseAdjust(value), false);
				this.ScrollToCaret();
				this.OnTextChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the number of characters selected in the text box.</summary>
		/// <returns>The number of characters selected in the text box.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than zero.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06003482 RID: 13442 RVA: 0x000C6C30 File Offset: 0x000C4E30
		// (set) Token: 0x06003483 RID: 13443 RVA: 0x000C6C4C File Offset: 0x000C4E4C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public virtual int SelectionLength
		{
			get
			{
				return this.document.SelectionLength();
			}
			set
			{
				if (value < 0)
				{
					string text = string.Format("'{0}' is not a valid value for 'SelectionLength'", value);
					throw new ArgumentOutOfRangeException("SelectionLength", text);
				}
				this.document.InvalidateSelectionArea();
				if (value != 0)
				{
					this.selection_length = value;
					int num = this.document.LineTagToCharIndex(this.document.selection_start.line, this.document.selection_start.pos);
					Line line;
					LineTag lineTag;
					int num2;
					this.document.CharIndexToLineTag(num + value, out line, out lineTag, out num2);
					this.document.SetSelectionEnd(line, num2, true);
					this.document.PositionCaret(line, num2);
				}
				else
				{
					this.selection_length = -1;
					this.document.SetSelectionEnd(this.document.selection_start.line, this.document.selection_start.pos, true);
					this.document.PositionCaret(this.document.selection_start.line, this.document.selection_start.pos);
				}
			}
		}

		/// <summary>Gets or sets the starting point of text selected in the text box.</summary>
		/// <returns>The starting position of text selected in the text box.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than zero.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06003484 RID: 13444 RVA: 0x000C6D58 File Offset: 0x000C4F58
		// (set) Token: 0x06003485 RID: 13445 RVA: 0x000C6D88 File Offset: 0x000C4F88
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int SelectionStart
		{
			get
			{
				return this.document.LineTagToCharIndex(this.document.selection_start.line, this.document.selection_start.pos);
			}
			set
			{
				if (value < 0)
				{
					string text = string.Format("'{0}' is not a valid value for 'SelectionStart'", value);
					throw new ArgumentOutOfRangeException("SelectionStart", text);
				}
				this.has_been_focused = true;
				this.document.InvalidateSelectionArea();
				this.document.SetSelectionStart(value, false);
				if (this.selection_length > -1)
				{
					this.document.SetSelectionEnd(value + this.selection_length, true);
				}
				else
				{
					this.document.SetSelectionEnd(value, true);
				}
				this.document.PositionCaret(this.document.selection_start.line, this.document.selection_start.pos);
				this.ScrollToCaret();
			}
		}

		/// <summary>Gets or sets a value indicating whether the defined shortcuts are enabled.</summary>
		/// <returns>true to enable the shortcuts; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06003486 RID: 13446 RVA: 0x000C6E3C File Offset: 0x000C503C
		// (set) Token: 0x06003487 RID: 13447 RVA: 0x000C6E44 File Offset: 0x000C5044
		[DefaultValue(true)]
		public virtual bool ShortcutsEnabled
		{
			get
			{
				return this.shortcuts_enabled;
			}
			set
			{
				this.shortcuts_enabled = value;
			}
		}

		/// <summary>Gets or sets the current text in the text box.</summary>
		/// <returns>The text displayed in the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06003488 RID: 13448 RVA: 0x000C6E50 File Offset: 0x000C5050
		// (set) Token: 0x06003489 RID: 13449 RVA: 0x000C6EDC File Offset: 0x000C50DC
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Localizable(true)]
		public override string Text
		{
			get
			{
				if (this.document == null || this.document.Root == null || this.document.Root.text == null)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 1; i <= this.document.Lines; i++)
				{
					Line line = this.document.GetLine(i);
					stringBuilder.Append(line.text.ToString());
				}
				return stringBuilder.ToString();
			}
			set
			{
				this.has_been_focused = false;
				if (value == this.Text)
				{
					return;
				}
				if (value != null && value != string.Empty)
				{
					this.document.Empty();
					this.document.Insert(this.document.GetLine(1), 0, false, value);
					this.document.PositionCaret(this.document.GetLine(1), 0);
					this.document.SetSelectionToCaret(true);
					this.ScrollToCaret();
				}
				else
				{
					this.document.Empty();
					if (base.IsHandleCreated)
					{
						this.CalculateDocument();
					}
				}
				this.OnTextChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets the length of text in the control.</summary>
		/// <returns>The number of characters contained in the text of the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x0600348A RID: 13450 RVA: 0x000C6F94 File Offset: 0x000C5194
		[Browsable(false)]
		public virtual int TextLength
		{
			get
			{
				if (this.document == null || this.document.Root == null || this.document.Root.text == null)
				{
					return 0;
				}
				return this.Text.Length;
			}
		}

		/// <summary>Indicates whether a multiline text box control automatically wraps words to the beginning of the next line when necessary.</summary>
		/// <returns>true if the multiline text box control wraps words; false if the text box control automatically scrolls horizontally when the user types past the right edge of the control. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x0600348B RID: 13451 RVA: 0x000C6FE0 File Offset: 0x000C51E0
		// (set) Token: 0x0600348C RID: 13452 RVA: 0x000C6FE8 File Offset: 0x000C51E8
		[DefaultValue(true)]
		[MWFCategory("Behavior")]
		[Localizable(true)]
		public bool WordWrap
		{
			get
			{
				return this.word_wrap;
			}
			set
			{
				if (value != this.word_wrap)
				{
					if (this.document.multiline)
					{
						this.word_wrap = value;
						this.document.Wrap = value;
					}
					this.CalculateDocument();
				}
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x0600348D RID: 13453 RVA: 0x000C7020 File Offset: 0x000C5220
		// (set) Token: 0x0600348E RID: 13454 RVA: 0x000C7028 File Offset: 0x000C5228
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x0600348F RID: 13455 RVA: 0x000C7034 File Offset: 0x000C5234
		// (set) Token: 0x06003490 RID: 13456 RVA: 0x000C703C File Offset: 0x000C523C
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		/// <summary>Gets or sets the default cursor for the control.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.Cursor" /> representing the current default cursor.</returns>
		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06003491 RID: 13457 RVA: 0x000C7048 File Offset: 0x000C5248
		protected override Cursor DefaultCursor
		{
			get
			{
				return Cursors.IBeam;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.Control.ImeMode" /> property can be set to an active value, to enable IME support.</summary>
		/// <returns>false if the <see cref="P:System.Windows.Forms.TextBoxBase.ReadOnly" /> property is true or if this <see cref="T:System.Windows.Forms.TextBoxBase" /> class is set to use a password mask character; otherwise, true.</returns>
		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06003492 RID: 13458 RVA: 0x000C7050 File Offset: 0x000C5250
		protected override bool CanEnableIme
		{
			get
			{
				return !this.ReadOnly && this.password_char == '\0';
			}
		}

		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> representing the information needed when creating a control.</returns>
		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06003493 RID: 13459 RVA: 0x000C706C File Offset: 0x000C526C
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <returns>A <see cref="T:System.Drawing.Size" /> value.</returns>
		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06003494 RID: 13460 RVA: 0x000C7074 File Offset: 0x000C5274
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 20);
			}
		}

		/// <summary>Gets or sets a value indicating whether control drawing is done in a buffer before the control is displayed. This property is not relevant for this class.</summary>
		/// <returns>true to implement double buffering on the control; otherwise, false.</returns>
		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x000C7080 File Offset: 0x000C5280
		// (set) Token: 0x06003496 RID: 13462 RVA: 0x000C7084 File Offset: 0x000C5284
		[EditorBrowsable(1)]
		protected override bool DoubleBuffered
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Appends text to the current text of a text box.</summary>
		/// <param name="text">The text to append to the current contents of the text box. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003497 RID: 13463 RVA: 0x000C7088 File Offset: 0x000C5288
		public void AppendText(string text)
		{
			bool flag = this.document.Lines == 1 && this.Text == string.Empty;
			if (this.document.caret.line.line_no != this.document.Lines || this.document.caret.pos != this.document.caret.line.TextLengthWithoutEnding())
			{
				this.document.MoveCaret(CaretDirection.CtrlEnd);
			}
			this.document.Insert(this.document.caret.line, this.document.caret.pos, false, text, this.document.CaretTag);
			this.document.MoveCaret(CaretDirection.CtrlEnd);
			this.document.SetSelectionToCaret(true);
			if (!flag)
			{
				this.ScrollToCaret();
			}
			this.has_been_focused = true;
			this.Modified = false;
			this.OnTextChanged(EventArgs.Empty);
		}

		/// <summary>Clears all text from the text box control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003498 RID: 13464 RVA: 0x000C718C File Offset: 0x000C538C
		public void Clear()
		{
			this.Modified = false;
			this.Text = string.Empty;
		}

		/// <summary>Clears information about the most recent operation from the undo buffer of the text box.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003499 RID: 13465 RVA: 0x000C71A0 File Offset: 0x000C53A0
		public void ClearUndo()
		{
			this.document.undo.Clear();
		}

		/// <summary>Copies the current selection in the text box to the Clipboard.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600349A RID: 13466 RVA: 0x000C71B4 File Offset: 0x000C53B4
		public void Copy()
		{
			DataObject dataObject = new DataObject(DataFormats.Text, this.SelectedText);
			if (this is RichTextBox)
			{
				dataObject.SetData(DataFormats.Rtf, ((RichTextBox)this).SelectedRtf);
			}
			Clipboard.SetDataObject(dataObject);
		}

		/// <summary>Moves the current selection in the text box to the Clipboard.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600349B RID: 13467 RVA: 0x000C71FC File Offset: 0x000C53FC
		public void Cut()
		{
			DataObject dataObject = new DataObject(DataFormats.Text, this.SelectedText);
			if (this is RichTextBox)
			{
				dataObject.SetData(DataFormats.Rtf, ((RichTextBox)this).SelectedRtf);
			}
			Clipboard.SetDataObject(dataObject);
			this.document.undo.BeginUserAction(Locale.GetText("Cut"));
			this.document.ReplaceSelection(string.Empty, false);
			this.document.undo.EndUserAction();
			this.Modified = true;
			this.OnTextChanged(EventArgs.Empty);
		}

		/// <summary>Replaces the current selection in the text box with the contents of the Clipboard.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600349C RID: 13468 RVA: 0x000C7290 File Offset: 0x000C5490
		public void Paste()
		{
			this.Paste(Clipboard.GetDataObject(), null, false);
		}

		/// <summary>Scrolls the contents of the control to the current caret position.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600349D RID: 13469 RVA: 0x000C72A0 File Offset: 0x000C54A0
		public void ScrollToCaret()
		{
			if (base.IsHandleCreated)
			{
				this.CaretMoved(this, EventArgs.Empty);
			}
		}

		/// <summary>Selects a range of text in the text box.</summary>
		/// <param name="start">The position of the first character in the current text selection within the text box. </param>
		/// <param name="length">The number of characters to select. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="start" /> parameter is less than zero.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600349E RID: 13470 RVA: 0x000C72BC File Offset: 0x000C54BC
		public void Select(int start, int length)
		{
			this.SelectionStart = start;
			this.SelectionLength = length;
		}

		/// <summary>Selects all text in the text box.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600349F RID: 13471 RVA: 0x000C72CC File Offset: 0x000C54CC
		public void SelectAll()
		{
			Line line = this.document.GetLine(this.document.Lines);
			this.document.SetSelectionStart(this.document.GetLine(1), 0, false);
			this.document.SetSelectionEnd(line, line.text.Length, true);
			this.document.PositionCaret(this.document.selection_end.line, this.document.selection_end.pos);
			this.selection_length = -1;
			this.CaretMoved(this, null);
			this.document.DisplayCaret();
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x000C7368 File Offset: 0x000C5568
		internal void SelectAllNoScroll()
		{
			Line line = this.document.GetLine(this.document.Lines);
			this.document.SetSelectionStart(this.document.GetLine(1), 0, false);
			this.document.SetSelectionEnd(line, line.text.Length, false);
			this.document.PositionCaret(this.document.selection_end.line, this.document.selection_end.pos);
			this.selection_length = -1;
			this.document.DisplayCaret();
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.TextBoxBase" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.TextBoxBase" />. The string includes the type and the <see cref="T:System.Windows.Forms.TextBoxBase" /> property of the control.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060034A1 RID: 13473 RVA: 0x000C73FC File Offset: 0x000C55FC
		public override string ToString()
		{
			return base.ToString() + ", Text: " + this.Text;
		}

		/// <summary>Undoes the last edit operation in the text box.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060034A2 RID: 13474 RVA: 0x000C7414 File Offset: 0x000C5614
		[MonoInternalNote("Deleting is classed as Typing, instead of its own Undo event")]
		public void Undo()
		{
			if (this.document.undo.Undo())
			{
				this.Modified = true;
				this.OnTextChanged(EventArgs.Empty);
			}
		}

		/// <summary>Specifies that the value of the <see cref="P:System.Windows.Forms.TextBoxBase.SelectionLength" /> property is zero so that no characters are selected in the control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060034A3 RID: 13475 RVA: 0x000C7440 File Offset: 0x000C5640
		public void DeselectAll()
		{
			this.SelectionLength = 0;
		}

		/// <summary>Retrieves the character that is closest to the specified location within the control.</summary>
		/// <returns>The character at the specified location.</returns>
		/// <param name="pt">The location from which to seek the nearest character. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060034A4 RID: 13476 RVA: 0x000C744C File Offset: 0x000C564C
		public virtual char GetCharFromPosition(Point pt)
		{
			return this.GetCharFromPositionInternal(pt);
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x000C7458 File Offset: 0x000C5658
		internal virtual char GetCharFromPositionInternal(Point p)
		{
			int num;
			LineTag lineTag = this.document.FindCursor(p.X, p.Y, out num);
			if (lineTag == null)
			{
				return '\0';
			}
			if (num < lineTag.Line.text.Length)
			{
				return lineTag.Line.text.get_Chars(num);
			}
			if (lineTag.Line.ending == LineEnding.Wrap)
			{
				Line line = this.document.GetLine(lineTag.Line.line_no + 1);
				if (line != null)
				{
					return line.text.get_Chars(0);
				}
			}
			if (lineTag.Line.line_no == this.document.Lines)
			{
				return lineTag.Line.text.get_Chars(lineTag.Line.text.Length - 1);
			}
			return '\0';
		}

		/// <summary>Retrieves the index of the character nearest to the specified location.</summary>
		/// <returns>The zero-based character index at the specified location.</returns>
		/// <param name="pt">The location to search. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060034A6 RID: 13478 RVA: 0x000C7530 File Offset: 0x000C5730
		public virtual int GetCharIndexFromPosition(Point pt)
		{
			int num;
			LineTag lineTag = this.document.FindCursor(pt.X, pt.Y, out num);
			if (lineTag == null)
			{
				return 0;
			}
			if (num < lineTag.Line.text.Length)
			{
				return this.document.LineTagToCharIndex(lineTag.Line, num);
			}
			if (lineTag.Line.ending == LineEnding.Wrap)
			{
				Line line = this.document.GetLine(lineTag.Line.line_no + 1);
				if (line != null)
				{
					return this.document.LineTagToCharIndex(line, 0);
				}
			}
			if (lineTag.Line.line_no == this.document.Lines)
			{
				return this.document.LineTagToCharIndex(lineTag.Line, lineTag.Line.text.Length - 1);
			}
			return 0;
		}

		/// <summary>Retrieves the location within the control at the specified character index.</summary>
		/// <returns>The location of the specified character within the client rectangle of the control.</returns>
		/// <param name="index">The index of the character for which to retrieve the location. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060034A7 RID: 13479 RVA: 0x000C7608 File Offset: 0x000C5808
		public virtual Point GetPositionFromCharIndex(int index)
		{
			Line line;
			LineTag lineTag;
			int num;
			this.document.CharIndexToLineTag(index, out line, out lineTag, out num);
			return new Point((int)(line.widths[num] + (float)line.X + (float)this.document.viewport_x), line.Y + this.document.viewport_y + lineTag.Shift);
		}

		/// <summary>Retrieves the index of the first character of a given line.</summary>
		/// <returns>The zero-based index of the first character in the specified line.</returns>
		/// <param name="lineNumber">The line for which to get the index of its first character. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="lineNumber" /> parameter is less than zero.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060034A8 RID: 13480 RVA: 0x000C7664 File Offset: 0x000C5864
		public int GetFirstCharIndexFromLine(int lineNumber)
		{
			Line line = this.document.GetLine(lineNumber + 1);
			if (line == null)
			{
				return -1;
			}
			return this.document.LineTagToCharIndex(line, 0);
		}

		/// <summary>Retrieves the index of the first character of the current line.</summary>
		/// <returns>The zero-based character index in the current line.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060034A9 RID: 13481 RVA: 0x000C7698 File Offset: 0x000C5898
		public int GetFirstCharIndexOfCurrentLine()
		{
			return this.document.LineTagToCharIndex(this.document.caret.line, 0);
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000C76B8 File Offset: 0x000C58B8
		protected override void CreateHandle()
		{
			this.CalculateDocument();
			base.CreateHandle();
			this.document.AlignCaret();
			this.ScrollToCaret();
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x000C76D8 File Offset: 0x000C58D8
		internal virtual void HandleLinkClicked(TextBoxBase.LinkRectangle link_clicked)
		{
		}

		/// <summary>Determines whether the specified key is an input key or a special key that requires preprocessing.</summary>
		/// <returns>true if the specified key is an input key; otherwise, false.</returns>
		/// <param name="keyData">One of the Keys value.</param>
		// Token: 0x060034AC RID: 13484 RVA: 0x000C76DC File Offset: 0x000C58DC
		protected override bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) != Keys.None)
			{
				return base.IsInputKey(keyData);
			}
			Keys keys = keyData & Keys.KeyCode;
			switch (keys)
			{
			case Keys.PageUp:
			case Keys.PageDown:
			case Keys.End:
			case Keys.Home:
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
				return true;
			default:
				if (keys != Keys.Tab)
				{
					return keys == Keys.Return && this.accepts_return && this.document.multiline;
				}
				return this.accepts_tab && this.document.multiline && (keyData & Keys.Control) == Keys.None;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TextBoxBase.AcceptsTabChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060034AD RID: 13485 RVA: 0x000C778C File Offset: 0x000C598C
		protected virtual void OnAcceptsTabChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TextBoxBase.AcceptsTabChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TextBoxBase.BorderStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060034AE RID: 13486 RVA: 0x000C77C0 File Offset: 0x000C59C0
		protected virtual void OnBorderStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TextBoxBase.BorderStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060034AF RID: 13487 RVA: 0x000C77F4 File Offset: 0x000C59F4
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			if (this.auto_size && !this.document.multiline && this.PreferredHeight != base.ClientSize.Height)
			{
				base.Height = this.PreferredHeight;
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060034B0 RID: 13488 RVA: 0x000C7848 File Offset: 0x000C5A48
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.FixupHeight();
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060034B1 RID: 13489 RVA: 0x000C7858 File Offset: 0x000C5A58
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raise the <see cref="E:System.Windows.Forms.TextBoxBase.HideSelectionChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060034B2 RID: 13490 RVA: 0x000C7864 File Offset: 0x000C5A64
		protected virtual void OnHideSelectionChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TextBoxBase.HideSelectionChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TextBoxBase.ModifiedChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060034B3 RID: 13491 RVA: 0x000C7898 File Offset: 0x000C5A98
		protected virtual void OnModifiedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TextBoxBase.ModifiedChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TextBoxBase.MultilineChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060034B4 RID: 13492 RVA: 0x000C78CC File Offset: 0x000C5ACC
		protected virtual void OnMultilineChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TextBoxBase.MultilineChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>This method is not relevant for this class.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060034B5 RID: 13493 RVA: 0x000C7900 File Offset: 0x000C5B00
		protected override void OnPaddingChanged(EventArgs e)
		{
			base.OnPaddingChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TextBoxBase.ReadOnlyChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060034B6 RID: 13494 RVA: 0x000C790C File Offset: 0x000C5B0C
		protected virtual void OnReadOnlyChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TextBoxBase.ReadOnlyChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Processes a command key.</summary>
		/// <returns>true if the command key was processed by the control; otherwise, false.</returns>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference that represents the window message to process. </param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the shortcut key to process. </param>
		// Token: 0x060034B7 RID: 13495 RVA: 0x000C7940 File Offset: 0x000C5B40
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return base.ProcessCmdKey(ref msg, keyData);
		}

		/// <returns>true if the key was processed by the control; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x060034B8 RID: 13496 RVA: 0x000C794C File Offset: 0x000C5B4C
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (this.accepts_tab && (keyData & (Keys.LButton | Keys.Back | Keys.Control)) == (Keys.LButton | Keys.Back | Keys.Control))
			{
				keyData ^= Keys.Control;
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x000C7988 File Offset: 0x000C5B88
		private bool ProcessKey(Keys keyData)
		{
			bool flag = (Control.ModifierKeys & Keys.Control) != Keys.None;
			bool flag2 = (Control.ModifierKeys & Keys.Shift) != Keys.None;
			Keys keys;
			if (this.shortcuts_enabled)
			{
				keys = keyData & Keys.KeyCode;
				switch (keys)
				{
				case Keys.V:
					return flag && !this.read_only && this.Paste(Clipboard.GetDataObject(), null, true);
				default:
					switch (keys)
					{
					case Keys.A:
						if (flag)
						{
							this.SelectAll();
							return true;
						}
						return false;
					default:
						if (keys == Keys.Insert)
						{
							if (!this.read_only)
							{
								if (flag2)
								{
									this.Paste(Clipboard.GetDataObject(), null, true);
									return true;
								}
								if (flag)
								{
									this.Copy();
									return true;
								}
							}
							return false;
						}
						if (keys == Keys.Delete)
						{
							if (!this.read_only)
							{
								if (flag2 && !this.read_only)
								{
									this.Cut();
									return true;
								}
								if (this.document.selection_visible)
								{
									this.document.ReplaceSelection(string.Empty, false);
								}
								else if (this.document.CaretPosition >= this.document.CaretLine.TextLengthWithoutEnding())
								{
									if (this.document.CaretLine.LineNo < this.document.Lines)
									{
										Line line = this.document.GetLine(this.document.CaretLine.LineNo + 1);
										this.document.Invalidate(line, 0, line, line.text.Length);
										this.document.Combine(this.document.CaretLine, line);
										this.document.UpdateView(this.document.CaretLine, this.document.Lines, 0);
									}
								}
								else if (!flag)
								{
									this.document.DeleteChar(this.document.CaretTag.Line, this.document.CaretPosition, true);
								}
								else
								{
									int num = this.document.CaretPosition;
									while (num < this.document.CaretLine.Text.Length && !Document.IsWordSeparator(this.document.CaretLine.Text.get_Chars(num)))
									{
										num++;
									}
									if (num < this.document.CaretLine.Text.Length)
									{
										num++;
									}
									this.document.DeleteChars(this.document.CaretTag.Line, this.document.CaretPosition, num - this.document.CaretPosition);
								}
								this.document.AlignCaret();
								this.document.UpdateCaret();
								this.CaretMoved(this, null);
								this.Modified = true;
								this.OnTextChanged(EventArgs.Empty);
								return true;
							}
						}
						break;
					case Keys.C:
						if (flag)
						{
							this.Copy();
							return true;
						}
						return false;
					}
					break;
				case Keys.X:
					if (flag && !this.read_only)
					{
						this.Cut();
						return true;
					}
					return false;
				case Keys.Z:
					if (flag && !this.read_only)
					{
						this.Undo();
						return true;
					}
					return false;
				}
			}
			keys = keyData & Keys.KeyCode;
			switch (keys)
			{
			case Keys.PageUp:
				if ((Control.ModifierKeys & Keys.Control) != Keys.None)
				{
					this.document.MoveCaret(CaretDirection.CtrlPgUp);
				}
				else
				{
					this.document.MoveCaret(CaretDirection.PgUp);
				}
				this.document.DisplayCaret();
				return true;
			case Keys.PageDown:
				if ((Control.ModifierKeys & Keys.Control) != Keys.None)
				{
					this.document.MoveCaret(CaretDirection.CtrlPgDn);
				}
				else
				{
					this.document.MoveCaret(CaretDirection.PgDn);
				}
				this.document.DisplayCaret();
				return true;
			case Keys.End:
				if ((Control.ModifierKeys & Keys.Control) != Keys.None)
				{
					this.document.MoveCaret(CaretDirection.CtrlEnd);
				}
				else
				{
					this.document.MoveCaret(CaretDirection.End);
				}
				if ((Control.ModifierKeys & Keys.Shift) == Keys.None)
				{
					this.document.SetSelectionToCaret(true);
				}
				else
				{
					this.document.SetSelectionToCaret(false);
				}
				this.CaretMoved(this, null);
				return true;
			case Keys.Home:
				if ((Control.ModifierKeys & Keys.Control) != Keys.None)
				{
					this.document.MoveCaret(CaretDirection.CtrlHome);
				}
				else
				{
					this.document.MoveCaret(CaretDirection.Home);
				}
				if ((Control.ModifierKeys & Keys.Shift) == Keys.None)
				{
					this.document.SetSelectionToCaret(true);
				}
				else
				{
					this.document.SetSelectionToCaret(false);
				}
				this.CaretMoved(this, null);
				return true;
			case Keys.Left:
				if (flag)
				{
					this.document.MoveCaret(CaretDirection.WordBack);
				}
				else if (!this.document.selection_visible || flag2)
				{
					this.document.MoveCaret(CaretDirection.CharBack);
				}
				else
				{
					this.document.MoveCaret(CaretDirection.SelectionStart);
				}
				if (!flag2)
				{
					this.document.SetSelectionToCaret(true);
				}
				else
				{
					this.document.SetSelectionToCaret(false);
				}
				this.CaretMoved(this, null);
				return true;
			case Keys.Up:
				if (flag)
				{
					if (this.document.CaretPosition == 0)
					{
						this.document.MoveCaret(CaretDirection.LineUp);
					}
					else
					{
						this.document.MoveCaret(CaretDirection.Home);
					}
				}
				else
				{
					this.document.MoveCaret(CaretDirection.LineUp);
				}
				if ((Control.ModifierKeys & Keys.Shift) == Keys.None)
				{
					this.document.SetSelectionToCaret(true);
				}
				else
				{
					this.document.SetSelectionToCaret(false);
				}
				this.CaretMoved(this, null);
				return true;
			case Keys.Right:
				if (flag)
				{
					this.document.MoveCaret(CaretDirection.WordForward);
				}
				else if (!this.document.selection_visible || flag2)
				{
					this.document.MoveCaret(CaretDirection.CharForward);
				}
				else
				{
					this.document.MoveCaret(CaretDirection.SelectionEnd);
				}
				if (!flag2)
				{
					this.document.SetSelectionToCaret(true);
				}
				else
				{
					this.document.SetSelectionToCaret(false);
				}
				this.CaretMoved(this, null);
				return true;
			case Keys.Down:
				if (flag)
				{
					if (this.document.CaretPosition == this.document.CaretLine.Text.Length)
					{
						this.document.MoveCaret(CaretDirection.LineDown);
					}
					else
					{
						this.document.MoveCaret(CaretDirection.End);
					}
				}
				else
				{
					this.document.MoveCaret(CaretDirection.LineDown);
				}
				if ((Control.ModifierKeys & Keys.Shift) == Keys.None)
				{
					this.document.SetSelectionToCaret(true);
				}
				else
				{
					this.document.SetSelectionToCaret(false);
				}
				this.CaretMoved(this, null);
				return true;
			default:
				if (keys == Keys.Tab)
				{
					if (!this.read_only && this.accepts_tab && this.document.multiline)
					{
						this.document.InsertCharAtCaret('\t', true);
						this.CaretMoved(this, null);
						this.Modified = true;
						this.OnTextChanged(EventArgs.Empty);
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x000C80A0 File Offset: 0x000C62A0
		internal virtual void RaiseSelectionChanged()
		{
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x000C80A4 File Offset: 0x000C62A4
		private void HandleBackspace(bool control)
		{
			bool flag = false;
			if (this.document.selection_visible)
			{
				this.document.undo.BeginUserAction(Locale.GetText("Delete"));
				this.document.ReplaceSelection(string.Empty, false);
				this.document.undo.EndUserAction();
				flag = true;
				this.document.SetSelectionToCaret(true);
			}
			else
			{
				this.document.SetSelectionToCaret(true);
				if (this.document.CaretPosition == 0)
				{
					if (this.document.CaretLine.LineNo > 1)
					{
						Line line = this.document.GetLine(this.document.CaretLine.LineNo - 1);
						int num = line.TextLengthWithoutEnding();
						this.document.Invalidate(line, 0, line, line.text.Length);
						this.document.Combine(line, this.document.CaretLine);
						this.document.UpdateView(line, this.document.Lines - line.LineNo, 0);
						this.document.PositionCaret(line, num);
						this.document.SetSelectionToCaret(true);
						this.document.UpdateCaret();
						flag = true;
					}
				}
				else
				{
					if (!control || this.document.CaretPosition == 0)
					{
						LineTag caretTag = this.document.CaretTag;
						int caretPosition = this.document.CaretPosition;
						this.document.MoveCaret(CaretDirection.CharBack);
						this.document.DeleteChar(caretTag.Line, caretPosition, false);
						this.document.SetSelectionToCaret(true);
					}
					else
					{
						int num2 = this.document.CaretPosition - 1;
						while (num2 > 0 && !Document.IsWordSeparator(this.document.CaretLine.Text.get_Chars(num2 - 1)))
						{
							num2--;
						}
						this.document.undo.BeginUserAction(Locale.GetText("Delete"));
						this.document.DeleteChars(this.document.CaretTag.Line, num2, this.document.CaretPosition - num2);
						this.document.undo.EndUserAction();
						this.document.PositionCaret(this.document.CaretLine, num2);
						this.document.SetSelectionToCaret(true);
					}
					this.document.UpdateCaret();
					flag = true;
				}
			}
			this.CaretMoved(this, null);
			if (flag)
			{
				this.Modified = true;
				this.OnTextChanged(EventArgs.Empty);
			}
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x000C8330 File Offset: 0x000C6530
		private void HandleEnter()
		{
			if (!this.read_only && this.document.multiline && (this.accepts_return || (base.FindForm() != null && base.FindForm().AcceptButton == null) || (Control.ModifierKeys & Keys.Control) != Keys.None))
			{
				if (this.document.selection_visible)
				{
					this.document.ReplaceSelection(string.Empty, false);
				}
				Line caretLine = this.document.CaretLine;
				this.document.Split(this.document.CaretLine, this.document.CaretTag, this.document.CaretPosition);
				caretLine.ending = this.document.StringToLineEnding(Environment.NewLine);
				this.document.InsertString(caretLine, caretLine.text.Length, this.document.LineEndingToString(caretLine.ending));
				this.document.UpdateView(caretLine, this.document.Lines - caretLine.line_no, 0);
				this.CaretMoved(this, null);
				this.Modified = true;
				this.OnTextChanged(EventArgs.Empty);
			}
		}

		/// <summary>Sets the specified bounds of the <see cref="T:System.Windows.Forms.TextBoxBase" /> control.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">Not used.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x060034BD RID: 13501 RVA: 0x000C8460 File Offset: 0x000C6660
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (!this.richtext && !this.document.multiline && height != this.PreferredHeight)
			{
				if ((specified & BoundsSpecified.Height) != BoundsSpecified.None)
				{
					Rectangle explicitBounds = base.ExplicitBounds;
					explicitBounds.Height = height;
					base.ExplicitBounds = explicitBounds;
					specified &= ~BoundsSpecified.Height;
				}
				height = this.PreferredHeight;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <param name="m">A Windows Message Object. </param>
		// Token: 0x060034BE RID: 13502 RVA: 0x000C84D4 File Offset: 0x000C66D4
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			switch (msg)
			{
			case Msg.WM_KEYDOWN:
				if (this.ProcessKeyMessage(ref m) || this.ProcessKey((Keys)(m.WParam.ToInt32() | (int)XplatUI.State.ModifierKeys)))
				{
					m.Result = IntPtr.Zero;
					return;
				}
				this.DefWndProc(ref m);
				return;
			default:
				if (msg != Msg.WM_SETFOCUS)
				{
					if (msg != Msg.WM_KILLFOCUS)
					{
						if (msg != Msg.WM_NCPAINT)
						{
							base.WndProc(ref m);
							return;
						}
						if (!ThemeEngine.Current.TextBoxBaseHandleWmNcPaint(this, ref m))
						{
							base.WndProc(ref m);
						}
					}
					else
					{
						base.WndProc(ref m);
						this.document.CaretLostFocus();
					}
				}
				else
				{
					base.WndProc(ref m);
					this.document.CaretHasFocus();
				}
				return;
			case Msg.WM_CHAR:
			{
				if (this.ProcessKeyMessage(ref m))
				{
					m.Result = IntPtr.Zero;
					return;
				}
				if (this.read_only)
				{
					return;
				}
				m.Result = IntPtr.Zero;
				int num = m.WParam.ToInt32();
				if (num == 127)
				{
					this.HandleBackspace(true);
				}
				else
				{
					if (num >= 32)
					{
						if (this.document.selection_visible)
						{
							this.document.ReplaceSelection(string.Empty, false);
						}
						char c = (char)(int)m.WParam;
						CharacterCasing characterCasing = this.character_casing;
						if (characterCasing != CharacterCasing.Upper)
						{
							if (characterCasing == CharacterCasing.Lower)
							{
								c = char.ToLower((char)(int)m.WParam);
							}
						}
						else
						{
							c = char.ToUpper((char)(int)m.WParam);
						}
						if (this.document.Length < this.max_length)
						{
							this.document.InsertCharAtCaret(c, true);
							this.OnTextUpdate();
							this.CaretMoved(this, null);
							this.Modified = true;
							this.OnTextChanged(EventArgs.Empty);
						}
						else
						{
							XplatUI.AudibleAlert(AlertType.Default);
						}
						return;
					}
					if (num == 8)
					{
						this.HandleBackspace(false);
					}
					else if (num == 13)
					{
						this.HandleEnter();
					}
				}
				return;
			}
			}
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x060034BF RID: 13503 RVA: 0x000C86F8 File Offset: 0x000C68F8
		// (set) Token: 0x060034C0 RID: 13504 RVA: 0x000C8700 File Offset: 0x000C6900
		internal Document Document
		{
			get
			{
				return this.document;
			}
			set
			{
				this.document = value;
			}
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x060034C1 RID: 13505 RVA: 0x000C870C File Offset: 0x000C690C
		// (set) Token: 0x060034C2 RID: 13506 RVA: 0x000C8714 File Offset: 0x000C6914
		internal bool EnableLinks
		{
			get
			{
				return this.enable_links;
			}
			set
			{
				this.enable_links = value;
				this.document.EnableLinks = value;
			}
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x060034C3 RID: 13507 RVA: 0x000C872C File Offset: 0x000C692C
		internal override bool ScaleChildrenInternal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x060034C4 RID: 13508 RVA: 0x000C8730 File Offset: 0x000C6930
		// (set) Token: 0x060034C5 RID: 13509 RVA: 0x000C8750 File Offset: 0x000C6950
		internal bool ShowSelection
		{
			get
			{
				return this.show_selection || !this.hide_selection || this.has_focus;
			}
			set
			{
				if (this.show_selection == value)
				{
					return;
				}
				this.show_selection = value;
				this.document.InvalidateSelectionArea();
			}
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x060034C6 RID: 13510 RVA: 0x000C8774 File Offset: 0x000C6974
		// (set) Token: 0x060034C7 RID: 13511 RVA: 0x000C8784 File Offset: 0x000C6984
		internal int TopMargin
		{
			get
			{
				return this.document.top_margin;
			}
			set
			{
				this.document.top_margin = value;
			}
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x060034C8 RID: 13512 RVA: 0x000C8794 File Offset: 0x000C6994
		internal ScrollBar UIAHScrollBar
		{
			get
			{
				return this.hscroll;
			}
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x060034C9 RID: 13513 RVA: 0x000C879C File Offset: 0x000C699C
		internal ScrollBar UIAVScrollBar
		{
			get
			{
				return this.vscroll;
			}
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x000C87A4 File Offset: 0x000C69A4
		internal Graphics CreateGraphicsInternal()
		{
			if (base.IsHandleCreated)
			{
				return base.CreateGraphics();
			}
			return base.DeviceContext;
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000C87C0 File Offset: 0x000C69C0
		internal override void OnPaintInternal(PaintEventArgs pevent)
		{
			this.Draw(pevent.Graphics, pevent.ClipRectangle);
			pevent.Handled = true;
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x000C87DC File Offset: 0x000C69DC
		internal void Draw(Graphics g, Rectangle clippingArea)
		{
			ThemeEngine.Current.TextBoxBaseFillBackground(this, g, clippingArea);
			this.document.Draw(g, clippingArea);
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x000C87F8 File Offset: 0x000C69F8
		private void FixupHeight()
		{
			if (!this.richtext && !this.document.multiline && this.PreferredHeight != base.ClientSize.Height)
			{
				base.ClientSize = new Size(base.ClientSize.Width, this.PreferredHeight);
			}
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x000C8858 File Offset: 0x000C6A58
		private bool IsDoubleClick(MouseEventArgs e)
		{
			if ((DateTime.Now - this.click_last).TotalMilliseconds > (double)SystemInformation.DoubleClickTime)
			{
				return false;
			}
			Size doubleClickSize = SystemInformation.DoubleClickSize;
			return e.X >= this.click_point_x - doubleClickSize.Width / 2 && e.X <= this.click_point_x + doubleClickSize.Width / 2 && e.Y >= this.click_point_y - doubleClickSize.Height / 2 && e.Y <= this.click_point_y + doubleClickSize.Height / 2;
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x000C8904 File Offset: 0x000C6B04
		private void TextBoxBase_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if ((Control.ModifierKeys & Keys.Shift) > Keys.None)
				{
					this.document.PositionCaret(e.X + this.document.ViewPortX, e.Y + this.document.ViewPortY);
					this.document.SetSelectionToCaret(false);
					this.document.DisplayCaret();
					return;
				}
				bool flag = this.IsDoubleClick(e);
				if (this.current_link != null)
				{
					this.HandleLinkClicked(this.current_link);
					return;
				}
				if (this.document.selection_visible && !flag)
				{
					this.document.SetSelectionToCaret(true);
					this.click_mode = CaretSelection.Position;
				}
				this.document.PositionCaret(e.X + this.document.ViewPortX, e.Y + this.document.ViewPortY);
				if (flag)
				{
					switch (this.click_mode)
					{
					case CaretSelection.Position:
						this.SelectWord();
						this.click_mode = CaretSelection.Word;
						break;
					case CaretSelection.Word:
						if (this is TextBox)
						{
							this.document.SetSelectionToCaret(true);
							this.click_mode = CaretSelection.Position;
						}
						else
						{
							this.document.ExpandSelection(CaretSelection.Line, false);
							this.click_mode = CaretSelection.Line;
						}
						break;
					case CaretSelection.Line:
						this.document.SetSelectionToCaret(true);
						this.SelectWord();
						this.click_mode = CaretSelection.Word;
						break;
					}
				}
				else
				{
					this.document.SetSelectionToCaret(true);
					this.click_mode = CaretSelection.Position;
				}
				this.click_point_x = e.X;
				this.click_point_y = e.Y;
				this.click_last = DateTime.Now;
			}
			if (e.Button == MouseButtons.Middle && XplatUI.RunningOnUnix)
			{
				Document.Marker marker;
				marker.tag = this.document.FindCursor(e.X + this.document.ViewPortX, e.Y + this.document.ViewPortY, out marker.pos);
				marker.line = marker.tag.Line;
				marker.height = marker.tag.Height;
				this.document.SetSelection(marker.line, marker.pos, marker.line, marker.pos);
				this.Paste(Clipboard.GetDataObject(true), null, true);
			}
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x000C8B74 File Offset: 0x000C6D74
		private void TextBoxBase_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (this.click_mode == CaretSelection.Position)
				{
					this.document.SetSelectionToCaret(false);
					this.document.DisplayCaret();
					if (this.Text.Length > 0)
					{
						this.RaiseSelectionChanged();
					}
				}
				if (this.scroll_timer != null)
				{
					this.scroll_timer.Enabled = false;
				}
				return;
			}
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000C8BE4 File Offset: 0x000C6DE4
		private void SizeControls()
		{
			if (this.hscroll.Visible)
			{
				this.canvas_height = base.ClientSize.Height - this.hscroll.Height;
			}
			else
			{
				this.canvas_height = base.ClientSize.Height;
			}
			if (this.vscroll.Visible)
			{
				this.canvas_width = base.ClientSize.Width - this.vscroll.Width;
				if (this.GetInheritedRtoL() == RightToLeft.Yes)
				{
					this.document.OffsetX = this.vscroll.Width;
				}
				else
				{
					this.document.OffsetX = 0;
				}
			}
			else
			{
				this.canvas_width = base.ClientSize.Width;
				this.document.OffsetX = 0;
			}
			this.document.ViewPortWidth = this.canvas_width;
			this.document.ViewPortHeight = this.canvas_height;
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000C8CE4 File Offset: 0x000C6EE4
		private void PositionControls()
		{
			if (this.canvas_height < 1 || this.canvas_width < 1)
			{
				return;
			}
			int num = ((!this.vscroll.Visible) ? 0 : this.vscroll.Width);
			int num2 = ((!this.hscroll.Visible) ? 0 : this.hscroll.Height);
			if (this.GetInheritedRtoL() == RightToLeft.Yes)
			{
				this.hscroll.Bounds = new Rectangle(base.ClientRectangle.Left + num, Math.Max(0, base.ClientRectangle.Height - this.hscroll.Height), base.ClientSize.Width, this.hscroll.Height);
				this.vscroll.Bounds = new Rectangle(base.ClientRectangle.Left, base.ClientRectangle.Top, this.vscroll.Width, Math.Max(0, base.ClientSize.Height - num2));
			}
			else
			{
				this.hscroll.Bounds = new Rectangle(base.ClientRectangle.Left, Math.Max(0, base.ClientRectangle.Height - this.hscroll.Height), Math.Max(0, base.ClientSize.Width - num), this.hscroll.Height);
				this.vscroll.Bounds = new Rectangle(Math.Max(0, base.ClientRectangle.Right - this.vscroll.Width), base.ClientRectangle.Top, this.vscroll.Width, Math.Max(0, base.ClientSize.Height - num2));
			}
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000C8ED0 File Offset: 0x000C70D0
		internal RightToLeft GetInheritedRtoL()
		{
			for (Control control = this; control != null; control = control.Parent)
			{
				if (control.RightToLeft != RightToLeft.Inherit)
				{
					return control.RightToLeft;
				}
			}
			return RightToLeft.No;
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000C8F08 File Offset: 0x000C7108
		private void TextBoxBase_SizeChanged(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				this.CalculateDocument();
			}
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000C8F1C File Offset: 0x000C711C
		private void TextBoxBase_RightToLeftChanged(object o, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				this.CalculateDocument();
			}
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000C8F30 File Offset: 0x000C7130
		private void TextBoxBase_MouseWheel(object sender, MouseEventArgs e)
		{
			if (!this.vscroll.Enabled)
			{
				return;
			}
			if (e.Delta < 0)
			{
				this.vscroll.Value = Math.Min(this.vscroll.Value + SystemInformation.MouseWheelScrollLines * 5, Math.Max(0, this.vscroll.Maximum - this.document.ViewPortHeight + 1));
			}
			else
			{
				this.vscroll.Value = Math.Max(0, this.vscroll.Value - SystemInformation.MouseWheelScrollLines * 5);
			}
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x000C8FC8 File Offset: 0x000C71C8
		internal virtual void SelectWord()
		{
			StringBuilder text = this.document.caret.line.text;
			int num = this.document.caret.pos;
			int num2 = this.document.caret.pos;
			if (text.Length >= 1)
			{
				if (num > 0)
				{
					num--;
					num2--;
				}
				while (num > 0 && text.get_Chars(num) == ' ')
				{
					num--;
				}
				if (num > 0)
				{
					while (num > 0 && text.get_Chars(num) != ' ')
					{
						num--;
					}
					if (text.get_Chars(num) == ' ')
					{
						num++;
					}
				}
				if (text.get_Chars(num2) == ' ')
				{
					while (num2 < text.Length && text.get_Chars(num2) == ' ')
					{
						num2++;
					}
				}
				else
				{
					while (num2 < text.Length && text.get_Chars(num2) != ' ')
					{
						num2++;
					}
					while (num2 < text.Length && text.get_Chars(num2) == ' ')
					{
						num2++;
					}
				}
				this.document.SetSelection(this.document.caret.line, num, this.document.caret.line, num2);
				this.document.PositionCaret(this.document.selection_end.line, this.document.selection_end.pos);
				this.document.DisplayCaret();
				return;
			}
			if (this.document.caret.line.line_no >= this.document.Lines)
			{
				return;
			}
			Line line = this.document.GetLine(this.document.caret.line.line_no + 1);
			this.document.PositionCaret(line, 0);
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000C91B4 File Offset: 0x000C73B4
		internal void CalculateDocument()
		{
			this.CalculateScrollBars();
			this.document.RecalculateDocument(this.CreateGraphicsInternal());
			if (this.document.caret.line != null && this.document.caret.line.Y < this.document.ViewPortHeight)
			{
				this.vscroll.Value = 0;
			}
			base.Invalidate();
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x000C9228 File Offset: 0x000C7428
		internal void CalculateScrollBars()
		{
			this.SizeControls();
			if (this.document.Width >= this.document.ViewPortWidth)
			{
				this.hscroll.SetValues(0, Math.Max(1, this.document.Width), -1, (this.document.ViewPortWidth >= 0) ? this.document.ViewPortWidth : 0);
				if (this.document.multiline)
				{
					this.hscroll.Enabled = true;
				}
			}
			else
			{
				this.hscroll.Enabled = false;
				this.hscroll.Maximum = this.document.ViewPortWidth;
			}
			if (this.document.Height >= this.document.ViewPortHeight)
			{
				this.vscroll.SetValues(0, Math.Max(1, this.document.Height), -1, (this.document.ViewPortHeight >= 0) ? this.document.ViewPortHeight : 0);
				if (this.document.multiline)
				{
					this.vscroll.Enabled = true;
				}
			}
			else
			{
				this.vscroll.Enabled = false;
				this.vscroll.Maximum = this.document.ViewPortHeight;
			}
			RichTextBoxScrollBars richTextBoxScrollBars;
			if (!this.WordWrap)
			{
				richTextBoxScrollBars = this.scrollbars;
				switch (richTextBoxScrollBars)
				{
				case RichTextBoxScrollBars.Horizontal:
				case RichTextBoxScrollBars.Both:
					if (this.richtext)
					{
						this.hscroll.Visible = this.hscroll.Enabled;
					}
					else
					{
						this.hscroll.Visible = this.Multiline;
					}
					break;
				default:
					switch (richTextBoxScrollBars)
					{
					case RichTextBoxScrollBars.ForcedHorizontal:
					case RichTextBoxScrollBars.ForcedBoth:
						this.hscroll.Visible = true;
						goto IL_01E0;
					}
					this.hscroll.Visible = false;
					break;
				}
				IL_01E0:;
			}
			else
			{
				this.hscroll.Visible = false;
			}
			richTextBoxScrollBars = this.scrollbars;
			if (richTextBoxScrollBars != RichTextBoxScrollBars.Vertical && richTextBoxScrollBars != RichTextBoxScrollBars.Both)
			{
				if (richTextBoxScrollBars != RichTextBoxScrollBars.ForcedVertical && richTextBoxScrollBars != RichTextBoxScrollBars.ForcedBoth)
				{
					this.vscroll.Visible = false;
				}
				else
				{
					this.vscroll.Visible = true;
				}
			}
			else if (this.richtext)
			{
				this.vscroll.Visible = this.vscroll.Enabled;
			}
			else
			{
				this.vscroll.Visible = this.Multiline;
			}
			this.PositionControls();
			this.SizeControls();
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x000C94BC File Offset: 0x000C76BC
		private void document_WidthChanged(object sender, EventArgs e)
		{
			this.CalculateScrollBars();
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x000C94C4 File Offset: 0x000C76C4
		private void document_HeightChanged(object sender, EventArgs e)
		{
			this.CalculateScrollBars();
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x000C94CC File Offset: 0x000C76CC
		private void ScrollLinks(int xChange, int yChange)
		{
			foreach (object obj in this.list_links)
			{
				TextBoxBase.LinkRectangle linkRectangle = (TextBoxBase.LinkRectangle)obj;
				linkRectangle.Scroll(xChange, yChange);
			}
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x000C953C File Offset: 0x000C773C
		private void hscroll_ValueChanged(object sender, EventArgs e)
		{
			int viewPortX = this.document.ViewPortX;
			this.document.ViewPortX = this.hscroll.Value;
			if (this.Focused)
			{
				this.document.CaretLostFocus();
			}
			if (this.vscroll.Visible)
			{
				if (this.GetInheritedRtoL() == RightToLeft.Yes)
				{
					XplatUI.ScrollWindow(this.Handle, new Rectangle(this.vscroll.Width, 0, base.ClientSize.Width - this.vscroll.Width, base.ClientSize.Height), viewPortX - this.hscroll.Value, 0, false);
				}
				else
				{
					XplatUI.ScrollWindow(this.Handle, new Rectangle(0, 0, base.ClientSize.Width - this.vscroll.Width, base.ClientSize.Height), viewPortX - this.hscroll.Value, 0, false);
				}
			}
			else
			{
				XplatUI.ScrollWindow(this.Handle, base.ClientRectangle, viewPortX - this.hscroll.Value, 0, false);
			}
			this.ScrollLinks(viewPortX - this.hscroll.Value, 0);
			if (this.Focused)
			{
				this.document.CaretHasFocus();
			}
			EventHandler eventHandler = (EventHandler)base.Events[TextBoxBase.HScrolledEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x060034DE RID: 13534 RVA: 0x000C96B8 File Offset: 0x000C78B8
		private void vscroll_ValueChanged(object sender, EventArgs e)
		{
			int viewPortY = this.document.ViewPortY;
			this.document.ViewPortY = this.vscroll.Value;
			if (this.Focused)
			{
				this.document.CaretLostFocus();
			}
			if (this.hscroll.Visible)
			{
				XplatUI.ScrollWindow(this.Handle, new Rectangle(0, 0, base.ClientSize.Width, base.ClientSize.Height - this.hscroll.Height), 0, viewPortY - this.vscroll.Value, false);
			}
			else
			{
				XplatUI.ScrollWindow(this.Handle, base.ClientRectangle, 0, viewPortY - this.vscroll.Value, false);
			}
			this.ScrollLinks(0, viewPortY - this.vscroll.Value);
			if (this.Focused)
			{
				this.document.CaretHasFocus();
			}
			EventHandler eventHandler = (EventHandler)base.Events[TextBoxBase.VScrolledEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x000C97D0 File Offset: 0x000C79D0
		private void TextBoxBase_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && base.Capture)
			{
				if (!base.ClientRectangle.Contains(e.X, e.Y))
				{
					if (this.scroll_timer == null)
					{
						this.scroll_timer = new Timer();
						this.scroll_timer.Interval = 100;
						this.scroll_timer.Tick += new EventHandler(this.ScrollTimerTickHandler);
					}
					if (!this.scroll_timer.Enabled)
					{
						this.scroll_timer.Start();
						this.ScrollTimerTickHandler(null, EventArgs.Empty);
					}
				}
				this.document.PositionCaret(e.X + this.document.ViewPortX, e.Y + this.document.ViewPortY);
				if (this.click_mode == CaretSelection.Position)
				{
					this.document.SetSelectionToCaret(false);
					this.document.DisplayCaret();
				}
			}
			bool flag = false;
			foreach (object obj in this.list_links)
			{
				TextBoxBase.LinkRectangle linkRectangle = (TextBoxBase.LinkRectangle)obj;
				if (linkRectangle.LinkAreaRectangle.Contains(e.X, e.Y))
				{
					XplatUI.SetCursor(this.window.Handle, Cursors.Hand.handle);
					flag = true;
					this.current_link = linkRectangle;
					break;
				}
			}
			if (!flag)
			{
				XplatUI.SetCursor(this.window.Handle, this.DefaultCursor.handle);
				this.current_link = null;
			}
		}

		// Token: 0x060034E0 RID: 13536 RVA: 0x000C999C File Offset: 0x000C7B9C
		private void TextBoxBase_FontOrColorChanged(object sender, EventArgs e)
		{
			this.document.SuspendRecalc();
			for (int i = 1; i <= this.document.Lines; i++)
			{
				Line line = this.document.GetLine(i);
				if (LineTag.FormatText(line, 1, line.text.Length, this.Font, this.ForeColor, Color.Empty, FormatSpecified.Font | FormatSpecified.Color))
				{
					this.document.RecalculateDocument(this.CreateGraphicsInternal(), line.LineNo, line.LineNo, false);
				}
			}
			this.document.ResumeRecalc(false);
			this.document.AlignCaret();
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x000C9A40 File Offset: 0x000C7C40
		private void ScrollTimerTickHandler(object sender, EventArgs e)
		{
			Point point = Cursor.Position;
			point = base.PointToClient(point);
			if (point.X < base.ClientRectangle.Left)
			{
				this.document.MoveCaret(CaretDirection.CharBackNoWrap);
				this.document.SetSelectionToCaret(false);
				this.CaretMoved(this, null);
			}
			else if (point.X > base.ClientRectangle.Right)
			{
				this.document.MoveCaret(CaretDirection.CharForwardNoWrap);
				this.document.SetSelectionToCaret(false);
				this.CaretMoved(this, null);
			}
			else if (point.Y > base.ClientRectangle.Bottom)
			{
				this.document.MoveCaret(CaretDirection.LineDown);
				this.document.SetSelectionToCaret(false);
				this.CaretMoved(this, null);
			}
			else if (point.Y < base.ClientRectangle.Top)
			{
				this.document.MoveCaret(CaretDirection.LineUp);
				this.document.SetSelectionToCaret(false);
				this.CaretMoved(this, null);
			}
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x000C9B58 File Offset: 0x000C7D58
		internal void CaretMoved(object sender, EventArgs e)
		{
			if (!base.IsHandleCreated || this.canvas_width < 1 || this.canvas_height < 1)
			{
				return;
			}
			this.document.MoveCaretToTextTag();
			Point caret = this.document.Caret;
			if (this.document.CaretLine.alignment == HorizontalAlignment.Left)
			{
				if (caret.X < this.document.ViewPortX)
				{
					do
					{
						if (this.hscroll.Value - this.document.ViewPortWidth / 3 >= this.hscroll.Minimum)
						{
							this.hscroll.SafeValueSet(this.hscroll.Value - this.document.ViewPortWidth / 3);
						}
						else
						{
							this.hscroll.Value = this.hscroll.Minimum;
						}
					}
					while (this.hscroll.Value > caret.X);
				}
				if (caret.X >= this.document.ViewPortWidth + this.document.ViewPortX && this.hscroll.Value != this.hscroll.Maximum)
				{
					if (caret.X - this.document.ViewPortWidth + 1 <= this.hscroll.Maximum)
					{
						if (caret.X - this.document.ViewPortWidth >= 0)
						{
							this.hscroll.SafeValueSet(caret.X - this.document.ViewPortWidth + 1);
						}
						else
						{
							this.hscroll.Value = 0;
						}
					}
					else
					{
						this.hscroll.Value = this.hscroll.Maximum;
					}
				}
			}
			else if (this.document.CaretLine.alignment == HorizontalAlignment.Right)
			{
			}
			if (this.Text.Length > 0)
			{
				this.RaiseSelectionChanged();
			}
			if (!this.document.multiline)
			{
				return;
			}
			int num = this.document.CaretLine.Height + 1;
			if (caret.Y < this.document.ViewPortY)
			{
				this.vscroll.SafeValueSet(caret.Y);
			}
			if (caret.Y + num > this.document.ViewPortY + this.canvas_height)
			{
				this.vscroll.Value = Math.Min(this.vscroll.Maximum, caret.Y - this.canvas_height + num);
			}
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x000C9DE4 File Offset: 0x000C7FE4
		internal bool Paste(IDataObject clip, DataFormats.Format format, bool obey_length)
		{
			if (clip == null)
			{
				return false;
			}
			if (format == null)
			{
				if (this is RichTextBox && clip.GetDataPresent(DataFormats.Rtf))
				{
					format = DataFormats.GetFormat(DataFormats.Rtf);
				}
				else if (this is RichTextBox && clip.GetDataPresent(DataFormats.Bitmap))
				{
					format = DataFormats.GetFormat(DataFormats.Bitmap);
				}
				else if (clip.GetDataPresent(DataFormats.UnicodeText))
				{
					format = DataFormats.GetFormat(DataFormats.UnicodeText);
				}
				else
				{
					if (!clip.GetDataPresent(DataFormats.Text))
					{
						return false;
					}
					format = DataFormats.GetFormat(DataFormats.Text);
				}
			}
			else
			{
				if (format.Name == DataFormats.Rtf && !(this is RichTextBox))
				{
					return false;
				}
				if (!clip.GetDataPresent(format.Name))
				{
					return false;
				}
			}
			if (format.Name == DataFormats.Rtf)
			{
				this.document.undo.BeginUserAction(Locale.GetText("Paste"));
				((RichTextBox)this).SelectedRtf = (string)clip.GetData(DataFormats.Rtf);
				this.document.undo.EndUserAction();
				this.Modified = true;
				return true;
			}
			if (format.Name == DataFormats.Bitmap)
			{
				this.document.undo.BeginUserAction(Locale.GetText("Paste"));
				this.document.MoveCaret(CaretDirection.CharForward);
				this.document.undo.EndUserAction();
				return true;
			}
			string text;
			if (format.Name == DataFormats.UnicodeText)
			{
				text = (string)clip.GetData(DataFormats.UnicodeText);
			}
			else
			{
				if (!(format.Name == DataFormats.Text))
				{
					return false;
				}
				text = (string)clip.GetData(DataFormats.Text);
			}
			if (!obey_length)
			{
				this.document.undo.BeginUserAction(Locale.GetText("Paste"));
				this.SelectedText = text;
				this.document.undo.EndUserAction();
			}
			else if (text.Length + (this.document.Length - this.SelectedText.Length) < this.max_length)
			{
				this.document.undo.BeginUserAction(Locale.GetText("Paste"));
				this.SelectedText = text;
				this.document.undo.EndUserAction();
			}
			else if (this.document.Length - this.SelectedText.Length < this.max_length)
			{
				this.document.undo.BeginUserAction(Locale.GetText("Paste"));
				this.SelectedText = text.Substring(0, this.max_length - (this.document.Length - this.SelectedText.Length));
				this.document.undo.EndUserAction();
			}
			this.Modified = true;
			return true;
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x000CA0F8 File Offset: 0x000C82F8
		internal virtual Color ChangeBackColor(Color backColor)
		{
			return backColor;
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x000CA0FC File Offset: 0x000C82FC
		internal override bool IsInputCharInternal(char charCode)
		{
			return true;
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x000CA100 File Offset: 0x000C8300
		internal virtual void OnTextUpdate()
		{
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060034E7 RID: 13543 RVA: 0x000CA104 File Offset: 0x000C8304
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
		}

		/// <summary>Retrieves the line number from the specified character position within the text of the control.</summary>
		/// <returns>The zero-based line number in which the character index is located.</returns>
		/// <param name="index">The character index position to search. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060034E8 RID: 13544 RVA: 0x000CA110 File Offset: 0x000C8310
		public virtual int GetLineFromCharIndex(int index)
		{
			Line line;
			LineTag lineTag;
			int num;
			this.document.CharIndexToLineTag(index, out line, out lineTag, out num);
			return line.LineNo;
		}

		/// <param name="mevent"></param>
		// Token: 0x060034E9 RID: 13545 RVA: 0x000CA138 File Offset: 0x000C8338
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			base.OnMouseUp(mevent);
		}

		// Token: 0x040018A8 RID: 6312
		internal HorizontalAlignment alignment;

		// Token: 0x040018A9 RID: 6313
		internal bool accepts_tab;

		// Token: 0x040018AA RID: 6314
		internal bool accepts_return;

		// Token: 0x040018AB RID: 6315
		internal bool auto_size;

		// Token: 0x040018AC RID: 6316
		internal bool backcolor_set;

		// Token: 0x040018AD RID: 6317
		internal CharacterCasing character_casing;

		// Token: 0x040018AE RID: 6318
		internal bool hide_selection;

		// Token: 0x040018AF RID: 6319
		private int max_length;

		// Token: 0x040018B0 RID: 6320
		internal bool modified;

		// Token: 0x040018B1 RID: 6321
		internal char password_char;

		// Token: 0x040018B2 RID: 6322
		internal bool read_only;

		// Token: 0x040018B3 RID: 6323
		internal bool word_wrap;

		// Token: 0x040018B4 RID: 6324
		internal Document document;

		// Token: 0x040018B5 RID: 6325
		internal LineTag caret_tag;

		// Token: 0x040018B6 RID: 6326
		internal int caret_pos;

		// Token: 0x040018B7 RID: 6327
		internal ImplicitHScrollBar hscroll;

		// Token: 0x040018B8 RID: 6328
		internal ImplicitVScrollBar vscroll;

		// Token: 0x040018B9 RID: 6329
		internal RichTextBoxScrollBars scrollbars;

		// Token: 0x040018BA RID: 6330
		internal Timer scroll_timer;

		// Token: 0x040018BB RID: 6331
		internal bool richtext;

		// Token: 0x040018BC RID: 6332
		internal bool show_selection;

		// Token: 0x040018BD RID: 6333
		internal ArrayList list_links;

		// Token: 0x040018BE RID: 6334
		private TextBoxBase.LinkRectangle current_link;

		// Token: 0x040018BF RID: 6335
		private bool enable_links;

		// Token: 0x040018C0 RID: 6336
		internal bool has_been_focused;

		// Token: 0x040018C1 RID: 6337
		internal int selection_length = -1;

		// Token: 0x040018C2 RID: 6338
		internal bool show_caret_w_selection;

		// Token: 0x040018C3 RID: 6339
		internal int canvas_width;

		// Token: 0x040018C4 RID: 6340
		internal int canvas_height;

		// Token: 0x040018C5 RID: 6341
		internal static int track_width = 2;

		// Token: 0x040018C6 RID: 6342
		internal static int track_border = 5;

		// Token: 0x040018C7 RID: 6343
		internal DateTime click_last;

		// Token: 0x040018C8 RID: 6344
		internal int click_point_x;

		// Token: 0x040018C9 RID: 6345
		internal int click_point_y;

		// Token: 0x040018CA RID: 6346
		internal CaretSelection click_mode;

		// Token: 0x040018CB RID: 6347
		internal BorderStyle actual_border_style;

		// Token: 0x040018CC RID: 6348
		internal bool shortcuts_enabled = true;

		// Token: 0x02000313 RID: 787
		internal class LinkRectangle
		{
			// Token: 0x060034EA RID: 13546 RVA: 0x000CA144 File Offset: 0x000C8344
			public LinkRectangle(Rectangle rect)
			{
				this.link_tag = null;
				this.link_area_rectangle = rect;
			}

			// Token: 0x17000DC3 RID: 3523
			// (get) Token: 0x060034EB RID: 13547 RVA: 0x000CA15C File Offset: 0x000C835C
			// (set) Token: 0x060034EC RID: 13548 RVA: 0x000CA164 File Offset: 0x000C8364
			public Rectangle LinkAreaRectangle
			{
				get
				{
					return this.link_area_rectangle;
				}
				set
				{
					this.link_area_rectangle = value;
				}
			}

			// Token: 0x17000DC4 RID: 3524
			// (get) Token: 0x060034ED RID: 13549 RVA: 0x000CA170 File Offset: 0x000C8370
			// (set) Token: 0x060034EE RID: 13550 RVA: 0x000CA178 File Offset: 0x000C8378
			public LineTag LinkTag
			{
				get
				{
					return this.link_tag;
				}
				set
				{
					this.link_tag = value;
				}
			}

			// Token: 0x060034EF RID: 13551 RVA: 0x000CA184 File Offset: 0x000C8384
			public void Scroll(int x_change, int y_change)
			{
				this.link_area_rectangle.X = this.link_area_rectangle.X + x_change;
				this.link_area_rectangle.Y = this.link_area_rectangle.Y + y_change;
			}

			// Token: 0x040018D7 RID: 6359
			private Rectangle link_area_rectangle;

			// Token: 0x040018D8 RID: 6360
			private LineTag link_tag;
		}
	}
}
