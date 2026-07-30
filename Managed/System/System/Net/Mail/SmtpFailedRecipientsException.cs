using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.Mail
{
	/// <summary>The exception that is thrown when e-mail is sent using an <see cref="T:System.Net.Mail.SmtpClient" /> and cannot be delivered to all recipients.</summary>
	// Token: 0x02000590 RID: 1424
	[Serializable]
	public class SmtpFailedRecipientsException : SmtpFailedRecipientException, ISerializable
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class.</summary>
		// Token: 0x06002C51 RID: 11345 RVA: 0x000AF095 File Offset: 0x000AD295
		public SmtpFailedRecipientsException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class with the specified <see cref="T:System.String" />.</summary>
		/// <param name="message">The exception message.</param>
		// Token: 0x06002C52 RID: 11346 RVA: 0x000AF09D File Offset: 0x000AD29D
		public SmtpFailedRecipientsException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class with the specified <see cref="T:System.String" /> and inner <see cref="T:System.Exception" />.</summary>
		/// <param name="message">The exception message.</param>
		/// <param name="innerException">The inner exception.</param>
		// Token: 0x06002C53 RID: 11347 RVA: 0x000AF0A6 File Offset: 0x000AD2A6
		public SmtpFailedRecipientsException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class with the specified <see cref="T:System.String" /> and array of type <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />.</summary>
		/// <param name="message">The exception message.</param>
		/// <param name="innerExceptions">The array of recipients with delivery errors.</param>
		// Token: 0x06002C54 RID: 11348 RVA: 0x000AF0B0 File Offset: 0x000AD2B0
		public SmtpFailedRecipientsException(string message, SmtpFailedRecipientException[] innerExceptions)
			: base(message)
		{
			this.innerExceptions = innerExceptions;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information required to serialize the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> instance. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> instance. </param>
		// Token: 0x06002C55 RID: 11349 RVA: 0x000AF0C0 File Offset: 0x000AD2C0
		protected SmtpFailedRecipientsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.innerExceptions = (SmtpFailedRecipientException[])info.GetValue("innerExceptions", typeof(SmtpFailedRecipientException[]));
		}

		/// <summary>Gets one or more <see cref="T:System.Net.Mail.SmtpFailedRecipientException" />s that indicate the e-mail recipients with SMTP delivery errors.</summary>
		/// <returns>An array of type <see cref="T:System.Net.Mail.SmtpFailedRecipientException" /> that lists the recipients with delivery errors.</returns>
		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002C56 RID: 11350 RVA: 0x000AF0F8 File Offset: 0x000AD2F8
		public SmtpFailedRecipientException[] InnerExceptions
		{
			get
			{
				return this.innerExceptions;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data that is needed to serialize the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used. </param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> to be used. </param>
		// Token: 0x06002C57 RID: 11351 RVA: 0x000AF100 File Offset: 0x000AD300
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			if (serializationInfo == null)
			{
				throw new ArgumentNullException("serializationInfo");
			}
			base.GetObjectData(serializationInfo, streamingContext);
			serializationInfo.AddValue("innerExceptions", this.innerExceptions);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.Mail.SmtpFailedRecipientsException" />. </param>
		// Token: 0x06002C58 RID: 11352 RVA: 0x000800E6 File Offset: 0x0007E2E6
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		// Token: 0x040024C0 RID: 9408
		private SmtpFailedRecipientException[] innerExceptions;
	}
}
