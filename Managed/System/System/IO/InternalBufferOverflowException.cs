using System;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception thrown when the internal buffer overflows.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003D9 RID: 985
	[Serializable]
	public class InternalBufferOverflowException : SystemException
	{
		/// <summary>Initializes a new default instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class.</summary>
		// Token: 0x06001E27 RID: 7719 RVA: 0x00077F57 File Offset: 0x00076157
		public InternalBufferOverflowException()
			: base("Internal buffer overflow occurred.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class with the error message to be displayed specified.</summary>
		/// <param name="message">The message to be given for the exception. </param>
		// Token: 0x06001E28 RID: 7720 RVA: 0x00039BD8 File Offset: 0x00037DD8
		public InternalBufferOverflowException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new, empty instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class that is serializable using the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">The information required to serialize the T:System.IO.InternalBufferOverflowException object.</param>
		/// <param name="context">The source and destination of the serialized stream associated with the T:System.IO.InternalBufferOverflowException object.</param>
		// Token: 0x06001E29 RID: 7721 RVA: 0x0006D1C3 File Offset: 0x0006B3C3
		protected InternalBufferOverflowException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InternalBufferOverflowException" /> class with the message to be displayed and the generated inner exception specified.</summary>
		/// <param name="message">The message to be given for the exception. </param>
		/// <param name="inner">The inner exception. </param>
		// Token: 0x06001E2A RID: 7722 RVA: 0x00039C0D File Offset: 0x00037E0D
		public InternalBufferOverflowException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
