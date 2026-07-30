using System;
using System.Threading;

namespace System.Text
{
	/// <summary>Provides a failure-handling mechanism, called a fallback, for an encoded input byte sequence that cannot be converted to an output character. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200026C RID: 620
	[Serializable]
	public abstract class DecoderFallback
	{
		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x0006AFFC File Offset: 0x000691FC
		private static object InternalSyncObject
		{
			get
			{
				if (DecoderFallback.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref DecoderFallback.s_InternalSyncObject, obj, null);
				}
				return DecoderFallback.s_InternalSyncObject;
			}
		}

		/// <summary>Gets an object that outputs a substitute string in place of an input byte sequence that cannot be decoded.</summary>
		/// <returns>A type derived from the <see cref="T:System.Text.DecoderFallback" /> class. The default value is a <see cref="T:System.Text.DecoderReplacementFallback" /> object that emits the QUESTION MARK character ("?", U+003F) in place of unknown byte sequences. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001C69 RID: 7273 RVA: 0x0006B028 File Offset: 0x00069228
		public static DecoderFallback ReplacementFallback
		{
			get
			{
				if (DecoderFallback.replacementFallback == null)
				{
					object internalSyncObject = DecoderFallback.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (DecoderFallback.replacementFallback == null)
						{
							DecoderFallback.replacementFallback = new DecoderReplacementFallback();
						}
					}
				}
				return DecoderFallback.replacementFallback;
			}
		}

		/// <summary>Gets an object that throws an exception when an input byte sequence cannot be decoded.</summary>
		/// <returns>A type derived from the <see cref="T:System.Text.DecoderFallback" /> class. The default value is a <see cref="T:System.Text.DecoderExceptionFallback" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x0006B088 File Offset: 0x00069288
		public static DecoderFallback ExceptionFallback
		{
			get
			{
				if (DecoderFallback.exceptionFallback == null)
				{
					object internalSyncObject = DecoderFallback.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (DecoderFallback.exceptionFallback == null)
						{
							DecoderFallback.exceptionFallback = new DecoderExceptionFallback();
						}
					}
				}
				return DecoderFallback.exceptionFallback;
			}
		}

		/// <summary>When overridden in a derived class, initializes a new instance of the <see cref="T:System.Text.DecoderFallbackBuffer" /> class. </summary>
		/// <returns>An object that provides a fallback buffer for a decoder.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001C6B RID: 7275
		public abstract DecoderFallbackBuffer CreateFallbackBuffer();

		/// <summary>When overridden in a derived class, gets the maximum number of characters the current <see cref="T:System.Text.DecoderFallback" /> object can return.</summary>
		/// <returns>The maximum number of characters the current <see cref="T:System.Text.DecoderFallback" /> object can return.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001C6C RID: 7276
		public abstract int MaxCharCount { get; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001C6D RID: 7277 RVA: 0x0006B0E8 File Offset: 0x000692E8
		internal bool IsMicrosoftBestFitFallback
		{
			get
			{
				return this.bIsMicrosoftBestFitFallback;
			}
		}

		// Token: 0x04000FC9 RID: 4041
		internal bool bIsMicrosoftBestFitFallback;

		// Token: 0x04000FCA RID: 4042
		private static volatile DecoderFallback replacementFallback;

		// Token: 0x04000FCB RID: 4043
		private static volatile DecoderFallback exceptionFallback;

		// Token: 0x04000FCC RID: 4044
		private static object s_InternalSyncObject;
	}
}
