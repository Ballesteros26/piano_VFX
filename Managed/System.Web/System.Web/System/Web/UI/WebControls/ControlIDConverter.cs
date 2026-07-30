using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides a type converter that retrieves a list of control IDs in the current container.</summary>
	// Token: 0x0200035D RID: 861
	public class ControlIDConverter : StringConverter
	{
		/// <summary>Returns a value indicating whether the control ID of the specified control is added to the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that is returned by the <see cref="M:System.Web.UI.WebControls.ControlIDConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> method.</summary>
		/// <returns>true in all cases.</returns>
		/// <param name="control">The control instance to test for inclusion in the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" />. </param>
		// Token: 0x06001FDD RID: 8157 RVA: 0x00008B66 File Offset: 0x00006D66
		protected virtual bool FilterControl(Control control)
		{
			return true;
		}

		/// <summary>Returns a collection of control IDs from the container within the <see cref="T:System.ComponentModel.Design.IDesignerHost" /> when provided with a format context.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a set of strings representing the control IDs of the controls in the current container. If no controls are currently contained, an empty collection is returned. If the context is null or there is no current container, then null is returned.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null. </param>
		// Token: 0x06001FDE RID: 8158 RVA: 0x00050610 File Offset: 0x0004E810
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (context == null)
			{
				return null;
			}
			IContainer container = context.Container;
			if (container == null)
			{
				return null;
			}
			ReadOnlyCollectionBase components = container.Components;
			ArrayList arrayList = new ArrayList(0);
			foreach (object obj in components)
			{
				Control control = (Control)obj;
				if (this.FilterControl(control))
				{
					arrayList.Add(control.ID);
				}
			}
			return new TypeConverter.StandardValuesCollection(arrayList);
		}

		/// <summary>Returns a value indicating whether the collection of standard values returned by the <see cref="M:System.Web.UI.WebControls.ControlIDConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> method is an exclusive list of possible values, using the specified context.</summary>
		/// <returns>false in all cases.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06001FDF RID: 8159 RVA: 0x00008A69 File Offset: 0x00006C69
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Returns a value indicating whether this object supports a standard set of control ID values that can be picked from a list, using the specified context.</summary>
		/// <returns>true if <see cref="M:System.Web.UI.WebControls.ControlIDConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> should be called to find a common set of control ID values the object supports; otherwise, false. This implementation returns true if the context is not null; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06001FE0 RID: 8160 RVA: 0x0005069C File Offset: 0x0004E89C
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return context != null;
		}
	}
}
