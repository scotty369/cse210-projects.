using System;

namespace ExerciseTracking
{
    public abstract class Activity
    {
        private DateTime date;
        private int length; 

        public Activity(DateTime date, int length)
        {
            this.date = date;
            this.length = length;
        }

        public DateTime Date => date;
        public int Length => length;

        public abstract double GetDistance();
        public abstract double GetSpeed();
        public abstract double GetPace();

        public virtual string GetSummary()
        {
            return $"{date.ToShortDateString()} {GetType().Name} ({length} min): Distance {GetDistance():F1} miles, Speed {GetSpeed():F1} mph, Pace: {GetPace():F2} min per mile";
        }
    }

    public class Running : Activity
    {
        private double distance; 

        public Running(DateTime date, int length, double distance)
            : base(date, length)
        {
            this.distance = distance;
        }

        public override double GetDistance() => distance;

        public override double GetSpeed() => (distance / Length) * 60;

        public override double GetPace() => Length / distance;
    }

    public class Cycling : Activity
    {
        private double speed; 

        public Cycling(DateTime date, int length, double speed)
            : base(date, length)
        {
            this.speed = speed;
        }

        public override double GetDistance() => (speed * Length) / 60;

        public override double GetSpeed() => speed;

        public override double GetPace() => 60 / speed;
    }

    public class Swimming : Activity
    {
        private int laps;
        private const double LapDistanceMiles = 50 / 1609.34; 

        public Swimming(DateTime date, int length, int laps)
            : base(date, length)
        {
            this.laps = laps;
        }

        public override double GetDistance() => laps * LapDistanceMiles;

        public override double GetSpeed() => (GetDistance() / Length) * 60;

        public override double GetPace() => Length / GetDistance();
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Activity> activities = new List<Activity>
            {
                new Running(new DateTime(2023, 11, 3), 30, 3.0),
                new Cycling(new DateTime(2023, 11, 3), 30, 20.0),
                new Swimming(new DateTime(2023, 11, 3), 30, 40)
            };

            foreach (Activity activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
            }
        }
    }
}
