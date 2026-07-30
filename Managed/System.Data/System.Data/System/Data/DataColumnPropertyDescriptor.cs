using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data
{
	// Token: 0x02000066 RID: 102
	internal sealed class DataColumnPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x060003F4 RID: 1012 RVA: 0x00013F56 File Offset: 0x00012156
		internal DataColumnPropertyDescriptor(DataColumn dataColumn)
			: base(dataColumn.ColumnName, null)
		{
			this.Column = dataColumn;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00013F6C File Offset: 0x0001216C
		public override AttributeCollection Attributes
		{
			get
			{
				if (typeof(IList).IsAssignableFrom(this.PropertyType))
				{
					Attribute[] array = new Attribute[base.Attributes.Count + 1];
					base.Attributes.CopyTo(array, 0);
					array[array.Length - 1] = new ListBindableAttribute(false);
					return new AttributeCollection(array);
				}
				return base.Attributes;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00013FCA File Offset: 0x000121CA
		internal DataColumn Column { get; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00013FD2 File Offset: 0x000121D2
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00013FDE File Offset: 0x000121DE
		public override bool IsReadOnly
		{
			get
			{
				return this.Column.ReadOnly;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00013FEB File Offset: 0x000121EB
		public override Type PropertyType
		{
			get
			{
				return this.Column.DataType;
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00013FF8 File Offset: 0x000121F8
		public override bool Equals(object other)
		{
			return other is DataColumnPropertyDescriptor && ((DataColumnPropertyDescriptor)other).Column == this.Column;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00014017 File Offset: 0x00012217
		public override int GetHashCode()
		{
			return this.Column.GetHashCode();
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00014024 File Offset: 0x00012224
		public override bool CanResetValue(object component)
		{
			DataRowView dataRowView = (DataRowView)component;
			if (!this.Column.IsSqlType)
			{
				return dataRowView.GetColumnValue(this.Column) != DBNull.Value;
			}
			return !DataStorage.IsObjectNull(dataRowView.GetColumnValue(this.Column));
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00014070 File Offset: 0x00012270
		public override object GetValue(object component)
		{
			return ((DataRowView)component).GetColumnValue(this.Column);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00014083 File Offset: 0x00012283
		public override void ResetValue(object component)
		{
			((DataRowView)component).SetColumnValue(this.Column, DBNull.Value);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001409B File Offset: 0x0001229B
		public override void SetValue(object component, object value)
		{
			((DataRowView)component).SetColumnValue(this.Column, value);
			this.OnValueChanged(component, EventArgs.Empty);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000061D5 File Offset: 0x000043D5
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x000140BB File Offset: 0x000122BB
		public override bool IsBrowsable
		{
			get
			{
				return this.Column.ColumnMapping != MappingType.Hidden && base.IsBrowsable;
			}
		}
	}
}
