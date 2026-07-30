using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001FE RID: 510
	internal static class ExceptionAggregator
	{
		// Token: 0x06000CB9 RID: 3257 RVA: 0x0002A857 File Offset: 0x00028A57
		internal static IEnumerable<TElement> WrapEnumerable<TElement>(IEnumerable<TElement> source, CancellationState cancellationState)
		{
			using (IEnumerator<TElement> enumerator = source.GetEnumerator())
			{
				for (;;)
				{
					TElement telement = default(TElement);
					try
					{
						if (!enumerator.MoveNext())
						{
							yield break;
						}
						telement = enumerator.Current;
					}
					catch (Exception ex)
					{
						ExceptionAggregator.ThrowOCEorAggregateException(ex, cancellationState);
					}
					yield return telement;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0002A86E File Offset: 0x00028A6E
		internal static IEnumerable<TElement> WrapQueryEnumerator<TElement, TIgnoreKey>(QueryOperatorEnumerator<TElement, TIgnoreKey> source, CancellationState cancellationState)
		{
			TElement elem = default(TElement);
			TIgnoreKey ignoreKey = default(TIgnoreKey);
			try
			{
				for (;;)
				{
					try
					{
						if (!source.MoveNext(ref elem, ref ignoreKey))
						{
							yield break;
						}
					}
					catch (Exception ex)
					{
						ExceptionAggregator.ThrowOCEorAggregateException(ex, cancellationState);
					}
					yield return elem;
				}
			}
			finally
			{
				source.Dispose();
			}
			yield break;
			yield break;
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0002A885 File Offset: 0x00028A85
		internal static void ThrowOCEorAggregateException(Exception ex, CancellationState cancellationState)
		{
			if (ExceptionAggregator.ThrowAnOCE(ex, cancellationState))
			{
				CancellationState.ThrowWithStandardMessageIfCanceled(cancellationState.ExternalCancellationToken);
				return;
			}
			throw new AggregateException(new Exception[] { ex });
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0002A8AB File Offset: 0x00028AAB
		internal static Func<T, U> WrapFunc<T, U>(Func<T, U> f, CancellationState cancellationState)
		{
			return delegate(T t)
			{
				U u = default(U);
				try
				{
					u = f(t);
				}
				catch (Exception ex)
				{
					ExceptionAggregator.ThrowOCEorAggregateException(ex, cancellationState);
				}
				return u;
			};
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0002A8CC File Offset: 0x00028ACC
		private static bool ThrowAnOCE(Exception ex, CancellationState cancellationState)
		{
			OperationCanceledException ex2 = ex as OperationCanceledException;
			return (ex2 != null && ex2.CancellationToken == cancellationState.ExternalCancellationToken && cancellationState.ExternalCancellationToken.IsCancellationRequested) || (ex2 != null && ex2.CancellationToken == cancellationState.MergedCancellationToken && cancellationState.MergedCancellationToken.IsCancellationRequested && cancellationState.ExternalCancellationToken.IsCancellationRequested);
		}
	}
}
