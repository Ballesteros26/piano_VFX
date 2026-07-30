using System;
using Unity;

namespace System.Security.AccessControl
{
	/// <summary>Represents an Access Control Entry (ACE) that contains a qualifier. The qualifier, represented by an <see cref="T:System.Security.AccessControl.AceQualifier" /> object, specifies whether the ACE allows access, denies access, causes system audits, or causes system alarms. The <see cref="T:System.Security.AccessControl.QualifiedAce" /> class is the abstract base class for the <see cref="T:System.Security.AccessControl.CommonAce" /> and <see cref="T:System.Security.AccessControl.ObjectAce" /> classes.</summary>
	// Token: 0x02000608 RID: 1544
	public abstract class QualifiedAce : KnownAce
	{
		// Token: 0x06004394 RID: 17300 RVA: 0x000ED3AB File Offset: 0x000EB5AB
		internal QualifiedAce(AceType type, AceFlags flags, byte[] opaque)
			: base(type, flags)
		{
			this.SetOpaque(opaque);
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x000ED3BC File Offset: 0x000EB5BC
		internal QualifiedAce(byte[] binaryForm, int offset)
			: base(binaryForm, offset)
		{
		}

		/// <summary>Gets a value that specifies whether the ACE allows access, denies access, causes system audits, or causes system alarms.</summary>
		/// <returns>A value that specifies whether the ACE allows access, denies access, causes system audits, or causes system alarms.</returns>
		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06004396 RID: 17302 RVA: 0x000ED3C8 File Offset: 0x000EB5C8
		public AceQualifier AceQualifier
		{
			get
			{
				switch (base.AceType)
				{
				case AceType.AccessAllowed:
				case AceType.AccessAllowedCompound:
				case AceType.AccessAllowedObject:
				case AceType.AccessAllowedCallback:
				case AceType.AccessAllowedCallbackObject:
					return AceQualifier.AccessAllowed;
				case AceType.AccessDenied:
				case AceType.AccessDeniedObject:
				case AceType.AccessDeniedCallback:
				case AceType.AccessDeniedCallbackObject:
					return AceQualifier.AccessDenied;
				case AceType.SystemAudit:
				case AceType.SystemAuditObject:
				case AceType.SystemAuditCallback:
				case AceType.SystemAuditCallbackObject:
					return AceQualifier.SystemAudit;
				case AceType.SystemAlarm:
				case AceType.SystemAlarmObject:
				case AceType.SystemAlarmCallback:
				case AceType.SystemAlarmCallbackObject:
					return AceQualifier.SystemAlarm;
				default:
					throw new ArgumentException("Unrecognised ACE type: " + base.AceType);
				}
			}
		}

		/// <summary>Specifies whether this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object contains callback data.</summary>
		/// <returns>true if this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object contains callback data; otherwise, false.</returns>
		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06004397 RID: 17303 RVA: 0x000ED44C File Offset: 0x000EB64C
		public bool IsCallback
		{
			get
			{
				return base.AceType == AceType.AccessAllowedCallback || base.AceType == AceType.AccessAllowedCallbackObject || base.AceType == AceType.AccessDeniedCallback || base.AceType == AceType.AccessDeniedCallbackObject || base.AceType == AceType.SystemAlarmCallback || base.AceType == AceType.SystemAlarmCallbackObject || base.AceType == AceType.SystemAuditCallback || base.AceType == AceType.SystemAuditCallbackObject;
			}
		}

		/// <summary>Gets the length of the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object. This property is valid only for callback Access Control Entries (ACEs).</summary>
		/// <returns>The length of the opaque callback data.</returns>
		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06004398 RID: 17304 RVA: 0x000ED4AB File Offset: 0x000EB6AB
		public int OpaqueLength
		{
			get
			{
				if (this.opaque == null)
				{
					return 0;
				}
				return this.opaque.Length;
			}
		}

		/// <summary>Returns the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object. </summary>
		/// <returns>An array of byte values that represents the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object.</returns>
		// Token: 0x06004399 RID: 17305 RVA: 0x000ED4BF File Offset: 0x000EB6BF
		public byte[] GetOpaque()
		{
			if (this.opaque == null)
			{
				return null;
			}
			return (byte[])this.opaque.Clone();
		}

		/// <summary>Sets the opaque callback data associated with this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object.</summary>
		/// <param name="opaque">An array of byte values that represents the opaque callback data for this <see cref="T:System.Security.AccessControl.QualifiedAce" /> object.</param>
		// Token: 0x0600439A RID: 17306 RVA: 0x000ED4DB File Offset: 0x000EB6DB
		public void SetOpaque(byte[] opaque)
		{
			if (opaque == null)
			{
				this.opaque = null;
				return;
			}
			this.opaque = (byte[])opaque.Clone();
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal QualifiedAce()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040021E8 RID: 8680
		private byte[] opaque;
	}
}
