using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Used to mark a method definition in an assembly as a trigger in SQL Server. The properties on the attribute reflect the physical attributes used when the type is registered with SQL Server. This class cannot be inherited.</summary>
	// Token: 0x020003BB RID: 955
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class SqlTriggerAttribute : Attribute
	{
		/// <summary>An attribute on a method definition in an assembly, used to mark the method as a trigger in SQL Server.</summary>
		// Token: 0x06002E16 RID: 11798 RVA: 0x000C82FF File Offset: 0x000C64FF
		public SqlTriggerAttribute()
		{
			this.m_fName = null;
			this.m_fTarget = null;
			this.m_fEvent = null;
		}

		/// <summary>The name of the trigger.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the name of the trigger.</returns>
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06002E17 RID: 11799 RVA: 0x000C831C File Offset: 0x000C651C
		// (set) Token: 0x06002E18 RID: 11800 RVA: 0x000C8324 File Offset: 0x000C6524
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

		/// <summary>The table to which the trigger applies.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the table name.</returns>
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x000C832D File Offset: 0x000C652D
		// (set) Token: 0x06002E1A RID: 11802 RVA: 0x000C8335 File Offset: 0x000C6535
		public string Target
		{
			get
			{
				return this.m_fTarget;
			}
			set
			{
				this.m_fTarget = value;
			}
		}

		/// <summary>The type of trigger and what data manipulation language (DML) action activates the trigger.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the type of trigger and what data manipulation language (DML) action activates the trigger.</returns>
		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002E1B RID: 11803 RVA: 0x000C833E File Offset: 0x000C653E
		// (set) Token: 0x06002E1C RID: 11804 RVA: 0x000C8346 File Offset: 0x000C6546
		public string Event
		{
			get
			{
				return this.m_fEvent;
			}
			set
			{
				this.m_fEvent = value;
			}
		}

		// Token: 0x04001B47 RID: 6983
		private string m_fName;

		// Token: 0x04001B48 RID: 6984
		private string m_fTarget;

		// Token: 0x04001B49 RID: 6985
		private string m_fEvent;
	}
}
