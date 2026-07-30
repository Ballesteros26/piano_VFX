using System;
using Unity;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> class represents information about a CMS/PKCS #7 message recipient. The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> class is an abstract class inherited by the <see cref="T:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo" /> and <see cref="T:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo" /> classes.</summary>
	// Token: 0x0200002D RID: 45
	public abstract class RecipientInfo
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00004302 File Offset: 0x00002502
		internal RecipientInfo(RecipientInfoType recipInfoType)
		{
			this._type = recipInfoType;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.EncryptedKey" /> abstract property retrieves the encrypted recipient keying material.</summary>
		/// <returns>An array of byte values that contain the encrypted recipient keying material.</returns>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000E6 RID: 230
		public abstract byte[] EncryptedKey { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.KeyEncryptionAlgorithm" /> abstract property retrieves the algorithm used to perform the key establishment.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that contains the value of the algorithm used to establish the key between the originator and recipient of the CMS/PKCS #7 message.</returns>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E7 RID: 231
		public abstract AlgorithmIdentifier KeyEncryptionAlgorithm { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.RecipientIdentifier" /> abstract property retrieves the identifier of the recipient.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier" /> object that contains the identifier of the recipient.</returns>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000E8 RID: 232
		public abstract SubjectIdentifier RecipientIdentifier { get; }

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.Type" /> property retrieves the type of the recipient. The type of the recipient determines which of two major protocols is used to establish a key between the originator and the recipient of a CMS/PKCS #7 message.</summary>
		/// <returns>A value of the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoType" /> enumeration that defines the type of the recipient.</returns>
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004311 File Offset: 0x00002511
		public RecipientInfoType Type
		{
			get
			{
				return this._type;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfo.Version" /> abstract property retrieves the version of the recipient information. Derived classes automatically set this property for their objects, and the value indicates whether it is using PKCS #7 or Cryptographic Message Syntax (CMS) to protect messages. The version also implies whether the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object establishes a cryptographic key by a key agreement algorithm or a key transport algorithm.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that represents the version of the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object.</returns>
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000EA RID: 234
		public abstract int Version { get; }

		// Token: 0x060000EB RID: 235 RVA: 0x00002FF8 File Offset: 0x000011F8
		internal RecipientInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040000E2 RID: 226
		private RecipientInfoType _type;
	}
}
