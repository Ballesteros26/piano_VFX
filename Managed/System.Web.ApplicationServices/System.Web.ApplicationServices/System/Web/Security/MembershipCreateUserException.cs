using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.Security
{
	/// <summary>The exception that is thrown when a user is not successfully created by a membership provider.</summary>
	// Token: 0x0200000E RID: 14
	[TypeForwardedFrom("System.Web, Version=2.0.0.0, Culture=Neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Serializable]
	public class MembershipCreateUserException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.MembershipCreateUserException" /> class with the specified <see cref="P:System.Web.Security.MembershipCreateUserException.StatusCode" /> value.</summary>
		/// <param name="statusCode">A <see cref="T:System.Web.Security.MembershipCreateStatus" /> enumeration value that describes the reason for the exception.</param>
		// Token: 0x0600002C RID: 44 RVA: 0x0000247F File Offset: 0x0000067F
		public MembershipCreateUserException(MembershipCreateStatus statusCode)
			: base(MembershipCreateUserException.GetMessageFromStatusCode(statusCode))
		{
			this._StatusCode = statusCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.MembershipCreateUserException" /> class and sets the <see cref="P:System.Exception.Message" /> property to the supplied <paramref name="message" /> parameter value</summary>
		/// <param name="message">A description of the reason for the exception.</param>
		// Token: 0x0600002D RID: 45 RVA: 0x0000249C File Offset: 0x0000069C
		public MembershipCreateUserException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.MembershipCreateUserException" /> class with the supplied serialization information and context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" />  that contains contextual information about the source or destination.</param>
		// Token: 0x0600002E RID: 46 RVA: 0x000024AD File Offset: 0x000006AD
		protected MembershipCreateUserException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._StatusCode = (MembershipCreateStatus)info.GetInt32("_StatusCode");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.MembershipCreateUserException" /> class.</summary>
		// Token: 0x0600002F RID: 47 RVA: 0x000024D0 File Offset: 0x000006D0
		public MembershipCreateUserException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.MembershipCreateUserException" /> class and sets the <see cref="P:System.Exception.Message" /> property to the supplied <paramref name="message" /> and the <see cref="P:System.Exception.InnerException" /> property to the supplied <paramref name="innerException" />.</summary>
		/// <param name="message">A description of the reason for the exception.</param>
		/// <param name="innerException">The exception that caused the <see cref="T:System.Web.Security.MembershipCreateUserException" />.</param>
		// Token: 0x06000030 RID: 48 RVA: 0x000024E0 File Offset: 0x000006E0
		public MembershipCreateUserException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Gets a description of the reason for the exception.</summary>
		/// <returns>A <see cref="T:System.Web.Security.MembershipCreateStatus" /> enumeration value that describes the reason for the exception.</returns>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000024F2 File Offset: 0x000006F2
		public MembershipCreateStatus StatusCode
		{
			get
			{
				return this._StatusCode;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06000032 RID: 50 RVA: 0x000024FA File Offset: 0x000006FA
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("_StatusCode", this._StatusCode);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000251C File Offset: 0x0000071C
		internal static string GetMessageFromStatusCode(MembershipCreateStatus statusCode)
		{
			switch (statusCode)
			{
			case MembershipCreateStatus.Success:
				return "No Error.";
			case MembershipCreateStatus.InvalidUserName:
				return "The username supplied is invalid.";
			case MembershipCreateStatus.InvalidPassword:
				return "The password supplied is invalid.  Passwords must conform to the password strength requirements configured for the default provider.";
			case MembershipCreateStatus.InvalidQuestion:
				return "The password-question supplied is invalid.  Note that the current provider configuration requires a valid password question and answer.  As a result, a CreateUser overload that accepts question and answer parameters must also be used.";
			case MembershipCreateStatus.InvalidAnswer:
				return "The password-answer supplied is invalid.";
			case MembershipCreateStatus.InvalidEmail:
				return "The E-mail supplied is invalid.";
			case MembershipCreateStatus.DuplicateUserName:
				return "The username is already in use.";
			case MembershipCreateStatus.DuplicateEmail:
				return "The E-mail address is already in use.";
			case MembershipCreateStatus.UserRejected:
				return "The user was rejected.";
			case MembershipCreateStatus.InvalidProviderUserKey:
				return "The provider user key supplied is invalid. It must be of type System.Guid.";
			case MembershipCreateStatus.DuplicateProviderUserKey:
				return "The provider user key is already in use.";
			default:
				return "The Provider encountered an unknown error.";
			}
		}

		// Token: 0x04000051 RID: 81
		private MembershipCreateStatus _StatusCode = MembershipCreateStatus.ProviderError;
	}
}
