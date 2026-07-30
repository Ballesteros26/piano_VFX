using System;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Represents a collection of <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> objects.</summary>
	// Token: 0x02000328 RID: 808
	public class XmlArrayItemAttributes : CollectionBase
	{
		/// <summary>Gets or sets the item at the specified index.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the collection member to get or set. </param>
		// Token: 0x17000621 RID: 1569
		public XmlArrayItemAttribute this[int index]
		{
			get
			{
				return (XmlArrayItemAttribute)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds an <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> to the collection.</summary>
		/// <returns>The index of the added item.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> to add to the collection. </param>
		// Token: 0x06001E76 RID: 7798 RVA: 0x000A6A8A File Offset: 0x000A4C8A
		public int Add(XmlArrayItemAttribute attribute)
		{
			return base.List.Add(attribute);
		}

		/// <summary>Inserts an <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> into the collection at the specified index. </summary>
		/// <param name="index">The index at which the attribute is inserted.</param>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" />  to insert.</param>
		// Token: 0x06001E77 RID: 7799 RVA: 0x000A6A98 File Offset: 0x000A4C98
		public void Insert(int index, XmlArrayItemAttribute attribute)
		{
			base.List.Insert(index, attribute);
		}

		/// <summary>Returns the zero-based index of the first occurrence of the specified <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> in the collection or -1 if the attribute is not found in the collection. </summary>
		/// <returns>The first index of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> in the collection or -1 if the attribute is not found in the collection.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> to locate in the collection.</param>
		// Token: 0x06001E78 RID: 7800 RVA: 0x000A6AA7 File Offset: 0x000A4CA7
		public int IndexOf(XmlArrayItemAttribute attribute)
		{
			return base.List.IndexOf(attribute);
		}

		/// <summary>Determines whether the collection contains the specified <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" />. </summary>
		/// <returns>true if the collection contains the specified <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" />; otherwise, false.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> to check for.</param>
		// Token: 0x06001E79 RID: 7801 RVA: 0x000A6AB5 File Offset: 0x000A4CB5
		public bool Contains(XmlArrayItemAttribute attribute)
		{
			return base.List.Contains(attribute);
		}

		/// <summary>Removes an <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> from the collection, if it is present. </summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> to remove.</param>
		// Token: 0x06001E7A RID: 7802 RVA: 0x000A6AC3 File Offset: 0x000A4CC3
		public void Remove(XmlArrayItemAttribute attribute)
		{
			base.List.Remove(attribute);
		}

		/// <summary>Copies an <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> array to the collection, starting at a specified target index. </summary>
		/// <param name="array">The array of <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> objects to copy to the collection.</param>
		/// <param name="index">The index at which the copied attributes begin.</param>
		// Token: 0x06001E7B RID: 7803 RVA: 0x000A6AD1 File Offset: 0x000A4CD1
		public void CopyTo(XmlArrayItemAttribute[] array, int index)
		{
			base.List.CopyTo(array, index);
		}
	}
}
