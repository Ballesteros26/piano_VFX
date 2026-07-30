using System;

namespace System.Windows.Forms
{
	/// <summary>A collection that stores <see cref="T:System.Windows.Forms.ColumnStyle" /> objects.</summary>
	// Token: 0x02000302 RID: 770
	public class TableLayoutColumnStyleCollection : TableLayoutStyleCollection
	{
		// Token: 0x0600336B RID: 13163 RVA: 0x000C2934 File Offset: 0x000C0B34
		internal TableLayoutColumnStyleCollection(TableLayoutPanel panel)
			: base(panel)
		{
		}

		/// <summary>Adds an item to the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="columnStyle">The <see cref="T:System.Windows.Forms.ColumnStyle" /> to add to the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />.</param>
		// Token: 0x0600336C RID: 13164 RVA: 0x000C2940 File Offset: 0x000C0B40
		public int Add(ColumnStyle columnStyle)
		{
			return base.Add(columnStyle);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Windows.Forms.ColumnStyle" /> is in the collection.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ColumnStyle" /> is found in the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />; otherwise, false.</returns>
		/// <param name="columnStyle">The <see cref="T:System.Windows.Forms.ColumnStyle" /> to locate in the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />. The value can be null.</param>
		// Token: 0x0600336D RID: 13165 RVA: 0x000C294C File Offset: 0x000C0B4C
		public bool Contains(ColumnStyle columnStyle)
		{
			return this.Contains(columnStyle);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />.</summary>
		/// <returns>The index of <paramref name="columnStyle" /> if found in the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />; otherwise, -1.</returns>
		/// <param name="columnStyle">The <see cref="T:System.Windows.Forms.ColumnStyle" /> to locate in the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />.</param>
		// Token: 0x0600336E RID: 13166 RVA: 0x000C2958 File Offset: 0x000C0B58
		public int IndexOf(ColumnStyle columnStyle)
		{
			return this.IndexOf(columnStyle);
		}

		/// <summary>Inserts a <see cref="T:System.Windows.Forms.ColumnStyle" /> into the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" /> at the specified position.</summary>
		/// <param name="index">The zero-based index at which <see cref="T:System.Windows.Forms.ColumnStyle" /> should be inserted.</param>
		/// <param name="columnStyle">The <see cref="T:System.Windows.Forms.ColumnStyle" /> to insert into the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />.</param>
		// Token: 0x0600336F RID: 13167 RVA: 0x000C2964 File Offset: 0x000C0B64
		public void Insert(int index, ColumnStyle columnStyle)
		{
			this.Insert(index, columnStyle);
		}

		/// <summary>Removes the first occurrence of a specific <see cref="T:System.Windows.Forms.ColumnStyle" /> from the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />.</summary>
		/// <param name="columnStyle">The <see cref="T:System.Windows.Forms.ColumnStyle" /> to remove from the <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" />. The value can be null.</param>
		// Token: 0x06003370 RID: 13168 RVA: 0x000C2970 File Offset: 0x000C0B70
		public void Remove(ColumnStyle columnStyle)
		{
			this.Remove(columnStyle);
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ColumnStyle" /> at the specified index.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ColumnStyle" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.ColumnStyle" /> to get or set.</param>
		// Token: 0x17000D6F RID: 3439
		public ColumnStyle this[int index]
		{
			get
			{
				return (ColumnStyle)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
