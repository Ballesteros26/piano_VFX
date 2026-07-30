using System;

namespace System.Web.UI.Design
{
	/// <summary>Defines an interface that enables the extension of specific behaviors of an HTML control designer.</summary>
	// Token: 0x02000091 RID: 145
	[Obsolete("Use IControlDesignerTag and IControlDesignerView instead")]
	public interface IHtmlControlDesignerBehavior
	{
		/// <summary>Gets the specified attribute.</summary>
		/// <returns>The attribute that was retrieved.</returns>
		/// <param name="attribute">The attribute to retrieve. </param>
		/// <param name="ignoreCase">true if the attribute syntax is case-insensitive; otherwise, false. </param>
		// Token: 0x0600046E RID: 1134
		object GetAttribute(string attribute, bool ignoreCase);

		/// <summary>Gets the specified style attribute.</summary>
		/// <returns>The style attribute that was retrieved.</returns>
		/// <param name="attribute">The style attribute to retrieve. </param>
		/// <param name="designTimeOnly">true if the attribute is only active at design time; otherwise, false. </param>
		/// <param name="ignoreCase">true if the attribute syntax is case-insensitive; otherwise, false. </param>
		// Token: 0x0600046F RID: 1135
		object GetStyleAttribute(string attribute, bool designTimeOnly, bool ignoreCase);

		/// <summary>Removes the specified attribute.</summary>
		/// <param name="attribute">The attribute to remove. </param>
		/// <param name="ignoreCase">true if the attribute syntax is case-insensitive; otherwise, false. </param>
		// Token: 0x06000470 RID: 1136
		void RemoveAttribute(string attribute, bool ignoreCase);

		/// <summary>Removes the specified style attribute.</summary>
		/// <param name="attribute">The style attribute to remove. </param>
		/// <param name="designTimeOnly">true if the attribute is only active at design time; otherwise, false. </param>
		/// <param name="ignoreCase">true if the attribute syntax is case-insensitive; otherwise, false. </param>
		// Token: 0x06000471 RID: 1137
		void RemoveStyleAttribute(string attribute, bool designTimeOnly, bool ignoreCase);

		/// <summary>Sets the specified attribute to the specified object.</summary>
		/// <param name="attribute">The attribute to set. </param>
		/// <param name="value">The object on which to set the attribute. </param>
		/// <param name="ignoreCase">true if the attribute syntax is case-insensitive; otherwise, false. </param>
		// Token: 0x06000472 RID: 1138
		void SetAttribute(string attribute, object value, bool ignoreCase);

		/// <summary>Sets the specified style attribute to the specified object.</summary>
		/// <param name="attribute">The attribute to set. </param>
		/// <param name="designTimeOnly">true if the attribute is only active at design-time; otherwise, false. </param>
		/// <param name="value">The object to set the attribute on. </param>
		/// <param name="ignoreCase">true if the attribute syntax is case-insensitive; otherwise, false. </param>
		// Token: 0x06000473 RID: 1139
		void SetStyleAttribute(string attribute, bool designTimeOnly, object value, bool ignoreCase);

		/// <summary>Gets or sets the designer that the behavior is associated with.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Design.HtmlControlDesigner" /> that the behavior is associated with.NoteThe <see cref="T:System.Web.UI.Design.IHtmlControlDesignerBehavior" /> interface is obsolete. Use the <see cref="T:System.Web.UI.Design.IControlDesignerTag" /> and <see cref="T:System.Web.UI.Design.IControlDesignerView" /> interfaces for equivalent control designer functionality.</returns>
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000474 RID: 1140
		// (set) Token: 0x06000475 RID: 1141
		HtmlControlDesigner Designer { get; set; }

		/// <summary>Gets the element that the designer is associated with.</summary>
		/// <returns>The object that the designer is associated with.NoteThe <see cref="T:System.Web.UI.Design.IHtmlControlDesignerBehavior" /> interface is obsolete. Use the <see cref="T:System.Web.UI.Design.IControlDesignerTag" /> and <see cref="T:System.Web.UI.Design.IControlDesignerView" /> interfaces for equivalent control designer functionality.</returns>
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000476 RID: 1142
		object DesignTimeElement { get; }
	}
}
