using System;
using System.Security.Authentication.ExtendedProtection;

namespace System.Web
{
	// Token: 0x02000043 RID: 67
	internal sealed class HttpChannelBindingToken : ChannelBinding
	{
		// Token: 0x060003B9 RID: 953 RVA: 0x00007246 File Offset: 0x00005446
		internal HttpChannelBindingToken(IntPtr token, int tokenSize)
		{
			base.SetHandle(token);
			this._size = tokenSize;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000725C File Offset: 0x0000545C
		protected override bool ReleaseHandle()
		{
			base.SetHandle(IntPtr.Zero);
			this._size = 0;
			return true;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060003BB RID: 955 RVA: 0x00007271 File Offset: 0x00005471
		public override int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x04000DA1 RID: 3489
		private int _size;
	}
}
