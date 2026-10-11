using Lab7.Entidades;
using Microsoft.Data.SqlClient;

namespace Lab7.Datos;

public static class Conexion
{
    public const string Cadena = "Server=localhost,1433;Database=Lab7;User Id=sa;Password=Lab7_Passw0rd!;TrustServerCertificate=True";
}

public class PersonaDatos
{
    public List<Persona> Listar(string buscar)
    {
        List<Persona> lista = new List<Persona>();
        using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
        {
            string sql = "SELECT IdPersona, Nombre, Apellido, Dni, Email, Telefono FROM Persona " +
                         "WHERE Estado = 1 AND (Nombre LIKE @b OR Apellido LIKE @b OR Dni LIKE @b) ORDER BY IdPersona";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@b", "%" + buscar + "%");
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Persona p = new Persona();
                p.IdPersona = dr.GetInt32(0);
                p.Nombre = dr.GetString(1);
                p.Apellido = dr.GetString(2);
                p.Dni = dr.GetString(3);
                p.Email = dr.GetString(4);
                p.Telefono = dr.GetString(5);
                lista.Add(p);
            }
        }
        return lista;
    }

    public void Insertar(Persona p)
    {
        using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
        {
            string sql = "INSERT INTO Persona(Nombre, Apellido, Dni, Email, Telefono) VALUES(@nombre, @apellido, @dni, @email, @telefono)";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            cmd.Parameters.AddWithValue("@apellido", p.Apellido);
            cmd.Parameters.AddWithValue("@dni", p.Dni);
            cmd.Parameters.AddWithValue("@email", p.Email);
            cmd.Parameters.AddWithValue("@telefono", p.Telefono);
            cn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void Actualizar(Persona p)
    {
        using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
        {
            string sql = "UPDATE Persona SET Nombre = @nombre, Apellido = @apellido, Dni = @dni, Email = @email, Telefono = @telefono " +
                         "WHERE IdPersona = @id";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            cmd.Parameters.AddWithValue("@apellido", p.Apellido);
            cmd.Parameters.AddWithValue("@dni", p.Dni);
            cmd.Parameters.AddWithValue("@email", p.Email);
            cmd.Parameters.AddWithValue("@telefono", p.Telefono);
            cmd.Parameters.AddWithValue("@id", p.IdPersona);
            cn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void Eliminar(int id)
    {
        using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
        {
            SqlCommand cmd = new SqlCommand("UPDATE Persona SET Estado = 0 WHERE IdPersona = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);
            cn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}

public class ProductoDatos
{
    public List<Producto> Listar(string buscar)
    {
        List<Producto> lista = new List<Producto>();
        using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
        {
            string sql = "SELECT IdProducto, Nombre, Precio, Stock FROM Producto WHERE Estado = 1 AND Nombre LIKE @b ORDER BY IdProducto";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@b", "%" + buscar + "%");
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Producto p = new Producto();
                p.IdProducto = dr.GetInt32(0);
                p.Nombre = dr.GetString(1);
                p.Precio = dr.GetDecimal(2);
                p.Stock = dr.GetInt32(3);
                lista.Add(p);
            }
        }
        return lista;
    }

    public void Insertar(Producto p)
    {
        using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Producto(Nombre, Precio, Stock) VALUES(@nombre, @precio, @stock)", cn);
            cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            cmd.Parameters.AddWithValue("@precio", p.Precio);
            cmd.Parameters.AddWithValue("@stock", p.Stock);
            cn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void Actualizar(Producto p)
    {
        using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
        {
            SqlCommand cmd = new SqlCommand("UPDATE Producto SET Nombre = @nombre, Precio = @precio, Stock = @stock WHERE IdProducto = @id", cn);
            cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            cmd.Parameters.AddWithValue("@precio", p.Precio);
            cmd.Parameters.AddWithValue("@stock", p.Stock);
            cmd.Parameters.AddWithValue("@id", p.IdProducto);
            cn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void Eliminar(int id)
    {
        using (SqlConnection cn = new SqlConnection(Conexion.Cadena))
        {
            SqlCommand cmd = new SqlCommand("UPDATE Producto SET Estado = 0 WHERE IdProducto = @id", cn);
            cmd.Parameters.AddWithValue("@id", id);
            cn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
