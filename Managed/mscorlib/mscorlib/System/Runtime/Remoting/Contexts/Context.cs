using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Defines an environment for the objects that are resident inside it and for which a policy can be enforced.</summary>
	// Token: 0x0200077A RID: 1914
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public class Context
	{
		// Token: 0x06004EE1 RID: 20193
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterContext(Context ctx);

		// Token: 0x06004EE2 RID: 20194
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseContext(Context ctx);

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.Context" /> class.</summary>
		// Token: 0x06004EE3 RID: 20195 RVA: 0x0011C8E4 File Offset: 0x0011AAE4
		public Context()
		{
			this.domain_id = Thread.GetDomainID();
			this.context_id = Interlocked.Increment(ref Context.global_count);
			Context.RegisterContext(this);
		}

		// Token: 0x06004EE4 RID: 20196 RVA: 0x0011C910 File Offset: 0x0011AB10
		~Context()
		{
			Context.ReleaseContext(this);
		}

		/// <summary>Gets the default context for the current application domain.</summary>
		/// <returns>The default context for the <see cref="T:System.AppDomain" /> namespace.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06004EE5 RID: 20197 RVA: 0x0011C93C File Offset: 0x0011AB3C
		public static Context DefaultContext
		{
			get
			{
				return AppDomain.InternalGetDefaultContext();
			}
		}

		/// <summary>Gets the context ID for the current context.</summary>
		/// <returns>The context ID for the current context.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06004EE6 RID: 20198 RVA: 0x0011C943 File Offset: 0x0011AB43
		public virtual int ContextID
		{
			get
			{
				return this.context_id;
			}
		}

		/// <summary>Gets the array of the current context properties.</summary>
		/// <returns>The current context properties array; otherwise, null if the context does not have any properties attributed to it.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06004EE7 RID: 20199 RVA: 0x0011C94B File Offset: 0x0011AB4B
		public virtual IContextProperty[] ContextProperties
		{
			get
			{
				if (this.context_properties == null)
				{
					return new IContextProperty[0];
				}
				return this.context_properties.ToArray();
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06004EE8 RID: 20200 RVA: 0x0011C967 File Offset: 0x0011AB67
		internal bool IsDefaultContext
		{
			get
			{
				return this.context_id == 0;
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06004EE9 RID: 20201 RVA: 0x0011C972 File Offset: 0x0011AB72
		internal bool NeedsContextSink
		{
			get
			{
				return this.context_id != 0 || (Context.global_dynamic_properties != null && Context.global_dynamic_properties.HasProperties) || (this.context_dynamic_properties != null && this.context_dynamic_properties.HasProperties);
			}
		}

		/// <summary>Registers a dynamic property implementing the <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" /> interface with the remoting service.</summary>
		/// <returns>true if the property was successfully registered; otherwise, false.</returns>
		/// <param name="prop">The dynamic property to register. </param>
		/// <param name="obj">The object/proxy for which the <paramref name="property" /> is registered. </param>
		/// <param name="ctx">The context for which the <paramref name="property" /> is registered. </param>
		/// <exception cref="T:System.ArgumentNullException">Either <paramref name="prop" /> or its name is null, or it is not dynamic (it does not implement <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" />). </exception>
		/// <exception cref="T:System.ArgumentException">Both an object as well as a context are specified (both <paramref name="obj" /> and <paramref name="ctx" /> are not null). </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004EEA RID: 20202 RVA: 0x0011C9A6 File Offset: 0x0011ABA6
		public static bool RegisterDynamicProperty(IDynamicProperty prop, ContextBoundObject obj, Context ctx)
		{
			return Context.GetDynamicPropertyCollection(obj, ctx).RegisterDynamicProperty(prop);
		}

		/// <summary>Unregisters a dynamic property implementing the <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" /> interface.</summary>
		/// <returns>true if the object was successfully unregistered; otherwise, false.</returns>
		/// <param name="name">The name of the dynamic property to unregister. </param>
		/// <param name="obj">The object/proxy for which the <paramref name="property" /> is registered. </param>
		/// <param name="ctx">The context for which the <paramref name="property" /> is registered. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">Both an object as well as a context are specified (both <paramref name="obj" /> and <paramref name="ctx" /> are not null). </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004EEB RID: 20203 RVA: 0x0011C9B5 File Offset: 0x0011ABB5
		public static bool UnregisterDynamicProperty(string name, ContextBoundObject obj, Context ctx)
		{
			return Context.GetDynamicPropertyCollection(obj, ctx).UnregisterDynamicProperty(name);
		}

		// Token: 0x06004EEC RID: 20204 RVA: 0x0011C9C4 File Offset: 0x0011ABC4
		private static DynamicPropertyCollection GetDynamicPropertyCollection(ContextBoundObject obj, Context ctx)
		{
			if (ctx == null && obj != null)
			{
				if (RemotingServices.IsTransparentProxy(obj))
				{
					return RemotingServices.GetRealProxy(obj).ObjectIdentity.ClientDynamicProperties;
				}
				return obj.ObjectIdentity.ServerDynamicProperties;
			}
			else
			{
				if (ctx != null && obj == null)
				{
					if (ctx.context_dynamic_properties == null)
					{
						ctx.context_dynamic_properties = new DynamicPropertyCollection();
					}
					return ctx.context_dynamic_properties;
				}
				if (ctx == null && obj == null)
				{
					if (Context.global_dynamic_properties == null)
					{
						Context.global_dynamic_properties = new DynamicPropertyCollection();
					}
					return Context.global_dynamic_properties;
				}
				throw new ArgumentException("Either obj or ctx must be null");
			}
		}

		// Token: 0x06004EED RID: 20205 RVA: 0x0011CA43 File Offset: 0x0011AC43
		internal static void NotifyGlobalDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (Context.global_dynamic_properties != null && Context.global_dynamic_properties.HasProperties)
			{
				Context.global_dynamic_properties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06004EEE RID: 20206 RVA: 0x0011CA66 File Offset: 0x0011AC66
		internal static bool HasGlobalDynamicSinks
		{
			get
			{
				return Context.global_dynamic_properties != null && Context.global_dynamic_properties.HasProperties;
			}
		}

		// Token: 0x06004EEF RID: 20207 RVA: 0x0011CA7B File Offset: 0x0011AC7B
		internal void NotifyDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (this.context_dynamic_properties != null && this.context_dynamic_properties.HasProperties)
			{
				this.context_dynamic_properties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06004EF0 RID: 20208 RVA: 0x0011CAA2 File Offset: 0x0011ACA2
		internal bool HasDynamicSinks
		{
			get
			{
				return this.context_dynamic_properties != null && this.context_dynamic_properties.HasProperties;
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06004EF1 RID: 20209 RVA: 0x0011CAB9 File Offset: 0x0011ACB9
		internal bool HasExitSinks
		{
			get
			{
				return !(this.GetClientContextSinkChain() is ClientContextTerminatorSink) || this.HasDynamicSinks || Context.HasGlobalDynamicSinks;
			}
		}

		/// <summary>Returns a specific context property, specified by name.</summary>
		/// <returns>The specified context property.</returns>
		/// <param name="name">The name of the property. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004EF2 RID: 20210 RVA: 0x0011CAD8 File Offset: 0x0011ACD8
		public virtual IContextProperty GetProperty(string name)
		{
			if (this.context_properties == null)
			{
				return null;
			}
			foreach (IContextProperty contextProperty in this.context_properties)
			{
				if (contextProperty.Name == name)
				{
					return contextProperty;
				}
			}
			return null;
		}

		/// <summary>Sets a specific context property by name.</summary>
		/// <param name="prop">The actual context property. </param>
		/// <exception cref="T:System.InvalidOperationException">There is an attempt to add properties to the default context. </exception>
		/// <exception cref="T:System.InvalidOperationException">The context is frozen. </exception>
		/// <exception cref="T:System.ArgumentNullException">The property or the property name is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004EF3 RID: 20211 RVA: 0x0011CB44 File Offset: 0x0011AD44
		public virtual void SetProperty(IContextProperty prop)
		{
			if (prop == null)
			{
				throw new ArgumentNullException("IContextProperty");
			}
			if (this == Context.DefaultContext)
			{
				throw new InvalidOperationException("Can not add properties to default context");
			}
			if (this.context_properties == null)
			{
				this.context_properties = new List<IContextProperty>();
			}
			this.context_properties.Add(prop);
		}

		/// <summary>Freezes the context, making it impossible to add or remove context properties from the current context.</summary>
		/// <exception cref="T:System.InvalidOperationException">The context is already frozen. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004EF4 RID: 20212 RVA: 0x0011CB94 File Offset: 0x0011AD94
		public virtual void Freeze()
		{
			if (this.context_properties != null)
			{
				foreach (IContextProperty contextProperty in this.context_properties)
				{
					contextProperty.Freeze(this);
				}
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> class representation of the current context.</summary>
		/// <returns>A <see cref="T:System.String" /> class representation of the current context.</returns>
		// Token: 0x06004EF5 RID: 20213 RVA: 0x0011CBF0 File Offset: 0x0011ADF0
		public override string ToString()
		{
			return "ContextID: " + this.context_id;
		}

		// Token: 0x06004EF6 RID: 20214 RVA: 0x0011CC08 File Offset: 0x0011AE08
		internal IMessageSink GetServerContextSinkChain()
		{
			if (this.server_context_sink_chain == null)
			{
				if (Context.default_server_context_sink == null)
				{
					Context.default_server_context_sink = new ServerContextTerminatorSink();
				}
				this.server_context_sink_chain = Context.default_server_context_sink;
				if (this.context_properties != null)
				{
					for (int i = this.context_properties.Count - 1; i >= 0; i--)
					{
						IContributeServerContextSink contributeServerContextSink = this.context_properties[i] as IContributeServerContextSink;
						if (contributeServerContextSink != null)
						{
							this.server_context_sink_chain = contributeServerContextSink.GetServerContextSink(this.server_context_sink_chain);
						}
					}
				}
			}
			return this.server_context_sink_chain;
		}

		// Token: 0x06004EF7 RID: 20215 RVA: 0x0011CC88 File Offset: 0x0011AE88
		internal IMessageSink GetClientContextSinkChain()
		{
			if (this.client_context_sink_chain == null)
			{
				this.client_context_sink_chain = new ClientContextTerminatorSink(this);
				if (this.context_properties != null)
				{
					foreach (IContextProperty contextProperty in this.context_properties)
					{
						IContributeClientContextSink contributeClientContextSink = contextProperty as IContributeClientContextSink;
						if (contributeClientContextSink != null)
						{
							this.client_context_sink_chain = contributeClientContextSink.GetClientContextSink(this.client_context_sink_chain);
						}
					}
				}
			}
			return this.client_context_sink_chain;
		}

		// Token: 0x06004EF8 RID: 20216 RVA: 0x0011CD10 File Offset: 0x0011AF10
		internal IMessageSink CreateServerObjectSinkChain(MarshalByRefObject obj, bool forceInternalExecute)
		{
			IMessageSink messageSink = new StackBuilderSink(obj, forceInternalExecute);
			messageSink = new ServerObjectTerminatorSink(messageSink);
			messageSink = new LeaseSink(messageSink);
			if (this.context_properties != null)
			{
				for (int i = this.context_properties.Count - 1; i >= 0; i--)
				{
					IContributeObjectSink contributeObjectSink = this.context_properties[i] as IContributeObjectSink;
					if (contributeObjectSink != null)
					{
						messageSink = contributeObjectSink.GetObjectSink(obj, messageSink);
					}
				}
			}
			return messageSink;
		}

		// Token: 0x06004EF9 RID: 20217 RVA: 0x0011CD74 File Offset: 0x0011AF74
		internal IMessageSink CreateEnvoySink(MarshalByRefObject serverObject)
		{
			IMessageSink messageSink = EnvoyTerminatorSink.Instance;
			if (this.context_properties != null)
			{
				foreach (IContextProperty contextProperty in this.context_properties)
				{
					IContributeEnvoySink contributeEnvoySink = contextProperty as IContributeEnvoySink;
					if (contributeEnvoySink != null)
					{
						messageSink = contributeEnvoySink.GetEnvoySink(serverObject, messageSink);
					}
				}
			}
			return messageSink;
		}

		// Token: 0x06004EFA RID: 20218 RVA: 0x0011CDE0 File Offset: 0x0011AFE0
		internal static Context SwitchToContext(Context newContext)
		{
			return AppDomain.InternalSetContext(newContext);
		}

		// Token: 0x06004EFB RID: 20219 RVA: 0x0011CDE8 File Offset: 0x0011AFE8
		internal static Context CreateNewContext(IConstructionCallMessage msg)
		{
			Context context = new Context();
			foreach (object obj in msg.ContextProperties)
			{
				IContextProperty contextProperty = (IContextProperty)obj;
				if (context.GetProperty(contextProperty.Name) == null)
				{
					context.SetProperty(contextProperty);
				}
			}
			context.Freeze();
			using (IEnumerator enumerator = msg.ContextProperties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!((IContextProperty)enumerator.Current).IsNewContextOK(context))
					{
						throw new RemotingException("A context property did not approve the candidate context for activating the object");
					}
				}
			}
			return context;
		}

		/// <summary>Executes code in another context.</summary>
		/// <param name="deleg">The delegate used to request the callback. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004EFC RID: 20220 RVA: 0x0011CEB0 File Offset: 0x0011B0B0
		public void DoCallBack(CrossContextDelegate deleg)
		{
			lock (this)
			{
				if (this.callback_object == null)
				{
					Context context = Context.SwitchToContext(this);
					this.callback_object = new ContextCallbackObject();
					Context.SwitchToContext(context);
				}
			}
			this.callback_object.DoCallBack(deleg);
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06004EFD RID: 20221 RVA: 0x0011CF10 File Offset: 0x0011B110
		private LocalDataStore MyLocalStore
		{
			get
			{
				if (this._localDataStore == null)
				{
					LocalDataStoreMgr localDataStoreMgr = Context._localDataStoreMgr;
					lock (localDataStoreMgr)
					{
						if (this._localDataStore == null)
						{
							this._localDataStore = Context._localDataStoreMgr.CreateLocalDataStore();
						}
					}
				}
				return this._localDataStore.Store;
			}
		}

		/// <summary>Allocates an unnamed data slot.</summary>
		/// <returns>A local data slot.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004EFE RID: 20222 RVA: 0x0011CF7C File Offset: 0x0011B17C
		public static LocalDataStoreSlot AllocateDataSlot()
		{
			return Context._localDataStoreMgr.AllocateDataSlot();
		}

		/// <summary>Allocates a named data slot.</summary>
		/// <returns>A local data slot object.</returns>
		/// <param name="name">The required name for the data slot. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004EFF RID: 20223 RVA: 0x0011CF88 File Offset: 0x0011B188
		public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
		{
			return Context._localDataStoreMgr.AllocateNamedDataSlot(name);
		}

		/// <summary>Frees a named data slot on all the contexts.</summary>
		/// <param name="name">The name of the data slot to free. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004F00 RID: 20224 RVA: 0x0011CF95 File Offset: 0x0011B195
		public static void FreeNamedDataSlot(string name)
		{
			Context._localDataStoreMgr.FreeNamedDataSlot(name);
		}

		/// <summary>Looks up a named data slot.</summary>
		/// <returns>Returns a local data slot.</returns>
		/// <param name="name">The data slot name. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004F01 RID: 20225 RVA: 0x0011CFA2 File Offset: 0x0011B1A2
		public static LocalDataStoreSlot GetNamedDataSlot(string name)
		{
			return Context._localDataStoreMgr.GetNamedDataSlot(name);
		}

		/// <summary>Retrieves the value from the specified slot on the current context.</summary>
		/// <returns>Returns the data associated with <paramref name="slot" />. </returns>
		/// <param name="slot">The data slot that contains the data. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004F02 RID: 20226 RVA: 0x0011CFAF File Offset: 0x0011B1AF
		public static object GetData(LocalDataStoreSlot slot)
		{
			return Thread.CurrentContext.MyLocalStore.GetData(slot);
		}

		/// <summary>Sets the data in the specified slot on the current context.</summary>
		/// <param name="slot">The data slot where the data is to be added. </param>
		/// <param name="data">The data that is to be added. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004F03 RID: 20227 RVA: 0x0011CFC1 File Offset: 0x0011B1C1
		public static void SetData(LocalDataStoreSlot slot, object data)
		{
			Thread.CurrentContext.MyLocalStore.SetData(slot, data);
		}

		// Token: 0x04002A15 RID: 10773
		private int domain_id;

		// Token: 0x04002A16 RID: 10774
		private int context_id;

		// Token: 0x04002A17 RID: 10775
		private UIntPtr static_data;

		// Token: 0x04002A18 RID: 10776
		private UIntPtr data;

		// Token: 0x04002A19 RID: 10777
		[ContextStatic]
		private static object[] local_slots;

		// Token: 0x04002A1A RID: 10778
		private static IMessageSink default_server_context_sink;

		// Token: 0x04002A1B RID: 10779
		private IMessageSink server_context_sink_chain;

		// Token: 0x04002A1C RID: 10780
		private IMessageSink client_context_sink_chain;

		// Token: 0x04002A1D RID: 10781
		private List<IContextProperty> context_properties;

		// Token: 0x04002A1E RID: 10782
		private static int global_count;

		// Token: 0x04002A1F RID: 10783
		private volatile LocalDataStoreHolder _localDataStore;

		// Token: 0x04002A20 RID: 10784
		private static LocalDataStoreMgr _localDataStoreMgr = new LocalDataStoreMgr();

		// Token: 0x04002A21 RID: 10785
		private static DynamicPropertyCollection global_dynamic_properties;

		// Token: 0x04002A22 RID: 10786
		private DynamicPropertyCollection context_dynamic_properties;

		// Token: 0x04002A23 RID: 10787
		private ContextCallbackObject callback_object;
	}
}
