using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	/// <summary>Provides a set of static (Shared in Visual Basic) methods for querying objects that implement <see cref="T:System.Collections.Generic.IEnumerable`1" />.</summary>
	// Token: 0x020000B0 RID: 176
	public static class Enumerable
	{
		/// <summary>Applies an accumulator function over a sequence.</summary>
		/// <returns>The final accumulator value.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to aggregate over.</param>
		/// <param name="func">An accumulator function to be invoked on each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="func" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000596 RID: 1430 RVA: 0x0000FEEC File Offset: 0x0000E0EC
		public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (func == null)
			{
				throw Error.ArgumentNull("func");
			}
			TSource tsource3;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				TSource tsource = enumerator.Current;
				while (enumerator.MoveNext())
				{
					TSource tsource2 = enumerator.Current;
					tsource = func(tsource, tsource2);
				}
				tsource3 = tsource;
			}
			return tsource3;
		}

		/// <summary>Applies an accumulator function over a sequence. The specified seed value is used as the initial accumulator value.</summary>
		/// <returns>The final accumulator value.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to aggregate over.</param>
		/// <param name="seed">The initial accumulator value.</param>
		/// <param name="func">An accumulator function to be invoked on each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="func" /> is null.</exception>
		// Token: 0x06000597 RID: 1431 RVA: 0x0000FF68 File Offset: 0x0000E168
		public static TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (func == null)
			{
				throw Error.ArgumentNull("func");
			}
			TAccumulate taccumulate = seed;
			foreach (TSource tsource in source)
			{
				taccumulate = func(taccumulate, tsource);
			}
			return taccumulate;
		}

		/// <summary>Applies an accumulator function over a sequence. The specified seed value is used as the initial accumulator value, and the specified function is used to select the result value.</summary>
		/// <returns>The transformed final accumulator value.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to aggregate over.</param>
		/// <param name="seed">The initial accumulator value.</param>
		/// <param name="func">An accumulator function to be invoked on each element.</param>
		/// <param name="resultSelector">A function to transform the final accumulator value into the result value.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
		/// <typeparam name="TResult">The type of the resulting value.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="func" /> or <paramref name="resultSelector" /> is null.</exception>
		// Token: 0x06000598 RID: 1432 RVA: 0x0000FFD4 File Offset: 0x0000E1D4
		public static TResult Aggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (func == null)
			{
				throw Error.ArgumentNull("func");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			TAccumulate taccumulate = seed;
			foreach (TSource tsource in source)
			{
				taccumulate = func(taccumulate, tsource);
			}
			return resultSelector(taccumulate);
		}

		/// <summary>Determines whether a sequence contains any elements.</summary>
		/// <returns>true if the source sequence contains any elements; otherwise, false.</returns>
		/// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to check for emptiness.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x06000599 RID: 1433 RVA: 0x00010054 File Offset: 0x0000E254
		public static bool Any<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			bool flag;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				flag = enumerator.MoveNext();
			}
			return flag;
		}

		/// <summary>Determines whether any element of a sequence satisfies a condition.</summary>
		/// <returns>true if any elements in the source sequence pass the test in the specified predicate; otherwise, false.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to apply the predicate to.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x0600059A RID: 1434 RVA: 0x0001009C File Offset: 0x0000E29C
		public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			foreach (TSource tsource in source)
			{
				if (predicate(tsource))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether all elements of a sequence satisfy a condition.</summary>
		/// <returns>true if every element of the source sequence passes the test in the specified predicate, or if the sequence is empty; otherwise, false.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements to apply the predicate to.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x0600059B RID: 1435 RVA: 0x0001010C File Offset: 0x0000E30C
		public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			foreach (TSource tsource in source)
			{
				if (!predicate(tsource))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001017C File Offset: 0x0000E37C
		public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> source, TSource element)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			Enumerable.AppendPrependIterator<TSource> appendPrependIterator;
			if ((appendPrependIterator = source as Enumerable.AppendPrependIterator<TSource>) == null)
			{
				return new Enumerable.AppendPrepend1Iterator<TSource>(source, element, true);
			}
			return appendPrependIterator.Append(element);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000101B4 File Offset: 0x0000E3B4
		public static IEnumerable<TSource> Prepend<TSource>(this IEnumerable<TSource> source, TSource element)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			Enumerable.AppendPrependIterator<TSource> appendPrependIterator;
			if ((appendPrependIterator = source as Enumerable.AppendPrependIterator<TSource>) == null)
			{
				return new Enumerable.AppendPrepend1Iterator<TSource>(source, element, false);
			}
			return appendPrependIterator.Prepend(element);
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Int32" /> values.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Int32" /> values to calculate the average of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x0600059E RID: 1438 RVA: 0x000101EC File Offset: 0x0000E3EC
		public static double Average(this IEnumerable<int> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double num4;
			using (IEnumerator<int> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				long num = (long)enumerator.Current;
				long num2 = 1L;
				checked
				{
					while (enumerator.MoveNext())
					{
						int num3 = enumerator.Current;
						num += unchecked((long)num3);
						num2 += 1L;
					}
					num4 = (double)num / (double)num2;
				}
			}
			return num4;
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Int32" /> values.</summary>
		/// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Int32" /> values to calculate the average of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum of the elements in the sequence is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x0600059F RID: 1439 RVA: 0x00010264 File Offset: 0x0000E464
		public static double? Average(this IEnumerable<int?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			using (IEnumerator<int?> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int? num = enumerator.Current;
					if (num != null)
					{
						long num2 = (long)num.GetValueOrDefault();
						long num3 = 1L;
						checked
						{
							while (enumerator.MoveNext())
							{
								num = enumerator.Current;
								if (num != null)
								{
									num2 += unchecked((long)num.GetValueOrDefault());
									num3 += 1L;
								}
							}
							return new double?((double)num2 / (double)num3);
						}
					}
				}
			}
			return null;
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Int64" /> values.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Int64" /> values to calculate the average of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060005A0 RID: 1440 RVA: 0x00010310 File Offset: 0x0000E510
		public static double Average(this IEnumerable<long> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			checked
			{
				double num4;
				using (IEnumerator<long> enumerator = source.GetEnumerator())
				{
					if (!enumerator.MoveNext())
					{
						throw Error.NoElements();
					}
					long num = enumerator.Current;
					long num2 = 1L;
					while (enumerator.MoveNext())
					{
						long num3 = enumerator.Current;
						num += num3;
						num2 += 1L;
					}
					num4 = (double)num / (double)num2;
				}
				return num4;
			}
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Int64" /> values.</summary>
		/// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Int64" /> values to calculate the average of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum of the elements in the sequence is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060005A1 RID: 1441 RVA: 0x00010388 File Offset: 0x0000E588
		public static double? Average(this IEnumerable<long?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			checked
			{
				using (IEnumerator<long?> enumerator = source.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						long? num = enumerator.Current;
						if (num != null)
						{
							long num2 = num.GetValueOrDefault();
							long num3 = 1L;
							while (enumerator.MoveNext())
							{
								num = enumerator.Current;
								if (num != null)
								{
									num2 += num.GetValueOrDefault();
									num3 += 1L;
								}
							}
							return new double?((double)num2 / (double)num3);
						}
					}
				}
				return null;
			}
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Single" /> values.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Single" /> values to calculate the average of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060005A2 RID: 1442 RVA: 0x00010430 File Offset: 0x0000E630
		public static float Average(this IEnumerable<float> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			float num4;
			using (IEnumerator<float> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				double num = (double)enumerator.Current;
				long num2 = 1L;
				while (enumerator.MoveNext())
				{
					float num3 = enumerator.Current;
					num += (double)num3;
					num2 += 1L;
				}
				num4 = (float)(num / (double)num2);
			}
			return num4;
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Single" /> values.</summary>
		/// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Single" /> values to calculate the average of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060005A3 RID: 1443 RVA: 0x000104A8 File Offset: 0x0000E6A8
		public static float? Average(this IEnumerable<float?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			using (IEnumerator<float?> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					float? num = enumerator.Current;
					if (num != null)
					{
						double num2 = (double)num.GetValueOrDefault();
						long num3 = 1L;
						while (enumerator.MoveNext())
						{
							num = enumerator.Current;
							if (num != null)
							{
								num2 += (double)num.GetValueOrDefault();
								checked
								{
									num3 += 1L;
								}
							}
						}
						return new float?((float)(num2 / (double)num3));
					}
				}
			}
			return null;
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Double" /> values.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Double" /> values to calculate the average of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060005A4 RID: 1444 RVA: 0x00010554 File Offset: 0x0000E754
		public static double Average(this IEnumerable<double> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double num4;
			using (IEnumerator<double> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				double num = enumerator.Current;
				long num2 = 1L;
				while (enumerator.MoveNext())
				{
					double num3 = enumerator.Current;
					num += num3;
					num2 += 1L;
				}
				num4 = num / (double)num2;
			}
			return num4;
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Double" /> values.</summary>
		/// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Double" /> values to calculate the average of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060005A5 RID: 1445 RVA: 0x000105C8 File Offset: 0x0000E7C8
		public static double? Average(this IEnumerable<double?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			using (IEnumerator<double?> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					double? num = enumerator.Current;
					if (num != null)
					{
						double num2 = num.GetValueOrDefault();
						long num3 = 1L;
						while (enumerator.MoveNext())
						{
							num = enumerator.Current;
							if (num != null)
							{
								num2 += num.GetValueOrDefault();
								checked
								{
									num3 += 1L;
								}
							}
						}
						return new double?(num2 / (double)num3);
					}
				}
			}
			return null;
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Decimal" /> values to calculate the average of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060005A6 RID: 1446 RVA: 0x00010670 File Offset: 0x0000E870
		public static decimal Average(this IEnumerable<decimal> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			decimal num4;
			using (IEnumerator<decimal> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				decimal num = enumerator.Current;
				long num2 = 1L;
				while (enumerator.MoveNext())
				{
					decimal num3 = enumerator.Current;
					num += num3;
					num2 += 1L;
				}
				num4 = num / num2;
			}
			return num4;
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Decimal" /> values to calculate the average of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum of the elements in the sequence is larger than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x060005A7 RID: 1447 RVA: 0x000106F0 File Offset: 0x0000E8F0
		public static decimal? Average(this IEnumerable<decimal?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			using (IEnumerator<decimal?> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					decimal? num = enumerator.Current;
					if (num != null)
					{
						decimal num2 = num.GetValueOrDefault();
						long num3 = 1L;
						while (enumerator.MoveNext())
						{
							num = enumerator.Current;
							if (num != null)
							{
								num2 += num.GetValueOrDefault();
								num3 += 1L;
							}
						}
						return new decimal?(num2 / num3);
					}
				}
			}
			return null;
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Int32" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values to calculate the average of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException">The sum of the elements in the sequence is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060005A8 RID: 1448 RVA: 0x000107A4 File Offset: 0x0000E9A4
		public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			double num3;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				long num = (long)selector(enumerator.Current);
				long num2 = 1L;
				checked
				{
					while (enumerator.MoveNext())
					{
						TSource tsource = enumerator.Current;
						num += unchecked((long)selector(tsource));
						num2 += 1L;
					}
					num3 = (double)num / (double)num2;
				}
			}
			return num3;
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Int32" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
		/// <param name="source">A sequence of values to calculate the average of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum of the elements in the sequence is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060005A9 RID: 1449 RVA: 0x00010838 File Offset: 0x0000EA38
		public static double? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					int? num = selector(tsource);
					if (num != null)
					{
						long num2 = (long)num.GetValueOrDefault();
						long num3 = 1L;
						checked
						{
							while (enumerator.MoveNext())
							{
								TSource tsource2 = enumerator.Current;
								num = selector(tsource2);
								if (num != null)
								{
									num2 += unchecked((long)num.GetValueOrDefault());
									num3 += 1L;
								}
							}
							return new double?((double)num2 / (double)num3);
						}
					}
				}
			}
			return null;
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Int64" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values to calculate the average of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException">The sum of the elements in the sequence is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060005AA RID: 1450 RVA: 0x000108FC File Offset: 0x0000EAFC
		public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			checked
			{
				double num3;
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					if (!enumerator.MoveNext())
					{
						throw Error.NoElements();
					}
					long num = selector(enumerator.Current);
					long num2 = 1L;
					while (enumerator.MoveNext())
					{
						TSource tsource = enumerator.Current;
						num += selector(tsource);
						num2 += 1L;
					}
					num3 = (double)num / (double)num2;
				}
				return num3;
			}
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Int64" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
		/// <param name="source">A sequence of values to calculate the average of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		// Token: 0x060005AB RID: 1451 RVA: 0x0001098C File Offset: 0x0000EB8C
		public static double? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			checked
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TSource tsource = enumerator.Current;
						long? num = selector(tsource);
						if (num != null)
						{
							long num2 = num.GetValueOrDefault();
							long num3 = 1L;
							while (enumerator.MoveNext())
							{
								TSource tsource2 = enumerator.Current;
								num = selector(tsource2);
								if (num != null)
								{
									num2 += num.GetValueOrDefault();
									num3 += 1L;
								}
							}
							return new double?((double)num2 / (double)num3);
						}
					}
				}
				return null;
			}
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Single" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values to calculate the average of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060005AC RID: 1452 RVA: 0x00010A50 File Offset: 0x0000EC50
		public static float Average<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			float num3;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				double num = (double)selector(enumerator.Current);
				long num2 = 1L;
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					num += (double)selector(tsource);
					num2 += 1L;
				}
				num3 = (float)(num / (double)num2);
			}
			return num3;
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Single" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
		/// <param name="source">A sequence of values to calculate the average of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x060005AD RID: 1453 RVA: 0x00010AE4 File Offset: 0x0000ECE4
		public static float? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					float? num = selector(tsource);
					if (num != null)
					{
						double num2 = (double)num.GetValueOrDefault();
						long num3 = 1L;
						while (enumerator.MoveNext())
						{
							TSource tsource2 = enumerator.Current;
							num = selector(tsource2);
							if (num != null)
							{
								num2 += (double)num.GetValueOrDefault();
								checked
								{
									num3 += 1L;
								}
							}
						}
						return new float?((float)(num2 / (double)num3));
					}
				}
			}
			return null;
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Double" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values to calculate the average of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060005AE RID: 1454 RVA: 0x00010BA8 File Offset: 0x0000EDA8
		public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			double num3;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				double num = selector(enumerator.Current);
				long num2 = 1L;
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					num += selector(tsource);
					num2 += 1L;
				}
				num3 = num / (double)num2;
			}
			return num3;
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Double" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
		/// <param name="source">A sequence of values to calculate the average of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x060005AF RID: 1455 RVA: 0x00010C38 File Offset: 0x0000EE38
		public static double? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					double? num = selector(tsource);
					if (num != null)
					{
						double num2 = num.GetValueOrDefault();
						long num3 = 1L;
						while (enumerator.MoveNext())
						{
							TSource tsource2 = enumerator.Current;
							num = selector(tsource2);
							if (num != null)
							{
								num2 += num.GetValueOrDefault();
								checked
								{
									num3 += 1L;
								}
							}
						}
						return new double?(num2 / (double)num3);
					}
				}
			}
			return null;
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Decimal" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values.</returns>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException">The sum of the elements in the sequence is larger than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x060005B0 RID: 1456 RVA: 0x00010CF8 File Offset: 0x0000EEF8
		public static decimal Average<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			decimal num3;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				decimal num = selector(enumerator.Current);
				long num2 = 1L;
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					num += selector(tsource);
					num2 += 1L;
				}
				num3 = num / num2;
			}
			return num3;
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Decimal" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
		/// <param name="source">A sequence of values to calculate the average of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum of the elements in the sequence is larger than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x060005B1 RID: 1457 RVA: 0x00010D94 File Offset: 0x0000EF94
		public static decimal? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					decimal? num = selector(tsource);
					if (num != null)
					{
						decimal num2 = num.GetValueOrDefault();
						long num3 = 1L;
						while (enumerator.MoveNext())
						{
							TSource tsource2 = enumerator.Current;
							num = selector(tsource2);
							if (num != null)
							{
								num2 += num.GetValueOrDefault();
								num3 += 1L;
							}
						}
						return new decimal?(num2 / num3);
					}
				}
			}
			return null;
		}

		/// <summary>Filters the elements of an <see cref="T:System.Collections.IEnumerable" /> based on a specified type.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains elements from the input sequence of type <paramref name="TResult" />.</returns>
		/// <param name="source">The <see cref="T:System.Collections.IEnumerable" /> whose elements to filter.</param>
		/// <typeparam name="TResult">The type to filter the elements of the sequence on.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060005B2 RID: 1458 RVA: 0x00010E60 File Offset: 0x0000F060
		public static IEnumerable<TResult> OfType<TResult>(this IEnumerable source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return Enumerable.OfTypeIterator<TResult>(source);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00010E76 File Offset: 0x0000F076
		private static IEnumerable<TResult> OfTypeIterator<TResult>(IEnumerable source)
		{
			foreach (object obj in source)
			{
				if (obj is TResult)
				{
					yield return (TResult)((object)obj);
				}
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		/// <summary>Casts the elements of an <see cref="T:System.Collections.IEnumerable" /> to the specified type.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains each element of the source sequence cast to the specified type.</returns>
		/// <param name="source">The <see cref="T:System.Collections.IEnumerable" /> that contains the elements to be cast to type <paramref name="TResult" />.</param>
		/// <typeparam name="TResult">The type to cast the elements of <paramref name="source" /> to.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidCastException">An element in the sequence cannot be cast to type <paramref name="TResult" />.</exception>
		// Token: 0x060005B4 RID: 1460 RVA: 0x00010E88 File Offset: 0x0000F088
		public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source)
		{
			IEnumerable<TResult> enumerable = source as IEnumerable<TResult>;
			if (enumerable != null)
			{
				return enumerable;
			}
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return Enumerable.CastIterator<TResult>(source);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00010EB5 File Offset: 0x0000F0B5
		private static IEnumerable<TResult> CastIterator<TResult>(IEnumerable source)
		{
			foreach (object obj in source)
			{
				yield return (TResult)((object)obj);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		/// <summary>Concatenates two sequences.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the concatenated elements of the two input sequences.</returns>
		/// <param name="first">The first sequence to concatenate.</param>
		/// <param name="second">The sequence to concatenate to the first sequence.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is null.</exception>
		// Token: 0x060005B6 RID: 1462 RVA: 0x00010EC8 File Offset: 0x0000F0C8
		public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			Enumerable.ConcatIterator<TSource> concatIterator;
			if ((concatIterator = first as Enumerable.ConcatIterator<TSource>) == null)
			{
				return new Enumerable.Concat2Iterator<TSource>(first, second);
			}
			return concatIterator.Concat(second);
		}

		/// <summary>Determines whether a sequence contains a specified element by using the default equality comparer.</summary>
		/// <returns>true if the source sequence contains an element that has the specified value; otherwise, false.</returns>
		/// <param name="source">A sequence in which to locate a value.</param>
		/// <param name="value">The value to locate in the sequence.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060005B7 RID: 1463 RVA: 0x00010F0C File Offset: 0x0000F10C
		public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value)
		{
			ICollection<TSource> collection;
			if ((collection = source as ICollection<TSource>) == null)
			{
				return source.Contains(value, null);
			}
			return collection.Contains(value);
		}

		/// <summary>Determines whether a sequence contains a specified element by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <returns>true if the source sequence contains an element that has the specified value; otherwise, false.</returns>
		/// <param name="source">A sequence in which to locate a value.</param>
		/// <param name="value">The value to locate in the sequence.</param>
		/// <param name="comparer">An equality comparer to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060005B8 RID: 1464 RVA: 0x00010F34 File Offset: 0x0000F134
		public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TSource>.Default;
			}
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			foreach (TSource tsource in source)
			{
				if (comparer.Equals(tsource, value))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Returns the number of elements in a sequence.</summary>
		/// <returns>The number of elements in the input sequence.</returns>
		/// <param name="source">A sequence that contains elements to be counted.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The number of elements in <paramref name="source" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060005B9 RID: 1465 RVA: 0x00010FA0 File Offset: 0x0000F1A0
		public static int Count<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			ICollection<TSource> collection;
			if ((collection = source as ICollection<TSource>) != null)
			{
				return collection.Count;
			}
			IIListProvider<TSource> iilistProvider;
			if ((iilistProvider = source as IIListProvider<TSource>) != null)
			{
				return iilistProvider.GetCount(false);
			}
			ICollection collection2;
			if ((collection2 = source as ICollection) != null)
			{
				return collection2.Count;
			}
			int num = 0;
			checked
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						num++;
					}
				}
				return num;
			}
		}

		/// <summary>Returns a number that represents how many elements in the specified sequence satisfy a condition.</summary>
		/// <returns>A number that represents how many elements in the sequence satisfy the condition in the predicate function.</returns>
		/// <param name="source">A sequence that contains elements to be tested and counted.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The number of elements in <paramref name="source" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060005BA RID: 1466 RVA: 0x00011028 File Offset: 0x0000F228
		public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			int num = 0;
			checked
			{
				foreach (TSource tsource in source)
				{
					if (predicate(tsource))
					{
						num++;
					}
				}
				return num;
			}
		}

		/// <summary>Returns an <see cref="T:System.Int64" /> that represents the total number of elements in a sequence.</summary>
		/// <returns>The number of elements in the source sequence.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements to be counted.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The number of elements exceeds <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060005BB RID: 1467 RVA: 0x00011098 File Offset: 0x0000F298
		public static long LongCount<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long num = 0L;
			checked
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						num += 1L;
					}
				}
				return num;
			}
		}

		/// <summary>Returns an <see cref="T:System.Int64" /> that represents how many elements in a sequence satisfy a condition.</summary>
		/// <returns>A number that represents how many elements in the sequence satisfy the condition in the predicate function.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements to be counted.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The number of matching elements exceeds <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060005BC RID: 1468 RVA: 0x000110EC File Offset: 0x0000F2EC
		public static long LongCount<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			long num = 0L;
			checked
			{
				foreach (TSource tsource in source)
				{
					if (predicate(tsource))
					{
						num += 1L;
					}
				}
				return num;
			}
		}

		/// <summary>Returns the elements of the specified sequence or the type parameter's default value in a singleton collection if the sequence is empty.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> object that contains the default value for the <paramref name="TSource" /> type if <paramref name="source" /> is empty; otherwise, <paramref name="source" />.</returns>
		/// <param name="source">The sequence to return a default value for if it is empty.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060005BD RID: 1469 RVA: 0x0001115C File Offset: 0x0000F35C
		public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source)
		{
			return source.DefaultIfEmpty(default(TSource));
		}

		/// <summary>Returns the elements of the specified sequence or the specified value in a singleton collection if the sequence is empty.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <paramref name="defaultValue" /> if <paramref name="source" /> is empty; otherwise, <paramref name="source" />.</returns>
		/// <param name="source">The sequence to return the specified value for if it is empty.</param>
		/// <param name="defaultValue">The value to return if the sequence is empty.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		// Token: 0x060005BE RID: 1470 RVA: 0x00011178 File Offset: 0x0000F378
		public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return new Enumerable.DefaultIfEmptyIterator<TSource>(source, defaultValue);
		}

		/// <summary>Returns distinct elements from a sequence by using the default equality comparer to compare values.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains distinct elements from the source sequence.</returns>
		/// <param name="source">The sequence to remove duplicate elements from.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060005BF RID: 1471 RVA: 0x0001118F File Offset: 0x0000F38F
		public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source)
		{
			return source.Distinct(null);
		}

		/// <summary>Returns distinct elements from a sequence by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains distinct elements from the source sequence.</returns>
		/// <param name="source">The sequence to remove duplicate elements from.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060005C0 RID: 1472 RVA: 0x00011198 File Offset: 0x0000F398
		public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return new Enumerable.DistinctIterator<TSource>(source, comparer);
		}

		/// <summary>Returns the element at a specified index in a sequence.</summary>
		/// <returns>The element at the specified position in the source sequence.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return an element from.</param>
		/// <param name="index">The zero-based index of the element to retrieve.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than or equal to the number of elements in <paramref name="source" />.</exception>
		// Token: 0x060005C1 RID: 1473 RVA: 0x000111B0 File Offset: 0x0000F3B0
		public static TSource ElementAt<TSource>(this IEnumerable<TSource> source, int index)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IPartition<TSource> partition;
			if ((partition = source as IPartition<TSource>) != null)
			{
				bool flag;
				TSource tsource = partition.TryGetElementAt(index, out flag);
				if (flag)
				{
					return tsource;
				}
			}
			else
			{
				IList<TSource> list;
				if ((list = source as IList<TSource>) != null)
				{
					return list[index];
				}
				if (index >= 0)
				{
					using (IEnumerator<TSource> enumerator = source.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (index == 0)
							{
								return enumerator.Current;
							}
							index--;
						}
					}
				}
			}
			throw Error.ArgumentOutOfRange("index");
		}

		/// <summary>Returns the element at a specified index in a sequence or a default value if the index is out of range.</summary>
		/// <returns>default(<paramref name="TSource" />) if the index is outside the bounds of the source sequence; otherwise, the element at the specified position in the source sequence.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return an element from.</param>
		/// <param name="index">The zero-based index of the element to retrieve.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060005C2 RID: 1474 RVA: 0x0001124C File Offset: 0x0000F44C
		public static TSource ElementAtOrDefault<TSource>(this IEnumerable<TSource> source, int index)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IPartition<TSource> partition;
			if ((partition = source as IPartition<TSource>) != null)
			{
				bool flag;
				return partition.TryGetElementAt(index, out flag);
			}
			if (index >= 0)
			{
				IList<TSource> list;
				if ((list = source as IList<TSource>) != null)
				{
					if (index < list.Count)
					{
						return list[index];
					}
				}
				else
				{
					using (IEnumerator<TSource> enumerator = source.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (index == 0)
							{
								return enumerator.Current;
							}
							index--;
						}
					}
				}
			}
			return default(TSource);
		}

		/// <summary>Returns the input typed as <see cref="T:System.Collections.Generic.IEnumerable`1" />.</summary>
		/// <returns>The input sequence typed as <see cref="T:System.Collections.Generic.IEnumerable`1" />.</returns>
		/// <param name="source">The sequence to type as <see cref="T:System.Collections.Generic.IEnumerable`1" />.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		// Token: 0x060005C3 RID: 1475 RVA: 0x000021A0 File Offset: 0x000003A0
		public static IEnumerable<TSource> AsEnumerable<TSource>(this IEnumerable<TSource> source)
		{
			return source;
		}

		/// <summary>Returns an empty <see cref="T:System.Collections.Generic.IEnumerable`1" /> that has the specified type argument.</summary>
		/// <returns>An empty <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose type argument is <paramref name="TResult" />.</returns>
		/// <typeparam name="TResult">The type to assign to the type parameter of the returned generic <see cref="T:System.Collections.Generic.IEnumerable`1" />.</typeparam>
		// Token: 0x060005C4 RID: 1476 RVA: 0x000112E8 File Offset: 0x0000F4E8
		public static IEnumerable<TResult> Empty<TResult>()
		{
			return Array.Empty<TResult>();
		}

		/// <summary>Produces the set difference of two sequences by using the default equality comparer to compare values.</summary>
		/// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
		/// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements that are not also in <paramref name="second" /> will be returned.</param>
		/// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements that also occur in the first sequence will cause those elements to be removed from the returned sequence.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is null.</exception>
		// Token: 0x060005C5 RID: 1477 RVA: 0x000112EF File Offset: 0x0000F4EF
		public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			return Enumerable.ExceptIterator<TSource>(first, second, null);
		}

		/// <summary>Produces the set difference of two sequences by using the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</summary>
		/// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
		/// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements that are not also in <paramref name="second" /> will be returned.</param>
		/// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements that also occur in the first sequence will cause those elements to be removed from the returned sequence.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is null.</exception>
		// Token: 0x060005C6 RID: 1478 RVA: 0x00011315 File Offset: 0x0000F515
		public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			return Enumerable.ExceptIterator<TSource>(first, second, comparer);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001133B File Offset: 0x0000F53B
		private static IEnumerable<TSource> ExceptIterator<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			Set<TSource> set = new Set<TSource>(comparer);
			foreach (TSource tsource in second)
			{
				set.Add(tsource);
			}
			foreach (TSource tsource2 in first)
			{
				if (set.Add(tsource2))
				{
					yield return tsource2;
				}
			}
			IEnumerator<TSource> enumerator2 = null;
			yield break;
			yield break;
		}

		/// <summary>Returns the first element of a sequence.</summary>
		/// <returns>The first element in the specified sequence.</returns>
		/// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the first element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The source sequence is empty.</exception>
		// Token: 0x060005C8 RID: 1480 RVA: 0x0001135C File Offset: 0x0000F55C
		public static TSource First<TSource>(this IEnumerable<TSource> source)
		{
			bool flag;
			TSource tsource = source.TryGetFirst(out flag);
			if (!flag)
			{
				throw Error.NoElements();
			}
			return tsource;
		}

		/// <summary>Returns the first element in a sequence that satisfies a specified condition.</summary>
		/// <returns>The first element in the sequence that passes the test in the specified predicate function.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate" />.-or-The source sequence is empty.</exception>
		// Token: 0x060005C9 RID: 1481 RVA: 0x0001137C File Offset: 0x0000F57C
		public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			bool flag;
			TSource tsource = source.TryGetFirst(predicate, out flag);
			if (!flag)
			{
				throw Error.NoMatch();
			}
			return tsource;
		}

		/// <summary>Returns the first element of a sequence, or a default value if the sequence contains no elements.</summary>
		/// <returns>default(<paramref name="TSource" />) if <paramref name="source" /> is empty; otherwise, the first element in <paramref name="source" />.</returns>
		/// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the first element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060005CA RID: 1482 RVA: 0x0001139C File Offset: 0x0000F59C
		public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source)
		{
			bool flag;
			return source.TryGetFirst(out flag);
		}

		/// <summary>Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.</summary>
		/// <returns>default(<paramref name="TSource" />) if <paramref name="source" /> is empty or if no element passes the test specified by <paramref name="predicate" />; otherwise, the first element in <paramref name="source" /> that passes the test specified by <paramref name="predicate" />.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x060005CB RID: 1483 RVA: 0x000113B4 File Offset: 0x0000F5B4
		public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			bool flag;
			return source.TryGetFirst(predicate, out flag);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000113CC File Offset: 0x0000F5CC
		private static TSource TryGetFirst<TSource>(this IEnumerable<TSource> source, out bool found)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IPartition<TSource> partition;
			if ((partition = source as IPartition<TSource>) != null)
			{
				return partition.TryGetFirst(out found);
			}
			IList<TSource> list;
			if ((list = source as IList<TSource>) != null)
			{
				if (list.Count > 0)
				{
					found = true;
					return list[0];
				}
			}
			else
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						found = true;
						return enumerator.Current;
					}
				}
			}
			found = false;
			return default(TSource);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00011460 File Offset: 0x0000F660
		private static TSource TryGetFirst<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, out bool found)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			OrderedEnumerable<TSource> orderedEnumerable;
			if ((orderedEnumerable = source as OrderedEnumerable<TSource>) != null)
			{
				return orderedEnumerable.TryGetFirst(predicate, out found);
			}
			foreach (TSource tsource in source)
			{
				if (predicate(tsource))
				{
					found = true;
					return tsource;
				}
			}
			found = false;
			return default(TSource);
		}

		/// <summary>Correlates the elements of two sequences based on equality of keys and groups the results. The default equality comparer is used to compare keys.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains elements of type <paramref name="TResult" /> that are obtained by performing a grouped join on two sequences.</returns>
		/// <param name="outer">The first sequence to join.</param>
		/// <param name="inner">The sequence to join to the first sequence.</param>
		/// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
		/// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
		/// <param name="resultSelector">A function to create a result element from an element from the first sequence and a collection of matching elements from the second sequence.</param>
		/// <typeparam name="TOuter">The type of the elements of the first sequence.</typeparam>
		/// <typeparam name="TInner">The type of the elements of the second sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
		/// <typeparam name="TResult">The type of the result elements.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="outer" /> or <paramref name="inner" /> or <paramref name="outerKeySelector" /> or <paramref name="innerKeySelector" /> or <paramref name="resultSelector" /> is null.</exception>
		// Token: 0x060005CE RID: 1486 RVA: 0x000114F0 File Offset: 0x0000F6F0
		public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
		{
			if (outer == null)
			{
				throw Error.ArgumentNull("outer");
			}
			if (inner == null)
			{
				throw Error.ArgumentNull("inner");
			}
			if (outerKeySelector == null)
			{
				throw Error.ArgumentNull("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw Error.ArgumentNull("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return Enumerable.GroupJoinIterator<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, null);
		}

		/// <summary>Correlates the elements of two sequences based on key equality and groups the results. A specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> is used to compare keys.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains elements of type <paramref name="TResult" /> that are obtained by performing a grouped join on two sequences.</returns>
		/// <param name="outer">The first sequence to join.</param>
		/// <param name="inner">The sequence to join to the first sequence.</param>
		/// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
		/// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
		/// <param name="resultSelector">A function to create a result element from an element from the first sequence and a collection of matching elements from the second sequence.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to hash and compare keys.</param>
		/// <typeparam name="TOuter">The type of the elements of the first sequence.</typeparam>
		/// <typeparam name="TInner">The type of the elements of the second sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
		/// <typeparam name="TResult">The type of the result elements.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="outer" /> or <paramref name="inner" /> or <paramref name="outerKeySelector" /> or <paramref name="innerKeySelector" /> or <paramref name="resultSelector" /> is null.</exception>
		// Token: 0x060005CF RID: 1487 RVA: 0x00011550 File Offset: 0x0000F750
		public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (outer == null)
			{
				throw Error.ArgumentNull("outer");
			}
			if (inner == null)
			{
				throw Error.ArgumentNull("inner");
			}
			if (outerKeySelector == null)
			{
				throw Error.ArgumentNull("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw Error.ArgumentNull("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return Enumerable.GroupJoinIterator<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000115B1 File Offset: 0x0000F7B1
		private static IEnumerable<TResult> GroupJoinIterator<TOuter, TInner, TKey, TResult>(IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			using (IEnumerator<TOuter> e = outer.GetEnumerator())
			{
				if (e.MoveNext())
				{
					Lookup<TKey, TInner> lookup = Lookup<TKey, TInner>.CreateForJoin(inner, innerKeySelector, comparer);
					do
					{
						TOuter touter = e.Current;
						yield return resultSelector(touter, lookup[outerKeySelector(touter)]);
					}
					while (e.MoveNext());
					lookup = null;
				}
			}
			IEnumerator<TOuter> e = null;
			yield break;
			yield break;
		}

		/// <summary>Groups the elements of a sequence according to a specified key selector function.</summary>
		/// <returns>An IEnumerable&lt;IGrouping&lt;TKey, TSource&gt;&gt; in C# or IEnumerable(Of IGrouping(Of TKey, TSource)) in Visual Basic where each <see cref="T:System.Linq.IGrouping`2" /> object contains a sequence of objects and a key.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
		// Token: 0x060005D1 RID: 1489 RVA: 0x000115E6 File Offset: 0x0000F7E6
		public static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return new GroupedEnumerable<TSource, TKey>(source, keySelector, null);
		}

		/// <summary>Groups the elements of a sequence according to a specified key selector function and compares the keys by using a specified comparer.</summary>
		/// <returns>An IEnumerable&lt;IGrouping&lt;TKey, TSource&gt;&gt; in C# or IEnumerable(Of IGrouping(Of TKey, TSource)) in Visual Basic where each <see cref="T:System.Linq.IGrouping`2" /> object contains a collection of objects and a key.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
		// Token: 0x060005D2 RID: 1490 RVA: 0x000115F0 File Offset: 0x0000F7F0
		public static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			return new GroupedEnumerable<TSource, TKey>(source, keySelector, comparer);
		}

		/// <summary>Groups the elements of a sequence according to a specified key selector function and projects the elements for each group by using a specified function.</summary>
		/// <returns>An IEnumerable&lt;IGrouping&lt;TKey, TElement&gt;&gt; in C# or IEnumerable(Of IGrouping(Of TKey, TElement)) in Visual Basic where each <see cref="T:System.Linq.IGrouping`2" /> object contains a collection of objects of type <paramref name="TElement" /> and a key.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="elementSelector">A function to map each source element to an element in the <see cref="T:System.Linq.IGrouping`2" />.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the elements in the <see cref="T:System.Linq.IGrouping`2" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is null.</exception>
		// Token: 0x060005D3 RID: 1491 RVA: 0x000115FA File Offset: 0x0000F7FA
		public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return new GroupedEnumerable<TSource, TKey, TElement>(source, keySelector, elementSelector, null);
		}

		/// <summary>Groups the elements of a sequence according to a key selector function. The keys are compared by using a comparer and each group's elements are projected by using a specified function.</summary>
		/// <returns>An IEnumerable&lt;IGrouping&lt;TKey, TElement&gt;&gt; in C# or IEnumerable(Of IGrouping(Of TKey, TElement)) in Visual Basic where each <see cref="T:System.Linq.IGrouping`2" /> object contains a collection of objects of type <paramref name="TElement" /> and a key.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="elementSelector">A function to map each source element to an element in an <see cref="T:System.Linq.IGrouping`2" />.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the elements in the <see cref="T:System.Linq.IGrouping`2" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is null.</exception>
		// Token: 0x060005D4 RID: 1492 RVA: 0x00011605 File Offset: 0x0000F805
		public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			return new GroupedEnumerable<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer);
		}

		/// <summary>Groups the elements of a sequence according to a specified key selector function and creates a result value from each group and its key.</summary>
		/// <returns>A collection of elements of type <paramref name="TResult" /> where each element represents a projection over a group and its key.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="resultSelector">A function to create a result value from each group.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
		// Token: 0x060005D5 RID: 1493 RVA: 0x00011610 File Offset: 0x0000F810
		public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector)
		{
			return new GroupedResultEnumerable<TSource, TKey, TResult>(source, keySelector, resultSelector, null);
		}

		/// <summary>Groups the elements of a sequence according to a specified key selector function and creates a result value from each group and its key. The elements of each group are projected by using a specified function.</summary>
		/// <returns>A collection of elements of type <paramref name="TResult" /> where each element represents a projection over a group and its key.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="elementSelector">A function to map each source element to an element in an <see cref="T:System.Linq.IGrouping`2" />.</param>
		/// <param name="resultSelector">A function to create a result value from each group.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the elements in each <see cref="T:System.Linq.IGrouping`2" />.</typeparam>
		/// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
		// Token: 0x060005D6 RID: 1494 RVA: 0x0001161B File Offset: 0x0000F81B
		public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
		{
			return new GroupedResultEnumerable<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector, resultSelector, null);
		}

		/// <summary>Groups the elements of a sequence according to a specified key selector function and creates a result value from each group and its key. The keys are compared by using a specified comparer.</summary>
		/// <returns>A collection of elements of type <paramref name="TResult" /> where each element represents a projection over a group and its key.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="resultSelector">A function to create a result value from each group.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys with.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
		// Token: 0x060005D7 RID: 1495 RVA: 0x00011627 File Offset: 0x0000F827
		public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			return new GroupedResultEnumerable<TSource, TKey, TResult>(source, keySelector, resultSelector, comparer);
		}

		/// <summary>Groups the elements of a sequence according to a specified key selector function and creates a result value from each group and its key. Key values are compared by using a specified comparer, and the elements of each group are projected by using a specified function.</summary>
		/// <returns>A collection of elements of type <paramref name="TResult" /> where each element represents a projection over a group and its key.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="elementSelector">A function to map each source element to an element in an <see cref="T:System.Linq.IGrouping`2" />.</param>
		/// <param name="resultSelector">A function to create a result value from each group.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys with.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the elements in each <see cref="T:System.Linq.IGrouping`2" />.</typeparam>
		/// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
		// Token: 0x060005D8 RID: 1496 RVA: 0x00011632 File Offset: 0x0000F832
		public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			return new GroupedResultEnumerable<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector, resultSelector, comparer);
		}

		/// <summary>Produces the set intersection of two sequences by using the default equality comparer to compare values.</summary>
		/// <returns>A sequence that contains the elements that form the set intersection of two sequences.</returns>
		/// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements that also appear in <paramref name="second" /> will be returned.</param>
		/// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements that also appear in the first sequence will be returned.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is null.</exception>
		// Token: 0x060005D9 RID: 1497 RVA: 0x0001163F File Offset: 0x0000F83F
		public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			return Enumerable.IntersectIterator<TSource>(first, second, null);
		}

		/// <summary>Produces the set intersection of two sequences by using the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</summary>
		/// <returns>A sequence that contains the elements that form the set intersection of two sequences.</returns>
		/// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements that also appear in <paramref name="second" /> will be returned.</param>
		/// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements that also appear in the first sequence will be returned.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is null.</exception>
		// Token: 0x060005DA RID: 1498 RVA: 0x00011665 File Offset: 0x0000F865
		public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			return Enumerable.IntersectIterator<TSource>(first, second, comparer);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001168B File Offset: 0x0000F88B
		private static IEnumerable<TSource> IntersectIterator<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			Set<TSource> set = new Set<TSource>(comparer);
			foreach (TSource tsource in second)
			{
				set.Add(tsource);
			}
			foreach (TSource tsource2 in first)
			{
				if (set.Remove(tsource2))
				{
					yield return tsource2;
				}
			}
			IEnumerator<TSource> enumerator2 = null;
			yield break;
			yield break;
		}

		/// <summary>Correlates the elements of two sequences based on matching keys. The default equality comparer is used to compare keys.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that has elements of type <paramref name="TResult" /> that are obtained by performing an inner join on two sequences.</returns>
		/// <param name="outer">The first sequence to join.</param>
		/// <param name="inner">The sequence to join to the first sequence.</param>
		/// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
		/// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
		/// <param name="resultSelector">A function to create a result element from two matching elements.</param>
		/// <typeparam name="TOuter">The type of the elements of the first sequence.</typeparam>
		/// <typeparam name="TInner">The type of the elements of the second sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
		/// <typeparam name="TResult">The type of the result elements.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="outer" /> or <paramref name="inner" /> or <paramref name="outerKeySelector" /> or <paramref name="innerKeySelector" /> or <paramref name="resultSelector" /> is null.</exception>
		// Token: 0x060005DC RID: 1500 RVA: 0x000116AC File Offset: 0x0000F8AC
		public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
		{
			if (outer == null)
			{
				throw Error.ArgumentNull("outer");
			}
			if (inner == null)
			{
				throw Error.ArgumentNull("inner");
			}
			if (outerKeySelector == null)
			{
				throw Error.ArgumentNull("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw Error.ArgumentNull("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return Enumerable.JoinIterator<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, null);
		}

		/// <summary>Correlates the elements of two sequences based on matching keys. A specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> is used to compare keys.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that has elements of type <paramref name="TResult" /> that are obtained by performing an inner join on two sequences.</returns>
		/// <param name="outer">The first sequence to join.</param>
		/// <param name="inner">The sequence to join to the first sequence.</param>
		/// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
		/// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
		/// <param name="resultSelector">A function to create a result element from two matching elements.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to hash and compare keys.</param>
		/// <typeparam name="TOuter">The type of the elements of the first sequence.</typeparam>
		/// <typeparam name="TInner">The type of the elements of the second sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
		/// <typeparam name="TResult">The type of the result elements.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="outer" /> or <paramref name="inner" /> or <paramref name="outerKeySelector" /> or <paramref name="innerKeySelector" /> or <paramref name="resultSelector" /> is null.</exception>
		// Token: 0x060005DD RID: 1501 RVA: 0x0001170C File Offset: 0x0000F90C
		public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (outer == null)
			{
				throw Error.ArgumentNull("outer");
			}
			if (inner == null)
			{
				throw Error.ArgumentNull("inner");
			}
			if (outerKeySelector == null)
			{
				throw Error.ArgumentNull("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw Error.ArgumentNull("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return Enumerable.JoinIterator<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001176D File Offset: 0x0000F96D
		private static IEnumerable<TResult> JoinIterator<TOuter, TInner, TKey, TResult>(IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			using (IEnumerator<TOuter> e = outer.GetEnumerator())
			{
				if (e.MoveNext())
				{
					Lookup<TKey, TInner> lookup = Lookup<TKey, TInner>.CreateForJoin(inner, innerKeySelector, comparer);
					if (lookup.Count != 0)
					{
						do
						{
							TOuter item = e.Current;
							Grouping<TKey, TInner> grouping = lookup.GetGrouping(outerKeySelector(item), false);
							if (grouping != null)
							{
								int count = grouping._count;
								TInner[] elements = grouping._elements;
								int num;
								for (int i = 0; i != count; i = num)
								{
									yield return resultSelector(item, elements[i]);
									num = i + 1;
								}
								elements = null;
							}
							item = default(TOuter);
						}
						while (e.MoveNext());
					}
					lookup = null;
				}
			}
			IEnumerator<TOuter> e = null;
			yield break;
			yield break;
		}

		/// <summary>Returns the last element of a sequence.</summary>
		/// <returns>The value at the last position in the source sequence.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the last element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The source sequence is empty.</exception>
		// Token: 0x060005DF RID: 1503 RVA: 0x000117A4 File Offset: 0x0000F9A4
		public static TSource Last<TSource>(this IEnumerable<TSource> source)
		{
			bool flag;
			TSource tsource = source.TryGetLast(out flag);
			if (!flag)
			{
				throw Error.NoElements();
			}
			return tsource;
		}

		/// <summary>Returns the last element of a sequence that satisfies a specified condition.</summary>
		/// <returns>The last element in the sequence that passes the test in the specified predicate function.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate" />.-or-The source sequence is empty.</exception>
		// Token: 0x060005E0 RID: 1504 RVA: 0x000117C4 File Offset: 0x0000F9C4
		public static TSource Last<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			bool flag;
			TSource tsource = source.TryGetLast(predicate, out flag);
			if (!flag)
			{
				throw Error.NoMatch();
			}
			return tsource;
		}

		/// <summary>Returns the last element of a sequence, or a default value if the sequence contains no elements.</summary>
		/// <returns>default(<paramref name="TSource" />) if the source sequence is empty; otherwise, the last element in the <see cref="T:System.Collections.Generic.IEnumerable`1" />.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the last element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060005E1 RID: 1505 RVA: 0x000117E4 File Offset: 0x0000F9E4
		public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source)
		{
			bool flag;
			return source.TryGetLast(out flag);
		}

		/// <summary>Returns the last element of a sequence that satisfies a condition or a default value if no such element is found.</summary>
		/// <returns>default(<paramref name="TSource" />) if the sequence is empty or if no elements pass the test in the predicate function; otherwise, the last element that passes the test in the predicate function.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x060005E2 RID: 1506 RVA: 0x000117FC File Offset: 0x0000F9FC
		public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			bool flag;
			return source.TryGetLast(predicate, out flag);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00011814 File Offset: 0x0000FA14
		private static TSource TryGetLast<TSource>(this IEnumerable<TSource> source, out bool found)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IPartition<TSource> partition;
			if ((partition = source as IPartition<TSource>) != null)
			{
				return partition.TryGetLast(out found);
			}
			IList<TSource> list;
			if ((list = source as IList<TSource>) != null)
			{
				int count = list.Count;
				if (count > 0)
				{
					found = true;
					return list[count - 1];
				}
			}
			else
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						TSource tsource;
						do
						{
							tsource = enumerator.Current;
						}
						while (enumerator.MoveNext());
						found = true;
						return tsource;
					}
				}
			}
			found = false;
			return default(TSource);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000118B8 File Offset: 0x0000FAB8
		private static TSource TryGetLast<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, out bool found)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			OrderedEnumerable<TSource> orderedEnumerable;
			if ((orderedEnumerable = source as OrderedEnumerable<TSource>) != null)
			{
				return orderedEnumerable.TryGetLast(predicate, out found);
			}
			IList<TSource> list;
			if ((list = source as IList<TSource>) != null)
			{
				for (int i = list.Count - 1; i >= 0; i--)
				{
					TSource tsource = list[i];
					if (predicate(tsource))
					{
						found = true;
						return tsource;
					}
				}
			}
			else
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TSource tsource2 = enumerator.Current;
						if (predicate(tsource2))
						{
							while (enumerator.MoveNext())
							{
								TSource tsource3 = enumerator.Current;
								if (predicate(tsource3))
								{
									tsource2 = tsource3;
								}
							}
							found = true;
							return tsource2;
						}
					}
				}
			}
			found = false;
			return default(TSource);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Lookup`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to a specified key selector function.</summary>
		/// <returns>A <see cref="T:System.Linq.Lookup`2" /> that contains keys and values.</returns>
		/// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Linq.Lookup`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
		// Token: 0x060005E5 RID: 1509 RVA: 0x000119A8 File Offset: 0x0000FBA8
		public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ToLookup(keySelector, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Lookup`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to a specified key selector function and key comparer.</summary>
		/// <returns>A <see cref="T:System.Linq.Lookup`2" /> that contains keys and values.</returns>
		/// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Linq.Lookup`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
		// Token: 0x060005E6 RID: 1510 RVA: 0x000119B2 File Offset: 0x0000FBB2
		public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return Lookup<TKey, TSource>.Create(source, keySelector, comparer);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Lookup`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to specified key selector and element selector functions.</summary>
		/// <returns>A <see cref="T:System.Linq.Lookup`2" /> that contains values of type <paramref name="TElement" /> selected from the input sequence.</returns>
		/// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Linq.Lookup`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is null.</exception>
		// Token: 0x060005E7 RID: 1511 RVA: 0x000119D8 File Offset: 0x0000FBD8
		public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return source.ToLookup(keySelector, elementSelector, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Lookup`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to a specified key selector function, a comparer and an element selector function.</summary>
		/// <returns>A <see cref="T:System.Linq.Lookup`2" /> that contains values of type <paramref name="TElement" /> selected from the input sequence.</returns>
		/// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Linq.Lookup`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is null.</exception>
		// Token: 0x060005E8 RID: 1512 RVA: 0x000119E3 File Offset: 0x0000FBE3
		public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			if (elementSelector == null)
			{
				throw Error.ArgumentNull("elementSelector");
			}
			return Lookup<TKey, TElement>.Create<TSource>(source, keySelector, elementSelector, comparer);
		}

		/// <summary>Returns the maximum value in a sequence of <see cref="T:System.Int32" /> values.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Int32" /> values to determine the maximum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060005E9 RID: 1513 RVA: 0x00011A18 File Offset: 0x0000FC18
		public static int Max(this IEnumerable<int> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			int num;
			using (IEnumerator<int> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = enumerator.Current;
				while (enumerator.MoveNext())
				{
					int num2 = enumerator.Current;
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		/// <summary>Returns the maximum value in a sequence of nullable <see cref="T:System.Int32" /> values.</summary>
		/// <returns>A value of type Nullable&lt;Int32&gt; in C# or Nullable(Of Int32) in Visual Basic that corresponds to the maximum value in the sequence. </returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Int32" /> values to determine the maximum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060005EA RID: 1514 RVA: 0x00011A84 File Offset: 0x0000FC84
		public static int? Max(this IEnumerable<int?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			int? num = null;
			using (IEnumerator<int?> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					num = enumerator.Current;
					if (num != null)
					{
						int num2 = num.GetValueOrDefault();
						if (num2 >= 0)
						{
							while (enumerator.MoveNext())
							{
								int? num3 = enumerator.Current;
								int valueOrDefault = num3.GetValueOrDefault();
								if (valueOrDefault > num2)
								{
									num2 = valueOrDefault;
									num = num3;
								}
							}
							return num;
						}
						while (enumerator.MoveNext())
						{
							int? num4 = enumerator.Current;
							int valueOrDefault2 = num4.GetValueOrDefault();
							if ((num4 != null) & (valueOrDefault2 > num2))
							{
								num2 = valueOrDefault2;
								num = num4;
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Returns the maximum value in a sequence of <see cref="T:System.Int64" /> values.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Int64" /> values to determine the maximum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060005EB RID: 1515 RVA: 0x00011B50 File Offset: 0x0000FD50
		public static long Max(this IEnumerable<long> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long num;
			using (IEnumerator<long> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = enumerator.Current;
				while (enumerator.MoveNext())
				{
					long num2 = enumerator.Current;
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		/// <summary>Returns the maximum value in a sequence of nullable <see cref="T:System.Int64" /> values.</summary>
		/// <returns>A value of type Nullable&lt;Int64&gt; in C# or Nullable(Of Int64) in Visual Basic that corresponds to the maximum value in the sequence. </returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Int64" /> values to determine the maximum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060005EC RID: 1516 RVA: 0x00011BBC File Offset: 0x0000FDBC
		public static long? Max(this IEnumerable<long?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long? num = null;
			using (IEnumerator<long?> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					num = enumerator.Current;
					if (num != null)
					{
						long num2 = num.GetValueOrDefault();
						if (num2 >= 0L)
						{
							while (enumerator.MoveNext())
							{
								long? num3 = enumerator.Current;
								long valueOrDefault = num3.GetValueOrDefault();
								if (valueOrDefault > num2)
								{
									num2 = valueOrDefault;
									num = num3;
								}
							}
							return num;
						}
						while (enumerator.MoveNext())
						{
							long? num4 = enumerator.Current;
							long valueOrDefault2 = num4.GetValueOrDefault();
							if ((num4 != null) & (valueOrDefault2 > num2))
							{
								num2 = valueOrDefault2;
								num = num4;
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Returns the maximum value in a sequence of <see cref="T:System.Double" /> values.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Double" /> values to determine the maximum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060005ED RID: 1517 RVA: 0x00011C88 File Offset: 0x0000FE88
		public static double Max(this IEnumerable<double> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double num;
			using (IEnumerator<double> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = enumerator.Current;
				while (double.IsNaN(num))
				{
					if (!enumerator.MoveNext())
					{
						return num;
					}
					num = enumerator.Current;
				}
				while (enumerator.MoveNext())
				{
					double num2 = enumerator.Current;
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		/// <summary>Returns the maximum value in a sequence of nullable <see cref="T:System.Double" /> values.</summary>
		/// <returns>A value of type Nullable&lt;Double&gt; in C# or Nullable(Of Double) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Double" /> values to determine the maximum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060005EE RID: 1518 RVA: 0x00011D14 File Offset: 0x0000FF14
		public static double? Max(this IEnumerable<double?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double? num = null;
			using (IEnumerator<double?> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					num = enumerator.Current;
					if (num != null)
					{
						double num2 = num.GetValueOrDefault();
						while (double.IsNaN(num2))
						{
							if (!enumerator.MoveNext())
							{
								return num;
							}
							double? num3 = enumerator.Current;
							if (num3 != null)
							{
								double? num4;
								num = (num4 = num3);
								num2 = num4.GetValueOrDefault();
							}
						}
						while (enumerator.MoveNext())
						{
							double? num5 = enumerator.Current;
							double valueOrDefault = num5.GetValueOrDefault();
							if ((num5 != null) & (valueOrDefault > num2))
							{
								num2 = valueOrDefault;
								num = num5;
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Returns the maximum value in a sequence of <see cref="T:System.Single" /> values.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Single" /> values to determine the maximum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060005EF RID: 1519 RVA: 0x00011DEC File Offset: 0x0000FFEC
		public static float Max(this IEnumerable<float> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			float num;
			using (IEnumerator<float> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = enumerator.Current;
				while (float.IsNaN(num))
				{
					if (!enumerator.MoveNext())
					{
						return num;
					}
					num = enumerator.Current;
				}
				while (enumerator.MoveNext())
				{
					float num2 = enumerator.Current;
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		/// <summary>Returns the maximum value in a sequence of nullable <see cref="T:System.Single" /> values.</summary>
		/// <returns>A value of type Nullable&lt;Single&gt; in C# or Nullable(Of Single) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Single" /> values to determine the maximum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060005F0 RID: 1520 RVA: 0x00011E78 File Offset: 0x00010078
		public static float? Max(this IEnumerable<float?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			float? num = null;
			using (IEnumerator<float?> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					num = enumerator.Current;
					if (num != null)
					{
						float num2 = num.GetValueOrDefault();
						while (float.IsNaN(num2))
						{
							if (!enumerator.MoveNext())
							{
								return num;
							}
							float? num3 = enumerator.Current;
							if (num3 != null)
							{
								float? num4;
								num = (num4 = num3);
								num2 = num4.GetValueOrDefault();
							}
						}
						while (enumerator.MoveNext())
						{
							float? num5 = enumerator.Current;
							float valueOrDefault = num5.GetValueOrDefault();
							if ((num5 != null) & (valueOrDefault > num2))
							{
								num2 = valueOrDefault;
								num = num5;
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Returns the maximum value in a sequence of <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Decimal" /> values to determine the maximum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060005F1 RID: 1521 RVA: 0x00011F50 File Offset: 0x00010150
		public static decimal Max(this IEnumerable<decimal> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			decimal num;
			using (IEnumerator<decimal> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = enumerator.Current;
				while (enumerator.MoveNext())
				{
					decimal num2 = enumerator.Current;
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		/// <summary>Returns the maximum value in a sequence of nullable <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>A value of type Nullable&lt;Decimal&gt; in C# or Nullable(Of Decimal) in Visual Basic that corresponds to the maximum value in the sequence. </returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Decimal" /> values to determine the maximum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060005F2 RID: 1522 RVA: 0x00011FC0 File Offset: 0x000101C0
		public static decimal? Max(this IEnumerable<decimal?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			decimal? num = null;
			using (IEnumerator<decimal?> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					num = enumerator.Current;
					if (num != null)
					{
						decimal num2 = num.GetValueOrDefault();
						while (enumerator.MoveNext())
						{
							decimal? num3 = enumerator.Current;
							decimal valueOrDefault = num3.GetValueOrDefault();
							if (num3 != null && valueOrDefault > num2)
							{
								num2 = valueOrDefault;
								num = num3;
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Returns the maximum value in a generic sequence.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x060005F3 RID: 1523 RVA: 0x00012064 File Offset: 0x00010264
		public static TSource Max<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			Comparer<TSource> @default = Comparer<TSource>.Default;
			TSource tsource = default(TSource);
			if (tsource == null)
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						tsource = enumerator.Current;
						if (tsource != null)
						{
							while (enumerator.MoveNext())
							{
								TSource tsource2 = enumerator.Current;
								if (tsource2 != null && @default.Compare(tsource2, tsource) > 0)
								{
									tsource = tsource2;
								}
							}
							return tsource;
						}
					}
					return tsource;
				}
			}
			using (IEnumerator<TSource> enumerator2 = source.GetEnumerator())
			{
				if (!enumerator2.MoveNext())
				{
					throw Error.NoElements();
				}
				tsource = enumerator2.Current;
				while (enumerator2.MoveNext())
				{
					TSource tsource3 = enumerator2.Current;
					if (@default.Compare(tsource3, tsource) > 0)
					{
						tsource = tsource3;
					}
				}
			}
			return tsource;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the maximum <see cref="T:System.Int32" /> value.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060005F4 RID: 1524 RVA: 0x00012160 File Offset: 0x00010360
		public static int Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			int num;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = selector(enumerator.Current);
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					int num2 = selector(tsource);
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the maximum nullable <see cref="T:System.Int32" /> value.</summary>
		/// <returns>The value of type Nullable&lt;Int32&gt; in C# or Nullable(Of Int32) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x060005F5 RID: 1525 RVA: 0x000121E8 File Offset: 0x000103E8
		public static int? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			int? num = null;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					num = selector(tsource);
					if (num != null)
					{
						int num2 = num.GetValueOrDefault();
						if (num2 >= 0)
						{
							while (enumerator.MoveNext())
							{
								TSource tsource2 = enumerator.Current;
								int? num3 = selector(tsource2);
								int valueOrDefault = num3.GetValueOrDefault();
								if (valueOrDefault > num2)
								{
									num2 = valueOrDefault;
									num = num3;
								}
							}
							return num;
						}
						while (enumerator.MoveNext())
						{
							TSource tsource3 = enumerator.Current;
							int? num4 = selector(tsource3);
							int valueOrDefault2 = num4.GetValueOrDefault();
							if ((num4 != null) & (valueOrDefault2 > num2))
							{
								num2 = valueOrDefault2;
								num = num4;
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the maximum <see cref="T:System.Int64" /> value.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060005F6 RID: 1526 RVA: 0x000122D4 File Offset: 0x000104D4
		public static long Max<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			long num;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = selector(enumerator.Current);
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					long num2 = selector(tsource);
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the maximum nullable <see cref="T:System.Int64" /> value.</summary>
		/// <returns>The value of type Nullable&lt;Int64&gt; in C# or Nullable(Of Int64) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x060005F7 RID: 1527 RVA: 0x0001235C File Offset: 0x0001055C
		public static long? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			long? num = null;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					num = selector(tsource);
					if (num != null)
					{
						long num2 = num.GetValueOrDefault();
						if (num2 >= 0L)
						{
							while (enumerator.MoveNext())
							{
								TSource tsource2 = enumerator.Current;
								long? num3 = selector(tsource2);
								long valueOrDefault = num3.GetValueOrDefault();
								if (valueOrDefault > num2)
								{
									num2 = valueOrDefault;
									num = num3;
								}
							}
							return num;
						}
						while (enumerator.MoveNext())
						{
							TSource tsource3 = enumerator.Current;
							long? num4 = selector(tsource3);
							long valueOrDefault2 = num4.GetValueOrDefault();
							if ((num4 != null) & (valueOrDefault2 > num2))
							{
								num2 = valueOrDefault2;
								num = num4;
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the maximum <see cref="T:System.Single" /> value.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060005F8 RID: 1528 RVA: 0x00012448 File Offset: 0x00010648
		public static float Max<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			float num;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = selector(enumerator.Current);
				while (float.IsNaN(num))
				{
					if (!enumerator.MoveNext())
					{
						return num;
					}
					num = selector(enumerator.Current);
				}
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					float num2 = selector(tsource);
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the maximum nullable <see cref="T:System.Single" /> value.</summary>
		/// <returns>The value of type Nullable&lt;Single&gt; in C# or Nullable(Of Single) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x060005F9 RID: 1529 RVA: 0x000124F4 File Offset: 0x000106F4
		public static float? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			float? num = null;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					num = selector(tsource);
					if (num != null)
					{
						float num2 = num.GetValueOrDefault();
						while (float.IsNaN(num2))
						{
							if (!enumerator.MoveNext())
							{
								return num;
							}
							float? num3 = selector(enumerator.Current);
							if (num3 != null)
							{
								float? num4;
								num = (num4 = num3);
								num2 = num4.GetValueOrDefault();
							}
						}
						while (enumerator.MoveNext())
						{
							TSource tsource2 = enumerator.Current;
							float? num5 = selector(tsource2);
							float valueOrDefault = num5.GetValueOrDefault();
							if ((num5 != null) & (valueOrDefault > num2))
							{
								num2 = valueOrDefault;
								num = num5;
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the maximum <see cref="T:System.Double" /> value.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060005FA RID: 1530 RVA: 0x000125EC File Offset: 0x000107EC
		public static double Max<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			double num;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = selector(enumerator.Current);
				while (double.IsNaN(num))
				{
					if (!enumerator.MoveNext())
					{
						return num;
					}
					num = selector(enumerator.Current);
				}
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					double num2 = selector(tsource);
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the maximum nullable <see cref="T:System.Double" /> value.</summary>
		/// <returns>The value of type Nullable&lt;Double&gt; in C# or Nullable(Of Double) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x060005FB RID: 1531 RVA: 0x00012698 File Offset: 0x00010898
		public static double? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			double? num = null;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					num = selector(tsource);
					if (num != null)
					{
						double num2 = num.GetValueOrDefault();
						while (double.IsNaN(num2))
						{
							if (!enumerator.MoveNext())
							{
								return num;
							}
							double? num3 = selector(enumerator.Current);
							if (num3 != null)
							{
								double? num4;
								num = (num4 = num3);
								num2 = num4.GetValueOrDefault();
							}
						}
						while (enumerator.MoveNext())
						{
							TSource tsource2 = enumerator.Current;
							double? num5 = selector(tsource2);
							double valueOrDefault = num5.GetValueOrDefault();
							if ((num5 != null) & (valueOrDefault > num2))
							{
								num2 = valueOrDefault;
								num = num5;
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the maximum <see cref="T:System.Decimal" /> value.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060005FC RID: 1532 RVA: 0x00012790 File Offset: 0x00010990
		public static decimal Max<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			decimal num;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = selector(enumerator.Current);
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					decimal num2 = selector(tsource);
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the maximum nullable <see cref="T:System.Decimal" /> value.</summary>
		/// <returns>The value of type Nullable&lt;Decimal&gt; in C# or Nullable(Of Decimal) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x060005FD RID: 1533 RVA: 0x0001281C File Offset: 0x00010A1C
		public static decimal? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			decimal? num = null;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					num = selector(tsource);
					if (num != null)
					{
						decimal num2 = num.GetValueOrDefault();
						while (enumerator.MoveNext())
						{
							TSource tsource2 = enumerator.Current;
							decimal? num3 = selector(tsource2);
							decimal valueOrDefault = num3.GetValueOrDefault();
							if (num3 != null && valueOrDefault > num2)
							{
								num2 = valueOrDefault;
								num = num3;
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a generic sequence and returns the maximum resulting value.</summary>
		/// <returns>The maximum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the value returned by <paramref name="selector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x060005FE RID: 1534 RVA: 0x000128D8 File Offset: 0x00010AD8
		public static TResult Max<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			Comparer<TResult> @default = Comparer<TResult>.Default;
			TResult tresult = default(TResult);
			if (tresult == null)
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TSource tsource = enumerator.Current;
						tresult = selector(tsource);
						if (tresult != null)
						{
							while (enumerator.MoveNext())
							{
								TSource tsource2 = enumerator.Current;
								TResult tresult2 = selector(tsource2);
								if (tresult2 != null && @default.Compare(tresult2, tresult) > 0)
								{
									tresult = tresult2;
								}
							}
							return tresult;
						}
					}
					return tresult;
				}
			}
			using (IEnumerator<TSource> enumerator2 = source.GetEnumerator())
			{
				if (!enumerator2.MoveNext())
				{
					throw Error.NoElements();
				}
				tresult = selector(enumerator2.Current);
				while (enumerator2.MoveNext())
				{
					TSource tsource3 = enumerator2.Current;
					TResult tresult3 = selector(tsource3);
					if (@default.Compare(tresult3, tresult) > 0)
					{
						tresult = tresult3;
					}
				}
			}
			return tresult;
		}

		/// <summary>Returns the minimum value in a sequence of <see cref="T:System.Int32" /> values.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Int32" /> values to determine the minimum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060005FF RID: 1535 RVA: 0x000129FC File Offset: 0x00010BFC
		public static int Min(this IEnumerable<int> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			int num;
			using (IEnumerator<int> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = enumerator.Current;
				while (enumerator.MoveNext())
				{
					int num2 = enumerator.Current;
					if (num2 < num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		/// <summary>Returns the minimum value in a sequence of nullable <see cref="T:System.Int32" /> values.</summary>
		/// <returns>A value of type Nullable&lt;Int32&gt; in C# or Nullable(Of Int32) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Int32" /> values to determine the minimum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x06000600 RID: 1536 RVA: 0x00012A68 File Offset: 0x00010C68
		public static int? Min(this IEnumerable<int?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			int? num = null;
			using (IEnumerator<int?> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					num = enumerator.Current;
					if (num != null)
					{
						int num2 = num.GetValueOrDefault();
						while (enumerator.MoveNext())
						{
							int? num3 = enumerator.Current;
							int valueOrDefault = num3.GetValueOrDefault();
							if ((num3 != null) & (valueOrDefault < num2))
							{
								num2 = valueOrDefault;
								num = num3;
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Returns the minimum value in a sequence of <see cref="T:System.Int64" /> values.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Int64" /> values to determine the minimum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000601 RID: 1537 RVA: 0x00012B08 File Offset: 0x00010D08
		public static long Min(this IEnumerable<long> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long num;
			using (IEnumerator<long> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = enumerator.Current;
				while (enumerator.MoveNext())
				{
					long num2 = enumerator.Current;
					if (num2 < num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		/// <summary>Returns the minimum value in a sequence of nullable <see cref="T:System.Int64" /> values.</summary>
		/// <returns>A value of type Nullable&lt;Int64&gt; in C# or Nullable(Of Int64) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Int64" /> values to determine the minimum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x06000602 RID: 1538 RVA: 0x00012B74 File Offset: 0x00010D74
		public static long? Min(this IEnumerable<long?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long? num = null;
			using (IEnumerator<long?> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					num = enumerator.Current;
					if (num != null)
					{
						long num2 = num.GetValueOrDefault();
						while (enumerator.MoveNext())
						{
							long? num3 = enumerator.Current;
							long valueOrDefault = num3.GetValueOrDefault();
							if ((num3 != null) & (valueOrDefault < num2))
							{
								num2 = valueOrDefault;
								num = num3;
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Returns the minimum value in a sequence of <see cref="T:System.Single" /> values.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Single" /> values to determine the minimum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000603 RID: 1539 RVA: 0x00012C14 File Offset: 0x00010E14
		public static float Min(this IEnumerable<float> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			float num;
			using (IEnumerator<float> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = enumerator.Current;
				while (enumerator.MoveNext())
				{
					float num2 = enumerator.Current;
					if (num2 < num)
					{
						num = num2;
					}
					else if (float.IsNaN(num2))
					{
						return num2;
					}
				}
			}
			return num;
		}

		/// <summary>Returns the minimum value in a sequence of nullable <see cref="T:System.Single" /> values.</summary>
		/// <returns>A value of type Nullable&lt;Single&gt; in C# or Nullable(Of Single) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Single" /> values to determine the minimum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x06000604 RID: 1540 RVA: 0x00012C90 File Offset: 0x00010E90
		public static float? Min(this IEnumerable<float?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			float? num = null;
			using (IEnumerator<float?> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					num = enumerator.Current;
					if (num != null)
					{
						float num2 = num.GetValueOrDefault();
						while (enumerator.MoveNext())
						{
							float? num3 = enumerator.Current;
							if (num3 != null)
							{
								float valueOrDefault = num3.GetValueOrDefault();
								if (valueOrDefault < num2)
								{
									num2 = valueOrDefault;
									num = num3;
								}
								else if (float.IsNaN(valueOrDefault))
								{
									return num3;
								}
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Returns the minimum value in a sequence of <see cref="T:System.Double" /> values.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Double" /> values to determine the minimum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000605 RID: 1541 RVA: 0x00012D3C File Offset: 0x00010F3C
		public static double Min(this IEnumerable<double> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double num;
			using (IEnumerator<double> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = enumerator.Current;
				while (enumerator.MoveNext())
				{
					double num2 = enumerator.Current;
					if (num2 < num)
					{
						num = num2;
					}
					else if (double.IsNaN(num2))
					{
						return num2;
					}
				}
			}
			return num;
		}

		/// <summary>Returns the minimum value in a sequence of nullable <see cref="T:System.Double" /> values.</summary>
		/// <returns>A value of type Nullable&lt;Double&gt; in C# or Nullable(Of Double) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Double" /> values to determine the minimum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x06000606 RID: 1542 RVA: 0x00012DB8 File Offset: 0x00010FB8
		public static double? Min(this IEnumerable<double?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double? num = null;
			using (IEnumerator<double?> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					num = enumerator.Current;
					if (num != null)
					{
						double num2 = num.GetValueOrDefault();
						while (enumerator.MoveNext())
						{
							double? num3 = enumerator.Current;
							if (num3 != null)
							{
								double valueOrDefault = num3.GetValueOrDefault();
								if (valueOrDefault < num2)
								{
									num2 = valueOrDefault;
									num = num3;
								}
								else if (double.IsNaN(valueOrDefault))
								{
									return num3;
								}
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Returns the minimum value in a sequence of <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Decimal" /> values to determine the minimum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000607 RID: 1543 RVA: 0x00012E64 File Offset: 0x00011064
		public static decimal Min(this IEnumerable<decimal> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			decimal num;
			using (IEnumerator<decimal> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = enumerator.Current;
				while (enumerator.MoveNext())
				{
					decimal num2 = enumerator.Current;
					if (num2 < num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		/// <summary>Returns the minimum value in a sequence of nullable <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>A value of type Nullable&lt;Decimal&gt; in C# or Nullable(Of Decimal) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Decimal" /> values to determine the minimum value of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x06000608 RID: 1544 RVA: 0x00012ED4 File Offset: 0x000110D4
		public static decimal? Min(this IEnumerable<decimal?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			decimal? num = null;
			using (IEnumerator<decimal?> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					num = enumerator.Current;
					if (num != null)
					{
						decimal num2 = num.GetValueOrDefault();
						while (enumerator.MoveNext())
						{
							decimal? num3 = enumerator.Current;
							decimal valueOrDefault = num3.GetValueOrDefault();
							if (num3 != null && valueOrDefault < num2)
							{
								num2 = valueOrDefault;
								num = num3;
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Returns the minimum value in a generic sequence.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x06000609 RID: 1545 RVA: 0x00012F78 File Offset: 0x00011178
		public static TSource Min<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			Comparer<TSource> @default = Comparer<TSource>.Default;
			TSource tsource = default(TSource);
			if (tsource == null)
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						tsource = enumerator.Current;
						if (tsource != null)
						{
							while (enumerator.MoveNext())
							{
								TSource tsource2 = enumerator.Current;
								if (tsource2 != null && @default.Compare(tsource2, tsource) < 0)
								{
									tsource = tsource2;
								}
							}
							return tsource;
						}
					}
					return tsource;
				}
			}
			using (IEnumerator<TSource> enumerator2 = source.GetEnumerator())
			{
				if (!enumerator2.MoveNext())
				{
					throw Error.NoElements();
				}
				tsource = enumerator2.Current;
				while (enumerator2.MoveNext())
				{
					TSource tsource3 = enumerator2.Current;
					if (@default.Compare(tsource3, tsource) < 0)
					{
						tsource = tsource3;
					}
				}
			}
			return tsource;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the minimum <see cref="T:System.Int32" /> value.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x0600060A RID: 1546 RVA: 0x00013074 File Offset: 0x00011274
		public static int Min<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			int num;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = selector(enumerator.Current);
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					int num2 = selector(tsource);
					if (num2 < num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the minimum nullable <see cref="T:System.Int32" /> value.</summary>
		/// <returns>The value of type Nullable&lt;Int32&gt; in C# or Nullable(Of Int32) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x0600060B RID: 1547 RVA: 0x000130FC File Offset: 0x000112FC
		public static int? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			int? num = null;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					num = selector(tsource);
					if (num != null)
					{
						int num2 = num.GetValueOrDefault();
						while (enumerator.MoveNext())
						{
							TSource tsource2 = enumerator.Current;
							int? num3 = selector(tsource2);
							int valueOrDefault = num3.GetValueOrDefault();
							if ((num3 != null) & (valueOrDefault < num2))
							{
								num2 = valueOrDefault;
								num = num3;
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the minimum <see cref="T:System.Int64" /> value.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x0600060C RID: 1548 RVA: 0x000131B4 File Offset: 0x000113B4
		public static long Min<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			long num;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = selector(enumerator.Current);
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					long num2 = selector(tsource);
					if (num2 < num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the minimum nullable <see cref="T:System.Int64" /> value.</summary>
		/// <returns>The value of type Nullable&lt;Int64&gt; in C# or Nullable(Of Int64) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x0600060D RID: 1549 RVA: 0x0001323C File Offset: 0x0001143C
		public static long? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			long? num = null;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					num = selector(tsource);
					if (num != null)
					{
						long num2 = num.GetValueOrDefault();
						while (enumerator.MoveNext())
						{
							TSource tsource2 = enumerator.Current;
							long? num3 = selector(tsource2);
							long valueOrDefault = num3.GetValueOrDefault();
							if ((num3 != null) & (valueOrDefault < num2))
							{
								num2 = valueOrDefault;
								num = num3;
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the minimum <see cref="T:System.Single" /> value.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x0600060E RID: 1550 RVA: 0x000132F4 File Offset: 0x000114F4
		public static float Min<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			float num;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = selector(enumerator.Current);
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					float num2 = selector(tsource);
					if (num2 < num)
					{
						num = num2;
					}
					else if (float.IsNaN(num2))
					{
						return num2;
					}
				}
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the minimum nullable <see cref="T:System.Single" /> value.</summary>
		/// <returns>The value of type Nullable&lt;Single&gt; in C# or Nullable(Of Single) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x0600060F RID: 1551 RVA: 0x0001338C File Offset: 0x0001158C
		public static float? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			float? num = null;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					num = selector(tsource);
					if (num != null)
					{
						float num2 = num.GetValueOrDefault();
						while (enumerator.MoveNext())
						{
							TSource tsource2 = enumerator.Current;
							float? num3 = selector(tsource2);
							if (num3 != null)
							{
								float valueOrDefault = num3.GetValueOrDefault();
								if (valueOrDefault < num2)
								{
									num2 = valueOrDefault;
									num = num3;
								}
								else if (float.IsNaN(valueOrDefault))
								{
									return num3;
								}
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the minimum <see cref="T:System.Double" /> value.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000610 RID: 1552 RVA: 0x00013454 File Offset: 0x00011654
		public static double Min<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			double num;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = selector(enumerator.Current);
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					double num2 = selector(tsource);
					if (num2 < num)
					{
						num = num2;
					}
					else if (double.IsNaN(num2))
					{
						return num2;
					}
				}
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the minimum nullable <see cref="T:System.Double" /> value.</summary>
		/// <returns>The value of type Nullable&lt;Double&gt; in C# or Nullable(Of Double) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x06000611 RID: 1553 RVA: 0x000134EC File Offset: 0x000116EC
		public static double? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			double? num = null;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					num = selector(tsource);
					if (num != null)
					{
						double num2 = num.GetValueOrDefault();
						while (enumerator.MoveNext())
						{
							TSource tsource2 = enumerator.Current;
							double? num3 = selector(tsource2);
							if (num3 != null)
							{
								double valueOrDefault = num3.GetValueOrDefault();
								if (valueOrDefault < num2)
								{
									num2 = valueOrDefault;
									num = num3;
								}
								else if (double.IsNaN(valueOrDefault))
								{
									return num3;
								}
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the minimum <see cref="T:System.Decimal" /> value.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000612 RID: 1554 RVA: 0x000135B4 File Offset: 0x000117B4
		public static decimal Min<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			decimal num;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoElements();
				}
				num = selector(enumerator.Current);
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					decimal num2 = selector(tsource);
					if (num2 < num)
					{
						num = num2;
					}
				}
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a sequence and returns the minimum nullable <see cref="T:System.Decimal" /> value.</summary>
		/// <returns>The value of type Nullable&lt;Decimal&gt; in C# or Nullable(Of Decimal) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x06000613 RID: 1555 RVA: 0x00013640 File Offset: 0x00011840
		public static decimal? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			decimal? num = null;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					num = selector(tsource);
					if (num != null)
					{
						decimal num2 = num.GetValueOrDefault();
						while (enumerator.MoveNext())
						{
							TSource tsource2 = enumerator.Current;
							decimal? num3 = selector(tsource2);
							decimal valueOrDefault = num3.GetValueOrDefault();
							if (num3 != null && valueOrDefault < num2)
							{
								num2 = valueOrDefault;
								num = num3;
							}
						}
						return num;
					}
				}
				return num;
			}
			return num;
		}

		/// <summary>Invokes a transform function on each element of a generic sequence and returns the minimum resulting value.</summary>
		/// <returns>The minimum value in the sequence.</returns>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the value returned by <paramref name="selector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x06000614 RID: 1556 RVA: 0x000136FC File Offset: 0x000118FC
		public static TResult Min<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			Comparer<TResult> @default = Comparer<TResult>.Default;
			TResult tresult = default(TResult);
			if (tresult == null)
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TSource tsource = enumerator.Current;
						tresult = selector(tsource);
						if (tresult != null)
						{
							while (enumerator.MoveNext())
							{
								TSource tsource2 = enumerator.Current;
								TResult tresult2 = selector(tsource2);
								if (tresult2 != null && @default.Compare(tresult2, tresult) < 0)
								{
									tresult = tresult2;
								}
							}
							return tresult;
						}
					}
					return tresult;
				}
			}
			using (IEnumerator<TSource> enumerator2 = source.GetEnumerator())
			{
				if (!enumerator2.MoveNext())
				{
					throw Error.NoElements();
				}
				tresult = selector(enumerator2.Current);
				while (enumerator2.MoveNext())
				{
					TSource tsource3 = enumerator2.Current;
					TResult tresult3 = selector(tsource3);
					if (@default.Compare(tresult3, tresult) < 0)
					{
						tresult = tresult3;
					}
				}
			}
			return tresult;
		}

		/// <summary>Sorts the elements of a sequence in ascending order according to a key.</summary>
		/// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted according to a key.</returns>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
		// Token: 0x06000615 RID: 1557 RVA: 0x00013820 File Offset: 0x00011A20
		public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return new OrderedEnumerable<TSource, TKey>(source, keySelector, null, false, null);
		}

		/// <summary>Sorts the elements of a sequence in ascending order by using a specified comparer.</summary>
		/// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted according to a key.</returns>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
		// Token: 0x06000616 RID: 1558 RVA: 0x0001382C File Offset: 0x00011A2C
		public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			return new OrderedEnumerable<TSource, TKey>(source, keySelector, comparer, false, null);
		}

		/// <summary>Sorts the elements of a sequence in descending order according to a key.</summary>
		/// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted in descending order according to a key.</returns>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
		// Token: 0x06000617 RID: 1559 RVA: 0x00013838 File Offset: 0x00011A38
		public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return new OrderedEnumerable<TSource, TKey>(source, keySelector, null, true, null);
		}

		/// <summary>Sorts the elements of a sequence in descending order by using a specified comparer.</summary>
		/// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted in descending order according to a key.</returns>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
		// Token: 0x06000618 RID: 1560 RVA: 0x00013844 File Offset: 0x00011A44
		public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			return new OrderedEnumerable<TSource, TKey>(source, keySelector, comparer, true, null);
		}

		/// <summary>Performs a subsequent ordering of the elements in a sequence in ascending order according to a key.</summary>
		/// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted according to a key.</returns>
		/// <param name="source">An <see cref="T:System.Linq.IOrderedEnumerable`1" /> that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
		// Token: 0x06000619 RID: 1561 RVA: 0x00013850 File Offset: 0x00011A50
		public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.CreateOrderedEnumerable<TKey>(keySelector, null, false);
		}

		/// <summary>Performs a subsequent ordering of the elements in a sequence in ascending order by using a specified comparer.</summary>
		/// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted according to a key.</returns>
		/// <param name="source">An <see cref="T:System.Linq.IOrderedEnumerable`1" /> that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
		// Token: 0x0600061A RID: 1562 RVA: 0x00013869 File Offset: 0x00011A69
		public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.CreateOrderedEnumerable<TKey>(keySelector, comparer, false);
		}

		/// <summary>Performs a subsequent ordering of the elements in a sequence in descending order, according to a key.</summary>
		/// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted in descending order according to a key.</returns>
		/// <param name="source">An <see cref="T:System.Linq.IOrderedEnumerable`1" /> that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
		// Token: 0x0600061B RID: 1563 RVA: 0x00013882 File Offset: 0x00011A82
		public static IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.CreateOrderedEnumerable<TKey>(keySelector, null, true);
		}

		/// <summary>Performs a subsequent ordering of the elements in a sequence in descending order by using a specified comparer.</summary>
		/// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted in descending order according to a key.</returns>
		/// <param name="source">An <see cref="T:System.Linq.IOrderedEnumerable`1" /> that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
		// Token: 0x0600061C RID: 1564 RVA: 0x0001389B File Offset: 0x00011A9B
		public static IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.CreateOrderedEnumerable<TKey>(keySelector, comparer, true);
		}

		/// <summary>Generates a sequence of integral numbers within a specified range.</summary>
		/// <returns>An IEnumerable&lt;Int32&gt; in C# or IEnumerable(Of Int32) in Visual Basic that contains a range of sequential integral numbers.</returns>
		/// <param name="start">The value of the first integer in the sequence.</param>
		/// <param name="count">The number of sequential integers to generate.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than 0.-or-<paramref name="start" /> + <paramref name="count" /> -1 is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x0600061D RID: 1565 RVA: 0x000138B4 File Offset: 0x00011AB4
		public static IEnumerable<int> Range(int start, int count)
		{
			long num = (long)start + (long)count - 1L;
			if (count < 0 || num > 2147483647L)
			{
				throw Error.ArgumentOutOfRange("count");
			}
			if (count == 0)
			{
				return EmptyPartition<int>.Instance;
			}
			return new Enumerable.RangeIterator(start, count);
		}

		/// <summary>Generates a sequence that contains one repeated value.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains a repeated value.</returns>
		/// <param name="element">The value to be repeated.</param>
		/// <param name="count">The number of times to repeat the value in the generated sequence.</param>
		/// <typeparam name="TResult">The type of the value to be repeated in the result sequence.</typeparam>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than 0.</exception>
		// Token: 0x0600061E RID: 1566 RVA: 0x000138F2 File Offset: 0x00011AF2
		public static IEnumerable<TResult> Repeat<TResult>(TResult element, int count)
		{
			if (count < 0)
			{
				throw Error.ArgumentOutOfRange("count");
			}
			if (count == 0)
			{
				return EmptyPartition<TResult>.Instance;
			}
			return new Enumerable.RepeatIterator<TResult>(element, count);
		}

		/// <summary>Inverts the order of the elements in a sequence.</summary>
		/// <returns>A sequence whose elements correspond to those of the input sequence in reverse order.</returns>
		/// <param name="source">A sequence of values to reverse.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x0600061F RID: 1567 RVA: 0x00013913 File Offset: 0x00011B13
		public static IEnumerable<TSource> Reverse<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return new Enumerable.ReverseIterator<TSource>(source);
		}

		/// <summary>Projects each element of a sequence into a new form.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are the result of invoking the transform function on each element of <paramref name="source" />.</returns>
		/// <param name="source">A sequence of values to invoke a transform function on.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the value returned by <paramref name="selector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x06000620 RID: 1568 RVA: 0x0001392C File Offset: 0x00011B2C
		public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			Enumerable.Iterator<TSource> iterator;
			if ((iterator = source as Enumerable.Iterator<TSource>) != null)
			{
				return iterator.Select<TResult>(selector);
			}
			IList<TSource> list;
			if ((list = source as IList<TSource>) != null)
			{
				TSource[] array;
				if ((array = source as TSource[]) != null)
				{
					if (array.Length != 0)
					{
						return new Enumerable.SelectArrayIterator<TSource, TResult>(array, selector);
					}
					return EmptyPartition<TResult>.Instance;
				}
				else
				{
					List<TSource> list2;
					if ((list2 = source as List<TSource>) != null)
					{
						return new Enumerable.SelectListIterator<TSource, TResult>(list2, selector);
					}
					return new Enumerable.SelectIListIterator<TSource, TResult>(list, selector);
				}
			}
			else
			{
				IPartition<TSource> partition;
				if ((partition = source as IPartition<TSource>) == null)
				{
					return new Enumerable.SelectEnumerableIterator<TSource, TResult>(source, selector);
				}
				if (!(partition is EmptyPartition<TSource>))
				{
					return new Enumerable.SelectIPartitionIterator<TSource, TResult>(partition, selector);
				}
				return EmptyPartition<TResult>.Instance;
			}
		}

		/// <summary>Projects each element of a sequence into a new form by incorporating the element's index.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are the result of invoking the transform function on each element of <paramref name="source" />.</returns>
		/// <param name="source">A sequence of values to invoke a transform function on.</param>
		/// <param name="selector">A transform function to apply to each source element; the second parameter of the function represents the index of the source element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the value returned by <paramref name="selector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x06000621 RID: 1569 RVA: 0x000139D8 File Offset: 0x00011BD8
		public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return Enumerable.SelectIterator<TSource, TResult>(source, selector);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000139FD File Offset: 0x00011BFD
		private static IEnumerable<TResult> SelectIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
		{
			int index = -1;
			foreach (TSource tsource in source)
			{
				int num = index;
				index = checked(num + 1);
				yield return selector(tsource, index);
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		/// <summary>Projects each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" /> and flattens the resulting sequences into one sequence.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.</returns>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the elements of the sequence returned by <paramref name="selector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x06000623 RID: 1571 RVA: 0x00013A14 File Offset: 0x00011C14
		public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return new Enumerable.SelectManySingleSelectorIterator<TSource, TResult>(source, selector);
		}

		/// <summary>Projects each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" />, and flattens the resulting sequences into one sequence. The index of each source element is used in the projected form of that element.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are the result of invoking the one-to-many transform function on each element of an input sequence.</returns>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="selector">A transform function to apply to each source element; the second parameter of the function represents the index of the source element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the elements of the sequence returned by <paramref name="selector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x06000624 RID: 1572 RVA: 0x00013A39 File Offset: 0x00011C39
		public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return Enumerable.SelectManyIterator<TSource, TResult>(source, selector);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00013A5E File Offset: 0x00011C5E
		private static IEnumerable<TResult> SelectManyIterator<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
		{
			int index = -1;
			foreach (TSource tsource in source)
			{
				int num = index;
				index = checked(num + 1);
				foreach (TResult tresult in selector(tsource, index))
				{
					yield return tresult;
				}
				IEnumerator<TResult> enumerator2 = null;
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		/// <summary>Projects each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" />, flattens the resulting sequences into one sequence, and invokes a result selector function on each element therein. The index of each source element is used in the intermediate projected form of that element.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are the result of invoking the one-to-many transform function <paramref name="collectionSelector" /> on each element of <paramref name="source" /> and then mapping each of those sequence elements and their corresponding source element to a result element.</returns>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="collectionSelector">A transform function to apply to each source element; the second parameter of the function represents the index of the source element.</param>
		/// <param name="resultSelector">A transform function to apply to each element of the intermediate sequence.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TCollection">The type of the intermediate elements collected by <paramref name="collectionSelector" />.</typeparam>
		/// <typeparam name="TResult">The type of the elements of the resulting sequence.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="collectionSelector" /> or <paramref name="resultSelector" /> is null.</exception>
		// Token: 0x06000626 RID: 1574 RVA: 0x00013A75 File Offset: 0x00011C75
		public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (collectionSelector == null)
			{
				throw Error.ArgumentNull("collectionSelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return Enumerable.SelectManyIterator<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00013AA9 File Offset: 0x00011CA9
		private static IEnumerable<TResult> SelectManyIterator<TSource, TCollection, TResult>(IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			int index = -1;
			foreach (TSource element in source)
			{
				int num = index;
				index = checked(num + 1);
				foreach (TCollection tcollection in collectionSelector(element, index))
				{
					yield return resultSelector(element, tcollection);
				}
				IEnumerator<TCollection> enumerator2 = null;
				element = default(TSource);
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		/// <summary>Projects each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" />, flattens the resulting sequences into one sequence, and invokes a result selector function on each element therein.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are the result of invoking the one-to-many transform function <paramref name="collectionSelector" /> on each element of <paramref name="source" /> and then mapping each of those sequence elements and their corresponding source element to a result element.</returns>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="collectionSelector">A transform function to apply to each element of the input sequence.</param>
		/// <param name="resultSelector">A transform function to apply to each element of the intermediate sequence.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TCollection">The type of the intermediate elements collected by <paramref name="collectionSelector" />.</typeparam>
		/// <typeparam name="TResult">The type of the elements of the resulting sequence.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="collectionSelector" /> or <paramref name="resultSelector" /> is null.</exception>
		// Token: 0x06000628 RID: 1576 RVA: 0x00013AC7 File Offset: 0x00011CC7
		public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (collectionSelector == null)
			{
				throw Error.ArgumentNull("collectionSelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return Enumerable.SelectManyIterator<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00013AFB File Offset: 0x00011CFB
		private static IEnumerable<TResult> SelectManyIterator<TSource, TCollection, TResult>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			foreach (TSource element in source)
			{
				foreach (TCollection tcollection in collectionSelector(element))
				{
					yield return resultSelector(element, tcollection);
				}
				IEnumerator<TCollection> enumerator2 = null;
				element = default(TSource);
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		/// <summary>Determines whether two sequences are equal by comparing the elements by using the default equality comparer for their type.</summary>
		/// <returns>true if the two source sequences are of equal length and their corresponding elements are equal according to the default equality comparer for their type; otherwise, false.</returns>
		/// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to compare to <paramref name="second" />.</param>
		/// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to compare to the first sequence.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is null.</exception>
		// Token: 0x0600062A RID: 1578 RVA: 0x00013B19 File Offset: 0x00011D19
		public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			return first.SequenceEqual(second, null);
		}

		/// <summary>Determines whether two sequences are equal by comparing their elements by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <returns>true if the two source sequences are of equal length and their corresponding elements compare equal according to <paramref name="comparer" />; otherwise, false.</returns>
		/// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to compare to <paramref name="second" />.</param>
		/// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to compare to the first sequence.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to use to compare elements.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is null.</exception>
		// Token: 0x0600062B RID: 1579 RVA: 0x00013B24 File Offset: 0x00011D24
		public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TSource>.Default;
			}
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			ICollection<TSource> collection;
			ICollection<TSource> collection2;
			if ((collection = first as ICollection<TSource>) != null && (collection2 = second as ICollection<TSource>) != null)
			{
				if (collection.Count != collection2.Count)
				{
					return false;
				}
				IList<TSource> list;
				IList<TSource> list2;
				if ((list = collection as IList<TSource>) != null && (list2 = collection2 as IList<TSource>) != null)
				{
					int count = collection.Count;
					for (int i = 0; i < count; i++)
					{
						if (!comparer.Equals(list[i], list2[i]))
						{
							return false;
						}
					}
					return true;
				}
			}
			bool flag;
			using (IEnumerator<TSource> enumerator = first.GetEnumerator())
			{
				using (IEnumerator<TSource> enumerator2 = second.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator2.MoveNext() || !comparer.Equals(enumerator.Current, enumerator2.Current))
						{
							return false;
						}
					}
					flag = !enumerator2.MoveNext();
				}
			}
			return flag;
		}

		/// <summary>Returns the only element of a sequence, and throws an exception if there is not exactly one element in the sequence.</summary>
		/// <returns>The single element of the input sequence.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the single element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The input sequence contains more than one element.-or-The input sequence is empty.</exception>
		// Token: 0x0600062C RID: 1580 RVA: 0x00013C48 File Offset: 0x00011E48
		public static TSource Single<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IList<TSource> list;
			if ((list = source as IList<TSource>) != null)
			{
				int count = list.Count;
				if (count == 0)
				{
					throw Error.NoElements();
				}
				if (count == 1)
				{
					return list[0];
				}
			}
			else
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					if (!enumerator.MoveNext())
					{
						throw Error.NoElements();
					}
					TSource tsource = enumerator.Current;
					if (!enumerator.MoveNext())
					{
						return tsource;
					}
				}
			}
			throw Error.MoreThanOneElement();
		}

		/// <summary>Returns the only element of a sequence that satisfies a specified condition, and throws an exception if more than one such element exists.</summary>
		/// <returns>The single element of the input sequence that satisfies a condition.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return a single element from.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate" />.-or-More than one element satisfies the condition in <paramref name="predicate" />.-or-The source sequence is empty.</exception>
		// Token: 0x0600062D RID: 1581 RVA: 0x00013CD8 File Offset: 0x00011ED8
		public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					if (predicate(tsource))
					{
						while (enumerator.MoveNext())
						{
							if (predicate(enumerator.Current))
							{
								throw Error.MoreThanOneMatch();
							}
						}
						return tsource;
					}
				}
			}
			throw Error.NoMatch();
		}

		/// <summary>Returns the only element of a sequence, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.</summary>
		/// <returns>The single element of the input sequence, or default(<paramref name="TSource" />) if the sequence contains no elements.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the single element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The input sequence contains more than one element.</exception>
		// Token: 0x0600062E RID: 1582 RVA: 0x00013D68 File Offset: 0x00011F68
		public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IList<TSource> list;
			if ((list = source as IList<TSource>) != null)
			{
				int count = list.Count;
				if (count == 0)
				{
					TSource tsource = default(TSource);
					return tsource;
				}
				if (count == 1)
				{
					return list[0];
				}
			}
			else
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					if (!enumerator.MoveNext())
					{
						TSource tsource = default(TSource);
						return tsource;
					}
					TSource tsource2 = enumerator.Current;
					if (!enumerator.MoveNext())
					{
						return tsource2;
					}
				}
			}
			throw Error.MoreThanOneElement();
		}

		/// <summary>Returns the only element of a sequence that satisfies a specified condition or a default value if no such element exists; this method throws an exception if more than one element satisfies the condition.</summary>
		/// <returns>The single element of the input sequence that satisfies the condition, or default(<paramref name="TSource" />) if no such element is found.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return a single element from.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x0600062F RID: 1583 RVA: 0x00013E04 File Offset: 0x00012004
		public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					if (predicate(tsource))
					{
						while (enumerator.MoveNext())
						{
							if (predicate(enumerator.Current))
							{
								throw Error.MoreThanOneMatch();
							}
						}
						return tsource;
					}
				}
			}
			return default(TSource);
		}

		/// <summary>Bypasses a specified number of elements in a sequence and then returns the remaining elements.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements that occur after the specified index in the input sequence.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return elements from.</param>
		/// <param name="count">The number of elements to skip before returning the remaining elements.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x06000630 RID: 1584 RVA: 0x00013E98 File Offset: 0x00012098
		public static IEnumerable<TSource> Skip<TSource>(this IEnumerable<TSource> source, int count)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IPartition<TSource> partition;
			if (count <= 0)
			{
				if (source is Enumerable.Iterator<TSource> || source is IPartition<TSource>)
				{
					return source;
				}
				count = 0;
			}
			else if ((partition = source as IPartition<TSource>) != null)
			{
				return partition.Skip(count);
			}
			IList<TSource> list;
			if ((list = source as IList<TSource>) != null)
			{
				return new Enumerable.ListPartition<TSource>(list, count, int.MaxValue);
			}
			return new Enumerable.EnumerablePartition<TSource>(source, count, -1);
		}

		/// <summary>Bypasses elements in a sequence as long as a specified condition is true and then returns the remaining elements.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements from the input sequence starting at the first element in the linear series that does not pass the test specified by <paramref name="predicate" />.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return elements from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x06000631 RID: 1585 RVA: 0x00013EFF File Offset: 0x000120FF
		public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return Enumerable.SkipWhileIterator<TSource>(source, predicate);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00013F24 File Offset: 0x00012124
		private static IEnumerable<TSource> SkipWhileIterator<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			using (IEnumerator<TSource> e = source.GetEnumerator())
			{
				while (e.MoveNext())
				{
					TSource tsource = e.Current;
					if (!predicate(tsource))
					{
						yield return tsource;
						while (e.MoveNext())
						{
							TSource tsource2 = e.Current;
							yield return tsource2;
						}
						yield break;
					}
				}
			}
			IEnumerator<TSource> e = null;
			yield break;
			yield break;
		}

		/// <summary>Bypasses elements in a sequence as long as a specified condition is true and then returns the remaining elements. The element's index is used in the logic of the predicate function.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements from the input sequence starting at the first element in the linear series that does not pass the test specified by <paramref name="predicate" />.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return elements from.</param>
		/// <param name="predicate">A function to test each source element for a condition; the second parameter of the function represents the index of the source element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x06000633 RID: 1587 RVA: 0x00013F3B File Offset: 0x0001213B
		public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return Enumerable.SkipWhileIterator<TSource>(source, predicate);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00013F60 File Offset: 0x00012160
		private static IEnumerable<TSource> SkipWhileIterator<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			using (IEnumerator<TSource> e = source.GetEnumerator())
			{
				int index = -1;
				while (e.MoveNext())
				{
					int num = index;
					index = checked(num + 1);
					TSource tsource = e.Current;
					if (!predicate(tsource, index))
					{
						yield return tsource;
						while (e.MoveNext())
						{
							TSource tsource2 = e.Current;
							yield return tsource2;
						}
						yield break;
					}
				}
			}
			IEnumerator<TSource> e = null;
			yield break;
			yield break;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00013F77 File Offset: 0x00012177
		public static IEnumerable<TSource> SkipLast<TSource>(this IEnumerable<TSource> source, int count)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (count <= 0)
			{
				return source.Skip(0);
			}
			return Enumerable.SkipLastIterator<TSource>(source, count);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00013F9A File Offset: 0x0001219A
		private static IEnumerable<TSource> SkipLastIterator<TSource>(IEnumerable<TSource> source, int count)
		{
			Queue<TSource> queue = new Queue<TSource>();
			using (IEnumerator<TSource> e = source.GetEnumerator())
			{
				while (e.MoveNext())
				{
					if (queue.Count == count)
					{
						do
						{
							yield return queue.Dequeue();
							queue.Enqueue(e.Current);
						}
						while (e.MoveNext());
						break;
					}
					queue.Enqueue(e.Current);
				}
			}
			IEnumerator<TSource> e = null;
			yield break;
			yield break;
		}

		/// <summary>Computes the sum of a sequence of <see cref="T:System.Int32" /> values.</summary>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Int32" /> values to calculate the sum of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000637 RID: 1591 RVA: 0x00013FB4 File Offset: 0x000121B4
		public static int Sum(this IEnumerable<int> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			int num = 0;
			checked
			{
				foreach (int num2 in source)
				{
					num += num2;
				}
				return num;
			}
		}

		/// <summary>Computes the sum of a sequence of nullable <see cref="T:System.Int32" /> values.</summary>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Int32" /> values to calculate the sum of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000638 RID: 1592 RVA: 0x0001400C File Offset: 0x0001220C
		public static int? Sum(this IEnumerable<int?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			int num = 0;
			checked
			{
				foreach (int? num2 in source)
				{
					if (num2 != null)
					{
						num += num2.GetValueOrDefault();
					}
				}
				return new int?(num);
			}
		}

		/// <summary>Computes the sum of a sequence of <see cref="T:System.Int64" /> values.</summary>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Int64" /> values to calculate the sum of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x06000639 RID: 1593 RVA: 0x00014078 File Offset: 0x00012278
		public static long Sum(this IEnumerable<long> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long num = 0L;
			checked
			{
				foreach (long num2 in source)
				{
					num += num2;
				}
				return num;
			}
		}

		/// <summary>Computes the sum of a sequence of nullable <see cref="T:System.Int64" /> values.</summary>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Int64" /> values to calculate the sum of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x0600063A RID: 1594 RVA: 0x000140D0 File Offset: 0x000122D0
		public static long? Sum(this IEnumerable<long?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long num = 0L;
			checked
			{
				foreach (long? num2 in source)
				{
					if (num2 != null)
					{
						num += num2.GetValueOrDefault();
					}
				}
				return new long?(num);
			}
		}

		/// <summary>Computes the sum of a sequence of <see cref="T:System.Single" /> values.</summary>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Single" /> values to calculate the sum of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x0600063B RID: 1595 RVA: 0x0001413C File Offset: 0x0001233C
		public static float Sum(this IEnumerable<float> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double num = 0.0;
			foreach (float num2 in source)
			{
				num += (double)num2;
			}
			return (float)num;
		}

		/// <summary>Computes the sum of a sequence of nullable <see cref="T:System.Single" /> values.</summary>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Single" /> values to calculate the sum of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x0600063C RID: 1596 RVA: 0x0001419C File Offset: 0x0001239C
		public static float? Sum(this IEnumerable<float?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double num = 0.0;
			foreach (float? num2 in source)
			{
				if (num2 != null)
				{
					num += (double)num2.GetValueOrDefault();
				}
			}
			return new float?((float)num);
		}

		/// <summary>Computes the sum of a sequence of <see cref="T:System.Double" /> values.</summary>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Double" /> values to calculate the sum of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x0600063D RID: 1597 RVA: 0x00014210 File Offset: 0x00012410
		public static double Sum(this IEnumerable<double> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double num = 0.0;
			foreach (double num2 in source)
			{
				num += num2;
			}
			return num;
		}

		/// <summary>Computes the sum of a sequence of nullable <see cref="T:System.Double" /> values.</summary>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Double" /> values to calculate the sum of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x0600063E RID: 1598 RVA: 0x00014270 File Offset: 0x00012470
		public static double? Sum(this IEnumerable<double?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			double num = 0.0;
			foreach (double? num2 in source)
			{
				if (num2 != null)
				{
					num += num2.GetValueOrDefault();
				}
			}
			return new double?(num);
		}

		/// <summary>Computes the sum of a sequence of <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <param name="source">A sequence of <see cref="T:System.Decimal" /> values to calculate the sum of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x0600063F RID: 1599 RVA: 0x000142E4 File Offset: 0x000124E4
		public static decimal Sum(this IEnumerable<decimal> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			decimal num = 0m;
			foreach (decimal num2 in source)
			{
				num += num2;
			}
			return num;
		}

		/// <summary>Computes the sum of a sequence of nullable <see cref="T:System.Decimal" /> values.</summary>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <param name="source">A sequence of nullable <see cref="T:System.Decimal" /> values to calculate the sum of.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06000640 RID: 1600 RVA: 0x00014344 File Offset: 0x00012544
		public static decimal? Sum(this IEnumerable<decimal?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			decimal num = 0m;
			foreach (decimal? num2 in source)
			{
				if (num2 != null)
				{
					num += num2.GetValueOrDefault();
				}
			}
			return new decimal?(num);
		}

		/// <summary>Computes the sum of the sequence of <see cref="T:System.Int32" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values.</returns>
		/// <param name="source">A sequence of values that are used to calculate a sum.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000641 RID: 1601 RVA: 0x000143B8 File Offset: 0x000125B8
		public static int Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			int num = 0;
			checked
			{
				foreach (TSource tsource in source)
				{
					num += selector(tsource);
				}
				return num;
			}
		}

		/// <summary>Computes the sum of the sequence of nullable <see cref="T:System.Int32" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values.</returns>
		/// <param name="source">A sequence of values that are used to calculate a sum.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000642 RID: 1602 RVA: 0x00014424 File Offset: 0x00012624
		public static int? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			int num = 0;
			checked
			{
				foreach (TSource tsource in source)
				{
					int? num2 = selector(tsource);
					if (num2 != null)
					{
						num += num2.GetValueOrDefault();
					}
				}
				return new int?(num);
			}
		}

		/// <summary>Computes the sum of the sequence of <see cref="T:System.Int64" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values.</returns>
		/// <param name="source">A sequence of values that are used to calculate a sum.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x06000643 RID: 1603 RVA: 0x000144A4 File Offset: 0x000126A4
		public static long Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
		{
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			long num = 0L;
			checked
			{
				foreach (TSource tsource in source)
				{
					num += selector(tsource);
				}
				return num;
			}
		}

		/// <summary>Computes the sum of the sequence of nullable <see cref="T:System.Int64" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values.</returns>
		/// <param name="source">A sequence of values that are used to calculate a sum.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x06000644 RID: 1604 RVA: 0x00014510 File Offset: 0x00012710
		public static long? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			long num = 0L;
			checked
			{
				foreach (TSource tsource in source)
				{
					long? num2 = selector(tsource);
					if (num2 != null)
					{
						num += num2.GetValueOrDefault();
					}
				}
				return new long?(num);
			}
		}

		/// <summary>Computes the sum of the sequence of <see cref="T:System.Single" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values.</returns>
		/// <param name="source">A sequence of values that are used to calculate a sum.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x06000645 RID: 1605 RVA: 0x00014594 File Offset: 0x00012794
		public static float Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			double num = 0.0;
			foreach (TSource tsource in source)
			{
				num += (double)selector(tsource);
			}
			return (float)num;
		}

		/// <summary>Computes the sum of the sequence of nullable <see cref="T:System.Single" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values.</returns>
		/// <param name="source">A sequence of values that are used to calculate a sum.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x06000646 RID: 1606 RVA: 0x00014608 File Offset: 0x00012808
		public static float? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			double num = 0.0;
			foreach (TSource tsource in source)
			{
				float? num2 = selector(tsource);
				if (num2 != null)
				{
					num += (double)num2.GetValueOrDefault();
				}
			}
			return new float?((float)num);
		}

		/// <summary>Computes the sum of the sequence of <see cref="T:System.Double" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values.</returns>
		/// <param name="source">A sequence of values that are used to calculate a sum.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x06000647 RID: 1607 RVA: 0x00014694 File Offset: 0x00012894
		public static double Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			double num = 0.0;
			foreach (TSource tsource in source)
			{
				num += selector(tsource);
			}
			return num;
		}

		/// <summary>Computes the sum of the sequence of nullable <see cref="T:System.Double" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values.</returns>
		/// <param name="source">A sequence of values that are used to calculate a sum.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		// Token: 0x06000648 RID: 1608 RVA: 0x00014708 File Offset: 0x00012908
		public static double? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			double num = 0.0;
			foreach (TSource tsource in source)
			{
				double? num2 = selector(tsource);
				if (num2 != null)
				{
					num += num2.GetValueOrDefault();
				}
			}
			return new double?(num);
		}

		/// <summary>Computes the sum of the sequence of <see cref="T:System.Decimal" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values.</returns>
		/// <param name="source">A sequence of values that are used to calculate a sum.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06000649 RID: 1609 RVA: 0x00014790 File Offset: 0x00012990
		public static decimal Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			decimal num = 0m;
			foreach (TSource tsource in source)
			{
				num += selector(tsource);
			}
			return num;
		}

		/// <summary>Computes the sum of the sequence of nullable <see cref="T:System.Decimal" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <returns>The sum of the projected values.</returns>
		/// <param name="source">A sequence of values that are used to calculate a sum.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x0600064A RID: 1610 RVA: 0x00014804 File Offset: 0x00012A04
		public static decimal? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			decimal num = 0m;
			foreach (TSource tsource in source)
			{
				decimal? num2 = selector(tsource);
				if (num2 != null)
				{
					num += num2.GetValueOrDefault();
				}
			}
			return new decimal?(num);
		}

		/// <summary>Returns a specified number of contiguous elements from the start of a sequence.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the specified number of elements from the start of the input sequence.</returns>
		/// <param name="source">The sequence to return elements from.</param>
		/// <param name="count">The number of elements to return.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x0600064B RID: 1611 RVA: 0x00014890 File Offset: 0x00012A90
		public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source, int count)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (count <= 0)
			{
				return EmptyPartition<TSource>.Instance;
			}
			IPartition<TSource> partition;
			if ((partition = source as IPartition<TSource>) != null)
			{
				return partition.Take(count);
			}
			IList<TSource> list;
			if ((list = source as IList<TSource>) != null)
			{
				return new Enumerable.ListPartition<TSource>(list, 0, count - 1);
			}
			return new Enumerable.EnumerablePartition<TSource>(source, 0, count - 1);
		}

		/// <summary>Returns elements from a sequence as long as a specified condition is true.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements from the input sequence that occur before the element at which the test no longer passes.</returns>
		/// <param name="source">A sequence to return elements from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x0600064C RID: 1612 RVA: 0x000148E6 File Offset: 0x00012AE6
		public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return Enumerable.TakeWhileIterator<TSource>(source, predicate);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001490B File Offset: 0x00012B0B
		private static IEnumerable<TSource> TakeWhileIterator<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			foreach (TSource tsource in source)
			{
				if (!predicate(tsource))
				{
					break;
				}
				yield return tsource;
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		/// <summary>Returns elements from a sequence as long as a specified condition is true. The element's index is used in the logic of the predicate function.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains elements from the input sequence that occur before the element at which the test no longer passes.</returns>
		/// <param name="source">The sequence to return elements from.</param>
		/// <param name="predicate">A function to test each source element for a condition; the second parameter of the function represents the index of the source element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x0600064E RID: 1614 RVA: 0x00014922 File Offset: 0x00012B22
		public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return Enumerable.TakeWhileIterator<TSource>(source, predicate);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00014947 File Offset: 0x00012B47
		private static IEnumerable<TSource> TakeWhileIterator<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			int index = -1;
			foreach (TSource tsource in source)
			{
				int num = index;
				index = checked(num + 1);
				if (!predicate(tsource, index))
				{
					break;
				}
				yield return tsource;
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001495E File Offset: 0x00012B5E
		public static IEnumerable<TSource> TakeLast<TSource>(this IEnumerable<TSource> source, int count)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (count <= 0)
			{
				return EmptyPartition<TSource>.Instance;
			}
			return Enumerable.TakeLastIterator<TSource>(source, count);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001497F File Offset: 0x00012B7F
		private static IEnumerable<TSource> TakeLastIterator<TSource>(IEnumerable<TSource> source, int count)
		{
			Queue<TSource> queue;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					yield break;
				}
				queue = new Queue<TSource>();
				queue.Enqueue(enumerator.Current);
				while (enumerator.MoveNext())
				{
					if (queue.Count >= count)
					{
						do
						{
							queue.Dequeue();
							queue.Enqueue(enumerator.Current);
						}
						while (enumerator.MoveNext());
						break;
					}
					queue.Enqueue(enumerator.Current);
				}
			}
			do
			{
				yield return queue.Dequeue();
			}
			while (queue.Count > 0);
			yield break;
		}

		/// <summary>Creates an array from a <see cref="T:System.Collections.Generic.IEnumerable`1" />.</summary>
		/// <returns>An array that contains the elements from the input sequence.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create an array from.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x06000652 RID: 1618 RVA: 0x00014998 File Offset: 0x00012B98
		public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IIListProvider<TSource> iilistProvider;
			if ((iilistProvider = source as IIListProvider<TSource>) == null)
			{
				return EnumerableHelpers.ToArray<TSource>(source);
			}
			return iilistProvider.ToArray();
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.List`1" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.List`1" /> that contains elements from the input sequence.</returns>
		/// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Collections.Generic.List`1" /> from.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		// Token: 0x06000653 RID: 1619 RVA: 0x000149CC File Offset: 0x00012BCC
		public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IIListProvider<TSource> iilistProvider;
			if ((iilistProvider = source as IIListProvider<TSource>) == null)
			{
				return new List<TSource>(source);
			}
			return iilistProvider.ToList();
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to a specified key selector function.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains keys and values.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.-or-<paramref name="keySelector" /> produces a key that is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="keySelector" /> produces duplicate keys for two elements.</exception>
		// Token: 0x06000654 RID: 1620 RVA: 0x000149FE File Offset: 0x00012BFE
		public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ToDictionary(keySelector, null);
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to a specified key selector function and key comparer.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains keys and values.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the keys returned by <paramref name="keySelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> is null.-or-<paramref name="keySelector" /> produces a key that is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="keySelector" /> produces duplicate keys for two elements.</exception>
		// Token: 0x06000655 RID: 1621 RVA: 0x00014A08 File Offset: 0x00012C08
		public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			int num = 0;
			ICollection<TSource> collection;
			if ((collection = source as ICollection<TSource>) != null)
			{
				num = collection.Count;
				if (num == 0)
				{
					return new Dictionary<TKey, TSource>(comparer);
				}
				TSource[] array;
				if ((array = collection as TSource[]) != null)
				{
					return Enumerable.ToDictionary<TSource, TKey>(array, keySelector, comparer);
				}
				List<TSource> list;
				if ((list = collection as List<TSource>) != null)
				{
					return Enumerable.ToDictionary<TSource, TKey>(list, keySelector, comparer);
				}
			}
			Dictionary<TKey, TSource> dictionary = new Dictionary<TKey, TSource>(num, comparer);
			foreach (TSource tsource in source)
			{
				dictionary.Add(keySelector(tsource), tsource);
			}
			return dictionary;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00014ACC File Offset: 0x00012CCC
		private static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(TSource[] source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Dictionary<TKey, TSource> dictionary = new Dictionary<TKey, TSource>(source.Length, comparer);
			for (int i = 0; i < source.Length; i++)
			{
				dictionary.Add(keySelector(source[i]), source[i]);
			}
			return dictionary;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00014B0C File Offset: 0x00012D0C
		private static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(List<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Dictionary<TKey, TSource> dictionary = new Dictionary<TKey, TSource>(source.Count, comparer);
			foreach (TSource tsource in source)
			{
				dictionary.Add(keySelector(tsource), tsource);
			}
			return dictionary;
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to specified key selector and element selector functions.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains values of type <paramref name="TElement" /> selected from the input sequence.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is null.-or-<paramref name="keySelector" /> produces a key that is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="keySelector" /> produces duplicate keys for two elements.</exception>
		// Token: 0x06000658 RID: 1624 RVA: 0x00014B70 File Offset: 0x00012D70
		public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return source.ToDictionary(keySelector, elementSelector, null);
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to a specified key selector function, a comparer, and an element selector function.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains values of type <paramref name="TElement" /> selected from the input sequence.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is null.-or-<paramref name="keySelector" /> produces a key that is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="keySelector" /> produces duplicate keys for two elements.</exception>
		// Token: 0x06000659 RID: 1625 RVA: 0x00014B7C File Offset: 0x00012D7C
		public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			if (elementSelector == null)
			{
				throw Error.ArgumentNull("elementSelector");
			}
			int num = 0;
			ICollection<TSource> collection;
			if ((collection = source as ICollection<TSource>) != null)
			{
				num = collection.Count;
				if (num == 0)
				{
					return new Dictionary<TKey, TElement>(comparer);
				}
				TSource[] array;
				if ((array = collection as TSource[]) != null)
				{
					return Enumerable.ToDictionary<TSource, TKey, TElement>(array, keySelector, elementSelector, comparer);
				}
				List<TSource> list;
				if ((list = collection as List<TSource>) != null)
				{
					return Enumerable.ToDictionary<TSource, TKey, TElement>(list, keySelector, elementSelector, comparer);
				}
			}
			Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>(num, comparer);
			foreach (TSource tsource in source)
			{
				dictionary.Add(keySelector(tsource), elementSelector(tsource));
			}
			return dictionary;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00014C54 File Offset: 0x00012E54
		private static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(TSource[] source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>(source.Length, comparer);
			for (int i = 0; i < source.Length; i++)
			{
				dictionary.Add(keySelector(source[i]), elementSelector(source[i]));
			}
			return dictionary;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00014C9C File Offset: 0x00012E9C
		private static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(List<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>(source.Count, comparer);
			foreach (TSource tsource in source)
			{
				dictionary.Add(keySelector(tsource), elementSelector(tsource));
			}
			return dictionary;
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00014D08 File Offset: 0x00012F08
		public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source)
		{
			return source.ToHashSet(null);
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00014D11 File Offset: 0x00012F11
		public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return new HashSet<TSource>(source, comparer);
		}

		/// <summary>Produces the set union of two sequences by using the default equality comparer.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements from both input sequences, excluding duplicates.</returns>
		/// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements form the first set for the union.</param>
		/// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements form the second set for the union.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is null.</exception>
		// Token: 0x0600065E RID: 1630 RVA: 0x00014D28 File Offset: 0x00012F28
		public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			return first.Union(second, null);
		}

		/// <summary>Produces the set union of two sequences by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements from both input sequences, excluding duplicates.</returns>
		/// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements form the first set for the union.</param>
		/// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements form the second set for the union.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is null.</exception>
		// Token: 0x0600065F RID: 1631 RVA: 0x00014D34 File Offset: 0x00012F34
		public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			Enumerable.UnionIterator<TSource> unionIterator;
			if ((unionIterator = first as Enumerable.UnionIterator<TSource>) == null || !Utilities.AreEqualityComparersEqual<TSource>(comparer, unionIterator._comparer))
			{
				return new Enumerable.UnionIterator2<TSource>(first, second, comparer);
			}
			return unionIterator.Union(second);
		}

		/// <summary>Filters a sequence of values based on a predicate.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains elements from the input sequence that satisfy the condition.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to filter.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x06000660 RID: 1632 RVA: 0x00014D88 File Offset: 0x00012F88
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			Enumerable.Iterator<TSource> iterator;
			if ((iterator = source as Enumerable.Iterator<TSource>) != null)
			{
				return iterator.Where(predicate);
			}
			TSource[] array;
			if ((array = source as TSource[]) != null)
			{
				if (array.Length != 0)
				{
					return new Enumerable.WhereArrayIterator<TSource>(array, predicate);
				}
				return EmptyPartition<TSource>.Instance;
			}
			else
			{
				List<TSource> list;
				if ((list = source as List<TSource>) != null)
				{
					return new Enumerable.WhereListIterator<TSource>(list, predicate);
				}
				return new Enumerable.WhereEnumerableIterator<TSource>(source, predicate);
			}
		}

		/// <summary>Filters a sequence of values based on a predicate. Each element's index is used in the logic of the predicate function.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains elements from the input sequence that satisfy the condition.</returns>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to filter.</param>
		/// <param name="predicate">A function to test each source element for a condition; the second parameter of the function represents the index of the source element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
		// Token: 0x06000661 RID: 1633 RVA: 0x00014DFC File Offset: 0x00012FFC
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return Enumerable.WhereIterator<TSource>(source, predicate);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00014E21 File Offset: 0x00013021
		private static IEnumerable<TSource> WhereIterator<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			int index = -1;
			foreach (TSource tsource in source)
			{
				int num = index;
				index = checked(num + 1);
				if (predicate(tsource, index))
				{
					yield return tsource;
				}
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		/// <summary>Applies a specified function to the corresponding elements of two sequences, producing a sequence of the results.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains merged elements of two input sequences.</returns>
		/// <param name="first">The first sequence to merge.</param>
		/// <param name="second">The second sequence to merge.</param>
		/// <param name="resultSelector">A function that specifies how to merge the elements from the two sequences.</param>
		/// <typeparam name="TFirst">The type of the elements of the first input sequence.</typeparam>
		/// <typeparam name="TSecond">The type of the elements of the second input sequence.</typeparam>
		/// <typeparam name="TResult">The type of the elements of the result sequence.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="first" /> or <paramref name="second" /> is null.</exception>
		// Token: 0x06000663 RID: 1635 RVA: 0x00014E38 File Offset: 0x00013038
		public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
		{
			if (first == null)
			{
				throw Error.ArgumentNull("first");
			}
			if (second == null)
			{
				throw Error.ArgumentNull("second");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return Enumerable.ZipIterator<TFirst, TSecond, TResult>(first, second, resultSelector);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00014E6C File Offset: 0x0001306C
		private static IEnumerable<TResult> ZipIterator<TFirst, TSecond, TResult>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
		{
			using (IEnumerator<TFirst> e = first.GetEnumerator())
			{
				using (IEnumerator<TSecond> e2 = second.GetEnumerator())
				{
					while (e.MoveNext() && e2.MoveNext())
					{
						yield return resultSelector(e.Current, e2.Current);
					}
				}
				IEnumerator<TSecond> e2 = null;
			}
			IEnumerator<TFirst> e = null;
			yield break;
			yield break;
		}

		// Token: 0x020000B1 RID: 177
		private abstract class AppendPrependIterator<TSource> : Enumerable.Iterator<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x06000665 RID: 1637 RVA: 0x00014E8A File Offset: 0x0001308A
			protected AppendPrependIterator(IEnumerable<TSource> source)
			{
				this._source = source;
			}

			// Token: 0x06000666 RID: 1638 RVA: 0x00014E99 File Offset: 0x00013099
			protected void GetSourceEnumerator()
			{
				this._enumerator = this._source.GetEnumerator();
			}

			// Token: 0x06000667 RID: 1639
			public abstract Enumerable.AppendPrependIterator<TSource> Append(TSource item);

			// Token: 0x06000668 RID: 1640
			public abstract Enumerable.AppendPrependIterator<TSource> Prepend(TSource item);

			// Token: 0x06000669 RID: 1641 RVA: 0x00014EAC File Offset: 0x000130AC
			protected bool LoadFromEnumerator()
			{
				if (this._enumerator.MoveNext())
				{
					this._current = this._enumerator.Current;
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x0600066A RID: 1642 RVA: 0x00014ED5 File Offset: 0x000130D5
			public override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x0600066B RID: 1643
			public abstract TSource[] ToArray();

			// Token: 0x0600066C RID: 1644
			public abstract List<TSource> ToList();

			// Token: 0x0600066D RID: 1645
			public abstract int GetCount(bool onlyIfCheap);

			// Token: 0x040003C7 RID: 967
			protected readonly IEnumerable<TSource> _source;

			// Token: 0x040003C8 RID: 968
			protected IEnumerator<TSource> _enumerator;
		}

		// Token: 0x020000B2 RID: 178
		private class AppendPrepend1Iterator<TSource> : Enumerable.AppendPrependIterator<TSource>
		{
			// Token: 0x0600066E RID: 1646 RVA: 0x00014EF7 File Offset: 0x000130F7
			public AppendPrepend1Iterator(IEnumerable<TSource> source, TSource item, bool appending)
				: base(source)
			{
				this._item = item;
				this._appending = appending;
			}

			// Token: 0x0600066F RID: 1647 RVA: 0x00014F0E File Offset: 0x0001310E
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.AppendPrepend1Iterator<TSource>(this._source, this._item, this._appending);
			}

			// Token: 0x06000670 RID: 1648 RVA: 0x00014F28 File Offset: 0x00013128
			public override bool MoveNext()
			{
				switch (this._state)
				{
				case 1:
					this._state = 2;
					if (!this._appending)
					{
						this._current = this._item;
						return true;
					}
					break;
				case 2:
					break;
				case 3:
					goto IL_0047;
				default:
					goto IL_0067;
				}
				base.GetSourceEnumerator();
				this._state = 3;
				IL_0047:
				if (base.LoadFromEnumerator())
				{
					return true;
				}
				if (this._appending)
				{
					this._current = this._item;
					return true;
				}
				IL_0067:
				this.Dispose();
				return false;
			}

			// Token: 0x06000671 RID: 1649 RVA: 0x00014FA4 File Offset: 0x000131A4
			public override Enumerable.AppendPrependIterator<TSource> Append(TSource item)
			{
				if (this._appending)
				{
					return new Enumerable.AppendPrependN<TSource>(this._source, null, new SingleLinkedNode<TSource>(this._item).Add(item), 0, 2);
				}
				return new Enumerable.AppendPrependN<TSource>(this._source, new SingleLinkedNode<TSource>(this._item), new SingleLinkedNode<TSource>(item), 1, 1);
			}

			// Token: 0x06000672 RID: 1650 RVA: 0x00014FF8 File Offset: 0x000131F8
			public override Enumerable.AppendPrependIterator<TSource> Prepend(TSource item)
			{
				if (this._appending)
				{
					return new Enumerable.AppendPrependN<TSource>(this._source, new SingleLinkedNode<TSource>(item), new SingleLinkedNode<TSource>(this._item), 1, 1);
				}
				return new Enumerable.AppendPrependN<TSource>(this._source, new SingleLinkedNode<TSource>(this._item).Add(item), null, 2, 0);
			}

			// Token: 0x06000673 RID: 1651 RVA: 0x0001504C File Offset: 0x0001324C
			private TSource[] LazyToArray()
			{
				LargeArrayBuilder<TSource> largeArrayBuilder = new LargeArrayBuilder<TSource>(true);
				if (!this._appending)
				{
					largeArrayBuilder.SlowAdd(this._item);
				}
				largeArrayBuilder.AddRange(this._source);
				if (this._appending)
				{
					largeArrayBuilder.SlowAdd(this._item);
				}
				return largeArrayBuilder.ToArray();
			}

			// Token: 0x06000674 RID: 1652 RVA: 0x000150A0 File Offset: 0x000132A0
			public override TSource[] ToArray()
			{
				int count = this.GetCount(true);
				if (count == -1)
				{
					return this.LazyToArray();
				}
				TSource[] array = new TSource[count];
				int num;
				if (this._appending)
				{
					num = 0;
				}
				else
				{
					array[0] = this._item;
					num = 1;
				}
				EnumerableHelpers.Copy<TSource>(this._source, array, num, count - 1);
				if (this._appending)
				{
					array[array.Length - 1] = this._item;
				}
				return array;
			}

			// Token: 0x06000675 RID: 1653 RVA: 0x0001510C File Offset: 0x0001330C
			public override List<TSource> ToList()
			{
				int count = this.GetCount(true);
				List<TSource> list = ((count == -1) ? new List<TSource>() : new List<TSource>(count));
				if (!this._appending)
				{
					list.Add(this._item);
				}
				list.AddRange(this._source);
				if (this._appending)
				{
					list.Add(this._item);
				}
				return list;
			}

			// Token: 0x06000676 RID: 1654 RVA: 0x00015168 File Offset: 0x00013368
			public override int GetCount(bool onlyIfCheap)
			{
				IIListProvider<TSource> iilistProvider;
				if ((iilistProvider = this._source as IIListProvider<TSource>) != null)
				{
					int count = iilistProvider.GetCount(onlyIfCheap);
					if (count != -1)
					{
						return count + 1;
					}
					return -1;
				}
				else
				{
					if (onlyIfCheap && !(this._source is ICollection<TSource>))
					{
						return -1;
					}
					return this._source.Count<TSource>() + 1;
				}
			}

			// Token: 0x040003C9 RID: 969
			private readonly TSource _item;

			// Token: 0x040003CA RID: 970
			private readonly bool _appending;
		}

		// Token: 0x020000B3 RID: 179
		private class AppendPrependN<TSource> : Enumerable.AppendPrependIterator<TSource>
		{
			// Token: 0x06000677 RID: 1655 RVA: 0x000151B5 File Offset: 0x000133B5
			public AppendPrependN(IEnumerable<TSource> source, SingleLinkedNode<TSource> prepended, SingleLinkedNode<TSource> appended, int prependCount, int appendCount)
				: base(source)
			{
				this._prepended = prepended;
				this._appended = appended;
				this._prependCount = prependCount;
				this._appendCount = appendCount;
			}

			// Token: 0x06000678 RID: 1656 RVA: 0x000151DC File Offset: 0x000133DC
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.AppendPrependN<TSource>(this._source, this._prepended, this._appended, this._prependCount, this._appendCount);
			}

			// Token: 0x06000679 RID: 1657 RVA: 0x00015204 File Offset: 0x00013404
			public override bool MoveNext()
			{
				switch (this._state)
				{
				case 1:
					this._node = this._prepended;
					this._state = 2;
					break;
				case 2:
					break;
				case 3:
					goto IL_0070;
				case 4:
					goto IL_00A2;
				default:
					this.Dispose();
					return false;
				}
				if (this._node != null)
				{
					this._current = this._node.Item;
					this._node = this._node.Linked;
					return true;
				}
				base.GetSourceEnumerator();
				this._state = 3;
				IL_0070:
				if (base.LoadFromEnumerator())
				{
					return true;
				}
				if (this._appended == null)
				{
					return false;
				}
				this._enumerator = this._appended.GetEnumerator(this._appendCount);
				this._state = 4;
				IL_00A2:
				return base.LoadFromEnumerator();
			}

			// Token: 0x0600067A RID: 1658 RVA: 0x000152C4 File Offset: 0x000134C4
			public override Enumerable.AppendPrependIterator<TSource> Append(TSource item)
			{
				SingleLinkedNode<TSource> singleLinkedNode = ((this._appended != null) ? this._appended.Add(item) : new SingleLinkedNode<TSource>(item));
				return new Enumerable.AppendPrependN<TSource>(this._source, this._prepended, singleLinkedNode, this._prependCount, this._appendCount + 1);
			}

			// Token: 0x0600067B RID: 1659 RVA: 0x00015310 File Offset: 0x00013510
			public override Enumerable.AppendPrependIterator<TSource> Prepend(TSource item)
			{
				SingleLinkedNode<TSource> singleLinkedNode = ((this._prepended != null) ? this._prepended.Add(item) : new SingleLinkedNode<TSource>(item));
				return new Enumerable.AppendPrependN<TSource>(this._source, singleLinkedNode, this._appended, this._prependCount + 1, this._appendCount);
			}

			// Token: 0x0600067C RID: 1660 RVA: 0x0001535C File Offset: 0x0001355C
			private TSource[] LazyToArray()
			{
				SparseArrayBuilder<TSource> sparseArrayBuilder = new SparseArrayBuilder<TSource>(true);
				if (this._prepended != null)
				{
					sparseArrayBuilder.Reserve(this._prependCount);
				}
				sparseArrayBuilder.AddRange(this._source);
				if (this._appended != null)
				{
					sparseArrayBuilder.Reserve(this._appendCount);
				}
				TSource[] array = sparseArrayBuilder.ToArray();
				int num = 0;
				for (SingleLinkedNode<TSource> singleLinkedNode = this._prepended; singleLinkedNode != null; singleLinkedNode = singleLinkedNode.Linked)
				{
					array[num++] = singleLinkedNode.Item;
				}
				num = array.Length - 1;
				for (SingleLinkedNode<TSource> singleLinkedNode2 = this._appended; singleLinkedNode2 != null; singleLinkedNode2 = singleLinkedNode2.Linked)
				{
					array[num--] = singleLinkedNode2.Item;
				}
				return array;
			}

			// Token: 0x0600067D RID: 1661 RVA: 0x00015408 File Offset: 0x00013608
			public override TSource[] ToArray()
			{
				int count = this.GetCount(true);
				if (count == -1)
				{
					return this.LazyToArray();
				}
				TSource[] array = new TSource[count];
				int num = 0;
				for (SingleLinkedNode<TSource> singleLinkedNode = this._prepended; singleLinkedNode != null; singleLinkedNode = singleLinkedNode.Linked)
				{
					array[num] = singleLinkedNode.Item;
					num++;
				}
				ICollection<TSource> collection;
				if ((collection = this._source as ICollection<TSource>) != null)
				{
					collection.CopyTo(array, num);
				}
				else
				{
					foreach (TSource tsource in this._source)
					{
						array[num] = tsource;
						num++;
					}
				}
				num = array.Length;
				for (SingleLinkedNode<TSource> singleLinkedNode2 = this._appended; singleLinkedNode2 != null; singleLinkedNode2 = singleLinkedNode2.Linked)
				{
					num--;
					array[num] = singleLinkedNode2.Item;
				}
				return array;
			}

			// Token: 0x0600067E RID: 1662 RVA: 0x000154F0 File Offset: 0x000136F0
			public override List<TSource> ToList()
			{
				int count = this.GetCount(true);
				List<TSource> list = ((count == -1) ? new List<TSource>() : new List<TSource>(count));
				for (SingleLinkedNode<TSource> singleLinkedNode = this._prepended; singleLinkedNode != null; singleLinkedNode = singleLinkedNode.Linked)
				{
					list.Add(singleLinkedNode.Item);
				}
				list.AddRange(this._source);
				if (this._appended != null)
				{
					IEnumerator<TSource> enumerator = this._appended.GetEnumerator(this._appendCount);
					while (enumerator.MoveNext())
					{
						TSource tsource = enumerator.Current;
						list.Add(tsource);
					}
				}
				return list;
			}

			// Token: 0x0600067F RID: 1663 RVA: 0x00015574 File Offset: 0x00013774
			public override int GetCount(bool onlyIfCheap)
			{
				IIListProvider<TSource> iilistProvider;
				if ((iilistProvider = this._source as IIListProvider<TSource>) != null)
				{
					int count = iilistProvider.GetCount(onlyIfCheap);
					if (count != -1)
					{
						return count + this._appendCount + this._prependCount;
					}
					return -1;
				}
				else
				{
					if (onlyIfCheap && !(this._source is ICollection<TSource>))
					{
						return -1;
					}
					return this._source.Count<TSource>() + this._appendCount + this._prependCount;
				}
			}

			// Token: 0x040003CB RID: 971
			private readonly SingleLinkedNode<TSource> _prepended;

			// Token: 0x040003CC RID: 972
			private readonly SingleLinkedNode<TSource> _appended;

			// Token: 0x040003CD RID: 973
			private readonly int _prependCount;

			// Token: 0x040003CE RID: 974
			private readonly int _appendCount;

			// Token: 0x040003CF RID: 975
			private SingleLinkedNode<TSource> _node;
		}

		// Token: 0x020000B4 RID: 180
		private sealed class Concat2Iterator<TSource> : Enumerable.ConcatIterator<TSource>
		{
			// Token: 0x06000680 RID: 1664 RVA: 0x000155D9 File Offset: 0x000137D9
			internal Concat2Iterator(IEnumerable<TSource> first, IEnumerable<TSource> second)
			{
				this._first = first;
				this._second = second;
			}

			// Token: 0x06000681 RID: 1665 RVA: 0x000155EF File Offset: 0x000137EF
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.Concat2Iterator<TSource>(this._first, this._second);
			}

			// Token: 0x06000682 RID: 1666 RVA: 0x00015604 File Offset: 0x00013804
			internal override Enumerable.ConcatIterator<TSource> Concat(IEnumerable<TSource> next)
			{
				bool flag = next is ICollection<TSource> && this._first is ICollection<TSource> && this._second is ICollection<TSource>;
				return new Enumerable.ConcatNIterator<TSource>(this, next, 2, flag);
			}

			// Token: 0x06000683 RID: 1667 RVA: 0x00015644 File Offset: 0x00013844
			public override int GetCount(bool onlyIfCheap)
			{
				int num;
				if (!EnumerableHelpers.TryGetCount<TSource>(this._first, out num))
				{
					if (onlyIfCheap)
					{
						return -1;
					}
					num = this._first.Count<TSource>();
				}
				int num2;
				if (!EnumerableHelpers.TryGetCount<TSource>(this._second, out num2))
				{
					if (onlyIfCheap)
					{
						return -1;
					}
					num2 = this._second.Count<TSource>();
				}
				return checked(num + num2);
			}

			// Token: 0x06000684 RID: 1668 RVA: 0x00015694 File Offset: 0x00013894
			internal override IEnumerable<TSource> GetEnumerable(int index)
			{
				if (index == 0)
				{
					return this._first;
				}
				if (index != 1)
				{
					return null;
				}
				return this._second;
			}

			// Token: 0x06000685 RID: 1669 RVA: 0x000156B0 File Offset: 0x000138B0
			public override TSource[] ToArray()
			{
				SparseArrayBuilder<TSource> sparseArrayBuilder = new SparseArrayBuilder<TSource>(true);
				bool flag = sparseArrayBuilder.ReserveOrAdd(this._first);
				bool flag2 = sparseArrayBuilder.ReserveOrAdd(this._second);
				TSource[] array = sparseArrayBuilder.ToArray();
				if (flag)
				{
					Marker marker = sparseArrayBuilder.Markers.First();
					EnumerableHelpers.Copy<TSource>(this._first, array, 0, marker.Count);
				}
				if (flag2)
				{
					Marker marker2 = sparseArrayBuilder.Markers.Last();
					EnumerableHelpers.Copy<TSource>(this._second, array, marker2.Index, marker2.Count);
				}
				return array;
			}

			// Token: 0x040003D0 RID: 976
			internal readonly IEnumerable<TSource> _first;

			// Token: 0x040003D1 RID: 977
			internal readonly IEnumerable<TSource> _second;
		}

		// Token: 0x020000B5 RID: 181
		private sealed class ConcatNIterator<TSource> : Enumerable.ConcatIterator<TSource>
		{
			// Token: 0x06000686 RID: 1670 RVA: 0x0001573F File Offset: 0x0001393F
			internal ConcatNIterator(Enumerable.ConcatIterator<TSource> tail, IEnumerable<TSource> head, int headIndex, bool hasOnlyCollections)
			{
				this._tail = tail;
				this._head = head;
				this._headIndex = headIndex;
				this._hasOnlyCollections = hasOnlyCollections;
			}

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x06000687 RID: 1671 RVA: 0x00015764 File Offset: 0x00013964
			private Enumerable.ConcatNIterator<TSource> PreviousN
			{
				get
				{
					return this._tail as Enumerable.ConcatNIterator<TSource>;
				}
			}

			// Token: 0x06000688 RID: 1672 RVA: 0x00015771 File Offset: 0x00013971
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.ConcatNIterator<TSource>(this._tail, this._head, this._headIndex, this._hasOnlyCollections);
			}

			// Token: 0x06000689 RID: 1673 RVA: 0x00015790 File Offset: 0x00013990
			internal override Enumerable.ConcatIterator<TSource> Concat(IEnumerable<TSource> next)
			{
				if (this._headIndex == 2147483645)
				{
					return new Enumerable.Concat2Iterator<TSource>(this, next);
				}
				bool flag = this._hasOnlyCollections && next is ICollection<TSource>;
				return new Enumerable.ConcatNIterator<TSource>(this, next, this._headIndex + 1, flag);
			}

			// Token: 0x0600068A RID: 1674 RVA: 0x000157D8 File Offset: 0x000139D8
			public override int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap && !this._hasOnlyCollections)
				{
					return -1;
				}
				int num = 0;
				Enumerable.ConcatNIterator<TSource> concatNIterator = this;
				checked
				{
					Enumerable.ConcatNIterator<TSource> concatNIterator2;
					do
					{
						concatNIterator2 = concatNIterator;
						IEnumerable<TSource> head = concatNIterator2._head;
						ICollection<TSource> collection = head as ICollection<TSource>;
						int num2 = ((collection != null) ? collection.Count : head.Count<TSource>());
						num += num2;
					}
					while ((concatNIterator = concatNIterator2.PreviousN) != null);
					return num + concatNIterator2._tail.GetCount(onlyIfCheap);
				}
			}

			// Token: 0x0600068B RID: 1675 RVA: 0x00015838 File Offset: 0x00013A38
			internal override IEnumerable<TSource> GetEnumerable(int index)
			{
				if (index > this._headIndex)
				{
					return null;
				}
				Enumerable.ConcatNIterator<TSource> concatNIterator = this;
				Enumerable.ConcatNIterator<TSource> concatNIterator2;
				for (;;)
				{
					concatNIterator2 = concatNIterator;
					if (index == concatNIterator2._headIndex)
					{
						break;
					}
					if ((concatNIterator = concatNIterator2.PreviousN) == null)
					{
						goto Block_3;
					}
				}
				return concatNIterator2._head;
				Block_3:
				return concatNIterator2._tail.GetEnumerable(index);
			}

			// Token: 0x0600068C RID: 1676 RVA: 0x0001587A File Offset: 0x00013A7A
			public override TSource[] ToArray()
			{
				if (!this._hasOnlyCollections)
				{
					return this.LazyToArray();
				}
				return this.PreallocatingToArray();
			}

			// Token: 0x0600068D RID: 1677 RVA: 0x00015894 File Offset: 0x00013A94
			private TSource[] LazyToArray()
			{
				SparseArrayBuilder<TSource> sparseArrayBuilder = new SparseArrayBuilder<TSource>(true);
				ArrayBuilder<int> arrayBuilder = default(ArrayBuilder<int>);
				int num = 0;
				for (;;)
				{
					IEnumerable<TSource> enumerable = this.GetEnumerable(num);
					if (enumerable == null)
					{
						break;
					}
					if (sparseArrayBuilder.ReserveOrAdd(enumerable))
					{
						arrayBuilder.Add(num);
					}
					num++;
				}
				TSource[] array = sparseArrayBuilder.ToArray();
				ArrayBuilder<Marker> markers = sparseArrayBuilder.Markers;
				for (int i = 0; i < markers.Count; i++)
				{
					Marker marker = markers[i];
					EnumerableHelpers.Copy<TSource>(this.GetEnumerable(arrayBuilder[i]), array, marker.Index, marker.Count);
				}
				return array;
			}

			// Token: 0x0600068E RID: 1678 RVA: 0x00015934 File Offset: 0x00013B34
			private TSource[] PreallocatingToArray()
			{
				int count = this.GetCount(true);
				if (count == 0)
				{
					return Array.Empty<TSource>();
				}
				TSource[] array = new TSource[count];
				int num = array.Length;
				Enumerable.ConcatNIterator<TSource> concatNIterator = this;
				checked
				{
					Enumerable.ConcatNIterator<TSource> concatNIterator2;
					do
					{
						concatNIterator2 = concatNIterator;
						ICollection<TSource> collection = (ICollection<TSource>)concatNIterator2._head;
						int count2 = collection.Count;
						if (count2 > 0)
						{
							num -= count2;
							collection.CopyTo(array, num);
						}
					}
					while ((concatNIterator = concatNIterator2.PreviousN) != null);
					Enumerable.Concat2Iterator<TSource> concat2Iterator = (Enumerable.Concat2Iterator<TSource>)concatNIterator2._tail;
					ICollection<TSource> collection2 = (ICollection<TSource>)concat2Iterator._second;
					int count3 = collection2.Count;
					if (count3 > 0)
					{
						collection2.CopyTo(array, num - count3);
					}
					if (num > count3)
					{
						((ICollection<TSource>)concat2Iterator._first).CopyTo(array, 0);
					}
					return array;
				}
			}

			// Token: 0x040003D2 RID: 978
			private readonly Enumerable.ConcatIterator<TSource> _tail;

			// Token: 0x040003D3 RID: 979
			private readonly IEnumerable<TSource> _head;

			// Token: 0x040003D4 RID: 980
			private readonly int _headIndex;

			// Token: 0x040003D5 RID: 981
			private readonly bool _hasOnlyCollections;
		}

		// Token: 0x020000B6 RID: 182
		private abstract class ConcatIterator<TSource> : Enumerable.Iterator<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x0600068F RID: 1679 RVA: 0x000159E5 File Offset: 0x00013BE5
			public override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x06000690 RID: 1680
			internal abstract IEnumerable<TSource> GetEnumerable(int index);

			// Token: 0x06000691 RID: 1681
			internal abstract Enumerable.ConcatIterator<TSource> Concat(IEnumerable<TSource> next);

			// Token: 0x06000692 RID: 1682 RVA: 0x00015A08 File Offset: 0x00013C08
			public override bool MoveNext()
			{
				if (this._state == 1)
				{
					this._enumerator = this.GetEnumerable(0).GetEnumerator();
					this._state = 2;
				}
				if (this._state > 1)
				{
					while (!this._enumerator.MoveNext())
					{
						int state = this._state;
						this._state = state + 1;
						IEnumerable<TSource> enumerable = this.GetEnumerable(state - 1);
						if (enumerable == null)
						{
							this.Dispose();
							return false;
						}
						this._enumerator.Dispose();
						this._enumerator = enumerable.GetEnumerator();
					}
					this._current = this._enumerator.Current;
					return true;
				}
				return false;
			}

			// Token: 0x06000693 RID: 1683
			public abstract int GetCount(bool onlyIfCheap);

			// Token: 0x06000694 RID: 1684
			public abstract TSource[] ToArray();

			// Token: 0x06000695 RID: 1685 RVA: 0x00015AA0 File Offset: 0x00013CA0
			public List<TSource> ToList()
			{
				int count = this.GetCount(true);
				List<TSource> list = ((count != -1) ? new List<TSource>(count) : new List<TSource>());
				int num = 0;
				for (;;)
				{
					IEnumerable<TSource> enumerable = this.GetEnumerable(num);
					if (enumerable == null)
					{
						break;
					}
					list.AddRange(enumerable);
					num++;
				}
				return list;
			}

			// Token: 0x040003D6 RID: 982
			private IEnumerator<TSource> _enumerator;
		}

		// Token: 0x020000B7 RID: 183
		private sealed class DefaultIfEmptyIterator<TSource> : Enumerable.Iterator<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x06000697 RID: 1687 RVA: 0x00015AEA File Offset: 0x00013CEA
			public DefaultIfEmptyIterator(IEnumerable<TSource> source, TSource defaultValue)
			{
				this._source = source;
				this._default = defaultValue;
			}

			// Token: 0x06000698 RID: 1688 RVA: 0x00015B00 File Offset: 0x00013D00
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.DefaultIfEmptyIterator<TSource>(this._source, this._default);
			}

			// Token: 0x06000699 RID: 1689 RVA: 0x00015B14 File Offset: 0x00013D14
			public override bool MoveNext()
			{
				int state = this._state;
				if (state != 1)
				{
					if (state == 2)
					{
						if (this._enumerator.MoveNext())
						{
							this._current = this._enumerator.Current;
							return true;
						}
					}
					this.Dispose();
					return false;
				}
				this._enumerator = this._source.GetEnumerator();
				if (this._enumerator.MoveNext())
				{
					this._current = this._enumerator.Current;
					this._state = 2;
				}
				else
				{
					this._current = this._default;
					this._state = -1;
				}
				return true;
			}

			// Token: 0x0600069A RID: 1690 RVA: 0x00015BA6 File Offset: 0x00013DA6
			public override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x0600069B RID: 1691 RVA: 0x00015BC8 File Offset: 0x00013DC8
			public TSource[] ToArray()
			{
				TSource[] array = this._source.ToArray<TSource>();
				if (array.Length != 0)
				{
					return array;
				}
				return new TSource[] { this._default };
			}

			// Token: 0x0600069C RID: 1692 RVA: 0x00015BFC File Offset: 0x00013DFC
			public List<TSource> ToList()
			{
				List<TSource> list = this._source.ToList<TSource>();
				if (list.Count == 0)
				{
					list.Add(this._default);
				}
				return list;
			}

			// Token: 0x0600069D RID: 1693 RVA: 0x00015C2C File Offset: 0x00013E2C
			public int GetCount(bool onlyIfCheap)
			{
				int num;
				if (!onlyIfCheap || this._source is ICollection<TSource> || this._source is ICollection)
				{
					num = this._source.Count<TSource>();
				}
				else
				{
					IIListProvider<TSource> iilistProvider;
					num = (((iilistProvider = this._source as IIListProvider<TSource>) != null) ? iilistProvider.GetCount(true) : (-1));
				}
				if (num != 0)
				{
					return num;
				}
				return 1;
			}

			// Token: 0x040003D7 RID: 983
			private readonly IEnumerable<TSource> _source;

			// Token: 0x040003D8 RID: 984
			private readonly TSource _default;

			// Token: 0x040003D9 RID: 985
			private IEnumerator<TSource> _enumerator;
		}

		// Token: 0x020000B8 RID: 184
		private sealed class DistinctIterator<TSource> : Enumerable.Iterator<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x0600069E RID: 1694 RVA: 0x00015C84 File Offset: 0x00013E84
			public DistinctIterator(IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
			{
				this._source = source;
				this._comparer = comparer;
			}

			// Token: 0x0600069F RID: 1695 RVA: 0x00015C9A File Offset: 0x00013E9A
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.DistinctIterator<TSource>(this._source, this._comparer);
			}

			// Token: 0x060006A0 RID: 1696 RVA: 0x00015CB0 File Offset: 0x00013EB0
			public override bool MoveNext()
			{
				int state = this._state;
				TSource tsource;
				if (state != 1)
				{
					if (state == 2)
					{
						while (this._enumerator.MoveNext())
						{
							tsource = this._enumerator.Current;
							if (this._set.Add(tsource))
							{
								this._current = tsource;
								return true;
							}
						}
					}
					this.Dispose();
					return false;
				}
				this._enumerator = this._source.GetEnumerator();
				if (!this._enumerator.MoveNext())
				{
					this.Dispose();
					return false;
				}
				tsource = this._enumerator.Current;
				this._set = new Set<TSource>(this._comparer);
				this._set.Add(tsource);
				this._current = tsource;
				this._state = 2;
				return true;
			}

			// Token: 0x060006A1 RID: 1697 RVA: 0x00015D6B File Offset: 0x00013F6B
			public override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
					this._set = null;
				}
				base.Dispose();
			}

			// Token: 0x060006A2 RID: 1698 RVA: 0x00015D94 File Offset: 0x00013F94
			private Set<TSource> FillSet()
			{
				Set<TSource> set = new Set<TSource>(this._comparer);
				set.UnionWith(this._source);
				return set;
			}

			// Token: 0x060006A3 RID: 1699 RVA: 0x00015DAD File Offset: 0x00013FAD
			public TSource[] ToArray()
			{
				return this.FillSet().ToArray();
			}

			// Token: 0x060006A4 RID: 1700 RVA: 0x00015DBA File Offset: 0x00013FBA
			public List<TSource> ToList()
			{
				return this.FillSet().ToList();
			}

			// Token: 0x060006A5 RID: 1701 RVA: 0x00015DC7 File Offset: 0x00013FC7
			public int GetCount(bool onlyIfCheap)
			{
				if (!onlyIfCheap)
				{
					return this.FillSet().Count;
				}
				return -1;
			}

			// Token: 0x040003DA RID: 986
			private readonly IEnumerable<TSource> _source;

			// Token: 0x040003DB RID: 987
			private readonly IEqualityComparer<TSource> _comparer;

			// Token: 0x040003DC RID: 988
			private Set<TSource> _set;

			// Token: 0x040003DD RID: 989
			private IEnumerator<TSource> _enumerator;
		}

		// Token: 0x020000B9 RID: 185
		internal abstract class Iterator<TSource> : IEnumerable<TSource>, IEnumerable, IEnumerator<TSource>, IDisposable, IEnumerator
		{
			// Token: 0x060006A6 RID: 1702 RVA: 0x00015DD9 File Offset: 0x00013FD9
			protected Iterator()
			{
				this._threadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00015DEC File Offset: 0x00013FEC
			public TSource Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x060006A8 RID: 1704
			public abstract Enumerable.Iterator<TSource> Clone();

			// Token: 0x060006A9 RID: 1705 RVA: 0x00015DF4 File Offset: 0x00013FF4
			public virtual void Dispose()
			{
				this._current = default(TSource);
				this._state = -1;
			}

			// Token: 0x060006AA RID: 1706 RVA: 0x00015E09 File Offset: 0x00014009
			public IEnumerator<TSource> GetEnumerator()
			{
				Enumerable.Iterator iterator = ((this._state == 0 && this._threadId == Environment.CurrentManagedThreadId) ? this : this.Clone());
				iterator._state = 1;
				return iterator;
			}

			// Token: 0x060006AB RID: 1707
			public abstract bool MoveNext();

			// Token: 0x060006AC RID: 1708 RVA: 0x00015E30 File Offset: 0x00014030
			public virtual IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
			{
				return new Enumerable.SelectEnumerableIterator<TSource, TResult>(this, selector);
			}

			// Token: 0x060006AD RID: 1709 RVA: 0x00015E39 File Offset: 0x00014039
			public virtual IEnumerable<TSource> Where(Func<TSource, bool> predicate)
			{
				return new Enumerable.WhereEnumerableIterator<TSource>(this, predicate);
			}

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x060006AE RID: 1710 RVA: 0x00015E42 File Offset: 0x00014042
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060006AF RID: 1711 RVA: 0x00015E4F File Offset: 0x0001404F
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060006B0 RID: 1712 RVA: 0x00015E57 File Offset: 0x00014057
			void IEnumerator.Reset()
			{
				throw Error.NotSupported();
			}

			// Token: 0x040003DE RID: 990
			private readonly int _threadId;

			// Token: 0x040003DF RID: 991
			internal int _state;

			// Token: 0x040003E0 RID: 992
			internal TSource _current;
		}

		// Token: 0x020000BA RID: 186
		private sealed class ListPartition<TSource> : Enumerable.Iterator<TSource>, IPartition<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x060006B1 RID: 1713 RVA: 0x00015E5E File Offset: 0x0001405E
			public ListPartition(IList<TSource> source, int minIndexInclusive, int maxIndexInclusive)
			{
				this._source = source;
				this._minIndexInclusive = minIndexInclusive;
				this._maxIndexInclusive = maxIndexInclusive;
			}

			// Token: 0x060006B2 RID: 1714 RVA: 0x00015E7B File Offset: 0x0001407B
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.ListPartition<TSource>(this._source, this._minIndexInclusive, this._maxIndexInclusive);
			}

			// Token: 0x060006B3 RID: 1715 RVA: 0x00015E94 File Offset: 0x00014094
			public override bool MoveNext()
			{
				int num = this._state - 1;
				if (num <= this._maxIndexInclusive - this._minIndexInclusive && num < this._source.Count - this._minIndexInclusive)
				{
					this._current = this._source[this._minIndexInclusive + num];
					this._state++;
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x060006B4 RID: 1716 RVA: 0x00015EFF File Offset: 0x000140FF
			public override IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
			{
				return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, selector, this._minIndexInclusive, this._maxIndexInclusive);
			}

			// Token: 0x060006B5 RID: 1717 RVA: 0x00015F1C File Offset: 0x0001411C
			public IPartition<TSource> Skip(int count)
			{
				int num = this._minIndexInclusive + count;
				if (num <= this._maxIndexInclusive)
				{
					return new Enumerable.ListPartition<TSource>(this._source, num, this._maxIndexInclusive);
				}
				return EmptyPartition<TSource>.Instance;
			}

			// Token: 0x060006B6 RID: 1718 RVA: 0x00015F58 File Offset: 0x00014158
			public IPartition<TSource> Take(int count)
			{
				int num = this._minIndexInclusive + count - 1;
				if (num < this._maxIndexInclusive)
				{
					return new Enumerable.ListPartition<TSource>(this._source, this._minIndexInclusive, num);
				}
				return this;
			}

			// Token: 0x060006B7 RID: 1719 RVA: 0x00015F90 File Offset: 0x00014190
			public TSource TryGetElementAt(int index, out bool found)
			{
				if (index <= this._maxIndexInclusive - this._minIndexInclusive && index < this._source.Count - this._minIndexInclusive)
				{
					found = true;
					return this._source[this._minIndexInclusive + index];
				}
				found = false;
				return default(TSource);
			}

			// Token: 0x060006B8 RID: 1720 RVA: 0x00015FE8 File Offset: 0x000141E8
			public TSource TryGetFirst(out bool found)
			{
				if (this._source.Count > this._minIndexInclusive)
				{
					found = true;
					return this._source[this._minIndexInclusive];
				}
				found = false;
				return default(TSource);
			}

			// Token: 0x060006B9 RID: 1721 RVA: 0x0001602C File Offset: 0x0001422C
			public TSource TryGetLast(out bool found)
			{
				int num = this._source.Count - 1;
				if (num >= this._minIndexInclusive)
				{
					found = true;
					return this._source[Math.Min(num, this._maxIndexInclusive)];
				}
				found = false;
				return default(TSource);
			}

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x060006BA RID: 1722 RVA: 0x00016078 File Offset: 0x00014278
			private int Count
			{
				get
				{
					int count = this._source.Count;
					if (count <= this._minIndexInclusive)
					{
						return 0;
					}
					return Math.Min(count - 1, this._maxIndexInclusive) - this._minIndexInclusive + 1;
				}
			}

			// Token: 0x060006BB RID: 1723 RVA: 0x000160B4 File Offset: 0x000142B4
			public TSource[] ToArray()
			{
				int count = this.Count;
				if (count == 0)
				{
					return Array.Empty<TSource>();
				}
				TSource[] array = new TSource[count];
				int num = 0;
				int num2 = this._minIndexInclusive;
				while (num != array.Length)
				{
					array[num] = this._source[num2];
					num++;
					num2++;
				}
				return array;
			}

			// Token: 0x060006BC RID: 1724 RVA: 0x00016108 File Offset: 0x00014308
			public List<TSource> ToList()
			{
				int count = this.Count;
				if (count == 0)
				{
					return new List<TSource>();
				}
				List<TSource> list = new List<TSource>(count);
				int num = this._minIndexInclusive + count;
				for (int num2 = this._minIndexInclusive; num2 != num; num2++)
				{
					list.Add(this._source[num2]);
				}
				return list;
			}

			// Token: 0x060006BD RID: 1725 RVA: 0x00016159 File Offset: 0x00014359
			public int GetCount(bool onlyIfCheap)
			{
				return this.Count;
			}

			// Token: 0x040003E1 RID: 993
			private readonly IList<TSource> _source;

			// Token: 0x040003E2 RID: 994
			private readonly int _minIndexInclusive;

			// Token: 0x040003E3 RID: 995
			private readonly int _maxIndexInclusive;
		}

		// Token: 0x020000BB RID: 187
		private sealed class EnumerablePartition<TSource> : Enumerable.Iterator<TSource>, IPartition<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x060006BE RID: 1726 RVA: 0x00016161 File Offset: 0x00014361
			internal EnumerablePartition(IEnumerable<TSource> source, int minIndexInclusive, int maxIndexInclusive)
			{
				this._source = source;
				this._minIndexInclusive = minIndexInclusive;
				this._maxIndexInclusive = maxIndexInclusive;
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001617E File Offset: 0x0001437E
			private bool HasLimit
			{
				get
				{
					return this._maxIndexInclusive != -1;
				}
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001618C File Offset: 0x0001438C
			private int Limit
			{
				get
				{
					return this._maxIndexInclusive + 1 - this._minIndexInclusive;
				}
			}

			// Token: 0x060006C1 RID: 1729 RVA: 0x0001619D File Offset: 0x0001439D
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.EnumerablePartition<TSource>(this._source, this._minIndexInclusive, this._maxIndexInclusive);
			}

			// Token: 0x060006C2 RID: 1730 RVA: 0x000161B6 File Offset: 0x000143B6
			public override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x060006C3 RID: 1731 RVA: 0x000161D8 File Offset: 0x000143D8
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					return -1;
				}
				if (!this.HasLimit)
				{
					return Math.Max(this._source.Count<TSource>() - this._minIndexInclusive, 0);
				}
				int num;
				using (IEnumerator<TSource> enumerator = this._source.GetEnumerator())
				{
					num = Math.Max((int)(Enumerable.EnumerablePartition<TSource>.SkipAndCount((uint)(this._maxIndexInclusive + 1), enumerator) - (uint)this._minIndexInclusive), 0);
				}
				return num;
			}

			// Token: 0x060006C4 RID: 1732 RVA: 0x00016250 File Offset: 0x00014450
			public override bool MoveNext()
			{
				int num = this._state - 3;
				if (num < -2)
				{
					this.Dispose();
					return false;
				}
				int state = this._state;
				if (state != 1)
				{
					if (state != 2)
					{
						goto IL_0054;
					}
				}
				else
				{
					this._enumerator = this._source.GetEnumerator();
					this._state = 2;
				}
				if (!this.SkipBeforeFirst(this._enumerator))
				{
					goto IL_009B;
				}
				this._state = 3;
				IL_0054:
				if ((!this.HasLimit || num < this.Limit) && this._enumerator.MoveNext())
				{
					if (this.HasLimit)
					{
						this._state++;
					}
					this._current = this._enumerator.Current;
					return true;
				}
				IL_009B:
				this.Dispose();
				return false;
			}

			// Token: 0x060006C5 RID: 1733 RVA: 0x000162FF File Offset: 0x000144FF
			public override IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
			{
				return new Enumerable.SelectIPartitionIterator<TSource, TResult>(this, selector);
			}

			// Token: 0x060006C6 RID: 1734 RVA: 0x00016308 File Offset: 0x00014508
			public IPartition<TSource> Skip(int count)
			{
				int num = this._minIndexInclusive + count;
				if (!this.HasLimit)
				{
					if (num < 0)
					{
						return new Enumerable.EnumerablePartition<TSource>(this, count, -1);
					}
				}
				else if (num > this._maxIndexInclusive)
				{
					return EmptyPartition<TSource>.Instance;
				}
				return new Enumerable.EnumerablePartition<TSource>(this._source, num, this._maxIndexInclusive);
			}

			// Token: 0x060006C7 RID: 1735 RVA: 0x00016354 File Offset: 0x00014554
			public IPartition<TSource> Take(int count)
			{
				int num = this._minIndexInclusive + count - 1;
				if (!this.HasLimit)
				{
					if (num < 0)
					{
						return new Enumerable.EnumerablePartition<TSource>(this, 0, count - 1);
					}
				}
				else if (num >= this._maxIndexInclusive)
				{
					return this;
				}
				return new Enumerable.EnumerablePartition<TSource>(this._source, this._minIndexInclusive, num);
			}

			// Token: 0x060006C8 RID: 1736 RVA: 0x000163A0 File Offset: 0x000145A0
			public TSource TryGetElementAt(int index, out bool found)
			{
				if (index >= 0 && (!this.HasLimit || index < this.Limit))
				{
					using (IEnumerator<TSource> enumerator = this._source.GetEnumerator())
					{
						if (Enumerable.EnumerablePartition<TSource>.SkipBefore(this._minIndexInclusive + index, enumerator) && enumerator.MoveNext())
						{
							found = true;
							return enumerator.Current;
						}
					}
				}
				found = false;
				return default(TSource);
			}

			// Token: 0x060006C9 RID: 1737 RVA: 0x0001641C File Offset: 0x0001461C
			public TSource TryGetFirst(out bool found)
			{
				using (IEnumerator<TSource> enumerator = this._source.GetEnumerator())
				{
					if (this.SkipBeforeFirst(enumerator) && enumerator.MoveNext())
					{
						found = true;
						return enumerator.Current;
					}
				}
				found = false;
				return default(TSource);
			}

			// Token: 0x060006CA RID: 1738 RVA: 0x0001647C File Offset: 0x0001467C
			public TSource TryGetLast(out bool found)
			{
				using (IEnumerator<TSource> enumerator = this._source.GetEnumerator())
				{
					if (this.SkipBeforeFirst(enumerator) && enumerator.MoveNext())
					{
						int num = this.Limit - 1;
						int num2 = (this.HasLimit ? 0 : int.MinValue);
						TSource tsource;
						do
						{
							num--;
							tsource = enumerator.Current;
						}
						while (num >= num2 && enumerator.MoveNext());
						found = true;
						return tsource;
					}
				}
				found = false;
				return default(TSource);
			}

			// Token: 0x060006CB RID: 1739 RVA: 0x0001650C File Offset: 0x0001470C
			public TSource[] ToArray()
			{
				using (IEnumerator<TSource> enumerator = this._source.GetEnumerator())
				{
					if (this.SkipBeforeFirst(enumerator) && enumerator.MoveNext())
					{
						int num = this.Limit - 1;
						int num2 = (this.HasLimit ? 0 : int.MinValue);
						int num3 = (this.HasLimit ? this.Limit : int.MaxValue);
						LargeArrayBuilder<TSource> largeArrayBuilder = new LargeArrayBuilder<TSource>(num3);
						do
						{
							num--;
							largeArrayBuilder.Add(enumerator.Current);
						}
						while (num >= num2 && enumerator.MoveNext());
						return largeArrayBuilder.ToArray();
					}
				}
				return Array.Empty<TSource>();
			}

			// Token: 0x060006CC RID: 1740 RVA: 0x000165BC File Offset: 0x000147BC
			public List<TSource> ToList()
			{
				List<TSource> list = new List<TSource>();
				using (IEnumerator<TSource> enumerator = this._source.GetEnumerator())
				{
					if (this.SkipBeforeFirst(enumerator) && enumerator.MoveNext())
					{
						int num = this.Limit - 1;
						int num2 = (this.HasLimit ? 0 : int.MinValue);
						do
						{
							num--;
							list.Add(enumerator.Current);
						}
						while (num >= num2 && enumerator.MoveNext());
					}
				}
				return list;
			}

			// Token: 0x060006CD RID: 1741 RVA: 0x00016640 File Offset: 0x00014840
			private bool SkipBeforeFirst(IEnumerator<TSource> en)
			{
				return Enumerable.EnumerablePartition<TSource>.SkipBefore(this._minIndexInclusive, en);
			}

			// Token: 0x060006CE RID: 1742 RVA: 0x0001664E File Offset: 0x0001484E
			private static bool SkipBefore(int index, IEnumerator<TSource> en)
			{
				return Enumerable.EnumerablePartition<TSource>.SkipAndCount(index, en) == index;
			}

			// Token: 0x060006CF RID: 1743 RVA: 0x0001665A File Offset: 0x0001485A
			private static int SkipAndCount(int index, IEnumerator<TSource> en)
			{
				return (int)Enumerable.EnumerablePartition<TSource>.SkipAndCount((uint)index, en);
			}

			// Token: 0x060006D0 RID: 1744 RVA: 0x00016664 File Offset: 0x00014864
			private static uint SkipAndCount(uint index, IEnumerator<TSource> en)
			{
				for (uint num = 0U; num < index; num += 1U)
				{
					if (!en.MoveNext())
					{
						return num;
					}
				}
				return index;
			}

			// Token: 0x040003E4 RID: 996
			private readonly IEnumerable<TSource> _source;

			// Token: 0x040003E5 RID: 997
			private readonly int _minIndexInclusive;

			// Token: 0x040003E6 RID: 998
			private readonly int _maxIndexInclusive;

			// Token: 0x040003E7 RID: 999
			private IEnumerator<TSource> _enumerator;
		}

		// Token: 0x020000BC RID: 188
		private sealed class RangeIterator : Enumerable.Iterator<int>, IPartition<int>, IIListProvider<int>, IEnumerable<int>, IEnumerable
		{
			// Token: 0x060006D1 RID: 1745 RVA: 0x00016688 File Offset: 0x00014888
			public RangeIterator(int start, int count)
			{
				this._start = start;
				this._end = start + count;
			}

			// Token: 0x060006D2 RID: 1746 RVA: 0x000166A0 File Offset: 0x000148A0
			public override Enumerable.Iterator<int> Clone()
			{
				return new Enumerable.RangeIterator(this._start, this._end - this._start);
			}

			// Token: 0x060006D3 RID: 1747 RVA: 0x000166BC File Offset: 0x000148BC
			public override bool MoveNext()
			{
				int num = this._state;
				if (num != 1)
				{
					if (num == 2)
					{
						num = this._current + 1;
						this._current = num;
						if (num != this._end)
						{
							return true;
						}
					}
					this._state = -1;
					return false;
				}
				this._current = this._start;
				this._state = 2;
				return true;
			}

			// Token: 0x060006D4 RID: 1748 RVA: 0x00016712 File Offset: 0x00014912
			public override void Dispose()
			{
				this._state = -1;
			}

			// Token: 0x060006D5 RID: 1749 RVA: 0x0001671B File Offset: 0x0001491B
			public override IEnumerable<TResult> Select<TResult>(Func<int, TResult> selector)
			{
				return new Enumerable.SelectIPartitionIterator<int, TResult>(this, selector);
			}

			// Token: 0x060006D6 RID: 1750 RVA: 0x00016724 File Offset: 0x00014924
			public int[] ToArray()
			{
				int[] array = new int[this._end - this._start];
				int num = this._start;
				for (int num2 = 0; num2 != array.Length; num2++)
				{
					array[num2] = num;
					num++;
				}
				return array;
			}

			// Token: 0x060006D7 RID: 1751 RVA: 0x00016764 File Offset: 0x00014964
			public List<int> ToList()
			{
				List<int> list = new List<int>(this._end - this._start);
				for (int num = this._start; num != this._end; num++)
				{
					list.Add(num);
				}
				return list;
			}

			// Token: 0x060006D8 RID: 1752 RVA: 0x000167A2 File Offset: 0x000149A2
			public int GetCount(bool onlyIfCheap)
			{
				return this._end - this._start;
			}

			// Token: 0x060006D9 RID: 1753 RVA: 0x000167B1 File Offset: 0x000149B1
			public IPartition<int> Skip(int count)
			{
				if (count >= this._end - this._start)
				{
					return EmptyPartition<int>.Instance;
				}
				return new Enumerable.RangeIterator(this._start + count, this._end - this._start - count);
			}

			// Token: 0x060006DA RID: 1754 RVA: 0x000167E8 File Offset: 0x000149E8
			public IPartition<int> Take(int count)
			{
				int num = this._end - this._start;
				if (count >= num)
				{
					return this;
				}
				return new Enumerable.RangeIterator(this._start, count);
			}

			// Token: 0x060006DB RID: 1755 RVA: 0x00016815 File Offset: 0x00014A15
			public int TryGetElementAt(int index, out bool found)
			{
				if (index < this._end - this._start)
				{
					found = true;
					return this._start + index;
				}
				found = false;
				return 0;
			}

			// Token: 0x060006DC RID: 1756 RVA: 0x00016837 File Offset: 0x00014A37
			public int TryGetFirst(out bool found)
			{
				found = true;
				return this._start;
			}

			// Token: 0x060006DD RID: 1757 RVA: 0x00016842 File Offset: 0x00014A42
			public int TryGetLast(out bool found)
			{
				found = true;
				return this._end - 1;
			}

			// Token: 0x040003E8 RID: 1000
			private readonly int _start;

			// Token: 0x040003E9 RID: 1001
			private readonly int _end;
		}

		// Token: 0x020000BD RID: 189
		private sealed class RepeatIterator<TResult> : Enumerable.Iterator<TResult>, IPartition<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x060006DE RID: 1758 RVA: 0x0001684F File Offset: 0x00014A4F
			public RepeatIterator(TResult element, int count)
			{
				this._current = element;
				this._count = count;
			}

			// Token: 0x060006DF RID: 1759 RVA: 0x00016865 File Offset: 0x00014A65
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.RepeatIterator<TResult>(this._current, this._count);
			}

			// Token: 0x060006E0 RID: 1760 RVA: 0x00016878 File Offset: 0x00014A78
			public override void Dispose()
			{
				this._state = -1;
			}

			// Token: 0x060006E1 RID: 1761 RVA: 0x00016884 File Offset: 0x00014A84
			public override bool MoveNext()
			{
				int num = this._state - 1;
				if (num >= 0 && num != this._count)
				{
					this._state++;
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x060006E2 RID: 1762 RVA: 0x000162FF File Offset: 0x000144FF
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.SelectIPartitionIterator<TResult, TResult2>(this, selector);
			}

			// Token: 0x060006E3 RID: 1763 RVA: 0x000168C0 File Offset: 0x00014AC0
			public TResult[] ToArray()
			{
				TResult[] array = new TResult[this._count];
				if (this._current != null)
				{
					Array.Fill<TResult>(array, this._current);
				}
				return array;
			}

			// Token: 0x060006E4 RID: 1764 RVA: 0x000168F4 File Offset: 0x00014AF4
			public List<TResult> ToList()
			{
				List<TResult> list = new List<TResult>(this._count);
				for (int num = 0; num != this._count; num++)
				{
					list.Add(this._current);
				}
				return list;
			}

			// Token: 0x060006E5 RID: 1765 RVA: 0x0001692B File Offset: 0x00014B2B
			public int GetCount(bool onlyIfCheap)
			{
				return this._count;
			}

			// Token: 0x060006E6 RID: 1766 RVA: 0x00016933 File Offset: 0x00014B33
			public IPartition<TResult> Skip(int count)
			{
				if (count >= this._count)
				{
					return EmptyPartition<TResult>.Instance;
				}
				return new Enumerable.RepeatIterator<TResult>(this._current, this._count - count);
			}

			// Token: 0x060006E7 RID: 1767 RVA: 0x00016957 File Offset: 0x00014B57
			public IPartition<TResult> Take(int count)
			{
				if (count >= this._count)
				{
					return this;
				}
				return new Enumerable.RepeatIterator<TResult>(this._current, count);
			}

			// Token: 0x060006E8 RID: 1768 RVA: 0x00016970 File Offset: 0x00014B70
			public TResult TryGetElementAt(int index, out bool found)
			{
				if (index < this._count)
				{
					found = true;
					return this._current;
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x060006E9 RID: 1769 RVA: 0x0001699C File Offset: 0x00014B9C
			public TResult TryGetFirst(out bool found)
			{
				found = true;
				return this._current;
			}

			// Token: 0x060006EA RID: 1770 RVA: 0x0001699C File Offset: 0x00014B9C
			public TResult TryGetLast(out bool found)
			{
				found = true;
				return this._current;
			}

			// Token: 0x040003EA RID: 1002
			private readonly int _count;
		}

		// Token: 0x020000BE RID: 190
		private sealed class ReverseIterator<TSource> : Enumerable.Iterator<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x060006EB RID: 1771 RVA: 0x000169A7 File Offset: 0x00014BA7
			public ReverseIterator(IEnumerable<TSource> source)
			{
				this._source = source;
			}

			// Token: 0x060006EC RID: 1772 RVA: 0x000169B6 File Offset: 0x00014BB6
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.ReverseIterator<TSource>(this._source);
			}

			// Token: 0x060006ED RID: 1773 RVA: 0x000169C4 File Offset: 0x00014BC4
			public override bool MoveNext()
			{
				if (this._state - 2 <= -2)
				{
					this.Dispose();
					return false;
				}
				int state = this._state;
				if (state == 1)
				{
					Buffer<TSource> buffer = new Buffer<TSource>(this._source);
					this._buffer = buffer._items;
					this._state = buffer._count + 2;
				}
				int num = this._state - 3;
				if (num != -1)
				{
					this._current = this._buffer[num];
					this._state--;
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x060006EE RID: 1774 RVA: 0x00016A4D File Offset: 0x00014C4D
			public override void Dispose()
			{
				this._buffer = null;
				base.Dispose();
			}

			// Token: 0x060006EF RID: 1775 RVA: 0x00016A5C File Offset: 0x00014C5C
			public TSource[] ToArray()
			{
				TSource[] array = this._source.ToArray<TSource>();
				Array.Reverse<TSource>(array);
				return array;
			}

			// Token: 0x060006F0 RID: 1776 RVA: 0x00016A6F File Offset: 0x00014C6F
			public List<TSource> ToList()
			{
				List<TSource> list = this._source.ToList<TSource>();
				list.Reverse();
				return list;
			}

			// Token: 0x060006F1 RID: 1777 RVA: 0x00016A84 File Offset: 0x00014C84
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					IEnumerable<TSource> source = this._source;
					if (source != null)
					{
						IIListProvider<TSource> iilistProvider;
						if ((iilistProvider = source as IIListProvider<TSource>) != null)
						{
							return iilistProvider.GetCount(true);
						}
						ICollection<TSource> collection;
						if ((collection = source as ICollection<TSource>) != null)
						{
							return collection.Count;
						}
						ICollection collection2;
						if ((collection2 = source as ICollection) != null)
						{
							return collection2.Count;
						}
					}
					return -1;
				}
				return this._source.Count<TSource>();
			}

			// Token: 0x040003EB RID: 1003
			private readonly IEnumerable<TSource> _source;

			// Token: 0x040003EC RID: 1004
			private TSource[] _buffer;
		}

		// Token: 0x020000BF RID: 191
		private sealed class SelectEnumerableIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x060006F2 RID: 1778 RVA: 0x00016AE1 File Offset: 0x00014CE1
			public SelectEnumerableIterator(IEnumerable<TSource> source, Func<TSource, TResult> selector)
			{
				this._source = source;
				this._selector = selector;
			}

			// Token: 0x060006F3 RID: 1779 RVA: 0x00016AF7 File Offset: 0x00014CF7
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.SelectEnumerableIterator<TSource, TResult>(this._source, this._selector);
			}

			// Token: 0x060006F4 RID: 1780 RVA: 0x00016B0A File Offset: 0x00014D0A
			public override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x060006F5 RID: 1781 RVA: 0x00016B2C File Offset: 0x00014D2C
			public override bool MoveNext()
			{
				int state = this._state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this._enumerator = this._source.GetEnumerator();
					this._state = 2;
				}
				if (this._enumerator.MoveNext())
				{
					this._current = this._selector(this._enumerator.Current);
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x060006F6 RID: 1782 RVA: 0x00016B94 File Offset: 0x00014D94
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.SelectEnumerableIterator<TSource, TResult2>(this._source, Utilities.CombineSelectors<TSource, TResult, TResult2>(this._selector, selector));
			}

			// Token: 0x060006F7 RID: 1783 RVA: 0x00016BB0 File Offset: 0x00014DB0
			public TResult[] ToArray()
			{
				LargeArrayBuilder<TResult> largeArrayBuilder = new LargeArrayBuilder<TResult>(true);
				foreach (TSource tsource in this._source)
				{
					largeArrayBuilder.Add(this._selector(tsource));
				}
				return largeArrayBuilder.ToArray();
			}

			// Token: 0x060006F8 RID: 1784 RVA: 0x00016C18 File Offset: 0x00014E18
			public List<TResult> ToList()
			{
				List<TResult> list = new List<TResult>();
				foreach (TSource tsource in this._source)
				{
					list.Add(this._selector(tsource));
				}
				return list;
			}

			// Token: 0x060006F9 RID: 1785 RVA: 0x00016C78 File Offset: 0x00014E78
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					return -1;
				}
				int num = 0;
				checked
				{
					foreach (TSource tsource in this._source)
					{
						this._selector(tsource);
						num++;
					}
					return num;
				}
			}

			// Token: 0x040003ED RID: 1005
			private readonly IEnumerable<TSource> _source;

			// Token: 0x040003EE RID: 1006
			private readonly Func<TSource, TResult> _selector;

			// Token: 0x040003EF RID: 1007
			private IEnumerator<TSource> _enumerator;
		}

		// Token: 0x020000C0 RID: 192
		private sealed class SelectArrayIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IPartition<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x060006FA RID: 1786 RVA: 0x00016CD8 File Offset: 0x00014ED8
			public SelectArrayIterator(TSource[] source, Func<TSource, TResult> selector)
			{
				this._source = source;
				this._selector = selector;
			}

			// Token: 0x060006FB RID: 1787 RVA: 0x00016CEE File Offset: 0x00014EEE
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.SelectArrayIterator<TSource, TResult>(this._source, this._selector);
			}

			// Token: 0x060006FC RID: 1788 RVA: 0x00016D04 File Offset: 0x00014F04
			public override bool MoveNext()
			{
				if ((this._state < 1) | (this._state == this._source.Length + 1))
				{
					this.Dispose();
					return false;
				}
				int state = this._state;
				this._state = state + 1;
				int num = state - 1;
				this._current = this._selector(this._source[num]);
				return true;
			}

			// Token: 0x060006FD RID: 1789 RVA: 0x00016D69 File Offset: 0x00014F69
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.SelectArrayIterator<TSource, TResult2>(this._source, Utilities.CombineSelectors<TSource, TResult, TResult2>(this._selector, selector));
			}

			// Token: 0x060006FE RID: 1790 RVA: 0x00016D84 File Offset: 0x00014F84
			public TResult[] ToArray()
			{
				TResult[] array = new TResult[this._source.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._selector(this._source[i]);
				}
				return array;
			}

			// Token: 0x060006FF RID: 1791 RVA: 0x00016DCC File Offset: 0x00014FCC
			public List<TResult> ToList()
			{
				TSource[] source = this._source;
				List<TResult> list = new List<TResult>(source.Length);
				for (int i = 0; i < source.Length; i++)
				{
					list.Add(this._selector(source[i]));
				}
				return list;
			}

			// Token: 0x06000700 RID: 1792 RVA: 0x00016E10 File Offset: 0x00015010
			public int GetCount(bool onlyIfCheap)
			{
				if (!onlyIfCheap)
				{
					foreach (TSource tsource in this._source)
					{
						this._selector(tsource);
					}
				}
				return this._source.Length;
			}

			// Token: 0x06000701 RID: 1793 RVA: 0x00016E52 File Offset: 0x00015052
			public IPartition<TResult> Skip(int count)
			{
				if (count >= this._source.Length)
				{
					return EmptyPartition<TResult>.Instance;
				}
				return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, this._selector, count, int.MaxValue);
			}

			// Token: 0x06000702 RID: 1794 RVA: 0x00016E7C File Offset: 0x0001507C
			public IPartition<TResult> Take(int count)
			{
				if (count < this._source.Length)
				{
					return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, this._selector, 0, count - 1);
				}
				return this;
			}

			// Token: 0x06000703 RID: 1795 RVA: 0x00016EB0 File Offset: 0x000150B0
			public TResult TryGetElementAt(int index, out bool found)
			{
				if (index < this._source.Length)
				{
					found = true;
					return this._selector(this._source[index]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x06000704 RID: 1796 RVA: 0x00016EEF File Offset: 0x000150EF
			public TResult TryGetFirst(out bool found)
			{
				found = true;
				return this._selector(this._source[0]);
			}

			// Token: 0x06000705 RID: 1797 RVA: 0x00016F0B File Offset: 0x0001510B
			public TResult TryGetLast(out bool found)
			{
				found = true;
				return this._selector(this._source[this._source.Length - 1]);
			}

			// Token: 0x040003F0 RID: 1008
			private readonly TSource[] _source;

			// Token: 0x040003F1 RID: 1009
			private readonly Func<TSource, TResult> _selector;
		}

		// Token: 0x020000C1 RID: 193
		private sealed class SelectListIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IPartition<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x06000706 RID: 1798 RVA: 0x00016F30 File Offset: 0x00015130
			public SelectListIterator(List<TSource> source, Func<TSource, TResult> selector)
			{
				this._source = source;
				this._selector = selector;
			}

			// Token: 0x06000707 RID: 1799 RVA: 0x00016F46 File Offset: 0x00015146
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.SelectListIterator<TSource, TResult>(this._source, this._selector);
			}

			// Token: 0x06000708 RID: 1800 RVA: 0x00016F5C File Offset: 0x0001515C
			public override bool MoveNext()
			{
				int state = this._state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this._enumerator = this._source.GetEnumerator();
					this._state = 2;
				}
				if (this._enumerator.MoveNext())
				{
					this._current = this._selector(this._enumerator.Current);
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x06000709 RID: 1801 RVA: 0x00016FC4 File Offset: 0x000151C4
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.SelectListIterator<TSource, TResult2>(this._source, Utilities.CombineSelectors<TSource, TResult, TResult2>(this._selector, selector));
			}

			// Token: 0x0600070A RID: 1802 RVA: 0x00016FE0 File Offset: 0x000151E0
			public TResult[] ToArray()
			{
				int count = this._source.Count;
				if (count == 0)
				{
					return Array.Empty<TResult>();
				}
				TResult[] array = new TResult[count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._selector(this._source[i]);
				}
				return array;
			}

			// Token: 0x0600070B RID: 1803 RVA: 0x00017038 File Offset: 0x00015238
			public List<TResult> ToList()
			{
				int count = this._source.Count;
				List<TResult> list = new List<TResult>(count);
				for (int i = 0; i < count; i++)
				{
					list.Add(this._selector(this._source[i]));
				}
				return list;
			}

			// Token: 0x0600070C RID: 1804 RVA: 0x00017084 File Offset: 0x00015284
			public int GetCount(bool onlyIfCheap)
			{
				int count = this._source.Count;
				if (!onlyIfCheap)
				{
					for (int i = 0; i < count; i++)
					{
						this._selector(this._source[i]);
					}
				}
				return count;
			}

			// Token: 0x0600070D RID: 1805 RVA: 0x000170C5 File Offset: 0x000152C5
			public IPartition<TResult> Skip(int count)
			{
				return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, this._selector, count, int.MaxValue);
			}

			// Token: 0x0600070E RID: 1806 RVA: 0x000170DE File Offset: 0x000152DE
			public IPartition<TResult> Take(int count)
			{
				return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, this._selector, 0, count - 1);
			}

			// Token: 0x0600070F RID: 1807 RVA: 0x000170F8 File Offset: 0x000152F8
			public TResult TryGetElementAt(int index, out bool found)
			{
				if (index < this._source.Count)
				{
					found = true;
					return this._selector(this._source[index]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x06000710 RID: 1808 RVA: 0x0001713C File Offset: 0x0001533C
			public TResult TryGetFirst(out bool found)
			{
				if (this._source.Count != 0)
				{
					found = true;
					return this._selector(this._source[0]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x06000711 RID: 1809 RVA: 0x00017180 File Offset: 0x00015380
			public TResult TryGetLast(out bool found)
			{
				int count = this._source.Count;
				if (count != 0)
				{
					found = true;
					return this._selector(this._source[count - 1]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x040003F2 RID: 1010
			private readonly List<TSource> _source;

			// Token: 0x040003F3 RID: 1011
			private readonly Func<TSource, TResult> _selector;

			// Token: 0x040003F4 RID: 1012
			private List<TSource>.Enumerator _enumerator;
		}

		// Token: 0x020000C2 RID: 194
		private sealed class SelectIListIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IPartition<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x06000712 RID: 1810 RVA: 0x000171C5 File Offset: 0x000153C5
			public SelectIListIterator(IList<TSource> source, Func<TSource, TResult> selector)
			{
				this._source = source;
				this._selector = selector;
			}

			// Token: 0x06000713 RID: 1811 RVA: 0x000171DB File Offset: 0x000153DB
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.SelectIListIterator<TSource, TResult>(this._source, this._selector);
			}

			// Token: 0x06000714 RID: 1812 RVA: 0x000171F0 File Offset: 0x000153F0
			public override bool MoveNext()
			{
				int state = this._state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this._enumerator = this._source.GetEnumerator();
					this._state = 2;
				}
				if (this._enumerator.MoveNext())
				{
					this._current = this._selector(this._enumerator.Current);
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x06000715 RID: 1813 RVA: 0x00017258 File Offset: 0x00015458
			public override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x06000716 RID: 1814 RVA: 0x0001727A File Offset: 0x0001547A
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.SelectIListIterator<TSource, TResult2>(this._source, Utilities.CombineSelectors<TSource, TResult, TResult2>(this._selector, selector));
			}

			// Token: 0x06000717 RID: 1815 RVA: 0x00017294 File Offset: 0x00015494
			public TResult[] ToArray()
			{
				int count = this._source.Count;
				if (count == 0)
				{
					return Array.Empty<TResult>();
				}
				TResult[] array = new TResult[count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._selector(this._source[i]);
				}
				return array;
			}

			// Token: 0x06000718 RID: 1816 RVA: 0x000172EC File Offset: 0x000154EC
			public List<TResult> ToList()
			{
				int count = this._source.Count;
				List<TResult> list = new List<TResult>(count);
				for (int i = 0; i < count; i++)
				{
					list.Add(this._selector(this._source[i]));
				}
				return list;
			}

			// Token: 0x06000719 RID: 1817 RVA: 0x00017338 File Offset: 0x00015538
			public int GetCount(bool onlyIfCheap)
			{
				int count = this._source.Count;
				if (!onlyIfCheap)
				{
					for (int i = 0; i < count; i++)
					{
						this._selector(this._source[i]);
					}
				}
				return count;
			}

			// Token: 0x0600071A RID: 1818 RVA: 0x00017379 File Offset: 0x00015579
			public IPartition<TResult> Skip(int count)
			{
				return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, this._selector, count, int.MaxValue);
			}

			// Token: 0x0600071B RID: 1819 RVA: 0x00017392 File Offset: 0x00015592
			public IPartition<TResult> Take(int count)
			{
				return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, this._selector, 0, count - 1);
			}

			// Token: 0x0600071C RID: 1820 RVA: 0x000173AC File Offset: 0x000155AC
			public TResult TryGetElementAt(int index, out bool found)
			{
				if (index < this._source.Count)
				{
					found = true;
					return this._selector(this._source[index]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x0600071D RID: 1821 RVA: 0x000173F0 File Offset: 0x000155F0
			public TResult TryGetFirst(out bool found)
			{
				if (this._source.Count != 0)
				{
					found = true;
					return this._selector(this._source[0]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x0600071E RID: 1822 RVA: 0x00017434 File Offset: 0x00015634
			public TResult TryGetLast(out bool found)
			{
				int count = this._source.Count;
				if (count != 0)
				{
					found = true;
					return this._selector(this._source[count - 1]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x040003F5 RID: 1013
			private readonly IList<TSource> _source;

			// Token: 0x040003F6 RID: 1014
			private readonly Func<TSource, TResult> _selector;

			// Token: 0x040003F7 RID: 1015
			private IEnumerator<TSource> _enumerator;
		}

		// Token: 0x020000C3 RID: 195
		private sealed class SelectIPartitionIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IPartition<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x0600071F RID: 1823 RVA: 0x00017479 File Offset: 0x00015679
			public SelectIPartitionIterator(IPartition<TSource> source, Func<TSource, TResult> selector)
			{
				this._source = source;
				this._selector = selector;
			}

			// Token: 0x06000720 RID: 1824 RVA: 0x0001748F File Offset: 0x0001568F
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.SelectIPartitionIterator<TSource, TResult>(this._source, this._selector);
			}

			// Token: 0x06000721 RID: 1825 RVA: 0x000174A4 File Offset: 0x000156A4
			public override bool MoveNext()
			{
				int state = this._state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this._enumerator = this._source.GetEnumerator();
					this._state = 2;
				}
				if (this._enumerator.MoveNext())
				{
					this._current = this._selector(this._enumerator.Current);
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x06000722 RID: 1826 RVA: 0x0001750C File Offset: 0x0001570C
			public override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x06000723 RID: 1827 RVA: 0x0001752E File Offset: 0x0001572E
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.SelectIPartitionIterator<TSource, TResult2>(this._source, Utilities.CombineSelectors<TSource, TResult, TResult2>(this._selector, selector));
			}

			// Token: 0x06000724 RID: 1828 RVA: 0x00017547 File Offset: 0x00015747
			public IPartition<TResult> Skip(int count)
			{
				return new Enumerable.SelectIPartitionIterator<TSource, TResult>(this._source.Skip(count), this._selector);
			}

			// Token: 0x06000725 RID: 1829 RVA: 0x00017560 File Offset: 0x00015760
			public IPartition<TResult> Take(int count)
			{
				return new Enumerable.SelectIPartitionIterator<TSource, TResult>(this._source.Take(count), this._selector);
			}

			// Token: 0x06000726 RID: 1830 RVA: 0x0001757C File Offset: 0x0001577C
			public TResult TryGetElementAt(int index, out bool found)
			{
				bool flag;
				TSource tsource = this._source.TryGetElementAt(index, out flag);
				found = flag;
				if (!flag)
				{
					return default(TResult);
				}
				return this._selector(tsource);
			}

			// Token: 0x06000727 RID: 1831 RVA: 0x000175B4 File Offset: 0x000157B4
			public TResult TryGetFirst(out bool found)
			{
				bool flag;
				TSource tsource = this._source.TryGetFirst(out flag);
				found = flag;
				if (!flag)
				{
					return default(TResult);
				}
				return this._selector(tsource);
			}

			// Token: 0x06000728 RID: 1832 RVA: 0x000175EC File Offset: 0x000157EC
			public TResult TryGetLast(out bool found)
			{
				bool flag;
				TSource tsource = this._source.TryGetLast(out flag);
				found = flag;
				if (!flag)
				{
					return default(TResult);
				}
				return this._selector(tsource);
			}

			// Token: 0x06000729 RID: 1833 RVA: 0x00017624 File Offset: 0x00015824
			private TResult[] LazyToArray()
			{
				LargeArrayBuilder<TResult> largeArrayBuilder = new LargeArrayBuilder<TResult>(true);
				foreach (TSource tsource in this._source)
				{
					largeArrayBuilder.Add(this._selector(tsource));
				}
				return largeArrayBuilder.ToArray();
			}

			// Token: 0x0600072A RID: 1834 RVA: 0x0001768C File Offset: 0x0001588C
			private TResult[] PreallocatingToArray(int count)
			{
				TResult[] array = new TResult[count];
				int num = 0;
				foreach (TSource tsource in this._source)
				{
					array[num] = this._selector(tsource);
					num++;
				}
				return array;
			}

			// Token: 0x0600072B RID: 1835 RVA: 0x000176F4 File Offset: 0x000158F4
			public TResult[] ToArray()
			{
				int count = this._source.GetCount(true);
				if (count == -1)
				{
					return this.LazyToArray();
				}
				if (count != 0)
				{
					return this.PreallocatingToArray(count);
				}
				return Array.Empty<TResult>();
			}

			// Token: 0x0600072C RID: 1836 RVA: 0x0001772C File Offset: 0x0001592C
			public List<TResult> ToList()
			{
				int count = this._source.GetCount(true);
				List<TResult> list;
				if (count != -1)
				{
					if (count == 0)
					{
						return new List<TResult>();
					}
					list = new List<TResult>(count);
				}
				else
				{
					list = new List<TResult>();
				}
				foreach (TSource tsource in this._source)
				{
					list.Add(this._selector(tsource));
				}
				return list;
			}

			// Token: 0x0600072D RID: 1837 RVA: 0x000177B0 File Offset: 0x000159B0
			public int GetCount(bool onlyIfCheap)
			{
				if (!onlyIfCheap)
				{
					foreach (TSource tsource in this._source)
					{
						this._selector(tsource);
					}
				}
				return this._source.GetCount(onlyIfCheap);
			}

			// Token: 0x040003F8 RID: 1016
			private readonly IPartition<TSource> _source;

			// Token: 0x040003F9 RID: 1017
			private readonly Func<TSource, TResult> _selector;

			// Token: 0x040003FA RID: 1018
			private IEnumerator<TSource> _enumerator;
		}

		// Token: 0x020000C4 RID: 196
		private sealed class SelectListPartitionIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IPartition<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x0600072E RID: 1838 RVA: 0x00017814 File Offset: 0x00015A14
			public SelectListPartitionIterator(IList<TSource> source, Func<TSource, TResult> selector, int minIndexInclusive, int maxIndexInclusive)
			{
				this._source = source;
				this._selector = selector;
				this._minIndexInclusive = minIndexInclusive;
				this._maxIndexInclusive = maxIndexInclusive;
			}

			// Token: 0x0600072F RID: 1839 RVA: 0x00017839 File Offset: 0x00015A39
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, this._selector, this._minIndexInclusive, this._maxIndexInclusive);
			}

			// Token: 0x06000730 RID: 1840 RVA: 0x00017858 File Offset: 0x00015A58
			public override bool MoveNext()
			{
				int num = this._state - 1;
				if (num <= this._maxIndexInclusive - this._minIndexInclusive && num < this._source.Count - this._minIndexInclusive)
				{
					this._current = this._selector(this._source[this._minIndexInclusive + num]);
					this._state++;
					return true;
				}
				this.Dispose();
				return false;
			}

			// Token: 0x06000731 RID: 1841 RVA: 0x000178CE File Offset: 0x00015ACE
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.SelectListPartitionIterator<TSource, TResult2>(this._source, Utilities.CombineSelectors<TSource, TResult, TResult2>(this._selector, selector), this._minIndexInclusive, this._maxIndexInclusive);
			}

			// Token: 0x06000732 RID: 1842 RVA: 0x000178F4 File Offset: 0x00015AF4
			public IPartition<TResult> Skip(int count)
			{
				int num = this._minIndexInclusive + count;
				if (num <= this._maxIndexInclusive)
				{
					return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, this._selector, num, this._maxIndexInclusive);
				}
				return EmptyPartition<TResult>.Instance;
			}

			// Token: 0x06000733 RID: 1843 RVA: 0x00017934 File Offset: 0x00015B34
			public IPartition<TResult> Take(int count)
			{
				int num = this._minIndexInclusive + count - 1;
				if (num < this._maxIndexInclusive)
				{
					return new Enumerable.SelectListPartitionIterator<TSource, TResult>(this._source, this._selector, this._minIndexInclusive, num);
				}
				return this;
			}

			// Token: 0x06000734 RID: 1844 RVA: 0x00017970 File Offset: 0x00015B70
			public TResult TryGetElementAt(int index, out bool found)
			{
				if (index <= this._maxIndexInclusive - this._minIndexInclusive && index < this._source.Count - this._minIndexInclusive)
				{
					found = true;
					return this._selector(this._source[this._minIndexInclusive + index]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x06000735 RID: 1845 RVA: 0x000179D0 File Offset: 0x00015BD0
			public TResult TryGetFirst(out bool found)
			{
				if (this._source.Count > this._minIndexInclusive)
				{
					found = true;
					return this._selector(this._source[this._minIndexInclusive]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x06000736 RID: 1846 RVA: 0x00017A1C File Offset: 0x00015C1C
			public TResult TryGetLast(out bool found)
			{
				int num = this._source.Count - 1;
				if (num >= this._minIndexInclusive)
				{
					found = true;
					return this._selector(this._source[Math.Min(num, this._maxIndexInclusive)]);
				}
				found = false;
				return default(TResult);
			}

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x06000737 RID: 1847 RVA: 0x00017A74 File Offset: 0x00015C74
			private int Count
			{
				get
				{
					int count = this._source.Count;
					if (count <= this._minIndexInclusive)
					{
						return 0;
					}
					return Math.Min(count - 1, this._maxIndexInclusive) - this._minIndexInclusive + 1;
				}
			}

			// Token: 0x06000738 RID: 1848 RVA: 0x00017AB0 File Offset: 0x00015CB0
			public TResult[] ToArray()
			{
				int count = this.Count;
				if (count == 0)
				{
					return Array.Empty<TResult>();
				}
				TResult[] array = new TResult[count];
				int num = 0;
				int num2 = this._minIndexInclusive;
				while (num != array.Length)
				{
					array[num] = this._selector(this._source[num2]);
					num++;
					num2++;
				}
				return array;
			}

			// Token: 0x06000739 RID: 1849 RVA: 0x00017B0C File Offset: 0x00015D0C
			public List<TResult> ToList()
			{
				int count = this.Count;
				if (count == 0)
				{
					return new List<TResult>();
				}
				List<TResult> list = new List<TResult>(count);
				int num = this._minIndexInclusive + count;
				for (int num2 = this._minIndexInclusive; num2 != num; num2++)
				{
					list.Add(this._selector(this._source[num2]));
				}
				return list;
			}

			// Token: 0x0600073A RID: 1850 RVA: 0x00017B68 File Offset: 0x00015D68
			public int GetCount(bool onlyIfCheap)
			{
				int count = this.Count;
				if (!onlyIfCheap)
				{
					int num = this._minIndexInclusive + count;
					for (int num2 = this._minIndexInclusive; num2 != num; num2++)
					{
						this._selector(this._source[num2]);
					}
				}
				return count;
			}

			// Token: 0x040003FB RID: 1019
			private readonly IList<TSource> _source;

			// Token: 0x040003FC RID: 1020
			private readonly Func<TSource, TResult> _selector;

			// Token: 0x040003FD RID: 1021
			private readonly int _minIndexInclusive;

			// Token: 0x040003FE RID: 1022
			private readonly int _maxIndexInclusive;
		}

		// Token: 0x020000C5 RID: 197
		private sealed class SelectManySingleSelectorIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x0600073B RID: 1851 RVA: 0x00017BB2 File Offset: 0x00015DB2
			internal SelectManySingleSelectorIterator(IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
			{
				this._source = source;
				this._selector = selector;
			}

			// Token: 0x0600073C RID: 1852 RVA: 0x00017BC8 File Offset: 0x00015DC8
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.SelectManySingleSelectorIterator<TSource, TResult>(this._source, this._selector);
			}

			// Token: 0x0600073D RID: 1853 RVA: 0x00017BDB File Offset: 0x00015DDB
			public override void Dispose()
			{
				if (this._subEnumerator != null)
				{
					this._subEnumerator.Dispose();
					this._subEnumerator = null;
				}
				if (this._sourceEnumerator != null)
				{
					this._sourceEnumerator.Dispose();
					this._sourceEnumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x0600073E RID: 1854 RVA: 0x00017C18 File Offset: 0x00015E18
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					return -1;
				}
				int num = 0;
				checked
				{
					foreach (TSource tsource in this._source)
					{
						num += this._selector(tsource).Count<TResult>();
					}
					return num;
				}
			}

			// Token: 0x0600073F RID: 1855 RVA: 0x00017C7C File Offset: 0x00015E7C
			public override bool MoveNext()
			{
				switch (this._state)
				{
				case 1:
					this._sourceEnumerator = this._source.GetEnumerator();
					this._state = 2;
					break;
				case 2:
					break;
				case 3:
					goto IL_006F;
				default:
					goto IL_00AA;
				}
				IL_0038:
				if (!this._sourceEnumerator.MoveNext())
				{
					goto IL_00AA;
				}
				TSource tsource = this._sourceEnumerator.Current;
				this._subEnumerator = this._selector(tsource).GetEnumerator();
				this._state = 3;
				IL_006F:
				if (!this._subEnumerator.MoveNext())
				{
					this._subEnumerator.Dispose();
					this._subEnumerator = null;
					this._state = 2;
					goto IL_0038;
				}
				this._current = this._subEnumerator.Current;
				return true;
				IL_00AA:
				this.Dispose();
				return false;
			}

			// Token: 0x06000740 RID: 1856 RVA: 0x00017D3C File Offset: 0x00015F3C
			public TResult[] ToArray()
			{
				SparseArrayBuilder<TResult> sparseArrayBuilder = new SparseArrayBuilder<TResult>(true);
				ArrayBuilder<IEnumerable<TResult>> arrayBuilder = default(ArrayBuilder<IEnumerable<TResult>>);
				foreach (TSource tsource in this._source)
				{
					IEnumerable<TResult> enumerable = this._selector(tsource);
					if (sparseArrayBuilder.ReserveOrAdd(enumerable))
					{
						arrayBuilder.Add(enumerable);
					}
				}
				TResult[] array = sparseArrayBuilder.ToArray();
				ArrayBuilder<Marker> markers = sparseArrayBuilder.Markers;
				for (int i = 0; i < markers.Count; i++)
				{
					Marker marker = markers[i];
					EnumerableHelpers.Copy<TResult>(arrayBuilder[i], array, marker.Index, marker.Count);
				}
				return array;
			}

			// Token: 0x06000741 RID: 1857 RVA: 0x00017E0C File Offset: 0x0001600C
			public List<TResult> ToList()
			{
				List<TResult> list = new List<TResult>();
				foreach (TSource tsource in this._source)
				{
					list.AddRange(this._selector(tsource));
				}
				return list;
			}

			// Token: 0x040003FF RID: 1023
			private readonly IEnumerable<TSource> _source;

			// Token: 0x04000400 RID: 1024
			private readonly Func<TSource, IEnumerable<TResult>> _selector;

			// Token: 0x04000401 RID: 1025
			private IEnumerator<TSource> _sourceEnumerator;

			// Token: 0x04000402 RID: 1026
			private IEnumerator<TResult> _subEnumerator;
		}

		// Token: 0x020000C6 RID: 198
		private abstract class UnionIterator<TSource> : Enumerable.Iterator<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x06000742 RID: 1858 RVA: 0x00017E6C File Offset: 0x0001606C
			protected UnionIterator(IEqualityComparer<TSource> comparer)
			{
				this._comparer = comparer;
			}

			// Token: 0x06000743 RID: 1859 RVA: 0x00017E7B File Offset: 0x0001607B
			public sealed override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
					this._set = null;
				}
				base.Dispose();
			}

			// Token: 0x06000744 RID: 1860
			internal abstract IEnumerable<TSource> GetEnumerable(int index);

			// Token: 0x06000745 RID: 1861
			internal abstract Enumerable.UnionIterator<TSource> Union(IEnumerable<TSource> next);

			// Token: 0x06000746 RID: 1862 RVA: 0x00017EA4 File Offset: 0x000160A4
			private void SetEnumerator(IEnumerator<TSource> enumerator)
			{
				IEnumerator<TSource> enumerator2 = this._enumerator;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
				this._enumerator = enumerator;
			}

			// Token: 0x06000747 RID: 1863 RVA: 0x00017EC0 File Offset: 0x000160C0
			private void StoreFirst()
			{
				Set<TSource> set = new Set<TSource>(this._comparer);
				TSource tsource = this._enumerator.Current;
				set.Add(tsource);
				this._current = tsource;
				this._set = set;
			}

			// Token: 0x06000748 RID: 1864 RVA: 0x00017EFC File Offset: 0x000160FC
			private bool GetNext()
			{
				Set<TSource> set = this._set;
				while (this._enumerator.MoveNext())
				{
					TSource tsource = this._enumerator.Current;
					if (set.Add(tsource))
					{
						this._current = tsource;
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000749 RID: 1865 RVA: 0x00017F40 File Offset: 0x00016140
			public sealed override bool MoveNext()
			{
				if (this._state == 1)
				{
					for (IEnumerable<TSource> enumerable = this.GetEnumerable(0); enumerable != null; enumerable = this.GetEnumerable(this._state - 1))
					{
						IEnumerator<TSource> enumerator = enumerable.GetEnumerator();
						this._state++;
						if (enumerator.MoveNext())
						{
							this.SetEnumerator(enumerator);
							this.StoreFirst();
							return true;
						}
					}
				}
				else if (this._state > 0)
				{
					while (!this.GetNext())
					{
						IEnumerable<TSource> enumerable2 = this.GetEnumerable(this._state - 1);
						if (enumerable2 == null)
						{
							goto IL_0094;
						}
						this.SetEnumerator(enumerable2.GetEnumerator());
						this._state++;
					}
					return true;
				}
				IL_0094:
				this.Dispose();
				return false;
			}

			// Token: 0x0600074A RID: 1866 RVA: 0x00017FE8 File Offset: 0x000161E8
			private Set<TSource> FillSet()
			{
				Set<TSource> set = new Set<TSource>(this._comparer);
				int num = 0;
				for (;;)
				{
					IEnumerable<TSource> enumerable = this.GetEnumerable(num);
					if (enumerable == null)
					{
						break;
					}
					set.UnionWith(enumerable);
					num++;
				}
				return set;
			}

			// Token: 0x0600074B RID: 1867 RVA: 0x0001801C File Offset: 0x0001621C
			public TSource[] ToArray()
			{
				return this.FillSet().ToArray();
			}

			// Token: 0x0600074C RID: 1868 RVA: 0x00018029 File Offset: 0x00016229
			public List<TSource> ToList()
			{
				return this.FillSet().ToList();
			}

			// Token: 0x0600074D RID: 1869 RVA: 0x00018036 File Offset: 0x00016236
			public int GetCount(bool onlyIfCheap)
			{
				if (!onlyIfCheap)
				{
					return this.FillSet().Count;
				}
				return -1;
			}

			// Token: 0x04000403 RID: 1027
			internal readonly IEqualityComparer<TSource> _comparer;

			// Token: 0x04000404 RID: 1028
			private IEnumerator<TSource> _enumerator;

			// Token: 0x04000405 RID: 1029
			private Set<TSource> _set;
		}

		// Token: 0x020000C7 RID: 199
		private sealed class UnionIterator2<TSource> : Enumerable.UnionIterator<TSource>
		{
			// Token: 0x0600074E RID: 1870 RVA: 0x00018048 File Offset: 0x00016248
			public UnionIterator2(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
				: base(comparer)
			{
				this._first = first;
				this._second = second;
			}

			// Token: 0x0600074F RID: 1871 RVA: 0x0001805F File Offset: 0x0001625F
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.UnionIterator2<TSource>(this._first, this._second, this._comparer);
			}

			// Token: 0x06000750 RID: 1872 RVA: 0x00018078 File Offset: 0x00016278
			internal override IEnumerable<TSource> GetEnumerable(int index)
			{
				if (index == 0)
				{
					return this._first;
				}
				if (index != 1)
				{
					return null;
				}
				return this._second;
			}

			// Token: 0x06000751 RID: 1873 RVA: 0x00018092 File Offset: 0x00016292
			internal override Enumerable.UnionIterator<TSource> Union(IEnumerable<TSource> next)
			{
				return new Enumerable.UnionIteratorN<TSource>(new SingleLinkedNode<IEnumerable<TSource>>(this._first).Add(this._second).Add(next), 2, this._comparer);
			}

			// Token: 0x04000406 RID: 1030
			private readonly IEnumerable<TSource> _first;

			// Token: 0x04000407 RID: 1031
			private readonly IEnumerable<TSource> _second;
		}

		// Token: 0x020000C8 RID: 200
		private sealed class UnionIteratorN<TSource> : Enumerable.UnionIterator<TSource>
		{
			// Token: 0x06000752 RID: 1874 RVA: 0x000180BC File Offset: 0x000162BC
			public UnionIteratorN(SingleLinkedNode<IEnumerable<TSource>> sources, int headIndex, IEqualityComparer<TSource> comparer)
				: base(comparer)
			{
				this._sources = sources;
				this._headIndex = headIndex;
			}

			// Token: 0x06000753 RID: 1875 RVA: 0x000180D3 File Offset: 0x000162D3
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.UnionIteratorN<TSource>(this._sources, this._headIndex, this._comparer);
			}

			// Token: 0x06000754 RID: 1876 RVA: 0x000180EC File Offset: 0x000162EC
			internal override IEnumerable<TSource> GetEnumerable(int index)
			{
				if (index <= this._headIndex)
				{
					return this._sources.GetNode(this._headIndex - index).Item;
				}
				return null;
			}

			// Token: 0x06000755 RID: 1877 RVA: 0x00018111 File Offset: 0x00016311
			internal override Enumerable.UnionIterator<TSource> Union(IEnumerable<TSource> next)
			{
				if (this._headIndex == 2147483645)
				{
					return new Enumerable.UnionIterator2<TSource>(this, next, this._comparer);
				}
				return new Enumerable.UnionIteratorN<TSource>(this._sources.Add(next), this._headIndex + 1, this._comparer);
			}

			// Token: 0x04000408 RID: 1032
			private readonly SingleLinkedNode<IEnumerable<TSource>> _sources;

			// Token: 0x04000409 RID: 1033
			private readonly int _headIndex;
		}

		// Token: 0x020000C9 RID: 201
		private sealed class WhereEnumerableIterator<TSource> : Enumerable.Iterator<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x06000756 RID: 1878 RVA: 0x0001814D File Offset: 0x0001634D
			public WhereEnumerableIterator(IEnumerable<TSource> source, Func<TSource, bool> predicate)
			{
				this._source = source;
				this._predicate = predicate;
			}

			// Token: 0x06000757 RID: 1879 RVA: 0x00018163 File Offset: 0x00016363
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.WhereEnumerableIterator<TSource>(this._source, this._predicate);
			}

			// Token: 0x06000758 RID: 1880 RVA: 0x00018176 File Offset: 0x00016376
			public override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x06000759 RID: 1881 RVA: 0x00018198 File Offset: 0x00016398
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					return -1;
				}
				int num = 0;
				checked
				{
					foreach (TSource tsource in this._source)
					{
						if (this._predicate(tsource))
						{
							num++;
						}
					}
					return num;
				}
			}

			// Token: 0x0600075A RID: 1882 RVA: 0x000181F8 File Offset: 0x000163F8
			public override bool MoveNext()
			{
				int state = this._state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this._enumerator = this._source.GetEnumerator();
					this._state = 2;
				}
				while (this._enumerator.MoveNext())
				{
					TSource tsource = this._enumerator.Current;
					if (this._predicate(tsource))
					{
						this._current = tsource;
						return true;
					}
				}
				this.Dispose();
				return false;
			}

			// Token: 0x0600075B RID: 1883 RVA: 0x00018267 File Offset: 0x00016467
			public override IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
			{
				return new Enumerable.WhereSelectEnumerableIterator<TSource, TResult>(this._source, this._predicate, selector);
			}

			// Token: 0x0600075C RID: 1884 RVA: 0x0001827C File Offset: 0x0001647C
			public TSource[] ToArray()
			{
				LargeArrayBuilder<TSource> largeArrayBuilder = new LargeArrayBuilder<TSource>(true);
				foreach (TSource tsource in this._source)
				{
					if (this._predicate(tsource))
					{
						largeArrayBuilder.Add(tsource);
					}
				}
				return largeArrayBuilder.ToArray();
			}

			// Token: 0x0600075D RID: 1885 RVA: 0x000182E8 File Offset: 0x000164E8
			public List<TSource> ToList()
			{
				List<TSource> list = new List<TSource>();
				foreach (TSource tsource in this._source)
				{
					if (this._predicate(tsource))
					{
						list.Add(tsource);
					}
				}
				return list;
			}

			// Token: 0x0600075E RID: 1886 RVA: 0x0001834C File Offset: 0x0001654C
			public override IEnumerable<TSource> Where(Func<TSource, bool> predicate)
			{
				return new Enumerable.WhereEnumerableIterator<TSource>(this._source, Utilities.CombinePredicates<TSource>(this._predicate, predicate));
			}

			// Token: 0x0400040A RID: 1034
			private readonly IEnumerable<TSource> _source;

			// Token: 0x0400040B RID: 1035
			private readonly Func<TSource, bool> _predicate;

			// Token: 0x0400040C RID: 1036
			private IEnumerator<TSource> _enumerator;
		}

		// Token: 0x020000CA RID: 202
		internal sealed class WhereArrayIterator<TSource> : Enumerable.Iterator<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x0600075F RID: 1887 RVA: 0x00018365 File Offset: 0x00016565
			public WhereArrayIterator(TSource[] source, Func<TSource, bool> predicate)
			{
				this._source = source;
				this._predicate = predicate;
			}

			// Token: 0x06000760 RID: 1888 RVA: 0x0001837B File Offset: 0x0001657B
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.WhereArrayIterator<TSource>(this._source, this._predicate);
			}

			// Token: 0x06000761 RID: 1889 RVA: 0x00018390 File Offset: 0x00016590
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					return -1;
				}
				int num = 0;
				checked
				{
					foreach (TSource tsource in this._source)
					{
						if (this._predicate(tsource))
						{
							num++;
						}
					}
					return num;
				}
			}

			// Token: 0x06000762 RID: 1890 RVA: 0x000183D4 File Offset: 0x000165D4
			public override bool MoveNext()
			{
				int i = this._state - 1;
				TSource[] source = this._source;
				while (i < source.Length)
				{
					TSource tsource = source[i];
					int state = this._state;
					this._state = state + 1;
					i = state;
					if (this._predicate(tsource))
					{
						this._current = tsource;
						return true;
					}
				}
				this.Dispose();
				return false;
			}

			// Token: 0x06000763 RID: 1891 RVA: 0x00018431 File Offset: 0x00016631
			public override IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
			{
				return new Enumerable.WhereSelectArrayIterator<TSource, TResult>(this._source, this._predicate, selector);
			}

			// Token: 0x06000764 RID: 1892 RVA: 0x00018448 File Offset: 0x00016648
			public TSource[] ToArray()
			{
				LargeArrayBuilder<TSource> largeArrayBuilder = new LargeArrayBuilder<TSource>(this._source.Length);
				foreach (TSource tsource in this._source)
				{
					if (this._predicate(tsource))
					{
						largeArrayBuilder.Add(tsource);
					}
				}
				return largeArrayBuilder.ToArray();
			}

			// Token: 0x06000765 RID: 1893 RVA: 0x000184A0 File Offset: 0x000166A0
			public List<TSource> ToList()
			{
				List<TSource> list = new List<TSource>();
				foreach (TSource tsource in this._source)
				{
					if (this._predicate(tsource))
					{
						list.Add(tsource);
					}
				}
				return list;
			}

			// Token: 0x06000766 RID: 1894 RVA: 0x000184E6 File Offset: 0x000166E6
			public override IEnumerable<TSource> Where(Func<TSource, bool> predicate)
			{
				return new Enumerable.WhereArrayIterator<TSource>(this._source, Utilities.CombinePredicates<TSource>(this._predicate, predicate));
			}

			// Token: 0x0400040D RID: 1037
			private readonly TSource[] _source;

			// Token: 0x0400040E RID: 1038
			private readonly Func<TSource, bool> _predicate;
		}

		// Token: 0x020000CB RID: 203
		private sealed class WhereListIterator<TSource> : Enumerable.Iterator<TSource>, IIListProvider<TSource>, IEnumerable<TSource>, IEnumerable
		{
			// Token: 0x06000767 RID: 1895 RVA: 0x000184FF File Offset: 0x000166FF
			public WhereListIterator(List<TSource> source, Func<TSource, bool> predicate)
			{
				this._source = source;
				this._predicate = predicate;
			}

			// Token: 0x06000768 RID: 1896 RVA: 0x00018515 File Offset: 0x00016715
			public override Enumerable.Iterator<TSource> Clone()
			{
				return new Enumerable.WhereListIterator<TSource>(this._source, this._predicate);
			}

			// Token: 0x06000769 RID: 1897 RVA: 0x00018528 File Offset: 0x00016728
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					return -1;
				}
				int num = 0;
				for (int i = 0; i < this._source.Count; i++)
				{
					TSource tsource = this._source[i];
					checked
					{
						if (this._predicate(tsource))
						{
							num++;
						}
					}
				}
				return num;
			}

			// Token: 0x0600076A RID: 1898 RVA: 0x00018574 File Offset: 0x00016774
			public override bool MoveNext()
			{
				int state = this._state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this._enumerator = this._source.GetEnumerator();
					this._state = 2;
				}
				while (this._enumerator.MoveNext())
				{
					TSource tsource = this._enumerator.Current;
					if (this._predicate(tsource))
					{
						this._current = tsource;
						return true;
					}
				}
				this.Dispose();
				return false;
			}

			// Token: 0x0600076B RID: 1899 RVA: 0x000185E3 File Offset: 0x000167E3
			public override IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
			{
				return new Enumerable.WhereSelectListIterator<TSource, TResult>(this._source, this._predicate, selector);
			}

			// Token: 0x0600076C RID: 1900 RVA: 0x000185F8 File Offset: 0x000167F8
			public TSource[] ToArray()
			{
				LargeArrayBuilder<TSource> largeArrayBuilder = new LargeArrayBuilder<TSource>(this._source.Count);
				for (int i = 0; i < this._source.Count; i++)
				{
					TSource tsource = this._source[i];
					if (this._predicate(tsource))
					{
						largeArrayBuilder.Add(tsource);
					}
				}
				return largeArrayBuilder.ToArray();
			}

			// Token: 0x0600076D RID: 1901 RVA: 0x00018658 File Offset: 0x00016858
			public List<TSource> ToList()
			{
				List<TSource> list = new List<TSource>();
				for (int i = 0; i < this._source.Count; i++)
				{
					TSource tsource = this._source[i];
					if (this._predicate(tsource))
					{
						list.Add(tsource);
					}
				}
				return list;
			}

			// Token: 0x0600076E RID: 1902 RVA: 0x000186A4 File Offset: 0x000168A4
			public override IEnumerable<TSource> Where(Func<TSource, bool> predicate)
			{
				return new Enumerable.WhereListIterator<TSource>(this._source, Utilities.CombinePredicates<TSource>(this._predicate, predicate));
			}

			// Token: 0x0400040F RID: 1039
			private readonly List<TSource> _source;

			// Token: 0x04000410 RID: 1040
			private readonly Func<TSource, bool> _predicate;

			// Token: 0x04000411 RID: 1041
			private List<TSource>.Enumerator _enumerator;
		}

		// Token: 0x020000CC RID: 204
		private sealed class WhereSelectArrayIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x0600076F RID: 1903 RVA: 0x000186BD File Offset: 0x000168BD
			public WhereSelectArrayIterator(TSource[] source, Func<TSource, bool> predicate, Func<TSource, TResult> selector)
			{
				this._source = source;
				this._predicate = predicate;
				this._selector = selector;
			}

			// Token: 0x06000770 RID: 1904 RVA: 0x000186DA File Offset: 0x000168DA
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.WhereSelectArrayIterator<TSource, TResult>(this._source, this._predicate, this._selector);
			}

			// Token: 0x06000771 RID: 1905 RVA: 0x000186F4 File Offset: 0x000168F4
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					return -1;
				}
				int num = 0;
				checked
				{
					foreach (TSource tsource in this._source)
					{
						if (this._predicate(tsource))
						{
							this._selector(tsource);
							num++;
						}
					}
					return num;
				}
			}

			// Token: 0x06000772 RID: 1906 RVA: 0x00018748 File Offset: 0x00016948
			public override bool MoveNext()
			{
				int i = this._state - 1;
				TSource[] source = this._source;
				while (i < source.Length)
				{
					TSource tsource = source[i];
					int state = this._state;
					this._state = state + 1;
					i = state;
					if (this._predicate(tsource))
					{
						this._current = this._selector(tsource);
						return true;
					}
				}
				this.Dispose();
				return false;
			}

			// Token: 0x06000773 RID: 1907 RVA: 0x000187B0 File Offset: 0x000169B0
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.WhereSelectArrayIterator<TSource, TResult2>(this._source, this._predicate, Utilities.CombineSelectors<TSource, TResult, TResult2>(this._selector, selector));
			}

			// Token: 0x06000774 RID: 1908 RVA: 0x000187D0 File Offset: 0x000169D0
			public TResult[] ToArray()
			{
				LargeArrayBuilder<TResult> largeArrayBuilder = new LargeArrayBuilder<TResult>(this._source.Length);
				foreach (TSource tsource in this._source)
				{
					if (this._predicate(tsource))
					{
						largeArrayBuilder.Add(this._selector(tsource));
					}
				}
				return largeArrayBuilder.ToArray();
			}

			// Token: 0x06000775 RID: 1909 RVA: 0x00018834 File Offset: 0x00016A34
			public List<TResult> ToList()
			{
				List<TResult> list = new List<TResult>();
				foreach (TSource tsource in this._source)
				{
					if (this._predicate(tsource))
					{
						list.Add(this._selector(tsource));
					}
				}
				return list;
			}

			// Token: 0x04000412 RID: 1042
			private readonly TSource[] _source;

			// Token: 0x04000413 RID: 1043
			private readonly Func<TSource, bool> _predicate;

			// Token: 0x04000414 RID: 1044
			private readonly Func<TSource, TResult> _selector;
		}

		// Token: 0x020000CD RID: 205
		private sealed class WhereSelectListIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x06000776 RID: 1910 RVA: 0x00018885 File Offset: 0x00016A85
			public WhereSelectListIterator(List<TSource> source, Func<TSource, bool> predicate, Func<TSource, TResult> selector)
			{
				this._source = source;
				this._predicate = predicate;
				this._selector = selector;
			}

			// Token: 0x06000777 RID: 1911 RVA: 0x000188A2 File Offset: 0x00016AA2
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.WhereSelectListIterator<TSource, TResult>(this._source, this._predicate, this._selector);
			}

			// Token: 0x06000778 RID: 1912 RVA: 0x000188BC File Offset: 0x00016ABC
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					return -1;
				}
				int num = 0;
				for (int i = 0; i < this._source.Count; i++)
				{
					TSource tsource = this._source[i];
					checked
					{
						if (this._predicate(tsource))
						{
							this._selector(tsource);
							num++;
						}
					}
				}
				return num;
			}

			// Token: 0x06000779 RID: 1913 RVA: 0x00018914 File Offset: 0x00016B14
			public override bool MoveNext()
			{
				int state = this._state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this._enumerator = this._source.GetEnumerator();
					this._state = 2;
				}
				while (this._enumerator.MoveNext())
				{
					TSource tsource = this._enumerator.Current;
					if (this._predicate(tsource))
					{
						this._current = this._selector(tsource);
						return true;
					}
				}
				this.Dispose();
				return false;
			}

			// Token: 0x0600077A RID: 1914 RVA: 0x0001898E File Offset: 0x00016B8E
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.WhereSelectListIterator<TSource, TResult2>(this._source, this._predicate, Utilities.CombineSelectors<TSource, TResult, TResult2>(this._selector, selector));
			}

			// Token: 0x0600077B RID: 1915 RVA: 0x000189B0 File Offset: 0x00016BB0
			public TResult[] ToArray()
			{
				LargeArrayBuilder<TResult> largeArrayBuilder = new LargeArrayBuilder<TResult>(this._source.Count);
				for (int i = 0; i < this._source.Count; i++)
				{
					TSource tsource = this._source[i];
					if (this._predicate(tsource))
					{
						largeArrayBuilder.Add(this._selector(tsource));
					}
				}
				return largeArrayBuilder.ToArray();
			}

			// Token: 0x0600077C RID: 1916 RVA: 0x00018A1C File Offset: 0x00016C1C
			public List<TResult> ToList()
			{
				List<TResult> list = new List<TResult>();
				for (int i = 0; i < this._source.Count; i++)
				{
					TSource tsource = this._source[i];
					if (this._predicate(tsource))
					{
						list.Add(this._selector(tsource));
					}
				}
				return list;
			}

			// Token: 0x04000415 RID: 1045
			private readonly List<TSource> _source;

			// Token: 0x04000416 RID: 1046
			private readonly Func<TSource, bool> _predicate;

			// Token: 0x04000417 RID: 1047
			private readonly Func<TSource, TResult> _selector;

			// Token: 0x04000418 RID: 1048
			private List<TSource>.Enumerator _enumerator;
		}

		// Token: 0x020000CE RID: 206
		private sealed class WhereSelectEnumerableIterator<TSource, TResult> : Enumerable.Iterator<TResult>, IIListProvider<TResult>, IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x0600077D RID: 1917 RVA: 0x00018A73 File Offset: 0x00016C73
			public WhereSelectEnumerableIterator(IEnumerable<TSource> source, Func<TSource, bool> predicate, Func<TSource, TResult> selector)
			{
				this._source = source;
				this._predicate = predicate;
				this._selector = selector;
			}

			// Token: 0x0600077E RID: 1918 RVA: 0x00018A90 File Offset: 0x00016C90
			public override Enumerable.Iterator<TResult> Clone()
			{
				return new Enumerable.WhereSelectEnumerableIterator<TSource, TResult>(this._source, this._predicate, this._selector);
			}

			// Token: 0x0600077F RID: 1919 RVA: 0x00018AA9 File Offset: 0x00016CA9
			public override void Dispose()
			{
				if (this._enumerator != null)
				{
					this._enumerator.Dispose();
					this._enumerator = null;
				}
				base.Dispose();
			}

			// Token: 0x06000780 RID: 1920 RVA: 0x00018ACC File Offset: 0x00016CCC
			public int GetCount(bool onlyIfCheap)
			{
				if (onlyIfCheap)
				{
					return -1;
				}
				int num = 0;
				checked
				{
					foreach (TSource tsource in this._source)
					{
						if (this._predicate(tsource))
						{
							this._selector(tsource);
							num++;
						}
					}
					return num;
				}
			}

			// Token: 0x06000781 RID: 1921 RVA: 0x00018B3C File Offset: 0x00016D3C
			public override bool MoveNext()
			{
				int state = this._state;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
				}
				else
				{
					this._enumerator = this._source.GetEnumerator();
					this._state = 2;
				}
				while (this._enumerator.MoveNext())
				{
					TSource tsource = this._enumerator.Current;
					if (this._predicate(tsource))
					{
						this._current = this._selector(tsource);
						return true;
					}
				}
				this.Dispose();
				return false;
			}

			// Token: 0x06000782 RID: 1922 RVA: 0x00018BB6 File Offset: 0x00016DB6
			public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
			{
				return new Enumerable.WhereSelectEnumerableIterator<TSource, TResult2>(this._source, this._predicate, Utilities.CombineSelectors<TSource, TResult, TResult2>(this._selector, selector));
			}

			// Token: 0x06000783 RID: 1923 RVA: 0x00018BD8 File Offset: 0x00016DD8
			public TResult[] ToArray()
			{
				LargeArrayBuilder<TResult> largeArrayBuilder = new LargeArrayBuilder<TResult>(true);
				foreach (TSource tsource in this._source)
				{
					if (this._predicate(tsource))
					{
						largeArrayBuilder.Add(this._selector(tsource));
					}
				}
				return largeArrayBuilder.ToArray();
			}

			// Token: 0x06000784 RID: 1924 RVA: 0x00018C50 File Offset: 0x00016E50
			public List<TResult> ToList()
			{
				List<TResult> list = new List<TResult>();
				foreach (TSource tsource in this._source)
				{
					if (this._predicate(tsource))
					{
						list.Add(this._selector(tsource));
					}
				}
				return list;
			}

			// Token: 0x04000419 RID: 1049
			private readonly IEnumerable<TSource> _source;

			// Token: 0x0400041A RID: 1050
			private readonly Func<TSource, bool> _predicate;

			// Token: 0x0400041B RID: 1051
			private readonly Func<TSource, TResult> _selector;

			// Token: 0x0400041C RID: 1052
			private IEnumerator<TSource> _enumerator;
		}
	}
}
