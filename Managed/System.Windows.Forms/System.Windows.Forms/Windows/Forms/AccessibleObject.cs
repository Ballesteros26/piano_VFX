using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using Accessibility;

namespace System.Windows.Forms
{
	/// <summary>Provides information that accessibility applications use to adjust an application's user interface (UI) for users with impairments.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000038 RID: 56
	[ComVisible(true)]
	public class AccessibleObject : StandardOleMarshalObject, IReflect, IAccessible
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AccessibleObject" /> class.</summary>
		// Token: 0x0600019D RID: 413 RVA: 0x0000EE2C File Offset: 0x0000D02C
		public AccessibleObject()
		{
			this.owner = null;
			this.value = null;
			this.name = null;
			this.role = AccessibleRole.Default;
			this.default_action = null;
			this.description = null;
			this.help = null;
			this.keyboard_shortcut = null;
			this.state = AccessibleStates.None;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000EE80 File Offset: 0x0000D080
		internal AccessibleObject(Control owner)
			: this()
		{
			this.owner = owner;
		}

		/// <summary>Gets the <see cref="T:System.Reflection.FieldInfo" /> object corresponding to the specified field and binding flag. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetField(System.String,System.Reflection.BindingFlags)" />.</summary>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object containing the field information for the named object that meets the search constraints specified in <paramref name="bindingAttr" />.</returns>
		/// <param name="name">The name of the field to find.</param>
		/// <param name="bindingAttr">The binding attributes used to control the search.</param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">The object implements multiple fields with the same name.</exception>
		// Token: 0x0600019F RID: 415 RVA: 0x0000EE90 File Offset: 0x0000D090
		FieldInfo IReflect.GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an array of <see cref="T:System.Reflection.FieldInfo" /> objects corresponding to all fields of the current class. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetFields(System.Reflection.BindingFlags)" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.FieldInfo" /> objects containing all the field information for this reflection object that meets the search constraints specified in <paramref name="bindingAttr" />.</returns>
		/// <param name="bindingAttr">The binding attributes used to control the search.</param>
		// Token: 0x060001A0 RID: 416 RVA: 0x0000EE98 File Offset: 0x0000D098
		FieldInfo[] IReflect.GetFields(BindingFlags bindingAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an array of <see cref="T:System.Reflection.MemberInfo" /> objects corresponding to all public members or to all members that match a specified name. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetMember(System.String,System.Reflection.BindingFlags)" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects matching the name parameter.</returns>
		/// <param name="name">The name of the member to find.</param>
		/// <param name="bindingAttr">The binding attributes used to control the search.</param>
		// Token: 0x060001A1 RID: 417 RVA: 0x0000EEA0 File Offset: 0x0000D0A0
		MemberInfo[] IReflect.GetMember(string name, BindingFlags bindingAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an array of <see cref="T:System.Reflection.MemberInfo" /> objects corresponding either to all public members or to all members of the current class. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetMembers(System.Reflection.BindingFlags)" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects containing all the member information for this reflection object.</returns>
		/// <param name="bindingAttr">The binding attributes used to control the search.</param>
		// Token: 0x060001A2 RID: 418 RVA: 0x0000EEA8 File Offset: 0x0000D0A8
		MemberInfo[] IReflect.GetMembers(BindingFlags bindingAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MethodInfo" /> object corresponding to a specified method under specified search constraints. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetMethod(System.String,System.Reflection.BindingFlags)" />.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object containing the method information, with the match being based on the method name and search constraints specified in <paramref name="bindingAttr" />.</returns>
		/// <param name="name">The name of the member to find.</param>
		/// <param name="bindingAttr">The binding attributes used to control the search.</param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">The object implements multiple methods with the same name.</exception>
		// Token: 0x060001A3 RID: 419 RVA: 0x0000EEB0 File Offset: 0x0000D0B0
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MethodInfo" /> object corresponding to a specified method, using a Type array to choose from among overloaded methods. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Type[],System.Reflection.ParameterModifier[])" />.</summary>
		/// <returns>The requested method that matches all the specified parameters.</returns>
		/// <param name="name">The name of the member to find.</param>
		/// <param name="bindingAttr">The binding attributes used to control the search.</param>
		/// <param name="binder">An object that implements <see cref="T:System.Reflection.Binder" />, containing properties related to this method.</param>
		/// <param name="types">An array used to choose among overloaded methods.</param>
		/// <param name="modifiers">An array of parameter modifiers used to make binding work with parameter signatures in which the types have been modified.</param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">The object implements multiple methods with the same name.</exception>
		// Token: 0x060001A4 RID: 420 RVA: 0x0000EEB8 File Offset: 0x0000D0B8
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an array of <see cref="T:System.Reflection.MethodInfo" /> objects with all public methods or all methods of the current class. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetMethods(System.Reflection.BindingFlags)" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects containing all the methods defined for this reflection object that meet the search constraints specified in bindingAttr.</returns>
		/// <param name="bindingAttr">The binding attributes used to control the search. </param>
		// Token: 0x060001A5 RID: 421 RVA: 0x0000EEC0 File Offset: 0x0000D0C0
		MethodInfo[] IReflect.GetMethods(BindingFlags bindingAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a <see cref="T:System.Reflection.PropertyInfo" /> object corresponding to a specified property under specified search constraints. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetProperty(System.String,System.Reflection.BindingFlags)" />.</summary>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object for the located property that meets the search constraints specified in <paramref name="bindingAttr" />, or null if the property was not located.</returns>
		/// <param name="name">The name of the property to find.</param>
		/// <param name="bindingAttr">The binding attributes used to control the search.</param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">The object implements multiple methods with the same name.</exception>
		// Token: 0x060001A6 RID: 422 RVA: 0x0000EEC8 File Offset: 0x0000D0C8
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a <see cref="T:System.Reflection.PropertyInfo" /> object corresponding to a specified property with specified search constraints. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetProperty(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Type,System.Type[],System.Reflection.ParameterModifier[])" />.</summary>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object for the located property, if a property with the specified name was located in this reflection object, or null if the property was not located.</returns>
		/// <param name="name">The name of the member to find.</param>
		/// <param name="bindingAttr">The binding attributes used to control the search.</param>
		/// <param name="binder">An object that implements Binder, containing properties related to this method.</param>
		/// <param name="returnType">An array used to choose among overloaded methods.</param>
		/// <param name="types">An array of parameter modifiers used to make binding work with parameter signatures in which the types have been modified.</param>
		/// <param name="modifiers">An array used to choose the parameter modifiers.</param>
		// Token: 0x060001A7 RID: 423 RVA: 0x0000EED0 File Offset: 0x0000D0D0
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an array of <see cref="T:System.Reflection.PropertyInfo" /> objects corresponding to all public properties or to all properties of the current class. For a description of this member, see <see cref="M:System.Reflection.IReflect.GetProperties(System.Reflection.BindingFlags)" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.PropertyInfo" /> objects for all the properties defined on the reflection object.</returns>
		/// <param name="bindingAttr">The binding attribute used to control the search.</param>
		// Token: 0x060001A8 RID: 424 RVA: 0x0000EED8 File Offset: 0x0000D0D8
		PropertyInfo[] IReflect.GetProperties(BindingFlags bindingAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Invokes a specified member. For a description of this member, see <see cref="M:System.Reflection.IReflect.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" />.</summary>
		/// <returns>The specified member.</returns>
		/// <param name="name">The name of the member to find.</param>
		/// <param name="invokeAttr">One of the <see cref="T:System.Reflection.BindingFlags" /> invocation attributes. </param>
		/// <param name="binder">One of the <see cref="T:System.Reflection.BindingFlags" /> bit flags. Implements Binder, containing properties related to this method.</param>
		/// <param name="target">The object on which to invoke the specified member. This parameter is ignored for static members.</param>
		/// <param name="args">An array of objects that contains the number, order, and type of the parameters of the member to be invoked. This is an empty array if there are no parameters.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects. </param>
		/// <param name="culture">An instance of <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. </param>
		/// <param name="namedParameters">A String array of parameters.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="invokeAttr" /> is <see cref="F:System.Reflection.BindingFlags.CreateInstance" /> and another bit flag is also set.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="invokeAttr" /> is not <see cref="F:System.Reflection.BindingFlags.CreateInstance" /> and name is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="invokeAttr" /> is not an invocation attribute from <see cref="T:System.Reflection.BindingFlags" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="invokeAttr" /> specifies both get and set for a property or field.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="invokeAttr" /> specifies both a field set and an Invoke method. <paramref name="args" /> is provided for a field get operation.</exception>
		/// <exception cref="T:System.ArgumentException">More than one argument is specified for a field set operation.</exception>
		/// <exception cref="T:System.MissingFieldException">The field or property cannot be found.</exception>
		/// <exception cref="T:System.MissingMethodException">The method cannot be found.</exception>
		/// <exception cref="T:System.Security.SecurityException">A private member is invoked without the necessary <see cref="T:System.Security.Permissions.ReflectionPermission" />.</exception>
		// Token: 0x060001A9 RID: 425 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
		object IReflect.InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the underlying type that represents the <see cref="T:System.Reflection.IReflect" /> object. For a description of this member, see <see cref="P:System.Reflection.IReflect.UnderlyingSystemType" />.</summary>
		/// <returns>The underlying type that represents the <see cref="T:System.Reflection.IReflect" /> object.</returns>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		Type IReflect.UnderlyingSystemType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Performs the specified object's default action. Not all objects have a default action. For a description of this member, see <see cref="M:Accessibility.IAccessible.accDoDefaultAction(System.Object)" />.</summary>
		/// <param name="childID">The child ID in the <see cref="T:Accessibility.IAccessible" /> interface/child ID pair that represents the accessible object.</param>
		// Token: 0x060001AB RID: 427 RVA: 0x0000EEF0 File Offset: 0x0000D0F0
		void IAccessible.accDoDefaultAction(object childID)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the number of child interfaces that belong to this object. For a description of this member, see <see cref="P:Accessibility.IAccessible.accChildCount" />.</summary>
		/// <returns>The number of child accessible objects that belong to this object. If the object has no child objects, this value is 0.</returns>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
		int IAccessible.accChildCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the object that has the keyboard focus. For a description of this member, see <see cref="P:Accessibility.IAccessible.accFocus" />.</summary>
		/// <returns>The object that has keyboard focus. </returns>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000EF00 File Offset: 0x0000D100
		object IAccessible.accFocus
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the child object at the specified screen coordinates. For a description of this member, see <see cref="M:Accessibility.IAccessible.accHitTest(System.Int32,System.Int32)" />.</summary>
		/// <returns>The accessible object at the point specified by <paramref name="xLeft" /> and <paramref name="yTop" />. </returns>
		/// <param name="xLeft">The horizontal coordinate.</param>
		/// <param name="yTop">The vertical coordinate.</param>
		// Token: 0x060001AE RID: 430 RVA: 0x0000EF08 File Offset: 0x0000D108
		object IAccessible.accHitTest(int xLeft, int yTop)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the object's current screen location. For a description of this member, see <see cref="M:Accessibility.IAccessible.accLocation(System.Int32@,System.Int32@,System.Int32@,System.Int32@,System.Object)" />.</summary>
		/// <param name="pxLeft">When this method returns, contains the x-coordinate of the object’s left edge. This parameter is passed uninitialized.</param>
		/// <param name="pyTop">When this method returns, contains the y-coordinate of the object’s top edge. This parameter is passed uninitialized.</param>
		/// <param name="pcxWidth">When this method returns, contains the width of the object. This parameter is passed uninitialized.</param>
		/// <param name="pcyHeight">When this method returns, contains the height of the object. This parameter is passed uninitialized.</param>
		/// <param name="childID">The ID number of the accessible object. This parameter is 0 to get the location of the object, or a child ID to get the location of one of the object's child objects.</param>
		// Token: 0x060001AF RID: 431 RVA: 0x0000EF10 File Offset: 0x0000D110
		void IAccessible.accLocation(out int pxLeft, out int pyTop, out int pcxWidth, out int pcyHeight, object childID)
		{
			throw new NotImplementedException();
		}

		/// <summary>Navigates to an accessible object relative to the current object. For a description of this member, see <see cref="M:Accessibility.IAccessible.accNavigate(System.Int32,System.Object)" />.</summary>
		/// <returns>The accessible object positioned at the value specified by <paramref name="navDir" />. </returns>
		/// <param name="navDir">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> enumerations that specifies the direction to navigate. </param>
		/// <param name="childID">The ID number of the accessible object. This parameter is 0 to start from the object, or a child ID to start from one of the object's child objects.</param>
		// Token: 0x060001B0 RID: 432 RVA: 0x0000EF18 File Offset: 0x0000D118
		object IAccessible.accNavigate(int navDir, object childID)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the parent accessible object of this object. For a description of this member, see <see cref="P:Accessibility.IAccessible.accParent" />.</summary>
		/// <returns>An <see cref="T:Accessibility.IAccessible" /> that represents the parent of the accessible object, or null if there is no parent object.</returns>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000EF20 File Offset: 0x0000D120
		object IAccessible.accParent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Modifies the selection or moves the keyboard focus of the accessible object. For a description of this member, see <see cref="M:Accessibility.IAccessible.accSelect(System.Int32,System.Object)" />.</summary>
		/// <param name="flagsSelect">A bitwise combination of the <see cref="T:System.Windows.Forms.AccessibleSelection" /> values.</param>
		/// <param name="childID">The ID number of the accessible object on which to perform the selection. This parameter is 0 to select the object, or a child ID to select one of the object's child objects.</param>
		// Token: 0x060001B2 RID: 434 RVA: 0x0000EF28 File Offset: 0x0000D128
		void IAccessible.accSelect(int flagsSelect, object childID)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the selected child objects of an accessible object. For a description of this member, see <see cref="P:Accessibility.IAccessible.accSelection" />.</summary>
		/// <returns>The selected child objects of an accessible object. </returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000EF30 File Offset: 0x0000D130
		object IAccessible.accSelection
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000EF38 File Offset: 0x0000D138
		object IAccessible.get_accChild(object childID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000EF40 File Offset: 0x0000D140
		string IAccessible.get_accDefaultAction(object childID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000EF48 File Offset: 0x0000D148
		string IAccessible.get_accDescription(object childID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000EF50 File Offset: 0x0000D150
		string IAccessible.get_accHelp(object childID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000EF58 File Offset: 0x0000D158
		int IAccessible.get_accHelpTopic(out string pszHelpFile, object childID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000EF60 File Offset: 0x0000D160
		string IAccessible.get_accKeyboardShortcut(object childID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000EF68 File Offset: 0x0000D168
		string IAccessible.get_accName(object childID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000EF70 File Offset: 0x0000D170
		object IAccessible.get_accRole(object childID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000EF78 File Offset: 0x0000D178
		object IAccessible.get_accState(object childID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000EF80 File Offset: 0x0000D180
		string IAccessible.get_accValue(object childID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000EF88 File Offset: 0x0000D188
		void IAccessible.set_accName(object childID, string newName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000EF90 File Offset: 0x0000D190
		void IAccessible.set_accValue(object childID, string newValue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the location and size of the accessible object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the accessible object.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The bounds of control cannot be retrieved. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000EF98 File Offset: 0x0000D198
		public virtual Rectangle Bounds
		{
			get
			{
				return this.owner.Bounds;
			}
		}

		/// <summary>Gets a string that describes the default action of the object. Not all objects have a default action.</summary>
		/// <returns>A description of the default action for an object, or null if this object has no default action.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The default action for the control cannot be retrieved. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000EFA8 File Offset: 0x0000D1A8
		public virtual string DefaultAction
		{
			get
			{
				return this.default_action;
			}
		}

		/// <summary>Gets a string that describes the visual appearance of the specified object. Not all objects have a description.</summary>
		/// <returns>A description of the object's visual appearance to the user, or null if the object does not have a description.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The description for the control cannot be retrieved. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000EFB0 File Offset: 0x0000D1B0
		public virtual string Description
		{
			get
			{
				return this.description;
			}
		}

		/// <summary>Gets a description of what the object does or how the object is used.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the description of what the object does or how the object is used. Returns null if no help is defined.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The help string for the control cannot be retrieved. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000EFB8 File Offset: 0x0000D1B8
		public virtual string Help
		{
			get
			{
				return this.help;
			}
		}

		/// <summary>Gets the shortcut key or access key for the accessible object.</summary>
		/// <returns>The shortcut key or access key for the accessible object, or null if there is no shortcut key associated with the object.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The shortcut for the control cannot be retrieved. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000EFC0 File Offset: 0x0000D1C0
		public virtual string KeyboardShortcut
		{
			get
			{
				return this.keyboard_shortcut;
			}
		}

		/// <summary>Gets or sets the object name.</summary>
		/// <returns>The object name, or null if the property has not been set.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The name of the control cannot be retrieved or set. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000EFC8 File Offset: 0x0000D1C8
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x0000EFD0 File Offset: 0x0000D1D0
		public virtual string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets the parent of an accessible object.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the parent of an accessible object, or null if there is no parent object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000EFDC File Offset: 0x0000D1DC
		public virtual AccessibleObject Parent
		{
			get
			{
				if (this.owner != null && this.owner.Parent != null)
				{
					return this.owner.Parent.AccessibilityObject;
				}
				return null;
			}
		}

		/// <summary>Gets the role of this accessible object.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values, or <see cref="F:System.Windows.Forms.AccessibleRole.None" /> if no role has been specified.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000F00C File Offset: 0x0000D20C
		public virtual AccessibleRole Role
		{
			get
			{
				return this.role;
			}
		}

		/// <summary>Gets the state of this accessible object.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values, or <see cref="F:System.Windows.Forms.AccessibleStates.None" />, if no state has been set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000F014 File Offset: 0x0000D214
		public virtual AccessibleStates State
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Gets or sets the value of an accessible object.</summary>
		/// <returns>The value of an accessible object, or null if the object has no value set.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The value cannot be set or retrieved. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000F01C File Offset: 0x0000D21C
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000F024 File Offset: 0x0000D224
		public virtual string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		/// <summary>Performs the default action associated with this accessible object.</summary>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The default action for the control cannot be performed. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060001CC RID: 460 RVA: 0x0000F030 File Offset: 0x0000D230
		public virtual void DoDefaultAction()
		{
			if (this.owner != null)
			{
				this.owner.DoDefaultAction();
			}
		}

		/// <summary>Retrieves the accessible child corresponding to the specified index.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the accessible child corresponding to the specified index.</returns>
		/// <param name="index">The zero-based index of the accessible child. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001CD RID: 461 RVA: 0x0000F048 File Offset: 0x0000D248
		public virtual AccessibleObject GetChild(int index)
		{
			if (this.owner != null && index < this.owner.Controls.Count)
			{
				return this.owner.Controls[index].AccessibilityObject;
			}
			return null;
		}

		/// <summary>Retrieves the number of children belonging to an accessible object.</summary>
		/// <returns>The number of children belonging to an accessible object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001CE RID: 462 RVA: 0x0000F090 File Offset: 0x0000D290
		public virtual int GetChildCount()
		{
			if (this.owner != null)
			{
				return this.owner.Controls.Count;
			}
			return -1;
		}

		/// <summary>Retrieves the object that has the keyboard focus.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that specifies the currently focused child. This method returns the calling object if the object itself is focused. Returns null if no object has focus.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The control cannot be retrieved. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001CF RID: 463 RVA: 0x0000F0B0 File Offset: 0x0000D2B0
		public virtual AccessibleObject GetFocused()
		{
			if (this.owner.has_focus)
			{
				return this.owner.AccessibilityObject;
			}
			return AccessibleObject.FindFocusControl(this.owner);
		}

		/// <summary>Gets an identifier for a Help topic identifier and the path to the Help file associated with this accessible object.</summary>
		/// <returns>An identifier for a Help topic, or -1 if there is no Help topic. On return, the <paramref name="fileName" /> parameter contains the path to the Help file associated with this accessible object.</returns>
		/// <param name="fileName">On return, this property contains the path to the Help file associated with this accessible object. </param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The Help topic for the control cannot be retrieved. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060001D0 RID: 464 RVA: 0x0000F0DC File Offset: 0x0000D2DC
		public virtual int GetHelpTopic(out string fileName)
		{
			fileName = null;
			return -1;
		}

		/// <summary>Retrieves the currently selected child.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the currently selected child. This method returns the calling object if the object itself is selected. Returns null if is no child is currently selected and the object itself does not have focus.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The selected child cannot be retrieved. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001D1 RID: 465 RVA: 0x0000F0E4 File Offset: 0x0000D2E4
		public virtual AccessibleObject GetSelected()
		{
			if ((this.state & AccessibleStates.Selected) != AccessibleStates.None)
			{
				return this;
			}
			return AccessibleObject.FindSelectedControl(this.owner);
		}

		/// <summary>Retrieves the child object at the specified screen coordinates.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the child object at the given screen coordinates. This method returns the calling object if the object itself is at the location specified. Returns null if no object is at the tested location.</returns>
		/// <param name="x">The horizontal screen coordinate. </param>
		/// <param name="y">The vertical screen coordinate. </param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The control cannot be hit tested. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060001D2 RID: 466 RVA: 0x0000F100 File Offset: 0x0000D300
		public virtual AccessibleObject HitTest(int x, int y)
		{
			Control control = AccessibleObject.FindHittestControl(this.owner, x, y);
			if (control != null)
			{
				return control.AccessibilityObject;
			}
			return null;
		}

		/// <summary>Navigates to another accessible object.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents one of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</returns>
		/// <param name="navdir">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values. </param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The navigation attempt fails. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060001D3 RID: 467 RVA: 0x0000F12C File Offset: 0x0000D32C
		public virtual AccessibleObject Navigate(AccessibleNavigation navdir)
		{
			int num;
			if (this.owner.Parent != null)
			{
				num = this.owner.Parent.Controls.IndexOf(this.owner);
			}
			else
			{
				num = -1;
			}
			switch (navdir)
			{
			case AccessibleNavigation.Up:
				if (this.owner.Parent != null)
				{
					for (int i = 0; i < this.owner.Parent.Controls.Count; i++)
					{
						if (this.owner != this.owner.Parent.Controls[i] && this.owner.Parent.Controls[i].Top < this.owner.Top)
						{
							return this.owner.Parent.Controls[i].AccessibilityObject;
						}
					}
				}
				return this.owner.AccessibilityObject;
			case AccessibleNavigation.Down:
				if (this.owner.Parent != null)
				{
					for (int j = 0; j < this.owner.Parent.Controls.Count; j++)
					{
						if (this.owner != this.owner.Parent.Controls[j] && this.owner.Parent.Controls[j].Top > this.owner.Bottom)
						{
							return this.owner.Parent.Controls[j].AccessibilityObject;
						}
					}
				}
				return this.owner.AccessibilityObject;
			case AccessibleNavigation.Left:
				if (this.owner.Parent != null)
				{
					for (int k = 0; k < this.owner.Parent.Controls.Count; k++)
					{
						if (this.owner != this.owner.Parent.Controls[k] && this.owner.Parent.Controls[k].Left < this.owner.Left)
						{
							return this.owner.Parent.Controls[k].AccessibilityObject;
						}
					}
				}
				return this.owner.AccessibilityObject;
			case AccessibleNavigation.Right:
				if (this.owner.Parent != null)
				{
					for (int l = 0; l < this.owner.Parent.Controls.Count; l++)
					{
						if (this.owner != this.owner.Parent.Controls[l] && this.owner.Parent.Controls[l].Left > this.owner.Right)
						{
							return this.owner.Parent.Controls[l].AccessibilityObject;
						}
					}
				}
				return this.owner.AccessibilityObject;
			case AccessibleNavigation.Next:
				if (this.owner.Parent == null)
				{
					return this.owner.AccessibilityObject;
				}
				if (num + 1 < this.owner.Parent.Controls.Count)
				{
					return this.owner.Parent.Controls[num + 1].AccessibilityObject;
				}
				return this.owner.Parent.Controls[0].AccessibilityObject;
			case AccessibleNavigation.Previous:
				if (this.owner.Parent == null)
				{
					return this.owner.AccessibilityObject;
				}
				if (num > 0)
				{
					return this.owner.Parent.Controls[num - 1].AccessibilityObject;
				}
				return this.owner.Parent.Controls[this.owner.Parent.Controls.Count - 1].AccessibilityObject;
			case AccessibleNavigation.FirstChild:
				if (this.owner.Controls.Count > 0)
				{
					return this.owner.Controls[0].AccessibilityObject;
				}
				return this.owner.AccessibilityObject;
			case AccessibleNavigation.LastChild:
				if (this.owner.Controls.Count > 0)
				{
					return this.owner.Controls[this.owner.Controls.Count - 1].AccessibilityObject;
				}
				return this.owner.AccessibilityObject;
			default:
				return this.owner.AccessibilityObject;
			}
		}

		/// <summary>Modifies the selection or moves the keyboard focus of the accessible object.</summary>
		/// <param name="flags">One of the <see cref="T:System.Windows.Forms.AccessibleSelection" /> values. </param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The selection cannot be performed. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060001D4 RID: 468 RVA: 0x0000F5C0 File Offset: 0x0000D7C0
		public virtual void Select(AccessibleSelection flags)
		{
			if ((flags & AccessibleSelection.TakeFocus) != AccessibleSelection.None)
			{
				this.owner.Focus();
			}
		}

		/// <summary>Associates an object with an instance of an <see cref="T:System.Windows.Forms.AccessibleObject" /> based on the handle of the object.</summary>
		/// <param name="handle">An <see cref="T:System.IntPtr" /> that contains the handle of the object. </param>
		// Token: 0x060001D5 RID: 469 RVA: 0x0000F5D8 File Offset: 0x0000D7D8
		protected void UseStdAccessibleObjects(IntPtr handle)
		{
		}

		/// <summary>Associates an object with an instance of an <see cref="T:System.Windows.Forms.AccessibleObject" /> based on the handle and the object id of the object.</summary>
		/// <param name="handle">An <see cref="T:System.IntPtr" /> that contains the handle of the object. </param>
		/// <param name="objid">An Int that defines the type of object that the <paramref name="handle" /> parameter refers to. </param>
		// Token: 0x060001D6 RID: 470 RVA: 0x0000F5DC File Offset: 0x0000D7DC
		protected void UseStdAccessibleObjects(IntPtr handle, int objid)
		{
			this.UseStdAccessibleObjects(handle, 0);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000F5E8 File Offset: 0x0000D7E8
		internal static AccessibleObject FindFocusControl(Control parent)
		{
			if (parent != null)
			{
				for (int i = 0; i < parent.Controls.Count; i++)
				{
					Control control = parent.Controls[i];
					if ((control.AccessibilityObject.state & AccessibleStates.Focused) != AccessibleStates.None)
					{
						return control.AccessibilityObject;
					}
					if (control.Controls.Count > 0)
					{
						AccessibleObject accessibleObject = AccessibleObject.FindFocusControl(control);
						if (accessibleObject != null)
						{
							return accessibleObject;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000F660 File Offset: 0x0000D860
		internal static AccessibleObject FindSelectedControl(Control parent)
		{
			if (parent != null)
			{
				for (int i = 0; i < parent.Controls.Count; i++)
				{
					Control control = parent.Controls[i];
					if ((control.AccessibilityObject.state & AccessibleStates.Selected) != AccessibleStates.None)
					{
						return control.AccessibilityObject;
					}
					if (control.Controls.Count > 0)
					{
						AccessibleObject accessibleObject = AccessibleObject.FindSelectedControl(control);
						if (accessibleObject != null)
						{
							return accessibleObject;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000F6D8 File Offset: 0x0000D8D8
		internal static Control FindHittestControl(Control parent, int x, int y)
		{
			Point point;
			point..ctor(x, y);
			Point point2 = parent.PointToClient(point);
			if (parent.ClientRectangle.Contains(point2))
			{
				return parent;
			}
			for (int i = 0; i < parent.Controls.Count; i++)
			{
				Control control = parent.Controls[i];
				point2 = control.PointToClient(point);
				if (control.ClientRectangle.Contains(point2))
				{
					return control;
				}
				if (control.Controls.Count > 0)
				{
					Control control2 = AccessibleObject.FindHittestControl(control, x, y);
					if (control2 != null)
					{
						return control2;
					}
				}
			}
			return null;
		}

		// Token: 0x04000529 RID: 1321
		internal string name;

		// Token: 0x0400052A RID: 1322
		internal string value;

		// Token: 0x0400052B RID: 1323
		internal Control owner;

		// Token: 0x0400052C RID: 1324
		internal AccessibleRole role;

		// Token: 0x0400052D RID: 1325
		internal AccessibleStates state;

		// Token: 0x0400052E RID: 1326
		internal string default_action;

		// Token: 0x0400052F RID: 1327
		internal string description;

		// Token: 0x04000530 RID: 1328
		internal string help;

		// Token: 0x04000531 RID: 1329
		internal string keyboard_shortcut;
	}
}
