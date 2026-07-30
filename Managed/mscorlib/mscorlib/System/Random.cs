using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents a pseudo-random number generator, a device that produces a sequence of numbers that meet certain statistical requirements for randomness.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001B3 RID: 435
	[ComVisible(true)]
	[Serializable]
	public class Random
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Random" /> class, using a time-dependent default seed value.</summary>
		// Token: 0x06001214 RID: 4628 RVA: 0x00049BAB File Offset: 0x00047DAB
		public Random()
			: this(Environment.TickCount)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Random" /> class, using the specified seed value.</summary>
		/// <param name="Seed">A number used to calculate a starting value for the pseudo-random number sequence. If a negative number is specified, the absolute value of the number is used. </param>
		// Token: 0x06001215 RID: 4629 RVA: 0x00049BB8 File Offset: 0x00047DB8
		public Random(int Seed)
		{
			int num = ((Seed == int.MinValue) ? int.MaxValue : Math.Abs(Seed));
			int num2 = 161803398 - num;
			this.SeedArray[55] = num2;
			int num3 = 1;
			for (int i = 1; i < 55; i++)
			{
				int num4 = 21 * i % 55;
				this.SeedArray[num4] = num3;
				num3 = num2 - num3;
				if (num3 < 0)
				{
					num3 += int.MaxValue;
				}
				num2 = this.SeedArray[num4];
			}
			for (int j = 1; j < 5; j++)
			{
				for (int k = 1; k < 56; k++)
				{
					this.SeedArray[k] -= this.SeedArray[1 + (k + 30) % 55];
					if (this.SeedArray[k] < 0)
					{
						this.SeedArray[k] += int.MaxValue;
					}
				}
			}
			this.inext = 0;
			this.inextp = 21;
			Seed = 1;
		}

		/// <summary>Returns a random number between 0.0 and 1.0.</summary>
		/// <returns>A double-precision floating point number greater than or equal to 0.0, and less than 1.0.</returns>
		// Token: 0x06001216 RID: 4630 RVA: 0x00049CB5 File Offset: 0x00047EB5
		protected virtual double Sample()
		{
			return (double)this.InternalSample() * 4.656612875245797E-10;
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00049CC8 File Offset: 0x00047EC8
		private int InternalSample()
		{
			int num = this.inext;
			int num2 = this.inextp;
			if (++num >= 56)
			{
				num = 1;
			}
			if (++num2 >= 56)
			{
				num2 = 1;
			}
			int num3 = this.SeedArray[num] - this.SeedArray[num2];
			if (num3 == 2147483647)
			{
				num3--;
			}
			if (num3 < 0)
			{
				num3 += int.MaxValue;
			}
			this.SeedArray[num] = num3;
			this.inext = num;
			this.inextp = num2;
			return num3;
		}

		/// <summary>Returns a nonnegative random number.</summary>
		/// <returns>A 32-bit signed integer greater than or equal to zero and less than <see cref="F:System.Int32.MaxValue" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001218 RID: 4632 RVA: 0x00049D3B File Offset: 0x00047F3B
		public virtual int Next()
		{
			return this.InternalSample();
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00049D44 File Offset: 0x00047F44
		private double GetSampleForLargeRange()
		{
			int num = this.InternalSample();
			if (this.InternalSample() % 2 == 0)
			{
				num = -num;
			}
			return ((double)num + 2147483646.0) / 4294967293.0;
		}

		/// <summary>Returns a random number within a specified range.</summary>
		/// <returns>A 32-bit signed integer greater than or equal to <paramref name="minValue" /> and less than <paramref name="maxValue" />; that is, the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />. If <paramref name="minValue" /> equals <paramref name="maxValue" />, <paramref name="minValue" /> is returned.</returns>
		/// <param name="minValue">The inclusive lower bound of the random number returned. </param>
		/// <param name="maxValue">The exclusive upper bound of the random number returned. <paramref name="maxValue" /> must be greater than or equal to <paramref name="minValue" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="minValue" /> is greater than <paramref name="maxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600121A RID: 4634 RVA: 0x00049D84 File Offset: 0x00047F84
		public virtual int Next(int minValue, int maxValue)
		{
			if (minValue > maxValue)
			{
				throw new ArgumentOutOfRangeException("minValue", Environment.GetResourceString("'{0}' cannot be greater than {1}.", new object[] { "minValue", "maxValue" }));
			}
			long num = (long)maxValue - (long)minValue;
			if (num <= 2147483647L)
			{
				return (int)(this.Sample() * (double)num) + minValue;
			}
			return (int)((long)(this.GetSampleForLargeRange() * (double)num) + (long)minValue);
		}

		/// <summary>Returns a nonnegative random number less than the specified maximum.</summary>
		/// <returns>A 32-bit signed integer greater than or equal to zero, and less than <paramref name="maxValue" />; that is, the range of return values ordinarily includes zero but not <paramref name="maxValue" />. However, if <paramref name="maxValue" /> equals zero, <paramref name="maxValue" /> is returned.</returns>
		/// <param name="maxValue">The exclusive upper bound of the random number to be generated. <paramref name="maxValue" /> must be greater than or equal to zero. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxValue" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600121B RID: 4635 RVA: 0x00049DEA File Offset: 0x00047FEA
		public virtual int Next(int maxValue)
		{
			if (maxValue < 0)
			{
				throw new ArgumentOutOfRangeException("maxValue", Environment.GetResourceString("'{0}' must be greater than zero.", new object[] { "maxValue" }));
			}
			return (int)(this.Sample() * (double)maxValue);
		}

		/// <summary>Returns a random number between 0.0 and 1.0.</summary>
		/// <returns>A double-precision floating point number greater than or equal to 0.0, and less than 1.0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600121C RID: 4636 RVA: 0x00049E1D File Offset: 0x0004801D
		public virtual double NextDouble()
		{
			return this.Sample();
		}

		/// <summary>Fills the elements of a specified array of bytes with random numbers.</summary>
		/// <param name="buffer">An array of bytes to contain random numbers. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600121D RID: 4637 RVA: 0x00049E28 File Offset: 0x00048028
		public virtual void NextBytes(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = (byte)(this.InternalSample() % 256);
			}
		}

		// Token: 0x04000A59 RID: 2649
		private const int MBIG = 2147483647;

		// Token: 0x04000A5A RID: 2650
		private const int MSEED = 161803398;

		// Token: 0x04000A5B RID: 2651
		private const int MZ = 0;

		// Token: 0x04000A5C RID: 2652
		private int inext;

		// Token: 0x04000A5D RID: 2653
		private int inextp;

		// Token: 0x04000A5E RID: 2654
		private int[] SeedArray = new int[56];
	}
}
