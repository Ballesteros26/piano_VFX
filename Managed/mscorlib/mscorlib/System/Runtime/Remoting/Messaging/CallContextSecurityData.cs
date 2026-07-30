using System;
using System.Security.Principal;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020007F5 RID: 2037
	[Serializable]
	internal class CallContextSecurityData : ICloneable
	{
		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x060051C5 RID: 20933 RVA: 0x00121654 File Offset: 0x0011F854
		// (set) Token: 0x060051C6 RID: 20934 RVA: 0x0012165C File Offset: 0x0011F85C
		internal IPrincipal Principal
		{
			get
			{
				return this._principal;
			}
			set
			{
				this._principal = value;
			}
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x060051C7 RID: 20935 RVA: 0x00121665 File Offset: 0x0011F865
		internal bool HasInfo
		{
			get
			{
				return this._principal != null;
			}
		}

		// Token: 0x060051C8 RID: 20936 RVA: 0x00121670 File Offset: 0x0011F870
		public object Clone()
		{
			return new CallContextSecurityData
			{
				_principal = this._principal
			};
		}

		// Token: 0x04002ACD RID: 10957
		private IPrincipal _principal;
	}
}
