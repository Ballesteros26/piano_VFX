using System;
using Unity;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.PublicKeyInfo" /> class represents information associated with a public key.</summary>
	// Token: 0x0200002C RID: 44
	public sealed class PublicKeyInfo
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x000042DC File Offset: 0x000024DC
		internal PublicKeyInfo(AlgorithmIdentifier algorithm, byte[] key)
		{
			this._algorithm = algorithm;
			this._key = key;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.PublicKeyInfo.Algorithm" /> property retrieves the algorithm identifier associated with the public key.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" />  object that represents the algorithm.</returns>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000042F2 File Offset: 0x000024F2
		public AlgorithmIdentifier Algorithm
		{
			get
			{
				return this._algorithm;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.PublicKeyInfo.KeyValue" /> property retrieves the value of the encoded public component of the public key pair.</summary>
		/// <returns>An array of byte values  that represents the encoded public component of the public key pair.</returns>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000042FA File Offset: 0x000024FA
		public byte[] KeyValue
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00002FF8 File Offset: 0x000011F8
		internal PublicKeyInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040000E0 RID: 224
		private AlgorithmIdentifier _algorithm;

		// Token: 0x040000E1 RID: 225
		private byte[] _key;
	}
}
