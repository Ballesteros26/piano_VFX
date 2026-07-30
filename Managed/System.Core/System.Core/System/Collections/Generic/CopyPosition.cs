using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000347 RID: 839
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	internal struct CopyPosition
	{
		// Token: 0x06001986 RID: 6534 RVA: 0x00053D51 File Offset: 0x00051F51
		internal CopyPosition(int row, int column)
		{
			this.Row = row;
			this.Column = column;
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001987 RID: 6535 RVA: 0x00053D64 File Offset: 0x00051F64
		public static CopyPosition Start
		{
			get
			{
				return default(CopyPosition);
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x00053D7A File Offset: 0x00051F7A
		internal int Row { get; }

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001989 RID: 6537 RVA: 0x00053D82 File Offset: 0x00051F82
		internal int Column { get; }

		// Token: 0x0600198A RID: 6538 RVA: 0x00053D8A File Offset: 0x00051F8A
		public CopyPosition Normalize(int endColumn)
		{
			if (this.Column != endColumn)
			{
				return this;
			}
			return new CopyPosition(this.Row + 1, 0);
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600198B RID: 6539 RVA: 0x00053DAA File Offset: 0x00051FAA
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("[{0}, {1}]", this.Row, this.Column);
			}
		}
	}
}
