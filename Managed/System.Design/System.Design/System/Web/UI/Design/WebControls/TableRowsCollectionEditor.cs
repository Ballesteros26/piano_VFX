using System;
using System.ComponentModel.Design;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides a user interface for editing rows of a table.</summary>
	// Token: 0x020000DD RID: 221
	public class TableRowsCollectionEditor : CollectionEditor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.TableRowsCollectionEditor" /> class.</summary>
		/// <param name="type">The type of the collection to edit. </param>
		// Token: 0x0600066F RID: 1647 RVA: 0x00005128 File Offset: 0x00003328
		public TableRowsCollectionEditor(Type type)
			: base(type)
		{
		}

		/// <summary>Indicates whether multiple instances may be selected.</summary>
		/// <returns>true if multiple items can be selected at once; otherwise, false. This implementation always returns false.</returns>
		// Token: 0x06000670 RID: 1648 RVA: 0x0000234B File Offset: 0x0000054B
		protected override bool CanSelectMultipleInstances()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an instance of the specified type.</summary>
		/// <returns>An object of the specified type.</returns>
		/// <param name="itemType">The <see cref="T:System.Type" /> of the item to create. </param>
		// Token: 0x06000671 RID: 1649 RVA: 0x0000234B File Offset: 0x0000054B
		protected override object CreateInstance(Type itemType)
		{
			throw new NotImplementedException();
		}
	}
}
