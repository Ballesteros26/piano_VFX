using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System.ComponentModel.DataAnnotations
{
	// Token: 0x0200003A RID: 58
	internal class ValidationAttributeStore
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00004C17 File Offset: 0x00002E17
		internal static ValidationAttributeStore Instance
		{
			get
			{
				return ValidationAttributeStore._singleton;
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004C1E File Offset: 0x00002E1E
		internal IEnumerable<ValidationAttribute> GetTypeValidationAttributes(ValidationContext validationContext)
		{
			ValidationAttributeStore.EnsureValidationContext(validationContext);
			return this.GetTypeStoreItem(validationContext.ObjectType).ValidationAttributes;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004C37 File Offset: 0x00002E37
		internal DisplayAttribute GetTypeDisplayAttribute(ValidationContext validationContext)
		{
			ValidationAttributeStore.EnsureValidationContext(validationContext);
			return this.GetTypeStoreItem(validationContext.ObjectType).DisplayAttribute;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004C50 File Offset: 0x00002E50
		internal IEnumerable<ValidationAttribute> GetPropertyValidationAttributes(ValidationContext validationContext)
		{
			ValidationAttributeStore.EnsureValidationContext(validationContext);
			return this.GetTypeStoreItem(validationContext.ObjectType).GetPropertyStoreItem(validationContext.MemberName).ValidationAttributes;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004C74 File Offset: 0x00002E74
		internal DisplayAttribute GetPropertyDisplayAttribute(ValidationContext validationContext)
		{
			ValidationAttributeStore.EnsureValidationContext(validationContext);
			return this.GetTypeStoreItem(validationContext.ObjectType).GetPropertyStoreItem(validationContext.MemberName).DisplayAttribute;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004C98 File Offset: 0x00002E98
		internal Type GetPropertyType(ValidationContext validationContext)
		{
			ValidationAttributeStore.EnsureValidationContext(validationContext);
			return this.GetTypeStoreItem(validationContext.ObjectType).GetPropertyStoreItem(validationContext.MemberName).PropertyType;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00004CBC File Offset: 0x00002EBC
		internal bool IsPropertyContext(ValidationContext validationContext)
		{
			ValidationAttributeStore.EnsureValidationContext(validationContext);
			ValidationAttributeStore.TypeStoreItem typeStoreItem = this.GetTypeStoreItem(validationContext.ObjectType);
			ValidationAttributeStore.PropertyStoreItem propertyStoreItem = null;
			return typeStoreItem.TryGetPropertyStoreItem(validationContext.MemberName, out propertyStoreItem);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004CEC File Offset: 0x00002EEC
		private ValidationAttributeStore.TypeStoreItem GetTypeStoreItem(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Dictionary<Type, ValidationAttributeStore.TypeStoreItem> typeStoreItems = this._typeStoreItems;
			ValidationAttributeStore.TypeStoreItem typeStoreItem2;
			lock (typeStoreItems)
			{
				ValidationAttributeStore.TypeStoreItem typeStoreItem = null;
				if (!this._typeStoreItems.TryGetValue(type, out typeStoreItem))
				{
					IEnumerable<Attribute> enumerable = TypeDescriptor.GetAttributes(type).Cast<Attribute>();
					typeStoreItem = new ValidationAttributeStore.TypeStoreItem(type, enumerable);
					this._typeStoreItems[type] = typeStoreItem;
				}
				typeStoreItem2 = typeStoreItem;
			}
			return typeStoreItem2;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00004D74 File Offset: 0x00002F74
		private static void EnsureValidationContext(ValidationContext validationContext)
		{
			if (validationContext == null)
			{
				throw new ArgumentNullException("validationContext");
			}
		}

		// Token: 0x040000B6 RID: 182
		private static ValidationAttributeStore _singleton = new ValidationAttributeStore();

		// Token: 0x040000B7 RID: 183
		private Dictionary<Type, ValidationAttributeStore.TypeStoreItem> _typeStoreItems = new Dictionary<Type, ValidationAttributeStore.TypeStoreItem>();

		// Token: 0x0200003B RID: 59
		private abstract class StoreItem
		{
			// Token: 0x06000156 RID: 342 RVA: 0x00004DA3 File Offset: 0x00002FA3
			internal StoreItem(IEnumerable<Attribute> attributes)
			{
				this._validationAttributes = attributes.OfType<ValidationAttribute>();
				this.DisplayAttribute = attributes.OfType<DisplayAttribute>().SingleOrDefault<DisplayAttribute>();
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x06000157 RID: 343 RVA: 0x00004DC8 File Offset: 0x00002FC8
			internal IEnumerable<ValidationAttribute> ValidationAttributes
			{
				get
				{
					return this._validationAttributes;
				}
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000158 RID: 344 RVA: 0x00004DD0 File Offset: 0x00002FD0
			// (set) Token: 0x06000159 RID: 345 RVA: 0x00004DD8 File Offset: 0x00002FD8
			internal DisplayAttribute DisplayAttribute { get; set; }

			// Token: 0x040000B8 RID: 184
			private static IEnumerable<ValidationAttribute> _emptyValidationAttributeEnumerable = new ValidationAttribute[0];

			// Token: 0x040000B9 RID: 185
			private IEnumerable<ValidationAttribute> _validationAttributes;
		}

		// Token: 0x0200003C RID: 60
		private class TypeStoreItem : ValidationAttributeStore.StoreItem
		{
			// Token: 0x0600015B RID: 347 RVA: 0x00004DEE File Offset: 0x00002FEE
			internal TypeStoreItem(Type type, IEnumerable<Attribute> attributes)
				: base(attributes)
			{
				this._type = type;
			}

			// Token: 0x0600015C RID: 348 RVA: 0x00004E0C File Offset: 0x0000300C
			internal ValidationAttributeStore.PropertyStoreItem GetPropertyStoreItem(string propertyName)
			{
				ValidationAttributeStore.PropertyStoreItem propertyStoreItem = null;
				if (!this.TryGetPropertyStoreItem(propertyName, out propertyStoreItem))
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The type '{0}' does not contain a public property named '{1}'.", this._type.Name, propertyName), "propertyName");
				}
				return propertyStoreItem;
			}

			// Token: 0x0600015D RID: 349 RVA: 0x00004E50 File Offset: 0x00003050
			internal bool TryGetPropertyStoreItem(string propertyName, out ValidationAttributeStore.PropertyStoreItem item)
			{
				if (string.IsNullOrEmpty(propertyName))
				{
					throw new ArgumentNullException("propertyName");
				}
				if (this._propertyStoreItems == null)
				{
					object syncRoot = this._syncRoot;
					lock (syncRoot)
					{
						if (this._propertyStoreItems == null)
						{
							this._propertyStoreItems = this.CreatePropertyStoreItems();
						}
					}
				}
				return this._propertyStoreItems.TryGetValue(propertyName, out item);
			}

			// Token: 0x0600015E RID: 350 RVA: 0x00004ECC File Offset: 0x000030CC
			private Dictionary<string, ValidationAttributeStore.PropertyStoreItem> CreatePropertyStoreItems()
			{
				Dictionary<string, ValidationAttributeStore.PropertyStoreItem> dictionary = new Dictionary<string, ValidationAttributeStore.PropertyStoreItem>();
				foreach (object obj in TypeDescriptor.GetProperties(this._type))
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
					ValidationAttributeStore.PropertyStoreItem propertyStoreItem = new ValidationAttributeStore.PropertyStoreItem(propertyDescriptor.PropertyType, ValidationAttributeStore.TypeStoreItem.GetExplicitAttributes(propertyDescriptor).Cast<Attribute>());
					dictionary[propertyDescriptor.Name] = propertyStoreItem;
				}
				return dictionary;
			}

			// Token: 0x0600015F RID: 351 RVA: 0x00004F54 File Offset: 0x00003154
			public static AttributeCollection GetExplicitAttributes(PropertyDescriptor propertyDescriptor)
			{
				List<Attribute> list = new List<Attribute>(propertyDescriptor.Attributes.Cast<Attribute>());
				IEnumerable<Attribute> enumerable = TypeDescriptor.GetAttributes(propertyDescriptor.PropertyType).Cast<Attribute>();
				bool flag = false;
				foreach (Attribute attribute in enumerable)
				{
					for (int i = list.Count - 1; i >= 0; i--)
					{
						if (attribute == list[i])
						{
							list.RemoveAt(i);
							flag = true;
						}
					}
				}
				if (!flag)
				{
					return propertyDescriptor.Attributes;
				}
				return new AttributeCollection(list.ToArray());
			}

			// Token: 0x040000BB RID: 187
			private object _syncRoot = new object();

			// Token: 0x040000BC RID: 188
			private Type _type;

			// Token: 0x040000BD RID: 189
			private Dictionary<string, ValidationAttributeStore.PropertyStoreItem> _propertyStoreItems;
		}

		// Token: 0x0200003D RID: 61
		private class PropertyStoreItem : ValidationAttributeStore.StoreItem
		{
			// Token: 0x06000160 RID: 352 RVA: 0x00004FF8 File Offset: 0x000031F8
			internal PropertyStoreItem(Type propertyType, IEnumerable<Attribute> attributes)
				: base(attributes)
			{
				this._propertyType = propertyType;
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x06000161 RID: 353 RVA: 0x00005008 File Offset: 0x00003208
			internal Type PropertyType
			{
				get
				{
					return this._propertyType;
				}
			}

			// Token: 0x040000BE RID: 190
			private Type _propertyType;
		}
	}
}
