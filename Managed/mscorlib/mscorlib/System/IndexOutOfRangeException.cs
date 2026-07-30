using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt is made to access an element of an array with an index that is outside the bounds of the array. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200018C RID: 396
	[ComVisible(true)]
	[Serializable]
	public sealed class IndexOutOfRangeException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IndexOutOfRangeException" /> class.</summary>
		// Token: 0x060010A8 RID: 4264 RVA: 0x00047363 File Offset: 0x00045563
		public IndexOutOfRangeException()
			: base(Environment.GetResourceString("Index was outside the bounds of the array."))
		{
			base.SetErrorCode(-2146233080);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IndexOutOfRangeException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x060010A9 RID: 4265 RVA: 0x00047380 File Offset: 0x00045580
		public IndexOutOfRangeException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233080);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IndexOutOfRangeException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x060010AA RID: 4266 RVA: 0x00047394 File Offset: 0x00045594
		public IndexOutOfRangeException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233080);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00031FC1 File Offset: 0x000301C1
		internal IndexOutOfRangeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
