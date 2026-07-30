using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an invalid attempt to access a method, such as accessing a private method from partially trusted code.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200019C RID: 412
	[ComVisible(true)]
	[Serializable]
	public class MethodAccessException : MemberAccessException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MethodAccessException" /> class, setting the <see cref="P:System.Exception.Message" /> property of the new instance to a system-supplied message that describes the error, such as "Attempt to access the method failed." This message takes into account the current system culture.</summary>
		// Token: 0x0600117D RID: 4477 RVA: 0x00048051 File Offset: 0x00046251
		public MethodAccessException()
			: base(Environment.GetResourceString("Attempt to access the method failed."))
		{
			base.SetErrorCode(-2146233072);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MethodAccessException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x0600117E RID: 4478 RVA: 0x0004806E File Offset: 0x0004626E
		public MethodAccessException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233072);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MethodAccessException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600117F RID: 4479 RVA: 0x00048082 File Offset: 0x00046282
		public MethodAccessException(string message, Exception inner)
			: base(message, inner)
		{
			base.SetErrorCode(-2146233072);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MethodAccessException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06001180 RID: 4480 RVA: 0x0003EAF7 File Offset: 0x0003CCF7
		protected MethodAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
