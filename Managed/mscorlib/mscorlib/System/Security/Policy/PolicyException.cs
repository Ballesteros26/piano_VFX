using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Policy
{
	/// <summary>The exception that is thrown when policy forbids code to run.</summary>
	// Token: 0x02000574 RID: 1396
	[ComVisible(true)]
	[Serializable]
	public class PolicyException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyException" /> class with default properties.</summary>
		// Token: 0x06003E7F RID: 15999 RVA: 0x000DFBEC File Offset: 0x000DDDEC
		public PolicyException()
			: base(Locale.GetText("Cannot run because of policy."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06003E80 RID: 16000 RVA: 0x000C7E43 File Offset: 0x000C6043
		public PolicyException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06003E81 RID: 16001 RVA: 0x00031FC1 File Offset: 0x000301C1
		protected PolicyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="exception">The exception that is the cause of the current exception. If the <paramref name="exception" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x06003E82 RID: 16002 RVA: 0x000C7E4C File Offset: 0x000C604C
		public PolicyException(string message, Exception exception)
			: base(message, exception)
		{
		}
	}
}
