using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200010F RID: 271
	public class DataGridViewDataErrorEventArgs : DataGridViewCellCancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs" /> class.</summary>
		/// <param name="exception">The exception that occurred.</param>
		/// <param name="columnIndex">The column index of the cell that raised the <see cref="E:System.Windows.Forms.DataGridView.DataError" />.</param>
		/// <param name="rowIndex">The row index of the cell that raised the <see cref="E:System.Windows.Forms.DataGridView.DataError" />.</param>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values indicating the context in which the error occurred. </param>
		// Token: 0x06001408 RID: 5128 RVA: 0x0004C334 File Offset: 0x0004A534
		public DataGridViewDataErrorEventArgs(Exception exception, int columnIndex, int rowIndex, DataGridViewDataErrorContexts context)
			: base(columnIndex, rowIndex)
		{
			this.exception = exception;
			this.context = context;
			this.throwException = false;
		}

		/// <summary>Gets details about the state of the <see cref="T:System.Windows.Forms.DataGridView" /> when the error occurred.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that specifies the context in which the error occurred.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x0004C354 File Offset: 0x0004A554
		public DataGridViewDataErrorContexts Context
		{
			get
			{
				return this.context;
			}
		}

		/// <summary>Gets the exception that represents the error.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the error.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x0004C35C File Offset: 0x0004A55C
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		/// <summary>Gets or sets a value indicating whether to throw the exception after the <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventHandler" /> delegate is finished with it.</summary>
		/// <returns>true if the exception should be thrown; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentException">When setting this property to true, the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.Exception" /> property value is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x0004C364 File Offset: 0x0004A564
		// (set) Token: 0x0600140C RID: 5132 RVA: 0x0004C36C File Offset: 0x0004A56C
		public bool ThrowException
		{
			get
			{
				return this.throwException;
			}
			set
			{
				this.throwException = value;
			}
		}

		// Token: 0x04000BA1 RID: 2977
		private Exception exception;

		// Token: 0x04000BA2 RID: 2978
		private DataGridViewDataErrorContexts context;

		// Token: 0x04000BA3 RID: 2979
		private bool throwException;
	}
}
