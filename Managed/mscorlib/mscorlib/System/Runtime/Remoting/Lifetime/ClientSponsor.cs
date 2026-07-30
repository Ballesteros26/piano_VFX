using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Lifetime
{
	/// <summary>Provides a default implementation for a lifetime sponsor class.</summary>
	// Token: 0x02000771 RID: 1905
	[ComVisible(true)]
	public class ClientSponsor : MarshalByRefObject, ISponsor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> class with default values.</summary>
		// Token: 0x06004E9D RID: 20125 RVA: 0x0011C034 File Offset: 0x0011A234
		public ClientSponsor()
		{
			this.renewal_time = new TimeSpan(0, 2, 0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" /> class with the renewal time of the sponsored object.</summary>
		/// <param name="renewalTime">The <see cref="T:System.TimeSpan" /> by which to increase the lifetime of the sponsored objects when renewal is requested. </param>
		// Token: 0x06004E9E RID: 20126 RVA: 0x0011C055 File Offset: 0x0011A255
		public ClientSponsor(TimeSpan renewalTime)
		{
			this.renewal_time = renewalTime;
		}

		/// <summary>Gets or sets the <see cref="T:System.TimeSpan" /> by which to increase the lifetime of the sponsored objects when renewal is requested.</summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> by which to increase the lifetime of the sponsored objects when renewal is requested.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06004E9F RID: 20127 RVA: 0x0011C06F File Offset: 0x0011A26F
		// (set) Token: 0x06004EA0 RID: 20128 RVA: 0x0011C077 File Offset: 0x0011A277
		public TimeSpan RenewalTime
		{
			get
			{
				return this.renewal_time;
			}
			set
			{
				this.renewal_time = value;
			}
		}

		/// <summary>Empties the list objects registered with the current <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004EA1 RID: 20129 RVA: 0x0011C080 File Offset: 0x0011A280
		public void Close()
		{
			foreach (object obj in this.registered_objects.Values)
			{
				(((MarshalByRefObject)obj).GetLifetimeService() as ILease).Unregister(this);
			}
			this.registered_objects.Clear();
		}

		// Token: 0x06004EA2 RID: 20130 RVA: 0x0011C0F4 File Offset: 0x0011A2F4
		~ClientSponsor()
		{
			this.Close();
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />, providing a lease for the current object.</summary>
		/// <returns>An <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> for the current object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004EA3 RID: 20131 RVA: 0x001173A8 File Offset: 0x001155A8
		public override object InitializeLifetimeService()
		{
			return base.InitializeLifetimeService();
		}

		/// <summary>Registers the specified <see cref="T:System.MarshalByRefObject" /> for sponsorship.</summary>
		/// <returns>true if registration succeeded; otherwise, false.</returns>
		/// <param name="obj">The object to register for sponsorship with the <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004EA4 RID: 20132 RVA: 0x0011C120 File Offset: 0x0011A320
		public bool Register(MarshalByRefObject obj)
		{
			if (this.registered_objects.ContainsKey(obj))
			{
				return false;
			}
			ILease lease = obj.GetLifetimeService() as ILease;
			if (lease == null)
			{
				return false;
			}
			lease.Register(this);
			this.registered_objects.Add(obj, obj);
			return true;
		}

		/// <summary>Requests a sponsoring client to renew the lease for the specified object.</summary>
		/// <returns>The additional lease time for the specified object.</returns>
		/// <param name="lease">The lifetime lease of the object that requires lease renewal. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004EA5 RID: 20133 RVA: 0x0011C06F File Offset: 0x0011A26F
		[SecurityCritical]
		public TimeSpan Renewal(ILease lease)
		{
			return this.renewal_time;
		}

		/// <summary>Unregisters the specified <see cref="T:System.MarshalByRefObject" /> from the list of objects sponsored by the current <see cref="T:System.Runtime.Remoting.Lifetime.ClientSponsor" />.</summary>
		/// <param name="obj">The object to unregister. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004EA6 RID: 20134 RVA: 0x0011C163 File Offset: 0x0011A363
		public void Unregister(MarshalByRefObject obj)
		{
			if (!this.registered_objects.ContainsKey(obj))
			{
				return;
			}
			(obj.GetLifetimeService() as ILease).Unregister(this);
			this.registered_objects.Remove(obj);
		}

		// Token: 0x040029FD RID: 10749
		private TimeSpan renewal_time;

		// Token: 0x040029FE RID: 10750
		private Hashtable registered_objects = new Hashtable();
	}
}
