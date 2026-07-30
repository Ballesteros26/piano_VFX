using System;

namespace System.Buffers
{
	// Token: 0x020009A8 RID: 2472
	public abstract class OwnedMemory<T> : IDisposable, IRetainable
	{
		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06005A5A RID: 23130
		public abstract int Length { get; }

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06005A5B RID: 23131
		public abstract Span<T> Span { get; }

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06005A5C RID: 23132 RVA: 0x0012C139 File Offset: 0x0012A339
		public Memory<T> Memory
		{
			get
			{
				if (this.IsDisposed)
				{
					ThrowHelper.ThrowObjectDisposedException_MemoryDisposed("OwnedMemory");
				}
				return new Memory<T>(this, 0, this.Length);
			}
		}

		// Token: 0x06005A5D RID: 23133
		public abstract MemoryHandle Pin();

		// Token: 0x06005A5E RID: 23134
		protected internal abstract bool TryGetArray(out ArraySegment<T> arraySegment);

		// Token: 0x06005A5F RID: 23135 RVA: 0x0012C15A File Offset: 0x0012A35A
		public void Dispose()
		{
			if (this.IsRetained)
			{
				ThrowHelper.ThrowInvalidOperationException_OutstandingReferences();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005A60 RID: 23136
		protected abstract void Dispose(bool disposing);

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06005A61 RID: 23137
		protected abstract bool IsRetained { get; }

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06005A62 RID: 23138
		public abstract bool IsDisposed { get; }

		// Token: 0x06005A63 RID: 23139
		public abstract void Retain();

		// Token: 0x06005A64 RID: 23140
		public abstract bool Release();
	}
}
