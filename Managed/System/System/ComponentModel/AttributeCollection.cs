using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of attributes.</summary>
	// Token: 0x0200022E RID: 558
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class AttributeCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeCollection" /> class.</summary>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that provides the attributes for this collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributes" /> is null.</exception>
		// Token: 0x060011FD RID: 4605 RVA: 0x0004CD40 File Offset: 0x0004AF40
		public AttributeCollection(params Attribute[] attributes)
		{
			if (attributes == null)
			{
				attributes = new Attribute[0];
			}
			this._attributes = attributes;
			for (int i = 0; i < attributes.Length; i++)
			{
				if (attributes[i] == null)
				{
					throw new ArgumentNullException("attributes");
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeCollection" /> class. </summary>
		// Token: 0x060011FE RID: 4606 RVA: 0x000020EB File Offset: 0x000002EB
		protected AttributeCollection()
		{
		}

		/// <summary>Creates a new <see cref="T:System.ComponentModel.AttributeCollection" /> from an existing <see cref="T:System.ComponentModel.AttributeCollection" />.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.AttributeCollection" /> that is a copy of <paramref name="existing" />.</returns>
		/// <param name="existing">An <see cref="T:System.ComponentModel.AttributeCollection" /> from which to create the copy.</param>
		/// <param name="newAttributes">An array of type <see cref="T:System.Attribute" /> that provides the attributes for this collection. Can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="existing" /> is null.</exception>
		// Token: 0x060011FF RID: 4607 RVA: 0x0004CD84 File Offset: 0x0004AF84
		public static AttributeCollection FromExisting(AttributeCollection existing, params Attribute[] newAttributes)
		{
			if (existing == null)
			{
				throw new ArgumentNullException("existing");
			}
			if (newAttributes == null)
			{
				newAttributes = new Attribute[0];
			}
			Attribute[] array = new Attribute[existing.Count + newAttributes.Length];
			int count = existing.Count;
			existing.CopyTo(array, 0);
			for (int i = 0; i < newAttributes.Length; i++)
			{
				if (newAttributes[i] == null)
				{
					throw new ArgumentNullException("newAttributes");
				}
				bool flag = false;
				for (int j = 0; j < existing.Count; j++)
				{
					if (array[j].TypeId.Equals(newAttributes[i].TypeId))
					{
						flag = true;
						array[j] = newAttributes[i];
						break;
					}
				}
				if (!flag)
				{
					array[count++] = newAttributes[i];
				}
			}
			Attribute[] array2;
			if (count < array.Length)
			{
				array2 = new Attribute[count];
				Array.Copy(array, 0, array2, 0, count);
			}
			else
			{
				array2 = array;
			}
			return new AttributeCollection(array2);
		}

		/// <summary>Gets the attribute collection.</summary>
		/// <returns>The attribute collection.</returns>
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0004CE54 File Offset: 0x0004B054
		protected virtual Attribute[] Attributes
		{
			get
			{
				return this._attributes;
			}
		}

		/// <summary>Gets the number of attributes.</summary>
		/// <returns>The number of attributes.</returns>
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x0004CE5C File Offset: 0x0004B05C
		public int Count
		{
			get
			{
				return this.Attributes.Length;
			}
		}

		/// <summary>Gets the attribute with the specified index number.</summary>
		/// <returns>The <see cref="T:System.Attribute" /> with the specified index number.</returns>
		/// <param name="index">The zero-based index of <see cref="T:System.ComponentModel.AttributeCollection" />. </param>
		// Token: 0x170003A6 RID: 934
		public virtual Attribute this[int index]
		{
			get
			{
				return this.Attributes[index];
			}
		}

		/// <summary>Gets the attribute with the specified type.</summary>
		/// <returns>The <see cref="T:System.Attribute" /> with the specified type or, if the attribute does not exist, the default value for the attribute type.</returns>
		/// <param name="attributeType">The <see cref="T:System.Type" /> of the <see cref="T:System.Attribute" /> to get from the collection. </param>
		// Token: 0x170003A7 RID: 935
		public virtual Attribute this[Type attributeType]
		{
			get
			{
				object obj = AttributeCollection.internalSyncObject;
				Attribute defaultAttribute;
				lock (obj)
				{
					if (this._foundAttributeTypes == null)
					{
						this._foundAttributeTypes = new AttributeCollection.AttributeEntry[5];
					}
					int i = 0;
					while (i < 5)
					{
						if (this._foundAttributeTypes[i].type == attributeType)
						{
							int index = this._foundAttributeTypes[i].index;
							if (index != -1)
							{
								return this.Attributes[index];
							}
							return this.GetDefaultAttribute(attributeType);
						}
						else
						{
							if (this._foundAttributeTypes[i].type == null)
							{
								break;
							}
							i++;
						}
					}
					int index2 = this._index;
					this._index = index2 + 1;
					i = index2;
					if (this._index >= 5)
					{
						this._index = 0;
					}
					this._foundAttributeTypes[i].type = attributeType;
					int num = this.Attributes.Length;
					for (int j = 0; j < num; j++)
					{
						Attribute attribute = this.Attributes[j];
						if (attribute.GetType() == attributeType)
						{
							this._foundAttributeTypes[i].index = j;
							return attribute;
						}
					}
					for (int k = 0; k < num; k++)
					{
						Attribute attribute2 = this.Attributes[k];
						Type type = attribute2.GetType();
						if (attributeType.IsAssignableFrom(type))
						{
							this._foundAttributeTypes[i].index = k;
							return attribute2;
						}
					}
					this._foundAttributeTypes[i].index = -1;
					defaultAttribute = this.GetDefaultAttribute(attributeType);
				}
				return defaultAttribute;
			}
		}

		/// <summary>Determines whether this collection of attributes has the specified attribute.</summary>
		/// <returns>true if the collection contains the attribute or is the default attribute for the type of attribute; otherwise, false.</returns>
		/// <param name="attribute">An <see cref="T:System.Attribute" /> to find in the collection. </param>
		// Token: 0x06001204 RID: 4612 RVA: 0x0004D024 File Offset: 0x0004B224
		public bool Contains(Attribute attribute)
		{
			Attribute attribute2 = this[attribute.GetType()];
			return attribute2 != null && attribute2.Equals(attribute);
		}

		/// <summary>Determines whether this attribute collection contains all the specified attributes in the attribute array.</summary>
		/// <returns>true if the collection contains all the attributes; otherwise, false.</returns>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> to find in the collection. </param>
		// Token: 0x06001205 RID: 4613 RVA: 0x0004D050 File Offset: 0x0004B250
		public bool Contains(Attribute[] attributes)
		{
			if (attributes == null)
			{
				return true;
			}
			for (int i = 0; i < attributes.Length; i++)
			{
				if (!this.Contains(attributes[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns the default <see cref="T:System.Attribute" /> of a given <see cref="T:System.Type" />.</summary>
		/// <returns>The default <see cref="T:System.Attribute" /> of a given <paramref name="attributeType" />.</returns>
		/// <param name="attributeType">The <see cref="T:System.Type" /> of the attribute to retrieve. </param>
		// Token: 0x06001206 RID: 4614 RVA: 0x0004D080 File Offset: 0x0004B280
		protected Attribute GetDefaultAttribute(Type attributeType)
		{
			object obj = AttributeCollection.internalSyncObject;
			Attribute attribute;
			lock (obj)
			{
				if (AttributeCollection._defaultAttributes == null)
				{
					AttributeCollection._defaultAttributes = new Hashtable();
				}
				if (AttributeCollection._defaultAttributes.ContainsKey(attributeType))
				{
					attribute = (Attribute)AttributeCollection._defaultAttributes[attributeType];
				}
				else
				{
					Attribute attribute2 = null;
					Type reflectionType = TypeDescriptor.GetReflectionType(attributeType);
					FieldInfo field = reflectionType.GetField("Default", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField);
					if (field != null && field.IsStatic)
					{
						attribute2 = (Attribute)field.GetValue(null);
					}
					else
					{
						ConstructorInfo constructor = reflectionType.UnderlyingSystemType.GetConstructor(new Type[0]);
						if (constructor != null)
						{
							attribute2 = (Attribute)constructor.Invoke(new object[0]);
							if (!attribute2.IsDefaultAttribute())
							{
								attribute2 = null;
							}
						}
					}
					AttributeCollection._defaultAttributes[attributeType] = attribute2;
					attribute = attribute2;
				}
			}
			return attribute;
		}

		/// <summary>Gets an enumerator for this collection.</summary>
		/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x06001207 RID: 4615 RVA: 0x0004D178 File Offset: 0x0004B378
		public IEnumerator GetEnumerator()
		{
			return this.Attributes.GetEnumerator();
		}

		/// <summary>Determines whether a specified attribute is the same as an attribute in the collection.</summary>
		/// <returns>true if the attribute is contained within the collection and has the same value as the attribute in the collection; otherwise, false.</returns>
		/// <param name="attribute">An instance of <see cref="T:System.Attribute" /> to compare with the attributes in this collection. </param>
		// Token: 0x06001208 RID: 4616 RVA: 0x0004D188 File Offset: 0x0004B388
		public bool Matches(Attribute attribute)
		{
			for (int i = 0; i < this.Attributes.Length; i++)
			{
				if (this.Attributes[i].Match(attribute))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the attributes in the specified array are the same as the attributes in the collection.</summary>
		/// <returns>true if all the attributes in the array are contained in the collection and have the same values as the attributes in the collection; otherwise, false.</returns>
		/// <param name="attributes">An array of <see cref="T:System.CodeDom.MemberAttributes" /> to compare with the attributes in this collection. </param>
		// Token: 0x06001209 RID: 4617 RVA: 0x0004D1BC File Offset: 0x0004B3BC
		public bool Matches(Attribute[] attributes)
		{
			for (int i = 0; i < attributes.Length; i++)
			{
				if (!this.Matches(attributes[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x0004D1E5 File Offset: 0x0004B3E5
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>true if access to the collection is synchronized (thread-safe); otherwise, false.</returns>
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x00004240 File Offset: 0x00002440
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x00009E57 File Offset: 0x00008057
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Copies the collection to an array, starting at the specified index.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> to copy the collection to. </param>
		/// <param name="index">The index to start from. </param>
		// Token: 0x0600120D RID: 4621 RVA: 0x0004D1ED File Offset: 0x0004B3ED
		public void CopyTo(Array array, int index)
		{
			Array.Copy(this.Attributes, 0, array, index, this.Attributes.Length);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />. </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x0600120E RID: 4622 RVA: 0x0004D205 File Offset: 0x0004B405
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Specifies an empty collection that you can use, rather than creating a new one. This field is read-only.</summary>
		// Token: 0x04001233 RID: 4659
		public static readonly AttributeCollection Empty = new AttributeCollection(null);

		// Token: 0x04001234 RID: 4660
		private static Hashtable _defaultAttributes;

		// Token: 0x04001235 RID: 4661
		private Attribute[] _attributes;

		// Token: 0x04001236 RID: 4662
		private static object internalSyncObject = new object();

		// Token: 0x04001237 RID: 4663
		private const int FOUND_TYPES_LIMIT = 5;

		// Token: 0x04001238 RID: 4664
		private AttributeCollection.AttributeEntry[] _foundAttributeTypes;

		// Token: 0x04001239 RID: 4665
		private int _index;

		// Token: 0x0200022F RID: 559
		private struct AttributeEntry
		{
			// Token: 0x0400123A RID: 4666
			public Type type;

			// Token: 0x0400123B RID: 4667
			public int index;
		}
	}
}
