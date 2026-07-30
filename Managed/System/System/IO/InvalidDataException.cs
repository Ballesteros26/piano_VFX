using System;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when a data stream is in an invalid format.</summary>
	// Token: 0x020003DA RID: 986
	[Serializable]
	public sealed class InvalidDataException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class.</summary>
		// Token: 0x06001E2B RID: 7723 RVA: 0x00077F64 File Offset: 0x00076164
		public InvalidDataException()
			: base(global::Locale.GetText("Invalid data format."))
		{
			base.HResult = -2146233085;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06001E2C RID: 7724 RVA: 0x00077F81 File Offset: 0x00076181
		public InvalidDataException(string message)
			: base(message)
		{
			base.HResult = -2146233085;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class with a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x06001E2D RID: 7725 RVA: 0x00077F95 File Offset: 0x00076195
		public InvalidDataException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233085;
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x0006D1C3 File Offset: 0x0006B3C3
		private InvalidDataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04001A5C RID: 6748
		private const int Result = -2146233085;
	}
}
