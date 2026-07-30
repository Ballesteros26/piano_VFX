using System;
using System.Diagnostics.Tracing;

namespace System.Buffers
{
	// Token: 0x0200040E RID: 1038
	[EventSource(Name = "System.Buffers.ArrayPoolEventSource")]
	internal sealed class ArrayPoolEventSource : EventSource
	{
		// Token: 0x06001FB5 RID: 8117 RVA: 0x0007B848 File Offset: 0x00079A48
		[Event(1, Level = EventLevel.Verbose)]
		internal unsafe void BufferRented(int bufferId, int bufferSize, int poolId, int bucketId)
		{
			EventSource.EventData* ptr;
			checked
			{
				ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
				ptr->Size = 4;
			}
			ptr->DataPointer = (IntPtr)((void*)(&bufferId));
			ptr[1].Size = 4;
			ptr[1].DataPointer = (IntPtr)((void*)(&bufferSize));
			ptr[2].Size = 4;
			ptr[2].DataPointer = (IntPtr)((void*)(&poolId));
			ptr[3].Size = 4;
			ptr[3].DataPointer = (IntPtr)((void*)(&bucketId));
			base.WriteEventCore(1, 4, ptr);
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x0007B8F4 File Offset: 0x00079AF4
		[Event(2, Level = EventLevel.Informational)]
		internal unsafe void BufferAllocated(int bufferId, int bufferSize, int poolId, int bucketId, ArrayPoolEventSource.BufferAllocatedReason reason)
		{
			EventSource.EventData* ptr;
			checked
			{
				ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)5) * (UIntPtr)sizeof(EventSource.EventData)];
				ptr->Size = 4;
			}
			ptr->DataPointer = (IntPtr)((void*)(&bufferId));
			ptr[1].Size = 4;
			ptr[1].DataPointer = (IntPtr)((void*)(&bufferSize));
			ptr[2].Size = 4;
			ptr[2].DataPointer = (IntPtr)((void*)(&poolId));
			ptr[3].Size = 4;
			ptr[3].DataPointer = (IntPtr)((void*)(&bucketId));
			ptr[4].Size = 4;
			ptr[4].DataPointer = (IntPtr)((void*)(&reason));
			base.WriteEventCore(2, 5, ptr);
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x0007B9C9 File Offset: 0x00079BC9
		[Event(3, Level = EventLevel.Verbose)]
		internal void BufferReturned(int bufferId, int bufferSize, int poolId)
		{
			base.WriteEvent(3, bufferId, bufferSize, poolId);
		}

		// Token: 0x04001B88 RID: 7048
		internal static readonly ArrayPoolEventSource Log = new ArrayPoolEventSource();

		// Token: 0x0200040F RID: 1039
		internal enum BufferAllocatedReason
		{
			// Token: 0x04001B8A RID: 7050
			Pooled,
			// Token: 0x04001B8B RID: 7051
			OverMaximumSize,
			// Token: 0x04001B8C RID: 7052
			PoolExhausted
		}
	}
}
