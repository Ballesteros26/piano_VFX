using System;
using System.Collections;

namespace System.Windows.Forms.Design.Behavior
{
	/// <summary>Stores <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> objects in a strongly typed collection.</summary>
	// Token: 0x02000046 RID: 70
	public sealed class BehaviorServiceAdornerCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> class with the given <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> reference.</summary>
		/// <param name="behaviorService">A <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> reference. </param>
		// Token: 0x0600025D RID: 605 RVA: 0x00008A29 File Offset: 0x00006C29
		public BehaviorServiceAdornerCollection(BehaviorService behaviorService)
			: this(behaviorService.Adorners)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> class with the given array.</summary>
		/// <param name="value">An array of type <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" />  to populate the collection.</param>
		// Token: 0x0600025E RID: 606 RVA: 0x00008A37 File Offset: 0x00006C37
		public BehaviorServiceAdornerCollection(Adorner[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			base.InnerList.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> class from an existing <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</summary>
		/// <param name="value">A <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> from which to populate the collection. </param>
		// Token: 0x0600025F RID: 607 RVA: 0x00008A37 File Offset: 0x00006C37
		public BehaviorServiceAdornerCollection(BehaviorServiceAdornerCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			base.InnerList.AddRange(value);
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00008A59 File Offset: 0x00006C59
		internal int State
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> element specified by <paramref name="index" />.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x17000077 RID: 119
		public Adorner this[int index]
		{
			get
			{
				return (Adorner)base.InnerList[index];
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

		/// <summary>Adds an <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> with the specified value to the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">An <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> to add to the end of the collection.</param>
		// Token: 0x06000263 RID: 611 RVA: 0x00008A91 File Offset: 0x00006C91
		public int Add(Adorner value)
		{
			this.state++;
			return base.InnerList.Add(value);
		}

		/// <summary>Copies the elements of an array to the end of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</summary>
		/// <param name="value">An array of type <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> to copy to the end of the collection</param>
		// Token: 0x06000264 RID: 612 RVA: 0x00008AAD File Offset: 0x00006CAD
		public void AddRange(Adorner[] value)
		{
			this.state++;
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			base.InnerList.AddRange(value);
		}

		/// <summary>Adds the contents of another <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> to add to the end of the collection.</param>
		// Token: 0x06000265 RID: 613 RVA: 0x00008AAD File Offset: 0x00006CAD
		public void AddRange(BehaviorServiceAdornerCollection value)
		{
			this.state++;
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			base.InnerList.AddRange(value);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> contains the specified <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" />.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> is contained in the collection; otherwise, false</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> to locate.</param>
		// Token: 0x06000266 RID: 614 RVA: 0x00008AD7 File Offset: 0x00006CD7
		public bool Contains(Adorner value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>Copies the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> values to a one-dimensional <see cref="T:System.Array" /> at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</param>
		/// <param name="index">The index in <paramref name="array" /> where copying begins.</param>
		// Token: 0x06000267 RID: 615 RVA: 0x00008AE5 File Offset: 0x00006CE5
		public void CopyTo(Adorner[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		/// <summary>Returns the index of an <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> in the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</summary>
		/// <returns>The index of the <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> of <paramref name="value" /> in the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> to locate.</param>
		// Token: 0x06000268 RID: 616 RVA: 0x00008AF4 File Offset: 0x00006CF4
		public int IndexOf(Adorner value)
		{
			return base.InnerList.IndexOf(value);
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> instance.</returns>
		// Token: 0x06000269 RID: 617 RVA: 0x00008B02 File Offset: 0x00006D02
		public new BehaviorServiceAdornerCollectionEnumerator GetEnumerator()
		{
			return new BehaviorServiceAdornerCollectionEnumerator(this);
		}

		/// <summary>Inserts an <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> into the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index where <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> to insert.</param>
		// Token: 0x0600026A RID: 618 RVA: 0x00008B0A File Offset: 0x00006D0A
		public void Insert(int index, Adorner value)
		{
			this.state++;
			base.InnerList.Insert(index, value);
		}

		/// <summary>Removes a specific <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> from the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> to remove from the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</param>
		// Token: 0x0600026B RID: 619 RVA: 0x00008B27 File Offset: 0x00006D27
		public void Remove(Adorner value)
		{
			this.state++;
			base.InnerList.Remove(value);
		}

		// Token: 0x040000FA RID: 250
		private int state;
	}
}
