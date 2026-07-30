using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Declares the base type of the container control of a property that returns an <see cref="T:System.Web.UI.ITemplate" /> interface and is marked with the <see cref="T:System.Web.UI.TemplateContainerAttribute" /> attribute. The control with the <see cref="T:System.Web.UI.ITemplate" /> property must implement the <see cref="T:System.Web.UI.INamingContainer" /> interface. This class cannot be inherited.</summary>
	// Token: 0x02000230 RID: 560
	[AttributeUsage(AttributeTargets.Property)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class TemplateContainerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TemplateContainerAttribute" /> class using the specified container type and the <see cref="P:System.Web.UI.TemplateContainerAttribute.BindingDirection" /> property.</summary>
		/// <param name="containerType">The <see cref="T:System.Type" /> for the container control.</param>
		/// <param name="bindingDirection">The <see cref="P:System.Web.UI.TemplateContainerAttribute.BindingDirection" /> for the container control.</param>
		// Token: 0x060016FF RID: 5887 RVA: 0x0003DA9A File Offset: 0x0003BC9A
		public TemplateContainerAttribute(Type containerType, BindingDirection bindingDirection)
		{
			this.containerType = containerType;
			this.direction = bindingDirection;
		}

		/// <summary>Gets the binding direction of the container control.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.BindingDirection" /> indicating the container control's binding direction. The default is <see cref="F:System.ComponentModel.BindingDirection.OneWay" />.</returns>
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x0003DAB0 File Offset: 0x0003BCB0
		public BindingDirection BindingDirection
		{
			get
			{
				return this.direction;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TemplateContainerAttribute" /> class using the specified container type.</summary>
		/// <param name="containerType">The <see cref="T:System.Type" /> for the container control. </param>
		// Token: 0x06001701 RID: 5889 RVA: 0x0003DAB8 File Offset: 0x0003BCB8
		public TemplateContainerAttribute(Type containerType)
		{
			this.containerType = containerType;
		}

		/// <summary>Gets the container control type.</summary>
		/// <returns>The container control <see cref="T:System.Type" />.</returns>
		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x0003DAC7 File Offset: 0x0003BCC7
		public Type ContainerType
		{
			get
			{
				return this.containerType;
			}
		}

		// Token: 0x04001590 RID: 5520
		private Type containerType;

		// Token: 0x04001591 RID: 5521
		private BindingDirection direction;
	}
}
