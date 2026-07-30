using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines a method that enables controls to obtain client-side script options.</summary>
	// Token: 0x020002D7 RID: 727
	public interface IPostBackContainer
	{
		/// <summary>Returns the options required for a postback script for a specified button control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.PostBackOptions" /> object containing the options required to generate a postback script for <paramref name="buttonControl" />.</returns>
		/// <param name="buttonControl">The control generating the postback event.</param>
		// Token: 0x06001B79 RID: 7033
		PostBackOptions GetPostBackOptions(IButtonControl buttonControl);
	}
}
