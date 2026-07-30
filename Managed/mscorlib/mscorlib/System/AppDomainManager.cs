using System;
using System.Reflection;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading;

namespace System
{
	/// <summary>Provides a managed equivalent of an unmanaged host.</summary>
	/// <exception cref="T:System.Security.SecurityException">The caller does not have the correct permissions. See the Requirements section.</exception>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000202 RID: 514
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Infrastructure = true)]
	public class AppDomainManager : MarshalByRefObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AppDomainManager" /> class. </summary>
		// Token: 0x060017D7 RID: 6103 RVA: 0x0005CEF0 File Offset: 0x0005B0F0
		public AppDomainManager()
		{
			this._flags = AppDomainManagerInitializationOptions.None;
		}

		/// <summary>Gets the application activator that handles the activation of add-ins and manifest-based applications for the domain.</summary>
		/// <returns>The application activator.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x0005CEFF File Offset: 0x0005B0FF
		public virtual ApplicationActivator ApplicationActivator
		{
			get
			{
				if (this._activator == null)
				{
					this._activator = new ApplicationActivator();
				}
				return this._activator;
			}
		}

		/// <summary>Gets the entry assembly for an application.</summary>
		/// <returns>The entry assembly for the application.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x0005CF1A File Offset: 0x0005B11A
		public virtual Assembly EntryAssembly
		{
			get
			{
				return Assembly.GetEntryAssembly();
			}
		}

		/// <summary>Gets the host execution context manager that manages the flow of the execution context.</summary>
		/// <returns>The host execution context manager.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public virtual HostExecutionContextManager HostExecutionContextManager
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the host security manager that participates in security decisions for the application domain.</summary>
		/// <returns>The host security manager.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x0000A42E File Offset: 0x0000862E
		public virtual HostSecurityManager HostSecurityManager
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the initialization flags for custom application domain managers.</summary>
		/// <returns>A bitwise combination of the enumeration values that describe the initialization action to perform. The default is <see cref="F:System.AppDomainManagerInitializationOptions.None" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x0005CF21 File Offset: 0x0005B121
		// (set) Token: 0x060017DD RID: 6109 RVA: 0x0005CF29 File Offset: 0x0005B129
		public AppDomainManagerInitializationOptions InitializationFlags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		/// <summary>Returns a new or existing application domain.</summary>
		/// <returns>A new or existing application domain.</returns>
		/// <param name="friendlyName">The friendly name of the domain. </param>
		/// <param name="securityInfo">An object that contains evidence mapped through the security policy to establish a top-of-stack permission set.</param>
		/// <param name="appDomainInfo">An object that contains application domain initialization information.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlAppDomain, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060017DE RID: 6110 RVA: 0x0005CF34 File Offset: 0x0005B134
		public virtual AppDomain CreateDomain(string friendlyName, Evidence securityInfo, AppDomainSetup appDomainInfo)
		{
			this.InitializeNewDomain(appDomainInfo);
			AppDomain appDomain = AppDomainManager.CreateDomainHelper(friendlyName, securityInfo, appDomainInfo);
			if ((this.HostSecurityManager.Flags & HostSecurityManagerOptions.HostPolicyLevel) == HostSecurityManagerOptions.HostPolicyLevel)
			{
				PolicyLevel domainPolicy = this.HostSecurityManager.DomainPolicy;
				if (domainPolicy != null)
				{
					appDomain.SetAppDomainPolicy(domainPolicy);
				}
			}
			return appDomain;
		}

		/// <summary>Initializes the new application domain.</summary>
		/// <param name="appDomainInfo">An object that contains application domain initialization information.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060017DF RID: 6111 RVA: 0x00002194 File Offset: 0x00000394
		public virtual void InitializeNewDomain(AppDomainSetup appDomainInfo)
		{
		}

		/// <summary>Indicates whether the specified operation is allowed in the application domain.</summary>
		/// <returns>true if the host allows the operation specified by <paramref name="state" /> to be performed in the application domain; otherwise, false.</returns>
		/// <param name="state">A subclass of <see cref="T:System.Security.SecurityState" /> that identifies the operation whose security status is requested.</param>
		// Token: 0x060017E0 RID: 6112 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool CheckSecuritySettings(SecurityState state)
		{
			return false;
		}

		/// <summary>Provides a helper method to create an application domain.</summary>
		/// <returns>A newly created application domain.</returns>
		/// <param name="friendlyName">The friendly name of the domain. </param>
		/// <param name="securityInfo">An object that contains evidence mapped through the security policy to establish a top-of-stack permission set.</param>
		/// <param name="appDomainInfo">An object that contains application domain initialization information.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="friendlyName" /> is null. </exception>
		// Token: 0x060017E1 RID: 6113 RVA: 0x0005CF78 File Offset: 0x0005B178
		protected static AppDomain CreateDomainHelper(string friendlyName, Evidence securityInfo, AppDomainSetup appDomainInfo)
		{
			return AppDomain.CreateDomain(friendlyName, securityInfo, appDomainInfo);
		}

		// Token: 0x04000C5F RID: 3167
		private ApplicationActivator _activator;

		// Token: 0x04000C60 RID: 3168
		private AppDomainManagerInitializationOptions _flags;
	}
}
