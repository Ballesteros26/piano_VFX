using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x0200009D RID: 157
	internal sealed class DataViewManagerListItemTypeDescriptor : ICustomTypeDescriptor
	{
		// Token: 0x060009D6 RID: 2518 RVA: 0x0002CC6A File Offset: 0x0002AE6A
		internal DataViewManagerListItemTypeDescriptor(DataViewManager dataViewManager)
		{
			this._dataViewManager = dataViewManager;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0002CC79 File Offset: 0x0002AE79
		internal void Reset()
		{
			this._propsCollection = null;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0002CC82 File Offset: 0x0002AE82
		internal DataView GetDataView(DataTable table)
		{
			DataView dataView = new DataView(table);
			dataView.SetDataViewManager(this._dataViewManager);
			return dataView;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0001A01E File Offset: 0x0001821E
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00004526 File Offset: 0x00002726
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00004526 File Offset: 0x00002726
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00004526 File Offset: 0x00002726
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00004526 File Offset: 0x00002726
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00004526 File Offset: 0x00002726
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00004526 File Offset: 0x00002726
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0001A026 File Offset: 0x00018226
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0001A026 File Offset: 0x00018226
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0001A02E File Offset: 0x0001822E
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0002CC98 File Offset: 0x0002AE98
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (this._propsCollection == null)
			{
				PropertyDescriptor[] array = null;
				DataSet dataSet = this._dataViewManager.DataSet;
				if (dataSet != null)
				{
					int count = dataSet.Tables.Count;
					array = new PropertyDescriptor[count];
					for (int i = 0; i < count; i++)
					{
						array[i] = new DataTablePropertyDescriptor(dataSet.Tables[i]);
					}
				}
				this._propsCollection = new PropertyDescriptorCollection(array);
			}
			return this._propsCollection;
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00005D82 File Offset: 0x00003F82
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04000671 RID: 1649
		private DataViewManager _dataViewManager;

		// Token: 0x04000672 RID: 1650
		private PropertyDescriptorCollection _propsCollection;
	}
}
