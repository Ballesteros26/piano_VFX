using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> interface to create a request message that constitutes a constructor call on a remote object.</summary>
	// Token: 0x02000802 RID: 2050
	[ComVisible(true)]
	[CLSCompliant(false)]
	[Serializable]
	public class ConstructionCall : MethodCall, IConstructionCallMessage, IMessage, IMethodCallMessage, IMethodMessage
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.ConstructionCall" /> class by copying an existing message. </summary>
		/// <param name="m">A remoting message.</param>
		// Token: 0x06005214 RID: 21012 RVA: 0x001226C3 File Offset: 0x001208C3
		public ConstructionCall(IMessage m)
			: base(m)
		{
			this._activationTypeName = base.TypeName;
			this._isContextOk = true;
		}

		// Token: 0x06005215 RID: 21013 RVA: 0x001226DF File Offset: 0x001208DF
		internal ConstructionCall(Type type)
		{
			this._activationType = type;
			this._activationTypeName = type.AssemblyQualifiedName;
			this._isContextOk = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.ConstructionCall" /> class from an array of remoting headers. </summary>
		/// <param name="headers">An array of remoting headers that contain key-value pairs. This array is used to initialize <see cref="T:System.Runtime.Remoting.Messaging.ConstructionCall" /> fields for those headers that belong to the namespace "http://schemas.microsoft.com/clr/soap/messageProperties".</param>
		// Token: 0x06005216 RID: 21014 RVA: 0x00122701 File Offset: 0x00120901
		public ConstructionCall(Header[] headers)
			: base(headers)
		{
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x0012270A File Offset: 0x0012090A
		internal ConstructionCall(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06005218 RID: 21016 RVA: 0x00122714 File Offset: 0x00120914
		internal override void InitDictionary()
		{
			ConstructionCallDictionary constructionCallDictionary = new ConstructionCallDictionary(this);
			this.ExternalProperties = constructionCallDictionary;
			this.InternalProperties = constructionCallDictionary.GetInternalProperties();
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06005219 RID: 21017 RVA: 0x0012273B File Offset: 0x0012093B
		// (set) Token: 0x0600521A RID: 21018 RVA: 0x00122743 File Offset: 0x00120943
		internal bool IsContextOk
		{
			get
			{
				return this._isContextOk;
			}
			set
			{
				this._isContextOk = value;
			}
		}

		/// <summary>Gets the type of the remote object to activate. </summary>
		/// <returns>The <see cref="T:System.Type" /> of the remote object to activate.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x0600521B RID: 21019 RVA: 0x0012274C File Offset: 0x0012094C
		public Type ActivationType
		{
			[SecurityCritical]
			get
			{
				if (this._activationType == null)
				{
					this._activationType = Type.GetType(this._activationTypeName);
				}
				return this._activationType;
			}
		}

		/// <summary>Gets the full type name of the remote object to activate. </summary>
		/// <returns>A <see cref="T:System.String" /> containing the full type name of the remote object to activate.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x0600521C RID: 21020 RVA: 0x00122773 File Offset: 0x00120973
		public string ActivationTypeName
		{
			[SecurityCritical]
			get
			{
				return this._activationTypeName;
			}
		}

		/// <summary>Gets or sets the activator that activates the remote object. </summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Activation.IActivator" /> that activates the remote object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x0600521D RID: 21021 RVA: 0x0012277B File Offset: 0x0012097B
		// (set) Token: 0x0600521E RID: 21022 RVA: 0x00122783 File Offset: 0x00120983
		public IActivator Activator
		{
			[SecurityCritical]
			get
			{
				return this._activator;
			}
			[SecurityCritical]
			set
			{
				this._activator = value;
			}
		}

		/// <summary>Gets the call site activation attributes for the remote object. </summary>
		/// <returns>An array of type <see cref="T:System.Object" /> containing the call site activation attributes for the remote object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x0600521F RID: 21023 RVA: 0x0012278C File Offset: 0x0012098C
		public object[] CallSiteActivationAttributes
		{
			[SecurityCritical]
			get
			{
				return this._activationAttributes;
			}
		}

		// Token: 0x06005220 RID: 21024 RVA: 0x00122794 File Offset: 0x00120994
		internal void SetActivationAttributes(object[] attributes)
		{
			this._activationAttributes = attributes;
		}

		/// <summary>Gets a list of properties that define the context in which the remote object is to be created. </summary>
		/// <returns>A <see cref="T:System.Collections.IList" /> that contains a list of properties that define the context in which the remote object is to be created.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06005221 RID: 21025 RVA: 0x0012279D File Offset: 0x0012099D
		public IList ContextProperties
		{
			[SecurityCritical]
			get
			{
				if (this._contextProperties == null)
				{
					this._contextProperties = new ArrayList();
				}
				return this._contextProperties;
			}
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x001227B8 File Offset: 0x001209B8
		internal override void InitMethodProperty(string key, object value)
		{
			if (key == "__Activator")
			{
				this._activator = (IActivator)value;
				return;
			}
			if (key == "__CallSiteActivationAttributes")
			{
				this._activationAttributes = (object[])value;
				return;
			}
			if (key == "__ActivationType")
			{
				this._activationType = (Type)value;
				return;
			}
			if (key == "__ContextProperties")
			{
				this._contextProperties = (IList)value;
				return;
			}
			if (!(key == "__ActivationTypeName"))
			{
				base.InitMethodProperty(key, value);
				return;
			}
			this._activationTypeName = (string)value;
		}

		// Token: 0x06005223 RID: 21027 RVA: 0x00122854 File Offset: 0x00120A54
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IList list = this._contextProperties;
			if (list != null && list.Count == 0)
			{
				list = null;
			}
			info.AddValue("__Activator", this._activator);
			info.AddValue("__CallSiteActivationAttributes", this._activationAttributes);
			info.AddValue("__ActivationType", null);
			info.AddValue("__ContextProperties", list);
			info.AddValue("__ActivationTypeName", this._activationTypeName);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties. </summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06005224 RID: 21028 RVA: 0x001228C8 File Offset: 0x00120AC8
		public override IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				return base.Properties;
			}
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06005225 RID: 21029 RVA: 0x001228D0 File Offset: 0x00120AD0
		// (set) Token: 0x06005226 RID: 21030 RVA: 0x001228D8 File Offset: 0x00120AD8
		internal RemotingProxy SourceProxy
		{
			get
			{
				return this._sourceProxy;
			}
			set
			{
				this._sourceProxy = value;
			}
		}

		// Token: 0x04002AFB RID: 11003
		private IActivator _activator;

		// Token: 0x04002AFC RID: 11004
		private object[] _activationAttributes;

		// Token: 0x04002AFD RID: 11005
		private IList _contextProperties;

		// Token: 0x04002AFE RID: 11006
		private Type _activationType;

		// Token: 0x04002AFF RID: 11007
		private string _activationTypeName;

		// Token: 0x04002B00 RID: 11008
		private bool _isContextOk;

		// Token: 0x04002B01 RID: 11009
		[NonSerialized]
		private RemotingProxy _sourceProxy;
	}
}
