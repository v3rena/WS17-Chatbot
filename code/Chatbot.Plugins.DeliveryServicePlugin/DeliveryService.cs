using Chatbot.BusinessLayer.Models;
using Chatbot.Common.Interfaces;
using Chatbot.Plugins.DeliveryService.Models;
using Chatbot.Plugins.DeliveryServicePlugin;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.Caching;
using System;

namespace Chatbot.Plugins.DeliveryService
{
    public class DeliveryService : IPlugin
    {
        private static readonly string HANDLE_PATTERN = @"^.*(?:order something|please order|want to order|delivery service plugin|submit order).*$";
        private static readonly Regex HANDLE_REGEX = new Regex(HANDLE_PATTERN, RegexOptions.IgnoreCase);

        private static readonly string EXTRACT_ORDER_PATTERN = @"^[a-zA-Z\s]*:\s*((?:\d+[a-zA-ZäöüÄÖÜ\s]+(?:\([a-zA-ZäöüÄÖÜ\s]*\))?,\s*)+)\s*to:\s*([a-zA-ZäöüÄÖÜß\-\s]+,[a-zA-Z0-9äöüÄÖÜß\-\s]+,[a-zA-ZäöüÄÖÜ0-9ß\-\s]+)$";
        private static readonly Regex EXTRACT_ORDER_REGEX = new Regex(EXTRACT_ORDER_PATTERN, RegexOptions.IgnoreCase);

        private static readonly string EXTRACT_POSITION_PATTERN = @"^([\d]+)\s([a-zA-ZäöüÄÖÜ\s]+)\s*\(?([a-zA-ZäöüÄÖÜ,\s]*)\)?$";
        private static readonly Regex EXTRACT_POSITION_REGEX = new Regex(EXTRACT_POSITION_PATTERN, RegexOptions.IgnoreCase);

        public string Name => "DeliveryService";

        public float CanHandle(Message message)
        {
            if (HANDLE_REGEX.IsMatch(message.Content))
            {
                return 1.0f;
            }
            return 0.0f;
        }

        public IDictionary<string, string> EnsureDefaultConfiguration(IDictionary<string, string> configuration)
        {
            return configuration;
        }
    
        public void RefreshConfiguration(IDictionary<string, string> configuration)
        {
            
        }

        public Message Handle(Message message)
        {
            Message response = new Message();
            response.SessionKey = message.SessionKey;

            if (message.Content.Contains("delivery service plugin"))
            {
                response.Content = "Usage of the Delivery Service Plugin:\n\n[...] order: [amount] [product] [|(extras)], [amount] [product] [|(extras)], ..., to: [name], [street], [zip city]";
                return response;
            }

            Order order;
            ObjectCache cache = MemoryCache.Default;

            if ("submit order".Equals(message.Content))
            {
                order = (Order) cache[message.SessionKey.ToString()];
                if (order == null)
                {
                    response.Content = "No order found or order expired!";
                    return response;
                }
                DeliveryServiceRepository dal = new DeliveryServiceRepository(new DeliveryServiceContext());
                dal.Create(order);
                response.Content = "Your order was successfully submitted!";
                return response;
            }

            Match match = EXTRACT_ORDER_REGEX.Match(message.Content);
            string rawOrder = match.Groups[1].Value;

            string rawAddress = match.Groups[2].Value;
            string[] addressLines = rawAddress.Split(',');

            if (addressLines.Length != 3)
            {
                response.Content = "Error";
                return response;
            }

            OrderAddress address = new OrderAddress();
            address.Name = addressLines[0].Trim();
            address.AddressStreet1 = addressLines[1].Trim();
            address.AddressStreet2 = addressLines[2].Trim();

            OrderWrapper orderWrapper = new OrderWrapper(address);

            string[] positions = rawOrder.Split(',');
            Match position;
            OrderPosition orderPosition;
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = positions[i].Trim();
                if (string.IsNullOrEmpty(positions[i]))
                {
                    continue;
                }
                position = EXTRACT_POSITION_REGEX.Match(positions[i]);
                orderPosition = new OrderPosition(position.Groups[2].Value, int.Parse(position.Groups[1].Value));
                if (position.Groups[3] != null)
                {
                    orderPosition.Comment = position.Groups[3].Value;
                }
                orderWrapper.Positions.Add(orderPosition);
            }

            order = new Order();
            order.SessionKey = message.SessionKey.Key;
            order.SetOrder(orderWrapper);

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(2.0);
            cache.Set(message.SessionKey.ToString(), order, policy);
            
            response.Content = "DeliveryService Plugin TRIGGERED!\n*autistic screeching*REEEEEEEEEEEE!!!!\n\n++++++++++++++++++++++++++++++\n\n" + order.OrderJsonData + "\n\nTo submit your order, please type \"submit order\"!";
            return response;
        }
    }
}
