using System;

namespace System.Windows.Forms
{
	/// <summary>Defines values that describe where to insert a new element when using <see cref="M:System.Windows.Forms.HtmlElement.InsertAdjacentElement(System.Windows.Forms.HtmlElementInsertionOrientation,System.Windows.Forms.HtmlElement)" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001BB RID: 443
	public enum HtmlElementInsertionOrientation
	{
		/// <summary>Insert the element before the current element.</summary>
		// Token: 0x04000F80 RID: 3968
		BeforeBegin,
		/// <summary>Insert the element after the current element, but before all other content in the current element.</summary>
		// Token: 0x04000F81 RID: 3969
		AfterBegin,
		/// <summary>Insert the element after the current element.</summary>
		// Token: 0x04000F82 RID: 3970
		BeforeEnd,
		/// <summary>Insert the element after the current element, but after all other content in the current element.</summary>
		// Token: 0x04000F83 RID: 3971
		AfterEnd
	}
}
