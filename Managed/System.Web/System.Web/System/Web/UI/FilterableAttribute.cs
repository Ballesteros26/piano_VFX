using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI
{
	/// <summary>Specifies whether the property to which the attribute is applied supports device filtering. This class cannot be inherited.</summary>
	// Token: 0x02000162 RID: 354
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class FilterableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.FilterableAttribute" /> class.</summary>
		/// <param name="filterable">true to indicate that the property to which the attribute is applied supports device filtering; otherwise, false.</param>
		// Token: 0x06000F48 RID: 3912 RVA: 0x0002B356 File Offset: 0x00029556
		public FilterableAttribute(bool filterable)
		{
			this._filterable = filterable;
		}

		/// <summary>Gets a value indicating whether the property to which the <see cref="T:System.Web.UI.FilterableAttribute" /> attribute is applied supports device filtering.</summary>
		/// <returns>true to indicate that the property to which the attribute is applied supports device filtering; otherwise, false.</returns>
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x0002B365 File Offset: 0x00029565
		public bool Filterable
		{
			get
			{
				return this._filterable;
			}
		}

		/// <summary>Determines whether the current instance of the <see cref="T:System.Web.UI.FilterableAttribute" /> class is equal to the specified object.</summary>
		/// <returns>true if the object contained in the <paramref name="obj" /> parameter is equal to the current instance of the <see cref="T:System.Web.UI.FilterableAttribute" /> object; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with this instance.</param>
		// Token: 0x06000F4A RID: 3914 RVA: 0x0002B370 File Offset: 0x00029570
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			FilterableAttribute filterableAttribute = obj as FilterableAttribute;
			return filterableAttribute != null && filterableAttribute.Filterable == this._filterable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000F4B RID: 3915 RVA: 0x0002B39D File Offset: 0x0002959D
		public override int GetHashCode()
		{
			return this._filterable.GetHashCode();
		}

		/// <summary>Determines whether the current instance of the <see cref="T:System.Web.UI.FilterableAttribute" /> class is equal to the <see cref="F:System.Web.UI.FilterableAttribute.Default" /> attribute.</summary>
		/// <returns>true if the current instance of <see cref="T:System.Web.UI.FilterableAttribute" /> is equal to <see cref="F:System.Web.UI.FilterableAttribute.Default" />; otherwise, false.</returns>
		// Token: 0x06000F4C RID: 3916 RVA: 0x0002B3AA File Offset: 0x000295AA
		public override bool IsDefaultAttribute()
		{
			return this.Equals(FilterableAttribute.Default);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> supports device filtering.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> contained in the <paramref name="instance" /> parameter supports device filtering; otherwise, false.</returns>
		/// <param name="instance">The <see cref="T:System.Object" /> to test.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instance" /> parameter is null.</exception>
		// Token: 0x06000F4D RID: 3917 RVA: 0x0002B3B7 File Offset: 0x000295B7
		public static bool IsObjectFilterable(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return FilterableAttribute.IsTypeFilterable(instance.GetType());
		}

		/// <summary>Determines whether a property supports device filtering.</summary>
		/// <returns>true if the property represented by the <see cref="T:System.ComponentModel.PropertyDescriptor" /> object contained in the <paramref name="propertyDescriptor" /> parameter supports device filtering; otherwise, false.</returns>
		/// <param name="propertyDescriptor">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that contains the properties of the property to test.</param>
		// Token: 0x06000F4E RID: 3918 RVA: 0x0002B3D4 File Offset: 0x000295D4
		public static bool IsPropertyFilterable(PropertyDescriptor propertyDescriptor)
		{
			FilterableAttribute filterableAttribute = (FilterableAttribute)propertyDescriptor.Attributes[typeof(FilterableAttribute)];
			return filterableAttribute == null || filterableAttribute.Filterable;
		}

		/// <summary>Determines whether the specified data type supports device filtering.</summary>
		/// <returns>true if the data type contained in the <paramref name="type" /> parameter supports device filtering; otherwise, false.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the data type to test.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null.</exception>
		// Token: 0x06000F4F RID: 3919 RVA: 0x0002B408 File Offset: 0x00029608
		public static bool IsTypeFilterable(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object obj = FilterableAttribute._filterableTypes[type];
			if (obj != null)
			{
				return (bool)obj;
			}
			FilterableAttribute filterableAttribute = (FilterableAttribute)TypeDescriptor.GetAttributes(type)[typeof(FilterableAttribute)];
			obj = filterableAttribute != null && filterableAttribute.Filterable;
			FilterableAttribute._filterableTypes[type] = obj;
			return (bool)obj;
		}

		/// <summary>Represents a predefined <see cref="T:System.Web.UI.FilterableAttribute" /> object that indicates that a property supports device filtering. This field is read-only.</summary>
		// Token: 0x04001247 RID: 4679
		public static readonly FilterableAttribute Yes = new FilterableAttribute(true);

		/// <summary>Represents a predefined <see cref="T:System.Web.UI.FilterableAttribute" /> object that indicates that a property does not support device filtering. This field is read-only.</summary>
		// Token: 0x04001248 RID: 4680
		public static readonly FilterableAttribute No = new FilterableAttribute(false);

		/// <summary>Represents a predefined <see cref="T:System.Web.UI.FilterableAttribute" /> object with default property settings. This field is read-only.</summary>
		// Token: 0x04001249 RID: 4681
		public static readonly FilterableAttribute Default = FilterableAttribute.Yes;

		// Token: 0x0400124A RID: 4682
		private bool _filterable;

		// Token: 0x0400124B RID: 4683
		private static Hashtable _filterableTypes = Hashtable.Synchronized(new Hashtable());
	}
}
