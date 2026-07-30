using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines properties and events that must be implemented to allow a control to act like a button on a Web page.</summary>
	// Token: 0x020002CF RID: 719
	public interface IButtonControl
	{
		/// <summary>Gets or sets a value indicating whether clicking the button causes page validation to occur.</summary>
		/// <returns>true if clicking the button causes page validation to occur; otherwise, false.</returns>
		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001B50 RID: 6992
		// (set) Token: 0x06001B51 RID: 6993
		bool CausesValidation { get; set; }

		/// <summary>Gets or sets an optional argument that is propagated to the <see cref="E:System.Web.UI.WebControls.IButtonControl.Command" /> event.</summary>
		/// <returns>The argument that is propagated to the <see cref="E:System.Web.UI.WebControls.IButtonControl.Command" /> event.</returns>
		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001B52 RID: 6994
		// (set) Token: 0x06001B53 RID: 6995
		string CommandArgument { get; set; }

		/// <summary>Gets or sets the command name that is propagated to the <see cref="E:System.Web.UI.WebControls.IButtonControl.Command" /> event.</summary>
		/// <returns>The name of the command that is propagated to the <see cref="E:System.Web.UI.WebControls.IButtonControl.Command" /> event.</returns>
		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001B54 RID: 6996
		// (set) Token: 0x06001B55 RID: 6997
		string CommandName { get; set; }

		/// <summary>Occurs when the button control is clicked.</summary>
		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06001B56 RID: 6998
		// (remove) Token: 0x06001B57 RID: 6999
		event EventHandler Click;

		/// <summary>Occurs when the button control is clicked.</summary>
		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06001B58 RID: 7000
		// (remove) Token: 0x06001B59 RID: 7001
		event CommandEventHandler Command;

		/// <summary>Gets or sets the URL of the Web page to post to from the current page when the button control is clicked.</summary>
		/// <returns>The URL of the Web page to post to from the current page when the button control is clicked.</returns>
		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06001B5A RID: 7002
		// (set) Token: 0x06001B5B RID: 7003
		string PostBackUrl { get; set; }

		/// <summary>Gets or sets the text caption displayed for the button.</summary>
		/// <returns>The text caption displayed for the button.</returns>
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06001B5C RID: 7004
		// (set) Token: 0x06001B5D RID: 7005
		string Text { get; set; }

		/// <summary>Gets or sets the name for the group of controls for which the button control causes validation when it posts back to the server.</summary>
		/// <returns>The name for the group of controls for which the button control causes validation when it posts back to the server.</returns>
		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06001B5E RID: 7006
		// (set) Token: 0x06001B5F RID: 7007
		string ValidationGroup { get; set; }
	}
}
