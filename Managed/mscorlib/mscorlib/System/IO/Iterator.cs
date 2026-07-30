using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace System.IO
{
	// Token: 0x02000396 RID: 918
	internal abstract class Iterator<TSource> : IEnumerable<TSource>, IEnumerable, IEnumerator<TSource>, IDisposable, IEnumerator
	{
		// Token: 0x06002AC5 RID: 10949 RVA: 0x00098A86 File Offset: 0x00096C86
		public Iterator()
		{
			this.threadId = Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06002AC6 RID: 10950 RVA: 0x00098A9E File Offset: 0x00096C9E
		public TSource Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x06002AC7 RID: 10951
		protected abstract Iterator<TSource> Clone();

		// Token: 0x06002AC8 RID: 10952 RVA: 0x00098AA6 File Offset: 0x00096CA6
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x00098AB5 File Offset: 0x00096CB5
		protected virtual void Dispose(bool disposing)
		{
			this.current = default(TSource);
			this.state = -1;
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x00098ACA File Offset: 0x00096CCA
		public IEnumerator<TSource> GetEnumerator()
		{
			if (this.threadId == Thread.CurrentThread.ManagedThreadId && this.state == 0)
			{
				this.state = 1;
				return this;
			}
			Iterator<TSource> iterator = this.Clone();
			iterator.state = 1;
			return iterator;
		}

		// Token: 0x06002ACB RID: 10955
		public abstract bool MoveNext();

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06002ACC RID: 10956 RVA: 0x00098AFC File Offset: 0x00096CFC
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x00098B09 File Offset: 0x00096D09
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x00014B5A File Offset: 0x00012D5A
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x04001686 RID: 5766
		private int threadId;

		// Token: 0x04001687 RID: 5767
		internal int state;

		// Token: 0x04001688 RID: 5768
		internal TSource current;
	}
}
