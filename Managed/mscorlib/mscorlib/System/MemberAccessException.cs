using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt to access a class member fails.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200019B RID: 411
	[ComVisible(true)]
	[Serializable]
	public class MemberAccessException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MemberAccessException" /> class.</summary>
		// Token: 0x06001179 RID: 4473 RVA: 0x0004800B File Offset: 0x0004620B
		public MemberAccessException()
			: base(Environment.GetResourceString("Cannot access member."))
		{
			base.SetErrorCode(-2146233062);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MemberAccessException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x0600117A RID: 4474 RVA: 0x00048028 File Offset: 0x00046228
		public MemberAccessException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233062);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MemberAccessException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600117B RID: 4475 RVA: 0x0004803C File Offset: 0x0004623C
		public MemberAccessException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233062);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MemberAccessException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x0600117C RID: 4476 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected MemberAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
