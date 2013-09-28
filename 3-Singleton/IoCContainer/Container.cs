using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoCContainer._3_Singleton.IoCContainer
{
	public class Container
	{
		private readonly List<Registration> _registrations;

		public Container()
		{
			_registrations = new List<Registration>();
		}

		public void Register<T, U>(bool isSingleton = false)
			where U : class
		{
			var abstractType = typeof(T);
			var concreteType = typeof(U);

			var registration = new Registration()
			{
				AbstractType = abstractType,
				ConcreteType = concreteType,
				IsSingleton = isSingleton
			};

			_registrations.Add(registration);
		}

		public T Resolve<T>()
			where T : class
		{
			var requestedType = typeof(T);

			return Resolve(requestedType) as T;
		}

		private object Resolve(Type requestedType)
		{
			var registration = _registrations.Where(reg => reg.ConcreteType == requestedType)
											 .FirstOrDefault();

			object instance = null;
			if (registration != null)
			{
				instance = registration.Instance;

				if (instance == null)
				{
					var constructorParameters = ResolveConstructorDependencies(registration.ConcreteType);

					instance = Activator.CreateInstance(registration.ConcreteType, constructorParameters.ToArray());

					if (registration.IsSingleton)
					{
						registration.Instance = instance;
					}
				}
			}

			return instance;
		}

		private List<object> ResolveConstructorDependencies(Type type)
		{
			var constructorInfo = type.GetConstructors().First();
			var parameterInfos = constructorInfo.GetParameters();

			var constructorParameters = new List<object>();

			foreach (var parameterInfo in parameterInfos)
			{
				var registration = _registrations.Where(
					reg => reg.AbstractType == parameterInfo.ParameterType).FirstOrDefault();

				object parameterInstance = Resolve(registration.ConcreteType);

				constructorParameters.Add(parameterInstance);
			}

			return constructorParameters;
		}
	}
}
