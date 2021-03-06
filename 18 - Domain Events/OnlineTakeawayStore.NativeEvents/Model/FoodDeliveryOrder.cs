﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTakeawayStore.Domain.EventsKeyword
{
    public class FoodDeliveryOrder
    {
        public event FoodDeliveryOrderCreatedHandler FoodDeliveryOrderCreated;
        public event FoodDeliveryOrderConfirmedHandler FoodDeliveryOrderConfirmed;

        public FoodDeliveryOrder(Guid id, Guid customerId, Guid restuarantId, List<int> menuItemIds, DateTime deliveryTime, 
            FoodDeliveryOrderCreatedHandler creationHandler)
        {
            this.Id = id;
            this.CustomerId = customerId;
            this.RestaurantId = restuarantId;
            this.MenuItemIds = menuItemIds;
            this.RequestedDeliveryTime = deliveryTime;
            
            // subscribe handler to event
            this.FoodDeliveryOrderCreated += creationHandler;

            Status = FoodDeliveryOrderSteps.Pending;

            // raise event
            if (FoodDeliveryOrderCreated != null)
                FoodDeliveryOrderCreated(this);
        }

        public Guid Id { get; private set; }

        public FoodDeliveryOrderSteps Status { get; private set; }

        public Guid CustomerId { get; private set; }

        public Guid RestaurantId { get; private set; }

        public List<int> MenuItemIds { get; private set; }

        public DateTime RequestedDeliveryTime { get; private set; }

        public void Confirm()
        {
            Status = FoodDeliveryOrderSteps.Confirmed;

            if (FoodDeliveryOrderConfirmed != null)
                FoodDeliveryOrderConfirmed(this);
        }

        public void Reject()
        {
            Status = FoodDeliveryOrderSteps.Rejected;
        }
    }

    // also from the Ubiquitous Language
    public enum FoodDeliveryOrderSteps
    {
        Pending,
        Validated,
        Confirmed,
        Cooked,
        Despatched,
        Rejected
    }

    public delegate void FoodDeliveryOrderCreatedHandler(FoodDeliveryOrder order);

    public delegate void FoodDeliveryOrderConfirmedHandler(FoodDeliveryOrder order);
}
