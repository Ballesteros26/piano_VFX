using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Represents a named argument of a custom attribute in the reflection-only context.</summary>
	// Token: 0x02000313 RID: 787
	[ComVisible(true)]
	[Serializable]
	public struct CustomAttributeNamedArgument
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> class, which represents the specified field or property of the custom attribute, and specifies the value of the field or property.</summary>
		/// <param name="memberInfo">A field or property of the custom attribute. The new <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> object represents this member and its value.</param>
		/// <param name="value">The value of the field or property of the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="memberInfo" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="memberInfo" /> is not a field or property of the custom attribute.</exception>
		// Token: 0x06002267 RID: 8807 RVA: 0x0008147C File Offset: 0x0007F67C
		public CustomAttributeNamedArgument(MemberInfo memberInfo, object value)
		{
			this.memberInfo = memberInfo;
			this.typedArgument = (CustomAttributeTypedArgument)value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> class, which represents the specified field or property of the custom attribute, and specifies a <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> object that describes the type and value of the field or property.</summary>
		/// <param name="memberInfo">A field or property of the custom attribute. The new <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> object represents this member and its value.</param>
		/// <param name="typedArgument">An object that describes the type and value of the field or property.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="memberInfo" /> is null.</exception>
		// Token: 0x06002268 RID: 8808 RVA: 0x00081491 File Offset: 0x0007F691
		public CustomAttributeNamedArgument(MemberInfo memberInfo, CustomAttributeTypedArgument typedArgument)
		{
			this.memberInfo = memberInfo;
			this.typedArgument = typedArgument;
		}

		/// <summary>Gets the attribute member that would be used to set the named argument.</summary>
		/// <returns>The attribute member that would be used to set the named argument.</returns>
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06002269 RID: 8809 RVA: 0x000814A1 File Offset: 0x0007F6A1
		public MemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
		}

		/// <summary>Gets a <see cref="T:System.Reflection.CustomAttributeTypedArgument" /> structure that can be used to obtain the type and value of the current named argument.</summary>
		/// <returns>A structure that can be used to obtain the type and value of the current named argument.</returns>
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x000814A9 File Offset: 0x0007F6A9
		public CustomAttributeTypedArgument TypedValue
		{
			get
			{
				return this.typedArgument;
			}
		}

		/// <summary>Gets a value that indicates whether the named argument is a field.</summary>
		/// <returns>true if the named argument is a field; otherwise, false.</returns>
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x0600226B RID: 8811 RVA: 0x000814B1 File Offset: 0x0007F6B1
		public bool IsField
		{
			get
			{
				return this.memberInfo.MemberType == MemberTypes.Field;
			}
		}

		/// <summary>Gets the name of the attribute member that would be used to set the named argument.</summary>
		/// <returns>The name of the attribute member that would be used to set the named argument.</returns>
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x000814C1 File Offset: 0x0007F6C1
		public string MemberName
		{
			get
			{
				return this.memberInfo.Name;
			}
		}

		/// <summary>Returns a string that consists of the argument name, the equal sign, and a string representation of the argument value.</summary>
		/// <returns>A string that consists of the argument name, the equal sign, and a string representation of the argument value.</returns>
		// Token: 0x0600226D RID: 8813 RVA: 0x000814CE File Offset: 0x0007F6CE
		public override string ToString()
		{
			return this.memberInfo.Name + " = " + this.typedArgument.ToString();
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x0600226E RID: 8814 RVA: 0x000814F8 File Offset: 0x0007F6F8
		public override bool Equals(object obj)
		{
			if (!(obj is CustomAttributeNamedArgument))
			{
				return false;
			}
			CustomAttributeNamedArgument customAttributeNamedArgument = (CustomAttributeNamedArgument)obj;
			return customAttributeNamedArgument.memberInfo == this.memberInfo && this.typedArgument.Equals(customAttributeNamedArgument.typedArgument);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600226F RID: 8815 RVA: 0x00081547 File Offset: 0x0007F747
		public override int GetHashCode()
		{
			return (this.memberInfo.GetHashCode() << 16) + this.typedArgument.GetHashCode();
		}

		/// <summary>Tests whether two <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> structures are equivalent.</summary>
		/// <returns>true if the two <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> structures are equal; otherwise, false.</returns>
		/// <param name="left">The structure to the left of the equality operator.</param>
		/// <param name="right">The structure to the right of the equality operator.</param>
		// Token: 0x06002270 RID: 8816 RVA: 0x00081569 File Offset: 0x0007F769
		public static bool operator ==(CustomAttributeNamedArgument left, CustomAttributeNamedArgument right)
		{
			return left.Equals(right);
		}

		/// <summary>Tests whether two <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> structures are different.</summary>
		/// <returns>true if the two <see cref="T:System.Reflection.CustomAttributeNamedArgument" /> structures are different; otherwise, false.</returns>
		/// <param name="left">The structure to the left of the inequality operator.</param>
		/// <param name="right">The structure to the right of the inequality operator.</param>
		// Token: 0x06002271 RID: 8817 RVA: 0x0008157E File Offset: 0x0007F77E
		public static bool operator !=(CustomAttributeNamedArgument left, CustomAttributeNamedArgument right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04001310 RID: 4880
		private CustomAttributeTypedArgument typedArgument;

		// Token: 0x04001311 RID: 4881
		private MemberInfo memberInfo;
	}
}
