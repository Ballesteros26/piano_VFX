using System;

namespace System.DirectoryServices.ActiveDirectory
{
	/// <summary>Specifies the data representation (syntax) type of a <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object.</summary>
	// Token: 0x02000048 RID: 72
	public enum ActiveDirectorySyntax
	{
		/// <summary>A case-sensitive string type.</summary>
		// Token: 0x040000C6 RID: 198
		CaseExactString,
		/// <summary>A case-insensitive string type.</summary>
		// Token: 0x040000C7 RID: 199
		CaseIgnoreString,
		/// <summary>A numeric value represented as a string.</summary>
		// Token: 0x040000C8 RID: 200
		NumericString,
		/// <summary>A directory string specification.</summary>
		// Token: 0x040000C9 RID: 201
		DirectoryString,
		/// <summary>A byte array represented as a string.</summary>
		// Token: 0x040000CA RID: 202
		OctetString,
		/// <summary>A security descriptor value type.</summary>
		// Token: 0x040000CB RID: 203
		SecurityDescriptor,
		/// <summary>A 32-bit integer value type.</summary>
		// Token: 0x040000CC RID: 204
		Int,
		/// <summary>A 64 bit (large) integer value type.</summary>
		// Token: 0x040000CD RID: 205
		Int64,
		/// <summary>A Boolean value type.</summary>
		// Token: 0x040000CE RID: 206
		Bool,
		/// <summary>An OID value type.</summary>
		// Token: 0x040000CF RID: 207
		Oid,
		/// <summary>A time expressed in generalized time format.</summary>
		// Token: 0x040000D0 RID: 208
		GeneralizedTime,
		/// <summary>A time expressed in Coordinated Universal Time format.</summary>
		// Token: 0x040000D1 RID: 209
		UtcTime,
		/// <summary>A distinguished name of a directory service object.</summary>
		// Token: 0x040000D2 RID: 210
		DN,
		/// <summary>An ADS_DN_WITH_BINARY structure used for mapping a distinguished name to a non-varying GUID. For more information, see the topic "ADS_DN_WITH_BINARY" in the MSDN Library at http://msdn.microsoft.com/library.</summary>
		// Token: 0x040000D3 RID: 211
		DNWithBinary,
		/// <summary>An ADS_DN_WITH_STRING structure used for mapping a distinguished name to a non-varying string value. For more information, see the topic "ADS_DN_WITH_STRING" in the MSDN Library at http://msdn.microsoft.com/library</summary>
		// Token: 0x040000D4 RID: 212
		DNWithString,
		/// <summary>An enumeration value type.</summary>
		// Token: 0x040000D5 RID: 213
		Enumeration,
		/// <summary>An IA5 character set string.</summary>
		// Token: 0x040000D6 RID: 214
		IA5String,
		/// <summary>A printable character set string.</summary>
		// Token: 0x040000D7 RID: 215
		PrintableString,
		/// <summary>An SID value type.</summary>
		// Token: 0x040000D8 RID: 216
		Sid,
		/// <summary>An AccessPoint object type.</summary>
		// Token: 0x040000D9 RID: 217
		AccessPointDN,
		/// <summary>An OR-Name object type.</summary>
		// Token: 0x040000DA RID: 218
		ORName,
		/// <summary>A Presentation-Address object type.</summary>
		// Token: 0x040000DB RID: 219
		PresentationAddress,
		/// <summary>A Replica-Link object type.</summary>
		// Token: 0x040000DC RID: 220
		ReplicaLink
	}
}
