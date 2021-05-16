using System.Collections.Generic;
using Application.Countries.Queries.GetAll;
using MediatR;

namespace Domain.RequestHandlers.Countries.Queries.GetAll
{
    public class GetCountriesQuery : IRequest<IEnumerable<CountryGetViewModel>>
    {
    }
}