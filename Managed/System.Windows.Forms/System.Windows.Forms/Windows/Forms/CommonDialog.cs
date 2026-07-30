using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Specifies the base class used for displaying dialog boxes on the screen.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200009B RID: 155
	[ToolboxItemFilter("System.Windows.Forms")]
	public abstract class CommonDialog : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CommonDialog" /> class.</summary>
		// Token: 0x0600077D RID: 1917 RVA: 0x00021AD0 File Offset: 0x0001FCD0
		public CommonDialog()
		{
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00021AD8 File Offset: 0x0001FCD8
		// Note: this type is marked as 'beforefieldinit'.
		static CommonDialog()
		{
			CommonDialog.HelpRequestEvent = new object();
		}

		/// <summary>Occurs when the user clicks the Help button on a common dialog box.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000069 RID: 105
		// (add) Token: 0x0600077F RID: 1919 RVA: 0x00021AE4 File Offset: 0x0001FCE4
		// (remove) Token: 0x06000780 RID: 1920 RVA: 0x00021AF8 File Offset: 0x0001FCF8
		public event EventHandler HelpRequest
		{
			add
			{
				base.Events.AddHandler(CommonDialog.HelpRequestEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(CommonDialog.HelpRequestEvent, value);
			}
		}

		/// <summary>Gets or sets an object that contains data about the control. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00021B0C File Offset: 0x0001FD0C
		// (set) Token: 0x06000782 RID: 1922 RVA: 0x00021B14 File Offset: 0x0001FD14
		[Bindable(true)]
		[Localizable(false)]
		[TypeConverter(typeof(StringConverter))]
		[DefaultValue(null)]
		[MWFCategory("Data")]
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00021B20 File Offset: 0x0001FD20
		internal virtual void InitFormsSize(Form form)
		{
			form.Width = 200;
			form.Height = 200;
		}

		/// <summary>When overridden in a derived class, resets the properties of a common dialog box to their default values.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000784 RID: 1924
		public abstract void Reset();

		/// <summary>Runs a common dialog box with a default owner.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.DialogResult.OK" /> if the user clicks OK in the dialog box; otherwise, <see cref="F:System.Windows.Forms.DialogResult.Cancel" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000785 RID: 1925 RVA: 0x00021B38 File Offset: 0x0001FD38
		public DialogResult ShowDialog()
		{
			return this.ShowDialog(null);
		}

		/// <summary>Runs a common dialog box with the specified owner.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.DialogResult.OK" /> if the user clicks OK in the dialog box; otherwise, <see cref="F:System.Windows.Forms.DialogResult.Cancel" />.</returns>
		/// <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window" /> that represents the top-level window that will own the modal dialog box. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000786 RID: 1926 RVA: 0x00021B44 File Offset: 0x0001FD44
		public DialogResult ShowDialog(IWin32Window owner)
		{
			if (this.form != null)
			{
				if (this.RunDialog(this.form.Handle))
				{
					this.form.ShowDialog(owner);
				}
				return this.form.DialogResult;
			}
			if (this.RunDialog((owner != null) ? owner.Handle : IntPtr.Zero))
			{
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}

		/// <summary>Defines the common dialog box hook procedure that is overridden to add specific functionality to a common dialog box.</summary>
		/// <returns>A zero value if the default dialog box procedure processes the message; a nonzero value if the default dialog box procedure ignores the message.</returns>
		/// <param name="hWnd">The handle to the dialog box window. </param>
		/// <param name="msg">The message being received. </param>
		/// <param name="wparam">Additional information about the message. </param>
		/// <param name="lparam">Additional information about the message. </param>
		// Token: 0x06000787 RID: 1927 RVA: 0x00021BB0 File Offset: 0x0001FDB0
		protected virtual IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			return IntPtr.Zero;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CommonDialog.HelpRequest" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.HelpEventArgs" /> that provides the event data. </param>
		// Token: 0x06000788 RID: 1928 RVA: 0x00021BB8 File Offset: 0x0001FDB8
		protected virtual void OnHelpRequest(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CommonDialog.HelpRequestEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Defines the owner window procedure that is overridden to add specific functionality to a common dialog box.</summary>
		/// <returns>The result of the message processing, which is dependent on the message sent.</returns>
		/// <param name="hWnd">The window handle of the message to send. </param>
		/// <param name="msg">The Win32 message to send. </param>
		/// <param name="wparam">The <paramref name="wparam" /> to send with the message. </param>
		/// <param name="lparam">The <paramref name="lparam" /> to send with the message. </param>
		// Token: 0x06000789 RID: 1929 RVA: 0x00021BEC File Offset: 0x0001FDEC
		protected virtual IntPtr OwnerWndProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			return IntPtr.Zero;
		}

		/// <summary>When overridden in a derived class, specifies a common dialog box.</summary>
		/// <returns>true if the dialog box was successfully run; otherwise, false.</returns>
		/// <param name="hwndOwner">A value that represents the window handle of the owner window for the common dialog box. </param>
		// Token: 0x0600078A RID: 1930
		protected abstract bool RunDialog(IntPtr hwndOwner);

		// Token: 0x04000781 RID: 1921
		internal CommonDialog.DialogForm form;

		// Token: 0x04000782 RID: 1922
		private object tag;

		// Token: 0x0200009C RID: 156
		internal class DialogForm : Form
		{
			// Token: 0x0600078B RID: 1931 RVA: 0x00021BF4 File Offset: 0x0001FDF4
			internal DialogForm(CommonDialog owner)
			{
				this.owner = owner;
				base.ControlBox = true;
				base.MinimizeBox = false;
				base.MaximizeBox = false;
				base.ShowInTaskbar = false;
				base.FormBorderStyle = FormBorderStyle.Sizable;
				base.StartPosition = FormStartPosition.CenterScreen;
			}

			// Token: 0x170001BE RID: 446
			// (get) Token: 0x0600078C RID: 1932 RVA: 0x00021C38 File Offset: 0x0001FE38
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.Style |= -2134376448;
					return createParams;
				}
			}

			// Token: 0x0600078D RID: 1933 RVA: 0x00021C60 File Offset: 0x0001FE60
			internal DialogResult RunDialog()
			{
				this.owner.InitFormsSize(this);
				base.ShowDialog();
				return base.DialogResult;
			}

			// Token: 0x04000784 RID: 1924
			protected CommonDialog owner;
		}
	}
}
