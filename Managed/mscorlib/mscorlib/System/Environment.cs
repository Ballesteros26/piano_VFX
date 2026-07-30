using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Mono;

namespace System
{
	/// <summary>Provides information about, and means to manipulate, the current environment and platform. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001F7 RID: 503
	[ComVisible(true)]
	public static class Environment
	{
		// Token: 0x0600176B RID: 5995 RVA: 0x00002119 File Offset: 0x00000319
		internal static string GetResourceString(string key)
		{
			return key;
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00002119 File Offset: 0x00000319
		internal static string GetResourceString(string key, CultureInfo culture)
		{
			return key;
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0005B92F File Offset: 0x00059B2F
		internal static string GetResourceString(string key, params object[] values)
		{
			return string.Format(CultureInfo.InvariantCulture, key, values);
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00002119 File Offset: 0x00000319
		internal static string GetRuntimeResourceString(string key)
		{
			return key;
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0005B92F File Offset: 0x00059B2F
		internal static string GetRuntimeResourceString(string key, params object[] values)
		{
			return string.Format(CultureInfo.InvariantCulture, key, values);
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0005B940 File Offset: 0x00059B40
		internal static string GetResourceStringEncodingName(int codePage)
		{
			if (codePage <= 12000)
			{
				if (codePage == 1200)
				{
					return Environment.GetResourceString("Unicode");
				}
				if (codePage == 1201)
				{
					return Environment.GetResourceString("Unicode (Big-Endian)");
				}
				if (codePage == 12000)
				{
					return Environment.GetResourceString("Unicode (UTF-32)");
				}
			}
			else if (codePage <= 20127)
			{
				if (codePage == 12001)
				{
					return Environment.GetResourceString("Unicode (UTF-32 Big-Endian)");
				}
				if (codePage == 20127)
				{
					return Environment.GetResourceString("US-ASCII");
				}
			}
			else
			{
				if (codePage == 65000)
				{
					return Environment.GetResourceString("Unicode (UTF-7)");
				}
				if (codePage == 65001)
				{
					return Environment.GetResourceString("Unicode (UTF-8)");
				}
			}
			return codePage.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal static bool IsWindows8OrAbove
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the command line for this process.</summary>
		/// <returns>A string containing command-line arguments.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="Path" />
		/// </PermissionSet>
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x0005B9F4 File Offset: 0x00059BF4
		public static string CommandLine
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in Environment.GetCommandLineArgs())
				{
					bool flag = false;
					string text2 = "";
					string text3 = text;
					for (int j = 0; j < text3.Length; j++)
					{
						if (text2.Length == 0 && char.IsWhiteSpace(text3[j]))
						{
							text2 = "\"";
						}
						else if (text3[j] == '"')
						{
							flag = true;
						}
					}
					if (flag && text2.Length != 0)
					{
						text3 = text3.Replace("\"", "\\\"");
					}
					stringBuilder.AppendFormat("{0}{1}{0} ", text2, text3);
				}
				if (stringBuilder.Length > 0)
				{
					StringBuilder stringBuilder2 = stringBuilder;
					int i = stringBuilder2.Length;
					stringBuilder2.Length = i - 1;
				}
				return stringBuilder.ToString();
			}
		}

		/// <summary>Gets or sets the fully qualified path of the current working directory.</summary>
		/// <returns>A string containing a directory path.</returns>
		/// <exception cref="T:System.ArgumentException">Attempted to set to an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentNullException">Attempted to set to null.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">Attempted to set a local path that cannot be found.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the appropriate permission.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x0005BAC4 File Offset: 0x00059CC4
		// (set) Token: 0x06001774 RID: 6004 RVA: 0x0005BACB File Offset: 0x00059CCB
		public static string CurrentDirectory
		{
			get
			{
				return Directory.GetCurrentDirectory();
			}
			set
			{
				Directory.SetCurrentDirectory(value);
			}
		}

		/// <summary>Gets a unique identifier for the current managed thread.</summary>
		/// <returns>An integer that represents a unique identifier for this managed thread.</returns>
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x0005BAD3 File Offset: 0x00059CD3
		public static int CurrentManagedThreadId
		{
			get
			{
				return Thread.CurrentThread.ManagedThreadId;
			}
		}

		/// <summary>Gets or sets the exit code of the process.</summary>
		/// <returns>A 32-bit signed integer containing the exit code. The default value is 0 (zero), which indicates that the process completed successfully.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06001776 RID: 6006
		// (set) Token: 0x06001777 RID: 6007
		public static extern int ExitCode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		/// <summary>Gets a value indicating whether the common language runtime (CLR) is shutting down.</summary>
		/// <returns>true if the CLR is shutting down; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06001778 RID: 6008
		public static extern bool HasShutdownStarted
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		/// <summary>Gets the NetBIOS name of this local computer.</summary>
		/// <returns>A string containing the name of this computer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The name of this computer cannot be obtained.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="COMPUTERNAME" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06001779 RID: 6009
		public static extern string MachineName
		{
			[EnvironmentPermission(SecurityAction.Demand, Read = "COMPUTERNAME")]
			[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600177A RID: 6010
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetNewLine();

		/// <summary>Gets the newline string defined for this environment.</summary>
		/// <returns>A string containing "\r\n" for non-Unix platforms, or a string containing "\n" for Unix platforms.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x0005BADF File Offset: 0x00059CDF
		public static string NewLine
		{
			get
			{
				if (Environment.nl != null)
				{
					return Environment.nl;
				}
				Environment.nl = Environment.GetNewLine();
				return Environment.nl;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x0600177C RID: 6012
		private static extern PlatformID Platform
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600177D RID: 6013
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetOSVersionString();

		/// <summary>Gets an <see cref="T:System.OperatingSystem" /> object that contains the current platform identifier and version number.</summary>
		/// <returns>An object that contains the platform identifier and version number.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property was unable to obtain the system version.-or- The obtained platform identifier is not a member of <see cref="T:System.PlatformID" /></exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x0005BB00 File Offset: 0x00059D00
		public static OperatingSystem OSVersion
		{
			get
			{
				if (Environment.os == null)
				{
					Version version = Environment.CreateVersionFromString(Environment.GetOSVersionString());
					PlatformID platformID = Environment.Platform;
					if (platformID == PlatformID.MacOSX)
					{
						platformID = PlatformID.Unix;
					}
					Environment.os = new OperatingSystem(platformID, version);
				}
				return Environment.os;
			}
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0005BB3C File Offset: 0x00059D3C
		internal static Version CreateVersionFromString(string info)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 1;
			int num6 = -1;
			if (info == null)
			{
				return new Version(0, 0, 0, 0);
			}
			foreach (char c in info)
			{
				if (char.IsDigit(c))
				{
					if (num6 < 0)
					{
						num6 = (int)(c - '0');
					}
					else
					{
						num6 = num6 * 10 + (int)(c - '0');
					}
				}
				else if (num6 >= 0)
				{
					switch (num5)
					{
					case 1:
						num = num6;
						break;
					case 2:
						num2 = num6;
						break;
					case 3:
						num3 = num6;
						break;
					case 4:
						num4 = num6;
						break;
					}
					num6 = -1;
					num5++;
				}
				if (num5 == 5)
				{
					break;
				}
			}
			if (num6 >= 0)
			{
				switch (num5)
				{
				case 1:
					num = num6;
					break;
				case 2:
					num2 = num6;
					break;
				case 3:
					num3 = num6;
					break;
				case 4:
					num4 = num6;
					break;
				}
			}
			return new Version(num, num2, num3, num4);
		}

		/// <summary>Gets current stack trace information.</summary>
		/// <returns>A string containing stack trace information. This value can be <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The requested stack trace information is out of range.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x0005BC24 File Offset: 0x00059E24
		public static string StackTrace
		{
			[EnvironmentPermission(SecurityAction.Demand, Unrestricted = true)]
			get
			{
				return new StackTrace(0, true).ToString();
			}
		}

		/// <summary>Gets the fully qualified path of the system directory.</summary>
		/// <returns>A string containing a directory path.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x0005BC32 File Offset: 0x00059E32
		public static string SystemDirectory
		{
			get
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.System);
			}
		}

		/// <summary>Gets the number of milliseconds elapsed since the system started.</summary>
		/// <returns>A 32-bit signed integer containing the amount of time in milliseconds that has passed since the last time the computer was started.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001782 RID: 6018
		public static extern int TickCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		/// <summary>Gets the network domain name associated with the current user.</summary>
		/// <returns>The network domain name associated with the current user.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system does not support retrieving the network domain name.</exception>
		/// <exception cref="T:System.InvalidOperationException">The network domain name cannot be retrieved.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="UserName;UserDomainName" />
		/// </PermissionSet>
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x0005BC3B File Offset: 0x00059E3B
		public static string UserDomainName
		{
			[EnvironmentPermission(SecurityAction.Demand, Read = "USERDOMAINNAME")]
			get
			{
				return Environment.MachineName;
			}
		}

		/// <summary>Gets a value indicating whether the current process is running in user interactive mode.</summary>
		/// <returns>true if the current process is running in user interactive mode; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("Currently always returns false, regardless of interactive state")]
		public static bool UserInteractive
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the user name of the person who is currently logged on to the Windows operating system.</summary>
		/// <returns>The user name of the person who is logged on to Windows.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="UserName" />
		/// </PermissionSet>
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001785 RID: 6021
		public static extern string UserName
		{
			[EnvironmentPermission(SecurityAction.Demand, Read = "USERNAME;USER")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		/// <summary>Gets a <see cref="T:System.Version" /> object that describes the major, minor, build, and revision numbers of the common language runtime.</summary>
		/// <returns>An object that displays the version of the common language runtime.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x0005BC42 File Offset: 0x00059E42
		public static Version Version
		{
			get
			{
				return new Version("4.0.30319.42000");
			}
		}

		/// <summary>Gets the amount of physical memory mapped to the process context.</summary>
		/// <returns>A 64-bit signed integer containing the number of bytes of physical memory mapped to the process context.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x0005BC4E File Offset: 0x00059E4E
		[MonoTODO("Currently always returns zero")]
		public static long WorkingSet
		{
			[EnvironmentPermission(SecurityAction.Demand, Unrestricted = true)]
			get
			{
				return 0L;
			}
		}

		/// <summary>Terminates this process and gives the underlying operating system the specified exit code.</summary>
		/// <param name="exitCode">Exit code to be given to the operating system. Use 0 (zero) to indicate that the process completed successfully.</param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have sufficient security permission to perform this function.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001788 RID: 6024
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Exit(int exitCode);

		// Token: 0x06001789 RID: 6025 RVA: 0x0005BC52 File Offset: 0x00059E52
		internal static void _Exit(int exitCode)
		{
			Environment.Exit(exitCode);
		}

		/// <summary>Replaces the name of each environment variable embedded in the specified string with the string equivalent of the value of the variable, then returns the resulting string.</summary>
		/// <returns>A string with each environment variable replaced by its value.</returns>
		/// <param name="name">A string containing the names of zero or more environment variables. Each environment variable is quoted with the percent sign character (%).</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600178A RID: 6026 RVA: 0x0005BC5C File Offset: 0x00059E5C
		public static string ExpandEnvironmentVariables(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			int num = name.IndexOf('%');
			if (num == -1)
			{
				return name;
			}
			int length = name.Length;
			int num2;
			if (num == length - 1 || (num2 = name.IndexOf('%', num + 1)) == -1)
			{
				return name;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(name, 0, num);
			Hashtable hashtable = null;
			do
			{
				string text = name.Substring(num + 1, num2 - num - 1);
				string text2 = Environment.GetEnvironmentVariable(text);
				if (text2 == null && Environment.IsRunningOnWindows)
				{
					if (hashtable == null)
					{
						hashtable = Environment.GetEnvironmentVariablesNoCase();
					}
					text2 = hashtable[text] as string;
				}
				int num3 = num2;
				if (text2 == null)
				{
					stringBuilder.Append('%');
					stringBuilder.Append(text);
					num2--;
				}
				else
				{
					stringBuilder.Append(text2);
				}
				int num4 = num2;
				num = name.IndexOf('%', num2 + 1);
				num2 = ((num == -1 || num2 > length - 1) ? (-1) : name.IndexOf('%', num + 1));
				int num5;
				if (num == -1 || num2 == -1)
				{
					num5 = length - num4 - 1;
				}
				else if (text2 != null)
				{
					num5 = num - num4 - 1;
				}
				else
				{
					num5 = num - num3;
				}
				if (num >= num4 || num == -1)
				{
					stringBuilder.Append(name, num4 + 1, num5);
				}
			}
			while (num2 > -1 && num2 < length);
			return stringBuilder.ToString();
		}

		/// <summary>Returns a string array containing the command-line arguments for the current process.</summary>
		/// <returns>An array of string where each element contains a command-line argument. The first element is the executable file name, and the following zero or more elements contain the remaining command-line arguments.</returns>
		/// <exception cref="T:System.NotSupportedException">The system does not support command-line arguments.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="Path" />
		/// </PermissionSet>
		// Token: 0x0600178B RID: 6027
		[EnvironmentPermission(SecurityAction.Demand, Read = "PATH")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetCommandLineArgs();

		// Token: 0x0600178C RID: 6028
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string internalGetEnvironmentVariable_native(IntPtr variable);

		// Token: 0x0600178D RID: 6029 RVA: 0x0005BD98 File Offset: 0x00059F98
		internal static string internalGetEnvironmentVariable(string variable)
		{
			if (variable == null)
			{
				return null;
			}
			string text;
			using (SafeStringMarshal safeStringMarshal = RuntimeMarshal.MarshalString(variable))
			{
				text = Environment.internalGetEnvironmentVariable_native(safeStringMarshal.Value);
			}
			return text;
		}

		/// <summary>Retrieves the value of an environment variable from the current process.</summary>
		/// <returns>The value of the environment variable specified by <paramref name="variable" />, or null if the environment variable is not found.</returns>
		/// <param name="variable">The name of the environment variable.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="variable" /> is null.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to perform this operation.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600178E RID: 6030 RVA: 0x0005BDE0 File Offset: 0x00059FE0
		public static string GetEnvironmentVariable(string variable)
		{
			return Environment.internalGetEnvironmentVariable(variable);
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x0005BDE8 File Offset: 0x00059FE8
		private static Hashtable GetEnvironmentVariablesNoCase()
		{
			Hashtable hashtable = new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
			foreach (string text in Environment.GetEnvironmentVariableNames())
			{
				hashtable[text] = Environment.internalGetEnvironmentVariable(text);
			}
			return hashtable;
		}

		/// <summary>Retrieves all environment variable names and their values from the current process.</summary>
		/// <returns>A dictionary that contains all environment variable names and their values; otherwise, an empty dictionary if no environment variables are found.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to perform this operation.</exception>
		/// <exception cref="T:System.OutOfMemoryException">The buffer is out of memory.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001790 RID: 6032 RVA: 0x0005BE2C File Offset: 0x0005A02C
		public static IDictionary GetEnvironmentVariables()
		{
			StringBuilder stringBuilder = null;
			if (SecurityManager.SecurityEnabled)
			{
				stringBuilder = new StringBuilder();
			}
			Hashtable hashtable = new Hashtable();
			foreach (string text in Environment.GetEnvironmentVariableNames())
			{
				hashtable[text] = Environment.internalGetEnvironmentVariable(text);
				if (stringBuilder != null)
				{
					stringBuilder.Append(text);
					stringBuilder.Append(";");
				}
			}
			if (stringBuilder != null)
			{
				new EnvironmentPermission(EnvironmentPermissionAccess.Read, stringBuilder.ToString()).Demand();
			}
			return hashtable;
		}

		/// <summary>Gets the path to the system special folder that is identified by the specified enumeration.</summary>
		/// <returns>The path to the specified system special folder, if that folder physically exists on your computer; otherwise, an empty string ("").A folder will not physically exist if the operating system did not create it, the existing folder was deleted, or the folder is a virtual directory, such as My Computer, which does not correspond to a physical path.</returns>
		/// <param name="folder">An enumerated constant that identifies a system special folder.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="folder" /> is not a member of <see cref="T:System.Environment.SpecialFolder" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current platform is not supported.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001791 RID: 6033 RVA: 0x0005BEA3 File Offset: 0x0005A0A3
		public static string GetFolderPath(Environment.SpecialFolder folder)
		{
			return Environment.GetFolderPath(folder, Environment.SpecialFolderOption.None);
		}

		// Token: 0x06001792 RID: 6034
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetWindowsFolderPath(int folder);

		/// <summary>Gets the path to the system special folder that is identified by the specified enumeration, and uses a specified option for accessing special folders.</summary>
		/// <returns>The path to the specified system special folder, if that folder physically exists on your computer; otherwise, an empty string ("").A folder will not physically exist if the operating system did not create it, the existing folder was deleted, or the folder is a virtual directory, such as My Computer, which does not correspond to a physical path.</returns>
		/// <param name="folder">An enumerated constant that identifies a system special folder.</param>
		/// <param name="option">Specifies options to use for accessing a special folder.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="folder" /> is not a member of <see cref="T:System.Environment.SpecialFolder" /></exception>
		/// <exception cref="T:System.PlatformNotSupportedException">
		///   <see cref="T:System.PlatformNotSupportedException" />
		/// </exception>
		// Token: 0x06001793 RID: 6035 RVA: 0x0005BEAC File Offset: 0x0005A0AC
		public static string GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
		{
			string text;
			if (Environment.IsRunningOnWindows)
			{
				text = Environment.GetWindowsFolderPath((int)folder);
			}
			else
			{
				text = Environment.UnixGetFolderPath(folder, option);
			}
			return text;
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0005BED4 File Offset: 0x0005A0D4
		private static string ReadXdgUserDir(string config_dir, string home_dir, string key, string fallback)
		{
			string text = Environment.internalGetEnvironmentVariable(key);
			if (text != null && text != string.Empty)
			{
				return text;
			}
			string text2 = Path.Combine(config_dir, "user-dirs.dirs");
			if (!File.Exists(text2))
			{
				return Path.Combine(home_dir, fallback);
			}
			try
			{
				using (StreamReader streamReader = new StreamReader(text2))
				{
					string text3;
					while ((text3 = streamReader.ReadLine()) != null)
					{
						text3 = text3.Trim();
						int num = text3.IndexOf('=');
						if (num > 8 && text3.Substring(0, num) == key)
						{
							string text4 = text3.Substring(num + 1).Trim(new char[] { '"' });
							bool flag = false;
							if (text4.StartsWithOrdinalUnchecked("$HOME/"))
							{
								flag = true;
								text4 = text4.Substring(6);
							}
							else if (!text4.StartsWithOrdinalUnchecked("/"))
							{
								flag = true;
							}
							return flag ? Path.Combine(home_dir, text4) : text4;
						}
					}
				}
			}
			catch (FileNotFoundException)
			{
			}
			return Path.Combine(home_dir, fallback);
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0005BFF0 File Offset: 0x0005A1F0
		internal static string UnixGetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
		{
			string text = Environment.internalGetHome();
			string text2 = Environment.internalGetEnvironmentVariable("XDG_DATA_HOME");
			if (text2 == null || text2 == string.Empty)
			{
				text2 = Path.Combine(text, ".local");
				text2 = Path.Combine(text2, "share");
			}
			string text3 = Environment.internalGetEnvironmentVariable("XDG_CONFIG_HOME");
			if (text3 == null || text3 == string.Empty)
			{
				text3 = Path.Combine(text, ".config");
			}
			switch (folder)
			{
			case Environment.SpecialFolder.Desktop:
			case Environment.SpecialFolder.DesktopDirectory:
				return Environment.ReadXdgUserDir(text3, text, "XDG_DESKTOP_DIR", "Desktop");
			case Environment.SpecialFolder.Programs:
			case Environment.SpecialFolder.Startup:
			case Environment.SpecialFolder.Recent:
			case Environment.SpecialFolder.SendTo:
			case Environment.SpecialFolder.StartMenu:
			case Environment.SpecialFolder.NetworkShortcuts:
			case Environment.SpecialFolder.CommonStartMenu:
			case Environment.SpecialFolder.CommonPrograms:
			case Environment.SpecialFolder.CommonStartup:
			case Environment.SpecialFolder.CommonDesktopDirectory:
			case Environment.SpecialFolder.PrinterShortcuts:
			case Environment.SpecialFolder.Cookies:
			case Environment.SpecialFolder.History:
			case Environment.SpecialFolder.Windows:
			case Environment.SpecialFolder.System:
			case Environment.SpecialFolder.SystemX86:
			case Environment.SpecialFolder.ProgramFilesX86:
			case Environment.SpecialFolder.CommonProgramFiles:
			case Environment.SpecialFolder.CommonProgramFilesX86:
			case Environment.SpecialFolder.CommonDocuments:
			case Environment.SpecialFolder.CommonAdminTools:
			case Environment.SpecialFolder.AdminTools:
			case Environment.SpecialFolder.CommonMusic:
			case Environment.SpecialFolder.CommonPictures:
			case Environment.SpecialFolder.CommonVideos:
			case Environment.SpecialFolder.Resources:
			case Environment.SpecialFolder.LocalizedResources:
			case Environment.SpecialFolder.CommonOemLinks:
			case Environment.SpecialFolder.CDBurning:
				return string.Empty;
			case Environment.SpecialFolder.MyDocuments:
				return text;
			case Environment.SpecialFolder.Favorites:
				if (Environment.Platform == PlatformID.MacOSX)
				{
					return Path.Combine(text, "Library", "Favorites");
				}
				return string.Empty;
			case Environment.SpecialFolder.MyMusic:
				if (Environment.Platform == PlatformID.MacOSX)
				{
					return Path.Combine(text, "Music");
				}
				return Environment.ReadXdgUserDir(text3, text, "XDG_MUSIC_DIR", "Music");
			case Environment.SpecialFolder.MyVideos:
				return Environment.ReadXdgUserDir(text3, text, "XDG_VIDEOS_DIR", "Videos");
			case Environment.SpecialFolder.MyComputer:
				return string.Empty;
			case Environment.SpecialFolder.Fonts:
				if (Environment.Platform == PlatformID.MacOSX)
				{
					return Path.Combine(text, "Library", "Fonts");
				}
				return Path.Combine(text, ".fonts");
			case Environment.SpecialFolder.Templates:
				return Environment.ReadXdgUserDir(text3, text, "XDG_TEMPLATES_DIR", "Templates");
			case Environment.SpecialFolder.ApplicationData:
				return text3;
			case Environment.SpecialFolder.LocalApplicationData:
				return text2;
			case Environment.SpecialFolder.InternetCache:
				if (Environment.Platform == PlatformID.MacOSX)
				{
					return Path.Combine(text, "Library", "Caches");
				}
				return string.Empty;
			case Environment.SpecialFolder.CommonApplicationData:
				return "/usr/share";
			case Environment.SpecialFolder.ProgramFiles:
				if (Environment.Platform == PlatformID.MacOSX)
				{
					return "/Applications";
				}
				return string.Empty;
			case Environment.SpecialFolder.MyPictures:
				if (Environment.Platform == PlatformID.MacOSX)
				{
					return Path.Combine(text, "Pictures");
				}
				return Environment.ReadXdgUserDir(text3, text, "XDG_PICTURES_DIR", "Pictures");
			case Environment.SpecialFolder.UserProfile:
				return text;
			case Environment.SpecialFolder.CommonTemplates:
				return "/usr/share/templates";
			}
			throw new ArgumentException("Invalid SpecialFolder");
		}

		/// <summary>Returns an array of string containing the names of the logical drives on the current computer.</summary>
		/// <returns>An array of strings where each element contains the name of a logical drive. For example, if the computer's hard drive is the first logical drive, the first element returned is "C:\".</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001796 RID: 6038 RVA: 0x0005C27B File Offset: 0x0005A47B
		[EnvironmentPermission(SecurityAction.Demand, Unrestricted = true)]
		public static string[] GetLogicalDrives()
		{
			return Environment.GetLogicalDrivesInternal();
		}

		// Token: 0x06001797 RID: 6039
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void internalBroadcastSettingChange();

		/// <summary>Retrieves the value of an environment variable from the current process or from the Windows operating system registry key for the current user or local machine.</summary>
		/// <returns>The value of the environment variable specified by the <paramref name="variable" /> and <paramref name="target" /> parameters, or null if the environment variable is not found.</returns>
		/// <param name="variable">The name of an environment variable.</param>
		/// <param name="target">One of the <see cref="T:System.EnvironmentVariableTarget" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="variable" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not a valid <see cref="T:System.EnvironmentVariableTarget" /> value.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to perform this operation.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001798 RID: 6040 RVA: 0x0005C284 File Offset: 0x0005A484
		public static string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
		{
			switch (target)
			{
			case EnvironmentVariableTarget.Process:
				return Environment.GetEnvironmentVariable(variable);
			case EnvironmentVariableTarget.User:
				break;
			case EnvironmentVariableTarget.Machine:
			{
				new EnvironmentPermission(PermissionState.Unrestricted).Demand();
				if (!Environment.IsRunningOnWindows)
				{
					return null;
				}
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment"))
				{
					object value = registryKey.GetValue(variable);
					return (value == null) ? null : value.ToString();
				}
				break;
			}
			default:
				goto IL_00AC;
			}
			new EnvironmentPermission(PermissionState.Unrestricted).Demand();
			if (!Environment.IsRunningOnWindows)
			{
				return null;
			}
			using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Environment", false))
			{
				object value2 = registryKey2.GetValue(variable);
				return (value2 == null) ? null : value2.ToString();
			}
			IL_00AC:
			throw new ArgumentException("target");
		}

		/// <summary>Retrieves all environment variable names and their values from the current process, or from the Windows operating system registry key for the current user or local machine.</summary>
		/// <returns>A dictionary that contains all environment variable names and their values from the source specified by the <paramref name="target" /> parameter; otherwise, an empty dictionary if no environment variables are found.</returns>
		/// <param name="target">One of the <see cref="T:System.EnvironmentVariableTarget" /> values.</param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to perform this operation for the specified value of <paramref name="target" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> contains an illegal value.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001799 RID: 6041 RVA: 0x0005C368 File Offset: 0x0005A568
		public static IDictionary GetEnvironmentVariables(EnvironmentVariableTarget target)
		{
			IDictionary dictionary = new Hashtable();
			switch (target)
			{
			case EnvironmentVariableTarget.Process:
				return Environment.GetEnvironmentVariables();
			case EnvironmentVariableTarget.User:
				break;
			case EnvironmentVariableTarget.Machine:
			{
				new EnvironmentPermission(PermissionState.Unrestricted).Demand();
				if (!Environment.IsRunningOnWindows)
				{
					return dictionary;
				}
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment"))
				{
					foreach (string text in registryKey.GetValueNames())
					{
						dictionary.Add(text, registryKey.GetValue(text));
					}
					return dictionary;
				}
				break;
			}
			default:
				goto IL_00E0;
			}
			new EnvironmentPermission(PermissionState.Unrestricted).Demand();
			if (!Environment.IsRunningOnWindows)
			{
				return dictionary;
			}
			using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Environment"))
			{
				foreach (string text2 in registryKey2.GetValueNames())
				{
					dictionary.Add(text2, registryKey2.GetValue(text2));
				}
				return dictionary;
			}
			IL_00E0:
			throw new ArgumentException("target");
		}

		/// <summary>Creates, modifies, or deletes an environment variable stored in the current process.</summary>
		/// <param name="variable">The name of an environment variable.</param>
		/// <param name="value">A value to assign to <paramref name="variable" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="variable" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="variable" /> contains a zero-length string, an initial hexadecimal zero character (0x00), or an equal sign ("="). -or-The length of <paramref name="variable" /> or <paramref name="value" /> is greater than or equal to 32,767 characters.-or-An error occurred during the execution of this operation.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to perform this operation.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600179A RID: 6042 RVA: 0x0005C480 File Offset: 0x0005A680
		[EnvironmentPermission(SecurityAction.Demand, Unrestricted = true)]
		public static void SetEnvironmentVariable(string variable, string value)
		{
			Environment.SetEnvironmentVariable(variable, value, EnvironmentVariableTarget.Process);
		}

		/// <summary>Creates, modifies, or deletes an environment variable stored in the current process or in the Windows operating system registry key reserved for the current user or local machine.</summary>
		/// <param name="variable">The name of an environment variable.</param>
		/// <param name="value">A value to assign to <paramref name="variable" />.</param>
		/// <param name="target">One of the <see cref="T:System.EnvironmentVariableTarget" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="variable" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="variable" /> contains a zero-length string, an initial hexadecimal zero character (0x00), or an equal sign ("="). -or-The length of <paramref name="variable" /> is greater than or equal to 32,767 characters.-or-<paramref name="target" /> is not a member of the <see cref="T:System.EnvironmentVariableTarget" /> enumeration. -or-<paramref name="target" /> is <see cref="F:System.EnvironmentVariableTarget.Machine" /> or <see cref="F:System.EnvironmentVariableTarget.User" /> and the length of <paramref name="variable" /> is greater than or equal to 255.-or-<paramref name="target" /> is <see cref="F:System.EnvironmentVariableTarget.Process" /> and the length of <paramref name="value" /> is greater than or equal to 32,767 characters. -or-An error occurred during the execution of this operation.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission to perform this operation.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600179B RID: 6043 RVA: 0x0005C48C File Offset: 0x0005A68C
		[EnvironmentPermission(SecurityAction.Demand, Unrestricted = true)]
		public static void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (variable == string.Empty)
			{
				throw new ArgumentException("String cannot be of zero length.", "variable");
			}
			if (variable.IndexOf('=') != -1)
			{
				throw new ArgumentException("Environment variable name cannot contain an equal character.", "variable");
			}
			if (variable[0] == '\0')
			{
				throw new ArgumentException("The first char in the string is the null character.", "variable");
			}
			switch (target)
			{
			case EnvironmentVariableTarget.Process:
				Environment.InternalSetEnvironmentVariable(variable, value);
				return;
			case EnvironmentVariableTarget.User:
				break;
			case EnvironmentVariableTarget.Machine:
			{
				if (!Environment.IsRunningOnWindows)
				{
					return;
				}
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment", true))
				{
					if (string.IsNullOrEmpty(value))
					{
						registryKey.DeleteValue(variable, false);
					}
					else
					{
						registryKey.SetValue(variable, value);
					}
					Environment.internalBroadcastSettingChange();
					return;
				}
				break;
			}
			default:
				goto IL_0106;
			}
			if (!Environment.IsRunningOnWindows)
			{
				return;
			}
			using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Environment", true))
			{
				if (string.IsNullOrEmpty(value))
				{
					registryKey2.DeleteValue(variable, false);
				}
				else
				{
					registryKey2.SetValue(variable, value);
				}
				Environment.internalBroadcastSettingChange();
				return;
			}
			IL_0106:
			throw new ArgumentException("target");
		}

		// Token: 0x0600179C RID: 6044
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalSetEnvironmentVariable(string variable, string value);

		/// <summary>Immediately terminates a process after writing a message to the Windows Application event log, and then includes the message in error reporting to Microsoft.</summary>
		/// <param name="message">A message that explains why the process was terminated, or null if no explanation is provided.</param>
		// Token: 0x0600179D RID: 6045 RVA: 0x0002126B File Offset: 0x0001F46B
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		public static void FailFast(string message)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0002126B File Offset: 0x0001F46B
		internal static void FailFast(string message, uint exitCode)
		{
			throw new NotImplementedException();
		}

		/// <summary>Immediately terminates a process after writing a message to the Windows Application event log, and then includes the message and exception information in error reporting to Microsoft.</summary>
		/// <param name="message">A message that explains why the process was terminated, or null if no explanation is provided.</param>
		/// <param name="exception">An exception that represents the error that caused the termination. This is typically the exception in a catch block.</param>
		// Token: 0x0600179F RID: 6047 RVA: 0x0005C5C8 File Offset: 0x0005A7C8
		[SecurityCritical]
		public static void FailFast(string message, Exception exception)
		{
			throw new ExecutionEngineException(message, exception);
		}

		// Token: 0x060017A0 RID: 6048
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIs64BitOperatingSystem();

		/// <summary>Determines whether the current operating system is a 64-bit operating system.</summary>
		/// <returns>true if the operating system is 64-bit; otherwise, false.</returns>
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x0005C5D1 File Offset: 0x0005A7D1
		public static bool Is64BitOperatingSystem
		{
			get
			{
				return Environment.GetIs64BitOperatingSystem();
			}
		}

		/// <summary>Gets the number of bytes in the operating system's memory page.</summary>
		/// <returns>The number of bytes in the system memory page.</returns>
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x0005C5D8 File Offset: 0x0005A7D8
		public static int SystemPageSize
		{
			get
			{
				return Environment.GetPageSize();
			}
		}

		/// <summary>Determines whether the current process is a 64-bit process.</summary>
		/// <returns>true if the process is 64-bit; otherwise, false.</returns>
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x0005C5DF File Offset: 0x0005A7DF
		public static bool Is64BitProcess
		{
			get
			{
				return IntPtr.Size == 8;
			}
		}

		/// <summary>Gets the number of processors on the current machine.</summary>
		/// <returns>The 32-bit signed integer that specifies the number of processors on the current machine. There is no default. If the current machine contains multiple processor groups, this property returns the number of logical processors that are available for use by the common language runtime (CLR).</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="NUMBER_OF_PROCESSORS" />
		/// </PermissionSet>
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060017A4 RID: 6052
		public static extern int ProcessorCount
		{
			[EnvironmentPermission(SecurityAction.Demand, Read = "NUMBER_OF_PROCESSORS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x0005C5E9 File Offset: 0x0005A7E9
		internal static bool IsRunningOnWindows
		{
			get
			{
				return Environment.Platform < PlatformID.Unix;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x0005C5F4 File Offset: 0x0005A7F4
		private static string GacPath
		{
			get
			{
				if (Environment.IsRunningOnWindows)
				{
					return Path.Combine(Path.Combine(new DirectoryInfo(Path.GetDirectoryName(typeof(int).Assembly.Location)).Parent.Parent.FullName, "mono"), "gac");
				}
				return Path.Combine(Path.Combine(Environment.internalGetGacPath(), "mono"), "gac");
			}
		}

		// Token: 0x060017A7 RID: 6055
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string internalGetGacPath();

		// Token: 0x060017A8 RID: 6056
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetLogicalDrivesInternal();

		// Token: 0x060017A9 RID: 6057
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetEnvironmentVariableNames();

		// Token: 0x060017AA RID: 6058
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetMachineConfigPath();

		// Token: 0x060017AB RID: 6059
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string internalGetHome();

		// Token: 0x060017AC RID: 6060
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetPageSize();

		// Token: 0x060017AD RID: 6061
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_bundled_machine_config();

		// Token: 0x060017AE RID: 6062 RVA: 0x0005C663 File Offset: 0x0005A863
		internal static string GetBundledMachineConfig()
		{
			return Environment.get_bundled_machine_config();
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x0005C66C File Offset: 0x0005A86C
		internal static bool IsUnix
		{
			get
			{
				int platform = (int)Environment.Platform;
				return platform == 4 || platform == 128 || platform == 6;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x0005C691 File Offset: 0x0005A891
		internal static bool IsMacOS
		{
			get
			{
				return Environment.Platform == PlatformID.MacOSX;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal static bool IsCLRHosted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x00002194 File Offset: 0x00000394
		internal static void TriggerCodeContractFailure(ContractFailureKind failureKind, string message, string condition, string exceptionAsString)
		{
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0005C69C File Offset: 0x0005A89C
		internal static string GetStackTrace(Exception e, bool needFileInfo)
		{
			StackTrace stackTrace;
			if (e == null)
			{
				stackTrace = new StackTrace(needFileInfo);
			}
			else
			{
				stackTrace = new StackTrace(e, needFileInfo);
			}
			return stackTrace.ToString(global::System.Diagnostics.StackTrace.TraceFormat.Normal);
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x00003B29 File Offset: 0x00001D29
		internal static bool IsWinRTSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000C16 RID: 3094
		private const int mono_corlib_version = 1051100001;

		// Token: 0x04000C17 RID: 3095
		private static string nl;

		// Token: 0x04000C18 RID: 3096
		private static OperatingSystem os;

		/// <summary>Specifies enumerated constants used to retrieve directory paths to system special folders.</summary>
		// Token: 0x020001F8 RID: 504
		[ComVisible(true)]
		public enum SpecialFolder
		{
			/// <summary>The My Documents folder.</summary>
			// Token: 0x04000C1A RID: 3098
			MyDocuments = 5,
			/// <summary>The logical Desktop rather than the physical file system location.</summary>
			// Token: 0x04000C1B RID: 3099
			Desktop = 0,
			/// <summary>The My Computer folder. </summary>
			// Token: 0x04000C1C RID: 3100
			MyComputer = 17,
			/// <summary>The directory that contains the user's program groups.</summary>
			// Token: 0x04000C1D RID: 3101
			Programs = 2,
			/// <summary>The directory that serves as a common repository for documents.</summary>
			// Token: 0x04000C1E RID: 3102
			Personal = 5,
			/// <summary>The directory that serves as a common repository for the user's favorite items.</summary>
			// Token: 0x04000C1F RID: 3103
			Favorites,
			/// <summary>The directory that corresponds to the user's Startup program group.</summary>
			// Token: 0x04000C20 RID: 3104
			Startup,
			/// <summary>The directory that contains the user's most recently used documents.</summary>
			// Token: 0x04000C21 RID: 3105
			Recent,
			/// <summary>The directory that contains the Send To menu items.</summary>
			// Token: 0x04000C22 RID: 3106
			SendTo,
			/// <summary>The directory that contains the Start menu items.</summary>
			// Token: 0x04000C23 RID: 3107
			StartMenu = 11,
			/// <summary>The My Music folder.</summary>
			// Token: 0x04000C24 RID: 3108
			MyMusic = 13,
			/// <summary>The directory used to physically store file objects on the desktop.</summary>
			// Token: 0x04000C25 RID: 3109
			DesktopDirectory = 16,
			/// <summary>The directory that serves as a common repository for document templates.</summary>
			// Token: 0x04000C26 RID: 3110
			Templates = 21,
			/// <summary>The directory that serves as a common repository for application-specific data for the current roaming user.</summary>
			// Token: 0x04000C27 RID: 3111
			ApplicationData = 26,
			/// <summary>The directory that serves as a common repository for application-specific data that is used by the current, non-roaming user.</summary>
			// Token: 0x04000C28 RID: 3112
			LocalApplicationData = 28,
			/// <summary>The directory that serves as a common repository for temporary Internet files.</summary>
			// Token: 0x04000C29 RID: 3113
			InternetCache = 32,
			/// <summary>The directory that serves as a common repository for Internet cookies.</summary>
			// Token: 0x04000C2A RID: 3114
			Cookies,
			/// <summary>The directory that serves as a common repository for Internet history items.</summary>
			// Token: 0x04000C2B RID: 3115
			History,
			/// <summary>The directory that serves as a common repository for application-specific data that is used by all users.</summary>
			// Token: 0x04000C2C RID: 3116
			CommonApplicationData,
			/// <summary>The System directory.</summary>
			// Token: 0x04000C2D RID: 3117
			System = 37,
			/// <summary>The program files directory.On a non-x86 system, passing <see cref="F:System.Environment.SpecialFolder.ProgramFiles" /> to the <see cref="M:System.Environment.GetFolderPath(System.Environment.SpecialFolder)" /> method returns the path for non-x86 programs. To get the x86 program files directory on a non-x86 system, use the <see cref="F:System.Environment.SpecialFolder.ProgramFilesX86" /> member. </summary>
			// Token: 0x04000C2E RID: 3118
			ProgramFiles,
			/// <summary>The My Pictures folder.</summary>
			// Token: 0x04000C2F RID: 3119
			MyPictures,
			/// <summary>The directory for components that are shared across applications.To get the x86 common program files directory on a non-x86 system, use the <see cref="F:System.Environment.SpecialFolder.ProgramFilesX86" /> member. </summary>
			// Token: 0x04000C30 RID: 3120
			CommonProgramFiles = 43,
			/// <summary>The file system directory that serves as a repository for videos that belong to a user.  Added in the .NET Framework 4. </summary>
			// Token: 0x04000C31 RID: 3121
			MyVideos = 14,
			/// <summary>A file system directory that contains the link objects that may exist in the My Network Places virtual folder. Added in the .NET Framework 4. </summary>
			// Token: 0x04000C32 RID: 3122
			NetworkShortcuts = 19,
			/// <summary>A virtual folder that contains fonts. Added in the .NET Framework 4. </summary>
			// Token: 0x04000C33 RID: 3123
			Fonts,
			/// <summary>The file system directory that contains the programs and folders that appear on the Start menu for all users. This special folder is valid only for Windows NT systems. Added in the .NET Framework 4. </summary>
			// Token: 0x04000C34 RID: 3124
			CommonStartMenu = 22,
			/// <summary>A folder for components that are shared across applications. This special folder is valid only for Windows NT, Windows 2000, and Windows XP systems. Added in the .NET Framework 4. </summary>
			// Token: 0x04000C35 RID: 3125
			CommonPrograms,
			/// <summary>The file system directory that contains the programs that appear in the Startup folder for all users. This special folder is valid only for Windows NT systems. Added in the .NET Framework 4.</summary>
			// Token: 0x04000C36 RID: 3126
			CommonStartup,
			/// <summary>The file system directory that contains files and folders that appear on the desktop for all users. This special folder is valid only for Windows NT systems. Added in the .NET Framework 4. </summary>
			// Token: 0x04000C37 RID: 3127
			CommonDesktopDirectory,
			/// <summary>The file system directory that contains the link objects that can exist in the Printers virtual folder. Added in the .NET Framework 4. </summary>
			// Token: 0x04000C38 RID: 3128
			PrinterShortcuts = 27,
			/// <summary>The Windows directory or SYSROOT. This corresponds to the %windir% or %SYSTEMROOT% environment variables. Added in the .NET Framework 4. </summary>
			// Token: 0x04000C39 RID: 3129
			Windows = 36,
			/// <summary>The user's profile folder. Applications should not create files or folders at this level; they should put their data under the locations referred to by <see cref="F:System.Environment.SpecialFolder.ApplicationData" />. Added in the .NET Framework 4. </summary>
			// Token: 0x04000C3A RID: 3130
			UserProfile = 40,
			/// <summary>The Windows System folder. Added in the .NET Framework 4. </summary>
			// Token: 0x04000C3B RID: 3131
			SystemX86,
			/// <summary>The Program Files folder. Added in the .NET Framework 4. On an x86 system, passing the <see cref="F:System.Environment.SpecialFolder.ProgramFilesX86" /> member to the <see cref="M:System.Environment.GetFolderPath(System.Environment.SpecialFolder)" /> method returns <see cref="F:System.String.Empty" />; use the <see cref="F:System.Environment.SpecialFolder.ProgramFiles" /> member instead. You can determine whether Windows is a 32-bit operating system by calling the <see cref="P:System.Environment.Is64BitOperatingSystem" /> property. </summary>
			// Token: 0x04000C3C RID: 3132
			ProgramFilesX86,
			/// <summary>The Program Files folder. Added in the .NET Framework 4. </summary>
			// Token: 0x04000C3D RID: 3133
			CommonProgramFilesX86 = 44,
			/// <summary>The file system directory that contains the templates that are available to all users. This special folder is valid only for Windows NT systems.  Added in the .NET Framework 4. </summary>
			// Token: 0x04000C3E RID: 3134
			CommonTemplates,
			/// <summary>The file system directory that contains documents that are common to all users. This special folder is valid for Windows NT systems, Windows 95, and Windows 98 systems with Shfolder.dll installed. Added in the .NET Framework 4. </summary>
			// Token: 0x04000C3F RID: 3135
			CommonDocuments,
			/// <summary>The file system directory that contains administrative tools for all users of the computer. Added in the .NET Framework 4. </summary>
			// Token: 0x04000C40 RID: 3136
			CommonAdminTools,
			/// <summary>The file system directory that is used to store administrative tools for an individual user. The Microsoft Management Console (MMC) will save customized consoles to this directory, and it will roam with the user. Added in the .NET Framework 4.</summary>
			// Token: 0x04000C41 RID: 3137
			AdminTools,
			/// <summary>The file system directory that serves as a repository for music files common to all users. Added in the .NET Framework 4. </summary>
			// Token: 0x04000C42 RID: 3138
			CommonMusic = 53,
			/// <summary>The file system directory that serves as a repository for image files common to all users. Added in the .NET Framework 4. </summary>
			// Token: 0x04000C43 RID: 3139
			CommonPictures,
			/// <summary>The file system directory that serves as a repository for video files common to all users. Added in the .NET Framework 4. </summary>
			// Token: 0x04000C44 RID: 3140
			CommonVideos,
			/// <summary>The file system directory that contains resource data. Added in the .NET Framework 4. </summary>
			// Token: 0x04000C45 RID: 3141
			Resources,
			/// <summary>The file system directory that contains localized resource data. Added in the .NET Framework 4. </summary>
			// Token: 0x04000C46 RID: 3142
			LocalizedResources,
			/// <summary>This value is recognized in Windows Vista for backward compatibility, but the special folder itself is no longer used. Added in the .NET Framework 4. </summary>
			// Token: 0x04000C47 RID: 3143
			CommonOemLinks,
			/// <summary>The file system directory that acts as a staging area for files waiting to be written to a CD. Added in the .NET Framework 4. </summary>
			// Token: 0x04000C48 RID: 3144
			CDBurning
		}

		/// <summary>Specifies options to use for getting the path to a special folder. </summary>
		// Token: 0x020001F9 RID: 505
		public enum SpecialFolderOption
		{
			/// <summary>The path to the folder is verified. If the folder exists, the path is returned. If the folder does not exist, an empty string is returned. This is the default behavior. </summary>
			// Token: 0x04000C4A RID: 3146
			None,
			/// <summary>The path to the folder is returned without verifying whether the path exists. If the folder is located on a network, specifying this option can reduce lag time. </summary>
			// Token: 0x04000C4B RID: 3147
			DoNotVerify = 16384,
			/// <summary>The path to the folder is created if it does not already exist.</summary>
			// Token: 0x04000C4C RID: 3148
			Create = 32768
		}
	}
}
