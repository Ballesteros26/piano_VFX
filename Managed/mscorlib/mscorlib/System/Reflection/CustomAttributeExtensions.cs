using System;
using System.Collections.Generic;

namespace System.Reflection
{
	/// <summary>Contains static methods for retrieving custom attributes.</summary>
	// Token: 0x020002BE RID: 702
	public static class CustomAttributeExtensions
	{
		/// <summary>Retrieves a custom attribute of a specified type that is applied to a specified assembly.</summary>
		/// <returns>A custom attribute that matches <paramref name="attributeType" />, or null if no such attribute is found.</returns>
		/// <param name="element">The assembly to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		// Token: 0x06001FEA RID: 8170 RVA: 0x0007DA78 File Offset: 0x0007BC78
		public static Attribute GetCustomAttribute(this Assembly element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		/// <summary>Retrieves a custom attribute of a specified type that is applied to a specified module.</summary>
		/// <returns>A custom attribute that matches <paramref name="attributeType" />, or null if no such attribute is found.</returns>
		/// <param name="element">The module to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		// Token: 0x06001FEB RID: 8171 RVA: 0x0007DA81 File Offset: 0x0007BC81
		public static Attribute GetCustomAttribute(this Module element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		/// <summary>Retrieves a custom attribute of a specified type that is applied to a specified member.</summary>
		/// <returns>A custom attribute that matches <paramref name="attributeType" />, or null if no such attribute is found.</returns>
		/// <param name="element">The member to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06001FEC RID: 8172 RVA: 0x0007DA8A File Offset: 0x0007BC8A
		public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		/// <summary>Retrieves a custom attribute of a specified type that is applied to a specified parameter.</summary>
		/// <returns>A custom attribute that matches <paramref name="attributeType" />, or null if no such attribute is found.</returns>
		/// <param name="element">The parameter to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06001FED RID: 8173 RVA: 0x0007DA93 File Offset: 0x0007BC93
		public static Attribute GetCustomAttribute(this ParameterInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		/// <summary>Retrieves a custom attribute of a specified type that is applied to a specified assembly. </summary>
		/// <returns>A custom attribute that matches <paramref name="T" />, or null if no such attribute is found.</returns>
		/// <param name="element">The assembly to inspect.</param>
		/// <typeparam name="T">The type of attribute to search for.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		// Token: 0x06001FEE RID: 8174 RVA: 0x0007DA9C File Offset: 0x0007BC9C
		public static T GetCustomAttribute<T>(this Assembly element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		/// <summary>Retrieves a custom attribute of a specified type that is applied to a specified module.</summary>
		/// <returns>A custom attribute that matches <paramref name="T" />, or null if no such attribute is found.</returns>
		/// <param name="element">The module to inspect.</param>
		/// <typeparam name="T">The type of attribute to search for.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		// Token: 0x06001FEF RID: 8175 RVA: 0x0007DAB3 File Offset: 0x0007BCB3
		public static T GetCustomAttribute<T>(this Module element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		/// <summary>Retrieves a custom attribute of a specified type that is applied to a specified member.</summary>
		/// <returns>A custom attribute that matches <paramref name="T" />, or null if no such attribute is found.</returns>
		/// <param name="element">The member to inspect.</param>
		/// <typeparam name="T">The type of attribute to search for.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06001FF0 RID: 8176 RVA: 0x0007DACA File Offset: 0x0007BCCA
		public static T GetCustomAttribute<T>(this MemberInfo element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		/// <summary>Retrieves a custom attribute of a specified type that is applied to a specified parameter.</summary>
		/// <returns>A custom attribute that matches <paramref name="T" />, or null if no such attribute is found.</returns>
		/// <param name="element">The parameter to inspect.</param>
		/// <typeparam name="T">The type of attribute to search for.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06001FF1 RID: 8177 RVA: 0x0007DAE1 File Offset: 0x0007BCE1
		public static T GetCustomAttribute<T>(this ParameterInfo element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		/// <summary>Retrieves a custom attribute of a specified type that is applied to a specified member, and optionally inspects the ancestors of that member.</summary>
		/// <returns>A custom attribute that matches <paramref name="attributeType" />, or null if no such attribute is found.</returns>
		/// <param name="element">The member to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <param name="inherit">true to inspect the ancestors of <paramref name="element" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06001FF2 RID: 8178 RVA: 0x0007DAF8 File Offset: 0x0007BCF8
		public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttribute(element, attributeType, inherit);
		}

		/// <summary>Retrieves a custom attribute of a specified type that is applied to a specified parameter, and optionally inspects the ancestors of that parameter.</summary>
		/// <returns>A custom attribute matching <paramref name="attributeType" />, or null if no such attribute is found.</returns>
		/// <param name="element">The parameter to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <param name="inherit">true to inspect the ancestors of <paramref name="element" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06001FF3 RID: 8179 RVA: 0x0007DB02 File Offset: 0x0007BD02
		public static Attribute GetCustomAttribute(this ParameterInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttribute(element, attributeType, inherit);
		}

		/// <summary>Retrieves a custom attribute of a specified type that is applied to a specified member, and optionally inspects the ancestors of that member.</summary>
		/// <returns>A custom attribute that matches <paramref name="T" />, or null if no such attribute is found.</returns>
		/// <param name="element">The member to inspect.</param>
		/// <param name="inherit">true to inspect the ancestors of <paramref name="element" />; otherwise, false. </param>
		/// <typeparam name="T">The type of attribute to search for.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06001FF4 RID: 8180 RVA: 0x0007DB0C File Offset: 0x0007BD0C
		public static T GetCustomAttribute<T>(this MemberInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T), inherit));
		}

		/// <summary>Retrieves a custom attribute of a specified type that is applied to a specified parameter, and optionally inspects the ancestors of that parameter.</summary>
		/// <returns>A custom attribute that matches <paramref name="T" />, or null if no such attribute is found.</returns>
		/// <param name="element">The parameter to inspect.</param>
		/// <param name="inherit">true to inspect the ancestors of <paramref name="element" />; otherwise, false. </param>
		/// <typeparam name="T">The type of attribute to search for.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one of the requested attributes was found. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06001FF5 RID: 8181 RVA: 0x0007DB24 File Offset: 0x0007BD24
		public static T GetCustomAttribute<T>(this ParameterInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T), inherit));
		}

		/// <summary>Retrieves a collection of custom attributes that are applied to a specified assembly.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The assembly to inspect.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		// Token: 0x06001FF6 RID: 8182 RVA: 0x0007DB3C File Offset: 0x0007BD3C
		public static IEnumerable<Attribute> GetCustomAttributes(this Assembly element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		/// <summary>Retrieves a collection of custom attributes that are applied to a specified module.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The module to inspect.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		// Token: 0x06001FF7 RID: 8183 RVA: 0x0007DB44 File Offset: 0x0007BD44
		public static IEnumerable<Attribute> GetCustomAttributes(this Module element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		/// <summary>Retrieves a collection of custom attributes that are applied to a specified member.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The member to inspect.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06001FF8 RID: 8184 RVA: 0x0007DB4C File Offset: 0x0007BD4C
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		/// <summary>Retrieves a collection of custom attributes that are applied to a specified parameter.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The parameter to inspect.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06001FF9 RID: 8185 RVA: 0x0007DB54 File Offset: 0x0007BD54
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		/// <summary>Retrieves a collection of custom attributes that are applied to a specified member, and optionally inspects the ancestors of that member.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" /> that match the specified criteria, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The member to inspect.</param>
		/// <param name="inherit">true to inspect the ancestors of <paramref name="element" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06001FFA RID: 8186 RVA: 0x0007DB5C File Offset: 0x0007BD5C
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, inherit);
		}

		/// <summary>Retrieves a collection of custom attributes that are applied to a specified parameter, and optionally inspects the ancestors of that parameter.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The parameter to inspect.</param>
		/// <param name="inherit">true to inspect the ancestors of <paramref name="element" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06001FFB RID: 8187 RVA: 0x0007DB65 File Offset: 0x0007BD65
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, inherit);
		}

		/// <summary>Retrieves a collection of custom attributes of a specified type that are applied to a specified assembly.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" /> and that match <paramref name="attributeType" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The assembly to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		// Token: 0x06001FFC RID: 8188 RVA: 0x0007DB6E File Offset: 0x0007BD6E
		public static IEnumerable<Attribute> GetCustomAttributes(this Assembly element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		/// <summary>Retrieves a collection of custom attributes of a specified type that are applied to a specified module.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" /> and that match <paramref name="attributeType" />, or an empty collection if no such attributes exist.</returns>
		/// <param name="element">The module to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		// Token: 0x06001FFD RID: 8189 RVA: 0x0007DB77 File Offset: 0x0007BD77
		public static IEnumerable<Attribute> GetCustomAttributes(this Module element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		/// <summary>Retrieves a collection of custom attributes of a specified type that are applied to a specified member.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" /> and that match <paramref name="attributeType" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The member to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06001FFE RID: 8190 RVA: 0x0007DB80 File Offset: 0x0007BD80
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		/// <summary>Retrieves a collection of custom attributes of a specified type that are applied to a specified parameter.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" /> and that match <paramref name="attributeType" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The parameter to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06001FFF RID: 8191 RVA: 0x0007DB89 File Offset: 0x0007BD89
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		/// <summary>Retrieves a collection of custom attributes of a specified type that are applied to a specified assembly. </summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" /> and that match <paramref name="T" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The assembly to inspect.</param>
		/// <typeparam name="T">The type of attribute to search for.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		// Token: 0x06002000 RID: 8192 RVA: 0x0007DB92 File Offset: 0x0007BD92
		public static IEnumerable<T> GetCustomAttributes<T>(this Assembly element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		/// <summary>Retrieves a collection of custom attributes of a specified type that are applied to a specified module.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" /> and that match <paramref name="T" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The module to inspect.</param>
		/// <typeparam name="T">The type of attribute to search for.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		// Token: 0x06002001 RID: 8193 RVA: 0x0007DBA9 File Offset: 0x0007BDA9
		public static IEnumerable<T> GetCustomAttributes<T>(this Module element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		/// <summary>Retrieves a collection of custom attributes of a specified type that are applied to a specified member.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" /> and that match <paramref name="T" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The member to inspect.</param>
		/// <typeparam name="T">The type of attribute to search for.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06002002 RID: 8194 RVA: 0x0007DBC0 File Offset: 0x0007BDC0
		public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		/// <summary>Retrieves a collection of custom attributes of a specified type that are applied to a specified parameter.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" /> and that match <paramref name="T" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The parameter to inspect.</param>
		/// <typeparam name="T">The type of attribute to search for.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06002003 RID: 8195 RVA: 0x0007DBD7 File Offset: 0x0007BDD7
		public static IEnumerable<T> GetCustomAttributes<T>(this ParameterInfo element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		/// <summary>Retrieves a collection of custom attributes of a specified type that are applied to a specified member, and optionally inspects the ancestors of that member.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" /> and that match <paramref name="attributeType" />, or an empty collection if no such attributes exist.</returns>
		/// <param name="element">The member to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <param name="inherit">true to inspect the ancestors of <paramref name="element" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06002004 RID: 8196 RVA: 0x0007DBEE File Offset: 0x0007BDEE
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, attributeType, inherit);
		}

		/// <summary>Retrieves a collection of custom attributes of a specified type that are applied to a specified parameter, and optionally inspects the ancestors of that parameter.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" /> and that match <paramref name="attributeType" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The parameter to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <param name="inherit">true to inspect the ancestors of <paramref name="element" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06002005 RID: 8197 RVA: 0x0007DBF8 File Offset: 0x0007BDF8
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, attributeType, inherit);
		}

		/// <summary>Retrieves a collection of custom attributes of a specified type that are applied to a specified member, and optionally inspects the ancestors of that member.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" /> and that match <paramref name="T" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The member to inspect.</param>
		/// <param name="inherit">true to inspect the ancestors of <paramref name="element" />; otherwise, false. </param>
		/// <typeparam name="T">The type of attribute to search for.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06002006 RID: 8198 RVA: 0x0007DC02 File Offset: 0x0007BE02
		public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element, bool inherit) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T), inherit);
		}

		/// <summary>Retrieves a collection of custom attributes of a specified type that are applied to a specified parameter, and optionally inspects the ancestors of that parameter.</summary>
		/// <returns>A collection of the custom attributes that are applied to <paramref name="element" /> and that match <paramref name="T" />, or an empty collection if no such attributes exist. </returns>
		/// <param name="element">The parameter to inspect.</param>
		/// <param name="inherit">true to inspect the ancestors of <paramref name="element" />; otherwise, false. </param>
		/// <typeparam name="T">The type of attribute to search for.</typeparam>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded. </exception>
		// Token: 0x06002007 RID: 8199 RVA: 0x0007DC1A File Offset: 0x0007BE1A
		public static IEnumerable<T> GetCustomAttributes<T>(this ParameterInfo element, bool inherit) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T), inherit);
		}

		/// <summary>Indicates whether custom attributes of a specified type are applied to a specified assembly.</summary>
		/// <returns>true if an attribute of the specified type is applied to <paramref name="element" />; otherwise, false.</returns>
		/// <param name="element">The assembly to inspect.</param>
		/// <param name="attributeType">The type of the attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		// Token: 0x06002008 RID: 8200 RVA: 0x0007DC32 File Offset: 0x0007BE32
		public static bool IsDefined(this Assembly element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		/// <summary>Indicates whether custom attributes of a specified type are applied to a specified module.</summary>
		/// <returns>true if an attribute of the specified type is applied to <paramref name="element" />; otherwise, false.</returns>
		/// <param name="element">The module to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		// Token: 0x06002009 RID: 8201 RVA: 0x0007DC3B File Offset: 0x0007BE3B
		public static bool IsDefined(this Module element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		/// <summary>Indicates whether custom attributes of a specified type are applied to a specified member.</summary>
		/// <returns>true if an attribute of the specified type is applied to <paramref name="element" />; otherwise, false.</returns>
		/// <param name="element">The member to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		// Token: 0x0600200A RID: 8202 RVA: 0x0007DC44 File Offset: 0x0007BE44
		public static bool IsDefined(this MemberInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		/// <summary>Indicates whether custom attributes of a specified type are applied to a specified parameter.</summary>
		/// <returns>true if an attribute of the specified type is applied to <paramref name="element" />; otherwise, false.</returns>
		/// <param name="element">The parameter to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		// Token: 0x0600200B RID: 8203 RVA: 0x0007DC4D File Offset: 0x0007BE4D
		public static bool IsDefined(this ParameterInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		/// <summary>Indicates whether custom attributes of a specified type are applied to a specified member, and, optionally, applied to its ancestors.</summary>
		/// <returns>true if an attribute of the specified type is applied to <paramref name="element" />; otherwise, false.</returns>
		/// <param name="element">The member to inspect.</param>
		/// <param name="attributeType">The type of the attribute to search for.</param>
		/// <param name="inherit">true to inspect the ancestors of <paramref name="element" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="element" /> is not a constructor, method, property, event, type, or field. </exception>
		// Token: 0x0600200C RID: 8204 RVA: 0x0007DC56 File Offset: 0x0007BE56
		public static bool IsDefined(this MemberInfo element, Type attributeType, bool inherit)
		{
			return Attribute.IsDefined(element, attributeType, inherit);
		}

		/// <summary>Indicates whether custom attributes of a specified type are applied to a specified parameter, and, optionally, applied to its ancestors.</summary>
		/// <returns>true if an attribute of the specified type is applied to <paramref name="element" />; otherwise, false.</returns>
		/// <param name="element">The parameter to inspect.</param>
		/// <param name="attributeType">The type of attribute to search for.</param>
		/// <param name="inherit">true to inspect the ancestors of <paramref name="element" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> or <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not derived from <see cref="T:System.Attribute" />. </exception>
		// Token: 0x0600200D RID: 8205 RVA: 0x0007DC60 File Offset: 0x0007BE60
		public static bool IsDefined(this ParameterInfo element, Type attributeType, bool inherit)
		{
			return Attribute.IsDefined(element, attributeType, inherit);
		}
	}
}
