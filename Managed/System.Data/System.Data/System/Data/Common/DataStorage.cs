using System;
using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Numerics;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000337 RID: 823
	internal abstract class DataStorage
	{
		// Token: 0x060025E7 RID: 9703 RVA: 0x000ABD05 File Offset: 0x000A9F05
		protected DataStorage(DataColumn column, Type type, object defaultValue, StorageType storageType)
			: this(column, type, defaultValue, DBNull.Value, false, storageType)
		{
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x000ABD18 File Offset: 0x000A9F18
		protected DataStorage(DataColumn column, Type type, object defaultValue, object nullValue, StorageType storageType)
			: this(column, type, defaultValue, nullValue, false, storageType)
		{
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x000ABD28 File Offset: 0x000A9F28
		protected DataStorage(DataColumn column, Type type, object defaultValue, object nullValue, bool isICloneable, StorageType storageType)
		{
			this._column = column;
			this._table = column.Table;
			this._dataType = type;
			this._storageTypeCode = storageType;
			this._defaultValue = defaultValue;
			this._nullValue = nullValue;
			this._isCloneable = isICloneable;
			this._isCustomDefinedType = DataStorage.IsTypeCustomType(this._storageTypeCode);
			this._isStringType = StorageType.String == this._storageTypeCode || StorageType.SqlString == this._storageTypeCode;
			this._isValueType = DataStorage.DetermineIfValueType(this._storageTypeCode, type);
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060025EA RID: 9706 RVA: 0x000ABDB4 File Offset: 0x000A9FB4
		internal DataSetDateTime DateTimeMode
		{
			get
			{
				return this._column.DateTimeMode;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060025EB RID: 9707 RVA: 0x000ABDC1 File Offset: 0x000A9FC1
		internal IFormatProvider FormatProvider
		{
			get
			{
				return this._table.FormatProvider;
			}
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x000ABDCE File Offset: 0x000A9FCE
		public virtual object Aggregate(int[] recordNos, AggregateType kind)
		{
			if (AggregateType.Count == kind)
			{
				return this.AggregateCount(recordNos);
			}
			return null;
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x000ABDE0 File Offset: 0x000A9FE0
		public object AggregateCount(int[] recordNos)
		{
			int num = 0;
			for (int i = 0; i < recordNos.Length; i++)
			{
				if (!this._dbNullBits.Get(recordNos[i]))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x000ABE18 File Offset: 0x000AA018
		protected int CompareBits(int recordNo1, int recordNo2)
		{
			bool flag = this._dbNullBits.Get(recordNo1);
			bool flag2 = this._dbNullBits.Get(recordNo2);
			if (!(flag ^ flag2))
			{
				return 0;
			}
			if (flag)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x060025EF RID: 9711
		public abstract int Compare(int recordNo1, int recordNo2);

		// Token: 0x060025F0 RID: 9712
		public abstract int CompareValueTo(int recordNo1, object value);

		// Token: 0x060025F1 RID: 9713 RVA: 0x00005DA6 File Offset: 0x00003FA6
		public virtual object ConvertValue(object value)
		{
			return value;
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x000ABE4C File Offset: 0x000AA04C
		protected void CopyBits(int srcRecordNo, int dstRecordNo)
		{
			this._dbNullBits.Set(dstRecordNo, this._dbNullBits.Get(srcRecordNo));
		}

		// Token: 0x060025F3 RID: 9715
		public abstract void Copy(int recordNo1, int recordNo2);

		// Token: 0x060025F4 RID: 9716
		public abstract object Get(int recordNo);

		// Token: 0x060025F5 RID: 9717 RVA: 0x000ABE66 File Offset: 0x000AA066
		protected object GetBits(int recordNo)
		{
			if (this._dbNullBits.Get(recordNo))
			{
				return this._nullValue;
			}
			return this._defaultValue;
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x000ABE83 File Offset: 0x000AA083
		public virtual int GetStringLength(int record)
		{
			return int.MaxValue;
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x000ABE8A File Offset: 0x000AA08A
		protected bool HasValue(int recordNo)
		{
			return !this._dbNullBits.Get(recordNo);
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x000ABE9B File Offset: 0x000AA09B
		public virtual bool IsNull(int recordNo)
		{
			return this._dbNullBits.Get(recordNo);
		}

		// Token: 0x060025F9 RID: 9721
		public abstract void Set(int recordNo, object value);

		// Token: 0x060025FA RID: 9722 RVA: 0x000ABEA9 File Offset: 0x000AA0A9
		protected void SetNullBit(int recordNo, bool flag)
		{
			this._dbNullBits.Set(recordNo, flag);
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x000ABEB8 File Offset: 0x000AA0B8
		public virtual void SetCapacity(int capacity)
		{
			if (this._dbNullBits == null)
			{
				this._dbNullBits = new BitArray(capacity);
				return;
			}
			this._dbNullBits.Length = capacity;
		}

		// Token: 0x060025FC RID: 9724
		public abstract object ConvertXmlToObject(string s);

		// Token: 0x060025FD RID: 9725 RVA: 0x000ABEDB File Offset: 0x000AA0DB
		public virtual object ConvertXmlToObject(XmlReader xmlReader, XmlRootAttribute xmlAttrib)
		{
			return this.ConvertXmlToObject(xmlReader.Value);
		}

		// Token: 0x060025FE RID: 9726
		public abstract string ConvertObjectToXml(object value);

		// Token: 0x060025FF RID: 9727 RVA: 0x000ABEE9 File Offset: 0x000AA0E9
		public virtual void ConvertObjectToXml(object value, XmlWriter xmlWriter, XmlRootAttribute xmlAttrib)
		{
			xmlWriter.WriteString(this.ConvertObjectToXml(value));
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x000ABEF8 File Offset: 0x000AA0F8
		public static DataStorage CreateStorage(DataColumn column, Type dataType, StorageType typeCode)
		{
			if (typeCode != StorageType.Empty || !(null != dataType))
			{
				switch (typeCode)
				{
				case StorageType.Empty:
					throw ExceptionBuilder.InvalidStorageType(TypeCode.Empty);
				case StorageType.DBNull:
					throw ExceptionBuilder.InvalidStorageType(TypeCode.DBNull);
				case StorageType.Boolean:
					return new BooleanStorage(column);
				case StorageType.Char:
					return new CharStorage(column);
				case StorageType.SByte:
					return new SByteStorage(column);
				case StorageType.Byte:
					return new ByteStorage(column);
				case StorageType.Int16:
					return new Int16Storage(column);
				case StorageType.UInt16:
					return new UInt16Storage(column);
				case StorageType.Int32:
					return new Int32Storage(column);
				case StorageType.UInt32:
					return new UInt32Storage(column);
				case StorageType.Int64:
					return new Int64Storage(column);
				case StorageType.UInt64:
					return new UInt64Storage(column);
				case StorageType.Single:
					return new SingleStorage(column);
				case StorageType.Double:
					return new DoubleStorage(column);
				case StorageType.Decimal:
					return new DecimalStorage(column);
				case StorageType.DateTime:
					return new DateTimeStorage(column);
				case StorageType.TimeSpan:
					return new TimeSpanStorage(column);
				case StorageType.String:
					return new StringStorage(column);
				case StorageType.Guid:
					return new ObjectStorage(column, dataType);
				case StorageType.ByteArray:
					return new ObjectStorage(column, dataType);
				case StorageType.CharArray:
					return new ObjectStorage(column, dataType);
				case StorageType.Type:
					return new ObjectStorage(column, dataType);
				case StorageType.DateTimeOffset:
					return new DateTimeOffsetStorage(column);
				case StorageType.BigInteger:
					return new BigIntegerStorage(column);
				case StorageType.Uri:
					return new ObjectStorage(column, dataType);
				case StorageType.SqlBinary:
					return new SqlBinaryStorage(column);
				case StorageType.SqlBoolean:
					return new SqlBooleanStorage(column);
				case StorageType.SqlByte:
					return new SqlByteStorage(column);
				case StorageType.SqlBytes:
					return new SqlBytesStorage(column);
				case StorageType.SqlChars:
					return new SqlCharsStorage(column);
				case StorageType.SqlDateTime:
					return new SqlDateTimeStorage(column);
				case StorageType.SqlDecimal:
					return new SqlDecimalStorage(column);
				case StorageType.SqlDouble:
					return new SqlDoubleStorage(column);
				case StorageType.SqlGuid:
					return new SqlGuidStorage(column);
				case StorageType.SqlInt16:
					return new SqlInt16Storage(column);
				case StorageType.SqlInt32:
					return new SqlInt32Storage(column);
				case StorageType.SqlInt64:
					return new SqlInt64Storage(column);
				case StorageType.SqlMoney:
					return new SqlMoneyStorage(column);
				case StorageType.SqlSingle:
					return new SqlSingleStorage(column);
				case StorageType.SqlString:
					return new SqlStringStorage(column);
				}
				return new ObjectStorage(column, dataType);
			}
			if (typeof(INullable).IsAssignableFrom(dataType))
			{
				return new SqlUdtStorage(column, dataType);
			}
			return new ObjectStorage(column, dataType);
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x000AC104 File Offset: 0x000AA304
		internal static StorageType GetStorageType(Type dataType)
		{
			for (int i = 0; i < DataStorage.s_storageClassType.Length; i++)
			{
				if (dataType == DataStorage.s_storageClassType[i])
				{
					return (StorageType)i;
				}
			}
			TypeCode typeCode = Type.GetTypeCode(dataType);
			if (TypeCode.Object != typeCode)
			{
				return (StorageType)typeCode;
			}
			return StorageType.Empty;
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x000AC142 File Offset: 0x000AA342
		internal static Type GetTypeStorage(StorageType storageType)
		{
			return DataStorage.s_storageClassType[(int)storageType];
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x000AC14B File Offset: 0x000AA34B
		internal static bool IsTypeCustomType(Type type)
		{
			return DataStorage.IsTypeCustomType(DataStorage.GetStorageType(type));
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x000AC158 File Offset: 0x000AA358
		internal static bool IsTypeCustomType(StorageType typeCode)
		{
			return StorageType.Object == typeCode || typeCode == StorageType.Empty || StorageType.CharArray == typeCode;
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x000AC168 File Offset: 0x000AA368
		internal static bool IsSqlType(StorageType storageType)
		{
			return StorageType.SqlBinary <= storageType;
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x000AC174 File Offset: 0x000AA374
		public static bool IsSqlType(Type dataType)
		{
			for (int i = 26; i < DataStorage.s_storageClassType.Length; i++)
			{
				if (dataType == DataStorage.s_storageClassType[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x000AC1A8 File Offset: 0x000AA3A8
		private static bool DetermineIfValueType(StorageType typeCode, Type dataType)
		{
			bool flag;
			switch (typeCode)
			{
			case StorageType.Boolean:
			case StorageType.Char:
			case StorageType.SByte:
			case StorageType.Byte:
			case StorageType.Int16:
			case StorageType.UInt16:
			case StorageType.Int32:
			case StorageType.UInt32:
			case StorageType.Int64:
			case StorageType.UInt64:
			case StorageType.Single:
			case StorageType.Double:
			case StorageType.Decimal:
			case StorageType.DateTime:
			case StorageType.TimeSpan:
			case StorageType.Guid:
			case StorageType.DateTimeOffset:
			case StorageType.BigInteger:
			case StorageType.SqlBinary:
			case StorageType.SqlBoolean:
			case StorageType.SqlByte:
			case StorageType.SqlDateTime:
			case StorageType.SqlDecimal:
			case StorageType.SqlDouble:
			case StorageType.SqlGuid:
			case StorageType.SqlInt16:
			case StorageType.SqlInt32:
			case StorageType.SqlInt64:
			case StorageType.SqlMoney:
			case StorageType.SqlSingle:
			case StorageType.SqlString:
				flag = true;
				break;
			case StorageType.String:
			case StorageType.ByteArray:
			case StorageType.CharArray:
			case StorageType.Type:
			case StorageType.Uri:
			case StorageType.SqlBytes:
			case StorageType.SqlChars:
				flag = false;
				break;
			default:
				flag = dataType.IsValueType;
				break;
			}
			return flag;
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x000AC268 File Offset: 0x000AA468
		internal static void ImplementsInterfaces(StorageType typeCode, Type dataType, out bool sqlType, out bool nullable, out bool xmlSerializable, out bool changeTracking, out bool revertibleChangeTracking)
		{
			if (DataStorage.IsSqlType(typeCode))
			{
				sqlType = true;
				nullable = true;
				changeTracking = false;
				revertibleChangeTracking = false;
				xmlSerializable = true;
				return;
			}
			if (typeCode != StorageType.Empty)
			{
				sqlType = false;
				nullable = false;
				changeTracking = false;
				revertibleChangeTracking = false;
				xmlSerializable = false;
				return;
			}
			Tuple<bool, bool, bool, bool> orAdd = DataStorage.s_typeImplementsInterface.GetOrAdd(dataType, DataStorage.s_inspectTypeForInterfaces);
			sqlType = false;
			nullable = orAdd.Item1;
			changeTracking = orAdd.Item2;
			revertibleChangeTracking = orAdd.Item3;
			xmlSerializable = orAdd.Item4;
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x000AC2E0 File Offset: 0x000AA4E0
		private static Tuple<bool, bool, bool, bool> InspectTypeForInterfaces(Type dataType)
		{
			return new Tuple<bool, bool, bool, bool>(typeof(INullable).IsAssignableFrom(dataType), typeof(IChangeTracking).IsAssignableFrom(dataType), typeof(IRevertibleChangeTracking).IsAssignableFrom(dataType), typeof(IXmlSerializable).IsAssignableFrom(dataType));
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x000AC332 File Offset: 0x000AA532
		internal static bool ImplementsINullableValue(StorageType typeCode, Type dataType)
		{
			return typeCode == StorageType.Empty && dataType.IsGenericType && dataType.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x000AC356 File Offset: 0x000AA556
		public static bool IsObjectNull(object value)
		{
			return value == null || DBNull.Value == value || DataStorage.IsObjectSqlNull(value);
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x000AC36C File Offset: 0x000AA56C
		public static bool IsObjectSqlNull(object value)
		{
			INullable nullable = value as INullable;
			return nullable != null && nullable.IsNull;
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x000AC38B File Offset: 0x000AA58B
		internal object GetEmptyStorageInternal(int recordCount)
		{
			return this.GetEmptyStorage(recordCount);
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x000AC394 File Offset: 0x000AA594
		internal void CopyValueInternal(int record, object store, BitArray nullbits, int storeIndex)
		{
			this.CopyValue(record, store, nullbits, storeIndex);
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x000AC3A1 File Offset: 0x000AA5A1
		internal void SetStorageInternal(object store, BitArray nullbits)
		{
			this.SetStorage(store, nullbits);
		}

		// Token: 0x06002610 RID: 9744
		protected abstract object GetEmptyStorage(int recordCount);

		// Token: 0x06002611 RID: 9745
		protected abstract void CopyValue(int record, object store, BitArray nullbits, int storeIndex);

		// Token: 0x06002612 RID: 9746
		protected abstract void SetStorage(object store, BitArray nullbits);

		// Token: 0x06002613 RID: 9747 RVA: 0x000AC3AB File Offset: 0x000AA5AB
		protected void SetNullStorage(BitArray nullbits)
		{
			this._dbNullBits = nullbits;
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x000AC3B4 File Offset: 0x000AA5B4
		internal static Type GetType(string value)
		{
			Type type = Type.GetType(value);
			if (null == type && "System.Numerics.BigInteger" == value)
			{
				type = typeof(BigInteger);
			}
			ObjectStorage.VerifyIDynamicMetaObjectProvider(type);
			return type;
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x000AC3F0 File Offset: 0x000AA5F0
		internal static string GetQualifiedName(Type type)
		{
			ObjectStorage.VerifyIDynamicMetaObjectProvider(type);
			return type.AssemblyQualifiedName;
		}

		// Token: 0x0400187D RID: 6269
		private static readonly Type[] s_storageClassType = new Type[]
		{
			null,
			typeof(object),
			typeof(DBNull),
			typeof(bool),
			typeof(char),
			typeof(sbyte),
			typeof(byte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(DateTime),
			typeof(TimeSpan),
			typeof(string),
			typeof(Guid),
			typeof(byte[]),
			typeof(char[]),
			typeof(Type),
			typeof(DateTimeOffset),
			typeof(BigInteger),
			typeof(Uri),
			typeof(SqlBinary),
			typeof(SqlBoolean),
			typeof(SqlByte),
			typeof(SqlBytes),
			typeof(SqlChars),
			typeof(SqlDateTime),
			typeof(SqlDecimal),
			typeof(SqlDouble),
			typeof(SqlGuid),
			typeof(SqlInt16),
			typeof(SqlInt32),
			typeof(SqlInt64),
			typeof(SqlMoney),
			typeof(SqlSingle),
			typeof(SqlString)
		};

		// Token: 0x0400187E RID: 6270
		internal readonly DataColumn _column;

		// Token: 0x0400187F RID: 6271
		internal readonly DataTable _table;

		// Token: 0x04001880 RID: 6272
		internal readonly Type _dataType;

		// Token: 0x04001881 RID: 6273
		internal readonly StorageType _storageTypeCode;

		// Token: 0x04001882 RID: 6274
		private BitArray _dbNullBits;

		// Token: 0x04001883 RID: 6275
		private readonly object _defaultValue;

		// Token: 0x04001884 RID: 6276
		internal readonly object _nullValue;

		// Token: 0x04001885 RID: 6277
		internal readonly bool _isCloneable;

		// Token: 0x04001886 RID: 6278
		internal readonly bool _isCustomDefinedType;

		// Token: 0x04001887 RID: 6279
		internal readonly bool _isStringType;

		// Token: 0x04001888 RID: 6280
		internal readonly bool _isValueType;

		// Token: 0x04001889 RID: 6281
		private static readonly Func<Type, Tuple<bool, bool, bool, bool>> s_inspectTypeForInterfaces = new Func<Type, Tuple<bool, bool, bool, bool>>(DataStorage.InspectTypeForInterfaces);

		// Token: 0x0400188A RID: 6282
		private static readonly ConcurrentDictionary<Type, Tuple<bool, bool, bool, bool>> s_typeImplementsInterface = new ConcurrentDictionary<Type, Tuple<bool, bool, bool, bool>>();
	}
}
