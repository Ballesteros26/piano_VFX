using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a text box in a <see cref="T:System.Windows.Forms.ToolStrip" /> that allows the user to enter text.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000380 RID: 896
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
	public class ToolStripTextBox : ToolStripControlHost
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> class.</summary>
		// Token: 0x06004083 RID: 16515 RVA: 0x001008F0 File Offset: 0x000FEAF0
		public ToolStripTextBox()
			: base(new ToolStripTextBox.ToolStripTextBoxControl())
		{
			ToolStripTextBox.ToolStripTextBoxControl toolStripTextBoxControl = this.TextBox as ToolStripTextBox.ToolStripTextBoxControl;
			toolStripTextBoxControl.OwnerItem = this;
			toolStripTextBoxControl.border_style = BorderStyle.None;
			toolStripTextBoxControl.TopMargin = 3;
			toolStripTextBoxControl.Border = BorderStyle.Fixed3D;
			this.border_style = BorderStyle.Fixed3D;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> class derived from a base control.</summary>
		/// <param name="c">The control from which to derive the <see cref="T:System.Windows.Forms.ToolStripTextBox" />. </param>
		// Token: 0x06004084 RID: 16516 RVA: 0x00100938 File Offset: 0x000FEB38
		[EditorBrowsable(1)]
		public ToolStripTextBox(Control c)
			: base(c)
		{
			throw new NotSupportedException("This construtor cannot be used.");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> class with the specified name. </summary>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</param>
		// Token: 0x06004085 RID: 16517 RVA: 0x0010094C File Offset: 0x000FEB4C
		public ToolStripTextBox(string name)
			: this()
		{
			base.Name = name;
		}

		// Token: 0x06004086 RID: 16518 RVA: 0x0010095C File Offset: 0x000FEB5C
		// Note: this type is marked as 'beforefieldinit'.
		static ToolStripTextBox()
		{
			ToolStripTextBox.AcceptsTabChangedEvent = new object();
			ToolStripTextBox.BorderStyleChangedEvent = new object();
			ToolStripTextBox.HideSelectionChangedEvent = new object();
			ToolStripTextBox.ModifiedChangedEvent = new object();
			ToolStripTextBox.MultilineChangedEvent = new object();
			ToolStripTextBox.ReadOnlyChangedEvent = new object();
			ToolStripTextBox.TextBoxTextAlignChangedEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.AcceptsTab" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003F2 RID: 1010
		// (add) Token: 0x06004087 RID: 16519 RVA: 0x001009B0 File Offset: 0x000FEBB0
		// (remove) Token: 0x06004088 RID: 16520 RVA: 0x001009C4 File Offset: 0x000FEBC4
		public event EventHandler AcceptsTabChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.AcceptsTabChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.AcceptsTabChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.BorderStyle" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003F3 RID: 1011
		// (add) Token: 0x06004089 RID: 16521 RVA: 0x001009D8 File Offset: 0x000FEBD8
		// (remove) Token: 0x0600408A RID: 16522 RVA: 0x001009EC File Offset: 0x000FEBEC
		public event EventHandler BorderStyleChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.BorderStyleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.BorderStyleChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.HideSelection" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003F4 RID: 1012
		// (add) Token: 0x0600408B RID: 16523 RVA: 0x00100A00 File Offset: 0x000FEC00
		// (remove) Token: 0x0600408C RID: 16524 RVA: 0x00100A14 File Offset: 0x000FEC14
		public event EventHandler HideSelectionChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.HideSelectionChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.HideSelectionChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.Modified" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003F5 RID: 1013
		// (add) Token: 0x0600408D RID: 16525 RVA: 0x00100A28 File Offset: 0x000FEC28
		// (remove) Token: 0x0600408E RID: 16526 RVA: 0x00100A3C File Offset: 0x000FEC3C
		public event EventHandler ModifiedChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.ModifiedChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.ModifiedChangedEvent, value);
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003F6 RID: 1014
		// (add) Token: 0x0600408F RID: 16527 RVA: 0x00100A50 File Offset: 0x000FEC50
		// (remove) Token: 0x06004090 RID: 16528 RVA: 0x00100A64 File Offset: 0x000FEC64
		[EditorBrowsable(1)]
		[Browsable(false)]
		public event EventHandler MultilineChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.MultilineChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.MultilineChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.ReadOnly" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003F7 RID: 1015
		// (add) Token: 0x06004091 RID: 16529 RVA: 0x00100A78 File Offset: 0x000FEC78
		// (remove) Token: 0x06004092 RID: 16530 RVA: 0x00100A8C File Offset: 0x000FEC8C
		public event EventHandler ReadOnlyChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.ReadOnlyChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.ReadOnlyChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.TextBoxTextAlign" /> property changes.</summary>
		// Token: 0x140003F8 RID: 1016
		// (add) Token: 0x06004093 RID: 16531 RVA: 0x00100AA0 File Offset: 0x000FECA0
		// (remove) Token: 0x06004094 RID: 16532 RVA: 0x00100AB4 File Offset: 0x000FECB4
		public event EventHandler TextBoxTextAlignChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.TextBoxTextAlignChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.TextBoxTextAlignChangedEvent, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether pressing ENTER in a multiline <see cref="T:System.Windows.Forms.TextBox" /> control creates a new line of text in the control or activates the default button for the form.</summary>
		/// <returns>true if the ENTER key creates a new line of text in a multiline version of the control; false if the ENTER key activates the default button for the form. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x06004095 RID: 16533 RVA: 0x00100AC8 File Offset: 0x000FECC8
		// (set) Token: 0x06004096 RID: 16534 RVA: 0x00100AD8 File Offset: 0x000FECD8
		[DefaultValue(false)]
		public bool AcceptsReturn
		{
			get
			{
				return this.TextBox.AcceptsReturn;
			}
			set
			{
				this.TextBox.AcceptsReturn = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether pressing the TAB key in a multiline text box control types a TAB character in the control instead of moving the focus to the next control in the tab order.</summary>
		/// <returns>true if users can enter tabs in a multiline text box using the TAB key; false if pressing the TAB key moves the focus. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x06004097 RID: 16535 RVA: 0x00100AE8 File Offset: 0x000FECE8
		// (set) Token: 0x06004098 RID: 16536 RVA: 0x00100AF8 File Offset: 0x000FECF8
		[DefaultValue(false)]
		public bool AcceptsTab
		{
			get
			{
				return this.TextBox.AcceptsTab;
			}
			set
			{
				this.TextBox.AcceptsTab = value;
			}
		}

		/// <summary>Gets or sets a custom string collection to use when the <see cref="P:System.Windows.Forms.ToolStripTextBox.AutoCompleteSource" /> property is set to CustomSource.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" /> to use with <see cref="P:System.Windows.Forms.TextBox.AutoCompleteSource" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x06004099 RID: 16537 RVA: 0x00100B08 File Offset: 0x000FED08
		// (set) Token: 0x0600409A RID: 16538 RVA: 0x00100B18 File Offset: 0x000FED18
		[DesignerSerializationVisibility(2)]
		[MonoTODO("AutoCompletion algorithm is currently not implemented.")]
		[Browsable(true)]
		[Localizable(true)]
		[EditorBrowsable(0)]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public AutoCompleteStringCollection AutoCompleteCustomSource
		{
			get
			{
				return this.TextBox.AutoCompleteCustomSource;
			}
			set
			{
				this.TextBox.AutoCompleteCustomSource = value;
			}
		}

		/// <summary>Gets or sets an option that controls how automatic completion works for the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AutoCompleteMode" /> values. The default is <see cref="F:System.Windows.Forms.AutoCompleteMode.None" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x0600409B RID: 16539 RVA: 0x00100B28 File Offset: 0x000FED28
		// (set) Token: 0x0600409C RID: 16540 RVA: 0x00100B38 File Offset: 0x000FED38
		[EditorBrowsable(0)]
		[DefaultValue(AutoCompleteMode.None)]
		[Browsable(true)]
		[MonoTODO("AutoCompletion algorithm is currently not implemented.")]
		public AutoCompleteMode AutoCompleteMode
		{
			get
			{
				return this.TextBox.AutoCompleteMode;
			}
			set
			{
				this.TextBox.AutoCompleteMode = value;
			}
		}

		/// <summary>Gets or sets a value specifying the source of complete strings used for automatic completion.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AutoCompleteSource" /> values. The default is <see cref="F:System.Windows.Forms.AutoCompleteSource.None" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x0600409D RID: 16541 RVA: 0x00100B48 File Offset: 0x000FED48
		// (set) Token: 0x0600409E RID: 16542 RVA: 0x00100B58 File Offset: 0x000FED58
		[DefaultValue(AutoCompleteSource.None)]
		[MonoTODO("AutoCompletion algorithm is currently not implemented.")]
		[Browsable(true)]
		[EditorBrowsable(0)]
		public AutoCompleteSource AutoCompleteSource
		{
			get
			{
				return this.TextBox.AutoCompleteSource;
			}
			set
			{
				this.TextBox.AutoCompleteSource = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x0600409F RID: 16543 RVA: 0x00100B68 File Offset: 0x000FED68
		// (set) Token: 0x060040A0 RID: 16544 RVA: 0x00100B70 File Offset: 0x000FED70
		[EditorBrowsable(1)]
		[Browsable(false)]
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
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value.</returns>
		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x060040A1 RID: 16545 RVA: 0x00100B7C File Offset: 0x000FED7C
		// (set) Token: 0x060040A2 RID: 16546 RVA: 0x00100B84 File Offset: 0x000FED84
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

		/// <summary>Gets or sets the border type of the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.BorderStyle.Fixed3D" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x060040A3 RID: 16547 RVA: 0x00100B90 File Offset: 0x000FED90
		// (set) Token: 0x060040A4 RID: 16548 RVA: 0x00100B98 File Offset: 0x000FED98
		[DispId(-504)]
		[DefaultValue(BorderStyle.Fixed3D)]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.border_style;
			}
			set
			{
				if (this.border_style != value)
				{
					this.border_style = value;
					(base.Control as ToolStripTextBox.ToolStripTextBoxControl).Border = value;
					this.OnBorderStyleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets a value indicating whether the user can undo the previous operation in a <see cref="T:System.Windows.Forms.ToolStripTextBox" /> control.</summary>
		/// <returns>true if the user can undo the previous operation performed in a text box control; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x060040A5 RID: 16549 RVA: 0x00100BCC File Offset: 0x000FEDCC
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public bool CanUndo
		{
			get
			{
				return this.TextBox.CanUndo;
			}
		}

		/// <summary>Gets or sets whether the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> control modifies the case of characters as they are typed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CharacterCasing" /> values. The default is <see cref="F:System.Windows.Forms.CharacterCasing.Normal" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x060040A6 RID: 16550 RVA: 0x00100BDC File Offset: 0x000FEDDC
		// (set) Token: 0x060040A7 RID: 16551 RVA: 0x00100BEC File Offset: 0x000FEDEC
		[DefaultValue(CharacterCasing.Normal)]
		public CharacterCasing CharacterCasing
		{
			get
			{
				return this.TextBox.CharacterCasing;
			}
			set
			{
				this.TextBox.CharacterCasing = value;
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
		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x060040A8 RID: 16552 RVA: 0x00100BFC File Offset: 0x000FEDFC
		// (set) Token: 0x060040A9 RID: 16553 RVA: 0x00100C0C File Offset: 0x000FEE0C
		[DefaultValue(true)]
		public bool HideSelection
		{
			get
			{
				return this.TextBox.HideSelection;
			}
			set
			{
				this.TextBox.HideSelection = value;
			}
		}

		/// <summary>Gets or sets the lines of text in a <see cref="T:System.Windows.Forms.ToolStripTextBox" /> control.</summary>
		/// <returns>An array of strings that contains the text in a text box control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x060040AA RID: 16554 RVA: 0x00100C1C File Offset: 0x000FEE1C
		// (set) Token: 0x060040AB RID: 16555 RVA: 0x00100C2C File Offset: 0x000FEE2C
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(0)]
		public string[] Lines
		{
			get
			{
				return this.TextBox.Lines;
			}
			set
			{
				this.TextBox.Lines = value;
			}
		}

		/// <summary>Gets or sets the maximum number of characters the user can type or paste into the text box control.</summary>
		/// <returns>The number of characters that can be entered into the control. The default is 32767 characters.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x060040AC RID: 16556 RVA: 0x00100C3C File Offset: 0x000FEE3C
		// (set) Token: 0x060040AD RID: 16557 RVA: 0x00100C4C File Offset: 0x000FEE4C
		[Localizable(true)]
		[DefaultValue(32767)]
		public int MaxLength
		{
			get
			{
				return this.TextBox.MaxLength;
			}
			set
			{
				this.TextBox.MaxLength = value;
			}
		}

		/// <summary>Gets or sets a value that indicates that the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> control has been modified by the user since the control was created or its contents were last set.</summary>
		/// <returns>true if the control's contents have been modified; otherwise, false. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x060040AE RID: 16558 RVA: 0x00100C5C File Offset: 0x000FEE5C
		// (set) Token: 0x060040AF RID: 16559 RVA: 0x00100C6C File Offset: 0x000FEE6C
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public bool Modified
		{
			get
			{
				return this.TextBox.Modified;
			}
			set
			{
				this.TextBox.Modified = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x060040B0 RID: 16560 RVA: 0x00100C7C File Offset: 0x000FEE7C
		// (set) Token: 0x060040B1 RID: 16561 RVA: 0x00100C8C File Offset: 0x000FEE8C
		[Browsable(false)]
		[DefaultValue(false)]
		[RefreshProperties(1)]
		[Localizable(true)]
		[EditorBrowsable(1)]
		public bool Multiline
		{
			get
			{
				return this.TextBox.Multiline;
			}
			set
			{
				this.TextBox.Multiline = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether text in the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> is read-only; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x060040B2 RID: 16562 RVA: 0x00100C9C File Offset: 0x000FEE9C
		// (set) Token: 0x060040B3 RID: 16563 RVA: 0x00100CAC File Offset: 0x000FEEAC
		[DefaultValue(false)]
		public bool ReadOnly
		{
			get
			{
				return this.TextBox.ReadOnly;
			}
			set
			{
				this.TextBox.ReadOnly = value;
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
		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x060040B4 RID: 16564 RVA: 0x00100CBC File Offset: 0x000FEEBC
		// (set) Token: 0x060040B5 RID: 16565 RVA: 0x00100CCC File Offset: 0x000FEECC
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public string SelectedText
		{
			get
			{
				return this.TextBox.SelectedText;
			}
			set
			{
				this.TextBox.SelectedText = value;
			}
		}

		/// <summary>Gets or sets the number of characters selected in the<see cref="T:System.Windows.Forms.ToolStripTextBox" />.</summary>
		/// <returns>The number of characters selected in the<see cref="T:System.Windows.Forms.ToolStripTextBox" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x060040B6 RID: 16566 RVA: 0x00100CDC File Offset: 0x000FEEDC
		// (set) Token: 0x060040B7 RID: 16567 RVA: 0x00100D0C File Offset: 0x000FEF0C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int SelectionLength
		{
			get
			{
				return (this.TextBox.SelectionLength != -1) ? this.TextBox.SelectionLength : 0;
			}
			set
			{
				this.TextBox.SelectionLength = value;
			}
		}

		/// <summary>Gets or sets the starting point of text selected in the<see cref="T:System.Windows.Forms.ToolStripTextBox" />.</summary>
		/// <returns>The starting position of text selected in the<see cref="T:System.Windows.Forms.ToolStripTextBox" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x060040B8 RID: 16568 RVA: 0x00100D1C File Offset: 0x000FEF1C
		// (set) Token: 0x060040B9 RID: 16569 RVA: 0x00100D2C File Offset: 0x000FEF2C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int SelectionStart
		{
			get
			{
				return this.TextBox.SelectionStart;
			}
			set
			{
				this.TextBox.SelectionStart = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the defined shortcuts are enabled.</summary>
		/// <returns>true to enable the shortcuts; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x060040BA RID: 16570 RVA: 0x00100D3C File Offset: 0x000FEF3C
		// (set) Token: 0x060040BB RID: 16571 RVA: 0x00100D4C File Offset: 0x000FEF4C
		[DefaultValue(true)]
		public bool ShortcutsEnabled
		{
			get
			{
				return this.TextBox.ShortcutsEnabled;
			}
			set
			{
				this.TextBox.ShortcutsEnabled = value;
			}
		}

		/// <summary>Gets the hosted <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		/// <returns>The hosted <see cref="T:System.Windows.Forms.TextBox" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x060040BC RID: 16572 RVA: 0x00100D5C File Offset: 0x000FEF5C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public TextBox TextBox
		{
			get
			{
				return (TextBox)base.Control;
			}
		}

		/// <summary>Gets or sets how text is aligned in a <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> enumeration values that specifies how text is aligned in the control. The default is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x060040BD RID: 16573 RVA: 0x00100D6C File Offset: 0x000FEF6C
		// (set) Token: 0x060040BE RID: 16574 RVA: 0x00100D7C File Offset: 0x000FEF7C
		[Localizable(true)]
		[DefaultValue(HorizontalAlignment.Left)]
		public HorizontalAlignment TextBoxTextAlign
		{
			get
			{
				return this.TextBox.TextAlign;
			}
			set
			{
				this.TextBox.TextAlign = value;
			}
		}

		/// <summary>Gets the length of text in the control.</summary>
		/// <returns>The number of characters contained in the text of the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x060040BF RID: 16575 RVA: 0x00100D8C File Offset: 0x000FEF8C
		[Browsable(false)]
		public int TextLength
		{
			get
			{
				return this.TextBox.TextLength;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x060040C0 RID: 16576 RVA: 0x00100D9C File Offset: 0x000FEF9C
		// (set) Token: 0x060040C1 RID: 16577 RVA: 0x00100DAC File Offset: 0x000FEFAC
		[DefaultValue(true)]
		[Localizable(true)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public bool WordWrap
		{
			get
			{
				return this.TextBox.WordWrap;
			}
			set
			{
				this.TextBox.WordWrap = value;
			}
		}

		/// <summary>Gets the spacing, in pixels, between the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> and adjacent items.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the spacing.</returns>
		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x060040C2 RID: 16578 RVA: 0x00100DBC File Offset: 0x000FEFBC
		protected internal override Padding DefaultMargin
		{
			get
			{
				return new Padding(1, 0, 1, 0);
			}
		}

		/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> in pixels. The default size is 100 pixels by 25 pixels.</returns>
		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x060040C3 RID: 16579 RVA: 0x00100DC8 File Offset: 0x000FEFC8
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 22);
			}
		}

		/// <summary>Appends text to the current text of the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</summary>
		/// <param name="text">The text to append to the current contents of the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060040C4 RID: 16580 RVA: 0x00100DD4 File Offset: 0x000FEFD4
		public void AppendText(string text)
		{
			this.TextBox.AppendText(text);
		}

		/// <summary>Clears all text from the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060040C5 RID: 16581 RVA: 0x00100DE4 File Offset: 0x000FEFE4
		public void Clear()
		{
			this.TextBox.Clear();
		}

		/// <summary>Clears information about the most recent operation from the undo buffer of the <see cref="T:System.Windows.Forms.ToolStripTextBox" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060040C6 RID: 16582 RVA: 0x00100DF4 File Offset: 0x000FEFF4
		public void ClearUndo()
		{
			this.TextBox.ClearUndo();
		}

		/// <summary>Copies the current selection in the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> to the Clipboard.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060040C7 RID: 16583 RVA: 0x00100E04 File Offset: 0x000FF004
		public void Copy()
		{
			this.TextBox.Copy();
		}

		/// <summary>Moves the current selection in the <see cref="T:System.Windows.Forms.ToolStripTextBox" /> to the Clipboard.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060040C8 RID: 16584 RVA: 0x00100E14 File Offset: 0x000FF014
		public void Cut()
		{
			this.TextBox.Cut();
		}

		/// <summary>Specifies that the value of the <see cref="P:System.Windows.Forms.ToolStripTextBox.SelectionLength" /> property is zero so that no characters are selected in the control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060040C9 RID: 16585 RVA: 0x00100E24 File Offset: 0x000FF024
		public void DeselectAll()
		{
			this.TextBox.DeselectAll();
		}

		/// <summary>Retrieves the character that is closest to the specified location within the control.</summary>
		/// <returns>The character at the specified location.</returns>
		/// <param name="pt">The location from which to seek the nearest character.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060040CA RID: 16586 RVA: 0x00100E34 File Offset: 0x000FF034
		public char GetCharFromPosition(Point pt)
		{
			return this.TextBox.GetCharFromPosition(pt);
		}

		/// <summary>Retrieves the index of the character nearest to the specified location.</summary>
		/// <returns>The zero-based character index at the specified location.</returns>
		/// <param name="pt">The location to search.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060040CB RID: 16587 RVA: 0x00100E44 File Offset: 0x000FF044
		public int GetCharIndexFromPosition(Point pt)
		{
			return this.TextBox.GetCharIndexFromPosition(pt);
		}

		/// <summary>Retrieves the index of the first character of a given line.</summary>
		/// <returns>The zero-based character index in the specified line.</returns>
		/// <param name="lineNumber">The line for which to get the index of its first character.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060040CC RID: 16588 RVA: 0x00100E54 File Offset: 0x000FF054
		public int GetFirstCharIndexFromLine(int lineNumber)
		{
			return this.TextBox.GetFirstCharIndexFromLine(lineNumber);
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
		// Token: 0x060040CD RID: 16589 RVA: 0x00100E64 File Offset: 0x000FF064
		public int GetFirstCharIndexOfCurrentLine()
		{
			return this.TextBox.GetFirstCharIndexOfCurrentLine();
		}

		/// <summary>Retrieves the line number from the specified character position within the text of the control.</summary>
		/// <returns>The zero-based line number in which the character index is located.</returns>
		/// <param name="index">The character index position to search.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060040CE RID: 16590 RVA: 0x00100E74 File Offset: 0x000FF074
		public int GetLineFromCharIndex(int index)
		{
			return this.TextBox.GetLineFromCharIndex(index);
		}

		/// <summary>Retrieves the location within the control at the specified character index.</summary>
		/// <returns>The location of the specified character.</returns>
		/// <param name="index">The index of the character for which to retrieve the location.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060040CF RID: 16591 RVA: 0x00100E84 File Offset: 0x000FF084
		public Point GetPositionFromCharIndex(int index)
		{
			return this.TextBox.GetPositionFromCharIndex(index);
		}

		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <param name="constrainingSize">The custom-sized area for a control. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060040D0 RID: 16592 RVA: 0x00100E94 File Offset: 0x000FF094
		public override Size GetPreferredSize(Size constrainingSize)
		{
			return base.GetPreferredSize(constrainingSize);
		}

		/// <summary>Replaces the current selection in the text box with the contents of the Clipboard.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060040D1 RID: 16593 RVA: 0x00100EA0 File Offset: 0x000FF0A0
		public void Paste()
		{
			this.TextBox.Paste();
		}

		/// <summary>Scrolls the contents of the control to the current caret position.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060040D2 RID: 16594 RVA: 0x00100EB0 File Offset: 0x000FF0B0
		public void ScrollToCaret()
		{
			this.TextBox.ScrollToCaret();
		}

		/// <summary>Selects a range of text in the text box.</summary>
		/// <param name="start">The position of the first character in the current text selection within the text box.</param>
		/// <param name="length">The number of characters to select.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060040D3 RID: 16595 RVA: 0x00100EC0 File Offset: 0x000FF0C0
		public void Select(int start, int length)
		{
			this.TextBox.Select(start, length);
		}

		/// <summary>Selects all text in the text box.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060040D4 RID: 16596 RVA: 0x00100ED0 File Offset: 0x000FF0D0
		public void SelectAll()
		{
			this.TextBox.SelectAll();
		}

		/// <summary>Undoes the last edit operation in the text box.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060040D5 RID: 16597 RVA: 0x00100EE0 File Offset: 0x000FF0E0
		public void Undo()
		{
			this.TextBox.Undo();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripTextBox.AcceptsTabChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060040D6 RID: 16598 RVA: 0x00100EF0 File Offset: 0x000FF0F0
		protected virtual void OnAcceptsTabChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripTextBox.AcceptsTabChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripTextBox.BorderStyleChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060040D7 RID: 16599 RVA: 0x00100F24 File Offset: 0x000FF124
		protected virtual void OnBorderStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripTextBox.BorderStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripTextBox.HideSelectionChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060040D8 RID: 16600 RVA: 0x00100F58 File Offset: 0x000FF158
		protected virtual void OnHideSelectionChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripTextBox.HideSelectionChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripTextBox.ModifiedChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060040D9 RID: 16601 RVA: 0x00100F8C File Offset: 0x000FF18C
		protected virtual void OnModifiedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripTextBox.ModifiedChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripTextBox.MultilineChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060040DA RID: 16602 RVA: 0x00100FC0 File Offset: 0x000FF1C0
		protected virtual void OnMultilineChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripTextBox.MultilineChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripTextBox.ReadOnlyChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060040DB RID: 16603 RVA: 0x00100FF4 File Offset: 0x000FF1F4
		protected virtual void OnReadOnlyChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripTextBox.ReadOnlyChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <param name="control">The control from which to subscribe events.</param>
		// Token: 0x060040DC RID: 16604 RVA: 0x00101028 File Offset: 0x000FF228
		protected override void OnSubscribeControlEvents(Control control)
		{
			base.OnSubscribeControlEvents(control);
			this.TextBox.AcceptsTabChanged += new EventHandler(this.HandleAcceptsTabChanged);
			this.TextBox.HideSelectionChanged += new EventHandler(this.HandleHideSelectionChanged);
			this.TextBox.ModifiedChanged += new EventHandler(this.HandleModifiedChanged);
			this.TextBox.MultilineChanged += new EventHandler(this.HandleMultilineChanged);
			this.TextBox.ReadOnlyChanged += new EventHandler(this.HandleReadOnlyChanged);
			this.TextBox.TextAlignChanged += new EventHandler(this.HandleTextAlignChanged);
			this.TextBox.TextChanged += new EventHandler(this.HandleTextChanged);
		}

		/// <param name="control">The control from which to unsubscribe events.</param>
		// Token: 0x060040DD RID: 16605 RVA: 0x001010E0 File Offset: 0x000FF2E0
		protected override void OnUnsubscribeControlEvents(Control control)
		{
			base.OnUnsubscribeControlEvents(control);
		}

		// Token: 0x060040DE RID: 16606 RVA: 0x001010EC File Offset: 0x000FF2EC
		private void HandleTextAlignChanged(object sender, EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripTextBox.TextBoxTextAlignChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x00101120 File Offset: 0x000FF320
		private void HandleReadOnlyChanged(object sender, EventArgs e)
		{
			this.OnReadOnlyChanged(e);
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x0010112C File Offset: 0x000FF32C
		private void HandleMultilineChanged(object sender, EventArgs e)
		{
			this.OnMultilineChanged(e);
		}

		// Token: 0x060040E1 RID: 16609 RVA: 0x00101138 File Offset: 0x000FF338
		private void HandleModifiedChanged(object sender, EventArgs e)
		{
			this.OnModifiedChanged(e);
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x00101144 File Offset: 0x000FF344
		private void HandleHideSelectionChanged(object sender, EventArgs e)
		{
			this.OnHideSelectionChanged(e);
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x00101150 File Offset: 0x000FF350
		private void HandleAcceptsTabChanged(object sender, EventArgs e)
		{
			this.OnAcceptsTabChanged(e);
		}

		// Token: 0x060040E4 RID: 16612 RVA: 0x0010115C File Offset: 0x000FF35C
		private void HandleTextChanged(object sender, EventArgs e)
		{
			this.OnTextChanged(e);
		}

		// Token: 0x04001B75 RID: 7029
		private BorderStyle border_style;

		// Token: 0x02000381 RID: 897
		private class ToolStripTextBoxControl : TextBox
		{
			// Token: 0x060040E6 RID: 16614 RVA: 0x00101170 File Offset: 0x000FF370
			protected override void OnLostFocus(EventArgs e)
			{
				base.OnLostFocus(e);
				base.Invalidate();
			}

			// Token: 0x060040E7 RID: 16615 RVA: 0x00101180 File Offset: 0x000FF380
			protected override void OnMouseEnter(EventArgs e)
			{
				base.OnMouseEnter(e);
				base.Invalidate();
				if (this.ShowToolTips)
				{
					this.ToolTipTimer.Start();
				}
			}

			// Token: 0x060040E8 RID: 16616 RVA: 0x001011B0 File Offset: 0x000FF3B0
			protected override void OnMouseLeave(EventArgs e)
			{
				base.OnMouseLeave(e);
				base.Invalidate();
				this.ToolTipTimer.Stop();
				this.ToolTipWindow.Hide(this);
			}

			// Token: 0x060040E9 RID: 16617 RVA: 0x001011E4 File Offset: 0x000FF3E4
			internal override void OnPaintInternal(PaintEventArgs e)
			{
				base.OnPaintInternal(e);
				if ((this.Focused || base.Entered || this.border == BorderStyle.FixedSingle) && this.border != BorderStyle.None)
				{
					ToolStripRenderer renderer = (base.Parent as ToolStrip).Renderer;
					if (renderer is ToolStripProfessionalRenderer)
					{
						using (Pen pen = new Pen((renderer as ToolStripProfessionalRenderer).ColorTable.ButtonSelectedBorder))
						{
							e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, base.Width - 1, base.Height - 1));
						}
					}
				}
			}

			// Token: 0x170010E5 RID: 4325
			// (set) Token: 0x060040EA RID: 16618 RVA: 0x001012A8 File Offset: 0x000FF4A8
			internal BorderStyle Border
			{
				set
				{
					this.border = value;
					base.Invalidate();
				}
			}

			// Token: 0x170010E6 RID: 4326
			// (set) Token: 0x060040EB RID: 16619 RVA: 0x001012B8 File Offset: 0x000FF4B8
			internal ToolStripItem OwnerItem
			{
				set
				{
					this.owner_item = value;
				}
			}

			// Token: 0x170010E7 RID: 4327
			// (get) Token: 0x060040EC RID: 16620 RVA: 0x001012C4 File Offset: 0x000FF4C4
			private bool ShowToolTips
			{
				get
				{
					return base.Parent != null && (base.Parent as ToolStrip).ShowItemToolTips;
				}
			}

			// Token: 0x170010E8 RID: 4328
			// (get) Token: 0x060040ED RID: 16621 RVA: 0x001012E4 File Offset: 0x000FF4E4
			private Timer ToolTipTimer
			{
				get
				{
					if (this.tooltip_timer == null)
					{
						this.tooltip_timer = new Timer();
						this.tooltip_timer.Enabled = false;
						this.tooltip_timer.Interval = 500;
						this.tooltip_timer.Tick += new EventHandler(this.ToolTipTimer_Tick);
					}
					return this.tooltip_timer;
				}
			}

			// Token: 0x170010E9 RID: 4329
			// (get) Token: 0x060040EE RID: 16622 RVA: 0x00101340 File Offset: 0x000FF540
			private ToolTip ToolTipWindow
			{
				get
				{
					if (this.tooltip_window == null)
					{
						this.tooltip_window = new ToolTip();
					}
					return this.tooltip_window;
				}
			}

			// Token: 0x060040EF RID: 16623 RVA: 0x00101360 File Offset: 0x000FF560
			private void ToolTipTimer_Tick(object o, EventArgs args)
			{
				string toolTip = this.owner_item.GetToolTip();
				if (!string.IsNullOrEmpty(toolTip))
				{
					this.ToolTipWindow.Present(this, toolTip);
				}
				this.ToolTipTimer.Stop();
			}

			// Token: 0x04001B7D RID: 7037
			private BorderStyle border;

			// Token: 0x04001B7E RID: 7038
			private Timer tooltip_timer;

			// Token: 0x04001B7F RID: 7039
			private ToolTip tooltip_window;

			// Token: 0x04001B80 RID: 7040
			private ToolStripItem owner_item;
		}
	}
}
