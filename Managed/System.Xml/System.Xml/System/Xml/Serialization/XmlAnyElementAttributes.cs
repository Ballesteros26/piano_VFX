using System;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Represents a collection of <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> objects.</summary>
	// Token: 0x02000325 RID: 805
	public class XmlAnyElementAttributes : CollectionBase
	{
		/// <summary>Gets or sets the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> at the specified index.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" />. </param>
		// Token: 0x17000613 RID: 1555
		public XmlAnyElementAttribute this[int index]
		{
			get
			{
				return (XmlAnyElementAttribute)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds an <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> to the collection.</summary>
		/// <returns>The index of the newly added <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> to add. </param>
		// Token: 0x06001E4E RID: 7758 RVA: 0x000A6A8A File Offset: 0x000A4C8A
		public int Add(XmlAnyElementAttribute attribute)
		{
			return base.List.Add(attribute);
		}

		/// <summary>Inserts an <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> into the collection at the specified index.</summary>
		/// <param name="index">The index where the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> is inserted. </param>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> to insert. </param>
		// Token: 0x06001E4F RID: 7759 RVA: 0x000A6A98 File Offset: 0x000A4C98
		public void Insert(int index, XmlAnyElementAttribute attribute)
		{
			base.List.Insert(index, attribute);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" />.</summary>
		/// <returns>The index of the specified <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> whose index you want. </param>
		// Token: 0x06001E50 RID: 7760 RVA: 0x000A6AA7 File Offset: 0x000A4CA7
		public int IndexOf(XmlAnyElementAttribute attribute)
		{
			return base.List.IndexOf(attribute);
		}

		/// <summary>Gets a value that indicates whether the specified <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> exists in the collection.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> exists in the collection; otherwise, false.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> you are interested in. </param>
		// Token: 0x06001E51 RID: 7761 RVA: 0x000A6AB5 File Offset: 0x000A4CB5
		public bool Contains(XmlAnyElementAttribute attribute)
		{
			return base.List.Contains(attribute);
		}

		/// <summary>Removes the specified <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> from the collection.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> to remove. </param>
		// Token: 0x06001E52 RID: 7762 RVA: 0x000A6AC3 File Offset: 0x000A4CC3
		public void Remove(XmlAnyElementAttribute attribute)
		{
			base.List.Remove(attribute);
		}

		/// <summary>Copies the entire collection to a compatible one-dimensional array of <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> objects, starting at the specified index of the target array. </summary>
		/// <param name="array">The one-dimensional array of <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> objects that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06001E53 RID: 7763 RVA: 0x000A6AD1 File Offset: 0x000A4CD1
		public void CopyTo(XmlAnyElementAttribute[] array, int index)
		{
			base.List.CopyTo(array, index);
		}
	}
}
