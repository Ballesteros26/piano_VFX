using System;
using System.Collections;

namespace UnityEngine
{
	// Token: 0x020001A5 RID: 421
	public abstract class CustomYieldInstruction : IEnumerator
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001345 RID: 4933
		public abstract bool keepWaiting { get; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x0001F8EC File Offset: 0x0001DAEC
		public object Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0001F900 File Offset: 0x0001DB00
		public bool MoveNext()
		{
			return this.keepWaiting;
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00002EC3 File Offset: 0x000010C3
		public virtual void Reset()
		{
		}
	}
}
