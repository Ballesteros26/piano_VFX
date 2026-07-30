using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents an exception handler in a byte array of IL to be passed to a method such as <see cref="M:System.Reflection.Emit.MethodBuilder.SetMethodBody(System.Byte[],System.Int32,System.Byte[],System.Collections.Generic.IEnumerable{System.Reflection.Emit.ExceptionHandler},System.Collections.Generic.IEnumerable{System.Int32})" />.</summary>
	// Token: 0x02000340 RID: 832
	[ComVisible(false)]
	public struct ExceptionHandler : IEquatable<ExceptionHandler>
	{
		/// <summary>Gets the token of the exception type handled by this handler.</summary>
		/// <returns>The token of the exception type handled by this handler, or 0 if none exists.</returns>
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060024AC RID: 9388 RVA: 0x000844E3 File Offset: 0x000826E3
		public int ExceptionTypeToken
		{
			get
			{
				return this.m_exceptionClass;
			}
		}

		/// <summary>Gets the byte offset at which the code that is protected by this exception handler begins.</summary>
		/// <returns>The byte offset at which the code that is protected by this exception handler begins.</returns>
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060024AD RID: 9389 RVA: 0x000844EB File Offset: 0x000826EB
		public int TryOffset
		{
			get
			{
				return this.m_tryStartOffset;
			}
		}

		/// <summary>Gets the length, in bytes, of the code protected by this exception handler.</summary>
		/// <returns>The length, in bytes, of the code protected by this exception handler.</returns>
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060024AE RID: 9390 RVA: 0x000844F3 File Offset: 0x000826F3
		public int TryLength
		{
			get
			{
				return this.m_tryEndOffset - this.m_tryStartOffset;
			}
		}

		/// <summary>Gets the byte offset at which the filter code for the exception handler begins.</summary>
		/// <returns>The byte offset at which the filter code begins, or 0 if no filter  is present.</returns>
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060024AF RID: 9391 RVA: 0x00084502 File Offset: 0x00082702
		public int FilterOffset
		{
			get
			{
				return this.m_filterOffset;
			}
		}

		/// <summary>Gets the byte offset of the first instruction of the exception handler.</summary>
		/// <returns>The byte offset of the first instruction of the exception handler.</returns>
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060024B0 RID: 9392 RVA: 0x0008450A File Offset: 0x0008270A
		public int HandlerOffset
		{
			get
			{
				return this.m_handlerStartOffset;
			}
		}

		/// <summary>Gets the length, in bytes, of the exception handler.</summary>
		/// <returns>The length, in bytes, of the exception handler.</returns>
		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060024B1 RID: 9393 RVA: 0x00084512 File Offset: 0x00082712
		public int HandlerLength
		{
			get
			{
				return this.m_handlerEndOffset - this.m_handlerStartOffset;
			}
		}

		/// <summary>Gets a value that represents the kind of exception handler this object represents.</summary>
		/// <returns>One of the enumeration values that specifies the kind of exception handler.</returns>
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x00084521 File Offset: 0x00082721
		public ExceptionHandlingClauseOptions Kind
		{
			get
			{
				return this.m_kind;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Emit.ExceptionHandler" /> class with the specified parameters.</summary>
		/// <param name="tryOffset">The byte offset of the first instruction protected by this exception handler.</param>
		/// <param name="tryLength">The number of bytes protected by this exception handler.</param>
		/// <param name="filterOffset">The byte offset of the beginning of the filter code. The filter code ends at the first instruction of the handler block. For non-filter exception handlers, specify 0 (zero) for this parameter.</param>
		/// <param name="handlerOffset">The byte offset of the first instruction of this exception handler.</param>
		/// <param name="handlerLength">The number of bytes in this exception handler.</param>
		/// <param name="kind">One of the enumeration values that specifies the kind of exception handler.</param>
		/// <param name="exceptionTypeToken">The token of the exception type handled by this exception handler. If not applicable, specify 0 (zero).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="tryOffset" />, <paramref name="filterOffset" />, <paramref name="handlerOffset" />, <paramref name="tryLength" />, or <paramref name="handlerLength" /> are negative.</exception>
		// Token: 0x060024B3 RID: 9395 RVA: 0x0008452C File Offset: 0x0008272C
		public ExceptionHandler(int tryOffset, int tryLength, int filterOffset, int handlerOffset, int handlerLength, ExceptionHandlingClauseOptions kind, int exceptionTypeToken)
		{
			if (tryOffset < 0)
			{
				throw new ArgumentOutOfRangeException("tryOffset", Environment.GetResourceString("Non-negative number required."));
			}
			if (tryLength < 0)
			{
				throw new ArgumentOutOfRangeException("tryLength", Environment.GetResourceString("Non-negative number required."));
			}
			if (filterOffset < 0)
			{
				throw new ArgumentOutOfRangeException("filterOffset", Environment.GetResourceString("Non-negative number required."));
			}
			if (handlerOffset < 0)
			{
				throw new ArgumentOutOfRangeException("handlerOffset", Environment.GetResourceString("Non-negative number required."));
			}
			if (handlerLength < 0)
			{
				throw new ArgumentOutOfRangeException("handlerLength", Environment.GetResourceString("Non-negative number required."));
			}
			if ((long)tryOffset + (long)tryLength > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("tryLength", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[]
				{
					0,
					int.MaxValue - tryOffset
				}));
			}
			if ((long)handlerOffset + (long)handlerLength > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("handlerLength", Environment.GetResourceString("Valid values are between {0} and {1}, inclusive.", new object[]
				{
					0,
					int.MaxValue - handlerOffset
				}));
			}
			if (kind == ExceptionHandlingClauseOptions.Clause && (exceptionTypeToken & 16777215) == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Token {0:x} is not a valid Type token.", new object[] { exceptionTypeToken }), "exceptionTypeToken");
			}
			if (!ExceptionHandler.IsValidKind(kind))
			{
				throw new ArgumentOutOfRangeException("kind", Environment.GetResourceString("Enum value was out of legal range."));
			}
			this.m_tryStartOffset = tryOffset;
			this.m_tryEndOffset = tryOffset + tryLength;
			this.m_filterOffset = filterOffset;
			this.m_handlerStartOffset = handlerOffset;
			this.m_handlerEndOffset = handlerOffset + handlerLength;
			this.m_kind = kind;
			this.m_exceptionClass = exceptionTypeToken;
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000846C6 File Offset: 0x000828C6
		internal ExceptionHandler(int tryStartOffset, int tryEndOffset, int filterOffset, int handlerStartOffset, int handlerEndOffset, int kind, int exceptionTypeToken)
		{
			this.m_tryStartOffset = tryStartOffset;
			this.m_tryEndOffset = tryEndOffset;
			this.m_filterOffset = filterOffset;
			this.m_handlerStartOffset = handlerStartOffset;
			this.m_handlerEndOffset = handlerEndOffset;
			this.m_kind = (ExceptionHandlingClauseOptions)kind;
			this.m_exceptionClass = exceptionTypeToken;
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000846FD File Offset: 0x000828FD
		private static bool IsValidKind(ExceptionHandlingClauseOptions kind)
		{
			return kind <= ExceptionHandlingClauseOptions.Finally || kind == ExceptionHandlingClauseOptions.Fault;
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x0008470A File Offset: 0x0008290A
		public override int GetHashCode()
		{
			return this.m_exceptionClass ^ this.m_tryStartOffset ^ this.m_tryEndOffset ^ this.m_filterOffset ^ this.m_handlerStartOffset ^ this.m_handlerEndOffset ^ (int)this.m_kind;
		}

		/// <summary>Indicates whether this instance of the <see cref="T:System.Reflection.Emit.ExceptionHandler" /> object is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> and this instance are equal; otherwise, false.</returns>
		/// <param name="obj">The object to compare this instance to.</param>
		// Token: 0x060024B7 RID: 9399 RVA: 0x0008473C File Offset: 0x0008293C
		public override bool Equals(object obj)
		{
			return obj is ExceptionHandler && this.Equals((ExceptionHandler)obj);
		}

		/// <summary>Indicates whether this instance of the <see cref="T:System.Reflection.Emit.ExceptionHandler" /> object is equal to another <see cref="T:System.Reflection.Emit.ExceptionHandler" /> object.</summary>
		/// <returns>true if <paramref name="other" /> and this instance are equal; otherwise, false.</returns>
		/// <param name="other">The exception handler object to compare this instance to.</param>
		// Token: 0x060024B8 RID: 9400 RVA: 0x00084754 File Offset: 0x00082954
		public bool Equals(ExceptionHandler other)
		{
			return other.m_exceptionClass == this.m_exceptionClass && other.m_tryStartOffset == this.m_tryStartOffset && other.m_tryEndOffset == this.m_tryEndOffset && other.m_filterOffset == this.m_filterOffset && other.m_handlerStartOffset == this.m_handlerStartOffset && other.m_handlerEndOffset == this.m_handlerEndOffset && other.m_kind == this.m_kind;
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.Reflection.Emit.ExceptionHandler" /> are equal.</summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x060024B9 RID: 9401 RVA: 0x000847C5 File Offset: 0x000829C5
		public static bool operator ==(ExceptionHandler left, ExceptionHandler right)
		{
			return left.Equals(right);
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.Reflection.Emit.ExceptionHandler" /> are not equal.</summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x060024BA RID: 9402 RVA: 0x000847CF File Offset: 0x000829CF
		public static bool operator !=(ExceptionHandler left, ExceptionHandler right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04001377 RID: 4983
		internal readonly int m_exceptionClass;

		// Token: 0x04001378 RID: 4984
		internal readonly int m_tryStartOffset;

		// Token: 0x04001379 RID: 4985
		internal readonly int m_tryEndOffset;

		// Token: 0x0400137A RID: 4986
		internal readonly int m_filterOffset;

		// Token: 0x0400137B RID: 4987
		internal readonly int m_handlerStartOffset;

		// Token: 0x0400137C RID: 4988
		internal readonly int m_handlerEndOffset;

		// Token: 0x0400137D RID: 4989
		internal readonly ExceptionHandlingClauseOptions m_kind;
	}
}
