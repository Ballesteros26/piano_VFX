using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Holds the security evidence for an application. This class cannot be inherited.</summary>
	// Token: 0x02000557 RID: 1367
	[ComVisible(true)]
	public sealed class ApplicationSecurityInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ApplicationSecurityInfo" /> class using the provided activation context. </summary>
		/// <param name="activationContext">An <see cref="T:System.ActivationContext" /> object that uniquely identifies the target application.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationContext" /> is null. </exception>
		// Token: 0x06003D7A RID: 15738 RVA: 0x000DCE04 File Offset: 0x000DB004
		public ApplicationSecurityInfo(ActivationContext activationContext)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
		}

		/// <summary>Gets or sets the evidence for the application.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.Evidence" /> object for the application.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Security.Policy.ApplicationSecurityInfo.ApplicationEvidence" /> is set to null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06003D7B RID: 15739 RVA: 0x000DCE1A File Offset: 0x000DB01A
		// (set) Token: 0x06003D7C RID: 15740 RVA: 0x000DCE22 File Offset: 0x000DB022
		public Evidence ApplicationEvidence
		{
			get
			{
				return this._evidence;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("ApplicationEvidence");
				}
				this._evidence = value;
			}
		}

		/// <summary>Gets or sets the application identity information.</summary>
		/// <returns>An <see cref="T:System.ApplicationId" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Security.Policy.ApplicationSecurityInfo.ApplicationId" /> is set to null.</exception>
		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06003D7D RID: 15741 RVA: 0x000DCE39 File Offset: 0x000DB039
		// (set) Token: 0x06003D7E RID: 15742 RVA: 0x000DCE41 File Offset: 0x000DB041
		public ApplicationId ApplicationId
		{
			get
			{
				return this._appid;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("ApplicationId");
				}
				this._appid = value;
			}
		}

		/// <summary>Gets or sets the default permission set.</summary>
		/// <returns>A <see cref="T:System.Security.PermissionSet" /> object representing the default permissions for the application. The default is a <see cref="T:System.Security.PermissionSet" /> with a permission state of <see cref="F:System.Security.Permissions.PermissionState.None" /></returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Security.Policy.ApplicationSecurityInfo.DefaultRequestSet" /> is set to null. </exception>
		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06003D7F RID: 15743 RVA: 0x000DCE58 File Offset: 0x000DB058
		// (set) Token: 0x06003D80 RID: 15744 RVA: 0x000DCE6F File Offset: 0x000DB06F
		public PermissionSet DefaultRequestSet
		{
			get
			{
				if (this._defaultSet == null)
				{
					return new PermissionSet(PermissionState.None);
				}
				return this._defaultSet;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("DefaultRequestSet");
				}
				this._defaultSet = value;
			}
		}

		/// <summary>Gets or sets the top element in the application, which is described in the deployment identity.</summary>
		/// <returns>An <see cref="T:System.ApplicationId" /> object describing the top element of the application.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Security.Policy.ApplicationSecurityInfo.DeploymentId" /> is set to null. </exception>
		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06003D81 RID: 15745 RVA: 0x000DCE86 File Offset: 0x000DB086
		// (set) Token: 0x06003D82 RID: 15746 RVA: 0x000DCE8E File Offset: 0x000DB08E
		public ApplicationId DeploymentId
		{
			get
			{
				return this._deployid;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("DeploymentId");
				}
				this._deployid = value;
			}
		}

		// Token: 0x04001F95 RID: 8085
		private Evidence _evidence;

		// Token: 0x04001F96 RID: 8086
		private ApplicationId _appid;

		// Token: 0x04001F97 RID: 8087
		private PermissionSet _defaultSet;

		// Token: 0x04001F98 RID: 8088
		private ApplicationId _deployid;
	}
}
