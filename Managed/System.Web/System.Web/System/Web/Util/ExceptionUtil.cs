using System;

namespace System.Web.Util
{
	// Token: 0x02000118 RID: 280
	internal static class ExceptionUtil
	{
		// Token: 0x06000E0A RID: 3594 RVA: 0x00026254 File Offset: 0x00024454
		internal static ArgumentException ParameterInvalid(string parameter)
		{
			return new ArgumentException(global::SR.GetString("The parameter '{0}' is invalid.", new object[] { parameter }), parameter);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00026270 File Offset: 0x00024470
		internal static ArgumentException ParameterNullOrEmpty(string parameter)
		{
			return new ArgumentException(global::SR.GetString("The string parameter '{0}' cannot be null or empty.", new object[] { parameter }), parameter);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0002628C File Offset: 0x0002448C
		internal static ArgumentException PropertyInvalid(string property)
		{
			return new ArgumentException(global::SR.GetString("The value assigned to property '{0}' is invalid.", new object[] { property }), property);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x000262A8 File Offset: 0x000244A8
		internal static ArgumentException PropertyNullOrEmpty(string property)
		{
			return new ArgumentException(global::SR.GetString("The value assigned to property '{0}' cannot be null or empty.", new object[] { property }), property);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x000262C4 File Offset: 0x000244C4
		internal static InvalidOperationException UnexpectedError(string methodName)
		{
			return new InvalidOperationException(global::SR.GetString("An unexpected error occurred in '{0}'.", new object[] { methodName }));
		}
	}
}
