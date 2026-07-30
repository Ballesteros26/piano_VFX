using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies constants that define the state of the link.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000210 RID: 528
	public enum LinkState
	{
		/// <summary>The state of a link in its normal state (none of the other states apply).</summary>
		// Token: 0x04001199 RID: 4505
		Normal,
		/// <summary>The state of a link over which a mouse pointer is resting.</summary>
		// Token: 0x0400119A RID: 4506
		Hover,
		/// <summary>The state of a link that has been clicked.</summary>
		// Token: 0x0400119B RID: 4507
		Active,
		/// <summary>The state of a link that has been visited.</summary>
		// Token: 0x0400119C RID: 4508
		Visited = 4
	}
}
