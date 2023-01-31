using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTestTask.Entities
{
    public record Dog
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int TailLength { get; set; }
        public int Weight { get; set; }
    }
}
