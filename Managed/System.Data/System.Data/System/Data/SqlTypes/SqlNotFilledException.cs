using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.SqlNotFilledException" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x020002D3 RID: 723
	[Serializable]
	public sealed class SqlNotFilledException : SqlTypeException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNotFilledException" /> class.</summary>
		// Token: 0x0600216A RID: 8554 RVA: 0x0009CBED File Offset: 0x0009ADED
		public SqlNotFilledException()
			: this(SQLResource.NotFilledMessage, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNotFilledException" /> class.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		// Token: 0x0600216B RID: 8555 RVA: 0x0009CBFB File Offset: 0x0009ADFB
		public SqlNotFilledException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNotFilledException" /> class.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		/// <param name="e">A reference to an inner exception.</param>
		// Token: 0x0600216C RID: 8556 RVA: 0x0009CB5F File Offset: 0x0009AD5F
		public SqlNotFilledException(string message, Exception e)
			: base(message, e)
		{
			base.HResult = -2146232015;
		}
	}
}
