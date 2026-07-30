using System;
using System.Runtime.Serialization;

namespace System.Security.Authentication
{
	/// <summary>The exception that is thrown when authentication fails for an authentication stream and cannot be retried.</summary>
	// Token: 0x0200037E RID: 894
	[Serializable]
	public class InvalidCredentialException : AuthenticationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class with no message. </summary>
		// Token: 0x06001B4C RID: 6988 RVA: 0x0006D1CD File Offset: 0x0006B3CD
		public InvalidCredentialException()
			: base(global::Locale.GetText("Invalid credentials exception."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the authentication failure.</param>
		// Token: 0x06001B4D RID: 6989 RVA: 0x0006D1DF File Offset: 0x0006B3DF
		public InvalidCredentialException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the authentication failure.</param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> that is the cause of the current exception.</param>
		// Token: 0x06001B4E RID: 6990 RVA: 0x0006D1E8 File Offset: 0x0006B3E8
		public InvalidCredentialException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information required to deserialize the new <see cref="T:System.Security.Authentication.InvalidCredentialException" /> instance. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> instance. </param>
		// Token: 0x06001B4F RID: 6991 RVA: 0x0006D1F2 File Offset: 0x0006B3F2
		protected InvalidCredentialException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}
	}
}
