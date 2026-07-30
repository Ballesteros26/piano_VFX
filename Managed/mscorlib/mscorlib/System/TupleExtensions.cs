using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000D6 RID: 214
	public static class TupleExtensions
	{
		// Token: 0x0600070F RID: 1807 RVA: 0x0002560B File Offset: 0x0002380B
		public static void Deconstruct<T1>(this Tuple<T1> value, out T1 item1)
		{
			item1 = value.Item1;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00025619 File Offset: 0x00023819
		public static void Deconstruct<T1, T2>(this Tuple<T1, T2> value, out T1 item1, out T2 item2)
		{
			item1 = value.Item1;
			item2 = value.Item2;
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00025633 File Offset: 0x00023833
		public static void Deconstruct<T1, T2, T3>(this Tuple<T1, T2, T3> value, out T1 item1, out T2 item2, out T3 item3)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00025659 File Offset: 0x00023859
		public static void Deconstruct<T1, T2, T3, T4>(this Tuple<T1, T2, T3, T4> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0002568C File Offset: 0x0002388C
		public static void Deconstruct<T1, T2, T3, T4, T5>(this Tuple<T1, T2, T3, T4, T5> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000256CC File Offset: 0x000238CC
		public static void Deconstruct<T1, T2, T3, T4, T5, T6>(this Tuple<T1, T2, T3, T4, T5, T6> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00025724 File Offset: 0x00023924
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7>(this Tuple<T1, T2, T3, T4, T5, T6, T7> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0002578C File Offset: 0x0002398C
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00025804 File Offset: 0x00023A04
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00025890 File Offset: 0x00023A90
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0002592C File Offset: 0x00023B2C
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x000259DC File Offset: 0x00023BDC
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00025A9C File Offset: 0x00023C9C
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
			item13 = value.Rest.Item6;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00025B70 File Offset: 0x00023D70
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
			item13 = value.Rest.Item6;
			item14 = value.Rest.Item7;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00025C54 File Offset: 0x00023E54
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
			item13 = value.Rest.Item6;
			item14 = value.Rest.Item7;
			item15 = value.Rest.Rest.Item1;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00025D50 File Offset: 0x00023F50
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
			item13 = value.Rest.Item6;
			item14 = value.Rest.Item7;
			item15 = value.Rest.Rest.Item1;
			item16 = value.Rest.Rest.Item2;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00025E64 File Offset: 0x00024064
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
			item13 = value.Rest.Item6;
			item14 = value.Rest.Item7;
			item15 = value.Rest.Rest.Item1;
			item16 = value.Rest.Rest.Item2;
			item17 = value.Rest.Rest.Item3;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00025F8C File Offset: 0x0002418C
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
			item13 = value.Rest.Item6;
			item14 = value.Rest.Item7;
			item15 = value.Rest.Rest.Item1;
			item16 = value.Rest.Rest.Item2;
			item17 = value.Rest.Rest.Item3;
			item18 = value.Rest.Rest.Item4;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x000260CC File Offset: 0x000242CC
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18, out T19 item19)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
			item13 = value.Rest.Item6;
			item14 = value.Rest.Item7;
			item15 = value.Rest.Rest.Item1;
			item16 = value.Rest.Rest.Item2;
			item17 = value.Rest.Rest.Item3;
			item18 = value.Rest.Rest.Item4;
			item19 = value.Rest.Rest.Item5;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00026224 File Offset: 0x00024424
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18, out T19 item19, out T20 item20)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
			item13 = value.Rest.Item6;
			item14 = value.Rest.Item7;
			item15 = value.Rest.Rest.Item1;
			item16 = value.Rest.Rest.Item2;
			item17 = value.Rest.Rest.Item3;
			item18 = value.Rest.Rest.Item4;
			item19 = value.Rest.Rest.Item5;
			item20 = value.Rest.Rest.Item6;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00026394 File Offset: 0x00024594
		public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>> value, out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18, out T19 item19, out T20 item20, out T21 item21)
		{
			item1 = value.Item1;
			item2 = value.Item2;
			item3 = value.Item3;
			item4 = value.Item4;
			item5 = value.Item5;
			item6 = value.Item6;
			item7 = value.Item7;
			item8 = value.Rest.Item1;
			item9 = value.Rest.Item2;
			item10 = value.Rest.Item3;
			item11 = value.Rest.Item4;
			item12 = value.Rest.Item5;
			item13 = value.Rest.Item6;
			item14 = value.Rest.Item7;
			item15 = value.Rest.Rest.Item1;
			item16 = value.Rest.Rest.Item2;
			item17 = value.Rest.Rest.Item3;
			item18 = value.Rest.Rest.Item4;
			item19 = value.Rest.Rest.Item5;
			item20 = value.Rest.Rest.Item6;
			item21 = value.Rest.Rest.Item7;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00026518 File Offset: 0x00024718
		public static ValueTuple<T1> ToValueTuple<T1>(this Tuple<T1> value)
		{
			return ValueTuple.Create<T1>(value.Item1);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00026525 File Offset: 0x00024725
		public static ValueTuple<T1, T2> ToValueTuple<T1, T2>(this Tuple<T1, T2> value)
		{
			return ValueTuple.Create<T1, T2>(value.Item1, value.Item2);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00026538 File Offset: 0x00024738
		public static ValueTuple<T1, T2, T3> ToValueTuple<T1, T2, T3>(this Tuple<T1, T2, T3> value)
		{
			return ValueTuple.Create<T1, T2, T3>(value.Item1, value.Item2, value.Item3);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00026551 File Offset: 0x00024751
		public static ValueTuple<T1, T2, T3, T4> ToValueTuple<T1, T2, T3, T4>(this Tuple<T1, T2, T3, T4> value)
		{
			return ValueTuple.Create<T1, T2, T3, T4>(value.Item1, value.Item2, value.Item3, value.Item4);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00026570 File Offset: 0x00024770
		public static ValueTuple<T1, T2, T3, T4, T5> ToValueTuple<T1, T2, T3, T4, T5>(this Tuple<T1, T2, T3, T4, T5> value)
		{
			return ValueTuple.Create<T1, T2, T3, T4, T5>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00026595 File Offset: 0x00024795
		public static ValueTuple<T1, T2, T3, T4, T5, T6> ToValueTuple<T1, T2, T3, T4, T5, T6>(this Tuple<T1, T2, T3, T4, T5, T6> value)
		{
			return ValueTuple.Create<T1, T2, T3, T4, T5, T6>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x000265C0 File Offset: 0x000247C0
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7> ToValueTuple<T1, T2, T3, T4, T5, T6, T7>(this Tuple<T1, T2, T3, T4, T5, T6, T7> value)
		{
			return ValueTuple.Create<T1, T2, T3, T4, T5, T6, T7>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x000265F4 File Offset: 0x000247F4
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8>(value.Rest.Item1));
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00026640 File Offset: 0x00024840
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8, T9>(value.Rest.Item1, value.Rest.Item2));
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00026698 File Offset: 0x00024898
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8, T9, T10>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3));
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x000266FC File Offset: 0x000248FC
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8, T9, T10, T11>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4));
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0002676C File Offset: 0x0002496C
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8, T9, T10, T11, T12>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5));
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x000267E4 File Offset: 0x000249E4
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8, T9, T10, T11, T12, T13>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6));
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00026868 File Offset: 0x00024A68
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, ValueTuple.Create<T8, T9, T10, T11, T12, T13, T14>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7));
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x000268F8 File Offset: 0x00024AF8
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15>(value.Rest.Rest.Item1)));
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0002699C File Offset: 0x00024B9C
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15, T16>(value.Rest.Rest.Item1, value.Rest.Rest.Item2)));
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00026A50 File Offset: 0x00024C50
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15, T16, T17>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3)));
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00026B14 File Offset: 0x00024D14
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15, T16, T17, T18>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4)));
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00026BE8 File Offset: 0x00024DE8
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15, T16, T17, T18, T19>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5)));
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00026CCC File Offset: 0x00024ECC
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15, T16, T17, T18, T19, T20>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6)));
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00026DC0 File Offset: 0x00024FC0
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20, T21>>> ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>> value)
		{
			return TupleExtensions.CreateLong<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20, T21>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLong<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20, T21>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, ValueTuple.Create<T15, T16, T17, T18, T19, T20, T21>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6, value.Rest.Rest.Item7)));
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00026EC3 File Offset: 0x000250C3
		public static Tuple<T1> ToTuple<T1>(this ValueTuple<T1> value)
		{
			return Tuple.Create<T1>(value.Item1);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00026ED0 File Offset: 0x000250D0
		public static Tuple<T1, T2> ToTuple<T1, T2>(this ValueTuple<T1, T2> value)
		{
			return Tuple.Create<T1, T2>(value.Item1, value.Item2);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00026EE3 File Offset: 0x000250E3
		public static Tuple<T1, T2, T3> ToTuple<T1, T2, T3>(this ValueTuple<T1, T2, T3> value)
		{
			return Tuple.Create<T1, T2, T3>(value.Item1, value.Item2, value.Item3);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00026EFC File Offset: 0x000250FC
		public static Tuple<T1, T2, T3, T4> ToTuple<T1, T2, T3, T4>(this ValueTuple<T1, T2, T3, T4> value)
		{
			return Tuple.Create<T1, T2, T3, T4>(value.Item1, value.Item2, value.Item3, value.Item4);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00026F1B File Offset: 0x0002511B
		public static Tuple<T1, T2, T3, T4, T5> ToTuple<T1, T2, T3, T4, T5>(this ValueTuple<T1, T2, T3, T4, T5> value)
		{
			return Tuple.Create<T1, T2, T3, T4, T5>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00026F40 File Offset: 0x00025140
		public static Tuple<T1, T2, T3, T4, T5, T6> ToTuple<T1, T2, T3, T4, T5, T6>(this ValueTuple<T1, T2, T3, T4, T5, T6> value)
		{
			return Tuple.Create<T1, T2, T3, T4, T5, T6>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00026F6B File Offset: 0x0002516B
		public static Tuple<T1, T2, T3, T4, T5, T6, T7> ToTuple<T1, T2, T3, T4, T5, T6, T7>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7> value)
		{
			return Tuple.Create<T1, T2, T3, T4, T5, T6, T7>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00026F9C File Offset: 0x0002519C
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8>(value.Rest.Item1));
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00026FE8 File Offset: 0x000251E8
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8, T9>(value.Rest.Item1, value.Rest.Item2));
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00027040 File Offset: 0x00025240
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8, T9, T10>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3));
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x000270A4 File Offset: 0x000252A4
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8, T9, T10, T11>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4));
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00027114 File Offset: 0x00025314
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8, T9, T10, T11, T12>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5));
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0002718C File Offset: 0x0002538C
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8, T9, T10, T11, T12, T13>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6));
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00027210 File Offset: 0x00025410
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, Tuple.Create<T8, T9, T10, T11, T12, T13, T14>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7));
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x000272A0 File Offset: 0x000254A0
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15>(value.Rest.Rest.Item1)));
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00027344 File Offset: 0x00025544
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16>>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15, T16>(value.Rest.Rest.Item1, value.Rest.Rest.Item2)));
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000273F8 File Offset: 0x000255F8
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17>>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15, T16, T17>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3)));
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x000274BC File Offset: 0x000256BC
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18>>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15, T16, T17, T18>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4)));
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00027590 File Offset: 0x00025790
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19>>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15, T16, T17, T18, T19>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5)));
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00027674 File Offset: 0x00025874
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20>>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15, T16, T17, T18, T19, T20>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6)));
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00027768 File Offset: 0x00025968
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>> ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20, T21>>> value)
		{
			return TupleExtensions.CreateLongRef<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>>(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7, TupleExtensions.CreateLongRef<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7, Tuple.Create<T15, T16, T17, T18, T19, T20, T21>(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6, value.Rest.Rest.Item7)));
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0002786B File Offset: 0x00025A6B
		private static ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> CreateLong<T1, T2, T3, T4, T5, T6, T7, TRest>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest) where TRest : struct, ITuple
		{
			return new ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>(item1, item2, item3, item4, item5, item6, item7, rest);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0002787E File Offset: 0x00025A7E
		private static Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> CreateLongRef<T1, T2, T3, T4, T5, T6, T7, TRest>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest) where TRest : ITuple
		{
			return new Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>(item1, item2, item3, item4, item5, item6, item7, rest);
		}
	}
}
