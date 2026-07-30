using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Web.Security
{
	/// <summary>The exception that is thrown when a password cannot be retrieved from the password store.</summary>
	// Token: 0x0200000F RID: 15
	[TypeForwardedFrom("System.Web, Version=2.0.0.0, Culture=Neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Serializable]
	public class MembershipPasswordException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.MembershipPasswordException" /> class and sets the <see cref="P:System.Exception.Message" /> property to the supplied <paramref name="message" />.</summary>
		/// <param name="message">A description of the reason for the exception.</param>
		// Token: 0x06000034 RID: 52 RVA: 0x000025A4 File Offset: 0x000007A4
		public MembershipPasswordException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.MembershipPasswordException" /> class with the supplied serialization information and context. </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06000035 RID: 53 RVA: 0x000025AD File Offset: 0x000007AD
		protected MembershipPasswordException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.MembershipPasswordException" /> class. </summary>
		// Token: 0x06000036 RID: 54 RVA: 0x000025B7 File Offset: 0x000007B7
		public MembershipPasswordException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.MembershipPasswordException" /> class and sets the <see cref="P:System.Exception.Message" /> property to the supplied <paramref name="message" /> and the <see cref="P:System.Exception.InnerException" /> property to the supplied <paramref name="innerException" />.</summary>
		/// <param name="message">A description of the reason for the exception.</param>
		/// <param name="innerException">The exception that caused the <see cref="T:System.Web.Security.MembershipPasswordException" />.</param>
		// Token: 0x06000037 RID: 55 RVA: 0x000025BF File Offset: 0x000007BF
		public MembershipPasswordException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
