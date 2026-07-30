using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>Represents a document referenced by a symbol store.</summary>
	// Token: 0x02000A71 RID: 2673
	[ComVisible(true)]
	public interface ISymbolDocument
	{
		/// <summary>Gets the checksum algorithm identifier.</summary>
		/// <returns>A GUID identifying the checksum algorithm. The value is all zeros, if there is no checksum.</returns>
		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x060061C3 RID: 25027
		Guid CheckSumAlgorithmId { get; }

		/// <summary>Gets the type of the current document.</summary>
		/// <returns>The type of the current document.</returns>
		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x060061C4 RID: 25028
		Guid DocumentType { get; }

		/// <summary>Checks whether the current document is stored in the symbol store.</summary>
		/// <returns>true if the current document is stored in the symbol store; otherwise, false.</returns>
		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x060061C5 RID: 25029
		bool HasEmbeddedSource { get; }

		/// <summary>Gets the language of the current document.</summary>
		/// <returns>The language of the current document.</returns>
		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x060061C6 RID: 25030
		Guid Language { get; }

		/// <summary>Gets the language vendor of the current document.</summary>
		/// <returns>The language vendor of the current document.</returns>
		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x060061C7 RID: 25031
		Guid LanguageVendor { get; }

		/// <summary>Gets the length, in bytes, of the embedded source.</summary>
		/// <returns>The source length of the current document.</returns>
		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x060061C8 RID: 25032
		int SourceLength { get; }

		/// <summary>Gets the URL of the current document.</summary>
		/// <returns>The URL of the current document.</returns>
		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x060061C9 RID: 25033
		string URL { get; }

		/// <summary>Returns the closest line that is a sequence point, given a line in the current document that might or might not be a sequence point.</summary>
		/// <returns>The closest line that is a sequence point.</returns>
		/// <param name="line">The specified line in the document. </param>
		// Token: 0x060061CA RID: 25034
		int FindClosestLine(int line);

		/// <summary>Gets the checksum.</summary>
		/// <returns>The checksum.</returns>
		// Token: 0x060061CB RID: 25035
		byte[] GetCheckSum();

		/// <summary>Gets the embedded document source for the specified range.</summary>
		/// <returns>The document source for the specified range.</returns>
		/// <param name="startLine">The starting line in the current document. </param>
		/// <param name="startColumn">The starting column in the current document. </param>
		/// <param name="endLine">The ending line in the current document. </param>
		/// <param name="endColumn">The ending column in the current document. </param>
		// Token: 0x060061CC RID: 25036
		byte[] GetSourceRange(int startLine, int startColumn, int endLine, int endColumn);
	}
}
