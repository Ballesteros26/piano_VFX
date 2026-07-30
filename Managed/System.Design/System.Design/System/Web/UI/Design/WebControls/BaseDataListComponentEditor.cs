using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides a component editor base class for Web server controls that are derived from the <see cref="T:System.Web.UI.WebControls.BaseDataList" /> class. </summary>
	// Token: 0x020000C5 RID: 197
	public abstract class BaseDataListComponentEditor : WindowsFormsComponentEditor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.BaseDataListComponentEditor" /> class.</summary>
		/// <param name="initialPage">The index in the array of page control types, of the initial page to display. </param>
		// Token: 0x060005BC RID: 1468 RVA: 0x000096B9 File Offset: 0x000078B9
		public BaseDataListComponentEditor(int initialPage)
		{
			this.initial_page = initialPage;
		}

		/// <summary>Edits the specified component by using the specified context descriptor and parent window.</summary>
		/// <returns>true the component was successfully edited; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that can be used to gain additional context information. </param>
		/// <param name="obj">An <see cref="T:System.Object" /> implementing the <see cref="T:System.ComponentModel.IComponent" />, which represents the component to edit. </param>
		/// <param name="parent">The <see cref="T:System.Windows.Forms.IWin32Window" /> that represents the parent window. </param>
		// Token: 0x060005BD RID: 1469 RVA: 0x0000234B File Offset: 0x0000054B
		public override bool EditComponent(ITypeDescriptorContext context, object obj, IWin32Window parent)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the index of the initial page to display in the component editor.</summary>
		/// <returns>The index of the initial page in the array.</returns>
		// Token: 0x060005BE RID: 1470 RVA: 0x000096C8 File Offset: 0x000078C8
		protected override int GetInitialComponentEditorPageIndex()
		{
			return this.initial_page;
		}

		// Token: 0x0400014B RID: 331
		private int initial_page;
	}
}
