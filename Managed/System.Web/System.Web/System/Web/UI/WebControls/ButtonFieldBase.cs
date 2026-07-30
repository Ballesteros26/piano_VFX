using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the abstract base class for button fields, such as the <see cref="T:System.Web.UI.WebControls.ButtonField" /> or <see cref="T:System.Web.UI.WebControls.CommandField" /> class. The <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> class provides the methods and properties that are common to all button fields.</summary>
	// Token: 0x02000343 RID: 835
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class ButtonFieldBase : DataControlField
	{
		/// <summary>Gets or sets the button type to display in the button field.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is ButtonType.Link.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value for the <see cref="P:System.Web.UI.WebControls.ButtonFieldBase.ButtonType" /> property is not one of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. </exception>
		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x0004A935 File Offset: 0x00048B35
		// (set) Token: 0x06001DE0 RID: 7648 RVA: 0x0004A948 File Offset: 0x00048B48
		[DefaultValue(ButtonType.Link)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual ButtonType ButtonType
		{
			get
			{
				return (ButtonType)base.ViewState.GetInt("ButtonType", 2);
			}
			set
			{
				base.ViewState["ButtonType"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when a button in a <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> object is clicked.</summary>
		/// <returns>true to perform validation when a button in a <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> is clicked; otherwise, false. The default is false.</returns>
		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x0004A966 File Offset: 0x00048B66
		// (set) Token: 0x06001DE2 RID: 7650 RVA: 0x0004A979 File Offset: 0x00048B79
		[DefaultValue(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool CausesValidation
		{
			get
			{
				return base.ViewState.GetBool("CausesValidation", false);
			}
			set
			{
				base.ViewState["CausesValidation"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether the header section is displayed in a <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> object.</summary>
		/// <returns>true to show the header section; otherwise, false. The default is false.</returns>
		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x0004A997 File Offset: 0x00048B97
		// (set) Token: 0x06001DE4 RID: 7652 RVA: 0x0004A9AA File Offset: 0x00048BAA
		[DefaultValue(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public override bool ShowHeader
		{
			get
			{
				return base.ViewState.GetBool("showHeader", false);
			}
			set
			{
				base.ViewState["showHeader"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the name of the group of validation controls to validate when a button in a <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> object is clicked.</summary>
		/// <returns>The name of the validation group to validate when a button in a <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> is clicked. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.ButtonFieldBase.ValidationGroup" /> property is not set.</returns>
		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x0004A9C8 File Offset: 0x00048BC8
		// (set) Token: 0x06001DE6 RID: 7654 RVA: 0x0004A9DF File Offset: 0x00048BDF
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual string ValidationGroup
		{
			get
			{
				return base.ViewState.GetString("ValidationGroup", string.Empty);
			}
			set
			{
				base.ViewState["ValidationGroup"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Copies the properties of the current object that is derived from the <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> class to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to which to copy the properties of the current <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" />.</param>
		// Token: 0x06001DE7 RID: 7655 RVA: 0x0004A9F8 File Offset: 0x00048BF8
		protected override void CopyProperties(DataControlField newField)
		{
			base.CopyProperties(newField);
			ButtonFieldBase buttonFieldBase = (ButtonFieldBase)newField;
			buttonFieldBase.ButtonType = this.ButtonType;
			buttonFieldBase.CausesValidation = this.CausesValidation;
			buttonFieldBase.ShowHeader = this.ShowHeader;
			buttonFieldBase.ValidationGroup = this.ValidationGroup;
		}
	}
}
