using System;

namespace Mono.AppleTls
{
	// Token: 0x020000AF RID: 175
	internal enum SecStatusCode
	{
		// Token: 0x04000937 RID: 2359
		Success,
		// Token: 0x04000938 RID: 2360
		Unimplemented = -4,
		// Token: 0x04000939 RID: 2361
		DiskFull = -34,
		// Token: 0x0400093A RID: 2362
		IO = -36,
		// Token: 0x0400093B RID: 2363
		OpWr = -49,
		// Token: 0x0400093C RID: 2364
		Param = -50,
		// Token: 0x0400093D RID: 2365
		WritePermissions = -61,
		// Token: 0x0400093E RID: 2366
		Allocate = -108,
		// Token: 0x0400093F RID: 2367
		UserCanceled = -128,
		// Token: 0x04000940 RID: 2368
		BadReq = -909,
		// Token: 0x04000941 RID: 2369
		InternalComponent = -2070,
		// Token: 0x04000942 RID: 2370
		CoreFoundationUnknown = -4960,
		// Token: 0x04000943 RID: 2371
		NotAvailable = -25291,
		// Token: 0x04000944 RID: 2372
		ReadOnly = -25292,
		// Token: 0x04000945 RID: 2373
		AuthFailed = -25293,
		// Token: 0x04000946 RID: 2374
		NoSuchKeyChain = -25294,
		// Token: 0x04000947 RID: 2375
		InvalidKeyChain = -25295,
		// Token: 0x04000948 RID: 2376
		DuplicateKeyChain = -25296,
		// Token: 0x04000949 RID: 2377
		DuplicateItem = -25299,
		// Token: 0x0400094A RID: 2378
		ItemNotFound = -25300,
		// Token: 0x0400094B RID: 2379
		InteractionNotAllowed = -25308,
		// Token: 0x0400094C RID: 2380
		Decode = -26275,
		// Token: 0x0400094D RID: 2381
		DuplicateCallback = -25297,
		// Token: 0x0400094E RID: 2382
		InvalidCallback = -25298,
		// Token: 0x0400094F RID: 2383
		BufferTooSmall = -25301,
		// Token: 0x04000950 RID: 2384
		DataTooLarge = -25302,
		// Token: 0x04000951 RID: 2385
		NoSuchAttribute = -25303,
		// Token: 0x04000952 RID: 2386
		InvalidItemRef = -25304,
		// Token: 0x04000953 RID: 2387
		InvalidSearchRef = -25305,
		// Token: 0x04000954 RID: 2388
		NoSuchClass = -25306,
		// Token: 0x04000955 RID: 2389
		NoDefaultKeychain = -25307,
		// Token: 0x04000956 RID: 2390
		ReadOnlyAttribute = -25309,
		// Token: 0x04000957 RID: 2391
		WrongSecVersion = -25310,
		// Token: 0x04000958 RID: 2392
		KeySizeNotAllowed = -25311,
		// Token: 0x04000959 RID: 2393
		NoStorageModule = -25312,
		// Token: 0x0400095A RID: 2394
		NoCertificateModule = -25313,
		// Token: 0x0400095B RID: 2395
		NoPolicyModule = -25314,
		// Token: 0x0400095C RID: 2396
		InteractionRequired = -25315,
		// Token: 0x0400095D RID: 2397
		DataNotAvailable = -25316,
		// Token: 0x0400095E RID: 2398
		DataNotModifiable = -25317,
		// Token: 0x0400095F RID: 2399
		CreateChainFailed = -25318,
		// Token: 0x04000960 RID: 2400
		InvalidPrefsDomain = -25319,
		// Token: 0x04000961 RID: 2401
		InDarkWake = -25320,
		// Token: 0x04000962 RID: 2402
		ACLNotSimple = -25240,
		// Token: 0x04000963 RID: 2403
		PolicyNotFound = -25241,
		// Token: 0x04000964 RID: 2404
		InvalidTrustSetting = -25242,
		// Token: 0x04000965 RID: 2405
		NoAccessForItem = -25243,
		// Token: 0x04000966 RID: 2406
		InvalidOwnerEdit = -25244,
		// Token: 0x04000967 RID: 2407
		TrustNotAvailable = -25245,
		// Token: 0x04000968 RID: 2408
		UnsupportedFormat = -25256,
		// Token: 0x04000969 RID: 2409
		UnknownFormat = -25257,
		// Token: 0x0400096A RID: 2410
		KeyIsSensitive = -25258,
		// Token: 0x0400096B RID: 2411
		MultiplePrivateKeys = -25259,
		// Token: 0x0400096C RID: 2412
		PassphraseRequired = -25260,
		// Token: 0x0400096D RID: 2413
		InvalidPasswordRef = -25261,
		// Token: 0x0400096E RID: 2414
		InvalidTrustSettings = -25262,
		// Token: 0x0400096F RID: 2415
		NoTrustSettings = -25263,
		// Token: 0x04000970 RID: 2416
		Pkcs12VerifyFailure = -25264,
		// Token: 0x04000971 RID: 2417
		NotSigner = -26267,
		// Token: 0x04000972 RID: 2418
		ServiceNotAvailable = -67585,
		// Token: 0x04000973 RID: 2419
		InsufficientClientID = -67586,
		// Token: 0x04000974 RID: 2420
		DeviceReset = -67587,
		// Token: 0x04000975 RID: 2421
		DeviceFailed = -67588,
		// Token: 0x04000976 RID: 2422
		AppleAddAppACLSubject = -67589,
		// Token: 0x04000977 RID: 2423
		ApplePublicKeyIncomplete = -67590,
		// Token: 0x04000978 RID: 2424
		AppleSignatureMismatch = -67591,
		// Token: 0x04000979 RID: 2425
		AppleInvalidKeyStartDate = -67592,
		// Token: 0x0400097A RID: 2426
		AppleInvalidKeyEndDate = -67593,
		// Token: 0x0400097B RID: 2427
		ConversionError = -67594,
		// Token: 0x0400097C RID: 2428
		AppleSSLv2Rollback = -67595,
		// Token: 0x0400097D RID: 2429
		QuotaExceeded = -67596,
		// Token: 0x0400097E RID: 2430
		FileTooBig = -67597,
		// Token: 0x0400097F RID: 2431
		InvalidDatabaseBlob = -67598,
		// Token: 0x04000980 RID: 2432
		InvalidKeyBlob = -67599,
		// Token: 0x04000981 RID: 2433
		IncompatibleDatabaseBlob = -67600,
		// Token: 0x04000982 RID: 2434
		IncompatibleKeyBlob = -67601,
		// Token: 0x04000983 RID: 2435
		HostNameMismatch = -67602,
		// Token: 0x04000984 RID: 2436
		UnknownCriticalExtensionFlag = -67603,
		// Token: 0x04000985 RID: 2437
		NoBasicConstraints = -67604,
		// Token: 0x04000986 RID: 2438
		NoBasicConstraintsCA = -67605,
		// Token: 0x04000987 RID: 2439
		InvalidAuthorityKeyID = -67606,
		// Token: 0x04000988 RID: 2440
		InvalidSubjectKeyID = -67607,
		// Token: 0x04000989 RID: 2441
		InvalidKeyUsageForPolicy = -67608,
		// Token: 0x0400098A RID: 2442
		InvalidExtendedKeyUsage = -67609,
		// Token: 0x0400098B RID: 2443
		InvalidIDLinkage = -67610,
		// Token: 0x0400098C RID: 2444
		PathLengthConstraintExceeded = -67611,
		// Token: 0x0400098D RID: 2445
		InvalidRoot = -67612,
		// Token: 0x0400098E RID: 2446
		CRLExpired = -67613,
		// Token: 0x0400098F RID: 2447
		CRLNotValidYet = -67614,
		// Token: 0x04000990 RID: 2448
		CRLNotFound = -67615,
		// Token: 0x04000991 RID: 2449
		CRLServerDown = -67616,
		// Token: 0x04000992 RID: 2450
		CRLBadURI = -67617,
		// Token: 0x04000993 RID: 2451
		UnknownCertExtension = -67618,
		// Token: 0x04000994 RID: 2452
		UnknownCRLExtension = -67619,
		// Token: 0x04000995 RID: 2453
		CRLNotTrusted = -67620,
		// Token: 0x04000996 RID: 2454
		CRLPolicyFailed = -67621,
		// Token: 0x04000997 RID: 2455
		IDPFailure = -67622,
		// Token: 0x04000998 RID: 2456
		SMIMEEmailAddressesNotFound = -67623,
		// Token: 0x04000999 RID: 2457
		SMIMEBadExtendedKeyUsage = -67624,
		// Token: 0x0400099A RID: 2458
		SMIMEBadKeyUsage = -67625,
		// Token: 0x0400099B RID: 2459
		SMIMEKeyUsageNotCritical = -67626,
		// Token: 0x0400099C RID: 2460
		SMIMENoEmailAddress = -67627,
		// Token: 0x0400099D RID: 2461
		SMIMESubjAltNameNotCritical = -67628,
		// Token: 0x0400099E RID: 2462
		SSLBadExtendedKeyUsage = -67629,
		// Token: 0x0400099F RID: 2463
		OCSPBadResponse = -67630,
		// Token: 0x040009A0 RID: 2464
		OCSPBadRequest = -67631,
		// Token: 0x040009A1 RID: 2465
		OCSPUnavailable = -67632,
		// Token: 0x040009A2 RID: 2466
		OCSPStatusUnrecognized = -67633,
		// Token: 0x040009A3 RID: 2467
		EndOfData = -67634,
		// Token: 0x040009A4 RID: 2468
		IncompleteCertRevocationCheck = -67635,
		// Token: 0x040009A5 RID: 2469
		NetworkFailure = -67636,
		// Token: 0x040009A6 RID: 2470
		OCSPNotTrustedToAnchor = -67637,
		// Token: 0x040009A7 RID: 2471
		RecordModified = -67638,
		// Token: 0x040009A8 RID: 2472
		OCSPSignatureError = -67639,
		// Token: 0x040009A9 RID: 2473
		OCSPNoSigner = -67640,
		// Token: 0x040009AA RID: 2474
		OCSPResponderMalformedReq = -67641,
		// Token: 0x040009AB RID: 2475
		OCSPResponderInternalError = -67642,
		// Token: 0x040009AC RID: 2476
		OCSPResponderTryLater = -67643,
		// Token: 0x040009AD RID: 2477
		OCSPResponderSignatureRequired = -67644,
		// Token: 0x040009AE RID: 2478
		OCSPResponderUnauthorized = -67645,
		// Token: 0x040009AF RID: 2479
		OCSPResponseNonceMismatch = -67646,
		// Token: 0x040009B0 RID: 2480
		CodeSigningBadCertChainLength = -67647,
		// Token: 0x040009B1 RID: 2481
		CodeSigningNoBasicConstraints = -67648,
		// Token: 0x040009B2 RID: 2482
		CodeSigningBadPathLengthConstraint = -67649,
		// Token: 0x040009B3 RID: 2483
		CodeSigningNoExtendedKeyUsage = -67650,
		// Token: 0x040009B4 RID: 2484
		CodeSigningDevelopment = -67651,
		// Token: 0x040009B5 RID: 2485
		ResourceSignBadCertChainLength = -67652,
		// Token: 0x040009B6 RID: 2486
		ResourceSignBadExtKeyUsage = -67653,
		// Token: 0x040009B7 RID: 2487
		TrustSettingDeny = -67654,
		// Token: 0x040009B8 RID: 2488
		InvalidSubjectName = -67655,
		// Token: 0x040009B9 RID: 2489
		UnknownQualifiedCertStatement = -67656,
		// Token: 0x040009BA RID: 2490
		MobileMeRequestQueued = -67657,
		// Token: 0x040009BB RID: 2491
		MobileMeRequestRedirected = -67658,
		// Token: 0x040009BC RID: 2492
		MobileMeServerError = -67659,
		// Token: 0x040009BD RID: 2493
		MobileMeServerNotAvailable = -67660,
		// Token: 0x040009BE RID: 2494
		MobileMeServerAlreadyExists = -67661,
		// Token: 0x040009BF RID: 2495
		MobileMeServerServiceErr = -67662,
		// Token: 0x040009C0 RID: 2496
		MobileMeRequestAlreadyPending = -67663,
		// Token: 0x040009C1 RID: 2497
		MobileMeNoRequestPending = -67664,
		// Token: 0x040009C2 RID: 2498
		MobileMeCSRVerifyFailure = -67665,
		// Token: 0x040009C3 RID: 2499
		MobileMeFailedConsistencyCheck = -67666,
		// Token: 0x040009C4 RID: 2500
		NotInitialized = -67667,
		// Token: 0x040009C5 RID: 2501
		InvalidHandleUsage = -67668,
		// Token: 0x040009C6 RID: 2502
		PVCReferentNotFound = -67669,
		// Token: 0x040009C7 RID: 2503
		FunctionIntegrityFail = -67670,
		// Token: 0x040009C8 RID: 2504
		InternalError = -67671,
		// Token: 0x040009C9 RID: 2505
		MemoryError = -67672,
		// Token: 0x040009CA RID: 2506
		InvalidData = -67673,
		// Token: 0x040009CB RID: 2507
		MDSError = -67674,
		// Token: 0x040009CC RID: 2508
		InvalidPointer = -67675,
		// Token: 0x040009CD RID: 2509
		SelfCheckFailed = -67676,
		// Token: 0x040009CE RID: 2510
		FunctionFailed = -67677,
		// Token: 0x040009CF RID: 2511
		ModuleManifestVerifyFailed = -67678,
		// Token: 0x040009D0 RID: 2512
		InvalidGUID = -67679,
		// Token: 0x040009D1 RID: 2513
		InvalidHandle = -67680,
		// Token: 0x040009D2 RID: 2514
		InvalidDBList = -67681,
		// Token: 0x040009D3 RID: 2515
		InvalidPassthroughID = -67682,
		// Token: 0x040009D4 RID: 2516
		InvalidNetworkAddress = -67683,
		// Token: 0x040009D5 RID: 2517
		CRLAlreadySigned = -67684,
		// Token: 0x040009D6 RID: 2518
		InvalidNumberOfFields = -67685,
		// Token: 0x040009D7 RID: 2519
		VerificationFailure = -67686,
		// Token: 0x040009D8 RID: 2520
		UnknownTag = -67687,
		// Token: 0x040009D9 RID: 2521
		InvalidSignature = -67688,
		// Token: 0x040009DA RID: 2522
		InvalidName = -67689,
		// Token: 0x040009DB RID: 2523
		InvalidCertificateRef = -67690,
		// Token: 0x040009DC RID: 2524
		InvalidCertificateGroup = -67691,
		// Token: 0x040009DD RID: 2525
		TagNotFound = -67692,
		// Token: 0x040009DE RID: 2526
		InvalidQuery = -67693,
		// Token: 0x040009DF RID: 2527
		InvalidValue = -67694,
		// Token: 0x040009E0 RID: 2528
		CallbackFailed = -67695,
		// Token: 0x040009E1 RID: 2529
		ACLDeleteFailed = -67696,
		// Token: 0x040009E2 RID: 2530
		ACLReplaceFailed = -67697,
		// Token: 0x040009E3 RID: 2531
		ACLAddFailed = -67698,
		// Token: 0x040009E4 RID: 2532
		ACLChangeFailed = -67699,
		// Token: 0x040009E5 RID: 2533
		InvalidAccessCredentials = -67700,
		// Token: 0x040009E6 RID: 2534
		InvalidRecord = -67701,
		// Token: 0x040009E7 RID: 2535
		InvalidACL = -67702,
		// Token: 0x040009E8 RID: 2536
		InvalidSampleValue = -67703,
		// Token: 0x040009E9 RID: 2537
		IncompatibleVersion = -67704,
		// Token: 0x040009EA RID: 2538
		PrivilegeNotGranted = -67705,
		// Token: 0x040009EB RID: 2539
		InvalidScope = -67706,
		// Token: 0x040009EC RID: 2540
		PVCAlreadyConfigured = -67707,
		// Token: 0x040009ED RID: 2541
		InvalidPVC = -67708,
		// Token: 0x040009EE RID: 2542
		EMMLoadFailed = -67709,
		// Token: 0x040009EF RID: 2543
		EMMUnloadFailed = -67710,
		// Token: 0x040009F0 RID: 2544
		AddinLoadFailed = -67711,
		// Token: 0x040009F1 RID: 2545
		InvalidKeyRef = -67712,
		// Token: 0x040009F2 RID: 2546
		InvalidKeyHierarchy = -67713,
		// Token: 0x040009F3 RID: 2547
		AddinUnloadFailed = -67714,
		// Token: 0x040009F4 RID: 2548
		LibraryReferenceNotFound = -67715,
		// Token: 0x040009F5 RID: 2549
		InvalidAddinFunctionTable = -67716,
		// Token: 0x040009F6 RID: 2550
		InvalidServiceMask = -67717,
		// Token: 0x040009F7 RID: 2551
		ModuleNotLoaded = -67718,
		// Token: 0x040009F8 RID: 2552
		InvalidSubServiceID = -67719,
		// Token: 0x040009F9 RID: 2553
		AttributeNotInContext = -67720,
		// Token: 0x040009FA RID: 2554
		ModuleManagerInitializeFailed = -67721,
		// Token: 0x040009FB RID: 2555
		ModuleManagerNotFound = -67722,
		// Token: 0x040009FC RID: 2556
		EventNotificationCallbackNotFound = -67723,
		// Token: 0x040009FD RID: 2557
		InputLengthError = -67724,
		// Token: 0x040009FE RID: 2558
		OutputLengthError = -67725,
		// Token: 0x040009FF RID: 2559
		PrivilegeNotSupported = -67726,
		// Token: 0x04000A00 RID: 2560
		DeviceError = -67727,
		// Token: 0x04000A01 RID: 2561
		AttachHandleBusy = -67728,
		// Token: 0x04000A02 RID: 2562
		NotLoggedIn = -67729,
		// Token: 0x04000A03 RID: 2563
		AlgorithmMismatch = -67730,
		// Token: 0x04000A04 RID: 2564
		KeyUsageIncorrect = -67731,
		// Token: 0x04000A05 RID: 2565
		KeyBlobTypeIncorrect = -67732,
		// Token: 0x04000A06 RID: 2566
		KeyHeaderInconsistent = -67733,
		// Token: 0x04000A07 RID: 2567
		UnsupportedKeyFormat = -67734,
		// Token: 0x04000A08 RID: 2568
		UnsupportedKeySize = -67735,
		// Token: 0x04000A09 RID: 2569
		InvalidKeyUsageMask = -67736,
		// Token: 0x04000A0A RID: 2570
		UnsupportedKeyUsageMask = -67737,
		// Token: 0x04000A0B RID: 2571
		InvalidKeyAttributeMask = -67738,
		// Token: 0x04000A0C RID: 2572
		UnsupportedKeyAttributeMask = -67739,
		// Token: 0x04000A0D RID: 2573
		InvalidKeyLabel = -67740,
		// Token: 0x04000A0E RID: 2574
		UnsupportedKeyLabel = -67741,
		// Token: 0x04000A0F RID: 2575
		InvalidKeyFormat = -67742,
		// Token: 0x04000A10 RID: 2576
		UnsupportedVectorOfBuffers = -67743,
		// Token: 0x04000A11 RID: 2577
		InvalidInputVector = -67744,
		// Token: 0x04000A12 RID: 2578
		InvalidOutputVector = -67745,
		// Token: 0x04000A13 RID: 2579
		InvalidContext = -67746,
		// Token: 0x04000A14 RID: 2580
		InvalidAlgorithm = -67747,
		// Token: 0x04000A15 RID: 2581
		InvalidAttributeKey = -67748,
		// Token: 0x04000A16 RID: 2582
		MissingAttributeKey = -67749,
		// Token: 0x04000A17 RID: 2583
		InvalidAttributeInitVector = -67750,
		// Token: 0x04000A18 RID: 2584
		MissingAttributeInitVector = -67751,
		// Token: 0x04000A19 RID: 2585
		InvalidAttributeSalt = -67752,
		// Token: 0x04000A1A RID: 2586
		MissingAttributeSalt = -67753,
		// Token: 0x04000A1B RID: 2587
		InvalidAttributePadding = -67754,
		// Token: 0x04000A1C RID: 2588
		MissingAttributePadding = -67755,
		// Token: 0x04000A1D RID: 2589
		InvalidAttributeRandom = -67756,
		// Token: 0x04000A1E RID: 2590
		MissingAttributeRandom = -67757,
		// Token: 0x04000A1F RID: 2591
		InvalidAttributeSeed = -67758,
		// Token: 0x04000A20 RID: 2592
		MissingAttributeSeed = -67759,
		// Token: 0x04000A21 RID: 2593
		InvalidAttributePassphrase = -67760,
		// Token: 0x04000A22 RID: 2594
		MissingAttributePassphrase = -67761,
		// Token: 0x04000A23 RID: 2595
		InvalidAttributeKeyLength = -67762,
		// Token: 0x04000A24 RID: 2596
		MissingAttributeKeyLength = -67763,
		// Token: 0x04000A25 RID: 2597
		InvalidAttributeBlockSize = -67764,
		// Token: 0x04000A26 RID: 2598
		MissingAttributeBlockSize = -67765,
		// Token: 0x04000A27 RID: 2599
		InvalidAttributeOutputSize = -67766,
		// Token: 0x04000A28 RID: 2600
		MissingAttributeOutputSize = -67767,
		// Token: 0x04000A29 RID: 2601
		InvalidAttributeRounds = -67768,
		// Token: 0x04000A2A RID: 2602
		MissingAttributeRounds = -67769,
		// Token: 0x04000A2B RID: 2603
		InvalidAlgorithmParms = -67770,
		// Token: 0x04000A2C RID: 2604
		MissingAlgorithmParms = -67771,
		// Token: 0x04000A2D RID: 2605
		InvalidAttributeLabel = -67772,
		// Token: 0x04000A2E RID: 2606
		MissingAttributeLabel = -67773,
		// Token: 0x04000A2F RID: 2607
		InvalidAttributeKeyType = -67774,
		// Token: 0x04000A30 RID: 2608
		MissingAttributeKeyType = -67775,
		// Token: 0x04000A31 RID: 2609
		InvalidAttributeMode = -67776,
		// Token: 0x04000A32 RID: 2610
		MissingAttributeMode = -67777,
		// Token: 0x04000A33 RID: 2611
		InvalidAttributeEffectiveBits = -67778,
		// Token: 0x04000A34 RID: 2612
		MissingAttributeEffectiveBits = -67779,
		// Token: 0x04000A35 RID: 2613
		InvalidAttributeStartDate = -67780,
		// Token: 0x04000A36 RID: 2614
		MissingAttributeStartDate = -67781,
		// Token: 0x04000A37 RID: 2615
		InvalidAttributeEndDate = -67782,
		// Token: 0x04000A38 RID: 2616
		MissingAttributeEndDate = -67783,
		// Token: 0x04000A39 RID: 2617
		InvalidAttributeVersion = -67784,
		// Token: 0x04000A3A RID: 2618
		MissingAttributeVersion = -67785,
		// Token: 0x04000A3B RID: 2619
		InvalidAttributePrime = -67786,
		// Token: 0x04000A3C RID: 2620
		MissingAttributePrime = -67787,
		// Token: 0x04000A3D RID: 2621
		InvalidAttributeBase = -67788,
		// Token: 0x04000A3E RID: 2622
		MissingAttributeBase = -67789,
		// Token: 0x04000A3F RID: 2623
		InvalidAttributeSubprime = -67790,
		// Token: 0x04000A40 RID: 2624
		MissingAttributeSubprime = -67791,
		// Token: 0x04000A41 RID: 2625
		InvalidAttributeIterationCount = -67792,
		// Token: 0x04000A42 RID: 2626
		MissingAttributeIterationCount = -67793,
		// Token: 0x04000A43 RID: 2627
		InvalidAttributeDLDBHandle = -67794,
		// Token: 0x04000A44 RID: 2628
		MissingAttributeDLDBHandle = -67795,
		// Token: 0x04000A45 RID: 2629
		InvalidAttributeAccessCredentials = -67796,
		// Token: 0x04000A46 RID: 2630
		MissingAttributeAccessCredentials = -67797,
		// Token: 0x04000A47 RID: 2631
		InvalidAttributePublicKeyFormat = -67798,
		// Token: 0x04000A48 RID: 2632
		MissingAttributePublicKeyFormat = -67799,
		// Token: 0x04000A49 RID: 2633
		InvalidAttributePrivateKeyFormat = -67800,
		// Token: 0x04000A4A RID: 2634
		MissingAttributePrivateKeyFormat = -67801,
		// Token: 0x04000A4B RID: 2635
		InvalidAttributeSymmetricKeyFormat = -67802,
		// Token: 0x04000A4C RID: 2636
		MissingAttributeSymmetricKeyFormat = -67803,
		// Token: 0x04000A4D RID: 2637
		InvalidAttributeWrappedKeyFormat = -67804,
		// Token: 0x04000A4E RID: 2638
		MissingAttributeWrappedKeyFormat = -67805,
		// Token: 0x04000A4F RID: 2639
		StagedOperationInProgress = -67806,
		// Token: 0x04000A50 RID: 2640
		StagedOperationNotStarted = -67807,
		// Token: 0x04000A51 RID: 2641
		VerifyFailed = -67808,
		// Token: 0x04000A52 RID: 2642
		QuerySizeUnknown = -67809,
		// Token: 0x04000A53 RID: 2643
		BlockSizeMismatch = -67810,
		// Token: 0x04000A54 RID: 2644
		PublicKeyInconsistent = -67811,
		// Token: 0x04000A55 RID: 2645
		DeviceVerifyFailed = -67812,
		// Token: 0x04000A56 RID: 2646
		InvalidLoginName = -67813,
		// Token: 0x04000A57 RID: 2647
		AlreadyLoggedIn = -67814,
		// Token: 0x04000A58 RID: 2648
		InvalidDigestAlgorithm = -67815,
		// Token: 0x04000A59 RID: 2649
		InvalidCRLGroup = -67816,
		// Token: 0x04000A5A RID: 2650
		CertificateCannotOperate = -67817,
		// Token: 0x04000A5B RID: 2651
		CertificateExpired = -67818,
		// Token: 0x04000A5C RID: 2652
		CertificateNotValidYet = -67819,
		// Token: 0x04000A5D RID: 2653
		CertificateRevoked = -67820,
		// Token: 0x04000A5E RID: 2654
		CertificateSuspended = -67821,
		// Token: 0x04000A5F RID: 2655
		InsufficientCredentials = -67822,
		// Token: 0x04000A60 RID: 2656
		InvalidAction = -67823,
		// Token: 0x04000A61 RID: 2657
		InvalidAuthority = -67824,
		// Token: 0x04000A62 RID: 2658
		VerifyActionFailed = -67825,
		// Token: 0x04000A63 RID: 2659
		InvalidCertAuthority = -67826,
		// Token: 0x04000A64 RID: 2660
		InvalidCRLAuthority = -67827,
		// Token: 0x04000A65 RID: 2661
		InvalidCRLEncoding = -67828,
		// Token: 0x04000A66 RID: 2662
		InvalidCRLType = -67829,
		// Token: 0x04000A67 RID: 2663
		InvalidCRL = -67830,
		// Token: 0x04000A68 RID: 2664
		InvalidFormType = -67831,
		// Token: 0x04000A69 RID: 2665
		InvalidID = -67832,
		// Token: 0x04000A6A RID: 2666
		InvalidIdentifier = -67833,
		// Token: 0x04000A6B RID: 2667
		InvalidIndex = -67834,
		// Token: 0x04000A6C RID: 2668
		InvalidPolicyIdentifiers = -67835,
		// Token: 0x04000A6D RID: 2669
		InvalidTimeString = -67836,
		// Token: 0x04000A6E RID: 2670
		InvalidReason = -67837,
		// Token: 0x04000A6F RID: 2671
		InvalidRequestInputs = -67838,
		// Token: 0x04000A70 RID: 2672
		InvalidResponseVector = -67839,
		// Token: 0x04000A71 RID: 2673
		InvalidStopOnPolicy = -67840,
		// Token: 0x04000A72 RID: 2674
		InvalidTuple = -67841,
		// Token: 0x04000A73 RID: 2675
		MultipleValuesUnsupported = -67842,
		// Token: 0x04000A74 RID: 2676
		NotTrusted = -67843,
		// Token: 0x04000A75 RID: 2677
		NoDefaultAuthority = -67844,
		// Token: 0x04000A76 RID: 2678
		RejectedForm = -67845,
		// Token: 0x04000A77 RID: 2679
		RequestLost = -67846,
		// Token: 0x04000A78 RID: 2680
		RequestRejected = -67847,
		// Token: 0x04000A79 RID: 2681
		UnsupportedAddressType = -67848,
		// Token: 0x04000A7A RID: 2682
		UnsupportedService = -67849,
		// Token: 0x04000A7B RID: 2683
		InvalidTupleGroup = -67850,
		// Token: 0x04000A7C RID: 2684
		InvalidBaseACLs = -67851,
		// Token: 0x04000A7D RID: 2685
		InvalidTupleCredentials = -67852,
		// Token: 0x04000A7E RID: 2686
		InvalidEncoding = -67853,
		// Token: 0x04000A7F RID: 2687
		InvalidValidityPeriod = -67854,
		// Token: 0x04000A80 RID: 2688
		InvalidRequestor = -67855,
		// Token: 0x04000A81 RID: 2689
		RequestDescriptor = -67856,
		// Token: 0x04000A82 RID: 2690
		InvalidBundleInfo = -67857,
		// Token: 0x04000A83 RID: 2691
		InvalidCRLIndex = -67858,
		// Token: 0x04000A84 RID: 2692
		NoFieldValues = -67859,
		// Token: 0x04000A85 RID: 2693
		UnsupportedFieldFormat = -67860,
		// Token: 0x04000A86 RID: 2694
		UnsupportedIndexInfo = -67861,
		// Token: 0x04000A87 RID: 2695
		UnsupportedLocality = -67862,
		// Token: 0x04000A88 RID: 2696
		UnsupportedNumAttributes = -67863,
		// Token: 0x04000A89 RID: 2697
		UnsupportedNumIndexes = -67864,
		// Token: 0x04000A8A RID: 2698
		UnsupportedNumRecordTypes = -67865,
		// Token: 0x04000A8B RID: 2699
		FieldSpecifiedMultiple = -67866,
		// Token: 0x04000A8C RID: 2700
		IncompatibleFieldFormat = -67867,
		// Token: 0x04000A8D RID: 2701
		InvalidParsingModule = -67868,
		// Token: 0x04000A8E RID: 2702
		DatabaseLocked = -67869,
		// Token: 0x04000A8F RID: 2703
		DatastoreIsOpen = -67870,
		// Token: 0x04000A90 RID: 2704
		MissingValue = -67871,
		// Token: 0x04000A91 RID: 2705
		UnsupportedQueryLimits = -67872,
		// Token: 0x04000A92 RID: 2706
		UnsupportedNumSelectionPreds = -67873,
		// Token: 0x04000A93 RID: 2707
		UnsupportedOperator = -67874,
		// Token: 0x04000A94 RID: 2708
		InvalidDBLocation = -67875,
		// Token: 0x04000A95 RID: 2709
		InvalidAccessRequest = -67876,
		// Token: 0x04000A96 RID: 2710
		InvalidIndexInfo = -67877,
		// Token: 0x04000A97 RID: 2711
		InvalidNewOwner = -67878,
		// Token: 0x04000A98 RID: 2712
		InvalidModifyMode = -67879,
		// Token: 0x04000A99 RID: 2713
		MissingRequiredExtension = -67880,
		// Token: 0x04000A9A RID: 2714
		ExtendedKeyUsageNotCritical = -67881,
		// Token: 0x04000A9B RID: 2715
		TimestampMissing = -67882,
		// Token: 0x04000A9C RID: 2716
		TimestampInvalid = -67883,
		// Token: 0x04000A9D RID: 2717
		TimestampNotTrusted = -67884,
		// Token: 0x04000A9E RID: 2718
		TimestampServiceNotAvailable = -67885,
		// Token: 0x04000A9F RID: 2719
		TimestampBadAlg = -67886,
		// Token: 0x04000AA0 RID: 2720
		TimestampBadRequest = -67887,
		// Token: 0x04000AA1 RID: 2721
		TimestampBadDataFormat = -67888,
		// Token: 0x04000AA2 RID: 2722
		TimestampTimeNotAvailable = -67889,
		// Token: 0x04000AA3 RID: 2723
		TimestampUnacceptedPolicy = -67890,
		// Token: 0x04000AA4 RID: 2724
		TimestampUnacceptedExtension = -67891,
		// Token: 0x04000AA5 RID: 2725
		TimestampAddInfoNotAvailable = -67892,
		// Token: 0x04000AA6 RID: 2726
		TimestampSystemFailure = -67893,
		// Token: 0x04000AA7 RID: 2727
		SigningTimeMissing = -67894,
		// Token: 0x04000AA8 RID: 2728
		TimestampRejection = -67895,
		// Token: 0x04000AA9 RID: 2729
		TimestampWaiting = -67896,
		// Token: 0x04000AAA RID: 2730
		TimestampRevocationWarning = -67897,
		// Token: 0x04000AAB RID: 2731
		TimestampRevocationNotification = -67898
	}
}
