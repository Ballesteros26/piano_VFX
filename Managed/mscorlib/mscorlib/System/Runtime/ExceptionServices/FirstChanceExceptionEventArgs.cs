using System;
using System.Runtime.ConstrainedExecution;

namespace System.Runtime.ExceptionServices
{
	/// <summary>Provides data for the notification event that is raised when a managed exception first occurs, before the common language runtime begins searching for event handlers.</summary>
	// Token: 0x0200082A RID: 2090
	public class FirstChanceExceptionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs" /> class with a specified exception.</summary>
		/// <param name="exception">The exception that was just thrown by managed code, and that will be examined by the <see cref="E:System.AppDomain.UnhandledException" /> event. </param>
		// Token: 0x0600537E RID: 21374 RVA: 0x001258BC File Offset: 0x00123ABC
		public FirstChanceExceptionEventArgs(Exception exception)
		{
			this.m_Exception = exception;
		}

		/// <summary>The managed exception object that corresponds to the exception thrown in managed code.</summary>
		/// <returns>The newly thrown exception.</returns>
		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x0600537F RID: 21375 RVA: 0x001258CB File Offset: 0x00123ACB
		public Exception Exception
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.m_Exception;
			}
		}

		// Token: 0x04002B67 RID: 11111
		private Exception m_Exception;
	}
}
