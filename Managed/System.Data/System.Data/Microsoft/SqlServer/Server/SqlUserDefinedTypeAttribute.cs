using System;
using System.Data.Common;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Used to mark a type definition in an assembly as a user-defined type (UDT) in SQL Server. The properties on the attribute reflect the physical characteristics used when the type is registered with SQL Server. This class cannot be inherited.</summary>
	// Token: 0x020003BE RID: 958
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
	public sealed class SqlUserDefinedTypeAttribute : Attribute
	{
		/// <summary>A required attribute on a user-defined type (UDT), used to confirm that the given type is a UDT and to indicate the storage format of the UDT.</summary>
		/// <param name="format">One of the <see cref="T:Microsoft.SqlServer.Server.Format" /> values representing the serialization format of the type.</param>
		// Token: 0x06002E2B RID: 11819 RVA: 0x000C8418 File Offset: 0x000C6618
		public SqlUserDefinedTypeAttribute(Format format)
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

		/// <summary>The maximum size of the instance, in bytes.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value representing the maximum size of the instance.</returns>
		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06002E2C RID: 11820 RVA: 0x000C8445 File Offset: 0x000C6645
		// (set) Token: 0x06002E2D RID: 11821 RVA: 0x000C844D File Offset: 0x000C664D
		public int MaxByteSize
		{
			get
			{
				return this.m_MaxByteSize;
			}
			set
			{
				if (value < -1)
				{
					throw ADP.ArgumentOutOfRange("MaxByteSize");
				}
				this.m_MaxByteSize = value;
			}
		}

		/// <summary>Indicates whether all instances of this user-defined type are the same length.</summary>
		/// <returns>true if all instances of this type are the same length; otherwise false.</returns>
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06002E2E RID: 11822 RVA: 0x000C8465 File Offset: 0x000C6665
		// (set) Token: 0x06002E2F RID: 11823 RVA: 0x000C846D File Offset: 0x000C666D
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

		/// <summary>Indicates whether the user-defined type is byte ordered.</summary>
		/// <returns>true if the user-defined type is byte ordered; otherwise false.</returns>
		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06002E30 RID: 11824 RVA: 0x000C8476 File Offset: 0x000C6676
		// (set) Token: 0x06002E31 RID: 11825 RVA: 0x000C847E File Offset: 0x000C667E
		public bool IsByteOrdered
		{
			get
			{
				return this.m_IsByteOrdered;
			}
			set
			{
				this.m_IsByteOrdered = value;
			}
		}

		/// <summary>The serialization format as a <see cref="T:Microsoft.SqlServer.Server.Format" />.</summary>
		/// <returns>A <see cref="T:Microsoft.SqlServer.Server.Format" /> value representing the serialization format.</returns>
		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06002E32 RID: 11826 RVA: 0x000C8487 File Offset: 0x000C6687
		public Format Format
		{
			get
			{
				return this.m_format;
			}
		}

		/// <summary>The name of the method used to validate instances of the user-defined type.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the name of the method used to validate instances of the user-defined type.</returns>
		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06002E33 RID: 11827 RVA: 0x000C848F File Offset: 0x000C668F
		// (set) Token: 0x06002E34 RID: 11828 RVA: 0x000C8497 File Offset: 0x000C6697
		public string ValidationMethodName
		{
			get
			{
				return this.m_ValidationMethodName;
			}
			set
			{
				this.m_ValidationMethodName = value;
			}
		}

		/// <summary>The SQL Server name of the user-defined type.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the SQL Server name of the user-defined type.</returns>
		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06002E35 RID: 11829 RVA: 0x000C84A0 File Offset: 0x000C66A0
		// (set) Token: 0x06002E36 RID: 11830 RVA: 0x000C84A8 File Offset: 0x000C66A8
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

		// Token: 0x04001B56 RID: 6998
		private int m_MaxByteSize;

		// Token: 0x04001B57 RID: 6999
		private bool m_IsFixedLength;

		// Token: 0x04001B58 RID: 7000
		private bool m_IsByteOrdered;

		// Token: 0x04001B59 RID: 7001
		private Format m_format;

		// Token: 0x04001B5A RID: 7002
		private string m_fName;

		// Token: 0x04001B5B RID: 7003
		internal const int YukonMaxByteSizeValue = 8000;

		// Token: 0x04001B5C RID: 7004
		private string m_ValidationMethodName;
	}
}
