using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the properties of the paging controls in a control that supports pagination. This class cannot be inherited.</summary>
	// Token: 0x020003E3 RID: 995
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class PagerSettings : IStateManager
	{
		/// <summary>Occurs when a property of a <see cref="T:System.Web.UI.WebControls.PagerSettings" /> object changes values.</summary>
		// Token: 0x140000CB RID: 203
		// (add) Token: 0x06002B98 RID: 11160 RVA: 0x00073A07 File Offset: 0x00071C07
		// (remove) Token: 0x06002B99 RID: 11161 RVA: 0x00073A1A File Offset: 0x00071C1A
		[Browsable(false)]
		public event EventHandler PropertyChanged
		{
			add
			{
				this.events.AddHandler(PagerSettings.propertyChangedEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(PagerSettings.propertyChangedEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> class.</summary>
		// Token: 0x06002B9A RID: 11162 RVA: 0x00073A2D File Offset: 0x00071C2D
		public PagerSettings()
		{
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x00073A4B File Offset: 0x00071C4B
		internal PagerSettings(Control ctrl)
		{
			this.ctrl = ctrl;
		}

		/// <summary>Gets or sets the URL to an image to display for the first-page button.</summary>
		/// <returns>The URL to an image to display for the first-page button. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.PagerSettings.FirstPageImageUrl" /> is not set.</returns>
		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06002B9C RID: 11164 RVA: 0x00073A70 File Offset: 0x00071C70
		// (set) Token: 0x06002B9D RID: 11165 RVA: 0x00073A9D File Offset: 0x00071C9D
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[NotifyParentProperty(true)]
		[WebCategory("Appearance")]
		[UrlProperty]
		public string FirstPageImageUrl
		{
			get
			{
				object obj = this.ViewState["FirstPageImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["FirstPageImageUrl"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets the text to display for the first-page button.</summary>
		/// <returns>The text to display for the first-page button. The default is "&amp;lt;&amp;lt;", which renders as "&lt;&lt;".</returns>
		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06002B9E RID: 11166 RVA: 0x00073AB8 File Offset: 0x00071CB8
		// (set) Token: 0x06002B9F RID: 11167 RVA: 0x00073AE5 File Offset: 0x00071CE5
		[NotifyParentProperty(true)]
		[DefaultValue("&lt;&lt;")]
		[WebCategory("Appearance")]
		public string FirstPageText
		{
			get
			{
				object obj = this.ViewState["FirstPageText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "&lt;&lt;";
			}
			set
			{
				this.ViewState["FirstPageText"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets the URL to an image to display for the last-page button.</summary>
		/// <returns>The URL to an image to display for the last-page button. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.PagerSettings.LastPageImageUrl" /> is not set.</returns>
		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x00073B00 File Offset: 0x00071D00
		// (set) Token: 0x06002BA1 RID: 11169 RVA: 0x00073B2D File Offset: 0x00071D2D
		[NotifyParentProperty(true)]
		[WebCategory("Appearance")]
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string LastPageImageUrl
		{
			get
			{
				object obj = this.ViewState["LastPageImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["LastPageImageUrl"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets the text to display for the last-page button.</summary>
		/// <returns>The text to display for the last-page button. The default is "&amp;gt;&amp;gt;", which renders as "&gt;&gt;".</returns>
		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06002BA2 RID: 11170 RVA: 0x00073B48 File Offset: 0x00071D48
		// (set) Token: 0x06002BA3 RID: 11171 RVA: 0x00073B75 File Offset: 0x00071D75
		[DefaultValue("&gt;&gt;")]
		[WebCategory("Appearance")]
		[NotifyParentProperty(true)]
		public string LastPageText
		{
			get
			{
				object obj = this.ViewState["LastPageText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "&gt;&gt;";
			}
			set
			{
				this.ViewState["LastPageText"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets the mode in which to display the pager controls in a control that supports pagination.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.PagerButtons" /> values. The default is PagerButtons.Numeric.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.PagerSettings.Mode" /> is set to a value that is not one of the <see cref="T:System.Web.UI.WebControls.PagerButtons" /> values.</exception>
		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06002BA4 RID: 11172 RVA: 0x00073B90 File Offset: 0x00071D90
		// (set) Token: 0x06002BA5 RID: 11173 RVA: 0x00073BB9 File Offset: 0x00071DB9
		[NotifyParentProperty(true)]
		[WebCategory("Appearance")]
		[DefaultValue(PagerButtons.Numeric)]
		public PagerButtons Mode
		{
			get
			{
				object obj = this.ViewState["Mode"];
				if (obj != null)
				{
					return (PagerButtons)obj;
				}
				return PagerButtons.Numeric;
			}
			set
			{
				this.ViewState["Mode"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets the URL to an image to display for the next-page button.</summary>
		/// <returns>The URL to an image to display for the next-page button. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.PagerSettings.NextPageImageUrl" /> is not set.</returns>
		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06002BA6 RID: 11174 RVA: 0x00073BD8 File Offset: 0x00071DD8
		// (set) Token: 0x06002BA7 RID: 11175 RVA: 0x00073C05 File Offset: 0x00071E05
		[NotifyParentProperty(true)]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[DefaultValue("")]
		[WebCategory("Appearance")]
		public string NextPageImageUrl
		{
			get
			{
				object obj = this.ViewState["NextPageImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["NextPageImageUrl"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets the text to display for the next-page button.</summary>
		/// <returns>The text to display for the next-page button. The default is "&amp;gt;", which renders as "&gt;".</returns>
		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x00073C20 File Offset: 0x00071E20
		// (set) Token: 0x06002BA9 RID: 11177 RVA: 0x00073C4D File Offset: 0x00071E4D
		[DefaultValue("&gt;")]
		[WebCategory("Appearance")]
		[NotifyParentProperty(true)]
		public string NextPageText
		{
			get
			{
				object obj = this.ViewState["NextPageText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "&gt;";
			}
			set
			{
				this.ViewState["NextPageText"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets the number of page buttons to display in the pager when the <see cref="P:System.Web.UI.WebControls.PagerSettings.Mode" /> property is set to the <see cref="F:System.Web.UI.WebControls.PagerButtons.Numeric" /> or <see cref="F:System.Web.UI.WebControls.PagerButtons.NumericFirstLast" /> value.</summary>
		/// <returns>The number of page buttons to display in the pager. The default is 10.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.PagerSettings.PageButtonCount" /> is set to a value that is less than 1.</exception>
		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06002BAA RID: 11178 RVA: 0x00073C68 File Offset: 0x00071E68
		// (set) Token: 0x06002BAB RID: 11179 RVA: 0x00073C92 File Offset: 0x00071E92
		[WebCategory("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(10)]
		public int PageButtonCount
		{
			get
			{
				object obj = this.ViewState["PageButtonCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 10;
			}
			set
			{
				this.ViewState["PageButtonCount"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets a value that specifies the location where the pager is displayed.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.PagerPosition" /> values. The default is <see cref="F:System.Web.UI.WebControls.PagerPosition.Bottom" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.PagerSettings.Position" /> is set to a value that is not one of the <see cref="T:System.Web.UI.WebControls.PagerPosition" /> values.</exception>
		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06002BAC RID: 11180 RVA: 0x00073CB0 File Offset: 0x00071EB0
		// (set) Token: 0x06002BAD RID: 11181 RVA: 0x00073CD9 File Offset: 0x00071ED9
		[WebCategory("Layout")]
		[DefaultValue(PagerPosition.Bottom)]
		[NotifyParentProperty(true)]
		public PagerPosition Position
		{
			get
			{
				object obj = this.ViewState["Position"];
				if (obj != null)
				{
					return (PagerPosition)obj;
				}
				return PagerPosition.Bottom;
			}
			set
			{
				this.ViewState["Position"] = value;
			}
		}

		/// <summary>Gets or sets the URL to an image to display for the previous-page button.</summary>
		/// <returns>The URL to an image to display for the previous-page button. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.PagerSettings.PreviousPageImageUrl" /> is not set.</returns>
		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06002BAE RID: 11182 RVA: 0x00073CF4 File Offset: 0x00071EF4
		// (set) Token: 0x06002BAF RID: 11183 RVA: 0x00073D21 File Offset: 0x00071F21
		[WebCategory("Appearance")]
		[NotifyParentProperty(true)]
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string PreviousPageImageUrl
		{
			get
			{
				object obj = this.ViewState["PreviousPageImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PreviousPageImageUrl"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets the text to display for the previous page button.</summary>
		/// <returns>The text to display for the previous page button. The default is "&amp;lt;", which renders as "&lt;".</returns>
		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06002BB0 RID: 11184 RVA: 0x00073D3C File Offset: 0x00071F3C
		// (set) Token: 0x06002BB1 RID: 11185 RVA: 0x00073D69 File Offset: 0x00071F69
		[DefaultValue("&lt;")]
		[NotifyParentProperty(true)]
		[WebCategory("Appearance")]
		public string PreviousPageText
		{
			get
			{
				object obj = this.ViewState["PreviousPageText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "&lt;";
			}
			set
			{
				this.ViewState["PreviousPageText"] = value;
				this.RaisePropertyChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether the paging controls are displayed in a control that supports pagination.</summary>
		/// <returns>true to display the pager; otherwise, false. The default is true.</returns>
		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06002BB2 RID: 11186 RVA: 0x00073D84 File Offset: 0x00071F84
		// (set) Token: 0x06002BB3 RID: 11187 RVA: 0x00073DAD File Offset: 0x00071FAD
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[WebCategory("Appearance")]
		public bool Visible
		{
			get
			{
				object obj = this.ViewState["Visible"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["Visible"] = value;
			}
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x00073DC8 File Offset: 0x00071FC8
		private void RaisePropertyChanged()
		{
			EventHandler eventHandler = this.events[PagerSettings.propertyChangedEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		/// <summary>Retrieves the string representation of a <see cref="T:System.Web.UI.WebControls.PagerSettings" /> object.</summary>
		/// <returns>An empty string ("").</returns>
		// Token: 0x06002BB5 RID: 11189 RVA: 0x0000EE9B File Offset: 0x0000D09B
		public override string ToString()
		{
			return string.Empty;
		}

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> object.</summary>
		/// <param name="state">An object that represents the state of the <see cref="T:System.Web.UI.WebControls.PagerSettings" />.</param>
		// Token: 0x06002BB6 RID: 11190 RVA: 0x00073DFA File Offset: 0x00071FFA
		void IStateManager.LoadViewState(object savedState)
		{
			this.ViewState.LoadViewState(savedState);
		}

		/// <summary>Saves the current view state of the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> object.</summary>
		/// <returns>An object that contains the saved state of the <see cref="T:System.Web.UI.WebControls.PagerSettings" />.</returns>
		// Token: 0x06002BB7 RID: 11191 RVA: 0x00073E08 File Offset: 0x00072008
		object IStateManager.SaveViewState()
		{
			return this.ViewState.SaveViewState();
		}

		/// <summary>Marks the starting point at which to begin tracking and saving view state changes to the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> object.</summary>
		// Token: 0x06002BB8 RID: 11192 RVA: 0x00073E15 File Offset: 0x00072015
		void IStateManager.TrackViewState()
		{
			this.ViewState.TrackViewState();
		}

		/// <summary>Gets a value that indicates whether the server control is tracking its view state changes.</summary>
		/// <returns>true if the data source view is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06002BB9 RID: 11193 RVA: 0x00073E22 File Offset: 0x00072022
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.ViewState.IsTrackingViewState;
			}
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x00073E30 File Offset: 0x00072030
		internal Table CreatePagerControl(int currentPage, int pageCount)
		{
			Table table = new Table();
			TableRow tableRow = new TableRow();
			table.Rows.Add(tableRow);
			int num = ((this.Mode == PagerButtons.Numeric || this.Mode == PagerButtons.NumericFirstLast) ? this.PageButtonCount : 1);
			int num2 = num * (currentPage / num);
			int num3 = num2 + num;
			if (num3 > pageCount)
			{
				num3 = pageCount;
				if (num3 - num2 < num)
				{
					num2 = num3 - num;
				}
				if (num2 < 0)
				{
					num2 = 0;
				}
			}
			if ((this.Mode == PagerButtons.NumericFirstLast || this.Mode == PagerButtons.NextPreviousFirstLast) && num2 > 0)
			{
				tableRow.Cells.Add(this.CreateCell(this.FirstPageText, this.FirstPageImageUrl, "Page", "First"));
			}
			if ((this.Mode == PagerButtons.NextPrevious || this.Mode == PagerButtons.NextPreviousFirstLast) && num2 > 0)
			{
				tableRow.Cells.Add(this.CreateCell(this.PreviousPageText, this.PreviousPageImageUrl, "Page", "Prev"));
			}
			if (this.Mode == PagerButtons.Numeric || this.Mode == PagerButtons.NumericFirstLast)
			{
				if (num2 > 0)
				{
					tableRow.Cells.Add(this.CreateCell("...", string.Empty, "Page", num2.ToString()));
				}
				for (int i = num2; i < num3; i++)
				{
					tableRow.Cells.Add(this.CreateCell((i + 1).ToString(), string.Empty, (i != currentPage) ? "Page" : "", (i != currentPage) ? (i + 1).ToString() : ""));
				}
				if (num3 < pageCount)
				{
					tableRow.Cells.Add(this.CreateCell("...", string.Empty, "Page", (num3 + 1).ToString()));
				}
			}
			if ((this.Mode == PagerButtons.NextPrevious || this.Mode == PagerButtons.NextPreviousFirstLast) && num3 < pageCount)
			{
				tableRow.Cells.Add(this.CreateCell(this.NextPageText, this.NextPageImageUrl, "Page", "Next"));
			}
			if ((this.Mode == PagerButtons.NumericFirstLast || this.Mode == PagerButtons.NextPreviousFirstLast) && num3 < pageCount)
			{
				tableRow.Cells.Add(this.CreateCell(this.LastPageText, this.LastPageImageUrl, "Page", "Last"));
			}
			return table;
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x00074064 File Offset: 0x00072264
		private TableCell CreateCell(string text, string image, string command, string argument)
		{
			TableCell tableCell = new TableCell();
			Control control;
			if (string.IsNullOrEmpty(command))
			{
				control = new Label
				{
					Text = text
				};
			}
			else
			{
				control = (Control)DataControlButton.CreateButton(string.IsNullOrEmpty(image) ? ButtonType.Link : ButtonType.Image, this.ctrl, text, image, command, argument, true);
			}
			tableCell.Controls.Add(control);
			return tableCell;
		}

		// Token: 0x04001B2A RID: 6954
		private static readonly object propertyChangedEvent = new object();

		// Token: 0x04001B2B RID: 6955
		private StateBag ViewState = new StateBag();

		// Token: 0x04001B2C RID: 6956
		private Control ctrl;

		// Token: 0x04001B2D RID: 6957
		private EventHandlerList events = new EventHandlerList();
	}
}
