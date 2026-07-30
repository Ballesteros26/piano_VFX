using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000046 RID: 70
	public class EnumeratedIterator : IEnumerator
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x0000D909 File Offset: 0x0000BB09
		public virtual bool MoveNext()
		{
			bool flag = this.hasMoreElements();
			if (flag)
			{
				this.tempAuxObj = this.nextElement();
			}
			return flag;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000D920 File Offset: 0x0000BB20
		public virtual void Reset()
		{
			this.tempAuxObj = null;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000D929 File Offset: 0x0000BB29
		public virtual object Current
		{
			get
			{
				return this.tempAuxObj;
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000D931 File Offset: 0x0000BB31
		public EnumeratedIterator(IEnumerator iterator)
		{
			this.i = iterator;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000D940 File Offset: 0x0000BB40
		public bool hasMoreElements()
		{
			return this.i.MoveNext();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000D94D File Offset: 0x0000BB4D
		public object nextElement()
		{
			return this.i.Current;
		}

		// Token: 0x040001AB RID: 427
		private object tempAuxObj;

		// Token: 0x040001AC RID: 428
		private IEnumerator i;
	}
}
