using System;

namespace System.Web.Compilation
{
	/// <summary>Defines an attribute that specifies the scope where a build provider will be applied when a resource is located. This class cannot be inherited.</summary>
	// Token: 0x02000602 RID: 1538
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class BuildProviderAppliesToAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.BuildProviderAppliesToAttribute" /> class that applies to the specified resource location. </summary>
		/// <param name="appliesTo">One of the <see cref="T:System.Web.Compilation.BuildProviderAppliesTo" /> values.</param>
		// Token: 0x0600429A RID: 17050 RVA: 0x000AFA71 File Offset: 0x000ADC71
		public BuildProviderAppliesToAttribute(BuildProviderAppliesTo appliesTo)
		{
			this._appliesTo = appliesTo;
		}

		/// <summary>Gets a value that indicates where the specified <see cref="T:System.Web.Compilation.BuildProvider" /> class will be applied when a resource with the appropriate extension is found.</summary>
		/// <returns>A <see cref="T:System.Web.Compilation.BuildProviderAppliesTo" /> value that indicates where the specified <see cref="T:System.Web.Compilation.BuildProvider" /> class will be applied when a resource with the appropriate extension is found.</returns>
		// Token: 0x1700151D RID: 5405
		// (get) Token: 0x0600429B RID: 17051 RVA: 0x000AFA80 File Offset: 0x000ADC80
		public BuildProviderAppliesTo AppliesTo
		{
			get
			{
				return this._appliesTo;
			}
		}

		// Token: 0x040023AD RID: 9133
		private BuildProviderAppliesTo _appliesTo;
	}
}
