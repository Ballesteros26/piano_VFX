using System;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies the column that is displayed in the referred table as a foreign-key column.</summary>
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public class DisplayColumnAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.DisplayColumnAttribute" /> class by using the specified column. </summary>
		/// <param name="displayColumn">The name of the column to use as the display column.</param>
		// Token: 0x06000066 RID: 102 RVA: 0x00002F8A File Offset: 0x0000118A
		public DisplayColumnAttribute(string displayColumn)
			: this(displayColumn, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.DisplayColumnAttribute" /> class by using the specified display and sort columns. </summary>
		/// <param name="displayColumn">The name of the column to use as the display column.</param>
		/// <param name="sortColumn">The name of the column to use for sorting.</param>
		// Token: 0x06000067 RID: 103 RVA: 0x00002F94 File Offset: 0x00001194
		public DisplayColumnAttribute(string displayColumn, string sortColumn)
			: this(displayColumn, sortColumn, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.DisplayColumnAttribute" /> class by using the specified display column, and the specified sort column and sort order. </summary>
		/// <param name="displayColumn">The name of the column to use as the display column.</param>
		/// <param name="sortColumn">The name of the column to use for sorting.</param>
		/// <param name="sortDescending">true to sort in descending order; otherwise, false. The default is false.</param>
		// Token: 0x06000068 RID: 104 RVA: 0x00002F9F File Offset: 0x0000119F
		public DisplayColumnAttribute(string displayColumn, string sortColumn, bool sortDescending)
		{
			this.DisplayColumn = displayColumn;
			this.SortColumn = sortColumn;
			this.SortDescending = sortDescending;
		}

		/// <summary>Gets the name of the column to use as the display field.</summary>
		/// <returns>The name of the display column.</returns>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002FBC File Offset: 0x000011BC
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002FC4 File Offset: 0x000011C4
		public string DisplayColumn { get; private set; }

		/// <summary>Gets the name of the column to use for sorting.</summary>
		/// <returns>The name of the sort column.</returns>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002FCD File Offset: 0x000011CD
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002FD5 File Offset: 0x000011D5
		public string SortColumn { get; private set; }

		/// <summary>Gets a value that indicates whether to sort in descending or ascending order.</summary>
		/// <returns>true if the column will be sorted in descending order; otherwise, false.</returns>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002FDE File Offset: 0x000011DE
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002FE6 File Offset: 0x000011E6
		public bool SortDescending { get; private set; }
	}
}
