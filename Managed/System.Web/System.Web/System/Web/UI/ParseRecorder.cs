using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity;

namespace System.Web.UI
{
	/// <summary>Abstract base class for objects that need to be notified of parse events during page parsing.</summary>
	// Token: 0x0200078B RID: 1931
	public abstract class ParseRecorder
	{
		/// <summary>Iinitializes a new instance of the <see cref="T:System.Web.UI.ParseRecorder" /> class.</summary>
		// Token: 0x06004E47 RID: 20039 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected ParseRecorder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a collection of parse recorder factories.</summary>
		/// <returns>The parse recorder factories. If the property is null, an empty collection is created and returned.</returns>
		// Token: 0x170017CE RID: 6094
		// (get) Token: 0x06004E48 RID: 20040 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public static IList<Func<ParseRecorder>> RecorderFactories
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>When implemented in a derived class, initializes the listener.</summary>
		/// <param name="parser">The template parser.</param>
		// Token: 0x06004E49 RID: 20041 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void Initialize(TemplateParser parser)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Called when the template parser is finished parsing the file.</summary>
		/// <param name="root">The control builder root.</param>
		// Token: 0x06004E4A RID: 20042 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void ParseComplete(ControlBuilder root)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>When implemented in a derived class, enables the parse recorder to access the generated CodeDom and insert and modify code.</summary>
		/// <param name="builder">The control builder.</param>
		/// <param name="codeCompileUnit">The code compile unit.</param>
		/// <param name="baseType">The code type declaration base type.</param>
		/// <param name="derivedType">The code type declaration derived type.</param>
		/// <param name="buildMethod">The build method.</param>
		/// <param name="dataBindingMethod">The data binding method.</param>
		// Token: 0x06004E4B RID: 20043 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void ProcessGeneratedCode(ControlBuilder builder, CodeCompileUnit codeCompileUnit, CodeTypeDeclaration baseType, CodeTypeDeclaration derivedType, CodeMemberMethod buildMethod, CodeMemberMethod dataBindingMethod)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Called when the template parser encounters a web control start tag.</summary>
		/// <param name="builder">The control builder.</param>
		/// <param name="tag">The tag.</param>
		// Token: 0x06004E4C RID: 20044 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void RecordBeginTag(ControlBuilder builder, Match tag)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Called when the template parser encounters a code block.</summary>
		/// <param name="builder">The control builder.</param>
		/// <param name="codeBlock">The code block.</param>
		// Token: 0x06004E4D RID: 20045 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void RecordCodeBlock(ControlBuilder builder, Match codeBlock)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Called when the template parser encounters a web control empty tag.</summary>
		/// <param name="builder">The control builder.</param>
		/// <param name="tag">The tag.</param>
		// Token: 0x06004E4E RID: 20046 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void RecordEmptyTag(ControlBuilder builder, Match tag)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Called when the template parser encounters a web control end tag.</summary>
		/// <param name="builder">The control builder.</param>
		/// <param name="tag">The tag.</param>
		// Token: 0x06004E4F RID: 20047 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void RecordEndTag(ControlBuilder builder, Match tag)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
