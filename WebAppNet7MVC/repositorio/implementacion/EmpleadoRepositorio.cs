using WebAppNet7MVC.models;
using WebAppNet7MVC.repositorio.contrato;
using System.Data; //Para trabajar con ado .net
using System.Data.SqlClient; //Para trabajar con ado .net
using System.Reflection;

namespace WebAppNet7MVC.repositorio.implementacion
{
    public class EmpleadoRepositorio : IGenericRepository<Empleado>
    {
        private readonly string _cadenaSQL = "";

        //Recibe ese parametro para obtener la cadena de conexion que esta en el appConfiguration
        public EmpleadoRepositorio(IConfiguration configuracion)
        {
            _cadenaSQL = configuracion.GetConnectionString("cadenaConexion");
        }

        public async Task<List<Empleado>> Lista()
        {
            List<Empleado> _lista = new List<Empleado>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListaEmpleados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new Empleado
                        {
                            idEmpleado = Convert.ToInt32(dr["idEmpleado"]),
                            nombreCompleto = dr["nombreCompleto"].ToString(),
                            refDepartamento = new Departamento()
                            {
                                idDepartamento = Convert.ToInt32(dr["idDepartamento"]),
                                nombre = dr["nombre"].ToString()
                            },
                            sueldo = Convert.ToInt32(dr["sueldo"]),
                            fechaContrato = dr["fechaContrato"].ToString()
                        });
                    }
                }

            }

            return _lista;
        }

        public async Task<bool> Editar(Empleado modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_EditarEmpleado", conexion);
                cmd.Parameters.AddWithValue("idEmpleado", modelo.idEmpleado);
                cmd.Parameters.AddWithValue("nombreCompleto", modelo.nombreCompleto);
                cmd.Parameters.AddWithValue("idDepartamento", modelo.refDepartamento.idDepartamento);
                cmd.Parameters.AddWithValue("sueldo", modelo.sueldo);
                cmd.Parameters.AddWithValue("fechaContrato", modelo.fechaContrato);
                cmd.CommandType = CommandType.StoredProcedure;

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                if (filasAfectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_EliminarEmpleado", conexion);
                cmd.Parameters.AddWithValue("idEmpleado", id);

                cmd.CommandType = CommandType.StoredProcedure;

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                if (filasAfectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> Guardar(Empleado modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_GuardarEmpleado", conexion);
                cmd.Parameters.AddWithValue("nombreCompleto", modelo.nombreCompleto);
                cmd.Parameters.AddWithValue("idDepartamento", modelo.refDepartamento.idDepartamento);
                cmd.Parameters.AddWithValue("sueldo", modelo.sueldo);
                cmd.Parameters.AddWithValue("fechaContrato", modelo.fechaContrato);
                cmd.CommandType = CommandType.StoredProcedure;

                int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                if (filasAfectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}
