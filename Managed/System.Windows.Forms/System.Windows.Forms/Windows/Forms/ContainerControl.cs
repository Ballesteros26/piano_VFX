using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides focus-management functionality for controls that can function as a container for other controls. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000A0 RID: 160
	[ClassInterface(1)]
	[ComVisible(true)]
	public class ContainerControl : ScrollableControl, IContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContainerControl" /> class.</summary>
		// Token: 0x06000798 RID: 1944 RVA: 0x00021C8C File Offset: 0x0001FE8C
		public ContainerControl()
		{
			this.active_control = null;
			this.unvalidated_control = null;
			base.ControlRemoved += this.OnControlRemoved;
			this.auto_scale_dimensions = SizeF.Empty;
			this.auto_scale_mode = AutoScaleMode.Inherit;
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ContainerControl.AutoValidate" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400006B RID: 107
		// (add) Token: 0x0600079A RID: 1946 RVA: 0x00021CEC File Offset: 0x0001FEEC
		// (remove) Token: 0x0600079B RID: 1947 RVA: 0x00021D00 File Offset: 0x0001FF00
		[Browsable(false)]
		[EditorBrowsable(1)]
		public event EventHandler AutoValidateChanged
		{
			add
			{
				base.Events.AddHandler(ContainerControl.OnValidateChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ContainerControl.OnValidateChanged, value);
			}
		}

		/// <summary>Activates the specified control.</summary>
		/// <returns>true if the control is successfully activated; otherwise, false.</returns>
		/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> to activate.</param>
		// Token: 0x0600079C RID: 1948 RVA: 0x00021D14 File Offset: 0x0001FF14
		bool IContainerControl.ActivateControl(Control control)
		{
			return base.Select(control);
		}

		/// <summary>Gets or sets the active control on the container control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that is currently active on the <see cref="T:System.Windows.Forms.ContainerControl" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Windows.Forms.Control" /> assigned could not be activated. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x00021D20 File Offset: 0x0001FF20
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x00021D28 File Offset: 0x0001FF28
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Control ActiveControl
		{
			get
			{
				return this.active_control;
			}
			set
			{
				if (value == null || (this.active_control == value && this.active_control.Focused))
				{
					return;
				}
				if (!base.Contains(value))
				{
					throw new ArgumentException("Cannot activate invisible or disabled control.");
				}
				Form form = base.FindForm();
				Control mostDeeplyNestedActiveControl = this.GetMostDeeplyNestedActiveControl((form != null) ? form : this);
				Control commonContainer = this.GetCommonContainer(mostDeeplyNestedActiveControl, value);
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				Control control = mostDeeplyNestedActiveControl;
				bool flag = true;
				Control control2 = commonContainer;
				this.active_control = value;
				while (control != commonContainer && control != null)
				{
					if (control == value)
					{
						control2 = value;
						flag = false;
						break;
					}
					control.FireLeave();
					if (control is ContainerControl)
					{
						((ContainerControl)control).active_control = null;
					}
					if (control.CausesValidation)
					{
						arrayList2.Add(control);
					}
					control = control.Parent;
				}
				Control control3 = null;
				bool flag2;
				if (value == control2)
				{
					flag2 = false;
				}
				else
				{
					flag2 = true;
					control = value;
					while (control != control2 && control != null)
					{
						if (control.CausesValidation)
						{
							flag2 = false;
						}
						control3 = control;
						control = control.Parent;
					}
				}
				Control control4 = this.PerformValidation((form != null) ? form : this, flag2, arrayList2, control3);
				if (control4 != null)
				{
					value = (this.active_control = control4);
					flag = true;
				}
				if (flag)
				{
					control = value;
					while (control != control2 && control != null)
					{
						arrayList.Add(control);
						control = control.Parent;
					}
					if (control2 != null && control == control2 && !(control2 is ContainerControl))
					{
						arrayList.Add(control);
					}
					for (int i = arrayList.Count - 1; i >= 0; i--)
					{
						control = (Control)arrayList[i];
						control.FireEnter();
					}
				}
				control = this;
				Control control5 = this;
				while (control != null)
				{
					if (control.Parent is ContainerControl)
					{
						((ContainerControl)control.Parent).active_control = control5;
						control5 = control.Parent;
					}
					control = control.Parent;
				}
				if (this is Form)
				{
					this.CheckAcceptButton();
				}
				base.ScrollControlIntoView(this.active_control);
				control = this;
				control5 = this;
				while (control != null)
				{
					if (control.Parent is ContainerControl)
					{
						control5 = control.Parent;
					}
					control = control.Parent;
				}
				if (control5.InternalContainsFocus)
				{
					this.SendControlFocus(this.active_control);
				}
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00021FCC File Offset: 0x000201CC
		private Control PerformValidation(ContainerControl top_container, bool postpone_validation, ArrayList validation_chain, Control topmost_under_root)
		{
			this.validation_failed = false;
			if (postpone_validation)
			{
				this.AddValidationChain(top_container, validation_chain);
				return null;
			}
			if (top_container.pending_validation_chain != null)
			{
				int num = top_container.pending_validation_chain.Count - 1;
				if (topmost_under_root == top_container.pending_validation_chain[num])
				{
					top_container.pending_validation_chain.RemoveAt(num);
				}
				this.AddValidationChain(top_container, validation_chain);
				validation_chain = top_container.pending_validation_chain;
				top_container.pending_validation_chain = null;
			}
			for (int i = 0; i < validation_chain.Count; i++)
			{
				if (!this.ValidateControl((Control)validation_chain[i]))
				{
					this.validation_failed = true;
					return (Control)validation_chain[i];
				}
			}
			return null;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00022084 File Offset: 0x00020284
		private void AddValidationChain(ContainerControl top_container, ArrayList validation_chain)
		{
			if (validation_chain.Count == 0)
			{
				return;
			}
			if (top_container.pending_validation_chain == null || top_container.pending_validation_chain.Count == 0)
			{
				top_container.pending_validation_chain = validation_chain;
				return;
			}
			foreach (object obj in validation_chain)
			{
				Control control = (Control)obj;
				if (!top_container.pending_validation_chain.Contains(control))
				{
					top_container.pending_validation_chain.Add(control);
				}
			}
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00022134 File Offset: 0x00020334
		private bool ValidateControl(Control c)
		{
			CancelEventArgs cancelEventArgs = new CancelEventArgs();
			c.FireValidating(cancelEventArgs);
			if (cancelEventArgs.Cancel)
			{
				return false;
			}
			c.FireValidated();
			return true;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00022164 File Offset: 0x00020364
		private Control GetMostDeeplyNestedActiveControl(ContainerControl container)
		{
			Control control = container.ActiveControl;
			while (control is ContainerControl)
			{
				if (((ContainerControl)control).ActiveControl == null)
				{
					break;
				}
				control = ((ContainerControl)control).ActiveControl;
			}
			return control;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x000221AC File Offset: 0x000203AC
		private Control GetCommonContainer(Control active_control, Control value)
		{
			for (Control control = active_control; control != null; control = control.Parent)
			{
				for (Control control2 = value.Parent; control2 != null; control2 = control2.Parent)
				{
					if (control2 == control)
					{
						return control2;
					}
				}
			}
			return null;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000221F4 File Offset: 0x000203F4
		internal void SendControlFocus(Control c)
		{
			if (c.IsHandleCreated)
			{
				XplatUI.SetFocus(c.window.Handle);
			}
		}

		/// <summary>Gets or sets the dimensions that the control was designed to.</summary>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> containing the dots per inch (DPI) or <see cref="T:System.Drawing.Font" /> size that the control was designed to.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The width or height of the <see cref="T:System.Drawing.SizeF" /> value is less than 0 when setting this value.</exception>
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00022214 File Offset: 0x00020414
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x0002221C File Offset: 0x0002041C
		[Localizable(true)]
		[EditorBrowsable(2)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public SizeF AutoScaleDimensions
		{
			get
			{
				return this.auto_scale_dimensions;
			}
			set
			{
				if (this.auto_scale_dimensions != value)
				{
					this.auto_scale_dimensions = value;
					this.PerformAutoScale();
				}
			}
		}

		/// <summary>Gets the scaling factor between the current and design-time automatic scaling dimensions. </summary>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> containing the scaling ratio between the current and design-time scaling automatic scaling dimensions.</returns>
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0002223C File Offset: 0x0002043C
		protected SizeF AutoScaleFactor
		{
			get
			{
				if (this.auto_scale_dimensions.IsEmpty)
				{
					return new SizeF(1f, 1f);
				}
				return new SizeF(this.CurrentAutoScaleDimensions.Width / this.auto_scale_dimensions.Width, this.CurrentAutoScaleDimensions.Height / this.auto_scale_dimensions.Height);
			}
		}

		/// <summary>Gets or sets the automatic scaling mode of the control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AutoScaleMode" /> that represents the current scaling mode. The default is <see cref="F:System.Windows.Forms.AutoScaleMode.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">An <see cref="T:System.Windows.Forms.AutoScaleMode" /> value that is not valid was used to set this property.</exception>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x000222A4 File Offset: 0x000204A4
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x000222AC File Offset: 0x000204AC
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(2)]
		public AutoScaleMode AutoScaleMode
		{
			get
			{
				return this.auto_scale_mode;
			}
			set
			{
				if (this is Form)
				{
					(this as Form).AutoScale = false;
				}
				if (this.auto_scale_mode != value)
				{
					this.auto_scale_mode = value;
					if (this.auto_scale_mode_set)
					{
						this.auto_scale_dimensions = SizeF.Empty;
					}
					this.auto_scale_mode_set = true;
					this.PerformAutoScale();
				}
			}
		}

		/// <returns>A <see cref="T:System.Windows.Forms.BindingContext" /> for the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x00022308 File Offset: 0x00020508
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x00022328 File Offset: 0x00020528
		[Browsable(false)]
		public override BindingContext BindingContext
		{
			get
			{
				if (base.BindingContext == null)
				{
					base.BindingContext = new BindingContext();
				}
				return base.BindingContext;
			}
			set
			{
				base.BindingContext = value;
			}
		}

		/// <summary>Gets the current run-time dimensions of the screen.</summary>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> containing the current dots per inch (DPI) or <see cref="T:System.Drawing.Font" /> size of the screen.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">A Win32 device context could not be created for the current screen.</exception>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00022334 File Offset: 0x00020534
		[EditorBrowsable(2)]
		[Browsable(false)]
		public SizeF CurrentAutoScaleDimensions
		{
			get
			{
				AutoScaleMode autoScaleMode = this.auto_scale_mode;
				if (autoScaleMode == AutoScaleMode.Font)
				{
					Size size = TextRenderer.MeasureText("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890", this.Font);
					int num = (int)Math.Round((double)((float)size.Width / 62f));
					return new SizeF((float)num, (float)size.Height);
				}
				if (autoScaleMode != AutoScaleMode.Dpi)
				{
					return this.auto_scale_dimensions;
				}
				return TextRenderer.GetDpi();
			}
		}

		/// <summary>Gets the form that the container control is assigned to.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Form" /> that the container control is assigned to. This property will return null if the control is hosted inside of Internet Explorer or in another hosting context where there is no parent form. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x000223A0 File Offset: 0x000205A0
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Form ParentForm
		{
			get
			{
				for (Control control = base.Parent; control != null; control = control.Parent)
				{
					if (control is Form)
					{
						return (Form)control;
					}
				}
				return null;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.Control.ImeMode" /> property can be set to an active value, to enable IME support.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x000223DC File Offset: 0x000205DC
		protected override bool CanEnableIme
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x000223E0 File Offset: 0x000205E0
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x000223E8 File Offset: 0x000205E8
		internal void PerformAutoScale(bool called_by_scale)
		{
			if (this.AutoScaleMode == AutoScaleMode.Inherit && !called_by_scale)
			{
				return;
			}
			if (this.layout_suspended > 0 && !called_by_scale)
			{
				this.auto_scale_pending = true;
				return;
			}
			this.auto_scale_pending = false;
			SizeF sizeF = this.AutoScaleFactor;
			if (this.AutoScaleMode == AutoScaleMode.Inherit)
			{
				ContainerControl containerControl = base.FindContainer(base.Parent);
				if (containerControl != null)
				{
					sizeF = containerControl.AutoScaleFactor;
				}
			}
			if (sizeF != new SizeF(1f, 1f))
			{
				this.is_auto_scaling = true;
				base.SuspendLayout();
				base.Scale(sizeF);
				base.ResumeLayout(false);
				this.is_auto_scaling = false;
			}
			this.auto_scale_dimensions = this.CurrentAutoScaleDimensions;
		}

		/// <summary>Performs scaling of the container control and its children.</summary>
		// Token: 0x060007B1 RID: 1969 RVA: 0x000224A0 File Offset: 0x000206A0
		public void PerformAutoScale()
		{
			this.PerformAutoScale(false);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x000224AC File Offset: 0x000206AC
		internal void PerformDelayedAutoScale()
		{
			if (this.auto_scale_pending)
			{
				this.PerformAutoScale();
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x000224C0 File Offset: 0x000206C0
		internal bool IsAutoScaling
		{
			get
			{
				return this.is_auto_scaling;
			}
		}

		/// <summary>Verifies the value of the control losing focus by causing the <see cref="E:System.Windows.Forms.Control.Validating" /> and <see cref="E:System.Windows.Forms.Control.Validated" /> events to occur, in that order. </summary>
		/// <returns>true if validation is successful; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060007B4 RID: 1972 RVA: 0x000224C8 File Offset: 0x000206C8
		public bool Validate()
		{
			if (!ContainerControl.ValidateWarned)
			{
				Console.WriteLine("ContainerControl.Validate is not yet implemented");
				ContainerControl.ValidateWarned = true;
			}
			return true;
		}

		/// <summary>Verifies the value of the control that is losing focus; conditionally dependent on whether automatic validation is turned on. </summary>
		/// <returns>true if validation is successful; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return false.</returns>
		/// <param name="checkAutoValidate">If true, the value of the <see cref="P:System.Windows.Forms.ContainerControl.AutoValidate" /> property is used to determine if validation should be performed; if false, validation is unconditionally performed.</param>
		// Token: 0x060007B5 RID: 1973 RVA: 0x000224E8 File Offset: 0x000206E8
		public bool Validate(bool checkAutoValidate)
		{
			return ((!checkAutoValidate || this.AutoValidate == AutoValidate.Disable) && checkAutoValidate) || this.Validate();
		}

		/// <summary>Causes all of the child controls within a control that support validation to validate their data. </summary>
		/// <returns>true if all of the children validated successfully; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return false.</returns>
		// Token: 0x060007B6 RID: 1974 RVA: 0x0002250C File Offset: 0x0002070C
		[EditorBrowsable(1)]
		[Browsable(false)]
		public virtual bool ValidateChildren()
		{
			return this.ValidateChildren(ValidationConstraints.Selectable);
		}

		/// <summary>Causes all of the child controls within a control that support validation to validate their data. </summary>
		/// <returns>true if all of the children validated successfully; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return false.</returns>
		/// <param name="validationConstraints">Places restrictions on which controls have their <see cref="E:System.Windows.Forms.Control.Validating" /> event raised.</param>
		// Token: 0x060007B7 RID: 1975 RVA: 0x00022518 File Offset: 0x00020718
		[EditorBrowsable(1)]
		[Browsable(false)]
		public virtual bool ValidateChildren(ValidationConstraints validationConstraints)
		{
			bool flag = (validationConstraints & ValidationConstraints.ImmediateChildren) != ValidationConstraints.ImmediateChildren;
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				if (!this.ValidateNestedControls(control, validationConstraints, flag))
				{
					return false;
				}
			}
			return true;
		}

		/// <param name="displayScrollbars">true to show the scroll bars; otherwise, false. </param>
		// Token: 0x060007B8 RID: 1976 RVA: 0x000225A8 File Offset: 0x000207A8
		[EditorBrowsable(2)]
		protected override void AdjustFormScrollbars(bool displayScrollbars)
		{
			base.AdjustFormScrollbars(displayScrollbars);
		}

		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060007B9 RID: 1977 RVA: 0x000225B4 File Offset: 0x000207B4
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x000225C0 File Offset: 0x000207C0
		private void OnControlRemoved(object sender, ControlEventArgs e)
		{
			if (e.Control == this.unvalidated_control)
			{
				this.unvalidated_control = null;
			}
			if (e.Control == this.active_control)
			{
				this.unvalidated_control = null;
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00022600 File Offset: 0x00020800
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			this.OnBindingContextChanged(EventArgs.Empty);
		}

		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x060007BC RID: 1980 RVA: 0x00022614 File Offset: 0x00020814
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return ToolStripManager.ProcessCmdKey(ref msg, keyData) || base.ProcessCmdKey(ref msg, keyData);
		}

		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x060007BD RID: 1981 RVA: 0x0002262C File Offset: 0x0002082C
		[EditorBrowsable(2)]
		protected override bool ProcessDialogChar(char charCode)
		{
			return (base.GetTopLevel() && this.ProcessMnemonic(charCode)) || base.ProcessDialogChar(charCode);
		}

		/// <returns>true if the key was processed by the control; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x060007BE RID: 1982 RVA: 0x0002265C File Offset: 0x0002085C
		protected override bool ProcessDialogKey(Keys keyData)
		{
			Keys keys = keyData & Keys.KeyCode;
			bool flag = true;
			Keys keys2 = keys;
			switch (keys2)
			{
			case Keys.Left:
				flag = false;
				break;
			case Keys.Up:
				flag = false;
				break;
			case Keys.Right:
				break;
			case Keys.Down:
				break;
			default:
				if (keys2 != Keys.Tab)
				{
					goto IL_008D;
				}
				if ((keyData & (Keys.Control | Keys.Alt)) == Keys.None && this.ProcessTabKey((Control.ModifierKeys & Keys.Shift) == Keys.None))
				{
					return true;
				}
				goto IL_008D;
			}
			if (base.SelectNextControl(this.active_control, flag, false, false, true))
			{
				return true;
			}
			IL_008D:
			return base.ProcessDialogKey(keyData);
		}

		/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x060007BF RID: 1983 RVA: 0x00022700 File Offset: 0x00020900
		protected override bool ProcessMnemonic(char charCode)
		{
			bool flag = false;
			Control nextControl = this.active_control;
			for (;;)
			{
				nextControl = base.GetNextControl(nextControl, true);
				if (nextControl != null)
				{
					if (nextControl.ProcessControlMnemonic(charCode))
					{
						break;
					}
				}
				else
				{
					if (flag)
					{
						return false;
					}
					flag = true;
				}
				if (nextControl == this.active_control)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Selects the next available control and makes it the active control.</summary>
		/// <returns>true if a control is selected; otherwise, false.</returns>
		/// <param name="forward">true to cycle forward through the controls in the <see cref="T:System.Windows.Forms.ContainerControl" />; otherwise, false. </param>
		// Token: 0x060007C0 RID: 1984 RVA: 0x00022754 File Offset: 0x00020954
		protected virtual bool ProcessTabKey(bool forward)
		{
			return base.SelectNextControl(this.active_control, forward, true, true, false);
		}

		/// <param name="directed">true to specify the direction of the control to select; otherwise, false. </param>
		/// <param name="forward">true to move forward in the tab order; false to move backward in the tab order. </param>
		// Token: 0x060007C1 RID: 1985 RVA: 0x00022768 File Offset: 0x00020968
		protected override void Select(bool directed, bool forward)
		{
			if (base.Parent != null)
			{
				IContainerControl containerControl = base.Parent.GetContainerControl();
				if (containerControl != null)
				{
					containerControl.ActiveControl = this;
				}
			}
			if (directed && this.auto_select_child)
			{
				base.SelectNextControl(null, forward, true, true, false);
			}
		}

		/// <summary>When overridden by a derived class, updates which button is the default button.</summary>
		// Token: 0x060007C2 RID: 1986 RVA: 0x000227B8 File Offset: 0x000209B8
		protected virtual void UpdateDefaultButton()
		{
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x000227BC File Offset: 0x000209BC
		[EditorBrowsable(2)]
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			if (msg != Msg.WM_SETFOCUS)
			{
				base.WndProc(ref m);
			}
			else if (this.active_control != null)
			{
				base.Select(this.active_control);
			}
			else
			{
				base.WndProc(ref m);
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00022814 File Offset: 0x00020A14
		internal void ChildControlRemoved(Control control)
		{
			ContainerControl containerControl = base.FindForm();
			if (containerControl == null)
			{
				containerControl = this;
			}
			ArrayList arrayList = containerControl.pending_validation_chain;
			if (arrayList != null)
			{
				this.RemoveChildrenFromValidation(arrayList, control);
				if (arrayList.Count == 0)
				{
					containerControl.pending_validation_chain = null;
				}
			}
			if (control == this.active_control || control.Contains(this.active_control))
			{
				base.SelectNextControl(this, true, true, true, true);
				if (control == this.active_control || control.Contains(this.active_control))
				{
					this.active_control = null;
				}
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x000228A8 File Offset: 0x00020AA8
		private bool RemoveChildrenFromValidation(ArrayList validation_chain, Control c)
		{
			if (this.RemoveFromValidationChain(validation_chain, c))
			{
				return true;
			}
			foreach (object obj in c.Controls)
			{
				Control control = (Control)obj;
				if (this.RemoveChildrenFromValidation(validation_chain, control))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00022938 File Offset: 0x00020B38
		private bool RemoveFromValidationChain(ArrayList validation_chain, Control c)
		{
			int num = validation_chain.IndexOf(c);
			if (num > -1)
			{
				this.pending_validation_chain.RemoveAt(num--);
				return true;
			}
			return false;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00022968 File Offset: 0x00020B68
		internal virtual void CheckAcceptButton()
		{
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0002296C File Offset: 0x00020B6C
		private bool ValidateNestedControls(Control c, ValidationConstraints constraints, bool recurse)
		{
			bool flag = true;
			if (!c.CausesValidation)
			{
				flag = true;
			}
			else if (!this.ValidateThisControl(c, constraints))
			{
				flag = true;
			}
			else if (!this.ValidateControl(c))
			{
				flag = false;
			}
			if (recurse)
			{
				foreach (object obj in c.Controls)
				{
					Control control = (Control)obj;
					if (!this.ValidateNestedControls(control, constraints, recurse))
					{
						return false;
					}
				}
				return flag;
			}
			return flag;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00022A2C File Offset: 0x00020C2C
		private bool ValidateThisControl(Control c, ValidationConstraints constraints)
		{
			return constraints == ValidationConstraints.None || (((constraints & ValidationConstraints.Enabled) != ValidationConstraints.Enabled || c.Enabled) && ((constraints & ValidationConstraints.Selectable) != ValidationConstraints.Selectable || c.GetStyle(ControlStyles.Selectable)) && ((constraints & ValidationConstraints.TabStop) != ValidationConstraints.TabStop || c.TabStop) && ((constraints & ValidationConstraints.Visible) != ValidationConstraints.Visible || c.Visible));
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060007CA RID: 1994 RVA: 0x00022AA0 File Offset: 0x00020CA0
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060007CB RID: 1995 RVA: 0x00022AAC File Offset: 0x00020CAC
		[EditorBrowsable(2)]
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			if (this.AutoScaleMode == AutoScaleMode.Font)
			{
				this.PerformAutoScale();
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00022AC8 File Offset: 0x00020CC8
		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);
		}

		/// <summary>Gets or sets a value that indicates whether controls in this container will be automatically validated when the focus changes.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AutoValidate" /> enumerated value that indicates whether contained controls are implicitly validated on focus change. The default is <see cref="F:System.Windows.Forms.AutoValidate.Inherit" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A <see cref="T:System.Windows.Forms.AutoValidate" /> value that is not valid was used to set this property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00022AD4 File Offset: 0x00020CD4
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x00022ADC File Offset: 0x00020CDC
		[Browsable(false)]
		[EditorBrowsable(1)]
		[AmbientValue(AutoValidate.Inherit)]
		public virtual AutoValidate AutoValidate
		{
			get
			{
				return this.auto_validate;
			}
			[MonoTODO("Currently does nothing with the setting")]
			set
			{
				if (this.auto_validate != value)
				{
					this.auto_validate = value;
					this.OnAutoValidateChanged(new EventArgs());
				}
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00022AFC File Offset: 0x00020CFC
		internal bool ShouldSerializeAutoValidate()
		{
			return this.AutoValidate != AutoValidate.Inherit;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ContainerControl.AutoValidateChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060007D0 RID: 2000 RVA: 0x00022B0C File Offset: 0x00020D0C
		protected virtual void OnAutoValidateChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ContainerControl.OnValidateChanged];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x04000785 RID: 1925
		private Control active_control;

		// Token: 0x04000786 RID: 1926
		private Control unvalidated_control;

		// Token: 0x04000787 RID: 1927
		private ArrayList pending_validation_chain;

		// Token: 0x04000788 RID: 1928
		internal bool auto_select_child = true;

		// Token: 0x04000789 RID: 1929
		private SizeF auto_scale_dimensions;

		// Token: 0x0400078A RID: 1930
		private AutoScaleMode auto_scale_mode;

		// Token: 0x0400078B RID: 1931
		private bool auto_scale_mode_set;

		// Token: 0x0400078C RID: 1932
		private bool auto_scale_pending;

		// Token: 0x0400078D RID: 1933
		private bool is_auto_scaling;

		// Token: 0x0400078E RID: 1934
		internal bool validation_failed;

		// Token: 0x0400078F RID: 1935
		[MonoTODO("Stub, not implemented")]
		private static bool ValidateWarned;

		// Token: 0x04000790 RID: 1936
		private AutoValidate auto_validate = AutoValidate.Inherit;

		// Token: 0x04000791 RID: 1937
		private static object OnValidateChanged = new object();
	}
}
