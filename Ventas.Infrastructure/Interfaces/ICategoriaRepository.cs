﻿using System.Collections.Generic;
using Ventas.Domain.Entities;
using Ventas.Domain.Repository;
using Ventas.Infrastructure.Models;

namespace Ventas.Infrastructure.Interfaces
{
    public interface ICategoriaRepository : IBaseRepository<Categoria>
    {
        List<CategoriaModels> GetAllCategory();

        List<CategoriaModels> GetCategoria(int IdCategoria);
    }
}
