using System;
using System.Collections.Generic;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that the use of <see cref="T:System.Object" /> on a member is meant to be treated as a dynamically dispatched type.</summary>
	// Token: 0x02000303 RID: 771
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public sealed class DynamicAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.DynamicAttribute" /> class.</summary>
		// Token: 0x06001770 RID: 6000 RVA: 0x0004CDD8 File Offset: 0x0004AFD8
		public DynamicAttribute()
		{
			this._transformFlags = new bool[] { true };
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.DynamicAttribute" /> class.</summary>
		/// <param name="transformFlags">Specifies, in a prefix traversal of a type's construction, which <see cref="T:System.Object" /> occurrences are meant to be treated as a dynamically dispatched type.</param>
		// Token: 0x06001771 RID: 6001 RVA: 0x0004CDF0 File Offset: 0x0004AFF0
		public DynamicAttribute(bool[] transformFlags)
		{
			if (transformFlags == null)
			{
				throw new ArgumentNullException("transformFlags");
			}
			this._transformFlags = transformFlags;
		}

		/// <summary>Specifies, in a prefix traversal of a type's construction, which <see cref="T:System.Object" /> occurrences are meant to be treated as a dynamically dispatched type.</summary>
		/// <returns>The list of <see cref="T:System.Object" /> occurrences that are meant to be treated as a dynamically dispatched type.</returns>
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x0004CE0D File Offset: 0x0004B00D
		public IList<bool> TransformFlags
		{
			get
			{
				return Array.AsReadOnly<bool>(this._transformFlags);
			}
		}

		// Token: 0x04000AD0 RID: 2768
		private readonly bool[] _transformFlags;
	}
}
