using System;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a GUID to be stored in or retrieved from a database.</summary>
	// Token: 0x020002C6 RID: 710
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlGuid : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06001FA4 RID: 8100 RVA: 0x00098D95 File Offset: 0x00096F95
		private SqlGuid(bool fNull)
		{
			this.m_value = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure using the supplied byte array parameter.</summary>
		/// <param name="value">A byte array. </param>
		// Token: 0x06001FA5 RID: 8101 RVA: 0x00098D9E File Offset: 0x00096F9E
		public SqlGuid(byte[] value)
		{
			if (value == null || value.Length != SqlGuid.s_sizeOfGuid)
			{
				throw new ArgumentException(SQLResource.InvalidArraySizeMessage);
			}
			this.m_value = new byte[SqlGuid.s_sizeOfGuid];
			value.CopyTo(this.m_value, 0);
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x00098DD5 File Offset: 0x00096FD5
		internal SqlGuid(byte[] value, bool ignored)
		{
			if (value == null || value.Length != SqlGuid.s_sizeOfGuid)
			{
				throw new ArgumentException(SQLResource.InvalidArraySizeMessage);
			}
			this.m_value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure using the specified <see cref="T:System.String" /> parameter.</summary>
		/// <param name="s">A <see cref="T:System.String" /> object. </param>
		// Token: 0x06001FA7 RID: 8103 RVA: 0x00098DF8 File Offset: 0x00096FF8
		public SqlGuid(string s)
		{
			this.m_value = new Guid(s).ToByteArray();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure using the specified <see cref="T:System.Guid" /> parameter.</summary>
		/// <param name="g">A <see cref="T:System.Guid" /></param>
		// Token: 0x06001FA8 RID: 8104 RVA: 0x00098E19 File Offset: 0x00097019
		public SqlGuid(Guid g)
		{
			this.m_value = g.ToByteArray();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure using the specified values.</summary>
		/// <param name="a">The first four bytes of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="b">The next two bytes of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="c">The next two bytes of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="d">The next byte of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="e">The next byte of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="f">The next byte of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="g">The next byte of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="h">The next byte of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="i">The next byte of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="j">The next byte of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="k">The next byte of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		// Token: 0x06001FA9 RID: 8105 RVA: 0x00098E28 File Offset: 0x00097028
		public SqlGuid(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
		{
			this = new SqlGuid(new Guid(a, b, c, d, e, f, g, h, i, j, k));
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure is null.</summary>
		/// <returns>true if null. Otherwise, false.</returns>
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001FAA RID: 8106 RVA: 0x00098E53 File Offset: 0x00097053
		public bool IsNull
		{
			get
			{
				return this.m_value == null;
			}
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. This property is read-only.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure.</returns>
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001FAB RID: 8107 RVA: 0x00098E5E File Offset: 0x0009705E
		public Guid Value
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return new Guid(this.m_value);
			}
		}

		/// <summary>Converts the supplied <see cref="T:System.Guid" /> parameter to <see cref="T:System.Data.SqlTypes.SqlGuid" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlGuid" /> whose <see cref="P:System.Data.SqlTypes.SqlGuid.Value" /> is equal to the <see cref="T:System.Guid" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Guid" />. </param>
		// Token: 0x06001FAC RID: 8108 RVA: 0x00098E79 File Offset: 0x00097079
		public static implicit operator SqlGuid(Guid x)
		{
			return new SqlGuid(x);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlGuid" /> parameter to <see cref="T:System.Guid" />.</summary>
		/// <returns>A new <see cref="T:System.Guid" /> equal to the <see cref="P:System.Data.SqlTypes.SqlGuid.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlGuid" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06001FAD RID: 8109 RVA: 0x00098E81 File Offset: 0x00097081
		public static explicit operator Guid(SqlGuid x)
		{
			return x.Value;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to a byte array.</summary>
		/// <returns>An array of bytes representing the <see cref="P:System.Data.SqlTypes.SqlGuid.Value" /> of this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure.</returns>
		// Token: 0x06001FAE RID: 8110 RVA: 0x00098E8C File Offset: 0x0009708C
		public byte[] ToByteArray()
		{
			byte[] array = new byte[SqlGuid.s_sizeOfGuid];
			this.m_value.CopyTo(array, 0);
			return array;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001FAF RID: 8111 RVA: 0x00098EB4 File Offset: 0x000970B4
		public override string ToString()
		{
			if (this.IsNull)
			{
				return SQLResource.NullString;
			}
			Guid guid = new Guid(this.m_value);
			return guid.ToString();
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> structure to <see cref="T:System.Data.SqlTypes.SqlGuid" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlGuid" /> equivalent to the value that is contained in the specified <see cref="T:System.String" />.</returns>
		/// <param name="s">The String to be parsed. </param>
		// Token: 0x06001FB0 RID: 8112 RVA: 0x00098EE9 File Offset: 0x000970E9
		public static SqlGuid Parse(string s)
		{
			if (s == SQLResource.NullString)
			{
				return SqlGuid.Null;
			}
			return new SqlGuid(s);
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x00098F04 File Offset: 0x00097104
		private static EComparison Compare(SqlGuid x, SqlGuid y)
		{
			int i = 0;
			while (i < SqlGuid.s_sizeOfGuid)
			{
				byte b = x.m_value[SqlGuid.s_rgiGuidOrder[i]];
				byte b2 = y.m_value[SqlGuid.s_rgiGuidOrder[i]];
				if (b != b2)
				{
					if (b >= b2)
					{
						return EComparison.GT;
					}
					return EComparison.LT;
				}
				else
				{
					i++;
				}
			}
			return EComparison.EQ;
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlGuid" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlGuid" /> whose <see cref="P:System.Data.SqlTypes.SqlGuid.Value" /> equals the value represented by the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" /> object. </param>
		// Token: 0x06001FB2 RID: 8114 RVA: 0x00098F4C File Offset: 0x0009714C
		public static explicit operator SqlGuid(SqlString x)
		{
			if (!x.IsNull)
			{
				return new SqlGuid(x.Value);
			}
			return SqlGuid.Null;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlBinary" /> parameter to <see cref="T:System.Data.SqlTypes.SqlGuid" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlGuid" /> whose <see cref="P:System.Data.SqlTypes.SqlGuid.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlBinary.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBinary" /> parameter.</returns>
		/// <param name="x">A SqlBinary object. </param>
		// Token: 0x06001FB3 RID: 8115 RVA: 0x00098F69 File Offset: 0x00097169
		public static explicit operator SqlGuid(SqlBinary x)
		{
			if (!x.IsNull)
			{
				return new SqlGuid(x.Value);
			}
			return SqlGuid.Null;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlGuid" /> structures to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06001FB4 RID: 8116 RVA: 0x00098F86 File Offset: 0x00097186
		public static SqlBoolean operator ==(SqlGuid x, SqlGuid y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(SqlGuid.Compare(x, y) == EComparison.EQ);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Performs a logical comparison on two <see cref="T:System.Data.SqlTypes.SqlGuid" /> structures to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06001FB5 RID: 8117 RVA: 0x00098FAF File Offset: 0x000971AF
		public static SqlBoolean operator !=(SqlGuid x, SqlGuid y)
		{
			return !(x == y);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlGuid" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06001FB6 RID: 8118 RVA: 0x00098FBD File Offset: 0x000971BD
		public static SqlBoolean operator <(SqlGuid x, SqlGuid y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(SqlGuid.Compare(x, y) == EComparison.LT);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlGuid" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06001FB7 RID: 8119 RVA: 0x00098FE6 File Offset: 0x000971E6
		public static SqlBoolean operator >(SqlGuid x, SqlGuid y)
		{
			if (!x.IsNull && !y.IsNull)
			{
				return new SqlBoolean(SqlGuid.Compare(x, y) == EComparison.GT);
			}
			return SqlBoolean.Null;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlGuid" /> to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06001FB8 RID: 8120 RVA: 0x00099010 File Offset: 0x00097210
		public static SqlBoolean operator <=(SqlGuid x, SqlGuid y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			EComparison ecomparison = SqlGuid.Compare(x, y);
			return new SqlBoolean(ecomparison == EComparison.LT || ecomparison == EComparison.EQ);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlGuid" /> to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06001FB9 RID: 8121 RVA: 0x0009904C File Offset: 0x0009724C
		public static SqlBoolean operator >=(SqlGuid x, SqlGuid y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			EComparison ecomparison = SqlGuid.Compare(x, y);
			return new SqlBoolean(ecomparison == EComparison.GT || ecomparison == EComparison.EQ);
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlGuid" /> structures to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false. If either instance is null, then the SqlGuid will be null.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06001FBA RID: 8122 RVA: 0x00099089 File Offset: 0x00097289
		public static SqlBoolean Equals(SqlGuid x, SqlGuid y)
		{
			return x == y;
		}

		/// <summary>Performs a logical comparison on two <see cref="T:System.Data.SqlTypes.SqlGuid" /> structures to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06001FBB RID: 8123 RVA: 0x00099092 File Offset: 0x00097292
		public static SqlBoolean NotEquals(SqlGuid x, SqlGuid y)
		{
			return x != y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlGuid" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06001FBC RID: 8124 RVA: 0x0009909B File Offset: 0x0009729B
		public static SqlBoolean LessThan(SqlGuid x, SqlGuid y)
		{
			return x < y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlGuid" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06001FBD RID: 8125 RVA: 0x000990A4 File Offset: 0x000972A4
		public static SqlBoolean GreaterThan(SqlGuid x, SqlGuid y)
		{
			return x > y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlGuid" /> to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06001FBE RID: 8126 RVA: 0x000990AD File Offset: 0x000972AD
		public static SqlBoolean LessThanOrEqual(SqlGuid x, SqlGuid y)
		{
			return x <= y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlGuid" /> to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06001FBF RID: 8127 RVA: 0x000990B6 File Offset: 0x000972B6
		public static SqlBoolean GreaterThanOrEqual(SqlGuid x, SqlGuid y)
		{
			return x >= y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> structure that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001FC0 RID: 8128 RVA: 0x000990BF File Offset: 0x000972BF
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to <see cref="T:System.Data.SqlTypes.SqlBinary" />.</summary>
		/// <returns>A SqlBinary structure that contains the bytes in the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure.</returns>
		// Token: 0x06001FC1 RID: 8129 RVA: 0x000990CC File Offset: 0x000972CC
		public SqlBinary ToSqlBinary()
		{
			return (SqlBinary)this;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to the supplied object and returns an indication of their relative values. Compares more than the last 6 bytes, but treats the last 6 bytes as the most significant ones in comparisons.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return Value Condition Less than zero This instance is less than object. Zero This instance is the same as object. Greater than zero This instance is greater than object -or- object is a null reference (Nothing) </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001FC2 RID: 8130 RVA: 0x000990DC File Offset: 0x000972DC
		public int CompareTo(object value)
		{
			if (value is SqlGuid)
			{
				SqlGuid sqlGuid = (SqlGuid)value;
				return this.CompareTo(sqlGuid);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlGuid));
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to the supplied <see cref="T:System.Data.SqlTypes.SqlGuid" /> and returns an indication of their relative values. Compares more than the last 6 bytes, but treats the last 6 bytes as the most significant ones in comparisons.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return Value Condition Less than zero This instance is less than object. Zero This instance is the same as object. Greater than zero This instance is greater than object -or- object is a null reference (Nothing). </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlGuid" /> to be compared.</param>
		// Token: 0x06001FC3 RID: 8131 RVA: 0x00099118 File Offset: 0x00097318
		public int CompareTo(SqlGuid value)
		{
			if (this.IsNull)
			{
				if (!value.IsNull)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (value.IsNull)
				{
					return 1;
				}
				if (this < value)
				{
					return -1;
				}
				if (this > value)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlGuid.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> object.</summary>
		/// <returns>true if object is an instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x06001FC4 RID: 8132 RVA: 0x00099170 File Offset: 0x00097370
		public override bool Equals(object value)
		{
			if (!(value is SqlGuid))
			{
				return false;
			}
			SqlGuid sqlGuid = (SqlGuid)value;
			if (sqlGuid.IsNull || this.IsNull)
			{
				return sqlGuid.IsNull && this.IsNull;
			}
			return (this == sqlGuid).Value;
		}

		/// <summary>Returns the hash code of this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001FC5 RID: 8133 RVA: 0x000991C8 File Offset: 0x000973C8
		public override int GetHashCode()
		{
			if (!this.IsNull)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An XmlSchema.</returns>
		// Token: 0x06001FC6 RID: 8134 RVA: 0x00004526 File Offset: 0x00002726
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x06001FC7 RID: 8135 RVA: 0x000991F4 File Offset: 0x000973F4
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_value = null;
				return;
			}
			this.m_value = new Guid(reader.ReadElementString()).ToByteArray();
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter </param>
		// Token: 0x06001FC8 RID: 8136 RVA: 0x00099245 File Offset: 0x00097445
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(XmlConvert.ToString(new Guid(this.m_value)));
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06001FC9 RID: 8137 RVA: 0x00094B5B File Offset: 0x00092D5B
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04001634 RID: 5684
		private static readonly int s_sizeOfGuid = 16;

		// Token: 0x04001635 RID: 5685
		private static readonly int[] s_rgiGuidOrder = new int[]
		{
			10, 11, 12, 13, 14, 15, 8, 9, 6, 7,
			4, 5, 0, 1, 2, 3
		};

		// Token: 0x04001636 RID: 5686
		private byte[] m_value;

		/// <summary>Represents a <see cref="T:System.DBNull" />  that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure.</summary>
		// Token: 0x04001637 RID: 5687
		public static readonly SqlGuid Null = new SqlGuid(true);
	}
}
