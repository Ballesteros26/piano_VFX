using System;

namespace System.Web.UI
{
	/// <summary>Provides a mechanism that data-bound controls use to request data-related operations from data source controls when data is retrieved. This class cannot be inherited.</summary>
	// Token: 0x020001C5 RID: 453
	public sealed class DataSourceSelectArguments
	{
		/// <summary>Gets a <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object with the sort expression set to <see cref="F:System.String.Empty" />. </summary>
		/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object.</returns>
		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600126F RID: 4719 RVA: 0x00032B1A File Offset: 0x00030D1A
		public static DataSourceSelectArguments Empty
		{
			get
			{
				return new DataSourceSelectArguments();
			}
		}

		/// <summary>Initializes a new default instance of the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> class. </summary>
		// Token: 0x06001270 RID: 4720 RVA: 0x00032B21 File Offset: 0x00030D21
		public DataSourceSelectArguments()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> class with the specified sort expression.</summary>
		/// <param name="sortExpression">A sort expression that data source controls use to sort the result of a data retrieval operation before the result is returned to a caller.</param>
		// Token: 0x06001271 RID: 4721 RVA: 0x00032B30 File Offset: 0x00030D30
		public DataSourceSelectArguments(string sortExpression)
		{
			this.sortExpression = sortExpression;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> class with the specified starting position and number of rows to return for paging scenarios.</summary>
		/// <param name="startRowIndex">The index of the data row that marks the beginning of data returned by a data retrieval operation.</param>
		/// <param name="maximumRows">The maximum number of rows that a data retrieval operation returns.</param>
		// Token: 0x06001272 RID: 4722 RVA: 0x00032B46 File Offset: 0x00030D46
		public DataSourceSelectArguments(int startRowIndex, int maximumRows)
		{
			this.startingRowIndex = startRowIndex;
			this.maxRows = maximumRows;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> class with the specified sort expression, starting position, and number of rows to return for paging scenarios.</summary>
		/// <param name="sortExpression">A sort expression that data source controls use to sort the result of a data retrieval operation before the result is returned to a caller.</param>
		/// <param name="startRowIndex">The index of the data row that marks the beginning of data returned by a data retrieval operation.</param>
		/// <param name="maximumRows">The maximum number of rows that a data retrieval operation returns.</param>
		// Token: 0x06001273 RID: 4723 RVA: 0x00032B63 File Offset: 0x00030D63
		public DataSourceSelectArguments(string sortExpression, int startRowIndex, int maximumRows)
		{
			this.sortExpression = sortExpression;
			this.startingRowIndex = startRowIndex;
			this.maxRows = maximumRows;
		}

		/// <summary>Adds one capability to the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> instance, which is used to compare supported capabilities and requested capabilities. </summary>
		/// <param name="capabilities">One of the <see cref="T:System.Web.UI.DataSourceCapabilities" /> values. </param>
		// Token: 0x06001274 RID: 4724 RVA: 0x00032B87 File Offset: 0x00030D87
		public void AddSupportedCapabilities(DataSourceCapabilities capabilities)
		{
			this.dsc |= capabilities;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Web.UI.DataSourceSelectArguments" /> instance is equal to the current instance.</summary>
		/// <returns>true if the specified <see cref="T:System.Web.UI.DataSourceSelectArguments" /> is equal to the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Web.UI.DataSourceSelectArguments" /> to compare with the current one.</param>
		// Token: 0x06001275 RID: 4725 RVA: 0x00032B98 File Offset: 0x00030D98
		public override bool Equals(object obj)
		{
			DataSourceSelectArguments dataSourceSelectArguments = obj as DataSourceSelectArguments;
			return dataSourceSelectArguments != null && (this.SortExpression == dataSourceSelectArguments.SortExpression && this.StartRowIndex == dataSourceSelectArguments.StartRowIndex && this.MaximumRows == dataSourceSelectArguments.MaximumRows && this.RetrieveTotalRowCount == dataSourceSelectArguments.RetrieveTotalRowCount) && this.TotalRowCount == dataSourceSelectArguments.TotalRowCount;
		}

		/// <summary>Returns the hash code for the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> type.</summary>
		/// <returns>The hash code for the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> type.</returns>
		// Token: 0x06001276 RID: 4726 RVA: 0x00032C00 File Offset: 0x00030E00
		public override int GetHashCode()
		{
			return ((this.SortExpression != null) ? this.SortExpression.GetHashCode() : 0) ^ this.StartRowIndex ^ this.MaximumRows ^ this.RetrieveTotalRowCount.GetHashCode() ^ this.TotalRowCount;
		}

		/// <summary>Compares the capabilities requested for an <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> operation against those that the specified data source view supports.</summary>
		/// <param name="view">The data source view that performs the data retrieval operation.</param>
		/// <exception cref="T:System.NotSupportedException">The data source view does not support the data source capability specified.</exception>
		// Token: 0x06001277 RID: 4727 RVA: 0x00032C48 File Offset: 0x00030E48
		public void RaiseUnsupportedCapabilitiesError(DataSourceView view)
		{
			DataSourceCapabilities requestedCapabilities = this.RequestedCapabilities;
			DataSourceCapabilities dataSourceCapabilities = (requestedCapabilities ^ this.dsc) & requestedCapabilities;
			if (dataSourceCapabilities == DataSourceCapabilities.None)
			{
				return;
			}
			if ((dataSourceCapabilities & DataSourceCapabilities.RetrieveTotalRowCount) > DataSourceCapabilities.None)
			{
				dataSourceCapabilities = DataSourceCapabilities.RetrieveTotalRowCount;
			}
			else if ((dataSourceCapabilities & DataSourceCapabilities.Page) > DataSourceCapabilities.None)
			{
				dataSourceCapabilities = DataSourceCapabilities.Page;
			}
			view.RaiseUnsupportedCapabilityError(dataSourceCapabilities);
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x00032C84 File Offset: 0x00030E84
		private DataSourceCapabilities RequestedCapabilities
		{
			get
			{
				DataSourceCapabilities dataSourceCapabilities = DataSourceCapabilities.None;
				if (!string.IsNullOrEmpty(this.SortExpression))
				{
					dataSourceCapabilities |= DataSourceCapabilities.Sort;
				}
				if (this.RetrieveTotalRowCount)
				{
					dataSourceCapabilities |= DataSourceCapabilities.RetrieveTotalRowCount;
				}
				if (this.StartRowIndex > 0 || this.MaximumRows > 0)
				{
					dataSourceCapabilities |= DataSourceCapabilities.Page;
				}
				return dataSourceCapabilities;
			}
		}

		/// <summary>Gets or sets a value that represents the maximum number of data rows that a data source control returns for a data retrieval operation.</summary>
		/// <returns>The maximum number of data rows that a data source returns for a data retrieval operation. The default value is 0, which indicates that all possible data rows are returned.</returns>
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x00032CC7 File Offset: 0x00030EC7
		// (set) Token: 0x0600127A RID: 4730 RVA: 0x00032CCF File Offset: 0x00030ECF
		public int MaximumRows
		{
			get
			{
				return this.maxRows;
			}
			set
			{
				this.maxRows = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a data source control should retrieve a count of all the data rows during a data retrieval operation.</summary>
		/// <returns>true if the data source control should retrieve a total data row count; otherwise, false.</returns>
		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x00032CD8 File Offset: 0x00030ED8
		// (set) Token: 0x0600127C RID: 4732 RVA: 0x00032CE0 File Offset: 0x00030EE0
		public bool RetrieveTotalRowCount
		{
			get
			{
				return this.getTotalRowCount;
			}
			set
			{
				this.getTotalRowCount = value;
			}
		}

		/// <summary>Gets or sets an expression that the data source view uses to sort the data retrieved by the <see cref="M:System.Web.UI.DataSourceView.Select(System.Web.UI.DataSourceSelectArguments,System.Web.UI.DataSourceViewSelectCallback)" /> method.</summary>
		/// <returns>A string that the data source view uses to sort data retrieved by the <see cref="M:System.Web.UI.DataSourceView.Select(System.Web.UI.DataSourceSelectArguments,System.Web.UI.DataSourceViewSelectCallback)" /> method. <see cref="F:System.String.Empty" /> is returned if sort expression has not been set.</returns>
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x00032CE9 File Offset: 0x00030EE9
		// (set) Token: 0x0600127E RID: 4734 RVA: 0x00032CFF File Offset: 0x00030EFF
		public string SortExpression
		{
			get
			{
				if (this.sortExpression == null)
				{
					return string.Empty;
				}
				return this.sortExpression;
			}
			set
			{
				this.sortExpression = value;
			}
		}

		/// <summary>Gets or sets a value that represents the starting position the data source control should use when retrieving data rows during a data retrieval operation.</summary>
		/// <returns>The starting row position from which a data source control retrieves data. The default value is 0, which indicates that the starting position is the beginning of the result set.</returns>
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x00032D08 File Offset: 0x00030F08
		// (set) Token: 0x06001280 RID: 4736 RVA: 0x00032D10 File Offset: 0x00030F10
		public int StartRowIndex
		{
			get
			{
				return this.startingRowIndex;
			}
			set
			{
				this.startingRowIndex = value;
			}
		}

		/// <summary>Gets or sets the number of rows retrieved during a data retrieval operation.</summary>
		/// <returns>The total number of data rows retrieved by the data retrieval operation. </returns>
		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x00032D19 File Offset: 0x00030F19
		// (set) Token: 0x06001282 RID: 4738 RVA: 0x00032D21 File Offset: 0x00030F21
		public int TotalRowCount
		{
			get
			{
				return this.totalRowCount;
			}
			set
			{
				this.totalRowCount = value;
			}
		}

		// Token: 0x04001420 RID: 5152
		private string sortExpression;

		// Token: 0x04001421 RID: 5153
		private int startingRowIndex;

		// Token: 0x04001422 RID: 5154
		private int maxRows;

		// Token: 0x04001423 RID: 5155
		private bool getTotalRowCount;

		// Token: 0x04001424 RID: 5156
		private int totalRowCount = -1;

		// Token: 0x04001425 RID: 5157
		private DataSourceCapabilities dsc;
	}
}
