using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	/// <summary>Base designer class for extending the design mode behavior of a root design document that supports nested components.</summary>
	// Token: 0x0200000A RID: 10
	public class ComponentDocumentDesigner : ComponentDesigner, IRootDesigner, IDesigner, IDisposable, IToolboxUser, ITypeDescriptorFilterService, IOleDragClient
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ComponentDocumentDesigner" /> class. </summary>
		// Token: 0x0600002D RID: 45 RVA: 0x000023C4 File Offset: 0x000005C4
		[MonoTODO]
		public ComponentDocumentDesigner()
		{
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.Design.IRootDesigner.SupportedTechnologies" />.</summary>
		/// <returns>An array of supported <see cref="T:System.ComponentModel.Design.ViewTechnology" /> values.</returns>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000023CC File Offset: 0x000005CC
		ViewTechnology[] IRootDesigner.SupportedTechnologies
		{
			get
			{
				return new ViewTechnology[] { ViewTechnology.WindowsForms };
			}
		}

		/// <summary>For a description of this member, see <see cref="T:System.ComponentModel.Design.ViewTechnology" />.</summary>
		/// <returns>An object that represents the view for this designer.</returns>
		/// <param name="technology">A <see cref="T:System.ComponentModel.Design.ViewTechnology" /> that indicates a particular view technology.</param>
		// Token: 0x0600002F RID: 47 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		object IRootDesigner.GetView(ViewTechnology technology)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Drawing.Design.IToolboxUser.GetToolSupported(System.Drawing.Design.ToolboxItem)" />.</summary>
		/// <returns>true if the tool is supported by the toolbox and can be enabled; false if the document designer does not know how to use the tool.</returns>
		/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to be tested for toolbox support.</param>
		// Token: 0x06000030 RID: 48 RVA: 0x000023D8 File Offset: 0x000005D8
		bool IToolboxUser.GetToolSupported(ToolboxItem tool)
		{
			return true;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Drawing.Design.IToolboxUser.ToolPicked(System.Drawing.Design.ToolboxItem)" />.</summary>
		/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to select.</param>
		// Token: 0x06000031 RID: 49 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		void IToolboxUser.ToolPicked(ToolboxItem tool)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.ITypeDescriptorFilterService.FilterAttributes(System.ComponentModel.IComponent,System.Collections.IDictionary)" />.</summary>
		/// <returns>true if the set of filtered attributes is to be cached; false if the filter service must query again.</returns>
		/// <param name="component">The component to filter the attributes of.</param>
		/// <param name="attributes">A dictionary of attributes that can be modified.</param>
		// Token: 0x06000032 RID: 50 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		bool ITypeDescriptorFilterService.FilterAttributes(IComponent component, IDictionary attributes)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.ITypeDescriptorFilterService.FilterEvents(System.ComponentModel.IComponent,System.Collections.IDictionary)" />.</summary>
		/// <returns>true if the set of filtered events is to be cached; false if the filter service must query again.</returns>
		/// <param name="component">The component to filter events for.</param>
		/// <param name="events">A dictionary of events that can be modified.</param>
		// Token: 0x06000033 RID: 51 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		bool ITypeDescriptorFilterService.FilterEvents(IComponent component, IDictionary events)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.ITypeDescriptorFilterService.FilterProperties(System.ComponentModel.IComponent,System.Collections.IDictionary)" />.</summary>
		/// <returns>true if the set of filtered properties is to be cached; false if the filter service must query again.</returns>
		/// <param name="component">The component to filter properties for.</param>
		/// <param name="properties">A dictionary of properties that can be modified.</param>
		// Token: 0x06000034 RID: 52 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		bool ITypeDescriptorFilterService.FilterProperties(IComponent component, IDictionary properties)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		bool IOleDragClient.AddComponent(IComponent component, string name, bool firstAdd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000023D8 File Offset: 0x000005D8
		bool IOleDragClient.CanModifyComponents
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		Control IOleDragClient.GetControlForComponent(object component)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		Control IOleDragClient.GetDesignerControl()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000023D8 File Offset: 0x000005D8
		[MonoTODO]
		bool IOleDragClient.IsDropOk(IComponent component)
		{
			return true;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		IComponent IOleDragClient.Component
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the control for the designer.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> the designer is editing.</returns>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public Control Control
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value indicating whether the component tray for the designer is in auto-arrange mode.</summary>
		/// <returns>true if the component tray for the designer is in auto-arrange mode; otherwise, false.</returns>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000234B File Offset: 0x0000054B
		public bool TrayAutoArrange
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value indicating whether the component tray for the designer is in large icon mode.</summary>
		/// <returns>true if the component tray for the designer is in large icon mode; otherwise, false.</returns>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x0600003F RID: 63 RVA: 0x0000234B File Offset: 0x0000054B
		public bool TrayLargeIcon
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes the designer with the specified component.</summary>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to associate with the designer. </param>
		// Token: 0x06000040 RID: 64 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override void Initialize(IComponent component)
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Design.ComponentDocumentDesigner" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000041 RID: 65 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the specified tool is supported by the designer.</summary>
		/// <returns>true if the tool should be enabled on the toolbox; false if the document designer doesn't know how to use the tool.</returns>
		/// <param name="tool">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to test for toolbox support. </param>
		// Token: 0x06000042 RID: 66 RVA: 0x000023D8 File Offset: 0x000005D8
		protected virtual bool GetToolSupported(ToolboxItem tool)
		{
			return true;
		}

		/// <summary>Adjusts the set of properties the component will expose through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="properties">An <see cref="T:System.Collections.IDictionary" /> that contains the properties for the class of the component. </param>
		// Token: 0x06000043 RID: 67 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		protected override void PreFilterProperties(IDictionary properties)
		{
			throw new NotImplementedException();
		}
	}
}
