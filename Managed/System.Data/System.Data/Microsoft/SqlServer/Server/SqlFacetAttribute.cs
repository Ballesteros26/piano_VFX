using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Annotates the returned result of a user-defined type (UDT) with additional information that can be used in Transact-SQL.</summary>
	// Token: 0x020003B5 RID: 949
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = false)]
	public class SqlFacetAttribute : Attribute
	{
		/// <summary>Indicates whether the return type of the user-defined type is of a fixed length.</summary>
		/// <returns>true if the return type is of a fixed length; otherwise false.</returns>
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x000C818A File Offset: 0x000C638A
		// (set) Token: 0x06002DF3 RID: 11763 RVA: 0x000C8192 File Offset: 0x000C6392
		public bool IsFixedLength
		{
			get
			{
				return this.m_IsFixedLength;
			}
			set
			{
				this.m_IsFixedLength = value;
			}
		}

		/// <summary>The maximum size, in logical units, of the underlying field type of the user-defined type.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the maximum size, in logical units, of the underlying field type.</returns>
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06002DF4 RID: 11764 RVA: 0x000C819B File Offset: 0x000C639B
		// (set) Token: 0x06002DF5 RID: 11765 RVA: 0x000C81A3 File Offset: 0x000C63A3
		public int MaxSize
		{
			get
			{
				return this.m_MaxSize;
			}
			set
			{
				this.m_MaxSize = value;
			}
		}

		/// <summary>The precision of the return type of the user-defined type.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the precision of the return type.</returns>
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06002DF6 RID: 11766 RVA: 0x000C81AC File Offset: 0x000C63AC
		// (set) Token: 0x06002DF7 RID: 11767 RVA: 0x000C81B4 File Offset: 0x000C63B4
		public int Precision
		{
			get
			{
				return this.m_Precision;
			}
			set
			{
				this.m_Precision = value;
			}
		}

		/// <summary>The scale of the return type of the user-defined type.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the scale of the return type.</returns>
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06002DF8 RID: 11768 RVA: 0x000C81BD File Offset: 0x000C63BD
		// (set) Token: 0x06002DF9 RID: 11769 RVA: 0x000C81C5 File Offset: 0x000C63C5
		public int Scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		/// <summary>Indicates whether the return type of the user-defined type can be null.</summary>
		/// <returns>true if the return type of the user-defined type can be null; otherwise false.</returns>
		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002DFA RID: 11770 RVA: 0x000C81CE File Offset: 0x000C63CE
		// (set) Token: 0x06002DFB RID: 11771 RVA: 0x000C81D6 File Offset: 0x000C63D6
		public bool IsNullable
		{
			get
			{
				return this.m_IsNullable;
			}
			set
			{
				this.m_IsNullable = value;
			}
		}

		// Token: 0x04001B31 RID: 6961
		private bool m_IsFixedLength;

		// Token: 0x04001B32 RID: 6962
		private int m_MaxSize;

		// Token: 0x04001B33 RID: 6963
		private int m_Scale;

		// Token: 0x04001B34 RID: 6964
		private int m_Precision;

		// Token: 0x04001B35 RID: 6965
		private bool m_IsNullable;
	}
}
