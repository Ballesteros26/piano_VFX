using System;

namespace System.CodeDom
{
	/// <summary>Specifies the name and mode for a code region.</summary>
	// Token: 0x02000787 RID: 1927
	[Serializable]
	public class CodeRegionDirective : CodeDirective
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRegionDirective" /> class with default values. </summary>
		// Token: 0x06003D1F RID: 15647 RVA: 0x000D8A48 File Offset: 0x000D6C48
		public CodeRegionDirective()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRegionDirective" /> class, specifying its mode and name. </summary>
		/// <param name="regionMode">One of the <see cref="T:System.CodeDom.CodeRegionMode" /> values.</param>
		/// <param name="regionText">The name for the region.</param>
		// Token: 0x06003D20 RID: 15648 RVA: 0x000DA01D File Offset: 0x000D821D
		public CodeRegionDirective(CodeRegionMode regionMode, string regionText)
		{
			this.RegionText = regionText;
			this.RegionMode = regionMode;
		}

		/// <summary>Gets or sets the name of the region.</summary>
		/// <returns>The name of the region.</returns>
		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06003D21 RID: 15649 RVA: 0x000DA033 File Offset: 0x000D8233
		// (set) Token: 0x06003D22 RID: 15650 RVA: 0x000DA044 File Offset: 0x000D8244
		public string RegionText
		{
			get
			{
				return this._regionText ?? string.Empty;
			}
			set
			{
				this._regionText = value;
			}
		}

		/// <summary>Gets or sets the mode for the region directive.</summary>
		/// <returns>One of the <see cref="T:System.CodeDom.CodeRegionMode" /> values. The default is <see cref="F:System.CodeDom.CodeRegionMode.None" />.</returns>
		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06003D23 RID: 15651 RVA: 0x000DA04D File Offset: 0x000D824D
		// (set) Token: 0x06003D24 RID: 15652 RVA: 0x000DA055 File Offset: 0x000D8255
		public CodeRegionMode RegionMode { get; set; }

		// Token: 0x04002DD7 RID: 11735
		private string _regionText;
	}
}
