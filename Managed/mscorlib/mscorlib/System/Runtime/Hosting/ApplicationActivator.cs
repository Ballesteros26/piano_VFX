using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Policy;

namespace System.Runtime.Hosting
{
	/// <summary>Provides the base class for the activation of manifest-based assemblies. </summary>
	// Token: 0x020006BA RID: 1722
	[MonoTODO("missing manifest support")]
	[ComVisible(true)]
	public class ApplicationActivator
	{
		/// <summary>Creates an instance of the application to be activated, using the specified activation context. </summary>
		/// <returns>An <see cref="T:System.Runtime.Remoting.ObjectHandle" /> that is a wrapper for the return value of the application execution. The return value must be unwrapped to access the real object.  </returns>
		/// <param name="activationContext">An <see cref="T:System.ActivationContext" /> that identifies the application to activate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationContext" /> is null. </exception>
		// Token: 0x06004973 RID: 18803 RVA: 0x00107AC2 File Offset: 0x00105CC2
		public virtual ObjectHandle CreateInstance(ActivationContext activationContext)
		{
			return this.CreateInstance(activationContext, null);
		}

		/// <summary>Creates an instance of the application to be activated, using the specified activation context  and custom activation data.  </summary>
		/// <returns>An <see cref="T:System.Runtime.Remoting.ObjectHandle" /> that is a wrapper for the return value of the application execution. The return value must be unwrapped to access the real object.</returns>
		/// <param name="activationContext">An <see cref="T:System.ActivationContext" /> that identifies the application to activate.</param>
		/// <param name="activationCustomData">Custom activation data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationContext" /> is null. </exception>
		// Token: 0x06004974 RID: 18804 RVA: 0x00107ACC File Offset: 0x00105CCC
		public virtual ObjectHandle CreateInstance(ActivationContext activationContext, string[] activationCustomData)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			return ApplicationActivator.CreateInstanceHelper(new AppDomainSetup(activationContext));
		}

		/// <summary>Creates an instance of an application using the specified <see cref="T:System.AppDomainSetup" />  object.</summary>
		/// <returns>An <see cref="T:System.Runtime.Remoting.ObjectHandle" /> that is a wrapper for the return value of the application execution. The return value must be unwrapped to access the real object. </returns>
		/// <param name="adSetup">An <see cref="T:System.AppDomainSetup" /> object whose <see cref="P:System.AppDomainSetup.ActivationArguments" /> property identifies the application to activate.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.AppDomainSetup.ActivationArguments" /> property of <paramref name="adSetup " />is null. </exception>
		/// <exception cref="T:System.Security.Policy.PolicyException">The application instance failed to execute because the policy settings on the current application domain do not provide permission for this application to run.</exception>
		// Token: 0x06004975 RID: 18805 RVA: 0x00107AE8 File Offset: 0x00105CE8
		protected static ObjectHandle CreateInstanceHelper(AppDomainSetup adSetup)
		{
			if (adSetup == null)
			{
				throw new ArgumentNullException("adSetup");
			}
			if (adSetup.ActivationArguments == null)
			{
				throw new ArgumentException(string.Format(Locale.GetText("{0} is missing it's {1} property"), "AppDomainSetup", "ActivationArguments"), "adSetup");
			}
			HostSecurityManager hostSecurityManager;
			if (AppDomain.CurrentDomain.DomainManager != null)
			{
				hostSecurityManager = AppDomain.CurrentDomain.DomainManager.HostSecurityManager;
			}
			else
			{
				hostSecurityManager = new HostSecurityManager();
			}
			Evidence evidence = new Evidence();
			evidence.AddHost(adSetup.ActivationArguments);
			TrustManagerContext trustManagerContext = new TrustManagerContext();
			if (!hostSecurityManager.DetermineApplicationTrust(evidence, null, trustManagerContext).IsApplicationTrustedToRun)
			{
				throw new PolicyException(Locale.GetText("Current policy doesn't allow execution of addin."));
			}
			return AppDomain.CreateDomain("friendlyName", null, adSetup).CreateInstance("assemblyName", "typeName", null);
		}
	}
}
