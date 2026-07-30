using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000015 RID: 21
	public class LdapConnection : ICloneable
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00004E18 File Offset: 0x00003018
		private void InitBlock()
		{
			this.defSearchCons = new LdapSearchConstraints();
			this.responseCtlSemaphore = new object();
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004E30 File Offset: 0x00003030
		public virtual int ProtocolVersion
		{
			get
			{
				BindProperties bindProperties = this.conn.BindProperties;
				if (bindProperties == null)
				{
					return 3;
				}
				return bindProperties.ProtocolVersion;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004E54 File Offset: 0x00003054
		public virtual string AuthenticationDN
		{
			get
			{
				BindProperties bindProperties = this.conn.BindProperties;
				if (bindProperties == null)
				{
					return null;
				}
				if (bindProperties.Anonymous)
				{
					return null;
				}
				return bindProperties.AuthenticationDN;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004E82 File Offset: 0x00003082
		public virtual string AuthenticationMethod
		{
			get
			{
				if (this.conn.BindProperties == null)
				{
					return "simple";
				}
				return this.conn.BindProperties.AuthenticationMethod;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004EA7 File Offset: 0x000030A7
		public virtual IDictionary SaslBindProperties
		{
			get
			{
				if (this.conn.BindProperties == null)
				{
					return null;
				}
				return this.conn.BindProperties.SaslBindProperties;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004EC8 File Offset: 0x000030C8
		public virtual object SaslBindCallbackHandler
		{
			get
			{
				if (this.conn.BindProperties == null)
				{
					return null;
				}
				return this.conn.BindProperties.SaslCallbackHandler;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00004EE9 File Offset: 0x000030E9
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00004EFC File Offset: 0x000030FC
		public virtual LdapConstraints Constraints
		{
			get
			{
				return (LdapConstraints)this.defSearchCons.Clone();
			}
			set
			{
				if (value is LdapSearchConstraints)
				{
					this.defSearchCons = (LdapSearchConstraints)value.Clone();
					return;
				}
				LdapSearchConstraints ldapSearchConstraints = (LdapSearchConstraints)this.defSearchCons.Clone();
				ldapSearchConstraints.HopLimit = value.HopLimit;
				ldapSearchConstraints.TimeLimit = value.TimeLimit;
				ldapSearchConstraints.setReferralHandler(value.getReferralHandler());
				ldapSearchConstraints.ReferralFollowing = value.ReferralFollowing;
				LdapControl[] controls = value.getControls();
				if (controls != null)
				{
					ldapSearchConstraints.setControls(controls);
				}
				Hashtable properties = ldapSearchConstraints.Properties;
				if (properties != null)
				{
					ldapSearchConstraints.Properties = properties;
				}
				this.defSearchCons = ldapSearchConstraints;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004F8D File Offset: 0x0000318D
		public virtual string Host
		{
			get
			{
				return this.conn.Host;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004F9A File Offset: 0x0000319A
		public virtual int Port
		{
			get
			{
				return this.conn.Port;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004FA7 File Offset: 0x000031A7
		public virtual LdapSearchConstraints SearchConstraints
		{
			get
			{
				return (LdapSearchConstraints)this.defSearchCons.Clone();
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004FB9 File Offset: 0x000031B9
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00004FC6 File Offset: 0x000031C6
		public bool SecureSocketLayer
		{
			get
			{
				return this.conn.Ssl;
			}
			set
			{
				this.conn.Ssl = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004FD4 File Offset: 0x000031D4
		public virtual bool Bound
		{
			get
			{
				return this.conn.Bound;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004FE1 File Offset: 0x000031E1
		public virtual bool Connected
		{
			get
			{
				return this.conn.Connected;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004FEE File Offset: 0x000031EE
		public virtual bool TLS
		{
			get
			{
				return this.conn.TLS;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004FFC File Offset: 0x000031FC
		public virtual LdapControl[] ResponseControls
		{
			get
			{
				if (this.responseCtls == null)
				{
					return null;
				}
				LdapControl[] array = new LdapControl[this.responseCtls.Length];
				object obj = this.responseCtlSemaphore;
				lock (obj)
				{
					for (int i = 0; i < this.responseCtls.Length; i++)
					{
						array[i] = (LdapControl)this.responseCtls[i].Clone();
					}
				}
				return array;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00005078 File Offset: 0x00003278
		internal virtual Connection Connection
		{
			get
			{
				return this.conn;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005080 File Offset: 0x00003280
		internal virtual string ConnectionName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000C7 RID: 199 RVA: 0x00005088 File Offset: 0x00003288
		// (remove) Token: 0x060000C8 RID: 200 RVA: 0x00005096 File Offset: 0x00003296
		public event CertificateValidationCallback UserDefinedServerCertValidationDelegate
		{
			add
			{
				this.conn.OnCertificateValidation += value;
			}
			remove
			{
				this.conn.OnCertificateValidation -= value;
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000050A4 File Offset: 0x000032A4
		public LdapConnection()
		{
			this.InitBlock();
			this.conn = new Connection();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000050C0 File Offset: 0x000032C0
		public object Clone()
		{
			object obj;
			LdapConnection ldapConnection;
			try
			{
				obj = base.MemberwiseClone();
				ldapConnection = (LdapConnection)obj;
			}
			catch (Exception)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			ldapConnection.conn = this.conn;
			if (this.defSearchCons != null)
			{
				ldapConnection.defSearchCons = (LdapSearchConstraints)this.defSearchCons.Clone();
			}
			else
			{
				ldapConnection.defSearchCons = null;
			}
			if (this.responseCtls != null)
			{
				ldapConnection.responseCtls = new LdapControl[this.responseCtls.Length];
				for (int i = 0; i < this.responseCtls.Length; i++)
				{
					ldapConnection.responseCtls[i] = (LdapControl)this.responseCtls[i].Clone();
				}
			}
			else
			{
				ldapConnection.responseCtls = null;
			}
			this.conn.incrCloneCount();
			return obj;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000518C File Offset: 0x0000338C
		~LdapConnection()
		{
			this.Disconnect(this.defSearchCons, false);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000051C0 File Offset: 0x000033C0
		public virtual object getProperty(string name)
		{
			if (name.ToUpper().Equals("version.sdk".ToUpper()))
			{
				return Connection.sdk;
			}
			if (name.ToUpper().Equals("version.protocol".ToUpper()))
			{
				return Connection.protocol;
			}
			if (name.ToUpper().Equals("version.security".ToUpper()))
			{
				return Connection.security;
			}
			return null;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000522A File Offset: 0x0000342A
		public virtual void AddUnsolicitedNotificationListener(LdapUnsolicitedNotificationListener listener)
		{
			if (listener != null)
			{
				this.conn.AddUnsolicitedNotificationListener(listener);
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000523B File Offset: 0x0000343B
		public virtual void RemoveUnsolicitedNotificationListener(LdapUnsolicitedNotificationListener listener)
		{
			if (listener != null)
			{
				this.conn.RemoveUnsolicitedNotificationListener(listener);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000524C File Offset: 0x0000344C
		public virtual void startTLS()
		{
			LdapMessage ldapMessage = this.MakeExtendedOperation(new LdapExtendedOperation("1.3.6.1.4.1.1466.20037", null), null);
			int messageID = ldapMessage.MessageID;
			this.conn.acquireWriteSemaphore(messageID);
			try
			{
				if (!this.conn.areMessagesComplete())
				{
					throw new LdapLocalException("OUTSTANDING_OPERATIONS", 1);
				}
				this.conn.stopReaderOnReply(messageID);
				((LdapExtendedResponse)this.SendRequestToServer(ldapMessage, this.defSearchCons.TimeLimit, null, null).getResponse()).chkResultCode();
				this.conn.startTLS();
			}
			finally
			{
				this.conn.startReader();
				this.conn.freeWriteSemaphore(messageID);
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005300 File Offset: 0x00003500
		public virtual void stopTLS()
		{
			if (!this.TLS)
			{
				throw new LdapLocalException("NO_STARTTLS", 1);
			}
			int num = this.conn.acquireWriteSemaphore();
			try
			{
				if (!this.conn.areMessagesComplete())
				{
					throw new LdapLocalException("OUTSTANDING_OPERATIONS", 1);
				}
				this.conn.stopTLS();
			}
			finally
			{
				this.conn.freeWriteSemaphore(num);
				this.Connect(this.Host, this.Port);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005384 File Offset: 0x00003584
		public virtual void Abandon(LdapSearchResults results)
		{
			this.Abandon(results, this.defSearchCons);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005393 File Offset: 0x00003593
		public virtual void Abandon(LdapSearchResults results, LdapConstraints cons)
		{
			results.Abandon();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000539B File Offset: 0x0000359B
		public virtual void Abandon(int id)
		{
			this.Abandon(id, this.defSearchCons);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000053AC File Offset: 0x000035AC
		public virtual void Abandon(int id, LdapConstraints cons)
		{
			try
			{
				this.conn.getMessageAgent(id).Abandon(id, cons);
			}
			catch (FieldAccessException)
			{
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000053E4 File Offset: 0x000035E4
		public virtual void Abandon(LdapMessageQueue queue)
		{
			this.Abandon(queue, this.defSearchCons);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000053F4 File Offset: 0x000035F4
		public virtual void Abandon(LdapMessageQueue queue, LdapConstraints cons)
		{
			if (queue != null)
			{
				MessageAgent messageAgent;
				if (queue is LdapSearchQueue)
				{
					messageAgent = queue.MessageAgent;
				}
				else
				{
					messageAgent = queue.MessageAgent;
				}
				int[] messageIDs = messageAgent.MessageIDs;
				for (int i = 0; i < messageIDs.Length; i++)
				{
					messageAgent.Abandon(messageIDs[i], cons);
				}
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000543B File Offset: 0x0000363B
		public virtual void Add(LdapEntry entry)
		{
			this.Add(entry, this.defSearchCons);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000544C File Offset: 0x0000364C
		public virtual void Add(LdapEntry entry, LdapConstraints cons)
		{
			LdapResponseQueue ldapResponseQueue = this.Add(entry, null, cons);
			LdapResponse ldapResponse = (LdapResponse)ldapResponseQueue.getResponse();
			object obj = this.responseCtlSemaphore;
			lock (obj)
			{
				this.responseCtls = ldapResponse.Controls;
			}
			this.chkResultCode(ldapResponseQueue, cons, ldapResponse);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000054B4 File Offset: 0x000036B4
		public virtual LdapResponseQueue Add(LdapEntry entry, LdapResponseQueue queue)
		{
			return this.Add(entry, queue, this.defSearchCons);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000054C4 File Offset: 0x000036C4
		public virtual LdapResponseQueue Add(LdapEntry entry, LdapResponseQueue queue, LdapConstraints cons)
		{
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			if (entry == null)
			{
				throw new ArgumentException("The LdapEntry parameter cannot be null");
			}
			if (entry.DN == null)
			{
				throw new ArgumentException("The DN value must be present in the LdapEntry object");
			}
			LdapMessage ldapMessage = new LdapAddRequest(entry, cons.getControls());
			return this.SendRequestToServer(ldapMessage, cons.TimeLimit, queue, null);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005519 File Offset: 0x00003719
		public virtual void Bind(string dn, string passwd)
		{
			this.Bind(dn, passwd, AuthenticationTypes.None);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005524 File Offset: 0x00003724
		public virtual void Bind(string dn, string passwd, AuthenticationTypes authenticationTypes)
		{
			this.Bind(3, dn, passwd, this.defSearchCons);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005535 File Offset: 0x00003735
		public virtual void Bind(int version, string dn, string passwd)
		{
			this.Bind(version, dn, passwd, this.defSearchCons);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005546 File Offset: 0x00003746
		public virtual void Bind(string dn, string passwd, LdapConstraints cons)
		{
			this.Bind(3, dn, passwd, cons);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005554 File Offset: 0x00003754
		public virtual void Bind(int version, string dn, string passwd, LdapConstraints cons)
		{
			sbyte[] array = null;
			if (passwd != null)
			{
				try
				{
					array = SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(passwd));
					passwd = null;
				}
				catch (IOException ex)
				{
					passwd = null;
					throw new SystemException(ex.ToString());
				}
			}
			this.Bind(version, dn, array, cons);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000055AC File Offset: 0x000037AC
		[CLSCompliant(false)]
		public virtual void Bind(int version, string dn, sbyte[] passwd)
		{
			this.Bind(version, dn, passwd, this.defSearchCons);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000055C0 File Offset: 0x000037C0
		[CLSCompliant(false)]
		public virtual void Bind(int version, string dn, sbyte[] passwd, LdapConstraints cons)
		{
			LdapResponseQueue ldapResponseQueue = this.Bind(version, dn, passwd, null, cons, null);
			LdapResponse ldapResponse = (LdapResponse)ldapResponseQueue.getResponse();
			if (ldapResponse != null)
			{
				object obj = this.responseCtlSemaphore;
				lock (obj)
				{
					this.responseCtls = ldapResponse.Controls;
				}
				this.chkResultCode(ldapResponseQueue, cons, ldapResponse);
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005630 File Offset: 0x00003830
		[CLSCompliant(false)]
		public virtual LdapResponseQueue Bind(int version, string dn, sbyte[] passwd, LdapResponseQueue queue)
		{
			return this.Bind(version, dn, passwd, queue, this.defSearchCons, null);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005644 File Offset: 0x00003844
		[CLSCompliant(false)]
		public virtual LdapResponseQueue Bind(int version, string dn, sbyte[] passwd, LdapResponseQueue queue, LdapConstraints cons, string mech)
		{
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			if (dn == null)
			{
				dn = "";
			}
			else
			{
				dn = dn.Trim();
			}
			if (passwd == null)
			{
				passwd = new sbyte[0];
			}
			bool flag = false;
			if (passwd.Length == 0)
			{
				flag = true;
				dn = "";
			}
			LdapMessage ldapMessage = new LdapBindRequest(version, dn, passwd, cons.getControls());
			int messageID = ldapMessage.MessageID;
			BindProperties bindProperties = new BindProperties(version, dn, "simple", flag, null, null);
			if (!this.conn.Connected)
			{
				if (this.conn.Host == null)
				{
					throw new LdapException("CONNECTION_IMPOSSIBLE", 91, null);
				}
				this.conn.connect(this.conn.Host, this.conn.Port);
			}
			this.conn.acquireWriteSemaphore(messageID);
			return this.SendRequestToServer(ldapMessage, cons.TimeLimit, queue, bindProperties);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000571B File Offset: 0x0000391B
		public virtual bool Compare(string dn, LdapAttribute attr)
		{
			return this.Compare(dn, attr, this.defSearchCons);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000572C File Offset: 0x0000392C
		public virtual bool Compare(string dn, LdapAttribute attr, LdapConstraints cons)
		{
			bool flag = false;
			LdapResponseQueue ldapResponseQueue = this.Compare(dn, attr, null, cons);
			LdapResponse ldapResponse = (LdapResponse)ldapResponseQueue.getResponse();
			object obj = this.responseCtlSemaphore;
			lock (obj)
			{
				this.responseCtls = ldapResponse.Controls;
			}
			if (ldapResponse.ResultCode == 6)
			{
				flag = true;
			}
			else if (ldapResponse.ResultCode == 5)
			{
				flag = false;
			}
			else
			{
				this.chkResultCode(ldapResponseQueue, cons, ldapResponse);
			}
			return flag;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000057B4 File Offset: 0x000039B4
		public virtual LdapResponseQueue Compare(string dn, LdapAttribute attr, LdapResponseQueue queue)
		{
			return this.Compare(dn, attr, queue, this.defSearchCons);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000057C8 File Offset: 0x000039C8
		public virtual LdapResponseQueue Compare(string dn, LdapAttribute attr, LdapResponseQueue queue, LdapConstraints cons)
		{
			if (attr.size() != 1)
			{
				throw new ArgumentException("compare: Exactly one value must be present in the LdapAttribute");
			}
			if (dn == null)
			{
				throw new ArgumentException("compare: DN cannot be null");
			}
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessage ldapMessage = new LdapCompareRequest(dn, attr.Name, attr.ByteValue, cons.getControls());
			return this.SendRequestToServer(ldapMessage, cons.TimeLimit, queue, null);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005830 File Offset: 0x00003A30
		public virtual void Connect(string host, int port)
		{
			SupportClass.Tokenizer tokenizer = new SupportClass.Tokenizer(host, " ");
			string text = null;
			while (tokenizer.HasMoreTokens())
			{
				try
				{
					int num = port;
					text = tokenizer.NextToken();
					int num2 = text.IndexOf(':');
					if (num2 != -1 && num2 + 1 != text.Length)
					{
						try
						{
							num = int.Parse(text.Substring(num2 + 1));
							text = text.Substring(0, num2);
						}
						catch (Exception)
						{
							throw new ArgumentException("INVALID_ADDRESS");
						}
					}
					this.conn = this.conn.destroyClone(true);
					this.conn.connect(text, num);
					break;
				}
				catch (LdapException ex)
				{
					if (!tokenizer.HasMoreTokens())
					{
						throw ex;
					}
				}
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000058EC File Offset: 0x00003AEC
		public virtual void Delete(string dn)
		{
			this.Delete(dn, this.defSearchCons);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000058FC File Offset: 0x00003AFC
		public virtual void Delete(string dn, LdapConstraints cons)
		{
			LdapResponseQueue ldapResponseQueue = this.Delete(dn, null, cons);
			LdapResponse ldapResponse = (LdapResponse)ldapResponseQueue.getResponse();
			object obj = this.responseCtlSemaphore;
			lock (obj)
			{
				this.responseCtls = ldapResponse.Controls;
			}
			this.chkResultCode(ldapResponseQueue, cons, ldapResponse);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005964 File Offset: 0x00003B64
		public virtual LdapResponseQueue Delete(string dn, LdapResponseQueue queue)
		{
			return this.Delete(dn, queue, this.defSearchCons);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005974 File Offset: 0x00003B74
		public virtual LdapResponseQueue Delete(string dn, LdapResponseQueue queue, LdapConstraints cons)
		{
			if (dn == null)
			{
				throw new ArgumentException("DN_PARAM_ERROR");
			}
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessage ldapMessage = new LdapDeleteRequest(dn, cons.getControls());
			return this.SendRequestToServer(ldapMessage, cons.TimeLimit, queue, null);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000059B6 File Offset: 0x00003BB6
		public virtual void Disconnect()
		{
			this.Disconnect(this.defSearchCons, true);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000059C5 File Offset: 0x00003BC5
		public virtual void Disconnect(LdapConstraints cons)
		{
			this.Disconnect(cons, true);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000059CF File Offset: 0x00003BCF
		private void Disconnect(LdapConstraints cons, bool how)
		{
			this.conn = this.conn.destroyClone(how);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000059E3 File Offset: 0x00003BE3
		public virtual LdapExtendedResponse ExtendedOperation(LdapExtendedOperation op)
		{
			return this.ExtendedOperation(op, this.defSearchCons);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000059F4 File Offset: 0x00003BF4
		public virtual LdapExtendedResponse ExtendedOperation(LdapExtendedOperation op, LdapConstraints cons)
		{
			LdapResponseQueue ldapResponseQueue = this.ExtendedOperation(op, cons, null);
			LdapExtendedResponse ldapExtendedResponse = (LdapExtendedResponse)ldapResponseQueue.getResponse();
			object obj = this.responseCtlSemaphore;
			lock (obj)
			{
				this.responseCtls = ldapExtendedResponse.Controls;
			}
			this.chkResultCode(ldapResponseQueue, cons, ldapExtendedResponse);
			return ldapExtendedResponse;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005A5C File Offset: 0x00003C5C
		public virtual LdapResponseQueue ExtendedOperation(LdapExtendedOperation op, LdapResponseQueue queue)
		{
			return this.ExtendedOperation(op, this.defSearchCons, queue);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005A6C File Offset: 0x00003C6C
		public virtual LdapResponseQueue ExtendedOperation(LdapExtendedOperation op, LdapConstraints cons, LdapResponseQueue queue)
		{
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessage ldapMessage = this.MakeExtendedOperation(op, cons);
			return this.SendRequestToServer(ldapMessage, cons.TimeLimit, queue, null);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005A9C File Offset: 0x00003C9C
		protected internal virtual LdapMessage MakeExtendedOperation(LdapExtendedOperation op, LdapConstraints cons)
		{
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			if (op.getID() == null)
			{
				throw new ArgumentException("OP_PARAM_ERROR");
			}
			return new LdapExtendedRequest(op, cons.getControls());
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005AC8 File Offset: 0x00003CC8
		public virtual void Modify(string dn, LdapModification mod)
		{
			this.Modify(dn, mod, this.defSearchCons);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005AD8 File Offset: 0x00003CD8
		public virtual void Modify(string dn, LdapModification mod, LdapConstraints cons)
		{
			this.Modify(dn, new LdapModification[] { mod }, cons);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005AF9 File Offset: 0x00003CF9
		public virtual void Modify(string dn, LdapModification[] mods)
		{
			this.Modify(dn, mods, this.defSearchCons);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005B0C File Offset: 0x00003D0C
		public virtual void Modify(string dn, LdapModification[] mods, LdapConstraints cons)
		{
			LdapResponseQueue ldapResponseQueue = this.Modify(dn, mods, null, cons);
			LdapResponse ldapResponse = (LdapResponse)ldapResponseQueue.getResponse();
			object obj = this.responseCtlSemaphore;
			lock (obj)
			{
				this.responseCtls = ldapResponse.Controls;
			}
			this.chkResultCode(ldapResponseQueue, cons, ldapResponse);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005B74 File Offset: 0x00003D74
		public virtual LdapResponseQueue Modify(string dn, LdapModification mod, LdapResponseQueue queue)
		{
			return this.Modify(dn, mod, queue, this.defSearchCons);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005B88 File Offset: 0x00003D88
		public virtual LdapResponseQueue Modify(string dn, LdapModification mod, LdapResponseQueue queue, LdapConstraints cons)
		{
			return this.Modify(dn, new LdapModification[] { mod }, queue, cons);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005BAB File Offset: 0x00003DAB
		public virtual LdapResponseQueue Modify(string dn, LdapModification[] mods, LdapResponseQueue queue)
		{
			return this.Modify(dn, mods, queue, this.defSearchCons);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005BBC File Offset: 0x00003DBC
		public virtual LdapResponseQueue Modify(string dn, LdapModification[] mods, LdapResponseQueue queue, LdapConstraints cons)
		{
			if (dn == null)
			{
				throw new ArgumentException("DN_PARAM_ERROR");
			}
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessage ldapMessage = new LdapModifyRequest(dn, mods, cons.getControls());
			return this.SendRequestToServer(ldapMessage, cons.TimeLimit, queue, null);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005C02 File Offset: 0x00003E02
		public virtual LdapEntry Read(string dn)
		{
			return this.Read(dn, this.defSearchCons);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005C11 File Offset: 0x00003E11
		public virtual LdapEntry Read(string dn, LdapSearchConstraints cons)
		{
			return this.Read(dn, null, cons);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005C1C File Offset: 0x00003E1C
		public virtual LdapEntry Read(string dn, string[] attrs)
		{
			return this.Read(dn, attrs, this.defSearchCons);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005C2C File Offset: 0x00003E2C
		public virtual LdapEntry Read(string dn, string[] attrs, LdapSearchConstraints cons)
		{
			LdapSearchResults ldapSearchResults = this.Search(dn, 0, null, attrs, false, cons);
			LdapEntry ldapEntry = null;
			if (ldapSearchResults.hasMore())
			{
				ldapEntry = ldapSearchResults.next();
				if (ldapSearchResults.hasMore())
				{
					throw new LdapLocalException("READ_MULTIPLE", 101);
				}
			}
			return ldapEntry;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005C70 File Offset: 0x00003E70
		public static LdapEntry Read(LdapUrl toGet)
		{
			LdapConnection ldapConnection = new LdapConnection();
			ldapConnection.Connect(toGet.Host, toGet.Port);
			LdapEntry ldapEntry = ldapConnection.Read(toGet.getDN(), toGet.AttributeArray);
			ldapConnection.Disconnect();
			return ldapEntry;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005CB0 File Offset: 0x00003EB0
		public static LdapEntry Read(LdapUrl toGet, LdapSearchConstraints cons)
		{
			LdapConnection ldapConnection = new LdapConnection();
			ldapConnection.Connect(toGet.Host, toGet.Port);
			LdapEntry ldapEntry = ldapConnection.Read(toGet.getDN(), toGet.AttributeArray, cons);
			ldapConnection.Disconnect();
			return ldapEntry;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005CEE File Offset: 0x00003EEE
		public virtual void Rename(string dn, string newRdn, bool deleteOldRdn)
		{
			this.Rename(dn, newRdn, deleteOldRdn, this.defSearchCons);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005CFF File Offset: 0x00003EFF
		public virtual void Rename(string dn, string newRdn, bool deleteOldRdn, LdapConstraints cons)
		{
			this.Rename(dn, newRdn, null, deleteOldRdn, cons);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005D0D File Offset: 0x00003F0D
		public virtual void Rename(string dn, string newRdn, string newParentdn, bool deleteOldRdn)
		{
			this.Rename(dn, newRdn, newParentdn, deleteOldRdn, this.defSearchCons);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005D20 File Offset: 0x00003F20
		public virtual void Rename(string dn, string newRdn, string newParentdn, bool deleteOldRdn, LdapConstraints cons)
		{
			LdapResponseQueue ldapResponseQueue = this.Rename(dn, newRdn, newParentdn, deleteOldRdn, null, cons);
			LdapResponse ldapResponse = (LdapResponse)ldapResponseQueue.getResponse();
			object obj = this.responseCtlSemaphore;
			lock (obj)
			{
				this.responseCtls = ldapResponse.Controls;
			}
			this.chkResultCode(ldapResponseQueue, cons, ldapResponse);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005D8C File Offset: 0x00003F8C
		public virtual LdapResponseQueue Rename(string dn, string newRdn, bool deleteOldRdn, LdapResponseQueue queue)
		{
			return this.Rename(dn, newRdn, deleteOldRdn, queue, this.defSearchCons);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005D9F File Offset: 0x00003F9F
		public virtual LdapResponseQueue Rename(string dn, string newRdn, bool deleteOldRdn, LdapResponseQueue queue, LdapConstraints cons)
		{
			return this.Rename(dn, newRdn, null, deleteOldRdn, queue, cons);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005DAF File Offset: 0x00003FAF
		public virtual LdapResponseQueue Rename(string dn, string newRdn, string newParentdn, bool deleteOldRdn, LdapResponseQueue queue)
		{
			return this.Rename(dn, newRdn, newParentdn, deleteOldRdn, queue, this.defSearchCons);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005DC4 File Offset: 0x00003FC4
		public virtual LdapResponseQueue Rename(string dn, string newRdn, string newParentdn, bool deleteOldRdn, LdapResponseQueue queue, LdapConstraints cons)
		{
			if (dn == null || newRdn == null)
			{
				throw new ArgumentException("RDN_PARAM_ERROR");
			}
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessage ldapMessage = new LdapModifyDNRequest(dn, newRdn, newParentdn, deleteOldRdn, cons.getControls());
			return this.SendRequestToServer(ldapMessage, cons.TimeLimit, queue, null);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005E11 File Offset: 0x00004011
		public virtual LdapSearchResults Search(string base_Renamed, int scope, string filter, string[] attrs, bool typesOnly)
		{
			return this.Search(base_Renamed, scope, filter, attrs, typesOnly, this.defSearchCons);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005E28 File Offset: 0x00004028
		public virtual LdapSearchResults Search(string base_Renamed, int scope, string filter, string[] attrs, bool typesOnly, LdapSearchConstraints cons)
		{
			LdapSearchQueue ldapSearchQueue = this.Search(base_Renamed, scope, filter, attrs, typesOnly, null, cons);
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			return new LdapSearchResults(this, ldapSearchQueue, cons);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005E5B File Offset: 0x0000405B
		public virtual LdapSearchQueue Search(string base_Renamed, int scope, string filter, string[] attrs, bool typesOnly, LdapSearchQueue queue)
		{
			return this.Search(base_Renamed, scope, filter, attrs, typesOnly, queue, this.defSearchCons);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005E74 File Offset: 0x00004074
		public virtual LdapSearchQueue Search(string base_Renamed, int scope, string filter, string[] attrs, bool typesOnly, LdapSearchQueue queue, LdapSearchConstraints cons)
		{
			if (filter == null)
			{
				filter = "objectclass=*";
			}
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessage ldapMessage = new LdapSearchRequest(base_Renamed, scope, filter, attrs, cons.Dereference, cons.MaxResults, cons.ServerTimeLimit, typesOnly, cons.getControls());
			LdapSearchQueue ldapSearchQueue = queue;
			MessageAgent messageAgent;
			if (ldapSearchQueue == null)
			{
				messageAgent = new MessageAgent();
				ldapSearchQueue = new LdapSearchQueue(messageAgent);
			}
			else
			{
				messageAgent = queue.MessageAgent;
			}
			try
			{
				messageAgent.sendMessage(this.conn, ldapMessage, cons.TimeLimit, ldapSearchQueue, null);
			}
			catch (LdapException ex)
			{
				throw ex;
			}
			return ldapSearchQueue;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005F08 File Offset: 0x00004108
		public static LdapSearchResults Search(LdapUrl toGet)
		{
			return LdapConnection.Search(toGet, null);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005F14 File Offset: 0x00004114
		public static LdapSearchResults Search(LdapUrl toGet, LdapSearchConstraints cons)
		{
			LdapConnection ldapConnection = new LdapConnection();
			ldapConnection.Connect(toGet.Host, toGet.Port);
			if (cons == null)
			{
				cons = ldapConnection.SearchConstraints;
			}
			else
			{
				cons = (LdapSearchConstraints)cons.Clone();
			}
			cons.BatchSize = 0;
			LdapSearchResults ldapSearchResults = ldapConnection.Search(toGet.getDN(), toGet.Scope, toGet.Filter, toGet.AttributeArray, false, cons);
			ldapConnection.Disconnect();
			return ldapSearchResults;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005F80 File Offset: 0x00004180
		public virtual LdapMessageQueue SendRequest(LdapMessage request, LdapMessageQueue queue)
		{
			return this.SendRequest(request, queue, null);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005F8C File Offset: 0x0000418C
		public virtual LdapMessageQueue SendRequest(LdapMessage request, LdapMessageQueue queue, LdapConstraints cons)
		{
			if (!request.Request)
			{
				throw new SystemException("Object is not a request message");
			}
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessageQueue ldapMessageQueue = queue;
			MessageAgent messageAgent;
			if (ldapMessageQueue == null)
			{
				messageAgent = new MessageAgent();
				if (request.Type == 3)
				{
					ldapMessageQueue = new LdapSearchQueue(messageAgent);
				}
				else
				{
					ldapMessageQueue = new LdapResponseQueue(messageAgent);
				}
			}
			else if (request.Type == 3)
			{
				messageAgent = queue.MessageAgent;
			}
			else
			{
				messageAgent = queue.MessageAgent;
			}
			try
			{
				messageAgent.sendMessage(this.conn, request, cons.TimeLimit, ldapMessageQueue, null);
			}
			catch (LdapException ex)
			{
				throw ex;
			}
			return ldapMessageQueue;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006020 File Offset: 0x00004220
		private LdapResponseQueue SendRequestToServer(LdapMessage msg, int timeout, LdapResponseQueue queue, BindProperties bindProps)
		{
			MessageAgent messageAgent;
			if (queue == null)
			{
				messageAgent = new MessageAgent();
				queue = new LdapResponseQueue(messageAgent);
			}
			else
			{
				messageAgent = queue.MessageAgent;
			}
			messageAgent.sendMessage(this.conn, msg, timeout, queue, bindProps);
			return queue;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000605C File Offset: 0x0000425C
		private ReferralInfo getReferralConnection(string[] referrals)
		{
			ReferralInfo referralInfo = null;
			Exception ex = null;
			LdapConnection ldapConnection = null;
			LdapReferralHandler referralHandler = this.defSearchCons.getReferralHandler();
			int i = 0;
			if (referralHandler == null || referralHandler is LdapAuthHandler)
			{
				for (i = 0; i < referrals.Length; i++)
				{
					string text = null;
					sbyte[] array = null;
					try
					{
						ldapConnection = new LdapConnection();
						ldapConnection.Constraints = this.defSearchCons;
						LdapUrl ldapUrl = new LdapUrl(referrals[i]);
						ldapConnection.Connect(ldapUrl.Host, ldapUrl.Port);
						if (referralHandler != null && referralHandler is LdapAuthHandler)
						{
							LdapAuthProvider authProvider = ((LdapAuthHandler)referralHandler).getAuthProvider(ldapUrl.Host, ldapUrl.Port);
							text = authProvider.DN;
							array = authProvider.Password;
						}
						ldapConnection.Bind(3, text, array);
						ex = null;
						referralInfo = new ReferralInfo(ldapConnection, referrals, ldapUrl);
						ldapConnection.Connection.ActiveReferral = referralInfo;
						break;
					}
					catch (Exception ex2)
					{
						if (ldapConnection != null)
						{
							try
							{
								ldapConnection.Disconnect();
								ldapConnection = null;
								ex = ex2;
							}
							catch (LdapException)
							{
							}
						}
					}
				}
			}
			else
			{
				try
				{
					ldapConnection = ((LdapBindHandler)referralHandler).Bind(referrals, this);
					if (ldapConnection == null)
					{
						LdapReferralException ex3 = new LdapReferralException("REFERRAL_ERROR");
						ex3.setReferrals(referrals);
						throw ex3;
					}
					for (int j = 0; j < referrals.Length; j++)
					{
						try
						{
							LdapUrl ldapUrl2 = new LdapUrl(referrals[j]);
							if (ldapUrl2.Host.ToUpper().Equals(ldapConnection.Host.ToUpper()) && ldapUrl2.Port == ldapConnection.Port)
							{
								referralInfo = new ReferralInfo(ldapConnection, referrals, ldapUrl2);
								break;
							}
						}
						catch (Exception)
						{
						}
					}
					if (referralInfo == null)
					{
						ex = new LdapLocalException("REFERRAL_BIND_MATCH", 91);
					}
				}
				catch (Exception ex4)
				{
					ldapConnection = null;
					ex = ex4;
				}
			}
			if (ex == null)
			{
				return referralInfo;
			}
			if (ex is LdapReferralException)
			{
				throw (LdapReferralException)ex;
			}
			LdapException ex5;
			if (ex is LdapException)
			{
				ex5 = (LdapException)ex;
			}
			else
			{
				ex5 = new LdapLocalException("SERVER_CONNECT_ERROR", new object[] { this.conn.Host }, 91, ex);
			}
			LdapReferralException ex6 = new LdapReferralException("REFERRAL_ERROR", ex5);
			ex6.setReferrals(referrals);
			ex6.FailedReferral = referrals[referrals.Length - 1];
			throw ex6;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006288 File Offset: 0x00004488
		private void chkResultCode(LdapMessageQueue queue, LdapConstraints cons, LdapResponse response)
		{
			if (response.ResultCode == 10 && cons.ReferralFollowing)
			{
				ArrayList arrayList = null;
				try
				{
					this.chaseReferral(queue, cons, response, response.Referrals, 0, false, null);
					return;
				}
				finally
				{
					this.releaseReferralConnections(arrayList);
				}
			}
			response.chkResultCode();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000062DC File Offset: 0x000044DC
		internal virtual ArrayList chaseReferral(LdapMessageQueue queue, LdapConstraints cons, LdapMessage msg, string[] initialReferrals, int hopCount, bool searchReference, ArrayList connectionList)
		{
			ArrayList arrayList = connectionList;
			LdapConnection ldapConnection = null;
			ReferralInfo referralInfo = null;
			if (arrayList == null)
			{
				arrayList = new ArrayList(cons.HopLimit);
			}
			string[] array;
			LdapMessage ldapMessage;
			if (initialReferrals != null)
			{
				array = initialReferrals;
				ldapMessage = msg.RequestingMessage;
			}
			else
			{
				LdapResponse ldapResponse = (LdapResponse)queue.getResponse();
				if (ldapResponse.ResultCode != 10)
				{
					ldapResponse.chkResultCode();
					return arrayList;
				}
				array = ldapResponse.Referrals;
				ldapMessage = ldapResponse.RequestingMessage;
			}
			try
			{
				if (hopCount++ > cons.HopLimit)
				{
					throw new LdapLocalException("Max hops exceeded", 97);
				}
				referralInfo = this.getReferralConnection(array);
				ldapConnection = referralInfo.ReferralConnection;
				LdapUrl referralUrl = referralInfo.ReferralUrl;
				arrayList.Add(ldapConnection);
				LdapMessage ldapMessage2 = this.rebuildRequest(ldapMessage, referralUrl, searchReference);
				try
				{
					MessageAgent messageAgent;
					if (queue is LdapResponseQueue)
					{
						messageAgent = queue.MessageAgent;
					}
					else
					{
						messageAgent = queue.MessageAgent;
					}
					messageAgent.sendMessage(ldapConnection.Connection, ldapMessage2, this.defSearchCons.TimeLimit, queue, null);
				}
				catch (InterThreadException ex)
				{
					LdapReferralException ex2 = new LdapReferralException("REFERRAL_SEND", 91, null, ex);
					ex2.setReferrals(initialReferrals);
					ReferralInfo activeReferral = ldapConnection.Connection.ActiveReferral;
					ex2.FailedReferral = activeReferral.ReferralUrl.ToString();
					throw ex2;
				}
				if (initialReferrals != null)
				{
					return arrayList;
				}
				arrayList = this.chaseReferral(queue, cons, null, null, hopCount, false, arrayList);
			}
			catch (Exception ex3)
			{
				if (ex3 is LdapReferralException)
				{
					throw (LdapReferralException)ex3;
				}
				LdapReferralException ex4 = new LdapReferralException("REFERRAL_ERROR", ex3);
				ex4.setReferrals(array);
				if (referralInfo != null)
				{
					ex4.FailedReferral = referralInfo.ReferralUrl.ToString();
				}
				else
				{
					ex4.FailedReferral = array[array.Length - 1];
				}
				throw ex4;
			}
			return arrayList;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006490 File Offset: 0x00004690
		private LdapMessage rebuildRequest(LdapMessage msg, LdapUrl url, bool reference)
		{
			string dn = url.getDN();
			string text = null;
			int type = msg.Type;
			switch (type)
			{
			case 0:
			case 6:
			case 8:
			case 10:
			case 12:
			case 14:
				goto IL_008E;
			case 1:
			case 2:
			case 4:
			case 5:
			case 7:
			case 9:
			case 11:
			case 13:
			case 15:
			case 16:
				break;
			case 3:
				if (reference)
				{
					text = url.Filter;
					goto IL_008E;
				}
				goto IL_008E;
			default:
				if (type == 23)
				{
					goto IL_008E;
				}
				break;
			}
			throw new LdapLocalException("IMPROPER_REFERRAL", new object[] { msg.Type }, 82);
			IL_008E:
			return msg.Clone(dn, text, reference);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006534 File Offset: 0x00004734
		internal virtual void releaseReferralConnections(ArrayList list)
		{
			if (list == null)
			{
				return;
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				try
				{
					LdapConnection ldapConnection = (LdapConnection)list[i];
					list.RemoveAt(i);
					ldapConnection.Disconnect();
				}
				catch (IndexOutOfRangeException)
				{
				}
				catch (LdapException)
				{
				}
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006598 File Offset: 0x00004798
		public virtual LdapSchema FetchSchema(string schemaDN)
		{
			return new LdapSchema(this.Read(schemaDN, LdapSchema.schemaTypeNames));
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000065AB File Offset: 0x000047AB
		public virtual string GetSchemaDN()
		{
			return this.GetSchemaDN("");
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000065B8 File Offset: 0x000047B8
		public virtual string GetSchemaDN(string dn)
		{
			string[] array = new string[] { "subschemaSubentry" };
			string[] stringValueArray = this.Read(dn, array).getAttribute(array[0]).StringValueArray;
			if (stringValueArray == null || stringValueArray.Length < 1)
			{
				throw new LdapLocalException("NO_SCHEMA", new object[] { dn }, 94);
			}
			if (stringValueArray.Length > 1)
			{
				throw new LdapLocalException("MULTIPLE_SCHEMA", new object[] { dn }, 19);
			}
			return stringValueArray[0];
		}

		// Token: 0x04000074 RID: 116
		private LdapSearchConstraints defSearchCons;

		// Token: 0x04000075 RID: 117
		private LdapControl[] responseCtls;

		// Token: 0x04000076 RID: 118
		private object responseCtlSemaphore;

		// Token: 0x04000077 RID: 119
		private Connection conn;

		// Token: 0x04000078 RID: 120
		private static object nameLock;

		// Token: 0x04000079 RID: 121
		private static int lConnNum;

		// Token: 0x0400007A RID: 122
		private string name;

		// Token: 0x0400007B RID: 123
		public const int SCOPE_BASE = 0;

		// Token: 0x0400007C RID: 124
		public const int SCOPE_ONE = 1;

		// Token: 0x0400007D RID: 125
		public const int SCOPE_SUB = 2;

		// Token: 0x0400007E RID: 126
		public const string NO_ATTRS = "1.1";

		// Token: 0x0400007F RID: 127
		public const string ALL_USER_ATTRS = "*";

		// Token: 0x04000080 RID: 128
		public const int Ldap_V3 = 3;

		// Token: 0x04000081 RID: 129
		public const int DEFAULT_PORT = 389;

		// Token: 0x04000082 RID: 130
		public const int DEFAULT_SSL_PORT = 636;

		// Token: 0x04000083 RID: 131
		public const string Ldap_PROPERTY_SDK = "version.sdk";

		// Token: 0x04000084 RID: 132
		public const string Ldap_PROPERTY_PROTOCOL = "version.protocol";

		// Token: 0x04000085 RID: 133
		public const string Ldap_PROPERTY_SECURITY = "version.security";

		// Token: 0x04000086 RID: 134
		public const string SERVER_SHUTDOWN_OID = "1.3.6.1.4.1.1466.20036";

		// Token: 0x04000087 RID: 135
		private const string START_TLS_OID = "1.3.6.1.4.1.1466.20037";
	}
}
