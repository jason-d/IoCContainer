using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IoCContainer.TestObjects;
using IoCContainer._4_MethodChainingSyntax.IoCContainer;
using NUnit.Framework;

namespace IoCContainer._4_MethodChainingSyntax.Tests
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

		[Test]
		public void Should_return_an_instance_with_method_chaining_syntax()
		{
			var container = new Container();
			container.For<ICounter>().Use<Counter>();
			container.For<IRepository>().Use<Repository>();
			container.For<Processor>().Use<Processor>();

			var processor = container.Resolve<Processor>();

			Assert.IsInstanceOf<Processor>(processor);
			Assert.IsInstanceOf<Counter>(processor.Counter);
			Assert.IsInstanceOf<Repository>(processor.Repository);
		}

		[Test]
		public void Should_return_the_same_object_if_a_singleton_with_method_chaining_syntax()
		{
			var container = new Container();
			container.For<ICounter>().Use<Counter>().AsSingleton();

			var counterA = container.Resolve<Counter>();
			var counterB = container.Resolve<Counter>();

			Assert.AreSame(counterA, counterB);
		}
	}
}
