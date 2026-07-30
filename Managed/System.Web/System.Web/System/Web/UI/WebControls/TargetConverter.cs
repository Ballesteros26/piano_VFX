using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Converts a value that represents the location (target) in which to display the content resulting from a Web navigation to a string. This class also converts a string to a target value.</summary>
	// Token: 0x02000426 RID: 1062
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TargetConverter : StringConverter
	{
		/// <summary>Returns a collection of standard values from the default context for the data type this type converter is designed for.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> containing a standard set of valid values, or null if the data type does not support a standard set of values.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		// Token: 0x06002FE9 RID: 12265 RVA: 0x0007E5C7 File Offset: 0x0007C7C7
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(new string[] { "_blank", "_parent", "_search", "_self", "_top" });
		}

		/// <summary>Returns a value that indicates whether the collection of standard values returned from the <see cref="M:System.Web.UI.WebControls.TargetConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> method is an exclusive list.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.Web.UI.WebControls.TargetConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> is an exhaustive list of possible values; false if other values are possible.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		// Token: 0x06002FEA RID: 12266 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Returns a value that indicates whether this object supports a standard set of values that can be picked from a list, using the specified context.</summary>
		/// <returns>true if <see cref="M:System.Web.UI.WebControls.TargetConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> should be called to find a common set of values the object supports; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		// Token: 0x06002FEB RID: 12267 RVA: 0x00008B66 File Offset: 0x00006D66
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
