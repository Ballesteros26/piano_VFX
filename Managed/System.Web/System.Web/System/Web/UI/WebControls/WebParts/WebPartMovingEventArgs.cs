using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartMoving" /> event. </summary>
	// Token: 0x020006D1 RID: 1745
	public class WebPartMovingEventArgs : WebPartCancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartMovingEventArgs" /> class. </summary>
		/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server control being moved.</param>
		/// <param name="zone">The target <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> to which <paramref name="webPart" /> is being moved.</param>
		/// <param name="zoneIndex">An integer that indicates the index of <paramref name="webPart" /> relative to other controls within <paramref name="zone" />.</param>
		// Token: 0x06004A1D RID: 18973 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebPartMovingEventArgs(WebPart webPart, WebPartZoneBase zone, int zoneIndex)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the Web Parts zone to which the Web Parts control is being moved.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> to which the Web Parts control is being moved.</returns>
		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x06004A1E RID: 18974 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004A1F RID: 18975 RVA: 0x0000B3E4 File Offset: 0x000095E4
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

		/// <summary>Gets or sets the index position of a Web Parts control within its zone.</summary>
		/// <returns>The numerical order of a control within its zone. The first control in a zone has an index value of zero.</returns>
		// Token: 0x170016DD RID: 5853
		// (get) Token: 0x06004A20 RID: 18976 RVA: 0x000CA6C8 File Offset: 0x000C88C8
		// (set) Token: 0x06004A21 RID: 18977 RVA: 0x0000B3E4 File Offset: 0x000095E4
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
