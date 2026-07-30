using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Principal;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Provides a set of properties that are carried with the execution code path during remote method calls.</summary>
	// Token: 0x020007F3 RID: 2035
	[SecurityCritical]
	[ComVisible(true)]
	[Serializable]
	public sealed class LogicalCallContext : ISerializable, ICloneable
	{
		// Token: 0x060051A5 RID: 20901 RVA: 0x00002111 File Offset: 0x00000311
		internal LogicalCallContext()
		{
		}

		// Token: 0x060051A6 RID: 20902 RVA: 0x00120FE4 File Offset: 0x0011F1E4
		[SecurityCritical]
		internal LogicalCallContext(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name.Equals("__RemotingData"))
				{
					this.m_RemotingData = (CallContextRemotingData)enumerator.Value;
				}
				else if (enumerator.Name.Equals("__SecurityData"))
				{
					if (context.State == StreamingContextStates.CrossAppDomain)
					{
						this.m_SecurityData = (CallContextSecurityData)enumerator.Value;
					}
				}
				else if (enumerator.Name.Equals("__HostContext"))
				{
					this.m_HostContext = enumerator.Value;
				}
				else if (enumerator.Name.Equals("__CorrelationMgrSlotPresent"))
				{
					this.m_IsCorrelationMgr = (bool)enumerator.Value;
				}
				else
				{
					this.Datastore[enumerator.Name] = enumerator.Value;
				}
			}
		}

		/// <summary>Populates a specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the current <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="context">The contextual information about the source or destination of the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have SerializationFormatter permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060051A7 RID: 20903 RVA: 0x001210C8 File Offset: 0x0011F2C8
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.SetType(LogicalCallContext.s_callContextType);
			if (this.m_RemotingData != null)
			{
				info.AddValue("__RemotingData", this.m_RemotingData);
			}
			if (this.m_SecurityData != null && context.State == StreamingContextStates.CrossAppDomain)
			{
				info.AddValue("__SecurityData", this.m_SecurityData);
			}
			if (this.m_HostContext != null)
			{
				info.AddValue("__HostContext", this.m_HostContext);
			}
			if (this.m_IsCorrelationMgr)
			{
				info.AddValue("__CorrelationMgrSlotPresent", this.m_IsCorrelationMgr);
			}
			if (this.HasUserData)
			{
				IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
				while (enumerator.MoveNext())
				{
					info.AddValue((string)enumerator.Key, enumerator.Value);
				}
			}
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060051A8 RID: 20904 RVA: 0x00121198 File Offset: 0x0011F398
		[SecuritySafeCritical]
		public object Clone()
		{
			LogicalCallContext logicalCallContext = new LogicalCallContext();
			if (this.m_RemotingData != null)
			{
				logicalCallContext.m_RemotingData = (CallContextRemotingData)this.m_RemotingData.Clone();
			}
			if (this.m_SecurityData != null)
			{
				logicalCallContext.m_SecurityData = (CallContextSecurityData)this.m_SecurityData.Clone();
			}
			if (this.m_HostContext != null)
			{
				logicalCallContext.m_HostContext = this.m_HostContext;
			}
			logicalCallContext.m_IsCorrelationMgr = this.m_IsCorrelationMgr;
			if (this.HasUserData)
			{
				IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
				if (!this.m_IsCorrelationMgr)
				{
					while (enumerator.MoveNext())
					{
						logicalCallContext.Datastore[(string)enumerator.Key] = enumerator.Value;
					}
				}
				else
				{
					while (enumerator.MoveNext())
					{
						string text = (string)enumerator.Key;
						if (text.Equals("System.Diagnostics.Trace.CorrelationManagerSlot"))
						{
							logicalCallContext.Datastore[text] = ((ICloneable)enumerator.Value).Clone();
						}
						else
						{
							logicalCallContext.Datastore[text] = enumerator.Value;
						}
					}
				}
			}
			return logicalCallContext;
		}

		// Token: 0x060051A9 RID: 20905 RVA: 0x001212A0 File Offset: 0x0011F4A0
		[SecurityCritical]
		internal void Merge(LogicalCallContext lc)
		{
			if (lc != null && this != lc && lc.HasUserData)
			{
				IDictionaryEnumerator enumerator = lc.Datastore.GetEnumerator();
				while (enumerator.MoveNext())
				{
					this.Datastore[(string)enumerator.Key] = enumerator.Value;
				}
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> contains information.</summary>
		/// <returns>A Boolean value indicating whether the current <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> contains information.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x060051AA RID: 20906 RVA: 0x001212F0 File Offset: 0x0011F4F0
		public bool HasInfo
		{
			[SecurityCritical]
			get
			{
				bool flag = false;
				if ((this.m_RemotingData != null && this.m_RemotingData.HasInfo) || (this.m_SecurityData != null && this.m_SecurityData.HasInfo) || this.m_HostContext != null || this.HasUserData)
				{
					flag = true;
				}
				return flag;
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x060051AB RID: 20907 RVA: 0x0012133C File Offset: 0x0011F53C
		private bool HasUserData
		{
			get
			{
				return this.m_Datastore != null && this.m_Datastore.Count > 0;
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x060051AC RID: 20908 RVA: 0x00121356 File Offset: 0x0011F556
		internal CallContextRemotingData RemotingData
		{
			get
			{
				if (this.m_RemotingData == null)
				{
					this.m_RemotingData = new CallContextRemotingData();
				}
				return this.m_RemotingData;
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x060051AD RID: 20909 RVA: 0x00121371 File Offset: 0x0011F571
		internal CallContextSecurityData SecurityData
		{
			get
			{
				if (this.m_SecurityData == null)
				{
					this.m_SecurityData = new CallContextSecurityData();
				}
				return this.m_SecurityData;
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x060051AE RID: 20910 RVA: 0x0012138C File Offset: 0x0011F58C
		// (set) Token: 0x060051AF RID: 20911 RVA: 0x00121394 File Offset: 0x0011F594
		internal object HostContext
		{
			get
			{
				return this.m_HostContext;
			}
			set
			{
				this.m_HostContext = value;
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x060051B0 RID: 20912 RVA: 0x0012139D File Offset: 0x0011F59D
		private Hashtable Datastore
		{
			get
			{
				if (this.m_Datastore == null)
				{
					this.m_Datastore = new Hashtable();
				}
				return this.m_Datastore;
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x060051B1 RID: 20913 RVA: 0x001213B8 File Offset: 0x0011F5B8
		// (set) Token: 0x060051B2 RID: 20914 RVA: 0x001213CF File Offset: 0x0011F5CF
		internal IPrincipal Principal
		{
			get
			{
				if (this.m_SecurityData != null)
				{
					return this.m_SecurityData.Principal;
				}
				return null;
			}
			[SecurityCritical]
			set
			{
				this.SecurityData.Principal = value;
			}
		}

		/// <summary>Empties a data slot with the specified name.</summary>
		/// <param name="name">The name of the data slot to empty. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060051B3 RID: 20915 RVA: 0x001213DD File Offset: 0x0011F5DD
		[SecurityCritical]
		public void FreeNamedDataSlot(string name)
		{
			this.Datastore.Remove(name);
		}

		/// <summary>Retrieves an object associated with the specified name from the current instance.</summary>
		/// <returns>The object in the logical call context associated with the specified name.</returns>
		/// <param name="name">The name of the item in the call context. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060051B4 RID: 20916 RVA: 0x001213EB File Offset: 0x0011F5EB
		[SecurityCritical]
		public object GetData(string name)
		{
			return this.Datastore[name];
		}

		/// <summary>Stores the specified object in the current instance, and associates it with the specified name.</summary>
		/// <param name="name">The name with which to associate the new item in the call context. </param>
		/// <param name="data">The object to store in the call context. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060051B5 RID: 20917 RVA: 0x001213F9 File Offset: 0x0011F5F9
		[SecurityCritical]
		public void SetData(string name, object data)
		{
			this.Datastore[name] = data;
			if (name.Equals("System.Diagnostics.Trace.CorrelationManagerSlot"))
			{
				this.m_IsCorrelationMgr = true;
			}
		}

		// Token: 0x060051B6 RID: 20918 RVA: 0x0012141C File Offset: 0x0011F61C
		private Header[] InternalGetOutgoingHeaders()
		{
			Header[] sendHeaders = this._sendHeaders;
			this._sendHeaders = null;
			this._recvHeaders = null;
			return sendHeaders;
		}

		// Token: 0x060051B7 RID: 20919 RVA: 0x00121432 File Offset: 0x0011F632
		internal void InternalSetHeaders(Header[] headers)
		{
			this._sendHeaders = headers;
			this._recvHeaders = null;
		}

		// Token: 0x060051B8 RID: 20920 RVA: 0x00121442 File Offset: 0x0011F642
		internal Header[] InternalGetHeaders()
		{
			if (this._sendHeaders != null)
			{
				return this._sendHeaders;
			}
			return this._recvHeaders;
		}

		// Token: 0x060051B9 RID: 20921 RVA: 0x0012145C File Offset: 0x0011F65C
		[SecurityCritical]
		internal IPrincipal RemovePrincipalIfNotSerializable()
		{
			IPrincipal principal = this.Principal;
			if (principal != null && !principal.GetType().IsSerializable)
			{
				this.Principal = null;
			}
			return principal;
		}

		// Token: 0x060051BA RID: 20922 RVA: 0x00121488 File Offset: 0x0011F688
		[SecurityCritical]
		internal void PropagateOutgoingHeadersToMessage(IMessage msg)
		{
			Header[] array = this.InternalGetOutgoingHeaders();
			if (array != null)
			{
				IDictionary properties = msg.Properties;
				foreach (Header header in array)
				{
					if (header != null)
					{
						string propertyKeyForHeader = LogicalCallContext.GetPropertyKeyForHeader(header);
						properties[propertyKeyForHeader] = header;
					}
				}
			}
		}

		// Token: 0x060051BB RID: 20923 RVA: 0x001214D2 File Offset: 0x0011F6D2
		internal static string GetPropertyKeyForHeader(Header header)
		{
			if (header == null)
			{
				return null;
			}
			if (header.HeaderNamespace != null)
			{
				return header.Name + ", " + header.HeaderNamespace;
			}
			return header.Name;
		}

		// Token: 0x060051BC RID: 20924 RVA: 0x00121500 File Offset: 0x0011F700
		[SecurityCritical]
		internal void PropagateIncomingHeadersToCallContext(IMessage msg)
		{
			IInternalMessage internalMessage = msg as IInternalMessage;
			if (internalMessage != null && !internalMessage.HasProperties())
			{
				return;
			}
			IDictionaryEnumerator enumerator = msg.Properties.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				if (!((string)enumerator.Key).StartsWith("__", StringComparison.Ordinal) && enumerator.Value is Header)
				{
					num++;
				}
			}
			Header[] array = null;
			if (num > 0)
			{
				array = new Header[num];
				num = 0;
				enumerator.Reset();
				while (enumerator.MoveNext())
				{
					if (!((string)enumerator.Key).StartsWith("__", StringComparison.Ordinal))
					{
						Header header = enumerator.Value as Header;
						if (header != null)
						{
							array[num++] = header;
						}
					}
				}
			}
			this._recvHeaders = array;
			this._sendHeaders = null;
		}

		// Token: 0x04002AC3 RID: 10947
		private static Type s_callContextType = typeof(LogicalCallContext);

		// Token: 0x04002AC4 RID: 10948
		private const string s_CorrelationMgrSlotName = "System.Diagnostics.Trace.CorrelationManagerSlot";

		// Token: 0x04002AC5 RID: 10949
		private Hashtable m_Datastore;

		// Token: 0x04002AC6 RID: 10950
		private CallContextRemotingData m_RemotingData;

		// Token: 0x04002AC7 RID: 10951
		private CallContextSecurityData m_SecurityData;

		// Token: 0x04002AC8 RID: 10952
		private object m_HostContext;

		// Token: 0x04002AC9 RID: 10953
		private bool m_IsCorrelationMgr;

		// Token: 0x04002ACA RID: 10954
		private Header[] _sendHeaders;

		// Token: 0x04002ACB RID: 10955
		private Header[] _recvHeaders;

		// Token: 0x020007F4 RID: 2036
		internal struct Reader
		{
			// Token: 0x060051BE RID: 20926 RVA: 0x001215D1 File Offset: 0x0011F7D1
			public Reader(LogicalCallContext ctx)
			{
				this.m_ctx = ctx;
			}

			// Token: 0x17000DE8 RID: 3560
			// (get) Token: 0x060051BF RID: 20927 RVA: 0x001215DA File Offset: 0x0011F7DA
			public bool IsNull
			{
				get
				{
					return this.m_ctx == null;
				}
			}

			// Token: 0x17000DE9 RID: 3561
			// (get) Token: 0x060051C0 RID: 20928 RVA: 0x001215E5 File Offset: 0x0011F7E5
			public bool HasInfo
			{
				get
				{
					return !this.IsNull && this.m_ctx.HasInfo;
				}
			}

			// Token: 0x060051C1 RID: 20929 RVA: 0x001215FC File Offset: 0x0011F7FC
			public LogicalCallContext Clone()
			{
				return (LogicalCallContext)this.m_ctx.Clone();
			}

			// Token: 0x17000DEA RID: 3562
			// (get) Token: 0x060051C2 RID: 20930 RVA: 0x0012160E File Offset: 0x0011F80E
			public IPrincipal Principal
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ctx.Principal;
					}
					return null;
				}
			}

			// Token: 0x060051C3 RID: 20931 RVA: 0x00121625 File Offset: 0x0011F825
			[SecurityCritical]
			public object GetData(string name)
			{
				if (!this.IsNull)
				{
					return this.m_ctx.GetData(name);
				}
				return null;
			}

			// Token: 0x17000DEB RID: 3563
			// (get) Token: 0x060051C4 RID: 20932 RVA: 0x0012163D File Offset: 0x0011F83D
			public object HostContext
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ctx.HostContext;
					}
					return null;
				}
			}

			// Token: 0x04002ACC RID: 10956
			private LogicalCallContext m_ctx;
		}
	}
}
