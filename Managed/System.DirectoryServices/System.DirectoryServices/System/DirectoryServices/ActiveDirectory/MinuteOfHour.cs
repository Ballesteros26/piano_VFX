using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Indicates the 15-minute intervals within an hour.</summary>
	// Token: 0x02000066 RID: 102
	public enum MinuteOfHour
	{
		/// <summary>Represents 0 to 14 minutes after the hour.</summary>
		// Token: 0x04000129 RID: 297
		Zero,
		/// <summary>Represents 15 to 29 minutes after the hour.</summary>
		// Token: 0x0400012A RID: 298
		Fifteen = 15,
		/// <summary>Represents 30 to 44 minutes after the hour.</summary>
		// Token: 0x0400012B RID: 299
		Thirty = 30,
		/// <summary>Represents 45 to 59 minutes after the hour.</summary>
		// Token: 0x0400012C RID: 300
		FortyFive = 45
	}
}
