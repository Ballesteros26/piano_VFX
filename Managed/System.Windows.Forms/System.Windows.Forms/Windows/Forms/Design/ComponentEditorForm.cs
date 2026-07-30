using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides a user interface for a <see cref="T:System.Windows.Forms.Design.WindowsFormsComponentEditor" />.</summary>
	// Token: 0x02000012 RID: 18
	[ToolboxItem(false)]
	[ClassInterface(1)]
	[ComVisible(true)]
	public partial class ComponentEditorForm : Form
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ComponentEditorForm" /> class.</summary>
		/// <param name="component">The component to be edited. </param>
		/// <param name="pageTypes">The set of <see cref="T:System.Windows.Forms.Design.ComponentEditorPage" /> objects to be shown in the form. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="component" /> is not an <see cref="T:System.ComponentModel.IComponent" />.</exception>
		// Token: 0x06000072 RID: 114 RVA: 0x00003FF8 File Offset: 0x000021F8
		[MonoTODO]
		public ComponentEditorForm(object component, Type[] pageTypes)
		{
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000073 RID: 115 RVA: 0x00004000 File Offset: 0x00002200
		// (remove) Token: 0x06000074 RID: 116 RVA: 0x0000400C File Offset: 0x0000220C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00004018 File Offset: 0x00002218
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00004020 File Offset: 0x00002220
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new virtual bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Activated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000077 RID: 119 RVA: 0x0000402C File Offset: 0x0000222C
		[MonoTODO]
		protected override void OnActivated(EventArgs e)
		{
		}

		/// <summary>Switches between component editor pages.</summary>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.ComponentModel.Design.CheckoutException">A designer file is checked into source code control and cannot be changed.</exception>
		// Token: 0x06000078 RID: 120 RVA: 0x00004030 File Offset: 0x00002230
		[MonoTODO]
		protected virtual void OnSelChangeSelector(object source, TreeViewEventArgs e)
		{
		}

		/// <summary>Provides a method to override in order to preprocess input messages before they are dispatched.</summary>
		/// <returns>true if the specified message is for a component editor page; otherwise, false.</returns>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" /> that specifies the message to preprocess. </param>
		// Token: 0x06000079 RID: 121 RVA: 0x00004034 File Offset: 0x00002234
		[MonoTODO]
		public override bool PreProcessMessage(ref Message msg)
		{
			throw new NotImplementedException();
		}

		/// <summary>Shows the form. The form will have no owner window.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values indicating the result code returned from the dialog box.</returns>
		// Token: 0x0600007A RID: 122 RVA: 0x0000403C File Offset: 0x0000223C
		[MonoTODO]
		public virtual DialogResult ShowForm()
		{
			throw new NotImplementedException();
		}

		/// <summary>Shows the specified page of the specified form. The form will have no owner window.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values indicating the result code returned from the dialog box.</returns>
		/// <param name="page">The index of the page to show. </param>
		// Token: 0x0600007B RID: 123 RVA: 0x00004044 File Offset: 0x00002244
		[MonoTODO]
		public virtual DialogResult ShowForm(int page)
		{
			throw new NotImplementedException();
		}

		/// <summary>Shows the form with the specified owner.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values indicating the result code returned from the dialog box.</returns>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.IWin32Window" /> to own the dialog. </param>
		// Token: 0x0600007C RID: 124 RVA: 0x0000404C File Offset: 0x0000224C
		[MonoTODO]
		public virtual DialogResult ShowForm(IWin32Window owner)
		{
			throw new NotImplementedException();
		}

		/// <summary>Shows the form and the specified page with the specified owner.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values indicating the result code returned from the dialog box.</returns>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.IWin32Window" /> to own the dialog. </param>
		/// <param name="page">The index of the page to show. </param>
		// Token: 0x0600007D RID: 125 RVA: 0x00004054 File Offset: 0x00002254
		[MonoTODO]
		public virtual DialogResult ShowForm(IWin32Window owner, int page)
		{
			throw new NotImplementedException();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HelpRequested" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.HelpEventArgs" /> that contains the event data.</param>
		// Token: 0x0600007E RID: 126 RVA: 0x0000405C File Offset: 0x0000225C
		[MonoTODO]
		protected override void OnHelpRequested(HelpEventArgs e)
		{
		}
	}
}
