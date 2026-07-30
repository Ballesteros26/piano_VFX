using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents the Token returned by the metadata to represent an event.</summary>
	// Token: 0x02000358 RID: 856
	[ComVisible(true)]
	[Serializable]
	public struct EventToken
	{
		// Token: 0x0600265E RID: 9822 RVA: 0x00088D27 File Offset: 0x00086F27
		internal EventToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of EventToken and is equal to this instance.</summary>
		/// <returns>Returns true if <paramref name="obj" /> is an instance of EventToken and equals the current instance; otherwise, false.</returns>
		/// <param name="obj">The object to be compared with this instance. </param>
		// Token: 0x0600265F RID: 9823 RVA: 0x00088D30 File Offset: 0x00086F30
		public override bool Equals(object obj)
		{
			bool flag = obj is EventToken;
			if (flag)
			{
				EventToken eventToken = (EventToken)obj;
				flag = this.tokValue == eventToken.tokValue;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.EventToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to the current instance.</param>
		// Token: 0x06002660 RID: 9824 RVA: 0x00088D61 File Offset: 0x00086F61
		public bool Equals(EventToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.EventToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x06002661 RID: 9825 RVA: 0x00088D71 File Offset: 0x00086F71
		public static bool operator ==(EventToken a, EventToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.EventToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x06002662 RID: 9826 RVA: 0x00088D84 File Offset: 0x00086F84
		public static bool operator !=(EventToken a, EventToken b)
		{
			return !object.Equals(a, b);
		}

		/// <summary>Generates the hash code for this event.</summary>
		/// <returns>Returns the hash code for this instance.</returns>
		// Token: 0x06002663 RID: 9827 RVA: 0x00088D9A File Offset: 0x00086F9A
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for this event.</summary>
		/// <returns>Read-only. Retrieves the metadata token for this event.</returns>
		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06002664 RID: 9828 RVA: 0x00088D9A File Offset: 0x00086F9A
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x04001405 RID: 5125
		internal int tokValue;

		/// <summary>The default EventToken with <see cref="P:System.Reflection.Emit.EventToken.Token" /> value 0.</summary>
		// Token: 0x04001406 RID: 5126
		public static readonly EventToken Empty;
	}
}
