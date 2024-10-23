using Balta.Domain.SharedContext.Results;
using MediatR;

namespace Balta.Application.SharedContext.UseCases.Abstractions;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
    where TResponse : IQueryResponse
{
}