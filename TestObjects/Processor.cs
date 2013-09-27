using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoCContainer.TestObjects
{
	public class Processor
	{
		private readonly ICounter _counter;
		private readonly IRepository _repository;

		public Processor(ICounter counter, IRepository repository)
		{
			_counter = counter;
			_repository = repository;
		}

		public ICounter Counter { get { return _counter; } }

		public IRepository Repository { get { return _repository; } }
	}
}
