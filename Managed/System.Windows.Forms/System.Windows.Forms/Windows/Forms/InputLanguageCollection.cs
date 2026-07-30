using System;
using System.Collections;

namespace System.Windows.Forms
{
	/// <summary>Stores <see cref="T:System.Windows.Forms.InputLanguage" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E5 RID: 485
	public class InputLanguageCollection : ReadOnlyCollectionBase
	{
		// Token: 0x06001E9B RID: 7835 RVA: 0x00072FA4 File Offset: 0x000711A4
		internal InputLanguageCollection(InputLanguage[] data)
		{
			base.InnerList.AddRange(data);
		}

		/// <summary>Gets the entry at the specified index of the <see cref="T:System.Windows.Forms.InputLanguageCollection" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.InputLanguage" /> at the specified index of the collection.</returns>
		/// <param name="index">The zero-based index of the entry to locate in the collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000780 RID: 1920
		public InputLanguage this[int index]
		{
			get
			{
				if (index >= base.InnerList.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return base.InnerList[index] as InputLanguage;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.InputLanguageCollection" /> contains the specified <see cref="T:System.Windows.Forms.InputLanguage" />.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.InputLanguage" /> is contained in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.InputLanguage" /> to locate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E9D RID: 7837 RVA: 0x00072FF4 File Offset: 0x000711F4
		public bool Contains(InputLanguage value)
		{
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				if (this[i].Culture == value.Culture && this[i].LayoutName == value.LayoutName)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Windows.Forms.InputLanguageCollection" /> values to a one-dimensional <see cref="T:System.Array" /> at the specified index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the values copied from <see cref="T:System.Windows.Forms.InputLanguageCollection" />. </param>
		/// <param name="index">The index in <paramref name="array" /> where copying begins. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> specifies a multidimensional array.-or- The number of elements in the <see cref="T:System.Windows.Forms.InputLanguageCollection" /> is greater than the available space between the <paramref name="index" /> and the end of <paramref name="array" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E9E RID: 7838 RVA: 0x00073054 File Offset: 0x00071254
		public void CopyTo(InputLanguage[] array, int index)
		{
			if (base.InnerList.Count > 0)
			{
				base.InnerList.CopyTo(array, index);
			}
		}

		/// <summary>Returns the index of an <see cref="T:System.Windows.Forms.InputLanguage" /> in the <see cref="T:System.Windows.Forms.InputLanguageCollection" />.</summary>
		/// <returns>The index of the <see cref="T:System.Windows.Forms.InputLanguage" /> in the <see cref="T:System.Windows.Forms.InputLanguageCollection" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Windows.Forms.InputLanguage" /> to locate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001E9F RID: 7839 RVA: 0x00073080 File Offset: 0x00071280
		public int IndexOf(InputLanguage value)
		{
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				if (this[i].Culture == value.Culture && this[i].LayoutName == value.LayoutName)
				{
					return i;
				}
			}
			return -1;
		}
	}
}
