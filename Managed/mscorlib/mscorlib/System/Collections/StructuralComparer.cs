using System;

namespace System.Collections
{
	// Token: 0x020009ED RID: 2541
	[Serializable]
	internal class StructuralComparer : IComparer
	{
		// Token: 0x06005DFE RID: 24062 RVA: 0x00136418 File Offset: 0x00134618
		public int Compare(object x, object y)
		{
			if (x == null)
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (y == null)
				{
					return 1;
				}
				IStructuralComparable structuralComparable = x as IStructuralComparable;
				if (structuralComparable != null)
				{
					return structuralComparable.CompareTo(y, this);
				}
				return Comparer.Default.Compare(x, y);
			}
		}
	}
}
