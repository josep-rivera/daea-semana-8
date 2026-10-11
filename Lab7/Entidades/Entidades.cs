namespace Lab7.Entidades;

public class Persona
{
    public int IdPersona { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Dni { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
}

public class Producto
{
    public int IdProducto { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
}
