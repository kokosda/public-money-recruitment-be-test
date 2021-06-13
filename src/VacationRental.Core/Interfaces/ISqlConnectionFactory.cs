using System.Data;

namespace VacationRental.Core.Interfaces
{
	public interface ISqlConnectionFactory
	{
		IDbConnection GetOpenConnection();
	}
}
