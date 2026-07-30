using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace System.Dynamic.Utils
{
	// Token: 0x0200033C RID: 828
	internal static class ContractUtils
	{
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x00052830 File Offset: 0x00050A30
		[ExcludeFromCodeCoverage]
		public static Exception Unreachable
		{
			get
			{
				return new InvalidOperationException("Code supposed to be unreachable");
			}
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x0005283C File Offset: 0x00050A3C
		public static void Requires(bool precondition, string paramName)
		{
			if (!precondition)
			{
				throw Error.InvalidArgumentValue(paramName);
			}
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00052848 File Offset: 0x00050A48
		public static void RequiresNotNull(object value, string paramName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(paramName);
			}
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00052854 File Offset: 0x00050A54
		public static void RequiresNotNull(object value, string paramName, int index)
		{
			if (value == null)
			{
				throw new ArgumentNullException(ContractUtils.GetParamName(paramName, index));
			}
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00052866 File Offset: 0x00050A66
		public static void RequiresNotEmpty<T>(ICollection<T> collection, string paramName)
		{
			ContractUtils.RequiresNotNull(collection, paramName);
			if (collection.Count == 0)
			{
				throw Error.NonEmptyCollectionRequired(paramName);
			}
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00052880 File Offset: 0x00050A80
		public static void RequiresNotNullItems<T>(IList<T> array, string arrayName)
		{
			ContractUtils.RequiresNotNull(array, arrayName);
			int i = 0;
			int count = array.Count;
			while (i < count)
			{
				if (array[i] == null)
				{
					throw new ArgumentNullException(ContractUtils.GetParamName(arrayName, i));
				}
				i++;
			}
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x00003C4C File Offset: 0x00001E4C
		[Conditional("DEBUG")]
		public static void AssertLockHeld(object lockObject)
		{
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x000528C2 File Offset: 0x00050AC2
		private static string GetParamName(string paramName, int index)
		{
			if (index < 0)
			{
				return paramName;
			}
			return string.Format("{0}[{1}]", paramName, index);
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x000528DB File Offset: 0x00050ADB
		public static void RequiresArrayRange<T>(IList<T> array, int offset, int count, string offsetName, string countName)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(countName);
			}
			if (offset < 0 || array.Count - offset < count)
			{
				throw new ArgumentOutOfRangeException(offsetName);
			}
		}
	}
}
