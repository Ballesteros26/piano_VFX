using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Used to mark a method definition in an assembly as a stored procedure. The properties on the attribute reflect the physical characteristics used when the type is registered with SQL Server. This class cannot be inherited.</summary>
	// Token: 0x020003BA RID: 954
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class SqlProcedureAttribute : Attribute
	{
		/// <summary>An attribute on a method definition in an assembly, used to indicate that the given method should be registered as a stored procedure in SQL Server.</summary>
		// Token: 0x06002E13 RID: 11795 RVA: 0x000C82DF File Offset: 0x000C64DF
		public SqlProcedureAttribute()
		{
			this.m_fName = null;
		}

		/// <summary>The name of the stored procedure.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the name of the stored procedure.</returns>
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06002E14 RID: 11796 RVA: 0x000C82EE File Offset: 0x000C64EE
		// (set) Token: 0x06002E15 RID: 11797 RVA: 0x000C82F6 File Offset: 0x000C64F6
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

		// Token: 0x04001B46 RID: 6982
		private string m_fName;
	}
}
