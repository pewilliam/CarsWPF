namespace CarsWPF
{
    internal class Cars
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public int Year { get; set; }
        public int QtPassengers { get; set; }
        public string Brand { get; set; }

        public Cars(int id, string name, string color, double price, int year, int qtPassengers, string brand)
        {
            Id = id;
            Name = name;
            Color = color;
            Price = price;
            Year = year;
            QtPassengers = qtPassengers;
            Brand = brand;
        }
    }
}
