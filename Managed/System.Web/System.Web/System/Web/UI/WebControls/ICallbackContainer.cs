using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines a method that enables controls to obtain a callback script.</summary>
	// Token: 0x020002D0 RID: 720
	public interface ICallbackContainer
	{
		/// <summary>Creates a script for initiating a client callback to a Web server.</summary>
		/// <returns>A script that, when run on a client, will initiate a callback to the Web server.</returns>
		/// <param name="buttonControl">The control initiating the callback request.</param>
		/// <param name="argument">The arguments used to build the callback script.</param>
		// Token: 0x06001B60 RID: 7008
		string GetCallbackScript(IButtonControl buttonControl, string argument);
	}
}
