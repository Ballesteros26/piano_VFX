using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.BindingManagerBase.DataError" /> event. </summary>
	// Token: 0x0200005F RID: 95
	public class BindingManagerDataErrorEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingManagerDataErrorEventArgs" /> class. </summary>
		/// <param name="exception">The <see cref="T:System.Exception" /> that occurred in the binding process that caused the <see cref="E:System.Windows.Forms.BindingManagerBase.DataError" /> event to be raised.</param>
		// Token: 0x060003C3 RID: 963 RVA: 0x0001337C File Offset: 0x0001157C
		public BindingManagerDataErrorEventArgs(Exception exception)
		{
			this.exception = exception;
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> caught in the binding process that caused the <see cref="E:System.Windows.Forms.BindingManagerBase.DataError" /> event to be raised.</summary>
		/// <returns>The <see cref="T:System.Exception" /> that caused the <see cref="E:System.Windows.Forms.BindingManagerBase.DataError" /> event to be raised. </returns>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0001338C File Offset: 0x0001158C
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x0400063A RID: 1594
		private Exception exception;
	}
}
