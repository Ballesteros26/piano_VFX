using System;

namespace Mono.Net.Security
{
	// Token: 0x0200006D RID: 109
	internal class AsyncWriteRequest : AsyncReadOrWriteRequest
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x000060E9 File Offset: 0x000042E9
		public AsyncWriteRequest(MobileAuthenticatedStream parent, bool sync, byte[] buffer, int offset, int size)
			: base(parent, sync, buffer, offset, size)
		{
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006180 File Offset: 0x00004380
		protected override AsyncOperationStatus Run(AsyncOperationStatus status)
		{
			if (base.UserBuffer.Size == 0)
			{
				base.UserResult = base.CurrentSize;
				return AsyncOperationStatus.Complete;
			}
			ValueTuple<int, bool> valueTuple = base.Parent.ProcessWrite(base.UserBuffer);
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
			if (item2)
			{
				return AsyncOperationStatus.Continue;
			}
			base.UserResult = base.CurrentSize;
			return AsyncOperationStatus.Complete;
		}
	}
}
