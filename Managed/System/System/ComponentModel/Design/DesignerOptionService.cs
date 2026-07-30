using System;
using System.Collections;
using System.Globalization;
using System.Security.Permissions;
using Unity;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a base class for getting and setting option values for a designer.</summary>
	// Token: 0x02000311 RID: 785
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class DesignerOptionService : IDesignerOptionService
	{
		/// <summary>Gets the options collection for this service.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> populated with available designer options.</returns>
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x00069539 File Offset: 0x00067739
		public DesignerOptionService.DesignerOptionCollection Options
		{
			get
			{
				if (this._options == null)
				{
					this._options = new DesignerOptionService.DesignerOptionCollection(this, null, string.Empty, null);
				}
				return this._options;
			}
		}

		/// <summary>Creates a new <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> with the given name and adds it to the given parent. </summary>
		/// <returns>A new <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> with the given name. </returns>
		/// <param name="parent">The parent designer option collection. All collections have a parent except the root object collection.</param>
		/// <param name="name">The name of this collection.</param>
		/// <param name="value">The object providing properties for this collection. Can be null if the collection should not provide any properties.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="parent" /> or <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.</exception>
		// Token: 0x060018F8 RID: 6392 RVA: 0x0006955C File Offset: 0x0006775C
		protected DesignerOptionService.DesignerOptionCollection CreateOptionCollection(DesignerOptionService.DesignerOptionCollection parent, string name, object value)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(global::SR.GetString("'{1}' is not a valid value for '{0}'.", new object[]
				{
					name.Length.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}), "name.Length");
			}
			return new DesignerOptionService.DesignerOptionCollection(this, parent, name, value);
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x000695D8 File Offset: 0x000677D8
		private PropertyDescriptor GetOptionProperty(string pageName, string valueName)
		{
			if (pageName == null)
			{
				throw new ArgumentNullException("pageName");
			}
			if (valueName == null)
			{
				throw new ArgumentNullException("valueName");
			}
			string[] array = pageName.Split(new char[] { '\\' });
			DesignerOptionService.DesignerOptionCollection designerOptionCollection = this.Options;
			foreach (string text in array)
			{
				designerOptionCollection = designerOptionCollection[text];
				if (designerOptionCollection == null)
				{
					return null;
				}
			}
			return designerOptionCollection.Properties[valueName];
		}

		/// <summary>Populates a <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</summary>
		/// <param name="options">The collection to populate.</param>
		// Token: 0x060018FA RID: 6394 RVA: 0x000027E8 File Offset: 0x000009E8
		protected virtual void PopulateOptionCollection(DesignerOptionService.DesignerOptionCollection options)
		{
		}

		/// <summary>Shows the options dialog box for the given object.</summary>
		/// <returns>true if the dialog box is shown; otherwise, false.</returns>
		/// <param name="options">The options collection containing the object to be invoked.</param>
		/// <param name="optionObject">The actual options object.</param>
		// Token: 0x060018FB RID: 6395 RVA: 0x00004240 File Offset: 0x00002440
		protected virtual bool ShowDialog(DesignerOptionService.DesignerOptionCollection options, object optionObject)
		{
			return false;
		}

		/// <summary>Gets the value of an option defined in this package.</summary>
		/// <returns>The value of the option named <paramref name="valueName" />.</returns>
		/// <param name="pageName">The page to which the option is bound.</param>
		/// <param name="valueName">The name of the option value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pageName" /> or <paramref name="valueName" /> is null.</exception>
		// Token: 0x060018FC RID: 6396 RVA: 0x00069648 File Offset: 0x00067848
		object IDesignerOptionService.GetOptionValue(string pageName, string valueName)
		{
			PropertyDescriptor optionProperty = this.GetOptionProperty(pageName, valueName);
			if (optionProperty != null)
			{
				return optionProperty.GetValue(null);
			}
			return null;
		}

		/// <summary>Sets the value of an option defined in this package.</summary>
		/// <param name="pageName">The page to which the option is bound</param>
		/// <param name="valueName">The name of the option value.</param>
		/// <param name="value">The value of the option.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pageName" /> or <paramref name="valueName" /> is null.</exception>
		// Token: 0x060018FD RID: 6397 RVA: 0x0006966C File Offset: 0x0006786C
		void IDesignerOptionService.SetOptionValue(string pageName, string valueName, object value)
		{
			PropertyDescriptor optionProperty = this.GetOptionProperty(pageName, valueName);
			if (optionProperty != null)
			{
				optionProperty.SetValue(null, value);
			}
		}

		// Token: 0x0400145C RID: 5212
		private DesignerOptionService.DesignerOptionCollection _options;

		/// <summary>Contains a collection of designer options. This class cannot be inherited.</summary>
		// Token: 0x02000312 RID: 786
		[TypeConverter(typeof(DesignerOptionService.DesignerOptionConverter))]
		[Editor("", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public sealed class DesignerOptionCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x060018FF RID: 6399 RVA: 0x00069690 File Offset: 0x00067890
			internal DesignerOptionCollection(DesignerOptionService service, DesignerOptionService.DesignerOptionCollection parent, string name, object value)
			{
				this._service = service;
				this._parent = parent;
				this._name = name;
				this._value = value;
				if (this._parent != null)
				{
					if (this._parent._children == null)
					{
						this._parent._children = new ArrayList(1);
					}
					this._parent._children.Add(this);
				}
			}

			/// <summary>Gets the number of child option collections this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> contains.</summary>
			/// <returns>The number of child option collections this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> contains.</returns>
			// Token: 0x17000510 RID: 1296
			// (get) Token: 0x06001900 RID: 6400 RVA: 0x000696F8 File Offset: 0x000678F8
			public int Count
			{
				get
				{
					this.EnsurePopulated();
					return this._children.Count;
				}
			}

			/// <summary>Gets the name of this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</summary>
			/// <returns>The name of this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</returns>
			// Token: 0x17000511 RID: 1297
			// (get) Token: 0x06001901 RID: 6401 RVA: 0x0006970B File Offset: 0x0006790B
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			/// <summary>Gets the parent collection object.</summary>
			/// <returns>The parent collection object, or null if there is no parent.</returns>
			// Token: 0x17000512 RID: 1298
			// (get) Token: 0x06001902 RID: 6402 RVA: 0x00069713 File Offset: 0x00067913
			public DesignerOptionService.DesignerOptionCollection Parent
			{
				get
				{
					return this._parent;
				}
			}

			/// <summary>Gets the collection of properties offered by this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />, along with all of its children.</summary>
			/// <returns>The collection of properties offered by this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />, along with all of its children.</returns>
			// Token: 0x17000513 RID: 1299
			// (get) Token: 0x06001903 RID: 6403 RVA: 0x0006971C File Offset: 0x0006791C
			public PropertyDescriptorCollection Properties
			{
				get
				{
					if (this._properties == null)
					{
						ArrayList arrayList;
						if (this._value != null)
						{
							PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this._value);
							arrayList = new ArrayList(properties.Count);
							using (IEnumerator enumerator = properties.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
									arrayList.Add(new DesignerOptionService.DesignerOptionCollection.WrappedPropertyDescriptor(propertyDescriptor, this._value));
								}
								goto IL_0076;
							}
						}
						arrayList = new ArrayList(1);
						IL_0076:
						this.EnsurePopulated();
						foreach (object obj2 in this._children)
						{
							DesignerOptionService.DesignerOptionCollection designerOptionCollection = (DesignerOptionService.DesignerOptionCollection)obj2;
							arrayList.AddRange(designerOptionCollection.Properties);
						}
						PropertyDescriptor[] array = (PropertyDescriptor[])arrayList.ToArray(typeof(PropertyDescriptor));
						this._properties = new PropertyDescriptorCollection(array, true);
					}
					return this._properties;
				}
			}

			/// <summary>Gets the child collection at the given index.</summary>
			/// <returns>The child collection at the specified index.</returns>
			/// <param name="index">The zero-based index of the child collection to get.</param>
			// Token: 0x17000514 RID: 1300
			public DesignerOptionService.DesignerOptionCollection this[int index]
			{
				get
				{
					this.EnsurePopulated();
					if (index < 0 || index >= this._children.Count)
					{
						throw new IndexOutOfRangeException("index");
					}
					return (DesignerOptionService.DesignerOptionCollection)this._children[index];
				}
			}

			/// <summary>Gets the child collection at the given name.</summary>
			/// <returns>The child collection with the name specified by the <paramref name="name" /> parameter, or null if the name is not found.</returns>
			/// <param name="name">The name of the child collection.</param>
			// Token: 0x17000515 RID: 1301
			public DesignerOptionService.DesignerOptionCollection this[string name]
			{
				get
				{
					this.EnsurePopulated();
					foreach (object obj in this._children)
					{
						DesignerOptionService.DesignerOptionCollection designerOptionCollection = (DesignerOptionService.DesignerOptionCollection)obj;
						if (string.Compare(designerOptionCollection.Name, name, true, CultureInfo.InvariantCulture) == 0)
						{
							return designerOptionCollection;
						}
					}
					return null;
				}
			}

			/// <summary>Copies the entire collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the collection. The <paramref name="array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			// Token: 0x06001906 RID: 6406 RVA: 0x000698DC File Offset: 0x00067ADC
			public void CopyTo(Array array, int index)
			{
				this.EnsurePopulated();
				this._children.CopyTo(array, index);
			}

			// Token: 0x06001907 RID: 6407 RVA: 0x000698F1 File Offset: 0x00067AF1
			private void EnsurePopulated()
			{
				if (this._children == null)
				{
					this._service.PopulateOptionCollection(this);
					if (this._children == null)
					{
						this._children = new ArrayList(1);
					}
				}
			}

			/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate this collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate this collection.</returns>
			// Token: 0x06001908 RID: 6408 RVA: 0x0006991B File Offset: 0x00067B1B
			public IEnumerator GetEnumerator()
			{
				this.EnsurePopulated();
				return this._children.GetEnumerator();
			}

			/// <summary>Returns the index of the first occurrence of a given value in a range of this collection.</summary>
			/// <returns>The index of the first occurrence of value within the entire collection, if found; otherwise, the lower bound of the collection minus 1.</returns>
			/// <param name="value">The object to locate in the collection.</param>
			// Token: 0x06001909 RID: 6409 RVA: 0x0006992E File Offset: 0x00067B2E
			public int IndexOf(DesignerOptionService.DesignerOptionCollection value)
			{
				this.EnsurePopulated();
				return this._children.IndexOf(value);
			}

			// Token: 0x0600190A RID: 6410 RVA: 0x00069944 File Offset: 0x00067B44
			private static object RecurseFindValue(DesignerOptionService.DesignerOptionCollection options)
			{
				if (options._value != null)
				{
					return options._value;
				}
				foreach (object obj in options)
				{
					object obj2 = DesignerOptionService.DesignerOptionCollection.RecurseFindValue((DesignerOptionService.DesignerOptionCollection)obj);
					if (obj2 != null)
					{
						return obj2;
					}
				}
				return null;
			}

			/// <summary>Displays a dialog box user interface (UI) with which the user can configure the options in this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</summary>
			/// <returns>true if the dialog box can be displayed; otherwise, false.</returns>
			// Token: 0x0600190B RID: 6411 RVA: 0x000699B0 File Offset: 0x00067BB0
			public bool ShowDialog()
			{
				object obj = DesignerOptionService.DesignerOptionCollection.RecurseFindValue(this);
				return obj != null && this._service.ShowDialog(this, obj);
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized and, therefore, thread safe.</summary>
			/// <returns>true if the access to the collection is synchronized; otherwise, false.</returns>
			// Token: 0x17000516 RID: 1302
			// (get) Token: 0x0600190C RID: 6412 RVA: 0x00004240 File Offset: 0x00002440
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>An object that can be used to synchronize access to the collection.</returns>
			// Token: 0x17000517 RID: 1303
			// (get) Token: 0x0600190D RID: 6413 RVA: 0x00002068 File Offset: 0x00000268
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>true if the collection has a fixed size; otherwise, false.</returns>
			// Token: 0x17000518 RID: 1304
			// (get) Token: 0x0600190E RID: 6414 RVA: 0x000027E2 File Offset: 0x000009E2
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x17000519 RID: 1305
			// (get) Token: 0x0600190F RID: 6415 RVA: 0x000027E2 File Offset: 0x000009E2
			bool IList.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets or sets the element at the specified index.</summary>
			/// <returns>The element at the specified index.</returns>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			// Token: 0x1700051A RID: 1306
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
			/// <returns>The position into which the new element was inserted.</returns>
			/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
			// Token: 0x06001912 RID: 6418 RVA: 0x000074E4 File Offset: 0x000056E4
			int IList.Add(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes all items from the collection.</summary>
			// Token: 0x06001913 RID: 6419 RVA: 0x000074E4 File Offset: 0x000056E4
			void IList.Clear()
			{
				throw new NotSupportedException();
			}

			/// <summary>Determines whether the collection contains a specific value.</summary>
			/// <returns>true if the <see cref="T:System.Object" /> is found in the collection; otherwise, false. </returns>
			/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection</param>
			// Token: 0x06001914 RID: 6420 RVA: 0x000699DF File Offset: 0x00067BDF
			bool IList.Contains(object value)
			{
				this.EnsurePopulated();
				return this._children.Contains(value);
			}

			/// <summary>Determines the index of a specific item in the collection.</summary>
			/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
			/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
			// Token: 0x06001915 RID: 6421 RVA: 0x0006992E File Offset: 0x00067B2E
			int IList.IndexOf(object value)
			{
				this.EnsurePopulated();
				return this._children.IndexOf(value);
			}

			/// <summary>Inserts an item into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
			/// <param name="value">The <see cref="T:System.Object" /> to insert into the collection.</param>
			// Token: 0x06001916 RID: 6422 RVA: 0x000074E4 File Offset: 0x000056E4
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes the first occurrence of a specific object from the collection.</summary>
			/// <param name="value">The <see cref="T:System.Object" /> to remove from the collection.</param>
			// Token: 0x06001917 RID: 6423 RVA: 0x000074E4 File Offset: 0x000056E4
			void IList.Remove(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes the collection item at the specified index.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			// Token: 0x06001918 RID: 6424 RVA: 0x000074E4 File Offset: 0x000056E4
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06001919 RID: 6425 RVA: 0x0000F0CE File Offset: 0x0000D2CE
			internal DesignerOptionCollection()
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}

			// Token: 0x0400145D RID: 5213
			private DesignerOptionService _service;

			// Token: 0x0400145E RID: 5214
			private DesignerOptionService.DesignerOptionCollection _parent;

			// Token: 0x0400145F RID: 5215
			private string _name;

			// Token: 0x04001460 RID: 5216
			private object _value;

			// Token: 0x04001461 RID: 5217
			private ArrayList _children;

			// Token: 0x04001462 RID: 5218
			private PropertyDescriptorCollection _properties;

			// Token: 0x02000313 RID: 787
			private sealed class WrappedPropertyDescriptor : PropertyDescriptor
			{
				// Token: 0x0600191A RID: 6426 RVA: 0x000699F3 File Offset: 0x00067BF3
				internal WrappedPropertyDescriptor(PropertyDescriptor property, object target)
					: base(property.Name, null)
				{
					this.property = property;
					this.target = target;
				}

				// Token: 0x1700051B RID: 1307
				// (get) Token: 0x0600191B RID: 6427 RVA: 0x00069A10 File Offset: 0x00067C10
				public override AttributeCollection Attributes
				{
					get
					{
						return this.property.Attributes;
					}
				}

				// Token: 0x1700051C RID: 1308
				// (get) Token: 0x0600191C RID: 6428 RVA: 0x00069A1D File Offset: 0x00067C1D
				public override Type ComponentType
				{
					get
					{
						return this.property.ComponentType;
					}
				}

				// Token: 0x1700051D RID: 1309
				// (get) Token: 0x0600191D RID: 6429 RVA: 0x00069A2A File Offset: 0x00067C2A
				public override bool IsReadOnly
				{
					get
					{
						return this.property.IsReadOnly;
					}
				}

				// Token: 0x1700051E RID: 1310
				// (get) Token: 0x0600191E RID: 6430 RVA: 0x00069A37 File Offset: 0x00067C37
				public override Type PropertyType
				{
					get
					{
						return this.property.PropertyType;
					}
				}

				// Token: 0x0600191F RID: 6431 RVA: 0x00069A44 File Offset: 0x00067C44
				public override bool CanResetValue(object component)
				{
					return this.property.CanResetValue(this.target);
				}

				// Token: 0x06001920 RID: 6432 RVA: 0x00069A57 File Offset: 0x00067C57
				public override object GetValue(object component)
				{
					return this.property.GetValue(this.target);
				}

				// Token: 0x06001921 RID: 6433 RVA: 0x00069A6A File Offset: 0x00067C6A
				public override void ResetValue(object component)
				{
					this.property.ResetValue(this.target);
				}

				// Token: 0x06001922 RID: 6434 RVA: 0x00069A7D File Offset: 0x00067C7D
				public override void SetValue(object component, object value)
				{
					this.property.SetValue(this.target, value);
				}

				// Token: 0x06001923 RID: 6435 RVA: 0x00069A91 File Offset: 0x00067C91
				public override bool ShouldSerializeValue(object component)
				{
					return this.property.ShouldSerializeValue(this.target);
				}

				// Token: 0x04001463 RID: 5219
				private object target;

				// Token: 0x04001464 RID: 5220
				private PropertyDescriptor property;
			}
		}

		// Token: 0x02000314 RID: 788
		internal sealed class DesignerOptionConverter : TypeConverter
		{
			// Token: 0x06001924 RID: 6436 RVA: 0x000027E2 File Offset: 0x000009E2
			public override bool GetPropertiesSupported(ITypeDescriptorContext cxt)
			{
				return true;
			}

			// Token: 0x06001925 RID: 6437 RVA: 0x00069AA4 File Offset: 0x00067CA4
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext cxt, object value, Attribute[] attributes)
			{
				PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
				DesignerOptionService.DesignerOptionCollection designerOptionCollection = value as DesignerOptionService.DesignerOptionCollection;
				if (designerOptionCollection == null)
				{
					return propertyDescriptorCollection;
				}
				foreach (object obj in designerOptionCollection)
				{
					DesignerOptionService.DesignerOptionCollection designerOptionCollection2 = (DesignerOptionService.DesignerOptionCollection)obj;
					propertyDescriptorCollection.Add(new DesignerOptionService.DesignerOptionConverter.OptionPropertyDescriptor(designerOptionCollection2));
				}
				foreach (object obj2 in designerOptionCollection.Properties)
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
					propertyDescriptorCollection.Add(propertyDescriptor);
				}
				return propertyDescriptorCollection;
			}

			// Token: 0x06001926 RID: 6438 RVA: 0x00069B64 File Offset: 0x00067D64
			public override object ConvertTo(ITypeDescriptorContext cxt, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
				{
					return global::SR.GetString("(Collection)");
				}
				return base.ConvertTo(cxt, culture, value, destinationType);
			}

			// Token: 0x02000315 RID: 789
			private class OptionPropertyDescriptor : PropertyDescriptor
			{
				// Token: 0x06001928 RID: 6440 RVA: 0x00069B8F File Offset: 0x00067D8F
				internal OptionPropertyDescriptor(DesignerOptionService.DesignerOptionCollection option)
					: base(option.Name, null)
				{
					this._option = option;
				}

				// Token: 0x1700051F RID: 1311
				// (get) Token: 0x06001929 RID: 6441 RVA: 0x00069BA5 File Offset: 0x00067DA5
				public override Type ComponentType
				{
					get
					{
						return this._option.GetType();
					}
				}

				// Token: 0x17000520 RID: 1312
				// (get) Token: 0x0600192A RID: 6442 RVA: 0x000027E2 File Offset: 0x000009E2
				public override bool IsReadOnly
				{
					get
					{
						return true;
					}
				}

				// Token: 0x17000521 RID: 1313
				// (get) Token: 0x0600192B RID: 6443 RVA: 0x00069BA5 File Offset: 0x00067DA5
				public override Type PropertyType
				{
					get
					{
						return this._option.GetType();
					}
				}

				// Token: 0x0600192C RID: 6444 RVA: 0x00004240 File Offset: 0x00002440
				public override bool CanResetValue(object component)
				{
					return false;
				}

				// Token: 0x0600192D RID: 6445 RVA: 0x00069BB2 File Offset: 0x00067DB2
				public override object GetValue(object component)
				{
					return this._option;
				}

				// Token: 0x0600192E RID: 6446 RVA: 0x000027E8 File Offset: 0x000009E8
				public override void ResetValue(object component)
				{
				}

				// Token: 0x0600192F RID: 6447 RVA: 0x000027E8 File Offset: 0x000009E8
				public override void SetValue(object component, object value)
				{
				}

				// Token: 0x06001930 RID: 6448 RVA: 0x00004240 File Offset: 0x00002440
				public override bool ShouldSerializeValue(object component)
				{
					return false;
				}

				// Token: 0x04001465 RID: 5221
				private DesignerOptionService.DesignerOptionCollection _option;
			}
		}
	}
}
