using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace Logic
{
    public class TrainManager
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
            Debug.WriteLine($"Train index used: {index}");
            return Trains[index];
        }

        [Conditional("DEBUG")]
        public void PrintTrains ()
        {
            foreach (List<Cart> Train in Trains)
            {
                Debug.WriteLine($"\n\nAmount of carts in sort: {Train.Count}");
                foreach (Cart Cart in Train)
                {
                    Debug.WriteLine("\nCart Content:");
                    foreach (Animal Animal in Cart.GetAnimals())
                    {
                        Debug.WriteLine(Animal.ToString());
                    }
                }
            }
        }

        private List<List<Animal>> SortIntoLists(List<Animal> NonSortedAnimals)
        {
            List<List<Animal>> ListsOfSortedAnimals = new List<List<Animal>> { };
            ListsOfSortedAnimals.Add(NonSortedAnimals.OrderBy(animal => animal.Size).ThenBy(animal => animal.DietType).ToList());
            //ListsOfSortedAnimals.Add(NonSortedAnimals.OrderByDescending(animal => animal.GetSize()).ThenBy(animal => animal.GetDietType()).ToList());
            //ListsOfSortedAnimals.Add(NonSortedAnimals.OrderByDescending(animal => animal.GetSize()).ThenByDescending(animal => animal.GetDietType()).ToList());
            //ListsOfSortedAnimals.Add(NonSortedAnimals.OrderBy(animal => animal.GetDietType()).ThenBy(animal => animal.GetSize()).ToList());
            //ListsOfSortedAnimals.Add(NonSortedAnimals.OrderByDescending(animal => animal.GetDietType()).ThenBy(animal => animal.GetSize()).ToList());
            ListsOfSortedAnimals.Add(NonSortedAnimals.OrderByDescending(animal => animal.DietType).ThenByDescending(x => x.Size).ToList());
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
                    train[train.Count - 1].TryAddAnimal(sort[0]);
                    sort.RemoveAt(0);
                    for (int i = 0; i < train.Count; i++)
                    {
                        for (int j = 0; j < sort.Count; j++)
                        {
                            if (train[i].TryAddAnimal(sort[j]))
                            {
                                sort.RemoveAt(j);
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