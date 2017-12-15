using Chatbot.DataAccessLayer.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chatbot.Plugins.DeliveryService.Models
{
    public class Order : IEntity
    {
        public Order() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid SessionKey { get; set; }
        public string OrderJsonData { get; set; }

        [NotMapped]
        private OrderWrapper _order;

        public OrderWrapper GetOrder()
        {
            if (_order == null && !string.IsNullOrEmpty(OrderJsonData))
            {
                _order = JsonConvert.DeserializeObject<OrderWrapper>(this.OrderJsonData);
            }
            return _order;
        }

        public void SetOrder(OrderWrapper order)
        {
            if (order != null)
            {
                OrderJsonData = JsonConvert.SerializeObject(order);
                _order = order;
            }
        }
    }

    public class OrderWrapper
    {
        public OrderWrapper() : this(null) { }

        public OrderWrapper(OrderAddress adress)
        {
            Adress = adress;
            Positions = new List<OrderPosition>();
        }

        [JsonProperty(PropertyName = "address")]
        public OrderAddress Adress { get; set; }
        [JsonProperty(PropertyName = "positions")]
        public IList<OrderPosition> Positions { get; set; }
    }
    
    public class OrderAddress
    {
        public OrderAddress() { }
        public OrderAddress(string name, string addressStreet1, string addressStreet2, string phone)
        {
            Name = name;
            AddressStreet1 = addressStreet1;
            AddressStreet2 = addressStreet2;
            PhoneNumber = phone;
        }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "address1")]
        public string AddressStreet1 { get; set; }
        [JsonProperty(PropertyName = "address2")]
        public string AddressStreet2 { get; set; }
        [JsonProperty(PropertyName = "phone")]
        public string PhoneNumber { get; set; }
    }

    public class OrderPosition
    {
        public OrderPosition() { }
        public OrderPosition(string name, int amount) : this(name, amount, null) { }
        public OrderPosition(string name, int amount, string comment)
        {
            Name = name;
            Amount = amount;
            Comment = comment;
        }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }
    }
}
