using System;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200004B RID: 75
	public enum FontEngineError
	{
		// Token: 0x040003A3 RID: 931
		Success,
		// Token: 0x040003A4 RID: 932
		Invalid_File_Path,
		// Token: 0x040003A5 RID: 933
		Invalid_File_Format,
		// Token: 0x040003A6 RID: 934
		Invalid_File_Structure,
		// Token: 0x040003A7 RID: 935
		Invalid_File,
		// Token: 0x040003A8 RID: 936
		Invalid_Table = 8,
		// Token: 0x040003A9 RID: 937
		Invalid_Glyph_Index = 16,
		// Token: 0x040003AA RID: 938
		Invalid_Character_Code,
		// Token: 0x040003AB RID: 939
		Invalid_Pixel_Size = 23,
		// Token: 0x040003AC RID: 940
		Invalid_Library = 33,
		// Token: 0x040003AD RID: 941
		Invalid_Face = 35,
		// Token: 0x040003AE RID: 942
		Invalid_Library_or_Face = 41,
		// Token: 0x040003AF RID: 943
		Atlas_Generation_Cancelled = 100,
		// Token: 0x040003B0 RID: 944
		Invalid_SharedTextureData
	}
}
