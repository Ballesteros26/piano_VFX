using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Data.Odbc
{
	/// <summary>Represents a parameter to an <see cref="T:System.Data.Odbc.OdbcCommand" /> and optionally, its mapping to a <see cref="T:System.Data.DataColumn" />. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002A9 RID: 681
	public sealed class OdbcParameter : DbParameter, ICloneable, IDataParameter, IDbDataParameter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class.</summary>
		// Token: 0x06001CCC RID: 7372 RVA: 0x00050F57 File Offset: 0x0004F157
		public OdbcParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name and an <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="value">An <see cref="T:System.Data.Odbc.OdbcParameter" /> object. </param>
		// Token: 0x06001CCD RID: 7373 RVA: 0x0008F581 File Offset: 0x0008D781
		public OdbcParameter(string name, object value)
			: this()
		{
			this.ParameterName = name;
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name and data type.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="type">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="type" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06001CCE RID: 7374 RVA: 0x0008F597 File Offset: 0x0008D797
		public OdbcParameter(string name, OdbcType type)
			: this()
		{
			this.ParameterName = name;
			this.OdbcType = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name, data type, and length.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="type">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="type" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06001CCF RID: 7375 RVA: 0x0008F5AD File Offset: 0x0008D7AD
		public OdbcParameter(string name, OdbcType type, int size)
			: this()
		{
			this.ParameterName = name;
			this.OdbcType = type;
			this.Size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name, data type, length, and source column name.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="type">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="sourcecolumn">The name of the source column. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="type" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06001CD0 RID: 7376 RVA: 0x0008F5CA File Offset: 0x0008D7CA
		public OdbcParameter(string name, OdbcType type, int size, string sourcecolumn)
			: this()
		{
			this.ParameterName = name;
			this.OdbcType = type;
			this.Size = size;
			this.SourceColumn = sourcecolumn;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name, data type, length, source column name, parameter direction, numeric precision, and other properties.</summary>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="odbcType">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="parameterDirection">One of the <see cref="T:System.Data.ParameterDirection" /> values. </param>
		/// <param name="isNullable">true if the value of the field can be null; otherwise false. </param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved. </param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved. </param>
		/// <param name="srcColumn">The name of the source column. </param>
		/// <param name="srcVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.Odbc.OdbcParameter" />. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="type" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06001CD1 RID: 7377 RVA: 0x0008F5F0 File Offset: 0x0008D7F0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public OdbcParameter(string parameterName, OdbcType odbcType, int size, ParameterDirection parameterDirection, bool isNullable, byte precision, byte scale, string srcColumn, DataRowVersion srcVersion, object value)
			: this()
		{
			this.ParameterName = parameterName;
			this.OdbcType = odbcType;
			this.Size = size;
			this.Direction = parameterDirection;
			this.IsNullable = isNullable;
			this.PrecisionInternal = precision;
			this.ScaleInternal = scale;
			this.SourceColumn = srcColumn;
			this.SourceVersion = srcVersion;
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name, data type, length, source column name, parameter direction, numeric precision, and other properties.</summary>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="odbcType">One of the <see cref="P:System.Data.Odbc.OdbcParameter.OdbcType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="parameterDirection">One of the <see cref="T:System.Data.ParameterDirection" /> values. </param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved. </param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved. </param>
		/// <param name="sourceColumn">The name of the source column. </param>
		/// <param name="sourceVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values. </param>
		/// <param name="sourceColumnNullMapping">true if the corresponding source column is nullable; false if it is not.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.Odbc.OdbcParameter" />. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="type" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06001CD2 RID: 7378 RVA: 0x0008F650 File Offset: 0x0008D850
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public OdbcParameter(string parameterName, OdbcType odbcType, int size, ParameterDirection parameterDirection, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value)
			: this()
		{
			this.ParameterName = parameterName;
			this.OdbcType = odbcType;
			this.Size = size;
			this.Direction = parameterDirection;
			this.PrecisionInternal = precision;
			this.ScaleInternal = scale;
			this.SourceColumn = sourceColumn;
			this.SourceVersion = sourceVersion;
			this.SourceColumnNullMapping = sourceColumnNullMapping;
			this.Value = value;
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.DbType" /> values. The default is <see cref="T:System.String" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property was not set to a valid <see cref="T:System.Data.DbType" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x0008F6B0 File Offset: 0x0008D8B0
		// (set) Token: 0x06001CD4 RID: 7380 RVA: 0x0008F6D0 File Offset: 0x0008D8D0
		public override DbType DbType
		{
			get
			{
				if (this._userSpecifiedType)
				{
					return this._typemap._dbType;
				}
				return TypeMap._NVarChar._dbType;
			}
			set
			{
				if (this._typemap == null || this._typemap._dbType != value)
				{
					this.PropertyTypeChanging();
					this._typemap = TypeMap.FromDbType(value);
					this._userSpecifiedType = true;
				}
			}
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.Odbc.OdbcParameter" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001CD5 RID: 7381 RVA: 0x0008F701 File Offset: 0x0008D901
		public override void ResetDbType()
		{
			this.ResetOdbcType();
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Odbc.OdbcType" /> of the parameter.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcType" /> value that is the <see cref="T:System.Data.Odbc.OdbcType" /> of the parameter. The default is Nchar.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x0008F709 File Offset: 0x0008D909
		// (set) Token: 0x06001CD7 RID: 7383 RVA: 0x0008F729 File Offset: 0x0008D929
		[DbProviderSpecificTypeProperty(true)]
		[DefaultValue(OdbcType.NChar)]
		public OdbcType OdbcType
		{
			get
			{
				if (this._userSpecifiedType)
				{
					return this._typemap._odbcType;
				}
				return TypeMap._NVarChar._odbcType;
			}
			set
			{
				if (this._typemap == null || this._typemap._odbcType != value)
				{
					this.PropertyTypeChanging();
					this._typemap = TypeMap.FromOdbcType(value);
					this._userSpecifiedType = true;
				}
			}
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.Odbc.OdbcParameter" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001CD8 RID: 7384 RVA: 0x0008F75A File Offset: 0x0008D95A
		public void ResetOdbcType()
		{
			this.PropertyTypeChanging();
			this._typemap = null;
			this._userSpecifiedType = false;
		}

		// Token: 0x1700054B RID: 1355
		// (set) Token: 0x06001CD9 RID: 7385 RVA: 0x0008F770 File Offset: 0x0008D970
		internal bool HasChanged
		{
			set
			{
				this._hasChanged = value;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x0008F779 File Offset: 0x0008D979
		internal bool UserSpecifiedType
		{
			get
			{
				return this._userSpecifiedType;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.Odbc.OdbcParameter" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.Odbc.OdbcParameter" />. The default is an empty string ("").</returns>
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001CDB RID: 7387 RVA: 0x0008F784 File Offset: 0x0008D984
		// (set) Token: 0x06001CDC RID: 7388 RVA: 0x0008F7A2 File Offset: 0x0008D9A2
		public override string ParameterName
		{
			get
			{
				string parameterName = this._parameterName;
				if (parameterName == null)
				{
					return ADP.StrEmpty;
				}
				return parameterName;
			}
			set
			{
				if (this._parameterName != value)
				{
					this.PropertyChanging();
					this._parameterName = value;
				}
			}
		}

		/// <summary>Gets or sets the number of digits used to represent the <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> property.</summary>
		/// <returns>The maximum number of digits used to represent the <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> property. The default value is 0, which indicates that the data provider sets the precision for <see cref="P:System.Data.Odbc.OdbcParameter.Value" />. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001CDD RID: 7389 RVA: 0x0008F7BF File Offset: 0x0008D9BF
		// (set) Token: 0x06001CDE RID: 7390 RVA: 0x0008F7C7 File Offset: 0x0008D9C7
		public new byte Precision
		{
			get
			{
				return this.PrecisionInternal;
			}
			set
			{
				this.PrecisionInternal = value;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x0008F7D0 File Offset: 0x0008D9D0
		// (set) Token: 0x06001CE0 RID: 7392 RVA: 0x0008F7F5 File Offset: 0x0008D9F5
		internal byte PrecisionInternal
		{
			get
			{
				byte b = this._precision;
				if (b == 0)
				{
					b = this.ValuePrecision(this.Value);
				}
				return b;
			}
			set
			{
				if (this._precision != value)
				{
					this.PropertyChanging();
					this._precision = value;
				}
			}
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x0008F80D File Offset: 0x0008DA0D
		private bool ShouldSerializePrecision()
		{
			return this._precision > 0;
		}

		/// <summary>Gets or sets the number of decimal places to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved.</summary>
		/// <returns>The number of decimal places to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved. The default is 0.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x0008F818 File Offset: 0x0008DA18
		// (set) Token: 0x06001CE3 RID: 7395 RVA: 0x0008F820 File Offset: 0x0008DA20
		public new byte Scale
		{
			get
			{
				return this.ScaleInternal;
			}
			set
			{
				this.ScaleInternal = value;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x0008F82C File Offset: 0x0008DA2C
		// (set) Token: 0x06001CE5 RID: 7397 RVA: 0x0008F857 File Offset: 0x0008DA57
		internal byte ScaleInternal
		{
			get
			{
				byte b = this._scale;
				if (!this.ShouldSerializeScale(b))
				{
					b = this.ValueScale(this.Value);
				}
				return b;
			}
			set
			{
				if (this._scale != value || !this._hasScale)
				{
					this.PropertyChanging();
					this._scale = value;
					this._hasScale = true;
				}
			}
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x0008F87E File Offset: 0x0008DA7E
		private bool ShouldSerializeScale()
		{
			return this.ShouldSerializeScale(this._scale);
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x0008F88C File Offset: 0x0008DA8C
		private bool ShouldSerializeScale(byte scale)
		{
			return this._hasScale && (scale != 0 || this.ShouldSerializePrecision());
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x0008F8A4 File Offset: 0x0008DAA4
		private int GetColumnSize(object value, int offset, int ordinal)
		{
			if (ODBC32.SQL_C.NUMERIC == this._bindtype._sql_c && this._internalPrecision != 0)
			{
				return Math.Min((int)this._internalPrecision, 29);
			}
			int num = this._bindtype._columnSize;
			if (0 >= num)
			{
				if (ODBC32.SQL_C.NUMERIC == this._typemap._sql_c)
				{
					num = 62;
				}
				else
				{
					num = this._internalSize;
					if (!this._internalShouldSerializeSize || 1073741823 <= num || num < 0)
					{
						if (!this._internalShouldSerializeSize && (ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0)
						{
							throw ADP.UninitializedParameterSize(ordinal, this._bindtype._type);
						}
						if (value == null || Convert.IsDBNull(value))
						{
							num = 0;
						}
						else if (value is string)
						{
							num = ((string)value).Length - offset;
							if ((ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0 && 1073741823 <= this._internalSize)
							{
								num = Math.Max(num, 4096);
							}
							if (ODBC32.SQL_TYPE.CHAR == this._bindtype._sql_type || ODBC32.SQL_TYPE.VARCHAR == this._bindtype._sql_type || ODBC32.SQL_TYPE.LONGVARCHAR == this._bindtype._sql_type)
							{
								num = Encoding.Default.GetMaxByteCount(num);
							}
						}
						else if (value is char[])
						{
							num = ((char[])value).Length - offset;
							if ((ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0 && 1073741823 <= this._internalSize)
							{
								num = Math.Max(num, 4096);
							}
							if (ODBC32.SQL_TYPE.CHAR == this._bindtype._sql_type || ODBC32.SQL_TYPE.VARCHAR == this._bindtype._sql_type || ODBC32.SQL_TYPE.LONGVARCHAR == this._bindtype._sql_type)
							{
								num = Encoding.Default.GetMaxByteCount(num);
							}
						}
						else if (value is byte[])
						{
							num = ((byte[])value).Length - offset;
							if ((ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0 && 1073741823 <= this._internalSize)
							{
								num = Math.Max(num, 8192);
							}
						}
						num = Math.Max(2, num);
					}
				}
			}
			return num;
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x0008FA78 File Offset: 0x0008DC78
		private int GetValueSize(object value, int offset)
		{
			if (ODBC32.SQL_C.NUMERIC == this._bindtype._sql_c && this._internalPrecision != 0)
			{
				return Math.Min((int)this._internalPrecision, 29);
			}
			int num = this._bindtype._columnSize;
			if (0 >= num)
			{
				bool flag = false;
				if (value is string)
				{
					num = ((string)value).Length - offset;
					flag = true;
				}
				else if (value is char[])
				{
					num = ((char[])value).Length - offset;
					flag = true;
				}
				else if (value is byte[])
				{
					num = ((byte[])value).Length - offset;
				}
				else
				{
					num = 0;
				}
				if (this._internalShouldSerializeSize && this._internalSize >= 0 && this._internalSize < num && this._bindtype == this._originalbindtype)
				{
					num = this._internalSize;
				}
				if (flag)
				{
					num *= 2;
				}
			}
			return num;
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x0008FB40 File Offset: 0x0008DD40
		private int GetParameterSize(object value, int offset, int ordinal)
		{
			int num = this._bindtype._bufferSize;
			if (0 >= num)
			{
				if (ODBC32.SQL_C.NUMERIC == this._typemap._sql_c)
				{
					num = 518;
				}
				else
				{
					num = this._internalSize;
					if (!this._internalShouldSerializeSize || 1073741823 <= num || num < 0)
					{
						if (num <= 0 && (ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0)
						{
							throw ADP.UninitializedParameterSize(ordinal, this._bindtype._type);
						}
						if (value == null || Convert.IsDBNull(value))
						{
							if (this._bindtype._sql_c == ODBC32.SQL_C.WCHAR)
							{
								num = 2;
							}
							else
							{
								num = 0;
							}
						}
						else if (value is string)
						{
							num = (((string)value).Length - offset) * 2 + 2;
						}
						else if (value is char[])
						{
							num = (((char[])value).Length - offset) * 2 + 2;
						}
						else if (value is byte[])
						{
							num = ((byte[])value).Length - offset;
						}
						if ((ParameterDirection.Output & this._internalDirection) != (ParameterDirection)0 && 1073741823 <= this._internalSize)
						{
							num = Math.Max(num, 8192);
						}
					}
					else if (ODBC32.SQL_C.WCHAR == this._bindtype._sql_c)
					{
						if (value is string && num < ((string)value).Length && this._bindtype == this._originalbindtype)
						{
							num = ((string)value).Length;
						}
						num = num * 2 + 2;
					}
					else if (value is byte[] && num < ((byte[])value).Length && this._bindtype == this._originalbindtype)
					{
						num = ((byte[])value).Length;
					}
				}
			}
			return num;
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x0008FCBC File Offset: 0x0008DEBC
		private byte GetParameterPrecision(object value)
		{
			if (this._internalPrecision != 0 && value is decimal)
			{
				if (this._internalPrecision < 29)
				{
					if (this._internalPrecision != 0)
					{
						byte precision = ((decimal)value).Precision;
						this._internalPrecision = Math.Max(this._internalPrecision, precision);
					}
					return this._internalPrecision;
				}
				return 29;
			}
			else
			{
				if (value == null || value is decimal || Convert.IsDBNull(value))
				{
					return 28;
				}
				return 0;
			}
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x0008FD34 File Offset: 0x0008DF34
		private byte GetParameterScale(object value)
		{
			if (!(value is decimal))
			{
				return this._internalScale;
			}
			byte b = (byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16);
			if (this._internalScale > 0 && this._internalScale < b)
			{
				return this._internalScale;
			}
			return b;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x06001CED RID: 7405 RVA: 0x0008FD82 File Offset: 0x0008DF82
		object ICloneable.Clone()
		{
			return new OdbcParameter(this);
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0008FD8C File Offset: 0x0008DF8C
		private void CopyParameterInternal()
		{
			this._internalValue = this.Value;
			this._internalPrecision = (this.ShouldSerializePrecision() ? this.PrecisionInternal : this.ValuePrecision(this._internalValue));
			this._internalShouldSerializeSize = this.ShouldSerializeSize();
			this._internalSize = (this._internalShouldSerializeSize ? this.Size : this.ValueSize(this._internalValue));
			this._internalDirection = this.Direction;
			this._internalScale = (this.ShouldSerializeScale() ? this.ScaleInternal : this.ValueScale(this._internalValue));
			this._internalOffset = this.Offset;
			this._internalUserSpecifiedType = this.UserSpecifiedType;
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x0008FE3C File Offset: 0x0008E03C
		private void CloneHelper(OdbcParameter destination)
		{
			this.CloneHelperCore(destination);
			destination._userSpecifiedType = this._userSpecifiedType;
			destination._typemap = this._typemap;
			destination._parameterName = this._parameterName;
			destination._precision = this._precision;
			destination._scale = this._scale;
			destination._hasScale = this._hasScale;
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x0008FE98 File Offset: 0x0008E098
		internal void ClearBinding()
		{
			if (!this._userSpecifiedType)
			{
				this._typemap = null;
			}
			this._bindtype = null;
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x0008FEB0 File Offset: 0x0008E0B0
		internal void PrepareForBind(OdbcCommand command, short ordinal, ref int parameterBufferSize)
		{
			this.CopyParameterInternal();
			object obj = this.ProcessAndGetParameterValue();
			int num = this._internalOffset;
			int num2 = this._internalSize;
			if (num > 0)
			{
				if (obj is string)
				{
					if (num > ((string)obj).Length)
					{
						throw ADP.OffsetOutOfRangeException();
					}
				}
				else if (obj is char[])
				{
					if (num > ((char[])obj).Length)
					{
						throw ADP.OffsetOutOfRangeException();
					}
				}
				else if (obj is byte[])
				{
					if (num > ((byte[])obj).Length)
					{
						throw ADP.OffsetOutOfRangeException();
					}
				}
				else
				{
					num = 0;
				}
			}
			ODBC32.SQL_TYPE sql_TYPE = this._bindtype._sql_type;
			if (sql_TYPE - ODBC32.SQL_TYPE.WLONGVARCHAR > 2)
			{
				if (sql_TYPE != ODBC32.SQL_TYPE.BIGINT)
				{
					if (sql_TYPE - ODBC32.SQL_TYPE.NUMERIC <= 1 && (!command.Connection.IsV3Driver || !command.Connection.TestTypeSupport(ODBC32.SQL_TYPE.NUMERIC) || command.Connection.TestRestrictedSqlBindType(this._bindtype._sql_type)))
					{
						this._bindtype = TypeMap._VarChar;
						if (obj != null && !Convert.IsDBNull(obj))
						{
							obj = ((decimal)obj).ToString(CultureInfo.CurrentCulture);
							num2 = ((string)obj).Length;
							num = 0;
						}
					}
				}
				else if (!command.Connection.IsV3Driver)
				{
					this._bindtype = TypeMap._VarChar;
					if (obj != null && !Convert.IsDBNull(obj))
					{
						obj = ((long)obj).ToString(CultureInfo.CurrentCulture);
						num2 = ((string)obj).Length;
						num = 0;
					}
				}
			}
			else
			{
				if (obj is char)
				{
					obj = obj.ToString();
					num2 = ((string)obj).Length;
					num = 0;
				}
				if (!command.Connection.TestTypeSupport(this._bindtype._sql_type))
				{
					if (ODBC32.SQL_TYPE.WCHAR == this._bindtype._sql_type)
					{
						this._bindtype = TypeMap._Char;
					}
					else if (ODBC32.SQL_TYPE.WVARCHAR == this._bindtype._sql_type)
					{
						this._bindtype = TypeMap._VarChar;
					}
					else if (ODBC32.SQL_TYPE.WLONGVARCHAR == this._bindtype._sql_type)
					{
						this._bindtype = TypeMap._Text;
					}
				}
			}
			ODBC32.SQL_C sql_C = this._bindtype._sql_c;
			if (!command.Connection.IsV3Driver && sql_C == ODBC32.SQL_C.WCHAR)
			{
				sql_C = ODBC32.SQL_C.CHAR;
				if (obj != null && !Convert.IsDBNull(obj) && obj is string)
				{
					obj = Encoding.GetEncoding(new CultureInfo(CultureInfo.CurrentCulture.LCID).TextInfo.ANSICodePage).GetBytes(obj.ToString());
					num2 = ((byte[])obj).Length;
				}
			}
			int parameterSize = this.GetParameterSize(obj, num, (int)ordinal);
			sql_TYPE = this._bindtype._sql_type;
			if (sql_TYPE != ODBC32.SQL_TYPE.WVARCHAR)
			{
				if (sql_TYPE != ODBC32.SQL_TYPE.VARBINARY)
				{
					if (sql_TYPE == ODBC32.SQL_TYPE.VARCHAR)
					{
						if (num2 > 8000)
						{
							this._bindtype = TypeMap._Text;
						}
					}
				}
				else if (num2 > 8000)
				{
					this._bindtype = TypeMap._Image;
				}
			}
			else if (num2 > 4000)
			{
				this._bindtype = TypeMap._NText;
			}
			this._prepared_Sql_C_Type = sql_C;
			this._preparedOffset = num;
			this._preparedSize = num2;
			this._preparedValue = obj;
			this._preparedBufferSize = parameterSize;
			this._preparedIntOffset = parameterBufferSize;
			this._preparedValueOffset = this._preparedIntOffset + IntPtr.Size;
			parameterBufferSize += parameterSize + IntPtr.Size;
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x000901C8 File Offset: 0x0008E3C8
		internal void Bind(OdbcStatementHandle hstmt, OdbcCommand command, short ordinal, CNativeBuffer parameterBuffer, bool allowReentrance)
		{
			ODBC32.SQL_C prepared_Sql_C_Type = this._prepared_Sql_C_Type;
			ODBC32.SQL_PARAM sql_PARAM = this.SqlDirectionFromParameterDirection();
			int preparedOffset = this._preparedOffset;
			int preparedSize = this._preparedSize;
			object obj = this._preparedValue;
			int valueSize = this.GetValueSize(obj, preparedOffset);
			int columnSize = this.GetColumnSize(obj, preparedOffset, (int)ordinal);
			byte parameterPrecision = this.GetParameterPrecision(obj);
			byte b = this.GetParameterScale(obj);
			HandleRef handleRef = parameterBuffer.PtrOffset(this._preparedValueOffset, this._preparedBufferSize);
			HandleRef handleRef2 = parameterBuffer.PtrOffset(this._preparedIntOffset, IntPtr.Size);
			if (ODBC32.SQL_C.NUMERIC == prepared_Sql_C_Type)
			{
				if (ODBC32.SQL_PARAM.INPUT_OUTPUT == sql_PARAM && obj is decimal && b < this._internalScale)
				{
					while (b < this._internalScale)
					{
						obj = (decimal)obj * 10m;
						b += 1;
					}
				}
				this.SetInputValue(obj, prepared_Sql_C_Type, valueSize, (int)parameterPrecision, 0, parameterBuffer);
				if (ODBC32.SQL_PARAM.INPUT != sql_PARAM)
				{
					parameterBuffer.WriteInt16(this._preparedValueOffset, (short)(((int)b << 8) | (int)parameterPrecision));
				}
			}
			else
			{
				this.SetInputValue(obj, prepared_Sql_C_Type, valueSize, preparedSize, preparedOffset, parameterBuffer);
			}
			if (!this._hasChanged && this._boundSqlCType == prepared_Sql_C_Type && this._boundParameterType == this._bindtype._sql_type && this._boundSize == columnSize && this._boundScale == (int)b && this._boundBuffer == handleRef.Handle && this._boundIntbuffer == handleRef2.Handle)
			{
				return;
			}
			ODBC32.RetCode retCode = hstmt.BindParameter(ordinal, (short)sql_PARAM, prepared_Sql_C_Type, this._bindtype._sql_type, (IntPtr)columnSize, (IntPtr)((int)b), handleRef, (IntPtr)this._preparedBufferSize, handleRef2);
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				if ("07006" == command.GetDiagSqlState())
				{
					command.Connection.FlagRestrictedSqlBindType(this._bindtype._sql_type);
					if (allowReentrance)
					{
						this.Bind(hstmt, command, ordinal, parameterBuffer, false);
						return;
					}
				}
				command.Connection.HandleError(hstmt, retCode);
			}
			this._hasChanged = false;
			this._boundSqlCType = prepared_Sql_C_Type;
			this._boundParameterType = this._bindtype._sql_type;
			this._boundSize = columnSize;
			this._boundScale = (int)b;
			this._boundBuffer = handleRef.Handle;
			this._boundIntbuffer = handleRef2.Handle;
			if (ODBC32.SQL_C.NUMERIC == prepared_Sql_C_Type)
			{
				OdbcDescriptorHandle descriptorHandle = command.GetDescriptorHandle(ODBC32.SQL_ATTR.APP_PARAM_DESC);
				retCode = descriptorHandle.SetDescriptionField1(ordinal, ODBC32.SQL_DESC.TYPE, (IntPtr)2);
				if (retCode != ODBC32.RetCode.SUCCESS)
				{
					command.Connection.HandleError(hstmt, retCode);
				}
				int num = (int)parameterPrecision;
				retCode = descriptorHandle.SetDescriptionField1(ordinal, ODBC32.SQL_DESC.PRECISION, (IntPtr)num);
				if (retCode != ODBC32.RetCode.SUCCESS)
				{
					command.Connection.HandleError(hstmt, retCode);
				}
				num = (int)b;
				retCode = descriptorHandle.SetDescriptionField1(ordinal, ODBC32.SQL_DESC.SCALE, (IntPtr)num);
				if (retCode != ODBC32.RetCode.SUCCESS)
				{
					command.Connection.HandleError(hstmt, retCode);
				}
				retCode = descriptorHandle.SetDescriptionField2(ordinal, ODBC32.SQL_DESC.DATA_PTR, handleRef);
				if (retCode != ODBC32.RetCode.SUCCESS)
				{
					command.Connection.HandleError(hstmt, retCode);
				}
			}
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x0009049C File Offset: 0x0008E69C
		internal void GetOutputValue(CNativeBuffer parameterBuffer)
		{
			if (this._hasChanged)
			{
				return;
			}
			if (this._bindtype != null && this._internalDirection != ParameterDirection.Input)
			{
				TypeMap bindtype = this._bindtype;
				this._bindtype = null;
				int num = (int)parameterBuffer.ReadIntPtr(this._preparedIntOffset);
				if (-1 == num)
				{
					this.Value = DBNull.Value;
					return;
				}
				if (0 <= num || num == -3)
				{
					this.Value = parameterBuffer.MarshalToManaged(this._preparedValueOffset, this._boundSqlCType, num);
					if (this._boundSqlCType == ODBC32.SQL_C.CHAR && this.Value != null && !Convert.IsDBNull(this.Value))
					{
						Encoding encoding = Encoding.GetEncoding(new CultureInfo(CultureInfo.CurrentCulture.LCID).TextInfo.ANSICodePage);
						this.Value = encoding.GetString((byte[])this.Value);
					}
					if (bindtype != this._typemap && this.Value != null && !Convert.IsDBNull(this.Value) && this.Value.GetType() != this._typemap._type)
					{
						this.Value = decimal.Parse((string)this.Value, CultureInfo.CurrentCulture);
					}
				}
			}
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x000905D0 File Offset: 0x0008E7D0
		private object ProcessAndGetParameterValue()
		{
			object obj = this._internalValue;
			if (this._internalUserSpecifiedType)
			{
				if (obj != null && !Convert.IsDBNull(obj))
				{
					Type type = obj.GetType();
					if (!type.IsArray)
					{
						if (!(type != this._typemap._type))
						{
							goto IL_00CE;
						}
						try
						{
							obj = Convert.ChangeType(obj, this._typemap._type, null);
							goto IL_00CE;
						}
						catch (Exception ex)
						{
							if (!ADP.IsCatchableExceptionType(ex))
							{
								throw;
							}
							throw ADP.ParameterConversionFailed(obj, this._typemap._type, ex);
						}
					}
					if (type == typeof(char[]))
					{
						obj = new string((char[])obj);
					}
				}
			}
			else if (this._typemap == null)
			{
				if (obj == null || Convert.IsDBNull(obj))
				{
					this._typemap = TypeMap._NVarChar;
				}
				else
				{
					Type type2 = obj.GetType();
					this._typemap = TypeMap.FromSystemType(type2);
				}
			}
			IL_00CE:
			this._originalbindtype = (this._bindtype = this._typemap);
			return obj;
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x000906D4 File Offset: 0x0008E8D4
		private void PropertyChanging()
		{
			this._hasChanged = true;
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x000906DD File Offset: 0x0008E8DD
		private void PropertyTypeChanging()
		{
			this.PropertyChanging();
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x000906E8 File Offset: 0x0008E8E8
		internal void SetInputValue(object value, ODBC32.SQL_C sql_c_type, int cbsize, int sizeorprecision, int offset, CNativeBuffer parameterBuffer)
		{
			if (ParameterDirection.Input != this._internalDirection && ParameterDirection.InputOutput != this._internalDirection)
			{
				this._internalValue = null;
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, (IntPtr)(-1));
				return;
			}
			if (value == null)
			{
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, (IntPtr)(-5));
				return;
			}
			if (Convert.IsDBNull(value))
			{
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, (IntPtr)(-1));
				return;
			}
			if (sql_c_type == ODBC32.SQL_C.WCHAR || sql_c_type == ODBC32.SQL_C.BINARY || sql_c_type == ODBC32.SQL_C.CHAR)
			{
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, (IntPtr)cbsize);
			}
			else
			{
				parameterBuffer.WriteIntPtr(this._preparedIntOffset, IntPtr.Zero);
			}
			parameterBuffer.MarshalToNative(this._preparedValueOffset, value, sql_c_type, sizeorprecision, offset);
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x000907A0 File Offset: 0x0008E9A0
		private ODBC32.SQL_PARAM SqlDirectionFromParameterDirection()
		{
			switch (this._internalDirection)
			{
			case ParameterDirection.Input:
				return ODBC32.SQL_PARAM.INPUT;
			case ParameterDirection.Output:
			case ParameterDirection.ReturnValue:
				return ODBC32.SQL_PARAM.OUTPUT;
			case ParameterDirection.InputOutput:
				return ODBC32.SQL_PARAM.INPUT_OUTPUT;
			}
			return ODBC32.SQL_PARAM.INPUT;
		}

		/// <summary>Gets or sets the value of the parameter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter. The default value is null.</returns>
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x000907DD File Offset: 0x0008E9DD
		// (set) Token: 0x06001CFA RID: 7418 RVA: 0x000907E5 File Offset: 0x0008E9E5
		public override object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._coercedValue = null;
				this._value = value;
			}
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x000907F5 File Offset: 0x0008E9F5
		private byte ValuePrecision(object value)
		{
			return this.ValuePrecisionCore(value);
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x000907FE File Offset: 0x0008E9FE
		private byte ValueScale(object value)
		{
			return this.ValueScaleCore(value);
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x00090807 File Offset: 0x0008EA07
		private int ValueSize(object value)
		{
			return this.ValueSizeCore(value);
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x00090810 File Offset: 0x0008EA10
		private OdbcParameter(OdbcParameter source)
			: this()
		{
			ADP.CheckArgumentNull(source, "source");
			source.CloneHelper(this);
			ICloneable cloneable = this._value as ICloneable;
			if (cloneable != null)
			{
				this._value = cloneable.Clone();
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001CFF RID: 7423 RVA: 0x00090850 File Offset: 0x0008EA50
		// (set) Token: 0x06001D00 RID: 7424 RVA: 0x00090858 File Offset: 0x0008EA58
		private object CoercedValue
		{
			get
			{
				return this._coercedValue;
			}
			set
			{
				this._coercedValue = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter is input-only, output-only, bidirectional, or a stored procedure return value parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is Input.</returns>
		/// <exception cref="T:System.ArgumentException">The property was not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values.</exception>
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x00090864 File Offset: 0x0008EA64
		// (set) Token: 0x06001D02 RID: 7426 RVA: 0x0009087E File Offset: 0x0008EA7E
		public override ParameterDirection Direction
		{
			get
			{
				ParameterDirection direction = this._direction;
				if (direction == (ParameterDirection)0)
				{
					return ParameterDirection.Input;
				}
				return direction;
			}
			set
			{
				if (this._direction == value)
				{
					return;
				}
				if (value - ParameterDirection.Input <= 2 || value == ParameterDirection.ReturnValue)
				{
					this.PropertyChanging();
					this._direction = value;
					return;
				}
				throw ADP.InvalidParameterDirection(value);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter accepts null values.</summary>
		/// <returns>true if null values are accepted; otherwise false. The default is false.</returns>
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001D03 RID: 7427 RVA: 0x000908A8 File Offset: 0x0008EAA8
		// (set) Token: 0x06001D04 RID: 7428 RVA: 0x000908B0 File Offset: 0x0008EAB0
		public override bool IsNullable
		{
			get
			{
				return this._isNullable;
			}
			set
			{
				this._isNullable = value;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x000908B9 File Offset: 0x0008EAB9
		// (set) Token: 0x06001D06 RID: 7430 RVA: 0x000908C1 File Offset: 0x0008EAC1
		public int Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				if (value < 0)
				{
					throw ADP.InvalidOffsetValue(value);
				}
				this._offset = value;
			}
		}

		/// <summary>Gets or sets the maximum size of the data within the column.</summary>
		/// <returns>The maximum size of the data within the column. The default value is inferred from the parameter value.</returns>
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x000908D8 File Offset: 0x0008EAD8
		// (set) Token: 0x06001D08 RID: 7432 RVA: 0x000908FD File Offset: 0x0008EAFD
		public override int Size
		{
			get
			{
				int num = this._size;
				if (num == 0)
				{
					num = this.ValueSize(this.Value);
				}
				return num;
			}
			set
			{
				if (this._size != value)
				{
					if (value < -1)
					{
						throw ADP.InvalidSizeValue(value);
					}
					this.PropertyChanging();
					this._size = value;
				}
			}
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x00090920 File Offset: 0x0008EB20
		private bool ShouldSerializeSize()
		{
			return this._size != 0;
		}

		/// <summary>Gets or sets the name of the source column mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.Odbc.OdbcParameter.Value" />.</summary>
		/// <returns>The name of the source column that will be used to set the value of this parameter. The default is an empty string ("").</returns>
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x0009092C File Offset: 0x0008EB2C
		// (set) Token: 0x06001D0B RID: 7435 RVA: 0x0009094A File Offset: 0x0008EB4A
		public override string SourceColumn
		{
			get
			{
				string sourceColumn = this._sourceColumn;
				if (sourceColumn == null)
				{
					return ADP.StrEmpty;
				}
				return sourceColumn;
			}
			set
			{
				this._sourceColumn = value;
			}
		}

		/// <summary>Sets or gets a value which indicates whether the source column is nullable. This lets <see cref="T:System.Data.Common.DbCommandBuilder" /> correctly generate Update statements for nullable columns.</summary>
		/// <returns>true if the source column is nullable; false if it is not.</returns>
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001D0C RID: 7436 RVA: 0x00090953 File Offset: 0x0008EB53
		// (set) Token: 0x06001D0D RID: 7437 RVA: 0x0009095B File Offset: 0x0008EB5B
		public override bool SourceColumnNullMapping
		{
			get
			{
				return this._sourceColumnNullMapping;
			}
			set
			{
				this._sourceColumnNullMapping = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when you load <see cref="P:System.Data.Odbc.OdbcParameter.Value" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is Current.</returns>
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x00090964 File Offset: 0x0008EB64
		// (set) Token: 0x06001D0F RID: 7439 RVA: 0x00090982 File Offset: 0x0008EB82
		public override DataRowVersion SourceVersion
		{
			get
			{
				DataRowVersion sourceVersion = this._sourceVersion;
				if (sourceVersion == (DataRowVersion)0)
				{
					return DataRowVersion.Current;
				}
				return sourceVersion;
			}
			set
			{
				if (value <= DataRowVersion.Current)
				{
					if (value != DataRowVersion.Original && value != DataRowVersion.Current)
					{
						goto IL_0032;
					}
				}
				else if (value != DataRowVersion.Proposed && value != DataRowVersion.Default)
				{
					goto IL_0032;
				}
				this._sourceVersion = value;
				return;
				IL_0032:
				throw ADP.InvalidDataRowVersion(value);
			}
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x000909BC File Offset: 0x0008EBBC
		private void CloneHelperCore(OdbcParameter destination)
		{
			destination._value = this._value;
			destination._direction = this._direction;
			destination._size = this._size;
			destination._offset = this._offset;
			destination._sourceColumn = this._sourceColumn;
			destination._sourceVersion = this._sourceVersion;
			destination._sourceColumnNullMapping = this._sourceColumnNullMapping;
			destination._isNullable = this._isNullable;
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x00090A2C File Offset: 0x0008EC2C
		internal object CompareExchangeParent(object value, object comparand)
		{
			object parent = this._parent;
			if (comparand == parent)
			{
				this._parent = value;
			}
			return parent;
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x00090A4C File Offset: 0x0008EC4C
		internal void ResetParent()
		{
			this._parent = null;
		}

		/// <summary>Gets a string that contains the <see cref="P:System.Data.Odbc.OdbcParameter.ParameterName" />.</summary>
		/// <returns>A string that contains the <see cref="P:System.Data.Odbc.OdbcParameter.ParameterName" />.</returns>
		// Token: 0x06001D13 RID: 7443 RVA: 0x0006E241 File Offset: 0x0006C441
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x00090A58 File Offset: 0x0008EC58
		private byte ValuePrecisionCore(object value)
		{
			if (value is decimal)
			{
				return ((decimal)value).Precision;
			}
			return 0;
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x0006E276 File Offset: 0x0006C476
		private byte ValueScaleCore(object value)
		{
			if (value is decimal)
			{
				return (byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16);
			}
			return 0;
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x00090A84 File Offset: 0x0008EC84
		private int ValueSizeCore(object value)
		{
			if (!ADP.IsNull(value))
			{
				string text = value as string;
				if (text != null)
				{
					return text.Length;
				}
				byte[] array = value as byte[];
				if (array != null)
				{
					return array.Length;
				}
				char[] array2 = value as char[];
				if (array2 != null)
				{
					return array2.Length;
				}
				if (value is byte || value is char)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x04001557 RID: 5463
		private bool _hasChanged;

		// Token: 0x04001558 RID: 5464
		private bool _userSpecifiedType;

		// Token: 0x04001559 RID: 5465
		private TypeMap _typemap;

		// Token: 0x0400155A RID: 5466
		private TypeMap _bindtype;

		// Token: 0x0400155B RID: 5467
		private string _parameterName;

		// Token: 0x0400155C RID: 5468
		private byte _precision;

		// Token: 0x0400155D RID: 5469
		private byte _scale;

		// Token: 0x0400155E RID: 5470
		private bool _hasScale;

		// Token: 0x0400155F RID: 5471
		private ODBC32.SQL_C _boundSqlCType;

		// Token: 0x04001560 RID: 5472
		private ODBC32.SQL_TYPE _boundParameterType;

		// Token: 0x04001561 RID: 5473
		private int _boundSize;

		// Token: 0x04001562 RID: 5474
		private int _boundScale;

		// Token: 0x04001563 RID: 5475
		private IntPtr _boundBuffer;

		// Token: 0x04001564 RID: 5476
		private IntPtr _boundIntbuffer;

		// Token: 0x04001565 RID: 5477
		private TypeMap _originalbindtype;

		// Token: 0x04001566 RID: 5478
		private byte _internalPrecision;

		// Token: 0x04001567 RID: 5479
		private bool _internalShouldSerializeSize;

		// Token: 0x04001568 RID: 5480
		private int _internalSize;

		// Token: 0x04001569 RID: 5481
		private ParameterDirection _internalDirection;

		// Token: 0x0400156A RID: 5482
		private byte _internalScale;

		// Token: 0x0400156B RID: 5483
		private int _internalOffset;

		// Token: 0x0400156C RID: 5484
		internal bool _internalUserSpecifiedType;

		// Token: 0x0400156D RID: 5485
		private object _internalValue;

		// Token: 0x0400156E RID: 5486
		private int _preparedOffset;

		// Token: 0x0400156F RID: 5487
		private int _preparedSize;

		// Token: 0x04001570 RID: 5488
		private int _preparedBufferSize;

		// Token: 0x04001571 RID: 5489
		private object _preparedValue;

		// Token: 0x04001572 RID: 5490
		private int _preparedIntOffset;

		// Token: 0x04001573 RID: 5491
		private int _preparedValueOffset;

		// Token: 0x04001574 RID: 5492
		private ODBC32.SQL_C _prepared_Sql_C_Type;

		// Token: 0x04001575 RID: 5493
		private object _value;

		// Token: 0x04001576 RID: 5494
		private object _parent;

		// Token: 0x04001577 RID: 5495
		private ParameterDirection _direction;

		// Token: 0x04001578 RID: 5496
		private int _size;

		// Token: 0x04001579 RID: 5497
		private int _offset;

		// Token: 0x0400157A RID: 5498
		private string _sourceColumn;

		// Token: 0x0400157B RID: 5499
		private DataRowVersion _sourceVersion;

		// Token: 0x0400157C RID: 5500
		private bool _sourceColumnNullMapping;

		// Token: 0x0400157D RID: 5501
		private bool _isNullable;

		// Token: 0x0400157E RID: 5502
		private object _coercedValue;
	}
}
