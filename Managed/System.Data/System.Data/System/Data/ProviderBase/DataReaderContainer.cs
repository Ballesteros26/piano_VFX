using System;
using System.Data.Common;

namespace System.Data.ProviderBase
{
	// Token: 0x020002FB RID: 763
	internal abstract class DataReaderContainer
	{
		// Token: 0x060021D4 RID: 8660 RVA: 0x0009DD24 File Offset: 0x0009BF24
		internal static DataReaderContainer Create(IDataReader dataReader, bool returnProviderSpecificTypes)
		{
			if (returnProviderSpecificTypes)
			{
				DbDataReader dbDataReader = dataReader as DbDataReader;
				if (dbDataReader != null)
				{
					return new DataReaderContainer.ProviderSpecificDataReader(dataReader, dbDataReader);
				}
			}
			return new DataReaderContainer.CommonLanguageSubsetDataReader(dataReader);
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x0009DD4C File Offset: 0x0009BF4C
		protected DataReaderContainer(IDataReader dataReader)
		{
			this._dataReader = dataReader;
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060021D6 RID: 8662 RVA: 0x0009DD5B File Offset: 0x0009BF5B
		internal int FieldCount
		{
			get
			{
				return this._fieldCount;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060021D7 RID: 8663
		internal abstract bool ReturnProviderSpecificTypes { get; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060021D8 RID: 8664
		protected abstract int VisibleFieldCount { get; }

		// Token: 0x060021D9 RID: 8665
		internal abstract Type GetFieldType(int ordinal);

		// Token: 0x060021DA RID: 8666
		internal abstract object GetValue(int ordinal);

		// Token: 0x060021DB RID: 8667
		internal abstract int GetValues(object[] values);

		// Token: 0x060021DC RID: 8668 RVA: 0x0009DD64 File Offset: 0x0009BF64
		internal string GetName(int ordinal)
		{
			string name = this._dataReader.GetName(ordinal);
			if (name == null)
			{
				return "";
			}
			return name;
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x0009DD88 File Offset: 0x0009BF88
		internal DataTable GetSchemaTable()
		{
			return this._dataReader.GetSchemaTable();
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x0009DD95 File Offset: 0x0009BF95
		internal bool NextResult()
		{
			this._fieldCount = 0;
			if (this._dataReader.NextResult())
			{
				this._fieldCount = this.VisibleFieldCount;
				return true;
			}
			return false;
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x0009DDBA File Offset: 0x0009BFBA
		internal bool Read()
		{
			return this._dataReader.Read();
		}

		// Token: 0x040016B3 RID: 5811
		protected readonly IDataReader _dataReader;

		// Token: 0x040016B4 RID: 5812
		protected int _fieldCount;

		// Token: 0x020002FC RID: 764
		private sealed class ProviderSpecificDataReader : DataReaderContainer
		{
			// Token: 0x060021E0 RID: 8672 RVA: 0x0009DDC7 File Offset: 0x0009BFC7
			internal ProviderSpecificDataReader(IDataReader dataReader, DbDataReader dbDataReader)
				: base(dataReader)
			{
				this._providerSpecificDataReader = dbDataReader;
				this._fieldCount = this.VisibleFieldCount;
			}

			// Token: 0x170005D6 RID: 1494
			// (get) Token: 0x060021E1 RID: 8673 RVA: 0x0000EF2B File Offset: 0x0000D12B
			internal override bool ReturnProviderSpecificTypes
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170005D7 RID: 1495
			// (get) Token: 0x060021E2 RID: 8674 RVA: 0x0009DDE4 File Offset: 0x0009BFE4
			protected override int VisibleFieldCount
			{
				get
				{
					int visibleFieldCount = this._providerSpecificDataReader.VisibleFieldCount;
					if (0 > visibleFieldCount)
					{
						return 0;
					}
					return visibleFieldCount;
				}
			}

			// Token: 0x060021E3 RID: 8675 RVA: 0x0009DE04 File Offset: 0x0009C004
			internal override Type GetFieldType(int ordinal)
			{
				return this._providerSpecificDataReader.GetProviderSpecificFieldType(ordinal);
			}

			// Token: 0x060021E4 RID: 8676 RVA: 0x0009DE12 File Offset: 0x0009C012
			internal override object GetValue(int ordinal)
			{
				return this._providerSpecificDataReader.GetProviderSpecificValue(ordinal);
			}

			// Token: 0x060021E5 RID: 8677 RVA: 0x0009DE20 File Offset: 0x0009C020
			internal override int GetValues(object[] values)
			{
				return this._providerSpecificDataReader.GetProviderSpecificValues(values);
			}

			// Token: 0x040016B5 RID: 5813
			private DbDataReader _providerSpecificDataReader;
		}

		// Token: 0x020002FD RID: 765
		private sealed class CommonLanguageSubsetDataReader : DataReaderContainer
		{
			// Token: 0x060021E6 RID: 8678 RVA: 0x0009DE2E File Offset: 0x0009C02E
			internal CommonLanguageSubsetDataReader(IDataReader dataReader)
				: base(dataReader)
			{
				this._fieldCount = this.VisibleFieldCount;
			}

			// Token: 0x170005D8 RID: 1496
			// (get) Token: 0x060021E7 RID: 8679 RVA: 0x000061D5 File Offset: 0x000043D5
			internal override bool ReturnProviderSpecificTypes
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170005D9 RID: 1497
			// (get) Token: 0x060021E8 RID: 8680 RVA: 0x0009DE44 File Offset: 0x0009C044
			protected override int VisibleFieldCount
			{
				get
				{
					int fieldCount = this._dataReader.FieldCount;
					if (0 > fieldCount)
					{
						return 0;
					}
					return fieldCount;
				}
			}

			// Token: 0x060021E9 RID: 8681 RVA: 0x0009DE64 File Offset: 0x0009C064
			internal override Type GetFieldType(int ordinal)
			{
				return this._dataReader.GetFieldType(ordinal);
			}

			// Token: 0x060021EA RID: 8682 RVA: 0x0009DE72 File Offset: 0x0009C072
			internal override object GetValue(int ordinal)
			{
				return this._dataReader.GetValue(ordinal);
			}

			// Token: 0x060021EB RID: 8683 RVA: 0x0009DE80 File Offset: 0x0009C080
			internal override int GetValues(object[] values)
			{
				return this._dataReader.GetValues(values);
			}
		}
	}
}
