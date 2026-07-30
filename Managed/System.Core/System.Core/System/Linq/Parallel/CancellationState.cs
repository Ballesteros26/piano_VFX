using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001EA RID: 490
	internal class CancellationState
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x00029A0D File Offset: 0x00027C0D
		internal CancellationToken MergedCancellationToken
		{
			get
			{
				if (this.MergedCancellationTokenSource != null)
				{
					return this.MergedCancellationTokenSource.Token;
				}
				return new CancellationToken(false);
			}
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00029A29 File Offset: 0x00027C29
		internal CancellationState(CancellationToken externalCancellationToken)
		{
			this.ExternalCancellationToken = externalCancellationToken;
			this.TopLevelDisposedFlag = new Shared<bool>(false);
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00029A44 File Offset: 0x00027C44
		internal static void ThrowIfCanceled(CancellationToken token)
		{
			if (token.IsCancellationRequested)
			{
				throw new OperationCanceledException(token);
			}
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00029A56 File Offset: 0x00027C56
		internal static void ThrowWithStandardMessageIfCanceled(CancellationToken externalCancellationToken)
		{
			if (externalCancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException("The query has been canceled via the token supplied to WithCancellation.", externalCancellationToken);
			}
		}

		// Token: 0x040007A5 RID: 1957
		internal CancellationTokenSource InternalCancellationTokenSource;

		// Token: 0x040007A6 RID: 1958
		internal CancellationToken ExternalCancellationToken;

		// Token: 0x040007A7 RID: 1959
		internal CancellationTokenSource MergedCancellationTokenSource;

		// Token: 0x040007A8 RID: 1960
		internal Shared<bool> TopLevelDisposedFlag;

		// Token: 0x040007A9 RID: 1961
		internal const int POLL_INTERVAL = 63;
	}
}
