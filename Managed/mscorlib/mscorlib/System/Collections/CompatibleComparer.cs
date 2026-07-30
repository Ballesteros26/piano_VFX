using System;

namespace System.Collections
{
	// Token: 0x020009BF RID: 2495
	[Serializable]
	internal class CompatibleComparer : IEqualityComparer
	{
		// Token: 0x06005C3D RID: 23613 RVA: 0x00131228 File Offset: 0x0012F428
		internal CompatibleComparer(IComparer comparer, IHashCodeProvider hashCodeProvider)
		{
			this._comparer = comparer;
			this._hcp = hashCodeProvider;
		}

		// Token: 0x06005C3E RID: 23614 RVA: 0x00131240 File Offset: 0x0012F440
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			if (this._comparer != null)
			{
				return this._comparer.Compare(a, b);
			}
			IComparable comparable = a as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(b);
			}
			throw new ArgumentException(Environment.GetResourceString("At least one object must implement IComparable."));
		}

		// Token: 0x06005C3F RID: 23615 RVA: 0x00131294 File Offset: 0x0012F494
		public bool Equals(object a, object b)
		{
			return this.Compare(a, b) == 0;
		}

		// Token: 0x06005C40 RID: 23616 RVA: 0x001312A1 File Offset: 0x0012F4A1
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._hcp != null)
			{
				return this._hcp.GetHashCode(obj);
			}
			return obj.GetHashCode();
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06005C41 RID: 23617 RVA: 0x001312CC File Offset: 0x0012F4CC
		internal IComparer Comparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06005C42 RID: 23618 RVA: 0x001312D4 File Offset: 0x0012F4D4
		internal IHashCodeProvider HashCodeProvider
		{
			get
			{
				return this._hcp;
			}
		}

		// Token: 0x04002F39 RID: 12089
		private IComparer _comparer;

		// Token: 0x04002F3A RID: 12090
		private IHashCodeProvider _hcp;
	}
}
