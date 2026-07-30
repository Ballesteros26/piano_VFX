using System;

namespace System.Web.UI
{
	/// <summary>Specifies how an ASP.NET server control property or event is persisted declaratively in an .aspx or .ascx file.</summary>
	// Token: 0x02000192 RID: 402
	public enum PersistenceMode
	{
		/// <summary>Specifies that the property or event persists as an attribute.</summary>
		// Token: 0x04001327 RID: 4903
		Attribute,
		/// <summary>Specifies that the property persists in the ASP.NET server control as a nested tag. This is commonly used for complex objects, those that have persistable properties of their own.</summary>
		// Token: 0x04001328 RID: 4904
		InnerProperty,
		/// <summary>Specifies that the property persists in the ASP.NET server control as inner text. Also indicates that this property is defined as the element's default property. Only one property can be designated the default property.</summary>
		// Token: 0x04001329 RID: 4905
		InnerDefaultProperty,
		/// <summary>Specifies that the property persists as the only inner text of the ASP.NET server control. The property value is HTML encoded. Only a string can be given this designation.</summary>
		// Token: 0x0400132A RID: 4906
		EncodedInnerDefaultProperty
	}
}
