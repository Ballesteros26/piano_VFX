using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.Mail
{
	/// <summary>Represents the exception that is thrown when the <see cref="T:System.Net.Mail.SmtpClient" /> is not able to complete a <see cref="Overload:System.Net.Mail.SmtpClient.Send" /> or <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> operation to a particular recipient.</summary>
	// Token: 0x0200058F RID: 1423
	[Serializable]
	public class SmtpFailedRecipientException : SmtpException, ISerializable
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> class.</summary>
		// Token: 0x06002C47 RID: 11335 RVA: 0x000AEFEE File Offset: 0x000AD1EE
		public SmtpFailedRecipientException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> class with the specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains the error message.</param>
		// Token: 0x06002C48 RID: 11336 RVA: 0x000AEFF6 File Offset: 0x000AD1F6
		public SmtpFailedRecipientException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source and destination of the serialized stream that is associated with the new instance. </param>
		// Token: 0x06002C49 RID: 11337 RVA: 0x000AEFFF File Offset: 0x000AD1FF
		protected SmtpFailedRecipientException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.failedRecipient = info.GetString("failedRecipient");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> class with the specified status code and e-mail address.</summary>
		/// <param name="statusCode">An <see cref="T:System.Net.Mail.SmtpStatusCode" /> value.</param>
		/// <param name="failedRecipient">A <see cref="T:System.String" /> that contains the e-mail address.</param>
		// Token: 0x06002C4A RID: 11338 RVA: 0x000AF028 File Offset: 0x000AD228
		public SmtpFailedRecipientException(SmtpStatusCode statusCode, string failedRecipient)
			: base(statusCode)
		{
			this.failedRecipient = failedRecipient;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified error message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. </param>
		// Token: 0x06002C4B RID: 11339 RVA: 0x000AF038 File Offset: 0x000AD238
		public SmtpFailedRecipientException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified error message, e-mail address, and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		/// <param name="failedRecipient">A <see cref="T:System.String" /> that contains the e-mail address.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		// Token: 0x06002C4C RID: 11340 RVA: 0x000AF042 File Offset: 0x000AD242
		public SmtpFailedRecipientException(string message, string failedRecipient, Exception innerException)
			: base(message, innerException)
		{
			this.failedRecipient = failedRecipient;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> class with the specified status code, e-mail address, and server response.</summary>
		/// <param name="statusCode">An <see cref="T:System.Net.Mail.SmtpStatusCode" /> value.</param>
		/// <param name="failedRecipient">A <see cref="T:System.String" /> that contains the e-mail address.</param>
		/// <param name="serverResponse">A <see cref="T:System.String" /> that contains the server response.</param>
		// Token: 0x06002C4D RID: 11341 RVA: 0x000AF053 File Offset: 0x000AD253
		public SmtpFailedRecipientException(SmtpStatusCode statusCode, string failedRecipient, string serverResponse)
			: base(statusCode, serverResponse)
		{
			this.failedRecipient = failedRecipient;
		}

		/// <summary>Indicates the e-mail address with delivery difficulties.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the e-mail address.</returns>
		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002C4E RID: 11342 RVA: 0x000AF064 File Offset: 0x000AD264
		public string FailedRecipient
		{
			get
			{
				return this.failedRecipient;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data that is needed to serialize the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06002C4F RID: 11343 RVA: 0x000AF06C File Offset: 0x000AD26C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			if (serializationInfo == null)
			{
				throw new ArgumentNullException("serializationInfo");
			}
			base.GetObjectData(serializationInfo, streamingContext);
			serializationInfo.AddValue("failedRecipient", this.failedRecipient);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data that is needed to serialize the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance, which holds the serialized data for the <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> instance that contains the destination of the serialized stream that is associated with the new <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />. </param>
		// Token: 0x06002C50 RID: 11344 RVA: 0x000800E6 File Offset: 0x0007E2E6
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x040024BF RID: 9407
		private string failedRecipient;
	}
}
