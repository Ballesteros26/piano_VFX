using System;

namespace System.Web.UI
{
	/// <summary>Specifies the default property of a control that a <see cref="T:System.Web.UI.WebControls.ControlParameter" /> object binds to at run time. This class cannot be inherited.</summary>
	// Token: 0x020001BC RID: 444
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class ControlValuePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ControlValuePropertyAttribute" /> class using the specified property name.</summary>
		/// <param name="name">The default property for the control.</param>
		// Token: 0x06001200 RID: 4608 RVA: 0x00031C08 File Offset: 0x0002FE08
		public ControlValuePropertyAttribute(string name)
		{
			this.propertyName = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ControlValuePropertyAttribute" /> class using the specified property name and default value.</summary>
		/// <param name="name">The default property for the control.</param>
		/// <param name="defaultValue">The default value for the default property.</param>
		// Token: 0x06001201 RID: 4609 RVA: 0x00031C17 File Offset: 0x0002FE17
		public ControlValuePropertyAttribute(string name, object defaultValue)
		{
			this.propertyName = name;
			this.propertyValue = defaultValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ControlValuePropertyAttribute" /> class using the specified property name and default value. The default value is also converted to the specified data type.</summary>
		/// <param name="name">The default property for the control.</param>
		/// <param name="type">The <see cref="T:System.Type" /> to which the default value is converted.</param>
		/// <param name="defaultValue">The default value for the default property.</param>
		// Token: 0x06001202 RID: 4610 RVA: 0x00031C2D File Offset: 0x0002FE2D
		public ControlValuePropertyAttribute(string name, Type type, string defaultValue)
		{
			this.propertyName = name;
			this.propertyValue = defaultValue;
			this.propertyType = type;
		}

		/// <summary>Gets the default property for a control.</summary>
		/// <returns>The default property for a control.</returns>
		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x00031C4A File Offset: 0x0002FE4A
		public string Name
		{
			get
			{
				return this.propertyName;
			}
		}

		/// <summary>Gets the default value for the default property of a control.</summary>
		/// <returns>The default value for the default property of a control.</returns>
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x00031C52 File Offset: 0x0002FE52
		public object DefaultValue
		{
			get
			{
				return this.propertyValue;
			}
		}

		/// <summary>Determines whether the current instance of the <see cref="T:System.Web.UI.ControlValuePropertyAttribute" /> object is equal to the specified object.</summary>
		/// <returns>true if the object contained in the <paramref name="obj" /> parameter is equal to the current instance of <see cref="T:System.Web.UI.ControlValuePropertyAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with this instance.</param>
		// Token: 0x06001205 RID: 4613 RVA: 0x00031C5C File Offset: 0x0002FE5C
		public override bool Equals(object obj)
		{
			if (obj != null && obj is ControlValuePropertyAttribute)
			{
				ControlValuePropertyAttribute controlValuePropertyAttribute = (ControlValuePropertyAttribute)obj;
				return this.propertyName == controlValuePropertyAttribute.propertyName && this.propertyValue == controlValuePropertyAttribute.propertyValue && this.propertyType == controlValuePropertyAttribute.propertyType;
			}
			return false;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001206 RID: 4614 RVA: 0x00031CB1 File Offset: 0x0002FEB1
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400140D RID: 5133
		private string propertyName;

		// Token: 0x0400140E RID: 5134
		private object propertyValue;

		// Token: 0x0400140F RID: 5135
		private Type propertyType;
	}
}
