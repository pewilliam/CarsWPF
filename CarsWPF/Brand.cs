namespace CarsWPF
{
    internal class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Brand(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
