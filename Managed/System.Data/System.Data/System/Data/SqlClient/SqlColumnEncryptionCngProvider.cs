using System;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003D8 RID: 984
	public class SqlColumnEncryptionCngProvider : SqlColumnEncryptionKeyStoreProvider
	{
		// Token: 0x06002E6E RID: 11886 RVA: 0x00010468 File Offset: 0x0000E668
		public SqlColumnEncryptionCngProvider()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x00056B71 File Offset: 0x00054D71
		public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x00056B71 File Offset: 0x00054D71
		public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x04001C22 RID: 7202
		public const string ProviderName = "MSSQL_CNG_STORE";
	}
}
