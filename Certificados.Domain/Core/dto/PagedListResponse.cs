using System.Collections.Generic;

namespace Certificados.Domain.Core.DTO
{
    public class PagedListResponse<T>
    {
        public int PaginaCorrente { get; set; }
        public int RegistrosPorPagina { get; set; }
        public bool HasNextPage { get; set; }

        public int? TotalPaginas { get; set; }
        public int? TotalRegistros { get; set; }

        public IEnumerable<T> ListaItens { get; set; }
    }
}
