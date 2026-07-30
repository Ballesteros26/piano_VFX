using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the default value for a property.</summary>
	// Token: 0x0200025B RID: 603
	[AttributeUsage(AttributeTargets.All)]
	public class DefaultValueAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class, converting the specified value to the specified type, and using an invariant culture as the translation context.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to convert the value to. </param>
		/// <param name="value">A <see cref="T:System.String" /> that can be converted to the type using the <see cref="T:System.ComponentModel.TypeConverter" /> for the type and the U.S. English culture. </param>
		// Token: 0x0600134E RID: 4942 RVA: 0x0005134C File Offset: 0x0004F54C
		public DefaultValueAttribute(Type type, string value)
		{
			try
			{
				this.value = TypeDescriptor.GetConverter(type).ConvertFromInvariantString(value);
			}
			catch
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a Unicode character.</summary>
		/// <param name="value">A Unicode character that is the default value. </param>
		// Token: 0x0600134F RID: 4943 RVA: 0x00051388 File Offset: 0x0004F588
		public DefaultValueAttribute(char value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using an 8-bit unsigned integer.</summary>
		/// <param name="value">An 8-bit unsigned integer that is the default value. </param>
		// Token: 0x06001350 RID: 4944 RVA: 0x0005139C File Offset: 0x0004F59C
		public DefaultValueAttribute(byte value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 16-bit signed integer.</summary>
		/// <param name="value">A 16-bit signed integer that is the default value. </param>
		// Token: 0x06001351 RID: 4945 RVA: 0x000513B0 File Offset: 0x0004F5B0
		public DefaultValueAttribute(short value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 32-bit signed integer.</summary>
		/// <param name="value">A 32-bit signed integer that is the default value. </param>
		// Token: 0x06001352 RID: 4946 RVA: 0x000513C4 File Offset: 0x0004F5C4
		public DefaultValueAttribute(int value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a 64-bit signed integer.</summary>
		/// <param name="value">A 64-bit signed integer that is the default value. </param>
		// Token: 0x06001353 RID: 4947 RVA: 0x000513D8 File Offset: 0x0004F5D8
		public DefaultValueAttribute(long value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a single-precision floating point number.</summary>
		/// <param name="value">A single-precision floating point number that is the default value. </param>
		// Token: 0x06001354 RID: 4948 RVA: 0x000513EC File Offset: 0x0004F5EC
		public DefaultValueAttribute(float value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a double-precision floating point number.</summary>
		/// <param name="value">A double-precision floating point number that is the default value. </param>
		// Token: 0x06001355 RID: 4949 RVA: 0x00051400 File Offset: 0x0004F600
		public DefaultValueAttribute(double value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a <see cref="T:System.Boolean" /> value.</summary>
		/// <param name="value">A <see cref="T:System.Boolean" /> that is the default value. </param>
		// Token: 0x06001356 RID: 4950 RVA: 0x00051414 File Offset: 0x0004F614
		public DefaultValueAttribute(bool value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class using a <see cref="T:System.String" />.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that is the default value. </param>
		// Token: 0x06001357 RID: 4951 RVA: 0x00051428 File Offset: 0x0004F628
		public DefaultValueAttribute(string value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultValueAttribute" /> class.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> that represents the default value. </param>
		// Token: 0x06001358 RID: 4952 RVA: 0x00051428 File Offset: 0x0004F628
		public DefaultValueAttribute(object value)
		{
			this.value = value;
		}

		/// <summary>Gets the default value of the property this attribute is bound to.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the default value of the property this attribute is bound to.</returns>
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x00051437 File Offset: 0x0004F637
		public virtual object Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DefaultValueAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x0600135A RID: 4954 RVA: 0x00051440 File Offset: 0x0004F640
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DefaultValueAttribute defaultValueAttribute = obj as DefaultValueAttribute;
			if (defaultValueAttribute == null)
			{
				return false;
			}
			if (this.Value != null)
			{
				return this.Value.Equals(defaultValueAttribute.Value);
			}
			return defaultValueAttribute.Value == null;
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Sets the default value for the property to which this attribute is bound.</summary>
		/// <param name="value">The default value.</param>
		// Token: 0x0600135C RID: 4956 RVA: 0x00051482 File Offset: 0x0004F682
		protected void SetValue(object value)
		{
			this.value = value;
		}

		// Token: 0x040012AC RID: 4780
		private object value;
	}
}
