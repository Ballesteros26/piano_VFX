using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000030 RID: 48
	[Editor("Microsoft.VSDesigner.Data.Design.DBParametersEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ListBindable(false)]
	public sealed class SqliteParameterCollection : DbParameterCollection
	{
		// Token: 0x06000259 RID: 601 RVA: 0x0000D788 File Offset: 0x0000B988
		internal SqliteParameterCollection(SqliteCommand cmd)
		{
			this._command = cmd;
			this._parameterList = new List<SqliteParameter>();
			this._unboundFlag = true;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000D7A9 File Offset: 0x0000B9A9
		public override bool IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000D7AC File Offset: 0x0000B9AC
		public override bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000D7AF File Offset: 0x0000B9AF
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000D7B2 File Offset: 0x0000B9B2
		public override object SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000D7B5 File Offset: 0x0000B9B5
		public override IEnumerator GetEnumerator()
		{
			return this._parameterList.GetEnumerator();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000D7C8 File Offset: 0x0000B9C8
		public SqliteParameter Add(string parameterName, DbType parameterType, int parameterSize, string sourceColumn)
		{
			SqliteParameter sqliteParameter = new SqliteParameter(parameterName, parameterType, parameterSize, sourceColumn);
			this.Add(sqliteParameter);
			return sqliteParameter;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		public SqliteParameter Add(string parameterName, DbType parameterType, int parameterSize)
		{
			SqliteParameter sqliteParameter = new SqliteParameter(parameterName, parameterType, parameterSize);
			this.Add(sqliteParameter);
			return sqliteParameter;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000D80C File Offset: 0x0000BA0C
		public SqliteParameter Add(string parameterName, DbType parameterType)
		{
			SqliteParameter sqliteParameter = new SqliteParameter(parameterName, parameterType);
			this.Add(sqliteParameter);
			return sqliteParameter;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000D82C File Offset: 0x0000BA2C
		public int Add(SqliteParameter parameter)
		{
			int num = -1;
			if (!string.IsNullOrEmpty(parameter.ParameterName))
			{
				num = this.IndexOf(parameter.ParameterName);
			}
			if (num == -1)
			{
				num = this._parameterList.Count;
				this._parameterList.Add(parameter);
			}
			this.SetParameter(num, parameter);
			return num;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000D87A File Offset: 0x0000BA7A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			return this.Add((SqliteParameter)value);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000D888 File Offset: 0x0000BA88
		public SqliteParameter AddWithValue(string parameterName, object value)
		{
			SqliteParameter sqliteParameter = new SqliteParameter(parameterName, value);
			this.Add(sqliteParameter);
			return sqliteParameter;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
		public void AddRange(SqliteParameter[] values)
		{
			int num = values.Length;
			for (int i = 0; i < num; i++)
			{
				this.Add(values[i]);
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000D8D0 File Offset: 0x0000BAD0
		public override void AddRange(Array values)
		{
			int length = values.Length;
			for (int i = 0; i < length; i++)
			{
				this.Add((SqliteParameter)values.GetValue(i));
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000D903 File Offset: 0x0000BB03
		public override void Clear()
		{
			this._unboundFlag = true;
			this._parameterList.Clear();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000D917 File Offset: 0x0000BB17
		public override bool Contains(string parameterName)
		{
			return this.IndexOf(parameterName) != -1;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000D926 File Offset: 0x0000BB26
		public override bool Contains(object value)
		{
			return this._parameterList.Contains((SqliteParameter)value);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000D939 File Offset: 0x0000BB39
		public override void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000D940 File Offset: 0x0000BB40
		public override int Count
		{
			get
			{
				return this._parameterList.Count;
			}
		}

		// Token: 0x17000052 RID: 82
		public SqliteParameter this[string parameterName]
		{
			get
			{
				return (SqliteParameter)this.GetParameter(parameterName);
			}
			set
			{
				this.SetParameter(parameterName, value);
			}
		}

		// Token: 0x17000053 RID: 83
		public SqliteParameter this[int index]
		{
			get
			{
				return (SqliteParameter)this.GetParameter(index);
			}
			set
			{
				this.SetParameter(index, value);
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000D97D File Offset: 0x0000BB7D
		protected override DbParameter GetParameter(string parameterName)
		{
			return this.GetParameter(this.IndexOf(parameterName));
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000D98C File Offset: 0x0000BB8C
		protected override DbParameter GetParameter(int index)
		{
			return this._parameterList[index];
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000D99C File Offset: 0x0000BB9C
		public override int IndexOf(string parameterName)
		{
			int count = this._parameterList.Count;
			for (int i = 0; i < count; i++)
			{
				if (string.Compare(parameterName, this._parameterList[i].ParameterName, true, CultureInfo.InvariantCulture) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000D9E3 File Offset: 0x0000BBE3
		public override int IndexOf(object value)
		{
			return this._parameterList.IndexOf((SqliteParameter)value);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000D9F6 File Offset: 0x0000BBF6
		public override void Insert(int index, object value)
		{
			this._unboundFlag = true;
			this._parameterList.Insert(index, (SqliteParameter)value);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000DA11 File Offset: 0x0000BC11
		public override void Remove(object value)
		{
			this._unboundFlag = true;
			this._parameterList.Remove((SqliteParameter)value);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000DA2C File Offset: 0x0000BC2C
		public override void RemoveAt(string parameterName)
		{
			this.RemoveAt(this.IndexOf(parameterName));
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000DA3B File Offset: 0x0000BC3B
		public override void RemoveAt(int index)
		{
			this._unboundFlag = true;
			this._parameterList.RemoveAt(index);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000DA50 File Offset: 0x0000BC50
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			this.SetParameter(this.IndexOf(parameterName), value);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000DA60 File Offset: 0x0000BC60
		protected override void SetParameter(int index, DbParameter value)
		{
			this._unboundFlag = true;
			this._parameterList[index] = (SqliteParameter)value;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000DA7B File Offset: 0x0000BC7B
		internal void Unbind()
		{
			this._unboundFlag = true;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000DA84 File Offset: 0x0000BC84
		internal void MapParameters(SqliteStatement activeStatement)
		{
			if (!this._unboundFlag || this._parameterList.Count == 0 || this._command._statementList == null)
			{
				return;
			}
			int num = 0;
			int num2 = -1;
			foreach (SqliteParameter sqliteParameter in this._parameterList)
			{
				num2++;
				string text = sqliteParameter.ParameterName;
				if (text == null)
				{
					text = string.Format(CultureInfo.InvariantCulture, ";{0}", num);
					num++;
				}
				bool flag = false;
				int num3;
				if (activeStatement == null)
				{
					num3 = this._command._statementList.Count;
				}
				else
				{
					num3 = 1;
				}
				SqliteStatement sqliteStatement = activeStatement;
				for (int i = 0; i < num3; i++)
				{
					flag = false;
					if (sqliteStatement == null)
					{
						sqliteStatement = this._command._statementList[i];
					}
					if (sqliteStatement._paramNames != null && sqliteStatement.MapParameter(text, sqliteParameter))
					{
						flag = true;
					}
					sqliteStatement = null;
				}
				if (!flag)
				{
					text = string.Format(CultureInfo.InvariantCulture, ";{0}", num2);
					sqliteStatement = activeStatement;
					for (int i = 0; i < num3; i++)
					{
						if (sqliteStatement == null)
						{
							sqliteStatement = this._command._statementList[i];
						}
						if (sqliteStatement._paramNames == null || sqliteStatement.MapParameter(text, sqliteParameter))
						{
						}
						sqliteStatement = null;
					}
				}
			}
			if (activeStatement == null)
			{
				this._unboundFlag = false;
			}
		}

		// Token: 0x040000F8 RID: 248
		private SqliteCommand _command;

		// Token: 0x040000F9 RID: 249
		private List<SqliteParameter> _parameterList;

		// Token: 0x040000FA RID: 250
		private bool _unboundFlag;
	}
}
