using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection
{
	/// <summary>Provides access to custom attribute data for assemblies, modules, types, members and parameters that are loaded into the reflection-only context.</summary>
	// Token: 0x02000310 RID: 784
	[ComVisible(true)]
	[Serializable]
	public class CustomAttributeData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeData" /> class.</summary>
		// Token: 0x06002251 RID: 8785 RVA: 0x00002111 File Offset: 0x00000311
		protected CustomAttributeData()
		{
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x00081043 File Offset: 0x0007F243
		internal CustomAttributeData(ConstructorInfo ctorInfo, Assembly assembly, IntPtr data, uint data_length)
		{
			this.ctorInfo = ctorInfo;
			this.lazyData = new CustomAttributeData.LazyCAttrData();
			this.lazyData.assembly = assembly;
			this.lazyData.data = data;
			this.lazyData.data_length = data_length;
		}

		// Token: 0x06002253 RID: 8787
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResolveArgumentsInternal(ConstructorInfo ctor, Assembly assembly, IntPtr data, uint data_length, out object[] ctorArgs, out object[] namedArgs);

		// Token: 0x06002254 RID: 8788 RVA: 0x00081084 File Offset: 0x0007F284
		private void ResolveArguments()
		{
			if (this.lazyData == null)
			{
				return;
			}
			object[] array;
			object[] array2;
			CustomAttributeData.ResolveArgumentsInternal(this.ctorInfo, this.lazyData.assembly, this.lazyData.data, this.lazyData.data_length, out array, out array2);
			this.ctorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>((array != null) ? CustomAttributeData.UnboxValues<CustomAttributeTypedArgument>(array) : EmptyArray<CustomAttributeTypedArgument>.Value);
			this.namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>((array2 != null) ? CustomAttributeData.UnboxValues<CustomAttributeNamedArgument>(array2) : EmptyArray<CustomAttributeNamedArgument>.Value);
			this.lazyData = null;
		}

		/// <summary>Gets a <see cref="T:System.Reflection.ConstructorInfo" /> object that represents the constructor that would have initialized the custom attribute.</summary>
		/// <returns>An object that represents the constructor that would have initialized the custom attribute represented by the current instance of the <see cref="T:System.Reflection.CustomAttributeData" /> class.</returns>
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x00081107 File Offset: 0x0007F307
		[ComVisible(true)]
		public virtual ConstructorInfo Constructor
		{
			get
			{
				return this.ctorInfo;
			}
		}

		/// <summary>Gets the list of positional arguments specified for the attribute instance represented by the <see cref="T:System.Reflection.CustomAttributeData" /> object.</summary>
		/// <returns>A collection of structures that represent the positional arguments specified for the custom attribute instance.</returns>
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x0008110F File Offset: 0x0007F30F
		[ComVisible(true)]
		public virtual IList<CustomAttributeTypedArgument> ConstructorArguments
		{
			get
			{
				this.ResolveArguments();
				return this.ctorArgs;
			}
		}

		/// <summary>Gets the list of named arguments specified for the attribute instance represented by the <see cref="T:System.Reflection.CustomAttributeData" /> object.</summary>
		/// <returns>A collection of structures that represent the named arguments specified for the custom attribute instance.</returns>
		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06002257 RID: 8791 RVA: 0x0008111D File Offset: 0x0007F31D
		public virtual IList<CustomAttributeNamedArgument> NamedArguments
		{
			get
			{
				this.ResolveArguments();
				return this.namedArgs;
			}
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target assembly.</summary>
		/// <returns>A list of objects that represent data about the attributes that have been applied to the target assembly.</returns>
		/// <param name="target">The assembly whose custom attribute data is to be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is null.</exception>
		// Token: 0x06002258 RID: 8792 RVA: 0x0008112B File Offset: 0x0007F32B
		public static IList<CustomAttributeData> GetCustomAttributes(Assembly target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target);
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target member.</summary>
		/// <returns>A list of objects that represent data about the attributes that have been applied to the target member.</returns>
		/// <param name="target">The member whose attribute data is to be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is null.</exception>
		// Token: 0x06002259 RID: 8793 RVA: 0x0008112B File Offset: 0x0007F32B
		public static IList<CustomAttributeData> GetCustomAttributes(MemberInfo target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target);
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x0008112B File Offset: 0x0007F32B
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeType target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target);
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target module.</summary>
		/// <returns>A list of objects that represent data about the attributes that have been applied to the target module.</returns>
		/// <param name="target">The module whose custom attribute data is to be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is null.</exception>
		// Token: 0x0600225B RID: 8795 RVA: 0x0008112B File Offset: 0x0007F32B
		public static IList<CustomAttributeData> GetCustomAttributes(Module target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target);
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target parameter.</summary>
		/// <returns>A list of objects that represent data about the attributes that have been applied to the target parameter.</returns>
		/// <param name="target">The parameter whose attribute data is to be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is null.</exception>
		// Token: 0x0600225C RID: 8796 RVA: 0x0008112B File Offset: 0x0007F32B
		public static IList<CustomAttributeData> GetCustomAttributes(ParameterInfo target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target);
		}

		/// <summary>Gets the type of the attribute.</summary>
		/// <returns>The type of the attribute.</returns>
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600225D RID: 8797 RVA: 0x00081133 File Offset: 0x0007F333
		public Type AttributeType
		{
			get
			{
				return this.ctorInfo.DeclaringType;
			}
		}

		/// <summary>Returns a string representation of the custom attribute.</summary>
		/// <returns>A string value that represents the custom attribute.</returns>
		// Token: 0x0600225E RID: 8798 RVA: 0x00081140 File Offset: 0x0007F340
		public override string ToString()
		{
			this.ResolveArguments();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[" + this.ctorInfo.DeclaringType.FullName + "(");
			for (int i = 0; i < this.ctorArgs.Count; i++)
			{
				stringBuilder.Append(this.ctorArgs[i].ToString());
				if (i + 1 < this.ctorArgs.Count)
				{
					stringBuilder.Append(", ");
				}
			}
			if (this.namedArgs.Count > 0)
			{
				stringBuilder.Append(", ");
			}
			for (int j = 0; j < this.namedArgs.Count; j++)
			{
				stringBuilder.Append(this.namedArgs[j].ToString());
				if (j + 1 < this.namedArgs.Count)
				{
					stringBuilder.Append(", ");
				}
			}
			stringBuilder.AppendFormat(")]", Array.Empty<object>());
			return stringBuilder.ToString();
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x00081258 File Offset: 0x0007F458
		private static T[] UnboxValues<T>(object[] values)
		{
			T[] array = new T[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				array[i] = (T)((object)values[i]);
			}
			return array;
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to the current instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x06002260 RID: 8800 RVA: 0x0008128C File Offset: 0x0007F48C
		public override bool Equals(object obj)
		{
			CustomAttributeData customAttributeData = obj as CustomAttributeData;
			if (customAttributeData == null || customAttributeData.ctorInfo != this.ctorInfo || customAttributeData.ctorArgs.Count != this.ctorArgs.Count || customAttributeData.namedArgs.Count != this.namedArgs.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ctorArgs.Count; i++)
			{
				if (this.ctorArgs[i].Equals(customAttributeData.ctorArgs[i]))
				{
					return false;
				}
			}
			for (int j = 0; j < this.namedArgs.Count; j++)
			{
				bool flag = false;
				for (int k = 0; k < customAttributeData.namedArgs.Count; k++)
				{
					if (this.namedArgs[j].Equals(customAttributeData.namedArgs[k]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x0008139C File Offset: 0x0007F59C
		public override int GetHashCode()
		{
			int num = ((this.ctorInfo == null) ? 13 : (this.ctorInfo.GetHashCode() << 16));
			if (this.ctorArgs != null)
			{
				for (int i = 0; i < this.ctorArgs.Count; i++)
				{
					num += num ^ (7 + this.ctorArgs[i].GetHashCode() << i * 4);
				}
			}
			if (this.namedArgs != null)
			{
				for (int j = 0; j < this.namedArgs.Count; j++)
				{
					num += this.namedArgs[j].GetHashCode() << 5;
				}
			}
			return num;
		}

		// Token: 0x04001309 RID: 4873
		private ConstructorInfo ctorInfo;

		// Token: 0x0400130A RID: 4874
		private IList<CustomAttributeTypedArgument> ctorArgs;

		// Token: 0x0400130B RID: 4875
		private IList<CustomAttributeNamedArgument> namedArgs;

		// Token: 0x0400130C RID: 4876
		private CustomAttributeData.LazyCAttrData lazyData;

		// Token: 0x02000311 RID: 785
		private class LazyCAttrData
		{
			// Token: 0x0400130D RID: 4877
			internal Assembly assembly;

			// Token: 0x0400130E RID: 4878
			internal IntPtr data;

			// Token: 0x0400130F RID: 4879
			internal uint data_length;
		}
	}
}
