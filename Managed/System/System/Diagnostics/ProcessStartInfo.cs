using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.Diagnostics
{
	/// <summary>Specifies a set of values that are used when you start a process.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001DC RID: 476
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true, SelfAffectingProcessMgmt = true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ProcessStartInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessStartInfo" /> class without specifying a file name with which to start the process.</summary>
		// Token: 0x06000EF9 RID: 3833 RVA: 0x000464F0 File Offset: 0x000446F0
		public ProcessStartInfo()
		{
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x000464FF File Offset: 0x000446FF
		internal ProcessStartInfo(Process parent)
		{
			this.weakParentProcess = new WeakReference(parent);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessStartInfo" /> class and specifies a file name such as an application or document with which to start the process.</summary>
		/// <param name="fileName">An application or document with which to start a process. </param>
		// Token: 0x06000EFB RID: 3835 RVA: 0x0004651A File Offset: 0x0004471A
		public ProcessStartInfo(string fileName)
		{
			this.fileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessStartInfo" /> class, specifies an application file name with which to start the process, and specifies a set of command-line arguments to pass to the application.</summary>
		/// <param name="fileName">An application with which to start a process. </param>
		/// <param name="arguments">Command-line arguments to pass to the application when the process starts. </param>
		// Token: 0x06000EFC RID: 3836 RVA: 0x00046530 File Offset: 0x00044730
		public ProcessStartInfo(string fileName, string arguments)
		{
			this.fileName = fileName;
			this.arguments = arguments;
		}

		/// <summary>Gets or sets the verb to use when opening the application or document specified by the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.</summary>
		/// <returns>The action to take with the file that the process opens. The default is an empty string (""), which signifies no action.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0004654D File Offset: 0x0004474D
		// (set) Token: 0x06000EFE RID: 3838 RVA: 0x00046563 File Offset: 0x00044763
		[DefaultValue("")]
		[MonitoringDescription("The verb to apply to the document specified by the FileName property.")]
		[TypeConverter("System.Diagnostics.Design.VerbConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[NotifyParentProperty(true)]
		public string Verb
		{
			get
			{
				if (this.verb == null)
				{
					return string.Empty;
				}
				return this.verb;
			}
			set
			{
				this.verb = value;
			}
		}

		/// <summary>Gets or sets the set of command-line arguments to use when starting the application.</summary>
		/// <returns>File type–specific arguments that the system can associate with the application specified in the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property. The default is an empty string (""). On Windows Vista and earlier versions of the Windows operating system, the length of the arguments added to the length of the full path to the process must be less than 2080. On Windows 7 and later versions, the length must be less than 32699.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0004656C File Offset: 0x0004476C
		// (set) Token: 0x06000F00 RID: 3840 RVA: 0x00046582 File Offset: 0x00044782
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SettingsBindable(true)]
		[MonitoringDescription("Command line arguments that will be passed to the application specified by the FileName property.")]
		public string Arguments
		{
			get
			{
				if (this.arguments == null)
				{
					return string.Empty;
				}
				return this.arguments;
			}
			set
			{
				this.arguments = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to start the process in a new window.</summary>
		/// <returns>true if the process should be started without creating a new window to contain it; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0004658B File Offset: 0x0004478B
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x00046593 File Offset: 0x00044793
		[NotifyParentProperty(true)]
		[MonitoringDescription("Whether to start the process without creating a new window to contain it.")]
		[DefaultValue(false)]
		public bool CreateNoWindow
		{
			get
			{
				return this.createNoWindow;
			}
			set
			{
				this.createNoWindow = value;
			}
		}

		/// <summary>Gets search paths for files, directories for temporary files, application-specific options, and other similar information.</summary>
		/// <returns>A string dictionary that provides environment variables that apply to this process and child processes. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x0004659C File Offset: 0x0004479C
		[MonitoringDescription("Set of environment variables that apply to this process and child processes.")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Diagnostics.Design.StringDictionaryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[NotifyParentProperty(true)]
		public StringDictionary EnvironmentVariables
		{
			get
			{
				if (this.environmentVariables == null)
				{
					this.environmentVariables = new CaseSensitiveStringDictionary();
					if (this.weakParentProcess == null || !this.weakParentProcess.IsAlive || ((Component)this.weakParentProcess.Target).Site == null || !((Component)this.weakParentProcess.Target).Site.DesignMode)
					{
						foreach (object obj in global::System.Environment.GetEnvironmentVariables())
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
							this.environmentVariables.Add((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
						}
					}
				}
				return this.environmentVariables;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x00046674 File Offset: 0x00044874
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IDictionary<string, string> Environment
		{
			get
			{
				if (this.environment == null)
				{
					this.environment = this.EnvironmentVariables.AsGenericDictionary();
				}
				return this.environment;
			}
		}

		/// <summary>Gets or sets a value indicating whether the input for an application is read from the <see cref="P:System.Diagnostics.Process.StandardInput" /> stream.</summary>
		/// <returns>true if input should be read from <see cref="P:System.Diagnostics.Process.StandardInput" />; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x00046695 File Offset: 0x00044895
		// (set) Token: 0x06000F06 RID: 3846 RVA: 0x0004669D File Offset: 0x0004489D
		[DefaultValue(false)]
		[MonitoringDescription("Whether the process command input is read from the Process instance's StandardInput member.")]
		[NotifyParentProperty(true)]
		public bool RedirectStandardInput
		{
			get
			{
				return this.redirectStandardInput;
			}
			set
			{
				this.redirectStandardInput = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the output of an application is written to the <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream.</summary>
		/// <returns>true if output should be written to <see cref="P:System.Diagnostics.Process.StandardOutput" />; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x000466A6 File Offset: 0x000448A6
		// (set) Token: 0x06000F08 RID: 3848 RVA: 0x000466AE File Offset: 0x000448AE
		[NotifyParentProperty(true)]
		[MonitoringDescription("Whether the process output is written to the Process instance's StandardOutput member.")]
		[DefaultValue(false)]
		public bool RedirectStandardOutput
		{
			get
			{
				return this.redirectStandardOutput;
			}
			set
			{
				this.redirectStandardOutput = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the error output of an application is written to the <see cref="P:System.Diagnostics.Process.StandardError" /> stream.</summary>
		/// <returns>true if error output should be written to <see cref="P:System.Diagnostics.Process.StandardError" />; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x000466B7 File Offset: 0x000448B7
		// (set) Token: 0x06000F0A RID: 3850 RVA: 0x000466BF File Offset: 0x000448BF
		[NotifyParentProperty(true)]
		[MonitoringDescription("Whether the process's error output is written to the Process instance's StandardError member.")]
		[DefaultValue(false)]
		public bool RedirectStandardError
		{
			get
			{
				return this.redirectStandardError;
			}
			set
			{
				this.redirectStandardError = value;
			}
		}

		/// <summary>Gets or sets the preferred encoding for error output.</summary>
		/// <returns>An object that represents the preferred encoding for error output. The default is null.</returns>
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x000466C8 File Offset: 0x000448C8
		// (set) Token: 0x06000F0C RID: 3852 RVA: 0x000466D0 File Offset: 0x000448D0
		public Encoding StandardErrorEncoding
		{
			get
			{
				return this.standardErrorEncoding;
			}
			set
			{
				this.standardErrorEncoding = value;
			}
		}

		/// <summary>Gets or sets the preferred encoding for standard output.</summary>
		/// <returns>An object that represents the preferred encoding for standard output. The default is null.</returns>
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x000466D9 File Offset: 0x000448D9
		// (set) Token: 0x06000F0E RID: 3854 RVA: 0x000466E1 File Offset: 0x000448E1
		public Encoding StandardOutputEncoding
		{
			get
			{
				return this.standardOutputEncoding;
			}
			set
			{
				this.standardOutputEncoding = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to use the operating system shell to start the process.</summary>
		/// <returns>true if the shell should be used when starting the process; false if the process should be created directly from the executable file. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x000466EA File Offset: 0x000448EA
		// (set) Token: 0x06000F10 RID: 3856 RVA: 0x000466F2 File Offset: 0x000448F2
		[MonitoringDescription("Whether to use the operating system shell to start the process.")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool UseShellExecute
		{
			get
			{
				return this.useShellExecute;
			}
			set
			{
				this.useShellExecute = value;
			}
		}

		/// <summary>Gets or sets the user name to be used when starting the process.</summary>
		/// <returns>The user name to use when starting the process.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x000466FB File Offset: 0x000448FB
		// (set) Token: 0x06000F12 RID: 3858 RVA: 0x00046711 File Offset: 0x00044911
		[NotifyParentProperty(true)]
		public string UserName
		{
			get
			{
				if (this.userName == null)
				{
					return string.Empty;
				}
				return this.userName;
			}
			set
			{
				this.userName = value;
			}
		}

		/// <summary>Gets or sets a secure string that contains the user password to use when starting the process.</summary>
		/// <returns>The user password to use when starting the process.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x0004671A File Offset: 0x0004491A
		// (set) Token: 0x06000F14 RID: 3860 RVA: 0x00046722 File Offset: 0x00044922
		public SecureString Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x0004672B File Offset: 0x0004492B
		// (set) Token: 0x06000F16 RID: 3862 RVA: 0x00046733 File Offset: 0x00044933
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string PasswordInClearText
		{
			get
			{
				return this.passwordInClearText;
			}
			set
			{
				this.passwordInClearText = value;
			}
		}

		/// <summary>Gets or sets a value that identifies the domain to use when starting the process. </summary>
		/// <returns>The Active Directory domain to use when starting the process. The domain property is primarily of interest to users within enterprise environments that use Active Directory.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x0004673C File Offset: 0x0004493C
		// (set) Token: 0x06000F18 RID: 3864 RVA: 0x00046752 File Offset: 0x00044952
		[NotifyParentProperty(true)]
		public string Domain
		{
			get
			{
				if (this.domain == null)
				{
					return string.Empty;
				}
				return this.domain;
			}
			set
			{
				this.domain = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the Windows user profile is to be loaded from the registry. </summary>
		/// <returns>true if the Windows user profile should be loaded; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x0004675B File Offset: 0x0004495B
		// (set) Token: 0x06000F1A RID: 3866 RVA: 0x00046763 File Offset: 0x00044963
		[NotifyParentProperty(true)]
		public bool LoadUserProfile
		{
			get
			{
				return this.loadUserProfile;
			}
			set
			{
				this.loadUserProfile = value;
			}
		}

		/// <summary>Gets or sets the application or document to start.</summary>
		/// <returns>The name of the application to start, or the name of a document of a file type that is associated with an application and that has a default open action available to it. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x0004676C File Offset: 0x0004496C
		// (set) Token: 0x06000F1C RID: 3868 RVA: 0x00046782 File Offset: 0x00044982
		[Editor("System.Diagnostics.Design.StartFileNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("The name of the application, document or URL to start.")]
		[SettingsBindable(true)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string FileName
		{
			get
			{
				if (this.fileName == null)
				{
					return string.Empty;
				}
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		/// <summary>When the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property is false, gets or sets the working directory for the process to be started. When <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> is true, gets or sets the directory that contains the process to be started.</summary>
		/// <returns>When <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> is true, the fully qualified name of the directory that contains the process to be started. When the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property is false, the working directory for the process to be started. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0004678B File Offset: 0x0004498B
		// (set) Token: 0x06000F1E RID: 3870 RVA: 0x000467A1 File Offset: 0x000449A1
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SettingsBindable(true)]
		[Editor("System.Diagnostics.Design.WorkingDirectoryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("The initial working directory for the process.")]
		public string WorkingDirectory
		{
			get
			{
				if (this.directory == null)
				{
					return string.Empty;
				}
				return this.directory;
			}
			set
			{
				this.directory = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether an error dialog box is displayed to the user if the process cannot be started.</summary>
		/// <returns>true if an error dialog box should be displayed on the screen if the process cannot be started; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x000467AA File Offset: 0x000449AA
		// (set) Token: 0x06000F20 RID: 3872 RVA: 0x000467B2 File Offset: 0x000449B2
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[MonitoringDescription("Whether to show an error dialog to the user if there is an error.")]
		public bool ErrorDialog
		{
			get
			{
				return this.errorDialog;
			}
			set
			{
				this.errorDialog = value;
			}
		}

		/// <summary>Gets or sets the window handle to use when an error dialog box is shown for a process that cannot be started.</summary>
		/// <returns>A pointer to the handle of the error dialog box that results from a process start failure.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x000467BB File Offset: 0x000449BB
		// (set) Token: 0x06000F22 RID: 3874 RVA: 0x000467C3 File Offset: 0x000449C3
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IntPtr ErrorDialogParentHandle
		{
			get
			{
				return this.errorDialogParentHandle;
			}
			set
			{
				this.errorDialogParentHandle = value;
			}
		}

		/// <summary>Gets or sets the window state to use when the process is started.</summary>
		/// <returns>One of the enumeration values that indicates whether the process is started in a window that is maximized, minimized, normal (neither maximized nor minimized), or not visible. The default is Normal.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The window style is not one of the <see cref="T:System.Diagnostics.ProcessWindowStyle" /> enumeration members. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x000467CC File Offset: 0x000449CC
		// (set) Token: 0x06000F24 RID: 3876 RVA: 0x000467D4 File Offset: 0x000449D4
		[DefaultValue(ProcessWindowStyle.Normal)]
		[MonitoringDescription("How the main window should be created when the process starts.")]
		[NotifyParentProperty(true)]
		public ProcessWindowStyle WindowStyle
		{
			get
			{
				return this.windowStyle;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ProcessWindowStyle), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ProcessWindowStyle));
				}
				this.windowStyle = value;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x0004680A File Offset: 0x00044A0A
		internal bool HaveEnvVars
		{
			get
			{
				return this.environmentVariables != null;
			}
		}

		/// <summary>Gets the set of verbs associated with the type of file specified by the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.</summary>
		/// <returns>The actions that the system can apply to the file indicated by the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00046818 File Offset: 0x00044A18
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string[] Verbs
		{
			get
			{
				PlatformID platform = global::System.Environment.OSVersion.Platform;
				if (platform == PlatformID.Unix || platform == PlatformID.MacOSX || platform == (PlatformID)128)
				{
					return ProcessStartInfo.empty;
				}
				string text = (string.IsNullOrEmpty(this.fileName) ? null : Path.GetExtension(this.fileName));
				if (text == null)
				{
					return ProcessStartInfo.empty;
				}
				RegistryKey registryKey = null;
				RegistryKey registryKey2 = null;
				RegistryKey registryKey3 = null;
				string[] array;
				try
				{
					registryKey = Registry.ClassesRoot.OpenSubKey(text);
					string text2 = ((registryKey != null) ? (registryKey.GetValue(null) as string) : null);
					registryKey2 = ((text2 != null) ? Registry.ClassesRoot.OpenSubKey(text2) : null);
					registryKey3 = ((registryKey2 != null) ? registryKey2.OpenSubKey("shell") : null);
					array = ((registryKey3 != null) ? registryKey3.GetSubKeyNames() : null);
				}
				finally
				{
					if (registryKey3 != null)
					{
						registryKey3.Close();
					}
					if (registryKey2 != null)
					{
						registryKey2.Close();
					}
					if (registryKey != null)
					{
						registryKey.Close();
					}
				}
				return array;
			}
		}

		// Token: 0x040010EE RID: 4334
		private string fileName;

		// Token: 0x040010EF RID: 4335
		private string arguments;

		// Token: 0x040010F0 RID: 4336
		private string directory;

		// Token: 0x040010F1 RID: 4337
		private string verb;

		// Token: 0x040010F2 RID: 4338
		private ProcessWindowStyle windowStyle;

		// Token: 0x040010F3 RID: 4339
		private bool errorDialog;

		// Token: 0x040010F4 RID: 4340
		private IntPtr errorDialogParentHandle;

		// Token: 0x040010F5 RID: 4341
		private bool useShellExecute = true;

		// Token: 0x040010F6 RID: 4342
		private string userName;

		// Token: 0x040010F7 RID: 4343
		private string domain;

		// Token: 0x040010F8 RID: 4344
		private SecureString password;

		// Token: 0x040010F9 RID: 4345
		private string passwordInClearText;

		// Token: 0x040010FA RID: 4346
		private bool loadUserProfile;

		// Token: 0x040010FB RID: 4347
		private bool redirectStandardInput;

		// Token: 0x040010FC RID: 4348
		private bool redirectStandardOutput;

		// Token: 0x040010FD RID: 4349
		private bool redirectStandardError;

		// Token: 0x040010FE RID: 4350
		private Encoding standardOutputEncoding;

		// Token: 0x040010FF RID: 4351
		private Encoding standardErrorEncoding;

		// Token: 0x04001100 RID: 4352
		private bool createNoWindow;

		// Token: 0x04001101 RID: 4353
		private WeakReference weakParentProcess;

		// Token: 0x04001102 RID: 4354
		internal StringDictionary environmentVariables;

		// Token: 0x04001103 RID: 4355
		private IDictionary<string, string> environment;

		// Token: 0x04001104 RID: 4356
		private static readonly string[] empty = new string[0];
	}
}
