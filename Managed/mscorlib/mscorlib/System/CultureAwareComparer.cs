using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020001C5 RID: 453
	[Serializable]
	internal sealed class CultureAwareComparer : StringComparer
	{
		// Token: 0x0600141D RID: 5149 RVA: 0x00051A63 File Offset: 0x0004FC63
		internal CultureAwareComparer(CultureInfo culture, bool ignoreCase)
		{
			this._compareInfo = culture.CompareInfo;
			this._ignoreCase = ignoreCase;
			this._options = (ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00051A8B File Offset: 0x0004FC8B
		internal CultureAwareComparer(CompareInfo compareInfo, bool ignoreCase)
		{
			this._compareInfo = compareInfo;
			this._ignoreCase = ignoreCase;
			this._options = (ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00051AAE File Offset: 0x0004FCAE
		internal CultureAwareComparer(CompareInfo compareInfo, CompareOptions options)
		{
			this._compareInfo = compareInfo;
			this._options = options;
			this._ignoreCase = (options & CompareOptions.IgnoreCase) == CompareOptions.IgnoreCase || (options & CompareOptions.OrdinalIgnoreCase) == CompareOptions.OrdinalIgnoreCase;
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x00051AE1 File Offset: 0x0004FCE1
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
			return this._compareInfo.Compare(x, y, this._options);
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x00051B06 File Offset: 0x0004FD06
		public override bool Equals(string x, string y)
		{
			return x == y || (x != null && y != null && this._compareInfo.Compare(x, y, this._options) == 0);
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x00051B2C File Offset: 0x0004FD2C
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return this._compareInfo.GetHashCodeOfString(obj, this._options);
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x00051B50 File Offset: 0x0004FD50
		public override bool Equals(object obj)
		{
			CultureAwareComparer cultureAwareComparer = obj as CultureAwareComparer;
			return cultureAwareComparer != null && this._ignoreCase == cultureAwareComparer._ignoreCase && this._compareInfo.Equals(cultureAwareComparer._compareInfo) && this._options == cultureAwareComparer._options;
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x00051B9C File Offset: 0x0004FD9C
		public override int GetHashCode()
		{
			int hashCode = this._compareInfo.GetHashCode();
			if (!this._ignoreCase)
			{
				return hashCode;
			}
			return ~hashCode;
		}

		// Token: 0x04000AEA RID: 2794
		private CompareInfo _compareInfo;

		// Token: 0x04000AEB RID: 2795
		private bool _ignoreCase;

		// Token: 0x04000AEC RID: 2796
		[OptionalField]
		private CompareOptions _options;
	}
}
