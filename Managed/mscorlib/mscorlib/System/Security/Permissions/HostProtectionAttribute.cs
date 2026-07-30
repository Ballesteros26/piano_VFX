using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows the use of declarative security actions to determine host protection requirements. This class cannot be inherited.</summary>
	// Token: 0x02000591 RID: 1425
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class HostProtectionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.HostProtectionAttribute" /> class with default values.</summary>
		// Token: 0x06003FBE RID: 16318 RVA: 0x000E3CF8 File Offset: 0x000E1EF8
		public HostProtectionAttribute()
			: base(SecurityAction.LinkDemand)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.HostProtectionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" /> value.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="action" /> is not <see cref="F:System.Security.Permissions.SecurityAction.LinkDemand" />. </exception>
		// Token: 0x06003FBF RID: 16319 RVA: 0x000E3D01 File Offset: 0x000E1F01
		public HostProtectionAttribute(SecurityAction action)
			: base(action)
		{
			if (action != SecurityAction.LinkDemand)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Only {0} is accepted."), SecurityAction.LinkDemand), "action");
			}
		}

		/// <summary>Gets or sets a value indicating whether external process management is exposed.</summary>
		/// <returns>true if external process management is exposed; otherwise, false. The default is false.</returns>
		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06003FC0 RID: 16320 RVA: 0x000E3D2E File Offset: 0x000E1F2E
		// (set) Token: 0x06003FC1 RID: 16321 RVA: 0x000E3D3B File Offset: 0x000E1F3B
		public bool ExternalProcessMgmt
		{
			get
			{
				return (this._resources & HostProtectionResource.ExternalProcessMgmt) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.ExternalProcessMgmt;
					return;
				}
				this._resources &= ~HostProtectionResource.ExternalProcessMgmt;
			}
		}

		/// <summary>Gets or sets a value indicating whether external threading is exposed.</summary>
		/// <returns>true if external threading is exposed; otherwise, false. The default is false.</returns>
		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06003FC2 RID: 16322 RVA: 0x000E3D5E File Offset: 0x000E1F5E
		// (set) Token: 0x06003FC3 RID: 16323 RVA: 0x000E3D6C File Offset: 0x000E1F6C
		public bool ExternalThreading
		{
			get
			{
				return (this._resources & HostProtectionResource.ExternalThreading) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.ExternalThreading;
					return;
				}
				this._resources &= ~HostProtectionResource.ExternalThreading;
			}
		}

		/// <summary>Gets or sets a value indicating whether resources might leak memory if the operation is terminated.</summary>
		/// <returns>true if resources might leak memory on termination; otherwise, false.</returns>
		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06003FC4 RID: 16324 RVA: 0x000E3D90 File Offset: 0x000E1F90
		// (set) Token: 0x06003FC5 RID: 16325 RVA: 0x000E3DA1 File Offset: 0x000E1FA1
		public bool MayLeakOnAbort
		{
			get
			{
				return (this._resources & HostProtectionResource.MayLeakOnAbort) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.MayLeakOnAbort;
					return;
				}
				this._resources &= ~HostProtectionResource.MayLeakOnAbort;
			}
		}

		/// <summary>Gets or sets a value indicating whether the security infrastructure is exposed.</summary>
		/// <returns>true if the security infrastructure is exposed; otherwise, false. The default is false.</returns>
		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06003FC6 RID: 16326 RVA: 0x000E3DCB File Offset: 0x000E1FCB
		// (set) Token: 0x06003FC7 RID: 16327 RVA: 0x000E3DD9 File Offset: 0x000E1FD9
		[ComVisible(true)]
		public bool SecurityInfrastructure
		{
			get
			{
				return (this._resources & HostProtectionResource.SecurityInfrastructure) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.SecurityInfrastructure;
					return;
				}
				this._resources &= ~HostProtectionResource.SecurityInfrastructure;
			}
		}

		/// <summary>Gets or sets a value indicating whether self-affecting process management is exposed.</summary>
		/// <returns>true if self-affecting process management is exposed; otherwise, false. The default is false.</returns>
		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06003FC8 RID: 16328 RVA: 0x000E3DFD File Offset: 0x000E1FFD
		// (set) Token: 0x06003FC9 RID: 16329 RVA: 0x000E3E0A File Offset: 0x000E200A
		public bool SelfAffectingProcessMgmt
		{
			get
			{
				return (this._resources & HostProtectionResource.SelfAffectingProcessMgmt) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.SelfAffectingProcessMgmt;
					return;
				}
				this._resources &= ~HostProtectionResource.SelfAffectingProcessMgmt;
			}
		}

		/// <summary>Gets or sets a value indicating whether self-affecting threading is exposed.</summary>
		/// <returns>true if self-affecting threading is exposed; otherwise, false. The default is false.</returns>
		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06003FCA RID: 16330 RVA: 0x000E3E2D File Offset: 0x000E202D
		// (set) Token: 0x06003FCB RID: 16331 RVA: 0x000E3E3B File Offset: 0x000E203B
		public bool SelfAffectingThreading
		{
			get
			{
				return (this._resources & HostProtectionResource.SelfAffectingThreading) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.SelfAffectingThreading;
					return;
				}
				this._resources &= ~HostProtectionResource.SelfAffectingThreading;
			}
		}

		/// <summary>Gets or sets a value indicating whether shared state is exposed.</summary>
		/// <returns>true if shared state is exposed; otherwise, false. The default is false.</returns>
		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06003FCC RID: 16332 RVA: 0x000E3E5F File Offset: 0x000E205F
		// (set) Token: 0x06003FCD RID: 16333 RVA: 0x000E3E6C File Offset: 0x000E206C
		public bool SharedState
		{
			get
			{
				return (this._resources & HostProtectionResource.SharedState) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.SharedState;
					return;
				}
				this._resources &= ~HostProtectionResource.SharedState;
			}
		}

		/// <summary>Gets or sets a value indicating whether synchronization is exposed.</summary>
		/// <returns>true if synchronization is exposed; otherwise, false. The default is false.</returns>
		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06003FCE RID: 16334 RVA: 0x000E3E8F File Offset: 0x000E208F
		// (set) Token: 0x06003FCF RID: 16335 RVA: 0x000E3E9C File Offset: 0x000E209C
		public bool Synchronization
		{
			get
			{
				return (this._resources & HostProtectionResource.Synchronization) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.Synchronization;
					return;
				}
				this._resources &= ~HostProtectionResource.Synchronization;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user interface is exposed.</summary>
		/// <returns>true if the user interface is exposed; otherwise, false. The default is false.</returns>
		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06003FD0 RID: 16336 RVA: 0x000E3EBF File Offset: 0x000E20BF
		// (set) Token: 0x06003FD1 RID: 16337 RVA: 0x000E3ED0 File Offset: 0x000E20D0
		public bool UI
		{
			get
			{
				return (this._resources & HostProtectionResource.UI) > HostProtectionResource.None;
			}
			set
			{
				if (value)
				{
					this._resources |= HostProtectionResource.UI;
					return;
				}
				this._resources &= ~HostProtectionResource.UI;
			}
		}

		/// <summary>Gets or sets flags specifying categories of functionality that are potentially harmful to the host.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.HostProtectionResource" /> values. The default is <see cref="F:System.Security.Permissions.HostProtectionResource.None" />.</returns>
		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06003FD2 RID: 16338 RVA: 0x000E3EFA File Offset: 0x000E20FA
		// (set) Token: 0x06003FD3 RID: 16339 RVA: 0x000E3F02 File Offset: 0x000E2102
		public HostProtectionResource Resources
		{
			get
			{
				return this._resources;
			}
			set
			{
				this._resources = value;
			}
		}

		/// <summary>Creates and returns a new host protection permission.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that corresponds to the current attribute.</returns>
		// Token: 0x06003FD4 RID: 16340 RVA: 0x000E3F0B File Offset: 0x000E210B
		public override IPermission CreatePermission()
		{
			return new HostProtectionPermission(this._resources);
		}

		// Token: 0x04002045 RID: 8261
		private HostProtectionResource _resources;
	}
}
