using System;

namespace System
{
	/// <summary>Defines the kinds of <see cref="T:System.Uri" />s for the <see cref="M:System.Uri.IsWellFormedUriString(System.String,System.UriKind)" /> and several <see cref="Overload:System.Uri.#ctor" /> methods.</summary>
	// Token: 0x020000FE RID: 254
	public enum UriKind
	{
		/// <summary>The kind of the Uri is indeterminate.</summary>
		// Token: 0x04000CBC RID: 3260
		RelativeOrAbsolute,
		/// <summary>The Uri is an absolute Uri.</summary>
		// Token: 0x04000CBD RID: 3261
		Absolute,
		/// <summary>The Uri is a relative Uri.</summary>
		// Token: 0x04000CBE RID: 3262
		Relative
	}
}
