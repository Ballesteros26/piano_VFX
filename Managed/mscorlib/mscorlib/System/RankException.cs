using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an array with the wrong number of dimensions is passed to a method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B4 RID: 436
	[ComVisible(true)]
	[Serializable]
	public class RankException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.RankException" /> class.</summary>
		// Token: 0x0600121E RID: 4638 RVA: 0x00049E61 File Offset: 0x00048061
		public RankException()
			: base(Environment.GetResourceString("Attempted to operate on an array with the incorrect number of dimensions."))
		{
			base.SetErrorCode(-2146233065);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.RankException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. </param>
		// Token: 0x0600121F RID: 4639 RVA: 0x00049E7E File Offset: 0x0004807E
		public RankException(string message)
			: base(message)
		{
			base.SetErrorCode(-2146233065);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.RankException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (Nothing in Visual Basic), the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06001220 RID: 4640 RVA: 0x00049E92 File Offset: 0x00048092
		public RankException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.SetErrorCode(-2146233065);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.RankException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06001221 RID: 4641 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected RankException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
