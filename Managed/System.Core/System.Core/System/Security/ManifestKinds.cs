using System;

namespace System.Security
{
	/// <summary>Represents the type of manifest that the signature information applies to.</summary>
	// Token: 0x0200035A RID: 858
	[Flags]
	public enum ManifestKinds
	{
		/// <summary>The manifest is for an application. </summary>
		// Token: 0x04000B99 RID: 2969
		Application = 2,
		/// <summary>The manifest is for deployment and application. The is the default value for verifying signatures. </summary>
		// Token: 0x04000B9A RID: 2970
		ApplicationAndDeployment = 3,
		/// <summary>The manifest is for deployment only.</summary>
		// Token: 0x04000B9B RID: 2971
		Deployment = 1,
		/// <summary>The manifest is of no particular type. </summary>
		// Token: 0x04000B9C RID: 2972
		None = 0
	}
}
