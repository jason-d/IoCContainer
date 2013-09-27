using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IoCContainer.TestObjects;
using IoCContainer._1_SimpleContainer.IoCContainer;
using NUnit.Framework;

namespace IoCContainer._1_SimpleContainer.Tests
{
	[TestFixture]
	class ContainerTests
	{
		[Test]
		public void Should_return_an_instance_of_a_registered_abstract_type()
		{
			var container = new Container();
			container.Register<ICounter, Counter>();

			var counter = container.Resolve<Counter>();

			Assert.IsInstanceOf<Counter>(counter);
		}

		[Test]
		public void Should_return_an_instance_of_a_registered_concrete_type()
		{
			var container = new Container();
			container.Register<Counter, Counter>();

			var counter = container.Resolve<Counter>();

			Assert.IsInstanceOf<Counter>(counter);
		}
	}
}
