using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003A4 RID: 932
	internal class SmiUniqueKeyProperty : SmiMetaDataProperty
	{
		// Token: 0x06002BD2 RID: 11218 RVA: 0x000C08EF File Offset: 0x000BEAEF
		internal SmiUniqueKeyProperty(IList<bool> columnIsKey)
		{
			this._columns = new ReadOnlyCollection<bool>(columnIsKey);
		}

		// Token: 0x17000745 RID: 1861
		internal bool this[int ordinal]
		{
			get
			{
				return this._columns.Count > ordinal && this._columns[ordinal];
			}
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x00005E03 File Offset: 0x00004003
		[Conditional("DEBUG")]
		internal void CheckCount(int countToMatch)
		{
		}

		// Token: 0x04001AB5 RID: 6837
		private IList<bool> _columns;
	}
}
