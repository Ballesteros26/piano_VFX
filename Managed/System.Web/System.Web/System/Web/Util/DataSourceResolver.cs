using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.Util
{
	// Token: 0x0200013A RID: 314
	internal class DataSourceResolver
	{
		// Token: 0x06000E72 RID: 3698 RVA: 0x00002050 File Offset: 0x00000250
		private DataSourceResolver()
		{
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x000277F0 File Offset: 0x000259F0
		public static IEnumerable ResolveDataSource(object o, string data_member)
		{
			IEnumerable enumerable = o as IEnumerable;
			if (enumerable != null)
			{
				return enumerable;
			}
			IListSource listSource = o as IListSource;
			if (listSource == null)
			{
				return null;
			}
			IList list = listSource.GetList();
			if (!listSource.ContainsListCollection)
			{
				return list;
			}
			ITypedList typedList = list as ITypedList;
			if (typedList == null)
			{
				return null;
			}
			PropertyDescriptorCollection itemProperties = typedList.GetItemProperties(new PropertyDescriptor[0]);
			if (itemProperties == null || itemProperties.Count == 0)
			{
				throw new HttpException("The selected data source did not contain any data members to bind to");
			}
			PropertyDescriptor propertyDescriptor = ((data_member == "") ? itemProperties[0] : itemProperties.Find(data_member, true));
			if (propertyDescriptor != null)
			{
				enumerable = propertyDescriptor.GetValue(list[0]) as IEnumerable;
			}
			if (enumerable == null)
			{
				throw new HttpException("A list corresponding to the selected DataMember was not found");
			}
			return enumerable;
		}
	}
}
