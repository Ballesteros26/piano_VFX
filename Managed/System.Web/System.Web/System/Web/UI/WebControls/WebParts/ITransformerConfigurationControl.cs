using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines the contract a control implements to act as a configuration control for a transformer in a Web Parts connection.</summary>
	// Token: 0x02000462 RID: 1122
	public interface ITransformerConfigurationControl
	{
		/// <summary>Occurs when transformer configuration is not completed.</summary>
		// Token: 0x140000FE RID: 254
		// (add) Token: 0x060033E5 RID: 13285
		// (remove) Token: 0x060033E6 RID: 13286
		event EventHandler Cancelled;

		/// <summary>Occurs when transformer configuration is successfully completed. </summary>
		// Token: 0x140000FF RID: 255
		// (add) Token: 0x060033E7 RID: 13287
		// (remove) Token: 0x060033E8 RID: 13288
		event EventHandler Succeeded;
	}
}
