using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartAdding" /> event. </summary>
	// Token: 0x020006CF RID: 1743
	public class WebPartAddingEventArgs : WebPartCancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartAddingEventArgs" /> class. </summary>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> (or server or user control) to be added to a Web page or opened on a page.</param>
		/// <param name="zone">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" />  that <paramref name="webPart" /> is being added to.</param>
		/// <param name="zoneIndex">An integer that represents the ordinal position that <paramref name="webPart" /> occupies in <paramref name="zone" />, relative to other controls in <paramref name="zone" />.</param>
		// Token: 0x06004A14 RID: 18964 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebPartAddingEventArgs(WebPart webPart, WebPartZoneBase zone, int zoneIndex)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the Web Parts zone that the Web Parts control is being added to.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> that the Web Parts control is being added to.</returns>
		// Token: 0x170016DA RID: 5850
		// (get) Token: 0x06004A15 RID: 18965 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004A16 RID: 18966 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebPartZoneBase Zone
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the index position of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control within its zone.</summary>
		/// <returns>The numerical order of a control within its zone. The first control in a zone has an index value of zero.</returns>
		// Token: 0x170016DB RID: 5851
		// (get) Token: 0x06004A17 RID: 18967 RVA: 0x000CA6AC File Offset: 0x000C88AC
		// (set) Token: 0x06004A18 RID: 18968 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public int ZoneIndex
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
