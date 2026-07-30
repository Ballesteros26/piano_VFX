using System;
using System.Collections.Specialized;

namespace System.Net
{
	// Token: 0x02000455 RID: 1109
	internal class KnownHttpVerb
	{
		// Token: 0x060020CC RID: 8396 RVA: 0x0007F4F8 File Offset: 0x0007D6F8
		internal KnownHttpVerb(string name, bool requireContentBody, bool contentBodyNotAllowed, bool connectRequest, bool expectNoContentResponse)
		{
			this.Name = name;
			this.RequireContentBody = requireContentBody;
			this.ContentBodyNotAllowed = contentBodyNotAllowed;
			this.ConnectRequest = connectRequest;
			this.ExpectNoContentResponse = expectNoContentResponse;
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x0007F528 File Offset: 0x0007D728
		static KnownHttpVerb()
		{
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Get.Name] = KnownHttpVerb.Get;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Connect.Name] = KnownHttpVerb.Connect;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Head.Name] = KnownHttpVerb.Head;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Put.Name] = KnownHttpVerb.Put;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.Post.Name] = KnownHttpVerb.Post;
			KnownHttpVerb.NamedHeaders[KnownHttpVerb.MkCol.Name] = KnownHttpVerb.MkCol;
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x0007F64C File Offset: 0x0007D84C
		public bool Equals(KnownHttpVerb verb)
		{
			return this == verb || string.Compare(this.Name, verb.Name, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x0007F66C File Offset: 0x0007D86C
		public static KnownHttpVerb Parse(string name)
		{
			KnownHttpVerb knownHttpVerb = KnownHttpVerb.NamedHeaders[name] as KnownHttpVerb;
			if (knownHttpVerb == null)
			{
				knownHttpVerb = new KnownHttpVerb(name, false, false, false, false);
			}
			return knownHttpVerb;
		}

		// Token: 0x04001DC7 RID: 7623
		internal string Name;

		// Token: 0x04001DC8 RID: 7624
		internal bool RequireContentBody;

		// Token: 0x04001DC9 RID: 7625
		internal bool ContentBodyNotAllowed;

		// Token: 0x04001DCA RID: 7626
		internal bool ConnectRequest;

		// Token: 0x04001DCB RID: 7627
		internal bool ExpectNoContentResponse;

		// Token: 0x04001DCC RID: 7628
		private static ListDictionary NamedHeaders = new ListDictionary(CaseInsensitiveAscii.StaticInstance);

		// Token: 0x04001DCD RID: 7629
		internal static KnownHttpVerb Get = new KnownHttpVerb("GET", false, true, false, false);

		// Token: 0x04001DCE RID: 7630
		internal static KnownHttpVerb Connect = new KnownHttpVerb("CONNECT", false, true, true, false);

		// Token: 0x04001DCF RID: 7631
		internal static KnownHttpVerb Head = new KnownHttpVerb("HEAD", false, true, false, true);

		// Token: 0x04001DD0 RID: 7632
		internal static KnownHttpVerb Put = new KnownHttpVerb("PUT", true, false, false, false);

		// Token: 0x04001DD1 RID: 7633
		internal static KnownHttpVerb Post = new KnownHttpVerb("POST", true, false, false, false);

		// Token: 0x04001DD2 RID: 7634
		internal static KnownHttpVerb MkCol = new KnownHttpVerb("MKCOL", false, false, false, false);
	}
}
