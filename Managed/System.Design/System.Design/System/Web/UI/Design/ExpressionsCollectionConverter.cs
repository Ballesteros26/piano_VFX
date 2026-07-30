using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	/// <summary>Provides a type converter for expression binding collections.</summary>
	// Token: 0x0200007B RID: 123
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ExpressionsCollectionConverter : TypeConverter
	{
		/// <summary>Converts an expression binding collection to the specified type.</summary>
		/// <returns>If <paramref name="destinationType" /> is not of type <see cref="T:System.String" />, the object produced by the type conversion; otherwise, if <paramref name="destinationType" /> is a string, an empty string ("").</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that indicates the component or control the expression binding collection belongs to. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that can be used to provide additional culture information.</param>
		/// <param name="value">The object to convert. </param>
		/// <param name="destinationType">The type to convert to.</param>
		// Token: 0x060003FA RID: 1018 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			throw new NotImplementedException();
		}
	}
}
