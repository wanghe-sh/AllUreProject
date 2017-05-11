using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class Image
    {
        public int Id { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Url { get; set; }

        public string ThumbnailUrl { get; set; }
    }
}
