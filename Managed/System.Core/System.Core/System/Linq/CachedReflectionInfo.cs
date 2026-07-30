using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq
{
	// Token: 0x020000A2 RID: 162
	internal static class CachedReflectionInfo
	{
		// Token: 0x0600044C RID: 1100 RVA: 0x0000AA16 File Offset: 0x00008C16
		public static MethodInfo Aggregate_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Aggregate_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Aggregate_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, object, object>>, object>(Queryable.Aggregate<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000AA4C File Offset: 0x00008C4C
		public static MethodInfo Aggregate_TSource_TAccumulate_3(Type TSource, Type TAccumulate)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Aggregate_TSource_TAccumulate_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Aggregate_TSource_TAccumulate_3 = new Func<IQueryable<object>, object, Expression<Func<object, object, object>>, object>(Queryable.Aggregate<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TAccumulate });
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000AA86 File Offset: 0x00008C86
		public static MethodInfo Aggregate_TSource_TAccumulate_TResult_4(Type TSource, Type TAccumulate, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Aggregate_TSource_TAccumulate_TResult_4) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Aggregate_TSource_TAccumulate_TResult_4 = new Func<IQueryable<object>, object, Expression<Func<object, object, object>>, Expression<Func<object, object>>, object>(Queryable.Aggregate<object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TAccumulate, TResult });
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000AAC4 File Offset: 0x00008CC4
		public static MethodInfo All_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_All_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_All_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, bool>(Queryable.All<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000AAFA File Offset: 0x00008CFA
		public static MethodInfo Any_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Any_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Any_TSource_1 = new Func<IQueryable<object>, bool>(Queryable.Any<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000AB30 File Offset: 0x00008D30
		public static MethodInfo Any_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Any_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Any_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, bool>(Queryable.Any<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0000AB66 File Offset: 0x00008D66
		public static MethodInfo Average_Int32_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Average_Int32_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Average_Int32_1 = new Func<IQueryable<int>, double>(Queryable.Average).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000AB88 File Offset: 0x00008D88
		public static MethodInfo Average_NullableInt32_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Average_NullableInt32_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Average_NullableInt32_1 = new Func<IQueryable<int?>, double?>(Queryable.Average).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0000ABAA File Offset: 0x00008DAA
		public static MethodInfo Average_Int64_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Average_Int64_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Average_Int64_1 = new Func<IQueryable<long>, double>(Queryable.Average).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0000ABCC File Offset: 0x00008DCC
		public static MethodInfo Average_NullableInt64_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Average_NullableInt64_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Average_NullableInt64_1 = new Func<IQueryable<long?>, double?>(Queryable.Average).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0000ABEE File Offset: 0x00008DEE
		public static MethodInfo Average_Single_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Average_Single_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Average_Single_1 = new Func<IQueryable<float>, float>(Queryable.Average).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000AC10 File Offset: 0x00008E10
		public static MethodInfo Average_NullableSingle_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Average_NullableSingle_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Average_NullableSingle_1 = new Func<IQueryable<float?>, float?>(Queryable.Average).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000AC32 File Offset: 0x00008E32
		public static MethodInfo Average_Double_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Average_Double_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Average_Double_1 = new Func<IQueryable<double>, double>(Queryable.Average).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000AC54 File Offset: 0x00008E54
		public static MethodInfo Average_NullableDouble_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Average_NullableDouble_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Average_NullableDouble_1 = new Func<IQueryable<double?>, double?>(Queryable.Average).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000AC76 File Offset: 0x00008E76
		public static MethodInfo Average_Decimal_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Average_Decimal_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Average_Decimal_1 = new Func<IQueryable<decimal>, decimal>(Queryable.Average).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000AC98 File Offset: 0x00008E98
		public static MethodInfo Average_NullableDecimal_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Average_NullableDecimal_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Average_NullableDecimal_1 = new Func<IQueryable<decimal?>, decimal?>(Queryable.Average).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000ACBA File Offset: 0x00008EBA
		public static MethodInfo Average_Int32_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_Int32_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_Int32_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int>>, double>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000ACF0 File Offset: 0x00008EF0
		public static MethodInfo Average_NullableInt32_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_NullableInt32_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_NullableInt32_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int?>>, double?>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000AD26 File Offset: 0x00008F26
		public static MethodInfo Average_Single_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_Single_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_Single_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, float>>, float>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000AD5C File Offset: 0x00008F5C
		public static MethodInfo Average_NullableSingle_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_NullableSingle_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_NullableSingle_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, float?>>, float?>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000AD92 File Offset: 0x00008F92
		public static MethodInfo Average_Int64_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_Int64_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_Int64_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, long>>, double>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000ADC8 File Offset: 0x00008FC8
		public static MethodInfo Average_NullableInt64_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_NullableInt64_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_NullableInt64_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, long?>>, double?>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000ADFE File Offset: 0x00008FFE
		public static MethodInfo Average_Double_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_Double_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_Double_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, double>>, double>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000AE34 File Offset: 0x00009034
		public static MethodInfo Average_NullableDouble_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_NullableDouble_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_NullableDouble_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, double?>>, double?>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000AE6A File Offset: 0x0000906A
		public static MethodInfo Average_Decimal_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_Decimal_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_Decimal_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, decimal>>, decimal>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000AEA0 File Offset: 0x000090A0
		public static MethodInfo Average_NullableDecimal_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Average_NullableDecimal_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Average_NullableDecimal_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, decimal?>>, decimal?>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000AED6 File Offset: 0x000090D6
		public static MethodInfo Cast_TResult_1(Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Cast_TResult_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Cast_TResult_1 = new Func<IQueryable, IQueryable<object>>(Queryable.Cast<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TResult });
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000AF0C File Offset: 0x0000910C
		public static MethodInfo Concat_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Concat_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Concat_TSource_2 = new Func<IQueryable<object>, IEnumerable<object>, IQueryable<object>>(Queryable.Concat<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000AF42 File Offset: 0x00009142
		public static MethodInfo Contains_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Contains_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Contains_TSource_2 = new Func<IQueryable<object>, object, bool>(Queryable.Contains<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000AF78 File Offset: 0x00009178
		public static MethodInfo Contains_TSource_3(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Contains_TSource_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Contains_TSource_3 = new Func<IQueryable<object>, object, IEqualityComparer<object>, bool>(Queryable.Contains<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000AFAE File Offset: 0x000091AE
		public static MethodInfo Count_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Count_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Count_TSource_1 = new Func<IQueryable<object>, int>(Queryable.Count<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000AFE4 File Offset: 0x000091E4
		public static MethodInfo Count_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Count_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Count_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, int>(Queryable.Count<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000B01A File Offset: 0x0000921A
		public static MethodInfo DefaultIfEmpty_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_DefaultIfEmpty_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_DefaultIfEmpty_TSource_1 = new Func<IQueryable<object>, IQueryable<object>>(Queryable.DefaultIfEmpty<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000B050 File Offset: 0x00009250
		public static MethodInfo DefaultIfEmpty_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_DefaultIfEmpty_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_DefaultIfEmpty_TSource_2 = new Func<IQueryable<object>, object, IQueryable<object>>(Queryable.DefaultIfEmpty<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000B086 File Offset: 0x00009286
		public static MethodInfo Distinct_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Distinct_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Distinct_TSource_1 = new Func<IQueryable<object>, IQueryable<object>>(Queryable.Distinct<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000B0BC File Offset: 0x000092BC
		public static MethodInfo Distinct_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Distinct_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Distinct_TSource_2 = new Func<IQueryable<object>, IEqualityComparer<object>, IQueryable<object>>(Queryable.Distinct<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000B0F2 File Offset: 0x000092F2
		public static MethodInfo ElementAt_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_ElementAt_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_ElementAt_TSource_2 = new Func<IQueryable<object>, int, object>(Queryable.ElementAt<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000B128 File Offset: 0x00009328
		public static MethodInfo ElementAtOrDefault_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_ElementAtOrDefault_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_ElementAtOrDefault_TSource_2 = new Func<IQueryable<object>, int, object>(Queryable.ElementAtOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000B15E File Offset: 0x0000935E
		public static MethodInfo Except_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Except_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Except_TSource_2 = new Func<IQueryable<object>, IEnumerable<object>, IQueryable<object>>(Queryable.Except<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000B194 File Offset: 0x00009394
		public static MethodInfo Except_TSource_3(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Except_TSource_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Except_TSource_3 = new Func<IQueryable<object>, IEnumerable<object>, IEqualityComparer<object>, IQueryable<object>>(Queryable.Except<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000B1CA File Offset: 0x000093CA
		public static MethodInfo First_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_First_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_First_TSource_1 = new Func<IQueryable<object>, object>(Queryable.First<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000B200 File Offset: 0x00009400
		public static MethodInfo First_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_First_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_First_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, object>(Queryable.First<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000B236 File Offset: 0x00009436
		public static MethodInfo FirstOrDefault_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_FirstOrDefault_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_FirstOrDefault_TSource_1 = new Func<IQueryable<object>, object>(Queryable.FirstOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000B26C File Offset: 0x0000946C
		public static MethodInfo FirstOrDefault_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_FirstOrDefault_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_FirstOrDefault_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, object>(Queryable.FirstOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000B2A2 File Offset: 0x000094A2
		public static MethodInfo GroupBy_TSource_TKey_2(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupBy_TSource_TKey_2 = new Func<IQueryable<object>, Expression<Func<object, object>>, IQueryable<IGrouping<object, object>>>(Queryable.GroupBy<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TKey });
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000B2DC File Offset: 0x000094DC
		public static MethodInfo GroupBy_TSource_TKey_3(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupBy_TSource_TKey_3 = new Func<IQueryable<object>, Expression<Func<object, object>>, IEqualityComparer<object>, IQueryable<IGrouping<object, object>>>(Queryable.GroupBy<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TKey });
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000B316 File Offset: 0x00009516
		public static MethodInfo GroupBy_TSource_TKey_TElement_3(Type TSource, Type TKey, Type TElement)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_3 = new Func<IQueryable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, IQueryable<IGrouping<object, object>>>(Queryable.GroupBy<object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TKey, TElement });
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000B354 File Offset: 0x00009554
		public static MethodInfo GroupBy_TSource_TKey_TElement_4(Type TSource, Type TKey, Type TElement)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_4) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_4 = new Func<IQueryable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, IEqualityComparer<object>, IQueryable<IGrouping<object, object>>>(Queryable.GroupBy<object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TKey, TElement });
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000B392 File Offset: 0x00009592
		public static MethodInfo GroupBy_TSource_TKey_TResult_3(Type TSource, Type TKey, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TResult_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupBy_TSource_TKey_TResult_3 = new Func<IQueryable<object>, Expression<Func<object, object>>, Expression<Func<object, IEnumerable<object>, object>>, IQueryable<object>>(Queryable.GroupBy<object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TKey, TResult });
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000B3D0 File Offset: 0x000095D0
		public static MethodInfo GroupBy_TSource_TKey_TResult_4(Type TSource, Type TKey, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TResult_4) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupBy_TSource_TKey_TResult_4 = new Func<IQueryable<object>, Expression<Func<object, object>>, Expression<Func<object, IEnumerable<object>, object>>, IEqualityComparer<object>, IQueryable<object>>(Queryable.GroupBy<object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TKey, TResult });
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000B410 File Offset: 0x00009610
		public static MethodInfo GroupBy_TSource_TKey_TElement_TResult_4(Type TSource, Type TKey, Type TElement, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_TResult_4) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_TResult_4 = new Func<IQueryable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, Expression<Func<object, IEnumerable<object>, object>>, IQueryable<object>>(Queryable.GroupBy<object, object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TKey, TElement, TResult });
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000B460 File Offset: 0x00009660
		public static MethodInfo GroupBy_TSource_TKey_TElement_TResult_5(Type TSource, Type TKey, Type TElement, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_TResult_5) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_TResult_5 = new Func<IQueryable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, Expression<Func<object, IEnumerable<object>, object>>, IEqualityComparer<object>, IQueryable<object>>(Queryable.GroupBy<object, object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TKey, TElement, TResult });
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000B4B0 File Offset: 0x000096B0
		public static MethodInfo GroupJoin_TOuter_TInner_TKey_TResult_5(Type TOuter, Type TInner, Type TKey, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupJoin_TOuter_TInner_TKey_TResult_5) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupJoin_TOuter_TInner_TKey_TResult_5 = new Func<IQueryable<object>, IEnumerable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, Expression<Func<object, IEnumerable<object>, object>>, IQueryable<object>>(Queryable.GroupJoin<object, object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TOuter, TInner, TKey, TResult });
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000B500 File Offset: 0x00009700
		public static MethodInfo GroupJoin_TOuter_TInner_TKey_TResult_6(Type TOuter, Type TInner, Type TKey, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_GroupJoin_TOuter_TInner_TKey_TResult_6) == null)
			{
				methodInfo = (CachedReflectionInfo.s_GroupJoin_TOuter_TInner_TKey_TResult_6 = new Func<IQueryable<object>, IEnumerable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, Expression<Func<object, IEnumerable<object>, object>>, IEqualityComparer<object>, IQueryable<object>>(Queryable.GroupJoin<object, object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TOuter, TInner, TKey, TResult });
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000B54D File Offset: 0x0000974D
		public static MethodInfo Intersect_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Intersect_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Intersect_TSource_2 = new Func<IQueryable<object>, IEnumerable<object>, IQueryable<object>>(Queryable.Intersect<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000B583 File Offset: 0x00009783
		public static MethodInfo Intersect_TSource_3(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Intersect_TSource_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Intersect_TSource_3 = new Func<IQueryable<object>, IEnumerable<object>, IEqualityComparer<object>, IQueryable<object>>(Queryable.Intersect<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000B5BC File Offset: 0x000097BC
		public static MethodInfo Join_TOuter_TInner_TKey_TResult_5(Type TOuter, Type TInner, Type TKey, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Join_TOuter_TInner_TKey_TResult_5) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Join_TOuter_TInner_TKey_TResult_5 = new Func<IQueryable<object>, IEnumerable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, Expression<Func<object, object, object>>, IQueryable<object>>(Queryable.Join<object, object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TOuter, TInner, TKey, TResult });
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000B60C File Offset: 0x0000980C
		public static MethodInfo Join_TOuter_TInner_TKey_TResult_6(Type TOuter, Type TInner, Type TKey, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Join_TOuter_TInner_TKey_TResult_6) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Join_TOuter_TInner_TKey_TResult_6 = new Func<IQueryable<object>, IEnumerable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, Expression<Func<object, object, object>>, IEqualityComparer<object>, IQueryable<object>>(Queryable.Join<object, object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TOuter, TInner, TKey, TResult });
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000B659 File Offset: 0x00009859
		public static MethodInfo Last_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Last_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Last_TSource_1 = new Func<IQueryable<object>, object>(Queryable.Last<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000B68F File Offset: 0x0000988F
		public static MethodInfo Last_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Last_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Last_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, object>(Queryable.Last<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000B6C5 File Offset: 0x000098C5
		public static MethodInfo LastOrDefault_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_LastOrDefault_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_LastOrDefault_TSource_1 = new Func<IQueryable<object>, object>(Queryable.LastOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000B6FB File Offset: 0x000098FB
		public static MethodInfo LastOrDefault_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_LastOrDefault_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_LastOrDefault_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, object>(Queryable.LastOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000B731 File Offset: 0x00009931
		public static MethodInfo LongCount_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_LongCount_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_LongCount_TSource_1 = new Func<IQueryable<object>, long>(Queryable.LongCount<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000B767 File Offset: 0x00009967
		public static MethodInfo LongCount_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_LongCount_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_LongCount_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, long>(Queryable.LongCount<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000B79D File Offset: 0x0000999D
		public static MethodInfo Max_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Max_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Max_TSource_1 = new Func<IQueryable<object>, object>(Queryable.Max<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000B7D3 File Offset: 0x000099D3
		public static MethodInfo Max_TSource_TResult_2(Type TSource, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Max_TSource_TResult_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Max_TSource_TResult_2 = new Func<IQueryable<object>, Expression<Func<object, object>>, object>(Queryable.Max<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TResult });
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000B80D File Offset: 0x00009A0D
		public static MethodInfo Min_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Min_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Min_TSource_1 = new Func<IQueryable<object>, object>(Queryable.Min<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000B843 File Offset: 0x00009A43
		public static MethodInfo Min_TSource_TResult_2(Type TSource, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Min_TSource_TResult_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Min_TSource_TResult_2 = new Func<IQueryable<object>, Expression<Func<object, object>>, object>(Queryable.Min<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TResult });
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000B87D File Offset: 0x00009A7D
		public static MethodInfo OfType_TResult_1(Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_OfType_TResult_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_OfType_TResult_1 = new Func<IQueryable, IQueryable<object>>(Queryable.OfType<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TResult });
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000B8B3 File Offset: 0x00009AB3
		public static MethodInfo OrderBy_TSource_TKey_2(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_OrderBy_TSource_TKey_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_OrderBy_TSource_TKey_2 = new Func<IQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(Queryable.OrderBy<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TKey });
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000B8ED File Offset: 0x00009AED
		public static MethodInfo OrderBy_TSource_TKey_3(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_OrderBy_TSource_TKey_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_OrderBy_TSource_TKey_3 = new Func<IQueryable<object>, Expression<Func<object, object>>, IComparer<object>, IOrderedQueryable<object>>(Queryable.OrderBy<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TKey });
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000B927 File Offset: 0x00009B27
		public static MethodInfo OrderByDescending_TSource_TKey_2(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_OrderByDescending_TSource_TKey_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_OrderByDescending_TSource_TKey_2 = new Func<IQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(Queryable.OrderByDescending<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TKey });
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000B961 File Offset: 0x00009B61
		public static MethodInfo OrderByDescending_TSource_TKey_3(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_OrderByDescending_TSource_TKey_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_OrderByDescending_TSource_TKey_3 = new Func<IQueryable<object>, Expression<Func<object, object>>, IComparer<object>, IOrderedQueryable<object>>(Queryable.OrderByDescending<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TKey });
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000B99B File Offset: 0x00009B9B
		public static MethodInfo Reverse_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Reverse_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Reverse_TSource_1 = new Func<IQueryable<object>, IQueryable<object>>(Queryable.Reverse<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000B9D1 File Offset: 0x00009BD1
		public static MethodInfo Select_TSource_TResult_2(Type TSource, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Select_TSource_TResult_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Select_TSource_TResult_2 = new Func<IQueryable<object>, Expression<Func<object, object>>, IQueryable<object>>(Queryable.Select<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TResult });
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000BA0B File Offset: 0x00009C0B
		public static MethodInfo Select_Index_TSource_TResult_2(Type TSource, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Select_Index_TSource_TResult_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Select_Index_TSource_TResult_2 = new Func<IQueryable<object>, Expression<Func<object, int, object>>, IQueryable<object>>(Queryable.Select<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TResult });
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000BA45 File Offset: 0x00009C45
		public static MethodInfo SelectMany_TSource_TResult_2(Type TSource, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SelectMany_TSource_TResult_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SelectMany_TSource_TResult_2 = new Func<IQueryable<object>, Expression<Func<object, IEnumerable<object>>>, IQueryable<object>>(Queryable.SelectMany<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TResult });
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000BA7F File Offset: 0x00009C7F
		public static MethodInfo SelectMany_Index_TSource_TResult_2(Type TSource, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SelectMany_Index_TSource_TResult_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SelectMany_Index_TSource_TResult_2 = new Func<IQueryable<object>, Expression<Func<object, int, IEnumerable<object>>>, IQueryable<object>>(Queryable.SelectMany<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TResult });
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000BAB9 File Offset: 0x00009CB9
		public static MethodInfo SelectMany_Index_TSource_TCollection_TResult_3(Type TSource, Type TCollection, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SelectMany_Index_TSource_TCollection_TResult_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SelectMany_Index_TSource_TCollection_TResult_3 = new Func<IQueryable<object>, Expression<Func<object, int, IEnumerable<object>>>, Expression<Func<object, object, object>>, IQueryable<object>>(Queryable.SelectMany<object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TCollection, TResult });
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000BAF7 File Offset: 0x00009CF7
		public static MethodInfo SelectMany_TSource_TCollection_TResult_3(Type TSource, Type TCollection, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SelectMany_TSource_TCollection_TResult_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SelectMany_TSource_TCollection_TResult_3 = new Func<IQueryable<object>, Expression<Func<object, IEnumerable<object>>>, Expression<Func<object, object, object>>, IQueryable<object>>(Queryable.SelectMany<object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TCollection, TResult });
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000BB35 File Offset: 0x00009D35
		public static MethodInfo SequenceEqual_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SequenceEqual_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SequenceEqual_TSource_2 = new Func<IQueryable<object>, IEnumerable<object>, bool>(Queryable.SequenceEqual<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000BB6B File Offset: 0x00009D6B
		public static MethodInfo SequenceEqual_TSource_3(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SequenceEqual_TSource_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SequenceEqual_TSource_3 = new Func<IQueryable<object>, IEnumerable<object>, IEqualityComparer<object>, bool>(Queryable.SequenceEqual<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000BBA1 File Offset: 0x00009DA1
		public static MethodInfo Single_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Single_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Single_TSource_1 = new Func<IQueryable<object>, object>(Queryable.Single<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000BBD7 File Offset: 0x00009DD7
		public static MethodInfo Single_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Single_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Single_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, object>(Queryable.Single<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000BC0D File Offset: 0x00009E0D
		public static MethodInfo SingleOrDefault_TSource_1(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SingleOrDefault_TSource_1) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SingleOrDefault_TSource_1 = new Func<IQueryable<object>, object>(Queryable.SingleOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000BC43 File Offset: 0x00009E43
		public static MethodInfo SingleOrDefault_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SingleOrDefault_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SingleOrDefault_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, object>(Queryable.SingleOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000BC79 File Offset: 0x00009E79
		public static MethodInfo Skip_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Skip_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Skip_TSource_2 = new Func<IQueryable<object>, int, IQueryable<object>>(Queryable.Skip<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000BCAF File Offset: 0x00009EAF
		public static MethodInfo SkipWhile_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SkipWhile_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SkipWhile_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, IQueryable<object>>(Queryable.SkipWhile<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000BCE5 File Offset: 0x00009EE5
		public static MethodInfo SkipWhile_Index_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SkipWhile_Index_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SkipWhile_Index_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int, bool>>, IQueryable<object>>(Queryable.SkipWhile<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0000BD1B File Offset: 0x00009F1B
		public static MethodInfo Sum_Int32_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Sum_Int32_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Sum_Int32_1 = new Func<IQueryable<int>, int>(Queryable.Sum).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0000BD3D File Offset: 0x00009F3D
		public static MethodInfo Sum_NullableInt32_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Sum_NullableInt32_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Sum_NullableInt32_1 = new Func<IQueryable<int?>, int?>(Queryable.Sum).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000BD5F File Offset: 0x00009F5F
		public static MethodInfo Sum_Int64_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Sum_Int64_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Sum_Int64_1 = new Func<IQueryable<long>, long>(Queryable.Sum).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000BD81 File Offset: 0x00009F81
		public static MethodInfo Sum_NullableInt64_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Sum_NullableInt64_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Sum_NullableInt64_1 = new Func<IQueryable<long?>, long?>(Queryable.Sum).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000BDA3 File Offset: 0x00009FA3
		public static MethodInfo Sum_Single_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Sum_Single_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Sum_Single_1 = new Func<IQueryable<float>, float>(Queryable.Sum).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000BDC5 File Offset: 0x00009FC5
		public static MethodInfo Sum_NullableSingle_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Sum_NullableSingle_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Sum_NullableSingle_1 = new Func<IQueryable<float?>, float?>(Queryable.Sum).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000BDE7 File Offset: 0x00009FE7
		public static MethodInfo Sum_Double_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Sum_Double_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Sum_Double_1 = new Func<IQueryable<double>, double>(Queryable.Sum).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000BE09 File Offset: 0x0000A009
		public static MethodInfo Sum_NullableDouble_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Sum_NullableDouble_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Sum_NullableDouble_1 = new Func<IQueryable<double?>, double?>(Queryable.Sum).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000BE2B File Offset: 0x0000A02B
		public static MethodInfo Sum_Decimal_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Sum_Decimal_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Sum_Decimal_1 = new Func<IQueryable<decimal>, decimal>(Queryable.Sum).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0000BE4D File Offset: 0x0000A04D
		public static MethodInfo Sum_NullableDecimal_1
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Sum_NullableDecimal_1) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Sum_NullableDecimal_1 = new Func<IQueryable<decimal?>, decimal?>(Queryable.Sum).GetMethodInfo());
				}
				return methodInfo;
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000BE6F File Offset: 0x0000A06F
		public static MethodInfo Sum_NullableDecimal_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_NullableDecimal_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_NullableDecimal_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, decimal?>>, decimal?>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000BEA5 File Offset: 0x0000A0A5
		public static MethodInfo Sum_Int32_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_Int32_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_Int32_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int>>, int>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000BEDB File Offset: 0x0000A0DB
		public static MethodInfo Sum_NullableInt32_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_NullableInt32_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_NullableInt32_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int?>>, int?>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000BF11 File Offset: 0x0000A111
		public static MethodInfo Sum_Int64_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_Int64_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_Int64_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, long>>, long>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000BF47 File Offset: 0x0000A147
		public static MethodInfo Sum_NullableInt64_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_NullableInt64_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_NullableInt64_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, long?>>, long?>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000BF7D File Offset: 0x0000A17D
		public static MethodInfo Sum_Single_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_Single_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_Single_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, float>>, float>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000BFB3 File Offset: 0x0000A1B3
		public static MethodInfo Sum_NullableSingle_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_NullableSingle_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_NullableSingle_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, float?>>, float?>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000BFE9 File Offset: 0x0000A1E9
		public static MethodInfo Sum_Double_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_Double_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_Double_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, double>>, double>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000C01F File Offset: 0x0000A21F
		public static MethodInfo Sum_NullableDouble_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_NullableDouble_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_NullableDouble_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, double?>>, double?>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000C055 File Offset: 0x0000A255
		public static MethodInfo Sum_Decimal_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Sum_Decimal_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Sum_Decimal_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, decimal>>, decimal>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000C08B File Offset: 0x0000A28B
		public static MethodInfo Take_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Take_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Take_TSource_2 = new Func<IQueryable<object>, int, IQueryable<object>>(Queryable.Take<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000C0C1 File Offset: 0x0000A2C1
		public static MethodInfo TakeWhile_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_TakeWhile_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_TakeWhile_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, IQueryable<object>>(Queryable.TakeWhile<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000C0F7 File Offset: 0x0000A2F7
		public static MethodInfo TakeWhile_Index_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_TakeWhile_Index_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_TakeWhile_Index_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int, bool>>, IQueryable<object>>(Queryable.TakeWhile<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000C12D File Offset: 0x0000A32D
		public static MethodInfo ThenBy_TSource_TKey_2(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_ThenBy_TSource_TKey_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_ThenBy_TSource_TKey_2 = new Func<IOrderedQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(Queryable.ThenBy<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TKey });
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000C167 File Offset: 0x0000A367
		public static MethodInfo ThenBy_TSource_TKey_3(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_ThenBy_TSource_TKey_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_ThenBy_TSource_TKey_3 = new Func<IOrderedQueryable<object>, Expression<Func<object, object>>, IComparer<object>, IOrderedQueryable<object>>(Queryable.ThenBy<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TKey });
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000C1A1 File Offset: 0x0000A3A1
		public static MethodInfo ThenByDescending_TSource_TKey_2(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_ThenByDescending_TSource_TKey_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_ThenByDescending_TSource_TKey_2 = new Func<IOrderedQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(Queryable.ThenByDescending<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TKey });
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000C1DB File Offset: 0x0000A3DB
		public static MethodInfo ThenByDescending_TSource_TKey_3(Type TSource, Type TKey)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_ThenByDescending_TSource_TKey_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_ThenByDescending_TSource_TKey_3 = new Func<IOrderedQueryable<object>, Expression<Func<object, object>>, IComparer<object>, IOrderedQueryable<object>>(Queryable.ThenByDescending<object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource, TKey });
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000C215 File Offset: 0x0000A415
		public static MethodInfo Union_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Union_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Union_TSource_2 = new Func<IQueryable<object>, IEnumerable<object>, IQueryable<object>>(Queryable.Union<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000C24B File Offset: 0x0000A44B
		public static MethodInfo Union_TSource_3(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Union_TSource_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Union_TSource_3 = new Func<IQueryable<object>, IEnumerable<object>, IEqualityComparer<object>, IQueryable<object>>(Queryable.Union<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000C281 File Offset: 0x0000A481
		public static MethodInfo Where_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Where_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Where_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, IQueryable<object>>(Queryable.Where<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000C2B7 File Offset: 0x0000A4B7
		public static MethodInfo Where_Index_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Where_Index_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Where_Index_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int, bool>>, IQueryable<object>>(Queryable.Where<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000C2ED File Offset: 0x0000A4ED
		public static MethodInfo Zip_TFirst_TSecond_TResult_3(Type TFirst, Type TSecond, Type TResult)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Zip_TFirst_TSecond_TResult_3) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Zip_TFirst_TSecond_TResult_3 = new Func<IQueryable<object>, IEnumerable<object>, Expression<Func<object, object, object>>, IQueryable<object>>(Queryable.Zip<object, object, object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TFirst, TSecond, TResult });
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000C32B File Offset: 0x0000A52B
		public static MethodInfo SkipLast_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_SkipLast_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_SkipLast_TSource_2 = new Func<IQueryable<object>, int, IQueryable<object>>(Queryable.SkipLast<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000C361 File Offset: 0x0000A561
		public static MethodInfo TakeLast_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_TakeLast_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_TakeLast_TSource_2 = new Func<IQueryable<object>, int, IQueryable<object>>(Queryable.TakeLast<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000C397 File Offset: 0x0000A597
		public static MethodInfo Append_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Append_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Append_TSource_2 = new Func<IQueryable<object>, object, IQueryable<object>>(Queryable.Append<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000C3CD File Offset: 0x0000A5CD
		public static MethodInfo Prepend_TSource_2(Type TSource)
		{
			MethodInfo methodInfo;
			if ((methodInfo = CachedReflectionInfo.s_Prepend_TSource_2) == null)
			{
				methodInfo = (CachedReflectionInfo.s_Prepend_TSource_2 = new Func<IQueryable<object>, object, IQueryable<object>>(Queryable.Prepend<object>).GetMethodInfo().GetGenericMethodDefinition());
			}
			return methodInfo.MakeGenericMethod(new Type[] { TSource });
		}

		// Token: 0x04000337 RID: 823
		private static MethodInfo s_Aggregate_TSource_2;

		// Token: 0x04000338 RID: 824
		private static MethodInfo s_Aggregate_TSource_TAccumulate_3;

		// Token: 0x04000339 RID: 825
		private static MethodInfo s_Aggregate_TSource_TAccumulate_TResult_4;

		// Token: 0x0400033A RID: 826
		private static MethodInfo s_All_TSource_2;

		// Token: 0x0400033B RID: 827
		private static MethodInfo s_Any_TSource_1;

		// Token: 0x0400033C RID: 828
		private static MethodInfo s_Any_TSource_2;

		// Token: 0x0400033D RID: 829
		private static MethodInfo s_Average_Int32_1;

		// Token: 0x0400033E RID: 830
		private static MethodInfo s_Average_NullableInt32_1;

		// Token: 0x0400033F RID: 831
		private static MethodInfo s_Average_Int64_1;

		// Token: 0x04000340 RID: 832
		private static MethodInfo s_Average_NullableInt64_1;

		// Token: 0x04000341 RID: 833
		private static MethodInfo s_Average_Single_1;

		// Token: 0x04000342 RID: 834
		private static MethodInfo s_Average_NullableSingle_1;

		// Token: 0x04000343 RID: 835
		private static MethodInfo s_Average_Double_1;

		// Token: 0x04000344 RID: 836
		private static MethodInfo s_Average_NullableDouble_1;

		// Token: 0x04000345 RID: 837
		private static MethodInfo s_Average_Decimal_1;

		// Token: 0x04000346 RID: 838
		private static MethodInfo s_Average_NullableDecimal_1;

		// Token: 0x04000347 RID: 839
		private static MethodInfo s_Average_Int32_TSource_2;

		// Token: 0x04000348 RID: 840
		private static MethodInfo s_Average_NullableInt32_TSource_2;

		// Token: 0x04000349 RID: 841
		private static MethodInfo s_Average_Single_TSource_2;

		// Token: 0x0400034A RID: 842
		private static MethodInfo s_Average_NullableSingle_TSource_2;

		// Token: 0x0400034B RID: 843
		private static MethodInfo s_Average_Int64_TSource_2;

		// Token: 0x0400034C RID: 844
		private static MethodInfo s_Average_NullableInt64_TSource_2;

		// Token: 0x0400034D RID: 845
		private static MethodInfo s_Average_Double_TSource_2;

		// Token: 0x0400034E RID: 846
		private static MethodInfo s_Average_NullableDouble_TSource_2;

		// Token: 0x0400034F RID: 847
		private static MethodInfo s_Average_Decimal_TSource_2;

		// Token: 0x04000350 RID: 848
		private static MethodInfo s_Average_NullableDecimal_TSource_2;

		// Token: 0x04000351 RID: 849
		private static MethodInfo s_Cast_TResult_1;

		// Token: 0x04000352 RID: 850
		private static MethodInfo s_Concat_TSource_2;

		// Token: 0x04000353 RID: 851
		private static MethodInfo s_Contains_TSource_2;

		// Token: 0x04000354 RID: 852
		private static MethodInfo s_Contains_TSource_3;

		// Token: 0x04000355 RID: 853
		private static MethodInfo s_Count_TSource_1;

		// Token: 0x04000356 RID: 854
		private static MethodInfo s_Count_TSource_2;

		// Token: 0x04000357 RID: 855
		private static MethodInfo s_DefaultIfEmpty_TSource_1;

		// Token: 0x04000358 RID: 856
		private static MethodInfo s_DefaultIfEmpty_TSource_2;

		// Token: 0x04000359 RID: 857
		private static MethodInfo s_Distinct_TSource_1;

		// Token: 0x0400035A RID: 858
		private static MethodInfo s_Distinct_TSource_2;

		// Token: 0x0400035B RID: 859
		private static MethodInfo s_ElementAt_TSource_2;

		// Token: 0x0400035C RID: 860
		private static MethodInfo s_ElementAtOrDefault_TSource_2;

		// Token: 0x0400035D RID: 861
		private static MethodInfo s_Except_TSource_2;

		// Token: 0x0400035E RID: 862
		private static MethodInfo s_Except_TSource_3;

		// Token: 0x0400035F RID: 863
		private static MethodInfo s_First_TSource_1;

		// Token: 0x04000360 RID: 864
		private static MethodInfo s_First_TSource_2;

		// Token: 0x04000361 RID: 865
		private static MethodInfo s_FirstOrDefault_TSource_1;

		// Token: 0x04000362 RID: 866
		private static MethodInfo s_FirstOrDefault_TSource_2;

		// Token: 0x04000363 RID: 867
		private static MethodInfo s_GroupBy_TSource_TKey_2;

		// Token: 0x04000364 RID: 868
		private static MethodInfo s_GroupBy_TSource_TKey_3;

		// Token: 0x04000365 RID: 869
		private static MethodInfo s_GroupBy_TSource_TKey_TElement_3;

		// Token: 0x04000366 RID: 870
		private static MethodInfo s_GroupBy_TSource_TKey_TElement_4;

		// Token: 0x04000367 RID: 871
		private static MethodInfo s_GroupBy_TSource_TKey_TResult_3;

		// Token: 0x04000368 RID: 872
		private static MethodInfo s_GroupBy_TSource_TKey_TResult_4;

		// Token: 0x04000369 RID: 873
		private static MethodInfo s_GroupBy_TSource_TKey_TElement_TResult_4;

		// Token: 0x0400036A RID: 874
		private static MethodInfo s_GroupBy_TSource_TKey_TElement_TResult_5;

		// Token: 0x0400036B RID: 875
		private static MethodInfo s_GroupJoin_TOuter_TInner_TKey_TResult_5;

		// Token: 0x0400036C RID: 876
		private static MethodInfo s_GroupJoin_TOuter_TInner_TKey_TResult_6;

		// Token: 0x0400036D RID: 877
		private static MethodInfo s_Intersect_TSource_2;

		// Token: 0x0400036E RID: 878
		private static MethodInfo s_Intersect_TSource_3;

		// Token: 0x0400036F RID: 879
		private static MethodInfo s_Join_TOuter_TInner_TKey_TResult_5;

		// Token: 0x04000370 RID: 880
		private static MethodInfo s_Join_TOuter_TInner_TKey_TResult_6;

		// Token: 0x04000371 RID: 881
		private static MethodInfo s_Last_TSource_1;

		// Token: 0x04000372 RID: 882
		private static MethodInfo s_Last_TSource_2;

		// Token: 0x04000373 RID: 883
		private static MethodInfo s_LastOrDefault_TSource_1;

		// Token: 0x04000374 RID: 884
		private static MethodInfo s_LastOrDefault_TSource_2;

		// Token: 0x04000375 RID: 885
		private static MethodInfo s_LongCount_TSource_1;

		// Token: 0x04000376 RID: 886
		private static MethodInfo s_LongCount_TSource_2;

		// Token: 0x04000377 RID: 887
		private static MethodInfo s_Max_TSource_1;

		// Token: 0x04000378 RID: 888
		private static MethodInfo s_Max_TSource_TResult_2;

		// Token: 0x04000379 RID: 889
		private static MethodInfo s_Min_TSource_1;

		// Token: 0x0400037A RID: 890
		private static MethodInfo s_Min_TSource_TResult_2;

		// Token: 0x0400037B RID: 891
		private static MethodInfo s_OfType_TResult_1;

		// Token: 0x0400037C RID: 892
		private static MethodInfo s_OrderBy_TSource_TKey_2;

		// Token: 0x0400037D RID: 893
		private static MethodInfo s_OrderBy_TSource_TKey_3;

		// Token: 0x0400037E RID: 894
		private static MethodInfo s_OrderByDescending_TSource_TKey_2;

		// Token: 0x0400037F RID: 895
		private static MethodInfo s_OrderByDescending_TSource_TKey_3;

		// Token: 0x04000380 RID: 896
		private static MethodInfo s_Reverse_TSource_1;

		// Token: 0x04000381 RID: 897
		private static MethodInfo s_Select_TSource_TResult_2;

		// Token: 0x04000382 RID: 898
		private static MethodInfo s_Select_Index_TSource_TResult_2;

		// Token: 0x04000383 RID: 899
		private static MethodInfo s_SelectMany_TSource_TResult_2;

		// Token: 0x04000384 RID: 900
		private static MethodInfo s_SelectMany_Index_TSource_TResult_2;

		// Token: 0x04000385 RID: 901
		private static MethodInfo s_SelectMany_Index_TSource_TCollection_TResult_3;

		// Token: 0x04000386 RID: 902
		private static MethodInfo s_SelectMany_TSource_TCollection_TResult_3;

		// Token: 0x04000387 RID: 903
		private static MethodInfo s_SequenceEqual_TSource_2;

		// Token: 0x04000388 RID: 904
		private static MethodInfo s_SequenceEqual_TSource_3;

		// Token: 0x04000389 RID: 905
		private static MethodInfo s_Single_TSource_1;

		// Token: 0x0400038A RID: 906
		private static MethodInfo s_Single_TSource_2;

		// Token: 0x0400038B RID: 907
		private static MethodInfo s_SingleOrDefault_TSource_1;

		// Token: 0x0400038C RID: 908
		private static MethodInfo s_SingleOrDefault_TSource_2;

		// Token: 0x0400038D RID: 909
		private static MethodInfo s_Skip_TSource_2;

		// Token: 0x0400038E RID: 910
		private static MethodInfo s_SkipWhile_TSource_2;

		// Token: 0x0400038F RID: 911
		private static MethodInfo s_SkipWhile_Index_TSource_2;

		// Token: 0x04000390 RID: 912
		private static MethodInfo s_Sum_Int32_1;

		// Token: 0x04000391 RID: 913
		private static MethodInfo s_Sum_NullableInt32_1;

		// Token: 0x04000392 RID: 914
		private static MethodInfo s_Sum_Int64_1;

		// Token: 0x04000393 RID: 915
		private static MethodInfo s_Sum_NullableInt64_1;

		// Token: 0x04000394 RID: 916
		private static MethodInfo s_Sum_Single_1;

		// Token: 0x04000395 RID: 917
		private static MethodInfo s_Sum_NullableSingle_1;

		// Token: 0x04000396 RID: 918
		private static MethodInfo s_Sum_Double_1;

		// Token: 0x04000397 RID: 919
		private static MethodInfo s_Sum_NullableDouble_1;

		// Token: 0x04000398 RID: 920
		private static MethodInfo s_Sum_Decimal_1;

		// Token: 0x04000399 RID: 921
		private static MethodInfo s_Sum_NullableDecimal_1;

		// Token: 0x0400039A RID: 922
		private static MethodInfo s_Sum_NullableDecimal_TSource_2;

		// Token: 0x0400039B RID: 923
		private static MethodInfo s_Sum_Int32_TSource_2;

		// Token: 0x0400039C RID: 924
		private static MethodInfo s_Sum_NullableInt32_TSource_2;

		// Token: 0x0400039D RID: 925
		private static MethodInfo s_Sum_Int64_TSource_2;

		// Token: 0x0400039E RID: 926
		private static MethodInfo s_Sum_NullableInt64_TSource_2;

		// Token: 0x0400039F RID: 927
		private static MethodInfo s_Sum_Single_TSource_2;

		// Token: 0x040003A0 RID: 928
		private static MethodInfo s_Sum_NullableSingle_TSource_2;

		// Token: 0x040003A1 RID: 929
		private static MethodInfo s_Sum_Double_TSource_2;

		// Token: 0x040003A2 RID: 930
		private static MethodInfo s_Sum_NullableDouble_TSource_2;

		// Token: 0x040003A3 RID: 931
		private static MethodInfo s_Sum_Decimal_TSource_2;

		// Token: 0x040003A4 RID: 932
		private static MethodInfo s_Take_TSource_2;

		// Token: 0x040003A5 RID: 933
		private static MethodInfo s_TakeWhile_TSource_2;

		// Token: 0x040003A6 RID: 934
		private static MethodInfo s_TakeWhile_Index_TSource_2;

		// Token: 0x040003A7 RID: 935
		private static MethodInfo s_ThenBy_TSource_TKey_2;

		// Token: 0x040003A8 RID: 936
		private static MethodInfo s_ThenBy_TSource_TKey_3;

		// Token: 0x040003A9 RID: 937
		private static MethodInfo s_ThenByDescending_TSource_TKey_2;

		// Token: 0x040003AA RID: 938
		private static MethodInfo s_ThenByDescending_TSource_TKey_3;

		// Token: 0x040003AB RID: 939
		private static MethodInfo s_Union_TSource_2;

		// Token: 0x040003AC RID: 940
		private static MethodInfo s_Union_TSource_3;

		// Token: 0x040003AD RID: 941
		private static MethodInfo s_Where_TSource_2;

		// Token: 0x040003AE RID: 942
		private static MethodInfo s_Where_Index_TSource_2;

		// Token: 0x040003AF RID: 943
		private static MethodInfo s_Zip_TFirst_TSecond_TResult_3;

		// Token: 0x040003B0 RID: 944
		private static MethodInfo s_SkipLast_TSource_2;

		// Token: 0x040003B1 RID: 945
		private static MethodInfo s_TakeLast_TSource_2;

		// Token: 0x040003B2 RID: 946
		private static MethodInfo s_Append_TSource_2;

		// Token: 0x040003B3 RID: 947
		private static MethodInfo s_Prepend_TSource_2;
	}
}
