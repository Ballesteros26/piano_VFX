using System;
using Unity;

namespace System.Data.SqlClient
{
	// Token: 0x020003D5 RID: 981
	public abstract class SqlColumnEncryptionKeyStoreProvider
	{
		// Token: 0x06002E59 RID: 11865 RVA: 0x00010468 File Offset: 0x0000E668
		protected SqlColumnEncryptionKeyStoreProvider()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002E5A RID: 11866
		public abstract byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey);

		// Token: 0x06002E5B RID: 11867
		public abstract byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey);
	}
}
