using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Selects the remoting surrogate that can be used to serialize an object that derives from a <see cref="T:System.MarshalByRefObject" />.</summary>
	// Token: 0x02000823 RID: 2083
	[ComVisible(true)]
	public class RemotingSurrogateSelector : ISurrogateSelector
	{
		/// <summary>Gets or sets the <see cref="T:System.Runtime.Remoting.Messaging.MessageSurrogateFilter" /> delegate for the current instance of the <see cref="T:System.Runtime.Remoting.Messaging.RemotingSurrogateSelector" />.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Messaging.MessageSurrogateFilter" /> delegate for the current instance of the <see cref="T:System.Runtime.Remoting.Messaging.RemotingSurrogateSelector" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06005346 RID: 21318 RVA: 0x001251E3 File Offset: 0x001233E3
		// (set) Token: 0x06005347 RID: 21319 RVA: 0x001251EB File Offset: 0x001233EB
		public MessageSurrogateFilter Filter
		{
			get
			{
				return this._filter;
			}
			set
			{
				this._filter = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> to the surrogate selector chain.</summary>
		/// <param name="selector">The next <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> to examine. </param>
		// Token: 0x06005348 RID: 21320 RVA: 0x001251F4 File Offset: 0x001233F4
		[SecurityCritical]
		public virtual void ChainSelector(ISurrogateSelector selector)
		{
			if (this._next != null)
			{
				selector.ChainSelector(this._next);
			}
			this._next = selector;
		}

		/// <summary>Returns the next <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> in the chain of surrogate selectors.</summary>
		/// <returns>The next <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> in the chain of surrogate selectors.</returns>
		// Token: 0x06005349 RID: 21321 RVA: 0x00125211 File Offset: 0x00123411
		[SecurityCritical]
		public virtual ISurrogateSelector GetNextSelector()
		{
			return this._next;
		}

		/// <summary>Returns the object at the root of the object graph.</summary>
		/// <returns>The object at the root of the object graph.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x0600534A RID: 21322 RVA: 0x00125219 File Offset: 0x00123419
		public object GetRootObject()
		{
			return this._rootObj;
		}

		/// <summary>Returns the appropriate surrogate for the given type in the given context.</summary>
		/// <returns>The appropriate surrogate for the given type in the given context.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> for which the surrogate is requested. </param>
		/// <param name="context">The source or destination of serialization. </param>
		/// <param name="ssout">When this method returns, contains an <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> that is appropriate for the specified object type. This parameter is passed uninitialized. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x0600534B RID: 21323 RVA: 0x00125224 File Offset: 0x00123424
		[SecurityCritical]
		public virtual ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector ssout)
		{
			if (type.IsMarshalByRef)
			{
				ssout = this;
				return RemotingSurrogateSelector._objRemotingSurrogate;
			}
			if (RemotingSurrogateSelector.s_cachedTypeObjRef.IsAssignableFrom(type))
			{
				ssout = this;
				return RemotingSurrogateSelector._objRefSurrogate;
			}
			if (this._next != null)
			{
				return this._next.GetSurrogate(type, context, out ssout);
			}
			ssout = null;
			return null;
		}

		/// <summary>Sets the object at the root of the object graph.</summary>
		/// <param name="obj">The object at the root of the object graph. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x0600534C RID: 21324 RVA: 0x00125273 File Offset: 0x00123473
		public void SetRootObject(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException();
			}
			this._rootObj = obj;
		}

		/// <summary>Sets up the current surrogate selector to use the SOAP format.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x0600534D RID: 21325 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO]
		public virtual void UseSoapFormat()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002B4F RID: 11087
		private static Type s_cachedTypeObjRef = typeof(ObjRef);

		// Token: 0x04002B50 RID: 11088
		private static ObjRefSurrogate _objRefSurrogate = new ObjRefSurrogate();

		// Token: 0x04002B51 RID: 11089
		private static RemotingSurrogate _objRemotingSurrogate = new RemotingSurrogate();

		// Token: 0x04002B52 RID: 11090
		private object _rootObj;

		// Token: 0x04002B53 RID: 11091
		private MessageSurrogateFilter _filter;

		// Token: 0x04002B54 RID: 11092
		private ISurrogateSelector _next;
	}
}
