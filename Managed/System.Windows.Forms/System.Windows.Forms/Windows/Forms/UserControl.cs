using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides an empty control that can be used to create other controls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003A3 RID: 931
	[ClassInterface(1)]
	[DefaultEvent("Load")]
	[ComVisible(true)]
	[Designer("System.Windows.Forms.Design.UserControlDocumentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[Designer("System.Windows.Forms.Design.ControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DesignerCategory("UserControl")]
	public class UserControl : ContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.UserControl" /> class.</summary>
		// Token: 0x060043F9 RID: 17401 RVA: 0x0010BD88 File Offset: 0x00109F88
		public UserControl()
		{
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		}

		// Token: 0x060043FA RID: 17402 RVA: 0x0010BD9C File Offset: 0x00109F9C
		// Note: this type is marked as 'beforefieldinit'.
		static UserControl()
		{
			UserControl.LoadEvent = new object();
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.UserControl.AutoSize" /> property changes. </summary>
		// Token: 0x14000435 RID: 1077
		// (add) Token: 0x060043FB RID: 17403 RVA: 0x0010BDA8 File Offset: 0x00109FA8
		// (remove) Token: 0x060043FC RID: 17404 RVA: 0x0010BDB4 File Offset: 0x00109FB4
		[EditorBrowsable(0)]
		[Browsable(true)]
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.UserControl.AutoValidate" /> property changes.</summary>
		// Token: 0x14000436 RID: 1078
		// (add) Token: 0x060043FD RID: 17405 RVA: 0x0010BDC0 File Offset: 0x00109FC0
		// (remove) Token: 0x060043FE RID: 17406 RVA: 0x0010BDCC File Offset: 0x00109FCC
		[Browsable(true)]
		[EditorBrowsable(0)]
		public new event EventHandler AutoValidateChanged
		{
			add
			{
				base.AutoValidateChanged += value;
			}
			remove
			{
				base.AutoValidateChanged -= value;
			}
		}

		/// <summary>Occurs before the control becomes visible for the first time.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000437 RID: 1079
		// (add) Token: 0x060043FF RID: 17407 RVA: 0x0010BDD8 File Offset: 0x00109FD8
		// (remove) Token: 0x06004400 RID: 17408 RVA: 0x0010BDEC File Offset: 0x00109FEC
		public event EventHandler Load
		{
			add
			{
				base.Events.AddHandler(UserControl.LoadEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(UserControl.LoadEvent, value);
			}
		}

		/// <filterpriority>1</filterpriority>
		// Token: 0x14000438 RID: 1080
		// (add) Token: 0x06004401 RID: 17409 RVA: 0x0010BE00 File Offset: 0x0010A000
		// (remove) Token: 0x06004402 RID: 17410 RVA: 0x0010BE0C File Offset: 0x0010A00C
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x06004403 RID: 17411 RVA: 0x0010BE18 File Offset: 0x0010A018
		// (set) Token: 0x06004404 RID: 17412 RVA: 0x0010BE20 File Offset: 0x0010A020
		[DesignerSerializationVisibility(1)]
		[Browsable(true)]
		[EditorBrowsable(0)]
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

		/// <summary>Gets or sets how the control will resize itself. </summary>
		/// <returns>A value from the <see cref="T:System.Windows.Forms.AutoSizeMode" /> enumeration. The default is <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly" />.</returns>
		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x06004405 RID: 17413 RVA: 0x0010BE2C File Offset: 0x0010A02C
		// (set) Token: 0x06004406 RID: 17414 RVA: 0x0010BE34 File Offset: 0x0010A034
		[Browsable(true)]
		[Localizable(true)]
		[DefaultValue(AutoSizeMode.GrowOnly)]
		public AutoSizeMode AutoSizeMode
		{
			get
			{
				return base.GetAutoSizeMode();
			}
			set
			{
				if (base.GetAutoSizeMode() != value)
				{
					base.SetAutoSizeMode(value);
				}
			}
		}

		/// <summary>Gets or sets how the control performs validation when the user changes focus to another control. </summary>
		/// <returns>A member of the <see cref="T:System.Windows.Forms.AutoValidate" /> enumeration. The default value for <see cref="T:System.Windows.Forms.UserControl" /> is <see cref="F:System.Windows.Forms.AutoValidate.EnablePreventFocusChange" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x06004407 RID: 17415 RVA: 0x0010BE4C File Offset: 0x0010A04C
		// (set) Token: 0x06004408 RID: 17416 RVA: 0x0010BE54 File Offset: 0x0010A054
		[EditorBrowsable(0)]
		[Browsable(true)]
		public override AutoValidate AutoValidate
		{
			get
			{
				return base.AutoValidate;
			}
			set
			{
				base.AutoValidate = value;
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x06004409 RID: 17417 RVA: 0x0010BE60 File Offset: 0x0010A060
		protected override Size DefaultSize
		{
			get
			{
				return new Size(150, 150);
			}
		}

		/// <returns>The text associated with this control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x0600440A RID: 17418 RVA: 0x0010BE74 File Offset: 0x0010A074
		// (set) Token: 0x0600440B RID: 17419 RVA: 0x0010BE7C File Offset: 0x0010A07C
		[Bindable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <returns>true if all of the children validated successfully; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return false.</returns>
		// Token: 0x0600440C RID: 17420 RVA: 0x0010BE88 File Offset: 0x0010A088
		[Browsable(true)]
		[EditorBrowsable(0)]
		public override bool ValidateChildren()
		{
			return base.ValidateChildren();
		}

		/// <returns>true if all of the children validated successfully; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return false.</returns>
		/// <param name="validationConstraints">Places restrictions on which controls have their <see cref="E:System.Windows.Forms.Control.Validating" /> event raised.</param>
		// Token: 0x0600440D RID: 17421 RVA: 0x0010BE90 File Offset: 0x0010A090
		[Browsable(true)]
		[EditorBrowsable(0)]
		public override bool ValidateChildren(ValidationConstraints validationConstraints)
		{
			return base.ValidateChildren(validationConstraints);
		}

		/// <summary>Raises the CreateControl event.</summary>
		// Token: 0x0600440E RID: 17422 RVA: 0x0010BE9C File Offset: 0x0010A09C
		[EditorBrowsable(2)]
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			this.OnLoad(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.UserControl.Load" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600440F RID: 17423 RVA: 0x0010BEB0 File Offset: 0x0010A0B0
		[EditorBrowsable(2)]
		protected virtual void OnLoad(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[UserControl.LoadEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06004410 RID: 17424 RVA: 0x0010BEE4 File Offset: 0x0010A0E4
		[EditorBrowsable(2)]
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x0010BEF0 File Offset: 0x0010A0F0
		[EditorBrowsable(2)]
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			if (msg != Msg.WM_SETFOCUS)
			{
				base.WndProc(ref m);
			}
			else
			{
				if (this.ActiveControl == null)
				{
					base.SelectNextControl(null, true, true, true, false);
				}
				base.WndProc(ref m);
			}
		}

		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x06004412 RID: 17426 RVA: 0x0010BF40 File Offset: 0x0010A140
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style |= 65536;
				createParams.ExStyle |= 65536;
				return createParams;
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06004413 RID: 17427 RVA: 0x0010BF7C File Offset: 0x0010A17C
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
		}

		/// <summary>Gets or sets the border style of the user control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.BorderStyle.Fixed3D" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x06004414 RID: 17428 RVA: 0x0010BF88 File Offset: 0x0010A188
		// (set) Token: 0x06004415 RID: 17429 RVA: 0x0010BF90 File Offset: 0x0010A190
		[DefaultValue(BorderStyle.None)]
		[EditorBrowsable(0)]
		[Browsable(true)]
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

		// Token: 0x06004416 RID: 17430 RVA: 0x0010BF9C File Offset: 0x0010A19C
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			Size empty = Size.Empty;
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				if (control.is_visible)
				{
					if (control.Dock == DockStyle.Left || control.Dock == DockStyle.Right)
					{
						empty.Width += control.PreferredSize.Width;
					}
					else if (control.Dock == DockStyle.Top || control.Dock == DockStyle.Bottom)
					{
						empty.Height += control.PreferredSize.Height;
					}
				}
			}
			foreach (object obj2 in base.Controls)
			{
				Control control2 = (Control)obj2;
				if (control2.is_visible)
				{
					if (control2.Dock == DockStyle.None)
					{
						if ((control2.Anchor & AnchorStyles.Bottom) != AnchorStyles.Bottom && (control2.Anchor & AnchorStyles.Right) != AnchorStyles.Right)
						{
							empty.Width = Math.Max(empty.Width, control2.Bounds.Right + control2.Margin.Right);
							empty.Height = Math.Max(empty.Height, control2.Bounds.Bottom + control2.Margin.Bottom);
						}
					}
				}
			}
			return empty;
		}
	}
}
