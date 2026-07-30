using System;
using Unity;

namespace System.ComponentModel.Design
{
	/// <summary>Specifies the target framework for a project.</summary>
	// Token: 0x020001D4 RID: 468
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	public sealed class ProjectTargetFrameworkAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ProjectTargetFrameworkAttribute" /> class. </summary>
		/// <param name="targetFrameworkMoniker">The target framework for the project.</param>
		// Token: 0x06000BF1 RID: 3057 RVA: 0x00002432 File Offset: 0x00000632
		public ProjectTargetFrameworkAttribute(string targetFrameworkMoniker)
		{
		}

		/// <summary>Gets the target framework for the project.</summary>
		/// <returns>The target framework for the project.</returns>
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0000970B File Offset: 0x0000790B
		public string TargetFrameworkMoniker
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
