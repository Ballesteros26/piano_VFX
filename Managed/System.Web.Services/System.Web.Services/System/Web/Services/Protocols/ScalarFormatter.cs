using System;
using System.Globalization;
using System.Threading;

namespace System.Web.Services.Protocols
{
	// Token: 0x0200004E RID: 78
	internal class ScalarFormatter
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x0000210F File Offset: 0x0000030F
		private ScalarFormatter()
		{
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008656 File Offset: 0x00006856
		internal static string ToString(object value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			if (value is string)
			{
				return (string)value;
			}
			if (value.GetType().IsEnum)
			{
				return ScalarFormatter.EnumToString(value);
			}
			return Convert.ToString(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00008690 File Offset: 0x00006890
		internal static object FromString(string value, Type type)
		{
			object obj;
			try
			{
				if (type == typeof(string))
				{
					obj = value;
				}
				else if (type.IsEnum)
				{
					obj = ScalarFormatter.EnumFromString(value, type);
				}
				else
				{
					obj = Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new ArgumentException(Res.GetString("WebChangeTypeFailed", new object[] { value, type.FullName }), "type", ex);
			}
			return obj;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000872C File Offset: 0x0000692C
		private static object EnumFromString(string value, Type type)
		{
			return Enum.Parse(type, value);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00008735 File Offset: 0x00006935
		private static string EnumToString(object value)
		{
			return Enum.Format(value.GetType(), value, "G");
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00008748 File Offset: 0x00006948
		internal static bool IsTypeSupported(Type type)
		{
			return type.IsEnum || type == typeof(int) || type == typeof(string) || type == typeof(long) || type == typeof(byte) || type == typeof(sbyte) || type == typeof(short) || type == typeof(bool) || type == typeof(char) || type == typeof(float) || type == typeof(decimal) || type == typeof(DateTime) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong) || type == typeof(double);
		}
	}
}
