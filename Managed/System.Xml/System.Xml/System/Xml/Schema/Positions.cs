using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200039A RID: 922
	internal class Positions
	{
		// Token: 0x06002522 RID: 9506 RVA: 0x000E04B0 File Offset: 0x000DE6B0
		public int Add(int symbol, object particle)
		{
			return this.positions.Add(new Position(symbol, particle));
		}

		// Token: 0x1700076C RID: 1900
		public Position this[int pos]
		{
			get
			{
				return (Position)this.positions[pos];
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06002524 RID: 9508 RVA: 0x000E04DC File Offset: 0x000DE6DC
		public int Count
		{
			get
			{
				return this.positions.Count;
			}
		}

		// Token: 0x0400192A RID: 6442
		private ArrayList positions = new ArrayList();
	}
}
