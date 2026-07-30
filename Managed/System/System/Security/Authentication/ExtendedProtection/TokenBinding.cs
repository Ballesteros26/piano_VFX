using System;
using Unity;

namespace System.Security.Authentication.ExtendedProtection
{
	// Token: 0x02000382 RID: 898
	public class TokenBinding
	{
		// Token: 0x06001B58 RID: 7000 RVA: 0x0006D4EC File Offset: 0x0006B6EC
		internal TokenBinding(TokenBindingType bindingType, byte[] rawData)
		{
			this.BindingType = bindingType;
			this._rawTokenBindingId = rawData;
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x0006D502 File Offset: 0x0006B702
		public byte[] GetRawTokenBindingId()
		{
			if (this._rawTokenBindingId == null)
			{
				return null;
			}
			return (byte[])this._rawTokenBindingId.Clone();
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x0006D51E File Offset: 0x0006B71E
		// (set) Token: 0x06001B5B RID: 7003 RVA: 0x0006D526 File Offset: 0x0006B726
		public TokenBindingType BindingType { get; private set; }

		// Token: 0x06001B5C RID: 7004 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal TokenBinding()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040018C6 RID: 6342
		private byte[] _rawTokenBindingId;
	}
}
