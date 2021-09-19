
namespace ExercicioFixacao233.Entities
{
    public class Employee
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public double Sallary { get; set; }
    
        public Employee()
        {

        }

        public Employee(string name, string email, double sallary)
        {
            Name = name;
            Email = email;
            Sallary = sallary;
        }

    }
}
