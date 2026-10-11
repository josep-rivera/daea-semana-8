using Lab7.Datos;
using Lab7.Entidades;

namespace Lab7.Negocio;

public class PersonaNegocio
{
    readonly PersonaDatos datos = new();

    public List<Persona> Listar(string buscar) => datos.Listar(buscar);

    public void Guardar(Persona p)
    {
        if (string.IsNullOrWhiteSpace(p.Nombre) || string.IsNullOrWhiteSpace(p.Apellido))
            throw new ArgumentException("Nombre y apellido son obligatorios.");
        if (p.Dni.Length != 8 || !p.Dni.All(char.IsDigit)) throw new ArgumentException("El DNI debe tener 8 dígitos.");
        if (!p.Email.Contains('@')) throw new ArgumentException("Email no válido.");
        if (string.IsNullOrWhiteSpace(p.Telefono)) throw new ArgumentException("El teléfono es obligatorio.");
        if (p.IdPersona == 0) datos.Insertar(p); else datos.Actualizar(p);
    }

    public void Eliminar(int id) => datos.Eliminar(id);
}

public class ProductoNegocio
{
    readonly ProductoDatos datos = new();

    public List<Producto> Listar(string buscar) => datos.Listar(buscar);

    public void Guardar(Producto p)
    {
        if (string.IsNullOrWhiteSpace(p.Nombre)) throw new ArgumentException("El nombre es obligatorio.");
        if (p.Precio <= 0) throw new ArgumentException("El precio debe ser mayor que 0.");
        if (p.Stock < 0) throw new ArgumentException("El stock no puede ser negativo.");
        if (p.IdProducto == 0) datos.Insertar(p); else datos.Actualizar(p);
    }

    public void Eliminar(int id) => datos.Eliminar(id);
}
