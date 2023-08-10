using WebAppNet7MVC.models;
using WebAppNet7MVC.repositorio.contrato;
using System.Data; //Para trabajar con ado .net
using System.Data.SqlClient; //Para trabajar con ado .net

//Esta clase hereda del contrato
namespace WebAppNet7MVC.repositorio.implementacion
{
    public class DepartamentoRepositorio:IGenericRepository<Departamento>
    {
        private readonly string _cadenaSQL = "";

        //Recibe ese parametro para obtener la cadena de conexion que esta en el appConfiguration
        public DepartamentoRepositorio(IConfiguration configuracion)
        {
            _cadenaSQL = configuracion.GetConnectionString("cadenaConexion");
        }

        public Task<bool> Editar(Departamento modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(int d)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Guardar(Departamento modelo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Departamento>> Lista()
        {
            List<Departamento> _lista = new List<Departamento>();

            using(var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListaDepartamentos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while(await dr.ReadAsync())
                    {
                        _lista.Add(new Departamento
                        {
                            idDepartamento = Convert.ToInt32(dr["idDepartamento"]),
                            nombre = dr["nombre"].ToString()
                        }) ; 
                    }
                }

            }

            return _lista;
        }

    }
}
