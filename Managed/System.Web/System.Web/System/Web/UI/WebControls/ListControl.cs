using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the abstract base class that defines the properties, methods, and events common for all list-type controls.</summary>
	// Token: 0x020003C0 RID: 960
	[DefaultEvent("SelectedIndexChanged")]
	[ParseChildren(true, "Items")]
	[ControlValueProperty("SelectedValue", null)]
	[DataBindingHandler("System.Web.UI.Design.WebControls.ListControlDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Designer("System.Web.UI.Design.WebControls.ListControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public abstract class ListControl : DataBoundControl, IEditableTextControl, ITextControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ListControl" /> class.</summary>
		// Token: 0x060027A7 RID: 10151 RVA: 0x0006727C File Offset: 0x0006547C
		public ListControl()
			: base(HtmlTextWriterTag.Select)
		{
		}

		/// <summary>Gets or sets a value that indicates whether list items are cleared before data binding.</summary>
		/// <returns>true if list items are not cleared before data binding; otherwise, false, if the items collection is cleared before data binding is performed. The default is false.</returns>
		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x060027A8 RID: 10152 RVA: 0x0006728E File Offset: 0x0006548E
		// (set) Token: 0x060027A9 RID: 10153 RVA: 0x000672A1 File Offset: 0x000654A1
		[Themeable(false)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		public virtual bool AppendDataBoundItems
		{
			get
			{
				return this.ViewState.GetBool("AppendDataBoundItems", false);
			}
			set
			{
				this.ViewState["AppendDataBoundItems"] = value;
				if (base.Initialized)
				{
					base.RequiresDataBinding = true;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether a postback to the server automatically occurs when the user changes the list selection.</summary>
		/// <returns>true if a postback to the server automatically occurs whenever the user changes the selection of the list; otherwise, false. The default is false.</returns>
		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x060027AA RID: 10154 RVA: 0x0004E514 File Offset: 0x0004C714
		// (set) Token: 0x060027AB RID: 10155 RVA: 0x0004E527 File Offset: 0x0004C727
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue(false)]
		[Themeable(false)]
		public virtual bool AutoPostBack
		{
			get
			{
				return this.ViewState.GetBool("AutoPostBack", false);
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		/// <summary>Gets or sets the field of the data source that provides the text content of the list items.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the field of the data source that provides the text content of the list items. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x060027AC RID: 10156 RVA: 0x000672C8 File Offset: 0x000654C8
		// (set) Token: 0x060027AD RID: 10157 RVA: 0x000672DF File Offset: 0x000654DF
		[WebCategory("Data")]
		[WebSysDescription("")]
		[DefaultValue("")]
		[Themeable(false)]
		public virtual string DataTextField
		{
			get
			{
				return this.ViewState.GetString("DataTextField", string.Empty);
			}
			set
			{
				this.ViewState["DataTextField"] = value;
				if (base.Initialized)
				{
					base.RequiresDataBinding = true;
				}
			}
		}

		/// <summary>Gets or sets the formatting string used to control how data bound to the list control is displayed.</summary>
		/// <returns>The formatting string for data bound to the control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x060027AE RID: 10158 RVA: 0x00067301 File Offset: 0x00065501
		// (set) Token: 0x060027AF RID: 10159 RVA: 0x00067318 File Offset: 0x00065518
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Data")]
		[Themeable(false)]
		public virtual string DataTextFormatString
		{
			get
			{
				return this.ViewState.GetString("DataTextFormatString", string.Empty);
			}
			set
			{
				this.ViewState["DataTextFormatString"] = value;
				if (base.Initialized)
				{
					base.RequiresDataBinding = true;
				}
			}
		}

		/// <summary>Gets or sets the field of the data source that provides the value of each list item.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the field of the data source that provides the value of each list item. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x060027B0 RID: 10160 RVA: 0x0006733A File Offset: 0x0006553A
		// (set) Token: 0x060027B1 RID: 10161 RVA: 0x00067351 File Offset: 0x00065551
		[Themeable(false)]
		[WebSysDescription("")]
		[DefaultValue("")]
		[WebCategory("Data")]
		public virtual string DataValueField
		{
			get
			{
				return this.ViewState.GetString("DataValueField", string.Empty);
			}
			set
			{
				this.ViewState["DataValueField"] = value;
				if (base.Initialized)
				{
					base.RequiresDataBinding = true;
				}
			}
		}

		/// <summary>Gets the collection of items in the list control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ListItemCollection" /> that represents the items within the list. The default is an empty list.</returns>
		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x060027B2 RID: 10162 RVA: 0x00067373 File Offset: 0x00065573
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ListItemsCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual ListItemCollection Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new ListItemCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.items).TrackViewState();
					}
				}
				return this.items;
			}
		}

		/// <summary>Gets or sets the lowest ordinal index of the selected items in the list.</summary>
		/// <returns>The lowest ordinal index of the selected items in the list. The default is -1, which indicates that nothing is selected.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index was set to less than -1, or greater than or equal to the number of items on the list at the time the list is rendered. </exception>
		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x060027B3 RID: 10163 RVA: 0x000673A4 File Offset: 0x000655A4
		// (set) Token: 0x060027B4 RID: 10164 RVA: 0x000673E8 File Offset: 0x000655E8
		[Themeable(false)]
		[WebCategory("Misc")]
		[DefaultValue(0)]
		[Browsable(false)]
		[Bindable(true)]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int SelectedIndex
		{
			get
			{
				if (this.items == null)
				{
					return -1;
				}
				for (int i = 0; i < this.items.Count; i++)
				{
					if (this.items[i].Selected)
					{
						return i;
					}
				}
				return -1;
			}
			set
			{
				this._selectedIndex = value;
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value >= this.Items.Count)
				{
					return;
				}
				this.ClearSelection();
				if (value == -1)
				{
					return;
				}
				this.items[value].Selected = true;
			}
		}

		/// <summary>Gets the selected item with the lowest index in the list control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ListItem" /> that represents the lowest indexed item selected from the list control. The default is null.</returns>
		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x00067438 File Offset: 0x00065638
		[Browsable(false)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public virtual ListItem SelectedItem
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				if (selectedIndex == -1)
				{
					return null;
				}
				return this.Items[selectedIndex];
			}
		}

		/// <summary>Gets the value of the selected item in the list control, or selects the item in the list control that contains the specified value.</summary>
		/// <returns>The value of the selected item in the list control. The default is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not in the list of available values and view state or other state has been loaded (a postback has been performed). For more information, see the Remarks section.</exception>
		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x060027B6 RID: 10166 RVA: 0x00067460 File Offset: 0x00065660
		// (set) Token: 0x060027B7 RID: 10167 RVA: 0x0006748F File Offset: 0x0006568F
		[Bindable(true, BindingDirection.TwoWay)]
		[Themeable(false)]
		[Browsable(false)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public virtual string SelectedValue
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				if (selectedIndex == -1)
				{
					return string.Empty;
				}
				return this.Items[selectedIndex].Value;
			}
			set
			{
				this._selectedValue = value;
				this.SetSelectedValue(value);
			}
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x000674A0 File Offset: 0x000656A0
		private bool SetSelectedValue(string value)
		{
			if (this.items != null && this.items.Count > 0)
			{
				int count = this.items.Count;
				ListItemCollection listItemCollection = this.Items;
				for (int i = 0; i < count; i++)
				{
					if (listItemCollection[i].Value == value)
					{
						this.ClearSelection();
						listItemCollection[i].Selected = true;
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.UI.WebControls.ListControl.SelectedValue" /> property of the <see cref="T:System.Web.UI.WebControls.ListControl" /> control.</summary>
		/// <returns>The <see cref="P:System.Web.UI.WebControls.ListControl.SelectedValue" /> of the <see cref="T:System.Web.UI.WebControls.ListControl" />.</returns>
		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x060027B9 RID: 10169 RVA: 0x0006750C File Offset: 0x0006570C
		// (set) Token: 0x060027BA RID: 10170 RVA: 0x00067514 File Offset: 0x00065714
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[WebSysDescription("")]
		public virtual string Text
		{
			get
			{
				return this.SelectedValue;
			}
			set
			{
				this.SelectedValue = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.ListControl" /> control. </summary>
		/// <returns>
		///   <see cref="F:System.Web.UI.HtmlTextWriterTag.Select" />.</returns>
		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x060027BB RID: 10171 RVA: 0x0006751D File Offset: 0x0006571D
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Select;
			}
		}

		/// <summary>Applies HTML attributes and styles to render to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object. </summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream to render HTML content on the client.</param>
		// Token: 0x060027BC RID: 10172 RVA: 0x00067521 File Offset: 0x00065721
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
		}

		/// <summary>Clears out the list selection and sets the <see cref="P:System.Web.UI.WebControls.ListItem.Selected" /> property of all items to false.</summary>
		// Token: 0x060027BD RID: 10173 RVA: 0x0006752C File Offset: 0x0006572C
		public virtual void ClearSelection()
		{
			if (this.items == null)
			{
				return;
			}
			int count = this.Items.Count;
			for (int i = 0; i < count; i++)
			{
				this.items[i].Selected = false;
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x060027BE RID: 10174 RVA: 0x0006756C File Offset: 0x0006576C
		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(e);
			IEnumerable enumerable = this.GetData().ExecuteSelect(DataSourceSelectArguments.Empty);
			base.InternalPerformDataBinding(enumerable);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060027BF RID: 10175 RVA: 0x00067598 File Offset: 0x00065798
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Page page = this.Page;
			if (page != null && base.IsEnabled)
			{
				page.RegisterEnabledControl(this);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ListControl.TextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x060027C0 RID: 10176 RVA: 0x000675C8 File Offset: 0x000657C8
		protected virtual void OnTextChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListControl.TextChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Binds the specified data source to the control that is derived from the <see cref="T:System.Web.UI.WebControls.ListControl" /> class.</summary>
		/// <param name="dataSource">An <see cref="T:System.Collections.IEnumerable" /> that represents the data source.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The cached value of <see cref="P:System.Web.UI.WebControls.ListControl.SelectedIndex" /> is out of range.</exception>
		/// <exception cref="T:System.ArgumentException">The cached values of <see cref="P:System.Web.UI.WebControls.ListControl.SelectedIndex" /> and <see cref="P:System.Web.UI.WebControls.ListControl.SelectedValue" /> do not match.</exception>
		// Token: 0x060027C1 RID: 10177 RVA: 0x000675F8 File Offset: 0x000657F8
		protected internal override void PerformDataBinding(IEnumerable dataSource)
		{
			if (dataSource != null)
			{
				if (!this.AppendDataBoundItems)
				{
					this.Items.Clear();
				}
				string text = this.DataTextFormatString;
				if (text.Length == 0)
				{
					text = null;
				}
				string text2 = this.DataTextField;
				string text3 = this.DataValueField;
				if (text2.Length == 0)
				{
					text2 = null;
				}
				if (text3.Length == 0)
				{
					text3 = null;
				}
				ListItemCollection listItemCollection = this.Items;
				foreach (object obj in dataSource)
				{
					string text5;
					string text4 = (text5 = null);
					if (text2 != null)
					{
						text5 = DataBinder.GetPropertyValue(obj, text2, text);
					}
					if (text3 != null)
					{
						text4 = DataBinder.GetPropertyValue(obj, text3).ToString();
					}
					else if (text2 == null)
					{
						text4 = (text5 = obj.ToString());
						if (text != null)
						{
							text5 = string.Format(text, obj);
						}
					}
					else if (text5 != null)
					{
						text4 = text5;
					}
					if (text5 == null)
					{
						text5 = text4;
					}
					listItemCollection.Add(new ListItem(text5, text4));
				}
			}
			if (!string.IsNullOrEmpty(this._selectedValue))
			{
				if (!this.SetSelectedValue(this._selectedValue))
				{
					throw new ArgumentOutOfRangeException("value", string.Format("'{0}' has a SelectedValue which is invalid because it does not exist in the list of items.", this.ID));
				}
				if (this._selectedIndex >= 0 && this._selectedIndex != this.SelectedIndex)
				{
					throw new ArgumentException("SelectedIndex and SelectedValue are mutually exclusive.");
				}
			}
			else if (this._selectedIndex >= 0)
			{
				this.SelectedIndex = this._selectedIndex;
			}
		}

		/// <summary>Retrieves data from the associated data source.</summary>
		// Token: 0x060027C2 RID: 10178 RVA: 0x00067770 File Offset: 0x00065970
		[global::System.MonoTODO("why override?")]
		protected override void PerformSelect()
		{
			this.OnDataBinding(EventArgs.Empty);
			base.RequiresDataBinding = false;
			base.MarkAsDataBound();
			this.OnDataBound(EventArgs.Empty);
		}

		/// <summary>Renders the items in the <see cref="T:System.Web.UI.WebControls.ListControl" /> control.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream used to write content to a Web page. </param>
		// Token: 0x060027C3 RID: 10179 RVA: 0x00067798 File Offset: 0x00065998
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			bool flag = false;
			Page page = this.Page;
			for (int i = 0; i < this.Items.Count; i++)
			{
				ListItem listItem = this.Items[i];
				if (page != null)
				{
					page.ClientScript.RegisterForEventValidation(this.UniqueID, listItem.Value);
				}
				writer.WriteBeginTag("option");
				if (listItem.Selected)
				{
					if (flag)
					{
						this.VerifyMultiSelect();
					}
					writer.WriteAttribute("selected", "selected", false);
					flag = true;
				}
				writer.WriteAttribute("value", listItem.Value, true);
				if (listItem.HasAttributes)
				{
					listItem.Attributes.Render(writer);
				}
				writer.Write(">");
				string text = HttpUtility.HtmlEncode(listItem.Text);
				writer.Write(text);
				writer.WriteEndTag("option");
				writer.WriteLine();
			}
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x00067878 File Offset: 0x00065A78
		internal ArrayList GetSelectedIndicesInternal()
		{
			ArrayList arrayList = null;
			int count;
			if (this.items != null && (count = this.items.Count) > 0)
			{
				arrayList = new ArrayList();
				for (int i = 0; i < count; i++)
				{
					if (this.items[i].Selected)
					{
						arrayList.Add(i);
					}
				}
			}
			return arrayList;
		}

		/// <summary>Saves the current view state of the <see cref="T:System.Web.UI.WebControls.ListControl" /> -derived control and the items it contains.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the saved state of the <see cref="T:System.Web.UI.WebControls.ListControl" /> control.</returns>
		// Token: 0x060027C5 RID: 10181 RVA: 0x000678D4 File Offset: 0x00065AD4
		protected override object SaveViewState()
		{
			object obj = null;
			object obj2 = base.SaveViewState();
			IStateManager stateManager = this.items;
			if (stateManager != null)
			{
				obj = stateManager.SaveViewState();
			}
			return new Pair(obj2, obj);
		}

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.ListControl" /> -derived control.</param>
		// Token: 0x060027C6 RID: 10182 RVA: 0x00067900 File Offset: 0x00065B00
		protected override void LoadViewState(object savedState)
		{
			object obj = null;
			object obj2 = null;
			Pair pair = savedState as Pair;
			if (pair != null)
			{
				obj = pair.First;
				obj2 = pair.Second;
			}
			base.LoadViewState(obj);
			if (obj2 != null)
			{
				((IStateManager)this.Items).LoadViewState(obj2);
			}
		}

		/// <summary>Sets the <see cref="P:System.Web.UI.WebControls.ListItem.Selected" /> property of a <see cref="T:System.Web.UI.WebControls.ListItem" /> control after a page is posted.</summary>
		/// <param name="selectedIndex">The index of the selected item in the <see cref="P:System.Web.UI.WebControls.ListControl.Items" /> collection.</param>
		// Token: 0x060027C7 RID: 10183 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		protected void SetPostDataSelection(int selectedIndex)
		{
			throw new NotImplementedException();
		}

		/// <summary>Marks the starting point to begin tracking and saving view-state changes to a <see cref="T:System.Web.UI.WebControls.ListControl" /> -derived control.</summary>
		// Token: 0x060027C8 RID: 10184 RVA: 0x00067940 File Offset: 0x00065B40
		protected override void TrackViewState()
		{
			base.TrackViewState();
			IStateManager stateManager = this.items;
			if (stateManager != null)
			{
				stateManager.TrackViewState();
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ListControl.SelectedIndexChanged" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060027C9 RID: 10185 RVA: 0x00067964 File Offset: 0x00065B64
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListControl.SelectedIndexChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Determines whether the list control supports multiselection mode.</summary>
		/// <exception cref="T:System.Web.HttpException">
		///   <see cref="P:System.Web.UI.WebControls.ListBox.SelectionMode" /> is set to <see cref="F:System.Web.UI.WebControls.ListSelectionMode.Single" />.</exception>
		// Token: 0x060027CA RID: 10186 RVA: 0x00067992 File Offset: 0x00065B92
		protected internal virtual void VerifyMultiSelect()
		{
			if (!this.MultiSelectOk())
			{
				throw new HttpException("Multi select is not supported");
			}
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x00008A69 File Offset: 0x00006C69
		internal virtual bool MultiSelectOk()
		{
			return false;
		}

		/// <summary>Occurs when the selection from the list control changes between posts to the server.</summary>
		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x060027CC RID: 10188 RVA: 0x000679A7 File Offset: 0x00065BA7
		// (remove) Token: 0x060027CD RID: 10189 RVA: 0x000679BA File Offset: 0x00065BBA
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.SelectedIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.SelectedIndexChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Web.UI.WebControls.ListControl.Text" /> and <see cref="P:System.Web.UI.WebControls.ListControl.SelectedValue" /> properties change.</summary>
		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x060027CE RID: 10190 RVA: 0x000679CD File Offset: 0x00065BCD
		// (remove) Token: 0x060027CF RID: 10191 RVA: 0x000679E0 File Offset: 0x00065BE0
		public event EventHandler TextChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.TextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.TextChangedEvent, value);
			}
		}

		/// <summary>Gets or sets a value indicating whether validation is performed when a control that is derived from the <see cref="T:System.Web.UI.WebControls.ListControl" /> class is clicked.</summary>
		/// <returns>true if validation is performed when the <see cref="T:System.Web.UI.WebControls.ListControl" /> control is clicked; otherwise, false. The default is false.</returns>
		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x060027D0 RID: 10192 RVA: 0x0004E53F File Offset: 0x0004C73F
		// (set) Token: 0x060027D1 RID: 10193 RVA: 0x000419B2 File Offset: 0x0003FBB2
		[WebCategory("Behavior")]
		[Themeable(false)]
		[WebSysDescription("")]
		[DefaultValue(false)]
		public virtual bool CausesValidation
		{
			get
			{
				return this.ViewState.GetBool("CausesValidation", false);
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		/// <summary>Gets or sets the group of controls for which the control that is derived from the <see cref="T:System.Web.UI.WebControls.ListControl" /> class causes validation when it posts back to the server. </summary>
		/// <returns>The group of controls for which the derived <see cref="T:System.Web.UI.WebControls.ListControl" /> causes validation when it posts back to the server. The default is an empty string ("").</returns>
		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x060027D2 RID: 10194 RVA: 0x00041BB3 File Offset: 0x0003FDB3
		// (set) Token: 0x060027D3 RID: 10195 RVA: 0x000419E1 File Offset: 0x0003FBE1
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue("")]
		public virtual string ValidationGroup
		{
			get
			{
				return this.ViewState.GetString("ValidationGroup", "");
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x000679F3 File Offset: 0x00065BF3
		// Note: this type is marked as 'beforefieldinit'.
		static ListControl()
		{
			ListControl.SelectedIndexChangedEvent = new object();
			ListControl.TextChangedEvent = new object();
		}

		// Token: 0x04001A62 RID: 6754
		private ListItemCollection items;

		// Token: 0x04001A63 RID: 6755
		private int _selectedIndex = -2;

		// Token: 0x04001A64 RID: 6756
		private string _selectedValue;
	}
}
