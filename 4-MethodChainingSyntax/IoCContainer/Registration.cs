using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoCContainer._4_MethodChainingSyntax.IoCContainer
{
	public class Registration
	{
		public Type AbstractType { get; set; }

		public Type ConcreteType { get; set; }

		public bool IsSingleton { get; set; }

		public object Instance { get; set; }
	}
}
