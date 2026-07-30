using System;
using System.Runtime.CompilerServices;

namespace System
{
	/// <summary>Encapsulates a method that has one parameter and returns a value of the type specified by the <paramref name="TResult" /> parameter.</summary>
	/// <returns>The return value of the method that this delegate encapsulates.</returns>
	/// <param name="arg">The parameter of the method that this delegate encapsulates.</param>
	/// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates.This type parameter is contravariant. That is, you can use either the type you specified or any type that is less derived. For more information about covariance and contravariance, see Covariance and Contravariance in Generics.</typeparam>
	/// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.This type parameter is covariant. That is, you can use either the type you specified or any type that is more derived. For more information about covariance and contravariance, see Covariance and Contravariance in Generics.</typeparam>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000111 RID: 273
	// (Invoke) Token: 0x060009D1 RID: 2513
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	public delegate TResult Func<in T, out TResult>(T arg);
}
