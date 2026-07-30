using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.Design.IControlDesignerView.ViewEvent" /> event.</summary>
	// Token: 0x020000B4 RID: 180
	public class ViewEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.ViewEventArgs" /> class for the type of view event on the design surface. </summary>
		/// <param name="eventType">The type of action that raised the event; used to initialize the <see cref="P:System.Web.UI.Design.ViewEventArgs.EventType" />.</param>
		/// <param name="region">The designer region that the action applies to; used to initialize the <see cref="P:System.Web.UI.Design.ViewEventArgs.Region" />.</param>
		/// <param name="eventArgs">The event arguments associated with <paramref name="eventType" />; used to initialize the <see cref="P:System.Web.UI.Design.ViewEventArgs.EventArgs" />.</param>
		// Token: 0x06000544 RID: 1348 RVA: 0x000094E4 File Offset: 0x000076E4
		public ViewEventArgs(ViewEvent eventType, DesignerRegion region, EventArgs eventArgs)
		{
			this.event_type = eventType;
			this.region = region;
			this.event_args = eventArgs;
		}

		/// <summary>Gets the type of action that raised the event.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Design.ViewEvent" /> that specifies the type of action that raised the event.</returns>
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x00009501 File Offset: 0x00007701
		public ViewEvent EventType
		{
			get
			{
				return this.event_type;
			}
		}

		/// <summary>Gets the designer region that the event applies to.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Design.DesignerRegion" /> that the action applies to.</returns>
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00009509 File Offset: 0x00007709
		public DesignerRegion Region
		{
			get
			{
				return this.region;
			}
		}

		/// <summary>Gets the event arguments that are associated with the action that raised the event.</summary>
		/// <returns>An <see cref="P:System.Web.UI.Design.ViewEventArgs.EventArgs" /> that contains additional event data that is specific to the type of event.</returns>
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00009511 File Offset: 0x00007711
		public EventArgs EventArgs
		{
			get
			{
				return this.event_args;
			}
		}

		// Token: 0x04000141 RID: 321
		private ViewEvent event_type;

		// Token: 0x04000142 RID: 322
		private DesignerRegion region;

		// Token: 0x04000143 RID: 323
		private EventArgs event_args;
	}
}
