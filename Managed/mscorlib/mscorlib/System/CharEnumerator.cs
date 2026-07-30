using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity;

namespace System
{
	/// <summary>Supports iterating over a <see cref="T:System.String" /> object and reading its individual characters. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000138 RID: 312
	[ComVisible(true)]
	[Serializable]
	public sealed class CharEnumerator : IEnumerator, ICloneable, IEnumerator<char>, IDisposable
	{
		// Token: 0x06000B8C RID: 2956 RVA: 0x0003598F File Offset: 0x00033B8F
		internal CharEnumerator(string str)
		{
			this.str = str;
			this.index = -1;
		}

		/// <summary>Creates a copy of the current <see cref="T:System.CharEnumerator" /> object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a copy of the current <see cref="T:System.CharEnumerator" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B8D RID: 2957 RVA: 0x0002C3A3 File Offset: 0x0002A5A3
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Increments the internal index of the current <see cref="T:System.CharEnumerator" /> object to the next character of the enumerated string.</summary>
		/// <returns>true if the index is successfully incremented and within the enumerated string; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B8E RID: 2958 RVA: 0x000359A8 File Offset: 0x00033BA8
		public bool MoveNext()
		{
			if (this.index < this.str.Length - 1)
			{
				this.index++;
				this.currentElement = this.str[this.index];
				return true;
			}
			this.index = this.str.Length;
			return false;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.CharEnumerator" /> class.</summary>
		// Token: 0x06000B8F RID: 2959 RVA: 0x00035A03 File Offset: 0x00033C03
		public void Dispose()
		{
			if (this.str != null)
			{
				this.index = this.str.Length;
			}
			this.str = null;
		}

		/// <summary>Gets the currently referenced character in the string enumerated by this <see cref="T:System.CharEnumerator" /> object. For a description of this member, see <see cref="P:System.Collections.IEnumerator.Current" />. </summary>
		/// <returns>The boxed Unicode character currently referenced by this <see cref="T:System.CharEnumerator" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">Enumeration has not started.-or-Enumeration has ended.</exception>
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x00035A28 File Offset: 0x00033C28
		object IEnumerator.Current
		{
			get
			{
				if (this.index == -1)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
				}
				if (this.index >= this.str.Length)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
				}
				return this.currentElement;
			}
		}

		/// <summary>Gets the currently referenced character in the string enumerated by this <see cref="T:System.CharEnumerator" /> object.</summary>
		/// <returns>The Unicode character currently referenced by this <see cref="T:System.CharEnumerator" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The index is invalid; that is, it is before the first or after the last character of the enumerated string. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x00035A7C File Offset: 0x00033C7C
		public char Current
		{
			get
			{
				if (this.index == -1)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration has not started. Call MoveNext."));
				}
				if (this.index >= this.str.Length)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Enumeration already finished."));
				}
				return this.currentElement;
			}
		}

		/// <summary>Initializes the index to a position logically before the first character of the enumerated string.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B92 RID: 2962 RVA: 0x00035ACB File Offset: 0x00033CCB
		public void Reset()
		{
			this.currentElement = '\0';
			this.index = -1;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal CharEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040007D1 RID: 2001
		private string str;

		// Token: 0x040007D2 RID: 2002
		private int index;

		// Token: 0x040007D3 RID: 2003
		private char currentElement;
	}
}
