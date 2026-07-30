using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace System.ComponentModel.DataAnnotations
{
	// Token: 0x02000005 RID: 5
	internal class AssociatedMetadataTypeTypeDescriptor : CustomTypeDescriptor
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000209A File Offset: 0x0000029A
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020A2 File Offset: 0x000002A2
		private Type AssociatedMetadataType { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020AB File Offset: 0x000002AB
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000020B3 File Offset: 0x000002B3
		private bool IsSelfAssociated { get; set; }

		// Token: 0x06000009 RID: 9 RVA: 0x000020BC File Offset: 0x000002BC
		public AssociatedMetadataTypeTypeDescriptor(ICustomTypeDescriptor parent, Type type, Type associatedMetadataType)
			: base(parent)
		{
			this.AssociatedMetadataType = associatedMetadataType ?? AssociatedMetadataTypeTypeDescriptor.TypeDescriptorCache.GetAssociatedMetadataType(type);
			this.IsSelfAssociated = type == this.AssociatedMetadataType;
			if (this.AssociatedMetadataType != null)
			{
				AssociatedMetadataTypeTypeDescriptor.TypeDescriptorCache.ValidateMetadataType(type, this.AssociatedMetadataType);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000210D File Offset: 0x0000030D
		public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			return this.GetPropertiesWithMetadata(base.GetProperties(attributes));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000211C File Offset: 0x0000031C
		public override PropertyDescriptorCollection GetProperties()
		{
			return this.GetPropertiesWithMetadata(base.GetProperties());
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000212C File Offset: 0x0000032C
		private PropertyDescriptorCollection GetPropertiesWithMetadata(PropertyDescriptorCollection originalCollection)
		{
			if (this.AssociatedMetadataType == null)
			{
				return originalCollection;
			}
			bool flag = false;
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in originalCollection)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				Attribute[] associatedMetadata = AssociatedMetadataTypeTypeDescriptor.TypeDescriptorCache.GetAssociatedMetadata(this.AssociatedMetadataType, propertyDescriptor.Name);
				PropertyDescriptor propertyDescriptor2 = propertyDescriptor;
				if (associatedMetadata.Length != 0)
				{
					propertyDescriptor2 = new MetadataPropertyDescriptorWrapper(propertyDescriptor, associatedMetadata);
					flag = true;
				}
				list.Add(propertyDescriptor2);
			}
			if (flag)
			{
				return new PropertyDescriptorCollection(list.ToArray(), true);
			}
			return originalCollection;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021D4 File Offset: 0x000003D4
		public override AttributeCollection GetAttributes()
		{
			AttributeCollection attributeCollection = base.GetAttributes();
			if (this.AssociatedMetadataType != null && !this.IsSelfAssociated)
			{
				Attribute[] array = TypeDescriptor.GetAttributes(this.AssociatedMetadataType).OfType<Attribute>().ToArray<Attribute>();
				attributeCollection = AttributeCollection.FromExisting(attributeCollection, array);
			}
			return attributeCollection;
		}

		// Token: 0x02000006 RID: 6
		private static class TypeDescriptorCache
		{
			// Token: 0x0600000E RID: 14 RVA: 0x00002220 File Offset: 0x00000420
			public static void ValidateMetadataType(Type type, Type associatedType)
			{
				Tuple<Type, Type> tuple = new Tuple<Type, Type>(type, associatedType);
				if (!AssociatedMetadataTypeTypeDescriptor.TypeDescriptorCache._validatedMetadataTypeCache.ContainsKey(tuple))
				{
					AssociatedMetadataTypeTypeDescriptor.TypeDescriptorCache.CheckAssociatedMetadataType(type, associatedType);
					AssociatedMetadataTypeTypeDescriptor.TypeDescriptorCache._validatedMetadataTypeCache.TryAdd(tuple, true);
				}
			}

			// Token: 0x0600000F RID: 15 RVA: 0x00002258 File Offset: 0x00000458
			public static Type GetAssociatedMetadataType(Type type)
			{
				Type type2 = null;
				if (AssociatedMetadataTypeTypeDescriptor.TypeDescriptorCache._metadataTypeCache.TryGetValue(type, out type2))
				{
					return type2;
				}
				MetadataTypeAttribute metadataTypeAttribute = (MetadataTypeAttribute)Attribute.GetCustomAttribute(type, typeof(MetadataTypeAttribute));
				if (metadataTypeAttribute != null)
				{
					type2 = metadataTypeAttribute.MetadataClassType;
				}
				AssociatedMetadataTypeTypeDescriptor.TypeDescriptorCache._metadataTypeCache.TryAdd(type, type2);
				return type2;
			}

			// Token: 0x06000010 RID: 16 RVA: 0x000022A8 File Offset: 0x000004A8
			private static void CheckAssociatedMetadataType(Type mainType, Type associatedMetadataType)
			{
				HashSet<string> hashSet = new HashSet<string>(from p in mainType.GetProperties()
					select p.Name);
				IEnumerable<string> enumerable = from f in associatedMetadataType.GetFields()
					select f.Name;
				IEnumerable<string> enumerable2 = from p in associatedMetadataType.GetProperties()
					select p.Name;
				HashSet<string> hashSet2 = new HashSet<string>(enumerable.Concat(enumerable2), StringComparer.Ordinal);
				if (!hashSet2.IsSubsetOf(hashSet))
				{
					hashSet2.ExceptWith(hashSet);
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "The associated metadata type for type '{0}' contains the following unknown properties or fields: {1}. Please make sure that the names of these members match the names of the properties on the main type.", mainType.FullName, string.Join(", ", hashSet2.ToArray<string>())));
				}
			}

			// Token: 0x06000011 RID: 17 RVA: 0x00002388 File Offset: 0x00000588
			public static Attribute[] GetAssociatedMetadata(Type type, string memberName)
			{
				Tuple<Type, string> tuple = new Tuple<Type, string>(type, memberName);
				Attribute[] customAttributes;
				if (AssociatedMetadataTypeTypeDescriptor.TypeDescriptorCache._typeMemberCache.TryGetValue(tuple, out customAttributes))
				{
					return customAttributes;
				}
				MemberTypes memberTypes = MemberTypes.Field | MemberTypes.Property;
				BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
				MemberInfo memberInfo = type.GetMember(memberName, memberTypes, bindingFlags).FirstOrDefault<MemberInfo>();
				if (memberInfo != null)
				{
					customAttributes = Attribute.GetCustomAttributes(memberInfo, true);
				}
				else
				{
					customAttributes = AssociatedMetadataTypeTypeDescriptor.TypeDescriptorCache.emptyAttributes;
				}
				AssociatedMetadataTypeTypeDescriptor.TypeDescriptorCache._typeMemberCache.TryAdd(tuple, customAttributes);
				return customAttributes;
			}

			// Token: 0x0400002E RID: 46
			private static readonly Attribute[] emptyAttributes = new Attribute[0];

			// Token: 0x0400002F RID: 47
			private static readonly ConcurrentDictionary<Type, Type> _metadataTypeCache = new ConcurrentDictionary<Type, Type>();

			// Token: 0x04000030 RID: 48
			private static readonly ConcurrentDictionary<Tuple<Type, string>, Attribute[]> _typeMemberCache = new ConcurrentDictionary<Tuple<Type, string>, Attribute[]>();

			// Token: 0x04000031 RID: 49
			private static readonly ConcurrentDictionary<Tuple<Type, Type>, bool> _validatedMetadataTypeCache = new ConcurrentDictionary<Tuple<Type, Type>, bool>();
		}
	}
}
