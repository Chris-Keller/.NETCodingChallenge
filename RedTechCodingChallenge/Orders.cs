namespace orders
{
    using System;

    public class Order
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string CustomerName { get; set; }
        public string Date { get; set; }
        public string CreatedBy { get; set; }

        public Order(int id, string type, string customerName, string date, string createdBy)
        {
            Id = id;
            Type = type;
            CustomerName = customerName;
            Date = date;
            CreatedBy = createdBy;
        }

        public int GetId()
        {
            return Id;
        }

        public string GetType()
        {
            return Type;
        }

        public string GetCustomerName()
        {
            return CustomerName;
        }

        public string GetDate()
        {
            return Date;
        }

        public string GetCreatedBy()
        {
            return CreatedBy;
        }

        public void ShowOrder()
        {
            Console.WriteLine($"ID: {Id}, Type: {Type}, Customer: {CustomerName}, Date: {Date}, Created By: {CreatedBy}");
        }
    }
}
