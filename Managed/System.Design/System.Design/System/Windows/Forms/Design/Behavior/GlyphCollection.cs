using System;
using System.Collections;

namespace System.Windows.Forms.Design.Behavior
{
	/// <summary>Stores <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> objects in a strongly typed collection.</summary>
	// Token: 0x0200004B RID: 75
	public class GlyphCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> class.</summary>
		// Token: 0x06000283 RID: 643 RVA: 0x00008C76 File Offset: 0x00006E76
		public GlyphCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> class with the given <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> array.</summary>
		/// <param name="value">An array of type <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> to populate the collection.</param>
		// Token: 0x06000284 RID: 644 RVA: 0x00008A37 File Offset: 0x00006C37
		public GlyphCollection(Glyph[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			base.InnerList.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> class based on another <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" />.</summary>
		/// <param name="value">A <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> to populate the collection.</param>
		// Token: 0x06000285 RID: 645 RVA: 0x00008A37 File Offset: 0x00006C37
		public GlyphCollection(GlyphCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			base.InnerList.AddRange(value);
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x1700007E RID: 126
		public Glyph this[int index]
		{
			get
			{
				return (Glyph)base.InnerList[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.InnerList[index] = value;
			}
		}

		/// <summary>Adds a <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> with the specified value to the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" />.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> to add to the end of the collection.</param>
		// Token: 0x06000288 RID: 648 RVA: 0x00008C91 File Offset: 0x00006E91
		public int Add(Glyph value)
		{
			return base.InnerList.Add(value);
		}

		/// <summary>Copies the elements of an array to the end of the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" />.</summary>
		/// <param name="value">An array of type <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> to copy to the end of the collection.</param>
		// Token: 0x06000289 RID: 649 RVA: 0x00008C9F File Offset: 0x00006E9F
		public void AddRange(Glyph[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			base.InnerList.AddRange(value);
		}

		/// <summary>Adds the contents of another <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> to add to the end of the collection.</param>
		// Token: 0x0600028A RID: 650 RVA: 0x00008C9F File Offset: 0x00006E9F
		public void AddRange(GlyphCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			base.InnerList.AddRange(value);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> contains the specified <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is contained in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> to locate.</param>
		// Token: 0x0600028B RID: 651 RVA: 0x00008AD7 File Offset: 0x00006CD7
		public bool Contains(Glyph value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>Copies the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> values to a one-dimensional <see cref="T:System.Array" /> at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" />.</param>
		/// <param name="index">The index in <paramref name="array" /> where copying begins. </param>
		// Token: 0x0600028C RID: 652 RVA: 0x00008AE5 File Offset: 0x00006CE5
		public void CopyTo(Glyph[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		/// <summary>Returns the index of a <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> in the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" />.</summary>
		/// <returns>The index of the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> of <paramref name="value" /> in the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> to locate.</param>
		// Token: 0x0600028D RID: 653 RVA: 0x00008AF4 File Offset: 0x00006CF4
		public int IndexOf(Glyph value)
		{
			return base.InnerList.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> into the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index where <paramref name="value" /> should be inserted. </param>
		/// <param name="value">The <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> to insert.</param>
		// Token: 0x0600028E RID: 654 RVA: 0x00008CBB File Offset: 0x00006EBB
		public void Insert(int index, Glyph value)
		{
			base.InnerList.Insert(index, value);
		}

		/// <summary>Removes a specific <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> from the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> to remove from the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" />.</param>
		// Token: 0x0600028F RID: 655 RVA: 0x00008CCA File Offset: 0x00006ECA
		public void Remove(Glyph value)
		{
			base.InnerList.Remove(value);
		}
	}
}
