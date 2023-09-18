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
        public Size Size { get; private set; }
        public DietType DietType { get; private set; }
        public Animal(Size size, DietType dietType)
        {
            Size = size;
            DietType = dietType;
        }
        private bool DoIEat(Animal animal)
        {
            if (DietType == DietType.Herbivore) return false;
            if ((int)Size < (int)animal.Size) 
            {
                return false;
            }
            return true;
        }
        private bool DoIGetEatenBy(Animal animal)
        {
            if (animal.DietType == DietType.Herbivore) return false;
            if ((int)Size > (int)animal.Size) return false;
            return true;
        }
        public bool CanIBeWith(Animal animal) => !DoIGetEatenBy(animal) && (DoIEat(animal) ? false : true);

        public override string ToString() => $"{Size} {DietType}"; // moet dit ook in uml???
    }
}
