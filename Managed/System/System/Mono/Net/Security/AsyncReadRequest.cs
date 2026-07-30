using System;

namespace Mono.Net.Security
{
	// Token: 0x0200006C RID: 108
	internal class AsyncReadRequest : AsyncReadOrWriteRequest
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x000060E9 File Offset: 0x000042E9
		public AsyncReadRequest(MobileAuthenticatedStream parent, bool sync, byte[] buffer, int offset, int size)
			: base(parent, sync, buffer, offset, size)
		{
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000060F8 File Offset: 0x000042F8
		protected override AsyncOperationStatus Run(AsyncOperationStatus status)
		{
			ValueTuple<int, bool> valueTuple = base.Parent.ProcessRead(base.UserBuffer);
			int item = valueTuple.Item1;
			bool item2 = valueTuple.Item2;
			if (item < 0)
			{
				base.UserResult = -1;
				return AsyncOperationStatus.Complete;
			}
			base.CurrentSize += item;
			base.UserBuffer.Offset += item;
			base.UserBuffer.Size -= item;
			if (item2 && base.CurrentSize == 0)
			{
				return AsyncOperationStatus.Continue;
			}
			base.UserResult = base.CurrentSize;
			return AsyncOperationStatus.Complete;
		}
	}
}
