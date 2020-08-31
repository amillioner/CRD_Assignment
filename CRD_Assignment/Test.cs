using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CRD_Assignment
{
    public class Test
    {
        //Design and implement a car rental system using object-oriented principles.
        //1. The system should allow reserving a car of a given type at a desired date and time for a given number of days.
        //2. There are 3 types of cars (sedan, SUV and van).
        //3. The number of cars of each type is limited.
        //Use Java as the implementation language.Use unit tests to prove the system satisfies the requirements.

        //Recommended time for this task: 2-4 hours

        public class Inventory
        {
            public List<Car> Sedans { get; set; }
            public List<Car> Trucks { get; set; }
            public List<Car> Vans { get; set; }
        }
        public class CarFactory
        {
            public static Car CreateCar(CarType type, CarSpecs specs, string features = null)
            {
                switch (type)
                {
                    case CarType.Sedan:
                        return new Sedan(specs, features);
                    case CarType.Truck:
                        return new Truck(specs, features);
                    case CarType.Van:
                        return new Van(specs, features);
                    default:
                        throw new ArgumentException("Car type does not exist!");
                }
            }
        }
        public enum CarType
        {
            Sedan, Truck, Van
        }

        public class CarSpecs
        {
            public string Make { get; }
            public string Model { get; }
            public int Year { get; }

            public CarSpecs(string make, string model, int year)
            {
                Make = make;
                Model = model;
                Year = year;
            }
            public override string ToString()
            {
                return $"The car is {Make} and of model {Model} and is made in {Year}.";
            }
        }
        public class ReservationInfo : EventArgs
        {
            public int Duration { get; set; }
            public DateTime StartDateTime { get; set; }
            public CarSpecs CarSpecs { get; set; }
            public ReservationInfo(CarSpecs specs)
            {
                CarSpecs = specs;
            }
        }
        public abstract class Car
        {
            protected CarSpecs CarSpecs { get; set; }
            protected Car(CarSpecs specs)
            {
                CarSpecs = specs;
            }
            public abstract bool ReserveCar(DateTime startDateTime, int days);
            public abstract List<ReservationInfo> GetCarInfo();

            public event EventHandler<ReservationInfo> CarReserved;
            protected virtual void OnCarReserved(ReservationInfo e)
            {
                var handler = CarReserved;
                handler?.Invoke(this, e);
            }


        }
        public class Sedan : Car
        {
            public string Features { get; }
            public string Status { get; set; }

            private readonly Dictionary<CarSpecs, List<ReservationInfo>> sedanInfo = new Dictionary<CarSpecs, List<ReservationInfo>>();

            public override List<ReservationInfo> GetCarInfo()
            {
                return sedanInfo.ContainsKey(CarSpecs) ? sedanInfo[CarSpecs] : null;
            }
            public Sedan(CarSpecs specs, string features) : base(specs)
            {
                CarSpecs = specs;
                Features = features;
            }

            public override bool ReserveCar(DateTime startDateTime, int days)
            {
                var res = new ReservationInfo(CarSpecs)
                {
                    Duration = days,
                    StartDateTime = startDateTime
                };
                if (sedanInfo.ContainsKey(res.CarSpecs))
                {
                    var key = sedanInfo[res.CarSpecs].Last();
                    var endDate = key.StartDateTime.AddDays(key.Duration);
                    if (res.StartDateTime > endDate)
                    {
                        sedanInfo[res.CarSpecs].Add(res);
                        OnCarReserved(res);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    sedanInfo[res.CarSpecs] = new List<ReservationInfo> { res };
                    OnCarReserved(res);
                }
                return true;
            }

            protected override void OnCarReserved(ReservationInfo e)
            {
                base.OnCarReserved(e);
                Status = $"Sedan is reserved from {e.StartDateTime} for {e.Duration} days.";
            }

            public override string ToString()
            {
                return base.ToString() + $"This sedan has additional {Features} features.";
            }
        }
        public class Truck : Car
        {
            public string Features { get; }
            public string Status { get; set; }

            public readonly Dictionary<CarSpecs, List<ReservationInfo>> truckInfo = new Dictionary<CarSpecs, List<ReservationInfo>>();
            public Truck(CarSpecs specs, string features) : base(specs)
            {
                CarSpecs = specs;
                Features = features;
            }
            public override List<ReservationInfo> GetCarInfo()
            {
                return truckInfo.ContainsKey(CarSpecs) ? truckInfo[CarSpecs] : null;
            }

            public override bool ReserveCar(DateTime startDateTime, int days)
            {
                var res = new ReservationInfo(CarSpecs)
                {
                    Duration = days,
                    StartDateTime = startDateTime
                };
                if (truckInfo.ContainsKey(res.CarSpecs))
                {
                    var key = truckInfo[res.CarSpecs].Last();
                    var endDate = key.StartDateTime.AddDays(key.Duration);
                    if (res.StartDateTime > endDate)
                    {
                        truckInfo[res.CarSpecs].Add(res);
                        OnCarReserved(res);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    truckInfo[res.CarSpecs] = new List<ReservationInfo> { res };
                    OnCarReserved(res);
                }
                return true;
            }


            protected override void OnCarReserved(ReservationInfo e)
            {
                base.OnCarReserved(e);
                Status = $"Truck is reserved from {e.StartDateTime} for {e.Duration} days.";
            }

            public override string ToString()
            {
                return base.ToString() + $"This truck has additional {Features} features.";
            }
        }
        public class Van : Car
        {
            public string Features { get; }
            public string Status { get; set; }

            public Dictionary<CarSpecs, List<ReservationInfo>> vanInfo = new Dictionary<CarSpecs, List<ReservationInfo>>();
            public Van(CarSpecs specs, string features) : base(specs)
            {
                CarSpecs = specs;
                Features = features;
            }
            public override List<ReservationInfo> GetCarInfo()
            {
                return vanInfo.ContainsKey(CarSpecs) ? vanInfo[CarSpecs] : null;
            }

            public override bool ReserveCar(DateTime startDateTime, int days)
            {
                var res = new ReservationInfo(CarSpecs)
                {
                    Duration = days,
                    StartDateTime = startDateTime
                };
                if (vanInfo.ContainsKey(res.CarSpecs))
                {
                    var key = vanInfo[res.CarSpecs].Last();
                    var endDate = key.StartDateTime.AddDays(key.Duration);
                    if (res.StartDateTime > endDate)
                    {
                        vanInfo[res.CarSpecs].Add(res);
                        OnCarReserved(res);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    vanInfo[res.CarSpecs] = new List<ReservationInfo> { res };
                    OnCarReserved(res);
                }
                return true;
            }

            protected override void OnCarReserved(ReservationInfo e)
            {
                base.OnCarReserved(e);
                Status = $"Van is reserved from {e.StartDateTime} for {e.Duration} days.";
            }

            public override string ToString()
            {
                return base.ToString() + $"This van has additional {Features} features.";
            }
        }

    }
}
