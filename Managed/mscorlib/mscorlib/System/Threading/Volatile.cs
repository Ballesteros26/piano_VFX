using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.Threading
{
	/// <summary>Contains methods for performing volatile memory operations.</summary>
	// Token: 0x020004B2 RID: 1202
	public static class Volatile
	{
		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method. </summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x06003845 RID: 14405
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Read(ref bool location);

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x06003846 RID: 14406
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte Read(ref byte location);

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x06003847 RID: 14407
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte Read(ref sbyte location);

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x06003848 RID: 14408
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short Read(ref short location);

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x06003849 RID: 14409
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ushort Read(ref ushort location);

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x0600384A RID: 14410
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Read(ref int location);

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x0600384B RID: 14411
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint Read(ref uint location);

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x0600384C RID: 14412
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Read(ref long location);

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x0600384D RID: 14413
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong Read(ref ulong location);

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x0600384E RID: 14414
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Read(ref IntPtr location);

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x0600384F RID: 14415
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern UIntPtr Read(ref UIntPtr location);

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x06003850 RID: 14416
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Read(ref double location);

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		// Token: 0x06003851 RID: 14417
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Read(ref float location);

		/// <summary>Reads the object reference from the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <returns>The reference to <paramref name="T" /> that was read. This reference is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache. </returns>
		/// <param name="location">The field to read.</param>
		/// <typeparam name="T">The type of field to read. This must be a reference type, not a value type.</typeparam>
		// Token: 0x06003852 RID: 14418
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern T Read<T>(ref T location) where T : class;

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer. </param>
		// Token: 0x06003853 RID: 14419
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref bool location, bool value);

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06003854 RID: 14420
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref byte location, byte value);

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06003855 RID: 14421
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref sbyte location, sbyte value);

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06003856 RID: 14422
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref short location, short value);

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06003857 RID: 14423
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref ushort location, ushort value);

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06003858 RID: 14424
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref int location, int value);

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06003859 RID: 14425
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref uint location, uint value);

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a memory operation appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x0600385A RID: 14426
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref long location, long value);

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x0600385B RID: 14427
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref ulong location, ulong value);

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x0600385C RID: 14428
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref IntPtr location, IntPtr value);

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x0600385D RID: 14429
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref UIntPtr location, UIntPtr value);

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x0600385E RID: 14430
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref double location, double value);

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x0600385F RID: 14431
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref float location, float value);

		/// <summary>Writes the specified object reference to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method. </summary>
		/// <param name="location">The field where the object reference is written.</param>
		/// <param name="value">The object reference to write. The reference is written immediately so that it is visible to all processors in the computer.</param>
		/// <typeparam name="T">The type of field to write. This must be a reference type, not a value type. </typeparam>
		// Token: 0x06003860 RID: 14432
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write<T>(ref T location, T value) where T : class;
	}
}
