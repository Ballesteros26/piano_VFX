using System;
using System.Runtime.CompilerServices;

namespace System
{
	/// <summary>Encapsulates a method that has no parameters and returns a value of the type specified by the <paramref name="TResult" /> parameter.</summary>
	/// <returns>The return value of the method that this delegate encapsulates.</returns>
	/// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.This type parameter is covariant. That is, you can use either the type you specified or any type that is more derived. For more information about covariance and contravariance, see Covariance and Contravariance in Generics.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000110 RID: 272
	// (Invoke) Token: 0x060009CD RID: 2509
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	public delegate TResult Func<out TResult>();
}
