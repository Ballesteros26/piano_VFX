using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a <see cref="T:System.Threading.Thread" /> is in an invalid <see cref="P:System.Threading.Thread.ThreadState" /> for the method call.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200049B RID: 1179
	[ComVisible(true)]
	[Serializable]
	public class ThreadStateException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadStateException" /> class with default properties.</summary>
		// Token: 0x06003770 RID: 14192 RVA: 0x000CA36E File Offset: 0x000C856E
		public ThreadStateException()
			: base(Environment.GetResourceString("Thread was in an invalid state for the operation being executed."))
		{
			base.SetErrorCode(-2146233056);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadStateException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06003771 RID: 14193 RVA: 0x000CA38B File Offset: 0x000C858B
		public ThreadStateException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233056);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadStateException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06003772 RID: 14194 RVA: 0x000CA39F File Offset: 0x000C859F
		public ThreadStateException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233056);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadStateException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06003773 RID: 14195 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected ThreadStateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
