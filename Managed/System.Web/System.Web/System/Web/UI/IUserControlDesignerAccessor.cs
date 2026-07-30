using System;

namespace System.Web.UI
{
	/// <summary>Defines the properties that allow the designer to access information about a user control at design time.</summary>
	// Token: 0x02000188 RID: 392
	public interface IUserControlDesignerAccessor
	{
		/// <summary>When implemented, gets or sets text between the opening and closing tags of a user control.</summary>
		/// <returns>The text placed between the opening and closing tags of a user control.</returns>
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06000F9C RID: 3996
		// (set) Token: 0x06000F9D RID: 3997
		string InnerText { get; set; }

		/// <summary>When implemented, gets or sets the full tag name of the user control.</summary>
		/// <returns>The full tag name of the user control.</returns>
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06000F9E RID: 3998
		// (set) Token: 0x06000F9F RID: 3999
		string TagName { get; set; }
	}
}
