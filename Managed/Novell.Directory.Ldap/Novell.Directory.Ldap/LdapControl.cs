using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000017 RID: 23
	public class LdapControl : ICloneable
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00006824 File Offset: 0x00004A24
		public virtual string ID
		{
			get
			{
				return new StringBuilder(this.control.ControlType.stringValue()).ToString();
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006840 File Offset: 0x00004A40
		public virtual bool Critical
		{
			get
			{
				return this.control.Criticality.booleanValue();
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00006852 File Offset: 0x00004A52
		internal static RespControlVector RegisteredControls
		{
			get
			{
				return LdapControl.registeredControls;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00006859 File Offset: 0x00004A59
		internal virtual RfcControl Asn1Object
		{
			get
			{
				return this.control;
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006864 File Offset: 0x00004A64
		[CLSCompliant(false)]
		public LdapControl(string oid, bool critical, sbyte[] values)
		{
			if (oid == null)
			{
				throw new ArgumentException("An OID must be specified");
			}
			if (values == null)
			{
				this.control = new RfcControl(new RfcLdapOID(oid), new Asn1Boolean(critical));
				return;
			}
			this.control = new RfcControl(new RfcLdapOID(oid), new Asn1Boolean(critical), new Asn1OctetString(values));
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000068BD File Offset: 0x00004ABD
		protected internal LdapControl(RfcControl control)
		{
			this.control = control;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000068CC File Offset: 0x00004ACC
		public object Clone()
		{
			LdapControl ldapControl;
			try
			{
				ldapControl = (LdapControl)base.MemberwiseClone();
			}
			catch (Exception)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			sbyte[] value = this.getValue();
			if (value != null)
			{
				sbyte[] array = new sbyte[value.Length];
				for (int i = 0; i < value.Length; i++)
				{
					array[i] = value[i];
				}
				ldapControl.control = new RfcControl(new RfcLdapOID(this.ID), new Asn1Boolean(this.Critical), new Asn1OctetString(array));
			}
			return ldapControl;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006954 File Offset: 0x00004B54
		[CLSCompliant(false)]
		public virtual sbyte[] getValue()
		{
			sbyte[] array = null;
			Asn1OctetString controlValue = this.control.ControlValue;
			if (controlValue != null)
			{
				array = controlValue.byteValue();
			}
			return array;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000697A File Offset: 0x00004B7A
		[CLSCompliant(false)]
		protected internal virtual void setValue(sbyte[] controlValue)
		{
			this.control.ControlValue = new Asn1OctetString(controlValue);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000698D File Offset: 0x00004B8D
		public static void register(string oid, Type controlClass)
		{
			LdapControl.registeredControls.registerResponseControl(oid, controlClass);
		}

		// Token: 0x04000091 RID: 145
		private static RespControlVector registeredControls = new RespControlVector(5, 5);

		// Token: 0x04000092 RID: 146
		private RfcControl control;
	}
}
