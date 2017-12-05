
using System.Data.Entity;

namespace Chatbot.Plugins.DeliveryService.Models
{
    class Order
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string AddressStreet1 { get; set; }
        public string AddressStreet2 { get; set; }
        public string PhoneNumber { get; set; }

        class OrderDbContext : DbContext
        {

        }
    }
}
