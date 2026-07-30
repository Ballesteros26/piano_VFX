using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AE5 RID: 2789
	internal enum TraceLoggingDataType
	{
		// Token: 0x0400319A RID: 12698
		Nil,
		// Token: 0x0400319B RID: 12699
		Utf16String,
		// Token: 0x0400319C RID: 12700
		MbcsString,
		// Token: 0x0400319D RID: 12701
		Int8,
		// Token: 0x0400319E RID: 12702
		UInt8,
		// Token: 0x0400319F RID: 12703
		Int16,
		// Token: 0x040031A0 RID: 12704
		UInt16,
		// Token: 0x040031A1 RID: 12705
		Int32,
		// Token: 0x040031A2 RID: 12706
		UInt32,
		// Token: 0x040031A3 RID: 12707
		Int64,
		// Token: 0x040031A4 RID: 12708
		UInt64,
		// Token: 0x040031A5 RID: 12709
		Float,
		// Token: 0x040031A6 RID: 12710
		Double,
		// Token: 0x040031A7 RID: 12711
		Boolean32,
		// Token: 0x040031A8 RID: 12712
		Binary,
		// Token: 0x040031A9 RID: 12713
		Guid,
		// Token: 0x040031AA RID: 12714
		FileTime = 17,
		// Token: 0x040031AB RID: 12715
		SystemTime,
		// Token: 0x040031AC RID: 12716
		HexInt32 = 20,
		// Token: 0x040031AD RID: 12717
		HexInt64,
		// Token: 0x040031AE RID: 12718
		CountedUtf16String,
		// Token: 0x040031AF RID: 12719
		CountedMbcsString,
		// Token: 0x040031B0 RID: 12720
		Struct,
		// Token: 0x040031B1 RID: 12721
		Char16 = 518,
		// Token: 0x040031B2 RID: 12722
		Char8 = 516,
		// Token: 0x040031B3 RID: 12723
		Boolean8 = 772,
		// Token: 0x040031B4 RID: 12724
		HexInt8 = 1028,
		// Token: 0x040031B5 RID: 12725
		HexInt16 = 1030,
		// Token: 0x040031B6 RID: 12726
		Utf16Xml = 2817,
		// Token: 0x040031B7 RID: 12727
		MbcsXml,
		// Token: 0x040031B8 RID: 12728
		CountedUtf16Xml = 2838,
		// Token: 0x040031B9 RID: 12729
		CountedMbcsXml,
		// Token: 0x040031BA RID: 12730
		Utf16Json = 3073,
		// Token: 0x040031BB RID: 12731
		MbcsJson,
		// Token: 0x040031BC RID: 12732
		CountedUtf16Json = 3094,
		// Token: 0x040031BD RID: 12733
		CountedMbcsJson,
		// Token: 0x040031BE RID: 12734
		HResult = 3847
	}
}
