using System.Threading;
using System.Threading.Tasks;

namespace DominicanWhoCodes.Shared.Domain
{
    public interface IUnitOfWork
    {
        Task<bool> CommitChanges(CancellationToken cancellationToken = default);
    }
}