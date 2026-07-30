using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Collects the characteristics associated with table layouts.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200030A RID: 778
	[TypeConverter(typeof(TableLayoutSettingsTypeConverter))]
	[Serializable]
	public sealed class TableLayoutSettings : LayoutSettings, ISerializable
	{
		// Token: 0x060033BA RID: 13242 RVA: 0x000C3D10 File Offset: 0x000C1F10
		internal TableLayoutSettings(TableLayoutPanel panel)
		{
			this.column_styles = new TableLayoutColumnStyleCollection(panel);
			this.row_styles = new TableLayoutRowStyleCollection(panel);
			this.grow_style = TableLayoutPanelGrowStyle.AddRows;
			this.column_count = 0;
			this.row_count = 0;
			this.columns = new Dictionary<object, int>();
			this.column_spans = new Dictionary<object, int>();
			this.rows = new Dictionary<object, int>();
			this.row_spans = new Dictionary<object, int>();
			this.panel = panel;
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x000C3D84 File Offset: 0x000C1F84
		private TableLayoutSettings(SerializationInfo serializationInfo, StreamingContext context)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(this);
			string @string = serializationInfo.GetString("SerializedString");
			if (!string.IsNullOrEmpty(@string) && converter != null)
			{
				TableLayoutSettings tableLayoutSettings = converter.ConvertFromInvariantString(@string) as TableLayoutSettings;
				this.column_styles = tableLayoutSettings.column_styles;
				this.row_styles = tableLayoutSettings.row_styles;
				this.grow_style = tableLayoutSettings.grow_style;
				this.column_count = tableLayoutSettings.column_count;
				this.row_count = tableLayoutSettings.row_count;
				this.columns = tableLayoutSettings.columns;
				this.column_spans = tableLayoutSettings.column_spans;
				this.rows = tableLayoutSettings.rows;
				this.row_spans = tableLayoutSettings.row_spans;
				this.panel = tableLayoutSettings.panel;
				this.isSerialized = true;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" />.</summary>
		/// <param name="si">The object to be populated with serialization information. </param>
		/// <param name="context">The destination context of the serialization.</param>
		// Token: 0x060033BC RID: 13244 RVA: 0x000C3E48 File Offset: 0x000C2048
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			TableLayoutSettingsTypeConverter tableLayoutSettingsTypeConverter = new TableLayoutSettingsTypeConverter();
			string text = tableLayoutSettingsTypeConverter.ConvertToInvariantString(this);
			si.AddValue("SerializedString", text);
		}

		/// <summary>Gets or sets the maximum number of columns allowed in the table layout.</summary>
		/// <returns>The maximum number of columns allowed in the table layout. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property value is less than 0.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x060033BD RID: 13245 RVA: 0x000C3E70 File Offset: 0x000C2070
		// (set) Token: 0x060033BE RID: 13246 RVA: 0x000C3E78 File Offset: 0x000C2078
		[DefaultValue(0)]
		public int ColumnCount
		{
			get
			{
				return this.column_count;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (this.column_count != value)
				{
					this.column_count = value;
					if (this.panel != null)
					{
						this.panel.PerformLayout(this.panel, "ColumnCount");
					}
				}
			}
		}

		/// <summary>Gets the collection of styles used to determine the look and feel of the table layout columns. </summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" /> that contains the column styles for the layout table. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x060033BF RID: 13247 RVA: 0x000C3EC8 File Offset: 0x000C20C8
		[DesignerSerializationVisibility(2)]
		public TableLayoutColumnStyleCollection ColumnStyles
		{
			get
			{
				return this.column_styles;
			}
		}

		/// <summary>Gets or sets a value indicating how the table layout should expand to accommodate new cells when all existing cells are occupied.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TableLayoutPanelGrowStyle" /> values. The default is <see cref="F:System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property value is not valid for the enumeration type.</exception>
		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x060033C0 RID: 13248 RVA: 0x000C3ED0 File Offset: 0x000C20D0
		// (set) Token: 0x060033C1 RID: 13249 RVA: 0x000C3ED8 File Offset: 0x000C20D8
		[DefaultValue(TableLayoutPanelGrowStyle.AddRows)]
		public TableLayoutPanelGrowStyle GrowStyle
		{
			get
			{
				return this.grow_style;
			}
			set
			{
				if (!Enum.IsDefined(typeof(TableLayoutPanelGrowStyle), value))
				{
					throw new ArgumentException();
				}
				if (this.grow_style != value)
				{
					this.grow_style = value;
					if (this.panel != null)
					{
						this.panel.PerformLayout(this.panel, "GrowStyle");
					}
				}
			}
		}

		/// <summary>Gets the current table layout engine.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> currently being used. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x060033C2 RID: 13250 RVA: 0x000C3F3C File Offset: 0x000C213C
		public override LayoutEngine LayoutEngine
		{
			get
			{
				if (this.panel != null)
				{
					return this.panel.LayoutEngine;
				}
				return base.LayoutEngine;
			}
		}

		/// <summary>Gets or sets the maximum number of rows allowed in the table layout.</summary>
		/// <returns>The maximum number of rows allowed in the table layout. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property value is less than 0.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x060033C3 RID: 13251 RVA: 0x000C3F5C File Offset: 0x000C215C
		// (set) Token: 0x060033C4 RID: 13252 RVA: 0x000C3F64 File Offset: 0x000C2164
		[DefaultValue(0)]
		public int RowCount
		{
			get
			{
				return this.row_count;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (this.row_count != value)
				{
					this.row_count = value;
					if (this.panel != null)
					{
						this.panel.PerformLayout();
					}
				}
			}
		}

		/// <summary>Gets the collection of styles used to determine the look and feel of the table layout rows.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" /> that contains the row styles for the layout table.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x060033C5 RID: 13253 RVA: 0x000C3FA8 File Offset: 0x000C21A8
		[DesignerSerializationVisibility(2)]
		public TableLayoutRowStyleCollection RowStyles
		{
			get
			{
				return this.row_styles;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the row and the column of the cell.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the cell position.</returns>
		/// <param name="control">A control contained within a cell.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		// Token: 0x060033C6 RID: 13254 RVA: 0x000C3FB0 File Offset: 0x000C21B0
		[DefaultValue(-1)]
		public TableLayoutPanelCellPosition GetCellPosition(object control)
		{
			if (control == null)
			{
				throw new ArgumentNullException();
			}
			int num;
			if (!this.columns.TryGetValue(control, ref num))
			{
				num = -1;
			}
			int num2;
			if (!this.rows.TryGetValue(control, ref num2))
			{
				num2 = -1;
			}
			return new TableLayoutPanelCellPosition(num, num2);
		}

		/// <summary>Gets the column position of the specified child control.</summary>
		/// <returns>The column position of the specified child control.</returns>
		/// <param name="control">A control contained within a cell.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		// Token: 0x060033C7 RID: 13255 RVA: 0x000C3FFC File Offset: 0x000C21FC
		[DefaultValue(-1)]
		public int GetColumn(object control)
		{
			if (control == null)
			{
				throw new ArgumentNullException();
			}
			int num;
			if (this.columns.TryGetValue(control, ref num))
			{
				return num;
			}
			return -1;
		}

		/// <summary>Gets the number of columns that the cell containing the child control spans.</summary>
		/// <returns>The number of columns that the cell containing the child control spans.</returns>
		/// <param name="control">A control contained within a cell.</param>
		// Token: 0x060033C8 RID: 13256 RVA: 0x000C402C File Offset: 0x000C222C
		public int GetColumnSpan(object control)
		{
			if (control == null)
			{
				throw new ArgumentNullException();
			}
			int num;
			if (this.column_spans.TryGetValue(control, ref num))
			{
				return num;
			}
			return 1;
		}

		/// <summary>Gets the row position of the specified child control.</summary>
		/// <returns>The row position of the specified child control.</returns>
		/// <param name="control">A control contained within a cell.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		// Token: 0x060033C9 RID: 13257 RVA: 0x000C405C File Offset: 0x000C225C
		[DefaultValue(-1)]
		public int GetRow(object control)
		{
			if (control == null)
			{
				throw new ArgumentNullException();
			}
			int num;
			if (this.rows.TryGetValue(control, ref num))
			{
				return num;
			}
			return -1;
		}

		/// <summary>Gets the number of rows that the cell containing the child control spans.</summary>
		/// <returns>The number of rows that the cell containing the child control spans.</returns>
		/// <param name="control">A control contained within a cell.</param>
		// Token: 0x060033CA RID: 13258 RVA: 0x000C408C File Offset: 0x000C228C
		public int GetRowSpan(object control)
		{
			if (control == null)
			{
				throw new ArgumentNullException();
			}
			int num;
			if (this.row_spans.TryGetValue(control, ref num))
			{
				return num;
			}
			return 1;
		}

		/// <summary>Sets the <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the row and the column of the cell.</summary>
		/// <param name="control">A control contained within a cell.</param>
		/// <param name="cellPosition">A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />  that represents the cell position.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		// Token: 0x060033CB RID: 13259 RVA: 0x000C40BC File Offset: 0x000C22BC
		[DefaultValue(-1)]
		public void SetCellPosition(object control, TableLayoutPanelCellPosition cellPosition)
		{
			if (control == null)
			{
				throw new ArgumentNullException();
			}
			this.columns[control] = cellPosition.Column;
			this.rows[control] = cellPosition.Row;
			if (this.panel != null)
			{
				this.panel.PerformLayout();
			}
		}

		/// <summary>Sets the column position for the specified child control.</summary>
		/// <param name="control">A control contained within a cell.</param>
		/// <param name="column">The column position for the specified child control.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="column" /> is less than -1.</exception>
		// Token: 0x060033CC RID: 13260 RVA: 0x000C4114 File Offset: 0x000C2314
		public void SetColumn(object control, int column)
		{
			if (control == null)
			{
				throw new ArgumentNullException();
			}
			if (column < -1)
			{
				throw new ArgumentException();
			}
			this.columns[control] = column;
			if (this.panel != null)
			{
				this.panel.PerformLayout();
			}
		}

		/// <summary>Sets the number of columns that the cell containing the child control spans.</summary>
		/// <param name="control">A control contained within a cell.</param>
		/// <param name="value">The number of columns that the cell containing the child control spans.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is less than 1.</exception>
		// Token: 0x060033CD RID: 13261 RVA: 0x000C4160 File Offset: 0x000C2360
		public void SetColumnSpan(object control, int value)
		{
			if (control == null)
			{
				throw new ArgumentNullException();
			}
			if (value < -1)
			{
				throw new ArgumentException();
			}
			this.column_spans[control] = value;
			if (this.panel != null)
			{
				this.panel.PerformLayout();
			}
		}

		/// <summary>Sets the row position of the specified child control.</summary>
		/// <param name="control">A control contained within a cell.</param>
		/// <param name="row">The row position of the specified child control.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="row" /> is less than -1.</exception>
		// Token: 0x060033CE RID: 13262 RVA: 0x000C41AC File Offset: 0x000C23AC
		public void SetRow(object control, int row)
		{
			if (control == null)
			{
				throw new ArgumentNullException();
			}
			if (row < -1)
			{
				throw new ArgumentException();
			}
			this.rows[control] = row;
			if (this.panel != null)
			{
				this.panel.PerformLayout();
			}
		}

		/// <summary>Sets the number of rows that the cell containing the child control spans.</summary>
		/// <param name="control">A control contained within a cell.</param>
		/// <param name="value">The number of rows that the cell containing the child control spans.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is less than 1.</exception>
		// Token: 0x060033CF RID: 13263 RVA: 0x000C41F8 File Offset: 0x000C23F8
		public void SetRowSpan(object control, int value)
		{
			if (control == null)
			{
				throw new ArgumentNullException();
			}
			if (value < -1)
			{
				throw new ArgumentException();
			}
			this.row_spans[control] = value;
			if (this.panel != null)
			{
				this.panel.PerformLayout();
			}
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x000C4244 File Offset: 0x000C2444
		internal List<ControlInfo> GetControls()
		{
			List<ControlInfo> list = new List<ControlInfo>();
			foreach (KeyValuePair<object, int> keyValuePair in this.columns)
			{
				list.Add(new ControlInfo
				{
					Control = keyValuePair.Key,
					Col = this.GetColumn(keyValuePair.Key),
					ColSpan = this.GetColumnSpan(keyValuePair.Key),
					Row = this.GetRow(keyValuePair.Key),
					RowSpan = this.GetRowSpan(keyValuePair.Key)
				});
			}
			return list;
		}

		// Token: 0x04001877 RID: 6263
		private TableLayoutColumnStyleCollection column_styles;

		// Token: 0x04001878 RID: 6264
		private TableLayoutRowStyleCollection row_styles;

		// Token: 0x04001879 RID: 6265
		private TableLayoutPanelGrowStyle grow_style;

		// Token: 0x0400187A RID: 6266
		private int column_count;

		// Token: 0x0400187B RID: 6267
		private int row_count;

		// Token: 0x0400187C RID: 6268
		private Dictionary<object, int> columns;

		// Token: 0x0400187D RID: 6269
		private Dictionary<object, int> column_spans;

		// Token: 0x0400187E RID: 6270
		private Dictionary<object, int> rows;

		// Token: 0x0400187F RID: 6271
		private Dictionary<object, int> row_spans;

		// Token: 0x04001880 RID: 6272
		internal TableLayoutPanel panel;

		// Token: 0x04001881 RID: 6273
		internal bool isSerialized;

		// Token: 0x0200030B RID: 779
		internal class StyleConverter : TypeConverter
		{
			// Token: 0x060033D2 RID: 13266 RVA: 0x000C4320 File Offset: 0x000C2520
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value == null || !(value is string))
				{
					return base.ConvertFrom(context, culture, value);
				}
				return Enum.Parse(typeof(TableLayoutSettings.StyleConverter), (string)value, true);
			}

			// Token: 0x060033D3 RID: 13267 RVA: 0x000C4354 File Offset: 0x000C2554
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (value == null || !(value is TableLayoutSettings.StyleConverter) || destinationType != typeof(string))
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				return ((TableLayoutSettings.StyleConverter)value).ToString();
			}
		}
	}
}
