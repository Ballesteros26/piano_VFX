using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.DesignerActionUIService.DesignerActionUIStateChange" /> event.</summary>
	// Token: 0x0200011A RID: 282
	public class DesignerActionUIStateChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionUIStateChangeEventArgs" /> class.</summary>
		/// <param name="relatedObject">The object that is associated with the panel.</param>
		/// <param name="changeType">A value that specifies whether the panel is being displayed or hidden.</param>
		// Token: 0x06000837 RID: 2103 RVA: 0x0000DA09 File Offset: 0x0000BC09
		public DesignerActionUIStateChangeEventArgs(object relatedObject, DesignerActionUIStateChangeType changeType)
		{
			this.related_object = relatedObject;
			this.change_type = changeType;
		}

		/// <summary>Gets a flag indicating whether the smart tag panel is being displayed or hidden.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionUIStateChangeType" /> that indicates the state of the panel.</returns>
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x0000DA1F File Offset: 0x0000BC1F
		public DesignerActionUIStateChangeType ChangeType
		{
			get
			{
				return this.change_type;
			}
		}

		/// <summary>Gets the object that is associated with the smart tag panel.</summary>
		/// <returns>The <see cref="T:System.Object" /> associated with the smart tag panel.</returns>
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0000DA27 File Offset: 0x0000BC27
		public object RelatedObject
		{
			get
			{
				return this.related_object;
			}
		}

		// Token: 0x040001C2 RID: 450
		private object related_object;

		// Token: 0x040001C3 RID: 451
		private DesignerActionUIStateChangeType change_type;
	}
}
