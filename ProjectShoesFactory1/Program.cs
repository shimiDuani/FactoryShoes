using DataStracture;
using Logic;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectShoesFactory1
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager m = new Manager(new TimeSpan(0, 0, 30), new TimeSpan(0, 2, 30));//טיימר ראשון ממתי להתחיל והשני כל כמה זמן לבדוק
            ConsoleManager cm = new ConsoleManager(m);
            AllDistributionPointsAndAllStock a = new AllDistributionPointsAndAllStock(m);
            a.DefultShoes();
            a.DefultDistributionPoints();
            cm.Init();
        }
    }
}
