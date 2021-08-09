using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Game.Management.Repositories
{
    public class PlanetIconsRepository
    {
        private static List<Sprite> _icons;

        public static List<Sprite> Icons => _icons;

        static PlanetIconsRepository()
        {
            _icons = new List<Sprite>();
            string path = Environment.CurrentDirectory.Replace(@"\Builds", "") + @"\Assets\Images\Planets";
            string[] links = Directory.GetFiles(path);
            string[] processedLinks = links.Where(s => s.EndsWith(".png") || s.EndsWith(".jpg")).ToArray();
            foreach (string link in processedLinks)
            {
                _icons.Add(SpritesLoader.LoadNewSprite(link));
            }
        }
    }
}
