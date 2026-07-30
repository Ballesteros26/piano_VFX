using System;
using System.Text;

namespace System.Web.Mail
{
	// Token: 0x020000FA RID: 250
	internal class MailUtil
	{
		// Token: 0x06000D52 RID: 3410 RVA: 0x00023E70 File Offset: 0x00022070
		public static bool NeedEncoding(string str)
		{
			foreach (int num in str)
			{
				if ((num <= 61 || num >= 127) && (num <= 31 || num >= 61))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00023EAF File Offset: 0x000220AF
		public static string Base64Encode(string str)
		{
			return Convert.ToBase64String(Encoding.Default.GetBytes(str));
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00023EC4 File Offset: 0x000220C4
		public static string GenerateBoundary()
		{
			StringBuilder stringBuilder = new StringBuilder("__MONO__Boundary");
			stringBuilder.Append("__");
			DateTime now = DateTime.Now;
			stringBuilder.Append(now.Year);
			stringBuilder.Append(now.Month);
			stringBuilder.Append(now.Day);
			stringBuilder.Append(now.Hour);
			stringBuilder.Append(now.Minute);
			stringBuilder.Append(now.Second);
			stringBuilder.Append(now.Millisecond);
			stringBuilder.Append("__");
			stringBuilder.Append(new Random().Next());
			stringBuilder.Append("__");
			return stringBuilder.ToString();
		}
	}
}
