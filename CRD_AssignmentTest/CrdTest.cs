using System;
using System.Collections.Generic;
using System.Linq;
using CRD_Assignment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static CRD_Assignment.Test;

namespace CRD_AssignmentTest
{
    [TestClass]
    public class CrdTest
    {
        private readonly Test test;

        Inventory inv = new Inventory();
        public CrdTest()
        {
            test = new Test();

            inv.Sedans = new List<Car>
            {
                CarFactory.CreateCar(CarType.Sedan, new CarSpecs("Audi", "S", 2005)),
                CarFactory.CreateCar(CarType.Sedan, new CarSpecs("BMW", "4", 2006), "Turbo"),
                CarFactory.CreateCar(CarType.Sedan, new CarSpecs("Porsche", "Panera", 2011)),
            };
            inv.Trucks = new List<Car>
            {
                CarFactory.CreateCar(CarType.Truck, new CarSpecs("GM", "Tahoe", 2014)),
                CarFactory.CreateCar(CarType.Truck, new CarSpecs("Ford", "F150", 2016)),
                CarFactory.CreateCar(CarType.Truck, new CarSpecs("Chevrolet", "Silverado", 2018))
            };
            inv.Vans = new List<Car>
            {
                CarFactory.CreateCar(CarType.Van, new CarSpecs("Toyota", "Sienna", 2016)),
                CarFactory.CreateCar(CarType.Van, new CarSpecs("Honda", "Odyssey", 2018)),
                CarFactory.CreateCar(CarType.Van, new CarSpecs("Chrysler", "Pacifica", 2019), "Voyager")
            };


        }
        [TestMethod]
        public void TestMethod1a()
        {
            var result = inv.Sedans[1].ReserveCar(DateTime.Now, 4);
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void TestMethod1b()
        {
            var res = new List<bool>
            {
                inv.Trucks[1].ReserveCar(DateTime.Now, 1),
                inv.Trucks[1].ReserveCar(DateTime.Now.AddDays(1), 2)
            };
            Assert.AreEqual(res.SequenceEqual(new[] { true, true }), true);
        }
        [TestMethod]
        public void TestMethod1c()
        {
            var result = inv.Vans[1].ReserveCar(DateTime.Now, 4);
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void TestMethod2()
        {
            var res = new List<bool>
            {
                inv.Sedans[1].ReserveCar(DateTime.Now, 4),
                inv.Trucks[1].ReserveCar(DateTime.Now, 4),
                inv.Vans[2].ReserveCar(DateTime.Now, 4)
            };
            Assert.AreEqual(res.SequenceEqual(new[] { true, true, true }), true);
        }
        [TestMethod]
        public void TestMethod3()
        {
            var res = new List<bool>
            {
                //reserve car with overlapping days
                inv.Sedans[1].ReserveCar(DateTime.Now, 4),
                inv.Sedans[1].ReserveCar(DateTime.Now.AddDays(1), 4)
            };
            Assert.AreEqual(res.SequenceEqual(new[] { true, false }), true);
        }
        [TestMethod]
        public void TestMethod4a()
        {
            var available = inv.Sedans[2].ReserveCar(DateTime.Now, 4);
            var r = inv.Sedans[2].GetCarInfo();
            Assert.AreEqual(r.Count > 0, true);
        }

        [TestMethod]
        public void TestMethod4b()
        {
            var available = inv.Trucks[2].ReserveCar(DateTime.Now, 4);
            var r = inv.Trucks[2].GetCarInfo();
            Assert.AreEqual(r.Count > 0, true);
        }

        [TestMethod]
        public void TestMethod4c()
        {
            var available = inv.Vans[2].ReserveCar(DateTime.Now, 4);
            var r = inv.Vans[2].GetCarInfo();
            Assert.AreEqual(r.Count > 0, true);
        }
        [TestMethod]
        public void TestMethod5()
        {
            try
            {
                var available = inv.Sedans[2].ReserveCar(DateTime.Now, 4);
                var r = inv.Sedans[5].GetCarInfo();
            }
            catch (Exception e)
            {
                Assert.AreEqual(e is ArgumentOutOfRangeException, true);
            }
        }
        [TestMethod]
        public void TestMethod6()
        {
            var res = new List<bool>
            {
                inv.Trucks[1].ReserveCar(DateTime.Now, 1),
                inv.Trucks[1].ReserveCar(DateTime.Now.AddDays(1), 2)

            };
            var info = inv.Trucks[1].GetCarInfo();
            Assert.AreEqual(info.Count == 2, true);
        }
    }
}
