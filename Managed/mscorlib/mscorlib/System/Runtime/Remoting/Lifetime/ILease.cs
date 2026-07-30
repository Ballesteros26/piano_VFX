using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	/// <summary>Defines a lifetime lease object that is used by the remoting lifetime service.</summary>
	// Token: 0x02000772 RID: 1906
	[ComVisible(true)]
	public interface ILease
	{
		/// <summary>Gets the amount of time remaining on the lease.</summary>
		/// <returns>The amount of time remaining on the lease.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06004EA7 RID: 20135
		TimeSpan CurrentLeaseTime { get; }

		/// <summary>Gets the current <see cref="T:System.Runtime.Remoting.Lifetime.LeaseState" /> of the lease.</summary>
		/// <returns>The current <see cref="T:System.Runtime.Remoting.Lifetime.LeaseState" /> of the lease.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06004EA8 RID: 20136
		LeaseState CurrentState { get; }

		/// <summary>Gets or sets the initial time for the lease.</summary>
		/// <returns>The initial time for the lease.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06004EA9 RID: 20137
		// (set) Token: 0x06004EAA RID: 20138
		TimeSpan InitialLeaseTime { get; set; }

		/// <summary>Gets or sets the amount of time by which a call to the remote object renews the <see cref="P:System.Runtime.Remoting.Lifetime.ILease.CurrentLeaseTime" />.</summary>
		/// <returns>The amount of time by which a call to the remote object renews the <see cref="P:System.Runtime.Remoting.Lifetime.ILease.CurrentLeaseTime" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06004EAB RID: 20139
		// (set) Token: 0x06004EAC RID: 20140
		TimeSpan RenewOnCallTime { get; set; }

		/// <summary>Gets or sets the amount of time to wait for a sponsor to return with a lease renewal time.</summary>
		/// <returns>The amount of time to wait for a sponsor to return with a lease renewal time.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06004EAD RID: 20141
		// (set) Token: 0x06004EAE RID: 20142
		TimeSpan SponsorshipTimeout { get; set; }

		/// <summary>Registers a sponsor for the lease without renewing the lease.</summary>
		/// <param name="obj">The callback object of the sponsor. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004EAF RID: 20143
		void Register(ISponsor obj);

		/// <summary>Registers a sponsor for the lease, and renews it by the specified <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="obj">The callback object of the sponsor. </param>
		/// <param name="renewalTime">The length of time to renew the lease by. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		// Token: 0x06004EB0 RID: 20144
		void Register(ISponsor obj, TimeSpan renewalTime);

		/// <summary>Renews a lease for the specified time.</summary>
		/// <returns>The new expiration time of the lease.</returns>
		/// <param name="renewalTime">The length of time to renew the lease by. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004EB1 RID: 20145
		TimeSpan Renew(TimeSpan renewalTime);

		/// <summary>Removes a sponsor from the sponsor list.</summary>
		/// <param name="obj">The lease sponsor to unregister. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004EB2 RID: 20146
		void Unregister(ISponsor obj);
	}
}
