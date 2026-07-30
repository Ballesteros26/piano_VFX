using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the value to pass to a property to cause the property to get its value from another source. This is known as ambience. This class cannot be inherited.</summary>
	// Token: 0x02000226 RID: 550
	[AttributeUsage(AttributeTargets.All)]
	public sealed class AmbientValueAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given the value and its type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the <paramref name="value" /> parameter. </param>
		/// <param name="value">The value for this attribute. </param>
		// Token: 0x060011CB RID: 4555 RVA: 0x0004C854 File Offset: 0x0004AA54
		public AmbientValueAttribute(Type type, string value)
		{
			try
			{
				this.value = TypeDescriptor.GetConverter(type).ConvertFromInvariantString(value);
			}
			catch
			{
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a Unicode character for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060011CC RID: 4556 RVA: 0x0004C890 File Offset: 0x0004AA90
		public AmbientValueAttribute(char value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given an 8-bit unsigned integer for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060011CD RID: 4557 RVA: 0x0004C8A4 File Offset: 0x0004AAA4
		public AmbientValueAttribute(byte value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a 16-bit signed integer for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060011CE RID: 4558 RVA: 0x0004C8B8 File Offset: 0x0004AAB8
		public AmbientValueAttribute(short value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a 32-bit signed integer for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060011CF RID: 4559 RVA: 0x0004C8CC File Offset: 0x0004AACC
		public AmbientValueAttribute(int value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a 64-bit signed integer for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060011D0 RID: 4560 RVA: 0x0004C8E0 File Offset: 0x0004AAE0
		public AmbientValueAttribute(long value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a single-precision floating point number for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060011D1 RID: 4561 RVA: 0x0004C8F4 File Offset: 0x0004AAF4
		public AmbientValueAttribute(float value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a double-precision floating-point number for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060011D2 RID: 4562 RVA: 0x0004C908 File Offset: 0x0004AB08
		public AmbientValueAttribute(double value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a Boolean value for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060011D3 RID: 4563 RVA: 0x0004C91C File Offset: 0x0004AB1C
		public AmbientValueAttribute(bool value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a string for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060011D4 RID: 4564 RVA: 0x0004C930 File Offset: 0x0004AB30
		public AmbientValueAttribute(string value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given an object for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x060011D5 RID: 4565 RVA: 0x0004C930 File Offset: 0x0004AB30
		public AmbientValueAttribute(object value)
		{
			this.value = value;
		}

		/// <summary>Gets the object that is the value of this <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</summary>
		/// <returns>The object that is the value of this <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</returns>
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x0004C93F File Offset: 0x0004AB3F
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.ComponentModel.AmbientValueAttribute" /> is equal to the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.AmbientValueAttribute" /> is equal to the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.ComponentModel.AmbientValueAttribute" /> to compare with the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</param>
		// Token: 0x060011D7 RID: 4567 RVA: 0x0004C948 File Offset: 0x0004AB48
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			AmbientValueAttribute ambientValueAttribute = obj as AmbientValueAttribute;
			if (ambientValueAttribute == null)
			{
				return false;
			}
			if (this.value != null)
			{
				return this.value.Equals(ambientValueAttribute.Value);
			}
			return ambientValueAttribute.Value == null;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</returns>
		// Token: 0x060011D8 RID: 4568 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04001228 RID: 4648
		private readonly object value;
	}
}
