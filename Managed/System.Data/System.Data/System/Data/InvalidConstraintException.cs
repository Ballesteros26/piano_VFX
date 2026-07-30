using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when incorrectly trying to create or access a relation.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200006E RID: 110
	[Serializable]
	public class InvalidConstraintException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidConstraintException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object. </param>
		/// <param name="context">Description of the source and destination of the specified serialized stream. </param>
		// Token: 0x06000422 RID: 1058 RVA: 0x000143E9 File Offset: 0x000125E9
		protected InvalidConstraintException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidConstraintException" /> class.</summary>
		// Token: 0x06000423 RID: 1059 RVA: 0x000144FC File Offset: 0x000126FC
		public InvalidConstraintException()
			: base("Invalid constraint.")
		{
			base.HResult = -2146232028;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidConstraintException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x06000424 RID: 1060 RVA: 0x00014514 File Offset: 0x00012714
		public InvalidConstraintException(string s)
			: base(s)
		{
			base.HResult = -2146232028;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidConstraintException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x06000425 RID: 1061 RVA: 0x00014528 File Offset: 0x00012728
		public InvalidConstraintException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232028;
		}
	}
}
