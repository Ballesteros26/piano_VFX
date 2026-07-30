using System;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x020001F6 RID: 502
	internal static class SqlServerEscapeHelper
	{
		// Token: 0x06001774 RID: 6004 RVA: 0x00072079 File Offset: 0x00070279
		internal static string EscapeIdentifier(string name)
		{
			return "[" + name.Replace("]", "]]") + "]";
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x0007209A File Offset: 0x0007029A
		internal static void EscapeIdentifier(StringBuilder builder, string name)
		{
			builder.Append("[");
			builder.Append(name.Replace("]", "]]"));
			builder.Append("]");
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x000720CB File Offset: 0x000702CB
		internal static string EscapeStringAsLiteral(string input)
		{
			return input.Replace("'", "''");
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x000720DD File Offset: 0x000702DD
		internal static string MakeStringLiteral(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return "''";
			}
			return "'" + SqlServerEscapeHelper.EscapeStringAsLiteral(input) + "'";
		}
	}
}
