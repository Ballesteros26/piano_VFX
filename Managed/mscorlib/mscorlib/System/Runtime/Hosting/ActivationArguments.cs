using System;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Runtime.Hosting
{
	/// <summary>Provides data for manifest-based activation of an application. This class cannot be inherited. </summary>
	// Token: 0x020006B9 RID: 1721
	[ComVisible(true)]
	[Serializable]
	public sealed class ActivationArguments : EvidenceBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> class with the specified activation context. </summary>
		/// <param name="activationData">An object that identifies the manifest-based activation application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationData" /> is null.</exception>
		// Token: 0x0600496B RID: 18795 RVA: 0x00107A10 File Offset: 0x00105C10
		public ActivationArguments(ActivationContext activationData)
		{
			if (activationData == null)
			{
				throw new ArgumentNullException("activationData");
			}
			this._context = activationData;
			this._identity = activationData.Identity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> class with the specified application identity.</summary>
		/// <param name="applicationIdentity">An object that identifies the manifest-based activation application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="applicationIdentity" /> is null.</exception>
		// Token: 0x0600496C RID: 18796 RVA: 0x00107A39 File Offset: 0x00105C39
		public ActivationArguments(ApplicationIdentity applicationIdentity)
		{
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			this._identity = applicationIdentity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> class with the specified activation context and activation data.</summary>
		/// <param name="activationContext">An object that identifies the manifest-based activation application.</param>
		/// <param name="activationData">An array of strings containing host-provided activation data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationContext" /> is null.</exception>
		// Token: 0x0600496D RID: 18797 RVA: 0x00107A56 File Offset: 0x00105C56
		public ActivationArguments(ActivationContext activationContext, string[] activationData)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			this._context = activationContext;
			this._identity = activationContext.Identity;
			this._data = activationData;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Hosting.ActivationArguments" /> class with the specified application identity and activation data.</summary>
		/// <param name="applicationIdentity">An object that identifies the manifest-based activation application.</param>
		/// <param name="activationData">An array of strings containing host-provided activation data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="applicationIdentity" /> is null.</exception>
		// Token: 0x0600496E RID: 18798 RVA: 0x00107A86 File Offset: 0x00105C86
		public ActivationArguments(ApplicationIdentity applicationIdentity, string[] activationData)
		{
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			this._identity = applicationIdentity;
			this._data = activationData;
		}

		/// <summary>Gets the activation context for manifest-based activation of an application.</summary>
		/// <returns>An object that identifies a manifest-based activation application.</returns>
		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x0600496F RID: 18799 RVA: 0x00107AAA File Offset: 0x00105CAA
		public ActivationContext ActivationContext
		{
			get
			{
				return this._context;
			}
		}

		/// <summary>Gets activation data from the host.</summary>
		/// <returns>An array of strings containing host-provided activation data.</returns>
		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06004970 RID: 18800 RVA: 0x00107AB2 File Offset: 0x00105CB2
		public string[] ActivationData
		{
			get
			{
				return this._data;
			}
		}

		/// <summary>Gets the application identity for a manifest-activated application.</summary>
		/// <returns>An object that identifies an application for manifest-based activation.</returns>
		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06004971 RID: 18801 RVA: 0x00107ABA File Offset: 0x00105CBA
		public ApplicationIdentity ApplicationIdentity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x0400267C RID: 9852
		private ActivationContext _context;

		// Token: 0x0400267D RID: 9853
		private ApplicationIdentity _identity;

		// Token: 0x0400267E RID: 9854
		private string[] _data;
	}
}
