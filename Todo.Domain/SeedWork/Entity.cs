using System;

namespace Todo.Domain.SeedWork
{
    public abstract class Entity 
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public bool Equals(Entity other)
        {
            return Id == other.Id;
        }

    }
}