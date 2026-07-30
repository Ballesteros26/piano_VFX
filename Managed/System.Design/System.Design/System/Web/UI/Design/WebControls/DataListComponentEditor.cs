using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides a component editor for a <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	// Token: 0x020000CE RID: 206
	public class DataListComponentEditor : BaseDataListComponentEditor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.DataListComponentEditor" /> class.</summary>
		// Token: 0x0600060A RID: 1546 RVA: 0x00009749 File Offset: 0x00007949
		public DataListComponentEditor()
			: base(0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.DataListComponentEditor" /> class, and sets its initial page to the specified index.</summary>
		/// <param name="initialPage">The index of the initial page.</param>
		// Token: 0x0600060B RID: 1547 RVA: 0x00009752 File Offset: 0x00007952
		public DataListComponentEditor(int initialPage)
			: base(initialPage)
		{
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0000234B File Offset: 0x0000054B
		public override bool EditComponent(ITypeDescriptorContext context, object obj, IWin32Window parent)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an array of <see cref="T:System.Type" /> objects corresponding to the pages that can be edited using this editor.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects corresponding to the pages that can be edited using this editor.</returns>
		// Token: 0x0600060D RID: 1549 RVA: 0x0000234B File Offset: 0x0000054B
		protected override Type[] GetComponentEditorPages()
		{
			throw new NotImplementedException();
		}
	}
}
