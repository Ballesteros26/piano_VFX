using System;

namespace System
{
	/// <summary>Controls how URI information is escaped.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000100 RID: 256
	public enum UriFormat
	{
		/// <summary>Escaping is performed according to the rules in RFC 2396.</summary>
		// Token: 0x04000CD2 RID: 3282
		UriEscaped = 1,
		/// <summary>No escaping is performed.</summary>
		// Token: 0x04000CD3 RID: 3283
		Unescaped,
		/// <summary>Characters that have a reserved meaning in the requested URI components remain escaped. All others are not escaped. See Remarks.</summary>
		// Token: 0x04000CD4 RID: 3284
		SafeUnescaped
	}
}
