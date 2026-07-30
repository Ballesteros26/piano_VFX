using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x0200044A RID: 1098
	internal class XplatUIDriverSupport
	{
		// Token: 0x060048B8 RID: 18616 RVA: 0x00119B64 File Offset: 0x00117D64
		internal static void ExecutionCallback(object state)
		{
			AsyncMethodData asyncMethodData = (AsyncMethodData)state;
			AsyncMethodResult result = asyncMethodData.Result;
			object obj;
			try
			{
				obj = asyncMethodData.Method.DynamicInvoke(asyncMethodData.Args);
			}
			catch (Exception ex)
			{
				if (result != null)
				{
					result.CompleteWithException(ex);
					return;
				}
				throw;
			}
			if (result != null)
			{
				result.Complete(obj);
			}
		}

		// Token: 0x060048B9 RID: 18617 RVA: 0x00119BDC File Offset: 0x00117DDC
		internal static void ExecuteClientMessage(GCHandle gchandle)
		{
			AsyncMethodData asyncMethodData = (AsyncMethodData)gchandle.Target;
			try
			{
				if (asyncMethodData.Context == null)
				{
					XplatUIDriverSupport.ExecutionCallback(asyncMethodData);
				}
				else
				{
					ExecutionContext.Run(asyncMethodData.Context, new ContextCallback(XplatUIDriverSupport.ExecutionCallback), asyncMethodData);
				}
			}
			finally
			{
				gchandle.Free();
			}
		}
	}
}
