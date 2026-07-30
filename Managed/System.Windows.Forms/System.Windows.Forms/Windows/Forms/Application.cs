using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	/// <summary>Provides static methods and properties to manage an application, such as methods to start and stop an application, to process Windows messages, and properties to get information about an application. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200003F RID: 63
	public sealed class Application
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x0000F7D4 File Offset: 0x0000D9D4
		private Application()
		{
			Application.browser_embedded = false;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000F7E4 File Offset: 0x0000D9E4
		static Application()
		{
			Application.InitializeUIAutomation();
		}

		/// <summary>Occurs when the application is about to shut down.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060001E5 RID: 485 RVA: 0x0000F820 File Offset: 0x0000DA20
		// (remove) Token: 0x060001E6 RID: 486 RVA: 0x0000F838 File Offset: 0x0000DA38
		public static event EventHandler ApplicationExit;

		/// <summary>Occurs when the application finishes processing and is about to enter the idle state.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060001E7 RID: 487 RVA: 0x0000F850 File Offset: 0x0000DA50
		// (remove) Token: 0x060001E8 RID: 488 RVA: 0x0000F858 File Offset: 0x0000DA58
		public static event EventHandler Idle
		{
			add
			{
				XplatUI.Idle += value;
			}
			remove
			{
				XplatUI.Idle -= value;
			}
		}

		/// <summary>Occurs when a thread is about to shut down. When the main thread for an application is about to be shut down, this event is raised first, followed by an <see cref="E:System.Windows.Forms.Application.ApplicationExit" /> event.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060001E9 RID: 489 RVA: 0x0000F860 File Offset: 0x0000DA60
		// (remove) Token: 0x060001EA RID: 490 RVA: 0x0000F878 File Offset: 0x0000DA78
		public static event EventHandler ThreadExit;

		/// <summary>Occurs when an untrapped thread exception is thrown.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060001EB RID: 491 RVA: 0x0000F890 File Offset: 0x0000DA90
		// (remove) Token: 0x060001EC RID: 492 RVA: 0x0000F8A8 File Offset: 0x0000DAA8
		public static event ThreadExceptionEventHandler ThreadException;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060001ED RID: 493 RVA: 0x0000F8C0 File Offset: 0x0000DAC0
		// (remove) Token: 0x060001EE RID: 494 RVA: 0x0000F8D8 File Offset: 0x0000DAD8
		internal static event EventHandler FormAdded;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060001EF RID: 495 RVA: 0x0000F8F0 File Offset: 0x0000DAF0
		// (remove) Token: 0x060001F0 RID: 496 RVA: 0x0000F908 File Offset: 0x0000DB08
		internal static event EventHandler PreRun;

		/// <summary>Occurs when the application is about to enter a modal state. </summary>
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060001F1 RID: 497 RVA: 0x0000F920 File Offset: 0x0000DB20
		// (remove) Token: 0x060001F2 RID: 498 RVA: 0x0000F938 File Offset: 0x0000DB38
		[EditorBrowsable(2)]
		public static event EventHandler EnterThreadModal;

		/// <summary>Occurs when the application is about to leave a modal state. </summary>
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060001F3 RID: 499 RVA: 0x0000F950 File Offset: 0x0000DB50
		// (remove) Token: 0x060001F4 RID: 500 RVA: 0x0000F968 File Offset: 0x0000DB68
		[EditorBrowsable(2)]
		public static event EventHandler LeaveThreadModal;

		// Token: 0x060001F5 RID: 501 RVA: 0x0000F980 File Offset: 0x0000DB80
		private static void InitializeUIAutomation()
		{
			Assembly assembly = null;
			try
			{
				assembly = Assembly.Load("UIAutomationWinforms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=f4ceacb585d99812");
			}
			catch
			{
			}
			if (assembly == null)
			{
				return;
			}
			try
			{
				Type type = assembly.GetType("Mono.UIAutomation.Winforms.Global", false);
				if (type == null)
				{
					throw new Exception(string.Format("Type {0} not found in assembly {1}.", "Mono.UIAutomation.Winforms.Global", "UIAutomationWinforms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=f4ceacb585d99812"));
				}
				MethodInfo method = type.GetMethod("Initialize", 24);
				if (method == null)
				{
					throw new Exception(string.Format("Method {0} not found in type {1}.", "Initialize", "Mono.UIAutomation.Winforms.Global"));
				}
				method.Invoke(null, new object[0]);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("Error setting up UIA: " + ex);
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000FA78 File Offset: 0x0000DC78
		internal static void CloseForms(Thread thread)
		{
			ArrayList arrayList = new ArrayList();
			FormCollection formCollection = Application.forms;
			lock (formCollection)
			{
				foreach (object obj in Application.forms)
				{
					Form form = (Form)obj;
					if (thread == null || thread == form.creator_thread)
					{
						arrayList.Add(form);
					}
				}
				foreach (object obj2 in arrayList)
				{
					Form form2 = (Form)obj2;
					form2.Dispose();
				}
			}
		}

		/// <summary>Gets a value indicating whether the caller can quit this application.</summary>
		/// <returns>true if the caller can quit this application; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000FB94 File Offset: 0x0000DD94
		public static bool AllowQuit
		{
			get
			{
				return !Application.browser_embedded;
			}
		}

		/// <summary>Gets the path for the application data that is shared among all users.</summary>
		/// <returns>The path for the application data that is shared among all users.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000FBA0 File Offset: 0x0000DDA0
		public static string CommonAppDataPath
		{
			get
			{
				return Application.CreateDataPath(Environment.GetFolderPath(35));
			}
		}

		/// <summary>Gets the registry key for the application data that is shared among all users.</summary>
		/// <returns>A <see cref="T:Microsoft.Win32.RegistryKey" /> representing the registry key of the application data that is shared among all users.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000FBB0 File Offset: 0x0000DDB0
		public static RegistryKey CommonAppDataRegistry
		{
			get
			{
				string text = string.Format("Software\\{0}\\{1}\\{2}", Application.CompanyName, Application.ProductName, Application.ProductVersion);
				return Registry.LocalMachine.CreateSubKey(text);
			}
		}

		/// <summary>Gets the company name associated with the application.</summary>
		/// <returns>The company name.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000FBE4 File Offset: 0x0000DDE4
		public static string CompanyName
		{
			get
			{
				string text = string.Empty;
				Assembly assembly = Assembly.GetEntryAssembly();
				if (assembly == null)
				{
					assembly = Assembly.GetCallingAssembly();
				}
				AssemblyCompanyAttribute[] array = (AssemblyCompanyAttribute[])assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
				if (array != null && array.Length > 0)
				{
					text = array[0].Company;
				}
				if ((text == null || text.Length == 0) && assembly.EntryPoint != null)
				{
					text = assembly.EntryPoint.DeclaringType.Namespace;
					if (text != null)
					{
						int num = text.IndexOf('.');
						if (num >= 0)
						{
							text = text.Substring(0, num);
						}
					}
				}
				if ((text == null || text.Length == 0) && assembly.EntryPoint != null)
				{
					text = assembly.EntryPoint.DeclaringType.FullName;
				}
				return text;
			}
		}

		/// <summary>Gets or sets the culture information for the current thread.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> representing the culture information for the current thread.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000FCB4 File Offset: 0x0000DEB4
		// (set) Token: 0x060001FC RID: 508 RVA: 0x0000FCC0 File Offset: 0x0000DEC0
		public static CultureInfo CurrentCulture
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture;
			}
			set
			{
				Thread.CurrentThread.CurrentUICulture = value;
			}
		}

		/// <summary>Gets or sets the current input language for the current thread.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.InputLanguage" /> representing the current input language for the current thread.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000FCD0 File Offset: 0x0000DED0
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000FCD8 File Offset: 0x0000DED8
		public static InputLanguage CurrentInputLanguage
		{
			get
			{
				return Application.input_language;
			}
			set
			{
				Application.input_language = value;
			}
		}

		/// <summary>Gets the path for the executable file that started the application, including the executable name.</summary>
		/// <returns>The path and executable name for the executable file that started the application.This path will be different depending on whether the Windows Forms application is deployed using ClickOnce. ClickOnce applications are stored in a per-user application cache in the C:\Documents and Settings\username directory. For more information, see Accessing Local and Remote Data in ClickOnce Applications.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000FCE0 File Offset: 0x0000DEE0
		public static string ExecutablePath
		{
			get
			{
				return Path.GetFullPath(Environment.GetCommandLineArgs()[0]);
			}
		}

		/// <summary>Gets the path for the application data of a local, non-roaming user.</summary>
		/// <returns>The path for the application data of a local, non-roaming user.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		public static string LocalUserAppDataPath
		{
			get
			{
				return Application.CreateDataPath(Environment.GetFolderPath(28));
			}
		}

		/// <summary>Gets a value indicating whether a message loop exists on this thread.</summary>
		/// <returns>true if a message loop exists; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000FD00 File Offset: 0x0000DF00
		public static bool MessageLoop
		{
			get
			{
				return Application.MWFThread.Current.MessageLoop;
			}
		}

		/// <summary>Gets the product name associated with this application.</summary>
		/// <returns>The product name.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000FD0C File Offset: 0x0000DF0C
		public static string ProductName
		{
			get
			{
				string text = string.Empty;
				Assembly assembly = Assembly.GetEntryAssembly();
				if (assembly == null)
				{
					assembly = Assembly.GetCallingAssembly();
				}
				AssemblyProductAttribute[] array = (AssemblyProductAttribute[])assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), true);
				if (array != null && array.Length > 0)
				{
					text = array[0].Product;
				}
				if ((text == null || text.Length == 0) && assembly.EntryPoint != null)
				{
					text = assembly.EntryPoint.DeclaringType.Namespace;
					if (text != null)
					{
						int num = text.LastIndexOf('.');
						if (num >= 0 && num < text.Length - 1)
						{
							text = text.Substring(num + 1);
						}
					}
					if (text == null || text.Length == 0)
					{
						text = assembly.EntryPoint.DeclaringType.FullName;
					}
				}
				return text;
			}
		}

		/// <summary>Gets the product version associated with this application.</summary>
		/// <returns>The product version.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000FDE0 File Offset: 0x0000DFE0
		public static string ProductVersion
		{
			get
			{
				string text = string.Empty;
				Assembly assembly = Assembly.GetEntryAssembly();
				if (assembly == null)
				{
					assembly = Assembly.GetCallingAssembly();
				}
				AssemblyInformationalVersionAttribute assemblyInformationalVersionAttribute = Attribute.GetCustomAttribute(assembly, typeof(AssemblyInformationalVersionAttribute)) as AssemblyInformationalVersionAttribute;
				if (assemblyInformationalVersionAttribute != null)
				{
					text = assemblyInformationalVersionAttribute.InformationalVersion;
				}
				if (text == null || text.Length == 0)
				{
					AssemblyFileVersionAttribute assemblyFileVersionAttribute = Attribute.GetCustomAttribute(assembly, typeof(AssemblyFileVersionAttribute)) as AssemblyFileVersionAttribute;
					if (assemblyFileVersionAttribute != null)
					{
						text = assemblyFileVersionAttribute.Version;
					}
				}
				if (text == null || text.Length == 0)
				{
					text = assembly.GetName().Version.ToString();
				}
				return text;
			}
		}

		/// <summary>Gets or sets the format string to apply to top-level window captions when they are displayed with a warning banner.</summary>
		/// <returns>The format string to apply to top-level window captions.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000FE80 File Offset: 0x0000E080
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000FE88 File Offset: 0x0000E088
		public static string SafeTopLevelCaptionFormat
		{
			get
			{
				return Application.safe_caption_format;
			}
			set
			{
				Application.safe_caption_format = value;
			}
		}

		/// <summary>Gets the path for the executable file that started the application, not including the executable name.</summary>
		/// <returns>The path for the executable file that started the application.This path will be different depending on whether the Windows Forms application is deployed using ClickOnce. ClickOnce applications are stored in a per-user application cache in the C:\Documents and Settings\username directory. For more information, see Accessing Local and Remote Data in ClickOnce Applications.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000FE90 File Offset: 0x0000E090
		public static string StartupPath
		{
			get
			{
				return Path.GetDirectoryName(Application.ExecutablePath);
			}
		}

		/// <summary>Gets the path for the application data of a user.</summary>
		/// <returns>The path for the application data of a user.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000FE9C File Offset: 0x0000E09C
		public static string UserAppDataPath
		{
			get
			{
				return Application.CreateDataPath(Environment.GetFolderPath(26));
			}
		}

		/// <summary>Gets the registry key for the application data of a user.</summary>
		/// <returns>A <see cref="T:Microsoft.Win32.RegistryKey" /> representing the registry key for the application data specific to the user.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000FEAC File Offset: 0x0000E0AC
		public static RegistryKey UserAppDataRegistry
		{
			get
			{
				string text = string.Format("Software\\{0}\\{1}\\{2}", Application.CompanyName, Application.ProductName, Application.ProductVersion);
				return Registry.CurrentUser.CreateSubKey(text);
			}
		}

		/// <summary>Gets or sets whether the wait cursor is used for all open forms of the application.</summary>
		/// <returns>true is the wait cursor is used for all open forms; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000FEE0 File Offset: 0x0000E0E0
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000FEE8 File Offset: 0x0000E0E8
		public static bool UseWaitCursor
		{
			get
			{
				return Application.use_wait_cursor;
			}
			set
			{
				Application.use_wait_cursor = value;
				if (Application.use_wait_cursor)
				{
					foreach (object obj in Application.OpenForms)
					{
						Form form = (Form)obj;
						form.Cursor = Cursors.WaitCursor;
					}
				}
			}
		}

		/// <summary>Gets a value specifying whether the current application is drawing controls with visual styles.</summary>
		/// <returns>true if visual styles are enabled for controls in the client area of application windows; otherwise, false.</returns>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000FF6C File Offset: 0x0000E16C
		public static bool RenderWithVisualStyles
		{
			get
			{
				if (VisualStyleInformation.IsSupportedByOS)
				{
					if (!VisualStyleInformation.IsEnabledByUser)
					{
						return false;
					}
					if (!XplatUI.ThemesEnabled)
					{
						return false;
					}
					if (Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled)
					{
						return true;
					}
					if (Application.VisualStyleState == VisualStyleState.ClientAreaEnabled)
					{
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>Gets a value that specifies how visual styles are applied to application windows.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleState" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000FFB8 File Offset: 0x0000E1B8
		// (set) Token: 0x0600020D RID: 525 RVA: 0x0000FFC0 File Offset: 0x0000E1C0
		public static VisualStyleState VisualStyleState
		{
			get
			{
				return Application.visual_style_state;
			}
			set
			{
				Application.visual_style_state = value;
			}
		}

		/// <summary>Adds a message filter to monitor Windows messages as they are routed to their destinations.</summary>
		/// <param name="value">The implementation of the <see cref="T:System.Windows.Forms.IMessageFilter" /> interface you want to install. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600020E RID: 526 RVA: 0x0000FFC8 File Offset: 0x0000E1C8
		public static void AddMessageFilter(IMessageFilter value)
		{
			ArrayList arrayList = Application.message_filters;
			lock (arrayList)
			{
				Application.message_filters.Add(value);
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00010018 File Offset: 0x0000E218
		internal static void AddKeyFilter(IKeyFilter value)
		{
			XplatUI.AddKeyFilter(value);
		}

		/// <summary>Processes all Windows messages currently in the message queue.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000210 RID: 528 RVA: 0x00010020 File Offset: 0x0000E220
		public static void DoEvents()
		{
			XplatUI.DoEvents();
		}

		/// <summary>Enables visual styles for the application.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000211 RID: 529 RVA: 0x00010028 File Offset: 0x0000E228
		public static void EnableVisualStyles()
		{
			Application.visual_styles_enabled = true;
			XplatUI.EnableThemes();
		}

		/// <summary>Runs any filters against a window message, and returns a copy of the modified message.</summary>
		/// <returns>True if the filters were processed; otherwise, false.</returns>
		/// <param name="message">The Windows event message to filter. </param>
		// Token: 0x06000212 RID: 530 RVA: 0x00010038 File Offset: 0x0000E238
		[EditorBrowsable(2)]
		public static bool FilterMessage(ref Message message)
		{
			ArrayList arrayList = Application.message_filters;
			lock (arrayList)
			{
				for (int i = 0; i < Application.message_filters.Count; i++)
				{
					IMessageFilter messageFilter = (IMessageFilter)Application.message_filters[i];
					if (messageFilter.PreFilterMessage(ref message))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Sets the application-wide default for the UseCompatibleTextRendering property defined on certain controls.</summary>
		/// <param name="defaultValue">The default value to use for new controls. If true, new controls that support UseCompatibleTextRendering use the GDI+ based <see cref="T:System.Drawing.Graphics" /> class for text rendering; if false, new controls use the GDI based <see cref="T:System.Windows.Forms.TextRenderer" /> class.</param>
		/// <exception cref="T:System.InvalidOperationException">You can only call this method before the first window is created by your Windows Forms application. </exception>
		// Token: 0x06000213 RID: 531 RVA: 0x000100BC File Offset: 0x0000E2BC
		public static void SetCompatibleTextRenderingDefault(bool defaultValue)
		{
			Application.use_compatible_text_rendering = defaultValue;
		}

		/// <summary>Gets a collection of open forms owned by the application.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FormCollection" /> containing all the currently open forms owned by this application.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
		/// </PermissionSet>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000100C4 File Offset: 0x0000E2C4
		public static FormCollection OpenForms
		{
			get
			{
				return Application.forms;
			}
		}

		/// <summary>Registers a callback for checking whether the message loop is running in hosted environments.</summary>
		/// <param name="callback">The method to call when Windows Forms needs to check if the hosting environment is still sending messages.</param>
		// Token: 0x06000215 RID: 533 RVA: 0x000100CC File Offset: 0x0000E2CC
		[EditorBrowsable(2)]
		[MonoNotSupported("Only applies when Winforms is being hosted by an unmanaged app.")]
		public static void RegisterMessageLoop(Application.MessageLoopCallback callback)
		{
		}

		/// <summary>Suspends or hibernates the system, or requests that the system be suspended or hibernated.</summary>
		/// <returns>true if the system is being suspended, otherwise, false.</returns>
		/// <param name="state">A <see cref="T:System.Windows.Forms.PowerState" /> indicating the power activity mode to which to transition. </param>
		/// <param name="force">true to force the suspended mode immediately; false to cause Windows to send a suspend request to every application. </param>
		/// <param name="disableWakeEvent">true to disable restoring the system's power status to active on a wake event, false to enable restoring the system's power status to active on a wake event. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000216 RID: 534 RVA: 0x000100D0 File Offset: 0x0000E2D0
		[MonoNotSupported("Empty stub.")]
		public static bool SetSuspendState(PowerState state, bool force, bool disableWakeEvent)
		{
			return false;
		}

		/// <summary>Instructs the application how to respond to unhandled exceptions.</summary>
		/// <param name="mode">An <see cref="T:System.Windows.Forms.UnhandledExceptionMode" /> value describing how the application should behave if an exception is thrown without being caught.</param>
		/// <exception cref="T:System.InvalidOperationException">You cannot set the exception mode after the application has created its first window.</exception>
		// Token: 0x06000217 RID: 535 RVA: 0x000100D4 File Offset: 0x0000E2D4
		[MonoNotSupported("Empty stub.")]
		public static void SetUnhandledExceptionMode(UnhandledExceptionMode mode)
		{
		}

		/// <summary>Instructs the application how to respond to unhandled exceptions, optionally applying thread-specific behavior.</summary>
		/// <param name="mode">An <see cref="T:System.Windows.Forms.UnhandledExceptionMode" /> value describing how the application should behave if an exception is thrown without being caught.</param>
		/// <param name="threadScope">true to set the thread exception mode; otherwise, false.</param>
		/// <exception cref="T:System.InvalidOperationException">You cannot set the exception mode after the application has created its first window.</exception>
		// Token: 0x06000218 RID: 536 RVA: 0x000100D8 File Offset: 0x0000E2D8
		[MonoNotSupported("Empty stub.")]
		public static void SetUnhandledExceptionMode(UnhandledExceptionMode mode, bool threadScope)
		{
		}

		/// <summary>Unregisters the message loop callback made with <see cref="M:System.Windows.Forms.Application.RegisterMessageLoop(System.Windows.Forms.Application.MessageLoopCallback)" />.</summary>
		// Token: 0x06000219 RID: 537 RVA: 0x000100DC File Offset: 0x0000E2DC
		[EditorBrowsable(2)]
		[MonoNotSupported("Only applies when Winforms is being hosted by an unmanaged app.")]
		public static void UnregisterMessageLoop()
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Application.Idle" /> event in hosted scenarios.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> objects to pass to the <see cref="E:System.Windows.Forms.Application.Idle" /> event.</param>
		// Token: 0x0600021A RID: 538 RVA: 0x000100E0 File Offset: 0x0000E2E0
		[EditorBrowsable(2)]
		public static void RaiseIdle(EventArgs e)
		{
			XplatUI.RaiseIdle(e);
		}

		/// <summary>Shuts down the application and starts a new instance immediately.</summary>
		/// <exception cref="T:System.NotSupportedException">Your code is not a Windows Forms application. You cannot call this method in this context.</exception>
		// Token: 0x0600021B RID: 539 RVA: 0x000100E8 File Offset: 0x0000E2E8
		public static void Restart()
		{
			if (Assembly.GetEntryAssembly() == null)
			{
				throw new NotSupportedException("The method 'Restart' is not supported by this application type.");
			}
			string text = null;
			PropertyInfo property = typeof(Environment).GetProperty("GacPath", 40);
			MethodInfo methodInfo = null;
			if (property != null)
			{
				methodInfo = property.GetGetMethod(true);
			}
			if (methodInfo != null)
			{
				string directoryName = Path.GetDirectoryName((string)methodInfo.Invoke(null, null));
				string directoryName2 = Path.GetDirectoryName(Path.GetDirectoryName(directoryName));
				if (XplatUI.RunningOnUnix)
				{
					text = Path.Combine(directoryName2, "bin/mono");
					if (!File.Exists(text))
					{
						text = "mono";
					}
				}
				else
				{
					text = Path.Combine(directoryName2, "bin\\mono.bat");
					if (!File.Exists(text))
					{
						text = Path.Combine(directoryName2, "bin\\mono.exe");
					}
					if (!File.Exists(text))
					{
						text = Path.Combine(directoryName2, "mono\\mono\\mini\\mono.exe");
					}
					if (!File.Exists(text))
					{
						throw new FileNotFoundException(string.Format("Windows mono path not found: '{0}'", text));
					}
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			for (int i = 0; i < commandLineArgs.Length; i++)
			{
				stringBuilder.Append(string.Format("\"{0}\" ", commandLineArgs[i]));
			}
			string text2 = stringBuilder.ToString();
			ProcessStartInfo startInfo = Process.GetCurrentProcess().StartInfo;
			if (text == null)
			{
				startInfo.FileName = commandLineArgs[0];
				startInfo.Arguments = text2.Remove(0, commandLineArgs[0].Length + 3);
			}
			else
			{
				startInfo.Arguments = text2;
				startInfo.FileName = text;
			}
			startInfo.WorkingDirectory = Environment.CurrentDirectory;
			Application.Exit();
			Process.Start(startInfo);
		}

		/// <summary>Informs all message pumps that they must terminate, and then closes all application windows after the messages have been processed.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600021C RID: 540 RVA: 0x0001028C File Offset: 0x0000E48C
		public static void Exit()
		{
			Application.Exit(new CancelEventArgs());
		}

		/// <summary>Informs all message pumps that they must terminate, and then closes all application windows after the messages have been processed.</summary>
		/// <param name="e">Returns whether any <see cref="T:System.Windows.Forms.Form" /> within the application cancelled the exit.</param>
		// Token: 0x0600021D RID: 541 RVA: 0x00010298 File Offset: 0x0000E498
		[EditorBrowsable(2)]
		public static void Exit(CancelEventArgs e)
		{
			FormCollection formCollection = Application.forms;
			lock (formCollection)
			{
				ArrayList arrayList = new ArrayList(Application.forms);
				foreach (object obj in arrayList)
				{
					Form form = (Form)obj;
					e.Cancel = form.FireClosingEvents(CloseReason.ApplicationExitCall, false);
					if (e.Cancel)
					{
						return;
					}
					form.suppress_closing_events = true;
					form.Close();
					form.Dispose();
				}
			}
			XplatUI.PostQuitMessage(0);
		}

		/// <summary>Exits the message loop on the current thread and closes all windows on the thread.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600021E RID: 542 RVA: 0x00010370 File Offset: 0x0000E570
		public static void ExitThread()
		{
			Application.CloseForms(Thread.CurrentThread);
			XplatUI.PostQuitMessage(0);
		}

		/// <summary>Initializes OLE on the current thread.</summary>
		/// <returns>One of the <see cref="T:System.Threading.ApartmentState" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600021F RID: 543 RVA: 0x00010384 File Offset: 0x0000E584
		public static ApartmentState OleRequired()
		{
			return 2;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Application.ThreadException" /> event. </summary>
		/// <param name="t">An <see cref="T:System.Exception" /> that represents the exception that was thrown. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000220 RID: 544 RVA: 0x00010388 File Offset: 0x0000E588
		public static void OnThreadException(Exception t)
		{
			if (Application.MWFThread.Current.HandlingException)
			{
				Console.WriteLine(t);
				Environment.Exit(1);
			}
			try
			{
				Application.MWFThread.Current.HandlingException = true;
				if (Application.ThreadException != null)
				{
					Application.ThreadException.Invoke(null, new ThreadExceptionEventArgs(t));
				}
				else if (SystemInformation.UserInteractive)
				{
					Form form = new ThreadExceptionDialog(t);
					form.ShowDialog();
				}
				else
				{
					Console.WriteLine(t.ToString());
					Application.Exit();
				}
			}
			finally
			{
				Application.MWFThread.Current.HandlingException = false;
			}
		}

		/// <summary>Removes a message filter from the message pump of the application.</summary>
		/// <param name="value">The implementation of the <see cref="T:System.Windows.Forms.IMessageFilter" /> to remove from the application. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000221 RID: 545 RVA: 0x00010438 File Offset: 0x0000E638
		public static void RemoveMessageFilter(IMessageFilter value)
		{
			ArrayList arrayList = Application.message_filters;
			lock (arrayList)
			{
				Application.message_filters.Remove(value);
			}
		}

		/// <summary>Begins running a standard application message loop on the current thread, without a form.</summary>
		/// <exception cref="T:System.InvalidOperationException">A main message loop is already running on this thread. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000222 RID: 546 RVA: 0x00010484 File Offset: 0x0000E684
		public static void Run()
		{
			Application.Run(new ApplicationContext());
		}

		/// <summary>Begins running a standard application message loop on the current thread, and makes the specified form visible.</summary>
		/// <param name="mainForm">A <see cref="T:System.Windows.Forms.Form" /> that represents the form to make visible. </param>
		/// <exception cref="T:System.InvalidOperationException">A main message loop is already running on the current thread. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000223 RID: 547 RVA: 0x00010490 File Offset: 0x0000E690
		public static void Run(Form mainForm)
		{
			Application.Run(new ApplicationContext(mainForm));
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000104A0 File Offset: 0x0000E6A0
		internal static void FirePreRun()
		{
			EventHandler preRun = Application.PreRun;
			if (preRun != null)
			{
				preRun.Invoke(null, EventArgs.Empty);
			}
		}

		/// <summary>Begins running a standard application message loop on the current thread, with an <see cref="T:System.Windows.Forms.ApplicationContext" />.</summary>
		/// <param name="context">An <see cref="T:System.Windows.Forms.ApplicationContext" /> in which the application is run. </param>
		/// <exception cref="T:System.InvalidOperationException">A main message loop is already running on this thread. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000225 RID: 549 RVA: 0x000104C8 File Offset: 0x0000E6C8
		public static void Run(ApplicationContext context)
		{
			if (SynchronizationContext.Current == null)
			{
				SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
			}
			Application.RunLoop(false, context);
			if (SynchronizationContext.Current is WindowsFormsSynchronizationContext)
			{
				WindowsFormsSynchronizationContext.Uninstall();
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000104FC File Offset: 0x0000E6FC
		private static void DisableFormsForModalLoop(Queue toplevels, ApplicationContext context)
		{
			FormCollection formCollection = Application.forms;
			lock (formCollection)
			{
				IEnumerator enumerator = Application.forms.GetEnumerator();
				IL_009E:
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Form form = (Form)obj;
					if (form != context.MainForm)
					{
						Control control = form;
						bool flag = false;
						while (control.Parent != context.MainForm)
						{
							control = control.Parent;
							if (control == null)
							{
								IL_0064:
								if (flag)
								{
									goto IL_009E;
								}
								if (form.IsHandleCreated && XplatUI.IsEnabled(form.Handle))
								{
									XplatUI.EnableWindow(form.Handle, false);
									toplevels.Enqueue(form);
									goto IL_009E;
								}
								goto IL_009E;
							}
						}
						flag = true;
						goto IL_0064;
					}
				}
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000105DC File Offset: 0x0000E7DC
		private static void EnableFormsForModalLoop(Queue toplevels, ApplicationContext context)
		{
			while (toplevels.Count > 0)
			{
				Form form = (Form)toplevels.Dequeue();
				if (form.IsHandleCreated)
				{
					XplatUI.EnableWindow(form.window.Handle, true);
					context.MainForm = form;
				}
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0001062C File Offset: 0x0000E82C
		internal static void RunLoop(bool Modal, ApplicationContext context)
		{
			Application.MWFThread mwfthread = Application.MWFThread.Current;
			MSG msg = default(MSG);
			if (context == null)
			{
				context = new ApplicationContext();
			}
			ApplicationContext context2 = mwfthread.Context;
			mwfthread.Context = context;
			if (context.MainForm != null)
			{
				context.MainForm.is_modal = Modal;
				context.MainForm.context = context;
				context.MainForm.closing = false;
				context.MainForm.Visible = true;
				if (context.MainForm != null)
				{
					context.MainForm.Activate();
				}
			}
			Queue queue;
			if (Modal)
			{
				queue = new Queue();
				Application.DisableFormsForModalLoop(queue, context);
				if (context.MainForm != null)
				{
					XplatUI.EnableWindow(context.MainForm.Handle, true);
					XplatUI.SetModal(context.MainForm.Handle, true);
				}
			}
			else
			{
				queue = null;
			}
			object obj = XplatUI.StartLoop(Thread.CurrentThread);
			mwfthread.MessageLoop = true;
			bool flag = false;
			while (!flag && XplatUI.GetMessage(obj, ref msg, IntPtr.Zero, 0, 0))
			{
				Message message = Message.Create(msg.hwnd, (int)msg.message, msg.wParam, msg.lParam);
				if (!Application.FilterMessage(ref message))
				{
					Msg message2 = msg.message;
					switch (message2)
					{
					case Msg.WM_KEYDOWN:
					case Msg.WM_KEYUP:
					case Msg.WM_CHAR:
					case Msg.WM_SYSKEYDOWN:
					case Msg.WM_SYSKEYUP:
					case Msg.WM_SYSCHAR:
					{
						Control control = Control.FromHandle(msg.hwnd);
						if (Application.keyboard_capture != null)
						{
							if (message.Msg == 260 && message.WParam.ToInt32() == 18)
							{
								Application.keyboard_capture.GetTopLevelToolStrip().Dismiss(ToolStripDropDownCloseReason.Keyboard);
								continue;
							}
							message.HWnd = Application.keyboard_capture.Handle;
							switch (Application.keyboard_capture.PreProcessControlMessageInternal(ref message))
							{
							case PreProcessControlState.MessageProcessed:
								continue;
							case PreProcessControlState.MessageNeeded:
							case PreProcessControlState.MessageNotNeeded:
								if ((message.Msg != 256 && message.Msg != 258) || Application.keyboard_capture.ProcessControlMnemonic((char)(int)message.WParam))
								{
									continue;
								}
								if (control == null || !Application.ControlOnToolStrip(control))
								{
									continue;
								}
								message.HWnd = msg.hwnd;
								break;
							}
						}
						if ((control != null && control.PreProcessControlMessageInternal(ref message) != PreProcessControlState.MessageProcessed) || control == null)
						{
							goto IL_0362;
						}
						break;
					}
					default:
						switch (message2)
						{
						case Msg.WM_LBUTTONDOWN:
						case Msg.WM_RBUTTONDOWN:
							break;
						default:
							if (message2 == Msg.WM_QUIT)
							{
								flag = true;
								goto IL_0377;
							}
							if (message2 != Msg.WM_MBUTTONDOWN)
							{
								goto IL_0362;
							}
							break;
						}
						if (Application.keyboard_capture != null)
						{
							Control control2 = Control.FromHandle(msg.hwnd);
							if (control2 == null)
							{
								ToolStripManager.FireAppClicked();
							}
							else if (control2 is ToolStrip)
							{
								if ((control2 as ToolStrip).GetTopLevelToolStrip() != Application.keyboard_capture.GetTopLevelToolStrip())
								{
									ToolStripManager.FireAppClicked();
								}
							}
							else if (control2.Parent == null || !(control2.Parent is ToolStripDropDownMenu) || (control2.Parent as ToolStripDropDownMenu).GetTopLevelToolStrip() != Application.keyboard_capture.GetTopLevelToolStrip())
							{
								if (control2.TopLevelControl != null)
								{
									ToolStripManager.FireAppClicked();
								}
							}
						}
						goto IL_0362;
					}
					IL_0377:
					if (context.MainForm == null || (!context.MainForm.closing && (!Modal || context.MainForm.Visible)))
					{
						continue;
					}
					if (!Modal)
					{
						XplatUI.PostQuitMessage(0);
						continue;
					}
					break;
					IL_0362:
					XplatUI.TranslateMessage(ref msg);
					XplatUI.DispatchMessage(ref msg);
					goto IL_0377;
				}
			}
			mwfthread.MessageLoop = false;
			XplatUI.EndLoop(Thread.CurrentThread);
			if (Modal)
			{
				Form mainForm = context.MainForm;
				context.MainForm = null;
				Application.EnableFormsForModalLoop(queue, context);
				if (context.MainForm != null && context.MainForm.IsHandleCreated)
				{
					XplatUI.SetModal(context.MainForm.Handle, false);
				}
				mainForm.RaiseCloseEvents(true, false);
				mainForm.is_modal = false;
			}
			if (context.MainForm != null)
			{
				context.MainForm.context = null;
				context.MainForm = null;
			}
			mwfthread.Context = context2;
			if (!Modal)
			{
				mwfthread.Exit();
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00010AB0 File Offset: 0x0000ECB0
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00010AB8 File Offset: 0x0000ECB8
		internal static ToolStrip KeyboardCapture
		{
			get
			{
				return Application.keyboard_capture;
			}
			set
			{
				Application.keyboard_capture = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00010AC0 File Offset: 0x0000ECC0
		internal static bool VisualStylesEnabled
		{
			get
			{
				return Application.visual_styles_enabled;
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00010AC8 File Offset: 0x0000ECC8
		internal static void AddForm(Form f)
		{
			FormCollection formCollection = Application.forms;
			lock (formCollection)
			{
				Application.forms.Add(f);
			}
			if (Application.FormAdded != null)
			{
				Application.FormAdded.Invoke(f, null);
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00010B2C File Offset: 0x0000ED2C
		internal static void RemoveForm(Form f)
		{
			FormCollection formCollection = Application.forms;
			lock (formCollection)
			{
				Application.forms.Remove(f);
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00010B78 File Offset: 0x0000ED78
		private static bool ControlOnToolStrip(Control c)
		{
			for (Control control = c.Parent; control != null; control = control.Parent)
			{
				if (control is ToolStrip)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00010BAC File Offset: 0x0000EDAC
		private static string CreateDataPath(string basePath)
		{
			string text = Path.Combine(basePath, Application.CompanyName);
			text = Path.Combine(text, Application.ProductName);
			text = Path.Combine(text, Application.ProductVersion);
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return text;
		}

		// Token: 0x040005AC RID: 1452
		private static bool browser_embedded;

		// Token: 0x040005AD RID: 1453
		private static InputLanguage input_language = InputLanguage.CurrentInputLanguage;

		// Token: 0x040005AE RID: 1454
		private static string safe_caption_format = "{1} - {0} - {2}";

		// Token: 0x040005AF RID: 1455
		private static readonly ArrayList message_filters = new ArrayList();

		// Token: 0x040005B0 RID: 1456
		private static readonly FormCollection forms = new FormCollection();

		// Token: 0x040005B1 RID: 1457
		private static bool use_wait_cursor;

		// Token: 0x040005B2 RID: 1458
		private static ToolStrip keyboard_capture;

		// Token: 0x040005B3 RID: 1459
		private static VisualStyleState visual_style_state = VisualStyleState.ClientAndNonClientAreasEnabled;

		// Token: 0x040005B4 RID: 1460
		private static bool visual_styles_enabled;

		// Token: 0x040005B5 RID: 1461
		internal static bool use_compatible_text_rendering = true;

		// Token: 0x02000040 RID: 64
		internal class MWFThread
		{
			// Token: 0x06000230 RID: 560 RVA: 0x00010BF0 File Offset: 0x0000EDF0
			private MWFThread()
			{
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x06000232 RID: 562 RVA: 0x00010C04 File Offset: 0x0000EE04
			// (set) Token: 0x06000233 RID: 563 RVA: 0x00010C0C File Offset: 0x0000EE0C
			public ApplicationContext Context
			{
				get
				{
					return this.context;
				}
				set
				{
					this.context = value;
				}
			}

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x06000234 RID: 564 RVA: 0x00010C18 File Offset: 0x0000EE18
			// (set) Token: 0x06000235 RID: 565 RVA: 0x00010C20 File Offset: 0x0000EE20
			public bool MessageLoop
			{
				get
				{
					return this.messageloop_started;
				}
				set
				{
					this.messageloop_started = value;
				}
			}

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x06000236 RID: 566 RVA: 0x00010C2C File Offset: 0x0000EE2C
			// (set) Token: 0x06000237 RID: 567 RVA: 0x00010C34 File Offset: 0x0000EE34
			public bool HandlingException
			{
				get
				{
					return this.handling_exception;
				}
				set
				{
					this.handling_exception = value;
				}
			}

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x06000238 RID: 568 RVA: 0x00010C40 File Offset: 0x0000EE40
			public static int LoopCount
			{
				get
				{
					Hashtable hashtable = Application.MWFThread.threads;
					int num2;
					lock (hashtable)
					{
						int num = 0;
						foreach (object obj in Application.MWFThread.threads.Values)
						{
							Application.MWFThread mwfthread = (Application.MWFThread)obj;
							if (mwfthread.messageloop_started)
							{
								num++;
							}
						}
						num2 = num;
					}
					return num2;
				}
			}

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x06000239 RID: 569 RVA: 0x00010CFC File Offset: 0x0000EEFC
			public static Application.MWFThread Current
			{
				get
				{
					Application.MWFThread mwfthread = null;
					Hashtable hashtable = Application.MWFThread.threads;
					lock (hashtable)
					{
						mwfthread = (Application.MWFThread)Application.MWFThread.threads[Thread.CurrentThread.GetHashCode()];
						if (mwfthread == null)
						{
							mwfthread = new Application.MWFThread();
							mwfthread.thread_id = Thread.CurrentThread.GetHashCode();
							Application.MWFThread.threads[mwfthread.thread_id] = mwfthread;
						}
					}
					return mwfthread;
				}
			}

			// Token: 0x0600023A RID: 570 RVA: 0x00010D94 File Offset: 0x0000EF94
			public void Exit()
			{
				if (this.context != null)
				{
					this.context.ExitThread();
				}
				this.context = null;
				if (Application.ThreadExit != null)
				{
					Application.ThreadExit.Invoke(null, EventArgs.Empty);
				}
				if (Application.MWFThread.LoopCount == 0 && Application.ApplicationExit != null)
				{
					Application.ApplicationExit.Invoke(null, EventArgs.Empty);
				}
				((Application.MWFThread)Application.MWFThread.threads[this.thread_id]).MessageLoop = false;
			}

			// Token: 0x040005BD RID: 1469
			private ApplicationContext context;

			// Token: 0x040005BE RID: 1470
			private bool messageloop_started;

			// Token: 0x040005BF RID: 1471
			private bool handling_exception;

			// Token: 0x040005C0 RID: 1472
			private int thread_id;

			// Token: 0x040005C1 RID: 1473
			private static readonly Hashtable threads = new Hashtable();
		}

		/// <summary>Represents a method that will check whether the hosting environment is still sending messages. </summary>
		/// <returns>true if the hosting environment is still sending messages; otherwise, false.</returns>
		// Token: 0x02000634 RID: 1588
		// (Invoke) Token: 0x06005082 RID: 20610
		[EditorBrowsable(2)]
		public delegate bool MessageLoopCallback();
	}
}
