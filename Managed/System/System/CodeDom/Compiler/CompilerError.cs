using System;
using System.Globalization;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents a compiler error or warning.</summary>
	// Token: 0x020007AA RID: 1962
	[Serializable]
	public class CompilerError
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerError" /> class.</summary>
		// Token: 0x06003F1B RID: 16155 RVA: 0x000DF420 File Offset: 0x000DD620
		public CompilerError()
			: this(string.Empty, 0, 0, string.Empty, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerError" /> class using the specified file name, line, column, error number, and error text.</summary>
		/// <param name="fileName">The file name of the file that the compiler was compiling when it encountered the error. </param>
		/// <param name="line">The line of the source of the error. </param>
		/// <param name="column">The column of the source of the error. </param>
		/// <param name="errorNumber">The error number of the error. </param>
		/// <param name="errorText">The error message text. </param>
		// Token: 0x06003F1C RID: 16156 RVA: 0x000DF439 File Offset: 0x000DD639
		public CompilerError(string fileName, int line, int column, string errorNumber, string errorText)
		{
			this.Line = line;
			this.Column = column;
			this.ErrorNumber = errorNumber;
			this.ErrorText = errorText;
			this.FileName = fileName;
		}

		/// <summary>Gets or sets the line number where the source of the error occurs.</summary>
		/// <returns>The line number of the source file where the compiler encountered the error.</returns>
		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06003F1D RID: 16157 RVA: 0x000DF466 File Offset: 0x000DD666
		// (set) Token: 0x06003F1E RID: 16158 RVA: 0x000DF46E File Offset: 0x000DD66E
		public int Line { get; set; }

		/// <summary>Gets or sets the column number where the source of the error occurs.</summary>
		/// <returns>The column number of the source file where the compiler encountered the error.</returns>
		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06003F1F RID: 16159 RVA: 0x000DF477 File Offset: 0x000DD677
		// (set) Token: 0x06003F20 RID: 16160 RVA: 0x000DF47F File Offset: 0x000DD67F
		public int Column { get; set; }

		/// <summary>Gets or sets the error number.</summary>
		/// <returns>The error number as a string.</returns>
		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06003F21 RID: 16161 RVA: 0x000DF488 File Offset: 0x000DD688
		// (set) Token: 0x06003F22 RID: 16162 RVA: 0x000DF490 File Offset: 0x000DD690
		public string ErrorNumber { get; set; }

		/// <summary>Gets or sets the text of the error message.</summary>
		/// <returns>The text of the error message.</returns>
		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06003F23 RID: 16163 RVA: 0x000DF499 File Offset: 0x000DD699
		// (set) Token: 0x06003F24 RID: 16164 RVA: 0x000DF4A1 File Offset: 0x000DD6A1
		public string ErrorText { get; set; }

		/// <summary>Gets or sets a value that indicates whether the error is a warning.</summary>
		/// <returns>true if the error is a warning; otherwise, false.</returns>
		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06003F25 RID: 16165 RVA: 0x000DF4AA File Offset: 0x000DD6AA
		// (set) Token: 0x06003F26 RID: 16166 RVA: 0x000DF4B2 File Offset: 0x000DD6B2
		public bool IsWarning { get; set; }

		/// <summary>Gets or sets the file name of the source file that contains the code which caused the error.</summary>
		/// <returns>The file name of the source file that contains the code which caused the error.</returns>
		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06003F27 RID: 16167 RVA: 0x000DF4BB File Offset: 0x000DD6BB
		// (set) Token: 0x06003F28 RID: 16168 RVA: 0x000DF4C3 File Offset: 0x000DD6C3
		public string FileName { get; set; }

		/// <summary>Provides an implementation of Object's <see cref="M:System.Object.ToString" /> method.</summary>
		/// <returns>A string representation of the compiler error.</returns>
		// Token: 0x06003F29 RID: 16169 RVA: 0x000DF4CC File Offset: 0x000DD6CC
		public override string ToString()
		{
			if (this.FileName.Length <= 0)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0} {1}: {2}", this.WarningString, this.ErrorNumber, this.ErrorText);
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}({1},{2}) : {3} {4}: {5}", new object[] { this.FileName, this.Line, this.Column, this.WarningString, this.ErrorNumber, this.ErrorText });
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06003F2A RID: 16170 RVA: 0x000DF55E File Offset: 0x000DD75E
		private string WarningString
		{
			get
			{
				if (!this.IsWarning)
				{
					return "error";
				}
				return "warning";
			}
		}
	}
}
