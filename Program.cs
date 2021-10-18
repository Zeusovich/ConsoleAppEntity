using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ConsoleAppEntity
{
    class Program
    {
        static void Main(string[] args)
        {
            #region CRUD
            /*   // добавление
                using (AppContext db = new AppContext())
                {
                    Worker worker1 = new Worker("AAAA", "AAAA", 10, "AAAA");
                    Worker worker2 = new Worker("BBB", "BBBB", 20, "BBBB");

                    // добавление
                    db.Workers.Add(worker1);
                    db.Workers.Add(worker2);
                    db.SaveChanges();

                    Console.WriteLine("Обьекты успешно сохранены");



                }

                // получение
                using (AppContext db = new AppContext())
                {
                    // получение обьектов из бд и вывод на консоль
                    var workers = db.Workers.ToList();
                    Console.WriteLine("Список обьектов:");
                    foreach (Worker w in workers)
                    {
                        Console.WriteLine($"{w.Id}.{w.FirstName} {w.Surname} - {w.Age}   {w.BossName}");
                    }
                }

                // редактирование
                using (AppContext db = new AppContext())
                {
                    // получаем первый обьект
                    Worker worker = db.Workers.FirstOrDefault();
                    if (worker != null)
                    {
                        worker.FirstName = "CCCC";
                        worker.Surname = "CCCC";
                        worker.Age = 15;
                        db.Workers.Update(worker);
                    }

                    Console.WriteLine("\nДанные после редактирования:");
                    var workers = db.Workers.ToList();
                    foreach (Worker w in workers)
                    {
                        Console.WriteLine($"{w.Id}.{w.FirstName} {w.Surname} - {w.Age}   {w.BossName}");
                    }
                }


                // удаление
                using (AppContext db = new AppContext())
                {

                    Worker worker = db.Workers.FirstOrDefault();
                    if (worker != null)
                    {
                        db.Workers.Remove(worker);
                        db.SaveChanges();
                    }

                    Console.WriteLine("\nДанные после удаления:");
                    var workers = db.Workers.ToList();
                    foreach (Worker w in workers)
                    {
                        Console.WriteLine($"{w.Id}.{w.FirstName} {w.Surname} - {w.Age}   {w.BossName}");
                    }

                }

                Console.ReadKey();
            }*/
            #endregion

            #region LINQ

            using (AppContext db = new AppContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Boss boss1 = new Boss("AAAA", "AAAA");
                Boss boss2 = new Boss("BBBB", "BBBB");
                Boss boss3 = new Boss("CCCC", "CCCC");
                Boss boss4 = new Boss("DDDD", "DDDD");

                db.Bosses.AddRange(boss1, boss2, boss3, boss4);

                Worker worker1 = new Worker
                {
                    FirstName = "AAA",
                    Surname = "AAA",
                    Age = 1,
                    Boss = boss1
                };
                Worker worker2 = new Worker
                {
                    FirstName = "BBB",
                    Surname = "BBB",
                    Age = 2,
                    Boss = boss2
                };
                Worker worker3 = new Worker
                {
                    FirstName = "CCC",
                    Surname = "CCC",
                    Age = 3,
                    Boss = boss2
                };
                Worker worker4 = new Worker
                {
                    FirstName = "DDD",
                    Surname = "DDD",
                    Age = 4,
                    Boss = boss3
                };
                Worker worker5 = new Worker
                {
                    FirstName = "EEE",
                    Surname = "EEE",
                    Age = 5,
                    Boss = boss4
                };
                Worker worker6 = new Worker
                {
                    FirstName = "FFF",
                    Surname = "FFF",
                    Age = 5,
                    Boss = boss4
                };
                /*Worker worker1 = new Worker("AAA", "AAA", 1, boss1);
                Worker worker2 = new Worker("BBB", "BBB", 2,boss2);
                Worker worker3 = new Worker("CCC", "CCC", 3,boss2);
                Worker worker4 = new Worker("DDD", "DDD", 4,boss3);
                Worker worker5 = new Worker("EEE", "EEE", 5,boss4);
                Worker worker6 = new Worker("FFF", "FFF", 6,boss4);*/

                db.Workers.AddRange(worker1, worker2, worker3, worker4, worker5, worker6);
                db.SaveChanges();
            }

            using(AppContext db = new AppContext())
            {
                var workers = (from worker in db.Workers.Include(p => p.Boss)
                                where worker.BossId >= 0
                                select worker).ToList();

                foreach (var worker in workers)
                    Console.WriteLine($"{worker.FirstName} ({worker.Surname}) - {worker.Boss.Name}.{worker.Boss.Department}");
            }

            using (AppContext db = new AppContext())
            {
                var workers = db.Workers.Join(db.Bosses,
                    w => w.BossId,
                    b => b.Id,
                    (w, b) => new
                    {

                        WorkerFullname = w.FullName,
                        WorkerAge = w.Age,
                        Boss = b.Name,
                        Department = b.Department

                    });
                foreach (var w in workers)
                    Console.WriteLine($" BossName - {w.Boss} in {w.Department} ;worker {w.WorkerFullname} age: {w.WorkerAge}");
                Console.ReadKey();
            }

        }

        #endregion
    }
}
