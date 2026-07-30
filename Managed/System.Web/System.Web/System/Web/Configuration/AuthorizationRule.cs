using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Security.Principal;
using System.Web.Util;
using System.Xml;

namespace System.Web.Configuration
{
	/// <summary>The <see cref="T:System.Web.Configuration.AuthorizationRule" /> class allows you to programmatically access and modify the authorization section of a configuration file. This class cannot be inherited.</summary>
	// Token: 0x02000584 RID: 1412
	public sealed class AuthorizationRule : ConfigurationElement
	{
		// Token: 0x06003B9A RID: 15258 RVA: 0x0009F77C File Offset: 0x0009D97C
		static AuthorizationRule()
		{
			AuthorizationRule.properties.Add(AuthorizationRule.rolesProp);
			AuthorizationRule.properties.Add(AuthorizationRule.usersProp);
			AuthorizationRule.properties.Add(AuthorizationRule.verbsProp);
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Web.Configuration.AuthorizationRule" /> class using the passed object. </summary>
		/// <param name="action">The <see cref="T:System.Web.Configuration.AuthorizationRule" /> object to use to initialize the new instance.</param>
		// Token: 0x06003B9B RID: 15259 RVA: 0x0009F830 File Offset: 0x0009DA30
		public AuthorizationRule(AuthorizationRuleAction action)
		{
			this.action = action;
			base[AuthorizationRule.rolesProp] = new CommaDelimitedStringCollection();
			base[AuthorizationRule.usersProp] = new CommaDelimitedStringCollection();
			base[AuthorizationRule.verbsProp] = new CommaDelimitedStringCollection();
		}

		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <returns>true if the objects are equal; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x06003B9C RID: 15260 RVA: 0x0009F884 File Offset: 0x0009DA84
		public override bool Equals(object obj)
		{
			AuthorizationRule authorizationRule = obj as AuthorizationRule;
			if (authorizationRule == null)
			{
				return false;
			}
			if (this.action != authorizationRule.Action)
			{
				return false;
			}
			if (this.Roles.Count != authorizationRule.Roles.Count || this.Users.Count != authorizationRule.Users.Count || this.Verbs.Count != authorizationRule.Verbs.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Roles.Count; i++)
			{
				if (this.Roles[i] != authorizationRule.Roles[i])
				{
					return false;
				}
			}
			for (int i = 0; i < this.Users.Count; i++)
			{
				if (this.Users[i] != authorizationRule.Users[i])
				{
					return false;
				}
			}
			for (int i = 0; i < this.Verbs.Count; i++)
			{
				if (this.Verbs[i] != authorizationRule.Verbs[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Serves as a hash function for this object.</summary>
		/// <returns>An integer representing the hash code for the current object.</returns>
		// Token: 0x06003B9D RID: 15261 RVA: 0x0009F9A0 File Offset: 0x0009DBA0
		public override int GetHashCode()
		{
			int num = (int)this.action;
			for (int i = 0; i < this.Roles.Count; i++)
			{
				num += this.Roles[i].GetHashCode();
			}
			for (int i = 0; i < this.Users.Count; i++)
			{
				num += this.Users[i].GetHashCode();
			}
			for (int i = 0; i < this.Verbs.Count; i++)
			{
				num += this.Verbs[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x06003B9E RID: 15262 RVA: 0x0009FA33 File Offset: 0x0009DC33
		[global::System.MonoTODO("Not implemented")]
		protected internal override bool IsModified()
		{
			return ((CommaDelimitedStringCollection)this.Roles).IsModified || ((CommaDelimitedStringCollection)this.Users).IsModified || ((CommaDelimitedStringCollection)this.Verbs).IsModified;
		}

		// Token: 0x06003B9F RID: 15263 RVA: 0x0009FA6E File Offset: 0x0009DC6E
		private void VerifyData()
		{
			if (this.Roles.Count == 0 && this.Users.Count == 0)
			{
				throw new ConfigurationErrorsException("You must supply either a list of users or roles when creating an AuthorizationRule");
			}
		}

		// Token: 0x06003BA0 RID: 15264 RVA: 0x0009FA95 File Offset: 0x0009DC95
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
			this.VerifyData();
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x0009FAA3 File Offset: 0x0009DCA3
		protected override void PreSerialize(XmlWriter writer)
		{
			base.PreSerialize(writer);
			this.VerifyData();
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x0009FAB4 File Offset: 0x0009DCB4
		protected internal override void Reset(ConfigurationElement parentElement)
		{
			AuthorizationRule authorizationRule = (AuthorizationRule)parentElement;
			this.Action = authorizationRule.Action;
			base.Reset(parentElement);
		}

		// Token: 0x06003BA3 RID: 15267 RVA: 0x0009FADB File Offset: 0x0009DCDB
		protected internal override void ResetModified()
		{
			base.ResetModified();
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x0009FAE4 File Offset: 0x0009DCE4
		protected internal override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			if (this.saveMode != ConfigurationSaveMode.Full && !this.IsModified())
			{
				return true;
			}
			this.PreSerialize(writer);
			writer.WriteStartElement((this.action == AuthorizationRuleAction.Allow) ? "allow" : "deny");
			if (this.Roles.Count > 0)
			{
				writer.WriteAttributeString("roles", this.Roles.ToString());
			}
			if (this.Users.Count > 0)
			{
				writer.WriteAttributeString("users", this.Users.ToString());
			}
			if (this.Verbs.Count > 0)
			{
				writer.WriteAttributeString("verbs", this.Verbs.ToString());
			}
			writer.WriteEndElement();
			return true;
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x0009FB99 File Offset: 0x0009DD99
		protected internal override void SetReadOnly()
		{
			base.SetReadOnly();
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x0009FBA4 File Offset: 0x0009DDA4
		protected internal override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			this.saveMode = saveMode;
			AuthorizationRule authorizationRule = sourceElement as AuthorizationRule;
			if (authorizationRule != null)
			{
				this.action = authorizationRule.Action;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.AuthorizationRule" /> action.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.AuthorizationRuleAction" /> values.</returns>
		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x06003BA7 RID: 15271 RVA: 0x0009FBD7 File Offset: 0x0009DDD7
		// (set) Token: 0x06003BA8 RID: 15272 RVA: 0x0009FBDF File Offset: 0x0009DDDF
		public AuthorizationRuleAction Action
		{
			get
			{
				return this.action;
			}
			set
			{
				this.action = value;
			}
		}

		/// <summary>Gets the roles associated with the resource.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> collection containing the roles whose authorization must be verified.</returns>
		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x06003BA9 RID: 15273 RVA: 0x0009FBE8 File Offset: 0x0009DDE8
		[ConfigurationProperty("roles")]
		[TypeConverter(typeof(CommaDelimitedStringCollectionConverter))]
		public StringCollection Roles
		{
			get
			{
				return (StringCollection)base[AuthorizationRule.rolesProp];
			}
		}

		/// <summary>Gets the users associated with the resource.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> collection containing the users whose authorization must be verified.</returns>
		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x06003BAA RID: 15274 RVA: 0x0009FBFA File Offset: 0x0009DDFA
		[ConfigurationProperty("users")]
		[TypeConverter(typeof(CommaDelimitedStringCollectionConverter))]
		public StringCollection Users
		{
			get
			{
				return (StringCollection)base[AuthorizationRule.usersProp];
			}
		}

		/// <summary>Gets the verbs associated with the resource.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> collection containing the verbs whose authorization must be verified. </returns>
		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06003BAB RID: 15275 RVA: 0x0009FC0C File Offset: 0x0009DE0C
		[ConfigurationProperty("verbs")]
		[TypeConverter(typeof(CommaDelimitedStringCollectionConverter))]
		public StringCollection Verbs
		{
			get
			{
				return (StringCollection)base[AuthorizationRule.verbsProp];
			}
		}

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x06003BAC RID: 15276 RVA: 0x0009FC1E File Offset: 0x0009DE1E
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthorizationRule.properties;
			}
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x0009FC28 File Offset: 0x0009DE28
		internal bool CheckVerb(string verb)
		{
			using (StringEnumerator enumerator = this.Verbs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (string.Compare(enumerator.Current, verb, true, Helpers.InvariantCulture) == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x0009FC8C File Offset: 0x0009DE8C
		internal bool CheckUser(string user)
		{
			foreach (string text in this.Users)
			{
				if (string.Compare(text, user, true, Helpers.InvariantCulture) == 0 || text == "*" || (text == "?" && user == ""))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x0009FD18 File Offset: 0x0009DF18
		internal bool CheckRole(IPrincipal user)
		{
			foreach (string text in this.Roles)
			{
				if (user.IsInRole(text))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400208F RID: 8335
		private static ConfigurationProperty rolesProp = new ConfigurationProperty("roles", typeof(StringCollection), null, PropertyHelper.CommaDelimitedStringCollectionConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002090 RID: 8336
		private static ConfigurationProperty usersProp = new ConfigurationProperty("users", typeof(StringCollection), null, PropertyHelper.CommaDelimitedStringCollectionConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002091 RID: 8337
		private static ConfigurationProperty verbsProp = new ConfigurationProperty("verbs", typeof(StringCollection), null, PropertyHelper.CommaDelimitedStringCollectionConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002092 RID: 8338
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002093 RID: 8339
		private AuthorizationRuleAction action;

		// Token: 0x04002094 RID: 8340
		private ConfigurationSaveMode saveMode = ConfigurationSaveMode.Full;
	}
}
