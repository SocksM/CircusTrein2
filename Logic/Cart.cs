using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
	public class Cart
	{
		private List<Animal> Animals = new List<Animal>();
		private const int MaxCapacity = 10;

		// ALLEEN PUBLIC VOOR UNIT TESTS 
		public bool IsThereEnoughRoomFor(Animal Animal) => 0 <= RoomLeft() - (int)Animal.Size;

		public int RoomLeft()
		{
			int RoomLeft = MaxCapacity;
			foreach (Animal Animal in Animals)
			{
				RoomLeft -= (int)Animal.Size;
			}
			return RoomLeft;
		}
		public List<Animal> GetAnimals() => Animals;

		public void AddAnimal(Animal Animal)
		{
			if (!CanBeInCart(Animal))
			{
				throw new InvalidOperationException("Animal cant be in this cart. Did you check if it could be in this cart?");
			}
			Animals.Add(Animal);
		}
		public bool CanBeInCart(Animal Animal)
		{
			if (Animals.Count == 0)
			{
				return true;
			}
			if (IsThereEnoughRoomFor(Animal))
			{
				foreach (Animal animal in Animals)
				{
					if (!Animal.CanIBeWith(animal))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		public bool TryAddAnimal(Animal Animal)
		{
			if (Animals.Count == 0)
			{
				Animals.Add(Animal);
				return true;
			}
			if (IsThereEnoughRoomFor(Animal))
			{
				foreach (Animal animal in Animals)
				{
					if (!Animal.CanIBeWith(animal))
					{
						return false;
					}
				}
				Animals.Add(Animal);
				return true;
			}
			return false;
		}

		public override string ToString()
		{
			string ReturnString = string.Empty;
			foreach (Animal Animal in Animals)
			{
				ReturnString += $"{Animal}, ";
			}
			return ReturnString;
		}

		public void AddAnimalNoRestictions_ONLY_FOR_UNIT_TESTS(Animal Animal) => Animals.Add(Animal);
	}
}