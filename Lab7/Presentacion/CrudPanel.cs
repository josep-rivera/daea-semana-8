namespace Lab7.Presentacion;

class CrudPanel : Panel
{
    readonly TextBox[] cajas;
    readonly TextBox buscar = new() { Width = 250 };
    readonly DataGridView grid = new()
    {
        Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, RowHeadersVisible = false
    };
    readonly Func<string, object> listar;
    readonly Action<int, string[]> guardar;
    readonly Action<int> eliminar;
    int id;

    public CrudPanel(string[] campos, Func<string, object> listar, Action<int, string[]> guardar, Action<int> eliminar)
    {
        this.listar = listar; this.guardar = guardar; this.eliminar = eliminar;
        Dock = DockStyle.Fill;

        var top = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40, Padding = new Padding(8) };
        var btnBuscar = new Button { Text = "Buscar", AutoSize = true };
        btnBuscar.Click += (_, _) => Refrescar();
        buscar.KeyDown += (_, e) => { if (e.KeyCode == Keys.Enter) Refrescar(); };
        top.Controls.AddRange(new Control[] { new Label { Text = "Buscar:", AutoSize = true, Padding = new Padding(0, 6, 0, 0) }, buscar, btnBuscar });

        var form = new TableLayoutPanel { Dock = DockStyle.Bottom, ColumnCount = 2, Padding = new Padding(8), AutoSize = true };
        cajas = new TextBox[campos.Length];
        for (int i = 0; i < campos.Length; i++)
        {
            cajas[i] = new TextBox { Width = 300 };
            form.Controls.Add(new Label { Text = campos[i], AutoSize = true, Padding = new Padding(0, 6, 12, 0) });
            form.Controls.Add(cajas[i]);
        }
        var botones = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 45, Padding = new Padding(8) };
        var btnNuevo = new Button { Text = "Nuevo" }; btnNuevo.Click += (_, _) => Limpiar();
        var btnGuardar = new Button { Text = "Guardar" }; btnGuardar.Click += (_, _) => Accion(() => guardar(id, cajas.Select(c => c.Text.Trim()).ToArray()));
        var btnEliminar = new Button { Text = "Eliminar" };
        btnEliminar.Click += (_, _) =>
        {
            if (id == 0) { MessageBox.Show("Seleccione un registro."); return; }
            if (MessageBox.Show("¿Eliminar el registro?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Accion(() => eliminar(id));
        };
        botones.Controls.AddRange(new Control[] { btnNuevo, btnGuardar, btnEliminar });

        grid.SelectionChanged += (_, _) =>
        {
            if (grid.CurrentRow == null) return;
            var c = grid.CurrentRow.Cells;
            id = Convert.ToInt32(c[0].Value);
            for (int i = 0; i < cajas.Length; i++) cajas[i].Text = Convert.ToString(c[i + 1].Value);
        };

        Controls.Add(grid); Controls.Add(top); Controls.Add(botones); Controls.Add(form);
        Refrescar();
    }

    void Accion(Action a)
    {
        try { a(); Limpiar(); Refrescar(); }
        catch (ArgumentException ex) { MessageBox.Show(ex.Message, "Validación"); }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
    }

    void Refrescar()
    {
        try { grid.DataSource = listar(buscar.Text.Trim()); }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Error de base de datos"); }
    }

    void Limpiar() { id = 0; foreach (var c in cajas) c.Clear(); grid.ClearSelection(); }
}
