using System;
using System.Collections;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020007F1 RID: 2033
	internal class IllogicalCallContext
	{
		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06005198 RID: 20888 RVA: 0x00120ED0 File Offset: 0x0011F0D0
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

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06005199 RID: 20889 RVA: 0x00120EEB File Offset: 0x0011F0EB
		// (set) Token: 0x0600519A RID: 20890 RVA: 0x00120EF3 File Offset: 0x0011F0F3
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

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x0600519B RID: 20891 RVA: 0x00120EFC File Offset: 0x0011F0FC
		internal bool HasUserData
		{
			get
			{
				return this.m_Datastore != null && this.m_Datastore.Count > 0;
			}
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x00120F16 File Offset: 0x0011F116
		public void FreeNamedDataSlot(string name)
		{
			this.Datastore.Remove(name);
		}

		// Token: 0x0600519D RID: 20893 RVA: 0x00120F24 File Offset: 0x0011F124
		public object GetData(string name)
		{
			return this.Datastore[name];
		}

		// Token: 0x0600519E RID: 20894 RVA: 0x00120F32 File Offset: 0x0011F132
		public void SetData(string name, object data)
		{
			this.Datastore[name] = data;
		}

		// Token: 0x0600519F RID: 20895 RVA: 0x00120F44 File Offset: 0x0011F144
		public IllogicalCallContext CreateCopy()
		{
			IllogicalCallContext illogicalCallContext = new IllogicalCallContext();
			illogicalCallContext.HostContext = this.HostContext;
			if (this.HasUserData)
			{
				IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
				while (enumerator.MoveNext())
				{
					illogicalCallContext.Datastore[(string)enumerator.Key] = enumerator.Value;
				}
			}
			return illogicalCallContext;
		}

		// Token: 0x04002AC0 RID: 10944
		private Hashtable m_Datastore;

		// Token: 0x04002AC1 RID: 10945
		private object m_HostContext;

		// Token: 0x020007F2 RID: 2034
		internal struct Reader
		{
			// Token: 0x060051A1 RID: 20897 RVA: 0x00120F9E File Offset: 0x0011F19E
			public Reader(IllogicalCallContext ctx)
			{
				this.m_ctx = ctx;
			}

			// Token: 0x17000DDF RID: 3551
			// (get) Token: 0x060051A2 RID: 20898 RVA: 0x00120FA7 File Offset: 0x0011F1A7
			public bool IsNull
			{
				get
				{
					return this.m_ctx == null;
				}
			}

			// Token: 0x060051A3 RID: 20899 RVA: 0x00120FB2 File Offset: 0x0011F1B2
			[SecurityCritical]
			public object GetData(string name)
			{
				if (!this.IsNull)
				{
					return this.m_ctx.GetData(name);
				}
				return null;
			}

			// Token: 0x17000DE0 RID: 3552
			// (get) Token: 0x060051A4 RID: 20900 RVA: 0x00120FCA File Offset: 0x0011F1CA
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

			// Token: 0x04002AC2 RID: 10946
			private IllogicalCallContext m_ctx;
		}
	}
}
