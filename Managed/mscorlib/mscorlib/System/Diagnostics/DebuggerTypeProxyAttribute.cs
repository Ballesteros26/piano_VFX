using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Specifies the display proxy for a type.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000A68 RID: 2664
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	public sealed class DebuggerTypeProxyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerTypeProxyAttribute" /> class using the type of the proxy. </summary>
		/// <param name="type">The proxy type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x06006172 RID: 24946 RVA: 0x0013FFBD File Offset: 0x0013E1BD
		public DebuggerTypeProxyAttribute(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.typeName = type.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerTypeProxyAttribute" /> class using the type name of the proxy. </summary>
		/// <param name="typeName">The type name of the proxy type.</param>
		// Token: 0x06006173 RID: 24947 RVA: 0x0013FFE5 File Offset: 0x0013E1E5
		public DebuggerTypeProxyAttribute(string typeName)
		{
			this.typeName = typeName;
		}

		/// <summary>Gets the type name of the proxy type. </summary>
		/// <returns>The type name of the proxy type.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x06006174 RID: 24948 RVA: 0x0013FFF4 File Offset: 0x0013E1F4
		public string ProxyTypeName
		{
			get
			{
				return this.typeName;
			}
		}

		/// <summary>Gets or sets the target type for the attribute.</summary>
		/// <returns>The target type for the attribute.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.DebuggerTypeProxyAttribute.Target" /> is set to null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x06006176 RID: 24950 RVA: 0x00140025 File Offset: 0x0013E225
		// (set) Token: 0x06006175 RID: 24949 RVA: 0x0013FFFC File Offset: 0x0013E1FC
		public Type Target
		{
			get
			{
				return this.target;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.targetName = value.AssemblyQualifiedName;
				this.target = value;
			}
		}

		/// <summary>Gets or sets the name of the target type.</summary>
		/// <returns>The name of the target type.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x06006177 RID: 24951 RVA: 0x0014002D File Offset: 0x0013E22D
		// (set) Token: 0x06006178 RID: 24952 RVA: 0x00140035 File Offset: 0x0013E235
		public string TargetTypeName
		{
			get
			{
				return this.targetName;
			}
			set
			{
				this.targetName = value;
			}
		}

		// Token: 0x040030AF RID: 12463
		private string typeName;

		// Token: 0x040030B0 RID: 12464
		private string targetName;

		// Token: 0x040030B1 RID: 12465
		private Type target;
	}
}
