using System;

namespace System.Linq
{
	// Token: 0x020000AD RID: 173
	internal static class Strings
	{
		// Token: 0x0600058C RID: 1420 RVA: 0x0000FDC8 File Offset: 0x0000DFC8
		internal static string ArgumentNotIEnumerableGeneric(string message)
		{
			return global::SR.Format("{0} is not IEnumerable<>", message);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000FDD5 File Offset: 0x0000DFD5
		internal static string ArgumentNotValid(string message)
		{
			return global::SR.Format("Argument {0} is not valid", message);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0000FDE2 File Offset: 0x0000DFE2
		internal static string NoMethodOnType(string name, object type)
		{
			return global::SR.Format("There is no method '{0}' on type '{1}'", name, type);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
		internal static string NoMethodOnTypeMatchingArguments(string name, object type)
		{
			return global::SR.Format("There is no method '{0}' on type '{1}' that matches the specified arguments", name, type);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0000FDFE File Offset: 0x0000DFFE
		internal static string EnumeratingNullEnumerableExpression()
		{
			return "Cannot enumerate a query created from a null IEnumerable<>";
		}
	}
}
