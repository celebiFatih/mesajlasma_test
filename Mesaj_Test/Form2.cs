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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-2HOS6DT\SQLEXPRESS;Initial Catalog=Messages;Integrated Security=True");
        public string numara;

        void gelenKutusu()
        {
            SqlDataAdapter da = new SqlDataAdapter("select MESAJID, (AD+ ' ' +SOYAD) as GONDEREN,BASLIK,ICERIK from TBLMESAJLAR inner join TBLKISILER1 on TBLMESAJLAR.GONDEREN=TBLKISILER1.NUMARA where ALICI =" + numara, baglanti);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;
        }

        void gidenKutusu()
        {
            SqlDataAdapter da2 = new SqlDataAdapter("select MESAJID, (AD+ ' ' +SOYAD) as ALICI,BASLIK,ICERIK from TBLMESAJLAR inner join TBLKISILER1 on TBLMESAJLAR.ALICI=TBLKISILER1.NUMARA where GONDEREN=" + numara, baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            lblNumara.Text = numara;
            gelenKutusu();
            gidenKutusu();

            //Ad soyad çekme
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select AD,SOYAD from TBLKISILER1 where numara="+numara,baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLMESAJLAR (GONDEREN,ALICI,BASLIK,ICERIK) VALUES (@p1,@p2,@p3,@p4)",baglanti);
            komut.Parameters.AddWithValue("@p1", numara);
            komut.Parameters.AddWithValue("@p2", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p3", textBox1.Text);
            komut.Parameters.AddWithValue("@p4", richTextBox1.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("mesajınız iletildi");
            gidenKutusu();
        }
    }
}
