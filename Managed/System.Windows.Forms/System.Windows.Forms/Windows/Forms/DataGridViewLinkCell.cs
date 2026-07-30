using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents a cell that contains a link. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200011B RID: 283
	public class DataGridViewLinkCell : DataGridViewCell
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewLinkCell" /> class.</summary>
		// Token: 0x0600146B RID: 5227 RVA: 0x0004D050 File Offset: 0x0004B250
		public DataGridViewLinkCell()
		{
			this.activeLinkColor = Color.Red;
			this.linkColor = Color.FromArgb(0, 0, 255);
			this.trackVisitedState = true;
			this.visited_link_color = Color.FromArgb(128, 0, 128);
		}

		/// <summary>Creates an exact copy of this cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewLinkCell" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600146C RID: 5228 RVA: 0x0004D0A0 File Offset: 0x0004B2A0
		public override object Clone()
		{
			DataGridViewLinkCell dataGridViewLinkCell = (DataGridViewLinkCell)base.Clone();
			dataGridViewLinkCell.activeLinkColor = this.activeLinkColor;
			dataGridViewLinkCell.linkColor = this.linkColor;
			dataGridViewLinkCell.linkVisited = this.linkVisited;
			dataGridViewLinkCell.linkBehavior = this.linkBehavior;
			dataGridViewLinkCell.visited_link_color = this.visited_link_color;
			dataGridViewLinkCell.trackVisitedState = this.trackVisitedState;
			return dataGridViewLinkCell;
		}

		/// <returns>A string that represents the current object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600146D RID: 5229 RVA: 0x0004D104 File Offset: 0x0004B304
		public override string ToString()
		{
			return string.Format("DataGridViewLinkCell {{ ColumnIndex={0}, RowIndex={1} }}", base.ColumnIndex, base.RowIndex);
		}

		/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewLinkCell" />. </summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewLinkCell" />. </returns>
		// Token: 0x0600146E RID: 5230 RVA: 0x0004D134 File Offset: 0x0004B334
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject(this);
		}

		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x0600146F RID: 5231 RVA: 0x0004D13C File Offset: 0x0004B33C
		protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return Rectangle.Empty;
			}
			object formattedValue = base.FormattedValue;
			Size size = Size.Empty;
			if (formattedValue != null)
			{
				size = DataGridViewCell.MeasureTextSize(graphics, formattedValue.ToString(), cellStyle.Font, TextFormatFlags.Left);
				size.Height += 3;
				return new Rectangle(1, (base.OwningRow.Height - size.Height) / 2 - 1, size.Width, size.Height);
			}
			return new Rectangle(1, 10, 0, 0);
		}

		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
		/// <param name="graphics">The graphics context for the cell.</param>
		/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x06001470 RID: 5232 RVA: 0x0004D1CC File Offset: 0x0004B3CC
		protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			if (base.DataGridView == null || string.IsNullOrEmpty(base.ErrorText))
			{
				return Rectangle.Empty;
			}
			Size size;
			size..ctor(12, 11);
			return new Rectangle(new Point(base.Size.Width - size.Width - 5, (base.Size.Height - size.Height) / 2), size);
		}

		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
		/// <param name="rowIndex">The zero-based row index of the cell.</param>
		/// <param name="constraintSize">The cell's maximum allowable size.</param>
		// Token: 0x06001471 RID: 5233 RVA: 0x0004D240 File Offset: 0x0004B440
		protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
		{
			object formattedValue = base.FormattedValue;
			if (formattedValue != null)
			{
				Size size = DataGridViewCell.MeasureTextSize(graphics, formattedValue.ToString(), cellStyle.Font, TextFormatFlags.Left);
				size.Height = Math.Max(size.Height, 20);
				size.Width += 4;
				return size;
			}
			return new Size(21, 20);
		}

		/// <returns>The value contained in the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		/// <param name="rowIndex">The index of the cell's parent row.</param>
		// Token: 0x06001472 RID: 5234 RVA: 0x0004D2A0 File Offset: 0x0004B4A0
		protected override object GetValue(int rowIndex)
		{
			if (this.useColumnTextForLinkValue)
			{
				return (base.OwningColumn as DataGridViewLinkColumn).Text;
			}
			return base.GetValue(rowIndex);
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when a key is released and the cell has focus.</summary>
		/// <returns>true if the SPACE key was released, the <see cref="P:System.Windows.Forms.DataGridViewLinkCell.TrackVisitedState" /> property is true, the <see cref="P:System.Windows.Forms.DataGridViewLinkCell.LinkVisited" /> property is false, and the CTRL, ALT, and SHIFT keys are not pressed; otherwise, false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains data about the key press.</param>
		/// <param name="rowIndex">The index of the row containing the cell.</param>
		// Token: 0x06001473 RID: 5235 RVA: 0x0004D2C8 File Offset: 0x0004B4C8
		protected override bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
		{
			return e.KeyCode != Keys.Space && this.trackVisitedState && !this.linkVisited && !e.Shift && !e.Control && !e.Alt;
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the mouse button is pressed while the pointer is over the cell.</summary>
		/// <returns>true if the mouse pointer is over the link; otherwise, false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains data about the mouse click.</param>
		// Token: 0x06001474 RID: 5236 RVA: 0x0004D31C File Offset: 0x0004B51C
		protected override bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return true;
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the mouse pointer leaves the cell.</summary>
		/// <returns>true if the link displayed by the cell is not in the normal state; otherwise, false.</returns>
		/// <param name="rowIndex">The index of the row containing the cell.</param>
		// Token: 0x06001475 RID: 5237 RVA: 0x0004D320 File Offset: 0x0004B520
		protected override bool MouseLeaveUnsharesRow(int rowIndex)
		{
			return this.linkState != LinkState.Normal;
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the mouse pointer moves over the cell.</summary>
		/// <returns>true if the mouse pointer is over the link and the link is has not yet changed color to reflect the hover state; otherwise, false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains data about the mouse click.</param>
		// Token: 0x06001476 RID: 5238 RVA: 0x0004D330 File Offset: 0x0004B530
		protected override bool MouseMoveUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return this.linkState == LinkState.Hover;
		}

		/// <summary>Indicates whether the row containing the cell will be unshared when the mouse button is released while the pointer is over the cell. </summary>
		/// <returns>true if the mouse pointer is over the link; otherwise, false.</returns>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains data about the mouse click.</param>
		// Token: 0x06001477 RID: 5239 RVA: 0x0004D344 File Offset: 0x0004B544
		protected override bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return this.linkState == LinkState.Hover;
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		/// <param name="rowIndex">The index of the cell's parent row. </param>
		// Token: 0x06001478 RID: 5240 RVA: 0x0004D350 File Offset: 0x0004B550
		protected override void OnKeyUp(KeyEventArgs e, int rowIndex)
		{
			if ((e.KeyData & Keys.Space) == Keys.Space)
			{
				this.linkState = LinkState.Normal;
				base.DataGridView.InvalidateCell(this);
			}
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06001479 RID: 5241 RVA: 0x0004D378 File Offset: 0x0004B578
		protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.linkState = LinkState.Active;
			base.DataGridView.InvalidateCell(this);
		}

		/// <param name="rowIndex">The index of the cell's parent row. </param>
		// Token: 0x0600147A RID: 5242 RVA: 0x0004D394 File Offset: 0x0004B594
		protected override void OnMouseLeave(int rowIndex)
		{
			base.OnMouseLeave(rowIndex);
			this.linkState = LinkState.Normal;
			base.DataGridView.InvalidateCell(this);
			base.DataGridView.Cursor = this.parent_cursor;
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
		// Token: 0x0600147B RID: 5243 RVA: 0x0004D3CC File Offset: 0x0004B5CC
		protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (this.linkState != LinkState.Hover)
			{
				this.linkState = LinkState.Hover;
				base.DataGridView.InvalidateCell(this);
				this.parent_cursor = base.DataGridView.Cursor;
				base.DataGridView.Cursor = Cursors.Hand;
			}
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
		// Token: 0x0600147C RID: 5244 RVA: 0x0004D420 File Offset: 0x0004B620
		protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
		{
			base.OnMouseUp(e);
			this.linkState = LinkState.Hover;
			this.LinkVisited = true;
			base.DataGridView.InvalidateCell(this);
		}

		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
		/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="cellState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the cell.</param>
		/// <param name="value">The data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="formattedValue">The formatted data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="errorText">An error message that is associated with the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
		/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
		/// <param name="paintParts">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values that specifies which parts of the cell need to be painted.</param>
		// Token: 0x0600147D RID: 5245 RVA: 0x0004D450 File Offset: 0x0004B650
		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0004D478 File Offset: 0x0004B678
		internal override void PaintPartContent(Graphics graphics, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle, object formattedValue)
		{
			Font font = cellStyle.Font;
			switch (this.LinkBehavior)
			{
			case LinkBehavior.SystemDefault:
			case LinkBehavior.AlwaysUnderline:
				font = new Font(font, 4);
				break;
			case LinkBehavior.HoverUnderline:
				if (this.linkState == LinkState.Hover)
				{
					font = new Font(font, 4);
				}
				break;
			}
			Color visitedLinkColor;
			if (this.linkState == LinkState.Active)
			{
				visitedLinkColor = this.ActiveLinkColor;
			}
			else if (this.linkVisited)
			{
				visitedLinkColor = this.VisitedLinkColor;
			}
			else
			{
				visitedLinkColor = this.LinkColor;
			}
			TextFormatFlags textFormatFlags = TextFormatFlags.VerticalCenter | TextFormatFlags.TextBoxControl | TextFormatFlags.EndEllipsis;
			cellBounds.Height -= 2;
			cellBounds.Width -= 2;
			if (formattedValue != null)
			{
				TextRenderer.DrawText(graphics, formattedValue.ToString(), font, cellBounds, visitedLinkColor, textFormatFlags);
			}
		}

		/// <summary>Gets or sets the color used to display an active link.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to display a link that is being selected. The default value is the user's Internet Explorer setting for the color of links in the hover state. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x0004D544 File Offset: 0x0004B744
		// (set) Token: 0x06001480 RID: 5248 RVA: 0x0004D54C File Offset: 0x0004B74C
		public Color ActiveLinkColor
		{
			get
			{
				return this.activeLinkColor;
			}
			set
			{
				this.activeLinkColor = value;
			}
		}

		/// <summary>Gets or sets a value that represents the behavior of a link.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.LinkBehavior" /> values. The default is <see cref="F:System.Windows.Forms.LinkBehavior.SystemDefault" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.LinkBehavior" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x0004D558 File Offset: 0x0004B758
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x0004D560 File Offset: 0x0004B760
		[DefaultValue(LinkBehavior.SystemDefault)]
		public LinkBehavior LinkBehavior
		{
			get
			{
				return this.linkBehavior;
			}
			set
			{
				this.linkBehavior = value;
			}
		}

		/// <summary>Gets or sets the color used to display an inactive and unvisited link.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to initially display a link. The default value is the user's Internet Explorer setting for the link color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x0004D56C File Offset: 0x0004B76C
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x0004D574 File Offset: 0x0004B774
		public Color LinkColor
		{
			get
			{
				return this.linkColor;
			}
			set
			{
				this.linkColor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the link was visited.</summary>
		/// <returns>true if the link has been visited; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x0004D580 File Offset: 0x0004B780
		// (set) Token: 0x06001486 RID: 5254 RVA: 0x0004D588 File Offset: 0x0004B788
		public bool LinkVisited
		{
			get
			{
				return this.linkVisited;
			}
			set
			{
				this.linkVisited = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the link changes color when it is visited.</summary>
		/// <returns>true if the link changes color when it is selected; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x0004D594 File Offset: 0x0004B794
		// (set) Token: 0x06001488 RID: 5256 RVA: 0x0004D59C File Offset: 0x0004B79C
		[DefaultValue(true)]
		public bool TrackVisitedState
		{
			get
			{
				return this.trackVisitedState;
			}
			set
			{
				this.trackVisitedState = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the column <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.Text" /> property value is displayed as the link text.</summary>
		/// <returns>true if the column <see cref="P:System.Windows.Forms.DataGridViewLinkColumn.Text" /> property value is displayed as the link text; false if the cell <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValue" /> property value is displayed as the link text. The default is false.</returns>
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0004D5A8 File Offset: 0x0004B7A8
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x0004D5B0 File Offset: 0x0004B7B0
		[DefaultValue(false)]
		public bool UseColumnTextForLinkValue
		{
			get
			{
				return this.useColumnTextForLinkValue;
			}
			set
			{
				this.useColumnTextForLinkValue = value;
			}
		}

		/// <summary>Gets or sets the color used to display a link that has been previously visited.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to display a link that has been visited. The default value is the user's Internet Explorer setting for the visited link color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x0004D5BC File Offset: 0x0004B7BC
		// (set) Token: 0x0600148C RID: 5260 RVA: 0x0004D5C4 File Offset: 0x0004B7C4
		public Color VisitedLinkColor
		{
			get
			{
				return this.visited_link_color;
			}
			set
			{
				this.visited_link_color = value;
			}
		}

		/// <returns>A <see cref="T:System.Type" /> representing the data type of the value in the cell.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x0004D5D0 File Offset: 0x0004B7D0
		public override Type ValueType
		{
			get
			{
				return (base.ValueType != null) ? base.ValueType : typeof(object);
			}
		}

		/// <summary>Gets the type of the cell's hosted editing control.</summary>
		/// <returns>Always null. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x0004D600 File Offset: 0x0004B800
		public override Type EditType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the display <see cref="T:System.Type" /> of the cell value.</summary>
		/// <returns>Always <see cref="T:System.String" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x0004D604 File Offset: 0x0004B804
		public override Type FormattedValueType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x04000BD3 RID: 3027
		private Color activeLinkColor;

		// Token: 0x04000BD4 RID: 3028
		private LinkBehavior linkBehavior;

		// Token: 0x04000BD5 RID: 3029
		private Color linkColor;

		// Token: 0x04000BD6 RID: 3030
		private bool linkVisited;

		// Token: 0x04000BD7 RID: 3031
		private Cursor parent_cursor;

		// Token: 0x04000BD8 RID: 3032
		private bool trackVisitedState;

		// Token: 0x04000BD9 RID: 3033
		private bool useColumnTextForLinkValue;

		// Token: 0x04000BDA RID: 3034
		private Color visited_link_color;

		// Token: 0x04000BDB RID: 3035
		private LinkState linkState;

		/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewLinkCell" /> control to accessibility client applications.</summary>
		// Token: 0x0200011C RID: 284
		protected class DataGridViewLinkCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject" /> class. </summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject" />.</param>
			// Token: 0x06001490 RID: 5264 RVA: 0x0004D610 File Offset: 0x0004B810
			public DataGridViewLinkCellAccessibleObject(DataGridViewCell owner)
				: base(owner)
			{
			}

			/// <summary>Performs the default action of the <see cref="T:System.Windows.Forms.DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject" />.</summary>
			/// <exception cref="T:System.InvalidOperationException">The cell returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property has a <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property value that is not null and a <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex" /> property value of -1, indicating that the cell is in a shared row.</exception>
			// Token: 0x06001491 RID: 5265 RVA: 0x0004D61C File Offset: 0x0004B81C
			[MonoTODO("Stub, does nothing")]
			[PermissionSet(2, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
			public override void DoDefaultAction()
			{
			}

			/// <summary>Gets the number of child accessible objects that belong to the <see cref="T:System.Windows.Forms.DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject" />.</summary>
			/// <returns>The value –1.</returns>
			// Token: 0x06001492 RID: 5266 RVA: 0x0004D620 File Offset: 0x0004B820
			public override int GetChildCount()
			{
				return -1;
			}

			/// <summary>Gets a string that represents the default action of the <see cref="T:System.Windows.Forms.DataGridViewLinkCell.DataGridViewLinkCellAccessibleObject" />.</summary>
			/// <returns>The string "Click".</returns>
			// Token: 0x170004BB RID: 1211
			// (get) Token: 0x06001493 RID: 5267 RVA: 0x0004D624 File Offset: 0x0004B824
			public override string DefaultAction
			{
				get
				{
					return "Click";
				}
			}
		}
	}
}
