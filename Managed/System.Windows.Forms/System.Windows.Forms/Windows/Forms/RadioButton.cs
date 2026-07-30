using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Enables the user to select a single option from a group of choices when paired with other <see cref="T:System.Windows.Forms.RadioButton" /> controls.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020002B0 RID: 688
	[DefaultProperty("Checked")]
	[DefaultBindingProperty("Checked")]
	[ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem,System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Designer("System.Windows.Forms.Design.RadioButtonDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ClassInterface(1)]
	[DefaultEvent("CheckedChanged")]
	[ComVisible(true)]
	public class RadioButton : ButtonBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.RadioButton" /> class.</summary>
		// Token: 0x06002DDD RID: 11741 RVA: 0x000B1220 File Offset: 0x000AF420
		public RadioButton()
		{
			this.appearance = Appearance.Normal;
			this.auto_check = true;
			this.radiobutton_alignment = 16;
			this.TextAlign = 16;
			this.TabStop = false;
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x000B1250 File Offset: 0x000AF450
		// Note: this type is marked as 'beforefieldinit'.
		static RadioButton()
		{
			RadioButton.AppearanceChangedEvent = new object();
			RadioButton.CheckedChangedEvent = new object();
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.RadioButton.Appearance" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002C4 RID: 708
		// (add) Token: 0x06002DDF RID: 11743 RVA: 0x000B1268 File Offset: 0x000AF468
		// (remove) Token: 0x06002DE0 RID: 11744 RVA: 0x000B127C File Offset: 0x000AF47C
		public event EventHandler AppearanceChanged
		{
			add
			{
				base.Events.AddHandler(RadioButton.AppearanceChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadioButton.AppearanceChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.RadioButton.Checked" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002C5 RID: 709
		// (add) Token: 0x06002DE1 RID: 11745 RVA: 0x000B1290 File Offset: 0x000AF490
		// (remove) Token: 0x06002DE2 RID: 11746 RVA: 0x000B12A4 File Offset: 0x000AF4A4
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(RadioButton.CheckedChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadioButton.CheckedChangedEvent, value);
			}
		}

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.RadioButton" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002C6 RID: 710
		// (add) Token: 0x06002DE3 RID: 11747 RVA: 0x000B12B8 File Offset: 0x000AF4B8
		// (remove) Token: 0x06002DE4 RID: 11748 RVA: 0x000B12C4 File Offset: 0x000AF4C4
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler DoubleClick
		{
			add
			{
				base.DoubleClick += value;
			}
			remove
			{
				base.DoubleClick -= value;
			}
		}

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.RadioButton" /> control with the mouse.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002C7 RID: 711
		// (add) Token: 0x06002DE5 RID: 11749 RVA: 0x000B12D0 File Offset: 0x000AF4D0
		// (remove) Token: 0x06002DE6 RID: 11750 RVA: 0x000B12DC File Offset: 0x000AF4DC
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		// Token: 0x06002DE7 RID: 11751 RVA: 0x000B12E8 File Offset: 0x000AF4E8
		private void PerformDefaultCheck()
		{
			if (!this.auto_check || this.Checked)
			{
				return;
			}
			bool flag = false;
			Control parent = base.Parent;
			if (parent != null)
			{
				for (int i = 0; i < parent.Controls.Count; i++)
				{
					RadioButton radioButton = parent.Controls[i] as RadioButton;
					if (radioButton != null && radioButton.auto_check)
					{
						if (radioButton.check_state == CheckState.Checked)
						{
							flag = true;
							break;
						}
					}
				}
			}
			if (!flag)
			{
				this.Checked = true;
			}
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x000B1380 File Offset: 0x000AF580
		private void UpdateSiblings()
		{
			if (!this.auto_check)
			{
				return;
			}
			Control parent = base.Parent;
			if (parent != null)
			{
				for (int i = 0; i < parent.Controls.Count; i++)
				{
					if (this != parent.Controls[i] && parent.Controls[i] is RadioButton && ((RadioButton)parent.Controls[i]).auto_check)
					{
						parent.Controls[i].TabStop = false;
						((RadioButton)parent.Controls[i]).Checked = false;
					}
				}
			}
			this.TabStop = true;
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x000B1438 File Offset: 0x000AF638
		internal override void Draw(PaintEventArgs pe)
		{
			Rectangle rectangle;
			Rectangle rectangle2;
			Rectangle rectangle3;
			ThemeEngine.Current.CalculateRadioButtonTextAndImageLayout(this, Point.Empty, out rectangle, out rectangle2, out rectangle3);
			if (base.FlatStyle != FlatStyle.System && this.Appearance != Appearance.Button)
			{
				ThemeEngine.Current.DrawRadioButton(pe.Graphics, this, rectangle, rectangle2, rectangle3, pe.ClipRectangle);
			}
			else
			{
				ThemeEngine.Current.DrawRadioButton(pe.Graphics, base.ClientRectangle, this);
			}
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x000B14AC File Offset: 0x000AF6AC
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			if (this.AutoSize)
			{
				return ThemeEngine.Current.CalculateRadioButtonAutoSize(this);
			}
			return base.GetPreferredSizeCore(proposedSize);
		}

		/// <summary>Gets or sets a value determining the appearance of the <see cref="T:System.Windows.Forms.RadioButton" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Appearance" /> values. The default value is <see cref="F:System.Windows.Forms.Appearance.Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.Appearance" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06002DEB RID: 11755 RVA: 0x000B14D8 File Offset: 0x000AF6D8
		// (set) Token: 0x06002DEC RID: 11756 RVA: 0x000B14E0 File Offset: 0x000AF6E0
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
					EventHandler eventHandler = (EventHandler)base.Events[RadioButton.AppearanceChangedEvent];
					if (eventHandler != null)
					{
						eventHandler.Invoke(this, EventArgs.Empty);
					}
					if (base.Parent != null)
					{
						base.Parent.PerformLayout(this, "Appearance");
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.RadioButton.Checked" /> value and the appearance of the control automatically change when the control is clicked.</summary>
		/// <returns>true if the <see cref="P:System.Windows.Forms.RadioButton.Checked" /> value and the appearance of the control automatically change on the <see cref="E:System.Windows.Forms.Control.Click" /> event; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06002DED RID: 11757 RVA: 0x000B154C File Offset: 0x000AF74C
		// (set) Token: 0x06002DEE RID: 11758 RVA: 0x000B1554 File Offset: 0x000AF754
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

		/// <summary>Gets or sets the location of the check box portion of the <see cref="T:System.Windows.Forms.RadioButton" />.</summary>
		/// <returns>One of the valid <see cref="T:System.Drawing.ContentAlignment" /> values. The default value is <see cref="F:System.Drawing.ContentAlignment.MiddleLeft" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x000B1560 File Offset: 0x000AF760
		// (set) Token: 0x06002DF0 RID: 11760 RVA: 0x000B1568 File Offset: 0x000AF768
		[DefaultValue(16)]
		[Localizable(true)]
		public ContentAlignment CheckAlign
		{
			get
			{
				return this.radiobutton_alignment;
			}
			set
			{
				if (value != this.radiobutton_alignment)
				{
					this.radiobutton_alignment = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the control is checked.</summary>
		/// <returns>true if the check box is checked; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x000B1584 File Offset: 0x000AF784
		// (set) Token: 0x06002DF2 RID: 11762 RVA: 0x000B1594 File Offset: 0x000AF794
		[Bindable(true, 0)]
		[DefaultValue(false)]
		[SettingsBindable(true)]
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
					this.UpdateSiblings();
					this.OnCheckedChanged(EventArgs.Empty);
				}
				else if (!value && this.check_state != CheckState.Unchecked)
				{
					this.TabStop = false;
					this.check_state = CheckState.Unchecked;
					base.Invalidate();
					this.OnCheckedChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.</summary>
		/// <returns>true if the user can give focus to this control using the TAB key; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x000B1608 File Offset: 0x000AF808
		// (set) Token: 0x06002DF4 RID: 11764 RVA: 0x000B1610 File Offset: 0x000AF810
		[DefaultValue(false)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		/// <summary>Gets or sets the alignment of the text on the <see cref="T:System.Windows.Forms.RadioButton" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.MiddleLeft" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06002DF5 RID: 11765 RVA: 0x000B161C File Offset: 0x000AF81C
		// (set) Token: 0x06002DF6 RID: 11766 RVA: 0x000B1624 File Offset: 0x000AF824
		[Localizable(true)]
		[DefaultValue(16)]
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

		/// <summary>Overrides <see cref="P:System.Windows.Forms.Control.CreateParams" />.</summary>
		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06002DF7 RID: 11767 RVA: 0x000B1630 File Offset: 0x000AF830
		protected override CreateParams CreateParams
		{
			get
			{
				base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
				base.SetStyle(ControlStyles.UserPaint, true);
				return base.CreateParams;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06002DF8 RID: 11768 RVA: 0x000B164C File Offset: 0x000AF84C
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.RadioButtonDefaultSize;
			}
		}

		/// <summary>Generates a <see cref="E:System.Windows.Forms.Control.Click" /> event for the control, simulating a click by a user.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002DF9 RID: 11769 RVA: 0x000B1658 File Offset: 0x000AF858
		public void PerformClick()
		{
			this.OnClick(EventArgs.Empty);
		}

		/// <summary>Overrides the <see cref="M:System.ComponentModel.Component.ToString" /> method.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002DFA RID: 11770 RVA: 0x000B1668 File Offset: 0x000AF868
		public override string ToString()
		{
			return base.ToString() + ", Checked: " + this.Checked;
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.RadioButton" /> control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.RadioButton.RadioButtonAccessibleObject" /> for the control.</returns>
		// Token: 0x06002DFB RID: 11771 RVA: 0x000B1688 File Offset: 0x000AF888
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			AccessibleObject accessibleObject = base.CreateAccessibilityInstance();
			accessibleObject.role = AccessibleRole.RadioButton;
			return accessibleObject;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckBox.CheckedChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002DFC RID: 11772 RVA: 0x000B16A8 File Offset: 0x000AF8A8
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RadioButton.CheckedChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002DFD RID: 11773 RVA: 0x000B16DC File Offset: 0x000AF8DC
		protected override void OnClick(EventArgs e)
		{
			if (this.auto_check)
			{
				if (!this.Checked)
				{
					this.Checked = true;
				}
			}
			else
			{
				this.Checked = !this.Checked;
			}
			base.OnClick(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002DFE RID: 11774 RVA: 0x000B1724 File Offset: 0x000AF924
		protected override void OnEnter(EventArgs e)
		{
			this.PerformDefaultCheck();
			base.OnEnter(e);
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.OnHandleCreated(System.EventArgs)" /> method.</summary>
		// Token: 0x06002DFF RID: 11775 RVA: 0x000B1734 File Offset: 0x000AF934
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06002E00 RID: 11776 RVA: 0x000B1740 File Offset: 0x000AF940
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			base.OnMouseUp(mevent);
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.ProcessMnemonic(System.Char)" /> method.</summary>
		// Token: 0x06002E01 RID: 11777 RVA: 0x000B174C File Offset: 0x000AF94C
		protected override bool ProcessMnemonic(char charCode)
		{
			if (Control.IsMnemonic(charCode, this.Text))
			{
				base.Select();
				this.PerformClick();
				return true;
			}
			return base.ProcessMnemonic(charCode);
		}

		// Token: 0x04001615 RID: 5653
		internal Appearance appearance;

		// Token: 0x04001616 RID: 5654
		internal bool auto_check;

		// Token: 0x04001617 RID: 5655
		internal ContentAlignment radiobutton_alignment;

		// Token: 0x04001618 RID: 5656
		internal CheckState check_state;

		/// <summary>Provides information about the <see cref="T:System.Windows.Forms.RadioButton" /> control to accessibility client applications.</summary>
		// Token: 0x020002B1 RID: 689
		[ComVisible(true)]
		public class RadioButtonAccessibleObject : ButtonBase.ButtonBaseAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.RadioButton.RadioButtonAccessibleObject" /> class. </summary>
			// Token: 0x06002E02 RID: 11778 RVA: 0x000B1780 File Offset: 0x000AF980
			public RadioButtonAccessibleObject(RadioButton owner)
				: base(owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets a string that describes the default action of the <see cref="T:System.Windows.Forms.RadioButton" /> control.</summary>
			/// <returns>A description of the default action of the <see cref="T:System.Windows.Forms.RadioButton" /> control.</returns>
			// Token: 0x17000BAA RID: 2986
			// (get) Token: 0x06002E03 RID: 11779 RVA: 0x000B1790 File Offset: 0x000AF990
			public override string DefaultAction
			{
				get
				{
					return "Select";
				}
			}

			/// <summary>Gets the role of this accessible object.</summary>
			/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.RadioButton" /> value.</returns>
			// Token: 0x17000BAB RID: 2987
			// (get) Token: 0x06002E04 RID: 11780 RVA: 0x000B1798 File Offset: 0x000AF998
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.RadioButton;
				}
			}

			/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.RadioButton" /> control.</summary>
			/// <returns>If the <see cref="P:System.Windows.Forms.RadioButton.Checked" /> property is set to true, returns <see cref="F:System.Windows.Forms.AccessibleStates.Checked" />.</returns>
			// Token: 0x17000BAC RID: 2988
			// (get) Token: 0x06002E05 RID: 11781 RVA: 0x000B179C File Offset: 0x000AF99C
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

			/// <summary>Raises the <see cref="E:System.Windows.Forms.RadioButton.Click" /> event.</summary>
			// Token: 0x06002E06 RID: 11782 RVA: 0x000B17F4 File Offset: 0x000AF9F4
			public override void DoDefaultAction()
			{
				this.owner.PerformClick();
			}

			// Token: 0x0400161B RID: 5659
			private new RadioButton owner;
		}
	}
}
