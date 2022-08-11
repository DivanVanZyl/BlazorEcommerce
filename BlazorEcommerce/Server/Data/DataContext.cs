namespace BlazorEcommerce.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "The Cuckoo's Egg 🐒",
                    Description = "The Cuckoo's Egg: Tracking a Spy Through the Maze of Computer Espionage is a 1989 book written by Clifford Stoll. It is his first-person account of the hunt for a computer hacker who broke into a computer at the Lawrence Berkeley National Laboratory (LBNL).",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/2/28/The_Cuckoo%27s_Egg.jpg",
                    Price = 9.99m
                },
                new Product
                {
                    Id = 2,
                    Title = "Don Quixote",
                    Description = "Don Quixote (/ˌdɒn kiːˈhoʊti/, also US: /-teɪ/)[1] is a Spanish epic novel by Miguel de Cervantes. Originally published in two parts, in 1605 and 1615, its full title is The Ingenious Gentleman Don Quixote of La Mancha or, in Spanish, El ingenioso hidalgo (or caballero, in Part 2) don Quijote de la Mancha.[a] A founding work of Western literature, it is often labelled as the first modern novel[2][3] and one of the greatest works ever written.[4][5] Don Quixote is also one of the most-translated books in the world.[6]",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Title_page_first_edition_Don_Quijote.jpg/375px-Title_page_first_edition_Don_Quijote.jpg",
                    Price = 19.99m
                },
                new Product
                {
                    Id = 3,
                    Title = "Down and Out in Paris and London",
                    Description = "Down and Out in Paris and London is the first full-length work by the English author George Orwell, published in 1933. It is a memoir[2] in two parts on the theme of poverty in the two cities. Its target audience was the middle- and upper-class members of society—those who were more likely to be well educated—and exposes the poverty existing in two prosperous cities: Paris and London. The first part is an account of living in near-extreme poverty destitution in Paris and the experience of casual labour in restaurant kitchens. The second part is a travelogue of life on the road in and around London from the tramp's perspective, with descriptions of the types of hostel accommodation available and some of the characters to be found living on the margins.",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/0/06/Downout_paris_london.jpg",
                    Price = 29.99m
                });
        }

        public DbSet<Product> Products { get; set; }
    }
}
