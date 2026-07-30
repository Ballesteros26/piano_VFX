using System;
using System.Diagnostics;

namespace System.Runtime.Versioning
{
	/// <summary>Specifies the resource exposure for a member of a class. This class cannot be inherited.</summary>
	// Token: 0x020006C1 RID: 1729
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, Inherited = false)]
	[Conditional("RESOURCE_ANNOTATION_WORK")]
	public sealed class ResourceExposureAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.ResourceExposureAttribute" /> class with the specified exposure level.</summary>
		/// <param name="exposureLevel">The scope of the resource.</param>
		// Token: 0x06004984 RID: 18820 RVA: 0x00107CC3 File Offset: 0x00105EC3
		public ResourceExposureAttribute(ResourceScope exposureLevel)
		{
			this._resourceExposureLevel = exposureLevel;
		}

		/// <summary>Gets the resource exposure scope.</summary>
		/// <returns>A <see cref="T:System.Runtime.Versioning.ResourceScope" /> object.</returns>
		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06004985 RID: 18821 RVA: 0x00107CD2 File Offset: 0x00105ED2
		public ResourceScope ResourceExposureLevel
		{
			get
			{
				return this._resourceExposureLevel;
			}
		}

		// Token: 0x04002689 RID: 9865
		private ResourceScope _resourceExposureLevel;
	}
}
