using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Represents a class member, such as a property or event. This is an abstract base class.</summary>
	// Token: 0x020002B4 RID: 692
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class MemberDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MemberDescriptor" /> class with the specified name of the member.</summary>
		/// <param name="name">The name of the member. </param>
		/// <exception cref="T:System.ArgumentException">The name is an empty string ("") or null.</exception>
		// Token: 0x060015B3 RID: 5555 RVA: 0x00055CF6 File Offset: 0x00053EF6
		protected MemberDescriptor(string name)
			: this(name, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MemberDescriptor" /> class with the specified name of the member and an array of attributes.</summary>
		/// <param name="name">The name of the member. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that contains the member attributes. </param>
		/// <exception cref="T:System.ArgumentException">The name is an empty string ("") or null. </exception>
		// Token: 0x060015B4 RID: 5556 RVA: 0x00055D00 File Offset: 0x00053F00
		protected MemberDescriptor(string name, Attribute[] attributes)
		{
			this.lockCookie = new object();
			base..ctor();
			try
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(global::SR.GetString("Invalid member name."));
				}
				this.name = name;
				this.displayName = name;
				this.nameHash = name.GetHashCode();
				if (attributes != null)
				{
					this.attributes = attributes;
					this.attributesFiltered = false;
				}
				this.originalAttributes = this.attributes;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MemberDescriptor" /> class with the specified <see cref="T:System.ComponentModel.MemberDescriptor" />.</summary>
		/// <param name="descr">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that contains the name of the member and its attributes. </param>
		// Token: 0x060015B5 RID: 5557 RVA: 0x00055D84 File Offset: 0x00053F84
		protected MemberDescriptor(MemberDescriptor descr)
		{
			this.lockCookie = new object();
			base..ctor();
			this.name = descr.Name;
			this.displayName = this.name;
			this.nameHash = this.name.GetHashCode();
			this.attributes = new Attribute[descr.Attributes.Count];
			descr.Attributes.CopyTo(this.attributes, 0);
			this.attributesFiltered = true;
			this.originalAttributes = this.attributes;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MemberDescriptor" /> class with the name in the specified <see cref="T:System.ComponentModel.MemberDescriptor" /> and the attributes in both the old <see cref="T:System.ComponentModel.MemberDescriptor" /> and the <see cref="T:System.Attribute" /> array.</summary>
		/// <param name="oldMemberDescriptor">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that has the name of the member and its attributes. </param>
		/// <param name="newAttributes">An array of <see cref="T:System.Attribute" /> objects with the attributes you want to add to the member. </param>
		// Token: 0x060015B6 RID: 5558 RVA: 0x00055E08 File Offset: 0x00054008
		protected MemberDescriptor(MemberDescriptor oldMemberDescriptor, Attribute[] newAttributes)
		{
			this.lockCookie = new object();
			base..ctor();
			this.name = oldMemberDescriptor.Name;
			this.displayName = oldMemberDescriptor.DisplayName;
			this.nameHash = this.name.GetHashCode();
			ArrayList arrayList = new ArrayList();
			if (oldMemberDescriptor.Attributes.Count != 0)
			{
				foreach (object obj in oldMemberDescriptor.Attributes)
				{
					arrayList.Add(obj);
				}
			}
			if (newAttributes != null)
			{
				foreach (Attribute obj2 in newAttributes)
				{
					arrayList.Add(obj2);
				}
			}
			this.attributes = new Attribute[arrayList.Count];
			arrayList.CopyTo(this.attributes, 0);
			this.attributesFiltered = false;
			this.originalAttributes = this.attributes;
		}

		/// <summary>Gets or sets an array of attributes.</summary>
		/// <returns>An array of type <see cref="T:System.Attribute" /> that contains the attributes of this member. </returns>
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x00055F04 File Offset: 0x00054104
		// (set) Token: 0x060015B8 RID: 5560 RVA: 0x00055F18 File Offset: 0x00054118
		protected virtual Attribute[] AttributeArray
		{
			get
			{
				this.CheckAttributesValid();
				this.FilterAttributesIfNeeded();
				return this.attributes;
			}
			set
			{
				object obj = this.lockCookie;
				lock (obj)
				{
					this.attributes = value;
					this.originalAttributes = value;
					this.attributesFiltered = false;
					this.attributeCollection = null;
				}
			}
		}

		/// <summary>Gets the collection of attributes for this member.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> that provides the attributes for this member, or an empty collection if there are no attributes in the <see cref="P:System.ComponentModel.MemberDescriptor.AttributeArray" />.</returns>
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x00055F70 File Offset: 0x00054170
		public virtual AttributeCollection Attributes
		{
			get
			{
				this.CheckAttributesValid();
				AttributeCollection attributeCollection = this.attributeCollection;
				if (attributeCollection == null)
				{
					object obj = this.lockCookie;
					lock (obj)
					{
						attributeCollection = this.CreateAttributeCollection();
						this.attributeCollection = attributeCollection;
					}
				}
				return attributeCollection;
			}
		}

		/// <summary>Gets the name of the category to which the member belongs, as specified in the <see cref="T:System.ComponentModel.CategoryAttribute" />.</summary>
		/// <returns>The name of the category to which the member belongs. If there is no <see cref="T:System.ComponentModel.CategoryAttribute" />, the category name is set to the default category, Misc.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x00055FCC File Offset: 0x000541CC
		public virtual string Category
		{
			get
			{
				if (this.category == null)
				{
					this.category = ((CategoryAttribute)this.Attributes[typeof(CategoryAttribute)]).Category;
				}
				return this.category;
			}
		}

		/// <summary>Gets the description of the member, as specified in the <see cref="T:System.ComponentModel.DescriptionAttribute" />.</summary>
		/// <returns>The description of the member. If there is no <see cref="T:System.ComponentModel.DescriptionAttribute" />, the property value is set to the default, which is an empty string ("").</returns>
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x00056001 File Offset: 0x00054201
		public virtual string Description
		{
			get
			{
				if (this.description == null)
				{
					this.description = ((DescriptionAttribute)this.Attributes[typeof(DescriptionAttribute)]).Description;
				}
				return this.description;
			}
		}

		/// <summary>Gets a value indicating whether the member is browsable, as specified in the <see cref="T:System.ComponentModel.BrowsableAttribute" />.</summary>
		/// <returns>true if the member is browsable; otherwise, false. If there is no <see cref="T:System.ComponentModel.BrowsableAttribute" />, the property value is set to the default, which is true.</returns>
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x00056036 File Offset: 0x00054236
		public virtual bool IsBrowsable
		{
			get
			{
				return ((BrowsableAttribute)this.Attributes[typeof(BrowsableAttribute)]).Browsable;
			}
		}

		/// <summary>Gets the name of the member.</summary>
		/// <returns>The name of the member.</returns>
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x00056057 File Offset: 0x00054257
		public virtual string Name
		{
			get
			{
				if (this.name == null)
				{
					return "";
				}
				return this.name;
			}
		}

		/// <summary>Gets the hash code for the name of the member, as specified in <see cref="M:System.String.GetHashCode" />.</summary>
		/// <returns>The hash code for the name of the member.</returns>
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x0005606D File Offset: 0x0005426D
		protected virtual int NameHashCode
		{
			get
			{
				return this.nameHash;
			}
		}

		/// <summary>Gets whether this member should be set only at design time, as specified in the <see cref="T:System.ComponentModel.DesignOnlyAttribute" />.</summary>
		/// <returns>true if this member should be set only at design time; false if the member can be set during run time.</returns>
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x00056075 File Offset: 0x00054275
		public virtual bool DesignTimeOnly
		{
			get
			{
				return DesignOnlyAttribute.Yes.Equals(this.Attributes[typeof(DesignOnlyAttribute)]);
			}
		}

		/// <summary>Gets the name that can be displayed in a window, such as a Properties window.</summary>
		/// <returns>The name to display for the member.</returns>
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x00056098 File Offset: 0x00054298
		public virtual string DisplayName
		{
			get
			{
				DisplayNameAttribute displayNameAttribute = this.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
				if (displayNameAttribute == null || displayNameAttribute.IsDefaultAttribute())
				{
					return this.displayName;
				}
				return displayNameAttribute.DisplayName;
			}
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x000560D8 File Offset: 0x000542D8
		private void CheckAttributesValid()
		{
			if (this.attributesFiltered && this.metadataVersion != TypeDescriptor.MetadataVersion)
			{
				this.attributesFilled = false;
				this.attributesFiltered = false;
				this.attributeCollection = null;
			}
		}

		/// <summary>Creates a collection of attributes using the array of attributes passed to the constructor.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.AttributeCollection" /> that contains the <see cref="P:System.ComponentModel.MemberDescriptor.AttributeArray" /> attributes.</returns>
		// Token: 0x060015C2 RID: 5570 RVA: 0x00056104 File Offset: 0x00054304
		protected virtual AttributeCollection CreateAttributeCollection()
		{
			return new AttributeCollection(this.AttributeArray);
		}

		/// <summary>Compares this instance to the given object to see if they are equivalent.</summary>
		/// <returns>true if equivalent; otherwise, false.</returns>
		/// <param name="obj">The object to compare to the current instance. </param>
		// Token: 0x060015C3 RID: 5571 RVA: 0x00056114 File Offset: 0x00054314
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			if (obj.GetType() != base.GetType())
			{
				return false;
			}
			MemberDescriptor memberDescriptor = (MemberDescriptor)obj;
			this.FilterAttributesIfNeeded();
			memberDescriptor.FilterAttributesIfNeeded();
			if (memberDescriptor.nameHash != this.nameHash)
			{
				return false;
			}
			if (memberDescriptor.category == null != (this.category == null) || (this.category != null && !memberDescriptor.category.Equals(this.category)))
			{
				return false;
			}
			if (!LocalAppContextSwitches.MemberDescriptorEqualsReturnsFalseIfEquivalent)
			{
				if (memberDescriptor.description == null != (this.description == null) || (this.description != null && !memberDescriptor.description.Equals(this.description)))
				{
					return false;
				}
			}
			else if (memberDescriptor.description == null != (this.description == null) || (this.description != null && !memberDescriptor.category.Equals(this.description)))
			{
				return false;
			}
			if (memberDescriptor.attributes == null != (this.attributes == null))
			{
				return false;
			}
			bool flag = true;
			if (this.attributes != null)
			{
				if (this.attributes.Length != memberDescriptor.attributes.Length)
				{
					return false;
				}
				for (int i = 0; i < this.attributes.Length; i++)
				{
					if (!this.attributes[i].Equals(memberDescriptor.attributes[i]))
					{
						flag = false;
						break;
					}
				}
			}
			return flag;
		}

		/// <summary>When overridden in a derived class, adds the attributes of the inheriting class to the specified list of attributes in the parent class.</summary>
		/// <param name="attributeList">An <see cref="T:System.Collections.IList" /> that lists the attributes in the parent class. Initially, this is empty. </param>
		// Token: 0x060015C4 RID: 5572 RVA: 0x00056264 File Offset: 0x00054464
		protected virtual void FillAttributes(IList attributeList)
		{
			if (this.originalAttributes != null)
			{
				foreach (Attribute attribute in this.originalAttributes)
				{
					attributeList.Add(attribute);
				}
			}
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0005629C File Offset: 0x0005449C
		private void FilterAttributesIfNeeded()
		{
			if (!this.attributesFiltered)
			{
				IList list;
				if (!this.attributesFilled)
				{
					list = new ArrayList();
					try
					{
						this.FillAttributes(list);
						goto IL_0034;
					}
					catch (ThreadAbortException)
					{
						throw;
					}
					catch (Exception)
					{
						goto IL_0034;
					}
				}
				list = new ArrayList(this.attributes);
				IL_0034:
				Hashtable hashtable = new Hashtable(list.Count);
				foreach (object obj in list)
				{
					Attribute attribute = (Attribute)obj;
					hashtable[attribute.TypeId] = attribute;
				}
				Attribute[] array = new Attribute[hashtable.Values.Count];
				hashtable.Values.CopyTo(array, 0);
				object obj2 = this.lockCookie;
				lock (obj2)
				{
					this.attributes = array;
					this.attributesFiltered = true;
					this.attributesFilled = true;
					this.metadataVersion = TypeDescriptor.MetadataVersion;
				}
			}
		}

		/// <summary>Finds the given method through reflection, searching only for public methods.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> that represents the method, or null if the method is not found.</returns>
		/// <param name="componentClass">The component that contains the method. </param>
		/// <param name="name">The name of the method to find. </param>
		/// <param name="args">An array of parameters for the method, used to choose between overloaded methods. </param>
		/// <param name="returnType">The type to return for the method. </param>
		// Token: 0x060015C6 RID: 5574 RVA: 0x000563C0 File Offset: 0x000545C0
		protected static MethodInfo FindMethod(Type componentClass, string name, Type[] args, Type returnType)
		{
			return MemberDescriptor.FindMethod(componentClass, name, args, returnType, true);
		}

		/// <summary>Finds the given method through reflection, with an option to search only public methods.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> that represents the method, or null if the method is not found.</returns>
		/// <param name="componentClass">The component that contains the method. </param>
		/// <param name="name">The name of the method to find. </param>
		/// <param name="args">An array of parameters for the method, used to choose between overloaded methods. </param>
		/// <param name="returnType">The type to return for the method. </param>
		/// <param name="publicOnly">Whether to restrict search to public methods. </param>
		// Token: 0x060015C7 RID: 5575 RVA: 0x000563CC File Offset: 0x000545CC
		protected static MethodInfo FindMethod(Type componentClass, string name, Type[] args, Type returnType, bool publicOnly)
		{
			MethodInfo methodInfo;
			if (publicOnly)
			{
				methodInfo = componentClass.GetMethod(name, args);
			}
			else
			{
				methodInfo = componentClass.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, args, null);
			}
			if (methodInfo != null && !methodInfo.ReturnType.IsEquivalentTo(returnType))
			{
				methodInfo = null;
			}
			return methodInfo;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.MemberDescriptor" />.</returns>
		// Token: 0x060015C8 RID: 5576 RVA: 0x0005606D File Offset: 0x0005426D
		public override int GetHashCode()
		{
			return this.nameHash;
		}

		/// <summary>Retrieves the object that should be used during invocation of members.</summary>
		/// <returns>The object to be used during member invocations.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the invocation target.</param>
		/// <param name="instance">The potential invocation target.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="instance" /> is null.</exception>
		// Token: 0x060015C9 RID: 5577 RVA: 0x00056411 File Offset: 0x00054611
		protected virtual object GetInvocationTarget(Type type, object instance)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return TypeDescriptor.GetAssociation(type, instance);
		}

		/// <summary>Gets a component site for the given component.</summary>
		/// <returns>The site of the component, or null if a site does not exist.</returns>
		/// <param name="component">The component for which you want to find a site. </param>
		// Token: 0x060015CA RID: 5578 RVA: 0x0005643C File Offset: 0x0005463C
		protected static ISite GetSite(object component)
		{
			if (!(component is IComponent))
			{
				return null;
			}
			return ((IComponent)component).Site;
		}

		/// <summary>Gets the component on which to invoke a method.</summary>
		/// <returns>An instance of the component to invoke. This method returns a visual designer when the property is attached to a visual designer.</returns>
		/// <param name="componentClass">A <see cref="T:System.Type" /> representing the type of component this <see cref="T:System.ComponentModel.MemberDescriptor" /> is bound to. For example, if this <see cref="T:System.ComponentModel.MemberDescriptor" /> describes a property, this parameter should be the class that the property is declared on. </param>
		/// <param name="component">An instance of the object to call. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="componentClass" /> or <paramref name="component" /> is null.</exception>
		// Token: 0x060015CB RID: 5579 RVA: 0x00056453 File Offset: 0x00054653
		[Obsolete("This method has been deprecated. Use GetInvocationTarget instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		protected static object GetInvokee(Type componentClass, object component)
		{
			if (componentClass == null)
			{
				throw new ArgumentNullException("componentClass");
			}
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return TypeDescriptor.GetAssociation(componentClass, component);
		}

		// Token: 0x04001369 RID: 4969
		private string name;

		// Token: 0x0400136A RID: 4970
		private string displayName;

		// Token: 0x0400136B RID: 4971
		private int nameHash;

		// Token: 0x0400136C RID: 4972
		private AttributeCollection attributeCollection;

		// Token: 0x0400136D RID: 4973
		private Attribute[] attributes;

		// Token: 0x0400136E RID: 4974
		private Attribute[] originalAttributes;

		// Token: 0x0400136F RID: 4975
		private bool attributesFiltered;

		// Token: 0x04001370 RID: 4976
		private bool attributesFilled;

		// Token: 0x04001371 RID: 4977
		private int metadataVersion;

		// Token: 0x04001372 RID: 4978
		private string category;

		// Token: 0x04001373 RID: 4979
		private string description;

		// Token: 0x04001374 RID: 4980
		private object lockCookie;
	}
}
