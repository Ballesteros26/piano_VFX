using System;
using System.Globalization;

namespace System
{
	// Token: 0x020001C6 RID: 454
	[Serializable]
	internal sealed class OrdinalComparer : StringComparer
	{
		// Token: 0x06001425 RID: 5157 RVA: 0x00051BC1 File Offset: 0x0004FDC1
		internal OrdinalComparer(bool ignoreCase)
		{
			this._ignoreCase = ignoreCase;
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x00051BD0 File Offset: 0x0004FDD0
		public override int Compare(string x, string y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			if (this._ignoreCase)
			{
				return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
			}
			return string.CompareOrdinal(x, y);
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00051BFA File Offset: 0x0004FDFA
		public override bool Equals(string x, string y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (this._ignoreCase)
			{
				return x.Length == y.Length && string.Compare(x, y, StringComparison.OrdinalIgnoreCase) == 0;
			}
			return x.Equals(y);
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x00051C35 File Offset: 0x0004FE35
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._ignoreCase)
			{
				return TextInfo.GetHashCodeOrdinalIgnoreCase(obj);
			}
			return obj.GetHashCode();
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x00051C5C File Offset: 0x0004FE5C
		public override bool Equals(object obj)
		{
			OrdinalComparer ordinalComparer = obj as OrdinalComparer;
			return ordinalComparer != null && this._ignoreCase == ordinalComparer._ignoreCase;
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00051C84 File Offset: 0x0004FE84
		public override int GetHashCode()
		{
			int hashCode = "OrdinalComparer".GetHashCode();
			if (!this._ignoreCase)
			{
				return hashCode;
			}
			return ~hashCode;
		}

		// Token: 0x04000AED RID: 2797
		private bool _ignoreCase;
	}
}
