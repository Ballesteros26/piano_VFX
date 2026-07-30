using System;
using System.Collections;
using System.ComponentModel.Design;

namespace System.Web.UI
{
	/// <summary>Provides a class that encapsulates theme and style sheet information for controls in a designer environment. </summary>
	// Token: 0x0200023B RID: 571
	public sealed class ThemeProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ThemeProvider" /> class. </summary>
		/// <param name="host">An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the current designer environment.</param>
		/// <param name="name">The name of the theme or style sheet theme that the <see cref="T:System.Web.UI.ThemeProvider" /> represents. This parameter can be null.</param>
		/// <param name="themeDefinition">Theme information passed to the <see cref="M:System.Web.UI.DesignTimeTemplateParser.ParseTheme(System.ComponentModel.Design.IDesignerHost,System.String,System.String)" /> method. These are the raw, concatenated contents of the theme.</param>
		/// <param name="cssFiles">The names of the cascading style sheet (CSS) files that are included with the theme that the <see cref="T:System.Web.UI.ThemeProvider" /> represents.</param>
		/// <param name="themePath">The absolute path of the file that contains the theme and control skin information for the named theme.</param>
		// Token: 0x060017A5 RID: 6053 RVA: 0x00040659 File Offset: 0x0003E859
		public ThemeProvider(IDesignerHost host, string name, string themeDefinition, string[] cssFiles, string themePath)
		{
			this.host = host;
			this.name = name;
			this.cssFiles = cssFiles;
		}

		/// <summary>Retrieves a <see cref="T:System.Web.UI.SkinBuilder" /> instance for the specified control, which is used to apply a theme and control skin in a designer environment.</summary>
		/// <returns>A <see cref="T:System.Web.UI.SkinBuilder" /> instance, if one is defined for the specified control type; otherwise, null.</returns>
		/// <param name="control">The control to apply a theme and control skin to.</param>
		// Token: 0x060017A6 RID: 6054 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public SkinBuilder GetSkinBuilder(Control control)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> object that contains a set of <see cref="T:System.Web.UI.SkinBuilder" /> objects for the specified <see cref="T:System.Type" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> that contains a list of one or more <see cref="T:System.Web.UI.ControlBuilder" /> objects associated with the <see cref="T:System.Type" /> passed to the method. This list is populated with <see cref="T:System.Collections.DictionaryEntry" /> objects where the <see cref="P:System.Web.UI.Control.SkinID" /> is a <see cref="P:System.Collections.DictionaryEntry.Key" />, and its associated <see cref="T:System.Web.UI.SkinBuilder" /> is the <see cref="P:System.Collections.DictionaryEntry.Value" />. </returns>
		/// <param name="type">A <see cref="T:System.Type" /> for which to retrieve any associated <see cref="T:System.Web.UI.ControlBuilder" /> objects.</param>
		// Token: 0x060017A7 RID: 6055 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public IDictionary GetSkinControlBuildersForControlType(Type type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> of skin identifiers that are associated with the specified <see cref="T:System.Type" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of skin identifiers, if control skins are defined for the specified <see cref="T:System.Type" />. If no control skins are associated with the type, an empty <see cref="T:System.Collections.ICollection" /> is returned. </returns>
		/// <param name="type">A <see cref="T:System.Type" /> for which to retrieve any associated skin names.</param>
		// Token: 0x060017A8 RID: 6056 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public ICollection GetSkinsForControl(Type type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the hash of the theme definition passed to the constructor.</summary>
		/// <returns>A hash code for the string passed as the theme definition to the class constructor.</returns>
		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x00003A1F File Offset: 0x00001C1F
		[global::System.MonoTODO("Not implemented")]
		public int ContentHashCode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a set of strings that represent the names of any cascading style sheet (CSS) files associated with the current theme, if the theme is a style sheet theme.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of strings that identify the CSS files associated with the theme or style sheet theme. This property might return null. </returns>
		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x00040677 File Offset: 0x0003E877
		public ICollection CssFiles
		{
			get
			{
				return this.cssFiles;
			}
		}

		/// <summary>Gets an <see cref="T:System.ComponentModel.Design.IDesignerHost" /> object that represents the current designer environment.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the current designer environment. </returns>
		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x0004067F File Offset: 0x0003E87F
		public IDesignerHost DesignerHost
		{
			get
			{
				return this.host;
			}
		}

		/// <summary>Gets the name of the theme or style sheet theme that the <see cref="T:System.Web.UI.ThemeProvider" /> instance represents.</summary>
		/// <returns>The name of the theme or style sheet theme that the <see cref="T:System.Web.UI.ThemeProvider" /> instance represents. </returns>
		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x00040687 File Offset: 0x0003E887
		public string ThemeName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040015EA RID: 5610
		private IDesignerHost host;

		// Token: 0x040015EB RID: 5611
		private string name;

		// Token: 0x040015EC RID: 5612
		private string[] cssFiles;
	}
}
