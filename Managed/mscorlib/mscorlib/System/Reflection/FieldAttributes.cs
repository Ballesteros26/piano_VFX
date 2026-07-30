using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies flags that describe the attributes of a field.</summary>
	// Token: 0x020002DD RID: 733
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum FieldAttributes
	{
		/// <summary>Specifies the access level of a given field.</summary>
		// Token: 0x040011A8 RID: 4520
		FieldAccessMask = 7,
		/// <summary>Specifies that the field cannot be referenced.</summary>
		// Token: 0x040011A9 RID: 4521
		PrivateScope = 0,
		/// <summary>Specifies that the field is accessible only by the parent type.</summary>
		// Token: 0x040011AA RID: 4522
		Private = 1,
		/// <summary>Specifies that the field is accessible only by subtypes in this assembly.</summary>
		// Token: 0x040011AB RID: 4523
		FamANDAssem = 2,
		/// <summary>Specifies that the field is accessible throughout the assembly.</summary>
		// Token: 0x040011AC RID: 4524
		Assembly = 3,
		/// <summary>Specifies that the field is accessible only by type and subtypes.</summary>
		// Token: 0x040011AD RID: 4525
		Family = 4,
		/// <summary>Specifies that the field is accessible by subtypes anywhere, as well as throughout this assembly.</summary>
		// Token: 0x040011AE RID: 4526
		FamORAssem = 5,
		/// <summary>Specifies that the field is accessible by any member for whom this scope is visible.</summary>
		// Token: 0x040011AF RID: 4527
		Public = 6,
		/// <summary>Specifies that the field represents the defined type, or else it is per-instance.</summary>
		// Token: 0x040011B0 RID: 4528
		Static = 16,
		/// <summary>Specifies that the field is initialized only, and can be set only in the body of a constructor. </summary>
		// Token: 0x040011B1 RID: 4529
		InitOnly = 32,
		/// <summary>Specifies that the field's value is a compile-time (static or early bound) constant. Any attempt to set it throws a <see cref="T:System.FieldAccessException" />.</summary>
		// Token: 0x040011B2 RID: 4530
		Literal = 64,
		/// <summary>Specifies that the field does not have to be serialized when the type is remoted.</summary>
		// Token: 0x040011B3 RID: 4531
		NotSerialized = 128,
		/// <summary>Specifies a special method, with the name describing how the method is special.</summary>
		// Token: 0x040011B4 RID: 4532
		SpecialName = 512,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x040011B5 RID: 4533
		PinvokeImpl = 8192,
		/// <summary>Reserved.</summary>
		// Token: 0x040011B6 RID: 4534
		ReservedMask = 38144,
		/// <summary>Specifies that the common language runtime (metadata internal APIs) should check the name encoding.</summary>
		// Token: 0x040011B7 RID: 4535
		RTSpecialName = 1024,
		/// <summary>Specifies that the field has marshaling information.</summary>
		// Token: 0x040011B8 RID: 4536
		HasFieldMarshal = 4096,
		/// <summary>Specifies that the field has a default value.</summary>
		// Token: 0x040011B9 RID: 4537
		HasDefault = 32768,
		/// <summary>Specifies that the field has a relative virtual address (RVA). The RVA is the location of the method body in the current image, as an address relative to the start of the image file in which it is located.</summary>
		// Token: 0x040011BA RID: 4538
		HasFieldRVA = 256
	}
}
