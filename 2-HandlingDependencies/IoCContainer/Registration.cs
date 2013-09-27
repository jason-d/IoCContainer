using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoCContainer._2_HandlingDependencies.IoCContainer
{
	public class Registration
	{
		public Type AbstractType { get; set; }
		public Type ConcreteType { get; set; }
	}
}
