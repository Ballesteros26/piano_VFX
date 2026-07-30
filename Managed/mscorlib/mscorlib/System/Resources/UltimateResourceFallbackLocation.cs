using System;
using System.Runtime.InteropServices;

namespace System.Resources
{
	/// <summary>Specifies whether a <see cref="T:System.Resources.ResourceManager" /> object looks for the resources of the app's default culture in the main assembly or in a satellite assembly. </summary>
	// Token: 0x020002B3 RID: 691
	[ComVisible(true)]
	[Serializable]
	public enum UltimateResourceFallbackLocation
	{
		/// <summary>Fallback resources are located in the main assembly.</summary>
		// Token: 0x04001128 RID: 4392
		MainAssembly,
		/// <summary>Fallback resources are located in a satellite assembly. </summary>
		// Token: 0x04001129 RID: 4393
		Satellite
	}
}
