using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides a type converter that retrieves a list of property names for the current control.</summary>
	// Token: 0x0200035F RID: 863
	public class ControlPropertyNameConverter : StringConverter
	{
		/// <summary>Returns a collection of property names for the control within a designer that implements <see cref="T:System.ComponentModel.Design.IDesignerHost" /> when provided with a format context.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that contains a set of strings representing property names for the current control. If the current control is null, an empty collection is returned. If the <paramref name="context" /> parameter is null, null is returned.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null.</param>
		// Token: 0x06001FED RID: 8173 RVA: 0x00003BEA File Offset: 0x00001DEA
		[global::System.MonoLimitation("This implementation always returns null")]
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return null;
		}

		/// <summary>Returns a value that indicates whether this object supports a standard set of values that can be chosen from a list, using the specified context.</summary>
		/// <returns>true if the <paramref name="context" /> parameter is not null; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null.</param>
		// Token: 0x06001FEE RID: 8174 RVA: 0x0005086B File Offset: 0x0004EA6B
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return context != null;
		}

		/// <summary>Returns a value that indicates whether the collection of standard values returned by the <see cref="Overload:System.Web.UI.WebControls.ControlPropertyNameConverter.GetStandardValues" /> method is an exclusive list of possible values, using the specified context.</summary>
		/// <returns>false in all cases, which indicates that the list is not exclusive.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null.</param>
		// Token: 0x06001FEF RID: 8175 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}
