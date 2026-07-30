using System;
using Unity;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier" /> class defines the type of the identifier of a subject, such as a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> or a <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" />.  The subject can be identified by the certificate issuer and serial number or the subject key.</summary>
	// Token: 0x02000035 RID: 53
	public sealed class SubjectIdentifier
	{
		// Token: 0x06000133 RID: 307 RVA: 0x00004AE6 File Offset: 0x00002CE6
		internal SubjectIdentifier(SubjectIdentifierType type, object value)
		{
			this._type = type;
			this._value = value;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type" /> property retrieves the type of subject identifier. The subject can be identified by the certificate issuer and serial number or the subject key.</summary>
		/// <returns>A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" />  enumeration that identifies the type of subject.</returns>
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00004AFC File Offset: 0x00002CFC
		public SubjectIdentifierType Type
		{
			get
			{
				return this._type;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Value" /> property retrieves the value of the subject identifier. Use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type" /> property to determine the type of subject identifier, and use the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Value" /> property to retrieve the corresponding value.</summary>
		/// <returns>An <see cref="T:System.Object" /> object that represents the value of the subject identifier. This <see cref="T:System.Object" /> can be one of the following objects as determined by the <see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type" /> property.<see cref="P:System.Security.Cryptography.Pkcs.SubjectIdentifier.Type" /> propertyObjectIssuerAndSerialNumber<see cref="T:System.Security.Cryptography.Xml.X509IssuerSerial" />SubjectKeyIdentifier<see cref="T:System.String" /></returns>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00004B04 File Offset: 0x00002D04
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00002FF8 File Offset: 0x000011F8
		internal SubjectIdentifier()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040000F8 RID: 248
		private SubjectIdentifierType _type;

		// Token: 0x040000F9 RID: 249
		private object _value;
	}
}
