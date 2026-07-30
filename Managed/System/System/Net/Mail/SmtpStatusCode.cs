using System;

namespace System.Net.Mail
{
	/// <summary>Specifies the outcome of sending e-mail by using the <see cref="T:System.Net.Mail.SmtpClient" /> class.</summary>
	// Token: 0x02000593 RID: 1427
	public enum SmtpStatusCode
	{
		/// <summary>The commands were sent in the incorrect sequence.</summary>
		// Token: 0x040024C6 RID: 9414
		BadCommandSequence = 503,
		/// <summary>The specified user is not local, but the receiving SMTP service accepted the message and attempted to deliver it. This status code is defined in RFC 1123, which is available at http://www.ietf.org.</summary>
		// Token: 0x040024C7 RID: 9415
		CannotVerifyUserWillAttemptDelivery = 252,
		/// <summary>The client was not authenticated or is not allowed to send mail using the specified SMTP host.</summary>
		// Token: 0x040024C8 RID: 9416
		ClientNotPermitted = 454,
		/// <summary>The SMTP service does not implement the specified command.</summary>
		// Token: 0x040024C9 RID: 9417
		CommandNotImplemented = 502,
		/// <summary>The SMTP service does not implement the specified command parameter.</summary>
		// Token: 0x040024CA RID: 9418
		CommandParameterNotImplemented = 504,
		/// <summary>The SMTP service does not recognize the specified command.</summary>
		// Token: 0x040024CB RID: 9419
		CommandUnrecognized = 500,
		/// <summary>The message is too large to be stored in the destination mailbox.</summary>
		// Token: 0x040024CC RID: 9420
		ExceededStorageAllocation = 552,
		/// <summary>The transaction could not occur. You receive this error when the specified SMTP host cannot be found.</summary>
		// Token: 0x040024CD RID: 9421
		GeneralFailure = -1,
		/// <summary>A Help message was returned by the service.</summary>
		// Token: 0x040024CE RID: 9422
		HelpMessage = 214,
		/// <summary>The SMTP service does not have sufficient storage to complete the request.</summary>
		// Token: 0x040024CF RID: 9423
		InsufficientStorage = 452,
		/// <summary>The SMTP service cannot complete the request. This error can occur if the client's IP address cannot be resolved (that is, a reverse lookup failed). You can also receive this error if the client domain has been identified as an open relay or source for unsolicited e-mail (spam). For details, see RFC 2505, which is available at http://www.ietf.org.</summary>
		// Token: 0x040024D0 RID: 9424
		LocalErrorInProcessing = 451,
		/// <summary>The destination mailbox is in use.</summary>
		// Token: 0x040024D1 RID: 9425
		MailboxBusy = 450,
		/// <summary>The syntax used to specify the destination mailbox is incorrect.</summary>
		// Token: 0x040024D2 RID: 9426
		MailboxNameNotAllowed = 553,
		/// <summary>The destination mailbox was not found or could not be accessed.</summary>
		// Token: 0x040024D3 RID: 9427
		MailboxUnavailable = 550,
		/// <summary>The email was successfully sent to the SMTP service.</summary>
		// Token: 0x040024D4 RID: 9428
		Ok = 250,
		/// <summary>The SMTP service is closing the transmission channel.</summary>
		// Token: 0x040024D5 RID: 9429
		ServiceClosingTransmissionChannel = 221,
		/// <summary>The SMTP service is not available; the server is closing the transmission channel.</summary>
		// Token: 0x040024D6 RID: 9430
		ServiceNotAvailable = 421,
		/// <summary>The SMTP service is ready.</summary>
		// Token: 0x040024D7 RID: 9431
		ServiceReady = 220,
		/// <summary>The SMTP service is ready to receive the e-mail content.</summary>
		// Token: 0x040024D8 RID: 9432
		StartMailInput = 354,
		/// <summary>The syntax used to specify a command or parameter is incorrect.</summary>
		// Token: 0x040024D9 RID: 9433
		SyntaxError = 501,
		/// <summary>A system status or system Help reply.</summary>
		// Token: 0x040024DA RID: 9434
		SystemStatus = 211,
		/// <summary>The transaction failed.</summary>
		// Token: 0x040024DB RID: 9435
		TransactionFailed = 554,
		/// <summary>The user mailbox is not located on the receiving server. You should resend using the supplied address information.</summary>
		// Token: 0x040024DC RID: 9436
		UserNotLocalTryAlternatePath = 551,
		/// <summary>The user mailbox is not located on the receiving server; the server forwards the e-mail.</summary>
		// Token: 0x040024DD RID: 9437
		UserNotLocalWillForward = 251,
		/// <summary>The SMTP server is configured to accept only TLS connections, and the SMTP client is attempting to connect by using a non-TLS connection. The solution is for the user to set EnableSsl=true on the SMTP Client.</summary>
		// Token: 0x040024DE RID: 9438
		MustIssueStartTlsFirst = 530
	}
}
