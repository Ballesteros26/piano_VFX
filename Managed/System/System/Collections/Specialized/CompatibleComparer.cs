using System;
using System.Globalization;

namespace System.Collections.Specialized
{
	// Token: 0x02000702 RID: 1794
	[Serializable]
	internal class CompatibleComparer : IEqualityComparer
	{
		// Token: 0x0600384B RID: 14411 RVA: 0x000CF107 File Offset: 0x000CD307
		internal CompatibleComparer(IComparer comparer, IHashCodeProvider hashCodeProvider)
		{
			this._comparer = comparer;
			this._hcp = hashCodeProvider;
		}

		// Token: 0x0600384C RID: 14412 RVA: 0x000CF120 File Offset: 0x000CD320
		public bool Equals(object a, object b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			try
			{
				if (this._comparer != null)
				{
					return this._comparer.Compare(a, b) == 0;
				}
				IComparable comparable = a as IComparable;
				if (comparable != null)
				{
					return comparable.CompareTo(b) == 0;
				}
			}
			catch (ArgumentException)
			{
				return false;
			}
			return a.Equals(b);
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x000CF190 File Offset: 0x000CD390
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

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x0600384E RID: 14414 RVA: 0x000CF1BB File Offset: 0x000CD3BB
		public IComparer Comparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x0600384F RID: 14415 RVA: 0x000CF1C3 File Offset: 0x000CD3C3
		public IHashCodeProvider HashCodeProvider
		{
			get
			{
				return this._hcp;
			}
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06003850 RID: 14416 RVA: 0x000CF1CB File Offset: 0x000CD3CB
		public static IComparer DefaultComparer
		{
			get
			{
				if (CompatibleComparer.defaultComparer == null)
				{
					CompatibleComparer.defaultComparer = new CaseInsensitiveComparer(CultureInfo.InvariantCulture);
				}
				return CompatibleComparer.defaultComparer;
			}
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06003851 RID: 14417 RVA: 0x000CF1EE File Offset: 0x000CD3EE
		public static IHashCodeProvider DefaultHashCodeProvider
		{
			get
			{
				if (CompatibleComparer.defaultHashProvider == null)
				{
					CompatibleComparer.defaultHashProvider = new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture);
				}
				return CompatibleComparer.defaultHashProvider;
			}
		}

		// Token: 0x04002C53 RID: 11347
		private IComparer _comparer;

		// Token: 0x04002C54 RID: 11348
		private static volatile IComparer defaultComparer;

		// Token: 0x04002C55 RID: 11349
		private IHashCodeProvider _hcp;

		// Token: 0x04002C56 RID: 11350
		private static volatile IHashCodeProvider defaultHashProvider;
	}
}
