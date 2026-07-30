using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Parallel;
using System.Threading;

namespace System.Linq
{
	/// <summary>Provides a set of methods for querying objects that implement ParallelQuery{TSource}. This is the parallel equivalent of <see cref="T:System.Linq.Enumerable" />.</summary>
	// Token: 0x02000099 RID: 153
	public static class ParallelEnumerable
	{
		/// <summary>Enables parallelization of a query.</summary>
		/// <returns>The source as a <see cref="T:System.Linq.ParallelQuery`1" /> to bind to ParallelEnumerable extension methods.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to convert to a <see cref="T:System.Linq.ParallelQuery`1" />.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x0600036D RID: 877 RVA: 0x000087B7 File Offset: 0x000069B7
		public static ParallelQuery<TSource> AsParallel<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ParallelEnumerableWrapper<TSource>(source);
		}

		/// <summary>Enables parallelization of a query, as sourced by a custom partitioner that is responsible for splitting the input sequence into partitions.</summary>
		/// <returns>The <paramref name="source" /> as a ParallelQuery to bind to ParallelEnumerable extension methods.</returns>
		/// <param name="source">A partitioner over the input sequence.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x0600036E RID: 878 RVA: 0x000087CD File Offset: 0x000069CD
		public static ParallelQuery<TSource> AsParallel<TSource>(this Partitioner<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new PartitionerQueryOperator<TSource>(source);
		}

		/// <summary>Enables treatment of a data source as if it were ordered, overriding the default of unordered. AsOrdered may only be invoked on generic sequences returned by AsParallel, ParallelEnumerable.Range, and ParallelEnumerable.Repeat.</summary>
		/// <returns>The source sequence which will maintain the original ordering in the subsequent query operators.</returns>
		/// <param name="source">The input sequence.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.InvalidOperationException">Thrown if <paramref name="source" /> contains no elements-or-if <paramref name="source" /> is not one of AsParallel, ParallelEnumerable.Range, or ParallelEnumerable.Repeat.</exception>
		// Token: 0x0600036F RID: 879 RVA: 0x000087E4 File Offset: 0x000069E4
		public static ParallelQuery<TSource> AsOrdered<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(source is ParallelEnumerableWrapper<TSource>) && !(source is IParallelPartitionable<TSource>))
			{
				PartitionerQueryOperator<TSource> partitionerQueryOperator = source as PartitionerQueryOperator<TSource>;
				if (partitionerQueryOperator == null)
				{
					throw new InvalidOperationException("AsOrdered may only be called on the result of AsParallel, ParallelEnumerable.Range, or ParallelEnumerable.Repeat.");
				}
				if (!partitionerQueryOperator.Orderable)
				{
					throw new InvalidOperationException("AsOrdered may not be used with a partitioner that is not orderable.");
				}
			}
			return new OrderingQueryOperator<TSource>(QueryOperator<TSource>.AsQueryOperator(source), true);
		}

		/// <summary>Enables treatment of a data source as if it were ordered, overriding the default of unordered. AsOrdered may only be invoked on non-generic sequences returned by AsParallel, ParallelEnumerable.Range, and ParallelEnumerable.Repeat.</summary>
		/// <returns>The source sequence which will maintain the original ordering in the subsequent query operators.</returns>
		/// <param name="source">The input sequence.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.InvalidOperationException">thrown if <paramref name="source" /> contains no elements-or- if AsOrdered is called midway through a query. It is allowed to be called immediately after <see cref="M:System.Linq.ParallelEnumerable.AsParallel(System.Collections.IEnumerable)" />, <see cref="M:System.Linq.ParallelEnumerable.Range(System.Int32,System.Int32)" /> or <see cref="M:System.Linq.ParallelEnumerable.Repeat``1(``0,System.Int32)" />.</exception>
		// Token: 0x06000370 RID: 880 RVA: 0x00008843 File Offset: 0x00006A43
		public static ParallelQuery AsOrdered(this ParallelQuery source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			ParallelEnumerableWrapper parallelEnumerableWrapper = source as ParallelEnumerableWrapper;
			if (parallelEnumerableWrapper == null)
			{
				throw new InvalidOperationException("Non-generic AsOrdered may only be called on the result of the non-generic AsParallel.");
			}
			return new OrderingQueryOperator<object>(QueryOperator<object>.AsQueryOperator(parallelEnumerableWrapper), true);
		}

		/// <summary>Allows an intermediate query to be treated as if no ordering is implied among the elements.</summary>
		/// <returns>The source sequence with arbitrary order.</returns>
		/// <param name="source">The input sequence.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x06000371 RID: 881 RVA: 0x00008872 File Offset: 0x00006A72
		public static ParallelQuery<TSource> AsUnordered<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new OrderingQueryOperator<TSource>(QueryOperator<TSource>.AsQueryOperator(source), false);
		}

		/// <summary>Enables parallelization of a query.</summary>
		/// <returns>The source as a ParallelQuery to bind to ParallelEnumerable extension methods.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to convert to a <see cref="T:System.Linq.ParallelQuery" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x06000372 RID: 882 RVA: 0x0000888E File Offset: 0x00006A8E
		public static ParallelQuery AsParallel(this IEnumerable source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ParallelEnumerableWrapper(source);
		}

		/// <summary>Converts a <see cref="T:System.Linq.ParallelQuery`1" /> into an <see cref="T:System.Collections.Generic.IEnumerable`1" /> to force sequential evaluation of the query.</summary>
		/// <returns>The source as an <see cref="T:System.Collections.Generic.IEnumerable`1" /> to bind to sequential extension methods.</returns>
		/// <param name="source">A <see cref="T:System.Linq.ParallelQuery`1" /> to convert to an <see cref="T:System.Collections.Generic.IEnumerable`1" />.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x06000373 RID: 883 RVA: 0x000088A4 File Offset: 0x00006AA4
		public static IEnumerable<TSource> AsSequential<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			ParallelEnumerableWrapper<TSource> parallelEnumerableWrapper = source as ParallelEnumerableWrapper<TSource>;
			if (parallelEnumerableWrapper != null)
			{
				return parallelEnumerableWrapper.WrappedEnumerable;
			}
			return source;
		}

		/// <summary>Sets the degree of parallelism to use in a query. Degree of parallelism is the maximum number of concurrently executing tasks that will be used to process the query.</summary>
		/// <returns>ParallelQuery representing the same query as source, with the limit on the degrees of parallelism set.</returns>
		/// <param name="source">A ParallelQuery on which to set the limit on the degrees of parallelism.</param>
		/// <param name="degreeOfParallelism">The degree of parallelism for the query. The default value is Math.Min(<see cref="P:System.Environment.ProcessorCount" />, MAX_SUPPORTED_DOP) where MAX_SUPPORTED_DOP is 64.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="degreeOfParallelism" /> is less than 1 or greater than 63.</exception>
		/// <exception cref="T:System.InvalidOperationException">WithDegreeOfParallelism is used multiple times in the query.</exception>
		// Token: 0x06000374 RID: 884 RVA: 0x000088D4 File Offset: 0x00006AD4
		public static ParallelQuery<TSource> WithDegreeOfParallelism<TSource>(this ParallelQuery<TSource> source, int degreeOfParallelism)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (degreeOfParallelism < 1 || degreeOfParallelism > 512)
			{
				throw new ArgumentOutOfRangeException("degreeOfParallelism");
			}
			QuerySettings empty = QuerySettings.Empty;
			empty.DegreeOfParallelism = new int?(degreeOfParallelism);
			return new QueryExecutionOption<TSource>(QueryOperator<TSource>.AsQueryOperator(source), empty);
		}

		/// <summary>Sets the <see cref="T:System.Threading.CancellationToken" /> to associate with the query.</summary>
		/// <returns>ParallelQuery representing the same query as source, but with the registered cancellation token.</returns>
		/// <param name="source">A ParallelQuery on which to set the option.</param>
		/// <param name="cancellationToken">A cancellation token.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <paramref name="cancellationToken" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="WithCancellation" /> is used multiple times in the query.</exception>
		// Token: 0x06000375 RID: 885 RVA: 0x00008928 File Offset: 0x00006B28
		public static ParallelQuery<TSource> WithCancellation<TSource>(this ParallelQuery<TSource> source, CancellationToken cancellationToken)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			QuerySettings empty = QuerySettings.Empty;
			empty.CancellationState = new CancellationState(cancellationToken);
			return new QueryExecutionOption<TSource>(QueryOperator<TSource>.AsQueryOperator(source), empty);
		}

		/// <summary>Sets the execution mode of the query.</summary>
		/// <returns>ParallelQuery representing the same query as source, but with the registered execution mode.</returns>
		/// <param name="source">A ParallelQuery on which to set the option.</param>
		/// <param name="executionMode">The mode in which to execute the query.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="executionMode" /> is not a valid <see cref="T:System.Linq.ParallelExecutionMode" /> value.</exception>
		/// <exception cref="T:System.InvalidOperationException">WithExecutionMode is used multiple times in the query.</exception>
		// Token: 0x06000376 RID: 886 RVA: 0x00008964 File Offset: 0x00006B64
		public static ParallelQuery<TSource> WithExecutionMode<TSource>(this ParallelQuery<TSource> source, ParallelExecutionMode executionMode)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (executionMode != ParallelExecutionMode.Default && executionMode != ParallelExecutionMode.ForceParallelism)
			{
				throw new ArgumentException("The executionMode argument contains an invalid value.");
			}
			QuerySettings empty = QuerySettings.Empty;
			empty.ExecutionMode = new ParallelExecutionMode?(executionMode);
			return new QueryExecutionOption<TSource>(QueryOperator<TSource>.AsQueryOperator(source), empty);
		}

		/// <summary>Sets the merge options for this query, which specify how the query will buffer output.</summary>
		/// <returns>ParallelQuery representing the same query as source, but with the registered merge options.</returns>
		/// <param name="source">A ParallelQuery on which to set the option.</param>
		/// <param name="mergeOptions">The merge options to set for this query.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mergeOptions" /> is not a valid <see cref="T:System.Linq.ParallelMergeOptions" /> value.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="WithMergeOptions" /> is used multiple times in the query.</exception>
		// Token: 0x06000377 RID: 887 RVA: 0x000089B0 File Offset: 0x00006BB0
		public static ParallelQuery<TSource> WithMergeOptions<TSource>(this ParallelQuery<TSource> source, ParallelMergeOptions mergeOptions)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (mergeOptions != ParallelMergeOptions.Default && mergeOptions != ParallelMergeOptions.AutoBuffered && mergeOptions != ParallelMergeOptions.NotBuffered && mergeOptions != ParallelMergeOptions.FullyBuffered)
			{
				throw new ArgumentException("The mergeOptions argument contains an invalid value.");
			}
			QuerySettings empty = QuerySettings.Empty;
			empty.MergeOptions = new ParallelMergeOptions?(mergeOptions);
			return new QueryExecutionOption<TSource>(QueryOperator<TSource>.AsQueryOperator(source), empty);
		}

		/// <summary>Generates a parallel sequence of integral numbers within a specified range.</summary>
		/// <returns>An IEnumerable&lt;Int32&gt; in C# or IEnumerable(Of Int32) in Visual Basic that contains a range of sequential integral numbers.</returns>
		/// <param name="start">The value of the first integer in the sequence.</param>
		/// <param name="count">The number of sequential integers to generate.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than 0 -or- <paramref name="start" /> + <paramref name="count" /> - 1 is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000378 RID: 888 RVA: 0x00008A04 File Offset: 0x00006C04
		public static ParallelQuery<int> Range(int start, int count)
		{
			if (count < 0 || (count > 0 && 2147483647 - (count - 1) < start))
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return new RangeEnumerable(start, count);
		}

		/// <summary>Generates a parallel sequence that contains one repeated value.</summary>
		/// <returns>A sequence that contains a repeated value.</returns>
		/// <param name="element">The value to be repeated.</param>
		/// <param name="count">The number of times to repeat the value in the generated sequence.</param>
		/// <typeparam name="TResult">The type of the value to be repeated in the result sequence.</typeparam>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than 0.</exception>
		// Token: 0x06000379 RID: 889 RVA: 0x00008A2C File Offset: 0x00006C2C
		public static ParallelQuery<TResult> Repeat<TResult>(TResult element, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return new RepeatEnumerable<TResult>(element, count);
		}

		/// <summary>Returns an empty ParallelQuery{TResult} that has the specified type argument.</summary>
		/// <returns>An empty sequence whose type argument is <paramref name="TResult" />.</returns>
		/// <typeparam name="TResult">The type to assign to the type parameter of the returned generic sequence.</typeparam>
		// Token: 0x0600037A RID: 890 RVA: 0x00008A44 File Offset: 0x00006C44
		public static ParallelQuery<TResult> Empty<TResult>()
		{
			return EmptyEnumerable<TResult>.Instance;
		}

		/// <summary>Invokes in parallel the specified action for each element in the <paramref name="source" />.</summary>
		/// <param name="source">The <see cref="T:System.Linq.ParallelQuery`1" /> whose elements will be processed by <paramref name="action" />.</param>
		/// <param name="action">An Action to invoke on each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600037B RID: 891 RVA: 0x00008A4B File Offset: 0x00006C4B
		public static void ForAll<TSource>(this ParallelQuery<TSource> source, Action<TSource> action)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			new ForAllOperator<TSource>(source, action).RunSynchronously();
		}

		/// <summary>Filters in parallel a sequence of values based on a predicate.</summary>
		/// <returns>A sequence that contains elements from the input sequence that satisfy the condition.</returns>
		/// <param name="source">A sequence to filter.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600037C RID: 892 RVA: 0x00008A75 File Offset: 0x00006C75
		public static ParallelQuery<TSource> Where<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new WhereQueryOperator<TSource>(source, predicate);
		}

		/// <summary>Filters in parallel a sequence of values based on a predicate. Each element's index is used in the logic of the predicate function.</summary>
		/// <returns>A sequence that contains elements from the input sequence that satisfy the condition.</returns>
		/// <param name="source">A sequence to filter.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.OverflowException">More than <see cref="F:System.Int32.MaxValue" /> elements are enumerated by the query.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600037D RID: 893 RVA: 0x00008A9A File Offset: 0x00006C9A
		public static ParallelQuery<TSource> Where<TSource>(this ParallelQuery<TSource> source, Func<TSource, int, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new IndexedWhereQueryOperator<TSource>(source, predicate);
		}

		/// <summary>Projects in parallel each element of a sequence into a new form.</summary>
		/// <returns>A sequence whose elements are the result of invoking the transform function on each element of <paramref name="source" />.</returns>
		/// <param name="source">A sequence of values to invoke a transform function on.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of elements resturned by selector.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600037E RID: 894 RVA: 0x00008ABF File Offset: 0x00006CBF
		public static ParallelQuery<TResult> Select<TSource, TResult>(this ParallelQuery<TSource> source, Func<TSource, TResult> selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			return new SelectQueryOperator<TSource, TResult>(source, selector);
		}

		/// <summary>Projects in parallel each element of a sequence into a new form by incorporating the element's index.</summary>
		/// <returns>A sequence whose elements are the result of invoking the transform function on each element of <paramref name="source" />, based on the index supplied to <paramref name="selector" />.</returns>
		/// <param name="source">A sequence of values to invoke a transform function on.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of elements resturned by selector.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.OverflowException">More than <see cref="F:System.Int32.MaxValue" /> elements are enumerated by the query. This condition might occur in streaming scenarios.</exception>
		// Token: 0x0600037F RID: 895 RVA: 0x00008AE4 File Offset: 0x00006CE4
		public static ParallelQuery<TResult> Select<TSource, TResult>(this ParallelQuery<TSource> source, Func<TSource, int, TResult> selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			return new IndexedSelectQueryOperator<TSource, TResult>(source, selector);
		}

		/// <summary>Merges in parallel two sequences by using the specified predicate function.</summary>
		/// <returns>A sequence that has elements of type <paramref name="TResult" /> that are obtained by performing <paramref name="resultSelector" /> pairwise on two sequences. If the sequence lengths are unequal, this truncates to the length of the shorter sequence.</returns>
		/// <param name="first">The first sequence to zip.</param>
		/// <param name="second">The second sequence to zip.</param>
		/// <param name="resultSelector">A function to create a result element from two matching elements.</param>
		/// <typeparam name="TFirst">The type of the elements of the first sequence.</typeparam>
		/// <typeparam name="TSecond">The type of the elements of the second sequence.</typeparam>
		/// <typeparam name="TResult">The type of the return elements.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> or <paramref name="resultSelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000380 RID: 896 RVA: 0x00008B09 File Offset: 0x00006D09
		public static ParallelQuery<TResult> Zip<TFirst, TSecond, TResult>(this ParallelQuery<TFirst> first, ParallelQuery<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new ZipQueryOperator<TFirst, TSecond, TResult>(first, second, resultSelector);
		}

		/// <summary>This Zip overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when invoked.</summary>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <param name="resultSelector">This parameter is not used.</param>
		/// <typeparam name="TFirst">This type parameter is not used.</typeparam>
		/// <typeparam name="TSecond">This type parameter is not used.</typeparam>
		/// <typeparam name="TResult">This type parameter is not used.</typeparam>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		// Token: 0x06000381 RID: 897 RVA: 0x00008B3D File Offset: 0x00006D3D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TResult> Zip<TFirst, TSecond, TResult>(this ParallelQuery<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Correlates in parallel the elements of two sequences based on matching keys. The default equality comparer is used to compare keys.</summary>
		/// <returns>A sequence that has elements of type <paramref name="TResult" /> that are obtained by performing an inner join on two sequences.</returns>
		/// <param name="outer">The first sequence to join.</param>
		/// <param name="inner">The sequence to join to the first sequence.</param>
		/// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
		/// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
		/// <param name="resultSelector">A function to create a result element from two matching elements.</param>
		/// <typeparam name="TOuter">The type of the elements of the second sequence.</typeparam>
		/// <typeparam name="TInner">The type of the elements of the first sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
		/// <typeparam name="TResult">The type of the result elements.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000382 RID: 898 RVA: 0x00008B49 File Offset: 0x00006D49
		public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
		{
			return outer.Join(inner, outerKeySelector, innerKeySelector, resultSelector, null);
		}

		/// <summary>This Join overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when invoked.</summary>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="outer">This parameter is not used.</param>
		/// <param name="inner">This parameter is not used.</param>
		/// <param name="outerKeySelector">This parameter is not used.</param>
		/// <param name="innerKeySelector">This parameter is not used.</param>
		/// <param name="resultSelector">This parameter is not used.</param>
		/// <typeparam name="TOuter">This type parameter is not used.</typeparam>
		/// <typeparam name="TInner">This type parameter is not used.</typeparam>
		/// <typeparam name="TKey">This type parameter is not used.</typeparam>
		/// <typeparam name="TResult">This type parameter is not used.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000383 RID: 899 RVA: 0x00008B3D File Offset: 0x00006D3D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Correlates in parallel the elements of two sequences based on matching keys. A specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> is used to compare keys.</summary>
		/// <returns>A sequence that has elements of type <paramref name="TResult" /> that are obtained by performing an inner join on two sequences.</returns>
		/// <param name="outer">The first sequence to join.</param>
		/// <param name="inner">The sequence to join to the first sequence.</param>
		/// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
		/// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
		/// <param name="resultSelector">A function to create a result element from two matching elements.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to hash and compare keys.</param>
		/// <typeparam name="TOuter">The type of the elements of the second sequence.</typeparam>
		/// <typeparam name="TInner">The type of the elements of the first sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
		/// <typeparam name="TResult">The type of the result elements.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000384 RID: 900 RVA: 0x00008B58 File Offset: 0x00006D58
		public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (outer == null)
			{
				throw new ArgumentNullException("outer");
			}
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			if (outerKeySelector == null)
			{
				throw new ArgumentNullException("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw new ArgumentNullException("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new JoinQueryOperator<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
		}

		/// <summary>This Join overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when invoked.</summary>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="outer">This parameter is not used.</param>
		/// <param name="inner">This parameter is not used.</param>
		/// <param name="outerKeySelector">This parameter is not used.</param>
		/// <param name="innerKeySelector">This parameter is not used.</param>
		/// <param name="resultSelector">This parameter is not used.</param>
		/// <param name="comparer">This parameter is not used.</param>
		/// <typeparam name="TOuter">This type parameter is not used.</typeparam>
		/// <typeparam name="TInner">This type parameter is not used.</typeparam>
		/// <typeparam name="TKey">This type parameter is not used.</typeparam>
		/// <typeparam name="TResult">This type parameter is not used.</typeparam>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		// Token: 0x06000385 RID: 901 RVA: 0x00008B3D File Offset: 0x00006D3D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Correlates in parallel the elements of two sequences based on equality of keys and groups the results. The default equality comparer is used to compare keys.</summary>
		/// <returns>A sequence that has elements of type <paramref name="TResult" /> that are obtained by performing a grouped join on two sequences.</returns>
		/// <param name="outer">The first sequence to join.</param>
		/// <param name="inner">The sequence to join to the first sequence.</param>
		/// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
		/// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
		/// <param name="resultSelector">A function to create a result element from an element from the first sequence and a collection of matching elements from the second sequence.</param>
		/// <typeparam name="TOuter">The type of the elements of the second sequence.</typeparam>
		/// <typeparam name="TInner">The type of the elements of the first sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
		/// <typeparam name="TResult">The type of the result elements.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000386 RID: 902 RVA: 0x00008BB9 File Offset: 0x00006DB9
		public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
		{
			return outer.GroupJoin(inner, outerKeySelector, innerKeySelector, resultSelector, null);
		}

		/// <summary>This GroupJoin overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="outer">This parameter is not used.</param>
		/// <param name="inner">This parameter is not used.</param>
		/// <param name="outerKeySelector">This parameter is not used.</param>
		/// <param name="innerKeySelector">This parameter is not used.</param>
		/// <param name="resultSelector">This parameter is not used.</param>
		/// <typeparam name="TOuter">This type parameter is not used.</typeparam>
		/// <typeparam name="TInner">This type parameter is not used.</typeparam>
		/// <typeparam name="TKey">This type parameter is not used.</typeparam>
		/// <typeparam name="TResult">This type parameter is not used.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000387 RID: 903 RVA: 0x00008B3D File Offset: 0x00006D3D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Correlates in parallel the elements of two sequences based on key equality and groups the results. A specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> is used to compare keys.</summary>
		/// <returns>A sequence that has elements of type <paramref name="TResult" /> that are obtained by performing a grouped join on two sequences.</returns>
		/// <param name="outer">The first sequence to join.</param>
		/// <param name="inner">The sequence to join to the first sequence.</param>
		/// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
		/// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
		/// <param name="resultSelector">A function to create a result element from an element from the first sequence and a collection of matching elements from the second sequence.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to hash and compare keys.</param>
		/// <typeparam name="TOuter">The type of the elements of the second sequence.</typeparam>
		/// <typeparam name="TInner">The type of the elements of the first sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
		/// <typeparam name="TResult">The type of the result elements.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000388 RID: 904 RVA: 0x00008BC8 File Offset: 0x00006DC8
		public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (outer == null)
			{
				throw new ArgumentNullException("outer");
			}
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			if (outerKeySelector == null)
			{
				throw new ArgumentNullException("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw new ArgumentNullException("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new GroupJoinQueryOperator<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
		}

		/// <summary>This GroupJoin overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="outer">This parameter is not used.</param>
		/// <param name="inner">This parameter is not used.</param>
		/// <param name="outerKeySelector">This parameter is not used.</param>
		/// <param name="innerKeySelector">This parameter is not used.</param>
		/// <param name="resultSelector">This parameter is not used.</param>
		/// <param name="comparer">This parameter is not used.</param>
		/// <typeparam name="TOuter">This type parameter is not used.</typeparam>
		/// <typeparam name="TInner">This type parameter is not used.</typeparam>
		/// <typeparam name="TKey">This type parameter is not used.</typeparam>
		/// <typeparam name="TResult">This type parameter is not used.</typeparam>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		// Token: 0x06000389 RID: 905 RVA: 0x00008B3D File Offset: 0x00006D3D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Projects in parallel each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" /> and flattens the resulting sequences into one sequence.</summary>
		/// <returns>A sequence whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.</returns>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the elements of the sequence returned by selector.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600038A RID: 906 RVA: 0x00008C29 File Offset: 0x00006E29
		public static ParallelQuery<TResult> SelectMany<TSource, TResult>(this ParallelQuery<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			return new SelectManyQueryOperator<TSource, TResult, TResult>(source, selector, null, null);
		}

		/// <summary>Projects in parallel each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" />, and flattens the resulting sequences into one sequence. The index of each source element is used in the projected form of that element.</summary>
		/// <returns>A sequence whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.</returns>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the elements of the sequence returned by selector.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.OverflowException">More than <see cref="F:System.Int32.MaxValue" /> elements are enumerated by the query.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600038B RID: 907 RVA: 0x00008C50 File Offset: 0x00006E50
		public static ParallelQuery<TResult> SelectMany<TSource, TResult>(this ParallelQuery<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			return new SelectManyQueryOperator<TSource, TResult, TResult>(source, null, selector, null);
		}

		/// <summary>Projects each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" />, flattens the resulting sequences into one sequence, and invokes a result selector function on each element therein.</summary>
		/// <returns>A sequence whose elements are the result of invoking the one-to-many transform function <paramref name="collectionSelector" /> on each element of <paramref name="source" /> based on the index supplied to <paramref name="collectionSelector" />, and then mapping each of those sequence elements and their corresponding source element to a result element. </returns>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="collectionSelector">A transform function to apply to each source element; the second parameter of the function represents the index of the source element.</param>
		/// <param name="resultSelector">A function to create a result element from an element from the first sequence and a collection of matching elements from the second sequence.</param>
		/// <typeparam name="TSource">The type of the intermediate elements collected by <paramref name="collectionSelector" />.</typeparam>
		/// <typeparam name="TCollection">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of elements in the result sequence.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.OverflowException">More than <see cref="F:System.Int32.MaxValue" /> elements are enumerated by the query.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600038C RID: 908 RVA: 0x00008C77 File Offset: 0x00006E77
		public static ParallelQuery<TResult> SelectMany<TSource, TCollection, TResult>(this ParallelQuery<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (collectionSelector == null)
			{
				throw new ArgumentNullException("collectionSelector");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new SelectManyQueryOperator<TSource, TCollection, TResult>(source, collectionSelector, null, resultSelector);
		}

		/// <summary>Projects each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" />, flattens the resulting sequences into one sequence, and invokes a result selector function on each element therein. The index of each source element is used in the intermediate projected form of that element.</summary>
		/// <returns>A sequence whose elements are the result of invoking the one-to-many transform function <paramref name="collectionSelector" /> on each element of <paramref name="source" /> based on the index supplied to <paramref name="collectionSelector" />, and then mapping each of those sequence elements and their corresponding source element to a result element.</returns>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="collectionSelector">A transform function to apply to each source element; the second parameter of the function represents the index of the source element.</param>
		/// <param name="resultSelector">A function to create a result element from an element from the first sequence and a collection of matching elements from the second sequence.</param>
		/// <typeparam name="TSource">The type of the intermediate elements collected by <paramref name="collectionSelector" />.</typeparam>
		/// <typeparam name="TCollection">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of elements to return.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.OverflowException">More than <see cref="F:System.Int32.MaxValue" /> elements are enumerated by the query.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600038D RID: 909 RVA: 0x00008CAC File Offset: 0x00006EAC
		public static ParallelQuery<TResult> SelectMany<TSource, TCollection, TResult>(this ParallelQuery<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (collectionSelector == null)
			{
				throw new ArgumentNullException("collectionSelector");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new SelectManyQueryOperator<TSource, TCollection, TResult>(source, null, collectionSelector, resultSelector);
		}

		/// <summary>Sorts in parallel the elements of a sequence in ascending order according to a key.</summary>
		/// <returns>An OrderedParallelQuery{TSource} whose elements are sorted according to a key.</returns>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600038E RID: 910 RVA: 0x00008CE1 File Offset: 0x00006EE1
		public static OrderedParallelQuery<TSource> OrderBy<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>(new SortQueryOperator<TSource, TKey>(source, keySelector, null, false));
		}

		/// <summary>Sorts in parallel the elements of a sequence in ascending order by using a specified comparer.</summary>
		/// <returns>An OrderedParallelQuery{TSource} whose elements are sorted according to a key.</returns>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An IComparer{TKey} to compare keys.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600038F RID: 911 RVA: 0x00008D0D File Offset: 0x00006F0D
		public static OrderedParallelQuery<TSource> OrderBy<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>(new SortQueryOperator<TSource, TKey>(source, keySelector, comparer, false));
		}

		/// <summary>Sorts in parallel the elements of a sequence in descending order according to a key.</summary>
		/// <returns>An OrderedParallelQuery{TSource} whose elements are sorted descending according to a key.</returns>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000390 RID: 912 RVA: 0x00008D39 File Offset: 0x00006F39
		public static OrderedParallelQuery<TSource> OrderByDescending<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>(new SortQueryOperator<TSource, TKey>(source, keySelector, null, true));
		}

		/// <summary>Sorts the elements of a sequence in descending order by using a specified comparer.</summary>
		/// <returns>An OrderedParallelQuery{TSource} whose elements are sorted descending according to a key.</returns>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An IComparer{TKey} to compare keys.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="KeySelector" /> is a null reference (Nothing in Visual Basic)..</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000391 RID: 913 RVA: 0x00008D65 File Offset: 0x00006F65
		public static OrderedParallelQuery<TSource> OrderByDescending<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>(new SortQueryOperator<TSource, TKey>(source, keySelector, comparer, true));
		}

		/// <summary>Performs in parallel a subsequent ordering of the elements in a sequence in ascending order according to a key.</summary>
		/// <returns>An OrderedParallelQuery{TSource} whose elements are sorted according to a key.</returns>
		/// <param name="source">An OrderedParallelQuery{TSource} that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000392 RID: 914 RVA: 0x00008D91 File Offset: 0x00006F91
		public static OrderedParallelQuery<TSource> ThenBy<TSource, TKey>(this OrderedParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>((QueryOperator<TSource>)source.OrderedEnumerable.CreateOrderedEnumerable<TKey>(keySelector, null, false));
		}

		/// <summary>Performs in parallel a subsequent ordering of the elements in a sequence in ascending order by using a specified comparer.</summary>
		/// <returns>An OrderedParallelQuery{TSource} whose elements are sorted according to a key.</returns>
		/// <param name="source">An OrderedParallelQuery{TSource} that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An IComparer{TKey} to compare keys.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000393 RID: 915 RVA: 0x00008DC7 File Offset: 0x00006FC7
		public static OrderedParallelQuery<TSource> ThenBy<TSource, TKey>(this OrderedParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>((QueryOperator<TSource>)source.OrderedEnumerable.CreateOrderedEnumerable<TKey>(keySelector, comparer, false));
		}

		/// <summary>Performs in parallel a subsequent ordering of the elements in a sequence in descending order, according to a key.</summary>
		/// <returns>A sequence whose elements are sorted descending according to a key.</returns>
		/// <param name="source">An OrderedParallelQuery{TSource} that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000394 RID: 916 RVA: 0x00008DFD File Offset: 0x00006FFD
		public static OrderedParallelQuery<TSource> ThenByDescending<TSource, TKey>(this OrderedParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>((QueryOperator<TSource>)source.OrderedEnumerable.CreateOrderedEnumerable<TKey>(keySelector, null, true));
		}

		/// <summary>Performs in parallel a subsequent ordering of the elements in a sequence in descending order by using a specified comparer.</summary>
		/// <returns>A sequence whose elements are sorted descending according to a key.</returns>
		/// <param name="source">An OrderedParallelQuery{TSource} that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An IComparer{TKey} to compare keys.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000395 RID: 917 RVA: 0x00008E33 File Offset: 0x00007033
		public static OrderedParallelQuery<TSource> ThenByDescending<TSource, TKey>(this OrderedParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>((QueryOperator<TSource>)source.OrderedEnumerable.CreateOrderedEnumerable<TKey>(keySelector, comparer, true));
		}

		/// <summary>Groups in parallel the elements of a sequence according to a specified key selector function.</summary>
		/// <returns>A sequence of groups that are sorted descending according to <paramref name="TKey" />.</returns>
		/// <param name="source">An OrderedParallelQuery{TSource}that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000396 RID: 918 RVA: 0x00008E69 File Offset: 0x00007069
		public static ParallelQuery<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.GroupBy(keySelector, null);
		}

		/// <summary>Groups in parallel the elements of a sequence according to a specified key selector function and compares the keys by using a specified <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <returns>A sequence of groups that are sorted descending according to <paramref name="TKey" />.</returns>
		/// <param name="source">An <see cref="T:System.Linq.OrderedParallelQuery`1" /> that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />&gt;.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000397 RID: 919 RVA: 0x00008E73 File Offset: 0x00007073
		public static ParallelQuery<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new GroupByQueryOperator<TSource, TKey, TSource>(source, keySelector, null, comparer);
		}

		/// <summary>Groups in parallel the elements of a sequence according to a specified key selector function and projects the elements for each group by using a specified function.</summary>
		/// <returns>A sequence of groups that are sorted descending according to <paramref name="TKey" />.</returns>
		/// <param name="source">An <see cref="T:System.Linq.OrderedParallelQuery`1" /> that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="elementSelector">A function to map each source element to an element in an <see cref="T:System.Linq.IGrouping`2" />.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the elements in the <see cref="T:System.Linq.IGrouping`2" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000398 RID: 920 RVA: 0x00008E9A File Offset: 0x0000709A
		public static ParallelQuery<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return source.GroupBy(keySelector, elementSelector, null);
		}

		/// <summary>Groups in parallel the elements of a sequence according to a key selector function. The keys are compared by using a comparer and each group's elements are projected by using a specified function.</summary>
		/// <returns>A sequence of groups that are sorted descending according to <paramref name="TKey" />.</returns>
		/// <param name="source">An OrderedParallelQuery{TSource}that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="elementSelector">A function to map each source element to an element in an IGrouping.</param>
		/// <param name="comparer">An IComparer{TSource} to compare keys.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the elements in the IGrouping</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000399 RID: 921 RVA: 0x00008EA5 File Offset: 0x000070A5
		public static ParallelQuery<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (elementSelector == null)
			{
				throw new ArgumentNullException("elementSelector");
			}
			return new GroupByQueryOperator<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer);
		}

		/// <summary>Groups in parallel the elements of a sequence according to a specified key selector function and creates a result value from each group and its key.</summary>
		/// <returns>A sequence of elements of type <paramref name="TResult" /> where each element represents a projection over a group and its key.</returns>
		/// <param name="source">A sequence whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="resultSelector">A function to create a result value from each group.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600039A RID: 922 RVA: 0x00008EDC File Offset: 0x000070DC
		public static ParallelQuery<TResult> GroupBy<TSource, TKey, TResult>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector)
		{
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return from grouping in source.GroupBy(keySelector)
				select resultSelector(grouping.Key, grouping);
		}

		/// <summary>Groups in parallel the elements of a sequence according to a specified key selector function and creates a result value from each group and its key. The keys are compared by using a specified comparer.</summary>
		/// <returns>A sequence of groups.</returns>
		/// <param name="source">A sequence whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="resultSelector">A function to create a result value from each group.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600039B RID: 923 RVA: 0x00008F24 File Offset: 0x00007124
		public static ParallelQuery<TResult> GroupBy<TSource, TKey, TResult>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return from grouping in source.GroupBy(keySelector, comparer)
				select resultSelector(grouping.Key, grouping);
		}

		/// <summary>Groups in parallel the elements of a sequence according to a specified key selector function and creates a result value from each group and its key. The elements of each group are projected by using a specified function.</summary>
		/// <returns>A sequence of elements of type <paramref name="TResult" /> where each element represents a projection over a group and its key.</returns>
		/// <param name="source">A sequence whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="elementSelector">A function to map each source element to an element in an IGrouping&lt;TKey, TElement&gt;.</param>
		/// <param name="resultSelector">A function to create a result value from each group.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the elements in each IGrouping{TKey, TElement}.</typeparam>
		/// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600039C RID: 924 RVA: 0x00008F6C File Offset: 0x0000716C
		public static ParallelQuery<TResult> GroupBy<TSource, TKey, TElement, TResult>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
		{
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return from grouping in source.GroupBy(keySelector, elementSelector)
				select resultSelector(grouping.Key, grouping);
		}

		/// <summary>Groups the elements of a sequence according to a specified key selector function and creates a result value from each group and its key. Key values are compared by using a specified comparer, and the elements of each group are projected by using a specified function.</summary>
		/// <returns>A sequence of elements of type <paramref name="TResult" /> where each element represents a projection over a group and its key.</returns>
		/// <param name="source">A sequence whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="elementSelector">A function to map each source element to an element in an IGrouping{Key, TElement}.</param>
		/// <param name="resultSelector">A function to create a result value from each group.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the elements in each IGrouping{TKey, TElement}.</typeparam>
		/// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600039D RID: 925 RVA: 0x00008FB4 File Offset: 0x000071B4
		public static ParallelQuery<TResult> GroupBy<TSource, TKey, TElement, TResult>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return from grouping in source.GroupBy(keySelector, elementSelector, comparer)
				select resultSelector(grouping.Key, grouping);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00008FFC File Offset: 0x000071FC
		private static T PerformAggregation<T>(this ParallelQuery<T> source, Func<T, T, T> reduce, T seed, bool seedIsSpecified, bool throwIfEmpty, QueryAggregationOptions options)
		{
			return new AssociativeAggregationOperator<T, T, T>(source, seed, null, seedIsSpecified, reduce, reduce, (T obj) => obj, throwIfEmpty, options).Aggregate();
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000903C File Offset: 0x0000723C
		private static TAccumulate PerformSequentialAggregation<TSource, TAccumulate>(this ParallelQuery<TSource> source, TAccumulate seed, bool seedIsSpecified, Func<TAccumulate, TSource, TAccumulate> func)
		{
			TAccumulate taccumulate2;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				TAccumulate taccumulate;
				if (seedIsSpecified)
				{
					taccumulate = seed;
				}
				else
				{
					if (!enumerator.MoveNext())
					{
						throw new InvalidOperationException("Sequence contains no elements");
					}
					taccumulate = (TAccumulate)((object)enumerator.Current);
				}
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					try
					{
						taccumulate = func(taccumulate, tsource);
					}
					catch (Exception ex)
					{
						throw new AggregateException(new Exception[] { ex });
					}
				}
				taccumulate2 = taccumulate;
			}
			return taccumulate2;
		}

		/// <summary>Applies in parallel an accumulator function over a sequence.</summary>
		/// <returns>The final accumulator value.</returns>
		/// <param name="source">A sequence to aggregate over.</param>
		/// <param name="func">An accumulator function to be invoked on each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="func" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003A0 RID: 928 RVA: 0x000090D8 File Offset: 0x000072D8
		public static TSource Aggregate<TSource>(this ParallelQuery<TSource> source, Func<TSource, TSource, TSource> func)
		{
			return source.Aggregate(func, QueryAggregationOptions.AssociativeCommutative);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000090E4 File Offset: 0x000072E4
		internal static TSource Aggregate<TSource>(this ParallelQuery<TSource> source, Func<TSource, TSource, TSource> func, QueryAggregationOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			if ((~(QueryAggregationOptions.Associative | QueryAggregationOptions.Commutative) & options) != QueryAggregationOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			if ((options & QueryAggregationOptions.Associative) != QueryAggregationOptions.Associative)
			{
				return source.PerformSequentialAggregation(default(TSource), false, func);
			}
			return source.PerformAggregation(func, default(TSource), false, true, options);
		}

		/// <summary>Applies in parallel an accumulator function over a sequence. The specified seed value is used as the initial accumulator value.</summary>
		/// <returns>The final accumulator value.</returns>
		/// <param name="source">A sequence to aggregate over.</param>
		/// <param name="seed">The initial accumulator value.</param>
		/// <param name="func">An accumulator function to be invoked on each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="func" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003A2 RID: 930 RVA: 0x00009149 File Offset: 0x00007349
		public static TAccumulate Aggregate<TSource, TAccumulate>(this ParallelQuery<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
		{
			return source.Aggregate(seed, func, QueryAggregationOptions.AssociativeCommutative);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00009154 File Offset: 0x00007354
		internal static TAccumulate Aggregate<TSource, TAccumulate>(this ParallelQuery<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, QueryAggregationOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			if ((~(QueryAggregationOptions.Associative | QueryAggregationOptions.Commutative) & options) != QueryAggregationOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			return source.PerformSequentialAggregation(seed, true, func);
		}

		/// <summary>Applies in parallel an accumulator function over a sequence. The specified seed value is used as the initial accumulator value, and the specified function is used to select the result value.</summary>
		/// <returns>The transformed final accumulator value.</returns>
		/// <param name="source">A sequence to aggregate over.</param>
		/// <param name="seed">The initial accumulator value.</param>
		/// <param name="func">An accumulator function to be invoked on each element.</param>
		/// <param name="resultSelector">A function to transform the final accumulator value into the result value.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
		/// <typeparam name="TResult">The type of the resulting value.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="func" /> or <paramref name="resultSelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003A4 RID: 932 RVA: 0x0000918C File Offset: 0x0000738C
		public static TResult Aggregate<TSource, TAccumulate, TResult>(this ParallelQuery<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			TAccumulate taccumulate = source.PerformSequentialAggregation(seed, true, func);
			TResult tresult;
			try
			{
				tresult = resultSelector(taccumulate);
			}
			catch (Exception ex)
			{
				throw new AggregateException(new Exception[] { ex });
			}
			return tresult;
		}

		/// <summary>Applies in parallel an accumulator function over a sequence. This overload is not available in the sequential implementation.</summary>
		/// <returns>The transformed final accumulator value.</returns>
		/// <param name="source">A sequence to aggregate over.</param>
		/// <param name="seed">The initial accumulator value.</param>
		/// <param name="updateAccumulatorFunc">An accumulator function to be invoked on each element in a partition. </param>
		/// <param name="combineAccumulatorsFunc">An accumulator function to be invoked on the yielded accumulator result from each partition. </param>
		/// <param name="resultSelector">A function to transform the final accumulator value into the result value. </param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
		/// <typeparam name="TResult">The type of the resulting value.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="updateAccumulatorFunc" /> or <paramref name="combineAccumulatorsFunc" /> or <paramref name="resultSelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003A5 RID: 933 RVA: 0x000091FC File Offset: 0x000073FC
		public static TResult Aggregate<TSource, TAccumulate, TResult>(this ParallelQuery<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> updateAccumulatorFunc, Func<TAccumulate, TAccumulate, TAccumulate> combineAccumulatorsFunc, Func<TAccumulate, TResult> resultSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (updateAccumulatorFunc == null)
			{
				throw new ArgumentNullException("updateAccumulatorFunc");
			}
			if (combineAccumulatorsFunc == null)
			{
				throw new ArgumentNullException("combineAccumulatorsFunc");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new AssociativeAggregationOperator<TSource, TAccumulate, TResult>(source, seed, null, true, updateAccumulatorFunc, combineAccumulatorsFunc, resultSelector, false, QueryAggregationOptions.AssociativeCommutative).Aggregate();
		}

		/// <summary>Applies in parallel an accumulator function over a sequence. This overload is not available in the sequential implementation.</summary>
		/// <returns>The transformed final accumulator value.</returns>
		/// <param name="source">A sequence to aggregate over.</param>
		/// <param name="seedFactory">A function that returns the initial accumulator value. </param>
		/// <param name="updateAccumulatorFunc">An accumulator function to be invoked on each element in a partition. </param>
		/// <param name="combineAccumulatorsFunc">An accumulator function to be invoked on the yielded accumulator result from each partition.</param>
		/// <param name="resultSelector">A function to transform the final accumulator value into the result value. </param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
		/// <typeparam name="TResult">The type of the resulting value.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="seedFactory" /> or <paramref name="updateAccumulatorFunc" /> or <paramref name="combineAccumulatorsFunc" /> or <paramref name="resultSelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003A6 RID: 934 RVA: 0x00009258 File Offset: 0x00007458
		public static TResult Aggregate<TSource, TAccumulate, TResult>(this ParallelQuery<TSource> source, Func<TAccumulate> seedFactory, Func<TAccumulate, TSource, TAccumulate> updateAccumulatorFunc, Func<TAccumulate, TAccumulate, TAccumulate> combineAccumulatorsFunc, Func<TAccumulate, TResult> resultSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (seedFactory == null)
			{
				throw new ArgumentNullException("seedFactory");
			}
			if (updateAccumulatorFunc == null)
			{
				throw new ArgumentNullException("updateAccumulatorFunc");
			}
			if (combineAccumulatorsFunc == null)
			{
				throw new ArgumentNullException("combineAccumulatorsFunc");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new AssociativeAggregationOperator<TSource, TAccumulate, TResult>(source, default(TAccumulate), seedFactory, true, updateAccumulatorFunc, combineAccumulatorsFunc, resultSelector, false, QueryAggregationOptions.AssociativeCommutative).Aggregate();
		}

		/// <summary>Returns the number of elements in a parallel sequence.</summary>
		/// <returns>The number of elements in the input sequence.</returns>
		/// <param name="source">A sequence that contains elements to be counted.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The number of elements in source is larger than <see cref="F:System.Int32.MaxValue" />. (In this case the InnerException is <see cref="T:System.OverflowException" />) -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003A7 RID: 935 RVA: 0x000092C8 File Offset: 0x000074C8
		public static int Count<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			ParallelEnumerableWrapper<TSource> parallelEnumerableWrapper = source as ParallelEnumerableWrapper<TSource>;
			if (parallelEnumerableWrapper != null)
			{
				ICollection<TSource> collection = parallelEnumerableWrapper.WrappedEnumerable as ICollection<TSource>;
				if (collection != null)
				{
					return collection.Count;
				}
			}
			return new CountAggregationOperator<TSource>(source).Aggregate();
		}

		/// <summary>Returns a number that represents how many elements in the specified parallel sequence satisfy a condition.</summary>
		/// <returns>A number that represents how many elements in the sequence satisfy the condition in the predicate function.</returns>
		/// <param name="source">A sequence that contains elements to be counted.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The number of elements in source is larger than <see cref="F:System.Int32.MaxValue" />. (In this case the InnerException is <see cref="T:System.OverflowException" />) -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003A8 RID: 936 RVA: 0x0000930E File Offset: 0x0000750E
		public static int Count<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new CountAggregationOperator<TSource>(source.Where(predicate)).Aggregate();
		}

		/// <summary>Returns an Int64 that represents the total number of elements in a parallel sequence.</summary>
		/// <returns>The number of elements in the input sequence.</returns>
		/// <param name="source">A sequence that contains elements to be counted.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The number of elements in source is larger than <see cref="F:System.Int64.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.OverflowException">The computed result is greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060003A9 RID: 937 RVA: 0x00009340 File Offset: 0x00007540
		public static long LongCount<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			ParallelEnumerableWrapper<TSource> parallelEnumerableWrapper = source as ParallelEnumerableWrapper<TSource>;
			if (parallelEnumerableWrapper != null)
			{
				ICollection<TSource> collection = parallelEnumerableWrapper.WrappedEnumerable as ICollection<TSource>;
				if (collection != null)
				{
					return (long)collection.Count;
				}
			}
			return new LongCountAggregationOperator<TSource>(source).Aggregate();
		}

		/// <summary>Returns an Int64 that represents how many elements in a parallel sequence satisfy a condition.</summary>
		/// <returns>A number that represents how many elements in the sequence satisfy the condition in the predicate function.</returns>
		/// <param name="source">A sequence that contains elements to be counted.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The number of elements in source is larger than <see cref="F:System.Int64.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.OverflowException">The computed result is greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060003AA RID: 938 RVA: 0x00009387 File Offset: 0x00007587
		public static long LongCount<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new LongCountAggregationOperator<TSource>(source.Where(predicate)).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003AB RID: 939 RVA: 0x000093B6 File Offset: 0x000075B6
		public static int Sum(this ParallelQuery<int> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new IntSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003AC RID: 940 RVA: 0x000093D1 File Offset: 0x000075D1
		public static int? Sum(this ParallelQuery<int?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableIntSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Int64.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003AD RID: 941 RVA: 0x000093EC File Offset: 0x000075EC
		public static long Sum(this ParallelQuery<long> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new LongSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Int64.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003AE RID: 942 RVA: 0x00009407 File Offset: 0x00007607
		public static long? Sum(this ParallelQuery<long?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableLongSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Single.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003AF RID: 943 RVA: 0x00009422 File Offset: 0x00007622
		public static float Sum(this ParallelQuery<float> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new FloatSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Single.MaxValue" />. -or-  One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003B0 RID: 944 RVA: 0x0000943D File Offset: 0x0000763D
		public static float? Sum(this ParallelQuery<float?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableFloatSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Double.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003B1 RID: 945 RVA: 0x00009458 File Offset: 0x00007658
		public static double Sum(this ParallelQuery<double> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DoubleSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Double.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003B2 RID: 946 RVA: 0x00009473 File Offset: 0x00007673
		public static double? Sum(this ParallelQuery<double?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDoubleSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003B3 RID: 947 RVA: 0x0000948E File Offset: 0x0000768E
		public static decimal Sum(this ParallelQuery<decimal> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DecimalSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003B4 RID: 948 RVA: 0x000094A9 File Offset: 0x000076A9
		public static decimal? Sum(this ParallelQuery<decimal?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDecimalSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003B5 RID: 949 RVA: 0x000094C4 File Offset: 0x000076C4
		public static int Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, int> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003B6 RID: 950 RVA: 0x000094D2 File Offset: 0x000076D2
		public static int? Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, int?> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Int64.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003B7 RID: 951 RVA: 0x000094E0 File Offset: 0x000076E0
		public static long Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, long> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Int64.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003B8 RID: 952 RVA: 0x000094EE File Offset: 0x000076EE
		public static long? Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, long?> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Single.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003B9 RID: 953 RVA: 0x000094FC File Offset: 0x000076FC
		public static float Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, float> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Single.MaxValue" />. -or-  One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003BA RID: 954 RVA: 0x0000950A File Offset: 0x0000770A
		public static float? Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, float?> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Double.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003BB RID: 955 RVA: 0x00009518 File Offset: 0x00007718
		public static double Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, double> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Double.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003BC RID: 956 RVA: 0x00009526 File Offset: 0x00007726
		public static double? Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, double?> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003BD RID: 957 RVA: 0x00009534 File Offset: 0x00007734
		public static decimal Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003BE RID: 958 RVA: 0x00009542 File Offset: 0x00007742
		public static decimal? Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal?> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x060003BF RID: 959 RVA: 0x00009550 File Offset: 0x00007750
		public static int Min(this ParallelQuery<int> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new IntMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003C0 RID: 960 RVA: 0x0000956C File Offset: 0x0000776C
		public static int? Min(this ParallelQuery<int?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableIntMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x060003C1 RID: 961 RVA: 0x00009588 File Offset: 0x00007788
		public static long Min(this ParallelQuery<long> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new LongMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003C2 RID: 962 RVA: 0x000095A4 File Offset: 0x000077A4
		public static long? Min(this ParallelQuery<long?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableLongMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x060003C3 RID: 963 RVA: 0x000095C0 File Offset: 0x000077C0
		public static float Min(this ParallelQuery<float> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new FloatMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003C4 RID: 964 RVA: 0x000095DC File Offset: 0x000077DC
		public static float? Min(this ParallelQuery<float?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableFloatMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x060003C5 RID: 965 RVA: 0x000095F8 File Offset: 0x000077F8
		public static double Min(this ParallelQuery<double> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DoubleMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003C6 RID: 966 RVA: 0x00009614 File Offset: 0x00007814
		public static double? Min(this ParallelQuery<double?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDoubleMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x060003C7 RID: 967 RVA: 0x00009630 File Offset: 0x00007830
		public static decimal Min(this ParallelQuery<decimal> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DecimalMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003C8 RID: 968 RVA: 0x0000964C File Offset: 0x0000784C
		public static decimal? Min(this ParallelQuery<decimal?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDecimalMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x060003C9 RID: 969 RVA: 0x00009668 File Offset: 0x00007868
		public static TSource Min<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return AggregationMinMaxHelpers<TSource>.ReduceMin(source);
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x060003CA RID: 970 RVA: 0x0000967E File Offset: 0x0000787E
		public static int Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, int> selector)
		{
			return source.Select(selector).Min<int>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003CB RID: 971 RVA: 0x0000968C File Offset: 0x0000788C
		public static int? Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, int?> selector)
		{
			return source.Select(selector).Min<int?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x060003CC RID: 972 RVA: 0x0000969A File Offset: 0x0000789A
		public static long Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, long> selector)
		{
			return source.Select(selector).Min<long>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003CD RID: 973 RVA: 0x000096A8 File Offset: 0x000078A8
		public static long? Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, long?> selector)
		{
			return source.Select(selector).Min<long?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x060003CE RID: 974 RVA: 0x000096B6 File Offset: 0x000078B6
		public static float Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, float> selector)
		{
			return source.Select(selector).Min<float>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003CF RID: 975 RVA: 0x000096C4 File Offset: 0x000078C4
		public static float? Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, float?> selector)
		{
			return source.Select(selector).Min<float?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x060003D0 RID: 976 RVA: 0x000096D2 File Offset: 0x000078D2
		public static double Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, double> selector)
		{
			return source.Select(selector).Min<double>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003D1 RID: 977 RVA: 0x000096E0 File Offset: 0x000078E0
		public static double? Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, double?> selector)
		{
			return source.Select(selector).Min<double?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x060003D2 RID: 978 RVA: 0x000096EE File Offset: 0x000078EE
		public static decimal Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal> selector)
		{
			return source.Select(selector).Min<decimal>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003D3 RID: 979 RVA: 0x000096FC File Offset: 0x000078FC
		public static decimal? Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal?> selector)
		{
			return source.Select(selector).Min<decimal?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the value returned by <paramref name="selector" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x060003D4 RID: 980 RVA: 0x0000970A File Offset: 0x0000790A
		public static TResult Min<TSource, TResult>(this ParallelQuery<TSource> source, Func<TSource, TResult> selector)
		{
			return source.Select(selector).Min<TResult>();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003D5 RID: 981 RVA: 0x00009718 File Offset: 0x00007918
		public static int Max(this ParallelQuery<int> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new IntMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003D6 RID: 982 RVA: 0x00009734 File Offset: 0x00007934
		public static int? Max(this ParallelQuery<int?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableIntMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003D7 RID: 983 RVA: 0x00009750 File Offset: 0x00007950
		public static long Max(this ParallelQuery<long> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new LongMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003D8 RID: 984 RVA: 0x0000976C File Offset: 0x0000796C
		public static long? Max(this ParallelQuery<long?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableLongMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003D9 RID: 985 RVA: 0x00009788 File Offset: 0x00007988
		public static float Max(this ParallelQuery<float> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new FloatMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003DA RID: 986 RVA: 0x000097A4 File Offset: 0x000079A4
		public static float? Max(this ParallelQuery<float?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableFloatMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003DB RID: 987 RVA: 0x000097C0 File Offset: 0x000079C0
		public static double Max(this ParallelQuery<double> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DoubleMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003DC RID: 988 RVA: 0x000097DC File Offset: 0x000079DC
		public static double? Max(this ParallelQuery<double?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDoubleMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003DD RID: 989 RVA: 0x000097F8 File Offset: 0x000079F8
		public static decimal Max(this ParallelQuery<decimal> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DecimalMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003DE RID: 990 RVA: 0x00009814 File Offset: 0x00007A14
		public static decimal? Max(this ParallelQuery<decimal?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDecimalMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x060003DF RID: 991 RVA: 0x00009830 File Offset: 0x00007A30
		public static TSource Max<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return AggregationMinMaxHelpers<TSource>.ReduceMax(source);
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x060003E0 RID: 992 RVA: 0x00009846 File Offset: 0x00007A46
		public static int Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, int> selector)
		{
			return source.Select(selector).Max<int>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003E1 RID: 993 RVA: 0x00009854 File Offset: 0x00007A54
		public static int? Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, int?> selector)
		{
			return source.Select(selector).Max<int?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x060003E2 RID: 994 RVA: 0x00009862 File Offset: 0x00007A62
		public static long Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, long> selector)
		{
			return source.Select(selector).Max<long>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003E3 RID: 995 RVA: 0x00009870 File Offset: 0x00007A70
		public static long? Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, long?> selector)
		{
			return source.Select(selector).Max<long?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x060003E4 RID: 996 RVA: 0x0000987E File Offset: 0x00007A7E
		public static float Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, float> selector)
		{
			return source.Select(selector).Max<float>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003E5 RID: 997 RVA: 0x0000988C File Offset: 0x00007A8C
		public static float? Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, float?> selector)
		{
			return source.Select(selector).Max<float?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x060003E6 RID: 998 RVA: 0x0000989A File Offset: 0x00007A9A
		public static double Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, double> selector)
		{
			return source.Select(selector).Max<double>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003E7 RID: 999 RVA: 0x000098A8 File Offset: 0x00007AA8
		public static double? Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, double?> selector)
		{
			return source.Select(selector).Max<double?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x060003E8 RID: 1000 RVA: 0x000098B6 File Offset: 0x00007AB6
		public static decimal Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal> selector)
		{
			return source.Select(selector).Max<decimal>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003E9 RID: 1001 RVA: 0x000098C4 File Offset: 0x00007AC4
		public static decimal? Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal?> selector)
		{
			return source.Select(selector).Max<decimal?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the value returned by <paramref name="selector" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x060003EA RID: 1002 RVA: 0x000098D2 File Offset: 0x00007AD2
		public static TResult Max<TSource, TResult>(this ParallelQuery<TSource> source, Func<TSource, TResult> selector)
		{
			return source.Select(selector).Max<TResult>();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum or count of the elements in the sequence is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003EB RID: 1003 RVA: 0x000098E0 File Offset: 0x00007AE0
		public static double Average(this ParallelQuery<int> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new IntAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum or count of the elements in the sequence is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003EC RID: 1004 RVA: 0x000098FB File Offset: 0x00007AFB
		public static double? Average(this ParallelQuery<int?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableIntAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum or count of the elements in the sequence is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003ED RID: 1005 RVA: 0x00009916 File Offset: 0x00007B16
		public static double Average(this ParallelQuery<long> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new LongAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum or count of the elements in the sequence is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003EE RID: 1006 RVA: 0x00009931 File Offset: 0x00007B31
		public static double? Average(this ParallelQuery<long?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableLongAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003EF RID: 1007 RVA: 0x0000994C File Offset: 0x00007B4C
		public static float Average(this ParallelQuery<float> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new FloatAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003F0 RID: 1008 RVA: 0x00009967 File Offset: 0x00007B67
		public static float? Average(this ParallelQuery<float?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableFloatAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003F1 RID: 1009 RVA: 0x00009982 File Offset: 0x00007B82
		public static double Average(this ParallelQuery<double> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DoubleAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <returns>Returns the average of the sequence of values.</returns>
		/// <param name="source">The source sequence.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">A sequence of values that are used to calculate an average.The average of the sequence of values.<paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003F2 RID: 1010 RVA: 0x0000999D File Offset: 0x00007B9D
		public static double? Average(this ParallelQuery<double?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDoubleAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003F3 RID: 1011 RVA: 0x000099B8 File Offset: 0x00007BB8
		public static decimal Average(this ParallelQuery<decimal> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DecimalAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060003F4 RID: 1012 RVA: 0x000099D3 File Offset: 0x00007BD3
		public static decimal? Average(this ParallelQuery<decimal?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDecimalAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum or count of the elements in the sequence is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException"> (Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x060003F5 RID: 1013 RVA: 0x000099EE File Offset: 0x00007BEE
		public static double Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, int> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum or count of the elements in the sequence is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException"> (Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x060003F6 RID: 1014 RVA: 0x000099FC File Offset: 0x00007BFC
		public static double? Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, int?> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum or count of the elements in the sequence is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException"> (Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x060003F7 RID: 1015 RVA: 0x00009A0A File Offset: 0x00007C0A
		public static double Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, long> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum or count of the elements in the sequence is larger than <see cref="F:System.Int64.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException"> (Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x060003F8 RID: 1016 RVA: 0x00009A18 File Offset: 0x00007C18
		public static double? Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, long?> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException"> (Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x060003F9 RID: 1017 RVA: 0x00009A26 File Offset: 0x00007C26
		public static float Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, float> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException"> (Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x060003FA RID: 1018 RVA: 0x00009A34 File Offset: 0x00007C34
		public static float? Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, float?> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException"> (Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x060003FB RID: 1019 RVA: 0x00009A42 File Offset: 0x00007C42
		public static double Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, double> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException"> (Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x060003FC RID: 1020 RVA: 0x00009A50 File Offset: 0x00007C50
		public static double? Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, double?> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException">(Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x060003FD RID: 1021 RVA: 0x00009A5E File Offset: 0x00007C5E
		public static decimal Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException"> (Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x060003FE RID: 1022 RVA: 0x00009A6C File Offset: 0x00007C6C
		public static decimal? Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal?> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Determines in parallel whether any element of a sequence satisfies a condition.</summary>
		/// <returns>true if any elements in the source sequence pass the test in the specified predicate; otherwise, false.</returns>
		/// <param name="source">A sequence to whose elements the predicate will be applied.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060003FF RID: 1023 RVA: 0x00009A7A File Offset: 0x00007C7A
		public static bool Any<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new AnyAllSearchOperator<TSource>(source, true, predicate).Aggregate();
		}

		/// <summary>Determines whether a parallel sequence contains any elements.</summary>
		/// <returns>true if the source sequence contains any elements; otherwise, false.</returns>
		/// <param name="source">The sequence to check for emptiness.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000400 RID: 1024 RVA: 0x00009AA5 File Offset: 0x00007CA5
		public static bool Any<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.Any((TSource x) => true);
		}

		/// <summary>Determines in parallel whether all elements of a sequence satisfy a condition.</summary>
		/// <returns>true if every element of the source sequence passes the test in the specified predicate, or if the sequence is empty; otherwise, false..</returns>
		/// <param name="source">A sequence whose elements to apply the predicate to.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000401 RID: 1025 RVA: 0x00009ADA File Offset: 0x00007CDA
		public static bool All<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new AnyAllSearchOperator<TSource>(source, false, predicate).Aggregate();
		}

		/// <summary>Determines in parallel whether a sequence contains a specified element by using the default equality comparer.</summary>
		/// <returns>true if the source sequence contains an element that has the specified value; otherwise, false.</returns>
		/// <param name="source">A sequence in which to locate a value.</param>
		/// <param name="value">The value to locate in the sequence.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000402 RID: 1026 RVA: 0x00009B05 File Offset: 0x00007D05
		public static bool Contains<TSource>(this ParallelQuery<TSource> source, TSource value)
		{
			return source.Contains(value, null);
		}

		/// <summary>Determines in parallel whether a sequence contains a specified element by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <returns>true if the source sequence contains an element that has the specified value; otherwise, false.</returns>
		/// <param name="source">A sequence in which to locate a value.</param>
		/// <param name="value">The value to locate in the sequence.</param>
		/// <param name="comparer">An equality comparer to compare values.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000403 RID: 1027 RVA: 0x00009B0F File Offset: 0x00007D0F
		public static bool Contains<TSource>(this ParallelQuery<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ContainsSearchOperator<TSource>(source, value, comparer).Aggregate();
		}

		/// <summary>Returns a specified number of contiguous elements from the start of a parallel sequence.</summary>
		/// <returns>A sequence that contains the specified number of elements from the start of the input sequence.</returns>
		/// <param name="source">The sequence to return elements from.</param>
		/// <param name="count">The number of elements to return.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000404 RID: 1028 RVA: 0x00009B2C File Offset: 0x00007D2C
		public static ParallelQuery<TSource> Take<TSource>(this ParallelQuery<TSource> source, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (count > 0)
			{
				return new TakeOrSkipQueryOperator<TSource>(source, count, true);
			}
			return ParallelEnumerable.Empty<TSource>();
		}

		/// <summary>Returns elements from a parallel sequence as long as a specified condition is true.</summary>
		/// <returns>A sequence that contains the elements from the input sequence that occur before the element at which the test no longer passes.</returns>
		/// <param name="source">The sequence to return elements from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000405 RID: 1029 RVA: 0x00009B4E File Offset: 0x00007D4E
		public static ParallelQuery<TSource> TakeWhile<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new TakeOrSkipWhileQueryOperator<TSource>(source, predicate, null, true);
		}

		/// <summary>Returns elements from a parallel sequence as long as a specified condition is true. The element's index is used in the logic of the predicate function.</summary>
		/// <returns>A sequence that contains elements from the input sequence that occur before the element at which the test no longer passes.</returns>
		/// <param name="source">The sequence to return elements from.</param>
		/// <param name="predicate">A function to test each source element for a condition; the second parameter of the function represents the index of the source element. </param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.OverflowException">More than <see cref="F:System.Int32.MaxValue" /> elements are enumerated by this query.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000406 RID: 1030 RVA: 0x00009B75 File Offset: 0x00007D75
		public static ParallelQuery<TSource> TakeWhile<TSource>(this ParallelQuery<TSource> source, Func<TSource, int, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new TakeOrSkipWhileQueryOperator<TSource>(source, null, predicate, true);
		}

		/// <summary>Bypasses a specified number of elements in a parallel sequence and then returns the remaining elements.</summary>
		/// <returns>A sequence that contains the elements that occur after the specified index in the input sequence.</returns>
		/// <param name="source">The sequence to return elements from.</param>
		/// <param name="count">The number of elements to skip before returning the remaining elements.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.OverflowException">Count is greater than <see cref="F:System.Int32.MaxValue" /></exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000407 RID: 1031 RVA: 0x00009B9C File Offset: 0x00007D9C
		public static ParallelQuery<TSource> Skip<TSource>(this ParallelQuery<TSource> source, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (count <= 0)
			{
				return source;
			}
			return new TakeOrSkipQueryOperator<TSource>(source, count, false);
		}

		/// <summary>Bypasses elements in a parallel sequence as long as a specified condition is true and then returns the remaining elements.</summary>
		/// <returns>A sequence that contains the elements from the input sequence starting at the first element in the linear series that does not pass the test specified by predicate.</returns>
		/// <param name="source">The sequence to return elements from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000408 RID: 1032 RVA: 0x00009BBA File Offset: 0x00007DBA
		public static ParallelQuery<TSource> SkipWhile<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new TakeOrSkipWhileQueryOperator<TSource>(source, predicate, null, false);
		}

		/// <summary>Bypasses elements in a parallel sequence as long as a specified condition is true and then returns the remaining elements. The element's index is used in the logic of the predicate function.</summary>
		/// <returns>A sequence that contains the elements from the input sequence starting at the first element in the linear series that does not pass the test specified by predicate.</returns>
		/// <param name="source">The sequence to return elements from.</param>
		/// <param name="predicate">A function to test each source element for a condition; the second parameter of the function represents the index of the source element. </param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.OverflowException">More than <see cref="F:System.Int32.MaxValue" /> elements are enumerated by the query.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000409 RID: 1033 RVA: 0x00009BE1 File Offset: 0x00007DE1
		public static ParallelQuery<TSource> SkipWhile<TSource>(this ParallelQuery<TSource> source, Func<TSource, int, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new TakeOrSkipWhileQueryOperator<TSource>(source, null, predicate, false);
		}

		/// <summary>Concatenates two parallel sequences.</summary>
		/// <returns>A sequence that contains the concatenated elements of the two input sequences.</returns>
		/// <param name="first">The first sequence to concatenate.</param>
		/// <param name="second">The sequence to concatenate to the first sequence.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x0600040A RID: 1034 RVA: 0x00009C08 File Offset: 0x00007E08
		public static ParallelQuery<TSource> Concat<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			return new ConcatQueryOperator<TSource>(first, second);
		}

		/// <summary>This Concat overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <typeparam name="TSource">This type parameter is not used.</typeparam>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		// Token: 0x0600040B RID: 1035 RVA: 0x00008B3D File Offset: 0x00006D3D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TSource> Concat<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Determines whether two parallel sequences are equal by comparing the elements by using the default equality comparer for their type.</summary>
		/// <returns>true if the two source sequences are of equal length and their corresponding elements are equal according to the default equality comparer for their type; otherwise, false.</returns>
		/// <param name="first">A sequence to compare to second.</param>
		/// <param name="second">A sequence to compare to the first input sequence.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600040C RID: 1036 RVA: 0x00009C2D File Offset: 0x00007E2D
		public static bool SequenceEqual<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			return first.SequenceEqual(second, null);
		}

		/// <summary>This SequenceEqual overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <typeparam name="TSource">This type parameter is not used.</typeparam>
		/// <exception cref="T:System.NotSupportedException">Thrown every time this method is called.</exception>
		// Token: 0x0600040D RID: 1037 RVA: 0x00008B3D File Offset: 0x00006D3D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static bool SequenceEqual<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Determines whether two parallel sequences are equal by comparing their elements by using a specified IEqualityComparer{T}.</summary>
		/// <returns>true if the two source sequences are of equal length and their corresponding elements are equal according to the default equality comparer for their type; otherwise, false.</returns>
		/// <param name="first">A sequence to compare to <paramref name="second" />.</param>
		/// <param name="second">A sequence to compare to the first input sequence.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to use to compare elements.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600040E RID: 1038 RVA: 0x00009C54 File Offset: 0x00007E54
		public static bool SequenceEqual<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			comparer = comparer ?? EqualityComparer<TSource>.Default;
			ParallelQuery parallelQuery = QueryOperator<TSource>.AsQueryOperator(first);
			QueryOperator<TSource> queryOperator = QueryOperator<TSource>.AsQueryOperator(second);
			QuerySettings querySettings = parallelQuery.SpecifiedQuerySettings.Merge(queryOperator.SpecifiedQuerySettings).WithDefaults().WithPerExecutionSettings(new CancellationTokenSource(), new Shared<bool>(false));
			IEnumerator<TSource> enumerator = first.GetEnumerator();
			try
			{
				IEnumerator<TSource> enumerator2 = second.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator2.MoveNext() || !comparer.Equals(enumerator.Current, enumerator2.Current))
						{
							return false;
						}
					}
					if (enumerator2.MoveNext())
					{
						return false;
					}
				}
				catch (Exception ex)
				{
					ExceptionAggregator.ThrowOCEorAggregateException(ex, querySettings.CancellationState);
				}
				finally
				{
					ParallelEnumerable.DisposeEnumerator<TSource>(enumerator2, querySettings.CancellationState);
				}
			}
			finally
			{
				ParallelEnumerable.DisposeEnumerator<TSource>(enumerator, querySettings.CancellationState);
			}
			return true;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00009D68 File Offset: 0x00007F68
		private static void DisposeEnumerator<TSource>(IEnumerator<TSource> e, CancellationState cancelState)
		{
			try
			{
				e.Dispose();
			}
			catch (Exception ex)
			{
				ExceptionAggregator.ThrowOCEorAggregateException(ex, cancelState);
			}
		}

		/// <summary>This SequenceEqual overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <param name="comparer">This parameter is not used.</param>
		/// <typeparam name="TSource">This type parameter is not used.</typeparam>
		/// <exception cref="T:System.NotSupportedException">Thrown every time this method is called.</exception>
		// Token: 0x06000410 RID: 1040 RVA: 0x00008B3D File Offset: 0x00006D3D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static bool SequenceEqual<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Returns distinct elements from a parallel sequence by using the default equality comparer to compare values.</summary>
		/// <returns>A sequence that contains distinct elements from the source sequence.</returns>
		/// <param name="source">The sequence to remove duplicate elements from.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000411 RID: 1041 RVA: 0x00009D98 File Offset: 0x00007F98
		public static ParallelQuery<TSource> Distinct<TSource>(this ParallelQuery<TSource> source)
		{
			return source.Distinct(null);
		}

		/// <summary>Returns distinct elements from a parallel sequence by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</summary>
		/// <returns>A sequence that contains distinct elements from the source sequence.</returns>
		/// <param name="source">The sequence to remove duplicate elements from.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" />  to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000412 RID: 1042 RVA: 0x00009DA1 File Offset: 0x00007FA1
		public static ParallelQuery<TSource> Distinct<TSource>(this ParallelQuery<TSource> source, IEqualityComparer<TSource> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DistinctQueryOperator<TSource>(source, comparer);
		}

		/// <summary>Produces the set union of two parallel sequences by using the default equality comparer.</summary>
		/// <returns>A sequence that contains the elements from both input sequences, excluding duplicates.</returns>
		/// <param name="first">A sequence whose distinct elements form the first set for the union.</param>
		/// <param name="second">A sequence whose distinct elements form the second set for the union.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000413 RID: 1043 RVA: 0x00009DB8 File Offset: 0x00007FB8
		public static ParallelQuery<TSource> Union<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second)
		{
			return first.Union(second, null);
		}

		/// <summary>This Union overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <typeparam name="TSource">This type parameter is not used.</typeparam>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000414 RID: 1044 RVA: 0x00008B3D File Offset: 0x00006D3D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TSource> Union<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Produces the set union of two parallel sequences by using a specified IEqualityComparer{T}.</summary>
		/// <returns>A sequence that contains the elements from both input sequences, excluding duplicates.</returns>
		/// <param name="first">A sequence whose distinct elements form the first set for the union.</param>
		/// <param name="second">A sequence whose distinct elements form the second set for the union.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000415 RID: 1045 RVA: 0x00009DC2 File Offset: 0x00007FC2
		public static ParallelQuery<TSource> Union<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			return new UnionQueryOperator<TSource>(first, second, comparer);
		}

		/// <summary>This Union overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <param name="comparer">This parameter is not used.</param>
		/// <typeparam name="TSource">This type parameter is not used.</typeparam>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000416 RID: 1046 RVA: 0x00008B3D File Offset: 0x00006D3D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TSource> Union<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Produces the set intersection of two parallel sequences by using the default equality comparer to compare values.</summary>
		/// <returns>A sequence that contains the elements that form the set intersection of two sequences.</returns>
		/// <param name="first">A sequence whose distinct elements that also appear in <paramref name="second" /> will be returned.</param>
		/// <param name="second">A sequence whose distinct elements that also appear in the first sequence will be returned.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000417 RID: 1047 RVA: 0x00009DE8 File Offset: 0x00007FE8
		public static ParallelQuery<TSource> Intersect<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second)
		{
			return first.Intersect(second, null);
		}

		/// <summary>This Intersect overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <typeparam name="TSource">This type parameter is not used.</typeparam>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		// Token: 0x06000418 RID: 1048 RVA: 0x00008B3D File Offset: 0x00006D3D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TSource> Intersect<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Produces the set intersection of two parallel sequences by using the specified IEqualityComparer{T} to compare values.</summary>
		/// <returns>A sequence that contains the elements that form the set intersection of two sequences.</returns>
		/// <param name="first">A sequence whose distinct elements that also appear in <paramref name="second" /> will be returned.</param>
		/// <param name="second">A sequence whose distinct elements that also appear in the first sequence will be returned.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000419 RID: 1049 RVA: 0x00009DF2 File Offset: 0x00007FF2
		public static ParallelQuery<TSource> Intersect<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			return new IntersectQueryOperator<TSource>(first, second, comparer);
		}

		/// <summary>This Intersect overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <param name="comparer">This parameter is not used.</param>
		/// <typeparam name="TSource">This type parameter is not used.</typeparam>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		// Token: 0x0600041A RID: 1050 RVA: 0x00008B3D File Offset: 0x00006D3D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TSource> Intersect<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Produces the set difference of two parallel sequences by using the default equality comparer to compare values.</summary>
		/// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
		/// <param name="first">A sequence whose elements that are not also in <paramref name="second" /> will be returned.</param>
		/// <param name="second">A sequence whose elements that also occur in the first sequence will cause those elements to be removed from the returned sequence.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600041B RID: 1051 RVA: 0x00009E18 File Offset: 0x00008018
		public static ParallelQuery<TSource> Except<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second)
		{
			return first.Except(second, null);
		}

		/// <summary>This Except overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <typeparam name="TSource">This type parameter is not used.</typeparam>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		// Token: 0x0600041C RID: 1052 RVA: 0x00008B3D File Offset: 0x00006D3D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TSource> Except<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Produces the set difference of two parallel sequences by using the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</summary>
		/// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
		/// <param name="first">A sequence whose elements that are not also in <paramref name="second" /> will be returned.</param>
		/// <param name="second">A sequence whose elements that also occur in the first sequence will cause those elements to be removed from the returned sequence. </param>
		/// <param name="comparer">
		///   <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600041D RID: 1053 RVA: 0x00009E22 File Offset: 0x00008022
		public static ParallelQuery<TSource> Except<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			return new ExceptQueryOperator<TSource>(first, second, comparer);
		}

		/// <summary>This Except overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <param name="comparer">This parameter is not used.</param>
		/// <typeparam name="TSource">This type parameter is not used.</typeparam>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		// Token: 0x0600041E RID: 1054 RVA: 0x00008B3D File Offset: 0x00006D3D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TSource> Except<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Converts a <see cref="T:System.Linq.ParallelQuery`1" /> into an <see cref="T:System.Collections.Generic.IEnumerable`1" /> to force sequential evaluation of the query.</summary>
		/// <returns>The input sequence typed as <see cref="T:System.Collections.Generic.IEnumerable`1" />.</returns>
		/// <param name="source">The sequence to cast as <see cref="T:System.Collections.Generic.IEnumerable`1" />.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x0600041F RID: 1055 RVA: 0x00009E48 File Offset: 0x00008048
		public static IEnumerable<TSource> AsEnumerable<TSource>(this ParallelQuery<TSource> source)
		{
			return source.AsSequential<TSource>();
		}

		/// <summary>Creates an array from a <see cref="T:System.Linq.ParallelQuery`1" />.</summary>
		/// <returns>An array that contains the elements from the input sequence.</returns>
		/// <param name="source">A sequence to create an array from.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000420 RID: 1056 RVA: 0x00009E50 File Offset: 0x00008050
		public static TSource[] ToArray<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			QueryOperator<TSource> queryOperator = source as QueryOperator<TSource>;
			if (queryOperator != null)
			{
				return queryOperator.ExecuteAndGetResultsAsArray();
			}
			return source.ToList<TSource>().ToArray<TSource>();
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.List`1" /> from an <see cref="T:System.Linq.ParallelQuery`1" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.List`1" />  that contains elements from the input sequence.</returns>
		/// <param name="source">A sequence to create a <see cref="T:System.Collections.Generic.List`1" /> from.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000421 RID: 1057 RVA: 0x00009E88 File Offset: 0x00008088
		public static List<TSource> ToList<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			List<TSource> list = new List<TSource>();
			QueryOperator<TSource> queryOperator = source as QueryOperator<TSource>;
			IEnumerator<TSource> enumerator;
			if (queryOperator != null)
			{
				if (queryOperator.OrdinalIndexState == OrdinalIndexState.Indexable && queryOperator.OutputOrdered)
				{
					return new List<TSource>(source.ToArray<TSource>());
				}
				enumerator = queryOperator.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered));
			}
			else
			{
				enumerator = source.GetEnumerator();
			}
			using (enumerator)
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					list.Add(tsource);
				}
			}
			return list;
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from a <see cref="T:System.Linq.ParallelQuery`1" /> according to a specified key selector function.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains keys and values.</returns>
		/// <param name="source">A sequence to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">
		///   <paramref name="keySelector" /> produces a key that is a null reference (Nothing in Visual Basic). -or- <paramref name="keySelector" /> produces duplicate keys for two elements. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000422 RID: 1058 RVA: 0x00009F1C File Offset: 0x0000811C
		public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ToDictionary(keySelector, EqualityComparer<TKey>.Default);
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" />  from a <see cref="T:System.Linq.ParallelQuery`1" /> according to a specified key selector function and key comparer.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains keys and values.</returns>
		/// <param name="source">A sequence to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">
		///   <paramref name="keySelector" /> produces a key that is a null reference (Nothing in Visual Basic). -or- <paramref name="keySelector" /> produces duplicate keys for two elements. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000423 RID: 1059 RVA: 0x00009F2C File Offset: 0x0000812C
		public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			Dictionary<TKey, TSource> dictionary = new Dictionary<TKey, TSource>(comparer);
			QueryOperator<TSource> queryOperator = source as QueryOperator<TSource>;
			IEnumerator<TSource> enumerator = ((queryOperator == null) ? source.GetEnumerator() : queryOperator.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true));
			using (enumerator)
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					try
					{
						TKey tkey = keySelector(tsource);
						dictionary.Add(tkey, tsource);
					}
					catch (Exception ex)
					{
						throw new AggregateException(new Exception[] { ex });
					}
				}
			}
			return dictionary;
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from a <see cref="T:System.Linq.ParallelQuery`1" /> according to specified key selector and element selector functions.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains values of type <paramref name="TElement" /> selected from the input sequence</returns>
		/// <param name="source">A sequence to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="elementSelector">A transform function to produce a result element value from each element. </param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">
		///   <paramref name="keySelector" /> produces a key that is a null reference (Nothing in Visual Basic). -or- <paramref name="keySelector" /> produces duplicate keys for two elements. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000424 RID: 1060 RVA: 0x00009FE4 File Offset: 0x000081E4
		public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return source.ToDictionary(keySelector, elementSelector, EqualityComparer<TKey>.Default);
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from a <see cref="T:System.Linq.ParallelQuery`1" /> according to a specified key selector function, a comparer, and an element selector function.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains values of type <paramref name="TElement" /> selected from the input sequence</returns>
		/// <param name="source">A sequence to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">
		///   <paramref name="keySelector" /> produces a key that is a null reference (Nothing in Visual Basic). -or- <paramref name="keySelector" /> produces duplicate keys for two elements. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000425 RID: 1061 RVA: 0x00009FF4 File Offset: 0x000081F4
		public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (elementSelector == null)
			{
				throw new ArgumentNullException("elementSelector");
			}
			Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>(comparer);
			QueryOperator<TSource> queryOperator = source as QueryOperator<TSource>;
			IEnumerator<TSource> enumerator = ((queryOperator == null) ? source.GetEnumerator() : queryOperator.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true));
			using (enumerator)
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					try
					{
						dictionary.Add(keySelector(tsource), elementSelector(tsource));
					}
					catch (Exception ex)
					{
						throw new AggregateException(new Exception[] { ex });
					}
				}
			}
			return dictionary;
		}

		/// <summary>Creates an <see cref="T:System.Linq.ILookup`2" /> from a <see cref="T:System.Linq.ParallelQuery`1" /> according to a specified key selector function.</summary>
		/// <returns>A <see cref="T:System.Linq.ILookup`2" /> that contains keys and values.</returns>
		/// <param name="source">The sequence to create a <see cref="T:System.Linq.ILookup`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000426 RID: 1062 RVA: 0x0000A0BC File Offset: 0x000082BC
		public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ToLookup(keySelector, EqualityComparer<TKey>.Default);
		}

		/// <summary>Creates an <see cref="T:System.Linq.ILookup`2" /> from a <see cref="T:System.Linq.ParallelQuery`1" /> according to a specified key selector function and key comparer.</summary>
		/// <returns>A <see cref="T:System.Linq.ILookup`2" /> that contains keys and values.</returns>
		/// <param name="source">The sequence to create a <see cref="T:System.Linq.ILookup`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> or is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000427 RID: 1063 RVA: 0x0000A0CC File Offset: 0x000082CC
		public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			comparer = comparer ?? EqualityComparer<TKey>.Default;
			ParallelQuery<IGrouping<TKey, TSource>> parallelQuery = source.GroupBy(keySelector, comparer);
			Lookup<TKey, TSource> lookup = new Lookup<TKey, TSource>(comparer);
			QueryOperator<IGrouping<TKey, TSource>> queryOperator = parallelQuery as QueryOperator<IGrouping<TKey, TSource>>;
			IEnumerator<IGrouping<TKey, TSource>> enumerator = ((queryOperator == null) ? parallelQuery.GetEnumerator() : queryOperator.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered)));
			using (enumerator)
			{
				while (enumerator.MoveNext())
				{
					IGrouping<TKey, TSource> grouping = enumerator.Current;
					lookup.Add(grouping);
				}
			}
			return lookup;
		}

		/// <summary>Creates an <see cref="T:System.Linq.ILookup`2" /> from a <see cref="T:System.Linq.ParallelQuery`1" /> according to specified key selector and element selector functions.</summary>
		/// <returns>A <see cref="T:System.Linq.ILookup`2" /> that contains values of type <paramref name="TElement" /> selected from the input sequence.</returns>
		/// <param name="source">The sequence to create a <see cref="T:System.Linq.ILookup`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="elementSelector">A transform function to produce a result element value from each element. </param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000428 RID: 1064 RVA: 0x0000A168 File Offset: 0x00008368
		public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return source.ToLookup(keySelector, elementSelector, EqualityComparer<TKey>.Default);
		}

		/// <summary>Creates an <see cref="T:System.Linq.ILookup`2" /> from a <see cref="T:System.Linq.ParallelQuery`1" /> according to a specified key selector function, a comparer and an element selector function.</summary>
		/// <returns>A Lookup&lt;(Of &lt;(TKey, TElement&gt;)&gt;) that contains values of type TElement selected from the input sequence.</returns>
		/// <param name="source">The sequence to create a <see cref="T:System.Linq.ILookup`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="elementSelector">A transform function to produce a result element value from each element. </param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000429 RID: 1065 RVA: 0x0000A178 File Offset: 0x00008378
		public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (elementSelector == null)
			{
				throw new ArgumentNullException("elementSelector");
			}
			comparer = comparer ?? EqualityComparer<TKey>.Default;
			ParallelQuery<IGrouping<TKey, TElement>> parallelQuery = source.GroupBy(keySelector, elementSelector, comparer);
			Lookup<TKey, TElement> lookup = new Lookup<TKey, TElement>(comparer);
			QueryOperator<IGrouping<TKey, TElement>> queryOperator = parallelQuery as QueryOperator<IGrouping<TKey, TElement>>;
			IEnumerator<IGrouping<TKey, TElement>> enumerator = ((queryOperator == null) ? parallelQuery.GetEnumerator() : queryOperator.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered)));
			using (enumerator)
			{
				while (enumerator.MoveNext())
				{
					IGrouping<TKey, TElement> grouping = enumerator.Current;
					lookup.Add(grouping);
				}
			}
			return lookup;
		}

		/// <summary>Inverts the order of the elements in a parallel sequence.</summary>
		/// <returns>A sequence whose elements correspond to those of the input sequence in reverse order.</returns>
		/// <param name="source">A sequence of values to reverse.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600042A RID: 1066 RVA: 0x0000A224 File Offset: 0x00008424
		public static ParallelQuery<TSource> Reverse<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ReverseQueryOperator<TSource>(source);
		}

		/// <summary>Filters the elements of a ParallelQuery based on a specified type.</summary>
		/// <returns>A sequence that contains elements from the input sequence of type .</returns>
		/// <param name="source">The sequence whose elements to filter.</param>
		/// <typeparam name="TResult">The type to filter the elements of the sequence on.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600042B RID: 1067 RVA: 0x0000A23A File Offset: 0x0000843A
		public static ParallelQuery<TResult> OfType<TResult>(this ParallelQuery source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.OfType<TResult>();
		}

		/// <summary>Converts the elements of a ParallelQuery to the specified type.</summary>
		/// <returns>A sequence that contains each element of the source sequence converted to the specified type.</returns>
		/// <param name="source">The sequence that contains the elements to be converted.</param>
		/// <typeparam name="TResult">The type to convert the elements of <paramref name="source" /> to.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source sequence could not be converted to <paramref name="TResult" />.</exception>
		// Token: 0x0600042C RID: 1068 RVA: 0x0000A250 File Offset: 0x00008450
		public static ParallelQuery<TResult> Cast<TResult>(this ParallelQuery source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.Cast<TResult>();
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000A268 File Offset: 0x00008468
		private static TSource GetOneWithPossibleDefault<TSource>(QueryOperator<TSource> queryOp, bool throwIfTwo, bool defaultIfEmpty)
		{
			using (IEnumerator<TSource> enumerator = queryOp.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered)))
			{
				if (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					if (throwIfTwo && enumerator.MoveNext())
					{
						throw new InvalidOperationException("Sequence contains more than one matching element");
					}
					return tsource;
				}
			}
			if (defaultIfEmpty)
			{
				return default(TSource);
			}
			throw new InvalidOperationException("Sequence contains no elements");
		}

		/// <summary>Returns the first element of a parallel sequence.</summary>
		/// <returns>The first element in the specified sequence.</returns>
		/// <param name="source">The sequence to return the first element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x0600042E RID: 1070 RVA: 0x0000A2E0 File Offset: 0x000084E0
		public static TSource First<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			FirstQueryOperator<TSource> firstQueryOperator = new FirstQueryOperator<TSource>(source, null);
			QuerySettings querySettings = firstQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (firstQueryOperator.LimitsParallelism && querySettings.ExecutionMode != ParallelExecutionMode.ForceParallelism)
			{
				return ExceptionAggregator.WrapEnumerable<TSource>(CancellableEnumerable.Wrap<TSource>(firstQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).First<TSource>();
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(firstQueryOperator, false, false);
		}

		/// <summary>Returns the first element in a parallel sequence that satisfies a specified condition.</summary>
		/// <returns>The first element in the sequence that passes the test in the specified predicate function.</returns>
		/// <param name="source">The sequence to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">No element in <paramref name="source" /> satisfies the condition in <paramref name="predicate" />.</exception>
		// Token: 0x0600042F RID: 1071 RVA: 0x0000A384 File Offset: 0x00008584
		public static TSource First<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			FirstQueryOperator<TSource> firstQueryOperator = new FirstQueryOperator<TSource>(source, predicate);
			QuerySettings querySettings = firstQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (firstQueryOperator.LimitsParallelism && querySettings.ExecutionMode != ParallelExecutionMode.ForceParallelism)
			{
				return ExceptionAggregator.WrapEnumerable<TSource>(CancellableEnumerable.Wrap<TSource>(firstQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).First(ExceptionAggregator.WrapFunc<TSource, bool>(predicate, querySettings.CancellationState));
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(firstQueryOperator, false, false);
		}

		/// <summary>Returns the first element of a parallel sequence, or a default value if the sequence contains no elements.</summary>
		/// <returns>default(TSource) if <paramref name="source" /> is empty; otherwise, the first element in <paramref name="source" />.</returns>
		/// <param name="source">The sequence to return the first element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000430 RID: 1072 RVA: 0x0000A440 File Offset: 0x00008640
		public static TSource FirstOrDefault<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			FirstQueryOperator<TSource> firstQueryOperator = new FirstQueryOperator<TSource>(source, null);
			QuerySettings querySettings = firstQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (firstQueryOperator.LimitsParallelism && querySettings.ExecutionMode != ParallelExecutionMode.ForceParallelism)
			{
				return ExceptionAggregator.WrapEnumerable<TSource>(CancellableEnumerable.Wrap<TSource>(firstQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).FirstOrDefault<TSource>();
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(firstQueryOperator, false, true);
		}

		/// <summary>Returns the first element of the parallel sequence that satisfies a condition or a default value if no such element is found.</summary>
		/// <returns>default(TSource) if <paramref name="source" /> is empty or if no element passes the test specified by predicate; otherwise, the first element in <paramref name="source" /> that passes the test specified by predicate.</returns>
		/// <param name="source">The sequence to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000431 RID: 1073 RVA: 0x0000A4E4 File Offset: 0x000086E4
		public static TSource FirstOrDefault<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			FirstQueryOperator<TSource> firstQueryOperator = new FirstQueryOperator<TSource>(source, predicate);
			QuerySettings querySettings = firstQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (firstQueryOperator.LimitsParallelism && querySettings.ExecutionMode != ParallelExecutionMode.ForceParallelism)
			{
				return ExceptionAggregator.WrapEnumerable<TSource>(CancellableEnumerable.Wrap<TSource>(firstQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).FirstOrDefault(ExceptionAggregator.WrapFunc<TSource, bool>(predicate, querySettings.CancellationState));
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(firstQueryOperator, false, true);
		}

		/// <summary>Returns the last element of a parallel sequence.</summary>
		/// <returns>The value at the last position in the source sequence.</returns>
		/// <param name="source">The sequence to return the last element from.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000432 RID: 1074 RVA: 0x0000A5A0 File Offset: 0x000087A0
		public static TSource Last<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			LastQueryOperator<TSource> lastQueryOperator = new LastQueryOperator<TSource>(source, null);
			QuerySettings querySettings = lastQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (lastQueryOperator.LimitsParallelism && querySettings.ExecutionMode != ParallelExecutionMode.ForceParallelism)
			{
				return ExceptionAggregator.WrapEnumerable<TSource>(CancellableEnumerable.Wrap<TSource>(lastQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).Last<TSource>();
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(lastQueryOperator, false, false);
		}

		/// <summary>Returns the last element of a parallel sequence that satisfies a specified condition.</summary>
		/// <returns>The last element in the sequence that passes the test in the specified predicate function.</returns>
		/// <param name="source">The sequence to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">No element in <paramref name="source" /> satisfies the condition in <paramref name="predicate" />.</exception>
		// Token: 0x06000433 RID: 1075 RVA: 0x0000A644 File Offset: 0x00008844
		public static TSource Last<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			LastQueryOperator<TSource> lastQueryOperator = new LastQueryOperator<TSource>(source, predicate);
			QuerySettings querySettings = lastQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (lastQueryOperator.LimitsParallelism && querySettings.ExecutionMode != ParallelExecutionMode.ForceParallelism)
			{
				return ExceptionAggregator.WrapEnumerable<TSource>(CancellableEnumerable.Wrap<TSource>(lastQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).Last(ExceptionAggregator.WrapFunc<TSource, bool>(predicate, querySettings.CancellationState));
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(lastQueryOperator, false, false);
		}

		/// <summary>Returns the last element of a parallel sequence, or a default value if the sequence contains no elements.</summary>
		/// <returns>default() if the source sequence is empty; otherwise, the last element in the sequence.</returns>
		/// <param name="source">The sequence to return an element from.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000434 RID: 1076 RVA: 0x0000A700 File Offset: 0x00008900
		public static TSource LastOrDefault<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			LastQueryOperator<TSource> lastQueryOperator = new LastQueryOperator<TSource>(source, null);
			QuerySettings querySettings = lastQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (lastQueryOperator.LimitsParallelism && querySettings.ExecutionMode != ParallelExecutionMode.ForceParallelism)
			{
				return ExceptionAggregator.WrapEnumerable<TSource>(CancellableEnumerable.Wrap<TSource>(lastQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).LastOrDefault<TSource>();
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(lastQueryOperator, false, true);
		}

		/// <summary>Returns the last element of a parallel sequence that satisfies a condition, or a default value if no such element is found.</summary>
		/// <returns>default() if the sequence is empty or if no elements pass the test in the predicate function; otherwise, the last element that passes the test in the predicate function.</returns>
		/// <param name="source">The sequence to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000435 RID: 1077 RVA: 0x0000A7A4 File Offset: 0x000089A4
		public static TSource LastOrDefault<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			LastQueryOperator<TSource> lastQueryOperator = new LastQueryOperator<TSource>(source, predicate);
			QuerySettings querySettings = lastQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (lastQueryOperator.LimitsParallelism && querySettings.ExecutionMode != ParallelExecutionMode.ForceParallelism)
			{
				return ExceptionAggregator.WrapEnumerable<TSource>(CancellableEnumerable.Wrap<TSource>(lastQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).LastOrDefault(ExceptionAggregator.WrapFunc<TSource, bool>(predicate, querySettings.CancellationState));
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(lastQueryOperator, false, true);
		}

		/// <summary>Returns the only element of a parallel sequence, and throws an exception if there is not exactly one element in the sequence.</summary>
		/// <returns>The single element of the input sequence.</returns>
		/// <param name="source">The sequence to return the single element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">The input sequence contains more than one element. -or- The input sequence is empty.</exception>
		// Token: 0x06000436 RID: 1078 RVA: 0x0000A860 File Offset: 0x00008A60
		public static TSource Single<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(new SingleQueryOperator<TSource>(source, null), true, false);
		}

		/// <summary>Returns the only element of a parallel sequence that satisfies a specified condition, and throws an exception if more than one such element exists.</summary>
		/// <returns>The single element of the input sequence that satisfies a condition.</returns>
		/// <param name="source">The sequence to return the single element of.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate" />. -or- More than one element satisfies the condition in <paramref name="predicate" />.</exception>
		// Token: 0x06000437 RID: 1079 RVA: 0x0000A87E File Offset: 0x00008A7E
		public static TSource Single<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(new SingleQueryOperator<TSource>(source, predicate), true, false);
		}

		/// <summary>Returns the only element of a parallel sequence, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.</summary>
		/// <returns>The single element of the input sequence, or default() if the sequence contains no elements.</returns>
		/// <param name="source">The sequence to return the single element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000438 RID: 1080 RVA: 0x0000A8AA File Offset: 0x00008AAA
		public static TSource SingleOrDefault<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(new SingleQueryOperator<TSource>(source, null), true, true);
		}

		/// <summary>Returns the only element of a parallel sequence that satisfies a specified condition or a default value if no such element exists; this method throws an exception if more than one element satisfies the condition.</summary>
		/// <returns>The single element of the input sequence that satisfies the condition, or default() if no such element is found.</returns>
		/// <param name="source">The sequence to return the single element of.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> is empty or multiple elements are returned.</exception>
		// Token: 0x06000439 RID: 1081 RVA: 0x0000A8C8 File Offset: 0x00008AC8
		public static TSource SingleOrDefault<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(new SingleQueryOperator<TSource>(source, predicate), true, true);
		}

		/// <summary>Returns the elements of the specified parallel sequence or the type parameter's default value in a singleton collection if the sequence is empty.</summary>
		/// <returns>A sequence that contains default(TSource) if <paramref name="source" /> is empty; otherwise, <paramref name="source" />.</returns>
		/// <param name="source">The sequence to return a default value for if it is empty.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600043A RID: 1082 RVA: 0x0000A8F4 File Offset: 0x00008AF4
		public static ParallelQuery<TSource> DefaultIfEmpty<TSource>(this ParallelQuery<TSource> source)
		{
			return source.DefaultIfEmpty(default(TSource));
		}

		/// <summary>Returns the elements of the specified parallel sequence or the specified value in a singleton collection if the sequence is empty.</summary>
		/// <returns>A sequence that contains defaultValue if <paramref name="source" /> is empty; otherwise, <paramref name="source" />.</returns>
		/// <param name="source">The sequence to return the specified value for if it is empty.</param>
		/// <param name="defaultValue">The value to return if the sequence is empty.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600043B RID: 1083 RVA: 0x0000A910 File Offset: 0x00008B10
		public static ParallelQuery<TSource> DefaultIfEmpty<TSource>(this ParallelQuery<TSource> source, TSource defaultValue)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DefaultIfEmptyQueryOperator<TSource>(source, defaultValue);
		}

		/// <summary>Returns the element at a specified index in a parallel sequence.</summary>
		/// <returns>The element at the specified position in the source sequence.</returns>
		/// <param name="source">A sequence to return an element from.</param>
		/// <param name="index">The zero-based index of the element to retrieve.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than or equal to the number of elements in <paramref name="source" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600043C RID: 1084 RVA: 0x0000A928 File Offset: 0x00008B28
		public static TSource ElementAt<TSource>(this ParallelQuery<TSource> source, int index)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			TSource tsource;
			if (new ElementAtQueryOperator<TSource>(source, index).Aggregate(out tsource, false))
			{
				return tsource;
			}
			throw new ArgumentOutOfRangeException("index");
		}

		/// <summary>Returns the element at a specified index in a parallel sequence or a default value if the index is out of range.</summary>
		/// <returns>default(TSource) if the index is outside the bounds of the source sequence; otherwise, the element at the specified position in the source sequence.</returns>
		/// <param name="source">A sequence to return an element from.</param>
		/// <param name="index">The zero-based index of the element to retrieve.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600043D RID: 1085 RVA: 0x0000A970 File Offset: 0x00008B70
		public static TSource ElementAtOrDefault<TSource>(this ParallelQuery<TSource> source, int index)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			TSource tsource;
			if (index >= 0 && new ElementAtQueryOperator<TSource>(source, index).Aggregate(out tsource, true))
			{
				return tsource;
			}
			return default(TSource);
		}

		// Token: 0x04000326 RID: 806
		private const string RIGHT_SOURCE_NOT_PARALLEL_STR = "The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.";
	}
}
