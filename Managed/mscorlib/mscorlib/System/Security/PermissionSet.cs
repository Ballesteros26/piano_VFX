using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using Unity;

namespace System.Security
{
	/// <summary>Represents a collection that can contain many different types of permissions.</summary>
	// Token: 0x02000546 RID: 1350
	[ComVisible(true)]
	[MonoTODO("CAS support is experimental (and unsupported).")]
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293")]
	[Serializable]
	public class PermissionSet : ISecurityEncodable, ICollection, IEnumerable, IStackWalk, IDeserializationCallback
	{
		// Token: 0x06003C9F RID: 15519 RVA: 0x000D9B64 File Offset: 0x000D7D64
		internal PermissionSet()
		{
			this.list = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.PermissionSet" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the enumeration values that specifies the permission set's access to resources. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06003CA0 RID: 15520 RVA: 0x000D9B77 File Offset: 0x000D7D77
		public PermissionSet(PermissionState state)
			: this()
		{
			this.state = CodeAccessPermission.CheckPermissionState(state, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.PermissionSet" /> class with initial values taken from the <paramref name="permSet" /> parameter.</summary>
		/// <param name="permSet">The set from which to take the value of the new <see cref="T:System.Security.PermissionSet" />, or null to create an empty <see cref="T:System.Security.PermissionSet" />. </param>
		// Token: 0x06003CA1 RID: 15521 RVA: 0x000D9B8C File Offset: 0x000D7D8C
		public PermissionSet(PermissionSet permSet)
			: this()
		{
			if (permSet != null)
			{
				this.state = permSet.state;
				foreach (object obj in permSet.list)
				{
					IPermission permission = (IPermission)obj;
					this.list.Add(permission);
				}
			}
		}

		// Token: 0x06003CA2 RID: 15522 RVA: 0x000D9C00 File Offset: 0x000D7E00
		internal PermissionSet(string xml)
			: this()
		{
			this.state = PermissionState.None;
			if (xml != null)
			{
				SecurityElement securityElement = SecurityElement.FromString(xml);
				this.FromXml(securityElement);
			}
		}

		// Token: 0x06003CA3 RID: 15523 RVA: 0x000D9C2B File Offset: 0x000D7E2B
		internal PermissionSet(IPermission perm)
			: this()
		{
			if (perm != null)
			{
				this.list.Add(perm);
			}
		}

		/// <summary>Adds a specified permission to the <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>The union of the permission added and any permission of the same type that already exists in the <see cref="T:System.Security.PermissionSet" />.</returns>
		/// <param name="perm">The permission to add. </param>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003CA4 RID: 15524 RVA: 0x000D9C44 File Offset: 0x000D7E44
		public IPermission AddPermission(IPermission perm)
		{
			if (perm == null || this._readOnly)
			{
				return perm;
			}
			if (this.state == PermissionState.Unrestricted)
			{
				return (IPermission)Activator.CreateInstance(perm.GetType(), PermissionSet.psUnrestricted);
			}
			IPermission permission = this.RemovePermission(perm.GetType());
			if (permission != null)
			{
				perm = perm.Union(permission);
			}
			this.list.Add(perm);
			return perm;
		}

		/// <summary>Declares that the calling code can access the resource protected by a permission demand through the code that calls this method, even if callers higher in the stack have not been granted permission to access the resource. Using <see cref="M:System.Security.PermissionSet.Assert" /> can create security vulnerabilities.</summary>
		/// <exception cref="T:System.Security.SecurityException">The <see cref="T:System.Security.PermissionSet" /> instance asserted has not been granted to the asserting code.-or- There is already an active <see cref="M:System.Security.PermissionSet.Assert" /> for the current frame. </exception>
		// Token: 0x06003CA5 RID: 15525 RVA: 0x000D9CA4 File Offset: 0x000D7EA4
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Assertion = true)]
		public void Assert()
		{
			int num = this.Count;
			foreach (object obj in this.list)
			{
				IPermission permission = (IPermission)obj;
				if (permission is IStackWalk)
				{
					if (!SecurityManager.IsGranted(permission))
					{
						return;
					}
				}
				else
				{
					num--;
				}
			}
			if (SecurityManager.SecurityEnabled && num > 0)
			{
				throw new NotSupportedException("Currently only declarative Assert are supported.");
			}
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x000D9D2C File Offset: 0x000D7F2C
		internal void Clear()
		{
			this.list.Clear();
		}

		/// <summary>Creates a copy of the <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Security.PermissionSet" />.</returns>
		// Token: 0x06003CA7 RID: 15527 RVA: 0x000D9D39 File Offset: 0x000D7F39
		public virtual PermissionSet Copy()
		{
			return new PermissionSet(this);
		}

		/// <summary>Copies the permission objects of the set to the indicated location in an <see cref="T:System.Array" />.</summary>
		/// <param name="array">The target array to which to copy. </param>
		/// <param name="index">The starting position in the array to begin copying (zero based). </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="array" /> parameter has more than one dimension. </exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is out of the range of the <paramref name="array" /> parameter. </exception>
		// Token: 0x06003CA8 RID: 15528 RVA: 0x000D9D44 File Offset: 0x000D7F44
		public virtual void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (this.list.Count > 0)
			{
				if (array.Rank > 1)
				{
					throw new ArgumentException(Locale.GetText("Array has more than one dimension"));
				}
				if (index < 0 || index >= array.Length)
				{
					throw new IndexOutOfRangeException("index");
				}
				this.list.CopyTo(array, index);
			}
		}

		/// <summary>Forces a <see cref="T:System.Security.SecurityException" /> at run time if all callers higher in the call stack have not been granted the permissions specified by the current instance.</summary>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have the permission demanded. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003CA9 RID: 15529 RVA: 0x000D9DAC File Offset: 0x000D7FAC
		[SecuritySafeCritical]
		public void Demand()
		{
			if (this.IsEmpty())
			{
				return;
			}
			int count = this.list.Count;
			if (this._ignored == null || this._ignored.Length != count)
			{
				this._ignored = new bool[count];
			}
			bool flag = this.IsUnrestricted();
			for (int i = 0; i < count; i++)
			{
				IPermission permission = (IPermission)this.list[i];
				if (permission.GetType().IsSubclassOf(typeof(CodeAccessPermission)))
				{
					this._ignored[i] = false;
					flag = true;
				}
				else
				{
					this._ignored[i] = true;
					permission.Demand();
				}
			}
			if (flag && SecurityManager.SecurityEnabled)
			{
				this.CasOnlyDemand(this._declsec ? 5 : 3);
			}
		}

		// Token: 0x06003CAA RID: 15530 RVA: 0x000D9E61 File Offset: 0x000D8061
		internal void CasOnlyDemand(int skip)
		{
			if (this._ignored == null)
			{
				this._ignored = new bool[this.list.Count];
			}
		}

		/// <summary>Causes any <see cref="M:System.Security.PermissionSet.Demand" /> that passes through the calling code for a permission that has an intersection with a permission of a type contained in the current <see cref="T:System.Security.PermissionSet" /> to fail.</summary>
		/// <exception cref="T:System.Security.SecurityException">A previous call to <see cref="M:System.Security.PermissionSet.Deny" /> has already restricted the permissions for the current stack frame. </exception>
		// Token: 0x06003CAB RID: 15531 RVA: 0x000D9E84 File Offset: 0x000D8084
		[Obsolete("Deny is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		[SecuritySafeCritical]
		public void Deny()
		{
			if (!SecurityManager.SecurityEnabled)
			{
				return;
			}
			using (IEnumerator enumerator = this.list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((IPermission)enumerator.Current) is IStackWalk)
					{
						throw new NotSupportedException("Currently only declarative Deny are supported.");
					}
				}
			}
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="et">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="et" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="et" /> parameter is not a valid permission element.-or- The <paramref name="et" /> parameter's version number is not supported. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003CAC RID: 15532 RVA: 0x000D9EF0 File Offset: 0x000D80F0
		public virtual void FromXml(SecurityElement et)
		{
			if (et == null)
			{
				throw new ArgumentNullException("et");
			}
			if (et.Tag != "PermissionSet")
			{
				throw new ArgumentException(string.Format("Invalid tag {0} expected {1}", et.Tag, "PermissionSet"), "et");
			}
			this.list.Clear();
			if (CodeAccessPermission.IsUnrestricted(et))
			{
				this.state = PermissionState.Unrestricted;
				return;
			}
			this.state = PermissionState.None;
			if (et.Children != null)
			{
				foreach (object obj in et.Children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					string text = securityElement.Attribute("class");
					if (text == null)
					{
						throw new ArgumentException(Locale.GetText("No permission class is specified."));
					}
					if (this.Resolver != null)
					{
						text = this.Resolver.ResolveClassName(text);
					}
					this.list.Add(PermissionBuilder.Create(text, securityElement));
				}
			}
		}

		/// <summary>Returns an enumerator for the permissions of the set.</summary>
		/// <returns>An enumerator object for the permissions of the set.</returns>
		// Token: 0x06003CAD RID: 15533 RVA: 0x000D9FF4 File Offset: 0x000D81F4
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Determines whether the current <see cref="T:System.Security.PermissionSet" /> is a subset of the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>true if the current <see cref="T:System.Security.PermissionSet" /> is a subset of the <paramref name="target" /> parameter; otherwise, false.</returns>
		/// <param name="target">The permission set to test for the subset relationship. This must be either a <see cref="T:System.Security.PermissionSet" /> or a <see cref="T:System.Security.NamedPermissionSet" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003CAE RID: 15534 RVA: 0x000DA004 File Offset: 0x000D8204
		public bool IsSubsetOf(PermissionSet target)
		{
			if (target == null || target.IsEmpty())
			{
				return this.IsEmpty();
			}
			if (target.IsUnrestricted())
			{
				return true;
			}
			if (this.IsUnrestricted())
			{
				return false;
			}
			if (this.IsUnrestricted() && (target == null || !target.IsUnrestricted()))
			{
				return false;
			}
			foreach (object obj in this.list)
			{
				IPermission permission = (IPermission)obj;
				Type type = permission.GetType();
				IPermission permission2;
				if (target.IsUnrestricted() && permission is CodeAccessPermission && permission is IUnrestrictedPermission)
				{
					permission2 = (IPermission)Activator.CreateInstance(type, PermissionSet.psUnrestricted);
				}
				else
				{
					permission2 = target.GetPermission(type);
				}
				if (!permission.IsSubsetOf(permission2))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Causes any <see cref="M:System.Security.PermissionSet.Demand" /> that passes through the calling code for any <see cref="T:System.Security.PermissionSet" /> that is not a subset of the current <see cref="T:System.Security.PermissionSet" /> to fail.</summary>
		// Token: 0x06003CAF RID: 15535 RVA: 0x000DA0E4 File Offset: 0x000D82E4
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		[SecuritySafeCritical]
		public void PermitOnly()
		{
			if (!SecurityManager.SecurityEnabled)
			{
				return;
			}
			using (IEnumerator enumerator = this.list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((IPermission)enumerator.Current) is IStackWalk)
					{
						throw new NotSupportedException("Currently only declarative Deny are supported.");
					}
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Security.PermissionSet" /> contains permissions that are not derived from <see cref="T:System.Security.CodeAccessPermission" />.</summary>
		/// <returns>true if the <see cref="T:System.Security.PermissionSet" /> contains permissions that are not derived from <see cref="T:System.Security.CodeAccessPermission" />; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003CB0 RID: 15536 RVA: 0x000DA150 File Offset: 0x000D8350
		public bool ContainsNonCodeAccessPermissions()
		{
			if (this.list.Count > 0)
			{
				using (IEnumerator enumerator = this.list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!((IPermission)enumerator.Current).GetType().IsSubclassOf(typeof(CodeAccessPermission)))
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		/// <summary>Converts an encoded <see cref="T:System.Security.PermissionSet" /> from one XML encoding format to another XML encoding format.</summary>
		/// <returns>An encrypted permission set with the specified output format.</returns>
		/// <param name="inFormat">A string representing one of the following encoding formats: ASCII, Unicode, or Binary. Possible values are "XMLASCII" or "XML", "XMLUNICODE", and "BINARY".</param>
		/// <param name="inData">An XML-encoded permission set.</param>
		/// <param name="outFormat">A string representing one of the following encoding formats: ASCII, Unicode, or Binary. Possible values are "XMLASCII" or "XML", "XMLUNICODE", and "BINARY".</param>
		/// <exception cref="T:System.NotImplementedException">In all cases.</exception>
		// Token: 0x06003CB1 RID: 15537 RVA: 0x000DA1CC File Offset: 0x000D83CC
		public static byte[] ConvertPermissionSet(string inFormat, byte[] inData, string outFormat)
		{
			if (inFormat == null)
			{
				throw new ArgumentNullException("inFormat");
			}
			if (outFormat == null)
			{
				throw new ArgumentNullException("outFormat");
			}
			if (inData == null)
			{
				return null;
			}
			if (inFormat == outFormat)
			{
				return inData;
			}
			PermissionSet permissionSet = null;
			if (inFormat == "BINARY")
			{
				if (outFormat.StartsWith("XML"))
				{
					using (MemoryStream memoryStream = new MemoryStream(inData))
					{
						permissionSet = (PermissionSet)new BinaryFormatter().Deserialize(memoryStream);
						memoryStream.Close();
					}
					string text = permissionSet.ToString();
					if (outFormat == "XML" || outFormat == "XMLASCII")
					{
						return Encoding.ASCII.GetBytes(text);
					}
					if (outFormat == "XMLUNICODE")
					{
						return Encoding.Unicode.GetBytes(text);
					}
				}
			}
			else
			{
				if (!inFormat.StartsWith("XML"))
				{
					return null;
				}
				if (outFormat == "BINARY")
				{
					string text2 = null;
					if (!(inFormat == "XML") && !(inFormat == "XMLASCII"))
					{
						if (inFormat == "XMLUNICODE")
						{
							text2 = Encoding.Unicode.GetString(inData);
						}
					}
					else
					{
						text2 = Encoding.ASCII.GetString(inData);
					}
					if (text2 != null)
					{
						permissionSet = new PermissionSet(PermissionState.None);
						permissionSet.FromXml(SecurityElement.FromString(text2));
						MemoryStream memoryStream2 = new MemoryStream();
						new BinaryFormatter().Serialize(memoryStream2, permissionSet);
						memoryStream2.Close();
						return memoryStream2.ToArray();
					}
				}
				else if (outFormat.StartsWith("XML"))
				{
					throw new XmlSyntaxException(string.Format(Locale.GetText("Can't convert from {0} to {1}"), inFormat, outFormat));
				}
			}
			throw new SerializationException(string.Format(Locale.GetText("Unknown output format {0}."), outFormat));
		}

		/// <summary>Gets a permission object of the specified type, if it exists in the set.</summary>
		/// <returns>A copy of the permission object of the type specified by the <paramref name="permClass" /> parameter contained in the <see cref="T:System.Security.PermissionSet" />, or null if none exists.</returns>
		/// <param name="permClass">The type of the desired permission object. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003CB2 RID: 15538 RVA: 0x000DA384 File Offset: 0x000D8584
		public IPermission GetPermission(Type permClass)
		{
			if (permClass == null || this.list.Count == 0)
			{
				return null;
			}
			foreach (object obj in this.list)
			{
				if (obj != null && obj.GetType().Equals(permClass))
				{
					return (IPermission)obj;
				}
			}
			return null;
		}

		/// <summary>Creates and returns a permission set that is the intersection of the current <see cref="T:System.Security.PermissionSet" /> and the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>A new permission set that represents the intersection of the current <see cref="T:System.Security.PermissionSet" /> and the specified target. This object is null if the intersection is empty.</returns>
		/// <param name="other">A permission set to intersect with the current <see cref="T:System.Security.PermissionSet" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003CB3 RID: 15539 RVA: 0x000DA408 File Offset: 0x000D8608
		public PermissionSet Intersect(PermissionSet other)
		{
			if (other == null || other.IsEmpty() || this.IsEmpty())
			{
				return null;
			}
			PermissionState permissionState = PermissionState.None;
			if (this.IsUnrestricted() && other.IsUnrestricted())
			{
				permissionState = PermissionState.Unrestricted;
			}
			PermissionSet permissionSet;
			if (permissionState == PermissionState.Unrestricted)
			{
				permissionSet = new PermissionSet(permissionState);
			}
			else if (this.IsUnrestricted())
			{
				permissionSet = other.Copy();
			}
			else if (other.IsUnrestricted())
			{
				permissionSet = this.Copy();
			}
			else
			{
				permissionSet = new PermissionSet(permissionState);
				this.InternalIntersect(permissionSet, this, other, false);
			}
			return permissionSet;
		}

		// Token: 0x06003CB4 RID: 15540 RVA: 0x000DA484 File Offset: 0x000D8684
		internal void InternalIntersect(PermissionSet intersect, PermissionSet a, PermissionSet b, bool unrestricted)
		{
			foreach (object obj in b.list)
			{
				IPermission permission = (IPermission)obj;
				IPermission permission2 = a.GetPermission(permission.GetType());
				if (permission2 != null)
				{
					intersect.AddPermission(permission.Intersect(permission2));
				}
				else if (unrestricted)
				{
					intersect.AddPermission(permission);
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Security.PermissionSet" /> is empty.</summary>
		/// <returns>true if the <see cref="T:System.Security.PermissionSet" /> is empty; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003CB5 RID: 15541 RVA: 0x000DA504 File Offset: 0x000D8704
		public bool IsEmpty()
		{
			if (this.state == PermissionState.Unrestricted)
			{
				return false;
			}
			if (this.list == null || this.list.Count == 0)
			{
				return true;
			}
			using (IEnumerator enumerator = this.list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!((IPermission)enumerator.Current).IsSubsetOf(null))
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Determines whether the <see cref="T:System.Security.PermissionSet" /> is Unrestricted.</summary>
		/// <returns>true if the <see cref="T:System.Security.PermissionSet" /> is Unrestricted; otherwise, false.</returns>
		// Token: 0x06003CB6 RID: 15542 RVA: 0x000DA588 File Offset: 0x000D8788
		public bool IsUnrestricted()
		{
			return this.state == PermissionState.Unrestricted;
		}

		/// <summary>Removes a permission of a certain type from the set.</summary>
		/// <returns>The permission removed from the set.</returns>
		/// <param name="permClass">The type of permission to delete. </param>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		// Token: 0x06003CB7 RID: 15543 RVA: 0x000DA594 File Offset: 0x000D8794
		public IPermission RemovePermission(Type permClass)
		{
			if (permClass == null || this._readOnly)
			{
				return null;
			}
			foreach (object obj in this.list)
			{
				if (obj.GetType().Equals(permClass))
				{
					this.list.Remove(obj);
					return (IPermission)obj;
				}
			}
			return null;
		}

		/// <summary>Sets a permission to the <see cref="T:System.Security.PermissionSet" />, replacing any existing permission of the same type.</summary>
		/// <returns>The set permission.</returns>
		/// <param name="perm">The permission to set. </param>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003CB8 RID: 15544 RVA: 0x000DA61C File Offset: 0x000D881C
		public IPermission SetPermission(IPermission perm)
		{
			if (perm == null || this._readOnly)
			{
				return perm;
			}
			IUnrestrictedPermission unrestrictedPermission = perm as IUnrestrictedPermission;
			if (unrestrictedPermission == null)
			{
				this.state = PermissionState.None;
			}
			else
			{
				this.state = (unrestrictedPermission.IsUnrestricted() ? this.state : PermissionState.None);
			}
			this.RemovePermission(perm.GetType());
			this.list.Add(perm);
			return perm;
		}

		/// <summary>Returns a string representation of the <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>A representation of the <see cref="T:System.Security.PermissionSet" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003CB9 RID: 15545 RVA: 0x000DA67B File Offset: 0x000D887B
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003CBA RID: 15546 RVA: 0x000DA688 File Offset: 0x000D8888
		public virtual SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("PermissionSet");
			securityElement.AddAttribute("class", base.GetType().FullName);
			securityElement.AddAttribute("version", 1.ToString());
			if (this.state == PermissionState.Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			foreach (object obj in this.list)
			{
				IPermission permission = (IPermission)obj;
				securityElement.AddChild(permission.ToXml());
			}
			return securityElement;
		}

		/// <summary>Creates a <see cref="T:System.Security.PermissionSet" /> that is the union of the current <see cref="T:System.Security.PermissionSet" /> and the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>A new permission set that represents the union of the current <see cref="T:System.Security.PermissionSet" /> and the specified <see cref="T:System.Security.PermissionSet" />.</returns>
		/// <param name="other">The permission set to form the union with the current <see cref="T:System.Security.PermissionSet" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003CBB RID: 15547 RVA: 0x000DA738 File Offset: 0x000D8938
		public PermissionSet Union(PermissionSet other)
		{
			if (other == null)
			{
				return this.Copy();
			}
			PermissionSet permissionSet = null;
			if (this.IsUnrestricted() || other.IsUnrestricted())
			{
				return new PermissionSet(PermissionState.Unrestricted);
			}
			permissionSet = this.Copy();
			foreach (object obj in other.list)
			{
				IPermission permission = (IPermission)obj;
				permissionSet.AddPermission(permission);
			}
			return permissionSet;
		}

		/// <summary>Gets the number of permission objects contained in the permission set.</summary>
		/// <returns>The number of permission objects contained in the <see cref="T:System.Security.PermissionSet" />.</returns>
		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06003CBC RID: 15548 RVA: 0x000DA7C0 File Offset: 0x000D89C0
		public virtual int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is guaranteed to be thread safe.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06003CBD RID: 15549 RVA: 0x000DA7CD File Offset: 0x000D89CD
		public virtual bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06003CBE RID: 15550 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the root object of the current collection.</summary>
		/// <returns>The root object of the current collection.</returns>
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06003CBF RID: 15551 RVA: 0x00002119 File Offset: 0x00000319
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06003CC0 RID: 15552 RVA: 0x000DA7DA File Offset: 0x000D89DA
		// (set) Token: 0x06003CC1 RID: 15553 RVA: 0x000DA7E2 File Offset: 0x000D89E2
		internal bool DeclarativeSecurity
		{
			get
			{
				return this._declsec;
			}
			set
			{
				this._declsec = value;
			}
		}

		/// <summary>Runs when the entire object graph has been deserialized.</summary>
		/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
		// Token: 0x06003CC2 RID: 15554 RVA: 0x00002194 File Offset: 0x00000394
		[MonoTODO("may not be required")]
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.PermissionSet" /> or <see cref="T:System.Security.NamedPermissionSet" /> object is equal to the current <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>true if the specified object is equal to the current <see cref="T:System.Security.PermissionSet" /> object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current <see cref="T:System.Security.PermissionSet" />. </param>
		// Token: 0x06003CC3 RID: 15555 RVA: 0x000DA7EC File Offset: 0x000D89EC
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			PermissionSet permissionSet = obj as PermissionSet;
			if (permissionSet == null)
			{
				return false;
			}
			if (this.state != permissionSet.state)
			{
				return false;
			}
			if (this.list.Count != permissionSet.Count)
			{
				return false;
			}
			for (int i = 0; i < this.list.Count; i++)
			{
				bool flag = false;
				int num = 0;
				while (i < permissionSet.list.Count)
				{
					if (this.list[i].Equals(permissionSet.list[num]))
					{
						flag = true;
						break;
					}
					num++;
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.PermissionSet" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.PermissionSet" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003CC4 RID: 15556 RVA: 0x000DA886 File Offset: 0x000D8A86
		[ComVisible(false)]
		public override int GetHashCode()
		{
			if (this.list.Count != 0)
			{
				return base.GetHashCode();
			}
			return (int)this.state;
		}

		/// <summary>Causes any previous <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame to be removed and no longer be in effect.</summary>
		/// <exception cref="T:System.InvalidOperationException">There is no previous <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame. </exception>
		// Token: 0x06003CC5 RID: 15557 RVA: 0x000DA8A2 File Offset: 0x000D8AA2
		public static void RevertAssert()
		{
			CodeAccessPermission.RevertAssert();
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06003CC6 RID: 15558 RVA: 0x000DA8A9 File Offset: 0x000D8AA9
		// (set) Token: 0x06003CC7 RID: 15559 RVA: 0x000DA8B1 File Offset: 0x000D8AB1
		internal PolicyLevel Resolver
		{
			get
			{
				return this._policyLevel;
			}
			set
			{
				this._policyLevel = value;
			}
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x000DA8BA File Offset: 0x000D8ABA
		internal void SetReadOnly(bool value)
		{
			this._readOnly = value;
		}

		// Token: 0x06003CC9 RID: 15561 RVA: 0x000DA8C4 File Offset: 0x000D8AC4
		private bool AllIgnored()
		{
			if (this._ignored == null)
			{
				throw new NotSupportedException("bad bad bad");
			}
			for (int i = 0; i < this._ignored.Length; i++)
			{
				if (!this._ignored[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003CCA RID: 15562 RVA: 0x000DA904 File Offset: 0x000D8B04
		internal static PermissionSet CreateFromBinaryFormat(byte[] data)
		{
			if (data == null || data[0] != 46 || data.Length < 2)
			{
				throw new SecurityException(Locale.GetText("Invalid data in 2.0 metadata format."));
			}
			int num = 1;
			int num2 = PermissionSet.ReadEncodedInt(data, ref num);
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			for (int i = 0; i < num2; i++)
			{
				IPermission permission = PermissionSet.ProcessAttribute(data, ref num);
				if (permission == null)
				{
					throw new SecurityException(Locale.GetText("Unsupported data found in 2.0 metadata format."));
				}
				permissionSet.AddPermission(permission);
			}
			return permissionSet;
		}

		// Token: 0x06003CCB RID: 15563 RVA: 0x000DA978 File Offset: 0x000D8B78
		internal static int ReadEncodedInt(byte[] data, ref int position)
		{
			int num;
			if ((data[position] & 128) == 0)
			{
				num = (int)data[position];
				position++;
			}
			else if ((data[position] & 64) == 0)
			{
				num = ((int)(data[position] & 63) << 8) | (int)data[position + 1];
				position += 2;
			}
			else
			{
				num = ((int)(data[position] & 31) << 24) | ((int)data[position + 1] << 16) | ((int)data[position + 2] << 8) | (int)data[position + 3];
				position += 4;
			}
			return num;
		}

		// Token: 0x06003CCC RID: 15564 RVA: 0x000DA9F0 File Offset: 0x000D8BF0
		internal static IPermission ProcessAttribute(byte[] data, ref int position)
		{
			int num = PermissionSet.ReadEncodedInt(data, ref position);
			string @string = Encoding.UTF8.GetString(data, position, num);
			position += num;
			Type type = Type.GetType(@string);
			SecurityAttribute securityAttribute = Activator.CreateInstance(type, PermissionSet.action) as SecurityAttribute;
			if (securityAttribute == null)
			{
				return null;
			}
			PermissionSet.ReadEncodedInt(data, ref position);
			int num2 = PermissionSet.ReadEncodedInt(data, ref position);
			for (int i = 0; i < num2; i++)
			{
				int num3 = position;
				position = num3 + 1;
				byte b = data[num3];
				bool flag;
				if (b != 83)
				{
					if (b != 84)
					{
						return null;
					}
					flag = true;
				}
				else
				{
					flag = false;
				}
				bool flag2 = false;
				num3 = position;
				position = num3 + 1;
				byte b2 = data[num3];
				if (b2 == 29)
				{
					flag2 = true;
					num3 = position;
					position = num3 + 1;
					b2 = data[num3];
				}
				int num4 = PermissionSet.ReadEncodedInt(data, ref position);
				string string2 = Encoding.UTF8.GetString(data, position, num4);
				position += num4;
				int num5 = 1;
				if (flag2)
				{
					num5 = BitConverter.ToInt32(data, position);
					position += 4;
				}
				object[] array = null;
				for (int j = 0; j < num5; j++)
				{
					object obj;
					switch (b2)
					{
					case 2:
						num3 = position;
						position = num3 + 1;
						obj = Convert.ToBoolean(data[num3]);
						break;
					case 3:
						obj = Convert.ToChar(data[position]);
						position += 2;
						break;
					case 4:
						num3 = position;
						position = num3 + 1;
						obj = Convert.ToSByte(data[num3]);
						break;
					case 5:
						num3 = position;
						position = num3 + 1;
						obj = Convert.ToByte(data[num3]);
						break;
					case 6:
						obj = Convert.ToInt16(data[position]);
						position += 2;
						break;
					case 7:
						obj = Convert.ToUInt16(data[position]);
						position += 2;
						break;
					case 8:
						obj = Convert.ToInt32(data[position]);
						position += 4;
						break;
					case 9:
						obj = Convert.ToUInt32(data[position]);
						position += 4;
						break;
					case 10:
						obj = Convert.ToInt64(data[position]);
						position += 8;
						break;
					case 11:
						obj = Convert.ToUInt64(data[position]);
						position += 8;
						break;
					case 12:
						obj = Convert.ToSingle(data[position]);
						position += 4;
						break;
					case 13:
						obj = Convert.ToDouble(data[position]);
						position += 8;
						break;
					case 14:
					{
						string text = null;
						if (data[position] != 255)
						{
							int num6 = PermissionSet.ReadEncodedInt(data, ref position);
							text = Encoding.UTF8.GetString(data, position, num6);
							position += num6;
						}
						else
						{
							position++;
						}
						obj = text;
						break;
					}
					default:
					{
						if (b2 != 80)
						{
							return null;
						}
						int num7 = PermissionSet.ReadEncodedInt(data, ref position);
						obj = Type.GetType(Encoding.UTF8.GetString(data, position, num7));
						position += num7;
						break;
					}
					}
					if (flag)
					{
						type.GetProperty(string2).SetValue(securityAttribute, obj, array);
					}
					else
					{
						type.GetField(string2).SetValue(securityAttribute, obj);
					}
				}
			}
			return securityAttribute.CreatePermission();
		}

		/// <summary>Adds a specified permission to the <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>The union of the permission added and any permission of the same type that already exists in the <see cref="T:System.Security.PermissionSet" />, or null if <paramref name="perm" /> is null.</returns>
		/// <param name="perm">The permission to add.</param>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		// Token: 0x06003CCE RID: 15566 RVA: 0x00032521 File Offset: 0x00030721
		protected virtual IPermission AddPermissionImpl(IPermission perm)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns an enumerator for the permissions of the set.</summary>
		/// <returns>An enumerator object for the permissions of the set.</returns>
		// Token: 0x06003CCF RID: 15567 RVA: 0x00032521 File Offset: 0x00030721
		protected virtual IEnumerator GetEnumeratorImpl()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a permission object of the specified type, if it exists in the set.</summary>
		/// <returns>A copy of the permission object, of the type specified by the <paramref name="permClass" /> parameter, contained in the <see cref="T:System.Security.PermissionSet" />, or null if none exists.</returns>
		/// <param name="permClass">The type of the permission object.</param>
		// Token: 0x06003CD0 RID: 15568 RVA: 0x00032521 File Offset: 0x00030721
		protected virtual IPermission GetPermissionImpl(Type permClass)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Removes a permission of a certain type from the set.</summary>
		/// <returns>The permission removed from the set.</returns>
		/// <param name="permClass">The type of the permission to remove.</param>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		// Token: 0x06003CD1 RID: 15569 RVA: 0x00032521 File Offset: 0x00030721
		protected virtual IPermission RemovePermissionImpl(Type permClass)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Sets a permission to the <see cref="T:System.Security.PermissionSet" />, replacing any existing permission of the same type.</summary>
		/// <returns>The set permission.</returns>
		/// <param name="perm">The permission to set.</param>
		/// <exception cref="T:System.InvalidOperationException">The method is called from a <see cref="T:System.Security.ReadOnlyPermissionSet" />.</exception>
		// Token: 0x06003CD2 RID: 15570 RVA: 0x00032521 File Offset: 0x00030721
		protected virtual IPermission SetPermissionImpl(IPermission perm)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x04001F4D RID: 8013
		private const string tagName = "PermissionSet";

		// Token: 0x04001F4E RID: 8014
		private const int version = 1;

		// Token: 0x04001F4F RID: 8015
		private static object[] psUnrestricted = new object[] { PermissionState.Unrestricted };

		// Token: 0x04001F50 RID: 8016
		private PermissionState state;

		// Token: 0x04001F51 RID: 8017
		private ArrayList list;

		// Token: 0x04001F52 RID: 8018
		private PolicyLevel _policyLevel;

		// Token: 0x04001F53 RID: 8019
		private bool _declsec;

		// Token: 0x04001F54 RID: 8020
		private bool _readOnly;

		// Token: 0x04001F55 RID: 8021
		private bool[] _ignored;

		// Token: 0x04001F56 RID: 8022
		private static object[] action = new object[] { (SecurityAction)0 };
	}
}
