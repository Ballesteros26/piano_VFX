using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Mono;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of an event and provides access to event metadata.</summary>
	// Token: 0x02000315 RID: 789
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_EventInfo))]
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class EventInfo : MemberInfo, _EventInfo
	{
		/// <summary>Gets the attributes for this event.</summary>
		/// <returns>The read-only attributes for this event.</returns>
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600227B RID: 8827
		public abstract EventAttributes Attributes { get; }

		/// <summary>Gets the Type object of the underlying event-handler delegate associated with this event.</summary>
		/// <returns>A read-only Type object representing the delegate event handler.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x0008179C File Offset: 0x0007F99C
		public virtual Type EventHandlerType
		{
			get
			{
				ParameterInfo[] parametersInternal = this.GetAddMethod(true).GetParametersInternal();
				if (parametersInternal.Length != 0)
				{
					return parametersInternal[0].ParameterType;
				}
				return null;
			}
		}

		/// <summary>Gets a value indicating whether the event is multicast.</summary>
		/// <returns>true if the delegate is an instance of a multicast delegate; otherwise, false.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x00003B29 File Offset: 0x00001D29
		public virtual bool IsMulticast
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the EventInfo has a name with a special meaning.</summary>
		/// <returns>true if this event has a special name; otherwise, false.</returns>
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x000817C4 File Offset: 0x0007F9C4
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & EventAttributes.SpecialName) > EventAttributes.None;
			}
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is an event.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is an event.</returns>
		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x0001EFC9 File Offset: 0x0001D1C9
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Event;
			}
		}

		/// <summary>Adds an event handler to an event source.</summary>
		/// <param name="target">The event source. </param>
		/// <param name="handler">Encapsulates a method or methods to be invoked when the event is raised by the target. </param>
		/// <exception cref="T:System.InvalidOperationException">The event does not have a public add accessor.</exception>
		/// <exception cref="T:System.ArgumentException">The handler that was passed in cannot be used. </exception>
		/// <exception cref="T:System.MethodAccessException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have access permission to the member. </exception>
		/// <exception cref="T:System.Reflection.TargetException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.The <paramref name="target" /> parameter is null and the event is not static.-or- The <see cref="T:System.Reflection.EventInfo" /> is not declared on the target. </exception>
		// Token: 0x06002281 RID: 8833 RVA: 0x000817D8 File Offset: 0x0007F9D8
		[DebuggerStepThrough]
		[DebuggerHidden]
		public virtual void AddEventHandler(object target, Delegate handler)
		{
			if (this.cached_add_event == null)
			{
				MethodInfo addMethod = this.GetAddMethod();
				if (addMethod == null)
				{
					throw new InvalidOperationException("Cannot add a handler to an event that doesn't have a visible add method");
				}
				if (addMethod.DeclaringType.IsValueType)
				{
					if (target == null && !addMethod.IsStatic)
					{
						throw new TargetException("Cannot add a handler to a non static event with a null target");
					}
					addMethod.Invoke(target, new object[] { handler });
					return;
				}
				else
				{
					this.cached_add_event = EventInfo.CreateAddEventDelegate(addMethod);
				}
			}
			this.cached_add_event(target, handler);
		}

		/// <summary>Returns the method used to add an event handler delegate to the event source.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to add an event handler delegate to the event source.</returns>
		// Token: 0x06002282 RID: 8834 RVA: 0x00081857 File Offset: 0x0007FA57
		public MethodInfo GetAddMethod()
		{
			return this.GetAddMethod(false);
		}

		/// <summary>When overridden in a derived class, retrieves the MethodInfo object for the <see cref="M:System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)" /> method of the event, specifying whether to return non-public methods.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to add an event handler delegate to the event source.</returns>
		/// <param name="nonPublic">true if non-public methods can be returned; otherwise, false. </param>
		/// <exception cref="T:System.MethodAccessException">
		///   <paramref name="nonPublic" /> is true, the method used to add an event handler delegate is non-public, and the caller does not have permission to reflect on non-public methods. </exception>
		// Token: 0x06002283 RID: 8835
		public abstract MethodInfo GetAddMethod(bool nonPublic);

		/// <summary>Returns the method that is called when the event is raised.</summary>
		/// <returns>The method that is called when the event is raised.</returns>
		// Token: 0x06002284 RID: 8836 RVA: 0x00081860 File Offset: 0x0007FA60
		public MethodInfo GetRaiseMethod()
		{
			return this.GetRaiseMethod(false);
		}

		/// <summary>When overridden in a derived class, returns the method that is called when the event is raised, specifying whether to return non-public methods.</summary>
		/// <returns>A MethodInfo object that was called when the event was raised.</returns>
		/// <param name="nonPublic">true if non-public methods can be returned; otherwise, false. </param>
		/// <exception cref="T:System.MethodAccessException">
		///   <paramref name="nonPublic" /> is true, the method used to add an event handler delegate is non-public, and the caller does not have permission to reflect on non-public methods. </exception>
		// Token: 0x06002285 RID: 8837
		public abstract MethodInfo GetRaiseMethod(bool nonPublic);

		/// <summary>Returns the method used to remove an event handler delegate from the event source.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to remove an event handler delegate from the event source.</returns>
		// Token: 0x06002286 RID: 8838 RVA: 0x00081869 File Offset: 0x0007FA69
		public MethodInfo GetRemoveMethod()
		{
			return this.GetRemoveMethod(false);
		}

		/// <summary>When overridden in a derived class, retrieves the MethodInfo object for removing a method of the event, specifying whether to return non-public methods.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method used to remove an event handler delegate from the event source.</returns>
		/// <param name="nonPublic">true if non-public methods can be returned; otherwise, false. </param>
		/// <exception cref="T:System.MethodAccessException">
		///   <paramref name="nonPublic" /> is true, the method used to add an event handler delegate is non-public, and the caller does not have permission to reflect on non-public methods. </exception>
		// Token: 0x06002287 RID: 8839
		public abstract MethodInfo GetRemoveMethod(bool nonPublic);

		/// <summary>Returns the methods that have been associated with the event in metadata using the .other directive, specifying whether to include non-public methods.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.EventInfo" /> objects representing methods that have been associated with an event in metadata by using the .other directive. If there are no methods matching the specification, an empty array is returned.</returns>
		/// <param name="nonPublic">true to include non-public methods; otherwise, false.</param>
		/// <exception cref="T:System.NotImplementedException">This method is not implemented.</exception>
		// Token: 0x06002288 RID: 8840 RVA: 0x00081872 File Offset: 0x0007FA72
		public virtual MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			return EmptyArray<MethodInfo>.Value;
		}

		/// <summary>Returns the public methods that have been associated with an event in metadata using the .other directive.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.EventInfo" /> objects representing the public methods that have been associated with the event in metadata by using the .other directive. If there are no such public methods, an empty array is returned.</returns>
		// Token: 0x06002289 RID: 8841 RVA: 0x00081879 File Offset: 0x0007FA79
		public MethodInfo[] GetOtherMethods()
		{
			return this.GetOtherMethods(false);
		}

		/// <summary>Removes an event handler from an event source.</summary>
		/// <param name="target">The event source. </param>
		/// <param name="handler">The delegate to be disassociated from the events raised by target. </param>
		/// <exception cref="T:System.InvalidOperationException">The event does not have a public remove accessor. </exception>
		/// <exception cref="T:System.ArgumentException">The handler that was passed in cannot be used. </exception>
		/// <exception cref="T:System.Reflection.TargetException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.The <paramref name="target" /> parameter is null and the event is not static.-or- The <see cref="T:System.Reflection.EventInfo" /> is not declared on the target. </exception>
		/// <exception cref="T:System.MethodAccessException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have access permission to the member. </exception>
		// Token: 0x0600228A RID: 8842 RVA: 0x00081882 File Offset: 0x0007FA82
		[DebuggerStepThrough]
		[DebuggerHidden]
		public virtual void RemoveEventHandler(object target, Delegate handler)
		{
			MethodInfo removeMethod = this.GetRemoveMethod();
			if (removeMethod == null)
			{
				throw new InvalidOperationException("Cannot remove a handler to an event that doesn't have a visible remove method");
			}
			removeMethod.Invoke(target, new object[] { handler });
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x0600228B RID: 8843 RVA: 0x0004BD58 File Offset: 0x00049F58
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600228C RID: 8844 RVA: 0x0007E911 File Offset: 0x0007CB11
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.EventInfo" /> objects are equal.</summary>
		/// <returns>true if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x0600228D RID: 8845 RVA: 0x00080329 File Offset: 0x0007E529
		public static bool operator ==(EventInfo left, EventInfo right)
		{
			return left == right || (!((left == null) ^ (right == null)) && left.Equals(right));
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.EventInfo" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x0600228E RID: 8846 RVA: 0x00080345 File Offset: 0x0007E545
		public static bool operator !=(EventInfo left, EventInfo right)
		{
			return left != right && (((left == null) ^ (right == null)) || !left.Equals(right));
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600228F RID: 8847 RVA: 0x0002126B File Offset: 0x0001F46B
		void _EventInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a T:System.Type object representing the <see cref="T:System.Reflection.EventInfo" /> type.</summary>
		/// <returns>A T:System.Type object representing the <see cref="T:System.Reflection.EventInfo" /> type.</returns>
		// Token: 0x06002290 RID: 8848 RVA: 0x00033A19 File Offset: 0x00031C19
		Type _EventInfo.GetType()
		{
			return base.GetType();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002291 RID: 8849 RVA: 0x0002126B File Offset: 0x0001F46B
		void _EventInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002292 RID: 8850 RVA: 0x0002126B File Offset: 0x0001F46B
		void _EventInfo.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x06002293 RID: 8851 RVA: 0x0002126B File Offset: 0x0001F46B
		void _EventInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x000818AF File Offset: 0x0007FAAF
		private static void AddEventFrame<T, D>(EventInfo.AddEvent<T, D> addEvent, object obj, object dele)
		{
			if (obj == null)
			{
				throw new TargetException("Cannot add a handler to a non static event with a null target");
			}
			if (!(obj is T))
			{
				throw new TargetException("Object doesn't match target");
			}
			addEvent((T)((object)obj), (D)((object)dele));
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x000818E4 File Offset: 0x0007FAE4
		private static void StaticAddEventAdapterFrame<D>(EventInfo.StaticAddEvent<D> addEvent, object obj, object dele)
		{
			addEvent((D)((object)dele));
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x000818F4 File Offset: 0x0007FAF4
		private static EventInfo.AddEventAdapter CreateAddEventDelegate(MethodInfo method)
		{
			Type[] array;
			Type type;
			string text;
			if (method.IsStatic)
			{
				array = new Type[] { method.GetParametersInternal()[0].ParameterType };
				type = typeof(EventInfo.StaticAddEvent<>);
				text = "StaticAddEventAdapterFrame";
			}
			else
			{
				array = new Type[]
				{
					method.DeclaringType,
					method.GetParametersInternal()[0].ParameterType
				};
				type = typeof(EventInfo.AddEvent<, >);
				text = "AddEventFrame";
			}
			object obj = Delegate.CreateDelegate(type.MakeGenericType(array), method);
			MethodInfo methodInfo = typeof(EventInfo).GetMethod(text, BindingFlags.Static | BindingFlags.NonPublic);
			methodInfo = methodInfo.MakeGenericMethod(array);
			return (EventInfo.AddEventAdapter)Delegate.CreateDelegate(typeof(EventInfo.AddEventAdapter), obj, methodInfo, true);
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodInfo" /> object for the <see cref="M:System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)" /> method of the event, including non-public methods.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodInfo" /> object for the <see cref="M:System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)" /> method.</returns>
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06002297 RID: 8855 RVA: 0x000819A7 File Offset: 0x0007FBA7
		public virtual MethodInfo AddMethod
		{
			get
			{
				return this.GetAddMethod(true);
			}
		}

		/// <summary>Gets the method that is called when the event is raised, including non-public methods.</summary>
		/// <returns>The method that is called when the event is raised.</returns>
		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06002298 RID: 8856 RVA: 0x000819B0 File Offset: 0x0007FBB0
		public virtual MethodInfo RaiseMethod
		{
			get
			{
				return this.GetRaiseMethod(true);
			}
		}

		/// <summary>Gets the MethodInfo object for removing a method of the event, including non-public methods.</summary>
		/// <returns>The MethodInfo object for removing a method of the event.</returns>
		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06002299 RID: 8857 RVA: 0x000819B9 File Offset: 0x0007FBB9
		public virtual MethodInfo RemoveMethod
		{
			get
			{
				return this.GetRemoveMethod(true);
			}
		}

		// Token: 0x0600229A RID: 8858
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EventInfo internal_from_handle_type(IntPtr event_handle, IntPtr type_handle);

		// Token: 0x0600229B RID: 8859 RVA: 0x000819C2 File Offset: 0x0007FBC2
		internal static EventInfo GetEventFromHandle(RuntimeEventHandle handle)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			return EventInfo.internal_from_handle_type(handle.Value, IntPtr.Zero);
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x000819F4 File Offset: 0x0007FBF4
		internal static EventInfo GetEventFromHandle(RuntimeEventHandle handle, RuntimeTypeHandle reflectedType)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			EventInfo eventInfo = EventInfo.internal_from_handle_type(handle.Value, reflectedType.Value);
			if (eventInfo == null)
			{
				throw new ArgumentException("The event handle and the type handle are incompatible.");
			}
			return eventInfo;
		}

		// Token: 0x04001314 RID: 4884
		private EventInfo.AddEventAdapter cached_add_event;

		// Token: 0x02000316 RID: 790
		// (Invoke) Token: 0x0600229E RID: 8862
		private delegate void AddEventAdapter(object _this, Delegate dele);

		// Token: 0x02000317 RID: 791
		// (Invoke) Token: 0x060022A2 RID: 8866
		private delegate void AddEvent<T, D>(T _this, D dele);

		// Token: 0x02000318 RID: 792
		// (Invoke) Token: 0x060022A6 RID: 8870
		private delegate void StaticAddEvent<D>(D dele);
	}
}
