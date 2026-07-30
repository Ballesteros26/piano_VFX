using System;

namespace System.Collections
{
	/// <summary>Provides objects for performing a structural comparison of two collection objects.</summary>
	// Token: 0x020009EB RID: 2539
	public static class StructuralComparisons
	{
		/// <summary>Gets a predefined object that performs a structural comparison of two objects.</summary>
		/// <returns>A predefined object that is used to perform a structural comparison of two collection objects.</returns>
		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x06005DF9 RID: 24057 RVA: 0x00136364 File Offset: 0x00134564
		public static IComparer StructuralComparer
		{
			get
			{
				IComparer comparer = StructuralComparisons.s_StructuralComparer;
				if (comparer == null)
				{
					comparer = new StructuralComparer();
					StructuralComparisons.s_StructuralComparer = comparer;
				}
				return comparer;
			}
		}

		/// <summary>Gets a predefined object that compares two objects for structural equality.</summary>
		/// <returns>A predefined object that is used to compare two collection objects for structural equality.</returns>
		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x06005DFA RID: 24058 RVA: 0x0013638C File Offset: 0x0013458C
		public static IEqualityComparer StructuralEqualityComparer
		{
			get
			{
				IEqualityComparer equalityComparer = StructuralComparisons.s_StructuralEqualityComparer;
				if (equalityComparer == null)
				{
					equalityComparer = new StructuralEqualityComparer();
					StructuralComparisons.s_StructuralEqualityComparer = equalityComparer;
				}
				return equalityComparer;
			}
		}

		// Token: 0x04002FB2 RID: 12210
		private static volatile IComparer s_StructuralComparer;

		// Token: 0x04002FB3 RID: 12211
		private static volatile IEqualityComparer s_StructuralEqualityComparer;
	}
}
