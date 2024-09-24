using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program1
{
    internal class _3_1
    {
        public class Animal
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public void Eat()
            {
                Console.WriteLine("Animal is eating.");
            }

            public void Sleep()
            {
                Console.WriteLine("Animal is sleeping.");
            }
        }

        // 자식 클래스
        public class Dog : Animal
        {
            public void Bark()
            {
                Console.WriteLine("Dog is bark");
            }

        }

        public class Cat : Animal
        {
            public void Meow()
            {
                Console.WriteLine("Cat is meow");
            }
        }

        static void Main(string[] args)
        {
            Dog dog = new Dog();
            dog.Name = "ss";
            dog.Age = 3;

            dog.Eat();
            dog.Sleep();
            dog.Bark();
        }
    }
}