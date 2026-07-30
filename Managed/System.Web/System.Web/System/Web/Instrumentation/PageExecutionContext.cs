using System;
using System.IO;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.Instrumentation
{
	/// <summary>Provides information about the current position in the page execution cycle.</summary>
	// Token: 0x020006A0 RID: 1696
	public class PageExecutionContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Instrumentation.PageExecutionContext" /> class.</summary>
		// Token: 0x060047C2 RID: 18370 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public PageExecutionContext()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a flag that indicates whether the block contains literal content that should be deeply scanned for selection mapping purposes.</summary>
		/// <returns>true if the block contains literal content that should be deeply scanned for selection mapping purposes; otherwise, false;</returns>
		// Token: 0x17001619 RID: 5657
		// (get) Token: 0x060047C3 RID: 18371 RVA: 0x000C9DB4 File Offset: 0x000C7FB4
		// (set) Token: 0x060047C4 RID: 18372 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool IsLiteral
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the length, in characters, of the block.</summary>
		/// <returns>The length, in characters, of the block.</returns>
		// Token: 0x1700161A RID: 5658
		// (get) Token: 0x060047C5 RID: 18373 RVA: 0x000C9DD0 File Offset: 0x000C7FD0
		// (set) Token: 0x060047C6 RID: 18374 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public int Length
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the zero-based start position of the block from the start of the rendered document.</summary>
		/// <returns>The zero-based start position of the block from the start of the rendered document</returns>
		// Token: 0x1700161B RID: 5659
		// (get) Token: 0x060047C7 RID: 18375 RVA: 0x000C9DEC File Offset: 0x000C7FEC
		// (set) Token: 0x060047C8 RID: 18376 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public int StartPosition
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text writer that is used to render the document. </summary>
		/// <returns>The text writer that is used to render the document.</returns>
		// Token: 0x1700161C RID: 5660
		// (get) Token: 0x060047C9 RID: 18377 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x060047CA RID: 18378 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public TextWriter TextWriter
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the virtual path of the source file.</summary>
		/// <returns>The virtual path of the source file.</returns>
		// Token: 0x1700161D RID: 5661
		// (get) Token: 0x060047CB RID: 18379 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x060047CC RID: 18380 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string VirtualPath
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
