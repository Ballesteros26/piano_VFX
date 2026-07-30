using System;

namespace System.Runtime.Versioning
{
	/// <summary>Defines the compatibility guarantee of a component, type, or type member that may span multiple versions.</summary>
	// Token: 0x020006BD RID: 1725
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class ComponentGuaranteesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.ComponentGuaranteesAttribute" /> class with a value that indicates a library, type, or member's guaranteed level of compatibility across multiple versions.</summary>
		/// <param name="guarantees">One of the enumeration values that specifies the level of compatibility that is guaranteed across multiple versions.</param>
		// Token: 0x06004977 RID: 18807 RVA: 0x00107BA9 File Offset: 0x00105DA9
		public ComponentGuaranteesAttribute(ComponentGuaranteesOptions guarantees)
		{
			this._guarantees = guarantees;
		}

		/// <summary>Gets a value that indicates the guaranteed level of compatibility of a library, type, or type member that spans multiple versions.</summary>
		/// <returns>One of the enumeration values that specifies the level of compatibility that is guaranteed across multiple versions.</returns>
		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06004978 RID: 18808 RVA: 0x00107BB8 File Offset: 0x00105DB8
		public ComponentGuaranteesOptions Guarantees
		{
			get
			{
				return this._guarantees;
			}
		}

		// Token: 0x04002684 RID: 9860
		private ComponentGuaranteesOptions _guarantees;
	}
}
