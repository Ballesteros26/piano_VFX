using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI
{
	/// <summary>Created when a user control (.ascx file) is specified for output caching, using either the @ OutputCache page directive or the <see cref="T:System.Web.UI.PartialCachingAttribute" /> attribute, and the user control is inserted into a page's control hierarchy by dynamically loading the user control with the <see cref="M:System.Web.UI.TemplateControl.LoadControl(System.String)" /> method.</summary>
	// Token: 0x0200021A RID: 538
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class PartialCachingControl : BasePartialCachingControl
	{
		// Token: 0x06001623 RID: 5667 RVA: 0x0003B7F6 File Offset: 0x000399F6
		internal PartialCachingControl(Type type, object[] parameters)
		{
			this.type = type;
			this.parameters = parameters;
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x0003B80C File Offset: 0x00039A0C
		internal override Control CreateControl()
		{
			this.control = (Control)Activator.CreateInstance(this.type, this.parameters);
			if (this.control is UserControl)
			{
				((UserControl)this.control).InitializeAsUserControl(this.Page);
			}
			return this.control;
		}

		/// <summary>Gets a reference to the user control that is cached.</summary>
		/// <returns>The user control that is cached.</returns>
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x0003B85E File Offset: 0x00039A5E
		public Control CachedControl
		{
			get
			{
				return this.control;
			}
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0000B3E4 File Offset: 0x000095E4
		internal PartialCachingControl()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400154D RID: 5453
		private Type type;

		// Token: 0x0400154E RID: 5454
		private object[] parameters;

		// Token: 0x0400154F RID: 5455
		private Control control;
	}
}
