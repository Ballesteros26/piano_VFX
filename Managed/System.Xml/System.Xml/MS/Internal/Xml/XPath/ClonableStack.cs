using System;
using System.Collections.Generic;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000013 RID: 19
	internal sealed class ClonableStack<T> : List<T>
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00002EB1 File Offset: 0x000010B1
		public ClonableStack()
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002EB9 File Offset: 0x000010B9
		public ClonableStack(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002EC2 File Offset: 0x000010C2
		private ClonableStack(IEnumerable<T> collection)
			: base(collection)
		{
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002ECB File Offset: 0x000010CB
		public void Push(T value)
		{
			base.Add(value);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002ED4 File Offset: 0x000010D4
		public T Pop()
		{
			int num = base.Count - 1;
			T t = base[num];
			base.RemoveAt(num);
			return t;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002EF8 File Offset: 0x000010F8
		public T Peek()
		{
			return base[base.Count - 1];
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002F08 File Offset: 0x00001108
		public ClonableStack<T> Clone()
		{
			return new ClonableStack<T>(this);
		}
	}
}
