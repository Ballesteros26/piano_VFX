using System;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Displays a message box that can contain text, buttons, and symbols that inform and instruct the user.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000257 RID: 599
	public class MessageBox
	{
		// Token: 0x06002759 RID: 10073 RVA: 0x00095F28 File Offset: 0x00094128
		private MessageBox()
		{
		}

		/// <summary>Displays a message box with specified text.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="text">The text to display in the message box. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600275A RID: 10074 RVA: 0x00095F30 File Offset: 0x00094130
		public static DialogResult Show(string text)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(null, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box in front of the specified object and with the specified text.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="owner"></param>
		/// <param name="text"></param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600275B RID: 10075 RVA: 0x00095F54 File Offset: 0x00094154
		public static DialogResult Show(IWin32Window owner, string text)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(owner, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box with specified text and caption.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption">The text to display in the title bar of the message box. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600275C RID: 10076 RVA: 0x00095F78 File Offset: 0x00094178
		public static DialogResult Show(string text, string caption)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box with specified text, caption, and buttons.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption">The text to display in the title bar of the message box. </param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="buttons" /> parameter specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600275D RID: 10077 RVA: 0x00095F98 File Offset: 0x00094198
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(null, text, caption, buttons, MessageBoxIcon.None);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box in front of the specified object and with the specified text, caption, and buttons.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="owner"></param>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption"></param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600275E RID: 10078 RVA: 0x00095FB8 File Offset: 0x000941B8
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(owner, text, caption, buttons, MessageBoxIcon.None);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box in front of the specified object and with the specified text, caption, buttons, and icon.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="owner"></param>
		/// <param name="text"></param>
		/// <param name="caption"></param>
		/// <param name="buttons"></param>
		/// <param name="icon"></param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.-or- <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600275F RID: 10079 RVA: 0x00095FD8 File Offset: 0x000941D8
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(owner, text, caption, buttons, icon);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box in front of the specified object and with the specified text and caption.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window" /> that will own the modal dialog box.</param>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption">The text to display in the title bar of the message box. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002760 RID: 10080 RVA: 0x00095FF8 File Offset: 0x000941F8
		public static DialogResult Show(IWin32Window owner, string text, string caption)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box with specified text, caption, buttons, and icon.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption">The text to display in the title bar of the message box. </param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box. </param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="buttons" /> parameter specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.-or- The <paramref name="icon" /> parameter specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002761 RID: 10081 RVA: 0x00096018 File Offset: 0x00094218
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(null, text, caption, buttons, icon);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, and default button.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption">The text to display in the title bar of the message box. </param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box. </param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.-or- <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.-or- <paramref name="defaultButton" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002762 RID: 10082 RVA: 0x00096038 File Offset: 0x00094238
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(null, text, caption, buttons, icon, defaultButton, MessageBoxOptions.DefaultDesktopOnly, false);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box in front of the specified object and with the specified text, caption, buttons, icon, and default button.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="owner"></param>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption"></param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box. </param>
		/// <param name="icon"></param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.-or- <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.-or- <paramref name="defaultButton" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002763 RID: 10083 RVA: 0x00096060 File Offset: 0x00094260
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(owner, text, caption, buttons, icon, defaultButton, MessageBoxOptions.DefaultDesktopOnly, false);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, and options.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption">The text to display in the title bar of the message box. </param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box. </param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box. </param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.-or- <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.-or- The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.-or- <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002764 RID: 10084 RVA: 0x00096088 File Offset: 0x00094288
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(null, text, caption, buttons, icon, defaultButton, options, false);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box in front of the specified object and with the specified text, caption, buttons, icon, default button, and options.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="owner"></param>
		/// <param name="text"></param>
		/// <param name="caption"></param>
		/// <param name="buttons"></param>
		/// <param name="icon"></param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values the specifies the default button for the message box. </param>
		/// <param name="options"></param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.-or- <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.-or- <paramref name="defaultButton" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.-or- <paramref name="options" /> specified <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> or <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" /> and specified a value in the <paramref name="owner" /> parameter. These two options should be used only if you invoke the version of this method that does not take an <paramref name="owner" /> parameter.-or- <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002765 RID: 10085 RVA: 0x000960AC File Offset: 0x000942AC
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(owner, text, caption, buttons, icon, defaultButton, options, false);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption">The text to display in the title bar of the message box. </param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box. </param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box. </param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <param name="displayHelpButton"></param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.-or- <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.-or- The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.-or- <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002766 RID: 10086 RVA: 0x000960D0 File Offset: 0x000942D0
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool displayHelpButton)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(null, text, caption, buttons, icon, defaultButton, options, displayHelpButton);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption">The text to display in the title bar of the message box. </param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box. </param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box. </param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.-or- <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.-or- The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.-or- <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002767 RID: 10087 RVA: 0x000960F4 File Offset: 0x000942F4
		[MonoTODO("Help is not implemented")]
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(null, text, caption, buttons, icon, defaultButton, options, true);
			messageBoxForm.SetHelpData(helpFilePath, null, HelpNavigator.TableOfContents, null);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and Help keyword.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption">The text to display in the title bar of the message box. </param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box. </param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box. </param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button. </param>
		/// <param name="keyword">The Help keyword to display when the user clicks the Help button. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.-or- <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.-or- The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.-or- <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002768 RID: 10088 RVA: 0x00096128 File Offset: 0x00094328
		[MonoTODO("Help is not implemented")]
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(null, text, caption, buttons, icon, defaultButton, options, true);
			messageBoxForm.SetHelpData(helpFilePath, keyword, HelpNavigator.TableOfContents, null);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and HelpNavigator.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption">The text to display in the title bar of the message box. </param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box. </param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box. </param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button. </param>
		/// <param name="navigator">One of the <see cref="T:System.Windows.Forms.HelpNavigator" /> values. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.-or- <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.-or- The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.-or- <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002769 RID: 10089 RVA: 0x0009615C File Offset: 0x0009435C
		[MonoTODO("Help is not implemented")]
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(null, text, caption, buttons, icon, defaultButton, options, true);
			messageBoxForm.SetHelpData(helpFilePath, null, navigator, null);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file, HelpNavigator, and Help topic.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption">The text to display in the title bar of the message box. </param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box. </param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box. </param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button. </param>
		/// <param name="navigator">One of the <see cref="T:System.Windows.Forms.HelpNavigator" /> values. </param>
		/// <param name="param">The numeric ID of the Help topic to display when the user clicks the Help button. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.-or- <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.-or- The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.-or- <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600276A RID: 10090 RVA: 0x0009618C File Offset: 0x0009438C
		[MonoTODO("Help is not implemented")]
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(null, text, caption, buttons, icon, defaultButton, options, true);
			messageBoxForm.SetHelpData(helpFilePath, null, navigator, param);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="owner"></param>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption"></param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box. </param>
		/// <param name="icon"></param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box. </param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.-or- <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.-or- The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.-or- <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600276B RID: 10091 RVA: 0x000961BC File Offset: 0x000943BC
		[MonoTODO("Help is not implemented")]
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(owner, text, caption, buttons, icon, defaultButton, options, true);
			messageBoxForm.SetHelpData(helpFilePath, null, HelpNavigator.TableOfContents, null);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and Help keyword.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window" /> that will own the modal dialog box.</param>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption">The text to display in the title bar of the message box. </param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box. </param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box. </param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button. </param>
		/// <param name="keyword">The Help keyword to display when the user clicks the Help button. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.-or- <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.-or- The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.-or- <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600276C RID: 10092 RVA: 0x000961F0 File Offset: 0x000943F0
		[MonoTODO("Help is not implemented")]
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(owner, text, caption, buttons, icon, defaultButton, options, true);
			messageBoxForm.SetHelpData(helpFilePath, keyword, HelpNavigator.TableOfContents, null);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file and HelpNavigator.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="owner"></param>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption"></param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box. </param>
		/// <param name="icon"></param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box. </param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button. </param>
		/// <param name="navigator"></param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.-or- <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.-or- The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.-or- <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600276D RID: 10093 RVA: 0x00096224 File Offset: 0x00094424
		[MonoTODO("Help is not implemented")]
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(owner, text, caption, buttons, icon, defaultButton, options, true);
			messageBoxForm.SetHelpData(helpFilePath, null, navigator, null);
			return messageBoxForm.RunDialog();
		}

		/// <summary>Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, using the specified Help file, HelpNavigator, and Help topic.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window" /> that will own the modal dialog box.</param>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption">The text to display in the title bar of the message box. </param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box. </param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values that specifies the default button for the message box. </param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <param name="helpFilePath">The path and name of the Help file to display when the user clicks the Help button. </param>
		/// <param name="navigator">One of the <see cref="T:System.Windows.Forms.HelpNavigator" /> values. </param>
		/// <param name="param">The numeric ID of the Help topic to display when the user clicks the Help button. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.-or- <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.-or- The <paramref name="defaultButton" /> specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.-or- <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600276E RID: 10094 RVA: 0x00096254 File Offset: 0x00094454
		[MonoTODO("Help is not implemented")]
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param)
		{
			MessageBox.MessageBoxForm messageBoxForm = new MessageBox.MessageBoxForm(owner, text, caption, buttons, icon, defaultButton, options, true);
			messageBoxForm.SetHelpData(helpFilePath, null, navigator, param);
			return messageBoxForm.RunDialog();
		}

		// Token: 0x02000258 RID: 600
		internal class MessageBoxForm : Form
		{
			// Token: 0x0600276F RID: 10095 RVA: 0x00096288 File Offset: 0x00094488
			public MessageBoxForm(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, bool displayHelpButton)
			{
				this.show_help = displayHelpButton;
				if (icon != MessageBoxIcon.None)
				{
					if (icon != MessageBoxIcon.Error)
					{
						if (icon != MessageBoxIcon.Question)
						{
							if (icon != MessageBoxIcon.Exclamation)
							{
								if (icon == MessageBoxIcon.Asterisk)
								{
									this.icon_image = SystemIcons.Information;
									this.alert_type = AlertType.Information;
								}
							}
							else
							{
								this.icon_image = SystemIcons.Warning;
								this.alert_type = AlertType.Warning;
							}
						}
						else
						{
							this.icon_image = SystemIcons.Question;
							this.alert_type = AlertType.Question;
						}
					}
					else
					{
						this.icon_image = SystemIcons.Error;
						this.alert_type = AlertType.Error;
					}
				}
				else
				{
					this.icon_image = null;
					this.alert_type = AlertType.Default;
				}
				this.msgbox_text = text;
				this.msgbox_buttons = buttons;
				this.msgbox_default = MessageBoxDefaultButton.Button1;
				if (owner != null)
				{
					base.Owner = Control.FromHandle(owner.Handle).FindForm();
				}
				else if (Application.MWFThread.Current.Context != null)
				{
					base.Owner = Application.MWFThread.Current.Context.MainForm;
				}
				this.Text = caption;
				base.ControlBox = true;
				base.MinimizeBox = false;
				base.MaximizeBox = false;
				base.ShowInTaskbar = base.Owner == null;
				base.FormBorderStyle = FormBorderStyle.FixedDialog;
			}

			// Token: 0x06002770 RID: 10096 RVA: 0x000963DC File Offset: 0x000945DC
			public MessageBoxForm(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool displayHelpButton)
				: this(owner, text, caption, buttons, icon, displayHelpButton)
			{
				this.msgbox_default = defaultButton;
			}

			// Token: 0x06002771 RID: 10097 RVA: 0x000963F8 File Offset: 0x000945F8
			public MessageBoxForm(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
				: this(owner, text, caption, buttons, icon, false)
			{
			}

			// Token: 0x170009B7 RID: 2487
			// (get) Token: 0x06002772 RID: 10098 RVA: 0x00096408 File Offset: 0x00094608
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.Style |= 113246208;
					if (!this.is_enabled)
					{
						createParams.Style |= 134217728;
					}
					return createParams;
				}
			}

			// Token: 0x06002773 RID: 10099 RVA: 0x0009644C File Offset: 0x0009464C
			public void SetHelpData(string file_path, string keyword, HelpNavigator navigator, object param)
			{
				this.help_file_path = file_path;
				this.help_keyword = keyword;
				this.help_navigator = navigator;
				this.help_param = param;
			}

			// Token: 0x170009B8 RID: 2488
			// (get) Token: 0x06002774 RID: 10100 RVA: 0x0009646C File Offset: 0x0009466C
			internal string HelpFilePath
			{
				get
				{
					return this.help_file_path;
				}
			}

			// Token: 0x170009B9 RID: 2489
			// (get) Token: 0x06002775 RID: 10101 RVA: 0x00096474 File Offset: 0x00094674
			internal string HelpKeyword
			{
				get
				{
					return this.help_keyword;
				}
			}

			// Token: 0x170009BA RID: 2490
			// (get) Token: 0x06002776 RID: 10102 RVA: 0x0009647C File Offset: 0x0009467C
			internal HelpNavigator HelpNavigator
			{
				get
				{
					return this.help_navigator;
				}
			}

			// Token: 0x170009BB RID: 2491
			// (get) Token: 0x06002777 RID: 10103 RVA: 0x00096484 File Offset: 0x00094684
			internal object HelpParam
			{
				get
				{
					return this.help_param;
				}
			}

			// Token: 0x06002778 RID: 10104 RVA: 0x0009648C File Offset: 0x0009468C
			public DialogResult RunDialog()
			{
				base.StartPosition = FormStartPosition.CenterScreen;
				if (!this.size_known)
				{
					this.InitFormsSize();
				}
				if (base.Owner != null)
				{
					base.TopMost = base.Owner.TopMost;
				}
				XplatUI.AudibleAlert(this.alert_type);
				base.ShowDialog();
				return base.DialogResult;
			}

			// Token: 0x06002779 RID: 10105 RVA: 0x000964E8 File Offset: 0x000946E8
			internal override void OnPaintInternal(PaintEventArgs e)
			{
				e.Graphics.DrawString(this.msgbox_text, this.Font, ThemeEngine.Current.ResPool.GetSolidBrush(Color.Black), this.text_rect);
				if (this.icon_image != null)
				{
					e.Graphics.DrawIcon(this.icon_image, 10, 10);
				}
			}

			// Token: 0x0600277A RID: 10106 RVA: 0x00096548 File Offset: 0x00094748
			private void InitFormsSize()
			{
				int num = (int)((double)Screen.GetWorkingArea(this).Width * 0.6);
				SizeF sizeF = TextRenderer.MeasureString(this.msgbox_text, this.Font, num);
				this.text_rect.Size = sizeF;
				if (this.icon_image != null)
				{
					sizeF.Width += (float)(this.icon_image.Width + 10);
					if ((float)this.icon_image.Height > sizeF.Height)
					{
						this.text_rect.Location = new Point(this.icon_image.Width + 10 + 10, (int)((float)(this.icon_image.Height / 2) - sizeF.Height / 2f) + 10);
					}
					else
					{
						this.text_rect.Location = new Point(this.icon_image.Width + 10 + 10, 12);
					}
					if (sizeF.Height < (float)this.icon_image.Height)
					{
						sizeF.Height = (float)this.icon_image.Height;
					}
				}
				else
				{
					this.text_rect.Location = new Point(15, 10);
				}
				sizeF.Height += 20f;
				int num2;
				switch (this.msgbox_buttons)
				{
				case MessageBoxButtons.OK:
					num2 = 1;
					break;
				case MessageBoxButtons.OKCancel:
					num2 = 2;
					break;
				case MessageBoxButtons.AbortRetryIgnore:
					num2 = 3;
					break;
				case MessageBoxButtons.YesNoCancel:
					num2 = 3;
					break;
				case MessageBoxButtons.YesNo:
					num2 = 2;
					break;
				case MessageBoxButtons.RetryCancel:
					num2 = 2;
					break;
				default:
					num2 = 0;
					break;
				}
				if (this.show_help)
				{
					num2++;
				}
				int num3 = 91 * num2;
				SizeF sizeF2;
				sizeF2..ctor(Math.Min(Math.Max(TextRenderer.MeasureString(this.Text, new Font(Control.DefaultFont, 1)).Width + 40f, sizeF.Width), (float)num), sizeF.Height);
				Size size = sizeF2.ToSize();
				if (size.Width > num3)
				{
					int num4 = size.Width + 20;
					int num5 = size.Height + 40;
					base.Height = num5;
					base.ClientSize = new Size(num4, num5);
				}
				else
				{
					int num6 = num3 + 20;
					int num5 = size.Height + 40;
					base.Height = num5;
					base.ClientSize = new Size(num6, num5);
				}
				this.button_left = base.ClientSize.Width / 2 - num3 / 2 + 5;
				this.AddButtons();
				this.size_known = true;
				MessageBoxDefaultButton messageBoxDefaultButton = this.msgbox_default;
				if (messageBoxDefaultButton != MessageBoxDefaultButton.Button2)
				{
					if (messageBoxDefaultButton == MessageBoxDefaultButton.Button3)
					{
						if (this.buttons[2] != null)
						{
							this.ActiveControl = this.buttons[2];
						}
					}
				}
				else if (this.buttons[1] != null)
				{
					this.ActiveControl = this.buttons[1];
				}
			}

			// Token: 0x0600277B RID: 10107 RVA: 0x00096854 File Offset: 0x00094A54
			protected override bool ProcessDialogKey(Keys keyData)
			{
				if (keyData == Keys.Escape)
				{
					this.CancelClick(this, null);
					return true;
				}
				if ((keyData & Keys.Modifiers) == Keys.Control && ((keyData & Keys.KeyCode) == Keys.C || (keyData & Keys.KeyCode) == Keys.Insert))
				{
					this.Copy();
				}
				return base.ProcessDialogKey(keyData);
			}

			// Token: 0x0600277C RID: 10108 RVA: 0x000968B0 File Offset: 0x00094AB0
			protected override bool ProcessDialogChar(char charCode)
			{
				if ((charCode == 'N' || charCode == 'n') && base.CancelButton != null && (base.CancelButton as Button).Text == "No")
				{
					base.CancelButton.PerformClick();
				}
				else if ((charCode == 'Y' || charCode == 'y') && (base.AcceptButton as Button).Text == "Yes")
				{
					base.AcceptButton.PerformClick();
				}
				else if ((charCode == 'A' || charCode == 'a') && base.CancelButton != null && (base.CancelButton as Button).Text == "Abort")
				{
					base.CancelButton.PerformClick();
				}
				else if ((charCode == 'R' || charCode == 'r') && (base.AcceptButton as Button).Text == "Retry")
				{
					base.AcceptButton.PerformClick();
				}
				else if ((charCode == 'I' || charCode == 'i') && this.buttons.Length >= 3 && this.buttons[2].Text == "Ignore")
				{
					this.buttons[2].PerformClick();
				}
				return base.ProcessDialogChar(charCode);
			}

			// Token: 0x0600277D RID: 10109 RVA: 0x00096A20 File Offset: 0x00094C20
			private void Copy()
			{
				string text = "---------------------------" + Environment.NewLine;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(text);
				stringBuilder.Append(this.Text).Append(Environment.NewLine);
				stringBuilder.Append(text);
				stringBuilder.Append(this.msgbox_text).Append(Environment.NewLine);
				stringBuilder.Append(text);
				foreach (Button button in this.buttons)
				{
					if (button == null)
					{
						break;
					}
					stringBuilder.Append(button.Text).Append("   ");
				}
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(text);
				DataObject dataObject = new DataObject(DataFormats.Text, stringBuilder.ToString());
				Clipboard.SetDataObject(dataObject);
			}

			// Token: 0x0600277E RID: 10110 RVA: 0x00096AFC File Offset: 0x00094CFC
			private void AddButtons()
			{
				if (!this.buttons_placed)
				{
					switch (this.msgbox_buttons)
					{
					case MessageBoxButtons.OK:
						this.buttons[0] = this.AddOkButton(0);
						break;
					case MessageBoxButtons.OKCancel:
						this.buttons[0] = this.AddOkButton(0);
						this.buttons[1] = this.AddCancelButton(1);
						break;
					case MessageBoxButtons.AbortRetryIgnore:
						this.buttons[0] = this.AddAbortButton(0);
						this.buttons[1] = this.AddRetryButton(1);
						this.buttons[2] = this.AddIgnoreButton(2);
						break;
					case MessageBoxButtons.YesNoCancel:
						this.buttons[0] = this.AddYesButton(0);
						this.buttons[1] = this.AddNoButton(1);
						this.buttons[2] = this.AddCancelButton(2);
						break;
					case MessageBoxButtons.YesNo:
						this.buttons[0] = this.AddYesButton(0);
						this.buttons[1] = this.AddNoButton(1);
						break;
					case MessageBoxButtons.RetryCancel:
						this.buttons[0] = this.AddRetryButton(0);
						this.buttons[1] = this.AddCancelButton(1);
						break;
					}
					if (this.show_help)
					{
						for (int i = 0; i <= 3; i++)
						{
							if (this.buttons[i] == null)
							{
								this.AddHelpButton(i);
								break;
							}
						}
					}
					this.buttons_placed = true;
				}
			}

			// Token: 0x0600277F RID: 10111 RVA: 0x00096C60 File Offset: 0x00094E60
			private Button AddButton(string text, int left, EventHandler click_event)
			{
				Button button = new Button();
				button.Text = Locale.GetText(text);
				button.Width = 86;
				button.Height = 23;
				button.Top = base.ClientSize.Height - button.Height - 10;
				button.Left = 91 * left + this.button_left;
				if (click_event != null)
				{
					button.Click += click_event;
				}
				if (text == "OK" || text == "Retry" || text == "Yes")
				{
					base.AcceptButton = button;
				}
				else if (text == "Cancel" || text == "Abort" || text == "No")
				{
					base.CancelButton = button;
				}
				base.Controls.Add(button);
				return button;
			}

			// Token: 0x06002780 RID: 10112 RVA: 0x00096D4C File Offset: 0x00094F4C
			private Button AddOkButton(int left)
			{
				return this.AddButton("OK", left, new EventHandler(this.OkClick));
			}

			// Token: 0x06002781 RID: 10113 RVA: 0x00096D68 File Offset: 0x00094F68
			private Button AddCancelButton(int left)
			{
				return this.AddButton("Cancel", left, new EventHandler(this.CancelClick));
			}

			// Token: 0x06002782 RID: 10114 RVA: 0x00096D84 File Offset: 0x00094F84
			private Button AddAbortButton(int left)
			{
				return this.AddButton("Abort", left, new EventHandler(this.AbortClick));
			}

			// Token: 0x06002783 RID: 10115 RVA: 0x00096DA0 File Offset: 0x00094FA0
			private Button AddRetryButton(int left)
			{
				return this.AddButton("Retry", left, new EventHandler(this.RetryClick));
			}

			// Token: 0x06002784 RID: 10116 RVA: 0x00096DBC File Offset: 0x00094FBC
			private Button AddIgnoreButton(int left)
			{
				return this.AddButton("Ignore", left, new EventHandler(this.IgnoreClick));
			}

			// Token: 0x06002785 RID: 10117 RVA: 0x00096DD8 File Offset: 0x00094FD8
			private Button AddYesButton(int left)
			{
				return this.AddButton("Yes", left, new EventHandler(this.YesClick));
			}

			// Token: 0x06002786 RID: 10118 RVA: 0x00096DF4 File Offset: 0x00094FF4
			private Button AddNoButton(int left)
			{
				return this.AddButton("No", left, new EventHandler(this.NoClick));
			}

			// Token: 0x06002787 RID: 10119 RVA: 0x00096E10 File Offset: 0x00095010
			private Button AddHelpButton(int left)
			{
				Button button = this.AddButton("Help", left, null);
				button.Click += delegate
				{
					base.Owner.RaiseHelpRequested(new HelpEventArgs(base.Owner.Location));
				};
				return button;
			}

			// Token: 0x06002788 RID: 10120 RVA: 0x00096E40 File Offset: 0x00095040
			private void OkClick(object sender, EventArgs e)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}

			// Token: 0x06002789 RID: 10121 RVA: 0x00096E50 File Offset: 0x00095050
			private void CancelClick(object sender, EventArgs e)
			{
				base.DialogResult = DialogResult.Cancel;
				base.Close();
			}

			// Token: 0x0600278A RID: 10122 RVA: 0x00096E60 File Offset: 0x00095060
			private void AbortClick(object sender, EventArgs e)
			{
				base.DialogResult = DialogResult.Abort;
				base.Close();
			}

			// Token: 0x0600278B RID: 10123 RVA: 0x00096E70 File Offset: 0x00095070
			private void RetryClick(object sender, EventArgs e)
			{
				base.DialogResult = DialogResult.Retry;
				base.Close();
			}

			// Token: 0x0600278C RID: 10124 RVA: 0x00096E80 File Offset: 0x00095080
			private void IgnoreClick(object sender, EventArgs e)
			{
				base.DialogResult = DialogResult.Ignore;
				base.Close();
			}

			// Token: 0x0600278D RID: 10125 RVA: 0x00096E90 File Offset: 0x00095090
			private void YesClick(object sender, EventArgs e)
			{
				base.DialogResult = DialogResult.Yes;
				base.Close();
			}

			// Token: 0x0600278E RID: 10126 RVA: 0x00096EA0 File Offset: 0x000950A0
			private void NoClick(object sender, EventArgs e)
			{
				base.DialogResult = DialogResult.No;
				base.Close();
			}

			// Token: 0x170009BC RID: 2492
			// (get) Token: 0x0600278F RID: 10127 RVA: 0x00096EB0 File Offset: 0x000950B0
			internal string UIAMessage
			{
				get
				{
					return this.msgbox_text;
				}
			}

			// Token: 0x170009BD RID: 2493
			// (get) Token: 0x06002790 RID: 10128 RVA: 0x00096EB8 File Offset: 0x000950B8
			internal Rectangle UIAMessageRectangle
			{
				get
				{
					return new Rectangle((int)this.text_rect.X, (int)this.text_rect.Y, (int)this.text_rect.Width, (int)this.text_rect.Height);
				}
			}

			// Token: 0x170009BE RID: 2494
			// (get) Token: 0x06002791 RID: 10129 RVA: 0x00096EFC File Offset: 0x000950FC
			internal Rectangle UIAIconRectangle
			{
				get
				{
					return new Rectangle(10, 10, (this.icon_image != null) ? this.icon_image.Width : (-1), (this.icon_image != null) ? this.icon_image.Height : (-1));
				}
			}

			// Token: 0x04001393 RID: 5011
			private const int space_border = 10;

			// Token: 0x04001394 RID: 5012
			private const int button_width = 86;

			// Token: 0x04001395 RID: 5013
			private const int button_height = 23;

			// Token: 0x04001396 RID: 5014
			private const int button_space = 5;

			// Token: 0x04001397 RID: 5015
			private const int space_image_text = 10;

			// Token: 0x04001398 RID: 5016
			private string msgbox_text;

			// Token: 0x04001399 RID: 5017
			private bool size_known;

			// Token: 0x0400139A RID: 5018
			private Icon icon_image;

			// Token: 0x0400139B RID: 5019
			private RectangleF text_rect;

			// Token: 0x0400139C RID: 5020
			private MessageBoxButtons msgbox_buttons;

			// Token: 0x0400139D RID: 5021
			private MessageBoxDefaultButton msgbox_default;

			// Token: 0x0400139E RID: 5022
			private bool buttons_placed;

			// Token: 0x0400139F RID: 5023
			private int button_left;

			// Token: 0x040013A0 RID: 5024
			private Button[] buttons = new Button[4];

			// Token: 0x040013A1 RID: 5025
			private bool show_help;

			// Token: 0x040013A2 RID: 5026
			private string help_file_path;

			// Token: 0x040013A3 RID: 5027
			private string help_keyword;

			// Token: 0x040013A4 RID: 5028
			private HelpNavigator help_navigator;

			// Token: 0x040013A5 RID: 5029
			private object help_param;

			// Token: 0x040013A6 RID: 5030
			private AlertType alert_type;
		}
	}
}
