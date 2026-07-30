using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents a variable-length argument list; that is, the parameters of a function that takes a variable number of arguments.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000205 RID: 517
	[StructLayout(LayoutKind.Auto)]
	public struct ArgIterator
	{
		// Token: 0x06001822 RID: 6178
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Setup(IntPtr argsp, IntPtr start);

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgIterator" /> structure using the specified argument list.</summary>
		/// <param name="arglist">An argument list consisting of mandatory and optional arguments. </param>
		// Token: 0x06001823 RID: 6179 RVA: 0x0005D59C File Offset: 0x0005B79C
		public ArgIterator(RuntimeArgumentHandle arglist)
		{
			this.sig = IntPtr.Zero;
			this.args = IntPtr.Zero;
			this.next_arg = (this.num_args = 0);
			this.Setup(arglist.args, IntPtr.Zero);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArgIterator" /> structure using the specified argument list and a pointer to an item in the list.</summary>
		/// <param name="arglist">An argument list consisting of mandatory and optional arguments. </param>
		/// <param name="ptr">A pointer to the argument in <paramref name="arglist" /> to access first, or the first mandatory argument in <paramref name="arglist" /> if <paramref name="ptr" /> is null.</param>
		// Token: 0x06001824 RID: 6180 RVA: 0x0005D5E4 File Offset: 0x0005B7E4
		[CLSCompliant(false)]
		public unsafe ArgIterator(RuntimeArgumentHandle arglist, void* ptr)
		{
			this.sig = IntPtr.Zero;
			this.args = IntPtr.Zero;
			this.next_arg = (this.num_args = 0);
			this.Setup(arglist.args, (IntPtr)ptr);
		}

		/// <summary>Concludes processing of the variable-length argument list represented by this instance.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001825 RID: 6181 RVA: 0x0005D62A File Offset: 0x0005B82A
		public void End()
		{
			this.next_arg = this.num_args;
		}

		/// <summary>This method is not supported, and always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>This comparison is not supported. No value is returned.</returns>
		/// <param name="o">An object to be compared to this instance. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001826 RID: 6182 RVA: 0x0005D638 File Offset: 0x0005B838
		public override bool Equals(object o)
		{
			throw new NotSupportedException(Locale.GetText("ArgIterator does not support Equals."));
		}

		/// <summary>Returns the hash code of this object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001827 RID: 6183 RVA: 0x0005D649 File Offset: 0x0005B849
		public override int GetHashCode()
		{
			return this.sig.GetHashCode();
		}

		/// <summary>Returns the next argument in a variable-length argument list.</summary>
		/// <returns>The next argument as a <see cref="T:System.TypedReference" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read beyond the end of the list. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001828 RID: 6184 RVA: 0x0005D656 File Offset: 0x0005B856
		[CLSCompliant(false)]
		public TypedReference GetNextArg()
		{
			if (this.num_args == this.next_arg)
			{
				throw new InvalidOperationException(Locale.GetText("Invalid iterator position."));
			}
			return this.IntGetNextArg();
		}

		// Token: 0x06001829 RID: 6185
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern TypedReference IntGetNextArg();

		/// <summary>Returns the next argument in a variable-length argument list that has a specified type.</summary>
		/// <returns>The next argument as a <see cref="T:System.TypedReference" /> object.</returns>
		/// <param name="rth">A runtime type handle that identifies the type of the argument to retrieve. </param>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read beyond the end of the list. </exception>
		/// <exception cref="T:System.ArgumentNullException">The pointer to the remaining arguments is zero.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600182A RID: 6186 RVA: 0x0005D67C File Offset: 0x0005B87C
		[CLSCompliant(false)]
		public TypedReference GetNextArg(RuntimeTypeHandle rth)
		{
			if (this.num_args == this.next_arg)
			{
				throw new InvalidOperationException(Locale.GetText("Invalid iterator position."));
			}
			return this.IntGetNextArg(rth.Value);
		}

		// Token: 0x0600182B RID: 6187
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern TypedReference IntGetNextArg(IntPtr rth);

		/// <summary>Returns the type of the next argument.</summary>
		/// <returns>The type of the next argument.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600182C RID: 6188 RVA: 0x0005D6A9 File Offset: 0x0005B8A9
		public RuntimeTypeHandle GetNextArgType()
		{
			if (this.num_args == this.next_arg)
			{
				throw new InvalidOperationException(Locale.GetText("Invalid iterator position."));
			}
			return new RuntimeTypeHandle(this.IntGetNextArgType());
		}

		// Token: 0x0600182D RID: 6189
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr IntGetNextArgType();

		/// <summary>Returns the number of arguments remaining in the argument list.</summary>
		/// <returns>The number of remaining arguments.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600182E RID: 6190 RVA: 0x0005D6D4 File Offset: 0x0005B8D4
		public int GetRemainingCount()
		{
			return this.num_args - this.next_arg;
		}

		// Token: 0x04000C7A RID: 3194
		private IntPtr sig;

		// Token: 0x04000C7B RID: 3195
		private IntPtr args;

		// Token: 0x04000C7C RID: 3196
		private int next_arg;

		// Token: 0x04000C7D RID: 3197
		private int num_args;
	}
}
