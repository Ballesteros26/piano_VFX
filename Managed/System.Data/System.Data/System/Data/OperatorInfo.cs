using System;

namespace System.Data
{
	// Token: 0x020000B3 RID: 179
	internal sealed class OperatorInfo
	{
		// Token: 0x06000A8E RID: 2702 RVA: 0x00031EF2 File Offset: 0x000300F2
		internal OperatorInfo(Nodes type, int op, int pri)
		{
			this._type = type;
			this._op = op;
			this._priority = pri;
		}

		// Token: 0x04000733 RID: 1843
		internal Nodes _type;

		// Token: 0x04000734 RID: 1844
		internal int _op;

		// Token: 0x04000735 RID: 1845
		internal int _priority;
	}
}
