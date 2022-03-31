using DataStracture;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    public class Manager
    {
        Dictionary<string, Dictionary<string, Shoe>> shoeCollection;//dictionary containing brand, model, and shoe
        BST<DistributionPiont> bstPiont;//Distribution point tree 
        DoubleLinkedList<NodeDateTime> LinkedListDayTime;//link list Time incidence of shoes
        Timer timer;
        public Manager(TimeSpan dueTime, TimeSpan tick)
        {
            shoeCollection = new Dictionary<string, Dictionary<string, Shoe>>();
            bstPiont = new BST<DistributionPiont>();
            LinkedListDayTime = new DoubleLinkedList<NodeDateTime>();
            Timer(dueTime,tick);
        }
        public void AddDistributionPoints(string myName, int zipCode)
        {
            bstPiont.Add(new DistributionPiont(myName, zipCode));
        }//Add new distribution points
        public void AddShoes(string brand, string modelName, float size, int amount)
        {
            if (!shoeCollection.ContainsKey(brand))//If there is no same brand
            {
                Dictionary<string, Shoe> newModel = new Dictionary<string, Shoe>();
                Shoe a = new Shoe() { Model = modelName, Brand = brand };
                a.BstSizeAndAmount.Add(new SizeAndAmount(size, amount));
                shoeCollection.Add(brand, newModel);
                newModel.Add(modelName, a);
                NodeDateTime node = new NodeDateTime(brand, modelName);
                LinkedListDayTime.AddLast(node); //adding the date to the double link list
                shoeCollection[brand][modelName].DateTimeNode = LinkedListDayTime.end;//Links the node we added to the Link List to the node in the class
            }
            else
            {
                if (!shoeCollection[brand].ContainsKey(modelName))//When there is the same brand but not the same model
                {
                    Shoe s = new Shoe() { Model = modelName, Brand = brand };
                    s.BstSizeAndAmount.Add(new SizeAndAmount(size, amount));
                    shoeCollection[brand].Add(modelName, s);
                    NodeDateTime node = new NodeDateTime(brand, modelName);
                    LinkedListDayTime.AddLast(node); //adding the date to the double link list
                    shoeCollection[brand][modelName].DateTimeNode = LinkedListDayTime.end;//Links the node we added to the end of the Link List to the node in the class
                }
                else//When there is the same brand and the same model
                {
                    SizeAndAmount s = new SizeAndAmount(size, amount);
                    SizeAndAmount t = new SizeAndAmount();
                    shoeCollection[brand][modelName].BstSizeAndAmount.Search(s, out t);//search the size if it exists
                    if (t != null)
                    {
                        t.Amount += amount;//update amount
                    }
                    else shoeCollection[brand][modelName].BstSizeAndAmount.Add(s);//If that size doesn't exist
                }
            }

        }//Add shoes to stock
        public bool BuyShoes(string brand, string modelName, float size, int amount, Action<string> act)
        {
            CheckShoeBrand(brand);
            CheckShoeModel(brand, modelName);
            SizeAndAmount t = CheckSizeAndAmount(brand, modelName, size, amount);//Find the same size and amount of the same model

            if (t != null)
            {
                if (amount <= t.Amount)//Checks whether there is amount requested by the customer in stock
                {
                    NodeDateTime nodeDateTime = new NodeDateTime(brand, modelName);
                    shoeCollection[brand][modelName].IncidencesShoes.EnQueue(nodeDateTime);//Save as circular queue the buy dates 

                    shoeCollection[brand][modelName].DateTimeNode.value.MyDateTime = nodeDateTime.MyDateTime;//save Real time sale
                    string LastBrand = LinkedListDayTime.end.value.Brand;//Save the last value before adding brand and model
                    string LastModel = LinkedListDayTime.end.value.Model;

                    if (!(LastBrand.CompareTo(brand) == 0 && LastModel.CompareTo(modelName) == 0))//If it's not the last
                    {
                        LinkedListDayTime.MoveToEndByNode(shoeCollection[brand][modelName].DateTimeNode);
                        shoeCollection[brand][modelName].DateTimeNode = LinkedListDayTime.end;//move to end
                    }
                    t.Amount -= amount;//Subtract amount
                    if (t.Amount == 0)
                    {
                        SizeAndAmount x = new SizeAndAmount();
                        shoeCollection[brand][modelName].BstSizeAndAmount.RemoveNotRequ(t, out x);//Delete the size if there is 0 left in stock
                        if (shoeCollection[brand][modelName].BstSizeAndAmount.Root() == null)
                        {
                            shoeCollection[brand].Remove(modelName);//Delete a model if all sizes are over
                            LinkedListDayTime.RemoveLast(out nodeDateTime);//Remove from linked list
                            if (!shoeCollection[brand].Any()) shoeCollection.Remove(brand);//Delete a brand if all models are run out
                        }
                    }
                }
                else//Not enough in stock
                {
                    return false;
                }
            }
            else//Size or amount not found
            {
                return false;
            }
            return true;
        }
        public SizeAndAmount CheckSizeAndAmount(string brand,string modelName,float size,int amount)
        {
            SizeAndAmount s = new SizeAndAmount(size, amount);
            SizeAndAmount t = new SizeAndAmount();
            shoeCollection[brand][modelName].BstSizeAndAmount.Search(s, out t);
            return t;
        }//check if size or amount exist
        public bool CheckShoeBrand(string brand)
        {
            return shoeCollection.ContainsKey(brand);
        }//check if brand found
        public bool CheckShoeModel(string brand,string model)
        {
            return shoeCollection[brand].ContainsKey(model);
        }//check if model found in brand
        public void PrintAll(Action<string> act)
        {
            foreach (var brand in shoeCollection)
            {
                act?.Invoke($"brand - {brand.Key}");
                foreach (var kvp in brand.Value)
                {
                    act?.Invoke($"Model = {kvp.Key}");
                    kvp.Value.BstSizeAndAmount.ScanInOrder(act);
                    act?.Invoke("---------------------------");
                    act?.Invoke(kvp.Value.IncidencesShoes.ToString());
                    act?.Invoke("---------------------------");
                }
                act?.Invoke("\n");
            }
        }//print all shoes
        public void PrintDistributionPointsByIncidence(Action<string> action)
        {
            BstComparerIncidence<DistributionPiont> bst = new BstComparerIncidence<DistributionPiont>();
            bst.Bst(bstPiont);
            bst.ScanInOrder(action);
        }
        public DistributionPiont FindClossedDistributionPiont(DistributionPiont point)
        {
            DistributionPiont DP = new DistributionPiont();
            DP = bstPiont.FindClosestValue(point);
            return DP;
        }//Find a distribution point closest to customer home
        public void TimerToIncedenceModel(object state)
        {
            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;
            while (LinkedListDayTime.start != null)//If there are nodes on the list
            {
                if (dateTime.Subtract(LinkedListDayTime.start.value.MyDateTime).TotalSeconds > 1200)//If the shoe not bought more than 120 seconds
                {
                    NodeDateTime nodeDateTime = new NodeDateTime();
                    LinkedListDayTime.RemoveFirst(out nodeDateTime);//remove from link list
                    shoeCollection[nodeDateTime.Brand].Remove(nodeDateTime.Model);
                    if (!shoeCollection[nodeDateTime.Brand].Any()) shoeCollection.Remove(nodeDateTime.Brand);
                }
                else break;
            }
            //PrintAll(CheckTimer);//chek
        }
        public void Timer(TimeSpan dueTime, TimeSpan tick)
        {
            timer = new Timer(TimerToIncedenceModel, null, dueTime, tick);
        }
        private void CheckTimer(string a)
        {
            Console.WriteLine(a);
        }
    }
}
