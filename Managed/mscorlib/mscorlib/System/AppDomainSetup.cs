using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Policy;
using Unity;

namespace System
{
	/// <summary>Represents assembly binding information that can be added to an instance of <see cref="T:System.AppDomain" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000203 RID: 515
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class AppDomainSetup : IAppDomainSetup
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainSetup" /> class.</summary>
		// Token: 0x060017E2 RID: 6114 RVA: 0x00002111 File Offset: 0x00000311
		public AppDomainSetup()
		{
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x0005CF84 File Offset: 0x0005B184
		internal AppDomainSetup(AppDomainSetup setup)
		{
			this.application_base = setup.application_base;
			this.application_name = setup.application_name;
			this.cache_path = setup.cache_path;
			this.configuration_file = setup.configuration_file;
			this.dynamic_base = setup.dynamic_base;
			this.license_file = setup.license_file;
			this.private_bin_path = setup.private_bin_path;
			this.private_bin_path_probe = setup.private_bin_path_probe;
			this.shadow_copy_directories = setup.shadow_copy_directories;
			this.shadow_copy_files = setup.shadow_copy_files;
			this.publisher_policy = setup.publisher_policy;
			this.path_changed = setup.path_changed;
			this.loader_optimization = setup.loader_optimization;
			this.disallow_binding_redirects = setup.disallow_binding_redirects;
			this.disallow_code_downloads = setup.disallow_code_downloads;
			this._activationArguments = setup._activationArguments;
			this.domain_initializer = setup.domain_initializer;
			this.application_trust = setup.application_trust;
			this.domain_initializer_args = setup.domain_initializer_args;
			this.disallow_appbase_probe = setup.disallow_appbase_probe;
			this.configuration_bytes = setup.configuration_bytes;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainSetup" /> class with the specified activation arguments required for manifest-based activation of an application domain.</summary>
		/// <param name="activationArguments">An object that specifies information required for the manifest-based activation of a new application domain.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationArguments" /> is null.</exception>
		// Token: 0x060017E4 RID: 6116 RVA: 0x0005D093 File Offset: 0x0005B293
		public AppDomainSetup(ActivationArguments activationArguments)
		{
			this._activationArguments = activationArguments;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainSetup" /> class with the specified activation context to use for manifest-based activation of an application domain.</summary>
		/// <param name="activationContext">The activation context to be used for an application domain.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationContext" /> is null.</exception>
		// Token: 0x060017E5 RID: 6117 RVA: 0x0005D0A2 File Offset: 0x0005B2A2
		public AppDomainSetup(ActivationContext activationContext)
		{
			this._activationArguments = new ActivationArguments(activationContext);
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x0005D0B8 File Offset: 0x0005B2B8
		private static string GetAppBase(string appBase)
		{
			if (appBase == null)
			{
				return null;
			}
			if (appBase.Length >= 8 && appBase.ToLower().StartsWith("file://"))
			{
				appBase = appBase.Substring(7);
				if (Path.DirectorySeparatorChar != '/')
				{
					appBase = appBase.Replace('/', Path.DirectorySeparatorChar);
				}
			}
			appBase = Path.GetFullPath(appBase);
			if (Path.DirectorySeparatorChar != '/')
			{
				bool flag = appBase.StartsWith("\\\\?\\", StringComparison.Ordinal);
				if (appBase.IndexOf(':', flag ? 6 : 2) != -1)
				{
					throw new NotSupportedException("The given path's format is not supported.");
				}
			}
			string directoryName = Path.GetDirectoryName(appBase);
			if (directoryName != null && directoryName.LastIndexOfAny(Path.GetInvalidPathChars()) >= 0)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid path characters in path: '{0}'"), appBase), "appBase");
			}
			string fileName = Path.GetFileName(appBase);
			if (fileName != null && fileName.LastIndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid filename characters in path: '{0}'"), appBase), "appBase");
			}
			return appBase;
		}

		/// <summary>Gets or sets the name of the directory containing the application.</summary>
		/// <returns>The name of the application base directory.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x0005D1A8 File Offset: 0x0005B3A8
		// (set) Token: 0x060017E8 RID: 6120 RVA: 0x0005D1B5 File Offset: 0x0005B3B5
		public string ApplicationBase
		{
			[SecuritySafeCritical]
			get
			{
				return AppDomainSetup.GetAppBase(this.application_base);
			}
			set
			{
				this.application_base = value;
			}
		}

		/// <summary>Gets or sets the name of the application.</summary>
		/// <returns>The name of the application.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x0005D1BE File Offset: 0x0005B3BE
		// (set) Token: 0x060017EA RID: 6122 RVA: 0x0005D1C6 File Offset: 0x0005B3C6
		public string ApplicationName
		{
			get
			{
				return this.application_name;
			}
			set
			{
				this.application_name = value;
			}
		}

		/// <summary>Gets or sets the name of an area specific to the application where files are shadow copied. </summary>
		/// <returns>The fully qualified name of the directory path and file name where files are shadow copied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x0005D1CF File Offset: 0x0005B3CF
		// (set) Token: 0x060017EC RID: 6124 RVA: 0x0005D1D7 File Offset: 0x0005B3D7
		public string CachePath
		{
			[SecuritySafeCritical]
			get
			{
				return this.cache_path;
			}
			set
			{
				this.cache_path = value;
			}
		}

		/// <summary>Gets or sets the name of the configuration file for an application domain.</summary>
		/// <returns>The name of the configuration file.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x0005D1E0 File Offset: 0x0005B3E0
		// (set) Token: 0x060017EE RID: 6126 RVA: 0x0005D22F File Offset: 0x0005B42F
		public string ConfigurationFile
		{
			[SecuritySafeCritical]
			get
			{
				if (this.configuration_file == null)
				{
					return null;
				}
				if (Path.IsPathRooted(this.configuration_file))
				{
					return this.configuration_file;
				}
				if (this.ApplicationBase == null)
				{
					throw new MemberAccessException("The ApplicationBase must be set before retrieving this property.");
				}
				return Path.Combine(this.ApplicationBase, this.configuration_file);
			}
			set
			{
				this.configuration_file = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the &lt;publisherPolicy&gt; section of the configuration file is applied to an application domain.</summary>
		/// <returns>true if the &lt;publisherPolicy&gt; section of the configuration file for an application domain is ignored; false if the declared publisher policy is honored.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x0005D238 File Offset: 0x0005B438
		// (set) Token: 0x060017F0 RID: 6128 RVA: 0x0005D240 File Offset: 0x0005B440
		public bool DisallowPublisherPolicy
		{
			get
			{
				return this.publisher_policy;
			}
			set
			{
				this.publisher_policy = value;
			}
		}

		/// <summary>Gets or sets the base directory where the directory for dynamically generated files is located.</summary>
		/// <returns>The directory where the <see cref="P:System.AppDomain.DynamicDirectory" /> is located.NoteThe return value of this property is different from the value assigned. See the Remarks section.</returns>
		/// <exception cref="T:System.MemberAccessException">This property cannot be set because the application name on the application domain is null.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x0005D24C File Offset: 0x0005B44C
		// (set) Token: 0x060017F2 RID: 6130 RVA: 0x0005D29C File Offset: 0x0005B49C
		public string DynamicBase
		{
			[SecuritySafeCritical]
			get
			{
				if (this.dynamic_base == null)
				{
					return null;
				}
				if (Path.IsPathRooted(this.dynamic_base))
				{
					return this.dynamic_base;
				}
				if (this.ApplicationBase == null)
				{
					throw new MemberAccessException("The ApplicationBase must be set before retrieving this property.");
				}
				return Path.Combine(this.ApplicationBase, this.dynamic_base);
			}
			[SecuritySafeCritical]
			set
			{
				if (this.application_name == null)
				{
					throw new MemberAccessException("ApplicationName must be set before the DynamicBase can be set.");
				}
				this.dynamic_base = Path.Combine(value, ((uint)this.application_name.GetHashCode()).ToString("x"));
			}
		}

		/// <summary>Gets or sets the location of the license file associated with this domain.</summary>
		/// <returns>The location and name of the license file.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x0005D2E0 File Offset: 0x0005B4E0
		// (set) Token: 0x060017F4 RID: 6132 RVA: 0x0005D2E8 File Offset: 0x0005B4E8
		public string LicenseFile
		{
			[SecuritySafeCritical]
			get
			{
				return this.license_file;
			}
			set
			{
				this.license_file = value;
			}
		}

		/// <summary>Specifies the optimization policy used to load an executable.</summary>
		/// <returns>An enumerated constant that is used with the <see cref="T:System.LoaderOptimizationAttribute" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x0005D2F1 File Offset: 0x0005B4F1
		// (set) Token: 0x060017F6 RID: 6134 RVA: 0x0005D2F9 File Offset: 0x0005B4F9
		[MonoLimitation("In Mono this is controlled by the --share-code flag")]
		public LoaderOptimization LoaderOptimization
		{
			get
			{
				return this.loader_optimization;
			}
			set
			{
				this.loader_optimization = value;
			}
		}

		/// <summary>Gets or sets the list of directories under the application base directory that are probed for private assemblies.</summary>
		/// <returns>A list of directory names separated by semicolons.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x0005D302 File Offset: 0x0005B502
		// (set) Token: 0x060017F8 RID: 6136 RVA: 0x0005D30A File Offset: 0x0005B50A
		public string PrivateBinPath
		{
			[SecuritySafeCritical]
			get
			{
				return this.private_bin_path;
			}
			set
			{
				this.private_bin_path = value;
				this.path_changed = true;
			}
		}

		/// <summary>Gets or sets a string value that includes or excludes <see cref="P:System.AppDomainSetup.ApplicationBase" /> from the search path for the application, and searches only <see cref="P:System.AppDomainSetup.PrivateBinPath" />.</summary>
		/// <returns>A null reference (Nothing in Visual Basic) to include the application base path when searching for assemblies; any non-null string value to exclude the path. The default value is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x0005D31A File Offset: 0x0005B51A
		// (set) Token: 0x060017FA RID: 6138 RVA: 0x0005D322 File Offset: 0x0005B522
		public string PrivateBinPathProbe
		{
			get
			{
				return this.private_bin_path_probe;
			}
			set
			{
				this.private_bin_path_probe = value;
				this.path_changed = true;
			}
		}

		/// <summary>Gets or sets the names of the directories containing assemblies to be shadow copied.</summary>
		/// <returns>A list of directory names separated by semicolons.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x0005D332 File Offset: 0x0005B532
		// (set) Token: 0x060017FC RID: 6140 RVA: 0x0005D33A File Offset: 0x0005B53A
		public string ShadowCopyDirectories
		{
			[SecuritySafeCritical]
			get
			{
				return this.shadow_copy_directories;
			}
			set
			{
				this.shadow_copy_directories = value;
			}
		}

		/// <summary>Gets or sets a string that indicates whether shadow copying is turned on or off.</summary>
		/// <returns>The string value "true" to indicate that shadow copying is turned on; or "false" to indicate that shadow copying is turned off.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x0005D343 File Offset: 0x0005B543
		// (set) Token: 0x060017FE RID: 6142 RVA: 0x0005D34B File Offset: 0x0005B54B
		public string ShadowCopyFiles
		{
			get
			{
				return this.shadow_copy_files;
			}
			set
			{
				this.shadow_copy_files = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether an application domain allows assembly binding redirection.</summary>
		/// <returns>true if redirection of assemblies is not allowed; false if it is allowed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x0005D354 File Offset: 0x0005B554
		// (set) Token: 0x06001800 RID: 6144 RVA: 0x0005D35C File Offset: 0x0005B55C
		public bool DisallowBindingRedirects
		{
			get
			{
				return this.disallow_binding_redirects;
			}
			set
			{
				this.disallow_binding_redirects = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether HTTP download of assemblies is allowed for an application domain.</summary>
		/// <returns>true if HTTP download of assemblies is not allowed; false if it is allowed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x0005D365 File Offset: 0x0005B565
		// (set) Token: 0x06001802 RID: 6146 RVA: 0x0005D36D File Offset: 0x0005B56D
		public bool DisallowCodeDownload
		{
			get
			{
				return this.disallow_code_downloads;
			}
			set
			{
				this.disallow_code_downloads = value;
			}
		}

		/// <summary>Gets or sets a string that specifies the target version and profile of the .NET Framework for the application domain, in a format that can be parsed by the <see cref="M:System.Runtime.Versioning.FrameworkName.#ctor(System.String)" /> constructor. </summary>
		/// <returns>The target version and profile of the .NET Framework. </returns>
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x0005D376 File Offset: 0x0005B576
		// (set) Token: 0x06001804 RID: 6148 RVA: 0x0005D37E File Offset: 0x0005B57E
		public string TargetFrameworkName { get; set; }

		/// <summary>Gets or sets data about the activation of an application domain.</summary>
		/// <returns>An object that contains data about the activation of an application domain.</returns>
		/// <exception cref="T:System.InvalidOperationException">The property is set to an <see cref="T:System.Runtime.Hosting.ActivationArguments" /> object whose application identity does not match the application identity of the <see cref="T:System.Security.Policy.ApplicationTrust" /> object returned by the <see cref="P:System.AppDomainSetup.ApplicationTrust" /> property. No exception is thrown if the <see cref="P:System.AppDomainSetup.ApplicationTrust" /> property is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x0005D387 File Offset: 0x0005B587
		// (set) Token: 0x06001806 RID: 6150 RVA: 0x0005D3A4 File Offset: 0x0005B5A4
		public ActivationArguments ActivationArguments
		{
			get
			{
				if (this._activationArguments != null)
				{
					return this._activationArguments;
				}
				this.DeserializeNonPrimitives();
				return this._activationArguments;
			}
			set
			{
				this._activationArguments = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.AppDomainInitializer" /> delegate, which represents a callback method that is invoked when the application domain is initialized.</summary>
		/// <returns>A delegate that represents a callback method that is invoked when the application domain is initialized.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x0005D3AD File Offset: 0x0005B5AD
		// (set) Token: 0x06001808 RID: 6152 RVA: 0x0005D3CA File Offset: 0x0005B5CA
		[MonoLimitation("it needs to be invoked within the created domain")]
		public AppDomainInitializer AppDomainInitializer
		{
			get
			{
				if (this.domain_initializer != null)
				{
					return this.domain_initializer;
				}
				this.DeserializeNonPrimitives();
				return this.domain_initializer;
			}
			set
			{
				this.domain_initializer = value;
			}
		}

		/// <summary>Gets or sets the arguments passed to the callback method represented by the <see cref="T:System.AppDomainInitializer" /> delegate. The callback method is invoked when the application domain is initialized.</summary>
		/// <returns>An array of strings that is passed to the callback method represented by the <see cref="T:System.AppDomainInitializer" /> delegate, when the callback method is invoked during <see cref="T:System.AppDomain" /> initialization.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x0005D3D3 File Offset: 0x0005B5D3
		// (set) Token: 0x0600180A RID: 6154 RVA: 0x0005D3DB File Offset: 0x0005B5DB
		[MonoLimitation("it needs to be used to invoke the initializer within the created domain")]
		public string[] AppDomainInitializerArguments
		{
			get
			{
				return this.domain_initializer_args;
			}
			set
			{
				this.domain_initializer_args = value;
			}
		}

		/// <summary>Gets or sets an object containing security and trust information.</summary>
		/// <returns>An object that contains security and trust information. </returns>
		/// <exception cref="T:System.InvalidOperationException">The property is set to an <see cref="T:System.Security.Policy.ApplicationTrust" /> object whose application identity does not match the application identity of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> object returned by the <see cref="P:System.AppDomainSetup.ActivationArguments" /> property. No exception is thrown if the <see cref="P:System.AppDomainSetup.ActivationArguments" /> property is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The property is set to null.</exception>
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x0005D3E4 File Offset: 0x0005B5E4
		// (set) Token: 0x0600180C RID: 6156 RVA: 0x0005D414 File Offset: 0x0005B614
		[MonoNotSupported("This property exists but not considered.")]
		public ApplicationTrust ApplicationTrust
		{
			get
			{
				if (this.application_trust != null)
				{
					return this.application_trust;
				}
				this.DeserializeNonPrimitives();
				if (this.application_trust == null)
				{
					this.application_trust = new ApplicationTrust();
				}
				return this.application_trust;
			}
			set
			{
				this.application_trust = value;
			}
		}

		/// <summary>Specifies whether the application base path and private binary path are probed when searching for assemblies to load.</summary>
		/// <returns>true if probing is not allowed; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x0005D41D File Offset: 0x0005B61D
		// (set) Token: 0x0600180E RID: 6158 RVA: 0x0005D425 File Offset: 0x0005B625
		[MonoNotSupported("This property exists but not considered.")]
		public bool DisallowApplicationBaseProbing
		{
			get
			{
				return this.disallow_appbase_probe;
			}
			set
			{
				this.disallow_appbase_probe = value;
			}
		}

		/// <summary>Returns the XML configuration information set by the <see cref="M:System.AppDomainSetup.SetConfigurationBytes(System.Byte[])" /> method, which overrides the application's XML configuration information.</summary>
		/// <returns>An array that contains the XML configuration information that was set by the <see cref="M:System.AppDomainSetup.SetConfigurationBytes(System.Byte[])" /> method, or null if the <see cref="M:System.AppDomainSetup.SetConfigurationBytes(System.Byte[])" /> method has not been called.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600180F RID: 6159 RVA: 0x0005D42E File Offset: 0x0005B62E
		[MonoNotSupported("This method exists but not considered.")]
		public byte[] GetConfigurationBytes()
		{
			if (this.configuration_bytes == null)
			{
				return null;
			}
			return this.configuration_bytes.Clone() as byte[];
		}

		/// <summary>Provides XML configuration information for the application domain, replacing the application's XML configuration information.</summary>
		/// <param name="value">An array that contains the XML configuration information to be used for the application domain.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001810 RID: 6160 RVA: 0x0005D44A File Offset: 0x0005B64A
		[MonoNotSupported("This method exists but not considered.")]
		public void SetConfigurationBytes(byte[] value)
		{
			this.configuration_bytes = value;
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x0005D454 File Offset: 0x0005B654
		private void DeserializeNonPrimitives()
		{
			lock (this)
			{
				if (this.serialized_non_primitives != null)
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					MemoryStream memoryStream = new MemoryStream(this.serialized_non_primitives);
					object[] array = (object[])binaryFormatter.Deserialize(memoryStream);
					this._activationArguments = (ActivationArguments)array[0];
					this.domain_initializer = (AppDomainInitializer)array[1];
					this.application_trust = (ApplicationTrust)array[2];
					this.serialized_non_primitives = null;
				}
			}
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0005D4E4 File Offset: 0x0005B6E4
		internal void SerializeNonPrimitives()
		{
			object[] array = new object[] { this._activationArguments, this.domain_initializer, this.application_trust };
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();
			binaryFormatter.Serialize(memoryStream, array);
			this.serialized_non_primitives = memoryStream.ToArray();
		}

		/// <summary>Sets the specified switches, making the application domain compatible with previous versions of the .NET Framework for the specified issues.</summary>
		/// <param name="switches">An enumerable set of string values that specify compatibility switches, or null to erase the existing compatibility switches.</param>
		// Token: 0x06001813 RID: 6163 RVA: 0x00002194 File Offset: 0x00000394
		[MonoTODO("not implemented, does not throw because it's used in testing moonlight")]
		public void SetCompatibilitySwitches(IEnumerable<string> switches)
		{
		}

		/// <summary>Gets or sets the display name of the assembly that provides the type of the application domain manager for application domains created using this <see cref="T:System.AppDomainSetup" /> object.</summary>
		/// <returns>The display name of the assembly that provides the <see cref="T:System.Type" /> of the application domain manager.</returns>
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x00032521 File Offset: 0x00030721
		// (set) Token: 0x06001815 RID: 6165 RVA: 0x0001FB35 File Offset: 0x0001DD35
		public string AppDomainManagerAssembly
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the full name of the type that provides the application domain manager for application domains created using this <see cref="T:System.AppDomainSetup" /> object.</summary>
		/// <returns>The full name of the type, including the namespace.</returns>
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x00032521 File Offset: 0x00030721
		// (set) Token: 0x06001817 RID: 6167 RVA: 0x0001FB35 File Offset: 0x0001DD35
		public string AppDomainManagerType
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a list of assemblies marked with the <see cref="F:System.Security.PartialTrustVisibilityLevel.NotVisibleByDefault" /> flag that are made visible to partial-trust code running in a sandboxed application domain. </summary>
		/// <returns>An array of partial assembly names, where each partial name consists of the simple assembly name and the public key.</returns>
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x00032521 File Offset: 0x00030721
		// (set) Token: 0x06001819 RID: 6169 RVA: 0x0001FB35 File Offset: 0x0001DD35
		public string[] PartialTrustVisibleAssemblies
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether interface caching is disabled for interop calls in the application domain, so that a QueryInterface is performed on each call.</summary>
		/// <returns>true if interface caching is disabled for interop calls in application domains created with the current <see cref="T:System.AppDomainSetup" /> object; otherwise, false.</returns>
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x0005D534 File Offset: 0x0005B734
		// (set) Token: 0x0600181B RID: 6171 RVA: 0x0001FB35 File Offset: 0x0001DD35
		public bool SandboxInterop
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Provides the common language runtime with an alternate implementation of a string comparison function. </summary>
		/// <param name="functionName">The name of the string comparison function to override.</param>
		/// <param name="functionVersion">The function version. For .NET Framework 4.5, its value must be 1 or greater.</param>
		/// <param name="functionPointer">A pointer to the function that overrides <paramref name="functionName" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="functionName" /> is null. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="functionVersion" /> is not 1 or greater.-or-<paramref name="functionPointer" /> is <see cref="F:System.IntPtr.Zero" />. </exception>
		// Token: 0x0600181C RID: 6172 RVA: 0x0001FB35 File Offset: 0x0001DD35
		[SecurityCritical]
		public void SetNativeFunction(string functionName, int functionVersion, IntPtr functionPointer)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000C61 RID: 3169
		private string application_base;

		// Token: 0x04000C62 RID: 3170
		private string application_name;

		// Token: 0x04000C63 RID: 3171
		private string cache_path;

		// Token: 0x04000C64 RID: 3172
		private string configuration_file;

		// Token: 0x04000C65 RID: 3173
		private string dynamic_base;

		// Token: 0x04000C66 RID: 3174
		private string license_file;

		// Token: 0x04000C67 RID: 3175
		private string private_bin_path;

		// Token: 0x04000C68 RID: 3176
		private string private_bin_path_probe;

		// Token: 0x04000C69 RID: 3177
		private string shadow_copy_directories;

		// Token: 0x04000C6A RID: 3178
		private string shadow_copy_files;

		// Token: 0x04000C6B RID: 3179
		private bool publisher_policy;

		// Token: 0x04000C6C RID: 3180
		private bool path_changed;

		// Token: 0x04000C6D RID: 3181
		private LoaderOptimization loader_optimization;

		// Token: 0x04000C6E RID: 3182
		private bool disallow_binding_redirects;

		// Token: 0x04000C6F RID: 3183
		private bool disallow_code_downloads;

		// Token: 0x04000C70 RID: 3184
		private ActivationArguments _activationArguments;

		// Token: 0x04000C71 RID: 3185
		private AppDomainInitializer domain_initializer;

		// Token: 0x04000C72 RID: 3186
		[NonSerialized]
		private ApplicationTrust application_trust;

		// Token: 0x04000C73 RID: 3187
		private string[] domain_initializer_args;

		// Token: 0x04000C74 RID: 3188
		private bool disallow_appbase_probe;

		// Token: 0x04000C75 RID: 3189
		private byte[] configuration_bytes;

		// Token: 0x04000C76 RID: 3190
		private byte[] serialized_non_primitives;
	}
}
