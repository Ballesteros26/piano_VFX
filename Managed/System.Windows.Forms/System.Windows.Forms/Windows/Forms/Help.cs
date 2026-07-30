using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Encapsulates the HTML Help 1.0 engine.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001AF RID: 431
	public class Help
	{
		// Token: 0x06001C07 RID: 7175 RVA: 0x0006C1A4 File Offset: 0x0006A3A4
		private Help()
		{
		}

		/// <summary>Displays the contents of the Help file at the specified URL.</summary>
		/// <param name="parent">A <see cref="T:System.Windows.Forms.Control" /> that identifies the parent of the Help dialog box. </param>
		/// <param name="url">The path and name of the Help file. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001C08 RID: 7176 RVA: 0x0006C1AC File Offset: 0x0006A3AC
		public static void ShowHelp(Control parent, string url)
		{
			Help.ShowHelp(parent, url, HelpNavigator.TableOfContents, null);
		}

		/// <summary>Displays the contents of the Help file found at the specified URL for a specific topic.</summary>
		/// <param name="parent">A <see cref="T:System.Windows.Forms.Control" /> that identifies the parent of the Help dialog box. </param>
		/// <param name="url">The path and name of the Help file. </param>
		/// <param name="navigator">One of the <see cref="T:System.Windows.Forms.HelpNavigator" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001C09 RID: 7177 RVA: 0x0006C1BC File Offset: 0x0006A3BC
		public static void ShowHelp(Control parent, string url, HelpNavigator navigator)
		{
			Help.ShowHelp(parent, url, navigator, null);
		}

		/// <summary>Displays the contents of the Help file located at the URL supplied by the user.</summary>
		/// <param name="parent">A <see cref="T:System.Windows.Forms.Control" /> that identifies the parent of the Help dialog box. </param>
		/// <param name="url">The path and name of the Help file. </param>
		/// <param name="command">One of the <see cref="T:System.Windows.Forms.HelpNavigator" /> values. </param>
		/// <param name="parameter">A string containing the topic identifier.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="parameter" /> is an integer.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001C0A RID: 7178 RVA: 0x0006C1C8 File Offset: 0x0006A3C8
		[MonoTODO("Stub, does nothing")]
		public static void ShowHelp(Control parent, string url, HelpNavigator command, object parameter)
		{
		}

		/// <summary>Displays the contents of the Help file found at the specified URL for a specific keyword.</summary>
		/// <param name="parent">A <see cref="T:System.Windows.Forms.Control" /> that identifies the parent of the Help dialog box. </param>
		/// <param name="url">The path and name of the Help file. </param>
		/// <param name="keyword">The keyword to display Help for. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001C0B RID: 7179 RVA: 0x0006C1D8 File Offset: 0x0006A3D8
		public static void ShowHelp(Control parent, string url, string keyword)
		{
			if (keyword == null || keyword == string.Empty)
			{
				Help.ShowHelp(parent, url, HelpNavigator.TableOfContents, null);
			}
			Help.ShowHelp(parent, url, HelpNavigator.Topic, keyword);
		}

		/// <summary>Displays the index of the specified Help file.</summary>
		/// <param name="parent">A <see cref="T:System.Windows.Forms.Control" /> that identifies the parent of the Help dialog box. </param>
		/// <param name="url">The path and name of the Help file. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001C0C RID: 7180 RVA: 0x0006C218 File Offset: 0x0006A418
		public static void ShowHelpIndex(Control parent, string url)
		{
			Help.ShowHelp(parent, url, HelpNavigator.Index, null);
		}

		/// <summary>Displays a Help pop-up window.</summary>
		/// <param name="parent">A <see cref="T:System.Windows.Forms.Control" /> that identifies the parent of the Help dialog box. </param>
		/// <param name="caption">The message to display in the pop-up window. </param>
		/// <param name="location">A value specifying the horizontal and vertical coordinates at which to display the pop-up window. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001C0D RID: 7181 RVA: 0x0006C228 File Offset: 0x0006A428
		[MonoTODO("Stub, does nothing")]
		public static void ShowPopup(Control parent, string caption, Point location)
		{
		}
	}
}
