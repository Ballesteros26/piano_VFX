using System;
using System.Drawing;

namespace System.Web.UI.Design
{
	/// <summary>Provides data for a <see cref="E:System.Web.UI.Design.IControlDesignerView.ViewEvent" /> event that is raised when you click on a selected control or a designer region in a selected control. This class cannot be inherited.</summary>
	// Token: 0x02000077 RID: 119
	public sealed class DesignerRegionMouseEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DesignerRegionMouseEventArgs" /> class with the specified region and location. </summary>
		/// <param name="region">The designer region that was clicked; used to initialize the <see cref="P:System.Web.UI.Design.DesignerRegionMouseEventArgs.Region" />.</param>
		/// <param name="location">The location that was clicked, relative to the upper left corner of the region; used to initialize the <see cref="P:System.Web.UI.Design.DesignerRegionMouseEventArgs.Location" />.</param>
		// Token: 0x060003E2 RID: 994 RVA: 0x00009049 File Offset: 0x00007249
		[MonoNotSupported("")]
		public DesignerRegionMouseEventArgs(DesignerRegion region, Point location)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the location within the control that was clicked.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> identifying the location within the region that was clicked.</returns>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public Point Location
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the designer region that was clicked, if any.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Design.DesignerRegion" /> that the click event applies to, or null if no region was clicked.</returns>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public DesignerRegion Region
		{
			[MonoNotSupported("")]
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
