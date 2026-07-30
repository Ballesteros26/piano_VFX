using System;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Represents an enumeration of the data types associated with data fields and parameters. </summary>
	// Token: 0x0200000F RID: 15
	public enum DataType
	{
		/// <summary>Represents a custom data type.</summary>
		// Token: 0x04000048 RID: 72
		Custom,
		/// <summary>Represents an instant in time, expressed as a date and time of day.</summary>
		// Token: 0x04000049 RID: 73
		DateTime,
		/// <summary>Represents a date value.</summary>
		// Token: 0x0400004A RID: 74
		Date,
		/// <summary>Represents a time value.</summary>
		// Token: 0x0400004B RID: 75
		Time,
		/// <summary>Represents a continuous time during which an object exists.</summary>
		// Token: 0x0400004C RID: 76
		Duration,
		/// <summary>Represents a phone number value.</summary>
		// Token: 0x0400004D RID: 77
		PhoneNumber,
		/// <summary>Represents a currency value.</summary>
		// Token: 0x0400004E RID: 78
		Currency,
		/// <summary>Represents text that is displayed.</summary>
		// Token: 0x0400004F RID: 79
		Text,
		/// <summary>Represents an HTML file.</summary>
		// Token: 0x04000050 RID: 80
		Html,
		/// <summary>Represents multi-line text.</summary>
		// Token: 0x04000051 RID: 81
		MultilineText,
		/// <summary>Represents an e-mail address.</summary>
		// Token: 0x04000052 RID: 82
		EmailAddress,
		/// <summary>Represent a password value.</summary>
		// Token: 0x04000053 RID: 83
		Password,
		/// <summary>Represents a URL value.</summary>
		// Token: 0x04000054 RID: 84
		Url,
		/// <summary>Represents a URL to an image.</summary>
		// Token: 0x04000055 RID: 85
		ImageUrl,
		/// <summary>Represents a credit card number.</summary>
		// Token: 0x04000056 RID: 86
		CreditCard,
		/// <summary>Represents a postal code.</summary>
		// Token: 0x04000057 RID: 87
		PostalCode,
		/// <summary>Represents file upload data type.</summary>
		// Token: 0x04000058 RID: 88
		Upload
	}
}
