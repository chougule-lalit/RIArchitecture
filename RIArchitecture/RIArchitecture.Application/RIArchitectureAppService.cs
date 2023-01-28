using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RIArchitecture.Application.Contracts;
using RIArchitecture.Infrastructure.RIArchitectureDomainService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIArchitecture.Application
{
    public abstract class RIArchitectureAppService : IRIArchitectureAppService, ITransientDependency
    {
        private IMapper _mapper;

        protected RIArchitectureAppService(
            )
        {
        }

        protected IMapper ObjectMapper => ObjectMapperRegistration(out _mapper);

        private IMapper ObjectMapperRegistration(out IMapper mapper)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RIArchitectureAutoMapperProfile>();
            });

            mapper = config.CreateMapper();
            return mapper;
        }
    }
}
