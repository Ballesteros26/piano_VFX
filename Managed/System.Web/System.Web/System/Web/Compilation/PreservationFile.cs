using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Xml;

namespace System.Web.Compilation
{
	// Token: 0x02000665 RID: 1637
	internal class PreservationFile
	{
		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x06004601 RID: 17921 RVA: 0x000C0BB8 File Offset: 0x000BEDB8
		// (set) Token: 0x06004602 RID: 17922 RVA: 0x000C0BC0 File Offset: 0x000BEDC0
		public string Assembly
		{
			get
			{
				return this._assembly;
			}
			set
			{
				this._assembly = value;
			}
		}

		// Token: 0x170015D5 RID: 5589
		// (get) Token: 0x06004603 RID: 17923 RVA: 0x000C0BC9 File Offset: 0x000BEDC9
		// (set) Token: 0x06004604 RID: 17924 RVA: 0x000C0BD1 File Offset: 0x000BEDD1
		public string FilePath
		{
			get
			{
				return this._filePath;
			}
			set
			{
				this._filePath = value;
			}
		}

		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x06004605 RID: 17925 RVA: 0x000C0BDA File Offset: 0x000BEDDA
		// (set) Token: 0x06004606 RID: 17926 RVA: 0x000C0BE2 File Offset: 0x000BEDE2
		public int FileHash
		{
			get
			{
				return this._fileHash;
			}
			set
			{
				this._fileHash = value;
			}
		}

		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x06004607 RID: 17927 RVA: 0x000C0BEB File Offset: 0x000BEDEB
		// (set) Token: 0x06004608 RID: 17928 RVA: 0x000C0BF3 File Offset: 0x000BEDF3
		public int Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x06004609 RID: 17929 RVA: 0x000C0BFC File Offset: 0x000BEDFC
		// (set) Token: 0x0600460A RID: 17930 RVA: 0x000C0C04 File Offset: 0x000BEE04
		public int Hash
		{
			get
			{
				return this._hash;
			}
			set
			{
				this._hash = value;
			}
		}

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x0600460B RID: 17931 RVA: 0x000C0C0D File Offset: 0x000BEE0D
		// (set) Token: 0x0600460C RID: 17932 RVA: 0x000C0C15 File Offset: 0x000BEE15
		public BuildResultTypeCode ResultType
		{
			get
			{
				return this._resultType;
			}
			set
			{
				this._resultType = value;
			}
		}

		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x0600460D RID: 17933 RVA: 0x000C0C1E File Offset: 0x000BEE1E
		// (set) Token: 0x0600460E RID: 17934 RVA: 0x000C0C26 File Offset: 0x000BEE26
		public string VirtualPath
		{
			get
			{
				return this._virtualPath;
			}
			set
			{
				this._virtualPath = value;
			}
		}

		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x0600460F RID: 17935 RVA: 0x000C0C2F File Offset: 0x000BEE2F
		// (set) Token: 0x06004610 RID: 17936 RVA: 0x000C0C37 File Offset: 0x000BEE37
		public List<string> FileDeps
		{
			get
			{
				return this._filedeps;
			}
			set
			{
				this._filedeps = value;
			}
		}

		// Token: 0x06004611 RID: 17937 RVA: 0x00002050 File Offset: 0x00000250
		public PreservationFile()
		{
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x000C0C40 File Offset: 0x000BEE40
		public PreservationFile(string filePath)
		{
			this._filePath = filePath;
			this.Parse(filePath);
		}

		// Token: 0x06004613 RID: 17939 RVA: 0x000C0C56 File Offset: 0x000BEE56
		public void Parse()
		{
			if (this._filePath == null)
			{
				throw new InvalidOperationException("File path is not defined");
			}
			this.Parse(this._filePath);
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x000C0C78 File Offset: 0x000BEE78
		public void Parse(string filePath)
		{
			if (filePath == null)
			{
				throw new ArgumentNullException("File path is required", "filePath");
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(filePath);
			XmlNode documentElement = xmlDocument.DocumentElement;
			if (documentElement.Name != "preserve")
			{
				throw new InvalidOperationException("Invalid assembly mapping file format");
			}
			this.ParseRecursively(documentElement);
		}

		// Token: 0x06004615 RID: 17941 RVA: 0x000C0CD0 File Offset: 0x000BEED0
		private void ParseRecursively(XmlNode root)
		{
			this._assembly = this.GetNonEmptyRequiredAttribute(root, "assembly");
			try
			{
				this._virtualPath = this.GetNonEmptyOptionalAttribute(root, "virtualPath");
				this._fileHash = this.GetNonEmptyOptionalAttributeInt32(root, "filehash");
				this._hash = this.GetNonEmptyOptionalAttributeInt32(root, "hash");
				this._flags = this.GetNonEmptyOptionalAttributeInt32(root, "flags");
				this._resultType = (BuildResultTypeCode)this.GetNonEmptyOptionalAttributeInt32(root, "resultType");
				foreach (object obj in root.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.NodeType == XmlNodeType.Element && !(xmlNode.Name != "filedeps"))
					{
						this.ReadFileDeps(xmlNode);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06004616 RID: 17942 RVA: 0x000C0DC4 File Offset: 0x000BEFC4
		private void ReadFileDeps(XmlNode node)
		{
			if (this._filedeps == null)
			{
				this._filedeps = new List<string>();
			}
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element && !(xmlNode.Name != "filedep"))
				{
					string nonEmptyRequiredAttribute = this.GetNonEmptyRequiredAttribute(xmlNode, "name");
					this._filedeps.Add(nonEmptyRequiredAttribute);
				}
			}
		}

		// Token: 0x06004617 RID: 17943 RVA: 0x000C0E60 File Offset: 0x000BF060
		public void Save()
		{
			if (this._filePath == null)
			{
				throw new InvalidOperationException("File path is not defined");
			}
			this.Save(this._filePath);
		}

		// Token: 0x06004618 RID: 17944 RVA: 0x000C0E84 File Offset: 0x000BF084
		public void Save(string filePath)
		{
			if (filePath == null)
			{
				throw new ArgumentNullException("File path is required", "filePath");
			}
			using (XmlWriter xmlWriter = XmlWriter.Create(filePath, new XmlWriterSettings
			{
				Indent = false,
				OmitXmlDeclaration = false,
				NewLineOnAttributes = false
			}))
			{
				xmlWriter.WriteStartElement("preserve");
				xmlWriter.WriteAttributeString("assembly", this._assembly);
				if (!string.IsNullOrEmpty(this._virtualPath))
				{
					xmlWriter.WriteAttributeString("virtualPath", this._virtualPath);
				}
				if (this._fileHash != 0)
				{
					xmlWriter.WriteAttributeString("filehash", this._fileHash.ToString());
				}
				if (this._flags != 0)
				{
					xmlWriter.WriteAttributeString("flags", this._flags.ToString());
				}
				if (this._hash != 0)
				{
					xmlWriter.WriteAttributeString("hash", this._hash.ToString());
				}
				if (this._resultType != BuildResultTypeCode.Unknown)
				{
					XmlWriter xmlWriter2 = xmlWriter;
					string text = "resultType";
					int resultType = (int)this._resultType;
					xmlWriter2.WriteAttributeString(text, resultType.ToString());
				}
				if (this._filedeps != null && this._filedeps.Count > 0)
				{
					xmlWriter.WriteStartElement("filedeps");
					foreach (string text2 in this._filedeps)
					{
						xmlWriter.WriteStartElement("filedep");
						xmlWriter.WriteAttributeString("name", text2);
						xmlWriter.WriteEndElement();
					}
					xmlWriter.WriteEndElement();
				}
				xmlWriter.WriteEndElement();
			}
		}

		// Token: 0x06004619 RID: 17945 RVA: 0x00022980 File Offset: 0x00020B80
		private string GetNonEmptyOptionalAttribute(XmlNode n, string name)
		{
			return HandlersUtil.ExtractAttributeValue(name, n, true);
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x000C1038 File Offset: 0x000BF238
		private int GetNonEmptyOptionalAttributeInt32(XmlNode n, string name)
		{
			string nonEmptyOptionalAttribute = this.GetNonEmptyOptionalAttribute(n, name);
			if (nonEmptyOptionalAttribute != null)
			{
				return int.Parse(nonEmptyOptionalAttribute);
			}
			return 0;
		}

		// Token: 0x0600461B RID: 17947 RVA: 0x000C1059 File Offset: 0x000BF259
		private string GetNonEmptyRequiredAttribute(XmlNode n, string name)
		{
			return HandlersUtil.ExtractAttributeValue(name, n, false, false);
		}

		// Token: 0x04002523 RID: 9507
		private string _filePath;

		// Token: 0x04002524 RID: 9508
		private string _assembly;

		// Token: 0x04002525 RID: 9509
		private int _fileHash;

		// Token: 0x04002526 RID: 9510
		private int _flags;

		// Token: 0x04002527 RID: 9511
		private int _hash;

		// Token: 0x04002528 RID: 9512
		private BuildResultTypeCode _resultType;

		// Token: 0x04002529 RID: 9513
		private string _virtualPath;

		// Token: 0x0400252A RID: 9514
		private List<string> _filedeps;
	}
}
