using System;
using System.Collections.Generic;

namespace System.Reflection
{
	/// <summary>Provides methods that retrieve information about types at run time.</summary>
	// Token: 0x020002BF RID: 703
	public static class RuntimeReflectionExtensions
	{
		// Token: 0x0600200E RID: 8206 RVA: 0x0007DC6A File Offset: 0x0007BE6A
		private static void CheckAndThrow(Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!(t is RuntimeType))
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a runtime Type object."));
			}
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x0007DC98 File Offset: 0x0007BE98
		private static void CheckAndThrow(MethodInfo m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("method");
			}
			if (!(m is RuntimeMethodInfo))
			{
				throw new ArgumentException(Environment.GetResourceString("MethodInfo must be a runtime MethodInfo object."));
			}
		}

		/// <summary>Retrieves a collection that represents all the properties defined on a specified type.</summary>
		/// <returns>A collection of properties for the specified type.</returns>
		/// <param name="type">The type that contains the properties.</param>
		// Token: 0x06002010 RID: 8208 RVA: 0x0007DCC6 File Offset: 0x0007BEC6
		public static IEnumerable<PropertyInfo> GetRuntimeProperties(this Type type)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Retrieves a collection that represents all the events defined on a specified type.</summary>
		/// <returns>A collection of events for the specified type.</returns>
		/// <param name="type">The type that contains the events.</param>
		// Token: 0x06002011 RID: 8209 RVA: 0x0007DCD6 File Offset: 0x0007BED6
		public static IEnumerable<EventInfo> GetRuntimeEvents(this Type type)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Retrieves a collection that represents all methods defined on a specified type.</summary>
		/// <returns>A collection of methods for the specified type.</returns>
		/// <param name="type">The type that contains the methods.</param>
		// Token: 0x06002012 RID: 8210 RVA: 0x0007DCE6 File Offset: 0x0007BEE6
		public static IEnumerable<MethodInfo> GetRuntimeMethods(this Type type)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Retrieves a collection that represents all the fields defined on a specified type.</summary>
		/// <returns>A collection of fields for the specified type.</returns>
		/// <param name="type">The type that contains the fields.</param>
		// Token: 0x06002013 RID: 8211 RVA: 0x0007DCF6 File Offset: 0x0007BEF6
		public static IEnumerable<FieldInfo> GetRuntimeFields(this Type type)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Retrieves an object that represents a specified property.</summary>
		/// <returns>An object that represents the specified property, or null if the property is not found.</returns>
		/// <param name="type">The type that contains the property.</param>
		/// <param name="name">The name of the property.</param>
		// Token: 0x06002014 RID: 8212 RVA: 0x0007DD06 File Offset: 0x0007BF06
		public static PropertyInfo GetRuntimeProperty(this Type type, string name)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetProperty(name);
		}

		/// <summary>Retrieves an object that represents the specified event.</summary>
		/// <returns>An object that represents the specified event, or null if the event is not found.</returns>
		/// <param name="type">The type that contains the event.</param>
		/// <param name="name">The name of the event.</param>
		// Token: 0x06002015 RID: 8213 RVA: 0x0007DD15 File Offset: 0x0007BF15
		public static EventInfo GetRuntimeEvent(this Type type, string name)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetEvent(name);
		}

		/// <summary>Retrieves an object that represents a specified method.</summary>
		/// <returns>An object that represents the specified method, or null if the method is not found.</returns>
		/// <param name="type">The type that contains the method.</param>
		/// <param name="name">The name of the method.</param>
		/// <param name="parameters">An array that contains the method's parameters.</param>
		// Token: 0x06002016 RID: 8214 RVA: 0x0007DD24 File Offset: 0x0007BF24
		public static MethodInfo GetRuntimeMethod(this Type type, string name, Type[] parameters)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetMethod(name, parameters);
		}

		/// <summary>Retrieves an object that represents a specified field.</summary>
		/// <returns>An object that represents the specified field, or null if the field is not found.</returns>
		/// <param name="type">The type that contains the field.</param>
		/// <param name="name">The name of the field.</param>
		// Token: 0x06002017 RID: 8215 RVA: 0x0007DD34 File Offset: 0x0007BF34
		public static FieldInfo GetRuntimeField(this Type type, string name)
		{
			RuntimeReflectionExtensions.CheckAndThrow(type);
			return type.GetField(name);
		}

		/// <summary>Retrieves an object that represents the specified method on the direct or indirect base class where the method was first declared.</summary>
		/// <returns>An object that represents the specified method's initial declaration on a base class.</returns>
		/// <param name="method">The method to retrieve information about.</param>
		// Token: 0x06002018 RID: 8216 RVA: 0x0007DD43 File Offset: 0x0007BF43
		public static MethodInfo GetRuntimeBaseDefinition(this MethodInfo method)
		{
			RuntimeReflectionExtensions.CheckAndThrow(method);
			return method.GetBaseDefinition();
		}

		/// <summary>Returns an interface mapping for the specified type and the specified interface.</summary>
		/// <returns>An object that represents the interface mapping for the specified interface and type.</returns>
		/// <param name="typeInfo">The type to retrieve a mapping for.</param>
		/// <param name="interfaceType">The interface to retrieve a mapping for.</param>
		// Token: 0x06002019 RID: 8217 RVA: 0x0007DD51 File Offset: 0x0007BF51
		public static InterfaceMapping GetRuntimeInterfaceMap(this TypeInfo typeInfo, Type interfaceType)
		{
			if (typeInfo == null)
			{
				throw new ArgumentNullException("typeInfo");
			}
			if (!(typeInfo is RuntimeType))
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a runtime Type object."));
			}
			return typeInfo.GetInterfaceMap(interfaceType);
		}

		/// <summary>Gets an object that represents the method represented by the specified delegate.</summary>
		/// <returns>An object that represents the method.</returns>
		/// <param name="del">The delegate to examine.</param>
		// Token: 0x0600201A RID: 8218 RVA: 0x0007DD86 File Offset: 0x0007BF86
		public static MethodInfo GetMethodInfo(this Delegate del)
		{
			if (del == null)
			{
				throw new ArgumentNullException("del");
			}
			return del.Method;
		}

		// Token: 0x04001160 RID: 4448
		private const BindingFlags everything = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
	}
}
