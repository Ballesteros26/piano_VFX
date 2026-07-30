using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Represents access to generic isolated storage capabilities.</summary>
	// Token: 0x0200059A RID: 1434
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
	[Serializable]
	public abstract class IsolatedStoragePermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.IsolatedStoragePermission" /> class with either restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06003FEE RID: 16366 RVA: 0x000E4408 File Offset: 0x000E2608
		protected IsolatedStoragePermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this.UsageAllowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
			}
		}

		/// <summary>Gets or sets the quota on the overall size of each user's total store.</summary>
		/// <returns>The size, in bytes, of the resource allocated to the user.</returns>
		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06003FEF RID: 16367 RVA: 0x000E4425 File Offset: 0x000E2625
		// (set) Token: 0x06003FF0 RID: 16368 RVA: 0x000E442D File Offset: 0x000E262D
		public long UserQuota
		{
			get
			{
				return this.m_userQuota;
			}
			set
			{
				this.m_userQuota = value;
			}
		}

		/// <summary>Gets or sets the type of isolated storage containment allowed.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.IsolatedStorageContainment" /> values.</returns>
		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06003FF1 RID: 16369 RVA: 0x000E4436 File Offset: 0x000E2636
		// (set) Token: 0x06003FF2 RID: 16370 RVA: 0x000E4440 File Offset: 0x000E2640
		public IsolatedStorageContainment UsageAllowed
		{
			get
			{
				return this.m_allowed;
			}
			set
			{
				if (!Enum.IsDefined(typeof(IsolatedStorageContainment), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "IsolatedStorageContainment");
				}
				this.m_allowed = value;
				if (this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage)
				{
					this.m_userQuota = long.MaxValue;
					this.m_machineQuota = long.MaxValue;
					this.m_expirationDays = long.MaxValue;
					this.m_permanentData = true;
				}
			}
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06003FF3 RID: 16371 RVA: 0x000E44CC File Offset: 0x000E26CC
		public bool IsUnrestricted()
		{
			return IsolatedStorageContainment.UnrestrictedIsolatedStorage == this.m_allowed;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06003FF4 RID: 16372 RVA: 0x000E44DC File Offset: 0x000E26DC
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				securityElement.AddAttribute("Allowed", this.m_allowed.ToString());
				if (this.m_userQuota > 0L)
				{
					securityElement.AddAttribute("UserQuota", this.m_userQuota.ToString());
				}
			}
			return securityElement;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not valid. </exception>
		// Token: 0x06003FF5 RID: 16373 RVA: 0x000E4550 File Offset: 0x000E2750
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			this.m_userQuota = 0L;
			this.m_machineQuota = 0L;
			this.m_expirationDays = 0L;
			this.m_permanentData = false;
			this.m_allowed = IsolatedStorageContainment.None;
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this.UsageAllowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
				return;
			}
			string text = esd.Attribute("Allowed");
			if (text != null)
			{
				this.UsageAllowed = (IsolatedStorageContainment)Enum.Parse(typeof(IsolatedStorageContainment), text);
			}
			text = esd.Attribute("UserQuota");
			if (text != null)
			{
				this.m_userQuota = long.Parse(text, CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x06003FF6 RID: 16374 RVA: 0x000E45EF File Offset: 0x000E27EF
		internal bool IsEmpty()
		{
			return this.m_userQuota == 0L && this.m_allowed == IsolatedStorageContainment.None;
		}

		// Token: 0x04002073 RID: 8307
		private const int version = 1;

		// Token: 0x04002074 RID: 8308
		internal long m_userQuota;

		// Token: 0x04002075 RID: 8309
		internal long m_machineQuota;

		// Token: 0x04002076 RID: 8310
		internal long m_expirationDays;

		// Token: 0x04002077 RID: 8311
		internal bool m_permanentData;

		// Token: 0x04002078 RID: 8312
		internal IsolatedStorageContainment m_allowed;
	}
}
