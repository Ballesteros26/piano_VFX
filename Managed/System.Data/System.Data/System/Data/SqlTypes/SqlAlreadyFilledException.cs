using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.SqlAlreadyFilledException" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
	// Token: 0x020002D4 RID: 724
	[Serializable]
	public sealed class SqlAlreadyFilledException : SqlTypeException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlAlreadyFilledException" /> class.</summary>
		// Token: 0x0600216D RID: 8557 RVA: 0x0009CC05 File Offset: 0x0009AE05
		public SqlAlreadyFilledException()
			: this(SQLResource.AlreadyFilledMessage, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlAlreadyFilledException" /> class.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		// Token: 0x0600216E RID: 8558 RVA: 0x0009CC13 File Offset: 0x0009AE13
		public SqlAlreadyFilledException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlAlreadyFilledException" /> class.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		/// <param name="e">A reference to an inner exception.</param>
		// Token: 0x0600216F RID: 8559 RVA: 0x0009CB5F File Offset: 0x0009AD5F
		public SqlAlreadyFilledException(string message, Exception e)
			: base(message, e)
		{
			base.HResult = -2146232015;
		}
	}
}
