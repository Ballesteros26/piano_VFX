using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Enforces a synchronization domain for the current context and all contexts that share the same instance.</summary>
	// Token: 0x0200078D RID: 1933
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(true)]
	[Serializable]
	public class SynchronizationAttribute : ContextAttribute, IContributeClientContextSink, IContributeServerContextSink
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> class with default values.</summary>
		// Token: 0x06004F38 RID: 20280 RVA: 0x0011D51C File Offset: 0x0011B71C
		public SynchronizationAttribute()
			: this(8, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> class with a Boolean value indicating whether reentry is required.</summary>
		/// <param name="reEntrant">A Boolean value indicating whether reentry is required. </param>
		// Token: 0x06004F39 RID: 20281 RVA: 0x0011D526 File Offset: 0x0011B726
		public SynchronizationAttribute(bool reEntrant)
			: this(8, reEntrant)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> class with a flag indicating the behavior of the object to which this attribute is applied.</summary>
		/// <param name="flag">An integer value indicating the behavior of the object to which this attribute is applied. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter was not one of the defined flags. </exception>
		// Token: 0x06004F3A RID: 20282 RVA: 0x0011D530 File Offset: 0x0011B730
		public SynchronizationAttribute(int flag)
			: this(flag, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> class with a flag indicating the behavior of the object to which this attribute is applied, and a Boolean value indicating whether reentry is required.</summary>
		/// <param name="flag">An integer value indicating the behavior of the object to which this attribute is applied. </param>
		/// <param name="reEntrant">true if reentry is required, and callouts must be intercepted and serialized; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter was not one of the defined flags. </exception>
		// Token: 0x06004F3B RID: 20283 RVA: 0x0011D53C File Offset: 0x0011B73C
		public SynchronizationAttribute(int flag, bool reEntrant)
			: base("Synchronization")
		{
			if (flag != 1 && flag != 4 && flag != 8 && flag != 2)
			{
				throw new ArgumentException("flag");
			}
			this._bReEntrant = reEntrant;
			this._flavor = flag;
		}

		/// <summary>Gets or sets a Boolean value indicating whether reentry is required.</summary>
		/// <returns>A Boolean value indicating whether reentry is required.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06004F3C RID: 20284 RVA: 0x0011D589 File Offset: 0x0011B789
		public virtual bool IsReEntrant
		{
			get
			{
				return this._bReEntrant;
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether the <see cref="T:System.Runtime.Remoting.Contexts.Context" /> implementing this instance of <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> is locked.</summary>
		/// <returns>A Boolean value indicating whether the <see cref="T:System.Runtime.Remoting.Contexts.Context" /> implementing this instance of <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> is locked.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06004F3D RID: 20285 RVA: 0x0011D591 File Offset: 0x0011B791
		// (set) Token: 0x06004F3E RID: 20286 RVA: 0x0011D59C File Offset: 0x0011B79C
		public virtual bool Locked
		{
			get
			{
				return this._lockCount > 0;
			}
			set
			{
				SynchronizationAttribute synchronizationAttribute;
				if (value)
				{
					this.AcquireLock();
					synchronizationAttribute = this;
					lock (synchronizationAttribute)
					{
						if (this._lockCount > 1)
						{
							this.ReleaseLock();
						}
						return;
					}
				}
				synchronizationAttribute = this;
				lock (synchronizationAttribute)
				{
					while (this._lockCount > 0 && this._ownerThread == Thread.CurrentThread)
					{
						this.ReleaseLock();
					}
				}
			}
		}

		// Token: 0x06004F3F RID: 20287 RVA: 0x0011D62C File Offset: 0x0011B82C
		internal void AcquireLock()
		{
			this._mutex.WaitOne();
			lock (this)
			{
				this._ownerThread = Thread.CurrentThread;
				this._lockCount++;
			}
		}

		// Token: 0x06004F40 RID: 20288 RVA: 0x0011D688 File Offset: 0x0011B888
		internal void ReleaseLock()
		{
			lock (this)
			{
				if (this._lockCount > 0 && this._ownerThread == Thread.CurrentThread)
				{
					this._lockCount--;
					this._mutex.ReleaseMutex();
					if (this._lockCount == 0)
					{
						this._ownerThread = null;
					}
				}
			}
		}

		/// <summary>Adds the Synchronized context property to the specified <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.</summary>
		/// <param name="ctorMsg">The <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> to which to add the property. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004F41 RID: 20289 RVA: 0x0011D6FC File Offset: 0x0011B8FC
		[SecurityCritical]
		[ComVisible(true)]
		public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			if (this._flavor != 1)
			{
				ctorMsg.ContextProperties.Add(this);
			}
		}

		/// <summary>Creates a CallOut sink and chains it in front of the provided chain of sinks at the context boundary on the client end of a remoting call.</summary>
		/// <returns>The composite sink chain with the new CallOut sink.</returns>
		/// <param name="nextSink">The chain of sinks composed so far. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004F42 RID: 20290 RVA: 0x0011D714 File Offset: 0x0011B914
		[SecurityCritical]
		public virtual IMessageSink GetClientContextSink(IMessageSink nextSink)
		{
			return new SynchronizedClientContextSink(nextSink, this);
		}

		/// <summary>Creates a synchronized dispatch sink and chains it in front of the provided chain of sinks at the context boundary on the server end of a remoting call.</summary>
		/// <returns>The composite sink chain with the new synchronized dispatch sink.</returns>
		/// <param name="nextSink">The chain of sinks composed so far. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004F43 RID: 20291 RVA: 0x0011D71D File Offset: 0x0011B91D
		[SecurityCritical]
		public virtual IMessageSink GetServerContextSink(IMessageSink nextSink)
		{
			return new SynchronizedServerContextSink(nextSink, this);
		}

		/// <summary>Returns a Boolean value indicating whether the context parameter meets the context attribute's requirements.</summary>
		/// <returns>true if the passed in context is OK; otherwise, false.</returns>
		/// <param name="ctx">The context to check. </param>
		/// <param name="msg">Information gathered at construction time of the context bound object marked by this attribute. The <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> can inspect, add to, and remove properties from the context while determining if the context is acceptable to it. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="ctx" /> or <paramref name="msg" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004F44 RID: 20292 RVA: 0x0011D728 File Offset: 0x0011B928
		[ComVisible(true)]
		[SecurityCritical]
		public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			SynchronizationAttribute synchronizationAttribute = ctx.GetProperty("Synchronization") as SynchronizationAttribute;
			int flavor = this._flavor;
			switch (flavor)
			{
			case 1:
				return synchronizationAttribute == null;
			case 2:
				return true;
			case 3:
				break;
			case 4:
				return synchronizationAttribute != null;
			default:
				if (flavor == 8)
				{
					return false;
				}
				break;
			}
			return false;
		}

		// Token: 0x06004F45 RID: 20293 RVA: 0x0011D77C File Offset: 0x0011B97C
		internal static void ExitContext()
		{
			if (Thread.CurrentContext.IsDefaultContext)
			{
				return;
			}
			SynchronizationAttribute synchronizationAttribute = Thread.CurrentContext.GetProperty("Synchronization") as SynchronizationAttribute;
			if (synchronizationAttribute == null)
			{
				return;
			}
			synchronizationAttribute.Locked = false;
		}

		// Token: 0x06004F46 RID: 20294 RVA: 0x0011D7B8 File Offset: 0x0011B9B8
		internal static void EnterContext()
		{
			if (Thread.CurrentContext.IsDefaultContext)
			{
				return;
			}
			SynchronizationAttribute synchronizationAttribute = Thread.CurrentContext.GetProperty("Synchronization") as SynchronizationAttribute;
			if (synchronizationAttribute == null)
			{
				return;
			}
			synchronizationAttribute.Locked = true;
		}

		/// <summary>Indicates that the class to which this attribute is applied cannot be created in a context that has synchronization. This field is constant.</summary>
		// Token: 0x04002A2D RID: 10797
		public const int NOT_SUPPORTED = 1;

		/// <summary>Indicates that the class to which this attribute is applied is not dependent on whether the context has synchronization. This field is constant.</summary>
		// Token: 0x04002A2E RID: 10798
		public const int SUPPORTED = 2;

		/// <summary>Indicates that the class to which this attribute is applied must be created in a context that has synchronization. This field is constant.</summary>
		// Token: 0x04002A2F RID: 10799
		public const int REQUIRED = 4;

		/// <summary>Indicates that the class to which this attribute is applied must be created in a context with a new instance of the synchronization property each time. This field is constant.</summary>
		// Token: 0x04002A30 RID: 10800
		public const int REQUIRES_NEW = 8;

		// Token: 0x04002A31 RID: 10801
		private bool _bReEntrant;

		// Token: 0x04002A32 RID: 10802
		private int _flavor;

		// Token: 0x04002A33 RID: 10803
		[NonSerialized]
		private int _lockCount;

		// Token: 0x04002A34 RID: 10804
		[NonSerialized]
		private Mutex _mutex = new Mutex(false);

		// Token: 0x04002A35 RID: 10805
		[NonSerialized]
		private Thread _ownerThread;
	}
}
