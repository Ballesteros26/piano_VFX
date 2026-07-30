using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to combine two delegates based on the <see cref="T:System.Delegate" /> type instead of the <see cref="T:System.MulticastDelegate" /> type. This class cannot be inherited. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001A1 RID: 417
	[ComVisible(true)]
	[Serializable]
	public sealed class MulticastNotSupportedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MulticastNotSupportedException" /> class.</summary>
		// Token: 0x06001199 RID: 4505 RVA: 0x0004840D File Offset: 0x0004660D
		public MulticastNotSupportedException()
			: base(Environment.GetResourceString("Attempted to add multiple callbacks to a delegate that does not support multicast."))
		{
			base.SetErrorCode(-2146233068);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MulticastNotSupportedException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x0600119A RID: 4506 RVA: 0x0004842A File Offset: 0x0004662A
		public MulticastNotSupportedException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233068);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MulticastNotSupportedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600119B RID: 4507 RVA: 0x0004843E File Offset: 0x0004663E
		public MulticastNotSupportedException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233068);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00031FC1 File Offset: 0x000301C1
		internal MulticastNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
