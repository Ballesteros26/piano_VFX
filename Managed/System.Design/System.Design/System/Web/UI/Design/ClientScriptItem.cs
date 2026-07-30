using System;

namespace System.Web.UI.Design
{
	/// <summary>Represents a client script element in a Web Form or user control at design time. This class cannot be inherited.</summary>
	// Token: 0x02000052 RID: 82
	public sealed class ClientScriptItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.ClientScriptItem" /> class using the provided parameters.</summary>
		/// <param name="text">The contents for the script element; a string of script statements to run on the client.</param>
		/// <param name="source">The src attribute value for the script element, specifying an external source location for the client script contents.</param>
		/// <param name="language">The language attribute value for the script element, specifying the language of the script statements.</param>
		/// <param name="type">The type attribute value for the script element, indicating the MIME type for the associated scripting engine.</param>
		/// <param name="id">The ID for the script element. This argument is required by the design host (for example, Visual Studio 2005).</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="id" /> is null (thrown by the design host).</exception>
		// Token: 0x060002A2 RID: 674 RVA: 0x00008DAD File Offset: 0x00006FAD
		public ClientScriptItem(string text, string source, string language, string type, string id)
		{
			this.text = text;
			this.source = source;
			this.language = language;
			this.type = type;
			this.id = id;
		}

		/// <summary>Gets the ID attribute value for the client script element.</summary>
		/// <returns>The ID value of the script element.</returns>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00008DDA File Offset: 0x00006FDA
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		/// <summary>Gets the language attribute value for the client script element.</summary>
		/// <returns>The language name specified for the language attribute in the script element.</returns>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00008DE2 File Offset: 0x00006FE2
		public string Language
		{
			get
			{
				return this.language;
			}
		}

		/// <summary>Gets the src attribute value for the client script element.</summary>
		/// <returns>The path to the source file specified for the src attribute in the script element.</returns>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00008DEA File Offset: 0x00006FEA
		public string Source
		{
			get
			{
				return this.source;
			}
		}

		/// <summary>Gets the script statements contained in the client script element.</summary>
		/// <returns>The script statements contained in the script element.</returns>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00008DF2 File Offset: 0x00006FF2
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		/// <summary>Gets the type attribute value for the client script element.</summary>
		/// <returns>The name of the MIME type associated with the script element.</returns>
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00008DFA File Offset: 0x00006FFA
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000116 RID: 278
		private string text;

		// Token: 0x04000117 RID: 279
		private string source;

		// Token: 0x04000118 RID: 280
		private string language;

		// Token: 0x04000119 RID: 281
		private string type;

		// Token: 0x0400011A RID: 282
		private string id;
	}
}
