using System;
using System.ComponentModel.Design;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides a user interface for editing the collection of cells in a table row.</summary>
	// Token: 0x020000DB RID: 219
	public class TableCellsCollectionEditor : CollectionEditor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.TableCellsCollectionEditor" /> class.</summary>
		/// <param name="type">The type of the collection to edit. </param>
		// Token: 0x06000669 RID: 1641 RVA: 0x00005128 File Offset: 0x00003328
		public TableCellsCollectionEditor(Type type)
			: base(type)
		{
		}

		/// <summary>Indicates whether multiple table cells can be selected at the same time.</summary>
		/// <returns>true if multiple cells can be selected at the same time; otherwise, false.</returns>
		// Token: 0x0600066A RID: 1642 RVA: 0x0000234B File Offset: 0x0000054B
		protected override bool CanSelectMultipleInstances()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an instance of the editor for use with the specified type.</summary>
		/// <returns>An object of the specified type.</returns>
		/// <param name="itemType">The <see cref="T:System.Type" /> of the item to create. </param>
		// Token: 0x0600066B RID: 1643 RVA: 0x0000234B File Offset: 0x0000054B
		protected override object CreateInstance(Type itemType)
		{
			throw new NotImplementedException();
		}
	}
}
