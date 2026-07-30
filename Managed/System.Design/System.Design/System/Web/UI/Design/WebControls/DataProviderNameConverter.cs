using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Creates a user-selectable list of ActiveX® Data Objects (ADO) for the .NET Framework (ADO.NET) provider names. </summary>
	// Token: 0x020000CF RID: 207
	public class DataProviderNameConverter : StringConverter
	{
		/// <summary>Returns a list of the available ActiveX® Data Objects (ADO) for the .NET Framework (ADO.NET) provider names.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> containing the names of the available ADO.NET providers.</returns>
		/// <param name="context">An object implementing the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about a context to a type converter so that the type converter can perform a conversion.</param>
		// Token: 0x0600060E RID: 1550 RVA: 0x0000975C File Offset: 0x0000795C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			DataTable factoryClasses = DbProviderFactories.GetFactoryClasses();
			if (factoryClasses == null)
			{
				return new TypeConverter.StandardValuesCollection(new string[0]);
			}
			string[] array = new string[factoryClasses.Rows.Count];
			int num = 0;
			foreach (object obj in factoryClasses.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				array[num++] = (string)dataRow["Name"];
			}
			return new TypeConverter.StandardValuesCollection(array);
		}

		/// <summary>Gets a value indicating whether the returned ActiveX® Data Objects (ADO) for the .NET Framework (ADO.NET) provider names are an exclusive list of possible values.</summary>
		/// <returns>Always false.</returns>
		/// <param name="context">An object implementing the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about a context to a type converter so that the type converter can perform a conversion.</param>
		// Token: 0x0600060F RID: 1551 RVA: 0x000023D8 File Offset: 0x000005D8
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Gets a value indicating whether this object returns a standard set of ActiveX® Data Objects (ADO) for the .NET Framework (ADO.NET) provider names that can be picked from a list.</summary>
		/// <returns>Always true.</returns>
		/// <param name="context">An object implementing the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about a context to a type converter so that the type converter can perform a conversion.</param>
		// Token: 0x06000610 RID: 1552 RVA: 0x0000241E File Offset: 0x0000061E
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}
