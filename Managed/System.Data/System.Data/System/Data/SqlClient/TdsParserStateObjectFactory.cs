using System;
using System.Data.SqlClient.SNI;

namespace System.Data.SqlClient
{
	// Token: 0x0200022F RID: 559
	internal sealed class TdsParserStateObjectFactory
	{
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x0000EF2B File Offset: 0x0000D12B
		public static bool UseManagedSNI
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x00080DC5 File Offset: 0x0007EFC5
		public EncryptionOptions EncryptionOptions
		{
			get
			{
				return SNILoadHandle.SingletonInstance.Options;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x00080DD1 File Offset: 0x0007EFD1
		public uint SNIStatus
		{
			get
			{
				return SNILoadHandle.SingletonInstance.Status;
			}
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x00080DDD File Offset: 0x0007EFDD
		public TdsParserStateObject CreateTdsParserStateObject(TdsParser parser)
		{
			return new TdsParserStateObjectManaged(parser);
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00080DE5 File Offset: 0x0007EFE5
		internal TdsParserStateObject CreateSessionObject(TdsParser tdsParser, TdsParserStateObject _pMarsPhysicalConObj, bool v)
		{
			return new TdsParserStateObjectManaged(tdsParser, _pMarsPhysicalConObj, true);
		}

		// Token: 0x04001226 RID: 4646
		public static readonly TdsParserStateObjectFactory Singleton = new TdsParserStateObjectFactory();
	}
}
