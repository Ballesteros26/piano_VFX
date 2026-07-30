using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>A token that is returned when an event handler is added to a Windows Runtime event. The token is used to remove the event handler from the event at a later time. </summary>
	// Token: 0x0200095E RID: 2398
	public struct EventRegistrationToken
	{
		// Token: 0x06005934 RID: 22836 RVA: 0x0012ABD2 File Offset: 0x00128DD2
		internal EventRegistrationToken(ulong value)
		{
			this.m_value = value;
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06005935 RID: 22837 RVA: 0x0012ABDB File Offset: 0x00128DDB
		internal ulong Value
		{
			get
			{
				return this.m_value;
			}
		}

		/// <summary>Indicates whether two <see cref="T:System.Runtime.InteropServices.WindowsRuntime.EventRegistrationToken" /> instances are equal. </summary>
		/// <returns>true if the two objects are equal; otherwise, false. </returns>
		/// <param name="left">The first instance to compare. </param>
		/// <param name="right">The second instance to compare. </param>
		// Token: 0x06005936 RID: 22838 RVA: 0x0012ABE3 File Offset: 0x00128DE3
		public static bool operator ==(EventRegistrationToken left, EventRegistrationToken right)
		{
			return left.Equals(right);
		}

		/// <summary>Indicates whether two <see cref="T:System.Runtime.InteropServices.WindowsRuntime.EventRegistrationToken" /> instances are not equal.</summary>
		/// <returns>true if the two instances are not equal; otherwise, false. </returns>
		/// <param name="left">The first instance to compare. </param>
		/// <param name="right">The second instance to compare. </param>
		// Token: 0x06005937 RID: 22839 RVA: 0x0012ABF8 File Offset: 0x00128DF8
		public static bool operator !=(EventRegistrationToken left, EventRegistrationToken right)
		{
			return !left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether the current object is equal to the specified object. </summary>
		/// <returns>true  if the current object is equal to <paramref name="obj" />; otherwise, false.</returns>
		/// <param name="obj">The object to compare.</param>
		// Token: 0x06005938 RID: 22840 RVA: 0x0012AC10 File Offset: 0x00128E10
		public override bool Equals(object obj)
		{
			return obj is EventRegistrationToken && ((EventRegistrationToken)obj).Value == this.Value;
		}

		/// <summary>Returns the hash code for this instance. </summary>
		/// <returns>The hash code for this instance. </returns>
		// Token: 0x06005939 RID: 22841 RVA: 0x0012AC3D File Offset: 0x00128E3D
		public override int GetHashCode()
		{
			return this.m_value.GetHashCode();
		}

		// Token: 0x04002E0A RID: 11786
		internal ulong m_value;
	}
}
