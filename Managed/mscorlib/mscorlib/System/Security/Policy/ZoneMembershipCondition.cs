using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing its zone of origin. This class cannot be inherited.</summary>
	// Token: 0x02000584 RID: 1412
	[ComVisible(true)]
	[Serializable]
	public sealed class ZoneMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition
	{
		// Token: 0x06003F3D RID: 16189 RVA: 0x000E23D1 File Offset: 0x000E05D1
		internal ZoneMembershipCondition()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ZoneMembershipCondition" /> class with the zone that determines membership.</summary>
		/// <param name="zone">The <see cref="T:System.Security.SecurityZone" /> for which to test. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="zone" /> parameter is not a valid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x06003F3E RID: 16190 RVA: 0x000E23E0 File Offset: 0x000E05E0
		public ZoneMembershipCondition(SecurityZone zone)
		{
			this.SecurityZone = zone;
		}

		/// <summary>Gets or sets the zone for which the membership condition tests.</summary>
		/// <returns>The zone for which the membership condition tests.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is null. </exception>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> to an invalid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06003F3F RID: 16191 RVA: 0x000E23F6 File Offset: 0x000E05F6
		// (set) Token: 0x06003F40 RID: 16192 RVA: 0x000E2400 File Offset: 0x000E0600
		public SecurityZone SecurityZone
		{
			get
			{
				return this.zone;
			}
			set
			{
				if (!Enum.IsDefined(typeof(SecurityZone), value))
				{
					throw new ArgumentException(Locale.GetText("invalid zone"));
				}
				if (value == SecurityZone.NoZone)
				{
					throw new ArgumentException(Locale.GetText("NoZone isn't valid for membership condition"));
				}
				this.zone = value;
			}
		}

		/// <summary>Determines whether the specified evidence satisfies the membership condition.</summary>
		/// <returns>true if the specified evidence satisfies the membership condition; otherwise, false.</returns>
		/// <param name="evidence">The evidence set against which to make the test. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is not a valid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x06003F41 RID: 16193 RVA: 0x000E2450 File Offset: 0x000E0650
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object obj = hostEnumerator.Current;
				Zone zone = obj as Zone;
				if (zone != null && zone.SecurityZone == this.zone)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new, identical copy of the current membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is not a valid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x06003F42 RID: 16194 RVA: 0x000E2493 File Offset: 0x000E0693
		public IMembershipCondition Copy()
		{
			return new ZoneMembershipCondition(this.zone);
		}

		/// <summary>Determines whether the zone from the specified object is equivalent to the zone contained in the current <see cref="T:System.Security.Policy.ZoneMembershipCondition" />.</summary>
		/// <returns>true if the zone from the specified object is equivalent to the zone contained in the current <see cref="T:System.Security.Policy.ZoneMembershipCondition" />; otherwise, false.</returns>
		/// <param name="o">The object to compare to the current <see cref="T:System.Security.Policy.ZoneMembershipCondition" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property for the current object or the specified object is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property for the current object or the specified object is not a valid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x06003F43 RID: 16195 RVA: 0x000E24A0 File Offset: 0x000E06A0
		public override bool Equals(object o)
		{
			ZoneMembershipCondition zoneMembershipCondition = o as ZoneMembershipCondition;
			return zoneMembershipCondition != null && zoneMembershipCondition.SecurityZone == this.zone;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element.</exception>
		// Token: 0x06003F44 RID: 16196 RVA: 0x000E24C7 File Offset: 0x000E06C7
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object. </param>
		/// <param name="level">The policy level context used to resolve named permission set references. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element. </exception>
		// Token: 0x06003F45 RID: 16197 RVA: 0x000E24D4 File Offset: 0x000E06D4
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
			string text = e.Attribute("Zone");
			if (text != null)
			{
				this.zone = (SecurityZone)Enum.Parse(typeof(SecurityZone), text);
			}
		}

		/// <summary>Gets the hash code for the current membership condition.</summary>
		/// <returns>The hash code for the current membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is not a valid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x06003F46 RID: 16198 RVA: 0x000E2523 File Offset: 0x000E0723
		public override int GetHashCode()
		{
			return this.zone.GetHashCode();
		}

		/// <summary>Creates and returns a string representation of the membership condition.</summary>
		/// <returns>A string representation of the state of the membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is not a valid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x06003F47 RID: 16199 RVA: 0x000E2536 File Offset: 0x000E0736
		public override string ToString()
		{
			return "Zone - " + this.zone;
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is not a valid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x06003F48 RID: 16200 RVA: 0x000E254D File Offset: 0x000E074D
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <param name="level">The policy level context for resolving named permission set references. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Policy.ZoneMembershipCondition.SecurityZone" /> property is not a valid <see cref="T:System.Security.SecurityZone" />. </exception>
		// Token: 0x06003F49 RID: 16201 RVA: 0x000E2556 File Offset: 0x000E0756
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = MembershipConditionHelper.Element(typeof(ZoneMembershipCondition), this.version);
			securityElement.AddAttribute("Zone", this.zone.ToString());
			return securityElement;
		}

		// Token: 0x04002015 RID: 8213
		private readonly int version = 1;

		// Token: 0x04002016 RID: 8214
		private SecurityZone zone;
	}
}
