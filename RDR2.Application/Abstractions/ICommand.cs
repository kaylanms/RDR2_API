using MediatR;

namespace RDR2.Application.Abstractions;

public interface ICommand<out TResponse> : IRequest<TResponse>;