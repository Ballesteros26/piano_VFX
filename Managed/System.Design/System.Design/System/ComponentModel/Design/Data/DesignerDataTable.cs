using System;
using System.Collections;

namespace System.ComponentModel.Design.Data
{
	/// <summary>Represents a table in the data store.</summary>
	// Token: 0x0200016D RID: 365
	public abstract class DesignerDataTable : DesignerDataTableBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataTable" /> class with the specified name. </summary>
		/// <param name="name">The name of the table.</param>
		// Token: 0x06000AEB RID: 2795 RVA: 0x000165E3 File Offset: 0x000147E3
		protected DesignerDataTable(string name)
			: base(name)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataTable" /> class with the specified name and owner. </summary>
		/// <param name="name">The name of the table.</param>
		/// <param name="owner">The owner of the table.</param>
		// Token: 0x06000AEC RID: 2796 RVA: 0x000165EC File Offset: 0x000147EC
		protected DesignerDataTable(string name, string owner)
			: base(name, owner)
		{
		}

		/// <summary>When overridden in a derived class, returns a collection of relationship objects.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.Design.Data.DesignerDataRelationship" /> objects.</returns>
		// Token: 0x06000AED RID: 2797
		protected abstract ICollection CreateRelationships();

		/// <summary>Gets a collection of relationships defined for a table.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.Design.Data.DesignerDataRelationship" /> objects.</returns>
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public ICollection Relationships
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
