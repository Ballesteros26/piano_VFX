using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Represents type declarations for class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types. </summary>
	// Token: 0x02000306 RID: 774
	[ComVisible(true)]
	[Serializable]
	public abstract class TypeInfo : Type, IReflectableType
	{
		// Token: 0x06002152 RID: 8530 RVA: 0x0007F409 File Offset: 0x0007D609
		[FriendAccessAllowed]
		internal TypeInfo()
		{
		}

		/// <summary>Returns a representation of the current type as a <see cref="T:System.Reflection.TypeInfo" /> object.</summary>
		/// <returns>A reference to the current type.</returns>
		// Token: 0x06002153 RID: 8531 RVA: 0x00002119 File Offset: 0x00000319
		TypeInfo IReflectableType.GetTypeInfo()
		{
			return this;
		}

		/// <summary>Returns the current type as a <see cref="T:System.Type" /> object.</summary>
		/// <returns>The current type.</returns>
		// Token: 0x06002154 RID: 8532 RVA: 0x00002119 File Offset: 0x00000319
		public virtual Type AsType()
		{
			return this;
		}

		/// <summary>Gets an array of the generic parameters of the current type.</summary>
		/// <returns>An array that contains the current type's generic parameters.</returns>
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06002155 RID: 8533 RVA: 0x0007F411 File Offset: 0x0007D611
		public virtual Type[] GenericTypeParameters
		{
			get
			{
				if (this.IsGenericTypeDefinition)
				{
					return this.GetGenericArguments();
				}
				return Type.EmptyTypes;
			}
		}

		/// <summary>Returns a value that indicates whether the specified type can be assigned to the current type.</summary>
		/// <returns>true if the specified type can be assigned to this type; otherwise, false.</returns>
		/// <param name="typeInfo">The type to check.</param>
		// Token: 0x06002156 RID: 8534 RVA: 0x0007F428 File Offset: 0x0007D628
		public virtual bool IsAssignableFrom(TypeInfo typeInfo)
		{
			if (typeInfo == null)
			{
				return false;
			}
			if (this == typeInfo)
			{
				return true;
			}
			if (typeInfo.IsSubclassOf(this))
			{
				return true;
			}
			if (base.IsInterface)
			{
				return typeInfo.ImplementInterface(this);
			}
			if (this.IsGenericParameter)
			{
				Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
				for (int i = 0; i < genericParameterConstraints.Length; i++)
				{
					if (!genericParameterConstraints[i].IsAssignableFrom(typeInfo))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Returns an object that represents the specified public event declared by the current type.</summary>
		/// <returns>An object that represents the specified event, if found; otherwise, null.</returns>
		/// <param name="name">The name of the event.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x06002157 RID: 8535 RVA: 0x0007F493 File Offset: 0x0007D693
		public virtual EventInfo GetDeclaredEvent(string name)
		{
			return this.GetEvent(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Returns an object that represents the specified public field declared by the current type.</summary>
		/// <returns>An object that represents the specified field, if found; otherwise, null.</returns>
		/// <param name="name">The name of the field.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x06002158 RID: 8536 RVA: 0x0007F49E File Offset: 0x0007D69E
		public virtual FieldInfo GetDeclaredField(string name)
		{
			return this.GetField(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Returns an object that represents the specified public method declared by the current type.</summary>
		/// <returns>An object that represents the specified method, if found; otherwise, null.</returns>
		/// <param name="name">The name of the method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x06002159 RID: 8537 RVA: 0x0007F4A9 File Offset: 0x0007D6A9
		public virtual MethodInfo GetDeclaredMethod(string name)
		{
			return base.GetMethod(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Returns a collection that contains all public methods declared on the current type that match the specified name.</summary>
		/// <returns>A collection that contains methods that match <paramref name="name" />.</returns>
		/// <param name="name">The method name to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x0600215A RID: 8538 RVA: 0x0007F4B4 File Offset: 0x0007D6B4
		public virtual IEnumerable<MethodInfo> GetDeclaredMethods(string name)
		{
			foreach (MethodInfo methodInfo in this.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (methodInfo.Name == name)
				{
					yield return methodInfo;
				}
			}
			MethodInfo[] array = null;
			yield break;
		}

		/// <summary>Returns an object that represents the specified public nested type declared by the current type.</summary>
		/// <returns>An object that represents the specified nested type, if found; otherwise, null.</returns>
		/// <param name="name">The name of the nested type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x0600215B RID: 8539 RVA: 0x0007F4CC File Offset: 0x0007D6CC
		public virtual TypeInfo GetDeclaredNestedType(string name)
		{
			Type nestedType = this.GetNestedType(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (nestedType == null)
			{
				return null;
			}
			return nestedType.GetTypeInfo();
		}

		/// <summary>Returns an object that represents the specified public property declared by the current type.</summary>
		/// <returns>An object that represents the specified property, if found; otherwise, null.</returns>
		/// <param name="name">The name of the property.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x0600215C RID: 8540 RVA: 0x0007F4F4 File Offset: 0x0007D6F4
		public virtual PropertyInfo GetDeclaredProperty(string name)
		{
			return base.GetProperty(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Gets a collection of the constructors declared by the current type.</summary>
		/// <returns>A collection of the constructors declared by the current type.</returns>
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600215D RID: 8541 RVA: 0x0007F4FF File Offset: 0x0007D6FF
		public virtual IEnumerable<ConstructorInfo> DeclaredConstructors
		{
			get
			{
				return this.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the events defined by the current type.</summary>
		/// <returns>A collection of the events defined by the current type.</returns>
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600215E RID: 8542 RVA: 0x0007F509 File Offset: 0x0007D709
		public virtual IEnumerable<EventInfo> DeclaredEvents
		{
			get
			{
				return this.GetEvents(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the fields defined by the current type.</summary>
		/// <returns>A collection of the fields defined by the current type.</returns>
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600215F RID: 8543 RVA: 0x0007F513 File Offset: 0x0007D713
		public virtual IEnumerable<FieldInfo> DeclaredFields
		{
			get
			{
				return this.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the members defined by the current type.</summary>
		/// <returns>A collection of the members defined by the current type.</returns>
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06002160 RID: 8544 RVA: 0x0007F51D File Offset: 0x0007D71D
		public virtual IEnumerable<MemberInfo> DeclaredMembers
		{
			get
			{
				return this.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the methods defined by the current type.</summary>
		/// <returns>A collection of the methods defined by the current type.</returns>
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06002161 RID: 8545 RVA: 0x0007F527 File Offset: 0x0007D727
		public virtual IEnumerable<MethodInfo> DeclaredMethods
		{
			get
			{
				return this.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the nested types defined by the current type.</summary>
		/// <returns>A collection of nested types defined by the current type.</returns>
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06002162 RID: 8546 RVA: 0x0007F531 File Offset: 0x0007D731
		public virtual IEnumerable<TypeInfo> DeclaredNestedTypes
		{
			get
			{
				foreach (Type type in this.GetNestedTypes(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					yield return type.GetTypeInfo();
				}
				Type[] array = null;
				yield break;
			}
		}

		/// <summary>Gets a collection of the properties defined by the current type. </summary>
		/// <returns>A collection of the properties defined by the current type.</returns>
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06002163 RID: 8547 RVA: 0x0007F541 File Offset: 0x0007D741
		public virtual IEnumerable<PropertyInfo> DeclaredProperties
		{
			get
			{
				return this.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the interfaces implemented by the current type.</summary>
		/// <returns>A collection of the interfaces implemented by the current type.</returns>
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06002164 RID: 8548 RVA: 0x0007F54B File Offset: 0x0007D74B
		public virtual IEnumerable<Type> ImplementedInterfaces
		{
			get
			{
				return this.GetInterfaces();
			}
		}
	}
}
