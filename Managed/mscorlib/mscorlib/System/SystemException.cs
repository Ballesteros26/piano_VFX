using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Defines the base class for predefined exceptions in the <see cref="N:System" /> namespace.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C7 RID: 455
	[ComVisible(true)]
	[Serializable]
	public class SystemException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class.</summary>
		// Token: 0x0600142B RID: 5163 RVA: 0x00051CA8 File Offset: 0x0004FEA8
		public SystemException()
			: base(Environment.GetResourceString("System error."))
		{
			base.SetErrorCode(-2146233087);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x0600142C RID: 5164 RVA: 0x00051CC5 File Offset: 0x0004FEC5
		public SystemException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233087);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600142D RID: 5165 RVA: 0x00051CD9 File Offset: 0x0004FED9
		public SystemException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233087);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x0600142E RID: 5166 RVA: 0x000325DC File Offset: 0x000307DC
		protected SystemException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
