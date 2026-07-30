using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	/// <summary>Provides a type converter for a property representing a Boolean field in a data source schema.</summary>
	// Token: 0x02000069 RID: 105
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class DataSourceBooleanViewSchemaConverter : DataSourceViewSchemaConverter
	{
		/// <summary>Returns a list of available Boolean values that can be assigned to the associated field.</summary>
		/// <returns>A collection of Boolean values.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		// Token: 0x06000341 RID: 833 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			throw new NotImplementedException();
		}
	}
}
