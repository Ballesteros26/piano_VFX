using System;
using System.Collections;

namespace System.ComponentModel.Design.Data
{
	/// <summary>Represents a stored procedure in the data store.</summary>
	// Token: 0x0200016C RID: 364
	public abstract class DesignerDataStoredProcedure
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataStoredProcedure" /> class with the specified name. </summary>
		/// <param name="name">The name of the stored procedure.</param>
		// Token: 0x06000AE5 RID: 2789 RVA: 0x000165B3 File Offset: 0x000147B3
		[MonoTODO]
		protected DesignerDataStoredProcedure(string name)
			: this(name, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataStoredProcedure" /> class with the specified name and owner. </summary>
		/// <param name="name">The name of the stored procedure.</param>
		/// <param name="owner">The data store owner of the stored procedure.</param>
		// Token: 0x06000AE6 RID: 2790 RVA: 0x000165BD File Offset: 0x000147BD
		[MonoTODO]
		protected DesignerDataStoredProcedure(string name, string owner)
		{
			this.name = name;
			this.owner = owner;
		}

		/// <summary>Gets the name of the stored procedure.</summary>
		/// <returns>The name of the stored procedure.</returns>
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x000165D3 File Offset: 0x000147D3
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the owner of the stored procedure.</summary>
		/// <returns>The owner of the stored procedure.</returns>
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x000165DB File Offset: 0x000147DB
		public string Owner
		{
			get
			{
				return this.owner;
			}
		}

		/// <summary>Gets a collection of parameters required for a stored procedure.</summary>
		/// <returns>A collection of parameters for the stored procedure.</returns>
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public ICollection Parameters
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, returns a collection of parameters for the stored procedure.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.Design.Data.DesignerDataParameter" /> objects.</returns>
		// Token: 0x06000AEA RID: 2794
		protected abstract ICollection CreateParameters();

		// Token: 0x04000290 RID: 656
		private string name;

		// Token: 0x04000291 RID: 657
		private string owner;
	}
}
