using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the format of an argument does not meet the parameter specifications of the invoked method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000163 RID: 355
	[ComVisible(true)]
	[Serializable]
	public class FormatException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class.</summary>
		// Token: 0x06000F3A RID: 3898 RVA: 0x0003EB01 File Offset: 0x0003CD01
		public FormatException()
			: base(Environment.GetResourceString("One of the identified items was in an invalid format."))
		{
			base.SetErrorCode(-2146233033);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000F3B RID: 3899 RVA: 0x0003EB1E File Offset: 0x0003CD1E
		public FormatException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233033);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06000F3C RID: 3900 RVA: 0x0003EB32 File Offset: 0x0003CD32
		public FormatException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233033);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06000F3D RID: 3901 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected FormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
