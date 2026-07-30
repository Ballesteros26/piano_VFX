using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt to marshal an object across a context boundary fails.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000142 RID: 322
	[ComVisible(true)]
	[Serializable]
	public class ContextMarshalException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ContextMarshalException" /> class with default properties.</summary>
		// Token: 0x06000BA9 RID: 2985 RVA: 0x00035C27 File Offset: 0x00033E27
		public ContextMarshalException()
			: base(Environment.GetResourceString("Attempted to marshal an object across a context boundary."))
		{
			base.SetErrorCode(-2146233084);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ContextMarshalException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06000BAA RID: 2986 RVA: 0x00035C44 File Offset: 0x00033E44
		public ContextMarshalException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233084);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ContextMarshalException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000BAB RID: 2987 RVA: 0x00035C58 File Offset: 0x00033E58
		public ContextMarshalException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233084);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ContextMarshalException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		// Token: 0x06000BAC RID: 2988 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected ContextMarshalException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
