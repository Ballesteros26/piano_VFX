using System;
using System.Collections;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000016 RID: 22
	public class LdapConstraints : ICloneable
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000662C File Offset: 0x0000482C
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00006634 File Offset: 0x00004834
		public virtual int HopLimit
		{
			get
			{
				return this.hopLimit;
			}
			set
			{
				this.hopLimit = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000663D File Offset: 0x0000483D
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00006645 File Offset: 0x00004845
		internal virtual Hashtable Properties
		{
			get
			{
				return this.properties;
			}
			set
			{
				this.properties = (Hashtable)value.Clone();
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00006658 File Offset: 0x00004858
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00006660 File Offset: 0x00004860
		public virtual bool ReferralFollowing
		{
			get
			{
				return this.doReferrals;
			}
			set
			{
				this.doReferrals = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00006669 File Offset: 0x00004869
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00006671 File Offset: 0x00004871
		public virtual int TimeLimit
		{
			get
			{
				return this.msLimit;
			}
			set
			{
				this.msLimit = value;
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000667A File Offset: 0x0000487A
		public LdapConstraints()
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000668A File Offset: 0x0000488A
		public LdapConstraints(int msLimit, bool doReferrals, LdapReferralHandler handler, int hop_limit)
		{
			this.msLimit = msLimit;
			this.doReferrals = doReferrals;
			this.refHandler = handler;
			this.hopLimit = hop_limit;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000066B7 File Offset: 0x000048B7
		public virtual LdapControl[] getControls()
		{
			return this.controls;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000066BF File Offset: 0x000048BF
		public virtual object getProperty(string name)
		{
			if (this.properties == null)
			{
				return null;
			}
			return this.properties[name];
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000066D7 File Offset: 0x000048D7
		internal virtual LdapReferralHandler getReferralHandler()
		{
			return this.refHandler;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000066DF File Offset: 0x000048DF
		public virtual void setControls(LdapControl control)
		{
			if (control == null)
			{
				this.controls = null;
				return;
			}
			this.controls = new LdapControl[1];
			this.controls[0] = (LdapControl)control.Clone();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000670C File Offset: 0x0000490C
		public virtual void setControls(LdapControl[] controls)
		{
			if (controls == null || controls.Length == 0)
			{
				this.controls = null;
				return;
			}
			this.controls = new LdapControl[controls.Length];
			for (int i = 0; i < controls.Length; i++)
			{
				this.controls[i] = (LdapControl)controls[i].Clone();
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006759 File Offset: 0x00004959
		public virtual void setProperty(string name, object value_Renamed)
		{
			if (this.properties == null)
			{
				this.properties = new Hashtable();
			}
			SupportClass.PutElement(this.properties, name, value_Renamed);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000677C File Offset: 0x0000497C
		public virtual void setReferralHandler(LdapReferralHandler handler)
		{
			this.refHandler = handler;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006788 File Offset: 0x00004988
		public object Clone()
		{
			object obj2;
			try
			{
				object obj = base.MemberwiseClone();
				if (this.controls != null)
				{
					((LdapConstraints)obj).controls = new LdapControl[this.controls.Length];
					this.controls.CopyTo(((LdapConstraints)obj).controls, 0);
				}
				if (this.properties != null)
				{
					((LdapConstraints)obj).properties = (Hashtable)this.properties.Clone();
				}
				obj2 = obj;
			}
			catch (Exception)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return obj2;
		}

		// Token: 0x04000088 RID: 136
		private int msLimit;

		// Token: 0x04000089 RID: 137
		private int hopLimit = 10;

		// Token: 0x0400008A RID: 138
		private bool doReferrals;

		// Token: 0x0400008B RID: 139
		private LdapReferralHandler refHandler;

		// Token: 0x0400008C RID: 140
		private LdapControl[] controls;

		// Token: 0x0400008D RID: 141
		private static object nameLock = new object();

		// Token: 0x0400008E RID: 142
		private static int lConsNum;

		// Token: 0x0400008F RID: 143
		private string name;

		// Token: 0x04000090 RID: 144
		private Hashtable properties;
	}
}
