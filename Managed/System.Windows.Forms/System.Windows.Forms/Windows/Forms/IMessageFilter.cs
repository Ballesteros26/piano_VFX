using System;

namespace System.Windows.Forms
{
	/// <summary>Defines a message filter interface.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001D1 RID: 465
	public interface IMessageFilter
	{
		/// <summary>Filters out a message before it is dispatched.</summary>
		/// <returns>true to filter the message and stop it from being dispatched; false to allow the message to continue to the next filter or control.</returns>
		/// <param name="m">The message to be dispatched. You cannot modify this message. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E01 RID: 7681
		bool PreFilterMessage(ref Message m);
	}
}
