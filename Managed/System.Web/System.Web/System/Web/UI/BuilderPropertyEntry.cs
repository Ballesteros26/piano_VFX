using System;

namespace System.Web.UI
{
	/// <summary>Serves as the base class for all property entries that require a control builder.</summary>
	// Token: 0x020001A9 RID: 425
	public abstract class BuilderPropertyEntry : PropertyEntry
	{
		/// <summary>Gets or sets the control builder for the property entry.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlBuilder" /> for this property entry.</returns>
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x0002CA3F File Offset: 0x0002AC3F
		// (set) Token: 0x06001056 RID: 4182 RVA: 0x0002CA47 File Offset: 0x0002AC47
		public ControlBuilder Builder { get; set; }
	}
}
