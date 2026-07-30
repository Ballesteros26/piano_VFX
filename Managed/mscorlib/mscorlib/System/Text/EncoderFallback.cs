using System;
using System.Threading;

namespace System.Text
{
	/// <summary>Provides a failure-handling mechanism, called a fallback, for an input character that cannot be converted to an encoded output byte sequence. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000277 RID: 631
	[Serializable]
	public abstract class EncoderFallback
	{
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x0006C34C File Offset: 0x0006A54C
		private static object InternalSyncObject
		{
			get
			{
				if (EncoderFallback.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref EncoderFallback.s_InternalSyncObject, obj, null);
				}
				return EncoderFallback.s_InternalSyncObject;
			}
		}

		/// <summary>Gets an object that outputs a substitute string in place of an input character that cannot be encoded.</summary>
		/// <returns>A type derived from the <see cref="T:System.Text.EncoderFallback" /> class. The default value is a <see cref="T:System.Text.EncoderReplacementFallback" /> object that replaces unknown input characters with the QUESTION MARK character ("?", U+003F).</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x0006C378 File Offset: 0x0006A578
		public static EncoderFallback ReplacementFallback
		{
			get
			{
				if (EncoderFallback.replacementFallback == null)
				{
					object internalSyncObject = EncoderFallback.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (EncoderFallback.replacementFallback == null)
						{
							EncoderFallback.replacementFallback = new EncoderReplacementFallback();
						}
					}
				}
				return EncoderFallback.replacementFallback;
			}
		}

		/// <summary>Gets an object that throws an exception when an input character cannot be encoded.</summary>
		/// <returns>A type derived from the <see cref="T:System.Text.EncoderFallback" /> class. The default value is a <see cref="T:System.Text.EncoderExceptionFallback" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001CCB RID: 7371 RVA: 0x0006C3D8 File Offset: 0x0006A5D8
		public static EncoderFallback ExceptionFallback
		{
			get
			{
				if (EncoderFallback.exceptionFallback == null)
				{
					object internalSyncObject = EncoderFallback.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (EncoderFallback.exceptionFallback == null)
						{
							EncoderFallback.exceptionFallback = new EncoderExceptionFallback();
						}
					}
				}
				return EncoderFallback.exceptionFallback;
			}
		}

		/// <summary>When overridden in a derived class, initializes a new instance of the <see cref="T:System.Text.EncoderFallbackBuffer" /> class. </summary>
		/// <returns>An object that provides a fallback buffer for an encoder.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001CCC RID: 7372
		public abstract EncoderFallbackBuffer CreateFallbackBuffer();

		/// <summary>When overridden in a derived class, gets the maximum number of characters the current <see cref="T:System.Text.EncoderFallback" /> object can return.</summary>
		/// <returns>The maximum number of characters the current <see cref="T:System.Text.EncoderFallback" /> object can return.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001CCD RID: 7373
		public abstract int MaxCharCount { get; }

		// Token: 0x04000FE4 RID: 4068
		internal bool bIsMicrosoftBestFitFallback;

		// Token: 0x04000FE5 RID: 4069
		private static volatile EncoderFallback replacementFallback;

		// Token: 0x04000FE6 RID: 4070
		private static volatile EncoderFallback exceptionFallback;

		// Token: 0x04000FE7 RID: 4071
		private static object s_InternalSyncObject;
	}
}
