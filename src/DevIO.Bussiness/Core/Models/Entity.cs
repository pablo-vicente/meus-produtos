using System;

namespace DevIO.Bussiness.Core.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = new Guid();
        }
        public Guid Id { get; set; }
    }
}