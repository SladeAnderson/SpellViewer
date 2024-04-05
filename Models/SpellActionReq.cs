using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpellViewer.Models
{
    public class SpellActionReq
    {
        public class SpellCreateReq
        {
            [Required]
            public string Casting_time { get; set; } = string.Empty;
            [Required]
            public List<string> Classes { get; set; } = new();
            [Required]
            public ComponentsReqModel Components { get; set; } = new();
            [Required]
            public string Description { get; set; } = string.Empty;
            [Required]
            public string Duration { get; set; } = string.Empty;
            [Required]
            public string Level { get; set; } = string.Empty;
            [Required]
            public string Name { get; set; } = string.Empty;
            [Required]
            public string Range { get; set; } = string.Empty;
            [Required]
            public bool Is_ritual { get; set; }
            [Required]
            public string School { get; set; } = string.Empty;
            [Required]
            public string Type { get; set; } = string.Empty;
        }

        public class ComponentsReqModel
        {
            [Required]
            public bool Is_material { get; set; }
            [Required]
            public List<string>? Materials_needed { get; set; }
            [Required]
            public bool Is_somatic { get; set; }
            [Required]
            public bool Is_verbal { get; set; }
        }

        public class SpellGetReq
        {
            [Required]
            public string Name { get; set; } = string.Empty;
        }

        public class SpellGetbyclassReq
        {
            [Required]
            public string ClassName {get; set;} = string.Empty;
        }

        public class SpellUpdateReq
        {
            [Required]
            public int? Id {get; set;}
            [Required]
            public string Casting_time { get; set; } = string.Empty;
            [Required]
            public List<string> Classes { get; set; } = new();
            [Required]
            public UpadteCompReqModel Components { get; set; } = new();
            [Required]
            public string Description { get; set; } = string.Empty;
            [Required]
            public string Duration { get; set; } = string.Empty;
            [Required]
            public string Level { get; set; } = string.Empty;
            [Required]
            public string Name { get; set; } = string.Empty;
            [Required]
            public string Range { get; set; } = string.Empty;
            [Required]
            public bool Is_ritual { get; set; }
            [Required]
            public string School { get; set; } = string.Empty;
        }

        public class UpadteCompReqModel
        {
            [Required]
            public bool Is_material { get; set; }
            [Required]
            public List<string>? Materials_needed { get; set; }
            [Required]
            public bool Is_somatic { get; set; }
            [Required]
            public bool Is_verbal { get; set; }
        }

        public class SpellDeleteReq
        {
            [Required]
            public int Id {get; set;}

        }

    }

}