using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000715 RID: 1813
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	internal struct CopyPosition
	{
		// Token: 0x0600392B RID: 14635 RVA: 0x000D1080 File Offset: 0x000CF280
		internal CopyPosition(int row, int column)
		{
			this.Row = row;
			this.Column = column;
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x0600392C RID: 14636 RVA: 0x000D1090 File Offset: 0x000CF290
		public static CopyPosition Start
		{
			get
			{
				return default(CopyPosition);
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x0600392D RID: 14637 RVA: 0x000D10A6 File Offset: 0x000CF2A6
		internal int Row { get; }

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x0600392E RID: 14638 RVA: 0x000D10AE File Offset: 0x000CF2AE
		internal int Column { get; }

		// Token: 0x0600392F RID: 14639 RVA: 0x000D10B6 File Offset: 0x000CF2B6
		public CopyPosition Normalize(int endColumn)
		{
			if (this.Column != endColumn)
			{
				return this;
			}
			return new CopyPosition(this.Row + 1, 0);
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06003930 RID: 14640 RVA: 0x000D10D6 File Offset: 0x000CF2D6
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("[{0}, {1}]", this.Row, this.Column);
			}
		}
	}
}
