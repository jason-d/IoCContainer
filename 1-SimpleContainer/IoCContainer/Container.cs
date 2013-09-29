using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoCContainer._1_SimpleContainer.IoCContainer
{
	public class Container
	{
		private readonly List<Registration> _registrations;

		public Container()
		{
			_registrations = new List<Registration>();
		}

		public void Register<T, U>()
			where U : class, new()
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

			Registration registration = GetRegistration(requestedType);

			object instance = null;
			if (registration != null)
			{
				instance = Activator.CreateInstance(registration.ConcreteType);
			}

			return (T)instance;
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
	}
}
