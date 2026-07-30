using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x0200023F RID: 575
	internal sealed class EmptyEnumerator : IEnumerator
	{
		// Token: 0x0600166E RID: 5742 RVA: 0x0000226C File Offset: 0x0000046C
		bool IEnumerator.MoveNext()
		{
			return false;
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00002F50 File Offset: 0x00001150
		void IEnumerator.Reset()
		{
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x00016C08 File Offset: 0x00014E08
		object IEnumerator.Current
		{
			get
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
		}
	}
}
