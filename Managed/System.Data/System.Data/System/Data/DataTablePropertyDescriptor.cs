using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000094 RID: 148
	internal sealed class DataTablePropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00028A66 File Offset: 0x00026C66
		public DataTable Table { get; }

		// Token: 0x060008C9 RID: 2249 RVA: 0x00028A6E File Offset: 0x00026C6E
		internal DataTablePropertyDescriptor(DataTable dataTable)
			: base(dataTable.TableName, null)
		{
			this.Table = dataTable;
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00013FD2 File Offset: 0x000121D2
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00017FE8 File Offset: 0x000161E8
		public override Type PropertyType
		{
			get
			{
				return typeof(IBindingList);
			}
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00028A84 File Offset: 0x00026C84
		public override bool Equals(object other)
		{
			return other is DataTablePropertyDescriptor && ((DataTablePropertyDescriptor)other).Table == this.Table;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00028AA3 File Offset: 0x00026CA3
		public override int GetHashCode()
		{
			return this.Table.GetHashCode();
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00028AB0 File Offset: 0x00026CB0
		public override object GetValue(object component)
		{
			return ((DataViewManagerListItemTypeDescriptor)component).GetDataView(this.Table);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00005E03 File Offset: 0x00004003
		public override void ResetValue(object component)
		{
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00005E03 File Offset: 0x00004003
		public override void SetValue(object component, object value)
		{
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}
	}
}
