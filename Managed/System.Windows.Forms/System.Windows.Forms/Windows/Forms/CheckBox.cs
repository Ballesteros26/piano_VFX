using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows <see cref="T:System.Windows.Forms.CheckBox" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000073 RID: 115
	[DefaultBindingProperty("CheckState")]
	[ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem,System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ClassInterface(1)]
	[ComVisible(true)]
	[DefaultEvent("CheckedChanged")]
	[DefaultProperty("Checked")]
	public class CheckBox : ButtonBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CheckBox" /> class.</summary>
		// Token: 0x06000502 RID: 1282 RVA: 0x00016A58 File Offset: 0x00014C58
		public CheckBox()
		{
			this.appearance = Appearance.Normal;
			this.auto_check = true;
			this.check_alignment = 16;
			this.TextAlign = 16;
			base.SetStyle(ControlStyles.StandardDoubleClick, false);
			base.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00016A9C File Offset: 0x00014C9C
		// Note: this type is marked as 'beforefieldinit'.
		static CheckBox()
		{
			CheckBox.AppearanceChangedEvent = new object();
			CheckBox.CheckedChangedEvent = new object();
			CheckBox.CheckStateChangedEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.CheckBox.Appearance" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06000504 RID: 1284 RVA: 0x00016ABC File Offset: 0x00014CBC
		// (remove) Token: 0x06000505 RID: 1285 RVA: 0x00016AD0 File Offset: 0x00014CD0
		public event EventHandler AppearanceChanged
		{
			add
			{
				base.Events.AddHandler(CheckBox.AppearanceChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(CheckBox.AppearanceChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.CheckBox.Checked" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06000506 RID: 1286 RVA: 0x00016AE4 File Offset: 0x00014CE4
		// (remove) Token: 0x06000507 RID: 1287 RVA: 0x00016AF8 File Offset: 0x00014CF8
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(CheckBox.CheckedChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(CheckBox.CheckedChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.CheckBox.CheckState" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06000508 RID: 1288 RVA: 0x00016B0C File Offset: 0x00014D0C
		// (remove) Token: 0x06000509 RID: 1289 RVA: 0x00016B20 File Offset: 0x00014D20
		public event EventHandler CheckStateChanged
		{
			add
			{
				base.Events.AddHandler(CheckBox.CheckStateChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(CheckBox.CheckStateChangedEvent, value);
			}
		}

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000050 RID: 80
		// (add) Token: 0x0600050A RID: 1290 RVA: 0x00016B34 File Offset: 0x00014D34
		// (remove) Token: 0x0600050B RID: 1291 RVA: 0x00016B40 File Offset: 0x00014D40
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event MouseEventHandler MouseDoubleClick
		{
			add
			{
				base.MouseDoubleClick += value;
			}
			remove
			{
				base.MouseDoubleClick -= value;
			}
		}

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000051 RID: 81
		// (add) Token: 0x0600050C RID: 1292 RVA: 0x00016B4C File Offset: 0x00014D4C
		// (remove) Token: 0x0600050D RID: 1293 RVA: 0x00016B68 File Offset: 0x00014D68
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler DoubleClick;

		// Token: 0x0600050E RID: 1294 RVA: 0x00016B84 File Offset: 0x00014D84
		internal override void Draw(PaintEventArgs pe)
		{
			Rectangle rectangle;
			Rectangle rectangle2;
			Rectangle rectangle3;
			ThemeEngine.Current.CalculateCheckBoxTextAndImageLayout(this, Point.Empty, out rectangle, out rectangle2, out rectangle3);
			if (base.FlatStyle != FlatStyle.System)
			{
				ThemeEngine.Current.DrawCheckBox(pe.Graphics, this, rectangle, rectangle2, rectangle3, pe.ClipRectangle);
			}
			else
			{
				ThemeEngine.Current.DrawCheckBox(pe.Graphics, base.ClientRectangle, this);
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00016BEC File Offset: 0x00014DEC
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			if (this.AutoSize)
			{
				return ThemeEngine.Current.CalculateCheckBoxAutoSize(this);
			}
			return base.GetPreferredSizeCore(proposedSize);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00016C18 File Offset: 0x00014E18
		internal override void HaveDoubleClick()
		{
			if (this.DoubleClick != null)
			{
				this.DoubleClick.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the value that determines the appearance of a <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Appearance" /> values. The default value is <see cref="F:System.Windows.Forms.Appearance.Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.Appearance" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x00016C38 File Offset: 0x00014E38
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x00016C40 File Offset: 0x00014E40
		[DefaultValue(Appearance.Normal)]
		[Localizable(true)]
		public Appearance Appearance
		{
			get
			{
				return this.appearance;
			}
			set
			{
				if (value != this.appearance)
				{
					this.appearance = value;
					this.OnAppearanceChanged(EventArgs.Empty);
					if (base.Parent != null)
					{
						base.Parent.PerformLayout(this, "Appearance");
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or set a value indicating whether the <see cref="P:System.Windows.Forms.CheckBox.Checked" /> or <see cref="P:System.Windows.Forms.CheckBox.CheckState" /> values and the <see cref="T:System.Windows.Forms.CheckBox" />'s appearance are automatically changed when the <see cref="T:System.Windows.Forms.CheckBox" /> is clicked.</summary>
		/// <returns>true if the <see cref="P:System.Windows.Forms.CheckBox.Checked" /> value or <see cref="P:System.Windows.Forms.CheckBox.CheckState" /> value and the appearance of the control are automatically changed on the <see cref="E:System.Windows.Forms.Control.Click" /> event; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00016C90 File Offset: 0x00014E90
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x00016C98 File Offset: 0x00014E98
		[DefaultValue(true)]
		public bool AutoCheck
		{
			get
			{
				return this.auto_check;
			}
			set
			{
				this.auto_check = value;
			}
		}

		/// <summary>Gets or sets the horizontal and vertical alignment of the check mark on a <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default value is MiddleLeft.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> enumeration values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00016CA4 File Offset: 0x00014EA4
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x00016CAC File Offset: 0x00014EAC
		[Localizable(true)]
		[DefaultValue(16)]
		[Bindable(true)]
		public ContentAlignment CheckAlign
		{
			get
			{
				return this.check_alignment;
			}
			set
			{
				if (value != this.check_alignment)
				{
					this.check_alignment = value;
					if (base.Parent != null)
					{
						base.Parent.PerformLayout(this, "CheckAlign");
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or set a value indicating whether the <see cref="T:System.Windows.Forms.CheckBox" /> is in the checked state.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.CheckBox" /> is in the checked state; otherwise, false. The default value is false.Note:If the <see cref="P:System.Windows.Forms.CheckBox.ThreeState" /> property is set to true, the <see cref="P:System.Windows.Forms.CheckBox.Checked" /> property will return true for either a Checked or Indeterminate<see cref="P:System.Windows.Forms.CheckBox.CheckState" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00016CE4 File Offset: 0x00014EE4
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x00016CF4 File Offset: 0x00014EF4
		[RefreshProperties(1)]
		[DefaultValue(false)]
		[SettingsBindable(true)]
		[Bindable(true)]
		public bool Checked
		{
			get
			{
				return this.check_state != CheckState.Unchecked;
			}
			set
			{
				if (value && this.check_state != CheckState.Checked)
				{
					this.check_state = CheckState.Checked;
					base.Invalidate();
					this.OnCheckedChanged(EventArgs.Empty);
				}
				else if (!value && this.check_state != CheckState.Unchecked)
				{
					this.check_state = CheckState.Unchecked;
					base.Invalidate();
					this.OnCheckedChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the state of the <see cref="T:System.Windows.Forms.CheckBox" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> enumeration values. The default value is Unchecked.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.CheckState" /> enumeration values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00016D5C File Offset: 0x00014F5C
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x00016D64 File Offset: 0x00014F64
		[DefaultValue(CheckState.Unchecked)]
		[Bindable(true)]
		[RefreshProperties(1)]
		public CheckState CheckState
		{
			get
			{
				return this.check_state;
			}
			set
			{
				if (value != this.check_state)
				{
					bool flag = this.check_state != CheckState.Unchecked;
					this.check_state = value;
					if (flag != (this.check_state != CheckState.Unchecked))
					{
						this.OnCheckedChanged(EventArgs.Empty);
					}
					this.OnCheckStateChanged(EventArgs.Empty);
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the alignment of the text on the <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.MiddleLeft" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00016DC0 File Offset: 0x00014FC0
		// (set) Token: 0x0600051C RID: 1308 RVA: 0x00016DC8 File Offset: 0x00014FC8
		[DefaultValue(16)]
		[Localizable(true)]
		public override ContentAlignment TextAlign
		{
			get
			{
				return base.TextAlign;
			}
			set
			{
				base.TextAlign = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.CheckBox" /> will allow three check states rather than two.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.CheckBox" /> is able to display three check states; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00016DD4 File Offset: 0x00014FD4
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x00016DDC File Offset: 0x00014FDC
		[DefaultValue(false)]
		public bool ThreeState
		{
			get
			{
				return this.three_state;
			}
			set
			{
				this.three_state = value;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00016DE8 File Offset: 0x00014FE8
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00016DF0 File Offset: 0x00014FF0
		protected override Size DefaultSize
		{
			get
			{
				return new Size(104, 24);
			}
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		/// <returns>A string that states the control type and the state of the <see cref="P:System.Windows.Forms.CheckBox.CheckState" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000521 RID: 1313 RVA: 0x00016DFC File Offset: 0x00014FFC
		public override string ToString()
		{
			return base.ToString() + ", CheckState: " + (int)this.check_state;
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.CheckBox.CheckBoxAccessibleObject" /> for the control.</returns>
		// Token: 0x06000522 RID: 1314 RVA: 0x00016E1C File Offset: 0x0001501C
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			AccessibleObject accessibleObject = base.CreateAccessibilityInstance();
			accessibleObject.role = AccessibleRole.CheckButton;
			return accessibleObject;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckBox.AppearanceChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000523 RID: 1315 RVA: 0x00016E3C File Offset: 0x0001503C
		protected virtual void OnAppearanceChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CheckBox.AppearanceChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckBox.CheckedChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000524 RID: 1316 RVA: 0x00016E70 File Offset: 0x00015070
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CheckBox.CheckedChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckBox.CheckStateChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000525 RID: 1317 RVA: 0x00016EA4 File Offset: 0x000150A4
		protected virtual void OnCheckStateChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CheckBox.CheckStateChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000526 RID: 1318 RVA: 0x00016ED8 File Offset: 0x000150D8
		protected override void OnClick(EventArgs e)
		{
			if (this.auto_check)
			{
				switch (this.check_state)
				{
				case CheckState.Unchecked:
					if (this.three_state)
					{
						this.CheckState = CheckState.Indeterminate;
					}
					else
					{
						this.CheckState = CheckState.Checked;
					}
					break;
				case CheckState.Checked:
					this.CheckState = CheckState.Unchecked;
					break;
				case CheckState.Indeterminate:
					this.CheckState = CheckState.Checked;
					break;
				}
			}
			base.OnClick(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000527 RID: 1319 RVA: 0x00016F50 File Offset: 0x00015150
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <param name="e"></param>
		// Token: 0x06000528 RID: 1320 RVA: 0x00016F5C File Offset: 0x0001515C
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
		}

		/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06000529 RID: 1321 RVA: 0x00016F68 File Offset: 0x00015168
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			base.OnMouseUp(mevent);
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process.</param>
		// Token: 0x0600052A RID: 1322 RVA: 0x00016F74 File Offset: 0x00015174
		protected override bool ProcessMnemonic(char charCode)
		{
			if (Control.IsMnemonic(charCode, this.Text))
			{
				base.Select();
				this.OnClick(EventArgs.Empty);
				return true;
			}
			return base.ProcessMnemonic(charCode);
		}

		// Token: 0x040006B8 RID: 1720
		internal Appearance appearance;

		// Token: 0x040006B9 RID: 1721
		internal bool auto_check;

		// Token: 0x040006BA RID: 1722
		internal ContentAlignment check_alignment;

		// Token: 0x040006BB RID: 1723
		internal CheckState check_state;

		// Token: 0x040006BC RID: 1724
		internal bool three_state;

		/// <summary>Provides information about the <see cref="T:System.Windows.Forms.CheckBox" /> control to accessibility client applications.</summary>
		// Token: 0x02000074 RID: 116
		[ComVisible(true)]
		public class CheckBoxAccessibleObject : ButtonBase.ButtonBaseAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CheckBox.CheckBoxAccessibleObject" /> class. </summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.CheckBox" /> that owns the <see cref="T:System.Windows.Forms.CheckBox.CheckBoxAccessibleObject" />.</param>
			// Token: 0x0600052B RID: 1323 RVA: 0x00016FAC File Offset: 0x000151AC
			public CheckBoxAccessibleObject(Control owner)
				: base(owner)
			{
				this.owner = (CheckBox)owner;
			}

			/// <summary>Gets a string that describes the default action of the <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
			/// <returns>The description of the default action of the <see cref="T:System.Windows.Forms.CheckBox" /> control.</returns>
			// Token: 0x17000132 RID: 306
			// (get) Token: 0x0600052C RID: 1324 RVA: 0x00016FC4 File Offset: 0x000151C4
			public override string DefaultAction
			{
				get
				{
					return "Select";
				}
			}

			/// <summary>Gets the role of this accessible object.</summary>
			/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.CheckButton" /> value.</returns>
			// Token: 0x17000133 RID: 307
			// (get) Token: 0x0600052D RID: 1325 RVA: 0x00016FCC File Offset: 0x000151CC
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.CheckButton;
				}
			}

			/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values. If the <see cref="P:System.Windows.Forms.CheckBox.CheckState" /> property is set to <see cref="F:System.Windows.Forms.CheckState.Checked" />, this property returns <see cref="F:System.Windows.Forms.AccessibleStates.Checked" />. If <see cref="P:System.Windows.Forms.CheckBox.CheckState" /> is set to <see cref="F:System.Windows.Forms.CheckState.Indeterminate" />, this property returns <see cref="F:System.Windows.Forms.AccessibleStates.Indeterminate" />.</returns>
			// Token: 0x17000134 RID: 308
			// (get) Token: 0x0600052E RID: 1326 RVA: 0x00016FD0 File Offset: 0x000151D0
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates accessibleStates = AccessibleStates.Default;
					if (this.owner.check_state == CheckState.Checked)
					{
						accessibleStates |= AccessibleStates.Checked;
					}
					if (this.owner.Focused)
					{
						accessibleStates |= AccessibleStates.Focused;
					}
					if (this.owner.CanFocus)
					{
						accessibleStates |= AccessibleStates.Focusable;
					}
					return accessibleStates;
				}
			}

			/// <summary>Performs the default action associated with this accessible object.</summary>
			// Token: 0x0600052F RID: 1327 RVA: 0x00017028 File Offset: 0x00015228
			public override void DoDefaultAction()
			{
				this.owner.Checked = !this.owner.Checked;
			}

			// Token: 0x040006C1 RID: 1729
			private new CheckBox owner;
		}
	}
}
