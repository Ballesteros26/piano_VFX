using System;

namespace System.CodeDom
{
	/// <summary>Represents a code checksum pragma code entity.  </summary>
	// Token: 0x0200075F RID: 1887
	[Serializable]
	public class CodeChecksumPragma : CodeDirective
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeChecksumPragma" /> class. </summary>
		// Token: 0x06003BF0 RID: 15344 RVA: 0x000D8A48 File Offset: 0x000D6C48
		public CodeChecksumPragma()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeChecksumPragma" /> class using a file name, a GUID representing the checksum algorithm, and a byte stream representing the checksum data.</summary>
		/// <param name="fileName">The path to the checksum file.</param>
		/// <param name="checksumAlgorithmId">A <see cref="T:System.Guid" /> that identifies the checksum algorithm to use.</param>
		/// <param name="checksumData">A byte array that contains the checksum data.</param>
		// Token: 0x06003BF1 RID: 15345 RVA: 0x000D8A50 File Offset: 0x000D6C50
		public CodeChecksumPragma(string fileName, Guid checksumAlgorithmId, byte[] checksumData)
		{
			this._fileName = fileName;
			this.ChecksumAlgorithmId = checksumAlgorithmId;
			this.ChecksumData = checksumData;
		}

		/// <summary>Gets or sets the path to the checksum file.</summary>
		/// <returns>The path to the checksum file.</returns>
		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06003BF2 RID: 15346 RVA: 0x000D8A6D File Offset: 0x000D6C6D
		// (set) Token: 0x06003BF3 RID: 15347 RVA: 0x000D8A7E File Offset: 0x000D6C7E
		public string FileName
		{
			get
			{
				return this._fileName ?? string.Empty;
			}
			set
			{
				this._fileName = value;
			}
		}

		/// <summary>Gets or sets a GUID that identifies the checksum algorithm to use.</summary>
		/// <returns>A <see cref="T:System.Guid" /> that identifies the checksum algorithm to use.</returns>
		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06003BF4 RID: 15348 RVA: 0x000D8A87 File Offset: 0x000D6C87
		// (set) Token: 0x06003BF5 RID: 15349 RVA: 0x000D8A8F File Offset: 0x000D6C8F
		public Guid ChecksumAlgorithmId { get; set; }

		/// <summary>Gets or sets the value of the data for the checksum calculation.</summary>
		/// <returns>A byte array that contains the data for the checksum calculation.</returns>
		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06003BF6 RID: 15350 RVA: 0x000D8A98 File Offset: 0x000D6C98
		// (set) Token: 0x06003BF7 RID: 15351 RVA: 0x000D8AA0 File Offset: 0x000D6CA0
		public byte[] ChecksumData { get; set; }

		// Token: 0x04002D79 RID: 11641
		private string _fileName;
	}
}
