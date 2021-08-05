namespace VacationRental.Core.Interfaces
{
	public interface IResponseContainerWithValue<T> : IResponseContainer
	{
		T Value { get; }
		
		void SetSuccessValue(T value);
	}
}
