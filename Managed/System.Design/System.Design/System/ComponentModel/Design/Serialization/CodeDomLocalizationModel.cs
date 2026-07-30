using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Determines the localization model to be used by the CodeDom resource adapter.</summary>
	// Token: 0x02000148 RID: 328
	public enum CodeDomLocalizationModel
	{
		/// <summary>The localization provider should ignore localized properties. It will still write out resources for objects that do not support code generation and are serializable.</summary>
		// Token: 0x04000247 RID: 583
		None,
		/// <summary>The localization provider will write out localized properties by assigning a resource to each property. This model is fast when the number of properties is small, but it scales poorly as the number of properties containing default values grows.</summary>
		// Token: 0x04000248 RID: 584
		PropertyAssignment,
		/// <summary>The localization provider will write localized property values into a resource file and use the <see cref="T:System.ComponentModel.ComponentResourceManager" /> class to reflect on properties by name to fill them at runtime. This uses reflection at runtime so it can be slow, but it scales better for large numbers of properties with default values.</summary>
		// Token: 0x04000249 RID: 585
		PropertyReflection
	}
}
