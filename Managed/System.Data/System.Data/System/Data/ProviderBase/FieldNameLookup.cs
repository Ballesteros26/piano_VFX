using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace System.Data.ProviderBase
{
	// Token: 0x020002FA RID: 762
	internal sealed class FieldNameLookup : BasicFieldNameLookup
	{
		// Token: 0x060021D0 RID: 8656 RVA: 0x0009DCC1 File Offset: 0x0009BEC1
		public FieldNameLookup(string[] fieldNames, int defaultLocaleID)
			: base(fieldNames)
		{
			this._defaultLocaleID = defaultLocaleID;
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x0009DCD1 File Offset: 0x0009BED1
		public FieldNameLookup(ReadOnlyCollection<string> columnNames, int defaultLocaleID)
			: base(columnNames)
		{
			this._defaultLocaleID = defaultLocaleID;
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x0009DCE1 File Offset: 0x0009BEE1
		public FieldNameLookup(IDataReader reader, int defaultLocaleID)
			: base(reader)
		{
			this._defaultLocaleID = defaultLocaleID;
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x0009DCF4 File Offset: 0x0009BEF4
		protected override CompareInfo GetCompareInfo()
		{
			CompareInfo compareInfo = null;
			if (-1 != this._defaultLocaleID)
			{
				compareInfo = CompareInfo.GetCompareInfo(this._defaultLocaleID);
			}
			if (compareInfo == null)
			{
				compareInfo = base.GetCompareInfo();
			}
			return compareInfo;
		}

		// Token: 0x040016B2 RID: 5810
		private readonly int _defaultLocaleID;
	}
}
