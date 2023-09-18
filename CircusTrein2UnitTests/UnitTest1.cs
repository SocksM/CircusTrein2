using Logic;
using System.Diagnostics;

namespace CircusTrein2UnitTests
{
    public class Tests
    {

        static Animal cs = new Animal(Size.Small, DietType.Carnivore);
        static Animal cm = new Animal(Size.Medium, DietType.Carnivore);
        static Animal cl = new Animal(Size.Large, DietType.Carnivore);

        static Animal hs = new Animal(Size.Small, DietType.Herbivore);
        static Animal hm = new Animal(Size.Medium, DietType.Herbivore);
        static Animal hl = new Animal(Size.Large, DietType.Herbivore);

        [SetUp]
        public void Setup() { }

        private static IEnumerable<TestCaseData> CanBeWith_Config()
        {
            yield return new TestCaseData(cs, cs, false);
            yield return new TestCaseData(cs, cm, false);
            yield return new TestCaseData(cs, cl, false);
            yield return new TestCaseData(cs, hs, false);
            yield return new TestCaseData(cs, hm, true);
            yield return new TestCaseData(cs, hl, true);

            yield return new TestCaseData(cm, cs, false);
            yield return new TestCaseData(cm, cm, false);
            yield return new TestCaseData(cm, cl, false);
            yield return new TestCaseData(cm, hs, false);
            yield return new TestCaseData(cm, hm, false);
            yield return new TestCaseData(cm, hl, true);

            yield return new TestCaseData(cl, cs, false);
            yield return new TestCaseData(cl, cm, false);
            yield return new TestCaseData(cl, cl, false);
            yield return new TestCaseData(cl, hs, false);
            yield return new TestCaseData(cl, hm, false);
            yield return new TestCaseData(cl, hl, false);

            yield return new TestCaseData(hs, cs, false);
            yield return new TestCaseData(hs, cm, false);
            yield return new TestCaseData(hs, cl, false);
            yield return new TestCaseData(hs, hs, true);
            yield return new TestCaseData(hs, hm, true);
            yield return new TestCaseData(hs, hl, true);

            yield return new TestCaseData(hm, cs, true);
            yield return new TestCaseData(hm, cm, false);
            yield return new TestCaseData(hm, cl, false);
            yield return new TestCaseData(hm, hs, true);
            yield return new TestCaseData(hm, hm, true);
            yield return new TestCaseData(hm, hl, true);

            yield return new TestCaseData(hl, cs, true);
            yield return new TestCaseData(hl, cm, true);
            yield return new TestCaseData(hl, cl, false);
            yield return new TestCaseData(hl, hs, true);
            yield return new TestCaseData(hl, hm, true);
            yield return new TestCaseData(hl, hl, true);
        }

        [Test, TestCaseSource(nameof(CanBeWith_Config))]
        public void CanBeWith_Test(Animal animal1, Animal animal2, bool CanIBeWith)
        {
            Console.WriteLine(animal1.ToString());
            Console.WriteLine(animal2.ToString());
            Assert.That(animal1.CanIBeWith(animal2), Is.EqualTo(CanIBeWith));
        }

        private static IEnumerable<TestCaseData> AreTrainsProperlyFilled_Config()
        {
            yield return new TestCaseData(new List<Animal> { cs, hm, hm, hm, hl, hl }, 2); // scenario 1
            yield return new TestCaseData(new List<Animal> { cs, hs, hs, hs, hs, hs, hm, hm, hl }, 2); // scenario 2
            yield return new TestCaseData(new List<Animal> { cs, cm, cl, hs, hm, hl }, 4); // scenario 3
            yield return new TestCaseData(new List<Animal> { cs, cs, cm, cl, hs, hm, hm, hm, hm, hm, hl}, 5); // scenario 4
            yield return new TestCaseData(new List<Animal> { cs, hs, hm, hl, hl}, 2); // scenario 5
            yield return new TestCaseData(new List<Animal> { cs, cs, cs, hm, hm, hl, hl, hl}, 3); // scenario 6
            yield return new TestCaseData(new List<Animal> { cs, cs, cs, cs, cs, cs, cs, cm, cm, cm, cl, cl, cl, hm, hm, hm, hm, hm, hl, hl, hl, hl, hl, hl}, 13); // scenario 7
            yield return new TestCaseData(new List<Animal> { }, 0); // scenario 8
        }

        [Test, TestCaseSource(nameof(AreTrainsProperlyFilled_Config))]
        public void AreTrainsProperlyFilled_Test(List<Animal> Animals, int ExpectedAmountOfCarts)
        {
            foreach (Animal animal in Animals) Console.WriteLine(animal.ToString());
            TrainManager trainManager = new TrainManager();
            trainManager.InsertAnimalsIntoCarts(Animals);
            int AmountOfCarts = trainManager.GetBestTrain().Count;
            trainManager.PrintTrains();
            Assert.That(AmountOfCarts, Is.EqualTo(ExpectedAmountOfCarts));
        }

        private static IEnumerable<TestCaseData> IsThereEnoughRoom_Config()
        {
            Cart cart1 = new Cart();
            cart1.AddAnimal_ONLY_FOR_UNIT_TESTS(hl); //hm,true
            Cart cart2 = new Cart();
            cart2.AddAnimal_ONLY_FOR_UNIT_TESTS(hs); //hm,true
            Cart cart3 = new Cart();
            cart3.AddAnimal_ONLY_FOR_UNIT_TESTS(hl);
            cart3.AddAnimal_ONLY_FOR_UNIT_TESTS(hl); //hm,false
            Cart cart4 = new Cart();
            cart4.AddAnimal_ONLY_FOR_UNIT_TESTS(hl);
            cart4.AddAnimal_ONLY_FOR_UNIT_TESTS(hs); //hm, false

            yield return new TestCaseData(cart1, hl, true, 1);
            yield return new TestCaseData(cart2, hl, true, 2);
            yield return new TestCaseData(cart3, hl, false, 3);
            yield return new TestCaseData(cart4, hl, false, 4);
        }

        [Test, TestCaseSource(nameof(IsThereEnoughRoom_Config))]
        public void IsThereEnoughRoom_Test(Cart Cart, Animal AnimalToBeAdded, bool ExpectedResult, int id) => Assert.That(Cart.IsThereEnoughRoomFor(AnimalToBeAdded), Is.EqualTo(ExpectedResult));

    }
}