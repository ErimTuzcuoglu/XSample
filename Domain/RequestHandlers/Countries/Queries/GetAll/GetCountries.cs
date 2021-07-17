using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Countries.Queries.GetAll;
using Domain.Entity;
using Domain.Interface.Persistence.Repository;
using MediatR;
using Persistence.Data.UnitOfWork;

namespace Domain.RequestHandlers.Countries.Queries.GetAll
{
    public class GetCountries : IRequestHandler<GetCountriesQuery, IEnumerable<CountryGetViewModel>>
    {
        public IUnitOfWork _uow { get; set; }

        public GetCountries(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<CountryGetViewModel>> Handle(GetCountriesQuery request,
            CancellationToken cancellationToken)
        {
            var countries = (await _uow.CountryRepository.GetAllAsync()).Select(country => new CountryGetViewModel()
            {
                Id = country.Id,
                Name = country.Name
            });
            throw new ArgumentException("sdf");
            return countries;
        }
    }
}