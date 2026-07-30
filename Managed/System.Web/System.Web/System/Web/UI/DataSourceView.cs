using System;
using System.Collections;
using System.ComponentModel;
using Unity;

namespace System.Web.UI
{
	/// <summary>Serves as the base class for all data source view classes, which define the capabilities of data source controls.</summary>
	// Token: 0x020001C6 RID: 454
	public abstract class DataSourceView
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataSourceView" /> class.</summary>
		/// <param name="owner">The data source control that the <see cref="T:System.Web.UI.DataSourceView" /> is associated with.</param>
		/// <param name="viewName">The name of the <see cref="T:System.Web.UI.DataSourceView" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null.- or -<paramref name="viewName" /> is null.</exception>
		// Token: 0x06001283 RID: 4739 RVA: 0x00032D2A File Offset: 0x00030F2A
		protected DataSourceView(IDataSource owner, string viewName)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this.viewName = viewName;
			owner.DataSourceChanged += this.OnDataSourceChanged;
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00032D64 File Offset: 0x00030F64
		private void OnDataSourceChanged(object sender, EventArgs e)
		{
			this.OnDataSourceViewChanged(EventArgs.Empty);
		}

		/// <summary>Performs an asynchronous delete operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView" /> object represents.</summary>
		/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of object or row keys to be deleted by the <see cref="M:System.Web.UI.DataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> operation.</param>
		/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent data elements and their original values.</param>
		/// <param name="callback">A <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> delegate that is used to notify a data-bound control when the asynchronous operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> supplied is null. </exception>
		// Token: 0x06001285 RID: 4741 RVA: 0x00032D74 File Offset: 0x00030F74
		public virtual void Delete(IDictionary keys, IDictionary oldValues, DataSourceViewOperationCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callBack");
			}
			int num;
			try
			{
				num = this.ExecuteDelete(keys, oldValues);
			}
			catch (Exception ex)
			{
				if (!callback(0, ex))
				{
					throw;
				}
				return;
			}
			callback(num, null);
		}

		/// <summary>Performs a delete operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView" /> object represents.</summary>
		/// <returns>The number of items that were deleted from the underlying data storage.</returns>
		/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of object or row keys to be deleted by the <see cref="M:System.Web.UI.DataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> operation.</param>
		/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent data elements and their original values.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="M:System.Web.UI.DataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> operation is not supported by the <see cref="T:System.Web.UI.DataSourceView" />. </exception>
		// Token: 0x06001286 RID: 4742 RVA: 0x00003A01 File Offset: 0x00001C01
		protected virtual int ExecuteDelete(IDictionary keys, IDictionary oldValues)
		{
			throw new NotSupportedException();
		}

		/// <summary>Performs an insert operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView" /> object represents.</summary>
		/// <returns>The number of items that were inserted into the underlying data storage.</returns>
		/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs used during an insert operation.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="M:System.Web.UI.DataSourceView.ExecuteInsert(System.Collections.IDictionary)" /> operation is not supported by the <see cref="T:System.Web.UI.DataSourceView" />. </exception>
		// Token: 0x06001287 RID: 4743 RVA: 0x00003A01 File Offset: 0x00001C01
		protected virtual int ExecuteInsert(IDictionary values)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets a list of data from the underlying data storage.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of data from the underlying data storage.</returns>
		/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that is used to request operations on the data beyond basic data retrieval.</param>
		// Token: 0x06001288 RID: 4744
		protected internal abstract IEnumerable ExecuteSelect(DataSourceSelectArguments arguments);

		/// <summary>Performs an update operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView" /> object represents.</summary>
		/// <returns>The number of items that were updated in the underlying data storage.</returns>
		/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of object or row keys to be updated by the update operation.</param>
		/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent data elements and their new values.</param>
		/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent data elements and their original values.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="M:System.Web.UI.DataSourceView.ExecuteUpdate(System.Collections.IDictionary,System.Collections.IDictionary,System.Collections.IDictionary)" /> operation is not supported by the <see cref="T:System.Web.UI.DataSourceView" />. </exception>
		// Token: 0x06001289 RID: 4745 RVA: 0x00003A01 File Offset: 0x00001C01
		protected virtual int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			throw new NotSupportedException();
		}

		/// <summary>Performs an asynchronous insert operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView" /> object represents.</summary>
		/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs used during an insert operation.</param>
		/// <param name="callback">A <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> delegate that is used to notify a data-bound control when the asynchronous operation is complete. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> supplied is null.</exception>
		// Token: 0x0600128A RID: 4746 RVA: 0x00032DC4 File Offset: 0x00030FC4
		public virtual void Insert(IDictionary values, DataSourceViewOperationCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			int num;
			try
			{
				num = this.ExecuteInsert(values);
			}
			catch (Exception ex)
			{
				if (!callback(0, ex))
				{
					throw;
				}
				return;
			}
			callback(num, null);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.DataSourceView.DataSourceViewChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x0600128B RID: 4747 RVA: 0x00032E14 File Offset: 0x00031014
		protected virtual void OnDataSourceViewChanged(EventArgs e)
		{
			if (this.eventsList != null)
			{
				EventHandler eventHandler = this.eventsList[DataSourceView.EventDataSourceViewChanged] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Called by the <see cref="M:System.Web.UI.DataSourceSelectArguments.RaiseUnsupportedCapabilitiesError(System.Web.UI.DataSourceView)" /> method to compare the capabilities requested for an <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> operation against those that the view supports.</summary>
		/// <param name="capability">One of the <see cref="T:System.Web.UI.DataSourceCapabilities" /> values that is compared against the capabilities that the view supports.</param>
		/// <exception cref="T:System.NotSupportedException">The data source view does not support the data source capability specified.</exception>
		// Token: 0x0600128C RID: 4748 RVA: 0x00032E4C File Offset: 0x0003104C
		protected internal virtual void RaiseUnsupportedCapabilityError(DataSourceCapabilities capability)
		{
			if ((capability & DataSourceCapabilities.Sort) != DataSourceCapabilities.None && !this.CanSort)
			{
				throw new NotSupportedException("Sort Capabilites");
			}
			if ((capability & DataSourceCapabilities.Page) != DataSourceCapabilities.None && !this.CanPage)
			{
				throw new NotSupportedException("Page Capabilites");
			}
			if ((capability & DataSourceCapabilities.RetrieveTotalRowCount) != DataSourceCapabilities.None && !this.CanRetrieveTotalRowCount)
			{
				throw new NotSupportedException("RetrieveTotalRowCount Capabilites");
			}
		}

		/// <summary>Gets a list of data asynchronously from the underlying data storage.</summary>
		/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that is used to request operations on the data beyond basic data retrieval.</param>
		/// <param name="callback">A <see cref="T:System.Web.UI.DataSourceViewSelectCallback" /> delegate that is used to notify a data-bound control when the asynchronous operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.DataSourceViewSelectCallback" /> supplied is null.</exception>
		// Token: 0x0600128D RID: 4749 RVA: 0x00032EA4 File Offset: 0x000310A4
		public virtual void Select(DataSourceSelectArguments arguments, DataSourceViewSelectCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callBack");
			}
			arguments.RaiseUnsupportedCapabilitiesError(this);
			IEnumerable enumerable = this.ExecuteSelect(arguments);
			callback(enumerable);
		}

		/// <summary>Performs an asynchronous update operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView" /> object represents.</summary>
		/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of object or row keys to be updated by the update operation.</param>
		/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent data elements and their new values.</param>
		/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent data elements and their original values.</param>
		/// <param name="callback">A <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> delegate that is used to notify a data-bound control when the asynchronous operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> supplied is null.</exception>
		// Token: 0x0600128E RID: 4750 RVA: 0x00032ED8 File Offset: 0x000310D8
		public virtual void Update(IDictionary keys, IDictionary values, IDictionary oldValues, DataSourceViewOperationCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			int num;
			try
			{
				num = this.ExecuteUpdate(keys, values, oldValues);
			}
			catch (Exception ex)
			{
				if (!callback(0, ex))
				{
					throw;
				}
				return;
			}
			callback(num, null);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports the <see cref="M:System.Web.UI.DataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> operation.</summary>
		/// <returns>true if the operation is supported; otherwise, false. The base class implementation returns false.</returns>
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual bool CanDelete
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports the <see cref="M:System.Web.UI.DataSourceView.ExecuteInsert(System.Collections.IDictionary)" /> operation.</summary>
		/// <returns>true if the operation is supported; otherwise, false. The base class implementation returns false.</returns>
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual bool CanInsert
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports paging through the data retrieved by the <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method.</summary>
		/// <returns>true if the operation is supported; otherwise, false. The base class implementation returns false.</returns>
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual bool CanPage
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports retrieving the total number of data rows, instead of the data.</summary>
		/// <returns>true if the operation is supported; otherwise, false. The base class implementation returns false.</returns>
		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual bool CanRetrieveTotalRowCount
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports a sorted view on the underlying data source.</summary>
		/// <returns>true if the operation is supported; otherwise, false. The default implementation returns false.</returns>
		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual bool CanSort
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports the <see cref="M:System.Web.UI.DataSourceView.ExecuteUpdate(System.Collections.IDictionary,System.Collections.IDictionary,System.Collections.IDictionary)" /> operation.</summary>
		/// <returns>true if the operation is supported; otherwise, false. The default implementation returns false.</returns>
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x00008A69 File Offset: 0x00006C69
		public virtual bool CanUpdate
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a list of event-handler delegates for the data source view.</summary>
		/// <returns>The list of event-handler delegates.</returns>
		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x00032F2C File Offset: 0x0003112C
		protected EventHandlerList Events
		{
			get
			{
				if (this.eventsList == null)
				{
					this.eventsList = new EventHandlerList();
				}
				return this.eventsList;
			}
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00032F47 File Offset: 0x00031147
		internal bool HasEvents()
		{
			return this.eventsList != null;
		}

		/// <summary>Gets the name of the data source view.</summary>
		/// <returns>The name of the <see cref="T:System.Web.UI.DataSourceView" />, if it has one. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x00032F52 File Offset: 0x00031152
		public string Name
		{
			get
			{
				return this.viewName;
			}
		}

		/// <summary>Occurs when the data source view has changed.</summary>
		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06001298 RID: 4760 RVA: 0x00032F5A File Offset: 0x0003115A
		// (remove) Token: 0x06001299 RID: 4761 RVA: 0x00032F6D File Offset: 0x0003116D
		public event EventHandler DataSourceViewChanged
		{
			add
			{
				this.Events.AddHandler(DataSourceView.EventDataSourceViewChanged, value);
			}
			remove
			{
				this.Events.RemoveHandler(DataSourceView.EventDataSourceViewChanged, value);
			}
		}

		/// <summary>Determines whether the specified command can be executed.</summary>
		/// <returns>true if the command can be executed; otherwise, false.</returns>
		/// <param name="commandName">The name of the command.</param>
		// Token: 0x0600129B RID: 4763 RVA: 0x00032F8C File Offset: 0x0003118C
		public virtual bool CanExecute(string commandName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Executes the specified command.</summary>
		/// <returns>The number of items that were affected by the operation.</returns>
		/// <param name="commandName">The name of the command.</param>
		/// <param name="keys">A dictionary of object keys or row keys to act on.</param>
		/// <param name="values">A dictionary of name/value pairs that represent data elements and their values.</param>
		// Token: 0x0600129C RID: 4764 RVA: 0x00032FA8 File Offset: 0x000311A8
		protected virtual int ExecuteCommand(string commandName, IDictionary keys, IDictionary values)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Executes the specified command.</summary>
		/// <param name="commandName">The name of the command.</param>
		/// <param name="keys">A dictionary of object keys or row keys to act on.</param>
		/// <param name="values">A dictionary of name/value pairs that represent data elements and their values.</param>
		/// <param name="callback">A <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> object.</param>
		// Token: 0x0600129D RID: 4765 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void ExecuteCommand(string commandName, IDictionary keys, IDictionary values, DataSourceViewOperationCallback callback)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001426 RID: 5158
		private string viewName = string.Empty;

		// Token: 0x04001427 RID: 5159
		private EventHandlerList eventsList;

		// Token: 0x04001428 RID: 5160
		private static readonly object EventDataSourceViewChanged = new object();
	}
}
