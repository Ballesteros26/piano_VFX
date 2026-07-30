using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a failure occurs in a managed thread after the underlying operating system thread has been started, but before the thread is ready to execute user code.</summary>
	// Token: 0x02000499 RID: 1177
	[Serializable]
	public sealed class ThreadStartException : SystemException
	{
		// Token: 0x0600376D RID: 14189 RVA: 0x000CA333 File Offset: 0x000C8533
		private ThreadStartException()
			: base(Environment.GetResourceString("Thread failed to start."))
		{
			base.SetErrorCode(-2146233051);
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x000CA350 File Offset: 0x000C8550
		private ThreadStartException(Exception reason)
			: base(Environment.GetResourceString("Thread failed to start."), reason)
		{
			base.SetErrorCode(-2146233051);
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x00031FC1 File Offset: 0x000301C1
		internal ThreadStartException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
