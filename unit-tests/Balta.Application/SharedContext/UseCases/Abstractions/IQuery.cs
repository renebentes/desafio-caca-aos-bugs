using Balta.Domain.SharedContext.Results;
using MediatR;

namespace Balta.Application.SharedContext.UseCases.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> where TResponse : IQueryResponse;