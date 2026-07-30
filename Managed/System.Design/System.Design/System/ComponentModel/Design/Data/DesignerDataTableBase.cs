using System;
using System.Collections;

namespace System.ComponentModel.Design.Data
{
	/// <summary>Defines the properties and methods shared between data-store tables and data-store views.</summary>
	// Token: 0x0200016E RID: 366
	public abstract class DesignerDataTableBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataTableBase" /> class. </summary>
		/// <param name="name">The name of the table or view.</param>
		// Token: 0x06000AEF RID: 2799 RVA: 0x000165F6 File Offset: 0x000147F6
		protected DesignerDataTableBase(string name)
			: this(name, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataTableBase" /> class. </summary>
		/// <param name="name">The name of the table or view.</param>
		/// <param name="owner">The data-store owner of the table or view.</param>
		// Token: 0x06000AF0 RID: 2800 RVA: 0x00016600 File Offset: 0x00014800
		protected DesignerDataTableBase(string name, string owner)
		{
			this.name = name;
			this.owner = owner;
		}

		/// <summary>Gets the name of the data-store table or view.</summary>
		/// <returns>The name of the data-store table or view.</returns>
		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x00016616 File Offset: 0x00014816
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the owner of the data-store table or view.</summary>
		/// <returns>The owner of the data-store table or view.</returns>
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0001661E File Offset: 0x0001481E
		public string Owner
		{
			get
			{
				return this.owner;
			}
		}

		/// <summary>Gets a collection of columns defined for a table or view.</summary>
		/// <returns>A collection of columns defined for a table or view.</returns>
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x0000234B File Offset: 0x0000054B
		[MonoTODO]
		public ICollection Columns
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, returns a collection of data-store column objects.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.Design.Data.DesignerDataColumn" /> objects.</returns>
		// Token: 0x06000AF4 RID: 2804
		protected abstract ICollection CreateColumns();

		// Token: 0x04000292 RID: 658
		private string name;

		// Token: 0x04000293 RID: 659
		private string owner;
	}
}
