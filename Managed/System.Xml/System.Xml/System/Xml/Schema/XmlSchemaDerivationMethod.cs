using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Provides different methods for preventing derivation.</summary>
	// Token: 0x0200044C RID: 1100
	[Flags]
	public enum XmlSchemaDerivationMethod
	{
		/// <summary>Override default derivation method to allow any derivation.</summary>
		// Token: 0x04001D76 RID: 7542
		[XmlEnum("")]
		Empty = 0,
		/// <summary>Refers to derivations by Substitution.</summary>
		// Token: 0x04001D77 RID: 7543
		[XmlEnum("substitution")]
		Substitution = 1,
		/// <summary>Refers to derivations by Extension.</summary>
		// Token: 0x04001D78 RID: 7544
		[XmlEnum("extension")]
		Extension = 2,
		/// <summary>Refers to derivations by Restriction.</summary>
		// Token: 0x04001D79 RID: 7545
		[XmlEnum("restriction")]
		Restriction = 4,
		/// <summary>Refers to derivations by List.</summary>
		// Token: 0x04001D7A RID: 7546
		[XmlEnum("list")]
		List = 8,
		/// <summary>Refers to derivations by Union.</summary>
		// Token: 0x04001D7B RID: 7547
		[XmlEnum("union")]
		Union = 16,
		/// <summary>#all. Refers to all derivation methods.</summary>
		// Token: 0x04001D7C RID: 7548
		[XmlEnum("#all")]
		All = 255,
		/// <summary>Accepts the default derivation method.</summary>
		// Token: 0x04001D7D RID: 7549
		[XmlIgnore]
		None = 256
	}
}
