using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the base class for all Web Parts part controls, which render a modular user interface on a Web Forms page. </summary>
	// Token: 0x02000486 RID: 1158
	public abstract class Part : Panel, INamingContainer, ICompositeControlDesignerAccessor
	{
		// Token: 0x06003472 RID: 13426 RVA: 0x0008AC3F File Offset: 0x00088E3F
		internal Part()
		{
			this.description = "";
			this.title = "";
			this.chrome_state = PartChromeState.Normal;
			this.chrome_type = PartChromeType.Default;
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public override void DataBind()
		{
			throw new NotImplementedException();
		}

		/// <summary>Allows the developer of a designer for a composite part control to recreate the control's child controls on the design surface.</summary>
		// Token: 0x06003474 RID: 13428 RVA: 0x000503B2 File Offset: 0x0004E5B2
		[global::System.MonoTODO("not sure exactly what this one does..")]
		void ICompositeControlDesignerAccessor.RecreateChildControls()
		{
			this.CreateChildControls();
		}

		/// <summary>Gets or sets whether a part control is in a minimized or normal state.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeState" /> values. The default is <see cref="F:System.Web.UI.WebControls.WebParts.PartChromeState.Normal" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeState" /> values. </exception>
		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06003475 RID: 13429 RVA: 0x0008AC6B File Offset: 0x00088E6B
		// (set) Token: 0x06003476 RID: 13430 RVA: 0x0008AC73 File Offset: 0x00088E73
		public virtual PartChromeState ChromeState
		{
			get
			{
				return this.chrome_state;
			}
			set
			{
				this.chrome_state = value;
			}
		}

		/// <summary>Gets or sets the type of border that frames a Web Parts control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> values. The default is <see cref="F:System.Web.UI.WebControls.WebParts.PartChromeType.Default" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> values. </exception>
		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06003477 RID: 13431 RVA: 0x0008AC7C File Offset: 0x00088E7C
		// (set) Token: 0x06003478 RID: 13432 RVA: 0x0008AC84 File Offset: 0x00088E84
		public virtual PartChromeType ChromeType
		{
			get
			{
				return this.chrome_type;
			}
			set
			{
				this.chrome_type = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> object that contains the child controls for a specified server control in the user interface hierarchy.</summary>
		/// <returns>The collection of child controls for the specified server control.</returns>
		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06003479 RID: 13433 RVA: 0x0008AC8D File Offset: 0x00088E8D
		public new virtual ControlCollection Controls
		{
			get
			{
				if (this.controls == null)
				{
					this.controls = new ControlCollection(this);
				}
				return this.controls;
			}
		}

		/// <summary>Gets or sets a brief phrase that summarizes what the part control does, for use in ToolTips and catalogs of part controls.</summary>
		/// <returns>A string that briefly summarizes the part control's functionality. The default value is an empty string ("").</returns>
		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x0600347A RID: 13434 RVA: 0x0008ACA9 File Offset: 0x00088EA9
		// (set) Token: 0x0600347B RID: 13435 RVA: 0x0008ACB1 File Offset: 0x00088EB1
		public virtual string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		/// <summary>Gets or sets the title of a part control.</summary>
		/// <returns>A string that represents the title of the part control. The default value is an empty string ("").</returns>
		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x0600347C RID: 13436 RVA: 0x0008ACBA File Offset: 0x00088EBA
		// (set) Token: 0x0600347D RID: 13437 RVA: 0x0008ACC2 File Offset: 0x00088EC2
		public virtual string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x04001D0F RID: 7439
		private string description;

		// Token: 0x04001D10 RID: 7440
		private string title;

		// Token: 0x04001D11 RID: 7441
		private PartChromeState chrome_state;

		// Token: 0x04001D12 RID: 7442
		private PartChromeType chrome_type;

		// Token: 0x04001D13 RID: 7443
		private ControlCollection controls;
	}
}
