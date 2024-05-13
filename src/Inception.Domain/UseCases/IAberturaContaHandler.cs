using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inception.Core.UseCases;

public interface IAberturaContaHandler
{
    Task Handle(AberturaContaCommand command, CancellationToken cancellationToken = default);
}