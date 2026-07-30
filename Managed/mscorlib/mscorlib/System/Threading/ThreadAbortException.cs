using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a call is made to the <see cref="M:System.Threading.Thread.Abort(System.Object)" /> method. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000486 RID: 1158
	[ComVisible(true)]
	[Serializable]
	public sealed class ThreadAbortException : SystemException
	{
		// Token: 0x060036F4 RID: 14068 RVA: 0x000C8D76 File Offset: 0x000C6F76
		private ThreadAbortException()
			: base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.ThreadAbort))
		{
			base.SetErrorCode(-2146233040);
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x00031FC1 File Offset: 0x000301C1
		internal ThreadAbortException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets an object that contains application-specific information related to the thread abort.</summary>
		/// <returns>An object containing application-specific information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x060036F6 RID: 14070 RVA: 0x000C8D8F File Offset: 0x000C6F8F
		public object ExceptionState
		{
			[SecuritySafeCritical]
			get
			{
				return Thread.CurrentThread.AbortReason;
			}
		}
	}
}
