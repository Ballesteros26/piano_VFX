using System;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;

namespace System.Security.Authentication.ExtendedProtection.Configuration
{
	// Token: 0x02000389 RID: 905
	internal static class ConfigUtil
	{
		// Token: 0x06001B6F RID: 7023 RVA: 0x0006D558 File Offset: 0x0006B758
		internal static T GetCustomAttribute<T>(MemberInfo m, bool inherit)
		{
			object[] customAttributes = m.GetCustomAttributes(typeof(T), false);
			if (customAttributes.Length == 0)
			{
				return default(T);
			}
			return (T)((object)customAttributes[0]);
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x0006D590 File Offset: 0x0006B790
		internal static ConfigurationProperty BuildProperty(Type t, string name)
		{
			PropertyInfo property = t.GetProperty(name);
			ConfigurationPropertyAttribute customAttribute = ConfigUtil.GetCustomAttribute<ConfigurationPropertyAttribute>(property, false);
			TypeConverterAttribute customAttribute2 = ConfigUtil.GetCustomAttribute<TypeConverterAttribute>(property, false);
			ConfigurationValidatorAttribute customAttribute3 = ConfigUtil.GetCustomAttribute<ConfigurationValidatorAttribute>(property, false);
			return new ConfigurationProperty(customAttribute.Name, property.PropertyType, customAttribute.DefaultValue, (customAttribute2 != null) ? ((TypeConverter)Activator.CreateInstance(Type.GetType(customAttribute2.ConverterTypeName))) : null, (customAttribute3 != null) ? customAttribute3.ValidatorInstance : null, customAttribute.Options);
		}
	}
}
