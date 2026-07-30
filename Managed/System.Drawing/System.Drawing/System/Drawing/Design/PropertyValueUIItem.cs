using System;
using System.Security.Permissions;

namespace System.Drawing.Design
{
	/// <summary>Provides information about a property displayed in the Properties window, including the associated event handler, pop-up information string, and the icon to display for the property.</summary>
	// Token: 0x02000122 RID: 290
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class PropertyValueUIItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.PropertyValueUIItem" /> class.</summary>
		/// <param name="uiItemImage">The icon to display. The image must be 8 x 8 pixels. </param>
		/// <param name="handler">The handler to invoke when the image is double-clicked. </param>
		/// <param name="tooltip">The <see cref="P:System.Drawing.Design.PropertyValueUIItem.ToolTip" /> to display for the property that this <see cref="T:System.Drawing.Design.PropertyValueUIItem" /> is associated with. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uiItemImage" /> or <paramref name="handler" /> is null.</exception>
		// Token: 0x06000D70 RID: 3440 RVA: 0x0001D941 File Offset: 0x0001BB41
		public PropertyValueUIItem(Image uiItemImage, PropertyValueUIItemInvokeHandler handler, string tooltip)
		{
			this.itemImage = uiItemImage;
			this.handler = handler;
			if (this.itemImage == null)
			{
				throw new ArgumentNullException("uiItemImage");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.tooltip = tooltip;
		}

		/// <summary>Gets the 8 x 8 pixel image that will be drawn in the Properties window.</summary>
		/// <returns>The image to use for the property icon.</returns>
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0001D97F File Offset: 0x0001BB7F
		public virtual Image Image
		{
			get
			{
				return this.itemImage;
			}
		}

		/// <summary>Gets the handler that is raised when a user double-clicks this item.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.PropertyValueUIItemInvokeHandler" /> indicating the event handler for this user interface (UI) item.</returns>
		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x0001D987 File Offset: 0x0001BB87
		public virtual PropertyValueUIItemInvokeHandler InvokeHandler
		{
			get
			{
				return this.handler;
			}
		}

		/// <summary>Gets or sets the information string to display for this item.</summary>
		/// <returns>A string containing the information string to display for this item.</returns>
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x0001D98F File Offset: 0x0001BB8F
		public virtual string ToolTip
		{
			get
			{
				return this.tooltip;
			}
		}

		/// <summary>Resets the user interface (UI) item.</summary>
		// Token: 0x06000D74 RID: 3444 RVA: 0x00002CE2 File Offset: 0x00000EE2
		public virtual void Reset()
		{
		}

		// Token: 0x04000A81 RID: 2689
		private Image itemImage;

		// Token: 0x04000A82 RID: 2690
		private PropertyValueUIItemInvokeHandler handler;

		// Token: 0x04000A83 RID: 2691
		private string tooltip;
	}
}
