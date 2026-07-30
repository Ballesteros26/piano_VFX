using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows button control.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000069 RID: 105
	[Designer("System.Windows.Forms.Design.ButtonBaseDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ComVisible(true)]
	[ClassInterface(1)]
	public class Button : ButtonBase, IButtonControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Button" /> class.</summary>
		// Token: 0x0600048B RID: 1163 RVA: 0x00015890 File Offset: 0x00013A90
		public Button()
		{
			this.dialog_result = DialogResult.None;
			base.SetStyle(ControlStyles.StandardDoubleClick, false);
		}

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.Button" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000049 RID: 73
		// (add) Token: 0x0600048C RID: 1164 RVA: 0x000158AC File Offset: 0x00013AAC
		// (remove) Token: 0x0600048D RID: 1165 RVA: 0x000158B8 File Offset: 0x00013AB8
		[EditorBrowsable(2)]
		[Browsable(false)]
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

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.Button" /> control with the mouse.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400004A RID: 74
		// (add) Token: 0x0600048E RID: 1166 RVA: 0x000158C4 File Offset: 0x00013AC4
		// (remove) Token: 0x0600048F RID: 1167 RVA: 0x000158D0 File Offset: 0x00013AD0
		[Browsable(false)]
		[EditorBrowsable(2)]
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

		/// <summary>Gets or sets the mode by which the <see cref="T:System.Windows.Forms.Button" /> automatically resizes itself.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values. The default value is <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly" />.</returns>
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x000158DC File Offset: 0x00013ADC
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x000158E4 File Offset: 0x00013AE4
		[DefaultValue(AutoSizeMode.GrowOnly)]
		[MWFCategory("Layout")]
		[Localizable(true)]
		[Browsable(true)]
		public AutoSizeMode AutoSizeMode
		{
			get
			{
				return base.GetAutoSizeMode();
			}
			set
			{
				base.SetAutoSizeMode(value);
			}
		}

		/// <summary>Gets or sets a value that is returned to the parent form when the button is clicked.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values. The default value is None.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.DialogResult" /> values. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x000158F0 File Offset: 0x00013AF0
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x000158F8 File Offset: 0x00013AF8
		[DefaultValue(DialogResult.None)]
		[MWFCategory("Behavior")]
		public virtual DialogResult DialogResult
		{
			get
			{
				return this.dialog_result;
			}
			set
			{
				this.dialog_result = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Windows.Forms.CreateParams" /> on the base class when creating a window. </summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" />.</returns>
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00015904 File Offset: 0x00013B04
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Notifies the <see cref="T:System.Windows.Forms.Button" /> whether it is the default button so that it can adjust its appearance accordingly.</summary>
		/// <param name="value">true if the button is to have the appearance of the default button; otherwise, false. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000495 RID: 1173 RVA: 0x0001590C File Offset: 0x00013B0C
		public virtual void NotifyDefault(bool value)
		{
			base.IsDefault = value;
		}

		/// <summary>Generates a <see cref="E:System.Windows.Forms.Control.Click" /> event for a button.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000496 RID: 1174 RVA: 0x00015918 File Offset: 0x00013B18
		public void PerformClick()
		{
			if (base.CanSelect)
			{
				this.OnClick(EventArgs.Empty);
			}
		}

		/// <filterpriority>2</filterpriority>
		// Token: 0x06000497 RID: 1175 RVA: 0x00015930 File Offset: 0x00013B30
		public override string ToString()
		{
			return base.ToString() + ", Text: " + this.Text;
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000498 RID: 1176 RVA: 0x00015948 File Offset: 0x00013B48
		protected override void OnClick(EventArgs e)
		{
			if (this.dialog_result != DialogResult.None)
			{
				Form form = base.FindForm();
				if (form != null)
				{
					form.DialogResult = this.dialog_result;
				}
			}
			base.OnClick(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000499 RID: 1177 RVA: 0x00015980 File Offset: 0x00013B80
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
		}

		/// <param name="e"></param>
		// Token: 0x0600049A RID: 1178 RVA: 0x0001598C File Offset: 0x00013B8C
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
		}

		/// <param name="e"></param>
		// Token: 0x0600049B RID: 1179 RVA: 0x00015998 File Offset: 0x00013B98
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnMouseUp(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x0600049C RID: 1180 RVA: 0x000159A4 File Offset: 0x00013BA4
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			base.OnMouseUp(mevent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600049D RID: 1181 RVA: 0x000159B0 File Offset: 0x00013BB0
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
		}

		/// <summary>Processes a mnemonic character. </summary>
		/// <returns>true if the mnemonic was processed; otherwise, false.</returns>
		/// <param name="charCode">The mnemonic character entered. </param>
		// Token: 0x0600049E RID: 1182 RVA: 0x000159BC File Offset: 0x00013BBC
		protected override bool ProcessMnemonic(char charCode)
		{
			if (base.UseMnemonic && Control.IsMnemonic(charCode, this.Text))
			{
				this.PerformClick();
				return true;
			}
			return base.ProcessMnemonic(charCode);
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x0600049F RID: 1183 RVA: 0x000159F4 File Offset: 0x00013BF4
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00015A00 File Offset: 0x00013C00
		internal override void Draw(PaintEventArgs pevent)
		{
			if (base.FlatStyle == FlatStyle.System)
			{
				base.Draw(pevent);
				return;
			}
			Rectangle rectangle;
			Rectangle rectangle2;
			ThemeEngine.Current.CalculateButtonTextAndImageLayout(this, out rectangle, out rectangle2);
			if (base.FlatStyle == FlatStyle.Standard)
			{
				ThemeEngine.Current.DrawButton(pevent.Graphics, this, rectangle, rectangle2, pevent.ClipRectangle);
			}
			else if (base.FlatStyle == FlatStyle.Flat)
			{
				ThemeEngine.Current.DrawFlatButton(pevent.Graphics, this, rectangle, rectangle2, pevent.ClipRectangle);
			}
			else if (base.FlatStyle == FlatStyle.Popup)
			{
				ThemeEngine.Current.DrawPopupButton(pevent.Graphics, this, rectangle, rectangle2, pevent.ClipRectangle);
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00015AA8 File Offset: 0x00013CA8
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			if (this.AutoSize)
			{
				return ThemeEngine.Current.CalculateButtonAutoSize(this);
			}
			return base.GetPreferredSizeCore(proposedSize);
		}

		// Token: 0x04000689 RID: 1673
		private DialogResult dialog_result;
	}
}
