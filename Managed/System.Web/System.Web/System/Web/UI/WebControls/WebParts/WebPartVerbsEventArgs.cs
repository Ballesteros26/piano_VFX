using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides event data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartZoneBase.CreateVerbs" /> event that is used by the <see cref="M:System.Web.UI.WebControls.WebParts.WebPartZoneBase.OnCreateVerbs(System.Web.UI.WebControls.WebParts.WebPartVerbsEventArgs)" /> method.</summary>
	// Token: 0x0200047B RID: 1147
	public class WebPartVerbsEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbsEventArgs" /> class.</summary>
		// Token: 0x06003437 RID: 13367 RVA: 0x0008A965 File Offset: 0x00088B65
		public WebPartVerbsEventArgs()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbsEventArgs" /> class using the specified Web Parts verb collection.</summary>
		/// <param name="verbs">A Web Parts verb collection.</param>
		// Token: 0x06003438 RID: 13368 RVA: 0x0008A96E File Offset: 0x00088B6E
		public WebPartVerbsEventArgs(WebPartVerbCollection verbs)
		{
			this._verbs = verbs;
		}

		/// <summary>Gets or sets the Web Parts verbs used in the event data.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbCollection" />.</returns>
		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x06003439 RID: 13369 RVA: 0x0008A97D File Offset: 0x00088B7D
		// (set) Token: 0x0600343A RID: 13370 RVA: 0x0008A993 File Offset: 0x00088B93
		public WebPartVerbCollection Verbs
		{
			get
			{
				if (this._verbs == null)
				{
					return WebPartVerbCollection.Empty;
				}
				return this._verbs;
			}
			set
			{
				this._verbs = value;
			}
		}

		// Token: 0x04001CFE RID: 7422
		private WebPartVerbCollection _verbs;
	}
}
