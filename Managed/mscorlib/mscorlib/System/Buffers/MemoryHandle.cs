using System;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x020009A7 RID: 2471
	public struct MemoryHandle : IDisposable
	{
		// Token: 0x06005A56 RID: 23126 RVA: 0x0012C0BB File Offset: 0x0012A2BB
		public unsafe MemoryHandle(IRetainable retainable, void* pinnedPointer = null, GCHandle handle = default(GCHandle))
		{
			this._retainable = retainable;
			this._pointer = pinnedPointer;
			this._handle = handle;
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06005A57 RID: 23127 RVA: 0x0012C0D2 File Offset: 0x0012A2D2
		public unsafe void* PinnedPointer
		{
			get
			{
				return this._pointer;
			}
		}

		// Token: 0x06005A58 RID: 23128 RVA: 0x0012C0DA File Offset: 0x0012A2DA
		internal unsafe void AddOffset(int offset)
		{
			if (this._pointer == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.pointer);
				return;
			}
			this._pointer = (void*)((byte*)this._pointer + offset);
		}

		// Token: 0x06005A59 RID: 23129 RVA: 0x0012C0FC File Offset: 0x0012A2FC
		public void Dispose()
		{
			if (this._handle.IsAllocated)
			{
				this._handle.Free();
			}
			if (this._retainable != null)
			{
				this._retainable.Release();
				this._retainable = null;
			}
			this._pointer = null;
		}

		// Token: 0x04002EF9 RID: 12025
		private IRetainable _retainable;

		// Token: 0x04002EFA RID: 12026
		private unsafe void* _pointer;

		// Token: 0x04002EFB RID: 12027
		private GCHandle _handle;
	}
}
