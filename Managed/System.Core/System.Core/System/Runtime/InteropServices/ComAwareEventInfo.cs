using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Permits late-bound registration of an event handler.</summary>
	// Token: 0x020002EB RID: 747
	public class ComAwareEventInfo : EventInfo
	{
		/// <summary>Gets the attributes for this event.</summary>
		/// <returns>The read-only attributes for this event.</returns>
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x0000227E File Offset: 0x0000047E
		[global::System.MonoTODO]
		public override EventAttributes Attributes
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the class that declares this member.</summary>
		/// <returns>The <see cref="T:System.Type" /> object for the class that declares this member.</returns>
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x0000227E File Offset: 0x0000047E
		[global::System.MonoTODO]
		public override Type DeclaringType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the current member.</summary>
		/// <returns>The name of this member.</returns>
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x0000227E File Offset: 0x0000047E
		[global::System.MonoTODO]
		public override string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComAwareEventInfo" /> class by using the specified type and a name of the event on the type.</summary>
		/// <param name="type">The type of object. </param>
		/// <param name="eventName">The name of an event on <paramref name="type" />.</param>
		// Token: 0x060016D1 RID: 5841 RVA: 0x0004AF59 File Offset: 0x00049159
		[global::System.MonoTODO]
		public ComAwareEventInfo(Type type, string eventName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Attaches an event handler to a COM object.</summary>
		/// <param name="target">The target object that the event delegate should bind to.</param>
		/// <param name="handler">The event delegate.</param>
		// Token: 0x060016D2 RID: 5842 RVA: 0x0000227E File Offset: 0x0000047E
		[global::System.MonoTODO]
		public override void AddEventHandler(object target, Delegate handler)
		{
			throw new NotImplementedException();
		}

		/// <summary>Detaches an event handler from a COM object.</summary>
		/// <param name="target">The target object that the event delegate is bound to.</param>
		/// <param name="handler">The event delegate.</param>
		/// <exception cref="T:System.InvalidOperationException">The event does not have a public remove accessor.</exception>
		/// <exception cref="T:System.ArgumentException">The handler that was passed in cannot be used.</exception>
		/// <exception cref="T:System.Reflection.TargetException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.The <paramref name="target" /> parameter is null and the event is not static.-or- The <see cref="T:System.Reflection.EventInfo" /> is not declared on the target.</exception>
		/// <exception cref="T:System.MethodAccessException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have access permission to the member.</exception>
		// Token: 0x060016D3 RID: 5843 RVA: 0x0000227E File Offset: 0x0000047E
		[global::System.MonoTODO]
		public override void RemoveEventHandler(object target, Delegate handler)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the method that was used to add an event handler delegate to the event source.</summary>
		/// <returns>The method that was used to add an event handler delegate to the event source.</returns>
		/// <param name="nonPublic">true to return non-public methods; otherwise, false.</param>
		/// <exception cref="T:System.MethodAccessException">
		///   <paramref name="nonPublic" /> is true and the method used to add an event handler delegate is non-public, but the caller does not have permission to reflect on non-public methods.</exception>
		// Token: 0x060016D4 RID: 5844 RVA: 0x0000227E File Offset: 0x0000047E
		[global::System.MonoTODO]
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, returns the method that was called when the event was raised. </summary>
		/// <returns>The object that was called when the event was raised.</returns>
		/// <param name="nonPublic">true to return non-public methods; otherwise, false. </param>
		/// <exception cref="T:System.MethodAccessException">
		///   <paramref name="nonPublic" /> is true and the method used to add an event handler delegate is non-public, but the caller does not have permission to reflect on non-public methods. </exception>
		// Token: 0x060016D5 RID: 5845 RVA: 0x0000227E File Offset: 0x0000047E
		[global::System.MonoTODO]
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, retrieves the <see cref="T:System.Reflection.MethodInfo" /> object for removing a method of the event.</summary>
		/// <returns>The method that was used to remove an event handler delegate from the event source.</returns>
		/// <param name="nonPublic">true to return non-public methods; otherwise, false. </param>
		/// <exception cref="T:System.MethodAccessException">
		///   <paramref name="nonPublic" /> is true and the method used to add an event handler delegate is non-public, but the caller does not have permission to reflect on non-public methods. </exception>
		// Token: 0x060016D6 RID: 5846 RVA: 0x0000227E File Offset: 0x0000047E
		[global::System.MonoTODO]
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, gets an array that contains all the custom attributes of the specified type that are applied to this member.</summary>
		/// <returns>An array that contains all the custom attributes of the specified type, or an array that has no elements if no attributes were defined.</returns>
		/// <param name="attributeType">The attribute type to search for. Only attributes that are assignable to this type can be returned.</param>
		/// <param name="inherit">true to search this member's inheritance chain to find the attributes; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">This member belongs to a type that is loaded into the reflection-only context. See How to: Load Assemblies into the Reflection-Only Context</exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded.</exception>
		// Token: 0x060016D7 RID: 5847 RVA: 0x0000227E File Offset: 0x0000047E
		[global::System.MonoTODO]
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, gets an array that contains all the custom attributes that are applied to this member.</summary>
		/// <returns>An array that contains all the custom attributes, or an array that has no elements if no attributes were defined.</returns>
		/// <param name="inherit">true to search this member's inheritance chain to find the attributes; otherwise, false.</param>
		/// <exception cref="T:System.InvalidOperationException">This member belongs to a type that is loaded into the reflection-only context. See How to: Load Assemblies into the Reflection-Only Context.</exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded.</exception>
		// Token: 0x060016D8 RID: 5848 RVA: 0x0000227E File Offset: 0x0000047E
		[global::System.MonoTODO]
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether one or more instances of the specified attribute are applied to this member.</summary>
		/// <returns>true if the specified attribute has been applied to this member; otherwise, false.</returns>
		/// <param name="attributeType">The attribute type to search for.</param>
		/// <param name="inherit">true to search this member's inheritance chain to find the attributes; otherwise, false.</param>
		// Token: 0x060016D9 RID: 5849 RVA: 0x0000227E File Offset: 0x0000047E
		[global::System.MonoTODO]
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the class object that was used to initialize this instance.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that was used to initialize the current object.</returns>
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x0000227E File Offset: 0x0000047E
		[global::System.MonoTODO]
		public override Type ReflectedType
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
