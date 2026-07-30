using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies type attributes.</summary>
	// Token: 0x02000303 RID: 771
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum TypeAttributes
	{
		/// <summary>Specifies type visibility information.</summary>
		// Token: 0x040012AF RID: 4783
		VisibilityMask = 7,
		/// <summary>Specifies that the class is not public.</summary>
		// Token: 0x040012B0 RID: 4784
		NotPublic = 0,
		/// <summary>Specifies that the class is public.</summary>
		// Token: 0x040012B1 RID: 4785
		Public = 1,
		/// <summary>Specifies that the class is nested with public visibility.</summary>
		// Token: 0x040012B2 RID: 4786
		NestedPublic = 2,
		/// <summary>Specifies that the class is nested with private visibility.</summary>
		// Token: 0x040012B3 RID: 4787
		NestedPrivate = 3,
		/// <summary>Specifies that the class is nested with family visibility, and is thus accessible only by methods within its own type and any derived types.</summary>
		// Token: 0x040012B4 RID: 4788
		NestedFamily = 4,
		/// <summary>Specifies that the class is nested with assembly visibility, and is thus accessible only by methods within its assembly.</summary>
		// Token: 0x040012B5 RID: 4789
		NestedAssembly = 5,
		/// <summary>Specifies that the class is nested with assembly and family visibility, and is thus accessible only by methods lying in the intersection of its family and assembly.</summary>
		// Token: 0x040012B6 RID: 4790
		NestedFamANDAssem = 6,
		/// <summary>Specifies that the class is nested with family or assembly visibility, and is thus accessible only by methods lying in the union of its family and assembly.</summary>
		// Token: 0x040012B7 RID: 4791
		NestedFamORAssem = 7,
		/// <summary>Specifies class layout information.</summary>
		// Token: 0x040012B8 RID: 4792
		LayoutMask = 24,
		/// <summary>Specifies that class fields are automatically laid out by the common language runtime.</summary>
		// Token: 0x040012B9 RID: 4793
		AutoLayout = 0,
		/// <summary>Specifies that class fields are laid out sequentially, in the order that the fields were emitted to the metadata.</summary>
		// Token: 0x040012BA RID: 4794
		SequentialLayout = 8,
		/// <summary>Specifies that class fields are laid out at the specified offsets.</summary>
		// Token: 0x040012BB RID: 4795
		ExplicitLayout = 16,
		/// <summary>Specifies class semantics information; the current class is contextful (else agile).</summary>
		// Token: 0x040012BC RID: 4796
		ClassSemanticsMask = 32,
		/// <summary>Specifies that the type is a class.</summary>
		// Token: 0x040012BD RID: 4797
		Class = 0,
		/// <summary>Specifies that the type is an interface.</summary>
		// Token: 0x040012BE RID: 4798
		Interface = 32,
		/// <summary>Specifies that the type is abstract.</summary>
		// Token: 0x040012BF RID: 4799
		Abstract = 128,
		/// <summary>Specifies that the class is concrete and cannot be extended.</summary>
		// Token: 0x040012C0 RID: 4800
		Sealed = 256,
		/// <summary>Specifies that the class is special in a way denoted by the name.</summary>
		// Token: 0x040012C1 RID: 4801
		SpecialName = 1024,
		/// <summary>Specifies that the class or interface is imported from another module.</summary>
		// Token: 0x040012C2 RID: 4802
		Import = 4096,
		/// <summary>Specifies that the class can be serialized.</summary>
		// Token: 0x040012C3 RID: 4803
		Serializable = 8192,
		/// <summary>Specifies a Windows Runtime type.</summary>
		// Token: 0x040012C4 RID: 4804
		[ComVisible(false)]
		WindowsRuntime = 16384,
		/// <summary>Used to retrieve string information for native interoperability.</summary>
		// Token: 0x040012C5 RID: 4805
		StringFormatMask = 196608,
		/// <summary>LPTSTR is interpreted as ANSI.</summary>
		// Token: 0x040012C6 RID: 4806
		AnsiClass = 0,
		/// <summary>LPTSTR is interpreted as UNICODE.</summary>
		// Token: 0x040012C7 RID: 4807
		UnicodeClass = 65536,
		/// <summary>LPTSTR is interpreted automatically.</summary>
		// Token: 0x040012C8 RID: 4808
		AutoClass = 131072,
		/// <summary>LPSTR is interpreted by some implementation-specific means, which includes the possibility of throwing a <see cref="T:System.NotSupportedException" />. Not used in the Microsoft implementation of the .NET Framework.</summary>
		// Token: 0x040012C9 RID: 4809
		CustomFormatClass = 196608,
		/// <summary>Used to retrieve non-standard encoding information for native interop. The meaning of the values of these 2 bits is unspecified. Not used in the Microsoft implementation of the .NET Framework.</summary>
		// Token: 0x040012CA RID: 4810
		CustomFormatMask = 12582912,
		/// <summary>Specifies that calling static methods of the type does not force the system to initialize the type.</summary>
		// Token: 0x040012CB RID: 4811
		BeforeFieldInit = 1048576,
		/// <summary>Attributes reserved for runtime use.</summary>
		// Token: 0x040012CC RID: 4812
		ReservedMask = 264192,
		/// <summary>Runtime should check name encoding.</summary>
		// Token: 0x040012CD RID: 4813
		RTSpecialName = 2048,
		/// <summary>Type has security associate with it.</summary>
		// Token: 0x040012CE RID: 4814
		HasSecurity = 262144
	}
}
