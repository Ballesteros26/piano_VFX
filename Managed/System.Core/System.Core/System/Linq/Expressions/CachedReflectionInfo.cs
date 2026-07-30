using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x0200021E RID: 542
	internal static class CachedReflectionInfo
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x0002C414 File Offset: 0x0002A614
		public static MethodInfo String_Format_String_ObjectArray
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_String_Format_String_ObjectArray) == null)
				{
					methodInfo = (CachedReflectionInfo.s_String_Format_String_ObjectArray = typeof(string).GetMethod("Format", new Type[]
					{
						typeof(string),
						typeof(object[])
					}));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x0002C464 File Offset: 0x0002A664
		public static ConstructorInfo InvalidCastException_Ctor_String
		{
			get
			{
				ConstructorInfo constructorInfo;
				if ((constructorInfo = CachedReflectionInfo.s_InvalidCastException_Ctor_String) == null)
				{
					constructorInfo = (CachedReflectionInfo.s_InvalidCastException_Ctor_String = typeof(InvalidCastException).GetConstructor(new Type[] { typeof(string) }));
				}
				return constructorInfo;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x0002C497 File Offset: 0x0002A697
		public static MethodInfo CallSiteOps_SetNotMatched
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_SetNotMatched) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_SetNotMatched = typeof(CallSiteOps).GetMethod("SetNotMatched"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x0002C4BC File Offset: 0x0002A6BC
		public static MethodInfo CallSiteOps_CreateMatchmaker
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_CreateMatchmaker) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_CreateMatchmaker = typeof(CallSiteOps).GetMethod("CreateMatchmaker"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x0002C4E1 File Offset: 0x0002A6E1
		public static MethodInfo CallSiteOps_GetMatch
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_GetMatch) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_GetMatch = typeof(CallSiteOps).GetMethod("GetMatch"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x0002C506 File Offset: 0x0002A706
		public static MethodInfo CallSiteOps_ClearMatch
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_ClearMatch) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_ClearMatch = typeof(CallSiteOps).GetMethod("ClearMatch"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x0002C52B File Offset: 0x0002A72B
		public static MethodInfo CallSiteOps_UpdateRules
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_UpdateRules) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_UpdateRules = typeof(CallSiteOps).GetMethod("UpdateRules"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x0002C550 File Offset: 0x0002A750
		public static MethodInfo CallSiteOps_GetRules
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_GetRules) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_GetRules = typeof(CallSiteOps).GetMethod("GetRules"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x0002C575 File Offset: 0x0002A775
		public static MethodInfo CallSiteOps_GetRuleCache
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_GetRuleCache) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_GetRuleCache = typeof(CallSiteOps).GetMethod("GetRuleCache"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x0002C59A File Offset: 0x0002A79A
		public static MethodInfo CallSiteOps_GetCachedRules
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_GetCachedRules) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_GetCachedRules = typeof(CallSiteOps).GetMethod("GetCachedRules"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x0002C5BF File Offset: 0x0002A7BF
		public static MethodInfo CallSiteOps_AddRule
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_AddRule) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_AddRule = typeof(CallSiteOps).GetMethod("AddRule"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x0002C5E4 File Offset: 0x0002A7E4
		public static MethodInfo CallSiteOps_MoveRule
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_MoveRule) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_MoveRule = typeof(CallSiteOps).GetMethod("MoveRule"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x0002C609 File Offset: 0x0002A809
		public static MethodInfo CallSiteOps_Bind
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_CallSiteOps_Bind) == null)
				{
					methodInfo = (CachedReflectionInfo.s_CallSiteOps_Bind = typeof(CallSiteOps).GetMethod("Bind"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x0002C62E File Offset: 0x0002A82E
		public static MethodInfo DynamicObject_TryGetMember
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_DynamicObject_TryGetMember) == null)
				{
					methodInfo = (CachedReflectionInfo.s_DynamicObject_TryGetMember = typeof(DynamicObject).GetMethod("TryGetMember"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x0002C653 File Offset: 0x0002A853
		public static MethodInfo DynamicObject_TrySetMember
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_DynamicObject_TrySetMember) == null)
				{
					methodInfo = (CachedReflectionInfo.s_DynamicObject_TrySetMember = typeof(DynamicObject).GetMethod("TrySetMember"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x0002C678 File Offset: 0x0002A878
		public static MethodInfo DynamicObject_TryDeleteMember
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_DynamicObject_TryDeleteMember) == null)
				{
					methodInfo = (CachedReflectionInfo.s_DynamicObject_TryDeleteMember = typeof(DynamicObject).GetMethod("TryDeleteMember"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x0002C69D File Offset: 0x0002A89D
		public static MethodInfo DynamicObject_TryGetIndex
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_DynamicObject_TryGetIndex) == null)
				{
					methodInfo = (CachedReflectionInfo.s_DynamicObject_TryGetIndex = typeof(DynamicObject).GetMethod("TryGetIndex"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0002C6C2 File Offset: 0x0002A8C2
		public static MethodInfo DynamicObject_TrySetIndex
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_DynamicObject_TrySetIndex) == null)
				{
					methodInfo = (CachedReflectionInfo.s_DynamicObject_TrySetIndex = typeof(DynamicObject).GetMethod("TrySetIndex"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0002C6E7 File Offset: 0x0002A8E7
		public static MethodInfo DynamicObject_TryDeleteIndex
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_DynamicObject_TryDeleteIndex) == null)
				{
					methodInfo = (CachedReflectionInfo.s_DynamicObject_TryDeleteIndex = typeof(DynamicObject).GetMethod("TryDeleteIndex"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x0002C70C File Offset: 0x0002A90C
		public static MethodInfo DynamicObject_TryConvert
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_DynamicObject_TryConvert) == null)
				{
					methodInfo = (CachedReflectionInfo.s_DynamicObject_TryConvert = typeof(DynamicObject).GetMethod("TryConvert"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x0002C731 File Offset: 0x0002A931
		public static MethodInfo DynamicObject_TryInvoke
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_DynamicObject_TryInvoke) == null)
				{
					methodInfo = (CachedReflectionInfo.s_DynamicObject_TryInvoke = typeof(DynamicObject).GetMethod("TryInvoke"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x0002C756 File Offset: 0x0002A956
		public static MethodInfo DynamicObject_TryInvokeMember
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_DynamicObject_TryInvokeMember) == null)
				{
					methodInfo = (CachedReflectionInfo.s_DynamicObject_TryInvokeMember = typeof(DynamicObject).GetMethod("TryInvokeMember"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x0002C77B File Offset: 0x0002A97B
		public static MethodInfo DynamicObject_TryBinaryOperation
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_DynamicObject_TryBinaryOperation) == null)
				{
					methodInfo = (CachedReflectionInfo.s_DynamicObject_TryBinaryOperation = typeof(DynamicObject).GetMethod("TryBinaryOperation"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x0002C7A0 File Offset: 0x0002A9A0
		public static MethodInfo DynamicObject_TryUnaryOperation
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_DynamicObject_TryUnaryOperation) == null)
				{
					methodInfo = (CachedReflectionInfo.s_DynamicObject_TryUnaryOperation = typeof(DynamicObject).GetMethod("TryUnaryOperation"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0002C7C5 File Offset: 0x0002A9C5
		public static MethodInfo DynamicObject_TryCreateInstance
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_DynamicObject_TryCreateInstance) == null)
				{
					methodInfo = (CachedReflectionInfo.s_DynamicObject_TryCreateInstance = typeof(DynamicObject).GetMethod("TryCreateInstance"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0002C7EA File Offset: 0x0002A9EA
		public static ConstructorInfo Nullable_Boolean_Ctor
		{
			get
			{
				ConstructorInfo constructorInfo;
				if ((constructorInfo = CachedReflectionInfo.s_Nullable_Boolean_Ctor) == null)
				{
					constructorInfo = (CachedReflectionInfo.s_Nullable_Boolean_Ctor = typeof(bool?).GetConstructor(new Type[] { typeof(bool) }));
				}
				return constructorInfo;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0002C81D File Offset: 0x0002AA1D
		public static ConstructorInfo Decimal_Ctor_Int32
		{
			get
			{
				ConstructorInfo constructorInfo;
				if ((constructorInfo = CachedReflectionInfo.s_Decimal_Ctor_Int32) == null)
				{
					constructorInfo = (CachedReflectionInfo.s_Decimal_Ctor_Int32 = typeof(decimal).GetConstructor(new Type[] { typeof(int) }));
				}
				return constructorInfo;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0002C850 File Offset: 0x0002AA50
		public static ConstructorInfo Decimal_Ctor_UInt32
		{
			get
			{
				ConstructorInfo constructorInfo;
				if ((constructorInfo = CachedReflectionInfo.s_Decimal_Ctor_UInt32) == null)
				{
					constructorInfo = (CachedReflectionInfo.s_Decimal_Ctor_UInt32 = typeof(decimal).GetConstructor(new Type[] { typeof(uint) }));
				}
				return constructorInfo;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x0002C883 File Offset: 0x0002AA83
		public static ConstructorInfo Decimal_Ctor_Int64
		{
			get
			{
				ConstructorInfo constructorInfo;
				if ((constructorInfo = CachedReflectionInfo.s_Decimal_Ctor_Int64) == null)
				{
					constructorInfo = (CachedReflectionInfo.s_Decimal_Ctor_Int64 = typeof(decimal).GetConstructor(new Type[] { typeof(long) }));
				}
				return constructorInfo;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0002C8B6 File Offset: 0x0002AAB6
		public static ConstructorInfo Decimal_Ctor_UInt64
		{
			get
			{
				ConstructorInfo constructorInfo;
				if ((constructorInfo = CachedReflectionInfo.s_Decimal_Ctor_UInt64) == null)
				{
					constructorInfo = (CachedReflectionInfo.s_Decimal_Ctor_UInt64 = typeof(decimal).GetConstructor(new Type[] { typeof(ulong) }));
				}
				return constructorInfo;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x0002C8EC File Offset: 0x0002AAEC
		public static ConstructorInfo Decimal_Ctor_Int32_Int32_Int32_Bool_Byte
		{
			get
			{
				ConstructorInfo constructorInfo;
				if ((constructorInfo = CachedReflectionInfo.s_Decimal_Ctor_Int32_Int32_Int32_Bool_Byte) == null)
				{
					constructorInfo = (CachedReflectionInfo.s_Decimal_Ctor_Int32_Int32_Int32_Bool_Byte = typeof(decimal).GetConstructor(new Type[]
					{
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(bool),
						typeof(byte)
					}));
				}
				return constructorInfo;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0002C95E File Offset: 0x0002AB5E
		public static FieldInfo Decimal_One
		{
			get
			{
				FieldInfo fieldInfo;
				if ((fieldInfo = CachedReflectionInfo.s_Decimal_One) == null)
				{
					fieldInfo = (CachedReflectionInfo.s_Decimal_One = typeof(decimal).GetField("One"));
				}
				return fieldInfo;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x0002C983 File Offset: 0x0002AB83
		public static FieldInfo Decimal_MinusOne
		{
			get
			{
				FieldInfo fieldInfo;
				if ((fieldInfo = CachedReflectionInfo.s_Decimal_MinusOne) == null)
				{
					fieldInfo = (CachedReflectionInfo.s_Decimal_MinusOne = typeof(decimal).GetField("MinusOne"));
				}
				return fieldInfo;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0002C9A8 File Offset: 0x0002ABA8
		public static FieldInfo Decimal_MinValue
		{
			get
			{
				FieldInfo fieldInfo;
				if ((fieldInfo = CachedReflectionInfo.s_Decimal_MinValue) == null)
				{
					fieldInfo = (CachedReflectionInfo.s_Decimal_MinValue = typeof(decimal).GetField("MinValue"));
				}
				return fieldInfo;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x0002C9CD File Offset: 0x0002ABCD
		public static FieldInfo Decimal_MaxValue
		{
			get
			{
				FieldInfo fieldInfo;
				if ((fieldInfo = CachedReflectionInfo.s_Decimal_MaxValue) == null)
				{
					fieldInfo = (CachedReflectionInfo.s_Decimal_MaxValue = typeof(decimal).GetField("MaxValue"));
				}
				return fieldInfo;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0002C9F2 File Offset: 0x0002ABF2
		public static FieldInfo Decimal_Zero
		{
			get
			{
				FieldInfo fieldInfo;
				if ((fieldInfo = CachedReflectionInfo.s_Decimal_Zero) == null)
				{
					fieldInfo = (CachedReflectionInfo.s_Decimal_Zero = typeof(decimal).GetField("Zero"));
				}
				return fieldInfo;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x0002CA17 File Offset: 0x0002AC17
		public static FieldInfo DateTime_MinValue
		{
			get
			{
				FieldInfo fieldInfo;
				if ((fieldInfo = CachedReflectionInfo.s_DateTime_MinValue) == null)
				{
					fieldInfo = (CachedReflectionInfo.s_DateTime_MinValue = typeof(DateTime).GetField("MinValue"));
				}
				return fieldInfo;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0002CA3C File Offset: 0x0002AC3C
		public static MethodInfo MethodBase_GetMethodFromHandle_RuntimeMethodHandle
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_MethodBase_GetMethodFromHandle_RuntimeMethodHandle) == null)
				{
					methodInfo = (CachedReflectionInfo.s_MethodBase_GetMethodFromHandle_RuntimeMethodHandle = typeof(MethodBase).GetMethod("GetMethodFromHandle", new Type[] { typeof(RuntimeMethodHandle) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0002CA74 File Offset: 0x0002AC74
		public static MethodInfo MethodBase_GetMethodFromHandle_RuntimeMethodHandle_RuntimeTypeHandle
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_MethodBase_GetMethodFromHandle_RuntimeMethodHandle_RuntimeTypeHandle) == null)
				{
					methodInfo = (CachedReflectionInfo.s_MethodBase_GetMethodFromHandle_RuntimeMethodHandle_RuntimeTypeHandle = typeof(MethodBase).GetMethod("GetMethodFromHandle", new Type[]
					{
						typeof(RuntimeMethodHandle),
						typeof(RuntimeTypeHandle)
					}));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x0002CAC4 File Offset: 0x0002ACC4
		public static MethodInfo MethodInfo_CreateDelegate_Type_Object
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_MethodInfo_CreateDelegate_Type_Object) == null)
				{
					methodInfo = (CachedReflectionInfo.s_MethodInfo_CreateDelegate_Type_Object = typeof(MethodInfo).GetMethod("CreateDelegate", new Type[]
					{
						typeof(Type),
						typeof(object)
					}));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x0002CB14 File Offset: 0x0002AD14
		public static MethodInfo String_op_Equality_String_String
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_String_op_Equality_String_String) == null)
				{
					methodInfo = (CachedReflectionInfo.s_String_op_Equality_String_String = typeof(string).GetMethod("op_Equality", new Type[]
					{
						typeof(string),
						typeof(string)
					}));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x0002CB64 File Offset: 0x0002AD64
		public static MethodInfo String_Equals_String_String
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_String_Equals_String_String) == null)
				{
					methodInfo = (CachedReflectionInfo.s_String_Equals_String_String = typeof(string).GetMethod("Equals", new Type[]
					{
						typeof(string),
						typeof(string)
					}));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x0002CBB4 File Offset: 0x0002ADB4
		public static MethodInfo DictionaryOfStringInt32_Add_String_Int32
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_DictionaryOfStringInt32_Add_String_Int32) == null)
				{
					methodInfo = (CachedReflectionInfo.s_DictionaryOfStringInt32_Add_String_Int32 = typeof(Dictionary<string, int>).GetMethod("Add", new Type[]
					{
						typeof(string),
						typeof(int)
					}));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x0002CC04 File Offset: 0x0002AE04
		public static ConstructorInfo DictionaryOfStringInt32_Ctor_Int32
		{
			get
			{
				ConstructorInfo constructorInfo;
				if ((constructorInfo = CachedReflectionInfo.s_DictionaryOfStringInt32_Ctor_Int32) == null)
				{
					constructorInfo = (CachedReflectionInfo.s_DictionaryOfStringInt32_Ctor_Int32 = typeof(Dictionary<string, int>).GetConstructor(new Type[] { typeof(int) }));
				}
				return constructorInfo;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0002CC37 File Offset: 0x0002AE37
		public static MethodInfo Type_GetTypeFromHandle
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Type_GetTypeFromHandle) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Type_GetTypeFromHandle = typeof(Type).GetMethod("GetTypeFromHandle"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0002CC5C File Offset: 0x0002AE5C
		public static MethodInfo Object_GetType
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Object_GetType) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Object_GetType = typeof(object).GetMethod("GetType"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0002CC81 File Offset: 0x0002AE81
		public static MethodInfo Decimal_op_Implicit_Byte
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Decimal_op_Implicit_Byte) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Decimal_op_Implicit_Byte = typeof(decimal).GetMethod("op_Implicit", new Type[] { typeof(byte) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x0002CCB9 File Offset: 0x0002AEB9
		public static MethodInfo Decimal_op_Implicit_SByte
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Decimal_op_Implicit_SByte) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Decimal_op_Implicit_SByte = typeof(decimal).GetMethod("op_Implicit", new Type[] { typeof(sbyte) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x0002CCF1 File Offset: 0x0002AEF1
		public static MethodInfo Decimal_op_Implicit_Int16
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Decimal_op_Implicit_Int16) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Decimal_op_Implicit_Int16 = typeof(decimal).GetMethod("op_Implicit", new Type[] { typeof(short) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0002CD29 File Offset: 0x0002AF29
		public static MethodInfo Decimal_op_Implicit_UInt16
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Decimal_op_Implicit_UInt16) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Decimal_op_Implicit_UInt16 = typeof(decimal).GetMethod("op_Implicit", new Type[] { typeof(ushort) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0002CD61 File Offset: 0x0002AF61
		public static MethodInfo Decimal_op_Implicit_Int32
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Decimal_op_Implicit_Int32) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Decimal_op_Implicit_Int32 = typeof(decimal).GetMethod("op_Implicit", new Type[] { typeof(int) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x0002CD99 File Offset: 0x0002AF99
		public static MethodInfo Decimal_op_Implicit_UInt32
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Decimal_op_Implicit_UInt32) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Decimal_op_Implicit_UInt32 = typeof(decimal).GetMethod("op_Implicit", new Type[] { typeof(uint) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x0002CDD1 File Offset: 0x0002AFD1
		public static MethodInfo Decimal_op_Implicit_Int64
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Decimal_op_Implicit_Int64) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Decimal_op_Implicit_Int64 = typeof(decimal).GetMethod("op_Implicit", new Type[] { typeof(long) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x0002CE09 File Offset: 0x0002B009
		public static MethodInfo Decimal_op_Implicit_UInt64
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Decimal_op_Implicit_UInt64) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Decimal_op_Implicit_UInt64 = typeof(decimal).GetMethod("op_Implicit", new Type[] { typeof(ulong) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x0002CE41 File Offset: 0x0002B041
		public static MethodInfo Decimal_op_Implicit_Char
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Decimal_op_Implicit_Char) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Decimal_op_Implicit_Char = typeof(decimal).GetMethod("op_Implicit", new Type[] { typeof(char) }));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x0002CE7C File Offset: 0x0002B07C
		public static MethodInfo Math_Pow_Double_Double
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_Math_Pow_Double_Double) == null)
				{
					methodInfo = (CachedReflectionInfo.s_Math_Pow_Double_Double = typeof(Math).GetMethod("Pow", new Type[]
					{
						typeof(double),
						typeof(double)
					}));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x0002CECC File Offset: 0x0002B0CC
		public static ConstructorInfo Closure_ObjectArray_ObjectArray
		{
			get
			{
				ConstructorInfo constructorInfo;
				if ((constructorInfo = CachedReflectionInfo.s_Closure_ObjectArray_ObjectArray) == null)
				{
					constructorInfo = (CachedReflectionInfo.s_Closure_ObjectArray_ObjectArray = typeof(Closure).GetConstructor(new Type[]
					{
						typeof(object[]),
						typeof(object[])
					}));
				}
				return constructorInfo;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x0002CF0C File Offset: 0x0002B10C
		public static FieldInfo Closure_Constants
		{
			get
			{
				FieldInfo fieldInfo;
				if ((fieldInfo = CachedReflectionInfo.s_Closure_Constants) == null)
				{
					fieldInfo = (CachedReflectionInfo.s_Closure_Constants = typeof(Closure).GetField("Constants"));
				}
				return fieldInfo;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x0002CF31 File Offset: 0x0002B131
		public static FieldInfo Closure_Locals
		{
			get
			{
				FieldInfo fieldInfo;
				if ((fieldInfo = CachedReflectionInfo.s_Closure_Locals) == null)
				{
					fieldInfo = (CachedReflectionInfo.s_Closure_Locals = typeof(Closure).GetField("Locals"));
				}
				return fieldInfo;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x0002CF58 File Offset: 0x0002B158
		public static MethodInfo RuntimeOps_CreateRuntimeVariables_ObjectArray_Int64Array
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_RuntimeOps_CreateRuntimeVariables_ObjectArray_Int64Array) == null)
				{
					methodInfo = (CachedReflectionInfo.s_RuntimeOps_CreateRuntimeVariables_ObjectArray_Int64Array = typeof(RuntimeOps).GetMethod("CreateRuntimeVariables", new Type[]
					{
						typeof(object[]),
						typeof(long[])
					}));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x0002CFA8 File Offset: 0x0002B1A8
		public static MethodInfo RuntimeOps_CreateRuntimeVariables
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_RuntimeOps_CreateRuntimeVariables) == null)
				{
					methodInfo = (CachedReflectionInfo.s_RuntimeOps_CreateRuntimeVariables = typeof(RuntimeOps).GetMethod("CreateRuntimeVariables", Type.EmptyTypes));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x0002CFD2 File Offset: 0x0002B1D2
		public static MethodInfo RuntimeOps_MergeRuntimeVariables
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_RuntimeOps_MergeRuntimeVariables) == null)
				{
					methodInfo = (CachedReflectionInfo.s_RuntimeOps_MergeRuntimeVariables = typeof(RuntimeOps).GetMethod("MergeRuntimeVariables"));
				}
				return methodInfo;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x0002CFF7 File Offset: 0x0002B1F7
		public static MethodInfo RuntimeOps_Quote
		{
			get
			{
				MethodInfo methodInfo;
				if ((methodInfo = CachedReflectionInfo.s_RuntimeOps_Quote) == null)
				{
					methodInfo = (CachedReflectionInfo.s_RuntimeOps_Quote = typeof(RuntimeOps).GetMethod("Quote"));
				}
				return methodInfo;
			}
		}

		// Token: 0x04000842 RID: 2114
		private static MethodInfo s_String_Format_String_ObjectArray;

		// Token: 0x04000843 RID: 2115
		private static ConstructorInfo s_InvalidCastException_Ctor_String;

		// Token: 0x04000844 RID: 2116
		private static MethodInfo s_CallSiteOps_SetNotMatched;

		// Token: 0x04000845 RID: 2117
		private static MethodInfo s_CallSiteOps_CreateMatchmaker;

		// Token: 0x04000846 RID: 2118
		private static MethodInfo s_CallSiteOps_GetMatch;

		// Token: 0x04000847 RID: 2119
		private static MethodInfo s_CallSiteOps_ClearMatch;

		// Token: 0x04000848 RID: 2120
		private static MethodInfo s_CallSiteOps_UpdateRules;

		// Token: 0x04000849 RID: 2121
		private static MethodInfo s_CallSiteOps_GetRules;

		// Token: 0x0400084A RID: 2122
		private static MethodInfo s_CallSiteOps_GetRuleCache;

		// Token: 0x0400084B RID: 2123
		private static MethodInfo s_CallSiteOps_GetCachedRules;

		// Token: 0x0400084C RID: 2124
		private static MethodInfo s_CallSiteOps_AddRule;

		// Token: 0x0400084D RID: 2125
		private static MethodInfo s_CallSiteOps_MoveRule;

		// Token: 0x0400084E RID: 2126
		private static MethodInfo s_CallSiteOps_Bind;

		// Token: 0x0400084F RID: 2127
		private static MethodInfo s_DynamicObject_TryGetMember;

		// Token: 0x04000850 RID: 2128
		private static MethodInfo s_DynamicObject_TrySetMember;

		// Token: 0x04000851 RID: 2129
		private static MethodInfo s_DynamicObject_TryDeleteMember;

		// Token: 0x04000852 RID: 2130
		private static MethodInfo s_DynamicObject_TryGetIndex;

		// Token: 0x04000853 RID: 2131
		private static MethodInfo s_DynamicObject_TrySetIndex;

		// Token: 0x04000854 RID: 2132
		private static MethodInfo s_DynamicObject_TryDeleteIndex;

		// Token: 0x04000855 RID: 2133
		private static MethodInfo s_DynamicObject_TryConvert;

		// Token: 0x04000856 RID: 2134
		private static MethodInfo s_DynamicObject_TryInvoke;

		// Token: 0x04000857 RID: 2135
		private static MethodInfo s_DynamicObject_TryInvokeMember;

		// Token: 0x04000858 RID: 2136
		private static MethodInfo s_DynamicObject_TryBinaryOperation;

		// Token: 0x04000859 RID: 2137
		private static MethodInfo s_DynamicObject_TryUnaryOperation;

		// Token: 0x0400085A RID: 2138
		private static MethodInfo s_DynamicObject_TryCreateInstance;

		// Token: 0x0400085B RID: 2139
		private static ConstructorInfo s_Nullable_Boolean_Ctor;

		// Token: 0x0400085C RID: 2140
		private static ConstructorInfo s_Decimal_Ctor_Int32;

		// Token: 0x0400085D RID: 2141
		private static ConstructorInfo s_Decimal_Ctor_UInt32;

		// Token: 0x0400085E RID: 2142
		private static ConstructorInfo s_Decimal_Ctor_Int64;

		// Token: 0x0400085F RID: 2143
		private static ConstructorInfo s_Decimal_Ctor_UInt64;

		// Token: 0x04000860 RID: 2144
		private static ConstructorInfo s_Decimal_Ctor_Int32_Int32_Int32_Bool_Byte;

		// Token: 0x04000861 RID: 2145
		private static FieldInfo s_Decimal_One;

		// Token: 0x04000862 RID: 2146
		private static FieldInfo s_Decimal_MinusOne;

		// Token: 0x04000863 RID: 2147
		private static FieldInfo s_Decimal_MinValue;

		// Token: 0x04000864 RID: 2148
		private static FieldInfo s_Decimal_MaxValue;

		// Token: 0x04000865 RID: 2149
		private static FieldInfo s_Decimal_Zero;

		// Token: 0x04000866 RID: 2150
		private static FieldInfo s_DateTime_MinValue;

		// Token: 0x04000867 RID: 2151
		private static MethodInfo s_MethodBase_GetMethodFromHandle_RuntimeMethodHandle;

		// Token: 0x04000868 RID: 2152
		private static MethodInfo s_MethodBase_GetMethodFromHandle_RuntimeMethodHandle_RuntimeTypeHandle;

		// Token: 0x04000869 RID: 2153
		private static MethodInfo s_MethodInfo_CreateDelegate_Type_Object;

		// Token: 0x0400086A RID: 2154
		private static MethodInfo s_String_op_Equality_String_String;

		// Token: 0x0400086B RID: 2155
		private static MethodInfo s_String_Equals_String_String;

		// Token: 0x0400086C RID: 2156
		private static MethodInfo s_DictionaryOfStringInt32_Add_String_Int32;

		// Token: 0x0400086D RID: 2157
		private static ConstructorInfo s_DictionaryOfStringInt32_Ctor_Int32;

		// Token: 0x0400086E RID: 2158
		private static MethodInfo s_Type_GetTypeFromHandle;

		// Token: 0x0400086F RID: 2159
		private static MethodInfo s_Object_GetType;

		// Token: 0x04000870 RID: 2160
		private static MethodInfo s_Decimal_op_Implicit_Byte;

		// Token: 0x04000871 RID: 2161
		private static MethodInfo s_Decimal_op_Implicit_SByte;

		// Token: 0x04000872 RID: 2162
		private static MethodInfo s_Decimal_op_Implicit_Int16;

		// Token: 0x04000873 RID: 2163
		private static MethodInfo s_Decimal_op_Implicit_UInt16;

		// Token: 0x04000874 RID: 2164
		private static MethodInfo s_Decimal_op_Implicit_Int32;

		// Token: 0x04000875 RID: 2165
		private static MethodInfo s_Decimal_op_Implicit_UInt32;

		// Token: 0x04000876 RID: 2166
		private static MethodInfo s_Decimal_op_Implicit_Int64;

		// Token: 0x04000877 RID: 2167
		private static MethodInfo s_Decimal_op_Implicit_UInt64;

		// Token: 0x04000878 RID: 2168
		private static MethodInfo s_Decimal_op_Implicit_Char;

		// Token: 0x04000879 RID: 2169
		private static MethodInfo s_Math_Pow_Double_Double;

		// Token: 0x0400087A RID: 2170
		private static ConstructorInfo s_Closure_ObjectArray_ObjectArray;

		// Token: 0x0400087B RID: 2171
		private static FieldInfo s_Closure_Constants;

		// Token: 0x0400087C RID: 2172
		private static FieldInfo s_Closure_Locals;

		// Token: 0x0400087D RID: 2173
		private static MethodInfo s_RuntimeOps_CreateRuntimeVariables_ObjectArray_Int64Array;

		// Token: 0x0400087E RID: 2174
		private static MethodInfo s_RuntimeOps_CreateRuntimeVariables;

		// Token: 0x0400087F RID: 2175
		private static MethodInfo s_RuntimeOps_MergeRuntimeVariables;

		// Token: 0x04000880 RID: 2176
		private static MethodInfo s_RuntimeOps_Quote;
	}
}
