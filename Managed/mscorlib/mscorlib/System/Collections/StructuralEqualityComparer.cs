using System;

namespace System.Collections
{
	// Token: 0x020009EC RID: 2540
	[Serializable]
	internal class StructuralEqualityComparer : IEqualityComparer
	{
		// Token: 0x06005DFB RID: 24059 RVA: 0x001363B4 File Offset: 0x001345B4
		public bool Equals(object x, object y)
		{
			if (x == null)
			{
				return y == null;
			}
			IStructuralEquatable structuralEquatable = x as IStructuralEquatable;
			if (structuralEquatable != null)
			{
				return structuralEquatable.Equals(y, this);
			}
			return y != null && x.Equals(y);
		}

		// Token: 0x06005DFC RID: 24060 RVA: 0x001363EC File Offset: 0x001345EC
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			IStructuralEquatable structuralEquatable = obj as IStructuralEquatable;
			if (structuralEquatable != null)
			{
				return structuralEquatable.GetHashCode(this);
			}
			return obj.GetHashCode();
		}
	}
}
