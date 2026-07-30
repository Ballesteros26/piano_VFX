using System;

namespace System.Windows.Forms
{
	/// <summary>A collection that stores <see cref="T:System.Windows.Forms.RowStyle" /> objects.</summary>
	// Token: 0x02000309 RID: 777
	public class TableLayoutRowStyleCollection : TableLayoutStyleCollection
	{
		// Token: 0x060033B2 RID: 13234 RVA: 0x000C3CAC File Offset: 0x000C1EAC
		internal TableLayoutRowStyleCollection(TableLayoutPanel panel)
			: base(panel)
		{
		}

		/// <summary>Adds a new <see cref="T:System.Windows.Forms.RowStyle" /> to the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="rowStyle">The <see cref="T:System.Windows.Forms.RowStyle" /> to add to the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />.</param>
		// Token: 0x060033B3 RID: 13235 RVA: 0x000C3CB8 File Offset: 0x000C1EB8
		public int Add(RowStyle rowStyle)
		{
			return base.Add(rowStyle);
		}

		/// <summary>Determines whether the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" /> contains a specific style.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.RowStyle" /> is found in the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />; otherwise, false.</returns>
		/// <param name="rowStyle">The <see cref="T:System.Windows.Forms.RowStyle" /> to locate in the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />.</param>
		// Token: 0x060033B4 RID: 13236 RVA: 0x000C3CC4 File Offset: 0x000C1EC4
		public bool Contains(RowStyle rowStyle)
		{
			return this.Contains(rowStyle);
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />.</summary>
		/// <returns>The index of <paramref name="rowStyle" /> if found in the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />; otherwise, -1.</returns>
		/// <param name="rowStyle">The <see cref="T:System.Windows.Forms.RowStyle" /> to locate in the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />.</param>
		// Token: 0x060033B5 RID: 13237 RVA: 0x000C3CD0 File Offset: 0x000C1ED0
		public int IndexOf(RowStyle rowStyle)
		{
			return this.IndexOf(rowStyle);
		}

		/// <summary>Inserts a <see cref="T:System.Windows.Forms.RowStyle" /> into the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" /> at the specified position.</summary>
		/// <param name="index">The zero-based index at which the <see cref="T:System.Windows.Forms.RowStyle" /> should be inserted.</param>
		/// <param name="rowStyle">The <see cref="T:System.Windows.Forms.RowStyle" /> to insert into the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />. The value can be null.</param>
		// Token: 0x060033B6 RID: 13238 RVA: 0x000C3CDC File Offset: 0x000C1EDC
		public void Insert(int index, RowStyle rowStyle)
		{
			this.Insert(index, rowStyle);
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />.</summary>
		/// <param name="rowStyle">The <see cref="T:System.Windows.Forms.RowStyle" /> to remove from the <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" />. The value can be null.</param>
		// Token: 0x060033B7 RID: 13239 RVA: 0x000C3CE8 File Offset: 0x000C1EE8
		public void Remove(RowStyle rowStyle)
		{
			this.Remove(rowStyle);
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.RowStyle" /> at the specified index.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.RowStyle" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.RowStyle" /> to get or set.</param>
		// Token: 0x17000D7D RID: 3453
		public RowStyle this[int index]
		{
			get
			{
				return (RowStyle)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
