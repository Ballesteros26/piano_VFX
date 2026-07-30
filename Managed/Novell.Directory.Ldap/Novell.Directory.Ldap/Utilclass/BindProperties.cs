using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000043 RID: 67
	public class BindProperties
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000CE67 File Offset: 0x0000B067
		public virtual int ProtocolVersion
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000CE6F File Offset: 0x0000B06F
		public virtual string AuthenticationDN
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000CE77 File Offset: 0x0000B077
		public virtual string AuthenticationMethod
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000CE7F File Offset: 0x0000B07F
		public virtual Hashtable SaslBindProperties
		{
			get
			{
				return this.bindProperties;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000CE87 File Offset: 0x0000B087
		public virtual object SaslCallbackHandler
		{
			get
			{
				return this.bindCallbackHandler;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000CE8F File Offset: 0x0000B08F
		public virtual bool Anonymous
		{
			get
			{
				return this.anonymous;
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000CE97 File Offset: 0x0000B097
		public BindProperties(int version, string dn, string method, bool anonymous, Hashtable bindProperties, object bindCallbackHandler)
		{
			this.version = version;
			this.dn = dn;
			this.method = method;
			this.anonymous = anonymous;
			this.bindProperties = bindProperties;
			this.bindCallbackHandler = bindCallbackHandler;
		}

		// Token: 0x04000197 RID: 407
		private int version = 3;

		// Token: 0x04000198 RID: 408
		private string dn;

		// Token: 0x04000199 RID: 409
		private string method;

		// Token: 0x0400019A RID: 410
		private bool anonymous;

		// Token: 0x0400019B RID: 411
		private Hashtable bindProperties;

		// Token: 0x0400019C RID: 412
		private object bindCallbackHandler;
	}
}
