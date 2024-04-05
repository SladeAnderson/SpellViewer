using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpellViewer.Models.Entities;

namespace SpellViewer.Models
{
    public interface ITokenRepo
    {
        string? CreateJWTToken(User user, List<string>? roles = null);
    }
}