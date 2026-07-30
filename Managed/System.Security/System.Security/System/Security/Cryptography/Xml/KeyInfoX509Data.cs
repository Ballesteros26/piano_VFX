using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Mono.Security.X509;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents an &lt;X509Data&gt; subelement of an XMLDSIG or XML Encryption &lt;KeyInfo&gt; element.</summary>
	// Token: 0x02000085 RID: 133
	public class KeyInfoX509Data : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> class.</summary>
		// Token: 0x060003D3 RID: 979 RVA: 0x00009C09 File Offset: 0x00007E09
		public KeyInfoX509Data()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> class from the specified ASN.1 DER encoding of an X.509v3 certificate.</summary>
		/// <param name="rgbCert">The ASN.1 DER encoding of an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> from.</param>
		// Token: 0x060003D4 RID: 980 RVA: 0x0000FD12 File Offset: 0x0000DF12
		public KeyInfoX509Data(byte[] rgbCert)
		{
			this.AddCertificate(new global::System.Security.Cryptography.X509Certificates.X509Certificate(rgbCert));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> class from the specified X.509v3 certificate.</summary>
		/// <param name="cert">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> from.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cert" /> parameter is null.</exception>
		// Token: 0x060003D5 RID: 981 RVA: 0x0000FD26 File Offset: 0x0000DF26
		public KeyInfoX509Data(global::System.Security.Cryptography.X509Certificates.X509Certificate cert)
		{
			this.AddCertificate(cert);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> class from the specified X.509v3 certificate.</summary>
		/// <param name="cert">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> from.</param>
		/// <param name="includeOption">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509IncludeOption" /> values that specifies how much of the certificate chain to include.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cert" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate has only a partial certificate chain.</exception>
		// Token: 0x060003D6 RID: 982 RVA: 0x0000FD38 File Offset: 0x0000DF38
		public KeyInfoX509Data(global::System.Security.Cryptography.X509Certificates.X509Certificate cert, X509IncludeOption includeOption)
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			switch (includeOption)
			{
			case X509IncludeOption.None:
			case X509IncludeOption.EndCertOnly:
				this.AddCertificate(cert);
				return;
			case X509IncludeOption.ExcludeRoot:
				this.AddCertificatesChainFrom(cert, false);
				return;
			case X509IncludeOption.WholeChain:
				this.AddCertificatesChainFrom(cert, true);
				return;
			default:
				return;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000FD8C File Offset: 0x0000DF8C
		private void AddCertificatesChainFrom(global::System.Security.Cryptography.X509Certificates.X509Certificate cert, bool root)
		{
			global::System.Security.Cryptography.X509Certificates.X509Chain x509Chain = new global::System.Security.Cryptography.X509Certificates.X509Chain();
			x509Chain.Build(new X509Certificate2(cert));
			foreach (X509ChainElement x509ChainElement in x509Chain.ChainElements)
			{
				byte[] array = x509ChainElement.Certificate.RawData;
				if (!root && new Mono.Security.X509.X509Certificate(array).IsSelfSigned)
				{
					array = null;
				}
				if (array != null)
				{
					this.AddCertificate(new global::System.Security.Cryptography.X509Certificates.X509Certificate(array));
				}
			}
		}

		/// <summary>Gets a list of the X.509v3 certificates contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>A list of the X.509 certificates contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000FDF2 File Offset: 0x0000DFF2
		public ArrayList Certificates
		{
			get
			{
				return this.X509CertificateList;
			}
		}

		/// <summary>Gets or sets the Certificate Revocation List (CRL) contained within the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>The Certificate Revocation List (CRL) contained within the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000FDFA File Offset: 0x0000DFFA
		// (set) Token: 0x060003DA RID: 986 RVA: 0x0000FE02 File Offset: 0x0000E002
		public byte[] CRL
		{
			get
			{
				return this.x509crl;
			}
			set
			{
				this.x509crl = value;
			}
		}

		/// <summary>Gets a list of <see cref="T:System.Security.Cryptography.Xml.X509IssuerSerial" /> structures that represent an issuer name and serial number pair.</summary>
		/// <returns>A list of <see cref="T:System.Security.Cryptography.Xml.X509IssuerSerial" /> structures that represent an issuer name and serial number pair.</returns>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000FE0B File Offset: 0x0000E00B
		public ArrayList IssuerSerials
		{
			get
			{
				return this.IssuerSerialList;
			}
		}

		/// <summary>Gets a list of the subject key identifiers (SKIs) contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>A list of the subject key identifiers (SKIs) contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000FE13 File Offset: 0x0000E013
		public ArrayList SubjectKeyIds
		{
			get
			{
				return this.SubjectKeyIdList;
			}
		}

		/// <summary>Gets a list of the subject names of the entities contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>A list of the subject names of the entities contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000FE1B File Offset: 0x0000E01B
		public ArrayList SubjectNames
		{
			get
			{
				return this.SubjectNameList;
			}
		}

		/// <summary>Adds the specified X.509v3 certificate to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" />.</summary>
		/// <param name="certificate">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificate" /> parameter is null.</exception>
		// Token: 0x060003DE RID: 990 RVA: 0x0000FE23 File Offset: 0x0000E023
		public void AddCertificate(global::System.Security.Cryptography.X509Certificates.X509Certificate certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (this.X509CertificateList == null)
			{
				this.X509CertificateList = new ArrayList();
			}
			this.X509CertificateList.Add(certificate);
		}

		/// <summary>Adds the specified issuer name and serial number pair to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <param name="issuerName">The issuer name portion of the pair to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object. </param>
		/// <param name="serialNumber">The serial number portion of the pair to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object. </param>
		// Token: 0x060003DF RID: 991 RVA: 0x0000FE54 File Offset: 0x0000E054
		public void AddIssuerSerial(string issuerName, string serialNumber)
		{
			if (issuerName == null)
			{
				throw new ArgumentException("issuerName");
			}
			if (this.IssuerSerialList == null)
			{
				this.IssuerSerialList = new ArrayList();
			}
			X509IssuerSerial x509IssuerSerial = new X509IssuerSerial(issuerName, serialNumber);
			this.IssuerSerialList.Add(x509IssuerSerial);
		}

		/// <summary>Adds the specified subject key identifier (SKI) byte array to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <param name="subjectKeyId">A byte array that represents the subject key identifier (SKI) to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object. </param>
		// Token: 0x060003E0 RID: 992 RVA: 0x0000FE9D File Offset: 0x0000E09D
		public void AddSubjectKeyId(byte[] subjectKeyId)
		{
			if (this.SubjectKeyIdList == null)
			{
				this.SubjectKeyIdList = new ArrayList();
			}
			this.SubjectKeyIdList.Add(subjectKeyId);
		}

		/// <summary>Adds the specified subject key identifier (SKI) string to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <param name="subjectKeyId">A string that represents the subject key identifier (SKI) to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</param>
		// Token: 0x060003E1 RID: 993 RVA: 0x0000FEC0 File Offset: 0x0000E0C0
		[ComVisible(false)]
		public void AddSubjectKeyId(string subjectKeyId)
		{
			if (this.SubjectKeyIdList == null)
			{
				this.SubjectKeyIdList = new ArrayList();
			}
			byte[] array = null;
			if (subjectKeyId != null)
			{
				array = Convert.FromBase64String(subjectKeyId);
			}
			this.SubjectKeyIdList.Add(array);
		}

		/// <summary>Adds the subject name of the entity that was issued an X.509v3 certificate to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <param name="subjectName">The name of the entity that was issued an X.509 certificate to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object. </param>
		// Token: 0x060003E2 RID: 994 RVA: 0x0000FEF9 File Offset: 0x0000E0F9
		public void AddSubjectName(string subjectName)
		{
			if (this.SubjectNameList == null)
			{
				this.SubjectNameList = new ArrayList();
			}
			this.SubjectNameList.Add(subjectName);
		}

		/// <summary>Returns an XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>An XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003E3 RID: 995 RVA: 0x0000FF1C File Offset: 0x0000E11C
		public override XmlElement GetXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("X509Data", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement.SetAttribute("xmlns", "http://www.w3.org/2000/09/xmldsig#");
			if (this.IssuerSerialList != null && this.IssuerSerialList.Count > 0)
			{
				foreach (object obj in this.IssuerSerialList)
				{
					X509IssuerSerial x509IssuerSerial = (X509IssuerSerial)obj;
					XmlElement xmlElement2 = xmlDocument.CreateElement("X509IssuerSerial", "http://www.w3.org/2000/09/xmldsig#");
					XmlElement xmlElement3 = xmlDocument.CreateElement("X509IssuerName", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement3.InnerText = x509IssuerSerial.IssuerName;
					xmlElement2.AppendChild(xmlElement3);
					XmlElement xmlElement4 = xmlDocument.CreateElement("X509SerialNumber", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement4.InnerText = x509IssuerSerial.SerialNumber;
					xmlElement2.AppendChild(xmlElement4);
					xmlElement.AppendChild(xmlElement2);
				}
			}
			if (this.SubjectKeyIdList != null && this.SubjectKeyIdList.Count > 0)
			{
				foreach (object obj2 in this.SubjectKeyIdList)
				{
					byte[] array = (byte[])obj2;
					XmlElement xmlElement5 = xmlDocument.CreateElement("X509SKI", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement5.InnerText = Convert.ToBase64String(array);
					xmlElement.AppendChild(xmlElement5);
				}
			}
			if (this.SubjectNameList != null && this.SubjectNameList.Count > 0)
			{
				foreach (object obj3 in this.SubjectNameList)
				{
					string text = (string)obj3;
					XmlElement xmlElement6 = xmlDocument.CreateElement("X509SubjectName", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement6.InnerText = text;
					xmlElement.AppendChild(xmlElement6);
				}
			}
			if (this.X509CertificateList != null && this.X509CertificateList.Count > 0)
			{
				foreach (object obj4 in this.X509CertificateList)
				{
					global::System.Security.Cryptography.X509Certificates.X509Certificate x509Certificate = (global::System.Security.Cryptography.X509Certificates.X509Certificate)obj4;
					XmlElement xmlElement7 = xmlDocument.CreateElement("X509Certificate", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement7.InnerText = Convert.ToBase64String(x509Certificate.GetRawCertData());
					xmlElement.AppendChild(xmlElement7);
				}
			}
			if (this.x509crl != null)
			{
				XmlElement xmlElement8 = xmlDocument.CreateElement("X509CRL", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement8.InnerText = Convert.ToBase64String(this.x509crl);
				xmlElement.AppendChild(xmlElement8);
			}
			return xmlElement;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> object and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object to match.</summary>
		/// <param name="element">The <see cref="T:System.Xml.XmlElement" /> object that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="element" /> parameter does not contain an &lt;X509IssuerName&gt; node.-or-The <paramref name="element" /> parameter does not contain an &lt;X509SerialNumber&gt; node.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Create" />
		/// </PermissionSet>
		// Token: 0x060003E4 RID: 996 RVA: 0x000101E8 File Offset: 0x0000E3E8
		public override void LoadXml(XmlElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (this.IssuerSerialList != null)
			{
				this.IssuerSerialList.Clear();
			}
			if (this.SubjectKeyIdList != null)
			{
				this.SubjectKeyIdList.Clear();
			}
			if (this.SubjectNameList != null)
			{
				this.SubjectNameList.Clear();
			}
			if (this.X509CertificateList != null)
			{
				this.X509CertificateList.Clear();
			}
			this.x509crl = null;
			if (element.LocalName != "X509Data" || element.NamespaceURI != "http://www.w3.org/2000/09/xmldsig#")
			{
				throw new CryptographicException("element");
			}
			XmlElement[] array = XmlSignature.GetChildElements(element, "X509IssuerSerial");
			if (array != null)
			{
				foreach (XmlElement xmlElement in array)
				{
					XmlElement childElement = XmlSignature.GetChildElement(xmlElement, "X509IssuerName", "http://www.w3.org/2000/09/xmldsig#");
					XmlElement childElement2 = XmlSignature.GetChildElement(xmlElement, "X509SerialNumber", "http://www.w3.org/2000/09/xmldsig#");
					this.AddIssuerSerial(childElement.InnerText, childElement2.InnerText);
				}
			}
			array = XmlSignature.GetChildElements(element, "X509SKI");
			if (array != null)
			{
				for (int j = 0; j < array.Length; j++)
				{
					byte[] array2 = Convert.FromBase64String(array[j].InnerXml);
					this.AddSubjectKeyId(array2);
				}
			}
			array = XmlSignature.GetChildElements(element, "X509SubjectName");
			if (array != null)
			{
				for (int k = 0; k < array.Length; k++)
				{
					this.AddSubjectName(array[k].InnerXml);
				}
			}
			array = XmlSignature.GetChildElements(element, "X509Certificate");
			if (array != null)
			{
				for (int l = 0; l < array.Length; l++)
				{
					byte[] array3 = Convert.FromBase64String(array[l].InnerXml);
					this.AddCertificate(new global::System.Security.Cryptography.X509Certificates.X509Certificate(array3));
				}
			}
			XmlElement childElement3 = XmlSignature.GetChildElement(element, "X509CRL", "http://www.w3.org/2000/09/xmldsig#");
			if (childElement3 != null)
			{
				this.x509crl = Convert.FromBase64String(childElement3.InnerXml);
			}
		}

		// Token: 0x040001E1 RID: 481
		private byte[] x509crl;

		// Token: 0x040001E2 RID: 482
		private ArrayList IssuerSerialList;

		// Token: 0x040001E3 RID: 483
		private ArrayList SubjectKeyIdList;

		// Token: 0x040001E4 RID: 484
		private ArrayList SubjectNameList;

		// Token: 0x040001E5 RID: 485
		private ArrayList X509CertificateList;
	}
}
