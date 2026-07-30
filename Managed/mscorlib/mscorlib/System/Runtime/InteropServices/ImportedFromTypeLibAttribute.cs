using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that the types defined within an assembly were originally defined in a type library.</summary>
	// Token: 0x020008B2 RID: 2226
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class ImportedFromTypeLibAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ImportedFromTypeLibAttribute" /> class with the name of the original type library file.</summary>
		/// <param name="tlbFile">The location of the original type library file. </param>
		// Token: 0x060054EF RID: 21743 RVA: 0x00128410 File Offset: 0x00126610
		public ImportedFromTypeLibAttribute(string tlbFile)
		{
			this._val = tlbFile;
		}

		/// <summary>Gets the name of the original type library file.</summary>
		/// <returns>The name of the original type library file.</returns>
		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x060054F0 RID: 21744 RVA: 0x0012841F File Offset: 0x0012661F
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002C06 RID: 11270
		internal string _val;
	}
}
