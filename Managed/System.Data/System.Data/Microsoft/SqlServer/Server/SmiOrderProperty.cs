using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003A5 RID: 933
	internal class SmiOrderProperty : SmiMetaDataProperty
	{
		// Token: 0x06002BD5 RID: 11221 RVA: 0x000C0921 File Offset: 0x000BEB21
		internal SmiOrderProperty(IList<SmiOrderProperty.SmiColumnOrder> columnOrders)
		{
			this._columns = new ReadOnlyCollection<SmiOrderProperty.SmiColumnOrder>(columnOrders);
		}

		// Token: 0x17000746 RID: 1862
		internal SmiOrderProperty.SmiColumnOrder this[int ordinal]
		{
			get
			{
				if (this._columns.Count <= ordinal)
				{
					return new SmiOrderProperty.SmiColumnOrder
					{
						Order = SortOrder.Unspecified,
						SortOrdinal = -1
					};
				}
				return this._columns[ordinal];
			}
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x00005E03 File Offset: 0x00004003
		[Conditional("DEBUG")]
		internal void CheckCount(int countToMatch)
		{
		}

		// Token: 0x04001AB6 RID: 6838
		private IList<SmiOrderProperty.SmiColumnOrder> _columns;

		// Token: 0x020003A6 RID: 934
		internal struct SmiColumnOrder
		{
			// Token: 0x04001AB7 RID: 6839
			internal int SortOrdinal;

			// Token: 0x04001AB8 RID: 6840
			internal SortOrder Order;
		}
	}
}
