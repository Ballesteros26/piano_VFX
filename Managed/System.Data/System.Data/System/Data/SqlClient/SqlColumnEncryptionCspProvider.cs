using System;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003D9 RID: 985
	public class SqlColumnEncryptionCspProvider : SqlColumnEncryptionKeyStoreProvider
	{
		// Token: 0x06002E71 RID: 11889 RVA: 0x00010468 File Offset: 0x0000E668
		public SqlColumnEncryptionCspProvider()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x00056B71 File Offset: 0x00054D71
		public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x00056B71 File Offset: 0x00054D71
		public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x04001C23 RID: 7203
		public const string ProviderName = "MSSQL_CSP_PROVIDER";
	}
}
