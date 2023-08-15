namespace Orders.Application.Features.Commands.CreateOrder
{
    internal class CreateOrderCommandResult
    {
        public string? PositiveMessage { get; set; }
        public string? ErrorDescription { get; set; }
    }
}
