using System;

namespace System
{
	/// <summary>Defines the parts of a URI for the <see cref="M:System.Uri.GetLeftPart(System.UriPartial)" /> method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FD RID: 253
	public enum UriPartial
	{
		/// <summary>The scheme segment of the URI.</summary>
		// Token: 0x04000CB7 RID: 3255
		Scheme,
		/// <summary>The scheme and authority segments of the URI.</summary>
		// Token: 0x04000CB8 RID: 3256
		Authority,
		/// <summary>The scheme, authority, and path segments of the URI.</summary>
		// Token: 0x04000CB9 RID: 3257
		Path,
		/// <summary>The scheme, authority, path, and query segments of the URI.</summary>
		// Token: 0x04000CBA RID: 3258
		Query
	}
}
