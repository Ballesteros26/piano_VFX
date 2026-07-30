using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Manages trust decisions for manifest-activated applications. </summary>
	// Token: 0x02000558 RID: 1368
	[ComVisible(true)]
	public static class ApplicationSecurityManager
	{
		/// <summary>Gets the current application trust manager.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.IApplicationTrustManager" /> that represents the current trust manager.</returns>
		/// <exception cref="T:System.Security.Policy.PolicyException">The policy on this application does not have a trust manager.</exception>
		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06003D83 RID: 15747 RVA: 0x000DCEA5 File Offset: 0x000DB0A5
		public static IApplicationTrustManager ApplicationTrustManager
		{
			[SecurityPermission(SecurityAction.Demand, ControlPolicy = true)]
			get
			{
				if (ApplicationSecurityManager._appTrustManager == null)
				{
					ApplicationSecurityManager._appTrustManager = new MonoTrustManager();
				}
				return ApplicationSecurityManager._appTrustManager;
			}
		}

		/// <summary>Gets an application trust collection that contains the cached trust decisions for the user.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> that contains the cached trust decisions for the user.</returns>
		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06003D84 RID: 15748 RVA: 0x000DCEBD File Offset: 0x000DB0BD
		public static ApplicationTrustCollection UserApplicationTrusts
		{
			get
			{
				if (ApplicationSecurityManager._userAppTrusts == null)
				{
					ApplicationSecurityManager._userAppTrusts = new ApplicationTrustCollection();
				}
				return ApplicationSecurityManager._userAppTrusts;
			}
		}

		/// <summary>Determines whether the user approves the specified application to execute with the requested permission set.</summary>
		/// <returns>true to execute the specified application; otherwise, false.</returns>
		/// <param name="activationContext">An <see cref="T:System.ActivationContext" /> identifying the activation context for the application.</param>
		/// <param name="context">A <see cref="T:System.Security.Policy.TrustManagerContext" />  identifying the trust manager context for the application.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="activationContext" /> parameter is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003D85 RID: 15749 RVA: 0x000DCED5 File Offset: 0x000DB0D5
		[MonoTODO("Missing application manifest support")]
		[SecurityPermission(SecurityAction.Demand, ControlPolicy = true, ControlEvidence = true)]
		public static bool DetermineApplicationTrust(ActivationContext activationContext, TrustManagerContext context)
		{
			if (activationContext == null)
			{
				throw new NullReferenceException("activationContext");
			}
			return ApplicationSecurityManager.ApplicationTrustManager.DetermineApplicationTrust(activationContext, context).IsApplicationTrustedToRun;
		}

		// Token: 0x04001F99 RID: 8089
		private static IApplicationTrustManager _appTrustManager;

		// Token: 0x04001F9A RID: 8090
		private static ApplicationTrustCollection _userAppTrusts;
	}
}
