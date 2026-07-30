using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200008B RID: 139
	internal class XmlSignature
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x00012254 File Offset: 0x00010454
		public static XmlElement GetChildElement(XmlElement xel, string element, string ns)
		{
			for (int i = 0; i < xel.ChildNodes.Count; i++)
			{
				XmlNode xmlNode = xel.ChildNodes[i];
				if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.LocalName == element && xmlNode.NamespaceURI == ns)
				{
					return xmlNode as XmlElement;
				}
			}
			return null;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000122B4 File Offset: 0x000104B4
		public static string GetAttributeFromElement(XmlElement xel, string attribute, string element)
		{
			XmlElement childElement = XmlSignature.GetChildElement(xel, element, "http://www.w3.org/2000/09/xmldsig#");
			if (childElement == null)
			{
				return null;
			}
			return childElement.GetAttribute(attribute);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x000122DC File Offset: 0x000104DC
		public static XmlElement[] GetChildElements(XmlElement xel, string element)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < xel.ChildNodes.Count; i++)
			{
				XmlNode xmlNode = xel.ChildNodes[i];
				if (xmlNode.NodeType == XmlNodeType.Element && xmlNode.LocalName == element && xmlNode.NamespaceURI == "http://www.w3.org/2000/09/xmldsig#")
				{
					arrayList.Add(xmlNode);
				}
			}
			return arrayList.ToArray(typeof(XmlElement)) as XmlElement[];
		}

		// Token: 0x0400021E RID: 542
		public const string NamespaceURI = "http://www.w3.org/2000/09/xmldsig#";

		// Token: 0x0400021F RID: 543
		public const string Prefix = "ds";

		// Token: 0x0200008C RID: 140
		public class ElementNames
		{
			// Token: 0x04000220 RID: 544
			public const string CanonicalizationMethod = "CanonicalizationMethod";

			// Token: 0x04000221 RID: 545
			public const string DigestMethod = "DigestMethod";

			// Token: 0x04000222 RID: 546
			public const string DigestValue = "DigestValue";

			// Token: 0x04000223 RID: 547
			public const string DSAKeyValue = "DSAKeyValue";

			// Token: 0x04000224 RID: 548
			public const string EncryptedKey = "EncryptedKey";

			// Token: 0x04000225 RID: 549
			public const string HMACOutputLength = "HMACOutputLength";

			// Token: 0x04000226 RID: 550
			public const string KeyInfo = "KeyInfo";

			// Token: 0x04000227 RID: 551
			public const string KeyName = "KeyName";

			// Token: 0x04000228 RID: 552
			public const string KeyValue = "KeyValue";

			// Token: 0x04000229 RID: 553
			public const string Manifest = "Manifest";

			// Token: 0x0400022A RID: 554
			public const string Object = "Object";

			// Token: 0x0400022B RID: 555
			public const string Reference = "Reference";

			// Token: 0x0400022C RID: 556
			public const string RetrievalMethod = "RetrievalMethod";

			// Token: 0x0400022D RID: 557
			public const string RSAKeyValue = "RSAKeyValue";

			// Token: 0x0400022E RID: 558
			public const string Signature = "Signature";

			// Token: 0x0400022F RID: 559
			public const string SignatureMethod = "SignatureMethod";

			// Token: 0x04000230 RID: 560
			public const string SignatureValue = "SignatureValue";

			// Token: 0x04000231 RID: 561
			public const string SignedInfo = "SignedInfo";

			// Token: 0x04000232 RID: 562
			public const string Transform = "Transform";

			// Token: 0x04000233 RID: 563
			public const string Transforms = "Transforms";

			// Token: 0x04000234 RID: 564
			public const string X509Data = "X509Data";

			// Token: 0x04000235 RID: 565
			public const string X509IssuerSerial = "X509IssuerSerial";

			// Token: 0x04000236 RID: 566
			public const string X509IssuerName = "X509IssuerName";

			// Token: 0x04000237 RID: 567
			public const string X509SerialNumber = "X509SerialNumber";

			// Token: 0x04000238 RID: 568
			public const string X509SKI = "X509SKI";

			// Token: 0x04000239 RID: 569
			public const string X509SubjectName = "X509SubjectName";

			// Token: 0x0400023A RID: 570
			public const string X509Certificate = "X509Certificate";

			// Token: 0x0400023B RID: 571
			public const string X509CRL = "X509CRL";
		}

		// Token: 0x0200008D RID: 141
		public class AttributeNames
		{
			// Token: 0x0400023C RID: 572
			public const string Algorithm = "Algorithm";

			// Token: 0x0400023D RID: 573
			public const string Encoding = "Encoding";

			// Token: 0x0400023E RID: 574
			public const string Id = "Id";

			// Token: 0x0400023F RID: 575
			public const string MimeType = "MimeType";

			// Token: 0x04000240 RID: 576
			public const string Type = "Type";

			// Token: 0x04000241 RID: 577
			public const string URI = "URI";
		}

		// Token: 0x0200008E RID: 142
		public class Uri
		{
			// Token: 0x04000242 RID: 578
			public const string Manifest = "http://www.w3.org/2000/09/xmldsig#Manifest";
		}
	}
}
