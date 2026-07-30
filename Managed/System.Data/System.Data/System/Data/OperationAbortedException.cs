using System;
using Unity;

namespace System.Data
{
	/// <summary>This exception is thrown when an ongoing operation is aborted by the user. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000118 RID: 280
	[Serializable]
	public sealed class OperationAbortedException : SystemException
	{
		// Token: 0x06000E52 RID: 3666 RVA: 0x0004B92D File Offset: 0x00049B2D
		private OperationAbortedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232010;
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0004B944 File Offset: 0x00049B44
		internal static OperationAbortedException Aborted(Exception inner)
		{
			OperationAbortedException ex;
			if (inner == null)
			{
				ex = new OperationAbortedException(SR.GetString("Operation aborted."), null);
			}
			else
			{
				ex = new OperationAbortedException(SR.GetString("Operation aborted due to an exception (see InnerException for details)."), inner);
			}
			return ex;
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00010468 File Offset: 0x0000E668
		internal OperationAbortedException()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
