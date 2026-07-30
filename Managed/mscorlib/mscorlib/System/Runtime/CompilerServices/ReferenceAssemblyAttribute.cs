using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Identifies an assembly as a reference assembly, which contains metadata but no executable code.</summary>
	// Token: 0x0200084F RID: 2127
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	[Serializable]
	public sealed class ReferenceAssemblyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.ReferenceAssemblyAttribute" /> class. </summary>
		// Token: 0x060053FC RID: 21500 RVA: 0x00002180 File Offset: 0x00000380
		public ReferenceAssemblyAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.ReferenceAssemblyAttribute" /> class by using the specified description. </summary>
		/// <param name="description">The description of the reference assembly. </param>
		// Token: 0x060053FD RID: 21501 RVA: 0x00126E0C File Offset: 0x0012500C
		public ReferenceAssemblyAttribute(string description)
		{
			this._description = description;
		}

		/// <summary>Gets the description of the reference assembly.</summary>
		/// <returns>The description of the reference assembly.</returns>
		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x060053FE RID: 21502 RVA: 0x00126E1B File Offset: 0x0012501B
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x04002BA0 RID: 11168
		private string _description;
	}
}
