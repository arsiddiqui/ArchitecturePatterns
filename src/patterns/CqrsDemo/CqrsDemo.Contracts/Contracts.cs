namespace CqrsDemo.Contracts;

public sealed record PlaceOrderCommand(string CustomerId, decimal Total);
public sealed record OrderCreatedResponse(Guid OrderId);

public sealed record GetOrderQuery(Guid OrderId);
public sealed record OrderReadDto(Guid Id, string CustomerId, decimal Total, string Status);
