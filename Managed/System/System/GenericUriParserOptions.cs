using System;

namespace System
{
	/// <summary>Specifies options for a <see cref="T:System.UriParser" />.</summary>
	// Token: 0x020000F3 RID: 243
	[Flags]
	public enum GenericUriParserOptions
	{
		/// <summary>The parser:</summary>
		// Token: 0x04000C2F RID: 3119
		Default = 0,
		/// <summary>The parser allows a registry-based authority.</summary>
		// Token: 0x04000C30 RID: 3120
		GenericAuthority = 1,
		/// <summary>The parser allows a URI with no authority.</summary>
		// Token: 0x04000C31 RID: 3121
		AllowEmptyAuthority = 2,
		/// <summary>The scheme does not define a user information part.</summary>
		// Token: 0x04000C32 RID: 3122
		NoUserInfo = 4,
		/// <summary>The scheme does not define a port.</summary>
		// Token: 0x04000C33 RID: 3123
		NoPort = 8,
		/// <summary>The scheme does not define a query part.</summary>
		// Token: 0x04000C34 RID: 3124
		NoQuery = 16,
		/// <summary>The scheme does not define a fragment part.</summary>
		// Token: 0x04000C35 RID: 3125
		NoFragment = 32,
		/// <summary>The parser does not convert back slashes into forward slashes.</summary>
		// Token: 0x04000C36 RID: 3126
		DontConvertPathBackslashes = 64,
		/// <summary>The parser does not canonicalize the URI.</summary>
		// Token: 0x04000C37 RID: 3127
		DontCompressPath = 128,
		/// <summary>The parser does not unescape path dots, forward slashes, or back slashes.</summary>
		// Token: 0x04000C38 RID: 3128
		DontUnescapePathDotsAndSlashes = 256,
		/// <summary>The parser supports Internationalized Domain Name (IDN) parsing (IDN) of host names. Whether IDN is used is dictated by configuration values. See the Remarks for more information.</summary>
		// Token: 0x04000C39 RID: 3129
		Idn = 512,
		/// <summary>The parser supports the parsing rules specified in RFC 3987 for International Resource Identifiers (IRI). Whether IRI is used is dictated by configuration values. See the Remarks for more information.</summary>
		// Token: 0x04000C3A RID: 3130
		IriParsing = 1024
	}
}
