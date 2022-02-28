using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PencatatanHandphone
{
    public partial class JoshFormCell : Form
    {
        // Data List of Smartphone Objects
        public static List<Smartphone> Smartphones = new List<Smartphone>()
        {
           new Smartphone(){Kode = "MK123",Harga = 1200000, Memory = 128, Merk = "SAMSUNG", Processor="SnapDragon 330",Status="Ready !", Tipe="AX33"},
           new Smartphone(){Kode = "MK124",Harga = 2200000, Memory = 64, Merk = "REALME", Processor="SnapDragon 390",Status="Ready !", Tipe="AX32"},
           new Smartphone(){Kode = "MK125",Harga = 1000000, Memory = 32, Merk = "VIVO", Processor="SnapDragon 300",Status="Sold Out!", Tipe="AX31"}
        };

        public JoshFormCell()
        {
            InitializeComponent();
        }

        /*
            Function Form_Load akan dijalankan ketika Form akan di Load
         */
        private void JoshFormCell_Load(object sender, EventArgs e)
        {
            cb_merk.Items.Add("SAMSUNG");
            cb_merk.Items.Add("REALME");
            cb_merk.Items.Add("OPPO");
            cb_merk.Items.Add("VIVO");

            nud_harga.Maximum = long.MaxValue;
            nud_memory.Maximum = long.MaxValue;
            LoadData();
            ClearData();
        }

        private void LoadData()
        {
            // Load PrePopulated Data to Data 
            BindingSource bs = new BindingSource();
            bs.DataSource = JoshFormCell.Smartphones;
            dgv_phone.DataSource = bs;
            int size = Smartphones.Count();
            tb_jmlh.Text = size.ToString();
        }

        private void ClearData()
        {
            tb_kode.Clear();
            cb_merk.SelectedIndex = -1;
            tb_tipe.Clear();
            tb_processor.Clear();
            nud_memory.Value = 0;
            nud_harga.Value = 0;
            rb_status_ready.Checked = false;
            rb_status_soldOut.Checked = false;
        }

        private void dgv_phone_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btn_show_data_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRow = dgv_phone.SelectedRows[0].DataBoundItem as Smartphone;
                tb_kode.Text = selectedRow.Kode;
                cb_merk.SelectedItem = selectedRow.Merk;
                tb_tipe.Text = selectedRow.Tipe;
                tb_processor.Text = selectedRow.Processor;
                nud_memory.Value = selectedRow.Memory;
                nud_harga.Value = selectedRow.Harga;
                
                if(selectedRow.Status == "Ready !")
                {
                    rb_status_ready.Checked = true;
                }
                else
                {
                    rb_status_soldOut.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured : " + ex.Message);
            }
        }

        private void btn_clear_data_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dresult = MessageBox.Show("Are you sure to clear data?", "Confirm", MessageBoxButtons.YesNo);

                if(dresult == DialogResult.Yes)
                {
                    ClearData();
                }
                else
                {
                    dresult = DialogResult.None;
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error Occured : " + ex.Message);
            }
        }

        public bool validate_form()
        {
            bool valid = false;

            // Check tb_kode
            if(tb_kode.Text.Trim() == "")
            {
                MessageBox.Show("Kode harus diisi!");
                tb_kode.Focus();
                valid = false;
            }
            else
            {
                valid = true;
            }
            
            
            if(cb_merk.SelectedIndex == -1)
            {
                MessageBox.Show("Merk harus dipilih!");
                cb_merk.Focus();
                valid = false;
            }
            else
            {
                valid = true;
            }

            if (tb_tipe.Text.Trim() == "")
            {
                MessageBox.Show("Tipe harus diisi !");
                tb_tipe.Focus();
                valid = false;
            }
            else
            {
                valid = true;
            }

            if (tb_processor.Text.Trim() == "")
            {
                MessageBox.Show("Processor harus diisi !");
                tb_processor.Focus();
                valid = false;
            }
            else
            {
                valid = true;
            }

            if (nud_memory.Value == 0)
            {
                MessageBox.Show("Memory tidak boleh 0 !");
                nud_memory.Focus();
                valid = false;
            }
            else
            {
                valid = true;
            }

            if (nud_harga.Value == 0)
            {
                MessageBox.Show("Harga tidak boleh 0 !");
                nud_harga.Focus();
                valid = false;
            }
            else
            {
                valid = true;
            }

            if (rb_status_ready.Checked == false && rb_status_soldOut.Checked == false)
            {
                MessageBox.Show("Kolom status harus dipilih !");
                valid = false;
            }
            else
            {
                valid = true;
            }

            return valid;
        }

        private void btn_tbh_data_Click(object sender, EventArgs e)
        {
            if (validate_form())
            {
                DialogResult dresult = MessageBox.Show("Are you sure to add this data?", "Confirm", MessageBoxButtons.YesNo);

                if(dresult == DialogResult.Yes)
                {

                    string status = rb_status_ready.Checked ? "Ready !" : "Sold Out!";

                    JoshFormCell.Smartphones.Add(new Smartphone()
                    {
                        Kode = tb_kode.Text,
                        Merk = cb_merk.SelectedItem.ToString(),
                        Memory = (long) nud_memory.Value,
                        Harga = (long) nud_harga.Value,
                        Processor = tb_processor.Text,
                        Status = status,
                        Tipe = tb_tipe.Text
                    });
                    ClearData();
                    LoadData();
                }
                else
                {
                    dresult = DialogResult.None;
                }
            }
        }
    }
}