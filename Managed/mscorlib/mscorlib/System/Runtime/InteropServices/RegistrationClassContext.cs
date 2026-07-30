using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the set of execution contexts in which a class object will be made available for requests to construct instances.</summary>
	// Token: 0x02000921 RID: 2337
	[Flags]
	public enum RegistrationClassContext
	{
		/// <summary>Disables activate-as-activator (AAA) activations for this activation only.</summary>
		// Token: 0x04002DBC RID: 11708
		DisableActivateAsActivator = 32768,
		/// <summary>Enables activate-as-activator (AAA) activations for this activation only.</summary>
		// Token: 0x04002DBD RID: 11709
		EnableActivateAsActivator = 65536,
		/// <summary>Allows the downloading of code from the Directory Service or the Internet.</summary>
		// Token: 0x04002DBE RID: 11710
		EnableCodeDownload = 8192,
		/// <summary>Begin this activation from the default context of the current apartment.</summary>
		// Token: 0x04002DBF RID: 11711
		FromDefaultContext = 131072,
		/// <summary>The code that manages objects of this class is an in-process handler.</summary>
		// Token: 0x04002DC0 RID: 11712
		InProcessHandler = 2,
		/// <summary>Not used.</summary>
		// Token: 0x04002DC1 RID: 11713
		InProcessHandler16 = 32,
		/// <summary>The code that creates and manages objects of this class is a DLL that runs in the same process as the caller of the function specifying the class context.</summary>
		// Token: 0x04002DC2 RID: 11714
		InProcessServer = 1,
		/// <summary>Not used.</summary>
		// Token: 0x04002DC3 RID: 11715
		InProcessServer16 = 8,
		/// <summary>The EXE code that creates and manages objects of this class runs on same machine but is loaded in a separate process space.</summary>
		// Token: 0x04002DC4 RID: 11716
		LocalServer = 4,
		/// <summary>Disallows the downloading of code from the Directory Service or the Internet.</summary>
		// Token: 0x04002DC5 RID: 11717
		NoCodeDownload = 1024,
		/// <summary>Specifies whether activation fails if it uses custom marshaling.</summary>
		// Token: 0x04002DC6 RID: 11718
		NoCustomMarshal = 4096,
		/// <summary>Overrides the logging of failures.</summary>
		// Token: 0x04002DC7 RID: 11719
		NoFailureLog = 16384,
		/// <summary>A remote machine context.</summary>
		// Token: 0x04002DC8 RID: 11720
		RemoteServer = 16,
		/// <summary>Not used.</summary>
		// Token: 0x04002DC9 RID: 11721
		Reserved1 = 64,
		/// <summary>Not used.</summary>
		// Token: 0x04002DCA RID: 11722
		Reserved2 = 128,
		/// <summary>Not used.</summary>
		// Token: 0x04002DCB RID: 11723
		Reserved3 = 256,
		/// <summary>Not used.</summary>
		// Token: 0x04002DCC RID: 11724
		Reserved4 = 512,
		/// <summary>Not used.</summary>
		// Token: 0x04002DCD RID: 11725
		Reserved5 = 2048
	}
}
