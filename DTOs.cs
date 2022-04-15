using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CUSC_larry
{
    public class Storage
    {
        public IEnumerable<DatWeather> WeatherData { get; }
        public IEnumerable<DatFootball> FootballData { get; }

        public Storage()
        {
            this.WeatherData = DeserializeDatWeather(Utilities.LoadDatFile("weather.dat"));
            this.FootballData = DeserializeDatFootball(Utilities.LoadDatFile("football.dat"));
        }

        public IDatData GetSmallestSpread<T>(IEnumerable<IDatData> data) => data.OrderBy(x => x.Spread).First();

        private IEnumerable<DatWeather> DeserializeDatWeather(byte[] data)
        {
            string raw = Encoding.UTF8.GetString(data);
            char[] extraneous = {' ', '*'};

            return raw.Split("\n")
                .Skip(2).SkipLast(2)
                .Select(x => Regex.Split(x.TrimStart(), @"\s+").Take(3))
                .Select(x => new DatWeather
                {
                    Day = Int32.Parse(x.ElementAt(0)),
                    MaxTemp = Int32.Parse(x.ElementAt(1).TrimEnd(extraneous)),
                    MinTemp = Int32.Parse(x.ElementAt(2).TrimEnd(extraneous))
                });
        }

        private IEnumerable<DatFootball> DeserializeDatFootball(byte[] data)
        {
            string raw = Encoding.UTF8.GetString(data);
            
            return raw.Split("\n")
                .Skip(1).SkipLast(1)
                .Where(x => !x.EndsWith("---"))
                .Select(x => 
                    Regex.Split(x.TrimStart(), @"\s+")
                    .Skip(1)
                    .Where(x => Regex.IsMatch(x, @"\w+")))
                .Select(x => new DatFootball
                {
                    Team = x.ElementAt(0),
                    GoalsFor = Int32.Parse(x.ElementAt(5)),
                    GoalsAgainst = Int32.Parse(x.ElementAt(6))
                });
        }
    }

    public interface IDatData
    {
        public int Spread { get; }
    }
    public class DatWeather : IDatData
    {
        public int Day { get; set; }
        public int MaxTemp { get; set; }
        public int MinTemp { get; set; }
        public int Spread => MaxTemp - MinTemp;
    }

    public class DatFootball : IDatData
    {
        public string Team { get; set;}
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int Spread => GoalsFor - GoalsAgainst;
    }
}