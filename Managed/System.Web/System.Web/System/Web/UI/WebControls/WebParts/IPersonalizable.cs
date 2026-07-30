using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines additional management capabilities for the application and extraction of personalization state. </summary>
	// Token: 0x02000485 RID: 1157
	public interface IPersonalizable
	{
		/// <summary>Gets a value that indicates whether the custom data that a control manages has changed. </summary>
		/// <returns>true if the custom data managed with the <see cref="T:System.Web.UI.WebControls.WebParts.IPersonalizable" /> interface has changed; otherwise, false.</returns>
		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x0600346F RID: 13423
		bool IsDirty { get; }

		/// <summary>Loads custom data into a control. </summary>
		/// <param name="state">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> that contains custom scoped data that was loaded from the underlying data store.</param>
		// Token: 0x06003470 RID: 13424
		void Load(PersonalizationDictionary state);

		/// <summary>Saves custom properties and internal state information in the control's <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> object.</summary>
		/// <param name="state">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> that contains custom scoped data that was loaded from the underlying data store.</param>
		// Token: 0x06003471 RID: 13425
		void Save(PersonalizationDictionary state);
	}
}
