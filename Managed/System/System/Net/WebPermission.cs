using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace System.Net
{
	/// <summary>Controls rights to access HTTP Internet resources.</summary>
	// Token: 0x0200046F RID: 1135
	[Serializable]
	public sealed class WebPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x000823F8 File Offset: 0x000805F8
		internal static Regex MatchAllRegex
		{
			get
			{
				if (WebPermission.s_MatchAllRegex == null)
				{
					WebPermission.s_MatchAllRegex = new Regex(".*");
				}
				return WebPermission.s_MatchAllRegex;
			}
		}

		/// <summary>This property returns an enumeration of a single connect permissions held by this <see cref="T:System.Net.WebPermission" />. The possible objects types contained in the returned enumeration are <see cref="T:System.String" /> and <see cref="T:System.Text.RegularExpressions.Regex" />.</summary>
		/// <returns>The <see cref="T:System.Collections.IEnumerator" /> interface that contains connect permissions.</returns>
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002181 RID: 8577 RVA: 0x0008241C File Offset: 0x0008061C
		public IEnumerator ConnectList
		{
			get
			{
				if (this.m_UnrestrictedConnect)
				{
					return new Regex[] { WebPermission.MatchAllRegex }.GetEnumerator();
				}
				ArrayList arrayList = new ArrayList(this.m_connectList.Count);
				for (int i = 0; i < this.m_connectList.Count; i++)
				{
					arrayList.Add((this.m_connectList[i] is DelayedRegex) ? ((DelayedRegex)this.m_connectList[i]).AsRegex : ((this.m_connectList[i] is Uri) ? ((Uri)this.m_connectList[i]).GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped) : this.m_connectList[i]));
				}
				return arrayList.GetEnumerator();
			}
		}

		/// <summary>This property returns an enumeration of a single accept permissions held by this <see cref="T:System.Net.WebPermission" />. The possible objects types contained in the returned enumeration are <see cref="T:System.String" /> and <see cref="T:System.Text.RegularExpressions.Regex" />.</summary>
		/// <returns>The <see cref="T:System.Collections.IEnumerator" /> interface that contains accept permissions.</returns>
		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x000824E0 File Offset: 0x000806E0
		public IEnumerator AcceptList
		{
			get
			{
				if (this.m_UnrestrictedAccept)
				{
					return new Regex[] { WebPermission.MatchAllRegex }.GetEnumerator();
				}
				ArrayList arrayList = new ArrayList(this.m_acceptList.Count);
				for (int i = 0; i < this.m_acceptList.Count; i++)
				{
					arrayList.Add((this.m_acceptList[i] is DelayedRegex) ? ((DelayedRegex)this.m_acceptList[i]).AsRegex : ((this.m_acceptList[i] is Uri) ? ((Uri)this.m_acceptList[i]).GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped) : this.m_acceptList[i]));
				}
				return arrayList.GetEnumerator();
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.WebPermission" /> class that passes all demands or fails all demands.</summary>
		/// <param name="state">A <see cref="T:System.Security.Permissions.PermissionState" /> value. </param>
		// Token: 0x06002183 RID: 8579 RVA: 0x000825A2 File Offset: 0x000807A2
		public WebPermission(PermissionState state)
		{
			this.m_noRestriction = state == PermissionState.Unrestricted;
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x000825CA File Offset: 0x000807CA
		internal WebPermission(bool unrestricted)
		{
			this.m_noRestriction = unrestricted;
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.WebPermission" /> class.</summary>
		// Token: 0x06002185 RID: 8581 RVA: 0x000825EF File Offset: 0x000807EF
		public WebPermission()
		{
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x0008260D File Offset: 0x0008080D
		internal WebPermission(NetworkAccess access)
		{
			this.m_UnrestrictedConnect = (access & NetworkAccess.Connect) > (NetworkAccess)0;
			this.m_UnrestrictedAccept = (access & NetworkAccess.Accept) > (NetworkAccess)0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebPermission" /> class with the specified access rights for the specified URI regular expression.</summary>
		/// <param name="access">A <see cref="T:System.Net.NetworkAccess" /> value that indicates what kind of access to grant to the specified URI. <see cref="F:System.Net.NetworkAccess.Accept" /> indicates that the application is allowed to accept connections from the Internet on a local resource. <see cref="F:System.Net.NetworkAccess.Connect" /> indicates that the application is allowed to connect to specific Internet resources. </param>
		/// <param name="uriRegex">A regular expression that describes the URI to which access is to be granted. </param>
		// Token: 0x06002187 RID: 8583 RVA: 0x00082648 File Offset: 0x00080848
		public WebPermission(NetworkAccess access, Regex uriRegex)
		{
			this.AddPermission(access, uriRegex);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebPermission" /> class with the specified access rights for the specified URI.</summary>
		/// <param name="access">A NetworkAccess value that indicates what kind of access to grant to the specified URI. <see cref="F:System.Net.NetworkAccess.Accept" /> indicates that the application is allowed to accept connections from the Internet on a local resource. <see cref="F:System.Net.NetworkAccess.Connect" /> indicates that the application is allowed to connect to specific Internet resources. </param>
		/// <param name="uriString">A URI string to which access rights are granted. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriString" /> is null. </exception>
		// Token: 0x06002188 RID: 8584 RVA: 0x0008266E File Offset: 0x0008086E
		public WebPermission(NetworkAccess access, string uriString)
		{
			this.AddPermission(access, uriString);
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x00082694 File Offset: 0x00080894
		internal WebPermission(NetworkAccess access, Uri uri)
		{
			this.AddPermission(access, uri);
		}

		/// <summary>Adds the specified URI string with the specified access rights to the current <see cref="T:System.Net.WebPermission" />.</summary>
		/// <param name="access">A <see cref="T:System.Net.NetworkAccess" /> that specifies the access rights that are granted to the URI. </param>
		/// <param name="uriString">A string that describes the URI to which access rights are granted. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriString" /> is null. </exception>
		// Token: 0x0600218A RID: 8586 RVA: 0x000826BC File Offset: 0x000808BC
		public void AddPermission(NetworkAccess access, string uriString)
		{
			if (uriString == null)
			{
				throw new ArgumentNullException("uriString");
			}
			if (this.m_noRestriction)
			{
				return;
			}
			Uri uri;
			if (Uri.TryCreate(uriString, UriKind.Absolute, out uri))
			{
				this.AddPermission(access, uri);
				return;
			}
			ArrayList arrayList = new ArrayList();
			if ((access & NetworkAccess.Connect) != (NetworkAccess)0 && !this.m_UnrestrictedConnect)
			{
				arrayList.Add(this.m_connectList);
			}
			if ((access & NetworkAccess.Accept) != (NetworkAccess)0 && !this.m_UnrestrictedAccept)
			{
				arrayList.Add(this.m_acceptList);
			}
			foreach (object obj in arrayList)
			{
				ArrayList arrayList2 = (ArrayList)obj;
				bool flag = false;
				foreach (object obj2 in arrayList2)
				{
					string text = obj2 as string;
					if (text != null && string.Compare(text, uriString, StringComparison.OrdinalIgnoreCase) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					arrayList2.Add(uriString);
				}
			}
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x000827E0 File Offset: 0x000809E0
		internal void AddPermission(NetworkAccess access, Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			if (this.m_noRestriction)
			{
				return;
			}
			ArrayList arrayList = new ArrayList();
			if ((access & NetworkAccess.Connect) != (NetworkAccess)0 && !this.m_UnrestrictedConnect)
			{
				arrayList.Add(this.m_connectList);
			}
			if ((access & NetworkAccess.Accept) != (NetworkAccess)0 && !this.m_UnrestrictedAccept)
			{
				arrayList.Add(this.m_acceptList);
			}
			foreach (object obj in arrayList)
			{
				ArrayList arrayList2 = (ArrayList)obj;
				bool flag = false;
				foreach (object obj2 in arrayList2)
				{
					if (obj2 is Uri && uri.Equals(obj2))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					arrayList2.Add(uri);
				}
			}
		}

		/// <summary>Adds the specified URI with the specified access rights to the current <see cref="T:System.Net.WebPermission" />.</summary>
		/// <param name="access">A NetworkAccess that specifies the access rights that are granted to the URI. </param>
		/// <param name="uriRegex">A regular expression that describes the set of URIs to which access rights are granted. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="uriRegex" /> parameter is null. </exception>
		// Token: 0x0600218C RID: 8588 RVA: 0x000828F0 File Offset: 0x00080AF0
		public void AddPermission(NetworkAccess access, Regex uriRegex)
		{
			if (uriRegex == null)
			{
				throw new ArgumentNullException("uriRegex");
			}
			if (this.m_noRestriction)
			{
				return;
			}
			if (uriRegex.ToString() == ".*")
			{
				if (!this.m_UnrestrictedConnect && (access & NetworkAccess.Connect) != (NetworkAccess)0)
				{
					this.m_UnrestrictedConnect = true;
					this.m_connectList.Clear();
				}
				if (!this.m_UnrestrictedAccept && (access & NetworkAccess.Accept) != (NetworkAccess)0)
				{
					this.m_UnrestrictedAccept = true;
					this.m_acceptList.Clear();
				}
				return;
			}
			this.AddAsPattern(access, new DelayedRegex(uriRegex));
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x00082978 File Offset: 0x00080B78
		internal void AddAsPattern(NetworkAccess access, DelayedRegex uriRegexPattern)
		{
			ArrayList arrayList = new ArrayList();
			if ((access & NetworkAccess.Connect) != (NetworkAccess)0 && !this.m_UnrestrictedConnect)
			{
				arrayList.Add(this.m_connectList);
			}
			if ((access & NetworkAccess.Accept) != (NetworkAccess)0 && !this.m_UnrestrictedAccept)
			{
				arrayList.Add(this.m_acceptList);
			}
			foreach (object obj in arrayList)
			{
				ArrayList arrayList2 = (ArrayList)obj;
				bool flag = false;
				foreach (object obj2 in arrayList2)
				{
					if (obj2 is DelayedRegex && string.Compare(uriRegexPattern.ToString(), obj2.ToString(), StringComparison.OrdinalIgnoreCase) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					arrayList2.Add(uriRegexPattern);
				}
			}
		}

		/// <summary>Checks the overall permission state of the <see cref="T:System.Net.WebPermission" />.</summary>
		/// <returns>true if the <see cref="T:System.Net.WebPermission" /> was created with the <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" /><see cref="T:System.Security.Permissions.PermissionState" />; otherwise, false.</returns>
		// Token: 0x0600218E RID: 8590 RVA: 0x00082A78 File Offset: 0x00080C78
		public bool IsUnrestricted()
		{
			return this.m_noRestriction;
		}

		/// <summary>Creates a copy of a <see cref="T:System.Net.WebPermission" />.</summary>
		/// <returns>A new instance of the <see cref="T:System.Net.WebPermission" /> class that has the same values as the original. </returns>
		// Token: 0x0600218F RID: 8591 RVA: 0x00082A80 File Offset: 0x00080C80
		public override IPermission Copy()
		{
			if (this.m_noRestriction)
			{
				return new WebPermission(true);
			}
			return new WebPermission((this.m_UnrestrictedConnect ? NetworkAccess.Connect : ((NetworkAccess)0)) | (this.m_UnrestrictedAccept ? NetworkAccess.Accept : ((NetworkAccess)0)))
			{
				m_acceptList = (ArrayList)this.m_acceptList.Clone(),
				m_connectList = (ArrayList)this.m_connectList.Clone()
			};
		}

		/// <summary>Determines whether the current <see cref="T:System.Net.WebPermission" /> is a subset of the specified object.</summary>
		/// <returns>true if the current instance is a subset of the <paramref name="target" /> parameter; otherwise, false. If the target is null, the method returns true for an empty current permission that is not unrestricted and false otherwise.</returns>
		/// <param name="target">The <see cref="T:System.Net.WebPermission" /> to compare to the current <see cref="T:System.Net.WebPermission" />. </param>
		/// <exception cref="T:System.ArgumentException">The target parameter is not an instance of <see cref="T:System.Net.WebPermission" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The current instance contains a Regex-encoded right and there is not exactly the same right found in the target instance. </exception>
		// Token: 0x06002190 RID: 8592 RVA: 0x00082AEC File Offset: 0x00080CEC
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.m_noRestriction && !this.m_UnrestrictedConnect && !this.m_UnrestrictedAccept && this.m_connectList.Count == 0 && this.m_acceptList.Count == 0;
			}
			WebPermission webPermission = target as WebPermission;
			if (webPermission == null)
			{
				throw new ArgumentException(global::SR.GetString("Cannot cast target permission type."), "target");
			}
			if (webPermission.m_noRestriction)
			{
				return true;
			}
			if (this.m_noRestriction)
			{
				return false;
			}
			if (!webPermission.m_UnrestrictedAccept)
			{
				if (this.m_UnrestrictedAccept)
				{
					return false;
				}
				if (this.m_acceptList.Count != 0)
				{
					if (webPermission.m_acceptList.Count == 0)
					{
						return false;
					}
					foreach (object obj in this.m_acceptList)
					{
						if (obj is DelayedRegex)
						{
							if (!WebPermission.isSpecialSubsetCase(obj.ToString(), webPermission.m_acceptList))
							{
								throw new NotSupportedException(global::SR.GetString("Cannot subset Regex. Only support if both patterns are identical."));
							}
						}
						else if (!WebPermission.isMatchedURI(obj, webPermission.m_acceptList))
						{
							return false;
						}
					}
				}
			}
			if (!webPermission.m_UnrestrictedConnect)
			{
				if (this.m_UnrestrictedConnect)
				{
					return false;
				}
				if (this.m_connectList.Count != 0)
				{
					if (webPermission.m_connectList.Count == 0)
					{
						return false;
					}
					foreach (object obj2 in this.m_connectList)
					{
						if (obj2 is DelayedRegex)
						{
							if (!WebPermission.isSpecialSubsetCase(obj2.ToString(), webPermission.m_connectList))
							{
								throw new NotSupportedException(global::SR.GetString("Cannot subset Regex. Only support if both patterns are identical."));
							}
						}
						else if (!WebPermission.isMatchedURI(obj2, webPermission.m_connectList))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x00082CD4 File Offset: 0x00080ED4
		private static bool isSpecialSubsetCase(string regexToCheck, ArrayList permList)
		{
			foreach (object obj in permList)
			{
				DelayedRegex delayedRegex = obj as DelayedRegex;
				Uri uri;
				if (delayedRegex != null)
				{
					if (string.Compare(regexToCheck, delayedRegex.ToString(), StringComparison.OrdinalIgnoreCase) == 0)
					{
						return true;
					}
				}
				else if ((uri = obj as Uri) != null)
				{
					if (string.Compare(regexToCheck, Regex.Escape(uri.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped)), StringComparison.OrdinalIgnoreCase) == 0)
					{
						return true;
					}
				}
				else if (string.Compare(regexToCheck, Regex.Escape(obj.ToString()), StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Returns the logical union between two instances of the <see cref="T:System.Net.WebPermission" /> class.</summary>
		/// <returns>A <see cref="T:System.Net.WebPermission" /> that represents the union of the current instance and the <paramref name="target" /> parameter. If either WebPermission is <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" />, the method returns a <see cref="T:System.Net.WebPermission" /> that is <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" />. If the target is null, the method returns a copy of the current <see cref="T:System.Net.WebPermission" />.</returns>
		/// <param name="target">The <see cref="T:System.Net.WebPermission" /> to combine with the current <see cref="T:System.Net.WebPermission" />. </param>
		/// <exception cref="T:System.ArgumentException">target is not null or of type <see cref="T:System.Net.WebPermission" />. </exception>
		// Token: 0x06002192 RID: 8594 RVA: 0x00082D88 File Offset: 0x00080F88
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			WebPermission webPermission = target as WebPermission;
			if (webPermission == null)
			{
				throw new ArgumentException(global::SR.GetString("Cannot cast target permission type."), "target");
			}
			if (this.m_noRestriction || webPermission.m_noRestriction)
			{
				return new WebPermission(true);
			}
			WebPermission webPermission2 = new WebPermission();
			if (this.m_UnrestrictedConnect || webPermission.m_UnrestrictedConnect)
			{
				webPermission2.m_UnrestrictedConnect = true;
			}
			else
			{
				webPermission2.m_connectList = (ArrayList)webPermission.m_connectList.Clone();
				for (int i = 0; i < this.m_connectList.Count; i++)
				{
					DelayedRegex delayedRegex = this.m_connectList[i] as DelayedRegex;
					if (delayedRegex == null)
					{
						if (this.m_connectList[i] is string)
						{
							webPermission2.AddPermission(NetworkAccess.Connect, (string)this.m_connectList[i]);
						}
						else
						{
							webPermission2.AddPermission(NetworkAccess.Connect, (Uri)this.m_connectList[i]);
						}
					}
					else
					{
						webPermission2.AddAsPattern(NetworkAccess.Connect, delayedRegex);
					}
				}
			}
			if (this.m_UnrestrictedAccept || webPermission.m_UnrestrictedAccept)
			{
				webPermission2.m_UnrestrictedAccept = true;
			}
			else
			{
				webPermission2.m_acceptList = (ArrayList)webPermission.m_acceptList.Clone();
				for (int j = 0; j < this.m_acceptList.Count; j++)
				{
					DelayedRegex delayedRegex2 = this.m_acceptList[j] as DelayedRegex;
					if (delayedRegex2 == null)
					{
						if (this.m_acceptList[j] is string)
						{
							webPermission2.AddPermission(NetworkAccess.Accept, (string)this.m_acceptList[j]);
						}
						else
						{
							webPermission2.AddPermission(NetworkAccess.Accept, (Uri)this.m_acceptList[j]);
						}
					}
					else
					{
						webPermission2.AddAsPattern(NetworkAccess.Accept, delayedRegex2);
					}
				}
			}
			return webPermission2;
		}

		/// <summary>Returns the logical intersection of two <see cref="T:System.Net.WebPermission" /> instances.</summary>
		/// <returns>A new <see cref="T:System.Net.WebPermission" /> that represents the intersection of the current instance and the <paramref name="target" /> parameter. If the intersection is empty, the method returns null.</returns>
		/// <param name="target">The <see cref="T:System.Net.WebPermission" /> to compare with the current instance. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not null or of type <see cref="T:System.Net.WebPermission" /></exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002193 RID: 8595 RVA: 0x00082F54 File Offset: 0x00081154
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			WebPermission webPermission = target as WebPermission;
			if (webPermission == null)
			{
				throw new ArgumentException(global::SR.GetString("Cannot cast target permission type."), "target");
			}
			if (this.m_noRestriction)
			{
				return webPermission.Copy();
			}
			if (webPermission.m_noRestriction)
			{
				return this.Copy();
			}
			WebPermission webPermission2 = new WebPermission();
			if (this.m_UnrestrictedConnect && webPermission.m_UnrestrictedConnect)
			{
				webPermission2.m_UnrestrictedConnect = true;
			}
			else if (this.m_UnrestrictedConnect || webPermission.m_UnrestrictedConnect)
			{
				webPermission2.m_connectList = (ArrayList)(this.m_UnrestrictedConnect ? webPermission : this).m_connectList.Clone();
			}
			else
			{
				WebPermission.intersectList(this.m_connectList, webPermission.m_connectList, webPermission2.m_connectList);
			}
			if (this.m_UnrestrictedAccept && webPermission.m_UnrestrictedAccept)
			{
				webPermission2.m_UnrestrictedAccept = true;
			}
			else if (this.m_UnrestrictedAccept || webPermission.m_UnrestrictedAccept)
			{
				webPermission2.m_acceptList = (ArrayList)(this.m_UnrestrictedAccept ? webPermission : this).m_acceptList.Clone();
			}
			else
			{
				WebPermission.intersectList(this.m_acceptList, webPermission.m_acceptList, webPermission2.m_acceptList);
			}
			if (!webPermission2.m_UnrestrictedConnect && !webPermission2.m_UnrestrictedAccept && webPermission2.m_connectList.Count == 0 && webPermission2.m_acceptList.Count == 0)
			{
				return null;
			}
			return webPermission2;
		}

		/// <summary>Reconstructs a <see cref="T:System.Net.WebPermission" /> from an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding from which to reconstruct the <see cref="T:System.Net.WebPermission" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="securityElement" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a permission element for this type. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002194 RID: 8596 RVA: 0x0008309C File Offset: 0x0008129C
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			if (!securityElement.Tag.Equals("IPermission"))
			{
				throw new ArgumentException(global::SR.GetString("Specified value does not contain 'IPermission' as its tag."), "securityElement");
			}
			string text = securityElement.Attribute("class");
			if (text == null)
			{
				throw new ArgumentException(global::SR.GetString("Specified value does not contain a 'class' attribute."), "securityElement");
			}
			if (text.IndexOf(base.GetType().FullName) < 0)
			{
				throw new ArgumentException(global::SR.GetString("The value class attribute is not valid."), "securityElement");
			}
			string text2 = securityElement.Attribute("Unrestricted");
			this.m_connectList = new ArrayList();
			this.m_acceptList = new ArrayList();
			this.m_UnrestrictedAccept = (this.m_UnrestrictedConnect = false);
			if (text2 != null && string.Compare(text2, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_noRestriction = true;
				return;
			}
			this.m_noRestriction = false;
			SecurityElement securityElement2 = securityElement.SearchForChildByTag("ConnectAccess");
			if (securityElement2 != null)
			{
				foreach (object obj in securityElement2.Children)
				{
					SecurityElement securityElement3 = (SecurityElement)obj;
					if (securityElement3.Tag.Equals("URI"))
					{
						string text3;
						try
						{
							text3 = securityElement3.Attribute("uri");
						}
						catch
						{
							text3 = null;
						}
						if (text3 == null)
						{
							throw new ArgumentException(global::SR.GetString("The '{0}' element contains one or more invalid values."), "ConnectAccess");
						}
						if (text3 == ".*")
						{
							this.m_UnrestrictedConnect = true;
							this.m_connectList = new ArrayList();
							break;
						}
						this.AddAsPattern(NetworkAccess.Connect, new DelayedRegex(text3));
					}
				}
			}
			securityElement2 = securityElement.SearchForChildByTag("AcceptAccess");
			if (securityElement2 != null)
			{
				foreach (object obj2 in securityElement2.Children)
				{
					SecurityElement securityElement4 = (SecurityElement)obj2;
					if (securityElement4.Tag.Equals("URI"))
					{
						string text3;
						try
						{
							text3 = securityElement4.Attribute("uri");
						}
						catch
						{
							text3 = null;
						}
						if (text3 == null)
						{
							throw new ArgumentException(global::SR.GetString("The '{0}' element contains one or more invalid values."), "AcceptAccess");
						}
						if (text3 == ".*")
						{
							this.m_UnrestrictedAccept = true;
							this.m_acceptList = new ArrayList();
							break;
						}
						this.AddAsPattern(NetworkAccess.Accept, new DelayedRegex(text3));
					}
				}
			}
		}

		/// <summary>Creates an XML encoding of a <see cref="T:System.Net.WebPermission" /> and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains an XML-encoded representation of the <see cref="T:System.Net.WebPermission" />, including state information.</returns>
		// Token: 0x06002195 RID: 8597 RVA: 0x00083334 File Offset: 0x00081534
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().FullName + ", " + base.GetType().Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (!this.IsUnrestricted())
			{
				if (this.m_UnrestrictedConnect || this.m_connectList.Count > 0)
				{
					SecurityElement securityElement2 = new SecurityElement("ConnectAccess");
					if (this.m_UnrestrictedConnect)
					{
						SecurityElement securityElement3 = new SecurityElement("URI");
						securityElement3.AddAttribute("uri", SecurityElement.Escape(".*"));
						securityElement2.AddChild(securityElement3);
					}
					else
					{
						foreach (object obj in this.m_connectList)
						{
							Uri uri = obj as Uri;
							string text;
							if (uri != null)
							{
								text = Regex.Escape(uri.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped));
							}
							else
							{
								text = obj.ToString();
							}
							if (obj is string)
							{
								text = Regex.Escape(text);
							}
							SecurityElement securityElement4 = new SecurityElement("URI");
							securityElement4.AddAttribute("uri", SecurityElement.Escape(text));
							securityElement2.AddChild(securityElement4);
						}
					}
					securityElement.AddChild(securityElement2);
				}
				if (this.m_UnrestrictedAccept || this.m_acceptList.Count > 0)
				{
					SecurityElement securityElement5 = new SecurityElement("AcceptAccess");
					if (this.m_UnrestrictedAccept)
					{
						SecurityElement securityElement6 = new SecurityElement("URI");
						securityElement6.AddAttribute("uri", SecurityElement.Escape(".*"));
						securityElement5.AddChild(securityElement6);
					}
					else
					{
						foreach (object obj2 in this.m_acceptList)
						{
							Uri uri2 = obj2 as Uri;
							string text;
							if (uri2 != null)
							{
								text = Regex.Escape(uri2.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped));
							}
							else
							{
								text = obj2.ToString();
							}
							if (obj2 is string)
							{
								text = Regex.Escape(text);
							}
							SecurityElement securityElement7 = new SecurityElement("URI");
							securityElement7.AddAttribute("uri", SecurityElement.Escape(text));
							securityElement5.AddChild(securityElement7);
						}
					}
					securityElement.AddChild(securityElement5);
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x000835D4 File Offset: 0x000817D4
		private static bool isMatchedURI(object uriToCheck, ArrayList uriPatternList)
		{
			string text = uriToCheck as string;
			foreach (object obj in uriPatternList)
			{
				DelayedRegex delayedRegex = obj as DelayedRegex;
				if (delayedRegex == null)
				{
					if (uriToCheck.GetType() == obj.GetType())
					{
						if (text != null && string.Compare(text, (string)obj, StringComparison.OrdinalIgnoreCase) == 0)
						{
							return true;
						}
						if (text == null && uriToCheck.Equals(obj))
						{
							return true;
						}
					}
				}
				else
				{
					string text2 = ((text != null) ? text : ((Uri)uriToCheck).GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped));
					Match match = delayedRegex.AsRegex.Match(text2);
					if (match != null && match.Index == 0 && match.Length == text2.Length)
					{
						return true;
					}
					if (text == null)
					{
						text2 = ((Uri)uriToCheck).GetComponents(UriComponents.HttpRequestUrl, UriFormat.SafeUnescaped);
						match = delayedRegex.AsRegex.Match(text2);
						if (match != null && match.Index == 0 && match.Length == text2.Length)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x00083710 File Offset: 0x00081910
		private static void intersectList(ArrayList A, ArrayList B, ArrayList result)
		{
			bool[] array = new bool[A.Count];
			bool[] array2 = new bool[B.Count];
			int num = 0;
			foreach (object obj in A)
			{
				int num2 = 0;
				foreach (object obj2 in B)
				{
					if (!array2[num2] && obj.GetType() == obj2.GetType())
					{
						if (obj is Uri)
						{
							if (obj.Equals(obj2))
							{
								result.Add(obj);
								array[num] = (array2[num2] = true);
								break;
							}
						}
						else if (string.Compare(obj.ToString(), obj2.ToString(), StringComparison.OrdinalIgnoreCase) == 0)
						{
							result.Add(obj);
							array[num] = (array2[num2] = true);
							break;
						}
					}
					num2++;
				}
				num++;
			}
			num = 0;
			foreach (object obj3 in A)
			{
				if (!array[num])
				{
					int num2 = 0;
					foreach (object obj4 in B)
					{
						if (!array2[num2])
						{
							bool flag;
							object obj5 = WebPermission.intersectPair(obj3, obj4, out flag);
							if (obj5 != null)
							{
								bool flag2 = false;
								foreach (object obj6 in result)
								{
									if (flag == obj6 is Uri && (flag ? obj5.Equals(obj6) : (string.Compare(obj6.ToString(), obj5.ToString(), StringComparison.OrdinalIgnoreCase) == 0)))
									{
										flag2 = true;
										break;
									}
								}
								if (!flag2)
								{
									result.Add(obj5);
								}
							}
						}
						num2++;
					}
				}
				num++;
			}
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x00083980 File Offset: 0x00081B80
		private static object intersectPair(object L, object R, out bool isUri)
		{
			isUri = false;
			DelayedRegex delayedRegex = L as DelayedRegex;
			DelayedRegex delayedRegex2 = R as DelayedRegex;
			if (delayedRegex != null && delayedRegex2 != null)
			{
				return new DelayedRegex(string.Concat(new string[]
				{
					"(?=(",
					delayedRegex.ToString(),
					"))(",
					delayedRegex2.ToString(),
					")"
				}));
			}
			if (delayedRegex != null && delayedRegex2 == null)
			{
				isUri = R is Uri;
				string text = (isUri ? ((Uri)R).GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped) : R.ToString());
				Match match = delayedRegex.AsRegex.Match(text);
				if (match != null && match.Index == 0 && match.Length == text.Length)
				{
					return R;
				}
				return null;
			}
			else if (delayedRegex == null && delayedRegex2 != null)
			{
				isUri = L is Uri;
				string text2 = (isUri ? ((Uri)L).GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped) : L.ToString());
				Match match2 = delayedRegex2.AsRegex.Match(text2);
				if (match2 != null && match2.Index == 0 && match2.Length == text2.Length)
				{
					return L;
				}
				return null;
			}
			else
			{
				isUri = L is Uri;
				if (isUri)
				{
					if (!L.Equals(R))
					{
						return null;
					}
					return L;
				}
				else
				{
					if (string.Compare(L.ToString(), R.ToString(), StringComparison.OrdinalIgnoreCase) != 0)
					{
						return null;
					}
					return L;
				}
			}
		}

		// Token: 0x04001E5A RID: 7770
		private bool m_noRestriction;

		// Token: 0x04001E5B RID: 7771
		[OptionalField]
		private bool m_UnrestrictedConnect;

		// Token: 0x04001E5C RID: 7772
		[OptionalField]
		private bool m_UnrestrictedAccept;

		// Token: 0x04001E5D RID: 7773
		private ArrayList m_connectList = new ArrayList();

		// Token: 0x04001E5E RID: 7774
		private ArrayList m_acceptList = new ArrayList();

		// Token: 0x04001E5F RID: 7775
		internal const string MatchAll = ".*";

		// Token: 0x04001E60 RID: 7776
		private static volatile Regex s_MatchAllRegex;
	}
}
