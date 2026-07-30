using System;

namespace System.Web.Configuration
{
	/// <summary>Defines whether to use Coordinated Universal Time (UTC) or local time for the ticket expiration date for forms authentication.</summary>
	// Token: 0x0200057A RID: 1402
	public enum TicketCompatibilityMode
	{
		/// <summary>Specifies that the ticket expiration date is stored as local time. This is the default value.</summary>
		// Token: 0x0400206E RID: 8302
		Framework20,
		/// <summary>Specifies that the ticket expiration date is stored as UTC.</summary>
		// Token: 0x0400206F RID: 8303
		Framework40
	}
}
