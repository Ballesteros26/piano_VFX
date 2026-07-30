using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Represents the personalization attribute. This class cannot be inherited.</summary>
	/// <exception cref="T:System.Web.HttpException">The property is a read-only or write-only public property.- or -The property is a private or protected read/write property.- or -The property has index parameters.</exception>
	// Token: 0x02000487 RID: 1159
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class PersonalizableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> class. </summary>
		// Token: 0x0600347F RID: 13439 RVA: 0x0008AD06 File Offset: 0x00088F06
		public PersonalizableAttribute()
			: this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> class using the provided parameter.</summary>
		/// <param name="isPersonalizable">A Boolean value indicating whether the property can be personalized.</param>
		// Token: 0x06003480 RID: 13440 RVA: 0x0008AD0F File Offset: 0x00088F0F
		public PersonalizableAttribute(bool isPersonalizable)
		{
			this.isPersonalizable = isPersonalizable;
			this.scope = PersonalizationScope.User;
			this.isSensitive = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> class using the provided parameter.</summary>
		/// <param name="scope">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> indicating the scope of the personalization.</param>
		// Token: 0x06003481 RID: 13441 RVA: 0x0008AD2C File Offset: 0x00088F2C
		public PersonalizableAttribute(PersonalizationScope scope)
			: this(scope, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> class using the provided parameters.</summary>
		/// <param name="scope">A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> indicating the scope of the personalization.</param>
		/// <param name="isSensitive">A Boolean value indicating whether the property information is considered sensitive.</param>
		// Token: 0x06003482 RID: 13442 RVA: 0x0008AD36 File Offset: 0x00088F36
		public PersonalizableAttribute(PersonalizationScope scope, bool isSensitive)
		{
			this.isPersonalizable = true;
			this.scope = scope;
			this.isSensitive = isSensitive;
		}

		/// <summary>Gets the setting that indicates whether the attribute can be personalized, as established by one of the constructors.</summary>
		/// <returns>true if the property can be personalized; otherwise, false.</returns>
		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x06003483 RID: 13443 RVA: 0x0008AD53 File Offset: 0x00088F53
		public bool IsPersonalizable
		{
			get
			{
				return this.isPersonalizable;
			}
		}

		/// <summary>Gets the setting that indicates whether the attribute is sensitive, as established by one of the constructors.</summary>
		/// <returns>true if the property is sensitive; otherwise, false.</returns>
		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06003484 RID: 13444 RVA: 0x0008AD5B File Offset: 0x00088F5B
		public bool IsSensitive
		{
			get
			{
				return this.isSensitive;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration value for the class instance, as set by one of the constructors.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationScope" /> enumeration value.</returns>
		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06003485 RID: 13445 RVA: 0x0008AD63 File Offset: 0x00088F63
		public PersonalizationScope Scope
		{
			get
			{
				return this.scope;
			}
		}

		/// <summary>When overridden, returns a Boolean evaluation of the current instance of <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> and another <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> instance supplied as a parameter.</summary>
		/// <returns>true if the values are equal; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> to be compared to the current instance.</param>
		// Token: 0x06003486 RID: 13446 RVA: 0x0008AD6C File Offset: 0x00088F6C
		public override bool Equals(object obj)
		{
			PersonalizableAttribute personalizableAttribute = obj as PersonalizableAttribute;
			return personalizableAttribute != null && (this.isPersonalizable == personalizableAttribute.IsPersonalizable && this.isSensitive == personalizableAttribute.IsSensitive) && this.scope == personalizableAttribute.Scope;
		}

		/// <summary>When overridden, returns a hash code of the attribute.</summary>
		/// <returns>A hash code in the form of an integer.</returns>
		// Token: 0x06003487 RID: 13447 RVA: 0x0008ADB1 File Offset: 0x00088FB1
		public override int GetHashCode()
		{
			return this.isPersonalizable.GetHashCode() ^ this.isSensitive.GetHashCode() ^ this.scope.GetHashCode();
		}

		/// <summary>Returns a collection of <see cref="T:System.Reflection.PropertyInfo" /> objects for the properties that match the parameter type and are marked as personalizable.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of personalizable properties.</returns>
		/// <param name="type">The type on which to look for Personalizable properties.</param>
		/// <exception cref="T:System.Web.HttpException">A public property on the type is marked as personalizable but is read-only.</exception>
		// Token: 0x06003488 RID: 13448 RVA: 0x0008ADDC File Offset: 0x00088FDC
		public static ICollection GetPersonalizableProperties(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			PropertyInfo[] properties = type.GetProperties();
			if (properties == null || properties.Length == 0)
			{
				return new PropertyInfo[0];
			}
			List<PropertyInfo> list = null;
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (PersonalizableAttribute.PropertyQualifies(propertyInfo))
				{
					if (list == null)
					{
						list = new List<PropertyInfo>();
					}
					list.Add(propertyInfo);
				}
			}
			return list;
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x0008AE44 File Offset: 0x00089044
		private static bool PropertyQualifies(PropertyInfo pi)
		{
			object[] customAttributes = pi.GetCustomAttributes(false);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				return false;
			}
			object[] array = customAttributes;
			int i = 0;
			while (i < array.Length)
			{
				PersonalizableAttribute personalizableAttribute = array[i] as PersonalizableAttribute;
				if (personalizableAttribute != null && personalizableAttribute.IsPersonalizable)
				{
					if (pi.GetSetMethod(false) == null)
					{
						throw new HttpException("A public property on the type is marked as personalizable but is read-only.");
					}
					return true;
				}
				else
				{
					i++;
				}
			}
			return false;
		}

		/// <summary>When overridden, returns a value that indicates whether the attribute instance equals the value of the static <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizableAttribute.Default" /> field.</summary>
		/// <returns>true if the attribute instance equals the static <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizableAttribute.Default" /> field; otherwise, false.</returns>
		// Token: 0x0600348A RID: 13450 RVA: 0x0008AEA3 File Offset: 0x000890A3
		public override bool IsDefaultAttribute()
		{
			return object.Equals(this, PersonalizableAttribute.Default);
		}

		/// <summary>Returns a value that indicates whether the current instance of <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> and the specified <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> have the same <see cref="P:System.Web.UI.WebControls.WebParts.PersonalizableAttribute.IsPersonalizable" /> property value.</summary>
		/// <returns>true if the two attributes have the same <see cref="P:System.Web.UI.WebControls.WebParts.PersonalizableAttribute.IsPersonalizable" /> value; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizableAttribute" /> to be compared to the current instance.</param>
		// Token: 0x0600348B RID: 13451 RVA: 0x0008AEB0 File Offset: 0x000890B0
		public override bool Match(object obj)
		{
			PersonalizableAttribute personalizableAttribute = obj as PersonalizableAttribute;
			return obj != null && this.isPersonalizable == personalizableAttribute.IsPersonalizable;
		}

		/// <summary>Returns an attribute instance that indicates no support for personalization. This field is read-only.</summary>
		// Token: 0x04001D14 RID: 7444
		public static readonly PersonalizableAttribute Default = new PersonalizableAttribute(false);

		/// <summary>Returns an attribute instance that indicates no support for personalization. This field is read-only.</summary>
		// Token: 0x04001D15 RID: 7445
		public static readonly PersonalizableAttribute NotPersonalizable = PersonalizableAttribute.Default;

		/// <summary>Returns an attribute instance that indicates support for personalization. This field is read-only.</summary>
		// Token: 0x04001D16 RID: 7446
		public static readonly PersonalizableAttribute Personalizable = new PersonalizableAttribute(PersonalizationScope.User, false);

		/// <summary>Returns an attribute instance that indicates support for personalization with a shared scope. This field is read-only.</summary>
		// Token: 0x04001D17 RID: 7447
		public static readonly PersonalizableAttribute SharedPersonalizable = new PersonalizableAttribute(PersonalizationScope.Shared, false);

		/// <summary>Returns an attribute instance that indicates support for personalization in <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> scope. This field is read-only.</summary>
		// Token: 0x04001D18 RID: 7448
		public static readonly PersonalizableAttribute UserPersonalizable = new PersonalizableAttribute(PersonalizationScope.User, false);

		// Token: 0x04001D19 RID: 7449
		private bool isPersonalizable;

		// Token: 0x04001D1A RID: 7450
		private bool isSensitive;

		// Token: 0x04001D1B RID: 7451
		private PersonalizationScope scope;
	}
}
