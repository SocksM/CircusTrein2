using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Logic
{
    public class Animal
    {
        private Size Size;
        private DietType DietType;
        public Animal(Size size, DietType dietType)
        {
            this.Size = size;
            this.DietType = dietType;
        }
        public Size GetSize() => Size;
        public DietType GetDietType() => DietType;
        private bool DoIEat(Animal animal)
        {
            if (DietType == DietType.Herbivore) return false;
            if ((int)Size < (int)animal.GetSize()) 
            {
                return false;
            }
            return true;
        }
        private bool DoIGetEatenBy(Animal animal)
        {
            if (animal.DietType == DietType.Herbivore) return false;
            if ((int)Size > (int)animal.GetSize()) return false;
            return true;
        }
        public bool CanIBeWith(Animal animal) => DoIGetEatenBy(animal) ? false : (DoIEat(animal) ? false : true);

        public override string ToString() => $"{Size} {DietType}"; // moet dit ook in uml???
    }
}
