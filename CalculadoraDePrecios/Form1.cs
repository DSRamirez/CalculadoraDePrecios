using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculadoraDePrecios
{
    public partial class btSalir : Form
    {
        public btSalir()
        {
            InitializeComponent();
        }
        private void TxtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Unicamente se aceptan números");
            }
        }

        private void TxtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Unicamente se aceptan números");
            }
        }

        private void TxtPrecio_Validated(object sender, EventArgs e)
        {
            if (txtPrecio.Text.Trim() == "")
            {
                epError.SetError(txtPrecio, "Introducir precio");
                txtPrecio.Focus();
            }
        }

        private void TxtPeso_Validated(object sender, EventArgs e)
        {
            if (txtPeso.Text.Trim() == "")
            {
                epError.SetError(txtPeso, "Introducir peso");
                txtPeso.Focus();
            }
        }

        private void CbProductos_Validated(object sender, EventArgs e)
        {
            if (cbProductos.Items.Count <= 0)
            {
                epError.SetError(cbProductos, "Seleccionar producto");
                cbProductos.Focus();
            }
        }

        private void BtAceptar_Click(object sender, EventArgs e)
        {
            Decimal Peso = Convert.ToDecimal(txtPeso.Text);
            Decimal Precio = Convert.ToDecimal(txtPrecio.Text);
            Decimal Monto = Peso * Precio / 1000;
            Decimal Descuento = Monto * 10 / 100;

            if (Monto >= 100)
            {
                Descuento = Monto * 10 / 100;
            }
            else
            {
                Descuento = 0;
            }

            dgCompra.Rows.Add(cbProductos.SelectedIndex +1, cbProductos.SelectedItem, txtPrecio.Text, txtPeso.Text, 
                Convert.ToString(Monto), Convert.ToString(Descuento));

            txtPeso.Clear();
            txtPrecio.Clear();
            cbProductos.Focus();
        }

        private void BtPagar_Click_1(object sender, EventArgs e)
        {
            Decimal MontoTotal = 0;
            Decimal DescuentoTotal = 0;

            foreach (DataGridViewRow Row in dgCompra.Rows)
            {
                MontoTotal = MontoTotal + Convert.ToDecimal(Row.Cells["MONTO"].Value);
                DescuentoTotal = DescuentoTotal + Convert.ToDecimal(Row.Cells["DESCUENTO"].Value);
            }

            lblTap.Text = "TOTAL A PAGAR: $" + (MontoTotal - DescuentoTotal);
        }

        private void BtCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
