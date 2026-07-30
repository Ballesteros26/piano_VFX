using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x02000029 RID: 41
	internal enum Minor
	{
		// Token: 0x0400009F RID: 159
		Undefined,
		// Token: 0x040000A0 RID: 160
		Skip,
		// Token: 0x040000A1 RID: 161
		AnsiCharSet,
		// Token: 0x040000A2 RID: 162
		MacCharSet,
		// Token: 0x040000A3 RID: 163
		PcCharSet,
		// Token: 0x040000A4 RID: 164
		PcaCharSet,
		// Token: 0x040000A5 RID: 165
		FontTbl,
		// Token: 0x040000A6 RID: 166
		FontAltName,
		// Token: 0x040000A7 RID: 167
		EmbeddedFont,
		// Token: 0x040000A8 RID: 168
		FontFile,
		// Token: 0x040000A9 RID: 169
		FileTbl,
		// Token: 0x040000AA RID: 170
		FileInfo,
		// Token: 0x040000AB RID: 171
		ColorTbl,
		// Token: 0x040000AC RID: 172
		StyleSheet,
		// Token: 0x040000AD RID: 173
		KeyCode,
		// Token: 0x040000AE RID: 174
		RevisionTbl,
		// Token: 0x040000AF RID: 175
		Info,
		// Token: 0x040000B0 RID: 176
		ITitle,
		// Token: 0x040000B1 RID: 177
		ISubject,
		// Token: 0x040000B2 RID: 178
		IAuthor,
		// Token: 0x040000B3 RID: 179
		IOperator,
		// Token: 0x040000B4 RID: 180
		IKeywords,
		// Token: 0x040000B5 RID: 181
		IComment,
		// Token: 0x040000B6 RID: 182
		IVersion,
		// Token: 0x040000B7 RID: 183
		IDoccomm,
		// Token: 0x040000B8 RID: 184
		IVerscomm,
		// Token: 0x040000B9 RID: 185
		NextFile,
		// Token: 0x040000BA RID: 186
		Template,
		// Token: 0x040000BB RID: 187
		FNSep,
		// Token: 0x040000BC RID: 188
		FNContSep,
		// Token: 0x040000BD RID: 189
		FNContNotice,
		// Token: 0x040000BE RID: 190
		ENSep,
		// Token: 0x040000BF RID: 191
		ENContSep,
		// Token: 0x040000C0 RID: 192
		ENContNotice,
		// Token: 0x040000C1 RID: 193
		PageNumLevel,
		// Token: 0x040000C2 RID: 194
		ParNumLevelStyle,
		// Token: 0x040000C3 RID: 195
		Header,
		// Token: 0x040000C4 RID: 196
		Footer,
		// Token: 0x040000C5 RID: 197
		HeaderLeft,
		// Token: 0x040000C6 RID: 198
		HeaderRight,
		// Token: 0x040000C7 RID: 199
		HeaderFirst,
		// Token: 0x040000C8 RID: 200
		FooterLeft,
		// Token: 0x040000C9 RID: 201
		FooterRight,
		// Token: 0x040000CA RID: 202
		FooterFirst,
		// Token: 0x040000CB RID: 203
		ParNumText,
		// Token: 0x040000CC RID: 204
		ParNumbering,
		// Token: 0x040000CD RID: 205
		ParNumTextAfter,
		// Token: 0x040000CE RID: 206
		ParNumTextBefore,
		// Token: 0x040000CF RID: 207
		BookmarkStart,
		// Token: 0x040000D0 RID: 208
		BookmarkEnd,
		// Token: 0x040000D1 RID: 209
		Pict,
		// Token: 0x040000D2 RID: 210
		Object,
		// Token: 0x040000D3 RID: 211
		ObjClass,
		// Token: 0x040000D4 RID: 212
		ObjName,
		// Token: 0x040000D5 RID: 213
		ObjTime,
		// Token: 0x040000D6 RID: 214
		ObjData,
		// Token: 0x040000D7 RID: 215
		ObjAlias,
		// Token: 0x040000D8 RID: 216
		ObjSection,
		// Token: 0x040000D9 RID: 217
		ObjResult,
		// Token: 0x040000DA RID: 218
		ObjItem,
		// Token: 0x040000DB RID: 219
		ObjTopic,
		// Token: 0x040000DC RID: 220
		DrawObject,
		// Token: 0x040000DD RID: 221
		Footnote,
		// Token: 0x040000DE RID: 222
		AnnotRefStart,
		// Token: 0x040000DF RID: 223
		AnnotRefEnd,
		// Token: 0x040000E0 RID: 224
		AnnotID,
		// Token: 0x040000E1 RID: 225
		AnnotAuthor,
		// Token: 0x040000E2 RID: 226
		Annotation,
		// Token: 0x040000E3 RID: 227
		AnnotRef,
		// Token: 0x040000E4 RID: 228
		AnnotTime,
		// Token: 0x040000E5 RID: 229
		AnnotIcon,
		// Token: 0x040000E6 RID: 230
		Field,
		// Token: 0x040000E7 RID: 231
		FieldInst,
		// Token: 0x040000E8 RID: 232
		FieldResult,
		// Token: 0x040000E9 RID: 233
		DataField,
		// Token: 0x040000EA RID: 234
		Index,
		// Token: 0x040000EB RID: 235
		IndexText,
		// Token: 0x040000EC RID: 236
		IndexRange,
		// Token: 0x040000ED RID: 237
		TOC,
		// Token: 0x040000EE RID: 238
		NeXTGraphic,
		// Token: 0x040000EF RID: 239
		MaxDestination,
		// Token: 0x040000F0 RID: 240
		FFNil,
		// Token: 0x040000F1 RID: 241
		FFRoman,
		// Token: 0x040000F2 RID: 242
		FFSwiss,
		// Token: 0x040000F3 RID: 243
		FFModern,
		// Token: 0x040000F4 RID: 244
		FFScript,
		// Token: 0x040000F5 RID: 245
		FFDecor,
		// Token: 0x040000F6 RID: 246
		FFTech,
		// Token: 0x040000F7 RID: 247
		FFBidirectional,
		// Token: 0x040000F8 RID: 248
		Red,
		// Token: 0x040000F9 RID: 249
		Green,
		// Token: 0x040000FA RID: 250
		Blue,
		// Token: 0x040000FB RID: 251
		IIntVersion,
		// Token: 0x040000FC RID: 252
		ICreateTime,
		// Token: 0x040000FD RID: 253
		IRevisionTime,
		// Token: 0x040000FE RID: 254
		IPrintTime,
		// Token: 0x040000FF RID: 255
		IBackupTime,
		// Token: 0x04000100 RID: 256
		IEditTime,
		// Token: 0x04000101 RID: 257
		IYear,
		// Token: 0x04000102 RID: 258
		IMonth,
		// Token: 0x04000103 RID: 259
		IDay,
		// Token: 0x04000104 RID: 260
		IHour,
		// Token: 0x04000105 RID: 261
		IMinute,
		// Token: 0x04000106 RID: 262
		ISecond,
		// Token: 0x04000107 RID: 263
		INPages,
		// Token: 0x04000108 RID: 264
		INWords,
		// Token: 0x04000109 RID: 265
		INChars,
		// Token: 0x0400010A RID: 266
		IIntID,
		// Token: 0x0400010B RID: 267
		CurHeadDate,
		// Token: 0x0400010C RID: 268
		CurHeadDateLong,
		// Token: 0x0400010D RID: 269
		CurHeadDateAbbrev,
		// Token: 0x0400010E RID: 270
		CurHeadTime,
		// Token: 0x0400010F RID: 271
		CurHeadPage,
		// Token: 0x04000110 RID: 272
		SectNum,
		// Token: 0x04000111 RID: 273
		CurFNote,
		// Token: 0x04000112 RID: 274
		CurAnnotRef,
		// Token: 0x04000113 RID: 275
		FNoteSep,
		// Token: 0x04000114 RID: 276
		FNoteCont,
		// Token: 0x04000115 RID: 277
		Cell,
		// Token: 0x04000116 RID: 278
		Row,
		// Token: 0x04000117 RID: 279
		Par,
		// Token: 0x04000118 RID: 280
		Sect,
		// Token: 0x04000119 RID: 281
		Page,
		// Token: 0x0400011A RID: 282
		Column,
		// Token: 0x0400011B RID: 283
		Line,
		// Token: 0x0400011C RID: 284
		SoftPage,
		// Token: 0x0400011D RID: 285
		SoftColumn,
		// Token: 0x0400011E RID: 286
		SoftLine,
		// Token: 0x0400011F RID: 287
		SoftLineHt,
		// Token: 0x04000120 RID: 288
		Tab,
		// Token: 0x04000121 RID: 289
		EmDash,
		// Token: 0x04000122 RID: 290
		EnDash,
		// Token: 0x04000123 RID: 291
		EmSpace,
		// Token: 0x04000124 RID: 292
		EnSpace,
		// Token: 0x04000125 RID: 293
		Bullet,
		// Token: 0x04000126 RID: 294
		LQuote,
		// Token: 0x04000127 RID: 295
		RQuote,
		// Token: 0x04000128 RID: 296
		LDblQuote,
		// Token: 0x04000129 RID: 297
		RDblQuote,
		// Token: 0x0400012A RID: 298
		Formula,
		// Token: 0x0400012B RID: 299
		NoBrkSpace,
		// Token: 0x0400012C RID: 300
		NoReqHyphen,
		// Token: 0x0400012D RID: 301
		NoBrkHyphen,
		// Token: 0x0400012E RID: 302
		OptDest,
		// Token: 0x0400012F RID: 303
		LTRMark,
		// Token: 0x04000130 RID: 304
		RTLMark,
		// Token: 0x04000131 RID: 305
		NoWidthJoiner,
		// Token: 0x04000132 RID: 306
		NoWidthNonJoiner,
		// Token: 0x04000133 RID: 307
		CurHeadPict,
		// Token: 0x04000134 RID: 308
		Additive,
		// Token: 0x04000135 RID: 309
		BasedOn,
		// Token: 0x04000136 RID: 310
		Next,
		// Token: 0x04000137 RID: 311
		DefTab,
		// Token: 0x04000138 RID: 312
		HyphHotZone,
		// Token: 0x04000139 RID: 313
		HyphConsecLines,
		// Token: 0x0400013A RID: 314
		HyphCaps,
		// Token: 0x0400013B RID: 315
		HyphAuto,
		// Token: 0x0400013C RID: 316
		LineStart,
		// Token: 0x0400013D RID: 317
		FracWidth,
		// Token: 0x0400013E RID: 318
		MakeBackup,
		// Token: 0x0400013F RID: 319
		RTFDefault,
		// Token: 0x04000140 RID: 320
		PSOverlay,
		// Token: 0x04000141 RID: 321
		DocTemplate,
		// Token: 0x04000142 RID: 322
		DefLanguage,
		// Token: 0x04000143 RID: 323
		FENoteType,
		// Token: 0x04000144 RID: 324
		FNoteEndSect,
		// Token: 0x04000145 RID: 325
		FNoteEndDoc,
		// Token: 0x04000146 RID: 326
		FNoteText,
		// Token: 0x04000147 RID: 327
		FNoteBottom,
		// Token: 0x04000148 RID: 328
		ENoteEndSect,
		// Token: 0x04000149 RID: 329
		ENoteEndDoc,
		// Token: 0x0400014A RID: 330
		ENoteText,
		// Token: 0x0400014B RID: 331
		ENoteBottom,
		// Token: 0x0400014C RID: 332
		FNoteStart,
		// Token: 0x0400014D RID: 333
		ENoteStart,
		// Token: 0x0400014E RID: 334
		FNoteRestartPage,
		// Token: 0x0400014F RID: 335
		FNoteRestart,
		// Token: 0x04000150 RID: 336
		FNoteRestartCont,
		// Token: 0x04000151 RID: 337
		ENoteRestart,
		// Token: 0x04000152 RID: 338
		ENoteRestartCont,
		// Token: 0x04000153 RID: 339
		FNoteNumArabic,
		// Token: 0x04000154 RID: 340
		FNoteNumLLetter,
		// Token: 0x04000155 RID: 341
		FNoteNumULetter,
		// Token: 0x04000156 RID: 342
		FNoteNumLRoman,
		// Token: 0x04000157 RID: 343
		FNoteNumURoman,
		// Token: 0x04000158 RID: 344
		FNoteNumChicago,
		// Token: 0x04000159 RID: 345
		ENoteNumArabic,
		// Token: 0x0400015A RID: 346
		ENoteNumLLetter,
		// Token: 0x0400015B RID: 347
		ENoteNumULetter,
		// Token: 0x0400015C RID: 348
		ENoteNumLRoman,
		// Token: 0x0400015D RID: 349
		ENoteNumURoman,
		// Token: 0x0400015E RID: 350
		ENoteNumChicago,
		// Token: 0x0400015F RID: 351
		PaperWidth,
		// Token: 0x04000160 RID: 352
		PaperHeight,
		// Token: 0x04000161 RID: 353
		PaperSize,
		// Token: 0x04000162 RID: 354
		LeftMargin,
		// Token: 0x04000163 RID: 355
		RightMargin,
		// Token: 0x04000164 RID: 356
		TopMargin,
		// Token: 0x04000165 RID: 357
		BottomMargin,
		// Token: 0x04000166 RID: 358
		FacingPage,
		// Token: 0x04000167 RID: 359
		GutterWid,
		// Token: 0x04000168 RID: 360
		MirrorMargin,
		// Token: 0x04000169 RID: 361
		Landscape,
		// Token: 0x0400016A RID: 362
		PageStart,
		// Token: 0x0400016B RID: 363
		WidowCtrl,
		// Token: 0x0400016C RID: 364
		LinkStyles,
		// Token: 0x0400016D RID: 365
		NoAutoTabIndent,
		// Token: 0x0400016E RID: 366
		WrapSpaces,
		// Token: 0x0400016F RID: 367
		PrintColorsBlack,
		// Token: 0x04000170 RID: 368
		NoExtraSpaceRL,
		// Token: 0x04000171 RID: 369
		NoColumnBalance,
		// Token: 0x04000172 RID: 370
		CvtMailMergeQuote,
		// Token: 0x04000173 RID: 371
		SuppressTopSpace,
		// Token: 0x04000174 RID: 372
		SuppressPreParSpace,
		// Token: 0x04000175 RID: 373
		CombineTblBorders,
		// Token: 0x04000176 RID: 374
		TranspMetafiles,
		// Token: 0x04000177 RID: 375
		SwapBorders,
		// Token: 0x04000178 RID: 376
		ShowHardBreaks,
		// Token: 0x04000179 RID: 377
		FormProtected,
		// Token: 0x0400017A RID: 378
		AllProtected,
		// Token: 0x0400017B RID: 379
		FormShading,
		// Token: 0x0400017C RID: 380
		FormDisplay,
		// Token: 0x0400017D RID: 381
		PrintData,
		// Token: 0x0400017E RID: 382
		RevProtected,
		// Token: 0x0400017F RID: 383
		Revisions,
		// Token: 0x04000180 RID: 384
		RevDisplay,
		// Token: 0x04000181 RID: 385
		RevBar,
		// Token: 0x04000182 RID: 386
		AnnotProtected,
		// Token: 0x04000183 RID: 387
		RTLDoc,
		// Token: 0x04000184 RID: 388
		LTRDoc,
		// Token: 0x04000185 RID: 389
		SectDef,
		// Token: 0x04000186 RID: 390
		ENoteHere,
		// Token: 0x04000187 RID: 391
		PrtBinFirst,
		// Token: 0x04000188 RID: 392
		PrtBin,
		// Token: 0x04000189 RID: 393
		SectStyleNum,
		// Token: 0x0400018A RID: 394
		NoBreak,
		// Token: 0x0400018B RID: 395
		ColBreak,
		// Token: 0x0400018C RID: 396
		PageBreak,
		// Token: 0x0400018D RID: 397
		EvenBreak,
		// Token: 0x0400018E RID: 398
		OddBreak,
		// Token: 0x0400018F RID: 399
		Columns,
		// Token: 0x04000190 RID: 400
		ColumnSpace,
		// Token: 0x04000191 RID: 401
		ColumnNumber,
		// Token: 0x04000192 RID: 402
		ColumnSpRight,
		// Token: 0x04000193 RID: 403
		ColumnWidth,
		// Token: 0x04000194 RID: 404
		ColumnLine,
		// Token: 0x04000195 RID: 405
		LineModulus,
		// Token: 0x04000196 RID: 406
		LineDist,
		// Token: 0x04000197 RID: 407
		LineStarts,
		// Token: 0x04000198 RID: 408
		LineRestart,
		// Token: 0x04000199 RID: 409
		LineRestartPg,
		// Token: 0x0400019A RID: 410
		LineCont,
		// Token: 0x0400019B RID: 411
		SectPageWid,
		// Token: 0x0400019C RID: 412
		SectPageHt,
		// Token: 0x0400019D RID: 413
		SectMarginLeft,
		// Token: 0x0400019E RID: 414
		SectMarginRight,
		// Token: 0x0400019F RID: 415
		SectMarginTop,
		// Token: 0x040001A0 RID: 416
		SectMarginBottom,
		// Token: 0x040001A1 RID: 417
		SectMarginGutter,
		// Token: 0x040001A2 RID: 418
		SectLandscape,
		// Token: 0x040001A3 RID: 419
		TitleSpecial,
		// Token: 0x040001A4 RID: 420
		HeaderY,
		// Token: 0x040001A5 RID: 421
		FooterY,
		// Token: 0x040001A6 RID: 422
		PageStarts,
		// Token: 0x040001A7 RID: 423
		PageCont,
		// Token: 0x040001A8 RID: 424
		PageRestart,
		// Token: 0x040001A9 RID: 425
		PageNumRight,
		// Token: 0x040001AA RID: 426
		PageNumTop,
		// Token: 0x040001AB RID: 427
		PageDecimal,
		// Token: 0x040001AC RID: 428
		PageURoman,
		// Token: 0x040001AD RID: 429
		PageLRoman,
		// Token: 0x040001AE RID: 430
		PageULetter,
		// Token: 0x040001AF RID: 431
		PageLLetter,
		// Token: 0x040001B0 RID: 432
		PageNumHyphSep,
		// Token: 0x040001B1 RID: 433
		PageNumSpaceSep,
		// Token: 0x040001B2 RID: 434
		PageNumColonSep,
		// Token: 0x040001B3 RID: 435
		PageNumEmdashSep,
		// Token: 0x040001B4 RID: 436
		PageNumEndashSep,
		// Token: 0x040001B5 RID: 437
		TopVAlign,
		// Token: 0x040001B6 RID: 438
		BottomVAlign,
		// Token: 0x040001B7 RID: 439
		CenterVAlign,
		// Token: 0x040001B8 RID: 440
		JustVAlign,
		// Token: 0x040001B9 RID: 441
		RTLSect,
		// Token: 0x040001BA RID: 442
		LTRSect,
		// Token: 0x040001BB RID: 443
		RowDef,
		// Token: 0x040001BC RID: 444
		RowGapH,
		// Token: 0x040001BD RID: 445
		CellPos,
		// Token: 0x040001BE RID: 446
		MergeRngFirst,
		// Token: 0x040001BF RID: 447
		MergePrevious,
		// Token: 0x040001C0 RID: 448
		RowLeft,
		// Token: 0x040001C1 RID: 449
		RowRight,
		// Token: 0x040001C2 RID: 450
		RowCenter,
		// Token: 0x040001C3 RID: 451
		RowLeftEdge,
		// Token: 0x040001C4 RID: 452
		RowHt,
		// Token: 0x040001C5 RID: 453
		RowHeader,
		// Token: 0x040001C6 RID: 454
		RowKeep,
		// Token: 0x040001C7 RID: 455
		RTLRow,
		// Token: 0x040001C8 RID: 456
		LTRRow,
		// Token: 0x040001C9 RID: 457
		RowBordTop,
		// Token: 0x040001CA RID: 458
		RowBordLeft,
		// Token: 0x040001CB RID: 459
		RowBordBottom,
		// Token: 0x040001CC RID: 460
		RowBordRight,
		// Token: 0x040001CD RID: 461
		RowBordHoriz,
		// Token: 0x040001CE RID: 462
		RowBordVert,
		// Token: 0x040001CF RID: 463
		CellBordBottom,
		// Token: 0x040001D0 RID: 464
		CellBordTop,
		// Token: 0x040001D1 RID: 465
		CellBordLeft,
		// Token: 0x040001D2 RID: 466
		CellBordRight,
		// Token: 0x040001D3 RID: 467
		CellShading,
		// Token: 0x040001D4 RID: 468
		CellBgPatH,
		// Token: 0x040001D5 RID: 469
		CellBgPatV,
		// Token: 0x040001D6 RID: 470
		CellFwdDiagBgPat,
		// Token: 0x040001D7 RID: 471
		CellBwdDiagBgPat,
		// Token: 0x040001D8 RID: 472
		CellHatchBgPat,
		// Token: 0x040001D9 RID: 473
		CellDiagHatchBgPat,
		// Token: 0x040001DA RID: 474
		CellDarkBgPatH,
		// Token: 0x040001DB RID: 475
		CellDarkBgPatV,
		// Token: 0x040001DC RID: 476
		CellFwdDarkBgPat,
		// Token: 0x040001DD RID: 477
		CellBwdDarkBgPat,
		// Token: 0x040001DE RID: 478
		CellDarkHatchBgPat,
		// Token: 0x040001DF RID: 479
		CellDarkDiagHatchBgPat,
		// Token: 0x040001E0 RID: 480
		CellBgPatLineColor,
		// Token: 0x040001E1 RID: 481
		CellBgPatColor,
		// Token: 0x040001E2 RID: 482
		ParDef,
		// Token: 0x040001E3 RID: 483
		StyleNum,
		// Token: 0x040001E4 RID: 484
		Hyphenate,
		// Token: 0x040001E5 RID: 485
		InTable,
		// Token: 0x040001E6 RID: 486
		Keep,
		// Token: 0x040001E7 RID: 487
		NoWidowControl,
		// Token: 0x040001E8 RID: 488
		KeepNext,
		// Token: 0x040001E9 RID: 489
		OutlineLevel,
		// Token: 0x040001EA RID: 490
		NoLineNum,
		// Token: 0x040001EB RID: 491
		PBBefore,
		// Token: 0x040001EC RID: 492
		SideBySide,
		// Token: 0x040001ED RID: 493
		QuadLeft,
		// Token: 0x040001EE RID: 494
		QuadRight,
		// Token: 0x040001EF RID: 495
		QuadJust,
		// Token: 0x040001F0 RID: 496
		QuadCenter,
		// Token: 0x040001F1 RID: 497
		FirstIndent,
		// Token: 0x040001F2 RID: 498
		LeftIndent,
		// Token: 0x040001F3 RID: 499
		RightIndent,
		// Token: 0x040001F4 RID: 500
		SpaceBefore,
		// Token: 0x040001F5 RID: 501
		SpaceAfter,
		// Token: 0x040001F6 RID: 502
		SpaceBetween,
		// Token: 0x040001F7 RID: 503
		SpaceMultiply,
		// Token: 0x040001F8 RID: 504
		SubDocument,
		// Token: 0x040001F9 RID: 505
		RTLPar,
		// Token: 0x040001FA RID: 506
		LTRPar,
		// Token: 0x040001FB RID: 507
		TabPos,
		// Token: 0x040001FC RID: 508
		TabLeft,
		// Token: 0x040001FD RID: 509
		TabRight,
		// Token: 0x040001FE RID: 510
		TabCenter,
		// Token: 0x040001FF RID: 511
		TabDecimal,
		// Token: 0x04000200 RID: 512
		TabBar,
		// Token: 0x04000201 RID: 513
		LeaderDot,
		// Token: 0x04000202 RID: 514
		LeaderHyphen,
		// Token: 0x04000203 RID: 515
		LeaderUnder,
		// Token: 0x04000204 RID: 516
		LeaderThick,
		// Token: 0x04000205 RID: 517
		LeaderEqual,
		// Token: 0x04000206 RID: 518
		ParLevel,
		// Token: 0x04000207 RID: 519
		ParBullet,
		// Token: 0x04000208 RID: 520
		ParSimple,
		// Token: 0x04000209 RID: 521
		ParNumCont,
		// Token: 0x0400020A RID: 522
		ParNumOnce,
		// Token: 0x0400020B RID: 523
		ParNumAcross,
		// Token: 0x0400020C RID: 524
		ParHangIndent,
		// Token: 0x0400020D RID: 525
		ParNumRestart,
		// Token: 0x0400020E RID: 526
		ParNumCardinal,
		// Token: 0x0400020F RID: 527
		ParNumDecimal,
		// Token: 0x04000210 RID: 528
		ParNumULetter,
		// Token: 0x04000211 RID: 529
		ParNumURoman,
		// Token: 0x04000212 RID: 530
		ParNumLLetter,
		// Token: 0x04000213 RID: 531
		ParNumLRoman,
		// Token: 0x04000214 RID: 532
		ParNumOrdinal,
		// Token: 0x04000215 RID: 533
		ParNumOrdinalText,
		// Token: 0x04000216 RID: 534
		ParNumBold,
		// Token: 0x04000217 RID: 535
		ParNumItalic,
		// Token: 0x04000218 RID: 536
		ParNumAllCaps,
		// Token: 0x04000219 RID: 537
		ParNumSmallCaps,
		// Token: 0x0400021A RID: 538
		ParNumUnder,
		// Token: 0x0400021B RID: 539
		ParNumDotUnder,
		// Token: 0x0400021C RID: 540
		ParNumDbUnder,
		// Token: 0x0400021D RID: 541
		ParNumNoUnder,
		// Token: 0x0400021E RID: 542
		ParNumWordUnder,
		// Token: 0x0400021F RID: 543
		ParNumStrikethru,
		// Token: 0x04000220 RID: 544
		ParNumForeColor,
		// Token: 0x04000221 RID: 545
		ParNumFont,
		// Token: 0x04000222 RID: 546
		ParNumFontSize,
		// Token: 0x04000223 RID: 547
		ParNumIndent,
		// Token: 0x04000224 RID: 548
		ParNumSpacing,
		// Token: 0x04000225 RID: 549
		ParNumInclPrev,
		// Token: 0x04000226 RID: 550
		ParNumCenter,
		// Token: 0x04000227 RID: 551
		ParNumLeft,
		// Token: 0x04000228 RID: 552
		ParNumRight,
		// Token: 0x04000229 RID: 553
		ParNumStartAt,
		// Token: 0x0400022A RID: 554
		BorderTop,
		// Token: 0x0400022B RID: 555
		BorderBottom,
		// Token: 0x0400022C RID: 556
		BorderLeft,
		// Token: 0x0400022D RID: 557
		BorderRight,
		// Token: 0x0400022E RID: 558
		BorderBetween,
		// Token: 0x0400022F RID: 559
		BorderBar,
		// Token: 0x04000230 RID: 560
		BorderBox,
		// Token: 0x04000231 RID: 561
		BorderSingle,
		// Token: 0x04000232 RID: 562
		BorderThick,
		// Token: 0x04000233 RID: 563
		BorderShadow,
		// Token: 0x04000234 RID: 564
		BorderDouble,
		// Token: 0x04000235 RID: 565
		BorderDot,
		// Token: 0x04000236 RID: 566
		BorderDash,
		// Token: 0x04000237 RID: 567
		BorderHair,
		// Token: 0x04000238 RID: 568
		BorderWidth,
		// Token: 0x04000239 RID: 569
		BorderColor,
		// Token: 0x0400023A RID: 570
		BorderSpace,
		// Token: 0x0400023B RID: 571
		Shading,
		// Token: 0x0400023C RID: 572
		BgPatH,
		// Token: 0x0400023D RID: 573
		BgPatV,
		// Token: 0x0400023E RID: 574
		FwdDiagBgPat,
		// Token: 0x0400023F RID: 575
		BwdDiagBgPat,
		// Token: 0x04000240 RID: 576
		HatchBgPat,
		// Token: 0x04000241 RID: 577
		DiagHatchBgPat,
		// Token: 0x04000242 RID: 578
		DarkBgPatH,
		// Token: 0x04000243 RID: 579
		DarkBgPatV,
		// Token: 0x04000244 RID: 580
		FwdDarkBgPat,
		// Token: 0x04000245 RID: 581
		BwdDarkBgPat,
		// Token: 0x04000246 RID: 582
		DarkHatchBgPat,
		// Token: 0x04000247 RID: 583
		DarkDiagHatchBgPat,
		// Token: 0x04000248 RID: 584
		BgPatLineColor,
		// Token: 0x04000249 RID: 585
		BgPatColor,
		// Token: 0x0400024A RID: 586
		Plain,
		// Token: 0x0400024B RID: 587
		Bold,
		// Token: 0x0400024C RID: 588
		AllCaps,
		// Token: 0x0400024D RID: 589
		Deleted,
		// Token: 0x0400024E RID: 590
		SubScript,
		// Token: 0x0400024F RID: 591
		SubScrShrink,
		// Token: 0x04000250 RID: 592
		NoSuperSub,
		// Token: 0x04000251 RID: 593
		Expand,
		// Token: 0x04000252 RID: 594
		ExpandTwips,
		// Token: 0x04000253 RID: 595
		Kerning,
		// Token: 0x04000254 RID: 596
		FontNum,
		// Token: 0x04000255 RID: 597
		FontSize,
		// Token: 0x04000256 RID: 598
		Italic,
		// Token: 0x04000257 RID: 599
		Outline,
		// Token: 0x04000258 RID: 600
		Revised,
		// Token: 0x04000259 RID: 601
		RevAuthor,
		// Token: 0x0400025A RID: 602
		RevDTTM,
		// Token: 0x0400025B RID: 603
		SmallCaps,
		// Token: 0x0400025C RID: 604
		Shadow,
		// Token: 0x0400025D RID: 605
		StrikeThru,
		// Token: 0x0400025E RID: 606
		Underline,
		// Token: 0x0400025F RID: 607
		DotUnderline,
		// Token: 0x04000260 RID: 608
		DbUnderline,
		// Token: 0x04000261 RID: 609
		NoUnderline,
		// Token: 0x04000262 RID: 610
		WordUnderline,
		// Token: 0x04000263 RID: 611
		SuperScript,
		// Token: 0x04000264 RID: 612
		SuperScrShrink,
		// Token: 0x04000265 RID: 613
		Invisible,
		// Token: 0x04000266 RID: 614
		ForeColor,
		// Token: 0x04000267 RID: 615
		BackColor,
		// Token: 0x04000268 RID: 616
		RTLChar,
		// Token: 0x04000269 RID: 617
		LTRChar,
		// Token: 0x0400026A RID: 618
		CharStyleNum,
		// Token: 0x0400026B RID: 619
		CharCharSet,
		// Token: 0x0400026C RID: 620
		Language,
		// Token: 0x0400026D RID: 621
		Gray,
		// Token: 0x0400026E RID: 622
		MacQD,
		// Token: 0x0400026F RID: 623
		PMMetafile,
		// Token: 0x04000270 RID: 624
		WinMetafile,
		// Token: 0x04000271 RID: 625
		DevIndBitmap,
		// Token: 0x04000272 RID: 626
		WinBitmap,
		// Token: 0x04000273 RID: 627
		PngBlip,
		// Token: 0x04000274 RID: 628
		PixelBits,
		// Token: 0x04000275 RID: 629
		BitmapPlanes,
		// Token: 0x04000276 RID: 630
		BitmapWid,
		// Token: 0x04000277 RID: 631
		PicWid,
		// Token: 0x04000278 RID: 632
		PicHt,
		// Token: 0x04000279 RID: 633
		PicGoalWid,
		// Token: 0x0400027A RID: 634
		PicGoalHt,
		// Token: 0x0400027B RID: 635
		PicScaleX,
		// Token: 0x0400027C RID: 636
		PicScaleY,
		// Token: 0x0400027D RID: 637
		PicScaled,
		// Token: 0x0400027E RID: 638
		PicCropTop,
		// Token: 0x0400027F RID: 639
		PicCropBottom,
		// Token: 0x04000280 RID: 640
		PicCropLeft,
		// Token: 0x04000281 RID: 641
		PicCropRight,
		// Token: 0x04000282 RID: 642
		PicMFHasBitmap,
		// Token: 0x04000283 RID: 643
		PicMFBitsPerPixel,
		// Token: 0x04000284 RID: 644
		PicBinary,
		// Token: 0x04000285 RID: 645
		BookmarkFirstCol,
		// Token: 0x04000286 RID: 646
		BookmarkLastCol,
		// Token: 0x04000287 RID: 647
		NeXTGWidth,
		// Token: 0x04000288 RID: 648
		NeXTGHeight,
		// Token: 0x04000289 RID: 649
		FieldDirty,
		// Token: 0x0400028A RID: 650
		FieldEdited,
		// Token: 0x0400028B RID: 651
		FieldLocked,
		// Token: 0x0400028C RID: 652
		FieldPrivate,
		// Token: 0x0400028D RID: 653
		FieldAlt,
		// Token: 0x0400028E RID: 654
		TOCType,
		// Token: 0x0400028F RID: 655
		TOCLevel,
		// Token: 0x04000290 RID: 656
		AbsWid,
		// Token: 0x04000291 RID: 657
		AbsHt,
		// Token: 0x04000292 RID: 658
		RPosMargH,
		// Token: 0x04000293 RID: 659
		RPosPageH,
		// Token: 0x04000294 RID: 660
		RPosColH,
		// Token: 0x04000295 RID: 661
		PosX,
		// Token: 0x04000296 RID: 662
		PosNegX,
		// Token: 0x04000297 RID: 663
		PosXCenter,
		// Token: 0x04000298 RID: 664
		PosXInside,
		// Token: 0x04000299 RID: 665
		PosXOutSide,
		// Token: 0x0400029A RID: 666
		PosXRight,
		// Token: 0x0400029B RID: 667
		PosXLeft,
		// Token: 0x0400029C RID: 668
		RPosMargV,
		// Token: 0x0400029D RID: 669
		RPosPageV,
		// Token: 0x0400029E RID: 670
		RPosParaV,
		// Token: 0x0400029F RID: 671
		PosY,
		// Token: 0x040002A0 RID: 672
		PosNegY,
		// Token: 0x040002A1 RID: 673
		PosYInline,
		// Token: 0x040002A2 RID: 674
		PosYTop,
		// Token: 0x040002A3 RID: 675
		PosYCenter,
		// Token: 0x040002A4 RID: 676
		PosYBottom,
		// Token: 0x040002A5 RID: 677
		NoWrap,
		// Token: 0x040002A6 RID: 678
		DistFromTextAll,
		// Token: 0x040002A7 RID: 679
		DistFromTextX,
		// Token: 0x040002A8 RID: 680
		DistFromTextY,
		// Token: 0x040002A9 RID: 681
		TextDistY,
		// Token: 0x040002AA RID: 682
		DropCapLines,
		// Token: 0x040002AB RID: 683
		DropCapType,
		// Token: 0x040002AC RID: 684
		ObjEmb,
		// Token: 0x040002AD RID: 685
		ObjLink,
		// Token: 0x040002AE RID: 686
		ObjAutoLink,
		// Token: 0x040002AF RID: 687
		ObjSubscriber,
		// Token: 0x040002B0 RID: 688
		ObjPublisher,
		// Token: 0x040002B1 RID: 689
		ObjICEmb,
		// Token: 0x040002B2 RID: 690
		ObjLinkSelf,
		// Token: 0x040002B3 RID: 691
		ObjLock,
		// Token: 0x040002B4 RID: 692
		ObjUpdate,
		// Token: 0x040002B5 RID: 693
		ObjHt,
		// Token: 0x040002B6 RID: 694
		ObjWid,
		// Token: 0x040002B7 RID: 695
		ObjSetSize,
		// Token: 0x040002B8 RID: 696
		ObjAlign,
		// Token: 0x040002B9 RID: 697
		ObjTransposeY,
		// Token: 0x040002BA RID: 698
		ObjCropTop,
		// Token: 0x040002BB RID: 699
		ObjCropBottom,
		// Token: 0x040002BC RID: 700
		ObjCropLeft,
		// Token: 0x040002BD RID: 701
		ObjCropRight,
		// Token: 0x040002BE RID: 702
		ObjScaleX,
		// Token: 0x040002BF RID: 703
		ObjScaleY,
		// Token: 0x040002C0 RID: 704
		ObjResRTF,
		// Token: 0x040002C1 RID: 705
		ObjResPict,
		// Token: 0x040002C2 RID: 706
		ObjResBitmap,
		// Token: 0x040002C3 RID: 707
		ObjResText,
		// Token: 0x040002C4 RID: 708
		ObjResMerge,
		// Token: 0x040002C5 RID: 709
		ObjBookmarkPubObj,
		// Token: 0x040002C6 RID: 710
		ObjPubAutoUpdate,
		// Token: 0x040002C7 RID: 711
		FNAlt,
		// Token: 0x040002C8 RID: 712
		AltKey,
		// Token: 0x040002C9 RID: 713
		ShiftKey,
		// Token: 0x040002CA RID: 714
		ControlKey,
		// Token: 0x040002CB RID: 715
		FunctionKey,
		// Token: 0x040002CC RID: 716
		ACBold,
		// Token: 0x040002CD RID: 717
		ACAllCaps,
		// Token: 0x040002CE RID: 718
		ACForeColor,
		// Token: 0x040002CF RID: 719
		ACSubScript,
		// Token: 0x040002D0 RID: 720
		ACExpand,
		// Token: 0x040002D1 RID: 721
		ACFontNum,
		// Token: 0x040002D2 RID: 722
		ACFontSize,
		// Token: 0x040002D3 RID: 723
		ACItalic,
		// Token: 0x040002D4 RID: 724
		ACLanguage,
		// Token: 0x040002D5 RID: 725
		ACOutline,
		// Token: 0x040002D6 RID: 726
		ACSmallCaps,
		// Token: 0x040002D7 RID: 727
		ACShadow,
		// Token: 0x040002D8 RID: 728
		ACStrikeThru,
		// Token: 0x040002D9 RID: 729
		ACUnderline,
		// Token: 0x040002DA RID: 730
		ACDotUnderline,
		// Token: 0x040002DB RID: 731
		ACDbUnderline,
		// Token: 0x040002DC RID: 732
		ACNoUnderline,
		// Token: 0x040002DD RID: 733
		ACWordUnderline,
		// Token: 0x040002DE RID: 734
		ACSuperScript,
		// Token: 0x040002DF RID: 735
		FontCharSet,
		// Token: 0x040002E0 RID: 736
		FontPitch,
		// Token: 0x040002E1 RID: 737
		FontCodePage,
		// Token: 0x040002E2 RID: 738
		FTypeNil,
		// Token: 0x040002E3 RID: 739
		FTypeTrueType,
		// Token: 0x040002E4 RID: 740
		FileNum,
		// Token: 0x040002E5 RID: 741
		FileRelPath,
		// Token: 0x040002E6 RID: 742
		FileOSNum,
		// Token: 0x040002E7 RID: 743
		SrcMacintosh,
		// Token: 0x040002E8 RID: 744
		SrcDOS,
		// Token: 0x040002E9 RID: 745
		SrcNTFS,
		// Token: 0x040002EA RID: 746
		SrcHPFS,
		// Token: 0x040002EB RID: 747
		SrcNetwork,
		// Token: 0x040002EC RID: 748
		DrawLock,
		// Token: 0x040002ED RID: 749
		DrawPageRelX,
		// Token: 0x040002EE RID: 750
		DrawColumnRelX,
		// Token: 0x040002EF RID: 751
		DrawMarginRelX,
		// Token: 0x040002F0 RID: 752
		DrawPageRelY,
		// Token: 0x040002F1 RID: 753
		DrawColumnRelY,
		// Token: 0x040002F2 RID: 754
		DrawMarginRelY,
		// Token: 0x040002F3 RID: 755
		DrawHeight,
		// Token: 0x040002F4 RID: 756
		DrawBeginGroup,
		// Token: 0x040002F5 RID: 757
		DrawGroupCount,
		// Token: 0x040002F6 RID: 758
		DrawEndGroup,
		// Token: 0x040002F7 RID: 759
		DrawArc,
		// Token: 0x040002F8 RID: 760
		DrawCallout,
		// Token: 0x040002F9 RID: 761
		DrawEllipse,
		// Token: 0x040002FA RID: 762
		DrawLine,
		// Token: 0x040002FB RID: 763
		DrawPolygon,
		// Token: 0x040002FC RID: 764
		DrawPolyLine,
		// Token: 0x040002FD RID: 765
		DrawRect,
		// Token: 0x040002FE RID: 766
		DrawTextBox,
		// Token: 0x040002FF RID: 767
		DrawOffsetX,
		// Token: 0x04000300 RID: 768
		DrawSizeX,
		// Token: 0x04000301 RID: 769
		DrawOffsetY,
		// Token: 0x04000302 RID: 770
		DrawSizeY,
		// Token: 0x04000303 RID: 771
		COAngle,
		// Token: 0x04000304 RID: 772
		COAccentBar,
		// Token: 0x04000305 RID: 773
		COBestFit,
		// Token: 0x04000306 RID: 774
		COBorder,
		// Token: 0x04000307 RID: 775
		COAttachAbsDist,
		// Token: 0x04000308 RID: 776
		COAttachBottom,
		// Token: 0x04000309 RID: 777
		COAttachCenter,
		// Token: 0x0400030A RID: 778
		COAttachTop,
		// Token: 0x0400030B RID: 779
		COLength,
		// Token: 0x0400030C RID: 780
		CONegXQuadrant,
		// Token: 0x0400030D RID: 781
		CONegYQuadrant,
		// Token: 0x0400030E RID: 782
		COOffset,
		// Token: 0x0400030F RID: 783
		COAttachSmart,
		// Token: 0x04000310 RID: 784
		CODoubleLine,
		// Token: 0x04000311 RID: 785
		CORightAngle,
		// Token: 0x04000312 RID: 786
		COSingleLine,
		// Token: 0x04000313 RID: 787
		COTripleLine,
		// Token: 0x04000314 RID: 788
		DrawTextBoxMargin,
		// Token: 0x04000315 RID: 789
		DrawTextBoxText,
		// Token: 0x04000316 RID: 790
		DrawRoundRect,
		// Token: 0x04000317 RID: 791
		DrawPointX,
		// Token: 0x04000318 RID: 792
		DrawPointY,
		// Token: 0x04000319 RID: 793
		DrawPolyCount,
		// Token: 0x0400031A RID: 794
		DrawArcFlipX,
		// Token: 0x0400031B RID: 795
		DrawArcFlipY,
		// Token: 0x0400031C RID: 796
		DrawLineBlue,
		// Token: 0x0400031D RID: 797
		DrawLineGreen,
		// Token: 0x0400031E RID: 798
		DrawLineRed,
		// Token: 0x0400031F RID: 799
		DrawLinePalette,
		// Token: 0x04000320 RID: 800
		DrawLineDashDot,
		// Token: 0x04000321 RID: 801
		DrawLineDashDotDot,
		// Token: 0x04000322 RID: 802
		DrawLineDash,
		// Token: 0x04000323 RID: 803
		DrawLineDot,
		// Token: 0x04000324 RID: 804
		DrawLineGray,
		// Token: 0x04000325 RID: 805
		DrawLineHollow,
		// Token: 0x04000326 RID: 806
		DrawLineSolid,
		// Token: 0x04000327 RID: 807
		DrawLineWidth,
		// Token: 0x04000328 RID: 808
		DrawHollowEndArrow,
		// Token: 0x04000329 RID: 809
		DrawEndArrowLength,
		// Token: 0x0400032A RID: 810
		DrawSolidEndArrow,
		// Token: 0x0400032B RID: 811
		DrawEndArrowWidth,
		// Token: 0x0400032C RID: 812
		DrawHollowStartArrow,
		// Token: 0x0400032D RID: 813
		DrawStartArrowLength,
		// Token: 0x0400032E RID: 814
		DrawSolidStartArrow,
		// Token: 0x0400032F RID: 815
		DrawStartArrowWidth,
		// Token: 0x04000330 RID: 816
		DrawBgFillBlue,
		// Token: 0x04000331 RID: 817
		DrawBgFillGreen,
		// Token: 0x04000332 RID: 818
		DrawBgFillRed,
		// Token: 0x04000333 RID: 819
		DrawBgFillPalette,
		// Token: 0x04000334 RID: 820
		DrawBgFillGray,
		// Token: 0x04000335 RID: 821
		DrawFgFillBlue,
		// Token: 0x04000336 RID: 822
		DrawFgFillGreen,
		// Token: 0x04000337 RID: 823
		DrawFgFillRed,
		// Token: 0x04000338 RID: 824
		DrawFgFillPalette,
		// Token: 0x04000339 RID: 825
		DrawFgFillGray,
		// Token: 0x0400033A RID: 826
		DrawFillPatIndex,
		// Token: 0x0400033B RID: 827
		DrawShadow,
		// Token: 0x0400033C RID: 828
		DrawShadowXOffset,
		// Token: 0x0400033D RID: 829
		DrawShadowYOffset,
		// Token: 0x0400033E RID: 830
		IndexNumber,
		// Token: 0x0400033F RID: 831
		IndexBold,
		// Token: 0x04000340 RID: 832
		IndexItalic,
		// Token: 0x04000341 RID: 833
		UnicodeCharBytes,
		// Token: 0x04000342 RID: 834
		UnicodeChar,
		// Token: 0x04000343 RID: 835
		UnicodeDestination,
		// Token: 0x04000344 RID: 836
		UnicodeDualDestination,
		// Token: 0x04000345 RID: 837
		UnicodeAnsiCodepage
	}
}
