using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Provides the application directory as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x02000555 RID: 1365
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationDirectory : EvidenceBase, IBuiltInEvidence
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ApplicationDirectory" /> class.</summary>
		/// <param name="name">The path of the application directory. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06003D66 RID: 15718 RVA: 0x000DCBC4 File Offset: 0x000DADC4
		public ApplicationDirectory(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length < 1)
			{
				throw new FormatException(Locale.GetText("Empty"));
			}
			this.directory = name;
		}

		/// <summary>Gets the path of the application directory.</summary>
		/// <returns>The path of the application directory.</returns>
		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06003D67 RID: 15719 RVA: 0x000DCBFA File Offset: 0x000DADFA
		public string Directory
		{
			get
			{
				return this.directory;
			}
		}

		/// <summary>Creates a new copy of the <see cref="T:System.Security.Policy.ApplicationDirectory" />.</summary>
		/// <returns>A new, identical copy of the <see cref="T:System.Security.Policy.ApplicationDirectory" />.</returns>
		// Token: 0x06003D68 RID: 15720 RVA: 0x000DCC02 File Offset: 0x000DAE02
		public object Copy()
		{
			return new ApplicationDirectory(this.Directory);
		}

		/// <summary>Determines whether instances of the same type of an evidence object are equivalent.</summary>
		/// <returns>true if the two instances are equivalent; otherwise, false.</returns>
		/// <param name="o">An object of same type as the current evidence object. </param>
		// Token: 0x06003D69 RID: 15721 RVA: 0x000DCC10 File Offset: 0x000DAE10
		public override bool Equals(object o)
		{
			ApplicationDirectory applicationDirectory = o as ApplicationDirectory;
			if (applicationDirectory != null)
			{
				this.ThrowOnInvalid(applicationDirectory.directory);
				return this.directory == applicationDirectory.directory;
			}
			return false;
		}

		/// <summary>Gets the hash code of the current application directory.</summary>
		/// <returns>The hash code of the current application directory.</returns>
		// Token: 0x06003D6A RID: 15722 RVA: 0x000DCC46 File Offset: 0x000DAE46
		public override int GetHashCode()
		{
			return this.Directory.GetHashCode();
		}

		/// <summary>Gets a string representation of the state of the <see cref="T:System.Security.Policy.ApplicationDirectory" /> evidence object.</summary>
		/// <returns>A representation of the state of the <see cref="T:System.Security.Policy.ApplicationDirectory" /> evidence object.</returns>
		// Token: 0x06003D6B RID: 15723 RVA: 0x000DCC54 File Offset: 0x000DAE54
		public override string ToString()
		{
			this.ThrowOnInvalid(this.Directory);
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.ApplicationDirectory");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Directory", this.directory));
			return securityElement.ToString();
		}

		// Token: 0x06003D6C RID: 15724 RVA: 0x000DCCA2 File Offset: 0x000DAEA2
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return (verbose ? 3 : 1) + this.directory.Length;
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x000DCCB7 File Offset: 0x000DAEB7
		private void ThrowOnInvalid(string appdir)
		{
			if (appdir.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid character(s) in directory {0}"), appdir), "other");
			}
		}

		// Token: 0x04001F93 RID: 8083
		private string directory;
	}
}
