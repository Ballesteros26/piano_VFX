using System;

namespace UnityEngine.Assertions
{
	// Token: 0x020003E4 RID: 996
	internal class AssertionMessageUtil
	{
		// Token: 0x0600229C RID: 8860 RVA: 0x0003A3F8 File Offset: 0x000385F8
		public static string GetMessage(string failureMessage)
		{
			return UnityString.Format("{0} {1}", new object[] { "Assertion failure.", failureMessage });
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x0003A428 File Offset: 0x00038628
		public static string GetMessage(string failureMessage, string expected)
		{
			return AssertionMessageUtil.GetMessage(UnityString.Format("{0}{1}{2} {3}", new object[]
			{
				failureMessage,
				Environment.NewLine,
				"Expected:",
				expected
			}));
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x0003A468 File Offset: 0x00038668
		public static string GetEqualityMessage(object actual, object expected, bool expectEqual)
		{
			return AssertionMessageUtil.GetMessage(UnityString.Format("Values are {0}equal.", new object[] { expectEqual ? "not " : "" }), UnityString.Format("{0} {2} {1}", new object[]
			{
				actual,
				expected,
				expectEqual ? "==" : "!="
			}));
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x0003A4CC File Offset: 0x000386CC
		public static string NullFailureMessage(object value, bool expectNull)
		{
			return AssertionMessageUtil.GetMessage(UnityString.Format("Value was {0}Null", new object[] { expectNull ? "not " : "" }), UnityString.Format("Value was {0}Null", new object[] { expectNull ? "" : "not " }));
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x0003A528 File Offset: 0x00038728
		public static string BooleanFailureMessage(bool expected)
		{
			return AssertionMessageUtil.GetMessage("Value was " + (!expected).ToString(), expected.ToString());
		}

		// Token: 0x04000D01 RID: 3329
		private const string k_Expected = "Expected:";

		// Token: 0x04000D02 RID: 3330
		private const string k_AssertionFailed = "Assertion failure.";
	}
}
