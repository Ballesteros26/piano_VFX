using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Converts a control on the Web Forms page that can be validated with a validation control to a string containing the control's ID.</summary>
	// Token: 0x02000439 RID: 1081
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ValidatedControlConverter : ControlIDConverter
	{
		// Token: 0x060031D3 RID: 12755 RVA: 0x00085318 File Offset: 0x00083518
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (context == null || context.Container == null || context.Container.Components == null)
			{
				return base.GetStandardValues(context);
			}
			ArrayList arrayList = new ArrayList();
			ComponentCollection components = context.Container.Components;
			int count = components.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.FilterControl((Control)components[i]))
				{
					string id = ((Control)components[i]).ID;
					if (id != null && id.Length > 0)
					{
						arrayList.Add(id);
					}
				}
			}
			arrayList.Sort();
			if (arrayList.Count > 0)
			{
				return new TypeConverter.StandardValuesCollection(arrayList);
			}
			return null;
		}

		/// <summary>Returns a value indicating whether the specified control should be added to the list of controls that can be validated.</summary>
		/// <returns>true if the control should be added to the list of controls that can be validated; otherwise, false.</returns>
		/// <param name="control">The control to check. </param>
		// Token: 0x060031D4 RID: 12756 RVA: 0x000853C7 File Offset: 0x000835C7
		protected override bool FilterControl(Control control)
		{
			return BaseValidator.GetValidationProperty(control) != null;
		}
	}
}
