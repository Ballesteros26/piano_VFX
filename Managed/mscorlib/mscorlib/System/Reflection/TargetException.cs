using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>Represents the exception that is thrown when an attempt is made to invoke an invalid target.</summary>
	// Token: 0x02000300 RID: 768
	[ComVisible(true)]
	[Serializable]
	public class TargetException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetException" /> class with an empty message and the root cause of the exception.</summary>
		// Token: 0x06002112 RID: 8466 RVA: 0x0007F045 File Offset: 0x0007D245
		public TargetException()
		{
			base.SetErrorCode(-2146232829);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetException" /> class with the given message and the root cause exception.</summary>
		/// <param name="message">A String describing the reason why the exception occurred. </param>
		// Token: 0x06002113 RID: 8467 RVA: 0x0007F058 File Offset: 0x0007D258
		public TargetException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146232829);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06002114 RID: 8468 RVA: 0x0007F06C File Offset: 0x0007D26C
		public TargetException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146232829);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">The data for serializing or deserializing the object. </param>
		/// <param name="context">The source of and destination for the object. </param>
		// Token: 0x06002115 RID: 8469 RVA: 0x0007E05D File Offset: 0x0007C25D
		protected TargetException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
