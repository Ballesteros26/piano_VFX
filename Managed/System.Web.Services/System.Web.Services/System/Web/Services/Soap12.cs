using System;

namespace System.Web.Services
{
	// Token: 0x0200000A RID: 10
	internal sealed class Soap12
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000210F File Offset: 0x0000030F
		private Soap12()
		{
		}

		// Token: 0x0400004D RID: 77
		internal const string Namespace = "http://www.w3.org/2003/05/soap-envelope";

		// Token: 0x0400004E RID: 78
		internal const string Encoding = "http://www.w3.org/2003/05/soap-encoding";

		// Token: 0x0400004F RID: 79
		internal const string RpcNamespace = "http://www.w3.org/2003/05/soap-rpc";

		// Token: 0x04000050 RID: 80
		internal const string Prefix = "soap12";

		// Token: 0x0200000B RID: 11
		internal class Attribute
		{
			// Token: 0x0600000D RID: 13 RVA: 0x0000210F File Offset: 0x0000030F
			private Attribute()
			{
			}

			// Token: 0x04000051 RID: 81
			internal const string UpgradeEnvelopeQname = "qname";

			// Token: 0x04000052 RID: 82
			internal const string Role = "role";

			// Token: 0x04000053 RID: 83
			internal const string Relay = "relay";
		}

		// Token: 0x0200000C RID: 12
		internal sealed class Element
		{
			// Token: 0x0600000E RID: 14 RVA: 0x0000210F File Offset: 0x0000030F
			private Element()
			{
			}

			// Token: 0x04000054 RID: 84
			internal const string Upgrade = "Upgrade";

			// Token: 0x04000055 RID: 85
			internal const string UpgradeEnvelope = "SupportedEnvelope";

			// Token: 0x04000056 RID: 86
			internal const string FaultRole = "Role";

			// Token: 0x04000057 RID: 87
			internal const string FaultReason = "Reason";

			// Token: 0x04000058 RID: 88
			internal const string FaultReasonText = "Text";

			// Token: 0x04000059 RID: 89
			internal const string FaultCode = "Code";

			// Token: 0x0400005A RID: 90
			internal const string FaultNode = "Node";

			// Token: 0x0400005B RID: 91
			internal const string FaultCodeValue = "Value";

			// Token: 0x0400005C RID: 92
			internal const string FaultSubcode = "Subcode";

			// Token: 0x0400005D RID: 93
			internal const string FaultDetail = "Detail";
		}

		// Token: 0x0200000D RID: 13
		internal sealed class Code
		{
			// Token: 0x0600000F RID: 15 RVA: 0x0000210F File Offset: 0x0000030F
			private Code()
			{
			}

			// Token: 0x0400005E RID: 94
			internal const string VersionMismatch = "VersionMismatch";

			// Token: 0x0400005F RID: 95
			internal const string MustUnderstand = "MustUnderstand";

			// Token: 0x04000060 RID: 96
			internal const string DataEncodingUnknown = "DataEncodingUnknown";

			// Token: 0x04000061 RID: 97
			internal const string Sender = "Sender";

			// Token: 0x04000062 RID: 98
			internal const string Receiver = "Receiver";

			// Token: 0x04000063 RID: 99
			internal const string RpcProcedureNotPresentSubcode = "ProcedureNotPresent";

			// Token: 0x04000064 RID: 100
			internal const string RpcBadArgumentsSubcode = "BadArguments";

			// Token: 0x04000065 RID: 101
			internal const string EncodingMissingIDFaultSubcode = "MissingID";

			// Token: 0x04000066 RID: 102
			internal const string EncodingUntypedValueFaultSubcode = "UntypedValue";
		}
	}
}
