using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.Mail
{
	/// <summary>Represents the exception that is thrown when the <see cref="T:System.Net.Mail.SmtpClient" /> is not able to complete a <see cref="Overload:System.Net.Mail.SmtpClient.Send" /> or <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> operation.</summary>
	// Token: 0x0200058E RID: 1422
	[Serializable]
	public class SmtpException : Exception, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class. </summary>
		// Token: 0x06002C3D RID: 11325 RVA: 0x000AEEF6 File Offset: 0x000AD0F6
		public SmtpException()
			: this(SmtpStatusCode.GeneralFailure)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified status code.</summary>
		/// <param name="statusCode">An <see cref="T:System.Net.Mail.SmtpStatusCode" /> value.</param>
		// Token: 0x06002C3E RID: 11326 RVA: 0x000AEEFF File Offset: 0x000AD0FF
		public SmtpException(SmtpStatusCode statusCode)
			: this(statusCode, "Syntax error, command unrecognized.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		// Token: 0x06002C3F RID: 11327 RVA: 0x000AEF0D File Offset: 0x000AD10D
		public SmtpException(string message)
			: this(SmtpStatusCode.GeneralFailure, message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes. </summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.Mail.SmtpException" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source and destination of the serialized stream associated with the new instance. </param>
		// Token: 0x06002C40 RID: 11328 RVA: 0x000AEF18 File Offset: 0x000AD118
		protected SmtpException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
			try
			{
				this.statusCode = (SmtpStatusCode)serializationInfo.GetValue("Status", typeof(int));
			}
			catch (SerializationException)
			{
				this.statusCode = (SmtpStatusCode)serializationInfo.GetValue("statusCode", typeof(SmtpStatusCode));
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified status code and error message.</summary>
		/// <param name="statusCode">An <see cref="T:System.Net.Mail.SmtpStatusCode" /> value.</param>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		// Token: 0x06002C41 RID: 11329 RVA: 0x000AEF84 File Offset: 0x000AD184
		public SmtpException(SmtpStatusCode statusCode, string message)
			: base(message)
		{
			this.statusCode = statusCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpException" /> class with the specified error message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. </param>
		// Token: 0x06002C42 RID: 11330 RVA: 0x000AEF94 File Offset: 0x000AD194
		public SmtpException(string message, Exception innerException)
			: base(message, innerException)
		{
			this.statusCode = SmtpStatusCode.GeneralFailure;
		}

		/// <summary>Gets the status code returned by an SMTP server when an e-mail message is transmitted.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpStatusCode" /> value that indicates the error that occurred.</returns>
		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002C43 RID: 11331 RVA: 0x000AEFA5 File Offset: 0x000AD1A5
		// (set) Token: 0x06002C44 RID: 11332 RVA: 0x000AEFAD File Offset: 0x000AD1AD
		public SmtpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
			set
			{
				this.statusCode = value;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.Mail.SmtpException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06002C45 RID: 11333 RVA: 0x000AEFB6 File Offset: 0x000AD1B6
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			if (serializationInfo == null)
			{
				throw new ArgumentNullException("serializationInfo");
			}
			base.GetObjectData(serializationInfo, streamingContext);
			serializationInfo.AddValue("Status", this.statusCode, typeof(int));
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.Mail.SmtpException" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" />, which holds the serialized data for the <see cref="T:System.Net.Mail.SmtpException" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the destination of the serialized stream associated with the new <see cref="T:System.Net.Mail.SmtpException" />. </param>
		// Token: 0x06002C46 RID: 11334 RVA: 0x000800E6 File Offset: 0x0007E2E6
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		// Token: 0x040024BE RID: 9406
		private SmtpStatusCode statusCode;
	}
}
