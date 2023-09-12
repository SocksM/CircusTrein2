using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Logic
{
    public class TrainManager //manager als in persoon die treinen managed
    {
        private List<List<Cart>> Trains = new List<List<Cart>>();

        public List<Cart> GetBestTrain()
        {
            int index = -1;
            int best = 0;
            for (int i = 0; i < Trains.Count; i++)
            {
                if (i == 0)
                {
                    best = Trains[0].Count;
                    index = i;
                }
                if (best > Trains[i].Count)
                {
                    best = Trains[i].Count;
                    index = i;
                }
            }
            if (index == -1)
            {
                return null;
            }
            Console.WriteLine($"Train index used: {index}");
            return Trains[index];
        }

        public void PrintTrains ()
        {
            foreach (List<Cart> Train in Trains)
            {
                Console.WriteLine($"\n\nAmount of carts in sort: {Train.Count}");
                foreach (Cart Cart in Train)
                {
                    Console.WriteLine("\nCart Content:");
                    foreach (Animal Animal in Cart.GetAnimals())
                    {
                        Console.WriteLine(Animal.ToString());
                    }
                }
            }
        }

        private List<List<Animal>> SortIntoLists(List<Animal> NonSortedAnimals)
        {
            List<List<Animal>> ListsOfSortedAnimals = new List<List<Animal>> { };
            ListsOfSortedAnimals.Add(NonSortedAnimals.OrderBy(x => x.GetSize()).ThenBy(x => x.GetDietType()).ToList());
            //ListsOfSortedAnimals.Add(NonSortedAnimals.OrderByDescending(x => x.GetSize()).ThenBy(x => x.GetDietType()).ToList());
            //ListsOfSortedAnimals.Add(NonSortedAnimals.OrderByDescending(x => x.GetSize()).ThenByDescending(x => x.GetDietType()).ToList());
            //ListsOfSortedAnimals.Add(NonSortedAnimals.OrderBy(x => x.GetDietType()).ThenBy(x => x.GetSize()).ToList());
            //ListsOfSortedAnimals.Add(NonSortedAnimals.OrderByDescending(x => x.GetDietType()).ThenBy(x => x.GetSize()).ToList());
            ListsOfSortedAnimals.Add(NonSortedAnimals.OrderByDescending(x => x.GetDietType()).ThenByDescending(x => x.GetSize()).ToList());
            return ListsOfSortedAnimals;  
        }

        public void InsertAnimalsIntoCarts(List<Animal> NonSortedAnimals)
        {
            List<List<Animal>> ListsOfSortedAnimals = SortIntoLists(NonSortedAnimals);
            foreach (List<Animal> sort in ListsOfSortedAnimals)
            {
                List<Cart> train = new List<Cart>();
                while (sort.Count > 0)
                {
                    train.Add(new Cart());
                    for (int i = 0; i < train.Count; i++)
                    {
                        for (int j = 0; j < sort.Count; j++)
                        {
                            if (train[i].AddAnimalIfPossible(sort[j]))
                            {
                                sort.Remove(sort[j]);
                                j -= 1;
                            }
                            if (train[i].RoomLeft() <= 0) break;
                        }
                    }
                }
                Trains.Add(train);
            }
        }
    }
}