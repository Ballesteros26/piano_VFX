using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Xsl
{
	// Token: 0x020004BC RID: 1212
	internal struct IListEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
	{
		// Token: 0x0600313D RID: 12605 RVA: 0x0011C868 File Offset: 0x0011AA68
		public IListEnumerator(IList<T> sequence)
		{
			this.sequence = sequence;
			this.index = 0;
			this.current = default(T);
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x00002F50 File Offset: 0x00001150
		public void Dispose()
		{
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x0600313F RID: 12607 RVA: 0x0011C884 File Offset: 0x0011AA84
		public T Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06003140 RID: 12608 RVA: 0x0011C88C File Offset: 0x0011AA8C
		object IEnumerator.Current
		{
			get
			{
				if (this.index == 0)
				{
					throw new InvalidOperationException(Res.GetString("Enumeration has not started. Call MoveNext.", new object[] { string.Empty }));
				}
				if (this.index > this.sequence.Count)
				{
					throw new InvalidOperationException(Res.GetString("Enumeration has already finished.", new object[] { string.Empty }));
				}
				return this.current;
			}
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x0011C8FC File Offset: 0x0011AAFC
		public bool MoveNext()
		{
			if (this.index < this.sequence.Count)
			{
				this.current = this.sequence[this.index];
				this.index++;
				return true;
			}
			this.current = default(T);
			return false;
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x0011C950 File Offset: 0x0011AB50
		void IEnumerator.Reset()
		{
			this.index = 0;
			this.current = default(T);
		}

		// Token: 0x0400202D RID: 8237
		private IList<T> sequence;

		// Token: 0x0400202E RID: 8238
		private int index;

		// Token: 0x0400202F RID: 8239
		private T current;
	}
}
