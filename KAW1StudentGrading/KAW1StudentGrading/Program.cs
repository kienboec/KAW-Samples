using System;

namespace KAW1StudentGrading
{
    using KAW1StudentGrading.LectureComponent;
    using KAW1StudentGrading.StudentComponent;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //MyFirstGeneratedClass1 asdf = new MyFirstGeneratedClass1();
            Student raiman = new Student() { Name = "Raiman" };
            Student hasan = new Student() { Name = "Hasan" };

            Lecture kaw = new Lecture() { Name = "KAW" };

            LectureGrade grade = new LectureGrade() { Name = "KAW-Note" };

            Laptop laptop = new Laptop();
        }
    }
}
