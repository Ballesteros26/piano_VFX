using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	/// <summary>Represents the base class for custom attributes.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200012E RID: 302
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_Attribute))]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	public abstract class Attribute : _Attribute
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x00032FF5 File Offset: 0x000311F5
		private static Attribute[] InternalGetCustomAttributes(PropertyInfo element, Type type, bool inherit)
		{
			return (Attribute[])MonoCustomAttrs.GetCustomAttributes(element, type, inherit);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00032FF5 File Offset: 0x000311F5
		private static Attribute[] InternalGetCustomAttributes(EventInfo element, Type type, bool inherit)
		{
			return (Attribute[])MonoCustomAttrs.GetCustomAttributes(element, type, inherit);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00033004 File Offset: 0x00031204
		private static Attribute[] InternalParamGetCustomAttributes(ParameterInfo parameter, Type attributeType, bool inherit)
		{
			if (parameter.Member.MemberType != MemberTypes.Method)
			{
				return null;
			}
			MethodInfo methodInfo = (MethodInfo)parameter.Member;
			MethodInfo baseDefinition = methodInfo.GetBaseDefinition();
			if (attributeType == null)
			{
				attributeType = typeof(Attribute);
			}
			if (methodInfo == baseDefinition)
			{
				return (Attribute[])parameter.GetCustomAttributes(attributeType, inherit);
			}
			List<Type> list = new List<Type>();
			List<Attribute> list2 = new List<Attribute>();
			for (;;)
			{
				foreach (Attribute attribute in (Attribute[])methodInfo.GetParametersInternal()[parameter.Position].GetCustomAttributes(attributeType, false))
				{
					Type type = attribute.GetType();
					if (!list.Contains(type))
					{
						list.Add(type);
						list2.Add(attribute);
					}
				}
				MethodInfo baseMethod = methodInfo.GetBaseMethod();
				if (baseMethod == methodInfo)
				{
					break;
				}
				methodInfo = baseMethod;
			}
			Attribute[] array2 = (Attribute[])Array.CreateInstance(attributeType, list2.Count);
			list2.CopyTo(array2, 0);
			return array2;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x000330F9 File Offset: 0x000312F9
		private static bool InternalIsDefined(PropertyInfo element, Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(element, attributeType, inherit);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000330F9 File Offset: 0x000312F9
		private static bool InternalIsDefined(EventInfo element, Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(element, attributeType, inherit);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00033104 File Offset: 0x00031304
		private static bool InternalParamIsDefined(ParameterInfo parameter, Type attributeType, bool inherit)
		{
			if (parameter.IsDefined(attributeType, inherit))
			{
				return true;
			}
			if (!inherit)
			{
				return false;
			}
			MemberInfo member = parameter.Member;
			if (member.MemberType != MemberTypes.Method)
			{
				return false;
			}
			MethodInfo methodInfo = ((MethodInfo)member).GetBaseMethod();
			while (!methodInfo.GetParametersInternal()[parameter.Position].IsDefined(attributeType, false))
			{
				MethodInfo baseMethod = methodInfo.GetBaseMethod();
				if (baseMethod == methodInfo)
				{
					return false;
				}
				methodInfo = baseMethod;
			}
			return true;
		}

		/// <summary>Retrieves an array of the custom attributes applied to a member of a type. Parameters specify the member, and the type of the custom attribute to search for.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes of type <paramref name="type" /> applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.MemberInfo" /> class that describes a constructor, event, field, method, or property member of a class. </param>
		/// <param name="type">The type, or a base type, of the custom attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A8D RID: 2701 RVA: 0x0003316D File Offset: 0x0003136D
		public static Attribute[] GetCustomAttributes(MemberInfo element, Type type)
		{
			return Attribute.GetCustomAttributes(element, type, true);
		}

		/// <summary>Retrieves an array of the custom attributes applied to a member of a type. Parameters specify the member, the type of the custom attribute to search for, and whether to search ancestors of the member.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes of type <paramref name="type" /> applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.MemberInfo" /> class that describes a constructor, event, field, method, or property member of a class. </param>
		/// <param name="type">The type, or a base type, of the custom attribute to search for.</param>
		/// <param name="inherit">If true, specifies to also search the ancestors of <paramref name="element" /> for custom attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="type" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A8E RID: 2702 RVA: 0x00033178 File Offset: 0x00031378
		public static Attribute[] GetCustomAttributes(MemberInfo element, Type type, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsSubclassOf(typeof(Attribute)) && type != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Type passed in must be derived from System.Attribute or System.Attribute itself."));
			}
			MemberTypes memberType = element.MemberType;
			if (memberType == MemberTypes.Event)
			{
				return Attribute.InternalGetCustomAttributes((EventInfo)element, type, inherit);
			}
			if (memberType == MemberTypes.Property)
			{
				return Attribute.InternalGetCustomAttributes((PropertyInfo)element, type, inherit);
			}
			return element.GetCustomAttributes(type, inherit) as Attribute[];
		}

		/// <summary>Retrieves an array of the custom attributes applied to a member of a type. A parameter specifies the member.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.MemberInfo" /> class that describes a constructor, event, field, method, or property member of a class. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A8F RID: 2703 RVA: 0x0003321A File Offset: 0x0003141A
		public static Attribute[] GetCustomAttributes(MemberInfo element)
		{
			return Attribute.GetCustomAttributes(element, true);
		}

		/// <summary>Retrieves an array of the custom attributes applied to a member of a type. Parameters specify the member, the type of the custom attribute to search for, and whether to search ancestors of the member.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.MemberInfo" /> class that describes a constructor, event, field, method, or property member of a class. </param>
		/// <param name="inherit">If true, specifies to also search the ancestors of <paramref name="element" /> for custom attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A90 RID: 2704 RVA: 0x00033224 File Offset: 0x00031424
		public static Attribute[] GetCustomAttributes(MemberInfo element, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			MemberTypes memberType = element.MemberType;
			if (memberType == MemberTypes.Event)
			{
				return Attribute.InternalGetCustomAttributes((EventInfo)element, typeof(Attribute), inherit);
			}
			if (memberType == MemberTypes.Property)
			{
				return Attribute.InternalGetCustomAttributes((PropertyInfo)element, typeof(Attribute), inherit);
			}
			return element.GetCustomAttributes(typeof(Attribute), inherit) as Attribute[];
		}

		/// <summary>Determines whether any custom attributes are applied to a member of a type. Parameters specify the member, and the type of the custom attribute to search for.</summary>
		/// <returns>true if a custom attribute of type <paramref name="attributeType" /> is applied to <paramref name="element" />; otherwise, false.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.MemberInfo" /> class that describes a constructor, event, field, method, type, or property member of a class. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A91 RID: 2705 RVA: 0x00033299 File Offset: 0x00031499
		public static bool IsDefined(MemberInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType, true);
		}

		/// <summary>Determines whether any custom attributes are applied to a member of a type. Parameters specify the member, the type of the custom attribute to search for, and whether to search ancestors of the member.</summary>
		/// <returns>true if a custom attribute of type <paramref name="attributeType" /> is applied to <paramref name="element" />; otherwise, false.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.MemberInfo" /> class that describes a constructor, event, field, method, type, or property member of a class. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <param name="inherit">If true, specifies to also search the ancestors of <paramref name="element" /> for custom attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A92 RID: 2706 RVA: 0x000332A4 File Offset: 0x000314A4
		public static bool IsDefined(MemberInfo element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Type passed in must be derived from System.Attribute or System.Attribute itself."));
			}
			MemberTypes memberType = element.MemberType;
			if (memberType == MemberTypes.Event)
			{
				return Attribute.InternalIsDefined((EventInfo)element, attributeType, inherit);
			}
			if (memberType == MemberTypes.Property)
			{
				return Attribute.InternalIsDefined((PropertyInfo)element, attributeType, inherit);
			}
			return element.IsDefined(attributeType, inherit);
		}

		/// <summary>Retrieves a custom attribute applied to a member of a type. Parameters specify the member, and the type of the custom attribute to search for.</summary>
		/// <returns>A reference to the single custom attribute of type <paramref name="attributeType" /> that is applied to <paramref name="element" />, or null if there is no such attribute.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.MemberInfo" /> class that describes a constructor, event, field, method, or property member of a class. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A93 RID: 2707 RVA: 0x00033341 File Offset: 0x00031541
		public static Attribute GetCustomAttribute(MemberInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType, true);
		}

		/// <summary>Retrieves a custom attribute applied to a member of a type. Parameters specify the member, the type of the custom attribute to search for, and whether to search ancestors of the member.</summary>
		/// <returns>A reference to the single custom attribute of type <paramref name="attributeType" /> that is applied to <paramref name="element" />, or null if there is no such attribute.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.MemberInfo" /> class that describes a constructor, event, field, method, or property member of a class. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <param name="inherit">If true, specifies to also search the ancestors of <paramref name="element" /> for custom attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A94 RID: 2708 RVA: 0x0003334C File Offset: 0x0003154C
		public static Attribute GetCustomAttribute(MemberInfo element, Type attributeType, bool inherit)
		{
			Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				return null;
			}
			if (customAttributes.Length == 1)
			{
				return customAttributes[0];
			}
			throw new AmbiguousMatchException(Environment.GetResourceString("Multiple custom attributes of the same type found."));
		}

		/// <summary>Retrieves an array of the custom attributes applied to a method parameter. A parameter specifies the method parameter.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.ParameterInfo" /> class that describes a parameter of a member of a class. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A95 RID: 2709 RVA: 0x00033384 File Offset: 0x00031584
		public static Attribute[] GetCustomAttributes(ParameterInfo element)
		{
			return Attribute.GetCustomAttributes(element, true);
		}

		/// <summary>Retrieves an array of the custom attributes applied to a method parameter. Parameters specify the method parameter, and the type of the custom attribute to search for.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes of type <paramref name="attributeType" /> applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.ParameterInfo" /> class that describes a parameter of a member of a class. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A96 RID: 2710 RVA: 0x0003338D File Offset: 0x0003158D
		public static Attribute[] GetCustomAttributes(ParameterInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType, true);
		}

		/// <summary>Retrieves an array of the custom attributes applied to a method parameter. Parameters specify the method parameter, the type of the custom attribute to search for, and whether to search ancestors of the method parameter.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes of type <paramref name="attributeType" /> applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.ParameterInfo" /> class that describes a parameter of a member of a class. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <param name="inherit">If true, specifies to also search the ancestors of <paramref name="element" /> for custom attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A97 RID: 2711 RVA: 0x00033398 File Offset: 0x00031598
		public static Attribute[] GetCustomAttributes(ParameterInfo element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Type passed in must be derived from System.Attribute or System.Attribute itself."));
			}
			if (element.Member == null)
			{
				throw new ArgumentException(Environment.GetResourceString("The ParameterInfo object is not valid."), "element");
			}
			if (element.Member.MemberType == MemberTypes.Method && inherit)
			{
				return Attribute.InternalParamGetCustomAttributes(element, attributeType, inherit);
			}
			return element.GetCustomAttributes(attributeType, inherit) as Attribute[];
		}

		/// <summary>Retrieves an array of the custom attributes applied to a method parameter. Parameters specify the method parameter, and whether to search ancestors of the method parameter.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.ParameterInfo" /> class that describes a parameter of a member of a class. </param>
		/// <param name="inherit">If true, specifies to also search the ancestors of <paramref name="element" /> for custom attributes. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Reflection.ParameterInfo.Member" /> property of <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A98 RID: 2712 RVA: 0x00033448 File Offset: 0x00031648
		public static Attribute[] GetCustomAttributes(ParameterInfo element, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (element.Member == null)
			{
				throw new ArgumentException(Environment.GetResourceString("The ParameterInfo object is not valid."), "element");
			}
			if (element.Member.MemberType == MemberTypes.Method && inherit)
			{
				return Attribute.InternalParamGetCustomAttributes(element, null, inherit);
			}
			return element.GetCustomAttributes(typeof(Attribute), inherit) as Attribute[];
		}

		/// <summary>Determines whether any custom attributes are applied to a method parameter. Parameters specify the method parameter, and the type of the custom attribute to search for.</summary>
		/// <returns>true if a custom attribute of type <paramref name="attributeType" /> is applied to <paramref name="element" />; otherwise, false.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.ParameterInfo" /> class that describes a parameter of a member of a class. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A99 RID: 2713 RVA: 0x000334B7 File Offset: 0x000316B7
		public static bool IsDefined(ParameterInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType, true);
		}

		/// <summary>Determines whether any custom attributes are applied to a method parameter. Parameters specify the method parameter, the type of the custom attribute to search for, and whether to search ancestors of the method parameter.</summary>
		/// <returns>true if a custom attribute of type <paramref name="attributeType" /> is applied to <paramref name="element" />; otherwise, false.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.ParameterInfo" /> class that describes a parameter of a member of a class. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <param name="inherit">If true, specifies to also search the ancestors of <paramref name="element" /> for custom attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.ExecutionEngineException">
		///   <paramref name="element" /> is not a method, constructor, or type. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A9A RID: 2714 RVA: 0x000334C4 File Offset: 0x000316C4
		public static bool IsDefined(ParameterInfo element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Type passed in must be derived from System.Attribute or System.Attribute itself."));
			}
			MemberTypes memberType = element.Member.MemberType;
			if (memberType == MemberTypes.Constructor)
			{
				return element.IsDefined(attributeType, false);
			}
			if (memberType == MemberTypes.Method)
			{
				return Attribute.InternalParamIsDefined(element, attributeType, inherit);
			}
			if (memberType != MemberTypes.Property)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid type for ParameterInfo member in Attribute class."));
			}
			return element.IsDefined(attributeType, false);
		}

		/// <summary>Retrieves a custom attribute applied to a method parameter. Parameters specify the method parameter, and the type of the custom attribute to search for.</summary>
		/// <returns>A reference to the single custom attribute of type <paramref name="attributeType" /> that is applied to <paramref name="element" />, or null if there is no such attribute.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.ParameterInfo" /> class that describes a parameter of a member of a class. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A9B RID: 2715 RVA: 0x0003356C File Offset: 0x0003176C
		public static Attribute GetCustomAttribute(ParameterInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType, true);
		}

		/// <summary>Retrieves a custom attribute applied to a method parameter. Parameters specify the method parameter, the type of the custom attribute to search for, and whether to search ancestors of the method parameter.</summary>
		/// <returns>A reference to the single custom attribute of type <paramref name="attributeType" /> that is applied to <paramref name="element" />, or null if there is no such attribute.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.ParameterInfo" /> class that describes a parameter of a member of a class. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <param name="inherit">If true, specifies to also search the ancestors of <paramref name="element" /> for custom attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A9C RID: 2716 RVA: 0x00033578 File Offset: 0x00031778
		public static Attribute GetCustomAttribute(ParameterInfo element, Type attributeType, bool inherit)
		{
			Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				return null;
			}
			if (customAttributes.Length == 0)
			{
				return null;
			}
			if (customAttributes.Length == 1)
			{
				return customAttributes[0];
			}
			throw new AmbiguousMatchException(Environment.GetResourceString("Multiple custom attributes of the same type found."));
		}

		/// <summary>Retrieves an array of the custom attributes applied to a module. Parameters specify the module, and the type of the custom attribute to search for.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes of type <paramref name="attributeType" /> applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Module" /> class that describes a portable executable file. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A9D RID: 2717 RVA: 0x000335B6 File Offset: 0x000317B6
		public static Attribute[] GetCustomAttributes(Module element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType, true);
		}

		/// <summary>Retrieves an array of the custom attributes applied to a module. A parameter specifies the module.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Module" /> class that describes a portable executable file. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A9E RID: 2718 RVA: 0x000335C0 File Offset: 0x000317C0
		public static Attribute[] GetCustomAttributes(Module element)
		{
			return Attribute.GetCustomAttributes(element, true);
		}

		/// <summary>Retrieves an array of the custom attributes applied to a module. Parameters specify the module, and an ignored search option.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Module" /> class that describes a portable executable file. </param>
		/// <param name="inherit">This parameter is ignored, and does not affect the operation of this method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A9F RID: 2719 RVA: 0x000335C9 File Offset: 0x000317C9
		public static Attribute[] GetCustomAttributes(Module element, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (Attribute[])element.GetCustomAttributes(typeof(Attribute), inherit);
		}

		/// <summary>Retrieves an array of the custom attributes applied to a module. Parameters specify the module, the type of the custom attribute to search for, and an ignored search option.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes of type <paramref name="attributeType" /> applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Module" /> class that describes a portable executable file. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <param name="inherit">This parameter is ignored, and does not affect the operation of this method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AA0 RID: 2720 RVA: 0x000335F8 File Offset: 0x000317F8
		public static Attribute[] GetCustomAttributes(Module element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Type passed in must be derived from System.Attribute or System.Attribute itself."));
			}
			return (Attribute[])element.GetCustomAttributes(attributeType, inherit);
		}

		/// <summary>Determines whether any custom attributes of a specified type are applied to a module. Parameters specify the module, and the type of the custom attribute to search for.</summary>
		/// <returns>true if a custom attribute of type <paramref name="attributeType" /> is applied to <paramref name="element" />; otherwise, false.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Module" /> class that describes a portable executable file. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AA1 RID: 2721 RVA: 0x0003366E File Offset: 0x0003186E
		public static bool IsDefined(Module element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType, false);
		}

		/// <summary>Determines whether any custom attributes are applied to a module. Parameters specify the module, the type of the custom attribute to search for, and an ignored search option. </summary>
		/// <returns>true if a custom attribute of type <paramref name="attributeType" /> is applied to <paramref name="element" />; otherwise, false.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Module" /> class that describes a portable executable file. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <param name="inherit">This parameter is ignored, and does not affect the operation of this method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AA2 RID: 2722 RVA: 0x00033678 File Offset: 0x00031878
		public static bool IsDefined(Module element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Type passed in must be derived from System.Attribute or System.Attribute itself."));
			}
			return element.IsDefined(attributeType, false);
		}

		/// <summary>Retrieves a custom attribute applied to a module. Parameters specify the module, and the type of the custom attribute to search for.</summary>
		/// <returns>A reference to the single custom attribute of type <paramref name="attributeType" /> that is applied to <paramref name="element" />, or null if there is no such attribute.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Module" /> class that describes a portable executable file. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AA3 RID: 2723 RVA: 0x000336E9 File Offset: 0x000318E9
		public static Attribute GetCustomAttribute(Module element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType, true);
		}

		/// <summary>Retrieves a custom attribute applied to a module. Parameters specify the module, the type of the custom attribute to search for, and an ignored search option.</summary>
		/// <returns>A reference to the single custom attribute of type <paramref name="attributeType" /> that is applied to <paramref name="element" />, or null if there is no such attribute.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Module" /> class that describes a portable executable file. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <param name="inherit">This parameter is ignored, and does not affect the operation of this method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AA4 RID: 2724 RVA: 0x000336F4 File Offset: 0x000318F4
		public static Attribute GetCustomAttribute(Module element, Type attributeType, bool inherit)
		{
			Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				return null;
			}
			if (customAttributes.Length == 1)
			{
				return customAttributes[0];
			}
			throw new AmbiguousMatchException(Environment.GetResourceString("Multiple custom attributes of the same type found."));
		}

		/// <summary>Retrieves an array of the custom attributes applied to an assembly. Parameters specify the assembly, and the type of the custom attribute to search for.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes of type <paramref name="attributeType" /> applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Assembly" /> class that describes a reusable collection of modules. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AA5 RID: 2725 RVA: 0x0003372C File Offset: 0x0003192C
		public static Attribute[] GetCustomAttributes(Assembly element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType, true);
		}

		/// <summary>Retrieves an array of the custom attributes applied to an assembly. Parameters specify the assembly, the type of the custom attribute to search for, and an ignored search option.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes of type <paramref name="attributeType" /> applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Assembly" /> class that describes a reusable collection of modules. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <param name="inherit">This parameter is ignored, and does not affect the operation of this method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AA6 RID: 2726 RVA: 0x00033738 File Offset: 0x00031938
		public static Attribute[] GetCustomAttributes(Assembly element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Type passed in must be derived from System.Attribute or System.Attribute itself."));
			}
			return (Attribute[])element.GetCustomAttributes(attributeType, inherit);
		}

		/// <summary>Retrieves an array of the custom attributes applied to an assembly. A parameter specifies the assembly.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Assembly" /> class that describes a reusable collection of modules. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AA7 RID: 2727 RVA: 0x000337AE File Offset: 0x000319AE
		public static Attribute[] GetCustomAttributes(Assembly element)
		{
			return Attribute.GetCustomAttributes(element, true);
		}

		/// <summary>Retrieves an array of the custom attributes applied to an assembly. Parameters specify the assembly, and an ignored search option.</summary>
		/// <returns>An <see cref="T:System.Attribute" /> array that contains the custom attributes applied to <paramref name="element" />, or an empty array if no such custom attributes exist.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Assembly" /> class that describes a reusable collection of modules. </param>
		/// <param name="inherit">This parameter is ignored, and does not affect the operation of this method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AA8 RID: 2728 RVA: 0x000337B7 File Offset: 0x000319B7
		public static Attribute[] GetCustomAttributes(Assembly element, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (Attribute[])element.GetCustomAttributes(typeof(Attribute), inherit);
		}

		/// <summary>Determines whether any custom attributes are applied to an assembly. Parameters specify the assembly, and the type of the custom attribute to search for.</summary>
		/// <returns>true if a custom attribute of type <paramref name="attributeType" /> is applied to <paramref name="element" />; otherwise, false.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Assembly" /> class that describes a reusable collection of modules. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AA9 RID: 2729 RVA: 0x000337E3 File Offset: 0x000319E3
		public static bool IsDefined(Assembly element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType, true);
		}

		/// <summary>Determines whether any custom attributes are applied to an assembly. Parameters specify the assembly, the type of the custom attribute to search for, and an ignored search option.</summary>
		/// <returns>true if a custom attribute of type <paramref name="attributeType" /> is applied to <paramref name="element" />; otherwise, false.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Assembly" /> class that describes a reusable collection of modules. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <param name="inherit">This parameter is ignored, and does not affect the operation of this method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AAA RID: 2730 RVA: 0x000337F0 File Offset: 0x000319F0
		public static bool IsDefined(Assembly element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(Environment.GetResourceString("Type passed in must be derived from System.Attribute or System.Attribute itself."));
			}
			return element.IsDefined(attributeType, false);
		}

		/// <summary>Retrieves a custom attribute applied to a specified assembly. Parameters specify the assembly and the type of the custom attribute to search for.</summary>
		/// <returns>A reference to the single custom attribute of type <paramref name="attributeType" /> that is applied to <paramref name="element" />, or null if there is no such attribute.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Assembly" /> class that describes a reusable collection of modules. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AAB RID: 2731 RVA: 0x00033861 File Offset: 0x00031A61
		public static Attribute GetCustomAttribute(Assembly element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType, true);
		}

		/// <summary>Retrieves a custom attribute applied to an assembly. Parameters specify the assembly, the type of the custom attribute to search for, and an ignored search option.</summary>
		/// <returns>A reference to the single custom attribute of type <paramref name="attributeType" /> that is applied to <paramref name="element" />, or null if there is no such attribute.</returns>
		/// <param name="element">An object derived from the <see cref="T:System.Reflection.Assembly" /> class that describes a reusable collection of modules. </param>
		/// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
		/// <param name="inherit">This parameter is ignored, and does not affect the operation of this method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000AAC RID: 2732 RVA: 0x0003386C File Offset: 0x00031A6C
		public static Attribute GetCustomAttribute(Assembly element, Type attributeType, bool inherit)
		{
			Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				return null;
			}
			if (customAttributes.Length == 1)
			{
				return customAttributes[0];
			}
			throw new AmbiguousMatchException(Environment.GetResourceString("Multiple custom attributes of the same type found."));
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AAE RID: 2734 RVA: 0x000338A4 File Offset: 0x00031AA4
		[SecuritySafeCritical]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			RuntimeType runtimeType = (RuntimeType)base.GetType();
			if ((RuntimeType)obj.GetType() != runtimeType)
			{
				return false;
			}
			FieldInfo[] fields = runtimeType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			for (int i = 0; i < fields.Length; i++)
			{
				object obj2 = ((RtFieldInfo)fields[i]).UnsafeGetValue(this);
				object obj3 = ((RtFieldInfo)fields[i]).UnsafeGetValue(obj);
				if (!Attribute.AreFieldValuesEqual(obj2, obj3))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00033920 File Offset: 0x00031B20
		private static bool AreFieldValuesEqual(object thisValue, object thatValue)
		{
			if (thisValue == null && thatValue == null)
			{
				return true;
			}
			if (thisValue == null || thatValue == null)
			{
				return false;
			}
			if (thisValue.GetType().IsArray)
			{
				if (!thisValue.GetType().Equals(thatValue.GetType()))
				{
					return false;
				}
				Array array = thisValue as Array;
				Array array2 = thatValue as Array;
				if (array.Length != array2.Length)
				{
					return false;
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (!Attribute.AreFieldValuesEqual(array.GetValue(i), array2.GetValue(i)))
					{
						return false;
					}
				}
			}
			else if (!thisValue.Equals(thatValue))
			{
				return false;
			}
			return true;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AB0 RID: 2736 RVA: 0x000339B4 File Offset: 0x00031BB4
		[SecuritySafeCritical]
		public override int GetHashCode()
		{
			Type type = base.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			object obj = null;
			for (int i = 0; i < fields.Length; i++)
			{
				object obj2 = ((RtFieldInfo)fields[i]).UnsafeGetValue(this);
				if (obj2 != null && !obj2.GetType().IsArray)
				{
					obj = obj2;
				}
				if (obj != null)
				{
					break;
				}
			}
			if (obj != null)
			{
				return obj.GetHashCode();
			}
			return type.GetHashCode();
		}

		/// <summary>When implemented in a derived class, gets a unique identifier for this <see cref="T:System.Attribute" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a unique identifier for the attribute.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00033A19 File Offset: 0x00031C19
		public virtual object TypeId
		{
			get
			{
				return base.GetType();
			}
		}

		/// <summary>When overridden in a derived class, returns a value that indicates whether this instance equals a specified object.</summary>
		/// <returns>true if this instance equals <paramref name="obj" />; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance of <see cref="T:System.Attribute" />. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AB2 RID: 2738 RVA: 0x00033A21 File Offset: 0x00031C21
		public virtual bool Match(object obj)
		{
			return this.Equals(obj);
		}

		/// <summary>When overridden in a derived class, indicates whether the value of this instance is the default value for the derived class.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000AB3 RID: 2739 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsDefaultAttribute()
		{
			return false;
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002126B File Offset: 0x0001F46B
		void _Attribute.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002126B File Offset: 0x0001F46B
		void _Attribute.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array that receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06000AB6 RID: 2742 RVA: 0x0002126B File Offset: 0x0001F46B
		void _Attribute.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002126B File Offset: 0x0001F46B
		void _Attribute.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}
	}
}
