using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Provides field representations of the Microsoft Intermediate Language (MSIL) instructions for emission by the <see cref="T:System.Reflection.Emit.ILGenerator" /> class members (such as <see cref="M:System.Reflection.Emit.ILGenerator.Emit(System.Reflection.Emit.OpCode)" />).</summary>
	// Token: 0x02000372 RID: 882
	[ComVisible(true)]
	public class OpCodes
	{
		// Token: 0x06002852 RID: 10322 RVA: 0x00002111 File Offset: 0x00000311
		internal OpCodes()
		{
		}

		/// <summary>Returns true or false if the supplied opcode takes a single byte argument.</summary>
		/// <returns>True or false.</returns>
		/// <param name="inst">An instance of an Opcode object. </param>
		// Token: 0x06002853 RID: 10323 RVA: 0x0008F3A8 File Offset: 0x0008D5A8
		public static bool TakesSingleByteArgument(OpCode inst)
		{
			OperandType operandType = inst.OperandType;
			return operandType == OperandType.ShortInlineBrTarget || operandType == OperandType.ShortInlineI || operandType == OperandType.ShortInlineR || operandType == OperandType.ShortInlineVar;
		}

		/// <summary>Fills space if opcodes are patched. No meaningful operation is performed although a processing cycle can be consumed.</summary>
		// Token: 0x040014B3 RID: 5299
		public static readonly OpCode Nop = new OpCode(1179903, 84215041);

		/// <summary>Signals the Common Language Infrastructure (CLI) to inform the debugger that a break point has been tripped.</summary>
		// Token: 0x040014B4 RID: 5300
		public static readonly OpCode Break = new OpCode(1180159, 17106177);

		/// <summary>Loads the argument at index 0 onto the evaluation stack.</summary>
		// Token: 0x040014B5 RID: 5301
		public static readonly OpCode Ldarg_0 = new OpCode(1245951, 84214017);

		/// <summary>Loads the argument at index 1 onto the evaluation stack.</summary>
		// Token: 0x040014B6 RID: 5302
		public static readonly OpCode Ldarg_1 = new OpCode(1246207, 84214017);

		/// <summary>Loads the argument at index 2 onto the evaluation stack.</summary>
		// Token: 0x040014B7 RID: 5303
		public static readonly OpCode Ldarg_2 = new OpCode(1246463, 84214017);

		/// <summary>Loads the argument at index 3 onto the evaluation stack.</summary>
		// Token: 0x040014B8 RID: 5304
		public static readonly OpCode Ldarg_3 = new OpCode(1246719, 84214017);

		/// <summary>Loads the local variable at index 0 onto the evaluation stack.</summary>
		// Token: 0x040014B9 RID: 5305
		public static readonly OpCode Ldloc_0 = new OpCode(1246975, 84214017);

		/// <summary>Loads the local variable at index 1 onto the evaluation stack.</summary>
		// Token: 0x040014BA RID: 5306
		public static readonly OpCode Ldloc_1 = new OpCode(1247231, 84214017);

		/// <summary>Loads the local variable at index 2 onto the evaluation stack.</summary>
		// Token: 0x040014BB RID: 5307
		public static readonly OpCode Ldloc_2 = new OpCode(1247487, 84214017);

		/// <summary>Loads the local variable at index 3 onto the evaluation stack.</summary>
		// Token: 0x040014BC RID: 5308
		public static readonly OpCode Ldloc_3 = new OpCode(1247743, 84214017);

		/// <summary>Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 0.</summary>
		// Token: 0x040014BD RID: 5309
		public static readonly OpCode Stloc_0 = new OpCode(17959679, 84214017);

		/// <summary>Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 1.</summary>
		// Token: 0x040014BE RID: 5310
		public static readonly OpCode Stloc_1 = new OpCode(17959935, 84214017);

		/// <summary>Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 2.</summary>
		// Token: 0x040014BF RID: 5311
		public static readonly OpCode Stloc_2 = new OpCode(17960191, 84214017);

		/// <summary>Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 3.</summary>
		// Token: 0x040014C0 RID: 5312
		public static readonly OpCode Stloc_3 = new OpCode(17960447, 84214017);

		/// <summary>Loads the argument (referenced by a specified short form index) onto the evaluation stack.</summary>
		// Token: 0x040014C1 RID: 5313
		public static readonly OpCode Ldarg_S = new OpCode(1249023, 85065985);

		/// <summary>Load an argument address, in short form, onto the evaluation stack.</summary>
		// Token: 0x040014C2 RID: 5314
		public static readonly OpCode Ldarga_S = new OpCode(1380351, 85065985);

		/// <summary>Stores the value on top of the evaluation stack in the argument slot at a specified index, short form.</summary>
		// Token: 0x040014C3 RID: 5315
		public static readonly OpCode Starg_S = new OpCode(17961215, 85065985);

		/// <summary>Loads the local variable at a specific index onto the evaluation stack, short form.</summary>
		// Token: 0x040014C4 RID: 5316
		public static readonly OpCode Ldloc_S = new OpCode(1249791, 85065985);

		/// <summary>Loads the address of the local variable at a specific index onto the evaluation stack, short form.</summary>
		// Token: 0x040014C5 RID: 5317
		public static readonly OpCode Ldloca_S = new OpCode(1381119, 85065985);

		/// <summary>Pops the current value from the top of the evaluation stack and stores it in a the local variable list at <paramref name="index" /> (short form).</summary>
		// Token: 0x040014C6 RID: 5318
		public static readonly OpCode Stloc_S = new OpCode(17961983, 85065985);

		/// <summary>Pushes a null reference (type O) onto the evaluation stack.</summary>
		// Token: 0x040014C7 RID: 5319
		public static readonly OpCode Ldnull = new OpCode(1643775, 84215041);

		/// <summary>Pushes the integer value of -1 onto the evaluation stack as an int32.</summary>
		// Token: 0x040014C8 RID: 5320
		public static readonly OpCode Ldc_I4_M1 = new OpCode(1381887, 84214017);

		/// <summary>Pushes the integer value of 0 onto the evaluation stack as an int32.</summary>
		// Token: 0x040014C9 RID: 5321
		public static readonly OpCode Ldc_I4_0 = new OpCode(1382143, 84214017);

		/// <summary>Pushes the integer value of 1 onto the evaluation stack as an int32.</summary>
		// Token: 0x040014CA RID: 5322
		public static readonly OpCode Ldc_I4_1 = new OpCode(1382399, 84214017);

		/// <summary>Pushes the integer value of 2 onto the evaluation stack as an int32.</summary>
		// Token: 0x040014CB RID: 5323
		public static readonly OpCode Ldc_I4_2 = new OpCode(1382655, 84214017);

		/// <summary>Pushes the integer value of 3 onto the evaluation stack as an int32.</summary>
		// Token: 0x040014CC RID: 5324
		public static readonly OpCode Ldc_I4_3 = new OpCode(1382911, 84214017);

		/// <summary>Pushes the integer value of 4 onto the evaluation stack as an int32.</summary>
		// Token: 0x040014CD RID: 5325
		public static readonly OpCode Ldc_I4_4 = new OpCode(1383167, 84214017);

		/// <summary>Pushes the integer value of 5 onto the evaluation stack as an int32.</summary>
		// Token: 0x040014CE RID: 5326
		public static readonly OpCode Ldc_I4_5 = new OpCode(1383423, 84214017);

		/// <summary>Pushes the integer value of 6 onto the evaluation stack as an int32.</summary>
		// Token: 0x040014CF RID: 5327
		public static readonly OpCode Ldc_I4_6 = new OpCode(1383679, 84214017);

		/// <summary>Pushes the integer value of 7 onto the evaluation stack as an int32.</summary>
		// Token: 0x040014D0 RID: 5328
		public static readonly OpCode Ldc_I4_7 = new OpCode(1383935, 84214017);

		/// <summary>Pushes the integer value of 8 onto the evaluation stack as an int32.</summary>
		// Token: 0x040014D1 RID: 5329
		public static readonly OpCode Ldc_I4_8 = new OpCode(1384191, 84214017);

		/// <summary>Pushes the supplied int8 value onto the evaluation stack as an int32, short form.</summary>
		// Token: 0x040014D2 RID: 5330
		public static readonly OpCode Ldc_I4_S = new OpCode(1384447, 84934913);

		/// <summary>Pushes a supplied value of type int32 onto the evaluation stack as an int32.</summary>
		// Token: 0x040014D3 RID: 5331
		public static readonly OpCode Ldc_I4 = new OpCode(1384703, 84018433);

		/// <summary>Pushes a supplied value of type int64 onto the evaluation stack as an int64.</summary>
		// Token: 0x040014D4 RID: 5332
		public static readonly OpCode Ldc_I8 = new OpCode(1450495, 84083969);

		/// <summary>Pushes a supplied value of type float32 onto the evaluation stack as type F (float).</summary>
		// Token: 0x040014D5 RID: 5333
		public static readonly OpCode Ldc_R4 = new OpCode(1516287, 85001473);

		/// <summary>Pushes a supplied value of type float64 onto the evaluation stack as type F (float).</summary>
		// Token: 0x040014D6 RID: 5334
		public static readonly OpCode Ldc_R8 = new OpCode(1582079, 84346113);

		/// <summary>Copies the current topmost value on the evaluation stack, and then pushes the copy onto the evaluation stack.</summary>
		// Token: 0x040014D7 RID: 5335
		public static readonly OpCode Dup = new OpCode(18097663, 84215041);

		/// <summary>Removes the value currently on top of the evaluation stack.</summary>
		// Token: 0x040014D8 RID: 5336
		public static readonly OpCode Pop = new OpCode(17966847, 84215041);

		/// <summary>Exits current method and jumps to specified method.</summary>
		// Token: 0x040014D9 RID: 5337
		public static readonly OpCode Jmp = new OpCode(1189887, 33817857);

		/// <summary>Calls the method indicated by the passed method descriptor.</summary>
		// Token: 0x040014DA RID: 5338
		public static readonly OpCode Call = new OpCode(437987583, 33817857);

		/// <summary>Calls the method indicated on the evaluation stack (as a pointer to an entry point) with arguments described by a calling convention.</summary>
		// Token: 0x040014DB RID: 5339
		public static readonly OpCode Calli = new OpCode(437987839, 34145537);

		/// <summary>Returns from the current method, pushing a return value (if present) from the callee's evaluation stack onto the caller's evaluation stack.</summary>
		// Token: 0x040014DC RID: 5340
		public static readonly OpCode Ret = new OpCode(437398271, 117769473);

		/// <summary>Unconditionally transfers control to a target instruction (short form).</summary>
		// Token: 0x040014DD RID: 5341
		public static readonly OpCode Br_S = new OpCode(1190911, 983297);

		/// <summary>Transfers control to a target instruction if <paramref name="value" /> is false, a null reference, or zero.</summary>
		// Token: 0x040014DE RID: 5342
		public static readonly OpCode Brfalse_S = new OpCode(51522815, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if <paramref name="value" /> is true, not null, or non-zero.</summary>
		// Token: 0x040014DF RID: 5343
		public static readonly OpCode Brtrue_S = new OpCode(51523071, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if two values are equal.</summary>
		// Token: 0x040014E0 RID: 5344
		public static readonly OpCode Beq_S = new OpCode(34746111, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is greater than or equal to the second value.</summary>
		// Token: 0x040014E1 RID: 5345
		public static readonly OpCode Bge_S = new OpCode(34746367, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is greater than the second value.</summary>
		// Token: 0x040014E2 RID: 5346
		public static readonly OpCode Bgt_S = new OpCode(34746623, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is less than or equal to the second value.</summary>
		// Token: 0x040014E3 RID: 5347
		public static readonly OpCode Ble_S = new OpCode(34746879, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is less than the second value.</summary>
		// Token: 0x040014E4 RID: 5348
		public static readonly OpCode Blt_S = new OpCode(34747135, 51314945);

		/// <summary>Transfers control to a target instruction (short form) when two unsigned integer values or unordered float values are not equal.</summary>
		// Token: 0x040014E5 RID: 5349
		public static readonly OpCode Bne_Un_S = new OpCode(34747391, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x040014E6 RID: 5350
		public static readonly OpCode Bge_Un_S = new OpCode(34747647, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x040014E7 RID: 5351
		public static readonly OpCode Bgt_Un_S = new OpCode(34747903, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is less than or equal to the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x040014E8 RID: 5352
		public static readonly OpCode Ble_Un_S = new OpCode(34748159, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is less than the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x040014E9 RID: 5353
		public static readonly OpCode Blt_Un_S = new OpCode(34748415, 51314945);

		/// <summary>Unconditionally transfers control to a target instruction.</summary>
		// Token: 0x040014EA RID: 5354
		public static readonly OpCode Br = new OpCode(1194239, 1281);

		/// <summary>Transfers control to a target instruction if <paramref name="value" /> is false, a null reference (Nothing in Visual Basic), or zero.</summary>
		// Token: 0x040014EB RID: 5355
		public static readonly OpCode Brfalse = new OpCode(51526143, 50332929);

		/// <summary>Transfers control to a target instruction if <paramref name="value" /> is true, not null, or non-zero.</summary>
		// Token: 0x040014EC RID: 5356
		public static readonly OpCode Brtrue = new OpCode(51526399, 50332929);

		/// <summary>Transfers control to a target instruction if two values are equal.</summary>
		// Token: 0x040014ED RID: 5357
		public static readonly OpCode Beq = new OpCode(34749439, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is greater than or equal to the second value.</summary>
		// Token: 0x040014EE RID: 5358
		public static readonly OpCode Bge = new OpCode(34749695, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is greater than the second value.</summary>
		// Token: 0x040014EF RID: 5359
		public static readonly OpCode Bgt = new OpCode(34749951, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is less than or equal to the second value.</summary>
		// Token: 0x040014F0 RID: 5360
		public static readonly OpCode Ble = new OpCode(34750207, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is less than the second value.</summary>
		// Token: 0x040014F1 RID: 5361
		public static readonly OpCode Blt = new OpCode(34750463, 50331905);

		/// <summary>Transfers control to a target instruction when two unsigned integer values or unordered float values are not equal.</summary>
		// Token: 0x040014F2 RID: 5362
		public static readonly OpCode Bne_Un = new OpCode(34750719, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x040014F3 RID: 5363
		public static readonly OpCode Bge_Un = new OpCode(34750975, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x040014F4 RID: 5364
		public static readonly OpCode Bgt_Un = new OpCode(34751231, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is less than or equal to the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x040014F5 RID: 5365
		public static readonly OpCode Ble_Un = new OpCode(34751487, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is less than the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x040014F6 RID: 5366
		public static readonly OpCode Blt_Un = new OpCode(34751743, 50331905);

		/// <summary>Implements a jump table.</summary>
		// Token: 0x040014F7 RID: 5367
		public static readonly OpCode Switch = new OpCode(51529215, 51053825);

		/// <summary>Loads a value of type int8 as an int32 onto the evaluation stack indirectly.</summary>
		// Token: 0x040014F8 RID: 5368
		public static readonly OpCode Ldind_I1 = new OpCode(51726079, 84215041);

		/// <summary>Loads a value of type unsigned int8 as an int32 onto the evaluation stack indirectly.</summary>
		// Token: 0x040014F9 RID: 5369
		public static readonly OpCode Ldind_U1 = new OpCode(51726335, 84215041);

		/// <summary>Loads a value of type int16 as an int32 onto the evaluation stack indirectly.</summary>
		// Token: 0x040014FA RID: 5370
		public static readonly OpCode Ldind_I2 = new OpCode(51726591, 84215041);

		/// <summary>Loads a value of type unsigned int16 as an int32 onto the evaluation stack indirectly.</summary>
		// Token: 0x040014FB RID: 5371
		public static readonly OpCode Ldind_U2 = new OpCode(51726847, 84215041);

		/// <summary>Loads a value of type int32 as an int32 onto the evaluation stack indirectly.</summary>
		// Token: 0x040014FC RID: 5372
		public static readonly OpCode Ldind_I4 = new OpCode(51727103, 84215041);

		/// <summary>Loads a value of type unsigned int32 as an int32 onto the evaluation stack indirectly.</summary>
		// Token: 0x040014FD RID: 5373
		public static readonly OpCode Ldind_U4 = new OpCode(51727359, 84215041);

		/// <summary>Loads a value of type int64 as an int64 onto the evaluation stack indirectly.</summary>
		// Token: 0x040014FE RID: 5374
		public static readonly OpCode Ldind_I8 = new OpCode(51793151, 84215041);

		/// <summary>Loads a value of type native int as a native int onto the evaluation stack indirectly.</summary>
		// Token: 0x040014FF RID: 5375
		public static readonly OpCode Ldind_I = new OpCode(51727871, 84215041);

		/// <summary>Loads a value of type float32 as a type F (float) onto the evaluation stack indirectly.</summary>
		// Token: 0x04001500 RID: 5376
		public static readonly OpCode Ldind_R4 = new OpCode(51859199, 84215041);

		/// <summary>Loads a value of type float64 as a type F (float) onto the evaluation stack indirectly.</summary>
		// Token: 0x04001501 RID: 5377
		public static readonly OpCode Ldind_R8 = new OpCode(51924991, 84215041);

		/// <summary>Loads an object reference as a type O (object reference) onto the evaluation stack indirectly.</summary>
		// Token: 0x04001502 RID: 5378
		public static readonly OpCode Ldind_Ref = new OpCode(51990783, 84215041);

		/// <summary>Stores a object reference value at a supplied address.</summary>
		// Token: 0x04001503 RID: 5379
		public static readonly OpCode Stind_Ref = new OpCode(85086719, 84215041);

		/// <summary>Stores a value of type int8 at a supplied address.</summary>
		// Token: 0x04001504 RID: 5380
		public static readonly OpCode Stind_I1 = new OpCode(85086975, 84215041);

		/// <summary>Stores a value of type int16 at a supplied address.</summary>
		// Token: 0x04001505 RID: 5381
		public static readonly OpCode Stind_I2 = new OpCode(85087231, 84215041);

		/// <summary>Stores a value of type int32 at a supplied address.</summary>
		// Token: 0x04001506 RID: 5382
		public static readonly OpCode Stind_I4 = new OpCode(85087487, 84215041);

		/// <summary>Stores a value of type int64 at a supplied address.</summary>
		// Token: 0x04001507 RID: 5383
		public static readonly OpCode Stind_I8 = new OpCode(101864959, 84215041);

		/// <summary>Stores a value of type float32 at a supplied address.</summary>
		// Token: 0x04001508 RID: 5384
		public static readonly OpCode Stind_R4 = new OpCode(135419647, 84215041);

		/// <summary>Stores a value of type float64 at a supplied address.</summary>
		// Token: 0x04001509 RID: 5385
		public static readonly OpCode Stind_R8 = new OpCode(152197119, 84215041);

		/// <summary>Adds two values and pushes the result onto the evaluation stack.</summary>
		// Token: 0x0400150A RID: 5386
		public static readonly OpCode Add = new OpCode(34822399, 84215041);

		/// <summary>Subtracts one value from another and pushes the result onto the evaluation stack.</summary>
		// Token: 0x0400150B RID: 5387
		public static readonly OpCode Sub = new OpCode(34822655, 84215041);

		/// <summary>Multiplies two values and pushes the result on the evaluation stack.</summary>
		// Token: 0x0400150C RID: 5388
		public static readonly OpCode Mul = new OpCode(34822911, 84215041);

		/// <summary>Divides two values and pushes the result as a floating-point (type F) or quotient (type int32) onto the evaluation stack.</summary>
		// Token: 0x0400150D RID: 5389
		public static readonly OpCode Div = new OpCode(34823167, 84215041);

		/// <summary>Divides two unsigned integer values and pushes the result (int32) onto the evaluation stack.</summary>
		// Token: 0x0400150E RID: 5390
		public static readonly OpCode Div_Un = new OpCode(34823423, 84215041);

		/// <summary>Divides two values and pushes the remainder onto the evaluation stack.</summary>
		// Token: 0x0400150F RID: 5391
		public static readonly OpCode Rem = new OpCode(34823679, 84215041);

		/// <summary>Divides two unsigned values and pushes the remainder onto the evaluation stack.</summary>
		// Token: 0x04001510 RID: 5392
		public static readonly OpCode Rem_Un = new OpCode(34823935, 84215041);

		/// <summary>Computes the bitwise AND of two values and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04001511 RID: 5393
		public static readonly OpCode And = new OpCode(34824191, 84215041);

		/// <summary>Compute the bitwise complement of the two integer values on top of the stack and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04001512 RID: 5394
		public static readonly OpCode Or = new OpCode(34824447, 84215041);

		/// <summary>Computes the bitwise XOR of the top two values on the evaluation stack, pushing the result onto the evaluation stack.</summary>
		// Token: 0x04001513 RID: 5395
		public static readonly OpCode Xor = new OpCode(34824703, 84215041);

		/// <summary>Shifts an integer value to the left (in zeroes) by a specified number of bits, pushing the result onto the evaluation stack.</summary>
		// Token: 0x04001514 RID: 5396
		public static readonly OpCode Shl = new OpCode(34824959, 84215041);

		/// <summary>Shifts an integer value (in sign) to the right by a specified number of bits, pushing the result onto the evaluation stack.</summary>
		// Token: 0x04001515 RID: 5397
		public static readonly OpCode Shr = new OpCode(34825215, 84215041);

		/// <summary>Shifts an unsigned integer value (in zeroes) to the right by a specified number of bits, pushing the result onto the evaluation stack.</summary>
		// Token: 0x04001516 RID: 5398
		public static readonly OpCode Shr_Un = new OpCode(34825471, 84215041);

		/// <summary>Negates a value and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04001517 RID: 5399
		public static readonly OpCode Neg = new OpCode(18048511, 84215041);

		/// <summary>Computes the bitwise complement of the integer value on top of the stack and pushes the result onto the evaluation stack as the same type.</summary>
		// Token: 0x04001518 RID: 5400
		public static readonly OpCode Not = new OpCode(18048767, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to int8, then extends (pads) it to int32.</summary>
		// Token: 0x04001519 RID: 5401
		public static readonly OpCode Conv_I1 = new OpCode(18180095, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to int16, then extends (pads) it to int32.</summary>
		// Token: 0x0400151A RID: 5402
		public static readonly OpCode Conv_I2 = new OpCode(18180351, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to int32.</summary>
		// Token: 0x0400151B RID: 5403
		public static readonly OpCode Conv_I4 = new OpCode(18180607, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to int64.</summary>
		// Token: 0x0400151C RID: 5404
		public static readonly OpCode Conv_I8 = new OpCode(18246399, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to float32.</summary>
		// Token: 0x0400151D RID: 5405
		public static readonly OpCode Conv_R4 = new OpCode(18312191, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to float64.</summary>
		// Token: 0x0400151E RID: 5406
		public static readonly OpCode Conv_R8 = new OpCode(18377983, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to unsigned int32, and extends it to int32.</summary>
		// Token: 0x0400151F RID: 5407
		public static readonly OpCode Conv_U4 = new OpCode(18181631, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to unsigned int64, and extends it to int64.</summary>
		// Token: 0x04001520 RID: 5408
		public static readonly OpCode Conv_U8 = new OpCode(18247423, 84215041);

		/// <summary>Calls a late-bound method on an object, pushing the return value onto the evaluation stack.</summary>
		// Token: 0x04001521 RID: 5409
		public static readonly OpCode Callvirt = new OpCode(438005759, 33817345);

		/// <summary>Copies the value type located at the address of an object (type &amp;, * or native int) to the address of the destination object (type &amp;, * or native int).</summary>
		// Token: 0x04001522 RID: 5410
		public static readonly OpCode Cpobj = new OpCode(85094655, 84738817);

		/// <summary>Copies the value type object pointed to by an address to the top of the evaluation stack.</summary>
		// Token: 0x04001523 RID: 5411
		public static readonly OpCode Ldobj = new OpCode(51606015, 84738817);

		/// <summary>Pushes a new object reference to a string literal stored in the metadata.</summary>
		// Token: 0x04001524 RID: 5412
		public static readonly OpCode Ldstr = new OpCode(1667839, 84542209);

		/// <summary>Creates a new object or a new instance of a value type, pushing an object reference (type O) onto the evaluation stack.</summary>
		// Token: 0x04001525 RID: 5413
		public static readonly OpCode Newobj = new OpCode(437875711, 33817345);

		/// <summary>Attempts to cast an object passed by reference to the specified class.</summary>
		// Token: 0x04001526 RID: 5414
		[ComVisible(true)]
		public static readonly OpCode Castclass = new OpCode(169440511, 84738817);

		/// <summary>Tests whether an object reference (type O) is an instance of a particular class.</summary>
		// Token: 0x04001527 RID: 5415
		public static readonly OpCode Isinst = new OpCode(169178623, 84738817);

		/// <summary>Converts the unsigned integer value on top of the evaluation stack to float32.</summary>
		// Token: 0x04001528 RID: 5416
		public static readonly OpCode Conv_R_Un = new OpCode(18380543, 84215041);

		/// <summary>Converts the boxed representation of a value type to its unboxed form.</summary>
		// Token: 0x04001529 RID: 5417
		public static readonly OpCode Unbox = new OpCode(169179647, 84739329);

		/// <summary>Throws the exception object currently on the evaluation stack.</summary>
		// Token: 0x0400152A RID: 5418
		public static readonly OpCode Throw = new OpCode(168983295, 134546177);

		/// <summary>Finds the value of a field in the object whose reference is currently on the evaluation stack.</summary>
		// Token: 0x0400152B RID: 5419
		public static readonly OpCode Ldfld = new OpCode(169049087, 83952385);

		/// <summary>Finds the address of a field in the object whose reference is currently on the evaluation stack.</summary>
		// Token: 0x0400152C RID: 5420
		public static readonly OpCode Ldflda = new OpCode(169180415, 83952385);

		/// <summary>Replaces the value stored in the field of an object reference or pointer with a new value.</summary>
		// Token: 0x0400152D RID: 5421
		public static readonly OpCode Stfld = new OpCode(185761279, 83952385);

		/// <summary>Pushes the value of a static field onto the evaluation stack.</summary>
		// Token: 0x0400152E RID: 5422
		public static readonly OpCode Ldsfld = new OpCode(1277695, 83952385);

		/// <summary>Pushes the address of a static field onto the evaluation stack.</summary>
		// Token: 0x0400152F RID: 5423
		public static readonly OpCode Ldsflda = new OpCode(1409023, 83952385);

		/// <summary>Replaces the value of a static field with a value from the evaluation stack.</summary>
		// Token: 0x04001530 RID: 5424
		public static readonly OpCode Stsfld = new OpCode(17989887, 83952385);

		/// <summary>Copies a value of a specified type from the evaluation stack into a supplied memory address.</summary>
		// Token: 0x04001531 RID: 5425
		public static readonly OpCode Stobj = new OpCode(68321791, 84739329);

		/// <summary>Converts the unsigned value on top of the evaluation stack to signed int8 and extends it to int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04001532 RID: 5426
		public static readonly OpCode Conv_Ovf_I1_Un = new OpCode(18187007, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to signed int16 and extends it to int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04001533 RID: 5427
		public static readonly OpCode Conv_Ovf_I2_Un = new OpCode(18187263, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to signed int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04001534 RID: 5428
		public static readonly OpCode Conv_Ovf_I4_Un = new OpCode(18187519, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to signed int64, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04001535 RID: 5429
		public static readonly OpCode Conv_Ovf_I8_Un = new OpCode(18253311, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to unsigned int8 and extends it to int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04001536 RID: 5430
		public static readonly OpCode Conv_Ovf_U1_Un = new OpCode(18188031, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to unsigned int16 and extends it to int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04001537 RID: 5431
		public static readonly OpCode Conv_Ovf_U2_Un = new OpCode(18188287, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to unsigned int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04001538 RID: 5432
		public static readonly OpCode Conv_Ovf_U4_Un = new OpCode(18188543, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to unsigned int64, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04001539 RID: 5433
		public static readonly OpCode Conv_Ovf_U8_Un = new OpCode(18254335, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to signed native int, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x0400153A RID: 5434
		public static readonly OpCode Conv_Ovf_I_Un = new OpCode(18189055, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to unsigned native int, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x0400153B RID: 5435
		public static readonly OpCode Conv_Ovf_U_Un = new OpCode(18189311, 84215041);

		/// <summary>Converts a value type to an object reference (type O).</summary>
		// Token: 0x0400153C RID: 5436
		public static readonly OpCode Box = new OpCode(18451711, 84739329);

		/// <summary>Pushes an object reference to a new zero-based, one-dimensional array whose elements are of a specific type onto the evaluation stack.</summary>
		// Token: 0x0400153D RID: 5437
		public static readonly OpCode Newarr = new OpCode(52006399, 84738817);

		/// <summary>Pushes the number of elements of a zero-based, one-dimensional array onto the evaluation stack.</summary>
		// Token: 0x0400153E RID: 5438
		public static readonly OpCode Ldlen = new OpCode(169185023, 84214529);

		/// <summary>Loads the address of the array element at a specified array index onto the top of the evaluation stack as type &amp; (managed pointer).</summary>
		// Token: 0x0400153F RID: 5439
		public static readonly OpCode Ldelema = new OpCode(202739711, 84738817);

		/// <summary>Loads the element with type int8 at a specified array index onto the top of the evaluation stack as an int32.</summary>
		// Token: 0x04001540 RID: 5440
		public static readonly OpCode Ldelem_I1 = new OpCode(202739967, 84214529);

		/// <summary>Loads the element with type unsigned int8 at a specified array index onto the top of the evaluation stack as an int32.</summary>
		// Token: 0x04001541 RID: 5441
		public static readonly OpCode Ldelem_U1 = new OpCode(202740223, 84214529);

		/// <summary>Loads the element with type int16 at a specified array index onto the top of the evaluation stack as an int32.</summary>
		// Token: 0x04001542 RID: 5442
		public static readonly OpCode Ldelem_I2 = new OpCode(202740479, 84214529);

		/// <summary>Loads the element with type unsigned int16 at a specified array index onto the top of the evaluation stack as an int32.</summary>
		// Token: 0x04001543 RID: 5443
		public static readonly OpCode Ldelem_U2 = new OpCode(202740735, 84214529);

		/// <summary>Loads the element with type int32 at a specified array index onto the top of the evaluation stack as an int32.</summary>
		// Token: 0x04001544 RID: 5444
		public static readonly OpCode Ldelem_I4 = new OpCode(202740991, 84214529);

		/// <summary>Loads the element with type unsigned int32 at a specified array index onto the top of the evaluation stack as an int32.</summary>
		// Token: 0x04001545 RID: 5445
		public static readonly OpCode Ldelem_U4 = new OpCode(202741247, 84214529);

		/// <summary>Loads the element with type int64 at a specified array index onto the top of the evaluation stack as an int64.</summary>
		// Token: 0x04001546 RID: 5446
		public static readonly OpCode Ldelem_I8 = new OpCode(202807039, 84214529);

		/// <summary>Loads the element with type native int at a specified array index onto the top of the evaluation stack as a native int.</summary>
		// Token: 0x04001547 RID: 5447
		public static readonly OpCode Ldelem_I = new OpCode(202741759, 84214529);

		/// <summary>Loads the element with type float32 at a specified array index onto the top of the evaluation stack as type F (float).</summary>
		// Token: 0x04001548 RID: 5448
		public static readonly OpCode Ldelem_R4 = new OpCode(202873087, 84214529);

		/// <summary>Loads the element with type float64 at a specified array index onto the top of the evaluation stack as type F (float).</summary>
		// Token: 0x04001549 RID: 5449
		public static readonly OpCode Ldelem_R8 = new OpCode(202938879, 84214529);

		/// <summary>Loads the element containing an object reference at a specified array index onto the top of the evaluation stack as type O (object reference).</summary>
		// Token: 0x0400154A RID: 5450
		public static readonly OpCode Ldelem_Ref = new OpCode(203004671, 84214529);

		/// <summary>Replaces the array element at a given index with the native int value on the evaluation stack.</summary>
		// Token: 0x0400154B RID: 5451
		public static readonly OpCode Stelem_I = new OpCode(219323391, 84214529);

		/// <summary>Replaces the array element at a given index with the int8 value on the evaluation stack.</summary>
		// Token: 0x0400154C RID: 5452
		public static readonly OpCode Stelem_I1 = new OpCode(219323647, 84214529);

		/// <summary>Replaces the array element at a given index with the int16 value on the evaluation stack.</summary>
		// Token: 0x0400154D RID: 5453
		public static readonly OpCode Stelem_I2 = new OpCode(219323903, 84214529);

		/// <summary>Replaces the array element at a given index with the int32 value on the evaluation stack.</summary>
		// Token: 0x0400154E RID: 5454
		public static readonly OpCode Stelem_I4 = new OpCode(219324159, 84214529);

		/// <summary>Replaces the array element at a given index with the int64 value on the evaluation stack.</summary>
		// Token: 0x0400154F RID: 5455
		public static readonly OpCode Stelem_I8 = new OpCode(236101631, 84214529);

		/// <summary>Replaces the array element at a given index with the float32 value on the evaluation stack.</summary>
		// Token: 0x04001550 RID: 5456
		public static readonly OpCode Stelem_R4 = new OpCode(252879103, 84214529);

		/// <summary>Replaces the array element at a given index with the float64 value on the evaluation stack.</summary>
		// Token: 0x04001551 RID: 5457
		public static readonly OpCode Stelem_R8 = new OpCode(269656575, 84214529);

		/// <summary>Replaces the array element at a given index with the object ref value (type O) on the evaluation stack.</summary>
		// Token: 0x04001552 RID: 5458
		public static readonly OpCode Stelem_Ref = new OpCode(286434047, 84214529);

		/// <summary>Loads the element at a specified array index onto the top of the evaluation stack as the type specified in the instruction. </summary>
		// Token: 0x04001553 RID: 5459
		public static readonly OpCode Ldelem = new OpCode(202613759, 84738817);

		/// <summary>Replaces the array element at a given index with the value on the evaluation stack, whose type is specified in the instruction.</summary>
		// Token: 0x04001554 RID: 5460
		public static readonly OpCode Stelem = new OpCode(470983935, 84738817);

		/// <summary>Converts the boxed representation of a type specified in the instruction to its unboxed form. </summary>
		// Token: 0x04001555 RID: 5461
		public static readonly OpCode Unbox_Any = new OpCode(169059839, 84738817);

		/// <summary>Converts the signed value on top of the evaluation stack to signed int8 and extends it to int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04001556 RID: 5462
		public static readonly OpCode Conv_Ovf_I1 = new OpCode(18199551, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to unsigned int8 and extends it to int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04001557 RID: 5463
		public static readonly OpCode Conv_Ovf_U1 = new OpCode(18199807, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to signed int16 and extending it to int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04001558 RID: 5464
		public static readonly OpCode Conv_Ovf_I2 = new OpCode(18200063, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to unsigned int16 and extends it to int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04001559 RID: 5465
		public static readonly OpCode Conv_Ovf_U2 = new OpCode(18200319, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to signed int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x0400155A RID: 5466
		public static readonly OpCode Conv_Ovf_I4 = new OpCode(18200575, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to unsigned int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x0400155B RID: 5467
		public static readonly OpCode Conv_Ovf_U4 = new OpCode(18200831, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to signed int64, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x0400155C RID: 5468
		public static readonly OpCode Conv_Ovf_I8 = new OpCode(18266623, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to unsigned int64, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x0400155D RID: 5469
		public static readonly OpCode Conv_Ovf_U8 = new OpCode(18266879, 84215041);

		/// <summary>Retrieves the address (type &amp;) embedded in a typed reference.</summary>
		// Token: 0x0400155E RID: 5470
		public static readonly OpCode Refanyval = new OpCode(18203391, 84739329);

		/// <summary>Throws <see cref="T:System.ArithmeticException" /> if value is not a finite number.</summary>
		// Token: 0x0400155F RID: 5471
		public static readonly OpCode Ckfinite = new OpCode(18400255, 84215041);

		/// <summary>Pushes a typed reference to an instance of a specific type onto the evaluation stack.</summary>
		// Token: 0x04001560 RID: 5472
		public static readonly OpCode Mkrefany = new OpCode(51627775, 84739329);

		/// <summary>Converts a metadata token to its runtime representation, pushing it onto the evaluation stack.</summary>
		// Token: 0x04001561 RID: 5473
		public static readonly OpCode Ldtoken = new OpCode(1429759, 84673793);

		/// <summary>Converts the value on top of the evaluation stack to unsigned int16, and extends it to int32.</summary>
		// Token: 0x04001562 RID: 5474
		public static readonly OpCode Conv_U2 = new OpCode(18207231, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to unsigned int8, and extends it to int32.</summary>
		// Token: 0x04001563 RID: 5475
		public static readonly OpCode Conv_U1 = new OpCode(18207487, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to native int.</summary>
		// Token: 0x04001564 RID: 5476
		public static readonly OpCode Conv_I = new OpCode(18207743, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to signed native int, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04001565 RID: 5477
		public static readonly OpCode Conv_Ovf_I = new OpCode(18207999, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to unsigned native int, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04001566 RID: 5478
		public static readonly OpCode Conv_Ovf_U = new OpCode(18208255, 84215041);

		/// <summary>Adds two integers, performs an overflow check, and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04001567 RID: 5479
		public static readonly OpCode Add_Ovf = new OpCode(34854655, 84215041);

		/// <summary>Adds two unsigned integer values, performs an overflow check, and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04001568 RID: 5480
		public static readonly OpCode Add_Ovf_Un = new OpCode(34854911, 84215041);

		/// <summary>Multiplies two integer values, performs an overflow check, and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04001569 RID: 5481
		public static readonly OpCode Mul_Ovf = new OpCode(34855167, 84215041);

		/// <summary>Multiplies two unsigned integer values, performs an overflow check, and pushes the result onto the evaluation stack.</summary>
		// Token: 0x0400156A RID: 5482
		public static readonly OpCode Mul_Ovf_Un = new OpCode(34855423, 84215041);

		/// <summary>Subtracts one integer value from another, performs an overflow check, and pushes the result onto the evaluation stack.</summary>
		// Token: 0x0400156B RID: 5483
		public static readonly OpCode Sub_Ovf = new OpCode(34855679, 84215041);

		/// <summary>Subtracts one unsigned integer value from another, performs an overflow check, and pushes the result onto the evaluation stack.</summary>
		// Token: 0x0400156C RID: 5484
		public static readonly OpCode Sub_Ovf_Un = new OpCode(34855935, 84215041);

		/// <summary>Transfers control from the fault or finally clause of an exception block back to the Common Language Infrastructure (CLI) exception handler.</summary>
		// Token: 0x0400156D RID: 5485
		public static readonly OpCode Endfinally = new OpCode(1236223, 117769473);

		/// <summary>Exits a protected region of code, unconditionally transferring control to a specific target instruction.</summary>
		// Token: 0x0400156E RID: 5486
		public static readonly OpCode Leave = new OpCode(1236479, 1281);

		/// <summary>Exits a protected region of code, unconditionally transferring control to a target instruction (short form).</summary>
		// Token: 0x0400156F RID: 5487
		public static readonly OpCode Leave_S = new OpCode(1236735, 984321);

		/// <summary>Stores a value of type native int at a supplied address.</summary>
		// Token: 0x04001570 RID: 5488
		public static readonly OpCode Stind_I = new OpCode(85123071, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to unsigned native int, and extends it to native int.</summary>
		// Token: 0x04001571 RID: 5489
		public static readonly OpCode Conv_U = new OpCode(18211071, 84215041);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x04001572 RID: 5490
		public static readonly OpCode Prefix7 = new OpCode(1243391, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x04001573 RID: 5491
		public static readonly OpCode Prefix6 = new OpCode(1243647, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x04001574 RID: 5492
		public static readonly OpCode Prefix5 = new OpCode(1243903, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x04001575 RID: 5493
		public static readonly OpCode Prefix4 = new OpCode(1244159, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x04001576 RID: 5494
		public static readonly OpCode Prefix3 = new OpCode(1244415, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x04001577 RID: 5495
		public static readonly OpCode Prefix2 = new OpCode(1244671, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x04001578 RID: 5496
		public static readonly OpCode Prefix1 = new OpCode(1244927, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x04001579 RID: 5497
		public static readonly OpCode Prefixref = new OpCode(1245183, 67437057);

		/// <summary>Returns an unmanaged pointer to the argument list of the current method.</summary>
		// Token: 0x0400157A RID: 5498
		public static readonly OpCode Arglist = new OpCode(1376510, 84215042);

		/// <summary>Compares two values. If they are equal, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.</summary>
		// Token: 0x0400157B RID: 5499
		public static readonly OpCode Ceq = new OpCode(34931198, 84215042);

		/// <summary>Compares two values. If the first value is greater than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.</summary>
		// Token: 0x0400157C RID: 5500
		public static readonly OpCode Cgt = new OpCode(34931454, 84215042);

		/// <summary>Compares two unsigned or unordered values. If the first value is greater than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.</summary>
		// Token: 0x0400157D RID: 5501
		public static readonly OpCode Cgt_Un = new OpCode(34931710, 84215042);

		/// <summary>Compares two values. If the first value is less than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.</summary>
		// Token: 0x0400157E RID: 5502
		public static readonly OpCode Clt = new OpCode(34931966, 84215042);

		/// <summary>Compares the unsigned or unordered values <paramref name="value1" /> and <paramref name="value2" />. If <paramref name="value1" /> is less than <paramref name="value2" />, then the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.</summary>
		// Token: 0x0400157F RID: 5503
		public static readonly OpCode Clt_Un = new OpCode(34932222, 84215042);

		/// <summary>Pushes an unmanaged pointer (type native int) to the native code implementing a specific method onto the evaluation stack.</summary>
		// Token: 0x04001580 RID: 5504
		public static readonly OpCode Ldftn = new OpCode(1378046, 84149506);

		/// <summary>Pushes an unmanaged pointer (type native int) to the native code implementing a particular virtual method associated with a specified object onto the evaluation stack.</summary>
		// Token: 0x04001581 RID: 5505
		public static readonly OpCode Ldvirtftn = new OpCode(169150462, 84149506);

		/// <summary>Loads an argument (referenced by a specified index value) onto the stack.</summary>
		// Token: 0x04001582 RID: 5506
		public static readonly OpCode Ldarg = new OpCode(1247742, 84804866);

		/// <summary>Load an argument address onto the evaluation stack.</summary>
		// Token: 0x04001583 RID: 5507
		public static readonly OpCode Ldarga = new OpCode(1379070, 84804866);

		/// <summary>Stores the value on top of the evaluation stack in the argument slot at a specified index.</summary>
		// Token: 0x04001584 RID: 5508
		public static readonly OpCode Starg = new OpCode(17959934, 84804866);

		/// <summary>Loads the local variable at a specific index onto the evaluation stack.</summary>
		// Token: 0x04001585 RID: 5509
		public static readonly OpCode Ldloc = new OpCode(1248510, 84804866);

		/// <summary>Loads the address of the local variable at a specific index onto the evaluation stack.</summary>
		// Token: 0x04001586 RID: 5510
		public static readonly OpCode Ldloca = new OpCode(1379838, 84804866);

		/// <summary>Pops the current value from the top of the evaluation stack and stores it in a the local variable list at a specified index.</summary>
		// Token: 0x04001587 RID: 5511
		public static readonly OpCode Stloc = new OpCode(17960702, 84804866);

		/// <summary>Allocates a certain number of bytes from the local dynamic memory pool and pushes the address (a transient pointer, type *) of the first allocated byte onto the evaluation stack.</summary>
		// Token: 0x04001588 RID: 5512
		public static readonly OpCode Localloc = new OpCode(51711998, 84215042);

		/// <summary>Transfers control from the filter clause of an exception back to the Common Language Infrastructure (CLI) exception handler.</summary>
		// Token: 0x04001589 RID: 5513
		public static readonly OpCode Endfilter = new OpCode(51515902, 117769474);

		/// <summary>Indicates that an address currently atop the evaluation stack might not be aligned to the natural size of the immediately following ldind, stind, ldfld, stfld, ldobj, stobj, initblk, or cpblk instruction.</summary>
		// Token: 0x0400158A RID: 5514
		public static readonly OpCode Unaligned = new OpCode(1184510, 68158466);

		/// <summary>Specifies that an address currently atop the evaluation stack might be volatile, and the results of reading that location cannot be cached or that multiple stores to that location cannot be suppressed.</summary>
		// Token: 0x0400158B RID: 5515
		public static readonly OpCode Volatile = new OpCode(1184766, 67437570);

		/// <summary>Performs a postfixed method call instruction such that the current method's stack frame is removed before the actual call instruction is executed.</summary>
		// Token: 0x0400158C RID: 5516
		public static readonly OpCode Tailcall = new OpCode(1185022, 67437570);

		/// <summary>Initializes each field of the value type at a specified address to a null reference or a 0 of the appropriate primitive type.</summary>
		// Token: 0x0400158D RID: 5517
		public static readonly OpCode Initobj = new OpCode(51516926, 84738818);

		/// <summary>Constrains the type on which a virtual method call is made.</summary>
		// Token: 0x0400158E RID: 5518
		public static readonly OpCode Constrained = new OpCode(1185534, 67961858);

		/// <summary>Copies a specified number bytes from a source address to a destination address.</summary>
		// Token: 0x0400158F RID: 5519
		public static readonly OpCode Cpblk = new OpCode(118626302, 84215042);

		/// <summary>Initializes a specified block of memory at a specific address to a given size and initial value.</summary>
		// Token: 0x04001590 RID: 5520
		public static readonly OpCode Initblk = new OpCode(118626558, 84215042);

		/// <summary>Rethrows the current exception.</summary>
		// Token: 0x04001591 RID: 5521
		public static readonly OpCode Rethrow = new OpCode(1186558, 134546178);

		/// <summary>Pushes the size, in bytes, of a supplied value type onto the evaluation stack.</summary>
		// Token: 0x04001592 RID: 5522
		public static readonly OpCode Sizeof = new OpCode(1383678, 84739330);

		/// <summary>Retrieves the type token embedded in a typed reference.</summary>
		// Token: 0x04001593 RID: 5523
		public static readonly OpCode Refanytype = new OpCode(18161150, 84215042);

		/// <summary>Specifies that the subsequent array address operation performs no type check at run time, and that it returns a managed pointer whose mutability is restricted.</summary>
		// Token: 0x04001594 RID: 5524
		public static readonly OpCode Readonly = new OpCode(1187582, 67437570);
	}
}
