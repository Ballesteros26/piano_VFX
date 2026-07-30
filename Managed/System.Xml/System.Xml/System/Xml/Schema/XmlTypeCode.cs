using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the W3C XML Schema Definition Language (XSD) schema types.</summary>
	// Token: 0x02000491 RID: 1169
	public enum XmlTypeCode
	{
		/// <summary>No type information.</summary>
		// Token: 0x04001E8D RID: 7821
		None,
		/// <summary>An item such as a node or atomic value.</summary>
		// Token: 0x04001E8E RID: 7822
		Item,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04001E8F RID: 7823
		Node,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04001E90 RID: 7824
		Document,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04001E91 RID: 7825
		Element,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04001E92 RID: 7826
		Attribute,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04001E93 RID: 7827
		Namespace,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04001E94 RID: 7828
		ProcessingInstruction,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04001E95 RID: 7829
		Comment,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04001E96 RID: 7830
		Text,
		/// <summary>Any atomic value of a union.</summary>
		// Token: 0x04001E97 RID: 7831
		AnyAtomicType,
		/// <summary>An untyped atomic value.</summary>
		// Token: 0x04001E98 RID: 7832
		UntypedAtomic,
		/// <summary>A W3C XML Schema xs:string type.</summary>
		// Token: 0x04001E99 RID: 7833
		String,
		/// <summary>A W3C XML Schema xs:boolean type.</summary>
		// Token: 0x04001E9A RID: 7834
		Boolean,
		/// <summary>A W3C XML Schema xs:decimal type.</summary>
		// Token: 0x04001E9B RID: 7835
		Decimal,
		/// <summary>A W3C XML Schema xs:float type.</summary>
		// Token: 0x04001E9C RID: 7836
		Float,
		/// <summary>A W3C XML Schema xs:double type.</summary>
		// Token: 0x04001E9D RID: 7837
		Double,
		/// <summary>A W3C XML Schema xs:Duration type.</summary>
		// Token: 0x04001E9E RID: 7838
		Duration,
		/// <summary>A W3C XML Schema xs:dateTime type.</summary>
		// Token: 0x04001E9F RID: 7839
		DateTime,
		/// <summary>A W3C XML Schema xs:time type.</summary>
		// Token: 0x04001EA0 RID: 7840
		Time,
		/// <summary>A W3C XML Schema xs:date type.</summary>
		// Token: 0x04001EA1 RID: 7841
		Date,
		/// <summary>A W3C XML Schema xs:gYearMonth type.</summary>
		// Token: 0x04001EA2 RID: 7842
		GYearMonth,
		/// <summary>A W3C XML Schema xs:gYear type.</summary>
		// Token: 0x04001EA3 RID: 7843
		GYear,
		/// <summary>A W3C XML Schema xs:gMonthDay type.</summary>
		// Token: 0x04001EA4 RID: 7844
		GMonthDay,
		/// <summary>A W3C XML Schema xs:gDay type.</summary>
		// Token: 0x04001EA5 RID: 7845
		GDay,
		/// <summary>A W3C XML Schema xs:gMonth type.</summary>
		// Token: 0x04001EA6 RID: 7846
		GMonth,
		/// <summary>A W3C XML Schema xs:hexBinary type.</summary>
		// Token: 0x04001EA7 RID: 7847
		HexBinary,
		/// <summary>A W3C XML Schema xs:base64Binary type.</summary>
		// Token: 0x04001EA8 RID: 7848
		Base64Binary,
		/// <summary>A W3C XML Schema xs:anyURI type.</summary>
		// Token: 0x04001EA9 RID: 7849
		AnyUri,
		/// <summary>A W3C XML Schema xs:QName type.</summary>
		// Token: 0x04001EAA RID: 7850
		QName,
		/// <summary>A W3C XML Schema xs:NOTATION type.</summary>
		// Token: 0x04001EAB RID: 7851
		Notation,
		/// <summary>A W3C XML Schema xs:normalizedString type.</summary>
		// Token: 0x04001EAC RID: 7852
		NormalizedString,
		/// <summary>A W3C XML Schema xs:token type.</summary>
		// Token: 0x04001EAD RID: 7853
		Token,
		/// <summary>A W3C XML Schema xs:language type.</summary>
		// Token: 0x04001EAE RID: 7854
		Language,
		/// <summary>A W3C XML Schema xs:NMTOKEN type.</summary>
		// Token: 0x04001EAF RID: 7855
		NmToken,
		/// <summary>A W3C XML Schema xs:Name type.</summary>
		// Token: 0x04001EB0 RID: 7856
		Name,
		/// <summary>A W3C XML Schema xs:NCName type.</summary>
		// Token: 0x04001EB1 RID: 7857
		NCName,
		/// <summary>A W3C XML Schema xs:ID type.</summary>
		// Token: 0x04001EB2 RID: 7858
		Id,
		/// <summary>A W3C XML Schema xs:IDREF type.</summary>
		// Token: 0x04001EB3 RID: 7859
		Idref,
		/// <summary>A W3C XML Schema xs:ENTITY type.</summary>
		// Token: 0x04001EB4 RID: 7860
		Entity,
		/// <summary>A W3C XML Schema xs:integer type.</summary>
		// Token: 0x04001EB5 RID: 7861
		Integer,
		/// <summary>A W3C XML Schema xs:nonPositiveInteger type.</summary>
		// Token: 0x04001EB6 RID: 7862
		NonPositiveInteger,
		/// <summary>A W3C XML Schema xs:negativeInteger type.</summary>
		// Token: 0x04001EB7 RID: 7863
		NegativeInteger,
		/// <summary>A W3C XML Schema xs:long type.</summary>
		// Token: 0x04001EB8 RID: 7864
		Long,
		/// <summary>A W3C XML Schema xs:int type.</summary>
		// Token: 0x04001EB9 RID: 7865
		Int,
		/// <summary>A W3C XML Schema xs:short type.</summary>
		// Token: 0x04001EBA RID: 7866
		Short,
		/// <summary>A W3C XML Schema xs:byte type.</summary>
		// Token: 0x04001EBB RID: 7867
		Byte,
		/// <summary>A W3C XML Schema xs:nonNegativeInteger type.</summary>
		// Token: 0x04001EBC RID: 7868
		NonNegativeInteger,
		/// <summary>A W3C XML Schema xs:unsignedLong type.</summary>
		// Token: 0x04001EBD RID: 7869
		UnsignedLong,
		/// <summary>A W3C XML Schema xs:unsignedInt type.</summary>
		// Token: 0x04001EBE RID: 7870
		UnsignedInt,
		/// <summary>A W3C XML Schema xs:unsignedShort type.</summary>
		// Token: 0x04001EBF RID: 7871
		UnsignedShort,
		/// <summary>A W3C XML Schema xs:unsignedByte type.</summary>
		// Token: 0x04001EC0 RID: 7872
		UnsignedByte,
		/// <summary>A W3C XML Schema xs:positiveInteger type.</summary>
		// Token: 0x04001EC1 RID: 7873
		PositiveInteger,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04001EC2 RID: 7874
		YearMonthDuration,
		/// <summary>This value supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x04001EC3 RID: 7875
		DayTimeDuration
	}
}
