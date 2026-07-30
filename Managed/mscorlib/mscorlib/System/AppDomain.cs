using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Principal;
using System.Threading;
using Mono.Security;

namespace System
{
	/// <summary>Represents an application domain, which is an isolated environment where applications execute. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001EF RID: 495
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_AppDomain))]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AppDomain : MarshalByRefObject, _AppDomain, IEvidenceFactory
	{
		// Token: 0x060016A8 RID: 5800 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal static bool IsAppXModel()
		{
			return false;
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal static bool IsAppXDesignMode()
		{
			return false;
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00002194 File Offset: 0x00000394
		internal static void CheckReflectionOnlyLoadSupported()
		{
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00002194 File Offset: 0x00000394
		internal static void CheckLoadFromSupported()
		{
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00035C1F File Offset: 0x00033E1F
		private AppDomain()
		{
		}

		// Token: 0x060016AD RID: 5805
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AppDomainSetup getSetup();

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x0005A15E File Offset: 0x0005835E
		private AppDomainSetup SetupInformationNoCopy
		{
			get
			{
				return this.getSetup();
			}
		}

		/// <summary>Gets the application domain configuration information for this instance.</summary>
		/// <returns>The application domain initialization information.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x0005A166 File Offset: 0x00058366
		public AppDomainSetup SetupInformation
		{
			get
			{
				return new AppDomainSetup(this.getSetup());
			}
		}

		/// <summary>Gets information describing permissions granted to an application and whether the application has a trust level that allows it to run.</summary>
		/// <returns>An object that encapsulates permission and trust information for the application in the application domain. </returns>
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public ApplicationTrust ApplicationTrust
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the base directory that the assembly resolver uses to probe for assemblies.</summary>
		/// <returns>The base directory that the assembly resolver uses to probe for assemblies.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x0005A174 File Offset: 0x00058374
		public string BaseDirectory
		{
			get
			{
				string applicationBase = this.SetupInformationNoCopy.ApplicationBase;
				if (SecurityManager.SecurityEnabled && applicationBase != null && applicationBase.Length > 0)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, applicationBase).Demand();
				}
				return applicationBase;
			}
		}

		/// <summary>Gets the path under the base directory where the assembly resolver should probe for private assemblies.</summary>
		/// <returns>The path under the base directory where the assembly resolver should probe for private assemblies.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x0005A1B0 File Offset: 0x000583B0
		public string RelativeSearchPath
		{
			get
			{
				string privateBinPath = this.SetupInformationNoCopy.PrivateBinPath;
				if (SecurityManager.SecurityEnabled && privateBinPath != null && privateBinPath.Length > 0)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, privateBinPath).Demand();
				}
				return privateBinPath;
			}
		}

		/// <summary>Gets the directory that the assembly resolver uses to probe for dynamically created assemblies.</summary>
		/// <returns>The directory that the assembly resolver uses to probe for dynamically created assemblies.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x0005A1EC File Offset: 0x000583EC
		public string DynamicDirectory
		{
			[SecuritySafeCritical]
			get
			{
				AppDomainSetup setupInformationNoCopy = this.SetupInformationNoCopy;
				if (setupInformationNoCopy.DynamicBase == null)
				{
					return null;
				}
				string text = Path.Combine(setupInformationNoCopy.DynamicBase, setupInformationNoCopy.ApplicationName);
				if (SecurityManager.SecurityEnabled && text != null && text.Length > 0)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
				}
				return text;
			}
		}

		/// <summary>Gets an indication whether the application domain is configured to shadow copy files.</summary>
		/// <returns>true if the application domain is configured to shadow copy files; otherwise, false.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x0005A23C File Offset: 0x0005843C
		public bool ShadowCopyFiles
		{
			get
			{
				return this.SetupInformationNoCopy.ShadowCopyFiles == "true";
			}
		}

		// Token: 0x060016B5 RID: 5813
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string getFriendlyName();

		/// <summary>Gets the friendly name of this application domain.</summary>
		/// <returns>The friendly name of this application domain.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x0005A253 File Offset: 0x00058453
		public string FriendlyName
		{
			[SecuritySafeCritical]
			get
			{
				return this.getFriendlyName();
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Policy.Evidence" /> associated with this application domain.</summary>
		/// <returns>The evidence associated with this application domain.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x0005A25C File Offset: 0x0005845C
		public Evidence Evidence
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true)]
			get
			{
				if (this._evidence == null)
				{
					lock (this)
					{
						Assembly entryAssembly = Assembly.GetEntryAssembly();
						if (entryAssembly == null)
						{
							if (this == AppDomain.DefaultDomain)
							{
								return new Evidence();
							}
							this._evidence = AppDomain.DefaultDomain.Evidence;
						}
						else
						{
							this._evidence = Evidence.GetDefaultHostEvidence(entryAssembly);
						}
					}
				}
				return new Evidence(this._evidence);
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x0005A2E4 File Offset: 0x000584E4
		internal IPrincipal DefaultPrincipal
		{
			get
			{
				if (AppDomain._principal == null)
				{
					PrincipalPolicy principalPolicy = this._principalPolicy;
					if (principalPolicy != PrincipalPolicy.UnauthenticatedPrincipal)
					{
						if (principalPolicy == PrincipalPolicy.WindowsPrincipal)
						{
							AppDomain._principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
						}
					}
					else
					{
						AppDomain._principal = new GenericPrincipal(new GenericIdentity(string.Empty, string.Empty), null);
					}
				}
				return AppDomain._principal;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x0005A338 File Offset: 0x00058538
		internal PermissionSet GrantedPermissionSet
		{
			get
			{
				return this._granted;
			}
		}

		/// <summary>Gets the permission set of a sandboxed application domain.</summary>
		/// <returns>The permission set of the sandboxed application domain.</returns>
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x0005A340 File Offset: 0x00058540
		public PermissionSet PermissionSet
		{
			get
			{
				PermissionSet permissionSet;
				if ((permissionSet = this._granted) == null)
				{
					permissionSet = (this._granted = new PermissionSet(PermissionState.Unrestricted));
				}
				return permissionSet;
			}
		}

		// Token: 0x060016BB RID: 5819
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AppDomain getCurDomain();

		/// <summary>Gets the current application domain for the current <see cref="T:System.Threading.Thread" />.</summary>
		/// <returns>The current application domain.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x0005A366 File Offset: 0x00058566
		public static AppDomain CurrentDomain
		{
			get
			{
				return AppDomain.getCurDomain();
			}
		}

		// Token: 0x060016BD RID: 5821
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AppDomain getRootDomain();

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x0005A370 File Offset: 0x00058570
		internal static AppDomain DefaultDomain
		{
			get
			{
				if (AppDomain.default_domain == null)
				{
					AppDomain rootDomain = AppDomain.getRootDomain();
					if (rootDomain == AppDomain.CurrentDomain)
					{
						AppDomain.default_domain = rootDomain;
					}
					else
					{
						AppDomain.default_domain = (AppDomain)RemotingServices.GetDomainProxy(rootDomain);
					}
				}
				return AppDomain.default_domain;
			}
		}

		/// <summary>Appends the specified directory name to the private path list.</summary>
		/// <param name="path">The name of the directory to be appended to the private path. </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x060016BF RID: 5823 RVA: 0x0005A3B0 File Offset: 0x000585B0
		[Obsolete("AppDomain.AppendPrivatePath has been deprecated. Please investigate the use of AppDomainSetup.PrivateBinPath instead.")]
		[SecurityCritical]
		[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public void AppendPrivatePath(string path)
		{
			if (path == null || path.Length == 0)
			{
				return;
			}
			AppDomainSetup setupInformationNoCopy = this.SetupInformationNoCopy;
			string text = setupInformationNoCopy.PrivateBinPath;
			if (text == null || text.Length == 0)
			{
				setupInformationNoCopy.PrivateBinPath = path;
				return;
			}
			text = text.Trim();
			if (text[text.Length - 1] != Path.PathSeparator)
			{
				text += Path.PathSeparator.ToString();
			}
			setupInformationNoCopy.PrivateBinPath = text + path;
		}

		/// <summary>Resets the path that specifies the location of private assemblies to the empty string ("").</summary>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x060016C0 RID: 5824 RVA: 0x0005A427 File Offset: 0x00058627
		[Obsolete("AppDomain.ClearPrivatePath has been deprecated. Please investigate the use of AppDomainSetup.PrivateBinPath instead.")]
		[SecurityCritical]
		[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public void ClearPrivatePath()
		{
			this.SetupInformationNoCopy.PrivateBinPath = string.Empty;
		}

		/// <summary>Resets the list of directories containing shadow copied assemblies to the empty string ("").</summary>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x060016C1 RID: 5825 RVA: 0x0005A439 File Offset: 0x00058639
		[Obsolete("Use AppDomainSetup.ShadowCopyDirectories")]
		[SecurityCritical]
		[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public void ClearShadowCopyPath()
		{
			this.SetupInformationNoCopy.ShadowCopyDirectories = string.Empty;
		}

		/// <summary>Creates a new instance of a specified COM type. Parameters specify the name of a file that contains an assembly containing the type and the name of the type.</summary>
		/// <returns>An object that is a wrapper for the new instance specified by <paramref name="typeName" />. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyName">The name of a file containing an assembly that defines the requested type. </param>
		/// <param name="typeName">The name of the requested type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.TypeLoadException">The type cannot be loaded. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.MissingMethodException">No public parameterless constructor was found. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> is not found. </exception>
		/// <exception cref="T:System.MemberAccessException">
		///   <paramref name="typeName" /> is an abstract class. -or-This member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyName" /> is an empty string (""). </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NullReferenceException">The COM object that is being referred to is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060016C2 RID: 5826 RVA: 0x0005A44B File Offset: 0x0005864B
		public ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName)
		{
			return Activator.CreateComInstanceFrom(assemblyName, typeName);
		}

		/// <summary>Creates a new instance of a specified COM type. Parameters specify the name of a file that contains an assembly containing the type and the name of the type.</summary>
		/// <returns>An object that is a wrapper for the new instance specified by <paramref name="typeName" />. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyFile">The name of a file containing an assembly that defines the requested type. </param>
		/// <param name="typeName">The name of the requested type. </param>
		/// <param name="hashValue">Represents the value of the computed hash code. </param>
		/// <param name="hashAlgorithm">Represents the hash algorithm used by the assembly manifest. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.TypeLoadException">The type cannot be loaded. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.MissingMethodException">No public parameterless constructor was found. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found. </exception>
		/// <exception cref="T:System.MemberAccessException">
		///   <paramref name="typeName" /> is an abstract class. -or-This member was invoked with a late-binding mechanism. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyFile" /> is the empty string (""). </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NullReferenceException">The COM object that is being referred to is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060016C3 RID: 5827 RVA: 0x0005A454 File Offset: 0x00058654
		public ObjectHandle CreateComInstanceFrom(string assemblyFile, string typeName, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			return Activator.CreateComInstanceFrom(assemblyFile, typeName, hashValue, hashAlgorithm);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0005A460 File Offset: 0x00058660
		internal ObjectHandle InternalCreateInstanceWithNoSecurity(string assemblyName, string typeName)
		{
			return this.CreateInstance(assemblyName, typeName);
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x0005A46C File Offset: 0x0005866C
		internal ObjectHandle InternalCreateInstanceWithNoSecurity(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			return this.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x0005A48E File Offset: 0x0005868E
		internal ObjectHandle InternalCreateInstanceFromWithNoSecurity(string assemblyName, string typeName)
		{
			return this.CreateInstanceFrom(assemblyName, typeName);
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0005A498 File Offset: 0x00058698
		internal ObjectHandle InternalCreateInstanceFromWithNoSecurity(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			return this.CreateInstanceFrom(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly.</summary>
		/// <returns>An object that is a wrapper for the new instance specified by <paramref name="typeName" />. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060016C8 RID: 5832 RVA: 0x0005A4BA File Offset: 0x000586BA
		public ObjectHandle CreateInstance(string assemblyName, string typeName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			return Activator.CreateInstance(assemblyName, typeName);
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly. A parameter specifies an array of activation attributes.</summary>
		/// <returns>An object that is a wrapper for the new instance specified by <paramref name="typeName" />. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060016C9 RID: 5833 RVA: 0x0005A4D1 File Offset: 0x000586D1
		public ObjectHandle CreateInstance(string assemblyName, string typeName, object[] activationAttributes)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			return Activator.CreateInstance(assemblyName, typeName, activationAttributes);
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly. Parameters specify a binder, binding flags, constructor arguments, culture-specific information used to interpret arguments, activation attributes, and authorization to create the type.</summary>
		/// <returns>An object that is a wrapper for the new instance specified by <paramref name="typeName" />. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects using reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <param name="securityAttributes">Information used to authorize creation of <paramref name="typeName" />. </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />.-or-<paramref name="securityAttributes" /> is not null. When legacy CAS policy is not enabled, <paramref name="securityAttributes" /> should be null.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060016CA RID: 5834 RVA: 0x0005A4EC File Offset: 0x000586EC
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
		}

		/// <summary>Creates a new instance of the specified type. Parameters specify the assembly where the type is defined, and the name of the type.</summary>
		/// <returns>An instance of the object specified by <paramref name="typeName" />.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060016CB RID: 5835 RVA: 0x0005A51C File Offset: 0x0005871C
		public object CreateInstanceAndUnwrap(string assemblyName, string typeName)
		{
			ObjectHandle objectHandle = this.CreateInstance(assemblyName, typeName);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		/// <summary>Creates a new instance of the specified type. Parameters specify the assembly where the type is defined, the name of the type, and an array of activation attributes.</summary>
		/// <returns>An instance of the object specified by <paramref name="typeName" />.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060016CC RID: 5836 RVA: 0x0005A540 File Offset: 0x00058740
		public object CreateInstanceAndUnwrap(string assemblyName, string typeName, object[] activationAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstance(assemblyName, typeName, activationAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		/// <summary>Creates a new instance of the specified type. Parameters specify the name of the type, and how it is found and created.</summary>
		/// <returns>An instance of the object specified by <paramref name="typeName" />.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects using reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">A culture-specific object used to govern the coercion of types. If <paramref name="culture" /> is null, the CultureInfo for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <param name="securityAttributes">Information used to authorize creation of <paramref name="typeName" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060016CD RID: 5837 RVA: 0x0005A564 File Offset: 0x00058764
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public object CreateInstanceAndUnwrap(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly. Parameters specify a binder, binding flags, constructor arguments, culture-specific information used to interpret arguments, and optional activation attributes.</summary>
		/// <returns>An object that is a wrapper for the new instance specified by <paramref name="typeName" />. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects using reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object. </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-<paramref name="assemblyName" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		// Token: 0x060016CE RID: 5838 RVA: 0x0005A594 File Offset: 0x00058794
		public ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, null);
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly, specifying whether the case of the type name is ignored; the binding attributes and the binder that are used to select the type to be created; the arguments of the constructor; the culture; and the activation attributes.</summary>
		/// <returns>An instance of the object specified by <paramref name="typeName" />.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects using reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">A culture-specific object used to govern the coercion of types. If <paramref name="culture" /> is null, the CultureInfo for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> or <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching constructor was found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typename" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to call this constructor. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-<paramref name="assemblyName" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		// Token: 0x060016CF RID: 5839 RVA: 0x0005A5C4 File Offset: 0x000587C4
		public object CreateInstanceAndUnwrap(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly file.</summary>
		/// <returns>An object that is a wrapper for the new instance, or null if <paramref name="typeName" /> is not found. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyFile">The name, including the path, of a file that contains an assembly that defines the requested type. The assembly is loaded using the <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" />  method.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null.-or- <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> was not found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> was not found in <paramref name="assemblyFile" />. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have sufficient permission to call this constructor. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-<paramref name="assemblyFile" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		// Token: 0x060016D0 RID: 5840 RVA: 0x0005A5F0 File Offset: 0x000587F0
		public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			return Activator.CreateInstanceFrom(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, null);
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly file, specifying whether the case of the type name is ignored; the binding attributes and the binder that are used to select the type to be created; the arguments of the constructor; the culture; and the activation attributes.</summary>
		/// <returns>The requested object, or null if <paramref name="typeName" /> is not found.</returns>
		/// <param name="assemblyFile">The file name and path of the assembly that defines the requested type. </param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null.-or- <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have sufficient permission to call this constructor. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-<paramref name="assemblyName" /> was compiled with a later version of the common language runtime that the version that is currently loaded.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		// Token: 0x060016D1 RID: 5841 RVA: 0x0005A620 File Offset: 0x00058820
		public object CreateInstanceFromAndUnwrap(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstanceFrom(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly file.</summary>
		/// <returns>An object that is a wrapper for the new instance, or null if <paramref name="typeName" /> is not found. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyFile">The name, including the path, of a file that contains an assembly that defines the requested type. The assembly is loaded using the <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" />  method.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null.-or- <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> was not found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> was not found in <paramref name="assemblyFile" />. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.MissingMethodException">No parameterless public constructor was found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have sufficient permission to call this constructor. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060016D2 RID: 5842 RVA: 0x0005A64C File Offset: 0x0005884C
		public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			return Activator.CreateInstanceFrom(assemblyFile, typeName);
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly file.</summary>
		/// <returns>An object that is a wrapper for the new instance, or null if <paramref name="typeName" /> is not found. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyFile">The name, including the path, of a file that contains an assembly that defines the requested type. The assembly is loaded using the <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" />  method. </param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> was not found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> was not found in <paramref name="assemblyFile" />. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have sufficient permission to call this constructor. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060016D3 RID: 5843 RVA: 0x0005A663 File Offset: 0x00058863
		public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, object[] activationAttributes)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			return Activator.CreateInstanceFrom(assemblyFile, typeName, activationAttributes);
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly file.</summary>
		/// <returns>An object that is a wrapper for the new instance, or null if <paramref name="typeName" /> is not found. The return value needs to be unwrapped to access the real object.</returns>
		/// <param name="assemblyFile">The name, including the path, of a file that contains an assembly that defines the requested type. The assembly is loaded using the <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" />  method.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <param name="securityAttributes">Information used to authorize creation of <paramref name="typeName" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null.-or- <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />.-or-<paramref name="securityAttributes" /> is not null. When legacy CAS policy is not enabled, <paramref name="securityAttributes" /> should be null.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> was not found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> was not found in <paramref name="assemblyFile" />. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have sufficient permission to call this constructor. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060016D4 RID: 5844 RVA: 0x0005A67C File Offset: 0x0005887C
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			return Activator.CreateInstanceFrom(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly file.</summary>
		/// <returns>The requested object, or null if <paramref name="typeName" /> is not found.</returns>
		/// <param name="assemblyName">The file name and path of the assembly that defines the requested type. </param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null.-or- <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.MissingMethodException">No parameterless public constructor was found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have sufficient permission to call this constructor. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060016D5 RID: 5845 RVA: 0x0005A6AC File Offset: 0x000588AC
		public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName)
		{
			ObjectHandle objectHandle = this.CreateInstanceFrom(assemblyName, typeName);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly file.</summary>
		/// <returns>The requested object, or null if <paramref name="typeName" /> is not found.</returns>
		/// <param name="assemblyName">The file name and path of the assembly that defines the requested type. </param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly (see the <see cref="P:System.Type.FullName" /> property). </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null.-or- <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.MissingMethodException">No parameterless public constructor was found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have sufficient permission to call this constructor. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060016D6 RID: 5846 RVA: 0x0005A6D0 File Offset: 0x000588D0
		public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName, object[] activationAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstanceFrom(assemblyName, typeName, activationAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		/// <summary>Creates a new instance of the specified type defined in the specified assembly file.</summary>
		/// <returns>The requested object, or null if <paramref name="typeName" /> is not found.</returns>
		/// <param name="assemblyName">The file name and path of the assembly that defines the requested type. </param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property. </param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not. </param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null. </param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.  </param>
		/// <param name="securityAttributes">Information used to authorize creation of <paramref name="typeName" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null.-or- <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The caller cannot provide activation attributes for an object that does not inherit from <see cref="T:System.MarshalByRefObject" />. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyName" /> was not found. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> was not found in <paramref name="assemblyName" />. </exception>
		/// <exception cref="T:System.MissingMethodException">No matching public constructor was found. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have sufficient permission to call this constructor. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060016D7 RID: 5847 RVA: 0x0005A6F4 File Offset: 0x000588F4
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstanceFrom(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes, securityAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		/// <summary>Defines a dynamic assembly with the specified name and access mode.</summary>
		/// <returns>A dynamic assembly with the specified name and access mode.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The access mode for the dynamic assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> begins with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060016D8 RID: 5848 RVA: 0x0005A724 File Offset: 0x00058924
		[SecuritySafeCritical]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
		{
			return this.DefineDynamicAssembly(name, access, null, null, null, null, null, false);
		}

		/// <summary>Defines a dynamic assembly using the specified name, access mode, and evidence.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> begins with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060016D9 RID: 5849 RVA: 0x0005A740 File Offset: 0x00058940
		[SecuritySafeCritical]
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence)
		{
			return this.DefineDynamicAssembly(name, access, null, evidence, null, null, null, false);
		}

		/// <summary>Defines a dynamic assembly using the specified name, access mode, and storage directory.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="dir">The name of the directory where the assembly will be saved. If <paramref name="dir" /> is null, the directory defaults to the current directory. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> begins with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060016DA RID: 5850 RVA: 0x0005A75C File Offset: 0x0005895C
		[SecuritySafeCritical]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir)
		{
			return this.DefineDynamicAssembly(name, access, dir, null, null, null, null, false);
		}

		/// <summary>Defines a dynamic assembly using the specified name, access mode, storage directory, and evidence.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="dir">The name of the directory where the assembly will be saved. If <paramref name="dir" /> is null, the directory defaults to the current directory. </param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> begins with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060016DB RID: 5851 RVA: 0x0005A778 File Offset: 0x00058978
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		[SecuritySafeCritical]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence)
		{
			return this.DefineDynamicAssembly(name, access, dir, evidence, null, null, null, false);
		}

		/// <summary>Defines a dynamic assembly using the specified name, access mode, and permission requests.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="requiredPermissions">The required permissions request. </param>
		/// <param name="optionalPermissions">The optional permissions request. </param>
		/// <param name="refusedPermissions">The refused permissions request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> begins with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060016DC RID: 5852 RVA: 0x0005A794 File Offset: 0x00058994
		[SecuritySafeCritical]
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
		{
			return this.DefineDynamicAssembly(name, access, null, null, requiredPermissions, optionalPermissions, refusedPermissions, false);
		}

		/// <summary>Defines a dynamic assembly using the specified name, access mode, evidence, and permission requests.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution. </param>
		/// <param name="requiredPermissions">The required permissions request. </param>
		/// <param name="optionalPermissions">The optional permissions request. </param>
		/// <param name="refusedPermissions">The refused permissions request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> begins with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060016DD RID: 5853 RVA: 0x0005A7B4 File Offset: 0x000589B4
		[SecuritySafeCritical]
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
		{
			return this.DefineDynamicAssembly(name, access, null, evidence, requiredPermissions, optionalPermissions, refusedPermissions, false);
		}

		/// <summary>Defines a dynamic assembly using the specified name, access mode, storage directory, and permission requests.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="dir">The name of the directory where the assembly will be saved. If <paramref name="dir" /> is null, the directory defaults to the current directory. </param>
		/// <param name="requiredPermissions">The required permissions request. </param>
		/// <param name="optionalPermissions">The optional permissions request. </param>
		/// <param name="refusedPermissions">The refused permissions request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> begins with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060016DE RID: 5854 RVA: 0x0005A7D4 File Offset: 0x000589D4
		[SecuritySafeCritical]
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
		{
			return this.DefineDynamicAssembly(name, access, dir, null, requiredPermissions, optionalPermissions, refusedPermissions, false);
		}

		/// <summary>Defines a dynamic assembly using the specified name, access mode, storage directory, evidence, and permission requests.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="dir">The name of the directory where the assembly will be saved. If <paramref name="dir" /> is null, the directory defaults to the current directory. </param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution. </param>
		/// <param name="requiredPermissions">The required permissions request. </param>
		/// <param name="optionalPermissions">The optional permissions request. </param>
		/// <param name="refusedPermissions">The refused permissions request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> begins with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060016DF RID: 5855 RVA: 0x0005A7F4 File Offset: 0x000589F4
		[SecuritySafeCritical]
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions)
		{
			return this.DefineDynamicAssembly(name, access, dir, evidence, requiredPermissions, optionalPermissions, refusedPermissions, false);
		}

		/// <summary>Defines a dynamic assembly using the specified name, access mode, storage directory, evidence, permission requests, and synchronization option.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="dir">The name of the directory where the dynamic assembly will be saved. If <paramref name="dir" /> is null, the directory defaults to the current directory. </param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution. </param>
		/// <param name="requiredPermissions">The required permissions request. </param>
		/// <param name="optionalPermissions">The optional permissions request. </param>
		/// <param name="refusedPermissions">The refused permissions request. </param>
		/// <param name="isSynchronized">true to synchronize the creation of modules, types, and members in the dynamic assembly; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> begins with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060016E0 RID: 5856 RVA: 0x0005A813 File Offset: 0x00058A13
		[SecuritySafeCritical]
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, bool isSynchronized)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			AppDomain.ValidateAssemblyName(name.Name);
			AssemblyBuilder assemblyBuilder = new AssemblyBuilder(name, dir, access, false);
			assemblyBuilder.AddPermissionRequests(requiredPermissions, optionalPermissions, refusedPermissions);
			return assemblyBuilder;
		}

		/// <summary>Defines a dynamic assembly with the specified name, access mode, storage directory, evidence, permission requests, synchronization option, and custom attributes.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="dir">The name of the directory where the dynamic assembly will be saved. If <paramref name="dir" /> is null, the current directory is used. </param>
		/// <param name="evidence">The evidence that is supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution. </param>
		/// <param name="requiredPermissions">The required permissions request. </param>
		/// <param name="optionalPermissions">The optional permissions request. </param>
		/// <param name="refusedPermissions">The refused permissions request. </param>
		/// <param name="isSynchronized">true to synchronize the creation of modules, types, and members in the dynamic assembly; otherwise, false. </param>
		/// <param name="assemblyAttributes">An enumerable list of attributes to be applied to the assembly, or null if there are no attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> starts with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		// Token: 0x060016E1 RID: 5857 RVA: 0x0005A844 File Offset: 0x00058A44
		[Obsolete("Declarative security for assembly level is no longer enforced")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, bool isSynchronized, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
		{
			AssemblyBuilder assemblyBuilder = this.DefineDynamicAssembly(name, access, dir, evidence, requiredPermissions, optionalPermissions, refusedPermissions, isSynchronized);
			if (assemblyAttributes != null)
			{
				foreach (CustomAttributeBuilder customAttributeBuilder in assemblyAttributes)
				{
					assemblyBuilder.SetCustomAttribute(customAttributeBuilder);
				}
			}
			return assemblyBuilder;
		}

		/// <summary>Defines a dynamic assembly with the specified name, access mode, and custom attributes.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The access mode for the dynamic assembly. </param>
		/// <param name="assemblyAttributes">An enumerable list of attributes to be applied to the assembly, or null if there are no attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> starts with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		// Token: 0x060016E2 RID: 5858 RVA: 0x0005A8A8 File Offset: 0x00058AA8
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
		{
			return this.DefineDynamicAssembly(name, access, null, null, null, null, null, false, assemblyAttributes);
		}

		/// <summary>Defines a dynamic assembly using the specified name, access mode, storage directory, and synchronization option.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed. </param>
		/// <param name="dir">The name of the directory where the dynamic assembly will be saved. If <paramref name="dir" /> is null, the current directory is used. </param>
		/// <param name="isSynchronized">true to synchronize the creation of modules, types, and members in the dynamic assembly; otherwise, false. </param>
		/// <param name="assemblyAttributes">An enumerable list of attributes to be applied to the assembly, or null if there are no attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> starts with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		// Token: 0x060016E3 RID: 5859 RVA: 0x0005A8C4 File Offset: 0x00058AC4
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, bool isSynchronized, IEnumerable<CustomAttributeBuilder> assemblyAttributes)
		{
			return this.DefineDynamicAssembly(name, access, dir, null, null, null, null, isSynchronized, assemblyAttributes);
		}

		/// <summary>Defines a dynamic assembly with the specified name, access mode, and custom attributes, and using the specified source for its security context.</summary>
		/// <returns>A dynamic assembly with the specified name and features.</returns>
		/// <param name="name">The unique identity of the dynamic assembly. </param>
		/// <param name="access">The access mode for the dynamic assembly. </param>
		/// <param name="assemblyAttributes">An enumerable list of attributes to be applied to the assembly, or null if there are no attributes.</param>
		/// <param name="securityContextSource">The source of the security context.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The Name property of <paramref name="name" /> is null.-or- The Name property of <paramref name="name" /> starts with white space, or contains a forward or backward slash. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="securityContextSource" /> was not one of the enumeration values.</exception>
		// Token: 0x060016E4 RID: 5860 RVA: 0x0005A8E2 File Offset: 0x00058AE2
		[MonoLimitation("The argument securityContextSource is ignored")]
		public AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, IEnumerable<CustomAttributeBuilder> assemblyAttributes, SecurityContextSource securityContextSource)
		{
			return this.DefineDynamicAssembly(name, access, assemblyAttributes);
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0005A8ED File Offset: 0x00058AED
		internal AssemblyBuilder DefineInternalDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
		{
			return new AssemblyBuilder(name, null, access, true);
		}

		/// <summary>Executes the code in another application domain that is identified by the specified delegate.</summary>
		/// <param name="callBackDelegate">A delegate that specifies a method to call. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callBackDelegate" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060016E6 RID: 5862 RVA: 0x0005A8F8 File Offset: 0x00058AF8
		public void DoCallBack(CrossAppDomainDelegate callBackDelegate)
		{
			if (callBackDelegate != null)
			{
				callBackDelegate();
			}
		}

		/// <summary>Executes the assembly contained in the specified file.</summary>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		/// <param name="assemblyFile">The name of the file that contains the assembly to execute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060016E7 RID: 5863 RVA: 0x0005A903 File Offset: 0x00058B03
		public int ExecuteAssembly(string assemblyFile)
		{
			return this.ExecuteAssembly(assemblyFile, null, null);
		}

		/// <summary>Executes the assembly contained in the specified file, using the specified evidence.</summary>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		/// <param name="assemblyFile">The name of the file that contains the assembly to execute. </param>
		/// <param name="assemblySecurity">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060016E8 RID: 5864 RVA: 0x0005A90E File Offset: 0x00058B0E
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity)
		{
			return this.ExecuteAssembly(assemblyFile, assemblySecurity, null);
		}

		/// <summary>Executes the assembly contained in the specified file, using the specified evidence and arguments.</summary>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		/// <param name="assemblyFile">The name of the file that contains the assembly to execute. </param>
		/// <param name="assemblySecurity">The supplied evidence for the assembly. </param>
		/// <param name="args">The arguments to the entry point of the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="assemblySecurity" /> is not null. When legacy CAS policy is not enabled, <paramref name="assemblySecurity" /> should be null. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060016E9 RID: 5865 RVA: 0x0005A91C File Offset: 0x00058B1C
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity, string[] args)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFile, assemblySecurity);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		/// <summary>Executes the assembly contained in the specified file, using the specified evidence, arguments, hash value, and hash algorithm.</summary>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		/// <param name="assemblyFile">The name of the file that contains the assembly to execute. </param>
		/// <param name="assemblySecurity">The supplied evidence for the assembly. </param>
		/// <param name="args">The arguments to the entry point of the assembly. </param>
		/// <param name="hashValue">Represents the value of the computed hash code. </param>
		/// <param name="hashAlgorithm">Represents the hash algorithm used by the assembly manifest. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyFile" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="assemblySecurity" /> is not null. When legacy CAS policy is not enabled, <paramref name="assemblySecurity" /> should be null. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060016EA RID: 5866 RVA: 0x0005A93C File Offset: 0x00058B3C
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity, string[] args, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFile, assemblySecurity, hashValue, hashAlgorithm);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		/// <summary>Executes the assembly contained in the specified file, using the specified arguments.</summary>
		/// <returns>The value that is returned by the entry point of the assembly.</returns>
		/// <param name="assemblyFile">The name of the file that contains the assembly to execute.</param>
		/// <param name="args">The arguments to the entry point of the assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-<paramref name="assemblyFile" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		// Token: 0x060016EB RID: 5867 RVA: 0x0005A960 File Offset: 0x00058B60
		public int ExecuteAssembly(string assemblyFile, string[] args)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFile, null);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		/// <summary>Executes the assembly contained in the specified file, using the specified arguments, hash value, and hash algorithm.</summary>
		/// <returns>The value that is returned by the entry point of the assembly.</returns>
		/// <param name="assemblyFile">The name of the file that contains the assembly to execute.</param>
		/// <param name="args">The arguments to the entry point of the assembly. </param>
		/// <param name="hashValue">Represents the value of the computed hash code. </param>
		/// <param name="hashAlgorithm">Represents the hash algorithm used by the assembly manifest. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFile" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyFile" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyFile" /> is not a valid assembly. -or-<paramref name="assemblyFile" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		// Token: 0x060016EC RID: 5868 RVA: 0x0005A980 File Offset: 0x00058B80
		public int ExecuteAssembly(string assemblyFile, string[] args, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyFile, null, hashValue, hashAlgorithm);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x0005A9A0 File Offset: 0x00058BA0
		private int ExecuteAssemblyInternal(Assembly a, string[] args)
		{
			if (a.EntryPoint == null)
			{
				throw new MissingMethodException("Entry point not found in assembly '" + a.FullName + "'.");
			}
			return this.ExecuteAssembly(a, args);
		}

		// Token: 0x060016EE RID: 5870
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int ExecuteAssembly(Assembly a, string[] args);

		// Token: 0x060016EF RID: 5871
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Assembly[] GetAssemblies(bool refOnly);

		/// <summary>Gets the assemblies that have been loaded into the execution context of this application domain.</summary>
		/// <returns>An array of assemblies in this application domain.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060016F0 RID: 5872 RVA: 0x0005A9D3 File Offset: 0x00058BD3
		public Assembly[] GetAssemblies()
		{
			return this.GetAssemblies(false);
		}

		/// <summary>Gets the value stored in the current application domain for the specified name.</summary>
		/// <returns>The value of the <paramref name="name" /> property, or null if the property does not exist.</returns>
		/// <param name="name">The name of a predefined application domain property, or the name of an application domain property you have defined.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060016F1 RID: 5873
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern object GetData(string name);

		/// <summary>Gets the type of the current instance.</summary>
		/// <returns>The type of the current instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060016F2 RID: 5874 RVA: 0x00033A19 File Offset: 0x00031C19
		public new Type GetType()
		{
			return base.GetType();
		}

		/// <summary>Gives the <see cref="T:System.AppDomain" /> an infinite lifetime by preventing a lease from being created.</summary>
		/// <returns>Always null.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060016F3 RID: 5875 RVA: 0x0000A42E File Offset: 0x0000862E
		[SecurityCritical]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x060016F4 RID: 5876
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Assembly LoadAssembly(string assemblyRef, Evidence securityEvidence, bool refOnly);

		/// <summary>Loads an <see cref="T:System.Reflection.Assembly" /> given its <see cref="T:System.Reflection.AssemblyName" />.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyRef">An object that describes the assembly to load. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyRef" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyRef" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyRef" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyRef" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060016F5 RID: 5877 RVA: 0x0005A9DC File Offset: 0x00058BDC
		[SecuritySafeCritical]
		public Assembly Load(AssemblyName assemblyRef)
		{
			return this.Load(assemblyRef, null);
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x0005A9E6 File Offset: 0x00058BE6
		internal Assembly LoadSatellite(AssemblyName assemblyRef, bool throwOnError)
		{
			if (assemblyRef == null)
			{
				throw new ArgumentNullException("assemblyRef");
			}
			Assembly assembly = this.LoadAssembly(assemblyRef.FullName, null, false);
			if (assembly == null && throwOnError)
			{
				throw new FileNotFoundException(null, assemblyRef.Name);
			}
			return assembly;
		}

		/// <summary>Loads an <see cref="T:System.Reflection.Assembly" /> given its <see cref="T:System.Reflection.AssemblyName" />.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyRef">An object that describes the assembly to load. </param>
		/// <param name="assemblySecurity">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyRef" /> is null</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyRef" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyRef" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyRef" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060016F7 RID: 5879 RVA: 0x0005AA1C File Offset: 0x00058C1C
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		[SecuritySafeCritical]
		public Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity)
		{
			if (assemblyRef == null)
			{
				throw new ArgumentNullException("assemblyRef");
			}
			if (assemblyRef.Name == null || assemblyRef.Name.Length == 0)
			{
				if (assemblyRef.CodeBase != null)
				{
					return Assembly.LoadFrom(assemblyRef.CodeBase, assemblySecurity);
				}
				throw new ArgumentException(Locale.GetText("assemblyRef.Name cannot be empty."), "assemblyRef");
			}
			else
			{
				Assembly assembly = this.LoadAssembly(assemblyRef.FullName, assemblySecurity, false);
				if (assembly != null)
				{
					return assembly;
				}
				if (assemblyRef.CodeBase == null)
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				string text = assemblyRef.CodeBase;
				if (text.ToLower(CultureInfo.InvariantCulture).StartsWith("file://"))
				{
					text = new Uri(text).LocalPath;
				}
				try
				{
					assembly = Assembly.LoadFrom(text, assemblySecurity);
				}
				catch
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				AssemblyName name = assembly.GetName();
				if (assemblyRef.Name != name.Name)
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				if (assemblyRef.Version != null && assemblyRef.Version != new Version(0, 0, 0, 0) && assemblyRef.Version != name.Version)
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				if (assemblyRef.CultureInfo != null && assemblyRef.CultureInfo.Equals(name))
				{
					throw new FileNotFoundException(null, assemblyRef.Name);
				}
				byte[] publicKeyToken = assemblyRef.GetPublicKeyToken();
				if (publicKeyToken != null && publicKeyToken.Length != 0)
				{
					byte[] publicKeyToken2 = name.GetPublicKeyToken();
					if (publicKeyToken2 == null || publicKeyToken.Length != publicKeyToken2.Length)
					{
						throw new FileNotFoundException(null, assemblyRef.Name);
					}
					for (int i = publicKeyToken.Length - 1; i >= 0; i--)
					{
						if (publicKeyToken2[i] != publicKeyToken[i])
						{
							throw new FileNotFoundException(null, assemblyRef.Name);
						}
					}
				}
				return assembly;
			}
		}

		/// <summary>Loads an <see cref="T:System.Reflection.Assembly" /> given its display name.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyString">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyString" /> is null</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyString" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyString" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyString" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060016F8 RID: 5880 RVA: 0x0005ABE4 File Offset: 0x00058DE4
		[SecuritySafeCritical]
		public Assembly Load(string assemblyString)
		{
			return this.Load(assemblyString, null, false);
		}

		/// <summary>Loads an <see cref="T:System.Reflection.Assembly" /> given its display name.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="assemblyString">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="assemblySecurity">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyString" /> is null</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="assemblyString" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="assemblyString" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyString" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060016F9 RID: 5881 RVA: 0x0005ABEF File Offset: 0x00058DEF
		[SecuritySafeCritical]
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public Assembly Load(string assemblyString, Evidence assemblySecurity)
		{
			return this.Load(assemblyString, assemblySecurity, false);
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0005ABFA File Offset: 0x00058DFA
		internal Assembly Load(string assemblyString, Evidence assemblySecurity, bool refonly)
		{
			if (assemblyString == null)
			{
				throw new ArgumentNullException("assemblyString");
			}
			if (assemblyString.Length == 0)
			{
				throw new ArgumentException("assemblyString cannot have zero length");
			}
			Assembly assembly = this.LoadAssembly(assemblyString, assemblySecurity, refonly);
			if (assembly == null)
			{
				throw new FileNotFoundException(null, assemblyString);
			}
			return assembly;
		}

		/// <summary>Loads the <see cref="T:System.Reflection.Assembly" /> with a common object file format (COFF) based image containing an emitted <see cref="T:System.Reflection.Assembly" />.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="rawAssembly">An array of type byte that is a COFF-based image containing an emitted assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060016FB RID: 5883 RVA: 0x0005AC37 File Offset: 0x00058E37
		[SecuritySafeCritical]
		public Assembly Load(byte[] rawAssembly)
		{
			return this.Load(rawAssembly, null, null);
		}

		/// <summary>Loads the <see cref="T:System.Reflection.Assembly" /> with a common object file format (COFF) based image containing an emitted <see cref="T:System.Reflection.Assembly" />. The raw bytes representing the symbols for the <see cref="T:System.Reflection.Assembly" /> are also loaded.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="rawAssembly">An array of type byte that is a COFF-based image containing an emitted assembly. </param>
		/// <param name="rawSymbolStore">An array of type byte containing the raw bytes representing the symbols for the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060016FC RID: 5884 RVA: 0x0005AC42 File Offset: 0x00058E42
		[SecuritySafeCritical]
		public Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
		{
			return this.Load(rawAssembly, rawSymbolStore, null);
		}

		// Token: 0x060016FD RID: 5885
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Assembly LoadAssemblyRaw(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence, bool refonly);

		/// <summary>Loads the <see cref="T:System.Reflection.Assembly" /> with a common object file format (COFF) based image containing an emitted <see cref="T:System.Reflection.Assembly" />. The raw bytes representing the symbols for the <see cref="T:System.Reflection.Assembly" /> are also loaded.</summary>
		/// <returns>The loaded assembly.</returns>
		/// <param name="rawAssembly">An array of type byte that is a COFF-based image containing an emitted assembly. </param>
		/// <param name="rawSymbolStore">An array of type byte containing the raw bytes representing the symbols for the assembly. </param>
		/// <param name="securityEvidence">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rawAssembly" /> is null. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="rawAssembly" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="rawAssembly" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">An assembly or module was loaded twice with two different evidences. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="securityEvidence" /> is not null. When legacy CAS policy is not enabled, <paramref name="securityEvidence" /> should be null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060016FE RID: 5886 RVA: 0x0005AC4D File Offset: 0x00058E4D
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlEvidence = true)]
		public Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence)
		{
			return this.Load(rawAssembly, rawSymbolStore, securityEvidence, false);
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0005AC59 File Offset: 0x00058E59
		internal Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence, bool refonly)
		{
			if (rawAssembly == null)
			{
				throw new ArgumentNullException("rawAssembly");
			}
			Assembly assembly = this.LoadAssemblyRaw(rawAssembly, rawSymbolStore, securityEvidence, refonly);
			assembly.FromByteArray = true;
			return assembly;
		}

		/// <summary>Establishes the security policy level for this application domain.</summary>
		/// <param name="domainPolicy">The security policy level. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="domainPolicy" /> is null. </exception>
		/// <exception cref="T:System.Security.Policy.PolicyException">The security policy level has already been set. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlDomainPolicy" />
		/// </PermissionSet>
		// Token: 0x06001700 RID: 5888 RVA: 0x0005AC7C File Offset: 0x00058E7C
		[Obsolete("AppDomain policy levels are obsolete")]
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, ControlPolicy = true)]
		public void SetAppDomainPolicy(PolicyLevel domainPolicy)
		{
			if (domainPolicy == null)
			{
				throw new ArgumentNullException("domainPolicy");
			}
			if (this._granted != null)
			{
				throw new PolicyException(Locale.GetText("An AppDomain policy is already specified."));
			}
			if (this.IsFinalizingForUnload())
			{
				throw new AppDomainUnloadedException();
			}
			PolicyStatement policyStatement = domainPolicy.Resolve(this._evidence);
			this._granted = policyStatement.PermissionSet;
		}

		/// <summary>Establishes the specified directory path as the location where assemblies are shadow copied.</summary>
		/// <param name="path">The fully qualified path to the shadow copy location. </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x06001701 RID: 5889 RVA: 0x0005ACD6 File Offset: 0x00058ED6
		[Obsolete("Use AppDomainSetup.SetCachePath")]
		[SecurityCritical]
		[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public void SetCachePath(string path)
		{
			this.SetupInformationNoCopy.CachePath = path;
		}

		/// <summary>Specifies how principal and identity objects should be attached to a thread if the thread attempts to bind to a principal while executing in this application domain.</summary>
		/// <param name="policy">One of the <see cref="T:System.Security.Principal.PrincipalPolicy" /> values that specifies the type of the principal object to attach to threads. </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x06001702 RID: 5890 RVA: 0x0005ACE4 File Offset: 0x00058EE4
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public void SetPrincipalPolicy(PrincipalPolicy policy)
		{
			if (this.IsFinalizingForUnload())
			{
				throw new AppDomainUnloadedException();
			}
			this._principalPolicy = policy;
			AppDomain._principal = null;
		}

		/// <summary>Turns on shadow copying.</summary>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x06001703 RID: 5891 RVA: 0x0005AD01 File Offset: 0x00058F01
		[Obsolete("Use AppDomainSetup.ShadowCopyFiles")]
		[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public void SetShadowCopyFiles()
		{
			this.SetupInformationNoCopy.ShadowCopyFiles = "true";
		}

		/// <summary>Establishes the specified directory path as the location of assemblies to be shadow copied.</summary>
		/// <param name="path">A list of directory names, where each name is separated by a semicolon. </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x06001704 RID: 5892 RVA: 0x0005AD13 File Offset: 0x00058F13
		[SecurityCritical]
		[Obsolete("Use AppDomainSetup.ShadowCopyDirectories")]
		[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public void SetShadowCopyPath(string path)
		{
			this.SetupInformationNoCopy.ShadowCopyDirectories = path;
		}

		/// <summary>Sets the default principal object to be attached to threads if they attempt to bind to a principal while executing in this application domain.</summary>
		/// <param name="principal">The principal object to attach to threads. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="principal" /> is null. </exception>
		/// <exception cref="T:System.Security.Policy.PolicyException">The thread principal has already been set. </exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPrincipal" />
		/// </PermissionSet>
		// Token: 0x06001705 RID: 5893 RVA: 0x0005AD21 File Offset: 0x00058F21
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		public void SetThreadPrincipal(IPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			if (AppDomain._principal != null)
			{
				throw new PolicyException(Locale.GetText("principal already present."));
			}
			if (this.IsFinalizingForUnload())
			{
				throw new AppDomainUnloadedException();
			}
			AppDomain._principal = principal;
		}

		// Token: 0x06001706 RID: 5894
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AppDomain InternalSetDomainByID(int domain_id);

		// Token: 0x06001707 RID: 5895
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AppDomain InternalSetDomain(AppDomain context);

		// Token: 0x06001708 RID: 5896
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalPushDomainRef(AppDomain domain);

		// Token: 0x06001709 RID: 5897
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalPushDomainRefByID(int domain_id);

		// Token: 0x0600170A RID: 5898
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalPopDomainRef();

		// Token: 0x0600170B RID: 5899
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Context InternalSetContext(Context context);

		// Token: 0x0600170C RID: 5900
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Context InternalGetContext();

		// Token: 0x0600170D RID: 5901
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Context InternalGetDefaultContext();

		// Token: 0x0600170E RID: 5902
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string InternalGetProcessGuid(string newguid);

		// Token: 0x0600170F RID: 5903 RVA: 0x0005AD5C File Offset: 0x00058F5C
		internal static object InvokeInDomain(AppDomain domain, MethodInfo method, object obj, object[] args)
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			bool flag = false;
			object obj3;
			try
			{
				AppDomain.InternalPushDomainRef(domain);
				flag = true;
				AppDomain.InternalSetDomain(domain);
				Exception ex;
				object obj2 = ((MonoMethod)method).InternalInvoke(obj, args, out ex);
				if (ex != null)
				{
					throw ex;
				}
				obj3 = obj2;
			}
			finally
			{
				AppDomain.InternalSetDomain(currentDomain);
				if (flag)
				{
					AppDomain.InternalPopDomainRef();
				}
			}
			return obj3;
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0005ADB8 File Offset: 0x00058FB8
		internal static object InvokeInDomainByID(int domain_id, MethodInfo method, object obj, object[] args)
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			bool flag = false;
			object obj3;
			try
			{
				AppDomain.InternalPushDomainRefByID(domain_id);
				flag = true;
				AppDomain.InternalSetDomainByID(domain_id);
				Exception ex;
				object obj2 = ((MonoMethod)method).InternalInvoke(obj, args, out ex);
				if (ex != null)
				{
					throw ex;
				}
				obj3 = obj2;
			}
			finally
			{
				AppDomain.InternalSetDomain(currentDomain);
				if (flag)
				{
					AppDomain.InternalPopDomainRef();
				}
			}
			return obj3;
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0005AE14 File Offset: 0x00059014
		internal static string GetProcessGuid()
		{
			if (AppDomain._process_guid == null)
			{
				AppDomain._process_guid = AppDomain.InternalGetProcessGuid(Guid.NewGuid().ToString());
			}
			return AppDomain._process_guid;
		}

		/// <summary>Creates a new application domain with the specified name.</summary>
		/// <returns>The newly created application domain.</returns>
		/// <param name="friendlyName">The friendly name of the domain. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="friendlyName" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x06001712 RID: 5906 RVA: 0x0005AE4A File Offset: 0x0005904A
		public static AppDomain CreateDomain(string friendlyName)
		{
			return AppDomain.CreateDomain(friendlyName, null, null);
		}

		/// <summary>Creates a new application domain with the given name using the supplied evidence.</summary>
		/// <returns>The newly created application domain.</returns>
		/// <param name="friendlyName">The friendly name of the domain. This friendly name can be displayed in user interfaces to identify the domain. For more information, see <see cref="P:System.AppDomain.FriendlyName" />. </param>
		/// <param name="securityInfo">Evidence that establishes the identity of the code that runs in the application domain. Pass null to use the evidence of the current application domain.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="friendlyName" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x06001713 RID: 5907 RVA: 0x0005AE54 File Offset: 0x00059054
		public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo)
		{
			return AppDomain.CreateDomain(friendlyName, securityInfo, null);
		}

		// Token: 0x06001714 RID: 5908
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AppDomain createDomain(string friendlyName, AppDomainSetup info);

		/// <summary>Creates a new application domain using the specified name, evidence, and application domain setup information.</summary>
		/// <returns>The newly created application domain.</returns>
		/// <param name="friendlyName">The friendly name of the domain. This friendly name can be displayed in user interfaces to identify the domain. For more information, see <see cref="P:System.AppDomain.FriendlyName" />. </param>
		/// <param name="securityInfo">Evidence that establishes the identity of the code that runs in the application domain. Pass null to use the evidence of the current application domain.</param>
		/// <param name="info">An object that contains application domain initialization information. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="friendlyName" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x06001715 RID: 5909 RVA: 0x0005AE60 File Offset: 0x00059060
		[MonoLimitation("Currently it does not allow the setup in the other domain")]
		[SecurityPermission(SecurityAction.Demand, ControlAppDomain = true)]
		public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup info)
		{
			if (friendlyName == null)
			{
				throw new ArgumentNullException("friendlyName");
			}
			AppDomain defaultDomain = AppDomain.DefaultDomain;
			if (info == null)
			{
				if (defaultDomain == null)
				{
					info = new AppDomainSetup();
				}
				else
				{
					info = defaultDomain.SetupInformation;
				}
			}
			else
			{
				info = new AppDomainSetup(info);
			}
			if (defaultDomain != null)
			{
				if (!info.Equals(defaultDomain.SetupInformation))
				{
					if (info.ApplicationBase == null)
					{
						info.ApplicationBase = defaultDomain.SetupInformation.ApplicationBase;
					}
					if (info.ConfigurationFile == null)
					{
						info.ConfigurationFile = Path.GetFileName(defaultDomain.SetupInformation.ConfigurationFile);
					}
				}
			}
			else if (info.ConfigurationFile == null)
			{
				info.ConfigurationFile = "[I don't have a config file]";
			}
			if (info.AppDomainInitializer != null && !info.AppDomainInitializer.Method.IsStatic)
			{
				throw new ArgumentException("Non-static methods cannot be invoked as an appdomain initializer");
			}
			info.SerializeNonPrimitives();
			AppDomain appDomain = (AppDomain)RemotingServices.GetDomainProxy(AppDomain.createDomain(friendlyName, info));
			if (securityInfo == null)
			{
				if (defaultDomain == null)
				{
					appDomain._evidence = null;
				}
				else
				{
					appDomain._evidence = defaultDomain.Evidence;
				}
			}
			else
			{
				appDomain._evidence = new Evidence(securityInfo);
			}
			if (info.AppDomainInitializer != null)
			{
				AppDomain.Loader loader = new AppDomain.Loader(info.AppDomainInitializer.Method.DeclaringType.Assembly.Location);
				appDomain.DoCallBack(new CrossAppDomainDelegate(loader.Load));
				AppDomain.Initializer initializer = new AppDomain.Initializer(info.AppDomainInitializer, info.AppDomainInitializerArguments);
				appDomain.DoCallBack(new CrossAppDomainDelegate(initializer.Initialize));
			}
			return appDomain;
		}

		/// <summary>Creates a new application domain with the given name, using evidence, application base path, relative search path, and a parameter that specifies whether a shadow copy of an assembly is to be loaded into the application domain.</summary>
		/// <returns>The newly created application domain.</returns>
		/// <param name="friendlyName">The friendly name of the domain. This friendly name can be displayed in user interfaces to identify the domain. For more information, see <see cref="P:System.AppDomain.FriendlyName" />. </param>
		/// <param name="securityInfo">Evidence that establishes the identity of the code that runs in the application domain. Pass null to use the evidence of the current application domain.</param>
		/// <param name="appBasePath">The base directory that the assembly resolver uses to probe for assemblies. For more information, see <see cref="P:System.AppDomain.BaseDirectory" />. </param>
		/// <param name="appRelativeSearchPath">The path relative to the base directory where the assembly resolver should probe for private assemblies. For more information, see <see cref="P:System.AppDomain.RelativeSearchPath" />. </param>
		/// <param name="shadowCopyFiles">If true, a shadow copy of an assembly is loaded into this application domain. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="friendlyName" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x06001716 RID: 5910 RVA: 0x0005AFC4 File Offset: 0x000591C4
		public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles)
		{
			return AppDomain.CreateDomain(friendlyName, securityInfo, AppDomain.CreateDomainSetup(appBasePath, appRelativeSearchPath, shadowCopyFiles));
		}

		/// <summary>Creates a new application domain using the specified name, evidence, application domain setup information, default permission set, and array of fully trusted assemblies.</summary>
		/// <returns>The newly created application domain.</returns>
		/// <param name="friendlyName">The friendly name of the domain. This friendly name can be displayed in user interfaces to identify the domain. For more information, see the description of <see cref="P:System.AppDomain.FriendlyName" />.</param>
		/// <param name="securityInfo">Evidence that establishes the identity of the code that runs in the application domain. Pass null to use the evidence of the current application domain.</param>
		/// <param name="info">An object that contains application domain initialization information.</param>
		/// <param name="grantSet">A default permission set that is granted to all assemblies loaded into the new application domain that do not have specific grants. </param>
		/// <param name="fullTrustAssemblies">An array of strong names representing assemblies to be considered fully trusted in the new application domain.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="friendlyName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The application domain is null.-or-The <see cref="P:System.AppDomainSetup.ApplicationBase" /> property is not set on the <see cref="T:System.AppDomainSetup" /> object that is supplied for <paramref name="info" />. </exception>
		// Token: 0x06001717 RID: 5911 RVA: 0x0005AFD6 File Offset: 0x000591D6
		public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup info, PermissionSet grantSet, params global::System.Security.Policy.StrongName[] fullTrustAssemblies)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.ApplicationTrust = new ApplicationTrust(grantSet, fullTrustAssemblies ?? EmptyArray<global::System.Security.Policy.StrongName>.Value);
			return AppDomain.CreateDomain(friendlyName, securityInfo, info);
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0005B008 File Offset: 0x00059208
		private static AppDomainSetup CreateDomainSetup(string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles)
		{
			AppDomainSetup appDomainSetup = new AppDomainSetup();
			appDomainSetup.ApplicationBase = appBasePath;
			appDomainSetup.PrivateBinPath = appRelativeSearchPath;
			if (shadowCopyFiles)
			{
				appDomainSetup.ShadowCopyFiles = "true";
			}
			else
			{
				appDomainSetup.ShadowCopyFiles = "false";
			}
			return appDomainSetup;
		}

		// Token: 0x06001719 RID: 5913
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalIsFinalizingForUnload(int domain_id);

		/// <summary>Indicates whether this application domain is unloading, and the objects it contains are being finalized by the common language runtime.</summary>
		/// <returns>true if this application domain is unloading and the common language runtime has started invoking finalizers; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600171A RID: 5914 RVA: 0x0005B045 File Offset: 0x00059245
		public bool IsFinalizingForUnload()
		{
			return AppDomain.InternalIsFinalizingForUnload(this.getDomainID());
		}

		// Token: 0x0600171B RID: 5915
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalUnload(int domain_id);

		// Token: 0x0600171C RID: 5916 RVA: 0x0005B052 File Offset: 0x00059252
		private int getDomainID()
		{
			return Thread.GetDomainID();
		}

		/// <summary>Unloads the specified application domain.</summary>
		/// <param name="domain">An application domain to unload. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="domain" /> is null. </exception>
		/// <exception cref="T:System.CannotUnloadAppDomainException">
		///   <paramref name="domain" /> could not be unloaded. </exception>
		/// <exception cref="T:System.Exception">An error occurred during the unload process.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x0600171D RID: 5917 RVA: 0x0005B059 File Offset: 0x00059259
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.MayFail)]
		[SecurityPermission(SecurityAction.Demand, ControlAppDomain = true)]
		public static void Unload(AppDomain domain)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			AppDomain.InternalUnload(domain.getDomainID());
		}

		/// <summary>Assigns the specified value to the specified application domain property.</summary>
		/// <param name="name">The name of a user-defined application domain property to create or change. </param>
		/// <param name="data">The value of the property. </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x0600171E RID: 5918
		[SecurityCritical]
		[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetData(string name, object data);

		/// <summary>Assigns the specified value to the specified application domain property, with a specified permission to demand of the caller when the property is retrieved.</summary>
		/// <param name="name">The name of a user-defined application domain property to create or change. </param>
		/// <param name="data">The value of the property. </param>
		/// <param name="permission">The permission to demand of the caller when the property is retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="name" /> specifies a system-defined property string and <paramref name="permission" /> is not null.</exception>
		// Token: 0x0600171F RID: 5919 RVA: 0x0005B074 File Offset: 0x00059274
		[MonoLimitation("The permission field is ignored")]
		public void SetData(string name, object data, IPermission permission)
		{
			this.SetData(name, data);
		}

		/// <summary>Establishes the specified directory path as the base directory for subdirectories where dynamically generated files are stored and accessed.</summary>
		/// <param name="path">The fully qualified path that is the base directory for subdirectories where dynamic assemblies are stored. </param>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x06001720 RID: 5920 RVA: 0x0005B07E File Offset: 0x0005927E
		[Obsolete("Use AppDomainSetup.DynamicBase")]
		[SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public void SetDynamicBase(string path)
		{
			this.SetupInformationNoCopy.DynamicBase = path;
		}

		/// <summary>Gets the current thread identifier.</summary>
		/// <returns>A 32-bit signed integer that is the identifier of the current thread.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001721 RID: 5921 RVA: 0x0005B08C File Offset: 0x0005928C
		[Obsolete("AppDomain.GetCurrentThreadId has been deprecated because it does not provide a stable Id when managed threads are running on fibers (aka lightweight threads). To get a stable identifier for a managed thread, use the ManagedThreadId property on Thread.'")]
		public static int GetCurrentThreadId()
		{
			return Thread.CurrentThreadId;
		}

		/// <summary>Obtains a string representation that includes the friendly name of the application domain and any context policies.</summary>
		/// <returns>A string formed by concatenating the literal string "Name:", the friendly name of the application domain, and either string representations of the context policies or the string "There are no context policies." </returns>
		/// <exception cref="T:System.AppDomainUnloadedException">The application domain represented by the current <see cref="T:System.AppDomain" /> has been unloaded.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001722 RID: 5922 RVA: 0x0005A253 File Offset: 0x00058453
		[SecuritySafeCritical]
		public override string ToString()
		{
			return this.getFriendlyName();
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x0005B094 File Offset: 0x00059294
		private static void ValidateAssemblyName(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentException("The Name of AssemblyName cannot be null or a zero-length string.");
			}
			bool flag = true;
			for (int i = 0; i < name.Length; i++)
			{
				char c = name[i];
				if (i == 0 && char.IsWhiteSpace(c))
				{
					flag = false;
					break;
				}
				if (c == '/' || c == '\\' || c == ':')
				{
					flag = false;
					break;
				}
			}
			if (!flag)
			{
				throw new ArgumentException("The Name of AssemblyName cannot start with whitespace, or contain '/', '\\'  or ':'.");
			}
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0005B102 File Offset: 0x00059302
		private void DoAssemblyLoad(Assembly assembly)
		{
			if (this.AssemblyLoad == null)
			{
				return;
			}
			this.AssemblyLoad(this, new AssemblyLoadEventArgs(assembly));
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0005B120 File Offset: 0x00059320
		private Assembly DoAssemblyResolve(string name, Assembly requestingAssembly, bool refonly)
		{
			ResolveEventHandler resolveEventHandler;
			if (refonly)
			{
				resolveEventHandler = this.ReflectionOnlyAssemblyResolve;
			}
			else
			{
				resolveEventHandler = this.AssemblyResolve;
			}
			if (resolveEventHandler == null)
			{
				return null;
			}
			Dictionary<string, object> dictionary;
			if (refonly)
			{
				dictionary = AppDomain.assembly_resolve_in_progress_refonly;
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, object>();
					AppDomain.assembly_resolve_in_progress_refonly = dictionary;
				}
			}
			else
			{
				dictionary = AppDomain.assembly_resolve_in_progress;
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, object>();
					AppDomain.assembly_resolve_in_progress = dictionary;
				}
			}
			if (dictionary.ContainsKey(name))
			{
				return null;
			}
			dictionary[name] = null;
			Assembly assembly2;
			try
			{
				Delegate[] invocationList = resolveEventHandler.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					Assembly assembly = ((ResolveEventHandler)invocationList[i])(this, new ResolveEventArgs(name, requestingAssembly));
					if (assembly != null)
					{
						return assembly;
					}
				}
				assembly2 = null;
			}
			finally
			{
				dictionary.Remove(name);
			}
			return assembly2;
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x0005B1E4 File Offset: 0x000593E4
		internal Assembly DoTypeResolve(object name_or_tb)
		{
			if (this.TypeResolve == null)
			{
				return null;
			}
			string text;
			if (name_or_tb is TypeBuilder)
			{
				text = ((TypeBuilder)name_or_tb).FullName;
			}
			else
			{
				text = (string)name_or_tb;
			}
			Dictionary<string, object> dictionary = AppDomain.type_resolve_in_progress;
			if (dictionary == null)
			{
				dictionary = (AppDomain.type_resolve_in_progress = new Dictionary<string, object>());
			}
			if (dictionary.ContainsKey(text))
			{
				return null;
			}
			dictionary[text] = null;
			Assembly assembly2;
			try
			{
				Delegate[] invocationList = this.TypeResolve.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					Assembly assembly = ((ResolveEventHandler)invocationList[i])(this, new ResolveEventArgs(text));
					if (assembly != null)
					{
						return assembly;
					}
				}
				assembly2 = null;
			}
			finally
			{
				dictionary.Remove(text);
			}
			return assembly2;
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x0005B2A0 File Offset: 0x000594A0
		internal Assembly DoResourceResolve(string name, Assembly requesting)
		{
			if (this.ResourceResolve == null)
			{
				return null;
			}
			Delegate[] invocationList = this.ResourceResolve.GetInvocationList();
			for (int i = 0; i < invocationList.Length; i++)
			{
				Assembly assembly = ((ResolveEventHandler)invocationList[i])(this, new ResolveEventArgs(name, requesting));
				if (assembly != null)
				{
					return assembly;
				}
			}
			return null;
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0005B2F3 File Offset: 0x000594F3
		private void DoDomainUnload()
		{
			if (this.DomainUnload != null)
			{
				this.DomainUnload(this, null);
			}
		}

		// Token: 0x06001729 RID: 5929
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void DoUnhandledException(Exception e);

		// Token: 0x0600172A RID: 5930 RVA: 0x0005B30A File Offset: 0x0005950A
		internal void DoUnhandledException(UnhandledExceptionEventArgs args)
		{
			if (this.UnhandledException != null)
			{
				this.UnhandledException(this, args);
			}
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0005B321 File Offset: 0x00059521
		internal byte[] GetMarshalledDomainObjRef()
		{
			return CADSerializer.SerializeObject(RemotingServices.Marshal(AppDomain.CurrentDomain, null, typeof(AppDomain))).GetBuffer();
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0005B344 File Offset: 0x00059544
		internal void ProcessMessageInDomain(byte[] arrRequest, CADMethodCallMessage cadMsg, out byte[] arrResponse, out CADMethodReturnMessage cadMrm)
		{
			IMessage message;
			if (arrRequest != null)
			{
				message = CADSerializer.DeserializeMessage(new MemoryStream(arrRequest), null);
			}
			else
			{
				message = new MethodCall(cadMsg);
			}
			IMessage message2 = ChannelServices.SyncDispatchMessage(message);
			cadMrm = CADMethodReturnMessage.Create(message2);
			if (cadMrm == null)
			{
				arrResponse = CADSerializer.SerializeMessage(message2).GetBuffer();
				return;
			}
			arrResponse = null;
		}

		/// <summary>Occurs when an assembly is loaded.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600172D RID: 5933 RVA: 0x0005B390 File Offset: 0x00059590
		// (remove) Token: 0x0600172E RID: 5934 RVA: 0x0005B3C8 File Offset: 0x000595C8
		[method: SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public event AssemblyLoadEventHandler AssemblyLoad;

		/// <summary>Occurs when the resolution of an assembly fails.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600172F RID: 5935 RVA: 0x0005B400 File Offset: 0x00059600
		// (remove) Token: 0x06001730 RID: 5936 RVA: 0x0005B438 File Offset: 0x00059638
		[method: SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public event ResolveEventHandler AssemblyResolve;

		/// <summary>Occurs when an <see cref="T:System.AppDomain" /> is about to be unloaded.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06001731 RID: 5937 RVA: 0x0005B470 File Offset: 0x00059670
		// (remove) Token: 0x06001732 RID: 5938 RVA: 0x0005B4A8 File Offset: 0x000596A8
		[method: SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public event EventHandler DomainUnload;

		/// <summary>Occurs when the default application domain's parent process exits.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06001733 RID: 5939 RVA: 0x0005B4E0 File Offset: 0x000596E0
		// (remove) Token: 0x06001734 RID: 5940 RVA: 0x0005B518 File Offset: 0x00059718
		[method: SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public event EventHandler ProcessExit;

		/// <summary>Occurs when the resolution of a resource fails because the resource is not a valid linked or embedded resource in the assembly.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06001735 RID: 5941 RVA: 0x0005B550 File Offset: 0x00059750
		// (remove) Token: 0x06001736 RID: 5942 RVA: 0x0005B588 File Offset: 0x00059788
		[method: SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public event ResolveEventHandler ResourceResolve;

		/// <summary>Occurs when the resolution of a type fails.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06001737 RID: 5943 RVA: 0x0005B5C0 File Offset: 0x000597C0
		// (remove) Token: 0x06001738 RID: 5944 RVA: 0x0005B5F8 File Offset: 0x000597F8
		[method: SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public event ResolveEventHandler TypeResolve;

		/// <summary>Occurs when an exception is not caught.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06001739 RID: 5945 RVA: 0x0005B630 File Offset: 0x00059830
		// (remove) Token: 0x0600173A RID: 5946 RVA: 0x0005B668 File Offset: 0x00059868
		[method: SecurityPermission(SecurityAction.LinkDemand, ControlAppDomain = true)]
		public event UnhandledExceptionEventHandler UnhandledException;

		/// <summary>Occurs when an exception is thrown in managed code, before the runtime searches the call stack for an exception handler in the application domain.</summary>
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600173B RID: 5947 RVA: 0x0005B6A0 File Offset: 0x000598A0
		// (remove) Token: 0x0600173C RID: 5948 RVA: 0x0005B6D8 File Offset: 0x000598D8
		public event EventHandler<FirstChanceExceptionEventArgs> FirstChanceException;

		/// <summary>Gets a value that indicates whether the current application domain has a set of permissions that is granted to all assemblies that are loaded into the application domain.</summary>
		/// <returns>true if the current application domain has a homogenous set of permissions; otherwise, false.</returns>
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x00003B29 File Offset: 0x00001D29
		[MonoTODO]
		public bool IsHomogenous
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether assemblies that are loaded into the current application domain execute with full trust.</summary>
		/// <returns>true if assemblies that are loaded into the current application domain execute with full trust; otherwise, false.</returns>
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x00003B29 File Offset: 0x00001D29
		[MonoTODO]
		public bool IsFullyTrusted
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the domain manager that was provided by the host when the application domain was initialized.</summary>
		/// <returns>An object that represents the domain manager provided by the host when the application domain was initialized, or null if no domain manager was provided.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlDomainPolicy" />
		/// </PermissionSet>
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x0005B70D File Offset: 0x0005990D
		public AppDomainManager DomainManager
		{
			get
			{
				return this._domain_manager;
			}
		}

		/// <summary>Occurs when the resolution of an assembly fails in the reflection-only context.</summary>
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06001740 RID: 5952 RVA: 0x0005B718 File Offset: 0x00059918
		// (remove) Token: 0x06001741 RID: 5953 RVA: 0x0005B750 File Offset: 0x00059950
		public event ResolveEventHandler ReflectionOnlyAssemblyResolve;

		/// <summary>Gets the activation context for the current application domain.</summary>
		/// <returns>An object that represents the activation context for the current application domain, or null if the domain has no activation context.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x0005B785 File Offset: 0x00059985
		public ActivationContext ActivationContext
		{
			get
			{
				return this._activation;
			}
		}

		/// <summary>Gets the identity of the application in the application domain.</summary>
		/// <returns>An object that identifies the application in the application domain.</returns>
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x0005B78D File Offset: 0x0005998D
		public ApplicationIdentity ApplicationIdentity
		{
			get
			{
				return this._applicationIdentity;
			}
		}

		/// <summary>Gets an integer that uniquely identifies the application domain within the process. </summary>
		/// <returns>An integer that identifies the application domain.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x0005B795 File Offset: 0x00059995
		public int Id
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.getDomainID();
			}
		}

		/// <summary>Returns the assembly display name after policy has been applied.</summary>
		/// <returns>A string containing the assembly display name after policy has been applied.</returns>
		/// <param name="assemblyName">The assembly display name, in the form provided by the <see cref="P:System.Reflection.Assembly.FullName" /> property.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001745 RID: 5957 RVA: 0x0005B79D File Offset: 0x0005999D
		[ComVisible(false)]
		[MonoTODO("This routine only returns the parameter currently")]
		public string ApplyPolicy(string assemblyName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (assemblyName.Length == 0)
			{
				throw new ArgumentException("assemblyName");
			}
			return assemblyName;
		}

		/// <summary>Creates a new application domain with the given name, using evidence, application base path, relative search path, and a parameter that specifies whether a shadow copy of an assembly is to be loaded into the application domain. Specifies a callback method that is invoked when the application domain is initialized, and an array of string arguments to pass the callback method.</summary>
		/// <returns>The newly created application domain.</returns>
		/// <param name="friendlyName">The friendly name of the domain. This friendly name can be displayed in user interfaces to identify the domain. For more information, see <see cref="P:System.AppDomain.FriendlyName" />. </param>
		/// <param name="securityInfo">Evidence that establishes the identity of the code that runs in the application domain. Pass null to use the evidence of the current application domain.</param>
		/// <param name="appBasePath">The base directory that the assembly resolver uses to probe for assemblies. For more information, see <see cref="P:System.AppDomain.BaseDirectory" />. </param>
		/// <param name="appRelativeSearchPath">The path relative to the base directory where the assembly resolver should probe for private assemblies. For more information, see <see cref="P:System.AppDomain.RelativeSearchPath" />. </param>
		/// <param name="shadowCopyFiles">true to load a shadow copy of an assembly into the application domain. </param>
		/// <param name="adInit">An <see cref="T:System.AppDomainInitializer" /> delegate that represents a callback method to invoke when the new <see cref="T:System.AppDomain" /> object is initialized.</param>
		/// <param name="adInitArgs">An array of string arguments to be passed to the callback represented by <paramref name="adInit" />, when the new <see cref="T:System.AppDomain" /> object is initialized.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="friendlyName" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlAppDomain" />
		/// </PermissionSet>
		// Token: 0x06001746 RID: 5958 RVA: 0x0005B7C4 File Offset: 0x000599C4
		public static AppDomain CreateDomain(string friendlyName, Evidence securityInfo, string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles, AppDomainInitializer adInit, string[] adInitArgs)
		{
			AppDomainSetup appDomainSetup = AppDomain.CreateDomainSetup(appBasePath, appRelativeSearchPath, shadowCopyFiles);
			appDomainSetup.AppDomainInitializerArguments = adInitArgs;
			appDomainSetup.AppDomainInitializer = adInit;
			return AppDomain.CreateDomain(friendlyName, securityInfo, appDomainSetup);
		}

		/// <summary>Executes an assembly given its display name.</summary>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly specified by <paramref name="assemblyName" /> is not found. </exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly specified by <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly specified by <paramref name="assemblyName" /> was found, but could not be loaded.</exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001747 RID: 5959 RVA: 0x0005B7F3 File Offset: 0x000599F3
		public int ExecuteAssemblyByName(string assemblyName)
		{
			return this.ExecuteAssemblyByName(assemblyName, null, null);
		}

		/// <summary>Executes an assembly given its display name, using the specified evidence.</summary>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="assemblySecurity">Evidence for loading the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly specified by <paramref name="assemblyName" /> is not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly specified by <paramref name="assemblyName" /> was found, but could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly specified by <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001748 RID: 5960 RVA: 0x0005B7FE File Offset: 0x000599FE
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public int ExecuteAssemblyByName(string assemblyName, Evidence assemblySecurity)
		{
			return this.ExecuteAssemblyByName(assemblyName, assemblySecurity, null);
		}

		/// <summary>Executes the assembly given its display name, using the specified evidence and arguments.</summary>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="assemblySecurity">Evidence for loading the assembly. </param>
		/// <param name="args">Command-line arguments to pass when starting the process. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly specified by <paramref name="assemblyName" /> is not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly specified by <paramref name="assemblyName" /> was found, but could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly specified by <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="assemblySecurity" /> is not null. When legacy CAS policy is not enabled, <paramref name="assemblySecurity" /> should be null. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001749 RID: 5961 RVA: 0x0005B80C File Offset: 0x00059A0C
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public int ExecuteAssemblyByName(string assemblyName, Evidence assemblySecurity, params string[] args)
		{
			Assembly assembly = Assembly.Load(assemblyName, assemblySecurity);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		/// <summary>Executes the assembly given an <see cref="T:System.Reflection.AssemblyName" />, using the specified evidence and arguments.</summary>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		/// <param name="assemblyName">An <see cref="T:System.Reflection.AssemblyName" /> object representing the name of the assembly. </param>
		/// <param name="assemblySecurity">Evidence for loading the assembly. </param>
		/// <param name="args">Command-line arguments to pass when starting the process. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly specified by <paramref name="assemblyName" /> is not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly specified by <paramref name="assemblyName" /> was found, but could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly specified by <paramref name="assemblyName" /> is not a valid assembly. -or-Version 2.0 or later of the common language runtime is currently loaded and <paramref name="assemblyName" /> was compiled with a later version.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="assemblySecurity" /> is not null. When legacy CAS policy is not enabled, <paramref name="assemblySecurity" /> should be null. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600174A RID: 5962 RVA: 0x0005B82C File Offset: 0x00059A2C
		[Obsolete("Use an overload that does not take an Evidence parameter")]
		public int ExecuteAssemblyByName(AssemblyName assemblyName, Evidence assemblySecurity, params string[] args)
		{
			Assembly assembly = Assembly.Load(assemblyName, assemblySecurity);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		/// <summary>Executes the assembly given its display name, using the specified arguments.</summary>
		/// <returns>The value that is returned by the entry point of the assembly.</returns>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="args">Command-line arguments to pass when starting the process.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly specified by <paramref name="assemblyName" /> is not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly specified by <paramref name="assemblyName" /> was found, but could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly specified by <paramref name="assemblyName" /> is not a valid assembly. -or-<paramref name="assemblyName" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		// Token: 0x0600174B RID: 5963 RVA: 0x0005B84C File Offset: 0x00059A4C
		public int ExecuteAssemblyByName(string assemblyName, params string[] args)
		{
			Assembly assembly = Assembly.Load(assemblyName, null);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		/// <summary>Executes the assembly given an <see cref="T:System.Reflection.AssemblyName" />, using the specified arguments.</summary>
		/// <returns>The value that is returned by the entry point of the assembly.</returns>
		/// <param name="assemblyName">An <see cref="T:System.Reflection.AssemblyName" /> object representing the name of the assembly.</param>
		/// <param name="args">Command-line arguments to pass when starting the process.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The assembly specified by <paramref name="assemblyName" /> is not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly specified by <paramref name="assemblyName" /> was found, but could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly specified by <paramref name="assemblyName" /> is not a valid assembly. -or-<paramref name="assemblyName" /> was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <exception cref="T:System.AppDomainUnloadedException">The operation is attempted on an unloaded application domain. </exception>
		/// <exception cref="T:System.MissingMethodException">The specified assembly has no entry point.</exception>
		// Token: 0x0600174C RID: 5964 RVA: 0x0005B86C File Offset: 0x00059A6C
		public int ExecuteAssemblyByName(AssemblyName assemblyName, params string[] args)
		{
			Assembly assembly = Assembly.Load(assemblyName, null);
			return this.ExecuteAssemblyInternal(assembly, args);
		}

		/// <summary>Returns a value that indicates whether the application domain is the default application domain for the process.</summary>
		/// <returns>true if the current <see cref="T:System.AppDomain" /> object represents the default application domain for the process; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600174D RID: 5965 RVA: 0x0005B889 File Offset: 0x00059A89
		public bool IsDefaultAppDomain()
		{
			return this == AppDomain.DefaultDomain;
		}

		/// <summary>Returns the assemblies that have been loaded into the reflection-only context of the application domain.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.Assembly" /> objects that represent the assemblies loaded into the reflection-only context of the application domain.</returns>
		/// <exception cref="T:System.AppDomainUnloadedException">An operation is attempted on an unloaded application domain. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600174E RID: 5966 RVA: 0x0005B893 File Offset: 0x00059A93
		public Assembly[] ReflectionOnlyGetAssemblies()
		{
			return this.GetAssemblies(true);
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600174F RID: 5967 RVA: 0x0002126B File Offset: 0x0001F46B
		void _AppDomain.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06001750 RID: 5968 RVA: 0x0002126B File Offset: 0x0001F46B
		void _AppDomain.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06001751 RID: 5969 RVA: 0x0002126B File Offset: 0x0001F46B
		void _AppDomain.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06001752 RID: 5970 RVA: 0x0002126B File Offset: 0x0001F46B
		void _AppDomain.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a nullable Boolean value that indicates whether any compatibility switches are set, and if so, whether the specified compatibility switch is set.</summary>
		/// <returns>A null reference (Nothing in Visual Basic) if no compatibility switches are set; otherwise, a Boolean value that indicates whether the compatibility switch that is specified by <paramref name="value" /> is set.</returns>
		/// <param name="value">The compatibility switch to test.</param>
		// Token: 0x06001753 RID: 5971 RVA: 0x0005B89C File Offset: 0x00059A9C
		public bool? IsCompatibilitySwitchSet(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return new bool?(this.compatibility_switch != null && this.compatibility_switch.Contains(value));
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0005B8C8 File Offset: 0x00059AC8
		internal void SetCompatibilitySwitch(string value)
		{
			if (this.compatibility_switch == null)
			{
				this.compatibility_switch = new List<string>();
			}
			this.compatibility_switch.Add(value);
		}

		/// <summary>Gets or sets a value that indicates whether CPU and memory monitoring of application domains is enabled for the current process. Once monitoring is enabled for a process, it cannot be disabled.</summary>
		/// <returns>true if monitoring is enabled; otherwise false.</returns>
		/// <exception cref="T:System.ArgumentException">The current process attempted to assign the value false to this property.</exception>
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x00015ED5 File Offset: 0x000140D5
		// (set) Token: 0x06001756 RID: 5974 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO("Currently always returns false")]
		public static bool MonitoringIsEnabled
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the number of bytes that survived the last collection and that are known to be referenced by the current application domain.</summary>
		/// <returns>The number of surviving bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The static (Shared in Visual Basic) <see cref="P:System.AppDomain.MonitoringIsEnabled" /> property is set to false.</exception>
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public long MonitoringSurvivedMemorySize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the total bytes that survived from the last collection for all application domains in the process.</summary>
		/// <returns>The total number of surviving bytes for the process.</returns>
		/// <exception cref="T:System.InvalidOperationException">The static (Shared in Visual Basic) <see cref="P:System.AppDomain.MonitoringIsEnabled" /> property is set to false.</exception>
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public static long MonitoringSurvivedProcessMemorySize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the total size, in bytes, of all memory allocations that have been made by the application domain since it was created, without subtracting memory that has been collected. </summary>
		/// <returns>The total size of all memory allocations.</returns>
		/// <exception cref="T:System.InvalidOperationException">The static (Shared in Visual Basic) <see cref="P:System.AppDomain.MonitoringIsEnabled" /> property is set to false.</exception>
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public long MonitoringTotalAllocatedMemorySize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the total processor time that has been used by all threads while executing in the current application domain, since the process started.</summary>
		/// <returns>Total processor time for the current application domain.</returns>
		/// <exception cref="T:System.InvalidOperationException">The static (Shared in Visual Basic) <see cref="P:System.AppDomain.MonitoringIsEnabled" /> property is set to false.</exception>
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public TimeSpan MonitoringTotalProcessorTime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x04000BF2 RID: 3058
		private IntPtr _mono_app_domain;

		// Token: 0x04000BF3 RID: 3059
		private static string _process_guid;

		// Token: 0x04000BF4 RID: 3060
		[ThreadStatic]
		private static Dictionary<string, object> type_resolve_in_progress;

		// Token: 0x04000BF5 RID: 3061
		[ThreadStatic]
		private static Dictionary<string, object> assembly_resolve_in_progress;

		// Token: 0x04000BF6 RID: 3062
		[ThreadStatic]
		private static Dictionary<string, object> assembly_resolve_in_progress_refonly;

		// Token: 0x04000BF7 RID: 3063
		private Evidence _evidence;

		// Token: 0x04000BF8 RID: 3064
		private PermissionSet _granted;

		// Token: 0x04000BF9 RID: 3065
		private PrincipalPolicy _principalPolicy;

		// Token: 0x04000BFA RID: 3066
		[ThreadStatic]
		private static IPrincipal _principal;

		// Token: 0x04000BFB RID: 3067
		private static AppDomain default_domain;

		// Token: 0x04000C04 RID: 3076
		private AppDomainManager _domain_manager;

		// Token: 0x04000C06 RID: 3078
		private ActivationContext _activation;

		// Token: 0x04000C07 RID: 3079
		private ApplicationIdentity _applicationIdentity;

		// Token: 0x04000C08 RID: 3080
		private List<string> compatibility_switch;

		// Token: 0x020001F0 RID: 496
		[Serializable]
		private class Loader
		{
			// Token: 0x0600175B RID: 5979 RVA: 0x0005B8E9 File Offset: 0x00059AE9
			public Loader(string assembly)
			{
				this.assembly = assembly;
			}

			// Token: 0x0600175C RID: 5980 RVA: 0x0005B8F8 File Offset: 0x00059AF8
			public void Load()
			{
				Assembly.LoadFrom(this.assembly);
			}

			// Token: 0x04000C09 RID: 3081
			private string assembly;
		}

		// Token: 0x020001F1 RID: 497
		[Serializable]
		private class Initializer
		{
			// Token: 0x0600175D RID: 5981 RVA: 0x0005B906 File Offset: 0x00059B06
			public Initializer(AppDomainInitializer initializer, string[] arguments)
			{
				this.initializer = initializer;
				this.arguments = arguments;
			}

			// Token: 0x0600175E RID: 5982 RVA: 0x0005B91C File Offset: 0x00059B1C
			public void Initialize()
			{
				this.initializer(this.arguments);
			}

			// Token: 0x04000C0A RID: 3082
			private AppDomainInitializer initializer;

			// Token: 0x04000C0B RID: 3083
			private string[] arguments;
		}
	}
}
