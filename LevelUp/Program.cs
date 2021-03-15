using System;
using System.Collections.Generic;
using System.Linq;

namespace LevelUp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var dataClass = new Seed();
            var people = dataClass.People;
            CalculateData(people);
        }

        static void CalculateData(List<Person> people)
        {
            Console.WriteLine($"Total de personas { people.Count }");
            Console.WriteLine($"Promedio de altura { people.Average(x => x.Altura) }");
            
            double sum = 0;
            foreach (var item in people)
            {
                sum += item.Altura;
            }
            Console.WriteLine($"Promedio de altura (tradicional) { sum / people.Count }");

            Console.WriteLine("--- Personas (10) ---");
            foreach (var item in people.Take(10))
            {
                Console.WriteLine($"Persona {item.Id} Nombre: {item.Nombre}");
            }

            Console.WriteLine("--- Más personas (11-20) ---");
            foreach (var item in people.Skip(10).Take(10))
            {
                Console.WriteLine($"Persona {item.Id} Nombre: {item.Nombre}");
            }

            var now = DateTime.Now.Date;
            Console.WriteLine("--- Personas (10) con 40 años o más ---");
            foreach (var item in people.Where(x => x.FechaNacimiento <= now.AddYears(-40)).Take(10))
            {
                Console.WriteLine($"Persona {item.Id} F. Nac: {item.FechaNacimiento:yyyy/MM/dd} Nombre: {item.Nombre}");
            }

            Console.WriteLine("--- Personas (10) con letra Z ---");
            foreach (var item in people.Where(x => x.Nombre.Contains("Z")).Take(10))
            {
                Console.WriteLine($"Persona {item.Id} Nombre: {item.Nombre}, Altura: {item.Altura}");
            }

            Console.WriteLine("--- Personas (10) con letra Z en proyección ---");
            var p = people.Where(x => x.Nombre.Contains("Z") && x.Altura >= 181)
                .Select(x => new OtraPersona
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Altura = x.Altura,
                    Year = x.FechaNacimiento.Year,
                })
                .Take(10);

            foreach (var item in p)
            {
                Console.WriteLine($"Persona {item.Id} Nombre: {item.Nombre}, Altura: {item.Altura}, Año {item.Year}");
            }


            Console.WriteLine("--- Personas con proyección de tipo anónimo ---");
            var pp = people.Where(x => x.Nombre.Contains("Y"))
                .Select(x => new
                {
                    x.Id,
                    Anio = x.FechaNacimiento.Year,
                })
                .Take(10);

            foreach (var item in pp)
            {
                Console.WriteLine($"Persona {item.Id} - Año {item.Anio}");
            }

            var otraNotacion = from c in people
                               where c.Altura >= 181
                               select new { c.Id, c.Altura };

            Console.WriteLine($"--- Personas con altura mayor a 181 (Promedio: {otraNotacion.Average(x => x.Altura)})");
            foreach (var item in otraNotacion)
            {
                Console.WriteLine($"Persona {item.Id} - Altura {item.Altura}");
            }
        }
    }
}
