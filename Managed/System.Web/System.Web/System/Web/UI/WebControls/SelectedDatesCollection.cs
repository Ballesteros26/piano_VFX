using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Encapsulates a collection of <see cref="T:System.DateTime" /> objects that represent the selected dates in a <see cref="T:System.Web.UI.WebControls.Calendar" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000405 RID: 1029
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class SelectedDatesCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> class with the specified date list.</summary>
		/// <param name="dateList">An <see cref="T:System.Collections.ArrayList" /> that represents a collection of dates. </param>
		// Token: 0x06002DA2 RID: 11682 RVA: 0x00078EB1 File Offset: 0x000770B1
		public SelectedDatesCollection(ArrayList dateList)
		{
			this.l = dateList;
		}

		/// <summary>Appends the specified <see cref="T:System.DateTime" /> object to the end of the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection.</summary>
		/// <param name="date">The <see cref="T:System.DateTime" /> to add to the collection. </param>
		// Token: 0x06002DA3 RID: 11683 RVA: 0x00078EC0 File Offset: 0x000770C0
		public void Add(DateTime date)
		{
			date = date.Date;
			if (!this.l.Contains(date))
			{
				this.l.Add(date);
			}
		}

		/// <summary>Removes all <see cref="T:System.DateTime" /> objects from the collection.</summary>
		// Token: 0x06002DA4 RID: 11684 RVA: 0x00078EF0 File Offset: 0x000770F0
		public void Clear()
		{
			this.l.Clear();
		}

		/// <summary>Returns a value indicating whether the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection contains the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> contains the specified <see cref="T:System.DateTime" />; otherwise, false.</returns>
		/// <param name="date">The <see cref="T:System.DateTime" /> to search for in the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" />. </param>
		// Token: 0x06002DA5 RID: 11685 RVA: 0x00078EFD File Offset: 0x000770FD
		public bool Contains(DateTime date)
		{
			return this.l.Contains(date.Date);
		}

		/// <summary>Copies the items from the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection to the specified <see cref="T:System.Array" />, starting with the specified index.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the copied items from the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" />. </param>
		/// <param name="index">The first index in the specified <see cref="T:System.Array" /> to receive the items. </param>
		// Token: 0x06002DA6 RID: 11686 RVA: 0x00078F16 File Offset: 0x00077116
		public void CopyTo(Array array, int index)
		{
			this.l.CopyTo(array, index);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.DateTime" /> objects within the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all <see cref="T:System.DateTime" /> objects within the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" />.</returns>
		// Token: 0x06002DA7 RID: 11687 RVA: 0x00078F25 File Offset: 0x00077125
		public IEnumerator GetEnumerator()
		{
			return this.l.GetEnumerator();
		}

		/// <summary>Removes the specified <see cref="T:System.DateTime" /> object from the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection.</summary>
		/// <param name="date">The <see cref="T:System.DateTime" /> to remove from the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" />. </param>
		// Token: 0x06002DA8 RID: 11688 RVA: 0x00078F32 File Offset: 0x00077132
		public void Remove(DateTime date)
		{
			this.l.Remove(date.Date);
		}

		/// <summary>Adds the specified range of dates to the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection.</summary>
		/// <param name="fromDate">A <see cref="T:System.DateTime" /> that specifies the initial date to add to the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" />. </param>
		/// <param name="toDate">A <see cref="T:System.DateTime" /> that specifies the end date to add to the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" />. </param>
		// Token: 0x06002DA9 RID: 11689 RVA: 0x00078F4C File Offset: 0x0007714C
		public void SelectRange(DateTime fromDate, DateTime toDate)
		{
			fromDate = fromDate.Date;
			toDate = toDate.Date;
			this.l.Clear();
			DateTime dateTime = fromDate;
			while (dateTime <= toDate)
			{
				this.Add(dateTime);
				dateTime = dateTime.AddDays(1.0);
			}
		}

		/// <summary>Gets the number of <see cref="T:System.DateTime" /> objects in the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection.</summary>
		/// <returns>The number of <see cref="T:System.DateTime" /> objects in the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" />.</returns>
		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06002DAA RID: 11690 RVA: 0x00078F9B File Offset: 0x0007719B
		public int Count
		{
			get
			{
				return this.l.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection is read-only.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06002DAB RID: 11691 RVA: 0x00078FA8 File Offset: 0x000771A8
		public bool IsReadOnly
		{
			get
			{
				return this.l.IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection is synchronized (thread safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06002DAC RID: 11692 RVA: 0x00078FB5 File Offset: 0x000771B5
		public bool IsSynchronized
		{
			get
			{
				return this.l.IsSynchronized;
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that represents an element in the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" />.</returns>
		/// <param name="index">An ordinal index value that specifies which <see cref="T:System.DateTime" /> to return. </param>
		// Token: 0x17000E8D RID: 3725
		public DateTime this[int index]
		{
			get
			{
				return (DateTime)this.l[index];
			}
		}

		/// <summary>Gets the object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06002DAE RID: 11694 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04001B82 RID: 7042
		private ArrayList l;
	}
}
