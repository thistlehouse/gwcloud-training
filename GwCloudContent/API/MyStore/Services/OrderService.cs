using MyStore.Contracts.OrderDto;
using MyStore.Contracts.OrderProductDto;
using MyStore.Models;
using MyStore.Repositories.Interfaces;

namespace MyStore.Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;
        IClientRepository _clientRepository;
        IProductRepository _productRepository;        

        public OrderService(IOrderRepository orderRepository, 
            IClientRepository clientRepository, 
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
        }

        public Order GetOrderById(Guid id)
        {
            return _orderRepository.GetOrderById(id);
        }

        public Order CreateOrder(Order request)
        {
            List<OrderProduct> orderProducts = new();
            
            Order order = new Order
            {
                Id = Guid.NewGuid(),
                ClientId = request.ClientId,
                OrderProducts = orderProducts,
                TotalToPay = request.TotalToPay
            };             

            foreach (OrderProduct op in request.OrderProducts)
            {
                Product product = _productRepository.GetById(op.ProductId);                

                OrderProduct orderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = op.Quantity,
                    Total = product.Price * op.Quantity
                };                
                
                orderProducts.Add(orderProduct);
            }

            _orderRepository.CreateOrder(order);
            
            return order;
        }

        public Order UpSertProduct(OrderProduct request)
        {
            if (request is null)
                Console.WriteLine($"Treat error for {request} is null");
                
            Order order = new Order();

            try
            {
                order = _orderRepository.GetOrderById(request.OrderId);                
                bool isProductInOrder = productExits(order, request);                
                OrderProduct orderProduct = new OrderProduct();

                Product product = order.OrderProducts
                    .Find(op => op.OrderId == request.OrderId).Product;

                if (isProductInOrder)
                {                    
                    orderProduct = order.OrderProducts
                        .Find(op =>
                            op.OrderId == request.OrderId &&
                            op.ProductId == request.ProductId);

                    if (request.Quantity == 0) order.OrderProducts.Remove(orderProduct);                                
                    else
                    {
                        orderProduct.Quantity += request.Quantity;
                        orderProduct.Total = orderProduct.Quantity * product.Price;
                    }
                }
                else
                {
                    orderProduct.OrderId = request.OrderId;
                    orderProduct.ProductId = request.ProductId;
                    orderProduct.Quantity = request.Quantity;
                    orderProduct.Total = request.Quantity * product.Price;

                    order.OrderProducts.Add(orderProduct);
                }

                order.TotalToPay = order.OrderProducts.Sum(op => op.Total);
                
                _orderRepository.Save();
                
            }
            catch (Exception e)
            {
               Console.WriteLine($"Something went wrong: {e.Message}");
            }
                       
            return order;
        }

        public Order RemoveProduct(OrderProduct request)
        {
            Order order = new Order();
            
            try
            {
                order = _orderRepository.GetOrderById(request.OrderId);                
                Product product = _productRepository.GetById(request.ProductId); 
                bool isProductInOrder = productExits(order, request);
            
                if (!isProductInOrder)
                    Console.WriteLine("You don't have this product in your order.");
                else
                {
                    OrderProduct removeProduct = order.OrderProducts
                        .Find(op => 
                            op.OrderId == request.OrderId &&
                            op.ProductId == product.Id);
                    
                    order.OrderProducts.Remove(removeProduct);
                    
                    order.TotalToPay = order.OrderProducts.Sum(op => op.Total);                
                    
                    _orderRepository.Save();                
                }
            }
            catch (Exception e)
            {                
                Console.WriteLine($"Something went wrong: {e.Message}");
            }

            return order;
        }

        private bool productExits(Order order, OrderProduct request)
        {
            return order.OrderProducts
                .Any(op => op.ProductId == request.ProductId);
        }
    }
}