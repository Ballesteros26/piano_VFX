using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Implements the basic functionality that represents the appearance and behavior of a table layout.</summary>
	// Token: 0x0200030D RID: 781
	[TypeConverter(typeof(TableLayoutSettings.StyleConverter))]
	public abstract class TableLayoutStyle
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TableLayoutStyle" /> class.</summary>
		// Token: 0x060033D4 RID: 13268 RVA: 0x000C4390 File Offset: 0x000C2590
		protected TableLayoutStyle()
		{
			this.size_type = SizeType.AutoSize;
		}

		/// <summary>Gets or sets a flag indicating how a row or column should be sized relative to its containing table.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.SizeType" /> values that specifies how rows or columns of user interface (UI) elements should be sized relative to their container. The default is <see cref="F:System.Windows.Forms.SizeType.AutoSize" />.</returns>
		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x060033D5 RID: 13269 RVA: 0x000C43A0 File Offset: 0x000C25A0
		// (set) Token: 0x060033D6 RID: 13270 RVA: 0x000C43A8 File Offset: 0x000C25A8
		[DefaultValue(SizeType.AutoSize)]
		public SizeType SizeType
		{
			get
			{
				return this.size_type;
			}
			set
			{
				if (this.size_type != value)
				{
					this.size_type = value;
					if (this.owner != null)
					{
						this.owner.PerformLayout();
					}
				}
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x060033D7 RID: 13271 RVA: 0x000C43D4 File Offset: 0x000C25D4
		// (set) Token: 0x060033D8 RID: 13272 RVA: 0x000C43DC File Offset: 0x000C25DC
		internal TableLayoutPanel Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				this.owner = value;
			}
		}

		// Token: 0x04001887 RID: 6279
		private SizeType size_type;

		// Token: 0x04001888 RID: 6280
		private TableLayoutPanel owner;
	}
}
