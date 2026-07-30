using System;

namespace System.Web.Services
{
	// Token: 0x02000006 RID: 6
	internal class Soap
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000210F File Offset: 0x0000030F
		private Soap()
		{
		}

		// Token: 0x0400002E RID: 46
		internal const string XmlNamespace = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x0400002F RID: 47
		internal const string Encoding = "http://schemas.xmlsoap.org/soap/encoding/";

		// Token: 0x04000030 RID: 48
		internal const string Namespace = "http://schemas.xmlsoap.org/soap/envelope/";

		// Token: 0x04000031 RID: 49
		internal const string ConformanceClaim = "http://ws-i.org/schemas/conformanceClaim/";

		// Token: 0x04000032 RID: 50
		internal const string BasicProfile1_1 = "http://ws-i.org/profiles/basic/1.1";

		// Token: 0x04000033 RID: 51
		internal const string Action = "SOAPAction";

		// Token: 0x04000034 RID: 52
		internal const string ArrayType = "Array";

		// Token: 0x04000035 RID: 53
		internal const string Prefix = "soap";

		// Token: 0x04000036 RID: 54
		internal const string ClaimPrefix = "wsi";

		// Token: 0x04000037 RID: 55
		internal const string DimeContentType = "application/dime";

		// Token: 0x04000038 RID: 56
		internal const string SoapContentType = "text/xml";

		// Token: 0x02000007 RID: 7
		internal class Attribute
		{
			// Token: 0x06000009 RID: 9 RVA: 0x0000210F File Offset: 0x0000030F
			private Attribute()
			{
			}

			// Token: 0x04000039 RID: 57
			internal const string MustUnderstand = "mustUnderstand";

			// Token: 0x0400003A RID: 58
			internal const string Actor = "actor";

			// Token: 0x0400003B RID: 59
			internal const string EncodingStyle = "encodingStyle";

			// Token: 0x0400003C RID: 60
			internal const string Lang = "lang";

			// Token: 0x0400003D RID: 61
			internal const string ConformsTo = "conformsTo";
		}

		// Token: 0x02000008 RID: 8
		internal class Element
		{
			// Token: 0x0600000A RID: 10 RVA: 0x0000210F File Offset: 0x0000030F
			private Element()
			{
			}

			// Token: 0x0400003E RID: 62
			internal const string Envelope = "Envelope";

			// Token: 0x0400003F RID: 63
			internal const string Header = "Header";

			// Token: 0x04000040 RID: 64
			internal const string Body = "Body";

			// Token: 0x04000041 RID: 65
			internal const string Fault = "Fault";

			// Token: 0x04000042 RID: 66
			internal const string FaultActor = "faultactor";

			// Token: 0x04000043 RID: 67
			internal const string FaultCode = "faultcode";

			// Token: 0x04000044 RID: 68
			internal const string FaultDetail = "detail";

			// Token: 0x04000045 RID: 69
			internal const string FaultString = "faultstring";

			// Token: 0x04000046 RID: 70
			internal const string StackTrace = "StackTrace";

			// Token: 0x04000047 RID: 71
			internal const string Message = "Message";

			// Token: 0x04000048 RID: 72
			internal const string Claim = "Claim";
		}

		// Token: 0x02000009 RID: 9
		internal class Code
		{
			// Token: 0x0600000B RID: 11 RVA: 0x0000210F File Offset: 0x0000030F
			private Code()
			{
			}

			// Token: 0x04000049 RID: 73
			internal const string Server = "Server";

			// Token: 0x0400004A RID: 74
			internal const string VersionMismatch = "VersionMismatch";

			// Token: 0x0400004B RID: 75
			internal const string MustUnderstand = "MustUnderstand";

			// Token: 0x0400004C RID: 76
			internal const string Client = "Client";
		}
	}
}
