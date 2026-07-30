using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.Security.Permissions;

namespace System
{
	/// <summary>Enables access to objects across application domain boundaries in applications that support remoting.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000219 RID: 537
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class MarshalByRefObject
	{
		// Token: 0x06001971 RID: 6513 RVA: 0x0005F32C File Offset: 0x0005D52C
		internal Identity GetObjectIdentity(MarshalByRefObject obj, out bool IsClient)
		{
			IsClient = false;
			Identity identity;
			if (RemotingServices.IsTransparentProxy(obj))
			{
				identity = RemotingServices.GetRealProxy(obj).ObjectIdentity;
				IsClient = true;
			}
			else
			{
				identity = obj.ObjectIdentity;
			}
			return identity;
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x0005F35F File Offset: 0x0005D55F
		// (set) Token: 0x06001973 RID: 6515 RVA: 0x0005F367 File Offset: 0x0005D567
		internal ServerIdentity ObjectIdentity
		{
			get
			{
				return this._identity;
			}
			set
			{
				this._identity = value;
			}
		}

		/// <summary>Creates an object that contains all the relevant information required to generate a proxy used to communicate with a remote object.</summary>
		/// <returns>Information required to generate a proxy.</returns>
		/// <param name="requestedType">The <see cref="T:System.Type" /> of the object that the new <see cref="T:System.Runtime.Remoting.ObjRef" /> will reference. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">This instance is not a valid remoting object. </exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06001974 RID: 6516 RVA: 0x0005F370 File Offset: 0x0005D570
		[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
		public virtual ObjRef CreateObjRef(Type requestedType)
		{
			if (this._identity == null)
			{
				throw new RemotingException(Locale.GetText("No remoting information was found for the object."));
			}
			return this._identity.CreateObjRef(requestedType);
		}

		/// <summary>Retrieves the current lifetime service object that controls the lifetime policy for this instance.</summary>
		/// <returns>An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> used to control the lifetime policy for this instance.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06001975 RID: 6517 RVA: 0x0005F396 File Offset: 0x0005D596
		[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
		public object GetLifetimeService()
		{
			if (this._identity == null)
			{
				return null;
			}
			return this._identity.Lease;
		}

		/// <summary>Obtains a lifetime service object to control the lifetime policy for this instance.</summary>
		/// <returns>An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> used to control the lifetime policy for this instance. This is the current lifetime service object for this instance if one exists; otherwise, a new lifetime service object initialized to the value of the <see cref="P:System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseManagerPollTime" /> property.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06001976 RID: 6518 RVA: 0x0005F3AD File Offset: 0x0005D5AD
		[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
		public virtual object InitializeLifetimeService()
		{
			if (this._identity != null && this._identity.Lease != null)
			{
				return this._identity.Lease;
			}
			return new Lease();
		}

		/// <summary>Creates a shallow copy of the current <see cref="T:System.MarshalByRefObject" /> object.</summary>
		/// <returns>A shallow copy of the current <see cref="T:System.MarshalByRefObject" /> object.</returns>
		/// <param name="cloneIdentity">false to delete the current <see cref="T:System.MarshalByRefObject" /> object's identity, which will cause the object to be assigned a new identity when it is marshaled across a remoting boundary. A value of false is usually appropriate. true to copy the current <see cref="T:System.MarshalByRefObject" /> object's identity to its clone, which will cause remoting client calls to be routed to the remote server object. </param>
		// Token: 0x06001977 RID: 6519 RVA: 0x0005F3D8 File Offset: 0x0005D5D8
		protected MarshalByRefObject MemberwiseClone(bool cloneIdentity)
		{
			MarshalByRefObject marshalByRefObject = (MarshalByRefObject)base.MemberwiseClone();
			if (!cloneIdentity)
			{
				marshalByRefObject._identity = null;
			}
			return marshalByRefObject;
		}

		// Token: 0x04000CBC RID: 3260
		[NonSerialized]
		private ServerIdentity _identity;
	}
}
