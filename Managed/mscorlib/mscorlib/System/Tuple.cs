using System;

namespace System
{
	/// <summary>Provides static methods for creating tuple objects. </summary>
	// Token: 0x020000E9 RID: 233
	public static class Tuple
	{
		/// <summary>Creates a new 1-tuple, or singleton.</summary>
		/// <returns>A tuple whose value is (<paramref name="item1" />).</returns>
		/// <param name="item1">The value of the only component of the tuple.</param>
		/// <typeparam name="T1">The type of the only component of the tuple.</typeparam>
		// Token: 0x060008AA RID: 2218 RVA: 0x0002ECE9 File Offset: 0x0002CEE9
		public static Tuple<T1> Create<T1>(T1 item1)
		{
			return new Tuple<T1>(item1);
		}

		/// <summary>Creates a new 2-tuple, or pair.</summary>
		/// <returns>A 2-tuple whose value is (<paramref name="item1" />, <paramref name="item2" />).</returns>
		/// <param name="item1">The value of the first component of the tuple.</param>
		/// <param name="item2">The value of the second component of the tuple.</param>
		/// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
		/// <typeparam name="T2">The type of the second component of the tuple.</typeparam>
		// Token: 0x060008AB RID: 2219 RVA: 0x0002ECF1 File Offset: 0x0002CEF1
		public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new Tuple<T1, T2>(item1, item2);
		}

		/// <summary>Creates a new 3-tuple, or triple.</summary>
		/// <returns>A 3-tuple whose value is (<paramref name="item1" />, <paramref name="item2" />, <paramref name="item3" />).</returns>
		/// <param name="item1">The value of the first component of the tuple.</param>
		/// <param name="item2">The value of the second component of the tuple.</param>
		/// <param name="item3">The value of the third component of the tuple.</param>
		/// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
		/// <typeparam name="T2">The type of the second component of the tuple.</typeparam>
		/// <typeparam name="T3">The type of the third component of the tuple.</typeparam>
		// Token: 0x060008AC RID: 2220 RVA: 0x0002ECFA File Offset: 0x0002CEFA
		public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
		{
			return new Tuple<T1, T2, T3>(item1, item2, item3);
		}

		/// <summary>Creates a new 4-tuple, or quadruple.</summary>
		/// <returns>A 4-tuple whose value is (<paramref name="item1" />, <paramref name="item2" />, <paramref name="item3" />, <paramref name="item4" />).</returns>
		/// <param name="item1">The value of the first component of the tuple.</param>
		/// <param name="item2">The value of the second component of the tuple.</param>
		/// <param name="item3">The value of the third component of the tuple.</param>
		/// <param name="item4">The value of the fourth component of the tuple.</param>
		/// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
		/// <typeparam name="T2">The type of the second component of the tuple.</typeparam>
		/// <typeparam name="T3">The type of the third component of the tuple.</typeparam>
		/// <typeparam name="T4">The type of the fourth component of the tuple.  </typeparam>
		// Token: 0x060008AD RID: 2221 RVA: 0x0002ED04 File Offset: 0x0002CF04
		public static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4);
		}

		/// <summary>Creates a new 5-tuple, or quintuple.</summary>
		/// <returns>A 5-tuple whose value is (<paramref name="item1" />, <paramref name="item2" />, <paramref name="item3" />, <paramref name="item4" />, <paramref name="item5" />).</returns>
		/// <param name="item1">The value of the first component of the tuple.</param>
		/// <param name="item2">The value of the second component of the tuple.</param>
		/// <param name="item3">The value of the third component of the tuple.</param>
		/// <param name="item4">The value of the fourth component of the tuple.</param>
		/// <param name="item5">The value of the fifth component of the tuple.</param>
		/// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
		/// <typeparam name="T2">The type of the second component of the tuple.</typeparam>
		/// <typeparam name="T3">The type of the third component of the tuple.</typeparam>
		/// <typeparam name="T4">The type of the fourth component of the tuple.</typeparam>
		/// <typeparam name="T5">The type of the fifth component of the tuple.</typeparam>
		// Token: 0x060008AE RID: 2222 RVA: 0x0002ED0F File Offset: 0x0002CF0F
		public static Tuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			return new Tuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
		}

		/// <summary>Creates a new 6-tuple, or sextuple.</summary>
		/// <returns>A 6-tuple whose value is (<paramref name="item1" />, <paramref name="item2" />, <paramref name="item3" />, <paramref name="item4" />, <paramref name="item5" />, <paramref name="item6" />).</returns>
		/// <param name="item1">The value of the first component of the tuple.</param>
		/// <param name="item2">The value of the second component of the tuple.</param>
		/// <param name="item3">The value of the third component of the tuple.</param>
		/// <param name="item4">The value of the fourth component of the tuple.</param>
		/// <param name="item5">The value of the fifth component of the tuple.</param>
		/// <param name="item6">The value of the sixth component of the tuple.</param>
		/// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
		/// <typeparam name="T2">The type of the second component of the tuple.</typeparam>
		/// <typeparam name="T3">The type of the third component of the tuple.</typeparam>
		/// <typeparam name="T4">The type of the fourth component of the tuple.</typeparam>
		/// <typeparam name="T5">The type of the fifth component of the tuple.</typeparam>
		/// <typeparam name="T6">The type of the sixth component of the tuple.</typeparam>
		// Token: 0x060008AF RID: 2223 RVA: 0x0002ED1C File Offset: 0x0002CF1C
		public static Tuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			return new Tuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
		}

		/// <summary>Creates a new 7-tuple, or septuple.</summary>
		/// <returns>A 7-tuple whose value is (<paramref name="item1" />, <paramref name="item2" />, <paramref name="item3" />, <paramref name="item4" />, <paramref name="item5" />, <paramref name="item6" />, <paramref name="item7" />).</returns>
		/// <param name="item1">The value of the first component of the tuple.</param>
		/// <param name="item2">The value of the second component of the tuple.</param>
		/// <param name="item3">The value of the third component of the tuple.</param>
		/// <param name="item4">The value of the fourth component of the tuple.</param>
		/// <param name="item5">The value of the fifth component of the tuple.</param>
		/// <param name="item6">The value of the sixth component of the tuple.</param>
		/// <param name="item7">The value of the seventh component of the tuple.</param>
		/// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
		/// <typeparam name="T2">The type of the second component of the tuple.</typeparam>
		/// <typeparam name="T3">The type of the third component of the tuple.</typeparam>
		/// <typeparam name="T4">The type of the fourth component of the tuple.</typeparam>
		/// <typeparam name="T5">The type of the fifth component of the tuple.</typeparam>
		/// <typeparam name="T6">The type of the sixth component of the tuple.</typeparam>
		/// <typeparam name="T7">The type of the seventh component of the tuple.</typeparam>
		// Token: 0x060008B0 RID: 2224 RVA: 0x0002ED2B File Offset: 0x0002CF2B
		public static Tuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			return new Tuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
		}

		/// <summary>Creates a new 8-tuple, or octuple.</summary>
		/// <returns>An 8-tuple (octuple) whose value is (<paramref name="item1" />, <paramref name="item2" />, <paramref name="item3" />, <paramref name="item4" />, <paramref name="item5" />, <paramref name="item6" />, <paramref name="item7" />, <paramref name="item8" />). </returns>
		/// <param name="item1">The value of the first component of the tuple.</param>
		/// <param name="item2">The value of the second component of the tuple.</param>
		/// <param name="item3">The value of the third component of the tuple.</param>
		/// <param name="item4">The value of the fourth component of the tuple.</param>
		/// <param name="item5">The value of the fifth component of the tuple.</param>
		/// <param name="item6">The value of the sixth component of the tuple.</param>
		/// <param name="item7">The value of the seventh component of the tuple.</param>
		/// <param name="item8">The value of the eighth component of the tuple.</param>
		/// <typeparam name="T1">The type of the first component of the tuple.</typeparam>
		/// <typeparam name="T2">The type of the second component of the tuple.</typeparam>
		/// <typeparam name="T3">The type of the third component of the tuple.</typeparam>
		/// <typeparam name="T4">The type of the fourth component of the tuple.</typeparam>
		/// <typeparam name="T5">The type of the fifth component of the tuple.</typeparam>
		/// <typeparam name="T6">The type of the sixth component of the tuple.</typeparam>
		/// <typeparam name="T7">The type of the seventh component of the tuple.</typeparam>
		/// <typeparam name="T8">The type of the eighth component of the tuple.</typeparam>
		// Token: 0x060008B1 RID: 2225 RVA: 0x0002ED3C File Offset: 0x0002CF3C
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
		{
			return new Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>(item1, item2, item3, item4, item5, item6, item7, new Tuple<T8>(item8));
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x000224D3 File Offset: 0x000206D3
		internal static int CombineHashCodes(int h1, int h2)
		{
			return ((h1 << 5) + h1) ^ h2;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0002ED54 File Offset: 0x0002CF54
		internal static int CombineHashCodes(int h1, int h2, int h3)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0002ED63 File Offset: 0x0002CF63
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2), Tuple.CombineHashCodes(h3, h4));
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0002ED78 File Offset: 0x0002CF78
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), h5);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0002ED8A File Offset: 0x0002CF8A
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), Tuple.CombineHashCodes(h5, h6));
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0002EDA3 File Offset: 0x0002CFA3
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), Tuple.CombineHashCodes(h5, h6, h7));
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0002EDBE File Offset: 0x0002CFBE
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), Tuple.CombineHashCodes(h5, h6, h7, h8));
		}
	}
}
