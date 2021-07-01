using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Mesaj_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-2HOS6DT\SQLEXPRESS;Initial Catalog=Messages;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLKISILER1 where NUMARA=@p1 and SIFRE=@p2", baglanti);//p1 de ki num değeri aynı satırdakı sıfreye esitse
            komut.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Form2 frm = new Form2();
                frm.numara = maskedTextBox1.Text;
                frm.Show();
            }
            else
            {
                MessageBox.Show("Hatalı bilgi");
            }
            baglanti.Close();
        }
    }
}
