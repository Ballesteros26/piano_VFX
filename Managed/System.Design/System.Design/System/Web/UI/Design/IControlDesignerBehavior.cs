using System;

namespace System.Web.UI.Design
{
	/// <summary>Enables the extension of specific behaviors of a control designer.</summary>
	// Token: 0x02000083 RID: 131
	[Obsolete("Use IControlDesignerTag interface instead")]
	public interface IControlDesignerBehavior
	{
		/// <summary>Provides an opportunity to perform processing when the designer enters or exits template mode.</summary>
		// Token: 0x06000427 RID: 1063
		void OnTemplateModeChanged();

		/// <summary>Gets the design-time view control object for the designer.</summary>
		/// <returns>The view control object for the designer.</returns>
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000428 RID: 1064
		object DesignTimeElementView { get; }

		/// <summary>Gets or sets the design-time HTML for the designer's control.</summary>
		/// <returns>The HTML used at design time to format the control.</returns>
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000429 RID: 1065
		// (set) Token: 0x0600042A RID: 1066
		string DesignTimeHtml { get; set; }
	}
}
