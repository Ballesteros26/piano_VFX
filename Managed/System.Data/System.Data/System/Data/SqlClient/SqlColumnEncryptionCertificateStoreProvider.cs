using System;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003D7 RID: 983
	public class SqlColumnEncryptionCertificateStoreProvider : SqlColumnEncryptionKeyStoreProvider
	{
		// Token: 0x06002E6B RID: 11883 RVA: 0x00010468 File Offset: 0x0000E668
		public SqlColumnEncryptionCertificateStoreProvider()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x00056B71 File Offset: 0x00054D71
		public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x00056B71 File Offset: 0x00054D71
		public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x04001C21 RID: 7201
		public const string ProviderName = "MSSQL_CERTIFICATE_STORE";
	}
}
