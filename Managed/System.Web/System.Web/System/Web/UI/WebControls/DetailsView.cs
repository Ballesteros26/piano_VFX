using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays the values of a single record from a data source in a table, where each data row represents a field of the record. The <see cref="T:System.Web.UI.WebControls.DetailsView" /> control allows you to edit, delete, and insert records.</summary>
	// Token: 0x02000387 RID: 903
	[ControlValueProperty("SelectedValue")]
	[DefaultEvent("PageIndexChanging")]
	[DataKeyProperty("DataKey")]
	[SupportsEventValidation]
	[ToolboxData("<{0}:DetailsView runat=\"server\" Width=\"125px\" Height=\"50px\"></{0}:DetailsView>")]
	[Designer("System.Web.UI.Design.WebControls.DetailsViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DetailsView : CompositeDataBoundControl, ICallbackEventHandler, ICallbackContainer, IDataItemContainer, INamingContainer, IPostBackEventHandler, IPostBackContainer, IDataBoundItemControl, IDataBoundControl, IFieldControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> class.</summary>
		// Token: 0x060022D3 RID: 8915 RVA: 0x00059DFE File Offset: 0x00057FFE
		public DetailsView()
		{
			this.rows = new DetailsViewRowCollection(new ArrayList());
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Web.UI.WebControls.DetailsView.PageIndex" /> property changes after a paging operation.</summary>
		// Token: 0x14000078 RID: 120
		// (add) Token: 0x060022D4 RID: 8916 RVA: 0x00059E2D File Offset: 0x0005802D
		// (remove) Token: 0x060022D5 RID: 8917 RVA: 0x00059E40 File Offset: 0x00058040
		public event EventHandler PageIndexChanged
		{
			add
			{
				base.Events.AddHandler(DetailsView.PageIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DetailsView.PageIndexChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Web.UI.WebControls.DetailsView.PageIndex" /> property changes before a paging operation.</summary>
		// Token: 0x14000079 RID: 121
		// (add) Token: 0x060022D6 RID: 8918 RVA: 0x00059E53 File Offset: 0x00058053
		// (remove) Token: 0x060022D7 RID: 8919 RVA: 0x00059E66 File Offset: 0x00058066
		public event DetailsViewPageEventHandler PageIndexChanging
		{
			add
			{
				base.Events.AddHandler(DetailsView.PageIndexChangingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DetailsView.PageIndexChangingEvent, value);
			}
		}

		/// <summary>Occurs when a button within a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is clicked.</summary>
		// Token: 0x1400007A RID: 122
		// (add) Token: 0x060022D8 RID: 8920 RVA: 0x00059E79 File Offset: 0x00058079
		// (remove) Token: 0x060022D9 RID: 8921 RVA: 0x00059E8C File Offset: 0x0005808C
		public event DetailsViewCommandEventHandler ItemCommand
		{
			add
			{
				base.Events.AddHandler(DetailsView.ItemCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DetailsView.ItemCommandEvent, value);
			}
		}

		/// <summary>Occurs when a record is created in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		// Token: 0x1400007B RID: 123
		// (add) Token: 0x060022DA RID: 8922 RVA: 0x00059E9F File Offset: 0x0005809F
		// (remove) Token: 0x060022DB RID: 8923 RVA: 0x00059EB2 File Offset: 0x000580B2
		public event EventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(DetailsView.ItemCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DetailsView.ItemCreatedEvent, value);
			}
		}

		/// <summary>Occurs when a Delete button within a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is clicked, but after the delete operation.</summary>
		// Token: 0x1400007C RID: 124
		// (add) Token: 0x060022DC RID: 8924 RVA: 0x00059EC5 File Offset: 0x000580C5
		// (remove) Token: 0x060022DD RID: 8925 RVA: 0x00059ED8 File Offset: 0x000580D8
		public event DetailsViewDeletedEventHandler ItemDeleted
		{
			add
			{
				base.Events.AddHandler(DetailsView.ItemDeletedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DetailsView.ItemDeletedEvent, value);
			}
		}

		/// <summary>Occurs when a Delete button within a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is clicked, but before the delete operation.</summary>
		// Token: 0x1400007D RID: 125
		// (add) Token: 0x060022DE RID: 8926 RVA: 0x00059EEB File Offset: 0x000580EB
		// (remove) Token: 0x060022DF RID: 8927 RVA: 0x00059EFE File Offset: 0x000580FE
		public event DetailsViewDeleteEventHandler ItemDeleting
		{
			add
			{
				base.Events.AddHandler(DetailsView.ItemDeletingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DetailsView.ItemDeletingEvent, value);
			}
		}

		/// <summary>Occurs when an Insert button within a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is clicked, but after the insert operation.</summary>
		// Token: 0x1400007E RID: 126
		// (add) Token: 0x060022E0 RID: 8928 RVA: 0x00059F11 File Offset: 0x00058111
		// (remove) Token: 0x060022E1 RID: 8929 RVA: 0x00059F24 File Offset: 0x00058124
		public event DetailsViewInsertedEventHandler ItemInserted
		{
			add
			{
				base.Events.AddHandler(DetailsView.ItemInsertedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DetailsView.ItemInsertedEvent, value);
			}
		}

		/// <summary>Occurs when an Insert button within a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is clicked, but before the insert operation.</summary>
		// Token: 0x1400007F RID: 127
		// (add) Token: 0x060022E2 RID: 8930 RVA: 0x00059F37 File Offset: 0x00058137
		// (remove) Token: 0x060022E3 RID: 8931 RVA: 0x00059F4A File Offset: 0x0005814A
		public event DetailsViewInsertEventHandler ItemInserting
		{
			add
			{
				base.Events.AddHandler(DetailsView.ItemInsertingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DetailsView.ItemInsertingEvent, value);
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control attempts to change between edit, insert, and read-only mode, but before the <see cref="P:System.Web.UI.WebControls.DetailsView.CurrentMode" /> property is updated.</summary>
		// Token: 0x14000080 RID: 128
		// (add) Token: 0x060022E4 RID: 8932 RVA: 0x00059F5D File Offset: 0x0005815D
		// (remove) Token: 0x060022E5 RID: 8933 RVA: 0x00059F70 File Offset: 0x00058170
		public event DetailsViewModeEventHandler ModeChanging
		{
			add
			{
				base.Events.AddHandler(DetailsView.ModeChangingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DetailsView.ModeChangingEvent, value);
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control attempts to change between edit, insert, and read-only mode, but after the <see cref="P:System.Web.UI.WebControls.DetailsView.CurrentMode" /> property is updated.</summary>
		// Token: 0x14000081 RID: 129
		// (add) Token: 0x060022E6 RID: 8934 RVA: 0x00059F83 File Offset: 0x00058183
		// (remove) Token: 0x060022E7 RID: 8935 RVA: 0x00059F96 File Offset: 0x00058196
		public event EventHandler ModeChanged
		{
			add
			{
				base.Events.AddHandler(DetailsView.ModeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DetailsView.ModeChangedEvent, value);
			}
		}

		/// <summary>Occurs when an Update button within a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is clicked, but after the update operation.</summary>
		// Token: 0x14000082 RID: 130
		// (add) Token: 0x060022E8 RID: 8936 RVA: 0x00059FA9 File Offset: 0x000581A9
		// (remove) Token: 0x060022E9 RID: 8937 RVA: 0x00059FBC File Offset: 0x000581BC
		public event DetailsViewUpdatedEventHandler ItemUpdated
		{
			add
			{
				base.Events.AddHandler(DetailsView.ItemUpdatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DetailsView.ItemUpdatedEvent, value);
			}
		}

		/// <summary>Occurs when an Update button within a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is clicked, but before the update operation.</summary>
		// Token: 0x14000083 RID: 131
		// (add) Token: 0x060022EA RID: 8938 RVA: 0x00059FCF File Offset: 0x000581CF
		// (remove) Token: 0x060022EB RID: 8939 RVA: 0x00059FE2 File Offset: 0x000581E2
		public event DetailsViewUpdateEventHandler ItemUpdating
		{
			add
			{
				base.Events.AddHandler(DetailsView.ItemUpdatingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DetailsView.ItemUpdatingEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DetailsView.PageIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060022EC RID: 8940 RVA: 0x00059FF8 File Offset: 0x000581F8
		protected virtual void OnPageIndexChanged(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[DetailsView.PageIndexChangedEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DetailsView.PageIndexChanging" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DetailsViewPageEventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.Web.HttpException">This method is called when the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is not bound to a data source control, the paging operation was not canceled, and an event handler is not registered for the event.</exception>
		// Token: 0x060022ED RID: 8941 RVA: 0x0005A030 File Offset: 0x00058230
		protected virtual void OnPageIndexChanging(DetailsViewPageEventArgs e)
		{
			if (base.Events != null)
			{
				DetailsViewPageEventHandler detailsViewPageEventHandler = (DetailsViewPageEventHandler)base.Events[DetailsView.PageIndexChangingEvent];
				if (detailsViewPageEventHandler != null)
				{
					detailsViewPageEventHandler(this, e);
					return;
				}
			}
			if (!base.IsBoundUsingDataSourceID)
			{
				throw new HttpException(string.Format(this.unhandledEventExceptionMessage, this.ID, "PageIndexChanging"));
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemCommand" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DetailsViewCommandEventArgs" /> that contains the event data.</param>
		// Token: 0x060022EE RID: 8942 RVA: 0x0005A08C File Offset: 0x0005828C
		protected virtual void OnItemCommand(DetailsViewCommandEventArgs e)
		{
			if (base.Events != null)
			{
				DetailsViewCommandEventHandler detailsViewCommandEventHandler = (DetailsViewCommandEventHandler)base.Events[DetailsView.ItemCommandEvent];
				if (detailsViewCommandEventHandler != null)
				{
					detailsViewCommandEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060022EF RID: 8943 RVA: 0x0005A0C4 File Offset: 0x000582C4
		protected virtual void OnItemCreated(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[DetailsView.ItemCreatedEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemDeleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DetailsViewDeletedEventArgs" /> that contains the event data.</param>
		// Token: 0x060022F0 RID: 8944 RVA: 0x0005A0FC File Offset: 0x000582FC
		protected virtual void OnItemDeleted(DetailsViewDeletedEventArgs e)
		{
			if (base.Events != null)
			{
				DetailsViewDeletedEventHandler detailsViewDeletedEventHandler = (DetailsViewDeletedEventHandler)base.Events[DetailsView.ItemDeletedEvent];
				if (detailsViewDeletedEventHandler != null)
				{
					detailsViewDeletedEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemInserted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DetailsViewInsertedEventArgs" /> that contains the event data.</param>
		// Token: 0x060022F1 RID: 8945 RVA: 0x0005A134 File Offset: 0x00058334
		protected virtual void OnItemInserted(DetailsViewInsertedEventArgs e)
		{
			if (base.Events != null)
			{
				DetailsViewInsertedEventHandler detailsViewInsertedEventHandler = (DetailsViewInsertedEventHandler)base.Events[DetailsView.ItemInsertedEvent];
				if (detailsViewInsertedEventHandler != null)
				{
					detailsViewInsertedEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemInserting" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DetailsViewInsertEventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.Web.HttpException">There is no handler for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemInserting" /> event.</exception>
		// Token: 0x060022F2 RID: 8946 RVA: 0x0005A16C File Offset: 0x0005836C
		protected virtual void OnItemInserting(DetailsViewInsertEventArgs e)
		{
			if (base.Events != null)
			{
				DetailsViewInsertEventHandler detailsViewInsertEventHandler = (DetailsViewInsertEventHandler)base.Events[DetailsView.ItemInsertingEvent];
				if (detailsViewInsertEventHandler != null)
				{
					detailsViewInsertEventHandler(this, e);
					return;
				}
			}
			if (!base.IsBoundUsingDataSourceID)
			{
				throw new HttpException(string.Format(this.unhandledEventExceptionMessage, this.ID, "ItemInserting"));
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemDeleting" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DetailsViewDeleteEventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.Web.HttpException">There is no handler for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemDeleting" /> event.</exception>
		// Token: 0x060022F3 RID: 8947 RVA: 0x0005A1C8 File Offset: 0x000583C8
		protected virtual void OnItemDeleting(DetailsViewDeleteEventArgs e)
		{
			if (base.Events != null)
			{
				DetailsViewDeleteEventHandler detailsViewDeleteEventHandler = (DetailsViewDeleteEventHandler)base.Events[DetailsView.ItemDeletingEvent];
				if (detailsViewDeleteEventHandler != null)
				{
					detailsViewDeleteEventHandler(this, e);
					return;
				}
			}
			if (!base.IsBoundUsingDataSourceID)
			{
				throw new HttpException(string.Format(this.unhandledEventExceptionMessage, this.ID, "ItemDeleting"));
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DetailsView.ModeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060022F4 RID: 8948 RVA: 0x0005A224 File Offset: 0x00058424
		protected virtual void OnModeChanged(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[DetailsView.ModeChangedEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DetailsView.ModeChanging" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DetailsViewModeEventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.Web.HttpException">There is no handler for the <see cref="E:System.Web.UI.WebControls.DetailsView.ModeChanging" /> event.</exception>
		// Token: 0x060022F5 RID: 8949 RVA: 0x0005A25C File Offset: 0x0005845C
		protected virtual void OnModeChanging(DetailsViewModeEventArgs e)
		{
			if (base.Events != null)
			{
				DetailsViewModeEventHandler detailsViewModeEventHandler = (DetailsViewModeEventHandler)base.Events[DetailsView.ModeChangingEvent];
				if (detailsViewModeEventHandler != null)
				{
					detailsViewModeEventHandler(this, e);
					return;
				}
			}
			if (!base.IsBoundUsingDataSourceID)
			{
				throw new HttpException(string.Format(this.unhandledEventExceptionMessage, this.ID, "ModeChanging"));
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemUpdated" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DetailsViewUpdatedEventArgs" /> that contains the event data.</param>
		// Token: 0x060022F6 RID: 8950 RVA: 0x0005A2B8 File Offset: 0x000584B8
		protected virtual void OnItemUpdated(DetailsViewUpdatedEventArgs e)
		{
			if (base.Events != null)
			{
				DetailsViewUpdatedEventHandler detailsViewUpdatedEventHandler = (DetailsViewUpdatedEventHandler)base.Events[DetailsView.ItemUpdatedEvent];
				if (detailsViewUpdatedEventHandler != null)
				{
					detailsViewUpdatedEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemUpdating" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DetailsViewUpdateEventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.Web.HttpException">There is no handler for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemUpdating" /> event.</exception>
		// Token: 0x060022F7 RID: 8951 RVA: 0x0005A2F0 File Offset: 0x000584F0
		protected virtual void OnItemUpdating(DetailsViewUpdateEventArgs e)
		{
			if (base.Events != null)
			{
				DetailsViewUpdateEventHandler detailsViewUpdateEventHandler = (DetailsViewUpdateEventHandler)base.Events[DetailsView.ItemUpdatingEvent];
				if (detailsViewUpdateEventHandler != null)
				{
					detailsViewUpdateEventHandler(this, e);
					return;
				}
			}
			if (!base.IsBoundUsingDataSourceID)
			{
				throw new HttpException(string.Format(this.unhandledEventExceptionMessage, this.ID, "ItemUpdating"));
			}
		}

		/// <summary>For a description of this property, see <see cref="P:System.Web.UI.WebControls.IDataBoundItemControl.Mode" />.</summary>
		/// <returns>The current mode of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x060022F8 RID: 8952 RVA: 0x0005A34C File Offset: 0x0005854C
		DataBoundControlMode IDataBoundItemControl.Mode
		{
			get
			{
				switch (this.CurrentMode)
				{
				case DetailsViewMode.ReadOnly:
					return DataBoundControlMode.ReadOnly;
				case DetailsViewMode.Edit:
					return DataBoundControlMode.Edit;
				case DetailsViewMode.Insert:
					return DataBoundControlMode.Insert;
				default:
					throw new InvalidOperationException(string.Format("Unsupported CurrentMode value '{0}'", this.CurrentMode));
				}
			}
		}

		/// <summary>For a description of this property, see <see cref="P:System.Web.UI.WebControls.IDataBoundControl.DataSourceObject" />.</summary>
		/// <returns>An object that contains the list of data items that the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control retrieves.</returns>
		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x0005A394 File Offset: 0x00058594
		IDataSource IDataBoundControl.DataSourceObject
		{
			get
			{
				return base.DataSourceObject;
			}
		}

		/// <summary>For a description of this property, see <see cref="P:System.Web.UI.WebControls.IFieldControl.FieldsGenerator" />. </summary>
		/// <returns>The control that automatically generates the columns for a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control. </returns>
		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x060022FA RID: 8954 RVA: 0x00003A1F File Offset: 0x00001C1F
		// (set) Token: 0x060022FB RID: 8955 RVA: 0x00003A1F File Offset: 0x00001C1F
		IAutoFieldGenerator IFieldControl.FieldsGenerator
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value indicating whether the paging feature is enabled.</summary>
		/// <returns>true to enable the paging feature; otherwise, false. The default is false.</returns>
		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x060022FC RID: 8956 RVA: 0x0005A39C File Offset: 0x0005859C
		// (set) Token: 0x060022FD RID: 8957 RVA: 0x0005A3C5 File Offset: 0x000585C5
		[WebCategory("Paging")]
		[DefaultValue(false)]
		public virtual bool AllowPaging
		{
			get
			{
				object obj = this.ViewState["AllowPaging"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowPaging"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the alternating data rows in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of alternating data rows in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x060022FE RID: 8958 RVA: 0x0005A3E3 File Offset: 0x000585E3
		[DefaultValue(null)]
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle AlternatingRowStyle
		{
			get
			{
				if (this.alternatingRowStyle == null)
				{
					this.alternatingRowStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.alternatingRowStyle.TrackViewState();
					}
				}
				return this.alternatingRowStyle;
			}
		}

		/// <summary>Gets or sets a value indicating whether the built-in controls to edit the current record are displayed in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>true to display the built-in controls to edit the current record; otherwise, false. The default is false.</returns>
		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x0005A414 File Offset: 0x00058614
		// (set) Token: 0x06002300 RID: 8960 RVA: 0x0005A43D File Offset: 0x0005863D
		[DefaultValue(false)]
		[WebCategory("Behavior")]
		public virtual bool AutoGenerateEditButton
		{
			get
			{
				object obj = this.ViewState["AutoGenerateEditButton"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AutoGenerateEditButton"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets a value indicating whether the built-in control to delete the current record is displayed in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>true to display the built-in control to delete the current record; otherwise, false. The default is false.</returns>
		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06002301 RID: 8961 RVA: 0x0005A45C File Offset: 0x0005865C
		// (set) Token: 0x06002302 RID: 8962 RVA: 0x0005A485 File Offset: 0x00058685
		[DefaultValue(false)]
		[WebCategory("Behavior")]
		public virtual bool AutoGenerateDeleteButton
		{
			get
			{
				object obj = this.ViewState["AutoGenerateDeleteButton"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AutoGenerateDeleteButton"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets a value indicating whether the built-in controls to insert a new record are displayed in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>true to display the built-in controls to insert a new record; otherwise, false. The default is false.</returns>
		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x0005A4A4 File Offset: 0x000586A4
		// (set) Token: 0x06002304 RID: 8964 RVA: 0x0005A4CD File Offset: 0x000586CD
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		public virtual bool AutoGenerateInsertButton
		{
			get
			{
				object obj = this.ViewState["AutoGenerateInsertButton"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AutoGenerateInsertButton"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets a value indicating whether row fields for each field in the data source are automatically generated and displayed in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>true to display automatically generated bound row fields for each field in the data source; otherwise, false. The default is true.</returns>
		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06002305 RID: 8965 RVA: 0x0005A4EC File Offset: 0x000586EC
		// (set) Token: 0x06002306 RID: 8966 RVA: 0x0005A515 File Offset: 0x00058715
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		public virtual bool AutoGenerateRows
		{
			get
			{
				object obj = this.ViewState["AutoGenerateRows"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["AutoGenerateRows"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets the URL to an image to display in the background of a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>The URL to an image to display in the background of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x0005A533 File Offset: 0x00058733
		// (set) Token: 0x06002308 RID: 8968 RVA: 0x0005A553 File Offset: 0x00058753
		[UrlProperty]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string BackImageUrl
		{
			get
			{
				if (base.ControlStyleCreated)
				{
					return ((TableStyle)base.ControlStyle).BackImageUrl;
				}
				return string.Empty;
			}
			set
			{
				((TableStyle)base.ControlStyle).BackImageUrl = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> object that represents the bottom pager row in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> that represents the bottom pager row in the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x0005A566 File Offset: 0x00058766
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual DetailsViewRow BottomPagerRow
		{
			get
			{
				this.EnsureChildControls();
				return this.bottomPagerRow;
			}
		}

		/// <summary>Gets or sets the text to render in an HTML caption element in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control. This property is provided to make the control more accessible to users of assistive technology devices.</summary>
		/// <returns>A string that represents the text to render in an HTML caption element in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control. The default value is an empty string ("").</returns>
		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x0005A574 File Offset: 0x00058774
		// (set) Token: 0x0600230B RID: 8971 RVA: 0x0005A5A1 File Offset: 0x000587A1
		[WebCategory("Accessibility")]
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string Caption
		{
			get
			{
				object obj = this.ViewState["Caption"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Caption"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets the horizontal or vertical position of the HTML caption element in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control. This property is provided to make the control more accessible to users of assistive technology devices.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> values. The default is TableCaptionAlign.NotSet.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> enumeration values.</exception>
		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x0005A5BC File Offset: 0x000587BC
		// (set) Token: 0x0600230D RID: 8973 RVA: 0x0005A5E5 File Offset: 0x000587E5
		[DefaultValue(TableCaptionAlign.NotSet)]
		[WebCategory("Accessibility")]
		public virtual TableCaptionAlign CaptionAlign
		{
			get
			{
				object obj = this.ViewState["CaptionAlign"];
				if (obj != null)
				{
					return (TableCaptionAlign)obj;
				}
				return TableCaptionAlign.NotSet;
			}
			set
			{
				this.ViewState["CaptionAlign"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets the amount of space between the contents of a cell and the cell's border.</summary>
		/// <returns>The amount of space, in pixels, between the contents of a cell and the cell's border. The default value is -1, which indicates that this property is not set.</returns>
		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x0600230E RID: 8974 RVA: 0x0005A603 File Offset: 0x00058803
		// (set) Token: 0x0600230F RID: 8975 RVA: 0x0005A61F File Offset: 0x0005881F
		[WebCategory("Layout")]
		[DefaultValue(-1)]
		public virtual int CellPadding
		{
			get
			{
				if (base.ControlStyleCreated)
				{
					return ((TableStyle)base.ControlStyle).CellPadding;
				}
				return -1;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellPadding = value;
			}
		}

		/// <summary>Gets or sets the amount of space between cells.</summary>
		/// <returns>The amount of space, in pixels, between cells. The default value is 0.</returns>
		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06002310 RID: 8976 RVA: 0x0005A632 File Offset: 0x00058832
		// (set) Token: 0x06002311 RID: 8977 RVA: 0x0005A64E File Offset: 0x0005884E
		[WebCategory("Layout")]
		[DefaultValue(0)]
		public virtual int CellSpacing
		{
			get
			{
				if (base.ControlStyleCreated)
				{
					return ((TableStyle)base.ControlStyle).CellSpacing;
				}
				return 0;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellSpacing = value;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of a command row in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of a command row in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x0005A661 File Offset: 0x00058861
		[DefaultValue(null)]
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle CommandRowStyle
		{
			get
			{
				if (this.commandRowStyle == null)
				{
					this.commandRowStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.commandRowStyle.TrackViewState();
					}
				}
				return this.commandRowStyle;
			}
		}

		/// <summary>Gets the current data-entry mode of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.DetailsViewMode" /> values.</returns>
		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x0005A68F File Offset: 0x0005888F
		// (set) Token: 0x06002314 RID: 8980 RVA: 0x0005A6A6 File Offset: 0x000588A6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DetailsViewMode CurrentMode
		{
			get
			{
				if (!this.hasCurrentMode)
				{
					return this.DefaultMode;
				}
				return this.currentMode;
			}
			private set
			{
				this.hasCurrentMode = true;
				this.currentMode = value;
			}
		}

		/// <summary>Get or sets the default data-entry mode of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.DetailsViewMode" /> values. The default is DetailsViewMode.ReadOnly.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.DetailsViewMode" /> enumeration values.</exception>
		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x0005A6B6 File Offset: 0x000588B6
		// (set) Token: 0x06002316 RID: 8982 RVA: 0x0005A6BE File Offset: 0x000588BE
		[DefaultValue(DetailsViewMode.ReadOnly)]
		[WebCategory("Behavior")]
		public virtual DetailsViewMode DefaultMode
		{
			get
			{
				return this.defaultMode;
			}
			set
			{
				this.defaultMode = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.DataControlField" /> objects that represent the explicitly declared row fields in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataControlFieldCollection" /> that contains all explicitly declared row fields in the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control. </returns>
		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06002317 RID: 8983 RVA: 0x0005A6D0 File Offset: 0x000588D0
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.DataControlFieldTypeEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Misc")]
		public virtual DataControlFieldCollection Fields
		{
			get
			{
				if (this.columns == null)
				{
					this.columns = new DataControlFieldCollection();
					this.columns.FieldsChanged += this.OnFieldsChanged;
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.columns).TrackViewState();
					}
				}
				return this.columns;
			}
		}

		/// <summary>Gets or sets an array that contains the names of the key fields for the data source.</summary>
		/// <returns>An array that contains the names of the key fields of the data source.</returns>
		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x0005A720 File Offset: 0x00058920
		// (set) Token: 0x06002319 RID: 8985 RVA: 0x0005A737 File Offset: 0x00058937
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Data")]
		[TypeConverter(typeof(StringArrayConverter))]
		public virtual string[] DataKeyNames
		{
			get
			{
				if (this.dataKeyNames == null)
				{
					return this.emptyKeys;
				}
				return this.dataKeyNames;
			}
			set
			{
				this.dataKeyNames = value;
				this.RequireBinding();
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x0005A746 File Offset: 0x00058946
		private IOrderedDictionary KeyTable
		{
			get
			{
				if (this._keyTable == null)
				{
					this._keyTable = new OrderedDictionary(this.DataKeyNames.Length);
				}
				return this._keyTable;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.DataKey" /> object that represents the primary key of the displayed record.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataKey" /> that represents the primary key of the displayed record.</returns>
		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x0005A769 File Offset: 0x00058969
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual DataKey DataKey
		{
			get
			{
				if (this.key == null)
				{
					this.key = new DataKey(this.KeyTable);
				}
				return this.key;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x0600231C RID: 8988 RVA: 0x0005A78A File Offset: 0x0005898A
		private DataKey OldEditValues
		{
			get
			{
				if (this.oldEditValues == null)
				{
					this.oldEditValues = new DataKey(new OrderedDictionary());
				}
				return this.oldEditValues;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the data rows when a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is in edit mode.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the data rows when a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is in edit mode.</returns>
		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x0005A7AA File Offset: 0x000589AA
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public TableItemStyle EditRowStyle
		{
			get
			{
				if (this.editRowStyle == null)
				{
					this.editRowStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.editRowStyle.TrackViewState();
					}
				}
				return this.editRowStyle;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the empty data row displayed when the data source bound to a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control does not contain any records.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that allows you to set the appearance of the empty data row.</returns>
		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x0005A7D8 File Offset: 0x000589D8
		[NotifyParentProperty(true)]
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public TableItemStyle EmptyDataRowStyle
		{
			get
			{
				if (this.emptyDataRowStyle == null)
				{
					this.emptyDataRowStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.emptyDataRowStyle.TrackViewState();
					}
				}
				return this.emptyDataRowStyle;
			}
		}

		/// <summary>Gets or sets the user-defined content for the empty data row rendered when a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is bound to a data source that does not contain any records.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the empty data row. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x0005A806 File Offset: 0x00058A06
		// (set) Token: 0x06002320 RID: 8992 RVA: 0x0005A80E File Offset: 0x00058A0E
		[Browsable(false)]
		[TemplateContainer(typeof(DetailsView), BindingDirection.OneWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public virtual ITemplate EmptyDataTemplate
		{
			get
			{
				return this.emptyDataTemplate;
			}
			set
			{
				this.emptyDataTemplate = value;
			}
		}

		/// <summary>Gets or sets the text to display in the empty data row rendered when a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is bound to a data source that does not contain any records.</summary>
		/// <returns>The text to display in the empty data row. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06002321 RID: 8993 RVA: 0x0005A818 File Offset: 0x00058A18
		// (set) Token: 0x06002322 RID: 8994 RVA: 0x0005A845 File Offset: 0x00058A45
		[WebCategory("Appearance")]
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string EmptyDataText
		{
			get
			{
				object obj = this.ViewState["EmptyDataText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["EmptyDataText"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets a value indicating whether client-side callback functions are used for paging operations in the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>true to use client-side callback functions for paging operations; otherwise, false. The default is false.</returns>
		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x0005A860 File Offset: 0x00058A60
		// (set) Token: 0x06002324 RID: 8996 RVA: 0x0005A889 File Offset: 0x00058A89
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		public virtual bool EnablePagingCallbacks
		{
			get
			{
				object obj = this.ViewState["EnablePagingCallbacks"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnablePagingCallbacks"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the header column in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the header column in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x0005A8A7 File Offset: 0x00058AA7
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle FieldHeaderStyle
		{
			get
			{
				if (this.fieldHeaderStyle == null)
				{
					this.fieldHeaderStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.fieldHeaderStyle.TrackViewState();
					}
				}
				return this.fieldHeaderStyle;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> object that represents the footer row in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> that represents the footer row in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x0005A8D5 File Offset: 0x00058AD5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual DetailsViewRow FooterRow
		{
			get
			{
				this.EnsureChildControls();
				return this.footerRow;
			}
		}

		/// <summary>Gets or sets the user-defined content for the footer row in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the footer row. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x0005A8E3 File Offset: 0x00058AE3
		// (set) Token: 0x06002328 RID: 9000 RVA: 0x0005A8EB File Offset: 0x00058AEB
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DetailsView), BindingDirection.OneWay)]
		[Browsable(false)]
		[DefaultValue(null)]
		public virtual ITemplate FooterTemplate
		{
			get
			{
				return this.footerTemplate;
			}
			set
			{
				this.footerTemplate = value;
			}
		}

		/// <summary>Gets or sets the text to display in the footer row of a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>The text to display in the footer row. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06002329 RID: 9001 RVA: 0x0005A8F4 File Offset: 0x00058AF4
		// (set) Token: 0x0600232A RID: 9002 RVA: 0x0005A921 File Offset: 0x00058B21
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		public virtual string FooterText
		{
			get
			{
				object obj = this.ViewState["FooterText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["FooterText"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the footer row in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the footer row in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x0600232B RID: 9003 RVA: 0x0005A93A File Offset: 0x00058B3A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public TableItemStyle FooterStyle
		{
			get
			{
				if (this.footerStyle == null)
				{
					this.footerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.footerStyle.TrackViewState();
					}
				}
				return this.footerStyle;
			}
		}

		/// <summary>Gets or sets the gridline style for a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.GridLines" /> values. The default is GridLines.Both.</returns>
		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x0005A968 File Offset: 0x00058B68
		// (set) Token: 0x0600232D RID: 9005 RVA: 0x0005A984 File Offset: 0x00058B84
		[DefaultValue(GridLines.Both)]
		[WebCategory("Appearance")]
		public virtual GridLines GridLines
		{
			get
			{
				if (base.ControlStyleCreated)
				{
					return ((TableStyle)base.ControlStyle).GridLines;
				}
				return GridLines.Both;
			}
			set
			{
				((TableStyle)base.ControlStyle).GridLines = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> object that represents the header row in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> that represents the header row in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x0600232E RID: 9006 RVA: 0x0005A997 File Offset: 0x00058B97
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual DetailsViewRow HeaderRow
		{
			get
			{
				this.EnsureChildControls();
				return this.headerRow;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the header row in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the header row in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x0600232F RID: 9007 RVA: 0x0005A9A5 File Offset: 0x00058BA5
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle HeaderStyle
		{
			get
			{
				if (this.headerStyle == null)
				{
					this.headerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.headerStyle.TrackViewState();
					}
				}
				return this.headerStyle;
			}
		}

		/// <summary>Gets or sets the user-defined content for the header row in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the header row. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06002330 RID: 9008 RVA: 0x0005A9D3 File Offset: 0x00058BD3
		// (set) Token: 0x06002331 RID: 9009 RVA: 0x0005A9DB File Offset: 0x00058BDB
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DetailsView), BindingDirection.OneWay)]
		[DefaultValue(null)]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this.headerTemplate;
			}
			set
			{
				this.headerTemplate = value;
			}
		}

		/// <summary>Gets or sets the text to display in the header row of a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>The text to display in the header row. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06002332 RID: 9010 RVA: 0x0005A9E4 File Offset: 0x00058BE4
		// (set) Token: 0x06002333 RID: 9011 RVA: 0x0005AA11 File Offset: 0x00058C11
		[Localizable(true)]
		[DefaultValue("")]
		[WebCategory("Appearance")]
		public virtual string HeaderText
		{
			get
			{
				object obj = this.ViewState["HeaderText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["HeaderText"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets the horizontal alignment of a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control on the page.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values. The default is HorizontalAlign.NotSet.</returns>
		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06002334 RID: 9012 RVA: 0x0005AA2A File Offset: 0x00058C2A
		// (set) Token: 0x06002335 RID: 9013 RVA: 0x0005AA46 File Offset: 0x00058C46
		[DefaultValue(HorizontalAlign.NotSet)]
		[Category("Layout")]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (base.ControlStyleCreated)
				{
					return ((TableStyle)base.ControlStyle).HorizontalAlign;
				}
				return HorizontalAlign.NotSet;
			}
			set
			{
				((TableStyle)base.ControlStyle).HorizontalAlign = value;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the data rows in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control when the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is in insert mode.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the data rows in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control when the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is in insert mode.</returns>
		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06002336 RID: 9014 RVA: 0x0005AA59 File Offset: 0x00058C59
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public TableItemStyle InsertRowStyle
		{
			get
			{
				if (this.insertRowStyle == null)
				{
					this.insertRowStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.insertRowStyle.TrackViewState();
					}
				}
				return this.insertRowStyle;
			}
		}

		/// <summary>Gets the number of records in the data source.</summary>
		/// <returns>The number of records in the data source.</returns>
		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06002337 RID: 9015 RVA: 0x0005AA87 File Offset: 0x00058C87
		// (set) Token: 0x06002338 RID: 9016 RVA: 0x0005AA8F File Offset: 0x00058C8F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int PageCount
		{
			get
			{
				return this.pageCount;
			}
			private set
			{
				this.pageCount = value;
			}
		}

		/// <summary>Gets or sets the index of the displayed record.</summary>
		/// <returns>The zero-based index of the data item being displayed in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control from the underlying data source.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than -1.</exception>
		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x0005AA98 File Offset: 0x00058C98
		// (set) Token: 0x0600233A RID: 9018 RVA: 0x0005AAAB File Offset: 0x00058CAB
		[WebCategory("Paging")]
		[Bindable(true, BindingDirection.OneWay)]
		[DefaultValue(0)]
		public virtual int PageIndex
		{
			get
			{
				if (this.CurrentMode == DetailsViewMode.Insert)
				{
					return -1;
				}
				return this.pageIndex;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("PageIndex must be non-negative");
				}
				if (this.pageIndex == value || value == -1)
				{
					return;
				}
				this.pageIndex = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> object that allows you to set the properties of the pager buttons in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> that allows you to set the properties of the pager buttons in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x0600233B RID: 9019 RVA: 0x0005AAD7 File Offset: 0x00058CD7
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Paging")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public virtual PagerSettings PagerSettings
		{
			get
			{
				if (this.pagerSettings == null)
				{
					this.pagerSettings = new PagerSettings(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.pagerSettings).TrackViewState();
					}
				}
				return this.pagerSettings;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the pager row in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the pager row in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x0600233C RID: 9020 RVA: 0x0005AB06 File Offset: 0x00058D06
		[WebCategory("Styles")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle PagerStyle
		{
			get
			{
				if (this.pagerStyle == null)
				{
					this.pagerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.pagerStyle.TrackViewState();
					}
				}
				return this.pagerStyle;
			}
		}

		/// <summary>Gets or sets the custom content for the pager row in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the pager row. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x0005AB34 File Offset: 0x00058D34
		// (set) Token: 0x0600233E RID: 9022 RVA: 0x0005AB3C File Offset: 0x00058D3C
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[TemplateContainer(typeof(DetailsView), BindingDirection.OneWay)]
		public virtual ITemplate PagerTemplate
		{
			get
			{
				return this.pagerTemplate;
			}
			set
			{
				this.pagerTemplate = value;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> objects that represent the data rows in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DetailsViewRowCollection" /> that contains all the data rows in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x0600233F RID: 9023 RVA: 0x0005AB45 File Offset: 0x00058D45
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual DetailsViewRowCollection Rows
		{
			get
			{
				this.EnsureChildControls();
				return this.rows;
			}
		}

		/// <summary>Gets or sets an object that implements the <see cref="T:System.Web.UI.IAutoFieldGenerator" /> interface in order to automatically populate rows in the view.</summary>
		/// <returns>An object that implement the <see cref="T:System.Web.UI.IAutoFieldGenerator" /> interface.</returns>
		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06002340 RID: 9024 RVA: 0x0005AB53 File Offset: 0x00058D53
		// (set) Token: 0x06002341 RID: 9025 RVA: 0x0005AB5B File Offset: 0x00058D5B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IAutoFieldGenerator RowsGenerator { get; set; }

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the data rows in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the data rows in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06002342 RID: 9026 RVA: 0x0005AB64 File Offset: 0x00058D64
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle RowStyle
		{
			get
			{
				if (this.rowStyle == null)
				{
					this.rowStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.rowStyle.TrackViewState();
					}
				}
				return this.rowStyle;
			}
		}

		/// <summary>Gets the data key value of the current record in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>The data key value of the current record in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x0005AB92 File Offset: 0x00058D92
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object SelectedValue
		{
			get
			{
				return this.DataKey.Value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> object that represents the top pager row in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> that represents the top pager row in the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06002344 RID: 9028 RVA: 0x0005AB9F File Offset: 0x00058D9F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual DetailsViewRow TopPagerRow
		{
			get
			{
				this.EnsureChildControls();
				return this.topPagerRow;
			}
		}

		/// <summary>Gets the data item bound to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the data item bound to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x0005ABAD File Offset: 0x00058DAD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual object DataItem
		{
			get
			{
				return this.dataItem;
			}
		}

		/// <summary>Gets the number of items in the underlying data source.</summary>
		/// <returns>The number of items in the underlying data source.</returns>
		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06002346 RID: 9030 RVA: 0x0005ABB5 File Offset: 0x00058DB5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int DataItemCount
		{
			get
			{
				return this.PageCount;
			}
		}

		/// <summary>Gets the index of the item being displayed in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control from the underlying data source.</summary>
		/// <returns>The zero-based index of the data item being displayed in a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control from the underlying data source.</returns>
		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x0005ABBD File Offset: 0x00058DBD
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int DataItemIndex
		{
			get
			{
				return this.PageIndex;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DisplayIndex" />.</summary>
		/// <returns>Returns 0.</returns>
		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x0005ABBD File Offset: 0x00058DBD
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.PageIndex;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.WebControls.DetailsView.DataItemIndex" />.</summary>
		/// <returns>Returns the <see cref="P:System.Web.UI.WebControls.DetailsView.DataItemIndex" /> value.</returns>
		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x0005ABC5 File Offset: 0x00058DC5
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.DataItemIndex;
			}
		}

		/// <summary>Gets or sets a value that indicates whether data-model validation is enabled.</summary>
		/// <returns>true if data model validation is enabled; otherwise, false.</returns>
		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x0600234A RID: 9034 RVA: 0x0005ABCD File Offset: 0x00058DCD
		// (set) Token: 0x0600234B RID: 9035 RVA: 0x0005ABD5 File Offset: 0x00058DD5
		[DefaultValue(true)]
		[global::System.MonoTODO("Make use of it in the code")]
		public virtual bool EnableModelValidation { get; set; }

		/// <summary>Determines whether the specified data type can be bound to a field in the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>true if the specified data type can be bound to a field in the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control; otherwise, false.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the data type to check.</param>
		// Token: 0x0600234C RID: 9036 RVA: 0x0005ABE0 File Offset: 0x00058DE0
		public virtual bool IsBindableType(Type type)
		{
			return type.IsPrimitive || type == typeof(string) || type == typeof(DateTime) || type == typeof(Guid) || type == typeof(decimal);
		}

		/// <summary>Creates the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object that gets passed to the Select command.</summary>
		/// <returns>The <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that gets passed to the Select command</returns>
		// Token: 0x0600234D RID: 9037 RVA: 0x0005AC40 File Offset: 0x00058E40
		protected override DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			DataSourceSelectArguments dataSourceSelectArguments = new DataSourceSelectArguments();
			DataSourceView data = this.GetData();
			if (this.AllowPaging && data.CanPage)
			{
				dataSourceSelectArguments.StartRowIndex = this.PageIndex;
				if (data.CanRetrieveTotalRowCount)
				{
					dataSourceSelectArguments.RetrieveTotalRowCount = true;
					dataSourceSelectArguments.MaximumRows = 1;
				}
				else
				{
					dataSourceSelectArguments.MaximumRows = -1;
				}
			}
			return dataSourceSelectArguments;
		}

		/// <summary>Creates the complete set of automatically generated and user-defined row fields used to generate the control hierarchy.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains both the automatically generated and the user-defined row fields for the specified data item.</returns>
		/// <param name="dataItem">The data item for which to create the row fields.</param>
		/// <param name="useDataSource">true to use the data item to create the automatically generated row fields; otherwise, false.</param>
		// Token: 0x0600234E RID: 9038 RVA: 0x0005AC98 File Offset: 0x00058E98
		protected virtual ICollection CreateFieldSet(object dataItem, bool useDataSource)
		{
			if (this.AutoGenerateRows)
			{
				IAutoFieldGenerator rowsGenerator = this.RowsGenerator;
				if (rowsGenerator != null)
				{
					return rowsGenerator.GenerateFields(this);
				}
			}
			ArrayList arrayList = new ArrayList();
			if (this.AutoGenerateRows)
			{
				if (useDataSource)
				{
					if (dataItem != null)
					{
						arrayList.AddRange(this.CreateAutoGeneratedRows(dataItem));
					}
				}
				else if (this.autoFieldProperties != null)
				{
					foreach (AutoGeneratedFieldProperties autoGeneratedFieldProperties in this.autoFieldProperties)
					{
						arrayList.Add(this.CreateAutoGeneratedRow(autoGeneratedFieldProperties));
					}
				}
			}
			arrayList.AddRange(this.Fields);
			if (this.AutoGenerateEditButton || this.AutoGenerateDeleteButton || this.AutoGenerateInsertButton)
			{
				arrayList.Add(new CommandField
				{
					ShowEditButton = this.AutoGenerateEditButton,
					ShowDeleteButton = this.AutoGenerateDeleteButton,
					ShowInsertButton = this.AutoGenerateInsertButton
				});
			}
			return arrayList;
		}

		/// <summary>Creates a set of automatically generated row fields for the specified data item.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the automatically generated row fields for the specified data item.</returns>
		/// <param name="dataItem">The data item for which to create the automatically generated row fields.</param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.WebControls.DetailsView" /> control does not have any properties or attributes from which to generate fields.</exception>
		// Token: 0x0600234F RID: 9039 RVA: 0x0005AD70 File Offset: 0x00058F70
		protected virtual ICollection CreateAutoGeneratedRows(object dataItem)
		{
			if (dataItem == null)
			{
				return null;
			}
			ArrayList arrayList = new ArrayList();
			this.autoFieldProperties = this.CreateAutoFieldProperties(dataItem);
			foreach (AutoGeneratedFieldProperties autoGeneratedFieldProperties in this.autoFieldProperties)
			{
				arrayList.Add(this.CreateAutoGeneratedRow(autoGeneratedFieldProperties));
			}
			return arrayList;
		}

		/// <summary>Creates an <see cref="T:System.Web.UI.WebControls.AutoGeneratedField" /> object that represents an automatically generated row field using the specified field properties.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.AutoGeneratedField" /> that contains the field properties specified by the <paramref name="fieldProperties" /> parameter.</returns>
		/// <param name="fieldProperties">An <see cref="T:System.Web.UI.WebControls.AutoGeneratedFieldProperties" /> that contains the properties for the <see cref="T:System.Web.UI.WebControls.AutoGeneratedField" />.</param>
		// Token: 0x06002350 RID: 9040 RVA: 0x0005ADBD File Offset: 0x00058FBD
		protected virtual AutoGeneratedField CreateAutoGeneratedRow(AutoGeneratedFieldProperties fieldProperties)
		{
			return new AutoGeneratedField(fieldProperties);
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x0005ADC8 File Offset: 0x00058FC8
		private AutoGeneratedFieldProperties[] CreateAutoFieldProperties(object dataItem)
		{
			if (this.IsBindableType(dataItem.GetType()))
			{
				AutoGeneratedFieldProperties autoGeneratedFieldProperties = new AutoGeneratedFieldProperties();
				((IStateManager)autoGeneratedFieldProperties).TrackViewState();
				autoGeneratedFieldProperties.Name = "Item";
				autoGeneratedFieldProperties.DataField = BoundField.ThisExpression;
				autoGeneratedFieldProperties.Type = dataItem.GetType();
				return new AutoGeneratedFieldProperties[] { autoGeneratedFieldProperties };
			}
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(dataItem, false);
			if (properties != null && properties.Count > 0)
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in properties)
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
					if (this.IsBindableType(propertyDescriptor.PropertyType))
					{
						AutoGeneratedFieldProperties autoGeneratedFieldProperties2 = new AutoGeneratedFieldProperties();
						((IStateManager)autoGeneratedFieldProperties2).TrackViewState();
						autoGeneratedFieldProperties2.Name = propertyDescriptor.Name;
						autoGeneratedFieldProperties2.DataField = propertyDescriptor.Name;
						for (int i = 0; i < this.DataKeyNames.Length; i++)
						{
							if (string.Compare(this.DataKeyNames[i], propertyDescriptor.Name, StringComparison.InvariantCultureIgnoreCase) == 0)
							{
								autoGeneratedFieldProperties2.IsReadOnly = true;
								break;
							}
						}
						autoGeneratedFieldProperties2.Type = propertyDescriptor.PropertyType;
						arrayList.Add(autoGeneratedFieldProperties2);
					}
				}
				if (arrayList.Count > 0)
				{
					return (AutoGeneratedFieldProperties[])arrayList.ToArray(typeof(AutoGeneratedFieldProperties));
				}
			}
			throw new HttpException(string.Format("DetailsView with id '{0}' did not have any properties or attributes from which to generate fields.  Ensure that your data source has content.", this.ID));
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> object using the specified item index, row type, and row state.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> with the specified item index, row type, and row state.</returns>
		/// <param name="rowIndex">The zero-based index of the data item to display.</param>
		/// <param name="rowType">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> values.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		// Token: 0x06002352 RID: 9042 RVA: 0x0005AF44 File Offset: 0x00059144
		protected virtual DetailsViewRow CreateRow(int rowIndex, DataControlRowType rowType, DataControlRowState rowState)
		{
			DetailsViewRow detailsViewRow;
			if (rowType == DataControlRowType.Pager)
			{
				detailsViewRow = new DetailsViewPagerRow(rowIndex, rowType, rowState);
			}
			else
			{
				detailsViewRow = new DetailsViewRow(rowIndex, rowType, rowState);
			}
			return detailsViewRow;
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x000478FE File Offset: 0x00045AFE
		private void RequireBinding()
		{
			if (base.Initialized)
			{
				base.RequiresDataBinding = true;
			}
		}

		/// <summary>Creates the containing table for the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Table" /> that represents the containing table for the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control. </returns>
		// Token: 0x06002354 RID: 9044 RVA: 0x0005AF6A File Offset: 0x0005916A
		protected virtual Table CreateTable()
		{
			return new ContainedTable(this);
		}

		/// <summary>Creates a default table style object for the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains the default table style for the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x06002355 RID: 9045 RVA: 0x00056369 File Offset: 0x00054569
		protected override Style CreateControlStyle()
		{
			return new TableStyle
			{
				GridLines = GridLines.Both,
				CellSpacing = 0
			};
		}

		/// <summary>Creates the control hierarchy used to render the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>The number of items in the data source.</returns>
		/// <param name="dataSource">An <see cref="T:System.Collections.IEnumerable" /> that represents the data source for the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</param>
		/// <param name="dataBinding">true to indicate that this method is being called during data binding; otherwise, false.</param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="dataSource" /> returns a null <see cref="T:System.Web.UI.DataSourceView" />. - or -d<paramref name="ataSource" /> is not an <see cref="T:System.Collections.ICollection" /> and cannot return a total row count.- or -<paramref name="dataBinding" /> is false and <paramref name="dataSource" /> does not implement the <see cref="T:System.Collections.ICollection" /> interface.- or -<paramref name="dataSource" /> does not implement the <see cref="T:System.Collections.ICollection" /> interface and <see cref="P:System.Web.UI.WebControls.DetailsView.AllowPaging" /> is set to true.</exception>
		// Token: 0x06002356 RID: 9046 RVA: 0x0005AF74 File Offset: 0x00059174
		protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
		{
			PagedDataSource pagedDataSource = new PagedDataSource();
			pagedDataSource.DataSource = ((this.CurrentMode != DetailsViewMode.Insert) ? dataSource : null);
			pagedDataSource.AllowPaging = this.AllowPaging;
			pagedDataSource.PageSize = 1;
			pagedDataSource.CurrentPageIndex = this.PageIndex;
			if (dataBinding && this.CurrentMode != DetailsViewMode.Insert)
			{
				DataSourceView data = this.GetData();
				if (data != null && data.CanPage)
				{
					pagedDataSource.AllowServerPaging = true;
					if (base.SelectArguments.RetrieveTotalRowCount)
					{
						pagedDataSource.VirtualCount = base.SelectArguments.TotalRowCount;
					}
				}
			}
			bool flag = this.AllowPaging && pagedDataSource.PageCount > 1;
			this.Controls.Clear();
			this.table = this.CreateTable();
			this.Controls.Add(this.table);
			this.headerRow = null;
			this.footerRow = null;
			this.topPagerRow = null;
			this.bottomPagerRow = null;
			ArrayList arrayList = new ArrayList();
			if (this.AllowPaging)
			{
				this.PageCount = pagedDataSource.DataSourceCount;
				if (this.PageIndex >= this.PageCount && this.PageCount > 0)
				{
					this.pageIndex = (pagedDataSource.CurrentPageIndex = this.PageCount - 1);
				}
				if (pagedDataSource.DataSource != null)
				{
					IEnumerator enumerator = pagedDataSource.GetEnumerator();
					if (enumerator.MoveNext())
					{
						this.dataItem = enumerator.Current;
					}
				}
			}
			else
			{
				int num = 0;
				object obj = null;
				if (pagedDataSource.DataSource != null)
				{
					IEnumerator enumerator2 = pagedDataSource.GetEnumerator();
					while (enumerator2.MoveNext())
					{
						obj = enumerator2.Current;
						if (num == this.PageIndex)
						{
							this.dataItem = enumerator2.Current;
						}
						num++;
					}
				}
				this.PageCount = num;
				if (this.PageIndex >= this.PageCount && this.PageCount > 0)
				{
					this.pageIndex = this.PageCount - 1;
					this.dataItem = obj;
				}
			}
			if (this.PageCount == 0 && this.CurrentMode != DetailsViewMode.Insert)
			{
				DetailsViewRow detailsViewRow = this.CreateEmptyRow();
				if (detailsViewRow != null)
				{
					this.table.Rows.Add(detailsViewRow);
					arrayList.Add(detailsViewRow);
				}
			}
			else
			{
				ICollection collection = this.CreateFieldSet(this.dataItem, dataBinding && this.dataItem != null);
				DataControlField[] array = new DataControlField[collection.Count];
				collection.CopyTo(array, 0);
				foreach (DataControlField dataControlField in array)
				{
					dataControlField.Initialize(false, this);
					if (this.EnablePagingCallbacks)
					{
						dataControlField.ValidateSupportsCallback();
					}
				}
				this.headerRow = this.CreateRow(-1, DataControlRowType.Header, DataControlRowState.Normal);
				DataControlFieldCell dataControlFieldCell = new DataControlFieldCell(null);
				dataControlFieldCell.ColumnSpan = 2;
				if (this.headerTemplate != null)
				{
					this.headerTemplate.InstantiateIn(dataControlFieldCell);
				}
				else if (!string.IsNullOrEmpty(this.HeaderText))
				{
					dataControlFieldCell.Text = this.HeaderText;
				}
				else
				{
					this.headerRow.Visible = false;
				}
				this.headerRow.Cells.Add(dataControlFieldCell);
				this.table.Rows.Add(this.headerRow);
				if ((flag && this.PagerSettings.Position == PagerPosition.Top) || this.PagerSettings.Position == PagerPosition.TopAndBottom)
				{
					this.topPagerRow = this.CreateRow(-1, DataControlRowType.Pager, DataControlRowState.Normal);
					this.InitializePager(this.topPagerRow, pagedDataSource);
					this.table.Rows.Add(this.topPagerRow);
				}
				foreach (DataControlField dataControlField2 in array)
				{
					DataControlRowState rowState = this.GetRowState(arrayList.Count);
					DetailsViewRow detailsViewRow2 = this.CreateRow(this.PageIndex, DataControlRowType.DataRow, rowState);
					this.InitializeRow(detailsViewRow2, dataControlField2);
					this.table.Rows.Add(detailsViewRow2);
					arrayList.Add(detailsViewRow2);
				}
				this.footerRow = this.CreateRow(-1, DataControlRowType.Footer, DataControlRowState.Normal);
				DataControlFieldCell dataControlFieldCell2 = new DataControlFieldCell(null);
				dataControlFieldCell2.ColumnSpan = 2;
				if (this.footerTemplate != null)
				{
					this.footerTemplate.InstantiateIn(dataControlFieldCell2);
				}
				else if (!string.IsNullOrEmpty(this.FooterText))
				{
					dataControlFieldCell2.Text = this.FooterText;
				}
				else
				{
					this.footerRow.Visible = false;
				}
				this.footerRow.Cells.Add(dataControlFieldCell2);
				this.table.Rows.Add(this.footerRow);
				if ((flag && this.PagerSettings.Position == PagerPosition.Bottom) || this.PagerSettings.Position == PagerPosition.TopAndBottom)
				{
					this.bottomPagerRow = this.CreateRow(-1, DataControlRowType.Pager, DataControlRowState.Normal);
					this.InitializePager(this.bottomPagerRow, pagedDataSource);
					this.table.Rows.Add(this.bottomPagerRow);
				}
			}
			this.rows = new DetailsViewRowCollection(arrayList);
			if (dataBinding)
			{
				this.DataBind(false);
			}
			this.OnItemCreated(EventArgs.Empty);
			return this.PageCount;
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.WebControls.BaseDataBoundControl.DataBind" /> method after verifying that the data listing control requires data binding and that a valid data source control is specified. </summary>
		// Token: 0x06002357 RID: 9047 RVA: 0x0005B430 File Offset: 0x00059630
		protected override void EnsureDataBound()
		{
			if (this.CurrentMode == DetailsViewMode.Insert)
			{
				if (base.RequiresDataBinding)
				{
					this.OnDataBinding(EventArgs.Empty);
					base.RequiresDataBinding = false;
					base.InternalPerformDataBinding(null);
					base.MarkAsDataBound();
					this.OnDataBound(EventArgs.Empty);
					return;
				}
			}
			else
			{
				base.EnsureDataBound();
			}
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x0005B480 File Offset: 0x00059680
		private DataControlRowState GetRowState(int index)
		{
			DataControlRowState dataControlRowState = ((index % 2 == 0) ? DataControlRowState.Normal : DataControlRowState.Alternate);
			if (this.CurrentMode == DetailsViewMode.Edit)
			{
				dataControlRowState |= DataControlRowState.Edit;
			}
			else if (this.CurrentMode == DetailsViewMode.Insert)
			{
				dataControlRowState |= DataControlRowState.Insert;
			}
			return dataControlRowState;
		}

		/// <summary>Creates the pager row for the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <param name="row">The <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> that contains the pager row.</param>
		/// <param name="pagedDataSource">A <see cref="T:System.Web.UI.WebControls.PagedDataSource" /> that contains the data for the current page.</param>
		// Token: 0x06002359 RID: 9049 RVA: 0x0005B4B4 File Offset: 0x000596B4
		protected virtual void InitializePager(DetailsViewRow row, PagedDataSource pagedDataSource)
		{
			TableCell tableCell = new TableCell();
			tableCell.ColumnSpan = 2;
			if (this.pagerTemplate != null)
			{
				this.pagerTemplate.InstantiateIn(tableCell);
			}
			else
			{
				tableCell.Controls.Add(this.PagerSettings.CreatePagerControl(pagedDataSource.CurrentPageIndex, pagedDataSource.PageCount));
			}
			row.Cells.Add(tableCell);
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x0005B514 File Offset: 0x00059714
		private DetailsViewRow CreateEmptyRow()
		{
			TableCell tableCell = new TableCell();
			if (this.emptyDataTemplate != null)
			{
				this.emptyDataTemplate.InstantiateIn(tableCell);
			}
			else
			{
				if (string.IsNullOrEmpty(this.EmptyDataText))
				{
					return null;
				}
				tableCell.Text = this.EmptyDataText;
			}
			DetailsViewRow detailsViewRow = this.CreateRow(-1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
			detailsViewRow.Cells.Add(tableCell);
			return detailsViewRow;
		}

		/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> object.</summary>
		/// <param name="row">The <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> to initialize.</param>
		/// <param name="field">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> that corresponds to the row.</param>
		// Token: 0x0600235B RID: 9051 RVA: 0x0005B570 File Offset: 0x00059770
		protected virtual void InitializeRow(DetailsViewRow row, DataControlField field)
		{
			if (!field.Visible)
			{
				row.Visible = false;
				return;
			}
			row.ContainingField = field;
			DataControlFieldCell dataControlFieldCell;
			if (field.ShowHeader)
			{
				dataControlFieldCell = new DataControlFieldCell(field);
				row.Cells.Add(dataControlFieldCell);
				field.InitializeCell(dataControlFieldCell, DataControlCellType.Header, row.RowState, row.RowIndex);
			}
			dataControlFieldCell = new DataControlFieldCell(field);
			if (!field.ShowHeader)
			{
				dataControlFieldCell.ColumnSpan = 2;
			}
			row.Cells.Add(dataControlFieldCell);
			field.InitializeCell(dataControlFieldCell, DataControlCellType.DataCell, row.RowState, row.RowIndex);
			if (this.CurrentMode == DetailsViewMode.Insert && !field.InsertVisible)
			{
				row.Visible = false;
			}
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x0005B614 File Offset: 0x00059814
		private void FillRowDataKey(object dataItem)
		{
			this.KeyTable.Clear();
			if (this.cachedKeyProperties == null)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(dataItem);
				this.cachedKeyProperties = new PropertyDescriptor[this.DataKeyNames.Length];
				for (int i = 0; i < this.DataKeyNames.Length; i++)
				{
					PropertyDescriptor propertyDescriptor = properties.Find(this.DataKeyNames[i], true);
					if (propertyDescriptor == null)
					{
						throw new InvalidOperationException(string.Concat(new object[]
						{
							"Property '",
							this.DataKeyNames[i],
							"' not found in object of type ",
							dataItem.GetType()
						}));
					}
					this.cachedKeyProperties[i] = propertyDescriptor;
				}
			}
			foreach (PropertyDescriptor propertyDescriptor2 in this.cachedKeyProperties)
			{
				this.KeyTable[propertyDescriptor2.Name] = propertyDescriptor2.GetValue(dataItem);
			}
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x0005B6EC File Offset: 0x000598EC
		private IOrderedDictionary GetRowValues(bool includeReadOnlyFields, bool includePrimaryKey)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			this.ExtractRowValues(orderedDictionary, includeReadOnlyFields, includePrimaryKey);
			return orderedDictionary;
		}

		/// <summary>Retrieves the values of each field displayed and stores them in the specified <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object.</summary>
		/// <param name="fieldValues">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> used to store the field values.</param>
		/// <param name="includeReadOnlyFields">true to include read-only fields; otherwise, false.</param>
		/// <param name="includeKeys">true to include the primary key field or fields; otherwise, false.</param>
		// Token: 0x0600235E RID: 9054 RVA: 0x0005B70C File Offset: 0x0005990C
		protected virtual void ExtractRowValues(IOrderedDictionary fieldValues, bool includeReadOnlyFields, bool includeKeys)
		{
			foreach (object obj in this.Rows)
			{
				DetailsViewRow detailsViewRow = (DetailsViewRow)obj;
				if (detailsViewRow.Cells.Count >= 1)
				{
					DataControlFieldCell dataControlFieldCell = detailsViewRow.Cells[detailsViewRow.Cells.Count - 1] as DataControlFieldCell;
					if (dataControlFieldCell != null)
					{
						dataControlFieldCell.ContainingField.ExtractValuesFromCell(fieldValues, dataControlFieldCell, detailsViewRow.RowState, includeReadOnlyFields);
					}
				}
			}
			if (!includeKeys && this.DataKeyNames != null)
			{
				foreach (string text in this.DataKeyNames)
				{
					fieldValues.Remove(text);
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control. </summary>
		/// <returns>If <see cref="P:System.Web.UI.WebControls.DetailsView.EnablePagingCallbacks" /> is true, this property returns <see cref="F:System.Web.UI.HtmlTextWriterTag.Div" />. Otherwise, it returns <see cref="F:System.Web.UI.HtmlTextWriterTag.Table" />. </returns>
		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x0600235F RID: 9055 RVA: 0x0005B7D8 File Offset: 0x000599D8
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				if (this.EnablePagingCallbacks)
				{
					return HtmlTextWriterTag.Div;
				}
				return HtmlTextWriterTag.Table;
			}
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.WebControls.BaseDataBoundControl.DataBind" /> method of the base class. </summary>
		// Token: 0x06002360 RID: 9056 RVA: 0x0005B7E8 File Offset: 0x000599E8
		public sealed override void DataBind()
		{
			this.cachedKeyProperties = null;
			base.DataBind();
			if (this.dataItem != null)
			{
				if (this.CurrentMode == DetailsViewMode.Edit)
				{
					this.oldEditValues = new DataKey(this.GetRowValues(false, true));
				}
				this.FillRowDataKey(this.dataItem);
				this.key = new DataKey(this.KeyTable);
			}
		}

		/// <summary>Binds the specified data source to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <param name="data">An <see cref="T:System.Collections.IEnumerable" /> that represents the data source.</param>
		// Token: 0x06002361 RID: 9057 RVA: 0x0005B843 File Offset: 0x00059A43
		protected internal override void PerformDataBinding(IEnumerable data)
		{
			base.PerformDataBinding(data);
		}

		/// <summary>Sets up the control hierarchy of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		// Token: 0x06002362 RID: 9058 RVA: 0x0005B84C File Offset: 0x00059A4C
		protected internal virtual void PrepareControlHierarchy()
		{
			if (this.table == null)
			{
				return;
			}
			this.table.Caption = this.Caption;
			this.table.CaptionAlign = this.CaptionAlign;
			foreach (object obj in this.table.Rows)
			{
				DetailsViewRow detailsViewRow = (DetailsViewRow)obj;
				switch (detailsViewRow.RowType)
				{
				case DataControlRowType.Header:
					if (this.headerStyle != null && !this.headerStyle.IsEmpty)
					{
						detailsViewRow.ControlStyle.CopyFrom(this.headerStyle);
					}
					break;
				case DataControlRowType.Footer:
					if (this.footerStyle != null && !this.footerStyle.IsEmpty)
					{
						detailsViewRow.ControlStyle.CopyFrom(this.footerStyle);
					}
					break;
				case DataControlRowType.DataRow:
					if (this.rowStyle != null && !this.rowStyle.IsEmpty)
					{
						detailsViewRow.ControlStyle.CopyFrom(this.rowStyle);
					}
					if ((detailsViewRow.RowState & DataControlRowState.Alternate) != DataControlRowState.Normal && this.alternatingRowStyle != null && !this.alternatingRowStyle.IsEmpty)
					{
						detailsViewRow.ControlStyle.CopyFrom(this.alternatingRowStyle);
					}
					break;
				case DataControlRowType.Pager:
					if (this.pagerStyle != null && !this.pagerStyle.IsEmpty)
					{
						detailsViewRow.ControlStyle.CopyFrom(this.pagerStyle);
					}
					break;
				case DataControlRowType.EmptyDataRow:
					if (this.emptyDataRowStyle != null && !this.emptyDataRowStyle.IsEmpty)
					{
						detailsViewRow.ControlStyle.CopyFrom(this.emptyDataRowStyle);
					}
					break;
				}
				if (detailsViewRow.ContainingField is CommandField)
				{
					if (this.commandRowStyle != null && !this.commandRowStyle.IsEmpty)
					{
						detailsViewRow.ControlStyle.CopyFrom(this.commandRowStyle);
					}
				}
				else
				{
					if ((detailsViewRow.RowState & DataControlRowState.Edit) != DataControlRowState.Normal && this.editRowStyle != null && !this.editRowStyle.IsEmpty)
					{
						detailsViewRow.ControlStyle.CopyFrom(this.editRowStyle);
					}
					if ((detailsViewRow.RowState & DataControlRowState.Insert) != DataControlRowState.Normal)
					{
						if (this.insertRowStyle != null && !this.insertRowStyle.IsEmpty)
						{
							detailsViewRow.ControlStyle.CopyFrom(this.insertRowStyle);
						}
						else if (this.editRowStyle != null && !this.editRowStyle.IsEmpty)
						{
							detailsViewRow.ControlStyle.CopyFrom(this.editRowStyle);
						}
					}
				}
				for (int i = 0; i < detailsViewRow.Cells.Count; i++)
				{
					DataControlFieldCell dataControlFieldCell = detailsViewRow.Cells[i] as DataControlFieldCell;
					if (dataControlFieldCell != null && dataControlFieldCell.ContainingField != null)
					{
						DataControlField containingField = dataControlFieldCell.ContainingField;
						if (i == 0 && containingField.ShowHeader)
						{
							if (this.fieldHeaderStyle != null && !this.fieldHeaderStyle.IsEmpty)
							{
								dataControlFieldCell.ControlStyle.CopyFrom(this.fieldHeaderStyle);
							}
							if (containingField.HeaderStyleCreated && !containingField.HeaderStyle.IsEmpty)
							{
								dataControlFieldCell.ControlStyle.CopyFrom(containingField.HeaderStyle);
							}
						}
						else
						{
							if (containingField.ControlStyleCreated && !containingField.ControlStyle.IsEmpty)
							{
								foreach (object obj2 in dataControlFieldCell.Controls)
								{
									WebControl webControl = ((Control)obj2) as WebControl;
									if (webControl != null)
									{
										webControl.ControlStyle.MergeWith(containingField.ControlStyle);
									}
								}
							}
							if (containingField.ItemStyleCreated && !containingField.ItemStyle.IsEmpty)
							{
								dataControlFieldCell.ControlStyle.CopyFrom(containingField.ItemStyle);
							}
						}
					}
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002363 RID: 9059 RVA: 0x0005BC48 File Offset: 0x00059E48
		protected internal override void OnInit(EventArgs e)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.RegisterRequiresControlState(this);
			}
			base.OnInit(e);
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x0005BC6D File Offset: 0x00059E6D
		private void OnFieldsChanged(object sender, EventArgs args)
		{
			this.RequireBinding();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.DataSourceView.DataSourceViewChanged" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002365 RID: 9061 RVA: 0x0005BC75 File Offset: 0x00059E75
		protected override void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			base.OnDataSourceViewChanged(sender, e);
			this.RequireBinding();
		}

		/// <summary>Determines whether the event for the Web server control is passed up the page's user interface (UI) server control hierarchy.</summary>
		/// <returns>true if the event has been canceled; otherwise, false.</returns>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002366 RID: 9062 RVA: 0x0005BC88 File Offset: 0x00059E88
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			DetailsViewCommandEventArgs detailsViewCommandEventArgs = e as DetailsViewCommandEventArgs;
			if (detailsViewCommandEventArgs != null)
			{
				bool flag = false;
				IButtonControl buttonControl = detailsViewCommandEventArgs.CommandSource as IButtonControl;
				if (buttonControl != null && buttonControl.CausesValidation)
				{
					this.Page.Validate(buttonControl.ValidationGroup);
					flag = true;
				}
				this.ProcessCommand(detailsViewCommandEventArgs, flag);
				return true;
			}
			return base.OnBubbleEvent(source, e);
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x0005BCDD File Offset: 0x00059EDD
		private void ProcessCommand(DetailsViewCommandEventArgs args, bool causesValidation)
		{
			this.OnItemCommand(args);
			this.ProcessEvent(args.CommandName, args.CommandArgument as string, causesValidation);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" />.</summary>
		/// <param name="eventArgument">A string that represents an optional event argument to pass to the event handler. </param>
		// Token: 0x06002368 RID: 9064 RVA: 0x0005BCFE File Offset: 0x00059EFE
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Raises the appropriate events for the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">The event argument from which to create a <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> for the event or events that are raised.</param>
		// Token: 0x06002369 RID: 9065 RVA: 0x0005BD08 File Offset: 0x00059F08
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			int num = eventArgument.IndexOf('$');
			CommandEventArgs commandEventArgs;
			if (num != -1)
			{
				commandEventArgs = new CommandEventArgs(eventArgument.Substring(0, num), eventArgument.Substring(num + 1));
			}
			else
			{
				commandEventArgs = new CommandEventArgs(eventArgument, null);
			}
			this.ProcessCommand(new DetailsViewCommandEventArgs(this, commandEventArgs), false);
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x0005BD60 File Offset: 0x00059F60
		private void ProcessEvent(string eventName, string param, bool causesValidation)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(eventName);
			if (num <= 1847791252U)
			{
				if (num <= 900713019U)
				{
					if (num != 254900552U)
					{
						if (num != 900713019U)
						{
							return;
						}
						if (!(eventName == "Cancel"))
						{
							return;
						}
						this.CancelEdit();
						return;
					}
					else
					{
						if (!(eventName == "Insert"))
						{
							return;
						}
						this.InsertItem(causesValidation);
					}
				}
				else if (num != 907026896U)
				{
					if (num != 1469573738U)
					{
						if (num != 1847791252U)
						{
							return;
						}
						if (!(eventName == "Update"))
						{
							return;
						}
						this.UpdateItem(param, causesValidation);
						return;
					}
					else
					{
						if (!(eventName == "Delete"))
						{
							return;
						}
						this.DeleteItem();
						return;
					}
				}
				else
				{
					if (!(eventName == "Prev"))
					{
						return;
					}
					if (this.PageIndex > 0)
					{
						this.SetPageIndex(this.PageIndex - 1);
						return;
					}
				}
			}
			else if (num <= 3705854472U)
			{
				if (num != 2334404017U)
				{
					if (num != 3267849393U)
					{
						if (num != 3705854472U)
						{
							return;
						}
						if (!(eventName == "Next"))
						{
							return;
						}
						if (this.PageIndex < this.PageCount - 1)
						{
							this.SetPageIndex(this.PageIndex + 1);
							return;
						}
					}
					else
					{
						if (!(eventName == "Edit"))
						{
							return;
						}
						this.ProcessChangeMode(DetailsViewMode.Edit);
						return;
					}
				}
				else
				{
					if (!(eventName == "New"))
					{
						return;
					}
					this.ProcessChangeMode(DetailsViewMode.Insert);
					return;
				}
			}
			else if (num != 3826132025U)
			{
				if (num != 3896349078U)
				{
					if (num != 3996994017U)
					{
						return;
					}
					if (!(eventName == "First"))
					{
						return;
					}
					this.SetPageIndex(0);
					return;
				}
				else
				{
					if (!(eventName == "Page"))
					{
						return;
					}
					int num3;
					if (!(param == "First"))
					{
						if (!(param == "Last"))
						{
							if (!(param == "Next"))
							{
								if (!(param == "Prev"))
								{
									int num2 = 0;
									int.TryParse(param, out num2);
									num3 = num2 - 1;
								}
								else
								{
									num3 = this.PageIndex - 1;
								}
							}
							else
							{
								num3 = this.PageIndex + 1;
							}
						}
						else
						{
							num3 = this.PageCount - 1;
						}
					}
					else
					{
						num3 = 0;
					}
					this.SetPageIndex(num3);
					return;
				}
			}
			else
			{
				if (!(eventName == "Last"))
				{
					return;
				}
				this.SetPageIndex(this.PageCount - 1);
				return;
			}
		}

		/// <summary>Sets the index of the currently displayed page in the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <param name="index">The index value to set.</param>
		// Token: 0x0600236B RID: 9067 RVA: 0x0005BFA8 File Offset: 0x0005A1A8
		public void SetPageIndex(int index)
		{
			DetailsViewPageEventArgs detailsViewPageEventArgs = new DetailsViewPageEventArgs(index);
			this.OnPageIndexChanging(detailsViewPageEventArgs);
			if (detailsViewPageEventArgs.Cancel || !base.IsBoundUsingDataSourceID)
			{
				return;
			}
			if (detailsViewPageEventArgs.NewPageIndex < 0 || detailsViewPageEventArgs.NewPageIndex >= this.PageCount)
			{
				return;
			}
			this.EndRowEdit(false);
			this.PageIndex = detailsViewPageEventArgs.NewPageIndex;
			this.OnPageIndexChanged(EventArgs.Empty);
		}

		/// <summary>Switches the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control to the specified mode. </summary>
		/// <param name="newMode">One of the <see cref="T:System.Web.UI.WebControls.DetailsViewMode" /> values.</param>
		// Token: 0x0600236C RID: 9068 RVA: 0x0005C00A File Offset: 0x0005A20A
		public void ChangeMode(DetailsViewMode newMode)
		{
			this.CurrentMode = newMode;
			this.RequireBinding();
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x0005C01C File Offset: 0x0005A21C
		private void ProcessChangeMode(DetailsViewMode newMode)
		{
			DetailsViewModeEventArgs detailsViewModeEventArgs = new DetailsViewModeEventArgs(newMode, false);
			this.OnModeChanging(detailsViewModeEventArgs);
			if (detailsViewModeEventArgs.Cancel || !base.IsBoundUsingDataSourceID)
			{
				return;
			}
			this.ChangeMode(detailsViewModeEventArgs.NewMode);
			this.OnModeChanged(EventArgs.Empty);
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x0005C060 File Offset: 0x0005A260
		private void CancelEdit()
		{
			DetailsViewModeEventArgs detailsViewModeEventArgs = new DetailsViewModeEventArgs(DetailsViewMode.ReadOnly, true);
			this.OnModeChanging(detailsViewModeEventArgs);
			if (detailsViewModeEventArgs.Cancel || !base.IsBoundUsingDataSourceID)
			{
				return;
			}
			this.EndRowEdit();
		}

		/// <summary>Updates the current record in the data source.</summary>
		/// <param name="causesValidation">true to perform page validation when the method is called; otherwise, false.</param>
		/// <exception cref="T:System.Web.HttpException">This method is called when the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is not in edit mode.- or -The <see cref="T:System.Web.UI.DataSourceView" /> associated with the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is null.</exception>
		// Token: 0x0600236F RID: 9071 RVA: 0x0005C093 File Offset: 0x0005A293
		public virtual void UpdateItem(bool causesValidation)
		{
			this.UpdateItem(null, causesValidation);
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x0005C0A0 File Offset: 0x0005A2A0
		private void UpdateItem(string param, bool causesValidation)
		{
			if (causesValidation && this.Page != null && !this.Page.IsValid)
			{
				return;
			}
			if (this.CurrentMode != DetailsViewMode.Edit)
			{
				throw new HttpException();
			}
			this.currentEditOldValues = this.OldEditValues.Values;
			this.currentEditRowKeys = this.DataKey.Values;
			this.currentEditNewValues = this.GetRowValues(false, false);
			DetailsViewUpdateEventArgs detailsViewUpdateEventArgs = new DetailsViewUpdateEventArgs(param, this.currentEditRowKeys, this.currentEditOldValues, this.currentEditNewValues);
			this.OnItemUpdating(detailsViewUpdateEventArgs);
			if (detailsViewUpdateEventArgs.Cancel || !base.IsBoundUsingDataSourceID)
			{
				return;
			}
			DataSourceView data = this.GetData();
			if (data == null)
			{
				throw new HttpException("The DataSourceView associated to data bound control was null");
			}
			data.Update(this.currentEditRowKeys, this.currentEditNewValues, this.currentEditOldValues, new DataSourceViewOperationCallback(this.UpdateCallback));
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x0005C170 File Offset: 0x0005A370
		private bool UpdateCallback(int recordsAffected, Exception exception)
		{
			DetailsViewUpdatedEventArgs detailsViewUpdatedEventArgs = new DetailsViewUpdatedEventArgs(recordsAffected, exception, this.currentEditRowKeys, this.currentEditOldValues, this.currentEditNewValues);
			this.OnItemUpdated(detailsViewUpdatedEventArgs);
			if (!detailsViewUpdatedEventArgs.KeepInEditMode)
			{
				this.EndRowEdit();
			}
			return detailsViewUpdatedEventArgs.ExceptionHandled;
		}

		/// <summary>Inserts the current record in the data source.</summary>
		/// <param name="causesValidation">true to perform page validation when the method is called; otherwise, false.</param>
		/// <exception cref="T:System.Web.HttpException">This method is called when the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is not in insert mode.- or -The <see cref="T:System.Web.UI.DataSourceView" /> associated with the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control is null.</exception>
		// Token: 0x06002372 RID: 9074 RVA: 0x0005C1B2 File Offset: 0x0005A3B2
		public virtual void InsertItem(bool causesValidation)
		{
			this.InsertItem(null, causesValidation);
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x0005C1BC File Offset: 0x0005A3BC
		private void InsertItem(string param, bool causesValidation)
		{
			if (causesValidation && this.Page != null && !this.Page.IsValid)
			{
				return;
			}
			if (this.CurrentMode != DetailsViewMode.Insert)
			{
				throw new HttpException();
			}
			this.currentEditNewValues = this.GetRowValues(false, true);
			DetailsViewInsertEventArgs detailsViewInsertEventArgs = new DetailsViewInsertEventArgs(param, this.currentEditNewValues);
			this.OnItemInserting(detailsViewInsertEventArgs);
			if (detailsViewInsertEventArgs.Cancel || !base.IsBoundUsingDataSourceID)
			{
				return;
			}
			DataSourceView data = this.GetData();
			if (data == null)
			{
				throw new HttpException("The DataSourceView associated to data bound control was null");
			}
			data.Insert(this.currentEditNewValues, new DataSourceViewOperationCallback(this.InsertCallback));
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x0005C250 File Offset: 0x0005A450
		private bool InsertCallback(int recordsAffected, Exception exception)
		{
			DetailsViewInsertedEventArgs detailsViewInsertedEventArgs = new DetailsViewInsertedEventArgs(recordsAffected, exception, this.currentEditNewValues);
			this.OnItemInserted(detailsViewInsertedEventArgs);
			if (!detailsViewInsertedEventArgs.KeepInInsertMode)
			{
				this.EndRowEdit();
			}
			return detailsViewInsertedEventArgs.ExceptionHandled;
		}

		/// <summary>Deletes the current record from the data source.</summary>
		// Token: 0x06002375 RID: 9077 RVA: 0x0005C288 File Offset: 0x0005A488
		public virtual void DeleteItem()
		{
			this.currentEditRowKeys = this.DataKey.Values;
			this.currentEditNewValues = this.GetRowValues(true, false);
			DetailsViewDeleteEventArgs detailsViewDeleteEventArgs = new DetailsViewDeleteEventArgs(this.PageIndex, this.currentEditRowKeys, this.currentEditNewValues);
			this.OnItemDeleting(detailsViewDeleteEventArgs);
			if (detailsViewDeleteEventArgs.Cancel || !base.IsBoundUsingDataSourceID)
			{
				return;
			}
			DataSourceView data = this.GetData();
			if (data != null)
			{
				data.Delete(this.currentEditRowKeys, this.currentEditNewValues, new DataSourceViewOperationCallback(this.DeleteCallback));
			}
			else
			{
				DetailsViewDeletedEventArgs detailsViewDeletedEventArgs = new DetailsViewDeletedEventArgs(0, null, this.currentEditRowKeys, this.currentEditNewValues);
				this.OnItemDeleted(detailsViewDeletedEventArgs);
			}
			if (this.PageIndex > 0 && this.PageIndex == this.PageCount - 1)
			{
				int num = this.PageIndex;
				this.PageIndex = num - 1;
			}
			this.RequireBinding();
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x0005C358 File Offset: 0x0005A558
		private bool DeleteCallback(int recordsAffected, Exception exception)
		{
			DetailsViewDeletedEventArgs detailsViewDeletedEventArgs = new DetailsViewDeletedEventArgs(recordsAffected, exception, this.currentEditRowKeys, this.currentEditNewValues);
			this.OnItemDeleted(detailsViewDeletedEventArgs);
			return detailsViewDeletedEventArgs.ExceptionHandled;
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x0005C386 File Offset: 0x0005A586
		private void EndRowEdit()
		{
			this.EndRowEdit(true);
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x0005C38F File Offset: 0x0005A58F
		private void EndRowEdit(bool switchToDefaultMode)
		{
			if (switchToDefaultMode)
			{
				this.ChangeMode(this.DefaultMode);
			}
			this.oldEditValues = new DataKey(new OrderedDictionary());
			this.currentEditRowKeys = null;
			this.currentEditOldValues = null;
			this.currentEditNewValues = null;
			this.RequireBinding();
		}

		/// <summary>Loads the state of the properties in the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control that need to be persisted, even when the <see cref="P:System.Web.UI.Control.EnableViewState" /> property is set to false.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</param>
		// Token: 0x06002379 RID: 9081 RVA: 0x0005C3CC File Offset: 0x0005A5CC
		protected internal override void LoadControlState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			object[] array = (object[])savedState;
			base.LoadControlState(array[0]);
			this.pageIndex = (int)array[1];
			this.pageCount = (int)array[2];
			this.CurrentMode = (DetailsViewMode)array[3];
			this.dataKeyNames = (string[])array[4];
			this.defaultMode = (DetailsViewMode)array[5];
			if (array[6] != null)
			{
				((IStateManager)this.DataKey).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				((IStateManager)this.OldEditValues).LoadViewState(array[7]);
			}
		}

		/// <summary>Saves the state of the properties in the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control that need to be persisted, even when the <see cref="P:System.Web.UI.Control.EnableViewState" /> property is set to false.</summary>
		/// <returns>Returns the server control's current view state. If there is no view state associated with the control, this method returns null.</returns>
		// Token: 0x0600237A RID: 9082 RVA: 0x0005C45C File Offset: 0x0005A65C
		protected internal override object SaveControlState()
		{
			object obj = base.SaveControlState();
			return new object[]
			{
				obj,
				this.pageIndex,
				this.pageCount,
				this.CurrentMode,
				this.dataKeyNames,
				this.defaultMode,
				(this.key == null) ? null : ((IStateManager)this.key).SaveViewState(),
				(this.oldEditValues == null) ? null : ((IStateManager)this.oldEditValues).SaveViewState()
			};
		}

		/// <summary>Marks the starting point to begin tracking and saving view-state changes to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		// Token: 0x0600237B RID: 9083 RVA: 0x0005C4F0 File Offset: 0x0005A6F0
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.columns != null)
			{
				((IStateManager)this.columns).TrackViewState();
			}
			if (this.pagerSettings != null)
			{
				((IStateManager)this.pagerSettings).TrackViewState();
			}
			if (this.alternatingRowStyle != null)
			{
				((IStateManager)this.alternatingRowStyle).TrackViewState();
			}
			if (this.footerStyle != null)
			{
				((IStateManager)this.footerStyle).TrackViewState();
			}
			if (this.headerStyle != null)
			{
				((IStateManager)this.headerStyle).TrackViewState();
			}
			if (this.pagerStyle != null)
			{
				((IStateManager)this.pagerStyle).TrackViewState();
			}
			if (this.rowStyle != null)
			{
				((IStateManager)this.rowStyle).TrackViewState();
			}
			if (this.editRowStyle != null)
			{
				((IStateManager)this.editRowStyle).TrackViewState();
			}
			if (this.insertRowStyle != null)
			{
				((IStateManager)this.insertRowStyle).TrackViewState();
			}
			if (this.emptyDataRowStyle != null)
			{
				((IStateManager)this.emptyDataRowStyle).TrackViewState();
			}
			if (this.key != null)
			{
				((IStateManager)this.key).TrackViewState();
			}
			if (this.autoFieldProperties != null)
			{
				AutoGeneratedFieldProperties[] array = this.autoFieldProperties;
				for (int i = 0; i < array.Length; i++)
				{
					((IStateManager)array[i]).TrackViewState();
				}
			}
			if (base.ControlStyleCreated)
			{
				base.ControlStyle.TrackViewState();
			}
		}

		/// <summary>Saves the current view state of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the saved state of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x0600237C RID: 9084 RVA: 0x0005C60C File Offset: 0x0005A80C
		protected override object SaveViewState()
		{
			object[] array = new object[13];
			array[0] = base.SaveViewState();
			array[1] = ((this.columns == null) ? null : ((IStateManager)this.columns).SaveViewState());
			array[2] = ((this.pagerSettings == null) ? null : ((IStateManager)this.pagerSettings).SaveViewState());
			array[3] = ((this.alternatingRowStyle == null) ? null : ((IStateManager)this.alternatingRowStyle).SaveViewState());
			array[4] = ((this.footerStyle == null) ? null : ((IStateManager)this.footerStyle).SaveViewState());
			array[5] = ((this.headerStyle == null) ? null : ((IStateManager)this.headerStyle).SaveViewState());
			array[6] = ((this.pagerStyle == null) ? null : ((IStateManager)this.pagerStyle).SaveViewState());
			array[7] = ((this.rowStyle == null) ? null : ((IStateManager)this.rowStyle).SaveViewState());
			array[8] = ((this.insertRowStyle == null) ? null : ((IStateManager)this.insertRowStyle).SaveViewState());
			array[9] = ((this.editRowStyle == null) ? null : ((IStateManager)this.editRowStyle).SaveViewState());
			array[10] = ((this.emptyDataRowStyle == null) ? null : ((IStateManager)this.emptyDataRowStyle).SaveViewState());
			if (this.autoFieldProperties != null)
			{
				object[] array2 = new object[this.autoFieldProperties.Length];
				bool flag = true;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = ((IStateManager)this.autoFieldProperties[i]).SaveViewState();
					if (array2[i] != null)
					{
						flag = false;
					}
				}
				if (!flag)
				{
					array[11] = array2;
				}
			}
			if (base.ControlStyleCreated)
			{
				array[12] = base.ControlStyle.SaveViewState();
			}
			for (int j = array.Length - 1; j >= 0; j--)
			{
				if (array[j] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</param>
		// Token: 0x0600237D RID: 9085 RVA: 0x0005C7A0 File Offset: 0x0005A9A0
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			if (array[11] != null)
			{
				object[] array2 = (object[])array[11];
				this.autoFieldProperties = new AutoGeneratedFieldProperties[array2.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					IStateManager stateManager = new AutoGeneratedFieldProperties();
					stateManager.TrackViewState();
					stateManager.LoadViewState(array2[i]);
					this.autoFieldProperties[i] = (AutoGeneratedFieldProperties)stateManager;
				}
			}
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.Fields).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.PagerSettings).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.AlternatingRowStyle).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.FooterStyle).LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				((IStateManager)this.HeaderStyle).LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				((IStateManager)this.PagerStyle).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				((IStateManager)this.RowStyle).LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				((IStateManager)this.InsertRowStyle).LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				((IStateManager)this.EditRowStyle).LoadViewState(array[9]);
			}
			if (array[10] != null)
			{
				((IStateManager)this.EmptyDataRowStyle).LoadViewState(array[10]);
			}
			if (array[12] != null)
			{
				base.ControlStyle.LoadViewState(array[12]);
			}
		}

		/// <summary>Raises the callback event using the specified arguments.</summary>
		/// <param name="eventArgument">The event arguments.</param>
		// Token: 0x0600237E RID: 9086 RVA: 0x0005C8EE File Offset: 0x0005AAEE
		void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
		{
			this.RaiseCallbackEvent(eventArgument);
		}

		/// <summary>Creates the arguments for the callback handler in the <see cref="Overload:System.Web.UI.ClientScriptManager.GetCallbackEventReference" /> method.</summary>
		/// <param name="eventArgument">The argument to pass to the event handler.</param>
		// Token: 0x0600237F RID: 9087 RVA: 0x0005C8F8 File Offset: 0x0005AAF8
		protected virtual void RaiseCallbackEvent(string eventArgument)
		{
			string[] array = eventArgument.Split(new char[] { '|' });
			this.PageIndex = int.Parse(array[0]);
			this.RaisePostBackEvent(array[1]);
			this.DataBind();
		}

		/// <summary>See the method <see cref="M:System.Web.UI.WebControls.DetailsView.GetCallbackResult" />.</summary>
		/// <returns>The results of the callback.</returns>
		// Token: 0x06002380 RID: 9088 RVA: 0x0005C934 File Offset: 0x0005AB34
		string ICallbackEventHandler.GetCallbackResult()
		{
			return this.GetCallbackResult();
		}

		/// <summary>Returns the result of a callback event that targets a control.</summary>
		/// <returns>The results of the callback.</returns>
		// Token: 0x06002381 RID: 9089 RVA: 0x0005C93C File Offset: 0x0005AB3C
		protected virtual string GetCallbackResult()
		{
			this.PrepareControlHierarchy();
			StringWriter stringWriter = new StringWriter();
			stringWriter.Write(this.PageIndex.ToString() + "|");
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
			this.RenderGrid(htmlTextWriter);
			return stringWriter.ToString();
		}

		/// <summary>Returns the callback string created using the specified argument.</summary>
		/// <returns>The complete callback string to be sent to the client.</returns>
		/// <param name="buttonControl">The control that initiated the callback.</param>
		/// <param name="argument">The callback code.</param>
		// Token: 0x06002382 RID: 9090 RVA: 0x0005C988 File Offset: 0x0005AB88
		protected virtual string GetCallbackScript(IButtonControl buttonControl, string argument)
		{
			if (this.EnablePagingCallbacks)
			{
				Page page = this.Page;
				if (page != null)
				{
					page.ClientScript.RegisterForEventValidation(this.UniqueID, argument);
				}
				return string.Concat(new string[] { "javascript:DetailsView_ClientEvent (\"", this.ClientID, "\",\"", buttonControl.CommandName, "$", buttonControl.CommandArgument, "\"); return false;" });
			}
			return null;
		}

		/// <summary>Creates the callback script for the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>The callback script for the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		/// <param name="buttonControl">The button control that posted the page back to the server.</param>
		/// <param name="argument">The argument for the callback event.</param>
		// Token: 0x06002383 RID: 9091 RVA: 0x0005CA01 File Offset: 0x0005AC01
		string ICallbackContainer.GetCallbackScript(IButtonControl control, string argument)
		{
			return this.GetCallbackScript(control, argument);
		}

		/// <summary>Sets the initialized state of the data-bound control before the control is loaded.</summary>
		/// <param name="sender">The <see cref="T:System.Web.UI.Page" /> that raised the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002384 RID: 9092 RVA: 0x0005CA0C File Offset: 0x0005AC0C
		protected override void OnPagePreLoad(object sender, EventArgs e)
		{
			base.OnPagePreLoad(sender, e);
			int num;
			if (this.Page.IsPostBack && this.EnablePagingCallbacks && int.TryParse(this.Page.Request.Form[this.ClientID + "_Page"], out num))
			{
				this.PageIndex = num;
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002385 RID: 9093 RVA: 0x0005CA6C File Offset: 0x0005AC6C
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Page page = this.Page;
			if (this.EnablePagingCallbacks && page != null)
			{
				ClientScriptManager clientScript = page.ClientScript;
				if (!clientScript.IsClientScriptIncludeRegistered(typeof(DetailsView), "DetailsView.js"))
				{
					string webResourceUrl = clientScript.GetWebResourceUrl(typeof(DetailsView), "DetailsView.js");
					clientScript.RegisterClientScriptInclude(typeof(DetailsView), "DetailsView.js", webResourceUrl);
				}
				clientScript.RegisterHiddenField(this.ClientID + "_Page", this.PageIndex.ToString());
				string text = this.ClientID + "_data";
				string text2 = string.Format("var {0} = new Object ();\n{0}.pageIndex = {1};\n{0}.uid = {2};\n{0}.form = {3};\n", new object[]
				{
					text,
					ClientScriptManager.GetScriptLiteral(this.PageIndex),
					ClientScriptManager.GetScriptLiteral(this.UniqueID),
					page.theForm
				});
				clientScript.RegisterStartupScript(typeof(TreeView), this.UniqueID, text2, true);
				clientScript.GetCallbackEventReference(this, "null", string.Empty, "null");
				clientScript.GetPostBackClientHyperlink(this, string.Empty, true);
			}
		}

		/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control on the client using the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		// Token: 0x06002386 RID: 9094 RVA: 0x0005CB96 File Offset: 0x0005AD96
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.PrepareControlHierarchy();
			if (this.EnablePagingCallbacks)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_div");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderGrid(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x0005CBD3 File Offset: 0x0005ADD3
		private void RenderGrid(HtmlTextWriter writer)
		{
			if (this.table == null)
			{
				return;
			}
			this.table.Render(writer);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.WebControls.IPostBackContainer.GetPostBackOptions(System.Web.UI.WebControls.IButtonControl)" />.</summary>
		/// <returns>A <see cref="T:System.Web.UI.PostBackOptions" /> with the information required for <see cref="Overload:System.Web.UI.Page.GetPostBackEventReference" /> to construct a valid script that, when executed on the client, initiates a client postback. </returns>
		/// <param name="buttonControl">The control generating the client-side postback event.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buttonControl" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="buttonControl" /> causes validation in the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control and attempts to use the same <see cref="T:System.Web.UI.WebControls.DetailsView" /> control as a postback target.</exception>
		// Token: 0x06002388 RID: 9096 RVA: 0x0005CBEC File Offset: 0x0005ADEC
		PostBackOptions IPostBackContainer.GetPostBackOptions(IButtonControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.CausesValidation)
			{
				throw new InvalidOperationException("A button that causes validation in DetailsView '" + this.ID + "' is attempting to use the container GridView as the post back target.  The button should either turn off validation or use itself as the post back container.");
			}
			return new PostBackOptions(this)
			{
				Argument = control.CommandName + "$" + control.CommandArgument,
				RequiresJavaScriptProtocol = true
			};
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x0005CC54 File Offset: 0x0005AE54
		// Note: this type is marked as 'beforefieldinit'.
		static DetailsView()
		{
			DetailsView.PageIndexChangedEvent = new object();
			DetailsView.PageIndexChangingEvent = new object();
			DetailsView.ItemCommandEvent = new object();
			DetailsView.ItemCreatedEvent = new object();
			DetailsView.ItemDeletedEvent = new object();
			DetailsView.ItemDeletingEvent = new object();
			DetailsView.ItemInsertedEvent = new object();
			DetailsView.ItemInsertingEvent = new object();
			DetailsView.ModeChangingEvent = new object();
			DetailsView.ModeChangedEvent = new object();
			DetailsView.ItemUpdatedEvent = new object();
			DetailsView.ItemUpdatingEvent = new object();
		}

		/// <summary>Gets or sets the name of the method on the page that is called when the control performs a delete operation.</summary>
		/// <returns>The name of the method on the page that is called when the control performs a delete operation.</returns>
		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x0600238A RID: 9098 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x0600238B RID: 9099 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public new virtual string DeleteMethod
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the name of the method on the page that is called when the control performs an insert operation.</summary>
		/// <returns>The name of the method on the page that is called when the control performs an insert operation.</returns>
		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x0600238D RID: 9101 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public new virtual string InsertMethod
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x0000E80B File Offset: 0x0000CA0B
		string IDataBoundControl.get_DataMember()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void IDataBoundControl.set_DataMember(string value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x0000E80B File Offset: 0x0000CA0B
		object IDataBoundControl.get_DataSource()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void IDataBoundControl.set_DataSource(object value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x0000E80B File Offset: 0x0000CA0B
		string IDataBoundControl.get_DataSourceID()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x0000B3E4 File Offset: 0x000095E4
		void IDataBoundControl.set_DataSourceID(string value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the name of the method on the page that is called when the control performs an update operation.</summary>
		/// <returns>The name of the method on the page that is called when the control performs an update operation.</returns>
		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06002394 RID: 9108 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06002395 RID: 9109 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public new virtual string UpdateMethod
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04001940 RID: 6464
		private object dataItem;

		// Token: 0x04001941 RID: 6465
		private Table table;

		// Token: 0x04001942 RID: 6466
		private DetailsViewRowCollection rows;

		// Token: 0x04001943 RID: 6467
		private DetailsViewRow headerRow;

		// Token: 0x04001944 RID: 6468
		private DetailsViewRow footerRow;

		// Token: 0x04001945 RID: 6469
		private DetailsViewRow bottomPagerRow;

		// Token: 0x04001946 RID: 6470
		private DetailsViewRow topPagerRow;

		// Token: 0x04001947 RID: 6471
		private IOrderedDictionary currentEditRowKeys;

		// Token: 0x04001948 RID: 6472
		private IOrderedDictionary currentEditNewValues;

		// Token: 0x04001949 RID: 6473
		private IOrderedDictionary currentEditOldValues;

		// Token: 0x0400194A RID: 6474
		private ITemplate pagerTemplate;

		// Token: 0x0400194B RID: 6475
		private ITemplate emptyDataTemplate;

		// Token: 0x0400194C RID: 6476
		private ITemplate headerTemplate;

		// Token: 0x0400194D RID: 6477
		private ITemplate footerTemplate;

		// Token: 0x0400194E RID: 6478
		private PropertyDescriptor[] cachedKeyProperties;

		// Token: 0x0400194F RID: 6479
		private readonly string[] emptyKeys = new string[0];

		// Token: 0x04001950 RID: 6480
		private readonly string unhandledEventExceptionMessage = "The DetailsView '{0}' fired event {1} which wasn't handled.";

		// Token: 0x04001951 RID: 6481
		private DataControlFieldCollection columns;

		// Token: 0x04001952 RID: 6482
		private PagerSettings pagerSettings;

		// Token: 0x04001953 RID: 6483
		private TableItemStyle alternatingRowStyle;

		// Token: 0x04001954 RID: 6484
		private TableItemStyle editRowStyle;

		// Token: 0x04001955 RID: 6485
		private TableItemStyle insertRowStyle;

		// Token: 0x04001956 RID: 6486
		private TableItemStyle emptyDataRowStyle;

		// Token: 0x04001957 RID: 6487
		private TableItemStyle footerStyle;

		// Token: 0x04001958 RID: 6488
		private TableItemStyle headerStyle;

		// Token: 0x04001959 RID: 6489
		private TableItemStyle pagerStyle;

		// Token: 0x0400195A RID: 6490
		private TableItemStyle rowStyle;

		// Token: 0x0400195B RID: 6491
		private TableItemStyle commandRowStyle;

		// Token: 0x0400195C RID: 6492
		private TableItemStyle fieldHeaderStyle;

		// Token: 0x0400195D RID: 6493
		private IOrderedDictionary _keyTable;

		// Token: 0x0400195E RID: 6494
		private DataKey key;

		// Token: 0x0400195F RID: 6495
		private DataKey oldEditValues;

		// Token: 0x04001960 RID: 6496
		private AutoGeneratedFieldProperties[] autoFieldProperties;

		// Token: 0x0400196D RID: 6509
		private int pageIndex;

		// Token: 0x0400196E RID: 6510
		private DetailsViewMode currentMode;

		// Token: 0x0400196F RID: 6511
		private bool hasCurrentMode;

		// Token: 0x04001970 RID: 6512
		private int pageCount;

		// Token: 0x04001971 RID: 6513
		private DetailsViewMode defaultMode;

		// Token: 0x04001972 RID: 6514
		private string[] dataKeyNames;

		// Token: 0x04001975 RID: 6517
		private const string onPreRenderScript = "var {0} = new Object ();\n{0}.pageIndex = {1};\n{0}.uid = {2};\n{0}.form = {3};\n";
	}
}
