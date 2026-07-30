using System;
using System.Threading;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000247 RID: 583
	internal class SNILoadHandle
	{
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x00082E60 File Offset: 0x00081060
		// (set) Token: 0x060019D9 RID: 6617 RVA: 0x00082E6D File Offset: 0x0008106D
		public SNIError LastError
		{
			get
			{
				return this._lastError.Value;
			}
			set
			{
				this._lastError.Value = value;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060019DA RID: 6618 RVA: 0x00082E7B File Offset: 0x0008107B
		public uint Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x00082E83 File Offset: 0x00081083
		public EncryptionOptions Options
		{
			get
			{
				return this._encryptionOption;
			}
		}

		// Token: 0x04001293 RID: 4755
		public static readonly SNILoadHandle SingletonInstance = new SNILoadHandle();

		// Token: 0x04001294 RID: 4756
		public readonly EncryptionOptions _encryptionOption;

		// Token: 0x04001295 RID: 4757
		public ThreadLocal<SNIError> _lastError = new ThreadLocal<SNIError>(() => new SNIError(SNIProviders.INVALID_PROV, 0U, 0U, string.Empty));

		// Token: 0x04001296 RID: 4758
		private readonly uint _status;
	}
}
