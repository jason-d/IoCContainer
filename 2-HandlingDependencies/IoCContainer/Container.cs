using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoCContainer._2_HandlingDependencies.IoCContainer
{
	public class Container
	{
		private readonly List<Registration> _registrations;

		public Container()
		{
			_registrations = new List<Registration>();
		}

		public void Register<T, U>()
			where U : class
		{
			var abstractType = typeof(T);
			var concreteType = typeof(U);

			var registration = new Registration()
			{
				AbstractType = abstractType,
				ConcreteType = concreteType
			};

			_registrations.Add(registration);
		}

		public T Resolve<T>()
		{
			var requestedType = typeof(T);

			return (T)Resolve(requestedType);
		}

		private object Resolve(Type requestedType)
		{
			var registration = GetRegistration(requestedType);

			object instance = null;
			if (registration != null)
			{
				var constructorParameters = ResolveConstructorDependencies(registration.ConcreteType);

				instance = Activator.CreateInstance(registration.ConcreteType, 
					constructorParameters.ToArray());
			}

			return instance;
		}

		private Registration GetRegistration(Type type)
		{
			Registration registration = null;

			if (type.IsInterface)
			{
				registration = _registrations.Where(reg => reg.AbstractType == type)
											 .FirstOrDefault();
			}
			else
			{
				registration = _registrations.Where(reg => reg.ConcreteType == type)
											 .FirstOrDefault();
			}

			return registration;
		}

		private List<object> ResolveConstructorDependencies(Type type)
		{
			var constructorInfo = type.GetConstructors().First();
			var parameterInfos = constructorInfo.GetParameters();

			var constructorParameters = new List<object>();

			foreach (var parameterInfo in parameterInfos)
			{
				object parameterInstance = Resolve(parameterInfo.ParameterType);

				constructorParameters.Add(parameterInstance);
			}

			return constructorParameters;
		}
	}
}
