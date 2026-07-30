using System;
using System.Configuration.Provider;
using System.Security.Cryptography;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Security
{
	// Token: 0x020004C5 RID: 1221
	internal sealed class MembershipHelper : IMembershipHelper
	{
		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x060036FF RID: 14079 RVA: 0x00090217 File Offset: 0x0008E417
		public int UserIsOnlineTimeWindow
		{
			get
			{
				return Membership.UserIsOnlineTimeWindow;
			}
		}

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x06003700 RID: 14080 RVA: 0x0009021E File Offset: 0x0008E41E
		public MembershipProviderCollection Providers
		{
			get
			{
				return Membership.Providers;
			}
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x00090228 File Offset: 0x0008E428
		private static SymmetricAlgorithm GetAlgorithm()
		{
			MachineKeySection config = MachineKeySection.Config;
			if (config.DecryptionKey.StartsWith("AutoGenerate"))
			{
				throw new ProviderException("You must explicitly specify a decryption key in the <machineKey> section when using encrypted passwords.");
			}
			SymmetricAlgorithm decryptionAlgorithm = config.GetDecryptionAlgorithm();
			if (decryptionAlgorithm == null)
			{
				throw new ProviderException(string.Format("Unsupported decryption attribute '{0}' in <machineKey> configuration section", config.Decryption));
			}
			decryptionAlgorithm.Key = config.GetDecryptionKey();
			return decryptionAlgorithm;
		}

		// Token: 0x06003702 RID: 14082 RVA: 0x00090284 File Offset: 0x0008E484
		public byte[] DecryptPassword(byte[] encodedPassword)
		{
			byte[] array;
			using (SymmetricAlgorithm algorithm = MembershipHelper.GetAlgorithm())
			{
				array = MachineKeySectionUtils.Decrypt(algorithm, encodedPassword, 0, encodedPassword.Length);
			}
			return array;
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x000902C0 File Offset: 0x0008E4C0
		public byte[] EncryptPassword(byte[] password)
		{
			byte[] array;
			using (SymmetricAlgorithm algorithm = MembershipHelper.GetAlgorithm())
			{
				array = MachineKeySectionUtils.Encrypt(algorithm, password);
			}
			return array;
		}

		// Token: 0x04001DE4 RID: 7652
		internal const int SALT_BYTES = 16;
	}
}
