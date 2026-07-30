using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the type of an object.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200023D RID: 573
	[ComVisible(true)]
	[Serializable]
	public enum TypeCode
	{
		/// <summary>A null reference.</summary>
		// Token: 0x04000F40 RID: 3904
		Empty,
		/// <summary>A general type representing any reference or value type not explicitly represented by another TypeCode.</summary>
		// Token: 0x04000F41 RID: 3905
		Object,
		/// <summary>A database null (column) value.</summary>
		// Token: 0x04000F42 RID: 3906
		DBNull,
		/// <summary>A simple type representing Boolean values of true or false.</summary>
		// Token: 0x04000F43 RID: 3907
		Boolean,
		/// <summary>An integral type representing unsigned 16-bit integers with values between 0 and 65535. The set of possible values for the <see cref="F:System.TypeCode.Char" /> type corresponds to the Unicode character set.</summary>
		// Token: 0x04000F44 RID: 3908
		Char,
		/// <summary>An integral type representing signed 8-bit integers with values between -128 and 127.</summary>
		// Token: 0x04000F45 RID: 3909
		SByte,
		/// <summary>An integral type representing unsigned 8-bit integers with values between 0 and 255.</summary>
		// Token: 0x04000F46 RID: 3910
		Byte,
		/// <summary>An integral type representing signed 16-bit integers with values between -32768 and 32767.</summary>
		// Token: 0x04000F47 RID: 3911
		Int16,
		/// <summary>An integral type representing unsigned 16-bit integers with values between 0 and 65535.</summary>
		// Token: 0x04000F48 RID: 3912
		UInt16,
		/// <summary>An integral type representing signed 32-bit integers with values between -2147483648 and 2147483647.</summary>
		// Token: 0x04000F49 RID: 3913
		Int32,
		/// <summary>An integral type representing unsigned 32-bit integers with values between 0 and 4294967295.</summary>
		// Token: 0x04000F4A RID: 3914
		UInt32,
		/// <summary>An integral type representing signed 64-bit integers with values between -9223372036854775808 and 9223372036854775807.</summary>
		// Token: 0x04000F4B RID: 3915
		Int64,
		/// <summary>An integral type representing unsigned 64-bit integers with values between 0 and 18446744073709551615.</summary>
		// Token: 0x04000F4C RID: 3916
		UInt64,
		/// <summary>A floating point type representing values ranging from approximately 1.5 x 10 -45 to 3.4 x 10 38 with a precision of 7 digits.</summary>
		// Token: 0x04000F4D RID: 3917
		Single,
		/// <summary>A floating point type representing values ranging from approximately 5.0 x 10 -324 to 1.7 x 10 308 with a precision of 15-16 digits.</summary>
		// Token: 0x04000F4E RID: 3918
		Double,
		/// <summary>A simple type representing values ranging from 1.0 x 10 -28 to approximately 7.9 x 10 28 with 28-29 significant digits.</summary>
		// Token: 0x04000F4F RID: 3919
		Decimal,
		/// <summary>A type representing a date and time value.</summary>
		// Token: 0x04000F50 RID: 3920
		DateTime,
		/// <summary>A sealed class type representing Unicode character strings.</summary>
		// Token: 0x04000F51 RID: 3921
		String = 18
	}
}
