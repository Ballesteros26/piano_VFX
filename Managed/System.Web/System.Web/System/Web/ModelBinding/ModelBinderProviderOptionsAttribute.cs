using System;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies options for a model-binder provider.</summary>
	// Token: 0x02000520 RID: 1312
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class ModelBinderProviderOptionsAttribute : Attribute
	{
		/// <summary>Gets or sets a value that specifies whether a model binder provider should appear at the beginning of the list of model-binder providers.</summary>
		/// <returns>true if the model binder provider should go at the beginning of the list; otherwise, false.</returns>
		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x060039EB RID: 14827 RVA: 0x0009CF68 File Offset: 0x0009B168
		// (set) Token: 0x060039EC RID: 14828 RVA: 0x0009CF70 File Offset: 0x0009B170
		public bool FrontOfList { get; set; }
	}
}
