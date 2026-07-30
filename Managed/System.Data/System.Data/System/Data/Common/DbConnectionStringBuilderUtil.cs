using System;
using System.Data.SqlClient;
using System.Reflection;

namespace System.Data.Common
{
	// Token: 0x02000382 RID: 898
	internal static class DbConnectionStringBuilderUtil
	{
		// Token: 0x06002AA3 RID: 10915 RVA: 0x000BCF98 File Offset: 0x000BB198
		internal static bool ConvertToBoolean(object value)
		{
			string text = value as string;
			if (text == null)
			{
				bool flag;
				try
				{
					flag = Convert.ToBoolean(value);
				}
				catch (InvalidCastException ex)
				{
					throw ADP.ConvertFailed(value.GetType(), typeof(bool), ex);
				}
				return flag;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "true") || StringComparer.OrdinalIgnoreCase.Equals(text, "yes"))
			{
				return true;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "false") || StringComparer.OrdinalIgnoreCase.Equals(text, "no"))
			{
				return false;
			}
			string text2 = text.Trim();
			return StringComparer.OrdinalIgnoreCase.Equals(text2, "true") || StringComparer.OrdinalIgnoreCase.Equals(text2, "yes") || (!StringComparer.OrdinalIgnoreCase.Equals(text2, "false") && !StringComparer.OrdinalIgnoreCase.Equals(text2, "no") && bool.Parse(text));
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x000BD08C File Offset: 0x000BB28C
		internal static bool ConvertToIntegratedSecurity(object value)
		{
			string text = value as string;
			if (text == null)
			{
				bool flag;
				try
				{
					flag = Convert.ToBoolean(value);
				}
				catch (InvalidCastException ex)
				{
					throw ADP.ConvertFailed(value.GetType(), typeof(bool), ex);
				}
				return flag;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "sspi") || StringComparer.OrdinalIgnoreCase.Equals(text, "true") || StringComparer.OrdinalIgnoreCase.Equals(text, "yes"))
			{
				return true;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "false") || StringComparer.OrdinalIgnoreCase.Equals(text, "no"))
			{
				return false;
			}
			string text2 = text.Trim();
			return StringComparer.OrdinalIgnoreCase.Equals(text2, "sspi") || StringComparer.OrdinalIgnoreCase.Equals(text2, "true") || StringComparer.OrdinalIgnoreCase.Equals(text2, "yes") || (!StringComparer.OrdinalIgnoreCase.Equals(text2, "false") && !StringComparer.OrdinalIgnoreCase.Equals(text2, "no") && bool.Parse(text));
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x000BD1A4 File Offset: 0x000BB3A4
		internal static int ConvertToInt32(object value)
		{
			int num;
			try
			{
				num = Convert.ToInt32(value);
			}
			catch (InvalidCastException ex)
			{
				throw ADP.ConvertFailed(value.GetType(), typeof(int), ex);
			}
			return num;
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000BD1E4 File Offset: 0x000BB3E4
		internal static string ConvertToString(object value)
		{
			string text;
			try
			{
				text = Convert.ToString(value);
			}
			catch (InvalidCastException ex)
			{
				throw ADP.ConvertFailed(value.GetType(), typeof(string), ex);
			}
			return text;
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000BD224 File Offset: 0x000BB424
		internal static bool TryConvertToApplicationIntent(string value, out ApplicationIntent result)
		{
			if (StringComparer.OrdinalIgnoreCase.Equals(value, "ReadOnly"))
			{
				result = ApplicationIntent.ReadOnly;
				return true;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(value, "ReadWrite"))
			{
				result = ApplicationIntent.ReadWrite;
				return true;
			}
			result = ApplicationIntent.ReadWrite;
			return false;
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x000BD258 File Offset: 0x000BB458
		internal static bool IsValidApplicationIntentValue(ApplicationIntent value)
		{
			return value == ApplicationIntent.ReadOnly || value == ApplicationIntent.ReadWrite;
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000BD264 File Offset: 0x000BB464
		internal static string ApplicationIntentToString(ApplicationIntent value)
		{
			if (value == ApplicationIntent.ReadOnly)
			{
				return "ReadOnly";
			}
			return "ReadWrite";
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x000BD278 File Offset: 0x000BB478
		internal static ApplicationIntent ConvertToApplicationIntent(string keyword, object value)
		{
			string text = value as string;
			if (text != null)
			{
				ApplicationIntent applicationIntent;
				if (DbConnectionStringBuilderUtil.TryConvertToApplicationIntent(text, out applicationIntent))
				{
					return applicationIntent;
				}
				text = text.Trim();
				if (DbConnectionStringBuilderUtil.TryConvertToApplicationIntent(text, out applicationIntent))
				{
					return applicationIntent;
				}
				throw ADP.InvalidConnectionOptionValue(keyword);
			}
			else
			{
				ApplicationIntent applicationIntent2;
				if (value is ApplicationIntent)
				{
					applicationIntent2 = (ApplicationIntent)value;
				}
				else
				{
					if (value.GetType().GetTypeInfo().IsEnum)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(ApplicationIntent), null);
					}
					try
					{
						applicationIntent2 = (ApplicationIntent)Enum.ToObject(typeof(ApplicationIntent), value);
					}
					catch (ArgumentException ex)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(ApplicationIntent), ex);
					}
				}
				if (DbConnectionStringBuilderUtil.IsValidApplicationIntentValue(applicationIntent2))
				{
					return applicationIntent2;
				}
				throw ADP.InvalidEnumerationValue(typeof(ApplicationIntent), (int)applicationIntent2);
			}
		}

		// Token: 0x0400198B RID: 6539
		private const string ApplicationIntentReadWriteString = "ReadWrite";

		// Token: 0x0400198C RID: 6540
		private const string ApplicationIntentReadOnlyString = "ReadOnly";
	}
}
