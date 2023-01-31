using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTestTask.Dtos
{
    public record CreateDogDto
    {
        [Required]
        public string Name { get; init; }

        [Required]
        public string Color { get; init; }

        [Range(1, 1000)]
        public int TailLength { get; init; }

        [Range(1, 1000)]
        public int Weight { get; init; }
    }
}
