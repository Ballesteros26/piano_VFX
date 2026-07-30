using System;

namespace System.Xml.Serialization
{
	/// <summary>Specifies that the member (a field that returns an array of <see cref="T:System.Xml.XmlAttribute" /> objects) can contain any XML attributes.</summary>
	// Token: 0x02000323 RID: 803
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false)]
	public class XmlAnyAttributeAttribute : Attribute
	{
	}
}
