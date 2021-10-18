using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntity
{
    class Worker
    {
        /*public Worker(string firstName, string surname, int age,Boss boss)
        {
            FirstName = firstName;
            Surname = surname;
            Age = age;
            Boss = boss;
        }*/
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [MaxLength(20)]
        public string Surname { get; set; }
        
        public int Age { get; set; }
        public string FullName { get; }
        [ForeignKey("BossInfoKey")]
        public int? BossId { get; set; }
        public Boss Boss { get; set; } // навигационное свойство
    }

    class Boss
    {
        public Boss(string name,string department)
        {
            Name = name;
            Department = department;
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public List<Worker> Workers { get; set; }

    }

    class Description
    {
        private string _label;
        
        public int DescriptionID { get; set; }

        public string Label
        {
            get { return _label; }
            set { _label = value; }
        } 
    }


}
