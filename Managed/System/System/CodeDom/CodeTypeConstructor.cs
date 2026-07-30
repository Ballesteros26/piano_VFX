using System;

namespace System.CodeDom
{
	/// <summary>Represents a static constructor for a class.</summary>
	// Token: 0x02000793 RID: 1939
	[Serializable]
	public class CodeTypeConstructor : CodeMemberMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeConstructor" /> class.</summary>
		// Token: 0x06003D5C RID: 15708 RVA: 0x000DA3A4 File Offset: 0x000D85A4
		public CodeTypeConstructor()
		{
			base.Name = ".cctor";
		}
	}
}
