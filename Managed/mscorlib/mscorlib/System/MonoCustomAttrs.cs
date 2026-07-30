using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200021D RID: 541
	internal static class MonoCustomAttrs
	{
		// Token: 0x0600198E RID: 6542 RVA: 0x0005F720 File Offset: 0x0005D920
		private static bool IsUserCattrProvider(object obj)
		{
			Type type = obj as Type;
			if (type is RuntimeType || type is TypeBuilder)
			{
				return false;
			}
			if (obj is Type)
			{
				return true;
			}
			if (MonoCustomAttrs.corlib == null)
			{
				MonoCustomAttrs.corlib = typeof(int).Assembly;
			}
			return obj.GetType().Assembly != MonoCustomAttrs.corlib;
		}

		// Token: 0x0600198F RID: 6543
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object[] GetCustomAttributesInternal(ICustomAttributeProvider obj, Type attributeType, bool pseudoAttrs);

		// Token: 0x06001990 RID: 6544 RVA: 0x0005F788 File Offset: 0x0005D988
		internal static object[] GetPseudoCustomAttributes(ICustomAttributeProvider obj, Type attributeType)
		{
			object[] array = null;
			if (obj is MonoMethod)
			{
				array = ((MonoMethod)obj).GetPseudoCustomAttributes();
			}
			else if (obj is FieldInfo)
			{
				array = ((FieldInfo)obj).GetPseudoCustomAttributes();
			}
			else if (obj is ParameterInfo)
			{
				array = ((ParameterInfo)obj).GetPseudoCustomAttributes();
			}
			else if (obj is Type)
			{
				array = MonoCustomAttrs.GetPseudoCustomAttributes((Type)obj);
			}
			if (attributeType != null && array != null)
			{
				int i = 0;
				while (i < array.Length)
				{
					if (attributeType.IsAssignableFrom(array[i].GetType()))
					{
						if (array.Length == 1)
						{
							return array;
						}
						return new object[] { array[i] };
					}
					else
					{
						i++;
					}
				}
				return EmptyArray<object>.Value;
			}
			return array;
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0005F834 File Offset: 0x0005DA34
		private static object[] GetPseudoCustomAttributes(Type type)
		{
			int num = 0;
			TypeAttributes attributes = type.Attributes;
			if ((attributes & TypeAttributes.Serializable) != TypeAttributes.NotPublic)
			{
				num++;
			}
			if ((attributes & TypeAttributes.Import) != TypeAttributes.NotPublic)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if ((attributes & TypeAttributes.Serializable) != TypeAttributes.NotPublic)
			{
				array[num++] = new SerializableAttribute();
			}
			if ((attributes & TypeAttributes.Import) != TypeAttributes.NotPublic)
			{
				array[num++] = new ComImportAttribute();
			}
			return array;
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0005F8A0 File Offset: 0x0005DAA0
		internal static object[] GetCustomAttributesBase(ICustomAttributeProvider obj, Type attributeType, bool inheritedOnly)
		{
			object[] array;
			if (MonoCustomAttrs.IsUserCattrProvider(obj))
			{
				array = obj.GetCustomAttributes(attributeType, true);
			}
			else
			{
				array = MonoCustomAttrs.GetCustomAttributesInternal(obj, attributeType, false);
			}
			if (!inheritedOnly)
			{
				object[] pseudoCustomAttributes = MonoCustomAttrs.GetPseudoCustomAttributes(obj, attributeType);
				if (pseudoCustomAttributes != null)
				{
					object[] array2 = new object[array.Length + pseudoCustomAttributes.Length];
					Array.Copy(array, array2, array.Length);
					Array.Copy(pseudoCustomAttributes, 0, array2, array.Length, pseudoCustomAttributes.Length);
					return array2;
				}
			}
			return array;
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0005F900 File Offset: 0x0005DB00
		internal static object[] GetCustomAttributes(ICustomAttributeProvider obj, Type attributeType, bool inherit)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (attributeType == typeof(MonoCustomAttrs))
			{
				attributeType = null;
			}
			object[] array = MonoCustomAttrs.GetCustomAttributesBase(obj, attributeType, false);
			if (!inherit && array.Length == 1)
			{
				if (array[0] == null)
				{
					throw new CustomAttributeFormatException("Invalid custom attribute format");
				}
				object[] array2;
				if (attributeType != null)
				{
					if (attributeType.IsAssignableFrom(array[0].GetType()))
					{
						array2 = (object[])Array.CreateInstance(attributeType, 1);
						array2[0] = array[0];
					}
					else
					{
						array2 = (object[])Array.CreateInstance(attributeType, 0);
					}
				}
				else
				{
					array2 = (object[])Array.CreateInstance(array[0].GetType(), 1);
					array2[0] = array[0];
				}
				return array2;
			}
			else
			{
				if (inherit && MonoCustomAttrs.GetBase(obj) == null)
				{
					inherit = false;
				}
				if (attributeType != null && attributeType.IsSealed && inherit && !MonoCustomAttrs.RetrieveAttributeUsage(attributeType).Inherited)
				{
					inherit = false;
				}
				int num = Math.Max(array.Length, 16);
				ICustomAttributeProvider customAttributeProvider = obj;
				List<object> list;
				object[] array4;
				if (inherit)
				{
					Dictionary<Type, MonoCustomAttrs.AttributeInfo> dictionary = new Dictionary<Type, MonoCustomAttrs.AttributeInfo>(num);
					int num2 = 0;
					list = new List<object>(num);
					for (;;)
					{
						foreach (object obj2 in array)
						{
							if (obj2 == null)
							{
								goto Block_22;
							}
							Type type = obj2.GetType();
							if (!(attributeType != null) || attributeType.IsAssignableFrom(type))
							{
								MonoCustomAttrs.AttributeInfo attributeInfo;
								AttributeUsageAttribute attributeUsageAttribute;
								if (dictionary.TryGetValue(type, out attributeInfo))
								{
									attributeUsageAttribute = attributeInfo.Usage;
								}
								else
								{
									attributeUsageAttribute = MonoCustomAttrs.RetrieveAttributeUsage(type);
								}
								if ((num2 == 0 || attributeUsageAttribute.Inherited) && (attributeUsageAttribute.AllowMultiple || attributeInfo == null || (attributeInfo != null && attributeInfo.InheritanceLevel == num2)))
								{
									list.Add(obj2);
								}
								if (attributeInfo == null)
								{
									dictionary.Add(type, new MonoCustomAttrs.AttributeInfo(attributeUsageAttribute, num2));
								}
							}
						}
						if ((customAttributeProvider = MonoCustomAttrs.GetBase(customAttributeProvider)) != null)
						{
							num2++;
							array = MonoCustomAttrs.GetCustomAttributesBase(customAttributeProvider, attributeType, true);
						}
						if (!inherit || customAttributeProvider == null)
						{
							goto IL_02C7;
						}
					}
					Block_22:
					throw new CustomAttributeFormatException("Invalid custom attribute format");
					IL_02C7:
					if (attributeType == null || attributeType.IsValueType)
					{
						array4 = new Attribute[list.Count];
					}
					else
					{
						array4 = Array.CreateInstance(attributeType, list.Count) as object[];
					}
					list.CopyTo(array4, 0);
					return array4;
				}
				if (attributeType == null)
				{
					object[] array3 = array;
					for (int i = 0; i < array3.Length; i++)
					{
						if (array3[i] == null)
						{
							throw new CustomAttributeFormatException("Invalid custom attribute format");
						}
					}
					Attribute[] array5 = new Attribute[array.Length];
					array.CopyTo(array5, 0);
					return array5;
				}
				list = new List<object>(num);
				foreach (object obj3 in array)
				{
					if (obj3 == null)
					{
						throw new CustomAttributeFormatException("Invalid custom attribute format");
					}
					Type type2 = obj3.GetType();
					if (!(attributeType != null) || attributeType.IsAssignableFrom(type2))
					{
						list.Add(obj3);
					}
				}
				if (attributeType == null || attributeType.IsValueType)
				{
					array4 = new Attribute[list.Count];
				}
				else
				{
					array4 = Array.CreateInstance(attributeType, list.Count) as object[];
				}
				list.CopyTo(array4, 0);
				return array4;
			}
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0005FC12 File Offset: 0x0005DE12
		internal static object[] GetCustomAttributes(ICustomAttributeProvider obj, bool inherit)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (!inherit)
			{
				return (object[])MonoCustomAttrs.GetCustomAttributesBase(obj, null, false).Clone();
			}
			return MonoCustomAttrs.GetCustomAttributes(obj, typeof(MonoCustomAttrs), inherit);
		}

		// Token: 0x06001995 RID: 6549
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CustomAttributeData[] GetCustomAttributesDataInternal(ICustomAttributeProvider obj);

		// Token: 0x06001996 RID: 6550 RVA: 0x0005FC49 File Offset: 0x0005DE49
		internal static IList<CustomAttributeData> GetCustomAttributesData(ICustomAttributeProvider obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return Array.AsReadOnly<CustomAttributeData>(MonoCustomAttrs.GetCustomAttributesDataInternal(obj));
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0005FC64 File Offset: 0x0005DE64
		internal static bool IsDefined(ICustomAttributeProvider obj, Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			AttributeUsageAttribute attributeUsageAttribute = null;
			while (!MonoCustomAttrs.IsUserCattrProvider(obj))
			{
				if (MonoCustomAttrs.IsDefinedInternal(obj, attributeType))
				{
					return true;
				}
				object[] pseudoCustomAttributes = MonoCustomAttrs.GetPseudoCustomAttributes(obj, attributeType);
				if (pseudoCustomAttributes != null)
				{
					for (int i = 0; i < pseudoCustomAttributes.Length; i++)
					{
						if (attributeType.IsAssignableFrom(pseudoCustomAttributes[i].GetType()))
						{
							return true;
						}
					}
				}
				if (attributeUsageAttribute == null)
				{
					if (!inherit)
					{
						return false;
					}
					attributeUsageAttribute = MonoCustomAttrs.RetrieveAttributeUsage(attributeType);
					if (!attributeUsageAttribute.Inherited)
					{
						return false;
					}
				}
				obj = MonoCustomAttrs.GetBase(obj);
				if (obj == null)
				{
					return false;
				}
			}
			return obj.IsDefined(attributeType, inherit);
		}

		// Token: 0x06001998 RID: 6552
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsDefinedInternal(ICustomAttributeProvider obj, Type AttributeType);

		// Token: 0x06001999 RID: 6553 RVA: 0x0005FCF4 File Offset: 0x0005DEF4
		private static PropertyInfo GetBasePropertyDefinition(MonoProperty property)
		{
			MethodInfo methodInfo = property.GetGetMethod(true);
			if (methodInfo == null || !methodInfo.IsVirtual)
			{
				methodInfo = property.GetSetMethod(true);
			}
			if (methodInfo == null || !methodInfo.IsVirtual)
			{
				return null;
			}
			MethodInfo baseMethod = methodInfo.GetBaseMethod();
			if (!(baseMethod != null) || !(baseMethod != methodInfo))
			{
				return null;
			}
			ParameterInfo[] indexParameters = property.GetIndexParameters();
			if (indexParameters != null && indexParameters.Length != 0)
			{
				Type[] array = new Type[indexParameters.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = indexParameters[i].ParameterType;
				}
				return baseMethod.DeclaringType.GetProperty(property.Name, property.PropertyType, array);
			}
			return baseMethod.DeclaringType.GetProperty(property.Name, property.PropertyType);
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0005FDB8 File Offset: 0x0005DFB8
		private static EventInfo GetBaseEventDefinition(MonoEvent evt)
		{
			MethodInfo methodInfo = evt.GetAddMethod(true);
			if (methodInfo == null || !methodInfo.IsVirtual)
			{
				methodInfo = evt.GetRaiseMethod(true);
			}
			if (methodInfo == null || !methodInfo.IsVirtual)
			{
				methodInfo = evt.GetRemoveMethod(true);
			}
			if (methodInfo == null || !methodInfo.IsVirtual)
			{
				return null;
			}
			MethodInfo baseMethod = methodInfo.GetBaseMethod();
			if (baseMethod != null && baseMethod != methodInfo)
			{
				BindingFlags bindingFlags = (methodInfo.IsPublic ? BindingFlags.Public : BindingFlags.NonPublic);
				bindingFlags |= (methodInfo.IsStatic ? BindingFlags.Static : BindingFlags.Instance);
				return baseMethod.DeclaringType.GetEvent(evt.Name, bindingFlags);
			}
			return null;
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x0005FE60 File Offset: 0x0005E060
		private static ICustomAttributeProvider GetBase(ICustomAttributeProvider obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is Type)
			{
				return ((Type)obj).BaseType;
			}
			MethodInfo methodInfo = null;
			if (obj is MonoProperty)
			{
				return MonoCustomAttrs.GetBasePropertyDefinition((MonoProperty)obj);
			}
			if (obj is MonoEvent)
			{
				return MonoCustomAttrs.GetBaseEventDefinition((MonoEvent)obj);
			}
			if (obj is MonoMethod)
			{
				methodInfo = (MethodInfo)obj;
			}
			if (methodInfo == null || !methodInfo.IsVirtual)
			{
				return null;
			}
			MethodInfo baseMethod = methodInfo.GetBaseMethod();
			if (baseMethod == methodInfo)
			{
				return null;
			}
			return baseMethod;
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0005FEE8 File Offset: 0x0005E0E8
		private static AttributeUsageAttribute RetrieveAttributeUsageNoCache(Type attributeType)
		{
			if (attributeType == typeof(AttributeUsageAttribute))
			{
				return new AttributeUsageAttribute(AttributeTargets.Class);
			}
			AttributeUsageAttribute attributeUsageAttribute = null;
			object[] customAttributes = MonoCustomAttrs.GetCustomAttributes(attributeType, typeof(AttributeUsageAttribute), false);
			if (customAttributes.Length == 0)
			{
				if (attributeType.BaseType != null)
				{
					attributeUsageAttribute = MonoCustomAttrs.RetrieveAttributeUsage(attributeType.BaseType);
				}
				if (attributeUsageAttribute != null)
				{
					return attributeUsageAttribute;
				}
				return MonoCustomAttrs.DefaultAttributeUsage;
			}
			else
			{
				if (customAttributes.Length > 1)
				{
					throw new FormatException("Duplicate AttributeUsageAttribute cannot be specified on an attribute type.");
				}
				return (AttributeUsageAttribute)customAttributes[0];
			}
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0005FF64 File Offset: 0x0005E164
		private static AttributeUsageAttribute RetrieveAttributeUsage(Type attributeType)
		{
			AttributeUsageAttribute attributeUsageAttribute = null;
			if (MonoCustomAttrs.usage_cache == null)
			{
				MonoCustomAttrs.usage_cache = new Dictionary<Type, AttributeUsageAttribute>();
			}
			if (MonoCustomAttrs.usage_cache.TryGetValue(attributeType, out attributeUsageAttribute))
			{
				return attributeUsageAttribute;
			}
			attributeUsageAttribute = MonoCustomAttrs.RetrieveAttributeUsageNoCache(attributeType);
			MonoCustomAttrs.usage_cache[attributeType] = attributeUsageAttribute;
			return attributeUsageAttribute;
		}

		// Token: 0x04000CC9 RID: 3273
		private static Assembly corlib;

		// Token: 0x04000CCA RID: 3274
		[ThreadStatic]
		private static Dictionary<Type, AttributeUsageAttribute> usage_cache;

		// Token: 0x04000CCB RID: 3275
		private static readonly AttributeUsageAttribute DefaultAttributeUsage = new AttributeUsageAttribute(AttributeTargets.All);

		// Token: 0x0200021E RID: 542
		private class AttributeInfo
		{
			// Token: 0x0600199F RID: 6559 RVA: 0x0005FFBA File Offset: 0x0005E1BA
			public AttributeInfo(AttributeUsageAttribute usage, int inheritanceLevel)
			{
				this._usage = usage;
				this._inheritanceLevel = inheritanceLevel;
			}

			// Token: 0x17000365 RID: 869
			// (get) Token: 0x060019A0 RID: 6560 RVA: 0x0005FFD0 File Offset: 0x0005E1D0
			public AttributeUsageAttribute Usage
			{
				get
				{
					return this._usage;
				}
			}

			// Token: 0x17000366 RID: 870
			// (get) Token: 0x060019A1 RID: 6561 RVA: 0x0005FFD8 File Offset: 0x0005E1D8
			public int InheritanceLevel
			{
				get
				{
					return this._inheritanceLevel;
				}
			}

			// Token: 0x04000CCC RID: 3276
			private AttributeUsageAttribute _usage;

			// Token: 0x04000CCD RID: 3277
			private int _inheritanceLevel;
		}
	}
}
