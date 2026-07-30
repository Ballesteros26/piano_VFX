using System;
using System.Collections;

namespace System.Web.UI
{
	/// <summary>Defines the metadata attribute that Web server controls and their members use to indicate whether their rendering can be affected by themes and control skins. This class cannot be inherited.</summary>
	// Token: 0x02000196 RID: 406
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class ThemeableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ThemeableAttribute" /> class, using the specified Boolean value to determine whether the attribute represents a type or member that is affected by themes and control skins.</summary>
		/// <param name="themeable">true to initialize the <see cref="T:System.Web.UI.ThemeableAttribute" /> to represent a type or member that can be affected by themes; otherwise, false.</param>
		// Token: 0x06000FC9 RID: 4041 RVA: 0x0002B7C4 File Offset: 0x000299C4
		public ThemeableAttribute(bool themeable)
		{
			this._themeable = themeable;
		}

		/// <summary>Gets a value indicating whether the current control or member of a control can be affected by themes and control skins defined for the Web application.</summary>
		/// <returns>true if the current type or member can be affected by themes; otherwise, false. The default is false.</returns>
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x0002B7D3 File Offset: 0x000299D3
		public bool Themeable
		{
			get
			{
				return this._themeable;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is the same instance as the current instance, or if the instances are different, but the attribute values are equivalent; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x06000FCB RID: 4043 RVA: 0x0002B7DC File Offset: 0x000299DC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ThemeableAttribute themeableAttribute = obj as ThemeableAttribute;
			return themeableAttribute != null && themeableAttribute.Themeable == this._themeable;
		}

		/// <summary>Serves as a hash function for the <see cref="T:System.Web.UI.ThemeableAttribute" /> type. </summary>
		/// <returns>A hash code for the current <see cref="T:System.Web.UI.ThemeableAttribute" />.</returns>
		// Token: 0x06000FCC RID: 4044 RVA: 0x0002B809 File Offset: 0x00029A09
		public override int GetHashCode()
		{
			return this._themeable.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current instance is equivalent to a <see cref="F:System.Web.UI.ThemeableAttribute.Default" /> instance of the <see cref="T:System.Web.UI.ThemeableAttribute" /> class.</summary>
		/// <returns>true if the current instance is equivalent to a <see cref="F:System.Web.UI.ThemeableAttribute.Default" /> instance of the class; otherwise, false.</returns>
		// Token: 0x06000FCD RID: 4045 RVA: 0x0002B816 File Offset: 0x00029A16
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ThemeableAttribute.Default);
		}

		/// <summary>Returns a value indicating whether the object passed to the method supports themes. </summary>
		/// <returns>true if the object supports themes and control skins; otherwise, false.</returns>
		/// <param name="instance">The object to test for themes support.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="instance" /> parameter is null.</exception>
		// Token: 0x06000FCE RID: 4046 RVA: 0x0002B823 File Offset: 0x00029A23
		public static bool IsObjectThemeable(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return ThemeableAttribute.IsTypeThemeable(instance.GetType());
		}

		/// <summary>Returns a value indicating whether the <see cref="T:System.Type" /> passed to the method supports themes.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> supports themes and control skins; otherwise, false.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> to test for themes support.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null.</exception>
		// Token: 0x06000FCF RID: 4047 RVA: 0x0002B840 File Offset: 0x00029A40
		public static bool IsTypeThemeable(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object obj = ThemeableAttribute._themeableTypes[type];
			if (obj != null)
			{
				return (bool)obj;
			}
			ThemeableAttribute themeableAttribute = Attribute.GetCustomAttribute(type, typeof(ThemeableAttribute)) as ThemeableAttribute;
			obj = themeableAttribute != null && themeableAttribute.Themeable;
			ThemeableAttribute._themeableTypes[type] = obj;
			return (bool)obj;
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.ThemeableAttribute" /> instance used to decorate a type or member that is affected by themes and control skins.</summary>
		// Token: 0x04001333 RID: 4915
		public static readonly ThemeableAttribute Yes = new ThemeableAttribute(true);

		/// <summary>Gets a <see cref="T:System.Web.UI.ThemeableAttribute" /> instance used to decorate a type or member that is not affected by themes and control skins.</summary>
		// Token: 0x04001334 RID: 4916
		public static readonly ThemeableAttribute No = new ThemeableAttribute(false);

		/// <summary>Gets a <see cref="T:System.Web.UI.ThemeableAttribute" /> instance that represents the application-defined default value of the attribute.</summary>
		// Token: 0x04001335 RID: 4917
		public static readonly ThemeableAttribute Default = ThemeableAttribute.Yes;

		// Token: 0x04001336 RID: 4918
		private bool _themeable;

		// Token: 0x04001337 RID: 4919
		private static Hashtable _themeableTypes = Hashtable.Synchronized(new Hashtable());
	}
}
