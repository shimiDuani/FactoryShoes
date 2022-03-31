using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectShoesFactory1
{
    class AllDistributionPointsAndAllStock
    {
        Manager mng;
        public AllDistributionPointsAndAllStock(Manager mng)
        {
            this.mng = mng;
        }
        public void DefultShoes()
        {
            mng.AddShoes("NIKE", "AIRFORCE", 45, 2);
            mng.AddShoes("NIKE", "AIRFORCE", 43, 5);
            mng.AddShoes("NIKE", "AIRFORCE", 36, 2);
            mng.AddShoes("NIKE", "JORDAN", 45, 2);
            mng.AddShoes("NIKE", "JORDAN", 42, 6);
            mng.AddShoes("VANS", "OLDSCHOOL", 40, 6);
            mng.AddShoes("VANS", "OLDSCHOOL", 43, 12);
            mng.AddShoes("VANS", "OLDSCHOOL", 39, 2);
            mng.AddShoes("VANS", "OLDSCHOOL", 44, 3);
            mng.AddShoes("ADIDAS", "GEEZEL", 42, 3);
            mng.AddShoes("ADIDAS", "ULTRABOOST", 40, 7);
            mng.AddShoes("ADIDAS", "ULTRABOOST", 37, 4);
        }
        public void DefultDistributionPoints()
        {
            mng.AddDistributionPoints("Kiryat Shmona", 36593);
            mng.AddDistributionPoints("Haifa", 42812);
            mng.AddDistributionPoints("Tel Aviv", 47445);
            mng.AddDistributionPoints("Rishon Lezion", 50295);
            mng.AddDistributionPoints("Jerusalem", 99889);
            mng.AddDistributionPoints("Beer sheva", 90403);
            mng.AddDistributionPoints("Eilat", 99607);
        }
    }
}
