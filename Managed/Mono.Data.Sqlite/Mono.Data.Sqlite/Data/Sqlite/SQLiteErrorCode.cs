using System;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000022 RID: 34
	public enum SQLiteErrorCode
	{
		// Token: 0x040000A9 RID: 169
		Ok,
		// Token: 0x040000AA RID: 170
		Error,
		// Token: 0x040000AB RID: 171
		Internal,
		// Token: 0x040000AC RID: 172
		Perm,
		// Token: 0x040000AD RID: 173
		Abort,
		// Token: 0x040000AE RID: 174
		Busy,
		// Token: 0x040000AF RID: 175
		Locked,
		// Token: 0x040000B0 RID: 176
		NoMem,
		// Token: 0x040000B1 RID: 177
		ReadOnly,
		// Token: 0x040000B2 RID: 178
		Interrupt,
		// Token: 0x040000B3 RID: 179
		IOErr,
		// Token: 0x040000B4 RID: 180
		Corrupt,
		// Token: 0x040000B5 RID: 181
		NotFound,
		// Token: 0x040000B6 RID: 182
		Full,
		// Token: 0x040000B7 RID: 183
		CantOpen,
		// Token: 0x040000B8 RID: 184
		Protocol,
		// Token: 0x040000B9 RID: 185
		Empty,
		// Token: 0x040000BA RID: 186
		Schema,
		// Token: 0x040000BB RID: 187
		TooBig,
		// Token: 0x040000BC RID: 188
		Constraint,
		// Token: 0x040000BD RID: 189
		Mismatch,
		// Token: 0x040000BE RID: 190
		Misuse,
		// Token: 0x040000BF RID: 191
		NOLFS,
		// Token: 0x040000C0 RID: 192
		Auth,
		// Token: 0x040000C1 RID: 193
		Format,
		// Token: 0x040000C2 RID: 194
		Range,
		// Token: 0x040000C3 RID: 195
		NotADatabase,
		// Token: 0x040000C4 RID: 196
		Row = 100,
		// Token: 0x040000C5 RID: 197
		Done
	}
}
