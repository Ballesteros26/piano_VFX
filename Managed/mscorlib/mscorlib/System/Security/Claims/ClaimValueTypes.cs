using System;
using System.Runtime.InteropServices;

namespace System.Security.Claims
{
	/// <summary>Defines claim value types according to the type URIs defined by W3C and OASIS. This class cannot be inherited.</summary>
	// Token: 0x02000634 RID: 1588
	[ComVisible(false)]
	public static class ClaimValueTypes
	{
		// Token: 0x04002356 RID: 9046
		private const string XmlSchemaNamespace = "http://www.w3.org/2001/XMLSchema";

		/// <summary>A URI that represents the base64Binary XML data type.</summary>
		// Token: 0x04002357 RID: 9047
		public const string Base64Binary = "http://www.w3.org/2001/XMLSchema#base64Binary";

		/// <summary>A URI that that represents the base64Octet XML data type.</summary>
		// Token: 0x04002358 RID: 9048
		public const string Base64Octet = "http://www.w3.org/2001/XMLSchema#base64Octet";

		/// <summary>A URI that represents the boolean XML data type.</summary>
		// Token: 0x04002359 RID: 9049
		public const string Boolean = "http://www.w3.org/2001/XMLSchema#boolean";

		/// <summary>A URI that represents the date XML data type.</summary>
		// Token: 0x0400235A RID: 9050
		public const string Date = "http://www.w3.org/2001/XMLSchema#date";

		/// <summary>A URI that represents the dateTime XML data type.</summary>
		// Token: 0x0400235B RID: 9051
		public const string DateTime = "http://www.w3.org/2001/XMLSchema#dateTime";

		/// <summary>A URI that represents the double XML data type.</summary>
		// Token: 0x0400235C RID: 9052
		public const string Double = "http://www.w3.org/2001/XMLSchema#double";

		/// <summary>A URI that represents the fqbn XML data type.</summary>
		// Token: 0x0400235D RID: 9053
		public const string Fqbn = "http://www.w3.org/2001/XMLSchema#fqbn";

		/// <summary>A URI that represents the hexBinary XML data type.</summary>
		// Token: 0x0400235E RID: 9054
		public const string HexBinary = "http://www.w3.org/2001/XMLSchema#hexBinary";

		/// <summary>A URI that represents the integer XML data type.</summary>
		// Token: 0x0400235F RID: 9055
		public const string Integer = "http://www.w3.org/2001/XMLSchema#integer";

		/// <summary>A URI that represents the integer32 XML data type.</summary>
		// Token: 0x04002360 RID: 9056
		public const string Integer32 = "http://www.w3.org/2001/XMLSchema#integer32";

		/// <summary>A URI that represents the integer64 XML data type.</summary>
		// Token: 0x04002361 RID: 9057
		public const string Integer64 = "http://www.w3.org/2001/XMLSchema#integer64";

		/// <summary>A URI that represents the sid XML data type.</summary>
		// Token: 0x04002362 RID: 9058
		public const string Sid = "http://www.w3.org/2001/XMLSchema#sid";

		/// <summary>A URI that represents the string XML data type.</summary>
		// Token: 0x04002363 RID: 9059
		public const string String = "http://www.w3.org/2001/XMLSchema#string";

		/// <summary>A URI that represents the time XML data type.</summary>
		// Token: 0x04002364 RID: 9060
		public const string Time = "http://www.w3.org/2001/XMLSchema#time";

		/// <summary>A URI that represents the uinteger32 XML data type.</summary>
		// Token: 0x04002365 RID: 9061
		public const string UInteger32 = "http://www.w3.org/2001/XMLSchema#uinteger32";

		/// <summary>A URI that represents the uinteger64 XML data type.</summary>
		// Token: 0x04002366 RID: 9062
		public const string UInteger64 = "http://www.w3.org/2001/XMLSchema#uinteger64";

		// Token: 0x04002367 RID: 9063
		private const string SoapSchemaNamespace = "http://schemas.xmlsoap.org/";

		/// <summary>A URI that represents the dns SOAP data type.</summary>
		// Token: 0x04002368 RID: 9064
		public const string DnsName = "http://schemas.xmlsoap.org/claims/dns";

		/// <summary>A URI that represents the emailaddress SOAP data type.</summary>
		// Token: 0x04002369 RID: 9065
		public const string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

		/// <summary>A URI that represents the rsa SOAP data type.</summary>
		// Token: 0x0400236A RID: 9066
		public const string Rsa = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa";

		/// <summary>A URI that represents the UPN SOAP data type.</summary>
		// Token: 0x0400236B RID: 9067
		public const string UpnName = "http://schemas.xmlsoap.org/claims/UPN";

		// Token: 0x0400236C RID: 9068
		private const string XmlSignatureConstantsNamespace = "http://www.w3.org/2000/09/xmldsig#";

		/// <summary>A URI that represents the DSAKeyValue XML Signature data type.</summary>
		// Token: 0x0400236D RID: 9069
		public const string DsaKeyValue = "http://www.w3.org/2000/09/xmldsig#DSAKeyValue";

		/// <summary>A URI that represents the KeyInfo XML Signature data type.</summary>
		// Token: 0x0400236E RID: 9070
		public const string KeyInfo = "http://www.w3.org/2000/09/xmldsig#KeyInfo";

		/// <summary>A URI that represents the RSAKeyValue XML Signature data type.</summary>
		// Token: 0x0400236F RID: 9071
		public const string RsaKeyValue = "http://www.w3.org/2000/09/xmldsig#RSAKeyValue";

		// Token: 0x04002370 RID: 9072
		private const string XQueryOperatorsNameSpace = "http://www.w3.org/TR/2002/WD-xquery-operators-20020816";

		/// <summary>A URI that represents the daytimeDuration XQuery data type.</summary>
		// Token: 0x04002371 RID: 9073
		public const string DaytimeDuration = "http://www.w3.org/TR/2002/WD-xquery-operators-20020816#dayTimeDuration";

		/// <summary>A URI that represents the yearMonthDuration XQuery data type.</summary>
		// Token: 0x04002372 RID: 9074
		public const string YearMonthDuration = "http://www.w3.org/TR/2002/WD-xquery-operators-20020816#yearMonthDuration";

		// Token: 0x04002373 RID: 9075
		private const string Xacml10Namespace = "urn:oasis:names:tc:xacml:1.0";

		/// <summary>A URI that represents the rfc822Name XACML 1.0 data type.</summary>
		// Token: 0x04002374 RID: 9076
		public const string Rfc822Name = "urn:oasis:names:tc:xacml:1.0:data-type:rfc822Name";

		/// <summary>A URI that represents the x500Name XACML 1.0 data type.</summary>
		// Token: 0x04002375 RID: 9077
		public const string X500Name = "urn:oasis:names:tc:xacml:1.0:data-type:x500Name";
	}
}
