using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Melanchall.DryWetMidi.Common;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x0200018C RID: 396
	public class ReadingSettings
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0002180E File Offset: 0x0001FA0E
		// (set) Token: 0x06000999 RID: 2457 RVA: 0x00021816 File Offset: 0x0001FA16
		public UnexpectedTrackChunksCountPolicy UnexpectedTrackChunksCountPolicy
		{
			get
			{
				return this._unexpectedTrackChunksCountPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<UnexpectedTrackChunksCountPolicy>("value", value);
				this._unexpectedTrackChunksCountPolicy = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0002182A File Offset: 0x0001FA2A
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x00021832 File Offset: 0x0001FA32
		public ExtraTrackChunkPolicy ExtraTrackChunkPolicy
		{
			get
			{
				return this._extraTrackChunkPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<ExtraTrackChunkPolicy>("value", value);
				this._extraTrackChunkPolicy = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00021846 File Offset: 0x0001FA46
		// (set) Token: 0x0600099D RID: 2461 RVA: 0x0002184E File Offset: 0x0001FA4E
		public UnknownChunkIdPolicy UnknownChunkIdPolicy
		{
			get
			{
				return this._unknownChunkIdPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<UnknownChunkIdPolicy>("value", value);
				this._unknownChunkIdPolicy = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x00021862 File Offset: 0x0001FA62
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x0002186A File Offset: 0x0001FA6A
		public MissedEndOfTrackPolicy MissedEndOfTrackPolicy
		{
			get
			{
				return this._missedEndOfTrackPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<MissedEndOfTrackPolicy>("value", value);
				this._missedEndOfTrackPolicy = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0002187E File Offset: 0x0001FA7E
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x00021886 File Offset: 0x0001FA86
		public SilentNoteOnPolicy SilentNoteOnPolicy
		{
			get
			{
				return this._silentNoteOnPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<SilentNoteOnPolicy>("value", value);
				this._silentNoteOnPolicy = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x0002189A File Offset: 0x0001FA9A
		// (set) Token: 0x060009A3 RID: 2467 RVA: 0x000218A2 File Offset: 0x0001FAA2
		public InvalidChunkSizePolicy InvalidChunkSizePolicy
		{
			get
			{
				return this._invalidChunkSizePolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<InvalidChunkSizePolicy>("value", value);
				this._invalidChunkSizePolicy = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x000218B6 File Offset: 0x0001FAB6
		// (set) Token: 0x060009A5 RID: 2469 RVA: 0x000218BE File Offset: 0x0001FABE
		public UnknownFileFormatPolicy UnknownFileFormatPolicy
		{
			get
			{
				return this._unknownFileFormatPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<UnknownFileFormatPolicy>("value", value);
				this._unknownFileFormatPolicy = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x000218D2 File Offset: 0x0001FAD2
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x000218DA File Offset: 0x0001FADA
		public UnknownChannelEventPolicy UnknownChannelEventPolicy
		{
			get
			{
				return this._unknownChannelEventPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<UnknownChannelEventPolicy>("value", value);
				this._unknownChannelEventPolicy = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x000218EE File Offset: 0x0001FAEE
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x000218F6 File Offset: 0x0001FAF6
		public UnknownChannelEventCallback UnknownChannelEventCallback { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x000218FF File Offset: 0x0001FAFF
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x00021907 File Offset: 0x0001FB07
		public InvalidChannelEventParameterValuePolicy InvalidChannelEventParameterValuePolicy
		{
			get
			{
				return this._invalidChannelEventParameterValuePolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<InvalidChannelEventParameterValuePolicy>("value", value);
				this._invalidChannelEventParameterValuePolicy = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x0002191B File Offset: 0x0001FB1B
		// (set) Token: 0x060009AD RID: 2477 RVA: 0x00021923 File Offset: 0x0001FB23
		public InvalidMetaEventParameterValuePolicy InvalidMetaEventParameterValuePolicy
		{
			get
			{
				return this._invalidMetaEventParameterValuePolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<InvalidMetaEventParameterValuePolicy>("value", value);
				this._invalidMetaEventParameterValuePolicy = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x00021937 File Offset: 0x0001FB37
		// (set) Token: 0x060009AF RID: 2479 RVA: 0x0002193F File Offset: 0x0001FB3F
		public InvalidSystemCommonEventParameterValuePolicy InvalidSystemCommonEventParameterValuePolicy
		{
			get
			{
				return this._invalidSystemCommonEventParameterValuePolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<InvalidSystemCommonEventParameterValuePolicy>("value", value);
				this._invalidSystemCommonEventParameterValuePolicy = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x00021953 File Offset: 0x0001FB53
		// (set) Token: 0x060009B1 RID: 2481 RVA: 0x0002195B File Offset: 0x0001FB5B
		public NotEnoughBytesPolicy NotEnoughBytesPolicy
		{
			get
			{
				return this._notEnoughBytesPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<NotEnoughBytesPolicy>("value", value);
				this._notEnoughBytesPolicy = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0002196F File Offset: 0x0001FB6F
		// (set) Token: 0x060009B3 RID: 2483 RVA: 0x00021977 File Offset: 0x0001FB77
		public NoHeaderChunkPolicy NoHeaderChunkPolicy
		{
			get
			{
				return this._noHeaderChunkPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue<NoHeaderChunkPolicy>("value", value);
				this._noHeaderChunkPolicy = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x0002198B File Offset: 0x0001FB8B
		// (set) Token: 0x060009B5 RID: 2485 RVA: 0x00021993 File Offset: 0x0001FB93
		public ChunkTypesCollection CustomChunkTypes { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0002199C File Offset: 0x0001FB9C
		// (set) Token: 0x060009B7 RID: 2487 RVA: 0x000219A4 File Offset: 0x0001FBA4
		public EventTypesCollection CustomMetaEventTypes { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x000219AD File Offset: 0x0001FBAD
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x000219B5 File Offset: 0x0001FBB5
		public Encoding TextEncoding { get; set; } = SmfConstants.DefaultTextEncoding;

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x000219BE File Offset: 0x0001FBBE
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x000219C6 File Offset: 0x0001FBC6
		public DecodeTextCallback DecodeTextCallback { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x000219CF File Offset: 0x0001FBCF
		public ICollection<ReadingHandler> ReadingHandlers { get; } = new List<ReadingHandler>();

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x000219D7 File Offset: 0x0001FBD7
		// (set) Token: 0x060009BE RID: 2494 RVA: 0x000219DF File Offset: 0x0001FBDF
		public ReaderSettings ReaderSettings { get; set; } = new ReaderSettings();

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x000219E8 File Offset: 0x0001FBE8
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x000219F0 File Offset: 0x0001FBF0
		internal bool UseReadingHandlers { get; private set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x000219F9 File Offset: 0x0001FBF9
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x00021A01 File Offset: 0x0001FC01
		internal ICollection<ReadingHandler> FileReadingHandlers { get; private set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x00021A0A File Offset: 0x0001FC0A
		// (set) Token: 0x060009C4 RID: 2500 RVA: 0x00021A12 File Offset: 0x0001FC12
		internal ICollection<ReadingHandler> TrackChunkReadingHandlers { get; private set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00021A1B File Offset: 0x0001FC1B
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x00021A23 File Offset: 0x0001FC23
		internal ICollection<ReadingHandler> EventReadingHandlers { get; private set; }

		// Token: 0x060009C7 RID: 2503 RVA: 0x00021A2C File Offset: 0x0001FC2C
		internal void PrepareReadingHandlers()
		{
			this.UseReadingHandlers = this.ReadingHandlers.Any<ReadingHandler>();
			foreach (ReadingHandler readingHandler in this.ReadingHandlers)
			{
				readingHandler.Initialize();
			}
			this.FileReadingHandlers = this.ReadingHandlers.Where((ReadingHandler h) => h.Scope.HasFlag(ReadingHandler.TargetScope.File)).ToArray<ReadingHandler>();
			this.TrackChunkReadingHandlers = this.ReadingHandlers.Where((ReadingHandler h) => h.Scope.HasFlag(ReadingHandler.TargetScope.TrackChunk)).ToArray<ReadingHandler>();
			this.EventReadingHandlers = this.ReadingHandlers.Where((ReadingHandler h) => h.Scope.HasFlag(ReadingHandler.TargetScope.Event)).ToArray<ReadingHandler>();
		}

		// Token: 0x04000918 RID: 2328
		private UnexpectedTrackChunksCountPolicy _unexpectedTrackChunksCountPolicy;

		// Token: 0x04000919 RID: 2329
		private ExtraTrackChunkPolicy _extraTrackChunkPolicy;

		// Token: 0x0400091A RID: 2330
		private UnknownChunkIdPolicy _unknownChunkIdPolicy;

		// Token: 0x0400091B RID: 2331
		private MissedEndOfTrackPolicy _missedEndOfTrackPolicy;

		// Token: 0x0400091C RID: 2332
		private SilentNoteOnPolicy _silentNoteOnPolicy;

		// Token: 0x0400091D RID: 2333
		private InvalidChunkSizePolicy _invalidChunkSizePolicy;

		// Token: 0x0400091E RID: 2334
		private UnknownFileFormatPolicy _unknownFileFormatPolicy;

		// Token: 0x0400091F RID: 2335
		private UnknownChannelEventPolicy _unknownChannelEventPolicy;

		// Token: 0x04000920 RID: 2336
		private InvalidChannelEventParameterValuePolicy _invalidChannelEventParameterValuePolicy;

		// Token: 0x04000921 RID: 2337
		private InvalidMetaEventParameterValuePolicy _invalidMetaEventParameterValuePolicy;

		// Token: 0x04000922 RID: 2338
		private InvalidSystemCommonEventParameterValuePolicy _invalidSystemCommonEventParameterValuePolicy;

		// Token: 0x04000923 RID: 2339
		private NotEnoughBytesPolicy _notEnoughBytesPolicy;

		// Token: 0x04000924 RID: 2340
		private NoHeaderChunkPolicy _noHeaderChunkPolicy;
	}
}
