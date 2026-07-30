using System;
using System.Data;

namespace System.ComponentModel.Design.Data
{
	/// <summary>Represents a parameter for a stored procedure. This class cannot be inherited.</summary>
	// Token: 0x02000169 RID: 361
	public sealed class DesignerDataParameter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataParameter" /> class with the specified name, data type, and input/output semantics. </summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="dataType">One of the <see cref="T:System.Data.DbType" /> values.</param>
		/// <param name="direction">One of the <see cref="T:System.Data.ParameterDirection" /> values.</param>
		// Token: 0x06000ADA RID: 2778 RVA: 0x00016519 File Offset: 0x00014719
		public DesignerDataParameter(string name, DbType dataType, ParameterDirection direction)
		{
			this.name = name;
			this.type = dataType;
			this.direction = direction;
		}

		/// <summary>Gets the database type of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.DbType" /> values.</returns>
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x00016536 File Offset: 0x00014736
		public DbType DataType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets the name of the parameter.</summary>
		/// <returns>The name of the parameter.</returns>
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0001653E File Offset: 0x0001473E
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets a value indicating whether the parameter is input-only, output-only, bidirectional, or a stored procedure return-value parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values.</returns>
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x00016546 File Offset: 0x00014746
		public ParameterDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		// Token: 0x04000286 RID: 646
		private string name;

		// Token: 0x04000287 RID: 647
		private DbType type;

		// Token: 0x04000288 RID: 648
		private ParameterDirection direction;
	}
}
