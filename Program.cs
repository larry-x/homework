using System;

namespace CUSC_larry
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hmwrk World!");

            Storage storage = new Storage();

            DatWeather weather = (DatWeather)storage.GetSmallestSpread<DatWeather>(storage.WeatherData);
            Console.WriteLine($"Day {weather.Day} had the smallest temperature spread of {weather.Spread} degrees.");

            DatFootball football = (DatFootball)storage.GetSmallestSpread<DatFootball>(storage.FootballData);
            Console.WriteLine($"The team with the least goals scored compared to goals scored against them was {football.Team} with a goals-for goals-against difference of {football.Spread} goals.");

            Console.ReadLine();
        }
    }
}
