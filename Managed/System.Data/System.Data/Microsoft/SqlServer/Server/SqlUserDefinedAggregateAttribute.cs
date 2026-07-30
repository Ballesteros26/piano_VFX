using System;
using System.Data.Common;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Indicates that the type should be registered as a user-defined aggregate. The properties on the attribute reflect the physical attributes used when the type is registered with SQL Server. This class cannot be inherited.</summary>
	// Token: 0x020003BC RID: 956
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public sealed class SqlUserDefinedAggregateAttribute : Attribute
	{
		/// <summary>A required attribute on a user-defined aggregate, used to indicate that the given type is a user-defined aggregate and the storage format of the user-defined aggregate.</summary>
		/// <param name="format">One of the <see cref="T:Microsoft.SqlServer.Server.Format" /> values representing the serialization format of the aggregate.</param>
		// Token: 0x06002E1D RID: 11805 RVA: 0x000C834F File Offset: 0x000C654F
		public SqlUserDefinedAggregateAttribute(Format format)
		{
			if (format == Format.Unknown)
			{
				throw ADP.NotSupportedUserDefinedTypeSerializationFormat(format, "format");
			}
			if (format - Format.Native > 1)
			{
				throw ADP.InvalidUserDefinedTypeSerializationFormat(format);
			}
			this.m_format = format;
		}

		/// <summary>The maximum size, in bytes, of the aggregate instance.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value representing the maximum size of the aggregate instance.</returns>
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002E1E RID: 11806 RVA: 0x000C8383 File Offset: 0x000C6583
		// (set) Token: 0x06002E1F RID: 11807 RVA: 0x000C838B File Offset: 0x000C658B
		public int MaxByteSize
		{
			get
			{
				return this.m_MaxByteSize;
			}
			set
			{
				if (value < -1 || value > 8000)
				{
					throw ADP.ArgumentOutOfRange(Res.GetString("range: 0-8000"), "MaxByteSize", value);
				}
				this.m_MaxByteSize = value;
			}
		}

		/// <summary>Indicates whether the aggregate is invariant to duplicates.</summary>
		/// <returns>true if the aggregate is invariant to duplicates; otherwise false.</returns>
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06002E20 RID: 11808 RVA: 0x000C83BB File Offset: 0x000C65BB
		// (set) Token: 0x06002E21 RID: 11809 RVA: 0x000C83C3 File Offset: 0x000C65C3
		public bool IsInvariantToDuplicates
		{
			get
			{
				return this.m_fInvariantToDup;
			}
			set
			{
				this.m_fInvariantToDup = value;
			}
		}

		/// <summary>Indicates whether the aggregate is invariant to nulls.</summary>
		/// <returns>true if the aggregate is invariant to nulls; otherwise false.</returns>
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002E22 RID: 11810 RVA: 0x000C83CC File Offset: 0x000C65CC
		// (set) Token: 0x06002E23 RID: 11811 RVA: 0x000C83D4 File Offset: 0x000C65D4
		public bool IsInvariantToNulls
		{
			get
			{
				return this.m_fInvariantToNulls;
			}
			set
			{
				this.m_fInvariantToNulls = value;
			}
		}

		/// <summary>Indicates whether the aggregate is invariant to order.</summary>
		/// <returns>true if the aggregate is invariant to order; otherwise false.</returns>
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002E24 RID: 11812 RVA: 0x000C83DD File Offset: 0x000C65DD
		// (set) Token: 0x06002E25 RID: 11813 RVA: 0x000C83E5 File Offset: 0x000C65E5
		public bool IsInvariantToOrder
		{
			get
			{
				return this.m_fInvariantToOrder;
			}
			set
			{
				this.m_fInvariantToOrder = value;
			}
		}

		/// <summary>Indicates whether the aggregate returns null if no values have been accumulated.</summary>
		/// <returns>true if the aggregate returns null if no values have been accumulated; otherwise false.</returns>
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002E26 RID: 11814 RVA: 0x000C83EE File Offset: 0x000C65EE
		// (set) Token: 0x06002E27 RID: 11815 RVA: 0x000C83F6 File Offset: 0x000C65F6
		public bool IsNullIfEmpty
		{
			get
			{
				return this.m_fNullIfEmpty;
			}
			set
			{
				this.m_fNullIfEmpty = value;
			}
		}

		/// <summary>The serialization format as a <see cref="T:Microsoft.SqlServer.Server.Format" />.</summary>
		/// <returns>A <see cref="T:Microsoft.SqlServer.Server.Format" /> representing the serialization format.</returns>
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06002E28 RID: 11816 RVA: 0x000C83FF File Offset: 0x000C65FF
		public Format Format
		{
			get
			{
				return this.m_format;
			}
		}

		/// <summary>The name of the aggregate.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the name of the aggregate.</returns>
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06002E29 RID: 11817 RVA: 0x000C8407 File Offset: 0x000C6607
		// (set) Token: 0x06002E2A RID: 11818 RVA: 0x000C840F File Offset: 0x000C660F
		public string Name
		{
			get
			{
				return this.m_fName;
			}
			set
			{
				this.m_fName = value;
			}
		}

		// Token: 0x04001B4A RID: 6986
		private int m_MaxByteSize;

		// Token: 0x04001B4B RID: 6987
		private bool m_fInvariantToDup;

		// Token: 0x04001B4C RID: 6988
		private bool m_fInvariantToNulls;

		// Token: 0x04001B4D RID: 6989
		private bool m_fInvariantToOrder = true;

		// Token: 0x04001B4E RID: 6990
		private bool m_fNullIfEmpty;

		// Token: 0x04001B4F RID: 6991
		private Format m_format;

		// Token: 0x04001B50 RID: 6992
		private string m_fName;

		/// <summary>The maximum size, in bytes, required to store the state of this aggregate instance during computation.</summary>
		// Token: 0x04001B51 RID: 6993
		public const int MaxByteSizeValue = 8000;
	}
}
