using System;

namespace System.Linq
{
	// Token: 0x020000AB RID: 171
	internal static class Error
	{
		// Token: 0x06000500 RID: 1280 RVA: 0x0000D1F3 File Offset: 0x0000B3F3
		internal static Exception ArgumentNotIEnumerableGeneric(string message)
		{
			return new ArgumentException(Strings.ArgumentNotIEnumerableGeneric(message));
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000D200 File Offset: 0x0000B400
		internal static Exception ArgumentNotValid(string message)
		{
			return new ArgumentException(Strings.ArgumentNotValid(message));
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000D20D File Offset: 0x0000B40D
		internal static Exception NoMethodOnType(string name, object type)
		{
			return new InvalidOperationException(Strings.NoMethodOnType(name, type));
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000D21B File Offset: 0x0000B41B
		internal static Exception NoMethodOnTypeMatchingArguments(string name, object type)
		{
			return new InvalidOperationException(Strings.NoMethodOnTypeMatchingArguments(name, type));
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000D229 File Offset: 0x0000B429
		internal static Exception EnumeratingNullEnumerableExpression()
		{
			return new InvalidOperationException(Strings.EnumeratingNullEnumerableExpression());
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000D235 File Offset: 0x0000B435
		internal static Exception ArgumentNull(string s)
		{
			return new ArgumentNullException(s);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000D23D File Offset: 0x0000B43D
		internal static Exception ArgumentOutOfRange(string s)
		{
			return new ArgumentOutOfRangeException(s);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000D245 File Offset: 0x0000B445
		internal static Exception MoreThanOneElement()
		{
			return new InvalidOperationException("Sequence contains more than one element");
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000D251 File Offset: 0x0000B451
		internal static Exception MoreThanOneMatch()
		{
			return new InvalidOperationException("Sequence contains more than one matching element");
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000D25D File Offset: 0x0000B45D
		internal static Exception NoElements()
		{
			return new InvalidOperationException("Sequence contains no elements");
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000D269 File Offset: 0x0000B469
		internal static Exception NoMatch()
		{
			return new InvalidOperationException("Sequence contains no matching element");
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000D275 File Offset: 0x0000B475
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
