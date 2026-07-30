using System;

namespace System.IO
{
	/// <summary>Provides data for the <see cref="E:System.IO.FileSystemWatcher.Error" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003C5 RID: 965
	public class ErrorEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.ErrorEventArgs" /> class.</summary>
		/// <param name="exception">An <see cref="T:System.Exception" /> that represents the error that occurred. </param>
		// Token: 0x06001DA4 RID: 7588 RVA: 0x00075CB2 File Offset: 0x00073EB2
		public ErrorEventArgs(Exception exception)
		{
			this.exception = exception;
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> that represents the error that occurred.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the error that occurred.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001DA5 RID: 7589 RVA: 0x00075CC1 File Offset: 0x00073EC1
		public virtual Exception GetException()
		{
			return this.exception;
		}

		// Token: 0x040019ED RID: 6637
		private Exception exception;
	}
}
