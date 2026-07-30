using System;
using Unity;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo" /> class defines key transport recipient information.        Key transport algorithms typically use the RSA algorithm, in which  an originator establishes a shared cryptographic key with a recipient by generating that key and  then transporting it to the recipient. This is in contrast to key agreement algorithms, in which the two parties that will be using a cryptographic key both take part in its generation, thereby mutually agreeing to that key.</summary>
	// Token: 0x02000025 RID: 37
	public sealed class KeyTransRecipientInfo : RecipientInfo
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00003BF4 File Offset: 0x00001DF4
		internal KeyTransRecipientInfo(byte[] encryptedKey, AlgorithmIdentifier keyEncryptionAlgorithm, SubjectIdentifier recipientIdentifier, int version)
			: base(RecipientInfoType.KeyTransport)
		{
			this._encryptedKey = encryptedKey;
			this._keyEncryptionAlgorithm = keyEncryptionAlgorithm;
			this._recipientIdentifier = recipientIdentifier;
			this._version = version;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.EncryptedKey" /> property retrieves the encrypted key for this key transport recipient.</summary>
		/// <returns>An array of byte values that represents the encrypted key.</returns>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003C1A File Offset: 0x00001E1A
		public override byte[] EncryptedKey
		{
			get
			{
				return this._encryptedKey;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.KeyEncryptionAlgorithm" /> property retrieves the key encryption algorithm used to encrypt the content encryption key.</summary>
		/// <returns> An  <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" />  object that stores the key encryption algorithm identifier.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003C22 File Offset: 0x00001E22
		public override AlgorithmIdentifier KeyEncryptionAlgorithm
		{
			get
			{
				return this._keyEncryptionAlgorithm;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.RecipientIdentifier" /> property retrieves the subject identifier associated with the encrypted content.</summary>
		/// <returns>A   <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier" />  object that  stores the identifier of the recipient taking part in the key transport.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003C2A File Offset: 0x00001E2A
		public override SubjectIdentifier RecipientIdentifier
		{
			get
			{
				return this._recipientIdentifier;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyTransRecipientInfo.Version" /> property retrieves the version of the key transport recipient. The version of the key transport recipient is automatically set for  objects in this class, and the value  implies that the recipient is taking part in a key transport algorithm.</summary>
		/// <returns>An int value that represents the version of the key transport <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object.</returns>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003C32 File Offset: 0x00001E32
		public override int Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00002FF8 File Offset: 0x000011F8
		internal KeyTransRecipientInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040000CB RID: 203
		private byte[] _encryptedKey;

		// Token: 0x040000CC RID: 204
		private AlgorithmIdentifier _keyEncryptionAlgorithm;

		// Token: 0x040000CD RID: 205
		private SubjectIdentifier _recipientIdentifier;

		// Token: 0x040000CE RID: 206
		private int _version;
	}
}
