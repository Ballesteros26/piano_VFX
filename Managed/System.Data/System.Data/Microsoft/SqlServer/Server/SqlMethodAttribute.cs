using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Indicates the determinism and data access properties of a method or property on a user-defined type (UDT). The properties on the attribute reflect the physical characteristics that are used when the type is registered with SQL Server.</summary>
	// Token: 0x020003B9 RID: 953
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class SqlMethodAttribute : SqlFunctionAttribute
	{
		/// <summary>An attribute on a user-defined type (UDT), used to indicate the determinism and data access properties of a method or a property on a UDT.</summary>
		// Token: 0x06002E0C RID: 11788 RVA: 0x000C828F File Offset: 0x000C648F
		public SqlMethodAttribute()
		{
			this.m_fCallOnNullInputs = true;
			this.m_fMutator = false;
			this.m_fInvokeIfReceiverIsNull = false;
		}

		/// <summary>Indicates whether the method on a user-defined type (UDT) is called when null input arguments are specified in the method invocation.</summary>
		/// <returns>true if the method is called when null input arguments are specified in the method invocation; false if the method returns a null value when any of its input parameters are null. If the method cannot be invoked (because of an attribute on the method), the SQL Server DbNull is returned.</returns>
		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002E0D RID: 11789 RVA: 0x000C82AC File Offset: 0x000C64AC
		// (set) Token: 0x06002E0E RID: 11790 RVA: 0x000C82B4 File Offset: 0x000C64B4
		public bool OnNullCall
		{
			get
			{
				return this.m_fCallOnNullInputs;
			}
			set
			{
				this.m_fCallOnNullInputs = value;
			}
		}

		/// <summary>Indicates whether a method on a user-defined type (UDT) is a mutator.</summary>
		/// <returns>true if the method is a mutator; otherwise false.</returns>
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06002E0F RID: 11791 RVA: 0x000C82BD File Offset: 0x000C64BD
		// (set) Token: 0x06002E10 RID: 11792 RVA: 0x000C82C5 File Offset: 0x000C64C5
		public bool IsMutator
		{
			get
			{
				return this.m_fMutator;
			}
			set
			{
				this.m_fMutator = value;
			}
		}

		/// <summary>Indicates whether SQL Server should invoke the method on null instances.</summary>
		/// <returns>true if SQL Server should invoke the method on null instances; otherwise false. If the method cannot be invoked (because of an attribute on the method), the SQL Server DbNull is returned.</returns>
		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06002E11 RID: 11793 RVA: 0x000C82CE File Offset: 0x000C64CE
		// (set) Token: 0x06002E12 RID: 11794 RVA: 0x000C82D6 File Offset: 0x000C64D6
		public bool InvokeIfReceiverIsNull
		{
			get
			{
				return this.m_fInvokeIfReceiverIsNull;
			}
			set
			{
				this.m_fInvokeIfReceiverIsNull = value;
			}
		}

		// Token: 0x04001B43 RID: 6979
		private bool m_fCallOnNullInputs;

		// Token: 0x04001B44 RID: 6980
		private bool m_fMutator;

		// Token: 0x04001B45 RID: 6981
		private bool m_fInvokeIfReceiverIsNull;
	}
}
