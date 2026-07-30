using System;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;X509IssuerSerial&gt; element of an XML digital signature.</summary>
	// Token: 0x0200008A RID: 138
	public struct X509IssuerSerial
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x00012220 File Offset: 0x00010420
		internal X509IssuerSerial(string issuer, string serial)
		{
			this._issuerName = issuer;
			this._serialNumber = serial;
		}

		/// <summary>Gets or sets an X.509 certificate issuer's distinguished name.</summary>
		/// <returns>An X.509 certificate issuer's distinguished name.</returns>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00012230 File Offset: 0x00010430
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x00012238 File Offset: 0x00010438
		public string IssuerName
		{
			get
			{
				return this._issuerName;
			}
			set
			{
				this._issuerName = value;
			}
		}

		/// <summary>Gets or sets an X.509 certificate issuer's serial number.</summary>
		/// <returns>An X.509 certificate issuer's serial number.</returns>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00012241 File Offset: 0x00010441
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x00012249 File Offset: 0x00010449
		public string SerialNumber
		{
			get
			{
				return this._serialNumber;
			}
			set
			{
				this._serialNumber = value;
			}
		}

		// Token: 0x0400021C RID: 540
		private string _issuerName;

		// Token: 0x0400021D RID: 541
		private string _serialNumber;
	}
}
