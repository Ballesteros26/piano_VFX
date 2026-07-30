using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting
{
	/// <summary>Stores all relevant information required to generate a proxy in order to communicate with a remote object.</summary>
	// Token: 0x02000752 RID: 1874
	[ComVisible(true)]
	[Serializable]
	public class ObjRef : IObjectReference, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class with default values.</summary>
		// Token: 0x06004D5C RID: 19804 RVA: 0x00116F11 File Offset: 0x00115111
		public ObjRef()
		{
			this.UpdateChannelInfo();
		}

		// Token: 0x06004D5D RID: 19805 RVA: 0x00116F1F File Offset: 0x0011511F
		internal ObjRef(string uri, IChannelInfo cinfo)
		{
			this.uri = uri;
			this.channel_info = cinfo;
		}

		// Token: 0x06004D5E RID: 19806 RVA: 0x00116F38 File Offset: 0x00115138
		internal ObjRef DeserializeInTheCurrentDomain(int domainId, byte[] tInfo)
		{
			string text = string.Copy(this.uri);
			ChannelInfo channelInfo = new ChannelInfo(new CrossAppDomainData(domainId));
			ObjRef objRef = new ObjRef(text, channelInfo);
			IRemotingTypeInfo remotingTypeInfo = (IRemotingTypeInfo)CADSerializer.DeserializeObjectSafe(tInfo);
			objRef.typeInfo = remotingTypeInfo;
			return objRef;
		}

		// Token: 0x06004D5F RID: 19807 RVA: 0x00116F75 File Offset: 0x00115175
		internal byte[] SerializeType()
		{
			if (this.typeInfo == null)
			{
				throw new Exception("Attempt to serialize a null TypeInfo.");
			}
			return CADSerializer.SerializeObject(this.typeInfo).GetBuffer();
		}

		// Token: 0x06004D60 RID: 19808 RVA: 0x00116F9C File Offset: 0x0011519C
		internal ObjRef(ObjRef o, bool unmarshalAsProxy)
		{
			this.channel_info = o.channel_info;
			this.uri = o.uri;
			this.typeInfo = o.typeInfo;
			this.envoyInfo = o.envoyInfo;
			this.flags = o.flags;
			if (unmarshalAsProxy)
			{
				this.flags |= ObjRef.MarshalledObjectRef;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class to reference a specified <see cref="T:System.MarshalByRefObject" /> of a specified <see cref="T:System.Type" />.</summary>
		/// <param name="o">The object that the new <see cref="T:System.Runtime.Remoting.ObjRef" /> instance will reference. </param>
		/// <param name="requestedType">The <see cref="T:System.Type" /> of the object that the new <see cref="T:System.Runtime.Remoting.ObjRef" /> instance will reference. </param>
		// Token: 0x06004D61 RID: 19809 RVA: 0x00117000 File Offset: 0x00115200
		public ObjRef(MarshalByRefObject o, Type requestedType)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			if (requestedType == null)
			{
				throw new ArgumentNullException("requestedType");
			}
			this.uri = RemotingServices.GetObjectUri(o);
			this.typeInfo = new TypeInfo(requestedType);
			if (!requestedType.IsInstanceOfType(o))
			{
				throw new RemotingException("The server object type cannot be cast to the requested type " + requestedType.FullName);
			}
			this.UpdateChannelInfo();
		}

		// Token: 0x06004D62 RID: 19810 RVA: 0x00117072 File Offset: 0x00115272
		internal ObjRef(Type type, string url, object remoteChannelData)
		{
			this.uri = url;
			this.typeInfo = new TypeInfo(type);
			if (remoteChannelData != null)
			{
				this.channel_info = new ChannelInfo(remoteChannelData);
			}
			this.flags |= ObjRef.WellKnowObjectRef;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class from serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination of the exception. </param>
		// Token: 0x06004D63 RID: 19811 RVA: 0x001170B0 File Offset: 0x001152B0
		protected ObjRef(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			bool flag = true;
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "uri"))
				{
					if (!(name == "typeInfo"))
					{
						if (!(name == "channelInfo"))
						{
							if (!(name == "envoyInfo"))
							{
								if (!(name == "fIsMarshalled"))
								{
									if (!(name == "objrefFlags"))
									{
										throw new NotSupportedException();
									}
									this.flags = Convert.ToInt32(enumerator.Value);
								}
								else
								{
									object value = enumerator.Value;
									int num;
									if (value is string)
									{
										num = ((IConvertible)value).ToInt32(null);
									}
									else
									{
										num = (int)value;
									}
									if (num == 0)
									{
										flag = false;
									}
								}
							}
							else
							{
								this.envoyInfo = (IEnvoyInfo)enumerator.Value;
							}
						}
						else
						{
							this.channel_info = (IChannelInfo)enumerator.Value;
						}
					}
					else
					{
						this.typeInfo = (IRemotingTypeInfo)enumerator.Value;
					}
				}
				else
				{
					this.uri = (string)enumerator.Value;
				}
			}
			if (flag)
			{
				this.flags |= ObjRef.MarshalledObjectRef;
			}
		}

		// Token: 0x06004D64 RID: 19812 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal bool IsPossibleToCAD()
		{
			return false;
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06004D65 RID: 19813 RVA: 0x001171E6 File Offset: 0x001153E6
		internal bool IsReferenceToWellKnow
		{
			get
			{
				return (this.flags & ObjRef.WellKnowObjectRef) > 0;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Remoting.IChannelInfo" /> for the <see cref="T:System.Runtime.Remoting.ObjRef" />.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.IChannelInfo" /> interface for the <see cref="T:System.Runtime.Remoting.ObjRef" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06004D66 RID: 19814 RVA: 0x001171F7 File Offset: 0x001153F7
		// (set) Token: 0x06004D67 RID: 19815 RVA: 0x001171FF File Offset: 0x001153FF
		public virtual IChannelInfo ChannelInfo
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.channel_info;
			}
			set
			{
				this.channel_info = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Remoting.IEnvoyInfo" /> for the <see cref="T:System.Runtime.Remoting.ObjRef" />.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.IEnvoyInfo" /> interface for the <see cref="T:System.Runtime.Remoting.ObjRef" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06004D68 RID: 19816 RVA: 0x00117208 File Offset: 0x00115408
		// (set) Token: 0x06004D69 RID: 19817 RVA: 0x00117210 File Offset: 0x00115410
		public virtual IEnvoyInfo EnvoyInfo
		{
			get
			{
				return this.envoyInfo;
			}
			set
			{
				this.envoyInfo = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Remoting.IRemotingTypeInfo" /> for the object that the <see cref="T:System.Runtime.Remoting.ObjRef" /> describes.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.IRemotingTypeInfo" /> for the object that the <see cref="T:System.Runtime.Remoting.ObjRef" /> describes.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06004D6A RID: 19818 RVA: 0x00117219 File Offset: 0x00115419
		// (set) Token: 0x06004D6B RID: 19819 RVA: 0x00117221 File Offset: 0x00115421
		public virtual IRemotingTypeInfo TypeInfo
		{
			get
			{
				return this.typeInfo;
			}
			set
			{
				this.typeInfo = value;
			}
		}

		/// <summary>Gets or sets the URI of the specific object instance.</summary>
		/// <returns>The URI of the specific object instance.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06004D6C RID: 19820 RVA: 0x0011722A File Offset: 0x0011542A
		// (set) Token: 0x06004D6D RID: 19821 RVA: 0x00117232 File Offset: 0x00115432
		public virtual string URI
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		/// <summary>Populates a specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the current <see cref="T:System.Runtime.Remoting.ObjRef" /> instance.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="context">The contextual information about the source or destination of the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have serialization formatter permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004D6E RID: 19822 RVA: 0x0011723C File Offset: 0x0011543C
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(base.GetType());
			info.AddValue("uri", this.uri);
			info.AddValue("typeInfo", this.typeInfo, typeof(IRemotingTypeInfo));
			info.AddValue("envoyInfo", this.envoyInfo, typeof(IEnvoyInfo));
			info.AddValue("channelInfo", this.channel_info, typeof(IChannelInfo));
			info.AddValue("objrefFlags", this.flags);
		}

		/// <summary>Returns a reference to the remote object that the <see cref="T:System.Runtime.Remoting.ObjRef" /> describes.</summary>
		/// <returns>A reference to the remote object that the <see cref="T:System.Runtime.Remoting.ObjRef" /> describes.</returns>
		/// <param name="context">The context where the current object resides. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have serialization formatter permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter, RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004D6F RID: 19823 RVA: 0x001172C8 File Offset: 0x001154C8
		[SecurityCritical]
		public virtual object GetRealObject(StreamingContext context)
		{
			if ((this.flags & ObjRef.MarshalledObjectRef) > 0)
			{
				return RemotingServices.Unmarshal(this);
			}
			return this;
		}

		/// <summary>Returns a Boolean value that indicates whether the current <see cref="T:System.Runtime.Remoting.ObjRef" /> instance references an object located in the current <see cref="T:System.AppDomain" />.</summary>
		/// <returns>A Boolean value that indicates whether the current <see cref="T:System.Runtime.Remoting.ObjRef" /> instance references an object located in the current <see cref="T:System.AppDomain" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004D70 RID: 19824 RVA: 0x001172E4 File Offset: 0x001154E4
		public bool IsFromThisAppDomain()
		{
			Identity identityForUri = RemotingServices.GetIdentityForUri(this.uri);
			return identityForUri != null && identityForUri.IsFromThisAppDomain;
		}

		/// <summary>Returns a Boolean value that indicates whether the current <see cref="T:System.Runtime.Remoting.ObjRef" /> instance references an object located in the current process.</summary>
		/// <returns>A Boolean value that indicates whether the current <see cref="T:System.Runtime.Remoting.ObjRef" /> instance references an object located in the current process.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06004D71 RID: 19825 RVA: 0x00117308 File Offset: 0x00115508
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool IsFromThisProcess()
		{
			foreach (object obj in this.channel_info.ChannelData)
			{
				if (obj is CrossAppDomainData)
				{
					return ((CrossAppDomainData)obj).ProcessID == RemotingConfiguration.ProcessId;
				}
			}
			return true;
		}

		// Token: 0x06004D72 RID: 19826 RVA: 0x00117352 File Offset: 0x00115552
		internal void UpdateChannelInfo()
		{
			this.channel_info = new ChannelInfo();
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06004D73 RID: 19827 RVA: 0x0011735F File Offset: 0x0011555F
		internal Type ServerType
		{
			get
			{
				if (this._serverType == null)
				{
					this._serverType = Type.GetType(this.typeInfo.TypeName);
				}
				return this._serverType;
			}
		}

		// Token: 0x06004D74 RID: 19828 RVA: 0x00002194 File Offset: 0x00000394
		internal void SetDomainID(int id)
		{
		}

		// Token: 0x0400299A RID: 10650
		private IChannelInfo channel_info;

		// Token: 0x0400299B RID: 10651
		private string uri;

		// Token: 0x0400299C RID: 10652
		private IRemotingTypeInfo typeInfo;

		// Token: 0x0400299D RID: 10653
		private IEnvoyInfo envoyInfo;

		// Token: 0x0400299E RID: 10654
		private int flags;

		// Token: 0x0400299F RID: 10655
		private Type _serverType;

		// Token: 0x040029A0 RID: 10656
		private static int MarshalledObjectRef = 1;

		// Token: 0x040029A1 RID: 10657
		private static int WellKnowObjectRef = 2;
	}
}
