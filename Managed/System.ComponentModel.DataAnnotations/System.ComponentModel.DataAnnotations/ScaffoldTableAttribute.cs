using System;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies whether a class or data table uses scaffolding.</summary>
	// Token: 0x0200002D RID: 45
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class ScaffoldTableAttribute : Attribute
	{
		/// <summary>Gets or sets the value that specifies whether scaffolding is enabled.</summary>
		/// <returns>true, if scaffolding is enabled; otherwise false.</returns>
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000424F File Offset: 0x0000244F
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00004257 File Offset: 0x00002457
		public bool Scaffold { get; private set; }

		/// <summary>Initializes a new instance of <see cref="T:System.ComponentModel.DataAnnotations.ScaffoldTableAttribute" /> using the <see cref="P:System.ComponentModel.DataAnnotations.ScaffoldTableAttribute.Scaffold" /> property.</summary>
		/// <param name="scaffold">The value that specifies whether scaffolding is enabled.</param>
		// Token: 0x06000104 RID: 260 RVA: 0x00004260 File Offset: 0x00002460
		public ScaffoldTableAttribute(bool scaffold)
		{
			this.Scaffold = scaffold;
		}
	}
}
