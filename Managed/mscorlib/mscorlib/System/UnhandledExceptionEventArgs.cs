using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Provides data for the event that is raised when there is an exception that is not handled in any application domain.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E7 RID: 487
	[ComVisible(true)]
	[Serializable]
	public class UnhandledExceptionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.UnhandledExceptionEventArgs" /> class with the exception object and a common language runtime termination flag.</summary>
		/// <param name="exception">The exception that is not handled. </param>
		/// <param name="isTerminating">true if the runtime is terminating; otherwise, false. </param>
		// Token: 0x06001670 RID: 5744 RVA: 0x0005910D File Offset: 0x0005730D
		public UnhandledExceptionEventArgs(object exception, bool isTerminating)
		{
			this._Exception = exception;
			this._IsTerminating = isTerminating;
		}

		/// <summary>Gets the unhandled exception object.</summary>
		/// <returns>The unhandled exception object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x00059123 File Offset: 0x00057323
		public object ExceptionObject
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._Exception;
			}
		}

		/// <summary>Indicates whether the common language runtime is terminating.</summary>
		/// <returns>true if the runtime is terminating; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x0005912B File Offset: 0x0005732B
		public bool IsTerminating
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._IsTerminating;
			}
		}

		// Token: 0x04000BC2 RID: 3010
		private object _Exception;

		// Token: 0x04000BC3 RID: 3011
		private bool _IsTerminating;
	}
}
