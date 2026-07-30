using System;

namespace System.Web
{
	/// <summary>Represents a parser error or warning. This class cannot be inherited. </summary>
	// Token: 0x020000CA RID: 202
	[Serializable]
	public sealed class ParserError
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ParserError" /> class.</summary>
		// Token: 0x06000AF8 RID: 2808 RVA: 0x00002050 File Offset: 0x00000250
		public ParserError()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ParserError" /> class by using the specified error text, virtual path, and source line number.</summary>
		/// <param name="errorText">The error message text.</param>
		/// <param name="virtualPath">The virtual path of the file being parsed when the error occurred.</param>
		/// <param name="line">The line number of the error source.</param>
		// Token: 0x06000AF9 RID: 2809 RVA: 0x0001CF02 File Offset: 0x0001B102
		public ParserError(string errorText, string virtualPath, int line)
		{
			this._errorText = errorText;
			this._virtualPath = virtualPath;
			this._line = line;
		}

		/// <summary>Gets or sets a string that represents the error for the <see cref="T:System.Web.ParserError" /> object.</summary>
		/// <returns>A string containing the error message.</returns>
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0001CF1F File Offset: 0x0001B11F
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x0001CF27 File Offset: 0x0001B127
		public string ErrorText
		{
			get
			{
				return this._errorText;
			}
			set
			{
				this._errorText = value;
			}
		}

		/// <summary>Gets or set the virtual path of the file that was being parsed when the error occurred.</summary>
		/// <returns>A string that specifies the virtual path of the file that contains the parser error.</returns>
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0001CF30 File Offset: 0x0001B130
		// (set) Token: 0x06000AFD RID: 2813 RVA: 0x0001CF38 File Offset: 0x0001B138
		public string VirtualPath
		{
			get
			{
				return this._virtualPath;
			}
			set
			{
				this._virtualPath = value;
			}
		}

		/// <summary>Gets or sets the line number of the source at which the error occurs.</summary>
		/// <returns>The source line number where the parser encountered the error.</returns>
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x0001CF41 File Offset: 0x0001B141
		// (set) Token: 0x06000AFF RID: 2815 RVA: 0x0001CF49 File Offset: 0x0001B149
		public int Line
		{
			get
			{
				return this._line;
			}
			set
			{
				this._line = value;
			}
		}

		// Token: 0x04001072 RID: 4210
		private string _errorText;

		// Token: 0x04001073 RID: 4211
		private string _virtualPath;

		// Token: 0x04001074 RID: 4212
		private int _line;
	}
}
