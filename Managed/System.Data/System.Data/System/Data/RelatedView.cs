using System;

namespace System.Data
{
	// Token: 0x020000E6 RID: 230
	internal sealed class RelatedView : DataView, IFilter
	{
		// Token: 0x06000C50 RID: 3152 RVA: 0x0003895C File Offset: 0x00036B5C
		public RelatedView(DataColumn[] columns, object[] values)
			: base(columns[0].Table, false)
		{
			if (values == null)
			{
				throw ExceptionBuilder.ArgumentNull("values");
			}
			this._parentRowView = null;
			this._parentKey = null;
			this._childKey = new DataKey(columns, true);
			this._filterValues = values;
			base.ResetRowViewCache();
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000389B3 File Offset: 0x00036BB3
		public RelatedView(DataRowView parentRowView, DataKey parentKey, DataColumn[] childKeyColumns)
			: base(childKeyColumns[0].Table, false)
		{
			this._filterValues = null;
			this._parentRowView = parentRowView;
			this._parentKey = new DataKey?(parentKey);
			this._childKey = new DataKey(childKeyColumns, true);
			base.ResetRowViewCache();
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x000389F4 File Offset: 0x00036BF4
		private object[] GetParentValues()
		{
			if (this._filterValues != null)
			{
				return this._filterValues;
			}
			if (!this._parentRowView.HasRecord())
			{
				return null;
			}
			return this._parentKey.Value.GetKeyValues(this._parentRowView.GetRecord());
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00038A40 File Offset: 0x00036C40
		public bool Invoke(DataRow row, DataRowVersion version)
		{
			object[] parentValues = this.GetParentValues();
			if (parentValues == null)
			{
				return false;
			}
			object[] keyValues = row.GetKeyValues(this._childKey, version);
			bool flag = true;
			if (keyValues.Length != parentValues.Length)
			{
				flag = false;
			}
			else
			{
				for (int i = 0; i < keyValues.Length; i++)
				{
					if (!keyValues[i].Equals(parentValues[i]))
					{
						flag = false;
						break;
					}
				}
			}
			IFilter filter = base.GetFilter();
			if (filter != null)
			{
				flag &= filter.Invoke(row, version);
			}
			return flag;
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00005D82 File Offset: 0x00003F82
		internal override IFilter GetFilter()
		{
			return this;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00038AB0 File Offset: 0x00036CB0
		public override DataRowView AddNew()
		{
			DataRowView dataRowView = base.AddNew();
			dataRowView.Row.SetKeyValues(this._childKey, this.GetParentValues());
			return dataRowView;
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00038ACF File Offset: 0x00036CCF
		internal override void SetIndex(string newSort, DataViewRowState newRowStates, IFilter newRowFilter)
		{
			base.SetIndex2(newSort, newRowStates, newRowFilter, false);
			base.Reset();
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00038AE4 File Offset: 0x00036CE4
		public override bool Equals(DataView dv)
		{
			RelatedView relatedView = dv as RelatedView;
			if (relatedView == null)
			{
				return false;
			}
			if (!base.Equals(dv))
			{
				return false;
			}
			if (this._filterValues != null)
			{
				return this.CompareArray(this._childKey.ColumnsReference, relatedView._childKey.ColumnsReference) && this.CompareArray(this._filterValues, relatedView._filterValues);
			}
			return relatedView._filterValues == null && (this.CompareArray(this._childKey.ColumnsReference, relatedView._childKey.ColumnsReference) && this.CompareArray(this._parentKey.Value.ColumnsReference, this._parentKey.Value.ColumnsReference)) && this._parentRowView.Equals(relatedView._parentRowView);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00038BC0 File Offset: 0x00036DC0
		private bool CompareArray(object[] value1, object[] value2)
		{
			if (value1 == null || value2 == null)
			{
				return value1 == value2;
			}
			if (value1.Length != value2.Length)
			{
				return false;
			}
			for (int i = 0; i < value1.Length; i++)
			{
				if (value1[i] != value2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000835 RID: 2101
		private readonly DataKey? _parentKey;

		// Token: 0x04000836 RID: 2102
		private readonly DataKey _childKey;

		// Token: 0x04000837 RID: 2103
		private readonly DataRowView _parentRowView;

		// Token: 0x04000838 RID: 2104
		private readonly object[] _filterValues;
	}
}
