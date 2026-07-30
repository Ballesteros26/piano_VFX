using System;
using Unity;

namespace System.Web.Compilation
{
	/// <summary>Contains properties for a script block being parsed.</summary>
	// Token: 0x0200065B RID: 1627
	[Serializable]
	public sealed class LinePragmaCodeInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.LinePragmaCodeInfo" /> class. </summary>
		// Token: 0x060045B4 RID: 17844 RVA: 0x00002050 File Offset: 0x00000250
		public LinePragmaCodeInfo()
		{
		}

		/// <summary>Gets the length of the script block.</summary>
		/// <returns>The length of the script block.</returns>
		// Token: 0x170015C0 RID: 5568
		// (get) Token: 0x060045B5 RID: 17845 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public int CodeLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the script block is located inside &lt;% %&gt; tags.</summary>
		/// <returns>true if the script block is contained inside &lt;% %&gt; tags; otherwise, false.</returns>
		// Token: 0x170015C1 RID: 5569
		// (get) Token: 0x060045B6 RID: 17846 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public bool IsCodeNugget
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the starting column of a script block in an .aspx file.</summary>
		/// <returns>The starting column of a script block in an .aspx file.</returns>
		// Token: 0x170015C2 RID: 5570
		// (get) Token: 0x060045B7 RID: 17847 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public int StartColumn
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the starting column of a script block in the generated source file.</summary>
		/// <returns>The starting column of a script block in the generated source file.</returns>
		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x060045B8 RID: 17848 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public int StartGeneratedColumn
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the starting line of a script block in an .aspx file.</summary>
		/// <returns>The starting line of a script block in an .aspx file.</returns>
		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x060045B9 RID: 17849 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public int StartLine
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.LinePragmaCodeInfo" /> class with parameters for initializing the <see cref="P:System.Web.Compilation.LinePragmaCodeInfo.StartLine" />, <see cref="P:System.Web.Compilation.LinePragmaCodeInfo.StartColumn" />, <see cref="P:System.Web.Compilation.LinePragmaCodeInfo.StartGeneratedColumn" />, <see cref="P:System.Web.Compilation.LinePragmaCodeInfo.CodeLength" />, and <see cref="P:System.Web.Compilation.LinePragmaCodeInfo.IsCodeNugget" /> properties.</summary>
		/// <param name="startLine">The starting line of a script block in an .aspx file.</param>
		/// <param name="startColumn">The starting column of a script block in an .aspx file.</param>
		/// <param name="startGeneratedColumn">The starting column of a script block in the generated source file.</param>
		/// <param name="codeLength">The length of the script block.</param>
		/// <param name="isCodeNugget">A value indicating whether the script block is located inside &lt;% %&gt; tags.</param>
		// Token: 0x060045BA RID: 17850 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public LinePragmaCodeInfo(int startLine, int startColumn, int startGeneratedColumn, int codeLength, bool isCodeNugget)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
