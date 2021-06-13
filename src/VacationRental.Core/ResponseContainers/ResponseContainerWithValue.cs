using VacationRental.Core.Interfaces;

namespace VacationRental.Core.ResponseContainers
{
	public sealed class ResponseContainerWithValue<T> : ResponseContainer, IResponseContainerWithValue<T>
	{
		public T Value { get; private set; }

		public void SetSuccessValue(T value)
		{
			Value = value;
			IsSuccess = true;
		}

		public new IResponseContainerWithValue<T> JoinWith(IResponseContainer anotherResponseContainer)
		{
			base.JoinWith(anotherResponseContainer);
			return this;
		}
	}
}
