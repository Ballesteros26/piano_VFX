using System;
using System.ComponentModel.Design;
using System.Drawing;

namespace System.Web.UI.Design
{
	/// <summary>Provides an interface for access to the visual representation and content of a control at design time.</summary>
	// Token: 0x02000085 RID: 133
	public interface IControlDesignerView
	{
		/// <summary>An event raised by the design host for the view and designer component.</summary>
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000433 RID: 1075
		// (remove) Token: 0x06000434 RID: 1076
		event ViewEventHandler ViewEvent;

		/// <summary>Gets the designer region that contains the associated control, if any.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.DesignerRegion" /> object if the associated control is contained in a designer region; otherwise null.</returns>
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000435 RID: 1077
		DesignerRegion ContainingRegion { get; }

		/// <summary>Gets the designer component for the naming container of the associated control, if any.</summary>
		/// <returns>An IDesigner object representing the designer component for the naming container for the associated control; otherwise null.</returns>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000436 RID: 1078
		IDesigner NamingContainerDesigner { get; }

		/// <summary>Gets a value indicating whether designer regions are supported.</summary>
		/// <returns>true if designer regions are supported; otherwise false.</returns>
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000437 RID: 1079
		bool SupportsRegions { get; }

		/// <summary>Retrieves the outer bounds of the designer view.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> containing information about the location and measurements of the view at design time.</returns>
		/// <param name="region">The <see cref="T:System.Web.UI.Design.DesignerRegion" /> for which you want to retrieve the bounds.</param>
		// Token: 0x06000438 RID: 1080
		Rectangle GetBounds(DesignerRegion region);

		/// <summary>Notifies the design host that the area represented by the provided rectangle needs to be repainted on the design surface.</summary>
		/// <param name="rectangle">A <see cref="T:System.Drawing.Rectangle" /> representing the location and outer measurements of the view on the design surface. The coordinate-system origin for this rectangle is the top-left corner of the element to which the behavior is attached.</param>
		// Token: 0x06000439 RID: 1081
		void Invalidate(Rectangle rectangle);

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IControlDesignerView.SetFlags(System.Web.UI.Design.ViewFlags,System.Boolean)" />.</summary>
		/// <param name="viewFlags">A member of the <see cref="T:System.Web.UI.Design.ViewFlags" /> enumeration.</param>
		/// <param name="setFlag">true to set the flag, false to cancel the flag.</param>
		// Token: 0x0600043A RID: 1082
		void SetFlags(ViewFlags viewFlags, bool setFlag);

		/// <summary>Puts the provided content into the provided designer region.</summary>
		/// <param name="region">A <see cref="T:System.Web.UI.Design.DesignerRegion" /> into which the content is to be put.</param>
		/// <param name="content">The HTML markup to be put into the designer region.</param>
		// Token: 0x0600043B RID: 1083
		void SetRegionContent(EditableDesignerRegion region, string content);

		/// <summary>Causes the associated control to redraw the invalidated regions within its client area.</summary>
		// Token: 0x0600043C RID: 1084
		void Update();
	}
}
