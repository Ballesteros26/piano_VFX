using System;
using System.Collections;

namespace System.ComponentModel.Design.Data
{
	/// <summary>Represents to the designer a relationship between two tables in the data source accessed through a data connection. This class cannot be inherited.</summary>
	// Token: 0x0200016A RID: 362
	public sealed class DesignerDataRelationship
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataRelationship" /> class with the specified name, parent column, and child relationship. </summary>
		/// <param name="name">The name of the relationship.</param>
		/// <param name="parentColumns">The columns in the parent table that define the relationship.</param>
		/// <param name="childTable">The child table in the relationship.</param>
		/// <param name="childColumns">The columns in the child table that define the relationship.</param>
		// Token: 0x06000ADE RID: 2782 RVA: 0x0001654E File Offset: 0x0001474E
		public DesignerDataRelationship(string name, ICollection parentColumns, DesignerDataTable childTable, ICollection childColumns)
		{
			this.name = name;
			this.parent_columns = parentColumns;
			this.child_table = childTable;
			this.child_columns = childColumns;
		}

		/// <summary>Gets the name of the relationship.</summary>
		/// <returns>The name of the relationship.</returns>
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x00016573 File Offset: 0x00014773
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets a collection of columns from the parent table that are part of the relationship between two tables.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.Design.Data.DesignerDataColumn" /> objects that define the relationship in the parent table.</returns>
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0001657B File Offset: 0x0001477B
		public ICollection ParentColumns
		{
			get
			{
				return this.parent_columns;
			}
		}

		/// <summary>Gets the child table referenced in the relationship.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.Data.DesignerDataTable" /> object that represents the child table in the relationship.</returns>
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00016583 File Offset: 0x00014783
		public DesignerDataTable ChildTable
		{
			get
			{
				return this.child_table;
			}
		}

		/// <summary>Gets a collection of columns from the child table that are part of the relationship.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.Design.Data.DesignerDataColumn" /> objects that define the relationship in the child table.</returns>
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0001658B File Offset: 0x0001478B
		public ICollection ChildColumns
		{
			get
			{
				return this.child_columns;
			}
		}

		// Token: 0x04000289 RID: 649
		private string name;

		// Token: 0x0400028A RID: 650
		private ICollection parent_columns;

		// Token: 0x0400028B RID: 651
		private ICollection child_columns;

		// Token: 0x0400028C RID: 652
		private DesignerDataTable child_table;
	}
}
