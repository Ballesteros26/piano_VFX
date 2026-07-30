using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003A7 RID: 935
	internal class SmiDefaultFieldsProperty : SmiMetaDataProperty
	{
		// Token: 0x06002BD8 RID: 11224 RVA: 0x000C0979 File Offset: 0x000BEB79
		internal SmiDefaultFieldsProperty(IList<bool> defaultFields)
		{
			this._defaults = new ReadOnlyCollection<bool>(defaultFields);
		}

		// Token: 0x17000747 RID: 1863
		internal bool this[int ordinal]
		{
			get
			{
				return this._defaults.Count > ordinal && this._defaults[ordinal];
			}
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x00005E03 File Offset: 0x00004003
		[Conditional("DEBUG")]
		internal void CheckCount(int countToMatch)
		{
		}

		// Token: 0x04001AB9 RID: 6841
		private IList<bool> _defaults;
	}
}
