using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x0200043B RID: 1083
	internal static class ValidationHelper
	{
		// Token: 0x060020A1 RID: 8353 RVA: 0x0007F06A File Offset: 0x0007D26A
		public static string[] MakeEmptyArrayNull(string[] stringArray)
		{
			if (stringArray == null || stringArray.Length == 0)
			{
				return null;
			}
			return stringArray;
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x0007F076 File Offset: 0x0007D276
		public static string MakeStringNull(string stringValue)
		{
			if (stringValue == null || stringValue.Length == 0)
			{
				return null;
			}
			return stringValue;
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x0007F086 File Offset: 0x0007D286
		public static string ExceptionMessage(Exception exception)
		{
			if (exception == null)
			{
				return string.Empty;
			}
			if (exception.InnerException == null)
			{
				return exception.Message;
			}
			return exception.Message + " (" + ValidationHelper.ExceptionMessage(exception.InnerException) + ")";
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x0007F0C0 File Offset: 0x0007D2C0
		public static string ToString(object objectValue)
		{
			if (objectValue == null)
			{
				return "(null)";
			}
			if (objectValue is string && ((string)objectValue).Length == 0)
			{
				return "(string.empty)";
			}
			if (objectValue is Exception)
			{
				return ValidationHelper.ExceptionMessage(objectValue as Exception);
			}
			if (objectValue is IntPtr)
			{
				return "0x" + ((IntPtr)objectValue).ToString("x");
			}
			return objectValue.ToString();
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x0007F134 File Offset: 0x0007D334
		public static string HashString(object objectValue)
		{
			if (objectValue == null)
			{
				return "(null)";
			}
			if (objectValue is string && ((string)objectValue).Length == 0)
			{
				return "(string.empty)";
			}
			return objectValue.GetHashCode().ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x0007F178 File Offset: 0x0007D378
		public static bool IsInvalidHttpString(string stringValue)
		{
			return stringValue.IndexOfAny(ValidationHelper.InvalidParamChars) != -1;
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x0007F18B File Offset: 0x0007D38B
		public static bool IsBlankString(string stringValue)
		{
			return stringValue == null || stringValue.Length == 0;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x0007F19B File Offset: 0x0007D39B
		public static bool ValidateTcpPort(int port)
		{
			return port >= 0 && port <= 65535;
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x0007F1AE File Offset: 0x0007D3AE
		public static bool ValidateRange(int actual, int fromAllowed, int toAllowed)
		{
			return actual >= fromAllowed && actual <= toAllowed;
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x0007F1C0 File Offset: 0x0007D3C0
		internal static void ValidateSegment(ArraySegment<byte> segment)
		{
			if (segment.Array == null)
			{
				throw new ArgumentNullException("segment");
			}
			if (segment.Offset < 0 || segment.Count < 0 || segment.Count > segment.Array.Length - segment.Offset)
			{
				throw new ArgumentOutOfRangeException("segment");
			}
		}

		// Token: 0x04001CC2 RID: 7362
		public static string[] EmptyArray = new string[0];

		// Token: 0x04001CC3 RID: 7363
		internal static readonly char[] InvalidMethodChars = new char[] { ' ', '\r', '\n', '\t' };

		// Token: 0x04001CC4 RID: 7364
		internal static readonly char[] InvalidParamChars = new char[]
		{
			'(', ')', '<', '>', '@', ',', ';', ':', '\\', '"',
			'\'', '/', '[', ']', '?', '=', '{', '}', ' ', '\t',
			'\r', '\n'
		};
	}
}
