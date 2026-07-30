using System;
using System.Runtime.CompilerServices;

namespace System.Windows.Markup
{
	/// <summary>Identifies the <see cref="T:System.Windows.Markup.ValueSerializer" /> class that a type or property should use when it is serialized.</summary>
	// Token: 0x02000128 RID: 296
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
	[TypeForwardedFrom("WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public sealed class ValueSerializerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Markup.ValueSerializerAttribute" /> class, using the specified type.</summary>
		/// <param name="valueSerializerType">A type that represents the type of the <see cref="T:System.Windows.Markup.ValueSerializer" /> class.</param>
		// Token: 0x060007FD RID: 2045 RVA: 0x00027548 File Offset: 0x00025748
		public ValueSerializerAttribute(Type valueSerializerType)
		{
			this._valueSerializerType = valueSerializerType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Markup.ValueSerializerAttribute" /> class, using an assembly qualified type name string.</summary>
		/// <param name="valueSerializerTypeName">The assembly qualified type name string for the <see cref="T:System.Windows.Markup.ValueSerializer" /> class to use.</param>
		// Token: 0x060007FE RID: 2046 RVA: 0x00027557 File Offset: 0x00025757
		public ValueSerializerAttribute(string valueSerializerTypeName)
		{
			this._valueSerializerTypeName = valueSerializerTypeName;
		}

		/// <summary>Gets the type of the <see cref="T:System.Windows.Markup.ValueSerializer" /> class reported by this attribute.</summary>
		/// <returns>The type of the <see cref="T:System.Windows.Markup.ValueSerializer" />.</returns>
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x00027566 File Offset: 0x00025766
		public Type ValueSerializerType
		{
			get
			{
				if (this._valueSerializerType == null && this._valueSerializerTypeName != null)
				{
					this._valueSerializerType = Type.GetType(this._valueSerializerTypeName);
				}
				return this._valueSerializerType;
			}
		}

		/// <summary>Gets the assembly qualified name of the <see cref="T:System.Windows.Markup.ValueSerializer" /> type for this type or property.</summary>
		/// <returns>The assembly qualified name of the type.</returns>
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x00027595 File Offset: 0x00025795
		public string ValueSerializerTypeName
		{
			get
			{
				if (this._valueSerializerType != null)
				{
					return this._valueSerializerType.AssemblyQualifiedName;
				}
				return this._valueSerializerTypeName;
			}
		}

		// Token: 0x04000D86 RID: 3462
		private Type _valueSerializerType;

		// Token: 0x04000D87 RID: 3463
		private string _valueSerializerTypeName;
	}
}
