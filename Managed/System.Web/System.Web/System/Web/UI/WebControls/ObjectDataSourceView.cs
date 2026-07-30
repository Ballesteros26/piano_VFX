using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Supports the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control and provides an interface for data-bound controls to perform data operations with business and data objects.</summary>
	// Token: 0x020003DF RID: 991
	public class ObjectDataSourceView : DataSourceView, IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> is associated with. </param>
		/// <param name="name">A unique name for the data source view, within the scope of the data source control that owns it.</param>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" />.</param>
		// Token: 0x06002AF6 RID: 10998 RVA: 0x00071797 File Offset: 0x0006F997
		public ObjectDataSourceView(ObjectDataSource owner, string name, HttpContext context)
			: base(owner, name)
		{
			this.owner = owner;
			this.context = context;
		}

		/// <summary>Occurs when a <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Delete" /> operation has completed.</summary>
		// Token: 0x140000BF RID: 191
		// (add) Token: 0x06002AF7 RID: 10999 RVA: 0x000717AF File Offset: 0x0006F9AF
		// (remove) Token: 0x06002AF8 RID: 11000 RVA: 0x000717C2 File Offset: 0x0006F9C2
		public event ObjectDataSourceStatusEventHandler Deleted
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.DeletedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.DeletedEvent, value);
			}
		}

		/// <summary>Occurs before a <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Delete" /> operation.</summary>
		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x06002AF9 RID: 11001 RVA: 0x000717D5 File Offset: 0x0006F9D5
		// (remove) Token: 0x06002AFA RID: 11002 RVA: 0x000717E8 File Offset: 0x0006F9E8
		public event ObjectDataSourceMethodEventHandler Deleting
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.DeletingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.DeletingEvent, value);
			}
		}

		/// <summary>Occurs before a filter operation.</summary>
		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x06002AFB RID: 11003 RVA: 0x000717FB File Offset: 0x0006F9FB
		// (remove) Token: 0x06002AFC RID: 11004 RVA: 0x0007180E File Offset: 0x0006FA0E
		public event ObjectDataSourceFilteringEventHandler Filtering
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.FilteringEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.FilteringEvent, value);
			}
		}

		/// <summary>Occurs when an <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Insert" /> operation has completed.</summary>
		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x06002AFD RID: 11005 RVA: 0x00071821 File Offset: 0x0006FA21
		// (remove) Token: 0x06002AFE RID: 11006 RVA: 0x00071834 File Offset: 0x0006FA34
		public event ObjectDataSourceStatusEventHandler Inserted
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.InsertedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.InsertedEvent, value);
			}
		}

		/// <summary>Occurs before an <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Insert" /> operation.</summary>
		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x06002AFF RID: 11007 RVA: 0x00071847 File Offset: 0x0006FA47
		// (remove) Token: 0x06002B00 RID: 11008 RVA: 0x0007185A File Offset: 0x0006FA5A
		public event ObjectDataSourceMethodEventHandler Inserting
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.InsertingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.InsertingEvent, value);
			}
		}

		/// <summary>Occurs after the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object creates an instance of the type that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.TypeName" /> property.</summary>
		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x06002B01 RID: 11009 RVA: 0x0007186D File Offset: 0x0006FA6D
		// (remove) Token: 0x06002B02 RID: 11010 RVA: 0x00071880 File Offset: 0x0006FA80
		public event ObjectDataSourceObjectEventHandler ObjectCreated
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.ObjectCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.ObjectCreatedEvent, value);
			}
		}

		/// <summary>Occurs before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object creates an instance of the type that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.TypeName" /> property.</summary>
		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x06002B03 RID: 11011 RVA: 0x00071893 File Offset: 0x0006FA93
		// (remove) Token: 0x06002B04 RID: 11012 RVA: 0x000718A6 File Offset: 0x0006FAA6
		public event ObjectDataSourceObjectEventHandler ObjectCreating
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.ObjectCreatingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.ObjectCreatingEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object discards an instance of an object that it has created. </summary>
		// Token: 0x140000C6 RID: 198
		// (add) Token: 0x06002B05 RID: 11013 RVA: 0x000718B9 File Offset: 0x0006FAB9
		// (remove) Token: 0x06002B06 RID: 11014 RVA: 0x000718CC File Offset: 0x0006FACC
		public event ObjectDataSourceDisposingEventHandler ObjectDisposing
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.ObjectDisposingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.ObjectDisposingEvent, value);
			}
		}

		/// <summary>Occurs when a data retrieval operation has completed.</summary>
		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x06002B07 RID: 11015 RVA: 0x000718DF File Offset: 0x0006FADF
		// (remove) Token: 0x06002B08 RID: 11016 RVA: 0x000718F2 File Offset: 0x0006FAF2
		public event ObjectDataSourceStatusEventHandler Selected
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.SelectedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.SelectedEvent, value);
			}
		}

		/// <summary>Occurs before a data retrieval operation.</summary>
		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x06002B09 RID: 11017 RVA: 0x00071905 File Offset: 0x0006FB05
		// (remove) Token: 0x06002B0A RID: 11018 RVA: 0x00071918 File Offset: 0x0006FB18
		public event ObjectDataSourceSelectingEventHandler Selecting
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.SelectingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.SelectingEvent, value);
			}
		}

		/// <summary>Occurs when an <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Update" /> operation has completed.</summary>
		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x06002B0B RID: 11019 RVA: 0x0007192B File Offset: 0x0006FB2B
		// (remove) Token: 0x06002B0C RID: 11020 RVA: 0x0007193E File Offset: 0x0006FB3E
		public event ObjectDataSourceStatusEventHandler Updated
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.UpdatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.UpdatedEvent, value);
			}
		}

		/// <summary>Occurs before an <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Update" /> operation.</summary>
		// Token: 0x140000CA RID: 202
		// (add) Token: 0x06002B0D RID: 11021 RVA: 0x00071951 File Offset: 0x0006FB51
		// (remove) Token: 0x06002B0E RID: 11022 RVA: 0x00071964 File Offset: 0x0006FB64
		public event ObjectDataSourceMethodEventHandler Updating
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.UpdatingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.UpdatingEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Deleted" /> event after the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object has completed a delete operation.</summary>
		/// <param name="e">An  <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> that contains the event data.</param>
		// Token: 0x06002B0F RID: 11023 RVA: 0x00071978 File Offset: 0x0006FB78
		protected virtual void OnDeleted(ObjectDataSourceStatusEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceStatusEventHandler objectDataSourceStatusEventHandler = (ObjectDataSourceStatusEventHandler)base.Events[ObjectDataSourceView.DeletedEvent];
				if (objectDataSourceStatusEventHandler != null)
				{
					objectDataSourceStatusEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Deleting" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object attempts a delete operation.</summary>
		/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs" /> that contains the event data.</param>
		// Token: 0x06002B10 RID: 11024 RVA: 0x000719B0 File Offset: 0x0006FBB0
		protected virtual void OnDeleting(ObjectDataSourceMethodEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceMethodEventHandler objectDataSourceMethodEventHandler = (ObjectDataSourceMethodEventHandler)base.Events[ObjectDataSourceView.DeletingEvent];
				if (objectDataSourceMethodEventHandler != null)
				{
					objectDataSourceMethodEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Filtering" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object attempts a filtering operation.</summary>
		/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceFilteringEventArgs" /> that contains the event data.</param>
		// Token: 0x06002B11 RID: 11025 RVA: 0x000719E8 File Offset: 0x0006FBE8
		protected virtual void OnFiltering(ObjectDataSourceFilteringEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceFilteringEventHandler objectDataSourceFilteringEventHandler = (ObjectDataSourceFilteringEventHandler)base.Events[ObjectDataSourceView.FilteringEvent];
				if (objectDataSourceFilteringEventHandler != null)
				{
					objectDataSourceFilteringEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Inserted" /> event after the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object has completed an insert operation.</summary>
		/// <param name="e">An  <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> that contains the event data.</param>
		// Token: 0x06002B12 RID: 11026 RVA: 0x00071A20 File Offset: 0x0006FC20
		protected virtual void OnInserted(ObjectDataSourceStatusEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceStatusEventHandler objectDataSourceStatusEventHandler = (ObjectDataSourceStatusEventHandler)base.Events[ObjectDataSourceView.InsertedEvent];
				if (objectDataSourceStatusEventHandler != null)
				{
					objectDataSourceStatusEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Inserting" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object attempts an insert operation.</summary>
		/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs" /> that contains the event data.</param>
		// Token: 0x06002B13 RID: 11027 RVA: 0x00071A58 File Offset: 0x0006FC58
		protected virtual void OnInserting(ObjectDataSourceMethodEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceMethodEventHandler objectDataSourceMethodEventHandler = (ObjectDataSourceMethodEventHandler)base.Events[ObjectDataSourceView.InsertingEvent];
				if (objectDataSourceMethodEventHandler != null)
				{
					objectDataSourceMethodEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.ObjectCreated" /> event after the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> creates an instance of the object that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.TypeName" /> property. </summary>
		/// <param name="e">An  <see cref="T:System.Web.UI.WebControls.ObjectDataSourceEventArgs" /> that contains the event data.</param>
		// Token: 0x06002B14 RID: 11028 RVA: 0x00071A90 File Offset: 0x0006FC90
		protected virtual void OnObjectCreated(ObjectDataSourceEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceObjectEventHandler objectDataSourceObjectEventHandler = (ObjectDataSourceObjectEventHandler)base.Events[ObjectDataSourceView.ObjectCreatedEvent];
				if (objectDataSourceObjectEventHandler != null)
				{
					objectDataSourceObjectEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.ObjectCreating" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object creates an instance of a business object to perform a data operation.</summary>
		/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceEventArgs" /> that contains the event data.</param>
		// Token: 0x06002B15 RID: 11029 RVA: 0x00071AC8 File Offset: 0x0006FCC8
		protected virtual void OnObjectCreating(ObjectDataSourceEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceObjectEventHandler objectDataSourceObjectEventHandler = (ObjectDataSourceObjectEventHandler)base.Events[ObjectDataSourceView.ObjectCreatingEvent];
				if (objectDataSourceObjectEventHandler != null)
				{
					objectDataSourceObjectEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.ObjectDisposing" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object discards an instantiated type. </summary>
		/// <param name="e">An  <see cref="T:System.Web.UI.WebControls.ObjectDataSourceDisposingEventArgs" /> that contains the event data.</param>
		// Token: 0x06002B16 RID: 11030 RVA: 0x00071B00 File Offset: 0x0006FD00
		protected virtual void OnObjectDisposing(ObjectDataSourceDisposingEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceDisposingEventHandler objectDataSourceDisposingEventHandler = (ObjectDataSourceDisposingEventHandler)base.Events[ObjectDataSourceView.ObjectDisposingEvent];
				if (objectDataSourceDisposingEventHandler != null)
				{
					objectDataSourceDisposingEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Selected" /> event after the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object has completed a data retrieval operation.</summary>
		/// <param name="e">An  <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> that contains the event data. </param>
		// Token: 0x06002B17 RID: 11031 RVA: 0x00071B38 File Offset: 0x0006FD38
		protected virtual void OnSelected(ObjectDataSourceStatusEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceStatusEventHandler objectDataSourceStatusEventHandler = (ObjectDataSourceStatusEventHandler)base.Events[ObjectDataSourceView.SelectedEvent];
				if (objectDataSourceStatusEventHandler != null)
				{
					objectDataSourceStatusEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Selecting" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object attempts a data retrieval operation.</summary>
		/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs" /> that contains the event data.</param>
		// Token: 0x06002B18 RID: 11032 RVA: 0x00071B70 File Offset: 0x0006FD70
		protected virtual void OnSelecting(ObjectDataSourceSelectingEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceSelectingEventHandler objectDataSourceSelectingEventHandler = (ObjectDataSourceSelectingEventHandler)base.Events[ObjectDataSourceView.SelectingEvent];
				if (objectDataSourceSelectingEventHandler != null)
				{
					objectDataSourceSelectingEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Updated" /> event after the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object has completed an update operation.</summary>
		/// <param name="e">An  <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> that contains the event data.</param>
		// Token: 0x06002B19 RID: 11033 RVA: 0x00071BA8 File Offset: 0x0006FDA8
		protected virtual void OnUpdated(ObjectDataSourceStatusEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceStatusEventHandler objectDataSourceStatusEventHandler = (ObjectDataSourceStatusEventHandler)base.Events[ObjectDataSourceView.UpdatedEvent];
				if (objectDataSourceStatusEventHandler != null)
				{
					objectDataSourceStatusEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Updating" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object attempts an update operation.</summary>
		/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs" /> that contains the event data.</param>
		// Token: 0x06002B1A RID: 11034 RVA: 0x00071BE0 File Offset: 0x0006FDE0
		protected virtual void OnUpdating(ObjectDataSourceMethodEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceMethodEventHandler objectDataSourceMethodEventHandler = (ObjectDataSourceMethodEventHandler)base.Events[ObjectDataSourceView.UpdatingEvent];
				if (objectDataSourceMethodEventHandler != null)
				{
					objectDataSourceMethodEventHandler(this, e);
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control supports the delete operation.</summary>
		/// <returns>true, if the operation is supported; otherwise, false. Deletion is not supported, if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> property is an empty string ("").</returns>
		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06002B1B RID: 11035 RVA: 0x00071C16 File Offset: 0x0006FE16
		public override bool CanDelete
		{
			get
			{
				return this.DeleteMethod.Length > 0;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control supports the insert operation.</summary>
		/// <returns>true, if the operation is supported; otherwise, false. Insertion is not supported, if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> property is an empty string.</returns>
		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06002B1C RID: 11036 RVA: 0x00071C26 File Offset: 0x0006FE26
		public override bool CanInsert
		{
			get
			{
				return this.InsertMethod.Length > 0;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control supports paging through the retrieved data.</summary>
		/// <returns>true, if the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.EnablePaging" /> value is set to true; otherwise, false. </returns>
		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06002B1D RID: 11037 RVA: 0x00071C36 File Offset: 0x0006FE36
		public override bool CanPage
		{
			get
			{
				return this.EnablePaging;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control supports retrieving the total number of data rows, in addition to the set of data.</summary>
		/// <returns>true, if the operation is supported; otherwise, false.</returns>
		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06002B1E RID: 11038 RVA: 0x00071C3E File Offset: 0x0006FE3E
		public override bool CanRetrieveTotalRowCount
		{
			get
			{
				return this.SelectCountMethod.Length > 0 || !this.EnablePaging;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control supports a sorted view on the underlying data source.</summary>
		/// <returns>true.</returns>
		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06002B1F RID: 11039 RVA: 0x00008B66 File Offset: 0x00006D66
		public override bool CanSort
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control supports the update operation.</summary>
		/// <returns>true, if the operation is supported; otherwise, false. Updating is not supported if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property is an empty string ("").</returns>
		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06002B20 RID: 11040 RVA: 0x00071C59 File Offset: 0x0006FE59
		public override bool CanUpdate
		{
			get
			{
				return this.UpdateMethod.Length > 0;
			}
		}

		/// <summary>Gets or sets a value that determines how the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control performs updates and deletes when data in a row in the underlying data storage changes during the time of the operation.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.ConflictOptions" /> values. The default is the <see cref="F:System.Web.UI.ConflictOptions.OverwriteChanges" /> value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.ConflictOptions" /> values.</exception>
		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06002B21 RID: 11041 RVA: 0x00071C69 File Offset: 0x0006FE69
		// (set) Token: 0x06002B22 RID: 11042 RVA: 0x00071C71 File Offset: 0x0006FE71
		public ConflictOptions ConflictDetection
		{
			get
			{
				return this.conflictDetection;
			}
			set
			{
				if (this.ConflictDetection == value)
				{
					return;
				}
				this.conflictDetection = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating whether <see cref="T:System.Web.UI.WebControls.Parameter" /> values that are passed to an update, insert, or delete operation are automatically converted from null to the <see cref="F:System.DBNull.Value" /> value.</summary>
		/// <returns>true, if null in <see cref="T:System.Web.UI.WebControls.Parameter" /> objects passed to the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> is automatically converted to the <see cref="F:System.DBNull.Value" /> value; otherwise, false. The default is false.</returns>
		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06002B23 RID: 11043 RVA: 0x00071C8F File Offset: 0x0006FE8F
		// (set) Token: 0x06002B24 RID: 11044 RVA: 0x00071C97 File Offset: 0x0006FE97
		public bool ConvertNullToDBNull
		{
			get
			{
				return this.convertNullToDBNull;
			}
			set
			{
				this.convertNullToDBNull = value;
			}
		}

		/// <summary>Gets or sets the name of a class that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control uses for a parameter in a data operation. The <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control uses the specified class instead of the <see cref="T:System.Web.UI.WebControls.Parameter" /> objects that are in the various parameters collections.</summary>
		/// <returns>A partially or fully qualified class name that identifies the type of the object that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> can use as a parameter for a <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Insert" />, <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Update" />, or <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Delete" /> operation. The default is an empty string ("").</returns>
		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x00071CA0 File Offset: 0x0006FEA0
		// (set) Token: 0x06002B26 RID: 11046 RVA: 0x00071CB6 File Offset: 0x0006FEB6
		public string DataObjectTypeName
		{
			get
			{
				if (this.dataObjectTypeName == null)
				{
					return string.Empty;
				}
				return this.dataObjectTypeName;
			}
			set
			{
				if (this.DataObjectTypeName == value)
				{
					return;
				}
				this.dataObjectTypeName = value;
				this.dataObjectType = null;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object invokes to delete data.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> uses to delete data. The default is an empty string ("").</returns>
		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06002B27 RID: 11047 RVA: 0x00071CE0 File Offset: 0x0006FEE0
		// (set) Token: 0x06002B28 RID: 11048 RVA: 0x00071CF6 File Offset: 0x0006FEF6
		public string DeleteMethod
		{
			get
			{
				if (this.deleteMethod == null)
				{
					return string.Empty;
				}
				return this.deleteMethod;
			}
			set
			{
				this.deleteMethod = value;
			}
		}

		/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> method.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> property.</returns>
		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06002B29 RID: 11049 RVA: 0x00071CFF File Offset: 0x0006FEFF
		public ParameterCollection DeleteParameters
		{
			get
			{
				if (this.deleteParameters == null)
				{
					this.deleteParameters = new ParameterCollection();
				}
				return this.deleteParameters;
			}
		}

		/// <summary>Gets or sets a value indicating whether the data source control supports paging through the set of data that it retrieves.</summary>
		/// <returns>true, if the data source control supports paging through the data it retrieves; otherwise, false.</returns>
		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06002B2A RID: 11050 RVA: 0x00071D1A File Offset: 0x0006FF1A
		// (set) Token: 0x06002B2B RID: 11051 RVA: 0x00071D22 File Offset: 0x0006FF22
		public bool EnablePaging
		{
			get
			{
				return this.enablePaging;
			}
			set
			{
				if (this.EnablePaging == value)
				{
					return;
				}
				this.enablePaging = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a filtering expression that is applied when the business object method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectMethod" /> property is called.</summary>
		/// <returns>A string that represents a filtering expression applied when data is retrieved using the business object method identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectMethod" /> property.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.FilterExpression" /> property was set and the <see cref="M:System.Web.UI.WebControls.ObjectDataSourceView.Select(System.Web.UI.DataSourceSelectArguments)" /> method does not return a <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06002B2C RID: 11052 RVA: 0x00071D40 File Offset: 0x0006FF40
		// (set) Token: 0x06002B2D RID: 11053 RVA: 0x00071D56 File Offset: 0x0006FF56
		public string FilterExpression
		{
			get
			{
				if (this.filterExpression == null)
				{
					return string.Empty;
				}
				return this.filterExpression;
			}
			set
			{
				if (this.FilterExpression == value)
				{
					return;
				}
				this.filterExpression = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets a collection of parameters that are associated with any parameter placeholders that are in the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.FilterExpression" /> string.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains a set of parameters associated with any parameter placeholders found in the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.FilterExpression" /> property.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.FilterExpression" /> property was set and the <see cref="M:System.Web.UI.WebControls.ObjectDataSourceView.Select(System.Web.UI.DataSourceSelectArguments)" /> method does not return a <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06002B2E RID: 11054 RVA: 0x00071D7C File Offset: 0x0006FF7C
		public ParameterCollection FilterParameters
		{
			get
			{
				if (this.filterParameters == null)
				{
					this.filterParameters = new ParameterCollection();
					this.filterParameters.ParametersChanged += this.OnParametersChanged;
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.filterParameters).TrackViewState();
					}
				}
				return this.filterParameters;
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object invokes to insert data.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> uses to insert data. The default value is an empty string ("").</returns>
		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x06002B2F RID: 11055 RVA: 0x00071DCC File Offset: 0x0006FFCC
		// (set) Token: 0x06002B30 RID: 11056 RVA: 0x00071DE2 File Offset: 0x0006FFE2
		public string InsertMethod
		{
			get
			{
				if (this.insertMethod == null)
				{
					return string.Empty;
				}
				return this.insertMethod;
			}
			set
			{
				this.insertMethod = value;
			}
		}

		/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> method.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> property.</returns>
		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06002B31 RID: 11057 RVA: 0x00071DEB File Offset: 0x0006FFEB
		public ParameterCollection InsertParameters
		{
			get
			{
				if (this.insertParameters == null)
				{
					this.insertParameters = new ParameterCollection();
				}
				return this.insertParameters;
			}
		}

		/// <summary>Gets or sets the name of the data retrieval method parameter that is used to indicate the number of records to retrieve for data source paging support.</summary>
		/// <returns>The name of the method parameter used to indicate the number of records to retrieve. The default is "maximumRows".</returns>
		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06002B32 RID: 11058 RVA: 0x00071E06 File Offset: 0x00070006
		// (set) Token: 0x06002B33 RID: 11059 RVA: 0x00071E1C File Offset: 0x0007001C
		public string MaximumRowsParameterName
		{
			get
			{
				if (this.maximumRowsParameterName == null)
				{
					return "maximumRows";
				}
				return this.maximumRowsParameterName;
			}
			set
			{
				if (this.MaximumRowsParameterName == value)
				{
					return;
				}
				this.maximumRowsParameterName = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a format string to apply to the names of the parameters for original values that are passed to the Delete or Update methods.</summary>
		/// <returns>A string that represents a format string applied to the names of any <paramref name="oldValues" /> passed to the <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Delete" /> or <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Update" /> method. The default is "{0}", which means the parameter name is simply the field name.</returns>
		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x06002B34 RID: 11060 RVA: 0x00071E3F File Offset: 0x0007003F
		// (set) Token: 0x06002B35 RID: 11061 RVA: 0x00071E55 File Offset: 0x00070055
		[DefaultValue("{0}")]
		public string OldValuesParameterFormatString
		{
			get
			{
				if (this.oldValuesParameterFormatString == null)
				{
					return "{0}";
				}
				return this.oldValuesParameterFormatString;
			}
			set
			{
				if (this.OldValuesParameterFormatString == value)
				{
					return;
				}
				this.oldValuesParameterFormatString = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> control invokes to retrieve a row count.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> uses to retrieve a row count. The default is an empty string (""). </returns>
		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x00071E78 File Offset: 0x00070078
		// (set) Token: 0x06002B37 RID: 11063 RVA: 0x00071E8E File Offset: 0x0007008E
		public string SelectCountMethod
		{
			get
			{
				if (this.selectCountMethod == null)
				{
					return string.Empty;
				}
				return this.selectCountMethod;
			}
			set
			{
				if (this.SelectCountMethod == value)
				{
					return;
				}
				this.selectCountMethod = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> control invokes to retrieve data.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> uses to retrieve data. The default is an empty string ("").</returns>
		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06002B38 RID: 11064 RVA: 0x00071EB1 File Offset: 0x000700B1
		// (set) Token: 0x06002B39 RID: 11065 RVA: 0x00071EC7 File Offset: 0x000700C7
		public string SelectMethod
		{
			get
			{
				if (this.selectMethod == null)
				{
					return string.Empty;
				}
				return this.selectMethod;
			}
			set
			{
				if (this.SelectMethod == value)
				{
					return;
				}
				this.selectMethod = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets the parameters collection containing the parameters that are used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectMethod" /> method.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property.</returns>
		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06002B3A RID: 11066 RVA: 0x00071EEC File Offset: 0x000700EC
		public ParameterCollection SelectParameters
		{
			get
			{
				if (this.selectParameters == null)
				{
					this.selectParameters = new ParameterCollection();
					this.selectParameters.ParametersChanged += this.OnParametersChanged;
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.selectParameters).TrackViewState();
					}
				}
				return this.selectParameters;
			}
		}

		/// <summary>Gets or sets the name of the data retrieval method parameter that is used to specify a sort expression for data source sorting support.</summary>
		/// <returns>The name of the method parameter used to indicate the parameter that accepts this sort expression value. The default is an empty string ("").</returns>
		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x00071F3C File Offset: 0x0007013C
		// (set) Token: 0x06002B3C RID: 11068 RVA: 0x00071F52 File Offset: 0x00070152
		public string SortParameterName
		{
			get
			{
				if (this.sortParameterName == null)
				{
					return string.Empty;
				}
				return this.sortParameterName;
			}
			set
			{
				this.sortParameterName = value;
			}
		}

		/// <summary>Gets or sets the name of the data retrieval method parameter that is used to indicate the integer index of the first record to retrieve from the results set for data source paging support.</summary>
		/// <returns>The name of the business object method parameter used to indicate the first record to retrieve. The default is "startRowIndex".</returns>
		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06002B3D RID: 11069 RVA: 0x00071F5B File Offset: 0x0007015B
		// (set) Token: 0x06002B3E RID: 11070 RVA: 0x00071F71 File Offset: 0x00070171
		public string StartRowIndexParameterName
		{
			get
			{
				if (this.startRowIndexParameterName == null)
				{
					return "startRowIndex";
				}
				return this.startRowIndexParameterName;
			}
			set
			{
				if (this.StartRowIndexParameterName == value)
				{
					return;
				}
				this.startRowIndexParameterName = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the name of the class that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control represents.</summary>
		/// <returns>A partially or fully qualified class name that identifies the type of the object that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> represents. The default is an empty string.</returns>
		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06002B3F RID: 11071 RVA: 0x00071F94 File Offset: 0x00070194
		// (set) Token: 0x06002B40 RID: 11072 RVA: 0x00071FAA File Offset: 0x000701AA
		public string TypeName
		{
			get
			{
				if (this.typeName == null)
				{
					return string.Empty;
				}
				return this.typeName;
			}
			set
			{
				if (this.TypeName == value)
				{
					return;
				}
				this.typeName = value;
				this.objectType = null;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object invokes to update data.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> uses to update data. The default is an empty string ("").</returns>
		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06002B41 RID: 11073 RVA: 0x00071FD4 File Offset: 0x000701D4
		// (set) Token: 0x06002B42 RID: 11074 RVA: 0x00071FEA File Offset: 0x000701EA
		public string UpdateMethod
		{
			get
			{
				if (this.updateMethod == null)
				{
					return string.Empty;
				}
				return this.updateMethod;
			}
			set
			{
				this.updateMethod = value;
			}
		}

		/// <summary>Gets the parameters collection containing the parameters that are used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> method.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property.</returns>
		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06002B43 RID: 11075 RVA: 0x00071FF3 File Offset: 0x000701F3
		public ParameterCollection UpdateParameters
		{
			get
			{
				if (this.updateParameters == null)
				{
					this.updateParameters = new ParameterCollection();
				}
				return this.updateParameters;
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06002B44 RID: 11076 RVA: 0x00072010 File Offset: 0x00070210
		private Type ObjectType
		{
			get
			{
				if (this.objectType == null)
				{
					this.objectType = HttpApplication.LoadType(this.TypeName);
					if (this.objectType == null)
					{
						throw new InvalidOperationException("Type not found: " + this.TypeName);
					}
				}
				return this.objectType;
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x06002B45 RID: 11077 RVA: 0x00072068 File Offset: 0x00070268
		private Type DataObjectType
		{
			get
			{
				if (this.dataObjectType == null)
				{
					this.dataObjectType = HttpApplication.LoadType(this.DataObjectTypeName);
					if (this.dataObjectType == null)
					{
						throw new InvalidOperationException("Type not found: " + this.DataObjectTypeName);
					}
				}
				return this.dataObjectType;
			}
		}

		/// <summary>Retrieves data from the object that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.TypeName" /> property by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectMethod" /> property and passing any values in the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectParameters" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of data rows. For more information, see <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectMethod" />.</returns>
		/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> used to request operations on the data beyond basic data retrieval.</param>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="arguments" /> passed to the <see cref="M:System.Web.UI.WebControls.ObjectDataSourceView.Select(System.Web.UI.DataSourceSelectArguments)" /> method specify that the data source should perform some additional work while retrieving data to enable paging or sorting through the retrieved data, but the data source control does not support the requested capability.</exception>
		// Token: 0x06002B46 RID: 11078 RVA: 0x000720BE File Offset: 0x000702BE
		public IEnumerable Select(DataSourceSelectArguments arguments)
		{
			return this.ExecuteSelect(arguments);
		}

		/// <summary>Performs an update operation by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property and using any parameters that are supplied in the <paramref name="keys" />, <paramref name="values" />, or <paramref name="oldValues" /> collections.</summary>
		/// <returns>The number of rows updated; otherwise, -1, if the number is not known.</returns>
		/// <param name="keys">A <see cref="T:System.Collections.IDictionary" /> of the key values used to identify the item to update. These parameters are used with the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property to perform the update operation. If there are no parameters associated with the method, pass null.</param>
		/// <param name="values">A <see cref="T:System.Collections.IDictionary" /> of new values to apply to the data source. These parameters are used with the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property to perform the update database operation. If there are no parameters associated with the method, pass null. </param>
		/// <param name="oldValues">A <see cref="T:System.Collections.IDictionary" /> that contains the additional non-key values used to match the item in the data source. Row values are passed to the delete method, only if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.ConflictDetection" /> property is set to the <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> field.</param>
		// Token: 0x06002B47 RID: 11079 RVA: 0x000720C7 File Offset: 0x000702C7
		public int Update(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			return this.ExecuteUpdate(keys, values, oldValues);
		}

		/// <summary>Performs a delete operation by calling the business object method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> property using the specified <paramref name="keys" /> and <paramref name="oldValues" /> collections.</summary>
		/// <returns>The number of rows deleted; otherwise, -1, if the number is not known.</returns>
		/// <param name="keys">A <see cref="T:System.Collections.IDictionary" /> of the key values used to identify the item to delete. These parameters are used with the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> property to perform the delete operation. If there are no parameters associated with the method, pass null.</param>
		/// <param name="oldValues">A <see cref="T:System.Collections.IDictionary" /> that contains the additional non-key values used to match the item in the data source. Row values are passed to the method only if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.ConflictDetection" /> property is set to the <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> field.</param>
		// Token: 0x06002B48 RID: 11080 RVA: 0x000720D2 File Offset: 0x000702D2
		public int Delete(IDictionary keys, IDictionary oldValues)
		{
			return this.ExecuteDelete(keys, oldValues);
		}

		/// <summary>Performs an insert operation by calling the business object method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> property using the specified <paramref name="values" /> collection.</summary>
		/// <returns>The number of rows inserted; otherwise, -1, if the number is not known.</returns>
		/// <param name="values">A <see cref="T:System.Collections.IDictionary" /> collection of parameters used with the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> property to perform the insert operation. If there are no parameters associated with the method, pass null.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.CanInsert" /> property returns false.</exception>
		// Token: 0x06002B49 RID: 11081 RVA: 0x000720DC File Offset: 0x000702DC
		public int Insert(IDictionary values)
		{
			return this.ExecuteInsert(values);
		}

		/// <summary>Performs an insert operation by calling the business object method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> property using the specified <paramref name="values" /> collection.</summary>
		/// <returns>The number of rows inserted; otherwise, -1, if the number is not known. For more information, see <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Insert" />.</returns>
		/// <param name="values">A <see cref="T:System.Collections.IDictionary" /> of parameters used with the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> property to perform the insert operation. If there are no parameters associated with the method, pass null.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.CanInsert" /> property returns false.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="values" /> is null or empty.</exception>
		// Token: 0x06002B4A RID: 11082 RVA: 0x000720E8 File Offset: 0x000702E8
		protected override int ExecuteInsert(IDictionary values)
		{
			if (!this.CanInsert)
			{
				throw new NotSupportedException("Insert operation not supported.");
			}
			IOrderedDictionary orderedDictionary;
			MethodInfo methodInfo;
			if (this.DataObjectTypeName.Length == 0)
			{
				orderedDictionary = this.MergeParameterValues(this.InsertParameters, values, null);
				methodInfo = this.GetObjectMethod(this.InsertMethod, orderedDictionary, DataObjectMethodType.Insert);
			}
			else
			{
				methodInfo = this.ResolveDataObjectMethod(this.InsertMethod, values, null, out orderedDictionary);
			}
			ObjectDataSourceMethodEventArgs objectDataSourceMethodEventArgs = new ObjectDataSourceMethodEventArgs(orderedDictionary);
			this.OnInserting(objectDataSourceMethodEventArgs);
			if (objectDataSourceMethodEventArgs.Cancel)
			{
				return -1;
			}
			ObjectDataSourceStatusEventArgs objectDataSourceStatusEventArgs = this.InvokeMethod(methodInfo, orderedDictionary);
			this.OnInserted(objectDataSourceStatusEventArgs);
			if (objectDataSourceStatusEventArgs.Exception != null && !objectDataSourceStatusEventArgs.ExceptionHandled)
			{
				throw objectDataSourceStatusEventArgs.Exception;
			}
			if (this.owner.EnableCaching)
			{
				this.owner.Cache.Expire();
			}
			this.OnDataSourceViewChanged(EventArgs.Empty);
			return -1;
		}

		/// <summary>Performs a delete operation using the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> method and the specified <paramref name="keys" /> and <paramref name="oldValues" /> collection.</summary>
		/// <returns>The number of rows deleted; otherwise, -1, if the number is not known. For more information, see <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Delete" />.</returns>
		/// <param name="keys">A <see cref="T:System.Collections.IDictionary" /> of parameters used with the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> property to perform the delete operation. If there are no parameters associated with the method, pass null.</param>
		/// <param name="oldValues">A <see cref="T:System.Collections.IDictionary" /> that contains row values that are evaluated, only if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.ConflictDetection" /> property is set to the  <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> field.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.CanDelete" /> property returns false.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.ConflictDetection" /> property is set to the <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> value, and no values are passed in the <paramref name="oldValues" /> collection.</exception>
		// Token: 0x06002B4B RID: 11083 RVA: 0x000721B0 File Offset: 0x000703B0
		protected override int ExecuteDelete(IDictionary keys, IDictionary oldValues)
		{
			if (!this.CanDelete)
			{
				throw new NotSupportedException("Delete operation not supported.");
			}
			if (this.ConflictDetection == ConflictOptions.CompareAllValues && (oldValues == null || oldValues.Count == 0))
			{
				throw new InvalidOperationException("ConflictDetection is set to CompareAllValues and oldValues collection is null or empty.");
			}
			IDictionary dictionary = this.BuildOldValuesList(keys, oldValues, false);
			IOrderedDictionary orderedDictionary;
			MethodInfo methodInfo;
			if (this.DataObjectTypeName.Length == 0)
			{
				orderedDictionary = this.MergeParameterValues(this.DeleteParameters, null, dictionary);
				methodInfo = this.GetObjectMethod(this.DeleteMethod, orderedDictionary, DataObjectMethodType.Delete);
			}
			else
			{
				methodInfo = this.ResolveDataObjectMethod(this.DeleteMethod, dictionary, null, out orderedDictionary);
			}
			ObjectDataSourceMethodEventArgs objectDataSourceMethodEventArgs = new ObjectDataSourceMethodEventArgs(orderedDictionary);
			this.OnDeleting(objectDataSourceMethodEventArgs);
			if (objectDataSourceMethodEventArgs.Cancel)
			{
				return -1;
			}
			ObjectDataSourceStatusEventArgs objectDataSourceStatusEventArgs = this.InvokeMethod(methodInfo, orderedDictionary);
			this.OnDeleted(objectDataSourceStatusEventArgs);
			if (objectDataSourceStatusEventArgs.Exception != null && !objectDataSourceStatusEventArgs.ExceptionHandled)
			{
				throw objectDataSourceStatusEventArgs.Exception;
			}
			if (this.owner.EnableCaching)
			{
				this.owner.Cache.Expire();
			}
			this.OnDataSourceViewChanged(EventArgs.Empty);
			return -1;
		}

		/// <summary>Performs an update operation by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property and using any parameters that are supplied in the <paramref name="keys" />, <paramref name="values" />, or <paramref name="oldValues" /> collections.</summary>
		/// <returns>The number of rows updated; or -1, if the number is not known. For more information, see <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Update" />.</returns>
		/// <param name="keys">A <see cref="T:System.Collections.IDictionary" /> of primary keys to use with the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property to perform the update database operation. If there are no keys associated with the method, pass null.</param>
		/// <param name="values">A <see cref="T:System.Collections.IDictionary" /> of values to be used with the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> to perform the update database operation. If there are no parameters associated with the method, pass null. </param>
		/// <param name="oldValues">A <see cref="T:System.Collections.IDictionary" /> that represents the original values in the underlying data store. If there are no parameters associated with the query, pass null.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.CanInsert" /> property returns false.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="oldValues" /> is null or empty and <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.ConflictDetection" /> is set to <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" />.</exception>
		// Token: 0x06002B4C RID: 11084 RVA: 0x000722A4 File Offset: 0x000704A4
		protected override int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			IDictionary dictionary = this.BuildOldValuesList(keys, oldValues, true);
			IOrderedDictionary orderedDictionary;
			MethodInfo methodInfo;
			if (this.DataObjectTypeName.Length == 0)
			{
				orderedDictionary = this.MergeParameterValues(this.UpdateParameters, values, dictionary);
				methodInfo = this.GetObjectMethod(this.UpdateMethod, orderedDictionary, DataObjectMethodType.Update);
			}
			else
			{
				if (this.ConflictDetection != ConflictOptions.CompareAllValues)
				{
					dictionary = null;
				}
				IDictionary dictionary2 = new Hashtable();
				if (keys != null)
				{
					foreach (object obj in keys)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						dictionary2[dictionaryEntry.Key] = dictionaryEntry.Value;
					}
				}
				if (values != null)
				{
					foreach (object obj2 in values)
					{
						DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
						dictionary2[dictionaryEntry2.Key] = dictionaryEntry2.Value;
					}
				}
				methodInfo = this.ResolveDataObjectMethod(this.UpdateMethod, dictionary2, dictionary, out orderedDictionary);
			}
			ObjectDataSourceMethodEventArgs objectDataSourceMethodEventArgs = new ObjectDataSourceMethodEventArgs(orderedDictionary);
			this.OnUpdating(objectDataSourceMethodEventArgs);
			if (objectDataSourceMethodEventArgs.Cancel)
			{
				return -1;
			}
			ObjectDataSourceStatusEventArgs objectDataSourceStatusEventArgs = this.InvokeMethod(methodInfo, orderedDictionary);
			this.OnUpdated(objectDataSourceStatusEventArgs);
			if (objectDataSourceStatusEventArgs.Exception != null && !objectDataSourceStatusEventArgs.ExceptionHandled)
			{
				throw objectDataSourceStatusEventArgs.Exception;
			}
			if (this.owner.EnableCaching)
			{
				this.owner.Cache.Expire();
			}
			this.OnDataSourceViewChanged(EventArgs.Empty);
			return -1;
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x0007243C File Offset: 0x0007063C
		private IDictionary BuildOldValuesList(IDictionary keys, IDictionary oldValues, bool keysWin)
		{
			IDictionary dictionary;
			if (this.ConflictDetection == ConflictOptions.CompareAllValues)
			{
				dictionary = new Hashtable();
				if (keys != null && !keysWin)
				{
					foreach (object obj in keys)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						dictionary[dictionaryEntry.Key] = dictionaryEntry.Value;
					}
				}
				if (oldValues != null)
				{
					foreach (object obj2 in oldValues)
					{
						DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
						dictionary[dictionaryEntry2.Key] = dictionaryEntry2.Value;
					}
				}
				if (keys == null || !keysWin)
				{
					return dictionary;
				}
				using (IDictionaryEnumerator dictionaryEnumerator = keys.GetEnumerator())
				{
					while (dictionaryEnumerator.MoveNext())
					{
						object obj3 = dictionaryEnumerator.Current;
						DictionaryEntry dictionaryEntry3 = (DictionaryEntry)obj3;
						dictionary[dictionaryEntry3.Key] = dictionaryEntry3.Value;
					}
					return dictionary;
				}
			}
			dictionary = keys;
			return dictionary;
		}

		/// <summary>Retrieves data from the object that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.TypeName" /> property by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectMethod" /> property and passing any values in the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectParameters" /> collection.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerable" /> list of data rows.</returns>
		/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> used to request operations on the data beyond basic data retrieval.</param>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="arguments" /> passed to the <see cref="M:System.Web.UI.WebControls.ObjectDataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method specify that the data source should perform some additional work while retrieving data to enable paging or sorting through the retrieved data, but the data source control does not support the requested capability.- or -The object returned by the <see cref="M:System.Web.UI.WebControls.ObjectDataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method is not a <see cref="T:System.Data.DataSet" /> or <see cref="T:System.Data.DataTable" />, and caching is enabled. Only <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> objects can be cached for the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> control.- or -Both caching and client impersonation are enabled. The <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> does not support caching when client impersonation is enabled.</exception>
		/// <exception cref="T:System.InvalidOperationException">The object returned by the <see cref="M:System.Web.UI.WebControls.ObjectDataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method is a <see cref="T:System.Data.DataSet" />, but has no tables in its <see cref="P:System.Data.DataSet.Tables" /> collection.- or - The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.EnablePaging" /> property is set to true, but the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.StartRowIndexParameterName" /> and <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.MaximumRowsParameterName" /> properties are not set.</exception>
		// Token: 0x06002B4E RID: 11086 RVA: 0x00072568 File Offset: 0x00070768
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			arguments.RaiseUnsupportedCapabilitiesError(this);
			IOrderedDictionary orderedDictionary = this.MergeParameterValues(this.SelectParameters, null, null);
			ObjectDataSourceSelectingEventArgs objectDataSourceSelectingEventArgs = new ObjectDataSourceSelectingEventArgs(orderedDictionary, arguments, false);
			object obj = null;
			if (this.owner.EnableCaching)
			{
				obj = this.owner.Cache.GetCachedObject(this.SelectMethod, this.SelectParameters);
			}
			if (obj == null)
			{
				this.OnSelecting(objectDataSourceSelectingEventArgs);
				if (objectDataSourceSelectingEventArgs.Cancel)
				{
					return new ArrayList();
				}
				if (this.CanPage)
				{
					if (this.StartRowIndexParameterName.Length == 0)
					{
						throw new InvalidOperationException("Paging is enabled, but the StartRowIndexParameterName property is not set.");
					}
					if (this.MaximumRowsParameterName.Length == 0)
					{
						throw new InvalidOperationException("Paging is enabled, but the MaximumRowsParameterName property is not set.");
					}
					orderedDictionary[this.StartRowIndexParameterName] = arguments.StartRowIndex;
					orderedDictionary[this.MaximumRowsParameterName] = arguments.MaximumRows;
				}
				if (this.SortParameterName.Length > 0)
				{
					orderedDictionary[this.SortParameterName] = arguments.SortExpression;
				}
				obj = this.InvokeSelect(this.SelectMethod, orderedDictionary);
				if (this.CanRetrieveTotalRowCount && arguments.RetrieveTotalRowCount)
				{
					arguments.TotalRowCount = this.QueryTotalRowCount(this.MergeParameterValues(this.SelectParameters, null, null), arguments);
				}
				if (this.owner.EnableCaching)
				{
					this.owner.Cache.SetCachedObject(this.SelectMethod, this.SelectParameters, obj);
				}
			}
			if (this.FilterExpression.Length > 0 && !(obj is DataGrid) && !(obj is DataView) && !(obj is DataTable))
			{
				throw new NotSupportedException("The FilterExpression property was set and the Select method does not return a DataSet, DataTable, or DataView.");
			}
			if (this.owner.EnableCaching && obj is IDataReader)
			{
				throw new NotSupportedException("Data source does not support caching objects that implement IDataReader");
			}
			if (obj is DataSet)
			{
				DataSet dataSet = (DataSet)obj;
				if (dataSet.Tables.Count == 0)
				{
					throw new InvalidOperationException("The select method returnet a DataSet which doesn't contain any table.");
				}
				obj = dataSet.Tables[0];
			}
			if (obj is DataTable)
			{
				DataView dataView = new DataView((DataTable)obj);
				if (arguments.SortExpression != null && arguments.SortExpression.Length > 0)
				{
					dataView.Sort = arguments.SortExpression;
				}
				if (this.FilterExpression.Length > 0)
				{
					IOrderedDictionary values = this.FilterParameters.GetValues(this.context, this.owner);
					ObjectDataSourceFilteringEventArgs objectDataSourceFilteringEventArgs = new ObjectDataSourceFilteringEventArgs(values);
					this.OnFiltering(objectDataSourceFilteringEventArgs);
					if (!objectDataSourceFilteringEventArgs.Cancel)
					{
						object[] array = new object[values.Count];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = values[i];
							if (array[i] == null)
							{
								return dataView;
							}
						}
						dataView.RowFilter = string.Format(this.FilterExpression, array);
					}
				}
				return dataView;
			}
			if (obj is IEnumerable)
			{
				return (IEnumerable)obj;
			}
			return new object[] { obj };
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x00072828 File Offset: 0x00070A28
		private int QueryTotalRowCount(IOrderedDictionary mergedParameters, DataSourceSelectArguments arguments)
		{
			ObjectDataSourceSelectingEventArgs objectDataSourceSelectingEventArgs = new ObjectDataSourceSelectingEventArgs(mergedParameters, arguments, true);
			this.OnSelecting(objectDataSourceSelectingEventArgs);
			if (objectDataSourceSelectingEventArgs.Cancel)
			{
				return 0;
			}
			return (int)Convert.ChangeType(this.InvokeSelect(this.SelectCountMethod, mergedParameters), typeof(int));
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x00072870 File Offset: 0x00070A70
		private object InvokeSelect(string methodName, IOrderedDictionary paramValues)
		{
			MethodInfo objectMethod = this.GetObjectMethod(methodName, paramValues, DataObjectMethodType.Select);
			ObjectDataSourceStatusEventArgs objectDataSourceStatusEventArgs = this.InvokeMethod(objectMethod, paramValues);
			this.OnSelected(objectDataSourceStatusEventArgs);
			if (objectDataSourceStatusEventArgs.Exception != null && !objectDataSourceStatusEventArgs.ExceptionHandled)
			{
				throw objectDataSourceStatusEventArgs.Exception;
			}
			return objectDataSourceStatusEventArgs.ReturnValue;
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000728B4 File Offset: 0x00070AB4
		private ObjectDataSourceStatusEventArgs InvokeMethod(MethodInfo method, IOrderedDictionary paramValues)
		{
			object obj = null;
			if (!method.IsStatic)
			{
				obj = this.CreateObjectInstance();
			}
			ParameterInfo[] parameters = method.GetParameters();
			ArrayList arrayList;
			object[] parameterArray = this.GetParameterArray(parameters, paramValues, out arrayList);
			if (parameterArray == null)
			{
				throw this.CreateMethodException(method.Name, paramValues);
			}
			object obj2 = null;
			Hashtable hashtable = null;
			ObjectDataSourceStatusEventArgs objectDataSourceStatusEventArgs;
			try
			{
				obj2 = method.Invoke(obj, parameterArray);
				if (arrayList != null)
				{
					hashtable = new Hashtable();
					foreach (object obj3 in arrayList)
					{
						ParameterInfo parameterInfo = (ParameterInfo)obj3;
						hashtable[parameterInfo.Name] = parameterArray[parameterInfo.Position];
					}
				}
				objectDataSourceStatusEventArgs = new ObjectDataSourceStatusEventArgs(obj2, hashtable, null);
			}
			catch (Exception ex)
			{
				objectDataSourceStatusEventArgs = new ObjectDataSourceStatusEventArgs(obj2, hashtable, ex);
			}
			finally
			{
				if (obj != null)
				{
					this.DisposeObjectInstance(obj);
				}
			}
			return objectDataSourceStatusEventArgs;
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000729B4 File Offset: 0x00070BB4
		private MethodInfo GetObjectMethod(string methodName, IOrderedDictionary parameters, DataObjectMethodType methodType)
		{
			MemberInfo[] member = this.ObjectType.GetMember(methodName, MemberTypes.Method, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			if (member.Length > 1)
			{
				DataObjectMethodAttribute dataObjectMethodAttribute = null;
				MethodInfo methodInfo = null;
				bool flag = false;
				foreach (MethodInfo methodInfo2 in member)
				{
					ParameterInfo[] parameters2 = methodInfo2.GetParameters();
					if (parameters2.Length == parameters.Count)
					{
						object[] customAttributes = methodInfo2.GetCustomAttributes(typeof(DataObjectMethodAttribute), true);
						DataObjectMethodAttribute dataObjectMethodAttribute2 = ((customAttributes != null && customAttributes.Length != 0) ? ((DataObjectMethodAttribute)customAttributes[0]) : null);
						if (dataObjectMethodAttribute2 == null || dataObjectMethodAttribute2.MethodType == methodType)
						{
							bool flag2 = true;
							foreach (ParameterInfo parameterInfo in parameters2)
							{
								if (!parameters.Contains(parameterInfo.Name))
								{
									flag2 = false;
									break;
								}
							}
							if (flag2)
							{
								if (dataObjectMethodAttribute2 != null)
								{
									if (dataObjectMethodAttribute != null)
									{
										if (dataObjectMethodAttribute.IsDefault)
										{
											if (dataObjectMethodAttribute2.IsDefault)
											{
												methodInfo = null;
												break;
											}
											goto IL_0100;
										}
										else
										{
											methodInfo = null;
											flag = !dataObjectMethodAttribute2.IsDefault;
										}
									}
									else
									{
										methodInfo = null;
									}
								}
								if (methodInfo == null)
								{
									dataObjectMethodAttribute = dataObjectMethodAttribute2;
									methodInfo = methodInfo2;
								}
								else
								{
									flag = true;
								}
							}
						}
					}
					IL_0100:;
				}
				if (!flag && methodInfo != null)
				{
					return methodInfo;
				}
			}
			else if (member.Length == 1)
			{
				MethodInfo methodInfo3 = member[0] as MethodInfo;
				if (methodInfo3 != null && methodInfo3.GetParameters().Length == parameters.Count)
				{
					return methodInfo3;
				}
			}
			throw this.CreateMethodException(methodName, parameters);
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x00072B18 File Offset: 0x00070D18
		private MethodInfo ResolveDataObjectMethod(string methodName, IDictionary values, IDictionary oldValues, out IOrderedDictionary paramValues)
		{
			MethodInfo methodInfo;
			if (oldValues != null)
			{
				methodInfo = this.ObjectType.GetMethod(methodName, new Type[] { this.DataObjectType, this.DataObjectType });
			}
			else
			{
				methodInfo = this.ObjectType.GetMethod(methodName, new Type[] { this.DataObjectType });
			}
			if (methodInfo == null)
			{
				throw new InvalidOperationException(string.Concat(new object[]
				{
					"ObjectDataSource ",
					this.owner.ID,
					" could not find a method named '",
					methodName,
					"' with parameters of type '",
					this.DataObjectType,
					"' in '",
					this.ObjectType,
					"'."
				}));
			}
			paramValues = new OrderedDictionary(StringComparer.InvariantCultureIgnoreCase);
			ParameterInfo[] parameters = methodInfo.GetParameters();
			if (oldValues != null)
			{
				if (this.FormatOldParameter(parameters[0].Name) == parameters[1].Name)
				{
					paramValues[parameters[0].Name] = this.CreateDataObject(values);
					paramValues[parameters[1].Name] = this.CreateDataObject(oldValues);
				}
				else
				{
					if (!(this.FormatOldParameter(parameters[1].Name) == parameters[0].Name))
					{
						throw new InvalidOperationException("Method '" + methodName + "' does not have any parameter that fits the value of OldValuesParameterFormatString.");
					}
					paramValues[parameters[0].Name] = this.CreateDataObject(oldValues);
					paramValues[parameters[1].Name] = this.CreateDataObject(values);
				}
			}
			else
			{
				paramValues[parameters[0].Name] = this.CreateDataObject(values);
			}
			return methodInfo;
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x00072CB4 File Offset: 0x00070EB4
		private Exception CreateMethodException(string methodName, IOrderedDictionary parameters)
		{
			string text = "";
			foreach (object obj in parameters.Keys)
			{
				string text2 = (string)obj;
				text = text + text2 + ", ";
			}
			return new InvalidOperationException(string.Concat(new object[]
			{
				"ObjectDataSource ",
				this.owner.ID,
				" could not find a method named '",
				methodName,
				"' with parameters ",
				text,
				"in type '",
				this.ObjectType,
				"'."
			}));
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x00072D74 File Offset: 0x00070F74
		private object CreateDataObject(IDictionary values)
		{
			object obj = Activator.CreateInstance(this.DataObjectType);
			foreach (object obj2 in values)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
				PropertyInfo property = this.DataObjectType.GetProperty((string)dictionaryEntry.Key);
				if (property == null)
				{
					throw new InvalidOperationException(string.Concat(new object[] { "Property ", dictionaryEntry.Key, " not found in type '", this.DataObjectType, "'." }));
				}
				object[] customAttributes = property.GetCustomAttributes(typeof(TypeConverterAttribute), true);
				Type propertyType = property.PropertyType;
				object value = dictionaryEntry.Value;
				object obj3 = this.ConvertParameterWithTypeConverter(customAttributes, propertyType, value);
				if (obj3 == null)
				{
					obj3 = this.ConvertParameter(propertyType, value);
				}
				property.SetValue(obj, obj3, null);
			}
			return obj;
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x00072E80 File Offset: 0x00071080
		private object CreateObjectInstance()
		{
			ObjectDataSourceEventArgs objectDataSourceEventArgs = new ObjectDataSourceEventArgs(null);
			this.OnObjectCreating(objectDataSourceEventArgs);
			if (objectDataSourceEventArgs.ObjectInstance != null)
			{
				return objectDataSourceEventArgs.ObjectInstance;
			}
			object obj = Activator.CreateInstance(this.ObjectType);
			objectDataSourceEventArgs.ObjectInstance = obj;
			this.OnObjectCreated(objectDataSourceEventArgs);
			return objectDataSourceEventArgs.ObjectInstance;
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x00072ECC File Offset: 0x000710CC
		private void DisposeObjectInstance(object obj)
		{
			ObjectDataSourceDisposingEventArgs objectDataSourceDisposingEventArgs = new ObjectDataSourceDisposingEventArgs(obj);
			this.OnObjectDisposing(objectDataSourceDisposingEventArgs);
			if (!objectDataSourceDisposingEventArgs.Cancel)
			{
				IDisposable disposable = obj as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x00072F00 File Offset: 0x00071100
		private object FindValueByName(string name, IDictionary values, bool format)
		{
			if (values == null)
			{
				return null;
			}
			foreach (object obj in values)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (format ? this.FormatOldParameter(dictionaryEntry.Key.ToString()) : dictionaryEntry.Key.ToString());
				if (string.Compare(name, text, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return values[dictionaryEntry.Key];
				}
			}
			return null;
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x00072F98 File Offset: 0x00071198
		private IOrderedDictionary MergeParameterValues(ParameterCollection viewParams, IDictionary values, IDictionary oldValues)
		{
			IOrderedDictionary values2 = viewParams.GetValues(this.context, this.owner);
			OrderedDictionary orderedDictionary = new OrderedDictionary(StringComparer.InvariantCultureIgnoreCase);
			foreach (object obj in values2.Keys)
			{
				string text = (string)obj;
				orderedDictionary[text] = values2[text];
				if (oldValues != null)
				{
					object obj2 = this.FindValueByName(text, oldValues, true);
					if (obj2 != null)
					{
						object obj3 = viewParams[text].ConvertValue(obj2);
						orderedDictionary[text] = obj3;
					}
				}
				if (values != null)
				{
					object obj4 = this.FindValueByName(text, values, false);
					if (obj4 != null)
					{
						object obj5 = viewParams[text].ConvertValue(obj4);
						orderedDictionary[text] = obj5;
					}
				}
			}
			if (values != null)
			{
				foreach (object obj6 in values)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj6;
					if (this.FindValueByName((string)dictionaryEntry.Key, orderedDictionary, false) == null)
					{
						orderedDictionary[dictionaryEntry.Key] = dictionaryEntry.Value;
					}
				}
			}
			if (oldValues != null)
			{
				foreach (object obj7 in oldValues)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj7;
					string text2 = this.FormatOldParameter((string)dictionaryEntry2.Key);
					if (this.FindValueByName(text2, orderedDictionary, false) == null)
					{
						orderedDictionary[text2] = dictionaryEntry2.Value;
					}
				}
			}
			return orderedDictionary;
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x00073158 File Offset: 0x00071358
		private object[] GetParameterArray(ParameterInfo[] methodParams, IOrderedDictionary viewParams, out ArrayList outParamInfos)
		{
			outParamInfos = null;
			object[] array = new object[methodParams.Length];
			foreach (ParameterInfo parameterInfo in methodParams)
			{
				if (!viewParams.Contains(parameterInfo.Name))
				{
					return null;
				}
				array[parameterInfo.Position] = this.ConvertParameter(parameterInfo.ParameterType, viewParams[parameterInfo.Name]);
				if (parameterInfo.ParameterType.IsByRef)
				{
					if (outParamInfos == null)
					{
						outParamInfos = new ArrayList();
					}
					outParamInfos.Add(parameterInfo);
				}
			}
			return array;
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x000731D8 File Offset: 0x000713D8
		private object ConvertParameterWithTypeConverter(object[] attributes, Type targetType, object value)
		{
			if (attributes == null || attributes.Length == 0 || value == null)
			{
				return null;
			}
			for (int i = 0; i < attributes.Length; i++)
			{
				TypeConverterAttribute typeConverterAttribute = attributes[i] as TypeConverterAttribute;
				if (typeConverterAttribute != null)
				{
					Type type = HttpApplication.LoadType(typeConverterAttribute.ConverterTypeName, false);
					if (!(type == null))
					{
						TypeConverter typeConverter = Activator.CreateInstance(type, new object[] { targetType }) as TypeConverter;
						if (typeConverter != null)
						{
							if (typeConverter.CanConvertFrom(value.GetType()))
							{
								return typeConverter.ConvertFrom(value);
							}
							if (typeConverter.CanConvertFrom(typeof(string)))
							{
								return typeConverter.ConvertFrom(value.ToString());
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x00073277 File Offset: 0x00071477
		private object ConvertParameter(Type targetType, object value)
		{
			return this.ConvertParameter(Type.GetTypeCode(targetType), value);
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x00073286 File Offset: 0x00071486
		private object ConvertParameter(TypeCode targetType, object value)
		{
			if (value == null)
			{
				if (targetType != TypeCode.Object && targetType != TypeCode.String)
				{
					value = 0;
				}
				else if (this.ConvertNullToDBNull)
				{
					return DBNull.Value;
				}
			}
			if (targetType == TypeCode.Object || targetType == TypeCode.Empty)
			{
				return value;
			}
			return Convert.ChangeType(value, targetType);
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x000732BC File Offset: 0x000714BC
		private string FormatOldParameter(string name)
		{
			string text = this.OldValuesParameterFormatString;
			if (text.Length > 0)
			{
				return string.Format(text, name);
			}
			return name;
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x00032D64 File Offset: 0x00030F64
		private void OnParametersChanged(object sender, EventArgs args)
		{
			this.OnDataSourceViewChanged(EventArgs.Empty);
		}

		/// <summary>Restores previously saved view state for the data source view.</summary>
		/// <param name="savedState">An object that represents the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> state to restore.</param>
		// Token: 0x06002B60 RID: 11104 RVA: 0x000732E4 File Offset: 0x000714E4
		protected virtual void LoadViewState(object savedState)
		{
			object[] array = ((savedState == null) ? new object[5] : ((object[])savedState));
			((IStateManager)this.SelectParameters).LoadViewState(array[0]);
			((IStateManager)this.UpdateParameters).LoadViewState(array[1]);
			((IStateManager)this.DeleteParameters).LoadViewState(array[2]);
			((IStateManager)this.InsertParameters).LoadViewState(array[3]);
			((IStateManager)this.FilterParameters).LoadViewState(array[4]);
		}

		/// <summary>Saves the changes to the view state for the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object since the time when the page was posted back to the server.</summary>
		/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> view state; otherwise null, if there is no view state associated with the object.</returns>
		// Token: 0x06002B61 RID: 11105 RVA: 0x0007334C File Offset: 0x0007154C
		protected virtual object SaveViewState()
		{
			object[] array = new object[5];
			if (this.selectParameters != null)
			{
				array[0] = ((IStateManager)this.selectParameters).SaveViewState();
			}
			if (this.updateParameters != null)
			{
				array[1] = ((IStateManager)this.updateParameters).SaveViewState();
			}
			if (this.deleteParameters != null)
			{
				array[2] = ((IStateManager)this.deleteParameters).SaveViewState();
			}
			if (this.insertParameters != null)
			{
				array[3] = ((IStateManager)this.insertParameters).SaveViewState();
			}
			if (this.filterParameters != null)
			{
				array[4] = ((IStateManager)this.filterParameters).SaveViewState();
			}
			object[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				if (array2[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <summary>Causes the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object to track changes to its view state so that the changes can be stored in the <see cref="P:System.Web.UI.Control.ViewState" /> object for the control and persisted across requests for the same page.</summary>
		// Token: 0x06002B62 RID: 11106 RVA: 0x000733E6 File Offset: 0x000715E6
		protected virtual void TrackViewState()
		{
			this.isTrackingViewState = true;
			if (this.selectParameters != null)
			{
				((IStateManager)this.selectParameters).TrackViewState();
			}
			if (this.filterParameters != null)
			{
				((IStateManager)this.filterParameters).TrackViewState();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object is saving changes to its view state.</summary>
		/// <returns>true, if the data source view is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x00073415 File Offset: 0x00071615
		protected bool IsTrackingViewState
		{
			get
			{
				return this.isTrackingViewState;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IStateManager.IsTrackingViewState" />.</summary>
		/// <returns>true, if the data source view is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x06002B64 RID: 11108 RVA: 0x0007341D File Offset: 0x0007161D
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.TrackViewState" />.</summary>
		// Token: 0x06002B65 RID: 11109 RVA: 0x00073425 File Offset: 0x00071625
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.LoadViewState(System.Object)" />.</summary>
		/// <param name="savedState">An object that represents the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> state to restore.</param>
		// Token: 0x06002B66 RID: 11110 RVA: 0x0007342D File Offset: 0x0007162D
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.SaveViewState" />.</summary>
		/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> view state; otherwise, null.</returns>
		// Token: 0x06002B67 RID: 11111 RVA: 0x00073436 File Offset: 0x00071636
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x00073440 File Offset: 0x00071640
		// Note: this type is marked as 'beforefieldinit'.
		static ObjectDataSourceView()
		{
			ObjectDataSourceView.DeletedEvent = new object();
			ObjectDataSourceView.DeletingEvent = new object();
			ObjectDataSourceView.FilteringEvent = new object();
			ObjectDataSourceView.InsertedEvent = new object();
			ObjectDataSourceView.InsertingEvent = new object();
			ObjectDataSourceView.ObjectCreatedEvent = new object();
			ObjectDataSourceView.ObjectCreatingEvent = new object();
			ObjectDataSourceView.ObjectDisposingEvent = new object();
			ObjectDataSourceView.SelectedEvent = new object();
			ObjectDataSourceView.SelectingEvent = new object();
			ObjectDataSourceView.UpdatedEvent = new object();
			ObjectDataSourceView.UpdatingEvent = new object();
		}

		/// <summary>Gets a or sets a value that indicates what culture information is used when converting string values to actual property types in order to construct an object of the type indicated by <see cref="P:System.Web.UI.WebControls.ObjectDataSource.DataObjectTypeName" />.</summary>
		/// <returns>The culture information. The default value is <see cref="F:System.Web.UI.WebControls.ParsingCulture.Invariant" />.</returns>
		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x06002B69 RID: 11113 RVA: 0x000734C8 File Offset: 0x000716C8
		// (set) Token: 0x06002B6A RID: 11114 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ParsingCulture ParsingCulture
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ParsingCulture.Invariant;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04001AF0 RID: 6896
		private ObjectDataSource owner;

		// Token: 0x04001AF1 RID: 6897
		private HttpContext context;

		// Token: 0x04001AF2 RID: 6898
		private Type objectType;

		// Token: 0x04001AF3 RID: 6899
		private Type dataObjectType;

		// Token: 0x04001AF4 RID: 6900
		private bool convertNullToDBNull;

		// Token: 0x04001AF5 RID: 6901
		private bool enablePaging;

		// Token: 0x04001AF6 RID: 6902
		private string dataObjectTypeName;

		// Token: 0x04001AF7 RID: 6903
		private string filterExpression;

		// Token: 0x04001AF8 RID: 6904
		private string maximumRowsParameterName;

		// Token: 0x04001AF9 RID: 6905
		private string oldValuesParameterFormatString;

		// Token: 0x04001AFA RID: 6906
		private string deleteMethod;

		// Token: 0x04001AFB RID: 6907
		private string insertMethod;

		// Token: 0x04001AFC RID: 6908
		private string selectCountMethod;

		// Token: 0x04001AFD RID: 6909
		private string selectMethod;

		// Token: 0x04001AFE RID: 6910
		private string sortParameterName;

		// Token: 0x04001AFF RID: 6911
		private string startRowIndexParameterName;

		// Token: 0x04001B00 RID: 6912
		private string typeName;

		// Token: 0x04001B01 RID: 6913
		private string updateMethod;

		// Token: 0x04001B02 RID: 6914
		private bool isTrackingViewState;

		// Token: 0x04001B03 RID: 6915
		private ParameterCollection selectParameters;

		// Token: 0x04001B04 RID: 6916
		private ParameterCollection updateParameters;

		// Token: 0x04001B05 RID: 6917
		private ParameterCollection deleteParameters;

		// Token: 0x04001B06 RID: 6918
		private ParameterCollection insertParameters;

		// Token: 0x04001B07 RID: 6919
		private ParameterCollection filterParameters;

		// Token: 0x04001B14 RID: 6932
		private ConflictOptions conflictDetection;
	}
}
