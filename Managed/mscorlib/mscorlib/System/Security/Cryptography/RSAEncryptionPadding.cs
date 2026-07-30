using System;
using Unity;

namespace System.Security.Cryptography
{
	// Token: 0x0200063E RID: 1598
	public sealed class RSAEncryptionPadding : IEquatable<RSAEncryptionPadding>
	{
		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06004573 RID: 17779 RVA: 0x000F4851 File Offset: 0x000F2A51
		public static RSAEncryptionPadding Pkcs1
		{
			get
			{
				return RSAEncryptionPadding.s_pkcs1;
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06004574 RID: 17780 RVA: 0x000F4858 File Offset: 0x000F2A58
		public static RSAEncryptionPadding OaepSHA1
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA1;
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06004575 RID: 17781 RVA: 0x000F485F File Offset: 0x000F2A5F
		public static RSAEncryptionPadding OaepSHA256
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA256;
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06004576 RID: 17782 RVA: 0x000F4866 File Offset: 0x000F2A66
		public static RSAEncryptionPadding OaepSHA384
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA384;
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06004577 RID: 17783 RVA: 0x000F486D File Offset: 0x000F2A6D
		public static RSAEncryptionPadding OaepSHA512
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA512;
			}
		}

		// Token: 0x06004578 RID: 17784 RVA: 0x000F4874 File Offset: 0x000F2A74
		private RSAEncryptionPadding(RSAEncryptionPaddingMode mode, HashAlgorithmName oaepHashAlgorithm)
		{
			this._mode = mode;
			this._oaepHashAlgorithm = oaepHashAlgorithm;
		}

		// Token: 0x06004579 RID: 17785 RVA: 0x000F488A File Offset: 0x000F2A8A
		public static RSAEncryptionPadding CreateOaep(HashAlgorithmName hashAlgorithm)
		{
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException(Environment.GetResourceString("The hash algorithm name cannot be null or empty."), "hashAlgorithm");
			}
			return new RSAEncryptionPadding(RSAEncryptionPaddingMode.Oaep, hashAlgorithm);
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x0600457A RID: 17786 RVA: 0x000F48B6 File Offset: 0x000F2AB6
		public RSAEncryptionPaddingMode Mode
		{
			get
			{
				return this._mode;
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x0600457B RID: 17787 RVA: 0x000F48BE File Offset: 0x000F2ABE
		public HashAlgorithmName OaepHashAlgorithm
		{
			get
			{
				return this._oaepHashAlgorithm;
			}
		}

		// Token: 0x0600457C RID: 17788 RVA: 0x000F48C6 File Offset: 0x000F2AC6
		public override int GetHashCode()
		{
			return RSAEncryptionPadding.CombineHashCodes(this._mode.GetHashCode(), this._oaepHashAlgorithm.GetHashCode());
		}

		// Token: 0x0600457D RID: 17789 RVA: 0x000224D3 File Offset: 0x000206D3
		private static int CombineHashCodes(int h1, int h2)
		{
			return ((h1 << 5) + h1) ^ h2;
		}

		// Token: 0x0600457E RID: 17790 RVA: 0x000F48EF File Offset: 0x000F2AEF
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RSAEncryptionPadding);
		}

		// Token: 0x0600457F RID: 17791 RVA: 0x000F48FD File Offset: 0x000F2AFD
		public bool Equals(RSAEncryptionPadding other)
		{
			return other != null && this._mode == other._mode && this._oaepHashAlgorithm == other._oaepHashAlgorithm;
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x000F4929 File Offset: 0x000F2B29
		public static bool operator ==(RSAEncryptionPadding left, RSAEncryptionPadding right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x06004581 RID: 17793 RVA: 0x000F493A File Offset: 0x000F2B3A
		public static bool operator !=(RSAEncryptionPadding left, RSAEncryptionPadding right)
		{
			return !(left == right);
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x000F4946 File Offset: 0x000F2B46
		public override string ToString()
		{
			return this._mode.ToString() + this._oaepHashAlgorithm.Name;
		}

		// Token: 0x06004584 RID: 17796 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal RSAEncryptionPadding()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040023BC RID: 9148
		private static readonly RSAEncryptionPadding s_pkcs1 = new RSAEncryptionPadding(RSAEncryptionPaddingMode.Pkcs1, default(HashAlgorithmName));

		// Token: 0x040023BD RID: 9149
		private static readonly RSAEncryptionPadding s_oaepSHA1 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA1);

		// Token: 0x040023BE RID: 9150
		private static readonly RSAEncryptionPadding s_oaepSHA256 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA256);

		// Token: 0x040023BF RID: 9151
		private static readonly RSAEncryptionPadding s_oaepSHA384 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA384);

		// Token: 0x040023C0 RID: 9152
		private static readonly RSAEncryptionPadding s_oaepSHA512 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA512);

		// Token: 0x040023C1 RID: 9153
		private RSAEncryptionPaddingMode _mode;

		// Token: 0x040023C2 RID: 9154
		private HashAlgorithmName _oaepHashAlgorithm;
	}
}
