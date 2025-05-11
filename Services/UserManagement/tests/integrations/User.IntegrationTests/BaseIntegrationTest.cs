using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.IntegrationTests
{
    public abstract class BaseIntegrationTest: IClassFixture<UserIntegrationTestWebFactory>
    {
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;
       protected  BaseIntegrationTest(UserIntegrationTestWebFactory factory )
        { 
            _scope = factory.Services.CreateScope();
            Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        }
    }
}
