using System;
using System.ComponentModel;
using System.Reflection;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Contains helper functions to convert property values to and from strings.</summary>
	// Token: 0x0200021D RID: 541
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public static class PropertyConverter
	{
		/// <summary>Converts the string representation to a value of the specified enumeration type. </summary>
		/// <returns>An enumeration of type <paramref name="enumType" />.</returns>
		/// <param name="enumType">A <see cref="T:System.Type" /> that represents the enumeration type to create from the <paramref name="value" /> parameter.</param>
		/// <param name="value">The <see cref="T:System.String" /> that represents a value in the enumerator.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.- or -<paramref name="value" /> is either an empty string ("") or contains only white spaces.- or - <paramref name="value" /> is a name, but not one of the named constants defined for the enumeration.</exception>
		// Token: 0x06001642 RID: 5698 RVA: 0x0003BA48 File Offset: 0x00039C48
		public static object EnumFromString(Type enumType, string value)
		{
			object obj = null;
			try
			{
				obj = Enum.Parse(enumType, value, true);
			}
			catch
			{
				obj = null;
			}
			return obj;
		}

		/// <summary>Converts the value of the specified enumeration type to its equivalent string representation.</summary>
		/// <returns>The string representation of <paramref name="enumValue" />.</returns>
		/// <param name="enumType">A <see cref="T:System.Type" /> that represents the enumeration type of <paramref name="enumValue" />. </param>
		/// <param name="enumValue">The value to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="enumType" /> or <paramref name="enumValue" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="enumType" /> parameter is not an <see cref="T:System.Enum" /> type.- or - The <paramref name="enumValue" /> parameter contains a value from an enumeration that differs in type from <paramref name="enumType" />.- or - The type of <paramref name="enumValue" /> is not an underlying type of <paramref name="enumType" />. </exception>
		// Token: 0x06001643 RID: 5699 RVA: 0x0003BA78 File Offset: 0x00039C78
		public static string EnumToString(Type enumType, object enumValue)
		{
			return Enum.Format(enumType, enumValue, "G");
		}

		/// <summary>Converts the string value to the specified object type.</summary>
		/// <returns>An object of type <paramref name="objType" />.</returns>
		/// <param name="objType">The <see cref="T:System.Type" /> to create from <paramref name="value" />.</param>
		/// <param name="propertyInfo">The properties to use during conversion.</param>
		/// <param name="value">The <see cref="T:System.String" /> to convert into an object.</param>
		/// <exception cref="T:System.Web.HttpException">An object of the type specified by <paramref name="objType" /> cannot be created from the <paramref name="value" /> parameter.</exception>
		// Token: 0x06001644 RID: 5700 RVA: 0x0003BA88 File Offset: 0x00039C88
		public static object ObjectFromString(Type objType, MemberInfo propertyInfo, string value)
		{
			if (objType == typeof(string))
			{
				return value;
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(propertyInfo.ReflectedType).Find(propertyInfo.Name, false);
			if (propertyDescriptor.Converter == null || !propertyDescriptor.Converter.CanConvertFrom(typeof(string)))
			{
				throw new HttpException(global::Locale.GetText("Cannot create an object of type '{0}' from its string representation '{1}' for the '{2}' property", new object[] { objType, value, propertyInfo.Name }));
			}
			return propertyDescriptor.Converter.ConvertFromInvariantString(value);
		}
	}
}
