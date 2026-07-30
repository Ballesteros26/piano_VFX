using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Defines identifiers for the standard set of commands that are available to most applications.</summary>
	// Token: 0x02000343 RID: 835
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class StandardCommands
	{
		// Token: 0x04001497 RID: 5271
		private static readonly Guid standardCommandSet = StandardCommands.ShellGuids.VSStandardCommandSet97;

		// Token: 0x04001498 RID: 5272
		private static readonly Guid ndpCommandSet = new Guid("{74D21313-2AEE-11d1-8BFB-00A0C90F26F7}");

		// Token: 0x04001499 RID: 5273
		private const int cmdidDesignerVerbFirst = 8192;

		// Token: 0x0400149A RID: 5274
		private const int cmdidDesignerVerbLast = 8448;

		// Token: 0x0400149B RID: 5275
		private const int cmdidArrangeIcons = 12298;

		// Token: 0x0400149C RID: 5276
		private const int cmdidLineupIcons = 12299;

		// Token: 0x0400149D RID: 5277
		private const int cmdidShowLargeIcons = 12300;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignBottom command. This field is read-only.</summary>
		// Token: 0x0400149E RID: 5278
		public static readonly CommandID AlignBottom = new CommandID(StandardCommands.standardCommandSet, 1);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignHorizontalCenters command. This field is read-only.</summary>
		// Token: 0x0400149F RID: 5279
		public static readonly CommandID AlignHorizontalCenters = new CommandID(StandardCommands.standardCommandSet, 2);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignLeft command. This field is read-only.</summary>
		// Token: 0x040014A0 RID: 5280
		public static readonly CommandID AlignLeft = new CommandID(StandardCommands.standardCommandSet, 3);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignRight command. This field is read-only.</summary>
		// Token: 0x040014A1 RID: 5281
		public static readonly CommandID AlignRight = new CommandID(StandardCommands.standardCommandSet, 4);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignToGrid command. This field is read-only.</summary>
		// Token: 0x040014A2 RID: 5282
		public static readonly CommandID AlignToGrid = new CommandID(StandardCommands.standardCommandSet, 5);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignTop command. This field is read-only.</summary>
		// Token: 0x040014A3 RID: 5283
		public static readonly CommandID AlignTop = new CommandID(StandardCommands.standardCommandSet, 6);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignVerticalCenters command. This field is read-only.</summary>
		// Token: 0x040014A4 RID: 5284
		public static readonly CommandID AlignVerticalCenters = new CommandID(StandardCommands.standardCommandSet, 7);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ArrangeBottom command. This field is read-only.</summary>
		// Token: 0x040014A5 RID: 5285
		public static readonly CommandID ArrangeBottom = new CommandID(StandardCommands.standardCommandSet, 8);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ArrangeRight command. This field is read-only.</summary>
		// Token: 0x040014A6 RID: 5286
		public static readonly CommandID ArrangeRight = new CommandID(StandardCommands.standardCommandSet, 9);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the BringForward command. This field is read-only.</summary>
		// Token: 0x040014A7 RID: 5287
		public static readonly CommandID BringForward = new CommandID(StandardCommands.standardCommandSet, 10);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the BringToFront command. This field is read-only.</summary>
		// Token: 0x040014A8 RID: 5288
		public static readonly CommandID BringToFront = new CommandID(StandardCommands.standardCommandSet, 11);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the CenterHorizontally command. This field is read-only.</summary>
		// Token: 0x040014A9 RID: 5289
		public static readonly CommandID CenterHorizontally = new CommandID(StandardCommands.standardCommandSet, 12);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the CenterVertically command. This field is read-only.</summary>
		// Token: 0x040014AA RID: 5290
		public static readonly CommandID CenterVertically = new CommandID(StandardCommands.standardCommandSet, 13);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ViewCode command. This field is read-only.</summary>
		// Token: 0x040014AB RID: 5291
		public static readonly CommandID ViewCode = new CommandID(StandardCommands.standardCommandSet, 333);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Document Outline command. This field is read-only.</summary>
		// Token: 0x040014AC RID: 5292
		public static readonly CommandID DocumentOutline = new CommandID(StandardCommands.standardCommandSet, 239);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Copy command. This field is read-only.</summary>
		// Token: 0x040014AD RID: 5293
		public static readonly CommandID Copy = new CommandID(StandardCommands.standardCommandSet, 15);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Cut command. This field is read-only.</summary>
		// Token: 0x040014AE RID: 5294
		public static readonly CommandID Cut = new CommandID(StandardCommands.standardCommandSet, 16);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Delete command. This field is read-only.</summary>
		// Token: 0x040014AF RID: 5295
		public static readonly CommandID Delete = new CommandID(StandardCommands.standardCommandSet, 17);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Group command. This field is read-only.</summary>
		// Token: 0x040014B0 RID: 5296
		public static readonly CommandID Group = new CommandID(StandardCommands.standardCommandSet, 20);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceConcatenate command. This field is read-only.</summary>
		// Token: 0x040014B1 RID: 5297
		public static readonly CommandID HorizSpaceConcatenate = new CommandID(StandardCommands.standardCommandSet, 21);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceDecrease command. This field is read-only.</summary>
		// Token: 0x040014B2 RID: 5298
		public static readonly CommandID HorizSpaceDecrease = new CommandID(StandardCommands.standardCommandSet, 22);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceIncrease command. This field is read-only.</summary>
		// Token: 0x040014B3 RID: 5299
		public static readonly CommandID HorizSpaceIncrease = new CommandID(StandardCommands.standardCommandSet, 23);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceMakeEqual command. This field is read-only.</summary>
		// Token: 0x040014B4 RID: 5300
		public static readonly CommandID HorizSpaceMakeEqual = new CommandID(StandardCommands.standardCommandSet, 24);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Paste command. This field is read-only.</summary>
		// Token: 0x040014B5 RID: 5301
		public static readonly CommandID Paste = new CommandID(StandardCommands.standardCommandSet, 26);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Properties command. This field is read-only.</summary>
		// Token: 0x040014B6 RID: 5302
		public static readonly CommandID Properties = new CommandID(StandardCommands.standardCommandSet, 28);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Redo command. This field is read-only.</summary>
		// Token: 0x040014B7 RID: 5303
		public static readonly CommandID Redo = new CommandID(StandardCommands.standardCommandSet, 29);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the MultiLevelRedo command. This field is read-only.</summary>
		// Token: 0x040014B8 RID: 5304
		public static readonly CommandID MultiLevelRedo = new CommandID(StandardCommands.standardCommandSet, 30);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SelectAll command. This field is read-only.</summary>
		// Token: 0x040014B9 RID: 5305
		public static readonly CommandID SelectAll = new CommandID(StandardCommands.standardCommandSet, 31);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SendBackward command. This field is read-only.</summary>
		// Token: 0x040014BA RID: 5306
		public static readonly CommandID SendBackward = new CommandID(StandardCommands.standardCommandSet, 32);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SendToBack command. This field is read-only.</summary>
		// Token: 0x040014BB RID: 5307
		public static readonly CommandID SendToBack = new CommandID(StandardCommands.standardCommandSet, 33);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToControl command. This field is read-only.</summary>
		// Token: 0x040014BC RID: 5308
		public static readonly CommandID SizeToControl = new CommandID(StandardCommands.standardCommandSet, 35);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToControlHeight command. This field is read-only.</summary>
		// Token: 0x040014BD RID: 5309
		public static readonly CommandID SizeToControlHeight = new CommandID(StandardCommands.standardCommandSet, 36);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToControlWidth command. This field is read-only.</summary>
		// Token: 0x040014BE RID: 5310
		public static readonly CommandID SizeToControlWidth = new CommandID(StandardCommands.standardCommandSet, 37);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToFit command. This field is read-only.</summary>
		// Token: 0x040014BF RID: 5311
		public static readonly CommandID SizeToFit = new CommandID(StandardCommands.standardCommandSet, 38);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToGrid command. This field is read-only.</summary>
		// Token: 0x040014C0 RID: 5312
		public static readonly CommandID SizeToGrid = new CommandID(StandardCommands.standardCommandSet, 39);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SnapToGrid command. This field is read-only.</summary>
		// Token: 0x040014C1 RID: 5313
		public static readonly CommandID SnapToGrid = new CommandID(StandardCommands.standardCommandSet, 40);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the TabOrder command. This field is read-only.</summary>
		// Token: 0x040014C2 RID: 5314
		public static readonly CommandID TabOrder = new CommandID(StandardCommands.standardCommandSet, 41);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Undo command. This field is read-only.</summary>
		// Token: 0x040014C3 RID: 5315
		public static readonly CommandID Undo = new CommandID(StandardCommands.standardCommandSet, 43);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the MultiLevelUndo command. This field is read-only.</summary>
		// Token: 0x040014C4 RID: 5316
		public static readonly CommandID MultiLevelUndo = new CommandID(StandardCommands.standardCommandSet, 44);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Ungroup command. This field is read-only.</summary>
		// Token: 0x040014C5 RID: 5317
		public static readonly CommandID Ungroup = new CommandID(StandardCommands.standardCommandSet, 45);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceConcatenate command. This field is read-only.</summary>
		// Token: 0x040014C6 RID: 5318
		public static readonly CommandID VertSpaceConcatenate = new CommandID(StandardCommands.standardCommandSet, 46);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceDecrease command. This field is read-only.</summary>
		// Token: 0x040014C7 RID: 5319
		public static readonly CommandID VertSpaceDecrease = new CommandID(StandardCommands.standardCommandSet, 47);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceIncrease command. This field is read-only.</summary>
		// Token: 0x040014C8 RID: 5320
		public static readonly CommandID VertSpaceIncrease = new CommandID(StandardCommands.standardCommandSet, 48);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceMakeEqual command. This field is read-only.</summary>
		// Token: 0x040014C9 RID: 5321
		public static readonly CommandID VertSpaceMakeEqual = new CommandID(StandardCommands.standardCommandSet, 49);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ShowGrid command. This field is read-only.</summary>
		// Token: 0x040014CA RID: 5322
		public static readonly CommandID ShowGrid = new CommandID(StandardCommands.standardCommandSet, 103);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ViewGrid command. This field is read-only.</summary>
		// Token: 0x040014CB RID: 5323
		public static readonly CommandID ViewGrid = new CommandID(StandardCommands.standardCommandSet, 125);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Replace command. This field is read-only.</summary>
		// Token: 0x040014CC RID: 5324
		public static readonly CommandID Replace = new CommandID(StandardCommands.standardCommandSet, 230);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the PropertiesWindow command. This field is read-only.</summary>
		// Token: 0x040014CD RID: 5325
		public static readonly CommandID PropertiesWindow = new CommandID(StandardCommands.standardCommandSet, 235);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the LockControls command. This field is read-only.</summary>
		// Token: 0x040014CE RID: 5326
		public static readonly CommandID LockControls = new CommandID(StandardCommands.standardCommandSet, 369);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the F1Help command. This field is read-only.</summary>
		// Token: 0x040014CF RID: 5327
		public static readonly CommandID F1Help = new CommandID(StandardCommands.standardCommandSet, 377);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ArrangeIcons command. This field is read-only.</summary>
		// Token: 0x040014D0 RID: 5328
		public static readonly CommandID ArrangeIcons = new CommandID(StandardCommands.ndpCommandSet, 12298);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the LineupIcons command. This field is read-only.</summary>
		// Token: 0x040014D1 RID: 5329
		public static readonly CommandID LineupIcons = new CommandID(StandardCommands.ndpCommandSet, 12299);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ShowLargeIcons command. This field is read-only.</summary>
		// Token: 0x040014D2 RID: 5330
		public static readonly CommandID ShowLargeIcons = new CommandID(StandardCommands.ndpCommandSet, 12300);

		/// <summary>Gets the first of a set of verbs. This field is read-only.</summary>
		// Token: 0x040014D3 RID: 5331
		public static readonly CommandID VerbFirst = new CommandID(StandardCommands.ndpCommandSet, 8192);

		/// <summary>Gets the last of a set of verbs. This field is read-only.</summary>
		// Token: 0x040014D4 RID: 5332
		public static readonly CommandID VerbLast = new CommandID(StandardCommands.ndpCommandSet, 8448);

		// Token: 0x02000344 RID: 836
		private static class VSStandardCommands
		{
			// Token: 0x040014D5 RID: 5333
			internal const int cmdidAlignBottom = 1;

			// Token: 0x040014D6 RID: 5334
			internal const int cmdidAlignHorizontalCenters = 2;

			// Token: 0x040014D7 RID: 5335
			internal const int cmdidAlignLeft = 3;

			// Token: 0x040014D8 RID: 5336
			internal const int cmdidAlignRight = 4;

			// Token: 0x040014D9 RID: 5337
			internal const int cmdidAlignToGrid = 5;

			// Token: 0x040014DA RID: 5338
			internal const int cmdidAlignTop = 6;

			// Token: 0x040014DB RID: 5339
			internal const int cmdidAlignVerticalCenters = 7;

			// Token: 0x040014DC RID: 5340
			internal const int cmdidArrangeBottom = 8;

			// Token: 0x040014DD RID: 5341
			internal const int cmdidArrangeRight = 9;

			// Token: 0x040014DE RID: 5342
			internal const int cmdidBringForward = 10;

			// Token: 0x040014DF RID: 5343
			internal const int cmdidBringToFront = 11;

			// Token: 0x040014E0 RID: 5344
			internal const int cmdidCenterHorizontally = 12;

			// Token: 0x040014E1 RID: 5345
			internal const int cmdidCenterVertically = 13;

			// Token: 0x040014E2 RID: 5346
			internal const int cmdidCode = 14;

			// Token: 0x040014E3 RID: 5347
			internal const int cmdidCopy = 15;

			// Token: 0x040014E4 RID: 5348
			internal const int cmdidCut = 16;

			// Token: 0x040014E5 RID: 5349
			internal const int cmdidDelete = 17;

			// Token: 0x040014E6 RID: 5350
			internal const int cmdidFontName = 18;

			// Token: 0x040014E7 RID: 5351
			internal const int cmdidFontSize = 19;

			// Token: 0x040014E8 RID: 5352
			internal const int cmdidGroup = 20;

			// Token: 0x040014E9 RID: 5353
			internal const int cmdidHorizSpaceConcatenate = 21;

			// Token: 0x040014EA RID: 5354
			internal const int cmdidHorizSpaceDecrease = 22;

			// Token: 0x040014EB RID: 5355
			internal const int cmdidHorizSpaceIncrease = 23;

			// Token: 0x040014EC RID: 5356
			internal const int cmdidHorizSpaceMakeEqual = 24;

			// Token: 0x040014ED RID: 5357
			internal const int cmdidLockControls = 369;

			// Token: 0x040014EE RID: 5358
			internal const int cmdidInsertObject = 25;

			// Token: 0x040014EF RID: 5359
			internal const int cmdidPaste = 26;

			// Token: 0x040014F0 RID: 5360
			internal const int cmdidPrint = 27;

			// Token: 0x040014F1 RID: 5361
			internal const int cmdidProperties = 28;

			// Token: 0x040014F2 RID: 5362
			internal const int cmdidRedo = 29;

			// Token: 0x040014F3 RID: 5363
			internal const int cmdidMultiLevelRedo = 30;

			// Token: 0x040014F4 RID: 5364
			internal const int cmdidSelectAll = 31;

			// Token: 0x040014F5 RID: 5365
			internal const int cmdidSendBackward = 32;

			// Token: 0x040014F6 RID: 5366
			internal const int cmdidSendToBack = 33;

			// Token: 0x040014F7 RID: 5367
			internal const int cmdidShowTable = 34;

			// Token: 0x040014F8 RID: 5368
			internal const int cmdidSizeToControl = 35;

			// Token: 0x040014F9 RID: 5369
			internal const int cmdidSizeToControlHeight = 36;

			// Token: 0x040014FA RID: 5370
			internal const int cmdidSizeToControlWidth = 37;

			// Token: 0x040014FB RID: 5371
			internal const int cmdidSizeToFit = 38;

			// Token: 0x040014FC RID: 5372
			internal const int cmdidSizeToGrid = 39;

			// Token: 0x040014FD RID: 5373
			internal const int cmdidSnapToGrid = 40;

			// Token: 0x040014FE RID: 5374
			internal const int cmdidTabOrder = 41;

			// Token: 0x040014FF RID: 5375
			internal const int cmdidToolbox = 42;

			// Token: 0x04001500 RID: 5376
			internal const int cmdidUndo = 43;

			// Token: 0x04001501 RID: 5377
			internal const int cmdidMultiLevelUndo = 44;

			// Token: 0x04001502 RID: 5378
			internal const int cmdidUngroup = 45;

			// Token: 0x04001503 RID: 5379
			internal const int cmdidVertSpaceConcatenate = 46;

			// Token: 0x04001504 RID: 5380
			internal const int cmdidVertSpaceDecrease = 47;

			// Token: 0x04001505 RID: 5381
			internal const int cmdidVertSpaceIncrease = 48;

			// Token: 0x04001506 RID: 5382
			internal const int cmdidVertSpaceMakeEqual = 49;

			// Token: 0x04001507 RID: 5383
			internal const int cmdidZoomPercent = 50;

			// Token: 0x04001508 RID: 5384
			internal const int cmdidBackColor = 51;

			// Token: 0x04001509 RID: 5385
			internal const int cmdidBold = 52;

			// Token: 0x0400150A RID: 5386
			internal const int cmdidBorderColor = 53;

			// Token: 0x0400150B RID: 5387
			internal const int cmdidBorderDashDot = 54;

			// Token: 0x0400150C RID: 5388
			internal const int cmdidBorderDashDotDot = 55;

			// Token: 0x0400150D RID: 5389
			internal const int cmdidBorderDashes = 56;

			// Token: 0x0400150E RID: 5390
			internal const int cmdidBorderDots = 57;

			// Token: 0x0400150F RID: 5391
			internal const int cmdidBorderShortDashes = 58;

			// Token: 0x04001510 RID: 5392
			internal const int cmdidBorderSolid = 59;

			// Token: 0x04001511 RID: 5393
			internal const int cmdidBorderSparseDots = 60;

			// Token: 0x04001512 RID: 5394
			internal const int cmdidBorderWidth1 = 61;

			// Token: 0x04001513 RID: 5395
			internal const int cmdidBorderWidth2 = 62;

			// Token: 0x04001514 RID: 5396
			internal const int cmdidBorderWidth3 = 63;

			// Token: 0x04001515 RID: 5397
			internal const int cmdidBorderWidth4 = 64;

			// Token: 0x04001516 RID: 5398
			internal const int cmdidBorderWidth5 = 65;

			// Token: 0x04001517 RID: 5399
			internal const int cmdidBorderWidth6 = 66;

			// Token: 0x04001518 RID: 5400
			internal const int cmdidBorderWidthHairline = 67;

			// Token: 0x04001519 RID: 5401
			internal const int cmdidFlat = 68;

			// Token: 0x0400151A RID: 5402
			internal const int cmdidForeColor = 69;

			// Token: 0x0400151B RID: 5403
			internal const int cmdidItalic = 70;

			// Token: 0x0400151C RID: 5404
			internal const int cmdidJustifyCenter = 71;

			// Token: 0x0400151D RID: 5405
			internal const int cmdidJustifyGeneral = 72;

			// Token: 0x0400151E RID: 5406
			internal const int cmdidJustifyLeft = 73;

			// Token: 0x0400151F RID: 5407
			internal const int cmdidJustifyRight = 74;

			// Token: 0x04001520 RID: 5408
			internal const int cmdidRaised = 75;

			// Token: 0x04001521 RID: 5409
			internal const int cmdidSunken = 76;

			// Token: 0x04001522 RID: 5410
			internal const int cmdidUnderline = 77;

			// Token: 0x04001523 RID: 5411
			internal const int cmdidChiseled = 78;

			// Token: 0x04001524 RID: 5412
			internal const int cmdidEtched = 79;

			// Token: 0x04001525 RID: 5413
			internal const int cmdidShadowed = 80;

			// Token: 0x04001526 RID: 5414
			internal const int cmdidCompDebug1 = 81;

			// Token: 0x04001527 RID: 5415
			internal const int cmdidCompDebug2 = 82;

			// Token: 0x04001528 RID: 5416
			internal const int cmdidCompDebug3 = 83;

			// Token: 0x04001529 RID: 5417
			internal const int cmdidCompDebug4 = 84;

			// Token: 0x0400152A RID: 5418
			internal const int cmdidCompDebug5 = 85;

			// Token: 0x0400152B RID: 5419
			internal const int cmdidCompDebug6 = 86;

			// Token: 0x0400152C RID: 5420
			internal const int cmdidCompDebug7 = 87;

			// Token: 0x0400152D RID: 5421
			internal const int cmdidCompDebug8 = 88;

			// Token: 0x0400152E RID: 5422
			internal const int cmdidCompDebug9 = 89;

			// Token: 0x0400152F RID: 5423
			internal const int cmdidCompDebug10 = 90;

			// Token: 0x04001530 RID: 5424
			internal const int cmdidCompDebug11 = 91;

			// Token: 0x04001531 RID: 5425
			internal const int cmdidCompDebug12 = 92;

			// Token: 0x04001532 RID: 5426
			internal const int cmdidCompDebug13 = 93;

			// Token: 0x04001533 RID: 5427
			internal const int cmdidCompDebug14 = 94;

			// Token: 0x04001534 RID: 5428
			internal const int cmdidCompDebug15 = 95;

			// Token: 0x04001535 RID: 5429
			internal const int cmdidExistingSchemaEdit = 96;

			// Token: 0x04001536 RID: 5430
			internal const int cmdidFind = 97;

			// Token: 0x04001537 RID: 5431
			internal const int cmdidGetZoom = 98;

			// Token: 0x04001538 RID: 5432
			internal const int cmdidQueryOpenDesign = 99;

			// Token: 0x04001539 RID: 5433
			internal const int cmdidQueryOpenNew = 100;

			// Token: 0x0400153A RID: 5434
			internal const int cmdidSingleTableDesign = 101;

			// Token: 0x0400153B RID: 5435
			internal const int cmdidSingleTableNew = 102;

			// Token: 0x0400153C RID: 5436
			internal const int cmdidShowGrid = 103;

			// Token: 0x0400153D RID: 5437
			internal const int cmdidNewTable = 104;

			// Token: 0x0400153E RID: 5438
			internal const int cmdidCollapsedView = 105;

			// Token: 0x0400153F RID: 5439
			internal const int cmdidFieldView = 106;

			// Token: 0x04001540 RID: 5440
			internal const int cmdidVerifySQL = 107;

			// Token: 0x04001541 RID: 5441
			internal const int cmdidHideTable = 108;

			// Token: 0x04001542 RID: 5442
			internal const int cmdidPrimaryKey = 109;

			// Token: 0x04001543 RID: 5443
			internal const int cmdidSave = 110;

			// Token: 0x04001544 RID: 5444
			internal const int cmdidSaveAs = 111;

			// Token: 0x04001545 RID: 5445
			internal const int cmdidSortAscending = 112;

			// Token: 0x04001546 RID: 5446
			internal const int cmdidSortDescending = 113;

			// Token: 0x04001547 RID: 5447
			internal const int cmdidAppendQuery = 114;

			// Token: 0x04001548 RID: 5448
			internal const int cmdidCrosstabQuery = 115;

			// Token: 0x04001549 RID: 5449
			internal const int cmdidDeleteQuery = 116;

			// Token: 0x0400154A RID: 5450
			internal const int cmdidMakeTableQuery = 117;

			// Token: 0x0400154B RID: 5451
			internal const int cmdidSelectQuery = 118;

			// Token: 0x0400154C RID: 5452
			internal const int cmdidUpdateQuery = 119;

			// Token: 0x0400154D RID: 5453
			internal const int cmdidParameters = 120;

			// Token: 0x0400154E RID: 5454
			internal const int cmdidTotals = 121;

			// Token: 0x0400154F RID: 5455
			internal const int cmdidViewCollapsed = 122;

			// Token: 0x04001550 RID: 5456
			internal const int cmdidViewFieldList = 123;

			// Token: 0x04001551 RID: 5457
			internal const int cmdidViewKeys = 124;

			// Token: 0x04001552 RID: 5458
			internal const int cmdidViewGrid = 125;

			// Token: 0x04001553 RID: 5459
			internal const int cmdidInnerJoin = 126;

			// Token: 0x04001554 RID: 5460
			internal const int cmdidRightOuterJoin = 127;

			// Token: 0x04001555 RID: 5461
			internal const int cmdidLeftOuterJoin = 128;

			// Token: 0x04001556 RID: 5462
			internal const int cmdidFullOuterJoin = 129;

			// Token: 0x04001557 RID: 5463
			internal const int cmdidUnionJoin = 130;

			// Token: 0x04001558 RID: 5464
			internal const int cmdidShowSQLPane = 131;

			// Token: 0x04001559 RID: 5465
			internal const int cmdidShowGraphicalPane = 132;

			// Token: 0x0400155A RID: 5466
			internal const int cmdidShowDataPane = 133;

			// Token: 0x0400155B RID: 5467
			internal const int cmdidShowQBEPane = 134;

			// Token: 0x0400155C RID: 5468
			internal const int cmdidSelectAllFields = 135;

			// Token: 0x0400155D RID: 5469
			internal const int cmdidOLEObjectMenuButton = 136;

			// Token: 0x0400155E RID: 5470
			internal const int cmdidObjectVerbList0 = 137;

			// Token: 0x0400155F RID: 5471
			internal const int cmdidObjectVerbList1 = 138;

			// Token: 0x04001560 RID: 5472
			internal const int cmdidObjectVerbList2 = 139;

			// Token: 0x04001561 RID: 5473
			internal const int cmdidObjectVerbList3 = 140;

			// Token: 0x04001562 RID: 5474
			internal const int cmdidObjectVerbList4 = 141;

			// Token: 0x04001563 RID: 5475
			internal const int cmdidObjectVerbList5 = 142;

			// Token: 0x04001564 RID: 5476
			internal const int cmdidObjectVerbList6 = 143;

			// Token: 0x04001565 RID: 5477
			internal const int cmdidObjectVerbList7 = 144;

			// Token: 0x04001566 RID: 5478
			internal const int cmdidObjectVerbList8 = 145;

			// Token: 0x04001567 RID: 5479
			internal const int cmdidObjectVerbList9 = 146;

			// Token: 0x04001568 RID: 5480
			internal const int cmdidConvertObject = 147;

			// Token: 0x04001569 RID: 5481
			internal const int cmdidCustomControl = 148;

			// Token: 0x0400156A RID: 5482
			internal const int cmdidCustomizeItem = 149;

			// Token: 0x0400156B RID: 5483
			internal const int cmdidRename = 150;

			// Token: 0x0400156C RID: 5484
			internal const int cmdidImport = 151;

			// Token: 0x0400156D RID: 5485
			internal const int cmdidNewPage = 152;

			// Token: 0x0400156E RID: 5486
			internal const int cmdidMove = 153;

			// Token: 0x0400156F RID: 5487
			internal const int cmdidCancel = 154;

			// Token: 0x04001570 RID: 5488
			internal const int cmdidFont = 155;

			// Token: 0x04001571 RID: 5489
			internal const int cmdidExpandLinks = 156;

			// Token: 0x04001572 RID: 5490
			internal const int cmdidExpandImages = 157;

			// Token: 0x04001573 RID: 5491
			internal const int cmdidExpandPages = 158;

			// Token: 0x04001574 RID: 5492
			internal const int cmdidRefocusDiagram = 159;

			// Token: 0x04001575 RID: 5493
			internal const int cmdidTransitiveClosure = 160;

			// Token: 0x04001576 RID: 5494
			internal const int cmdidCenterDiagram = 161;

			// Token: 0x04001577 RID: 5495
			internal const int cmdidZoomIn = 162;

			// Token: 0x04001578 RID: 5496
			internal const int cmdidZoomOut = 163;

			// Token: 0x04001579 RID: 5497
			internal const int cmdidRemoveFilter = 164;

			// Token: 0x0400157A RID: 5498
			internal const int cmdidHidePane = 165;

			// Token: 0x0400157B RID: 5499
			internal const int cmdidDeleteTable = 166;

			// Token: 0x0400157C RID: 5500
			internal const int cmdidDeleteRelationship = 167;

			// Token: 0x0400157D RID: 5501
			internal const int cmdidRemove = 168;

			// Token: 0x0400157E RID: 5502
			internal const int cmdidJoinLeftAll = 169;

			// Token: 0x0400157F RID: 5503
			internal const int cmdidJoinRightAll = 170;

			// Token: 0x04001580 RID: 5504
			internal const int cmdidAddToOutput = 171;

			// Token: 0x04001581 RID: 5505
			internal const int cmdidOtherQuery = 172;

			// Token: 0x04001582 RID: 5506
			internal const int cmdidGenerateChangeScript = 173;

			// Token: 0x04001583 RID: 5507
			internal const int cmdidSaveSelection = 174;

			// Token: 0x04001584 RID: 5508
			internal const int cmdidAutojoinCurrent = 175;

			// Token: 0x04001585 RID: 5509
			internal const int cmdidAutojoinAlways = 176;

			// Token: 0x04001586 RID: 5510
			internal const int cmdidEditPage = 177;

			// Token: 0x04001587 RID: 5511
			internal const int cmdidViewLinks = 178;

			// Token: 0x04001588 RID: 5512
			internal const int cmdidStop = 179;

			// Token: 0x04001589 RID: 5513
			internal const int cmdidPause = 180;

			// Token: 0x0400158A RID: 5514
			internal const int cmdidResume = 181;

			// Token: 0x0400158B RID: 5515
			internal const int cmdidFilterDiagram = 182;

			// Token: 0x0400158C RID: 5516
			internal const int cmdidShowAllObjects = 183;

			// Token: 0x0400158D RID: 5517
			internal const int cmdidShowApplications = 184;

			// Token: 0x0400158E RID: 5518
			internal const int cmdidShowOtherObjects = 185;

			// Token: 0x0400158F RID: 5519
			internal const int cmdidShowPrimRelationships = 186;

			// Token: 0x04001590 RID: 5520
			internal const int cmdidExpand = 187;

			// Token: 0x04001591 RID: 5521
			internal const int cmdidCollapse = 188;

			// Token: 0x04001592 RID: 5522
			internal const int cmdidRefresh = 189;

			// Token: 0x04001593 RID: 5523
			internal const int cmdidLayout = 190;

			// Token: 0x04001594 RID: 5524
			internal const int cmdidShowResources = 191;

			// Token: 0x04001595 RID: 5525
			internal const int cmdidInsertHTMLWizard = 192;

			// Token: 0x04001596 RID: 5526
			internal const int cmdidShowDownloads = 193;

			// Token: 0x04001597 RID: 5527
			internal const int cmdidShowExternals = 194;

			// Token: 0x04001598 RID: 5528
			internal const int cmdidShowInBoundLinks = 195;

			// Token: 0x04001599 RID: 5529
			internal const int cmdidShowOutBoundLinks = 196;

			// Token: 0x0400159A RID: 5530
			internal const int cmdidShowInAndOutBoundLinks = 197;

			// Token: 0x0400159B RID: 5531
			internal const int cmdidPreview = 198;

			// Token: 0x0400159C RID: 5532
			internal const int cmdidOpen = 261;

			// Token: 0x0400159D RID: 5533
			internal const int cmdidOpenWith = 199;

			// Token: 0x0400159E RID: 5534
			internal const int cmdidShowPages = 200;

			// Token: 0x0400159F RID: 5535
			internal const int cmdidRunQuery = 201;

			// Token: 0x040015A0 RID: 5536
			internal const int cmdidClearQuery = 202;

			// Token: 0x040015A1 RID: 5537
			internal const int cmdidRecordFirst = 203;

			// Token: 0x040015A2 RID: 5538
			internal const int cmdidRecordLast = 204;

			// Token: 0x040015A3 RID: 5539
			internal const int cmdidRecordNext = 205;

			// Token: 0x040015A4 RID: 5540
			internal const int cmdidRecordPrevious = 206;

			// Token: 0x040015A5 RID: 5541
			internal const int cmdidRecordGoto = 207;

			// Token: 0x040015A6 RID: 5542
			internal const int cmdidRecordNew = 208;

			// Token: 0x040015A7 RID: 5543
			internal const int cmdidInsertNewMenu = 209;

			// Token: 0x040015A8 RID: 5544
			internal const int cmdidInsertSeparator = 210;

			// Token: 0x040015A9 RID: 5545
			internal const int cmdidEditMenuNames = 211;

			// Token: 0x040015AA RID: 5546
			internal const int cmdidDebugExplorer = 212;

			// Token: 0x040015AB RID: 5547
			internal const int cmdidDebugProcesses = 213;

			// Token: 0x040015AC RID: 5548
			internal const int cmdidViewThreadsWindow = 214;

			// Token: 0x040015AD RID: 5549
			internal const int cmdidWindowUIList = 215;

			// Token: 0x040015AE RID: 5550
			internal const int cmdidNewProject = 216;

			// Token: 0x040015AF RID: 5551
			internal const int cmdidOpenProject = 217;

			// Token: 0x040015B0 RID: 5552
			internal const int cmdidOpenSolution = 218;

			// Token: 0x040015B1 RID: 5553
			internal const int cmdidCloseSolution = 219;

			// Token: 0x040015B2 RID: 5554
			internal const int cmdidFileNew = 221;

			// Token: 0x040015B3 RID: 5555
			internal const int cmdidFileOpen = 222;

			// Token: 0x040015B4 RID: 5556
			internal const int cmdidFileClose = 223;

			// Token: 0x040015B5 RID: 5557
			internal const int cmdidSaveSolution = 224;

			// Token: 0x040015B6 RID: 5558
			internal const int cmdidSaveSolutionAs = 225;

			// Token: 0x040015B7 RID: 5559
			internal const int cmdidSaveProjectItemAs = 226;

			// Token: 0x040015B8 RID: 5560
			internal const int cmdidPageSetup = 227;

			// Token: 0x040015B9 RID: 5561
			internal const int cmdidPrintPreview = 228;

			// Token: 0x040015BA RID: 5562
			internal const int cmdidExit = 229;

			// Token: 0x040015BB RID: 5563
			internal const int cmdidReplace = 230;

			// Token: 0x040015BC RID: 5564
			internal const int cmdidGoto = 231;

			// Token: 0x040015BD RID: 5565
			internal const int cmdidPropertyPages = 232;

			// Token: 0x040015BE RID: 5566
			internal const int cmdidFullScreen = 233;

			// Token: 0x040015BF RID: 5567
			internal const int cmdidProjectExplorer = 234;

			// Token: 0x040015C0 RID: 5568
			internal const int cmdidPropertiesWindow = 235;

			// Token: 0x040015C1 RID: 5569
			internal const int cmdidTaskListWindow = 236;

			// Token: 0x040015C2 RID: 5570
			internal const int cmdidOutputWindow = 237;

			// Token: 0x040015C3 RID: 5571
			internal const int cmdidObjectBrowser = 238;

			// Token: 0x040015C4 RID: 5572
			internal const int cmdidDocOutlineWindow = 239;

			// Token: 0x040015C5 RID: 5573
			internal const int cmdidImmediateWindow = 240;

			// Token: 0x040015C6 RID: 5574
			internal const int cmdidWatchWindow = 241;

			// Token: 0x040015C7 RID: 5575
			internal const int cmdidLocalsWindow = 242;

			// Token: 0x040015C8 RID: 5576
			internal const int cmdidCallStack = 243;

			// Token: 0x040015C9 RID: 5577
			internal const int cmdidAutosWindow = 747;

			// Token: 0x040015CA RID: 5578
			internal const int cmdidThisWindow = 748;

			// Token: 0x040015CB RID: 5579
			internal const int cmdidAddNewItem = 220;

			// Token: 0x040015CC RID: 5580
			internal const int cmdidAddExistingItem = 244;

			// Token: 0x040015CD RID: 5581
			internal const int cmdidNewFolder = 245;

			// Token: 0x040015CE RID: 5582
			internal const int cmdidSetStartupProject = 246;

			// Token: 0x040015CF RID: 5583
			internal const int cmdidProjectSettings = 247;

			// Token: 0x040015D0 RID: 5584
			internal const int cmdidProjectReferences = 367;

			// Token: 0x040015D1 RID: 5585
			internal const int cmdidStepInto = 248;

			// Token: 0x040015D2 RID: 5586
			internal const int cmdidStepOver = 249;

			// Token: 0x040015D3 RID: 5587
			internal const int cmdidStepOut = 250;

			// Token: 0x040015D4 RID: 5588
			internal const int cmdidRunToCursor = 251;

			// Token: 0x040015D5 RID: 5589
			internal const int cmdidAddWatch = 252;

			// Token: 0x040015D6 RID: 5590
			internal const int cmdidEditWatch = 253;

			// Token: 0x040015D7 RID: 5591
			internal const int cmdidQuickWatch = 254;

			// Token: 0x040015D8 RID: 5592
			internal const int cmdidToggleBreakpoint = 255;

			// Token: 0x040015D9 RID: 5593
			internal const int cmdidClearBreakpoints = 256;

			// Token: 0x040015DA RID: 5594
			internal const int cmdidShowBreakpoints = 257;

			// Token: 0x040015DB RID: 5595
			internal const int cmdidSetNextStatement = 258;

			// Token: 0x040015DC RID: 5596
			internal const int cmdidShowNextStatement = 259;

			// Token: 0x040015DD RID: 5597
			internal const int cmdidEditBreakpoint = 260;

			// Token: 0x040015DE RID: 5598
			internal const int cmdidDetachDebugger = 262;

			// Token: 0x040015DF RID: 5599
			internal const int cmdidCustomizeKeyboard = 263;

			// Token: 0x040015E0 RID: 5600
			internal const int cmdidToolsOptions = 264;

			// Token: 0x040015E1 RID: 5601
			internal const int cmdidNewWindow = 265;

			// Token: 0x040015E2 RID: 5602
			internal const int cmdidSplit = 266;

			// Token: 0x040015E3 RID: 5603
			internal const int cmdidCascade = 267;

			// Token: 0x040015E4 RID: 5604
			internal const int cmdidTileHorz = 268;

			// Token: 0x040015E5 RID: 5605
			internal const int cmdidTileVert = 269;

			// Token: 0x040015E6 RID: 5606
			internal const int cmdidTechSupport = 270;

			// Token: 0x040015E7 RID: 5607
			internal const int cmdidAbout = 271;

			// Token: 0x040015E8 RID: 5608
			internal const int cmdidDebugOptions = 272;

			// Token: 0x040015E9 RID: 5609
			internal const int cmdidDeleteWatch = 274;

			// Token: 0x040015EA RID: 5610
			internal const int cmdidCollapseWatch = 275;

			// Token: 0x040015EB RID: 5611
			internal const int cmdidPbrsToggleStatus = 282;

			// Token: 0x040015EC RID: 5612
			internal const int cmdidPropbrsHide = 283;

			// Token: 0x040015ED RID: 5613
			internal const int cmdidDockingView = 284;

			// Token: 0x040015EE RID: 5614
			internal const int cmdidHideActivePane = 285;

			// Token: 0x040015EF RID: 5615
			internal const int cmdidPaneNextTab = 286;

			// Token: 0x040015F0 RID: 5616
			internal const int cmdidPanePrevTab = 287;

			// Token: 0x040015F1 RID: 5617
			internal const int cmdidPaneCloseToolWindow = 288;

			// Token: 0x040015F2 RID: 5618
			internal const int cmdidPaneActivateDocWindow = 289;

			// Token: 0x040015F3 RID: 5619
			internal const int cmdidDockingViewFloater = 291;

			// Token: 0x040015F4 RID: 5620
			internal const int cmdidAutoHideWindow = 292;

			// Token: 0x040015F5 RID: 5621
			internal const int cmdidMoveToDropdownBar = 293;

			// Token: 0x040015F6 RID: 5622
			internal const int cmdidFindCmd = 294;

			// Token: 0x040015F7 RID: 5623
			internal const int cmdidStart = 295;

			// Token: 0x040015F8 RID: 5624
			internal const int cmdidRestart = 296;

			// Token: 0x040015F9 RID: 5625
			internal const int cmdidAddinManager = 297;

			// Token: 0x040015FA RID: 5626
			internal const int cmdidMultiLevelUndoList = 298;

			// Token: 0x040015FB RID: 5627
			internal const int cmdidMultiLevelRedoList = 299;

			// Token: 0x040015FC RID: 5628
			internal const int cmdidToolboxAddTab = 300;

			// Token: 0x040015FD RID: 5629
			internal const int cmdidToolboxDeleteTab = 301;

			// Token: 0x040015FE RID: 5630
			internal const int cmdidToolboxRenameTab = 302;

			// Token: 0x040015FF RID: 5631
			internal const int cmdidToolboxTabMoveUp = 303;

			// Token: 0x04001600 RID: 5632
			internal const int cmdidToolboxTabMoveDown = 304;

			// Token: 0x04001601 RID: 5633
			internal const int cmdidToolboxRenameItem = 305;

			// Token: 0x04001602 RID: 5634
			internal const int cmdidToolboxListView = 306;

			// Token: 0x04001603 RID: 5635
			internal const int cmdidWindowUIGetList = 308;

			// Token: 0x04001604 RID: 5636
			internal const int cmdidInsertValuesQuery = 309;

			// Token: 0x04001605 RID: 5637
			internal const int cmdidShowProperties = 310;

			// Token: 0x04001606 RID: 5638
			internal const int cmdidThreadSuspend = 311;

			// Token: 0x04001607 RID: 5639
			internal const int cmdidThreadResume = 312;

			// Token: 0x04001608 RID: 5640
			internal const int cmdidThreadSetFocus = 313;

			// Token: 0x04001609 RID: 5641
			internal const int cmdidDisplayRadix = 314;

			// Token: 0x0400160A RID: 5642
			internal const int cmdidOpenProjectItem = 315;

			// Token: 0x0400160B RID: 5643
			internal const int cmdidPaneNextPane = 316;

			// Token: 0x0400160C RID: 5644
			internal const int cmdidPanePrevPane = 317;

			// Token: 0x0400160D RID: 5645
			internal const int cmdidClearPane = 318;

			// Token: 0x0400160E RID: 5646
			internal const int cmdidGotoErrorTag = 319;

			// Token: 0x0400160F RID: 5647
			internal const int cmdidTaskListSortByCategory = 320;

			// Token: 0x04001610 RID: 5648
			internal const int cmdidTaskListSortByFileLine = 321;

			// Token: 0x04001611 RID: 5649
			internal const int cmdidTaskListSortByPriority = 322;

			// Token: 0x04001612 RID: 5650
			internal const int cmdidTaskListSortByDefaultSort = 323;

			// Token: 0x04001613 RID: 5651
			internal const int cmdidTaskListFilterByNothing = 325;

			// Token: 0x04001614 RID: 5652
			internal const int cmdidTaskListFilterByCategoryCodeSense = 326;

			// Token: 0x04001615 RID: 5653
			internal const int cmdidTaskListFilterByCategoryCompiler = 327;

			// Token: 0x04001616 RID: 5654
			internal const int cmdidTaskListFilterByCategoryComment = 328;

			// Token: 0x04001617 RID: 5655
			internal const int cmdidToolboxAddItem = 329;

			// Token: 0x04001618 RID: 5656
			internal const int cmdidToolboxReset = 330;

			// Token: 0x04001619 RID: 5657
			internal const int cmdidSaveProjectItem = 331;

			// Token: 0x0400161A RID: 5658
			internal const int cmdidViewForm = 332;

			// Token: 0x0400161B RID: 5659
			internal const int cmdidViewCode = 333;

			// Token: 0x0400161C RID: 5660
			internal const int cmdidPreviewInBrowser = 334;

			// Token: 0x0400161D RID: 5661
			internal const int cmdidBrowseWith = 336;

			// Token: 0x0400161E RID: 5662
			internal const int cmdidSearchSetCombo = 307;

			// Token: 0x0400161F RID: 5663
			internal const int cmdidSearchCombo = 337;

			// Token: 0x04001620 RID: 5664
			internal const int cmdidEditLabel = 338;

			// Token: 0x04001621 RID: 5665
			internal const int cmdidExceptions = 339;

			// Token: 0x04001622 RID: 5666
			internal const int cmdidToggleSelMode = 341;

			// Token: 0x04001623 RID: 5667
			internal const int cmdidToggleInsMode = 342;

			// Token: 0x04001624 RID: 5668
			internal const int cmdidLoadUnloadedProject = 343;

			// Token: 0x04001625 RID: 5669
			internal const int cmdidUnloadLoadedProject = 344;

			// Token: 0x04001626 RID: 5670
			internal const int cmdidElasticColumn = 345;

			// Token: 0x04001627 RID: 5671
			internal const int cmdidHideColumn = 346;

			// Token: 0x04001628 RID: 5672
			internal const int cmdidTaskListPreviousView = 347;

			// Token: 0x04001629 RID: 5673
			internal const int cmdidZoomDialog = 348;

			// Token: 0x0400162A RID: 5674
			internal const int cmdidFindNew = 349;

			// Token: 0x0400162B RID: 5675
			internal const int cmdidFindMatchCase = 350;

			// Token: 0x0400162C RID: 5676
			internal const int cmdidFindWholeWord = 351;

			// Token: 0x0400162D RID: 5677
			internal const int cmdidFindSimplePattern = 276;

			// Token: 0x0400162E RID: 5678
			internal const int cmdidFindRegularExpression = 352;

			// Token: 0x0400162F RID: 5679
			internal const int cmdidFindBackwards = 353;

			// Token: 0x04001630 RID: 5680
			internal const int cmdidFindInSelection = 354;

			// Token: 0x04001631 RID: 5681
			internal const int cmdidFindStop = 355;

			// Token: 0x04001632 RID: 5682
			internal const int cmdidFindHelp = 356;

			// Token: 0x04001633 RID: 5683
			internal const int cmdidFindInFiles = 277;

			// Token: 0x04001634 RID: 5684
			internal const int cmdidReplaceInFiles = 278;

			// Token: 0x04001635 RID: 5685
			internal const int cmdidNextLocation = 279;

			// Token: 0x04001636 RID: 5686
			internal const int cmdidPreviousLocation = 280;

			// Token: 0x04001637 RID: 5687
			internal const int cmdidTaskListNextError = 357;

			// Token: 0x04001638 RID: 5688
			internal const int cmdidTaskListPrevError = 358;

			// Token: 0x04001639 RID: 5689
			internal const int cmdidTaskListFilterByCategoryUser = 359;

			// Token: 0x0400163A RID: 5690
			internal const int cmdidTaskListFilterByCategoryShortcut = 360;

			// Token: 0x0400163B RID: 5691
			internal const int cmdidTaskListFilterByCategoryHTML = 361;

			// Token: 0x0400163C RID: 5692
			internal const int cmdidTaskListFilterByCurrentFile = 362;

			// Token: 0x0400163D RID: 5693
			internal const int cmdidTaskListFilterByChecked = 363;

			// Token: 0x0400163E RID: 5694
			internal const int cmdidTaskListFilterByUnchecked = 364;

			// Token: 0x0400163F RID: 5695
			internal const int cmdidTaskListSortByDescription = 365;

			// Token: 0x04001640 RID: 5696
			internal const int cmdidTaskListSortByChecked = 366;

			// Token: 0x04001641 RID: 5697
			internal const int cmdidStartNoDebug = 368;

			// Token: 0x04001642 RID: 5698
			internal const int cmdidFindNext = 370;

			// Token: 0x04001643 RID: 5699
			internal const int cmdidFindPrev = 371;

			// Token: 0x04001644 RID: 5700
			internal const int cmdidFindSelectedNext = 372;

			// Token: 0x04001645 RID: 5701
			internal const int cmdidFindSelectedPrev = 373;

			// Token: 0x04001646 RID: 5702
			internal const int cmdidSearchGetList = 374;

			// Token: 0x04001647 RID: 5703
			internal const int cmdidInsertBreakpoint = 375;

			// Token: 0x04001648 RID: 5704
			internal const int cmdidEnableBreakpoint = 376;

			// Token: 0x04001649 RID: 5705
			internal const int cmdidF1Help = 377;

			// Token: 0x0400164A RID: 5706
			internal const int cmdidPropSheetOrProperties = 397;

			// Token: 0x0400164B RID: 5707
			internal const int cmdidTshellStep = 398;

			// Token: 0x0400164C RID: 5708
			internal const int cmdidTshellRun = 399;

			// Token: 0x0400164D RID: 5709
			internal const int cmdidMarkerCmd0 = 400;

			// Token: 0x0400164E RID: 5710
			internal const int cmdidMarkerCmd1 = 401;

			// Token: 0x0400164F RID: 5711
			internal const int cmdidMarkerCmd2 = 402;

			// Token: 0x04001650 RID: 5712
			internal const int cmdidMarkerCmd3 = 403;

			// Token: 0x04001651 RID: 5713
			internal const int cmdidMarkerCmd4 = 404;

			// Token: 0x04001652 RID: 5714
			internal const int cmdidMarkerCmd5 = 405;

			// Token: 0x04001653 RID: 5715
			internal const int cmdidMarkerCmd6 = 406;

			// Token: 0x04001654 RID: 5716
			internal const int cmdidMarkerCmd7 = 407;

			// Token: 0x04001655 RID: 5717
			internal const int cmdidMarkerCmd8 = 408;

			// Token: 0x04001656 RID: 5718
			internal const int cmdidMarkerCmd9 = 409;

			// Token: 0x04001657 RID: 5719
			internal const int cmdidMarkerLast = 409;

			// Token: 0x04001658 RID: 5720
			internal const int cmdidMarkerEnd = 410;

			// Token: 0x04001659 RID: 5721
			internal const int cmdidReloadProject = 412;

			// Token: 0x0400165A RID: 5722
			internal const int cmdidUnloadProject = 413;

			// Token: 0x0400165B RID: 5723
			internal const int cmdidDetachAttachOutline = 420;

			// Token: 0x0400165C RID: 5724
			internal const int cmdidShowHideOutline = 421;

			// Token: 0x0400165D RID: 5725
			internal const int cmdidSyncOutline = 422;

			// Token: 0x0400165E RID: 5726
			internal const int cmdidRunToCallstCursor = 423;

			// Token: 0x0400165F RID: 5727
			internal const int cmdidNoCmdsAvailable = 424;

			// Token: 0x04001660 RID: 5728
			internal const int cmdidContextWindow = 427;

			// Token: 0x04001661 RID: 5729
			internal const int cmdidAlias = 428;

			// Token: 0x04001662 RID: 5730
			internal const int cmdidGotoCommandLine = 429;

			// Token: 0x04001663 RID: 5731
			internal const int cmdidEvaluateExpression = 430;

			// Token: 0x04001664 RID: 5732
			internal const int cmdidImmediateMode = 431;

			// Token: 0x04001665 RID: 5733
			internal const int cmdidEvaluateStatement = 432;

			// Token: 0x04001666 RID: 5734
			internal const int cmdidFindResultWindow1 = 433;

			// Token: 0x04001667 RID: 5735
			internal const int cmdidFindResultWindow2 = 434;

			// Token: 0x04001668 RID: 5736
			internal const int cmdidWindow1 = 570;

			// Token: 0x04001669 RID: 5737
			internal const int cmdidWindow2 = 571;

			// Token: 0x0400166A RID: 5738
			internal const int cmdidWindow3 = 572;

			// Token: 0x0400166B RID: 5739
			internal const int cmdidWindow4 = 573;

			// Token: 0x0400166C RID: 5740
			internal const int cmdidWindow5 = 574;

			// Token: 0x0400166D RID: 5741
			internal const int cmdidWindow6 = 575;

			// Token: 0x0400166E RID: 5742
			internal const int cmdidWindow7 = 576;

			// Token: 0x0400166F RID: 5743
			internal const int cmdidWindow8 = 577;

			// Token: 0x04001670 RID: 5744
			internal const int cmdidWindow9 = 578;

			// Token: 0x04001671 RID: 5745
			internal const int cmdidWindow10 = 579;

			// Token: 0x04001672 RID: 5746
			internal const int cmdidWindow11 = 580;

			// Token: 0x04001673 RID: 5747
			internal const int cmdidWindow12 = 581;

			// Token: 0x04001674 RID: 5748
			internal const int cmdidWindow13 = 582;

			// Token: 0x04001675 RID: 5749
			internal const int cmdidWindow14 = 583;

			// Token: 0x04001676 RID: 5750
			internal const int cmdidWindow15 = 584;

			// Token: 0x04001677 RID: 5751
			internal const int cmdidWindow16 = 585;

			// Token: 0x04001678 RID: 5752
			internal const int cmdidWindow17 = 586;

			// Token: 0x04001679 RID: 5753
			internal const int cmdidWindow18 = 587;

			// Token: 0x0400167A RID: 5754
			internal const int cmdidWindow19 = 588;

			// Token: 0x0400167B RID: 5755
			internal const int cmdidWindow20 = 589;

			// Token: 0x0400167C RID: 5756
			internal const int cmdidWindow21 = 590;

			// Token: 0x0400167D RID: 5757
			internal const int cmdidWindow22 = 591;

			// Token: 0x0400167E RID: 5758
			internal const int cmdidWindow23 = 592;

			// Token: 0x0400167F RID: 5759
			internal const int cmdidWindow24 = 593;

			// Token: 0x04001680 RID: 5760
			internal const int cmdidWindow25 = 594;

			// Token: 0x04001681 RID: 5761
			internal const int cmdidMoreWindows = 595;

			// Token: 0x04001682 RID: 5762
			internal const int cmdidTaskListTaskHelp = 598;

			// Token: 0x04001683 RID: 5763
			internal const int cmdidClassView = 599;

			// Token: 0x04001684 RID: 5764
			internal const int cmdidMRUProj1 = 600;

			// Token: 0x04001685 RID: 5765
			internal const int cmdidMRUProj2 = 601;

			// Token: 0x04001686 RID: 5766
			internal const int cmdidMRUProj3 = 602;

			// Token: 0x04001687 RID: 5767
			internal const int cmdidMRUProj4 = 603;

			// Token: 0x04001688 RID: 5768
			internal const int cmdidMRUProj5 = 604;

			// Token: 0x04001689 RID: 5769
			internal const int cmdidMRUProj6 = 605;

			// Token: 0x0400168A RID: 5770
			internal const int cmdidMRUProj7 = 606;

			// Token: 0x0400168B RID: 5771
			internal const int cmdidMRUProj8 = 607;

			// Token: 0x0400168C RID: 5772
			internal const int cmdidMRUProj9 = 608;

			// Token: 0x0400168D RID: 5773
			internal const int cmdidMRUProj10 = 609;

			// Token: 0x0400168E RID: 5774
			internal const int cmdidMRUProj11 = 610;

			// Token: 0x0400168F RID: 5775
			internal const int cmdidMRUProj12 = 611;

			// Token: 0x04001690 RID: 5776
			internal const int cmdidMRUProj13 = 612;

			// Token: 0x04001691 RID: 5777
			internal const int cmdidMRUProj14 = 613;

			// Token: 0x04001692 RID: 5778
			internal const int cmdidMRUProj15 = 614;

			// Token: 0x04001693 RID: 5779
			internal const int cmdidMRUProj16 = 615;

			// Token: 0x04001694 RID: 5780
			internal const int cmdidMRUProj17 = 616;

			// Token: 0x04001695 RID: 5781
			internal const int cmdidMRUProj18 = 617;

			// Token: 0x04001696 RID: 5782
			internal const int cmdidMRUProj19 = 618;

			// Token: 0x04001697 RID: 5783
			internal const int cmdidMRUProj20 = 619;

			// Token: 0x04001698 RID: 5784
			internal const int cmdidMRUProj21 = 620;

			// Token: 0x04001699 RID: 5785
			internal const int cmdidMRUProj22 = 621;

			// Token: 0x0400169A RID: 5786
			internal const int cmdidMRUProj23 = 622;

			// Token: 0x0400169B RID: 5787
			internal const int cmdidMRUProj24 = 623;

			// Token: 0x0400169C RID: 5788
			internal const int cmdidMRUProj25 = 624;

			// Token: 0x0400169D RID: 5789
			internal const int cmdidSplitNext = 625;

			// Token: 0x0400169E RID: 5790
			internal const int cmdidSplitPrev = 626;

			// Token: 0x0400169F RID: 5791
			internal const int cmdidCloseAllDocuments = 627;

			// Token: 0x040016A0 RID: 5792
			internal const int cmdidNextDocument = 628;

			// Token: 0x040016A1 RID: 5793
			internal const int cmdidPrevDocument = 629;

			// Token: 0x040016A2 RID: 5794
			internal const int cmdidTool1 = 630;

			// Token: 0x040016A3 RID: 5795
			internal const int cmdidTool2 = 631;

			// Token: 0x040016A4 RID: 5796
			internal const int cmdidTool3 = 632;

			// Token: 0x040016A5 RID: 5797
			internal const int cmdidTool4 = 633;

			// Token: 0x040016A6 RID: 5798
			internal const int cmdidTool5 = 634;

			// Token: 0x040016A7 RID: 5799
			internal const int cmdidTool6 = 635;

			// Token: 0x040016A8 RID: 5800
			internal const int cmdidTool7 = 636;

			// Token: 0x040016A9 RID: 5801
			internal const int cmdidTool8 = 637;

			// Token: 0x040016AA RID: 5802
			internal const int cmdidTool9 = 638;

			// Token: 0x040016AB RID: 5803
			internal const int cmdidTool10 = 639;

			// Token: 0x040016AC RID: 5804
			internal const int cmdidTool11 = 640;

			// Token: 0x040016AD RID: 5805
			internal const int cmdidTool12 = 641;

			// Token: 0x040016AE RID: 5806
			internal const int cmdidTool13 = 642;

			// Token: 0x040016AF RID: 5807
			internal const int cmdidTool14 = 643;

			// Token: 0x040016B0 RID: 5808
			internal const int cmdidTool15 = 644;

			// Token: 0x040016B1 RID: 5809
			internal const int cmdidTool16 = 645;

			// Token: 0x040016B2 RID: 5810
			internal const int cmdidTool17 = 646;

			// Token: 0x040016B3 RID: 5811
			internal const int cmdidTool18 = 647;

			// Token: 0x040016B4 RID: 5812
			internal const int cmdidTool19 = 648;

			// Token: 0x040016B5 RID: 5813
			internal const int cmdidTool20 = 649;

			// Token: 0x040016B6 RID: 5814
			internal const int cmdidTool21 = 650;

			// Token: 0x040016B7 RID: 5815
			internal const int cmdidTool22 = 651;

			// Token: 0x040016B8 RID: 5816
			internal const int cmdidTool23 = 652;

			// Token: 0x040016B9 RID: 5817
			internal const int cmdidTool24 = 653;

			// Token: 0x040016BA RID: 5818
			internal const int cmdidExternalCommands = 654;

			// Token: 0x040016BB RID: 5819
			internal const int cmdidPasteNextTBXCBItem = 655;

			// Token: 0x040016BC RID: 5820
			internal const int cmdidToolboxShowAllTabs = 656;

			// Token: 0x040016BD RID: 5821
			internal const int cmdidProjectDependencies = 657;

			// Token: 0x040016BE RID: 5822
			internal const int cmdidCloseDocument = 658;

			// Token: 0x040016BF RID: 5823
			internal const int cmdidToolboxSortItems = 659;

			// Token: 0x040016C0 RID: 5824
			internal const int cmdidViewBarView1 = 660;

			// Token: 0x040016C1 RID: 5825
			internal const int cmdidViewBarView2 = 661;

			// Token: 0x040016C2 RID: 5826
			internal const int cmdidViewBarView3 = 662;

			// Token: 0x040016C3 RID: 5827
			internal const int cmdidViewBarView4 = 663;

			// Token: 0x040016C4 RID: 5828
			internal const int cmdidViewBarView5 = 664;

			// Token: 0x040016C5 RID: 5829
			internal const int cmdidViewBarView6 = 665;

			// Token: 0x040016C6 RID: 5830
			internal const int cmdidViewBarView7 = 666;

			// Token: 0x040016C7 RID: 5831
			internal const int cmdidViewBarView8 = 667;

			// Token: 0x040016C8 RID: 5832
			internal const int cmdidViewBarView9 = 668;

			// Token: 0x040016C9 RID: 5833
			internal const int cmdidViewBarView10 = 669;

			// Token: 0x040016CA RID: 5834
			internal const int cmdidViewBarView11 = 670;

			// Token: 0x040016CB RID: 5835
			internal const int cmdidViewBarView12 = 671;

			// Token: 0x040016CC RID: 5836
			internal const int cmdidViewBarView13 = 672;

			// Token: 0x040016CD RID: 5837
			internal const int cmdidViewBarView14 = 673;

			// Token: 0x040016CE RID: 5838
			internal const int cmdidViewBarView15 = 674;

			// Token: 0x040016CF RID: 5839
			internal const int cmdidViewBarView16 = 675;

			// Token: 0x040016D0 RID: 5840
			internal const int cmdidViewBarView17 = 676;

			// Token: 0x040016D1 RID: 5841
			internal const int cmdidViewBarView18 = 677;

			// Token: 0x040016D2 RID: 5842
			internal const int cmdidViewBarView19 = 678;

			// Token: 0x040016D3 RID: 5843
			internal const int cmdidViewBarView20 = 679;

			// Token: 0x040016D4 RID: 5844
			internal const int cmdidViewBarView21 = 680;

			// Token: 0x040016D5 RID: 5845
			internal const int cmdidViewBarView22 = 681;

			// Token: 0x040016D6 RID: 5846
			internal const int cmdidViewBarView23 = 682;

			// Token: 0x040016D7 RID: 5847
			internal const int cmdidViewBarView24 = 683;

			// Token: 0x040016D8 RID: 5848
			internal const int cmdidSolutionCfg = 684;

			// Token: 0x040016D9 RID: 5849
			internal const int cmdidSolutionCfgGetList = 685;

			// Token: 0x040016DA RID: 5850
			internal const int cmdidManageIndexes = 675;

			// Token: 0x040016DB RID: 5851
			internal const int cmdidManageRelationships = 676;

			// Token: 0x040016DC RID: 5852
			internal const int cmdidManageConstraints = 677;

			// Token: 0x040016DD RID: 5853
			internal const int cmdidTaskListCustomView1 = 678;

			// Token: 0x040016DE RID: 5854
			internal const int cmdidTaskListCustomView2 = 679;

			// Token: 0x040016DF RID: 5855
			internal const int cmdidTaskListCustomView3 = 680;

			// Token: 0x040016E0 RID: 5856
			internal const int cmdidTaskListCustomView4 = 681;

			// Token: 0x040016E1 RID: 5857
			internal const int cmdidTaskListCustomView5 = 682;

			// Token: 0x040016E2 RID: 5858
			internal const int cmdidTaskListCustomView6 = 683;

			// Token: 0x040016E3 RID: 5859
			internal const int cmdidTaskListCustomView7 = 684;

			// Token: 0x040016E4 RID: 5860
			internal const int cmdidTaskListCustomView8 = 685;

			// Token: 0x040016E5 RID: 5861
			internal const int cmdidTaskListCustomView9 = 686;

			// Token: 0x040016E6 RID: 5862
			internal const int cmdidTaskListCustomView10 = 687;

			// Token: 0x040016E7 RID: 5863
			internal const int cmdidTaskListCustomView11 = 688;

			// Token: 0x040016E8 RID: 5864
			internal const int cmdidTaskListCustomView12 = 689;

			// Token: 0x040016E9 RID: 5865
			internal const int cmdidTaskListCustomView13 = 690;

			// Token: 0x040016EA RID: 5866
			internal const int cmdidTaskListCustomView14 = 691;

			// Token: 0x040016EB RID: 5867
			internal const int cmdidTaskListCustomView15 = 692;

			// Token: 0x040016EC RID: 5868
			internal const int cmdidTaskListCustomView16 = 693;

			// Token: 0x040016ED RID: 5869
			internal const int cmdidTaskListCustomView17 = 694;

			// Token: 0x040016EE RID: 5870
			internal const int cmdidTaskListCustomView18 = 695;

			// Token: 0x040016EF RID: 5871
			internal const int cmdidTaskListCustomView19 = 696;

			// Token: 0x040016F0 RID: 5872
			internal const int cmdidTaskListCustomView20 = 697;

			// Token: 0x040016F1 RID: 5873
			internal const int cmdidTaskListCustomView21 = 698;

			// Token: 0x040016F2 RID: 5874
			internal const int cmdidTaskListCustomView22 = 699;

			// Token: 0x040016F3 RID: 5875
			internal const int cmdidTaskListCustomView23 = 700;

			// Token: 0x040016F4 RID: 5876
			internal const int cmdidTaskListCustomView24 = 701;

			// Token: 0x040016F5 RID: 5877
			internal const int cmdidTaskListCustomView25 = 702;

			// Token: 0x040016F6 RID: 5878
			internal const int cmdidTaskListCustomView26 = 703;

			// Token: 0x040016F7 RID: 5879
			internal const int cmdidTaskListCustomView27 = 704;

			// Token: 0x040016F8 RID: 5880
			internal const int cmdidTaskListCustomView28 = 705;

			// Token: 0x040016F9 RID: 5881
			internal const int cmdidTaskListCustomView29 = 706;

			// Token: 0x040016FA RID: 5882
			internal const int cmdidTaskListCustomView30 = 707;

			// Token: 0x040016FB RID: 5883
			internal const int cmdidTaskListCustomView31 = 708;

			// Token: 0x040016FC RID: 5884
			internal const int cmdidTaskListCustomView32 = 709;

			// Token: 0x040016FD RID: 5885
			internal const int cmdidTaskListCustomView33 = 710;

			// Token: 0x040016FE RID: 5886
			internal const int cmdidTaskListCustomView34 = 711;

			// Token: 0x040016FF RID: 5887
			internal const int cmdidTaskListCustomView35 = 712;

			// Token: 0x04001700 RID: 5888
			internal const int cmdidTaskListCustomView36 = 713;

			// Token: 0x04001701 RID: 5889
			internal const int cmdidTaskListCustomView37 = 714;

			// Token: 0x04001702 RID: 5890
			internal const int cmdidTaskListCustomView38 = 715;

			// Token: 0x04001703 RID: 5891
			internal const int cmdidTaskListCustomView39 = 716;

			// Token: 0x04001704 RID: 5892
			internal const int cmdidTaskListCustomView40 = 717;

			// Token: 0x04001705 RID: 5893
			internal const int cmdidTaskListCustomView41 = 718;

			// Token: 0x04001706 RID: 5894
			internal const int cmdidTaskListCustomView42 = 719;

			// Token: 0x04001707 RID: 5895
			internal const int cmdidTaskListCustomView43 = 720;

			// Token: 0x04001708 RID: 5896
			internal const int cmdidTaskListCustomView44 = 721;

			// Token: 0x04001709 RID: 5897
			internal const int cmdidTaskListCustomView45 = 722;

			// Token: 0x0400170A RID: 5898
			internal const int cmdidTaskListCustomView46 = 723;

			// Token: 0x0400170B RID: 5899
			internal const int cmdidTaskListCustomView47 = 724;

			// Token: 0x0400170C RID: 5900
			internal const int cmdidTaskListCustomView48 = 725;

			// Token: 0x0400170D RID: 5901
			internal const int cmdidTaskListCustomView49 = 726;

			// Token: 0x0400170E RID: 5902
			internal const int cmdidTaskListCustomView50 = 727;

			// Token: 0x0400170F RID: 5903
			internal const int cmdidObjectSearch = 728;

			// Token: 0x04001710 RID: 5904
			internal const int cmdidCommandWindow = 729;

			// Token: 0x04001711 RID: 5905
			internal const int cmdidCommandWindowMarkMode = 730;

			// Token: 0x04001712 RID: 5906
			internal const int cmdidLogCommandWindow = 731;

			// Token: 0x04001713 RID: 5907
			internal const int cmdidShell = 732;

			// Token: 0x04001714 RID: 5908
			internal const int cmdidSingleChar = 733;

			// Token: 0x04001715 RID: 5909
			internal const int cmdidZeroOrMore = 734;

			// Token: 0x04001716 RID: 5910
			internal const int cmdidOneOrMore = 735;

			// Token: 0x04001717 RID: 5911
			internal const int cmdidBeginLine = 736;

			// Token: 0x04001718 RID: 5912
			internal const int cmdidEndLine = 737;

			// Token: 0x04001719 RID: 5913
			internal const int cmdidBeginWord = 738;

			// Token: 0x0400171A RID: 5914
			internal const int cmdidEndWord = 739;

			// Token: 0x0400171B RID: 5915
			internal const int cmdidCharInSet = 740;

			// Token: 0x0400171C RID: 5916
			internal const int cmdidCharNotInSet = 741;

			// Token: 0x0400171D RID: 5917
			internal const int cmdidOr = 742;

			// Token: 0x0400171E RID: 5918
			internal const int cmdidEscape = 743;

			// Token: 0x0400171F RID: 5919
			internal const int cmdidTagExp = 744;

			// Token: 0x04001720 RID: 5920
			internal const int cmdidPatternMatchHelp = 745;

			// Token: 0x04001721 RID: 5921
			internal const int cmdidRegExList = 746;

			// Token: 0x04001722 RID: 5922
			internal const int cmdidDebugReserved1 = 747;

			// Token: 0x04001723 RID: 5923
			internal const int cmdidDebugReserved2 = 748;

			// Token: 0x04001724 RID: 5924
			internal const int cmdidDebugReserved3 = 749;

			// Token: 0x04001725 RID: 5925
			internal const int cmdidWildZeroOrMore = 754;

			// Token: 0x04001726 RID: 5926
			internal const int cmdidWildSingleChar = 755;

			// Token: 0x04001727 RID: 5927
			internal const int cmdidWildSingleDigit = 756;

			// Token: 0x04001728 RID: 5928
			internal const int cmdidWildCharInSet = 757;

			// Token: 0x04001729 RID: 5929
			internal const int cmdidWildCharNotInSet = 758;

			// Token: 0x0400172A RID: 5930
			internal const int cmdidFindWhatText = 759;

			// Token: 0x0400172B RID: 5931
			internal const int cmdidTaggedExp1 = 760;

			// Token: 0x0400172C RID: 5932
			internal const int cmdidTaggedExp2 = 761;

			// Token: 0x0400172D RID: 5933
			internal const int cmdidTaggedExp3 = 762;

			// Token: 0x0400172E RID: 5934
			internal const int cmdidTaggedExp4 = 763;

			// Token: 0x0400172F RID: 5935
			internal const int cmdidTaggedExp5 = 764;

			// Token: 0x04001730 RID: 5936
			internal const int cmdidTaggedExp6 = 765;

			// Token: 0x04001731 RID: 5937
			internal const int cmdidTaggedExp7 = 766;

			// Token: 0x04001732 RID: 5938
			internal const int cmdidTaggedExp8 = 767;

			// Token: 0x04001733 RID: 5939
			internal const int cmdidTaggedExp9 = 768;

			// Token: 0x04001734 RID: 5940
			internal const int cmdidEditorWidgetClick = 769;

			// Token: 0x04001735 RID: 5941
			internal const int cmdidCmdWinUpdateAC = 770;

			// Token: 0x04001736 RID: 5942
			internal const int cmdidSlnCfgMgr = 771;

			// Token: 0x04001737 RID: 5943
			internal const int cmdidAddNewProject = 772;

			// Token: 0x04001738 RID: 5944
			internal const int cmdidAddExistingProject = 773;

			// Token: 0x04001739 RID: 5945
			internal const int cmdidAddNewSolutionItem = 774;

			// Token: 0x0400173A RID: 5946
			internal const int cmdidAddExistingSolutionItem = 775;

			// Token: 0x0400173B RID: 5947
			internal const int cmdidAutoHideContext1 = 776;

			// Token: 0x0400173C RID: 5948
			internal const int cmdidAutoHideContext2 = 777;

			// Token: 0x0400173D RID: 5949
			internal const int cmdidAutoHideContext3 = 778;

			// Token: 0x0400173E RID: 5950
			internal const int cmdidAutoHideContext4 = 779;

			// Token: 0x0400173F RID: 5951
			internal const int cmdidAutoHideContext5 = 780;

			// Token: 0x04001740 RID: 5952
			internal const int cmdidAutoHideContext6 = 781;

			// Token: 0x04001741 RID: 5953
			internal const int cmdidAutoHideContext7 = 782;

			// Token: 0x04001742 RID: 5954
			internal const int cmdidAutoHideContext8 = 783;

			// Token: 0x04001743 RID: 5955
			internal const int cmdidAutoHideContext9 = 784;

			// Token: 0x04001744 RID: 5956
			internal const int cmdidAutoHideContext10 = 785;

			// Token: 0x04001745 RID: 5957
			internal const int cmdidAutoHideContext11 = 786;

			// Token: 0x04001746 RID: 5958
			internal const int cmdidAutoHideContext12 = 787;

			// Token: 0x04001747 RID: 5959
			internal const int cmdidAutoHideContext13 = 788;

			// Token: 0x04001748 RID: 5960
			internal const int cmdidAutoHideContext14 = 789;

			// Token: 0x04001749 RID: 5961
			internal const int cmdidAutoHideContext15 = 790;

			// Token: 0x0400174A RID: 5962
			internal const int cmdidAutoHideContext16 = 791;

			// Token: 0x0400174B RID: 5963
			internal const int cmdidAutoHideContext17 = 792;

			// Token: 0x0400174C RID: 5964
			internal const int cmdidAutoHideContext18 = 793;

			// Token: 0x0400174D RID: 5965
			internal const int cmdidAutoHideContext19 = 794;

			// Token: 0x0400174E RID: 5966
			internal const int cmdidAutoHideContext20 = 795;

			// Token: 0x0400174F RID: 5967
			internal const int cmdidAutoHideContext21 = 796;

			// Token: 0x04001750 RID: 5968
			internal const int cmdidAutoHideContext22 = 797;

			// Token: 0x04001751 RID: 5969
			internal const int cmdidAutoHideContext23 = 798;

			// Token: 0x04001752 RID: 5970
			internal const int cmdidAutoHideContext24 = 799;

			// Token: 0x04001753 RID: 5971
			internal const int cmdidAutoHideContext25 = 800;

			// Token: 0x04001754 RID: 5972
			internal const int cmdidAutoHideContext26 = 801;

			// Token: 0x04001755 RID: 5973
			internal const int cmdidAutoHideContext27 = 802;

			// Token: 0x04001756 RID: 5974
			internal const int cmdidAutoHideContext28 = 803;

			// Token: 0x04001757 RID: 5975
			internal const int cmdidAutoHideContext29 = 804;

			// Token: 0x04001758 RID: 5976
			internal const int cmdidAutoHideContext30 = 805;

			// Token: 0x04001759 RID: 5977
			internal const int cmdidAutoHideContext31 = 806;

			// Token: 0x0400175A RID: 5978
			internal const int cmdidAutoHideContext32 = 807;

			// Token: 0x0400175B RID: 5979
			internal const int cmdidAutoHideContext33 = 808;

			// Token: 0x0400175C RID: 5980
			internal const int cmdidShellNavBackward = 809;

			// Token: 0x0400175D RID: 5981
			internal const int cmdidShellNavForward = 810;

			// Token: 0x0400175E RID: 5982
			internal const int cmdidShellNavigate1 = 811;

			// Token: 0x0400175F RID: 5983
			internal const int cmdidShellNavigate2 = 812;

			// Token: 0x04001760 RID: 5984
			internal const int cmdidShellNavigate3 = 813;

			// Token: 0x04001761 RID: 5985
			internal const int cmdidShellNavigate4 = 814;

			// Token: 0x04001762 RID: 5986
			internal const int cmdidShellNavigate5 = 815;

			// Token: 0x04001763 RID: 5987
			internal const int cmdidShellNavigate6 = 816;

			// Token: 0x04001764 RID: 5988
			internal const int cmdidShellNavigate7 = 817;

			// Token: 0x04001765 RID: 5989
			internal const int cmdidShellNavigate8 = 818;

			// Token: 0x04001766 RID: 5990
			internal const int cmdidShellNavigate9 = 819;

			// Token: 0x04001767 RID: 5991
			internal const int cmdidShellNavigate10 = 820;

			// Token: 0x04001768 RID: 5992
			internal const int cmdidShellNavigate11 = 821;

			// Token: 0x04001769 RID: 5993
			internal const int cmdidShellNavigate12 = 822;

			// Token: 0x0400176A RID: 5994
			internal const int cmdidShellNavigate13 = 823;

			// Token: 0x0400176B RID: 5995
			internal const int cmdidShellNavigate14 = 824;

			// Token: 0x0400176C RID: 5996
			internal const int cmdidShellNavigate15 = 825;

			// Token: 0x0400176D RID: 5997
			internal const int cmdidShellNavigate16 = 826;

			// Token: 0x0400176E RID: 5998
			internal const int cmdidShellNavigate17 = 827;

			// Token: 0x0400176F RID: 5999
			internal const int cmdidShellNavigate18 = 828;

			// Token: 0x04001770 RID: 6000
			internal const int cmdidShellNavigate19 = 829;

			// Token: 0x04001771 RID: 6001
			internal const int cmdidShellNavigate20 = 830;

			// Token: 0x04001772 RID: 6002
			internal const int cmdidShellNavigate21 = 831;

			// Token: 0x04001773 RID: 6003
			internal const int cmdidShellNavigate22 = 832;

			// Token: 0x04001774 RID: 6004
			internal const int cmdidShellNavigate23 = 833;

			// Token: 0x04001775 RID: 6005
			internal const int cmdidShellNavigate24 = 834;

			// Token: 0x04001776 RID: 6006
			internal const int cmdidShellNavigate25 = 835;

			// Token: 0x04001777 RID: 6007
			internal const int cmdidShellNavigate26 = 836;

			// Token: 0x04001778 RID: 6008
			internal const int cmdidShellNavigate27 = 837;

			// Token: 0x04001779 RID: 6009
			internal const int cmdidShellNavigate28 = 838;

			// Token: 0x0400177A RID: 6010
			internal const int cmdidShellNavigate29 = 839;

			// Token: 0x0400177B RID: 6011
			internal const int cmdidShellNavigate30 = 840;

			// Token: 0x0400177C RID: 6012
			internal const int cmdidShellNavigate31 = 841;

			// Token: 0x0400177D RID: 6013
			internal const int cmdidShellNavigate32 = 842;

			// Token: 0x0400177E RID: 6014
			internal const int cmdidShellNavigate33 = 843;

			// Token: 0x0400177F RID: 6015
			internal const int cmdidShellWindowNavigate1 = 844;

			// Token: 0x04001780 RID: 6016
			internal const int cmdidShellWindowNavigate2 = 845;

			// Token: 0x04001781 RID: 6017
			internal const int cmdidShellWindowNavigate3 = 846;

			// Token: 0x04001782 RID: 6018
			internal const int cmdidShellWindowNavigate4 = 847;

			// Token: 0x04001783 RID: 6019
			internal const int cmdidShellWindowNavigate5 = 848;

			// Token: 0x04001784 RID: 6020
			internal const int cmdidShellWindowNavigate6 = 849;

			// Token: 0x04001785 RID: 6021
			internal const int cmdidShellWindowNavigate7 = 850;

			// Token: 0x04001786 RID: 6022
			internal const int cmdidShellWindowNavigate8 = 851;

			// Token: 0x04001787 RID: 6023
			internal const int cmdidShellWindowNavigate9 = 852;

			// Token: 0x04001788 RID: 6024
			internal const int cmdidShellWindowNavigate10 = 853;

			// Token: 0x04001789 RID: 6025
			internal const int cmdidShellWindowNavigate11 = 854;

			// Token: 0x0400178A RID: 6026
			internal const int cmdidShellWindowNavigate12 = 855;

			// Token: 0x0400178B RID: 6027
			internal const int cmdidShellWindowNavigate13 = 856;

			// Token: 0x0400178C RID: 6028
			internal const int cmdidShellWindowNavigate14 = 857;

			// Token: 0x0400178D RID: 6029
			internal const int cmdidShellWindowNavigate15 = 858;

			// Token: 0x0400178E RID: 6030
			internal const int cmdidShellWindowNavigate16 = 859;

			// Token: 0x0400178F RID: 6031
			internal const int cmdidShellWindowNavigate17 = 860;

			// Token: 0x04001790 RID: 6032
			internal const int cmdidShellWindowNavigate18 = 861;

			// Token: 0x04001791 RID: 6033
			internal const int cmdidShellWindowNavigate19 = 862;

			// Token: 0x04001792 RID: 6034
			internal const int cmdidShellWindowNavigate20 = 863;

			// Token: 0x04001793 RID: 6035
			internal const int cmdidShellWindowNavigate21 = 864;

			// Token: 0x04001794 RID: 6036
			internal const int cmdidShellWindowNavigate22 = 865;

			// Token: 0x04001795 RID: 6037
			internal const int cmdidShellWindowNavigate23 = 866;

			// Token: 0x04001796 RID: 6038
			internal const int cmdidShellWindowNavigate24 = 867;

			// Token: 0x04001797 RID: 6039
			internal const int cmdidShellWindowNavigate25 = 868;

			// Token: 0x04001798 RID: 6040
			internal const int cmdidShellWindowNavigate26 = 869;

			// Token: 0x04001799 RID: 6041
			internal const int cmdidShellWindowNavigate27 = 870;

			// Token: 0x0400179A RID: 6042
			internal const int cmdidShellWindowNavigate28 = 871;

			// Token: 0x0400179B RID: 6043
			internal const int cmdidShellWindowNavigate29 = 872;

			// Token: 0x0400179C RID: 6044
			internal const int cmdidShellWindowNavigate30 = 873;

			// Token: 0x0400179D RID: 6045
			internal const int cmdidShellWindowNavigate31 = 874;

			// Token: 0x0400179E RID: 6046
			internal const int cmdidShellWindowNavigate32 = 875;

			// Token: 0x0400179F RID: 6047
			internal const int cmdidShellWindowNavigate33 = 876;

			// Token: 0x040017A0 RID: 6048
			internal const int cmdidOBSDoFind = 877;

			// Token: 0x040017A1 RID: 6049
			internal const int cmdidOBSMatchCase = 878;

			// Token: 0x040017A2 RID: 6050
			internal const int cmdidOBSMatchSubString = 879;

			// Token: 0x040017A3 RID: 6051
			internal const int cmdidOBSMatchWholeWord = 880;

			// Token: 0x040017A4 RID: 6052
			internal const int cmdidOBSMatchPrefix = 881;

			// Token: 0x040017A5 RID: 6053
			internal const int cmdidBuildSln = 882;

			// Token: 0x040017A6 RID: 6054
			internal const int cmdidRebuildSln = 883;

			// Token: 0x040017A7 RID: 6055
			internal const int cmdidDeploySln = 884;

			// Token: 0x040017A8 RID: 6056
			internal const int cmdidCleanSln = 885;

			// Token: 0x040017A9 RID: 6057
			internal const int cmdidBuildSel = 886;

			// Token: 0x040017AA RID: 6058
			internal const int cmdidRebuildSel = 887;

			// Token: 0x040017AB RID: 6059
			internal const int cmdidDeploySel = 888;

			// Token: 0x040017AC RID: 6060
			internal const int cmdidCleanSel = 889;

			// Token: 0x040017AD RID: 6061
			internal const int cmdidCancelBuild = 890;

			// Token: 0x040017AE RID: 6062
			internal const int cmdidBatchBuildDlg = 891;

			// Token: 0x040017AF RID: 6063
			internal const int cmdidBuildCtx = 892;

			// Token: 0x040017B0 RID: 6064
			internal const int cmdidRebuildCtx = 893;

			// Token: 0x040017B1 RID: 6065
			internal const int cmdidDeployCtx = 894;

			// Token: 0x040017B2 RID: 6066
			internal const int cmdidCleanCtx = 895;

			// Token: 0x040017B3 RID: 6067
			internal const int cmdidMRUFile1 = 900;

			// Token: 0x040017B4 RID: 6068
			internal const int cmdidMRUFile2 = 901;

			// Token: 0x040017B5 RID: 6069
			internal const int cmdidMRUFile3 = 902;

			// Token: 0x040017B6 RID: 6070
			internal const int cmdidMRUFile4 = 903;

			// Token: 0x040017B7 RID: 6071
			internal const int cmdidMRUFile5 = 904;

			// Token: 0x040017B8 RID: 6072
			internal const int cmdidMRUFile6 = 905;

			// Token: 0x040017B9 RID: 6073
			internal const int cmdidMRUFile7 = 906;

			// Token: 0x040017BA RID: 6074
			internal const int cmdidMRUFile8 = 907;

			// Token: 0x040017BB RID: 6075
			internal const int cmdidMRUFile9 = 908;

			// Token: 0x040017BC RID: 6076
			internal const int cmdidMRUFile10 = 909;

			// Token: 0x040017BD RID: 6077
			internal const int cmdidMRUFile11 = 910;

			// Token: 0x040017BE RID: 6078
			internal const int cmdidMRUFile12 = 911;

			// Token: 0x040017BF RID: 6079
			internal const int cmdidMRUFile13 = 912;

			// Token: 0x040017C0 RID: 6080
			internal const int cmdidMRUFile14 = 913;

			// Token: 0x040017C1 RID: 6081
			internal const int cmdidMRUFile15 = 914;

			// Token: 0x040017C2 RID: 6082
			internal const int cmdidMRUFile16 = 915;

			// Token: 0x040017C3 RID: 6083
			internal const int cmdidMRUFile17 = 916;

			// Token: 0x040017C4 RID: 6084
			internal const int cmdidMRUFile18 = 917;

			// Token: 0x040017C5 RID: 6085
			internal const int cmdidMRUFile19 = 918;

			// Token: 0x040017C6 RID: 6086
			internal const int cmdidMRUFile20 = 919;

			// Token: 0x040017C7 RID: 6087
			internal const int cmdidMRUFile21 = 920;

			// Token: 0x040017C8 RID: 6088
			internal const int cmdidMRUFile22 = 921;

			// Token: 0x040017C9 RID: 6089
			internal const int cmdidMRUFile23 = 922;

			// Token: 0x040017CA RID: 6090
			internal const int cmdidMRUFile24 = 923;

			// Token: 0x040017CB RID: 6091
			internal const int cmdidMRUFile25 = 924;

			// Token: 0x040017CC RID: 6092
			internal const int cmdidGotoDefn = 925;

			// Token: 0x040017CD RID: 6093
			internal const int cmdidGotoDecl = 926;

			// Token: 0x040017CE RID: 6094
			internal const int cmdidBrowseDefn = 927;

			// Token: 0x040017CF RID: 6095
			internal const int cmdidShowMembers = 928;

			// Token: 0x040017D0 RID: 6096
			internal const int cmdidShowBases = 929;

			// Token: 0x040017D1 RID: 6097
			internal const int cmdidShowDerived = 930;

			// Token: 0x040017D2 RID: 6098
			internal const int cmdidShowDefns = 931;

			// Token: 0x040017D3 RID: 6099
			internal const int cmdidShowRefs = 932;

			// Token: 0x040017D4 RID: 6100
			internal const int cmdidShowCallers = 933;

			// Token: 0x040017D5 RID: 6101
			internal const int cmdidShowCallees = 934;

			// Token: 0x040017D6 RID: 6102
			internal const int cmdidDefineSubset = 935;

			// Token: 0x040017D7 RID: 6103
			internal const int cmdidSetSubset = 936;

			// Token: 0x040017D8 RID: 6104
			internal const int cmdidCVGroupingNone = 950;

			// Token: 0x040017D9 RID: 6105
			internal const int cmdidCVGroupingSortOnly = 951;

			// Token: 0x040017DA RID: 6106
			internal const int cmdidCVGroupingGrouped = 952;

			// Token: 0x040017DB RID: 6107
			internal const int cmdidCVShowPackages = 953;

			// Token: 0x040017DC RID: 6108
			internal const int cmdidQryManageIndexes = 954;

			// Token: 0x040017DD RID: 6109
			internal const int cmdidBrowseComponent = 955;

			// Token: 0x040017DE RID: 6110
			internal const int cmdidPrintDefault = 956;

			// Token: 0x040017DF RID: 6111
			internal const int cmdidBrowseDoc = 957;

			// Token: 0x040017E0 RID: 6112
			internal const int cmdidStandardMax = 1000;

			// Token: 0x040017E1 RID: 6113
			internal const int cmdidFormsFirst = 24576;

			// Token: 0x040017E2 RID: 6114
			internal const int cmdidFormsLast = 28671;

			// Token: 0x040017E3 RID: 6115
			internal const int cmdidVBEFirst = 32768;

			// Token: 0x040017E4 RID: 6116
			internal const int msotcidBookmarkWellMenu = 32769;

			// Token: 0x040017E5 RID: 6117
			internal const int cmdidZoom200 = 32770;

			// Token: 0x040017E6 RID: 6118
			internal const int cmdidZoom150 = 32771;

			// Token: 0x040017E7 RID: 6119
			internal const int cmdidZoom100 = 32772;

			// Token: 0x040017E8 RID: 6120
			internal const int cmdidZoom75 = 32773;

			// Token: 0x040017E9 RID: 6121
			internal const int cmdidZoom50 = 32774;

			// Token: 0x040017EA RID: 6122
			internal const int cmdidZoom25 = 32775;

			// Token: 0x040017EB RID: 6123
			internal const int cmdidZoom10 = 32784;

			// Token: 0x040017EC RID: 6124
			internal const int msotcidZoomWellMenu = 32785;

			// Token: 0x040017ED RID: 6125
			internal const int msotcidDebugPopWellMenu = 32786;

			// Token: 0x040017EE RID: 6126
			internal const int msotcidAlignWellMenu = 32787;

			// Token: 0x040017EF RID: 6127
			internal const int msotcidArrangeWellMenu = 32788;

			// Token: 0x040017F0 RID: 6128
			internal const int msotcidCenterWellMenu = 32789;

			// Token: 0x040017F1 RID: 6129
			internal const int msotcidSizeWellMenu = 32790;

			// Token: 0x040017F2 RID: 6130
			internal const int msotcidHorizontalSpaceWellMenu = 32791;

			// Token: 0x040017F3 RID: 6131
			internal const int msotcidVerticalSpaceWellMenu = 32800;

			// Token: 0x040017F4 RID: 6132
			internal const int msotcidDebugWellMenu = 32801;

			// Token: 0x040017F5 RID: 6133
			internal const int msotcidDebugMenuVB = 32802;

			// Token: 0x040017F6 RID: 6134
			internal const int msotcidStatementBuilderWellMenu = 32803;

			// Token: 0x040017F7 RID: 6135
			internal const int msotcidProjWinInsertMenu = 32804;

			// Token: 0x040017F8 RID: 6136
			internal const int msotcidToggleMenu = 32805;

			// Token: 0x040017F9 RID: 6137
			internal const int msotcidNewObjInsertWellMenu = 32806;

			// Token: 0x040017FA RID: 6138
			internal const int msotcidSizeToWellMenu = 32807;

			// Token: 0x040017FB RID: 6139
			internal const int msotcidCommandBars = 32808;

			// Token: 0x040017FC RID: 6140
			internal const int msotcidVBOrderMenu = 32809;

			// Token: 0x040017FD RID: 6141
			internal const int msotcidMSOnTheWeb = 32810;

			// Token: 0x040017FE RID: 6142
			internal const int msotcidVBDesignerMenu = 32816;

			// Token: 0x040017FF RID: 6143
			internal const int msotcidNewProjectWellMenu = 32817;

			// Token: 0x04001800 RID: 6144
			internal const int msotcidProjectWellMenu = 32818;

			// Token: 0x04001801 RID: 6145
			internal const int msotcidVBCode1ContextMenu = 32819;

			// Token: 0x04001802 RID: 6146
			internal const int msotcidVBCode2ContextMenu = 32820;

			// Token: 0x04001803 RID: 6147
			internal const int msotcidVBWatchContextMenu = 32821;

			// Token: 0x04001804 RID: 6148
			internal const int msotcidVBImmediateContextMenu = 32822;

			// Token: 0x04001805 RID: 6149
			internal const int msotcidVBLocalsContextMenu = 32823;

			// Token: 0x04001806 RID: 6150
			internal const int msotcidVBFormContextMenu = 32824;

			// Token: 0x04001807 RID: 6151
			internal const int msotcidVBControlContextMenu = 32825;

			// Token: 0x04001808 RID: 6152
			internal const int msotcidVBProjWinContextMenu = 32826;

			// Token: 0x04001809 RID: 6153
			internal const int msotcidVBProjWinContextBreakMenu = 32827;

			// Token: 0x0400180A RID: 6154
			internal const int msotcidVBPreviewWinContextMenu = 32828;

			// Token: 0x0400180B RID: 6155
			internal const int msotcidVBOBContextMenu = 32829;

			// Token: 0x0400180C RID: 6156
			internal const int msotcidVBForms3ContextMenu = 32830;

			// Token: 0x0400180D RID: 6157
			internal const int msotcidVBForms3ControlCMenu = 32831;

			// Token: 0x0400180E RID: 6158
			internal const int msotcidVBForms3ControlCMenuGroup = 32832;

			// Token: 0x0400180F RID: 6159
			internal const int msotcidVBForms3ControlPalette = 32833;

			// Token: 0x04001810 RID: 6160
			internal const int msotcidVBForms3ToolboxCMenu = 32834;

			// Token: 0x04001811 RID: 6161
			internal const int msotcidVBForms3MPCCMenu = 32835;

			// Token: 0x04001812 RID: 6162
			internal const int msotcidVBForms3DragDropCMenu = 32836;

			// Token: 0x04001813 RID: 6163
			internal const int msotcidVBToolBoxContextMenu = 32837;

			// Token: 0x04001814 RID: 6164
			internal const int msotcidVBToolBoxGroupContextMenu = 32838;

			// Token: 0x04001815 RID: 6165
			internal const int msotcidVBPropBrsHostContextMenu = 32839;

			// Token: 0x04001816 RID: 6166
			internal const int msotcidVBPropBrsContextMenu = 32840;

			// Token: 0x04001817 RID: 6167
			internal const int msotcidVBPalContextMenu = 32841;

			// Token: 0x04001818 RID: 6168
			internal const int msotcidVBProjWinProjectContextMenu = 32842;

			// Token: 0x04001819 RID: 6169
			internal const int msotcidVBProjWinFormContextMenu = 32843;

			// Token: 0x0400181A RID: 6170
			internal const int msotcidVBProjWinModClassContextMenu = 32844;

			// Token: 0x0400181B RID: 6171
			internal const int msotcidVBProjWinRelDocContextMenu = 32845;

			// Token: 0x0400181C RID: 6172
			internal const int msotcidVBDockedWindowContextMenu = 32846;

			// Token: 0x0400181D RID: 6173
			internal const int msotcidVBShortCutForms = 32847;

			// Token: 0x0400181E RID: 6174
			internal const int msotcidVBShortCutCodeWindows = 32848;

			// Token: 0x0400181F RID: 6175
			internal const int msotcidVBShortCutMisc = 32849;

			// Token: 0x04001820 RID: 6176
			internal const int msotcidVBBuiltInMenus = 32850;

			// Token: 0x04001821 RID: 6177
			internal const int msotcidPreviewWinFormPos = 32851;

			// Token: 0x04001822 RID: 6178
			internal const int msotcidVBAddinFirst = 33280;
		}

		// Token: 0x02000345 RID: 837
		private static class ShellGuids
		{
			// Token: 0x04001823 RID: 6179
			internal static readonly Guid VSStandardCommandSet97 = new Guid("{5efc7975-14bc-11cf-9b2b-00aa00573819}");

			// Token: 0x04001824 RID: 6180
			internal static readonly Guid guidDsdCmdId = new Guid("{1F0FD094-8e53-11d2-8f9c-0060089fc486}");

			// Token: 0x04001825 RID: 6181
			internal static readonly Guid SID_SOleComponentUIManager = new Guid("{5efc7974-14bc-11cf-9b2b-00aa00573819}");

			// Token: 0x04001826 RID: 6182
			internal static readonly Guid GUID_VSTASKCATEGORY_DATADESIGNER = new Guid("{6B32EAED-13BB-11d3-A64F-00C04F683820}");

			// Token: 0x04001827 RID: 6183
			internal static readonly Guid GUID_PropertyBrowserToolWindow = new Guid(-285584864, -7528, 4560, new byte[] { 143, 120, 0, 160, 201, 17, 0, 87 });
		}
	}
}
