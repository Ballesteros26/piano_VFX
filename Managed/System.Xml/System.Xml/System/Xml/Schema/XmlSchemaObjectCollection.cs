using System;
using System.Collections;

namespace System.Xml.Schema
{
	/// <summary>A collection of <see cref="T:System.Xml.Schema.XmlSchemaObject" />s.</summary>
	// Token: 0x0200046E RID: 1134
	public class XmlSchemaObjectCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the XmlSchemaObjectCollection class.</summary>
		// Token: 0x06002CAA RID: 11434 RVA: 0x000A6AE0 File Offset: 0x000A4CE0
		public XmlSchemaObjectCollection()
		{
		}

		/// <summary>Initializes a new instance of the XmlSchemaObjectCollection class that takes an <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</summary>
		/// <param name="parent">The <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		// Token: 0x06002CAB RID: 11435 RVA: 0x001073A8 File Offset: 0x001055A8
		public XmlSchemaObjectCollection(XmlSchemaObject parent)
		{
			this.parent = parent;
		}

		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaObject" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		// Token: 0x170009BB RID: 2491
		public virtual XmlSchemaObject this[int index]
		{
			get
			{
				return (XmlSchemaObject)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Returns an enumerator for iterating through the XmlSchemaObjects contained in the XmlSchemaObjectCollection.</summary>
		/// <returns>The iterator returns <see cref="T:System.Xml.Schema.XmlSchemaObjectEnumerator" />.</returns>
		// Token: 0x06002CAE RID: 11438 RVA: 0x001073CA File Offset: 0x001055CA
		public new XmlSchemaObjectEnumerator GetEnumerator()
		{
			return new XmlSchemaObjectEnumerator(base.InnerList.GetEnumerator());
		}

		/// <summary>Adds an <see cref="T:System.Xml.Schema.XmlSchemaObject" /> to the XmlSchemaObjectCollection.</summary>
		/// <returns>The index at which the item has been added.</returns>
		/// <param name="item">The <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than Count. </exception>
		/// <exception cref="T:System.InvalidCastException">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> parameter specified is not of type <see cref="T:System.Xml.Schema.XmlSchemaExternal" /> or its derived types <see cref="T:System.Xml.Schema.XmlSchemaImport" />, <see cref="T:System.Xml.Schema.XmlSchemaInclude" />, and <see cref="T:System.Xml.Schema.XmlSchemaRedefine" />.</exception>
		// Token: 0x06002CAF RID: 11439 RVA: 0x000A6A8A File Offset: 0x000A4C8A
		public int Add(XmlSchemaObject item)
		{
			return base.List.Add(item);
		}

		/// <summary>Inserts an <see cref="T:System.Xml.Schema.XmlSchemaObject" /> to the XmlSchemaObjectCollection.</summary>
		/// <param name="index">The zero-based index at which an item should be inserted. </param>
		/// <param name="item">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than Count. </exception>
		// Token: 0x06002CB0 RID: 11440 RVA: 0x000A6A98 File Offset: 0x000A4C98
		public void Insert(int index, XmlSchemaObject item)
		{
			base.List.Insert(index, item);
		}

		/// <summary>Gets the collection index corresponding to the specified <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</summary>
		/// <returns>The index corresponding to the specified <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</returns>
		/// <param name="item">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> whose index you want to return. </param>
		// Token: 0x06002CB1 RID: 11441 RVA: 0x000A6AA7 File Offset: 0x000A4CA7
		public int IndexOf(XmlSchemaObject item)
		{
			return base.List.IndexOf(item);
		}

		/// <summary>Indicates if the specified <see cref="T:System.Xml.Schema.XmlSchemaObject" /> is in the XmlSchemaObjectCollection.</summary>
		/// <returns>true if the specified qualified name is in the collection; otherwise, returns false. If null is supplied, false is returned because there is no qualified name with a null name.</returns>
		/// <param name="item">The <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		// Token: 0x06002CB2 RID: 11442 RVA: 0x000A6AB5 File Offset: 0x000A4CB5
		public bool Contains(XmlSchemaObject item)
		{
			return base.List.Contains(item);
		}

		/// <summary>Removes an <see cref="T:System.Xml.Schema.XmlSchemaObject" /> from the XmlSchemaObjectCollection.</summary>
		/// <param name="item">The <see cref="T:System.Xml.Schema.XmlSchemaObject" /> to remove. </param>
		// Token: 0x06002CB3 RID: 11443 RVA: 0x000A6AC3 File Offset: 0x000A4CC3
		public void Remove(XmlSchemaObject item)
		{
			base.List.Remove(item);
		}

		/// <summary>Copies all the <see cref="T:System.Xml.Schema.XmlSchemaObject" />s from the collection into the given array, starting at the given index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the XmlSchemaObjectCollection. The array must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in the array at which copying begins. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multi-dimensional.- or - <paramref name="index" /> is equal to or greater than the length of <paramref name="array" />.- or - The number of elements in the source <see cref="T:System.Xml.Schema.XmlSchemaObject" /> is greater than the available space from index to the end of the destination array. </exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Xml.Schema.XmlSchemaObject" /> cannot be cast automatically to the type of the destination array. </exception>
		// Token: 0x06002CB4 RID: 11444 RVA: 0x000A6AD1 File Offset: 0x000A4CD1
		public void CopyTo(XmlSchemaObject[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>OnInsert is invoked before the standard Insert behavior. For more information, see OnInsert method <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <param name="index">The index of <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		/// <param name="item">The item. </param>
		// Token: 0x06002CB5 RID: 11445 RVA: 0x001073DC File Offset: 0x001055DC
		protected override void OnInsert(int index, object item)
		{
			if (this.parent != null)
			{
				this.parent.OnAdd(this, item);
			}
		}

		/// <summary>OnSet is invoked before the standard Set behavior. For more information, see the OnSet method for <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <param name="index">The index of <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		/// <param name="oldValue">The old value. </param>
		/// <param name="newValue">The new value. </param>
		// Token: 0x06002CB6 RID: 11446 RVA: 0x001073F3 File Offset: 0x001055F3
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			if (this.parent != null)
			{
				this.parent.OnRemove(this, oldValue);
				this.parent.OnAdd(this, newValue);
			}
		}

		/// <summary>OnClear is invoked before the standard Clear behavior. For more information, see OnClear method for <see cref="T:System.Collections.CollectionBase" />.</summary>
		// Token: 0x06002CB7 RID: 11447 RVA: 0x00107417 File Offset: 0x00105617
		protected override void OnClear()
		{
			if (this.parent != null)
			{
				this.parent.OnClear(this);
			}
		}

		/// <summary>OnRemove is invoked before the standard Remove behavior. For more information, see the OnRemove method for <see cref="T:System.Collections.CollectionBase" />.</summary>
		/// <param name="index">The index of <see cref="T:System.Xml.Schema.XmlSchemaObject" />. </param>
		/// <param name="item">The item. </param>
		// Token: 0x06002CB8 RID: 11448 RVA: 0x0010742D File Offset: 0x0010562D
		protected override void OnRemove(int index, object item)
		{
			if (this.parent != null)
			{
				this.parent.OnRemove(this, item);
			}
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x00107444 File Offset: 0x00105644
		internal XmlSchemaObjectCollection Clone()
		{
			return new XmlSchemaObjectCollection { this };
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x00107452 File Offset: 0x00105652
		private void Add(XmlSchemaObjectCollection collToAdd)
		{
			base.InnerList.InsertRange(0, collToAdd);
		}

		// Token: 0x04001DDE RID: 7646
		private XmlSchemaObject parent;
	}
}
