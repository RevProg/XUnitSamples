using MyAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPIIntegrationTests.Overrides;

public class MyTestService : IMyService
{
    public int Total { get; set; } = 22;
}
