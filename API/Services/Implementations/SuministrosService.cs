using API_Farmacia.DTOs;
using API_Farmacia.Models;
using API_Farmacia.Repositories.Interfaces;
using API_Farmacia.Services.Interfaces;

namespace API_Farmacia.Services.Implementations
{
    public class SuministrosService : ISuministrosService
    {
        private readonly ISuministrosRepository _repository;
        public SuministrosService(ISuministrosRepository repository)
        {
            _repository = repository;
        }
        public bool Add(SuministroPostDTO suministro)
        {
            if (!ValidateSuministroPostDTO(suministro))
                return false;

            Suministro newSuministro = new Suministro
            {
                CodBarra = suministro.CodBarra,
                Descripcion = suministro.Descripcion,
                PrecioUnitario = suministro.PrecioUnitario,      // ← CORRECTO
                IdTipoSuministro = (int)suministro.IdTipoVenta,  // ← CORRECTO
                IdTipoVenta = suministro.IdTipoVenta,            // ← CORRECTO
                Stock = suministro.Stock,                        // ← CORRECTO
                UrlImagen = suministro.UrlImagen                 // ← CORRECTO
            };

            _repository.Add(newSuministro);
            return true;
        }


        public bool Delete(int id)
        {
            if (_repository.GetById(id) == null)
                return false;
            else
            {
                _repository.Delete(id);
                return true;
            }
        }

        public List<SuministroDTO> GetAll()
        {
            List<Suministro> suministros = _repository.GetAll();
            List<SuministroDTO> suministrosDTO = new List<SuministroDTO>();
            foreach (var suministro in suministros)
            {
                SuministroDTO suministroDTO = new SuministroDTO
                {
                    IdSuministro = suministro.IdSuministro,
                    CodBarra = suministro.CodBarra,
                    Descripcion = suministro.Descripcion,
                    PrecioUnitario = suministro.PrecioUnitario,
                    IdTipoSuministro = suministro.IdTipoSuministro,
                    IdTipoVenta = suministro.IdTipoVenta,
                    Stock = suministro.Stock,
                    UrlImagen = suministro.UrlImagen
                };
                suministrosDTO.Add(suministroDTO);
            }
            return suministrosDTO;
        }

        public SuministroDTO? GetById(int id)
        {
            Suministro suministro = _repository.GetById(id);
            if (suministro == null)
                return null;
            else
            {
                SuministroDTO suministroDTO = new SuministroDTO
                {
                    IdSuministro = suministro.IdSuministro,
                    CodBarra = suministro.CodBarra,
                    Descripcion = suministro.Descripcion,
                    PrecioUnitario = suministro.PrecioUnitario,
                    IdTipoSuministro = suministro.IdTipoSuministro,
                    IdTipoVenta = suministro.IdTipoVenta,
                    Stock = suministro.Stock,
                    UrlImagen = suministro.UrlImagen
                };
                return suministroDTO;
            }
        }

        public List<SuministroDTO>? GetProductosMasVendidos()
        {
            List<Suministro> suministros = _repository.GetProductosMasVendido();
            List<SuministroDTO> suministrosDTO = new List<SuministroDTO>();
            foreach (var suministro in suministros)
            {
                SuministroDTO suministroDTO = new SuministroDTO
                {
                    IdSuministro = suministro.IdSuministro,
                    CodBarra = suministro.CodBarra,
                    Descripcion = suministro.Descripcion,
                    PrecioUnitario = suministro.PrecioUnitario,
                    IdTipoSuministro = suministro.IdTipoSuministro,
                    IdTipoVenta = suministro.IdTipoVenta,
                    Stock = suministro.Stock,
                    UrlImagen = suministro.UrlImagen
                };
                suministrosDTO.Add(suministroDTO);
            }
            return suministrosDTO;

        }

        public List<TipoSuministroDTO> tiposSuministros()
        {
            var tipos = _repository.tiposSuministros();
            var tiposDTO = tipos.Select(t => new TipoSuministroDTO
            {
                IdTipoSuministro = t.IdTipoSuministro,
                Tipo = t.Tipo
            }).ToList();
            return tiposDTO;
        }

        public bool Update(SuministroPostDTO suministro, int id)
        {
            if (!ValidateSuministroPostDTO(suministro))
                return false;
            else
            {
                Suministro newSuministro = new Suministro // Corrected variable name
                {
                    CodBarra = suministro.CodBarra,
                    Descripcion = suministro.Descripcion,
                    PrecioUnitario = suministro.PrecioUnitario,
                    IdTipoSuministro = suministro.IdTipoSuministro,
                    IdTipoVenta = suministro.IdTipoVenta,
                    Stock = suministro.Stock,
                    UrlImagen = suministro.UrlImagen
                };

                _repository.Update(newSuministro); // Corrected variable name
                return true;
            }
        }




        private bool ValidateSuministroDTO(SuministroDTO suministro)
        {
            if (suministro.CodBarra == null || suministro.CodBarra <= 0)
                return false;
            if (string.IsNullOrWhiteSpace(suministro.Descripcion))
                return false;
            if (suministro.PrecioUnitario == null || suministro.PrecioUnitario <= 0)
                return false;
            if (suministro.IdTipoSuministro == null || suministro.IdTipoSuministro <= 0)
                return false;
            if (suministro.IdTipoVenta == null || suministro.IdTipoVenta <= 0)
                return false;
            if (suministro.Stock == null || suministro.Stock < 0)
                return false;
            return true;
        }

        private bool ValidateSuministroPostDTO(SuministroPostDTO s)
        {
            if (s.CodBarra <= 0) return false;
            if (string.IsNullOrWhiteSpace(s.Descripcion)) return false;
            if (s.PrecioUnitario <= 0) return false;
            if (s.IdTipoSuministro <= 0) return false;
            if (s.IdTipoVenta <= 0) return false;
            if (s.Stock < 0) return false;
            return true;
        }


        public List<SuministroDTO>? GetSuministrosPorTipo(int idTipoSuministro)
        {
            if (idTipoSuministro <= 0)
                return null;
            var suministros = _repository.GetSuministrosPorTipo(idTipoSuministro);
            var suministroDTOs = suministros.Select(s => new SuministroDTO
            {
                IdSuministro = s.IdSuministro,
                CodBarra = s.CodBarra,
                Descripcion = s.Descripcion,
                PrecioUnitario = s.PrecioUnitario,
                IdTipoSuministro = s.IdTipoSuministro,
                IdTipoVenta = s.IdTipoVenta,
                UrlImagen = s.UrlImagen,
                Stock = s.Stock
            }).ToList();
            return suministroDTOs;
        }
    }
}
