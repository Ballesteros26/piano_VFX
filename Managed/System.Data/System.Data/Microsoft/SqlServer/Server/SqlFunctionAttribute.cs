using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Used to mark a method definition of a user-defined aggregate as a function in SQL Server. The properties on the attribute reflect the physical characteristics used when the type is registered with SQL Server.</summary>
	// Token: 0x020003B8 RID: 952
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public class SqlFunctionAttribute : Attribute
	{
		/// <summary>An optional attribute on a user-defined aggregate, used to indicate that the method should be registered in SQL Server as a function. Also used to set the <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.DataAccess" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.FillRowMethodName" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.IsDeterministic" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.IsPrecise" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.Name" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.SystemDataAccess" />, and <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.TableDefinition" /> properties of the function attribute.</summary>
		// Token: 0x06002DFD RID: 11773 RVA: 0x000C81DF File Offset: 0x000C63DF
		public SqlFunctionAttribute()
		{
			this.m_fDeterministic = false;
			this.m_eDataAccess = DataAccessKind.None;
			this.m_eSystemDataAccess = SystemDataAccessKind.None;
			this.m_fPrecise = false;
			this.m_fName = null;
			this.m_fTableDefinition = null;
			this.m_FillRowMethodName = null;
		}

		/// <summary>Indicates whether the user-defined function is deterministic.</summary>
		/// <returns>true if the function is deterministic; otherwise false.</returns>
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002DFE RID: 11774 RVA: 0x000C8218 File Offset: 0x000C6418
		// (set) Token: 0x06002DFF RID: 11775 RVA: 0x000C8220 File Offset: 0x000C6420
		public bool IsDeterministic
		{
			get
			{
				return this.m_fDeterministic;
			}
			set
			{
				this.m_fDeterministic = value;
			}
		}

		/// <summary>Indicates whether the function involves access to user data stored in the local instance of SQL Server.</summary>
		/// <returns>
		///   <see cref="T:Microsoft.SqlServer.Server.DataAccessKind" />.None: Does not access data. <see cref="T:Microsoft.SqlServer.Server.DataAccessKind" />.Read: Only reads user data.</returns>
		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06002E00 RID: 11776 RVA: 0x000C8229 File Offset: 0x000C6429
		// (set) Token: 0x06002E01 RID: 11777 RVA: 0x000C8231 File Offset: 0x000C6431
		public DataAccessKind DataAccess
		{
			get
			{
				return this.m_eDataAccess;
			}
			set
			{
				this.m_eDataAccess = value;
			}
		}

		/// <summary>Indicates whether the function requires access to data stored in the system catalogs or virtual system tables of SQL Server.</summary>
		/// <returns>
		///   <see cref="T:Microsoft.SqlServer.Server.DataAccessKind" />.None: Does not access system data. <see cref="T:Microsoft.SqlServer.Server.DataAccessKind" />.Read: Only reads system data.</returns>
		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06002E02 RID: 11778 RVA: 0x000C823A File Offset: 0x000C643A
		// (set) Token: 0x06002E03 RID: 11779 RVA: 0x000C8242 File Offset: 0x000C6442
		public SystemDataAccessKind SystemDataAccess
		{
			get
			{
				return this.m_eSystemDataAccess;
			}
			set
			{
				this.m_eSystemDataAccess = value;
			}
		}

		/// <summary>Indicates whether the function involves imprecise computations, such as floating point operations.</summary>
		/// <returns>true if the function involves precise computations; otherwise false.</returns>
		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06002E04 RID: 11780 RVA: 0x000C824B File Offset: 0x000C644B
		// (set) Token: 0x06002E05 RID: 11781 RVA: 0x000C8253 File Offset: 0x000C6453
		public bool IsPrecise
		{
			get
			{
				return this.m_fPrecise;
			}
			set
			{
				this.m_fPrecise = value;
			}
		}

		/// <summary>The name under which the function should be registered in SQL Server.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the name under which the function should be registered.</returns>
		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06002E06 RID: 11782 RVA: 0x000C825C File Offset: 0x000C645C
		// (set) Token: 0x06002E07 RID: 11783 RVA: 0x000C8264 File Offset: 0x000C6464
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

		/// <summary>A string that represents the table definition of the results, if the method is used as a table-valued function (TVF).</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the table definition of the results.</returns>
		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06002E08 RID: 11784 RVA: 0x000C826D File Offset: 0x000C646D
		// (set) Token: 0x06002E09 RID: 11785 RVA: 0x000C8275 File Offset: 0x000C6475
		public string TableDefinition
		{
			get
			{
				return this.m_fTableDefinition;
			}
			set
			{
				this.m_fTableDefinition = value;
			}
		}

		/// <summary>The name of a method in the same class as the table-valued function (TVF) that is used by the TVF contract.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the name of a method used by the TVF contract.</returns>
		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06002E0A RID: 11786 RVA: 0x000C827E File Offset: 0x000C647E
		// (set) Token: 0x06002E0B RID: 11787 RVA: 0x000C8286 File Offset: 0x000C6486
		public string FillRowMethodName
		{
			get
			{
				return this.m_FillRowMethodName;
			}
			set
			{
				this.m_FillRowMethodName = value;
			}
		}

		// Token: 0x04001B3C RID: 6972
		private bool m_fDeterministic;

		// Token: 0x04001B3D RID: 6973
		private DataAccessKind m_eDataAccess;

		// Token: 0x04001B3E RID: 6974
		private SystemDataAccessKind m_eSystemDataAccess;

		// Token: 0x04001B3F RID: 6975
		private bool m_fPrecise;

		// Token: 0x04001B40 RID: 6976
		private string m_fName;

		// Token: 0x04001B41 RID: 6977
		private string m_fTableDefinition;

		// Token: 0x04001B42 RID: 6978
		private string m_FillRowMethodName;
	}
}
