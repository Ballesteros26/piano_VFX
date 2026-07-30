using System;
using System.Collections;
using System.Reflection;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides the information necessary to create an instance of an object. This class cannot be inherited.</summary>
	// Token: 0x02000354 RID: 852
	public sealed class InstanceDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.InstanceDescriptor" /> class using the specified member information and arguments.</summary>
		/// <param name="member">The member information for the descriptor. This can be a <see cref="T:System.Reflection.MethodInfo" />, <see cref="T:System.Reflection.ConstructorInfo" />, <see cref="T:System.Reflection.FieldInfo" />, or <see cref="T:System.Reflection.PropertyInfo" />. If this is a <see cref="T:System.Reflection.MethodInfo" />, <see cref="T:System.Reflection.FieldInfo" />, or <see cref="T:System.Reflection.PropertyInfo" />, it must represent a static member. </param>
		/// <param name="arguments">The collection of arguments to pass to the member. This parameter can be null or an empty collection if there are no arguments. The collection can also consist of other instances of <see cref="T:System.ComponentModel.Design.Serialization.InstanceDescriptor" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="member" /> is of type <see cref="T:System.Reflection.MethodInfo" />, <see cref="T:System.Reflection.FieldInfo" />, or <see cref="T:System.Reflection.PropertyInfo" />, and it does not represent a static member.<paramref name="member" /> is of type <see cref="T:System.Reflection.PropertyInfo" /> and is not readable.<paramref name="member" /> is of type <see cref="T:System.Reflection.MethodInfo" /> or <see cref="T:System.Reflection.ConstructorInfo" />, and the number of arguments in <paramref name="arguments" /> does not match the signature of <paramref name="member." /><paramref name="member" /> is of type <see cref="T:System.Reflection.ConstructorInfo" /> and represents a static member.<paramref name="member" /> is of type <see cref="T:System.Reflection.FieldInfo" />, and the number of arguments in <paramref name="arguments" /> is not zero. </exception>
		// Token: 0x06001A7B RID: 6779 RVA: 0x0006B0C3 File Offset: 0x000692C3
		public InstanceDescriptor(MemberInfo member, ICollection arguments)
			: this(member, arguments, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.InstanceDescriptor" /> class using the specified member information, arguments, and value indicating whether the specified information completely describes the instance.</summary>
		/// <param name="member">The member information for the descriptor. This can be a <see cref="T:System.Reflection.MethodInfo" />, <see cref="T:System.Reflection.ConstructorInfo" />, <see cref="T:System.Reflection.FieldInfo" />, or <see cref="T:System.Reflection.PropertyInfo" />. If this is a <see cref="T:System.Reflection.MethodInfo" />, <see cref="T:System.Reflection.FieldInfo" />, or <see cref="T:System.Reflection.PropertyInfo" />, it must represent a static member. </param>
		/// <param name="arguments">The collection of arguments to pass to the member. This parameter can be null or an empty collection if there are no arguments. The collection can also consist of other instances of <see cref="T:System.ComponentModel.Design.Serialization.InstanceDescriptor" />. </param>
		/// <param name="isComplete">true if the specified information completely describes the instance; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="member" /> is of type <see cref="T:System.Reflection.MethodInfo" />, <see cref="T:System.Reflection.FieldInfo" />, or <see cref="T:System.Reflection.PropertyInfo" />, and it does not represent a static member<paramref name="member" /> is of type <see cref="T:System.Reflection.PropertyInfo" /> and is not readable.<paramref name="member" /> is of type <see cref="T:System.Reflection.MethodInfo" /> or <see cref="T:System.Reflection.ConstructorInfo" /> and the number of arguments in <paramref name="arguments" /> does not match the signature of <paramref name="member" />.<paramref name="member" /> is of type <see cref="T:System.Reflection.ConstructorInfo" /> and represents a static member<paramref name="member" /> is of type <see cref="T:System.Reflection.FieldInfo" />, and the number of arguments in <paramref name="arguments" /> is not zero.</exception>
		// Token: 0x06001A7C RID: 6780 RVA: 0x0006B0D0 File Offset: 0x000692D0
		public InstanceDescriptor(MemberInfo member, ICollection arguments, bool isComplete)
		{
			this.member = member;
			this.isComplete = isComplete;
			if (arguments == null)
			{
				this.arguments = new object[0];
			}
			else
			{
				object[] array = new object[arguments.Count];
				arguments.CopyTo(array, 0);
				this.arguments = array;
			}
			if (member is FieldInfo)
			{
				if (!((FieldInfo)member).IsStatic)
				{
					throw new ArgumentException(global::SR.GetString("Parameter must be static."));
				}
				if (this.arguments.Count != 0)
				{
					throw new ArgumentException(global::SR.GetString("Length mismatch."));
				}
			}
			else if (member is ConstructorInfo)
			{
				ConstructorInfo constructorInfo = (ConstructorInfo)member;
				if (constructorInfo.IsStatic)
				{
					throw new ArgumentException(global::SR.GetString("Parameter cannot be static."));
				}
				if (this.arguments.Count != constructorInfo.GetParameters().Length)
				{
					throw new ArgumentException(global::SR.GetString("Length mismatch."));
				}
			}
			else if (member is MethodInfo)
			{
				MethodInfo methodInfo = (MethodInfo)member;
				if (!methodInfo.IsStatic)
				{
					throw new ArgumentException(global::SR.GetString("Parameter must be static."));
				}
				if (this.arguments.Count != methodInfo.GetParameters().Length)
				{
					throw new ArgumentException(global::SR.GetString("Length mismatch."));
				}
			}
			else if (member is PropertyInfo)
			{
				PropertyInfo propertyInfo = (PropertyInfo)member;
				if (!propertyInfo.CanRead)
				{
					throw new ArgumentException(global::SR.GetString("Parameter must be readable."));
				}
				MethodInfo getMethod = propertyInfo.GetGetMethod();
				if (getMethod != null && !getMethod.IsStatic)
				{
					throw new ArgumentException(global::SR.GetString("Parameter must be static."));
				}
			}
		}

		/// <summary>Gets the collection of arguments that can be used to reconstruct an instance of the object that this instance descriptor represents.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of arguments that can be used to create the object.</returns>
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x0006B24A File Offset: 0x0006944A
		public ICollection Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		/// <summary>Gets a value indicating whether the contents of this <see cref="T:System.ComponentModel.Design.Serialization.InstanceDescriptor" /> completely identify the instance.</summary>
		/// <returns>true if the instance is completely described; otherwise, false.</returns>
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x0006B252 File Offset: 0x00069452
		public bool IsComplete
		{
			get
			{
				return this.isComplete;
			}
		}

		/// <summary>Gets the member information that describes the instance this descriptor is associated with.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberInfo" /> that describes the instance that this object is associated with.</returns>
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x0006B25A File Offset: 0x0006945A
		public MemberInfo MemberInfo
		{
			get
			{
				return this.member;
			}
		}

		/// <summary>Invokes this instance descriptor and returns the object the descriptor describes.</summary>
		/// <returns>The object this instance descriptor describes.</returns>
		// Token: 0x06001A80 RID: 6784 RVA: 0x0006B264 File Offset: 0x00069464
		public object Invoke()
		{
			object[] array = new object[this.arguments.Count];
			this.arguments.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] is InstanceDescriptor)
				{
					array[i] = ((InstanceDescriptor)array[i]).Invoke();
				}
			}
			if (this.member is ConstructorInfo)
			{
				return ((ConstructorInfo)this.member).Invoke(array);
			}
			if (this.member is MethodInfo)
			{
				return ((MethodInfo)this.member).Invoke(null, array);
			}
			if (this.member is PropertyInfo)
			{
				return ((PropertyInfo)this.member).GetValue(null, array);
			}
			if (this.member is FieldInfo)
			{
				return ((FieldInfo)this.member).GetValue(null);
			}
			return null;
		}

		// Token: 0x04001839 RID: 6201
		private MemberInfo member;

		// Token: 0x0400183A RID: 6202
		private ICollection arguments;

		// Token: 0x0400183B RID: 6203
		private bool isComplete;
	}
}
