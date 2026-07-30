using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you call the <see cref="M:System.Data.DataRow.EndEdit" /> method within the <see cref="E:System.Data.DataTable.RowChanging" /> event.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200006D RID: 109
	[Serializable]
	public class InRowChangingEventException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InRowChangingEventException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x0600041E RID: 1054 RVA: 0x000143E9 File Offset: 0x000125E9
		protected InRowChangingEventException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InRowChangingEventException" /> class.</summary>
		// Token: 0x0600041F RID: 1055 RVA: 0x000144BB File Offset: 0x000126BB
		public InRowChangingEventException()
			: base("Operation not supported in the RowChanging event.")
		{
			base.HResult = -2146232029;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InRowChangingEventException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x06000420 RID: 1056 RVA: 0x000144D3 File Offset: 0x000126D3
		public InRowChangingEventException(string s)
			: base(s)
		{
			base.HResult = -2146232029;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InRowChangingEventException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x06000421 RID: 1057 RVA: 0x000144E7 File Offset: 0x000126E7
		public InRowChangingEventException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232029;
		}
	}
}
