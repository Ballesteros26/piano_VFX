using System;

namespace System.Threading
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Application.ThreadException" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200012D RID: 301
	public class ThreadExceptionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadExceptionEventArgs" /> class.</summary>
		/// <param name="t">The <see cref="T:System.Exception" /> that occurred. </param>
		// Token: 0x06000831 RID: 2097 RVA: 0x0002825D File Offset: 0x0002645D
		public ThreadExceptionEventArgs(Exception t)
		{
			this.exception = t;
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> that occurred.</summary>
		/// <returns>The <see cref="T:System.Exception" /> that occurred.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0002826C File Offset: 0x0002646C
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x04000D9C RID: 3484
		private Exception exception;
	}
}
