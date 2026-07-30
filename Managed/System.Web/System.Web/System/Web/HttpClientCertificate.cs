using System;
using System.Collections.Specialized;
using System.Security.Permissions;
using System.Web.Util;
using Unity;

namespace System.Web
{
	/// <summary>Provides the client certificate fields issued by the client in response to the server's request for the client's identity.</summary>
	// Token: 0x02000088 RID: 136
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HttpClientCertificate : NameValueCollection
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x0000ED60 File Offset: 0x0000CF60
		internal HttpClientCertificate(HttpWorkerRequest hwr)
		{
			this.hwr = hwr;
			this.flags = this.GetIntNoPresense("CERT_FLAGS");
			if (this.IsPresent)
			{
				this.from = hwr.GetClientCertificateValidFrom();
				this.until = hwr.GetClientCertificateValidUntil();
				return;
			}
			this.from = DateTime.Now;
			this.until = this.from;
		}

		/// <summary>Gets or sets the certificate issuer, in binary format.</summary>
		/// <returns>The certificate issuer, expressed in binary format.</returns>
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0000EDC3 File Offset: 0x0000CFC3
		public byte[] BinaryIssuer
		{
			get
			{
				return this.hwr.GetClientCertificateBinaryIssuer();
			}
		}

		/// <summary>Gets the encoding of the certificate.</summary>
		/// <returns>One of the CERT_CONTEXT.dwCertEncodingType values.</returns>
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0000EDD0 File Offset: 0x0000CFD0
		public int CertEncoding
		{
			get
			{
				return this.hwr.GetClientCertificateEncoding();
			}
		}

		/// <summary>Gets a string containing the binary stream of the entire certificate content, in ASN.1 format.</summary>
		/// <returns>The client certificate.</returns>
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0000EDDD File Offset: 0x0000CFDD
		public byte[] Certificate
		{
			get
			{
				return this.hwr.GetClientCertificate();
			}
		}

		/// <summary>Gets the unique ID for the client certificate, if provided.</summary>
		/// <returns>The client certificate ID.</returns>
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0000EDEA File Offset: 0x0000CFEA
		public string Cookie
		{
			get
			{
				return this.GetString("CERT_COOKIE");
			}
		}

		/// <summary>A set of flags that provide additional client certificate information.</summary>
		/// <returns>A set of Boolean flags.</returns>
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0000EDF7 File Offset: 0x0000CFF7
		public int Flags
		{
			get
			{
				return this.flags;
			}
		}

		/// <summary>Gets a value that indicates whether the client certificate is present.</summary>
		/// <returns>true if the client certificate is present; otherwise, false.</returns>
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0000EDFF File Offset: 0x0000CFFF
		public bool IsPresent
		{
			get
			{
				return (this.flags & 1) == 1;
			}
		}

		/// <summary>A string that contains a list of subfield values containing information about the certificate issuer.</summary>
		/// <returns>The certificate issuer's information.</returns>
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0000EE0C File Offset: 0x0000D00C
		public string Issuer
		{
			get
			{
				return this.GetString("CERT_ISSUER");
			}
		}

		/// <summary>Gets a value that indicates whether the client certificate is valid.</summary>
		/// <returns>true if the client certificate is valid; otherwise, false.</returns>
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0000EE19 File Offset: 0x0000D019
		public bool IsValid
		{
			get
			{
				return !this.IsPresent || (this.flags & 2) == 0;
			}
		}

		/// <summary>Gets the number of bits in the digital certificate key size. For example, 128.</summary>
		/// <returns>The number of bits in the key size.</returns>
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0000EE30 File Offset: 0x0000D030
		public int KeySize
		{
			get
			{
				return this.GetInt("CERT_KEYSIZE");
			}
		}

		/// <summary>Gets the public key binary value from the certificate.</summary>
		/// <returns>A byte array that contains the public key value.</returns>
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0000EE3D File Offset: 0x0000D03D
		public byte[] PublicKey
		{
			get
			{
				return this.hwr.GetClientCertificatePublicKey();
			}
		}

		/// <summary>Gets the number of bits in the server certificate private key. For example, 1024.</summary>
		/// <returns>The number of bits in the server certificate private key.</returns>
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0000EE4A File Offset: 0x0000D04A
		public int SecretKeySize
		{
			get
			{
				return this.GetInt("CERT_SECRETKEYSIZE");
			}
		}

		/// <summary>Provides the certificate serial number as an ASCII representation of hexadecimal bytes separated by hyphens. For example, 04-67-F3-02.</summary>
		/// <returns>The certificate serial number.</returns>
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0000EE57 File Offset: 0x0000D057
		public string SerialNumber
		{
			get
			{
				return this.GetString("CERT_SERIALNUMBER");
			}
		}

		/// <summary>Gets the issuer field of the server certificate.</summary>
		/// <returns>The issuer field of the server certificate.</returns>
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0000EE64 File Offset: 0x0000D064
		public string ServerIssuer
		{
			get
			{
				return this.GetString("CERT_SERVER_ISSUER");
			}
		}

		/// <summary>Gets the subject field of the server certificate.</summary>
		/// <returns>The subject field of the server certificate.</returns>
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0000EE71 File Offset: 0x0000D071
		public string ServerSubject
		{
			get
			{
				return this.GetString("CERT_SERVER_SUBJECT");
			}
		}

		/// <summary>Gets the subject field of the client certificate.</summary>
		/// <returns>The subject field of the client certificate.</returns>
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x0000EE7E File Offset: 0x0000D07E
		public string Subject
		{
			get
			{
				return this.GetString("CERT_SUBJECT");
			}
		}

		/// <summary>Gets the date when the certificate becomes valid. The date varies with international settings.</summary>
		/// <returns>The date when the certificate becomes valid.</returns>
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0000EE8B File Offset: 0x0000D08B
		public DateTime ValidFrom
		{
			get
			{
				return this.from;
			}
		}

		/// <summary>Gets the certificate expiration date.</summary>
		/// <returns>The certificate expiration date.</returns>
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0000EE93 File Offset: 0x0000D093
		public DateTime ValidUntil
		{
			get
			{
				return this.until;
			}
		}

		/// <summary>Returns individual client certificate fields by name.</summary>
		/// <returns>The value of the item specified by <paramref name="field" />.</returns>
		/// <param name="field">The item in the collection to retrieve. </param>
		// Token: 0x0600060D RID: 1549 RVA: 0x0000EE9B File Offset: 0x0000D09B
		public override string Get(string field)
		{
			return string.Empty;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0000EEA2 File Offset: 0x0000D0A2
		private int GetInt(string variable)
		{
			if (!this.IsPresent)
			{
				return 0;
			}
			return this.GetIntNoPresense(variable);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0000EEB8 File Offset: 0x0000D0B8
		private int GetIntNoPresense(string variable)
		{
			string serverVariable = this.hwr.GetServerVariable(variable);
			if (serverVariable == null)
			{
				return 0;
			}
			int num;
			try
			{
				num = int.Parse(serverVariable, Helpers.InvariantCulture);
			}
			catch
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0000EEFC File Offset: 0x0000D0FC
		private string GetString(string variable)
		{
			if (!this.IsPresent)
			{
				return string.Empty;
			}
			string serverVariable = this.hwr.GetServerVariable(variable);
			if (serverVariable != null)
			{
				return serverVariable;
			}
			return string.Empty;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal HttpClientCertificate()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000F2D RID: 3885
		private HttpWorkerRequest hwr;

		// Token: 0x04000F2E RID: 3886
		private int flags;

		// Token: 0x04000F2F RID: 3887
		private DateTime from;

		// Token: 0x04000F30 RID: 3888
		private DateTime until;
	}
}
