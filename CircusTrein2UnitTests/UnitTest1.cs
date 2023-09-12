using Logic;

namespace CircusTrein2UnitTests
{
    public class Tests
    {
        static int[] ScenarioOutcomes = new int[] { 2, 2, 4, 5, 2, 3, 13, 0 };

        static Animal ck = new Animal(Size.Small, DietType.Carnivore);
        static Animal cm = new Animal(Size.Medium, DietType.Carnivore);
        static Animal cg = new Animal(Size.Large, DietType.Carnivore);

        static Animal hk = new Animal(Size.Small, DietType.Herbivore);
        static Animal hm = new Animal(Size.Medium, DietType.Herbivore);
        static Animal hg = new Animal(Size.Large, DietType.Herbivore);

        [SetUp]
        public void Setup() { }

        private static IEnumerable<TestCaseData> CanBeWith_Config()
        {
            yield return new TestCaseData(ck, ck, false);
            yield return new TestCaseData(ck, cm, false);
            yield return new TestCaseData(ck, cg, false);
            yield return new TestCaseData(ck, hk, false);
            yield return new TestCaseData(ck, hm, true);
            yield return new TestCaseData(ck, hg, true);

            yield return new TestCaseData(cm, ck, false);
            yield return new TestCaseData(cm, cm, false);
            yield return new TestCaseData(cm, cg, false);
            yield return new TestCaseData(cm, hk, false);
            yield return new TestCaseData(cm, hm, false);
            yield return new TestCaseData(cm, hg, true);

            yield return new TestCaseData(cg, ck, false);
            yield return new TestCaseData(cg, cm, false);
            yield return new TestCaseData(cg, cg, false);
            yield return new TestCaseData(cg, hk, false);
            yield return new TestCaseData(cg, hm, false);
            yield return new TestCaseData(cg, hg, false);

            yield return new TestCaseData(hk, ck, false);
            yield return new TestCaseData(hk, cm, false);
            yield return new TestCaseData(hk, cg, false);
            yield return new TestCaseData(hk, hk, true);
            yield return new TestCaseData(hk, hm, true);
            yield return new TestCaseData(hk, hg, true);

            yield return new TestCaseData(hm, ck, true);
            yield return new TestCaseData(hm, cm, false);
            yield return new TestCaseData(hm, cg, false);
            yield return new TestCaseData(hm, hk, true);
            yield return new TestCaseData(hm, hm, true);
            yield return new TestCaseData(hm, hg, true);

            yield return new TestCaseData(hg, ck, true);
            yield return new TestCaseData(hg, cm, true);
            yield return new TestCaseData(hg, cg, false);
            yield return new TestCaseData(hg, hk, true);
            yield return new TestCaseData(hg, hm, true);
            yield return new TestCaseData(hg, hg, true);
        }

        [Test, TestCaseSource(nameof(CanBeWith_Config))]
        public void CanBeWith_Test(Animal animal1, Animal animal2, bool CanIBeWith)
        {
            Console.WriteLine(animal1.ToString());
            Console.WriteLine(animal2.ToString());
            Assert.AreEqual(CanIBeWith, animal1.CanIBeWith(animal2));
        }

        private static IEnumerable<TestCaseData> AreTrainsProperlyFilled_Config()
        {
            yield return new TestCaseData(new List<Animal> { ck, hm, hm, hm, hg, hg }, 2); // scenario 1
            yield return new TestCaseData(new List<Animal> { ck, hk, hk, hk, hk, hk, hm, hm, hg }, 2); // scenario 2
            yield return new TestCaseData(new List<Animal> { ck, cm, cg, hk, hm, hg }, 4); // scenario 3
            yield return new TestCaseData(new List<Animal> { ck, ck, cm, cg, hk, hm, hm, hm, hm, hm, hg}, 5); // scenario 4
            yield return new TestCaseData(new List<Animal> { ck, hk, hm, hg, hg}, 2); // scenario 5
            yield return new TestCaseData(new List<Animal> { ck, ck, ck, hm, hm, hg, hg, hg}, 3); // scenario 6
            yield return new TestCaseData(new List<Animal> { ck, ck, ck, ck, ck, ck, ck, cm, cm, cm, cg, cg, cg, hm, hm, hm, hm, hm, hg, hg, hg, hg, hg, hg}, 13); // scenario 7
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
            Assert.AreEqual(ExpectedAmountOfCarts, AmountOfCarts);
        }

        private static IEnumerable<TestCaseData> IsThereEnoughRoom_Config()
        {
            Cart cart1 = new Cart();
            cart1.AddAnimal_ONLY_FOR_UNIT_TESTS(hg); //hm,true
            Cart cart2 = new Cart();
            cart2.AddAnimal_ONLY_FOR_UNIT_TESTS(hk); //hm,true
            Cart cart3 = new Cart();
            cart3.AddAnimal_ONLY_FOR_UNIT_TESTS(hg);
            cart3.AddAnimal_ONLY_FOR_UNIT_TESTS(hg); //hm,false
            Cart cart4 = new Cart();
            cart4.AddAnimal_ONLY_FOR_UNIT_TESTS(hg);
            cart4.AddAnimal_ONLY_FOR_UNIT_TESTS(hk); //hm, false

            yield return new TestCaseData(cart1, hg, true, 1);
            yield return new TestCaseData(cart2, hg, true, 2);
            yield return new TestCaseData(cart3, hg, false, 3);
            yield return new TestCaseData(cart4, hg, false, 4);
        }

        [Test, TestCaseSource(nameof(IsThereEnoughRoom_Config))]
        public void IsThereEnoughRoom_Test(Cart Cart, Animal AnimalToBeAdded, bool ExpectedResult, int id) => Assert.AreEqual(ExpectedResult, Cart.IsThereEnoughRoomFor(AnimalToBeAdded));

    }
}