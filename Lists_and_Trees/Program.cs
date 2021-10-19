using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace ConsoleApp12
{
    class Program
    {
        public static void TreeSearch(Tree<string> t, ref string[] mas)
        {
            int count = 0;
            PointTree<string> p = t.root;
            p.left = null;
            p.right = null;
            for (int i = 0; i < mas.Length - 1; i++)
            {
                for (int j = i + 1; j < mas.Length; j++)
                {
                    if (mas[i] == mas[j])
                    {
                        mas[j] = mas[j] + count;
                        count++;
                    }
                }
            }
            foreach (string x in mas) Console.WriteLine(x);
            Tree<string> q = new Tree<string>(p);
            for (int i = 1; i < mas.Length; i++)
            {
                q.TreeSearch(mas[i]);
            }
            Console.WriteLine("\nПолучившееся дерево: \n");
            q.Write(p, 3);
        }
        public static void TreeLookFor(Tree<string> t, Dictionary<string, Person> dt)
        {
            int count = 0;
            Console.WriteLine("\nТеперь переходим к поиску элемента в дереве: ");
            string surname = Console.ReadLine();
            Tree<string>.LookFor(surname, t.root, dt, ref count);
            if (count == 0) Console.WriteLine("\nТаких элементов в дереве нет!\n");
            else Console.WriteLine($"\nВсего таких элементов {count}\n");
        }
        public static void ListThree()
        {
            Tree<string> t = null;
            Console.WriteLine("\nВведите количество элементов в дереве: ");
            ProvUint(out uint size);
            Person[] mas = new Person[size];
            string[] strmas = new string[size];
            if (size > 0)
            {
                int swithcaseF = 0;
                Console.WriteLine("\nКак вы хотите сформировать элементы?");
                Console.WriteLine("1. Вручную");
                Console.WriteLine("2. Автоматически");
                ProvMenu(ref swithcaseF, 0, 3);
                switch (swithcaseF)
                {
                    case 1:
                        for (int i = 0; i < size; i++)
                        {
                            mas[i] = Sozdanie();
                            strmas[i] = mas[i].Surname;
                        }
                        break;
                    case 2:
                        for (int i = 0; i < size; i++)
                        {
                            mas[i] = RandomSozdanie();
                            strmas[i] = mas[i].Surname;
                        }
                        break;
                }
            }
            if (size > 0)
            {
                int count = 0;
                PointTree<string> p = new PointTree<string>(strmas[0]);
                t = new Tree<string>(p);
                p = t.Formation(size, p, ref count, strmas);
                t.root = p;
                t.Write(p, 3);
            }
            else Console.WriteLine("\nДерево пустое!");
            Console.WriteLine("\nА теперь превратим его в дерево поиска:");
            TreeSearch(t, ref strmas);
            Dictionary<string, Person> dt = new Dictionary<string, Person>();
            for (int i = 0; i < mas.Length; i++) dt.Add(strmas[i], mas[i]);
            TreeLookFor(t, dt);
        }
        public static void Add(ref ListDir<Person> d)
        {
            Person per = new Person();
            Person.count--;
            uint pos = 100;
            int switchfour = 0;
            Console.WriteLine("\nКак вы хотите сформировать добавляемый элемент?");
            Console.WriteLine("1. Вручную");
            Console.WriteLine("2. Автоматически");
            ProvMenu(ref switchfour, 0, 3);
            switch (switchfour)
            {
                case 1:
                    Person p = Sozdanie();
                    per = p;
                    break;
                case 2:
                    Person n = RandomSozdanie();
                    per = n;
                    break;
            }
            Console.WriteLine("\nСозданный элемент: ");
            per.Write();
            Console.WriteLine("\nВведите позицию, на которую нужно вставить элемент: ");
            while (pos > d.Length + 1)
            {
                ProvUint(out pos);
                d.Add(per, pos);
            }
            Console.WriteLine("\nПолучившийся список: \n");
            d.Write();
        }
        public static void ListTwo()
        {
            ListDir<Person> d = null;
            Console.WriteLine("\nКак вы хотите создать список?");
            Console.WriteLine("1. Создать пустой список");
            Console.WriteLine("2. Создать дефолтный список с определенной емкостью");
            Console.WriteLine("3. Создать список со значениями");
            int switchtwo = 0;
            ProvMenu(ref switchtwo, 0, 4);
            switch (switchtwo)
            {
                case 1:
                    d = new ListDir<Person>();
                    Console.WriteLine("\nСписок создан!");
                    break;
                case 2:
                    Console.WriteLine("\nВведите, пожалуйста, емкость списка: ");
                    ProvUint(out uint capa);
                    d = new ListDir<Person>(capa);
                    Console.WriteLine("\nСписок создан!");
                    break;
                case 3:
                    Console.WriteLine("\nВведите, пожалуйста, длину списка:");
                    ProvUint(out uint len);
                    Person[] mas = new Person[len];
                    Console.WriteLine("\nКак вы хотите сформировать значения?");
                    Console.WriteLine("1. Вручную");
                    Console.WriteLine("2. Автоматически");
                    int switchthree = 0;
                    ProvMenu(ref switchthree, 0, 3);
                    switch (switchthree)
                    {
                        case 1:
                            for (int i = 0; i < len; i++)
                            {
                                Person p = Sozdanie();
                                mas[i] = p;
                            }
                            break;
                        case 2:
                            for (int i = 0; i < len; i++)
                            {
                                Person p = RandomSozdanie();
                                mas[i] = p;
                            }
                            break;
                    }
                    d = new ListDir<Person>(mas);
                    Console.WriteLine("\nСписок создан!");
                    break;
            }
            if (d.Length != 0) Console.WriteLine("\nСформированный список: \n");
            d.Write();
            Console.WriteLine("\nА теперь переходим к добавлению...");
            Add(ref d);
        }
        public static void Delete(ref ListOne<Person> l)
        {
            Console.WriteLine("\nВведите имя, от которого надо избавиться: ");
            string name = Console.ReadLine();
            while (l.beg.next != null && l.beg.data.Name == name)
            {
                l.beg = l.beg.next;
            }
            if (l.beg.next == null && l.beg.data.Name == name)
            {
                Console.WriteLine("\nСписок стал пустым!\n");
                return;
            }
            Point<Person> p = l.beg;
            for (int i = 0; i < l.Length - 2; i++)
            {
                while (p.next.next != null && p.next.data.Name == name)
                {
                    Console.WriteLine(i + 1);
                    l.Delete(i + 1);
                }
                p = p.next;
            }
            p = l.beg;
            while (p.next.next != null) p = p.next;
            if (p.next.data.Name == name) p.next = null;
            if (l.Length == 0)
            {
                Console.WriteLine("\nСписок стал пустым!\n");
                return;
            }
            Console.WriteLine("\nПолучившийся список: \n");
            l.Write();
        }
        public static void ListOne()
        {
            ListOne<Person> l = null;
            Console.WriteLine("\nКак вы хотите создать список?");
            Console.WriteLine("1. Создать пустой список");
            Console.WriteLine("2. Создать дефолтный список с определенной емкостью");
            Console.WriteLine("3. Создать список со значениями");
            int switchone = 0;
            ProvMenu(ref switchone, 0, 4);
            switch (switchone)
            {
                case 1:
                    l = new ListOne<Person>();
                    Console.WriteLine("\nСписок создан!");
                    break;
                case 2:
                    Console.WriteLine("\nВведите, пожалуйста, емкость списка: ");
                    ProvUint(out uint capa);
                    l = new ListOne<Person>(capa);
                    Console.WriteLine("\nСписок создан!");
                    break;
                case 3:
                    Console.WriteLine("\nВведите, пожалуйста, длину списка:");
                    ProvUint(out uint len);
                    Person[] mas = new Person[len];
                    Console.WriteLine("\nКак вы хотите сформировать значения?");
                    Console.WriteLine("1. Вручную");
                    Console.WriteLine("2. Автоматически");
                    int switchtwo = 0;
                    ProvMenu(ref switchtwo, 0, 3);
                    switch (switchtwo)
                    {
                        case 1:
                            for (int i = 0; i < len; i++)
                            {
                                Person p = Sozdanie();
                                mas[i] = p;
                            }
                            break;
                        case 2:
                            for (int i = 0; i < len; i++)
                            {
                                Person p = RandomSozdanie();
                                mas[i] = p;
                            }
                            break;
                    }
                    l = new ListOne<Person>(mas);
                    Console.WriteLine("\nСписок создан!");
                    break;
            }
            if (l.Length != 0)
            {
                Console.WriteLine("\nПолучившийся список: \n");
                l.Write();
                Console.WriteLine("\nА теперь переходим к удалению...");
                Delete(ref l);
            }
            else
            {
                Console.WriteLine("\nВаш список пустой, удалять нечего!\n");
            }
        }
        public static Person RandomSozdanie()
        {
            int men = 0;
            Console.WriteLine("\n1. Создать преподавателя");
            Console.WriteLine("2. Создать студента");
            Console.WriteLine("3. Создать сотрудника");
            Console.WriteLine("4. Создать просто человека");
            ProvMenu(ref men, 0, 5);
            switch (men)
            {
                case 1:
                    Teacher t = new Teacher();
                    Person.count--;
                    Teacher ter = (Teacher)t.Init();
                    Console.WriteLine($"\nЧеловек №{Person.count} создан\n");
                    return ter;
                case 2:
                    Student s = new Student();
                    Person.count--;
                    Student ser = (Student)s.Init();
                    Console.WriteLine($"\nЧеловек №{Person.count} создан\n");
                    return ser;
                case 3:
                    Employee e = new Employee();
                    Person.count--;
                    Employee eer = (Employee)e.Init();
                    Console.WriteLine($"\nЧеловек №{Person.count} создан\n");
                    return eer;
                case 4:
                    Person p = new Person();
                    Person.count--;
                    Person per = (Person)p.Init();
                    Console.WriteLine($"\nЧеловек №{Person.count} создан\n");
                    return per;
            }
            return new Person();
        }
        public static Person Sozdanie()
        {
            int men = 0;
            Console.WriteLine("\n1. Создать преподавателя");
            Console.WriteLine("2. Создать студента");
            Console.WriteLine("3. Создать сотрудника");
            Console.WriteLine("4. Создать просто человека");
            ProvMenu(ref men, 0, 5);
            switch (men)
            {
                case 1:
                    Console.WriteLine("\nЕго имя: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Его фамилия: ");
                    string surname = Console.ReadLine();
                    Console.WriteLine("Его возраст: ");
                    ProvUint(out uint age);
                    Console.WriteLine("Его преподаваемый предмет: ");
                    string discipline = Console.ReadLine();
                    Console.WriteLine("Количество его студентов: ");
                    ProvUint(out uint stud);
                    Teacher t = new Teacher(name: name, surname: surname, age: age, discipline: discipline, kolStud: stud);
                    Console.WriteLine($"\nЧеловек №{Person.count} создан\n");
                    return t;
                case 2:
                    Console.WriteLine("\nЕго имя: ");
                    string namest = Console.ReadLine();
                    Console.WriteLine("Его фамилия: ");
                    string surnamest = Console.ReadLine();
                    Console.WriteLine("Его возраст: ");
                    ProvUint(out uint agest);
                    Console.WriteLine("Его курс: ");
                    ProvUint(out uint course);
                    Console.WriteLine("Его средняя оценка: ");
                    ProvDouble(out double mark);
                    Student s = new Student(name: namest, surname: surnamest, age: agest, course: course, avmark: mark);
                    Console.WriteLine($"\nЧеловек №{Person.count} создан\n");
                    return s;
                case 3:
                    Console.WriteLine("\nЕго имя: ");
                    string namesot = Console.ReadLine();
                    Console.WriteLine("Его фамилия: ");
                    string surnamesot = Console.ReadLine();
                    Console.WriteLine("Его возраст: ");
                    ProvUint(out uint agesot);
                    Console.WriteLine("Его зарплата: ");
                    ProvUint(out uint salary);
                    Console.WriteLine("Сколько лет он работает?");
                    ProvUint(out uint years);
                    Employee e = new Employee(name: namesot, surname: surnamesot, age: agesot, salary: salary, years: years);
                    Console.WriteLine($"\nЧеловек №{Person.count} создан\n");
                    return e;
                case 4:
                    Console.WriteLine("\nЕго имя: ");
                    string nameper = Console.ReadLine();
                    Console.WriteLine("Его фамилия: ");
                    string surnameper = Console.ReadLine();
                    Console.WriteLine("Его возраст: ");
                    ProvUint(out uint ageper);
                    Person p = new Person(name: nameper, surname: surnameper, age: ageper);
                    Console.WriteLine($"\nЧеловек №{Person.count} создан\n");
                    return p;
            }
            return new Person();
        }
        static public void ProvDouble(out double a)
        {
            double n;
            bool ok;
            a = 0;
            do
            {
                string buf = Console.ReadLine();
                ok = double.TryParse(buf, out n);
                if (ok && n != 0) a = n;
                else Console.WriteLine("Введите, пожалуйста, положительное число!");
            } while (!ok || n == 0);
        }
        static public void ProvUint(out uint a)
        {
            uint n;
            bool ok;
            a = 0;
            do
            {
                string buf = Console.ReadLine();
                ok = uint.TryParse(buf, out n);
                if (ok && n != 0) a = n;
                else Console.WriteLine("Введите, пожалуйста, натуральное число!");
            } while (!ok || n == 0);
        }
        public static void ProvMenu(ref int switchcase, int niz, int verh)
        {
            int n;
            bool ok;
            do
            {
                string buf = Console.ReadLine();
                ok = int.TryParse(buf, out n);
                if (ok && n > niz && n < verh) switchcase = n;
                else Console.WriteLine("Выберите, пожалуйста, один из пунктов меню!");
            } while (!ok || n < (niz + 1) || n > (verh - 1));
        }
        static void Main(string[] args)
        {
            int swithcase = 0;
            do
            {
                Console.WriteLine("С чем вы хотите работать?");
                Console.WriteLine("1. С однонаправленным списком");
                Console.WriteLine("2. С двунаправленным списком");
                Console.WriteLine("3. С деревом");
                Console.WriteLine("4. Ни с чем. Выход из программы");
                ProvMenu(ref swithcase, 0, 5);
                switch (swithcase)
                {
                    case 1:
                        ListOne();
                        break;
                    case 2:
                        ListTwo();
                        break;
                    case 3:
                        ListThree();
                        break;
                }
            } while (swithcase != 4);
        }
    }
}