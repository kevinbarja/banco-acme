namespace AcmeBank.Persistence.Entities
{
    public partial class Customer : Person
    {
        public virtual int PersonId { get => base.Id; }
    }
}
