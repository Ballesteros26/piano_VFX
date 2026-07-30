using System;

namespace Melanchall.DryWetMidi.Standards
{
	// Token: 0x02000066 RID: 102
	public enum GeneralMidiProgram : byte
	{
		// Token: 0x04000125 RID: 293
		AcousticGrandPiano,
		// Token: 0x04000126 RID: 294
		BrightAcousticPiano,
		// Token: 0x04000127 RID: 295
		ElectricGrandPiano,
		// Token: 0x04000128 RID: 296
		HonkyTonkPiano,
		// Token: 0x04000129 RID: 297
		ElectricPiano1,
		// Token: 0x0400012A RID: 298
		ElectricPiano2,
		// Token: 0x0400012B RID: 299
		Harpsichord,
		// Token: 0x0400012C RID: 300
		Clavi,
		// Token: 0x0400012D RID: 301
		Celesta,
		// Token: 0x0400012E RID: 302
		Glockenspiel,
		// Token: 0x0400012F RID: 303
		MusicBox,
		// Token: 0x04000130 RID: 304
		Vibraphone,
		// Token: 0x04000131 RID: 305
		Marimba,
		// Token: 0x04000132 RID: 306
		Xylophone,
		// Token: 0x04000133 RID: 307
		TubularBells,
		// Token: 0x04000134 RID: 308
		Dulcimer,
		// Token: 0x04000135 RID: 309
		DrawbarOrgan,
		// Token: 0x04000136 RID: 310
		PercussiveOrgan,
		// Token: 0x04000137 RID: 311
		RockOrgan,
		// Token: 0x04000138 RID: 312
		ChurchOrgan,
		// Token: 0x04000139 RID: 313
		ReedOrgan,
		// Token: 0x0400013A RID: 314
		Accordion,
		// Token: 0x0400013B RID: 315
		Harmonica,
		// Token: 0x0400013C RID: 316
		TangoAccordion,
		// Token: 0x0400013D RID: 317
		AcousticGuitar1,
		// Token: 0x0400013E RID: 318
		AcousticGuitar2,
		// Token: 0x0400013F RID: 319
		ElectricGuitar1,
		// Token: 0x04000140 RID: 320
		ElectricGuitar2,
		// Token: 0x04000141 RID: 321
		ElectricGuitar3,
		// Token: 0x04000142 RID: 322
		OverdrivenGuitar,
		// Token: 0x04000143 RID: 323
		DistortionGuitar,
		// Token: 0x04000144 RID: 324
		GuitarHarmonics,
		// Token: 0x04000145 RID: 325
		AcousticBass,
		// Token: 0x04000146 RID: 326
		ElectricBass1,
		// Token: 0x04000147 RID: 327
		ElectricBass2,
		// Token: 0x04000148 RID: 328
		FretlessBass,
		// Token: 0x04000149 RID: 329
		SlapBass1,
		// Token: 0x0400014A RID: 330
		SlapBass2,
		// Token: 0x0400014B RID: 331
		SynthBass1,
		// Token: 0x0400014C RID: 332
		SynthBass2,
		// Token: 0x0400014D RID: 333
		Violin,
		// Token: 0x0400014E RID: 334
		Viola,
		// Token: 0x0400014F RID: 335
		Cello,
		// Token: 0x04000150 RID: 336
		Contrabass,
		// Token: 0x04000151 RID: 337
		TremoloStrings,
		// Token: 0x04000152 RID: 338
		PizzicatoStrings,
		// Token: 0x04000153 RID: 339
		OrchestralHarp,
		// Token: 0x04000154 RID: 340
		Timpani,
		// Token: 0x04000155 RID: 341
		StringEnsemble1,
		// Token: 0x04000156 RID: 342
		StringEnsemble2,
		// Token: 0x04000157 RID: 343
		SynthStrings1,
		// Token: 0x04000158 RID: 344
		SynthStrings2,
		// Token: 0x04000159 RID: 345
		ChoirAahs,
		// Token: 0x0400015A RID: 346
		VoiceOohs,
		// Token: 0x0400015B RID: 347
		SynthVoice,
		// Token: 0x0400015C RID: 348
		OrchestraHit,
		// Token: 0x0400015D RID: 349
		Trumpet,
		// Token: 0x0400015E RID: 350
		Trombone,
		// Token: 0x0400015F RID: 351
		Tuba,
		// Token: 0x04000160 RID: 352
		MutedTrumpet,
		// Token: 0x04000161 RID: 353
		FrenchHorn,
		// Token: 0x04000162 RID: 354
		BrassSection,
		// Token: 0x04000163 RID: 355
		SynthBrass1,
		// Token: 0x04000164 RID: 356
		SynthBrass2,
		// Token: 0x04000165 RID: 357
		SopranoSax,
		// Token: 0x04000166 RID: 358
		AltoSax,
		// Token: 0x04000167 RID: 359
		TenorSax,
		// Token: 0x04000168 RID: 360
		BaritoneSax,
		// Token: 0x04000169 RID: 361
		Oboe,
		// Token: 0x0400016A RID: 362
		EnglishHorn,
		// Token: 0x0400016B RID: 363
		Bassoon,
		// Token: 0x0400016C RID: 364
		Clarinet,
		// Token: 0x0400016D RID: 365
		Piccolo,
		// Token: 0x0400016E RID: 366
		Flute,
		// Token: 0x0400016F RID: 367
		Recorder,
		// Token: 0x04000170 RID: 368
		PanFlute,
		// Token: 0x04000171 RID: 369
		BlownBottle,
		// Token: 0x04000172 RID: 370
		Shakuhachi,
		// Token: 0x04000173 RID: 371
		Whistle,
		// Token: 0x04000174 RID: 372
		Ocarina,
		// Token: 0x04000175 RID: 373
		Lead1,
		// Token: 0x04000176 RID: 374
		Lead2,
		// Token: 0x04000177 RID: 375
		Lead3,
		// Token: 0x04000178 RID: 376
		Lead4,
		// Token: 0x04000179 RID: 377
		Lead5,
		// Token: 0x0400017A RID: 378
		Lead6,
		// Token: 0x0400017B RID: 379
		Lead7,
		// Token: 0x0400017C RID: 380
		Lead8,
		// Token: 0x0400017D RID: 381
		Pad1,
		// Token: 0x0400017E RID: 382
		Pad2,
		// Token: 0x0400017F RID: 383
		Pad3,
		// Token: 0x04000180 RID: 384
		Pad4,
		// Token: 0x04000181 RID: 385
		Pad5,
		// Token: 0x04000182 RID: 386
		Pad6,
		// Token: 0x04000183 RID: 387
		Pad7,
		// Token: 0x04000184 RID: 388
		Pad8,
		// Token: 0x04000185 RID: 389
		Fx1,
		// Token: 0x04000186 RID: 390
		Fx2,
		// Token: 0x04000187 RID: 391
		Fx3,
		// Token: 0x04000188 RID: 392
		Fx4,
		// Token: 0x04000189 RID: 393
		Fx5,
		// Token: 0x0400018A RID: 394
		Fx6,
		// Token: 0x0400018B RID: 395
		Fx7,
		// Token: 0x0400018C RID: 396
		Fx8,
		// Token: 0x0400018D RID: 397
		Sitar,
		// Token: 0x0400018E RID: 398
		Banjo,
		// Token: 0x0400018F RID: 399
		Shamisen,
		// Token: 0x04000190 RID: 400
		Koto,
		// Token: 0x04000191 RID: 401
		Kalimba,
		// Token: 0x04000192 RID: 402
		BagPipe,
		// Token: 0x04000193 RID: 403
		Fiddle,
		// Token: 0x04000194 RID: 404
		Shanai,
		// Token: 0x04000195 RID: 405
		TinkleBell,
		// Token: 0x04000196 RID: 406
		Agogo,
		// Token: 0x04000197 RID: 407
		SteelDrums,
		// Token: 0x04000198 RID: 408
		Woodblock,
		// Token: 0x04000199 RID: 409
		TaikoDrum,
		// Token: 0x0400019A RID: 410
		MelodicTom,
		// Token: 0x0400019B RID: 411
		SynthDrum,
		// Token: 0x0400019C RID: 412
		ReverseCymbal,
		// Token: 0x0400019D RID: 413
		GuitarFretNoise,
		// Token: 0x0400019E RID: 414
		BreathNoise,
		// Token: 0x0400019F RID: 415
		Seashore,
		// Token: 0x040001A0 RID: 416
		BirdTweet,
		// Token: 0x040001A1 RID: 417
		TelephoneRing,
		// Token: 0x040001A2 RID: 418
		Helicopter,
		// Token: 0x040001A3 RID: 419
		Applause,
		// Token: 0x040001A4 RID: 420
		Gunshot
	}
}
