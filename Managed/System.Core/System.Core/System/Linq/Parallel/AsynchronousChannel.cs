using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000106 RID: 262
	internal sealed class AsynchronousChannel<T> : IDisposable
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x0001D139 File Offset: 0x0001B339
		internal AsynchronousChannel(int index, int chunkSize, CancellationToken cancellationToken, IntValueEvent consumerEvent)
			: this(index, 512, chunkSize, cancellationToken, consumerEvent)
		{
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0001D14C File Offset: 0x0001B34C
		internal AsynchronousChannel(int index, int capacity, int chunkSize, CancellationToken cancellationToken, IntValueEvent consumerEvent)
		{
			if (chunkSize == 0)
			{
				chunkSize = Scheduling.GetDefaultChunkSize<T>();
			}
			this._index = index;
			this._buffer = new T[capacity + 1][];
			this._producerBufferIndex = 0;
			this._consumerBufferIndex = 0;
			this._producerEvent = new ManualResetEventSlim();
			this._consumerEvent = consumerEvent;
			this._chunkSize = chunkSize;
			this._producerChunk = new T[chunkSize];
			this._producerChunkIndex = 0;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x0001D1C8 File Offset: 0x0001B3C8
		internal bool IsFull
		{
			get
			{
				int producerBufferIndex = this._producerBufferIndex;
				int consumerBufferIndex = this._consumerBufferIndex;
				return producerBufferIndex == consumerBufferIndex - 1 || (consumerBufferIndex == 0 && producerBufferIndex == this._buffer.Length - 1);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0001D201 File Offset: 0x0001B401
		internal bool IsChunkBufferEmpty
		{
			get
			{
				return this._producerBufferIndex == this._consumerBufferIndex;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0001D215 File Offset: 0x0001B415
		internal bool IsDone
		{
			get
			{
				return this._done;
			}
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0001D21F File Offset: 0x0001B41F
		internal void FlushBuffers()
		{
			this.FlushCachedChunk();
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0001D228 File Offset: 0x0001B428
		internal void SetDone()
		{
			this._done = true;
			lock (this)
			{
				if (this._consumerEvent != null)
				{
					this._consumerEvent.Set(this._index);
				}
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0001D280 File Offset: 0x0001B480
		internal void Enqueue(T item)
		{
			int producerChunkIndex = this._producerChunkIndex;
			this._producerChunk[producerChunkIndex] = item;
			if (producerChunkIndex == this._chunkSize - 1)
			{
				this.EnqueueChunk(this._producerChunk);
				this._producerChunk = new T[this._chunkSize];
			}
			this._producerChunkIndex = (producerChunkIndex + 1) % this._chunkSize;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0001D2DC File Offset: 0x0001B4DC
		private void EnqueueChunk(T[] chunk)
		{
			if (this.IsFull)
			{
				this.WaitUntilNonFull();
			}
			int producerBufferIndex = this._producerBufferIndex;
			this._buffer[producerBufferIndex] = chunk;
			Interlocked.Exchange(ref this._producerBufferIndex, (producerBufferIndex + 1) % this._buffer.Length);
			if (this._consumerIsWaiting == 1 && !this.IsChunkBufferEmpty)
			{
				this._consumerIsWaiting = 0;
				this._consumerEvent.Set(this._index);
			}
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0001D350 File Offset: 0x0001B550
		private void WaitUntilNonFull()
		{
			do
			{
				this._producerEvent.Reset();
				Interlocked.Exchange(ref this._producerIsWaiting, 1);
				if (this.IsFull)
				{
					this._producerEvent.Wait(this._cancellationToken);
				}
				else
				{
					this._producerIsWaiting = 0;
				}
			}
			while (this.IsFull);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0001D3A4 File Offset: 0x0001B5A4
		private void FlushCachedChunk()
		{
			if (this._producerChunk != null && this._producerChunkIndex != 0)
			{
				T[] array = new T[this._producerChunkIndex];
				Array.Copy(this._producerChunk, 0, array, 0, this._producerChunkIndex);
				this.EnqueueChunk(array);
				this._producerChunk = null;
			}
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0001D3F0 File Offset: 0x0001B5F0
		internal bool TryDequeue(ref T item)
		{
			if (this._consumerChunk == null)
			{
				if (!this.TryDequeueChunk(ref this._consumerChunk))
				{
					return false;
				}
				this._consumerChunkIndex = 0;
			}
			item = this._consumerChunk[this._consumerChunkIndex];
			this._consumerChunkIndex++;
			if (this._consumerChunkIndex == this._consumerChunk.Length)
			{
				this._consumerChunk = null;
			}
			return true;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0001D459 File Offset: 0x0001B659
		private bool TryDequeueChunk(ref T[] chunk)
		{
			if (this.IsChunkBufferEmpty)
			{
				return false;
			}
			chunk = this.InternalDequeueChunk();
			return true;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0001D470 File Offset: 0x0001B670
		internal bool TryDequeue(ref T item, ref bool isDone)
		{
			isDone = false;
			if (this._consumerChunk == null)
			{
				if (!this.TryDequeueChunk(ref this._consumerChunk, ref isDone))
				{
					return false;
				}
				this._consumerChunkIndex = 0;
			}
			item = this._consumerChunk[this._consumerChunkIndex];
			this._consumerChunkIndex++;
			if (this._consumerChunkIndex == this._consumerChunk.Length)
			{
				this._consumerChunk = null;
			}
			return true;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0001D4E0 File Offset: 0x0001B6E0
		private bool TryDequeueChunk(ref T[] chunk, ref bool isDone)
		{
			isDone = false;
			while (this.IsChunkBufferEmpty)
			{
				if (this.IsDone && this.IsChunkBufferEmpty)
				{
					isDone = true;
					return false;
				}
				Interlocked.Exchange(ref this._consumerIsWaiting, 1);
				if (this.IsChunkBufferEmpty && !this.IsDone)
				{
					return false;
				}
				this._consumerIsWaiting = 0;
			}
			chunk = this.InternalDequeueChunk();
			return true;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0001D540 File Offset: 0x0001B740
		private T[] InternalDequeueChunk()
		{
			int consumerBufferIndex = this._consumerBufferIndex;
			T[] array = this._buffer[consumerBufferIndex];
			this._buffer[consumerBufferIndex] = null;
			Interlocked.Exchange(ref this._consumerBufferIndex, (consumerBufferIndex + 1) % this._buffer.Length);
			if (this._producerIsWaiting == 1 && !this.IsFull)
			{
				this._producerIsWaiting = 0;
				this._producerEvent.Set();
			}
			return array;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0001D5A6 File Offset: 0x0001B7A6
		internal void DoneWithDequeueWait()
		{
			this._consumerIsWaiting = 0;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0001D5B4 File Offset: 0x0001B7B4
		public void Dispose()
		{
			lock (this)
			{
				this._producerEvent.Dispose();
				this._producerEvent = null;
				this._consumerEvent = null;
			}
		}

		// Token: 0x04000536 RID: 1334
		private T[][] _buffer;

		// Token: 0x04000537 RID: 1335
		private readonly int _index;

		// Token: 0x04000538 RID: 1336
		private volatile int _producerBufferIndex;

		// Token: 0x04000539 RID: 1337
		private volatile int _consumerBufferIndex;

		// Token: 0x0400053A RID: 1338
		private volatile bool _done;

		// Token: 0x0400053B RID: 1339
		private T[] _producerChunk;

		// Token: 0x0400053C RID: 1340
		private int _producerChunkIndex;

		// Token: 0x0400053D RID: 1341
		private T[] _consumerChunk;

		// Token: 0x0400053E RID: 1342
		private int _consumerChunkIndex;

		// Token: 0x0400053F RID: 1343
		private int _chunkSize;

		// Token: 0x04000540 RID: 1344
		private ManualResetEventSlim _producerEvent;

		// Token: 0x04000541 RID: 1345
		private IntValueEvent _consumerEvent;

		// Token: 0x04000542 RID: 1346
		private volatile int _producerIsWaiting;

		// Token: 0x04000543 RID: 1347
		private volatile int _consumerIsWaiting;

		// Token: 0x04000544 RID: 1348
		private CancellationToken _cancellationToken;
	}
}
