using System;
using System.Collections.Generic;
using System.Text;

namespace SupplierService.Domain.Entities
{
    public class Supplier
    {

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Phone { get; private set; }

        public string Address { get; private set; }

        private Supplier()
        {
        }

        public Supplier(
            string name,
            string email,
            string phone,
            string address)
        {
            Validate(name, email, phone, address);

            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
        }

        public void Update(
            string name,
            string email,
            string phone,
            string address)
        {
            Validate(name, email, phone, address);

            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
        }

        private static void Validate(
            string name,
            string email,
            string phone,
            string address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Supplier name is required.");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Supplier email is required.");

            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Supplier phone is required.");

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Supplier address is required.");
        }

    }
}
