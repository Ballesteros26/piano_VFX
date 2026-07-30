using System;
using System.Dynamic.Utils;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Stores information necessary to emit debugging symbol information for a source file, in particular the file name and unique language identifier.</summary>
	// Token: 0x020002B5 RID: 693
	public class SymbolDocumentInfo
	{
		// Token: 0x060014A7 RID: 5287 RVA: 0x0003D858 File Offset: 0x0003BA58
		internal SymbolDocumentInfo(string fileName)
		{
			ContractUtils.RequiresNotNull(fileName, "fileName");
			this.FileName = fileName;
		}

		/// <summary>The source file name.</summary>
		/// <returns>The string representing the source file name.</returns>
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x0003D872 File Offset: 0x0003BA72
		public string FileName { get; }

		/// <summary>Returns the language's unique identifier, if any.</summary>
		/// <returns>The language's unique identifier</returns>
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x0003D87A File Offset: 0x0003BA7A
		public virtual Guid Language
		{
			get
			{
				return Guid.Empty;
			}
		}

		/// <summary>Returns the language vendor's unique identifier, if any.</summary>
		/// <returns>The language vendor's unique identifier.</returns>
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x0003D87A File Offset: 0x0003BA7A
		public virtual Guid LanguageVendor
		{
			get
			{
				return Guid.Empty;
			}
		}

		/// <summary>Returns the document type's unique identifier, if any. Defaults to the GUID for a text file.</summary>
		/// <returns>The document type's unique identifier.</returns>
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x0003D881 File Offset: 0x0003BA81
		public virtual Guid DocumentType
		{
			get
			{
				return SymbolDocumentInfo.DocumentType_Text;
			}
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0000220F File Offset: 0x0000040F
		internal SymbolDocumentInfo()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040009CC RID: 2508
		internal static readonly Guid DocumentType_Text = new Guid(1518771467, 26129, 4563, 189, 42, 0, 0, 248, 8, 73, 189);
	}
}
