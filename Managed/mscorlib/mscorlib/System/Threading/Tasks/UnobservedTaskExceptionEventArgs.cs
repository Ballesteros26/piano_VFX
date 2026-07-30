using System;

namespace System.Threading.Tasks
{
	/// <summary>Provides data for the event that is raised when a faulted <see cref="T:System.Threading.Tasks.Task" />'s exception goes unobserved.</summary>
	// Token: 0x02000521 RID: 1313
	public class UnobservedTaskExceptionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.UnobservedTaskExceptionEventArgs" /> class with the unobserved exception.</summary>
		/// <param name="exception">The Exception that has gone unobserved.</param>
		// Token: 0x06003BF6 RID: 15350 RVA: 0x000D8A88 File Offset: 0x000D6C88
		public UnobservedTaskExceptionEventArgs(AggregateException exception)
		{
			this.m_exception = exception;
		}

		/// <summary>Marks the <see cref="P:System.Threading.Tasks.UnobservedTaskExceptionEventArgs.Exception" /> as "observed," thus preventing it from triggering exception escalation policy which, by default, terminates the process.</summary>
		// Token: 0x06003BF7 RID: 15351 RVA: 0x000D8A97 File Offset: 0x000D6C97
		public void SetObserved()
		{
			this.m_observed = true;
		}

		/// <summary>Gets whether this exception has been marked as "observed."</summary>
		/// <returns>true if this exception has been marked as "observed"; otherwise false.</returns>
		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06003BF8 RID: 15352 RVA: 0x000D8AA0 File Offset: 0x000D6CA0
		public bool Observed
		{
			get
			{
				return this.m_observed;
			}
		}

		/// <summary>The Exception that went unobserved.</summary>
		/// <returns>The Exception that went unobserved.</returns>
		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06003BF9 RID: 15353 RVA: 0x000D8AA8 File Offset: 0x000D6CA8
		public AggregateException Exception
		{
			get
			{
				return this.m_exception;
			}
		}

		// Token: 0x04001F15 RID: 7957
		private AggregateException m_exception;

		// Token: 0x04001F16 RID: 7958
		internal bool m_observed;
	}
}
