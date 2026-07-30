using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a shortcut menu. </summary>
	// Token: 0x020000A3 RID: 163
	[ClassInterface(1)]
	[ComVisible(true)]
	[DefaultEvent("Opening")]
	public class ContextMenuStrip : ToolStripDropDownMenu
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> class. </summary>
		// Token: 0x060007E3 RID: 2019 RVA: 0x00022D28 File Offset: 0x00020F28
		public ContextMenuStrip()
		{
			this.source_control = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> class and associates it with the specified container.</summary>
		/// <param name="container">A component that implements <see cref="T:System.ComponentModel.IContainer" /> that is the container of the <see cref="T:System.Windows.Forms.ContextMenuStrip" />.</param>
		// Token: 0x060007E4 RID: 2020 RVA: 0x00022D38 File Offset: 0x00020F38
		public ContextMenuStrip(IContainer container)
		{
			this.source_control = null;
		}

		/// <summary>Gets the last control that caused this <see cref="T:System.Windows.Forms.ContextMenuStrip" /> to be displayed.</summary>
		/// <returns>The control that caused this <see cref="T:System.Windows.Forms.ContextMenuStrip" /> to be displayed.</returns>
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00022D48 File Offset: 0x00020F48
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public Control SourceControl
		{
			get
			{
				return this.source_control;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060007E6 RID: 2022 RVA: 0x00022D50 File Offset: 0x00020F50
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <param name="visible">true to make the control visible; otherwise, false.</param>
		// Token: 0x060007E7 RID: 2023 RVA: 0x00022D5C File Offset: 0x00020F5C
		protected override void SetVisibleCore(bool visible)
		{
			base.SetVisibleCore(visible);
			if (visible)
			{
				XplatUI.SetTopmost(this.Handle, true);
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00022D78 File Offset: 0x00020F78
		internal void SetSourceControl(Control source_control)
		{
			this.source_control = source_control;
			this.container = source_control;
		}

		// Token: 0x04000797 RID: 1943
		private Control source_control;

		// Token: 0x04000798 RID: 1944
		internal Control container;
	}
}
