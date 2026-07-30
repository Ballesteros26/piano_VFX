using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Data
{
	// Token: 0x020000E7 RID: 231
	internal sealed class RelationshipConverter : ExpandableObjectConverter
	{
		// Token: 0x06000C5A RID: 3162 RVA: 0x00038BFB File Offset: 0x00036DFB
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00038C1C File Offset: 0x00036E1C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(InstanceDescriptor) && value is DataRelation)
			{
				DataRelation dataRelation = (DataRelation)value;
				DataTable table = dataRelation.ParentKey.Table;
				DataTable table2 = dataRelation.ChildKey.Table;
				ConstructorInfo constructorInfo;
				object[] array;
				if (string.IsNullOrEmpty(table.Namespace) && string.IsNullOrEmpty(table2.Namespace))
				{
					constructorInfo = typeof(DataRelation).GetConstructor(new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(string),
						typeof(string[]),
						typeof(string[]),
						typeof(bool)
					});
					array = new object[]
					{
						dataRelation.RelationName,
						dataRelation.ParentKey.Table.TableName,
						dataRelation.ChildKey.Table.TableName,
						dataRelation.ParentColumnNames,
						dataRelation.ChildColumnNames,
						dataRelation.Nested
					};
				}
				else
				{
					constructorInfo = typeof(DataRelation).GetConstructor(new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(string),
						typeof(string),
						typeof(string),
						typeof(string[]),
						typeof(string[]),
						typeof(bool)
					});
					array = new object[]
					{
						dataRelation.RelationName,
						dataRelation.ParentKey.Table.TableName,
						dataRelation.ParentKey.Table.Namespace,
						dataRelation.ChildKey.Table.TableName,
						dataRelation.ChildKey.Table.Namespace,
						dataRelation.ParentColumnNames,
						dataRelation.ChildColumnNames,
						dataRelation.Nested
					};
				}
				return new InstanceDescriptor(constructorInfo, array);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
