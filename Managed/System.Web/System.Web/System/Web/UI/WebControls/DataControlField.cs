using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the base class for all data control field types, which represent a column of data in tabular data-bound controls such as <see cref="T:System.Web.UI.WebControls.DetailsView" /> and <see cref="T:System.Web.UI.WebControls.GridView" />.</summary>
	// Token: 0x02000371 RID: 881
	[DefaultProperty("HeaderText")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class DataControlField : IStateManager, IDataSourceViewSchemaAccessor
	{
		// Token: 0x14000066 RID: 102
		// (add) Token: 0x06002120 RID: 8480 RVA: 0x00054C7A File Offset: 0x00052E7A
		// (remove) Token: 0x06002121 RID: 8481 RVA: 0x00054C8D File Offset: 0x00052E8D
		internal event EventHandler FieldChanged
		{
			add
			{
				this.events.AddHandler(DataControlField.fieldChangedEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(DataControlField.fieldChangedEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataControlField" /> class.</summary>
		// Token: 0x06002122 RID: 8482 RVA: 0x00054CA0 File Offset: 0x00052EA0
		protected DataControlField()
		{
			this.viewState = new StateBag();
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x00054CBE File Offset: 0x00052EBE
		internal void SetDirty()
		{
			this.viewState.SetDirty(true);
		}

		/// <summary>Gets a dictionary of state information that allows you to save and restore the view state of a <see cref="T:System.Web.UI.WebControls.DataControlField" /> object across multiple requests for the same page.</summary>
		/// <returns>An instance of <see cref="T:System.Web.UI.StateBag" /> that contains the <see cref="T:System.Web.UI.WebControls.DataControlField" /> view-state information.</returns>
		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06002124 RID: 8484 RVA: 0x00054CCC File Offset: 0x00052ECC
		protected StateBag ViewState
		{
			get
			{
				return this.viewState;
			}
		}

		/// <summary>Extracts the value of the data control field from the current table cell and adds the value to the specified <see cref="T:System.Collections.IDictionary" /> collection.</summary>
		/// <param name="dictionary">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" />.</param>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the text or controls of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		/// <param name="includeReadOnly">true to indicate that the values of read-only fields are included in the <paramref name="dictionary" /> collection; otherwise, false.</param>
		// Token: 0x06002125 RID: 8485 RVA: 0x0000393A File Offset: 0x00001B3A
		public virtual void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
		{
		}

		/// <summary>Performs basic instance initialization for a data control field.</summary>
		/// <returns>Always returns false.</returns>
		/// <param name="sortingEnabled">A value that indicates whether the control supports the sorting of columns of data.</param>
		/// <param name="control">The data control that owns the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</param>
		// Token: 0x06002126 RID: 8486 RVA: 0x00054CD4 File Offset: 0x00052ED4
		public virtual bool Initialize(bool sortingEnabled, Control control)
		{
			this.sortingEnabled = sortingEnabled;
			this.control = control;
			return false;
		}

		/// <summary>Adds text or controls to a cell's controls collection.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the text or controls of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</param>
		/// <param name="cellType">One of the <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> values.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values, specifying the state of the row that contains the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" />.</param>
		/// <param name="rowIndex">The index of the row that the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> is contained in.</param>
		// Token: 0x06002127 RID: 8487 RVA: 0x00054CE8 File Offset: 0x00052EE8
		public virtual void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			if (cellType != DataControlCellType.Header)
			{
				if (cellType == DataControlCellType.Footer)
				{
					string footerText = this.FooterText;
					cell.Text = ((footerText.Length > 0) ? footerText : "&nbsp;");
				}
				return;
			}
			if (this.HeaderText.Length > 0 && this.sortingEnabled && this.SortExpression.Length > 0)
			{
				cell.Controls.Add((Control)DataControlButton.CreateButton(string.IsNullOrEmpty(this.HeaderImageUrl) ? ButtonType.Link : ButtonType.Image, this.control, this.HeaderText, this.HeaderImageUrl, "Sort", this.SortExpression, true));
				return;
			}
			if (this.HeaderImageUrl.Length > 0)
			{
				Image image = new Image();
				image.ImageUrl = this.HeaderImageUrl;
				cell.Controls.Add(image);
				return;
			}
			cell.Text = ((this.HeaderText.Length > 0) ? this.HeaderText : "&nbsp;");
		}

		/// <summary>Creates a duplicate copy of the current <see cref="T:System.Web.UI.WebControls.DataControlField" />-derived object.</summary>
		/// <returns>A duplicate copy of the current <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
		// Token: 0x06002128 RID: 8488 RVA: 0x00054DD8 File Offset: 0x00052FD8
		protected internal DataControlField CloneField()
		{
			DataControlField dataControlField = this.CreateField();
			this.CopyProperties(dataControlField);
			return dataControlField;
		}

		/// <summary>When overridden in a derived class, creates an empty <see cref="T:System.Web.UI.WebControls.DataControlField" />-derived object.</summary>
		/// <returns>An empty <see cref="T:System.Web.UI.WebControls.DataControlField" />-derived object.</returns>
		// Token: 0x06002129 RID: 8489
		protected abstract DataControlField CreateField();

		/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.DataControlField" />-derived object to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to which to copy the properties of the current <see cref="T:System.Web.UI.WebControls.DataControlField" />.</param>
		// Token: 0x0600212A RID: 8490 RVA: 0x00054DF4 File Offset: 0x00052FF4
		protected virtual void CopyProperties(DataControlField newField)
		{
			newField.AccessibleHeaderText = this.AccessibleHeaderText;
			newField.ControlStyle.CopyFrom(this.ControlStyle);
			newField.FooterStyle.CopyFrom(this.FooterStyle);
			newField.FooterText = this.FooterText;
			newField.HeaderImageUrl = this.HeaderImageUrl;
			newField.HeaderStyle.CopyFrom(this.HeaderStyle);
			newField.HeaderText = this.HeaderText;
			newField.InsertVisible = this.InsertVisible;
			newField.ItemStyle.CopyFrom(this.ItemStyle);
			newField.ShowHeader = this.ShowHeader;
			newField.SortExpression = this.SortExpression;
			newField.Visible = this.Visible;
		}

		/// <summary>Raises the FieldChanged event.</summary>
		// Token: 0x0600212B RID: 8491 RVA: 0x00054EA8 File Offset: 0x000530A8
		protected virtual void OnFieldChanged()
		{
			EventHandler eventHandler = this.events[DataControlField.fieldChangedEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		/// <summary>Restores the data source view's previously saved view state.</summary>
		/// <param name="savedState">An object that represents the <see cref="T:System.Web.UI.WebControls.DataControlField" /> state to restore.</param>
		// Token: 0x0600212C RID: 8492 RVA: 0x00054EDC File Offset: 0x000530DC
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			object[] array = (object[])savedState;
			this.viewState.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.ControlStyle).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.FooterStyle).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.HeaderStyle).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.ItemStyle).LoadViewState(array[4]);
			}
		}

		/// <summary>Saves the changes made to the <see cref="T:System.Web.UI.WebControls.DataControlField" /> view state since the time the page was posted back to the server.</summary>
		/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.DataControlField" /> view state. If there is no view state associated with the object, this method returns null.</returns>
		// Token: 0x0600212D RID: 8493 RVA: 0x00054F50 File Offset: 0x00053150
		protected virtual object SaveViewState()
		{
			object[] array = new object[5];
			array[0] = this.viewState.SaveViewState();
			if (this.controlStyle != null)
			{
				array[1] = ((IStateManager)this.controlStyle).SaveViewState();
			}
			if (this.footerStyle != null)
			{
				array[2] = ((IStateManager)this.footerStyle).SaveViewState();
			}
			if (this.headerStyle != null)
			{
				array[3] = ((IStateManager)this.headerStyle).SaveViewState();
			}
			if (this.itemStyle != null)
			{
				array[4] = ((IStateManager)this.itemStyle).SaveViewState();
			}
			if (array[0] == null && array[1] == null && array[2] == null && array[3] == null && array[4] == null)
			{
				return null;
			}
			return array;
		}

		/// <summary>Causes the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object to track changes to its view state so they can be stored in the control's <see cref="P:System.Web.UI.WebControls.DataControlField.ViewState" /> property and persisted across requests for the same page.</summary>
		// Token: 0x0600212E RID: 8494 RVA: 0x00054FE8 File Offset: 0x000531E8
		protected virtual void TrackViewState()
		{
			if (this.controlStyle != null)
			{
				((IStateManager)this.controlStyle).TrackViewState();
			}
			if (this.footerStyle != null)
			{
				((IStateManager)this.footerStyle).TrackViewState();
			}
			if (this.headerStyle != null)
			{
				((IStateManager)this.headerStyle).TrackViewState();
			}
			if (this.itemStyle != null)
			{
				((IStateManager)this.itemStyle).TrackViewState();
			}
			this.viewState.TrackViewState();
			this.tracking = true;
		}

		/// <summary>When overridden in a derived class, signals that the controls contained by a field support callbacks.</summary>
		/// <exception cref="T:System.NotSupportedException">The method is called on a default instance of the <see cref="T:System.Web.UI.WebControls.DataControlField" /> class.</exception>
		// Token: 0x0600212F RID: 8495 RVA: 0x00055053 File Offset: 0x00053253
		public virtual void ValidateSupportsCallback()
		{
			throw new NotSupportedException("Callback not supported");
		}

		/// <summary>Restores the data control field's previously saved view state.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that contains the saved view state values for the control.</param>
		// Token: 0x06002130 RID: 8496 RVA: 0x0005505F File Offset: 0x0005325F
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		/// <summary>Saves the changes made to the <see cref="T:System.Web.UI.WebControls.DataControlField" /> view state since the time the page was posted back to the server.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the saved view state values for the control.</returns>
		// Token: 0x06002131 RID: 8497 RVA: 0x00055068 File Offset: 0x00053268
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		/// <summary>Causes the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object to track changes to its view state so they can be stored in the control's <see cref="P:System.Web.UI.WebControls.DataControlField.ViewState" /> property and persisted across requests for the same page.</summary>
		// Token: 0x06002132 RID: 8498 RVA: 0x00055070 File Offset: 0x00053270
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x00055078 File Offset: 0x00053278
		internal Exception GetNotSupportedPropException(string propName)
		{
			return new NotSupportedException("The property '" + propName + "' is not supported in " + base.GetType().Name);
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06002134 RID: 8500 RVA: 0x0005509A File Offset: 0x0005329A
		internal bool ControlStyleCreated
		{
			get
			{
				return this.controlStyle != null;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x000550A5 File Offset: 0x000532A5
		internal bool HeaderStyleCreated
		{
			get
			{
				return this.headerStyle != null;
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06002136 RID: 8502 RVA: 0x000550B0 File Offset: 0x000532B0
		internal bool FooterStyleCreated
		{
			get
			{
				return this.footerStyle != null;
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06002137 RID: 8503 RVA: 0x000550BB File Offset: 0x000532BB
		internal bool ItemStyleCreated
		{
			get
			{
				return this.itemStyle != null;
			}
		}

		/// <summary>Gets or sets text that is rendered as the AbbreviatedText property value in some controls.</summary>
		/// <returns>A string that represents abbreviated text read by screen readers. The default value is an empty string ("").</returns>
		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06002138 RID: 8504 RVA: 0x000550C8 File Offset: 0x000532C8
		// (set) Token: 0x06002139 RID: 8505 RVA: 0x000550F5 File Offset: 0x000532F5
		[WebCategory("Accessibility")]
		[Localizable(true)]
		[DefaultValue("")]
		[global::System.MonoTODO("Render this")]
		public virtual string AccessibleHeaderText
		{
			get
			{
				object obj = this.viewState["accessibleHeaderText"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.viewState["accessibleHeaderText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets a reference to the data control that the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object is associated with.</summary>
		/// <returns>The data control that owns the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x0600213A RID: 8506 RVA: 0x0005510E File Offset: 0x0005330E
		protected Control Control
		{
			get
			{
				return this.control;
			}
		}

		/// <summary>Gets the style of any Web server controls contained by the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that governs the appearance of Web server controls contained by the field.</returns>
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x0600213B RID: 8507 RVA: 0x00055116 File Offset: 0x00053316
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[WebCategory("Styles")]
		public Style ControlStyle
		{
			get
			{
				if (this.controlStyle == null)
				{
					this.controlStyle = new Style();
					if (this.IsTrackingViewState)
					{
						this.controlStyle.TrackViewState();
					}
				}
				return this.controlStyle;
			}
		}

		/// <summary>Gets a value indicating whether a data control field is currently viewed in a design-time environment.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.DataControlField" /> is currently viewed in a design-time environment; otherwise, false.</returns>
		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x0600213C RID: 8508 RVA: 0x00055144 File Offset: 0x00053344
		protected bool DesignMode
		{
			get
			{
				return this.control != null && this.control.Site != null && this.control.Site.DesignMode;
			}
		}

		/// <summary>Gets or sets the style of the footer of the data control field.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that governs the appearance of the footer item of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x0600213D RID: 8509 RVA: 0x0005516D File Offset: 0x0005336D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle FooterStyle
		{
			get
			{
				if (this.footerStyle == null)
				{
					this.footerStyle = new TableItemStyle();
					if (this.IsTrackingViewState)
					{
						this.footerStyle.TrackViewState();
					}
				}
				return this.footerStyle;
			}
		}

		/// <summary>Gets or sets the text that is displayed in the footer item of a data control field.</summary>
		/// <returns>A string that is displayed in the footer item of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x0005519C File Offset: 0x0005339C
		// (set) Token: 0x0600213F RID: 8511 RVA: 0x000551C9 File Offset: 0x000533C9
		[Localizable(true)]
		[DefaultValue("")]
		[WebCategory("Appearance")]
		public virtual string FooterText
		{
			get
			{
				object obj = this.viewState["footerText"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.viewState["footerText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the URL of an image that is displayed in the header item of a data control field.</summary>
		/// <returns>A string that represents a fully qualified or relative URL to an image that is displayed in the header item of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x000551E4 File Offset: 0x000533E4
		// (set) Token: 0x06002141 RID: 8513 RVA: 0x00055211 File Offset: 0x00053411
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Appearance")]
		public virtual string HeaderImageUrl
		{
			get
			{
				object obj = this.viewState["headerImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.viewState["headerImageUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the style of the header of the data control field.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that governs the appearance of the <see cref="T:System.Web.UI.WebControls.DataControlField" /> header item.</returns>
		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x0005522A File Offset: 0x0005342A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public TableItemStyle HeaderStyle
		{
			get
			{
				if (this.headerStyle == null)
				{
					this.headerStyle = new TableItemStyle();
					if (this.IsTrackingViewState)
					{
						this.headerStyle.TrackViewState();
					}
				}
				return this.headerStyle;
			}
		}

		/// <summary>Gets or sets the text that is displayed in the header item of a data control field.</summary>
		/// <returns>A string that is displayed in the header item of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x00055258 File Offset: 0x00053458
		// (set) Token: 0x06002144 RID: 8516 RVA: 0x00055285 File Offset: 0x00053485
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		public virtual string HeaderText
		{
			get
			{
				object obj = this.viewState["headerText"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.viewState["headerText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object is visible when its parent data-bound control is in insert mode.</summary>
		/// <returns>true if the field is visible when its parent data-bound control is rendered in insert mode; otherwise, false. The default value is true.</returns>
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06002145 RID: 8517 RVA: 0x000552A0 File Offset: 0x000534A0
		// (set) Token: 0x06002146 RID: 8518 RVA: 0x000552C9 File Offset: 0x000534C9
		[DefaultValue(true)]
		[WebCategory("Behavior")]
		public virtual bool InsertVisible
		{
			get
			{
				object obj = this.viewState["InsertVisible"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.viewState["InsertVisible"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets the style of any text-based content displayed by a data control field.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that governs the appearance of text displayed in a <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06002147 RID: 8519 RVA: 0x000552E7 File Offset: 0x000534E7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[DefaultValue(null)]
		public TableItemStyle ItemStyle
		{
			get
			{
				if (this.itemStyle == null)
				{
					this.itemStyle = new TableItemStyle();
					if (this.IsTrackingViewState)
					{
						this.itemStyle.TrackViewState();
					}
				}
				return this.itemStyle;
			}
		}

		/// <summary>Gets or sets a value indicating whether the header item of a data control field is rendered.</summary>
		/// <returns>true if the header item of the <see cref="T:System.Web.UI.WebControls.DataControlField" /> is rendered; otherwise, false. The default is true.</returns>
		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x00055318 File Offset: 0x00053518
		// (set) Token: 0x06002149 RID: 8521 RVA: 0x00055341 File Offset: 0x00053541
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		public virtual bool ShowHeader
		{
			get
			{
				object obj = this.viewState["showHeader"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.viewState["showHeader"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a sort expression that is used by a data source control to sort data.</summary>
		/// <returns>A sort expression that is used by a data source control to sort data. The default value is an empty string ("").</returns>
		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x0600214A RID: 8522 RVA: 0x00055360 File Offset: 0x00053560
		// (set) Token: 0x0600214B RID: 8523 RVA: 0x0005538D File Offset: 0x0005358D
		[WebCategory("Behavior")]
		[DefaultValue("")]
		public virtual string SortExpression
		{
			get
			{
				object obj = this.viewState["sortExpression"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.viewState["sortExpression"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether a data control field is rendered.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.DataControlField" /> is rendered; otherwise, false. The default value is true.</returns>
		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x0600214C RID: 8524 RVA: 0x000553A8 File Offset: 0x000535A8
		// (set) Token: 0x0600214D RID: 8525 RVA: 0x000553D1 File Offset: 0x000535D1
		[DefaultValue(true)]
		[WebCategory("Behavior")]
		public bool Visible
		{
			get
			{
				object obj = this.viewState["visible"];
				return obj == null || (bool)obj;
			}
			set
			{
				if (value == this.Visible)
				{
					return;
				}
				this.viewState["visible"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object is saving changes to its view state.</summary>
		/// <returns>true if the data source view is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x0600214E RID: 8526 RVA: 0x000553F9 File Offset: 0x000535F9
		protected bool IsTrackingViewState
		{
			get
			{
				return this.tracking;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object is saving changes to its view state.</summary>
		/// <returns>true to indicate that the <see cref="T:System.Web.UI.WebControls.DataControlField" /> is saving changes to its view state; otherwise, false.</returns>
		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x00055401 File Offset: 0x00053601
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		/// <summary>Gets or sets the schema associated with this <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <returns>The schema associated with this <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x00055409 File Offset: 0x00053609
		// (set) Token: 0x06002151 RID: 8529 RVA: 0x0005541B File Offset: 0x0005361B
		object IDataSourceViewSchemaAccessor.DataSourceViewSchema
		{
			get
			{
				return this.viewState["dataSourceViewSchema"];
			}
			set
			{
				this.viewState["dataSourceViewSchema"] = value;
			}
		}

		/// <summary>Returns a string that represents this <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <returns>A string that represents this <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
		// Token: 0x06002152 RID: 8530 RVA: 0x0005542E File Offset: 0x0005362E
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.HeaderText))
			{
				return base.ToString();
			}
			return this.HeaderText;
		}

		/// <summary>Gets or sets a value that specifies whether the control validates client input.</summary>
		/// <returns>true if the control validates client input; otherwise, false.</returns>
		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06002154 RID: 8532 RVA: 0x00055458 File Offset: 0x00053658
		// (set) Token: 0x06002155 RID: 8533 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal virtual ValidateRequestMode ValidateRequestMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ValidateRequestMode.Inherit;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x040018C1 RID: 6337
		private static readonly object fieldChangedEvent = new object();

		// Token: 0x040018C2 RID: 6338
		private bool tracking;

		// Token: 0x040018C3 RID: 6339
		private StateBag viewState;

		// Token: 0x040018C4 RID: 6340
		private Control control;

		// Token: 0x040018C5 RID: 6341
		private Style controlStyle;

		// Token: 0x040018C6 RID: 6342
		private TableItemStyle footerStyle;

		// Token: 0x040018C7 RID: 6343
		private TableItemStyle headerStyle;

		// Token: 0x040018C8 RID: 6344
		private TableItemStyle itemStyle;

		// Token: 0x040018C9 RID: 6345
		private bool sortingEnabled;

		// Token: 0x040018CA RID: 6346
		private EventHandlerList events = new EventHandlerList();
	}
}
