using System;

namespace OnlineOrderingSystem
{
    public class Address
    {
        private string streetAddress;
        private string city;
        private string stateProvince;
        private string country;

        public Address(string streetAddress, string city, string stateProvince, string country)
        {
            this.streetAddress = streetAddress;
            this.city = city;
            this.stateProvince = stateProvince;
            this.country = country;
        }

        public bool IsInUSA()
        {
            return country.Equals("USA", StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return $"{streetAddress}\n{city}, {stateProvince}\n{country}";
        }
    }

    public class Customer
    {
        private string name;
        private Address address;

        public Customer(string name, Address address)
        {
            this.name = name;
            this.address = address;
        }

        public bool IsInUSA()
        {
            return address.IsInUSA();
        }

        public string Name => name;

        public Address Address => address;
    }

    public class Product
    {
        private string name;
        private string productId;
        private decimal price;
        private int quantity;

        public Product(string name, string productId, decimal price, int quantity)
        {
            this.name = name;
            this.productId = productId;
            this.price = price;
            this.quantity = quantity;
        }

        public decimal TotalCost()
        {
            return price * quantity;
        }

        public string Name => name;
        public string ProductId => productId;
    }

    public class Order
    {
        private List<Product> products;
        private Customer customer;

        public Order(Customer customer)
        {
            this.customer = customer;
            this.products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public decimal CalculateTotalCost()
        {
            decimal totalCost = 0;
            foreach (var product in products)
            {
                totalCost += product.TotalCost();
            }

            if (customer.IsInUSA())
            {
                totalCost += 5;
            }
            else
            {
                totalCost += 35;
            }

            return totalCost;
        }

        public string GetPackingLabel()
        {
            var packingLabel = "Packing Label:\n";
            foreach (var product in products)
            {
                packingLabel += $"Product: {product.Name}, ID: {product.ProductId}\n";
            }
            return packingLabel;
        }

        public string GetShippingLabel()
        {
            return $"Shipping Label:\nCustomer: {customer.Name}\n{customer.Address}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create addresses
            Address address1 = new Address("123 Main St", "Springfield", "IL", "USA");
            Address address2 = new Address("456 Elm St", "Toronto", "ON", "Canada");

            // Create customers
            Customer customer1 = new Customer("John Doe", address1);
            Customer customer2 = new Customer("Jane Smith", address2);

            // Create products
            Product product1 = new Product("Laptop", "P1001", 999.99m, 1);
            Product product2 = new Product("Mouse", "P1002", 19.99m, 2);
            Product product3 = new Product("Keyboard", "P1003", 49.99m, 1);
            Product product4 = new Product("Monitor", "P1004", 199.99m, 1);

            // Create orders and add products
            Order order1 = new Order(customer1);
            order1.AddProduct(product1);
            order1.AddProduct(product2);

            Order order2 = new Order(customer2);
            order2.AddProduct(product3);
            order2.AddProduct(product4);

            // List of orders
            List<Order> orders = new List<Order> { order1, order2 };

            // Display order details
            foreach (var order in orders)
            {
                Console.WriteLine(order.GetPackingLabel());
                Console.WriteLine(order.GetShippingLabel());
                Console.WriteLine($"Total Price: ${order.CalculateTotalCost():F2}\n");
            }
        }
    }
}
