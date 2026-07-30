using System;
using System.ComponentModel;

namespace System.Drawing.Printing
{
	/// <summary>Provides data for the <see cref="E:System.Drawing.Printing.PrintDocument.BeginPrint" /> and <see cref="E:System.Drawing.Printing.PrintDocument.EndPrint" /> events.</summary>
	// Token: 0x020000CD RID: 205
	public class PrintEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintEventArgs" /> class.</summary>
		// Token: 0x06000AF2 RID: 2802 RVA: 0x00017E45 File Offset: 0x00016045
		public PrintEventArgs()
		{
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00017E4D File Offset: 0x0001604D
		internal PrintEventArgs(PrintAction action)
		{
			this.action = action;
		}

		/// <summary>Returns <see cref="F:System.Drawing.Printing.PrintAction.PrintToFile" /> in all cases.</summary>
		/// <returns>
		///   <see cref="F:System.Drawing.Printing.PrintAction.PrintToFile" /> in all cases.</returns>
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00017E5C File Offset: 0x0001605C
		public PrintAction PrintAction
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x00017E64 File Offset: 0x00016064
		// (set) Token: 0x06000AF6 RID: 2806 RVA: 0x00017E6C File Offset: 0x0001606C
		internal GraphicsPrinter GraphicsContext
		{
			get
			{
				return this.graphics_context;
			}
			set
			{
				this.graphics_context = value;
			}
		}

		// Token: 0x04000713 RID: 1811
		private GraphicsPrinter graphics_context;

		// Token: 0x04000714 RID: 1812
		private PrintAction action;
	}
}
