using System;

namespace Melanchall.DryWetMidi.Common
{
	// Token: 0x020001BF RID: 447
	public enum ControlName : byte
	{
		// Token: 0x040009AB RID: 2475
		BankSelect,
		// Token: 0x040009AC RID: 2476
		Modulation,
		// Token: 0x040009AD RID: 2477
		BreathController,
		// Token: 0x040009AE RID: 2478
		FootController = 4,
		// Token: 0x040009AF RID: 2479
		PortamentoTime,
		// Token: 0x040009B0 RID: 2480
		DataEntryMsb,
		// Token: 0x040009B1 RID: 2481
		ChannelVolume,
		// Token: 0x040009B2 RID: 2482
		Balance,
		// Token: 0x040009B3 RID: 2483
		Pan = 10,
		// Token: 0x040009B4 RID: 2484
		ExpressionController,
		// Token: 0x040009B5 RID: 2485
		EffectControl1,
		// Token: 0x040009B6 RID: 2486
		EffectControl2,
		// Token: 0x040009B7 RID: 2487
		GeneralPurposeController1 = 16,
		// Token: 0x040009B8 RID: 2488
		GeneralPurposeController2,
		// Token: 0x040009B9 RID: 2489
		GeneralPurposeController3,
		// Token: 0x040009BA RID: 2490
		GeneralPurposeController4,
		// Token: 0x040009BB RID: 2491
		LsbForBankSelect = 32,
		// Token: 0x040009BC RID: 2492
		LsbForModulation,
		// Token: 0x040009BD RID: 2493
		LsbForBreathController,
		// Token: 0x040009BE RID: 2494
		LsbForController3,
		// Token: 0x040009BF RID: 2495
		LsbForFootController,
		// Token: 0x040009C0 RID: 2496
		LsbForPortamentoTime,
		// Token: 0x040009C1 RID: 2497
		LsbForDataEntry,
		// Token: 0x040009C2 RID: 2498
		LsbForChannelVolume,
		// Token: 0x040009C3 RID: 2499
		LsbForBalance,
		// Token: 0x040009C4 RID: 2500
		LsbForController9,
		// Token: 0x040009C5 RID: 2501
		LsbForPan,
		// Token: 0x040009C6 RID: 2502
		LsbForExpressionController,
		// Token: 0x040009C7 RID: 2503
		LsbForEffectControl1,
		// Token: 0x040009C8 RID: 2504
		LsbForEffectControl2,
		// Token: 0x040009C9 RID: 2505
		LsbForController14,
		// Token: 0x040009CA RID: 2506
		LsbForController15,
		// Token: 0x040009CB RID: 2507
		LsbForGeneralPurposeController1,
		// Token: 0x040009CC RID: 2508
		LsbForGeneralPurposeController2,
		// Token: 0x040009CD RID: 2509
		LsbForGeneralPurposeController3,
		// Token: 0x040009CE RID: 2510
		LsbForGeneralPurposeController4,
		// Token: 0x040009CF RID: 2511
		LsbForController20,
		// Token: 0x040009D0 RID: 2512
		LsbForController21,
		// Token: 0x040009D1 RID: 2513
		LsbForController22,
		// Token: 0x040009D2 RID: 2514
		LsbForController23,
		// Token: 0x040009D3 RID: 2515
		LsbForController24,
		// Token: 0x040009D4 RID: 2516
		LsbForController25,
		// Token: 0x040009D5 RID: 2517
		LsbForController26,
		// Token: 0x040009D6 RID: 2518
		LsbForController27,
		// Token: 0x040009D7 RID: 2519
		LsbForController28,
		// Token: 0x040009D8 RID: 2520
		LsbForController29,
		// Token: 0x040009D9 RID: 2521
		LsbForController30,
		// Token: 0x040009DA RID: 2522
		LsbForController31,
		// Token: 0x040009DB RID: 2523
		DamperPedal,
		// Token: 0x040009DC RID: 2524
		Portamento,
		// Token: 0x040009DD RID: 2525
		Sostenuto,
		// Token: 0x040009DE RID: 2526
		SoftPedal,
		// Token: 0x040009DF RID: 2527
		LegatoFootswitch,
		// Token: 0x040009E0 RID: 2528
		Hold2,
		// Token: 0x040009E1 RID: 2529
		SoundController1,
		// Token: 0x040009E2 RID: 2530
		SoundController2,
		// Token: 0x040009E3 RID: 2531
		SoundController3,
		// Token: 0x040009E4 RID: 2532
		SoundController4,
		// Token: 0x040009E5 RID: 2533
		SoundController5,
		// Token: 0x040009E6 RID: 2534
		SoundController6,
		// Token: 0x040009E7 RID: 2535
		SoundController7,
		// Token: 0x040009E8 RID: 2536
		SoundController8,
		// Token: 0x040009E9 RID: 2537
		SoundController9,
		// Token: 0x040009EA RID: 2538
		SoundController10,
		// Token: 0x040009EB RID: 2539
		GeneralPurposeController5,
		// Token: 0x040009EC RID: 2540
		GeneralPurposeController6,
		// Token: 0x040009ED RID: 2541
		GeneralPurposeController7,
		// Token: 0x040009EE RID: 2542
		GeneralPurposeController8,
		// Token: 0x040009EF RID: 2543
		PortamentoControl,
		// Token: 0x040009F0 RID: 2544
		HighResolutionVelocityPrefix = 88,
		// Token: 0x040009F1 RID: 2545
		Effects1Depth = 91,
		// Token: 0x040009F2 RID: 2546
		Effects2Depth,
		// Token: 0x040009F3 RID: 2547
		Effects3Depth,
		// Token: 0x040009F4 RID: 2548
		Effects4Depth,
		// Token: 0x040009F5 RID: 2549
		Effects5Depth,
		// Token: 0x040009F6 RID: 2550
		DataIncrement,
		// Token: 0x040009F7 RID: 2551
		DataDecrement,
		// Token: 0x040009F8 RID: 2552
		NonRegisteredParameterNumberLsb,
		// Token: 0x040009F9 RID: 2553
		NonRegisteredParameterNumberMsb,
		// Token: 0x040009FA RID: 2554
		RegisteredParameterNumberLsb,
		// Token: 0x040009FB RID: 2555
		RegisteredParameterNumberMsb,
		// Token: 0x040009FC RID: 2556
		AllSoundOff = 120,
		// Token: 0x040009FD RID: 2557
		ResetAllControllers,
		// Token: 0x040009FE RID: 2558
		LocalControl,
		// Token: 0x040009FF RID: 2559
		AllNotesOff,
		// Token: 0x04000A00 RID: 2560
		OmniModeOff,
		// Token: 0x04000A01 RID: 2561
		OmniModeOn,
		// Token: 0x04000A02 RID: 2562
		MonoModeOn,
		// Token: 0x04000A03 RID: 2563
		PolyModeOn,
		// Token: 0x04000A04 RID: 2564
		Undefined = 255
	}
}
