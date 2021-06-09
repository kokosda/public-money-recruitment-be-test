namespace VacationRental.Core.Interfaces
{
	public interface IResponseContainerWithValue<out T> : IResponseContainer
	{
		T Value { get; }
	}
}
