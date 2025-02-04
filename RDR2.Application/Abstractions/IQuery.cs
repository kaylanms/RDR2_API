using MediatR;

namespace RDR2.Application.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>;