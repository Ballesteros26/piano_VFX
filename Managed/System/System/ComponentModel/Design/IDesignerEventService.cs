using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides event notifications when root designers are added and removed, when a selected component changes, and when the current root designer changes.</summary>
	// Token: 0x02000328 RID: 808
	public interface IDesignerEventService
	{
		/// <summary>Gets the root designer for the currently active document.</summary>
		/// <returns>The currently active document, or null if there is no active document.</returns>
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001998 RID: 6552
		IDesignerHost ActiveDesigner { get; }

		/// <summary>Gets a collection of root designers for design documents that are currently active in the development environment.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerCollection" /> containing the root designers that have been created and not yet disposed.</returns>
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001999 RID: 6553
		DesignerCollection Designers { get; }

		/// <summary>Occurs when the current root designer changes.</summary>
		// Token: 0x14000032 RID: 50
		// (add) Token: 0x0600199A RID: 6554
		// (remove) Token: 0x0600199B RID: 6555
		event ActiveDesignerEventHandler ActiveDesignerChanged;

		/// <summary>Occurs when a root designer is created.</summary>
		// Token: 0x14000033 RID: 51
		// (add) Token: 0x0600199C RID: 6556
		// (remove) Token: 0x0600199D RID: 6557
		event DesignerEventHandler DesignerCreated;

		/// <summary>Occurs when a root designer for a document is disposed.</summary>
		// Token: 0x14000034 RID: 52
		// (add) Token: 0x0600199E RID: 6558
		// (remove) Token: 0x0600199F RID: 6559
		event DesignerEventHandler DesignerDisposed;

		/// <summary>Occurs when the current design-view selection changes.</summary>
		// Token: 0x14000035 RID: 53
		// (add) Token: 0x060019A0 RID: 6560
		// (remove) Token: 0x060019A1 RID: 6561
		event EventHandler SelectionChanged;
	}
}
