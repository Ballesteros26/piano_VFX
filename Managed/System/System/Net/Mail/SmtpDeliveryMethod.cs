using System;

namespace System.Net.Mail
{
	/// <summary>Specifies how email messages are delivered.</summary>
	// Token: 0x0200058D RID: 1421
	public enum SmtpDeliveryMethod
	{
		/// <summary>Email is sent through the network to an SMTP server.</summary>
		// Token: 0x040024BB RID: 9403
		Network,
		/// <summary>Email is copied to the directory specified by the <see cref="P:System.Net.Mail.SmtpClient.PickupDirectoryLocation" /> property for delivery by an external application.</summary>
		// Token: 0x040024BC RID: 9404
		SpecifiedPickupDirectory,
		/// <summary>Email is copied to the pickup directory used by a local Internet Information Services (IIS) for delivery.</summary>
		// Token: 0x040024BD RID: 9405
		PickupDirectoryFromIis
	}
}
