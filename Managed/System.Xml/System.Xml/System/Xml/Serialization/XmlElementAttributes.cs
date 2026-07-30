using System;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Represents a collection of <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> objects used by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> to override the default way it serializes a class.</summary>
	// Token: 0x02000331 RID: 817
	public class XmlElementAttributes : CollectionBase
	{
		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a valid index in the <see cref="T:System.Xml.Serialization.XmlElementAttributes" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Xml.Serialization.XmlElementAttributes" /> is read-only. </exception>
		// Token: 0x17000664 RID: 1636
		public XmlElementAttribute this[int index]
		{
			get
			{
				return (XmlElementAttribute)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds an <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> to the collection.</summary>
		/// <returns>The zero-based index of the newly added item.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> to add. </param>
		// Token: 0x06001F54 RID: 8020 RVA: 0x000A6A8A File Offset: 0x000A4C8A
		public int Add(XmlElementAttribute attribute)
		{
			return base.List.Add(attribute);
		}

		/// <summary>Inserts an <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> into the collection.</summary>
		/// <param name="index">The zero-based index where the member is inserted. </param>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> to insert. </param>
		// Token: 0x06001F55 RID: 8021 RVA: 0x000A6A98 File Offset: 0x000A4C98
		public void Insert(int index, XmlElementAttribute attribute)
		{
			base.List.Insert(index, attribute);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Xml.Serialization.XmlElementAttribute" />.</summary>
		/// <returns>The zero-based index of the <see cref="T:System.Xml.Serialization.XmlElementAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> whose index is being retrieved.</param>
		// Token: 0x06001F56 RID: 8022 RVA: 0x000A6AA7 File Offset: 0x000A4CA7
		public int IndexOf(XmlElementAttribute attribute)
		{
			return base.List.IndexOf(attribute);
		}

		/// <summary>Determines whether the collection contains the specified object.</summary>
		/// <returns>true if the object exists in the collection; otherwise, false.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> to look for. </param>
		// Token: 0x06001F57 RID: 8023 RVA: 0x000A6AB5 File Offset: 0x000A4CB5
		public bool Contains(XmlElementAttribute attribute)
		{
			return base.List.Contains(attribute);
		}

		/// <summary>Removes the specified object from the collection.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> to remove from the collection. </param>
		// Token: 0x06001F58 RID: 8024 RVA: 0x000A6AC3 File Offset: 0x000A4CC3
		public void Remove(XmlElementAttribute attribute)
		{
			base.List.Remove(attribute);
		}

		/// <summary>Copies the <see cref="T:System.Xml.Serialization.XmlElementAttributes" />, or a portion of it to a one-dimensional array.</summary>
		/// <param name="array">The <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> array to hold the copied elements. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		// Token: 0x06001F59 RID: 8025 RVA: 0x000A6AD1 File Offset: 0x000A4CD1
		public void CopyTo(XmlElementAttribute[] array, int index)
		{
			base.List.CopyTo(array, index);
		}
	}
}
