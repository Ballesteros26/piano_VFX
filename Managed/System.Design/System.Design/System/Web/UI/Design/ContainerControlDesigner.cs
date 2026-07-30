using System;
using System.Collections;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using Unity;

namespace System.Web.UI.Design
{
	/// <summary>Provides designer functionality for controls that contain child controls or properties that can be modified at design time.</summary>
	// Token: 0x02000057 RID: 87
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ContainerControlDesigner : ControlDesigner
	{
		/// <summary>Gets a value indicating if the control can be resized at design time.</summary>
		/// <returns>true, if the control can be resized; otherwise, false.</returns>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override bool AllowResize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the caption that is displayed for a control at design time.</summary>
		/// <returns>The string used for the control frame caption at design time, if the control has a design-time caption; otherwise, an empty string ("").</returns>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual string FrameCaption
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the style that is applied to the control frame at design time.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> for the control frame at design time.</returns>
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual Style FrameStyle
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Adds the style attributes for the control at design time.</summary>
		/// <param name="styleAttributes">A keyed collection of style attributes.</param>
		// Token: 0x060002B7 RID: 695 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected virtual void AddDesignTimeCssAttributes(IDictionary styleAttributes)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the collection of style attributes for the control at design time.</summary>
		/// <returns>A collection of style attributes applied to the control on the design surface. The style attribute names are keys used to access the style attribute values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x060002B8 RID: 696 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public virtual IDictionary GetDesignTimeCssAttributes()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the HTML markup that is used to represent the control at design time.</summary>
		/// <returns>An HTML markup string that represents the control.</returns>
		/// <param name="regions">A collection of designer regions.</param>
		// Token: 0x060002B9 RID: 697 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the content for the editable region of the control at design time.</summary>
		/// <returns>The persisted content of the region contained within the <see cref="T:System.Web.UI.Design.ContainerControlDesigner" />.</returns>
		/// <param name="region">An editable design region contained within the control.</param>
		// Token: 0x060002BA RID: 698 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the persistable content of the control at design time.</summary>
		/// <returns>null.</returns>
		// Token: 0x060002BB RID: 699 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override string GetPersistenceContent()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the content for the editable region of the control at design time.</summary>
		/// <param name="region">An editable design region contained within the control.</param>
		/// <param name="content">Content to assign for the editable design region.</param>
		// Token: 0x060002BC RID: 700 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value that specifies whether to use the HTML nowrap attribute on tables.</summary>
		/// <returns>true to use the HTML nowrap attribute on tables; otherwise, false.</returns>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00008E2C File Offset: 0x0000702C
		protected virtual bool NoWrap
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}
	}
}
