using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Provaders.DAL;
using Provaders.DAL.Operator.Interface;
using Provaders.DAL.Operator;
using Provaders.DAL.Administrator;

namespace KakNibudCA
{
    class Program
    {
        static void Main(string[] args)
        {
            Operator op = new Operator();
            Console.WriteLine("Введите логотип");
            op.logo = Console.ReadLine();

            Console.WriteLine("Введите префиксы");
            short val = 0;
            List<Prefics> prefics = new List<Prefics>();

            do
            {
                val = Int16.Parse(Console.ReadLine());
                if (val <= 0) break;

                Prefics p = new Prefics();
                p.pref = val;
                prefics.Add(p);

            } while (true);

            op.prefics = prefics;

            Console.WriteLine("Введите имя оператора");
            op.nameOperator = Console.ReadLine();
            Console.WriteLine("Введите процент");
            op.procent = Double.Parse(Console.ReadLine());


            Administator ad = new Administator(@"\\dc\Students\Для сохранения\PMP-162.1");
            string mes = "Всё нормально!";

            ad.CreateOperatorSerialize(op, out mes);
            //bool opcreate = ad.CreateOperator(op,out mes);
            Console.WriteLine(mes);


            Console.ReadLine();

        }
    }
}
