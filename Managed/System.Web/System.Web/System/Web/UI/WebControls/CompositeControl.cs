using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Implements the basic functionality required by Web controls that contain child controls.</summary>
	// Token: 0x02000356 RID: 854
	[Designer("System.Web.UI.Design.WebControls.CompositeControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class CompositeControl : WebControl, INamingContainer, ICompositeControlDesignerAccessor
	{
		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x0004789D File Offset: 0x00045A9D
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Binds a data source to the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> and all its child controls.</summary>
		// Token: 0x06001FB4 RID: 8116 RVA: 0x0005038D File Offset: 0x0004E58D
		public override void DataBind()
		{
			this.EnsureChildControls();
			base.DataBind();
		}

		/// <summary>Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06001FB5 RID: 8117 RVA: 0x0005039B File Offset: 0x0004E59B
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			base.Render(writer);
		}

		/// <summary>Enables a designer to recreate the composite control's collection of child controls in the design-time environment.</summary>
		// Token: 0x06001FB6 RID: 8118 RVA: 0x000503AA File Offset: 0x0004E5AA
		void ICompositeControlDesignerAccessor.RecreateChildControls()
		{
			this.RecreateChildControls();
		}

		/// <summary>Recreates the child controls in a control derived from <see cref="T:System.Web.UI.WebControls.CompositeControl" />. </summary>
		// Token: 0x06001FB7 RID: 8119 RVA: 0x000503B2 File Offset: 0x0004E5B2
		[global::System.MonoTODO("not sure exactly what this one does..")]
		protected virtual void RecreateChildControls()
		{
			this.CreateChildControls();
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> object that represents the child controls in a <see cref="T:System.Web.UI.WebControls.CompositeControl" />.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that represents the child controls in the <see cref="T:System.Web.UI.WebControls.CompositeControl" />.</returns>
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x00047ACE File Offset: 0x00045CCE
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}
	}
}
