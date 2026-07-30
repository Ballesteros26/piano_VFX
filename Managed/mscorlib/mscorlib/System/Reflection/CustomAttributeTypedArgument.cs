using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Represents an argument of a custom attribute in the reflection-only context, or an element of an array argument.</summary>
	// Token: 0x02000314 RID: 788
	[ComVisible(true)]
	[Serializable]
	public struct CustomAttributeTypedArgument
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> class with the specified type and value.</summary>
		/// <param name="argumentType">The type of the custom attribute argument.</param>
		/// <param name="value">The value of the custom attribute argument.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="argumentType" /> is null.</exception>
		// Token: 0x06002272 RID: 8818 RVA: 0x00081598 File Offset: 0x0007F798
		public CustomAttributeTypedArgument(Type argumentType, object value)
		{
			if (argumentType == null)
			{
				throw new ArgumentNullException("argumentType");
			}
			this.argumentType = argumentType;
			this.value = value;
			if (value is Array)
			{
				Array array = (Array)value;
				Type elementType = array.GetType().GetElementType();
				CustomAttributeTypedArgument[] array2 = new CustomAttributeTypedArgument[array.GetLength(0)];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = new CustomAttributeTypedArgument(elementType, array.GetValue(i));
				}
				this.value = new ReadOnlyCollection<CustomAttributeTypedArgument>(array2);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> class with the specified value.</summary>
		/// <param name="value">The value of the custom attribute argument.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06002273 RID: 8819 RVA: 0x0008161D File Offset: 0x0007F81D
		public CustomAttributeTypedArgument(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.argumentType = value.GetType();
			this.value = value;
		}

		/// <summary>Gets the type of the argument or of the array argument element.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the type of the argument or of the array element.</returns>
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06002274 RID: 8820 RVA: 0x00081640 File Offset: 0x0007F840
		public Type ArgumentType
		{
			get
			{
				return this.argumentType;
			}
		}

		/// <summary>Gets the value of the argument for a simple argument or for an element of an array argument; gets a collection of values for an array argument.</summary>
		/// <returns>An object that represents the value of the argument or element, or a generic <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> objects that represent the values of an array-type argument.</returns>
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x00081648 File Offset: 0x0007F848
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Returns a string consisting of the argument name, the equal sign, and a string representation of the argument value.</summary>
		/// <returns>A string consisting of the argument name, the equal sign, and a string representation of the argument value.</returns>
		// Token: 0x06002276 RID: 8822 RVA: 0x00081650 File Offset: 0x0007F850
		public override string ToString()
		{
			string text = ((this.value != null) ? this.value.ToString() : string.Empty);
			if (this.argumentType == typeof(string))
			{
				return "\"" + text + "\"";
			}
			if (this.argumentType == typeof(Type))
			{
				return "typeof (" + text + ")";
			}
			if (this.argumentType.IsEnum)
			{
				return "(" + this.argumentType.Name + ")" + text;
			}
			return text;
		}

		/// <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false. </returns>
		/// <param name="obj">The object to compare with the current instance. </param>
		// Token: 0x06002277 RID: 8823 RVA: 0x000816F4 File Offset: 0x0007F8F4
		public override bool Equals(object obj)
		{
			if (!(obj is CustomAttributeTypedArgument))
			{
				return false;
			}
			CustomAttributeTypedArgument customAttributeTypedArgument = (CustomAttributeTypedArgument)obj;
			if (!(customAttributeTypedArgument.argumentType == this.argumentType) || this.value == null)
			{
				return customAttributeTypedArgument.value == null;
			}
			return this.value.Equals(customAttributeTypedArgument.value);
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x00081748 File Offset: 0x0007F948
		public override int GetHashCode()
		{
			return (this.argumentType.GetHashCode() << 16) + ((this.value != null) ? this.value.GetHashCode() : 0);
		}

		/// <summary>Tests whether two <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structures are equivalent.</summary>
		/// <returns>true if the two <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structures are equal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure to the right of the equality operator.</param>
		// Token: 0x06002279 RID: 8825 RVA: 0x0008176F File Offset: 0x0007F96F
		public static bool operator ==(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
		{
			return left.Equals(right);
		}

		/// <summary>Tests whether two <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structures are different.</summary>
		/// <returns>true if the two <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structures are different; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure to the left of the inequality operator.</param>
		/// <param name="right">The <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure to the right of the inequality operator.</param>
		// Token: 0x0600227A RID: 8826 RVA: 0x00081784 File Offset: 0x0007F984
		public static bool operator !=(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04001312 RID: 4882
		private Type argumentType;

		// Token: 0x04001313 RID: 4883
		private object value;
	}
}
