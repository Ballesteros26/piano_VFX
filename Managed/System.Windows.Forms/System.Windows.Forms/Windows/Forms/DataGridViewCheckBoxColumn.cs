using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Hosts a collection of <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FA RID: 250
	[ToolboxBitmap("")]
	public class DataGridViewCheckBoxColumn : DataGridViewColumn
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxColumn" /> and configures it to display check boxes with two or three states. </summary>
		/// <param name="threeState">true to display check boxes with three states; false to display check boxes with two states. </param>
		// Token: 0x060012EC RID: 4844 RVA: 0x00049844 File Offset: 0x00047A44
		public DataGridViewCheckBoxColumn(bool threeState)
		{
			this.CellTemplate = new DataGridViewCheckBoxCell(threeState);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxColumn" /> class to the default state.</summary>
		// Token: 0x060012ED RID: 4845 RVA: 0x00049858 File Offset: 0x00047A58
		public DataGridViewCheckBoxColumn()
			: this(false)
		{
		}

		/// <summary>Gets or sets the template used to create new cells.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after. The default value is a new <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> instance.</returns>
		/// <exception cref="T:System.InvalidCastException">The property is set to a value that is not of type <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x00049864 File Offset: 0x00047A64
		// (set) Token: 0x060012EF RID: 4847 RVA: 0x0004986C File Offset: 0x00047A6C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public override DataGridViewCell CellTemplate
		{
			get
			{
				return base.CellTemplate;
			}
			set
			{
				base.CellTemplate = value as DataGridViewCheckBoxCell;
			}
		}

		/// <summary>Gets or sets the column's default cell style.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied as the default style.</returns>
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x0004987C File Offset: 0x00047A7C
		// (set) Token: 0x060012F1 RID: 4849 RVA: 0x00049884 File Offset: 0x00047A84
		[Browsable(true)]
		public override DataGridViewCellStyle DefaultCellStyle
		{
			get
			{
				return base.DefaultCellStyle;
			}
			set
			{
				base.DefaultCellStyle = value;
			}
		}

		/// <summary>Gets or sets the underlying value corresponding to a cell value of false, which appears as an unchecked box.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing a value that the cells in this column will treat as a false value. The default is null.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCheckBoxColumn.CellTemplate" /> property is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x00049890 File Offset: 0x00047A90
		// (set) Token: 0x060012F3 RID: 4851 RVA: 0x000498C4 File Offset: 0x00047AC4
		[TypeConverter(typeof(StringConverter))]
		[DefaultValue(null)]
		public object FalseValue
		{
			get
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				return (base.CellTemplate as DataGridViewCheckBoxCell).FalseValue;
			}
			set
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				(base.CellTemplate as DataGridViewCheckBoxCell).FalseValue = value;
			}
		}

		/// <summary>Gets or sets the flat style appearance of the check box cells.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FlatStyle" /> value indicating the appearance of cells in the column. The default is <see cref="F:System.Windows.Forms.FlatStyle.Standard" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCheckBoxColumn.CellTemplate" /> property is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x000498F0 File Offset: 0x00047AF0
		// (set) Token: 0x060012F5 RID: 4853 RVA: 0x00049924 File Offset: 0x00047B24
		[DefaultValue(FlatStyle.Standard)]
		public FlatStyle FlatStyle
		{
			get
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				return (base.CellTemplate as DataGridViewCheckBoxCell).FlatStyle;
			}
			set
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				(base.CellTemplate as DataGridViewCheckBoxCell).FlatStyle = value;
			}
		}

		/// <summary>Gets or sets the underlying value corresponding to an indeterminate or null cell value, which appears as a disabled checkbox.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing a value that the cells in this column will treat as an indeterminate value. The default is null.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCheckBoxColumn.CellTemplate" /> property is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x00049950 File Offset: 0x00047B50
		// (set) Token: 0x060012F7 RID: 4855 RVA: 0x00049984 File Offset: 0x00047B84
		[TypeConverter(typeof(StringConverter))]
		[DefaultValue(null)]
		public object IndeterminateValue
		{
			get
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				return (base.CellTemplate as DataGridViewCheckBoxCell).IndeterminateValue;
			}
			set
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				(base.CellTemplate as DataGridViewCheckBoxCell).IndeterminateValue = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the hosted check box cells will allow three check states rather than two.</summary>
		/// <returns>true if the hosted <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> objects are able to have a third, indeterminate, state; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCheckBoxColumn.CellTemplate" /> property is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x000499B0 File Offset: 0x00047BB0
		// (set) Token: 0x060012F9 RID: 4857 RVA: 0x000499E4 File Offset: 0x00047BE4
		[DefaultValue(false)]
		public bool ThreeState
		{
			get
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				return (base.CellTemplate as DataGridViewCheckBoxCell).ThreeState;
			}
			set
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				(base.CellTemplate as DataGridViewCheckBoxCell).ThreeState = value;
			}
		}

		/// <summary>Gets or sets the underlying value corresponding to a cell value of true, which appears as a checked box.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing a value that the cell will treat as a true value. The default is null.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCheckBoxColumn.CellTemplate" /> property is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x00049A10 File Offset: 0x00047C10
		// (set) Token: 0x060012FB RID: 4859 RVA: 0x00049A44 File Offset: 0x00047C44
		[TypeConverter(typeof(StringConverter))]
		[DefaultValue(null)]
		public object TrueValue
		{
			get
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				return (base.CellTemplate as DataGridViewCheckBoxCell).TrueValue;
			}
			set
			{
				if (base.CellTemplate == null)
				{
					throw new InvalidOperationException("CellTemplate is null.");
				}
				(base.CellTemplate as DataGridViewCheckBoxCell).TrueValue = value;
			}
		}

		/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012FC RID: 4860 RVA: 0x00049A70 File Offset: 0x00047C70
		public override string ToString()
		{
			return string.Format("DataGridViewCheckBoxColumn {{ Name={0}, Index={1} }}", base.Name, base.Index);
		}
	}
}
