using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Represents a sorted collection of <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> objects in the <see cref="T:System.Windows.Forms.NumericUpDown" /> control.</summary>
	// Token: 0x0200027A RID: 634
	[ListBindable(false)]
	public class NumericUpDownAccelerationCollection : MarshalByRefObject, ICollection<NumericUpDownAcceleration>, IEnumerable<NumericUpDownAcceleration>, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" /> class.</summary>
		// Token: 0x0600296B RID: 10603 RVA: 0x0009FDE4 File Offset: 0x0009DFE4
		public NumericUpDownAccelerationCollection()
		{
			this.items = new List<NumericUpDownAcceleration>();
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x0009FDF8 File Offset: 0x0009DFF8
		IEnumerator<NumericUpDownAcceleration> IEnumerable<NumericUpDownAcceleration>.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		/// <summary>Gets the enumerator for the collection.</summary>
		// Token: 0x0600296D RID: 10605 RVA: 0x0009FE0C File Offset: 0x0009E00C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		/// <summary>Gets the number of objects in the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />.</summary>
		/// <returns>The number of objects in the collection.</returns>
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x0600296E RID: 10606 RVA: 0x0009FE20 File Offset: 0x0009E020
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" /> is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x0600296F RID: 10607 RVA: 0x0009FE30 File Offset: 0x0009E030
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> at the specified index number.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> with the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> to get from the collection.</param>
		// Token: 0x17000A20 RID: 2592
		public NumericUpDownAcceleration this[int index]
		{
			get
			{
				return this.items[index];
			}
		}

		/// <summary>Adds a new <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> to the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />.</summary>
		/// <param name="acceleration">The <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> to add to the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="acceleration" /> is null.</exception>
		// Token: 0x06002971 RID: 10609 RVA: 0x0009FE44 File Offset: 0x0009E044
		public void Add(NumericUpDownAcceleration acceleration)
		{
			if (acceleration == null)
			{
				throw new ArgumentNullException("Acceleration cannot be null");
			}
			int i;
			for (i = 0; i < this.items.Count; i++)
			{
				if (acceleration.Seconds < this.items[i].Seconds)
				{
					break;
				}
			}
			this.items.Insert(i, acceleration);
		}

		/// <summary>Adds the elements of the specified array to the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />, keeping the collection sorted.</summary>
		/// <param name="accelerations">An array of type <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" />  containing the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="accelerations" /> is null, or one of the entries in the <paramref name="accelerations" /> array is null.</exception>
		// Token: 0x06002972 RID: 10610 RVA: 0x0009FEAC File Offset: 0x0009E0AC
		public void AddRange(params NumericUpDownAcceleration[] accelerations)
		{
			for (int i = 0; i < accelerations.Length; i++)
			{
				this.Add(accelerations[i]);
			}
		}

		/// <summary>Removes all elements from the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />.</summary>
		// Token: 0x06002973 RID: 10611 RVA: 0x0009FED8 File Offset: 0x0009E0D8
		public void Clear()
		{
			this.items.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" /> contains a specific <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" />.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> is found in the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />; otherwise, false.</returns>
		/// <param name="acceleration">The <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> to locate in the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />.</param>
		// Token: 0x06002974 RID: 10612 RVA: 0x0009FEE8 File Offset: 0x0009E0E8
		public bool Contains(NumericUpDownAcceleration acceleration)
		{
			return this.items.Contains(acceleration);
		}

		/// <summary>Copies the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" /> values to a one-dimensional <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> that is the destination of the values copied from <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />. </param>
		/// <param name="index">The index in <paramref name="array" /> where copying begins.</param>
		// Token: 0x06002975 RID: 10613 RVA: 0x0009FEF8 File Offset: 0x0009E0F8
		public void CopyTo(NumericUpDownAcceleration[] array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Removes the first occurrence of the specified <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> from the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> is removed from <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />; otherwise, false.</returns>
		/// <param name="acceleration">The <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> to remove from the collection.</param>
		// Token: 0x06002976 RID: 10614 RVA: 0x0009FF08 File Offset: 0x0009E108
		public bool Remove(NumericUpDownAcceleration acceleration)
		{
			return this.items.Remove(acceleration);
		}

		// Token: 0x0400149A RID: 5274
		private List<NumericUpDownAcceleration> items;
	}
}
