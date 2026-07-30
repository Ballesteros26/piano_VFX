using System;
using Unity;

namespace System.Web.UI.Design
{
	/// <summary>Contains the design-time markup for content and regions.</summary>
	// Token: 0x020000B7 RID: 183
	public class ViewRendering
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.ViewRendering" /> class by using the specified content and regions.</summary>
		/// <param name="content">HTML markup.</param>
		/// <param name="regions">A collection that contains the regions.</param>
		// Token: 0x0600054C RID: 1356 RVA: 0x00002364 File Offset: 0x00000564
		[MonoNotSupported("")]
		public ViewRendering(string content, DesignerRegionCollection regions)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the design-time HTML markup.</summary>
		/// <returns>The HTML markup to display at design time.</returns>
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public string Content
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.Design.DesignerRegion" /> objects at design time.</summary>
		/// <returns>A collection of regions.</returns>
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoNotSupported("")]
		public DesignerRegionCollection Regions
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.ViewRendering" /> class by using the specified content, regions, and visibility mode.</summary>
		/// <param name="content">The HTML markup.</param>
		/// <param name="regions">A collection of regions</param>
		/// <param name="visible">A value that indicates whether the control is rendered.</param>
		// Token: 0x0600054F RID: 1359 RVA: 0x00009519 File Offset: 0x00007719
		public ViewRendering(string content, DesignerRegionCollection regions, bool visible)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a value that indicates whether the control is visible.</summary>
		/// <returns>true if the control is rendered; otherwise, false. The default is true.</returns>
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00009520 File Offset: 0x00007720
		public bool Visible
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}
	}
}
