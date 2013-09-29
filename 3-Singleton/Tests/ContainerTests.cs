using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IoCContainer.TestObjects;
using IoCContainer._3_Singleton.IoCContainer;
using NUnit.Framework;

namespace IoCContainer._3_Singleton.Tests
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

		[Test]
		public void Should_return_an_instance_for_a_requested_abstract_type()
		{
			var container = new Container();
			container.Register<ICounter, Counter>();

			var counter = container.Resolve<ICounter>();

			Assert.IsInstanceOf<Counter>(counter);
		}

		[Test]
		public void Should_return_null_for_an_unregistered_type()
		{
			var container = new Container();

			var counter = container.Resolve<Counter>();

			Assert.IsNull(counter);
		}

		[Test]
		public void Should_resolve_dependencies()
		{
			var container = new Container();
			container.Register<ICounter, Counter>();
			container.Register<IRepository, Repository>();
			container.Register<Processor, Processor>();

			var processor = container.Resolve<Processor>();

			Assert.IsInstanceOf<Processor>(processor);
			Assert.IsInstanceOf<Counter>(processor.Counter);
			Assert.IsInstanceOf<Repository>(processor.Repository);
		}

		[Test]
		public void Should_return_different_objects_if_not_a_singleton()
		{
			var container = new Container();
			container.Register<ICounter, Counter>();

			var counterA = container.Resolve<Counter>();
			var counterB = container.Resolve<Counter>();

			Assert.AreNotSame(counterA, counterB);
		}

		[Test]
		public void Should_return_the_same_object_if_a_singleton()
		{
			var container = new Container();
			container.Register<ICounter, Counter>(isSingleton: true);

			var counterA = container.Resolve<Counter>();
			var counterB = container.Resolve<Counter>();

			Assert.AreSame(counterA, counterB);
		}
	}
}
