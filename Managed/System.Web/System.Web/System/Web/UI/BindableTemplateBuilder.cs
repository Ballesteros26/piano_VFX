using System;
using System.Collections.Specialized;
using Unity;

namespace System.Web.UI
{
	/// <summary>Supports page parsing of data-bound controls that automatically bind to an ASP.NET data source control within templated content sections. This class cannot be inherited.</summary>
	// Token: 0x02000787 RID: 1927
	public sealed class BindableTemplateBuilder : TemplateBuilder, IBindableTemplate
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.BindableTemplateBuilder" /> class.</summary>
		// Token: 0x06004E42 RID: 20034 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public BindableTemplateBuilder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Retrieves a set of name/value pairs for values that are bound using two-way ASP.NET data-binding syntax within the templated content at design-time and in no-compile pages.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of name/value pairs, where the name represents the data item field specified as the first parameter to bind within the templated content and the value is the current value of a property value bound using two-way ASP.NET data-binding syntax.</returns>
		/// <param name="container">The <see cref="T:System.Web.UI.Control" /> from which to extract name/value pairs, which are passed by the data-bound control to an associated data source control in two-way data-binding scenarios.</param>
		// Token: 0x06004E43 RID: 20035 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public IOrderedDictionary ExtractValues(Control container)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
