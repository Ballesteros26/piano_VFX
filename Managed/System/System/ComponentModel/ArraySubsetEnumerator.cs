using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000229 RID: 553
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal class ArraySubsetEnumerator : IEnumerator
	{
		// Token: 0x060011E0 RID: 4576 RVA: 0x0004CB0C File Offset: 0x0004AD0C
		public ArraySubsetEnumerator(Array array, int count)
		{
			this.array = array;
			this.total = count;
			this.current = -1;
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0004CB29 File Offset: 0x0004AD29
		public bool MoveNext()
		{
			if (this.current < this.total - 1)
			{
				this.current++;
				return true;
			}
			return false;
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x0004CB4C File Offset: 0x0004AD4C
		public void Reset()
		{
			this.current = -1;
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x0004CB55 File Offset: 0x0004AD55
		public object Current
		{
			get
			{
				if (this.current == -1)
				{
					throw new InvalidOperationException();
				}
				return this.array.GetValue(this.current);
			}
		}

		// Token: 0x0400122A RID: 4650
		private Array array;

		// Token: 0x0400122B RID: 4651
		private int total;

		// Token: 0x0400122C RID: 4652
		private int current;
	}
}
