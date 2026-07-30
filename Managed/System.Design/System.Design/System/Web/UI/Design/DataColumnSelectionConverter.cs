using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	/// <summary>Provides a type converter for a property representing the field name of a bound column field in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
	// Token: 0x02000063 RID: 99
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DataColumnSelectionConverter : TypeConverter
	{
		/// <summary>Indicates whether the specified source type can be converted to the type of the associated control property.</summary>
		/// <returns>true if the converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  implementation that can be used to gain additional context information.</param>
		/// <param name="sourceType">The type to convert from.</param>
		// Token: 0x0600031E RID: 798 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts the specified object to the type of the associated control property.</summary>
		/// <returns>An <see cref="T:System.Object" /> instance that represents the converted object.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  implementation that can be used to gain additional context information.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> object that can be used to support localization features.</param>
		/// <param name="value">The object to convert.</param>
		/// <exception cref="T:System.NotSupportedException">The conversion could not be performed. </exception>
		// Token: 0x0600031F RID: 799 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a list of available values that can be assigned to the associated control property.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> instance containing available values for assignment to the associated control property.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  implementation that can be used to gain additional context information.</param>
		// Token: 0x06000320 RID: 800 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether this converter returns a list containing all possible values that can be assigned to the associated control property.</summary>
		/// <returns>true if this converter returns a list containing all possible values that can be assigned to the associated control property; otherwise false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  implementation that can be used to gain additional context information.</param>
		// Token: 0x06000321 RID: 801 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether this converter returns a set of available values for assignment to a control property, within the specified context.</summary>
		/// <returns>true if this converter returns a standard set of available values for assignment to the associated control property; otherwise false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  implementation that can be used to gain additional context information.</param>
		// Token: 0x06000322 RID: 802 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			throw new NotImplementedException();
		}
	}
}
