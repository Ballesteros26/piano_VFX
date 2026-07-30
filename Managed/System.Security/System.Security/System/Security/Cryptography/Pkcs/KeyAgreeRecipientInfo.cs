using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo" /> class defines key agreement recipient information. Key agreement algorithms typically use the Diffie-Hellman key agreement algorithm, in which the two parties that establish a shared cryptographic key both take part in its generation and, by definition, agree on that key. This is in contrast to key transport algorithms, in which one party generates the key unilaterally and sends, or transports it, to the other party.</summary>
	// Token: 0x02000024 RID: 36
	[MonoTODO]
	public sealed class KeyAgreeRecipientInfo : RecipientInfo
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00003BDE File Offset: 0x00001DDE
		internal KeyAgreeRecipientInfo()
			: base(RecipientInfoType.KeyAgreement)
		{
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.Date" /> property retrieves the date and time of the start of the key agreement protocol by the originator.</summary>
		/// <returns>The date and time of the start of the key agreement protocol by the originator.</returns>
		/// <exception cref="T:System.InvalidOperationException">The recipient identifier type is not a subject key identifier.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003BE7 File Offset: 0x00001DE7
		public DateTime Date
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.EncryptedKey" /> property retrieves the encrypted recipient keying material.</summary>
		/// <returns>An array of byte values that contain the encrypted recipient keying material.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003BEE File Offset: 0x00001DEE
		public override byte[] EncryptedKey
		{
			get
			{
				return null;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.KeyEncryptionAlgorithm" /> property retrieves the algorithm used to perform the key agreement.</summary>
		/// <returns>The value of the algorithm used to perform the key agreement.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003BEE File Offset: 0x00001DEE
		public override AlgorithmIdentifier KeyEncryptionAlgorithm
		{
			get
			{
				return null;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.OriginatorIdentifierOrKey" /> property retrieves information about the originator of the key agreement for key agreement algorithms that warrant it.</summary>
		/// <returns>An object that contains information about the originator of the key agreement.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003BEE File Offset: 0x00001DEE
		public SubjectIdentifierOrKey OriginatorIdentifierOrKey
		{
			get
			{
				return null;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.OtherKeyAttribute" /> property retrieves attributes of the keying material.</summary>
		/// <returns>The attributes of the keying material.</returns>
		/// <exception cref="T:System.InvalidOperationException">The recipient identifier type is not a subject key identifier.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003BEE File Offset: 0x00001DEE
		public CryptographicAttributeObject OtherKeyAttribute
		{
			get
			{
				return null;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.RecipientIdentifier" /> property retrieves the identifier of the recipient.</summary>
		/// <returns>The identifier of the recipient.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003BEE File Offset: 0x00001DEE
		public override SubjectIdentifier RecipientIdentifier
		{
			get
			{
				return null;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo.Version" /> property retrieves the version of the key agreement recipient. This is automatically set for  objects in this class, and the value  implies that the recipient is taking part in a key agreement algorithm.</summary>
		/// <returns>The version of the <see cref="T:System.Security.Cryptography.Pkcs.KeyAgreeRecipientInfo" /> object.</returns>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003BF1 File Offset: 0x00001DF1
		public override int Version
		{
			get
			{
				return 0;
			}
		}
	}
}
