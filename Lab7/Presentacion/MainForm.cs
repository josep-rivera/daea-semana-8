using System.Globalization;
using Lab7.Entidades;
using Lab7.Negocio;

namespace Lab7.Presentacion;

class MainForm : Form
{
    public MainForm()
    {
        Text = "Laboratorio 7 - CRUD en capas (ADO.NET)";
        Width = 900; Height = 650;
        var personas = new PersonaNegocio();
        var productos = new ProductoNegocio();

        var tabPersona = new TabPage("Persona (Sección A)");
        tabPersona.Controls.Add(new CrudPanel(
            new[] { "Nombre", "Apellido", "DNI", "Email", "Teléfono" },
            b => personas.Listar(b),
            (id, v) => personas.Guardar(new Persona { IdPersona = id, Nombre = v[0], Apellido = v[1], Dni = v[2], Email = v[3], Telefono = v[4] }),
            personas.Eliminar));

        var tabProducto = new TabPage("Productos (Sección B)");
        tabProducto.Controls.Add(new CrudPanel(
            new[] { "Nombre", "Precio", "Stock" },
            b => productos.Listar(b),
            (id, v) =>
            {
                if (!decimal.TryParse(v[1], NumberStyles.Number, CultureInfo.InvariantCulture, out var precio)) throw new ArgumentException("Precio no válido (use punto decimal).");
                if (!int.TryParse(v[2], out var stock)) throw new ArgumentException("Stock no válido.");
                productos.Guardar(new Producto { IdProducto = id, Nombre = v[0], Precio = precio, Stock = stock });
            },
            productos.Eliminar));

        Controls.Add(new TabControl { Dock = DockStyle.Fill, TabPages = { tabPersona, tabProducto } });
    }
}
