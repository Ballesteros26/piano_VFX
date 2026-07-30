using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the application elements on which it is valid to apply an attribute.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200012F RID: 303
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum AttributeTargets
	{
		/// <summary>Attribute can be applied to an assembly.</summary>
		// Token: 0x040007A7 RID: 1959
		Assembly = 1,
		/// <summary>Attribute can be applied to a module.</summary>
		// Token: 0x040007A8 RID: 1960
		Module = 2,
		/// <summary>Attribute can be applied to a class.</summary>
		// Token: 0x040007A9 RID: 1961
		Class = 4,
		/// <summary>Attribute can be applied to a structure; that is, a value type.</summary>
		// Token: 0x040007AA RID: 1962
		Struct = 8,
		/// <summary>Attribute can be applied to an enumeration.</summary>
		// Token: 0x040007AB RID: 1963
		Enum = 16,
		/// <summary>Attribute can be applied to a constructor.</summary>
		// Token: 0x040007AC RID: 1964
		Constructor = 32,
		/// <summary>Attribute can be applied to a method.</summary>
		// Token: 0x040007AD RID: 1965
		Method = 64,
		/// <summary>Attribute can be applied to a property.</summary>
		// Token: 0x040007AE RID: 1966
		Property = 128,
		/// <summary>Attribute can be applied to a field.</summary>
		// Token: 0x040007AF RID: 1967
		Field = 256,
		/// <summary>Attribute can be applied to an event.</summary>
		// Token: 0x040007B0 RID: 1968
		Event = 512,
		/// <summary>Attribute can be applied to an interface.</summary>
		// Token: 0x040007B1 RID: 1969
		Interface = 1024,
		/// <summary>Attribute can be applied to a parameter.</summary>
		// Token: 0x040007B2 RID: 1970
		Parameter = 2048,
		/// <summary>Attribute can be applied to a delegate.</summary>
		// Token: 0x040007B3 RID: 1971
		Delegate = 4096,
		/// <summary>Attribute can be applied to a return value.</summary>
		// Token: 0x040007B4 RID: 1972
		ReturnValue = 8192,
		/// <summary>Attribute can be applied to a generic parameter.</summary>
		// Token: 0x040007B5 RID: 1973
		GenericParameter = 16384,
		/// <summary>Attribute can be applied to any application element.</summary>
		// Token: 0x040007B6 RID: 1974
		All = 32767
	}
}
