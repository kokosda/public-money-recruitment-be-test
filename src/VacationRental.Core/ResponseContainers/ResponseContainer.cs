using VacationRental.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace VacationRental.Core.ResponseContainers
{
	public class ResponseContainer : IResponseContainer
	{
		private bool _isSuccess;

		public bool IsSuccess
		{
			get => _isSuccess;
			protected set => _isSuccess = value;
		}

		public string Messages => string.Join(Environment.NewLine, MessagesList);

		private List<string> MessagesList { get; }

		public ResponseContainer()
		{
			MessagesList = new List<string>();
			IsSuccess = true;
		}

		public void AddMessage(string message)
		{
			MessagesList.Add(message);
		}

		public void AddErrorMessage(string message)
		{
			AddMessage(message);
			_isSuccess = false;
		}

		public IResponseContainer JoinWith(IResponseContainer anotherResponseContainer)
		{
			if (anotherResponseContainer is null)
				anotherResponseContainer = new ResponseContainer();

			_isSuccess = anotherResponseContainer.IsSuccess;

			if (!string.IsNullOrWhiteSpace(anotherResponseContainer.Messages))
				MessagesList.Add(anotherResponseContainer.Messages);

			return this;
		}

		public IResponseContainer AsInterface()
		{
			return this;
		}
	}
}
