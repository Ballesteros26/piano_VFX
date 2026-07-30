using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt to unload an application domain fails.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000136 RID: 310
	[ComVisible(true)]
	[Serializable]
	public class CannotUnloadAppDomainException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CannotUnloadAppDomainException" /> class.</summary>
		// Token: 0x06000B37 RID: 2871 RVA: 0x00034C82 File Offset: 0x00032E82
		public CannotUnloadAppDomainException()
			: base(Environment.GetResourceString("Attempt to unload the AppDomain failed."))
		{
			base.SetErrorCode(-2146234347);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CannotUnloadAppDomainException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x06000B38 RID: 2872 RVA: 0x00034C9F File Offset: 0x00032E9F
		public CannotUnloadAppDomainException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146234347);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CannotUnloadAppDomainException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000B39 RID: 2873 RVA: 0x00034CB3 File Offset: 0x00032EB3
		public CannotUnloadAppDomainException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146234347);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CannotUnloadAppDomainException" /> class from serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000B3A RID: 2874 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected CannotUnloadAppDomainException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
