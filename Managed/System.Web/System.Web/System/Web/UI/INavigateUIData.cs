using System;

namespace System.Web.UI
{
	/// <summary>Provides an interface that classes implement to provide navigation user interface data and values to navigation controls. </summary>
	// Token: 0x02000179 RID: 377
	public interface INavigateUIData
	{
		/// <summary>Gets text that represents the description of a navigation node of a navigation control.</summary>
		/// <returns>Text that is the description of a node of a navigation control; otherwise, null.</returns>
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06000F7C RID: 3964
		string Description { get; }

		/// <summary>Gets the text that represents the name of a navigation node of a navigation control.</summary>
		/// <returns>Text that represents the name of a node of a navigation control; otherwise, null.</returns>
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06000F7D RID: 3965
		string Name { get; }

		/// <summary>Gets the URL to navigate to when the navigation node is clicked.</summary>
		/// <returns>The URL to navigate to when the node is clicked; otherwise, null.</returns>
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06000F7E RID: 3966
		string NavigateUrl { get; }

		/// <summary>Gets a non-displayed value that is used to store any additional data about the navigation node.</summary>
		/// <returns>A value that is not displayed and is used to store additional data about the navigation node; otherwise, null.</returns>
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06000F7F RID: 3967
		string Value { get; }
	}
}
