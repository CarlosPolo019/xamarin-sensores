using System;
using System.Collections.Generic;
using System.Text;

namespace Parcial.Models
{
    public enum MenuItemType
    {
        
        About,
        ParcialBarometer,
        ParcialOrientation
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
