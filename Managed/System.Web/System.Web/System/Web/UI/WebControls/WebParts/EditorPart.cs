using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the base class for controls that reside in <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zones and are used to edit <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
	// Token: 0x02000483 RID: 1155
	[Bindable(false)]
	[Designer("System.Web.UI.Design.WebControls.WebParts.EditorPartDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public abstract class EditorPart : Part
	{
		/// <summary>Saves the values in an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control to the corresponding properties in the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>true if the action of saving values from the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is successful; otherwise (if an error occurs), false. </returns>
		// Token: 0x0600345E RID: 13406
		public abstract bool ApplyChanges();

		/// <summary>Retrieves the current state of an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control's parent zone.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> that has the current state of the <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zone that contains an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control.</returns>
		// Token: 0x0600345F RID: 13407 RVA: 0x00003A1F File Offset: 0x00001C1F
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override IDictionary GetDesignModeState()
		{
			throw new NotImplementedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> that contains the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control is null.</exception>
		// Token: 0x06003460 RID: 13408 RVA: 0x0008AAE6 File Offset: 0x00088CE6
		protected internal override void OnPreRender(EventArgs e)
		{
			if (this.zone == null)
			{
				throw new InvalidOperationException();
			}
			base.OnPreRender(e);
			if (!this.Display)
			{
				this.Visible = false;
			}
		}

		/// <summary>Retrieves the property values from a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control for its associated <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control.</summary>
		// Token: 0x06003461 RID: 13409
		public abstract void SyncChanges();

		/// <summary>Gets a value that indicates whether a control should be displayed when its associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is in edit mode.</summary>
		/// <returns>A Boolean value that indicates whether the control should be displayed. The default value is true.</returns>
		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x06003462 RID: 13410 RVA: 0x0008AB0C File Offset: 0x00088D0C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool Display
		{
			get
			{
				return this.display;
			}
		}

		/// <summary>Gets a string that contains the title text displayed in the title bar of an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control.</summary>
		/// <returns>A string that represents the complete, visible title of the control. The default value is a calculated, culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x06003463 RID: 13411 RVA: 0x0008AB14 File Offset: 0x00088D14
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string DisplayTitle
		{
			get
			{
				return this.displayTitle;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control that is currently being edited. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> that is currently in edit mode.</returns>
		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x06003464 RID: 13412 RVA: 0x0008AB1C File Offset: 0x00088D1C
		protected WebPart WebPartToEdit
		{
			get
			{
				return this.webPartToEdit;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control associated with the current Web page.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> on the page. </returns>
		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x06003465 RID: 13413 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected WebPartManager WebPartManager
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zone that contains an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> that contains the control.</returns>
		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x06003466 RID: 13414 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected EditorZoneBase Zone
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x04001D0A RID: 7434
		private bool display = true;

		// Token: 0x04001D0B RID: 7435
		private WebPart webPartToEdit;

		// Token: 0x04001D0C RID: 7436
		private object zone;

		// Token: 0x04001D0D RID: 7437
		private string displayTitle;
	}
}
