using System;

namespace System.Data.Odbc
{
	// Token: 0x0200025F RID: 607
	internal sealed class DbCache
	{
		// Token: 0x06001ACB RID: 6859 RVA: 0x00086BCC File Offset: 0x00084DCC
		internal DbCache(OdbcDataReader record, int count)
		{
			this._count = count;
			this._record = record;
			this._randomaccess = !record.IsBehavior(CommandBehavior.SequentialAccess);
			this._values = new object[count];
			this._isBadValue = new bool[count];
		}

		// Token: 0x170004E2 RID: 1250
		internal object this[int i]
		{
			get
			{
				if (this._isBadValue[i])
				{
					OverflowException ex = (OverflowException)this.Values[i];
					throw new OverflowException(ex.Message, ex);
				}
				return this.Values[i];
			}
			set
			{
				this.Values[i] = value;
				this._isBadValue[i] = false;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x00086C6E File Offset: 0x00084E6E
		internal int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x00086C76 File Offset: 0x00084E76
		internal void InvalidateValue(int i)
		{
			this._isBadValue[i] = true;
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x00086C81 File Offset: 0x00084E81
		internal object[] Values
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x00086C8C File Offset: 0x00084E8C
		internal object AccessIndex(int i)
		{
			object[] values = this.Values;
			if (this._randomaccess)
			{
				for (int j = 0; j < i; j++)
				{
					if (values[j] == null)
					{
						values[j] = this._record.GetValue(j);
					}
				}
			}
			return values[i];
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x00086CCB File Offset: 0x00084ECB
		internal DbSchemaInfo GetSchema(int i)
		{
			if (this._schema == null)
			{
				this._schema = new DbSchemaInfo[this.Count];
			}
			if (this._schema[i] == null)
			{
				this._schema[i] = new DbSchemaInfo();
			}
			return this._schema[i];
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00086D08 File Offset: 0x00084F08
		internal void FlushValues()
		{
			int num = this._values.Length;
			for (int i = 0; i < num; i++)
			{
				this._values[i] = null;
			}
		}

		// Token: 0x04001331 RID: 4913
		private bool[] _isBadValue;

		// Token: 0x04001332 RID: 4914
		private DbSchemaInfo[] _schema;

		// Token: 0x04001333 RID: 4915
		private object[] _values;

		// Token: 0x04001334 RID: 4916
		private OdbcDataReader _record;

		// Token: 0x04001335 RID: 4917
		internal int _count;

		// Token: 0x04001336 RID: 4918
		internal bool _randomaccess = true;
	}
}
