using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.X509;

namespace Mono.Security.Authenticode
{
	// Token: 0x020000A8 RID: 168
	public class SoftwarePublisherCertificate
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x0001EB2C File Offset: 0x0001CD2C
		public SoftwarePublisherCertificate()
		{
			this.pkcs7 = new PKCS7.SignedData();
			this.pkcs7.ContentInfo.ContentType = "1.2.840.113549.1.7.1";
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001EB54 File Offset: 0x0001CD54
		public SoftwarePublisherCertificate(byte[] data)
			: this()
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(data);
			if (contentInfo.ContentType != "1.2.840.113549.1.7.2")
			{
				throw new ArgumentException(Locale.GetText("Unsupported ContentType"));
			}
			this.pkcs7 = new PKCS7.SignedData(contentInfo.Content);
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0001EBAF File Offset: 0x0001CDAF
		public X509CertificateCollection Certificates
		{
			get
			{
				return this.pkcs7.Certificates;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x0001EBBC File Offset: 0x0001CDBC
		public ArrayList Crls
		{
			get
			{
				return this.pkcs7.Crls;
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001EBC9 File Offset: 0x0001CDC9
		public byte[] GetBytes()
		{
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo("1.2.840.113549.1.7.2");
			contentInfo.Content.Add(this.pkcs7.ASN1);
			return contentInfo.GetBytes();
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001EBF4 File Offset: 0x0001CDF4
		public static SoftwarePublisherCertificate CreateFromFile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			byte[] array = null;
			using (FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
			}
			if (array.Length < 2)
			{
				return null;
			}
			if (array[0] != 48)
			{
				try
				{
					array = SoftwarePublisherCertificate.PEM(array);
				}
				catch (Exception ex)
				{
					throw new CryptographicException("Invalid encoding", ex);
				}
			}
			return new SoftwarePublisherCertificate(array);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001EC90 File Offset: 0x0001CE90
		private static byte[] PEM(byte[] data)
		{
			string text = ((data[1] == 0) ? Encoding.Unicode.GetString(data) : Encoding.ASCII.GetString(data));
			int num = text.IndexOf("-----BEGIN PKCS7-----") + "-----BEGIN PKCS7-----".Length;
			int num2 = text.IndexOf("-----END PKCS7-----", num);
			return Convert.FromBase64String((num == -1 || num2 == -1) ? text : text.Substring(num, num2 - num));
		}

		// Token: 0x0400042A RID: 1066
		private PKCS7.SignedData pkcs7;

		// Token: 0x0400042B RID: 1067
		private const string header = "-----BEGIN PKCS7-----";

		// Token: 0x0400042C RID: 1068
		private const string footer = "-----END PKCS7-----";
	}
}
