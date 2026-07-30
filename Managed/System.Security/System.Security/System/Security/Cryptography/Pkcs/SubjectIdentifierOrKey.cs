using System;
using Unity;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey" /> class defines the type of the identifier of a subject, such as a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> or a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" />.  The subject can be identified by the certificate issuer and serial number, the hash of the subject key, or the subject key. </summary>
	// Token: 0x02000036 RID: 54
	public sealed class SubjectIdentifierOrKey
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00004B0C File Offset: 0x00002D0C
		internal SubjectIdentifierOrKey(SubjectIdentifierOrKeyType type, object value)
		{
			this._type = type;
			this._value = value;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type" /> property retrieves the type of subject identifier or key. The subject can be identified by the certificate issuer and serial number, the hash of the subject key, or the subject key.</summary>
		/// <returns>A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKeyType" />  enumeration that specifies the type of subject identifier.</returns>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00004B22 File Offset: 0x00002D22
		public SubjectIdentifierOrKeyType Type
		{
			get
			{
				return this._type;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Value" /> property retrieves the value of the subject identifier or  key. Use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type" /> property to determine the type of subject identifier or key, and use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Value" /> property to retrieve the corresponding value.</summary>
		/// <returns>An <see cref="T:System.Object" /> object that represents the value of the subject identifier or key. This <see cref="T:System.Object" /> can be one of the following objects as determined by the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type" /> property.<see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifierOrKey.Type" /> propertyObjectIssuerAndSerialNumber<see cref="T:System.Security.Cryptography.Xml.X509IssuerSerial" />SubjectKeyIdentifier<see cref="T:System.String" />PublicKeyInfo<see cref="T:System.Security.Cryptography.Pkcs.PublicKeyInfo" /></returns>
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00004B2A File Offset: 0x00002D2A
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00002FF8 File Offset: 0x000011F8
		internal SubjectIdentifierOrKey()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040000FA RID: 250
		private SubjectIdentifierOrKeyType _type;

		// Token: 0x040000FB RID: 251
		private object _value;
	}
}
