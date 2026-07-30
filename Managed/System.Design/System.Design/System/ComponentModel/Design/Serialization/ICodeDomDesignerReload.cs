using System;
using System.CodeDom;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides an interface that can be used to optimize the reloading of a designer.</summary>
	// Token: 0x02000157 RID: 343
	public interface ICodeDomDesignerReload
	{
		/// <summary>Indicates whether the designer should reload in order to import the specified compile unit correctly.</summary>
		/// <returns>true if the designer should reload; otherwise, false.</returns>
		/// <param name="newTree">A <see cref="T:System.CodeDom.CodeCompileUnit" /> containing the designer document code. </param>
		// Token: 0x06000A77 RID: 2679
		bool ShouldReloadDesigner(CodeCompileUnit newTree);
	}
}
