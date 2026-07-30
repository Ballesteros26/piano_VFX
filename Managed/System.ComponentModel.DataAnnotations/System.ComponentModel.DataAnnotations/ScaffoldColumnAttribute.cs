using System;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies whether a class or data column uses scaffolding.</summary>
	// Token: 0x0200002C RID: 44
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class ScaffoldColumnAttribute : Attribute
	{
		/// <summary>Gets or sets the value that specifies whether scaffolding is enabled.</summary>
		/// <returns>true, if scaffolding is enabled; otherwise false.</returns>
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000422F File Offset: 0x0000242F
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00004237 File Offset: 0x00002437
		public bool Scaffold { get; private set; }

		/// <summary>Initializes a new instance of <see cref="T:System.ComponentModel.DataAnnotations.ScaffoldColumnAttribute" /> using the <see cref="P:System.ComponentModel.DataAnnotations.ScaffoldColumnAttribute.Scaffold" /> property.</summary>
		/// <param name="scaffold">The value that specifies whether scaffolding is enabled.</param>
		// Token: 0x06000101 RID: 257 RVA: 0x00004240 File Offset: 0x00002440
		public ScaffoldColumnAttribute(bool scaffold)
		{
			this.Scaffold = scaffold;
		}
	}
}
