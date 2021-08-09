using Game.Entities;
using System.Collections.Generic;

namespace Game.Management.Repositories
{
    static class LocationsRepository
    {
        private static List<Location> _locations;

        public static List<Location> Locations { get => _locations; set => _locations = value; }

        static LocationsRepository()
        {
            _locations = new List<Location>();
        }
    }
}
