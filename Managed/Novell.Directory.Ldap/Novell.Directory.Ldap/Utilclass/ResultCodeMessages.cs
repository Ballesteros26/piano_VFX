using System;
using System.Resources;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x0200004F RID: 79
	public class ResultCodeMessages : ResourceManager
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x0000E894 File Offset: 0x0000CA94
		public object[][] getContents()
		{
			return ResultCodeMessages.contents;
		}

		// Token: 0x040001F8 RID: 504
		internal static readonly object[][] contents = new object[][]
		{
			new object[] { "0", "Success" },
			new object[] { "1", "Operations Error" },
			new object[] { "2", "Protocol Error" },
			new object[] { "3", "Timelimit Exceeded" },
			new object[] { "4", "Sizelimit Exceeded" },
			new object[] { "5", "Compare False" },
			new object[] { "6", "Compare True" },
			new object[] { "7", "Authentication Method Not Supported" },
			new object[] { "8", "Strong Authentication Required" },
			new object[] { "9", "Partial Results" },
			new object[] { "10", "Referral" },
			new object[] { "11", "Administrative Limit Exceeded" },
			new object[] { "12", "Unavailable Critical Extension" },
			new object[] { "13", "Confidentiality Required" },
			new object[] { "14", "SASL Bind In Progress" },
			new object[] { "16", "No Such Attribute" },
			new object[] { "17", "Undefined Attribute Type" },
			new object[] { "18", "Inappropriate Matching" },
			new object[] { "19", "Constraint Violation" },
			new object[] { "20", "Attribute Or Value Exists" },
			new object[] { "21", "Invalid Attribute Syntax" },
			new object[] { "32", "No Such Object" },
			new object[] { "33", "Alias Problem" },
			new object[] { "34", "Invalid DN Syntax" },
			new object[] { "35", "Is Leaf" },
			new object[] { "36", "Alias Dereferencing Problem" },
			new object[] { "48", "Inappropriate Authentication" },
			new object[] { "49", "Invalid Credentials" },
			new object[] { "50", "Insufficient Access Rights" },
			new object[] { "51", "Busy" },
			new object[] { "52", "Unavailable" },
			new object[] { "53", "Unwilling To Perform" },
			new object[] { "54", "Loop Detect" },
			new object[] { "64", "Naming Violation" },
			new object[] { "65", "Object Class Violation" },
			new object[] { "66", "Not Allowed On Non-leaf" },
			new object[] { "67", "Not Allowed On RDN" },
			new object[] { "68", "Entry Already Exists" },
			new object[] { "69", "Object Class Modifications Prohibited" },
			new object[] { "71", "Affects Multiple DSAs" },
			new object[] { "80", "Other" },
			new object[] { "81", "Server Down" },
			new object[] { "82", "Local Error" },
			new object[] { "83", "Encoding Error" },
			new object[] { "84", "Decoding Error" },
			new object[] { "85", "Ldap Timeout" },
			new object[] { "86", "Authentication Unknown" },
			new object[] { "87", "Filter Error" },
			new object[] { "88", "User Cancelled" },
			new object[] { "89", "Parameter Error" },
			new object[] { "90", "No Memory" },
			new object[] { "91", "Connect Error" },
			new object[] { "92", "Ldap Not Supported" },
			new object[] { "93", "Control Not Found" },
			new object[] { "94", "No Results Returned" },
			new object[] { "95", "More Results To Return" },
			new object[] { "96", "Client Loop" },
			new object[] { "97", "Referral Limit Exceeded" },
			new object[] { "112", "TLS not supported" }
		};
	}
}
