using System;

namespace System.Runtime.Versioning
{
	/// <summary>Describes the compatibility guarantee of a component, type, or type member that may span multiple versions.</summary>
	// Token: 0x020006BC RID: 1724
	[Flags]
	[Serializable]
	public enum ComponentGuaranteesOptions
	{
		/// <summary>The developer does not guarantee compatibility across versions. Consumers of the component, type, or member can expect future versions to break the existing client.</summary>
		// Token: 0x04002680 RID: 9856
		None = 0,
		/// <summary>The developer promises multi-version exchange compatibility for the type. Consumers of the type can expect compatibility across future versions and can use the type in all their interfaces. Versioning problems cannot be fixed by side-by-side execution. </summary>
		// Token: 0x04002681 RID: 9857
		Exchange = 1,
		/// <summary>The developer promises stable compatibility across versions. Consumers of the type can expect that future versions will not break the existing client. However, if they do and if the client has not used the type in its interfaces, side-by-side execution may fix the problem.</summary>
		// Token: 0x04002682 RID: 9858
		Stable = 2,
		/// <summary>The component has been tested to work when more than one version of the assembly is loaded into the same application domain. Future versions can break compatibility. However, when such breaking changes are made, the old version is not modified but continues to exist alongside the new version.</summary>
		// Token: 0x04002683 RID: 9859
		SideBySide = 4
	}
}
