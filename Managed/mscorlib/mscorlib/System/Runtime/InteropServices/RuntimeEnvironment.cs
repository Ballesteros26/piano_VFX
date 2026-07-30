using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a collection of static methods that return information about the common language runtime environment.</summary>
	// Token: 0x020008EC RID: 2284
	[ComVisible(true)]
	public class RuntimeEnvironment
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.RuntimeEnvironment" /> class.</summary>
		// Token: 0x0600558E RID: 21902 RVA: 0x00002111 File Offset: 0x00000311
		[Obsolete("Do not create instances of the RuntimeEnvironment class.  Call the static methods directly on this type instead", true)]
		public RuntimeEnvironment()
		{
		}

		/// <summary>Tests whether the specified assembly is loaded in the global assembly cache.</summary>
		/// <returns>true if the assembly is loaded in the global assembly cache; otherwise, false.</returns>
		/// <param name="a">The assembly to test. </param>
		// Token: 0x0600558F RID: 21903 RVA: 0x00128F15 File Offset: 0x00127115
		public static bool FromGlobalAccessCache(Assembly a)
		{
			return a.GlobalAssemblyCache;
		}

		/// <summary>Gets the version number of the common language runtime that is running the current process.</summary>
		/// <returns>A string containing the version number of the common language runtime.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06005590 RID: 21904 RVA: 0x00128F1D File Offset: 0x0012711D
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string GetSystemVersion()
		{
			return Assembly.GetExecutingAssembly().ImageRuntimeVersion;
		}

		/// <summary>Returns the directory where the common language runtime is installed.</summary>
		/// <returns>A string that contains the path to the directory where the common language runtime is installed.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06005591 RID: 21905 RVA: 0x00128F29 File Offset: 0x00127129
		[SecuritySafeCritical]
		public static string GetRuntimeDirectory()
		{
			if (Environment.GetEnvironmentVariable("CSC_SDK_PATH_DISABLED") != null)
			{
				return null;
			}
			return RuntimeEnvironment.GetRuntimeDirectoryImpl();
		}

		// Token: 0x06005592 RID: 21906 RVA: 0x00128F3E File Offset: 0x0012713E
		private static string GetRuntimeDirectoryImpl()
		{
			return Path.GetDirectoryName(typeof(object).Assembly.Location);
		}

		/// <summary>Gets the path to the system configuration file.</summary>
		/// <returns>The path to the system configuration file.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06005593 RID: 21907 RVA: 0x00128F59 File Offset: 0x00127159
		public static string SystemConfigurationFile
		{
			[SecuritySafeCritical]
			get
			{
				return Environment.GetMachineConfigPath();
			}
		}

		// Token: 0x06005594 RID: 21908 RVA: 0x00014B5A File Offset: 0x00012D5A
		private static IntPtr GetRuntimeInterfaceImpl(Guid clsid, Guid riid)
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns the specified interface on the specified class.</summary>
		/// <returns>An unmanaged pointer to the requested interface.</returns>
		/// <param name="clsid">The identifier for the desired class.</param>
		/// <param name="riid">The identifier for the desired interface.</param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">IUnknown::QueryInterface failure.</exception>
		// Token: 0x06005595 RID: 21909 RVA: 0x00128F60 File Offset: 0x00127160
		[ComVisible(false)]
		[SecurityCritical]
		public static IntPtr GetRuntimeInterfaceAsIntPtr(Guid clsid, Guid riid)
		{
			return RuntimeEnvironment.GetRuntimeInterfaceImpl(clsid, riid);
		}

		/// <summary>Returns an instance of a type that represents a COM object by a pointer to its IUnknown interface.</summary>
		/// <returns>An object that represents the specified unmanaged COM object.</returns>
		/// <param name="clsid">The identifier for the desired class.</param>
		/// <param name="riid">The identifier for the desired interface.</param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">IUnknown::QueryInterface failure.</exception>
		// Token: 0x06005596 RID: 21910 RVA: 0x00128F6C File Offset: 0x0012716C
		[ComVisible(false)]
		[SecurityCritical]
		public static object GetRuntimeInterfaceAsObject(Guid clsid, Guid riid)
		{
			IntPtr intPtr = IntPtr.Zero;
			object objectForIUnknown;
			try
			{
				intPtr = RuntimeEnvironment.GetRuntimeInterfaceImpl(clsid, riid);
				objectForIUnknown = Marshal.GetObjectForIUnknown(intPtr);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.Release(intPtr);
				}
			}
			return objectForIUnknown;
		}
	}
}
