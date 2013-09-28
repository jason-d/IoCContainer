using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoCContainer._4_MethodChainingSyntax.IoCContainer
{
	public class RegistrationExpression
	{
		private readonly Registration _registration;

		public RegistrationExpression(Registration registration)
		{
			_registration = registration;
		}

		public RegistrationExpression Use<T>()
			where T : class
		{
			_registration.ConcreteType = typeof(T);

			return this;
		}

		public RegistrationExpression AsSingleton()
		{
			_registration.IsSingleton = true;

			return this;
		}
	}
}
