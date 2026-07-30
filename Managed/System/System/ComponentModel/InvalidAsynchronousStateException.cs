using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Thrown when a thread on which an operation should execute no longer exists or has no message loop. </summary>
	// Token: 0x02000298 RID: 664
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[Serializable]
	public class InvalidAsynchronousStateException : ArgumentException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class. </summary>
		// Token: 0x060014B0 RID: 5296 RVA: 0x00052F71 File Offset: 0x00051171
		public InvalidAsynchronousStateException()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class with the specified detailed description.</summary>
		/// <param name="message">A detailed description of the error.</param>
		// Token: 0x060014B1 RID: 5297 RVA: 0x00052F7A File Offset: 0x0005117A
		public InvalidAsynchronousStateException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class with the specified detailed description and the specified exception. </summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x060014B2 RID: 5298 RVA: 0x00052F83 File Offset: 0x00051183
		public InvalidAsynchronousStateException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InvalidAsynchronousStateException" /> class with the given <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />. </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x060014B3 RID: 5299 RVA: 0x00052F8D File Offset: 0x0005118D
		protected InvalidAsynchronousStateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
